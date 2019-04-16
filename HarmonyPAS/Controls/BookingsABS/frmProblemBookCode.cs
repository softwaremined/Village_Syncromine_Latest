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
    public partial class frmProblemBookCode : DevExpress.XtraEditors.XtraForm
    {
        public frmProblemBookCode()
        {
            InitializeComponent();
        }

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        private void Closebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmProblemBookCode_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);          
               
            _dbMan.SqlStatement = "Select distinct Description,ProblemID from CODE_PROBLEM where ProblemID = '"+lblProblemID.Text+"' and activity = '"+lblAcitvity.Text+"'   ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;


            foreach (DataRow dr in dt.Rows)
            {
                txtDescription.Text = dr["Description"].ToString();
                txtProblemID.Text = dr["ProblemID"].ToString();
            }
        }

        private void Savebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}