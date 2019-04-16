namespace Mineware.Systems.Reports.WorstPerformers
{
    partial class ucWorstPerformerss
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
            this.pgTopPanelsRepSettings = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.repositoryItemRadioGroup2 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.repositoryItemRadioGroup3 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.repositoryItemRadioGroup4 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iMeas = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.iType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.iCrew = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.riCrewLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pgTopPanelsRepSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCrewLookUpEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // pgTopPanelsRepSettings
            // 
            this.pgTopPanelsRepSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgTopPanelsRepSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgTopPanelsRepSettings.Location = new System.Drawing.Point(0, 0);
            this.pgTopPanelsRepSettings.Name = "pgTopPanelsRepSettings";
            this.pgTopPanelsRepSettings.RecordWidth = 144;
            this.pgTopPanelsRepSettings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riProdmonth,
            this.riSection,
            this.repositoryItemComboBox1,
            this.repositoryItemRadioGroup1,
            this.repositoryItemRadioGroup2,
            this.repositoryItemRadioGroup3,
            this.repositoryItemRadioGroup4,
            this.riCrewLookUpEdit});
            this.pgTopPanelsRepSettings.RowHeaderWidth = 56;
            this.pgTopPanelsRepSettings.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonth,
            this.iSection,
            this.iMeas,
            this.iActivity,
            this.iType,
            this.iCrew});
            this.pgTopPanelsRepSettings.Size = new System.Drawing.Size(310, 343);
            this.pgTopPanelsRepSettings.TabIndex = 9;
            this.pgTopPanelsRepSettings.Click += new System.EventHandler(this.pgTopPanelsRepSettings_Click);
            this.pgTopPanelsRepSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pgTopPanelsRepSettings_MouseDown);
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
            // riSection
            // 
            this.riSection.AutoHeight = false;
            this.riSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riSection.Name = "riSection";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Gold", "Gold"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Cmgt", "Cmgt")});
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            // 
            // repositoryItemRadioGroup2
            // 
            this.repositoryItemRadioGroup2.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Dynamic", "Dynamic"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Locked", "Locked")});
            this.repositoryItemRadioGroup2.Name = "repositoryItemRadioGroup2";
            // 
            // repositoryItemRadioGroup3
            // 
            this.repositoryItemRadioGroup3.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(0)), "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "Development")});
            this.repositoryItemRadioGroup3.Name = "repositoryItemRadioGroup3";
            // 
            // repositoryItemRadioGroup4
            // 
            this.repositoryItemRadioGroup4.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("MO Summary", "MO Summary"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Crew Summary", "Crew Summary")});
            this.repositoryItemRadioGroup4.Name = "repositoryItemRadioGroup4";
            // 
            // iProdmonth
            // 
            this.iProdmonth.Fixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iProdmonth.InternalFixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iProdmonth.IsChildRowsLoaded = true;
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.Caption = "Prodmonth";
            this.iProdmonth.Properties.FieldName = "Prodmonth";
            this.iProdmonth.Properties.RowEdit = this.riProdmonth;
            // 
            // iSection
            // 
            this.iSection.IsChildRowsLoaded = true;
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "SectionID";
            this.iSection.Properties.RowEdit = this.riSection;
            this.iSection.Visible = false;
            // 
            // iMeas
            // 
            this.iMeas.Name = "iMeas";
            this.iMeas.Properties.Caption = "Measurement";
            this.iMeas.Properties.FieldName = "Meas";
            this.iMeas.Properties.RowEdit = this.repositoryItemRadioGroup1;
            this.iMeas.Visible = false;
            // 
            // iActivity
            // 
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Activity";
            this.iActivity.Properties.RowEdit = this.repositoryItemRadioGroup3;
            this.iActivity.Visible = false;
            // 
            // iType
            // 
            this.iType.Fixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iType.InternalFixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iType.Name = "iType";
            this.iType.Properties.Caption = "Type";
            this.iType.Properties.FieldName = "Type";
            this.iType.Properties.RowEdit = this.repositoryItemRadioGroup4;
            // 
            // iCrew
            // 
            this.iCrew.Name = "iCrew";
            this.iCrew.Properties.Caption = "Crew";
            this.iCrew.Properties.FieldName = "Crew";
            this.iCrew.Properties.RowEdit = this.riCrewLookUpEdit;
            // 
            // riCrewLookUpEdit
            // 
            this.riCrewLookUpEdit.AutoHeight = false;
            this.riCrewLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riCrewLookUpEdit.Name = "riCrewLookUpEdit";
            // 
            // ucWorstPerformerss
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgTopPanelsRepSettings);
            this.Name = "ucWorstPerformerss";
            this.Size = new System.Drawing.Size(310, 343);
            this.Load += new System.EventHandler(this.ucWorstPerformerss_Load);
            this.Controls.SetChildIndex(this.pgTopPanelsRepSettings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgTopPanelsRepSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riCrewLookUpEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgTopPanelsRepSettings;
        private Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup2;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow iMeas;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup3;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup4;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow iActivity;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow iType;
        //private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit isCrew;
       // private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow iCrew;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riCrewLookUpEdit;
    }
}
