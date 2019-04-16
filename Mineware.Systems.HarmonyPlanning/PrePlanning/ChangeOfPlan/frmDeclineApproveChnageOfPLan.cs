using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    public partial class frmDeclineApproveChnageOfPLan : DevExpress.XtraEditors.XtraForm
    {
        public  bool canceledAction = true;
        private int theApproveRequestID;
        //public  string cancelbutton;
        public string theSystemDBTag = "";
        public TUserCurrentInfo UserCurrentInfo;

        public frmDeclineApproveChnageOfPLan()
        {
            InitializeComponent();
        }

        public void Decline(int ApproveRequestID,string ChangeType, string WPName)
        {
            theApproveRequestID = ApproveRequestID;
            Text = "DECLINE";
            labelControl1.Text = ChangeType + ":  " + WPName;
            ShowDialog();
            if (canceledAction == false)
            {
               // theresult = canceledAction;
                MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
                _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _SaveData.SqlStatement = "[sp_RevisedPlanning_ApproveDecline]";
                _SaveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                SqlParameter[] _paramCollection = 
                    {
                        _SaveData.CreateParameter("@ApproveRequestID", SqlDbType.Int,0,Convert.ToInt32(theApproveRequestID)),
                        _SaveData.CreateParameter("@RequestStauts", SqlDbType.Int, 0,Convert.ToInt32(2)),
                        _SaveData.CreateParameter("@UserID", SqlDbType.VarChar, 50,TUserInfo.UserID),
                        _SaveData.CreateParameter("@Comments", SqlDbType.VarChar, 255,editComments.Text),
                    };

                _SaveData.ParamCollection = _paramCollection;
                _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                _SaveData.ExecuteInstruction();

                //ucRequeststatus ucreq = new ucRequeststatus();
                
            }
            else
            {
                //theresult = canceledAction;
            }

            
        }
        

        public void Approve(int ApproveRequestID, string ChangeType, string WPName)
        {
            theApproveRequestID = ApproveRequestID;
            Text = "APPROVE";
            labelControl1.Text = ChangeType + ":  "+ WPName;
            ShowDialog();

            if (canceledAction == false)
            {


                MWDataManager.clsDataAccess _SaveData = new MWDataManager.clsDataAccess();
                _SaveData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _SaveData.SqlStatement = "[sp_RevisedPlanning_ApproveDecline]";
                _SaveData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                SqlParameter[] _paramCollection = 
                    {
                        _SaveData.CreateParameter("@ApproveRequestID", SqlDbType.Int,0,Convert.ToInt32(theApproveRequestID)),
                        _SaveData.CreateParameter("@RequestStauts", SqlDbType.Int, 0,Convert.ToInt32(1)),
                        _SaveData.CreateParameter("@UserID", SqlDbType.VarChar, 50,TUserInfo.UserID),
                        _SaveData.CreateParameter("@Comments", SqlDbType.VarChar, 255,editComments.Text),
                    };

                _SaveData.ParamCollection = _paramCollection;
                _SaveData.queryReturnType = MWDataManager.ReturnType.longNumber;
                clsDataResult DataRe = _SaveData.ExecuteInstruction();
                if (DataRe.success == false)
                {
                    //MessageBox.Show(DataRe.Message);
                }   
            }
            else
            {
                
            }

            //ucRequeststatus ucr = new ucRequeststatus();
            //ucr.gridControl1.RefreshDataSource();
            //ucr.gridControl1.Refresh();
            

            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canceledAction = true;
            Close();


        }

        private void frmDeclineApproveChnageOfPLan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (canceledAction == false)
            {
                if (editComments.Text == "")
                {
                    e.Cancel = true;
                    MessageBox.Show("You need to provide a message!");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManFixFigures = new MWDataManager.clsDataAccess();
            _dbManFixFigures.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManFixFigures.SqlStatement = " Exec sp_FixTons ";
            _dbManFixFigures.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManFixFigures.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManFixFigures.ExecuteInstruction();
            //  ucRequeststatus ucr = new ucRequeststatus();
            // ucr.loadRequestedData();

            ///Make Fixes to Data

            canceledAction = false;
          
            Close();
        }
    }
}