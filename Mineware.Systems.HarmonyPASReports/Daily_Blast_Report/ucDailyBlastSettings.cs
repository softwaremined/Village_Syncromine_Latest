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


namespace Mineware.Systems.HarmonyPASReports.Daily_Blast_Report
{
    public partial class ucDailyBlastSettings : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;

        private DailyBlastSettingsProperties reportSettings = new DailyBlastSettingsProperties();

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgDailyBlast;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riReportDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iReportDate;

        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        public ucDailyBlastSettings()
        {
            InitializeComponent();
            ActiveReport.reportType = Global.ReportsControls.SharedItems.reportTypes.FastReports;
            ActiveReport.isDone = false;
            ActiveReport.ShowZoom = true;
            ActiveReport.DirectPrint = false;
            ActiveReport.ShowNavigation = true;
        }

        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pgDailyBlast = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riReportDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.iReportDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgDailyBlast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // pgDailyBlast
            // 
            this.pgDailyBlast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgDailyBlast.Location = new System.Drawing.Point(0, 0);
            this.pgDailyBlast.Name = "pgDailyBlast";
            this.pgDailyBlast.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgDailyBlast.RecordWidth = 136;
            this.pgDailyBlast.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riReportDate});
            this.pgDailyBlast.RowHeaderWidth = 64;
            this.pgDailyBlast.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iReportDate});
            this.pgDailyBlast.Size = new System.Drawing.Size(383, 375);
            this.pgDailyBlast.TabIndex = 4;
            // 
            // riReportDate
            // 
            this.riReportDate.AutoHeight = false;
            this.riReportDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riReportDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riReportDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.riReportDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.riReportDate.EditFormat.FormatString = "yyyy-MM-dd";
            this.riReportDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.riReportDate.Name = "riReportDate";
            // 
            // iReportDate
            // 
            this.iReportDate.IsChildRowsLoaded = true;
            this.iReportDate.Name = "iReportDate";
            this.iReportDate.Properties.Caption = "ReportDate";
            this.iReportDate.Properties.FieldName = "ReportDate";
            this.iReportDate.Properties.Format.FormatString = "yyyy-MM-dd";
            this.iReportDate.Properties.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.iReportDate.Properties.RowEdit = this.riReportDate;
            // 
            // ucDailyBlastSettings
            // 
            this.Controls.Add(this.pgDailyBlast);
            this.Name = "ucDailyBlastSettings";
            this.Size = new System.Drawing.Size(383, 375);
            this.Load += new System.EventHandler(this.ucDailyBlastSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pgDailyBlast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).EndInit();
            this.ResumeLayout(false);
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
            Report theReport = new Report();
            DailyBlastSettingsProperties currentReportSettings = theReportSettings as DailyBlastSettingsProperties;
            ucDailyBlastSettings _ucDailyBlast = new ucDailyBlastSettings { theConnection = ActiveReport.UserCurrentInfo.Connection };
            
            // Page Shaft Data
            MWDataManager.clsDataAccess _DailyBlastReportShaftData = new MWDataManager.clsDataAccess 
            { 
                ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), 
                SqlStatement = "SP_DailyBlast_Shafts", 
                queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, 
                ResultsTableName = "DailyBlast_Shafts" 
            };
            try
            {
                //SqlParameter[] _paramCollection = 
                //{
                //    _DailyBlastReportShaftData.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                      //_DailyBlastReportShaftData.CreateParameter("@TheDate", SqlDbType.Date , 0, reportSettings.ReportDate),
                //};

                //_DailyBlastReportShaftData.ParamCollection = _paramCollection;

                _DailyBlastReportShaftData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportShaftData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            // Page 2 Data
            MWDataManager.clsDataAccess _DailyBlastReportPage2Data = new MWDataManager.clsDataAccess 
            {
                ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection),
                SqlStatement = "SP_DailyBlastStope_BU", 
                queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, 
                ResultsTableName = "DailyBlastStopeData_BU" };
            try
            {
                SqlParameter[] _paramCollection1 = 
                    {
                     _DailyBlastReportPage2Data.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportPage2Data.ParamCollection = _paramCollection1;

                _DailyBlastReportPage2Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportPage2Data.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            // Page 3/4 Data 
            MWDataManager.clsDataAccess _DailyBlastReportPage34Data = new MWDataManager.clsDataAccess { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), SqlStatement = "SP_DailyBlastStope_MO", queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, ResultsTableName = "DailyBlastStopeData_MO" };
            try
            {
                SqlParameter[] _paramCollection2 = 
                    {
                     _DailyBlastReportPage34Data.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportPage34Data.ParamCollection = _paramCollection2;

                _DailyBlastReportPage34Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportPage34Data.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            //SP_DailyBlastStope_Gold_BuildData Build Data for SP's
            MWDataManager.clsDataAccess _DailyBlastReportBuildData = new MWDataManager.clsDataAccess { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), SqlStatement = "SP_DailyBlastStope_Gold_BuildData", queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, ResultsTableName = "DailyBlast_KG_sp" };
            try
            {
                SqlParameter[] _paramCollection3 = 
                    {
                     _DailyBlastReportBuildData.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportBuildData.ParamCollection = _paramCollection3;

                _DailyBlastReportBuildData.queryReturnType = MWDataManager.ReturnType.longNumber;
                _DailyBlastReportBuildData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            // Page 5 Data 
            MWDataManager.clsDataAccess _DailyBlastReportPage5Data = new MWDataManager.clsDataAccess { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), SqlStatement = "SP_DailyBlast_KG", queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, ResultsTableName = "DailyBlast_KG" };
            try
            {
                SqlParameter[] _paramCollection11 = 
                    {
                     _DailyBlastReportPage5Data.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportPage5Data.ParamCollection = _paramCollection11;

                _DailyBlastReportPage5Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportPage5Data.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            // Page 67 Data 
            MWDataManager.clsDataAccess _DailyBlastReportPage67Data = new MWDataManager.clsDataAccess { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), SqlStatement = "SP_DailyBlast_KG_Shaft", queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, ResultsTableName = "DailyBlast_KG_Shaft" };
            try
            {
                SqlParameter[] _paramCollection12 = 
                    {
                     _DailyBlastReportPage67Data.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportPage67Data.ParamCollection = _paramCollection12;

                _DailyBlastReportPage67Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportPage67Data.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            // Page 8 Data 
            MWDataManager.clsDataAccess _DailyBlastReportPage8Data = new MWDataManager.clsDataAccess { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), SqlStatement = "Sp_DailyBlast_Crews", queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, ResultsTableName = "DailyBlast_Crews" };
            try
            {
                SqlParameter[] _paramCollection4 = 
                    {
                     _DailyBlastReportPage8Data.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportPage8Data.ParamCollection = _paramCollection4;

                _DailyBlastReportPage8Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportPage8Data.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            // Page 9 Data 
            MWDataManager.clsDataAccess _DailyBlastReportPage9Data = new MWDataManager.clsDataAccess { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), SqlStatement = "Sp_DailyBlast_Crew_List", queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, ResultsTableName = "DailyBlast_Crew_List" };
            try
            {
                SqlParameter[] _paramCollection99 = 
                    {
                     _DailyBlastReportPage9Data.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportPage9Data.ParamCollection = _paramCollection99;

                _DailyBlastReportPage9Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportPage9Data.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            // Page 10 Data 
            MWDataManager.clsDataAccess _DailyBlastReportPage10Data = new MWDataManager.clsDataAccess { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), SqlStatement = "SP_DailyBlastDev_BU", queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, ResultsTableName = "DailyBlastDevData_BU" };
            try
            {
                SqlParameter[] _paramCollection5 = 
                    {
                     _DailyBlastReportPage10Data.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportPage10Data.ParamCollection = _paramCollection5;

                _DailyBlastReportPage10Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportPage10Data.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            // Page 11-15 Data 
            MWDataManager.clsDataAccess _DailyBlastReportPage11Data = new MWDataManager.clsDataAccess { ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection), SqlStatement = "SP_DailyBlastStope_Dev_Riggs", queryExecutionType = MWDataManager.ExecutionType.StoreProcedure, ResultsTableName = "DailyBlastDevData_Riggs" };
            try
            {
                SqlParameter[] _paramCollection6 = 
                    {
                     _DailyBlastReportPage11Data.CreateParameter("@TheDate", SqlDbType.VarChar, 50, String.Format("{0:yyyy-MM-dd}", reportSettings.ReportDate.ToShortDateString())),
                    };

                _DailyBlastReportPage11Data.ParamCollection = _paramCollection6;

                _DailyBlastReportPage11Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DailyBlastReportPage11Data.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            DataSet repDailyBlastDataSet = new DataSet();
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportShaftData.ResultsDataTable);
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportPage2Data.ResultsDataTable);
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportPage34Data.ResultsDataTable);
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportPage5Data.ResultsDataTable);
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportPage67Data.ResultsDataTable);
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportPage8Data.ResultsDataTable);
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportPage9Data.ResultsDataTable);
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportPage10Data.ResultsDataTable);
            repDailyBlastDataSet.Tables.Add(_DailyBlastReportPage11Data.ResultsDataTable);
            theReport.RegisterData(repDailyBlastDataSet);

            theReport.Load(TGlobalItems.ReportsFolder + "\\DailyBlastReport.frx");

            theReport.SetParameterValue("TheDate", reportSettings.ReportDate.ToShortDateString());

            if (TParameters.DesignReport)
            {
                theReport.Design();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
            }
            else
            {
                theReport.Prepare();
                ActiveReport.SetReport = theReport;
                ActiveReport.isDone = true;
            }
        }

        private void ucDailyBlastSettings_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;

            MWDataManager.clsDataAccess _TheDateTime = new MWDataManager.clsDataAccess();
            _TheDateTime.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            try
            {
                _TheDateTime.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _TheDateTime.SqlStatement = "select max(CALENDARDATE) TheDate from seccal a inner join caltype b on " +
                                            "a.CALENDARCODE = b.CALENDARCODE and " +
                                            "a.BEGINDATE <= b.CALENDARDATE and " +
                                            "a.ENDDATE >= b.CALENDARDATE and " +
                                            "b.CALENDARDATE < Getdate() and " +
                                            //"seccaltype = 0 and " +
                                            "WORKINGDAY = 'Y'";
                _TheDateTime.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _TheDateTime.queryReturnType = MWDataManager.ReturnType.DataTable;
                _TheDateTime.ExecuteInstruction();

                DataTable SubB = _TheDateTime.ResultsDataTable;

                reportSettings.ReportDate = Convert.ToDateTime(SubB.Rows[0]["TheDate"].ToString());
            }
            catch (Exception _exception)
            {
                throw new ApplicationException(_exception.Message, _exception);
            }

            pgDailyBlast.SelectedObject = reportSettings;
        }
    }
}
