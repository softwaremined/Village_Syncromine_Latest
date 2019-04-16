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
using System.Drawing;

namespace Mineware.Systems.Reports.MODailyDetail
{
    public partial class ucMODailyProduction : Mineware.Systems.Global.ReportsControls.ucReportSettingsControl
    {
        private Thread theReportThread;
        string theSystemDBTag = "DBHARMONYPAS";
        public string checking;
        DataTable dtSections = new DataTable();
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        //Procedures procs = new Procedures();
        Report KPIDbl = new Report();

        Report KPIDbl1 = new Report();

        Report theReportVamp = new Report();
        Report theReportDev = new Report();
        Report theReport = new Report();


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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions11 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject41 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject42 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject43 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject44 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions12 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject45 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject46 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject47 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject48 = new DevExpress.Utils.SerializableAppearanceObject();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pgMODaily = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riReportDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.rpSectionID = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.repositoryItemRadioGroup2 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iReportDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.iColors = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.MODailyWPLblNS = new System.Windows.Forms.Label();
            this.MODailyWPLblVamp = new System.Windows.Forms.Label();
            this.MODailyWPLblDev = new System.Windows.Forms.Label();
            this.MODailyDateLbl = new System.Windows.Forms.Label();
            this.MODailyWPLbl = new System.Windows.Forms.Label();
            this.MODailyClickLbl = new System.Windows.Forms.Label();
            this.Stoping = new System.Windows.Forms.DataGridView();
            this.StartDate1 = new System.Windows.Forms.DateTimePicker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ABSBtn = new System.Windows.Forms.Button();
            this.btnColors = new System.Windows.Forms.Button();
            this.lblColors = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pgMODaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stoping)).BeginInit();
            this.SuspendLayout();
            // 
            // pgMODaily
            // 
            this.pgMODaily.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgMODaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgMODaily.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgMODaily.Location = new System.Drawing.Point(0, 0);
            this.pgMODaily.Margin = new System.Windows.Forms.Padding(1);
            this.pgMODaily.Name = "pgMODaily";
            this.pgMODaily.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgMODaily.Padding = new System.Windows.Forms.Padding(2);
            this.pgMODaily.RecordWidth = 140;
            this.pgMODaily.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riReportDate,
            this.rpSectionID,
            this.riProdmonth,
            this.repositoryItemRadioGroup1,
            this.repositoryItemRadioGroup2});
            this.pgMODaily.RowHeaderWidth = 60;
            this.pgMODaily.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonth,
            this.iSection,
            this.iReportDate,
            this.iActivity,
            this.iColors});
            this.pgMODaily.Size = new System.Drawing.Size(315, 311);
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions11, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject41, serializableAppearanceObject42, serializableAppearanceObject43, serializableAppearanceObject44, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions12, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject45, serializableAppearanceObject46, serializableAppearanceObject47, serializableAppearanceObject48, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.riProdmonth.Mask.EditMask = "yyyyMM";
            this.riProdmonth.Mask.IgnoreMaskBlank = false;
            this.riProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riProdmonth.Name = "riProdmonth";
            this.riProdmonth.EditValueChanged += new System.EventHandler(this.riProdmonth_EditValueChanged);
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repositoryItemRadioGroup1.Appearance.Options.UseFont = true;
            this.repositoryItemRadioGroup1.ColumnIndent = 0;
            this.repositoryItemRadioGroup1.Columns = 2;
            this.repositoryItemRadioGroup1.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Development")});
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            // 
            // repositoryItemRadioGroup2
            // 
            this.repositoryItemRadioGroup2.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "Safety"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Blast")});
            this.repositoryItemRadioGroup2.Name = "repositoryItemRadioGroup2";
            this.repositoryItemRadioGroup2.SelectedIndexChanged += new System.EventHandler(this.repositoryItemRadioGroup2_SelectedIndexChanged);
            // 
            // iProdmonth
            // 
            this.iProdmonth.IsChildRowsLoaded = true;
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.Caption = "ProdMonth";
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
            // iActivity
            // 
            this.iActivity.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iActivity.Appearance.Options.UseFont = true;
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Activity";
            this.iActivity.Properties.RowEdit = this.repositoryItemRadioGroup1;
            // 
            // iColors
            // 
            this.iColors.Name = "iColors";
            this.iColors.Properties.Caption = "Colors";
            this.iColors.Properties.FieldName = "Color";
            this.iColors.Properties.RowEdit = this.repositoryItemRadioGroup2;
            // 
            // MODailyWPLblNS
            // 
            this.MODailyWPLblNS.AutoSize = true;
            this.MODailyWPLblNS.Location = new System.Drawing.Point(76, 243);
            this.MODailyWPLblNS.Name = "MODailyWPLblNS";
            this.MODailyWPLblNS.Size = new System.Drawing.Size(35, 13);
            this.MODailyWPLblNS.TabIndex = 89;
            this.MODailyWPLblNS.Text = "label6";
            this.MODailyWPLblNS.Visible = false;
            // 
            // MODailyWPLblVamp
            // 
            this.MODailyWPLblVamp.AutoSize = true;
            this.MODailyWPLblVamp.Location = new System.Drawing.Point(57, 262);
            this.MODailyWPLblVamp.Name = "MODailyWPLblVamp";
            this.MODailyWPLblVamp.Size = new System.Drawing.Size(35, 13);
            this.MODailyWPLblVamp.TabIndex = 88;
            this.MODailyWPLblVamp.Text = "label6";
            this.MODailyWPLblVamp.Visible = false;
            // 
            // MODailyWPLblDev
            // 
            this.MODailyWPLblDev.AutoSize = true;
            this.MODailyWPLblDev.Location = new System.Drawing.Point(302, 253);
            this.MODailyWPLblDev.Name = "MODailyWPLblDev";
            this.MODailyWPLblDev.Size = new System.Drawing.Size(35, 13);
            this.MODailyWPLblDev.TabIndex = 87;
            this.MODailyWPLblDev.Text = "label6";
            this.MODailyWPLblDev.Visible = false;
            // 
            // MODailyDateLbl
            // 
            this.MODailyDateLbl.AutoSize = true;
            this.MODailyDateLbl.Location = new System.Drawing.Point(302, 240);
            this.MODailyDateLbl.Name = "MODailyDateLbl";
            this.MODailyDateLbl.Size = new System.Drawing.Size(35, 13);
            this.MODailyDateLbl.TabIndex = 86;
            this.MODailyDateLbl.Text = "label6";
            this.MODailyDateLbl.Visible = false;
            // 
            // MODailyWPLbl
            // 
            this.MODailyWPLbl.AutoSize = true;
            this.MODailyWPLbl.Location = new System.Drawing.Point(57, 249);
            this.MODailyWPLbl.Name = "MODailyWPLbl";
            this.MODailyWPLbl.Size = new System.Drawing.Size(35, 13);
            this.MODailyWPLbl.TabIndex = 85;
            this.MODailyWPLbl.Text = "label6";
            this.MODailyWPLbl.Visible = false;
            // 
            // MODailyClickLbl
            // 
            this.MODailyClickLbl.AutoSize = true;
            this.MODailyClickLbl.Location = new System.Drawing.Point(76, 264);
            this.MODailyClickLbl.Name = "MODailyClickLbl";
            this.MODailyClickLbl.Size = new System.Drawing.Size(35, 13);
            this.MODailyClickLbl.TabIndex = 84;
            this.MODailyClickLbl.Text = "label6";
            this.MODailyClickLbl.Visible = false;
            // 
            // Stoping
            // 
            this.Stoping.AllowUserToAddRows = false;
            this.Stoping.AllowUserToResizeColumns = false;
            this.Stoping.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Stoping.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.Stoping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Stoping.ColumnHeadersVisible = false;
            this.Stoping.Cursor = System.Windows.Forms.Cursors.Default;
            this.Stoping.GridColor = System.Drawing.Color.White;
            this.Stoping.Location = new System.Drawing.Point(117, 231);
            this.Stoping.Name = "Stoping";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Stoping.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.Stoping.RowHeadersVisible = false;
            this.Stoping.RowTemplate.Height = 15;
            this.Stoping.Size = new System.Drawing.Size(51, 33);
            this.Stoping.TabIndex = 90;
            this.Stoping.Visible = false;
            // 
            // StartDate1
            // 
            this.StartDate1.Location = new System.Drawing.Point(15, 243);
            this.StartDate1.Name = "StartDate1";
            this.StartDate1.Size = new System.Drawing.Size(55, 21);
            this.StartDate1.TabIndex = 91;
            this.StartDate1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 240);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 52);
            this.textBox1.TabIndex = 95;
            this.textBox1.Visible = false;
            // 
            // ABSBtn
            // 
            this.ABSBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ABSBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ABSBtn.ImageKey = "Game Levels.bmp";
            this.ABSBtn.Location = new System.Drawing.Point(24, 249);
            this.ABSBtn.Name = "ABSBtn";
            this.ABSBtn.Size = new System.Drawing.Size(75, 25);
            this.ABSBtn.TabIndex = 96;
            this.ABSBtn.Text = "Remove Colours";
            this.ABSBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ABSBtn.UseVisualStyleBackColor = false;
            this.ABSBtn.Visible = false;
            this.ABSBtn.Click += new System.EventHandler(this.ABSBtn_Click);
            // 
            // btnColors
            // 
            this.btnColors.Location = new System.Drawing.Point(213, 253);
            this.btnColors.Name = "btnColors";
            this.btnColors.Size = new System.Drawing.Size(83, 47);
            this.btnColors.TabIndex = 98;
            this.btnColors.Text = "Blast/Problem Colors";
            this.btnColors.UseVisualStyleBackColor = true;
            this.btnColors.Visible = false;
            this.btnColors.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // lblColors
            // 
            this.lblColors.AutoSize = true;
            this.lblColors.Location = new System.Drawing.Point(127, 185);
            this.lblColors.Name = "lblColors";
            this.lblColors.Size = new System.Drawing.Size(59, 13);
            this.lblColors.TabIndex = 99;
            this.lblColors.Text = "ABS Colors";
            this.lblColors.Visible = false;
            // 
            // ucMODailyProduction
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.Controls.Add(this.Stoping);
            this.Controls.Add(this.lblColors);
            this.Controls.Add(this.btnColors);
            this.Controls.Add(this.ABSBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.StartDate1);
            this.Controls.Add(this.MODailyWPLblNS);
            this.Controls.Add(this.MODailyWPLblVamp);
            this.Controls.Add(this.MODailyWPLblDev);
            this.Controls.Add(this.MODailyDateLbl);
            this.Controls.Add(this.MODailyWPLbl);
            this.Controls.Add(this.MODailyClickLbl);
            this.Controls.Add(this.pgMODaily);
            this.Name = "ucMODailyProduction";
            this.Size = new System.Drawing.Size(315, 311);
            this.Load += new System.EventHandler(this.ucMODailyProduction_Load);
            this.Controls.SetChildIndex(this.pgMODaily, 0);
            this.Controls.SetChildIndex(this.MODailyClickLbl, 0);
            this.Controls.SetChildIndex(this.MODailyWPLbl, 0);
            this.Controls.SetChildIndex(this.MODailyDateLbl, 0);
            this.Controls.SetChildIndex(this.MODailyWPLblDev, 0);
            this.Controls.SetChildIndex(this.MODailyWPLblVamp, 0);
            this.Controls.SetChildIndex(this.MODailyWPLblNS, 0);
            this.Controls.SetChildIndex(this.StartDate1, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.ABSBtn, 0);
            this.Controls.SetChildIndex(this.btnColors, 0);
            this.Controls.SetChildIndex(this.lblColors, 0);
            this.Controls.SetChildIndex(this.Stoping, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgMODaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stoping)).EndInit();
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
            reportSettings.Activity = 0;
            iActivity.Properties.Value = reportSettings.Activity;

            reportSettings.Color = "0";
            iColors.Properties.Value = reportSettings.Color;


            MWDataManager.clsDataAccess _dbManCheck = new MWDataManager.clsDataAccess();
            _dbManCheck.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManCheck.SqlStatement = "select CheckMeas,Banner from SysSet ";
            _dbManCheck.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManCheck.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _dbManLosses.ResultsTableName = "Days";  //get table name
            _dbManCheck.ExecuteInstruction();

          

            SysSettings.CheckMeas = _dbManCheck.ResultsDataTable.Rows[0][0].ToString();
            SysSettings.Banner = _dbManCheck.ResultsDataTable.Rows[0][1].ToString();
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
            //Report theReport = new Report();
            //MODailyProductionSettingsProperties currentReportSettings = theReportSettings as MODailyProductionSettingsProperties;
            //ucMODailyProduction _ucEfficiency = new ucMODailyProduction { theConnection = ActiveReport.UserCurrentInfo.Connection };

            ////DataSet repUserActivitySet = new DataSet();
            //MWDataManager.clsDataAccess _StopeReportData = new MWDataManager.clsDataAccess();
            //MWDataManager.clsDataAccess _DevReportData = new MWDataManager.clsDataAccess();
            //try

            //{
            //    _StopeReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _StopeReportData.SqlStatement = "SP_MODailyReport";
            //    _StopeReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _StopeReportData.ResultsTableName = "MODailyStopeData";

            //    SqlParameter[] _paramCollection =
            //        {
            //                _StopeReportData.CreateParameter("@Prodmonth", SqlDbType.VarChar, 6, TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)),
            //                _StopeReportData.CreateParameter("@Section", SqlDbType.Text, 100, reportSettings.SectionID),
            //                _StopeReportData.CreateParameter("@TheDate", SqlDbType.DateTime, 50,reportSettings.ReportDate),
            //        };

            //    _StopeReportData.ParamCollection = _paramCollection;
            //    _StopeReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    _StopeReportData.ExecuteInstruction();

            //    _DevReportData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _DevReportData.SqlStatement = "SP_MODaily_Dev_Report";
            //    _DevReportData.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //    _DevReportData.ResultsTableName = "MODailyDevData";

            //    SqlParameter[] _DevparamCollection =
            //        {
            //                _DevReportData.CreateParameter("@Prodmonth", SqlDbType.VarChar, 6, TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth)),
            //                _DevReportData.CreateParameter("@Section", SqlDbType.Text, 100, reportSettings.SectionID),
            //                _DevReportData.CreateParameter("@TheDate", SqlDbType.DateTime, 50,reportSettings.ReportDate),
            //        };

            //    _DevReportData.ParamCollection = _DevparamCollection;
            //    _DevReportData.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    _DevReportData.ExecuteInstruction();
            //}
            //catch (Exception _exception)
            //{
            //    throw new ApplicationException("Report Section:MODailyStopeData:" + _exception.Message, _exception);
            //}


            //DataSet repDataSet = new DataSet();
            //repDataSet.Tables.Add(_StopeReportData.ResultsDataTable);
            //repDataSet.Tables.Add(_DevReportData.ResultsDataTable);
            //theReport.RegisterData(repDataSet);

            //theReport.Load(TGlobalItems.ReportsFolder + "\\MODaily.frx");

            //theReport.SetParameterValue("Banner", TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).Mine);
            //theReport.SetParameterValue("Prodmonth", TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth));
            //theReport.SetParameterValue("Section", reportSettings.SectionID);
            //theReport.SetParameterValue("TheDate", reportSettings.ReportDate);
            //theReport.SetParameterValue("logo", TGlobalItems.ClientLogo);
            //theReport.SetParameterValue("logoMineware", TGlobalItems.CompanyLogo);

            //if (TParameters.DesignReport)
            //{
            //    theReport.Design();
            //}

            //theReport.Prepare();
            //ActiveReport.SetReport = theReport;
            //ActiveReport.isDone = true;

            //if (SelectGroup.SelectedIndex == 0)
            //    LoadStoping();


            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = _dbMan1.SqlStatement + "select * from Code_TopPanelsReport \r\n";

            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ResultsTableName = "Banner";
            _dbMan1.ExecuteInstruction();

            NoTopPanels = _dbMan1.ResultsDataTable.Rows[0][0].ToString();


            if (reportSettings.Activity == 0)
                LoadStoping();

            if (reportSettings.Activity == 1)
                LoadDevelopment();

        }
        string NoTopPanels = "";
        void LoadDevelopment()
        {
            DataGridView dt = new DataGridView();
            string WPLable = "";
            string SBLabel = "";
            string CurrShftLbl = "";
            Stoping = dt;
           // Stoping.Parent = panel1;
            //Stoping.Dock = DockStyle.Fill;
            Stoping.Visible = false;

            

            string month2 = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);
            string Section = procs.ExtractBeforeColon(reportSettings.SectionID);


            Stoping.RowCount = 600;
            Stoping.ColumnCount = 96;
            //Stoping.ColumnCount = 54;

            //set blanks for RiskRating column
            for (int w = 0; w < Stoping.RowCount; w++)
            {
                Stoping.Rows[w].Cells[50].Value = "";
                Stoping.Rows[w].Cells[51].Value = "";
                Stoping.Rows[w].Cells[53].Value = "";
            }

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan.SqlStatement = " declare @prev integer \r\n";

            _dbMan.SqlStatement = _dbMan.SqlStatement + "set @prev = ( select max(prodmonth) aaaa from vw_PlanMonth where prodmonth < '" + month2 + "')\r\n ";


            _dbMan.SqlStatement = _dbMan.SqlStatement + "select case when pmold1 is null then 'Red' when pmold1 is not null and pmold1 < @prev then 'orange' else '' end as newwpflag, \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "* from (select e.description endtt, p.workplaceid, w.riskrating, convert(numeric(18,1),BookMetresadvance) bookadv, MOFC CheckSqm,  p.pegid, p.Pumahola, isnull(p.bookprob,'')bookprob, p.pegtoface, p.bookcode, p.workingday, p.adv, p.shiftday, p.sectionid, cr.CrewName orgunitds, p.activity, p.calendardate, w.description, w.priority, pm.fl, pm.adv sqmtotal, s.Sectionid_1 sbid, s.Name_1 sbname, p.ABSCode ABSCode, isnull(booktons,0)booktons, p.MOFC \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "  from vw_Planning p, vw_PlanMonth pm,crew cr, (select * from SECTION_COMPLETE where prodmonth = '" + month2 + "' and Sectionid_2 = '" + Section + "') s, Workplace w, ENDTYPE e\r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "where p.workplaceid = pm.workplaceid and p.sectionid = pm.sectionid and p.prodmonth = pm.prodmonth and p.activity = pm.activity and cr.GangNo = pm.OrgUnitDay and \r\n";
            //  if (SysSettings.Banner == "Moab Khotsong" && month2 == "201701" && Section == "4202" )
            // {
            //     _dbMan.SqlStatement = _dbMan.SqlStatement + " calendardate not in ( '2016-12-24', '2016-12-25', '2016-12-26', '2016-12-27', '2016-12-28', '2016-12-29', '2016-12-30', '2016-12-31', '2017-01-01', '2017-01-02', '2017-01-03', '2017-01-04', '2017-01-05', '2017-01-06', '2017-01-07') and ";
            //  }
            _dbMan.SqlStatement = _dbMan.SqlStatement + "p.prodmonth = s.prodmonth and p.sectionid = s.sectionid \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "and p.workplaceid = w.workplaceid and w.endtypeid = e.endtypeid and \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "p.prodmonth = '" + month2 + "' \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "and p.activity in (1) ) a \r\n";

            _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "(select workplaceid wz, max(prodmonth) pmold1 from vw_PlanMonth \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "where prodmonth < '" + month2 + "' group by workplaceid) newwp on  a.workplaceid = newwp.wz \r\n";


            _dbMan.SqlStatement = _dbMan.SqlStatement + "order by a.sbid, a.sectionid, a.orgunitds ,a.activity, a.description, a.calendardate\r\n";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();





            ////aaaaaaa


            DataTable Neil = _dbMan.ResultsDataTable;

            if (Neil.Rows.Count == 0)
            {
                MessageBox.Show("No Planning or Bookings have been entered for                                            " +
                   "Section   " + procs.ExtractBeforeColon(reportSettings.SectionID) + "", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Cursor = Cursors.Default;
                ActiveReport.isDone = true;
                return;
            }

            int x = 1;
            x = 1;

            int col = 3;
            TimeSpan Span;

            string checkm;

            StartDate1.Value = Convert.ToDateTime(Neil.Rows[0]["calendardate"].ToString());
            SBLabel = Neil.Rows[0]["sbid"].ToString() + ":" + Neil.Rows[0]["sbname"].ToString();

            CurrShftLbl = "1";
            WPLable = "";
            Stoping.Columns[0].HeaderText = "Section";
            Stoping.Columns[1].HeaderText = "Gang";
            Stoping.Columns[2].HeaderText = "Workplace";
            Stoping.Columns[3].HeaderText = "Peg To Face";
            Stoping.Columns[4].HeaderText = "";
            Stoping.Columns[5].HeaderText = "";
            Stoping.Columns[6].HeaderText = "";
            Stoping.Columns[7].HeaderText = "";
            Stoping.Columns[8].HeaderText = "";
            Stoping.Columns[9].HeaderText = "";
            Stoping.Columns[10].HeaderText = "";
            Stoping.Columns[11].HeaderText = "";
            Stoping.Columns[12].HeaderText = "";
            Stoping.Columns[13].HeaderText = "";
            Stoping.Columns[14].HeaderText = "";
            Stoping.Columns[15].HeaderText = "";
            Stoping.Columns[16].HeaderText = "";
            Stoping.Columns[17].HeaderText = "";
            Stoping.Columns[18].HeaderText = "";
            Stoping.Columns[19].HeaderText = "";
            Stoping.Columns[20].HeaderText = "";
            Stoping.Columns[21].HeaderText = "";
            Stoping.Columns[22].HeaderText = "";
            Stoping.Columns[23].HeaderText = "";
            Stoping.Columns[24].HeaderText = "";
            Stoping.Columns[25].HeaderText = "";

            Stoping.Columns[26].HeaderText = "";
            Stoping.Columns[27].HeaderText = "";
            Stoping.Columns[28].HeaderText = "";
            Stoping.Columns[29].HeaderText = "";
            Stoping.Columns[30].HeaderText = "";
            Stoping.Columns[31].HeaderText = "";
            Stoping.Columns[32].HeaderText = "";
            Stoping.Columns[33].HeaderText = "";
            Stoping.Columns[34].HeaderText = "";
            Stoping.Columns[35].HeaderText = "";
            Stoping.Columns[36].HeaderText = "";
            Stoping.Columns[37].HeaderText = "";
            Stoping.Columns[38].HeaderText = "";
            Stoping.Columns[39].HeaderText = "";

            Stoping.Columns[40].HeaderText = "";
            Stoping.Columns[41].HeaderText = "";
            Stoping.Columns[42].HeaderText = "";
            Stoping.Columns[43].HeaderText = "";

            Stoping.Columns[44].HeaderText = "Prog. Plan";
            Stoping.Columns[45].HeaderText = "Prog. Book";
            Stoping.Columns[46].HeaderText = "Prog. Var";
            Stoping.Columns[47].HeaderText = "Mth. Call";
            Stoping.Columns[48].HeaderText = "Mth. F/C";
            Stoping.Columns[49].HeaderText = "MO. F/C";
            Stoping.Columns[50].HeaderText = "Daily Adv";


            string CellValue = "";
            string CellValueStart = "";
            string CellValueEnd = "";
            string CellValueB = "";
            string CellValueStartB = "";
            string CellValueEndB = "";

            Decimal redevprogplan = 0;
            Decimal redevprogbook = 0;
            Decimal redevmnth = 0;

            Decimal devTonsprogplan = 0;
            Decimal devTonsprogbook = 0;
            Decimal devTonsmnth = 0;


            foreach (DataRow r in Neil.Rows)
            {

                Span = Convert.ToDateTime(r["calendardate"].ToString()).Subtract(StartDate1.Value);

                col = Convert.ToInt32(Span.Days) + 4;


                if (Section == "4202" && month2 == "201701" && col > 35)
                    col = col - 12;

                if (month2 == "201901")
                {
                    if (Convert.ToDateTime(r["calendardate"].ToString()) > Convert.ToDateTime("29 Dec 2018"))
                    {
                        Span = Convert.ToDateTime(r["calendardate"].ToString()).Subtract(StartDate1.Value);
                        col = Convert.ToInt32(Span.Days) + 4 - 4;
                    }

                }

                Stoping.Rows[x].Cells[52].Value = "       ";
                Stoping.Rows[x - 1].Cells[52].Value = "";

                Stoping.Rows[x].Cells[53].Value = "";
                Stoping.Rows[x - 1].Cells[53].Value = "";

                if (WPLable != r["workplaceid"].ToString())
                {
                    // do sb
                    if (SBLabel != r["sbid"].ToString() + ":" + r["sbname"].ToString())
                    {
                        Stoping.Rows[x].Cells[0].Value = SBLabel;
                        Stoping.Rows[x].Cells[1].Value = "";
                        Stoping.Rows[x].Cells[2].Value = "";
                        Stoping.Rows[x].Cells[3].Value = "a";

                        Stoping.Rows[x - 1].Cells[0].Value = "";
                        Stoping.Rows[x - 1].Cells[1].Value = "";
                        Stoping.Rows[x - 1].Cells[2].Value = "";
                        Stoping.Rows[x - 1].Cells[3].Value = "a";

                        Stoping.Rows[x + 1].Cells[3].Value = "c";

                        Stoping.Rows[x].Cells[44].Value = "0";
                        Stoping.Rows[x].Cells[45].Value = "0";
                        Stoping.Rows[x].Cells[46].Value = "0";
                        Stoping.Rows[x].Cells[47].Value = "0";
                        Stoping.Rows[x].Cells[48].Value = "0";
                        Stoping.Rows[x].Cells[49].Value = "0";

                        Stoping.Rows[x - 1].Cells[44].Value = "";
                        Stoping.Rows[x - 1].Cells[45].Value = "";
                        Stoping.Rows[x - 1].Cells[46].Value = "";
                        Stoping.Rows[x - 1].Cells[47].Value = "";
                        Stoping.Rows[x - 1].Cells[48].Value = "";
                        Stoping.Rows[x - 1].Cells[49].Value = "";

                        for (int y = 4; y < 44; y++)
                        {
                            Stoping.Rows[x - 1].Cells[y].Value = "080 ";
                            Stoping.Rows[x].Cells[y].Value = "080 ";
                            try
                            {
                                if (Stoping.Rows[450].Cells[y].Value != null)
                                {


                                    Stoping.Rows[x].Cells[y].Value = "082" + Stoping.Rows[450].Cells[y].Value;


                                }
                                Stoping.Rows[450].Cells[y].Value = null;
                            }
                            catch { }
                        }

                        ///////New Colors dev///////
                        for (int y = 54; y < 96; y++)
                        {
                            Stoping.Rows[x - 1].Cells[y].Value = "080 ";
                            Stoping.Rows[x].Cells[y].Value = "080 ";
                            
                                if (Stoping.Rows[450].Cells[y].Value != null)
                                {


                                    Stoping.Rows[x].Cells[y].Value = "082" + Stoping.Rows[450].Cells[y].Value;


                                }
                                Stoping.Rows[450].Cells[y].Value = null;
                           
                        }

                        SBLabel = r["sbid"].ToString() + ":" + r["sbname"].ToString();
                        x = x + 2;

                    }

                    //Stoping.Rows[x].Cells[1].Value = r["orgunitds"].ToString();

                    if (r["OrgUnitDS"].ToString() != "")
                        Stoping.Rows[x].Cells[1].Value = r["OrgUnitDS"].ToString();
                    else
                        Stoping.Rows[x].Cells[1].Value = r["OrgUnitDS"].ToString();


                    string wp = " ";


                    Stoping.Rows[x - 1].Cells[51].Value = "";

                    Stoping.Rows[x].Cells[51].Value = wp;

                    Stoping.Rows[x].Cells[2].Value = r["description"].ToString();
                    Stoping.Rows[x].Cells[53].Value = r["newwpflag"].ToString();

                    Stoping.Rows[x].Cells[50].Value = r["adv"].ToString() + "";

                    Stoping.Rows[x].Cells[52].Value = r["workplaceid"].ToString();



                    //if ((r["CheckSqm"] != null) )// (Convert.ToInt32(r["CheckSqm"].ToString()) > 0) )
                    //{
                    //    if (Convert.ToDateTime(r["calendardate"].ToString()).ToString("ddd") == SysSettings.CheckMeas)
                    //      Stoping.Rows[x].Cells[49].Value = String.Format("{0:0.0}", Math.Round(Convert.ToDecimal(r["CheckSqm"].ToString()),1));
                    //}




                    Stoping.Rows[x].Cells[3].Value = r["fl"].ToString() + "   ";
                    if (x == 1)
                        Stoping.Rows[x - 1].Cells[3].Value = "c";
                    Stoping.Rows[x - 1].Cells[0].Value = "";
                    Stoping.Rows[x - 1].Cells[1].Value = "";
                    Stoping.Rows[x - 1].Cells[2].Value = "";
                    if (Stoping.Rows[x - 1].Cells[3].Value != "c")
                        Stoping.Rows[x - 1].Cells[3].Value = "";




                    Stoping.Rows[x].Cells[44].Value = "0";
                    Stoping.Rows[x].Cells[45].Value = "0";
                    Stoping.Rows[x].Cells[46].Value = "0";
                    Stoping.Rows[x].Cells[47].Value = "0";
                    Stoping.Rows[x].Cells[48].Value = "0";
                    Stoping.Rows[x].Cells[49].Value = "0";

                    Stoping.Rows[x - 1].Cells[44].Value = "";
                    Stoping.Rows[x - 1].Cells[45].Value = "";
                    Stoping.Rows[x - 1].Cells[46].Value = "";
                    Stoping.Rows[x - 1].Cells[47].Value = "";
                    Stoping.Rows[x - 1].Cells[48].Value = "";
                    Stoping.Rows[x - 1].Cells[49].Value = "";


                    //if ((r["CheckSqm"] != null))// (Convert.ToInt32(r["CheckSqm"].ToString()) > 0) )
                    //{
                    //    if (Convert.ToDateTime(r["calendardate"].ToString()).ToString("ddd") == SysSettings.CheckMeas)
                    //        Stoping.Rows[x].Cells[49].Value = String.Format("{0:0.0}", Math.Round(Convert.ToDecimal(r["CheckSqm"].ToString()), 1));
                    //}



                    for (int y = 4; y < 44; y++)
                    {
                        Stoping.Rows[x - 1].Cells[y].Value = "000 ";
                        Stoping.Rows[x].Cells[y].Value = "000 ";
                    }

                    for (int y = 54; y < 96; y++)
                    {
                        Stoping.Rows[x - 1].Cells[y].Value = "000 ";
                        Stoping.Rows[x].Cells[y].Value = "000 ";
                    }

                    //if (r["priority"].ToString() == "Y")
                    //{
                    //    Stoping[2, x].Style.Font = new Font("Microsoft Sans Serif", 6.75F, FontStyle.Bold);

                    //}

                    Stoping.Rows[x].Cells[44].Value = "0.0";
                    Stoping.Rows[x].Cells[45].Value = "0.0";
                    Stoping.Rows[x].Cells[46].Value = (Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)).ToString();



                    if ((Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)) < 0)
                    {
                        Stoping.Rows[x].Cells[46].Value = "1" + Stoping.Rows[x].Cells[46].Value;
                    }
                    else
                    {
                        Stoping.Rows[x].Cells[46].Value = "0" + Stoping.Rows[x].Cells[46].Value;
                    }

                    Stoping.Rows[x].Cells[47].Value = String.Format("{0:0.0}", Convert.ToDecimal(r["sqmtotal"].ToString()));
                    WPLable = r["workplaceid"].ToString();
                    x = x + 2;

                }



                if (x == 3)
                {
                    Stoping.Columns[col].HeaderText = Convert.ToDateTime(r["calendardate"].ToString()).ToString("dd MMM ddd");
                    Stoping.Columns[col].Visible = true;
                    Stoping.Text = r["shiftday"].ToString();
                    TotShiftLbl = r["shiftday"].ToString();
                    if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now)
                    {
                        if (Convert.ToInt32(CurrShftLbl) <= Convert.ToInt32(r["shiftday"].ToString()))
                            CurrShftLbl = r["shiftday"].ToString();
                    }
                }


                if ((r["CheckSqm"] != null))// (Convert.ToInt32(r["CheckSqm"].ToString()) > 0) )
                {
                    if (r["CheckSqm"].ToString() != "")
                    {
                        //if (r["bookadv"].ToString() != "")
                        //{
                        //    if (Convert.ToDateTime(r["calendardate"].ToString()).ToString("ddd") == SysSettings.CheckMeas)
                        Stoping.Rows[x].Cells[49].Value = 5;
                        //}
                    }
                }

                if (r["bookadv"].ToString() != "")
                {
                    if (Stoping.Rows[456].Cells[col].Value == null)
                        Stoping.Rows[456].Cells[col].Value = "0.0";

                    Stoping.Rows[456].Cells[col].Value = Convert.ToDecimal(Stoping.Rows[456].Cells[col].Value) + Math.Round(Convert.ToDecimal(r["booktons"].ToString()), 1);

                    if (r["endtt"].ToString().Trim() == "PNL" || r["endtt"].ToString().Trim() == "RRs")
                    {
                        if (Stoping.Rows[455].Cells[col].Value == null)
                            Stoping.Rows[455].Cells[col].Value = "0.0";

                        Stoping.Rows[455].Cells[col].Value = Convert.ToDecimal(Stoping.Rows[455].Cells[col].Value) + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1);

                        //redevprogplan = 0;
                        redevprogbook = redevprogbook + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1); ;
                        //float redevmnth = 0;
                    }
                }

                if (r["endtt"].ToString().Trim() == "PNL" || r["endtt"].ToString().Trim() == "RRs")
                {
                    redevmnth = redevmnth + Math.Round(Convert.ToDecimal(r["adv"].ToString()), 1);
                    if (Convert.ToDateTime(r["calendardate"].ToString()) < DateTime.Now)
                        redevprogplan = redevprogplan + Math.Round(Convert.ToDecimal(r["adv"].ToString()), 1);
                }

                //devTonsmnth = devTonsmnth + Math.Round(Convert.ToDecimal(r["tons"].ToString()), 1);

                if (Convert.ToDateTime(r["calendardate"].ToString()) < DateTime.Now)
                {
                    //devTonsprogplan = devTonsprogplan + Math.Round(Convert.ToDecimal(r["tons"].ToString()), 1);
                    if (r["bookadv"].ToString() != "")
                        devTonsprogbook = devTonsprogbook + Math.Round(Convert.ToDecimal(r["booktons"].ToString()), 1);

                }
                if (Convert.ToDateTime(r["calendardate"].ToString()) < DateTime.Now)
                    Stoping.Rows[x - 2].Cells[44].Value = (Math.Round(Convert.ToDecimal(Stoping.Rows[x - 2].Cells[44].Value), 1) + Math.Round(Convert.ToDecimal(r["adv"].ToString()), 1));

                if ((r["CheckSqm"] != DBNull.Value))// (Convert.ToInt32(r["CheckSqm"].ToString()) > 0) )
                {

                    if ((r["CheckSqm"].ToString() != null))
                        Stoping.Rows[x - 2].Cells[49].Value = String.Format("{0:0.0}", Math.Round(Convert.ToDecimal(r["CheckSqm"].ToString()), 1));
                }


                if (r["bookadv"].ToString() != "")
                {
                    if (Convert.ToDateTime(r["calendardate"].ToString()) < DateTime.Now)
                    {
                        if (Stoping.Rows[450].Cells[col].Value == null)
                            Stoping.Rows[450].Cells[col].Value = "0.0";
                        Stoping.Rows[450].Cells[col].Value = (Math.Round(Convert.ToDecimal(Stoping.Rows[450].Cells[col].Value), 1) + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1));

                        if (Stoping.Rows[451].Cells[col].Value == null)
                            Stoping.Rows[451].Cells[col].Value = "0.0";
                        Stoping.Rows[451].Cells[col].Value = (Math.Round(Convert.ToDecimal(Stoping.Rows[451].Cells[col].Value), 1) + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1));
                    }
                    if (Convert.ToDateTime(r["calendardate"].ToString()) < DateTime.Now)
                        Stoping.Rows[x - 2].Cells[45].Value = (Math.Round(Convert.ToDecimal(Stoping.Rows[x - 2].Cells[45].Value), 1) + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1));
                    //Stoping.Rows[x - 2].Cells[44].Value = (Math.Round(Convert.ToDecimal(Stoping.Rows[x - 2].Cells[44].Value)) + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1));

                    Stoping.Rows[x - 2].Cells[3].Value = r["pegid"].ToString();


                    if ((r["CheckSqm"] != null))// (Convert.ToInt32(r["CheckSqm"].ToString()) > 0) )
                    {
                        if (r["CheckSqm"].ToString() != "")
                        {
                            if (r["bookadv"].ToString() != "")
                            {
                                // if (Convert.ToDateTime(r["calendardate"].ToString()).ToString("ddd") == SysSettings.CheckMeas)
                                Stoping.Rows[x - 2].Cells[49].Value = String.Format("{0:0.0}", Math.Round(Convert.ToDecimal(r["CheckSqm"].ToString()), 1));
                            }
                        }
                    }

                    if (r["adv"] != DBNull.Value)
                        Stoping.Rows[x].Cells[51].Value = r["adv"].ToString();
                    else
                        Stoping.Rows[x].Cells[51].Value = "";



                    if (r["workingday"].ToString() == "Y")
                    {
                        if (r["bookprob"].ToString() != "")
                        {
                            if (Convert.ToDecimal(r["bookadv"].ToString()) == Convert.ToDecimal("0.00"))
                            {
                                Stoping.Rows[x - 2].Cells[col].Value = "101" + r["bookprob"].ToString();
                            }
                            else
                            {
                                Stoping.Rows[x - 2].Cells[col].Value = "102" + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1);
                            }
                        }
                        else
                        {
                            Stoping.Rows[x - 2].Cells[col].Value = "002" + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1);
                            if (Convert.ToDecimal(r["bookadv"].ToString()) == Convert.ToDecimal("0.00"))
                            {
                                if (r["bookcode"].ToString() != "PR")
                                    Stoping.Rows[x - 2].Cells[col].Value = "203" + r["bookcode"].ToString();
                            }

                        }
                    }


                    ////////New Colors Dev//////////////
                    if (col + 50 >= 54 && col + 50 <= 94)
                    {
                        if (r["workingday"].ToString() == "Y")
                        {
                            if (r["bookprob"].ToString() != "")
                            {
                                if (Convert.ToDecimal(r["bookadv"].ToString()) == Convert.ToDecimal("0.00"))
                                {
                                    Stoping.Rows[x - 2].Cells[col + 50].Value = "101" + r["bookprob"].ToString();
                                    Stoping.Rows[x - 3].Cells[col + 50].Value = "101" + r["bookprob"].ToString();
                                }
                                else
                                {
                                    Stoping.Rows[x - 2].Cells[col + 50].Value = "102" + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1);
                                    Stoping.Rows[x - 3].Cells[col + 50].Value = "102" + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1);
                                }
                            }
                            else
                            {
                                Stoping.Rows[x - 2].Cells[col + 50].Value = "002" + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1);
                                Stoping.Rows[x - 3].Cells[col + 50].Value = "002" + Math.Round(Convert.ToDecimal(r["bookadv"].ToString()), 1);
                                if (Convert.ToDecimal(r["bookadv"].ToString()) == Convert.ToDecimal("0.00"))
                                {
                                    if (r["bookcode"].ToString() != "PR")
                                    {
                                        Stoping.Rows[x - 2].Cells[col + 50].Value = "203" + r["bookcode"].ToString();
                                        Stoping.Rows[x - 3].Cells[col + 50].Value = "203" + r["bookcode"].ToString();
                                    }
                                }

                            }
                        }
                    }


                    CellValue = Stoping.Rows[x - 2].Cells[col].Value.ToString();
                    CellValueB = Stoping.Rows[x - 3].Cells[col].Value.ToString();

                    //CellValueB = CellValueB + "          ";

                    //do abs
                    if (ABSBtn.Text == "Remove Colours")
                    {
                        if (r["ABSCode"].ToString() == "Safe")
                        {
                            CellValueStart = CellValue.Substring(0, 1);
                            CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                            Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "3" + CellValueEnd;


                            CellValueStartB = CellValueB.Substring(0, 1);
                            CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                            Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "3" + CellValueEndB;

                        }
                        if (r["ABSCode"].ToString() == "Unsafe")
                        {
                            CellValueStart = CellValue.Substring(0, 1);
                            CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                            Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "6" + CellValueEnd;

                            CellValueStartB = CellValueB.Substring(0, 1);
                            CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                            Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "6" + CellValueEndB;

                        }

                        if (r["ABSCode"].ToString() == "No Vis.")
                        {
                            CellValueStart = CellValue.Substring(0, 1);
                            CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                            Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "9" + CellValueEnd;

                            CellValueStartB = CellValueB.Substring(0, 1);
                            CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                            Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "9" + CellValueEndB;
                        }
                    }

                }
                else
                {

                    if (r["workingday"].ToString() == "N")
                    {
                        
                            Stoping.Rows[x - 3].Cells[col].Value = "010 ";
                            Stoping.Rows[x - 2].Cells[col].Value = "010 ";
                            if (r["bookprob"].ToString() != "")
                            {
                                Stoping.Rows[x - 3].Cells[col].Value = "121" + r["bookprob"].ToString();
                            }
                        

                    }
                    else
                    {
                        if (r["bookprob"].ToString() != "")
                        {
                            Stoping.Rows[x - 2].Cells[col].Value = "101" + r["bookprob"].ToString();
                        }
                    }

                    ///////////New Colors Dev////////////////
                    if (col + 50 >= 54 && col + 50 <= 94)
                    {
                        if (r["workingday"].ToString() == "N")
                        {
                            Stoping.Rows[x - 3].Cells[col + 50].Value = "010 ";
                            Stoping.Rows[x - 2].Cells[col + 50].Value = "010 ";
                            if (r["bookprob"].ToString() != "")
                            {
                                Stoping.Rows[x - 2].Cells[col + 50].Value = "121" + r["bookprob"].ToString();
                                Stoping.Rows[x - 3].Cells[col + 50].Value = "121" + r["bookprob"].ToString();
                            }
                        }
                        else
                        {
                            if (r["bookprob"].ToString() != "")
                            {
                                Stoping.Rows[x - 2].Cells[col + 50].Value = "101" + r["bookprob"].ToString();
                                Stoping.Rows[x - 3].Cells[col + 50].Value = "101" + r["bookprob"].ToString();
                            }
                        }
                    }

                }

            }

            Stoping.Rows[x].Cells[0].Value = SBLabel;
            Stoping.Rows[x].Cells[1].Value = "";
            Stoping.Rows[x].Cells[2].Value = "";
            Stoping.Rows[x].Cells[3].Value = "a";

            Stoping.Rows[x - 1].Cells[1].Value = "";
            Stoping.Rows[x - 1].Cells[2].Value = "";
            Stoping.Rows[x - 1].Cells[3].Value = "a";

            Stoping.Rows[x - 1].Cells[44].Value = "";
            Stoping.Rows[x - 1].Cells[45].Value = "";
            Stoping.Rows[x - 1].Cells[46].Value = "";
            Stoping.Rows[x - 1].Cells[47].Value = "";
            Stoping.Rows[x - 1].Cells[48].Value = "";
            Stoping.Rows[x - 1].Cells[49].Value = "";

            for (int y = 4; y < 44; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "080 ";
                Stoping.Rows[x].Cells[y].Value = "080 ";
                if (Stoping.Rows[450].Cells[y].Value != null)
                {
                    Stoping.Rows[x].Cells[y].Value = "082" + Stoping.Rows[450].Cells[y].Value;
                }
            }

            ///////New Colors Dev/////////////
            for (int y = 54; y < 96; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "080 ";
                Stoping.Rows[x].Cells[y].Value = "080 ";
                if (Stoping.Rows[450].Cells[y].Value != null)
                {
                    Stoping.Rows[x].Cells[y].Value = "082" + Stoping.Rows[450].Cells[y].Value;
                }
            }

            x = x + 2;

            Stoping.Rows[x].Cells[0].Value = "Total m";
            Stoping.Rows[x].Cells[1].Value = "";
            Stoping.Rows[x].Cells[2].Value = "";
            Stoping.Rows[x].Cells[3].Value = "b";

            Stoping.Rows[x - 1].Cells[1].Value = "";
            Stoping.Rows[x - 1].Cells[2].Value = "";
            Stoping.Rows[x - 1].Cells[3].Value = "b";

            Stoping.Rows[x - 1].Cells[44].Value = "";
            Stoping.Rows[x - 1].Cells[45].Value = "";
            Stoping.Rows[x - 1].Cells[46].Value = "";
            Stoping.Rows[x - 1].Cells[47].Value = "";
            Stoping.Rows[x - 1].Cells[48].Value = "";
            Stoping.Rows[x - 1].Cells[49].Value = "";
            ////Maybe
            Stoping.Rows[x - 1].Cells[52].Value = "                      ";



            for (int y = 4; y < 44; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "050 ";
                Stoping.Rows[x].Cells[y].Value = "050 ";


                if (Stoping.Rows[451].Cells[y].Value != null)
                {
                    Stoping.Rows[x].Cells[y].Value = "052" + Stoping.Rows[451].Cells[y].Value;

                    Stoping.Rows[x + 2].Cells[y].Value = "052" + Stoping.Rows[451].Cells[y].Value;
                }
            }

            /////////New Colors Dev//////////
            for (int y = 54; y < 96; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "050 ";
                Stoping.Rows[x].Cells[y].Value = "050 ";

                if (Stoping.Rows[451].Cells[y].Value != null)
                {
                    Stoping.Rows[x].Cells[y].Value = "052" + Stoping.Rows[451].Cells[y].Value;

                    Stoping.Rows[x + 2].Cells[y].Value = "052" + Stoping.Rows[451].Cells[y].Value;
                }
            }


            Decimal progplan = 0;
            Decimal progplanTot = 0;
            Decimal progbook = 0;
            Decimal progbookTot = 0;
            Decimal MonthCall = 0;
            Decimal MonthCallTot = 0;

            Decimal mofc1 = 0;
            Decimal mofc1tot = 0;

            Decimal FC = 0;
            Decimal FCTot = 0;

            Decimal PlanAdv = 0;
            Decimal PlanAdvTot = 0;

            for (int y = 0; y < x; y++)
            {
                Stoping.Rows[y].Height = 13;

                //do forcast
                if (Stoping.Rows[y].Cells[45].Value != null)
                {
                    if (Stoping.Rows[y].Cells[45].Value.ToString() != "")
                    {
                        //Stoping.Rows[y].Cells[48].Value = (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) / Convert.ToDecimal(CurrShftLbl.Text) * Convert.ToDecimal(TotShiftLbl.Text)).ToString("0.00");
                        Stoping.Rows[y].Cells[48].Value = String.Format("{0:0.0}", (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) / Convert.ToDecimal(CurrShftLbl) * Convert.ToDecimal(TotShiftLbl)));

                        Stoping.Rows[y].Cells[48].Value = Math.Round((Convert.ToDecimal(Stoping.Rows[y].Cells[48].Value)), 1);

                        Stoping.Rows[y].Cells[46].Value = (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[y].Cells[44].Value));

                        if ((Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[y].Cells[44].Value)) < 0)
                        {
                            Stoping.Rows[y].Cells[46].Value = "1" + Stoping.Rows[y].Cells[46].Value;
                        }
                        else
                        {
                            Stoping.Rows[y].Cells[46].Value = "0" + Stoping.Rows[y].Cells[46].Value;

                        }

                        if (Stoping.Rows[y].Cells[50].Value.ToString() != "")
                        {
                            Stoping.Rows[y].Cells[50].Value = Math.Round((Convert.ToDecimal(Stoping.Rows[y].Cells[50].Value)), 1);
                            PlanAdv = PlanAdv + Math.Round((Convert.ToDecimal(Stoping.Rows[y].Cells[50].Value)), 1);
                            PlanAdvTot = PlanAdvTot + Math.Round((Convert.ToDecimal(Stoping.Rows[y].Cells[50].Value)), 1);
                        }


                        progplan = progplan + Convert.ToDecimal(Stoping.Rows[y].Cells[44].Value);
                        progplanTot = progplanTot + Convert.ToDecimal(Stoping.Rows[y].Cells[44].Value);

                        progbook = progbook + Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value);
                        progbookTot = progbookTot + Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value);

                        MonthCall = MonthCall + Convert.ToDecimal(Stoping.Rows[y].Cells[47].Value);
                        MonthCallTot = MonthCallTot + Convert.ToDecimal(Stoping.Rows[y].Cells[47].Value);

                        FC = FC + Convert.ToDecimal(Stoping.Rows[y].Cells[48].Value);
                        FCTot = FCTot + Convert.ToDecimal(Stoping.Rows[y].Cells[48].Value);
                        mofc1 = mofc1 + Convert.ToDecimal(Stoping.Rows[y].Cells[49].Value);
                        mofc1tot = mofc1tot + Convert.ToDecimal(Stoping.Rows[y].Cells[49].Value);
                        
                        //





                    }
                }
                if (Stoping.Rows[y].Cells[0].Value != null)
                {
                    if (Stoping.Rows[y].Cells[0].Value.ToString() != "")
                    {
                        Stoping.Rows[y].Cells[44].Value = progplan.ToString();
                        Stoping.Rows[y].Cells[45].Value = progbook.ToString();
                        Stoping.Rows[y].Cells[46].Value = (progbook - progplan).ToString();
                        Stoping.Rows[y].Cells[47].Value = MonthCall.ToString();
                        Stoping.Rows[y].Cells[48].Value = FC.ToString();
                        Stoping.Rows[y].Cells[49].Value = mofc1.ToString();
                        Stoping.Rows[y].Cells[50].Value = PlanAdv.ToString();

                        if ((Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[y].Cells[44].Value)) < 0)
                        {
                            Stoping.Rows[y].Cells[46].Value = "1" + Stoping.Rows[y].Cells[46].Value;
                        }
                        else
                        {
                            Stoping.Rows[y].Cells[46].Value = "0" + Stoping.Rows[y].Cells[46].Value;

                        }
                        //Stoping.Rows[y].Cells[50].Value = PlanAdv.ToString();
                       
                        progplan = 0;
                        progbook = 0;
                        MonthCall = 0;
                        FC = 0;
                        mofc1 = 0;
                        PlanAdv = 0;
                    }
                }

                //if (Stoping.Rows[y].Cells[50].Value.ToString() != "")
                //{

                //    PlanAdv = PlanAdv + Convert.ToDecimal(String.Format("{0:0.0}", (Convert.ToDecimal(Stoping.Rows[y].Cells[50].Value))));
                //    Stoping.Rows[y].Cells[50].Value = Math.Round((Convert.ToDecimal(PlanAdv)), 1);
                //    PlanAdv = 0;
                //}

            }

            Stoping.Rows[x - 1].Cells[44].Value = "";
            Stoping.Rows[x - 1].Cells[45].Value = "";
            Stoping.Rows[x - 1].Cells[46].Value = "";
            Stoping.Rows[x - 1].Cells[47].Value = "";
            Stoping.Rows[x - 1].Cells[48].Value = "";
            Stoping.Rows[x - 1].Cells[49].Value = "";
           // Stoping.Rows[x - 1].Cells[50].Value = "";

            Stoping.Rows[x].Cells[44].Value = progplanTot.ToString();
            Stoping.Rows[x].Cells[45].Value = progbookTot.ToString();
            Stoping.Rows[x].Cells[46].Value = (progbookTot - progplanTot).ToString();
            Stoping.Rows[x].Cells[47].Value = MonthCallTot.ToString();
            Stoping.Rows[x].Cells[48].Value = FCTot.ToString();
            Stoping.Rows[x].Cells[49].Value = mofc1tot.ToString();
            Stoping.Rows[x].Cells[50].Value = PlanAdv.ToString();

            if ((Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)) < 0)
            {
                Stoping.Rows[x].Cells[46].Value = "1" + Stoping.Rows[x].Cells[46].Value;
            }
            else
            {
                Stoping.Rows[x].Cells[46].Value = "0" + Stoping.Rows[x].Cells[46].Value;

            }

            x = x + 2;

            Stoping.Rows[x].Cells[0].Value = "Daily Booked M";
            Stoping.Rows[x].Cells[1].Value = "";
            Stoping.Rows[x].Cells[2].Value = "";
            Stoping.Rows[x].Cells[3].Value = "";

            Stoping.Rows[x - 1].Cells[1].Value = "";
            Stoping.Rows[x - 1].Cells[2].Value = "";
            Stoping.Rows[x - 1].Cells[3].Value = "";

            Stoping.Rows[x - 1].Cells[44].Value = "";
            Stoping.Rows[x - 1].Cells[45].Value = "";
            Stoping.Rows[x - 1].Cells[46].Value = "";
            Stoping.Rows[x - 1].Cells[47].Value = "";
            Stoping.Rows[x - 1].Cells[48].Value = "";
            Stoping.Rows[x - 1].Cells[49].Value = "";
           // Stoping.Rows[x - 1].Cells[50].Value = "";

            Stoping.Rows[x].Cells[44].Value = "";
            Stoping.Rows[x].Cells[45].Value = "";
            Stoping.Rows[x].Cells[46].Value = "";
            Stoping.Rows[x].Cells[47].Value = "";
            Stoping.Rows[x].Cells[48].Value = "";
            Stoping.Rows[x].Cells[49].Value = "";
            //Stoping.Rows[x].Cells[50].Value = "";

            decimal prog = 0;
            decimal booked = 0;

            decimal DayPlan = 0;
            decimal ProgDayPlan = 0;

            for (int y = 4; y < 44; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "000 ";
                Stoping.Rows[x].Cells[y].Value = "000 ";




                if (Stoping.Rows[451].Cells[y].Value != null)
                {
                    Stoping.Rows[x].Cells[y].Value = "002" + Stoping.Rows[451].Cells[y].Value;
                    //booked = Convert.ToDecimal(Stoping.Rows[451].Cells[y].Value);
                    //prog = prog + booked;

                    //Stoping.Rows[x + 2].Cells[y].Value = "002" + Math.Round(Convert.ToDecimal(prog), 0);
                }
            }

            ////////New Colors Dev///////////
            for (int y = 54; y < 96; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "000 ";
                Stoping.Rows[x].Cells[y].Value = "000 ";

                if (Stoping.Rows[451].Cells[y].Value != null)
                {
                    Stoping.Rows[x-1].Cells[y].Value = "002" + Stoping.Rows[451].Cells[y].Value;
                    Stoping.Rows[x].Cells[y].Value = "002" + Stoping.Rows[451].Cells[y].Value;
                    //booked = Convert.ToDecimal(Stoping.Rows[451].Cells[y].Value);
                    //prog = prog + booked;

                    //Stoping.Rows[x + 2].Cells[y].Value = "002" + Math.Round(Convert.ToDecimal(prog), 0);
                }
            }


            Stoping.Rows[x].Cells[44].Value = "";//redevprogplan.ToString();
            Stoping.Rows[x].Cells[45].Value = "";//redevprogbook.ToString();
            Stoping.Rows[x].Cells[46].Value = "";//(redevprogbook - redevprogplan).ToString();
            Stoping.Rows[x].Cells[47].Value = "";//redevmnth.ToString();
            Stoping.Rows[x].Cells[48].Value = "";
            Stoping.Rows[x].Cells[49].Value = "";
            //Stoping.Rows[x].Cells[50].Value = "";

            //if ((Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)) < 0)
            //{
            //    //Stoping.Rows[x].Cells[46].Value = "1" + Stoping.Rows[x].Cells[46].Value;
            //}
            //else
            //{
            //    //Stoping.Rows[x].Cells[46].Value = "0" + Stoping.Rows[x].Cells[46].Value;

            //}

            // Stoping.Rows[x].Cells[48].Value = (Convert.ToDecimal(redevprogbook) / Convert.ToDecimal(CurrShftLbl.Text) * Convert.ToDecimal(TotShiftLbl.Text)).ToString("0.00");


            x = x + 2;

            Stoping.Rows[x].Cells[0].Value = "Prog Booked M";
            Stoping.Rows[x].Cells[1].Value = "";
            Stoping.Rows[x].Cells[2].Value = "";
            Stoping.Rows[x].Cells[3].Value = "";

            Stoping.Rows[x - 1].Cells[1].Value = "";
            Stoping.Rows[x - 1].Cells[2].Value = "";
            Stoping.Rows[x - 1].Cells[3].Value = "";

            Stoping.Rows[x - 1].Cells[44].Value = "";
            Stoping.Rows[x - 1].Cells[45].Value = "";
            Stoping.Rows[x - 1].Cells[46].Value = "";
            Stoping.Rows[x - 1].Cells[47].Value = "";
            Stoping.Rows[x - 1].Cells[48].Value = "";
            Stoping.Rows[x - 1].Cells[49].Value = "";
            //Stoping.Rows[x - 1].Cells[50].Value = "";

            Stoping.Rows[x].Cells[44].Value = "";
            Stoping.Rows[x].Cells[45].Value = "";
            Stoping.Rows[x].Cells[46].Value = "";
            Stoping.Rows[x].Cells[47].Value = "";
            Stoping.Rows[x].Cells[48].Value = "";
            Stoping.Rows[x].Cells[49].Value = "";
            //Stoping.Rows[x].Cells[50].Value = "";



            for (int y = 4; y < 44; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "000 ";
                Stoping.Rows[x].Cells[y].Value = "000 ";


                if (Stoping.Rows[451].Cells[y].Value != null)
                {
                    booked = Convert.ToDecimal(Stoping.Rows[451].Cells[y].Value);
                    prog = prog + booked;

                    Stoping.Rows[x].Cells[y].Value = "002" + Math.Round(Convert.ToDecimal(prog), 1);
                }
            }


            ///////New Colors Dev/////////
            for (int y = 54; y < 96; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "000 ";
                Stoping.Rows[x].Cells[y].Value = "000 ";


                if (Stoping.Rows[451].Cells[y].Value != null)
                {
                    booked = Convert.ToDecimal(Stoping.Rows[451].Cells[y].Value);
                    prog = prog + booked;

                    Stoping.Rows[x].Cells[y].Value = "002" + Math.Round(Convert.ToDecimal(prog), 1);
                }
            }


            


            for (int y = 0; y < x + 1; y++)
            {

                if (Stoping.Rows[y].Cells[50].Value != "" && Stoping.Rows[y].Cells[50].Value != "0")
                {
                    DayPlan = Convert.ToDecimal(Convert.ToDecimal(Stoping.Rows[y].Cells[50].Value));
                    ProgDayPlan = ProgDayPlan + DayPlan;
                    Stoping.Rows[y].Cells[50].Value = (DayPlan).ToString();
                    //Stoping.Rows[y + 8].Cells[50].Value = (Convert.ToDecimal(ProgDayPlan).ToString());
                    Stoping.Rows[y].Cells[51].Value = (ProgDayPlan).ToString();
                    
                }
                if (y == x)
                {
                    Stoping.Rows[x - 4].Cells[50].Value =  (PlanAdvTot).ToString();
                    Stoping.Rows[x - 4].Cells[51].Value =  (PlanAdvTot).ToString();
                }



            }
           // Stoping.Rows[x+8].Cells[50].Value = (ProgDayPlan).ToString();
            Stoping.Rows[x].Cells[51].Value = (ProgDayPlan).ToString();
            //Stoping.Rows[351].Cells[50].Value = Math.Round(ProgDayPlan, 1);


            Stoping.Rows[x].Cells[44].Value = "";// Math.Round(devTonsprogplan, 0);
            Stoping.Rows[x].Cells[45].Value = "";//Math.Round(devTonsprogbook,0);
            Stoping.Rows[x].Cells[46].Value = "";//Math.Round(devTonsprogbook - devTonsprogplan,0).ToString();
            Stoping.Rows[x].Cells[47].Value = "";//Math.Round(devTonsmnth, 0);
            Stoping.Rows[x].Cells[48].Value = "";
            Stoping.Rows[x].Cells[49].Value = "";

            //if ((Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)) < 0)
            //{
            //    //Stoping.Rows[x].Cells[46].Value = "1" + Stoping.Rows[x].Cells[46].Value;
            //}
            //else
            //{
            //    //Stoping.Rows[x].Cells[46].Value = "0" + Stoping.Rows[x].Cells[46].Value;

            //}

            // Stoping.Rows[x].Cells[48].Value = (Convert.ToDecimal(devTonsprogbook) / Convert.ToDecimal(CurrShftLbl.Text) * Convert.ToDecimal(TotShiftLbl.Text)).ToString("0.00");

            Stoping.RowCount = x + 5;

            Report theReport = new Report();

            MWDataManager.clsDataAccess _dbManMOSec = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbManMOSec.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManMOSec.SqlStatement = " select Distinct SectionID_2+':'+Name_2 MOSec from Section_Complete " +
                                " Where SectionID_2 = '" + Section + "' " +
                                " and prodmonth = '" + month2 + "'" +

                                " ";
            _dbManMOSec.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManMOSec.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManMOSec.ResultsTableName = "MODaily_Stoping_Headings";
            _dbManMOSec.ExecuteInstruction();

            string MOSec = _dbManMOSec.ResultsDataTable.Rows[0][0].ToString();

            Color color = Color.DarkGray;           
           

            MWDataManager.clsDataAccess _dbManStopingHeading = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbManStopingHeading.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManStopingHeading.SqlStatement = "select (select a_Color from SysSet) a, (select s_Color from SysSet) b, '"+color.ToArgb() + "' t "+
                                ", (select b_Color from SysSet) s , " +
                                "'" + clsUserInfo.UserID.ToString() + "' userid,'" + MOSec + "' section,'" + SysSettings.Banner + "' banner,  '" + Stoping.Columns[1].HeaderText.ToString() + "' orgunit ," +
                                "'" + Stoping.Columns[3].HeaderText.ToString() + "' FL,'" + Stoping.Columns[4].HeaderText.ToString() + "' Col1, " +
                                "'" + Stoping.Columns[5].HeaderText.ToString() + "' Col2, '" + Stoping.Columns[6].HeaderText.ToString() + "' Col3,'" + Stoping.Columns[7].HeaderText.ToString() + "' Col4, " +
                                "'" + Stoping.Columns[8].HeaderText.ToString() + "' Col5, '" + Stoping.Columns[9].HeaderText.ToString() + "' Col6,'" + Stoping.Columns[10].HeaderText.ToString() + "' Col7, " +
                                "'" + Stoping.Columns[11].HeaderText.ToString() + "' Col8, '" + Stoping.Columns[12].HeaderText.ToString() + "' Col9,'" + Stoping.Columns[13].HeaderText.ToString() + "' Col10, " +
                                "'" + Stoping.Columns[14].HeaderText.ToString() + "' Col11, '" + Stoping.Columns[15].HeaderText.ToString() + "' Col12,'" + Stoping.Columns[16].HeaderText.ToString() + "' Col13, " +
                                "'" + Stoping.Columns[17].HeaderText.ToString() + "' Col14, '" + Stoping.Columns[18].HeaderText.ToString() + "' Col15,'" + Stoping.Columns[19].HeaderText.ToString() + "' Col16, " +
                                "'" + Stoping.Columns[20].HeaderText.ToString() + "' Col17, '" + Stoping.Columns[21].HeaderText.ToString() + "' Col18,'" + Stoping.Columns[22].HeaderText.ToString() + "' Col19, " +
                                "'" + Stoping.Columns[23].HeaderText.ToString() + "' Col20, '" + Stoping.Columns[24].HeaderText.ToString() + "' Col21,'" + Stoping.Columns[25].HeaderText.ToString() + "' Col22, " +
                                "'" + Stoping.Columns[26].HeaderText.ToString() + "' Col23, '" + Stoping.Columns[27].HeaderText.ToString() + "' Col24,'" + Stoping.Columns[28].HeaderText.ToString() + "' Col25, " +
                                "'" + Stoping.Columns[29].HeaderText.ToString() + "' Col26, '" + Stoping.Columns[30].HeaderText.ToString() + "' Col27,'" + Stoping.Columns[31].HeaderText.ToString() + "' Col28, " +
                                "'" + Stoping.Columns[32].HeaderText.ToString() + "' Col29, '" + Stoping.Columns[33].HeaderText.ToString() + "' Col30,'" + Stoping.Columns[34].HeaderText.ToString() + "' Col31, " +
                                "'" + Stoping.Columns[35].HeaderText.ToString() + "' Col32, '" + Stoping.Columns[36].HeaderText.ToString() + "' Col33,'" + Stoping.Columns[37].HeaderText.ToString() + "' Col34, " +
                                "'" + Stoping.Columns[38].HeaderText.ToString() + "' Col35, '" + Stoping.Columns[39].HeaderText.ToString() + "' Col36,'" + Stoping.Columns[40].HeaderText.ToString() + "' Col37, " +
                                "'" + Stoping.Columns[41].HeaderText.ToString() + "' Col38,'" + Stoping.Columns[42].HeaderText.ToString() + "' Col39,'" + Stoping.Columns[43].HeaderText.ToString() + "' Col40,'" + Stoping.Columns[44].HeaderText.ToString() + "' ProgPlan, '" + Stoping.Columns[45].HeaderText.ToString() + "' ProgBook, " +
                                "'" + Stoping.Columns[47].HeaderText.ToString() + "' MonthCall, '" + Stoping.Columns[48].HeaderText.ToString() + "' MonthFC,'" + Stoping.Columns[49].HeaderText.ToString() + "' MOFC, " +
                                "'" + Stoping.Columns[46].HeaderText.ToString() + "' Spare, '" + month2 + "' Spare1,'" + reportSettings.SectionID + "' Colour,'" + Stoping.Columns[2].HeaderText.ToString() + "' Workplace, '" + "Plan Adv" + "' RiskRating ";
            _dbManStopingHeading.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManStopingHeading.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManStopingHeading.ResultsTableName = "MODaily_Stoping_Headings";
            _dbManStopingHeading.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbManStopingData = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbManStopingData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            string wp1 = "";
            string flag1 = "";

            for (int y = 0; y < Stoping.RowCount - 3; y++)
            {
                if (Stoping.Rows[y].Cells[1].Value != null)
                {
                    wp1 = "               ";
                    flag1 = "";
                    if (y > 0)
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "Union ";

                    if (Stoping.Rows[y].Cells[52].Value != null)
                        wp1 = Stoping.Rows[y].Cells[52].Value.ToString();
                    if (Stoping.Rows[y].Cells[53].Value != null)
                        flag1 = Stoping.Rows[y].Cells[53].Value.ToString();




                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "Select '" + Stoping.Rows[y].Cells[51].Value.ToString() + "' Userid," + y + " Line,'" + Stoping.Rows[y].Cells[0].Value + "' Section,  '" + Stoping.Rows[y].Cells[1].Value + "' Orgunit, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + " '" + Stoping.Rows[y].Cells[3].Value.ToString() + "' FL,'" + Stoping.Rows[y].Cells[4].Value.ToString() + "' Col1, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[5].Value.ToString() + "' Col2, '" + Stoping.Rows[y].Cells[6].Value.ToString() + "' Col3,'" + Stoping.Rows[y].Cells[7].Value.ToString() + "' Col4, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[8].Value.ToString() + "' Col5, '" + Stoping.Rows[y].Cells[9].Value.ToString() + "' Col6,'" + Stoping.Rows[y].Cells[10].Value.ToString() + "' Col7, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[11].Value.ToString() + "' Col8, '" + Stoping.Rows[y].Cells[12].Value.ToString() + "' Col9,'" + Stoping.Rows[y].Cells[13].Value.ToString() + "' Col10, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[14].Value.ToString() + "' Col11, '" + Stoping.Rows[y].Cells[15].Value.ToString() + "' Col12,'" + Stoping.Rows[y].Cells[16].Value.ToString() + "' Col13, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[17].Value.ToString() + "' Col14, '" + Stoping.Rows[y].Cells[18].Value.ToString() + "' Col15,'" + Stoping.Rows[y].Cells[19].Value.ToString() + "' Col16, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[20].Value.ToString() + "' Col17, '" + Stoping.Rows[y].Cells[21].Value.ToString() + "' Col18,'" + Stoping.Rows[y].Cells[22].Value.ToString() + "' Col19, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[23].Value.ToString() + "' Col20, '" + Stoping.Rows[y].Cells[24].Value.ToString() + "' Col21,'" + Stoping.Rows[y].Cells[25].Value.ToString() + "' Col22, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[26].Value.ToString() + "' Col23, '" + Stoping.Rows[y].Cells[27].Value.ToString() + "' Col24,'" + Stoping.Rows[y].Cells[28].Value.ToString() + "' Col25, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[29].Value.ToString() + "' Col26, '" + Stoping.Rows[y].Cells[30].Value.ToString() + "' Col27,'" + Stoping.Rows[y].Cells[31].Value.ToString() + "' Col28, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[32].Value.ToString() + "' Col29, '" + Stoping.Rows[y].Cells[33].Value.ToString() + "' Col30,'" + Stoping.Rows[y].Cells[34].Value.ToString() + "' Col31, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[35].Value.ToString() + "' Col32, '" + Stoping.Rows[y].Cells[36].Value.ToString() + "' Col33,'" + Stoping.Rows[y].Cells[37].Value.ToString() + "' Col34, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[38].Value.ToString() + "' Col35, '" + Stoping.Rows[y].Cells[39].Value.ToString() + "' Col36,'" + Stoping.Rows[y].Cells[40].Value + "' Col37, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[41].Value.ToString() + "' Col38, '" + Stoping.Rows[y].Cells[42].Value.ToString() + "' Col39,'" + Stoping.Rows[y].Cells[43].Value.ToString() + "' Col40, '" + Stoping.Rows[y].Cells[44].Value.ToString() + "' ProgPlan, '" + Stoping.Rows[y].Cells[45].Value.ToString() + "' ProgBook, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[47].Value.ToString() + "' MonthCall, '" + Stoping.Rows[y].Cells[48].Value.ToString() + "' MonthFC,'" + Stoping.Rows[y].Cells[49].Value.ToString() + "' MOFC, ";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[46].Value.ToString() + "' Spare,'D' Spare1,'" + wp1 + "'+'Kopanang Mine' Colour,'" + Stoping.Rows[y].Cells[2].Value.ToString() + "' Workplace, '" + Stoping.Rows[y].Cells[50].Value.ToString() + "' RiskRating, '" + Stoping.Rows[y].Cells[50].Value.ToString() + "' newFC, '" + flag1 + "' newwp ";


                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "    ,'" + Stoping.Rows[y].Cells[54].Value.ToString() + "' Color1, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[55].Value.ToString() + "' Color2, '" + Stoping.Rows[y].Cells[56].Value.ToString() + "' Color3,'" + Stoping.Rows[y].Cells[57].Value.ToString() + "' Color4, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[58].Value.ToString() + "' Color5, '" + Stoping.Rows[y].Cells[59].Value.ToString() + "' Color6,'" + Stoping.Rows[y].Cells[60].Value.ToString() + "' Color7, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[61].Value.ToString() + "' Color8, '" + Stoping.Rows[y].Cells[62].Value.ToString() + "' Color9,'" + Stoping.Rows[y].Cells[63].Value.ToString() + "' Color10, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[64].Value.ToString() + "' Color11, '" + Stoping.Rows[y].Cells[65].Value.ToString() + "' Color12,'" + Stoping.Rows[y].Cells[66].Value.ToString() + "' Color13, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[67].Value.ToString() + "' Color14, '" + Stoping.Rows[y].Cells[68].Value.ToString() + "' Color15,'" + Stoping.Rows[y].Cells[69].Value.ToString() + "' Color16, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[70].Value.ToString() + "' Color17, '" + Stoping.Rows[y].Cells[71].Value.ToString() + "' Color18,'" + Stoping.Rows[y].Cells[72].Value.ToString() + "' Color19, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[73].Value.ToString() + "' Color20, '" + Stoping.Rows[y].Cells[74].Value.ToString() + "' Color21,'" + Stoping.Rows[y].Cells[75].Value.ToString() + "' Color22, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[76].Value.ToString() + "' Color23, '" + Stoping.Rows[y].Cells[77].Value.ToString() + "' Color24,'" + Stoping.Rows[y].Cells[78].Value.ToString() + "' Color25, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[79].Value.ToString() + "' Color26, '" + Stoping.Rows[y].Cells[80].Value.ToString() + "' Color27,'" + Stoping.Rows[y].Cells[81].Value.ToString() + "' Color28, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[82].Value.ToString() + "' Color29, '" + Stoping.Rows[y].Cells[83].Value.ToString() + "' Color30,'" + Stoping.Rows[y].Cells[84].Value.ToString() + "' Color31, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[85].Value.ToString() + "' Color32, '" + Stoping.Rows[y].Cells[86].Value.ToString() + "' Color33,'" + Stoping.Rows[y].Cells[87].Value.ToString() + "' Color34, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[88].Value.ToString() + "' Color35, '" + Stoping.Rows[y].Cells[89].Value.ToString() + "' Color36,'" + Stoping.Rows[y].Cells[90].Value + "' Color37, \r\n";
                    _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[91].Value.ToString() + "' Color38, '" + Stoping.Rows[y].Cells[92].Value.ToString() + "' Color39,'" + Stoping.Rows[y].Cells[93].Value.ToString() + "' Color40 \r\n";
                }

            }

            _dbManStopingData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManStopingData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManStopingData.ResultsTableName = "MODaily_Stoping_Data";
            _dbManStopingData.ExecuteInstruction();

            DataSet dsStopeHead = new DataSet();
            dsStopeHead.Tables.Add(_dbManStopingHeading.ResultsDataTable);
            DataSet dsStopeData = new DataSet();
            dsStopeData.Tables.Add(_dbManStopingData.ResultsDataTable);

            theReportDev.RegisterData(dsStopeHead);
            theReportDev.RegisterData(dsStopeData);

            theReportDev.Load(TGlobalItems.ReportsFolder + "\\MODailyDev.frx");
            try
            {
                if (reportSettings.Color == "0")
                {
                    lblColors.Text = "ABS Colors";
                    //return;
                }
                else
                {
                    lblColors.Text = "New Colors";
                }
            }
            catch { }

            theReportDev.SetParameterValue("Colors", lblColors.Text);

            //theReportDev.Design();

            //if (TParameters.DesignReport)
            //{
            //    theReportDev.Design();
            //}

            theReportDev.Prepare();
            ActiveReport.SetReport = theReportDev;
            ActiveReport.isDone = true;
        }

        private void pgMODaily_Click(object sender, EventArgs e)
        {

        }

        string TotShiftLbl = "";
        public void LoadStoping()
        {
            string month2 = TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth);
            string Section = procs.ExtractBeforeColon(reportSettings.SectionID);

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();           
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " declare @prev integer \r\n";



            _dbMan.SqlStatement = _dbMan.SqlStatement + "  set @prev = (select max(prodmonth) aaaa from vw_Planmonth where prodmonth < '" + month2 + "')   \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "select a.*, CrewName OrgUnitDS from(select distinct case when pmold1 is null then 'Red' when pmold1 is not null and pmold1 < @prev then 'orange' else '' end as newwpflag,  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " a.*, case when www IS not null then 'Top20' else '' end as top20, '' perbasedfc from(select p.workplaceid, w.riskrating, p.calendardate, p.orgunitds OrgUnitNo, p.orgunitds org, p.shiftday, p.sectionid, p.activity, p.workingday workingday, isnull(convert(varchar(20),p.booksqm),'')booksqm, p.mofc  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + ", p.bookcode, p.Pumahola, p.ProblemID, isnull(p.AdjSqm,0)AdjSqm, p.sqm plansqm, isnull(ABSCode, 'Safe')ABSCode  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + ", w.description, convert(decimal(18, 0), pm.fl) fl, pm.sqmtotal, s.Sectionid_1 sbid, s.Name_1 sbname, cr.CrewName from vw_Planning p, vw_Planmonth pm, crew cr, (select * from SECTION_COMPLETE where prodmonth = '" + month2 + "' and Sectionid_2 = '" + Section + "') s, Workplace w  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "where p.workplaceid = pm.workplaceid and p.sectionid = pm.sectionid and p.prodmonth = pm.prodmonth and p.activity = pm.activity and  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "p.prodmonth = s.prodmonth and p.sectionid = s.sectionid  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "and p.workplaceid = w.workplaceid  and  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "p.prodmonth = '" + month2 + "' and cr.GangNo = pm.OrgUnitDay  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "and p.activity in (0, 9) and isnull(pm.tons,0) >= 0 and p.PlanCode = 'MP' and pm.PlanCode = 'MP' ) a  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " left outer join  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " (select top(" + NoTopPanels + ") * from(select workplaceid www, sum(content) cc from vw_Planmonth where activity <> 1 and prodmonth = '" + month2 + "' and PlanCode = 'MP'  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " group by workplaceid) a order by cc desc) Top20  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " on a.workplaceid = top20.www  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "(select workplaceid wz, max(prodmonth) pmold1 from vw_Planmonth  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "where prodmonth < '" + month2 + "' and PlanCode = 'MP'  group by workplaceid) newwp on  a.workplaceid = newwp.wz  \r\n";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " )a  \r\n";

            //if (month2 == "201901")
            //{
            //    _dbMan.SqlStatement = _dbMan.SqlStatement + "  where Calendardate <> '2019-01-21 00:00:00.000' \r\n";
            //    _dbMan.SqlStatement = _dbMan.SqlStatement + "  and Calendardate <> '2019-01-20 00:00:00.000' \r\n";
            //}


            _dbMan.SqlStatement = _dbMan.SqlStatement + "order by a.sbid, a.sectionid, a.activity, a.description, a.calendardate";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            textBox1.Text = _dbMan.SqlStatement.ToString();

            DataTable Neil = _dbMan.ResultsDataTable;

            int x = 1;
            x = 1;

            int col = 3;
            TimeSpan Span;

            string checkm;

            if (Neil.Rows.Count == 0)
            {
                MessageBox.Show("No Planning or Bookings have been entered for                                            " +
                   "Section   " + reportSettings.SectionID + "", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Cursor = Cursors.Default;
                ActiveReport.isDone = true;
                return;
            }
            //this.Cursor = Cursors.WaitCursor;            
            Stoping.Dispose();

            DataGridView dt = new DataGridView();

            Stoping = dt;
            //Stoping.Parent = panel1;
            Stoping.Visible = false;

            Stoping.RowCount = 600;
            Stoping.ColumnCount = 96;
            //Stoping.ColumnCount = 55 + 40;

            //set blanks for RiskRating column
            for (int w = 0; w < Stoping.RowCount; w++)
            {
                try
                {
                    Stoping.Rows[w].Cells[51].Value = "";
                    Stoping.Rows[w].Cells[54].Value = "";
                }
                catch { }
            }

            StartDate1.Value = Convert.ToDateTime(Neil.Rows[0]["calendardate"].ToString());
            string SBLabel = Neil.Rows[0]["sbid"].ToString() + ":" + Neil.Rows[0]["sbname"].ToString();
            string CurrShftLbl = "1";

           string WPLable = "";
            Stoping.Columns[0].HeaderText = "Section"; //Section
            Stoping.Columns[1].HeaderText = "Gang"; //Orgunit
            Stoping.Columns[2].HeaderText = "Workplace"; //Workplace
            Stoping.Columns[3].HeaderText = "   FL"; //FL
            Stoping.Columns[4].HeaderText = "";
            Stoping.Columns[5].HeaderText = "";
            Stoping.Columns[6].HeaderText = "";
            Stoping.Columns[7].HeaderText = "";
            Stoping.Columns[8].HeaderText = "";
            Stoping.Columns[9].HeaderText = "";
            Stoping.Columns[10].HeaderText = "";
            Stoping.Columns[11].HeaderText = "";
            Stoping.Columns[12].HeaderText = "";
            Stoping.Columns[13].HeaderText = "";
            Stoping.Columns[14].HeaderText = "";
            Stoping.Columns[15].HeaderText = "";
            Stoping.Columns[16].HeaderText = "";
            Stoping.Columns[17].HeaderText = "";
            Stoping.Columns[18].HeaderText = "";
            Stoping.Columns[19].HeaderText = "";
            Stoping.Columns[20].HeaderText = "";
            Stoping.Columns[21].HeaderText = "";
            Stoping.Columns[22].HeaderText = "";
            Stoping.Columns[23].HeaderText = "";
            Stoping.Columns[24].HeaderText = "";
            Stoping.Columns[25].HeaderText = "";

            Stoping.Columns[26].HeaderText = "";
            Stoping.Columns[27].HeaderText = "";
            Stoping.Columns[28].HeaderText = "";
            Stoping.Columns[29].HeaderText = "";
            Stoping.Columns[30].HeaderText = "";
            Stoping.Columns[31].HeaderText = "";
            Stoping.Columns[32].HeaderText = "";
            Stoping.Columns[33].HeaderText = "";
            Stoping.Columns[34].HeaderText = "";
            Stoping.Columns[35].HeaderText = "";
            Stoping.Columns[36].HeaderText = "";
            Stoping.Columns[37].HeaderText = "";
            Stoping.Columns[38].HeaderText = "";
            Stoping.Columns[39].HeaderText = "";

            Stoping.Columns[40].HeaderText = "";
            Stoping.Columns[41].HeaderText = "";
            Stoping.Columns[42].HeaderText = "";
            Stoping.Columns[43].HeaderText = "";

            Stoping.Columns[44].HeaderText = "Prog. Plan";
            Stoping.Columns[45].HeaderText = "Prog. Book";
            Stoping.Columns[46].HeaderText = "Prog. Var";
            Stoping.Columns[47].HeaderText = "Mth. Call";
            Stoping.Columns[48].HeaderText = "Mth. F/C";
            Stoping.Columns[49].HeaderText = "MO. F/C";
            Stoping.Columns[51].HeaderText = "Plan Sqm";
            
            Stoping.Columns[55].HeaderText = "";
            Stoping.Columns[56].HeaderText = "";
            Stoping.Columns[57].HeaderText = "";
            Stoping.Columns[58].HeaderText = "";
            Stoping.Columns[59].HeaderText = "";
            Stoping.Columns[60].HeaderText = "";
            Stoping.Columns[61].HeaderText = "";
            Stoping.Columns[62].HeaderText = "";
            Stoping.Columns[63].HeaderText = "";
            Stoping.Columns[64].HeaderText = "";
            Stoping.Columns[65].HeaderText = "";
            Stoping.Columns[66].HeaderText = "";
            Stoping.Columns[67].HeaderText = "";
            Stoping.Columns[68].HeaderText = "";
            Stoping.Columns[69].HeaderText = "";
            Stoping.Columns[70].HeaderText = "";
            Stoping.Columns[71].HeaderText = "";
            Stoping.Columns[72].HeaderText = "";
            Stoping.Columns[73].HeaderText = "";
            Stoping.Columns[74].HeaderText = "";
            Stoping.Columns[75].HeaderText = "";
            Stoping.Columns[76].HeaderText = "";
            Stoping.Columns[77].HeaderText = "";
            Stoping.Columns[78].HeaderText = "";
            Stoping.Columns[79].HeaderText = "";
            Stoping.Columns[80].HeaderText = "";
            Stoping.Columns[81].HeaderText = "";
            Stoping.Columns[82].HeaderText = "";
            Stoping.Columns[83].HeaderText = "";
            Stoping.Columns[84].HeaderText = "";
            Stoping.Columns[85].HeaderText = "";
            Stoping.Columns[86].HeaderText = "";
            Stoping.Columns[87].HeaderText = "";
            Stoping.Columns[88].HeaderText = "";
            Stoping.Columns[89].HeaderText = "";
            Stoping.Columns[90].HeaderText = "";
            Stoping.Columns[91].HeaderText = "";
            Stoping.Columns[92].HeaderText = "";
            Stoping.Columns[93].HeaderText = "";
            Stoping.Columns[94].HeaderText = "";
            Stoping.Columns[95].HeaderText = "";

            string CellValue = "";
            string CellValueStart = "";
            string CellValueEnd = "";
            string CellValueB = "";
            string CellValueStartB = "";
            string CellValueEndB = "";


            foreach (DataRow r in Neil.Rows)
            {
                Span = Convert.ToDateTime(r["calendardate"].ToString()).Subtract(StartDate1.Value);
                string bbbb = r["calendardate"].ToString();
                col = Convert.ToInt32(Span.Days) + 4;

                if (month2 == "201901")
                {
                    if (Convert.ToDateTime(r["calendardate"].ToString()) > Convert.ToDateTime("29 Dec 2018"))
                    {
                    Span = Convert.ToDateTime(r["calendardate"].ToString()).Subtract(StartDate1.Value);
                    col = Convert.ToInt32(Span.Days) + 4 - 4;
                    }

                }


                    
               
                    if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now)
                {
                    if (Convert.ToInt32(CurrShftLbl) <= Convert.ToInt32(r["shiftday"].ToString()))
                        CurrShftLbl = r["shiftday"].ToString();
                }

                Stoping.Rows[x].Cells[53].Value = "      ";
                Stoping.Rows[x - 1].Cells[53].Value = "      ";

                //Stoping.Rows[x].Cells[col + 50].Value = "";
                //Stoping.Rows[x - 1].Cells[col + 50].Value = "";

                if (WPLable != r["workplaceid"].ToString() + r["activity"].ToString())
                {
                    // do sb
                    if (SBLabel != r["sbid"].ToString() + ":" + r["sbname"].ToString())
                    {
                        Stoping.Rows[x].Cells[0].Value = SBLabel;
                        Stoping.Rows[x].Cells[1].Value = "";
                        Stoping.Rows[x].Cells[2].Value = "";
                        Stoping.Rows[x].Cells[3].Value = "a";//3
                        //Stoping.Rows[x].Cells[3].Value = "";

                        Stoping.Rows[x - 1].Cells[0].Value = "";
                        Stoping.Rows[x - 1].Cells[1].Value = "";
                        Stoping.Rows[x - 1].Cells[2].Value = "";
                        Stoping.Rows[x - 1].Cells[3].Value = "a";//3
                        Stoping.Rows[x + 1].Cells[3].Value = "c";//3

                        Stoping.Rows[x].Cells[44].Value = "0";
                        Stoping.Rows[x].Cells[45].Value = "0";
                        Stoping.Rows[x].Cells[46].Value = "0";
                        Stoping.Rows[x].Cells[47].Value = "0";
                        Stoping.Rows[x].Cells[48].Value = "0";
                        Stoping.Rows[x].Cells[49].Value = "0";
                        Stoping.Rows[x].Cells[50].Value = "0";
                        Stoping.Rows[x].Cells[51].Value = "";
                        Stoping.Rows[x].Cells[52].Value = "";

                        Stoping.Rows[x - 1].Cells[44].Value = "";
                        Stoping.Rows[x - 1].Cells[45].Value = "";
                        Stoping.Rows[x - 1].Cells[46].Value = "";
                        Stoping.Rows[x - 1].Cells[47].Value = "";
                        Stoping.Rows[x - 1].Cells[48].Value = "";
                        Stoping.Rows[x - 1].Cells[49].Value = "";
                        Stoping.Rows[x - 1].Cells[50].Value = "";
                        Stoping.Rows[x - 1].Cells[51].Value = "";
                        Stoping.Rows[x - 1].Cells[52].Value = "";

                        if (x == 15)
                        {
                            string aa = "";
                        }

                        for (int y = 4; y < 44; y++)
                        {
                            Stoping.Rows[x - 1].Cells[y].Value = "   ";
                            Stoping.Rows[x].Cells[y].Value = "   ";
                            if (Stoping.Rows[455].Cells[y].Value != null)
                            {
                                if (Stoping[y, x - 2].Style.BackColor == System.Drawing.Color.SkyBlue)
                                {
                                    Stoping.Rows[x - 1].Cells[y].Value = "021" + Stoping.Rows[450].Cells[y].Value;
                                    Stoping.Rows[x].Cells[y].Value = "022" + Stoping.Rows[455].Cells[y].Value;
                                    if (Stoping.Rows[455].Cells[y].Value.ToString() != "")
                                    {
                                        if (Convert.ToDecimal(Stoping.Rows[455].Cells[y].Value.ToString()) < 0)
                                        {
                                            Stoping.Rows[x].Cells[y].Value = "122" + Stoping.Rows[455].Cells[y].Value;
                                        }
                                    }
                                }
                                else
                                {
                                    Stoping.Rows[x].Cells[y].Value = "082" + Stoping.Rows[450].Cells[y].Value;
                                }
                            }

                            Stoping.Rows[450].Cells[y].Value = null;
                            Stoping.Rows[455].Cells[y].Value = null;
                        }

                        /////////New Colors////////
                        for (int y = 55; y < 96; y++)
                        {
                            Stoping.Rows[x - 1].Cells[y].Value = "   ";
                            Stoping.Rows[x].Cells[y].Value = "   ";
                            if (Stoping.Rows[455].Cells[y].Value != null)
                            {
                                if (Stoping[y, x - 2].Style.BackColor == System.Drawing.Color.SkyBlue)
                                {
                                    Stoping.Rows[x - 1].Cells[y].Value = "021" + Stoping.Rows[450].Cells[y].Value;
                                    Stoping.Rows[x].Cells[y].Value = "022" + Stoping.Rows[455].Cells[y].Value;
                                    if (Stoping.Rows[455].Cells[y].Value.ToString() != "")
                                    {
                                        if (Convert.ToDecimal(Stoping.Rows[455].Cells[y].Value.ToString()) < 0)
                                        {
                                            Stoping.Rows[x].Cells[y].Value = "122" + Stoping.Rows[455].Cells[y].Value;
                                        }
                                    }
                                }
                                else
                                {
                                    Stoping.Rows[x].Cells[y].Value = "082" + Stoping.Rows[450].Cells[y].Value;
                                }
                            }

                            Stoping.Rows[450].Cells[y].Value = null;
                            Stoping.Rows[455].Cells[y].Value = null;
                        }


                        SBLabel = r["sbid"].ToString() + ":" + r["sbname"].ToString();
                        x = x + 2;

                    }

                    if (r["OrgUnitDS"].ToString() != "")
                        Stoping.Rows[x].Cells[1].Value = procs.ExtractAfterColon(r["OrgUnitDS"].ToString());
                    else
                        Stoping.Rows[x].Cells[1].Value = procs.ExtractBeforeColon(r["OrgUnitDS"].ToString());

                    //S//toping.Rows[x].Cells[1].Value = r["orgunitds"].ToString();//1


                    string wp = r["workplaceid"].ToString() ;
                  

                    Stoping.Rows[x - 1].Cells[52].Value = "";

                    Stoping.Rows[x].Cells[52].Value = wp;

                    Stoping.Rows[x - 1].Cells[53].Value = r["workplaceid"].ToString();
                    Stoping.Rows[x].Cells[53].Value = r["workplaceid"].ToString();

                    Stoping.Rows[x].Cells[2].Value =  r["description"].ToString();//2
                    if (r["activity"].ToString() == "9")
                    {
                        Stoping.Rows[x].Cells[2].Value =  r["description"].ToString() + " Ldg";//2
                    }
                    Stoping.Rows[x].Cells[3].Value = r["fl"].ToString();


                    if (r["newwpflag"].ToString() == "")
                        Stoping.Rows[x].Cells[54].Value = "";

                    if (r["newwpflag"].ToString() == "Red")
                        Stoping.Rows[x].Cells[54].Value = "Red";

                    if (r["newwpflag"].ToString() == "orange")
                        Stoping.Rows[x].Cells[54].Value = "orange";


                    //top 20


                    //Stoping.Rows[x - 1].Cells[52].Value = r["top20"].ToString();

                    if (r["plansqm"] != DBNull.Value)
                        Stoping.Rows[x].Cells[51].Value = r["plansqm"].ToString();
                    else
                        Stoping.Rows[x].Cells[51].Value = "";

                    //if (r["adv"] != DBNull.Value)
                    //    Stoping.Rows[x].Cells[50].Value = r["adv"].ToString();
                    //else
                    //    Stoping.Rows[x].Cells[50].Value = "";
                    //Stoping.Rows[x].Cells[50].Value = "0";// r["Pumahola"].ToString();

                    //if (x == 1)
                    //    Stoping.Rows[x].Cells[3].Value = "c";//3


                    Stoping.Rows[x - 1].Cells[0].Value = "";
                    Stoping.Rows[x - 1].Cells[1].Value = "";
                    Stoping.Rows[x - 1].Cells[2].Value = "";
                    if (Stoping.Rows[x - 1].Cells[3].Value != "c")//3
                        Stoping.Rows[x - 1].Cells[3].Value = "";//3

                    Stoping.Rows[x].Cells[44].Value = "0";
                    Stoping.Rows[x].Cells[45].Value = "0";
                    Stoping.Rows[x].Cells[46].Value = "0";
                    Stoping.Rows[x].Cells[47].Value = "0";
                    Stoping.Rows[x].Cells[48].Value = "0";
                    Stoping.Rows[x].Cells[49].Value = "0";


                    Stoping.Rows[x - 1].Cells[44].Value = "";
                    Stoping.Rows[x - 1].Cells[45].Value = "";
                    Stoping.Rows[x - 1].Cells[46].Value = "";
                    Stoping.Rows[x - 1].Cells[47].Value = "";
                    Stoping.Rows[x - 1].Cells[48].Value = "";
                    Stoping.Rows[x - 1].Cells[49].Value = "";
                    Stoping.Rows[x - 1].Cells[50].Value = "";
                    Stoping.Rows[x - 1].Cells[51].Value = "";




                    Stoping.Rows[x - 1].Cells[52].Value = r["top20"].ToString();



                    for (int y = 4; y < 44; y++)
                    {
                        Stoping.Rows[x - 1].Cells[y].Value = "000 ";
                        Stoping.Rows[x].Cells[y].Value = "000 ";
                    }                    

                    for (int y = 55; y < 96; y++)
                    {
                        Stoping.Rows[x - 1].Cells[y].Value = "000 ";
                        Stoping.Rows[x].Cells[y].Value = "000 ";
                    }

                    Stoping.Rows[x].Cells[44].Value = "0";
                    Stoping.Rows[x].Cells[45].Value = "0";
                    Stoping.Rows[x].Cells[46].Value = (Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)).ToString();

                    if ((Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)) < 0)
                    {
                        Stoping[46, x].Style.ForeColor = System.Drawing.Color.Red;
                    }

                    if ((Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)) < 0)
                    {
                        Stoping.Rows[x].Cells[46].Value = "1" + Stoping.Rows[x].Cells[46].Value;
                    }
                    else
                    {
                        Stoping.Rows[x].Cells[46].Value = "0" + Stoping.Rows[x].Cells[46].Value;

                    }

                    Stoping.Rows[x].Cells[47].Value = Math.Round(Convert.ToDecimal(r["sqmtotal"].ToString()), 0);



                    WPLable = r["workplaceid"].ToString() + r["activity"].ToString();
                    x = x + 2;

                }

                

                if (x == 3)
                {
                    Stoping.Columns[col].HeaderText = Convert.ToDateTime(r["calendardate"].ToString()).ToString("dd MMM ddd");
                    Stoping.Columns[col].Visible = true;
                    Stoping.Text = r["shiftday"].ToString();
                    TotShiftLbl = r["shiftday"].ToString();

                }
                // do check meas
                checkm = "N";
                if (Convert.ToDateTime(r["calendardate"].ToString()).ToString("ddd") == SysSettings.CheckMeas)//  "Thu")
                {
                    if (r["workingday"].ToString() != "ZN")
                    {
                        if (col > 1)
                        {
                            checkm = "Y";
                            if (r["workingday"].ToString() != "N")
                                Stoping[col, x - 2].Style.BackColor = System.Drawing.Color.SkyBlue;
                        }
                    }
                }

                if (Stoping.Rows[x - 2].Cells[44].Value == null)
                {
                    Stoping.Rows[x - 2].Cells[44].Value = "0";
                }

                if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now)
                    Stoping.Rows[x - 2].Cells[44].Value = Convert.ToInt32(Stoping.Rows[x - 2].Cells[44].Value.ToString()) + Convert.ToInt32(r["plansqm"].ToString());

                if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now)
                {
                    
                        if (Stoping.Rows[450].Cells[col].Value == null)
                        {
                            Stoping.Rows[450].Cells[col].Value = "0";
                        }
                   
                    if (r["booksqm"].ToString() != "" && r["booksqm"].ToString() != "0") ///////////////// AdjSqm
                    {
                        //if (SysSettings.AdjBook == "Y")
                        //{
                            //Stoping.Rows[450].Cells[col].Value = Convert.ToInt32(Stoping.Rows[450].Cells[col].Value.ToString()) + Convert.ToInt32(r["booksqm"].ToString()) + Convert.ToInt32(r["Adjsqm"].ToString());
                        //}
                        //else
                        //{
                           Stoping.Rows[450].Cells[col].Value = Convert.ToInt32(Stoping.Rows[450].Cells[col].Value.ToString()) + Convert.ToInt32(r["booksqm"].ToString());
                        //}
                    }
                }

                if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now)
                {
                    try
                    {
                        if (Stoping.Rows[455].Cells[col].Value == null)
                        {
                            Stoping.Rows[455].Cells[col].Value = "0";
                        }
                        if (r["booksqm"].ToString() != "" )
                        {
                            //if (SysSettings.AdjBook == "Y")
                            //{
                            Stoping.Rows[455].Cells[col].Value = Convert.ToInt32(Stoping.Rows[455].Cells[col].Value.ToString()) + Convert.ToInt32(r["Adjsqm"].ToString()); //+ Convert.ToInt32(r["booksqm"].ToString())
                            //Stoping.Rows[455].Cells[col].Value = Convert.ToInt32(Stoping.Rows[455].Cells[col].Value.ToString() + Convert.ToInt32(r["booksqm"].ToString());                                                                                                                                             //}
                                                                                                                                                                           //else
                                                                                                                                                                           //{
                                                                                                                                                                           //    Stoping.Rows[455].Cells[col].Value = Convert.ToInt32(Stoping.Rows[455].Cells[col].Value.ToString()) + Convert.ToInt32(r["booksqm"].ToString());
                                                                                                                                                                           //}
                        }
                    }
                    catch
                    {
                    }
                }

                if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now)
                {
                    try
                    {
                        if (Stoping.Rows[451].Cells[col].Value == null)
                        {
                            Stoping.Rows[451].Cells[col].Value = "0";
                        }

                        if (Stoping.Rows[470].Cells[col].Value == null)
                        {
                            Stoping.Rows[470].Cells[col].Value = "0";
                        }

                        if (r["booksqm"].ToString() != "")
                        {
                            //if (SysSettings.AdjBook == "Y")
                            //{
                            Stoping.Rows[451].Cells[col].Value = Convert.ToInt32(Stoping.Rows[451].Cells[col].Value.ToString()) + Convert.ToInt32(r["Booksqm"].ToString());


                            //}
                            //else
                            //{
                            //    Stoping.Rows[451].Cells[col].Value = Convert.ToInt32(Stoping.Rows[451].Cells[col].Value.ToString()) + Convert.ToInt32(r["Booksqm"].ToString());


                            //}
                        }
                    }
                    catch
                    {
                    }
                }

                if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now)
                {
                    try
                    {
                        if (Stoping.Rows[456].Cells[col].Value == null)
                        {
                            Stoping.Rows[456].Cells[col].Value = "0";
                        }
                        if (r["booksqm"].ToString() != "")
                        {
                            //if (SysSettings.AdjBook == "Y")
                            //{
                            Stoping.Rows[456].Cells[col].Value = Convert.ToInt32(Stoping.Rows[456].Cells[col].Value.ToString()) + Convert.ToInt32(r["Adjsqm"].ToString());//+ Convert.ToInt32(r["booksqm"].ToString()) 
                                                                                                                                                                          //}
                                                                                                                                                                          //else
                                                                                                                                                                          //{
                                                                                                                                                                          //    Stoping.Rows[456].Cells[col].Value = Convert.ToInt32(Stoping.Rows[456].Cells[col].Value.ToString()) + Convert.ToInt32(r["booksqm"].ToString());
                                                                                                                                                                          //}
                        }

                    }
                    catch {                    }

                }


                if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now.AddDays(-0))
                {
                    if (checkm == "Y")
                    {
                        Stoping.Rows[x - 2].Cells[49].Value = "0";
                        if (r["mofc"].ToString() != "")
                        {
                            Stoping.Rows[x - 2].Cells[49].Value = r["mofc"].ToString();
                        }


                        if (r["perbasedfc"].ToString() != "")
                        {
                            Stoping.Rows[x - 2].Cells[50].Value = r["perbasedfc"].ToString();
                        }
                    }
                }

                if (r["booksqm"].ToString() != "" )
                {


                    if (Stoping.Rows[x - 2].Cells[45].Value == null)
                    {
                        Stoping.Rows[x - 2].Cells[45].Value = "0";
                    }

                    if (Convert.ToDateTime(r["calendardate"].ToString()) < System.DateTime.Now)
                    {
                        //if (SysSettings.AdjBook == "Y")
                        //{
                            Stoping.Rows[x - 2].Cells[45].Value = Convert.ToInt32(Stoping.Rows[x - 2].Cells[45].Value.ToString()) + Convert.ToInt32(r["booksqm"].ToString()) + Convert.ToInt32(r["Adjsqm"].ToString());
                        //}
                        //else
                        //{
                        //    Stoping.Rows[x - 2].Cells[45].Value = Convert.ToInt32(Stoping.Rows[x - 2].Cells[45].Value.ToString()) + Convert.ToInt32(r["booksqm"].ToString());
                        //}


                    }



                    if (checkm == "N")
                    {
                        if (r["workingday"].ToString() == "Y")
                        {
                            if (r["ProblemID"].ToString() != "")
                            {
                                Stoping.Rows[x - 3].Cells[col + 51].Value = "1";
                                Stoping.Rows[x - 2].Cells[col + 51].Value = "1";

                                if (r["booksqm"].ToString() == "0")
                                {
                                    Stoping.Rows[x - 2].Cells[col].Value = "101" + r["ProblemID"].ToString();
                                }
                                else
                                {
                                    //if (SysSettings.AdjBook == "Y")
                                    //{
                                    Stoping.Rows[x - 2].Cells[col].Value = "102" + Convert.ToString(Convert.ToInt32(r["booksqm"].ToString()));// + Convert.ToInt32(r["Adjsqm"].ToString()));
                                    //}
                                    //else
                                    //{
                                    //    Stoping.Rows[x - 2].Cells[col].Value = "102" + r["booksqm"].ToString();
                                    //}
                                }
                            }
                            else if (r["booksqm"].ToString() != "" )
                            {
                                string zz = r["booksqm"].ToString();
                                string aa = r["bookcode"].ToString();
                                string bb = r["ProblemID"].ToString();
                                Stoping.Rows[x - 3].Cells[col + 51].Value = "3";
                                Stoping.Rows[x - 2].Cells[col + 51].Value = "3";

                                //if (SysSettings.AdjBook == "Y")
                                //{
                                Stoping.Rows[x - 2].Cells[col].Value = "002" + Convert.ToString(Convert.ToInt32(r["booksqm"].ToString()));// + Convert.ToInt32(r["Adjsqm"].ToString()));
                                //}
                                //else
                                //{
                                //    Stoping.Rows[x - 2].Cells[col].Value = "002" + r["booksqm"].ToString();
                                //}
                                if (r["bookcode"].ToString() != "BL" && r["bookcode"].ToString() != "PR")
                                {
                                    Stoping.Rows[x - 2].Cells[col].Value = "203" + r["bookcode"].ToString();
                                }

                            }
                            //else { Stoping.Rows[x - 2].Cells[col].Value = "0020"; }
                        }
                        else
                        {
                            Stoping.Rows[x - 3].Cells[col + 51].Value = "2";
                            Stoping.Rows[x - 2].Cells[col + 51].Value = "2";


                            if ((r["Pumahola"].ToString() == "Y"))
                            {
                                Stoping.Rows[x - 3].Cells[col].Value = "050 ";
                                if (r["bookprob"].ToString() != "")
                                {
                                    if (r["booksqm"].ToString() != "")
                                    {
                                        Stoping.Rows[x - 2].Cells[col].Value = "151" + r["ProblemID"].ToString();
                                    }
                                    else
                                    {
                                        //if (SysSettings.AdjBook == "Y")
                                        //{
                                            Stoping.Rows[x - 2].Cells[col].Value = "151" + Convert.ToString(Convert.ToInt32(r["booksqm"].ToString()) + Convert.ToInt32(r["Adjsqm"].ToString()));
                                        //}
                                        //else
                                        //{
                                        //    Stoping.Rows[x - 2].Cells[col].Value = "151" + r["booksqm"].ToString();
                                        //}
                                    }
                                }
                                else
                                {

                                    //if (SysSettings.AdjBook == "Y")
                                    //{
                                        Stoping.Rows[x - 2].Cells[col].Value = "052" + Convert.ToString(Convert.ToInt32(r["booksqm"].ToString()) + Convert.ToInt32(r["Adjsqm"].ToString()));
                                    //}
                                    //else
                                    //{
                                    //    Stoping.Rows[x - 2].Cells[col].Value = "052" + r["booksqm"].ToString();
                                    //}
                                    if (r["booksqm"].ToString() == "0" && r["bookcode"].ToString() != "PR")
                                    {
                                        Stoping.Rows[x - 2].Cells[col].Value = "253" + r["bookcode"].ToString();
                                    }

                                }

                            }
                            else
                            {
                                Stoping.Rows[x - 3].Cells[col + 51].Value = "2";
                                Stoping.Rows[x - 2].Cells[col + 51].Value = "2";

                                try
                                {
                                    if (r["booksqm"].ToString() != "")
                                    {
                                        Stoping.Rows[x - 3].Cells[col].Value = "012";
                                        if (Convert.ToInt32(r["booksqm"].ToString()) + Convert.ToInt32(r["Adjsqm"].ToString()) < 0)
                                            Stoping.Rows[x - 2].Cells[col].Value = "112" + Convert.ToString(Convert.ToInt32(r["booksqm"].ToString()) + Convert.ToInt32(r["Adjsqm"].ToString()));
                                        else
                                            Stoping.Rows[x - 2].Cells[col].Value = "012" + Convert.ToString(Convert.ToInt32(r["booksqm"].ToString()) + Convert.ToInt32(r["Adjsqm"].ToString()));
                                    }
                                }
                                catch
                                {

                                }


                            }

                        }


                    }
                    else
                    {
                        Stoping.Rows[x - 2].Cells[col].Value = "020 ";
                        Stoping.Rows[x - 3].Cells[col].Value = "020 ";                        

                        if (r["mofc"].ToString() != "")
                        {
                            Stoping.Rows[x - 2].Cells[49].Value = r["mofc"].ToString();
                        }

                        if (Convert.ToDecimal(r["AdjSqm"].ToString()) < 0)
                        {
                            Stoping.Rows[x - 2].Cells[col].Value = "122" + r["AdjSqm"].ToString();
                        }
                        else
                        {
                            Stoping.Rows[x - 2].Cells[col].Value = "022" + r["AdjSqm"].ToString();
                        }



                        if (r["ProblemID"].ToString() != "")
                        {
                            Stoping.Rows[x - 3].Cells[col + 51].Value = "1";
                            Stoping.Rows[x - 2].Cells[col + 51].Value = "1";

                            //Stoping.Rows[x - 2].Cells[col].Value = "122" + r["AdjSqm"].ToString();
                            if ((r["booksqm"].ToString() != ""))
                            {
                                Stoping.Rows[x - 3].Cells[col].Value = "121" + r["ProblemID"].ToString();

                            }
                            else
                            {
                                //if (SysSettings.AdjBook == "Y")
                                //{
                                Stoping.Rows[x - 3].Cells[col].Value = "121" + Convert.ToString(Convert.ToInt32(r["booksqm"].ToString()));// + Convert.ToInt32(r["Adjsqm"].ToString()); 
                                //}
                                //else
                                //{
                                //    Stoping.Rows[x - 3].Cells[col].Value = "121" + r["booksqm"].ToString();
                                //}
                            }

                        }
                        else
                        {
                            Stoping.Rows[x - 3].Cells[col + 51].Value = "3";
                            Stoping.Rows[x - 2].Cells[col + 51].Value = "3";

                            //if (SysSettings.AdjBook == "Y")
                            //{
                            Stoping.Rows[x - 3].Cells[col].Value = "021" + Convert.ToString(Convert.ToInt32(r["booksqm"].ToString())); //+ Convert.ToInt32(r["Adjsqm"].ToString()); 
                            //}
                            //else
                            //{
                            //    Stoping.Rows[x - 3].Cells[col].Value = "021" + r["booksqm"].ToString();
                            //}
                            if (r["booksqm"].ToString() == "")
                            {
                                Stoping.Rows[x - 3].Cells[col].Value = "221" + r["ProblemID"].ToString();
                            }
                        }

                    }

                    try
                    {
                        CellValue = Stoping.Rows[x - 2].Cells[col].Value.ToString();
                        CellValueB = Stoping.Rows[x - 3].Cells[col].Value.ToString();
                    }
                    catch { }

                    if (CellValueB == "")
                        CellValueB = "1020";


                    // do abs
                    if (ABSBtn.Text == "Remove Colours")
                    {
                        if (r["ABSCode"].ToString() == "Safe")
                        {
                            if (checkm == "N")
                            {
                                CellValueStart = CellValue.Substring(0, 1);
                                CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                                Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "3" + CellValueEnd;
                                try
                                {
                                    CellValueStartB = CellValueB.Substring(0, 1);
                                    CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                                    Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "3" + CellValueEndB;
                                }
                                catch
                                {
                                    CellValueStartB = "000";
                                    CellValueEndB = "000";
                                    Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "3" + CellValueEndB;
                                }
                            }
                            else
                            {
                                CellValueStart = CellValue.Substring(0, 1);
                                CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                                Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "A" + CellValueEnd;
                                try
                                {
                                    CellValueStartB = CellValueB.Substring(0, 1);
                                CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                                    Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "A" + CellValueEndB;
                                }
                                catch
                                {
                                   
                                }
                            }
                        }

                        if (r["ABSCode"].ToString() == "Unsafe")
                        {
                            if (checkm == "N")
                            {
                                CellValueStart = CellValue.Substring(0, 1);
                                CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                                Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "6" + CellValueEnd;

                                CellValueStartB = CellValueB.Substring(0, 1);
                                CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                                Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "6" + CellValueEndB;
                            }
                            else
                            {
                                CellValueStart = CellValue.Substring(0, 1);
                                CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                                Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "S" + CellValueEnd;

                                CellValueStartB = CellValueB.Substring(0, 1);
                                CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                                Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "S" + CellValueEndB;
                            }
                        }

                        if (r["ABSCode"].ToString() == "No Vis.")
                        {
                            if (checkm == "N")
                            {
                                CellValueStart = CellValue.Substring(0, 1);
                                CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                                Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "9" + CellValueEnd;

                                CellValueStartB = CellValueB.Substring(0, 1);
                                CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                                Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "9" + CellValueEndB;
                            }
                            else
                            {
                                CellValueStart = CellValue.Substring(0, 1);
                                CellValueEnd = CellValue.Substring(2, CellValue.Length - 2);
                                Stoping.Rows[x - 2].Cells[col].Value = CellValueStart + "B" + CellValueEnd;

                                CellValueStartB = CellValueB.Substring(0, 1);
                                CellValueEndB = CellValueB.Substring(2, CellValueB.Length - 2);
                                Stoping.Rows[x - 3].Cells[col].Value = CellValueStartB + "B" + CellValueEndB;
                            }
                        }
                    }

                }
                else
                {
                    if (checkm == "Y")
                    {
                        Stoping.Rows[x - 3].Cells[col].Value = "020 ";
                        Stoping.Rows[x - 2].Cells[col].Value = "020 ";

                        if (r["ProblemID"].ToString() != "")
                        {
                            Stoping.Rows[x - 3].Cells[col].Value = "121" + r["ProblemID"].ToString();

                            Stoping.Rows[x - 3].Cells[col + 51].Value = "1";
                            Stoping.Rows[x - 2].Cells[col + 51].Value = "1";
                        }
                    }
                    else
                    {
                        if (r["workingday"].ToString() == "N")
                        {
                            try
                            {
                                Stoping.Rows[x - 3].Cells[col + 51].Value = "2";
                                Stoping.Rows[x - 2].Cells[col + 51].Value = "2";

                                Stoping.Rows[x - 3].Cells[col].Value = "010 ";
                                Stoping.Rows[x - 2].Cells[col].Value = "010 ";
                                if ((r["Pumahola"].ToString() == "Y"))
                                {
                                    Stoping.Rows[x - 3].Cells[col].Value = "050 ";
                                    Stoping.Rows[x - 2].Cells[col].Value = "050 ";

                                }
                            }
                            catch { };
                        }

                        if (r["ProblemID"].ToString() != "")
                        {
                            Stoping.Rows[x - 2].Cells[col].Value = "101" + r["ProblemID"].ToString();

                            Stoping.Rows[x - 3].Cells[col + 51].Value = "1";
                            Stoping.Rows[x - 2].Cells[col + 51].Value = "1";
                        }
                    }

                }




            }

            Stoping.Rows[x].Cells[0].Value = SBLabel;
            Stoping.Rows[x].Cells[1].Value = "";
            Stoping.Rows[x].Cells[2].Value = "";
            Stoping.Rows[x].Cells[3].Value = "a";//3
            //Stoping.Rows[x].Cells[3].Value = "";

            Stoping.Rows[x - 1].Cells[1].Value = "";
            Stoping.Rows[x - 1].Cells[2].Value = "";
            Stoping.Rows[x - 1].Cells[3].Value = "a";//3
            //Stoping.Rows[x - 1].Cells[3].Value = "";//3
            //Stoping.Rows[x + 1].Cells[3].Value = "c";

            Stoping.Rows[x - 1].Cells[44].Value = "";
            Stoping.Rows[x - 1].Cells[45].Value = "";
            Stoping.Rows[x - 1].Cells[46].Value = "";
            Stoping.Rows[x - 1].Cells[47].Value = "";
            Stoping.Rows[x - 1].Cells[48].Value = "";
            Stoping.Rows[x - 1].Cells[49].Value = "";
            Stoping.Rows[x - 1].Cells[50].Value = "";
            Stoping.Rows[x - 1].Cells[51].Value = "";
            Stoping.Rows[x - 1].Cells[52].Value = "";
            Stoping.Rows[x - 1].Cells[53].Value = "      ";

            for (int y = 4; y <= 44; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "   ";
                Stoping.Rows[x].Cells[y].Value = "   ";
                if (Stoping.Rows[455].Cells[y].Value != null)
                {

                    if (Stoping[y, x - 2].Style.BackColor == System.Drawing.Color.SkyBlue)
                    {
                        Stoping.Rows[x - 1].Cells[y].Value = "021" + Stoping.Rows[450].Cells[y].Value;
                        //Stoping.Rows[x - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        if (Stoping.Rows[455].Cells[y].Value.ToString() != "")
                        {
                            if (Convert.ToDecimal(Stoping.Rows[455].Cells[y].Value.ToString()) >= 0)
                            {
                                Stoping.Rows[x].Cells[y].Value = "022" + Stoping.Rows[455].Cells[y].Value;
                            }
                            else
                            {
                                Stoping.Rows[x].Cells[y].Value = "122" + Stoping.Rows[455].Cells[y].Value;
                            }
                        }
                    }
                    else
                    {
                        Stoping.Rows[x].Cells[y].Value = "082" + Stoping.Rows[450].Cells[y].Value;
                    }
                }


            }
            

            ///////New Colors//////////////
            for (int y = 55; y <= 95; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "   ";
                Stoping.Rows[x].Cells[y].Value = "   ";
                if (Stoping.Rows[455].Cells[y].Value != null)
                {

                    if (Stoping[y, x - 2].Style.BackColor == System.Drawing.Color.SkyBlue)
                    {
                        Stoping.Rows[x - 1].Cells[y].Value = "021" + Stoping.Rows[450].Cells[y].Value;
                        //Stoping.Rows[x - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        if (Stoping.Rows[455].Cells[y].Value.ToString() != "")
                        {
                            if (Convert.ToDecimal(Stoping.Rows[455].Cells[y].Value.ToString()) >= 0)
                            {
                                Stoping.Rows[x].Cells[y].Value = "022" + Stoping.Rows[455].Cells[y].Value;
                            }
                            else
                            {
                                Stoping.Rows[x].Cells[y].Value = "122" + Stoping.Rows[455].Cells[y].Value;
                            }
                        }
                    }
                    else
                    {
                        Stoping.Rows[x].Cells[y].Value = "082" + Stoping.Rows[450].Cells[y].Value;
                    }
                }
            }


            x = x + 2;

            Stoping.Rows[x].Cells[0].Value = "Total";
            Stoping.Rows[x].Cells[1].Value = "";
            Stoping.Rows[x].Cells[2].Value = "";
            Stoping.Rows[x].Cells[3].Value = "b";//3

            Stoping.Rows[x - 1].Cells[1].Value = "";
            Stoping.Rows[x - 1].Cells[2].Value = "";
            Stoping.Rows[x - 1].Cells[3].Value = "b";//3

            Stoping.Rows[x - 1].Cells[44].Value = "";
            Stoping.Rows[x - 1].Cells[45].Value = "";
            Stoping.Rows[x - 1].Cells[46].Value = "";
            Stoping.Rows[x - 1].Cells[47].Value = "";
            Stoping.Rows[x - 1].Cells[48].Value = "";
            Stoping.Rows[x - 1].Cells[49].Value = "";

            Stoping.Rows[x + 2].Cells[0].Value = "Daily Booked Sqm";

            Stoping.Rows[x + 4].Cells[0].Value = "Prog Booked Sqm";

            Stoping.Rows[x + 6].Cells[0].Value = "Prog Recon";

            Stoping.Rows[x + 8].Cells[0].Value = "Adj Booking Sqm";

            Stoping.Rows[x + 1].Cells[1].Value = "";
            Stoping.Rows[x + 1].Cells[2].Value = "";
            Stoping.Rows[x + 1].Cells[3].Value = "";

            Stoping.Rows[x + 1].Cells[44].Value = "";
            Stoping.Rows[x + 1].Cells[45].Value = "";
            Stoping.Rows[x + 1].Cells[46].Value = "";
            Stoping.Rows[x + 1].Cells[47].Value = "";
            Stoping.Rows[x + 1].Cells[48].Value = "";
            Stoping.Rows[x + 1].Cells[49].Value = "";

            Stoping.Rows[x + 2].Cells[1].Value = "";
            Stoping.Rows[x + 2].Cells[2].Value = "";
            Stoping.Rows[x + 2].Cells[3].Value = "";

            Stoping.Rows[x + 2].Cells[44].Value = "";
            Stoping.Rows[x + 2].Cells[45].Value = "";
            Stoping.Rows[x + 2].Cells[46].Value = "";
            Stoping.Rows[x + 2].Cells[47].Value = "";
            Stoping.Rows[x + 2].Cells[48].Value = "";
            Stoping.Rows[x + 2].Cells[49].Value = "";

            Stoping.Rows[x + 3].Cells[1].Value = "";
            Stoping.Rows[x + 3].Cells[2].Value = "";
            Stoping.Rows[x + 3].Cells[3].Value = "";

            Stoping.Rows[x + 3].Cells[44].Value = "";
            Stoping.Rows[x + 3].Cells[45].Value = "";
            Stoping.Rows[x + 3].Cells[46].Value = "";
            Stoping.Rows[x + 3].Cells[47].Value = "";
            Stoping.Rows[x + 3].Cells[48].Value = "";
            Stoping.Rows[x + 3].Cells[49].Value = "";

            Stoping.Rows[x + 4].Cells[1].Value = "";
            Stoping.Rows[x + 4].Cells[2].Value = "";
            Stoping.Rows[x + 4].Cells[3].Value = "";

            Stoping.Rows[x + 4].Cells[44].Value = "";
            Stoping.Rows[x + 4].Cells[45].Value = "";
            Stoping.Rows[x + 4].Cells[46].Value = "";
            Stoping.Rows[x + 4].Cells[47].Value = "";
            Stoping.Rows[x + 4].Cells[48].Value = "";
            Stoping.Rows[x + 4].Cells[49].Value = "";

            Stoping.Rows[x + 5].Cells[1].Value = "";
            Stoping.Rows[x + 5].Cells[2].Value = "";
            Stoping.Rows[x + 5].Cells[3].Value = "";

            Stoping.Rows[x + 5].Cells[44].Value = "";
            Stoping.Rows[x + 5].Cells[45].Value = "";
            Stoping.Rows[x + 5].Cells[46].Value = "";
            Stoping.Rows[x + 5].Cells[47].Value = "";
            Stoping.Rows[x + 5].Cells[48].Value = "";
            Stoping.Rows[x + 5].Cells[49].Value = "";

            Stoping.Rows[x + 6].Cells[1].Value = "";
            Stoping.Rows[x + 6].Cells[2].Value = "";
            Stoping.Rows[x + 6].Cells[3].Value = "";

            Stoping.Rows[x + 6].Cells[44].Value = "";
            Stoping.Rows[x + 6].Cells[45].Value = "";
            Stoping.Rows[x + 6].Cells[46].Value = "";
            Stoping.Rows[x + 6].Cells[47].Value = "";
            Stoping.Rows[x + 6].Cells[48].Value = "";
            Stoping.Rows[x + 6].Cells[49].Value = "";

            Stoping.Rows[x + 7].Cells[1].Value = "";
            Stoping.Rows[x + 7].Cells[2].Value = "";
            Stoping.Rows[x + 7].Cells[3].Value = "";

            Stoping.Rows[x + 7].Cells[44].Value = "";
            Stoping.Rows[x + 7].Cells[45].Value = "";
            Stoping.Rows[x + 7].Cells[46].Value = "";
            Stoping.Rows[x + 7].Cells[47].Value = "";
            Stoping.Rows[x + 7].Cells[48].Value = "";
            Stoping.Rows[x + 7].Cells[49].Value = "";

            Stoping.Rows[x + 8].Cells[1].Value = "";
            Stoping.Rows[x + 8].Cells[2].Value = "";
            Stoping.Rows[x + 8].Cells[3].Value = "";

            Stoping.Rows[x + 8].Cells[44].Value = "";
            Stoping.Rows[x + 8].Cells[45].Value = "";
            Stoping.Rows[x + 8].Cells[46].Value = "";
            Stoping.Rows[x + 8].Cells[47].Value = "";
            Stoping.Rows[x + 8].Cells[48].Value = "";
            Stoping.Rows[x + 8].Cells[49].Value = "";

            Stoping.Rows[x + 1].Cells[50].Value = "";
            Stoping.Rows[x + 2].Cells[50].Value = "";
            Stoping.Rows[x + 3].Cells[50].Value = "";
            Stoping.Rows[x + 4].Cells[50].Value = "";
            Stoping.Rows[x + 5].Cells[50].Value = "";
            Stoping.Rows[x + 6].Cells[50].Value = "";
            Stoping.Rows[x + 7].Cells[50].Value = "";
            Stoping.Rows[x + 8].Cells[50].Value = "";

            Stoping.Rows[x + 1].Cells[51].Value = "";
            Stoping.Rows[x + 2].Cells[51].Value = "";
            Stoping.Rows[x + 3].Cells[51].Value = "";
            Stoping.Rows[x + 4].Cells[51].Value = "";
            Stoping.Rows[x + 5].Cells[51].Value = "";
            Stoping.Rows[x + 6].Cells[51].Value = "";
            Stoping.Rows[x + 7].Cells[51].Value = "";
            Stoping.Rows[x + 8].Cells[51].Value = "";


            Stoping.Rows[x + 1].Cells[52].Value = "";
            Stoping.Rows[x + 2].Cells[52].Value = "";
            Stoping.Rows[x + 3].Cells[52].Value = "";
            Stoping.Rows[x + 4].Cells[52].Value = "";
            Stoping.Rows[x + 5].Cells[52].Value = "";
            Stoping.Rows[x + 6].Cells[52].Value = "";
            Stoping.Rows[x + 7].Cells[52].Value = "";
            Stoping.Rows[x + 8].Cells[52].Value = "";

            Stoping.Rows[x + 1].Cells[53].Value = "      ";
            Stoping.Rows[x + 2].Cells[53].Value = "      ";
            Stoping.Rows[x + 3].Cells[53].Value = "      ";
            Stoping.Rows[x + 4].Cells[53].Value = "      ";
            Stoping.Rows[x + 5].Cells[53].Value = "      ";
            Stoping.Rows[x + 6].Cells[53].Value = "      ";
            Stoping.Rows[x + 7].Cells[53].Value = "      ";
            Stoping.Rows[x + 8].Cells[53].Value = "      ";



            int prog = 0;
            int booking = 0;
            int AdjBooking = 0;
            int Recon = 0;
            int progRecon = 0;

            for (int y = 4; y < 44; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "090 ";
                Stoping.Rows[x].Cells[y].Value = "090 ";

                Stoping.Rows[x + 1].Cells[y].Value = "002 ";
                Stoping.Rows[x + 2].Cells[y].Value = "002 ";
                Stoping.Rows[x + 3].Cells[y].Value = "002 ";
                Stoping.Rows[x + 4].Cells[y].Value = "002 ";
                Stoping.Rows[x + 5].Cells[y].Value = "002 ";
                Stoping.Rows[x + 6].Cells[y].Value = "002 ";
                Stoping.Rows[x + 7].Cells[y].Value = "002 ";
                Stoping.Rows[x + 8].Cells[y].Value = "002 ";

                if (Stoping.Rows[457].Cells[y].Value != null)
                {
                    // Stoping.Rows[x + 2].Cells[y].Value = "002" + Stoping.Rows[456].Cells[y].Value;
                    //// Stoping.Rows[x + 4].Cells[y].Value = "002" + Stoping.Rows[460].Cells[y].Value;

                    // Stoping.Rows[x + 6].Cells[y].Value = "002" + Stoping.Rows[458].Cells[y].Value;
                    // Stoping.Rows[x + 8].Cells[y].Value = "002" + Stoping.Rows[459].Cells[y].Value;
                }

                if (Stoping.Rows[456].Cells[y].Value != null)
                {
                    Recon = Convert.ToInt16(Stoping.Rows[456].Cells[y].Value);
                    booking = Convert.ToInt16(Stoping.Rows[451].Cells[y].Value);
                    //Recon = Recon + booking;
                    prog = prog + booking;

                    progRecon = Recon + progRecon;

                    AdjBooking = prog + progRecon;

                    if (1 == 1)//(Stoping[y, x - 4].Style.BackColor == Color.SkyBlue)
                    {
                        Stoping.Rows[x - 1].Cells[y].Value = "021" + Stoping.Rows[451].Cells[y].Value;
                        //Stoping.Rows[x - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        Stoping.Rows[x].Cells[y].Value = "022" + Stoping.Rows[456].Cells[y].Value;
                        // Stoping.Rows[x + 6].Cells[y].Value = "002" + Stoping.Rows[456].Cells[y].Value;

                        //Recon = Convert.ToInt16(Stoping.Rows[456].Cells[y].Value); 

                        Stoping.Rows[x + 2].Cells[y].Value = "002" + Stoping.Rows[451].Cells[y].Value;



                        //Recon = Recon + booking;



                        //prog = prog + booking;

                        //AdjBooking = Recon - booking;

                        Stoping.Rows[x + 4].Cells[y].Value = "002" + prog;


                        Stoping.Rows[x + 8].Cells[y].Value = "002" + AdjBooking;
                        //Stoping.Rows[x].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        //Stoping[y, x].Style.ForeColor = Color.Black;
                        if (Stoping.Rows[456].Cells[y].Value.ToString() != "" || Stoping.Rows[456].Cells[y].Value.ToString() !=  "0" || Stoping.Rows[456].Cells[y].Value.ToString() != null)
                        {
                            string aa = "";
                            if (Stoping.Columns[y].HeaderText.ToString() != "")
                            {
                                aa = Stoping.Columns[y].HeaderText.ToString().Substring(7, 3);
                            }
                            if (Convert.ToDecimal(Stoping.Rows[456].Cells[y].Value.ToString()) < 0)
                            {

                                Stoping.Rows[x].Cells[y].Value = "122" + Stoping.Rows[456].Cells[y].Value;
                                // Stoping.Rows[x + 6].Cells[y].Value = "102" + Stoping.Rows[456].Cells[y].Value;
                                if (aa == SysSettings.CheckMeas)
                                {
                                    if (progRecon < 0)
                                        Stoping.Rows[x + 6].Cells[y].Value = "102" + progRecon;
                                    else
                                        Stoping.Rows[x + 6].Cells[y].Value = "002" + progRecon;
                                }

                            }
                            else
                            {
                                if (aa == SysSettings.CheckMeas)
                                {
                                    if (progRecon < 0)
                                        Stoping.Rows[x + 6].Cells[y].Value = "102" + progRecon;
                                    else
                                        Stoping.Rows[x + 6].Cells[y].Value = "002" + progRecon;
                                }

                            }

                        }

                        if (AdjBooking.ToString() != "")
                        {
                            if (Convert.ToDecimal(AdjBooking) < 0)
                            {

                                Stoping.Rows[x + 8].Cells[y].Value = "102" + AdjBooking;
                            }
                            else
                            {
                                Stoping.Rows[x + 8].Cells[y].Value = "702" + AdjBooking;
                            }
                        }

                    }
                    else
                    {
                        Stoping.Rows[x].Cells[y].Value = "092" + Stoping.Rows[451].Cells[y].Value;

                        Stoping.Rows[x + 2].Cells[y].Value = "002" + Stoping.Rows[451].Cells[y].Value;

                        // booking = Convert.ToInt16(Stoping.Rows[451].Cells[y].Value);

                        // prog = prog + booking;

                        Stoping.Rows[x + 4].Cells[y].Value = "002" + prog;


                        if (AdjBooking.ToString() != "")
                        {
                            if (Convert.ToDecimal(AdjBooking) < 0)
                            {

                                Stoping.Rows[x + 8].Cells[y].Value = "102" + AdjBooking;
                            }
                            else
                            {
                                Stoping.Rows[x + 8].Cells[y].Value = "702" + AdjBooking;
                            }
                        }



                    }
                }
            }

            //int prog = 0;
            //int booking = 0;
            //int AdjBooking = 0;
            //int Recon = 0;
            //progRecon = 0;

            ///////New Colors///////
            for (int y = 55; y <= 95; y++)
            {
                Stoping.Rows[x - 1].Cells[y].Value = "090 ";
                Stoping.Rows[x].Cells[y].Value = "090 ";

                Stoping.Rows[x + 1].Cells[y].Value = "002 ";
                Stoping.Rows[x + 2].Cells[y].Value = "002 ";
                Stoping.Rows[x + 3].Cells[y].Value = "002 ";
                Stoping.Rows[x + 4].Cells[y].Value = "002 ";
                Stoping.Rows[x + 5].Cells[y].Value = "002 ";
                Stoping.Rows[x + 6].Cells[y].Value = "002 ";
                Stoping.Rows[x + 7].Cells[y].Value = "002 ";
                Stoping.Rows[x + 8].Cells[y].Value = "002 ";

                if (Stoping.Rows[457].Cells[y].Value != null)
                {
                    // Stoping.Rows[x + 2].Cells[y].Value = "002" + Stoping.Rows[456].Cells[y].Value;
                    //// Stoping.Rows[x + 4].Cells[y].Value = "002" + Stoping.Rows[460].Cells[y].Value;

                    // Stoping.Rows[x + 6].Cells[y].Value = "002" + Stoping.Rows[458].Cells[y].Value;
                    // Stoping.Rows[x + 8].Cells[y].Value = "002" + Stoping.Rows[459].Cells[y].Value;
                }

                if (Stoping.Rows[456].Cells[y].Value != null)
                {
                    if (1 == 1)//(Stoping[y, x - 4].Style.BackColor == Color.SkyBlue)
                    {
                        Stoping.Rows[x - 1].Cells[y].Value = "021" + Stoping.Rows[451].Cells[y].Value;
                        //Stoping.Rows[x - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        Stoping.Rows[x].Cells[y].Value = "022" + Stoping.Rows[456].Cells[y].Value;
                        // Stoping.Rows[x + 6].Cells[y].Value = "002" + Stoping.Rows[456].Cells[y].Value;

                        //Recon = Convert.ToInt16(Stoping.Rows[456].Cells[y].Value); 

                        Stoping.Rows[x + 2].Cells[y].Value = "002" + Stoping.Rows[451].Cells[y].Value;



                        //Recon = Recon + booking;



                        //prog = prog + booking;

                        //AdjBooking = Recon - booking;

                        Stoping.Rows[x + 4].Cells[y].Value = "002" + prog;


                        Stoping.Rows[x + 8].Cells[y].Value = "002" + AdjBooking;
                        //Stoping.Rows[x].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        //Stoping[y, x].Style.ForeColor = Color.Black;
                        if (Stoping.Rows[456].Cells[y].Value.ToString() != "" || Stoping.Rows[456].Cells[y].Value.ToString() != "0")
                        {
                            string aa = "";
                            if (Stoping.Columns[y].HeaderText.ToString() != "")
                            {
                                aa = Stoping.Columns[y].HeaderText.ToString().Substring(7, 3);
                            }
                            if (Convert.ToDecimal(Stoping.Rows[456].Cells[y].Value.ToString()) < 0)
                            {

                                Stoping.Rows[x].Cells[y].Value = "122" + Stoping.Rows[456].Cells[y].Value;
                                // Stoping.Rows[x + 6].Cells[y].Value = "102" + Stoping.Rows[456].Cells[y].Value;
                                if (aa == SysSettings.CheckMeas)
                                {
                                    if (progRecon < 0)
                                        Stoping.Rows[x + 6].Cells[y].Value = "102" + progRecon;
                                    else
                                        Stoping.Rows[x + 6].Cells[y].Value = "002" + progRecon;
                                }

                            }
                            else
                            {
                                if (aa == SysSettings.CheckMeas)
                                {
                                    if (progRecon < 0)
                                        Stoping.Rows[x + 6].Cells[y].Value = "102" + progRecon;
                                    else
                                        Stoping.Rows[x + 6].Cells[y].Value = "002" + progRecon;
                                }

                            }
                        }

                        if (AdjBooking.ToString() != "")
                        {
                            if (Convert.ToDecimal(AdjBooking) < 0)
                            {

                                Stoping.Rows[x + 8].Cells[y].Value = "102" + AdjBooking;
                            }
                            else
                            {
                                Stoping.Rows[x + 8].Cells[y].Value = "702" + AdjBooking;
                            }
                        }

                    }
                    else
                    {
                        Stoping.Rows[x].Cells[y].Value = "092" + Stoping.Rows[451].Cells[y].Value;

                        Stoping.Rows[x + 2].Cells[y].Value = "002" + Stoping.Rows[451].Cells[y].Value;

                        // booking = Convert.ToInt16(Stoping.Rows[451].Cells[y].Value);

                        // prog = prog + booking;

                        Stoping.Rows[x + 4].Cells[y].Value = "002" + prog;


                        if (AdjBooking.ToString() != "")
                        {
                            if (Convert.ToDecimal(AdjBooking) < 0)
                            {

                                Stoping.Rows[x + 8].Cells[y].Value = "102" + AdjBooking;
                            }
                            else
                            {
                                Stoping.Rows[x + 8].Cells[y].Value = "702" + AdjBooking;
                            }
                        }
                    }
                }
            }


            Stoping.RowCount = x + 12;



            int progplan = 0;
            int progplanTot = 0;
            int progbook = 0;
            int progbookTot = 0;
            Decimal MonthCall = 0;
            Decimal MonthCallTot = 0;

            int mofc1 = 0;
            int mofc1tot = 0;

            int mofc1a = 0;
            int mofc1tota = 0;

            int FC = 0;
            int FCTot = 0;
            int pum = 0;
            int pumtot = 0;

            for (int y = 0; y < x; y++)
            {
                Stoping.Rows[y].Height = 13;

                //do forcast
                if (Stoping.Rows[y].Cells[45].Value != null)
                {
                    if (Stoping.Rows[y].Cells[45].Value.ToString() != "")
                    {
                        Stoping.Rows[y].Cells[48].Value = (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) / Convert.ToDecimal(CurrShftLbl) * Convert.ToDecimal(TotShiftLbl)).ToString("0");
                        Stoping.Rows[y].Cells[48].Value = Math.Round((Convert.ToDecimal(Stoping.Rows[y].Cells[48].Value)), 1);
                        string aa = Stoping.Rows[y].Cells[44].Value.ToString();


                        try
                        {
                            Stoping.Rows[y].Cells[46].Value = (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(aa));
                        }
                        catch
                        {
                            Stoping.Rows[y].Cells[46].Value = (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(0));
                        }
                        decimal cc = 0;
                        try
                        {
                            if (aa != "0")
                            {
                                cc = (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(aa.Substring(0, 2)));
                            }
                        }
                        catch
                        {
                            try
                            {
                                cc = (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(aa));
                            }
                            catch
                            {
                                cc = (Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value));
                            }
                        }

                        if ((Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(cc)) < 0)
                        {
                            Stoping.Rows[y].Cells[46].Value = "1" + Stoping.Rows[y].Cells[46].Value;
                        }
                        else
                        {
                            Stoping.Rows[y].Cells[46].Value = "0" + Stoping.Rows[y].Cells[46].Value;

                        }

                        
                        //Stoping.Rows[y].Cells[44].Value = "0";
                        //}
                        try
                        {
                            if (Stoping.Rows[y].Cells[44].Value.ToString().Substring(0, 1) == "0")
                            {
                                Stoping.Rows[y].Cells[44].Value = "0";
                            }
                            else
                            {
                                progplan = progplan + Convert.ToInt32(Stoping.Rows[y].Cells[44].Value);
                                progplanTot = progplanTot + Convert.ToInt32(Stoping.Rows[y].Cells[44].Value);
                            }
                        }
                        catch
                        {
                            progplan = progplan + Convert.ToInt32(0);
                            progplanTot = progplanTot + Convert.ToInt32(0);
                        }
                        //progplan = progplan + Convert.ToInt32(Stoping.Rows[y].Cells[44].Value);
                        //progplanTot = progplanTot + Convert.ToInt32(Stoping.Rows[y].Cells[44].Value);

                        progbook = progbook + Convert.ToInt32(Stoping.Rows[y].Cells[45].Value);
                        progbookTot = progbookTot + Convert.ToInt32(Stoping.Rows[y].Cells[45].Value);

                        MonthCall = MonthCall + Convert.ToDecimal(Stoping.Rows[y].Cells[47].Value);
                        MonthCallTot = MonthCallTot + Convert.ToDecimal(Stoping.Rows[y].Cells[47].Value);

                        FC = FC + Convert.ToInt32(Stoping.Rows[y].Cells[48].Value);
                        FCTot = FCTot + Convert.ToInt32(Stoping.Rows[y].Cells[48].Value);
                        mofc1 = mofc1 + Convert.ToInt32(Stoping.Rows[y].Cells[49].Value);
                        mofc1tot = mofc1tot + Convert.ToInt32(Stoping.Rows[y].Cells[49].Value);
                        //p//um = pum + Convert.ToInt32(Stoping.Rows[y].Cells[50].Value);
                        //pumtot = pumtot + Convert.ToInt32(Stoping.Rows[y].Cells[50].Value);

                        try
                        {
                            if (Stoping.Rows[y].Cells[50].Value.ToString() != "")
                            {
                                mofc1a = mofc1a + Convert.ToInt32(Stoping.Rows[y].Cells[50].Value);
                                mofc1tota = mofc1tota + Convert.ToInt32(Stoping.Rows[y].Cells[50].Value);

                            }
                        }
                        catch { Stoping.Rows[y].Cells[50].Value = "0"; }


                    }
                }
                if (Stoping.Rows[y].Cells[0].Value != null)
                {
                    if (Stoping.Rows[y].Cells[0].Value.ToString() != "")
                    {
                        Stoping.Rows[y].Cells[44].Value = progplan.ToString();
                        Stoping.Rows[y].Cells[45].Value = progbook.ToString();
                        Stoping.Rows[y].Cells[46].Value = (progbook - progplan).ToString();
                        Stoping.Rows[y].Cells[47].Value = Math.Round(MonthCall, 0);
                        Stoping.Rows[y].Cells[48].Value = FC.ToString();
                        Stoping.Rows[y].Cells[49].Value = mofc1.ToString();
                        Stoping.Rows[y].Cells[50].Value = mofc1a.ToString();

                        if (progbook - progplan < 0)
                        {
                            Stoping[46, y].Style.ForeColor = System.Drawing.Color.Red;
                        }
                        if ((Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[y].Cells[44].Value)) < 0)
                        {
                            Stoping.Rows[y].Cells[46].Value = "1" + Stoping.Rows[y].Cells[46].Value;
                        }
                        else
                        {
                            Stoping.Rows[y].Cells[46].Value = "0" + Stoping.Rows[y].Cells[46].Value;

                        }
                        progplan = 0;
                        progbook = 0;
                        MonthCall = 0;
                        FC = 0;
                        mofc1 = 0;
                        mofc1a = 0;
                        pum = 0;
                    }
                }

            }

            Stoping.Rows[x - 1].Cells[44].Value = "";
            Stoping.Rows[x - 1].Cells[45].Value = "";
            Stoping.Rows[x - 1].Cells[46].Value = "";
            Stoping.Rows[x - 1].Cells[47].Value = "";
            Stoping.Rows[x - 1].Cells[48].Value = "";
            Stoping.Rows[x - 1].Cells[49].Value = "";
            Stoping.Rows[x - 1].Cells[50].Value = "";
            Stoping.Rows[x - 1].Cells[51].Value = "";
            Stoping.Rows[x - 1].Cells[52].Value = "";
            Stoping.Rows[x - 1].Cells[53].Value = "      ";

            ////////New Colors///////////
            //for (int i = 55; i < 95; i++)
            //{
            //    Stoping.Rows[x - 1].Cells[i].Value = "";
            //    Stoping.Rows[x].Cells[i].Value = "";
            //}

            Stoping.Rows[x].Cells[44].Value = progplanTot.ToString();
            Stoping.Rows[x].Cells[45].Value = progbookTot.ToString();
            Stoping.Rows[x].Cells[46].Value = (progbookTot - progplanTot).ToString();
            Stoping.Rows[x].Cells[47].Value = Math.Round(MonthCallTot, 0);
            Stoping.Rows[x].Cells[48].Value = FCTot.ToString();
            Stoping.Rows[x].Cells[49].Value = mofc1tot.ToString();
            Stoping.Rows[x].Cells[50].Value = mofc1tota.ToString();

            if ((Convert.ToDecimal(Stoping.Rows[x].Cells[45].Value) - Convert.ToDecimal(Stoping.Rows[x].Cells[44].Value)) < 0)
            {
                Stoping.Rows[x].Cells[46].Value = "1" + Stoping.Rows[x].Cells[46].Value;
            }
            else
            {
                Stoping.Rows[x].Cells[46].Value = "0" + Stoping.Rows[x].Cells[46].Value;

            }




            Stoping.Rows[x + 8].Cells[50].Value = (Convert.ToDecimal(progbookTot) / (Convert.ToDecimal(progplanTot) + Convert.ToDecimal(0.000001))).ToString();


            // for (int y = 0; y < x+1; y++)
            // {
            //     if (Stoping.Rows[y].Cells[49].Value != "")
            //         Stoping.Rows[y].Cells[50].Value = Math.Round(((Convert.ToDecimal(Stoping.Rows[y].Cells[47].Value) - Convert.ToDecimal(Stoping.Rows[y].Cells[44].Value)) * Convert.ToDecimal(Stoping.Rows[x + 8].Cells[50].Value)) + Convert.ToDecimal(Stoping.Rows[y].Cells[45].Value), 0);

            // }

            Stoping.Rows[x + 8].Cells[50].Value = "";

            //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            //_dbMan1.ConnectionString = ConfigurationSettings.AppSettings["SQLConnectionStr"];
            //_dbMan1.SqlStatement = "delete from Temp_MoDailyHeading where userid = '" + clsUserInfo.UserID.ToString() + "'     insert into Temp_MoDailyHeading values('" + clsUserInfo.UserID.ToString() + "','" + Stoping.Columns[0].HeaderText.ToString() + "',  '" + Stoping.Columns[1].HeaderText.ToString() + "' ,";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + " '" + Stoping.Columns[3].HeaderText.ToString() + "','" + Stoping.Columns[4].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[5].HeaderText.ToString() + "', '" + Stoping.Columns[6].HeaderText.ToString() + "','" + Stoping.Columns[7].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[8].HeaderText.ToString() + "', '" + Stoping.Columns[9].HeaderText.ToString() + "','" + Stoping.Columns[10].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[11].HeaderText.ToString() + "', '" + Stoping.Columns[12].HeaderText.ToString() + "','" + Stoping.Columns[13].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[14].HeaderText.ToString() + "', '" + Stoping.Columns[15].HeaderText.ToString() + "','" + Stoping.Columns[16].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[17].HeaderText.ToString() + "', '" + Stoping.Columns[18].HeaderText.ToString() + "','" + Stoping.Columns[19].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[20].HeaderText.ToString() + "', '" + Stoping.Columns[21].HeaderText.ToString() + "','" + Stoping.Columns[22].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[23].HeaderText.ToString() + "', '" + Stoping.Columns[24].HeaderText.ToString() + "','" + Stoping.Columns[25].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[26].HeaderText.ToString() + "', '" + Stoping.Columns[27].HeaderText.ToString() + "','" + Stoping.Columns[28].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[29].HeaderText.ToString() + "', '" + Stoping.Columns[30].HeaderText.ToString() + "','" + Stoping.Columns[31].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[32].HeaderText.ToString() + "', '" + Stoping.Columns[33].HeaderText.ToString() + "','" + Stoping.Columns[34].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[35].HeaderText.ToString() + "', '" + Stoping.Columns[36].HeaderText.ToString() + "','" + Stoping.Columns[37].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[38].HeaderText.ToString() + "', '" + Stoping.Columns[39].HeaderText.ToString() + "','" + Stoping.Columns[40].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[41].HeaderText.ToString() + "','" + Stoping.Columns[42].HeaderText.ToString() + "','" + Stoping.Columns[43].HeaderText.ToString() + "','" + Stoping.Columns[44].HeaderText.ToString() + "', '" + Stoping.Columns[45].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[47].HeaderText.ToString() + "', '" + Stoping.Columns[48].HeaderText.ToString() + "','" + Stoping.Columns[49].HeaderText.ToString() + "' , ";
            //_dbMan1.SqlStatement = _dbMan1.SqlStatement + "'" + Stoping.Columns[46].HeaderText.ToString() + "','" + ProdMonth1Txt.Text.ToString() + "','" + cmbSections.Text.ToString() + "','" + Stoping.Columns[2].HeaderText.ToString() + "', '" + "Risk Rating" + "' )";

            //_dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan1.queryReturnType = MWDataManager.ReturnType.longNumber;
            //_dbMan1.ExecuteInstruction();



            //MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
            //_dbMan2.ConnectionString = ConfigurationSettings.AppSettings["SQLConnectionStr"];
            //_dbMan2.SqlStatement = "delete from Temp_MODailyDataStoping where userid = '" + clsUserInfo.UserID.ToString() + "' ";
            //// Stoping.RowCount-10 
            //for (int y = 0; y < Stoping.RowCount - 3; y++)
            //{
            //    if (Stoping.Rows[y].Cells[1].Value != null)
            //    {

            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "insert into Temp_MODailyDataStoping values('" + clsUserInfo.UserID.ToString() + "','" + y.ToString() + "','" + Stoping.Rows[y].Cells[0].Value + "',  '" + Stoping.Rows[y].Cells[1].Value + "' ,";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + " '" + Stoping.Rows[y].Cells[3].Value.ToString() + "','" + Stoping.Rows[y].Cells[4].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[5].Value.ToString() + "', '" + Stoping.Rows[y].Cells[6].Value.ToString() + "','" + Stoping.Rows[y].Cells[7].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[8].Value.ToString() + "', '" + Stoping.Rows[y].Cells[9].Value.ToString() + "','" + Stoping.Rows[y].Cells[10].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[11].Value.ToString() + "', '" + Stoping.Rows[y].Cells[12].Value.ToString() + "','" + Stoping.Rows[y].Cells[13].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[14].Value.ToString() + "', '" + Stoping.Rows[y].Cells[15].Value.ToString() + "','" + Stoping.Rows[y].Cells[16].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[17].Value.ToString() + "', '" + Stoping.Rows[y].Cells[18].Value.ToString() + "','" + Stoping.Rows[y].Cells[19].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[20].Value.ToString() + "', '" + Stoping.Rows[y].Cells[21].Value.ToString() + "','" + Stoping.Rows[y].Cells[22].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[23].Value.ToString() + "', '" + Stoping.Rows[y].Cells[24].Value.ToString() + "','" + Stoping.Rows[y].Cells[25].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[26].Value.ToString() + "', '" + Stoping.Rows[y].Cells[27].Value.ToString() + "','" + Stoping.Rows[y].Cells[28].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[29].Value.ToString() + "', '" + Stoping.Rows[y].Cells[30].Value.ToString() + "','" + Stoping.Rows[y].Cells[31].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[32].Value.ToString() + "', '" + Stoping.Rows[y].Cells[33].Value.ToString() + "','" + Stoping.Rows[y].Cells[34].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[35].Value.ToString() + "', '" + Stoping.Rows[y].Cells[36].Value.ToString() + "','" + Stoping.Rows[y].Cells[37].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[38].Value.ToString() + "', '" + Stoping.Rows[y].Cells[39].Value.ToString() + "','" + Stoping.Rows[y].Cells[40].Value + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[41].Value.ToString() + "', '" + Stoping.Rows[y].Cells[42].Value.ToString() + "','" + Stoping.Rows[y].Cells[43].Value.ToString() + "', '" + Stoping.Rows[y].Cells[44].Value.ToString() + "', '" + Stoping.Rows[y].Cells[45].Value.ToString() + "', ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[47].Value.ToString() + "', '" + Stoping.Rows[y].Cells[48].Value.ToString() + "','" + Stoping.Rows[y].Cells[49].Value.ToString() + "' , ";
            //        _dbMan2.SqlStatement = _dbMan2.SqlStatement + "'" + Stoping.Rows[y].Cells[46].Value.ToString() + "','" + Stoping.Rows[y].Cells[50].Value.ToString() + "' ,'" + SysSettings.Banner + "','" + Stoping.Rows[y].Cells[2].Value.ToString() + "', '" + Stoping.Rows[y].Cells[51].Value.ToString() + "') ";
            //    }
            //}
            //_dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan2.queryReturnType = MWDataManager.ReturnType.longNumber;
            //_dbMan2.ExecuteInstruction();

            Report theReport = new Report();


            MWDataManager.clsDataAccess _dbManMOSec = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbManMOSec.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManMOSec.SqlStatement = " select  Distinct SectionID_2+':'+Name_2 MOSec from Section_Complete " +
                                " Where SectionID_2 = '" + Section + "' " +
                                " and prodmonth = '"+month2+"'" +
                             
                                " ";
            _dbManMOSec.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManMOSec.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManMOSec.ResultsTableName = "MODaily_Stoping_Headings";
            _dbManMOSec.ExecuteInstruction();

            string MOSec = _dbManMOSec.ResultsDataTable.Rows[0][0].ToString();

            MWDataManager.clsDataAccess _dbManStopingHeading = new MWDataManager.clsDataAccess();
            //if (month2 != "201901")
            //{
              
                //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
                _dbManStopingHeading.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManStopingHeading.SqlStatement = "select (select a_Color from SysSet) a, (select s_Color from SysSet) b " +
                                    ",(select b_Color from SysSet)  s , " +
                                    "'" + clsUserInfo.UserID.ToString() + "' userid,'" + MOSec + "' section,'" + SysSettings.Banner + "' banner,  '" + Stoping.Columns[1].HeaderText.ToString() + "' orgunit ," +
                                    "'" + Stoping.Columns[3].HeaderText.ToString() + "' FL,'" + Stoping.Columns[4].HeaderText.ToString() + "' Col1, " +
                                    "'" + Stoping.Columns[5].HeaderText.ToString() + "' Col2, '" + Stoping.Columns[6].HeaderText.ToString() + "' Col3,'" + Stoping.Columns[7].HeaderText.ToString() + "' Col4, " +
                                    "'" + Stoping.Columns[8].HeaderText.ToString() + "' Col5, '" + Stoping.Columns[9].HeaderText.ToString() + "' Col6,'" + Stoping.Columns[10].HeaderText.ToString() + "' Col7, " +
                                    "'" + Stoping.Columns[11].HeaderText.ToString() + "' Col8, '" + Stoping.Columns[12].HeaderText.ToString() + "' Col9,'" + Stoping.Columns[13].HeaderText.ToString() + "' Col10, " +
                                    "'" + Stoping.Columns[14].HeaderText.ToString() + "' Col11, '" + Stoping.Columns[15].HeaderText.ToString() + "' Col12,'" + Stoping.Columns[16].HeaderText.ToString() + "' Col13, " +
                                    "'" + Stoping.Columns[17].HeaderText.ToString() + "' Col14, '" + Stoping.Columns[18].HeaderText.ToString() + "' Col15,'" + Stoping.Columns[19].HeaderText.ToString() + "' Col16, " +
                                    "'" + Stoping.Columns[20].HeaderText.ToString() + "' Col17, '" + Stoping.Columns[21].HeaderText.ToString() + "' Col18,'" + Stoping.Columns[22].HeaderText.ToString() + "' Col19, " +
                                    "'" + Stoping.Columns[23].HeaderText.ToString() + "' Col20, '" + Stoping.Columns[24].HeaderText.ToString() + "' Col21,'" + Stoping.Columns[25].HeaderText.ToString() + "' Col22, " +
                                    "'" + Stoping.Columns[26].HeaderText.ToString() + "' Col23, '" + Stoping.Columns[27].HeaderText.ToString() + "' Col24,'" + Stoping.Columns[28].HeaderText.ToString() + "' Col25, " +
                                    "'" + Stoping.Columns[29].HeaderText.ToString() + "' Col26, '" + Stoping.Columns[30].HeaderText.ToString() + "' Col27,'" + Stoping.Columns[31].HeaderText.ToString() + "' Col28, " +
                                    "'" + Stoping.Columns[32].HeaderText.ToString() + "' Col29, '" + Stoping.Columns[33].HeaderText.ToString() + "' Col30,'" + Stoping.Columns[34].HeaderText.ToString() + "' Col31, " +
                                    "'" + Stoping.Columns[35].HeaderText.ToString() + "' Col32, '" + Stoping.Columns[36].HeaderText.ToString() + "' Col33,'" + Stoping.Columns[37].HeaderText.ToString() + "' Col34, " +
                                    "'" + Stoping.Columns[38].HeaderText.ToString() + "' Col35, '" + Stoping.Columns[39].HeaderText.ToString() + "' Col36,'" + Stoping.Columns[40].HeaderText.ToString() + "' Col37, " +
                                    "'" + Stoping.Columns[41].HeaderText.ToString() + "' Col38,'" + Stoping.Columns[42].HeaderText.ToString() + "' Col39,'" + Stoping.Columns[43].HeaderText.ToString() + "' Col40,'" + Stoping.Columns[44].HeaderText.ToString() + "' ProgPlan, '" + Stoping.Columns[45].HeaderText.ToString() + "' ProgBook, " +
                                    "'" + Stoping.Columns[47].HeaderText.ToString() + "' MonthCall, '" + Stoping.Columns[48].HeaderText.ToString() + "' MonthFC,'" + Stoping.Columns[49].HeaderText.ToString() + "' MOFC, " +
                                    "'" + Stoping.Columns[46].HeaderText.ToString() + "' Spare,'" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' Spare1,'" + reportSettings.SectionID + "' Colour,'" + Stoping.Columns[2].HeaderText.ToString() + "' Workplace, '" + "Plan Sqm" + "' RiskRating";
                _dbManStopingHeading.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManStopingHeading.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManStopingHeading.ResultsTableName = "MODaily_Stoping_Headings";
                _dbManStopingHeading.ExecuteInstruction();
            //}
            //else
            //{
               
            //        //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            //        _dbManStopingHeading.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    _dbManStopingHeading.SqlStatement = "select (select a_Color from SysSet) a, (select s_Color from SysSet) b " +
            //                        ",(select b_Color from SysSet)  s , " +
            //                        "'" + clsUserInfo.UserID.ToString() + "' userid,'" + MOSec + "' section,'" + SysSettings.Banner + "' banner,  '" + Stoping.Columns[1].HeaderText.ToString() + "' orgunit ," +
            //                        "'" + Stoping.Columns[3].HeaderText.ToString() + "' FL,'" + Stoping.Columns[4].HeaderText.ToString() + "' Col1, " +
            //                        "'" + Stoping.Columns[5].HeaderText.ToString() + "' Col2, '" + Stoping.Columns[6].HeaderText.ToString() + "' Col3,'" + Stoping.Columns[7].HeaderText.ToString() + "' Col4, " +
            //                        "'" + Stoping.Columns[8].HeaderText.ToString() + "' Col5, '" + Stoping.Columns[9].HeaderText.ToString() + "' Col6,'" + Stoping.Columns[10].HeaderText.ToString() + "' Col7, " +
            //                        "'" + Stoping.Columns[11].HeaderText.ToString() + "' Col8, '" + Stoping.Columns[12].HeaderText.ToString() + "' Col9,'" + Stoping.Columns[13].HeaderText.ToString() + "' Col10, " +
            //                        "'" + Stoping.Columns[14].HeaderText.ToString() + "' Col11, '" + Stoping.Columns[15].HeaderText.ToString() + "' Col12,'" + Stoping.Columns[16].HeaderText.ToString() + "' Col13, " +
            //                        "'" + Stoping.Columns[17].HeaderText.ToString() + "' Col14, '" + Stoping.Columns[18].HeaderText.ToString() + "' Col15,'" + Stoping.Columns[19].HeaderText.ToString() + "' Col16, " +
            //                        "'" + Stoping.Columns[20].HeaderText.ToString() + "' Col17, '" + Stoping.Columns[21].HeaderText.ToString() + "' Col18,'" + Stoping.Columns[22].HeaderText.ToString() + "' Col19, " +
            //                        "'" + Stoping.Columns[23].HeaderText.ToString() + "' Col20, '" + Stoping.Columns[24].HeaderText.ToString() + "' Col21,'" + Stoping.Columns[25].HeaderText.ToString() + "' Col22, " +
            //                        "'" + Stoping.Columns[26].HeaderText.ToString() + "' Col23, '" + Stoping.Columns[27].HeaderText.ToString() + "' Col24,'" + Stoping.Columns[28].HeaderText.ToString() + "' Col25, " +
            //                        "'" + Stoping.Columns[29].HeaderText.ToString() + "' Col26, '" + Stoping.Columns[30].HeaderText.ToString() + "' Col27,'" + Stoping.Columns[31].HeaderText.ToString() + "' Col28, " +
            //                        "'" + Stoping.Columns[32].HeaderText.ToString() + "' Col29, '" + Stoping.Columns[33].HeaderText.ToString() + "' Col30,'" + Stoping.Columns[34].HeaderText.ToString() + "' Col31, " +
            //                        "'" + Stoping.Columns[35].HeaderText.ToString() + "' Col32, '" + Stoping.Columns[36].HeaderText.ToString() + "' Col33,'" + Stoping.Columns[37].HeaderText.ToString() + "' Col34, " +
            //                        "'" + Stoping.Columns[38].HeaderText.ToString() + "' Col35, '" + Stoping.Columns[39].HeaderText.ToString() + "' Col36,'" + Stoping.Columns[40].HeaderText.ToString() + "' Col37, " +
            //                        "'" + Stoping.Columns[41].HeaderText.ToString() + "' Col38,'" + Stoping.Columns[42].HeaderText.ToString() + "' Col39,'" + Stoping.Columns[44].HeaderText.ToString() + "' Col40,'" + Stoping.Columns[45].HeaderText.ToString() + "' ProgPlan, '" + Stoping.Columns[46].HeaderText.ToString() + "' ProgBook, " +
            //                        "'" + Stoping.Columns[47].HeaderText.ToString() + "' MonthCall, '" + Stoping.Columns[48].HeaderText.ToString() + "' MonthFC,'" + Stoping.Columns[49].HeaderText.ToString() + "' MOFC, " +
            //                        "'" + Stoping.Columns[46].HeaderText.ToString() + "' Spare,'" + TProductionGlobal.ProdMonthAsString(reportSettings.Prodmonth) + "' Spare1,'" + reportSettings.SectionID + "' Colour,'" + Stoping.Columns[2].HeaderText.ToString() + "' Workplace, '" + "Risk Rating" + "' RiskRating";
            //    _dbManStopingHeading.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //    _dbManStopingHeading.queryReturnType = MWDataManager.ReturnType.DataTable;
            //    _dbManStopingHeading.ResultsTableName = "MODaily_Stoping_Headings";
            //    _dbManStopingHeading.ExecuteInstruction();
            //}

            MWDataManager.clsDataAccess _dbManStopingData = new MWDataManager.clsDataAccess();
            //_dbMan.ConnectionString = ConfigurationManager.AppSettings["SQLConnectionStr"];
            _dbManStopingData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            

            string ExtID = "";
            string wp1 = "               ";
            for (int y = 0; y < Stoping.RowCount - 3; y++)
            {
                if (Stoping.Rows[y].Cells[1].Value != null)
                {
                    //if (month2 != "201901")
                    //{
                        if (y > 0)
                            _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "Union ";

                        ExtID = "                                                                     ";
                        wp1 = "               ";
                        if (Stoping.Rows[y].Cells[52].Value != null)
                            ExtID = Stoping.Rows[y].Cells[52].Value.ToString();

                        if (Stoping.Rows[y].Cells[53].Value != null)
                            wp1 = Stoping.Rows[y].Cells[53].Value.ToString();

                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "Select '" + ExtID + "' Userid," + y + " Line,'" + Stoping.Rows[y].Cells[0].Value + "' Section,  '" + Stoping.Rows[y].Cells[1].Value + "' Orgunit,   \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + " '" + Stoping.Rows[y].Cells[3].Value.ToString() + "' FL,'" + Stoping.Rows[y].Cells[4].Value.ToString() + "' Col1, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[5].Value.ToString() + "' Col2, '" + Stoping.Rows[y].Cells[6].Value.ToString() + "' Col3,'" + Stoping.Rows[y].Cells[7].Value.ToString() + "' Col4, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[8].Value.ToString() + "' Col5, '" + Stoping.Rows[y].Cells[9].Value.ToString() + "' Col6,'" + Stoping.Rows[y].Cells[10].Value.ToString() + "' Col7, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[11].Value.ToString() + "' Col8, '" + Stoping.Rows[y].Cells[12].Value.ToString() + "' Col9,'" + Stoping.Rows[y].Cells[13].Value.ToString() + "' Col10, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[14].Value.ToString() + "' Col11, '" + Stoping.Rows[y].Cells[15].Value.ToString() + "' Col12,'" + Stoping.Rows[y].Cells[16].Value.ToString() + "' Col13, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[17].Value.ToString() + "' Col14, '" + Stoping.Rows[y].Cells[18].Value.ToString() + "' Col15,'" + Stoping.Rows[y].Cells[19].Value.ToString() + "' Col16, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[20].Value.ToString() + "' Col17, '" + Stoping.Rows[y].Cells[21].Value.ToString() + "' Col18,'" + Stoping.Rows[y].Cells[22].Value.ToString() + "' Col19, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[23].Value.ToString() + "' Col20, '" + Stoping.Rows[y].Cells[24].Value.ToString() + "' Col21,'" + Stoping.Rows[y].Cells[25].Value.ToString() + "' Col22, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[26].Value.ToString() + "' Col23, '" + Stoping.Rows[y].Cells[27].Value.ToString() + "' Col24,'" + Stoping.Rows[y].Cells[28].Value.ToString() + "' Col25, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[29].Value.ToString() + "' Col26, '" + Stoping.Rows[y].Cells[30].Value.ToString() + "' Col27,'" + Stoping.Rows[y].Cells[31].Value.ToString() + "' Col28, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[32].Value.ToString() + "' Col29, '" + Stoping.Rows[y].Cells[33].Value.ToString() + "' Col30,'" + Stoping.Rows[y].Cells[34].Value.ToString() + "' Col31, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[35].Value.ToString() + "' Col32, '" + Stoping.Rows[y].Cells[36].Value.ToString() + "' Col33,'" + Stoping.Rows[y].Cells[37].Value.ToString() + "' Col34, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[38].Value.ToString() + "' Col35, '" + Stoping.Rows[y].Cells[39].Value.ToString() + "' Col36,'" + Stoping.Rows[y].Cells[40].Value + "' Col37, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[41].Value.ToString() + "' Col38, '" + Stoping.Rows[y].Cells[42].Value.ToString() + "' Col39,'" + Stoping.Rows[y].Cells[43].Value.ToString() + "' Col40, '" + Stoping.Rows[y].Cells[44].Value.ToString() + "' ProgPlan, '" + Stoping.Rows[y].Cells[45].Value.ToString() + "' ProgBook, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[47].Value.ToString() + "' MonthCall, '" + Stoping.Rows[y].Cells[48].Value.ToString() + "' MonthFC,'" + Stoping.Rows[y].Cells[49].Value.ToString() + "' MOFC, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[46].Value.ToString() + "' Spare,'" + Stoping.Rows[y].Cells[50].Value.ToString() + "' Spare1, '" + wp1 + SysSettings.Banner + "' Colour,'" + Stoping.Rows[y].Cells[2].Value.ToString() + "' Workplace, '" + Stoping.Rows[y].Cells[51].Value.ToString() + "' RiskRating, '" + Stoping.Rows[y].Cells[54].Value.ToString() + "' NewWp \r\n";


                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "    ,'" + Stoping.Rows[y].Cells[55].Value.ToString() + "' Color1, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[56].Value.ToString() + "' Color2, '" + Stoping.Rows[y].Cells[57].Value.ToString() + "' Color3,'" + Stoping.Rows[y].Cells[58].Value.ToString() + "' Color4, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[59].Value.ToString() + "' Color5, '" + Stoping.Rows[y].Cells[60].Value.ToString() + "' Color6,'" + Stoping.Rows[y].Cells[61].Value.ToString() + "' Color7, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[62].Value.ToString() + "' Color8, '" + Stoping.Rows[y].Cells[63].Value.ToString() + "' Color9,'" + Stoping.Rows[y].Cells[64].Value.ToString() + "' Color10, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[65].Value.ToString() + "' Color11, '" + Stoping.Rows[y].Cells[64].Value.ToString() + "' Color12,'" + Stoping.Rows[y].Cells[67].Value.ToString() + "' Color13, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[68].Value.ToString() + "' Color14, '" + Stoping.Rows[y].Cells[69].Value.ToString() + "' Color15,'" + Stoping.Rows[y].Cells[70].Value.ToString() + "' Color16, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[71].Value.ToString() + "' Color17, '" + Stoping.Rows[y].Cells[72].Value.ToString() + "' Color18,'" + Stoping.Rows[y].Cells[73].Value.ToString() + "' Color19, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[74].Value.ToString() + "' Color20, '" + Stoping.Rows[y].Cells[75].Value.ToString() + "' Color21,'" + Stoping.Rows[y].Cells[76].Value.ToString() + "' Color22, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[77].Value.ToString() + "' Color23, '" + Stoping.Rows[y].Cells[78].Value.ToString() + "' Color24,'" + Stoping.Rows[y].Cells[79].Value.ToString() + "' Color25, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[80].Value.ToString() + "' Color26, '" + Stoping.Rows[y].Cells[81].Value.ToString() + "' Color27,'" + Stoping.Rows[y].Cells[82].Value.ToString() + "' Color28, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[83].Value.ToString() + "' Color29, '" + Stoping.Rows[y].Cells[84].Value.ToString() + "' Color30,'" + Stoping.Rows[y].Cells[85].Value.ToString() + "' Color31, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[86].Value.ToString() + "' Color32, '" + Stoping.Rows[y].Cells[87].Value.ToString() + "' Color33,'" + Stoping.Rows[y].Cells[88].Value.ToString() + "' Color34, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[89].Value.ToString() + "' Color35, '" + Stoping.Rows[y].Cells[90].Value.ToString() + "' Color36,'" + Stoping.Rows[y].Cells[91].Value + "' Color37, \r\n";
                        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[92].Value.ToString() + "' Color38, '" + Stoping.Rows[y].Cells[93].Value.ToString() + "' Color39,'" + Stoping.Rows[y].Cells[94].Value.ToString() + "' Color40 \r\n";
                //    }
                //    else
                //    {
                //        if (y > 0)
                //            _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "Union ";

                //        ExtID = "                                                                     ";
                //        wp1 = "               ";
                //        if (Stoping.Rows[y].Cells[52].Value != null)
                //            ExtID = Stoping.Rows[y].Cells[52].Value.ToString();

                //        if (Stoping.Rows[y].Cells[53].Value != null)
                //            wp1 = Stoping.Rows[y].Cells[53].Value.ToString();

                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "Select '" + ExtID + "' Userid," + y + " Line,'" + Stoping.Rows[y].Cells[0].Value + "' Section,  '" + Stoping.Rows[y].Cells[1].Value + "' Orgunit,   \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + " '" + Stoping.Rows[y].Cells[3].Value.ToString() + "' FL,'" + Stoping.Rows[y].Cells[4].Value.ToString() + "' Col1, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[5].Value.ToString() + "' Col2, '" + Stoping.Rows[y].Cells[6].Value.ToString() + "' Col3,'" + Stoping.Rows[y].Cells[7].Value.ToString() + "' Col4, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[8].Value.ToString() + "' Col5, '" + Stoping.Rows[y].Cells[9].Value.ToString() + "' Col6,'" + Stoping.Rows[y].Cells[10].Value.ToString() + "' Col7, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[11].Value.ToString() + "' Col8, '" + Stoping.Rows[y].Cells[12].Value.ToString() + "' Col9,'" + Stoping.Rows[y].Cells[13].Value.ToString() + "' Col10, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[14].Value.ToString() + "' Col11, '" + Stoping.Rows[y].Cells[15].Value.ToString() + "' Col12,'" + Stoping.Rows[y].Cells[16].Value.ToString() + "' Col13, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[17].Value.ToString() + "' Col14, '" + Stoping.Rows[y].Cells[18].Value.ToString() + "' Col15,'" + Stoping.Rows[y].Cells[19].Value.ToString() + "' Col16, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[20].Value.ToString() + "' Col17, '" + Stoping.Rows[y].Cells[21].Value.ToString() + "' Col18,'" + Stoping.Rows[y].Cells[22].Value.ToString() + "' Col19, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[23].Value.ToString() + "' Col20, '" + Stoping.Rows[y].Cells[24].Value.ToString() + "' Col21,'" + Stoping.Rows[y].Cells[25].Value.ToString() + "' Col22, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[26].Value.ToString() + "' Col23, '" + Stoping.Rows[y].Cells[27].Value.ToString() + "' Col24,'" + Stoping.Rows[y].Cells[28].Value.ToString() + "' Col25, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[29].Value.ToString() + "' Col26, '" + Stoping.Rows[y].Cells[30].Value.ToString() + "' Col27,'" + Stoping.Rows[y].Cells[31].Value.ToString() + "' Col28, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[32].Value.ToString() + "' Col29, '" + Stoping.Rows[y].Cells[33].Value.ToString() + "' Col30,'" + Stoping.Rows[y].Cells[34].Value.ToString() + "' Col31, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[35].Value.ToString() + "' Col32, '" + Stoping.Rows[y].Cells[36].Value.ToString() + "' Col33,'" + Stoping.Rows[y].Cells[37].Value.ToString() + "' Col34, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[38].Value.ToString() + "' Col35, '" + Stoping.Rows[y].Cells[39].Value.ToString() + "' Col36,'" + Stoping.Rows[y].Cells[40].Value + "' Col37, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[41].Value.ToString() + "' Col38, '" + Stoping.Rows[y].Cells[42].Value.ToString() + "' Col39,'" + Stoping.Rows[y].Cells[44].Value.ToString() + "' Col40, '" + Stoping.Rows[y].Cells[45].Value.ToString() + "' ProgPlan, '" + Stoping.Rows[y].Cells[46].Value.ToString() + "' ProgBook, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[47].Value.ToString() + "' MonthCall, '" + Stoping.Rows[y].Cells[48].Value.ToString() + "' MonthFC,'" + Stoping.Rows[y].Cells[49].Value.ToString() + "' MOFC, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[46].Value.ToString() + "' Spare,'" + Stoping.Rows[y].Cells[50].Value.ToString() + "' Spare1, '" + wp1 + SysSettings.Banner + "' Colour,'" + Stoping.Rows[y].Cells[2].Value.ToString() + "' Workplace, '" + Stoping.Rows[y].Cells[51].Value.ToString() + "' RiskRating, '" + Stoping.Rows[y].Cells[54].Value.ToString() + "' NewWp \r\n";


                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "    ,'" + Stoping.Rows[y].Cells[55].Value.ToString() + "' Color1, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[56].Value.ToString() + "' Color2, '" + Stoping.Rows[y].Cells[57].Value.ToString() + "' Color3,'" + Stoping.Rows[y].Cells[58].Value.ToString() + "' Color4, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[59].Value.ToString() + "' Color5, '" + Stoping.Rows[y].Cells[60].Value.ToString() + "' Color6,'" + Stoping.Rows[y].Cells[61].Value.ToString() + "' Color7, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[62].Value.ToString() + "' Color8, '" + Stoping.Rows[y].Cells[63].Value.ToString() + "' Color9,'" + Stoping.Rows[y].Cells[64].Value.ToString() + "' Color10, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[65].Value.ToString() + "' Color11, '" + Stoping.Rows[y].Cells[64].Value.ToString() + "' Color12,'" + Stoping.Rows[y].Cells[67].Value.ToString() + "' Color13, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[68].Value.ToString() + "' Color14, '" + Stoping.Rows[y].Cells[69].Value.ToString() + "' Color15,'" + Stoping.Rows[y].Cells[70].Value.ToString() + "' Color16, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[71].Value.ToString() + "' Color17, '" + Stoping.Rows[y].Cells[72].Value.ToString() + "' Color18,'" + Stoping.Rows[y].Cells[73].Value.ToString() + "' Color19, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[74].Value.ToString() + "' Color20, '" + Stoping.Rows[y].Cells[75].Value.ToString() + "' Color21,'" + Stoping.Rows[y].Cells[76].Value.ToString() + "' Color22, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[77].Value.ToString() + "' Color23, '" + Stoping.Rows[y].Cells[78].Value.ToString() + "' Color24,'" + Stoping.Rows[y].Cells[79].Value.ToString() + "' Color25, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[80].Value.ToString() + "' Color26, '" + Stoping.Rows[y].Cells[81].Value.ToString() + "' Color27,'" + Stoping.Rows[y].Cells[82].Value.ToString() + "' Color28, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[83].Value.ToString() + "' Color29, '" + Stoping.Rows[y].Cells[84].Value.ToString() + "' Color30,'" + Stoping.Rows[y].Cells[85].Value.ToString() + "' Color31, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[86].Value.ToString() + "' Color32, '" + Stoping.Rows[y].Cells[87].Value.ToString() + "' Color33,'" + Stoping.Rows[y].Cells[88].Value.ToString() + "' Color34, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[89].Value.ToString() + "' Color35, '" + Stoping.Rows[y].Cells[90].Value.ToString() + "' Color36,'" + Stoping.Rows[y].Cells[91].Value + "' Color37, \r\n";
                //        _dbManStopingData.SqlStatement = _dbManStopingData.SqlStatement + "'" + Stoping.Rows[y].Cells[92].Value.ToString() + "' Color38, '" + Stoping.Rows[y].Cells[93].Value.ToString() + "' Color39,'" + Stoping.Rows[y].Cells[94].Value.ToString() + "' Color40 \r\n";
                //    }
                }
            }


            //_dbManStopingData.SqlStatement = "select * from Temp_MODailyDataStoping where userid = '" + clsUserInfo.UserID + "' order by line asc ";
            _dbManStopingData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManStopingData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManStopingData.ResultsTableName = "MODaily_Stoping_Data";
            _dbManStopingData.ExecuteInstruction();


            DataSet dsStopeHead = new DataSet();
            dsStopeHead.Tables.Add(_dbManStopingHeading.ResultsDataTable);
            DataSet dsStopeData = new DataSet();
            dsStopeData.Tables.Add(_dbManStopingData.ResultsDataTable);

            KPIDbl.RegisterData(dsStopeHead);
            KPIDbl.RegisterData(dsStopeData);

            if (reportSettings.Color == "0")
            {
                try
                {
                    lblColors.Text = "ABS Colors";
                }
                catch { }
                //return;
            }
            else
            {
                try
                {
                    lblColors.Text = "New Colors";
                }
                catch { }
            }

            //KPIDbl.Load("MODailyStoping1.frx");
            if (month2 != "201901")
            {
                KPIDbl.Load(TGlobalItems.ReportsFolder + "\\MODailyStoping1.frx");
            }
            else
            {
                KPIDbl.Load(TGlobalItems.ReportsFolder + "\\MODailyStoping2.frx");
            }

            KPIDbl.SetParameterValue("Colors",lblColors.Text);

            //KPIDbl.Design();

            if (TParameters.DesignReport)
            {
                theReport.Design();
            }

            KPIDbl.Prepare();
            ActiveReport.SetReport = KPIDbl;
            ActiveReport.isDone = true;


           // Cursor = Cursors.Default;
            Application.Idle += new System.EventHandler(this.Application_Idle);
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            ///NightShift
            if (theReport.GetParameterValue("MODailyDate") != null)
            {
                MODailyDateLbl.Text = theReport.GetParameterValue("MODailyDate").ToString();
            }


            if (theReport.GetParameterValue("MODailyClick") != null)
            {
                MODailyClickLbl.Text = theReport.GetParameterValue("MODailyClick").ToString();
            }

            if (theReport.GetParameterValue("MODailyWP") != null)
            {
                MODailyWPLblNS.Text = theReport.GetParameterValue("MODailyWP").ToString();
            }


            ////MoDailyStoping

            if (KPIDbl.GetParameterValue("MODailyDate") != null)
            {
                MODailyDateLbl.Text = KPIDbl.GetParameterValue("MODailyDate").ToString();
            }


            if (KPIDbl.GetParameterValue("KPI") != null)
            {
                MODailyClickLbl.Text = KPIDbl.GetParameterValue("KPI").ToString();
            }

            if (KPIDbl.GetParameterValue("MODailyWP") != null)
            {
                MODailyWPLbl.Text = KPIDbl.GetParameterValue("MODailyWP").ToString();
            }

            ////MoDailyDev

            if (theReportDev.GetParameterValue("MODailyDate") != null)
            {
                MODailyDateLbl.Text = theReportDev.GetParameterValue("MODailyDate").ToString();
            }


            if (theReportDev.GetParameterValue("KPI") != null)
            {
                MODailyClickLbl.Text = theReportDev.GetParameterValue("KPI").ToString();
            }

            if (theReportDev.GetParameterValue("MODailyWP") != null)
            {
                MODailyWPLblDev.Text = theReportDev.GetParameterValue("MODailyWP").ToString();
            }


            ////MoDailyVamp

            if (theReportVamp.GetParameterValue("MODailyDate") != null)
            {
                MODailyDateLbl.Text = theReportVamp.GetParameterValue("MODailyDate").ToString();
            }


            if (theReportVamp.GetParameterValue("MODailyClick") != null)
            {
                MODailyClickLbl.Text = theReportVamp.GetParameterValue("MODailyClick").ToString();
            }

            if (theReportVamp.GetParameterValue("MODailyWP") != null)
            {
                MODailyWPLblVamp.Text = theReportVamp.GetParameterValue("MODailyWP").ToString();
            }
        }

        private void btnColors_Click(object sender, EventArgs e)
        {
            if (lblColors.Text == "ABS Colors")
            {
                lblColors.Text = "New Colors";
                return;
            }

            if (lblColors.Text == "New Colors")
            {
                lblColors.Text = "ABS Colors";
                return;
            }
        }

        private void ABSBtn_Click(object sender, EventArgs e)
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

        private void repositoryItemRadioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (reportSettings.Color == "0")
            //{
            //    lblColors.Text = "ABS Colors";
            //    //return;
            //}
            //else
            //{
            //    lblColors.Text = "New Colors";
            //}

            //if (lblColors.Text == "New Colors")
            //{
            //    lblColors.Text = "ABS Colors";
            //    return;
            //}
        }
    }
}
