using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.Global.sysMessages;
using FastReport;
using Mineware.Systems.Global;
using System.Threading;
using Mineware.Systems.ProductionGlobal;
using System.Data.SqlClient;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Reports.GradeReport
{
    public partial class ucGradeReport : ucReportSettingsControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;
        private DataTable dtMOName;

        private bool ErrFound;
        string _DefaultShift = "Y";

        private clsGradeReportSettingsProperties reportSettings = new clsGradeReportSettingsProperties();
        Report theReport = new Report();

        MWDataManager.clsDataAccess _DailyGradeReportData = new MWDataManager.clsDataAccess();
        private DevExpress.XtraVerticalGrid.PropertyGridControl pgGradeReport;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpMOName;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riReportDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iReportDate;

        private string _theConnection;
        public string theConnection { get { return _theConnection; } set { _theConnection = value; } }

        public ucGradeReport()
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
            this.pgGradeReport = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riReportDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.rpMOName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riPaymilit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.riShiftsNo = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.riCutOffGrade = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.riShifts = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riShiftsDefault = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rpActivity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riGold = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riCmgt = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riTopPanels = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rpTopPanels = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iReportDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iPaylimit = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iCutOffGrade = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.catForeCast = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.iShiftsDefault = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iShifts = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iShiftsNo = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.catTopPanels = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.iTopPanels = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgGradeReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMOName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPaymilit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riShiftsNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCutOffGrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riShiftsDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCmgt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTopPanels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpTopPanels)).BeginInit();
            this.SuspendLayout();
            // 
            // pgGradeReport
            // 
            this.pgGradeReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgGradeReport.Location = new System.Drawing.Point(0, 0);
            this.pgGradeReport.Name = "pgGradeReport";
            this.pgGradeReport.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgGradeReport.RecordWidth = 105;
            this.pgGradeReport.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riReportDate,
            this.rpMOName,
            this.riPaymilit,
            this.riShiftsNo,
            this.riCutOffGrade,
            this.riShifts,
            this.riShiftsDefault,
            this.rpActivity,
            this.riGold,
            this.riCmgt,
            this.riTopPanels,
            this.rpTopPanels});
            this.pgGradeReport.RowHeaderWidth = 95;
            this.pgGradeReport.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iActivity,
            this.iReportDate,
            this.iPaylimit,
            this.iCutOffGrade,
            this.catForeCast,
            this.iShiftsDefault,
            this.iShifts,
            this.iShiftsNo,
            this.catTopPanels,
            this.iTopPanels});
            this.pgGradeReport.Size = new System.Drawing.Size(378, 311);
            this.pgGradeReport.TabIndex = 4;
            this.pgGradeReport.Click += new System.EventHandler(this.pgGradeReport_Click);
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
            // rpMOName
            // 
            this.rpMOName.AutoHeight = false;
            this.rpMOName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpMOName.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Shaft", "Shaft")});
            this.rpMOName.Name = "rpMOName";
            this.rpMOName.NullText = "[Select a Shaft]";
            this.rpMOName.EditValueChanged += new System.EventHandler(this.rpMOName_EditValueChanged);
            // 
            // riPaymilit
            // 
            this.riPaymilit.AutoHeight = false;
            this.riPaymilit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riPaymilit.Name = "riPaymilit";
            // 
            // riShiftsNo
            // 
            this.riShiftsNo.AutoHeight = false;
            this.riShiftsNo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riShiftsNo.Name = "riShiftsNo";
            // 
            // riCutOffGrade
            // 
            this.riCutOffGrade.AutoHeight = false;
            this.riCutOffGrade.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riCutOffGrade.Name = "riCutOffGrade";
            // 
            // riShifts
            // 
            this.riShifts.AutoHeight = false;
            this.riShifts.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.riShifts.Name = "riShifts";
            this.riShifts.CheckedChanged += new System.EventHandler(this.riShiftsDefault_CheckedChanged);
            // 
            // riShiftsDefault
            // 
            this.riShiftsDefault.AutoHeight = false;
            this.riShiftsDefault.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.riShiftsDefault.Name = "riShiftsDefault";
            this.riShiftsDefault.CheckedChanged += new System.EventHandler(this.riShiftsDefault_CheckedChanged);
            // 
            // rpActivity
            // 
            this.rpActivity.AutoHeight = false;
            this.rpActivity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpActivity.Name = "rpActivity";
            this.rpActivity.NullText = "[Select an Activity]";
            this.rpActivity.EditValueChanged += new System.EventHandler(this.rpActivity_EditValueChanged);
            // 
            // riGold
            // 
            this.riGold.AutoHeight = false;
            this.riGold.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.riGold.Name = "riGold";
            // 
            // riCmgt
            // 
            this.riCmgt.AutoHeight = false;
            this.riCmgt.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.riCmgt.Name = "riCmgt";
            // 
            // riTopPanels
            // 
            this.riTopPanels.AutoHeight = false;
            this.riTopPanels.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.riTopPanels.Name = "riTopPanels";
            // 
            // rpTopPanels
            // 
            this.rpTopPanels.Columns = 1;
            this.rpTopPanels.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Gold", "Top 10 Gold"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Cmgt", "Top 10 Cmg/t"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Top Panels", "Top Panels")});
            this.rpTopPanels.Name = "rpTopPanels";
            // 
            // iActivity
            // 
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Activity";
            this.iActivity.Properties.RowEdit = this.rpActivity;
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
            // iPaylimit
            // 
            this.iPaylimit.IsChildRowsLoaded = true;
            this.iPaylimit.Name = "iPaylimit";
            this.iPaylimit.Properties.Caption = "Paylimit";
            this.iPaylimit.Properties.FieldName = "Paylimit";
            this.iPaylimit.Properties.RowEdit = this.riPaymilit;
            // 
            // iCutOffGrade
            // 
            this.iCutOffGrade.Name = "iCutOffGrade";
            this.iCutOffGrade.Properties.Caption = "Cutt Off Grade";
            this.iCutOffGrade.Properties.FieldName = "CutOffGrade";
            this.iCutOffGrade.Properties.RowEdit = this.riCutOffGrade;
            // 
            // catForeCast
            // 
            this.catForeCast.Name = "catForeCast";
            this.catForeCast.Properties.Caption = "Linear Fore Cast";
            // 
            // iShiftsDefault
            // 
            this.iShiftsDefault.Name = "iShiftsDefault";
            this.iShiftsDefault.Properties.Caption = "Use Total Shifts";
            this.iShiftsDefault.Properties.FieldName = "useShiftsDefault";
            this.iShiftsDefault.Properties.RowEdit = this.riShiftsDefault;
            // 
            // iShifts
            // 
            this.iShifts.Name = "iShifts";
            this.iShifts.Properties.Caption = "Use Total Shifts Minus";
            this.iShifts.Properties.FieldName = "useShifts";
            this.iShifts.Properties.RowEdit = this.riShifts;
            // 
            // iShiftsNo
            // 
            this.iShiftsNo.Name = "iShiftsNo";
            this.iShiftsNo.Properties.Caption = "Total Shifts Minus Shift/s";
            this.iShiftsNo.Properties.FieldName = "ShiftsNo";
            this.iShiftsNo.Properties.RowEdit = this.riShiftsNo;
            // 
            // catTopPanels
            // 
            this.catTopPanels.Name = "catTopPanels";
            this.catTopPanels.Properties.Caption = "Top Panels";
            // 
            // iTopPanels
            // 
            this.iTopPanels.Height = 57;
            this.iTopPanels.Name = "iTopPanels";
            this.iTopPanels.Properties.Caption = "Top Panels";
            this.iTopPanels.Properties.FieldName = "TopPanels";
            this.iTopPanels.Properties.RowEdit = this.rpTopPanels;
            // 
            // ucGradeReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.Controls.Add(this.pgGradeReport);
            this.Name = "ucGradeReport";
            this.Load += new System.EventHandler(this.ucGradeReport_Load);
            this.Controls.SetChildIndex(this.pgGradeReport, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgGradeReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMOName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPaymilit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riShiftsNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCutOffGrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riShiftsDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCmgt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTopPanels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpTopPanels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void LoadMOName(string _activity)
        {
            MWDataManager.clsDataAccess _MONameData = new MWDataManager.clsDataAccess();

            _MONameData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _MONameData.SqlStatement =
                            " Select Distinct PeerName_3 Shaft \r\n " +
                            " from PLanmonth a \r\n " +
                            " inner join Section_Complete b on \n" +
                            "   a.Prodmonth = b.Prodmonth and \n" +
                            "   a.sectionid = b.Sectionid \n" +
                            " where a.Prodmonth = (Select currentproductionmonth from Sysset) and \n" +
                            "       Activity = '" + _activity + "' and PLancode = 'MP'";

            _MONameData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _MONameData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _MONameData.ExecuteInstruction();
            dtMOName = _MONameData.ResultsDataTable;

            if (dtMOName.Rows.Count != 0)
            {
                rpMOName.DataSource = dtMOName;
                rpMOName.DisplayMember = "Shaft";
                rpMOName.ValueMember = "Shaft";
            }
            //reportSettings.Shaft = dtMOName.Rows[0]["Shaft"].ToString();

        }

        private void ucGradeReport_Load(object sender, EventArgs e)
        {
            reportSettings.ReportDate = DateTime.Now;
            reportSettings.Paylimit = 1330;
            reportSettings.ShiftsNo = 2;
            reportSettings.CutOffGrade = 860;
            reportSettings.useShiftsDefault = true;
            reportSettings.useShifts = false;
            reportSettings.TopPanels = "Gold";

            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            DataTable dtActivityData = BMEBL.GetActivity();

            rpActivity.DataSource = dtActivityData;
            rpActivity.DisplayMember = "Description";
            rpActivity.ValueMember = "Activity";

            setScreenSelection();

            pgGradeReport.SelectedObject = reportSettings;
        }

        private void setScreenSelection()
        {
            iShiftsNo.Visible = reportSettings.useShifts;
            iActivity.Properties.Value = reportSettings.Activity;

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
            return theResult;
        }

        private void LoadReportData()
        {

            if (reportSettings.useShiftsDefault == false)
                _DefaultShift = "N";

            string _topPanels = "";

            if (reportSettings.TopPanels == "Gold")
                _topPanels = "G";

            if (reportSettings.TopPanels == "Cmgt")
                _topPanels = "C";

            if (reportSettings.TopPanels == "Top Panels")
                _topPanels = "T";

            ucGradeReport _ucEfficiency = new ucGradeReport { theConnection = ActiveReport.UserCurrentInfo.Connection };

            try
            {
                if (reportSettings.Activity.ToString() == "0")
                {
                    _DailyGradeReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _DailyGradeReportData.SqlStatement = "SP_DailyGradeReport";
                    _DailyGradeReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _DailyGradeReportData.ResultsTableName = "DailyGradeReportData";
                    SqlParameter[] _paramCollection1 =
                    {
                        _DailyGradeReportData.CreateParameter("@TheDate", SqlDbType.DateTime, 50, reportSettings.ReportDate.ToString("yyyy-MM-dd")),
                        _DailyGradeReportData.CreateParameter("@ShiftsNo", SqlDbType.Int, 10, reportSettings.ShiftsNo),
                        _DailyGradeReportData.CreateParameter("@DefaultShift", SqlDbType.VarChar, 1, _DefaultShift),
                        _DailyGradeReportData.CreateParameter("@PayLimit", SqlDbType.Int, 10, reportSettings.Paylimit),
                        _DailyGradeReportData.CreateParameter("@CutOffGrade", SqlDbType.Int, 10, reportSettings.CutOffGrade),
                        _DailyGradeReportData.CreateParameter("@TopPanels", SqlDbType.VarChar, 1, _topPanels),
                    };
                    _DailyGradeReportData.ParamCollection = _paramCollection1;
                    _DailyGradeReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _DailyGradeReportData.ExecuteInstruction();
                }
                else
                {
                    _DailyGradeReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _DailyGradeReportData.SqlStatement = "sp_DailyGradeReport_Development";
                    _DailyGradeReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _DailyGradeReportData.ResultsTableName = "DailyGradeReportData";
                    SqlParameter[] _paramCollection1 =
                    {
                        _DailyGradeReportData.CreateParameter("@TheDate", SqlDbType.DateTime, 50, reportSettings.ReportDate.ToString("yyyy-MM-dd")),
                        _DailyGradeReportData.CreateParameter("@ShiftsNo", SqlDbType.Int, 10, reportSettings.ShiftsNo),
                        _DailyGradeReportData.CreateParameter("@DefaultShift", SqlDbType.VarChar, 1, _DefaultShift),
                        _DailyGradeReportData.CreateParameter("@PayLimit", SqlDbType.Int, 10, reportSettings.Paylimit),
                        _DailyGradeReportData.CreateParameter("@CutOffGrade", SqlDbType.Int, 10, reportSettings.CutOffGrade),
                    };
                    _DailyGradeReportData.ParamCollection = _paramCollection1;
                    _DailyGradeReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _DailyGradeReportData.ExecuteInstruction();
                }
            }
            catch (Exception _exception)
            {
                throw new ApplicationException("Report Section:Grade Report:" + _exception.Message, _exception);
            }
            if (_DailyGradeReportData.ResultsDataTable.Rows.Count == 0)
            {
                _sysMessagesClass.viewMessage(MessageType.Info, "Grade Report", "No data found for your selection", ButtonTypes.OK, MessageDisplayType.Small);
                ErrFound = true;
            }
        }

        private void createReport(Object theReportSettings)
        {
            clsGradeReportSettingsProperties currentReportSettings = theReportSettings as clsGradeReportSettingsProperties;
            DataSet repDataSet = new DataSet();

            repDataSet.Tables.Add(_DailyGradeReportData.ResultsDataTable);
            theReport.Clear();

            theReport.RegisterData(repDataSet);
            if (reportSettings.Activity.ToString() == "0")
                theReport.Load(TGlobalItems.ReportsFolder + "\\GradeReport.frx");
            else
                theReport.Load(TGlobalItems.ReportsFolder + "\\GradeReportDevelopment.frx");

            theReport.SetParameterValue("TheDate", reportSettings.ReportDate);
            theReport.SetParameterValue("PayLimit", reportSettings.Paylimit);
            theReport.SetParameterValue("ShiftsNo", reportSettings.ShiftsNo);
            theReport.SetParameterValue("DefaultShift", _DefaultShift);
            theReport.SetParameterValue("CutOffGrade", reportSettings.CutOffGrade);
            theReport.SetParameterValue("Banner", SysSettings.Banner);
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

        private void CheckForErrors()
        {
            if (reportSettings.Activity != null)
            {
                if (reportSettings.Activity == "")
                {
                    _sysMessagesClass.viewMessage(MessageType.Info, "Grade Report", "Please select an Activity ", ButtonTypes.OK, MessageDisplayType.Small);
                    ErrFound = true;
                }
            }
            else
            {
                _sysMessagesClass.viewMessage(MessageType.Info, "Grade Report", "Please select an Activity ", ButtonTypes.OK, MessageDisplayType.Small);
                ErrFound = true;
            }
        }

        private void riShiftsDefault_CheckedChanged(object sender, EventArgs e)
        {
            setScreenSelection();
        }

        private void rpActivity_EditValueChanged(object sender, EventArgs e)
        {
            pgGradeReport.PostEditor();
            if (reportSettings.Activity != null)
                if (reportSettings.Activity != "")
                {
                    //LoadMOName(reportSettings.Activity.ToString());
                    if (reportSettings.Activity.ToString() == "1")
                    {
                        iPaylimit.Enabled = false;
                        iCutOffGrade.Enabled = false;
                    }
                    else
                    {
                        iPaylimit.Enabled = true;
                        iCutOffGrade.Enabled = true;
                    }
                }
        }

        private void rpMOName_EditValueChanged(object sender, EventArgs e)
        {
            pgGradeReport.PostEditor();
        }

        private void pgGradeReport_Click(object sender, EventArgs e)
        {

        }
    }
}
