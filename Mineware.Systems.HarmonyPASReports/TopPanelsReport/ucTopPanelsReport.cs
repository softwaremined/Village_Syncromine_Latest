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

namespace Mineware.Systems.Reports.TopPanelsReport
{
    public partial class ucTopPanelsReport : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        Report Top20Graph = new Report();
        Procedures procs = new Procedures();

        string theSystemDBTag = "DBHARMONYPAS";
        private clsTopPanelsReportSettings  reportSettings = new clsTopPanelsReportSettings();
        DataTable dtSections = new DataTable();
        Report theReport = new Report();
        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        public ucTopPanelsReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void LoadSections()
        {
           

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "  \r\n";

            _dbMan.SqlStatement += "  Select SectionID+':' + Name SectionID, 0 orderby  \r\n";
            _dbMan.SqlStatement += "  from Section s where s.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  \r\n";
            _dbMan.SqlStatement += "  and Hierarchicalid = 1  \r\n";
            _dbMan.SqlStatement += "  union  \r\n";
            _dbMan.SqlStatement += "  Select SectionID + ':' + Name SectionID, 1 orderby  \r\n";
            _dbMan.SqlStatement += "     from Section s where s.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  \r\n";
            _dbMan.SqlStatement += "  and Hierarchicalid = 4  \r\n";

            _dbMan.SqlStatement += "  ";
            _dbMan.SqlStatement += "   ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            dtSections = _dbMan.ResultsDataTable;
            riSection.DataSource = dtSections;
            riSection.DisplayMember = "SectionID";
            riSection.ValueMember = "SectionID";
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

        private void createReport(Object theReportSettings)
        {
            //Procedures pProc = new Procedures();
            //DataSet dsTopPanels = new DataSet();
            //StringBuilder sb = new StringBuilder();
            //FastReport.Report theReport = new FastReport.Report();
            //Procedures procs = new Procedures();

            //int nProdMonth = Convert .ToInt32 (TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            //string strSectionID = procs.ExtractBeforeColon(reportSettings.SectionID);
            //int nSection = 1;

            //if (pProc.ExtractBeforeColon(reportSettings.SectionID) != "1")
            //    nSection = 4;

            //#region Database Call

            //DateTime? dtStart = null;
            //DateTime? dtEnd = null;

            //#region Get Start & End Date

            //if (!dtStart.HasValue && !dtEnd.HasValue)
            //{

            //    MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            //    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            //    _dbMan1.SqlStatement = "select MIN(BeginDate) as 'StartDate', MAX(EndDate) as 'EndDate' from SECCAL where Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'";
            //    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    _dbMan1.ExecuteInstruction();

            //    if (_dbMan1.ResultsDataTable.Rows.Count != 0)
            //    {
            //        dtStart = Convert.ToDateTime(_dbMan1.ResultsDataTable.Rows[0]["StartDate"]);
            //        dtEnd = Convert.ToDateTime(_dbMan1.ResultsDataTable.Rows[0]["EndDate"]);
            //    }
            //}

            //#endregion

            //DateTime dtCurrent = dtStart.Value;
            //MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //SqlConnection pConn = new SqlConnection(_dbMan.ConnectionString);
            //SqlCommand pCmd = new SqlCommand();
            //pCmd.CommandType = CommandType.Text;
            //pCmd.Connection = pConn;

            //#region Top Panels

            //sb.AppendLine();
            //sb.AppendLine("-- Top Panels");
            //sb.AppendLine(string.Format("exec Report_TopPanels '{0}', '{1}', {2}", nProdMonth, strSectionID, nSection));

            //#endregion


            //pCmd.CommandText = sb.ToString();

            //pConn.Open();
            //SqlDataAdapter pAdap = new SqlDataAdapter(pCmd);
            //pAdap.Fill(dsTopPanels);
            //pConn.Close();

            //if (dsTopPanels.Tables.Count == 0)
            //{
            //    MessageBox.Show("No Top Panels found for selected search criteria. Please try again.", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    // return;
            //    theReport.Prepare();
            //    theReport.Refresh();
            //    ActiveReport.SetReport = theReport;
            //    ActiveReport.isDone = true;
            //    return;
            //}
            //else
            //{
            //    if (dsTopPanels.Tables[0].Rows.Count == 0)
            //    {
            //        MessageBox.Show("No Top Panels found for selected search criteria. Please try again.", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        // return;
            //        theReport.Prepare();
            //        theReport.Refresh();
            //        ActiveReport.SetReport = theReport;
            //        ActiveReport.isDone = true;
            //        return;
            //    }
            //}

            //dsTopPanels.Tables[0].TableName = "TopPanels";

            //#endregion

            //#region Totals

            //DataTable tblTotals = new DataTable("Totals");


            //tblTotals.Columns.Add("MonthlyPlanningSQM");
            //tblTotals.Columns.Add("MonthlyPlanningCMGT");
            //tblTotals.Columns.Add("MonthlyPlanningGT");
            //tblTotals.Columns.Add("MonthlyPlanningKGs");
            //tblTotals.Columns.Add("ProgressGradeActualCMGT");
            //tblTotals.Columns.Add("ProgressGradeActualGT");
            //tblTotals.Columns.Add("ProgressGradeVarCMGT");
            //tblTotals.Columns.Add("ProgressGradeVarGT");
            //tblTotals.Columns.Add("ProgressivePlannedSQM");
            //tblTotals.Columns.Add("ProgressiveActualSQM");
            //tblTotals.Columns.Add("ProgressiveVarSQM");
            //tblTotals.Columns.Add("ProgressivePlannedKG");
            //tblTotals.Columns.Add("ProgressiveActualKG");
            //tblTotals.Columns.Add("ProgressiveVarKG");
            //tblTotals.Columns.Add("GradePerformance");
            //tblTotals.Columns.Add("SQMPerformance");
            //tblTotals.Columns.Add("KGPerformance");


            //decimal fMonthlyPlanningSQM = 0.0m;
            //decimal fMonthlyPlanningCMGT = 0.0m;
            //decimal fMonthlyPlanningGT = 0.0m;
            //decimal fMonthlyPlanningKGs = 0.0m;
            //decimal fProgressGradeActualCMGT = 0.0m;
            //decimal fProgressGradeActualGT = 0.0m;
            //decimal fProgressGradeVarCMGT = 0.0m;
            //decimal fProgressGradeVarGT = 0.0m;
            //decimal fProgressivePlannedSQM = 0.0m;
            //decimal fProgressiveActualSQM = 0.0m;
            //decimal fProgressiveVarSQM = 0.0m;
            //decimal fProgressivePlannedKG = 0.0m;
            //decimal fProgressiveActualKG = 0.0m;
            //decimal fProgressiveVarKG = 0.0m;

            //decimal fMonthlyPlanningSQMSUM = 0.0m;
            //decimal fMonthlyPlanningCMGTSUM = 0.0m;
            //decimal fMonthlyPlanningGTSUM = 0.0m;
            //decimal fMonthlyPlanningKGsSUM = 0.0m;
            //decimal fProgressGradeActualCMGTSUM = 0.0m;
            //decimal fProgressGradeActualGTSUM = 0.0m;
            //decimal fProgressGradeVarCMGTSUM = 0.0m;
            //decimal fProgressGradeVarGTSUM = 0.0m;
            //decimal fProgressivePlannedSQMSUM = 0.0m;
            //decimal fProgressiveActualSQMSUM = 0.0m;
            //decimal fProgressiveVarSQMSUM = 0.0m;
            //decimal fProgressivePlannedKGSUM = 0.0m;
            //decimal fProgressiveActualKGSUM = 0.0m;
            //decimal fProgressiveVarKGSUM = 0.0m;

            //foreach (DataRow dr in dsTopPanels.Tables["TopPanels"].Rows)
            //{
            //    #region Sum

            //    if (dr["MonthlyPlanningSQM"] != DBNull.Value)
            //        fMonthlyPlanningSQMSUM += Convert.ToDecimal(dr["MonthlyPlanningSQM"]);

            //    if (dr["MonthlyPlanningCMGT"] != DBNull.Value)
            //        fMonthlyPlanningCMGTSUM += Convert.ToDecimal(dr["MonthlyPlanningCMGT"]);
            //    if (dr["MonthlyPlanningGT"] != DBNull.Value)
            //        fMonthlyPlanningGTSUM += Convert.ToDecimal(dr["MonthlyPlanningGT"]);

            //    if (dr["MonthlyPlanningKGs"] != DBNull.Value)
            //        fMonthlyPlanningKGsSUM += Convert.ToDecimal(dr["MonthlyPlanningKGs"]);

            //    if (dr["ProgressGradeActualCMGT"] != DBNull.Value)
            //        fProgressGradeActualCMGTSUM += Convert.ToDecimal(dr["ProgressGradeActualCMGT"]);
            //    if (dr["ProgressGradeActualGT"] != DBNull.Value)
            //        fProgressGradeActualGTSUM += Convert.ToDecimal(dr["ProgressGradeActualGT"]);
            //    if (dr["ProgressGradeVarCMGT"] != DBNull.Value)
            //        fProgressGradeVarCMGTSUM += Convert.ToDecimal(dr["ProgressGradeVarCMGT"]);
            //    if (dr["ProgressGradeVarGT"] != DBNull.Value)
            //        fProgressGradeVarGTSUM += Convert.ToDecimal(dr["ProgressGradeVarGT"]);

            //    if (dr["ProgressivePlannedSQM"] != DBNull.Value)
            //        fProgressivePlannedSQMSUM += Convert.ToDecimal(dr["ProgressivePlannedSQM"]);
            //    if (dr["ProgressiveActualSQM"] != DBNull.Value)
            //        fProgressiveActualSQMSUM += Convert.ToDecimal(dr["ProgressiveActualSQM"]);
            //    if (dr["ProgressiveVarSQM"] != DBNull.Value)
            //        fProgressiveVarSQMSUM += Convert.ToDecimal(dr["ProgressiveVarSQM"]);

            //    if (dr["ProgressivePlannedKG"] != DBNull.Value)
            //        fProgressivePlannedKGSUM += Convert.ToDecimal(dr["ProgressivePlannedKG"]);
            //    if (dr["ProgressiveActualKG"] != DBNull.Value)
            //        fProgressiveActualKGSUM += Convert.ToDecimal(dr["ProgressiveActualKG"]);
            //    if (dr["ProgressiveVarKG"] != DBNull.Value)
            //        fProgressiveVarKGSUM += Convert.ToDecimal(dr["ProgressiveVarKG"]);

            //    #endregion

            //    if (dr["MonthlyPlanningSQM"] != DBNull.Value)
            //        fMonthlyPlanningSQM += Convert.ToDecimal(dr["MonthlyPlanningSQM"]);

            //    if (dr["MonthlyPlanningCMGT"] != DBNull.Value && dr["ProgressivePlannedSQM"] != DBNull.Value)
            //        fMonthlyPlanningCMGT += Convert.ToDecimal(dr["MonthlyPlanningCMGT"]) * Convert.ToDecimal(dr["ProgressivePlannedSQM"]);


            //    if (dr["MonthlyPlanningGT"] != DBNull.Value && dr["ProgressivePlannedSQM"] != DBNull.Value)
            //        fMonthlyPlanningGT += Convert.ToDecimal(dr["MonthlyPlanningGT"]) * Convert.ToDecimal(dr["ProgressivePlannedSQM"]);

            //    if (dr["MonthlyPlanningKGs"] != DBNull.Value)
            //        fMonthlyPlanningKGs += Convert.ToDecimal(dr["MonthlyPlanningKGs"]);

            //    if (dr["ProgressGradeActualGT"] != DBNull.Value
            //        && dr["ProgressiveActualSQM"] != DBNull.Value)
            //        fProgressGradeActualGT += Convert.ToDecimal(dr["ProgressGradeActualGT"]) * Convert.ToDecimal(dr["ProgressiveActualSQM"]);
            //    //fProgressGradeVarCMGT += Convert.ToDecimal(dr["ProgressGradeVarCMGT"]);
            //    //fProgressGradeVarGT += Convert.ToDecimal(dr["ProgressGradeVarGT"]);

            //    if (dr["ProgressivePlannedSQM"] != DBNull.Value)
            //        fProgressivePlannedSQM += Convert.ToDecimal(dr["ProgressivePlannedSQM"]);
            //    if (dr["ProgressiveActualSQM"] != DBNull.Value)
            //        fProgressiveActualSQM += Convert.ToDecimal(dr["ProgressiveActualSQM"]);
            //    if (dr["ProgressiveVarSQM"] != DBNull.Value)
            //        fProgressiveVarSQM += Convert.ToDecimal(dr["ProgressiveVarSQM"]);

            //    if (dr["ProgressivePlannedKG"] != DBNull.Value)
            //        fProgressivePlannedKG += Convert.ToDecimal(dr["ProgressivePlannedKG"]);
            //    if (dr["ProgressiveActualKG"] != DBNull.Value)
            //        fProgressiveActualKG += Convert.ToDecimal(dr["ProgressiveActualKG"]);
            //    if (dr["ProgressiveVarKG"] != DBNull.Value)
            //        fProgressiveVarKG += Convert.ToDecimal(dr["ProgressiveVarKG"]);
            //}


            //if (fProgressivePlannedSQMSUM != 0)
            //    fMonthlyPlanningCMGT = fMonthlyPlanningCMGT / fProgressivePlannedSQMSUM;
            //if (fProgressivePlannedSQMSUM != 0)
            //    fMonthlyPlanningGT = fMonthlyPlanningGT / fProgressivePlannedSQMSUM;

            //if (fProgressiveActualSQMSUM != 0)
            //    fProgressGradeActualCMGT = fProgressiveActualKGSUM / fProgressiveActualSQMSUM / 0.0278m * 1000;
            //if (fProgressiveActualSQMSUM != 0)
            //    fProgressGradeActualGT = fProgressGradeActualGT / fProgressiveActualSQMSUM;
            //if (fProgressGradeActualCMGT != 0)
            //    fProgressGradeVarCMGT = fMonthlyPlanningCMGT - fProgressGradeActualCMGT;
            //if (fProgressGradeActualGT != 0)
            //    fProgressGradeVarGT = fMonthlyPlanningGT - fProgressGradeActualGT;

            //DataRow drTotals = tblTotals.NewRow();

            //drTotals["MonthlyPlanningSQM"] = DisplayFmt.CustomDecimal(fMonthlyPlanningSQM, 0);
            //drTotals["MonthlyPlanningCMGT"] = DisplayFmt.CustomDecimal(fMonthlyPlanningCMGT, 0);
            //drTotals["MonthlyPlanningGT"] = DisplayFmt.CustomDecimal(fMonthlyPlanningGT, 2);
            //drTotals["MonthlyPlanningKGs"] = DisplayFmt.CustomDecimal(fMonthlyPlanningKGs, 3);
            //drTotals["ProgressGradeActualCMGT"] = DisplayFmt.CustomDecimal(fProgressGradeActualCMGT, 0);
            //drTotals["ProgressGradeActualGT"] = DisplayFmt.CustomDecimal(fProgressGradeActualGT, 2);
            //drTotals["ProgressGradeVarCMGT"] = DisplayFmt.CustomDecimal(fProgressGradeVarCMGT, 0);
            //drTotals["ProgressGradeVarGT"] = DisplayFmt.CustomDecimal(fProgressGradeVarGT, 2);
            //drTotals["ProgressivePlannedSQM"] = DisplayFmt.CustomDecimal(fProgressivePlannedSQM, 0);
            //drTotals["ProgressiveActualSQM"] = DisplayFmt.CustomDecimal(fProgressiveActualSQM, 0);
            //drTotals["ProgressiveVarSQM"] = DisplayFmt.CustomDecimal(fProgressiveVarSQM, 0);
            //drTotals["ProgressivePlannedKG"] = DisplayFmt.CustomDecimal(fProgressivePlannedKG, 3);
            //drTotals["ProgressiveActualKG"] = DisplayFmt.CustomDecimal(fProgressiveActualKG, 3);
            //drTotals["ProgressiveVarKG"] = DisplayFmt.CustomDecimal(fProgressiveVarKG, 3);

            //drTotals["GradePerformance"] = 0.0m;
            //drTotals["SQMPerformance"] = 0.0m;
            //drTotals["KGPerformance"] = 0.0m;

            //if (fMonthlyPlanningCMGT != 0)
            //    drTotals["GradePerformance"] = DisplayFmt.PercSmall_WithSign(fProgressGradeActualCMGT / fMonthlyPlanningCMGT);
            //if (fProgressivePlannedSQM != 0)
            //    drTotals["SQMPerformance"] = DisplayFmt.PercSmall_WithSign(fProgressiveActualSQM / fProgressivePlannedSQM);
            //if (fProgressivePlannedKG != 0)
            //    drTotals["KGPerformance"] = DisplayFmt.PercSmall_WithSign(fProgressiveActualKG / fProgressivePlannedKG);

            //tblTotals.Rows.Add(drTotals);
            //tblTotals.AcceptChanges();



            //dsTopPanels.Tables.Add(tblTotals);

            //#endregion


            //#region Custom Data

            //DataTable tblCustom = new DataTable("Custom");
            //tblCustom.Columns.Add("Banner");
            //tblCustom.Columns.Add("Month");
            //tblCustom.Columns.Add("Shaft");
            //tblCustom.Columns.Add("Section");

            //DateTime dtMonth = new DateTime(Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth).Substring(0, 4)),
            //    Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth).Substring(4, 2)), 1);
            //SysSettings aa = new SysSettings();
            //aa.systemDBTag = theSystemDBTag;
            //aa.connection = UserCurrentInfo.Connection;
            //aa.GetSysSettings();
            //DataRow drCustom = tblCustom.NewRow();
            //drCustom["Banner"] = "PERFORMANCE OF TOP PANELS";
            //drCustom["Month"] = dtMonth.ToString("MMM-yy");
            //drCustom["Shaft"] = SysSettings.Banner;
            //drCustom["Section"] = reportSettings.SectionID;
            //tblCustom.Rows.Add(drCustom);
            //tblCustom.AcceptChanges();

            //theReport.RegisterData(tblCustom, "CustomData");

            //#endregion

            //theReport.RegisterData(dsTopPanels);

            //theReport.Load(TGlobalItems.ReportsFolder + "\\TopPanelsReport.frx");


            ////if (MessageBox.Show("Design?", "Design?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            ////    theReport.Design();

            //theReport.Prepare();
            //theReport.Refresh();
            //ActiveReport.SetReport = theReport;
            //ActiveReport.isDone = true;
            ////theReport.Design();

            MWDataManager.clsDataAccess _dbMan11 = new MWDataManager.clsDataAccess();
            _dbMan11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan11.SqlStatement = _dbMan11.SqlStatement + "select * from Code_TopPanelsReport \r\n";

            _dbMan11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan11.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan11.ResultsTableName = "Banner";
            _dbMan11.ExecuteInstruction();

            string NoTopPanels = _dbMan11.ResultsDataTable.Rows[0][0].ToString();

            if (reportSettings.ReportType == "0")
            {
            DataTable Graph = new DataTable();
            TimeSpan Span;
            if (procs.ExtractBeforeColon(reportSettings.SectionID) == "1" || procs.ExtractBeforeColon(reportSettings.SectionID) == "0" || procs.ExtractBeforeColon(reportSettings.SectionID) == "GM")
            {


                MWDataManager.clsDataAccess _dbManBanner = new MWDataManager.clsDataAccess();
                _dbManBanner.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManBanner.SqlStatement = "Select '" + SysSettings.Banner + "' Banner, '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' ProdMonth, '" + reportSettings.SectionID + "' Section, " + NoTopPanels + " NoTopPanels ";

                _dbManBanner.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManBanner.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManBanner.ResultsTableName = "Banner";
                _dbManBanner.ExecuteInstruction();

                DataSet ReportDataset = new DataSet();
                ReportDataset.Tables.Add(_dbManBanner.ResultsDataTable);

                theReport.RegisterData(ReportDataset);
                Top20Graph.RegisterData(ReportDataset);


                ////////////////////////////////////////Graph//////////////////////////////////////////////////

                string GraphWorkplace = "";
                int q = -1;
                int GraphPlanSqm = 0;
                int GraphProgPlanSqm = 0;


                int GraphBookSqm = 0;
                int GraphBookProgSqm = 0;

                decimal Trendline = 0;






                for (int s = 0; s <= 10; s++)
                {
                    Graph.Columns.Add();
                }

                MWDataManager.clsDataAccess _dbManGraph = new MWDataManager.clsDataAccess();
                _dbManGraph.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " select '1. Top 20 Panels' groupa, b.CalendarDate, case when  b.CalendarDate < GETDATE()-1 then 'Y' else 'N' end as sssss,SUM(b.plansqm) PlanSQM,SUM(b.BookSqm) BookSqm,SUM(b.AdjSqm) AdjSqm, max(workingday) wday,\r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  convert(decimal(18,0),SUM(b.BookSqm+b.AdjSqm)/SUM(b.plansqm)*100) ach from  (select top(20) p.workplaceid, W.description, p.prodmonth, \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " p.SECTIONid, s2.SectionID mosec, s2.Name moname,    p.fl, Content cont, cmgt \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " from tbl_PlanMonth p, tbl_Section s, tbl_Section s1, tbl_Section s2, tbl_Workplace w \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " where  p.Workplaceid = w.WorkplaceID and  p.Sectionid = s.sectionid \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and p.Prodmonth = s.prodmonth and  s.reporttoSectionid = s1.sectionid \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s.Prodmonth = s1.prodmonth and  s1.reporttoSectionid = s2.sectionid \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s1.Prodmonth = s2.prodmonth and  p.activity <> 1  and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' \r\n";

                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and (s1.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "' or s2.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "') \r\n";

                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " order by cont desc) a,  (select WorkplaceID, p.prodmonth, p.sectionid, CalendarDate, \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " WorkingDay, case when Sqm != 0 then sqm else NULL end as plansqm, BookSqm, Content plancont, BookGrams+adjcont bookcont,ShiftDay,BookProb, AdjSqm \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " from tbl_Planning p, tbl_Section s, tbl_Section s1, tbl_Section s2   where  p.prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and Activity <> 1 \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  and p.Prodmonth = s.Prodmonth and p.SectionID = s.SectionID and  \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " s.Prodmonth = s1.Prodmonth and s.ReportToSectionid = s1.SectionID and \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " s1.Prodmonth = s2.Prodmonth and s1.ReportToSectionid = s2.SectionID \r\n";

                //    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  and (s1.reporttoSectionid = '" + procs.ExtractBeforeColon( reportSettings.SectionID) + "' or s2.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "') \r\n";

                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " ) b where a.workplaceid = b.workplaceid and a.prodmonth = b.prodmonth \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and a.Sectionid = b.sectionid \r\n";
                //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " group by b.CalendarDate  order by calendardate \r\n";

                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  select '1. Top "+NoTopPanels+ " Panels' groupa, b.CalendarDate, case when b.CalendarDate < GETDATE() - 1 then 'Y' else 'N' end as sssss,SUM(b.plansqm) PlanSQM,SUM(isnull(b.BookSqm,0)) BookSqm,SUM(isnull(b.AdjSqm,0)) AdjSqm, max(workingday) wday, \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " convert(decimal(18, 0), SUM(b.BookSqm + b.AdjSqm) / SUM(b.plansqm) * 100) ach from  (select top("+ NoTopPanels + ") p.workplaceid, W.description, p.prodmonth, \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "   p.SECTIONid, s2.SectionID mosec, s2.Name moname, p.fl, 0 cont, cmgt \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  from vw_PlanMonth p, Section s, Section s1, Section s2, Workplace w \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  where  p.Workplaceid = w.WorkplaceID and  p.Sectionid = s.sectionid  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and p.Prodmonth = s.prodmonth and  s.reporttoSectionid = s1.sectionid  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s.Prodmonth = s1.prodmonth and  s1.reporttoSectionid = s2.sectionid  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s1.Prodmonth = s2.prodmonth and  p.activity <> 1  and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  \r\n";

                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " order by cont desc) a,  (select WorkplaceID, p.prodmonth, p.sectionid, CalendarDate,  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  WorkingDay, case when Sqm != 0 then sqm else NULL end as plansqm, BookSqm, Grams plancont, BookGrams+adjcont bookcont,ShiftDay,ProblemID, AdjSqm  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " from vw_Planning p, Section s, Section s1, Section s2 where  p.prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and Activity <> 1  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and p.Prodmonth = s.Prodmonth and p.SectionID = s.SectionID and  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " s.Prodmonth = s1.Prodmonth and s.ReportToSectionid = s1.SectionID and  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " s1.Prodmonth = s2.Prodmonth and s1.ReportToSectionid = s2.SectionID  \r\n";

                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " ) b where a.workplaceid = b.workplaceid and a.prodmonth = b.prodmonth  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and a.Sectionid = b.sectionid  \r\n";
                _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " group by b.CalendarDate order by calendardate  \r\n";




                _dbManGraph.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGraph.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGraph.ResultsTableName = "Graph";
                _dbManGraph.ExecuteInstruction();

                int sumbook = 0;

                int xx1 = 0;

                int wday = 0;


                int sumbookmm = 0;
                int sumplanmm = 0;

                foreach (DataRow r in _dbManGraph.ResultsDataTable.Rows)
                {
                    if (r["BookSqm"] != DBNull.Value || r["BookSqm"] != null)
                    {
                        sumbook = sumbook + Convert.ToInt32(r["BookSqm"].ToString()) + Convert.ToInt32(r["AdjSqm"].ToString());

                        if (r["sssss"].ToString() == "Y")
                        {
                            sumbookmm = sumbookmm + Convert.ToInt32(r["BookSqm"].ToString()) + Convert.ToInt32(r["AdjSqm"].ToString());
                            if (r["PlanSqm"] != DBNull.Value)
                                sumplanmm = sumplanmm + Convert.ToInt32(r["PlanSqm"].ToString());

                        }



                    }

                    if (r["wday"].ToString() == "Y")
                        if (r["sssss"].ToString() == "Y")
                            xx1 = xx1 + 1;

                }

                int zz1 = 0;

                int fistsqm = 0;

                decimal Prevsqm = 0;

                foreach (DataRow r in _dbManGraph.ResultsDataTable.Rows)
                {
                    // MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).Date.ToString());
                    // MessageBox.Show(DateTime.Now.Date.ToString());
                    if (r["wday"].ToString() == "Y")
                    {
                        if (zz1 == 0)
                            if (r["BookSqm"] != DBNull.Value)
                                fistsqm = Convert.ToInt32(r["BookSqm"].ToString()) + Convert.ToInt32(r["AdjSqm"].ToString());



                        zz1 = zz1 + 1;

                        Graph.Rows.Add();
                        q = q + 1;

                        GraphWorkplace = r["groupa"].ToString();


                        Graph.Rows[q][0] = GraphWorkplace;
                        Graph.Rows[q][1] = Convert.ToDateTime(r["CalendarDate"].ToString()).ToShortDateString();
                        if (r["PlanSqm"] != DBNull.Value)
                        {
                            Graph.Rows[q][2] = r["PlanSqm"].ToString();
                        }
                        else
                        {
                            Graph.Rows[q][2] = null;
                        }
                        Graph.Rows[q][3] = r["BookSqm"].ToString();



                        if (r["PlanSqm"] != DBNull.Value)
                        {

                            GraphProgPlanSqm = GraphProgPlanSqm + Convert.ToInt32(r["PlanSqm"].ToString());
                        }

                        if (r["BookSqm"] != DBNull.Value)
                        {
                            GraphBookProgSqm = GraphBookProgSqm + Convert.ToInt32(r["BookSqm"].ToString());
                        }

                        Graph.Rows[q][4] = GraphProgPlanSqm.ToString();
                        Graph.Rows[q][5] = GraphBookProgSqm.ToString();

                        // if (r["BookSqm"] != DBNull.Value)
                        // {
                        //     GraphBookSqm = GraphBookSqm + Convert.ToInt16(r["BookSqm"].ToString());
                        // }


                        // ach
                        Graph.Rows[q][7] = r["ach"].ToString();
                        Graph.Rows[q][8] = r["sssss"].ToString();

                        Trendline = (sumbook / (xx1 + Convert.ToDecimal(0.001)));

                        int avgbook = 0;

                        if (xx1 > 0)
                            avgbook = (sumbook / xx1);

                        decimal diffa = (Convert.ToDecimal(avgbook) - Convert.ToDecimal(fistsqm)) / Convert.ToDecimal(10);



                        if (zz1 == 1)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(2)) + fistsqm) + Prevsqm).ToString();

                        if (zz1 == 2)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(1.9))) + Prevsqm).ToString();

                        if (zz1 == 3)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(1.8))) + Prevsqm).ToString();

                        if (zz1 == 4)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(1.5))) + Prevsqm).ToString();

                        if (zz1 == 5)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(1))) + Prevsqm).ToString();

                        if (zz1 == 6)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(0.8))) + Prevsqm).ToString();

                        if (zz1 == 7)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(0.5))) + Prevsqm).ToString();

                        if (zz1 == 8)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(0.3))) + Prevsqm).ToString();

                        if (zz1 == 9)
                            Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(0.2))) + Prevsqm).ToString();


                        if (zz1 >= 10)
                            Graph.Rows[q][6] = (avgbook).ToString();

                        Prevsqm = ((Convert.ToDecimal(Graph.Rows[q][6])));
                    }

                }


                //Graph = _dbManGraph.ResultsDataTable;

                Graph.TableName = "Graph";
                DataSet ReportDataGraph = new DataSet();
                ReportDataGraph.Tables.Add(Graph);


                Top20Graph.RegisterData(ReportDataGraph);

                //new code
                string Head1 = "";
                string Head2 = "";
                string Head3 = "";

                if (reportSettings.Meas == "Gold")
                    Head1 = "Gold";
                else
                    Head1 = "Cmgt";

                if (reportSettings.Type == "Dynamic")
                    Head2 = "Dynamic";
                else
                    Head2 = "Locked";

                if (reportSettings.Type == "Dynamic")
                    Head3 = "Standard";
                else
                    Head3 = "Cycle";

                MWDataManager.clsDataAccess _dbMan12 = new MWDataManager.clsDataAccess();
                _dbMan12.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan12.SqlStatement = _dbMan12.SqlStatement + " select '" + Head1 + "' Head1, '" + Head2 + "' Head2, '" + Head3 + "' Head3 ";
                _dbMan12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan12.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan12.ResultsTableName = "Headings";
                _dbMan12.ExecuteInstruction();

                DataTable dtHeading = _dbMan12.ResultsDataTable;
                DataSet dsHeadings = new DataSet();
                dsHeadings.Tables.Add(dtHeading);
                theReport.RegisterData(dsHeadings);

                Double aa = Math.Round(sumbookmm / (sumplanmm + 0.00001) * 100, 0);


                MWDataManager.clsDataAccess _dbMan12new = new MWDataManager.clsDataAccess();
                _dbMan12new.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan12new.SqlStatement = _dbMan12new.SqlStatement + " select '" + aa + "' aa ";
                _dbMan12new.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan12new.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan12new.ResultsTableName = "Headings123";
                _dbMan12new.ExecuteInstruction();

                DataTable dtHeadingaa = _dbMan12new.ResultsDataTable;

                DataSet ReportDataGraph1 = new DataSet();
                ReportDataGraph1.Tables.Add(dtHeadingaa);
                Top20Graph.RegisterData(ReportDataGraph1);


                //Top20Graph.Load("Top20Graph.frx");

                //FastReport.Utils.XmlItem item = FastReport.Utils.Config.Root.FindItem("Forms").FindItem("PreviewForm");
                //item.SetProp("Maximized", "0");
                //item.SetProp("Left", Convert.ToString(Right - 800));
                //item.SetProp("Top", "0");
                //item.SetProp("Width", "800");
                //item.SetProp("Height", "650");
                ////Crew_Resize += new (pcReport_Resize);
                //// Top20Graph.Design();
                //Top20Graph.Show(false);
                //Top20Graph.Preview.ZoomWholePage();
                ///////////////////////////////////////////////////////////////////////////////////////////////

                MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
                _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDate.SqlStatement = " select MIN(CalendarDate) StartDate, MAX(CalendarDate) EndDate from vw_Planning where Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and activity <> 1 ";

                _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDate.ResultsTableName = "CalDate";
                _dbManDate.ExecuteInstruction();

                DataTable CalDate = new DataTable();
                int Day = 0;

                DateTime BeginDate;
                DateTime EndDate;

                //BeginDate = DateTime.

                BeginDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][0]);
                EndDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][1]);


                Span = DateTime.Now.Subtract(BeginDate);

                if (Convert.ToInt32(Span.Days) < 34)
                {



                }
                else
                {
                    BeginDate = EndDate.AddDays(-34);

                }


                CalDate.Rows.Add();

                for (int cc = 0; cc <= 34; cc++)
                {
                    if (BeginDate.AddDays(Day) <= EndDate)
                    {

                        CalDate.Columns.Add();
                        CalDate.Rows[0][cc] = BeginDate.AddDays(Day).ToString("dd MMM ddd");
                        Day = Day + 1;


                    }
                    else
                    {
                        CalDate.Columns.Add();
                        CalDate.Rows[0][cc] = "";
                    }

                }

                CalDate.Columns.Add();
                CalDate.Rows[0][CalDate.Columns.Count - 1] = Day.ToString();

                CalDate.TableName = "CalDates";



                DataSet ReportDate = new DataSet();
                ReportDate.Tables.Add(CalDate);

                theReport.RegisterData(ReportDate);


                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = _dbMan.SqlStatement + " select '1. Top " + NoTopPanels + " Panels' groupa, * from (select top(" + NoTopPanels + ") p.workplaceid, W.description, p.prodmonth, p.SECTIONid, s2.SectionID mosec, s2.Name moname,\r\n  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "  p.fl, convert(decimal(18,3),kg) cont, isnull(cmgt,0)cmgt\r\n  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " from vw_PlanMonth p, Section s, Section s1, Section s2, Workplace w\r\n ";


                _dbMan.SqlStatement = _dbMan.SqlStatement + " where\r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " p.Workplaceid = w.WorkplaceID and\r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " p.Sectionid = s.sectionid and p.Prodmonth = s.prodmonth and\r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " s.reporttoSectionid = s1.sectionid and s.Prodmonth = s1.prodmonth and\r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " s1.reporttoSectionid = s2.sectionid and s1.Prodmonth = s2.prodmonth and plancode = 'MP' and\r\n ";




                if (reportSettings.Meas == "Gold")
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " p.activity <> 1 and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'   order by cont desc) a,\r\n ";
                else
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " p.activity <> 1 and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'   order by cmgt desc) a,\r\n ";



                _dbMan.SqlStatement = _dbMan.SqlStatement + " (select WorkplaceID, prodmonth, sectionid, CalendarDate, WorkingDay, Sqm plansqm, BookSqm+isnull(AdjSqm,0) BookSqm, isnull(Grams,0) plancont, BookGrams+adjcont bookcont,ShiftDay,ProblemID\r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " from vw_Planning   where\r\n ";

                _dbMan.SqlStatement = _dbMan.SqlStatement + " prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and plancode = 'MP' \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and Activity <> 1) b\r\n ";

                _dbMan.SqlStatement = _dbMan.SqlStatement + " where a.workplaceid = b.workplaceid and a.prodmonth = b.prodmonth and a.Sectionid = b.sectionid\r\n ";



                _dbMan.SqlStatement = _dbMan.SqlStatement + "union\r\n ";

                _dbMan.SqlStatement = _dbMan.SqlStatement + "select * from (select top(" + NoTopPanels + ") groupa, workplaceid, description, prodmonth, SECTIONid, mosec, moname, fl, cont, sqmcmgt/(sqm+0.1) cmgt\r\n ";

                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select '2. Top " + NoTopPanels + " Crews' groupa, OrgUnitDay \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " workplaceid, p.CrewName description, p.prodmonth, p.SECTIONid, s2.SectionID mosec, s2.Name moname,\r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "sum(p.fl) fl,  convert(decimal(18,3),sum(kg)) cont, SUM(Sqm) sqm,  sUM(Sqm*cmgt) sqmcmgt  from vw_PlanMonth p, Section s, Section s1, Section s2, Workplace w\r\n  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "where  p.Workplaceid = w.WorkplaceID and  p.Sectionid = s.sectionid and p.Prodmonth = s.prodmonth\r\n  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "and  s.reporttoSectionid = s1.sectionid and s.Prodmonth = s1.prodmonth and plancode = 'MP'  and  s1.reporttoSectionid = s2.sectionid\r\n  ";



                _dbMan.SqlStatement = _dbMan.SqlStatement + "and s1.Prodmonth = s2.prodmonth and  p.activity <> 1 and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and p.OrgUnitDay <> '' group by p.OrgUnitDay,p.CrewName,\r\n  ";
                if (reportSettings.Meas == "Gold")
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "p.prodmonth, p.SECTIONid, s2.SectionID, s2.Name ) a order by cont desc) a,\r\n ";
                else
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "p.prodmonth, p.SECTIONid, s2.SectionID, s2.Name ) a order by sqmcmgt/(convert(numeric(10,2),sqm)+0.00001) desc) a,\r\n ";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select * from  ( \r\n ";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "select OrgUnitDay \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " WorkplaceID, prodmonth, sectionid, CalendarDate, WorkingDay, \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "sum(Sqm) plansqm, sum(BookSqm + isnull(adjsqm,0)) BookSqm, sum(isnull(Grams,0)) plancont, \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "sum(BookGrams + adjcont) bookcont, max(ShiftDay) ShiftDay,ProblemID from ( \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "   select a.*, b.OrgUnitDay from( \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " select * from vw_Planning where plancode = 'MP' )a \r\n ";


                _dbMan.SqlStatement = _dbMan.SqlStatement + " left outer join(Select Prodmonth, SectionID, Workplaceid, OrgUnitDay from Planmonth where plancode = 'MP' )b \r\n ";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "  on a.Prodmonth = b.Prodmonth \r\n ";


                _dbMan.SqlStatement = _dbMan.SqlStatement + " and a.Workplaceid = b.Workplaceid \r\n ";


                _dbMan.SqlStatement = _dbMan.SqlStatement + " and a.SectionID = b.SectionID)a \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " group by OrgUnitDay, prodmonth, sectionid, CalendarDate,WorkingDay, ProblemID \r\n ";




                _dbMan.SqlStatement = _dbMan.SqlStatement + " ) b  ) b\r\n  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "where a.workplaceid = b.workplaceid\r\n  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "and a.prodmonth = b.prodmonth and a.Sectionid = b.sectionid\r\n ";





                if (reportSettings.Meas == "Gold")
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " order by groupa, Cont desc, b.workplaceid, b.CalendarDate\r\n ";
                else
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " order by groupa, cmgt desc, b.workplaceid, b.CalendarDate\r\n ";

                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Grapha";
                _dbMan.ExecuteInstruction();

                DataTable Data = new DataTable();







                //Data.Columns.Count = 20;

                ///////////Load Data into DataTable////////////////////////////

                string Workplace = "";
                int y = -1;
                int PlanSqm = 0;
                int ProgSqm = 0;
                decimal PlanCont = 0;
                decimal ProgCont = 0;

                int BookSqm = 0;

                decimal BookCont = 0;


                int TotalShift = 0;
                int col = 16;

                for (int s = 0; s <= 62; s++)
                {
                    Data.Columns.Add();
                }




                foreach (DataRow dr in _dbMan.ResultsDataTable.Rows)
                {
                    // MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).Date.ToString());
                    // MessageBox.Show(DateTime.Now.Date.ToString());

                    if (Workplace != dr["Workplaceid"].ToString())
                    {
                        Data.Rows.Add();



                        y = y + 1;

                        Workplace = dr["Workplaceid"].ToString();

                        Data.Rows[y][0] = Workplace;  /////////Workplace Column
                        Data.Rows[y][61] = dr["groupa"].ToString();
                        Data.Rows[y][62] = (y + Convert.ToDecimal(1)).ToString();


                        Data.Rows[y][1] = dr["Description"].ToString();

                        Data.Rows[y][2] = dr["mosec"].ToString();

                        Data.Rows[y][3] = dr["moname"].ToString();

                        Data.Rows[y][4] = Math.Round(Convert.ToDecimal(dr["fl"].ToString()), 0).ToString();

                        //Data.Rows[y][5] = dr[""].ToString();

                        Data.Rows[y][6] = Math.Round(Convert.ToDecimal(dr["CMGT"].ToString()), 0).ToString();
                        Data.Rows[y][11] = 0;


                        PlanSqm = 0;
                        ProgSqm = 0;
                        PlanCont = 0;
                        ProgCont = 0;

                        BookSqm = 0;

                        BookCont = 0;


                        TotalShift = 0;
                        col = 16;

                        //////////////For first time/////////////////////


                        if (Convert.ToDateTime(dr["CalendarDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                        {

                            Data.Rows[y][7] = dr["ShiftDay"].ToString();

                            Data.Rows[y][8] = dr["Plansqm"].ToString(); // Actual Call
                        }
                        else
                        {
                            Data.Rows[y][7] = "";

                            Data.Rows[y][8] = "";
                        }

                        int zz = 0;

                        //string a = dr["CalendarDate"].ToString();

                        //if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date > DateTime.Now.Date.AddDays(-3))
                        //{
                        //    a = dr["CalendarDate"].ToString();
                        //}

                        //MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).ToString());
                        if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date < DateTime.Now.Date.AddDays(0))
                        {
                            // string a = dr["CalendarDate"].ToString();

                            ProgSqm = ProgSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                            Data.Rows[y][9] = ProgSqm.ToString();
                            ProgCont = ProgCont + Convert.ToDecimal(dr["plancont"].ToString());

                            Data.Rows[y][10] = ProgCont.ToString();

                            if (Data.Rows[y][11] == "")
                                Data.Rows[y][11] = 0;

                            if (dr["BookSqm"] != DBNull.Value)
                            {
                                BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                                Data.Rows[y][11] = BookSqm.ToString();
                            }
                            if (dr["BookCont"] != DBNull.Value)
                            {
                                BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                                Data.Rows[y][12] = BookCont.ToString();
                            }
                        }

                        PlanSqm = PlanSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                        Data.Rows[y][13] = PlanSqm.ToString();

                        //if (dr["BookSqm"] != DBNull.Value)
                        //{
                        //    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                        //    Data.Rows[y][14] = BookSqm.ToString();
                        //}

                        PlanCont = PlanCont + Convert.ToDecimal(dr["plancont"].ToString());

                        Data.Rows[y][14] = BookSqm.ToString();

                        //if (dr["BookCont"] != DBNull.Value)
                        //{
                        //    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                        //    Data.Rows[y][16] = BookCont.ToString();
                        //}
                        if (dr["WorkingDay"].ToString() != "N")
                        {

                            TotalShift = TotalShift + 1;
                        }

                        Data.Rows[y][15] = TotalShift.ToString();


                        Span = Convert.ToDateTime(dr["calendardate"].ToString()).Subtract(BeginDate);

                        col = Convert.ToInt32(Span.Days) + 16;


                        if (dr["BookSqm"] != DBNull.Value)
                        {
                            if (dr["ProblemID"].ToString() == "")
                            {
                                if (col >= 15)
                                    Data.Rows[y][col] = dr["BookSqm"].ToString();
                            }
                            else
                            {
                                if (dr["ProblemID"] != DBNull.Value)
                                {
                                    if (col >= 15)
                                        if (Convert.ToDecimal(dr["BookSqm"].ToString()) > 0)
                                            Data.Rows[y][col] = Convert.ToInt32(dr["BookSqm"].ToString());
                                        else
                                            Data.Rows[y][col] = dr["ProblemID"].ToString();
                                }
                            }
                        }
                        else
                        {
                            if (col >= 15)
                                Data.Rows[y][col] = "";
                        }
                        col++;



                    }
                    ////////////////////////////////////////////////


                    else
                    {


                        if (Convert.ToDateTime(dr["CalendarDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                        {

                            Data.Rows[y][7] = dr["ShiftDay"].ToString();

                            Data.Rows[y][8] = dr["Plansqm"].ToString(); // Actual Call
                        }


                        if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date < DateTime.Now.Date.AddDays(0))
                        {
                            ProgSqm = ProgSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                            Data.Rows[y][9] = ProgSqm.ToString();
                            ProgCont = ProgCont + Convert.ToDecimal(dr["plancont"].ToString());

                            Data.Rows[y][10] = ProgCont.ToString();

                            if (Data.Rows[y][11] == "")
                                Data.Rows[y][11] = 0;

                            if (dr["BookSqm"] != DBNull.Value)
                            {
                                BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                                Data.Rows[y][11] = BookSqm.ToString();
                            }
                            if (dr["BookCont"] != DBNull.Value)
                            {
                                BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                                Data.Rows[y][12] = BookCont.ToString();
                            }
                        }

                        PlanSqm = PlanSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                        Data.Rows[y][13] = PlanSqm.ToString();

                        //if (dr["BookSqm"] != DBNull.Value)
                        //{
                        //    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                        //    Data.Rows[y][14] = BookSqm.ToString();
                        //}

                        PlanCont = PlanCont + Convert.ToDecimal(dr["plancont"].ToString());

                        Data.Rows[y][14] = PlanCont.ToString();

                        //if (dr["BookCont"] != DBNull.Value)
                        //{
                        //    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                        //    Data.Rows[y][16] = BookCont.ToString();
                        //}
                        if (dr["WorkingDay"].ToString() != "N")
                        {

                            TotalShift = TotalShift + 1;
                        }

                        Data.Rows[y][15] = TotalShift.ToString();

                        Span = Convert.ToDateTime(dr["calendardate"].ToString()).Subtract(BeginDate);

                        col = Convert.ToInt32(Span.Days) + 16;

                        //if (col > 50)
                        //    col = 50;


                        if (dr["BookSqm"] != DBNull.Value)
                        {
                            if (dr["ProblemID"].ToString() == "")
                            {
                                if (col >= 16)
                                    Data.Rows[y][col] = dr["BookSqm"].ToString();
                            }
                            else
                            {
                                if (dr["ProblemID"] != DBNull.Value)
                                {
                                    if (col >= 16)
                                        if (Convert.ToDecimal(dr["BookSqm"].ToString()) > 0)
                                            Data.Rows[y][col] = Convert.ToInt32(dr["BookSqm"].ToString());
                                        else
                                            Data.Rows[y][col] = dr["ProblemID"].ToString();
                                }
                            }
                        }
                        else
                        {
                            if (col >= 16)
                                Data.Rows[y][col] = "";
                        }
                        col++;
                        //y++;

                    }

                }

                Data.TableName = "Data";
                DataSet ReportData = new DataSet();
                ReportData.Tables.Add(Data);

                theReport.RegisterData(ReportData);



                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "select * from (select * from \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "(Select '' HR_Rank, p.workplaceid, W.description, p.prodmonth, p.SECTIONid, s2.SectionID mosec, s2.Name moname, \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "p.fl, '' cont, cmgt   from PlanMonth p, Section s, Section s1, Section s2, Workplace w \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "where  p.Workplaceid = w.WorkplaceID and  p.Sectionid = s.sectionid and p.Prodmonth = s.prodmonth and \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "s.reporttoSectionid = s1.sectionid and s.Prodmonth = s1.prodmonth and  s1.reporttoSectionid = s2.sectionid \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "and s1.Prodmonth = s2.prodmonth and  p.activity <> 1 and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' ) a where HR_Rank < 6) a, \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "(select WorkplaceID, prodmonth, sectionid, CalendarDate, WorkingDay, Sqm plansqm, BookSqm+adjsqm BookSqm, \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "grams plancont, BookGrams+adjcont bookcont,ShiftDay  from vw_Planning   where  prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and prodmonth = 0 \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "and Activity <> 1) b  where a.workplaceid = b.workplaceid and a.prodmonth = b.prodmonth \r\n";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and a.Sectionid = b.sectionid  order by Cont desc, b.workplaceid, b.CalendarDate \r\n";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Banner";
                _dbMan1.ExecuteInstruction();

                DataTable Data1 = new DataTable();



                Workplace = "";
                y = -1;
                PlanSqm = 0;
                ProgSqm = 0;
                PlanCont = 0;
                ProgCont = 0;

                BookSqm = 0;

                BookCont = 0;


                TotalShift = 0;
                col = 16;


                for (int s = 0; s <= 60; s++)
                {
                    Data1.Columns.Add();
                }



                foreach (DataRow dr in _dbMan1.ResultsDataTable.Rows)
                {
                    // MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).Date.ToString());
                    // MessageBox.Show(DateTime.Now.Date.ToString());

                    if (Workplace != dr["Workplaceid"].ToString())
                    {
                        Data1.Rows.Add();
                        y = y + 1;

                        Workplace = dr["Workplaceid"].ToString();

                        Data1.Rows[y][0] = Workplace;  /////////Workplace Column

                        Data1.Rows[y][1] = dr["Description"].ToString();

                        Data1.Rows[y][2] = dr["mosec"].ToString();

                        Data1.Rows[y][3] = dr["moname"].ToString();

                        Data1.Rows[y][4] = Math.Round(Convert.ToDecimal(dr["fl"].ToString()), 0).ToString();

                        //Data.Rows[y][5] = dr[""].ToString();

                        Data1.Rows[y][6] = Math.Round(Convert.ToDecimal(dr["CMGT"].ToString()), 0).ToString();
                        Data1.Rows[y][11] = 0;


                        PlanSqm = 0;
                        ProgSqm = 0;
                        PlanCont = 0;
                        ProgCont = 0;

                        BookSqm = 0;

                        BookCont = 0;


                        TotalShift = 0;
                        col = 16;

                        //////////////For first time/////////////////////



                        if (Convert.ToDateTime(dr["CalendarDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                        {

                            Data1.Rows[y][7] = dr["ShiftDay"].ToString();

                            Data1.Rows[y][8] = dr["Plansqm"].ToString(); // Actual Call
                        }
                        else
                        {
                            Data1.Rows[y][7] = "";

                            Data1.Rows[y][8] = "";
                        }

                        int zz = 0;

                        //MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).ToString());
                        if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date <= DateTime.Now.Date)
                        {


                            ProgSqm = ProgSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                            Data1.Rows[y][9] = ProgSqm.ToString();
                            ProgCont = ProgCont + Convert.ToDecimal(dr["plancont"].ToString());

                            Data1.Rows[y][10] = ProgCont.ToString();

                            if (Data1.Rows[y][11] == "")
                                Data1.Rows[y][11] = 0;

                            if (dr["BookSqm"] != DBNull.Value)
                            {
                                BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                                Data1.Rows[y][11] = BookSqm.ToString();
                            }
                            if (dr["BookCont"] != DBNull.Value)
                            {
                                BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                                Data1.Rows[y][12] = BookCont.ToString();
                            }
                        }

                        PlanSqm = PlanSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                        Data1.Rows[y][13] = PlanSqm.ToString();

                        //if (dr["BookSqm"] != DBNull.Value)
                        //{
                        //    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                        //    Data.Rows[y][14] = BookSqm.ToString();
                        //}

                        PlanCont = PlanCont + Convert.ToDecimal(dr["plancont"].ToString());

                        Data1.Rows[y][14] = BookSqm.ToString();

                        //if (dr["BookCont"] != DBNull.Value)
                        //{
                        //    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                        //    Data.Rows[y][16] = BookCont.ToString();
                        //}
                        if (dr["WorkingDay"].ToString() != "N")
                        {

                            TotalShift = TotalShift + 1;
                        }

                        Data1.Rows[y][15] = TotalShift.ToString();




                        if (dr["BookSqm"] != DBNull.Value)
                        {
                            if (dr["ProblemID"].ToString() == "")
                            {

                                Data.Rows[y][col] = Convert.ToInt32(dr["BookSqm"].ToString());
                            }
                            else
                            {
                                if (dr["ProblemID"] != DBNull.Value)
                                {
                                    if (Convert.ToDecimal(dr["BookSqm"].ToString()) > 0)
                                        Data.Rows[y][col] = dr["BookSqm"].ToString();
                                    else
                                        Data.Rows[y][col] = dr["ProblemID"].ToString();
                                }
                            }
                        }
                        else
                        {
                            Data.Rows[y][col] = "";
                        }
                        col++;

                    }
                    ////////////////////////////////////////////////


                    else
                    {


                        if (Convert.ToDateTime(dr["CalendarDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                        {

                            Data1.Rows[y][7] = dr["ShiftDay"].ToString();

                            Data1.Rows[y][8] = dr["Plansqm"].ToString(); // Actual Call
                        }


                        if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date <= DateTime.Now.Date)
                        {
                            ProgSqm = ProgSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                            Data1.Rows[y][9] = ProgSqm.ToString();
                            ProgCont = ProgCont + Convert.ToDecimal(dr["plancont"].ToString());

                            Data1.Rows[y][10] = ProgCont.ToString();

                            if (Data1.Rows[y][11] == "")
                                Data1.Rows[y][11] = 0;

                            if (dr["BookSqm"] != DBNull.Value)
                            {
                                BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                                Data1.Rows[y][11] = BookSqm.ToString();
                            }
                            if (dr["BookCont"] != DBNull.Value)
                            {
                                BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                                Data1.Rows[y][12] = BookCont.ToString();
                            }
                        }

                        PlanSqm = PlanSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                        Data1.Rows[y][13] = PlanSqm.ToString();

                        //if (dr["BookSqm"] != DBNull.Value)
                        //{
                        //    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                        //    Data.Rows[y][14] = BookSqm.ToString();
                        //}

                        PlanCont = PlanCont + Convert.ToDecimal(dr["plancont"].ToString());

                        Data1.Rows[y][14] = PlanCont.ToString();

                        //if (dr["BookCont"] != DBNull.Value)
                        //{
                        //    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                        //    Data.Rows[y][16] = BookCont.ToString();
                        //}
                        if (dr["WorkingDay"].ToString() != "N")
                        {

                            TotalShift = TotalShift + 1;
                        }

                        Data1.Rows[y][15] = TotalShift.ToString();




                        if (dr["BookSqm"] != DBNull.Value)
                        {
                            if (dr["ProblemID"].ToString() == "")
                            {

                                Data.Rows[y][col] = dr["BookSqm"].ToString();
                            }
                            else
                            {
                                if (dr["ProblemID"] != DBNull.Value)
                                {
                                    if (Convert.ToDecimal(dr["BookSqm"].ToString()) > 0)
                                        Data.Rows[y][col] = dr["BookSqm"].ToString();
                                    else
                                        Data.Rows[y][col] = dr["ProblemID"].ToString();
                                }
                            }
                        }
                        else
                        {
                            Data.Rows[y][col] = "";
                        }
                        col++;
                        //y++;

                    }

                }


                Data1.TableName = "Data1";
                DataSet ReportData1 = new DataSet();
                ReportData1.Tables.Add(Data1);


                theReport.RegisterData(ReportData1);
            }
            else
            {
                if (reportSettings.ReportType == "0")
                {
                    MWDataManager.clsDataAccess _dbManBanner = new MWDataManager.clsDataAccess();
                    _dbManBanner.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManBanner.SqlStatement = "Select '" + SysSettings.Banner + "' Banner, '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' ProdMonth, '" + reportSettings.SectionID + "' Section, '"+NoTopPanels+ "' NoTopPanels ";

                    _dbManBanner.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManBanner.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManBanner.ResultsTableName = "Banner";
                    _dbManBanner.ExecuteInstruction();

                    DataSet ReportDataset = new DataSet();
                    ReportDataset.Tables.Add(_dbManBanner.ResultsDataTable);

                    theReport.RegisterData(ReportDataset);
                    Top20Graph.RegisterData(ReportDataset);


                    ////////////////////////////////////////Graph//////////////////////////////////////////////////

                    string GraphWorkplace = "";
                    int q = -1;
                    int GraphPlanSqm = 0;
                    int GraphProgPlanSqm = 0;


                    int GraphBookSqm = 0;
                    int GraphBookProgSqm = 0;

                    decimal Trendline = 0;






                    for (int s = 0; s <= 10; s++)
                    {
                        Graph.Columns.Add();
                    }

                    MWDataManager.clsDataAccess _dbManGraph = new MWDataManager.clsDataAccess();
                    _dbManGraph.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " select '1. Top 20 Panels' groupa, b.CalendarDate, case when  b.CalendarDate < GETDATE()-1 then 'Y' else 'N' end as sssss,SUM(b.plansqm) PlanSQM,SUM(b.BookSqm) BookSqm,SUM(b.AdjSqm) AdjSqm, max(workingday) wday,\r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  convert(decimal(18,0),SUM(b.BookSqm+b.AdjSqm)/SUM(b.plansqm)*100) ach from  (select top(20) p.workplaceid, W.description, p.prodmonth, \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " p.SECTIONid, s2.SectionID mosec, s2.Name moname,    p.fl, Content cont, cmgt \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " from tbl_PlanMonth p, tbl_Section s, tbl_Section s1, tbl_Section s2, tbl_Workplace w \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " where  p.Workplaceid = w.WorkplaceID and  p.Sectionid = s.sectionid \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and p.Prodmonth = s.prodmonth and  s.reporttoSectionid = s1.sectionid \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s.Prodmonth = s1.prodmonth and  s1.reporttoSectionid = s2.sectionid \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s1.Prodmonth = s2.prodmonth and  p.activity <> 1  and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' \r\n";

                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and (s1.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "' or s2.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "') \r\n";

                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " order by cont desc) a,  (select WorkplaceID, p.prodmonth, p.sectionid, CalendarDate, \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " WorkingDay, case when Sqm != 0 then sqm else NULL end as plansqm, BookSqm, Content plancont, BookGrams+adjcont bookcont,ShiftDay,BookProb, AdjSqm \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " from tbl_Planning p, tbl_Section s, tbl_Section s1, tbl_Section s2   where  p.prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and Activity <> 1 \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  and p.Prodmonth = s.Prodmonth and p.SectionID = s.SectionID and  \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " s.Prodmonth = s1.Prodmonth and s.ReportToSectionid = s1.SectionID and \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " s1.Prodmonth = s2.Prodmonth and s1.ReportToSectionid = s2.SectionID \r\n";

                    //    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  and (s1.reporttoSectionid = '" + procs.ExtractBeforeColon( reportSettings.SectionID) + "' or s2.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "') \r\n";

                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " ) b where a.workplaceid = b.workplaceid and a.prodmonth = b.prodmonth \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and a.Sectionid = b.sectionid \r\n";
                    //_dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " group by b.CalendarDate  order by calendardate \r\n";

                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  select '1. Top "+NoTopPanels+" Panels' groupa, b.CalendarDate, case when b.CalendarDate < GETDATE() - 1 then 'Y' else 'N' end as sssss,SUM(b.plansqm) PlanSQM,SUM(b.BookSqm) BookSqm,SUM(b.AdjSqm) AdjSqm, max(workingday) wday, \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " convert(decimal(18, 0), SUM(b.BookSqm + b.AdjSqm) / SUM(b.plansqm) * 100) ach from  (select top(" + NoTopPanels + ") p.workplaceid, W.description, p.prodmonth, \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "   p.SECTIONid, s2.SectionID mosec, s2.Name moname, p.fl, 0 cont, cmgt \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  from PlanMonth p, Section s, Section s1, Section s2, Workplace w \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  where  p.Workplaceid = w.WorkplaceID and  p.Sectionid = s.sectionid  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and p.Prodmonth = s.prodmonth and  s.reporttoSectionid = s1.sectionid  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s.Prodmonth = s1.prodmonth and  s1.reporttoSectionid = s2.sectionid  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and s1.Prodmonth = s2.prodmonth and  p.activity <> 1  and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and(s1.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "' or s2.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "')  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " order by cont desc) a,  (select WorkplaceID, p.prodmonth, p.sectionid, CalendarDate,  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + "  WorkingDay, case when Sqm != 0 then sqm else NULL end as plansqm, BookSqm, Grams plancont, BookGrams+adjcont bookcont,ShiftDay,ProblemID, AdjSqm  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " from vw_Planning p, Section s, Section s1, Section s2 where  p.prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'  and Activity <> 1  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and p.Prodmonth = s.Prodmonth and p.SectionID = s.SectionID and  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " s.Prodmonth = s1.Prodmonth and s.ReportToSectionid = s1.SectionID and  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " s1.Prodmonth = s2.Prodmonth and s1.ReportToSectionid = s2.SectionID  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and(s1.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "' or s2.reporttoSectionid = '" + procs.ExtractBeforeColon(reportSettings.SectionID) + "')  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " ) b where a.workplaceid = b.workplaceid and a.prodmonth = b.prodmonth  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " and a.Sectionid = b.sectionid  \r\n";
                    _dbManGraph.SqlStatement = _dbManGraph.SqlStatement + " group by b.CalendarDate order by calendardate  \r\n";




                    _dbManGraph.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManGraph.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManGraph.ResultsTableName = "Graph";
                    _dbManGraph.ExecuteInstruction();

                    int sumbook = 0;

                    int xx1 = 0;

                    int wday = 0;


                    int sumbookmm = 0;
                    int sumplanmm = 0;

                    foreach (DataRow r in _dbManGraph.ResultsDataTable.Rows)
                    {
                        if (r["BookSqm"] != DBNull.Value)
                        {
                            sumbook = sumbook + Convert.ToInt32(r["BookSqm"].ToString()) + Convert.ToInt32(r["AdjSqm"].ToString());

                            if (r["sssss"].ToString() == "Y")
                            {
                                sumbookmm = sumbookmm + Convert.ToInt32(r["BookSqm"].ToString()) + Convert.ToInt32(r["AdjSqm"].ToString());
                                if (r["PlanSqm"] != DBNull.Value)
                                    sumplanmm = sumplanmm + Convert.ToInt32(r["PlanSqm"].ToString());

                            }



                        }

                        if (r["wday"].ToString() == "Y")
                            if (r["sssss"].ToString() == "Y")
                                xx1 = xx1 + 1;

                    }

                    int zz1 = 0;

                    int fistsqm = 0;

                    decimal Prevsqm = 0;

                    foreach (DataRow r in _dbManGraph.ResultsDataTable.Rows)
                    {
                        // MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).Date.ToString());
                        // MessageBox.Show(DateTime.Now.Date.ToString());
                        if (r["wday"].ToString() == "Y")
                        {
                            if (zz1 == 0)
                                if (r["BookSqm"] != DBNull.Value)
                                    fistsqm = Convert.ToInt32(r["BookSqm"].ToString()) + Convert.ToInt32(r["AdjSqm"].ToString());



                            zz1 = zz1 + 1;

                            Graph.Rows.Add();
                            q = q + 1;

                            GraphWorkplace = r["groupa"].ToString();


                            Graph.Rows[q][0] = GraphWorkplace;
                            Graph.Rows[q][1] = Convert.ToDateTime(r["CalendarDate"].ToString()).ToShortDateString();
                            if (r["PlanSqm"] != DBNull.Value)
                            {
                                Graph.Rows[q][2] = r["PlanSqm"].ToString();
                            }
                            else
                            {
                                Graph.Rows[q][2] = null;
                            }
                            Graph.Rows[q][3] = r["BookSqm"].ToString();



                            if (r["PlanSqm"] != DBNull.Value)
                            {

                                GraphProgPlanSqm = GraphProgPlanSqm + Convert.ToInt32(r["PlanSqm"].ToString());
                            }

                            if (r["BookSqm"] != DBNull.Value)
                            {
                                GraphBookProgSqm = GraphBookProgSqm + Convert.ToInt32(r["BookSqm"].ToString());
                            }

                            Graph.Rows[q][4] = GraphProgPlanSqm.ToString();
                            Graph.Rows[q][5] = GraphBookProgSqm.ToString();

                            // if (r["BookSqm"] != DBNull.Value)
                            // {
                            //     GraphBookSqm = GraphBookSqm + Convert.ToInt16(r["BookSqm"].ToString());
                            // }


                            // ach
                            Graph.Rows[q][7] = r["ach"].ToString();
                            Graph.Rows[q][8] = r["sssss"].ToString();

                            Trendline = (sumbook / xx1);

                            int avgbook = 0;

                            if (xx1 > 0)
                                avgbook = (sumbook / xx1);

                            decimal diffa = (Convert.ToDecimal(avgbook) - Convert.ToDecimal(fistsqm)) / Convert.ToDecimal(10);



                            if (zz1 == 1)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(2)) + fistsqm) + Prevsqm).ToString();

                            if (zz1 == 2)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(1.9))) + Prevsqm).ToString();

                            if (zz1 == 3)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(1.8))) + Prevsqm).ToString();

                            if (zz1 == 4)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(1.5))) + Prevsqm).ToString();

                            if (zz1 == 5)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(1))) + Prevsqm).ToString();

                            if (zz1 == 6)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(0.8))) + Prevsqm).ToString();

                            if (zz1 == 7)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(0.5))) + Prevsqm).ToString();

                            if (zz1 == 8)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(0.3))) + Prevsqm).ToString();

                            if (zz1 == 9)
                                Graph.Rows[q][6] = (((diffa * Convert.ToDecimal(0.2))) + Prevsqm).ToString();


                            if (zz1 >= 10)
                                Graph.Rows[q][6] = (avgbook).ToString();

                            Prevsqm = ((Convert.ToDecimal(Graph.Rows[q][6])));
                        }

                    }


                    //Graph = _dbManGraph.ResultsDataTable;

                    Graph.TableName = "Graph";
                    DataSet ReportDataGraph = new DataSet();
                    ReportDataGraph.Tables.Add(Graph);


                    Top20Graph.RegisterData(ReportDataGraph);

                    //new code
                    string Head1 = "";
                    string Head2 = "";
                    string Head3 = "";

                    if (reportSettings.Meas == "Gold")
                        Head1 = "Gold";
                    else
                        Head1 = "Cmgt";

                    if (reportSettings.Type == "Dynamic")
                        Head2 = "Dynamic";
                    else
                        Head2 = "Locked";

                    if (reportSettings.Type == "Dynamic")
                        Head3 = "Standard";
                    else
                        Head3 = "Cycle";

                    MWDataManager.clsDataAccess _dbMan12 = new MWDataManager.clsDataAccess();
                    _dbMan12.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan12.SqlStatement = _dbMan12.SqlStatement + " select '" + Head1 + "' Head1, '" + Head2 + "' Head2, '" + Head3 + "' Head3 ";
                    _dbMan12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan12.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan12.ResultsTableName = "Headings";
                    _dbMan12.ExecuteInstruction();

                    DataTable dtHeading = _dbMan12.ResultsDataTable;
                    DataSet dsHeadings = new DataSet();
                    dsHeadings.Tables.Add(dtHeading);
                    theReport.RegisterData(dsHeadings);

                    Double aa = Math.Round(sumbookmm / (sumplanmm + 0.00001) * 100, 0);


                    MWDataManager.clsDataAccess _dbMan12new = new MWDataManager.clsDataAccess();
                    _dbMan12new.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan12new.SqlStatement = _dbMan12new.SqlStatement + " select '" + aa + "' aa ";
                    _dbMan12new.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan12new.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan12new.ResultsTableName = "Headings123";
                    _dbMan12new.ExecuteInstruction();

                    DataTable dtHeadingaa = _dbMan12new.ResultsDataTable;

                    DataSet ReportDataGraph1 = new DataSet();
                    ReportDataGraph1.Tables.Add(dtHeadingaa);
                    Top20Graph.RegisterData(ReportDataGraph1);


                    //Top20Graph.Load("Top20Graph.frx");

                    //FastReport.Utils.XmlItem item = FastReport.Utils.Config.Root.FindItem("Forms").FindItem("PreviewForm");
                    //item.SetProp("Maximized", "0");
                    //item.SetProp("Left", Convert.ToString(Right - 800));
                    //item.SetProp("Top", "0");
                    //item.SetProp("Width", "800");
                    //item.SetProp("Height", "650");
                    ////Crew_Resize += new (pcReport_Resize);
                    //// Top20Graph.Design();
                    //Top20Graph.Show(false);
                    //Top20Graph.Preview.ZoomWholePage();
                    ///////////////////////////////////////////////////////////////////////////////////////////////

                    MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
                    _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManDate.SqlStatement = " select MIN(CalendarDate) StartDate, MAX(CalendarDate) EndDate from Planning where Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and activity <> 1 ";

                    _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManDate.ResultsTableName = "CalDate";
                    _dbManDate.ExecuteInstruction();

                    DataTable CalDate = new DataTable();
                    int Day = 0;

                    DateTime BeginDate;
                    DateTime EndDate;

                    //BeginDate = DateTime.

                    BeginDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][0]);
                    EndDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][1]);


                    Span = DateTime.Now.Subtract(BeginDate);

                    if (Convert.ToInt32(Span.Days) < 34)
                    {



                    }
                    else
                    {
                        BeginDate = EndDate.AddDays(-34);

                    }


                    CalDate.Rows.Add();

                    for (int cc = 0; cc <= 34; cc++)
                    {
                        if (BeginDate.AddDays(Day) <= EndDate)
                        {

                            CalDate.Columns.Add();
                            CalDate.Rows[0][cc] = BeginDate.AddDays(Day).ToString("dd MMM ddd");
                            Day = Day + 1;


                        }
                        else
                        {
                            CalDate.Columns.Add();
                            CalDate.Rows[0][cc] = "";
                        }

                    }

                    CalDate.Columns.Add();
                    CalDate.Rows[0][CalDate.Columns.Count - 1] = Day.ToString();

                    CalDate.TableName = "CalDates";



                    DataSet ReportDate = new DataSet();
                    ReportDate.Tables.Add(CalDate);

                    theReport.RegisterData(ReportDate);


                        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                        _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " select '1. Top " + NoTopPanels + " Panels' groupa, * from (select top(" + NoTopPanels + ") p.workplaceid, W.description, p.prodmonth, p.SECTIONid, s2.SectionID mosec, s2.Name moname,\r\n  ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "  p.fl, convert(decimal(18,3),kg) cont, isnull(cmgt,0)cmgt\r\n  ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " from vw_PlanMonth p, Section s, Section s1, Section s2, Workplace w\r\n ";


                        _dbMan.SqlStatement = _dbMan.SqlStatement + " where\r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " p.Workplaceid = w.WorkplaceID and\r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " p.Sectionid = s.sectionid and p.Prodmonth = s.prodmonth and\r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " s.reporttoSectionid = s1.sectionid and s.Prodmonth = s1.prodmonth and\r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " s1.reporttoSectionid = s2.sectionid and s1.Prodmonth = s2.prodmonth and plancode = 'LP' and\r\n ";




                        if (reportSettings.Meas == "Gold")
                            _dbMan.SqlStatement = _dbMan.SqlStatement + " p.activity <> 1 and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'   order by cont desc) a,\r\n ";
                        else
                            _dbMan.SqlStatement = _dbMan.SqlStatement + " p.activity <> 1 and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'   order by cmgt desc) a,\r\n ";



                        _dbMan.SqlStatement = _dbMan.SqlStatement + " (select WorkplaceID, prodmonth, sectionid, CalendarDate, WorkingDay, Sqm plansqm, BookSqm+isnull(AdjSqm,0) BookSqm, isnull(Grams,0) plancont, BookGrams+adjcont bookcont,ShiftDay,ProblemID\r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " from vw_Planning   where\r\n ";

                        _dbMan.SqlStatement = _dbMan.SqlStatement + " prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and plancode = 'MP' \r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " and Activity <> 1) b\r\n ";

                        _dbMan.SqlStatement = _dbMan.SqlStatement + " where a.workplaceid = b.workplaceid and a.prodmonth = b.prodmonth and a.Sectionid = b.sectionid\r\n ";



                        _dbMan.SqlStatement = _dbMan.SqlStatement + "union\r\n ";

                        _dbMan.SqlStatement = _dbMan.SqlStatement + "select * from (select top(" + NoTopPanels + ") groupa, workplaceid, description, prodmonth, SECTIONid, mosec, moname, fl, cont, sqmcmgt/(sqm+0.1) cmgt\r\n ";

                        _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select '2. Top " + NoTopPanels + " Crews' groupa, OrgUnitDay \r\n";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " workplaceid, p.CrewName description, p.prodmonth, p.SECTIONid, s2.SectionID mosec, s2.Name moname,\r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "sum(p.fl) fl,  convert(decimal(18,3),sum(kg)) cont, SUM(Sqm) sqm,  sUM(Sqm*cmgt) sqmcmgt  from vw_PlanMonth p, Section s, Section s1, Section s2, Workplace w\r\n  ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "where  p.Workplaceid = w.WorkplaceID and  p.Sectionid = s.sectionid and p.Prodmonth = s.prodmonth\r\n  ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "and  s.reporttoSectionid = s1.sectionid and s.Prodmonth = s1.prodmonth and plancode = 'LP' and   s1.reporttoSectionid = s2.sectionid\r\n  ";



                        _dbMan.SqlStatement = _dbMan.SqlStatement + "and s1.Prodmonth = s2.prodmonth and  p.activity <> 1 and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and p.OrgUnitDay <> '' group by p.OrgUnitDay,p.CrewName,\r\n  ";
                        if (reportSettings.Meas == "Gold")
                            _dbMan.SqlStatement = _dbMan.SqlStatement + "p.prodmonth, p.SECTIONid, s2.SectionID, s2.Name ) a order by cont desc) a,\r\n ";
                        else
                            _dbMan.SqlStatement = _dbMan.SqlStatement + "p.prodmonth, p.SECTIONid, s2.SectionID, s2.Name ) a order by sqmcmgt/(convert(numeric(10,2),sqm)+0.00001) desc) a,\r\n ";


                        _dbMan.SqlStatement = _dbMan.SqlStatement + "(select * from  ( \r\n ";


                        _dbMan.SqlStatement = _dbMan.SqlStatement + "select OrgUnitDay \r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " WorkplaceID, prodmonth, sectionid, CalendarDate, WorkingDay, \r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "sum(Sqm) plansqm, sum(BookSqm + isnull(adjsqm,0)) BookSqm, sum(isnull(Grams,0)) plancont, \r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "sum(BookGrams + adjcont) bookcont, max(ShiftDay) ShiftDay,ProblemID from ( \r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "   select a.*, b.OrgUnitDay from( \r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " select * from vw_Planning where plancode = 'LP' )a \r\n ";


                        _dbMan.SqlStatement = _dbMan.SqlStatement + " left outer join(Select Prodmonth, SectionID, Workplaceid, OrgUnitDay from Planmonth where plancode = 'LP' )b \r\n ";


                        _dbMan.SqlStatement = _dbMan.SqlStatement + "  on a.Prodmonth = b.Prodmonth \r\n ";


                        _dbMan.SqlStatement = _dbMan.SqlStatement + " and a.Workplaceid = b.Workplaceid \r\n ";


                        _dbMan.SqlStatement = _dbMan.SqlStatement + " and a.SectionID = b.SectionID)a \r\n ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " group by OrgUnitDay, prodmonth, sectionid, CalendarDate,WorkingDay, ProblemID \r\n ";




                        _dbMan.SqlStatement = _dbMan.SqlStatement + " ) b  ) b\r\n  ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "where a.workplaceid = b.workplaceid\r\n  ";
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "and a.prodmonth = b.prodmonth and a.Sectionid = b.sectionid\r\n ";





                        if (reportSettings.Meas == "Gold")
                            _dbMan.SqlStatement = _dbMan.SqlStatement + " order by groupa, Cont desc, b.workplaceid, b.CalendarDate\r\n ";
                        else
                            _dbMan.SqlStatement = _dbMan.SqlStatement + " order by groupa, cmgt desc, b.workplaceid, b.CalendarDate\r\n ";

                        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbMan.ResultsTableName = "Grapha";
                        _dbMan.ExecuteInstruction();

                        DataTable Data = new DataTable();







                    //Data.Columns.Count = 20;

                    ///////////Load Data into DataTable////////////////////////////

                    string Workplace = "";
                    int y = -1;
                    int PlanSqm = 0;
                    int ProgSqm = 0;
                    decimal PlanCont = 0;
                    decimal ProgCont = 0;

                    int BookSqm = 0;

                    decimal BookCont = 0;


                    int TotalShift = 0;
                    int col = 16;

                    for (int s = 0; s <= 62; s++)
                    {
                        Data.Columns.Add();
                    }




                    foreach (DataRow dr in _dbMan.ResultsDataTable.Rows)
                    {
                        // MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).Date.ToString());
                        // MessageBox.Show(DateTime.Now.Date.ToString());

                        if (Workplace != dr["Workplaceid"].ToString())
                        {
                            Data.Rows.Add();



                            y = y + 1;

                            Workplace = dr["Workplaceid"].ToString();

                            Data.Rows[y][0] = Workplace;  /////////Workplace Column
                            Data.Rows[y][61] = dr["groupa"].ToString();
                            Data.Rows[y][62] = (y + Convert.ToDecimal(1)).ToString();


                            Data.Rows[y][1] = dr["Description"].ToString();

                            Data.Rows[y][2] = dr["mosec"].ToString();

                            Data.Rows[y][3] = dr["moname"].ToString();

                            Data.Rows[y][4] = Math.Round(Convert.ToDecimal(dr["fl"].ToString()), 0).ToString();

                            //Data.Rows[y][5] = dr[""].ToString();

                            Data.Rows[y][6] = Math.Round(Convert.ToDecimal(dr["CMGT"].ToString()), 0).ToString();
                            Data.Rows[y][11] = 0;


                            PlanSqm = 0;
                            ProgSqm = 0;
                            PlanCont = 0;
                            ProgCont = 0;

                            BookSqm = 0;

                            BookCont = 0;


                            TotalShift = 0;
                            col = 16;

                            //////////////For first time/////////////////////


                            if (Convert.ToDateTime(dr["CalendarDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                            {

                                Data.Rows[y][7] = dr["ShiftDay"].ToString();

                                Data.Rows[y][8] = dr["Plansqm"].ToString(); // Actual Call
                            }
                            else
                            {
                                Data.Rows[y][7] = "";

                                Data.Rows[y][8] = "";
                            }

                            int zz = 0;

                            //string a = dr["CalendarDate"].ToString();

                            //if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date > DateTime.Now.Date.AddDays(-3))
                            //{
                            //    a = dr["CalendarDate"].ToString();
                            //}

                            //MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).ToString());
                            if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date < DateTime.Now.Date.AddDays(0))
                            {
                                // string a = dr["CalendarDate"].ToString();

                                ProgSqm = ProgSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                                Data.Rows[y][9] = ProgSqm.ToString();
                                ProgCont = ProgCont + Convert.ToDecimal(dr["plancont"].ToString());

                                Data.Rows[y][10] = ProgCont.ToString();

                                if (Data.Rows[y][11] == "")
                                    Data.Rows[y][11] = 0;

                                if (dr["BookSqm"] != DBNull.Value)
                                {
                                    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                                    Data.Rows[y][11] = BookSqm.ToString();
                                }
                                if (dr["BookCont"] != DBNull.Value)
                                {
                                    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                                    Data.Rows[y][12] = BookCont.ToString();
                                }
                            }

                            PlanSqm = PlanSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                            Data.Rows[y][13] = PlanSqm.ToString();

                            //if (dr["BookSqm"] != DBNull.Value)
                            //{
                            //    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                            //    Data.Rows[y][14] = BookSqm.ToString();
                            //}

                            PlanCont = PlanCont + Convert.ToDecimal(dr["plancont"].ToString());

                            Data.Rows[y][14] = BookSqm.ToString();

                            //if (dr["BookCont"] != DBNull.Value)
                            //{
                            //    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                            //    Data.Rows[y][16] = BookCont.ToString();
                            //}
                            if (dr["WorkingDay"].ToString() != "N")
                            {

                                TotalShift = TotalShift + 1;
                            }

                            Data.Rows[y][15] = TotalShift.ToString();


                            Span = Convert.ToDateTime(dr["calendardate"].ToString()).Subtract(BeginDate);

                            col = Convert.ToInt32(Span.Days) + 16;


                            if (dr["BookSqm"] != DBNull.Value)
                            {
                                if (dr["ProblemID"].ToString() == "")
                                {
                                    if (col >= 15)
                                        Data.Rows[y][col] = dr["BookSqm"].ToString();
                                }
                                else
                                {
                                    if (dr["ProblemID"] != DBNull.Value)
                                    {
                                        if (col >= 15)
                                            if (Convert.ToDecimal(dr["BookSqm"].ToString()) > 0)
                                                Data.Rows[y][col] = Convert.ToInt32(dr["BookSqm"].ToString());
                                            else
                                                Data.Rows[y][col] = dr["ProblemID"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                if (col >= 15)
                                    Data.Rows[y][col] = "";
                            }
                            col++;



                        }
                        ////////////////////////////////////////////////


                        else
                        {


                            if (Convert.ToDateTime(dr["CalendarDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                            {

                                Data.Rows[y][7] = dr["ShiftDay"].ToString();

                                Data.Rows[y][8] = dr["Plansqm"].ToString(); // Actual Call
                            }


                            if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date < DateTime.Now.Date.AddDays(0))
                            {
                                ProgSqm = ProgSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                                Data.Rows[y][9] = ProgSqm.ToString();
                                ProgCont = ProgCont + Convert.ToDecimal(dr["plancont"].ToString());

                                Data.Rows[y][10] = ProgCont.ToString();

                                if (Data.Rows[y][11] == "")
                                    Data.Rows[y][11] = 0;

                                if (dr["BookSqm"] != DBNull.Value)
                                {
                                    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                                    Data.Rows[y][11] = BookSqm.ToString();
                                }
                                if (dr["BookCont"] != DBNull.Value)
                                {
                                    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                                    Data.Rows[y][12] = BookCont.ToString();
                                }
                            }

                            PlanSqm = PlanSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                            Data.Rows[y][13] = PlanSqm.ToString();

                            //if (dr["BookSqm"] != DBNull.Value)
                            //{
                            //    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                            //    Data.Rows[y][14] = BookSqm.ToString();
                            //}

                            PlanCont = PlanCont + Convert.ToDecimal(dr["plancont"].ToString());

                            Data.Rows[y][14] = PlanCont.ToString();

                            //if (dr["BookCont"] != DBNull.Value)
                            //{
                            //    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                            //    Data.Rows[y][16] = BookCont.ToString();
                            //}
                            if (dr["WorkingDay"].ToString() != "N")
                            {

                                TotalShift = TotalShift + 1;
                            }

                            Data.Rows[y][15] = TotalShift.ToString();

                            Span = Convert.ToDateTime(dr["calendardate"].ToString()).Subtract(BeginDate);

                            col = Convert.ToInt32(Span.Days) + 16;

                            //if (col > 50)
                            //    col = 50;


                            if (dr["BookSqm"] != DBNull.Value)
                            {
                                if (dr["ProblemID"].ToString() == "")
                                {
                                    if (col >= 16)
                                        Data.Rows[y][col] = dr["BookSqm"].ToString();
                                }
                                else
                                {
                                    if (dr["ProblemID"] != DBNull.Value)
                                    {
                                        if (col >= 16)
                                            if (Convert.ToDecimal(dr["BookSqm"].ToString()) > 0)
                                                Data.Rows[y][col] = Convert.ToInt32(dr["BookSqm"].ToString());
                                            else
                                                Data.Rows[y][col] = dr["ProblemID"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                if (col >= 16)
                                    Data.Rows[y][col] = "";
                            }
                            col++;
                            //y++;

                        }

                    }

                    Data.TableName = "Data";
                    DataSet ReportData = new DataSet();
                    ReportData.Tables.Add(Data);

                    theReport.RegisterData(ReportData);



                    MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "select * from (select * from \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "(Select '' HR_Rank, p.workplaceid, W.description, p.prodmonth, p.SECTIONid, s2.SectionID mosec, s2.Name moname, \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "p.fl, '' cont, cmgt   from PlanMonth p, Section s, Section s1, Section s2, Workplace w \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "where  p.Workplaceid = w.WorkplaceID and  p.Sectionid = s.sectionid and p.Prodmonth = s.prodmonth and \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "s.reporttoSectionid = s1.sectionid and s.Prodmonth = s1.prodmonth and  s1.reporttoSectionid = s2.sectionid \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "and s1.Prodmonth = s2.prodmonth and  p.activity <> 1 and p.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' ) a where HR_Rank < 6) a, \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "(select WorkplaceID, prodmonth, sectionid, CalendarDate, WorkingDay, Sqm plansqm, BookSqm+adjsqm BookSqm, \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "grams plancont, BookGrams+adjcont bookcont,ShiftDay  from vw_Planning   where  prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' and prodmonth = 0  \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "and Activity <> 1) b  where a.workplaceid = b.workplaceid and a.prodmonth = b.prodmonth \r\n";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and a.Sectionid = b.sectionid  order by Cont desc, b.workplaceid, b.CalendarDate \r\n";
                    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan1.ResultsTableName = "Banner";
                    _dbMan1.ExecuteInstruction();

                    DataTable Data1 = new DataTable();



                    Workplace = "";
                    y = -1;
                    PlanSqm = 0;
                    ProgSqm = 0;
                    PlanCont = 0;
                    ProgCont = 0;

                    BookSqm = 0;

                    BookCont = 0;


                    TotalShift = 0;
                    col = 16;


                    for (int s = 0; s <= 60; s++)
                    {
                        Data1.Columns.Add();
                    }



                    foreach (DataRow dr in _dbMan1.ResultsDataTable.Rows)
                    {
                        // MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).Date.ToString());
                        // MessageBox.Show(DateTime.Now.Date.ToString());

                        if (Workplace != dr["Workplaceid"].ToString())
                        {
                            Data1.Rows.Add();
                            y = y + 1;

                            Workplace = dr["Workplaceid"].ToString();

                            Data1.Rows[y][0] = Workplace;  /////////Workplace Column

                            Data1.Rows[y][1] = dr["Description"].ToString();

                            Data1.Rows[y][2] = dr["mosec"].ToString();

                            Data1.Rows[y][3] = dr["moname"].ToString();

                            Data1.Rows[y][4] = Math.Round(Convert.ToDecimal(dr["fl"].ToString()), 0).ToString();

                            //Data.Rows[y][5] = dr[""].ToString();

                            Data1.Rows[y][6] = Math.Round(Convert.ToDecimal(dr["CMGT"].ToString()), 0).ToString();
                            Data1.Rows[y][11] = 0;


                            PlanSqm = 0;
                            ProgSqm = 0;
                            PlanCont = 0;
                            ProgCont = 0;

                            BookSqm = 0;

                            BookCont = 0;


                            TotalShift = 0;
                            col = 16;

                            //////////////For first time/////////////////////



                            if (Convert.ToDateTime(dr["CalendarDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                            {

                                Data1.Rows[y][7] = dr["ShiftDay"].ToString();

                                Data1.Rows[y][8] = dr["Plansqm"].ToString(); // Actual Call
                            }
                            else
                            {
                                Data1.Rows[y][7] = "";

                                Data1.Rows[y][8] = "";
                            }

                            int zz = 0;

                            //MessageBox.Show(Convert.ToDateTime(dr["CalendarDate"].ToString()).ToString());
                            if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date <= DateTime.Now.Date)
                            {


                                ProgSqm = ProgSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                                Data1.Rows[y][9] = ProgSqm.ToString();
                                ProgCont = ProgCont + Convert.ToDecimal(dr["plancont"].ToString());

                                Data1.Rows[y][10] = ProgCont.ToString();

                                if (Data1.Rows[y][11] == "")
                                    Data1.Rows[y][11] = 0;

                                if (dr["BookSqm"] != DBNull.Value)
                                {
                                    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                                    Data1.Rows[y][11] = BookSqm.ToString();
                                }
                                if (dr["BookCont"] != DBNull.Value)
                                {
                                    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                                    Data1.Rows[y][12] = BookCont.ToString();
                                }
                            }

                            PlanSqm = PlanSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                            Data1.Rows[y][13] = PlanSqm.ToString();

                            //if (dr["BookSqm"] != DBNull.Value)
                            //{
                            //    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                            //    Data.Rows[y][14] = BookSqm.ToString();
                            //}

                            PlanCont = PlanCont + Convert.ToDecimal(dr["plancont"].ToString());

                            Data1.Rows[y][14] = BookSqm.ToString();

                            //if (dr["BookCont"] != DBNull.Value)
                            //{
                            //    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                            //    Data.Rows[y][16] = BookCont.ToString();
                            //}
                            if (dr["WorkingDay"].ToString() != "N")
                            {

                                TotalShift = TotalShift + 1;
                            }

                            Data1.Rows[y][15] = TotalShift.ToString();




                            if (dr["BookSqm"] != DBNull.Value)
                            {
                                if (dr["ProblemID"].ToString() == "")
                                {

                                    Data.Rows[y][col] = Convert.ToInt32(dr["BookSqm"].ToString());
                                }
                                else
                                {
                                    if (dr["ProblemID"] != DBNull.Value)
                                    {
                                        if (Convert.ToDecimal(dr["BookSqm"].ToString()) > 0)
                                            Data.Rows[y][col] = dr["BookSqm"].ToString();
                                        else
                                            Data.Rows[y][col] = dr["ProblemID"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                Data.Rows[y][col] = "";
                            }
                            col++;

                        }
                        ////////////////////////////////////////////////


                        else
                        {


                            if (Convert.ToDateTime(dr["CalendarDate"].ToString()).ToShortDateString() == DateTime.Now.ToShortDateString())
                            {

                                Data1.Rows[y][7] = dr["ShiftDay"].ToString();

                                Data1.Rows[y][8] = dr["Plansqm"].ToString(); // Actual Call
                            }


                            if (Convert.ToDateTime(dr["CalendarDate"].ToString()).Date <= DateTime.Now.Date)
                            {
                                ProgSqm = ProgSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                                Data1.Rows[y][9] = ProgSqm.ToString();
                                ProgCont = ProgCont + Convert.ToDecimal(dr["plancont"].ToString());

                                Data1.Rows[y][10] = ProgCont.ToString();

                                if (Data1.Rows[y][11] == "")
                                    Data1.Rows[y][11] = 0;

                                if (dr["BookSqm"] != DBNull.Value)
                                {
                                    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                                    Data1.Rows[y][11] = BookSqm.ToString();
                                }
                                if (dr["BookCont"] != DBNull.Value)
                                {
                                    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                                    Data1.Rows[y][12] = BookCont.ToString();
                                }
                            }

                            PlanSqm = PlanSqm + Convert.ToInt32(dr["Plansqm"].ToString());

                            Data1.Rows[y][13] = PlanSqm.ToString();

                            //if (dr["BookSqm"] != DBNull.Value)
                            //{
                            //    BookSqm = BookSqm + Convert.ToInt32(dr["BookSqm"].ToString());

                            //    Data.Rows[y][14] = BookSqm.ToString();
                            //}

                            PlanCont = PlanCont + Convert.ToDecimal(dr["plancont"].ToString());

                            Data1.Rows[y][14] = PlanCont.ToString();

                            //if (dr["BookCont"] != DBNull.Value)
                            //{
                            //    BookCont = BookCont + Convert.ToDecimal(dr["BookCont"].ToString());

                            //    Data.Rows[y][16] = BookCont.ToString();
                            //}
                            if (dr["WorkingDay"].ToString() != "N")
                            {

                                TotalShift = TotalShift + 1;
                            }

                            Data1.Rows[y][15] = TotalShift.ToString();




                            if (dr["BookSqm"] != DBNull.Value)
                            {
                                if (dr["ProblemID"].ToString() == "")
                                {

                                    Data.Rows[y][col] = dr["BookSqm"].ToString();
                                }
                                else
                                {
                                    if (dr["ProblemID"] != DBNull.Value)
                                    {
                                        if (Convert.ToDecimal(dr["BookSqm"].ToString()) > 0)
                                            Data.Rows[y][col] = dr["BookSqm"].ToString();
                                        else
                                            Data.Rows[y][col] = dr["ProblemID"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                Data.Rows[y][col] = "";
                            }
                            col++;
                            //y++;

                        }

                    }


                    Data1.TableName = "Data1";
                    DataSet ReportData1 = new DataSet();
                    ReportData1.Tables.Add(Data1);


                    theReport.RegisterData(ReportData1);
                }

            }






            theReport.Load(TGlobalItems.ReportsFolder + "\\Top20.frx");
            //theReport.Design();

            theReport.Prepare();
            theReport.Refresh();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;
        }
            //theReport.Design();



            if (reportSettings.ReportType == "1")
            {
                Procedures pProc = new Procedures();
                DataSet dsTopPanels = new DataSet();
                StringBuilder sb = new StringBuilder();
                FastReport.Report theReport = new FastReport.Report();
                Procedures procs = new Procedures();

                int nProdMonth = Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
                string strSectionID = procs.ExtractBeforeColon(reportSettings.SectionID);
                int nSection = 1;

                if (pProc.ExtractBeforeColon(reportSettings.SectionID) != "1")
                    nSection = 4;

                #region Database Call

                DateTime? dtStart = null;
                DateTime? dtEnd = null;

                #region Get Start & End Date

                if (!dtStart.HasValue && !dtEnd.HasValue)
                {

                    MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan1.SqlStatement = "select MIN(BeginDate) as 'StartDate', MAX(EndDate) as 'EndDate' from SECCAL where Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'";
                    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan1.ExecuteInstruction();

                    if (_dbMan1.ResultsDataTable.Rows.Count != 0)
                    {
                        dtStart = Convert.ToDateTime(_dbMan1.ResultsDataTable.Rows[0]["StartDate"]);
                        dtEnd = Convert.ToDateTime(_dbMan1.ResultsDataTable.Rows[0]["EndDate"]);
                    }
                }

                #endregion

                DateTime dtCurrent = dtStart.Value;
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                SqlConnection pConn = new SqlConnection(_dbMan.ConnectionString);
                SqlCommand pCmd = new SqlCommand();
                pCmd.CommandType = CommandType.Text;
                pCmd.Connection = pConn;

                #region Top Panels

                sb.AppendLine();
                sb.AppendLine("-- Top Panels");
                sb.AppendLine(string.Format("exec Report_TopPanels '{0}', '{1}', {2}", nProdMonth, strSectionID, nSection));

                #endregion


                pCmd.CommandText = sb.ToString();

                pConn.Open();
                SqlDataAdapter pAdap = new SqlDataAdapter(pCmd);
                pAdap.Fill(dsTopPanels);
                pConn.Close();

                if (dsTopPanels.Tables.Count == 0)
                {
                    MessageBox.Show("No Top Panels found for selected search criteria. Please try again.", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // return;
                    theReport.Prepare();
                    theReport.Refresh();
                    ActiveReport.SetReport = theReport;
                    ActiveReport.isDone = true;
                    return;
                }
                else
                {
                    if (dsTopPanels.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("No Top Panels found for selected search criteria. Please try again.", "No Data Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // return;
                        theReport.Prepare();
                        theReport.Refresh();
                        ActiveReport.SetReport = theReport;
                        ActiveReport.isDone = true;
                        return;
                    }
                }

                dsTopPanels.Tables[0].TableName = "TopPanels";

                #endregion

                #region Totals

                DataTable tblTotals = new DataTable("Totals");


                tblTotals.Columns.Add("MonthlyPlanningSQM");
                tblTotals.Columns.Add("MonthlyPlanningCMGT");
                tblTotals.Columns.Add("MonthlyPlanningGT");
                tblTotals.Columns.Add("MonthlyPlanningKGs");
                tblTotals.Columns.Add("ProgressGradeActualCMGT");
                tblTotals.Columns.Add("ProgressGradeActualGT");
                tblTotals.Columns.Add("ProgressGradeVarCMGT");
                tblTotals.Columns.Add("ProgressGradeVarGT");
                tblTotals.Columns.Add("ProgressivePlannedSQM");
                tblTotals.Columns.Add("ProgressiveActualSQM");
                tblTotals.Columns.Add("ProgressiveVarSQM");
                tblTotals.Columns.Add("ProgressivePlannedKG");
                tblTotals.Columns.Add("ProgressiveActualKG");
                tblTotals.Columns.Add("ProgressiveVarKG");
                tblTotals.Columns.Add("GradePerformance");
                tblTotals.Columns.Add("SQMPerformance");
                tblTotals.Columns.Add("KGPerformance");


                decimal fMonthlyPlanningSQM = 0.0m;
                decimal fMonthlyPlanningCMGT = 0.0m;
                decimal fMonthlyPlanningGT = 0.0m;
                decimal fMonthlyPlanningKGs = 0.0m;
                decimal fProgressGradeActualCMGT = 0.0m;
                decimal fProgressGradeActualGT = 0.0m;
                decimal fProgressGradeVarCMGT = 0.0m;
                decimal fProgressGradeVarGT = 0.0m;
                decimal fProgressivePlannedSQM = 0.0m;
                decimal fProgressiveActualSQM = 0.0m;
                decimal fProgressiveVarSQM = 0.0m;
                decimal fProgressivePlannedKG = 0.0m;
                decimal fProgressiveActualKG = 0.0m;
                decimal fProgressiveVarKG = 0.0m;

                decimal fMonthlyPlanningSQMSUM = 0.0m;
                decimal fMonthlyPlanningCMGTSUM = 0.0m;
                decimal fMonthlyPlanningGTSUM = 0.0m;
                decimal fMonthlyPlanningKGsSUM = 0.0m;
                decimal fProgressGradeActualCMGTSUM = 0.0m;
                decimal fProgressGradeActualGTSUM = 0.0m;
                decimal fProgressGradeVarCMGTSUM = 0.0m;
                decimal fProgressGradeVarGTSUM = 0.0m;
                decimal fProgressivePlannedSQMSUM = 0.0m;
                decimal fProgressiveActualSQMSUM = 0.0m;
                decimal fProgressiveVarSQMSUM = 0.0m;
                decimal fProgressivePlannedKGSUM = 0.0m;
                decimal fProgressiveActualKGSUM = 0.0m;
                decimal fProgressiveVarKGSUM = 0.0m;

                foreach (DataRow dr in dsTopPanels.Tables["TopPanels"].Rows)
                {
                    #region Sum

                    if (dr["MonthlyPlanningSQM"] != DBNull.Value)
                        fMonthlyPlanningSQMSUM += Convert.ToDecimal(dr["MonthlyPlanningSQM"]);

                    if (dr["MonthlyPlanningCMGT"] != DBNull.Value)
                        fMonthlyPlanningCMGTSUM += Convert.ToDecimal(dr["MonthlyPlanningCMGT"]);
                    if (dr["MonthlyPlanningGT"] != DBNull.Value)
                        fMonthlyPlanningGTSUM += Convert.ToDecimal(dr["MonthlyPlanningGT"]);

                    if (dr["MonthlyPlanningKGs"] != DBNull.Value)
                        fMonthlyPlanningKGsSUM += Convert.ToDecimal(dr["MonthlyPlanningKGs"]);

                    if (dr["ProgressGradeActualCMGT"] != DBNull.Value)
                        fProgressGradeActualCMGTSUM += Convert.ToDecimal(dr["ProgressGradeActualCMGT"]);
                    if (dr["ProgressGradeActualGT"] != DBNull.Value)
                        fProgressGradeActualGTSUM += Convert.ToDecimal(dr["ProgressGradeActualGT"]);
                    if (dr["ProgressGradeVarCMGT"] != DBNull.Value)
                        fProgressGradeVarCMGTSUM += Convert.ToDecimal(dr["ProgressGradeVarCMGT"]);
                    if (dr["ProgressGradeVarGT"] != DBNull.Value)
                        fProgressGradeVarGTSUM += Convert.ToDecimal(dr["ProgressGradeVarGT"]);

                    if (dr["ProgressivePlannedSQM"] != DBNull.Value)
                        fProgressivePlannedSQMSUM += Convert.ToDecimal(dr["ProgressivePlannedSQM"]);
                    if (dr["ProgressiveActualSQM"] != DBNull.Value)
                        fProgressiveActualSQMSUM += Convert.ToDecimal(dr["ProgressiveActualSQM"]);
                    if (dr["ProgressiveVarSQM"] != DBNull.Value)
                        fProgressiveVarSQMSUM += Convert.ToDecimal(dr["ProgressiveVarSQM"]);

                    if (dr["ProgressivePlannedKG"] != DBNull.Value)
                        fProgressivePlannedKGSUM += Convert.ToDecimal(dr["ProgressivePlannedKG"]);
                    if (dr["ProgressiveActualKG"] != DBNull.Value)
                        fProgressiveActualKGSUM += Convert.ToDecimal(dr["ProgressiveActualKG"]);
                    if (dr["ProgressiveVarKG"] != DBNull.Value)
                        fProgressiveVarKGSUM += Convert.ToDecimal(dr["ProgressiveVarKG"]);

                    #endregion

                    if (dr["MonthlyPlanningSQM"] != DBNull.Value)
                        fMonthlyPlanningSQM += Convert.ToDecimal(dr["MonthlyPlanningSQM"]);

                    if (dr["MonthlyPlanningCMGT"] != DBNull.Value && dr["ProgressivePlannedSQM"] != DBNull.Value)
                        fMonthlyPlanningCMGT += Convert.ToDecimal(dr["MonthlyPlanningCMGT"]) * Convert.ToDecimal(dr["ProgressivePlannedSQM"]);


                    if (dr["MonthlyPlanningGT"] != DBNull.Value && dr["ProgressivePlannedSQM"] != DBNull.Value)
                        fMonthlyPlanningGT += Convert.ToDecimal(dr["MonthlyPlanningGT"]) * Convert.ToDecimal(dr["ProgressivePlannedSQM"]);

                    if (dr["MonthlyPlanningKGs"] != DBNull.Value)
                        fMonthlyPlanningKGs += Convert.ToDecimal(dr["MonthlyPlanningKGs"]);

                    if (dr["ProgressGradeActualGT"] != DBNull.Value
                        && dr["ProgressiveActualSQM"] != DBNull.Value)
                        fProgressGradeActualGT += Convert.ToDecimal(dr["ProgressGradeActualGT"]) * Convert.ToDecimal(dr["ProgressiveActualSQM"]);
                    //fProgressGradeVarCMGT += Convert.ToDecimal(dr["ProgressGradeVarCMGT"]);
                    //fProgressGradeVarGT += Convert.ToDecimal(dr["ProgressGradeVarGT"]);

                    if (dr["ProgressivePlannedSQM"] != DBNull.Value)
                        fProgressivePlannedSQM += Convert.ToDecimal(dr["ProgressivePlannedSQM"]);
                    if (dr["ProgressiveActualSQM"] != DBNull.Value)
                        fProgressiveActualSQM += Convert.ToDecimal(dr["ProgressiveActualSQM"]);
                    if (dr["ProgressiveVarSQM"] != DBNull.Value)
                        fProgressiveVarSQM += Convert.ToDecimal(dr["ProgressiveVarSQM"]);

                    if (dr["ProgressivePlannedKG"] != DBNull.Value)
                        fProgressivePlannedKG += Convert.ToDecimal(dr["ProgressivePlannedKG"]);
                    if (dr["ProgressiveActualKG"] != DBNull.Value)
                        fProgressiveActualKG += Convert.ToDecimal(dr["ProgressiveActualKG"]);
                    if (dr["ProgressiveVarKG"] != DBNull.Value)
                        fProgressiveVarKG += Convert.ToDecimal(dr["ProgressiveVarKG"]);
                }


                if (fProgressivePlannedSQMSUM != 0)
                    fMonthlyPlanningCMGT = fMonthlyPlanningCMGT / fProgressivePlannedSQMSUM;
                if (fProgressivePlannedSQMSUM != 0)
                    fMonthlyPlanningGT = fMonthlyPlanningGT / fProgressivePlannedSQMSUM;

                if (fProgressiveActualSQMSUM != 0)
                    fProgressGradeActualCMGT = fProgressiveActualKGSUM / fProgressiveActualSQMSUM / 0.0278m * 1000;
                if (fProgressiveActualSQMSUM != 0)
                    fProgressGradeActualGT = fProgressGradeActualGT / fProgressiveActualSQMSUM;
                if (fProgressGradeActualCMGT != 0)
                    fProgressGradeVarCMGT = fMonthlyPlanningCMGT - fProgressGradeActualCMGT;
                if (fProgressGradeActualGT != 0)
                    fProgressGradeVarGT = fMonthlyPlanningGT - fProgressGradeActualGT;

                DataRow drTotals = tblTotals.NewRow();

                drTotals["MonthlyPlanningSQM"] = DisplayFmt.CustomDecimal(fMonthlyPlanningSQM, 0);
                drTotals["MonthlyPlanningCMGT"] = DisplayFmt.CustomDecimal(fMonthlyPlanningCMGT, 0);
                drTotals["MonthlyPlanningGT"] = DisplayFmt.CustomDecimal(fMonthlyPlanningGT, 2);
                drTotals["MonthlyPlanningKGs"] = DisplayFmt.CustomDecimal(fMonthlyPlanningKGs, 3);
                drTotals["ProgressGradeActualCMGT"] = DisplayFmt.CustomDecimal(fProgressGradeActualCMGT, 0);
                drTotals["ProgressGradeActualGT"] = DisplayFmt.CustomDecimal(fProgressGradeActualGT, 2);
                drTotals["ProgressGradeVarCMGT"] = DisplayFmt.CustomDecimal(fProgressGradeVarCMGT, 0);
                drTotals["ProgressGradeVarGT"] = DisplayFmt.CustomDecimal(fProgressGradeVarGT, 2);
                drTotals["ProgressivePlannedSQM"] = DisplayFmt.CustomDecimal(fProgressivePlannedSQM, 0);
                drTotals["ProgressiveActualSQM"] = DisplayFmt.CustomDecimal(fProgressiveActualSQM, 0);
                drTotals["ProgressiveVarSQM"] = DisplayFmt.CustomDecimal(fProgressiveVarSQM, 0);
                drTotals["ProgressivePlannedKG"] = DisplayFmt.CustomDecimal(fProgressivePlannedKG, 3);
                drTotals["ProgressiveActualKG"] = DisplayFmt.CustomDecimal(fProgressiveActualKG, 3);
                drTotals["ProgressiveVarKG"] = DisplayFmt.CustomDecimal(fProgressiveVarKG, 3);

                drTotals["GradePerformance"] = 0.0m;
                drTotals["SQMPerformance"] = 0.0m;
                drTotals["KGPerformance"] = 0.0m;

                if (fMonthlyPlanningCMGT != 0)
                    drTotals["GradePerformance"] = DisplayFmt.PercSmall_WithSign(fProgressGradeActualCMGT / fMonthlyPlanningCMGT);
                if (fProgressivePlannedSQM != 0)
                    drTotals["SQMPerformance"] = DisplayFmt.PercSmall_WithSign(fProgressiveActualSQM / fProgressivePlannedSQM);
                if (fProgressivePlannedKG != 0)
                    drTotals["KGPerformance"] = DisplayFmt.PercSmall_WithSign(fProgressiveActualKG / fProgressivePlannedKG);

                tblTotals.Rows.Add(drTotals);
                tblTotals.AcceptChanges();



                dsTopPanels.Tables.Add(tblTotals);

                #endregion


                #region Custom Data

                DataTable tblCustom = new DataTable("Custom");
                tblCustom.Columns.Add("Banner");
                tblCustom.Columns.Add("Month");
                tblCustom.Columns.Add("Shaft");
                tblCustom.Columns.Add("Section");

                DateTime dtMonth = new DateTime(Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth).Substring(0, 4)),
                    Convert.ToInt32(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth).Substring(4, 2)), 1);
                SysSettings aa = new SysSettings();
                aa.systemDBTag = theSystemDBTag;
                aa.connection = UserCurrentInfo.Connection;
                aa.GetSysSettings();
                DataRow drCustom = tblCustom.NewRow();
                drCustom["Banner"] = "PERFORMANCE OF TOP PANELS";
                drCustom["Month"] = dtMonth.ToString("MMM-yy");
                drCustom["Shaft"] = SysSettings.Banner;
                drCustom["Section"] = reportSettings.SectionID;
                tblCustom.Rows.Add(drCustom);
                tblCustom.AcceptChanges();

                theReport.RegisterData(tblCustom, "CustomData");

                #endregion

                theReport.RegisterData(dsTopPanels);

                theReport.Load(TGlobalItems.ReportsFolder + "\\TopPanelsReport.frx");


                //if (MessageBox.Show("Design?", "Design?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                //    theReport.Design();

                theReport.Prepare();
                theReport.Refresh();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
                //theReport.Design();
            }
        }

        private void ucTopPanelsReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            // reportSettings.ReportDate = DateTime.Now;
            //  DateTime dt = new DateTime();
            //  reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "(select currentproductionmonth from sysset)";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            DataTable dt = new DataTable();
            dt = _dbMan.ResultsDataTable;
            foreach (DataRow dr in dt.Rows)
            {
                reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(dr["currentproductionmonth"].ToString());
            }
            // reportSettings.Prodmonth = Convert.ToInt32(TProductionGlobal.ProdMonthAsString(dt));
            iProdmonth.Properties.Value = reportSettings.Prodmonth;
            LoadSections();
            reportSettings.SectionID = dtSections.Rows[0][0].ToString();
         pgTopPanelsRepSettings.SelectedObject = reportSettings;

            reportSettings.Type = "Dynamic";
            reportSettings.Meas = "Gold";
            iType.Properties.Value = reportSettings.Type;
            iMeas.Properties.Value = reportSettings.Meas;


            reportSettings.ReportType = "0";
            iBottomFilter.Properties.Value = reportSettings.ReportType;
        }

        private void riProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            pgTopPanelsRepSettings.PostEditor();
            LoadSections();
        }

        private void pgTopPanelsRepSettings_Click(object sender, EventArgs e)
        {

        }

        private void T20RG_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
