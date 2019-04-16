using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mineware.Systems.WindowsServices.Configuration;
using Mineware.Systems.WindowsServices.Helpers;
using Mineware.Systems.WindowsServices.ImportPlugin.Attributes;
using Mineware.Systems.WindowsServices.ImportPlugin.Models;
using Mineware.Systems.WindowsServices.ImportPlugin.ViewModels;

namespace Mineware.Systems.WindowsServices.ImportPlugin
{
	internal struct CacheItem
	{
		public int Hour { get; set; }

		public int Minute { get; set; }

		public DateTime RunDate { get; set; }
	}
	internal static class RunCache
	{
		public static readonly List<CacheItem> _cacheItems = new List<CacheItem>();

		public static bool HasRun(CacheItem cacheItem)
		{
			return _cacheItems.Any(x => x.Hour == cacheItem.Hour
										&& x.Minute == cacheItem.Minute
										&& x.RunDate.ToString("yyyy/MM/dd HH:mm") == cacheItem.RunDate.ToString("yyyy/MM/dd HH:mm"));
		}

		public static void SetRun(CacheItem cacheItem)
		{
			if (_cacheItems.Contains(cacheItem))
			{
				return;
			}

			_cacheItems.Add(cacheItem);
		}
	}
	public class Plugin : BaseWinNtServicePlugin
	{
		private int? _hourToRunImport;
		private int? _minuteToImport;
		private bool _isRunning;

		public string ConsMARTConnectionString => ConfigurationManager.AppSettings["Mineware.Systems.ImportUtility.ConsMART.Connection"];

		public static string ImportErrorLogFile { get; set; }

		protected override void RunInstance()
		{
			var now = DateTime.Now;
			if (!RunImport(now) || _isRunning)
			{
				return;
			}

			try
			{
				var cacheItem = new CacheItem
				{
					Hour = GetHoursToRunImport(),
					Minute = GetMinutesToRunImport(),
					RunDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)
				};
				if (RunCache.HasRun(cacheItem))
				{
					return;
				}
				RunCache.SetRun(cacheItem);

				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Start Running Import");
				_isRunning = true;

				var connections = new List<ConnectionConfig>();

				var section = ConfigurationManager.GetSection("ImportUtilityConnectionSection");
				if (section != null)
				{
					var jobs = (section as ConnectionSection).Connections;
					LogMessage($"[{Thread.CurrentThread.ManagedThreadId}] - Job Count = {jobs.Count}");
					for (int i = 0; i < jobs.Count; i++)
					{
						connections.Add(jobs[i]);
						LogMessage($"[{Thread.CurrentThread.ManagedThreadId}] - {jobs[i].Operation} -> {jobs[i].Connection}");
					}
				}
				else
				{
					LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Could not retrieve ConnectionSection");
					return;
				}

				foreach (var connection in connections)
				{
					var connectionString = connection.Connection;
					var operation = connection.Operation;

					HandleDailyImport(operation, connectionString);
					HandleMonthlyImport(operation, connectionString);
					HandleCalendarImport(connectionString, operation);
				}
			}
			catch (Exception ex)
			{
				Directory.CreateDirectory(Thread.GetDomain().BaseDirectory + "\\ErrorLog");
				if (string.IsNullOrEmpty(ImportErrorLogFile))
				{
					ImportErrorLogFile = "\\ErrorLog\\Generic_ErrorLog.txt";
				}
				using (var sw = File.AppendText(Thread.GetDomain().BaseDirectory + ImportErrorLogFile))
				{
					sw.WriteLine(ex.Message);
				}
			}
			finally
			{
				_isRunning = false;
			}
		}

		private bool RunImport(DateTime currentTime)
		{
			return currentTime.Hour == GetHoursToRunImport() && currentTime.Minute == GetMinutesToRunImport();
		}

		private int GetHoursToRunImport()
		{
			if (_hourToRunImport != null)
			{
				return _hourToRunImport.Value;
			}

			_hourToRunImport = Global.GetSettingValueAsInt("Mineware.Systems.ImportUtility.HourToRunImport");
			return _hourToRunImport.Value;
		}

		private int GetMinutesToRunImport()
		{
			if (_minuteToImport != null)
			{
				return _minuteToImport.Value;
			}

			_minuteToImport = Global.GetSettingValueAsInt("Mineware.Systems.ImportUtility.MinuteToRunImport");
			return _minuteToImport.Value;
		}

