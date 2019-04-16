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
using System.IO;
using FastReport;

namespace Mineware.Systems.Production.SysAdminScreens.SetupCycles
{
    public partial class GeoInspFrm : DevExpress.XtraEditors.XtraForm
    {

        DialogResult result1;
        OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        Report theReport3 = new Report();
        string sourceFile;
        string destinationFile;
        string FileName = "";

        string s = "";
        string wp = "";
        public string ActType = "";

        string ImageDir = "";

        public string _theSystemDBTag;
        public string _UserCurrentInfoConnection;

        public GeoInspFrm()
        {
            InitializeComponent();
        }

        private void GeoInspFrm_Load(object sender, EventArgs e)
        {
            tabPage1.Text = " Data ";
            tabPage2.Text = "Report";

            MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
            _dbManWPST2.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST2.SqlStatement = "Select * from sysset  \r\n";
            _dbManWPST2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST2.ExecuteInstruction();

            if (_dbManWPST2.ResultsDataTable.Rows.Count > 0)
            {
                ImageDir = _dbManWPST2.ResultsDataTable.Rows[0]["REPDIR"].ToString();
            }

            Cat25Txt_EditValueChanged(null,null);
            Cat26Txt_EditValueChanged(null, null);
            Cat27Txt_EditValueChanged(null, null);
            Cat28Txt_EditValueChanged(null, null);
            Cat29Txt_EditValueChanged(null, null);
            Cat30Txt_EditValueChanged(null, null);
            Cat31Txt_EditValueChanged(null, null);
            Cat32Txt_EditValueChanged(null, null);
            Cat33Txt_EditValueChanged(null, null);


            if ("a" == "a")
            {
                label29.Visible = false;
                Cat26Txt.Visible = false;
                //Cat26NotesTxt.Visible = false;
                pbx2.Visible = false;

                label30.Visible = false;
                Cat27Txt.Visible = false;
                Cat27NotesTxt.Visible = false;
                pbx3.Visible = false;

                label31.Visible = false;
                Cat28Txt.Visible = false;
                Cat28NotesTxt.Visible = false;
                pbx4.Visible = false;


                label33.Top = label29.Top;
                Cat29Txt.Top = Cat26Txt.Top;
                Cat29NotesTxt.Top = Cat26Txt.Top;
                pbx5.Top = pbx2.Top;

                label34.Top = label30.Top;
                Cat30Txt.Top = Cat27Txt.Top;
                Cat30NotesTxt.Top = Cat27NotesTxt.Top;
                pbx6.Top = pbx3.Top;

                label35.Top = label31.Top;
                Cat31Txt.Top = Cat28Txt.Top;
                Cat31NotesTxt.Top = Cat28NotesTxt.Top;
                pbx7.Top = pbx4.Top;

                label36.Top = 158;
                Cat32Txt.Top = 158;
                Cat32NotesTxt.Top = 158;
                pbx8.Top = 158;

                label37.Top = 181;
                Cat33Txt.Top = 181;
                Cat33NotesTxt.Top = 181;
                pbx9.Top = 181;


                Cat26NotesTxt.Top = Cat33NotesTxt.Top + 23;
                label4.Top = Cat26NotesTxt.Top;



            }

            if (EditLbl.Text == "1")
            {
                tabControl1.TabPages.Remove(tabPage1);
            }
            else
            {
                if (WkLbl.Text != "7")
                {
                    if (WkLbl.Text != "6")
                    {
                        tabControl1.TabPages.Remove(tabPage1);
                    }
                }

            }

            LoadGrid();
            LoadChart();

            LoadReport();
        }

