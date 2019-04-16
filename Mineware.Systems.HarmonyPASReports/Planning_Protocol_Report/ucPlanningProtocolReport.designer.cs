namespace Mineware.Systems.Reports
{
    partial class ucPlanningProtocolReport
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
            this.pgPlanningProtocolReport = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.rpActivity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rpPrint = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.rpWPSelection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rpPlanningProtocolData = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.rpMOSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rpProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iMOSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iPlanningProtocolData = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgPlanningProtocolReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpWPSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlanningProtocolData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMOSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpProdmonth)).BeginInit();
            this.SuspendLayout();
            // 
            // pgPlanningProtocolReport
            // 
            this.pgPlanningProtocolReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgPlanningProtocolReport.Location = new System.Drawing.Point(0, 0);
            this.pgPlanningProtocolReport.Name = "pgPlanningProtocolReport";
            this.pgPlanningProtocolReport.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgPlanningProtocolReport.Padding = new System.Windows.Forms.Padding(5);
            this.pgPlanningProtocolReport.RecordWidth = 122;
            this.pgPlanningProtocolReport.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpActivity,
            this.rpPrint,
            this.rpWPSelection,
            this.rpPlanningProtocolData,
            this.rpMOSection,
            this.rpProdmonth});
            this.pgPlanningProtocolReport.RowHeaderWidth = 78;
            this.pgPlanningProtocolReport.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonth,
            this.iActivity,
            this.iMOSection,
            this.iPlanningProtocolData});
            this.pgPlanningProtocolReport.Size = new System.Drawing.Size(338, 318);
            this.pgPlanningProtocolReport.TabIndex = 15;
            this.pgPlanningProtocolReport.Click += new System.EventHandler(this.pgPlanningProtocolReport_Click);
            // 
            // rpActivity
            // 
            this.rpActivity.AutoHeight = false;
            this.rpActivity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpActivity.Name = "rpActivity";
            this.rpActivity.NullText = "";
            this.rpActivity.EditValueChanged += new System.EventHandler(this.rpActivity_EditValueChanged);
            this.rpActivity.Enter += new System.EventHandler(this.rpActivity_Enter);
            // 
            // rpPrint
            // 
            this.rpPrint.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("MO Section", "MO Section"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Workplace", "Workplace")});
            this.rpPrint.Name = "rpPrint";
            this.rpPrint.SelectedIndexChanged += new System.EventHandler(this.rpPrint_SelectedIndexChanged);
            // 
            // rpWPSelection
            // 
            this.rpWPSelection.AutoHeight = false;
            this.rpWPSelection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpWPSelection.Name = "rpWPSelection";
            this.rpWPSelection.NullText = "";
            this.rpWPSelection.Enter += new System.EventHandler(this.rpWPSelection_Enter);
            // 
            // rpPlanningProtocolData
            // 
            this.rpPlanningProtocolData.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Dynamic", "Un-Approved"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Locked", "Approved")});
            this.rpPlanningProtocolData.Name = "rpPlanningProtocolData";
            this.rpPlanningProtocolData.SelectedIndexChanged += new System.EventHandler(this.rpPlanningProtocolData_SelectedIndexChanged);
            // 
            // rpMOSection
            // 
            this.rpMOSection.AutoHeight = false;
            this.rpMOSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpMOSection.Name = "rpMOSection";
            this.rpMOSection.NullText = "";
            this.rpMOSection.EditValueChanged += new System.EventHandler(this.rpMOSection_EditValueChanged);
            // 
            // rpProdmonth
            // 
            this.rpProdmonth.AutoHeight = false;
            this.rpProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.rpProdmonth.Mask.EditMask = "yyyyMM";
            this.rpProdmonth.Mask.IgnoreMaskBlank = false;
            this.rpProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.rpProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.rpProdmonth.Name = "rpProdmonth";
            // 
            // iProdmonth
            // 
            this.iProdmonth.IsChildRowsLoaded = true;
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.Caption = "Production Month";
            this.iProdmonth.Properties.FieldName = "Prodmonth";
            this.iProdmonth.Properties.RowEdit = this.rpProdmonth;
            // 
            // iActivity
            // 
            this.iActivity.Height = 18;
            this.iActivity.IsChildRowsLoaded = true;
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Code";
            this.iActivity.Properties.RowEdit = this.rpActivity;
            // 
            // iMOSection
            // 
            this.iMOSection.IsChildRowsLoaded = true;
            this.iMOSection.Name = "iMOSection";
            this.iMOSection.Properties.Caption = "MO Section";
            this.iMOSection.Properties.FieldName = "SECTIONID_2";
            this.iMOSection.Properties.RowEdit = this.rpMOSection;
            // 
            // iPlanningProtocolData
            // 
            this.iPlanningProtocolData.IsChildRowsLoaded = true;
            this.iPlanningProtocolData.Name = "iPlanningProtocolData";
            this.iPlanningProtocolData.Properties.Caption = "Planning Protocol Data";
            this.iPlanningProtocolData.Properties.FieldName = "PPD";
            this.iPlanningProtocolData.Properties.RowEdit = this.rpPlanningProtocolData;
            this.iPlanningProtocolData.Visible = false;
            // 
            // ucPlanningProtocolReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgPlanningProtocolReport);
            this.Name = "ucPlanningProtocolReport";
            this.Size = new System.Drawing.Size(338, 318);
            this.Load += new System.EventHandler(this.ucPlanningProtocolReport_Load);
            this.Controls.SetChildIndex(this.pgPlanningProtocolReport, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgPlanningProtocolReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpWPSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlanningProtocolData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMOSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpProdmonth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgPlanningProtocolReport;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpActivity;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rpPrint;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpWPSelection;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rpPlanningProtocolData;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iActivity;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iPlanningProtocolData;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpMOSection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iMOSection;
        private Global.CustomControls.MWRepositoryItemProdMonth rpProdmonth;

    }
}
