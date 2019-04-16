using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning;
using Mineware.Systems.Planning.PrePlanning;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class frmPrePlanningNotification : ucBaseUserControl 
    {
        MWDataManager.clsDataAccess _UserProfiles = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _UserList = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _UpdateSecurity = new MWDataManager.clsDataAccess();
        public frmPrePlanningNotification()
        {
            InitializeComponent();
           // gcSettings.DataSource = null;

        }

        private DataTable getUserList(string excludeUserID, string profile)
        {
            MWDataManager.clsDataAccess _UserList = new MWDataManager.clsDataAccess();
            _UserList.ConnectionString = TUserInfo.Connection;
            _UserList.SqlStatement = "SELECT USERID,NAME FROM USERS " +
                                     "WHERE USERPROFILEID = '" + profile + "'";
            _UserList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _UserList.queryReturnType = MWDataManager.ReturnType.DataTable;
            _UserList.ExecuteInstruction();

            return _UserList.ResultsDataTable;

        }

        private void gvSettings_ShownEditor(object sender, EventArgs e)
        {
            GridView view;
            view = sender as GridView;

            if (view.FocusedColumn.FieldName == "CPM_UserID1" || view.FocusedColumn.FieldName == "CPM_UserID2")
            {
            //    string profile = lbUserProfiles.SelectedValue.ToString();
           //     reUsers.DataSource = getUserList("", profile);
                rieUser.ValueMember = "USERID";
                rieUser.DisplayMember = "NAME";
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _UpdateData = new MWDataManager.clsDataAccess();
            _UpdateData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _UpdateData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _UpdateData.queryReturnType = MWDataManager.ReturnType.DataTable;
            StringBuilder sb = new StringBuilder();

            foreach (DataRow r in dsPrePlanningNotification.Sections.Rows)
            {

                sb.Append(String.Format("UPDATE PrePlanning_Notification_Security SET CPM_UserID1 = '{0}',", r["CPM_UserID1"]));
                sb.Append(String.Format("CPM_UserID2 = '{0}',", r["CPM_UserID2"]));
                _UpdateData.SqlStatement = "UPDATE PrePlanning_Notification_Security SET CPM_UserID1 = '" + r["CPM_UserID1"].ToString() + "'," +
                                           "                                             CPM_UserID2 = '" + r["CPM_UserID2"].ToString() + "'," +
                                           "                                             CrewChange = '" + r["CrewChange"].ToString() + "'," +
                                           "                                             CallChange = '" + r["CallChange"].ToString() + "'," +
                                           "                                             StopWPChange = '" + r["StopWPChange"].ToString() + "'," +
                                           "                                             NewWPChange = '" + r["NewWPChange"].ToString() + "'," +
                                           "                                             WPMove = '" + r["WPMove"].ToString() + "', " +
                                           "                                             StartWPChange='" + r["StartWPChange"].ToString () + "', " +
                                            "                                             MiningMethodChange='" + r["MiningMethodChange"].ToString() + "' " +
                                           "WHERE Shaft = '" + r["Shaft"].ToString() + "' and " +
                                           "      Section = '" + r["Section"].ToString() + "' and " +
                                           "      Department =  '" + r["Department"].ToString() + "' ";

                _UpdateData.ExecuteInstruction();

            }

            foreach (DataRow r in dsPrePlanningNotification.Shafts.Rows)
            {


                _UpdateData.SqlStatement = "UPDATE PrePlanning_Notification_Security SET CPM_UserID1 = '" + r["CPM_UserID1"].ToString() + "'," +
                                           "                                             CPM_UserID2 = '" + r["CPM_UserID2"].ToString() + "'," +
                                           "                                             CrewChange = '" + r["CrewChange"].ToString() + "'," +
                                           "                                             CallChange = '" + r["CallChange"].ToString() + "'," +
                                           "                                             StopWPChange = '" + r["StopWPChange"].ToString() + "'," +
                                           "                                             NewWPChange = '" + r["NewWPChange"].ToString() + "'," +
                                           "                                             WPMove = '" + r["WPMove"].ToString() + "', " +
                                           "                                             StartWPChange='" + r["StartWPChange"].ToString() + "', " +
                                             "                                             MiningMethodChange='" + r["MiningMethodChange"].ToString() + "' " +
                                           "WHERE Shaft = '" + r["Shaft"].ToString() + "' and " +
                                           "      Section = '" + r["Section"].ToString() + "' and " +
                                           "      Department =  '" + r["Department"].ToString() + "' ";

                _UpdateData.ExecuteInstruction();

            }

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Notification Data was saved", Color.CornflowerBlue);

        }

        private void dsPrePlanningNotificationBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void frmPrePlanningNotification_Load(object sender, EventArgs e)
        {
            _UpdateSecurity.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _UpdateSecurity.SqlStatement = "sp_Update_Replanning_Notification_Table";
            _UpdateSecurity.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

            SqlParameter[] _paramCollection =  
                            {
                             _UpdateSecurity.CreateParameter("@Prodmonth", SqlDbType.Int, 0,TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth),   
                            };
            _UpdateSecurity.ParamCollection = _paramCollection;
            _UpdateSecurity.ExecuteInstruction();


            _UserList.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _UserList.SqlStatement = "sp_Get_User_List";
            _UserList.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            _UserList.queryReturnType = MWDataManager.ReturnType.DataTable;
            //SqlParameter[] _paramCollection2 =  
            //                {
            //                 _UserList.CreateParameter(null),   
            //                };
            //_UserList.ParamCollection = _paramCollection2;
            _UserList.ExecuteInstruction();

            rieUser.DataSource = _UserList.ResultsDataTable;
            rieUser.ValueMember = "UserID";
            rieUser.DisplayMember = "UserName";

            gcSettings.ForceInitialize();
            string theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            SqlDataAdapter oleDBAdapter1 = new SqlDataAdapter("DECLARE @sysDB varchar(max), @theSQL varchar(max) SET @sysDB = (SELECT [Syncro_Database] FROM [dbo].[SYSSET]) SET @theSQL = ' SELECT DISTINCT  DepartmentID Department,[Description]  FROM PrePlanning_Notification_Security a INNER JOIN ' + @sysDB + '[dbo].[tblDepartments] b on a.Department = b.[DepartmentID]  ORDER BY b.[Description]' exec (@theSQL) ", theConnection);
            SqlDataAdapter oleDBAdapter2 = new SqlDataAdapter("SELECT *  FROM PrePlanning_Notification_Security WHERE Section = 'NONE' ORDER BY Shaft ", theConnection);
            SqlDataAdapter oleDBAdapter3 = new SqlDataAdapter("SELECT * FROM PrePlanning_Notification_Security WHERE Section <> 'NONE' ORDER BY Section ", theConnection);

            //SqlDataAdapter oleDBAdapter1 = new SqlDataAdapter("SELECT DISTINCT TemplateName    FROM PlanProt_ApproveUsers ORDER BY Department ", TUserInfo.ConnectionString);
            //SqlDataAdapter oleDBAdapter2 = new SqlDataAdapter("SELECT *  FROM PlanProt_ApproveUsers WHERE Section = 'NONE' ORDER BY Shaft ", TUserInfo.ConnectionString);
            //SqlDataAdapter oleDBAdapter3 = new SqlDataAdapter("SELECT * FROM PlanProt_ApproveUsers WHERE Section <> 'NONE' ORDER BY Section ", TUserInfo.ConnectionString);



            //oleDBAdapter1.AcceptChangesDuringUpdate = true;
            //oleDBAdapter2.AcceptChangesDuringUpdate = true;
            //oleDBAdapter3.AcceptChangesDuringUpdate = true;
            //oleDBAdapter1.AcceptChangesDuringFill = true;
            //oleDBAdapter2.AcceptChangesDuringFill = true;
            //oleDBAdapter3.AcceptChangesDuringFill = true;
            oleDBAdapter1.Fill(dsPrePlanningNotification.Department);
            oleDBAdapter2.Fill(dsPrePlanningNotification.Shafts);
            oleDBAdapter3.Fill(dsPrePlanningNotification.Sections);

            gvShafts.ViewCaption = "Shafts";



            //MWDataManager.clsDataAccess _UserProfiles = new MWDataManager.clsDataAccess();
            //_UserProfiles.ConnectionString = TUserInfo.ConnectionString;
            //_UserProfiles.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_UserProfiles.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_UserProfiles.SqlStatement = "SELECT USERPROFILEID FROM dbo.UserProfiles";
            //_UserProfiles.ExecuteInstruction();

            //lbUserProfiles.DataSource = _UserProfiles.ResultsDataTable;
            //lbUserProfiles.ValueMember = "USERPROFILEID";
            //lbUserProfiles.DisplayMember = "USERPROFILEID";

            //_UserProfiles.ConnectionString = TUserInfo.ConnectionString;
            //_UserProfiles.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_UserProfiles.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_UserProfiles.SqlStatement = "SELECT PPNS.*,0 Updated FROM PrePlanning_Notification_Security PPNS";
            //_UserProfiles.ExecuteInstruction();

            //gcSettings.DataSource = _UserProfiles.ResultsDataTable;
        }
    }
}