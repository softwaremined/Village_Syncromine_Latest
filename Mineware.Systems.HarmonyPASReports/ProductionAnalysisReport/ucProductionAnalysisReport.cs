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

namespace Mineware.Systems.Reports.ProductionAnalysisReport
{
    public partial class ucProductionAnalysisReport : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        DataTable tblDays = new DataTable("NewDays");
        public string checking;
        bool blnForceBoxholeInPlanning = false;
        //private DataTable dtSections;
        DataTable dtSections = new DataTable();
        private clsProductionAnalysisRepSettings reportSettings = new clsProductionAnalysisRepSettings();
        protected decimal fPreviousMillMonth;
        private int procShft;
        DateTime dtToSelect;
        string minProd;
        string maxProd;
        private bool foundNoSection;
        private string procDateStart;
        private string procDateEnd;
        private string stDatePrev3;
        private string stDatePrev2;
        private string stDatePrev1;
        private string stDateNext3;
        private string stDateNext2;
        private string stDateNext1;
        private string endDatePrev3;
        private string endDatePrev2;
        private string endDatePrev1;
        private string endDateNext3;
        private string endDateNext2;
        private string endDateNext1;

        public ucProductionAnalysisReport()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
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
            if (reportSettings.ProdMonthSelection == "")
            {
              //  rdgrpDates.Focus();
                MessageBox.Show("Please Select Either 'Mill Month' or 'From-To Date'. \r\n ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (reportSettings.ProdMonthSelection == "FromToDate")
            {
                if (reportSettings .StartDate  > reportSettings .EndDate )
                {
                   // dteFromDate.Focus();
                    MessageBox.Show("From Date is Greater Than the To Date. \r\n ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(reportSettings .SectionID ))
            {
                MessageBox.Show("Please select a Section first.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StringBuilder sb = new StringBuilder();
            Procedures procs = new Procedures();

            bool bIsProgressiveReport = false;

            if (reportSettings .Type =="Progressive")
                bIsProgressiveReport = true;

            string strActivity = reportSettings.Activity;
            string strSectionID = procs.ExtractBeforeColon(reportSettings.SectionID);
            string strProdMonth = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);
            int nProdMonthYear = Convert.ToInt32(strProdMonth.Substring(0, 4));
            int nProdMonthMonth = Convert.ToInt32(strProdMonth.Substring(4, 2));
            DateTime dtProdMonth = new DateTime(nProdMonthYear, nProdMonthMonth, 1);
            string strHier = "";
            DataSet DataSetProdAnal = new DataSet();
            DateTime dtStart = DateTime.Now;

            #region SQL Database Call

            #region Hier

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbMan1.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            if (reportSettings.ProdMonthSelection == "Prodmonth")
            {
                sb.AppendLine("select top(1) Hierarchicalid from SECTION");
                sb.AppendLine("where SectionID = '" + strSectionID + "'");
                sb.AppendLine("and Prodmonth = '" + strProdMonth + "'");
            }
            else
            {
                sb.AppendLine(" select top(1) Hierarchicalid \r\n ");
                sb.AppendLine(" from section where SectionID = '" + strSectionID + "' AND prodmonth >= '" + minProd + "' and prodmonth <= '" + maxProd + "'  ");
            }

            _dbMan1.SqlStatement = sb.ToString();
            _dbMan1.ExecuteInstruction();

            strHier = _dbMan1.ResultsDataTable.Rows[0][0].ToString();



            #endregion

            #region Reports



            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataSet;
            string strFromDate = String.Format("{0:yyyy-MM-dd}", reportSettings.StartDate );
            string strToDate = String.Format("{0:yyyy-MM-dd}", reportSettings .EndDate );
            string strTypeReport = "M";
            if (reportSettings.ProdMonthSelection == "FromToDate")
            {
                try
                {
                    strTypeReport = "D";
                    get_Shifts(strSectionID, strProdMonth, strFromDate, strToDate, strHier);

                    DateTime aa = reportSettings.StartDate;
                    get_PrevDates(strSectionID, strHier, aa);
                    stDatePrev1 = procDateStart;
                    endDatePrev1 = procDateEnd;

                    aa = Convert.ToDateTime(stDatePrev1.ToString());
                    get_PrevDates(strSectionID, strHier, aa);
                    stDatePrev2 = procDateStart;
                    endDatePrev2 = procDateEnd;

                    aa = Convert.ToDateTime(stDatePrev2.ToString());
                    get_PrevDates(strSectionID, strHier, aa);
                    stDatePrev3 = procDateStart;
                    endDatePrev3 = procDateEnd;

                    aa = reportSettings.EndDate;
                    get_NextDates(strSectionID, strHier, aa);
                    stDateNext1 = procDateStart;
                    endDateNext1 = procDateEnd;

                    aa = Convert.ToDateTime(endDateNext1.ToString());
                    get_NextDates(strSectionID, strHier, aa);
                    stDateNext2 = procDateStart;
                    endDateNext2 = procDateEnd;

                    aa = Convert.ToDateTime(endDateNext2.ToString());
                    get_NextDates(strSectionID, strHier, aa);
                    stDateNext3 = procDateStart;
                    endDateNext3 = procDateEnd;
                }
                catch (Exception e)
                {
                    return;
                }
            }
            sb.Clear();
            try
            {
                if (reportSettings .Activity =="Stoping")
                {
                    #region Report_Production_Analysis_Stoping_SQM

                    sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                    sb.AppendLine();

                    if (reportSettings.ProdMonthSelection == "Prodmonth")
                    {
                        //parameters wat saak maar : dtDateToSelect.ToString("yyyyMM"), strSectionID, strHier, bIsProgressiveReport, strTypeReport
                        for (int i = -3; i <= -1; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }

                        for (int i = 1; i <= 3; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }
                    }
                    else
                    {
                        //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_SQM_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                    }

                    #endregion

                    #region Report_Production_Analysis_Stoping_KG

                    sb.AppendLine("exec Report_Production_Analysis_Stoping_KG '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                    sb.AppendLine();

                    if (reportSettings.ProdMonthSelection == "Prodmonth")
                    {
                        for (int i = -3; i <= -1; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_KG_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }

                        for (int i = 1; i <= 3; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_KG_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }
                    }
                    else
                    {
                        //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_KG_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_KG_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_KG_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_KG_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_KG_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_KG_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                    }
                    #endregion

                    #region Report_Production_Analysis_Stoping_GT

                    if (!bIsProgressiveReport)
                    {
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_GT '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                        sb.AppendLine();

                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                                sb.AppendLine("exec Report_Production_Analysis_Stoping_GT_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                                sb.AppendLine();
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                                sb.AppendLine("exec Report_Production_Analysis_Stoping_GT_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                                sb.AppendLine();
                            }
                        }
                        else
                        {
                            //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_GT_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_GT_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_GT_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_GT_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_GT_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_GT_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        }
                    }

                    #endregion

                    #region Report_Production_Analysis_Stoping_AdvancePerBlast

                    if (!bIsProgressiveReport)
                    {
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                        sb.AppendLine();

                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                                sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                                sb.AppendLine();
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                                sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                                sb.AppendLine();
                            }
                        }
                        else
                        {
                            //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        }
                    }

                    #endregion

                    #region Report_Production_Analysis_Stoping_Tons

                    sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                    sb.AppendLine();

                    if (reportSettings.ProdMonthSelection == "Prodmonth")
                    {
                        for (int i = -3; i <= -1; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }

                        for (int i = 1; i <= 3; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }
                    }
                    else
                    {
                        //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Stoping_Tons_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                    }

                    #endregion

                }

                if (reportSettings .Activity =="Development")
                {
                    #region Report_Production_Analysis_Development_MAdv

                    sb.AppendLine("exec Report_Production_Analysis_Development_MAdv '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                    sb.AppendLine();

                    if (reportSettings.ProdMonthSelection == "Prodmonth")
                    {
                        for (int i = -3; i <= -1; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Development_MAdv_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }

                        for (int i = 1; i <= 3; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Development_MAdv_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }
                    }
                    else
                    {
                        //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                        sb.AppendLine("exec Report_Production_Analysis_Development_MAdv_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_MAdv_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_MAdv_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_MAdv_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_MAdv_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_MAdv_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                    }


                    #endregion

                    #region Report_Production_Analysis_Development_NoOfBlasts

                    if (!bIsProgressiveReport)
                    {
                        sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                        sb.AppendLine();

                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                                sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                                sb.AppendLine();
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                                sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                                sb.AppendLine();
                            }
                        }
                        else
                        {
                            //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                            sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine("exec Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        }

                    }
                    #endregion

                    #region Report_Production_Analysis_Development_Tons

                    sb.AppendLine("exec Report_Production_Analysis_Development_Tons '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                    sb.AppendLine();

                    if (reportSettings.ProdMonthSelection == "Prodmonth")
                    {
                        for (int i = -3; i <= 3; i++)
                        {
                            if (i != 0)
                            {
                                sb.AppendLine("exec Report_Production_Analysis_Development_Tons_Monthly_Average '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                                sb.AppendLine();
                            }
                        }
                    }
                    else
                    {
                        //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                        sb.AppendLine("exec Report_Production_Analysis_Development_Tons_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_Tons_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_Tons_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_Tons_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_Tons_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Development_Tons_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");

                    }
                    #endregion

                }

                #region Report_Production_Analysis_Engineering_Breakdowns

                if (reportSettings .Activity =="Engineering")
                {

                    sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                    sb.AppendLine();
                    if (reportSettings.ProdMonthSelection == "Prodmonth")
                    {
                        for (int i = -3; i <= -1; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }

                        for (int i = 1; i <= 3; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }
                    }
                    else
                    {
                        //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                        sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                    }
                }

                #endregion

                #region Report_Production_Analysis_HR_Compliment

                if (reportSettings .Activity =="HR")
                {
                    sb.AppendLine("exec Report_Production_Analysis_HR_Compliment '" + strProdMonth + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + ", '" + strTypeReport + "' ");
                    sb.AppendLine();

                    if (reportSettings.ProdMonthSelection == "Prodmonth")
                    {
                        for (int i = -3; i <= -1; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_HR_Compliment_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }

                        for (int i = 1; i <= 3; i++)
                        {
                            DateTime dtDateToSelect = dtProdMonth.AddMonths(i);
                            sb.AppendLine("exec Report_Production_Analysis_HR_Compliment_Monthly_Average '" + dtDateToSelect.ToString("yyyyMM") + "', '" + strFromDate + "', '" + strToDate + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                            sb.AppendLine();
                        }
                    }
                    else
                    {
                        //parameters wat saak maar : strFromDate, strToDate, strSectionID, strHier, bIsProgressiveReport, strTypeReport, procShft                    
                        sb.AppendLine("exec Report_Production_Analysis_HR_Compliment_Monthly_Average '" + strProdMonth + "', '" + stDatePrev1 + "', '" + endDatePrev1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_HR_Compliment_Monthly_Average '" + strProdMonth + "', '" + stDatePrev2 + "', '" + endDatePrev2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_HR_Compliment_Monthly_Average '" + strProdMonth + "', '" + stDatePrev3 + "', '" + endDatePrev3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_HR_Compliment_Monthly_Average '" + strProdMonth + "', '" + stDateNext1 + "', '" + endDateNext1 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_HR_Compliment_Monthly_Average '" + strProdMonth + "', '" + stDateNext2 + "', '" + endDateNext2 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                        sb.AppendLine("exec Report_Production_Analysis_HR_Compliment_Monthly_Average '" + strProdMonth + "', '" + stDateNext3 + "', '" + endDateNext3 + "', '" + strSectionID + "', " + strHier + "," + bIsProgressiveReport + ", '" + strTypeReport + "', '" + procShft + "' ");
                    }
                }

                #endregion

                #endregion

                _dbMan.SqlStatement = sb.ToString();
                _dbMan.ExecuteInstruction();
            }
            catch (Exception e)
            {

            }
            #endregion


            #region DataTables + C# Coding

            FastReport.Report theReport = new FastReport.Report();

            theReport.Load(TGlobalItems.ReportsFolder + "\\ProductionAnalysisReport.frx");

            foreach (FastReport.ReportPage p in theReport.Pages)
            {
                p.Visible = false;
            }
            try
            {

                if (reportSettings .Activity =="Stoping")
                {
                    #region Report_Production_Analysis_Stoping_SQM

                    DataTable tblStopingSQM = new DataTable("StopingSQM");
                    int nCount = 1;
                    try
                    {
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblStopingSQM.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanP");
                                tblStopingSQM.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookP");
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblStopingSQM.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanF");
                                tblStopingSQM.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookF");
                                nCount++;
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                tblStopingSQM.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                tblStopingSQM.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookP");
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                tblStopingSQM.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                tblStopingSQM.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookF");
                                nCount++;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }

                    tblStopingSQM.Columns.Add("CalendarDate");
                    tblStopingSQM.Columns.Add("SQMPlanned");
                    tblStopingSQM.Columns.Add("SQMBooked");

                    foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[0].Rows)
                    {
                        nCount = 1;
                        DataRow drNew = tblStopingSQM.NewRow();
                        //MessageBox.Show(_dbMan.ResultsDataSet.Tables[nCount].Rows[0][0].ToString());
                        //MessageBox.Show(_dbMan.ResultsDataSet.Tables[nCount].Rows[0][1].ToString());
                        try
                        {
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMM") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMM") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                    nCount++;
                                }
                            }
                            else
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                    nCount++;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                        }
                        drNew["CalendarDate"] = dr["CalendarDate"];
                        drNew["SQMPlanned"] = dr["SQMPlanned"];
                        drNew["SQMBooked"] = dr["SQMBooked"];
                        tblStopingSQM.Rows.Add(drNew);
                    }


                    if (bIsProgressiveReport)
                    {
                        bool bFirst = true;
                        decimal fPlannedValue = 0.0m;
                        decimal fBookedValue = 0.0m;

                        foreach (DataRow dr in tblStopingSQM.Rows)
                        {
                            if (bFirst)
                            {
                                if (dr["SQMPlanned"] != DBNull.Value)
                                    fPlannedValue = Convert.ToDecimal(dr["SQMPlanned"]);
                                if (dr["SQMBooked"] != DBNull.Value)
                                    fBookedValue = Convert.ToDecimal(dr["SQMBooked"]);
                                bFirst = false;
                                continue;
                            }
                            if (dr["SQMPlanned"] != DBNull.Value)
                                fPlannedValue += Convert.ToDecimal(dr["SQMPlanned"]);
                            if (dr["SQMBooked"] != DBNull.Value)
                                fBookedValue += Convert.ToDecimal(dr["SQMBooked"]);
                            dr["SQMPlanned"] = fPlannedValue;
                            dr["SQMBooked"] = fBookedValue;
                        }
                    }


                    tblStopingSQM.AcceptChanges();

                    DataSetProdAnal.Tables.Add(tblStopingSQM);

                    theReport.Pages[0].Visible = true;

                    #endregion

                    #region Report_Production_Analysis_Stoping_KG

                    DataTable tblStopingKG = new DataTable("StopingKG");
                    nCount = 1;
                    try
                    {
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblStopingKG.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanP");
                                tblStopingKG.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookP");
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblStopingKG.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanF");
                                tblStopingKG.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookF");
                                nCount++;
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                tblStopingKG.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                tblStopingKG.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookP");
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                tblStopingKG.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                tblStopingKG.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookF");
                                nCount++;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }

                    tblStopingKG.Columns.Add("CalendarDate");
                    tblStopingKG.Columns.Add("Plan");
                    tblStopingKG.Columns.Add("Book");

                    foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[7].Rows)
                    {
                        nCount = 1;
                        DataRow drNew = tblStopingKG.NewRow();
                        try
                        {
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMM") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMM") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][1];
                                    nCount++;
                                }
                            }
                            else
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][1]; nCount++;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                        }
                        drNew["CalendarDate"] = dr["CalendarDate"];
                        drNew["Plan"] = dr["Plan"];
                        drNew["Book"] = dr["Book"];
                        tblStopingKG.Rows.Add(drNew);
                    }


                    if (bIsProgressiveReport)
                    {
                        bool bFirst = true;
                        decimal fPlannedValue = 0.0m;
                        decimal fBookedValue = 0.0m;

                        foreach (DataRow dr in tblStopingKG.Rows)
                        {
                            if (bFirst)
                            {
                                fPlannedValue = Convert.ToDecimal(dr["Plan"]);
                                fBookedValue = Convert.ToDecimal(dr["Book"]);
                                bFirst = false;
                                continue;
                            }
                            if (dr["Plan"] != DBNull.Value)
                                fPlannedValue += Convert.ToDecimal(dr["Plan"]);
                            if (dr["Book"] != DBNull.Value)
                                fBookedValue += Convert.ToDecimal(dr["Book"]);
                            dr["Plan"] = fPlannedValue;
                            dr["Book"] = fBookedValue;
                        }
                    }


                    tblStopingKG.AcceptChanges();

                    DataSetProdAnal.Tables.Add(tblStopingKG);

                    theReport.Pages[1].Visible = true;

                    #endregion

                    #region Report_Production_Analysis_Stoping_GT

                    if (!bIsProgressiveReport)
                    {
                        DataTable tblStopingGT = new DataTable("StopingGT");
                        nCount = 1;
                        try
                        {
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    tblStopingGT.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanP");
                                    tblStopingGT.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookP");
                                    nCount++;
                                }

                                for (int i = 1; i <= 3; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    tblStopingGT.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanF");
                                    tblStopingGT.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookF");
                                    nCount++;
                                }
                            }
                            else
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    tblStopingGT.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                    tblStopingGT.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookP");
                                    nCount++;
                                }

                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    tblStopingGT.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                    tblStopingGT.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookF");
                                    nCount++;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                        }

                        tblStopingGT.Columns.Add("CalendarDate");
                        tblStopingGT.Columns.Add("Plan");
                        tblStopingGT.Columns.Add("Book");

                        foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[14].Rows)
                        {
                            nCount = 1;
                            DataRow drNew = tblStopingGT.NewRow();
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + 14].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMM") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + 14].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + 14].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMM") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + 14].Rows[0][1];
                                    nCount++;
                                }
                            }
                            else
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + 14].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + 14].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + 14].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + 14].Rows[0][1];
                                    nCount++;
                                }
                            }
                            drNew["CalendarDate"] = dr["CalendarDate"];
                            drNew["Plan"] = dr["Plan"];
                            drNew["Book"] = dr["Book"];
                            tblStopingGT.Rows.Add(drNew);
                        }




                        tblStopingGT.AcceptChanges();

                        DataSetProdAnal.Tables.Add(tblStopingGT);

                        theReport.Pages[2].Visible = true;

                    }

                    #endregion

                    #region Report_Production_Analysis_Stoping_AdvancePerBlast

                    if (!bIsProgressiveReport)
                    {
                        DataTable tblStopingAdvancePerBlast = new DataTable("StopingAdvancePerBlast");
                        nCount = 1;
                        try
                        {
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    tblStopingAdvancePerBlast.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanP");
                                    tblStopingAdvancePerBlast.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookP");
                                    nCount++;
                                }

                                for (int i = 1; i <= 3; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    tblStopingAdvancePerBlast.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanF");
                                    tblStopingAdvancePerBlast.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookF");
                                    nCount++;
                                }
                            }
                            else
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    tblStopingAdvancePerBlast.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                    tblStopingAdvancePerBlast.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookP");
                                    nCount++;
                                }

                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    tblStopingAdvancePerBlast.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                    tblStopingAdvancePerBlast.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookF");
                                    nCount++;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                        }

                        tblStopingAdvancePerBlast.Columns.Add("CalendarDate");
                        tblStopingAdvancePerBlast.Columns.Add("PlanAdvancePerBlast");
                        tblStopingAdvancePerBlast.Columns.Add("BookAdvancePerBlast");

                        foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[21].Rows)
                        {
                            nCount = 1;
                            DataRow drNew = tblStopingAdvancePerBlast.NewRow();
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + 21].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMM") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + 21].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + 21].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMM") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + 21].Rows[0][1];
                                    nCount++;
                                }
                            }
                            else
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + 21].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + 21].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + 21].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + 21].Rows[0][1];
                                    nCount++;
                                }
                            }
                            drNew["CalendarDate"] = dr["CalendarDate"];
                            drNew["PlanAdvancePerBlast"] = dr["PlanAdvancePerBlast"];
                            drNew["BookAdvancePerBlast"] = dr["BookAdvancePerBlast"];
                            tblStopingAdvancePerBlast.Rows.Add(drNew);
                        }

                        tblStopingAdvancePerBlast.AcceptChanges();

                        DataSetProdAnal.Tables.Add(tblStopingAdvancePerBlast);

                        theReport.Pages[3].Visible = true;
                    }

                    #endregion

                    #region Report_Production_Analysis_Stoping_Tons

                    DataTable tblStopingTons = new DataTable("StopingTons");
                    try
                    {
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= 3; i++)
                            {
                                if (i < 0)
                                {
                                    tblStopingTons.Columns.Add(dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_PlanP");
                                    tblStopingTons.Columns.Add(dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_BookP");
                                }
                                else if (i > 0)
                                {
                                    tblStopingTons.Columns.Add(dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_PlanF");
                                    tblStopingTons.Columns.Add(dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_BookF");
                                }
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                tblStopingTons.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                tblStopingTons.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookP");
                            }
                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                tblStopingTons.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                tblStopingTons.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookF");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }

                    tblStopingTons.Columns.Add("CalendarDate");
                    tblStopingTons.Columns.Add("TonsPlanned");
                    tblStopingTons.Columns.Add("TonsBooked");
                    int tableCount = bIsProgressiveReport ? 14 : 28;
                    foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[tableCount].Rows)
                    {

                        DataRow drNew = tblStopingTons.NewRow();
                        nCount = 1;
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= 3; i++)
                            {
                                if (i < 0)
                                {
                                    drNew[dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][0];
                                    drNew[dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][1];
                                    nCount++;
                                }
                                else if (i > 0)
                                {
                                    drNew[dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][0];
                                    drNew[dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][1];
                                    nCount++;
                                }
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][1];
                                nCount++;
                            }
                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][1];
                                nCount++;
                            }
                        }

                        drNew["CalendarDate"] = dr["CalendarDate"];
                        drNew["TonsPlanned"] = dr["TonsPlanned"];
                        drNew["TonsBooked"] = dr["TonsBooked"];
                        tblStopingTons.Rows.Add(drNew);
                    }

                    if (bIsProgressiveReport)
                    {
                        bool bFirst = true;
                        decimal fPlannedValue = 0.0m;
                        decimal fBookedValue = 0.0m;

                        foreach (DataRow dr in tblStopingTons.Rows)
                        {
                            if (bFirst)
                            {
                                if (dr["TonsPlanned"] != DBNull.Value)
                                    fPlannedValue = Convert.ToDecimal(dr["TonsPlanned"]);
                                if (dr["TonsBooked"] != DBNull.Value)
                                    fBookedValue = Convert.ToDecimal(dr["TonsBooked"]);
                                bFirst = false;
                                continue;
                            }
                            if (dr["TonsPlanned"] != DBNull.Value)
                                fPlannedValue += Convert.ToDecimal(dr["TonsPlanned"]);
                            if (dr["TonsBooked"] != DBNull.Value)
                                fBookedValue += Convert.ToDecimal(dr["TonsBooked"]);
                            dr["TonsPlanned"] = fPlannedValue;
                            dr["TonsBooked"] = fBookedValue;
                        }
                    }

                    tblStopingTons.AcceptChanges();

                    DataSetProdAnal.Tables.Add(tblStopingTons);

                    theReport.Pages[8].Visible = true;

                    #endregion

                }

                if (reportSettings .Activity =="Development")
                {
                    #region Report_Production_Analysis_Development_MAdv

                    DataTable tblDevelopmentMAdv = new DataTable("DevelopmentMAdv");
                    int nCount = 1;
                    try
                    {
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanP");
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookReefP");
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookWasteP");
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanF");
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookReefF");
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookWasteF");
                                nCount++;
                            }
                        }
                        else
                        {

                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookReefP");
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookWasteP");
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookReefF");
                                tblDevelopmentMAdv.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookWasteF");
                                nCount++;
                            }


                        }
                    }
                    catch (Exception e)
                    {
                    }
                    tblDevelopmentMAdv.Columns.Add("CalendarDate");
                    tblDevelopmentMAdv.Columns.Add("AdvReef");
                    tblDevelopmentMAdv.Columns.Add("AdvWaste");
                    tblDevelopmentMAdv.Columns.Add("BookAdvReef");
                    tblDevelopmentMAdv.Columns.Add("BookAdvWaste");

                    foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[0].Rows)
                    {
                        nCount = 1;
                        DataRow drNew = tblDevelopmentMAdv.NewRow();
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                drNew[dtToSelect.ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMM") + "_BookReefP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                drNew[dtToSelect.ToString("yyyyMM") + "_BookWasteP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][2];
                                nCount++;
                            }
                            for (int i = 1; i <= 3; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                drNew[dtToSelect.ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMM") + "_BookReefF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                drNew[dtToSelect.ToString("yyyyMM") + "_BookWasteF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][2];
                                nCount++;
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookReefP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookWasteP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][2];
                                nCount++;
                            }
                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookReefF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookWasteF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][2];
                                nCount++;
                            }
                        }

                        drNew["CalendarDate"] = dr["CalendarDate"];
                        drNew["AdvReef"] = dr["AdvReef"];
                        drNew["AdvWaste"] = dr["AdvWaste"];
                        drNew["BookAdvReef"] = dr["BookAdvReef"];
                        drNew["BookAdvWaste"] = dr["BookAdvWaste"];
                        tblDevelopmentMAdv.Rows.Add(drNew);
                    }

                    if (bIsProgressiveReport)
                    {
                        bool bFirst = true;
                        decimal fPlannedReefValue = 0.0m;
                        decimal fPlannedWasteValue = 0.0m;
                        decimal fBookedReefValue = 0.0m;
                        decimal fBookedWasteValue = 0.0m;

                        foreach (DataRow dr in tblDevelopmentMAdv.Rows)
                        {
                            if (bFirst)
                            {
                                fPlannedReefValue = Convert.ToDecimal(dr["AdvReef"]);
                                fPlannedWasteValue = Convert.ToDecimal(dr["AdvWaste"]);
                                fBookedReefValue = Convert.ToDecimal(dr["BookAdvReef"]);
                                fBookedWasteValue = Convert.ToDecimal(dr["BookAdvWaste"] == DBNull.Value ? 0 : dr["BookAdvWaste"]);
                                bFirst = false;
                                continue;
                            }
                            if (dr["AdvReef"] != DBNull.Value)
                                fPlannedReefValue += Convert.ToDecimal(dr["AdvReef"]);
                            if (dr["AdvWaste"] != DBNull.Value)
                                fPlannedWasteValue += Convert.ToDecimal(dr["AdvWaste"]);
                            if (dr["BookAdvReef"] != DBNull.Value)
                                fBookedReefValue += Convert.ToDecimal(dr["BookAdvReef"]);
                            if (dr["BookAdvWaste"] != DBNull.Value)
                                fBookedWasteValue += Convert.ToDecimal(dr["BookAdvWaste"]);
                            dr["AdvReef"] = fPlannedReefValue;
                            dr["AdvWaste"] = fPlannedWasteValue;
                            dr["BookAdvReef"] = fBookedReefValue;
                            dr["BookAdvWaste"] = fBookedWasteValue;
                        }
                    }

                    tblDevelopmentMAdv.AcceptChanges();

                    DataSetProdAnal.Tables.Add(tblDevelopmentMAdv);

                    theReport.Pages[4].Visible = true;

                    #endregion

                    #region Report_Production_Analysis_Development_NoOfBlasts

                    if (!bIsProgressiveReport)
                    {
                        DataTable tblDevelopmentNoOfBlasts = new DataTable("DevelopmentNoOfBlasts");
                        nCount = 1;
                        try
                        {
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    tblDevelopmentNoOfBlasts.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanP");
                                    tblDevelopmentNoOfBlasts.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookP");
                                    nCount++;
                                }

                                for (int i = 1; i <= 3; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    tblDevelopmentNoOfBlasts.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanF");
                                    tblDevelopmentNoOfBlasts.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookF");
                                    nCount++;
                                }
                            }
                            else
                            {

                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    tblDevelopmentNoOfBlasts.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                    tblDevelopmentNoOfBlasts.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookP");
                                    nCount++;
                                }

                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    tblDevelopmentNoOfBlasts.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                    tblDevelopmentNoOfBlasts.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookF");
                                    nCount++;
                                }


                            }
                        }
                        catch (Exception e)
                        {
                        }
                        tblDevelopmentNoOfBlasts.Columns.Add("CalendarDate");
                        tblDevelopmentNoOfBlasts.Columns.Add("PlanNoOfBlasts");
                        tblDevelopmentNoOfBlasts.Columns.Add("BookNoOfBlasts");

                        foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[7].Rows)
                        {
                            nCount = 1;
                            DataRow drNew = tblDevelopmentNoOfBlasts.NewRow();
                            try
                            {
                                if (reportSettings.ProdMonthSelection == "Prodmonth")
                                {
                                    for (int i = -3; i <= -1; i++)
                                    {
                                        dtToSelect = dtProdMonth.AddMonths(i);
                                        drNew[dtToSelect.ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][0];
                                        drNew[dtToSelect.ToString("yyyyMM") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][1];
                                        nCount++;
                                    }
                                    for (int i = 1; i <= 3; i++)
                                    {
                                        dtToSelect = dtProdMonth.AddMonths(i);
                                        drNew[dtToSelect.ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][0];
                                        drNew[dtToSelect.ToString("yyyyMM") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][1];
                                        nCount++;
                                    }
                                }
                                else
                                {
                                    for (int i = -3; i <= -1; i++)
                                    {
                                        if (i == -3)
                                            dtToSelect = Convert.ToDateTime(stDatePrev3);
                                        if (i == -2)
                                            dtToSelect = Convert.ToDateTime(stDatePrev2);
                                        if (i == -1)
                                            dtToSelect = Convert.ToDateTime(stDatePrev1);
                                        drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][0];
                                        drNew[dtToSelect.ToString("yyyyMMdd") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][1];
                                        nCount++;
                                    }
                                    for (int i = 1; i <= 3; i++)
                                    {
                                        if (i == 1)
                                            dtToSelect = Convert.ToDateTime(stDateNext1);
                                        if (i == 2)
                                            dtToSelect = Convert.ToDateTime(stDateNext2);
                                        if (i == 3)
                                            dtToSelect = Convert.ToDateTime(stDateNext3);
                                        drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][0];
                                        drNew[dtToSelect.ToString("yyyyMMdd") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + 7].Rows[0][1];
                                        nCount++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                            }
                            drNew["CalendarDate"] = dr["CalendarDate"];
                            drNew["PlanNoOfBlasts"] = dr["PlanNoOfBlasts"];
                            drNew["BookNoOfBlasts"] = dr["BookNoOfBlasts"];
                            tblDevelopmentNoOfBlasts.Rows.Add(drNew);
                        }

                        tblDevelopmentNoOfBlasts.AcceptChanges();

                        DataSetProdAnal.Tables.Add(tblDevelopmentNoOfBlasts);

                        theReport.Pages[5].Visible = true;
                    }


                    #endregion

                    #region Report_Production_Analysis_Stoping_Tons

                    DataTable tblDevelopmentTons = new DataTable("DevelopmentTons");
                    try
                    {
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= 3; i++)
                            {
                                if (i < 0)
                                {
                                    tblDevelopmentTons.Columns.Add(dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_PlanP");
                                    tblDevelopmentTons.Columns.Add(dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_BookP");
                                }
                                else if (i > 0)
                                {
                                    tblDevelopmentTons.Columns.Add(dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_PlanF");
                                    tblDevelopmentTons.Columns.Add(dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_BookF");
                                }
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                tblDevelopmentTons.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                tblDevelopmentTons.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookP");
                            }
                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                tblDevelopmentTons.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                tblDevelopmentTons.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookF");
                            }

                        }
                    }
                    catch (Exception e)
                    {
                    }

                    tblDevelopmentTons.Columns.Add("CalendarDate");//linda
                    tblDevelopmentTons.Columns.Add("TonsPlanned");
                    tblDevelopmentTons.Columns.Add("TonsBooked");
                    int tableCount = bIsProgressiveReport ? 7 : 14;
                    foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[tableCount].Rows)
                    {

                        DataRow drNew = tblDevelopmentTons.NewRow();
                        nCount = 1;
                        try
                        {
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= 3; i++)
                                {
                                    if (i < 0)
                                    {
                                        drNew[dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][0];
                                        drNew[dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][1];
                                        nCount++;
                                    }
                                    else if (i > 0)
                                    {
                                        drNew[dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][0];
                                        drNew[dtProdMonth.AddMonths(i).ToString("yyyyMM") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][1];
                                        nCount++;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookP"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][1];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][0];
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_BookF"] = _dbMan.ResultsDataSet.Tables[nCount + tableCount].Rows[0][1];
                                    nCount++;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                        }

                        drNew["CalendarDate"] = dr["CalendarDate"];
                        drNew["TonsPlanned"] = dr["TonsPlanned"];
                        drNew["TonsBooked"] = dr["TonsBooked"];
                        tblDevelopmentTons.Rows.Add(drNew);
                    }

                    if (bIsProgressiveReport)
                    {
                        bool bFirst = true;
                        decimal fPlannedValue = 0.0m;
                        decimal fBookedValue = 0.0m;

                        foreach (DataRow dr in tblDevelopmentTons.Rows)
                        {
                            if (bFirst)
                            {
                                if (dr["TonsPlanned"] != DBNull.Value)
                                    fPlannedValue = Convert.ToDecimal(dr["TonsPlanned"]);
                                if (dr["TonsBooked"] != DBNull.Value)
                                    fBookedValue = Convert.ToDecimal(dr["TonsBooked"]);
                                bFirst = false;
                                continue;
                            }
                            if (dr["TonsPlanned"] != DBNull.Value)
                                fPlannedValue += Convert.ToDecimal(dr["TonsPlanned"]);
                            if (dr["TonsBooked"] != DBNull.Value)
                                fBookedValue += Convert.ToDecimal(dr["TonsBooked"]);
                            dr["TonsPlanned"] = fPlannedValue;
                            dr["TonsBooked"] = fBookedValue;
                        }
                    }

                    tblDevelopmentTons.AcceptChanges();

                    DataSetProdAnal.Tables.Add(tblDevelopmentTons);

                    theReport.Pages[9].Visible = true;

                    #endregion

                }

                #region Report_Production_Analysis_Engineering_Breakdowns

                if (reportSettings .Activity =="Engineering")
                {

                    DataTable tblEngineeringBreakDowns = new DataTable("EngineeringBreakDowns");
                    int nCount = 1;
                    try
                    {
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblEngineeringBreakDowns.Columns.Add(dtToSelect.ToString("yyyyMM") + "_P");
                                _dbMan.ResultsDataSet.Tables[nCount].TableName = dtToSelect.ToString("yyyyMM") + "_P";
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                try
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    tblEngineeringBreakDowns.Columns.Add(dtToSelect.ToString("yyyyMM") + "_F");
                                    _dbMan.ResultsDataSet.Tables[nCount].TableName = dtToSelect.ToString("yyyyMM") + "_F";
                                }
                                catch (Exception e)
                                { }
                                nCount++;
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                tblEngineeringBreakDowns.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_P");
                                _dbMan.ResultsDataSet.Tables[nCount].TableName = dtToSelect.ToString("yyyyMMdd") + "_P";
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                try
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    tblEngineeringBreakDowns.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_F");
                                    _dbMan.ResultsDataSet.Tables[nCount].TableName = dtToSelect.ToString("yyyyMMdd") + "_F";
                                }
                                catch (Exception e)
                                { }
                                nCount++;
                            }

                        }
                    }
                    catch (Exception e)
                    {
                    }


                    tblEngineeringBreakDowns.Columns.Add("CalendarDate");
                    tblEngineeringBreakDowns.Columns.Add("Value");
                    try
                    {
                        foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[0].Rows)
                        {
                            nCount = 1;
                            DataRow drNew = tblEngineeringBreakDowns.NewRow();
                            if (reportSettings.ProdMonthSelection == "Prodmonth")
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_P"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    dtToSelect = dtProdMonth.AddMonths(i);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_F"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                    nCount++;
                                }
                            }
                            else
                            {
                                for (int i = -3; i <= -1; i++)
                                {
                                    if (i == -3)
                                        dtToSelect = Convert.ToDateTime(stDatePrev3);
                                    if (i == -2)
                                        dtToSelect = Convert.ToDateTime(stDatePrev2);
                                    if (i == -1)
                                        dtToSelect = Convert.ToDateTime(stDatePrev1);
                                    drNew[dtToSelect.ToString("yyyyMMdd") + "_P"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                    nCount++;
                                }
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (i == 1)
                                        dtToSelect = Convert.ToDateTime(stDateNext1);
                                    if (i == 2)
                                        dtToSelect = Convert.ToDateTime(stDateNext2);
                                    if (i == 3)
                                        dtToSelect = Convert.ToDateTime(stDateNext3);
                                    drNew[dtToSelect.ToString("yyyyMM") + "_F"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                    nCount++;
                                }
                            }
                            drNew["CalendarDate"] = dr["CalendarDate"];
                            drNew["Value"] = dr["Value"];
                            tblEngineeringBreakDowns.Rows.Add(drNew);
                        }
                    }
                    catch (Exception e)
                    {
                    }

                    if (bIsProgressiveReport)
                    {
                        bool bFirst = true;
                        decimal fValue = 0.0m;

                        foreach (DataRow dr in tblEngineeringBreakDowns.Rows)
                        {
                            if (bFirst)
                            {
                                fValue = Convert.ToDecimal(dr["Value"]);
                                bFirst = false;
                                continue;
                            }
                            if (dr["Value"] != DBNull.Value)
                                fValue += Convert.ToDecimal(dr["Value"]);
                            dr["Value"] = fValue;
                        }
                        tblEngineeringBreakDowns.AcceptChanges();
                    }


                    tblEngineeringBreakDowns.AcceptChanges();

                    DataSetProdAnal.Tables.Add(tblEngineeringBreakDowns);

                    theReport.Pages[6].Visible = true;
                }

                #endregion

                #region Report_Production_Analysis_HR_Compliment

                if (reportSettings .Activity =="HR")
                {
                    DataTable tblHRCompliment = new DataTable("HRCompliment");
                    int nCount = 1;
                    try
                    {
                        if (reportSettings.ProdMonthSelection == "Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanP");
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookAtWorkP");
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookUnAvailableP");
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMM") + "_PlanF");
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookAtWorkF");
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMM") + "_BookUnAvailableF");
                                nCount++;
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanP");
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookAtWorkP");
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookUnAvailableP");
                                nCount++;
                            }

                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_PlanF");
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookAtWorkF");
                                tblHRCompliment.Columns.Add(dtToSelect.ToString("yyyyMMdd") + "_BookUnAvailableF");
                                nCount++;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }

                    tblHRCompliment.Columns.Add("CalendarDate");
                    tblHRCompliment.Columns.Add("PlanValue");
                    tblHRCompliment.Columns.Add("BookValueAtWork");
                    tblHRCompliment.Columns.Add("BookValueUnAvailables");

                    foreach (DataRow dr in _dbMan.ResultsDataSet.Tables[0].Rows)
                    {
                        nCount = 1;
                        DataRow drNew = tblHRCompliment.NewRow();
                        if (reportSettings .ProdMonthSelection =="Prodmonth")
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                drNew[dtToSelect.ToString("yyyyMM") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMM") + "_BookAtWorkP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                drNew[dtToSelect.ToString("yyyyMM") + "_BookUnAvailableP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][2];
                                nCount++;
                            }
                            for (int i = 1; i <= 3; i++)
                            {
                                dtToSelect = dtProdMonth.AddMonths(i);
                                drNew[dtToSelect.ToString("yyyyMM") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMM") + "_BookAtWorkF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                drNew[dtToSelect.ToString("yyyyMM") + "_BookUnAvailableF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][2];
                                nCount++;
                            }
                        }
                        else
                        {
                            for (int i = -3; i <= -1; i++)
                            {
                                if (i == -3)
                                    dtToSelect = Convert.ToDateTime(stDatePrev3);
                                if (i == -2)
                                    dtToSelect = Convert.ToDateTime(stDatePrev2);
                                if (i == -1)
                                    dtToSelect = Convert.ToDateTime(stDatePrev1);
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookAtWorkP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookUnAvailableP"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][2];
                                nCount++;
                            }
                            for (int i = 1; i <= 3; i++)
                            {
                                if (i == 1)
                                    dtToSelect = Convert.ToDateTime(stDateNext1);
                                if (i == 2)
                                    dtToSelect = Convert.ToDateTime(stDateNext2);
                                if (i == 3)
                                    dtToSelect = Convert.ToDateTime(stDateNext3);
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_PlanF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][0];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookAtWorkF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][1];
                                drNew[dtToSelect.ToString("yyyyMMdd") + "_BookUnAvailableF"] = _dbMan.ResultsDataSet.Tables[nCount].Rows[0][2];
                                nCount++;
                            }
                        }
                        drNew["CalendarDate"] = dr["CalendarDate"];
                        drNew["PlanValue"] = dr["PlanValue"];
                        drNew["BookValueAtWork"] = dr["BookValueAtWork"];
                        drNew["BookValueUnAvailables"] = dr["BookValueUnAvailables"];
                        tblHRCompliment.Rows.Add(drNew);
                    }



                    tblHRCompliment.AcceptChanges();

                    //CSV(tblHRCompliment);

                    DataSetProdAnal.Tables.Add(tblHRCompliment);

                    theReport.Pages[7].Visible = true;
                }

                #endregion

                #endregion

            }
            catch (Exception e)
            {

            }
            TimeSpan tsDiff = DateTime.Now - dtStart;

            //MessageBox.Show(tsDiff.TotalMilliseconds.ToString());

            #region Custom Data

            DataTable tblCustom = new DataTable("Custom");
            tblCustom.Columns.Add("Banner");
            tblCustom.Columns.Add("ProdMonth");
            tblCustom.Columns.Add("GraphType");
            tblCustom.Columns.Add("Activity");
            tblCustom.Columns.Add("Section");

            string strGraphType = "Daily";
            if (bIsProgressiveReport)
                strGraphType = "Progressive";

            DataRow drCustom = tblCustom.NewRow();
            drCustom["Banner"] = SysSettings.Banner;
            if (reportSettings .ProdMonthSelection =="Prodmonth")
                drCustom["ProdMonth"] = "Prod Month : " + strProdMonth;
            else
                drCustom["ProdMonth"] = "From " + String.Format("{0:yyyy-MM-dd}", reportSettings .StartDate ) +
                                        " to " + String.Format("{0:yyyy-MM-dd}", reportSettings.EndDate);
            drCustom["GraphType"] = strGraphType;
            drCustom["Activity"] = reportSettings.Activity;
            drCustom["Section"] = reportSettings.SectionID;
            tblCustom.Rows.Add(drCustom);
            tblCustom.AcceptChanges();

            theReport.RegisterData(tblCustom, "CustomData");

            #endregion

            theReport.RegisterData(DataSetProdAnal);

            //if (MessageBox.Show("Design?", "Design?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            //    theReport.Design();

            bool bDidFindResults = false;

            foreach (DataTable tbl in DataSetProdAnal.Tables)
            {
                if (tbl.Rows.Count != 0)
                {
                    bDidFindResults = true;
                    break;
                }
            }


           // pcReport.Clear();
            if (bDidFindResults)
            {
                theReport.Prepare();
                theReport.Refresh();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
            }
            else
            {
                MessageBox.Show("No data found for selected search criteria. Please try again.",
                    "No Data Found.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                theReport.Prepare();
                theReport.Refresh();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
               // return;
            }
        }

        private void ucProductionAnalysisReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            // reportSettings.ReportDate = DateTime.Now;
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "(SELECT MAX(MillMonth)MillMonth FROM CALENDARMILL WHERE StartDate <= GETDATE())";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            DataTable dt = new DataTable();
            dt = _dbMan.ResultsDataTable;
            foreach (DataRow dr in dt.Rows  )
            {
                reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(dr["MillMonth"].ToString());
            }
           // reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            reportSettings.StartDate = DateTime.Now;
            iFromDate.Properties.Value = reportSettings.StartDate;
            reportSettings.EndDate = DateTime.Now;
            iToDate.Properties.Value = reportSettings.EndDate;
            iProdmonth.Properties.Value = reportSettings.Prodmonth;
            reportSettings.ProdMonthSelection = "Prodmonth";
            reportSettings.Activity = "Stoping";
            reportSettings.Type = "Daily";
          //  iProdmonth.Enabled = false;

            LoadSections(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            reportSettings.SectionID = dtSections.Rows[0][0].ToString();
            iFromDate.Visible = false;
            iToDate.Visible = false;
            iProdmonth.Visible = true;
        pgProductionAnalysisRepSettings     .SelectedObject = reportSettings;
        }

        private void LoadSections(string strProdMonth)
        {
          
            StringBuilder sb = new StringBuilder();
            if (reportSettings.ProdMonthSelection == "")
            {
                foundNoSection = false;
                // cmbSection.Items.Clear();
              
            }
            else
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                if (reportSettings.ProdMonthSelection == "Prodmonth")
                {
                    sb.AppendLine("select SectionID+':'+Name SectionID from SECTION");
                    sb.AppendLine("where Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'");
                    sb.AppendLine("order by Hierarchicalid, SectionID");
                    _dbMan.SqlStatement = sb.ToString();
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();
                    DataTable dtSec = _dbMan.ResultsDataTable;
                    if (dtSec.Rows.Count == 0)
                        foundNoSection = true;
                    Error_Section();
                }
                else
                {
                    //sb.AppendLine(" select distinct((SectionId + ':' + Name)) Section, HierarchicalId \r\n " );
                    sb.AppendLine(" select max(minProd) minProd, max(maxProd) maxProd \r\n ");
                    sb.AppendLine(" from \r\n ");
                    sb.AppendLine(" ( \r\n ");
                    sb.AppendLine("     select min(s.ProdMonth) minProd, maxProd = 0 from Section s \r\n ");
                    sb.AppendLine("     inner join Seccal scal on \r\n ");
                    sb.AppendLine("       s.Prodmonth = scal.Prodmonth and \r\n ");
                    sb.AppendLine("       s.SectionID = scal.Sectionid \r\n ");
                    // sb.AppendLine("     where s.HierarchicalType = 'Pro' and \r\n ");
                    sb.AppendLine("        where (scal.BeginDate <= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.StartDate) + "' and \r\n ");
                    sb.AppendLine("         scal.EndDate >= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.StartDate) + "' ) \r\n ");
                    sb.AppendLine("     union all \r\n ");
                    sb.AppendLine("     select minProd = 0, max(s.ProdMonth) maxProd from Section s \r\n ");
                    sb.AppendLine("     inner join Seccal scal on \r\n ");
                    sb.AppendLine("       s.Prodmonth = scal.Prodmonth and \r\n ");
                    sb.AppendLine("       s.SectionID = scal.Sectionid \r\n ");
                    // sb.AppendLine("     where s.HierarchicalType = 'Pro' and \r\n ");
                    sb.AppendLine("        where ( scal.BeginDate <= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.EndDate) + "' and \r\n ");
                    sb.AppendLine("         scal.EndDate >= '" + String.Format("{0:yyyy-MM-dd}", reportSettings.EndDate) + "' ) \r\n ");
                    sb.AppendLine(" ) a \r\n ");

                    _dbMan.SqlStatement = sb.ToString();
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();

                    minProd = strProdMonth;
                    maxProd = strProdMonth;
                    DataTable dtSec = _dbMan.ResultsDataTable;
                    if (dtSec.Rows.Count > 0)
                    {
                        minProd = dtSec.Rows[0]["minProd"].ToString();
                        maxProd = dtSec.Rows[0]["maxProd"].ToString();

                        if ((minProd == "0") &
                            (maxProd != "0"))
                            minProd = dtSec.Rows[0]["maxProd"].ToString();

                        if ((maxProd == "0") &
                            (minProd != "0"))
                            maxProd = dtSec.Rows[0]["minProd"].ToString();

                        if ((minProd == "0") &
                            (maxProd == "0"))
                            foundNoSection = true;
                        Error_Section();
                    }

                    sb.Clear();
                    sb.AppendLine("select distinct SectionID+':'+Name SectionID from SECTION");
                    //  sb.AppendLine("select distinct SectionID, Name) Section, HierarchicalID, SectionID from SECTION");
                    sb.AppendLine("where Prodmonth >= '" + minProd + "' and Prodmonth <= '" + maxProd + "' ");
                    sb.AppendLine("order by SectionID");
                    _dbMan.SqlStatement = sb.ToString();
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();
                }

                riSection.ValueMember = "SectionID";
                riSection.DisplayMember = "SectionID";
                riSection.DataSource = _dbMan.ResultsDataTable;
                dtSections = _dbMan.ResultsDataTable;
            }
        
        }

        private void Error_Section()
        {
            if (foundNoSection == true)
                MessageBox.Show("No Sections for your Date Selection. \r\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void get_Shifts(string theSection, string theMonth, string theFDate, string theTDate, string theHier)
        {
            MWDataManager.clsDataAccess _dbManA = new MWDataManager.clsDataAccess();
            _dbManA.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManA.SqlStatement = " select count(distinct(p.CalendarDate)) cntShfts \r\n " +
                        " from PLANNING p \r\n " +
                        " inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON P.Calendardate = CT.Calendardate \r\n " +
                        " where  \r\n ";
            if (reportSettings .ProdMonthSelection =="Prodmonth")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " p.ProdMonth = '" + theMonth + "' \r\n ";
            else
                _dbManA.SqlStatement = _dbManA.SqlStatement + "  p.CalendarDate >= '" + theFDate + "' and p.CalendarDate <= '" + theTDate + "'  \r\n ";
            if (reportSettings .Activity =="Stoping")
                _dbManA.SqlStatement = _dbManA.SqlStatement + "  and p.Activity IN (0,9) \r\n ";
            else
                if (reportSettings.Activity == "Development")
                _dbManA.SqlStatement = _dbManA.SqlStatement + "  and p.Activity = 1 \r\n ";
            _dbManA.SqlStatement = _dbManA.SqlStatement + " and CT.WorkingDay = 'Y' ";
            if (theHier == "1")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.Sectionid_5 = '" + theSection + "' ";
            if (theHier == "2")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.Sectionid_4 = '" + theSection + "' ";
            if (theHier == "3")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.Sectionid_3 = '" + theSection + "' ";
            if (theHier == "4")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.Sectionid_2 = '" + theSection + "' ";
            if (theHier == "5")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.Sectionid_1 = '" + theSection + "' ";
            if (theHier == "6")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID = '" + theSection + "' ";
            _dbManA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManA.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManA.ExecuteInstruction();

            DataTable dtShft = _dbManA.ResultsDataTable;
            procShft = 0;
            if (dtShft.Rows.Count > 0)
            {
                procShft = Convert.ToInt32(dtShft.Rows[0]["cntShfts"].ToString());
                if (procShft == 0)
                {
                    MessageBox.Show("The no of Shifts is Zero. \r\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void get_PrevDates(string theSection, string theHier, DateTime theDate)
        {
            procDateEnd = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(theDate.ToString()).AddDays(-1));

            MWDataManager.clsDataAccess _dbManA = new MWDataManager.clsDataAccess();
            _dbManA.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManA.SqlStatement = " select distinct Top " + procShft + " CalendarDate \r\n " +
                        " from PLANNING p \r\n " +
                        " inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth \r\n " +
                        " where  \r\n " +
                        "  p.CalendarDate <= '" + procDateEnd + "'  \r\n ";
            if (reportSettings .Activity =="Stoping")
                _dbManA.SqlStatement = _dbManA.SqlStatement + "  and p.Activity IN (0,9) \r\n ";
            else
                if (reportSettings.Activity == "Development")
                _dbManA.SqlStatement = _dbManA.SqlStatement + "  and p.Activity = 1 \r\n ";
            if (theHier == "1")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_5 = '" + theSection + "' \r\n ";
            if (theHier == "2")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_4 = '" + theSection + "' \r\n ";
            if (theHier == "3")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_3 = '" + theSection + "' \r\n ";
            if (theHier == "4")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_2 = '" + theSection + "' \r\n ";
            if (theHier == "5")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_1 = '" + theSection + "' \r\n ";
            if (theHier == "6")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID = '" + theSection + "' \r\n ";

            _dbManA.SqlStatement = _dbManA.SqlStatement + " order by CalendarDate desc ";
            _dbManA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManA.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManA.ExecuteInstruction();

            DataTable dtCount = _dbManA.ResultsDataTable;
            foreach (DataRow rr in dtCount.Rows)
            {
                procDateStart = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(rr["CalendarDate"].ToString()));
            }
        }

        private void get_NextDates(string theSection, string theHier, DateTime theDate)
        {
            procDateStart = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(theDate.ToString()).AddDays(1));

            MWDataManager.clsDataAccess _dbManA = new MWDataManager.clsDataAccess();
            _dbManA.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManA.SqlStatement = " select distinct Top " + procShft + " CalendarDate \r\n " +
                        " from PLANNING p \r\n " +
                        " inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth \r\n " +
                        " where  \r\n " +
                        "  p.CalendarDate >= '" + procDateStart + "'  \r\n " +
                        "  and p.Activity IN (0,9) ";
            if (theHier == "1")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_5 = '" + theSection + "' \r\n ";
            if (theHier == "2")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_4 = '" + theSection + "' \r\n ";
            if (theHier == "3")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_3 = '" + theSection + "' \r\n ";
            if (theHier == "4")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_2 = '" + theSection + "' \r\n ";
            if (theHier == "5")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID_1 = '" + theSection + "' \r\n ";
            if (theHier == "6")
                _dbManA.SqlStatement = _dbManA.SqlStatement + " and sc.SectionID = '" + theSection + "' \r\n ";
            _dbManA.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManA.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManA.ExecuteInstruction();

            DataTable dtCount = _dbManA.ResultsDataTable;
            foreach (DataRow rr in dtCount.Rows)
            {
                procDateEnd = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(rr["CalendarDate"].ToString()));
            }
        }

        private void riProdmonthSelection_EditValueChanged(object sender, EventArgs e)
        {
         pgProductionAnalysisRepSettings    .PostEditor();
            if (reportSettings.ProdMonthSelection == "Prodmonth")
            {
                iFromDate.Visible = false;
                iToDate.Visible = false;
                iProdmonth.Visible = true;
                LoadSections(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            }
            else
            {
                iFromDate.Visible = true;
                iToDate.Visible = true;
                iProdmonth.Visible = false;
                LoadSections(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            }
        }

        private void riStartDate_EditValueChanged(object sender, EventArgs e)
        {
            pgProductionAnalysisRepSettings.PostEditor();
            if (reportSettings .EndDate   > reportSettings .StartDate )
            {
               LoadSections(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            }
        }

        private void riEndDate_EditValueChanged(object sender, EventArgs e)
        {
            pgProductionAnalysisRepSettings.PostEditor();
            if (reportSettings.EndDate > reportSettings.StartDate)
            {
                LoadSections(TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            }
        }
    }
}
