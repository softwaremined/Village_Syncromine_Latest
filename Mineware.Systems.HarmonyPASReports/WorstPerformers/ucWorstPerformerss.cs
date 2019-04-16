using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using FastReport;
using System.Threading;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.WorstPerformers
{
    public partial class ucWorstPerformerss : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        string graphtype = "";
        private Thread theReportThread;
        Report Top20Graph = new Report();
        Procedures procs = new Procedures();

        string theSystemDBTag = "DBHARMONYPAS";
        private clsWorstPerformers reportSettings = new clsWorstPerformers();
        DataTable dtSections = new DataTable();
        DataTable dtCrew = new DataTable();
        DataTable dtStoping = new DataTable();
        DataTable dtDev = new DataTable();
        Report theReport = new Report();
        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        public ucWorstPerformerss()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;

            
        }

        private void ucWorstPerformerss_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            // reportSettings.ReportDate = DateTime.Now;
            //  DateTime dt = new DateTime();
            //  reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "(SELECT MAX(MillMonth)MillMonth FROM CALENDARMILL WHERE StartDate <= GETDATE())";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            DataTable dt = new DataTable();
            dt = _dbMan.ResultsDataTable;
            foreach (DataRow dr in dt.Rows)
            {
                reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(dr["MillMonth"].ToString());
            }
            // reportSettings.Prodmonth = Convert.ToInt32(TProductionGlobal.ProdMonthAsString(dt));
            iProdmonth.Properties.Value = reportSettings.Prodmonth;
            LoadSections();
            reportSettings.SectionID = dtSections.Rows[0][0].ToString();
            //reportSettings.Crew = dtCrew.Rows[0][0].ToString();


            reportSettings.Type = "MO Summary";

            reportSettings.Meas = "Gold";

            iType.Properties.Value = reportSettings.Type;
            iMeas.Properties.Value = reportSettings.Meas;


            reportSettings.Activity = 0;
            iActivity.Properties.Value = reportSettings.Activity;

            pgTopPanelsRepSettings.SelectedObject = reportSettings;

            if (reportSettings.Type == "MO Summary")
            {
                iCrew.Visible = false;
                //iSection.Visible = true;
            }
            else
            {
                iCrew.Visible = true;
               // iSection.Visible = false;
            }



        }


        private void LoadSections()
        {


            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " Select SectionID+':'+Name SectionID " +
                                  "from Section s where s.Prodmonth =  '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' ";

            _dbMan.SqlStatement += " and Hierarchicalid  = 4 ";
            _dbMan.SqlStatement += " order by SectionID   ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            dtSections = _dbMan.ResultsDataTable;
            riSection.DataSource = dtSections;
            riSection.DisplayMember = "SectionID";
            riSection.ValueMember = "SectionID";



            MWDataManager.clsDataAccess _dbManCrew = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbManCrew.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManCrew.SqlStatement = " Select Distinct(OrgUnitDay) Crew From PLanmonth where prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' " +
                                  "  ";


            _dbManCrew.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManCrew.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManCrew.ExecuteInstruction();

            dtCrew = _dbManCrew.ResultsDataTable;
            riCrewLookUpEdit.DataSource = dtCrew;
            riCrewLookUpEdit.DisplayMember = "Crew";
            riCrewLookUpEdit.ValueMember = "Crew";
        }

        public override bool prepareReport()
        {
            bool theResult = false;
            theReportThread = new Thread(new ParameterizedThreadStart(createReport));
            theReportThread.SetApartmentState(ApartmentState.STA);
            theReportThread.Start(reportSettings);
            theResult = true;
            return theResult;
        }

        string Plan = "";
        string Book = "";
        private void createReport(Object theReportSettings)
        {

            Plan = "Y";
            Book = "Y";
            if (reportSettings.Type == "Crew Summary")
            {

                if (reportSettings.Crew == "")
                {


                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    if (reportSettings.Activity == 0)
                    {

                        _dbMan.SqlStatement = "select Top 20 '" + SysSettings.Banner + "' banner, '" + Plan + "' planchk, '" + Book + "' Bookchk, convert(varchar(50), orgunitds+ '  ') org, a.*, case when a.booksqm is null then 0-a.plansqm else a.booksqm-a.plansqm end as var from " +
                                            "(select  orgunitds, " +
                                            "sum(sqm) plansqm, ";
                        if (SysSettings.AdjBook == "Y")
                        {
                            _dbMan.SqlStatement = _dbMan.SqlStatement + " sum(p.booksqm + p.AdjSqm) booksqm ";
                        }
                        else
                        {
                            _dbMan.SqlStatement = _dbMan.SqlStatement + " sum(p.booksqm) booksqm ";
                        }
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " from vw_Planning p  " +
                                           " " +
                                           "where p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' " +

                                           "and p.activity in(0,9) " +
                                           "group by orgunitds) a " +
                                           "order by var ";


                    }
                    else
                    {
                        _dbMan.SqlStatement = "select Top 20  '" + SysSettings.Banner + "' banner, '" + Plan + "' planchk,'" + Book + "' Bookchk, convert(varchar(50), orgunitds+ '  ') org, a.*, case when a.booksqm is null then 0-a.plansqm else a.booksqm-a.plansqm end as var from " +
                                            " (select  orgunitds, " +
                                            "sum(sqm) plansqm, ";
                        if (SysSettings.AdjBook == "Y")
                        {
                            _dbMan.SqlStatement = _dbMan.SqlStatement + " sum(p.booksqm + p.AdjSqm) booksqm ";
                        }
                        else
                        {
                            _dbMan.SqlStatement = _dbMan.SqlStatement + " sum(p.booksqm) booksqm ";
                        }
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " from vw_Planning p  " +
                                            " " +
                                            "where p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' " +

                                            "and p.activity in(1) " +
                                            "group by orgunitds) a " +
                                            "order by var ";
                    }
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "OrgUnit";
                    _dbMan.ExecuteInstruction();

                    DataSet dsOrg = new DataSet();
                    dsOrg.Tables.Add(_dbMan.ResultsDataTable);



                    theReport.RegisterData(dsOrg);

                    theReport.Load(TGlobalItems.ReportsFolder + "\\WorstPerformersOrg.frx");

                    //theReport.Design();

                    theReport.Prepare();
                    theReport.Refresh();
                    ActiveReport.SetReport = theReport;
                    ActiveReport.isDone = true;
                }
                else
                {
                    string Crew = reportSettings.Crew;

                    MWDataManager.clsDataAccess _dbManCrew = new MWDataManager.clsDataAccess();
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbManCrew.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    if (reportSettings.Activity == 0)
                    {

                        _dbManCrew.SqlStatement = "Select 'aa' aa,'" + SysSettings.Banner + "' banner ,p.WorkplaceID, SUM(Sqm) Sqm, ";
                        if (SysSettings.AdjBook == "Y")
                        {
                            _dbManCrew.SqlStatement = _dbManCrew.SqlStatement + "SUM(BookSqm)+SUM(AdjSqm)-SUM(Sqm) BookVar ";
                        }
                        else
                        {

                            _dbManCrew.SqlStatement = _dbManCrew.SqlStatement + "SUM(BookSqm-Sqm) BookVar ";
                        }

                        _dbManCrew.SqlStatement = _dbManCrew.SqlStatement + " ,OrgUnitds, " +
                                                "w.Description from vw_Planning p, Workplace w where p.OrgUnitDS = '" + Crew + "' and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and p.Activity in (0,9) " +
                                                "and p.WorkplaceID = w.WorkplaceID " +
                                                "group by p.WorkplaceID,p.Orgunitds,w.Description ";

                    }
                    else
                    {
                        _dbManCrew.SqlStatement = "Select 'aa' aa,'" + SysSettings.Banner + "' banner ,p.WorkplaceID,SUM(adv) Sqm, ";
                        if (SysSettings.AdjBook == "Y")
                        {
                            _dbManCrew.SqlStatement = _dbManCrew.SqlStatement + "SUM((Bookadv)-adv) BookVar ";
                        }
                        else
                        {

                            _dbManCrew.SqlStatement = _dbManCrew.SqlStatement + "SUM(Bookadv-adv) BookVar ";
                        }

                        _dbManCrew.SqlStatement = _dbManCrew.SqlStatement + ",OrgUnitds,w.Description from vw_Planning p, Workplace w where p.OrgUnitDS = '" + Crew + "' and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and p.Activity in (1) " +
                                                "and p.WorkplaceID = w.WorkplaceID " +
                                                "group by p.WorkplaceID,p.Orgunitds,w.Description ";
                    }


                    _dbManCrew.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManCrew.queryReturnType = MWDataManager.ReturnType.DataTable;

                    _dbManCrew.ResultsTableName = "Crew";

                    _dbManCrew.ExecuteInstruction();

                    DataSet dsCrew = new DataSet();
                    dsCrew.Tables.Add(_dbManCrew.ResultsDataTable);

                    theReport.RegisterData(dsCrew);


                    MWDataManager.clsDataAccess _dbManProb = new MWDataManager.clsDataAccess();
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbManProb.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    if (reportSettings.Activity == 0)
                    {

                        _dbManProb.SqlStatement = "select a.*, sbossnotes from (select 'a' a, '" + SysSettings.Banner + "' banner, p.CalendarDate, p.ProblemID, pb.Description, p.workplaceid, p.SBossNotes from vw_Planning p, " +
                                                "Code_Problem pb where p.ProblemID = pb.ProblemID  " +
                                                "and p.OrgUnitDS = '" + Crew + "' and p.Prodmonth =  '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and p.Activity in (0,9)) a " +
                        "";
                    }
                    else
                    {
                        _dbManProb.SqlStatement = "select a.*, sbossnotes from (select 'a' a, '" + SysSettings.Banner + "' banner, p.CalendarDate,p.ProblemID,pb.Description, p.workplaceid, p.SBossNotes from vw_Planning p, " +
                                                " Code_Problem pb where p.ProblemID = pb.ProblemID  " +
                                                "and p.OrgUnitDS = '" + Crew + "' and p.Prodmonth =  '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and p.Activity in (1)) a " +
                                                " ";
                    }


                    _dbManProb.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManProb.queryReturnType = MWDataManager.ReturnType.DataTable;

                    _dbManProb.ResultsTableName = "Prob";
                    // textBox1.Text = _dbManProb.SqlStatement;
                    _dbManProb.ExecuteInstruction();

                    DataSet dsProb = new DataSet();
                    dsProb.Tables.Add(_dbManProb.ResultsDataTable);

                    theReport.RegisterData(dsProb);

                    MWDataManager.clsDataAccess _dbManChart = new MWDataManager.clsDataAccess();
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbManChart.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    if (reportSettings.Activity == 0)
                    {

                        _dbManChart.SqlStatement = "Select 'a'a, p.PROBLEMID, COUNT(p.problemid) TotalCount " +
                                                "from vw_Planning p, Code_Problem pb where p.OrgUnitDS = '" + Crew + "'  " +
                                                "and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' " +
                                                "and p.Activity in (0,9) and p.ProblemID = pb.ProblemID " +
                                                "group by p.ProblemID having COUNT(p.problemid) > 0 order by p.ProblemID desc";
                    }
                    else
                    {
                        _dbManChart.SqlStatement = "Select 'a'a, p.PROBLEMID, COUNT(p.problemid) TotalCount " +
                                                 "from vw_Planning p, Code_Problem pb where p.OrgUnitDS = '" + Crew + "' and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and p.Activity in (1) " +
                                                 "and p.ProblemID = pb.ProblemID " +
                                                "and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  " +
                                                 "group by p.ProblemID " +
                                                 "having COUNT(p.problemid) > 0 " +
                                                 "order by p.ProblemID desc";
                    }



                    _dbManChart.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManChart.queryReturnType = MWDataManager.ReturnType.DataTable;

                    _dbManChart.ResultsTableName = "Chart";

                    _dbManChart.ExecuteInstruction();

                    DataSet dsChart = new DataSet();
                    dsChart.Tables.Add(_dbManChart.ResultsDataTable);

                    theReport.RegisterData(dsChart);

                    theReport.Load(TGlobalItems.ReportsFolder + "\\WorstPerformersCrew.frx");

                   // theReport.Design();

                    theReport.Prepare();
                    theReport.Refresh();
                    ActiveReport.SetReport = theReport;
                    ActiveReport.isDone = true;
                }
            }
            else
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                if (reportSettings.Activity == 0)
                {

                    _dbMan.SqlStatement = "select Top 20 '" + SysSettings.Banner + "' banner, '" + Plan + "' planchk, '" + Book + "' Bookchk, convert(varchar(50), orgunitds+ '  ') org, a.*, case when a.booksqm is null then 0-a.plansqm else a.booksqm-a.plansqm end as var from " +
                                        "(select  orgunitds, " +
                                        "sum(sqm) plansqm, ";
                    if (SysSettings.AdjBook == "Y")
                    {
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " sum(p.booksqm + p.AdjSqm) booksqm ";
                    }
                    else
                    {
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " sum(p.booksqm) booksqm ";
                    }
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " from vw_Planning p  " +
                                       " " +
                                       "where p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' " +

                                       "and p.activity in(0,9) " +
                                       "group by orgunitds) a " +
                                       "order by var ";


                }
                else
                {
                    _dbMan.SqlStatement = "select Top 20  '" + SysSettings.Banner + "' banner, '" + Plan + "' planchk,'" + Book + "' Bookchk, convert(varchar(50), orgunitds+ '  ') org, a.*, case when a.booksqm is null then 0-a.plansqm else a.booksqm-a.plansqm end as var from " +
                                        " (select  orgunitds, " +
                                        "sum(sqm) plansqm, ";
                    if (SysSettings.AdjBook == "Y")
                    {
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " sum(p.booksqm + p.AdjSqm) booksqm ";
                    }
                    else
                    {
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " sum(p.booksqm) booksqm ";
                    }
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " from vw_Planning p  " +
                                        " " +
                                        "where p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' " +

                                        "and p.activity in(1) " +
                                        "group by orgunitds) a " +
                                        "order by var ";
                }
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "OrgUnit";
                _dbMan.ExecuteInstruction();

                DataSet dsOrg = new DataSet();
                dsOrg.Tables.Add(_dbMan.ResultsDataTable);



                theReport.RegisterData(dsOrg);


                ////MO DATA

                ////////////////////////////////////////MO Chart///////////////////////////
                MWDataManager.clsDataAccess _dbManMO = new MWDataManager.clsDataAccess();
                //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                _dbManMO.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                if (reportSettings.Activity == 0)
                {

                    _dbManMO.SqlStatement = "select '" + SysSettings.Banner + "' banner, a.*, a.booksqm-a.plansqm var from (select  s2.sectionid sec, s2.name mo, " +
                                            " sum(sqm) plansqm, ";
                    if (SysSettings.AdjBook == "Y")
                    {
                        _dbManMO.SqlStatement = _dbManMO.SqlStatement + " sum(p.booksqm + p.AdjSqm) booksqm ";
                    }
                    else
                    {
                        _dbManMO.SqlStatement = _dbManMO.SqlStatement + " sum(p.booksqm) booksqm ";
                    }
                    _dbManMO.SqlStatement = _dbManMO.SqlStatement + " from vw_Planning p  " +
                    ", Section s, Section s1, Section s2 where p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' " +
                                                " and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and s.reporttosectionid = s1.sectionid " +
                                                "and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +
                                                "and activity in (0,9) group by s2.sectionid, s2.name) a  " +

                                                //  "(select  s2.name mo, sum(squaremetres) booksqm from booking p, tbl_Section s, tbl_Section s1, tbl_Section s2 " +
                                                //    "where p.calendardate <= '" + EndDate.Value.ToShortDateString() + "' and p.calendardate >= '" + StartDate.Value.ToShortDateString() + "' and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth " +
                                                //   "and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid " +
                                                //    "and s1.prodmonth = s2.prodmonth and activity = 0 group by s2.name) b on a.mo = b.mo " +
                                                "order by var ";

                }
                else
                {
                    _dbManMO.SqlStatement = "select '" + SysSettings.Banner + "' banner, a.*, a.booksqm-a.plansqm var from (select  s2.sectionid sec, s2.name mo, " +
                                            " sum(adv) plansqm, ";
                    if (SysSettings.AdjBook == "Y")
                    {
                        _dbManMO.SqlStatement = _dbManMO.SqlStatement + " sum(p.bookadv) booksqm ";
                    }
                    else
                    {
                        _dbManMO.SqlStatement = _dbManMO.SqlStatement + " sum(p.booksqm) booksqm ";
                    }
                    _dbManMO.SqlStatement = _dbManMO.SqlStatement + " from vw_Planning p , Section s, Section s1, Section s2 where p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' " +
                                                " and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and s.reporttosectionid = s1.sectionid " +
                                                "and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +
                                                "and activity in (1) group by s2.sectionid, s2.name) a  " +

                                                // "(select  s2.name mo, sum(squaremetres) booksqm from booking p, tbl_Section s, tbl_Section s1, tbl_Section s2 " +
                                                //  "where p.calendardate <= '" + EndDate.Value.ToShortDateString() + "' and p.calendardate >= '" + StartDate.Value.ToShortDateString() + "' and p.sectionid = s.sectionid and p.prodmonth = s.prodmonth " +
                                                //  "and s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid " +
                                                //  "and s1.prodmonth = s2.prodmonth and activity = 0 group by s2.name) b on a.mo = b.mo " +


                                                "order by var ";
                }
                _dbManMO.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMO.queryReturnType = MWDataManager.ReturnType.DataTable;

                _dbManMO.ResultsTableName = "MO";

                _dbManMO.ExecuteInstruction();

                DataSet dsMO = new DataSet();
                dsMO.Tables.Add(_dbManMO.ResultsDataTable);

                theReport.RegisterData(dsMO);


                MWDataManager.clsDataAccess _dbManSD = new MWDataManager.clsDataAccess();
                //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                _dbManSD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _dbManSD.SqlStatement = "select Top 12 convert(varchar(50),a.prodmonth) pm, a.*, case when b.booksqm is null then 0 else b.booksqm end as booksqm " +
                                       ",case when c.meassqm is null then 0 else c.meassqm end as meassqm, " +
                                      " case when c.meassqm is null then 0 else c.meassqm-a.plansqm end as var " +
                                         "from ( " +
                                      " select p.prodmonth, sum(p.sqmtotal) plansqm from vw_PlanMonth p, Section s, Section s1, Section s2 where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and s.reporttosectionid = s1.sectionid " +
                                       "and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +
                                       "and activity in(0,9) " +
                                       "group by p.prodmonth) a " +
                                       "left outer join " +
                                       "(select p.prodmonth, ";
                if (SysSettings.AdjBook == "Y")
                {
                    _dbManSD.SqlStatement = _dbManSD.SqlStatement + "sum(p.booksqm + AdjSqm) booksqm";
                }
                else
                {
                    _dbManSD.SqlStatement = _dbManSD.SqlStatement + " sum(p.booksqm) booksqm ";
                }

                _dbManSD.SqlStatement = _dbManSD.SqlStatement + " from vw_Planning p, Section s, Section s1, Section s2 where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and s.reporttosectionid = s1.sectionid " +
                "and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +
                "and activity in(0,9)  " +
                "group by p.prodmonth) b on " +
                "a.prodmonth = b.prodmonth " +
                "left outer join " +
                "(select p.prodmonth, sum(p.sqmtotal) meassqm from survey p, Section s, Section s1, Section s2 where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and s.reporttosectionid = s1.sectionid " +
                "and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +
                "and activity in (0,9) " +
               " group by p.prodmonth) c on " +
                "a.prodmonth = c.prodmonth " +
                "order by a.prodmonth desc ";


                _dbManSD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSD.queryReturnType = MWDataManager.ReturnType.DataTable;

                _dbManSD.ResultsTableName = "Stoping";
                _dbManSD.ExecuteInstruction();

                DataSet dsStoping = new DataSet();
                dsStoping.Tables.Add(_dbManSD.ResultsDataTable);

                theReport.RegisterData(dsStoping);
                dtStoping = _dbManSD.ResultsDataTable;



                ////////////////Development//////////////////
                MWDataManager.clsDataAccess _dbManDev = new MWDataManager.clsDataAccess();
                //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                _dbManDev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _dbManDev.SqlStatement = "select Top 12 a.*, convert(varchar(50),a.prodmonth) pp,'" + reportSettings.SectionID + "'  Section, case when b.booksqm is null then 0 else b.booksqm end as booksqm " +
                                        ",case when c.meassqm is null then 0 else c.meassqm end as meassqm, " +
                                        "case when c.meassqm is null then 0 else c.meassqm-a.plansqm end as var " +
                                        "    from ( " +
                                        "select p.prodmonth, sum(p.adv) plansqm from vw_PlanMonth p, Section s, Section s1, Section s2 where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and s.reporttosectionid = s1.sectionid " +
                                        "and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +
                                        "and activity = 1  " +
                                        "group by p.prodmonth) a " +
                                        "left outer join " +
                                        "(select p.prodmonth, sum(p.bookadv) booksqm from vw_Planning p, Section s, Section s1, Section s2 where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and s.reporttosectionid = s1.sectionid " +
                                        "and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +
                                        "and activity = 1  " +
                                        "group by p.prodmonth) b on " +
                                        "a.prodmonth = b.prodmonth " +
                                        "left outer join " +
                                        "(select p.prodmonth, sum(p.ReefMetres+WasteMetres) meassqm from survey p, Section s, Section s1, Section s2 where p.sectionid = s.sectionid and p.prodmonth = s.prodmonth and s.reporttosectionid = s1.sectionid " +
                                        "and s.prodmonth = s1.prodmonth and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth " +
                                        "and activity = 1  " +
                                        "group by p.prodmonth) c on " +
                                        "a.prodmonth = c.prodmonth " +
                                        "order by a.prodmonth desc";


                _dbManDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDev.ResultsTableName = "Development";

                _dbManDev.ExecuteInstruction();

                DataSet dsDev = new DataSet();
                dsDev.Tables.Add(_dbManDev.ResultsDataTable);

                theReport.RegisterData(dsDev);

                _dbManDev.ExecuteInstruction();
                dtDev = _dbManDev.ResultsDataTable;



               

                theReport.Load(TGlobalItems.ReportsFolder + "\\WorstPerformers.frx");

                // theReport.Design();

                theReport.Prepare();
                theReport.Refresh();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;

            }
           
        }

        private void pgTopPanelsRepSettings_Click(object sender, EventArgs e)
        {
            string aa = "";

          
        }

        private void pgTopPanelsRepSettings_MouseDown(object sender, MouseEventArgs e)
        {
            if (reportSettings.Type == "MO Summary")
            {
                iCrew.Visible = false;
                //iSection.Visible = true;
            }
            else
            {
                iCrew.Visible = true;
                //iSection.Visible = false;
            }
        }
    }
}
