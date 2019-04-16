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

namespace Mineware.Systems.Production.SysAdminScreens.SetupCycles 
{
    public partial class frmDepartmentMain : DevExpress.XtraEditors.XtraForm
    {

        clsSetupCycles _clsSetupCycles = new clsSetupCycles();

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public frmDepartmentMain()
        {
            InitializeComponent();
        }

        private void frmDepartmentMain_Load(object sender, EventArgs e)
        {
            _clsSetupCycles.theData.ConnectionString = TConnections.GetConnectionString(TConnections.GetConnectionString("DBHARMONYPAS", "Doornkop"));

            loadWorkNoteWorkplaces();
        }


        private void loadWorkNoteWorkplaces()
        {
            MWDataManager.clsDataAccess _dbManMain = new MWDataManager.clsDataAccess();
            _dbManMain.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManMain.SqlStatement = "Select distinct s3.sectionid sec,'N' MainChecked  \r\n"+
                                    "from planning p, section s, section s2, SECTION s3  \r\n" +
                                    "where p.prodmonth = s.prodmonth and p.sectionid = s.sectionid    \r\n"+ 
                                    "and s.reporttosectionid = s2.sectionid  and s.prodmonth = s2.prodmonth   \r\n"+ 
                                    "and s2.reporttosectionid = s3.sectionid     and s2.prodmonth = s3.prodmonth  \r\n"+ 
                                    "and p.Prodmonth = (select max(Prodmonth) FROM planning) ";


            _dbManMain.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManMain.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManMain.ExecuteInstruction();

            DataTable dtMain = _dbManMain.ResultsDataTable;

            MWDataManager.clsDataAccess _dbManSub = new MWDataManager.clsDataAccess();
            _dbManSub.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManSub.SqlStatement = "Select b.sec ,a.Description,a.WorkplaceID, 'N' Checked from (   \r\n" +
                                    "Select SectionID minerID,p.WorkplaceID,wp.Description from PLANNING p, WORKPLACE wp    \r\n"+
                                    "where p.WorkplaceID = wp.WorkplaceID and p.Prodmonth = '201806'   \r\n"+
                                    "group by SectionID ,p.WorkplaceID,wp.Description )a   \r\n"+
                                    "left outer join   \r\n"+
                                    "(Select distinct   s.SectionID MinerID, s3.sectionid sec, s3.Name   \r\n"+
                                    "from planning p, section s, section s2, SECTION s3   \r\n"+
                                    "where p.prodmonth = s.prodmonth and p.sectionid = s.sectionid   \r\n"+
                                    "and s.reporttosectionid = s2.sectionid  and s.prodmonth = s2.prodmonth  \r\n"+
                                    "and s2.reporttosectionid = s3.sectionid     and s2.prodmonth = s3.prodmonth  \r\n"+
                                    "and p.Prodmonth = (select max(Prodmonth) FROM planning)  ) b  \r\n"+
                                    "on a.minerID = b.MinerID ";


            _dbManSub.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSub.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSub.ExecuteInstruction();

            DataTable dtSub = _dbManSub.ResultsDataTable;

            DataSet dsRelations = new DataSet();

            dsRelations.Tables.Add(dtMain);
            dsRelations.Tables.Add(dtSub);

            dsRelations.Relations.Clear();

            DataColumn MainColumn = dsRelations.Tables[0].Columns[0];
            DataColumn SubColumn = dsRelations.Tables[1].Columns[0];
            dsRelations.Relations.Add("Detials",MainColumn,SubColumn);

            gcMOWorkplaces.DataSource = dsRelations.Tables[0];
            gcMOWorkplaces.LevelTree.Nodes.Add("Detials", gvWPLevel);

            colSectionID.FieldName = "sec";
            colChecked.FieldName = "MainChecked";

            colWpSectionid.FieldName = "sec";
            colWPID.FieldName = "WorkplaceID";
            colWP.FieldName = "Description";
            colWPChecked.FieldName = "Checked";
        }

        private void btnRockMech_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HidePanels();

            LoadWalkAboutAssGrid();
        }

