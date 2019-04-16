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
using System.Drawing.Imaging;

namespace Mineware.Systems.Production.SysAdminScreens.SetupCycles
{
    public partial class RockEngFrm : DevExpress.XtraEditors.XtraForm
    {
        clsSetupCycles _clsSetupCycles = new clsSetupCycles();

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


        int catrate1 = 0;
        int catrate2 = 0;
        int catrate3 = 0;
        int catrate4 = 0;
        int catrate5 = 0;
        int catrate6 = 0;
        int catrate7 = 0;
        int catrate8 = 0;
        int catrate9 = 0;
        int catrate10 = 0;
        int catrate11 = 0;
        int catrate12 = 0;
        int catrate13 = 0;
        int catrate14 = 0;
        int catrate15 = 0;
        int catrate16 = 0;
        int catrate17 = 0;
        int catrate18 = 0;
        int catrate19 = 0;
        int catrate20 = 0;

        public RockEngFrm()
        {
            InitializeComponent();
        }

        private void RockEngFrm_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "    Data    ";
            tabPage2.Text = "   Report   ";

            tabControl1.Visible = false;

            SetEverythingBack();

            tabControl1.Visible = true;

            _clsSetupCycles.theData.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);


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


            tabControl1.Visible = false;
            //this.Icon = PAS.Properties.Resources.testbutton3;

            //Have to Still DO This
            //PicBox.Image = PAS.Properties.Resources.Blank;

            if (EditLbl.Text != "Y")
            {
                tabControl1.TabPages.Remove(tabPage1);
                RCRockEngineering.Visible = true;
            }

