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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using Mineware.Systems.Global;
using DevExpress.XtraGrid.Views.Base;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class frmGeologyProp : DevExpress.XtraEditors.XtraForm
    {
        // public frmGeologyMain MainFrm1;
        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;
        DataTable Holes = new DataTable();

        public frmGeologyProp()
        {
            InitializeComponent();
           // MainFrm1 = _MainFrm1;
        }

        private void frmGeologyProp_Load(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbMan1.SqlStatement = "select nodeid, MachineNo mm, Workplace, HoleNo, Length, SDate, PrevMachineNo, ";
            _dbMan1.SqlStatement = _dbMan1.SqlStatement + "AddDelay, CONVERT(VARCHAR(11),SDate,106)  sss from [dbo].[tbl_GeoScience_PlanLongTerm]  ";
            _dbMan1.SqlStatement = _dbMan1.SqlStatement + "where sdate is not null and MachineNo = '" + Machlabel.Text + "' Order by sdate desc ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            Holes = _dbMan1.ResultsDataTable;

            PrevWPList.Items.Add("Machine Commencement");

            foreach (DataRow row in Holes.Rows)
            {
                PrevWPList.Items.Add(((row["Workplace"].ToString() + "                                                         ").Substring(0, 25) + row["HoleNo"].ToString() + "                              ").Substring(0, 50));
            }

            if (Holes.Rows.Count > 0)
            {
                PrevWPList.SelectedIndex = 1;
            }
        }

        private void PrevWPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PrevWPList.Text != "Machine Commencement")
                PevHolelabel.Text = PrevWPList.Text.Substring(24, 26).Trim();
            else
                PevHolelabel.Text = "Machine Commencement";
        }

        private void PevHolelabel_TextChanged(object sender, EventArgs e)
        {
            Starttm.Enabled = true;
            if (PrevWPList.Text != "Machine Commencement")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbMan1.SqlStatement = "select compdate from [dbo].[tbl_GeoScience_PlanLongTerm] ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "where machineno = '" + Machlabel.Text + "' and holeno = '" + PevHolelabel.Text + "' ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable PevHoles = _dbMan1.ResultsDataTable;

                if (PevHoles.Rows.Count > 0)
                {
                    Starttm.Value = Convert.ToDateTime(PevHoles.Rows[0]["compdate"].ToString()).AddDays(1);
                    Starttm.Enabled = false;

                }

            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbMan1.SqlStatement = "update [dbo].[tbl_GeoScience_PlanLongTerm] set sdate = '" + String.Format("{0:yyyy-MM-dd}", Starttm.Value) + "'";
            _dbMan1.SqlStatement = _dbMan1.SqlStatement + " ,adddelay = '" + Delaytxt.Text + "', prevworkplace =  '" + PevHolelabel.Text + "' ";
            _dbMan1.SqlStatement = _dbMan1.SqlStatement + "where machineno = '" + Machlabel.Text + "' and workplace = '" + WPlabel.Text + "'  and holeno = '" + Holelabel.Text + "' ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Info", "Data saved successfuly.", Color.CornflowerBlue);
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbMan1.SqlStatement = "update [dbo].[tbl_GeoScience_PlanLongTerm] set sdate = '" + String.Format("{0:yyyy-MM-dd}", Starttm.Value) + "'";
            _dbMan1.SqlStatement = _dbMan1.SqlStatement + " ,adddelay = '" + Delaytxt.Text + "', prevworkplace =  '" + PevHolelabel.Text + "' ";
            _dbMan1.SqlStatement = _dbMan1.SqlStatement + "where machineno = '" + Machlabel.Text + "' and workplace = '" + WPlabel.Text + "'  and holeno = '" + Holelabel.Text + "' ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();
            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Info", "Data saved successfuly.", Color.CornflowerBlue);
            this.Close();
        }
    }
}
