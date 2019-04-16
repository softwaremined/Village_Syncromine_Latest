using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using System.Drawing;
using MWDataManager;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Calendars
{
    class clsCalendarsData : clsBase
    {
        string totshift;

        public DataTable GetCalendarCodes(string _calendarCode)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select CalendarCode from CODE_CALENDAR where [Description] != 'Section Calendar'");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;
        }

        public DataTable getCalendarList(string _calendarCode)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                if (_calendarCode == "Mill" || _calendarCode == "MillAFGP" || _calendarCode == "MILL")
                {
                    sb.Clear();
                    sb.AppendLine("select MillMonth [Month], StartDate, EndDate, TotalShifts,CalendarCode, ");
                    sb.AppendLine("NextMonth = case when convert(varchar(2), ");
                    sb.AppendLine("DATEPART(month, DATEADD(MONTH,+1,convert(date, convert(varchar(4),MillMonth) ");
                    sb.AppendLine("+ '-' + substring(MillMonth,5,2) + '-01')) ");
                    sb.AppendLine(")) < 10 then Convert(varchar(4),DATEPART(Year, DATEADD(MONTH,+1, ");
                    sb.AppendLine("convert(date, convert(varchar(4),MillMonth) + '-' + substring(MillMonth,5,2) + '-01'))))+ ");
                    sb.AppendLine("'0' + CONVERT(varchar(2),DATEPART(MONTH, DATEADD(MONTH,+1, ");
                    sb.AppendLine("convert(date, convert(varchar(4),MillMonth) + '-' + substring(MillMonth,5,2) + '-01')) )) ");
                    sb.AppendLine("else Convert(varchar(4),DATEPART(Year, DATEADD(MONTH,+1, ");
                    sb.AppendLine("convert(date, convert(varchar(4),MillMonth) + '-' + substring(MillMonth,5,2) + '-01'))))+ ");
                    sb.AppendLine("CONVERT(varchar(2),DATEPART(MONTH, DATEADD(MONTH,+1, ");
                    sb.AppendLine("convert(date, convert(varchar(4),MillMonth) + '-' + substring(MillMonth,5,2) + '-01')))) end, ");
                    sb.AppendLine("NextFirstDate = DATEADD(Day,+1,EndDate), Status = '' ");
                    sb.AppendLine("from CALENDARMILL order by [MillMonth] desc");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();
                }

                else
                {
                    sb.Clear();
                    sb.AppendLine("select [Month], StartDate, EndDate, TotalShifts,CalendarCode, ");
                    sb.AppendLine("NextMonth = case when convert(varchar(2), ");
                    sb.AppendLine("DATEPART(month, DATEADD(MONth,+1,convert(date, convert(varchar(4),Month) ");
                    sb.AppendLine("+ '-' + substring(Month,5,2) + '-01')) ");
                    sb.AppendLine(")) < 10 then Convert(varchar(4),DATEPART(Year, DATEADD(MONth,+1, ");
                    sb.AppendLine("convert(date, convert(varchar(4),Month) + '-' + substring(Month,5,2) + '-01'))))+ ");
                    sb.AppendLine("'0' + CONVERT(varchar(2),DATEPART(month, DATEADD(MONth,+1, ");
                    sb.AppendLine("convert(date, convert(varchar(4),Month) + '-' + substring(Month,5,2) + '-01')) )) ");
                    sb.AppendLine("else Convert(varchar(4),DATEPART(Year, DATEADD(MONth,+1, ");
                    sb.AppendLine("convert(date, convert(varchar(4),Month) + '-' + substring(Month,5,2) + '-01'))))+ ");
                    sb.AppendLine("CONVERT(varchar(2),DATEPART(month, DATEADD(MONth,+1, ");
                    sb.AppendLine("convert(date, convert(varchar(4),Month) + '-' + substring(Month,5,2) + '-01')))) end, ");
                    sb.AppendLine("NextFirstDate = DATEADD(Day,+1,EndDate), Status = ''  ");
                    sb.AppendLine("from CALENDAROTHER where CalendarCode = '" + _calendarCode + "' order by [Month] desc");
                    newData.ConnectionString = theData.ConnectionString;
                    newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    newData.SqlStatement = sb.ToString();
                    newData.ExecuteInstruction();
                }


            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;

        }

        public DataTable getTotalShiftsList(string _BeginDate, string _EndDate, string _calendarCode)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {
                sb.Clear();
                sb.AppendLine("select count(WorkingDay) from Caltype ");
                sb.AppendLine("where CalendarCode = '" + _calendarCode + "' and WorkingDay = 'Y' and ");
                sb.AppendLine("CalendarDate >= '" + _BeginDate + "' and ");
                sb.AppendLine("CalendarDate <= '" + _EndDate + "'");
                newData.ConnectionString = theData.ConnectionString;
                newData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                newData.queryReturnType = MWDataManager.ReturnType.DataTable;
                newData.SqlStatement = sb.ToString();
                newData.ExecuteInstruction();

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
            return newData.ResultsDataTable;

        }
      
        public void SaveData(DataTable SafetyCalendarData, string _calendarCode)
        {
            try
            {


                bool FoundRec = false;
                
                theData.SqlStatement = " ";
               
                foreach (DataRow r in SafetyCalendarData.Rows)
                {
                    string aa = r["Status"].ToString();
                    if (r["Status"].ToString() != "")
                    {
                        if (_calendarCode == "Mill")
                        {
                            if (r["Status"].ToString() == "New")
                            {
                                theData.SqlStatement = "select count(WorkingDay) \r\n " +
                                                       "from Caltype \r\n " +
                                                       "where CalendarCode = '" + _calendarCode + "' and WorkingDay = 'Y' and \r\n " +
                                                       "CalendarDate >= '" + r["StartDate"].ToString() + "' and \r\n " +
                                                       "CalendarDate <= '" + r["EndDate"] + "' ";

                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                theData.ExecuteInstruction();
                                DataTable dt = new DataTable();
                                dt = theData.ResultsDataTable;

                                foreach (DataRow dr in dt.Rows)
                                {
                                    totshift = "";
                                    totshift = dr["Column1"].ToString();
                                }

                                theData.SqlStatement = theData.SqlStatement +
                                                       "insert into CALENDARMILL (MillMonth, OreFlowID, StartDate, EndDate, CalendarCode, TotalShifts) values " +
                                                       "('" + r["Month"] + "', 'DP1', '" + r["StartDate"].ToString() + "', " +
                                                       "'" + r["EndDate"] + "', " +
                                                       "'" + _calendarCode + "', " +
                                                       "'" + totshift + "') ";
                                FoundRec = true;
                            }
                            if (r["Status"].ToString() == "Update")
                            {
                                theData.SqlStatement = "select count(WorkingDay) \r\n " +
                                                       "from Caltype \r\n " +
                                                       "where CalendarCode = '" + _calendarCode + "' and WorkingDay = 'Y' and \r\n " +
                                                       "CalendarDate >= '" + r["StartDate"].ToString() + "' and \r\n " +
                                                       "CalendarDate <= '" + r["EndDate"] + "' ";

                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                theData.ExecuteInstruction();
                                DataTable dt = new DataTable();
                                dt = theData.ResultsDataTable;

                                foreach (DataRow dr in dt.Rows)
                                {
                                    totshift = "";
                                    totshift = dr["Column1"].ToString();
                                }

                                theData.SqlStatement = theData.SqlStatement +
                                                       "update CALENDARMILL " +
                                                       "set StartDate = '" + r["StartDate"].ToString() + "', " +
                                                       "EndDate = '" + r["EndDate"] + "', " +
                                                       "CalendarCode = '" + _calendarCode + "', " +
                                                       "TotalShifts = '" + totshift + "' " +
                                                       "where MillMonth = " + r["Month"] + "";
                                FoundRec = true;
                            }
                            if (r["Status"].ToString() == "Delete")
                            {
                                theData.SqlStatement = theData.SqlStatement +
                                   " delete from CALENDARMILL where MillMonth = '" + r["Month"] + "'";
                                FoundRec = true;

                            }
                            if (FoundRec == true)
                            {
                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                                theData.ExecuteInstruction();
                            }

                        }

                        else
                        {
                            if (r["Status"].ToString() == "New")
                            {
                                theData.SqlStatement = "select count(WorkingDay) \r\n " +
                                                       "from Caltype \r\n " +
                                                       "where CalendarCode = '" + _calendarCode + "' and WorkingDay = 'Y' and \r\n " +
                                                       "CalendarDate >= '" + r["StartDate"].ToString() + "' and \r\n " +
                                                       "CalendarDate <= '" + r["EndDate"] + "' ";

                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                theData.ExecuteInstruction();
                                DataTable dt = new DataTable();
                                dt = theData.ResultsDataTable;

                                foreach (DataRow dr in dt.Rows)
                                {
                                    totshift = "";
                                    totshift = dr["Column1"].ToString();
                                }

                                theData.SqlStatement = theData.SqlStatement +
                                                       "insert into CALENDAROTHER ( Month, StartDate, EndDate, CalendarCode, TotalShifts) values " +
                                                       "('" + r["Month"] + "', '" + r["StartDate"].ToString() + "', " +
                                                       "'" + r["EndDate"] + "', " +
                                                       "'" + _calendarCode + "', " +
                                                       "'" + totshift + "') ";
                                FoundRec = true;
                            }
                            if (r["Status"].ToString() == "Update")
                            {
                                theData.SqlStatement = "select count(WorkingDay) \r\n " +
                                                       "from Caltype \r\n " +
                                                       "where CalendarCode = '" + _calendarCode + "' and WorkingDay = 'Y' and \r\n " +
                                                       "CalendarDate >= '" + r["StartDate"].ToString() + "' and \r\n " +
                                                       "CalendarDate <= '" + r["EndDate"] + "' ";

                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                theData.ExecuteInstruction();
                                DataTable dt = new DataTable();
                                dt = theData.ResultsDataTable;

                                foreach (DataRow dr in dt.Rows)
                                {
                                    totshift = "";
                                    totshift = dr["Column1"].ToString();
                                }

                                theData.SqlStatement = theData.SqlStatement +
                                                       "update CALENDAROTHER " +
                                                       "set StartDate = '" + r["StartDate"].ToString() + "', " +
                                                       "EndDate = '" + r["EndDate"] + "', " +
                                                       "CalendarCode = '" + _calendarCode + "', " +
                                                       "TotalShifts = '" + totshift + "' " +
                                                       "where Month = " + r["Month"] + " and CalendarCode = '" + _calendarCode + "'";
                                FoundRec = true;
                            }
                            if (r["Status"].ToString() == "Delete")
                            {
                                theData.SqlStatement = theData.SqlStatement +
                                   " delete from CALENDAROTHER where Month = '" + r["Month"] + "' and CalendarCode = '" + _calendarCode + "'";
                                FoundRec = true;

                            }
                            if (FoundRec == true)
                            {
                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                                theData.ExecuteInstruction();
                            }
                        }
                        
                    }
                }
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }

        
        public void SaveData2(DataTable SafetyCalendarData, string _calendarCode, string TotalShifts, string Month, string StartDate, string EndDate)
        {
            try
            {


                bool FoundRec = false;

                theData.SqlStatement = " ";
                
               
                        if (_calendarCode == "MILL")
                        {
                                string MillCode = "";
                                if (SysSettings.Banner == "Joel Mine")
                                {
                                        MillCode = "JP1";
                                }
                                if (SysSettings.Banner == "Doornkop Mine")
                                {
                                    MillCode = "DP1";
                                }






                    theData.SqlStatement = theData.SqlStatement +
                                                       "begin try insert into CALENDARMILL (MillMonth, OreFlowID, StartDate, EndDate, CalendarCode, TotalShifts) values " +
                                                       "('" + Month + "', '" + MillCode + "', '" + StartDate + "', " +
                                                       "'" + EndDate + "', " +
                                                       "'" + _calendarCode + "', " +
                                                       "'" + TotalShifts + "') end try begin catch ";
                                //FoundRec = true;
                           
                                

                                theData.SqlStatement = theData.SqlStatement +
                                                       "update CALENDARMILL " +
                                                       "set StartDate = '" + StartDate + "', " +
                                                       "EndDate = '" + EndDate + "', " +
                                                       "CalendarCode = '" + _calendarCode + "', " +
                                                       "TotalShifts = '" + TotalShifts + "' " +
                                                       "where MillMonth = " + Month + " end catch";
                                FoundRec = true;
                           
                            if (FoundRec == true)
                            {
                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                                theData.ExecuteInstruction();
                            }

                        }

                        else
                        {
                           
                                

                                theData.SqlStatement = theData.SqlStatement +
                                                       " begin try insert into CALENDAROTHER ( Month, StartDate, EndDate, CalendarCode, TotalShifts) values " +
                                                       "('" + Month + "', '" + StartDate + "', " +
                                                       "'" + EndDate + "', " +
                                                       "'" + _calendarCode + "', " +
                                                       "'" + TotalShifts + "') end try begin catch ";
                               
                           
                                

                                theData.SqlStatement = theData.SqlStatement +
                                                       "update CALENDAROTHER " +
                                                       "set StartDate = '" + StartDate + "', " +
                                                       "EndDate = '" + EndDate + "', " +
                                                       "CalendarCode = '" + _calendarCode + "', " +
                                                       "TotalShifts = '" + TotalShifts + "' " +
                                                       "where Month = " + Month + " and CalendarCode = '" + _calendarCode + "' end catch";
                                FoundRec = true;
                           
                            if (FoundRec == true)
                            {
                                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                                theData.ExecuteInstruction();
                            }
                        

                    }
               
            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", _exception.Message, Color.Red);
            }
        }
    }
}
