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
using FastReport;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class ucGeology : Mineware.Systems.Global.ucBaseUserControl
    {
        clsDeptCapt _clsSetupCycles = new clsDeptCapt();

        string clickcol = "";
        string clickcolnum = "N";

        string clickRMS = "0";

        string clickAct = "0";

        Report theReportGeoSum = new Report();

        public ucGeology()
        {
            InitializeComponent();
        }

        private void ucGeology_Load(object sender, EventArgs e)
        {
            _clsSetupCycles.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            this.Cursor = Cursors.WaitCursor;
            LoadGeoInspGrid();
            this.Cursor = Cursors.Default;
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

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            GeoInspFrm FPMessagefrm = new GeoInspFrm();
            FPMessagefrm.WPLbl.Text = WPLbl.Text;
            FPMessagefrm.WkLbl.Text = clickcol;
            FPMessagefrm.WkLbl2.Text = WKLbl.Text;
            FPMessagefrm.RRLbl.Text = label36.Text;

            FPMessagefrm._theSystemDBTag = theSystemDBTag;
            FPMessagefrm._UserCurrentInfoConnection = UserCurrentInfo.Connection;


            FPMessagefrm.ShowDialog();
            LoadGeoInspGrid();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelwwk.Text = listBox2.SelectedItem.ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelsum1.Text = listBox1.SelectedItem.ToString();
        }

        private void labelwwk_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (labelwwk.Text != "")
            {

                MWDataManager.clsDataAccess _dbManWP = new MWDataManager.clsDataAccess();
                _dbManWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWP.SqlStatement = " select distinct(s1.reporttosectionid) mo from  " +
                                        "planning p, section s, section s1  where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth  " +
                                        " and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth   " +
                                        " and activity <> 1   " +
                                        " and Datepart(isowk, calendardate) = substring('" + labelwwk.Text + "',8,2) and year(calendardate) =  substring('" + labelwwk.Text + "',1,4)  " +
                                        " order by s1.reporttosectionid ";
                _dbManWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWP.ExecuteInstruction();

                DataTable dtwp = _dbManWP.ResultsDataTable;


                listBox1.Items.Add("");
                foreach (DataRow dr in dtwp.Rows)
                {
                    listBox1.Items.Add(dr["mo"].ToString());
                }

                listBox1.SelectedIndex = 0;




            }
        }

        private void labelsum1_TextChanged(object sender, EventArgs e)
        {
            if (labelsum1.Text == "")
                return;


            MWDataManager.clsDataAccess _dbManWP = new MWDataManager.clsDataAccess();
            _dbManWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManWP.SqlStatement = "declare @year1 varchar(10)  \r\n" +
                                    "declare @ww varchar(10)  \r\n" +
                                    "declare @mo varchar(10)  \r\n" +

                                    "set @year1 = substring('" + labelwwk.Text + "',1,4)   \r\n" +
                                    "set @ww = substring('" + labelwwk.Text + "',8,2)  \r\n" +
                                    "set @mo = '" + labelsum1.Text + "'  \r\n" +

                                    "select @mo mo, @year1 +' wk'+@ww www, '" + SysSettings.Banner + "' hh, * from (select '1' ll, 'RIF/RIH' lbl, workplace,   \r\n" +
                                    "case when cat25rate = 1 then 'O' else 'R' end as CC, cat25note  \r\n" +

                                    " from  [dbo].[tbl_DPT_GeoScienceInspection] where  \r\n" +
                                    "captyear = @year1 and captweek = convert(decimal(18,0),@ww) and workplace in (  \r\n" +

                                    "select distinct(description) aa from planning p, workplace w where p.workplaceid = w.workplaceid and  \r\n" +
                                    " year(calendardate) = @year1 and datepart(week,calendardate) = convert(decimal(18,0),@ww)  \r\n" +
                                    " and substring(sectionid,1,3) = @mo  \r\n" +
                                    " )  \r\n" +
                                    " and cat25rate <> 0 \r\n" +

                                    " union \r\n" +

                                    " select '2' ll, 'Fault' lbl, workplace,  \r\n" +
                                    "case when cat29rate = 1 then 'O' else 'R' end as CC, cat29note \r\n" +

                                    " from  [dbo].[tbl_DPT_GeoScienceInspection] where \r\n" +
                                    "captyear = @year1 and captweek = convert(decimal(18,0),@ww) and workplace in ( \r\n" +

                                    "select distinct(description) aa from planning p, workplace w where p.workplaceid = w.workplaceid and \r\n" +
                                    " year(calendardate) = @year1 and datepart(week,calendardate) = convert(decimal(18,0),@ww)  \r\n" +
                                    " and substring(sectionid,1,3) = @mo \r\n" +
                                    " ) \r\n" +
                                    " and cat29rate <> 0 \r\n" +

                                    "  union \r\n" +

                                    " select '3' ll, 'Dyke' lbl, workplace, \r\n" +
                                    "case when cat30rate = 1 then 'O' else 'R' end as CC, cat30note \r\n" +

                                    "from  [dbo].[tbl_DPT_GeoScienceInspection] where \r\n" +
                                    "captyear = @year1 and captweek = convert(decimal(18,0),@ww) and workplace in ( \r\n" +

                                    "select distinct(description) aa from planning p, workplace w where p.workplaceid = w.workplaceid and \r\n" +
                                    " year(calendardate) = @year1 and datepart(week,calendardate) = convert(decimal(18,0),@ww)  \r\n" +
                                    " and substring(sectionid,1,3) = @mo \r\n" +
                                    " ) \r\n" +
                                    " and cat30rate <> 0 \r\n" +

                                    "   union \r\n" +

                                    " select '4' ll, 'Sill' lbl, workplace,  \r\n" +
                                    "case when cat31rate = 1 then 'O' else 'R' end as CC, cat31note \r\n" +

                                    " from  [dbo].[tbl_DPT_GeoScienceInspection] where \r\n" +
                                    "captyear = @year1 and captweek = convert(decimal(18,0),@ww) and workplace in ( \r\n" +

                                    "select distinct(description) aa from planning p, workplace w where p.workplaceid = w.workplaceid and \r\n" +
                                    " year(calendardate) = @year1 and datepart(week,calendardate) = convert(decimal(18,0),@ww)  \r\n" +
                                    " and substring(sectionid,1,3) = @mo \r\n" +
                                    " ) \r\n" +
                                    " and cat31rate <> 0 \r\n" +

                                    "  union \r\n" +

                                    " select '5' ll, 'Reef Position' lbl, workplace, \r\n" +
                                    "case when cat32rate = 1 then 'O' else 'R' end as CC, cat32note \r\n" +

                                    " from  [dbo].[tbl_DPT_GeoScienceInspection] where \r\n" +
                                    "captyear = @year1 and captweek = convert(decimal(18,0),@ww) and workplace in ( \r\n" +

                                    "select distinct(description) aa from planning p, workplace w where p.workplaceid = w.workplaceid and \r\n" +
                                    " year(calendardate) = @year1 and datepart(week,calendardate) = convert(decimal(18,0),@ww)  \r\n" +
                                    " and substring(sectionid,1,3) = @mo \r\n" +
                                    " ) \r\n" +
                                    " and cat32rate <> 0 \r\n" +

                                    "  union \r\n" +

                                    " select '6' ll, 'Stope Width' lbl, workplace,  \r\n" +
                                    "case when cat33rate = 1 then 'O' else 'R' end as CC, cat33note \r\n" +

                                    "from  [dbo].[tbl_DPT_GeoScienceInspection] where \r\n" +
                                    "captyear = @year1 and captweek = convert(decimal(18,0),@ww) and workplace in ( \r\n" +

                                    "select distinct(description) aa from planning p, workplace w where p.workplaceid = w.workplaceid and \r\n" +
                                    " year(calendardate) = @year1 and datepart(week,calendardate) = convert(decimal(18,0),@ww) \r\n" +
                                    " and substring(sectionid,1,3) = @mo \r\n" +
                                    " ) \r\n" +
                                    " and cat33rate <> 0) a \r\n";
            _dbManWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWP.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWP.ResultsTableName = "REDetail2";
            _dbManWP.ExecuteInstruction();



            DataSet Data1 = new DataSet();
            Data1.Tables.Add(_dbManWP.ResultsDataTable);

            theReportGeoSum.RegisterData(Data1);




            theReportGeoSum.Load(TGlobalItems.ReportsFolder + "\\GeolSumReport.frx");
            //theReportGeoSum.Design();

            previewControl3.Clear();
            theReportGeoSum.Prepare();
            theReportGeoSum.Preview = previewControl3;
            theReportGeoSum.ShowPrepared();




        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
