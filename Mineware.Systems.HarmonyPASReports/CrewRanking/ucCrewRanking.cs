using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using FastReport;
using System.Threading;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.CrewRanking
{
    public partial class ucCrewRanking : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;
        //private DataTable dtSections;
        DataTable dtSections = new DataTable();
        private clsCrewRankingSettings reportSettings = new clsCrewRankingSettings();

        public string PerfMonth3;
        public string PerfMonth12;
        public int Themonth;
        public string Sec;
        private DateTime _theDate;
        public ucCrewRanking()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void pgCrewRankingRepSettings_Click(object sender, EventArgs e)
        {

        }

        private void ucCrewRanking_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            // reportSettings.ReportDate = DateTime.Now;
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = "(SELECT MAX(MillMonth)MillMonth FROM CALENDARMILL WHERE StartDate <= GETDATE())";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();
            DataTable dt = new DataTable();
            dt = _dbMan1.ResultsDataTable;
            foreach (DataRow dr in dt.Rows)
            {
                reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(dr["MillMonth"].ToString());
            }
            //reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            iProdmonth.Properties.Value = reportSettings.Prodmonth;
            PROD();
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.SqlStatement = " Select 0 SectionID,'Total Mine' Name  ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable What = _dbMan.ResultsDataTable;
            riSection.DataSource = _dbMan.ResultsDataTable;
            riSection.DisplayMember = "Name";
            riSection.ValueMember = "Name";

            reportSettings.SectionID = _dbMan.ResultsDataTable.Rows[0][1].ToString();
            reportSettings.Activity = "Stoping";
            reportSettings.By = "Crew";
            reportSettings.From = "Actual/Book";
            reportSettings.OrderBy = "1 Month";
            reportSettings.RatingBy = "M2";
            pgCrewRankingRepSettings.SelectedObject = reportSettings;
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
            string rateby = "";
            string orderby = "";
            string persect = "";
            string reporttype = "";
            string type = "";
            string typeOfReport = "";
            Report theReport = new Report();
            if (reportSettings .By == "Crew")
            {
                type = "OrgUnitDay";
                typeOfReport = "Gang No";
            }

            if (reportSettings.By =="MO")
            {
                type = "sc.SectionID_2+" + "'':''" + "+Name_2";
                typeOfReport = "MO";
            }

            if (reportSettings.By == "Shift Boss")
            {
                type = "sc.SectionID_1+" + "'':''" + "+Name_1";
                typeOfReport = "Shift Boss";
            }

            if (reportSettings.Activity=="Stoping")
            {
                reporttype = "Report_CrewRanking_New";
            }
            else
            {
                reporttype = "Report_CrewRanking_New_Dev";
            }

            if (reportSettings.RatingBy == "M2")
            {
                if (reportSettings.From == "Actual/Book")
                {
                    rateby = "B";
                }
                else
                {
                    rateby = "AB";
                }
            }
            else
            {
                if (reportSettings.From == "Actual/Book")
                {
                    rateby = "P";
                }
                else
                {
                    rateby = "AP";
                }
            }

            if (reportSettings.OrderBy == "1 Month")
            {
                orderby = "1 Month";
            }
            if (reportSettings.OrderBy == "3 Month")
            {
                orderby = "3 Month";
            }
            if (reportSettings.OrderBy == "12 Month")
            {
                orderby = "12 Month";
            }
            //if (chkPerSection.Checked == true)
            //{
            //    persect = "T";
            //}
            //else
            //{
                persect = "F";
           // }
            string status = "";
            string sectid = "";
            if (reportSettings.SectionID == "Total Mine")
            {
                sectid = "Total Mine";
            }
            else
            {
                string sectid1 = reportSettings.SectionID;
                string sectid2 = sectid1.IndexOf(":").ToString();
                int sec = Convert.ToInt32(sectid2);
                sectid = sectid1.Substring(0, sec);
            }
            DataSet ReportDataset = new DataSet();
            if (reportSettings.SectionID == "Total Mine" && persect == "F")
            {
                if ((rateby == "B" || rateby == "AB") && orderby == "1 Month")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','1 Month','T' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                }
                if ((rateby == "B" || rateby == "AB") && orderby == "3 Month")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','3 Month','T' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                }
                if ((rateby == "B" || rateby == "AB") && orderby == "12 Month")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','12 Month','T' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                }
                if ((rateby == "P" || rateby == "AP") && orderby == "1 Month")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','1 Month','T','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                }
                if ((rateby == "P" || rateby == "AP") && orderby == "3 Month")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','3 Month','T','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                }
                if ((rateby == "P" || rateby == "AP") && orderby == "12 Month")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','12 Month','T','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                }
            }
            else
            {
                if ((rateby == "B" || rateby == "AB") && orderby == "1 Month")
                {
                    //status ="T";

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','1 Month','T' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan2.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','3 Month','F','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan2.ResultsTableName = "CrewPerformance 3MONTH";
                    _dbMan2.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                    _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan3.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','12 Month','F' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan3.ResultsTableName = "CrewPerformance 12MONTH";
                    _dbMan3.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan2.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan3.ResultsDataTable);



                }

                if ((rateby == "B" || rateby == "AB") && orderby == "3 Month")
                {
                    //status ="T";

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','1 Month','F' ,'" + reportSettings.SectionID + "','" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan2.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','3 Month','T','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan2.ResultsTableName = "CrewPerformance 3MONTH";
                    _dbMan2.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                    _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan3.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','12 Month','F' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan3.ResultsTableName = "CrewPerformance 12MONTH";
                    _dbMan3.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan2.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan3.ResultsDataTable);
                }

                if ((rateby == "B" || rateby == "AB") && orderby == "12 Month")
                {
                    //status ="T";

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','1 Month','F','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan2.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','3 Month','F','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan2.ResultsTableName = "CrewPerformance 3MONTH";
                    _dbMan2.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                    _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan3.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','12 Month','T','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan3.ResultsTableName = "CrewPerformance 12MONTH";
                    _dbMan3.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan2.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan3.ResultsDataTable);
                }

                if ((rateby == "P" || rateby == "AP") && orderby == "1 Month")
                {
                    //status ="T";

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','1 Month','T','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan2.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','3 Month','F','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan2.ResultsTableName = "CrewPerformance 3MONTH";
                    _dbMan2.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                    _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan3.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','12 Month','F','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan3.ResultsTableName = "CrewPerformance 12MONTH";
                    _dbMan3.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan2.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan3.ResultsDataTable);
                }

                if ((rateby == "P" || rateby == "AP") && orderby == "3 Month")
                {
                    //status ="T";

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','1 Month','F','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan2.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','3 Month','T' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan2.ResultsTableName = "CrewPerformance 3MONTH";
                    _dbMan2.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                    _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan3.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','12 Month','F' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan3.ResultsTableName = "CrewPerformance 12MONTH";
                    _dbMan3.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan2.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan3.ResultsDataTable);
                }

                if ((rateby == "P" || rateby == "AP") && orderby == "12 Month")
                {
                    //status ="T";

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','1 Month','F' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "CrewPerformance 1MONTH";
                    _dbMan.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan2.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','3 Month','F' ,'" + reportSettings.SectionID + "' ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan2.ResultsTableName = "CrewPerformance 3MONTH";
                    _dbMan2.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                    _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                    _dbMan3.SqlStatement = "exec " + reporttype + " '" + TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth) + "', '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "','" + sectid + "','" + rateby + "','12 Month','T','" + reportSettings.SectionID + "'  ,'" + persect + "','" + type + "'" +
                                          "  ";
                    _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan3.ResultsTableName = "CrewPerformance 12MONTH";
                    _dbMan3.ExecuteInstruction();

                    ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan2.ResultsDataTable);
                    ReportDataset.Tables.Add(_dbMan3.ResultsDataTable);


                }
            }

            //For chart
            //MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
            ////_dbMan2.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            //_dbMan2.SqlStatement = " exec Report_CrewPerformance '" + PerfMonth3.ToString() + "', '" + PerfMonth12.ToString() + "', " +
            //                      " '" + CrewCombo.SelectedItem.ToString() + "' ";
            //_dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbMan2.ResultsTableName = "CrewPerformanceGraph";
            //_dbMan2.ExecuteInstruction();

            //get the banner from sysset
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbMan1.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan1.SqlStatement = "select banner from sysset ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ResultsTableName = "sysset";
            _dbMan1.ExecuteInstruction();


            //ReportDataset.Tables.Add(_dbMan.ResultsDataTable);
            theReport.Clear();
            DataSet BannerDataSet = new DataSet();
            BannerDataSet.Tables.Add(_dbMan1.ResultsDataTable);

            //DataSet GraphDataSet = new DataSet();
            //GraphDataSet.Tables.Add(_dbMan2.ResultsDataTable);

            theReport.RegisterData(ReportDataset);
            theReport.RegisterData(BannerDataSet);
            //theReport.RegisterData(GraphDataSet);

            theReport.SetParameterValue("CrewRanking", "None");
            theReport.SetParameterValue("CrewRanking2", "None");
            theReport.SetParameterValue("CrewRanking3", "None");

            //if (Text == "Gang Ranking Report")
            //{
            //    FastReport.Utils.XmlItem item = FastReport.Utils.Config.Root.FindItem("Forms").FindItem("PreviewForm");
            //    item.SetProp("Maximized", "0");
            //    item.SetProp("Left", "600");
            //    item.SetProp("Top", "0");
            //    item.SetProp("Width", "700");
            //    item.SetProp("Height", "600");
            //}
            if (persect == "T")
            {
                //theReport.Load("CrewRankingReportNew.frx");
                if (reportSettings.Activity == "Stoping")
                {
                    theReport.Load(TGlobalItems.ReportsFolder + "\\CrewRankingReportNew.frx");

                    if ((rateby == "B" || rateby == "AB") && orderby == "1 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", true));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", true));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", false));
                    }
                    if ((rateby == "B" || rateby == "AB") && orderby == "3 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", true));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", false));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", true));
                    }
                    if ((rateby == "B" || rateby == "AB") && orderby == "12 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", false));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", true));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", true));
                    }



                    if ((rateby == "P" || rateby == "AP") && orderby == "1 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.SQM12Percentage", false));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.SQM3Percentage", false));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.SQM1Percentage", true));
                    }
                    if ((rateby == "P" || rateby == "AP") && orderby == "3 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.SQM12Percentage", false));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.SQM3Percentage", true));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.SQM1Percentage", false));
                    }
                    if ((rateby == "P" || rateby == "AP") && orderby == "12 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.SQM12Percentage", true));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.SQM3Percentage", false));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.SQM1Percentage", false));
                    }
                }
                else
                {
                    theReport.Load(TGlobalItems.ReportsFolder + "\\CrewRankingReportNewDev.frx");

                    if ((rateby == "B" || rateby == "AB") && orderby == "1 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", true));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", true));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", false));
                    }
                    if ((rateby == "B" || rateby == "AB") && orderby == "3 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", true));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", false));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", true));
                    }
                    if ((rateby == "B" || rateby == "AB") && orderby == "12 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", false));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", true));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", true));
                    }



                    if ((rateby == "P" || rateby == "AP") && orderby == "1 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.ADV12Percentage", false));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.ADV3Percentage", false));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.ADV1Percentage", true));
                    }
                    if ((rateby == "P" || rateby == "AP") && orderby == "3 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.ADV12Percentage", false));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.ADV3Percentage", true));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.ADV1Percentage", false));
                    }
                    if ((rateby == "P" || rateby == "AP") && orderby == "12 Month")
                    {
                        (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.ADV12Percentage", true));
                        (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.ADV3Percentage", false));
                        (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.ADV1Percentage", false));
                    }
                }
                theReport.SetParameterValue("sect", reportSettings.SectionID);
                theReport.SetParameterValue("Prodmonth", TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth));
                theReport.SetParameterValue("Rating", rateby);
                //if (M2BookRdBtn.Checked == true)
                //{
                //    theReport.SetParameterValue("Rating", rateby);
                //    theReport.SetParameterValue("RatingBy", "M2 Book/Actual");
                //    if (btnBook.Checked == true)
                //    {
                //        theReport.SetParameterValue("From", "Book");
                //        theReport.SetParameterValue("OrderBy", orderby);
                //    }
                //    else
                //    {
                //        theReport.SetParameterValue("From", "Actual");
                //        theReport.SetParameterValue("OrderBy", orderby);
                //    }
                //}

                //else
                //{
                //    theReport.SetParameterValue("Rating", rateby);
                //    theReport.SetParameterValue("RatingBy", "M2 Percentage");
                //    if (btnBook.Checked == true)
                //    {
                //        theReport.SetParameterValue("From", "Book");
                //        theReport.SetParameterValue("OrderBy", orderby);
                //    }
                //    else
                //    {
                //        theReport.SetParameterValue("From", "Actual");
                //        theReport.SetParameterValue("OrderBy", orderby);
                //    }
                //}
                if (reportSettings .RatingBy =="M2")
                {
                    theReport.SetParameterValue("Rating", rateby);
                    theReport.SetParameterValue("type", typeOfReport);

                    if (reportSettings .From =="Actual/Book" )
                    {
                        theReport.SetParameterValue("From", "Actual/Book");
                        theReport.SetParameterValue("OrderBy", orderby);
                        if (reportSettings.RatingBy == "M")
                        {
                            theReport.SetParameterValue("RatingBy", "M");
                        }
                        else
                        {
                            theReport.SetParameterValue("RatingBy", "M2");
                        }

                    }
                    else
                    {
                        theReport.SetParameterValue("From", "Actual/Planned");
                        theReport.SetParameterValue("OrderBy", orderby);
                        // theReport.SetParameterValue("RatingBy", "M2");
                        if (reportSettings.RatingBy == "M")
                        {
                            theReport.SetParameterValue("RatingBy", "M");
                        }
                        else
                        {
                            theReport.SetParameterValue("RatingBy", "M2");
                        }
                    }
                }

                else
                {
                    theReport.SetParameterValue("Rating", rateby);
                    theReport.SetParameterValue("type", typeOfReport);
                    if (reportSettings.RatingBy == "M2 Percentage")
                    {
                        theReport.SetParameterValue("RatingBy", "M2 Percentage");
                    }
                    else
                    {
                        theReport.SetParameterValue("RatingBy", "M Percentage");
                    }
                    //  theReport.SetParameterValue("RatingBy", "M2 Percentage");
                    if (reportSettings.From == "Actual/Book")
                    {
                        theReport.SetParameterValue("From", "Actual/Book");
                        theReport.SetParameterValue("OrderBy", orderby);
                    }
                    else
                    {
                        theReport.SetParameterValue("From", "Actual/Planned");
                        theReport.SetParameterValue("OrderBy", orderby);
                    }
                }

            }
            if (persect == "F")
            {
                if (reportSettings.Activity=="Stoping")
                {
                    theReport.Load(TGlobalItems.ReportsFolder + "\\CrewRankingReportNewNS.frx");
                }
                else
                {
                    theReport.Load(TGlobalItems.ReportsFolder + "\\CrewRankingReportNewNSDev.frx");
                }
                //if (rateby == "B" && orderby == "1 Month")
                //{
                //    //(theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", true));
                //    //(theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", true));
                //    (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", false));
                //}
                //if (rateby == "B" && orderby == "3 Month")
                //{
                //   // (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", true));
                //    (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", false));
                //   // (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", true));
                //}
                //if (rateby == "B" && orderby == "12 Month")
                //{
                //    (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.tot12mnthRank", false));
                //    //(theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.tot3mnthRank", true));
                //    //(theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.tot1mnthRank", true));
                //}



                //if (rateby == "P" && orderby == "1 Month")
                //{
                //    (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.SQM12Percentage", false));
                //    (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.SQM3Percentage", false));
                //    (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.SQM1Percentage", true));
                //}
                //if (rateby == "P" && orderby == "3 Month")
                //{
                //    (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.SQM12Percentage", false));
                //    (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.SQM3Percentage", true));
                //    (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.SQM1Percentage", false));
                //}
                //if (rateby == "P" && orderby == "12 Month")
                //{
                //    (theReport.FindObject("Data2") as DataBand).Sort.Add(new Sort("CrewPerformance 12MONTH.SQM12Percentage", true));
                //    (theReport.FindObject("Data3") as DataBand).Sort.Add(new Sort("CrewPerformance 3MONTH.SQM3Percentage", false));
                //    (theReport.FindObject("Data4") as DataBand).Sort.Add(new Sort("CrewPerformance 1MONTH.SQM1Percentage", false));
                //}
                theReport.SetParameterValue("sect", reportSettings.SectionID);
                theReport.SetParameterValue("Prodmonth", TProductionGlobal.ProdMonthAsString ( reportSettings.Prodmonth));
                theReport.SetParameterValue("Rating", rateby);
                if (reportSettings.RatingBy == "M2")
                {
                    theReport.SetParameterValue("Rating", rateby);
                    theReport.SetParameterValue("type", typeOfReport);

                    if (reportSettings.From == "Actual/Book")
                    {
                        theReport.SetParameterValue("From", "Actual/Book");
                        theReport.SetParameterValue("OrderBy", orderby);
                        if (reportSettings.RatingBy == "M")
                        {
                            theReport.SetParameterValue("RatingBy", "M");
                        }
                        else
                        {
                            theReport.SetParameterValue("RatingBy", "M2");
                        }
                    }
                    else
                    {
                        theReport.SetParameterValue("From", "Actual/Planned");
                        theReport.SetParameterValue("OrderBy", orderby);

                        if (reportSettings.RatingBy == "M")
                        {
                            theReport.SetParameterValue("RatingBy", "M");
                        }
                        else
                        {
                            theReport.SetParameterValue("RatingBy", "M2");
                        }
                    }
                }

                else
                {
                    theReport.SetParameterValue("Rating", rateby);
                    theReport.SetParameterValue("type", typeOfReport);
                    if (reportSettings.RatingBy == "M2 Percentage")
                    {
                        theReport.SetParameterValue("RatingBy", "M2 Percentage");
                    }
                    else
                    {
                        theReport.SetParameterValue("RatingBy", "M Percentage");
                    }
                    if (reportSettings.From == "Actual/Book")
                    {
                        theReport.SetParameterValue("From", "Actual/Book");
                        theReport.SetParameterValue("OrderBy", orderby);
                    }
                    else
                    {
                        theReport.SetParameterValue("From", "Actual/Planned");
                        theReport.SetParameterValue("OrderBy", orderby);
                    }
                }
                if (TParameters.DesignReport)
                {
                    theReport.Design();
                }

                theReport.Prepare();
                theReport.Refresh();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
            }
     }

        private void riProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            //int month12 = 0;
            //int month3 = 0;

            //PerfMonth3 = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);// r["pm"].ToString();
            //PerfMonth12 = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);// r["pm"].ToString();
            //                                            // //}


            //_theDate = Convert.ToDateTime(PerfMonth3.Substring(0, 4) + "-" + PerfMonth3.Substring(4, 2) + "-01");
            //_theDate = _theDate.AddMonths(-2);

            //if (_theDate.Month < 10)
            //    PerfMonth3 = Convert.ToString(_theDate.Year) + "0" + Convert.ToString(_theDate.Month);
            //else
            //    PerfMonth3 = Convert.ToString(_theDate.Year) + Convert.ToString(_theDate.Month);

            //_theDate = Convert.ToDateTime(PerfMonth12.Substring(0, 4) + "-" + PerfMonth12.Substring(4, 2) + "-01");
            //_theDate = _theDate.AddMonths(-11);

            //if (_theDate.Month < 10)
            //    PerfMonth12 = Convert.ToString(_theDate.Year) + "0" + Convert.ToString(_theDate.Month);
            //else
            //    PerfMonth12 = Convert.ToString(_theDate.Year) + Convert.ToString(_theDate.Month);
            PROD();
        }

        public void PROD()
        {
            int month12 = 0;
            int month3 = 0;
            pgCrewRankingRepSettings.PostEditor();
            PerfMonth3 = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);// r["pm"].ToString();
            PerfMonth12 = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);// r["pm"].ToString();
                                                                                        // //}


            _theDate = Convert.ToDateTime(PerfMonth3.Substring(0, 4) + "-" + PerfMonth3.Substring(4, 2) + "-01");
            _theDate = _theDate.AddMonths(-2);

            if (_theDate.Month < 10)
                PerfMonth3 = Convert.ToString(_theDate.Year) + "0" + Convert.ToString(_theDate.Month);
            else
                PerfMonth3 = Convert.ToString(_theDate.Year) + Convert.ToString(_theDate.Month);

            _theDate = Convert.ToDateTime(PerfMonth12.Substring(0, 4) + "-" + PerfMonth12.Substring(4, 2) + "-01");
            _theDate = _theDate.AddMonths(-11);

            if (_theDate.Month < 10)
                PerfMonth12 = Convert.ToString(_theDate.Year) + "0" + Convert.ToString(_theDate.Month);
            else
                PerfMonth12 = Convert.ToString(_theDate.Year) + Convert.ToString(_theDate.Month);
        }

        private void riActivity_EditValueChanged(object sender, EventArgs e)
        {
            pgCrewRankingRepSettings.PostEditor();
            if(iActivity .Properties .Value.ToString () =="Development")
            {
                riRatingBy.Items[1].Description = "M Percentage";
                riRatingBy.Items[0].Description = "M";
            }
            else
            {
                riRatingBy.Items[1].Description = "M2 Percentage";
                riRatingBy.Items[0].Description = "M2";
            }
        }
    }
}
