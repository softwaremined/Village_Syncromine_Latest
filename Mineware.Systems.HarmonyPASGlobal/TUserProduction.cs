using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mineware.Systems.Global;
using System.Data;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionGlobal
{
    public  class TUserProduction
    {
        private string m_ConnectionString = "";
        private string m_SiteTag = "";
        private string m_UserID = "";
        private string m_InquirerID = "";
        private string m_SectionID = "";
       // private int m_BackDateBooking = 0;  //0 = none; 1 = one day; 2 = full;
        private string m_UpdateCurrentPlan = "";
        private int m_HierarchicalID = 0;
        private string m_GradeChangeAllert = ""; //Y = Yes N = No
        private string m_GoldenPanelAlert = "";
        private string m_StandardUser = "";
        private string m_UnpayPanelAlert = "";
        private int m_DaysBackdate = 0;
        private string m_SBBehindBookingsUser = "";
        private bool m_UpdateCrew = false;
        private bool m_GoldGradechange = false;
        private int m_DaysLockNextPlan = 0;
        //private bool m_ForceBooking = false;
        private string m_SystemAdminProfile = "";
        private bool m_ForecastBook = false;
        private string m_BookingType = "";
        private List<string> m_ReportSections = new List<string>();
        private List<string> m_PlanBookSections = new List<string>();
        private bool m_BackdatedRevisedPlanning = false;
        private string m_WPProduction = "";
        private string m_WPSurface = "";
        private string m_WPUnderGround = "";
        private string m_WPEditName = "";
        private string m_WPEditAttribute = "";
        private string m_WPClassify = "";

        public string ConnectionString { get { return m_ConnectionString; } set { m_ConnectionString = value; } }
        public string UserID { get { return m_UserID; } }
        public string SiteTag { get { return m_SiteTag; } set { m_SiteTag = value; } }
        public string SectionID { get { return m_SectionID; } }
        //public int BackDateBooking { get { return m_BackDateBooking; } }
        public string UpdateCurrentPlan { get { return m_UpdateCurrentPlan; } }
        public int HierarchicalID { get { return m_HierarchicalID; } }
        public string GradeChangeAllert { get { return m_GradeChangeAllert; } }
        public string GoldenPanelAlert { get { return m_GoldenPanelAlert; } }
        public string StandardUser { get { return m_StandardUser; } }
        public string UnpayPanelAlert { get { return m_UnpayPanelAlert; } }
        public int DaysBackdate { get { return m_DaysBackdate; } }
        public string SBBehindBookingsUser { get { return m_SBBehindBookingsUser; } }
        public bool UpdateCrew { get { return m_UpdateCrew; } }
        public bool GoldGradechange { get { return m_GoldGradechange; } }
        public int DaysLockNextPlan { get { return m_DaysLockNextPlan; } }
        //public bool ForceBooking { get { return m_ForceBooking; } }
        public string SystemAdminProfile { get { return m_SystemAdminProfile; } }
        public bool ForecastBook { get { return m_ForecastBook; } }
        public string BookingType { get { return m_BookingType; } }
        public List<string> ReportSections { get { return m_ReportSections; } }
        public List<string> PlanBookSections { get { return m_PlanBookSections; } }
        public bool BackdatedRevisedPlanning { get { return m_BackdatedRevisedPlanning ; } }
        public string WPProduction { get { return m_WPProduction; } }
        public string WPSurface { get { return m_WPSurface; } }
        public string WPUnderGround { get { return m_WPUnderGround; } }
        public string WPEditName { get { return m_WPEditName; } }
        public string WPEditAttribute { get { return m_WPEditAttribute; } }
        public string WPClassify { get { return m_WPClassify; } }



        public void SetUserInfo(string UserID,string systemDBTag, string connection)
        {

            string theSectionId = "";
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(systemDBTag, connection); ;
            if (_dbMan.ConnectionString != "")
            {
                _dbMan.SqlStatement = String.Format("Select * from Users where UserID = '{0}'", UserID);
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;

                // m_SystemAdminProfile = "System Admin";
                m_SystemAdminProfile = "1";

                _dbMan.ExecuteInstruction();
                foreach (DataRow dr in _dbMan.ResultsDataTable.Rows)
                {
                    m_UserID = dr["UserID"].ToString();
                    int SECTION = 63430;
                    string Sect = Convert.ToString(SECTION);
                    m_SectionID = Sect;//dr["SectionID"].ToString();
                    //m_PlanBookSectionID = dr["PlanBookSectionID"].ToString();

                    if (dr["ForecastBook"].ToString() == "Y")
                        m_ForecastBook = true;
                    else
                        m_ForecastBook = false;

                    if (dr["BackdatedRevisedPlanning"].ToString() == "")
                    {
                        m_BackdatedRevisedPlanning = false;
                    }
                    else
                    {
                        m_BackdatedRevisedPlanning = Convert.ToBoolean(dr["BackdatedRevisedPlanning"]);
                    }

                    m_WPProduction = dr["WPProduction"].ToString();
                    m_WPSurface = dr["WPSurface"].ToString();
                    m_WPUnderGround = dr["WPUnderGround"].ToString();
                    m_WPEditName = dr["WPEditName"].ToString();
                    m_WPEditAttribute = dr["WPEditAttribute"].ToString();
                    m_WPClassify = dr["WPClassify"].ToString();

                    m_BookingType = dr["BookingType"].ToString();
                    //m_BackDateBooking = Convert.ToInt32(dr["BackdateBooking"].ToString());
                    m_DaysBackdate = Convert.ToInt32(dr["DaysBackdate"].ToString());
                    //m_ForceBooking = Convert.ToBoolean(dr["ForceBooking"].ToString());
                }

                 theSectionId = m_SectionID;

                _dbMan.SqlStatement = "Select HierarchicalID from Section where ProdMonth = " + TProductionGlobal.getSystemSettingsProductioInfo(SiteTag).CurrentProductionMonth +
                                      " AND SectionID = '" + theSectionId + "'";
                _dbMan.ExecuteInstruction();

                foreach (DataRow dr in _dbMan.ResultsDataTable.Rows)
                {
                    m_HierarchicalID = Convert.ToInt32(dr["HierarchicalID"].ToString());
                }

                // add planning sections
                _dbMan.SqlStatement = String.Format("SELECT [SectionID] FROM [USERS_SECTION] WHERE UserID = '{0}' and LinkType = 'P'", UserID);
                _dbMan.ExecuteInstruction();

                foreach (DataRow dr in _dbMan.ResultsDataTable.Rows)
                {
                    m_PlanBookSections.Add(dr["SectionID"].ToString());
                }

                // add report sections
                _dbMan.SqlStatement = String.Format("SELECT [SectionID] FROM [USERS_SECTION] WHERE UserID = '{0}' and LinkType = 'R'", UserID);
                _dbMan.ExecuteInstruction();

                foreach (DataRow dr in _dbMan.ResultsDataTable.Rows)
                {
                    m_ReportSections.Add(dr["SectionID"].ToString());
                }

            }

        }

    }
}
