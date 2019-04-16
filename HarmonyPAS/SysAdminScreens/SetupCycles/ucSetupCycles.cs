using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.XtraGrid.EditForm.Helpers;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SetupCycles
{
    public partial class ucSetupCycles : ucBaseUserControl
    {

        clsSetupCycles _clsSetupCycles = new clsSetupCycles();

        bool IsCycleCode = false;

        public ucSetupCycles()
        {
            InitializeComponent();
        }

        private void ucSetupCycles_Load(object sender, EventArgs e)
        {
            _clsSetupCycles.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            dtCopyAndPaste.ColumnCount = 24;
            dtCopyAndPaste.RowCount = 1;

            for (int i = 0; i < dtCopyAndPaste.ColumnCount; i++)
            {
                dtCopyAndPaste.Columns[i].Width = 45;
            }
            

            rpgWorktypeCodes.Visible = false;

            xtraTabControl1.TabPages[0].Text = "         Cycle Codes         ";
            xtraTabControl1.TabPages[1].Text = "            Stoping          ";
            xtraTabControl1.TabPages[2].Text = "        Ledging Cycle        ";
            xtraTabControl1.TabPages[3].Text = "         Developement        ";
            xtraTabControl1.TabPages[4].Text = "    MO Cycle Configuration   ";


            gcWorkTypes.DataSource = _clsSetupCycles.LoadCycleCodes();

            colCodeDescription.FieldName = "Description";
            colCodeWorkType.FieldName = "CycleCode";

            DataTable dtCodes = _clsSetupCycles.LoadCycleCodes();

            foreach (DataRow dr in dtCodes.Rows)
            {
                if(dr["Developement"].ToString() == "Y")
                    lbxCodeDev.Items.Add(dr["CycleCode"].ToString() + ":" + dr["Description"].ToString());
                if (dr["Stoping"].ToString() == "Y")
                    lbxCodeStp.Items.Add(dr["CycleCode"].ToString() + ":" + dr["Description"].ToString());
                if (dr["Ledging"].ToString() == "Y")
                    lbxCodeLdg.Items.Add(dr["CycleCode"].ToString() + ":" + dr["Description"].ToString());
            }

            HidePanels();
        }

        private void LoadPanels()
        {
            cbxCycleNum.Enabled = true;
            txtCycle.Enabled = true;
            txtAdv.Enabled = true;

            //Stoping
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                ribbonControl1.Visible = false;
            }

            //Stoping
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                //ribbonControl1.Visible = false;
                rpgStopingCycle.Visible = true;
                repositoryCbxCycles.Items.Clear();
                cbxCycleNum.EditValue = "";

                _clsSetupCycles.theData.ResultsDataTable.Clear();
                DataTable dtCycles = _clsSetupCycles.LoadStopingCycleNames();

                if (dtCycles.Rows.Count == 0)
                    return;

                foreach (DataRow dr in dtCycles.Rows)
                {
                    repositoryCbxCycles.Items.Add(dr["Name"].ToString());
                }
            }

            //Ledging
            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                rpgStopingCycle.Visible = true;
                repositoryCbxCycles.Items.Clear();
                cbxCycleNum.EditValue = "";

                cbxCycleNum.Enabled = false;
                txtCycle.Enabled = false;
                txtAdv.Enabled = false;
                gcLedging.Visible = true;

                LoadLedging();
            }


            //Developement
            if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                txtAdv.Enabled = false;

                rpgStopingCycle.Visible = true;
                repositoryCbxCycles.Items.Clear();
                cbxCycleNum.EditValue = "";

                _clsSetupCycles.theData.ResultsDataTable.Clear();
                DataTable dtCycles = _clsSetupCycles.LoadDevelopementCycleNames();

                if (dtCycles.Rows.Count == 0)
                    return;

                foreach (DataRow dr in dtCycles.Rows)
                {
                    repositoryCbxCycles.Items.Add(dr["Name"].ToString());
                }
            }

            //MO Cycle Config
            if (xtraTabControl1.SelectedTabPageIndex == 4)
            {
                ribbonControl1.Visible = false;

                _clsSetupCycles.theData.ResultsDataTable.Clear();
                DataTable dtCycles = _clsSetupCycles.LoadMOCyclesList();

                DataSet dsCycles = new DataSet();
                dsCycles.Tables.Add(dtCycles);

                lbxMoDevelopement.Items.Clear();
                lbxMoStoping.Items.Clear();

                if (dtCycles.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCycles.Rows)
                    {
                        if (dr["Type"].ToString() == "S")
                        {
                            lbxMoStoping.Items.Add(dr["Name"].ToString());
                        }

                        if (dr["Type"].ToString() == "D")
                        {
                            lbxMoDevelopement.Items.Add(dr["Name"].ToString());
                        }
                    }
                }


                _clsSetupCycles.theData.ResultsDataTable.Clear();
                DataTable dtCyclesGrid = _clsSetupCycles.LoadMOCycleGrid();

                DataSet dsCyclesGrid = new DataSet();

                if (dsCyclesGrid.Tables.Count > 0)
                    dsCyclesGrid.Tables.Clear();

                    dsCyclesGrid.Tables.Add(dtCyclesGrid);
                

                gcMOCycles.DataSource = dsCyclesGrid.Tables[0];

                colMoSection.FieldName = "sec";
                colMoStoping.FieldName = "StopingCycle";
                colMoDevelope.FieldName = "DevCycle";

                LoadWpExceptions();
            }
        }

        private void LoadLedging()
        {
            _clsSetupCycles.theData.ResultsDataTable.Clear();
            DataTable dtLdgCycles = _clsSetupCycles.LoadLedgeCycles();

            DataSet dsLdgCycles = new DataSet();
            dsLdgCycles.Tables.Add(dtLdgCycles);

            gcLedging.DataSource = dtLdgCycles;

            colLdg1.FieldName = "Day1";
            colLdg2.FieldName = "Day2";
            colLdg3.FieldName = "Day3";
            colLdg4.FieldName = "Day4";
            colLdg5.FieldName = "Day5";
            colLdg6.FieldName = "Day6";
            colLdg7.FieldName = "Day7";
            colLdg8.FieldName = "Day8";
            colLdg9.FieldName = "Day9";
            colLdg10.FieldName = "Day10";
            colLdg11.FieldName = "Day11";
            colLdg12.FieldName = "Day12";
            colLdg13.FieldName = "Day13";
            colLdg14.FieldName = "Day14";
            colLdg15.FieldName = "Day15";
            colLdg16.FieldName = "Day16";
            colLdg17.FieldName = "Day17";
            colLdg18.FieldName = "Day18";
            colLdg19.FieldName = "Day19";
            colLdg20.FieldName = "Day20";
            colLdg21.FieldName = "Day21";
            colLdg22.FieldName = "Day22";
            colLdg23.FieldName = "Day23";
            colLdg24.FieldName = "Day24";
            colLdg25.FieldName = "Day25";
        }

        private void LoadWpExceptions()
        {
            //_clsSetupCycles.theData.ResultsDataTable.Clear();
            DataTable dtCyclesGridExcep = _clsSetupCycles.LoadMOCycleGridExcep();

            gcWPExceptions.DataSource = dtCyclesGridExcep;

            colExcepWorkplace.FieldName = "Wp";
            colExcepCycle.FieldName = "Cycle";

            //Workplaces
            DataTable dtExcepWP = _clsSetupCycles.LoadExcepWP();

            foreach (DataRow dr in dtExcepWP.Rows)
            {
                lbxExcepWorkplace.Items.Add(dr["Workplaceid"].ToString() + ":" + dr["Description"].ToString());
            }

            //Cycle
            DataTable dtExcepCycles = _clsSetupCycles.LoadExcepCycles();

            lbxExcepCycle.Items.Clear();

            foreach (DataRow dr in dtExcepCycles.Rows)
            {
                lbxExcepCycle.Items.Add(dr["Name"].ToString());
            }
        }

        private void HidePanels()
        {
            rpgWorktypeCodes.Visible = false;
            rpgStopingCycle.Visible = false;
            rpgSave.Visible = false;
            txtCycle.EditValue = "";
            txtAdv.EditValue = "";

            //if ()
        }

        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //rpgActions.Visible = true;
        }

        private void cbxCycleNum_EditValueChanged(object sender, EventArgs e)
        {
            txtCycle.EditValue = cbxCycleNum.EditValue.ToString();

            //Stoping
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                rpgStopingCycle.Visible = true;
                //repositoryCbxCycles.Items.Clear();

                LoadStopeCycleGrid();
            }


            //Developement
            if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                rpgStopingCycle.Visible = true;
                //repositoryCbxCycles.Items.Clear();

                LoadDevCycleGrid();
            }
        }

        private void LoadDevCycleGrid()
        {
            _clsSetupCycles.theData.ResultsDataTable.Clear();
            DataTable dtCycles = _clsSetupCycles.LoadDevelopementCycles(cbxCycleNum.EditValue.ToString());

            DataSet dsCycles = new DataSet();
            dsCycles.Tables.Add(dtCycles);

            gcDevelopement.DataSource = dsCycles.Tables[0];

            colDevFL.FieldName = "Endtypeaa";
            colDevAdv.FieldName = "AdvBlast";
            colDev1.FieldName = "Day1";
            colDev2.FieldName = "Day2";
            colDev3.FieldName = "Day3";
            colDev4.FieldName = "Day4";
            colDev5.FieldName = "Day5";
            colDev6.FieldName = "Day6";
            colDev7.FieldName = "Day7";
            colDev8.FieldName = "Day8";
            colDev9.FieldName = "Day9";
            colDev10.FieldName = "Day10";
            colDev11.FieldName = "Day11";
            colDev12.FieldName = "Day12";
            colDev13.FieldName = "Day13";
            colDev14.FieldName = "Day14";
            colDev15.FieldName = "Day15";
            colDev16.FieldName = "Day16";
            colDev17.FieldName = "Day17";
            colDev18.FieldName = "Day18";
            colDev19.FieldName = "Day19";
            colDev20.FieldName = "Day20";
            colDev21.FieldName = "Day21";
            colDev22.FieldName = "Day22";
            colDev23.FieldName = "Day23";
            colDev24.FieldName = "Day24";
        }

        private void LoadStopeCycleGrid()
        {
            _clsSetupCycles.theData.ResultsDataTable.Clear();
            DataTable dtCycles = _clsSetupCycles.LoadStopingCycles(cbxCycleNum.EditValue.ToString());

            DataSet dsCycles = new DataSet();
            dsCycles.Tables.Add(dtCycles);

            if (dtCycles.Rows.Count > 0)
                txtAdv.EditValue = dtCycles.Rows[0]["AdvBlast"].ToString();

            gcStoping.DataSource = null;

            gcStoping.DataSource = dtCycles;

            colStpFL.FieldName = "FL";
            colStp1.FieldName = "Day1";
            colStp2.FieldName = "Day2";
            colStp3.FieldName = "Day3";
            colStp4.FieldName = "Day4";
            colStp5.FieldName = "Day5";
            colStp6.FieldName = "Day6";
            colStp7.FieldName = "Day7";
            colStp8.FieldName = "Day8";
            colStp9.FieldName = "Day9";
            colStp10.FieldName = "Day10";
            colStp11.FieldName = "Day11";
            colStp12.FieldName = "Day12";
            colStp13.FieldName = "Day13";
            colStp14.FieldName = "Day14";
            colStp15.FieldName = "Day15";
            colStp16.FieldName = "Day16";
            colStp17.FieldName = "Day17";
            colStp18.FieldName = "Day18";
            colStp19.FieldName = "Day19";
            colStp20.FieldName = "Day20";
            colStp21.FieldName = "Day21";
            colStp22.FieldName = "Day22";
            colStp23.FieldName = "Day23";
            colStp24.FieldName = "Day24";
        }


        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            ribbonControl1.Visible = true;
            LblDidCopy.Text = "N";
            HidePanels();           

            LoadPanels();


            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                txtAdv.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                txtAdv.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void lbxCodeStp_MouseDown(object sender, MouseEventArgs e)
        {
            //lblCodeStope.Text = "None";

            IsCycleCode = true;

            if (lbxCodeStp.Items.Count == 0)
                return;
            int index = lbxCodeStp.IndexFromPoint(e.X, e.Y);
            if (index > -1)
            {
                string s = lbxCodeStp.Items[index].ToString();
                LblCodeStope.Text = ExtractBeforeColon(lbxCodeStp.Items[index].ToString());
                DragDropEffects dde1 = DoDragDrop(s,
                    DragDropEffects.All);
            }
        }

        public string ExtractBeforeColon(string TheString)
        {
            if (TheString != "")
            {
                string BeforeColon;
                int index = TheString.IndexOf(":");
                BeforeColon = TheString.Substring(0, index);
                return BeforeColon;
            }
            else
            {
                return "";
            }
        }

        private void gcStoping_DragDrop(object sender, DragEventArgs e)
        {
                Point p = this.gcStoping.PointToClient(new Point(e.X, e.Y));
                int row = gvStoping.CalcHitInfo(p.X, p.Y).RowHandle;
                if (row > -1)
                {
                    if ( gvStoping.FocusedColumn.Name.ToString() == "FL" )
                    {
                        return;
                    }
                    else if (gvStoping.CalcHitInfo(p.X, p.Y).Column.FieldName != null)
                    {
                        this.gvStoping.SetRowCellValue(row, gvStoping.CalcHitInfo(p.X, p.Y).Column.FieldName, LblCodeStope.Text);
                    }
                }
        }

        private void gcStoping_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gcStoping_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gvStoping_RowCellClick(object sender, RowCellClickEventArgs e)
        {            
                if (gvStoping.FocusedColumn.FieldName.ToString() == "FL")
                {
                    return;
                }
                else if (LblCodeStope.Text != "None" && LblCodeStope.Text != "Code")
                {
                gvStoping.SetRowCellValue(gvStoping.FocusedRowHandle, gvStoping.FocusedColumn, LblCodeStope.Text);
                }
            
        }

        private void lbxCodeLdg_MouseDown(object sender, MouseEventArgs e)
        {

            IsCycleCode = true;

            if (lbxCodeLdg.Items.Count == 0)
                return;
            int index = lbxCodeLdg.IndexFromPoint(e.X, e.Y);
            if (index > -1)
            {
                string s = lbxCodeLdg.Items[index].ToString();
                lblCodeLdg.Text = ExtractBeforeColon(lbxCodeLdg.Items[index].ToString());
                DragDropEffects dde1 = DoDragDrop(s,
                    DragDropEffects.All);
            }
        }

        private void lbxCodeDev_MouseDown(object sender, MouseEventArgs e)
        {

            IsCycleCode = true;

            if (lbxCodeDev.Items.Count == 0)
                return;
            int index = lbxCodeDev.IndexFromPoint(e.X, e.Y);
            if (index > -1)
            {
                string s = lbxCodeDev.Items[index].ToString();
                lblCodeDev.Text = ExtractBeforeColon(lbxCodeDev.Items[index].ToString());
                DragDropEffects dde1 = DoDragDrop(s,
                    DragDropEffects.All);
            }
        }

        private void gvLedging_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (gvLedging.FocusedColumn.FieldName.ToString() == "FL")
            {
                return;
            }
            else if (lblCodeLdg.Text != "None" && lblCodeLdg.Text != "Code")
            {
                gvLedging.SetRowCellValue(gvLedging.FocusedRowHandle, gvLedging.FocusedColumn, lblCodeLdg.Text);
            }
        }

        private void gcLedging_DragDrop(object sender, DragEventArgs e)
        {
            Point p = this.gcLedging.PointToClient(new Point(e.X, e.Y));
            int row = gvLedging.CalcHitInfo(p.X, p.Y).RowHandle;
            if (row > -1)
            {
                if (gvLedging.FocusedColumn.Name.ToString() == "FL")
                {
                    return;
                }
                else if (gvLedging.CalcHitInfo(p.X, p.Y).Column.FieldName != null)
                {
                    this.gvLedging.SetRowCellValue(row, gvLedging.CalcHitInfo(p.X, p.Y).Column.FieldName, lblCodeLdg.Text);
                }
            }
        }

        private void gcDevelopement_DragDrop(object sender, DragEventArgs e)
        {
            Point p = this.gcDevelopement.PointToClient(new Point(e.X, e.Y));
            int row = gvDevelopement.CalcHitInfo(p.X, p.Y).RowHandle;
            if (row > -1)
            {
                if (gvDevelopement.FocusedColumn.Name.ToString() == "FL")
                {
                    return;
                }
                else if (gvDevelopement.CalcHitInfo(p.X, p.Y).Column.FieldName != null)
                {
                    this.gvDevelopement.SetRowCellValue(row, gvDevelopement.CalcHitInfo(p.X, p.Y).Column.FieldName, lblCodeDev.Text);
                }
            }
        }

        private void gcLedging_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gcDevelopement_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gcDevelopement_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gcLedging_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gvDevelopement_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (gvDevelopement.FocusedColumn.FieldName.ToString() == "FL")
            {
                return;
            }

            if (gvDevelopement.FocusedColumn.FieldName.ToString() == "Endtypeaa")
            {
                return;
            }

            else if (lblCodeDev.Text != "None" && lblCodeDev.Text != "Code")
            {
                gvDevelopement.SetRowCellValue(gvDevelopement.FocusedRowHandle, gvDevelopement.FocusedColumn, lblCodeDev.Text);
            }
        }

        private void btnStpApply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Stoping
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                SaveStoping();
                LoadStopeCycleGrid();
            }

            //Ledging
            if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                SaveLedging();
                LoadLedging();
            }

            //Developement
            if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                SaveDevelope();
                LoadDevCycleGrid();
            }

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Cycle saved", Color.CornflowerBlue);
        }


        private void SaveStoping()
        {
            if (txtAdv.EditValue.ToString() == "")
            {
                MessageBox.Show("Please Enter the Adv/Blast for the Cycle", "Missing Adv/Blast", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtCycle.EditValue.ToString() == "")
            {
                MessageBox.Show("Please Enter a cycle name", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string SqlStatement = "";

            SqlStatement = SqlStatement + "delete from Code_Cycle_RawData  where Name = '" + txtCycle.EditValue.ToString() + "' and Type = 'S'   \r\n\r\n";

            for (int x = 0; x <= 74; x++)
            {
                string FL = gvStoping.GetRowCellValue(x, gvStoping.Columns["FL"]).ToString();
                string Day1 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day1"]).ToString();
                string Day2 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day2"]).ToString();
                string Day3 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day3"]).ToString();
                string Day4 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day4"]).ToString();
                string Day5 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day5"]).ToString();
                string Day6 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day6"]).ToString();
                string Day7 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day7"]).ToString();
                string Day8 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day8"]).ToString();
                string Day9 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day9"]).ToString();
                string Day10 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day10"]).ToString();
                string Day11 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day11"]).ToString();
                string Day12 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day12"]).ToString();
                string Day13 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day13"]).ToString();
                string Day14 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day14"]).ToString();
                string Day15 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day15"]).ToString();
                string Day16 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day16"]).ToString();
                string Day17 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day17"]).ToString();
                string Day18 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day18"]).ToString();
                string Day19 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day19"]).ToString();
                string Day20 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day20"]).ToString();
                string Day21 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day21"]).ToString();
                string Day22 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day22"]).ToString();
                string Day23 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day23"]).ToString();
                string Day24 = gvStoping.GetRowCellValue(x, gvStoping.Columns["Day24"]).ToString();

                SqlStatement = SqlStatement + " Insert into Code_Cycle_RawData(Name,FL,AdvBlast,Type,Day1,Day2,Day3,Day4,Day5,Day6,Day7,Day8,Day9,Day10,Day11,Day12,Day13,Day14,Day15,   \r\n" +
                 " Day16,Day17,Day18,Day19,Day20,Day21,Day22,Day23,Day24 ) values(    \r\n" +
                 " '" + txtCycle.EditValue.ToString() + "','" + FL + "','" + txtAdv.EditValue.ToString() + "' ,'S', '" + Day1 + "', '" + Day2 + "',    \r\n" +
                 " '" + Day3 + "','" + Day4 + "','" + Day5 + "','" + Day6 + "',     \r\n" +
                 " '" + Day7 + "','" + Day8 + "','" + Day9 + "','" + Day10 + "',     \r\n" +
                 " '" + Day11 + "','" + Day12 + "','" + Day13 + "','" + Day14 + "',     \r\n" +
                 " '" + Day15 + "','" + Day16 + "','" + Day17 + "','" + Day18 + "',     \r\n" +
                 " '" + Day19 + "','" + Day20 + "','" + Day21 + "','" + Day22 + "',     \r\n" +
                 " '" + Day23 + "','" + Day24 + "')  \r\n\r\n";

            }



            _clsSetupCycles.theData.ResultsDataTable.Clear();
             _clsSetupCycles.SaveStoping(SqlStatement);           
        }

        private void SaveLedging()
        {
            string SqlStatement = " ";

            string Day1 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day1"]).ToString();
            string Day2 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day2"]).ToString();
            string Day3 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day3"]).ToString();
            string Day4 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day4"]).ToString();
            string Day5 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day5"]).ToString();
            string Day6 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day6"]).ToString();
            string Day7 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day7"]).ToString();
            string Day8 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day8"]).ToString();
            string Day9 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day9"]).ToString();
            string Day10 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day10"]).ToString();
            string Day11 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day11"]).ToString();
            string Day12 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day12"]).ToString();
            string Day13 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day13"]).ToString();
            string Day14 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day14"]).ToString();
            string Day15 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day15"]).ToString();
            string Day16 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day16"]).ToString();
            string Day17 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day17"]).ToString();
            string Day18 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day18"]).ToString();
            string Day19 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day19"]).ToString();
            string Day20 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day20"]).ToString();
            string Day21 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day21"]).ToString();
            string Day22 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day22"]).ToString();
            string Day23 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day23"]).ToString();
            string Day24 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day24"]).ToString();
            string Day25 = gvLedging.GetRowCellValue(0, gvLedging.Columns["Day25"]).ToString();

            SqlStatement = " Delete from dbo.Code_Cycle_Ledging    \r\n" +

             " Insert into dbo.Code_Cycle_Ledging values(     \r\n" +
             " '" + Day1 + "' , '" + Day2 + "', '" + Day3 + "',    \r\n" +
             " '" + Day4 + "','" + Day5 + "','" + Day6 + "','" + Day7 + "',    \r\n" +
             " '" + Day8 + "','" + Day9 + "','" + Day10 + "','" + Day11 + "',    \r\n" +
             " '" + Day12 + "','" + Day13 + "','" + Day14 + "','" + Day15 + "',    \r\n" +
             " '" + Day16 + "','" + Day17 + "','" + Day18 + "','" + Day19 + "',    \r\n" +
             " '" + Day20 + "','" + Day21 + "','" + Day22 + "','" + Day23 + "',    \r\n" +
             " '" + Day24 + "','" + Day25 + "') ";//'" + LedgingCycleDG.Rows[x].Cells[25].Value + "')";



            _clsSetupCycles.theData.ResultsDataTable.Clear();
            _clsSetupCycles.SaveLedging(SqlStatement);
        }

        private void SaveDevelope()
        {
            //if (txtAdv.EditValue.ToString() == "")
            //{
            //    MessageBox.Show("Please Enter the Adv/Blast for the Cycle", "Missing Adv/Blast", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (txtCycle.EditValue.ToString() == "")
            {
                MessageBox.Show("Please Enter a cycle name", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string SqlStatement = "";

            SqlStatement = SqlStatement + "delete from Code_Cycle_RawData where Type = 'D' and Name = '" + txtCycle.EditValue.ToString() + "'   \r\n\r\n";

            for (int x = 0; x <= gvDevelopement.RowCount-1; x++)
            {
                string FL = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Endtypeaa"]).ToString();
                string Adv = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["AdvBlast"]).ToString();
                string Day1 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day1"]).ToString();
                string Day2 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day2"]).ToString();
                string Day3 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day3"]).ToString();
                string Day4 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day4"]).ToString();
                string Day5 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day5"]).ToString();
                string Day6 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day6"]).ToString();
                string Day7 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day7"]).ToString();
                string Day8 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day8"]).ToString();
                string Day9 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day9"]).ToString();
                string Day10 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day10"]).ToString();
                string Day11 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day11"]).ToString();
                string Day12 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day12"]).ToString();
                string Day13 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day13"]).ToString();
                string Day14 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day14"]).ToString();
                string Day15 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day15"]).ToString();
                string Day16 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day16"]).ToString();
                string Day17 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day17"]).ToString();
                string Day18 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day18"]).ToString();
                string Day19 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day19"]).ToString();
                string Day20 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day20"]).ToString();
                string Day21 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day21"]).ToString();
                string Day22 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day22"]).ToString();
                string Day23 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day23"]).ToString();
                string Day24 = gvDevelopement.GetRowCellValue(x, gvDevelopement.Columns["Day24"]).ToString();

                //SqlStatement = SqlStatement + " Insert into Code_Cycle_RawData(Name,FL,AdvBlast,Type,Day1,Day2,Day3,Day4,Day5,Day6,Day7,Day8,Day9,Day10,Day11,Day12,Day13,Day14,Day15,   \r\n" +
                // " Day16,Day17,Day18,Day19,Day20,Day21,Day22,Day23,Day24 ) values(    \r\n" +
                // " '" + txtCycle.EditValue.ToString() + "','" + FL + "','" + txtAdv.EditValue.ToString() + "' ,'S', '" + Day1 + "', '" + Day2 + "',    \r\n" +
                // " '" + Day3 + "','" + Day4 + "','" + Day5 + "','" + Day6 + "',     \r\n" +
                // " '" + Day7 + "','" + Day8 + "','" + Day9 + "','" + Day10 + "',     \r\n" +
                // " '" + Day11 + "','" + Day12 + "','" + Day13 + "','" + Day14 + "',     \r\n" +
                // " '" + Day15 + "','" + Day16 + "','" + Day17 + "','" + Day18 + "',     \r\n" +
                // " '" + Day19 + "','" + Day20 + "','" + Day21 + "','" + Day22 + "',     \r\n" +
                // " '" + Day23 + "','" + Day24 + "')  \r\n\r\n";

                string BeforeColon;
                int index = FL.IndexOf(":");
                BeforeColon = FL.Substring(0, index);
                if (Day1 != "")
                {
                    SqlStatement = SqlStatement + " Insert into Code_Cycle_RawData (Name,FL,AdvBlast,Type,Day1,Day2,Day3,Day4,Day5,Day6,Day7,Day8,Day9,Day10,Day11,Day12,Day13,Day14,Day15,      \r\n" +
                     " Day16,Day17,Day18,Day19,Day20,Day21,Day22,Day23,Day24 ) values(      \r\n" +
                     " '" + txtCycle.EditValue.ToString() + "','" + BeforeColon + "','" + Convert.ToDecimal(Adv) + "' ,'D', '" + Day1 + "', '" + Day2 + "',     \r\n" +
                     " '" + Day3 + "','" + Day4 + "','" + Day5 + "','" + Day6 + "',     \r\n" +
                     " '" + Day7 + "','" + Day8+ "','" + Day9 + "','" + Day10 + "',     \r\n" +
                     " '" + Day11 + "','" + Day12 + "','" + Day13 + "','" + Day14 + "',     \r\n" +
                     " '" + Day15 + "','" + Day16 + "','" + Day17 + "','" + Day18 + "',      \r\n" +
                     " '" + Day19 + "','" + Day20 + "','" + Day21 + "','" + Day22 + "',     \r\n" +
                     " '" + Day23 + "','" + Day24 + "')  \r\n\r\n"; 
                }

            }



            _clsSetupCycles.theData.ResultsDataTable.Clear();
            _clsSetupCycles.SaveDevelopement(SqlStatement);
        }

        private void lbxStoping_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbxMoStoping.Items.Count == 0)
                return;
            int index = lbxMoStoping.IndexFromPoint(e.X, e.Y);
            if (index > -1)
            {
                string s = lbxMoStoping.Items[index].ToString();
                lblCode.Text = lbxMoStoping.Items[index].ToString();
                lblType.Text = "Stope";
                DragDropEffects dde1 = DoDragDrop(s,
                    DragDropEffects.All);
            }
        }

        private void lbxDevelopement_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbxMoDevelopement.Items.Count == 0)
                return;
            int index = lbxMoDevelopement.IndexFromPoint(e.X, e.Y);
            if (index > -1)
            {
                string s = lbxMoDevelopement.Items[index].ToString();
                lblCode.Text = lbxMoDevelopement.Items[index].ToString();
                lblType.Text = "Dev";
                DragDropEffects dde1 = DoDragDrop(s,
                    DragDropEffects.All);
            }
        }

        private void gcMOCycles_DragDrop(object sender, DragEventArgs e)
        {
            Point p = this.gcMOCycles.PointToClient(new Point(e.X, e.Y));
            int row = gvMoCycles.CalcHitInfo(p.X, p.Y).RowHandle;

            if (row > -1)
            {
                if (lblCode.Text == "Blank")
                {
                    MessageBox.Show("Please Select a Cycle");
                    return;
                }

                string test = gvMoCycles.CalcHitInfo(p.X, p.Y).Column.FieldName;

                if (gvMoCycles.CalcHitInfo(p.X, p.Y).Column.FieldName == "DevCycle" && lblType.Text == "Stope")
                {
                    MessageBox.Show("Can't apply a Stoping Cycle to Developement");
                    return;
                }

                if (gvMoCycles.CalcHitInfo(p.X, p.Y).Column.FieldName == "StopingCycle" && lblType.Text == "Dev")
                {
                    MessageBox.Show("Can't apply a Developement Cycle to Stoping");
                    return;
                }
                
                    this.gvMoCycles.SetRowCellValue(row, gvMoCycles.CalcHitInfo(p.X, p.Y).Column.FieldName, lblCode.Text);
                    lblCode.Text = "Blank";


                SaveMoCycleConfig();
            }
            
        }

        private void SaveMoCycleConfig()
        {
            string SqlStatement = "";

            SqlStatement = SqlStatement + " delete from Code_Cycle_MOCycleConfig   \r\n\r\n";

            for (int x = 0; x <= gvMoCycles.RowCount - 1; x++)
            {
                string MO = gvMoCycles.GetRowCellValue(x, gvMoCycles.Columns["sec"]).ToString();
                string StpCycle = gvMoCycles.GetRowCellValue(x, gvMoCycles.Columns["StopingCycle"]).ToString();
                string DevCycle = gvMoCycles.GetRowCellValue(x, gvMoCycles.Columns["DevCycle"]).ToString();

                SqlStatement = SqlStatement + " insert into Code_Cycle_MOCycleConfig values('" + ExtractBeforeColon(MO) + "','" + StpCycle + "','" + DevCycle + "')   \r\n\r\n";
            }


            _clsSetupCycles.theData.ResultsDataTable.Clear();
            _clsSetupCycles.SaveMOCycle(SqlStatement);

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Cycle Applied", Color.CornflowerBlue);

            _clsSetupCycles.theData.ResultsDataTable.Clear();
            DataTable dtCyclesGrid = _clsSetupCycles.LoadMOCycleGrid();

            DataSet dsCyclesGrid = new DataSet();
            dsCyclesGrid.Tables.Add(dtCyclesGrid);

            gcMOCycles.DataSource = null;

            gcMOCycles.DataSource = dtCyclesGrid;

            colMoSection.FieldName = "sec";
            colMoStoping.FieldName = "StopingCycle";
            colMoDevelope.FieldName = "DevCycle";
        }

        private void gcMOCycles_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gcMOCycles_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string SqlStatement = "";

            if (lbxExcepWorkplace.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a workplace first");
                return;
            }

            if (lbxExcepCycle.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Cycle first");
                return;
            }

            string WPID = ExtractBeforeColon(lbxExcepWorkplace.SelectedItem.ToString());
            string Cycle = lbxExcepCycle.SelectedItem.ToString();


            SqlStatement = SqlStatement + " insert into Code_Cycle_WPExceptions values('" + WPID + "','" + Cycle + "') ";

            _clsSetupCycles.theData.ResultsDataTable.Clear();
            _clsSetupCycles.AddMOException(SqlStatement);

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Exception added", Color.CornflowerBlue);

            LoadWpExceptions();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string SqlStatement = "";

            if (gvWPExceptions.RowCount > 0)
            {
                string CycleWPID = gvWPExceptions.GetRowCellValue(gvWPExceptions.FocusedRowHandle, gvWPExceptions.Columns["Wp"]).ToString();
                

                SqlStatement = " Delete from Code_Cycle_WPExceptions where workplaceid = '" + ExtractBeforeColon(CycleWPID) + "'  ";

                _clsSetupCycles.theData.ResultsDataTable.Clear();
                _clsSetupCycles.DeleteMOException(SqlStatement);

                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Cycle deleted", Color.CornflowerBlue);

                LoadWpExceptions();
            }

          
        }

        private void btnDepartments_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDepartmentMain frm = new frmDepartmentMain();
            frm._theSystemDBTag = theSystemDBTag;
            frm._UserCurrentInfoConnection = UserCurrentInfo.Connection;
            frm.ShowDialog();
        }

        private void txtCycle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gvStoping_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (gvStoping.GetRowCellValue(e.RowHandle, e.Column).ToString() == "BL" || gvStoping.GetRowCellValue(e.RowHandle, e.Column).ToString() == "SR")
            {
                //e.Appearance.BackColor = Color.Tomato;
                e.Appearance.ForeColor = Color.Tomato;
            }
        }

        private void gvLedging_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (gvLedging.GetRowCellValue(e.RowHandle, e.Column).ToString() == "BL" || gvLedging.GetRowCellValue(e.RowHandle, e.Column).ToString() == "SR")
            {
                //e.Appearance.BackColor = Color.Tomato;
                e.Appearance.ForeColor = Color.Tomato;
            }
        }

        private void gvDevelopement_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (gvDevelopement.GetRowCellValue(e.RowHandle, e.Column).ToString() == "BL" || gvDevelopement.GetRowCellValue(e.RowHandle, e.Column).ToString() == "SR")
            {
                //e.Appearance.BackColor = Color.Tomato;
                e.Appearance.ForeColor = Color.Tomato;
            }
        }

        private void gvStoping_KeyDown(object sender, KeyEventArgs e)
        {
            LblCodeStope.Text = "Code";

            //Copy
            if (e.Control && e.KeyValue == 67)
            {
                if (gvStoping.FocusedRowHandle < 0)
                    return;

                for (int i = 0; i < dtCopyAndPaste.ColumnCount; i++)
                {
                    dtCopyAndPaste.Rows[0].Cells[i].Value = gvStoping.GetRowCellValue(gvStoping.FocusedRowHandle, gvStoping.Columns[i+1]).ToString();
                }

                LblDidCopy.Text = "Y";
            }

            //Paste
            if (e.Control && e.KeyValue == 86)
            {
                if (gvStoping.FocusedRowHandle < 0)
                    return;

                if (LblDidCopy.Text == "N")
                    return;


                for (int i = 0; i < dtCopyAndPaste.ColumnCount; i++)
                {
                    gvStoping.SetRowCellValue(gvStoping.FocusedRowHandle, gvStoping.Columns[i + 1], dtCopyAndPaste.Rows[0].Cells[i].Value.ToString());
                }

            }
        }

        private void gvDevelopement_KeyDown(object sender, KeyEventArgs e)
        {
            lblCodeDev.Text = "Code";

            //Copy
            if (e.Control && e.KeyValue == 67)
            {
                if (gvDevelopement.FocusedRowHandle < 0)
                    return;

                for (int i = 0; i < dtCopyAndPaste.ColumnCount; i++)
                {
                    dtCopyAndPaste.Rows[0].Cells[i].Value = gvDevelopement.GetRowCellValue(gvDevelopement.FocusedRowHandle, gvDevelopement.Columns[i + 2]).ToString();
                }

                LblDidCopy.Text = "Y";
            }

            //Paste
            if (e.Control && e.KeyValue == 86)
            {
                if (gvDevelopement.FocusedRowHandle < 0)
                    return;

                if (LblDidCopy.Text == "N")
                    return;


                for (int i = 0; i < dtCopyAndPaste.ColumnCount; i++)
                {
                    gvDevelopement.SetRowCellValue(gvDevelopement.FocusedRowHandle, gvDevelopement.Columns[i + 2], dtCopyAndPaste.Rows[0].Cells[i].Value.ToString());
                }

            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void gcMOCycles_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void xtrTProblemNotes_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