        public void LoadWalkAboutAssGrid()
        {

            // load summary grid

            DataTable dtwk = _clsSetupCycles.LoadSumGrid();

            listBoxWk.Items.Clear();
            listBoxWk.Items.Add("");
            foreach (DataRow dr in dtwk.Rows)
            {
                listBoxWk.Items.Add(dr["a"].ToString());
            }

            listBoxWk.SelectedIndex = 0;


            RockMechPanel.BringToFront();
            RockMechPanel.Visible = true;
            RockMechPanel.Dock = DockStyle.Fill;

           

            DataTable dtDetail = _clsSetupCycles.LoadWalkAboutGrid();

            DataSet ds = new DataSet();

            ds.Tables.Add(dtDetail);


            gridControl6.DataSource = ds.Tables[0];

            col1SecID.FieldName = "nn";
            col1SecID.Visible = false;
            col1Wp.FieldName = "description";

            if (SysSettings.Banner == "Great Noligwa" || SysSettings.Banner == "Moab Khotsong" || SysSettings.Banner == "Kopanang")
            {
                col1RR.FieldName = "rrr";
            }
            else
            {
                col1RR.FieldName = "aa";
            }


            ColActFFFF.FieldName = "activityfinal";

            col1Wk1.FieldName = "wp6";
            col1Wk2.FieldName = "wp5";
            col1Wk3.FieldName = "wp4";
            col1Wk4.FieldName = "wp3";
            col1Wk5.FieldName = "wp2";
            col1Wk6.FieldName = "wp1";
            col1Act.FieldName = "wpnow";

            Col1Day1.FieldName = "col6";
            Col1Day2.FieldName = "col5";
            Col1Day3.FieldName = "col4";
            Col1Day4.FieldName = "col3";
            Col1Day5.FieldName = "col2";
            Col1Day6.FieldName = "col1";
            Col1Day7.FieldName = "colnow";

            colLastVisit.FieldName = "LastVisitDate";
            colDaysSinceLastVisit.FieldName = "DaysSince";

            if (dtDetail.Rows.Count > 0)
            {
                Col1Day1.Caption = dtDetail.Rows[0]["hhwk6"].ToString();
                Col1Day2.Caption = dtDetail.Rows[0]["hhwk5"].ToString();
                Col1Day3.Caption = dtDetail.Rows[0]["hhwk4"].ToString();
                Col1Day4.Caption = dtDetail.Rows[0]["hhwk3"].ToString();
                Col1Day5.Caption = dtDetail.Rows[0]["hhwk2"].ToString();
                Col1Day6.Caption = dtDetail.Rows[0]["hhwk1"].ToString();
                Col1Day7.Caption = dtDetail.Rows[0]["hhwnow"].ToString();
                // Col1Day8.Caption = dtDetail.Rows[0]["wp6"].ToString();
            }

            bandedGridView11.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            bandedGridView11.Columns[0].GroupIndex = 0;

            bandedGridView11.ExpandAllGroups();



            gridControl6.Dock = DockStyle.Fill;
            gridControl6.Visible = true;
        }

        private void btnVentilation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HidePanels();

