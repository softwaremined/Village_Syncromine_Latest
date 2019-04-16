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
//using Mineware.Systems.Production;
using System.Threading;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.MODaily
{
    public partial class ucMODailyProduction : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;
        DataTable dtSections = new DataTable();
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();


        private MODailyProductionSettingsProperties reportSettings = new MODailyProductionSettingsProperties();


        private DevExpress.XtraVerticalGrid.PropertyGridControl pgMODaily;
        private Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpSectionID;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riReportDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iReportDate;

        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        public ucMODailyProduction()
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pgMODaily = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riReportDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.rpSectionID = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iReportDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgMODaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            this.SuspendLayout();
            // 
            // pgMODaily
            // 
            this.pgMODaily.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgMODaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgMODaily.Location = new System.Drawing.Point(0, 0);
            this.pgMODaily.Name = "pgMODaily";
            this.pgMODaily.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgMODaily.Padding = new System.Windows.Forms.Padding(5);
            this.pgMODaily.RecordWidth = 136;
            this.pgMODaily.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riReportDate,
            this.rpSectionID,
            this.riProdmonth});
            this.pgMODaily.RowHeaderWidth = 64;
            this.pgMODaily.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonth,
            this.iSection,
            this.iReportDate});
            this.pgMODaily.Size = new System.Drawing.Size(378, 311);
            this.pgMODaily.TabIndex = 4;
            this.pgMODaily.Click += new System.EventHandler(this.pgMODaily_Click);
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
            this.riReportDate.EditValueChanged += new System.EventHandler(this.riReportDate_EditValueChanged);
            // 
            // rpSectionID
            // 
            this.rpSectionID.AutoHeight = false;
            this.rpSectionID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpSectionID.Name = "rpSectionID";
            this.rpSectionID.NullText = "";
            // 
            // riProdmonth
            // 
            this.riProdmonth.AutoHeight = false;
            this.riProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riProdmonth.Mask.EditMask = "yyyyMM";
            this.riProdmonth.Mask.IgnoreMaskBlank = false;
            this.riProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riProdmonth.Name = "riProdmonth";
            this.riProdmonth.EditValueChanged += new System.EventHandler(this.riProdmonth_EditValueChanged);
            // 
            // iProdmonth
            // 
            this.iProdmonth.IsChildRowsLoaded = true;
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.Caption = "Production Month";
            this.iProdmonth.Properties.FieldName = "Prodmonth";
            this.iProdmonth.Properties.RowEdit = this.riProdmonth;
            // 
            // iSection
            // 
            this.iSection.IsChildRowsLoaded = true;
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "SectionID";
            this.iSection.Properties.RowEdit = this.rpSectionID;
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
            // ucMODailyProduction
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.Controls.Add(this.pgMODaily);
            this.Name = "ucMODailyProduction";
            this.Load += new System.EventHandler(this.ucMODailyProduction_Load);
            this.Controls.SetChildIndex(this.pgMODaily, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgMODaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        Procedures procs = new Procedures();

        private void LoadSections()
        {

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " Select SectionID, SectionID  +' : ' + Name Name " +
                                  "from Section s where s.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'";

            _dbMan.SqlStatement += " and Hierarchicalid  = 4 ";
            _dbMan.SqlStatement += " order by SectionID asc  ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            dtSections = _dbMan.ResultsDataTable;
            rpSectionID.DataSource = dtSections;
            rpSectionID.DisplayMember = "Name";
            rpSectionID.ValueMember = "SectionID";



            // this.gbSectionID.Text = "Section ID";
        }

        private void ucMODailyProduction_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;
            //reportSettings.ReportDate = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Rundate;
            reportSettings.Prodmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            LoadSections();
            reportSettings.SectionID = dtSections.Rows[0][0].ToString();
            pgMODaily.SelectedObject = reportSettings;
            reportSettings.ReportDate = DateTime.Now;
            iReportDate.Properties.Value = reportSettings.ReportDate;
        }

        public override bool prepareReport()
        {
            bool theResult = false;

            if (CheckForErrors() == false)
            {
                theReportThread = new Thread(new ParameterizedThreadStart(createReport));
                theReportThread.SetApartmentState(ApartmentState.STA);
                theReportThread.Start(reportSettings);
                theResult = true;
            }
            return theResult;
        }

        private Boolean CheckForErrors()
        {
            Boolean haserror = false;
            if (reportSettings.SectionID == null)
            {
                _sysMessagesClass.viewMessage(MessageType.Info, "MO Daily Report", "Please select a Section", ButtonTypes.OK, MessageDisplayType.Small);
                haserror = true;
            }

            return haserror;
        }

        private void createReport(Object theReportSettings)
        {
            Report theReport = new Report();
            MODailyProductionSettingsProperties currentReportSettings = theReportSettings as MODailyProductionSettingsProperties;
            ucMODailyProduction _ucEfficiency = new ucMODailyProduction { theConnection = ActiveReport.UserCurrentInfo.Connection };


            //////Get SectionID

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " Select SectionID, SectionID  +' : ' + Name Name " +
                                  "from Section s where s.Prodmonth = '" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "'";

            _dbMan.SqlStatement += " and SectionID  = '" + reportSettings.SectionID + "' ";
            _dbMan.SqlStatement += "   ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            string MO = _dbMan.ResultsDataTable.Rows[0][1].ToString();

            //DataSet repUserActivitySet = new DataSet();
            MWDataManager.clsDataAccess _StopeReportData = new MWDataManager.clsDataAccess();
            MWDataManager.clsDataAccess _DevReportData = new MWDataManager.clsDataAccess();
            try

            {
                _StopeReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _StopeReportData.SqlStatement = "SP_MODailyReport";
                _StopeReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _StopeReportData.ResultsTableName = "MODailyStopeData";

                SqlParameter[] _paramCollection =
                    {
                            _StopeReportData.CreateParameter("@Prodmonth", SqlDbType.VarChar, 6, TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)),
                            _StopeReportData.CreateParameter("@Section", SqlDbType.Text, 100,reportSettings.SectionID),
                            _StopeReportData.CreateParameter("@TheDate", SqlDbType.DateTime, 50,reportSettings.ReportDate),
                    };

                _StopeReportData.ParamCollection = _paramCollection;
                _StopeReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _StopeReportData.ExecuteInstruction();

                _DevReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _DevReportData.SqlStatement = "SP_MODaily_Dev_Report";
                _DevReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                _DevReportData.ResultsTableName = "MODailyDevData";

                SqlParameter[] _DevparamCollection =
                    {
                            _DevReportData.CreateParameter("@Prodmonth", SqlDbType.VarChar, 6, TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)),
                            _DevReportData.CreateParameter("@Section", SqlDbType.Text, 100, reportSettings.SectionID),
                            _DevReportData.CreateParameter("@TheDate", SqlDbType.DateTime, 50,reportSettings.ReportDate),
                    };

                _DevReportData.ParamCollection = _DevparamCollection;
                _DevReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _DevReportData.ExecuteInstruction();
            }
            catch (Exception _exception)
            {
                throw new ApplicationException("Report Section:MODailyStopeData:" + _exception.Message, _exception);
            }


            DataSet repDataSet = new DataSet();
            repDataSet.Tables.Add(_StopeReportData.ResultsDataTable);
            repDataSet.Tables.Add(_DevReportData.ResultsDataTable);
            theReport.RegisterData(repDataSet);

            theReport.Load(TGlobalItems.ReportsFolder + "\\MODaily.frx");

            theReport.SetParameterValue("Banner", SysSettings.Banner);
            theReport.SetParameterValue("Prodmonth", TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            theReport.SetParameterValue("Section", MO);
            theReport.SetParameterValue("TheDate", reportSettings.ReportDate);
            theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
            theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);
            //theReport.Design();
            if (TParameters.DesignReport)
            {
                theReport.Design();
            }

            theReport.Prepare();
            ActiveReport.SetReport = theReport;
            ActiveReport.isDone = true;

        }

        private void pgMODaily_Click(object sender, EventArgs e)
        {

        }

        private void riReportDate_EditValueChanged(object sender, EventArgs e)
        {
            LoadSections();
        }

        private void riProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadSections();
        }
    }
}
