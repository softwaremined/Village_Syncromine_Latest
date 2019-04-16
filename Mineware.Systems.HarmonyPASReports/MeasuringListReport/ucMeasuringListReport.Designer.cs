namespace Mineware.Systems.Reports.MeasuringListReport
{
    partial class ucMeasuringListReport
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
            this.ParamLbl1 = new DevExpress.XtraEditors.LabelControl();
            this.ParamLbl = new DevExpress.XtraEditors.LabelControl();
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
            this.pgMeasuringList.Click += new System.EventHandler(this.pgMeasuringList_Click);
            this.pgMeasuringList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pgMeasuringList_MouseClick);
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
            this.iSection.Properties.FieldName = "SectionID";
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
            // ParamLbl1
            // 
            this.ParamLbl1.Location = new System.Drawing.Point(190, 309);
            this.ParamLbl1.Name = "ParamLbl1";
            this.ParamLbl1.Size = new System.Drawing.Size(63, 13);
            this.ParamLbl1.TabIndex = 4;
            this.ParamLbl1.Text = "labelControl1";
            this.ParamLbl1.Visible = false;
            // 
            // ParamLbl
            // 
            this.ParamLbl.Location = new System.Drawing.Point(190, 290);
            this.ParamLbl.Name = "ParamLbl";
            this.ParamLbl.Size = new System.Drawing.Size(63, 13);
            this.ParamLbl.TabIndex = 5;
            this.ParamLbl.Text = "labelControl2";
            this.ParamLbl.Visible = false;
            this.ParamLbl.TextChanged += new System.EventHandler(this.ParamLbl_TextChanged);
            // 
            // ucMeasuringListReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ParamLbl);
            this.Controls.Add(this.ParamLbl1);
            this.Controls.Add(this.pgMeasuringList);
            this.Name = "ucMeasuringListReport";
            this.Size = new System.Drawing.Size(323, 325);
            this.Load += new System.EventHandler(this.ucMeasuringListReport_Load);
            this.Click += new System.EventHandler(this.ucMeasuringListReport_Click);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucMeasuringListReport_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ucMeasuringListReport_MouseClick);
            this.Controls.SetChildIndex(this.pgMeasuringList, 0);
            this.Controls.SetChildIndex(this.ParamLbl1, 0);
            this.Controls.SetChildIndex(this.ParamLbl, 0);
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
        private DevExpress.XtraEditors.LabelControl ParamLbl1;
        public DevExpress.XtraEditors.LabelControl ParamLbl;
    }
}
