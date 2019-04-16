using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Mineware.Systems.WindowsServices.Extensions;

namespace Mineware.Systems.WindowsServices.WorkplaceStripperPlugin
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

				using (var destinationConnection = new SqlConnection(GetDestinationConnection()))
				{
					destinationConnection.Open();
					using (var gmsiConnection = new SqlConnection(GetGmsiConnection()))
					{
						Import(gmsiConnection, destinationConnection);

						using (var cmd = new SqlCommand($"DBCC SHRINKFILE({Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.DestinationDatabaseLog")}, 1);", destinationConnection))
						{
							if (destinationConnection.State != ConnectionState.Open)
							{
								destinationConnection.Open();
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

			_hourToRunImport = Global.GetSettingValueAsInt("Mineware.Systems.WorkplaceStripPlugin.HourToRunImport");
			return _hourToRunImport.Value;
		}

		private int GetMinutesToRunImport()
		{
			if (_minuteToImport != null)
			{
				return _minuteToImport.Value;
			}

			_minuteToImport = Global.GetSettingValueAsInt("Mineware.Systems.WorkplaceStripPlugin.MinuteToRunImport");
			return _minuteToImport.Value;
		}

		public static string WorkplaceFile => $"{Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.WorkplaceFilePath")}\\Workplace.wkp";

		public string ImportLogFile { get; set; }

		public static string ImportErrorLogFile { get; set; }

		/// <summary>
		/// Required Settings
		/// - GmsiDatabaseServer
		/// - GmsiDatabase
		/// - GmsiDatabaseUserId
		/// - GmsiDatabasePassword
		/// </summary>
		/// <returns></returns>
		private static string GetGmsiConnection()
		{
			return
				$"Server={Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.GmsiDatabaseServer")};" +
				$"Database={Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.GmsiDatabase")};" +
				$"User Id={Global.Decrypt(Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.GmsiDatabaseUserId"))};" +
				$"Password={Global.Decrypt(Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.GmsiDatabasePassword"))};";
		}

		/// <summary>
		/// Required Settings
		/// - DestinationDatabaseServer
		/// - DestinationDatabase
		/// - DestinationDatabaseUserId
		/// - DestinationDatabasePassword
		/// </summary>
		/// <returns></returns>
		private static string GetDestinationConnection()
		{
			return
				$"Server={Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.DestinationDatabaseServer")};" +
				$"Database={Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.DestinationDatabase")};" +
				$"User Id={Global.Decrypt(Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.DestinationDatabaseUserId"))};" +
				$"Password={Global.Decrypt(Global.GetSettingValueAsString("Mineware.Systems.WorkplaceStripPlugin.DestinationDatabasePassword"))};";
		}

		public void Import(SqlConnection gmsiConnection, SqlConnection destinationConnection)
		{
			InsertIntoGmsiDatesTable(destinationConnection);
			InsertIntoBlockLink(gmsiConnection, destinationConnection);
		}

		private void InsertIntoGmsiDatesTable(SqlConnection destinationConnection)
		{
			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Stripper - Start Import into GMSI Dates");
				if (destinationConnection.State != ConnectionState.Open)
				{
					destinationConnection.Open();
				}

				var sql = new StringBuilder();
				sql.AppendLine("SELECT w.EndTypeId, w.Description, IsNull(w.OldWorkplaceId, w.WorkplaceId) WorkplaceId, Type = (CASE WHEN w.Activity = 0 THEN 'S' ELSE 'D' END), g.TheDate");
				sql.AppendLine("FROM Workplace w with(nolock)");
				sql.AppendLine("LEFT OUTER JOIN GMSIDATES g with(nolock) ON w.workplaceid = g.workplaceid");
				sql.AppendLine("WHERE w.activity NOT IN('10', '11')");
				sql.AppendLine("AND IsNull(w.Inactive, '') <> 'Y'");
				sql.AppendLine("And w.CreationDate > DateAdd(day, -90, GetDate())");
				sql.AppendLine("ORDER BY activity");

				using (var dt = destinationConnection.ExecuteQuery(sql.ToString()))
				{
					var workplaceDates = dt.GetDataFromDataTable<WorkplaceDate>();
					var log = new StringBuilder();
					var workplacelog = new StringBuilder();
					foreach (var item in workplaceDates)
					{
						const string strLoc = "UG    ";
						const string strStatusCode = "ACT";

						var strMajorGroup = "STOPE ";
						var strTheGroup = "PANL";
						var strTheName = item.Description;
						var strId = item.WorkplaceId;
						string strTheDate;

						if (item.Type == "D")
						{
							strMajorGroup = "DEV   ";
						}

						if (item.EndTypeId.ToString() != "" && item.EndTypeId != 0)
						{
							strTheGroup = item.EndTypeId.ToString();

							if (strTheGroup.Length < 4)
							{
								strTheGroup = strTheGroup.PadRight(4, ' ');
							}
						}

						if (strTheName.Length < 32)
						{
							strTheName = strTheName.PadRight(32, ' ');
						}

						if (strId.Length < 8)
						{
							strId = strId.PadRight(8, ' ');
						}

						if (item.TheDate == null || item.TheDate.Value.ToString(CultureInfo.InvariantCulture) == "0")
						{
							strTheDate = DateTime.Now.ToString("MM/dd/yyyy");

							const string strCommandText = "INSERT INTO GmsiDates (WorkplaceId, Thedate) VALUES (@WorkplaceId, @Thedate)";
							using (var destinationCommand = new SqlCommand(strCommandText, destinationConnection))
							{
								destinationCommand.AddSqlParameter("@WorkplaceId", item.WorkplaceId);
								destinationCommand.AddSqlParameter("@Thedate", DateTime.Now);
								destinationCommand.ExecuteNonQuery();
								LogMessage($"[{DateTime.Now}] - 1 records inserted into GmsiDates.");
							}
						}
						else
						{
							strTheDate = Convert.ToDateTime(item.TheDate).ToString("MM/dd/yyyy");
						}

						if (strTheGroup.Length > 6)
						{
							log.AppendLine($"[{strId}] - {strTheName} Workplace Group too long.");
						}
						else if (strTheName.Length > 32)
						{
							log.AppendLine($"[{strId}] - {strTheName} Workplace Description too long.");
						}
						else if (strId.Length > 8)
						{
							log.AppendLine($"[{strId}] - {strTheName} Workplace Id too long.");
						}
						else
						{
							workplacelog.AppendLine($"{strLoc}{strMajorGroup}{strTheGroup}{strTheName}{strId}{strStatusCode} {strTheDate}");
						}
					}
					LogMessage(log.ToString());//GS - Write to the file only once, otherwise it takes 30 seconds to process 4500 records instead of only 100ms.
					LogWorkplaceResult(workplacelog.ToString());//GS - Write to the file only once, otherwise it takes 30 seconds to process 4500 records instead of only 100ms.
				}
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Stripper - Failed to insert GMSI Dates successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				throw;
			}
			finally
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Stripper - Exiting GMSI Dates");
			}
		}

		private void InsertIntoBlockLink(SqlConnection gmsiConnection, SqlConnection destinationConnection)
		{
			try
			{
				if (destinationConnection.State != ConnectionState.Open)
				{
					destinationConnection.Open();
				}
			}
			catch (SqlException ex)
			{
				throw new SqlConnectionException("Could not open SQL Connection", ex);
			}
			try
			{
				if (gmsiConnection.State != ConnectionState.Open)
				{
					gmsiConnection.Open();
				}
			}
			catch (SqlException ex)
			{
				throw new SqlConnectionException("Could not open GMSI SQL Connection", ex);
			}

			try
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Stripper - Start Import Block Link");
				var strCommandText = "DELETE FROM GMSIImport;";

				using (var destinationCommand = new SqlCommand(strCommandText, destinationConnection))
				{
					destinationCommand.ExecuteNonQuery();
					LogMessage($"[{destinationCommand.ExecuteNonQuery()}] - records deleted from GMSIImport.");
				}

				strCommandText = "SELECT distinct date_sampled, ExternalID, WPNAme, Sheet_Channel_Width_cms, Sheet_Stope_Width_cms, [Sheet cmg/t] AS cmgt, ";
				strCommandText += "[Sheet RIH] AS RIH, [Sheet RIF] AS RIF, [Latest Assay Date] AS AssayDate, Assayed, AuthorisedDate, [Block Name] AS BlockName, Sheet_Footwall_Width_cms, ";
				strCommandText += "Sheet_HangingWall_Width_cms, [Dev Meters] AS DevMetres FROM ExtSamplingPASView WHERE assayed = 'All';";

				using (var destinationAdapter = new SqlDataAdapter(strCommandText, gmsiConnection))
				{
					var dtExtSamplingPasView = new DataTable();
					destinationAdapter.Fill(dtExtSamplingPasView);
					LogMessage($"[{dtExtSamplingPasView.Rows.Count}] - records available in ExtSamplingPASView.");

					foreach (DataRow drExtSamplingPasViewRow in dtExtSamplingPasView.Rows)
					{
						strCommandText = "INSERT INTO GMSIImport (Samplingdate, Workplaceid, Description, Channelwidth, Stopewidth, cmgt, ";
						strCommandText += "RIH, RIF, AssayDate, Assayed, AuthDate, BlockName, FootWall, HangingWall, DevMetres) ";
						strCommandText += "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}');";
						strCommandText = string.Format(strCommandText, drExtSamplingPasViewRow["date_sampled"], drExtSamplingPasViewRow["ExternalID"], drExtSamplingPasViewRow["WPNAme"],
							drExtSamplingPasViewRow["Sheet_Channel_Width_cms"], drExtSamplingPasViewRow["Sheet_Stope_Width_cms"], drExtSamplingPasViewRow["cmgt"], drExtSamplingPasViewRow["RIH"],
							drExtSamplingPasViewRow["RIF"], drExtSamplingPasViewRow["AssayDate"], drExtSamplingPasViewRow["Assayed"], drExtSamplingPasViewRow["AuthorisedDate"], drExtSamplingPasViewRow["BlockName"],
							drExtSamplingPasViewRow["Sheet_Footwall_Width_cms"], drExtSamplingPasViewRow["Sheet_HangingWall_Width_cms"], drExtSamplingPasViewRow["DevMetres"]);

						using (var destinationCommand = new SqlCommand(strCommandText, destinationConnection))
						{
							destinationCommand.ExecuteNonQuery();
						}
					}

					LogMessage($"[{DateTime.Now}] - {dtExtSamplingPasView.Rows.Count} records inserted into GMSIImport.");
				}

				strCommandText = "SELECT g.* FROM GMSIImport g INNER JOIN WORKPLACE w ON w.WorkplaceID = g.workplaceid GROUP BY g.Samplingdate, g.Workplaceid, g.Description, ";
				strCommandText += "g.Channelwidth, g.Stopewidth, g.cmgt, g.RIH,RIF, g.AssayDate, g.Assayed, g.AuthDate, g.blockname, g.footwall, g.hangingwall, g.devmetres ";
				strCommandText += "ORDER BY g.workplaceid, g.samplingdate, g.assaydate DESC;";

				using (var destinationAdapter = new SqlDataAdapter(strCommandText, destinationConnection))
				{
					var dtGmsiImportWorkplace = new DataTable();
					destinationAdapter.Fill(dtGmsiImportWorkplace);
					LogMessage($"[{dtGmsiImportWorkplace.Rows.Count}] - records available in ExtSamplingPASView.");

					var intRecordInserted = 0;
					foreach (DataRow drGmsiImportWorkplace in dtGmsiImportWorkplace.Rows)
					{
						try
						{
							strCommandText = $"DELETE FROM Sampling WHERE workplaceid = '{drGmsiImportWorkplace["workplaceid"]}' AND Calendardate ='{drGmsiImportWorkplace["SamplingDate"]}';";

							using (var destinationCommand = new SqlCommand(strCommandText, destinationConnection))
							{
								destinationCommand.ExecuteNonQuery();
								LogMessage($"[{destinationCommand.ExecuteNonQuery()}] - records deleted from Sampling.");
							}

							strCommandText = "INSERT INTO Sampling (Calendardate, Workplaceid, Channelwidth, SWidth, HangingWall, FootWall, cmgt, RIF, assaydate, authdate) ";
							strCommandText += "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');";

							strCommandText = string.Format(strCommandText, drGmsiImportWorkplace["Samplingdate"], drGmsiImportWorkplace["Workplaceid"], drGmsiImportWorkplace["Channelwidth"]
								, drGmsiImportWorkplace["Stopewidth"], drGmsiImportWorkplace["HangingWall"], drGmsiImportWorkplace["FootWall"]
								, drGmsiImportWorkplace["cmgt"], drGmsiImportWorkplace["RIF"], drGmsiImportWorkplace["assaydate"]
								, drGmsiImportWorkplace["authdate"]);

							using (var destinationCommand = new SqlCommand(strCommandText, destinationConnection))
							{
								destinationCommand.ExecuteNonQuery();
								intRecordInserted++;
							}
						}
						catch (Exception ex)
						{
							LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Stripper - Failed to insert Block Link successfully. {ex}, SQL - {strCommandText}");
							throw;
						}
					}
					LogMessage($"[{intRecordInserted}] - records inserted into Sampling.");
				}
			}
			catch (Exception ex)
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Stripper - Failed to insert Block Link successfully. {ex.Message}, Inner: {ex.InnerException?.Message}");
				throw;
			}
			finally
			{
				LogMessage($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} : Workplace Stripper - Exiting Block Link");
			}
		}

		private void LogMessage(string message)
		{
			Directory.CreateDirectory(Thread.GetDomain().BaseDirectory + "\\ImportLog");
			using (var sw = File.AppendText(Thread.GetDomain().BaseDirectory + ImportLogFile))
			{
				sw.WriteLine(message);
			}
		}

		private void LogWorkplaceResult(string message)
		{
			File.AppendAllText(WorkplaceFile, message);
		}
	}
}
