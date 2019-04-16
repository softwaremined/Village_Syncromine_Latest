using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using MWDataManager;
using System.Data.SqlClient;
using System.Drawing;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SamplingCapture
{
    class clsSamplingCaptureData : clsBase
    {
        private MWDataManager.clsDataAccess TheData = new MWDataManager.clsDataAccess();
        public TUserCurrentInfo CurrentUser = new TUserCurrentInfo();

        public string SystemDBTag;

        public void setConnectionString()
        {
            TheData.ConnectionString = TConnections.GetConnectionString(resHarmonyPAS.systemDBTag, CurrentUser.Connection);
        }

        public DataTable getSamplingData()
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select w.*, s1.*, Status = '' from workplace w left outer join ");
                sb.AppendLine("(Select workplaceid, max(CalendarDate) CalendarDate");
                sb.AppendLine("from sampling group by workplaceid) s");
                sb.AppendLine("on w.workplaceid = s.workplaceid left outer join");
                sb.AppendLine("sampling s1 on s.workplaceid = s1.workplaceid and");
                sb.AppendLine("s.calendardate = s1.calendardate");
                sb.AppendLine("where w.reefwaste = 'R'");
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

        public DataTable getGroupedSamplingData(string _workplaceID)
        {
            clsDataAccess newData = new clsDataAccess();
            try
            {

                sb.Clear();
                sb.AppendLine("select w.*, s1.*, Status = '' from workplace w left outer join ");
                sb.AppendLine("(Select workplaceid, max(CalendarDate) CalendarDate");
                sb.AppendLine("from sampling group by workplaceid) s");
                sb.AppendLine("on w.workplaceid = s.workplaceid left outer join");
                sb.AppendLine("sampling s1 on s.workplaceid = s1.workplaceid and");
                sb.AppendLine("s.calendardate = s1.calendardate");
                sb.AppendLine("where w.reefwaste = 'R' and s1.WorkplaceID = '"+ _workplaceID +"'");
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
        public void SaveData(DateTime _calendarDate, string _workplaceID, int _stopeWidth, int _channel, int _hangingWall, int _footWall, int _cmgt, int _rif, string action)
        {
            try
            {
                theData.SqlStatement = "select * from SAMPLING \r\n " +
                                       "where CalendarDate = '"+ _calendarDate +"' \r\n " +
                                       "order by CalendarDate desc ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                DataTable dtAct = theData.ResultsDataTable;

                if (dtAct.Rows.Count == 0)
                {
                    if (_calendarDate <= DateTime.Now)
                    {
                        theData.SqlStatement = "insert into SAMPLING (CalendarDate, WorkplaceID, SWidth, \r\n " +
                           "Channelwidth, HangingWall, Footwall, Cmgt, RIF) values \r\n " +
                           "('" + _calendarDate + "', '" + _workplaceID + "',  \r\n " +
                           "isnull(" + _stopeWidth + ", 0), isnull(" + _channel + ", 0), \r\n " +
                           "isnull(" + _hangingWall + ", 0), isnull(" + _footWall + ", 0),\r\n " +
                           "isnull(" + _cmgt + ", 0), isnull(" + _rif + ", 0))";
                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        theData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Section Data was saved", Color.CornflowerBlue);
                    }

                    else
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", "Date cannot be later than the current date", Color.Red);


                }

                else 
                {
                    if (_calendarDate >= Convert.ToDateTime(dtAct.Rows[0]["CalendarDate"].ToString()))
                    {
                        theData.SqlStatement = theData.SqlStatement +
                        "delete from SAMPLING " +
                        "where WorkplaceID = '" + _workplaceID + "'" +
                        "and CalendarDate = '" + _calendarDate + "'";

                        theData.SqlStatement = theData.SqlStatement +
                        "insert into SAMPLING (CalendarDate, WorkplaceID, SWidth, " +
                        "Channelwidth, HangingWall, Footwall, Cmgt, RIF) values " +
                        "('" + _calendarDate + "', '" + _workplaceID + "', " +
                        "isnull(" + _stopeWidth + ", 0), isnull(" + _channel + ", 0), " +
                        "isnull(" + _hangingWall + ", 0), isnull(" + _footWall + ", 0), " +
                        "isnull(" + _cmgt + ", 0), isnull(" + _rif + ", 0)) ";
                        theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();

                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Section Data was saved", Color.CornflowerBlue);
                    }

                    else
                        Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", "Date cannot be earlier than the current date", Color.Red);
                }

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }
        }
    }
}
