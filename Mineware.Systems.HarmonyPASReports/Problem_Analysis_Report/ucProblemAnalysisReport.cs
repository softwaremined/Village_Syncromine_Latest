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

namespace Mineware.Systems.Reports.Problem_Analysis_Report
{
    public partial class ucProblemAnalysisReport : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string ThePeriodText;
        public string TheSectionsText;   
        public string checking;
        public string TheAvailableText;
        public List<string> result;
        private string _pmonth;

        private ProblemAnalysisReportProperties reportSettings = new ProblemAnalysisReportProperties();

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgProblemAnalysis;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riPeriod;
        private Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth riFromProdmonth;
        private Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth riToProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riFromDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riToDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riSections;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSectionSingle;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit riSectionSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riActivity;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riType;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riRun;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riDetailsGraph;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riTrendGraph;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riPerShaft;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riLostBlasts;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riGraphInfo;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit riAvailable;

        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iPeriod;        
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iFromProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iToProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iFromDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iToDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSections;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSectionSingle;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSectionSelect;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iActivity;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iType;        
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow iRun;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iDetails;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iDetailsGraph;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iTrendGraph;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iPerShaft;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iLostBlasts;        
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iGraphInfo;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iAvailable;

        private string _theConnection;
        public string theConnection 
        { 
            get 
            { 
                return _theConnection; 
            } 
            set 
            { 
                _theConnection = value; 
            } 
        }

        public DataTable DTAvailable;
        public DataTable DTSections;
        public DataTable DTSectionsSelected;
        public DataTable TempProblemCode;

