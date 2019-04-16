using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Global;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Mineware.Systems.Planning;
using Mineware.Systems.Planning.PlanningProtocolTemplates;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.MiningMethodsInPlanningSetup
{
    public partial class ucMiningMethodsPlanningSetup : ucBaseUserControl
    {
        DataTable dt = new DataTable();
        public ucMiningMethodsPlanningSetup()
        {

            InitializeComponent();
        }

        private void ucMiningMethodsPlanningSetup_Load(object sender, EventArgs e)
        {
             MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "select a.TargetID, a.Activity,b.ShowInPlanning  ShowInPlanning1,b.RequireDocuments RequireDocuments1, TheActivity = Case when a.Activity in (0,3) then 'Stoping' else 'Development' end, " +
                                  "cast(a.TargetID as Varchar(10))+':'+a.Description Method, " +
                                  "ShowInPlanning=case when b.ShowInPlanning ='Y' then cast(0 as bit) ELSE cast(1 as bit) END, RequireDocuments=case when b.RequireDocuments ='Y' THEN cast( 0 as bit) ELSE cast( 1 as bit) END from   " +
                                  " " + TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Bonus_Database + " dbo.Bonus_poolDEfaults a left join BONUS_POOL_VALIDATIONS b on " +
                                  "a.TargetID =b.TargetID and " +
                                  "a.Activity =b.Activity order by TheActivity desc,a.TargetID asc " ;
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement ;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
           
            dt = _dbMan.ResultsDataTable;
            gridControl1.DataSource = dt;
            

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {

                MWDataManager.clsDataAccess _dbManDelete = new MWDataManager.clsDataAccess();
                _dbManDelete.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDelete.SqlStatement = "Delete from BONUS_POOL_VALIDATIONS where TargetID = '" + dr["TargetID"].ToString() + "' and Activity = '" + dr["Activity"].ToString () + "'";
                _dbManDelete.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDelete.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDelete.ExecuteInstruction();

                MWDataManager.clsDataAccess _dbManLastMonthWP = new MWDataManager.clsDataAccess();
                _dbManLastMonthWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManLastMonthWP.SqlStatement = "Insert into PlanProt_ProfileAccess (DepartmentID ,FullAccess ,ReadOnlyAccess, TemplateID)  values ('" + dr["ProfileName"].ToString() + "' , '" + dr["FullAccess"] + "' , '" + dr["ReadOnlyAccess"] + "', '" + TempID + "')";
                //if (dr["FullAccess"].ToString() == "False")
                //{
                //    _dbManLastMonthWP.SqlStatement = "Insert into PlanProt_ProfileAccess (DepartmentID ,TemplateID,AccessLevel)  values ('" + dr["DepartmentID"].ToString() + "' , '" + TempID + "' ,  'True')";
                //}
                //else
                //{
                //    _dbManLastMonthWP.SqlStatement = "Insert into PlanProt_ProfileAccess (DepartmentID ,TemplateID,AccessLevel)  values ('" + dr["DepartmentID"].ToString() + "' , '" + TempID + "' ,  'False')";
                //}
                char ShowInPlanning1;
                char RequireDocuments1;
                if(dr ["ShowInPlanning"].ToString () == "True")
                {
                    ShowInPlanning1 = 'N';
                }
                else 
                {
                    ShowInPlanning1 = 'Y';
                }

                if (dr["RequireDocuments"].ToString() == "True")
                {
                    RequireDocuments1 = 'N';
                }
                else
                {
                    RequireDocuments1 = 'Y';
                }
                _dbManLastMonthWP.SqlStatement = "Insert into BONUS_POOL_VALIDATIONS (TargetID ,Activity,ShowInPlanning,RequireDocuments)  values ('" + Convert .ToInt32 ( dr["TargetID"])+ "' , '" + Convert.ToInt32 ( dr["Activity"]) + "' ,  '" + ShowInPlanning1 + "', '" + RequireDocuments1 + "')";


                _dbManLastMonthWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManLastMonthWP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManLastMonthWP.ExecuteInstruction();



            }
        }
    }
}
