using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Mineware.Systems.Global;
using System.Drawing;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SectionCalendar
{
    class clsSectionCalendarData:clsBase 
    {
        private StringBuilder sb = new StringBuilder();

        public DataTable getSecCal(string prodmonth)
        {
            try
            {
                string theMonth = "";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                sb.Clear();
                sb.AppendLine(" select theMonth = substring(convert(varchar(6),dateadd(month,-1, ");
                sb.AppendLine(" cast(substring('" + prodmonth + "',1,4) + '-'+substring('" + prodmonth + "',5,2) +'-01' as date))),1,4) ");
		        sb.AppendLine(" + ");
		        sb.AppendLine(" substring(convert(varchar(10),dateadd(month,-1, ");
                sb.AppendLine(" cast(substring('" + prodmonth + "',1,4) + '-'+substring('" + prodmonth + "',5,2) +'-01' as date))),6,2) ");
                theData.SqlStatement = sb.ToString();
                theData.ExecuteInstruction();

                DataTable y = theData.ResultsDataTable;

                if (y.Rows.Count != 0)
                    theMonth = y.Rows[0]["theMonth"].ToString();

                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;

                sb.Clear();
                sb.AppendLine("select s.sectionid, s.name, calendarcode, 0 Status, TestDate = GetDate(), ");
                sb.AppendLine("CASE WHEN begindate IS NULL THEN '1900-01-01' else convert(varchar(10),begindate,120) end begindate,");
                sb.AppendLine("CASE WHEN enddate IS NULL THEN '1900-01-01' else convert(varchar(10),enddate,120) end enddate,");
                sb.AppendLine("Isnull(totalshifts,0) totalshifts from section s left outer join seccal sc");
                sb.AppendLine("on s.sectionid = sc.sectionid and s.prodmonth = sc.prodmonth");
                sb.AppendLine("where s.prodmonth = '" + prodmonth + "' and s.hierarchicalid = 5 ");
                sb.AppendLine("order by s.sectionid");
                theData.SqlStatement = sb.ToString();
                theData.ExecuteInstruction();

                if (theData.ResultsDataTable.Rows.Count == 0)
                {
                    Global.sysMessages.sysMessagesClass message = new Global.sysMessages.sysMessagesClass();
                    message.viewMessage(MessageType.Info,"NO DATA","There are no sections for production month " + prodmonth + ".\r\n Please add the sections in the orgstructure in the system settings",ButtonTypes.OK,MessageDisplayType.FullScreen);
                }
                else
                {
                    sb.Clear();
                    sb.AppendLine("select s.ProdMonth,s.sectionid, s.name, isnull(CalendarCode,'') calendarcode, 0 Status,  TestDate = GetDate(), ");
                    sb.AppendLine("    BDate = CASE WHEN sc.BeginDate IS NULL then ");
                    sb.AppendLine("                     case when aa.NewConvBeginDate = '' then '' ");
                    sb.AppendLine("                          else aa.NewConvBeginDate end ");
                    sb.AppendLine("                     else convert(varchar(10),sc.begindate,120) end , ");
                    sb.AppendLine("    CASE WHEN sc.EndDate IS NULL THEN '' else convert(varchar(10),sc.EndDate,120) end EDate , ");
                    sb.AppendLine("    isnull(TotalShifts, 0) totalshifts,  ");
                    sb.AppendLine("    begindate = CASE WHEN sc.BeginDate IS NULL  then ");
                    sb.AppendLine("                     case when aa.NewConvBeginDate = '' then GetDate() ");
                    sb.AppendLine("                          else aa.NewBeginDate end ");
                    sb.AppendLine("                     else sc.begindate end , ");
                    sb.AppendLine("    CASE WHEN sc.EndDate IS NULL THEN GetDate() else sc.EndDate end enddate, ");
                    sb.AppendLine("    aa.NewConvBeginDate, aa.NewBeginDate, '"+theMonth+"' PrevMonth ");
                    sb.AppendLine("    from Section s "); 
                    sb.AppendLine("    left outer join Seccal sc on ");
                    sb.AppendLine("       s.SectionID = sc.SectionID and s.ProdMonth = sc.ProdMonth ");
                    sb.AppendLine("    left outer join ");
	                sb.AppendLine("        ( ");
                    sb.AppendLine("            select s.SectionID, NewConvBeginDate = case when sc.EndDate is null then '' else ");
                    sb.AppendLine("                   convert(varchar(10),DateAdd(Day,1,sc.EndDate),120) end, ");
                    sb.AppendLine("                   NewBeginDate = case when sc.EndDate is null then GetDate() else DateAdd(Day,1,sc.EndDate) end ");
		            sb.AppendLine("            from Section s  ");
		            sb.AppendLine("            left outer join Seccal sc on ");
		            sb.AppendLine("               s.SectionID = sc.SectionID and "); 
		            sb.AppendLine("               s.ProdMonth = sc.ProdMonth ");
                    sb.AppendLine("            where s.ProdMonth = " + theMonth + " and s.HierarchicalID = 5 "); 
	                sb.AppendLine("        ) aa on aa.SectionID = s.SectionID ");
                    sb.AppendLine("    where s.ProdMonth = " + prodmonth + " and s.HierarchicalID = 5 ");
                    theData.SqlStatement = sb.ToString();
                    theData.ExecuteInstruction();
                }

                return theData.ResultsDataTable;
            }
            catch (Exception e)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", e.Message, Color.Red);
                return null;
            }

        }

        public DataTable saveData(string theUserID, string theProdMonth)
        {

            try
            {
                theData.SqlStatement = "select * from sect where UserID = '" + theUserID + "' ";
                theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                theData.ExecuteInstruction();

                DataTable dt = new DataTable();
                dt = theData.ResultsDataTable;

                foreach (DataRow d in dt.Rows)
                {
                    theData.SqlStatement = "";
                    theData.SqlStatement = "SELECT * FROM Seccal WHERE ProdMonth = '" + theProdMonth + "' " +
                            " and SectionID = '" + d["SectionID"].ToString() + "' ";
                    theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    theData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    theData.ExecuteInstruction();

                    DataTable dtSect = new DataTable();
                    dtSect = theData.ResultsDataTable;

                    if (dtSect.Rows.Count > 0)
                    {
                        theData.SqlStatement =
                                " UPDATE SECCAL \r\n " +
                                " SET CalendarCode = '" + d["CalendarCode"].ToString() + "', \r\n " +
                                "     BeginDate = '" + d["BeginDate"].ToString() + "', \r\n " +
                                "     EndDate =' " + d["EndDate"].ToString() + "', \r\n " +
                                "     TotalShifts='" + d["TotalShifts"].ToString() + "' \r\n " +
                                " where prodmonth = '" + theProdMonth + "' and \r\n " +
                                "      sectionid = '" + d["SectionID"].ToString() + "' ";
                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();
                    }
                    else
                    {
                        theData.SqlStatement =
                                " insert into SECCAL (ProdMonth, SectionID, CalendarCode, BeginDate, EndDate, Totalshifts) values ( \r\n " +
                                " '" + theProdMonth + "', \r\n " +
                                " '" + d["SectionID"].ToString() + "' , \r\n " +
                                " '" + d["CalendarCode"].ToString() + "', \r\n " +
                                " '" + d["BeginDate"].ToString() + "', \r\n " +
                                " '" + d["EndDate"].ToString() + "', \r\n " +
                                " '" + d["TotalShifts"].ToString() + "') ";
                        theData.queryReturnType = MWDataManager.ReturnType.longNumber;
                        theData.ExecuteInstruction();
                    }
                }
            }
            catch (Exception e)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", e.Message, Color.Red);
            }
            return theData.ResultsDataTable;
        }
    }
}