        void LoadReport()
        {

            string wk = WkLbl2.Text.Substring(3, 2);

            MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
            _dbManWPST2.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManWPST2.SqlStatement = "select top (20) * from (   \r\n"+
                                        "select 'z' bb, Action_Status, [Action_Title], [Start_Date],    \r\n"+
                                        "datediff(day, [Start_Date], getdate()) ss from tbl_Incidents   \r\n"+
                                        " where workplace = '" + WPLbl.Text + "'    \r\n" +
                                        "--and disciplinename = 'RMS' and hazard = 'A'   \r\n"+
                                        "and[Start_Date] = (     \r\n"+
                                        "select max([Start_Date]) dd from tbl_Incidents   \r\n"+
                                        "where workplace = '" + WPLbl.Text + "'    \r\n" +
                                        "--and disciplinename = 'RMS' and hazard = 'A'   \r\n"+
                                        ") group by[Action_Title], Action_Status, [Start_Date]   \r\n"+
                                        " union all   \r\n"+
                                        " select 'a' , '', '', null, ''    \r\n"+
                                        " union all   \r\n"+
                                        " select 'b ', '', '', null, ''    \r\n"+
                                        " union all   \r\n"+
                                        " select 'c  ' , '', '', null, ''    \r\n"+
                                        " union   \r\n"+
                                        " select 'd   ' , '', '', null, ''    \r\n"+
                                        " union   \r\n"+
                                        " select 'e    ' , '', '', null, ''    \r\n"+
                                        " union   \r\n"+
                                        " select 'f     ' , '', '', null, ''    \r\n"+
                                        " union   \r\n"+
                                        " select 'g     ' , '', '', null, ''    \r\n"+
                                        " union   \r\n"+
                                        " select 'h     ' , '', '', null, ''    \r\n"+
                                        " union   \r\n"+
                                        " select 'i     ' , '', '', null, ''    \r\n"+
                                        " union   \r\n"+
                                        " select 'j     ' , '', '', null, ''    \r\n"+
                                        " )a   \r\n"+
                                        " order  by bb \r\n";
            //_dbManWPST2.SqlStatement = "select top (20) * from ( select 'z' bb, ActionStatus, action, datesubmitted, datediff(day,datesubmitted,getdate()) ss  from tbl_Incidents  \r\n" +
            //                            "where workplace = '" + WPLbl.Text + "' and disciplinename = 'RMS' and hazard = 'A'  \r\n" +
            //                            "and datesubmitted = (  \r\n" +

            //                            "select max(datesubmitted) dd from tbl_Incidents   \r\n" +
            //                            "where workplace = '" + WPLbl.Text + "' and disciplinename = 'RMS' and hazard = 'A') group by Action, ActionStatus, DateSubmitted  \r\n" +
            //                            " union all \r\n" +
            //                            " select 'a' , '', '', null, '' \r\n" +
            //                            " union all \r\n" +
            //                            " select 'b ', '', '', null, '' \r\n" +
            //                            " union all \r\n" +
            //                            " select 'c  ' , '', '', null, '' \r\n" +
            //                            " union \r\n" +
            //                            " select 'd   ' , '', '', null, '' \r\n" +
            //                            " union \r\n" +
            //                            " select 'e    ' , '', '', null, '' \r\n" +
            //                            " union \r\n" +
            //                            " select 'f     ' , '', '', null, '' \r\n" +
            //                            " union \r\n" +
            //                            " select 'g     ' , '', '', null, '' \r\n" +
            //                            " union \r\n" +
            //                            " select 'h     ' , '', '', null, '' \r\n" +
            //                            " union \r\n" +
            //                            " select 'i     ' , '', '', null, '' \r\n" +
            //                            " union \r\n" +
            //                            " select 'j     ' , '', '', null, '' \r\n" +
            //                            " )a \r\n" +
            //                            " order  by bb  \r\n";
            _dbManWPST2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST2.ResultsTableName = "Table2";
            _dbManWPST2.ExecuteInstruction();

            
                DataSet dsABS1 = new DataSet();
                dsABS1.Tables.Add(_dbManWPST2.ResultsDataTable);

            theReport3.RegisterData(dsABS1);


            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbMan.SqlStatement = " select '" + SysSettings.Banner + "' banner, '" + RRLbl.Text + "' rr,  * from [dbo].[tbl_DPT_GeoScienceInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + wk + "' and captyear =  (select max(CaptYear) from [dbo].[tbl_DPT_GeoScienceInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + wk + "') ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ResultsTableName = "DevSummary";
            _dbMan.ExecuteInstruction();

