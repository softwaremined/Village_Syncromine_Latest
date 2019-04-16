using System;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.Survey
{
    class clsSurvey : clsBase
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        public DataTable get_Sysset()
        {
            theData.SqlStatement = " select *, theRunDate = Convert(varchar(10), RunDate, 120) from Sysset ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Sections(string _prodmonth)
        {
            theData.SqlStatement =
                        " Select SectionID, Name \r\n " +
                        " from Section_Complete where Prodmonth = '" + _prodmonth + "' \r\n " +
                        " order by SectionID ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Sections", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Stoping(string _prodmonth, string _peername)
        {
            theData.SqlStatement = "[sp_Survey_STP_Load]";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.ResultsTableName = "STP_Survey";

            SqlParameter[] _paramCollection =
                        {
                    theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prodmonth),
                    theData.CreateParameter("@SectionID", SqlDbType.Text, 20, _peername),
                };

            theData.ParamCollection = _paramCollection;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Stoping", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Stoping_Detail(string _prodmonth, string _sectionid, string _workplaceid, string _seqno)
        {
            theData.SqlStatement = "[sp_Survey_STP_Load_Detail]";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.ResultsTableName = "STP_Survey_Detail";

            SqlParameter[] _paramCollection =
                {
                    theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prodmonth),
                    theData.CreateParameter("@SectionID", SqlDbType.Text, 20, _sectionid),
                    theData.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 12, _workplaceid),
                    theData.CreateParameter("@SeqNo", SqlDbType.Text, 7, _seqno),
                };

            theData.ParamCollection = _paramCollection;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Stoping_Detail", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Development(string _prodmonth, string _peername)
        {
            theData.SqlStatement = "[sp_Survey_DEV_Load]";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.ResultsTableName = "DEV_Survey";

            SqlParameter[] _paramCollection =
                {
                    theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prodmonth),
                    theData.CreateParameter("@SectionID", SqlDbType.Text, 20, _peername),
                };

            theData.ParamCollection = _paramCollection;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Development", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
       
        public DataTable get_Development_Detail(string _prodmonth, string _sectionid, string _workplaceid, string _seqno)
        {
            theData.SqlStatement = "[sp_Survey_DEV_Load_Detail]";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.ResultsTableName = "DEV_Survey_Detail";

            SqlParameter[] _paramCollection =
                {
                    theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prodmonth),
                    theData.CreateParameter("@SectionID", SqlDbType.Text, 20, _sectionid),
                    theData.CreateParameter("@WorkplaceID", SqlDbType.VarChar, 12, _workplaceid),
                    theData.CreateParameter("@SeqNo", SqlDbType.Text, 7, _seqno),
                };

            theData.ParamCollection = _paramCollection;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Development_Detail", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Contractors(string _prodmonth, string Contractor)
        {
            theData.SqlStatement =
                        "select SectionID_2 SectionID from Section_Complete " +
                        "where ProdMonth= '" + _prodmonth + "' and " +
                        "      SectionID = '" + Contractor + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ResultsTableName = "MOID";
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Contractors", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_MineMethods()
        {
            theData.SqlStatement =
                        "select MethodID, MethodDesc, MethodShortDesc from  [Code_MiningMethod] " +
                        "union " +
                        "select 0 MethodID, '' MethodDesc, '' MethodShortDesc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_MineMethods", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_StopeTypes()
        {
            theData.SqlStatement =
                        "select StopeTypeID, StopeTypeDesc from Code_StopeTypes ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_MineMethods", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Indicators()
        {
            theData.SqlStatement =
                        "select IndicatorID, IndicatorDesc from Code_Indicators " +
                        "union " +
                        "select 0 IndicatorID, '' IndicatorDesc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Indicators", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Crews(string _bookdate)
        {
            theData.SqlStatement =
                        "select GangNo GangNo from Crew " +
                        "   --where CalendarDate  = '" + _bookdate + "' " +
                        " union select '' GangNo ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_CrewsM", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable get_PegNumbers(string _workplaceid)
        {
            theData.SqlStatement =
                        "select PegID PegNo, Value PegValue \r\n " +
                        " from Peg where WorkplaceID = '" + _workplaceid + "' \r\n " +
                        " order by Value desc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_PegNumbers", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_CubicTypes(string _act)
        {
            if (_act == "Dev")
            {
                theData.SqlStatement =
                    "(select convert(varchar(3),CubicTypeID) CubicTypeID, CubicTypeDesc from Code_CubicTypes " +
                    " where Activity = 1) " +
                    " union " +
                    "(select '' CubicTypeID, '' CubicTypeDesc) ";
            }
            else
            {
                theData.SqlStatement =
                    "(select convert(varchar(3),CubicTypeID) CubicTypeID, CubicTypeDesc from Code_CubicTypes " +
                    " where Activity = 0) " +
                    " union " +
                    "(select '' CubicTypeID, '' CubicTypeDesc) ";
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_CubicTypes", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_CleanTypes(string _act)
        {
            if (_act == "Dev")
            {
                theData.SqlStatement =
                    "select convert(varchar(7),CleanTypeID) CleanTypeID, CleanTypeDesc from Code_CleanTypes " +
                    "where CleanTypeDesc in ('Haulage Vampings', 'X/C Vampings', 'T/W Vampings', 'B/H Vampings') " + // Added - Shaista Anjum Exclude these 4 items - 13/FEB/2013
                    "union all " +
                    "select '' CleanTypeID, '' CleanTypeDesc order by CleanTypeID";
            }
            else
            {
                theData.SqlStatement =
                    "select convert(varchar(7),CleanTypeID) CleanTypeID, CleanTypeDesc from Code_CleanTypes " +
                    "WHERE CleanTypeDesc NOT IN('Haulage Vampings', 'X/C Vampings', 'T/W Vampings', 'B/H Vampings') " + // Added - Shaista Anjum Exclude these 4 items - 13/FEB/2013
                    "union " +
                    "select '' CleanTypeID, '' CleanTypeDesc ";
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_CleanTypes", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Entries(string _prodmonth, string _contractor, string _workplaceid, string _act)
        {
            theData.SqlStatement =
                        "select SeqNo from Survey \r\n " +
                        "where ProdMonth = '" + _prodmonth + "'and  \r\n " +
                        "      SECTIONID = '" + _contractor + "' and \r\n " +
                        "      WorkplaceID = '" + _workplaceid + "' and \r\n ";
            if (_act == "Dev")
                theData.SqlStatement = theData.SqlStatement + " Activity = 1 \r\n ";
            else
                theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) \r\n ";
            theData.SqlStatement = theData.SqlStatement + " order by SeqNo ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Entries", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Density(string _prodmonth, string _sectionid, string _workplaceid, string _act)
        {
            theData.SqlStatement =
                        " select '' TheType, 'Insitu: '+convert(varchar,convert(decimal(7,2),Density)) RockDensity, \r\n " +
                        "        Density from ( \r\n " +
                        " select top(1) Density , '' TheType from Survey \r\n " +
                        " where ProdMonth = '" + _prodmonth + "' and \r\n " +
                        "      SECTIONID = '" + _sectionid + "' and \r\n " +
                        "      WorkplaceID = '" + _workplaceid + "' \r\n ";
            if (_act == "Dev")
                theData.SqlStatement = theData.SqlStatement + " and Activity = 1) a \r\n ";
            else
                theData.SqlStatement = theData.SqlStatement + " and Activity in (0,9)) a \r\n ";
            theData.SqlStatement = theData.SqlStatement + 
                        " union all \r\n " +
                        " select 'RD' TheType, \r\n " +
                        "   'Insitu: '+Convert(varchar,convert(decimal(7,2),Density)) RockDensity, \r\n " +
                        " Density Density \r\n " +
                        " from Workplace \r\n " + // 
                        " where WorkplaceID = '" + _workplaceid + "' \r\n " +
                        " union \r\n " +
                        " select 'BRD' TheType, \r\n " +
                        " 'BrokenRockDensity: '+Convert(varchar,convert(decimal(7,2), BrokenRockDensity)) RockDensity, \r\n " +
                        " BrokenRockDensity Density \r\n " +
                        " from Sysset ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Density", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Destinations()
        {
            theData.SqlStatement =
                        "select '1', 'Packed' OreflowID,'Packed' Name \r\n " +
                        " union \r\n " +
                        " select '2', 'Dumped' OreflowID,'Dumped' Name  \r\n " +
                        " union \r\n " +
                        " select '3',OreflowID, Name from OREFLOWENTITIES where OreFlowCode='Mill' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_Destinations", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_CheckLockedStatus(string _prodmonth)
        {
            theData.SqlStatement =
                        "select Locked from Survey_Locks where ProdMonth = '" + _prodmonth + "'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_CheckLockedStatus", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        
        public DataTable get_MaxSeqNo(string _prodmonth, string _contractor, string _workplaceid, string _act)
        {
            theData.SqlStatement =
                        "select isnull(max(SeqNo),0) SeqNo from Survey " +
                        "where ProdMonth = '" + _prodmonth + "'and  " +
                        "      SECTIONID = '" + _contractor + "' and " +
                        "      WorkplaceID = '" + _workplaceid + "' and ";
            if (_act == "Dev")
                theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            else
                theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_MaxSeqNo", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_EndTypes(string _workplaceid)
        {
            theData.SqlStatement =
                        "select EndHeight = convert(numeric(7,2), e.EndHeight), \r\n " +
                        " EndWidth = convert(numeric(7,2), e.EndWidth), " +
                        " w.ReefWaste ReefWaste from Workplace w " +
                        " inner join Endtype e on " +
                        "   e.EndTypeID = w.EndTypeID " +
                        " where w.WorkplaceID = '" + _workplaceid + "' and " +
                        "      w.Activity = 1 ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_EndTypes", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_PlanCrews(string _prodmonth, string _sectionid, string _workplaceid, string _act)
        {
            theData.SqlStatement =
                        "select  OrgUnitDay, OrgUnitAfternoon, OrgUnitNight " +
                        " from PlanMonth " +
                        " where ProdMonth = '" + _prodmonth + "'and  " +
                        "      SECTIONID = '" + _sectionid + "' and " +
                        "      WorkplaceID = '" + _workplaceid + "' and \r\n " +
                        "      PlanCode='MP' and IsCubics = 'N' and ";
            if (_act == "Dev")
                theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            else
                theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            //theData.SqlStatement = theData.SqlStatement + " order by CalendarDate desc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_PlanCrews", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_SurveyVisibles()
        {
            theData.SqlStatement =
                        " select * from Survey_Fields ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_PlanDates", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable get_PlanDates(string _prodmonth, string _sectionid, string _workplaceid)
        {
            theData.SqlStatement =
                        " select MAX(CalendarDate) AS CD \r\n " +
                        " from Planning \r\n " +
                        " where ProdMonth = '" + _prodmonth + "' and \r\n " +
                        "       SECTIONID = '" + _sectionid + "' AND \r\n " +
                        "       WorkplaceID = '" + _workplaceid + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_PlanDates", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable find_Peg(string _wpid, string _pegid)
        {
            theData.SqlStatement =
                "select Top 1 Value PegValue from Peg " +
                " where WorkplaceID = '" + _wpid + "' and " +
                " PegID = '" + _pegid + "' order by PegID Desc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "find_Peg", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable find_Survey(string _prodmonth, string _wpid)
        {
            theData.SqlStatement =
                    " select Top 1 max(prodmonth) prodmonth,  max(SeqNo) max, isnull(ProgTo,0.0) ProgTo from Survey " +
                    " where Workplaceid = '" + _wpid + "' and " +
                    " ProdMonth <= '" + _prodmonth + "' " +
                    " group by ProgTo order by prodmonth desc";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "find_Survey", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable find_Workplace(string _wpid)
        {
            theData.SqlStatement =
                    "select EndTypeID from Workplace " +
                    "where WorkplaceID = '" + _wpid + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "find_Workplace", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public bool delete_Survey(string _prodmonth, string _contractor, string _workplaceid, int _seqno, string _act)
        {
            theData.SqlStatement =
                        "delete from Survey " +
                        "where ProdMonth = '" + _prodmonth + "'and  " +
                        "      SECTIONID = '" + _contractor + "' and " +
                        "      WorkplaceID = '" + _workplaceid + "' and " +
                        "      SeqNo = " + _seqno + " ";
            if (_act == "Dev")
                theData.SqlStatement = theData.SqlStatement + " and Activity = 1 ";
            else
                theData.SqlStatement = theData.SqlStatement + " and Activity in (0,9) ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "delete_Survey", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public DataTable find_TheWP(string _prodmonth, string _contractor, string _workplaceid, string _seqno, string _act)
        {
            theData.SqlStatement =
                    "select * from Survey " +
                    "where ProdMonth = '" + _prodmonth + "'and  " +
                    "      SECTIONID = '" + _contractor + "' and " +
                    "      WorkplaceID = '" + _workplaceid + "' and " +
                    "      SeqNo = '" + _seqno + "' and ";
            if (_act == "Dev")
                theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            else
                theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "getfind_Workplace", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_WPSearch(string _prodmonth, string _sectionid, string _act)
        {
            if (_act == "0")
            { 
                theData.SqlStatement =
                        " Select w.WorkplaceID WorkplaceID, w.Description Workplace, \r\n " +
                        "        w.ReefWaste RW, Density Dens, 0 EH, 0 EW, \r\n " +
                        "        isnull(k.StopeWidth,0) SW, isnull(k.ChannelWidth,0) CW, " +
                        "        isnull(k.CMGT,0) cmgt \r\n " +
                        " from Workplace w \r\n " +
                        " left outer join \r\n " +
                        "       (select k.* from Kriging k \r\n " +
                        "          inner join \r\n " +
                        "                 (select prodmonth,workplaceid, activity, max(weekno) weekno \r\n " +
                        "                  from kriging \r\n " +
                        "                  group by prodmonth, workplaceid, activity \r\n " +
                        "                 ) kr on \r\n " +
                        "                   k.prodmonth = kr.prodmonth and \r\n " +
                        "                   k.workplaceid = kr.workplaceid and \r\n " +
                        "                   k.activity = kr.activity and \r\n " +
                        "                   k.weekno = kr.weekno \r\n " +
                        "       ) k on \r\n " +
                        "       k.ProdMonth = '" + _prodmonth + "' and \r\n " +
                        "       k.WorkplaceID = w.WorkplaceID and \r\n " +
                        "       k.Activity = w.Activity \r\n " +
                        " where \r\n " +
                        "         w.workplaceid not in \r\n " +
                        "            (Select WorkplaceID from PlanMonth \r\n " +
                        "             where SectionID = '" + _sectionid + "' and \r\n " +
                        "                   Prodmonth = '" + _prodmonth + "' and plancode = 'MP') ";
            }
            else
            {
                theData.SqlStatement = 
                        " Select w.WorkplaceID WorkplaceID, w.Description Workplace, \r\n " +
                        "        w.ReefWaste RW, Density Dens, 0 SW, 0 CW, \r\n " +
                        "        isnull(k.EndWidth,0) EW, isnull(k.EndHeight,0) EH, \r\n " +
                        "        isnull(k.CMGT,0) cmgt \r\n " +
                        " from Workplace w \r\n " +
                        " left outer join \r\n " +
                        "   (select k.* from Kriging k \r\n " +
                        "    inner join \r\n " +
                        "       (select prodmonth,workplaceid, activity, max(weekno) weekno \r\n " +
                        "        from kriging \r\n " +
                        "        group by prodmonth, workplaceid, activity \r\n " +
                        "       ) kr on \r\n " +
                        "       k.prodmonth = kr.prodmonth and \r\n " +
                        "       k.workplaceid = kr.workplaceid and \r\n " +
                        "       k.activity = kr.activity and \r\n " +
                        "       k.weekno = kr.weekno \r\n " +
                        "   ) k on \r\n " +
                        "   k.ProdMonth = '" + _prodmonth + "' and \r\n " +
                        "   k.WorkplaceID = w.WorkplaceID and \r\n " +
                        "   k.Activity = w.Activity \r\n " +
                        " where  \r\n " +
                        "       w.workplaceid not in \r\n " +
                        "       (Select WorkplaceID from PlanMonth \r\n " +
                        "        where SectionID = '" + _sectionid + "' and \r\n " +
                        "              Prodmonth = '" + _prodmonth + "' and plancode = 'MP') ";
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_WPSearch", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable Save_WPAdd(string _prodmonth, string _sectionid, string _workplaceid, string _activity)
        {
            theData.SqlStatement = "[sp_Insert_Zeros_PlanMonth]";
            theData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            theData.ResultsTableName = "STP_Survey_Plan";

            SqlParameter[] _paramCollection =
                        {
                    theData.CreateParameter("@ProdMonth", SqlDbType.VarChar, 6, _prodmonth),
                    theData.CreateParameter("@SectionID", SqlDbType.Text, 20, _sectionid),
                    theData.CreateParameter("@WorkplaceID", SqlDbType.Text, 12, _workplaceid),
                    theData.CreateParameter("@Activity", SqlDbType.Text, 1, _activity),
                };

            theData.ParamCollection = _paramCollection;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_WPAdd", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }


        public DataTable getActivty( string _workplaceid)
        {
            theData.SqlStatement =
                    " select activity from workplace \r\n " +
                    " where  \r\n " +
                    "       WorkplaceID = '" + _workplaceid + "' \r\n " +
                    "       \r\n ";
         
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "find_Workplace", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable find_Survey(string _prodmonth, string _sectionid, string _workplaceid, string _act)
        {
            theData.SqlStatement =
                    " select count(*) cntSurbvey from Survey \r\n " +
                    " where Prodmonth = '" + _prodmonth + "' and \r\n " +
                    "       WorkplaceID = '" + _workplaceid + "' and \r\n " +
                    "       SectionID = '" + _sectionid + "' and \r\n ";
            if (_act == "Stp")
                theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            else
                theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "find_Workplace", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable find_Plan(string _prodmonth, string _sectionid, string _workplaceid, string _act)
        {
            theData.SqlStatement =
                    " select SurveyAdded from PlanMonth \r\n " +
                    " where Prodmonth = '" + _prodmonth + "' and \r\n " +
                    "       WorkplaceID = '" + _workplaceid + "' and \r\n " +
                    "       SectionID = '" + _sectionid + "' and \r\n ";
            if (_act == "Stp")
                theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            else
                theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            //if (_act != "0")
            //{
            //    theData.SqlStatement =
            //            " select sum(Cubics) Cubics, sum(SQM) Sqm, sum(0) Adv from PlanMonth \r\n " +
            //            " where Prodmonth = '" + _prodmonth + "' and \r\n " +
            //            "       WorkplaceID = '" + _workplaceid + "' and \r\n " +
            //            "       SectionID = '" + _sectionid + "' and \r\n ";
            //    if (_act == "Stp")
            //        theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            //    else
            //        theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            //}
            //else
            //{
            //    theData.SqlStatement =
            //            " select sum(0) Cubics, sum(0) Sqm, sum(Adv) Adv from PlanMonth \r\n " +
            //            " where Prodmonth = '" + _prodmonth + "' and \r\n " +
            //            "       WorkplaceID = '" + _workplaceid + "' and \r\n " +
            //            "       SectionID = '" + _sectionid + "' and \r\n ";
            //    if (_act == "Stp")
            //        theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            //    else
            //        theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            //    //"       Startdate = PlanEndDate", ";
            //}
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "getfind_Workplace", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public bool delete_WP(string _prodmonth, string _sectionid, string _workplaceid, string _act)
        {
            theData.SqlStatement =
                        " delete from Planning \r\n " +
                        " where ProdMonth = '" + _prodmonth + "'and \r\n " +
                        "      SECTIONID = '" + _sectionid + "' and \r\n " +
                        "      WorkplaceID = '" + _workplaceid + "' and \r\n ";
            if (_act == "Stp")
                theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            else
                theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            theData.SqlStatement = theData.SqlStatement +
                        "delete from PlanMonth \r\n " +
                        "where ProdMonth = '" + _prodmonth + "' and \r\n " +
                        "      SECTIONID = '" + _sectionid + "' and \r\n " +
                        "      WorkplaceID = '" + _workplaceid + "' and \r\n ";
            if (_act == "Stp")
                theData.SqlStatement = theData.SqlStatement + " Activity in (0,9) ";
            else
                theData.SqlStatement = theData.SqlStatement + " Activity = 1 ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "delete_Survey", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
    }
}
