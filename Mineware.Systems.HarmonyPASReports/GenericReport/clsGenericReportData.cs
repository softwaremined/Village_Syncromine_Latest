﻿using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.GenericReport
{
    class clsGenericReportData
    {
        private Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();

        public string connectionString;
        public string DBTag;
        public DataTable get_Sysset()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " select *, theRunDate = Convert(varchar(10), RunDate, 120) from Sysset ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_HierID(string thePMonth, string theSectionID)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " select HierarchicalID, Name from Section where " +
                                        " ProdMonth = '" + thePMonth + "'  " +
                                        " and sectionID  = '" + theSectionID + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_HierID", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Sections(string _prodmonth)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " select SectionID,Name,HierarchicalID \r\n " +
                        " from Section \r\n " +
                        " where ProdMonth = '" + _prodmonth + "' " +
                        " order by HierarchicalID, Name";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_Sections", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }


        public DataTable get_Level()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = "select '0'OreflowID, 'All Levels'Name, '0'LevelNumber union select OreflowID, Name, LevelNumber from OREFLOWENTITIES where oreflowcode = 'LVL' \r\n " +
                      
                        " ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_Sections", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable get_Activity()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " select '0' Act, 'Stoping & Ledging' ActDesc \r\n " +
                        " union all \r\n " +
                        " select '1' Act, 'Stoping' ActDesc  " +
                        " union all \r\n " +
                        " select '2' Act, 'Ledging' ActDesc  ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_Activity", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Sections(string theFPMonth, string theTPMonth)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " select distinct(SectionID),Name,HierarchicalID from Section where " +
                                                " ProdMonth >= '" + theFPMonth + "'and  ProdMonth <= '" + theTPMonth + "' \r\n " +
                                                " order by HierarchicalID, Name";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_Sections", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_LevSum()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " Select HierarchicalID, Description \r\n " +
                " from Hierarch  \r\n " +
                " order by HierarchicalID ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportData", "get_LevSum", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_MinMaxDatesForSection(string theFPMonth, string theTPMonth, string theSectionID, string theHierID)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " select min(s.BeginDate) StartDate, max(s.EndDate) EndDate \r\n " +
                        " from Seccal s \r\n " +
                        " inner join Section_Complete sc on \r\n " +
                        "   sc.ProdMonth = s.ProdMonth and \r\n " +
                        "   sc.SectionID_1 = s.SectionID \r\n " +
                        " where s.ProdMonth >= '" + theFPMonth + "' and s.ProdMonth <= '" + theTPMonth + "'  \r\n ";
            if (theHierID == "5")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_1  = '" + theSectionID + "' ";
            if (theHierID == "4")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_2  = '" + theSectionID + "' ";
            if (theHierID == "3")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_3  = '" + theSectionID + "' ";
            if (theHierID == "2")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_4  = '" + theSectionID + "' ";
            if (theHierID == "1")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_5 = '" + theSectionID + "' ";

            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_MinMaxDatesForSection", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable get_MinMaxMonthsForSection(string theFDate, string theTDate, string theSectionID, string theHierID)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                        " select min(BeginMonth) BeginMonth, max(EndMonth) EndMonth \r\n " +
                        " from \r\n " +
                        " ( \r\n " +
                        "    select min(s.ProdMonth) BeginMonth,  min(s.ProdMonth) EndMonth \r\n " +
                        "   from Seccal s \r\n " +
                        "   inner join Section_Complete sc on \r\n " +
                        "     sc.ProdMonth = s.ProdMonth and \r\n " +
                        "     sc.SectionID_1 = s.SectionID \r\n " +
                        "   where BeginDate <= '" + theFDate + "' and EndDate >= '" + theFDate + "' \r\n ";
            if (theHierID == "6")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_1  = '" + theSectionID + "' ";
            if (theHierID == "5")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_2  = '" + theSectionID + "' ";
            if (theHierID == "4")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_3  = '" + theSectionID + "' ";
            if (theHierID == "3")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_4  = '" + theSectionID + "' ";
            if (theHierID == "2")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_5 = '" + theSectionID + "' ";
            if (theHierID == "1")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_6  = '" + theSectionID + "' ";
            theData.SqlStatement = theData.SqlStatement + "   union \r\n " +
                        "      select max(s.ProdMonth) BeginMonth,  max(s.ProdMonth) EndMonth \r\n " +
                        "   from Seccal s \r\n " +
                        "   inner join Section_Complete sc on \r\n " +
                        "     sc.ProdMonth = s.ProdMonth and \r\n " +
                        "     sc.SectionID_1 = s.SectionID \r\n " +
                        "   where StartDate <= '" + theTDate + "' and EndDate >= '" + theTDate + "' \r\n ";
                       // "   and sc.sectionID_6  = 'SD * S' \r\n ";
            if (theHierID == "5")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_1  = '" + theSectionID + "' ";
            if (theHierID == "4")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_2  = '" + theSectionID + "' ";
            if (theHierID == "3")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_3  = '" + theSectionID + "' ";
            if (theHierID == "2")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_4  = '" + theSectionID + "' ";
            if (theHierID == "1")
                theData.SqlStatement = theData.SqlStatement + " and sc.sectionID_5 = '" + theSectionID + "' ";

            theData.SqlStatement = theData.SqlStatement + " ) a ";

            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_MinMaxMonthsForSection", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable get_MinMaxMonths(string theFDate, string theTDate)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                        " select max(MinProd) MinProd, max(MaxProd) MaxProd \r\n " +
                        " from \r\n " +
                        " ( \r\n " +
                        "  select min(sc.PRODMONTH) MinProd, 0 MaxProd \r\n " +
                        "  from SECTION_COMPLETE sc \r\n " +
                        "  inner join SECCAL s on \r\n " +
                        "    s.Prodmonth = sc.Prodmonth and \r\n " +
                        "    s.SectionID = sc.SECTIONID_1  \r\n " +
                        "  where s.BeginDate <= '" + theFDate + "' and s.EndDate >= '" + theFDate + "' \r\n " +
                        "  union \r\n " +
                        "  select 0, max(sc.PRODMONTH) MaxProd \r\n " +
                        "  from SECTION_COMPLETE sc \r\n " +
                        "  inner join SECCAL s on \r\n " +
                        "    s.Prodmonth = sc.Prodmonth and  \r\n " +
                        "    s.SectionID = sc.SECTIONID_1  \r\n " +
                        "  where s.BeginDate <= '" + theTDate + "' and s.EndDate >= '" + theTDate + "' \r\n " +
                        "   ) a ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_MinMaxMonths", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable get_ExistReports(string theUserID, string theExistReportName)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " select \r\n ";
            if (theExistReportName != "")
                theData.SqlStatement = theData.SqlStatement + " * \r\n ";
            else
                theData.SqlStatement = theData.SqlStatement + " ReportName \r\n ";
            theData.SqlStatement = theData.SqlStatement + " from GenericReport_Options where " +
                                                        " UserID = '" + theUserID + "' \r\n ";
            if (theExistReportName != "")
                theData.SqlStatement = theData.SqlStatement +
                        " and ReportName = '" + theExistReportName + "' \r\n ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsBookingsABS", "get_ExistReports", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public void saveReport(string theUserID, string theSaveReport, string theDateType,
                                int thePMonth, int theFPMonth, int theTPMonth,
                                string theCalenDate, string theFromDate, string theToDate,
                                string theSecionID, string theHierID, string theName,
                                string theLevelGM, string theLevelMN, string theLevelMNM, 
                                string theLevelMO,
                                string theLevelSB, string theLevelMiner, string theLevelWP,
                                string thePlanDyn, string thePlanLock, string thePlanDynProg, string thePlanLockProg,
                                string theBookings, string theMeasurement, string thePlanBuss, 
                                string theForeCast, string theAbsVar,
                                string theAuthPlanOnly, string theStopeLedge,
                                string theStpSqm, string theStpSqmOn, string theStpSqmOff,
                                string theStpSqmOS, string theStpSqmOSF,
                                string theStpCmgt, string theStpCmgtTotal, string theStpGT, string theStpGTTotal,
                                string theStpSW, string theStpSWIdeal, string theStpSWFault,
                                string theStpCW, string theStpKG,
                                string theStpFL, string theStpFLOn, string theStpFLOff, string theStpFLOS,
                                string theStpAdv, string theStpAdvOn, string theStpAdvOff,
                                string theStpTons, string theStpTonsOn, string theStpTonsOff,
                                string theStpTonsOS, string theStpTonsFault,
                                string theStpCubics, string theStpCubTons, string theStpCubGT, string theStpCubKG,
                                string theStpMeasSweeps, string theStpLabour, string theStpShftInfo,
                                string theDevAdv, string theDevAdvOn, string theDevAdvOff,
                                string theDevEH, string theDevEW,
                                string theDevTons, string theDevTonsOn, string theDevTonsOff,
                                string theDevCmgt, string theDevCmgtTotal, string theDevGT, string theDevGTTotal,
                                string theDevKG,
                                string theDevCubics, string theDevCubGT, string theDevCubTons, string theDevCubKG,
                                string theDevLabour, string theDevShftInfo, string theDevDrillRig)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " delete from GenericReport_Options \r\n " +
                                        " where ReportName = '" + theSaveReport + "' \r\n " +
                                        " and UserID = '" + theUserID + "' \r\n ";

            theData.SqlStatement = theData.SqlStatement + " insert into GenericReport_Options ( \r\n " +
                    " UserID, ReportName, \r\n " +
                    " DateType, ProdMonth, FromProdMonth, ToProdMonth, CalendarDate, FromDate, ToDate, \r\n " +
                    " SectionID, HierID, SectionName, \r\n " +
                    " LevelGM, LevelMN, LevelMNM, LevelMO, LevelSB, LevelMiner, LevelWP, \r\n " +
                    " PlanDyn, PlanLock, PlanDynProg, PlanLockProg, Book, Meas, PlanBuss, FC, AbsVar, \r\n " +
                    " AuthPlanOnly, StopeLedge, \r\n " +
                    " StpSqm, StpSqmOn, StpSqmOff, StpSqmOS, StpSqmOSF, \r\n " +
                    " StpCmgt, StpCmgtTot, StpGT, StpGTTot, StpSW, StpSWIdeal, StpSWFault, \r\n " +
                    " StpCW, StpKG, \r\n " +
                    " StpFL, StpFLOn, StpFLOff, StpFLOS, \r\n " +
                    " StpAdv, StpAdvOn, StpAdvOff, \r\n " +
                    " StpTons, StpTonsOn, StpTonsOff, StpTonsOS, StpTonsFault, \r\n " +
                    " StpCubics, StpCubTons, StpCubGT, StpCubKG, \r\n " +
                    " StpMeasSweeps, StpLabour, StpShftInfo, \r\n " +
                    " DevAdv, DevAdvOn, DevAdvOff, \r\n " +
                    " DevEH, DevEW, \r\n " +
                    " DevTons, DevTonsOn, DevTonsOff, \r\n " +                   
                    " DevCmgt, DevCmgtTot, DevGT, DevGTTot,  \f\n " +
                    " DevKG, \r\n " +
                    " DevCubics, DevCubTons, DevCubGT, DevCubKG, \r\n " +
                    " DevLabour, DevShftInfo, DevDrillRig) \r\n " +
                    " values ( \r\n " +
                    " '" + theUserID + "', '" + theSaveReport + "', \r\n " +
                    " '" + theDateType + "', " + thePMonth + ", \r\n " +
                    " " + theFPMonth + ", " + theTPMonth + ", \r\n " +
                    " '" + theCalenDate + "', '" + theFromDate + "', '" + theToDate + "', \r\n " +
                    " '" + theSecionID + "', '" + theHierID + "', '" + theName + "', \r\n " +
                    " '" + theLevelGM + "', '" + theLevelMN + "', '" + theLevelMNM + "', '" + theLevelMO + "', \r\n " +
                    " '" + theLevelSB + "', '" + theLevelMiner + "', '" + theLevelWP + "', \r\n " +
                    " '" + thePlanDyn + "', '" + thePlanLock + "', '" + thePlanDynProg + "', '" + thePlanLockProg + "', \r\n " +
                    " '" + theBookings + "', '" + theMeasurement + "', '" + thePlanBuss + "', \r\n " +
                    " '" + theForeCast + "', '" + theAbsVar + "',\r\n " +
                    " '" + theAuthPlanOnly + "', '" + theStopeLedge + "',\r\n " +
                    " '" + theStpSqm + "', '" + theStpSqmOn + "', '" + theStpSqmOff + "', \r\n " +
                    " '" + theStpSqmOS + "', '" + theStpSqmOSF + "', \r\n " +
                    " '" + theStpCmgt + "', '" + theStpCmgtTotal + "', '" + theStpGT + "', '" + theStpGTTotal + "', \r\n " +
                    " '" + theStpSW + "', '" + theStpSWIdeal + "', '" + theStpSWFault + "', \r\n " +
                    " '" + theStpCW + "', '" + theStpKG + "', \r\n " +
                    " '" + theStpFL + "', '" + theStpFLOn + "', '" + theStpFLOff + "', '" + theStpFLOS + "', \r\n " +
                    " '" + theStpAdv + "', '" + theStpAdvOn + "', '" + theStpAdvOff + "', \r\n " +
                    " '" + theStpTons + "', '" + theStpTonsOn + "', '" + theStpTonsOff + "', \r\n " +
                    " '" + theStpTonsOS + "', '" + theStpTonsFault + "', \r\n " +
                    " '" + theStpCubics + "', '" + theStpCubTons + "', '" + theStpCubGT + "', '" + theStpCubKG + "', \r\n " +
                    " '" + theStpMeasSweeps + "', '" + theStpLabour + "', '" + theStpShftInfo + "', \r\n " +

                    " '" + theDevAdv + "', '" + theDevAdvOn + "', '" + theDevAdvOff + "', \r\n " +
                    " '" + theDevEH + "', '" + theDevEW + "', \r\n " +
                    " '" + theDevTons + "', '" + theDevTonsOn + "', '" + theDevTonsOff + "', \r\n " +
                    " '" + theDevCmgt + "', '" + theDevCmgtTotal + "', '" + theDevGT + "', '" + theDevGTTotal + "', \r\n " +
                    " '" + theDevKG + "', \r\n " +
                    " '" + theDevCubics + "', '" + theDevCubTons + "', '" + theDevCubGT + "', '" + theDevCubKG + "', \r\n " +
                    " '" + theDevDrillRig + "', '" + theDevLabour + "', '" + theDevShftInfo + "' \r\n " +
                    " ) ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsGenericReportData", "saveReport", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                //return theData.ResultsDataTable;
            }
            //else
                //return theData.ResultsDataTable;
        }
    }
}