            if (_dbMan.ResultsDataTable.Rows.Count > 0)
            {


                Cat25Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat25Rate"].ToString();
                Cat26Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat26Rate"].ToString();
                Cat27Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat27Rate"].ToString();
                Cat28Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat28Rate"].ToString();
                Cat29Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat29Rate"].ToString();
                Cat30Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat30Rate"].ToString();
                Cat31Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat31Rate"].ToString();
                Cat32Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat32Rate"].ToString();
                Cat33Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat33Rate"].ToString();


                Cat25NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat25Note"].ToString();
                Cat26NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat26Note"].ToString();
                Cat27NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat27Note"].ToString();
                Cat28NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat28Note"].ToString();
                Cat29NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat29Note"].ToString();
                Cat30NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat30Note"].ToString();
                Cat31NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat31Note"].ToString();
                Cat32NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat32Note"].ToString();
                Cat33NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat33Note"].ToString();



                txtAttachment.Text = _dbMan.ResultsDataTable.Rows[0]["picture"].ToString();

                PicBox.ImageLocation = _dbMan.ResultsDataTable.Rows[0]["picture"].ToString();

                txtAttachment2.Text = _dbMan.ResultsDataTable.Rows[0]["picture1"].ToString();

                Picbox2.ImageLocation = _dbMan.ResultsDataTable.Rows[0]["picture1"].ToString();

                if (_dbMan.ResultsDataTable.Rows[0]["WPStatus"].ToString() == "Green")
                {
                    GreenCheck.Checked = true;
                }

                if (_dbMan.ResultsDataTable.Rows[0]["WPStatus"].ToString() == "Orange")
                {
                    OrangeCheck.Checked = true;
                }

                if (_dbMan.ResultsDataTable.Rows[0]["WPStatus"].ToString() == "Red")
                {
                    RedCheck.Checked = true;
                }

                MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                _dbManImage.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                _dbManImage.SqlStatement = "Select picture, Picture1 from [dbo].[tbl_DPT_GeoScienceInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + wk + "' ";

                //MineWarePics$\"+BusUnit+"\\RockEng


                _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManImage.ResultsTableName = "Image";
                _dbManImage.ExecuteInstruction();


                MWDataManager.clsDataAccess _dbManChart = new MWDataManager.clsDataAccess();
                _dbManChart.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                _dbManChart.SqlStatement = " select top(10) * from ( \r\n" +

                                  "select description, max(aa) aa, Calendardate, max(SWidth) SWidth, max(CorrCut) CorrCut, max(Hangwall) Hangwall \r\n" +
                                  ", max(Footwall) Footwall, max(allocatedWidth) allocatedWidth, max(Notes) Notes from ( \r\n" +



                                  "select description, a.CalendarDate aa ,substring(convert(varchar(11),a.CalendarDate),0,12) Calendardate,a.SWidth,a.CorrCut,a.Hangwall,a.Footwall, " +
                                           " case when a.allocatedWidth = 0 then null else a.allocatedWidth end as allocatedWidth ,a.Notes from [dbo].[SAMPLING_Imported_Notes] a  \r\n" +
                                  " left outer  join WORKPLACE w on convert(varchar(50),a.gmsiwpis) = w.WorkplaceID  \r\n" +
                                  " and calendardate > getdate()-2000  and swidth > 0 where description = '" + WPLbl.Text + "'    \r\n" +
                                  "  ) a group by description, Calendardate ) a order by aa desc  ";


                _dbManChart.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManChart.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManChart.ResultsTableName = "Chart";
                _dbManChart.ExecuteInstruction();


                DataSet ReportDatasetReport = new DataSet();
                ReportDatasetReport.Tables.Add(_dbMan.ResultsDataTable);

                theReport3.RegisterData(ReportDatasetReport);

                DataSet ReportDatasetReportImage = new DataSet();
                ReportDatasetReportImage.Tables.Add(_dbManImage.ResultsDataTable);

                theReport3.RegisterData(ReportDatasetReportImage);


                DataSet ReportDatasetChart = new DataSet();
                ReportDatasetChart.Tables.Add(_dbManChart.ResultsDataTable);

                theReport3.RegisterData(ReportDatasetChart);

                theReport3.Load(TGlobalItems.ReportsFolder + "\\GeoInsp.frx");

                //theReport3.Design();

                pcReport.Clear();
                theReport3.Prepare();
                theReport3.Preview = pcReport;
                theReport3.ShowPrepared();
            }
            else
            {

                _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbMan.SqlStatement = " select '" + SysSettings.Banner + "' banner, '" + RRLbl.Text + "' rr,  * from [dbo].[tbl_DPT_GeoScienceInspection] where workplace = '" + WPLbl.Text + "' and captweek < '" + wk + "'  order by captyear desc, actweek desc ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "DevSummary";
                _dbMan.ExecuteInstruction();

                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Cat25Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat25Rate"].ToString();
                    Cat26Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat26Rate"].ToString();
                    Cat27Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat27Rate"].ToString();
                    Cat28Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat28Rate"].ToString();
                    Cat29Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat29Rate"].ToString();
                    Cat30Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat30Rate"].ToString();
                    Cat31Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat31Rate"].ToString();
                    Cat32Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat32Rate"].ToString();
                    Cat33Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat33Rate"].ToString();


                    Cat25NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat25Note"].ToString();
                    Cat26NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat26Note"].ToString();
                    Cat27NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat27Note"].ToString();
                    Cat28NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat28Note"].ToString();
                    Cat29NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat29Note"].ToString();
                    Cat30NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat30Note"].ToString();
                    Cat31NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat31Note"].ToString();
                    Cat32NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat32Note"].ToString();
                    Cat33NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat33Note"].ToString();
                }




            }

