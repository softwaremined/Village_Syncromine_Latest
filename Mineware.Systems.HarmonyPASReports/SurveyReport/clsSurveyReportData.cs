using System;
using System.Data;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.SurveyReport

{
    class clsSurveyReportData
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
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
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportData", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Sections(string _frommonth, string _tomonth, string _hierid)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " Select distinct(SectionID) SectionID, Name SectionName,  Hierarchicalid Hier \r\n " +
                " from Section s \r\n " +
                " where s.Prodmonth >= '" + _frommonth + "' and \r\n " +
                "       s.ProdMonth <= '" + _tomonth + "' and \r\n " +
                "    Hierarchicalid  <= '"+ _hierid+"' \r\n " +
                " order by s.HierarchicalID, s.SectionID ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportData", "get_Sections", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Hier(string _sectionid, string _frommonth)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                "select * from section " +
                " where sectionID = '" + _sectionid + "' and " +
                "       Prodmonth = '" + _frommonth + "' ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportData", "get_Hier", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
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
        public DataTable get_Reefs()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " Select ReefID, Description ReefDesc, Checked  = 'true' \r\n " +
                " from Reef \r\n " +
                " where Selected = '1' " +
                " order by Description ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportData", "get_Reefs", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Shafts()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " Select OreflowID, Name ShaftDesc \r\n " +
                " from OreflowEntities \r\n " +
                " where OreflowCode = 'Shaft' " +
                " order by Name ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportData", "get_Shafts", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_CleanTypes()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " Select CleanTypeID, CleanTypeDesc \r\n " +
                " from Code_CleanTypes \r\n " +
                " where SurvRep = 'Y' " +
                " order by CleanTypeDesc ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportData", "get_CleanTypes", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_SignOffs()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement =
                " Select OrderBy, Description SignDesc \r\n " +
                " from Code_SignOffs \r\n " +
                " where Name = 'Survey' " +
                " order by OrderBy ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsSurveyReportData", "get_SignOffs", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
    }
}
