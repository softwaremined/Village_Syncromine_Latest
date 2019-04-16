namespace Mineware.Systems.Reports.DuplicateSampleReport
{ 
    partial class ucDuplicateSampleReport
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
            this.pgCourseBlankReport = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.rpShaft = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.iShaft = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgCourseBlankReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpShaft)).BeginInit();
            this.SuspendLayout();
            // 
            // pgCourseBlankReport
            // 
            this.pgCourseBlankReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgCourseBlankReport.Location = new System.Drawing.Point(0, 0);
            this.pgCourseBlankReport.Name = "pgCourseBlankReport";
            this.pgCourseBlankReport.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpShaft});
            this.pgCourseBlankReport.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iShaft});
            this.pgCourseBlankReport.Size = new System.Drawing.Size(349, 349);
            this.pgCourseBlankReport.TabIndex = 0;
            // 
            // rpShaft
            // 
            this.rpShaft.AutoHeight = false;
            this.rpShaft.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpShaft.Name = "rpShaft";
            this.rpShaft.NullText = "";
            // 
            // iShaft
            // 
            this.iShaft.Name = "iShaft";
            this.iShaft.Properties.Caption = "Shaft";
            this.iShaft.Properties.FieldName = "Shaft";
            this.iShaft.Properties.RowEdit = this.rpShaft;
            // 
            // ucCourseBlankSampleReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgCourseBlankReport);
            this.Name = "ucCourseBlankSampleReport";
            this.Size = new System.Drawing.Size(349, 349);
            this.Load += new System.EventHandler(this.ucCourseBlankSampleReport_Load);
            this.Controls.SetChildIndex(this.pgCourseBlankReport, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgCourseBlankReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpShaft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgCourseBlankReport;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iShaft;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpShaft;
    }
}
