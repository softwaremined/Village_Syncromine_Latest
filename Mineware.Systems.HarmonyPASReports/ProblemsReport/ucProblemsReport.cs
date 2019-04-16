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

namespace Mineware.Systems.Reports.ProblemsReport
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

            //setReportSetttings();
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
            Report theReport3 = new Report();
            //Report theReport = new Report();
            //DataSet repDataSet = new DataSet();

            //MWDataManager.clsDataAccess _ProblemsReportData = new MWDataManager.clsDataAccess();
            //try
            //{
            //    _ProblemsReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _ProblemsReportData.SqlStatement = "sp_ProblemsReport_Detail";
            //    _ProblemsReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _ProblemsReportData.ResultsTableName = "Detail_Problem_Data";
            //    SqlParameter[] _paramCollection =
            //        {
            //            _ProblemsReportData.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ReportDate),
            //            _ProblemsReportData.CreateParameter("@Activity", SqlDbType.VarChar, 20, reportSettings.Activity),
            //            _ProblemsReportData.CreateParameter("@Level", SqlDbType.VarChar, 20, reportSettings.TheType),
            //        };
            //    _ProblemsReportData.ParamCollection = _paramCollection;
            //    _ProblemsReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    clsDataResult errorData = _ProblemsReportData.ExecuteInstruction();
            //    if (_ProblemsReportData.ResultsDataTable == null)
            //    {
            //        //_ProblemsHeadingData.SqlStatement = "sp_ProblemsReport_Zeroes";
            //        //_ProblemsHeadingData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //        //_ProblemsHeadingData.ResultsTableName = "Week_Problem_Data";
            //        //_ProblemsHeadingData.ParamCollection = null;
            //        //_ProblemsHeadingData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //        //clsDataResult errorWeekNull = _ProblemsHeadingData.ExecuteInstruction();
            //    }
            //    repDataSet.Tables.Add(_ProblemsReportData.ResultsDataTable);
            //}
            //catch (Exception _exception)
            //{
            //    throw new ApplicationException("Report Section:_ProblemsReportData:" + _exception.Message, _exception);
            //}


            //MWDataManager.clsDataAccess _ProblemsHeadingData = new MWDataManager.clsDataAccess();
            //try
            //{
            //    _ProblemsHeadingData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _ProblemsHeadingData.SqlStatement = "sp_ProblemsReport_Headings";
            //    _ProblemsHeadingData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _ProblemsHeadingData.ResultsTableName = "Problem_Report_Headings";
            //    SqlParameter[] _paramCollection7 =
            //    {
            //        _ProblemsHeadingData.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ReportDate),
            //    };
            //    _ProblemsHeadingData.ParamCollection = _paramCollection7;
            //    _ProblemsHeadingData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    clsDataResult errorHead = _ProblemsHeadingData.ExecuteInstruction();
            //    if (_ProblemsHeadingData.ResultsDataTable == null)
            //    {
            //        //_ProblemsHeadingData.SqlStatement = "sp_ProblemsReport_Zeroes";
            //        //_ProblemsHeadingData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //        //_ProblemsHeadingData.ResultsTableName = "Week_Problem_Data";
            //        //_ProblemsHeadingData.ParamCollection = null;
            //        //_ProblemsHeadingData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //        //clsDataResult errorWeekNull = _ProblemsHeadingData.ExecuteInstruction();
            //    }
            //    repDataSet.Tables.Add(_ProblemsHeadingData.ResultsDataTable);
            //}
            //catch (Exception _exception)
            //{
            //    throw new ApplicationException("Report Section:_ProblemsHeadingData:" + _exception.Message, _exception);
            //}

            //if (reportSettings.IncludeGraphs == true)
            //{
            //    MWDataManager.clsDataAccess _ProblemsWeekReportData = new MWDataManager.clsDataAccess();
            //    _ProblemsWeekReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _ProblemsWeekReportData.SqlStatement = "sp_ProblemsReport_Week";
            //    _ProblemsWeekReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _ProblemsWeekReportData.ResultsTableName = "Week_Problem_Data";
            //    SqlParameter[] _paramCollection1 =
            //        {
            //            _ProblemsWeekReportData.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ReportDate),
            //            _ProblemsWeekReportData.CreateParameter("@Activity", SqlDbType.VarChar, 20, reportSettings.Activity),
            //            _ProblemsWeekReportData.CreateParameter("@Level", SqlDbType.VarChar, 20, reportSettings.TheType),
            //        };
            //    _ProblemsWeekReportData.ParamCollection = _paramCollection1;
            //    _ProblemsWeekReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    clsDataResult errorMsgWeek = _ProblemsWeekReportData.ExecuteInstruction();
            //    if (_ProblemsWeekReportData.ResultsDataTable == null)
            //    {
            //        _ProblemsWeekReportData.SqlStatement = "sp_ProblemsReport_Zeroes";
            //        _ProblemsWeekReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //        _ProblemsWeekReportData.ResultsTableName = "Week_Problem_Data";
            //        _ProblemsWeekReportData.ParamCollection = null;
            //        _ProblemsWeekReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //        clsDataResult errorWeekNull = _ProblemsWeekReportData.ExecuteInstruction();
            //    }
            //    repDataSet.Tables.Add(_ProblemsWeekReportData.ResultsDataTable);

            //    MWDataManager.clsDataAccess _ProblemsMonthReportData = new MWDataManager.clsDataAccess();
            //    _ProblemsMonthReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _ProblemsMonthReportData.SqlStatement = "sp_ProblemsReport_Month";
            //    _ProblemsMonthReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _ProblemsMonthReportData.ResultsTableName = "Month_Problem_Data";
            //    SqlParameter[] _paramCollection2 =
            //        {
            //            _ProblemsMonthReportData.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ReportDate),
            //            _ProblemsMonthReportData.CreateParameter("@Activity", SqlDbType.VarChar, 20, reportSettings.Activity),
            //            _ProblemsMonthReportData.CreateParameter("@Level", SqlDbType.VarChar, 20, reportSettings.TheType),
            //        };
            //    _ProblemsMonthReportData.ParamCollection = _paramCollection2;
            //    _ProblemsMonthReportData.queryReturnType = MWDataManager.ReturnType.DataTable;

            //    clsDataResult errorMsgMonth = _ProblemsMonthReportData.ExecuteInstruction();
            //    if (_ProblemsMonthReportData.ResultsDataTable == null)
            //    {
            //        _ProblemsMonthReportData.SqlStatement = "sp_ProblemsReport_Zeroes";
            //        _ProblemsMonthReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //        _ProblemsMonthReportData.ResultsTableName = "Month_Problem_Data";
            //        _ProblemsMonthReportData.ParamCollection = null;
            //        _ProblemsMonthReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //        clsDataResult errorMonthNull = _ProblemsMonthReportData.ExecuteInstruction();
            //    }
            //    repDataSet.Tables.Add(_ProblemsMonthReportData.ResultsDataTable);

            //    MWDataManager.clsDataAccess _ProblemsYearReportData = new MWDataManager.clsDataAccess();
            //    _ProblemsYearReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _ProblemsYearReportData.SqlStatement = "sp_ProblemsReport_FinYear";
            //    _ProblemsYearReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _ProblemsYearReportData.ResultsTableName = "FinYear_Problem_Data";
            //    SqlParameter[] _paramCollection3 =
            //        {
            //            _ProblemsYearReportData.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ReportDate),
            //            _ProblemsYearReportData.CreateParameter("@Activity", SqlDbType.VarChar, 20, reportSettings.Activity),
            //            _ProblemsYearReportData.CreateParameter("@Level", SqlDbType.VarChar, 20, reportSettings.TheType),
            //        };
            //    _ProblemsYearReportData.ParamCollection = _paramCollection3;
            //    _ProblemsYearReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    clsDataResult errorMsgYear = _ProblemsYearReportData.ExecuteInstruction();
            //    if (_ProblemsYearReportData.ResultsDataTable == null)
            //    {
            //        _ProblemsYearReportData.SqlStatement = "sp_ProblemsReport_Zeroes";
            //        _ProblemsYearReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //        _ProblemsYearReportData.ResultsTableName = "FinYear_Problem_Data";
            //        _ProblemsYearReportData.ParamCollection = null;
            //        _ProblemsYearReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //        clsDataResult errorYearNull = _ProblemsYearReportData.ExecuteInstruction();
            //    }
            //    repDataSet.Tables.Add(_ProblemsYearReportData.ResultsDataTable);

            //    MWDataManager.clsDataAccess _ProblemsWeekPieReportData = new MWDataManager.clsDataAccess();
            //    _ProblemsWeekPieReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _ProblemsWeekPieReportData.SqlStatement = "sp_ProblemsReport_WeekPie";
            //    _ProblemsWeekPieReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _ProblemsWeekPieReportData.ResultsTableName = "Week_Problems_Data_Pie";
            //    SqlParameter[] _paramCollection4 =
            //        {
            //            _ProblemsWeekPieReportData.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ReportDate),
            //            _ProblemsWeekPieReportData.CreateParameter("@Activity", SqlDbType.VarChar, 20, reportSettings.Activity),
            //            _ProblemsWeekPieReportData.CreateParameter("@Level", SqlDbType.VarChar, 20, reportSettings.TheType),
            //        };
            //    _ProblemsWeekPieReportData.ParamCollection = _paramCollection4;
            //    _ProblemsWeekPieReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    clsDataResult errorMsgWeekPie = _ProblemsWeekPieReportData.ExecuteInstruction();
            //    if (_ProblemsWeekPieReportData.ResultsDataTable == null)
            //    {
            //        _ProblemsWeekPieReportData.SqlStatement = "sp_ProblemsReport_ZeroesPie";
            //        _ProblemsWeekPieReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //        _ProblemsWeekPieReportData.ResultsTableName = "Week_Problems_Data_Pie";
            //        _ProblemsWeekPieReportData.ParamCollection = null;
            //        _ProblemsWeekPieReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //        clsDataResult errorWeekPieNull = _ProblemsWeekPieReportData.ExecuteInstruction();
            //    }
            //    repDataSet.Tables.Add(_ProblemsWeekPieReportData.ResultsDataTable);

            //    MWDataManager.clsDataAccess _ProblemsMonthPieReportData = new MWDataManager.clsDataAccess();
            //    _ProblemsMonthPieReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _ProblemsMonthPieReportData.SqlStatement = "sp_ProblemsReport_MonthPie";
            //    _ProblemsMonthPieReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _ProblemsMonthPieReportData.ResultsTableName = "Month_Problems_Data_Pie";
            //    SqlParameter[] _paramCollection5 =
            //        {
            //            _ProblemsMonthPieReportData.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ReportDate),
            //            _ProblemsMonthPieReportData.CreateParameter("@Activity", SqlDbType.VarChar, 20, reportSettings.Activity),
            //            _ProblemsMonthPieReportData.CreateParameter("@Level", SqlDbType.VarChar, 20, reportSettings.TheType),
            //        };
            //    _ProblemsMonthPieReportData.ParamCollection = _paramCollection5;
            //    _ProblemsMonthPieReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    clsDataResult errorMsgMonthPie = _ProblemsMonthPieReportData.ExecuteInstruction();
            //    if (_ProblemsMonthPieReportData.ResultsDataTable == null)
            //    {
            //        _ProblemsMonthPieReportData.SqlStatement = "sp_ProblemsReport_ZeroesPie";
            //        _ProblemsMonthPieReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //        _ProblemsMonthPieReportData.ResultsTableName = "Month_Problems_Data_Pie";
            //        _ProblemsMonthPieReportData.ParamCollection = null;
            //        _ProblemsMonthPieReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //        clsDataResult errorMonthPieNull = _ProblemsMonthPieReportData.ExecuteInstruction();
            //    }
            //    repDataSet.Tables.Add(_ProblemsMonthPieReportData.ResultsDataTable);

            //    MWDataManager.clsDataAccess _ProblemsYearPieReportData = new MWDataManager.clsDataAccess();
            //    _ProblemsYearPieReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _ProblemsYearPieReportData.SqlStatement = "sp_ProblemsReport_FinYearPie";
            //    _ProblemsYearPieReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _ProblemsYearPieReportData.ResultsTableName = "FinYear_Problems_Data_Pie";
            //    SqlParameter[] _paramCollection6 =
            //        {
            //            _ProblemsYearPieReportData.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ReportDate),
            //            _ProblemsYearPieReportData.CreateParameter("@Activity", SqlDbType.VarChar, 20, reportSettings.Activity),
            //            _ProblemsYearPieReportData.CreateParameter("@Level", SqlDbType.VarChar, 20, reportSettings.TheType),
            //        };
            //    _ProblemsYearPieReportData.ParamCollection = _paramCollection6;
            //    _ProblemsYearPieReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    clsDataResult errorMsgYearPie = _ProblemsYearPieReportData.ExecuteInstruction();
            //    if (_ProblemsYearPieReportData.ResultsDataTable == null)
            //    {
            //        _ProblemsYearPieReportData.SqlStatement = "sp_ProblemsReport_ZeroesPie";
            //        _ProblemsYearPieReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //        _ProblemsYearPieReportData.ResultsTableName = "FinYear_Problems_Data_Pie";
            //        _ProblemsYearPieReportData.ParamCollection = null;
            //        _ProblemsYearPieReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //        clsDataResult errorYearPieNull = _ProblemsYearPieReportData.ExecuteInstruction();
            //    }
            //    repDataSet.Tables.Add(_ProblemsYearPieReportData.ResultsDataTable);
            //}

            //theReport.RegisterData(repDataSet);

            //if (reportSettings.IncludeGraphs == true)
            //    theReport.Load(TGlobalItems.ReportsFolder + "\\WeeklyProblemsReport.frx");
            //else
            //    theReport.Load(TGlobalItems.ReportsFolder + "\\WeeklyProblemsReport_NoGraphs.frx");
            //theReport.SetParameterValue("Banner", Banner);
            //theReport.SetParameterValue("theDate", reportSettings.ReportDate.ToShortDateString());
            //theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
            //theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);

            //if (TParameters.DesignReport)
            //{
            //    theReport.Design();
            //}
            //theReport.Prepare();
            //ActiveReport.SetReport = theReport;
            //ActiveReport.isDone = true;


            /////////////////////////////////////////////////////
            //////////////Problem History Pareto//////////////////
            MWDataManager.clsDataAccess _dbManGraph = new MWDataManager.clsDataAccess();
            _dbManGraph.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);          
            _dbManGraph.SqlStatement = "exec Proc_LostBlast @ProdMonth = '"+  ProdMonthTxt.Text + "'";
            _dbManGraph.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManGraph.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManGraph.ResultsTableName = "GraphCombo";
            //textBox1.Text = _dbManGraph.SqlStatement;
            _dbManGraph.ExecuteInstruction();

            DataSet dsGraphCombo = new DataSet();

            dsGraphCombo.Tables.Add(_dbManGraph.ResultsDataTable);

            theReport3.RegisterData(dsGraphCombo);


            MWDataManager.clsDataAccess _dbManMO = new MWDataManager.clsDataAccess();
            _dbManMO.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManMO.SqlStatement = "select '" + SysSettings.Banner + "' banner,'" +  ProdMonthTxt.Text + "' Prodmonth, moid,MOName ,convert(integer,Sum(Tons)) Tons,COUNT(problemid) Problem  from lostblast  where Prodmonth = '"+ ProdMonthTxt.Text+"' " +
                                    "group by MOID,MOName " +
                                    "order by MOID";
            _dbManMO.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManMO.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManMO.ResultsTableName = "MO";
            //textBox1.Text = _dbManGraph.SqlStatement;
            _dbManMO.ExecuteInstruction();
            DataSet dsMO = new DataSet();

            dsMO.Tables.Add(_dbManMO.ResultsDataTable);

            theReport3.RegisterData(dsMO);


            ////MessageBox.Show(GraphType);
           // theReport3.Load("ProblemHistoryParito.frx");
            theReport3.Load(TGlobalItems.ReportsFolder + "\\ProblemHistoryParito.frx");
            // theReport3.Design();

            theReport3.Prepare();
            ActiveReport.SetReport = theReport3;
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
