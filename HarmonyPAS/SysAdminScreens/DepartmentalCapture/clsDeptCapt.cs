using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Mineware.Systems.Global;
using System.Drawing;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    class clsDeptCapt : clsBase
    {
        #region Departments

        public DataTable LoadSumGrid()
        {
            theData.SqlStatement = "select a, yy, ww from ( \r\n" +
                                    "   select convert(varchar(10),captyear) +' Wk' + substring('00'+convert(varchar(10),actweek),3,2) a , convert(varchar(10),captyear) yy, actweek ww  \r\n" +
                                    "   from tbl_DPT_RockMechInspection where workplace like '%Sum%')b\r\n" +
                                    "   group by a, yy, ww  \r\n" +
                                    "   order by  \r\n" +
                                    "   yy desc, convert(decimal(18,0), ww) desc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadWalkAboutGrid()
        {
            theData.SqlStatement = "exec [sp_RockEng_Gridload]";

            //theData.SqlStatement = " Select * from (  " +
            //                                    "  select *, isnull(convert(decimal(18,0),0+0.4),0) aa from ( select  * \r\n " +



            //                                    "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-42))),2,2) hhwk6 \r\n " +
            //                                    "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-35))),2,2) hhwk5 \r\n " +
            //                                    "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-28))),2,2) hhwk4 \r\n " +
            //                                    "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-21))),2,2) hhwk3 \r\n" +
            //                                    "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-14))),2,2) hhwk2 \r\n" +
            //                                    "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-7))),2,2) hhwk1 \r\n" +
            //                                    "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()))),2,2) hhwnow \r\n" +

            //                                       " , case when act = '1' then 'Dev' Else 'Stp' end as activityfinal \r\n" +
            //                                    "    from (select nn, a.description, act, rr, isnull(rrr,0) rrr, case when description like '%-S%' then 'a' else isnull(wp6, '') end as wp6 , case when description like '%-S%' then 'a' else isnull(wp5, '') end as wp5, case when description like '%-S%' then 'a' else isnull(wp4, '') end as wp4 , case when description like '%-S%' then 'a' else isnull(wp3, '') end as wp3 \r\n" +
            //                                    "  , case when description like '%-S%' then 'a' else isnull(wp2, '') end as wp2, case when description like '%-S%' then 'a' else isnull(wp1, '') end as wp1 , case when description like '%-S%' then 'a' else isnull(wpnow, '') end as wpnow , isnull(colnow, '') colnow, isnull(col6, '') col6, isnull(col5, '') col5 , isnull(col4, '') col4 , isnull(col3, '') col3 , isnull(col2, '') col2 , isnull(col1, '') col1   from " +
            //                                    "(select substring(sectionid,1,3) mo,   w.description, p.workplaceid w, max(p.activity) act, max(riskrating) rr from Planning p, Workplace w where p.workplaceid = w.workplaceid \r\n" +
            //                                    "  and calendardate > getdate()-60 and calendardate < getdate()+7 \r\n" +
            //                                    "  and p.activity <> 100 group by w.description, substring(sectionid,1,3) , p.workplaceid \r\n" +

            //                                    " union \r\n" +



            //                                    " select [mo] ,[WPDescription] ,[WNo] ,[act] ,[rr] from [vw_Department_Walkabout_Sum_RockMech] \r\n" +





            //                                    ") a \r\n" +

            //                                    "  left outer  join \r\n" +
            //                                    "  (select Workplace w, 'Y' col6 from [tbl_DPT_RockMechInspection] where \r\n" +
            //                                    "   CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-42))),1,2)  and captyear = datepart(year,getdate()-42)) zz6 on a.Description = zz6.w COLLATE DATABASE_DEFAULT\r\n" +

            //                                    "  left outer  join \r\n" +
            //                                    "  (select Workplace w, 'Y' col5 from [tbl_DPT_RockMechInspection] where \r\n" +
            //                                    "  CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-35))),1,2)  and captyear = datepart(year,getdate()-35)) zz5 on a.Description = zz5.w COLLATE DATABASE_DEFAULT\r\n" +

            //                                    "  left outer  join \r\n" +
            //                                    "  (select Workplace w, 'Y' col4 from [tbl_DPT_RockMechInspection] where \r\n" +
            //                                    "  CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-28))),1,2)  and captyear = datepart(year,getdate()-28)) zz4 on a.Description = zz4.w COLLATE DATABASE_DEFAULT\r\n" +


            //                                    "  left outer  join \r\n" +
            //                                    "  (select Workplace w, 'Y' col3 from [tbl_DPT_RockMechInspection] where \r\n" +
            //                                    "  CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-21))),1,2)  and captyear = datepart(year,getdate()-21)) zz3 on a.Description = zz3.w COLLATE DATABASE_DEFAULT\r\n" +

            //                                    "  left outer  join \r\n" +
            //                                    "  (select Workplace w, 'Y' col2 from [tbl_DPT_RockMechInspection] where \r\n" +
            //                                    " CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-14))),1,2)  and captyear = datepart(year,getdate()-14)) zz2 on a.Description = zz2.w COLLATE DATABASE_DEFAULT\r\n" +

            //                                    "  left outer  join \r\n" +
            //                                    " (select Workplace w, 'Y' col1 from [tbl_DPT_RockMechInspection] where \r\n" +
            //                                    " CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-7))),1,2)  and captyear = datepart(year,getdate()-7)) zz1 on a.Description = zz1.w COLLATE DATABASE_DEFAULT\r\n" +

            //                                    "  left outer  join \r\n" +
            //                                    "  (select Workplace w, 'Y' colnow, case when cat12rate <> '' then  convert(decimal(18,0),cat12rate) else 0 end as rrr from [tbl_DPT_RockMechInspection] where  \r\n" +
            //                                    "  CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()))),1,2)  and captyear = datepart(year,getdate())) zz on a.Description = zz.w COLLATE DATABASE_DEFAULT\r\n" +

            //                                    "  left outer  join \r\n" +
            //                                    "  (select distinct(workplaceid) wpnow from Planning where activity <> 100  and \r\n" +
            //                                    "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()) and year(calendardate) = year(Getdate()) ) b on a.w = b.wpnow \r\n" +
            //                                    "  left outer  join \r\n" +
            //                                    "  (select distinct(workplaceid) wp1 from Planning where activity <> 100  and \r\n" +
            //                                    "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-7) and year(calendardate) = year(Getdate())  ) c on a.w = c.wp1 \r\n" +
            //                                    "  left outer  join \r\n" +
            //                                    "  (select distinct(workplaceid) wp2 from Planning where activity <> 100  and \r\n" +
            //                                    "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-14) and year(calendardate) = year(Getdate())  ) d on a.w = d.wp2 \r\n" +

            //                                    "   left outer  join \r\n" +
            //                                    "  (select distinct(workplaceid) wp3 from Planning where activity <> 100  and \r\n" +
            //                                    "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-21) and year(calendardate) = year(Getdate())  ) e on a.w = e.wp3 \r\n" +

            //                                    "  left outer  join \r\n" +
            //                                    "  (select distinct(workplaceid) wp4 from Planning where activity <> 100  and \r\n" +
            //                                    "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-28) and year(calendardate) = year(Getdate())  ) f on a.w = f.wp4 \r\n" +

            //                                    "    left outer  join \r\n" +
            //                                    "  (select distinct(workplaceid) wp5 from Planning where activity <> 100  and \r\n" +
            //                                    "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-35) and year(calendardate) = year(Getdate())  ) g on a.w = g.wp5 \r\n" +

            //                                    "    left outer  join \r\n" +
            //                                    "  (select distinct(workplaceid) wp6 from Planning where activity <> 100 and \r\n" +
            //                                    "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-42) and year(calendardate) = year(Getdate())  ) h on a.w = h.wp6 \r\n" +

            //                                    "  left outer join \r\n" +
            //                                    "   (select s.sectionid, s.sectionid +' '+ name nn from Section s, ( select sectionid, max(prodmonth) pm from Section where hierarchicalid = 4 \r\n" +
            //                                    "  group by sectionid) b where s.sectionid = b.sectionid and s.prodmonth = b.pm) i on a.mo = i.sectionid) a \r\n" +

            //                                    "  where (wpnow+ wp1 + wp2 + wp3 + wp4 + wp5 + wp6 <> '') or  description like '%-S%' \r\n" +


            //                                    " ) a     \r\n" +

            //                                    " )a  \r\n" +
            //                                    " Left outer join   \r\n" +
            //                                    " (Select DATEDIFF(DAY, LastVisitDate, Getdate()) DaysSince, a.* from(  \r\n" +
            //                                    "Select Max(CaptDate) LastVisitDate, Workplace from [tbl_DPT_RockMechInspection]  \r\n" +
            //                                    "Group by Workplace) a) b on a.Description = b.Workplace";

            //Risk Rating Stuff
            //" left outer join  " +
            //" (select * from [AGA_ONE].Dbo.vw_RMS_RiskRating_per_Stream " +
            //" where stream = 'RockEng' " +

            //" ) b on a.description = b.workplacename  order by nn, description ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadSumGeoGrid()
        {
            theData.SqlStatement = "select distinct(a) a from ( " +
                                           "select convert(varchar(10),captyear) +' Wk' + substring('00'+convert(varchar(10),captweek+1000),5,2) a " +
                                           "from [dbo].[tbl_DPT_GeoScienceInspection]) a order by a desc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadGeologyGrid()
        {
            theData.SqlStatement = "exec [sp_GeoInspection_Gridload]";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadVentilationGrid()
        {
            theData.SqlStatement = "exec [sp_VentInspection_GridLoad]";

            //theData.SqlStatement = "   " +
            //"   select *, isnull(convert(decimal(18,0),0+0.4),0) aa from ( select  *  \r\n " +
            //"   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 42))), 2, 2) hhwk6  \r\n" +
            //"   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 35))), 2, 2) hhwk5  \r\n" +
            //"   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 28))), 2, 2) hhwk4  \r\n" +
            //"   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 21))), 2, 2) hhwk3  \r\n" +
            //"  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 14))), 2, 2) hhwk2  \r\n" +
            //"  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 7))), 2, 2) hhwk1  \r\n" +
            //"  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate()))), 2, 2) hhwnow  \r\n" +
            //"    from(select nn, a.description, rr, isnull(wp6, '') wp6, isnull(wp5, '') wp5, isnull(wp4, '') wp4, isnull(wp3, '') wp3  \r\n" +
            //"  , isnull(wp2, '') wp2, isnull(wp1, '') wp1, isnull(wpnow, '') wpnow, isnull(colnow, '') colnow, isnull(col6, '') col6, isnull(col5, '') col5, isnull(col4, '') col4, isnull(col3, '') col3, isnull(col2, '') col2, isnull(col1, '') col1   \r\n" +
            //"  from(select substring(sectionid, 1, 3) mo, w.description, p.workplaceid w, max(riskrating) rr from planning p, workplace w where p.workplaceid = w.workplaceid  \r\n" +
            //"  and calendardate > getdate() - 60 and calendardate < getdate() + 7  \r\n" +
            //"  and p.activity <> 1 group by w.description, substring(sectionid, 1, 3), p.workplaceid) a  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select workplace w, 'Y' col6 from tbl_DPT_VentInspection where  \r\n" +
            //"   CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 42))), 1, 2) ) zz6 on a.Description = zz6.w  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select workplace w, 'Y' col5 from tbl_DPT_VentInspection where  \r\n" +
            //"  CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 35))), 1, 2) ) zz5 on a.Description = zz5.w  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select workplace w, 'Y' col4 from tbl_DPT_VentInspection where  \r\n" +
            //"  CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 28))), 1, 2) ) zz4 on a.Description = zz4.w  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select workplace w, 'Y' col3 from tbl_DPT_VentInspection where  \r\n" +
            //"  CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 21))), 1, 2) ) zz3 on a.Description = zz3.w  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select workplace w, 'Y' col2 from tbl_DPT_VentInspection where  \r\n" +
            //" CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 14))), 1, 2) ) zz2 on a.Description = zz2.w  \r\n" +
            //"  left outer  join  \r\n" +
            //" (select workplace w, 'Y' col1 from tbl_DPT_VentInspection where  \r\n" +
            //" CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 7))), 1, 2) ) zz1 on a.Description = zz1.w  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select workplace w, 'Y' colnow from tbl_DPT_VentInspection where  \r\n" +
            //"  CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate()))), 1, 2) ) zz on a.Description = zz.w  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select distinct(workplaceid) wpnow from planning where activity <> 1 and mocycle <> '' and  \r\n" +
            //"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate()) and year(calendardate) = year(Getdate())) b on a.w = b.wpnow  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select distinct(workplaceid) wp1 from planning where activity <> 1  and  \r\n" +
            //"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 7) and year(calendardate) = year(Getdate())) c on a.w = c.wp1  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select distinct(workplaceid) wp2 from planning where activity <> 1  and  \r\n" +
            //"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 14) and year(calendardate) = year(Getdate())) d on a.w = d.wp2  \r\n" +
            //"   left outer  join  \r\n" +
            //"  (select distinct(workplaceid) wp3 from planning where activity <> 1 and  \r\n" +
            //"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 21) and year(calendardate) = year(Getdate())) e on a.w = e.wp3  \r\n" +
            //"  left outer  join  \r\n" +
            //"  (select distinct(workplaceid) wp4 from planning where activity <> 1 and  \r\n" +
            //"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 28) and year(calendardate) = year(Getdate())) f on a.w = f.wp4  \r\n" +
            //"    left outer  join  \r\n" +
            //"  (select distinct(workplaceid) wp5 from planning where activity <> 1 and  \r\n" +
            //"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 35) and year(calendardate) = year(Getdate())) g on a.w = g.wp5  \r\n" +
            //"    left outer  join  \r\n" +
            //"  (select distinct(workplaceid) wp6 from planning where activity <> 1 and  \r\n" +
            //"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 42) and year(calendardate) = year(Getdate())) h on a.w = h.wp6  \r\n" +
            //"  left outer join  \r\n" +
            //"   (select s.sectionid, s.sectionid + ' ' + name nn from section s, (select sectionid, max(prodmonth)pm from section where hierarchicalid = 4  \r\n" +
            //"  group by sectionid) b where s.sectionid = b.sectionid and s.prodmonth = b.pm) i on a.mo = i.sectionid) a  \r\n" +
            //"  where wpnow + wp1 + wp2 + wp3 <> ''  \r\n" +
            //" ) a  ";

            //" --left outer join   (select * from [AGA_ONE].Dbo.vw_RMS_RiskRating_per_Stream  where stream = 'Vent'  ) b on a.description = b.workplacename ";


            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }


        #endregion

        #region RockEng

        public void SaveRockEngInspection(string SqlStatement)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
        }

        public DataTable LoadRockMechGraph(string SqlStatement, string GraphName)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ResultsTableName = GraphName;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        #endregion



        #region Sampling



        #endregion


    }
}