            if (ActType == "Dev")
            {
                DistToHolTxt2.Visible = false;
                label7.Visible = false;
               // label9.Visible = false;
              //  label10.Visible = false;
                label11.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label19.Visible = false;
                label20.Visible = false;
                label21.Visible = false;
                label22.Visible = false;
                label23.Visible = false;
                label25.Visible = false;
                label30.Visible = false;
                label31.Visible = false;
                //label32.Visible = false;
                label34.Visible = false;
                label35.Visible = false;
                label36.Visible = false;
                label23.Visible = false;
                label30.Left += 50;
                label31.Left += 50;
                label36.Left += 50;
               // commentsTxt.Visible = false;
                //groupBox2.Visible = false;


               // Cat9NotesTxt.Visible = false;
               // Cat10NotesTxt.Visible = false;
                Cat11NotesTxt.Visible = false;
                Cat12NotesTxt.Visible = false;
                Cat15NotesTxt.Visible = false;
                Cat16NotesTxt.Visible = false;
                Cat17NotesTxt.Visible = false;
                Cat18NotesTxt.Visible = false;
                Cat19NotesTxt.Visible = false;
                Cat20NotesTxt.Visible = false;

                Cat15Txt.Visible = false;


               // Cat9AmountTxt.Visible = false;
              //  Cat10AmountTxt.Visible = false;
                Cat11AmountTxt.Visible = false;
                Cat17AmountTxt.Visible = false;
                Cat18AmountTxt.Visible = false;
                Cat19AmountTxt.Visible = false;
                Cat20AmountTxt.Visible = false;

              //  Cat9RateTxt.Visible = false;
              //  Cat10RateTxt.Visible = false;
                Cat11RateTxt.Visible = false;
                Cat17RateTxt.Visible = false;
                Cat18RateTxt.Visible = false;
                Cat19RateTxt.Visible = false;
                Cat20RateTxt.Visible = false;
                Cat12Txt.Visible = false;

              //  pbx9.Visible = false;
              //  pbx10.Visible = false;
                pbx11.Visible = false;
                pbx12.Visible = false;
                pbx13.Visible = false;
                pbx14.Visible = false;
                pbx19.Visible = false;

                NewPnlYCheck.Visible = false;
                NewPnlNCheck.Visible = false;
                Cat13NotesTxt.Visible = false;

                ChecklistYCheck.Visible = false;
                ChecklistNCheck.Visible = false;
                Cat14NotesTxt.Visible = false;

                SecEscYCheck.Visible = false;
                SecEscNCheck.Visible = false;
                Cat16NotesTxt.Visible = false;




                //AdjustBookGB.Text = "FOGM-1:  Prevention";

                label13.Text = "Distance to holing.";
                label13.AutoSize = false;
                label13.SendToBack();
                label13.Width = 400;
                label13.Height = 25;
                label20.Visible = false;
                Cat1RateTxt.Left += 50;
                Cat1AmountTxt.Left += 50;
                Cat1NotesTxt.Left += 50;
                Cat1NotesTxt.Height = 30;
                Cat1NotesTxt.Width -= 50;
                Cat1NotesTxt.Multiline = true;
                label17.Left += 50;

                pbx1.Left += 50;
                pbx1.Visible = true;



                label1.Text = "Are the last survey off-sets and \r\nshiftboss measurements on line? (direction)";
                label1.Width = 400;
                label1.Height = 40;
                label1.SendToBack();
                label1.AutoSize = false;
                label1.Top += 10;
                Cat2RateTxt.Left += 50;
                Cat2RateTxt.Top += 15;
                Cat2AmountTxt.Left += 50;
                Cat2AmountTxt.Top += 15;
                Cat2NotesTxt.Left += 50;
                Cat2NotesTxt.Top += 15;
                Cat2NotesTxt.Height = 30;
                Cat2NotesTxt.Multiline = true;
                Cat2NotesTxt.Width -= 50;

                pbx2.Left += 50;
                pbx2.Top += 15;


                label6.Text = "Are all identified geological structures \r\ndemarcated on the plan?";
                label6.Top += 35;
                label6.Width = 400;
                label6.Height = 40;
                label6.SendToBack();
                label6.AutoSize = false;
                Cat3RateTxt.Left += 50;
                Cat3RateTxt.Top += 40;
                Cat3AmountTxt.Left += 50;
                Cat3AmountTxt.Top += 40;
                Cat3NotesTxt.Left += 50;
                Cat3NotesTxt.Top += 40;
                Cat3NotesTxt.Height = 30;
                Cat3NotesTxt.Multiline = true;
                Cat3NotesTxt.Width -= 50;

                pbx3.Left += 50;
                pbx3.Top += 40;


                label5.Text = "Is the tunnel's dimensions greater than 4m x 4m? \r\n(additional support recommendations)?";
                label5.Top += 60;
                label5.Width = 400;
                label5.Height = 25;
                label5.SendToBack();
                label5.AutoSize = false;
                Cat4RateTxt.Left += 52;
                Cat4RateTxt.Top += 65;
                Cat4AmountTxt.Left += 52;
                Cat4AmountTxt.Top += 65;
                Cat4NotesTxt.Left += 52;
                Cat4NotesTxt.Top += 65;
                Cat4NotesTxt.Height = 30;
                Cat4NotesTxt.Multiline = true;
                Cat4NotesTxt.Width -= 50;

                pbx4.Left += 52;
                pbx4.Top += 65;


                //////New


                label9.Text = "Is the tunnel being developed sub \r\nparallel to dip or strike? (Additional support)";
                label9.Top += 85-105;
                label9.Width = 400;
                label9.Height = 25;
                label9.SendToBack();
                label9.AutoSize = false;
                Cat9RateTxt.Left += 52;
                Cat9RateTxt.Top += 90 - 105;
                Cat9AmountTxt.Left += 52;
                Cat9AmountTxt.Top += 90 - 105;
                Cat9NotesTxt.Left += 52;
                Cat9NotesTxt.Top += 95 -105;
                Cat9NotesTxt.Height = 30;
                Cat9NotesTxt.Multiline = true;
                Cat9NotesTxt.Width -= 50;

                pbx9.Left += 52;
                pbx9.Top += 90 - 105;


               ///////////////////


                label4.Text = "Has geology feature been interseted \r\nor is intersection imminent.";
                label4.Top += 115;
                label4.Width = 400;
                label4.Height = 40;
                label4.SendToBack();
                label4.AutoSize = false;
                Cat5RateTxt.Left += 50;
                Cat5RateTxt.Top += 120;
                Cat5AmountTxt.Left += 50;
                Cat5AmountTxt.Top += 120;
                Cat5NotesTxt.Left += 50;
                Cat5NotesTxt.Top += 120;
                Cat5NotesTxt.Height = 30;
                Cat5NotesTxt.Multiline = true;
                Cat5NotesTxt.Width -= 50;

                pbx5.Left += 50;
                pbx5.Top += 120;

                label3.Text = "Has a flat end intersected a dyke known to be \r\nseismically active or hanging witdth of 5m or more.";
                label3.Top += 145;
                label3.AutoSize = false;
                label3.Width = 400;
                label3.Height = 40;
                label3.SendToBack();
                Cat6RateTxt.Left += 50;
                Cat6RateTxt.Top += 150;
                Cat6AmountTxt.Left += 50;
                Cat6AmountTxt.Top += 150;
                Cat6NotesTxt.Left += 50;
                Cat6NotesTxt.Top += 150;
                Cat6NotesTxt.Height = 30;
                Cat6NotesTxt.Multiline = true;
                Cat6NotesTxt.Width -= 50;

                pbx6.Left += 50;
                pbx6.Top += 150;


                //label34.Text = "FOGM-3:  Promotion";
                //label34.Top -= 85;
                //label34.Height = 10;
                //label34.Visible = true;

                label2.Text = "Any increase in seismic activity reported \r\non daily seismic reports?";
                label2.Top += 175;
                label2.Width = 400;
                label2.Height = 40;
                label2.SendToBack();
                label2.AutoSize = false;
                Cat7RateTxt.Left += 50;
                Cat7RateTxt.Top += 180;
                Cat7AmountTxt.Left += 50;
                Cat7AmountTxt.Top += 180;
                Cat7NotesTxt.Left += 50;
                Cat7NotesTxt.Top += 180;
                Cat7NotesTxt.Height = 30;
                Cat7NotesTxt.Multiline = true;
                Cat7NotesTxt.Width -= 50;

                pbx7.Left += 50;
                pbx7.Top += 180;



                ////New



                label10.Text = "Have any reports from crew \r\nregarding ground conditions?";
                label10.Top += 205-50;
                label10.Width = 400;
                label10.Height = 40;
                label10.SendToBack();
                label10.AutoSize = false;
                Cat10RateTxt.Left += 50;
                Cat10RateTxt.Top += 210 - 50;
                Cat10AmountTxt.Left += 50;
                Cat10AmountTxt.Top += 210 - 50;
                Cat10NotesTxt.Left += 50;
                Cat10NotesTxt.Top += 210 -50;
                Cat10NotesTxt.Height = 30;
                Cat10NotesTxt.Multiline = true;
                Cat10NotesTxt.Width -= 50;

                pbx10.Left += 50;
                pbx10.Top += 210 - 50;

                //label35.Text = "FOGM-4:  Provide Warning";
                //label35.Top -= 85;
                //label35.Visible = true;

                label8.Text = "Were any ground control issues identified by the \r\nStrata Control Officer or Rock Engineer?";
                label8.Top += 235;
                label8.Width = 400;
                label8.Height = 30;
                label8.SendToBack();
                label8.AutoSize = false;
                Cat8RateTxt.Left += 50;
                Cat8RateTxt.Top += 240;
                Cat8AmountTxt.Left += 50;
                Cat8AmountTxt.Top += 240;
                Cat8NotesTxt.Left += 50;
                Cat8NotesTxt.Top += 240;
                Cat8NotesTxt.Height = 30;
                Cat8NotesTxt.Multiline = true;
                Cat8NotesTxt.Width -= 50;

                pbx8.Left += 50;
                pbx8.Top += 240;

            }
            else
            {
                   // AdjustBookGB.Text = "FOGM 1 - Prevention (Stoping)";

                    label13.Visible = false;
                    Cat1RateTxt.Visible = false;
                    Cat1AmountTxt.Visible = false;
                    Cat1NotesTxt.Visible = false;
                    label17.Visible = false;

                    label1.Text = "Lead Lag";
                    label6.Text = "Siding Depth";
                    label5.Text = "Siding lag";
                    label4.Text = "Gully Lead (max 2m including siding)";
                    label3.Text = "Gully Direction";
                    label2.Text = "Face Length (max 35mm including siding)";
                    label8.Text = "Face Shape (Panel straight)";
                    label9.Text = "Approach Distance to structure";

                    label10.Text = "Escape Gully to Face";
                    label11.Text = "Pillars (Cut as Planned)";
                    //label14.Text = "Panel Rating";
                    //label13.Text = "Face Shape (Panel straight)";

                    //label7.Visible = true;
                    label19.Text = "Adherence to ledging blueprint plan";

                    // AdjustBookGB.Height = 450;

                    //label34.Visible = true;

                    label20.Text = "Gully Lag (min 2m)";
                    label21.Text = "Are limit lines adhered to?";

                    //label35.Visible = true;

                    label22.Text = "Change in mining direction ";

                    label23.Text = "Geological Anomolies?  ";


                    label26.Visible = false;
                    label27.Visible = false;
                    label12.Visible = false;
                    label28.Visible = false;

                    Cat22Txt.Visible = false;
                    Cat23Txt.Visible = false;
                    Cat24Txt.Visible = false;



                
            }


