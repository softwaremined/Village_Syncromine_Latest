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
using FastReport;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    public partial class frmAddExtendedBreak : DevExpress.XtraEditors.XtraForm
    {
        public frmAddExtendedBreak()
        {
            InitializeComponent();
        }

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        private void frmAddExtendedBreak_Load(object sender, EventArgs e)
        {

            MWDataManager.clsDataAccess _dbManExtBreaks = new MWDataManager.clsDataAccess();
            _dbManExtBreaks.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManExtBreaks.SqlStatement = " select * from [dbo].[tbl_ExtendedBreakSetup]  ";
            _dbManExtBreaks.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManExtBreaks.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManExtBreaks.ExecuteInstruction();

            DataTable dtExtBreak = _dbManExtBreaks.ResultsDataTable;

            //WPNotestxt.Text = "";

            //foreach (DataRow dr in dtExtBreak.Rows)
            //{
            //    WPNotestxt.Text = dr["notes"].ToString();
            //}


            treeView1.Nodes.Clear();

            TreeNode tNode;

            tNode = treeView1.Nodes.Add("Total Mine");

            treeView1.SelectedNode = tNode;

            MWDataManager.clsDataAccess _dbMansb = new MWDataManager.clsDataAccess();
            _dbMansb.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbMansb.SqlStatement = "select s2.SectionID +':'+s2.Name mo  " +
                                    " from PLANNING p, section s, section s1, section s2 , section s3 " +
                                    " where p.SectionID = s.SectionID and p.Prodmonth = s.Prodmonth " +
                                    " and s.ReportToSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth " +
                                    " and s1.ReportToSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth " +
                                    " and s2.ReportToSectionid = s3.SectionID and s2.Prodmonth = s3.Prodmonth " +
                                    " and p.prodmonth = (select max(CurrentProductionMonth) from SYSSET)  " +
                                    " group by s2.SectionID ,s2.Name " +
                                    " order by s2.SectionID  ";
            _dbMansb.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMansb.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMansb.ExecuteInstruction();

            DataTable Data2 = _dbMansb.ResultsDataTable;

            for (int mo = 0; mo < Data2.Rows.Count; mo++)
            {
                treeView1.Nodes[0].Nodes.Add(Data2.Rows[mo]["mo"].ToString());
            }

            treeView1.ExpandAll();
            //treeView1.Selected;

            InitiateDateEdit.EditValue = String.Format("{0:dd-MMM-yyyy}", StartDateDTP.Value.AddDays(-Convert.ToInt64(DefDaysTxt.Text)));
        }

        private void Closebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeView1.SelectedNode.Text == "")
            {
                MessageBox.Show("Please select a section.", "Insufficient Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DescTxt.Focus();
                return;
            }


            string StopNote = "N";

            if (StopNoteCB.Checked == true)
            {
                StopNote = "Y";
            }

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbMan1.SqlStatement = " select * from [dbo].[tbl_ExtendedBreakSetup]  where section = '" + treeView1.SelectedNode.Text + "' and [Description] = '" + DescTxt.Text + "' " +
                "  ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            if (_dbMan1.ResultsDataTable.Rows.Count > 0)
            {

                MessageBox.Show("There is already data saved for this section.", "Section Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //DescTxt.Focus();
                return;
            }

            else
            {

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbMan.SqlStatement = " insert into [dbo].[tbl_ExtendedBreakSetup] VALUES ( '" + treeView1.SelectedNode.Text + "' , '" + String.Format("{0:yyyy-MM-dd}", StartDateDTP.Value.AddDays(-Convert.ToInt64(DefDaysTxt.Text))) + "', '" + DefDaysTxt.Text + "', '" + String.Format("{0:yyyy-MM-dd}", StartDateDTP.Value) + "', '" + String.Format("{0:yyyy-MM-dd}", EndDateDTP.Value) + "' " +
                    " , '" + DescTxt.Text + "', '" + StopNote + "' ) ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();


                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Extendead Break Added ", Color.CornflowerBlue);

                Close();

                //Valid = "Y";
                //SysAdminFrm.LoadExtendedBreaks();
            }
        }

        private void StartDateDTP_ValueChanged(object sender, EventArgs e)
        {
            InitiateDateEdit.EditValue = String.Format("{0:dd-MMM-yyyy}", StartDateDTP.Value.AddDays(-Convert.ToInt64(DefDaysTxt.Text)));
        }

        private void DefDaysTxt_TextChanged(object sender, EventArgs e)
        {
            InitiateDateEdit.EditValue = String.Format("{0:dd-MMM-yyyy}", StartDateDTP.Value.AddDays(-Convert.ToInt64(DefDaysTxt.Text)));
        }

        private void EndDateDTP_ValueChanged(object sender, EventArgs e)
        {
            InitiateDateEdit.EditValue = String.Format("{0:dd-MMM-yyyy}", StartDateDTP.Value.AddDays(-Convert.ToInt64(DefDaysTxt.Text)));
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SectionEdit.EditValue = treeView1.SelectedNode.Text;
        }
    }
}