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
using DevExpress.XtraGrid;
using Mineware.Systems.Production.SysAdminScreens.SetupCycles;
using FastReport;
using System.IO;

namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    public partial class ucRockEng : Mineware.Systems.Global.ucBaseUserControl
    {
        clsDeptCapt _clsSetupCycles = new clsDeptCapt();

        string clickcol = "";
        string clickcolnum = "N";

        string clickRMS = "0";

        string clickAct = "0";

        Report RockSumReport = new Report();

        public Image Base64ToImage(string base64String, string wwww1)
        {
            // Convert Base64 String to byte[]
            string s;

            s = base64String.Trim().Replace(" ", "+");
            s = base64String.Trim().Replace("_", "/");

            if (s.Length % 4 > 0)
                s = s.PadRight(s.Length + 4 - s.Length % 4, '=');

            decimal aa = s.Length;



            byte[] imageBytes = Convert.FromBase64String(s);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);


            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);


            MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
            _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManImage.SqlStatement = "update [dbo].[tbl_DPT_RockMechInspection] set picture = '" + s + "' where workplace = '" + wwww1 + "' and (captyear*100)+captweek = (select max(captyear*100+captweek) from  [dbo].[tbl_DPT_RockMechInspection] where workplace = '" + wwww1 + "') ";
            _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManImage.ResultsTableName = "Image";
            _dbManImage.ExecuteInstruction();


            return image;

        }

        public ucRockEng()
        {
            InitializeComponent();
        }

        private void ucRockEng_Load(object sender, EventArgs e)
        {
            _clsSetupCycles.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            this.Cursor = Cursors.WaitCursor;
            LoadWalkAboutAssGrid();
            this.Cursor = Cursors.Default;
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


            //Need Help
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


        string Section = "";
        private void bandedGridView11_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            WPLbl.Text = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[1]).ToString();
            label36.Text = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[2]).ToString();
            clickRMS = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[2]).ToString();

            Section = bandedGridView11.GetRowCellValue(e.RowHandle, bandedGridView11.Columns[0]).ToString();
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

            RockEngFrm FPMessagefrm = new RockEngFrm();

            FPMessagefrm.WPLbl.Text = WPLbl.Text;
            FPMessagefrm.WkLbl.Text = clickcol;
            FPMessagefrm.WkLbl2.Text = clickcol;
            FPMessagefrm.SecLbl.Text = Section;
            FPMessagefrm.RRLbl.Text = label36.Text;
            FPMessagefrm.EditLbl.Text = clickcolnum;
            FPMessagefrm.Cat12Txt.Text = clickRMS;
            FPMessagefrm.ActType = clickAct;

            FPMessagefrm._theSystemDBTag = theSystemDBTag;
            FPMessagefrm._UserCurrentInfoConnection = UserCurrentInfo.Connection;

            FPMessagefrm.ShowDialog();//so you dont need to put this, Perfect the way you doing it

            LoadWalkAboutAssGrid();
        }

        private void listBoxWk_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelWk.Text = listBoxWk.SelectedItem.ToString();
        }

        private void listBoxSum_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelSum.Text = listBoxSum.SelectedItem.ToString();
        }

        private void labelWk_TextChanged(object sender, EventArgs e)
        {
            listBoxSum.Items.Clear();
            if (labelWk.Text != "")
            {

                MWDataManager.clsDataAccess _dbManWP = new MWDataManager.clsDataAccess();
                _dbManWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWP.SqlStatement = " select distinct(s1.reporttosectionid) mo from  " +
                                        "planning p, section s, section s1  where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth  " +
                                        " and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth   " +
                                        " and activity <> 1   " +
                                        " and Datepart(isowk, calendardate) = substring('" + labelWk.Text + "',8,2) and year(calendardate) =  substring('" + labelWk.Text + "',1,4)  " +
                                        " order by s1.reporttosectionid ";
                _dbManWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWP.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWP.ExecuteInstruction();

                DataTable dtwp = _dbManWP.ResultsDataTable;


                listBoxSum.Items.Add("");
                foreach (DataRow dr in dtwp.Rows)
                {
                    listBoxSum.Items.Add(dr["mo"].ToString());
                }

                listBoxSum.SelectedIndex = 0;
            }
        }

        private void labelSum_TextChanged(object sender, EventArgs e)
        {
            if (labelSum.Text != "")
            {
                Cursor = Cursors.WaitCursor;

                string Rate = "1";

                MWDataManager.clsDataAccess _dbManWP = new MWDataManager.clsDataAccess();
                _dbManWP.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                 _dbManWP.SqlStatement = "select distinct(aa)+ '-Summary' sss from ( " +
                                            "select rtrim(substring(description,1,4))+' '+rtrim(substring(GridCode,1,4)) aa from " +
                                            "planning p, workplace w, Section s1 , section s2  \r\n"+
                                            " where p.workplaceid = w.workplaceid and  p.activity <> 1  \r\n" +
                                            " and p.sectionid = s1.sectionid and p.prodmonth = s1.prodmonth  \r\n" +
                                            " and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.Prodmonth  \r\n" +
                                            "and Datepart(isowk, calendardate) = substring('" + labelWk.Text + "',8,2) and year(calendardate) =  substring('" + labelWk.Text + "',1,4) " +
                                            "and s2.ReportToSectionid = '" + labelSum.Text + "' ) a order by aa+ '-Summary' ";
                    _dbManWP.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManWP.queryReturnType = MWDataManager.ReturnType.DataTable;                   

                _dbManWP.ExecuteInstruction();


                DataTable dtwp = _dbManWP.ResultsDataTable;

                string sum1 = "            ";
                string sum2 = "            ";
                string sum3 = "            ";
                string sum4 = "            ";
                string sum5 = "            ";
                string sum6 = "            ";
                string sum7 = "            ";
                string sum8 = "            ";
                string sum9 = "            ";
                string sum10 = "            ";
                string sum11 = "            ";
                string sum12 = "            ";


                if (dtwp.Rows.Count > 0)
                    sum1 = _dbManWP.ResultsDataTable.Rows[0]["sss"].ToString();
                if (dtwp.Rows.Count > 1)
                    sum2 = _dbManWP.ResultsDataTable.Rows[1]["sss"].ToString();
                if (dtwp.Rows.Count > 2)
                    sum3 = _dbManWP.ResultsDataTable.Rows[2]["sss"].ToString();
                if (dtwp.Rows.Count > 3)
                    sum4 = _dbManWP.ResultsDataTable.Rows[3]["sss"].ToString();
                if (dtwp.Rows.Count > 4)
                    sum5 = _dbManWP.ResultsDataTable.Rows[4]["sss"].ToString();
                if (dtwp.Rows.Count > 5)
                    sum6 = _dbManWP.ResultsDataTable.Rows[5]["sss"].ToString();
                if (dtwp.Rows.Count > 6)
                    sum7 = _dbManWP.ResultsDataTable.Rows[6]["sss"].ToString();
                if (dtwp.Rows.Count > 7)
                    sum8 = _dbManWP.ResultsDataTable.Rows[7]["sss"].ToString();

                if (dtwp.Rows.Count > 8)
                    sum9 = _dbManWP.ResultsDataTable.Rows[8]["sss"].ToString();
                if (dtwp.Rows.Count > 9)
                    sum10 = _dbManWP.ResultsDataTable.Rows[9]["sss"].ToString();
                if (dtwp.Rows.Count > 10)
                    sum11 = _dbManWP.ResultsDataTable.Rows[10]["sss"].ToString();
                if (dtwp.Rows.Count > 11)
                    sum12 = _dbManWP.ResultsDataTable.Rows[11]["sss"].ToString();

                DataTable dtMoreSum = new DataTable();
                DataSet dsMoreSum = new DataSet();

                dtMoreSum.Clear();
                dsMoreSum.Clear();


                MWDataManager.clsDataAccess _dbManMorePage = new MWDataManager.clsDataAccess();
                _dbManMorePage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                if (dtwp.Rows.Count > 8)
                    _dbManMorePage.SqlStatement = "Select 'Yes' MoreSumFlag ";
                if (dtwp.Rows.Count <= 8)
                    _dbManMorePage.SqlStatement = "Select 'No' MoreSumFlag ";
                _dbManMorePage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMorePage.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMorePage.ResultsTableName = "MoreSum";
                _dbManMorePage.ExecuteInstruction();

                dtMoreSum = _dbManMorePage.ResultsDataTable;


                dsMoreSum.Tables.Add(dtMoreSum);



                // get image 1
                MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                            " and workplace = '" + sum1 + "' ";
                _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManImage.ExecuteInstruction();


                string Sum1Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                string Sum1Tab = "";
                if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                {

                    if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                        {
                            PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum1);
                            PicBox.Image.Save(Application.StartupPath + "\\" + "Sum1.bmp");
                            Sum1Image = Application.StartupPath + "\\" + "Sum1.bmp";
                            Sum1Tab = "Y";
                        }

                    }
                }

                MWDataManager.clsDataAccess _dbManImageRE1 = new MWDataManager.clsDataAccess();
                _dbManImageRE1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManImageRE1.SqlStatement = " update [dbo].[tbl_DPT_RockMechInspection]  set Cat12Rate =  replace(Cat12Rate,'.','') where Cat12Rate like '%.' " +
                                                 "select top(1) * from (Select 1 nn, picture, '" + Sum1Image + "' pp, '" + Sum1Tab + "' SumTab, '" + sum1 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum1 + "' " +
                                                "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                " " +
                                                "union select	2 nn, '', '', '' SumTab, '" + sum1 + "' name1) a order by nn ";
                _dbManImageRE1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManImageRE1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManImageRE1.ResultsTableName = "RESum1";
                _dbManImageRE1.ExecuteInstruction();

                DataSet ReportDS1Sum = new DataSet();
                ReportDS1Sum.Tables.Add(_dbManImageRE1.ResultsDataTable);



                string Header1 = "";
                string Header2 = "";
                string Header3 = "";
                string Header4 = "";
                string Header5 = "";
                string Header6 = "";
                string Header7 = "";
                string Header8 = "";
                string Header9 = "";
                string Header10 = "";
                string Header11 = "";
                string Header12 = "";
                string Header13 = "";
                string Header14 = "";
                string Header15 = "";
                string Header16 = "";
                string Header17 = "";
                string Header18 = "";
                string Header19 = "";
                string Header20 = "";






                // Sum1 Detail

                if (SysSettings.Banner == "Mponeng" || SysSettings.Banner == "Tau Tona" || SysSettings.Banner == "Savuka")
                {
                    
                    Header1 = "Stoping Width";
                    Header2 = "Siding Depth";
                    Header3 = "Panel Length";
                    Header4 = "Distance To Pillar";
                    Header5 = "Geological Feature";
                    Header6 = "Off Reef";
                    Header7 = "Lead Lag Top";
                    Header8 = "Lead Lag Bottom";
                    Header9 = "Gully Direction";
                    Header10 = "Ground Conditions";
                    Header11 = "Panel Face Shape";
                    Header12 = "Panel Rating";
                    Header13 = "New /Restart Panel";
                    Header14 = "Checklist Submitted";
                    Header15 = "2nd Escape";
                    Header16 = "Support TYpe";
                    Header17 = "Standard Applicable";
                    Header18 = "Special Support Rec.";
                    Header19 = "Special Area Support Rec.";


                    MWDataManager.clsDataAccess _dbManImageRE1Detail = new MWDataManager.clsDataAccess();
                    _dbManImageRE1Detail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE1Detail.SqlStatement = "select * from (select  \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (   \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and  convert(Decimal(18,2),isnull(cat12rate,0)) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like( rtrim(substring('" + sum1 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum1 + "',4,3))))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE1Detail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE1Detail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE1Detail.ResultsTableName = "REDetail1";
                    _dbManImageRE1Detail.ExecuteInstruction();

                    DataSet ReportDS1Detail = new DataSet();
                    ReportDS1Detail.Tables.Add(_dbManImageRE1Detail.ResultsDataTable);


                    // workplace2
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum2 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum2Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum2Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum2);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum2.bmp");
                                Sum2Image = Application.StartupPath + "\\" + "Sum2.bmp";
                                Sum2Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE2 = new MWDataManager.clsDataAccess();
                    _dbManImageRE2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE2.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum2Image + "' pp, '" + Sum2Tab + "' SumTab, '" + sum2 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum2 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum2 + "' name1) a order by nn ";
                    _dbManImageRE2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE2.ResultsTableName = "RESum2";
                    _dbManImageRE2.ExecuteInstruction();

                    DataSet ReportDSSum2 = new DataSet();
                    ReportDSSum2.Tables.Add(_dbManImageRE2.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE1Detail2 = new MWDataManager.clsDataAccess();
                    _dbManImageRE1Detail2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE1Detail2.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%'   and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%'   and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%'    and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%'    and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'   and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and    workplace like (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%'   and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+ "',4,3)))+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like( rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum1+"',4,3)))+'%'   and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' +convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like rtrim('"+sum2+"',1,3) +''+ ltrim(rtrim(substring('"+sum2+"',4,3)))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE1Detail2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE1Detail2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE1Detail2.ResultsTableName = "REDetail2";
                    _dbManImageRE1Detail2.ExecuteInstruction();

                    DataSet ReportDSDetail2 = new DataSet();
                    ReportDSDetail2.Tables.Add(_dbManImageRE1Detail2.ResultsDataTable);


                    // workplace3
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum3 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum3Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum3Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum3);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum3.bmp");
                                Sum3Image = Application.StartupPath + "\\" + "Sum3.bmp";
                                Sum3Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE3 = new MWDataManager.clsDataAccess();
                    _dbManImageRE3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE3.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum3Image + "' pp, '" + Sum3Tab + "' SumTab, '" + sum3 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum3 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum3 + "' name1) a order by nn ";
                    _dbManImageRE3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE3.ResultsTableName = "RESum3";
                    _dbManImageRE3.ExecuteInstruction();

                    DataSet ReportDSSum3 = new DataSet();
                    ReportDSSum3.Tables.Add(_dbManImageRE3.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE3Detail3 = new MWDataManager.clsDataAccess();
                    _dbManImageRE3Detail3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE3Detail3.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat16rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum3 + "',1,len('" + sum3 + "')4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE3Detail3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE3Detail3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE3Detail3.ResultsTableName = "REDetail3";
                    _dbManImageRE3Detail3.ExecuteInstruction();

                    DataSet ReportDSDetail3 = new DataSet();
                    ReportDSDetail3.Tables.Add(_dbManImageRE3Detail3.ResultsDataTable);


                    // workplace4
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum4 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum4Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum4Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum4);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum4.bmp");
                                Sum4Image = Application.StartupPath + "\\" + "Sum4.bmp";
                                Sum4Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE4 = new MWDataManager.clsDataAccess();
                    _dbManImageRE4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE4.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum4Image + "' pp, '" + Sum4Tab + "' SumTab, '" + sum4 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum4 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum4 + "' name1) a order by nn ";
                    _dbManImageRE4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE4.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE4.ResultsTableName = "RESum4";
                    _dbManImageRE4.ExecuteInstruction();

                    DataSet ReportDSSum4 = new DataSet();
                    ReportDSSum4.Tables.Add(_dbManImageRE4.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE4Detail4 = new MWDataManager.clsDataAccess();
                    _dbManImageRE4Detail4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE4Detail4.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' +convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' +convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum4 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''   \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''   \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE4Detail4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE4Detail4.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE4Detail4.ResultsTableName = "REDetail4";
                    _dbManImageRE4Detail4.ExecuteInstruction();

                    DataSet ReportDSDetail4 = new DataSet();
                    
                    ReportDSDetail4.Tables.Add(_dbManImageRE4Detail4.ResultsDataTable);


                    // workplace5
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum5 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum5Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum5Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum5);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum5.bmp");
                                Sum5Image = Application.StartupPath + "\\" + "Sum5.bmp";
                                Sum5Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE5 = new MWDataManager.clsDataAccess();
                    _dbManImageRE5.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE5.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum5Image + "' pp, '" + Sum5Tab + "' SumTab, '" + sum5 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum5 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum5 + "' name1) a order by nn ";
                    _dbManImageRE5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE5.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE5.ResultsTableName = "RESum5";
                    _dbManImageRE5.ExecuteInstruction();

                    DataSet ReportDSSum5 = new DataSet();
                    ReportDSSum5.Tables.Add(_dbManImageRE5.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE5Detail5 = new MWDataManager.clsDataAccess();
                    _dbManImageRE5Detail5.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE5Detail5.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum5 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE5Detail5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE5Detail5.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE5Detail5.ResultsTableName = "REDetail5";
                    _dbManImageRE5Detail5.ExecuteInstruction();

                    DataSet ReportDSDetail5 = new DataSet();
                    ReportDSDetail5.Tables.Add(_dbManImageRE5Detail5.ResultsDataTable);



                    // workplace6
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum6 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum6Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum6Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum6);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum6.bmp");
                                Sum6Image = Application.StartupPath + "\\" + "Sum6.bmp";
                                Sum6Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE6 = new MWDataManager.clsDataAccess();
                    _dbManImageRE6.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE6.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum6Image + "' pp, '" + Sum6Tab + "' SumTab, '" + sum6 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum6 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum6 + "' name1) a order by nn ";
                    _dbManImageRE6.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE6.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE6.ResultsTableName = "RESum6";
                    _dbManImageRE6.ExecuteInstruction();

                    DataSet ReportDSSum6 = new DataSet();
                    ReportDSSum6.Tables.Add(_dbManImageRE6.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE6Detail6 = new MWDataManager.clsDataAccess();
                    _dbManImageRE6Detail6.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE6Detail6.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum6 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE6Detail6.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE6Detail6.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE6Detail6.ResultsTableName = "REDetail6";
                    _dbManImageRE6Detail6.ExecuteInstruction();

                    DataSet ReportDSDetail6 = new DataSet();
                    ReportDSDetail6.Tables.Add(_dbManImageRE6Detail6.ResultsDataTable);


                    // workplace7
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' +convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum7 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum7Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum7Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum7);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum7.bmp");
                                Sum7Image = Application.StartupPath + "\\" + "Sum7.bmp";
                                Sum7Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE7 = new MWDataManager.clsDataAccess();
                    _dbManImageRE7.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE7.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum7Image + "' pp, '" + Sum7Tab + "' SumTab, '" + sum7 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum7 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum7 + "' name1) a order by nn ";
                    _dbManImageRE7.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE7.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE7.ResultsTableName = "RESum7";
                    _dbManImageRE7.ExecuteInstruction();

                    DataSet ReportDSSum7 = new DataSet();
                    ReportDSSum7.Tables.Add(_dbManImageRE7.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE7Detail7 = new MWDataManager.clsDataAccess();
                    _dbManImageRE7Detail7.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE7Detail7.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum7 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE7Detail7.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE7Detail7.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE7Detail7.ResultsTableName = "REDetail7";
                    _dbManImageRE7Detail7.ExecuteInstruction();

                    DataSet ReportDSDetail7 = new DataSet();
                    ReportDSDetail7.Tables.Add(_dbManImageRE7Detail7.ResultsDataTable);


                    // workplace8
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum8 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum8Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum8Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum8);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum8.bmp");
                                Sum8Image = Application.StartupPath + "\\" + "Sum8.bmp";
                                Sum8Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE8 = new MWDataManager.clsDataAccess();
                    _dbManImageRE8.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE8.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum8Image + "' pp, '" + Sum8Tab + "' SumTab, '" + sum8 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum8 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum8 + "' name1) a order by nn ";
                    _dbManImageRE8.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE8.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE8.ResultsTableName = "RESum8";
                    _dbManImageRE8.ExecuteInstruction();

                    DataSet ReportDSSum8 = new DataSet();
                    ReportDSSum8.Tables.Add(_dbManImageRE8.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE8Detail8 = new MWDataManager.clsDataAccess();
                    _dbManImageRE8Detail8.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE8Detail8.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum8 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE8Detail8.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE8Detail8.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE8Detail8.ResultsTableName = "REDetail8";
                    _dbManImageRE8Detail8.ExecuteInstruction();

                    DataSet ReportDSDetail8 = new DataSet();
                    ReportDSDetail8.Tables.Add(_dbManImageRE8Detail8.ResultsDataTable);



                    // workplace9
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' +convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum9 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum9Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum9Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum9);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum9.bmp");
                                Sum9Image = Application.StartupPath + "\\" + "Sum9.bmp";
                                Sum9Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE9 = new MWDataManager.clsDataAccess();
                    _dbManImageRE9.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE9.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum9Image + "' pp, '" + Sum9Tab + "' SumTab, '" + sum9 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum9 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum9 + "' name1) a order by nn ";
                    _dbManImageRE9.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE9.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE9.ResultsTableName = "RESum9";
                    _dbManImageRE9.ExecuteInstruction();

                    DataSet ReportDSSum9 = new DataSet();
                    ReportDSSum9.Tables.Add(_dbManImageRE9.ResultsDataTable);

                    //if (sum9)

                    MWDataManager.clsDataAccess _dbManImageRE9Detail9 = new MWDataManager.clsDataAccess();
                    _dbManImageRE9Detail9.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE9Detail9.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +



                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE9Detail9.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE9Detail9.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE9Detail9.ResultsTableName = "REDetail9";
                    _dbManImageRE9Detail9.ExecuteInstruction();

                    DataSet ReportDSDetail9 = new DataSet();
                    ReportDSDetail9.Tables.Add(_dbManImageRE9Detail9.ResultsDataTable);

                    // workplace10
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum10 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum10Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum10Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum10);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum10.bmp");
                                Sum10Image = Application.StartupPath + "\\" + "Sum10.bmp";
                                Sum10Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE10 = new MWDataManager.clsDataAccess();
                    _dbManImageRE10.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE10.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum10Image + "' pp, '" + Sum10Tab + "' SumTab, '" + sum10 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum10 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum10 + "' name1) a order by nn ";
                    _dbManImageRE10.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE10.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE10.ResultsTableName = "RESum10";
                    _dbManImageRE10.ExecuteInstruction();

                    DataSet ReportDSSum10 = new DataSet();
                    ReportDSSum10.Tables.Add(_dbManImageRE10.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE10Detail10 = new MWDataManager.clsDataAccess();
                    _dbManImageRE10Detail10.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE10Detail10.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum10 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE10Detail10.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE10Detail10.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE10Detail10.ResultsTableName = "REDetail10";
                    _dbManImageRE10Detail10.ExecuteInstruction();

                    DataSet ReportDSDetail10 = new DataSet();
                    ReportDSDetail10.Tables.Add(_dbManImageRE10Detail10.ResultsDataTable);

                    // workplace11
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum11 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum11Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum11Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum11);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum11.bmp");
                                Sum11Image = Application.StartupPath + "\\" + "Sum11.bmp";
                                Sum11Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE11 = new MWDataManager.clsDataAccess();
                    _dbManImageRE11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE11.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum11Image + "' pp, '" + Sum11Tab + "' SumTab, '" + sum11 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum11 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum11 + "' name1) a order by nn ";
                    _dbManImageRE11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE11.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE11.ResultsTableName = "RESum11";
                    _dbManImageRE11.ExecuteInstruction();

                    DataSet ReportDSSum11 = new DataSet();
                    ReportDSSum11.Tables.Add(_dbManImageRE11.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE11Detail11 = new MWDataManager.clsDataAccess();
                    _dbManImageRE11Detail11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE11Detail11.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum11 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE11Detail11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE11Detail11.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE11Detail11.ResultsTableName = "REDetail11";
                    _dbManImageRE11Detail11.ExecuteInstruction();

                    DataSet ReportDSDetail11 = new DataSet();
                    ReportDSDetail11.Tables.Add(_dbManImageRE11Detail11.ResultsDataTable);


                    // workplace12
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum12 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum12Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum12Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum12);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum12.bmp");
                                Sum12Image = Application.StartupPath + "\\" + "Sum12.bmp";
                                Sum12Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE12 = new MWDataManager.clsDataAccess();
                    _dbManImageRE12.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE12.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum12Image + "' pp, '" + Sum12Tab + "' SumTab, '" + sum12 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum12 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum12 + "' name1) a order by nn ";
                    _dbManImageRE12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE12.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE12.ResultsTableName = "RESum12";
                    _dbManImageRE12.ExecuteInstruction();

                    DataSet ReportDSSum12 = new DataSet();
                    ReportDSSum12.Tables.Add(_dbManImageRE12.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE12Detail12a = new MWDataManager.clsDataAccess();
                    _dbManImageRE12Detail12a.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE12Detail12a.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                         " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat18rate) cat18rate , '' cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum12 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 19, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE12Detail12a.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE12Detail12a.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE12Detail12a.ResultsTableName = "REDetail12";
                    _dbManImageRE12Detail12a.ExecuteInstruction();

                    DataSet ReportDSDetail12a = new DataSet();
                    ReportDSDetail12a.Tables.Add(_dbManImageRE12Detail12a.ResultsDataTable);

                    ///////////////////////////////////////////////
                    RockSumReport.RegisterData(ReportDS1Sum);
                    RockSumReport.RegisterData(ReportDS1Detail);

                    RockSumReport.RegisterData(ReportDSSum2);
                    RockSumReport.RegisterData(ReportDSDetail2);

                    RockSumReport.RegisterData(ReportDSSum3);
                    RockSumReport.RegisterData(ReportDSDetail3);

                    RockSumReport.RegisterData(ReportDSSum4);
                    RockSumReport.RegisterData(ReportDSDetail4);

                    RockSumReport.RegisterData(ReportDSSum5);
                    RockSumReport.RegisterData(ReportDSDetail5);

                    RockSumReport.RegisterData(ReportDSSum6);
                    RockSumReport.RegisterData(ReportDSDetail6);

                    RockSumReport.RegisterData(ReportDSSum7);
                    RockSumReport.RegisterData(ReportDSDetail7);

                    RockSumReport.RegisterData(ReportDSSum8);
                    RockSumReport.RegisterData(ReportDSDetail8);

                    RockSumReport.RegisterData(ReportDSSum9);
                    RockSumReport.RegisterData(ReportDSDetail9);

                    RockSumReport.RegisterData(ReportDSSum10);
                    RockSumReport.RegisterData(ReportDSDetail10);

                    RockSumReport.RegisterData(ReportDSSum11);
                    RockSumReport.RegisterData(ReportDSDetail11);

                    RockSumReport.RegisterData(ReportDSSum12);
                    RockSumReport.RegisterData(ReportDSDetail12a);
                }
                else
                {
                    //Header1 = "Stoping Width";
                    //Header2 = "Siding Depth";
                    //Header3 = "Panel Length";
                    //Header4 = "Distance To Pillar";
                    //Header5 = "Geological Feature";
                    //Header6 = "Off Reef";
                    //Header7 = "Lead Lag Top";
                    //Header8 = "Lead Lag Bottom";
                    //Header9 = "Gully Direction";
                    //Header10 = "Ground Conditions";
                    //Header11 = "Panel Face Shape";
                    //Header12 = "Panel Rating";
                    //Header13 = "New /Restart Panel";
                    //Header14 = "Checklist Submitted";
                    //Header15 = "2nd Escape";
                    //Header16 = "Support TYpe";
                    //Header17 = "Standard Applicable";
                    //Header18 = "Special Support Rec.";
                    //Header19 = "Special Area Support Rec.";

                    Header1 = "Stope Width";
                    Header2 = "Lead Lag";
                    Header3 = "Siding Depth";
                    Header4 = "Siding Lag";
                    Header5 = "Gully Lead (max 2m including siding)";
                    Header6 = "Gully Direction";
                    Header7 = "Face Length (max 40m including siding)";
                    Header8 = "Face Shape (Panel Straight)";
                    Header9 = "Approach angle to structure (35°)";
                    Header10 = "Escape Gully to Face";
                    Header11 = "Pillars (Cut as Planned)";
                    Header12 = "Panel Rating";
                    Header13 = "New / Restart Panel";
                    Header14 = "Checklist submitted Y/N";
                    Header15 = "aaa";
                    Header16 = "Panel length 3m beyond Gully position";
                    Header17 = "Support Rec. (Structure, Ground Cond.)";
                    Header18 = "Ledging Blueprint reflected on Plan";
                    //Header15 = "Gully lag (min 2m)";
                    // Header16 = "Should width from centre line (max 5m)";
                    // Header17 = "Support Rec. (Structure, Ground Cond.)";
                    //  Header18 = "Ledging Blueprint reflected on Plan";


                    MWDataManager.clsDataAccess _dbManImageRE1Detail = new MWDataManager.clsDataAccess();
                    _dbManImageRE1Detail.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE1Detail.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        " convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +
                                                      // " and workplace like(rtrim(substring('" +sum1 + "', 1, 3))) + '%' and workplace like + '%' + ltrim(rtrim(substring('" + sum1 + "', 4, 3))) + '%'   and workplace not like '%Sum%' and cat1rate >= 1   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                         "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like (rtrim(substring('" + sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like( rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'    and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like (rtrim(substring('" + sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('" + sum1 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like  (rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like  (rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like  (rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and  workplace like  (rtrim(substring('"+sum1+"',1,3)) +''+ ltrim(rtrim(substring('"+sum1+"',4,3))))+'%'   and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +


                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE1Detail.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE1Detail.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE1Detail.ResultsTableName = "REDetail1";
                    _dbManImageRE1Detail.ExecuteInstruction();

                    DataSet ReportDS1Detail = new DataSet();
                    ReportDS1Detail.Tables.Add(_dbManImageRE1Detail.ResultsDataTable);


                    // workplace2
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum2 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum2Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum2Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum2);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum2.bmp");
                                Sum2Image = Application.StartupPath + "\\" + "Sum2.bmp";
                                Sum2Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE2 = new MWDataManager.clsDataAccess();
                    _dbManImageRE2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE2.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum2Image + "' pp, '" + Sum2Tab + "' SumTab, '" + sum2 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum2 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum2 + "' name1) a order by nn ";
                    _dbManImageRE2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE2.ResultsTableName = "RESum2";
                    _dbManImageRE2.ExecuteInstruction();

                    DataSet ReportDSSum2 = new DataSet();
                    ReportDSSum2.Tables.Add(_dbManImageRE2.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE1Detail2 = new MWDataManager.clsDataAccess();
                    _dbManImageRE1Detail2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE1Detail2.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum3+"',1,3)) +''+ ltrim(rtrim(substring('"+sum3+"',4,3))))+'%' and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+ "',4,3))))+'%' and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('"+sum2+"',1,3)) +''+ ltrim(rtrim(substring('"+sum2+"',4,3))))+'%' and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                        "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE1Detail2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE1Detail2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE1Detail2.ResultsTableName = "REDetail2";
                    _dbManImageRE1Detail2.ExecuteInstruction();

                    DataSet ReportDSDetail2 = new DataSet();
                    ReportDSDetail2.Tables.Add(_dbManImageRE1Detail2.ResultsDataTable);


                    // workplace3
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum3 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum3Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum3Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum3);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum3.bmp");
                                Sum3Image = Application.StartupPath + "\\" + "Sum3.bmp";
                                Sum3Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE3 = new MWDataManager.clsDataAccess();
                    _dbManImageRE3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE3.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum3Image + "' pp, '" + Sum3Tab + "' SumTab, '" + sum3 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum3 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum3 + "' name1) a order by nn ";
                    _dbManImageRE3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE3.ResultsTableName = "RESum3";
                    _dbManImageRE3.ExecuteInstruction();

                    DataSet ReportDSSum3 = new DataSet();
                    ReportDSSum3.Tables.Add(_dbManImageRE3.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE3Detail3 = new MWDataManager.clsDataAccess();
                    _dbManImageRE3Detail3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE3Detail3.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                       " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum3 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum3 + "',4,3))))+'%' and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                       "  and workplace like  (rtrim(substring('" + sum2 + "',1,3)) +''+ ltrim(rtrim(substring('" + sum2 + "',4,3))))+'%' and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE3Detail3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE3Detail3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE3Detail3.ResultsTableName = "REDetail3";
                    _dbManImageRE3Detail3.ExecuteInstruction();

                    DataSet ReportDSDetail3 = new DataSet();
                    ReportDSDetail3.Tables.Add(_dbManImageRE3Detail3.ResultsDataTable);


                    // workplace4
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum4 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum4Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum4Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum4);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum4.bmp");
                                Sum4Image = Application.StartupPath + "\\" + "Sum4.bmp";
                                Sum4Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE4 = new MWDataManager.clsDataAccess();
                    _dbManImageRE4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE4.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum4Image + "' pp, '" + Sum4Tab + "' SumTab, '" + sum4 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum4 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum4 + "' name1) a order by nn ";
                    _dbManImageRE4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE4.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE4.ResultsTableName = "RESum4";
                    _dbManImageRE4.ExecuteInstruction();

                    DataSet ReportDSSum4 = new DataSet();
                    ReportDSSum4.Tables.Add(_dbManImageRE4.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE4Detail4 = new MWDataManager.clsDataAccess();
                    _dbManImageRE4Detail4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE4Detail4.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+ "',4,3))))+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum4+"',1,3)) +''+ ltrim(rtrim(substring('"+sum4+"',4,3))))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE4Detail4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE4Detail4.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE4Detail4.ResultsTableName = "REDetail4";
                    _dbManImageRE4Detail4.ExecuteInstruction();

                    DataSet ReportDSDetail4 = new DataSet();
                    ReportDSDetail4.Tables.Add(_dbManImageRE4Detail4.ResultsDataTable);


                    // workplace5
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum5 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum5Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum5Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum5);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum5.bmp");
                                Sum5Image = Application.StartupPath + "\\" + "Sum5.bmp";
                                Sum5Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE5 = new MWDataManager.clsDataAccess();
                    _dbManImageRE5.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE5.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum5Image + "' pp, '" + Sum5Tab + "' SumTab, '" + sum5 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum5 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum5 + "' name1) a order by nn ";
                    _dbManImageRE5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE5.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE5.ResultsTableName = "RESum5";
                    _dbManImageRE5.ExecuteInstruction();

                    DataSet ReportDSSum5 = new DataSet();
                    ReportDSSum5.Tables.Add(_dbManImageRE5.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE5Detail5 = new MWDataManager.clsDataAccess();
                    _dbManImageRE5Detail5.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE5Detail5.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+ "',4,3))))+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum5+"',1,3)) +''+ ltrim(rtrim(substring('"+sum5+"',4,3))))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE5Detail5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE5Detail5.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE5Detail5.ResultsTableName = "REDetail5";
                    _dbManImageRE5Detail5.ExecuteInstruction();

                    DataSet ReportDSDetail5 = new DataSet();
                    ReportDSDetail5.Tables.Add(_dbManImageRE5Detail5.ResultsDataTable);



                    // workplace6
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum6 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum6Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum6Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum6);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum6.bmp");
                                Sum6Image = Application.StartupPath + "\\" + "Sum6.bmp";
                                Sum6Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE6 = new MWDataManager.clsDataAccess();
                    _dbManImageRE6.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE6.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum6Image + "' pp, '" + Sum6Tab + "' SumTab, '" + sum6 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum6 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum6 + "' name1) a order by nn ";
                    _dbManImageRE6.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE6.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE6.ResultsTableName = "RESum6";
                    _dbManImageRE6.ExecuteInstruction();

                    DataSet ReportDSSum6 = new DataSet();
                    ReportDSSum6.Tables.Add(_dbManImageRE6.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE6Detail6 = new MWDataManager.clsDataAccess();
                    _dbManImageRE6Detail6.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE6Detail6.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+ "',4,3))))+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum6+"',1,3)) +''+ ltrim(rtrim(substring('"+sum6+"',4,3))))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE6Detail6.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE6Detail6.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE6Detail6.ResultsTableName = "REDetail6";
                    _dbManImageRE6Detail6.ExecuteInstruction();

                    DataSet ReportDSDetail6 = new DataSet();
                    ReportDSDetail6.Tables.Add(_dbManImageRE6Detail6.ResultsDataTable);


                    // workplace7
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum7 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum7Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum7Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum7);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum7.bmp");
                                Sum7Image = Application.StartupPath + "\\" + "Sum7.bmp";
                                Sum7Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE7 = new MWDataManager.clsDataAccess();
                    _dbManImageRE7.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE7.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum7Image + "' pp, '" + Sum7Tab + "' SumTab, '" + sum7 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum7 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum7 + "' name1) a order by nn ";
                    _dbManImageRE7.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE7.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE7.ResultsTableName = "RESum7";
                    _dbManImageRE7.ExecuteInstruction();

                    DataSet ReportDSSum7 = new DataSet();
                    ReportDSSum7.Tables.Add(_dbManImageRE7.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE7Detail7 = new MWDataManager.clsDataAccess();
                    _dbManImageRE7Detail7.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE7Detail7.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+ "',4,3))))+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum7+"',1,3)) +''+ ltrim(rtrim(substring('"+sum7+"',4,3))))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE7Detail7.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE7Detail7.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE7Detail7.ResultsTableName = "REDetail7";
                    _dbManImageRE7Detail7.ExecuteInstruction();

                    DataSet ReportDSDetail7 = new DataSet();
                    ReportDSDetail7.Tables.Add(_dbManImageRE7Detail7.ResultsDataTable);


                    // workplace8
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum8 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum8Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum8Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum8);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum8.bmp");
                                Sum8Image = Application.StartupPath + "\\" + "Sum8.bmp";
                                Sum8Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE8 = new MWDataManager.clsDataAccess();
                    _dbManImageRE8.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE8.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum8Image + "' pp, '" + Sum8Tab + "' SumTab, '" + sum8 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum8 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum8 + "' name1) a order by nn ";
                    _dbManImageRE8.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE8.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE8.ResultsTableName = "RESum8";
                    _dbManImageRE8.ExecuteInstruction();

                    DataSet ReportDSSum8 = new DataSet();
                    ReportDSSum8.Tables.Add(_dbManImageRE8.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE8Detail8 = new MWDataManager.clsDataAccess();
                    _dbManImageRE8Detail8.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE8Detail8.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+ "',4,3))))+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like  (rtrim(substring('"+sum8+"',1,3)) +''+ ltrim(rtrim(substring('"+sum8+"',4,3))))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE8Detail8.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE8Detail8.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE8Detail8.ResultsTableName = "REDetail8";
                    _dbManImageRE8Detail8.ExecuteInstruction();

                    DataSet ReportDSDetail8 = new DataSet();
                    ReportDSDetail8.Tables.Add(_dbManImageRE8Detail8.ResultsDataTable);

                    // workplace9
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum9 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum9Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum9Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum9);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum9.bmp");
                                Sum9Image = Application.StartupPath + "\\" + "Sum9.bmp";
                                Sum9Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE9 = new MWDataManager.clsDataAccess();
                    _dbManImageRE9.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE9.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum9Image + "' pp, '" + Sum9Tab + "' SumTab, '" + sum9 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum9 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum9 + "' name1) a order by nn ";
                    _dbManImageRE9.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE9.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE9.ResultsTableName = "RESum9";
                    _dbManImageRE9.ExecuteInstruction();

                    DataSet ReportDSSum9 = new DataSet();
                    ReportDSSum9.Tables.Add(_dbManImageRE9.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE9Detail9 = new MWDataManager.clsDataAccess();
                    _dbManImageRE9Detail9.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE9Detail9.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' +convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        "and workplace like substring('" + sum9 + "',1, 4)+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE9Detail9.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE9Detail9.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE9Detail9.ResultsTableName = "REDetail9";
                    _dbManImageRE9Detail9.ExecuteInstruction();

                    DataSet ReportDSDetail9 = new DataSet();
                    ReportDSDetail9.Tables.Add(_dbManImageRE9Detail9.ResultsDataTable);


                    // workplace10
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum10 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum10Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum10Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum10);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum10.bmp");
                                Sum10Image = Application.StartupPath + "\\" + "Sum10.bmp";
                                Sum10Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE10 = new MWDataManager.clsDataAccess();
                    _dbManImageRE10.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE10.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum10Image + "' pp, '" + Sum10Tab + "' SumTab, '" + sum10 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum10 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum10 + "' name1) a order by nn ";
                    _dbManImageRE10.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE10.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE10.ResultsTableName = "RESum10";
                    _dbManImageRE10.ExecuteInstruction();

                    DataSet ReportDSSum10 = new DataSet();
                    ReportDSSum10.Tables.Add(_dbManImageRE10.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE10Detail10 = new MWDataManager.clsDataAccess();
                    _dbManImageRE10Detail10.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE10Detail10.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+ "',4,3))))+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum10+"',1,3)) +''+ ltrim(rtrim(substring('"+sum10+"',4,3))))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE10Detail10.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE10Detail10.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE10Detail10.ResultsTableName = "REDetail10";
                    _dbManImageRE10Detail10.ExecuteInstruction();

                    DataSet ReportDSDetail10 = new DataSet();
                    ReportDSDetail10.Tables.Add(_dbManImageRE10Detail10.ResultsDataTable);

                    // workplace11
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum11 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum11Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum11Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum11);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum11.bmp");
                                Sum11Image = Application.StartupPath + "\\" + "Sum11.bmp";
                                Sum11Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE11 = new MWDataManager.clsDataAccess();
                    _dbManImageRE11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE11.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum11Image + "' pp, '" + Sum11Tab + "' SumTab, '" + sum11 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum11 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum11 + "' name1) a order by nn ";
                    _dbManImageRE11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE11.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE11.ResultsTableName = "RESum11";
                    _dbManImageRE11.ExecuteInstruction();

                    DataSet ReportDSSum11 = new DataSet();
                    ReportDSSum11.Tables.Add(_dbManImageRE11.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE11Detail11 = new MWDataManager.clsDataAccess();
                    _dbManImageRE11Detail11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE11Detail11.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+ "',4,3))))+'%'  and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum11+"',1,3)) +''+ ltrim(rtrim(substring('"+sum11+"',4,3))))+'%'  and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE11Detail11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE11Detail11.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE11Detail11.ResultsTableName = "REDetail11";
                    _dbManImageRE11Detail11.ExecuteInstruction();

                    DataSet ReportDSDetail11 = new DataSet();
                    ReportDSDetail11.Tables.Add(_dbManImageRE11Detail11.ResultsDataTable);


                    // workplace12
                    // MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                    _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImage.SqlStatement = "select * from [dbo].[tbl_DPT_RockMechInspection] where convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "' " +
                                                " and workplace = '" + sum12 + "' ";
                    _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImage.ExecuteInstruction();


                    string Sum12Image = "";// Application.StartupPath + "\\" + "Neil.bmp";
                    string Sum12Tab = "";
                    if (_dbManImage.ResultsDataTable.Rows.Count > 0)
                    {

                        if (_dbManImage.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                        {

                            if (_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                            {
                                PicBox.Image = Base64ToImage(_dbManImage.ResultsDataTable.Rows[0]["picture"].ToString(), sum12);
                                PicBox.Image.Save(Application.StartupPath + "\\" + "Sum12.bmp");
                                Sum12Image = Application.StartupPath + "\\" + "Sum12.bmp";
                                Sum12Tab = "Y";
                            }

                        }
                    }



                    MWDataManager.clsDataAccess _dbManImageRE12 = new MWDataManager.clsDataAccess();
                    _dbManImageRE12.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE12.SqlStatement = "select top(1) * from (Select 1 nn, picture, '" + Sum12Image + "' pp, '" + Sum12Tab + "' SumTab, '" + sum12 + "' name1 from [dbo].[tbl_DPT_RockMechInspection] where workplace  = '" + sum12 + "' " +
                                                    "and convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'   " +
                                                    " " +
                                                    "union select	2 nn, '', '', '' SumTab, '" + sum12 + "' name1) a order by nn ";
                    _dbManImageRE12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE12.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE12.ResultsTableName = "RESum12";
                    _dbManImageRE12.ExecuteInstruction();

                    DataSet ReportDSSum12 = new DataSet();
                    ReportDSSum12.Tables.Add(_dbManImageRE12.ResultsDataTable);



                    MWDataManager.clsDataAccess _dbManImageRE12Detail12 = new MWDataManager.clsDataAccess();
                    _dbManImageRE12Detail12.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManImageRE12Detail12.SqlStatement = "select * from (select \r\n" +
                                                        "ROW_NUMBER() OVER(ORDER BY a ASC) AS Rowno, * from (  \r\n" +
                                                        " select case when cat1rate = 1 then 'O' else 'R' end as RR, workplace, 1 a, '" + Header1 + "' lbl, convert(varchar(10),cat1rate) cat1rate, cat1amount, cat1note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat1rate >= " + Rate + "  \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat2rate = 1 then 'O' else 'R' end as RR, workplace, 2 a, '" + Header2 + "' lbl,  convert(varchar(10),cat2rate) cat2rate , convert(varchar(10),cat2amount) cat2amount, cat2note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat2rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat3rate = 1 then 'O' else 'R' end as RR, workplace, 3 a, '" + Header3 + "' lbl, convert(varchar(10),cat3rate) cat3rate , cat3amount, cat3note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat3rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat4rate = 1 then 'O' else 'R' end as RR, workplace, 4 a, '" + Header4 + "'lbl, convert(varchar(10),cat4rate) cat4rate , cat4amount, cat4note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat4rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat5rate = 1 then 'O' else 'R' end as RR, workplace, 5 a, '" + Header5 + "' lbl, convert(varchar(10),cat5rate) cat5rate , cat5amount, cat5note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat5rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat6rate = 1 then 'O' else 'R' end as RR, workplace, 6 a, '" + Header6 + "' lbl, convert(varchar(10),cat6rate) cat6rate , cat6amount, cat6note from [dbo].[tbl_DPT_RockMechInspection]  where \r\n " +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat6rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat7rate = 1 then 'O' else 'R' end as RR, workplace, 7 a, '" + Header7 + "' lbl, convert(varchar(10),cat7rate) cat7rate , cat7amount, cat7note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat7rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat8rate = 1 then 'O' else 'R' end as RR, workplace, 8 a, '" + Header8 + "' lbl, convert(varchar(10),cat8rate) cat8rate , cat8amount, cat8note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat8rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat9rate = 1 then 'O' else 'R' end as RR, workplace, 9 a, '" + Header9 + "' lbl, convert(varchar(10),cat9rate) cat9rate , cat9amount, cat9note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat9rate >= " + Rate + "   \r\n" +


                                                         " union all  \r\n" +

                                                        " select  case when cat10rate = 1 then 'O' else 'R' end as RR, workplace, 10 a, '" + Header10 + "' lbl, convert(varchar(10),cat10rate) cat10rate , cat10amount, cat10note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat10rate >= " + Rate + "   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  case when cat11rate = 1 then 'O' else 'R' end as RR, workplace, 11 a, '" + Header11 + "' lbl, convert(varchar(10),cat11rate) cat11rate , cat11amount, cat11note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat11rate >= " + Rate + "   \r\n" +

                                                       " union all  \r\n" +

                                                        " select  'R' RR, workplace, 12 a, '" + Header12 + "' lbl, convert(varchar(10),cat12rate) cat12rate , '' cat12amount, cat12note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+ "',4,3))))+'%' and workplace not like '%Sum%' and convert(decimal(18,2),cat12rate) >= 200   \r\n" +



                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 13 a, '" + Header13 + "' lbl, convert(varchar(10),cat13rate) cat13rate , '' cat13amount, cat13note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat13rate = 'Y'   \r\n" +

                                                        " union all  \r\n" +

                                                        " select  'R' RR, workplace, 14 a, '" + Header14 + "' lbl, convert(varchar(10),cat14rate) cat14rate , '' cat14amount, cat14note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat14rate = 'N'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat15rate = 1 then 'O' else 'R' end as RR, workplace, 15 a, '" + Header15 + "' lbl, convert(varchar(10),cat15rate) cat15rate , '' cat15amount, cat15note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat15rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select 'O'  RR, workplace, 16 a, '" + Header16 + "' lbl, convert(varchar(10),cat16rate) cat16rate , '' cat16amount, cat16note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat16rate ='Y'   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat17rate = 1 then 'O' else 'R' end as RR, workplace, 17 a, '" + Header17 + "' lbl, convert(varchar(10),cat17rate) cat17rate , cat17amount, cat17note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat17rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat18rate = 1 then 'O' else 'R' end as RR, workplace, 18 a, '" + Header18 + "' lbl, convert(varchar(10),cat18rate) cat18rate , cat18amount, cat18note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat18rate >= " + Rate + "   \r\n" +

                                                         " union all  \r\n" +

                                                        " select  case when cat19rate = 1 then 'O' else 'R' end as RR, workplace, 19 a, '" + Header19 + "' lbl, convert(varchar(10),cat19rate) cat19rate , cat19amount, cat19note from [dbo].[tbl_DPT_RockMechInspection]  where  \r\n" +
                                                        "convert(varchar(10),captyear) +' Wk' + convert(varchar(10),actweek) = '" + labelWk.Text + "'  \r\n" +
                                                        " and workplace like  (rtrim(substring('"+sum12+"',1,3)) +''+ ltrim(rtrim(substring('"+sum12+"',4,3))))+'%' and workplace not like '%Sum%' and cat19rate >= " + Rate + "   \r\n" +

                                                          "union all select '', '', 20, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 21, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 22, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 23, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 24, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 25, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 26, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 27, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 28, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 29, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 30, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 31, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 32, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 33, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 34, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 35, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 36, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 37, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 38, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 39, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 40, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 41, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 42, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 43, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 44, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 45, '', null, null, ''  \r\n" +
                                                        "union all select '', '', 46, '', null, null, ''  \r\n" +
                                                        ") a) a   \r\n" +
                                                        "where rowno <= 26 or rr <> ''     \r\n" +

                                                        " ";

                    _dbManImageRE12Detail12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManImageRE12Detail12.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManImageRE12Detail12.ResultsTableName = "REDetail12";
                    _dbManImageRE12Detail12.ExecuteInstruction();

                    DataSet ReportDSDetail12 = new DataSet();
                    ReportDSDetail12.Tables.Add(_dbManImageRE12Detail12.ResultsDataTable);
                    //////////////////////////////////////////////////////////////
                    RockSumReport.RegisterData(ReportDS1Sum);
                    RockSumReport.RegisterData(ReportDS1Detail);

                    RockSumReport.RegisterData(ReportDSSum2);
                    RockSumReport.RegisterData(ReportDSDetail2);

                    RockSumReport.RegisterData(ReportDSSum3);
                    RockSumReport.RegisterData(ReportDSDetail3);

                    RockSumReport.RegisterData(ReportDSSum4);
                    RockSumReport.RegisterData(ReportDSDetail4);

                    RockSumReport.RegisterData(ReportDSSum5);
                    RockSumReport.RegisterData(ReportDSDetail5);

                    RockSumReport.RegisterData(ReportDSSum6);
                    RockSumReport.RegisterData(ReportDSDetail6);

                    RockSumReport.RegisterData(ReportDSSum7);
                    RockSumReport.RegisterData(ReportDSDetail7);

                    RockSumReport.RegisterData(ReportDSSum8);
                    RockSumReport.RegisterData(ReportDSDetail8);

                    RockSumReport.RegisterData(ReportDSSum9);
                    RockSumReport.RegisterData(ReportDSDetail9);

                    RockSumReport.RegisterData(ReportDSSum10);
                    RockSumReport.RegisterData(ReportDSDetail10);

                    RockSumReport.RegisterData(ReportDSSum11);
                    RockSumReport.RegisterData(ReportDSDetail11);

                    RockSumReport.RegisterData(ReportDSSum12);
                    RockSumReport.RegisterData(ReportDSDetail12);
                }






                //Headers 
                MWDataManager.clsDataAccess _dbManHeaderDetials = new MWDataManager.clsDataAccess();
                _dbManHeaderDetials.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHeaderDetials.SqlStatement = "select '" + SysSettings.Banner + "' mine ,  '" + labelWk.Text + "' lblwk ,   '" + labelSum.Text + "' lblMO ";
                _dbManHeaderDetials.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHeaderDetials.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHeaderDetials.ResultsTableName = "HeaderDetials";
                _dbManHeaderDetials.ExecuteInstruction();

                DataSet HeaderDetials = new DataSet();
                HeaderDetials.Tables.Add(_dbManHeaderDetials.ResultsDataTable);

                RockSumReport.RegisterData(HeaderDetials);

                RockSumReport.RegisterData(dsMoreSum);



                //RockSumReport.RegisterData(ReportDS1Sum);
                //RockSumReport.RegisterData(ReportDS1Detail);

                //RockSumReport.RegisterData(ReportDSSum2);
                //RockSumReport.RegisterData(ReportDSDetail2);

                //RockSumReport.RegisterData(ReportDSSum3);
                //RockSumReport.RegisterData(ReportDSDetail3);

                //RockSumReport.RegisterData(ReportDSSum4);
                //RockSumReport.RegisterData(ReportDSDetail4);

                //RockSumReport.RegisterData(ReportDSSum5);
                //RockSumReport.RegisterData(ReportDSDetail5);

                //RockSumReport.RegisterData(ReportDSSum6);
                //RockSumReport.RegisterData(ReportDSDetail6);

                //RockSumReport.RegisterData(ReportDSSum7);
                //RockSumReport.RegisterData(ReportDSDetail7);

                //RockSumReport.RegisterData(ReportDSSum8);
                //RockSumReport.RegisterData(ReportDSDetail8);


                //Fafa
                RockSumReport.Load(TGlobalItems.ReportsFolder + "\\RESummary.frx");
                //RockSumReport.Design();
                Cursor = Cursors.Default;

                previewControl2.Clear();
                RockSumReport.Prepare();
                RockSumReport.Preview = previewControl2;
                RockSumReport.ShowPrepared();


            }




        }

        private void WPLbl_Click(object sender, EventArgs e)
        {

        }

        private void gridControl6_Click(object sender, EventArgs e)
        {

        }
    }
}
