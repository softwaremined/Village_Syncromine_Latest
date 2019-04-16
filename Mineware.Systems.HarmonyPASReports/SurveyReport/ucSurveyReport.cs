using System;
using System.Threading;
using System.Data;
using FastReport;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global;
using System.Windows.Forms;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.SurveyReport
{
    public partial class ucSurveyReport : ucReportSettingsControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        clsSurveyReportSettings reportSettings = new clsSurveyReportSettings();
        clsSurveyReportData _clsSurveyReportData = new clsSurveyReportData();

        DataSet repSurveyDataSet = new DataSet();

        MWDataManager.clsDataAccess _dtStoping = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _dtReportData = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _dtStopingType = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _dtDev = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _dtDevCapWork = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _dtDevDumpMillPack = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _dtSweeps = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess _dtSweepsSum = new MWDataManager.clsDataAccess();
        private DataTable tblSignOffs = new DataTable("SignOffs");        

        private string theSystemDBTag = "DBHARMONYPAS";
        Report theReport = new Report();
        private Thread theReportThread;

        MWDataManager.clsDataAccess dtDev = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtDevCapWork = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtDevDumpMillPack = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtSweeps = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtSweepsSum = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtStp = new MWDataManager.clsDataAccess();

        private DataTable dtReefs;
        private DataTable dtShafts;
        private DataTable dtSignOffs;
        private DataTable dtCleanTypes;
        private DataTable dtSysset;
        private DataTable dtHier;
        private DataTable dtSections;
        private DataTable dtLevSum;

        private bool Stp_Found = false;
        private bool Dev_Found = false;
        private bool Old_Found = false;

        private string ReefsID;
        private string ReefsDesc;

        private string ShaftsID;
        private string ShaftsDesc;

        private string SignOffsID;
        private string SignOffsDesc;

        private string CleanTypeID;
        private string CleanTypeDesc;

        private bool ErrFound;

        private string _ProdMonth;
        private string _FromMonth;
        private string _ToMonth;
        private string _HierID;

        private int SelectLevel;

        public ucSurveyReport()
        {
            InitializeComponent();
        }
        private void ucSurveyReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = UserCurrentInfo;
            reportSettings.UserCurrentInfo = UserCurrentInfo.Connection;
            reportSettings.DBTag = theSystemDBTag;

            setReportSetttings();
        }
        private void setReportSetttings()
        {
            _clsSurveyReportData.DBTag = theSystemDBTag;
            reportSettings.LvlGM = true;
            reportSettings.LvlMM = true;
            reportSettings.LvlMNM = true;
            reportSettings.LvlMO = true;
            reportSettings.LvlSB = true;
            reportSettings.LvlMiner = true;
            reportSettings.LvlWP = true;

            reportSettings.DisplayID = false;
            reportSettings.DisplayName = false;
            reportSettings.DisplayIDName = true;

            reportSettings.Paid = false;
            reportSettings.Unpaid = false;
            reportSettings.PaidUnpaid = true;

            reportSettings.ReportTypeStp = true;
            reportSettings.ReportTypeDev = false;
            reportSettings.ReportTypeSweeps = false;
            reportSettings.ReportTypeTM = false;

            reportSettings.Reclamation = true;
            reportSettings.HighLight = false;

            _clsSurveyReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtSysset = _clsSurveyReportData.get_Sysset();
            if (dtSysset.Rows.Count > 0)
            {
                _ProdMonth = dtSysset.Rows[0]["CurrentProductionMonth"].ToString();
                _HierID = (Convert.ToInt32(dtSysset.Rows[0]["MOHierarchicalID"].ToString()) + 2).ToString();
            }
           
            int theYear = Convert.ToInt32(_ProdMonth.Substring(0, 4));
            int theMonth = Convert.ToInt32(_ProdMonth.Substring(4, 2));
            DateTime theDate = new DateTime(theYear, theMonth, 1);

            reportSettings.FromMonth = theDate.Date;
            reportSettings.ToMonth = theDate.Date;
            _FromMonth = string.Format("{0:yyyyMM}", reportSettings.FromMonth);
            _ToMonth = string.Format("{0:yyyyMM}", reportSettings.ToMonth);

            setScreenSelection();
            Load_LevelOfSummaries();
            Load_Reefs();
            reportSettings.ReefDesc = ReefsDesc;
            Load_Shafts();
            reportSettings.ShaftDesc = ShaftsDesc;
            Load_SignOffs();
            reportSettings.SignOffsDesc = SignOffsDesc;
            Load_CleanTypes();
            reportSettings.CleanTypeDesc = CleanTypeDesc;

            Load_Sections();

            pgSettingsMain.SelectedObject = reportSettings;
            pgSettingsMain.PostEditor();
        }
        private void setScreenSelection()
        {
            rowFromMonth.Properties.Value = reportSettings.FromMonth;
            rowToMonth.Properties.Value = reportSettings.ToMonth;

            rowSectionID.Properties.Value = reportSettings.SectionID;

            rowGM.Properties.Value = reportSettings.LvlGM;
            rowMM.Properties.Value = reportSettings.LvlMM;
            rowMNM.Properties.Value = reportSettings.LvlMNM;
            rowMO.Properties.Value = reportSettings.LvlMO;
            rowSB.Properties.Value = reportSettings.LvlSB;
            rowMiner.Properties.Value = reportSettings.LvlMiner;
            rowWP.Properties.Value = reportSettings.LvlWP;


            rowGM.Enabled = true;
            rowMM.Enabled = true;
            rowMNM.Enabled = true;
            rowMO.Enabled = true;
            rowSB.Enabled = true;
            rowMiner.Enabled = true;
            rowWP.Enabled = true;

            rowReefs.Properties.Value = reportSettings.ReefDesc;
            rowShafts.Properties.Value = reportSettings.ShaftDesc;
            rowSignOffs.Properties.Value = reportSettings.SignOffsDesc;
            rowCleanTypes.Properties.Value = reportSettings.CleanTypeDesc;

            rowDisplayID.Properties.Value = reportSettings.DisplayID;
            rowDisplayName.Properties.Value = reportSettings.DisplayName;
            rowDisplayIDName.Properties.Value = reportSettings.DisplayIDName;

            rowReportTypeStp.Properties.Value = reportSettings.ReportTypeStp;
            rowReportTypeDev.Properties.Value = reportSettings.ReportTypeDev;
            rowReportTypeSweeps.Properties.Value = reportSettings.ReportTypeSweeps;
            rowReportTypeTM.Properties.Value = reportSettings.ReportTypeTM;
            rowCleanTypes.Enabled = false;

            rowPaid.Properties.Value = reportSettings.Paid;
            rowUnpaid.Properties.Value = reportSettings.Unpaid;
            rowPaidUnPaid.Properties.Value = reportSettings.PaidUnpaid;

            rowReclamation.Properties.Value = reportSettings.Reclamation;
            rowHighLight.Properties.Value = reportSettings.HighLight;
        }

        public override bool prepareReport()
        {
            bool theResult = false;

            ErrFound = false;
            CheckForErrors();

            if (ErrFound == false)
            {
                LoadReportData();
            }

            if (ErrFound == false)
            {
                theReportThread = new Thread(new ParameterizedThreadStart(createReport));
                theReportThread.SetApartmentState(ApartmentState.STA);
                theReportThread.Start(reportSettings);
                theResult = true;
            }
            return theResult;
        }
        private void LoadReportData()
        {
            string TheReportType = "";
            if (reportSettings.ReportTypeStp == true)
                TheReportType = "Stoping";
            if (reportSettings.ReportTypeDev == true)
                TheReportType = "Development";
            if (reportSettings.ReportTypeSweeps == true)
                TheReportType = "Sweepings";
            if (reportSettings.ReportTypeTM == true)
                TheReportType = "Total Mine";

            string TheMonth = "";
            if (reportSettings.FromMonth == reportSettings.ToMonth)
                TheMonth = "For Month : " + reportSettings.FromMonth.ToString("MMM-yyyy");
            else
                TheMonth = "For Month : " + reportSettings.FromMonth.ToString("MMM-yyyy") + " to " + reportSettings.ToMonth.ToString("MMM-yyyy");

            string ThePaid = "";
            if (reportSettings.Paid == true)
                ThePaid = "Paid";
            if (reportSettings.Unpaid == true)
                ThePaid = "UnPaid";
            if (reportSettings.PaidUnpaid == true)
                ThePaid = "Paid and Unpaid";

            string TheSummary = "";
            if (reportSettings.ReportTypeTM == false)
            {
                if (reportSettings.LvlGM == true)
                    TheSummary = TheSummary + rowGM.Properties.Caption + ", ";
                if (reportSettings.LvlMM == true)
                    TheSummary = TheSummary + rowMM.Properties.Caption + ", ";
                if (reportSettings.LvlMNM == true)
                    TheSummary = TheSummary + rowMNM.Properties.Caption + ", ";
                if (reportSettings.LvlMO == true)
                    TheSummary = TheSummary + rowMO.Properties.Caption + ", ";
                if (reportSettings.LvlSB == true)
                    TheSummary = TheSummary + rowSB.Properties.Caption + ", ";
                if (reportSettings.LvlMiner == true)
                    TheSummary = TheSummary + rowMiner.Properties.Caption + ", ";
                if (reportSettings.LvlWP == true)
                    TheSummary = TheSummary + "Workplace, ";
                TheSummary = TheSummary.Remove(TheSummary.Length - 2, 2);
            }

            string SelectBy = "";
            if (SelectLevel == 1)
                SelectBy = "sc.SectionID_5";
            if (SelectLevel == 2)
                SelectBy = "sc.SectionID_4";
            if (SelectLevel == 3)
                SelectBy = "sc.SectionID_3";
            if (SelectLevel == 4)
                SelectBy = "sc.SectionID_2";
            if (SelectLevel == 5)
                SelectBy = "sc.SectionID_1";
            if (SelectLevel == 6)
                SelectBy = "sc.SectionID";

            Load_SelectedReefs();
            Load_SelectedShafts();
            Load_SelectedSignOffs();
            Load_SelectedCleanTypes();

            _dtReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dtStoping.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dtStopingType.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dtDev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dtDevCapWork.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dtDevDumpMillPack.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dtSweeps.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dtSweepsSum.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dtReportData.SqlStatement = " select '" + reportSettings.SectionID + "' TheSection, \r\n " +
                " '" + TheReportType + "' TheReportType, \r\n " +
                " '" + TheMonth + "' TheMonth, \r\n " +
                " '" + ThePaid + "' ThePaid, \r\n " +
                " '" + ReefsDesc + "' TheReefs, \r\n " +
                " '" + TheSummary + "' TheSummary, \r\n " +
                " soMM, soPM, soMO, soPlan, soRM, soEval, soSurv, soGeol, \r\n ";
            if (reportSettings.Reclamation == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 Reclamation, ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 Reclamation, \r\n ";
            if (reportSettings.HighLight == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 HighLines, ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 HighLines, \r\n ";
            if (reportSettings.LvlGM == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 MM, ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 MM, ";
            if (reportSettings.LvlMM == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 PM, ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 PM, ";
            if (reportSettings.LvlMNM == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 UM, ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 UM, ";
            if (reportSettings.LvlMO == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 MO, ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 MO, ";
            if (reportSettings.LvlSB == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 SB, ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 SB, ";
            if (reportSettings.LvlMiner == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 Miner, ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 Miner, ";
            if (reportSettings.LvlWP == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 WP, \r\n ";
            else
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 WP, \r\n ";

            if (reportSettings.ReportTypeStp == true)
                _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  0 Activity, \r\n ";
            else
                if (reportSettings.ReportTypeDev == true)
                    _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  1 Activity, \r\n ";
                else
                    if (reportSettings.ReportTypeSweeps == true)
                        _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  2 Activity, \r\n ";
                    else
                        _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  3 Activity, \r\n ";
            _dtReportData.SqlStatement = _dtReportData.SqlStatement + "  Banner Banner from Sysset ";
            _dtReportData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dtReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dtReportData.ResultsTableName = "ReportData";
            _dtReportData.ExecuteInstruction();

            if ((reportSettings.ReportTypeStp == true) |
                (reportSettings.ReportTypeTM == true))
            {
                _dtStoping.SqlStatement = "exec sp_SurveyReport_Stoping  ";
                if (reportSettings.ReportTypeStp == true)
                    _dtStoping.SqlStatement = _dtStoping.SqlStatement + " '1', ";
                else
                    _dtStoping.SqlStatement = _dtStoping.SqlStatement + " '4', ";
                if (reportSettings.Reclamation == true)
                    _dtStoping.SqlStatement = _dtStoping.SqlStatement + " 'Y', ";
                else
                    _dtStoping.SqlStatement = _dtStoping.SqlStatement + " 'N', ";
                if (reportSettings.DisplayID == true)
                    _dtStoping.SqlStatement = _dtStoping.SqlStatement + " '1', ";
                else
                    if (reportSettings.DisplayIDName == true)
                        _dtStoping.SqlStatement = _dtStoping.SqlStatement + " '2', ";
                    else
                        if (reportSettings.DisplayName == true)
                            _dtStoping.SqlStatement = _dtStoping.SqlStatement + " '3', ";
                if (reportSettings.Paid == true)
                    _dtStoping.SqlStatement = _dtStoping.SqlStatement + " '1', ";
                else
                    if (reportSettings.Unpaid == true)
                        _dtStoping.SqlStatement = _dtStoping.SqlStatement + " '2', ";
                    else
                        if (reportSettings.PaidUnpaid == true)
                            _dtStoping.SqlStatement = _dtStoping.SqlStatement + " '3', ";
                _dtStoping.SqlStatement = _dtStoping.SqlStatement +
                     " '" + _FromMonth + "', '" + _ToMonth + "', " +
                     " '" + ReefsID + "', '" + ShaftsID + "', " +
                     " '" + reportSettings.SectionID + "', " +
                     " '" + SelectBy + "' ";
                _dtStoping.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dtStoping.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dtStoping.ResultsTableName = "Survey_Stoping";
                clsDataResult errorMsg = _dtStoping.ExecuteInstruction(); 

                if (_dtStoping.ResultsDataTable.Rows.Count == 0)
                {
                    _dtStoping.SqlStatement = "exec sp_SurveyReport_Stoping_Zeroes ";
                    _dtStoping.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtStoping.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtStoping.ResultsTableName = "Survey_Stoping";
                    _dtStoping.ExecuteInstruction();

                    _dtStopingType.SqlStatement = "exec sp_SurveyReport_StopeType_Zeroes ";
                    _dtStopingType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtStopingType.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtStopingType.ResultsTableName = "Survey_StopingType2";
                    clsDataResult errorMsg1 = _dtStopingType.ExecuteInstruction();                        
                }
                else
                { 
                    Stp_Found = true;

                    _dtStopingType.SqlStatement = "exec sp_SurveyReport_StopingType  ";
                    if (reportSettings.Reclamation == true)
                        _dtStopingType.SqlStatement = _dtStopingType.SqlStatement + " 'Y', ";
                    else
                        _dtStopingType.SqlStatement = _dtStopingType.SqlStatement + " 'N', ";
                    if (reportSettings.Paid == true)
                        _dtStopingType.SqlStatement = _dtStopingType.SqlStatement + " '1', ";
                    else
                        if (reportSettings.Unpaid == true)
                        _dtStopingType.SqlStatement = _dtStopingType.SqlStatement + " '2', ";
                    else
                            if (reportSettings.PaidUnpaid == true)
                        _dtStopingType.SqlStatement = _dtStopingType.SqlStatement + " '3', ";
                    _dtStopingType.SqlStatement = _dtStopingType.SqlStatement +
                         " '" + _FromMonth + "', '" + _ToMonth + "', " +
                         " '" + ReefsID + "', '" + ShaftsID + "', " +
                         " '" + reportSettings.SectionID + "', " +
                         " '" + SelectBy + "' ";
                    _dtStopingType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtStopingType.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtStopingType.ResultsTableName = "Survey_StopingType2";
                    clsDataResult errorMsg2 = _dtStopingType.ExecuteInstruction();

                    if (_dtStopingType.ResultsDataTable.Rows.Count == 0)
                    {
                        _dtStopingType.SqlStatement = "exec sp_SurveyReport_StopeType_Zeroes ";
                        _dtStopingType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dtStopingType.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dtStopingType.ResultsTableName = "Survey_StopingType2";
                        clsDataResult errorMsg1 = _dtStopingType.ExecuteInstruction();
                    }
                }
                if (reportSettings.ReportTypeStp == true)
                {
                    _dtDev.SqlStatement = "exec sp_SurveyReport_Development_Zeroes ";
                    _dtDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDev.ResultsTableName = "Survey_Development";
                    clsDataResult errorMsg3 = _dtDev.ExecuteInstruction();

                    _dtDevCapWork.SqlStatement = "exec sp_SurveyReport_DevCapOngoingWork_Zeroes ";
                    _dtDevCapWork.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDevCapWork.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDevCapWork.ResultsTableName = "Survey_DevCapOngoingWork";
                    clsDataResult errorMsg4 = _dtDevCapWork.ExecuteInstruction();

                    _dtDevDumpMillPack.SqlStatement = "exec sp_SurveyReport_DevDumpMillPack_Zeroes ";
                    _dtDevDumpMillPack.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDevDumpMillPack.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDevDumpMillPack.ResultsTableName = "Survey_DevDumpMillPack";
                    clsDataResult errorMsg5 = _dtDevDumpMillPack.ExecuteInstruction();

                    _dtSweeps.SqlStatement = "exec sp_SurveyReport_Sweepings_Zeroes ";
                    _dtSweeps.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtSweeps.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtSweeps.ResultsTableName = "Survey_Sweepings";
                    clsDataResult errorMsg6 = _dtSweeps.ExecuteInstruction();

                    _dtSweepsSum.SqlStatement = "exec sp_SurveyReport_SweepingsSum_Zeroes ";
                    _dtSweepsSum.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtSweepsSum.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtSweepsSum.ResultsTableName = "Survey_SweepingsSum";
                    clsDataResult errorMsg7 = _dtSweepsSum.ExecuteInstruction();
                } 
            }
            if ((reportSettings.ReportTypeDev == true) |
                (reportSettings.ReportTypeTM == true))
            {
                _dtDev.SqlStatement = "exec sp_SurveyReport_Development  ";
                if (reportSettings.ReportTypeDev == true)
                    _dtDev.SqlStatement = _dtDev.SqlStatement + " '2', ";
                else
                    _dtDev.SqlStatement = _dtDev.SqlStatement + " '4', ";
                if (reportSettings.DisplayID == true)
                    _dtDev.SqlStatement = _dtDev.SqlStatement + " '1', ";
                else
                    if (reportSettings.DisplayIDName == true)
                        _dtDev.SqlStatement = _dtDev.SqlStatement + " '2', ";
                    else
                        if (reportSettings.DisplayName == true)
                            _dtDev.SqlStatement = _dtDev.SqlStatement + " '3', ";
                if (reportSettings.Paid == true)
                    _dtDev.SqlStatement = _dtDev.SqlStatement + " '1', ";
                else
                    if (reportSettings.Unpaid == true)
                    _dtDev.SqlStatement = _dtDev.SqlStatement + " '2', ";
                else
                        if (reportSettings.PaidUnpaid == true)
                    _dtDev.SqlStatement = _dtDev.SqlStatement + " '3', ";
                _dtDev.SqlStatement = _dtDev.SqlStatement +
                     " '" + _FromMonth + "', '" + _ToMonth + "', " +
                     " '" + ReefsID + "', '" + ShaftsID + "', " +
                     " '" + reportSettings.SectionID + "', " +
                     " '" + SelectBy + "' ";
                _dtDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dtDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dtDev.ResultsTableName = "Survey_Development";
                clsDataResult errorMsg = _dtDev.ExecuteInstruction();

                DataTable SurvDev = _dtDev.ResultsDataTable;

                if (SurvDev.Rows.Count == 0)
                {
                    _dtDev.SqlStatement = "exec sp_SurveyReport_Development_Zeroes ";
                    _dtDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDev.ResultsTableName = "Survey_Development";
                    clsDataResult errorMsg1 = _dtDev.ExecuteInstruction();
                }
                else
                {
                    Dev_Found = true;
 
                    _dtDevCapWork.SqlStatement = "exec sp_SurveyReport_DevCapOngoingWork  ";
                    if (reportSettings.Paid == true)
                        _dtDevCapWork.SqlStatement = _dtDevCapWork.SqlStatement + " '1', ";
                    else
                        if (reportSettings.Unpaid == true)
                        _dtDevCapWork.SqlStatement = _dtDevCapWork.SqlStatement + " '2', ";
                    else
                            if (reportSettings.PaidUnpaid == true)
                        _dtDevCapWork.SqlStatement = _dtDevCapWork.SqlStatement + " '3', ";
                    _dtDevCapWork.SqlStatement = _dtDevCapWork.SqlStatement +
                         " '" + _FromMonth + "', '" + _ToMonth + "', " +
                         " '" + ReefsID + "', '" + ShaftsID + "', " +
                         " '" + reportSettings.SectionID + "', " +
                         " '" + SelectBy + "' ";
                    _dtDevCapWork.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDevCapWork.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDevCapWork.ResultsTableName = "Survey_DevCapOngoingWork";
                    clsDataResult errorMsg2 = _dtDevCapWork.ExecuteInstruction();

                    DataTable Surv_DevCapOnGoingWork = _dtDevCapWork.ResultsDataTable;

                    if (Surv_DevCapOnGoingWork.Rows.Count == 0)
                    {
                        _dtDevCapWork.SqlStatement = "exec sp_SurveyReport_DevCapOngoingWork_Zeroes ";
                        _dtDevCapWork.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dtDevCapWork.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dtDevCapWork.ResultsTableName = "Survey_DevCapOngoingWork";
                        clsDataResult errorMsg3 = _dtDevCapWork.ExecuteInstruction();
                    }

                    _dtDevDumpMillPack.SqlStatement = "exec sp_SurveyReport_DevDumpMillPack  ";
                    if (reportSettings.Paid == true)
                        _dtDevDumpMillPack.SqlStatement = _dtDevDumpMillPack.SqlStatement + " '1', ";
                    else
                        if (reportSettings.Unpaid == true)
                        _dtDevDumpMillPack.SqlStatement = _dtDevDumpMillPack.SqlStatement + " '2', ";
                    else
                            if (reportSettings.PaidUnpaid == true)
                        _dtDevDumpMillPack.SqlStatement = _dtDevDumpMillPack.SqlStatement + " '3', ";
                    _dtDevDumpMillPack.SqlStatement = _dtDevDumpMillPack.SqlStatement +
                         " '" + _FromMonth + "', '" + _ToMonth + "', " +
                         " '" + ReefsID + "', '" + ShaftsID + "', " +
                         " '" + reportSettings.SectionID + "', " +
                         " '" + SelectBy + "' ";
                    _dtDevDumpMillPack.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDevDumpMillPack.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDevDumpMillPack.ResultsTableName = "Survey_DevDumpMillPack";
                    clsDataResult errorMsg4 = _dtDevDumpMillPack.ExecuteInstruction();

                    DataTable Surv_DevDumpMillPack = _dtDevDumpMillPack.ResultsDataTable;

                    if (Surv_DevDumpMillPack.Rows.Count == 0)
                    {
                        _dtDevDumpMillPack.SqlStatement = "exec sp_SurveyReport_DevDumpMillPack_Zeroes ";
                        _dtDevDumpMillPack.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dtDevDumpMillPack.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dtDevDumpMillPack.ResultsTableName = "Survey_DevDumpMillPack";
                        clsDataResult errorMsg5 = _dtDevDumpMillPack.ExecuteInstruction();
                    }
                }

                if (reportSettings.ReportTypeDev == true)
                {
                    _dtStoping.SqlStatement = "exec sp_SurveyReport_Stoping_Zeroes ";
                    _dtStoping.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtStoping.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtStoping.ResultsTableName = "Survey_Stoping";
                    clsDataResult errorMsg6 = _dtStoping.ExecuteInstruction();

                    _dtSweeps.SqlStatement = "exec sp_SurveyReport_Sweepings_Zeroes ";
                    _dtSweeps.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtSweeps.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtSweeps.ResultsTableName = "Survey_Sweepings";
                    clsDataResult errorMsg7 = _dtSweeps.ExecuteInstruction();

                    _dtSweepsSum.SqlStatement = "exec sp_SurveyReport_SweepingsSum_Zeroes ";
                    _dtSweepsSum.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtSweepsSum.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtSweepsSum.ResultsTableName = "Survey_SweepingsSum";
                    clsDataResult errorMsg8 = _dtSweepsSum.ExecuteInstruction();

                    _dtStopingType.SqlStatement = "exec sp_SurveyReport_StopeType_Zeroes ";
                    _dtStopingType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtStopingType.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtStopingType.ResultsTableName = "Survey_StopingType2";
                    clsDataResult errorMsg9 = _dtStopingType.ExecuteInstruction();
                }
            }//////// END DEVELOPMENT    END DEVELOPMENT

            if ((reportSettings.ReportTypeSweeps == true) |
                (reportSettings.ReportTypeTM == true))
            {
                _dtSweeps.SqlStatement = "exec sp_SurveyReport_Sweepings  ";
                if (reportSettings.ReportTypeSweeps == true)
                    _dtSweeps.SqlStatement = _dtSweeps.SqlStatement + " '3', ";
                else
                    _dtSweeps.SqlStatement = _dtSweeps.SqlStatement + " '4', ";
                if (reportSettings.DisplayID == true)
                    _dtSweeps.SqlStatement = _dtSweeps.SqlStatement + " '1', ";
                else
                    if (reportSettings.DisplayIDName == true)
                        _dtSweeps.SqlStatement = _dtSweeps.SqlStatement + " '2', ";
                    else
                        if (reportSettings.DisplayName == true)
                            _dtSweeps.SqlStatement = _dtSweeps.SqlStatement + " '3', ";
                _dtSweeps.SqlStatement = _dtSweeps.SqlStatement +
                         " '" + _FromMonth + "', '" + _ToMonth + "', " +
                         " '" + ReefsID + "', '" + ShaftsID + "', '" + CleanTypeID + "'," +
                         " '" + reportSettings.SectionID + "', " +
                         " '" + SelectBy + "' ";
                _dtSweeps.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dtSweeps.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dtSweeps.ResultsTableName = "Survey_Sweepings";
                clsDataResult errorMsg = _dtSweeps.ExecuteInstruction();

                DataTable Surv_Sweeps = _dtSweeps.ResultsDataTable;

                if (_dtSweeps.ResultsDataTable.Rows.Count == 0)
                {
                    _dtSweeps.SqlStatement = "exec sp_SurveyReport_Sweepings_Zeroes ";
                    _dtSweeps.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtSweeps.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtSweeps.ResultsTableName = "Survey_Sweepings";
                    clsDataResult errorMsg1 = _dtSweeps.ExecuteInstruction();
                }
                else
                { 
                    Old_Found = true;

                    _dtSweepsSum.SqlStatement = "exec sp_SurveyReport_SweepingsSum  ";
                    _dtSweepsSum.SqlStatement = _dtSweepsSum.SqlStatement +
                         " '" + _FromMonth + "', '" + _ToMonth + "', " +
                         " '" + ReefsID + "', '" + ShaftsID + "', " +
                         " '" + CleanTypeID + "', " +
                         " '" + reportSettings.SectionID + "', " +
                         " '" + SelectBy + "' ";
                    _dtSweepsSum.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtSweepsSum.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtSweepsSum.ResultsTableName = "Survey_SweepingsSum";
                    clsDataResult errorMsg2 = _dtSweepsSum.ExecuteInstruction();

                    DataTable Surv_SweepsSum = _dtSweepsSum.ResultsDataTable;

                    if (Surv_SweepsSum.Rows.Count == 0)
                    {
                        _dtSweepsSum.SqlStatement = "exec sp_SurveyReport_SweepingsSum_Zeroes ";
                        _dtSweepsSum.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dtSweepsSum.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dtSweepsSum.ResultsTableName = "Survey_SweepingsSum";
                        clsDataResult errorMsg3 = _dtSweepsSum.ExecuteInstruction();
                    }
                }

                if (reportSettings.ReportTypeSweeps == true)
                {
                    _dtDev.SqlStatement = "exec sp_SurveyReport_Development_Zeroes ";
                    _dtDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDev.ResultsTableName = "Survey_Development";
                    clsDataResult errorMsg4 = _dtDev.ExecuteInstruction();

                    _dtDevCapWork.SqlStatement = "exec sp_SurveyReport_DevCapOngoingWork_Zeroes ";
                    _dtDevCapWork.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDevCapWork.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDevCapWork.ResultsTableName = "Survey_DevCapOngoingWork";
                    clsDataResult errorMsg5 = _dtDevCapWork.ExecuteInstruction();

                    _dtDevDumpMillPack.SqlStatement = "exec sp_SurveyReport_DevDumpMillPack_Zeroes ";
                    _dtDevDumpMillPack.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtDevDumpMillPack.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtDevDumpMillPack.ResultsTableName = "Survey_DevDumpMillPack";
                    clsDataResult errorMsg6 = _dtDevDumpMillPack.ExecuteInstruction();

                    _dtStoping.SqlStatement = "exec sp_SurveyReport_Stoping_Zeroes ";
                    _dtStoping.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtStoping.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtStoping.ResultsTableName = "Survey_Stoping";
                    clsDataResult errorMsg7 = _dtStoping.ExecuteInstruction();

                    _dtStopingType.SqlStatement = "exec sp_SurveyReport_StopeType_Zeroes ";
                    _dtStopingType.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dtStopingType.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dtStopingType.ResultsTableName = "Survey_StopingType2";
                    clsDataResult errorMsg8 = _dtStopingType.ExecuteInstruction();
                }
            }

            tblSignOffs.Clear();
            tblSignOffs.Reset();
            tblSignOffs.Columns.Add("SignOff");
            int i;
            for (i = 0; i <= rpSignOffs.Items.Count - 1; i++)
            {
                if (rpSignOffs.Items[i].CheckState.ToString() == "Checked")
                {
                    DataRow drNew = tblSignOffs.NewRow();
                    drNew["SignOff"] = rpSignOffs.Items[i].Value.ToString();
                    tblSignOffs.Rows.Add(drNew);
                    //SelectCleanType = SelectCleanType + rpSignOffs.Items[i].Value.ToString() + ",";
                }
            }
            tblSignOffs.AcceptChanges();
            //repSurveyDataSet.Tables.Add(tblSignOffs);
            if ((Stp_Found == false) &&
                (Dev_Found == false) &&
                (Old_Found == false))
            {
                ErrFound = true;
                _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", "No data found for your selection", ButtonTypes.OK, MessageDisplayType.Small);

            }
        }
        private void createReport(Object theReportSettings)
        {
            repSurveyDataSet.Tables.Clear();
          
            repSurveyDataSet.Tables.Add(_dtReportData.ResultsDataTable);
            repSurveyDataSet.Tables.Add(_dtStoping.ResultsDataTable);
            repSurveyDataSet.Tables.Add(_dtStopingType.ResultsDataTable);
            repSurveyDataSet.Tables.Add(_dtDev.ResultsDataTable);
            repSurveyDataSet.Tables.Add(_dtDevCapWork.ResultsDataTable);
            repSurveyDataSet.Tables.Add(_dtDevDumpMillPack.ResultsDataTable);
            repSurveyDataSet.Tables.Add(_dtSweeps.ResultsDataTable);
            repSurveyDataSet.Tables.Add(_dtSweepsSum.ResultsDataTable);
            repSurveyDataSet.Tables.Add(tblSignOffs);

                
            theReport.RegisterData(repSurveyDataSet);
            theReport.Load(TGlobalItems.ReportsFolder + "\\Survey_ProductionResults.frx");
            theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
            theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);
            //theReport.Design();
            if (TParameters.DesignReport == true)
            {
                theReport.Design();
            }
            theReport.Prepare();
            theReport.PrintSettings.PrintMode = PrintMode.Scale;
            theReport.SetParameterValue("ReportProperties", theReportSettings);
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;
        }
        private void CheckForErrors()
        {
            if (Convert.ToInt32(_FromMonth) > Convert.ToInt32(_ToMonth))
            {
                _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", "To Production Month cannot be smaller than From Production Month ", ButtonTypes.OK, MessageDisplayType.Small);
                ErrFound = true;
            }
            if (ErrFound == false)
            {
                if (reportSettings.SectionID != null)
                {
                    if (reportSettings.SectionID == "")
                    {
                        _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", "Please select a Section ", ButtonTypes.OK, MessageDisplayType.Small);
                        ErrFound = true;
                    }
                }
                else
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", "Please select a Section ", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }
            if (ErrFound == false)
            {
                if ((reportSettings.LvlGM == false) && (reportSettings.LvlMM == false) &&
                    (reportSettings.LvlMNM == false) && (reportSettings.LvlMO == false) &&
                    (reportSettings.LvlSB == false) && (reportSettings.LvlMiner == false) &&
                    (reportSettings.LvlWP == false))
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", " Please select a Level of summary for the report ", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }

            if (ErrFound == false)
            {
                if ((reportSettings.DisplayID == false) && (reportSettings.DisplayIDName == false) &&
                    (reportSettings.DisplayName == false))
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", " Please select a Display report ", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }

            if (ErrFound == false)
            {
                if (reportSettings.ReportTypeSweeps == false)
                {
                    if ((reportSettings.Paid == false) && (reportSettings.Unpaid == false) &&
                        (reportSettings.PaidUnpaid == false))
                    {
                        _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", " Please select a Paid / Unpaid / Paid and Unpaid ", ButtonTypes.OK, MessageDisplayType.Small);
                        ErrFound = true;
                    }
                }
            }

            if (ErrFound == false)
            {
                if ((reportSettings.ReportTypeStp == false) && (reportSettings.ReportTypeDev == false) &&
                    (reportSettings.ReportTypeSweeps == false) && (reportSettings.ReportTypeTM == false))
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", " Please select a Report Type ", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }
            if (ErrFound == false)
            {
                if (ReefsDesc == "")
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", " Please select a Reef Type ", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }

            if (ErrFound == false)
            {
                if (ShaftsDesc == "")
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", " Please select a Shaft Type ", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }
            if (ErrFound == false)
            {
                if (reportSettings.ReportTypeSweeps == true)
                    if (CleanTypeDesc == "")
                    {
                        _sysMessagesClass.viewMessage(MessageType.Info, "Survey Report", " Please select a Cleaning Type ", ButtonTypes.OK, MessageDisplayType.Small);
                        ErrFound = true;
                    }
            }
        }
        private void Load_Sections()
        {
            _clsSurveyReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtSections = _clsSurveyReportData.get_Sections(_FromMonth, _ToMonth, _HierID);

            rpSectionID.DataSource = dtSections;
            rpSectionID.DisplayMember = "SectionName";
            rpSectionID.ValueMember = "SectionID";
        }
        private void Load_Reefs()
        {
            _clsSurveyReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtReefs = _clsSurveyReportData.get_Reefs();

            bool foundReefs = false;
            ReefsID = "(";
            ReefsDesc = "";
            rpReefs.ResetEvents();
            rpReefs.Items.Clear();
            foreach (DataRow rr in dtReefs.Rows)
            {
                ReefsID = ReefsID + rr["ReefID"].ToString() + ",";
                ReefsDesc = ReefsDesc + rr["ReefDesc"].ToString() + ",";

                //rpReefs.Items.Add(rr["ReefDesc"].ToString(), CheckState.Unchecked, true);
                rpReefs.Items.Add(rr["ReefDesc"].ToString(), CheckState.Checked, true);
                foundReefs = true;
            }
            if (foundReefs == true)
            {
                ReefsID = ReefsID.Remove(ReefsID.Length - 1, 1);
                ReefsID = ReefsID + ")";
                ReefsDesc = ReefsDesc.Remove(ReefsDesc.Length - 1, 1);
            }
            else
            {
                ReefsID = "";
                ReefsDesc = "";
            }
            reportSettings.ReefDesc = ReefsDesc;
            rowReefs.Properties.Value = reportSettings.ReefDesc;
            pgSettingsMain.PostEditor();
        }
        private void Load_SelectedReefs()
        {
            bool foundReefs = false;
            ReefsID = "(";
            ReefsDesc = "";
            int i;
            for (i = 1; i <= rpReefs.Items.Count; i++)
            {
                //MessageBox.Show(rpReefs.Items[i - 1].Value.ToString());
                //MessageBox.Show(rpReefs.Items[i - 1].CheckState.ToString());
                if (rpReefs.Items[i-1].CheckState.ToString() == "Checked")
                {
                    DataRow[] EditedRow = dtReefs.Select(" ReefDesc = '" + rpReefs.Items[i-1].Value.ToString() + "' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        ReefsID = ReefsID + dr["ReefID"].ToString() + ",";
                        ReefsDesc = ReefsDesc + rpReefs.Items[i-1].Value.ToString() + ",";
                        foundReefs = true;
                    }
                }
            }
            if (foundReefs == true)
            {
                ReefsID = ReefsID.Remove(ReefsID.Length - 1, 1);
                ReefsID = ReefsID + ")";
                ReefsDesc = ReefsDesc.Remove(ReefsDesc.Length - 1, 1);
            }
            else
            {
                ReefsID = "";
                ReefsDesc = "";
            }
        }

        private void Load_Shafts()
        {
            _clsSurveyReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtShafts = _clsSurveyReportData.get_Shafts();

            bool foundShafts = false;
            ShaftsID = "(''";
            ShaftsDesc = "";
            rpShafts.ResetEvents();
            rpShafts.Items.Clear();
            foreach (DataRow rr in dtShafts.Rows)
            {
                ShaftsID = ShaftsID + rr["OreflowID"].ToString() + "'',''";
                ShaftsDesc = ShaftsDesc + rr["ShaftDesc"].ToString() + ",";

                rpShafts.Items.Add(rr["ShaftDesc"].ToString(), CheckState.Checked, true);
                foundShafts = true;
            }
            if (foundShafts == true)
            {
                ShaftsID = ShaftsID.Remove(ShaftsID.Length - 3, 3);
                ShaftsID = ShaftsID + ")";
                ShaftsDesc = ShaftsDesc.Remove(ShaftsDesc.Length - 1, 1);
            }
            else
            {
                ShaftsID = "";
                ShaftsDesc = "";
            }
            reportSettings.ShaftDesc = ShaftsDesc;
            rowShafts.Properties.Value = reportSettings.SignOffsDesc;
            pgSettingsMain.PostEditor();
        }
        private void Load_SelectedShafts()
        {
            bool foundShafts = false;
            ShaftsID = "(''";
            ShaftsDesc = "";
            int i;
            for (i = 1; i <= rpShafts.Items.Count; i++)
            {
                if (rpShafts.Items[i-1].CheckState.ToString() == "Checked")
                {
                    DataRow[] EditedRow = dtShafts.Select(" ShaftDesc = '" + rpShafts.Items[i-1].Value.ToString() + "' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        ShaftsID = ShaftsID + dr["OreflowID"].ToString() + "'',''";
                        ShaftsDesc = ShaftsDesc + rpShafts.Items[i-1].Value.ToString() + ",";
                        foundShafts = true;
                    }
                }
            }
            if (foundShafts == true)
            {
                ShaftsID = ShaftsID.Remove(ShaftsID.Length - 3, 3);
                ShaftsID = ShaftsID + ")";
                ShaftsDesc = ShaftsDesc.Remove(ShaftsDesc.Length - 1, 1);
            }
            else
            {
                ShaftsID = "";
                ShaftsDesc = "";
            }
        }
        private void Load_SignOffs()
        {
            _clsSurveyReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtSignOffs = _clsSurveyReportData.get_SignOffs();

            bool foundSignOffs = false;
            SignOffsID = "(";
            SignOffsDesc = "";
            rpSignOffs.ResetEvents();
            rpSignOffs.Items.Clear();
            foreach (DataRow rr in dtSignOffs.Rows)
            {
                SignOffsID = SignOffsID + rr["OrderBy"].ToString() + ",";
                SignOffsDesc = SignOffsDesc + rr["SignDesc"].ToString() + ",";

                rpSignOffs.Items.Add(rr["SignDesc"].ToString(), CheckState.Checked, true);
            }
            if (foundSignOffs == true)
            {
                SignOffsID = SignOffsID.Remove(SignOffsID.Length - 1, 1);
                SignOffsID = SignOffsID + ")";
                SignOffsDesc = SignOffsDesc.Remove(SignOffsDesc.Length - 1, 1);
            }
            else
            {
                SignOffsID = "";
                SignOffsDesc = "";
            }
            reportSettings.SignOffsDesc = SignOffsDesc;
            rowSignOffs.Properties.Value = reportSettings.SignOffsDesc;
            pgSettingsMain.PostEditor();
        }
        private void Load_SelectedSignOffs()
        {
            bool foundSignOffs = false;
            SignOffsID = "(";
            SignOffsDesc = "";
            int i;
            for (i = 1; i <= rpShafts.Items.Count; i++)
            {
                if (rpSignOffs.Items[i-1].CheckState.ToString() == "Checked")
                {
                    DataRow[] EditedRow = dtSignOffs.Select(" SignDesc = '" + rpSignOffs.Items[i-1].Value.ToString() + "' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        SignOffsID = SignOffsID + dr["OrderBy"].ToString() + ",";
                        SignOffsDesc = SignOffsDesc + rpSignOffs.Items[i-1].Value.ToString() + ",";
                        foundSignOffs = true;
                    }
                }
            }
            if (foundSignOffs == true)
            {
                SignOffsID = SignOffsID.Remove(SignOffsID.Length - 1, 1);
                SignOffsID = SignOffsID + ")";
                SignOffsDesc = SignOffsDesc.Remove(SignOffsDesc.Length - 1, 1);
            }
            else
            {
                SignOffsID = "";
                SignOffsDesc = "";
            }
        }
        private void Load_CleanTypes()
        {
            _clsSurveyReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtCleanTypes = _clsSurveyReportData.get_CleanTypes();

            bool foundClean = false;
            CleanTypeID = "(";
            CleanTypeDesc = "";
            rpCleanTypes.ResetEvents();
            rpCleanTypes.Items.Clear();
            
            foreach (DataRow rr in dtCleanTypes.Rows)
            {
                CleanTypeID = CleanTypeID + rr["CleanTypeID"].ToString() + ",";
                CleanTypeDesc = CleanTypeDesc + rr["CleanTypeDesc"].ToString() + ",";

                rpCleanTypes.Items.Add(rr["CleanTypeDesc"].ToString(), CheckState.Checked, true);
                foundClean = true;
            }
            if (foundClean == true)
            {
                CleanTypeID = CleanTypeID.Remove(CleanTypeID.Length - 1, 1);
                CleanTypeID = CleanTypeID + ")";
                CleanTypeDesc = CleanTypeDesc.Remove(CleanTypeDesc.Length - 1, 1);
            }
            else
            {
                CleanTypeID = "";
                CleanTypeDesc = "";
            }
            reportSettings.CleanTypeDesc = CleanTypeDesc;
            rowCleanTypes.Properties.Value = reportSettings.CleanTypeDesc;
            pgSettingsMain.PostEditor();
        }
        private void Load_SelectedCleanTypes()
        {
            bool foundClean = false;
            CleanTypeID = "(";
            CleanTypeDesc = "";
            int i;
            for (i = 1; i <= rpCleanTypes.Items.Count; i++)
            {
                if (rpCleanTypes.Items[i-1].CheckState.ToString() == "Checked")
                {
                    DataRow[] EditedRow = dtCleanTypes.Select(" CleanTypeDesc = '" + rpCleanTypes.Items[i-1].Value.ToString() + "' ");
                    foreach (DataRow dr in EditedRow)
                    {
                        CleanTypeID = CleanTypeID + dr["CleanTypeID"].ToString() + ",";
                        CleanTypeDesc = CleanTypeDesc + rpCleanTypes.Items[i-1].Value.ToString() + ",";
                        foundClean = true;
                    }
                    
                }
            }
            if (foundClean == true)
            {
                CleanTypeID = CleanTypeID.Remove(CleanTypeID.Length - 1, 1);
                CleanTypeID = CleanTypeID + ")";
                CleanTypeDesc = CleanTypeDesc.Remove(CleanTypeDesc.Length - 1, 1);
            }
            else
            {
                CleanTypeID = "";
                CleanTypeDesc = "";
            }
        }
        private void Load_Hier()
        {
            _clsSurveyReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtHier = _clsSurveyReportData.get_Hier(reportSettings.SectionID, _FromMonth);
            SelectLevel = 1;
            if (dtHier.Rows.Count > 0)
                SelectLevel = Convert.ToInt32(dtHier.Rows[0]["HierarchicalID"].ToString());
        }
        public void Load_LevelOfSummaries()
        {
            _clsSurveyReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtLevSum = _clsSurveyReportData.get_LevSum();

            foreach (DataRow dr in dtLevSum.Rows)
            {
                if (Convert.ToInt32(dr["HierarchicalID"].ToString()) == 1)
                    rowGM.Properties.Caption = dr["Description"].ToString();
                if (Convert.ToInt32(dr["HierarchicalID"].ToString()) == 2)
                    rowMM.Properties.Caption = dr["Description"].ToString();
                if (Convert.ToInt32(dr["HierarchicalID"].ToString()) == 3)
                    rowMNM.Properties.Caption = dr["Description"].ToString();
                if (Convert.ToInt32(dr["HierarchicalID"].ToString()) == 4)
                    rowMO.Properties.Caption = dr["Description"].ToString();
                if (Convert.ToInt32(dr["HierarchicalID"].ToString()) == 5)
                    rowSB.Properties.Caption = dr["Description"].ToString();
                if (Convert.ToInt32(dr["HierarchicalID"].ToString()) == 6)
                    rowMiner.Properties.Caption = dr["Description"].ToString();
            }
        }

        private void rpFromMonth_EditValueChanged(object sender, EventArgs e)
        {
            pgSettingsMain.PostEditor();
            _FromMonth = string.Format("{0:yyyyMM}", reportSettings.FromMonth);
            Load_Sections();
        }

        private void rpToMonth_EditValueChanged(object sender, EventArgs e)
        {
            pgSettingsMain.PostEditor();
            _ToMonth = string.Format("{0:yyyyMM}", reportSettings.ToMonth);
            Load_Sections();
        }

        private void rpSectionID_EditValueChanged(object sender, EventArgs e)
        {
            pgSettingsMain.PostEditor();
            Load_Hier();
            Select_SummaryLevel();
        }
        private void Select_SummaryLevel()
        {
            reportSettings.LvlGM = true;
            reportSettings.LvlMM = true;
            reportSettings.LvlMNM = true;
            reportSettings.LvlMO = true;
            reportSettings.LvlSB = true;
            reportSettings.LvlMiner = true;
            reportSettings.LvlWP = true;

            rowGM.Enabled = true;
            rowMM.Enabled = true;
            rowMNM.Enabled = true;
            rowMO.Enabled = true;
            rowSB.Enabled = true;
            rowMiner.Enabled = true;
            rowWP.Enabled = true;

            if (SelectLevel == 2)
            {
                reportSettings.LvlGM = false;
                rowGM.Enabled = false;
            }
            if (SelectLevel == 3)
            {
                reportSettings.LvlGM = false;
                rowGM.Enabled = false;
                reportSettings.LvlMM = false;
                rowMM.Enabled = false;
            }
            if (SelectLevel == 4)
            {
                reportSettings.LvlGM = false;
                rowGM.Enabled = false;
                reportSettings.LvlMM = false;
                rowMM.Enabled = false;
                reportSettings.LvlMNM = false;
                rowMNM.Enabled = false;
            }
            if (SelectLevel == 5)
            {
                reportSettings.LvlGM = false;
                rowGM.Enabled = false;
                reportSettings.LvlMM = false;
                rowMM.Enabled = false;
                reportSettings.LvlMNM = false;
                rowMNM.Enabled = false;
                reportSettings.LvlMO = false;
                rowMO.Enabled = false;
            }
            if (SelectLevel == 6)
            {
                reportSettings.LvlGM = false;
                rowGM.Enabled = false;
                reportSettings.LvlMM = false;
                rowMM.Enabled = false;
                reportSettings.LvlMNM = false;
                rowMNM.Enabled = false;
                reportSettings.LvlMO = false;
                rowMO.Enabled = false;
                reportSettings.LvlSB = false;
                rowSB.Enabled = false;
            }
            if (SelectLevel == 7)
            {
                reportSettings.LvlGM = false;
                rowGM.Enabled = false;
                reportSettings.LvlMM = false;
                rowMM.Enabled = false;
                reportSettings.LvlMNM = false;
                rowMNM.Enabled = false;
                reportSettings.LvlMO = false;
                rowMO.Enabled = false;
                reportSettings.LvlSB = false;
                rowSB.Enabled = false;
                reportSettings.LvlMiner = false;
                rowMiner.Enabled = false;
            }
        }

        private void rpReefs_EditValueChanged(object sender, EventArgs e)
        {
            Load_SelectedReefs();
            pgSettingsMain.PostEditor();
        }

        private void rpShafts_EditValueChanged(object sender, EventArgs e)
        {
            Load_SelectedShafts();
            pgSettingsMain.PostEditor();
        }

        private void rpSignOffs_EditValueChanged(object sender, EventArgs e)
        {
            Load_SelectedSignOffs();
            pgSettingsMain.PostEditor();
        }

        private void rpCleanTypes_EditValueChanged(object sender, EventArgs e)
        {
            Load_SelectedCleanTypes();
            pgSettingsMain.PostEditor();
        }

        private void rpReportTypeStp_EditValueChanged(object sender, EventArgs e)
        {
            pgSettingsMain.PostEditor();
            if (reportSettings.ReportTypeSweeps == true)
                rowCleanTypes.Enabled = true;
            else
            {
                if (reportSettings.ReportTypeTM == true)
                {
                    rowCleanTypes.Enabled = true;
                    Load_CleanTypes();
                }
                else
                    rowCleanTypes.Enabled = false;
            }
        }

        private void pgSettingsMain_Click(object sender, EventArgs e)
        {

        }
    }
}
