namespace Mineware.Systems.Reports.TonnageReport
{
    partial class ucTonnageReport
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pgTonnageRepSettings = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riProdmonthSelection = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riFromDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.riTodate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.riSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riGraphType = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riType = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iProdmonthSelection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iFromDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iToDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iGraphType = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iType = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgTonnageRepSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonthSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTodate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTodate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGraphType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).BeginInit();
            this.SuspendLayout();
            // 
            // pgTonnageRepSettings
            // 
            this.pgTonnageRepSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgTonnageRepSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgTonnageRepSettings.Location = new System.Drawing.Point(0, 0);
            this.pgTonnageRepSettings.Name = "pgTonnageRepSettings";
            this.pgTonnageRepSettings.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgTonnageRepSettings.RecordWidth = 125;
            this.pgTonnageRepSettings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riProdmonthSelection,
            this.riProdmonth,
            this.riFromDate,
            this.riTodate,
            this.riSection,
            this.riGraphType,
            this.riType});
            this.pgTonnageRepSettings.RowHeaderWidth = 75;
            this.pgTonnageRepSettings.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonthSelection,
            this.iProdmonth,
            this.iFromDate,
            this.iToDate,
            this.iSection,
            this.iGraphType,
            this.iType});
            this.pgTonnageRepSettings.Size = new System.Drawing.Size(337, 324);
            this.pgTonnageRepSettings.TabIndex = 0;
            // 
            // riProdmonthSelection
            // 
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject11, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject12, "", null, null, true)});
            this.riProdmonth.Mask.EditMask = "yyyyMM";
            this.riProdmonth.Mask.IgnoreMaskBlank = false;
            this.riProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riProdmonth.Name = "riProdmonth";
            // 
            // riFromDate
            // 
            this.riFromDate.AutoHeight = false;
            this.riFromDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riFromDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riFromDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.riFromDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.riFromDate.EditFormat.FormatString = "yyyy-MM-dd";
            this.riFromDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.riFromDate.Name = "riFromDate";
            // 
            // riTodate
            // 
            this.riTodate.AutoHeight = false;
            this.riTodate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riTodate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riTodate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.riTodate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.riTodate.EditFormat.FormatString = "yyyy-MM-dd";
            this.riTodate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.riTodate.Name = "riTodate";
            // 
            // riSection
            // 
            this.riSection.AutoHeight = false;
            this.riSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riSection.Name = "riSection";
            // 
            // riGraphType
            // 
            this.riGraphType.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Daily", "Daily"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Progressive", "Progressive")});
            this.riGraphType.Name = "riGraphType";
            // 
            // riType
            // 
            this.riType.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Reef", "Reef"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Waste", "Waste"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Total", "Total")});
            this.riType.Name = "riType";
            // 
            // iProdmonthSelection
            // 
            this.iProdmonthSelection.Name = "iProdmonthSelection";
            this.iProdmonthSelection.Properties.Caption = "Prodmonth Selection";
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
            this.iFromDate.Properties.RowEdit = this.riFromDate;
            // 
            // iToDate
            // 
            this.iToDate.Name = "iToDate";
            this.iToDate.Properties.Caption = "To Date";
            this.iToDate.Properties.FieldName = "EndDate";
            this.iToDate.Properties.RowEdit = this.riTodate;
            // 
            // iSection
            // 
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "SectionID";
            this.iSection.Properties.RowEdit = this.riSection;
            // 
            // iGraphType
            // 
            this.iGraphType.Name = "iGraphType";
            this.iGraphType.Properties.Caption = "Graph Type";
            this.iGraphType.Properties.FieldName = "GraphType";
            this.iGraphType.Properties.RowEdit = this.riGraphType;
            // 
            // iType
            // 
            this.iType.Name = "iType";
            this.iType.Properties.Caption = "Type";
            this.iType.Properties.FieldName = "Type";
            this.iType.Properties.RowEdit = this.riType;
            // 
            // ucTonnageReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgTonnageRepSettings);
            this.Name = "ucTonnageReport";
            this.Size = new System.Drawing.Size(337, 324);
            this.Load += new System.EventHandler(this.ucTonnageReport_Load);
            this.Controls.SetChildIndex(this.pgTonnageRepSettings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgTonnageRepSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonthSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTodate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riTodate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riGraphType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgTonnageRepSettings;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riProdmonthSelection;
        private Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riFromDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riTodate;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riGraphType;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riType;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonthSelection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iFromDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iToDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iGraphType;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iType;
    }
}
