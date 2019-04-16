using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;
using System.Threading;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.ProblemHistoryGraph
{
    public partial class ucProblemsReport : ucReportSettingsControl
    {
        private sysMessagesClass _sysMessagesClass = new sysMessagesClass();
        private clsProblemsReportData _clsProblemsReportData = new clsProblemsReportData();
        private clsProblemsReportSettings reportSettings = new clsProblemsReportSettings();

        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        private Thread theReportThread;
        private string theSystemDBTag = "DBHARMONYPAS";

        private DataTable dtActivity;
        private DataTable dtProblemType;
        private DataTable dtSysset;
        private DataTable dtHier;

        private string TypeText;
        private string ProblemTypeText;

        private DateTime RunDate;
        private string TheRunDate;
        private string Banner;
        private int MOHier;
        private int Prodmonth;

        string Hier;
        string sec;

        string GraphType;

        DataTable dtSections = new DataTable();

        Procedures procs = new Procedures();

        public ucProblemsReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void ucProblemsReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            reportSettings.UserCurrentInfo = this.UserCurrentInfo.Connection;
            reportSettings.DBTag = theSystemDBTag;  // this.theSystemDBTag;

            setReportSetttings();

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan.SqlStatement = "select currentproductionmonth from sysset";


            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            ProdMonthTxt.Text = Convert.ToString(_dbMan.ResultsDataTable.Rows[0][0].ToString());

            Procedures procs = new Procedures();
            procs.ProdMonthVis2(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;

            /////WPAS

            //HQCatCmb.Items.Clear();
            //MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            //_dbMan.SqlStatement = "select Distinct(HQCat) from PROBLEM";


            //_dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbMan.ExecuteInstruction();

            //DataTable dtResp = _dbMan.ResultsDataTable;

            HQCatCmb.Items.Add("All");
            //foreach (DataRow dr in dtResp.Rows)
            //{

            //    HQCatCmb.Items.Add(dr["HQCat"].ToString());

            //}

            ProbGroupCmb.Items.Clear();
            MWDataManager.clsDataAccess _dbManProbGroup = new MWDataManager.clsDataAccess();
            _dbManProbGroup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbManProbGroup.SqlStatement = "Select Distinct Description from CODE_PROBLEM_TYPE where Deleted = 'N' " +
                                           "Order by Description";


            _dbManProbGroup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManProbGroup.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManProbGroup.ExecuteInstruction();

            DataTable dtProbGroup = _dbManProbGroup.ResultsDataTable;

            ProbGroupCmb.Items.Add("All");
            foreach (DataRow drProbGroup in dtProbGroup.Rows)
            {

                ProbGroupCmb.Items.Add(drProbGroup["Description"].ToString());

            }

            HQCatCmb.SelectedIndex = 0;
            UnderSectionCmb.SelectedIndex = 0;
            reportSettings.Section = UnderSectionCmb.Text;
            ProbGroupCmb.SelectedIndex = 0;

            ProblemsRgb.SelectedIndex = 3;

            label11.Text = "selected for the Number";

            NumberBtn.BackColor = System.Drawing.Color.LightGreen;
        }
        private void setReportSetttings()
        {
            _clsProblemsReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtSysset = _clsProblemsReportData.get_Sysset();
            if (dtSysset.Rows.Count > 0)
            {
                RunDate = Convert.ToDateTime(dtSysset.Rows[0]["RunDate"].ToString());
                TheRunDate = dtSysset.Rows[0]["TheRunDate"].ToString();
                Prodmonth = Convert.ToInt32(dtSysset.Rows[0]["CurrentProductionMonth"].ToString());
                Banner = dtSysset.Rows[0]["Banner"].ToString();
                MOHier = Convert.ToInt32(dtSysset.Rows[0]["MOHierarchicalID"].ToString());
            }
            reportSettings.ReportDate = RunDate.Date;
            iReportDate.Properties.Value = reportSettings.ReportDate;
            reportSettings.Prodmonth = Prodmonth.ToString();
            iProdmonth.Properties.Value = reportSettings.Prodmonth;
            reportSettings.IncludeGraphs = false;

            Load_Activity();
            Load_Hier();
            reportSettings.Activity = "Stoping";
            reportSettings.TheType = MOHier.ToString();

            iActivity.Properties.Value = reportSettings.Activity;
            iType.Properties.Value = reportSettings.TheType;
            iIncludeGraph.Properties.Value = reportSettings.IncludeGraphs;
            pgProblemReport.SelectedObject = reportSettings;
        }
        public override bool prepareReport()
        {
            bool theResult;
            theReportThread = new Thread(new ParameterizedThreadStart(createReport));
            theReportThread.SetApartmentState(ApartmentState.STA);
            theReportThread.Start(reportSettings);
            theResult = true;
            return theResult;
        }

        private void createReport(Object theReportSettings)
        {
            Report theReport2prob = new Report();
            String Lbl2 = "";

            //if (radioGroup1.SelectedIndex == 0)
            //    Lbl2 = "Prodmonth- " + ProdMonthTxt.Value.ToString();
            //else
            //    Lbl2 = "From- " + String.Format("{0:dd-MMM-yyyy}", FromDate.Value) + "   To- " + String.Format("{0:dd-MMM-yyyy}", ToDate.Value);

            //int NoLostBlasts = 0;

            //// do no blasts
            //MWDataManager.clsDataAccess _dbMan7 = new MWDataManager.clsDataAccess();
            //_dbMan7.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            ////_dbMan7.SqlStatement = "select sum(bb) bb, sum(lb) lb \r\n ";

            //_dbMan7.SqlStatement = "select * \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "from (select  case when bookprob <> '' then 1 else 0 end as pr, ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "case when booktons <> 0 then 1 else 0 end as bb, case when PlanSqm <> 0 and  bookprob <> '' then 1 else 0 end as lb  from PLANNING_Vamping p,SECTION s,SECTION s1,SECTION s2,SECTION s3,SECTION s4 \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "where p.prodmonth <> '200000' \r\n ";

            //if (radioGroup1.SelectedIndex == 0)
            //{
            //    _dbMan7.SqlStatement = _dbMan7.SqlStatement + "and p.prodmonth = '" + ProdMonthTxt.Value.ToString() + "' \r\n ";
            //}
            //if (radioGroup1.SelectedIndex != 0)
            //{
            //    _dbMan7.SqlStatement = _dbMan7.SqlStatement + "and calendardate >= '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "' and calendardate <= '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "' \r\n ";
            //}

            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and p.Prodmonth = s.Prodmonth \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and p.Prodmonth = s1.Prodmonth \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and p.Prodmonth = s2.Prodmonth \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and p.Prodmonth = s3.Prodmonth \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and p.Prodmonth = s4.Prodmonth \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and p.SectionID = s.SectionID \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and s.ReportToSectionid = s1.SectionID \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and s1.ReportToSectionid = s2.SectionID \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and s2.ReportToSectionid = s3.SectionID \r\n ";
            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + "and s3.ReportToSectionid = s4.SectionID \r\n ";

            //if (Hier == "1")
            //{
            //    _dbMan7.SqlStatement = _dbMan7.SqlStatement + " and s4.ReportToSectionid = '" + sec + "' \r\n ";
            //}
            //if (Hier == "2")
            //{
            //    _dbMan7.SqlStatement = _dbMan7.SqlStatement + " and s3.ReportToSectionid = '" + sec + "' \r\n ";
            //}
            //if (Hier == "3")
            //{
            //    _dbMan7.SqlStatement = _dbMan7.SqlStatement + " and s2.ReportToSectionid = '" + sec + "' \r\n ";
            //}
            //if (Hier == "4")
            //{
            //    _dbMan7.SqlStatement = _dbMan7.SqlStatement + " and s1.ReportToSectionid = '" + sec + "' \r\n ";
            //}
            //if (Hier == "5")
            //{
            //    _dbMan7.SqlStatement = _dbMan7.SqlStatement + " and s.ReportToSectionid = '" + sec + "' \r\n ";
            //}
            //if (Hier == "6")
            //{
            //    _dbMan7.SqlStatement = _dbMan7.SqlStatement + " and s.SectionID = '" + sec + "' \r\n ";
            //}



            //_dbMan7.SqlStatement = _dbMan7.SqlStatement + ") a  ";

            //_dbMan7.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan7.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbMan7.ResultsTableName = "Data";
            //textBox1.Text = _dbMan7.SqlStatement;
            //_dbMan7.ExecuteInstruction();
            //DataTable Neil = _dbMan7.ResultsDataTable;

            //NoBlastsLbl.Text = "0";// Neil.Rows[0]["bb"].ToString();
            //NoDualBlastsLbl.Text = "0";
            //NoProblemsLbl.Text = "0";
            //LostBlastLbl.Text = "0";

            //for (int i = 0; i < Neil.Rows.Count; i++)
            //{
            //    NoBlastsLbl.Text = (Convert.ToInt32(NoBlastsLbl.Text) + Convert.ToInt32(Neil.Rows[i]["bb"].ToString())).ToString();
            //    NoProblemsLbl.Text = (Convert.ToInt32(NoProblemsLbl.Text) + Convert.ToInt32(Neil.Rows[i]["pr"].ToString())).ToString();
            //    LostBlastLbl.Text = (Convert.ToInt32(LostBlastLbl.Text) + Convert.ToInt32(Neil.Rows[i]["lb"].ToString())).ToString();

            //}


            if (TypeRgb.SelectedIndex == 0)
            {

                MWDataManager.clsDataAccess _dbManGraph = new MWDataManager.clsDataAccess();
                _dbManGraph.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

              
                    if (label11.Text == "selected for the Number")
                    {
                        _dbManGraph.SqlStatement = "select Top 10 description, sum(TheCount) TheCount, '" + SysSettings.Banner + "' banner, '" + Lbl2 + "' Lbl2, '" + GraphType + "' Graph, '" + label11.Text + "' label, '"+ reportSettings.Section + "' TheSection, '" + HQCatCmb.Text + "' theHqCat, '' theProbGroup from (select *, TheCount1 TheCount  from (select \r\n " +
                             " sum(count11) [TheCount1], SUM(BookSqm) BookSQM, SUM(BookTons) BookTons, SUM(BookKgs) BookKgs, wwwwp, Description [Description], max(mo) mo \r\n " +
                                              "from ";

                    }
                    else
                    {
                        //_dbManGraph.SqlStatement = "select Top 10 description, sum(TheCount) TheCount, '" + SysSettings.Banner + "' banner, '" + Lbl2 + "' Lbl2, '" + GraphType + "' Graph, '" + label11.Text + "' label, '" + procs.ExtractAfterColon(UnderSectionCmb.Text) + "' TheSection, '" + HQCatCmb.Text + "' theHqCat, '" + ProbGroupCmb.Text + "' theProbGroup from (select *, TheCount1*advblast TheCount from (select \r\n " +
                        if (label11.Text == "selected for the Sqm")
                            _dbManGraph.SqlStatement = "select Top 10 description, SUM(plansqm) TheCount, '" + SysSettings.Banner + "' banner, '" + Lbl2 + "' Lbl2, '" + GraphType + "' Graph, '" + label11.Text + "' label, '" + reportSettings.Section + "' TheSection, '" + HQCatCmb.Text + "' theHqCat, '" + ProbGroupCmb.Text + "' theProbGroup from (select *, TheCount1*advblast TheCount from (select \r\n ";
                        if (label11.Text == "selected for the Stope Tons")
                            _dbManGraph.SqlStatement = "select Top 10 description, SUM(plantons) TheCount, '" + SysSettings.Banner + "' banner, '" + Lbl2 + "' Lbl2, '" + GraphType + "' Graph, '" + label11.Text + "' label, '" + reportSettings.Section + "' TheSection, '" + HQCatCmb.Text + "' theHqCat, '" + ProbGroupCmb.Text + "' theProbGroup from (select *, TheCount1*advblast TheCount from (select \r\n ";
                        if (label11.Text == "selected for the Stope Kgs")
                            _dbManGraph.SqlStatement = "select Top 10 description, SUM(plankg) TheCount, '" + SysSettings.Banner + "' banner, '" + Lbl2 + "' Lbl2, '" + GraphType + "' Graph, '" + label11.Text + "' label, '" + reportSettings.Section + "' TheSection, '" + HQCatCmb.Text + "' theHqCat, '" + ProbGroupCmb.Text + "' theProbGroup from (select *, TheCount1*advblast TheCount from (select \r\n ";

                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " sum(count11) [TheCount1], SUM(BookSqm) BookSQM, SUM(BookTons) BookTons, SUM(BookKgs) BookKgs, wwwwp, Description [Description], max(mo) mo, sum(plansqm)plansqm, sum(plantons)plantons, sum(plankg)plankg \r\n " +
                                             "from ";
                    }

                    if (label11.Text == "selected for the Number")
                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "( select  1 count11, a.BookSQM, a.BookTons, a.BookGrams/1000 BookKgs, \r\n ";
                    if (label11.Text == "selected for the Sqm")
                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "( select  Convert(decimal(10,0),(pm.FL * 1)) count11, a.BookSQM, a.BookTons, a.BookGrams/1000 BookKgs, \r\n ";
                    if (label11.Text == "selected for the Stope Tons")
                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "( select  Convert(decimal(10,0),pm.FL * 1*pm.Density*pm.SW/100) count11, a.BookSQM, a.BookTons, a.BookGrams/1000 BookKgs, \r\n ";
                    if (label11.Text == "selected for the Stope Kgs")
                        //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "( select Convert(decimal(10,0),((pm.FL * 1*pm.Dens) * pm.cmGT/100 )/31.10348) count11, \r\n ";
                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "( select  Convert(decimal(10,3),((pm.FL * 1*pm.Density) * pm.cmGT/100 )/1000) count11, a.BookSQM, a.BookTons, a.BookGrams/1000 BookKgs, \r\n ";

                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " d.workplaceid wwwwp, b.Description Description, s1.ReportToSectionid mo, a.sqm PlanSqm, a.tons PlanTons, a.reefsqm*a.cmgt/100*(select RockDensity from sysset)/1000 plankg from Planning a, CODE_Problem b, Problem_Type c, Workplace d, SECTION s, SECTION s1, SECTION s2, SECTION s3, SECTION s4, PlanMonth pm, Planning pp \r\n ";

                    if (radioGroup1.SelectedIndex == 0)
                    {
                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "where a.Prodmonth = '" + ProdMonthTxt.Value.ToString() + "' and a.ProblemID = b.ProblemID \r\n ";
                    }
                    if (radioGroup1.SelectedIndex != 0)
                    {
                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "where a.calendardate >= '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "' and a.calendardate <= '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "' and a.ProblemID = b.ProblemID \r\n ";
                    }
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.prodmonth = pm.prodmonth and a.sectionid = pm.sectionid and a.workplaceid = pm.workplaceid and a.activity = pm.activity \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "--and b.ProblemGroupCode = c.ProblemGroupCode \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.WorkplaceID = d.WorkplaceID \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.Activity in (0,9) \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.ProblemID = b.ProblemID \r\n ";

                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.Prodmonth = s.Prodmonth \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.Prodmonth = s1.Prodmonth \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.Prodmonth = s2.Prodmonth \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.Prodmonth = s3.Prodmonth \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.Prodmonth = s4.Prodmonth \r\n ";

                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.WorkplaceID = pp.WorkplaceID \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.calendardate = pp.calendardate \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.sectionid = pp.sectionid \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.prodmonth = pp.prodmonth \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.Activity = pp.Activity \r\n ";

                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.SectionID = s.SectionID \r\n ";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and s.ReportToSectionid = s1.SectionID \r\n ";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and s1.ReportToSectionid = s2.SectionID \r\n ";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and s2.ReportToSectionid = s3.SectionID \r\n ";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and s3.ReportToSectionid = s4.SectionID \r\n ";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.ProblemID is not null \r\n ";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.ProblemID = b.ProblemID \r\n ";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "and a.ProblemID = c.ProblemID \r\n ";

                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and a.Activity = b.Activity  \r\n ";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and a.Activity = c.Activity   and pm.PlanCode = 'MP'  \r\n ";





                if (Hier == "1")
                {
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s4.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "2")
                {
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s3.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "3")
                {
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s2.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "4")
                {
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s1.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "5")
                {
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "6")
                {
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s.SectionID = '" + sec + "' \r\n ";
                }

              

                if (HQCatCmb.SelectedIndex > 0)
                {
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and b.HQCat = '" + HQCatCmb.Text + "' \r\n ";
                }

                if (ProblemsRgb.SelectedIndex == 1)
                {
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and CausedLostBlast = 'Y' \r\n ";
                }
                else
                {
                    if (ProblemsRgb.SelectedIndex == 2)
                    {
                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and a.CausedLostBlast = 'N' \r\n ";
                    }

                    if (ProblemsRgb.SelectedIndex == 3)
                    {
                        _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and a.booktons = 0 and pp.MOCycle in ('BL','SUBL') \r\n ";
                    }
                }

              
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + ") a group by wwwwp, Description) a \r\n ";

                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "left outer join (select sectionid   sss, stopingcycle, DevCycle from Code_Cycle_MOCycleConfig ) bb  on a.mo = bb.sss  \r\n ";

                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "left outer join \r\n ";

                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "(select Name Name1, avg(advBlast) advBlast from Code_Cycle_RawData \r\n ";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " group by Name) zzz on bb.stopingcycle = zzz.Name1) a group by  Description order by theCount Desc \r\n ";

                _dbManGraph.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGraph.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGraph.ResultsTableName = "Graph";
                textBox1.Text = _dbManGraph.SqlStatement;
                _dbManGraph.ExecuteInstruction();

                DataSet dsGraph = new DataSet();

                dsGraph.Tables.Add(_dbManGraph.ResultsDataTable);
                theReport2prob.RegisterData(dsGraph);



            }
            else
            {
                MWDataManager.clsDataAccess _dbManGraphDev = new MWDataManager.clsDataAccess();
                _dbManGraphDev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
             
                    _dbManGraphDev.SqlStatement = "select Top 10 '" + SysSettings.Banner + "' banner, '" + Lbl2 + "' Lbl2, '" + GraphType + "' Graph, '" + label11.Text + "' label, '" + reportSettings.Section + "' TheSection, '" + HQCatCmb.Text + "' theHqCat, '" + ProbGroupCmb.Text + "' theProbGroup, \r\n ";

                    if (label11.Text == "selected for the Number")
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " sum(count1) [TheCount], Description [Description] \r\n ";
                    if (label11.Text == "selected for the Development meters")
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " sum(count1)-SUM(BookMetresadvance) [TheCount1], Description [Description], SUM(advBlast)-SUM(BookMetresadvance) TheCount \r\n ";
                    if (label11.Text == "selected for the Development Tons")
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " sum(count1)-SUM(BookTons) [TheCount1], Description [Description], sum(advBlast * count1)-SUM(BookTons) TheCount \r\n ";
                    if (label11.Text == "selected for the Development Kgs")
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " sum(count1)-SUM(BookKgs) [TheCount1], Description [Description], sum(advBlast * count1)-SUM(BookKgs) [TheCount] \r\n ";

                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "from ";

                    if (label11.Text == "selected for the Number")
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "( select Distinct 1 count1, a.BookMetresadvance, a.BookTons, (a.BookGrams/1000) BookKgs, \r\n ";
                    if (label11.Text == "selected for the Development meters")
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "( select Distinct 1.5 count1, a.BookMetresadvance, a.BookTons, (a.BookGrams/1000) BookKgs, \r\n ";
                    if (label11.Text == "selected for the Development Tons")
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "( select Distinct Convert(decimal(10,0),pm.DHeight * pm.DWidth * pm.Density) count1, a.BookMetresadvance, a.BookTons, (a.BookGrams/1000) BookKgs, \r\n ";
                    if (label11.Text == "selected for the Development Kgs")
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "( select Distinct Convert(decimal(10,3),((pm.cmgt/100 * pm.DWidth * pm.Density) )/1000)  count1, a.BookMetresadvance, a.BookTons, (a.BookGrams/1000) BookKgs, \r\n ";

                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " b.Description Description, d.Description wp1, d.endtypeid, s1.ReportToSectionid mo from planning a, CODE_Problem b, Problem_Type c, Workplace d,SECTION s,SECTION s1,SECTION s2,SECTION s3,SECTION s4, PlanMonth pm, Planning pp \r\n ";
                    //_dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "where a.Prodmonth = '" + ProdMonthTxt.Value.ToString() + "' and a.ProblemID = b.ProblemID ";
                    if (radioGroup1.SelectedIndex == 0)
                    {
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "where a.Prodmonth = '" + ProdMonthTxt.Value.ToString() + "' and a.ProblemID = b.ProblemID  \r\n";
                    }
                    if (radioGroup1.SelectedIndex != 0)
                    {
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "where a.calendardate >= '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "' and a.calendardate <= '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "' and a.ProblemID = b.ProblemID \r\n ";
                    }
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.prodmonth = pm.prodmonth and a.sectionid = pm.sectionid and a.workplaceid = pm.workplaceid and a.activity = pm.activity \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "--and b.ProblemGroupCode = c.ProblemGroupCode \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.WorkplaceID = d.WorkplaceID \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.Activity in (1) \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.ProblemID = b.ProblemID \r\n ";

                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.Prodmonth = s.Prodmonth \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.Prodmonth = s1.Prodmonth \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.Prodmonth = s2.Prodmonth \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.Prodmonth = s3.Prodmonth \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.Prodmonth = s4.Prodmonth \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.SectionID = s.SectionID \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and s.ReportToSectionid = s1.SectionID \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and s1.ReportToSectionid = s2.SectionID \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and s2.ReportToSectionid = s3.SectionID \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and s3.ReportToSectionid = s4.SectionID \r\n ";

                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.WorkplaceID = pp.WorkplaceID \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.calendardate = pp.calendardate \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.sectionid = pp.sectionid \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.prodmonth = pp.prodmonth \r\n ";
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "and a.Activity = pp.Activity \r\n ";

               
                

                if (Hier == "1")
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and s4.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "2")
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and s3.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "3")
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and s2.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "4")
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and s1.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "5")
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and s.ReportToSectionid = '" + sec + "' \r\n ";
                }
                if (Hier == "6")
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and s.SectionID = '" + sec + "' \r\n ";
                }

                if (ProbGroupCmb.SelectedIndex > 0)
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and c.Description = '" + ProbGroupCmb.Text + "' \r\n ";
                }

                if (HQCatCmb.SelectedIndex > 0)
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and b.HQCat = '" + HQCatCmb.Text + "' \r\n ";
                }

                if (ProblemsRgb.SelectedIndex == 1)
                {
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and a.CausedLostBlast = 'Y' \r\n ";
                }
                else
                {
                    if (ProblemsRgb.SelectedIndex == 2)
                    {
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and a.CausedLostBlast = 'N' \r\n ";
                    }

                    if (ProblemsRgb.SelectedIndex == 3)
                    {
                        _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " and a.booktons = 0 and pp.MOCycle in ('BL','SUBL') \r\n ";
                    }
                }



                _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " ) a left outer join (select sectionid sec1111, stopingcycle, DevCycle from Code_Cycle_MOCycleConfig ) bb  on a.mo = bb.sec1111  \r\n ";

                _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "left outer join \r\n ";

                _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + "(select Name Name1, fl Endtype1, avg(advBlast) advBlast from Code_Cycle_RawData \r\n ";
                _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " group by Name, fl) zzz on bb.devcycle = zzz.Name1 and a.endtypeid = zzz.endtype1  \r\n ";



              
                    _dbManGraphDev.SqlStatement = _dbManGraphDev.SqlStatement + " group by Description order  by theCount Desc ";
          

            




                _dbManGraphDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGraphDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGraphDev.ResultsTableName = "Graph";
                textBox1.Text = _dbManGraphDev.SqlStatement;
                _dbManGraphDev.ExecuteInstruction();
                DataSet dsGraph = new DataSet();
                // DataSet dsGraph = new DataSet();
                dsGraph.Tables.Add(_dbManGraphDev.ResultsDataTable);

                theReport2prob.RegisterData(dsGraph);
                ////MessageBox.Show(GraphType);

            }

            theReport2prob.Load(TGlobalItems.ReportsFolder + "\\ProblemHistoryGraph.frx");
            //theReport2prob.Design();
         

            theReport2prob.Prepare();
            ActiveReport.SetReport = theReport2prob;
            ActiveReport.isDone = true;
        }
        public void Load_Hier()
        {
            _clsProblemsReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtHier = _clsProblemsReportData.get_Hier(Prodmonth);

            if (dtHier.Rows.Count != 0)
            {
                riType.DataSource = dtHier;
                riType.DisplayMember = "HierDesc";
                riType.ValueMember = "HierID";
            }
        }
        public void Load_Activity()
        {
            _clsProblemsReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtActivity = _clsProblemsReportData.get_Activity();

            if (dtActivity.Rows.Count != 0)
            {
                riActivity.DataSource = dtActivity;
                riActivity.DisplayMember = "Activity";
                riActivity.ValueMember = "Activity";
            }
        }

        private void pgProblemReport_Click(object sender, EventArgs e)
        {

        }

        private void UnderSectionCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            sec = procs.ExtractBeforeColon(UnderSectionCmb.SelectedItem.ToString());

            for (int x = 0; x <= dtSections.Rows.Count - 1; x++)
            {
                if (sec == dtSections.Rows[x][1].ToString())
                {
                    Hier = dtSections.Rows[x]["Hierarchicalid"].ToString();
                    break;
                }
            }
        }

        private void ProdMonthTxt_ValueChanged(object sender, EventArgs e)
        {
            UnderSectionCmb.Items.Clear();

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan.SqlStatement = "select * from Section where Prodmonth = '" + ProdMonthTxt.Value.ToString() + "'  and Hierarchicalid < 5 " +
                                   "order by Hierarchicalid, sectionid ";


            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            dtSections = _dbMan.ResultsDataTable;

            //UnderSectionCmb.Items.Add("All");
            foreach (DataRow dr in dtSections.Rows)
            {

                UnderSectionCmb.Items.Add(dr["sectionid"].ToString() + ":" + dr["name"].ToString());

            }
        }

        private void NumberBtn_Click(object sender, EventArgs e)
        {
            label11.Text = "selected for the Number";
            NumberBtn.BackColor = System.Drawing.Color.LightGreen;
            SqmAdvBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            TonsBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            OuncesBtn.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        private void SqmAdvBtn_Click(object sender, EventArgs e)
        {
            if (TypeRgb.SelectedIndex == 0)
                label11.Text = "selected for the Sqm";
            if (TypeRgb.SelectedIndex == 1)
                label11.Text = "selected for the Development meters";

            NumberBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            SqmAdvBtn.BackColor = System.Drawing.Color.LightGreen;
            TonsBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            OuncesBtn.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        private void TonsBtn_Click(object sender, EventArgs e)
        {
            if (TypeRgb.SelectedIndex == 0)
                label11.Text = "selected for the Stope Tons";
            if (TypeRgb.SelectedIndex == 1)
                label11.Text = "selected for the Development Tons";
            NumberBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            SqmAdvBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            TonsBtn.BackColor = System.Drawing.Color.LightGreen;
            OuncesBtn.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        private void OuncesBtn_Click(object sender, EventArgs e)
        {
            if (TypeRgb.SelectedIndex == 0)
                label11.Text = "selected for the Stope Kgs";
            if (TypeRgb.SelectedIndex == 1)
                label11.Text = "selected for the Development Kgs";

            NumberBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            SqmAdvBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            TonsBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            OuncesBtn.BackColor = System.Drawing.Color.LightGreen;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                FromLbl.Text = "Prod Month";
                FromDate.Visible = false;
                ToDate.Visible = false;
                ToLbl.Visible = false;
                ProdMonth1Txt.Visible = true;
                ProdMonthTxt.Visible = true;
            }
            else
            {
                FromLbl.Text = "From";
                FromDate.Visible = true;
                ToDate.Visible = true;
                ToLbl.Visible = true;
                ProdMonth1Txt.Visible = false;
                ProdMonthTxt.Visible = false;

                if (radioGroup1.SelectedIndex == 2)
                {
                    FromDate.Value = DateTime.Now.AddDays(-7);
                    ToDate.Value = DateTime.Now.AddDays(0);

                }

                if (radioGroup1.SelectedIndex == 3)
                {
                    FromDate.Value = DateTime.Now.AddDays(-30);
                    ToDate.Value = DateTime.Now.AddDays(0);

                }

            }
        }

        private void TypeRgb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypeRgb.SelectedIndex == 0)
                SqmAdvBtn.Text = "Sqm";
            if (TypeRgb.SelectedIndex == 1)
                SqmAdvBtn.Text = "Adv";
        }

        private void ProdMonthTxt_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonthTxt.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;
        }

        //public void Load_ProblemType()
        //{
        //    _clsProblemsReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
        //    dtProblemType = _clsProblemsReportData.get_ProblemType();

        //    if (dtProblemType.Rows.Count != 0)
        //    {
        //        riType.DataSource = dtProblemType;
        //        riType.DisplayMember = "TheProblemType";
        //        riType.ValueMember = "TheProblemType";
        //    }
        //}
    }
}