		private void HandleDailyImport(string operation, string connectionString)
		{
			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Start Daily Import");
				var connectionHelper = new MsSqlConnectionHelper(connectionString);

				var sqlBuilder = new StringBuilder();
				sqlBuilder.AppendLine("Select CurrentProductionMonth, dbo.GetPrevProdMonth(CurrentProductionMonth) PreviousProductionMonth");
				sqlBuilder.AppendLine("From SysSet");
				var prodMonths = connectionHelper.ExecuteTransactionalQuery<SysSetProdMonths>(null, null, sqlBuilder.ToString()).Single();
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Retrieved SysSet");

				connectionHelper = new MsSqlConnectionHelper(connectionString);
				var parameters = new List<SqlParameter>
				{
					new SqlParameter("@Operation", SqlDbType.VarChar, 20) {Value = operation},
					new SqlParameter("@FromMonth", SqlDbType.Int) {Value = prodMonths.PreviousProductionMonth},
					new SqlParameter("@CalendarDate", SqlDbType.Date) {Value = DBNull.Value},
				};
				var planBookValues = connectionHelper.ExecuteTransactionalQuery<PlanBookValues>("sp_Select_PlanBookValues", parameters.ToArray(), null);
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Retrieved PlanBookValues");
				

				var splitPlanBookValues = GetSplitPlanBookValues(planBookValues.ToList());

				LogMessage($"ConsMART Import - {operation} : Data split into {splitPlanBookValues.Count} pages");

				foreach (var keyValuePair in splitPlanBookValues)
				{
					LogMessage($"ConsMART Import - {operation} : Processing Page {keyValuePair.Key}");

					var consmartConnectionHelper = new MsSqlConnectionHelper(ConsMARTConnectionString);
					var table = GetProductionDataTable();

					foreach (var item in keyValuePair.Value)
					{
						var units = GetUnits(item).ToList();
						foreach (var unit in units)
						{
							table.Rows.Add(operation, item.ProdMonth, item.Calendardate, item.SectionId, item.SectionName, item.SectionId_1, item.SectionName_1,
								item.SectionId_2, item.SectionName_2, item.SectionId_3, item.SectionName_3, item.SectionId_4, item.SectionName_4, item.SectionId_5, item.SectionName_5,
								item.OrgUnitDay, item.OrgUnitAfternoon, item.OrgUnitNight, item.WorkplaceId, item.WorkplaceDescription, item.WorkplaceReefWaste, item.WorkplaceGridCode,
								item.WorkplaceDivisionCode, item.LevelNumber, item.ShiftDay, item.WorkingDay, item.ActivityCode, item.ActivityDesc, unit.Version, unit.UnitOfMeasure, unit.Value ?? string.Empty);
						}
					}

					parameters = new List<SqlParameter>
					{
						new SqlParameter("@data", SqlDbType.Structured) {Value = table}
					};
					LogMessage($"ConsMART Import - {operation} : Attempting Insert of {table.Rows.Count} rows");


					foreach (DataRow _dr in table.AsEnumerable()
						.GroupBy(r => new
						{
							c1 = r.Field<string>("Operation"),
							c2 = r.Field<string>("ProdMonth"),
							c3 = r.Field<DateTime>("CalendarDate"),
							c4 = r.Field<string>("SectionId"),
							c5 = r.Field<string>("SectionName"),
							c6 = r.Field<string>("SectionId_1"),
							c7 = r.Field<string>("SectionName_1"),
							c8 = r.Field<string>("SectionId_2"),
							c9 = r.Field<string>("SectionName_2"),
							c10 = r.Field<string>("SectionId_3"),
							c11 = r.Field<string>("SectionName_3"),
							c12 = r.Field<string>("SectionId_4"),
							c13 = r.Field<string>("SectionName_4"),
							c14 = r.Field<string>("SectionId_5"),
							c15 = r.Field<string>("SectionName_5"),
							c16 = r.Field<string>("WorkplaceID"),
							c17 = r.Field<string>("WorkplaceDescription"),
							c18 = r.Field<string>("WorkplaceReefWaste"),
							c19 = r.Field<string>("WorkplaceGridCode"),
							c20 = r.Field<string>("WorkplaceDivisionCode"),
							c21 = r.Field<string>("LevelNumber"),
							c22 = r.Field<string>("ShiftDay"),
							c23 = r.Field<string>("WorkingDay"),
							c24 = r.Field<string>("Activity_Code"),
							c25 = r.Field<string>("Activity_Desc"),
							c26 = r.Field<string>("Version"),
							c27 = r.Field<string>("UnitOfMeasure")

						}).Where(grp => grp.Count() > 1).SelectMany(itm => itm))
					{
						StringBuilder output = new StringBuilder();
						foreach (DataColumn col in table.Columns)
						{
							output.AppendFormat("{0} ", _dr[col]);
						}

						LogMessage($"ConsMART Import - {operation} : Duplicate Record: {output}");
						// Handle your Duplicate row entry
					}

					consmartConnectionHelper.ExecuteTransactionalInstruction("sp_InsertUpdate_ProductionDataUsingTable", parameters.ToArray(), null);
					LogMessage($"ConsMART Import - {operation} : Imported {table.Rows.Count} rows");
				}
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Failed to import daily successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				throw;
			}
			finally
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Exiting Daily Import");
			}
		}

		private Dictionary<int, IEnumerable<PlanBookValues>> GetSplitPlanBookValues(List<PlanBookValues> input)
		{
			var result = new Dictionary<int, IEnumerable<PlanBookValues>>();
			var pageCount = 1;
			var pageSize = 1500;

			while (true)
			{
				var items = input.Skip(pageCount * pageSize)
						   .Take(pageSize);
				if (!items.Any())
				{
					break;
				}

				result.Add(pageCount, items);
				pageCount++;
			}

			return result;
		}

		private void HandleMonthlyImport(string operation, string connectionString)
		{
			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Start Monthly Import");
				var connectionHelper = new MsSqlConnectionHelper(connectionString);

				var sqlBuilder = new StringBuilder();
				sqlBuilder.AppendLine("Select CurrentProductionMonth, dbo.GetPrevProdMonth(CurrentProductionMonth) PreviousProductionMonth");
				sqlBuilder.AppendLine("From SysSet");
				var prodMonths = connectionHelper.ExecuteTransactionalQuery<SysSetProdMonths>(null, null, sqlBuilder.ToString()).Single();

				var parameters = new List<SqlParameter>
				{
					new SqlParameter("@Operation", SqlDbType.VarChar, 20) {Value = operation},
					new SqlParameter("@FromMonth", SqlDbType.Int) {Value = prodMonths.PreviousProductionMonth}
				};
				var planBookValues = connectionHelper.ExecuteTransactionalQuery<PlanMeasValues>("sp_Select_PlanMeasValues", parameters.ToArray(), null);

				var consmartConnectionHelper = new MsSqlConnectionHelper(ConsMARTConnectionString);


				var rem = 0;
				//var count = Math.DivRem(planBookValues.Count(),100, out rem);

				var tables = new List<DataTable>();

				var distinctValues = planBookValues.Distinct().ToList();

				DataTable table = null;
				var counter = 0;
				foreach (var item in distinctValues)
				{
					Math.DivRem(counter, 100, out rem);
					if (rem == 0)
					{
						table = GetProductionDataMonthlyTable();
						tables.Add(table);
					}

					counter++;
					var units = GetPlanMeasUnits(item).OrderBy(x => x.UnitOfMeasure).ThenBy(x => x.Version).ToList();
					foreach (var unit in units)
					{
						table.Rows.Add(operation, item.ProdMonth, item.SectionId, item.SectionName, item.SectionId_1, item.SectionName_1,
							item.SectionId_2, item.SectionName_2, item.SectionId_3, item.SectionName_3, item.SectionId_4, item.SectionName_4, item.SectionId_5, item.SectionName_5,
							item.OrgUnitDay, item.OrgUnitAfternoon, item.OrgUnitNight, item.WorkplaceId, item.WorkplaceDescription, item.WorkplaceReefWaste, item.WorkplaceGridCode,
							item.WorkplaceDivisionCode, item.LevelNumber, item.ActivityCode, item.ActivityDesc, item.SurveySequenceNo, unit.Version, unit.UnitOfMeasure, unit.Value ?? string.Empty);
					}
					
				}

				foreach (var item in tables)
				{
					var view = new DataView(item);
					view.Sort = "Operation, ProdMonth, SectionId, WorkplaceId, UnitOfMeasure, Version";
					var temp = view.ToTable();

					parameters = new List<SqlParameter>
					{
						new SqlParameter("@data", SqlDbType.Structured) {Value = temp}
					};
					consmartConnectionHelper.ExecuteTransactionalInstruction("sp_InsertUpdate_ProductionDataMonthlyUsingTable", parameters.ToArray(), null);
					LogMessage($"ConsMART Import - {operation} : {item.Rows.Count} rows");
				}
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Failed to import monthly successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				throw;
			}
			finally
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Exiting Monthly Import");
			}
		}

		private void HandleCalendarImport(string connectionString, string mine)
		{
			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Start Calendar Import");
				var connectionHelper = new MsSqlConnectionHelper(connectionString);

				var sqlBuilder = new StringBuilder();
				sqlBuilder.AppendLine($"Select c.CalendarDate, c.WorkingDay, s.ProdMonth, s.SectionId, s.CalendarCode, s.BeginDate, s.EndDate, s.TotalShifts, '{mine}' Mine");
				sqlBuilder.AppendLine("from caltype c ");
				sqlBuilder.AppendLine("inner join SECCAL s on c.CalendarCode = s.CalendarCode ");
				sqlBuilder.AppendLine("				and c.CalendarDate >= s.BeginDate ");
				sqlBuilder.AppendLine("				and c.CalendarDate <= s.EndDate ");
				var calendarData = connectionHelper.ExecuteTransactionalQuery<ConsMartCalendar>(null, null, sqlBuilder.ToString());

				var table = GetCalendarsDataTable();
				foreach (var item in calendarData)
				{
					GetCalendarDataRow(table, item);
				}

				var consmartConnectionHelper = new MsSqlConnectionHelper(ConsMARTConnectionString);
				var parameters = new List<SqlParameter>
				{
					new SqlParameter("@data", SqlDbType.Structured) {Value = table}
				};
				consmartConnectionHelper.ExecuteTransactionalInstruction("sp_InsertUpdate_Calendars", parameters.ToArray(), null);
				LogMessage($"ConsMART Import - {mine} : {table.Rows.Count} rows");
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Failed to import calendars successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				throw;
			}
			finally
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : ConsMART Import - Exiting Calendar Import");
			}
		}

		private IEnumerable<UnitValueViewModel> GetUnits(PlanBookValues item)
		{
			var properties = typeof(PlanBookValues).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var property in properties)
			{
				var customAttribute = property.GetCustomAttribute<UnitAttribute>();
				if (customAttribute != null)
				{
					var value = property.GetValue(item, null);
					var model = new UnitValueViewModel
					{
						UnitOfMeasure = customAttribute.UnitOfMeasure,
						Version = customAttribute.Version,
						Value = value?.ToString()
					};
					if (item.ActivityCode == 0 || item.ActivityCode == 9)
					{
						model.UnitOfMeasure = customAttribute.StopeUnitOfMeasure ?? customAttribute.UnitOfMeasure;
					}
					else if (item.ActivityCode == 1)
					{
						model.UnitOfMeasure = customAttribute.DevUnitOfMeasure ?? customAttribute.UnitOfMeasure;
					}
					yield return model;
				}
			}
		}

		private IEnumerable<UnitValueViewModel> GetPlanMeasUnits(PlanMeasValues item)
		{
			var properties = typeof(PlanMeasValues).GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var property in properties)
			{
				var customAttribute = property.GetCustomAttribute<UnitAttribute>();
				if (customAttribute != null)
				{
					var value = property.GetValue(item, null);
					var model = new UnitValueViewModel
					{
						UnitOfMeasure = customAttribute.UnitOfMeasure,
						Version = customAttribute.Version,
						Value = value?.ToString()
					};
					if (item.ActivityCode == 0 || item.ActivityCode == 9)
					{
						model.UnitOfMeasure = customAttribute.UnitOfMeasure;
					}
					else if (item.ActivityCode == 1)
					{
						model.UnitOfMeasure = customAttribute.UnitOfMeasure;
					}
					yield return model;
				}
			}
		}

		private DataTable GetProductionDataTable()
		{
			var table = new DataTable();

			table.Columns.Add("Operation", typeof(string));
			table.Columns.Add("ProdMonth", typeof(string));
			table.Columns.Add("CalendarDate", typeof(DateTime));
			table.Columns.Add("SectionId", typeof(string));
			table.Columns.Add("SectionName", typeof(string));
			table.Columns.Add("SectionId_1", typeof(string));
			table.Columns.Add("SectionName_1", typeof(string));
			table.Columns.Add("SectionId_2", typeof(string));
			table.Columns.Add("SectionName_2", typeof(string));
			table.Columns.Add("SectionId_3", typeof(string));
			table.Columns.Add("SectionName_3", typeof(string));
			table.Columns.Add("SectionId_4", typeof(string));
			table.Columns.Add("SectionName_4", typeof(string));
			table.Columns.Add("SectionId_5", typeof(string));
			table.Columns.Add("SectionName_5", typeof(string));
			table.Columns.Add("OrgUnitDay", typeof(string));
			table.Columns.Add("OrgUnitAfternoon", typeof(string));
			table.Columns.Add("OrgUnitNight", typeof(string));
			table.Columns.Add("WorkplaceID", typeof(string));
			table.Columns.Add("WorkplaceDescription", typeof(string));
			table.Columns.Add("WorkplaceReefWaste", typeof(string));
			table.Columns.Add("WorkplaceGridCode", typeof(string));
			table.Columns.Add("WorkplaceDivisionCode", typeof(string));
			table.Columns.Add("LevelNumber", typeof(string));
			table.Columns.Add("ShiftDay", typeof(string));
			table.Columns.Add("WorkingDay", typeof(string));
			table.Columns.Add("Activity_Code", typeof(string));
			table.Columns.Add("Activity_Desc", typeof(string));
			table.Columns.Add("Version", typeof(string));
			table.Columns.Add("UnitOfMeasure", typeof(string));
			table.Columns.Add("Amount", typeof(string));

			return table;
		}

		private DataTable GetProductionDataMonthlyTable()
		{
			var table = new DataTable();

			table.Columns.Add("Operation", typeof(string));
			table.Columns.Add("ProdMonth", typeof(string));
			table.Columns.Add("SectionId", typeof(string));
			table.Columns.Add("SectionName", typeof(string));
			table.Columns.Add("SectionId_1", typeof(string));
			table.Columns.Add("SectionName_1", typeof(string));
			table.Columns.Add("SectionId_2", typeof(string));
			table.Columns.Add("SectionName_2", typeof(string));
			table.Columns.Add("SectionId_3", typeof(string));
			table.Columns.Add("SectionName_3", typeof(string));
			table.Columns.Add("SectionId_4", typeof(string));
			table.Columns.Add("SectionName_4", typeof(string));
			table.Columns.Add("SectionId_5", typeof(string));
			table.Columns.Add("SectionName_5", typeof(string));
			table.Columns.Add("OrgUnitDay", typeof(string));
			table.Columns.Add("OrgUnitAfternoon", typeof(string));
			table.Columns.Add("OrgUnitNight", typeof(string));
			table.Columns.Add("WorkplaceID", typeof(string));
			table.Columns.Add("WorkplaceDescription", typeof(string));
			table.Columns.Add("WorkplaceReefWaste", typeof(string));
			table.Columns.Add("WorkplaceGridCode", typeof(string));
			table.Columns.Add("WorkplaceDivisionCode", typeof(string));
			table.Columns.Add("LevelNumber", typeof(string));
			table.Columns.Add("Activity_Code", typeof(string));
			table.Columns.Add("Activity_Desc", typeof(string));
			table.Columns.Add("SequenceNumber", typeof(string));
			table.Columns.Add("Version", typeof(string));
			table.Columns.Add("UnitOfMeasure", typeof(string));
			table.Columns.Add("Amount", typeof(string));

			return table;
		}

		private DataTable GetCalendarsDataTable()
		{
			var properties = typeof(Calendars).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			var table = new DataTable();

			foreach (var property in properties)
			{
				if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType));
				}
				else
				{
					table.Columns.Add(property.Name, property.PropertyType);
				}
			}

			return table;
		}

		private void GetCalendarDataRow(DataTable table, ConsMartCalendar instance)
		{
			var properties = typeof(ConsMartCalendar).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var values = new List<object>();
			foreach (var property in properties)
			{
				values.Add(property.GetValue(instance, null));
			}

			table.Rows.Add(values.ToArray());
		}

		private static void LogMessage(string message)
		{
			try
			{

				var baseDirectory = Thread.GetDomain().BaseDirectory;
				var directory = Path.Combine(baseDirectory, "ImportLog");
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				using (var sw = File.AppendText(Path.Combine(directory, "LogFile.txt")))
				{
					sw.WriteLine(message);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}

	public class ConsMartCalendar
	{
		public DateTime CalendarDate { get; set; }

		public string WorkingDay { get; set; }

		public string ProdMonth { get; set; }

		public string SectionId { get; set; }

		public string CalendarCode { get; set; }

		public DateTime BeginDate { get; set; }

		public DateTime EndDate { get; set; }

		public int TotalShifts { get; set; }

		public string Mine { get; set; }
	}
}
