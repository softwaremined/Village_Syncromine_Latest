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
using Mineware.Systems.HarmonyPASGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.HarmonyPASReports.TopPanelsReport
{
    public partial class ucTopPanelsReport : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
       
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
            _dbMan.SqlStatement = " Select SectionID+':'+Name SectionID " +
                                  "from Section s where s.Prodmonth =  '" + THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' ";

            _dbMan.SqlStatement += " and Hierarchicalid  <= 4 ";
            _dbMan.SqlStatement += " order by SectionID   ";

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
            Procedures pProc = new Procedures();
            DataSet dsTopPanels = new DataSet();
            StringBuilder sb = new StringBuilder();
            FastReport.Report theReport = new FastReport.Report();
            Procedures procs = new Procedures();

            int nProdMonth = Convert .ToInt32 (THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth));
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
                _dbMan1.SqlStatement = "select MIN(BeginDate) as 'StartDate', MAX(EndDate) as 'EndDate' from SECCAL where Prodmonth = '" + THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'";
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
             
            DateTime dtMonth = new DateTime(Convert.ToInt32(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth).Substring(0, 4)),
                Convert.ToInt32(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth).Substring(4, 2)), 1);
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

        private void ucTopPanelsReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            // reportSettings.ReportDate = DateTime.Now;
            //  DateTime dt = new DateTime();
            //  reportSettings.Prodmonth = THarmonyPASGlobal.ProdMonthAsDate(THarmonyPASGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
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
                reportSettings.Prodmonth = THarmonyPASGlobal.ProdMonthAsDate(dr["MillMonth"].ToString());
            }
            // reportSettings.Prodmonth = Convert.ToInt32(THarmonyPASGlobal.ProdMonthAsString(dt));
            iProdmonth.Properties.Value = reportSettings.Prodmonth;
            LoadSections();
            reportSettings.SectionID = dtSections.Rows[0][0].ToString();
         pgTopPanelsRepSettings   .SelectedObject = reportSettings;
        }

        private void riProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            pgTopPanelsRepSettings.PostEditor();
            LoadSections();
        }
    }
}
