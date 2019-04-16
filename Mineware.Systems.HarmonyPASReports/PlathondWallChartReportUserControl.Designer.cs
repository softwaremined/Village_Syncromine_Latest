namespace Mineware.Systems.HarmonyPASReports.PlathondWallChartReport
{
    partial class PlathondWallChartReportUserControl
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            this.pgMeasuringList = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riActivity = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.iProdMonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgMeasuringList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            this.SuspendLayout();
            // 
            // pgMeasuringList
            // 
            this.pgMeasuringList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgMeasuringList.Location = new System.Drawing.Point(0, 0);
            this.pgMeasuringList.Name = "pgMeasuringList";
            this.pgMeasuringList.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgMeasuringList.RecordWidth = 127;
            this.pgMeasuringList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riSection,
            this.riActivity,
            this.riProdmonth});
            this.pgMeasuringList.RowHeaderWidth = 73;
            this.pgMeasuringList.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdMonth,
            this.iSection,
            this.iActivity});
            this.pgMeasuringList.Size = new System.Drawing.Size(323, 325);
            this.pgMeasuringList.TabIndex = 0;
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
            this.riActivity.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Stoping", "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Development", "Development")});
            this.riActivity.Name = "riActivity";
            // 
            // riProdmonth
            // 
            this.riProdmonth.AutoHeight = false;
            this.riProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2)});
            this.riProdmonth.Mask.EditMask = "yyyyMM";
            this.riProdmonth.Mask.IgnoreMaskBlank = false;
            this.riProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.riProdmonth.Mask.UseMaskAsDisplayFormat = true;
            this.riProdmonth.Name = "riProdmonth";
            // 
            // iProdMonth
            // 
            this.iProdMonth.IsChildRowsLoaded = true;
            this.iProdMonth.Name = "iProdMonth";
            this.iProdMonth.Properties.Caption = "Production Month";
            this.iProdMonth.Properties.FieldName = "Prodmonth";
            this.iProdMonth.Properties.RowEdit = this.riProdmonth;
            // 
            // iSection
            // 
            this.iSection.Height = 18;
            this.iSection.IsChildRowsLoaded = true;
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "NAME";
            this.iSection.Properties.RowEdit = this.riSection;
            // 
            // iActivity
            // 
            this.iActivity.IsChildRowsLoaded = true;
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Activity";
            this.iActivity.Properties.RowEdit = this.riActivity;
            // 
            // PlathondWallChartReportUserControl
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgMeasuringList);
            this.Name = "PlathondWallChartReportUserControl";
            this.Size = new System.Drawing.Size(323, 325);
            this.Load += new System.EventHandler(this.PlathondWallChartReportUserControl_Load);
            this.Controls.SetChildIndex(this.pgMeasuringList, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgMeasuringList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgMeasuringList;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riActivity;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdMonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iActivity;
        private Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
    }
}