            LoadReport();

            tabControl1.Visible = true;
        }

        private void SetEverythingBack()
        {
            pbx1.Image = pbxGreen.Image;
            pbx2.Image = pbxGreen.Image;
            pbx3.Image = pbxGreen.Image;
            pbx4.Image = pbxGreen.Image;
            pbx5.Image = pbxGreen.Image;
            pbx6.Image = pbxGreen.Image;
            pbx7.Image = pbxGreen.Image;
            pbx8.Image = pbxGreen.Image;
            pbx9.Image = pbxGreen.Image;
            pbx10.Image = pbxGreen.Image;
            pbx11.Image = pbxGreen.Image;
            pbx12.Image = pbxGreen.Image;
            pbx13.Image = pbxGreen.Image;
            pbx14.Image = pbxGreen.Image;
            pbx15.Image = pbxGreen.Image;
            pbx16.Image = pbxGreen.Image;
            pbx17.Image = pbxGreen.Image;
            pbx18.Image = pbxGreen.Image;
            pbx19.Image = pbxGreen.Image;

            //pbx1.Visible = true;
            pbx2.Visible = true;
            pbx3.Visible = true;
            pbx4.Visible = true;
            pbx5.Visible = true;
            pbx6.Visible = true;
            pbx7.Visible = true;
            pbx8.Visible = true;
            pbx9.Visible = true;
            pbx10.Visible = true;
            pbx11.Visible = true;
            pbx12.Visible = true;
            //pbx13.Visible = true;
            //pbx14.Visible = true;
            //pbx15.Visible = true;
            //pbx16.Visible = true;
            //pbx17.Visible = true;
            //pbx18.Visible = true;
            //pbx19.Visible = true;
        }

        void LoadReport()
        {
            Cursor = Cursors.WaitCursor;

            string SqlStatementGraph = "";

            SqlStatementGraph = "select * from  tbl_LicenceToOperate_Seismic where wpdescription = '" + WPLbl.Text + "' and TheDate >= getdate()-10 order by thedate desc";

            DataSet dsABS111 = new DataSet();
            dsABS111.Tables.Add(_clsSetupCycles.LoadRockMechGraph(SqlStatementGraph, "Graph"));

            if (EditLbl.Text == "Y")
            {
                if (dsABS111.Tables[0].Rows.Count > 0)
                    Cat24Txt.Text = dsABS111.Tables[0].Rows[0]["risk"].ToString();
            }


            theReport3.RegisterData(dsABS111);

            //DataSet ReportDatasetReportImage = new DataSet();
            //ReportDatasetReportImage.Tables.Add(_dbManImage.ResultsDataTable);



            MWDataManager.clsDataAccess _dbManWPST2 = new MWDataManager.clsDataAccess();
            _dbManWPST2.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
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
            _dbManWPST2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManWPST2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManWPST2.ResultsTableName = "Table2";
            _dbManWPST2.ExecuteInstruction();



           
                DataSet dsABS1 = new DataSet();
                dsABS1.Tables.Add(_dbManWPST2.ResultsDataTable);

                theReport3.RegisterData(dsABS1);
            


            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
            _dbMan.SqlStatement = "  select a.*, b.CrewName from ( select a.*, b.SWidth from ( " +
                            " select '" + SysSettings.Banner + "' banner, '" + RRLbl.Text + "' rr,'" + SecLbl.Text + "' Section,   * from [dbo].[tbl_DPT_RockMechInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + Convert.ToInt32(WkLbl.Text) + "' and captyear = (select max(CaptYear) from [dbo].[tbl_DPT_RockMechInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + Convert.ToInt32(WkLbl.Text) + "') "+
                            "  )A left outer join ( select max(s.Calendardate)Calendardate, w.WorkplaceID,w.Description, max(SWidth) SWidth, cr.CrewName  " +
                            " from SAMPLING s, workplace w, crew cr, planmonth pm where s.Workplaceid = w.WorkplaceID  and s.Workplaceid = pm.WorkplaceID  and cr.GangNo = pm.OrgunitDay and pm.Plancode = 'MP'  " +
                            " group by s.workplaceid,w.Description,w.workplaceid , cr.CrewName  ) b on a.Workplace = b.Description" +


                            "  )a left outer join (select  w.WorkplaceID,w.Description, cr.CrewName    \r\n" +
                            "  from workplace w, crew cr,   planmonth pm  \r\n" +
                            " where pm.Workplaceid = w.WorkplaceID  \r\n" +
                            "  and pm.Workplaceid = pm.WorkplaceID  and cr.GangNo = pm.OrgunitDay  \r\n" +
                            " and pm.Plancode = 'MP' and pm.Prodmonth = (select currentproductionmonth from sysset)   \r\n" +
                            "   group by w.Description,w.workplaceid , cr.CrewName)b  on a.Workplace = b.Description \r\n" +



                             "  ";
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


                //DistToHolTxt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat13Rate"].ToString();
                DistToHolTxt2.Text = _dbMan.ResultsDataTable.Rows[0]["Cat13Rate"].ToString();
                

                //if (_dbMan.ResultsDataTable.Rows[0]["Cat13Rate"].ToString() == "N")
                //{
                //    NewPnlYCheck.Checked = false;
                //    NewPnlNCheck.Checked = true;
                //}
                //else
                //{
                //    NewPnlYCheck.Checked = true;
                //    NewPnlNCheck.Checked = false;
                //}

                if (_dbMan.ResultsDataTable.Rows[0]["Cat14Rate"].ToString() == "N")
                {
                    ChecklistYCheck.Checked = false;
                    ChecklistNCheck.Checked = true;
                }
                else
                {
                    ChecklistYCheck.Checked = true;
                    ChecklistNCheck.Checked = false;
                }

                Cat15Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat15Rate"].ToString();

                if (_dbMan.ResultsDataTable.Rows[0]["Cat16Rate"].ToString() == "N")
                {
                    SecEscYCheck.Checked = false;
                    SecEscNCheck.Checked = true;
                }
                else
                {
                    SecEscYCheck.Checked = true;
                    SecEscNCheck.Checked = false;
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

                TxtCrew.Text = _dbMan.ResultsDataTable.Rows[0]["CrewName"].ToString();

                //byte [] aa = _dbMan.ResultsDataTable.Rows[0]["picture"];
                // Console.WriteLine(aa.Length);


                //PicBox.ImageLocation = _dbMan.ResultsDataTable.Rows[0]["picture"].ToString();

                if (_dbMan.ResultsDataTable.Rows[0]["WPASuser"].ToString() == "Tablet")
                {

                    //string converted = _dbMan.ResultsDataTable.Rows[0]["picture"].ToString().Replace('-', '+');
                    //converted = converted.Replace('_', '/');

                    //PicBox.Image = Base64ToImage(converted);

                    // string s = converted;
                    //  byte[] data = System.Text.Encoding.ASCII.GetBytes(s);


                    // byte[] data = (byte[])_dbMan.ResultsDataTable.Rows[0]["picture"];
                    // MemoryStream ms = new MemoryStream(data);
                    // PicBox.Image = Image.FromStream(ms);

                    if (_dbMan.ResultsDataTable.Rows[0]["picture"].ToString() != "")
                    {
                        //PicBox.Image = Base64ToImage(_dbMan.ResultsDataTable.Rows[0]["picture"].ToString());
                    }

                    PicBox.Image.Save(Application.StartupPath + "\\" + "Neil.bmp");


                }
                else
                {

                    PicBox.ImageLocation = _dbMan.ResultsDataTable.Rows[0]["picture"].ToString();
                }

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

                string aa = Application.StartupPath + "\\" + "Neil.bmp";

                MWDataManager.clsDataAccess _dbManImage = new MWDataManager.clsDataAccess();
                _dbManImage.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);

                _dbManImage.SqlStatement = "Select '" + aa + "' pp, Picture from [dbo].[tbl_DPT_RockMechInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + Convert.ToInt32(WkLbl.Text) + "' and captyear = (select max(CaptYear) from [dbo].[tbl_DPT_RockMechInspection] where workplace = '" + WPLbl.Text + "' and captweek = '" + Convert.ToInt32(WkLbl.Text) + "') ";

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



                if (ActType == "Dev")
                {
                    theReport3.Load(TGlobalItems.ReportsFolder + "\\RockEngDEv.frx");
                }
                else
                {
                    //if (SysSettings.Banner == "Great Noligwa" || SysSettings.Banner == "Moab Khotsong" || SysSettings.Banner == "Kopanang")
                        theReport3.Load(TGlobalItems.ReportsFolder + "\\RockEngVR.frx");
                    //else
                    //    theReport3.Load("RockEng.frx");
                }



                //theReport3.Design();

                pcReport.Clear();
                theReport3.Prepare();

                theReport3.Preview = pcReport;
                theReport3.ShowPrepared();
            }
            else
            {

                // MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(_theSystemDBTag, _UserCurrentInfoConnection);
                _dbMan.SqlStatement = " select '" + SysSettings.Banner + "' banner, '" + RRLbl.Text + "' rr,  r.*, cr.crewname from [dbo].[tbl_DPT_RockMechInspection] r,workplace w, crew cr,   planmonth pm  where workplace = '" + WPLbl.Text + "'  and w.Workplaceid = pm.Workplaceid    and pm.OrgUnitDay = cr.GangNo and pm.Prodmonth = (select currentproductionmonth from sysset)    " +
                      "    and pm.plancode = 'MP' and w.Description = r.Workplace    order by captyear desc, convert(decimal(18,0),actweek)   desc ";
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
                    DistToHolTxt2.Text = _dbMan.ResultsDataTable.Rows[0]["Cat13Rate"].ToString();
                    //if (_dbMan.ResultsDataTable.Rows[0]["Cat13Rate"].ToString() == "Y")
                    //{
                    //    NewPnlYCheck.Checked = true;
                    //    NewPnlNCheck.Checked = false;
                    //}
                    //else
                    //{
                    //    NewPnlNCheck.Checked = true;
                    //    NewPnlYCheck.Checked = false;
                    //}

                    if (_dbMan.ResultsDataTable.Rows[0]["Cat14Rate"].ToString() == "Y")
                    {
                        ChecklistYCheck.Checked = true;
                        ChecklistNCheck.Checked = false;
                    }
                    else
                    {
                        ChecklistNCheck.Checked = true;
                        ChecklistYCheck.Checked = false;
                    }

                    Cat15Txt.Text = _dbMan.ResultsDataTable.Rows[0]["Cat15Rate"].ToString();

                    if (_dbMan.ResultsDataTable.Rows[0]["Cat16Rate"].ToString() == "Y")
                    {
                        SecEscYCheck.Checked = true;
                        SecEscNCheck.Checked = false;
                    }
                    else
                    {
                        SecEscNCheck.Checked = true;
                        SecEscYCheck.Checked = false;
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

                    TxtCrew.Text = _dbMan.ResultsDataTable.Rows[0]["CrewName"].ToString();

                }

            }

            if (_dbMan.ResultsDataTable.Rows.Count == 0 && WkLbl.Text != "7")
            {
                //MessageBox.Show("Unable to capture data for this week.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
            }

            Cursor = Cursors.Default;
        }

        private void RockEnginSavebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

            //decimal panelRating = 0;

            //try
            //{
            //     int CheckValue = Convert.ToInt32(Cat12Txt.Text);


            //}
            //catch (Exception)
            //{
            //    panelRating = Math.Round(  Convert.ToDecimal( Cat12Txt.Text) , 0 , MidpointRounding.ToEven);

            //    Cat12Txt.Text = Convert.ToString(panelRating);

            //}

            string SqlStatement = "";

            
            SqlStatement = " delete from [dbo].[tbl_DPT_RockMechInspection] where workplace = '" + WPLbl.Text + "' and actweek = convert(decimal(18,0),'" + WkLbl.Text + "')    \r\n\r\n";
            
            
            SqlStatement = SqlStatement + " insert into [dbo].[tbl_DPT_RockMechInspection] VALUES ( '" + WPLbl.Text + "', datepart(Year,GETDATE()), datepart(ISOWK,GETDATE()), convert(decimal(18,0),'" + WkLbl.Text + "'), getdate(), '" + TUserInfo.UserID + "', \r\n" +
                                  " '" + Cat1RateTxt.Text + "', '" + Cat1AmountTxt.Text + "', '" + Cat1NotesTxt.Text + "', \r\n" +
                                  " '" + Cat2RateTxt.Text + "', '" + Cat2AmountTxt.Text + "', '" + Cat2NotesTxt.Text + "', \r\n" +
                                  " '" + Cat3RateTxt.Text + "', '" + Cat3AmountTxt.Text + "', '" + Cat3NotesTxt.Text + "', \r\n" +
                                  " '" + Cat4RateTxt.Text + "', '" + Cat4AmountTxt.Text + "', '" + Cat4NotesTxt.Text + "', \r\n" +
                                  " '" + Cat5RateTxt.Text + "', '" + Cat5AmountTxt.Text + "', '" + Cat5NotesTxt.Text + "', \r\n" +
                                  " '" + Cat6RateTxt.Text + "', '" + Cat6AmountTxt.Text + "', '" + Cat6NotesTxt.Text + "', \r\n" +
                                  " '" + Cat7RateTxt.Text + "', '" + Cat7AmountTxt.Text + "', '" + Cat7NotesTxt.Text + "', \r\n" +
                                  " '" + Cat8RateTxt.Text + "', '" + Cat8AmountTxt.Text + "', '" + Cat8NotesTxt.Text + "', \r\n" +
                                  " '" + Cat9RateTxt.Text + "', '" + Cat9AmountTxt.Text + "', '" + Cat9NotesTxt.Text + "', \r\n" +
                                  " '" + Cat10RateTxt.Text + "', '" + Cat10AmountTxt.Text + "', '" + Cat10NotesTxt.Text + "', \r\n" +
                                  " '" + Cat11RateTxt.Text + "', '" + Cat11AmountTxt.Text + "', '" + Cat11NotesTxt.Text + "', \r\n" +

                                  " '" + Cat12Txt.Text + "', '" + Cat12NotesTxt.Text + "', \r\n" +
                                  " '" + DistToHolTxt2.Text + "', '" + Cat13NotesTxt.Text + "', \r\n" +
                                  " '" + checklist + "', '" + Cat14NotesTxt.Text + "', \r\n" +
                                  " '" + Cat15Txt.Text + "',  '" + Cat15NotesTxt.Text + "', \r\n" +
                                  " '" + SecEsc + "', '" + Cat16NotesTxt.Text + "', \r\n" +
                                  " '" + Cat17RateTxt.Text + "', '" + Cat17AmountTxt.Text + "', '" + Cat17NotesTxt.Text + "', \r\n" +
                                  " '" + Cat18RateTxt.Text + "', '" + Cat18AmountTxt.Text + "', '" + Cat18NotesTxt.Text + "', \r\n" +
                                  " '" + Cat19RateTxt.Text + "', '" + Cat19AmountTxt.Text + "', '" + Cat19NotesTxt.Text + "', \r\n" +
                                  " '" + Cat20RateTxt.Text + "', '" + Cat20AmountTxt.Text + "', '" + Cat20NotesTxt.Text + "', \r\n" +
                                  " '" + Cat21Txt.Text + "',  \r\n" +
                                  " '" + Cat22Txt.Text + "',  \r\n" +
                                  " '" + Cat23Txt.Text + "',  \r\n" +
                                  " '" + Cat24Txt.Text + "', '" + txtAttachment.Text + "',  '" + commentsTxt.Text + "' \r\n" +

                                  " ,  '" + WPStatus + "' ) ";

            _clsSetupCycles.SaveRockEngInspection(SqlStatement);

            Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Data Saved", "Inspection saved", Color.CornflowerBlue);

            LoadReport();
        }

        private void RockEnginAddImagebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp, *.tiff) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png;*.bpm; *.tiff";
            result1 = openFileDialog1.ShowDialog();

            GetFile();
        }

        void GetFile()
        {

            Random r = new Random();
            //System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(@"C:\Images");
            // System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(@"\\172.18.16.77\images\GradeSheets");
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(ImageDir + "\\RockMech");

            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //string[] files = System.IO.Directory.GetFiles(@"C:\Images");
            //string[] files = System.IO.Directory.GetFiles(@"\\172.18.16.77\images\GradeSheets");
            string[] files = System.IO.Directory.GetFiles(ImageDir + "\\RockMech");

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
                destinationFile = ImageDir + "\\RockMech" + "\\" + FileName + Ext;


                if (files.Length != 0)
                {
                    foreach (FileInfo f1 in list2)
                    {
                        if (f1.Name == openFileDialog1.SafeFileName + Name.ToString())
                        {
                            Name = r.Next(0, 50);

                            //destinationFile = @"C:\Images" + "\\" + FileName + Ext;//+ FileName + Name.ToString() + Ext
                            destinationFile = ImageDir + "\\RockMech" + "\\" + FileName + Ext;//+ FileName + Name.ToString() + Ext
                            //PicBox.Image = Image.FromFile(openFileDialog1.FileName); 
                        }

                    }

                    try
                    {
                        //System.IO.File.Copy(sourceFile, destinationFile, true);
                        SaveImage(sourceFile, FileName, Ext, ImageDir + "\\RockMech\\");
                    }
                    catch
                    {

                    }

                }
                else
                {
                    //System.IO.File.Copy(sourceFile, destinationFile, true);
                    System.IO.File.Copy(sourceFile, ImageDir + "\\RockMech" + "\\" + FileName + Ext, true);

                   

                    dir2 = new System.IO.DirectoryInfo(ImageDir + "\\RockMech");

                    list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                    //PicBox.Image = Image.FromFile(openFileDialog1.FileName); 
                }

                txtAttachment.Text = destinationFile;
                PicBox.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void SaveImage(string sourceFile, string Filename,string Ext,string destinatioPath)
        {
            Bitmap imageRockEng = new Bitmap(sourceFile);
            System.Drawing.Imaging.ImageCodecInfo jgpEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            //Set Image quality to 60
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 60L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            imageRockEng.Save(destinatioPath+"\\"+Filename+""+Ext, jgpEncoder, myEncoderParameters);

        }

        private System.Drawing.Imaging.ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
        {
            System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders();
            foreach (System.Drawing.Imaging.ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void Cat1RateTxt_TextChanged(object sender, EventArgs e)
        {
           
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
            pbx2.Visible = false;

            if (Cat2RateTxt.Value.ToString() == "0")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxGreen.Image;
                pbx2.Visible = true;
            }

            if (Cat2RateTxt.Value.ToString() == "1")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxOrange.Image;
                pbx2.Visible = true;
            }

            if (Cat2RateTxt.Value.ToString() == "2")
            {
                pbx2.BringToFront();
                pbx2.Image = pbxRed.Image;
                pbx2.Visible = true;
            }
        }

        private void Cat3RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx3.Visible = false;

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
            pbx4.Visible = false;

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
            pbx5.Visible = false;

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
            pbx6.Visible = false;

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

        private void Cat7RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx7.Visible = false;

            if (Cat7RateTxt.Value.ToString() == "0")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxGreen.Image;
                pbx7.Visible = true;
            }

            if (Cat7RateTxt.Value.ToString() == "1")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxOrange.Image;
                pbx7.Visible = true;
            }

            if (Cat7RateTxt.Value.ToString() == "2")
            {
                pbx7.BringToFront();
                pbx7.Image = pbxRed.Image;
                pbx7.Visible = true;
            }
        }

        private void Cat8RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx8.Visible = false;

            if (Cat8RateTxt.Value.ToString() == "0")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxGreen.Image;
                pbx8.Visible = true;
            }

            if (Cat8RateTxt.Value.ToString() == "1")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxOrange.Image;
                pbx8.Visible = true;
            }

            if (Cat8RateTxt.Value.ToString() == "2")
            {
                pbx8.BringToFront();
                pbx8.Image = pbxRed.Image;
                pbx8.Visible = true;
            }
        }

        private void Cat9RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx9.Visible = false;

            if (Cat9RateTxt.Value.ToString() == "0")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxGreen.Image;
                pbx9.Visible = true;
            }

            if (Cat9RateTxt.Value.ToString() == "1")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxOrange.Image;
                pbx9.Visible = true;
            }

            if (Cat9RateTxt.Value.ToString() == "2")
            {
                pbx9.BringToFront();
                pbx9.Image = pbxRed.Image;
                pbx9.Visible = true;
            }
        }

        private void Cat10RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx10.Visible = false;

            if (Cat10RateTxt.Value.ToString() == "0")
            {
                pbx10.BringToFront();
                pbx10.Image = pbxGreen.Image;
                pbx10.Visible = true;
            }

            if (Cat10RateTxt.Value.ToString() == "1")
            {
                pbx10.BringToFront();
                pbx10.Image = pbxOrange.Image;
                pbx10.Visible = true;
            }

            if (Cat10RateTxt.Value.ToString() == "2")
            {
                pbx10.BringToFront();
                pbx10.Image = pbxRed.Image;
                pbx10.Visible = true;
            }
        }

        private void Cat11RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx11.Visible = false;

            if (Cat11RateTxt.Value.ToString() == "0")
            {
                pbx11.BringToFront();
                pbx11.Image = pbxGreen.Image;
                pbx11.Visible = true;
            }

            if (Cat11RateTxt.Value.ToString() == "1")
            {
                pbx11.BringToFront();
                pbx11.Image = pbxOrange.Image;
                pbx11.Visible = true;
            }

            if (Cat11RateTxt.Value.ToString() == "2")
            {
                pbx11.BringToFront();
                pbx11.Image = pbxRed.Image;
                pbx11.Visible = true;
            }
        }

        private void Cat12Txt_EditValueChanged(object sender, EventArgs e)
        {
            pbx12.Visible = false;

            return;

            if (Cat12Txt.Text.ToString() == "0")
            {
                pbx12.BringToFront();
                pbx12.Image = pbxGreen.Image;
                pbx12.Visible = true;
            }

            if (Cat12Txt.Text.ToString() == "1")
            {
                pbx12.BringToFront();
                pbx12.Image = pbxOrange.Image;
                pbx12.Visible = true;
            }

            if (Cat12Txt.Text.ToString() == "2")
            {
                pbx12.BringToFront();
                pbx12.Image = pbxRed.Image;
                pbx12.Visible = true;
            }
        }

        private void Cat17RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx16.Visible = false;

            return;

            if (Cat17RateTxt.Text.ToString() == "0")
            {
                pbx16.BringToFront();
                pbx16.Image = pbxGreen.Image;
                pbx16.Visible = true;
            }

            if (Cat17RateTxt.Text.ToString() == "1")
            {
                pbx16.BringToFront();
                pbx16.Image = pbxOrange.Image;
                pbx16.Visible = true;
            }

            if (Cat17RateTxt.Text.ToString() == "2")
            {
                pbx16.BringToFront();
                pbx16.Image = pbxRed.Image;
                pbx16.Visible = true;
            }
        }

        private void Cat18RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx17.Visible = false;

            return;

            if (Cat18RateTxt.Text.ToString() == "0")
            {
                pbx17.BringToFront();
                pbx17.Image = pbxGreen.Image;
                pbx17.Visible = true;
            }

            if (Cat18RateTxt.Text.ToString() == "1")
            {
                pbx17.BringToFront();
                pbx17.Image = pbxOrange.Image;
                pbx17.Visible = true;
            }

            if (Cat18RateTxt.Text.ToString() == "2")
            {
                pbx17.BringToFront();
                pbx17.Image = pbxRed.Image;
                pbx17.Visible = true;
            }
        }

        private void Cat19RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx18.Visible = false;

            return;

            if (Cat19RateTxt.Text.ToString() == "0")
            {
                pbx18.BringToFront();
                pbx18.Image = pbxGreen.Image;
                pbx18.Visible = true;
            }

            if (Cat19RateTxt.Text.ToString() == "1")
            {
                pbx18.BringToFront();
                pbx18.Image = pbxOrange.Image;
                pbx18.Visible = true;
            }

            if (Cat19RateTxt.Text.ToString() == "2")
            {
                pbx18.BringToFront();
                pbx18.Image = pbxRed.Image;
                pbx18.Visible = true;
            }
        }

        private void Cat20RateTxt_EditValueChanged(object sender, EventArgs e)
        {
            pbx19.Visible = false;

            return;

            if (Cat20RateTxt.Text.ToString() == "0")
            {
                pbx19.BringToFront();
                pbx19.Image = pbxGreen.Image;
                pbx19.Visible = true;
            }

            if (Cat20RateTxt.Text.ToString() == "1")
            {
                pbx19.BringToFront();
                pbx19.Image = pbxOrange.Image;
                pbx19.Visible = true;
            }

            if (Cat20RateTxt.Text.ToString() == "2")
            {
                pbx19.BringToFront();
                pbx19.Image = pbxRed.Image;
                pbx19.Visible = true;
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void AdjustBookGB_Enter(object sender, EventArgs e)
        {

        }

        private void pcReport_Load(object sender, EventArgs e)
        {

        }
    }
}