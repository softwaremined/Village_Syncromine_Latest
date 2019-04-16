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
using Mineware.Systems.WindowsServices.WorkplaceImportPlugin.Models;

namespace Mineware.Systems.WindowsServices.WorkplaceImportPlugin
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
		public string ConsMARTConnectionString => ConfigurationManager.AppSettings["Mineware.Systems.WorkplaceImportPlugin.ConsMART.Connection"];

		public static string ImportErrorLogFile { get; set; }

		private int? _hourToRunImport;
		private int? _minuteToImport;
		private bool _isRunning;

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

				var section = ConfigurationManager.GetSection("WorkplaceImportConnectionSection");
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

				foreach (var connection in connections)
				{
					var connectionString = connection.Connection;
					var operation = connection.Operation;

					HandleWorkplaceImport(operation, connectionString);
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

			_hourToRunImport = Global.GetSettingValueAsInt("Mineware.Systems.WorkplaceImportPlugin.HourToRunImport");
			return _hourToRunImport.Value;
		}

		private int GetMinutesToRunImport()
		{
			if (_minuteToImport != null)
			{
				return _minuteToImport.Value;
			}

			_minuteToImport = Global.GetSettingValueAsInt("Mineware.Systems.WorkplaceImportPlugin.MinuteToRunImport");
			return _minuteToImport.Value;
		}

		private void HandleWorkplaceImport(string operation, string connectionString)
		{
			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Import - Entering HandleWorkplaceImport");
				var connectionHelper = new MsSqlConnectionHelper(connectionString);
				var parameters = new List<SqlParameter>
				{
					new SqlParameter("@Operation", SqlDbType.VarChar, 20) {Value = operation}
				};
				var workplaceValues = connectionHelper.ExecuteTransactionalQuery<Workplace>("sp_SelectList_WorkplaceComplete", parameters.ToArray(), null);
				var table = GetWorkplaceDataTable();
				foreach (var item in workplaceValues)
				{
					GetWorkplaceDataRow(table, item);
				}

				var consmartConnectionHelper = new MsSqlConnectionHelper(ConsMARTConnectionString);

				parameters = new List<SqlParameter>
				{
					new SqlParameter("@Data", SqlDbType.Structured) {Value = table}
				};
				consmartConnectionHelper.ExecuteTransactionalInstruction("sp_InsertUpdateDelete_Workplace", parameters.ToArray(), null);
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Import - Failed to import workplaces successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				throw;
			}
			finally
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Import - Exiting HandleWorkplaceImport");
			}
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

		private DataTable GetWorkplaceDataTable()
		{
			var properties = typeof(Workplace).GetProperties(BindingFlags.Public | BindingFlags.Instance);

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

		private void GetWorkplaceDataRow(DataTable table, Workplace instance)
		{
			var properties = typeof(Workplace).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var values = new List<object>();
			foreach (var property in properties)
			{
				values.Add(property.GetValue(instance, null));
			}

			table.Rows.Add(values.ToArray());
		}
	}
}