            if (_dbMan.ResultsDataTable.Rows.Count == 0 && WkLbl.Text != "7")
            {
                if (_dbMan.ResultsDataTable.Rows.Count == 0 && WkLbl.Text != "6")
                    MessageBox.Show("Unable to capture data for this week.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
            }
        }

        private void LoadGrid()
        {

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            //_dbMan.SqlStatement = " select top(10) * from (select description, a.CalendarDate aa ,substring(convert(varchar(11),a.CalendarDate),0,12) Calendardate,a.SWidth,a.CorrCut,a.Hangwall,a.Footwall,  \r\n" +
            //                      " case when a.allocatedWidth = 0 then null else a.allocatedWidth end as allocatedWidth ,a.Notes from [dbo].[SAMPLING_Imported_Notes] a   \r\n" +
            //                      " left outer  join workplace_total w on convert(varchar(50),a.gmsiwpis) = w.gmsiwpid   \r\n" +
            //                      " and calendardate > getdate()-5000 and  swidth > 0 ) a where description = '" + WPLbl.Text + "'  \r\n" +
            //                      " order by aa desc ";

            _dbMan.SqlStatement = " select top(10) * from ( \r\n" +

                                   "select description, max(aa) aa, Calendardate, max(SWidth) SWidth, max(CorrCut) CorrCut, max(Hangwall) Hangwall \r\n" +
                                   ", max(Footwall) Footwall, max(allocatedWidth) allocatedWidth, max(Notes) Notes from ( \r\n" +



                                   "select description, a.CalendarDate aa ,substring(convert(varchar(11),a.CalendarDate),0,12) Calendardate,a.SWidth,a.CorrCut,a.Hangwall,a.Footwall, " +
                                            " case when a.allocatedWidth = 0 then null else a.allocatedWidth end as allocatedWidth ,a.Notes from [dbo].[SAMPLING_Imported_Notes] a  \r\n" +
                                   " left outer  join workplace w on convert(varchar(50),a.gmsiwpis) = w.WorkplaceID  \r\n" +
                                   " and calendardate > getdate()-2000  and swidth > 0 where description = '" + WPLbl.Text + "'    \r\n" +
                                   "  ) a group by description, Calendardate ) a order by aa desc  ";

            

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();


            DataTable dt = _dbMan.ResultsDataTable;

            DataSet ds = new DataSet();

            ds.Tables.Add(dt);

            if (dt.Rows.Count > 0)
            {
                colDate.FieldName = "Calendardate";
                colFootWall.FieldName = "Footwall";
                colHangWall.FieldName = "Hangwall";
                colChannel.FieldName = "CorrCut";
                colStopeWidth.FieldName = "SWidth";
                colAllocSW.FieldName = "allocatedWidth";
                colNote.FieldName = "Notes";

            }


            WalkAboutGrid.DataSource = ds.Tables[0];
        }

