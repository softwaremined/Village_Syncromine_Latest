namespace Mineware.Systems.HarmonyPASReports.TopPanelsReport
{
    partial class ucTopPanelsReport
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pgTopPanelsRepSettings = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgTopPanelsRepSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).BeginInit();
            this.SuspendLayout();
            // 
            // pgTopPanelsRepSettings
            // 
            this.pgTopPanelsRepSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgTopPanelsRepSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgTopPanelsRepSettings.Location = new System.Drawing.Point(0, 0);
            this.pgTopPanelsRepSettings.Name = "pgTopPanelsRepSettings";
            this.pgTopPanelsRepSettings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riProdmonth,
            this.riSection});
            this.pgTopPanelsRepSettings.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonth,
            this.iSection});
            this.pgTopPanelsRepSettings.Size = new System.Drawing.Size(378, 311);
            this.pgTopPanelsRepSettings.TabIndex = 4;
            // 
            // riProdmonth
            // 
            this.riProdmonth.AutoHeight = false;
            this.riProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.riProdmonth.Mask.EditMask = "yyyyMM";
            this.riProdmonth.Mask.IgnoreMaskBlank = false;
            this.riProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riProdmonth.Name = "riProdmonth";
            this.riProdmonth.EditValueChanged += new System.EventHandler(this.riProdmonth_EditValueChanged);
            // 
            // riSection
            // 
            this.riSection.AutoHeight = false;
            this.riSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riSection.Name = "riSection";
            // 
            // iProdmonth
            // 
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.Caption = "Prodmonth";
            this.iProdmonth.Properties.FieldName = "Prodmonth";
            this.iProdmonth.Properties.RowEdit = this.riProdmonth;
            // 
            // iSection
            // 
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "SectionID";
            this.iSection.Properties.RowEdit = this.riSection;
            // 
            // ucTopPanelsReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgTopPanelsRepSettings);
            this.Name = "ucTopPanelsReport";
            this.Load += new System.EventHandler(this.ucTopPanelsReport_Load);
            this.Controls.SetChildIndex(this.pgTopPanelsRepSettings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgTopPanelsRepSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgTopPanelsRepSettings;
        private Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
    }
}
