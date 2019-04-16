using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SetupCycles
{
    class clsSetupCycles : clsBase
    {
        public DataTable LoadCycleCodes()
        {
            theData.SqlStatement = "SELECT * FROM CODE_CYCLE";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadStopingCycleNames()
        {
            theData.SqlStatement = " select distinct(Name) Name from Code_Cycle_RawData where Type = 'S' order by Name   ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadDevelopementCycleNames()
        {
            theData.SqlStatement = " select distinct(Name) Name from Code_Cycle_RawData where Type = 'D' order by Name   ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadMOCyclesList()
        {
            theData.SqlStatement = "select Distinct Name,Type from Code_Cycle_RawData";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadMOCycleGrid()
        {
            theData.SqlStatement = "select Distinct SectionID+ ':' + a.name sec,isnull(StopingCycle,'') StopingCycle,isnull(DevCycle,'') DevCycle from (Select a.sectionid, name from SECTION a, (    \r\n" +
"Select sectionid, max(prodmonth)pm from SECTION where Hierarchicalid = '4' and Prodmonth = (select currentproductionmonth from sysset) group by sectionid) b where a.SectionID = b.SectionID and a.Prodmonth = b.pm) a   \r\n" +
"left outer join   \r\n" +
"(   \r\n" +

"select a.sec,case when mo.StopingCycle is null then '' else mo.StopingCycle end as StopingCycle,   \r\n" +
"case when mo.DevCycle is null then '' else mo.DevCycle end as DevCycle, c.Name from(   \r\n" +


"Select distinct s3.sectionid sec, s3.Name   \r\n" +
"from section s, section s2, SECTION s3   \r\n" +
"where s.reporttosectionid = s2.sectionid  and s.prodmonth = s2.prodmonth   \r\n" +
"and s2.reporttosectionid = s3.sectionid     and s2.prodmonth = s3.prodmonth   \r\n" +
"and s.Prodmonth = (select currentproductionmonth from sysset)   \r\n" +

") a   \r\n" +
" left outer join   \r\n" +
" Code_Cycle_MOCycleConfig mo on a.sec = mo.Sectionid   \r\n" +
" left outer join   \r\n" +
" (select Distinct sectionid, name from SECTION where  Hierarchicalid = 4) c   \r\n" +
" on a.sec = c.sectionid  where c.Name is not null   \r\n" +

" ) b on a.SectionID = b.sec   \r\n" +
" --where StopingCycle is not null  \r\n"+
" ---order by a.sectionid ";

            //theData.SqlStatement = "select SectionID+ ':' + a.name sec,StopingCycle,DevCycle from (Select a.sectionid, name from SECTION a, (    \r\n" +

            //"Select sectionid, max(prodmonth)pm from SECTION where Hierarchicalid = '4' group by sectionid) b where a.SectionID = b.SectionID and a.Prodmonth = b.pm) a    \r\n" +
            //"left outer join    \r\n" +
            //"(    \r\n" +
            //"select a.sec,case when mo.StopingCycle is null then '' else mo.StopingCycle end as StopingCycle,    \r\n" +
            //"case when mo.DevCycle is null then '' else mo.DevCycle end as DevCycle, c.Name from(    \r\n" +
            //"Select distinct s3.sectionid sec, s3.Name    \r\n" +
            //"from planning p, section s, section s2, SECTION s3    \r\n" +
            //"where p.prodmonth = s.prodmonth and p.sectionid = s.sectionid    \r\n" +
            //"and s.reporttosectionid = s2.sectionid  and s.prodmonth = s2.prodmonth    \r\n" +
            //"and s2.reporttosectionid = s3.sectionid     and s2.prodmonth = s3.prodmonth    \r\n" +
            //"and p.Prodmonth = (select max(Prodmonth) FROM planning)) a    \r\n" +
            //" left outer join    \r\n" +
            //" Code_Cycle_MOCycleConfig mo on a.sec = mo.Sectionid    \r\n" +
            //" left outer join    \r\n" +
            //" (select sectionid, name from SECTION where Prodmonth = (select MAX(prodmonth) from section) and Hierarchicalid = 4) c    \r\n" +
            //" on a.sec = c.sectionid  ) b on a.SectionID = b.sec    \r\n" +
            //" order by a.sectionid ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadMOCycleGridExcep()
        {
            theData.SqlStatement = "select c.Workplaceid, c.Cycle,w.Description,w.Workplaceid +':'+ w.Description Wp from Code_Cycle_WPExceptions c    \r\n" +
            " left outer join WORKPLACE w    \r\n" +
            " on c.Workplaceid = w.WorkplaceID    \r\n" +
            " order by c.workplaceid";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadExcepWP()
        {
            theData.SqlStatement = " Select * from Workplace ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadExcepCycles()
        {
            theData.SqlStatement = " select Distinct Name Name from Code_Cycle_RawData  ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadStopingCycles(string Num)
        {
            theData.SqlStatement = " select * from Code_Cycle_RawData where Name = '" + Num + "' and Type = 'S' order by FL ASC ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadDevelopementCycles(string Num)
        {
            theData.SqlStatement = " select *,CONVERT(varchar(5),e.endtypeid) + ':' + e.description Endtypeaa from dbo.EndType e left outer join  \r\n" +
            " (select * from dbo.Code_Cycle_RawData where Name = '" + Num.ToString() + "') a on e.EndTypeID = a.fl \r\n" +
            " order by EndTypeID ";
            //theData.SqlStatement = " select *,CONVERT(varchar(5),e.endtypeid) + ':' + e.description Endtypeaa from dbo.Code_EndType e left outer join  \r\n" +
            //" (select * from dbo.Code_Cycle_RawData where Name = '" + Num.ToString() + "') a on e.EndTypeID = a.fl \r\n" +
            //" order by EndTypeID ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadLedgeCycles()
        {
            theData.SqlStatement = "Select * from dbo.Code_Cycle_Ledging ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public void SaveStoping(string SqlStatement)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
        }

        public void SaveLedging(string SqlStatement)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
        }

        public void SaveDevelopement(string SqlStatement)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
        }

        public void SaveMOCycle(string SqlStatement)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
        }

        public void AddMOException(string SqlStatement)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
        }

        public void DeleteMOException(string SqlStatement)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
        }

        #region Departments

        public DataTable LoadSumGrid()
        {
            theData.SqlStatement = "select a, yy, ww from ( \r\n" +
                                    "   select convert(varchar(10),captyear) +' Wk' + substring('00'+convert(varchar(10),actweek),3,2) a , convert(varchar(10),captyear) yy, actweek ww  \r\n" +
                                    "   from tbl_DPT_RockMechInspection where workplace like '%Sum%') a  \r\n" +
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
            theData.SqlStatement = " Select * from (  " +
                                                "  select *, isnull(convert(decimal(18,0),0+0.4),0) aa from ( select  * \r\n " +



                                                "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-42))),2,2) hhwk6 \r\n " +
                                                "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-35))),2,2) hhwk5 \r\n " +
                                                "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-28))),2,2) hhwk4 \r\n " +
                                                "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-21))),2,2) hhwk3 \r\n" +
                                                "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-14))),2,2) hhwk2 \r\n" +
                                                "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()-7))),2,2) hhwk1 \r\n" +
                                                "  , 'Wk-' + substring(convert(varchar(5),100+(DATEPART(ISOWK,Getdate()))),2,2) hhwnow \r\n" +

                                                   " , case when act = '1' then 'Dev' Else 'Stp' end as activityfinal \r\n" +
                                                "    from (select nn, a.description, act, rr, isnull(rrr,0) rrr, case when description like '%-S%' then 'a' else isnull(wp6, '') end as wp6 , case when description like '%-S%' then 'a' else isnull(wp5, '') end as wp5, case when description like '%-S%' then 'a' else isnull(wp4, '') end as wp4 , case when description like '%-S%' then 'a' else isnull(wp3, '') end as wp3 \r\n" +
                                                "  , case when description like '%-S%' then 'a' else isnull(wp2, '') end as wp2, case when description like '%-S%' then 'a' else isnull(wp1, '') end as wp1 , case when description like '%-S%' then 'a' else isnull(wpnow, '') end as wpnow , isnull(colnow, '') colnow, isnull(col6, '') col6, isnull(col5, '') col5 , isnull(col4, '') col4 , isnull(col3, '') col3 , isnull(col2, '') col2 , isnull(col1, '') col1   from " +
                                                "(select substring(sectionid,1,3) mo,   w.description, p.workplaceid w, max(p.activity) act, max(riskrating) rr from Planning p, Workplace w where p.workplaceid = w.workplaceid \r\n" +
                                                "  and calendardate > getdate()-60 and calendardate < getdate()+7 \r\n" +
                                                "  and p.activity <> 100 group by w.description, substring(sectionid,1,3) , p.workplaceid \r\n" +

                                                " union \r\n" +



                                                " select [mo] ,[WPDescription] ,[WNo] ,[act] ,[rr] from [vw_Department_Walkabout_Sum_RockMech] \r\n" +





                                                ") a \r\n" +

                                                "  left outer  join \r\n" +
                                                "  (select Workplace w, 'Y' col6 from [tbl_DPT_RockMechInspection] where \r\n" +
                                                "   CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-42))),1,2)  and captyear = datepart(year,getdate()-42)) zz6 on a.Description = zz6.w COLLATE DATABASE_DEFAULT\r\n" +

                                                "  left outer  join \r\n" +
                                                "  (select Workplace w, 'Y' col5 from [tbl_DPT_RockMechInspection] where \r\n" +
                                                "  CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-35))),1,2)  and captyear = datepart(year,getdate()-35)) zz5 on a.Description = zz5.w COLLATE DATABASE_DEFAULT\r\n" +

                                                "  left outer  join \r\n" +
                                                "  (select Workplace w, 'Y' col4 from [tbl_DPT_RockMechInspection] where \r\n" +
                                                "  CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-28))),1,2)  and captyear = datepart(year,getdate()-28)) zz4 on a.Description = zz4.w COLLATE DATABASE_DEFAULT\r\n" +


                                                "  left outer  join \r\n" +
                                                "  (select Workplace w, 'Y' col3 from [tbl_DPT_RockMechInspection] where \r\n" +
                                                "  CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-21))),1,2)  and captyear = datepart(year,getdate()-21)) zz3 on a.Description = zz3.w COLLATE DATABASE_DEFAULT\r\n" +

                                                "  left outer  join \r\n" +
                                                "  (select Workplace w, 'Y' col2 from [tbl_DPT_RockMechInspection] where \r\n" +
                                                " CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-14))),1,2)  and captyear = datepart(year,getdate()-14)) zz2 on a.Description = zz2.w COLLATE DATABASE_DEFAULT\r\n" +

                                                "  left outer  join \r\n" +
                                                " (select Workplace w, 'Y' col1 from [tbl_DPT_RockMechInspection] where \r\n" +
                                                " CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()-7))),1,2)  and captyear = datepart(year,getdate()-7)) zz1 on a.Description = zz1.w COLLATE DATABASE_DEFAULT\r\n" +

                                                "  left outer  join \r\n" +
                                                "  (select Workplace w, 'Y' colnow, case when cat12rate <> '' then  convert(decimal(18,0),cat12rate) else 0 end as rrr from [tbl_DPT_RockMechInspection] where  \r\n" +
                                                "  CaptWeek =  substring(convert(varchar(5),(DATEPART(ISOWK,Getdate()))),1,2)  and captyear = datepart(year,getdate())) zz on a.Description = zz.w COLLATE DATABASE_DEFAULT\r\n" +

                                                "  left outer  join \r\n" +
                                                "  (select distinct(workplaceid) wpnow from Planning where activity <> 100  and \r\n" +
                                                "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()) and year(calendardate) = year(Getdate()) ) b on a.w = b.wpnow \r\n" +
                                                "  left outer  join \r\n" +
                                                "  (select distinct(workplaceid) wp1 from Planning where activity <> 100  and \r\n" +
                                                "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-7) and year(calendardate) = year(Getdate())  ) c on a.w = c.wp1 \r\n" +
                                                "  left outer  join \r\n" +
                                                "  (select distinct(workplaceid) wp2 from Planning where activity <> 100  and \r\n" +
                                                "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-14) and year(calendardate) = year(Getdate())  ) d on a.w = d.wp2 \r\n" +

                                                "   left outer  join \r\n" +
                                                "  (select distinct(workplaceid) wp3 from Planning where activity <> 100  and \r\n" +
                                                "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-21) and year(calendardate) = year(Getdate())  ) e on a.w = e.wp3 \r\n" +

                                                "  left outer  join \r\n" +
                                                "  (select distinct(workplaceid) wp4 from Planning where activity <> 100  and \r\n" +
                                                "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-28) and year(calendardate) = year(Getdate())  ) f on a.w = f.wp4 \r\n" +

                                                "    left outer  join \r\n" +
                                                "  (select distinct(workplaceid) wp5 from Planning where activity <> 100  and \r\n" +
                                                "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-35) and year(calendardate) = year(Getdate())  ) g on a.w = g.wp5 \r\n" +

                                                "    left outer  join \r\n" +
                                                "  (select distinct(workplaceid) wp6 from Planning where activity <> 100 and \r\n" +
                                                "  DATEPART(ISOWK,calendardate) = DATEPART(ISOWK,Getdate()-42) and year(calendardate) = year(Getdate())  ) h on a.w = h.wp6 \r\n" +

                                                "  left outer join \r\n" +
                                                "   (select s.sectionid, s.sectionid +' '+ name nn from Section s, ( select sectionid, max(prodmonth) pm from Section where hierarchicalid = 4 \r\n" +
                                                "  group by sectionid) b where s.sectionid = b.sectionid and s.prodmonth = b.pm) i on a.mo = i.sectionid) a \r\n" +

                                                "  where (wpnow+ wp1 + wp2 + wp3 + wp4 + wp5 + wp6 <> '') or  description like '%-S%' \r\n" +


                                                " ) a     \r\n" +

                                                " )a  \r\n" +
                                                " Left outer join   \r\n" +
                                                " (Select DATEDIFF(DAY, LastVisitDate, Getdate()) DaysSince, a.* from(  \r\n" +
                                                "Select Max(CaptDate) LastVisitDate, Workplace from [tbl_DPT_RockMechInspection]  \r\n" +
                                                "Group by Workplace) a) b on a.Description = b.Workplace";

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
            theData.SqlStatement = "     select  *    \r\n"+
"   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 42))), 2, 2) hhwk6  \r\n" +
"   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 35))), 2, 2) hhwk5  \r\n" +
"   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 28))), 2, 2) hhwk4  \r\n" +
"   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 21))), 2, 2) hhwk3  \r\n" +
"  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 14))), 2, 2) hhwk2  \r\n" +
"  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 7))), 2, 2) hhwk1  \r\n" +
"  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate()))), 2, 2) hhwnow  \r\n" +
"    from(select nn, a.description, rr, isnull(wp6, '') wp6, isnull(wp5, '') wp5, isnull(wp4, '') wp4, isnull(wp3, '') wp3  \r\n" +
"  , isnull(wp2, '') wp2, isnull(wp1, '') wp1, isnull(wpnow, '') wpnow, isnull(colnow, '') colnow, isnull(col6, '') col6, isnull(col5, '') col5, isnull(col4, '') col4, isnull(col3, '') col3, isnull(col2, '') col2, isnull(col1, '') col1  \r\n" +
"  from(select substring(sectionid, 1, 3) mo, w.description, p.workplaceid w, max(riskrating) rr from planning p, workplace w where p.workplaceid = w.workplaceid  \r\n" +
"  and calendardate > getdate() - 60 and calendardate < getdate() + 7  \r\n" +
"  and p.activity <> 1 group by w.description, substring(sectionid, 1, 3), p.workplaceid) a  \r\n" +
"  left outer  join  \r\n" +
"  (select workplace w, 'Y' col6 from tbl_DPT_GeoScienceInspection where  \r\n" +
"   convert(decimal(18, 0), captweek) = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 42))), 1, 2) and captdate > getdate() - 150) zz6 on a.Description = zz6.w  \r\n" +
"  left outer  join  \r\n" +
"  (select workplace w, 'Y' col5 from tbl_DPT_GeoScienceInspection where  \r\n" +
"  convert(decimal(18, 0), captweek) = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 35))), 1, 2) and captdate > getdate() - 150) zz5 on a.Description = zz5.w  \r\n" +
"  left outer  join  \r\n" +
"  (select workplace w, 'Y' col4 from tbl_DPT_GeoScienceInspection where  \r\n" +
"  convert(decimal(18, 0), captweek) = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 28))), 1, 2) and captdate > getdate() - 150) zz4 on a.Description = zz4.w  \r\n" +
"  left outer  join  \r\n" +
"  (select workplace w, 'Y' col3 from tbl_DPT_GeoScienceInspection where  \r\n" +
"  convert(decimal(18, 0), captweek) = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 21))), 1, 2) and captdate > getdate() - 150) zz3 on a.Description = zz3.w  \r\n" +
"  left outer  join  \r\n" +
"  (select workplace w, 'Y' col2 from tbl_DPT_GeoScienceInspection where  \r\n" +
" convert(decimal(18, 0), captweek) = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 14))), 1, 2) and captdate > getdate() - 150) zz2 on a.Description = zz2.w  \r\n" +
"  left outer  join  \r\n" +
" (select workplace w, 'Y' col1 from tbl_DPT_GeoScienceInspection where  \r\n" +
" convert(decimal(18, 0), captweek) = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 7))), 1, 2) and captdate > getdate() - 150) zz1 on a.Description = zz1.w  \r\n" +
"  left outer  join  \r\n" +
"  (select workplace w, 'Y' colnow from tbl_DPT_GeoScienceInspection where  \r\n" +
"  convert(decimal(18, 0), captweek) = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate()))), 1, 2) and captdate > getdate() - 150) zz on a.Description = zz.w  \r\n" +
"  left outer  join  \r\n" +
"  (select distinct(workplaceid) wpnow from planning where activity <> 1 and mocycle <> '' and  \r\n" +
"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate())) b on a.w = b.wpnow  \r\n" +
"  left outer  join  \r\n" +
"  (select distinct(workplaceid) wp1 from planning where activity <> 1 and  \r\n" +
"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 7)) c on a.w = c.wp1  \r\n" +
"  left outer  join  \r\n" +
"  (select distinct(workplaceid) wp2 from planning where activity <> 1 and  \r\n" +
"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 14)) d on a.w = d.wp2  \r\n" +
"   left outer  join  \r\n" +
"  (select distinct(workplaceid) wp3 from planning where activity <> 1 and  \r\n" +
"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 21)) e on a.w = e.wp3  \r\n" +
"  left outer  join  \r\n" +
"  (select distinct(workplaceid) wp4 from planning where activity <> 1 and  \r\n" +
"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 28)) f on a.w = f.wp4  \r\n" +
"    left outer  join  \r\n" +
"  (select distinct(workplaceid) wp5 from planning where activity <> 1 and  \r\n" +
"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 35)) g on a.w = g.wp5  \r\n" +
"    left outer  join  \r\n" +
"  (select distinct(workplaceid) wp6 from planning where activity <> 1 and  \r\n" +
"  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 42)) h on a.w = h.wp6  \r\n" +
"  left outer join  \r\n" +
"   (select s.sectionid, s.sectionid + ' ' + name nn from section s, (select sectionid, max(prodmonth)pm from section where hierarchicalid = 4  \r\n" +
"  group by sectionid) b where s.sectionid = b.sectionid and s.prodmonth = b.pm) i on a.mo = i.sectionid) a  \r\n" +
" where wpnow + wp1 + wp2 + wp3 <> '' \r\n";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        public DataTable LoadVentilationGrid()
        {
            theData.SqlStatement = "   " +
            "   select *, isnull(convert(decimal(18,0),0+0.4),0) aa from ( select  *  \r\n "+
            "   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 42))), 2, 2) hhwk6  \r\n"+
            "   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 35))), 2, 2) hhwk5  \r\n"+
            "   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 28))), 2, 2) hhwk4  \r\n"+
            "   , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 21))), 2, 2) hhwk3  \r\n"+
            "  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 14))), 2, 2) hhwk2  \r\n"+
            "  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate() - 7))), 2, 2) hhwk1  \r\n"+
            "  , 'Wk-' + substring(convert(varchar(5), 100 + (DATEPART(ISOWK, Getdate()))), 2, 2) hhwnow  \r\n"+
            "    from(select nn, a.description, rr, isnull(wp6, '') wp6, isnull(wp5, '') wp5, isnull(wp4, '') wp4, isnull(wp3, '') wp3  \r\n"+
            "  , isnull(wp2, '') wp2, isnull(wp1, '') wp1, isnull(wpnow, '') wpnow, isnull(colnow, '') colnow, isnull(col6, '') col6, isnull(col5, '') col5, isnull(col4, '') col4, isnull(col3, '') col3, isnull(col2, '') col2, isnull(col1, '') col1   \r\n"+
            "  from(select substring(sectionid, 1, 3) mo, w.description, p.workplaceid w, max(riskrating) rr from planning p, workplace w where p.workplaceid = w.workplaceid  \r\n"+
            "  and calendardate > getdate() - 60 and calendardate < getdate() + 7  \r\n"+
            "  and p.activity <> 1 group by w.description, substring(sectionid, 1, 3), p.workplaceid) a  \r\n"+
            "  left outer  join  \r\n"+
            "  (select workplace w, 'Y' col6 from tbl_DPT_VentInspection where  \r\n"+
            "   CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 42))), 1, 2) ) zz6 on a.Description = zz6.w  \r\n"+
            "  left outer  join  \r\n"+
            "  (select workplace w, 'Y' col5 from tbl_DPT_VentInspection where  \r\n"+
            "  CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 35))), 1, 2) ) zz5 on a.Description = zz5.w  \r\n"+
            "  left outer  join  \r\n"+
            "  (select workplace w, 'Y' col4 from tbl_DPT_VentInspection where  \r\n"+
            "  CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 28))), 1, 2) ) zz4 on a.Description = zz4.w  \r\n"+
            "  left outer  join  \r\n"+
            "  (select workplace w, 'Y' col3 from tbl_DPT_VentInspection where  \r\n"+
            "  CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 21))), 1, 2) ) zz3 on a.Description = zz3.w  \r\n"+
            "  left outer  join  \r\n"+
            "  (select workplace w, 'Y' col2 from tbl_DPT_VentInspection where  \r\n"+
            " CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 14))), 1, 2) ) zz2 on a.Description = zz2.w  \r\n"+
            "  left outer  join  \r\n"+
            " (select workplace w, 'Y' col1 from tbl_DPT_VentInspection where  \r\n"+
            " CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate() - 7))), 1, 2) ) zz1 on a.Description = zz1.w  \r\n"+
            "  left outer  join  \r\n"+
            "  (select workplace w, 'Y' colnow from tbl_DPT_VentInspection where  \r\n"+
            "  CaptWeek = substring(convert(varchar(5), (DATEPART(ISOWK, Getdate()))), 1, 2) ) zz on a.Description = zz.w  \r\n"+
            "  left outer  join  \r\n"+
            "  (select distinct(workplaceid) wpnow from planning where activity <> 1 and mocycle <> '' and  \r\n"+
            "  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate()) and year(calendardate) = year(Getdate())) b on a.w = b.wpnow  \r\n"+
            "  left outer  join  \r\n"+
            "  (select distinct(workplaceid) wp1 from planning where activity <> 1  and  \r\n"+
            "  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 7) and year(calendardate) = year(Getdate())) c on a.w = c.wp1  \r\n"+
            "  left outer  join  \r\n"+
            "  (select distinct(workplaceid) wp2 from planning where activity <> 1  and  \r\n"+
            "  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 14) and year(calendardate) = year(Getdate())) d on a.w = d.wp2  \r\n"+
            "   left outer  join  \r\n"+
            "  (select distinct(workplaceid) wp3 from planning where activity <> 1 and  \r\n"+
            "  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 21) and year(calendardate) = year(Getdate())) e on a.w = e.wp3  \r\n"+
            "  left outer  join  \r\n"+
            "  (select distinct(workplaceid) wp4 from planning where activity <> 1 and  \r\n"+
            "  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 28) and year(calendardate) = year(Getdate())) f on a.w = f.wp4  \r\n"+
            "    left outer  join  \r\n"+
            "  (select distinct(workplaceid) wp5 from planning where activity <> 1 and  \r\n"+
            "  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 35) and year(calendardate) = year(Getdate())) g on a.w = g.wp5  \r\n"+
            "    left outer  join  \r\n"+
            "  (select distinct(workplaceid) wp6 from planning where activity <> 1 and  \r\n"+
            "  DATEPART(ISOWK, calendardate) = DATEPART(ISOWK, Getdate() - 42) and year(calendardate) = year(Getdate())) h on a.w = h.wp6  \r\n"+
            "  left outer join  \r\n"+
            "   (select s.sectionid, s.sectionid + ' ' + name nn from section s, (select sectionid, max(prodmonth)pm from section where hierarchicalid = 4  \r\n"+
            "  group by sectionid) b where s.sectionid = b.sectionid and s.prodmonth = b.pm) i on a.mo = i.sectionid) a  \r\n"+
            "  where wpnow + wp1 + wp2 + wp3 <> ''  \r\n"+
            " ) a  ";

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

        public DataTable LoadRockMechGraph(string SqlStatement,string GraphName)
        {
            theData.SqlStatement = SqlStatement;
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ResultsTableName = GraphName;
            theData.ExecuteInstruction();

            return theData.ResultsDataTable;
        }

        #endregion

    }
}