            LoadVentAssGrid();
        }

        public void LoadVentAssGrid()
        {
            VentPanel.Visible = true;
            VentPanel.Dock = DockStyle.Fill;

            DataTable dtDetail = _clsSetupCycles.LoadVentilationGrid(); 

            //DataSet ds = new DataSet();

            //ds.Tables.Add(dtDetail);


            gridControl3.DataSource = dtDetail;

            col1SecIDa.FieldName = "nn";
            col1SecIDa.Visible = false;
            col1Wpa.FieldName = "description";
            col1RRa.FieldName = "aa";
            col1Wk1a.FieldName = "wp6";
            col1Wk2a.FieldName = "wp5";
            col1Wk3a.FieldName = "wp4";
            col1Wk4a.FieldName = "wp3";
            col1Wk5a.FieldName = "wp2";
            col1Wk6a.FieldName = "wp1";
            col1Act.FieldName = "wpnow";

            Col1Day1a.FieldName = "col6";
            Col1Day2a.FieldName = "col5";
            Col1Day3a.FieldName = "col4";
            Col1Day4a.FieldName = "col3";
            Col1Day5a.FieldName = "col2";
            Col1Day6a.FieldName = "col1";
            Col1Day7a.FieldName = "colnow";


            Col1Day1a.Caption = dtDetail.Rows[0]["hhwk6"].ToString();
            Col1Day2a.Caption = dtDetail.Rows[0]["hhwk5"].ToString();
            Col1Day3a.Caption = dtDetail.Rows[0]["hhwk4"].ToString();
            Col1Day4a.Caption = dtDetail.Rows[0]["hhwk3"].ToString();
            Col1Day5a.Caption = dtDetail.Rows[0]["hhwk2"].ToString();
            Col1Day6a.Caption = dtDetail.Rows[0]["hhwk1"].ToString();
            Col1Day7a.Caption = dtDetail.Rows[0]["hhwnow"].ToString();
            // Col1Day8.Caption = dtDetail.Rows[0]["wp6"].ToString();


            bandedGridView3.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            bandedGridView3.Columns[0].GroupIndex = 0;

            bandedGridView3.ExpandAllGroups();

            gridControl3.Dock = DockStyle.Fill;
            gridControl3.Visible = true;
        }


        private void btnGeology_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HidePanels();

            LoadGeoInspGrid();
        }

        public void LoadGeoInspGrid()
        {
            DataTable dtwk = _clsSetupCycles.LoadSumGeoGrid();

            listBox2.Items.Clear();
            listBox2.Items.Add("");
            foreach (DataRow dr in dtwk.Rows)
            {
                listBox2.Items.Add(dr["a"].ToString());
            }

            listBox2.SelectedIndex = 0;


            GeoScienceInspPnl.Dock = DockStyle.Fill;
            GeoScienceInspPnl.Visible = true;



            DataTable dtDetail = _clsSetupCycles.LoadGeologyGrid();

            DataSet ds = new DataSet();

            ds.Tables.Add(dtDetail);


            gridControl2.DataSource = ds.Tables[0];

            col1SecIDGS.FieldName = "nn";
            col1SecIDGS.Visible = false;
            col1WpGS.FieldName = "description";
            col1RRGS.FieldName = "rr";
            col1Wk1GS.FieldName = "wp6";
            col1Wk2GS.FieldName = "wp5";
            col1Wk3GS.FieldName = "wp4";
            col1Wk4GS.FieldName = "wp3";
            col1Wk5GS.FieldName = "wp2";
            col1Wk6GS.FieldName = "wp1";
            col1WkNowGS.FieldName = "wpnow";

            Col1Day1GS.FieldName = "col6";
            Col1Day2GS.FieldName = "col5";
            Col1Day3GS.FieldName = "col4";
            Col1Day4GS.FieldName = "col3";
            Col1Day5GS.FieldName = "col2";
            Col1Day6GS.FieldName = "col1";
            Col1Day7GS.FieldName = "colnow";


            Col1Day1GS.Caption = dtDetail.Rows[0]["hhwk6"].ToString();
            Col1Day2GS.Caption = dtDetail.Rows[0]["hhwk5"].ToString();
            Col1Day3GS.Caption = dtDetail.Rows[0]["hhwk4"].ToString();
            Col1Day4GS.Caption = dtDetail.Rows[0]["hhwk3"].ToString();
            Col1Day5GS.Caption = dtDetail.Rows[0]["hhwk2"].ToString();
            Col1Day6GS.Caption = dtDetail.Rows[0]["hhwk1"].ToString();
            Col1Day7GS.Caption = dtDetail.Rows[0]["hhwnow"].ToString();
            // Col1Day8.Caption = dtDetail.Rows[0]["wp6"].ToString();


            bandedGridView2.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            bandedGridView2.Columns[0].GroupIndex = 0;

            bandedGridView2.ExpandAllGroups();

            gridControl2.Dock = DockStyle.Fill;
            gridControl2.Visible = true;
        }


        public void HidePanels()
        {
            GeoScienceInspPnl.Dock = DockStyle.None;
            GeoScienceInspPnl.Visible = false;

            RockMechPanel.Dock = DockStyle.None;
            RockMechPanel.Visible = false;

            VentPanel.Dock = DockStyle.None;
            VentPanel.Visible = false;
        }

        private void bandedGridView11_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;


            if (e.Column.AbsoluteIndex == 1)
            {
                // if (View.GetRowCellValue(e.RowHandle, "col1wp").ToString().Contains("Sum") == true) 
                //e.DisplayText = "No Act.";
                // e.Appearance.ForeColor = Color.DarkGray;
                if (View.FocusedRowHandle != GridControl.InvalidRowHandle)
                {

                    if (View.GetRowCellValue(e.RowHandle, e.Column).ToString().Contains("Sum") == true)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                    }
                }
            }



            if (View.GetRowCellValue(e.RowHandle, "wp6").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 3)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp5").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 4)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp4").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 5)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp3").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 6)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp2").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 7)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp1").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 8)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wpnow").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 9)
                {
                    //e.Appearance.BackColor = Color.LightGray;
                    e.DisplayText = "No Act1.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }
        }


        string clickcol = "";
        string clickcolnum = "N";

        string clickRMS = "0";

        string clickAct = "0";

        private void bandedGridView11_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            WPLbl.Text = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[1]).ToString();
            label36.Text = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[2]).ToString();
            clickRMS = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[2]).ToString();


            clickAct = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[17]).ToString();

            clickcol = e.Column.ToString();

            clickcolnum = "N";


            clickcol = "0";


            if (e.Column.Name.ToString() == "Col1Day1")
            {
                clickcol = bandedGridView11.Columns[3].Caption.ToString().Substring(3, 2);
            }
            //WKLbl.Text = bandedGridView11.Columns[3].Caption.ToString();

            if (e.Column.Name.ToString() == "Col1Day2")
                clickcol = bandedGridView11.Columns[4].Caption.ToString().Substring(3, 2); ;
            // WKLbl.Text = bandedGridView11.Columns[4].Caption.ToString();

            if (e.Column.Name.ToString() == "Col1Day3")
                clickcol = bandedGridView11.Columns[5].Caption.ToString().Substring(3, 2); ;
            // WKLbl.Text = bandedGridView11.Columns[5].Caption.ToString();

            if (e.Column.Name.ToString() == "Col1Day4")
                clickcol = bandedGridView11.Columns[6].Caption.ToString().Substring(3, 2);
            //WKLbl.Text = bandedGridView11.Columns[8].Caption.ToString().Substring(3, 2);

            if (e.Column.Name.ToString() == "Col1Day5")
                clickcol = bandedGridView11.Columns[7].Caption.ToString().Substring(3, 2); ;
            //WKLbl.Text = bandedGridView11.Columns[6].Caption.ToString().Substring(3, 2);

            if (e.Column.Name.ToString() == "Col1Day6")
                clickcol = bandedGridView11.Columns[8].Caption.ToString().Substring(3, 2);
            // WKLbl.Text = bandedGridView11.Columns[8].Caption.ToString();

            if (e.Column.Name.ToString() == "Col1Day7")
            {
                clickcol = bandedGridView11.Columns[9].Caption.ToString().Substring(3, 2);
                clickcolnum = "Y";
            }
        }

        private void bandedGridView11_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.Name.ToString() == "col1RR")
            {
                if (Convert.ToDecimal(e.CellValue) < Convert.ToDecimal("70"))
                {
                    e.Appearance.ForeColor = Color.Green;
                }


                //if ((decimal)e.CellValue < Convert.ToInt32("70"))
                //{
                //    e.Appearance.ForeColor = Color.Green;
                //}

                if (Convert.ToDecimal(e.CellValue) > 70 && Convert.ToDecimal(e.CellValue) < 140)
                {
                    e.Appearance.ForeColor = Color.Orange;
                }

                if (Convert.ToDecimal(e.CellValue) > 140)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }


            //// 0 -- invisible
            if (e.CellValue != null && e.Column.ColumnType == typeof(decimal))
            {
                if ((decimal)e.CellValue == 0)
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                }
            }
            if (e.CellValue != null && e.Column.ColumnType == typeof(int))
            {
                //comented out
                //if (e.CellValue == "0")
                //{
                //    e.Appearance.ForeColor = e.Appearance.BackColor;
                //}
            }
        }

        private void gridControl6_DoubleClick(object sender, EventArgs e)
        {
            if (bandedGridView11.FocusedColumn.Name == "col1RR" || bandedGridView11.FocusedColumn.Name == "colLastVisit" || bandedGridView11.FocusedColumn.Name == "col1Wp" || bandedGridView11.FocusedColumn.Name == "colDaysSinceLastVisit" || bandedGridView11.FocusedColumn.Name == "ColActFFFF")
            {
                return;
            }

            RockEngFrm  FPMessagefrm = new RockEngFrm( );

            FPMessagefrm.WPLbl.Text = WPLbl.Text;
            FPMessagefrm.WkLbl.Text = clickcol;
            FPMessagefrm.WkLbl2.Text = clickcol;
            FPMessagefrm.RRLbl.Text = label36.Text;
            FPMessagefrm.EditLbl.Text = clickcolnum;
            FPMessagefrm.Cat12Txt.Text = clickRMS;
            FPMessagefrm.ActType = clickAct;

            FPMessagefrm.ShowDialog();
            LoadWalkAboutAssGrid();
        }

        private void gridControl3_DoubleClick(object sender, EventArgs e)
        {
            VentInspection FPMessagefrm = new VentInspection();
                FPMessagefrm.WPLbl.Text = WPLbl.Text;
                FPMessagefrm.WkLbl.Text = clickcol;
                FPMessagefrm.WkLbl2.Text = clickcol;
                FPMessagefrm.RRLbl.Text = label36.Text;
                FPMessagefrm.EditLbl.Text = clickcolnum;
                FPMessagefrm.Cat12Txt.Text = clickRMS;

                FPMessagefrm.ShowDialog();

            LoadVentAssGrid();
        }

        private void bandedGridView3_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            WPLbl.Text = bandedGridView3.GetRowCellValue(e.RowHandle, bandedGridView3.Columns[1]).ToString();
            label36.Text = bandedGridView3.GetRowCellValue(e.RowHandle, bandedGridView3.Columns[2]).ToString();
            clickRMS = bandedGridView3.GetRowCellValue(e.RowHandle, bandedGridView3.Columns[2]).ToString();

            clickcol = e.Column.ToString();

            clickcolnum = "N";


            clickcol = "0";


            if (e.Column.Name.ToString() == "Col1Day1a")
            {
                clickcol = bandedGridView3.Columns[3].Caption.ToString().Substring(3, 2);
            }
            //WKLbl.Text = bandedGridView11.Columns[3].Caption.ToString();

            if (e.Column.Name.ToString() == "Col1Day2a")
                clickcol = bandedGridView3.Columns[4].Caption.ToString().Substring(3, 2); ;
            // WKLbl.Text = bandedGridView11.Columns[4].Caption.ToString();

            if (e.Column.Name.ToString() == "Col1Day3a")
                clickcol = bandedGridView3.Columns[5].Caption.ToString().Substring(3, 2); ;
            // WKLbl.Text = bandedGridView11.Columns[5].Caption.ToString();

            if (e.Column.Name.ToString() == "Col1Day4a")
                clickcol = bandedGridView3.Columns[6].Caption.ToString().Substring(3, 2);
            //WKLbl.Text = bandedGridView11.Columns[8].Caption.ToString().Substring(3, 2);

            if (e.Column.Name.ToString() == "Col1Day5a")
                clickcol = bandedGridView3.Columns[7].Caption.ToString().Substring(3, 2); ;
            //WKLbl.Text = bandedGridView11.Columns[6].Caption.ToString().Substring(3, 2);

            if (e.Column.Name.ToString() == "Col1Day6a")
                clickcol = bandedGridView3.Columns[8].Caption.ToString().Substring(3, 2);
            // WKLbl.Text = bandedGridView11.Columns[8].Caption.ToString();

            if (e.Column.Name.ToString() == "Col1Day7a")
            {
                clickcol = bandedGridView3.Columns[9].Caption.ToString().Substring(3, 2);
                clickcolnum = "Y";
            }
        }

        private void bandedGridView3_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle < 10000000)
            {
                if (View.GetRowCellValue(e.RowHandle, "wp6").ToString() == "")
                {
                    if (e.Column.AbsoluteIndex == 3)
                    {
                        e.DisplayText = "No Act.";
                        e.Appearance.ForeColor = Color.DarkGray;
                        e.Appearance.BackColor = Color.Gainsboro;
                    }
                }

                if (View.GetRowCellValue(e.RowHandle, "wp5").ToString() == "")
                {
                    if (e.Column.AbsoluteIndex == 4)
                    {
                        e.DisplayText = "No Act.";
                        e.Appearance.ForeColor = Color.DarkGray;
                        e.Appearance.BackColor = Color.Gainsboro;
                    }
                }

                if (View.GetRowCellValue(e.RowHandle, "wp4").ToString() == "")
                {
                    if (e.Column.AbsoluteIndex == 5)
                    {
                        e.DisplayText = "No Act.";
                        e.Appearance.ForeColor = Color.DarkGray;
                        e.Appearance.BackColor = Color.Gainsboro;
                    }
                }

                if (View.GetRowCellValue(e.RowHandle, "wp3").ToString() == "")
                {
                    if (e.Column.AbsoluteIndex == 6)
                    {
                        e.DisplayText = "No Act.";
                        e.Appearance.ForeColor = Color.DarkGray;
                        e.Appearance.BackColor = Color.Gainsboro;
                    }
                }

                if (View.GetRowCellValue(e.RowHandle, "wp2").ToString() == "")
                {
                    if (e.Column.AbsoluteIndex == 7)
                    {
                        e.DisplayText = "No Act.";
                        e.Appearance.ForeColor = Color.DarkGray;
                        e.Appearance.BackColor = Color.Gainsboro;
                    }
                }

                if (View.GetRowCellValue(e.RowHandle, "wp1").ToString() == "")
                {
                    if (e.Column.AbsoluteIndex == 8)
                    {
                        e.DisplayText = "No Act.";
                        e.Appearance.ForeColor = Color.DarkGray;
                        e.Appearance.BackColor = Color.Gainsboro;
                    }
                }

                if (View.GetRowCellValue(e.RowHandle, "wpnow").ToString() == "")
                {
                    if (e.Column.AbsoluteIndex == 9)
                    {
                        //e.Appearance.BackColor = Color.LightGray;
                        e.DisplayText = "No Act.";
                        e.Appearance.ForeColor = Color.DarkGray;
                        e.Appearance.BackColor = Color.Gainsboro;
                    }
                }
            }
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            GeoInspFrm FPMessagefrm = new GeoInspFrm();
                FPMessagefrm.WPLbl.Text = WPLbl.Text;
                FPMessagefrm.WkLbl.Text = clickcol;
                FPMessagefrm.WkLbl2.Text = WKLbl.Text;
                FPMessagefrm.RRLbl.Text = label36.Text;

                FPMessagefrm.ShowDialog();
            LoadGeoInspGrid();
        }

        private void bandedGridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;

            if (View.GetRowCellValue(e.RowHandle, "wp6").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 3)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp5").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 4)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp4").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 5)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp3").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 6)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp2").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 7)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wp1").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 8)
                {
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }

            if (View.GetRowCellValue(e.RowHandle, "wpnow").ToString() == "")
            {
                if (e.Column.AbsoluteIndex == 9)
                {
                    //e.Appearance.BackColor = Color.LightGray;
                    e.DisplayText = "No Act.";
                    e.Appearance.ForeColor = Color.DarkGray;
                    e.Appearance.BackColor = Color.Gainsboro;
                }
            }
        }

        private void bandedGridView2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            WPLbl.Text = bandedGridView2.GetRowCellValue(e.RowHandle, bandedGridView2.Columns[1]).ToString();
            label36.Text = bandedGridView2.GetRowCellValue(e.RowHandle, bandedGridView2.Columns[2]).ToString();

            clickcol = e.Column.ToString();

            clickcol = "0";

            if (e.Column.Name.ToString() == "Col1Day1GS")
            {
                clickcol = "1";
                WKLbl.Text = bandedGridView2.Columns[3].Caption.ToString();
            }

            if (e.Column.Name.ToString() == "Col1Day2GS")
            {
                clickcol = "2";
                WKLbl.Text = bandedGridView2.Columns[4].Caption.ToString();
            }

            if (e.Column.Name.ToString() == "Col1Day3GS")
            {
                clickcol = "3";
                WKLbl.Text = bandedGridView2.Columns[5].Caption.ToString();
            }

            if (e.Column.Name.ToString() == "Col1Day4GS")
            {
                clickcol = "4";
                WKLbl.Text = bandedGridView2.Columns[6].Caption.ToString();
            }

            if (e.Column.Name.ToString() == "Col1Day5GS")
            {
                clickcol = "5";
                WKLbl.Text = bandedGridView2.Columns[7].Caption.ToString();
            }

            if (e.Column.Name.ToString() == "Col1Day6GS")
            {
                clickcol = "6";
                WKLbl.Text = bandedGridView2.Columns[8].Caption.ToString();
            }

            if (e.Column.Name.ToString() == "Col1Day7GS")
            {
                clickcol = "7";
                WKLbl.Text = bandedGridView2.Columns[9].Caption.ToString();
            }
        }

        private void bandedGridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.Name.ToString() == "col1RRGS")
            {
                if ((decimal)e.CellValue < 70)
                {
                    e.Appearance.ForeColor = Color.Green;
                }

                if ((decimal)e.CellValue > 70 && (decimal)e.CellValue < 140)
                {
                    e.Appearance.ForeColor = Color.Orange;
                }

                if ((decimal)e.CellValue > 140)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }


            //// 0 -- invisible
            if (e.CellValue != null && e.Column.ColumnType == typeof(decimal))
            {
                if ((decimal)e.CellValue == 0)
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                }
            }
            if (e.CellValue != null && e.Column.ColumnType == typeof(int))
            {
                if ((int)e.CellValue == 0)
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                }
            }
        }

        private void btnAddNote_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gcMOWorkplaces.Views.Count; i++)
            {
                GridView CurrentView = (GridView)gcMOWorkplaces.Views[i];

                if (CurrentView.Name == "gvWPLevel")
                {

                    MWDataManager.clsDataAccess _dbManSub = new MWDataManager.clsDataAccess();
                    _dbManSub.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                    for (int Row = 0; Row < CurrentView.RowCount; Row++)
                    {
                        string test = CurrentView.GetRowCellValue(Row, CurrentView.Columns["Checked"]).ToString();

                        if (CurrentView.GetRowCellValue(Row, CurrentView.Columns["Checked"]).ToString() == "Y")
                        {
                            _dbManSub.SqlStatement = _dbManSub.SqlStatement + "  insert into tbl_MoNotes values (  \r\n" +
                                "  '"+ CurrentView.GetRowCellValue(Row, CurrentView.Columns["WorkplaceID"]).ToString() + "',getdate(), '"+ TUserInfo.UserID +"'  )   \r\n\r\n";
                            
                        }
                    }

                    _dbManSub.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManSub.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManSub.ExecuteInstruction();

                    Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Notes saved", Color.CornflowerBlue);
                }

               
            }



            
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            //GridView CurrentView = (GridView)sender;

            //if (CurrentView.Name == "gvMoLevel")
            //{
            //    GridView Test = GetDetailView(CurrentView.FocusedRowHandle, 0);


            //}
        }

        private void gvMoLevel_RowCellClick(object sender, RowCellClickEventArgs e)
        {

        }

        private void gvMoLevel_DoubleClick(object sender, EventArgs e)
        {

            if (gvMoLevel.FocusedColumn.FieldName == "MainChecked")
            {
                if (gvMoLevel.GetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.FocusedColumn.FieldName).ToString() == "N")
                {
                    gvMoLevel.SetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.FocusedColumn.FieldName, "Y");
                    CheckWPforMain(gvMoLevel.GetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.Columns["sec"]).ToString(), "Y");
                    return;
                }

                if (gvMoLevel.GetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.FocusedColumn).ToString() == "Y")
                {
                    gvMoLevel.SetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.FocusedColumn, "N");
                    CheckWPforMain(gvMoLevel.GetRowCellValue(gvMoLevel.FocusedRowHandle, gvMoLevel.Columns["sec"]).ToString(), "N");
                    return;
                }
            }
        }

        private void gvWPLevel_DoubleClick(object sender, EventArgs e)
        {
            GridView currentView = (GridView)sender;

            if (currentView.FocusedColumn.FieldName == "Checked")
            {
                if (currentView.GetRowCellValue(currentView.FocusedRowHandle, currentView.FocusedColumn).ToString() == "N")
                {
                    currentView.SetRowCellValue(currentView.FocusedRowHandle, currentView.FocusedColumn, "Y");
                    return;
                }

                if (currentView.GetRowCellValue(currentView.FocusedRowHandle, currentView.FocusedColumn).ToString() == "Y")
                {
                    currentView.SetRowCellValue(currentView.FocusedRowHandle, currentView.FocusedColumn, "N");
                    return;
                }
            }
        }


        private void CheckWPforMain(string _section,string _Value)
        {

            for (int i = 0; i < gcMOWorkplaces.Views.Count; i++)
            {
                GridView CurrentView = (GridView)gcMOWorkplaces.Views[i];

                if (CurrentView.Name == "gvWPLevel")
                {
                    for (int subRow = 0; subRow < CurrentView.RowCount; subRow++)
                    {
                            if (CurrentView.GetRowCellValue(subRow, CurrentView.Columns["sec"]).ToString() == _section )
                            {
                                CurrentView.SetRowCellValue(subRow, CurrentView.Columns["Checked"], _Value);
                            }
                    }
                }


            }
        }

    }
}