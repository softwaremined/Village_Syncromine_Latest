using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning;
using Mineware.Systems.Planning.PrePlanning;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class frmManageChangeOfPlann : Form 
    {
        private MWDataManager.clsDataAccess _RequestList = new MWDataManager.clsDataAccess();
        public frmManageChangeOfPlann()
        {
            InitializeComponent();

            loadRequestList();
        }

        public void loadRequestList()
        {

            _RequestList.ConnectionString = TUserInfo.Connection;
            _RequestList.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _RequestList.queryReturnType = MWDataManager.ReturnType.DataTable;
            _RequestList.SqlStatement = "sp_RequestPlanningUserList '" + TUserInfo.UserID + "'";
            _RequestList.ExecuteInstruction();
            if (_RequestList.ResultsDataTable.DefaultView .Count  == 0)
            {
                Visible = false;
            }
            else
            {
                gcRequestList.DataSource = _RequestList.ResultsDataTable;
                ShowDialog();
            }
        }

        private void gvRequests_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
    
        {
            _RequestList.ResultsDataTable.AcceptChanges();
            int DSRow = gvRequests.GetDataSourceRowIndex(e.RowHandle);
            if (DSRow >= 0)
            {
                focusControle.Controls.Clear();
                ReplanningTypes ReplanningTypes = new ReplanningTypes();
                replanningType theType = ReplanningTypes.getReplanningType(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeID"].ToString()));
                switch (theType)
                {
                    case replanningType.rpStopWp:
                        btnApprove.Enabled = true;
                        ucStopWorkplace ucStopWorkplace = new ucStopWorkplace();
                        focusControle.Controls.Clear();
                        ucStopWorkplace.Parent = focusControle;
                        ucStopWorkplace.Dock = DockStyle.Fill;
                        ucStopWorkplace.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
                        break;
                    case replanningType .rpCrewChnages :
                        btnApprove.Enabled = true;
                        ucCrewMinerChange ucCrewMinerChange = new ucCrewMinerChange();
                        focusControle.Controls.Clear();
                        ucCrewMinerChange .Parent  = focusControle;
                        ucCrewMinerChange.Dock = DockStyle.Fill;
                        ucCrewMinerChange.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
                        break;
                    case replanningType.rpNewWP :
                        btnApprove.Enabled = true;
                       ucAddWorkplace  ucAddWorkplace = new ucAddWorkplace ();
                        focusControle.Controls.Clear();
                        ucAddWorkplace . Parent = focusControle;
                       ucAddWorkplace . Dock = DockStyle.Fill;
                       ucAddWorkplace . LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
                        break;

                    case replanningType .rpCallCahnges :
                        btnApprove.Enabled = true;
                        ucPlanningValueChanges ucPlanningValueChanges = new ucPlanningValueChanges();
                        focusControle.Controls.Clear();
                        ucPlanningValueChanges.Parent = focusControle;
                        ucPlanningValueChanges.Dock = DockStyle.Fill;
                        ucPlanningValueChanges.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
                        break;

                    case replanningType .rpMoveWP :
                        btnApprove.Enabled = true;
                        MoveBookings ucMoveBookings = new MoveBookings();
                        focusControle.Controls.Clear();
                        ucMoveBookings.Parent = focusControle;
                        ucMoveBookings.Dock = DockStyle.Fill;
                        ucMoveBookings.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
                        break;

                    case replanningType.rpStartWP :
                        btnApprove.Enabled = true;
                        StartWorkplace StartWP = new StartWorkplace();
                        focusControle.Controls.Clear();
                        StartWP.Parent = focusControle;
                        StartWP.Dock = DockStyle.Fill;
                        StartWP.LoadDetails(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ChangeRequestID"].ToString()));
                        break;
                }

            
            }
            else
            {
                focusControle.Controls.Clear();
                Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            _RequestList.ResultsDataTable.AcceptChanges();
            gcRequestList.DataSource = _RequestList.ResultsDataTable;

            gvRequests.RefreshData();
            frmDeclineApproveChnageOfPLan frmDeclineApproveChnageOfPLan = new frmDeclineApproveChnageOfPLan();
            int DSRow = gvRequests.FocusedRowHandle;
            frmDeclineApproveChnageOfPLan.Decline(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ApproveRequestID"].ToString()),_RequestList .ResultsDataTable .Rows[DSRow ]["ChangeType"].ToString (), _RequestList .ResultsDataTable .Rows[DSRow ]["WPName"].ToString ());
            if (frmDeclineApproveChnageOfPLan.canceledAction == true)
            {
                gcRequestList.Refresh();
                gvRequests.RefreshData();
                // gvRequests1.DeleteRow(gvRequests1.FocusedRowHandle);
                //gvRequests.DeleteRow(DSRow);
                //focusControle1.Controls.Clear();
                //  gcAppDecList.Refresh();
                //gridControl1.Refresh();
                btnDecline.Enabled = true;
                
            }
            else
            {
                gcRequestList.Refresh();
                gvRequests.RefreshData();
                gvRequests.DeleteRow(gvRequests.FocusedRowHandle);
                focusControle.Controls.Clear();
                btnDecline.Enabled = false;
               
            }
           
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManFixFigures = new MWDataManager.clsDataAccess();
            _RequestList.ConnectionString = TUserInfo.Connection;
            _dbManFixFigures.SqlStatement = " Exec sp_FixTons ";
            _dbManFixFigures.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManFixFigures.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManFixFigures.ExecuteInstruction();



            _RequestList.ResultsDataTable.AcceptChanges();
            gcRequestList.DataSource = _RequestList.ResultsDataTable;

            gvRequests.RefreshData();
            frmDeclineApproveChnageOfPLan frmDeclineApproveChnageOfPLan = new frmDeclineApproveChnageOfPLan();
            int DSRow = gvRequests.FocusedRowHandle;
            frmDeclineApproveChnageOfPLan.Approve(Convert.ToInt32(_RequestList.ResultsDataTable.Rows[DSRow]["ApproveRequestID"].ToString()), _RequestList.ResultsDataTable.Rows[DSRow]["ChangeType"].ToString(), _RequestList.ResultsDataTable.Rows[DSRow]["WPName"].ToString());
            if (frmDeclineApproveChnageOfPLan.canceledAction == true)
            {
               
                gcRequestList.Refresh();
                gvRequests.RefreshData();
                // gvRequests1.DeleteRow(gvRequests1.FocusedRowHandle);
                //gvRequests.DeleteRow(DSRow);
                //focusControle1.Controls.Clear();
                //  gcAppDecList.Refresh();
                //gridControl1.Refresh();
                btnApprove.Enabled = true;

            }
            else
            {
               
                gcRequestList.Refresh();
                gvRequests.RefreshData();
                gvRequests.DeleteRow(gvRequests.FocusedRowHandle);
                focusControle.Controls.Clear();
                btnApprove.Enabled = false;

            }
           
        }

        private void gcRequestList_Click(object sender, EventArgs e)
        {

        }

        private void frmManageChangeOfPlann_Load(object sender, EventArgs e)
        {

        }
    }
}