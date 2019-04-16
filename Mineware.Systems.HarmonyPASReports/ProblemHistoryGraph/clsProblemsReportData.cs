
using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Reports.ProblemHistoryGraph
{
    class clsProblemsReportData
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
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsProblemsFeport", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Hier(int _mohier)
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = "   select '0' HierID, 'Total Mine' HierDesc  union  Select SectionID HierID, Name HierDesc from Section \r\n " +
                " where Prodmonth = '"+ _mohier + "' and Hierarchicalid = 4 ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsProblemsFeport", "get_Sysset", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Activity()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " SELECT 'Stoping' Activity union SELECT 'Development' Activity union SELECT 'Vamping' Activity";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsProblemsFeport", "get_ReportType", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_ProblemType()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = "SELECT 'Business Units' TheProblemType union Select 'Shaft Managers' TheProblemType";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsProblemsFeport", "get_ProblemType", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_Zeroes()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " exec sp_ProblemsReport_Zeroes";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsProblemsFeport", "get_Zeroes", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
        public DataTable get_ZeroesPie()
        {
            theData.ConnectionString = connectionString;
            theData.SqlStatement = " exec sp_ProblemsReport_ZeroesPie ";
            theData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            theData.queryReturnType = MWDataManager.ReturnType.DataTable;
            theData.ExecuteInstruction();
            clsDataResult errorMsg = theData.ExecuteInstruction();
            if (errorMsg.success == false)
            {
                _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", DBTag, "clsProblemsFeport", "get_ZeroesPie", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theData.ResultsDataTable;
            }
            else
                return theData.ResultsDataTable;
        }
    }

}
