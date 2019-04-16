using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.BookingsABS
{    

    public partial class frmCloseActions : DevExpress.XtraEditors.XtraForm
    {


        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public frmCloseActions()
        {
            InitializeComponent();
        }

        private void Savebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

            if (lblActionType.Text == "Normal Action")
            {
                if (cbxResolved.Checked == true)
                {
                    _dbMan.SqlStatement = " " +
                        " update tbl_Incidents set Responsible_Person_Feedback = '" + Notestxt.Text + "' , Action_Status = 'Closed' , \r\n" +
                        " Action_Closed_By = '" + TUserInfo.UserID + "' , Action_Close_Date =  getdate(),Complete_Date =  getdate(),Action_Complete = '100',Action_Progress = '100'   \r\n" +
                        " where isnull([Mineware_Action_ID],'') + convert(varchar(300),isnull( [Pivot_Action_ID], '')) = '" + lblActionID.Text + "'   ";
                }

                if (cbxResolved.Checked == false)
                {
                    _dbMan.SqlStatement = " " +
                        " update tbl_Incidents set Responsible_Person_Feedback = '" + Notestxt.Text + "'   \r\n" +
                        " where isnull([Mineware_Action_ID],'') + convert(varchar(300),isnull( [Pivot_Action_ID], '')) = '" + lblActionID.Text + "'   ";
                }
            }



            if (lblActionType.Text == "Pivot Action")
            {
                _dbMan.SqlStatement = " " +
                        " insert into tbl_Incidents_Close_Request   \r\n" +
                        " (Operation,Workplace,Pivot_Action_ID,Responsible_Person_Feedback,Action_Closed_By,Action_Close_Date)   \r\n" +
                        " values ('RE','"+lblWorkplace.Text+"','"+lblPivotActionID.Text+"','"+Notestxt.Text+"','"+TUserInfo.UserID+ "',getdate())   ";
            }


           
            
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbMan.ExecuteInstruction();

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Action Closed", Color.CornflowerBlue);
            this.Close();
        }

        private void frmCloseActions_Load(object sender, EventArgs e)
        {
            if (lblActionType.Text == "Pivot Action")
            {
                cbxResolved.Visible = false;
            }
        }

        private void Closebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}