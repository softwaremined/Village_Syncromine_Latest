using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using Mineware.Systems.Global;
using DevExpress.XtraScheduler.Commands;
using Mineware.Systems.GlobalConnect;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.Production.SysAdminScreens.SetupCycles;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class ucVentilation : Mineware.Systems.Global.ucBaseUserControl
    {
        clsDeptCapt _clsSetupCycles = new clsDeptCapt();

       

        string clickcol = "";
        string clickcolnum = "N";

        string clickRMS = "0";

        string clickAct = "0";

        public ucVentilation()
        {
            InitializeComponent();
        }

        private void ucVentilation_Load(object sender, EventArgs e)
        {
            _clsSetupCycles.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            this.Cursor = Cursors.WaitCursor;
            LoadVentAssGrid();
            this.Cursor = Cursors.Default;
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
            ColActVS.FieldName = "wpnow";

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

        private void gridControl3_DoubleClick(object sender, EventArgs e)
        {
            
            VentInspection FPMessagefrm = new VentInspection();
            FPMessagefrm.WPLbl.Text = WPLbl.Text;
            FPMessagefrm.WkLbl.Text = clickcol;
            FPMessagefrm.WkLbl2.Text = clickcol;
            FPMessagefrm.RRLbl.Text = label36.Text;
            FPMessagefrm.EditLbl.Text = clickcolnum;
            FPMessagefrm.Cat12Txt.Text = clickRMS;
            FPMessagefrm.theSystemDBTag = theSystemDBTag;
            FPMessagefrm.UserCurrentInfoConnection = UserCurrentInfo.Connection;
            FPMessagefrm.ShowDialog();

            LoadVentAssGrid();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl3_Click(object sender, EventArgs e)
        {

        }
    }
}
