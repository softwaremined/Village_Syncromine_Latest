using System;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.BusinessPlanImport
{
    class clsBusinessPlanImport : clsBase
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        public DataTable save_DeleteTempBussPlan()
        {
            theData.SqlStatement =
                    " delete from tempBusPlanImport ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "get_DeleteBussPlan", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable save_DeleteBussPlan(int _activity, string _startmonth, string _endmonth)
        {
            if (_activity == 0)
            {
                theData.SqlStatement =
                    " delete from BusinessPlan_Stoping \r\n " +
                    " where Prodmonth >= '" + _startmonth + "' and Prodmonth <= '" + _endmonth + "' ";
            }
            else
            {
                theData.SqlStatement =
                    " delete from BusinessPlan_Development \r\n " +
                    " where Prodmonth >= '" + _startmonth + "' and Prodmonth <= '" + _endmonth + "' ";
            }
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "get_DeleteBussPlan", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable get_Section(string _prodmonth, string _sectionid)
        {
            theData.SqlStatement =
                    " select distinct s.SectionID from section s, sysset ss \r\n " +
                    " where s.SectionID ='" + _sectionid + "' and \r\n " +
                    "       s.PRODMONTH='" + _prodmonth + "' and \r\n " +
                    "       s.HierarchicalID = ss.MOHierarchicalID ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "find_Section", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_SectionInTempBuss(string _prodmonth, string _sectionid)
        {
            theData.SqlStatement =
                    " select * from tempBusPlanImport \r\n " +
                    " where SectionID ='" + _sectionid + "' and \r\n " +
                    "       PRODMONTH='" + _prodmonth + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "find_SectionInTempBuss", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable save_SectionInTempBuss(string _prodmonth, string _sectionid)
        {
            theData.SqlStatement =
                    " insert into tempBusPlanImport \r\n " +
                    " (SectionID, ProdMonth) values ( \r\n " +
                    "  '" + _sectionid + "', '" + _prodmonth + "') ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "save_SectionInTempBuss", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Workplace(string _workplaceid)
        {
            theData.SqlStatement =
                    " select * from Workplace \r\n " +
                    " where WorkplaceID ='" + _workplaceid + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "find_Workplace", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_WorkplaceInTempBuss(string _workplaceid)
        {
            theData.SqlStatement =
                    " select * from tempBusPlanImport \r\n " +
                    " where WorkplaceID ='" + _workplaceid + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "find_WorkplaceInTempBuss", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable save_WorkplaceInTempBuss(string _prodmonth, string _section, string _workplaceid)
        {
            theData.SqlStatement =
                    " insert into tempBusPlanImport \r\n " +
                    " (SectionID, ProdMonth, WorkplaceID ) values ( \r\n " +
                    "  '" + _section + "', '" + _prodmonth + "', '" + _workplaceid + "') ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "save_WorkplaceInTempBuss", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_RecordInTempBuss(string _prodmonth, string _section, string _workplace)
        {
            theData.SqlStatement =
                    " select * from tempBusPlanImport \r\n " +
                    " where SectionID = '" + _section + "' and \r\n " +
                    "       PRODMONTH='" + _prodmonth + "' and \r\n " +
                    "       WorkplaceID ='" + _workplace + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "save_WorkplaceInTempBuss", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }

        public DataTable get_BussPlanLock(int _year, int _act)
        {
            theData.SqlStatement = 
                    " select * from BusinessPlan_Locks \r\n " +
                    " where Year = '"+ _year + "' and Activity = '" + _act + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "get_BussPlan", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_OpsPlan(string _prodmonth, string _sectionid)
        {
            theData.SqlStatement =
                    " select distinct OpsPlanLink from section \r\n " +
                    " where OpsPlanLink = '" + _sectionid.Replace("\"", "") + "' AND \r\n " +
                    "       PRODMONTH = '" + _prodmonth + "'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "get_OpsPlan", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_OpsPlan(string _sectionid)
        {
            theData.SqlStatement =
                    " select distinct OpsPlanLink from section \r\n " +
                    " where OpsPlanLink = '" + _sectionid.Replace("\"", "") + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "get_OpsPlan", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable save_DeleteOpsPlan(string _bmonth, string _emonth)
        {
            theData.SqlStatement =
                " delete from TM1Import \r\n " +
                " where fiscal_period >= '" + _bmonth + "' and \r\n " +
                "      fiscal_period <= '" + _emonth + "' and source_ind = 'Production'";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "get_OpsPlan", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public bool save_BusPlan(string _version, string _comp, string _account, string _projno,
                                 string _projtask, string _resp, string _occ, string _period,
                                 string _source, string _budvaltype, string _budval, string _allbudval)
        {
            theData.SqlStatement =
                " INSERT INTO TM1Import \r\n " +
                " (VERSION_DSC ,COMPANY_NAME, ACCOUNT_NO, \r\n " +
                "  PROJECT_NO, PROJECT_TASK, RESP_CENTER, \r\n " +
                "  OCCUPATION_CD, FISCAL_PERIOD, SOURCE_IND, \r\n " +
                "  BUD_VAL_TYPE, BUD_VAL, POST_ALLOC_BUD_VAL) \r\n " +
                "  VALUES \r\n " +
                " ('" + _version + "','" + _comp + "','" + _account + "', \r\n " +
                "  '" + _projno + "','" + _projtask + "' ,'" + _resp + "', \r\n " +
                "  '" + _occ + "', '" + _period + "','" + _source + "', \r\n " +
                "  '" + _budvaltype + "','" + _budval + "','" + _allbudval + "') ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "save_BusPlan", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public bool save_Stoping(string strWorkplaceID, string strSectionID, string strProdMonth,
                                 string stpReefSqmLedge, string stpOSSqmLedge, string stpOSFSqmLedge,
                                 string stpReefSqmStope, string stpOSSqmStope, string stpOSFSqmStope,
                                 string stpSqmWasteLedge, string stpSqmWasteStope,
                                 string stpSqmWaste, string stpSqmLedge, string stpSqmStope, string stpSqm,
                                 string stpSWFault, string stpSW,
                                 string stpFLReef, string stpFLOS, string stpFL,
                                 string stpDens, string stpCmgt, string stpCW,
                                 string stpCubics,
                                 string stpReefTonsLedge, string stpOSTonsLedge, string stpOSFTonsLedge,
                                 string stpReefTonsStope, string stpOSTonsStope, string stpOSFTonsStope,
                                 string stpWasteTonsStope, string stpWasteTonsLedge,
                                 string stpTonsOS, string stpTonsOSF,
                                 string stpTonsReef, string stpTonsWaste,
                                 string stpTonsLedge, string stpTonsStope, string stpTons,
                                 string stpContent, string stpContentLedge, string stpContentStope)
        {
            theData.SqlStatement =
                " insert into BusinessPlan_Stoping \r\n " +
                " (WorkplaceID, SectionID, Prodmonth, \r\n " +
                "  SqmReeflLedge, SqmOSLedge, SqmOSFLedge, \r\n " +
                "  SqmReefStope, SqmOSStope, SqmOSFStope,  \r\n " +
                "  SqmFaultStope, SqmFaultLedge, \r\n " +
                "  SQMWasteLedge, SQMWasteStope, \r\n " +
                "  SQMWaste, SQMLedge, SQMStope, SQM, \r\n " +
                "  SWFault, SW, \r\n " +
                "  FLReef, FLOS, FL, \r\n " +
                "  Density, Cmgt, CW, Cubics, ImportSourceID, \r\n " +
                "  TonsReefLedge, TonsOSLedge, TonsOSFLedge, \r\n " +
                "  TonsReefStope, TonsOSStope, TonsOSFStope, \r\n " +
                "  TonsWasteStope, TonsWasteLedge, \r\n " +
                "  TonsFaultLedge, TonsFaultStope, TonsFault, \r\n " +
                "  TonsOS, TonsOSF, \r\n " +
                "  TonsReef, TonsWaste, TonsLedge, TonsStope, Tons, \r\n " +
                "  Content, ContentLedge, ContentStope) \r\n " +
                " values ( \r\n " +
                "  '" + strWorkplaceID + "', '" + strSectionID + "', '" + strProdMonth + "', \r\n " +
                "  '" + stpReefSqmLedge + "', '" + stpOSSqmLedge + "', '" + stpOSFSqmLedge + "', \r\n " +
                "  '" + stpReefSqmStope + "', '" + stpOSSqmStope + "', '" + stpOSFSqmStope + "', \r\n " +
                "  0, 0, \r\n " +
                "  '" + stpSqmWasteLedge + "', '" + stpSqmWasteStope + "', \r\n " +
                "  '" + stpSqmWaste + "', '" + stpSqmLedge + "', '" + stpSqmStope + "', '" + stpSqm + "',\r\n " +
                "  '" + stpSWFault + "', '" + stpSW + "', \r\n " +
                "  '" + stpFLReef + "', '" + stpFLOS + "', '" + stpFL + "', \r\n " +
                "  '" + stpDens + "', '" + stpCmgt + "', '" + stpCW + "', \r\n " +
                "  '" + stpCubics + "', 1, \r\n " +
                "  '" + stpReefTonsLedge + "', '" + stpOSTonsLedge + "', '" + stpOSFTonsLedge + "', \r\n " +
                "  '" + stpReefTonsStope + "', '" + stpOSTonsStope + "', '" + stpOSFTonsStope + "', \r\n " +
                "  '" + stpWasteTonsStope + "', '" + stpWasteTonsLedge + "', \r\n " +
                "  0,0,0, \r\n " +
                "  '" + stpTonsOS + "', '" + stpTonsOSF + "', \r\n " +
                "  '" + stpTonsReef + "', '" + stpTonsWaste + "', \r\n " +
                "  '" + stpTonsLedge + "', '" + stpTonsStope + "', '" + stpTons + "', \r\n " +
                "  '" + stpContent + "', '" + stpContentLedge + "', '" + stpContentStope + "') ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "save_Stoping", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public bool save_Development(string strWorkplaceID, string strSectionID, string strProdMonth,
                                 string devAdvReef, string devAdvWaste, string devHeight, string devWidth,
                                 string devDens, string devCmgt, string devCubics,
                                 string devIndicator, string devMinigMethod,
                                 string devAdv, string devContent,
                                 string devTons, string devTonsReef, string devTonsWaste)
        {
            theData.SqlStatement =
                " insert into BusinessPlan_Development \r\n " +
                " (WorkplaceID, SectionID, Prodmonth, \r\n " +
                "  MAdvReef, MAdvWaste, Height, Width, \r\n " +
                "  Density, Cmgt, Cubics, \r\n " +
                "  IndicatorID, MiningMethodID, ImportSourceID, \r\n " +
                "  MAdv, Content, \r\n " +
                "  Tons, TonsReef ,TonsWaste) values ( \r\n " +
                "  '" + strWorkplaceID + "', '" + strSectionID + "', '" + strProdMonth + "', \r\n " +
                "  '" + devAdvReef + "', '" + devAdvWaste + "', '" + devHeight + "', '" + devWidth + "', \r\n " +
                "  '" + devDens + "', '" + devCmgt + "', '" + devCubics + "', \r\n " +
                "  '" + devIndicator + "', '" + devMinigMethod + "', 1, \r\n " +
                "  '" + devAdv + "', '" + devContent + "', \r\n " +
                "  '" + devTons + "', '" + devTonsReef + "', '" + devTonsWaste + "' \r\n " +
                ") ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "save_Development", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
        public bool save_BusPlanLocks(string _year, string _activity, string _startmonth, string _endmonth)
        {
            theData.SqlStatement =
                " delete from BusinessPlan_Locks \r\n " +
                " where Year = '" + _year + "' \r\n " +
                " and Activity = '" + _activity + "' \r\n " +
                " INSERT INTO BusinessPlan_Locks \r\n " +
                "           ([Year] \r\n " +
                "           ,[ProdMonthStart] \r\n " +
                "           ,[ProdMonthEnd] \r\n " +
                "           ,[IsLocked] \r\n " +
                "           ,[Activity]) \r\n " +
                "     VALUES \r\n " +
                "           ('" + _year + "' \r\n " +
                "           ,'" + _startmonth + "' \r\n " +
                "           ,'" + _endmonth + "'\r\n " +
                "           ,'false' \r\n " +
                "           ,'" + _activity + "') ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.longNumber;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsBusinessPlanImport", "save_BusPlanLocks", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return false;
            }
            else
                return true;
        }
    }
}
