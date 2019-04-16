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
using Mineware.Systems.HarmonyPASGlobal;
//using DevExpress.XtraGrid.Columns;
using Mineware.Systems.HarmonyPASReports.MeasuringListReport;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.HarmonyPASReports.PlathondWallChartReport
{
    public partial class PlathondWallChartReportUserControl : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;
        private PlathondWallChartReportSettingsPropertiesClass reportSettings = new PlathondWallChartReportSettingsPropertiesClass();
        Report theReport = new Report();
        Procedures procs = new Procedures();
        private string _theConnection;

        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        public PlathondWallChartReportUserControl()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void PlathondWallChartReportUserControl_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            reportSettings.Prodmonth = THarmonyPASGlobal.ProdMonthAsDate(THarmonyPASGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());

            iProdMonth.Properties.Value = reportSettings.Prodmonth;
            LoadSections();
            reportSettings.Activity = "Stoping";
            pgMeasuringList.SelectedObject = reportSettings;
        }

        public void LoadSections()
        {
            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            DataTable dtSectionData = new DataTable();

            if (BMEBL.GetPlanSectionsAndNameADO(THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth)) == true)
            {
                dtSectionData = BMEBL.ResultsDataTable;
                riSection.DataSource = BMEBL.ResultsDataTable;
                riSection.DisplayMember = "NAME";
                riSection.ValueMember = "NAME";
            }
            reportSettings.NAME = dtSectionData.Rows[0]["NAME"].ToString();
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
            string _act = "";

            if (reportSettings.Activity == "Stoping")
            {
                _act = "0";
            }

            else if (reportSettings.Activity == "Development")
            {
                _act = "1";
            }
            MWDataManager.clsDataAccess _plathondWallRoomReportData = new MWDataManager.clsDataAccess();

                _plathondWallRoomReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _plathondWallRoomReportData.SqlStatement = "sp_PlathondWallRoom";
                _plathondWallRoomReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _plathondWallRoomReportData.ResultsTableName = "PlathondWallRoomData";

                SqlParameter[] _DevparamCollection =
                    {
                            _plathondWallRoomReportData.CreateParameter("@Prodmonth", SqlDbType.VarChar, 6, THarmonyPASGlobal.ProdMonthAsString ( reportSettings.Prodmonth)),
                            _plathondWallRoomReportData.CreateParameter("@Section", SqlDbType.VarChar, 50, reportSettings.NAME),
                            _plathondWallRoomReportData.CreateParameter("@Activity", SqlDbType.VarChar, 1, _act),
                    };

                _plathondWallRoomReportData.ParamCollection = _DevparamCollection;
                _plathondWallRoomReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
                clsDataResult result = _plathondWallRoomReportData.ExecuteInstruction();

            if (!result.success)
            {
                throw new ApplicationException("Report Section:MODailyStopeData:" + result.Message);
            }

            DataSet repDataSet = new DataSet();


            repDataSet.Tables.Add(_plathondWallRoomReportData.ResultsDataTable);
            theReport.RegisterData(repDataSet);

            theReport.Load(TGlobalItems.ReportsFolder + "\\PlathondWallRoom.frx");

            theReport.SetParameterValue("Banner", THarmonyPASGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Mine);
            theReport.SetParameterValue("Prodmonth", THarmonyPASGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            theReport.SetParameterValue("SectionName", reportSettings.NAME);
            theReport.SetParameterValue("TheDate", reportSettings.Activity);
            theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
            theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);

            if (TParameters.DesignReport)
            {
                theReport.Design();
            }

            theReport.Prepare();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;

        }
    }
}
