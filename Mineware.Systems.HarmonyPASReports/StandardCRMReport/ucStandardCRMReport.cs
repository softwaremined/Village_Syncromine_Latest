using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using FastReport;
using Mineware.Systems.Global;
using Mineware.Systems.Global.sysMessages;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.StandardCRMReport
{
    public partial class ucStandardCRMReport : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        private clsStandardCRMReportSettings reportSettings = new clsStandardCRMReportSettings();
        DataTable dtSections = new DataTable();
        Report theReport = new Report();
        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }
        Procedures procs = new Procedures();

        public ucStandardCRMReport()
        {
            InitializeComponent();

            ActiveReport.ShowNavigation = true;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.ShowBtnSave = true;
            ActiveReport.ShowCopy = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.isAutoReport = true;
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;

        }

        private void ucCourseBlankSampleReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;

            LoadShaft();
            pgStandardCRMReport.SelectedObject = reportSettings;
        }

        private void LoadShaft()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "select distinct [Description] Name from PAS_Central_Test.dbo.CODE_WPDIVISION";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            DataTable dtShaft = _dbMan.ResultsDataTable;
            rpShaft.DataSource = dtShaft;
            rpShaft.DisplayMember = "Name";
            rpShaft.ValueMember = "Name";
        }

        public override bool prepareReport()
        {
            bool theResult = false;
            this.theReportThread = new Thread(new ParameterizedThreadStart(this.createReport));
            theReportThread.SetApartmentState(ApartmentState.STA);
            theReportThread.Start(reportSettings);

            theResult = true;
            return theResult;

        }

        public override object getAutoProperties()
        {
            return reportSettings;
        }

        private void createReport(Object theReportSettings)
        {

            try
            {
                ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.AsposeExcel;
                clsStandardCRMReportSettings currentReportSettings = theReportSettings as clsStandardCRMReportSettings;
                string designerFile = TGlobalItems.ReportsFolder + "\\StandardCRM.Xlsx";
                MWDataManager.clsDataAccess _courseBlankData = new MWDataManager.clsDataAccess();
                _courseBlankData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _courseBlankData.SqlStatement = "sp_StandardCRMDataReport";
                _courseBlankData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _courseBlankData.ResultsTableName = "StandardCRMData";

                SqlParameter[] _paramCollection =
                    {
                            _courseBlankData.CreateParameter("@Shaft", SqlDbType.VarChar, 50,reportSettings.Shaft.ToString())
                    };

                _courseBlankData.ParamCollection = _paramCollection;
                _courseBlankData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _courseBlankData.ExecuteInstruction();
                DataSet repDataSet = new DataSet();
                repDataSet.Tables.Add(_courseBlankData.ResultsDataTable);
                ActiveReport.SetReport = designerFile;
                ActiveReport.SetReportData(repDataSet);
                ActiveReport.isDone = true;

            }
            catch (Exception _exception)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Exception Error", _exception.Message, Color.Red);
            }

        }


    }
}
