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
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;

namespace Mineware.Systems.Production.SysAdminScreens.SetupCycles
{
    public partial class VentInspection : DevExpress.XtraEditors.XtraForm
    {
        public string theSystemDBTag;
        public string UserCurrentInfoConnection;
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



        public VentInspection()
        {
            InitializeComponent();
        }

        private void VentInspection_Load(object sender, EventArgs e)
        {
            //this.Icon = PAS.Properties.Resources.testbutton3;

            tabPage1.Text = " Data ";
            tabPage2.Text = "Report";

            Cat1RateTxt_EditValueChanged(null, null);
            Cat2RateTxt_EditValueChanged(null, null);
            Cat3RateTxt_EditValueChanged(null, null);
            Cat4RateTxt_EditValueChanged(null, null);
            Cat5RateTxt_EditValueChanged(null, null);
            Cat6RateTxt_EditValueChanged(null, null);
            Cat7RateTxt_EditValueChanged(null, null);
            Cat8RateTxt_EditValueChanged(null, null);
            Cat9RateTxt_EditValueChanged(null, null);
            Cat10RateTxt_EditValueChanged(null, null);


            MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
            _dbManWPST2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfoConnection);
            _dbManWPST2.SqlStatement = "Select * from sysset  \r\n";
            _dbManWPST2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST2.ExecuteInstruction();

            if (_dbManWPST2.ResultsDataTable.Rows.Count > 0)
            {
                ImageDir = _dbManWPST2.ResultsDataTable.Rows[0]["REPDIR"].ToString();
            }

            if (EditLbl.Text != "Y")
            {
                tabControl1.TabPages.Remove(tabPage1);
                RCVentilation.Visible = false;
            }

            LoadReport();
        }

        void LoadReport()
        {
            MWDataManager.clsDataAccess _dbManWPST21 = new MWDataManager.clsDataAccess();
            _dbManWPST21.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfoConnection);
            _dbManWPST21.SqlStatement = "select * from  tbl_LicenceToOperate_Seismic where wpdescription = '" + WPLbl.Text + "' order by thedate desc";

            _dbManWPST21.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST21.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST21.ResultsTableName = "Graph";
            _dbManWPST21.ExecuteInstruction();

            DataSet dsABS111 = new DataSet();
            dsABS111.Tables.Add(_dbManWPST21.ResultsDataTable);

            if (EditLbl.Text == "Y")
            {
                if(_dbManWPST21.ResultsDataTable.Rows.Count > 0)
                Cat24Txt.Text = _dbManWPST21.ResultsDataTable.Rows[0]["risk"].ToString();

            }

            theReport3.RegisterData(dsABS111);

            //DataSet ReportDatasetReportImage = new DataSet();
            //ReportDatasetReportImage.Tables.Add(_dbManImage.ResultsDataTable);

            MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
            _dbManWPST2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfoConnection);
            _dbManWPST2.SqlStatement = "select top (20) * from (   \r\n" +
                                        "select 'z' bb, Action_Status, [Action_Title], [Start_Date],    \r\n" +
                                        "datediff(day, [Start_Date], getdate()) ss from tbl_Incidents   \r\n" +
                                        " where workplace = '" + WPLbl.Text + "'    \r\n" +
                                        "--and disciplinename = 'RMS' and hazard = 'A'   \r\n" +
                                        "and[Start_Date] = (     \r\n" +
                                        "select max([Start_Date]) dd from tbl_Incidents   \r\n" +
                                        "where workplace = '" + WPLbl.Text + "'    \r\n" +
                                        "--and disciplinename = 'RMS' and hazard = 'A'   \r\n" +
                                        ") group by[Action_Title], Action_Status, [Start_Date]   \r\n" +
                                        " union all   \r\n" +
                                        " select 'a' , '', '', null, ''    \r\n" +
                                        " union all   \r\n" +
                                        " select 'b ', '', '', null, ''    \r\n" +
                                        " union all   \r\n" +
                                        " select 'c  ' , '', '', null, ''    \r\n" +
                                        " union   \r\n" +
                                        " select 'd   ' , '', '', null, ''    \r\n" +
                                        " union   \r\n" +
                                        " select 'e    ' , '', '', null, ''    \r\n" +
                                        " union   \r\n" +
                                        " select 'f     ' , '', '', null, ''    \r\n" +
                                        " union   \r\n" +
                                        " select 'g     ' , '', '', null, ''    \r\n" +
                                        " union   \r\n" +
                                        " select 'h     ' , '', '', null, ''    \r\n" +
                                        " union   \r\n" +
                                        " select 'i     ' , '', '', null, ''    \r\n" +
                                        " union   \r\n" +
                                        " select 'j     ' , '', '', null, ''    \r\n" +
                                        " )a   \r\n" +
                                        " order  by bb \r\n";
            //_dbManWPST2.SqlStatement = "select top (20) * from ( select 'z' bb, ActionStatus, action, datesubmitted, datediff(day,datesubmitted,getdate()) ss  from tbl_Incidents  \r\n" +
            //                            "where workplace = '" + WPLbl.Text + "' and disciplinename = 'RMS' and hazard = 'A'  \r\n" +
            //                            "and datesubmitted = (  \r\n" +

            //                            "select max(datesubmitted) dd from tbl_Incidents    \r\n" +
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
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfoConnection);
            _dbMan.SqlStatement = " select '" + SysSettings.Banner + "' banner, '" + RRLbl.Text + "' rr,  * from [dbo].[tbl_DPT_VentInspection] where workplace = '" + WPLbl.Text + "' and captweek = convert(decimal(18,0),'" + WkLbl.Text + "')  and captyear = datepart(year,getdate())  ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ResultsTableName = "DevSummary";
            _dbMan.ExecuteInstruction();

            if (_dbMan.ResultsDataTable.Rows.Count > 0)
            {

                Cat1RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat1Rate"].ToString();
                Cat2RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat2Rate"].ToString();
                Cat3RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat3Rate"].ToString();
                Cat4RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat4Rate"].ToString();
                Cat5RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat5Rate"].ToString();
                Cat6RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat6Rate"].ToString();
                Cat7RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat7Rate"].ToString();
                Cat8RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat8Rate"].ToString();
                Cat9RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat9Rate"].ToString();
                Cat10RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat10Rate"].ToString();
                Cat11RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat11Rate"].ToString();
                if (_dbMan.ResultsDataTable.Rows[0]["Cat12Rate"].ToString() != "")
                    Cat12Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat12Rate"].ToString();

                if (_dbMan.ResultsDataTable.Rows[0]["Cat13Rate"].ToString() == "Y")
                {
                    NewPnlYCheck.Checked = true;
                }
                else
                {
                    NewPnlNCheck.Checked = true;
                }

                if (_dbMan.ResultsDataTable.Rows[0]["Cat14Rate"].ToString() == "Y")
                {
                    ChecklistYCheck.Checked = true;
                }
                else
                {
                    ChecklistNCheck.Checked = true;
                }

                Cat15Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat15Rate"].ToString();

                if (_dbMan.ResultsDataTable.Rows[0]["Cat16Rate"].ToString() == "Y")
                {
                    SecEscYCheck.Checked = true;
                }
                else
                {
                    SecEscNCheck.Checked = true;
                }

                //Cat16Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat16Rate"].ToString();
                Cat17RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat17Rate"].ToString();
                Cat18RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat18Rate"].ToString();
                Cat19RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat19Rate"].ToString();
                Cat20RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat20Rate"].ToString();
                Cat21Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat21Rate"].ToString();
                Cat22Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat22Rate"].ToString();
                Cat23Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat23Rate"].ToString();

                if (EditLbl.Text != "Y")
                    Cat24Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat24Rate"].ToString();


                Cat1AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat1Amount"].ToString();
                Cat2AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat2Amount"].ToString();
                Cat3AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat3Amount"].ToString();
                Cat4AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat4Amount"].ToString();
                Cat5AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat5Amount"].ToString();
                Cat6AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat6Amount"].ToString();
                Cat7AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat7Amount"].ToString();
                Cat8AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat8Amount"].ToString();
                Cat9AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat9Amount"].ToString();
                Cat10AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat10Amount"].ToString();
                Cat11AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat11Amount"].ToString();
                Cat17AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat17Amount"].ToString();
                Cat18AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat18Amount"].ToString();
                Cat19AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat19Amount"].ToString();
                Cat20AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat20Amount"].ToString();


                Cat1NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat1Note"].ToString();
                Cat2NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat2Note"].ToString();
                Cat3NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat3Note"].ToString();
                Cat4NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat4Note"].ToString();
                Cat5NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat5Note"].ToString();
                Cat6NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat6Note"].ToString();
                Cat7NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat7Note"].ToString();
                Cat8NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat8Note"].ToString();
                Cat9NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat9Note"].ToString();
                Cat10NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat10Note"].ToString();
                Cat11NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat11Note"].ToString();
                Cat12NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat12Note"].ToString();
                Cat13NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat13Note"].ToString();
                Cat14NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat14Note"].ToString();
                Cat15NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat15Note"].ToString();
                Cat16NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat16Note"].ToString();
                Cat17NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat17Note"].ToString();
                Cat18NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat18Note"].ToString();
                Cat19NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat19Note"].ToString();
                Cat20NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat20Note"].ToString();

                commentsTxt.Text = _dbMan.ResultsDataTable.Rows[0]["comments"].ToString();

                txtAttachment.Text = _dbMan.ResultsDataTable.Rows[0]["picture"].ToString();

                PicBox.ImageLocation = _dbMan.ResultsDataTable.Rows[0]["picture"].ToString();

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
                _dbManImage.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfoConnection);

                _dbManImage.SqlStatement = "Select picture from [dbo].[tbl_DPT_VentInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + WkLbl.Text + "' and captyear = datepart(year,getdate()) ";

                //MineWarePics$\"+BusUnit+"\\RockEng


                _dbManImage.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManImage.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManImage.ResultsTableName = "Image";
                _dbManImage.ExecuteInstruction();


                DataSet ReportDatasetReport = new DataSet();
                ReportDatasetReport.Tables.Add(_dbMan.ResultsDataTable);

                theReport3.RegisterData(ReportDatasetReport);

                DataSet ReportDatasetReportImage = new DataSet();
                ReportDatasetReportImage.Tables.Add(_dbManImage.ResultsDataTable);

                theReport3.RegisterData(ReportDatasetReportImage);

                theReport3.Load(TGlobalItems.ReportsFolder + "\\Vent.frx");

                //theReport3.Design();

                pcReport.Clear();
                theReport3.Prepare();
                theReport3.Preview = pcReport;
                theReport3.ShowPrepared();
            }
            else
            {

                //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfoConnection);
                _dbMan.SqlStatement = " select '" + SysSettings.Banner + "' banner, '" + RRLbl.Text + "' rr,  * from [dbo].[tbl_DPT_VentInspection] where workplace = '" + WPLbl.Text + "'  order by captyear desc, actweek desc ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "DevSummary";
                _dbMan.ExecuteInstruction();

                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Cat1RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat1Rate"].ToString();
                    Cat2RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat2Rate"].ToString();
                    Cat3RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat3Rate"].ToString();
                    Cat4RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat4Rate"].ToString();
                    Cat5RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat5Rate"].ToString();
                    Cat6RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat6Rate"].ToString();
                    Cat7RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat7Rate"].ToString();
                    Cat8RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat8Rate"].ToString();
                    Cat9RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat9Rate"].ToString();
                    Cat10RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat10Rate"].ToString();
                    Cat11RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat11Rate"].ToString();


                    if (_dbMan.ResultsDataTable.Rows[0]["Cat13Rate"].ToString() == "Y")
                    {
                        NewPnlYCheck.Checked = true;
                    }
                    else
                    {
                        NewPnlNCheck.Checked = true;
                    }

                    if (_dbMan.ResultsDataTable.Rows[0]["Cat14Rate"].ToString() == "Y")
                    {
                        ChecklistYCheck.Checked = true;
                    }
                    else
                    {
                        ChecklistNCheck.Checked = true;
                    }

                    Cat15Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat15Rate"].ToString();

                    if (_dbMan.ResultsDataTable.Rows[0]["Cat16Rate"].ToString() == "Y")
                    {
                        SecEscYCheck.Checked = true;
                    }
                    else
                    {
                        SecEscNCheck.Checked = true;
                    }

                    //Cat16Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat16Rate"].ToString();
                    Cat17RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat17Rate"].ToString();
                    Cat18RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat18Rate"].ToString();
                    Cat19RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat19Rate"].ToString();
                    Cat20RateTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat20Rate"].ToString();
                    Cat21Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat21Rate"].ToString();
                    Cat22Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat22Rate"].ToString();
                    Cat23Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat23Rate"].ToString();

                    if (EditLbl.Text != "Y")
                        Cat24Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat24Rate"].ToString();


                    Cat1AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat1Amount"].ToString();
                    Cat2AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat2Amount"].ToString();
                    Cat3AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat3Amount"].ToString();
                    Cat4AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat4Amount"].ToString();
                    Cat5AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat5Amount"].ToString();
                    Cat6AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat6Amount"].ToString();
                    Cat7AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat7Amount"].ToString();
                    Cat8AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat8Amount"].ToString();
                    Cat9AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat9Amount"].ToString();
                    Cat10AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat10Amount"].ToString();
                    Cat11AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat11Amount"].ToString();
                    Cat17AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat17Amount"].ToString();
                    Cat18AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat18Amount"].ToString();
                    Cat19AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat19Amount"].ToString();
                    Cat20AmountTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat20Amount"].ToString();


                    Cat1NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat1Note"].ToString();
                    Cat2NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat2Note"].ToString();
                    Cat3NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat3Note"].ToString();
                    Cat4NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat4Note"].ToString();
                    Cat5NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat5Note"].ToString();
                    Cat6NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat6Note"].ToString();
                    Cat7NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat7Note"].ToString();
                    Cat8NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat8Note"].ToString();
                    Cat9NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat9Note"].ToString();
                    Cat10NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat10Note"].ToString();
                    Cat11NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat11Note"].ToString();
                    Cat12NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat12Note"].ToString();
                    Cat13NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat13Note"].ToString();
                    Cat14NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat14Note"].ToString();
                    Cat15NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat15Note"].ToString();
                    Cat16NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat16Note"].ToString();
                    Cat17NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat17Note"].ToString();
                    Cat18NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat18Note"].ToString();
                    Cat19NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat19Note"].ToString();
                    Cat20NotesTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat20Note"].ToString();

                    commentsTxt.Text = _dbMan.ResultsDataTable.Rows[0]["comments"].ToString();

                }

            }

            if (_dbMan.ResultsDataTable.Rows.Count == 0 && WkLbl.Text != "7")
            {
                //MessageBox.Show("Unable to capture data for this week.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
            }




        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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

            string newPnl = "Y";
            if (NewPnlNCheck.Checked == true)
                newPnl = "N";

            string checklist = "Y";
            if (ChecklistNCheck.Checked == true)
                checklist = "N";

            string SecEsc = "Y";
            if (SecEscNCheck.Checked == true)
                SecEsc = "N";

            MWDataManager.clsDataAccess _dbManDelete = new MWDataManager.clsDataAccess();
            _dbManDelete.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfoConnection);
            _dbManDelete.SqlStatement = " delete from [dbo].[tbl_DPT_VentInspection] where workplace = '" + WPLbl.Text + "' and actweek = convert(decimal(18,0),'" + WkLbl.Text + "') and captyear = datepart(Year,GETDATE())  ";
            _dbManDelete.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDelete.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDelete.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfoConnection);
            _dbMan.SqlStatement = " insert into [dbo].[tbl_DPT_VentInspection] VALUES ( '" + WPLbl.Text + "', datepart(Year,GETDATE()), datepart(ISOWK,GETDATE()), convert(decimal(18,0),'" + WkLbl.Text + "'), getdate(), '" + TUserInfo.UserID + "', " +
                                  " '" + Cat1RateTxt.Text + "', '" + Cat1AmountTxt.Text + "', '" + Cat1NotesTxt.Text + "', " +
                                  " '" + Cat2RateTxt.Text + "', '" + Cat2AmountTxt.Text + "', '" + Cat2NotesTxt.Text + "', " +
                                  " '" + Cat3RateTxt.Text + "', '" + Cat3AmountTxt.Text + "', '" + Cat3NotesTxt.Text + "', " +
                                  " '" + Cat4RateTxt.Text + "', '" + Cat4AmountTxt.Text + "', '" + Cat4NotesTxt.Text + "', " +
                                  " '" + Cat5RateTxt.Text + "', '" + Cat5AmountTxt.Text + "', '" + Cat5NotesTxt.Text + "', " +
                                  " '" + Cat6RateTxt.Text + "', '" + Cat6AmountTxt.Text + "', '" + Cat6NotesTxt.Text + "', " +
                                  " '" + Cat7RateTxt.Text + "', '" + Cat7AmountTxt.Text + "', '" + Cat7NotesTxt.Text + "', " +
                                  " '" + Cat8RateTxt.Text + "', '" + Cat8AmountTxt.Text + "', '" + Cat8NotesTxt.Text + "', " +
                                  " '" + Cat9RateTxt.Text + "', '" + Cat9AmountTxt.Text + "', '" + Cat9NotesTxt.Text + "', " +
                                  " '" + Cat10RateTxt.Text + "', '" + Cat10AmountTxt.Text + "', '" + Cat10NotesTxt.Text + "', " +
                                  " '', '', '', " +

                                  " '', '', " +
                                  " '', '', " +
                                  " '', '', " +
                                  " '',  '', " +
                                  " '', '', " +
                                  " '', '', '', " +
                                  " '', '', '', " +
                                  " '', '', '', " +
                                  " '', '', '', " +
                                  " '',  " +
                                  " '',  " +
                                  " '',  " +
                                  " '', '" + txtAttachment.Text + "',  '" + commentsTxt.Text + "' " +

                                  " ,  '" + WPStatus + "' ) ";
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
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(ImageDir + "\\Ventilation");

            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //string[] files = System.IO.Directory.GetFiles(@"C:\Images");
            //string[] files = System.IO.Directory.GetFiles(@"\\172.18.16.77\images\GradeSheets");
            string[] files = System.IO.Directory.GetFiles(ImageDir + "\\Ventilation");

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
                destinationFile = ImageDir + "\\Ventilation" + "\\" + FileName + Ext;


                if (files.Length != 0)
                {
                    foreach (FileInfo f1 in list2)
                    {
                        if (f1.Name == openFileDialog1.SafeFileName + Name.ToString())
                        {
                            Name = r.Next(0, 50);

                            //destinationFile = @"C:\Images" + "\\" + FileName + Ext;//+ FileName + Name.ToString() + Ext
                            destinationFile = ImageDir + "\\Ventilation" + "\\" + FileName + Ext;//+ FileName + Name.ToString() + Ext
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
                    System.IO.File.Copy(sourceFile, ImageDir + "\\Ventilation" + "\\" + FileName + Ext, true);



                    dir2 = new System.IO.DirectoryInfo(ImageDir + "\\Ventilation");

                    list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                    //PicBox.Image = Image.FromFile(openFileDialog1.FileName); 
                }

                txtAttachment.Text = destinationFile;
                PicBox.Image = Image.FromFile(openFileDialog1.FileName);

            }
        }

        private void Cat1RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat1RateTxt.Value.ToString() == "0")
            {
                pbx1.BringToFront();
                pbx1.Image = pbxGreen.Image;
                pbx1.Visible = true;
            }

            if (Cat1RateTxt.Value.ToString() == "1")
            {
                pbx1.BringToFront();
                pbx1.Image = pbxOrange.Image;
                pbx1.Visible = true;
            }

            if (Cat1RateTxt.Value.ToString() == "2")
            {
                pbx1.BringToFront();
                pbx1.Image = pbxRed.Image;
                pbx1.Visible = true;
            }
        }

        private void Cat2RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat2RateTxt.Text.ToString() == "0")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxGreen.Image;
                pbx2.Visible = true;
            }

            if (Cat2RateTxt.Text.ToString() == "1")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxOrange.Image;
                pbx2.Visible = true;
            }

            if (Cat2RateTxt.Text.ToString() == "2")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxRed.Image;
                pbx2.Visible = true;
            }
        }

        private void Cat3RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat3RateTxt.Value.ToString() == "0")
            {
                pbx3.BringToFront();
                pbx3.Image = pbxGreen.Image;
                pbx3.Visible = true;
            }

            if (Cat3RateTxt.Value.ToString() == "1")
            {
                pbx3.BringToFront();
                pbx3.Image = pbxOrange.Image;
                pbx3.Visible = true;
            }

            if (Cat3RateTxt.Value.ToString() == "2")
            {
                pbx3.BringToFront();
                pbx3.Image = pbxRed.Image;
                pbx3.Visible = true;
            }
        }

        private void Cat4RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat4RateTxt.Value.ToString() == "0")
            {
                pbx4.BringToFront();
                pbx4.Image = pbxGreen.Image;
                pbx4.Visible = true;
            }

            if (Cat4RateTxt.Value.ToString() == "1")
            {
                pbx4.BringToFront();
                pbx4.Image = pbxOrange.Image;
                pbx4.Visible = true;
            }

            if (Cat4RateTxt.Value.ToString() == "2")
            {
                pbx4.BringToFront();
                pbx4.Image = pbxRed.Image;
                pbx4.Visible = true;
            }
        }

        private void Cat5RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat5RateTxt.Value.ToString() == "0")
            {
                pbx5.BringToFront();
                pbx5.Image = pbxGreen.Image;
                pbx5.Visible = true;
            }

            if (Cat5RateTxt.Value.ToString() == "1")
            {
                pbx5.BringToFront();
                pbx5.Image = pbxOrange.Image;
                pbx5.Visible = true;
            }

            if (Cat5RateTxt.Value.ToString() == "2")
            {
                pbx5.BringToFront();
                pbx5.Image = pbxRed.Image;
                pbx5.Visible = true;
            }
        }

        private void Cat6RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat6RateTxt.Value.ToString() == "0")
            {
                pbx6.BringToFront();
                pbx6.Image = pbxGreen.Image;
                pbx6.Visible = true;
            }

            if (Cat6RateTxt.Value.ToString() == "1")
            {
                pbx6.BringToFront();
                pbx6.Image = pbxOrange.Image;
                pbx6.Visible = true;
            }

            if (Cat6RateTxt.Value.ToString() == "2")
            {
                pbx6.BringToFront();
                pbx6.Image = pbxRed.Image;
                pbx6.Visible = true;
            }
        }

        private void Cat8RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat8RateTxt.Value.ToString() == "0")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxGreen.Image;
                pbx7.Visible = true;
            }

            if (Cat8RateTxt.Value.ToString() == "1")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxOrange.Image;
                pbx7.Visible = true;
            }

            if (Cat8RateTxt.Value.ToString() == "2")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxRed.Image;
                pbx7.Visible = true;
            }
        }

        private void Cat9RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat9RateTxt.Value.ToString() == "0")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxGreen.Image;
                pbx8.Visible = true;
            }

            if (Cat9RateTxt.Value.ToString() == "1")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxOrange.Image;
                pbx8.Visible = true;
            }

            if (Cat9RateTxt.Value.ToString() == "2")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxRed.Image;
                pbx8.Visible = true;
            }
        }

        private void Cat10RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat10RateTxt.Value.ToString() == "0")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxGreen.Image;
                pbx9.Visible = true;
            }

            if (Cat10RateTxt.Value.ToString() == "1")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxOrange.Image;
                pbx9.Visible = true;
            }

            if (Cat10RateTxt.Value.ToString() == "2")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxRed.Image;
                pbx9.Visible = true;
            }
        }

        private void Cat7RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            if (Cat7RateTxt.Value.ToString() == "0")
            {
                pbx10.BringToFront();
                pbx10.Image = pbxGreen.Image;
                pbx10.Visible = true;
            }

            if (Cat7RateTxt.Value.ToString() == "1")
            {
                pbx10.BringToFront();
                pbx10.Image = pbxOrange.Image;
                pbx10.Visible = true;
            }

            if (Cat7RateTxt.Value.ToString() == "2")
            {
                pbx10.BringToFront();
                pbx10.Image = pbxRed.Image;
                pbx10.Visible = true;
            }
        }
    }
}