        private void LoadChart()
        {

            chart1.Series[0].Points.Clear();

            int Rowcount = 0;

            for (int s = 0; s <= gridView1.RowCount - 1; s++)
            {

                string Day = String.Format("{0:yyyy-MM-dd}", gridView1.GetRowCellValue(Rowcount, gridView1.Columns[0]).ToString());

                chart1.Series[0].Points.AddXY(Day, gridView1.GetRowCellValue(Rowcount, gridView1.Columns[1]).ToString());
                chart1.Series[1].Points.AddXY(Day, gridView1.GetRowCellValue(Rowcount, gridView1.Columns[3]).ToString());
                chart1.Series[2].Points.AddXY(Day, gridView1.GetRowCellValue(Rowcount, gridView1.Columns[2]).ToString());
                chart1.Series[3].Points.AddXY(Day, gridView1.GetRowCellValue(Rowcount, gridView1.Columns[4]).ToString());
                if (gridView1.GetRowCellValue(Rowcount, gridView1.Columns[5]).ToString() != "")
                    chart1.Series[4].Points.AddXY(Day, gridView1.GetRowCellValue(Rowcount, gridView1.Columns[5]).ToString());
                else
                    chart1.Series[4].Points.AddXY(Day, DBNull.Value);
                Rowcount = Rowcount + 1;
            }


        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Convert.ToDecimal(Cat25Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat25Txt.Focus();
                return;
            }
            if (Convert.ToDecimal(Cat26Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat26Txt.Focus();
                return;
            }
            if (Convert.ToDecimal(Cat27Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat27Txt.Focus();
                return;
            }
            if (Convert.ToDecimal(Cat28Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat28Txt.Focus();
                return;
            }
            if (Convert.ToDecimal(Cat29Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat29Txt.Focus();
                return;
            }
            if (Convert.ToDecimal(Cat30Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat30Txt.Focus();
                return;
            }
            if (Convert.ToDecimal(Cat31Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat31Txt.Focus();
                return;
            }
            if (Convert.ToDecimal(Cat32Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat32Txt.Focus();
                return;
            }
            if (Convert.ToDecimal(Cat33Txt.Text) > 2)
            {
                MessageBox.Show("Value must be less than 3");
                Cat33Txt.Focus();
                return;
            }


            string WPStatus = "";
            if (GreenCheck.Checked == true)
            {
                WPStatus = "Green";
            }


            if (OrangeCheck.Checked == true)
            {
                WPStatus = "Orange";
            }


            if (RedCheck.Checked == true)
            {
                WPStatus = "Red";
            }

            string captweek = WkLbl2.Text + "      ";

            captweek = captweek.Substring(3, 2);

            MWDataManager.clsDataAccess _dbManDelete = new MWDataManager.clsDataAccess();
            _dbManDelete.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbManDelete.SqlStatement = " delete from [dbo].[tbl_DPT_GeoScienceInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + captweek + "' and captyear = datepart(Year,GETDATE())";
            _dbManDelete.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDelete.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDelete.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbMan.SqlStatement = " insert into [dbo].[tbl_DPT_GeoScienceInspection] VALUES ( '" + WPLbl.Text + "', datepart(Year,GETDATE()), '" + captweek + "', '" + Convert.ToInt32(WkLbl.Text) + "', \r\n getdate(), '" + TUserInfo.UserID + "', " +

                                  " '" + Cat25Txt.Text + "', '" + Cat25NotesTxt.Text + "',  \r\n " +
                                  " '" + Cat26Txt.Text + "', '" + Cat26NotesTxt.Text + "', \r\n" +
                                  " '" + Cat27Txt.Text + "', '" + Cat27NotesTxt.Text + "', \r\n" +
                                  " '" + Cat28Txt.Text + "', '" + Cat28NotesTxt.Text + "', \r\n" +
                                  " '" + Cat29Txt.Text + "', '" + Cat29NotesTxt.Text + "', \r\n" +
                                  " '" + Cat30Txt.Text + "', '" + Cat30NotesTxt.Text + "', \r\n" +
                                  " '" + Cat31Txt.Text + "', '" + Cat31NotesTxt.Text + "', \r\n" +
                                  " '" + Cat32Txt.Text + "', '" + Cat32NotesTxt.Text + "', \r\n" +
                                  " '" + Cat33Txt.Text + "', '" + Cat33NotesTxt.Text + "', \r\n" +

                                  " '" + txtAttachment.Text + "', '" + WPStatus + "' " +

                                  ",'" + txtAttachment2.Text + "' ) ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();


            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Inspection saved", Color.CornflowerBlue);

            LoadReport();
        }

        private void btnAddImage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
            openFileDialog1.FileName = null;
            result1 = openFileDialog1.ShowDialog();

            GetFile();
        }

        void GetFile()
        {

            Random r = new Random();
            //System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(@"C:\Images");
            // System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(@"\\172.18.16.77\images\GradeSheets");
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(ImageDir + "\\GeoScience");

            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //string[] files = System.IO.Directory.GetFiles(@"C:\Images");
            //string[] files = System.IO.Directory.GetFiles(@"\\172.18.16.77\images\GradeSheets");
            string[] files = System.IO.Directory.GetFiles(ImageDir + "\\GeoScience");

            if (result1 == DialogResult.OK)
            {


                int Name = r.Next(0, 50);
                FileName = "";
                string Ext = "";

                int index = 0;

                sourceFile = openFileDialog1.FileName;


                index = openFileDialog1.SafeFileName.IndexOf(".");
                //FileName = openFileDialog1.SafeFileName.Substring(0, index);

                if (WPLbl.Text != "")
                {
                    FileName = String.Format("{0:yyyyMMddhhmmss}", date1.Value);
                }
                else
                {
                    MessageBox.Show("Please select a workplace");
                    return;
                }

                Ext = openFileDialog1.SafeFileName.Substring(index);

                //destinationFile = @"C:\Images" + "\\" + FileName + Ext;
                destinationFile = ImageDir + "\\GeoScience" + "\\" + FileName + Ext;


                if (files.Length != 0)
                {
                    foreach (FileInfo f1 in list2)
                    {
                        if (f1.Name == openFileDialog1.SafeFileName + Name.ToString())
                        {
                            Name = r.Next(0, 50);

                            //destinationFile = @"C:\Images" + "\\" + FileName + Ext;//+ FileName + Name.ToString() + Ext
                            destinationFile = ImageDir + "\\GeoScience" + "\\" + FileName + Ext;//+ FileName + Name.ToString() + Ext
                            //PicBox.Image = Image.FromFile(openFileDialog1.FileName); 
                        }

                    }

                    try
                    {
                        System.IO.File.Copy(sourceFile, destinationFile, true);
                    }
                    catch
                    {

                    }

                }
                else
                {
                    //System.IO.File.Copy(sourceFile, destinationFile, true);
                    System.IO.File.Copy(sourceFile, ImageDir + "\\GeoScience" + "\\" + FileName + Ext, true);



                    dir2 = new System.IO.DirectoryInfo(ImageDir + "\\GeoScience");

                    list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                    //PicBox.Image = Image.FromFile(openFileDialog1.FileName); 
                }

                txtAttachment.Text = destinationFile;
                PicBox.Image = Image.FromFile(openFileDialog1.FileName);

            }
        }

        private void Cat25Txt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat25Txt.Text.ToString() == "0")
            {
                pbx1.BringToFront();
                pbx1.Image = pbxGreen.Image;
                pbx1.Visible = true;
            }

            if (Cat25Txt.Text.ToString() == "1")
            {
                pbx1.BringToFront();
                pbx1.Image = pbxOrange.Image;
                pbx1.Visible = true;
            }

            if (Cat25Txt.Text.ToString() == "2")
            {
                pbx1.BringToFront();
                pbx1.Image = pbxRed.Image;
                pbx1.Visible = true;
            }
        }

        private void Cat26Txt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat26Txt.Text.ToString() == "0")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxGreen.Image;
                pbx2.Visible = true;
            }

            if (Cat26Txt.Text.ToString() == "1")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxOrange.Image;
                pbx2.Visible = true;
            }

            if (Cat26Txt.Text.ToString() == "2")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxRed.Image;
                pbx2.Visible = true;
            }
        }

        private void Cat27Txt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat27Txt.Text.ToString() == "0")
            {
                pbx3.BringToFront();
                pbx3.Image = pbxGreen.Image;
                pbx3.Visible = true;
            }

            if (Cat27Txt.Text.ToString() == "1")
            {
                pbx3.BringToFront();
                pbx3.Image = pbxOrange.Image;
                pbx3.Visible = true;
            }

            if (Cat27Txt.Text.ToString() == "2")
            {
                pbx3.BringToFront();
                pbx3.Image = pbxRed.Image;
                pbx3.Visible = true;
            }
        }

        private void Cat28Txt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat28Txt.Text.ToString() == "0")
            {
                pbx4.BringToFront();
                pbx4.Image = pbxGreen.Image;
                pbx4.Visible = true;
            }

            if (Cat28Txt.Text.ToString() == "1")
            {
                pbx4.BringToFront();
                pbx4.Image = pbxOrange.Image;
                pbx4.Visible = true;
            }

            if (Cat28Txt.Text.ToString() == "2")
            {
                pbx4.BringToFront();
                pbx4.Image = pbxRed.Image;
                pbx4.Visible = true;
            }
        }

        private void Cat29Txt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat29Txt.Text.ToString() == "0")
            {
                pbx5.BringToFront();
                pbx5.Image = pbxGreen.Image;
                pbx5.Visible = true;
            }

            if (Cat29Txt.Text.ToString() == "1")
            {
                pbx5.BringToFront();
                pbx5.Image = pbxOrange.Image;
                pbx5.Visible = true;
            }

            if (Cat29Txt.Text.ToString() == "2")
            {
                pbx5.BringToFront();
                pbx5.Image = pbxRed.Image;
                pbx5.Visible = true;
            }
        }

        private void Cat30Txt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat30Txt.Text.ToString() == "0")
            {
                pbx6.BringToFront();
                pbx6.Image = pbxGreen.Image;
                pbx6.Visible = true;
            }

            if (Cat30Txt.Text.ToString() == "1")
            {
                pbx6.BringToFront();
                pbx6.Image = pbxOrange.Image;
                pbx6.Visible = true;
            }

            if (Cat30Txt.Text.ToString() == "2")
            {
                pbx6.BringToFront();
                pbx6.Image = pbxRed.Image;
                pbx6.Visible = true;
            }
        }

        private void Cat31Txt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat31Txt.Text.ToString() == "0")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxGreen.Image;
                pbx7.Visible = true;
            }

            if (Cat31Txt.Text.ToString() == "1")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxOrange.Image;
                pbx7.Visible = true;
            }

            if (Cat31Txt.Text.ToString() == "2")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxRed.Image;
                pbx7.Visible = true;
            }
        }

        private void Cat32Txt_EditValueChanged(object sender, EventArgs e)
        {

            if (Cat32Txt.Text.ToString() == "0")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxGreen.Image;
                pbx8.Visible = true;
            }

            if (Cat32Txt.Text.ToString() == "1")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxOrange.Image;
                pbx8.Visible = true;
            }

            if (Cat32Txt.Text.ToString() == "2")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxRed.Image;
                pbx8.Visible = true;
            }
        }

        private void Cat33Txt_EditValueChanged(object sender, EventArgs e)
        {

            if (Cat33Txt.Text.ToString() == "0")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxGreen.Image;
                pbx9.Visible = true;
            }

            if (Cat33Txt.Text.ToString() == "1")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxOrange.Image;
                pbx9.Visible = true;
            }

            if (Cat33Txt.Text.ToString() == "2")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxRed.Image;
                pbx9.Visible = true;
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}