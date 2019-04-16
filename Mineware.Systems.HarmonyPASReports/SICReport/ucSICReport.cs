using System;
using System.Threading;
using System.Data;
using FastReport;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global;
using System.Data.SqlClient;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.SICReport
{
    public partial class ucSICReport : ucReportSettingsControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        clsSICReportSettings reportSettings = new clsSICReportSettings();
        clsSICReportData _clsSICReportData = new clsSICReportData();

        private string theSystemDBTag = "DBHARMONYPAS";
        Report theReport = new Report();
        private Thread theReportThread;

        MWDataManager.clsDataAccess dtSysSettings = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtSICReport = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtSICTotal = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtSICTramming = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtSICKpi = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtSICMilling = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtSICHoisting = new MWDataManager.clsDataAccess();
        MWDataManager.clsDataAccess dtShifts = new MWDataManager.clsDataAccess();

        private DataTable dtSections;
        private DataTable dtMills;
        private DataTable dtHierID;
        private DataTable dtParameters;
        private DataTable dtTemps;
        private DataTable dtSicDetail;

        private bool ErrFound;



        private string TheLevel;
        private bool deleteTempTables;
        private bool saveTemp_MOStartDate;
        private bool saveTemp_SectionStartDate;
        private bool saveTempWorkdaysMO;

        private string SectionName;

        public ucSICReport()
        {
            InitializeComponent();
        }

        private void ucSICReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            reportSettings.UserCurrentInfo = this.UserCurrentInfo.Connection;
            reportSettings.DBTag = theSystemDBTag;

            setReportSetttings();
        }
        private void setReportSetttings()
        {
            _clsSICReportData.DBTag = theSystemDBTag;
            reportSettings.ReportType = "Summary";
            rowReportType.Properties.Value = reportSettings.ReportType;

            reportSettings.CalendarDate = DateTime.Now;
            rowDate.Properties.Value = reportSettings.CalendarDate;

            Load_Sections();
            rowSectionID.Properties.Value = reportSettings.SectionID;

            LoadMills();

            reportSettings.OreflowID = "<<<All>>>";
            rowMill.Properties.Value = reportSettings.OreflowID;
            rowMill.Visible = false;
            pgSettingsMain.SelectedObject = reportSettings;
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
            _clsSICReportData.connectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            string _theDate = reportSettings.CalendarDate.ToString("yyyy-MM-dd");

            String TheRepType = "";
            if (reportSettings.ReportType.ToString() == "Summary")
                TheRepType = "1";
            else
                TheRepType = "2";

            deleteTempTables = _clsSICReportData.delete_TempTable(UserCurrentInfo.UserID);

            Load_HierID();
            string theSection = reportSettings.SectionID.ToString() + ":" + SectionName;
            dtSysSettings.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysSettings.SqlStatement =
                " select Banner Banner,  ServerPath Shaft, \r\n " +
                " '" + _theDate + "' TheDate, \r\n " +
                " '" + theSection + "' TheSection, \r\n " +
                " '" + TheRepType + "' TheRepType, \r\n " +
                " '" + TheLevel + "' TheLevel \r\n " +
                " from Sysset ";
            dtSysSettings.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            dtSysSettings.queryReturnType = MWDataManager.ReturnType.DataTable;
            dtSysSettings.ResultsTableName = "SysSettings";
            clsDataResult errSysset = dtSysSettings.ExecuteInstruction();

            saveTemp_MOStartDate = _clsSICReportData.save_Temp_MOStartDate(_theDate, UserCurrentInfo.UserID);
            saveTemp_SectionStartDate = _clsSICReportData.saveTemp_SectionStartDate(_theDate, UserCurrentInfo.UserID);

            saveTempWorkdaysMO = _clsSICReportData.saveTempWorkdaysMO(_theDate, UserCurrentInfo.UserID);
            
            if (reportSettings.ReportType.ToString() == "Detail")
            {
                dtSICReport.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtSICReport.SqlStatement = "sp_SICReport_Detail";
                dtSICReport.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                dtSICReport.ResultsTableName = "TheQuery";

                SqlParameter[] _paramCollection =
                    {
                        dtSICReport.CreateParameter("@UserID", SqlDbType.VarChar, 20, UserCurrentInfo.UserID),
                        dtSICReport.CreateParameter("@CalendarDate", SqlDbType.VarChar, 10, _theDate),
                        dtSICReport.CreateParameter("@SectionID", SqlDbType.VarChar, 20, reportSettings.SectionID.ToString()),
                        dtSICReport.CreateParameter("@Section", SqlDbType.Int, 0, TheLevel),
                    };

                dtSICReport.ParamCollection = _paramCollection;
                dtSICReport.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult errDetail = dtSICReport.ExecuteInstruction();

                if (dtSICReport.ResultsDataTable.Rows.Count != 0)
                {
                    dtShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        dtShifts.SqlStatement = string.Format("select '{0}' Shift, '{1}' TotalShifts, '{2}' DevCheck, '{3}' StpCheck"
                        , dtSICReport.ResultsDataTable.Rows[0]["Shifts"]
                        , dtSICReport.ResultsDataTable.Rows[0]["TotalShifts"]
                        , dtSICReport.ResultsDataTable.Compute("max(DevCheck)", string.Empty)
                        , dtSICReport.ResultsDataTable.Compute("max(StopeCheck)", string.Empty));
                }
                else
                {
                    dtShifts.SqlStatement = string.Format("select '0' Shift, '0' TotalShifts");
                }
                dtShifts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                dtShifts.queryReturnType = MWDataManager.ReturnType.DataTable;
                dtShifts.ResultsTableName = "Shifts";
                clsDataResult errShifts = dtShifts.ExecuteInstruction();

                if (dtSICReport.ResultsDataTable.Rows.Count == 0)
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "SIC Report", "No data found for your selection", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }

            if (reportSettings.ReportType.ToString() == "Summary")
            {
                
                dtSICTotal.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtSICTotal.SqlStatement = "sp_SICReport_Total";
                dtSICTotal.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                dtSICTotal.ResultsTableName = "TheQuery";

                SqlParameter[] _paramCollection =
                    {
                        dtSICTotal.CreateParameter("@UserID", SqlDbType.VarChar, 20, UserCurrentInfo.UserID),
                        dtSICTotal.CreateParameter("@CalendarDate", SqlDbType.VarChar, 10, _theDate),
                        dtSICTotal.CreateParameter("@SectionID", SqlDbType.VarChar, 20, reportSettings.SectionID.ToString()),
                        dtSICTotal.CreateParameter("@Section", SqlDbType.Int, 0, TheLevel),
                        dtSICTotal.CreateParameter("@MOName", SqlDbType.VarChar, 50, SectionName),
                    };

                dtSICTotal.ParamCollection = _paramCollection;
                dtSICTotal.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult errTotal = dtSICTotal.ExecuteInstruction();

                if (dtSICTotal.ResultsDataTable.Rows.Count == 0)
                {
                    dtSICTotal.SqlStatement = " exec SICReport_Total_Zeroes ";
                    dtSICTotal.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    dtSICTotal.queryReturnType = MWDataManager.ReturnType.DataTable;
                    dtSICTotal.ResultsTableName = "TheQuery";
                    dtSICTotal.ExecuteInstruction();
                }

                dtSICTramming.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtSICTramming.SqlStatement = "sp_SICReport_Tramming";
                dtSICTramming.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                dtSICTramming.ResultsTableName = "TheQuery3";

                SqlParameter[] _paramCollection1 =
                    {
                        dtSICTramming.CreateParameter("@UserID", SqlDbType.VarChar, 20, UserCurrentInfo.UserID),
                        dtSICTramming.CreateParameter("@CalendarDate", SqlDbType.VarChar, 10, _theDate),
                        dtSICTramming.CreateParameter("@SectionID", SqlDbType.VarChar, 20, reportSettings.SectionID.ToString()),
                        dtSICTramming.CreateParameter("@Section", SqlDbType.Int, 0, TheLevel),
                        dtSICTramming.CreateParameter("@MOName", SqlDbType.VarChar, 50, SectionName),
                    };

                dtSICTramming.ParamCollection = _paramCollection1;
                dtSICTramming.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult errTram = dtSICTramming.ExecuteInstruction();
               
                if (dtSICTramming.ResultsDataTable.Rows.Count == 0)
                {
                    dtSICTramming.SqlStatement = " exec sp_SICReport_Tramming_Zeroes ";
                    dtSICTramming.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    dtSICTramming.queryReturnType = MWDataManager.ReturnType.DataTable;
                    dtSICTramming.ResultsTableName = "TheQuery3";
                    clsDataResult dr2 = dtSICTramming.ExecuteInstruction();
                }

                dtSICKpi.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtSICKpi.SqlStatement = "sp_SICReport_KPI";
                dtSICKpi.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                dtSICKpi.ResultsTableName = "TheQuery4";

                SqlParameter[] _paramCollection2 =
                    {
                        dtSICKpi.CreateParameter("@UserID", SqlDbType.VarChar, 20, UserCurrentInfo.UserID),
                        dtSICKpi.CreateParameter("@CalendarDate", SqlDbType.VarChar, 10, _theDate),
                        dtSICKpi.CreateParameter("@SectionID", SqlDbType.VarChar, 20, reportSettings.SectionID.ToString()),
                        dtSICKpi.CreateParameter("@Section", SqlDbType.Int, 0, TheLevel),
                        
                    };

                dtSICKpi.ParamCollection = _paramCollection2;
                dtSICKpi.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult errKPI = dtSICKpi.ExecuteInstruction();

                dtSICMilling.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtSICMilling.SqlStatement = "sp_SICReport_Milling";
                dtSICMilling.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                dtSICMilling.ResultsTableName = "Milling";

                SqlParameter[] _paramCollection3 =
                    {
                        dtSICMilling.CreateParameter("@CalendarDate", SqlDbType.VarChar, 10, _theDate),
                        dtSICMilling.CreateParameter("@Mill", SqlDbType.VarChar, 10, reportSettings.OreflowID.ToString()),

                    };

                dtSICMilling.ParamCollection = _paramCollection3;
                dtSICMilling.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult errMilling = dtSICMilling.ExecuteInstruction();
                
                dtSICHoisting.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtSICHoisting.SqlStatement = "sp_SICReport_Hoisting";
                dtSICHoisting.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                dtSICHoisting.ResultsTableName = "Hoisting";

                SqlParameter[] _paramCollection4 =
                    {
                        dtSICHoisting.CreateParameter("@CalendarDate", SqlDbType.VarChar, 10, _theDate),
                        dtSICHoisting.CreateParameter("@Section", SqlDbType.Int, 0, TheLevel),

                    };

                dtSICHoisting.ParamCollection = _paramCollection4;
                dtSICHoisting.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult errHoist = dtSICHoisting.ExecuteInstruction();

                if (dtSICTotal.ResultsDataTable.Rows.Count == 0)
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "SIC Report", "No data found for your selection", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }           
        }

        private void createReport(Object theReportSettings)
        {
            DataSet repSICDataSet = new DataSet();
            repSICDataSet.Tables.Add(dtSysSettings.ResultsDataTable);
            if (reportSettings.ReportType.ToString() == "Detail")
            {
                repSICDataSet.Tables.Add(dtSICReport.ResultsDataTable);
                repSICDataSet.Tables.Add(dtShifts.ResultsDataTable);
                theReport.RegisterData(repSICDataSet);
                theReport.Load(TGlobalItems.ReportsFolder + "\\SICMO.frx");
            }
            else
            {
                repSICDataSet.Tables.Add(dtSICTotal.ResultsDataTable);
                repSICDataSet.Tables.Add(dtSICTramming.ResultsDataTable);
                repSICDataSet.Tables.Add(dtSICKpi.ResultsDataTable);
                repSICDataSet.Tables.Add(dtSICMilling.ResultsDataTable);
                repSICDataSet.Tables.Add(dtSICHoisting.ResultsDataTable);
                theReport.RegisterData(repSICDataSet);
                theReport.Load(TGlobalItems.ReportsFolder + "\\SIC.frx");
            }

            theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
            theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);

            if (TParameters.DesignReport == true)
            {
                theReport.Design();
            }
            theReport.Prepare();

            theReport.SetParameterValue("ReportProperties", theReportSettings);
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;
        }
        private void CheckForErrors()
        {
            if (reportSettings.SectionID != null)
            {
                if (reportSettings.SectionID == "")
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "SIC Report", "Please select a Section ", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }
            else
            {
                _sysMessagesClass.viewMessage(MessageType.Info, "SIC Report", "Please select a Section ", ButtonTypes.OK, MessageDisplayType.Small);
                ErrFound = true;
            }
        }
        private void Load_Sections()
        {
            _clsSICReportData.connectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSections = _clsSICReportData.get_Sections(reportSettings.CalendarDate.ToString("yyyy-MM-dd"));
            if (dtSections.Rows.Count > 0)
            {
                luSectionID.DataSource = dtSections;
                luSectionID.DisplayMember = "Name";
                luSectionID.ValueMember = "SectionID";
            }
        }
        private void LoadMills()
        {
            luMill.DataSource = null;
            _clsSICReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtMills = _clsSICReportData.get_Mills();

            if (dtMills.Rows.Count > 0)
            {
                luMill.DataSource = dtMills;
                luMill.DisplayMember = "Name";
                luMill.ValueMember = "OreflowID";
            }
        }

        private void luSectionID_EditValueChanged(object sender, EventArgs e)
        {
            pgSettingsMain.PostEditor();
            SectionName = luSectionID.GetDisplayText(reportSettings.SectionID);

            Load_HierID();
            Load_MillVisible();
        }
        private void Load_MillVisible()
        {
            //if (TheLevel == "1")
            //{
            //    if (reportSettings.ReportType.ToString() == "Summary")
            //        rowMill.Visible = false;
            //    else
            //        rowMill.Visible = true;
            //}
            //else
            //    rowMill.Visible = false;
        }
        private void Load_HierID()
        {
            _clsSICReportData.connectionString = TConnections.GetConnectionString(reportSettings.DBTag, UserCurrentInfo.Connection);
            dtHierID = _clsSICReportData.get_HierID(reportSettings.CalendarDate.ToString("yyyy-MM-dd"), reportSettings.SectionID.ToString(), SectionName);
            TheLevel = "";
            if (dtHierID.Rows.Count > 0)
            {
                TheLevel = dtHierID.Rows[0]["HierID"].ToString();
            }
        }

        private void rdgrpReportType_EditValueChanged(object sender, EventArgs e)
        {
            pgSettingsMain.PostEditor();
            Load_MillVisible();
        }

        private void dteDate_EditValueChanged(object sender, EventArgs e)
        {
            pgSettingsMain.PostEditor();
            Load_Sections();
        }

        private void pgSettingsMain_Click(object sender, EventArgs e)
        {

        }
    }
}
