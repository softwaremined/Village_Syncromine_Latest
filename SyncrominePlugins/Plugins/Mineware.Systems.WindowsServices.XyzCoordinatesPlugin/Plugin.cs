using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Mineware.Systems.WindowsServices.Extensions;

namespace Mineware.Systems.WindowsServices.XyzCoordinatesPlugin
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

		protected override void RunInstance()
		{
			var now = DateTime.Now;
			if (!RunImport(now) || _isRunning)
			{
				return;
			}

			try
			{
				ImportLogFile = $@"\ImportLog\ImportLog_{DateTime.Now:yyyyMMddTHHmmss}.txt";

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

				var pasConnectionString = GetPasConnection();
				using (var pasConnection = new SqlConnection(pasConnectionString))
				{
					pasConnection.Open();
					var gmsiConnectionString = GetGmsiConnection();
					using (var gmsiConnection = new SqlConnection(gmsiConnectionString))
					{
						Import(gmsiConnection, pasConnection);
						using (var cmd = new SqlCommand($"DBCC SHRINKFILE({Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.DestinationDatabaseLog")}, 1);", pasConnection))
						{
							if (pasConnection.State != ConnectionState.Open)
							{
								pasConnection.Open();
							}
							cmd.ExecuteNonQuery();
						}
					}
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

			_hourToRunImport = Global.GetSettingValueAsInt("Mineware.Systems.XyzImportPlugin.HourToRunImport");
			return _hourToRunImport.Value;
		}

		private int GetMinutesToRunImport()
		{
			if (_minuteToImport != null)
			{
				return _minuteToImport.Value;
			}

			_minuteToImport = Global.GetSettingValueAsInt("Mineware.Systems.XyzImportPlugin.MinuteToRunImport");
			return _minuteToImport.Value;
		}

		public string ImportLogFile { get; set; }

		public static string ImportErrorLogFile { get; set; }

		public static string CsvPath => Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.CsvPath");

		private string GetGmsiConnection()
		{
			return
				$"Server={Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.GmsiDatabaseServer")};" +
				$"Database={Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.GmsiDatabase")};" +
				$"User Id={Global.Decrypt(Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.GmsiDatabaseUserId"))};" +
				$"Password={Global.Decrypt(Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.GmsiDatabasePassword"))};";
		}

		public static string GetPasConnection()
		{
			return
				$"Server={Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.DestinationDatabaseServer")};" +
				$"Database={Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.DestinationDatabase")};" +
				$"User Id={Global.Decrypt(Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.DestinationDatabaseUserId"))};" +
				$"Password={Global.Decrypt(Global.GetSettingValueAsString("Mineware.Systems.XyzImportPlugin.DestinationDatabasePassword"))};";
		}

		public void Import(SqlConnection gmsiConnection, SqlConnection pasConnection)
		{
			var dtPlanningWorkplace = GetPlanningAndWorkplace(pasConnection);
			if (dtPlanningWorkplace.Rows.Count <= 0)
			{
				return;
			}

			DeleteFromXyzCoordinates(pasConnection);
			ImportIntoXyzCoordinates(pasConnection, gmsiConnection, dtPlanningWorkplace);
			CreateCsvFile(pasConnection);
		}

		private DataTable GetPlanningAndWorkplace(SqlConnection destinationConnection)
		{
			if (destinationConnection.State != ConnectionState.Open)
			{
				destinationConnection.Open();
			}

			var sql = new StringBuilder();
			sql.AppendLine("DECLARE @FromDate date");
			sql.AppendLine("DECLARE @ToDate date");

			sql.AppendLine("SET @FromDate = (SELECT CONVERT(VARCHAR(10), GETDATE() - 8, 120))");
			sql.AppendLine("SET @ToDate = (SELECT CONVERT(VARCHAR(10), GETDATE(), 120))");

			sql.AppendLine("SELECT p.*, w.Description OldWPName, w.Description, ISNULL(p.Booksqm, 0) AS SQM, ISNULL(p.Bookfl, 0) SW");
			sql.AppendLine("	FROM Planning p with(NoLock)");
			sql.AppendLine("INNER JOIN Workplace w with(NoLock) ON w.WorkplaceID = p.WorkplaceID");
			sql.AppendLine("WHERE p.Activity = 0");
			//sql.AppendLine("AND p.booksqm > 0");
			sql.AppendLine("AND p.CalendarDate between @FromDate AND @ToDate;");

			using (var destinationAdapter = new SqlDataAdapter(sql.ToString(), destinationConnection))
			{
				var dtPlanningWorkplace = new DataTable();
				destinationAdapter.Fill(dtPlanningWorkplace);
				LogMessage($"[{DateTime.Now}] - {dtPlanningWorkplace.Rows.Count} records available from Planning and Workplace after join.");
				return dtPlanningWorkplace;
			}
		}

		private void DeleteFromXyzCoordinates(SqlConnection destinationConnection)
		{
			if (destinationConnection.State != ConnectionState.Open)
			{
				destinationConnection.Open();
			}

			using (var destinationCommand = new SqlCommand())
			{
				destinationCommand.Connection = destinationConnection;

				destinationCommand.CommandText = "DELETE FROM XYZCoordinates WHERE Blast_Date >= (SELECT CONVERT(VARCHAR(10), GETDATE() - 8, 120));";
				destinationCommand.CommandText += "DELETE FROM XYZCoordinates_Errors WHERE Blast_Date >= (SELECT CONVERT(VARCHAR(10), GETDATE() - 8, 120));";
				var count = destinationCommand.ExecuteNonQuery();
				LogMessage($"[{count}] - records deleted from XYZCoordinates and XYZCoordinates_Errors.");
			}
		}

		private void ImportIntoXyzCoordinates(SqlConnection pasConnection, SqlConnection gmsiConnection, DataTable dtPlanningWorkplace)
		{
			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : XYZ Import - Start Import");
				if (pasConnection.State != ConnectionState.Open)
				{
					pasConnection.Open();
				}

				if (gmsiConnection.State != ConnectionState.Open)
				{
					gmsiConnection.Open();
				}

				foreach (DataRow drPlanningWorkplace in dtPlanningWorkplace.Rows)
				{
					var dtCalendarDate = GetCalendarDates(gmsiConnection, drPlanningWorkplace);
					if (dtCalendarDate.Rows.Count > 1)
					{
						var intCount = 1;
						float fltX1 = 0, fltX2 = 0, fltY1 = 0, fltY2 = 0, fltZ1 = 0, fltZ2 = 0;
						var dtBookDate = DateTime.Now;
						var blnRecordFound = false;

						foreach (DataRow drCalendarDate in dtCalendarDate.Rows)
						{
							var dtSampleSectionView = GetDataFromSampleSectionView(gmsiConnection, drCalendarDate, drPlanningWorkplace["Description"].ToString());
							if (dtSampleSectionView.Rows.Count <= 0)
							{
								continue;
							}
							var sectionx = dtSampleSectionView.Rows[0]["Section_X"];
							var sectiony = dtSampleSectionView.Rows[0]["Section_Y"];
							var sectionz = dtSampleSectionView.Rows[0]["Section_Z"];
							switch (intCount)
							{
								case 1:
									fltX2 = Convert.ToSingle(sectionx);
									fltY2 = Convert.ToSingle(sectiony);
									fltZ2 = Convert.ToSingle(sectionz);
									dtBookDate = Convert.ToDateTime(drCalendarDate["CalendarDate"]);
									break;
								case 2:
									fltX1 = Convert.ToSingle(sectionx);
									fltY1 = Convert.ToSingle(sectiony);
									fltZ1 = Convert.ToSingle(sectionz);
									break;
							}
							intCount++;
							blnRecordFound = true;
						}

						if (!blnRecordFound)
						{
							continue;
						}

						var dtPlanning = GetDataFromPlanning(pasConnection, drPlanningWorkplace, dtBookDate);
						float adv = 0;
						if (dtPlanning.Rows.Count > 0)
						{
							adv = Convert.ToSingle(dtPlanning.Rows[0]["Advance"] == DBNull.Value ? 0 : dtPlanning.Rows[0]["Advance"]);
						}

						var dblD = Math.Sqrt(Math.Pow(Math.Abs(fltX2 - fltX1), 2) + Math.Pow(Math.Abs(fltY2 - fltY1), 2));
						if (dblD == 0)
						{
							InsertIntoXyzCoordinatesErrors(pasConnection, drPlanningWorkplace, "The Ratio Calculated value = 0");
						}
						else
						{
							float fltX = fltX2,
								fltY = fltY2,
								fltZ = fltZ2;

							var xCalc = fltX2 - fltX1;
							if (xCalc != 0)
							{
								fltX = fltX1 + (xCalc + adv) / xCalc * xCalc;
							}

							var yCalc = fltY2 - fltY1;
							if (fltY2 - fltY1 != 0)
							{
								fltY = fltY1 + (yCalc + adv) / yCalc * yCalc;
							}

							var zCalc = fltZ2 - fltZ1;
							if (zCalc != 0)
							{
								fltZ = fltZ1 + (zCalc + adv) / zCalc * zCalc;
							}

							var dtBlastDate = Convert.ToDateTime(drPlanningWorkplace["Calendardate"]);
							var dtExDate = DateTime.Now;

							var sql = new StringBuilder();
							sql.AppendLine("INSERT INTO XYZCoordinates");
							sql.AppendLine("(Export_Date, Blast_Date, X_Coordinate, Y_Coordinate, Z_Coordinate, Activity, SQM,");
							sql.AppendLine("	WPID, Description, ProdMonth, StopeWidth, Blast_Time, BL_Date, Ex_Date, X1, Y1, Z1, X2, Y2, Z2, Adv)");
							sql.AppendLine("VALUES(");
							sql.AppendLine("	(SELECT CONVERT(VARCHAR(10), GETDATE(), 120)), @Blast_Date, @X_Coordinate, @Y_Coordinate, @Z_Coordinate, @Activity, @SQM, ");
							sql.AppendLine("@WPID, @Description, @ProdMonth, @StopeWidth, '120000', @BL_Date, @Ex_Date, @X1, @Y1, @Z1, @X2, @Y2, @Z2, @Adv);");
							using (var destinationCommand = new SqlCommand(sql.ToString(), pasConnection))
							{
								destinationCommand.AddSqlParameter("@Blast_Date", drPlanningWorkplace["Calendardate"]);
								destinationCommand.AddSqlParameter("@X_Coordinate", fltX);
								destinationCommand.AddSqlParameter("@Y_Coordinate", fltY);
								destinationCommand.AddSqlParameter("@Z_Coordinate", fltZ);
								destinationCommand.AddSqlParameter("@Activity", drPlanningWorkplace["Activity"].ToString() == "0" ? "1" : "0");
								destinationCommand.AddSqlParameter("@SQM", drPlanningWorkplace["SQM"]);
								destinationCommand.AddSqlParameter("@WPID", drPlanningWorkplace["WorkplaceID"]);
								destinationCommand.AddSqlParameter("@Description", drPlanningWorkplace["Description"]);
								destinationCommand.AddSqlParameter("@ProdMonth", drPlanningWorkplace["ProdMonth"]);
								destinationCommand.AddSqlParameter("@StopeWidth", drPlanningWorkplace["SW"]);
								destinationCommand.AddSqlParameter("@BL_Date", dtBlastDate.ToString("yyyyMMdd"));
								destinationCommand.AddSqlParameter("@Ex_Date", dtExDate.ToString("yyyyMMdd"));
								destinationCommand.AddSqlParameter("@X1", fltX1);
								destinationCommand.AddSqlParameter("@Y1", fltY1);
								destinationCommand.AddSqlParameter("@Z1", fltZ1);
								destinationCommand.AddSqlParameter("@X2", fltX2);
								destinationCommand.AddSqlParameter("@Y2", fltY2);
								destinationCommand.AddSqlParameter("@Z2", fltZ2);
								destinationCommand.AddSqlParameter("@Adv", adv);
								destinationCommand.ExecuteNonQuery();
								LogMessage($"[{DateTime.Now}] - 1 records inserted into XYZCoordinates.");
							}
						}
					}
					else
					{
						InsertIntoXyzCoordinatesErrors(pasConnection, drPlanningWorkplace, dtCalendarDate.Rows.Count == 1 ? "Only One Sample" : "No Sample");
					}
				}
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : XYZ Import - Failed to import xyz successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				throw;
			}
			finally
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : XYZ Import - Exiting Xyz Import");
			}
		}

		public void CreateCsvFile(SqlConnection pasConnection)
		{
			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : XYZ Import - Entering CreateCsvFile");

				var finalSql = new StringBuilder();
				finalSql.AppendLine("select Ex_Date, Blast_Time, X_Coordinate, Y_Coordinate, Z_Coordinate, SQM, StopeWidth, Activity, WPID, Description, Bl_Date");
				finalSql.AppendLine(" from XYZCoordinates with(nolock)");
				finalSql.AppendLine(" where Export_Date = CONVERT(VARCHAR(10), GETDATE(), 120)");
				finalSql.AppendLine(" order by Bl_Date desc, Description");

				var xyzResult = pasConnection.ExecuteQuery(finalSql.ToString());

				if (!Directory.Exists(CsvPath))
				{
					Directory.CreateDirectory(CsvPath);
				}

				File.WriteAllText(string.Concat(CsvPath, "\\XYZ Coordinates ", DateTime.Now.ToString("yyyyMMdd_HHmmss"), ".txt"), xyzResult.ToCsv());
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : XYZ Import - Failed to CreateCsvFile successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				throw;
			}
			finally
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : XYZ Import - Exiting CreateCsvFile");
			}
		}

		private DataTable GetCalendarDates(SqlConnection sourceConnection, DataRow drPlanningWorkplace)
		{
			if (sourceConnection.State != ConnectionState.Open)
			{
				sourceConnection.Open();
			}

			var strCommandText = "SELECT distinct TOP 2 Date_Sampled AS CalendarDate FROM SampleSectionView ";
			strCommandText += string.Format(CultureInfo.InvariantCulture, "WHERE WPName = '{0}' AND Date_Sampled < (SELECT CONVERT(VARCHAR(10), '{1}', 120)) ORDER BY CalendarDate DESC;", drPlanningWorkplace["Description"],
				drPlanningWorkplace["Calendardate"]);

			return sourceConnection.ExecuteQuery(strCommandText);
		}

		private void InsertIntoXyzCoordinatesErrors(SqlConnection connection, DataRow drPlanningWorkplace, string errorMessage)
		{
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			var sql = new StringBuilder();
			sql.AppendLine("INSERT INTO XYZCoordinates_Errors(Export_Date, Blast_Date, SQM, WPID, Description, OldWPName, ProdMonth, ErrorMsg)");
			sql.AppendLine("VALUES(CONVERT(VARCHAR(10), GETDATE(), 120), @Blast_Date, @SQM, @WPID, @Description, @OldWPName, @ProdMonth, @ErrorMsg)");

			using (var command = new SqlCommand(sql.ToString(), connection))
			{
				command.AddSqlParameter("@Blast_Date", drPlanningWorkplace["Calendardate"]);
				command.AddSqlParameter("@SQM", drPlanningWorkplace["SQM"]);
				command.AddSqlParameter("@WPID", drPlanningWorkplace["WorkplaceID"]);
				command.AddSqlParameter("@Description", drPlanningWorkplace["Description"]);
				command.AddSqlParameter("@OldWPName", drPlanningWorkplace["OldWPName"]);
				command.AddSqlParameter("@ProdMonth", drPlanningWorkplace["ProdMonth"]);
				command.AddSqlParameter("@ErrorMsg", errorMessage);
				command.ExecuteNonQuery();
				LogMessage($"[{DateTime.Now}] - 1 records inserted into XYZCoordinates_Errors.");
			}
		}

		private DataTable GetDataFromSampleSectionView(SqlConnection sourceConnection, DataRow drCalendarDate, string workplaceName)
		{
			if (sourceConnection.State != ConnectionState.Open)
			{
				sourceConnection.Open();
			}

			var strCommandText = "SELECT AVG(Section_X) Section_X, AVG(Section_Y) Section_Y, AVG(Section_Z) Section_Z ";
			strCommandText += string.Format(CultureInfo.InvariantCulture, "FROM SampleSectionView WHERE WPName = '{0}' AND CAST(Date_Sampled as date) = CAST('{1}' as Date)", workplaceName, drCalendarDate["Calendardate"]);

			return sourceConnection.ExecuteQuery(strCommandText);
		}

		private DataTable GetDataFromPlanning(SqlConnection destinationConnection, DataRow drPlanningWorkplace, DateTime dtBookDate)
		{
			if (destinationConnection.State != ConnectionState.Open)
			{
				destinationConnection.Open();
			}

			var strCommandText = "SELECT SUM(BookMetresAdvance) Advance FROM Planning WHERE Activity = 0 AND ";
			strCommandText += string.Format(CultureInfo.InvariantCulture, "CalendarDate >= (SELECT CONVERT(VARCHAR(10), '{0}', 120)) AND CalendarDate <= '{1}' AND WorkplaceID = '{2}';",
				dtBookDate, drPlanningWorkplace["CalendarDate"], drPlanningWorkplace["WorkplaceID"]);

			return destinationConnection.ExecuteQuery(strCommandText);
		}

		private void LogMessage(string message)
		{
			Directory.CreateDirectory(Thread.GetDomain().BaseDirectory + "\\ImportLog");
			using (var sw = File.AppendText(Thread.GetDomain().BaseDirectory + ImportLogFile))
			{
				sw.WriteLine(message);
			}
		}
	}
}