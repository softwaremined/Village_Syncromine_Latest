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

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class frmAddOldWp : DevExpress.XtraEditors.XtraForm
    {

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public frmAddOldWp()
        {
            InitializeComponent();
        }



        private void frmAddOldWp_Load(object sender, EventArgs e)
        {


            LoadVampWP();
        }


        public void LoadVampWP()
        {
            MWDataManager.clsDataAccess _dbManVampWP = new MWDataManager.clsDataAccess();
            _dbManVampWP.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManVampWP.SqlStatement = " select Description from WORKPLACE_Total " +

                                        "where Description not in (select Description from WORKPLACE) " +
                                        " and ((Activity = 1 and EndTypeID <> '') or (Activity <> 1)) " +
                                        "order by Description ";
            _dbManVampWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManVampWP.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManVampWP.ExecuteInstruction();

            DataTable dtVampWp = new DataTable();

            dtVampWp = _dbManVampWP.ResultsDataTable;

            BindingSource bswp = new BindingSource();

            bswp.DataSource = dtVampWp;

            VampwplistBox.Items.Clear();
            VampwplistBox.DataSource = bswp;
            VampwplistBox.DisplayMember = "Description";
        }

        private void RockEnginSavebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveVampWP();
        }

        void SaveVampWP()
        {
            if (NewWpLbl.Visible == false)
            {
                MessageBox.Show("Please Select a workplace to activiate", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (WPlinetxt.Text == "")
            //{
            //    MessageBox.Show("Please Eneter a Line for the workplace", "Enter Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}




            MWDataManager.clsDataAccess _dbManVampCheck = new MWDataManager.clsDataAccess();
            _dbManVampCheck.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

            _dbManVampCheck.SqlStatement = "select CONVERT(varchar(1),activity) activity from  WORKPLACE where Description = '" + NewWpLbl.Text + "' ";
            _dbManVampCheck.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManVampCheck.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManVampCheck.ExecuteInstruction();

            DataTable DataVamp = _dbManVampCheck.ResultsDataTable;


            string act = DataVamp.Rows[0]["activity"].ToString();

            if (act != "1")
            {
                _dbManVampCheck.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManVampCheck.SqlStatement = "Insert into workplace " +
                                              " select 'S'+substring(convert(varchar(10),1+b  + 100000),2,5) workplaceid, oreflowid, r.reefid, endtypeid, w.description, '0' act, " +
                                              "'R' reefwaste,  case when  gmsiwpid like '%200' then 'GN' else null end as stpcode, null, null, Line, w.direction, null, null, null, 0, 0, GMSIWPID, null  from WORKPLACE w, reef r, " +
                                              "(select max(convert(int,substring(workplaceid,2,5))) b from workplace_total where Activity = 0) b " +
                                              "where w.reefid = r.shortdesc and w.Description = '" + NewWpLbl.Text + "'  and GMSIWPID  = (select max(GMSIWPID) from WORKPLACE where Description = '" + NewWpLbl.Text + "') ";
                _dbManVampCheck.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVampCheck.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVampCheck.ExecuteInstruction();

            }
            else
            {
                _dbManVampCheck.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbManVampCheck.SqlStatement = "Insert into workplace " +
                                              "select 'D'+substring(convert(varchar(10),1+b  + 100000),2,5)  workplaceid, oreflowid, reefid, endtypeid, description, 1 act, " +
                                              " reefwaste,  case when  wpid like '%200' then 'GN' else null end as stpcode ,EndWidth , EndHeight, Line, direction, null, null, null, 0, 0, wpid, null  " +
                                              "  from ( " +
                                              " select row_number()over (order by w.description) a, " +
                                              "  oreflowid, r.reefid, e.endtypeid, w.description, w.GMSIWPID wpid, w.Line, w.direction, e.EndHeight, e.EndWidth, e.ReefWaste  " +

                                              "  from WORKPLACE w, reef r, ENDTYPE e " +
                                              " where  w.reefid = r.shortdesc and w.EndTypeID = e.ProcessCode and  w.activity = 1  and w.Description = '" + NewWpLbl.Text + "' and GMSIWPID  = (select max(GMSIWPID) from WORKPLACE where Description = '" + NewWpLbl.Text + "')) a, " +

                                              " (select max(convert(int,substring(workplaceid,2,5))) b from workplace where Activity = 1) b ";
                _dbManVampCheck.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManVampCheck.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManVampCheck.ExecuteInstruction();
            }

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "saved", Color.CornflowerBlue);
        }

    }
}