        public ucProblemAnalysisReport()
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pgProblemAnalysis = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riPeriod = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riFromProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riToProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riFromDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.riToDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.riSections = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riSectionSingle = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riSectionSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.riActivity = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riType = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riRun = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riDetails = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riDetailsGraph = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riTrendGraph = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riPerShaft = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riLostBlasts = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.riGraphInfo = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riAvailable = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.iPeriod = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iFromProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iToProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iFromDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iToDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSections = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSectionSingle = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSectionSelect = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iType = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iRun = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.iDetails = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iDetailsGraph = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iTrendGraph = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iPerShaft = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iLostBlasts = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iGraphInfo = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iAvailable = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgProblemAnalysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riToProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riToDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSectionSingle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSectionSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDetailsGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTrendGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPerShaft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riLostBlasts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGraphInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riAvailable)).BeginInit();
            this.SuspendLayout();
            // 
            // pgProblemAnalysis
            // 
            this.pgProblemAnalysis.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgProblemAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProblemAnalysis.Location = new System.Drawing.Point(0, 0);
            this.pgProblemAnalysis.Name = "pgProblemAnalysis";
            this.pgProblemAnalysis.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgProblemAnalysis.Padding = new System.Windows.Forms.Padding(5);
            this.pgProblemAnalysis.RecordWidth = 136;
            this.pgProblemAnalysis.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riPeriod,
            this.riFromProdmonth,
            this.riToProdmonth,
            this.riFromDate,
            this.riToDate,
            this.riSections,
            this.riSectionSingle,
            this.riSectionSelect,
            this.riActivity,
            this.riType,
            this.riRun,
            this.riDetails,
            this.riDetailsGraph,
            this.riTrendGraph,
            this.riPerShaft,
            this.riLostBlasts,
            this.riGraphInfo,
            this.riAvailable});
            this.pgProblemAnalysis.RowHeaderWidth = 64;
            this.pgProblemAnalysis.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iPeriod,
            this.iFromProdmonth,
            this.iToProdmonth,
            this.iFromDate,
            this.iToDate,
            this.iSections,
            this.iSectionSingle,
            this.iSectionSelect,
            this.iActivity,
            this.iType,
            this.iRun,
            this.iPerShaft,
            this.iLostBlasts,
            this.iGraphInfo,
            this.iAvailable});
            this.pgProblemAnalysis.Size = new System.Drawing.Size(431, 485);
            this.pgProblemAnalysis.TabIndex = 4;
            this.pgProblemAnalysis.Click += new System.EventHandler(this.pgProblemAnalysis_Click);
            // 
            // riPeriod
            // 
            this.riPeriod.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Prodmonth", "Production Month"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("FromTo", "From To Date")});
            this.riPeriod.Name = "riPeriod";
            this.riPeriod.EditValueChanged += new System.EventHandler(this.riPeriod_EditValueChanged);
            // 
            // riFromProdmonth
            // 
            this.riFromProdmonth.AutoHeight = false;
            this.riFromProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riFromProdmonth.Mask.EditMask = "yyyyMM";
            this.riFromProdmonth.Mask.IgnoreMaskBlank = false;
            this.riFromProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riFromProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riFromProdmonth.Name = "riFromProdmonth";
            // 
            // riToProdmonth
            // 
            this.riToProdmonth.AutoHeight = false;
            this.riToProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riToProdmonth.Mask.EditMask = "yyyyMM";
            this.riToProdmonth.Mask.IgnoreMaskBlank = false;
            this.riToProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riToProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riToProdmonth.Name = "riToProdmonth";
            // 
            // riFromDate
            // 
            this.riFromDate.AutoHeight = false;
            this.riFromDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riFromDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riFromDate.Name = "riFromDate";
            // 
            // riToDate
            // 
            this.riToDate.AutoHeight = false;
            this.riToDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riToDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riToDate.Name = "riToDate";
            // 
            // riSections
            // 
            this.riSections.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Single", "Single Section"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Selected", "Selected Shaft Manager")});
            this.riSections.Name = "riSections";
            this.riSections.EditValueChanged += new System.EventHandler(this.riSections_EditValueChanged);
            // 
            // riSectionSingle
            // 
            this.riSectionSingle.AutoHeight = false;
            this.riSectionSingle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riSectionSingle.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("sectionid", "SectionID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "NAME")});
            this.riSectionSingle.Name = "riSectionSingle";
            this.riSectionSingle.NullText = "";
            // 
            // riSectionSelect
            // 
            this.riSectionSelect.AutoHeight = false;
            this.riSectionSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riSectionSelect.Name = "riSectionSelect";
            // 
            // riActivity
            // 
            this.riActivity.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Stoping", "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Development", "Development")});
            this.riActivity.Name = "riActivity";
            // 
            // riType
            // 
            this.riType.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("ProbDesc", "Problem Description"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("ProbNote", "Problem Notes")});
            this.riType.Name = "riType";
            // 
            // riRun
            // 
            this.riRun.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Details"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Details Graph"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Trend Graph")});
            this.riRun.Name = "riRun";
            // 
            // riDetails
            // 
            this.riDetails.AutoHeight = false;
            this.riDetails.Caption = "Check";
            this.riDetails.Name = "riDetails";
            // 
            // riDetailsGraph
            // 
            this.riDetailsGraph.AutoHeight = false;
            this.riDetailsGraph.Caption = "Check";
            this.riDetailsGraph.Name = "riDetailsGraph";
            // 
            // riTrendGraph
            // 
            this.riTrendGraph.AutoHeight = false;
            this.riTrendGraph.Caption = "Check";
            this.riTrendGraph.Name = "riTrendGraph";
            // 
            // riPerShaft
            // 
            this.riPerShaft.AutoHeight = false;
            this.riPerShaft.Caption = "Check";
            this.riPerShaft.Name = "riPerShaft";
            // 
            // riLostBlasts
            // 
            this.riLostBlasts.AutoHeight = false;
            this.riLostBlasts.Caption = "Check";
            this.riLostBlasts.Name = "riLostBlasts";
            // 
            // riGraphInfo
            // 
            this.riGraphInfo.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Potential", "Potential Var m²"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("NoProb", "No Problems")});
            this.riGraphInfo.Name = "riGraphInfo";
            // 
            // riAvailable
            // 
            this.riAvailable.AutoHeight = false;
            this.riAvailable.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riAvailable.Name = "riAvailable";
            this.riAvailable.EditValueChanged += new System.EventHandler(this.riAvailable_EditValueChanged);
            // 
            // iPeriod
            // 
            this.iPeriod.Height = 40;
            this.iPeriod.IsChildRowsLoaded = true;
            this.iPeriod.Name = "iPeriod";
            this.iPeriod.Properties.Caption = "Period";
            this.iPeriod.Properties.FieldName = "Period";
            this.iPeriod.Properties.RowEdit = this.riPeriod;
            // 
            // iFromProdmonth
            // 
            this.iFromProdmonth.Height = 20;
            this.iFromProdmonth.IsChildRowsLoaded = true;
            this.iFromProdmonth.Name = "iFromProdmonth";
            this.iFromProdmonth.Properties.Caption = "From Production Month";
            this.iFromProdmonth.Properties.FieldName = "FromProdmonth";
            this.iFromProdmonth.Properties.RowEdit = this.riFromProdmonth;
            // 
            // iToProdmonth
            // 
            this.iToProdmonth.Height = 20;
            this.iToProdmonth.IsChildRowsLoaded = true;
            this.iToProdmonth.Name = "iToProdmonth";
            this.iToProdmonth.Properties.Caption = "To Production Month";
            this.iToProdmonth.Properties.FieldName = "ToProdmonth";
            this.iToProdmonth.Properties.RowEdit = this.riToProdmonth;
            // 
            // iFromDate
            // 
            this.iFromDate.Height = 20;
            this.iFromDate.IsChildRowsLoaded = true;
            this.iFromDate.Name = "iFromDate";
            this.iFromDate.Properties.Caption = "From Date";
            this.iFromDate.Properties.FieldName = "FromDate";
            this.iFromDate.Properties.RowEdit = this.riFromDate;
            // 
            // iToDate
            // 
            this.iToDate.Height = 20;
            this.iToDate.IsChildRowsLoaded = true;
            this.iToDate.Name = "iToDate";
            this.iToDate.Properties.Caption = "To Date";
            this.iToDate.Properties.FieldName = "ToDate";
            this.iToDate.Properties.RowEdit = this.riToDate;
            // 
            // iSections
            // 
            this.iSections.Height = 40;
            this.iSections.IsChildRowsLoaded = true;
            this.iSections.Name = "iSections";
            this.iSections.Properties.Caption = "Sections";
            this.iSections.Properties.FieldName = "Sections";
            this.iSections.Properties.RowEdit = this.riSections;
            this.iSections.Visible = false;
            // 
            // iSectionSingle
            // 
            this.iSectionSingle.Height = 20;
            this.iSectionSingle.IsChildRowsLoaded = true;
            this.iSectionSingle.Name = "iSectionSingle";
            this.iSectionSingle.Properties.Caption = "Section";
            this.iSectionSingle.Properties.FieldName = "NAME";
            this.iSectionSingle.Properties.RowEdit = this.riSectionSingle;
            // 
            // iSectionSelect
            // 
            this.iSectionSelect.Height = 20;
            this.iSectionSelect.IsChildRowsLoaded = true;
            this.iSectionSelect.Name = "iSectionSelect";
            this.iSectionSelect.Properties.Caption = "Section Select";
            this.iSectionSelect.Properties.FieldName = "SectionSelect";
            this.iSectionSelect.Properties.RowEdit = this.riSectionSelect;
            this.iSectionSelect.Visible = false;
            // 
            // iActivity
            // 
            this.iActivity.Height = 40;
            this.iActivity.IsChildRowsLoaded = true;
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Activity";
            this.iActivity.Properties.RowEdit = this.riActivity;
            // 
            // iType
            // 
            this.iType.Height = 40;
            this.iType.IsChildRowsLoaded = true;
            this.iType.Name = "iType";
            this.iType.Properties.Caption = "Type";
            this.iType.Properties.FieldName = "Type";
            this.iType.Properties.RowEdit = this.riType;
            this.iType.Visible = false;
            // 
            // iRun
            // 
            this.iRun.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iDetails,
            this.iDetailsGraph,
            this.iTrendGraph});
            this.iRun.Height = 17;
            this.iRun.Name = "iRun";
            this.iRun.Properties.Caption = "Run";
            this.iRun.Visible = false;
            // 
            // iDetails
            // 
            this.iDetails.Height = 20;
            this.iDetails.Name = "iDetails";
            this.iDetails.Properties.Caption = "Details";
            this.iDetails.Properties.FieldName = "Details";
            this.iDetails.Properties.RowEdit = this.riDetails;
            // 
            // iDetailsGraph
            // 
            this.iDetailsGraph.Height = 20;
            this.iDetailsGraph.Name = "iDetailsGraph";
            this.iDetailsGraph.Properties.Caption = "Details Graph";
            this.iDetailsGraph.Properties.FieldName = "DetailsGraph";
            this.iDetailsGraph.Properties.RowEdit = this.riDetailsGraph;
            // 
            // iTrendGraph
            // 
            this.iTrendGraph.Height = 20;
            this.iTrendGraph.Name = "iTrendGraph";
            this.iTrendGraph.Properties.Caption = "Trend Graph";
            this.iTrendGraph.Properties.FieldName = "TrendGraph";
            this.iTrendGraph.Properties.RowEdit = this.riTrendGraph;
            // 
            // iPerShaft
            // 
            this.iPerShaft.Height = 20;
            this.iPerShaft.Name = "iPerShaft";
            this.iPerShaft.Properties.Caption = "Per Shaft";
            this.iPerShaft.Properties.FieldName = "PerShaft";
            this.iPerShaft.Properties.RowEdit = this.riPerShaft;
            this.iPerShaft.Visible = false;
            // 
            // iLostBlasts
            // 
            this.iLostBlasts.Height = 20;
            this.iLostBlasts.Name = "iLostBlasts";
            this.iLostBlasts.Properties.Caption = "Lost Blasts";
            this.iLostBlasts.Properties.FieldName = "LostBlasts";
            this.iLostBlasts.Properties.RowEdit = this.riLostBlasts;
            this.iLostBlasts.Visible = false;
            // 
            // iGraphInfo
            // 
            this.iGraphInfo.Height = 40;
            this.iGraphInfo.IsChildRowsLoaded = true;
            this.iGraphInfo.Name = "iGraphInfo";
            this.iGraphInfo.Properties.Caption = "Graph Info";
            this.iGraphInfo.Properties.FieldName = "GraphInfo";
            this.iGraphInfo.Properties.RowEdit = this.riGraphInfo;
            this.iGraphInfo.Visible = false;
            // 
            // iAvailable
            // 
            this.iAvailable.Height = 20;
            this.iAvailable.IsChildRowsLoaded = true;
            this.iAvailable.Name = "iAvailable";
            this.iAvailable.Properties.Caption = "Available";
            this.iAvailable.Properties.FieldName = "Available";
            this.iAvailable.Properties.RowEdit = this.riAvailable;
            // 
            // ucProblemAnalysisReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseForeColor = true;
            this.Controls.Add(this.pgProblemAnalysis);
            this.Name = "ucProblemAnalysisReport";
            this.Size = new System.Drawing.Size(431, 485);
            this.Load += new System.EventHandler(this.ucProblemAnalysisReport_Load);
            this.Controls.SetChildIndex(this.pgProblemAnalysis, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgProblemAnalysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riToProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riToDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSectionSingle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSectionSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riDetailsGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTrendGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riPerShaft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riLostBlasts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGraphInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riAvailable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public string GetAvailableList()
        {
            MWDataManager.clsDataAccess _Available = new MWDataManager.clsDataAccess();
            _Available.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _Available.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _Available.SqlStatement = "SELECT [dbo].[Get_ProblemID_AGG]()";
            _Available.queryReturnType = MWDataManager.ReturnType.DataTable;
            _Available.ExecuteInstruction();
            if (_Available.ResultsDataTable.Rows.Count > 0)
            {
                return _Available.ResultsDataTable.Rows[0][0].ToString();
            }
            else
            {
                return "A";
            }
        }

        public DataTable GetAvailable()
        {
            MWDataManager.clsDataAccess _Available = new MWDataManager.clsDataAccess();
            _Available.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _Available.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _Available.SqlStatement = "Select * from CODE_PROBLEM where activity = 0 and deleted = 'N'";
            _Available.queryReturnType = MWDataManager.ReturnType.DataTable;
            _Available.ExecuteInstruction();

            return _Available.ResultsDataTable;
        }
        
        public void GetAvailableData()
        {
            DTAvailable = GetAvailable();

            riAvailable.DataSource = DTAvailable;
            riAvailable.DisplayMember = "Description";
            riAvailable.ValueMember = "ProblemID";
        }

        private void ucProblemAnalysisReport_Load(object sender, EventArgs e)
        {
            ActiveReport.UserCurrentInfo = this.UserCurrentInfo;         
            reportSettings.FromProdmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            reportSettings.ToProdmonth = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            reportSettings.FromDate = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Rundate;
            reportSettings.ToDate = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Rundate;

            reportSettings.Period = "Prodmonth";
            reportSettings.Sections = "Single";
            reportSettings.Activity = "Stoping";
            reportSettings.Type = "ProbDesc";
            reportSettings.GraphInfo = "Potential";

            reportSettings.Available = GetAvailableList();
                        
            reportSettings.Details = false;
            reportSettings.DetailsGraph = false;
            reportSettings.TrendGraph = false;
            reportSettings.PerShaft = false;
            reportSettings.LostBlasts = false;
            iFromProdmonth.Visible = true;
            iToProdmonth.Visible = true;
            iToDate.Visible  = false;
            iFromDate.Visible = false;
           
            iSectionSelect.Enabled = false;

            GetAvailableData();

            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;
            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            _pmonth = TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString();
            
            DataTable section = new DataTable();

            if (BMEBL.GetPlanSectionsAndNameADO(_pmonth) == true)
            {
                section = BMEBL.ResultsDataTable;
                riSectionSingle.DataSource = BMEBL.ResultsDataTable;
                riSectionSingle.DisplayMember = "NAME";
                riSectionSingle.ValueMember = "NAME";
            }
            reportSettings.NAME = section.Rows[0]["NAME"].ToString();
            pgProblemAnalysis.SelectedObject = reportSettings;
        }

        private void riPeriod_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.RadioGroup editor = (sender as DevExpress.XtraEditors.RadioGroup);
            ThePeriodText = editor.SelectedIndex.ToString();
            pgProblemAnalysis.PostEditor();
            if (ThePeriodText == "0")
            {
                iFromProdmonth.Visible = true;
                iToProdmonth.Visible = true;
                iFromDate.Visible = false;
                iToDate.Visible = false;
            }
            else if (ThePeriodText == "1")
            {
                iFromProdmonth.Visible = false;
                iToProdmonth.Visible = false;
                iFromDate.Visible = true;
                iToDate.Visible = true;
            }
        }

        private void riSections_EditValueChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.RadioGroup editor = (sender as DevExpress.XtraEditors.RadioGroup);
            //TheSectionsText = editor.Properties.GetDisplayText(editor.EditValue);
            TheSectionsText = editor.SelectedIndex.ToString();

            if (TheSectionsText == "0")
            {
                iSectionSingle.Enabled = true;
                iSectionSelect.Enabled = false;
            }
            else if (TheSectionsText == "1")
            {
                iSectionSingle.Enabled = false;
                iSectionSelect.Enabled = true;
            }
        }

        private void riAvailable_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        public override bool prepareReport()
        {
            bool theResult;

            if (reportSettings.Available == "")
            {
                // sysMessages.viewMessage(Global.MessageType.Info, "Select Value", "Please make a user selection for the report.", Global.ButtonTypes.OK, Global.MessageDisplayType.FullScreen);
                MessageBox.Show("Please Select At Least one Problem Type", "", MessageBoxButtons.OK);
                theResult = false;
            }
            else
            {
                theResult = true;
            }
            if (theResult == true)
            {
                theReportThread = new Thread(new ParameterizedThreadStart(createReport));
                theReportThread.SetApartmentState(ApartmentState.STA);
                theReportThread.Start(reportSettings);
                theResult = true;
            }
            //  }
            return theResult;
        }

        private void createReport(Object theReportSettings)
        {
            Report theReport = new Report();
            ProblemAnalysisReportProperties currentReportSettings = theReportSettings as ProblemAnalysisReportProperties;
            ucProblemAnalysisReport _ProblemAnalysisReport = new ucProblemAnalysisReport { theConnection = ActiveReport.UserCurrentInfo.Connection };

            //DevExpress.XtraEditors.CheckedComboBoxEdit editor = (sender as DevExpress.XtraEditors.CheckedComboBoxEdit);
            //editor.CheckAll();
            TheAvailableText = reportSettings.Available;
            result = TheAvailableText.Split(',').ToList();

            MWDataManager.clsDataAccess _ProblemAnalysis = new MWDataManager.clsDataAccess();
            _ProblemAnalysis.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _ProblemAnalysis.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _ProblemAnalysis.SqlStatement = "DELETE FROM Temp_Problem_Analysis";
            _ProblemAnalysis.queryReturnType = MWDataManager.ReturnType.longNumber;
            _ProblemAnalysis.ExecuteInstruction();

            foreach (string ProblemCode in result)
            {
                _ProblemAnalysis.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _ProblemAnalysis.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _ProblemAnalysis.SqlStatement = "Insert into Temp_Problem_Analysis Values ('" + UserCurrentInfo.UserID + "', '" + ProblemCode.Trim() + "')";
                _ProblemAnalysis.queryReturnType = MWDataManager.ReturnType.longNumber;
                _ProblemAnalysis.ExecuteInstruction();
            }

            //DataSet repUserActivitySet = new DataSet();  
            //MWDataManager.clsDataAccess _ProblemAnalysis = new MWDataManager.clsDataAccess();

            if (reportSettings.Activity == "Stoping")
            {
                try
                {
                    _ProblemAnalysis.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _ProblemAnalysis.SqlStatement = "SP_Problem_Analysis_Report_Stp";
                    _ProblemAnalysis.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _ProblemAnalysis.ResultsTableName = "Problem Analysis Data Stp";

                    SqlParameter[] _paramCollection = 
                            {
                                _ProblemAnalysis.CreateParameter("@Period", SqlDbType.VarChar, 10, reportSettings.Period),
                                _ProblemAnalysis.CreateParameter("@FromMonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsString(reportSettings.FromProdmonth)),
                                _ProblemAnalysis.CreateParameter("@ToMonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsString(reportSettings.ToProdmonth)),
                                _ProblemAnalysis.CreateParameter("@Section", SqlDbType.VarChar , 60, reportSettings.NAME),
                                _ProblemAnalysis.CreateParameter("@UserID", SqlDbType.VarChar , 50, UserCurrentInfo.UserID),
                                _ProblemAnalysis.CreateParameter("@FromDate", SqlDbType.Date, 50, reportSettings.FromDate),
                                _ProblemAnalysis.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ToDate),
                            };

                    _ProblemAnalysis.ParamCollection = _paramCollection;
                    _ProblemAnalysis.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _ProblemAnalysis.ExecuteInstruction();
                }
                catch (Exception _exception)
                {
                    throw new ApplicationException("Report Section:_ProblemAnalysis:" + _exception.Message, _exception);
                }

                DataSet repDataSet = new DataSet();
                repDataSet.Tables.Add(_ProblemAnalysis.ResultsDataTable);
                theReport.RegisterData(repDataSet);

                theReport.Load(TGlobalItems.ReportsFolder + "\\ProblemAnalysisReport_Stp.frx");

                theReport.SetParameterValue("Banner", SysSettings.Banner);
                theReport.SetParameterValue("Prodmonth", TProductionGlobal.ProdMonthAsString(reportSettings.FromProdmonth) + " - " + TProductionGlobal.ProdMonthAsString(reportSettings.ToProdmonth));
                theReport.SetParameterValue("Sections", reportSettings.NAME);
                theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
                theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);
            }
            else if (reportSettings.Activity == "Development")
            {
                try
                {
                    _ProblemAnalysis.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _ProblemAnalysis.SqlStatement = "SP_Problem_Analysis_Report_Dev";
                    _ProblemAnalysis.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
                    _ProblemAnalysis.ResultsTableName = "Problem Analysis Data Dev";

                    SqlParameter[] _paramCollection = 
                            {
                                _ProblemAnalysis.CreateParameter("@Period", SqlDbType.VarChar, 10, reportSettings.Period),
                                _ProblemAnalysis.CreateParameter("@FromMonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsString(reportSettings.FromProdmonth)),
                                _ProblemAnalysis.CreateParameter("@ToMonth", SqlDbType.Int, 0, TProductionGlobal.ProdMonthAsString(reportSettings.ToProdmonth)),
                                _ProblemAnalysis.CreateParameter("@Section", SqlDbType.VarChar , 60, reportSettings.NAME),
                                _ProblemAnalysis.CreateParameter("@UserID", SqlDbType.VarChar , 50, UserCurrentInfo.UserID),
                                _ProblemAnalysis.CreateParameter("@FromDate", SqlDbType.Date, 50, reportSettings.FromDate),
                                _ProblemAnalysis.CreateParameter("@ToDate", SqlDbType.Date, 50, reportSettings.ToDate),
                            };

                    _ProblemAnalysis.ParamCollection = _paramCollection;
                    _ProblemAnalysis.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _ProblemAnalysis.ExecuteInstruction();
                }
                catch (Exception _exception)
                {
                    throw new ApplicationException("Report Section:_ProblemAnalysis:" + _exception.Message, _exception);
                }

                DataSet repDataSet = new DataSet();
                repDataSet.Tables.Add(_ProblemAnalysis.ResultsDataTable);
                theReport.RegisterData(repDataSet);

                theReport.Load(TGlobalItems.ReportsFolder + "\\ProblemAnalysisReport_Dev.frx");

                theReport.SetParameterValue("Banner", SysSettings.Banner);
                theReport.SetParameterValue("Prodmonth", TProductionGlobal.ProdMonthAsString(reportSettings.FromProdmonth) + " - " + TProductionGlobal.ProdMonthAsString(reportSettings.ToProdmonth));
                theReport.SetParameterValue("Sections", reportSettings.NAME);
            }     
            

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

        private void pgProblemAnalysis_Click(object sender, EventArgs e)
        {

        }
    }
}
