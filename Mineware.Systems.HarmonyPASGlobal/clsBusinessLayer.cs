using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace CPMBusinessLayer
{
    public enum ReturnType
    {
        SQLDataReader,
        DataTable,
        longNumber,
        DataSet
    }

    public class clsBusinessLayer : IDisposable
    {

        private DataTable _resultsDataTable;
        public DataTable ResultsDataTable { get { return _resultsDataTable; } }
        private SqlDataReader _resultsDataReader;
        public SqlDataReader ResultsDataReader { get { return _resultsDataReader; } }
        private TUserCurrentInfo _TUserCurrentInfo;
        private string _systemDBTag;

          DataTable d1 = new DataTable();
          DataTable d2 = new DataTable();
          DataTable d3 = new DataTable();
          DataTable d4 = new DataTable();
          DataTable d5 = new DataTable();
          DataTable d6 = new DataTable();
          DataTable d7 = new DataTable();

        private MWDataManager.clsDataAccess Bus_Logic = new MWDataManager.clsDataAccess();
        private MWDataManager.clsDataAccess Bus_Logic2 = new MWDataManager.clsDataAccess();

        public ReturnType _queryReturnType;
        public ReturnType queryReturnType { get { return _queryReturnType; } set { _queryReturnType = value; } }
        public TUserCurrentInfo SetUserCurrentInfo { set { _TUserCurrentInfo = value; } }
        public string SetsystemDBTag { set { _systemDBTag = value; } }

        public DataTable GetUserProfiles()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "select * from Syncro_UserProfiles";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public void Dispose()
        {
            if (_resultsDataTable != null)
            {
                _resultsDataTable.Dispose();
                _resultsDataTable = null;
            }
            if (_resultsDataReader != null)
            {
                _resultsDataReader.Dispose();
                _resultsDataReader = null;
            }
            if (Bus_Logic != null)
            {
                Bus_Logic.Dispose();
                Bus_Logic = null;
            }
        }

        public bool isClokingImportActive()
        {
            bool theResult = false;

            StringBuilder theSQL = new StringBuilder();

            MWDataManager.clsDataAccess _isClokingImportActive = new MWDataManager.clsDataAccess();
            _isClokingImportActive.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            _isClokingImportActive.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _isClokingImportActive.queryReturnType = MWDataManager.ReturnType.DataTable;
            theSQL.Append("SELECT [IMPORTING_CLOCKING_DATA]  FROM [SYSSET] ");

            _isClokingImportActive.SqlStatement = theSQL.ToString();
            _isClokingImportActive.ExecuteInstruction();
            foreach (DataRow r in _isClokingImportActive.ResultsDataTable.Rows)
            {
                theResult = Convert.ToBoolean( r["IMPORTING_CLOCKING_DATA"].ToString());
            }





            return theResult;
        }

        public DataTable getMiningMethods(int theActivity)
        {
            MWDataManager.clsDataAccess _ActivityList = new MWDataManager.clsDataAccess();
            _ActivityList.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            _ActivityList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _ActivityList.queryReturnType = MWDataManager.ReturnType.DataTable;
            string activityString = "";
            switch (theActivity)
            {
                case 0:
                    activityString = "(0,3)";
                    break;
                case 1:
                    activityString = "(1)";
                    break;
                case 2:
                    activityString = "(2)";
                    break;
                case 8:
                    activityString = "(8)";
                    break;
            }
            _ActivityList.SqlStatement = "SELECT 1 theID, -1 TargetID,'' Description UNION " +
                                         "select 2 theID,TargetID, Description from Code_Methods " +
                                         "where Activity in " + activityString +
                                         "ORDER BY theID";
            _ActivityList.ExecuteInstruction();

            return _ActivityList.ResultsDataTable;
        }

        public DataTable getSundryMiningActivity(int theActivity)
        {
            MWDataManager.clsDataAccess _ActivityList = new MWDataManager.clsDataAccess();
            _ActivityList.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _ActivityList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _ActivityList.queryReturnType = MWDataManager.ReturnType.DataTable;
            string activityString = "";
            //switch (theActivity)
            //{
            //    case 0:
            //        activityString = "(0,3)";
            //        break;
            //    case 1:
            //        activityString = "(1)";
            //        break;
            //    case 2:
            //        activityString = "(2)";
            //        break;
            //}
            _ActivityList.SqlStatement = "select st.UnitBase,st.SMID,su.Description UnitType  ,st.SMDescription    from SUNDRYMINING_TYPE st inner join SUNDRYMINING_UNIT su on " +
                                         "su.UnitBase =st.UnitBase ";
            _ActivityList.ExecuteInstruction();

            return _ActivityList.ResultsDataTable;
        }

        public DataTable getSweepsVampsActivity(int theActivity)
        {
            MWDataManager.clsDataAccess _ActivityList = new MWDataManager.clsDataAccess();
            _ActivityList.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _ActivityList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _ActivityList.queryReturnType = MWDataManager.ReturnType.DataTable;
            string activityString = "";
            //switch (theActivity)
            //{
            //    case 0:
            //        activityString = "(0,3)";
            //        break;
            //    case 1:
            //        activityString = "(1)";
            //        break;
            //    case 2:
            //        activityString = "(2)";
            //        break;
            //}
            _ActivityList.SqlStatement = "select st.UnitBase,st.OGID,su.Description UnitType  ,convert(varchar(2),st.OGID)+':'+st.OGDescription   Activity   from OLDGOLD_TYPE st inner join OLDGOLD_UNIT su on " +
                                         "su.UnitBase =st.UnitBase ";
            _ActivityList.ExecuteInstruction();

            return _ActivityList.ResultsDataTable;
        }

        public DataTable getDepthRange(int theActivity)
        {
            MWDataManager.clsDataAccess _ActivityList = new MWDataManager.clsDataAccess();
            _ActivityList.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _ActivityList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _ActivityList.queryReturnType = MWDataManager.ReturnType.DataTable;
            string activityString = "";
            //switch (theActivity)
            //{
            //    case 0:
            //        activityString = "(0,3)";
            //        break;
            //    case 1:
            //        activityString = "(1)";
            //        break;
            //    case 2:
            //        activityString = "(2)";
            //        break;
            //}
            _ActivityList.SqlStatement = "select concat( cm_Greater ,'cm', '-'  , cm_Less,'cm') AS DepthRange  from OLDGOLD_Depth";
            _ActivityList.ExecuteInstruction();

            return _ActivityList.ResultsDataTable;
        }

        public bool LoadAllLevel(string Thelevel)
        {
            bool _executionResult = false;
            //int _executionResult;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }

                if (Thelevel == null)
                {
                    Thelevel = "0";
                }
                Bus_Logic.SqlStatement = "Select HierarchicalID,Description from hierarch \r\n " +
                        " where hierarchicalType = 'Pro' and \r\n " +
                        "       HierarchicalID > " + Thelevel + " and \r\n " +
                        "       HierarchicalID <= " + TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID + " +2 " +
                        " union all \r\n " +
                        " select HierarchicalID = " + TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID + " +3 ," +
                            " 'Crew' Description " +
                         " union all \r\n " +
                        " select HierarchicalID = " + TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID + " +4 ," +
                            " 'Workplace' Description ";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;

            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }


            return _executionResult;
        }

        public DataTable GetReportType()
        {
                MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
                _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _WPData.SqlStatement = "SELECT 'Stoping' TheType union SELECT 'Development' TheType";
                _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;        
                _WPData.ExecuteInstruction();

                return _WPData.ResultsDataTable;
        }

        public DataTable GetReportTheGroup()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "SELECT 'MO' TheGroup union SELECT 'Shaft' TheGroup";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public DataTable GetTheProblemType()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "SELECT 'Business Units' TheProblemType union Select 'Shaft Managers' TheProblemType";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public DataTable GetTheShift()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "SELECT 'All' TheShift UNION ALL SELECT 'Morning' TheShift UNION ALL SELECT 'Afternoon' TheShift UNION ALL SELECT 'Night' TheShift";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public DataTable GetTheType()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "SELECT 'All' TheType UNION ALL SELECT 'Mechanical' TheType UNION ALL SELECT 'Electrical' TheType";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public DataTable GetTheCallOut()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "SELECT 'All' TheCallOut UNION ALL SELECT 'Yes' TheCallOut UNION ALL SELECT 'No' TheCallOut";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public DataTable GetTheShow()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "SELECT 'Accepted' TheShow UNION ALL SELECT 'Rejected' TheShow";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public DataTable GetTheAccepted()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "SELECT 'All' TheAccepted UNION ALL SELECT 'Current' TheAccepted UNION ALL SELECT 'Overdue' TheAccepted UNION ALL SELECT 'Signed Off' TheAccepted";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        public DataTable GetTheFromToDate()
        {
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.SqlStatement = "SELECT 'All' TheFromToDate UNION ALL SELECT 'Between Dates' TheFromToDate";
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.ExecuteInstruction();

            return _WPData.ResultsDataTable;
        }

        #region test if workplace has a unapproved chnage of plann
        public bool hasReplanning(string workplaceID, string prodmonth)
        {
            bool theResult = false;
            StringBuilder theSQL = new StringBuilder();

            MWDataManager.clsDataAccess _isStoppedRequest = new MWDataManager.clsDataAccess();
            _isStoppedRequest.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            _isStoppedRequest.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _isStoppedRequest.queryReturnType = MWDataManager.ReturnType.DataTable;

            theSQL.Append("SELECT * FROM PREPLANNING_CHANGEREQUEST PPRC ");
            theSQL.Append("INNER JOIN PREPLANNING_CHANGEREQUEST_APPROVAL PPRA ON ");
            theSQL.Append("PPRC.ChangeRequestID = PPRA.ChangeRequestID ");
            theSQL.Append("WHERE PPRC.WorkplaceID =  '" + workplaceID + "' and ");
            theSQL.Append("PPRC.Prodmonth = " + prodmonth + " and ");
            theSQL.Append("PPRA.Approved = 0 and PPRA.Declined = 0 ");

            _isStoppedRequest.SqlStatement = theSQL.ToString();
            _isStoppedRequest.ExecuteInstruction();
            if (_isStoppedRequest.ResultsDataTable.Rows.Count > 0)
                theResult = true;
            else theResult = false;

            return theResult;
        }

        public bool approved(string WorkplaceID, string ProdMonth)
        {
            bool app = false;
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM PLANMONTH WHERE Workplaceid = '" + WorkplaceID + "' and Prodmonth='" + ProdMonth + "' and (AutoUnPlan is null or AutoUnPlan = '0') and PlanCode = 'MP'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                if ((bool)r["Locked"] == false)
                {


                    app = false ;

                }
                else
                {
                    app = true ;
                }
            }

            return app;
        }
        #endregion

        public bool result(string WorkplaceID1, string ProdMonth1)  //Test for Stopped Workplace
        {
            bool res = false;
            //theSectionID = SectionID;
            //theWorkplaceID = WorkplaceID;
            //theProdmonth = ProdMonth;
            //theSectionID_2 = SectionID_2;
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT * FROM PLANMONTH WHERE Workplaceid = '" + WorkplaceID1 + "' and Prodmonth='" + ProdMonth1 + "'";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                //string temp = Convert.ToDateTime(r["StoppedDate"]).ToString("yyyy/MM/dd");

                if (r["StoppedDate"].ToString () != "")
                {


                    res = true;

                }
                else
                {
                    res = false;
                }
            }

            return res;
        }

        public bool Startwp(string WorkplaceID1, string ProdMonth1)  //Test for Stopped Workplace
        {
            bool res = false;
            //theSectionID = SectionID;
            //theWorkplaceID = WorkplaceID;
            //theProdmonth = ProdMonth;
            //theSectionID_2 = SectionID_2;
            MWDataManager.clsDataAccess _WPData1 = new MWDataManager.clsDataAccess();
            _WPData1.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            _WPData1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData1.SqlStatement = "select description from WORKPLACE where WorkplaceID='" + WorkplaceID1 + "' ";
                                    
            _WPData1.ExecuteInstruction();

            DataTable wpid = new DataTable();
            string wpdesc = "";
            wpid = _WPData1.ResultsDataTable;
            foreach (DataRow dr in wpid.Rows)
            {
                wpdesc = dr["description"].ToString();
            }
            MWDataManager.clsDataAccess _WPData = new MWDataManager.clsDataAccess();
            _WPData.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            _WPData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _WPData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _WPData.SqlStatement = "SELECT DISTINCT * FROM PLANMONTH PPC left join "+
                                    " PREPLANNING_CHANGEREQUEST PPCR ON PPC.Workplaceid=PPCR.Workplaceid and "+
                                    " ppc.Prodmonth =ppcr.ProdMonth and "+
                                    " ppc.sectionid=ppcr.sectionid and "+
                                    " ppc.sectionid_2= ppcr.sectionid_2 " +
                                        //"WHERE PPC.Workplaceid= '" + WorkplaceID1 + "' and PPC.Prodmonth='" + ProdMonth1 + "'  AND PPC.plancode='MP' and ppcr.changeid=5 ";
                                          "WHERE PPCR.OldWorkplaceid= '" + wpdesc + "' and PPC.Prodmonth='" + ProdMonth1 + "'  AND PPC.plancode='MP' and ppcr.changeid=5 ";
                //"SELECT DISTINCT WP.WORKPLACEID,* FROM PLANMONTH PPC LEFT JOIN WORKPLACE WP ON PPC.Workplaceid=WP.WORKPLACEID LEFT JOIN PREPLANNING_CHANGEREQUEST PPCR ON PPC.WorkplaceDesc=PPCR.OldworkplaceID WHERE PPC.Workplaceid= '" + WorkplaceID1 + "' and PPC.Prodmonth='" + ProdMonth1 + "'  AND PPC.Locked=1 ";
            _WPData.ExecuteInstruction();

            foreach (DataRow r in _WPData.ResultsDataTable.Rows)
            {
                if (r["StoppedDate"].ToString() != "" && r["ChangeID"].ToString() == "5")
                {
                    //if (r["ChangeID"].ToString() == "5")
                    //{
                        res = true;
                    //}
                }
                else
                {
                    if (r["StoppedDate"].ToString() == "")
                    {
                        res = true;

                    }
                }
            }

            return res;
        }

        public bool CheckWP(string WorkplaceID, int Prodmonth)
        {
            bool theResult = false;
            MWDataManager.clsDataAccess _sendRequest = new MWDataManager.clsDataAccess();
            _sendRequest.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            _sendRequest.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _sendRequest.queryReturnType = MWDataManager.ReturnType.DataTable;
            _sendRequest.SqlStatement = "sp_RevisedPlanning_MovePlan_Check";

            SqlParameter[] _paramCollectionS =
            {
                 _sendRequest.CreateParameter("@ProdMonth", SqlDbType.Int, 0,Prodmonth  ),
                 _sendRequest.CreateParameter("@WorkplaceID", SqlDbType.VarChar , 20,WorkplaceID  ),
            };
            _sendRequest.ParamCollection = _paramCollectionS;
            _sendRequest.ExecuteInstruction();
            if (_sendRequest.ResultsDataTable.Rows.Count > 0)
                theResult = true;
            else theResult = false;

            return theResult;
        }

        public bool get_reOrgDaySelection(int prodMonth, string theSectionIDMO)
        {
            bool _executionResult=false ;
            Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            Bus_Logic .queryExecutionType =MWDataManager .ExecutionType .GeneralSQLStatement ;
            switch (queryReturnType )
            {
                case  ReturnType.DataTable :
                    Bus_Logic .queryReturnType =MWDataManager .ReturnType .DataTable ;
                    break ;
                case  ReturnType.SQLDataReader :
                    Bus_Logic .queryReturnType =MWDataManager .ReturnType .SQLDataReader ;
                    break ;
            }

            Bus_Logic .SqlStatement ="SELECT 1 thepos,'' Crew_Org " +
                                         " UNION " +
                                         " SELECT 2 thepos,'Contractor' Crew_Org" +
                                         " UNION " +
                                         "SELECT 3 thepos,Crew_Org = GangNo FROM Crew";
                                   //      " ORDER BY SECTIONID";

            Bus_Logic.ExecuteInstruction();

            switch (queryReturnType )
            {
                case ReturnType .DataTable :
                    _resultsDataTable =Bus_Logic .ResultsDataTable ;
                    break ;
                case ReturnType .SQLDataReader :
                    _resultsDataReader =Bus_Logic .ResultsDataReader ;
                    break ;
            }
            _executionResult =true ;
            return _executionResult;
        }

        // 2019/09/03 - DvdB - Updated this function to check the SECTION_COMPLETE data is available  fro the deletes MO
        public bool get_MinerSectionStartEndDates(string theProdMonth, string MoSectionID)
        {
            bool _executionResult = false;

            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT SC.SectionID,Begindate StartDate, EndDate  FROM  SECTION_COMPLETE SC " +
                                         "inner join SECCAL on " +
                                         "SC.PRODMONTH = SECCAL.PRODMONTH and " +
                                         "SC.SECTIONID_1 = SECCAL.SECTIONID   " + 
                                         "WHERE SC.PRODMONTH = " + theProdMonth + " and sc.Sectionid_2 = '" + MoSectionID + "'";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;

        }
        
        public bool get_BeginEndDates(string theSectionIDMO, string theProdMonth)
        {
            bool _executionResult = false;
          
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT MIN(BeginDate) StartDate,MAX(EndDate) EndDate FROM  SECTION_COMPLETE SC " +
                                         "inner join SECCAL on " +
                                         "SC.PRODMONTH = SECCAL.PRODMONTH and " +
                                         "SC.SECTIONID_1 = SECCAL.SECTIONID   " +
                                         "WHERE SC.SECTIONID_2 = '" + theSectionIDMO + "' and " +
                                         "SC.PRODMONTH = " + theProdMonth;

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;

        }

        public bool 

         has_ReplanningRequests(string userID)
        {

            Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;

            Bus_Logic.SqlStatement = "SELECT * FROM PREPLANNING_CHANGEREQUEST_APPROVAL " +
                                     "WHERE User1 = '" + userID +"' or " +
                                     "      User2 = '" + userID + "'";
            Bus_Logic.ExecuteInstruction();
            if (Bus_Logic.ResultsDataTable.Rows.Count > 0)
            {
                return true;
            }
            else return false;

        }

        public bool get_ShaftList(string BusinessUnit)
        {
            bool _executionResult = false;

            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }

              //  Bus_Logic.SqlStatement = "Select DISTINCT theShaft from BME_DATA where theShaft <> 'MINE' ORDER BY theShaft";

                // Chnaged the SQL to show total mine - Dolf - 2010/12/20

                Bus_Logic.SqlStatement = "SELECT DISTINCT 1 theOrder,BusinessUnit BusinessUnit FROM dbo.BME_BusinessUnits " +
                                         "WHERE BusinessUnit = '" + BusinessUnit + "' " +
                                         "UNION " +
                                         "SELECT 2 theOrder, Shaft BMESelection FROM dbo.BME_Shafts " +
                                         "WHERE BusinessUnit = '" + BusinessUnit + "'";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;

        }

        public bool get_BusinessUnit()
        {
            bool _executionResult = false;

            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }

                //  Bus_Logic.SqlStatement = "Select DISTINCT theShaft from BME_DATA where theShaft <> 'MINE' ORDER BY theShaft";

                // Changed the SQL to show total mine - Dolf - 2010/12/20

                Bus_Logic.SqlStatement = "SELECT * FROM BME_BusinessUnits";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;

        }

        public DataTable LoadSections(string prodmonth)
        {
            try
            {
                MWDataManager.clsDataAccess _Section = new MWDataManager.clsDataAccess();
                _Section.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                _Section.SqlStatement = "Select distinct 2 Sort, Name_5 Name from PLANMONTH a inner join SECTION_COMPLETE b on " +
                                        "a.PRODMONTH = b.PRODMONTH and  " +
                                        "a.SECTIONID = b.SECTIONID " +
                                        "where a.PRODMONTH = " + prodmonth + " " +
                                        "union " +
                                        "Select distinct 3 Sort, Name_4 Name from PLANMONTH a inner join SECTION_COMPLETE b on " +
                                        "a.PRODMONTH = b.PRODMONTH and " +
                                        "a.SECTIONID = b.SECTIONID " +
                                        "where a.PRODMONTH = " + prodmonth + " " +
                                        "union " +
                                        "Select distinct 4 Sort, Name_3 Name from PLANMONTH a inner join SECTION_COMPLETE b on " +
                                        "a.PRODMONTH = b.PRODMONTH and " +
                                        "a.SECTIONID = b.SECTIONID " +
                                        "where a.PRODMONTH = " + prodmonth + " " +
                                        "union " +
                                        "Select distinct 5 Sort, Name_2 Name from PLANMONTH a inner join SECTION_COMPLETE b on " +
                                        "a.PRODMONTH = b.PRODMONTH and  " +
                                        "a.SECTIONID = b.SECTIONID " +
                                        "where a.PRODMONTH = " + prodmonth + " " +
                                        "union " +
                                        "Select distinct 6 Sort, Name_1 Name from PLANMONTH a inner join SECTION_COMPLETE b on " +
                                        "a.PRODMONTH = b.PRODMONTH and  " +
                                        "a.SECTIONID = b.SECTIONID " +
                                        "where a.PRODMONTH = " + prodmonth;
                _Section.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _Section.queryReturnType = MWDataManager.ReturnType.DataTable;
                _Section.ExecuteInstruction();

                return _Section.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

        }

        public DataTable LoadSections(string FromMonth, string ToMonth)
        {
            try
            {
                MWDataManager.clsDataAccess _Section = new MWDataManager.clsDataAccess();
                _Section.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                _Section.SqlStatement = "Select distinct 2 Sort, Name_5 Name from PLANMONTH a inner join SECTION_COMPLETE b on \n" +
                                        "a.PRODMONTH = b.PRODMONTH and  \n" +
                                        "a.SECTIONID = b.SECTIONID \n" +
                                        "where a.PRODMONTH >= " + FromMonth + " \n" +
                                        " and a.ProdMonth <= " + ToMonth + " \n" +
                                        "union \n" +
                                        "Select distinct 3 Sort, Name_4 Name from PLANMONTH a inner join SECTION_COMPLETE b on \n" +
                                        "a.PRODMONTH = b.PRODMONTH and \n" +
                                        "a.SECTIONID = b.SECTIONID \n" +
                                        "where a.PRODMONTH >= " + FromMonth + " \n" +
                                        " and a.ProdMonth <= " + ToMonth + " \n" +
                                        "union \n" +
                                        "Select distinct 4 Sort, Name_3 Name from PLANMONTH a inner join SECTION_COMPLETE b on \n" +
                                        "a.PRODMONTH = b.PRODMONTH and \n" +
                                        "a.SECTIONID = b.SECTIONID \n" +
                                        "where a.PRODMONTH >= " + FromMonth + " \n" +
                                        " and a.ProdMonth <= " + ToMonth + " \n" +
                                        "union \n" +
                                        "Select distinct 5 Sort, Name_2 Name from PLANMONTH a inner join SECTION_COMPLETE b on \n" +
                                        "a.PRODMONTH = b.PRODMONTH and  \n" +
                                        "a.SECTIONID = b.SECTIONID \n" +
                                        "where a.PRODMONTH >= " + FromMonth + " \n" +
                                        " and a.ProdMonth <= " + ToMonth + " \n" +
                                        "union \n" +
                                        "Select distinct 6 Sort, Name_1 Name from PLANMONTH a inner join SECTION_COMPLETE b on \n" +
                                        "a.PRODMONTH = b.PRODMONTH and  \n" +
                                        "a.SECTIONID = b.SECTIONID \n" +
                                        "wherea.PRODMONTH >= " + FromMonth + " \n" +
                                        " and a.ProdMonth <= " + ToMonth;
                _Section.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _Section.queryReturnType = MWDataManager.ReturnType.DataTable;
                _Section.ExecuteInstruction();

                return _Section.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

        }

        public DataTable LoadEngSections(string prodmonth)
        {
            try
            {
                MWDataManager.clsDataAccess _Section = new MWDataManager.clsDataAccess();
                _Section.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                _Section.SqlStatement = "Select distinct 1 Sort, SectionID, Name from SECTION " +
                                        "where PRODMONTH = " + prodmonth + " " +
                                        "and HierarchicalID = 1 and HierarchicalType = 'Eng'" +
                                        "union " +
                                        "Select distinct 2 Sort, SectionID, Name from SECTION " +
                                        "where PRODMONTH = " + prodmonth + " " +
                                        "and HierarchicalID = 2 and HierarchicalType = 'Eng'" +
                                        "union " +
                                        "Select distinct 3 Sort, SectionID, Name from SECTION " +
                                        "where PRODMONTH = " + prodmonth + " " +
                                        "and HierarchicalID = 3 and HierarchicalType = 'Eng'" +
                                        "union " +
                                        "Select distinct 4 Sort, SectionID, Name from SECTION " +
                                        "where PRODMONTH = " + prodmonth + " " +
                                        "and HierarchicalID = 4 and HierarchicalType = 'Eng'" +
                                        "union " +
                                        "Select distinct 5 Sort, SectionID, Name from SECTION " +
                                        "where PRODMONTH = " + prodmonth + " " +
                                        "and HierarchicalID = 5 and HierarchicalType = 'Eng'" +
                                        "union " +
                                        "Select distinct 6 Sort, SectionID, Name from SECTION " +
                                        "where PRODMONTH = " + prodmonth + " " +
                                        "and HierarchicalID = 6 and HierarchicalType = 'Eng'";
                _Section.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _Section.queryReturnType = MWDataManager.ReturnType.DataTable;
                _Section.ExecuteInstruction();

                return _Section.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

        }

        public DataTable LoadMOSections(string prodmonth)
        {
            try
            {
                MWDataManager.clsDataAccess _Section = new MWDataManager.clsDataAccess();
                _Section.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                _Section.SqlStatement = "Select distinct 5 Sort, Name_2 Name from PLANMONTH a inner join SECTION_COMPLETE b on " +
                                        "a.PRODMONTH = b.PRODMONTH and  " +
                                        "a.SECTIONID = b.SECTIONID " +
                                        "where a.PRODMONTH = " + prodmonth;
                _Section.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _Section.queryReturnType = MWDataManager.ReturnType.DataTable;
                _Section.ExecuteInstruction();

                return _Section.ResultsDataTable;
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

        } 

        public bool get_HierID(string prodmonth, string sectionID)
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }

                //  Bus_Logic.SqlStatement = "Select DISTINCT theShaft from BME_DATA where theShaft <> 'MINE' ORDER BY theShaft";

                // Chnaged the SQL to show total mine - Dolf - 2010/12/20

                Bus_Logic.SqlStatement = " select HierarchicalID from Section where " +
                                        " ProdMonth = '" + prodmonth + "' and HierarchicalType = 'Pro' " +
                                        " and sectionID  = '" + sectionID + "' ";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;

            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }


            return _executionResult;
        }

        public bool get_AllSections(string prodmonth)
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }

                //  Bus_Logic.SqlStatement = "Select DISTINCT theShaft from BME_DATA where theShaft <> 'MINE' ORDER BY theShaft";

                // Chnaged the SQL to show total mine - Dolf - 2010/12/20

                Bus_Logic.SqlStatement = " select SectionID,Name,HierarchicalID from Section where " +
                                        " ProdMonth = '" + prodmonth + "' and HierarchicalType = 'Pro' " +
                    // "and sectiontype = 0 " +
                                        "order by HierarchicalID, Name";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;

            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }


            return _executionResult;
        }

        public int theGetHierarchicalLevel(string ProdMonth, string SectionID)
        {
            int TheLevel = 1;
            Bus_Logic2.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
            Bus_Logic2.queryReturnType = MWDataManager.ReturnType.DataTable;
            Bus_Logic2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            Bus_Logic2.SqlStatement = string.Format("Select HierarchicalID from Section where SectionID = '{0}' AND ProdMonth = {1}", SectionID.ToString(), ProdMonth.ToString());
            Bus_Logic2.ExecuteInstruction();

            foreach (DataRow s in Bus_Logic.ResultsDataTable.Rows)
            {
                TheLevel = Convert.ToInt32(s["HierarchicalID"].ToString());
            }

            return TheLevel;
        }

        public bool get_Sections(string prodmonth, string HierarchicalID)
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }

                //  Bus_Logic.SqlStatement = "Select DISTINCT theShaft from BME_DATA where theShaft <> 'MINE' ORDER BY theShaft";

                // Chnaged the SQL to show total mine - Dolf - 2010/12/20

                //Bus_Logic.SqlStatement = "select * from section where " +
                //                         "prodmonth = '" + prodmonth + "' " +
                //                         "and hierarchicalid = '" + HierarchicalID + "' " +
                //                         "and HierarchicalType = 'Pro' ";// +
                //                          //   "order by sectionid";

                Bus_Logic.SqlStatement = "select distinct SectionID_2 SectionID, NAME_2 Name from section_complete a  " +
                                     
                                         " where A.prodmonth = '" + prodmonth + "' " +
                                         "order by NAME_2 ";

                    Bus_Logic.ExecuteInstruction();

                    switch (queryReturnType)
                    {
                        case ReturnType.DataTable:
                            _resultsDataTable = Bus_Logic.ResultsDataTable;
                            break;
                        case ReturnType.SQLDataReader:
                            _resultsDataReader = Bus_Logic.ResultsDataReader;
                            break;
                    }
                    _executionResult = true;
                
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }
        
        
            return _executionResult;
        }

        public DataTable GetSectionsMO(string Prodmonth, List<string> PlanBookSection)
        {
            Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            bool HasError = false;
            try
            {
                int x = -1;
                Bus_Logic.SqlStatement = "";
                foreach (string TheSection in PlanBookSection)
                {
                    x += 1;
                    String TheName = "";
                    int level;
                    GetHierarchicalLevel TheLevel = new GetHierarchicalLevel();


                    level = TheLevel.theGetHierarchicalLevel(Prodmonth, PlanBookSection[x]);

                    switch (level)
                    {
                        case 1: TheName = "SectionID_5"; break;
                        case 2: TheName = "SectionID_4"; break;
                        case 3: TheName = "SectionID_3"; break;
                        case 4: TheName = "SectionID_2"; break;
                        case 5: TheName = "SectionID_1"; break;
                        case 6: TheName = "SectionID"; break;
                    }

                    Bus_Logic.SqlStatement += String.Format("select distinct name_2 Name,SectionID_2,SectionID from planmonth a " +
                                                            "inner join section_complete b on " +
                                                            "a.prodmonth = b.prodmonth and " +
                                                            "a.sectionid = b.sectionid " +

                                                            "where a.prodmonth = {0} " +
                                                            "and b.{1} = '{2}' Union ", Prodmonth, TheName, PlanBookSection[x]);


                }

                Bus_Logic.SqlStatement = Bus_Logic.SqlStatement.Substring(0, Bus_Logic.SqlStatement.Length - 6);

                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                Bus_Logic.ExecuteInstruction();
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return null;
            }
            else
            {
                return Bus_Logic.ResultsDataTable;
            }
        }

        public DataTable GetSections(string Prodmonth, List<string> PlanBookSection)
        {
            Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
            bool HasError = false;
            try
            {
                int x = -1;
                Bus_Logic.SqlStatement = "";
                foreach (string TheSection in PlanBookSection)
                {
                    x += 1;
                    String TheName = "";
                    int level;
                    GetHierarchicalLevel TheLevel = new GetHierarchicalLevel();
                    TheLevel.UserCurrentInfo = _TUserCurrentInfo;
                    TheLevel.theSystemDBTag = _systemDBTag;


                    level = TheLevel.theGetHierarchicalLevel(Prodmonth, PlanBookSection[x]);

                    switch (level)
                    {
                        case 1: TheName = "SectionID_5"; break;
                        case 2: TheName = "SectionID_4"; break;
                        case 3: TheName = "SectionID_3"; break;
                        case 4: TheName = "SectionID_2"; break;
                        case 5: TheName = "SectionID_1"; break;
                        case 6: TheName = "SectionID"; break;
                    }

                    Bus_Logic.SqlStatement += String.Format("select distinct name_1 Name from planmonth a " +
                                                            "inner join section_complete b on " +
                                                            "a.prodmonth = b.prodmonth and " +
                                                            "a.sectionid = b.sectionid " +

                                                            "where a.prodmonth = {0} " +
                                                            "and b.{1} = '{2}' Union ", Prodmonth, TheName, PlanBookSection[x]);


                }

                Bus_Logic.SqlStatement = Bus_Logic.SqlStatement.Substring(0, Bus_Logic.SqlStatement.Length - 6);

                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                Bus_Logic.ExecuteInstruction();
            }
            catch
            {
                HasError = true;
            }

            if (HasError == true)
            {
                return null;
            }
            else
            {
                return Bus_Logic.ResultsDataTable;
            }
        }

        public bool getsectionID(string prodmonth, string name)
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }

                //  Bus_Logic.SqlStatement = "Select DISTINCT theShaft from BME_DATA where theShaft <> 'MINE' ORDER BY theShaft";

                // Chnaged the SQL to show total mine - Dolf - 2010/12/20

                Bus_Logic.SqlStatement = "select sectionid from section where prodmonth = '" + prodmonth + "' and name= '" + name + "'";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;

            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }


            return _executionResult;
        }

        public bool LoadLevel(string Thelevel)
        {
            bool _executionResult = false;
            //int _executionResult;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }

                //  Bus_Logic.SqlStatement = "Select DISTINCT theShaft from BME_DATA where theShaft <> 'MINE' ORDER BY theShaft";

                // Chnaged the SQL to show total mine - Dolf - 2010/12/20
                if (Thelevel == null )
                {
                    Thelevel = "0";
                }
                Bus_Logic.SqlStatement = "Select HierarchicalID,Description from hierarch where hierarchicalType = 'Pro' and HierarchicalID > " + Thelevel + " and HierarchicalID <= " + TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID + " + 1";

                Bus_Logic.ExecuteInstruction();

               

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;

            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }


            return _executionResult;
        }
        
        public bool get_SectionsReportsNotSaved(string prodmonth, string activity)
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                StringBuilder sb
                    = new StringBuilder();
                sb.AppendLine("SELECT DISTINCT Name_2 NAME,SectionID_2 SECTIONID");
                sb.AppendLine("FROM( select SC.NAME_2,SC.SectionID_2,");
                sb.AppendLine("dbo.fn_FileExists('" + TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).PlanProtSaveDir + "' + [FileName]) FileExsits ");
                sb.AppendLine("from section SE ");
                sb.AppendLine("INNER JOIN PLANMONTH PP ON ");
                sb.AppendLine("SE.Prodmonth = PP.Prodmonth and ");
                sb.AppendLine("SE.SectionID = PP.SectionID ");
                sb.AppendLine("LEFT JOIN ");
                sb.AppendLine("PLANPROT_APPROVEDTEMPLATE PPAT ON ");
                sb.AppendLine("PP.Prodmonth = PPAT.Prodmonth and ");
                sb.AppendLine("PP.WorkplaceID = PPAT.WorkplaceID ");
                sb.AppendLine("INNER JOIN ");
                sb.AppendLine("[dbo].[SECTION_COMPLETE] SC ON ");
                sb.AppendLine("SE.SectionID = SC.SectionID and ");
                sb.AppendLine("SE.Prodmonth = SC.Prodmonth ");
                sb.AppendLine("where SE.Prodmonth = " + prodmonth + " and       ");
                sb.AppendLine("PP.Locked = 1 and       	  ");
                sb.AppendLine("PP.[Activity] = "+ activity + ") ");
                sb.AppendLine("theData       	  ");
                sb.AppendLine("WHERE FileExsits = 0");
                Bus_Logic.SqlStatement = sb.ToString();

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }

        public bool get_ActivityPlan()
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT [Activity] as [Code] " +
                                        "       ,[Description] as [Desc] " +
                                        "  FROM [CODE_ACTIVITY]";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }


        public bool get_Activity()
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection); 
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT Activity ,Description" +
                                        "  FROM CODE_ACTIVITY";

                Bus_Logic.ExecuteInstruction();

                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }

        public DataTable GetActivity()
        {
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT Activity ,Description" +
                                        "  FROM CODE_ACTIVITY";

                Bus_Logic.ExecuteInstruction();

            }
            catch (Exception except)
            {
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return Bus_Logic.ResultsDataTable;
        }

        public bool get_ReviseActivityPlan()
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT [Activity] as [Code] " +
                                        "       ,[Description] as [Desc] " +
                                        "  FROM [CODE_ACTIVITY] WHERE [Activity] IN (0,1) ";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }

        public bool get_ReviseActivity()
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT [Activity] as [Code] " +
                                        "       ,[Description] as [Desc] " +
                                        "  FROM [CODE_ACTIVITY] WHERE [Activity] IN (0,1) ";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }

        public bool get_WeekDay()
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT 0 Code, 'Monday' [Day] " +
                                        "UNION " +
                                        "SELECT 1 Code, 'Tuesday' [Day] " +
                                        "union " +
                                        "select 2 Code, 'Wednesday' [Day] " +
                                        "union " +
                                        "select 3 Code, 'Thursday' [Day] " +
                                        "union " +
                                        "Select 4 Code, 'Friday' [Day]";

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }

        #region get_Activity_Reports
        #region get_Activity_Reports
        public bool get_Activity_Reports()
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT 0 Code, 'Stoping' [Desc] " +
                                        "UNION " +
                                        "SELECT 1 Code, 'Development' [Desc] " +
                                        "union " +
                                        "select 7 Code, 'Dev.m3' [Desc] " +
                                        "union " +
                                        "select 8 Code, 'Sweepings' [Desc] ";

                Bus_Logic.ExecuteInstruction();

                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }
        #endregion
        #endregion

        public DataTable GetActivityReports()
        {
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "SELECT 0 Code, 'Stoping' [Desc] " +
                                        "UNION " +
                                        "SELECT 1 Code, 'Development' [Desc] " +
                                        "union " +
                                        "select 8 Code, 'Sweepings' [Desc] ";

                Bus_Logic.ExecuteInstruction();

            }
            catch (Exception except)
            {
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return Bus_Logic.ResultsDataTable;
        }

        public bool get_Plan()
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                //Bus_Logic.SqlStatement = "SELECT 0 Code, 'Planning' [Descd] " +
                //                        "UNION " +
                Bus_Logic.SqlStatement = "SELECT 1 Code, 'Dynamic Plan' [Descd] " +
                                        "union " +
                                        "select 2 Code, 'Lock Plan' [Descd] ";
                                       

                Bus_Logic.ExecuteInstruction();

                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        _resultsDataTable = Bus_Logic.ResultsDataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        _resultsDataReader = Bus_Logic.ResultsDataReader;
                        break;
                }
                _executionResult = true;
            }
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return _executionResult;
        }

        public bool GetPlanSectionsAndNameADO(String Prodmonth)
        {
            bool _executionResult = false;
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }
                for (int i = 1; i < 6; i++)
                {
                    if (i == TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID )
                    {

                        Bus_Logic.SqlStatement = " select distinct SectionID_5 As sectionid, NAME_5 As NAME from planmonth a inner join section_complete b on " +
                                                 "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_5 As sectionid, NAME_5 As NAME from planmonth_Oldgold a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_5 As sectionid, NAME_5 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "order by NAME_5, SectionID_5 ";



                        Bus_Logic.ExecuteInstruction();
                       
                        d1 = Bus_Logic.ResultsDataTable;
                    }

                    if (i == TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID -1)
                    {

                        Bus_Logic.SqlStatement = " select distinct SectionID_4 As sectionid, NAME_4 As NAME from planmonth a inner join section_complete b on " +
                                                 "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_4 As sectionid, NAME_4 As NAME from planmonth_Oldgold a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_4 As sectionid, NAME_4 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "order by NAME_4, SectionID_4 ";



                        Bus_Logic.ExecuteInstruction();
                       
                        d2 = Bus_Logic.ResultsDataTable;


                    }

                         if (i == TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID - 2)
                    {

                        Bus_Logic.SqlStatement = " select distinct SectionID_3 As sectionid, NAME_3 As NAME from planmonth a inner join section_complete b on " +
                                                 "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_3 As sectionid, NAME_3 As NAME from planmonth_Oldgold a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_3 As sectionid, NAME_3 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "order by NAME_3, SectionID_3 ";



                        Bus_Logic.ExecuteInstruction();
                        d3 = Bus_Logic.ResultsDataTable;


                    }

                         if (i == TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID - 3)
                    {

                        Bus_Logic.SqlStatement = " select distinct b.SectionID_2 As sectionid, NAME_2 As NAME from planmonth a inner join section_complete b on " +
                                                 "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_2 As sectionid, NAME_2 As NAME from planmonth_Oldgold a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_2 As sectionid, NAME_2 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "order by NAME_2, b.SectionID_2 ";



                        clsDataResult dr = Bus_Logic.ExecuteInstruction();
                        d4 = Bus_Logic.ResultsDataTable;

                    }

                         if (i == TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID - 4)
                    {

                        Bus_Logic.SqlStatement = " select distinct SectionID_1 As sectionid, NAME_1 As NAME from planmonth a inner join section_complete b on " +
                                                 "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_1 As sectionid, NAME_1 As NAME from planmonth_Oldgold a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct SectionID_1 As sectionid, NAME_1 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "order by NAME_1, SectionID_1 ";



                        Bus_Logic.ExecuteInstruction();
                        d5 = Bus_Logic.ResultsDataTable;

                    }

                         if (i == TProductionGlobal.getSystemSettingsProductioInfo(_TUserCurrentInfo.Connection).MOHierarchicalID - 5)
                    {

                        Bus_Logic.SqlStatement = " select distinct a.SectionID As sectionid, NAME As NAME from planmonth a inner join section_complete b on " +
                                                 "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct a.SectionID As sectionid, NAME As NAME from planmonth_Oldgold a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "union " +
                                                  "select distinct a.SectionID As sectionid, NAME As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on " +
                                                  "a.prodmonth = b.prodmonth and " +
                                                  "a.sectionid = b.sectionid " +
                                                  "Where " +
                                                  "a.Prodmonth = '" + Prodmonth + "' " +
                                                  "order by NAME, SectionID ";



                        Bus_Logic.ExecuteInstruction();
                        d6 = Bus_Logic.ResultsDataTable;


                    }

                                          

                    switch (queryReturnType)
                    {
                        case ReturnType.DataTable:
                            DataSet ds = new DataSet();
                            DataTable dt = new DataTable();
                            dt.Merge(d1);
                            dt.AcceptChanges();
                            dt.Merge(d2);
                            dt.AcceptChanges();
                            dt.Merge(d3);
                            dt.AcceptChanges();
                            dt.Merge(d4);
                            dt.AcceptChanges();
                            dt.Merge(d5);
                            dt.AcceptChanges();
                            dt.Merge(d6);
                            dt.AcceptChanges();
                            _resultsDataTable = dt;
                            break;
                        case ReturnType.SQLDataReader:
                            _resultsDataReader = Bus_Logic.ResultsDataReader;
                            break;
                    }
                    _executionResult = true;
                }
             }
            
            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }
        
        // }

            return _executionResult;
      
        }

        public DataTable GetPlanSectionsAndName(String Prodmonth, String ForWhoSectionID, int FromLevel, int ToLevel)
        {
            bool _executionResult = false;
            //int HierarchLevel = 1;
            //string HierarchName = "";
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;


                Bus_Logic.SqlStatement = String.Format("select distinct 2 TheLevel, SectionID_5 As sectionid, NAME_5 As NAME from planmonth a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 2 TheLevel, SectionID_5 As sectionid, NAME_5 As NAME from planmonth_Oldgold a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 2 TheLevel, SectionID_5 As sectionid, NAME_5 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "\n" +
                                                       "Union\n" +
                                                       "select distinct 3 TheLevel, SectionID_4 As sectionid, NAME_4 As NAME from planmonth a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 3 TheLevel, SectionID_4 As sectionid, NAME_4 As NAME from planmonth_Oldgold a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 3 TheLevel, SectionID_4 As sectionid, NAME_4 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "Union\n" +
                                                       "select distinct 4 TheLevel, SectionID_3 As sectionid, NAME_3 As NAME from planmonth a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 4 TheLevel, SectionID_3 As sectionid, NAME_3 As NAME from planmonth_Oldgold a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 4 TheLevel, SectionID_3 As sectionid, NAME_3 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "\n" +
                                                       "Union\n" +
                                                       "select distinct 5 TheLevel, b.SectionID_2 As sectionid, NAME_2 As NAME from planmonth a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 5 TheLevel, b.SectionID_2 As sectionid, NAME_2 As NAME from planmonth_Oldgold a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 5 TheLevel, b.SectionID_2 As sectionid, NAME_2 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "Union\n" +
                                                       "select distinct 6 TheLevel, b.SectionID_1 As sectionid, NAME_1 As NAME from planmonth a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 6 TheLevel, b.SectionID_1 As sectionid, NAME_1 As NAME from planmonth_Oldgold a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 6 TheLevel, b.SectionID_1 As sectionid, NAME_1 As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "Union\n" +
                                                       "select distinct 7 TheLevel, b.SectionID As sectionid, NAME As NAME from planmonth a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 7 TheLevel, b.SectionID As sectionid, NAME As NAME from planmonth_Oldgold a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1})) \n" +
                                                       "union \n" +
                                                       "select distinct 7 TheLevel, b.SectionID As sectionid, NAME As NAME from PLANMONTH_SUNDRYMINING a inner join section_complete b on \n" +
                                                       "a.prodmonth = b.prodmonth and \n" +
                                                       "a.sectionid = b.sectionid \n" +
                                                       "Where \n" +
                                                       "a.Prodmonth = {0} \n" +
                                                       "and (b.SectionID_5 IN ({1}) \n" +
                                                       "or b.SectionID_4 IN ({1}) \n" +
                                                       "or b.SectionID_3 IN ({1}) \n" +
                                                       "or b.SectionID_2 IN ({1}) \n" +
                                                       "or b.SectionID_1 IN ({1}) \n" +
                                                       "or b.SectionID IN ({1}))) a \n" +
                                                       "Where TheLevel >= {2} \n" +
                                                       "and TheLevel <= {3}    \n" +
                                                       "Order By TheLevel, Name ", Prodmonth, ForWhoSectionID, FromLevel, ToLevel);

                Bus_Logic.ExecuteInstruction();



               
            }

            catch (Exception except)
            {
                _executionResult = false;
                throw new ApplicationException(except.Message, except);
            }


            return Bus_Logic.ResultsDataTable;

        }

        public DataTable loadRunDate(string _whatCalen, string Prodmonth)
        {
            if (Prodmonth != null)
            {
                if (_whatCalen == "P")
                {
                    Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                    Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                    Bus_Logic.SqlStatement = "SELECT Max(EndDate) EDate FROM  SECTION_COMPLETE SC " +
                                             "inner join SECCAL on " +
                                             "SC.PRODMONTH = SECCAL.PRODMONTH and " +
                                             "SC.SECTIONID_1 = SECCAL.SECTIONID   " +
                                             "WHERE SC.PRODMONTH = '" + Prodmonth + "' ";

                    Bus_Logic.ExecuteInstruction();

                    return Bus_Logic.ResultsDataTable;
                }
                else
                {
                    Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                    Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                    Bus_Logic.SqlStatement = "select max(EndDate) EDate from CALENDARMILL  " +
                                             "WHERE MillMonth= '" + Prodmonth + "' ";

                    Bus_Logic.ExecuteInstruction();

                    return Bus_Logic.ResultsDataTable;
                }
            }
            else
            {
                return null;
            }
        }

        public DataTable LoadMOName(string _activity)
        {
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = " Select Distinct PeerName_3 Shaft \r\n " +
                            " from PLanmonth a \r\n " +
                            " inner join Section_Complete b on \n" +
                            "   a.Prodmonth = b.Prodmonth and \n" +
                            "   a.sectionid = b.Sectionid \n" +
                            " where a.Prodmonth = (Select currentproductionmonth from Sysset) and \n" +
                            "       Activity = '" + _activity + "' and PLancode = 'MP'";

                Bus_Logic.ExecuteInstruction();

            }
            catch (Exception except)
            {
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return Bus_Logic.ResultsDataTable;
        }

        public DataTable getReportToList(string _prodmonth, int _levelid)
        {
            try
            {
                Bus_Logic.ConnectionString = TConnections.GetConnectionString(_systemDBTag, _TUserCurrentInfo.Connection);
                Bus_Logic.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                switch (queryReturnType)
                {
                    case ReturnType.DataTable:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.DataTable;
                        break;
                    case ReturnType.SQLDataReader:
                        Bus_Logic.queryReturnType = MWDataManager.ReturnType.SQLDataReader;
                        break;
                }


                Bus_Logic.SqlStatement = "select distinct Hierarchicalid,  " +
                                        "Name = (select  " +
                                        "case when Hierarchicalid = 1 then 'Business Coach' " +
                                        "when Hierarchicalid = 2 then 'Mine Manager'  " +
                                        "when Hierarchicalid = 3 then 'Mining Manager' " +
                                        "when Hierarchicalid = 4 then 'Mine Overseer' " +
                                        "when Hierarchicalid = 5 then 'Coach' " +
                                        "when Hierarchicalid = 6 then 'Miner' " +
                                        "end) from Section where Hierarchicalid > " + _levelid + " and  " +
                                        "ProdMonth = '" + _prodmonth + "'  " +
                                        "union  \r\n " +
                                        "select 7 hierarchicalid, 'Workplace' Name " +
                                        "order by Hierarchicalid";

                Bus_Logic.ExecuteInstruction();

            }
            catch (Exception except)
            {
                throw new ApplicationException(except.Message, except);
            }
            finally
            {
            }

            return Bus_Logic.ResultsDataTable;
        }
    }
}
