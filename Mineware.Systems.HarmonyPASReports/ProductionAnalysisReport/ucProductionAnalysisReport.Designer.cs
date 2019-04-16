namespace Mineware.Systems.Reports.ProductionAnalysisReport
{
    partial class ucProductionAnalysisReport
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pgProductionAnalysisRepSettings = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riProdmonthSelection = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riStartDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.riEndDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.riSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riActivity = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riType = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iProdmonthSelection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iFromDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iToDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iType = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgProductionAnalysisRepSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonthSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riStartDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEndDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).BeginInit();
            this.SuspendLayout();
            // 
            // pgProductionAnalysisRepSettings
            // 
            this.pgProductionAnalysisRepSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgProductionAnalysisRepSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProductionAnalysisRepSettings.Location = new System.Drawing.Point(0, 0);
            this.pgProductionAnalysisRepSettings.Name = "pgProductionAnalysisRepSettings";
            this.pgProductionAnalysisRepSettings.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgProductionAnalysisRepSettings.RecordWidth = 140;
            this.pgProductionAnalysisRepSettings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riProdmonthSelection,
            this.riProdmonth,
            this.riStartDate,
            this.riEndDate,
            this.riSection,
            this.riActivity,
            this.riType});
            this.pgProductionAnalysisRepSettings.RowHeaderWidth = 60;
            this.pgProductionAnalysisRepSettings.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonthSelection,
            this.iProdmonth,
            this.iFromDate,
            this.iToDate,
            this.iSection,
            this.iActivity,
            this.iType});
            this.pgProductionAnalysisRepSettings.Size = new System.Drawing.Size(343, 311);
            this.pgProductionAnalysisRepSettings.TabIndex = 4;
            // 
            // riProdmonthSelection
            // 
            this.riProdmonthSelection.Columns = 3;
            this.riProdmonthSelection.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Prodmonth", "Prodmonth"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("FromToDate", "FromToDate")});
            this.riProdmonthSelection.Name = "riProdmonthSelection";
            this.riProdmonthSelection.EditValueChanged += new System.EventHandler(this.riProdmonthSelection_EditValueChanged);
            // 
            // riProdmonth
            // 
            this.riProdmonth.AutoHeight = false;
            this.riProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject7, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject8, "", null, null, true)});
            this.riProdmonth.Mask.EditMask = "yyyyMM";
            this.riProdmonth.Mask.IgnoreMaskBlank = false;
            this.riProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riProdmonth.Name = "riProdmonth";
            // 
            // riStartDate
            // 
            this.riStartDate.AutoHeight = false;
            this.riStartDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riStartDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riStartDate.Name = "riStartDate";
            this.riStartDate.EditValueChanged += new System.EventHandler(this.riStartDate_EditValueChanged);
            // 
            // riEndDate
            // 
            this.riEndDate.AutoHeight = false;
            this.riEndDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riEndDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riEndDate.Name = "riEndDate";
            this.riEndDate.EditValueChanged += new System.EventHandler(this.riEndDate_EditValueChanged);
            // 
            // riSection
            // 
            this.riSection.AutoHeight = false;
            this.riSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riSection.Name = "riSection";
            // 
            // riActivity
            // 
            this.riActivity.Columns = 2;
            this.riActivity.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Stoping", "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Development", "Development"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Engineering", "Engineering"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("HR", "HR")});
            this.riActivity.Name = "riActivity";
            // 
            // riType
            // 
            this.riType.Columns = 3;
            this.riType.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Daily", "Daily"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Progressive", "Progressive")});
            this.riType.Name = "riType";
            // 
            // iProdmonthSelection
            // 
            this.iProdmonthSelection.Name = "iProdmonthSelection";
            this.iProdmonthSelection.Properties.Caption = "Selection";
            this.iProdmonthSelection.Properties.FieldName = "ProdMonthSelection";
            this.iProdmonthSelection.Properties.RowEdit = this.riProdmonthSelection;
            // 
            // iProdmonth
            // 
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.Caption = "Prodmonth";
            this.iProdmonth.Properties.FieldName = "Prodmonth";
            this.iProdmonth.Properties.RowEdit = this.riProdmonth;
            // 
            // iFromDate
            // 
            this.iFromDate.Name = "iFromDate";
            this.iFromDate.Properties.Caption = "From Date";
            this.iFromDate.Properties.FieldName = "StartDate";
            this.iFromDate.Properties.RowEdit = this.riStartDate;
            // 
            // iToDate
            // 
            this.iToDate.Name = "iToDate";
            this.iToDate.Properties.Caption = "To Date";
            this.iToDate.Properties.FieldName = "EndDate";
            this.iToDate.Properties.RowEdit = this.riEndDate;
            // 
            // iSection
            // 
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "SectionID";
            this.iSection.Properties.RowEdit = this.riSection;
            // 
            // iActivity
            // 
            this.iActivity.Height = 36;
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Activity";
            this.iActivity.Properties.RowEdit = this.riActivity;
            // 
            // iType
            // 
            this.iType.Name = "iType";
            this.iType.Properties.Caption = "Type";
            this.iType.Properties.FieldName = "Type";
            this.iType.Properties.RowEdit = this.riType;
            // 
            // ucProductionAnalysisReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgProductionAnalysisRepSettings);
            this.Name = "ucProductionAnalysisReport";
            this.Size = new System.Drawing.Size(343, 311);
            this.Load += new System.EventHandler(this.ucProductionAnalysisReport_Load);
            this.Controls.SetChildIndex(this.pgProductionAnalysisRepSettings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgProductionAnalysisRepSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonthSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riStartDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEndDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgProductionAnalysisRepSettings;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riProdmonthSelection;
        private Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riStartDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riEndDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riActivity;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riType;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonthSelection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iFromDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iToDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iActivity;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iType;
    }
}
