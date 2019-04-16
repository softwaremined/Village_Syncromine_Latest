using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    class clsWorknoteData : clsBase
    {
        public DataTable WorkplaceGridDS(string Workplace,string Shift)
        {
            theData.SqlStatement = "select MAX(col1) col1, MAX(col2) col2, MAX(col3) col3,MAX(col4) col4,MAX(col5) col5,   \r\n" +
                          "MAX(col6) col6, MAX(col7) col7, MAX(col8) col8 , MAX(col9) col9 , MAX(col10) col10,    \r\n" +
                          "MAX(col11) col11,MAX(col12) col12,MAX(col13) col13,MAX(col14) col14,MAX(col15) col15,    \r\n" +
                          "MAX(col16) col16,MAX(col17) col17,MAX(col18) col18,MAX(col19) col19,MAX(col20) col20,    \r\n" +
                          "MAX(col21) col21,MAX(col22) col22,MAX(col23) col23,MAX(col24) col24,MAX(col25) col25,    \r\n" +
                          "MAX(col26) col26,MAX(col27) col27,MAX(col28) col28,MAX(col29) col29,MAX(col30) col30,    \r\n" +
                          "MAX(col31) col31   \r\n" +
                          "from(select     \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate(), 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate(), 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col1,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 1, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 1, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col2,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 2, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 2, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col3,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 3, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 3, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col4,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 4, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 4, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col5,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 5, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 5, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col6,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 6, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 6, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col7,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 7, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 7, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col8,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 8, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 8, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col9,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 9, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 9, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col10,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 10, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 10, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col11,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 11, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 11, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col12,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 12, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 12, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col13,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 13, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 13, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col14,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 14, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 14, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col15,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 15, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 15, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col16,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 16, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 16, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col17,   \r\n" +
                          "case     \r\n" +
                         "when calendardate = CONVERT(VARCHAR(10), getdate() - 17, 20) and wd = 'N' then 'OFF'   \r\n" +
                         " when calendardate = CONVERT(VARCHAR(10), getdate() - 17, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col18,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 18, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 18, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col19,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 19, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 19, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col20,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 20, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 20, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col21,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 21, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 21, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col22,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 22, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 22, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col23,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 23, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 23, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col24,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 24, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 24, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col25,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 25, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 25, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col26,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 26, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 26, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col27,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 27, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 27, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col28,   \r\n" +
                          "case     \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 28, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 28, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col29,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 29, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 29, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col30,   \r\n" +
                          "case    \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 30, 20) and wd = 'N' then 'OFF'   \r\n" +
                          "when calendardate = CONVERT(VARCHAR(10), getdate() - 30, 20) and wd = 'Y' And AnswerDate IS not null then 'OK'    \r\n" +
                          "else ''   \r\n" +
                          "end as col31,   \r\n" +
                          "*from(   \r\n" +
                          "Select calendardate, max(wd) wd from(   \r\n" +
                          "select w.description, a.* from vw_Plansect_workDay_Combined a   \r\n" +
                          "left outer join WORKPLACE w  on a.workplaceid = w.WorkplaceID) a   \r\n" +
                          " where calendardate > getdate() - 30   \r\n" +
                          "and calendardate < getdate()   \r\n" +
                          "and description = '" + Workplace+ "' group by calendardate   \r\n" +
                          ") a   \r\n" +
                          "left outer join   \r\n" +
                          "(select answerdate from tbl_Call_Center where worktypecat = 'STP'   \r\n" +
                          "and shaftid <> '1111111111' and answerdate >= CONVERT(VARCHAR(10), getdate() - 30, 20)   \r\n" +
                          "and wpequipno = '" + Workplace + "' and ShiftCode = '"+Shift+"'   \r\n" +
                          "group by answerdate) b on a.calendardate = b.answerdate   \r\n" +
                          ")a ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            if (Shift =="D")
            { theData.ResultsDataTable.TableName = "Data2"; }
            else
            { theData.ResultsDataTable.TableName = "DataNS"; }

            return theData.ResultsDataTable;
        }

        public DataTable CallCentre(string Workplace)
        {
            theData.SqlStatement = "select ROW_NUMBER() OVER(ORDER BY colOrder, num desc) AS Row, * from ( select Top 10 QuestionDescr, num, colOrder from( select \r\n " +
                                    "  QuestionDescr, count(QuestionDescr) num, 'a' colOrder from  tbl_Call_Center where worktypecat = 'STP' \r\n " +
                                    " and shaftid <> '11111111' and answerdate >= CONVERT(VARCHAR(10),getdate()-30, 20) \r\n " +
                                    " and wpequipno = '" + Workplace + "' \r\n " +
                                    " and ragind = 'R' group by QuestionDescr \r\n " +
                                    " union  \r\n " +
                                    " select ' ',0,'z1' \r\n " +
                                    " union \r\n " +
                                    " select '  ',0,'z2' \r\n " +
                                    " union  \r\n " +
                                    " select '   ',0,'z3' \r\n " +
                                    " union  \r\n " +
                                    " select '    ',0,'z4' \r\n " +
                                    " union  \r\n " +
                                    " select '     ',0,'z5' \r\n " +
                                    " union \r\n " +
                                    " select '      ',0,'z6' \r\n " +
                                    " union  \r\n " +
                                    " select '       ',0,'z7' \r\n " +
                                    " union  \r\n " +
                                    " select '        ',0,'z8' \r\n " +
                                    " union  \r\n " +
                                    " select '         ',0,'z9' \r\n " +
                                    " union \r\n " +
                                    " select '          ',0,'z10')a) a \r\n " +
                                    " \r\n  ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "Data3";

            return theData.ResultsDataTable;
        }

        public DataTable Seismic(string Workplace)
        {
            theData.SqlStatement = "select *, isnull(convert(varchar(10),Seismic), 'No Data') Seismic1, isnull(convert(varchar(10),risk), 'No Data') risk1 from (select getdate() Column1, '" + Workplace + "' wp,  '" + SysSettings.Banner + "' banner) a  \r\n" +

                                "left outer join (select a.wpdescription, convert(decimal(18,1),a.risk) Seismic from  (select * from tbl_LicenceToOperate_Seismic where wpdescription = '" + Workplace + "') a,  \r\n" +
                                   "(select wpdescription wp1, max(thedate) dd from  tbl_LicenceToOperate_Seismic group by wpdescription) b  \r\n" +
                                   "where a.wpdescription = b.wp1 and a.thedate = b.dd   and wpdescription = '" + Workplace + "') ac on a.wp = ac.wpdescription  \r\n" +

                                   "left outer join (select a.wpdescription, convert(decimal(18,0),a.resrisk) risk from (select * from [dbo].[tbl_LicenceToOperate_Risk]  where wpdescription = '" + Workplace + "') a,  \r\n" +
                                   "(select wpdescription wp1, max(thedate) dd from  [tbl_LicenceToOperate_Risk] group by wpdescription) b  \r\n" +
                                   "where a.wpdescription = b.wp1 and a.thedate = b.dd and wpdescription = '" + Workplace + "') ad on a.wp = ad.wpdescription  \r\n";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "Date";

            return theData.ResultsDataTable;
        }

        public DataTable Incident(string Workplace)
        {
            theData.SqlStatement = "select * from (    \r\n" +
                                        "select top(10) '' stepname, Action_Title action, hazard, ''  majorhazard, criticalcontrol, Start_Date datesubmitted, workplace, Action_Close_Date dateactionclosed, Action_Closed_By fixedby  \r\n" +
                                        " from tbl_Incidents where workplace like '" + Workplace + "'   \r\n" +
                                        " --and answer = 'No'    \r\n" +
                                        " order by datesubmitted desc   \r\n" +
                                        " ) a    \r\n" +
                                        " union select '', '','','','',null,'',null,''   \r\n" +
                                        " order by datesubmitted desc   ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "DHazard";

            return theData.ResultsDataTable;
        }

        public DataTable RockEngData(string Workplace)
        {
            theData.SqlStatement = "select top(1) *, b.SWidth from (    \r\n" +
                                        "select a.*,w.Description from[dbo].[tbl_DPT_RockMechInspection] a, workplace w    \r\n" +
                                        "where a.workplace = w.description and workplace  = '" + Workplace + "'    \r\n" +
                                        "and captdate = (select  max(captdate) from[dbo].[tbl_DPT_RockMechInspection]    \r\n" +
                                        "        where workplace = '" + Workplace + "' ))a     \r\n" +

                                        "left outer join (select max(Calendardate)Calendardate, w.WorkplaceID, w.Description, max(SWidth) SWidth " +
                                                                    " from SAMPLING s, workplace w where s.Workplaceid = w.WorkplaceID " +
                                                                    " group by s.workplaceid,w.Description,w.workplaceid ) b " +
                                                                     " on a.Workplace = b.Description " +
                                                    "  union select '', null, null,  null, '', null, null, null , null, null, null, null, null, null, null      \r\n" +
                                        ", null, null, null, null, null,  null, null, null, null, null, null, null, null, null, null, null, null    \r\n" +
                                        ", null, null, null, null, null, null, null, null,  null, null, null, null, null, null, null, null, null    \r\n" +
                                        ", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null    \r\n" +
                                        " , null, null, null, null,null,null,null,null order by captdate desc";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "RMDetails";

            return theData.ResultsDataTable;
        }

        public DataTable RockEngPicture(string Workplace)
        {
            theData.SqlStatement = "select top(1) * from (Select 1 nn, picture, '' pp from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + Workplace + "' " +
                                            "and captdate = (select  " +
                                            "max(captdate) from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + Workplace + "' ) " +
                                            "union select	2 nn, '', '') a order by nn ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "ImageRE";

            return theData.ResultsDataTable;
        }

        public DataTable LicenseToOperate(string Workplace)
        {
            theData.SqlStatement = "exec [sp_LicenceToOperate_3)WorkplaceDeviations] '" + Workplace + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "TempDetailsWPD";

            return theData.ResultsDataTable;
        }

        public DataTable WPPlanBook(string Workplace)
        {
            theData.SqlStatement = "exec [sp_MorningPlanBook] '" + Workplace +"' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "WPPlanBook";

            return theData.ResultsDataTable;
        }

        public DataTable Temp_LTO(string Workplace)
        {
            theData.SqlStatement = "exec [dbo].[sp_LicenceToOperate_TempReading] '" + Workplace + "'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "TempDetails";

            return theData.ResultsDataTable;
        }

        public DataTable EmployeeDetail(string Workplace)
        {
            theData.SqlStatement = "   select a.*, b.CrewName gangno from( \r\n" +
                                    " select top(1) *, 'http://10.1.1.113/api/resource/GetEmployeeImage?EmployeeNumber=' + EmployeeNo + '' mm, \r\n" +
                                    "  'http://10.1.1.113/api/resource/GetEmployeeImage?EmployeeNumber=' + tlnumber + '' tlll, ddd dd, secid sectionid, '30' tmp1 from( \r\n" +
                                    "  select substring(OrgUnitDay, 1, 15) ddd, substring(OrgUnitDay, 1, 7) + '_____' + '2' + substring(OrgUnitDay, 13, 3)   nn, s.EmployeeNo, s.name, s.sectionid secid from planmonth pm, planning p, workplace w, section s \r\n" +
                                    "  where p.workplaceid = w.workplaceid and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and \r\n" +
                                    "  p.prodmonth = pm.Prodmonth and p.workplaceid = pm.workplaceid and p.sectionid = pm.sectionid  and \r\n" +
                                    "  w.description = '" + Workplace + "' \r\n" +
                                    "  and calendardate = convert(varchar(15), getdate(), 106)) a \r\n" +
                                    "   left outer join(SELECT gangno gangno2, employeename tlname, employeeno tlnumber from[dbo].EmployeeAllCombined where occno in ( \r\n" +
                                    "   select occno from[EmployeeOccupation] \r\n" +
                                    "   where occdescription like '%Team L%')) b on a.ddd = b.gangno2 \r\n" +
                                    "  left outer join(SELECT gangno nsfinalgang, substring(gangno, 1, 7) + '_____' + '2' + substring(gangno, 13, 3)  gangnons, employeename tlnamens, employeeno tlnumberns, 'http://10.1.1.113/api/resource/GetEmployeeImage?EmployeeNumber=' + employeeno + '' tlllNS \r\n" +
                                    "  from[dbo].[EmployeeAllCombined] where occno in (select occno from[EmployeeOccupation] \r\n" +
                                    "  where occdescription like '%Team L%') ) c on a.nn = c.gangnons \r\n" +
                                    " )a left outer join (select CrewName, GangNo from Crew)b \r\n" +
                                    " on a.gangno2 = b.GangNo   ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "EmpDetails";

            return theData.ResultsDataTable;
        }

        public DataTable MONotes(string Workplace)
        {
            theData.SqlStatement = "select a.Workplace ,a.MoNote LatestMoNote  from tbl_MoNotes a, \r\n" +
                                        " (select workplace, max(calendardate) dd from tbl_MoNotes group by workplace) b , workplace wp   \r\n" +
                                        "where a.Workplace = b.Workplace and a.calendardate = b.dd  \r\n" +
                                        "and a.workplace = wp.WorkplaceID   \r\n" +
                                        "and wp.description = '" + Workplace + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "MoNotes";

            return theData.ResultsDataTable;
        }

        public DataTable Seismic_10Days(string Workplace)
        {
            theData.SqlStatement = "select * from  tbl_LicenceToOperate_Seismic where wpdescription = '" + Workplace + "' and TheDate >= getdate()-10 order by thedate desc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "Graph";

            return theData.ResultsDataTable;
        }

        public DataTable Radiation(string Workplace)
        {
            theData.SqlStatement = " select max(prodmonth)prodmonth, max(w.Workplaceid), max(p.OrgUnitDay),  \r\n" +
                                 "  max([CURRENT_YR_MAX_DOSE])[CURRENT_YR_MAX_DOSE], max([TOTAL_5YR_MAX_DOSE])[TOTAL_5YR_MAX_DOSE]  \r\n" +
                                 "  from planmonth p , workplace w, vw_RadiationGangData r  \r\n" +
                                 "  where w.workplaceid = p.Workplaceid  \r\n" +
                                 "  and p.PlanCode = 'MP'  \r\n" +
                                 "  and w.Description = '" + Workplace + "'  \r\n" +
                                 "  and r.[GANG_CD] = p.OrgUnitDay  \r\n";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "Radiation";

            return theData.ResultsDataTable;
        }

        public DataTable PrintOnlyWP(string Workplace,string Barcode1,string Barcode2)
        {
            theData.SqlStatement = "select '" + Barcode1 + "' + workplaceid+ '" + Barcode2 + "' barcode, convert(decimal(18,0), activity) activity, workplaceid wpid, '" + Workplace + "' WPDescription from workplace where description = '" + Workplace + "'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "Bcode";

            return theData.ResultsDataTable;
        }

        public DataTable PrintOnlyALL(string Workplace, string Barcode1,string Section)
        {
            theData.SqlStatement = "select '" + Barcode1 + "' barcode, '" + Section+ "' wpid, '" + Workplace + "' WPDescription  ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "Bcode";

            return theData.ResultsDataTable;
        }

        public DataTable SeismicChecklist()
        {
            theData.SqlStatement = "SELECT TOP 1 A.FormsID FROM [dbo].[tbl_OCR_Forms_Departmental_Checklists] A WHERE A.FormType = 'Worknote_Seismic'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            theData.ResultsDataTable.TableName = "Seismic";

            return theData.ResultsDataTable;
        }


    }
}
