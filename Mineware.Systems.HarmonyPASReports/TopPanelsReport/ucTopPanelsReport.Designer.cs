namespace Mineware.Systems.Reports.TopPanelsReport
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
            this.pgTopPanelsRepSettings = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.repositoryItemRadioGroup2 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.rgBottom = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSection = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.iMeas = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.iBottomFilter = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgTopPanelsRepSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // pgTopPanelsRepSettings
            // 
            this.pgTopPanelsRepSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgTopPanelsRepSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgTopPanelsRepSettings.Location = new System.Drawing.Point(0, 0);
            this.pgTopPanelsRepSettings.Name = "pgTopPanelsRepSettings";
            this.pgTopPanelsRepSettings.Padding = new System.Windows.Forms.Padding(5);
            this.pgTopPanelsRepSettings.RecordWidth = 127;
            this.pgTopPanelsRepSettings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riProdmonth,
            this.riSection,
            this.repositoryItemComboBox1,
            this.repositoryItemRadioGroup1,
            this.repositoryItemRadioGroup2,
            this.rgBottom});
            this.pgTopPanelsRepSettings.RowHeaderWidth = 73;
            this.pgTopPanelsRepSettings.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonth,
            this.iSection,
            this.iType,
            this.iMeas,
            this.iBottomFilter});
            this.pgTopPanelsRepSettings.Size = new System.Drawing.Size(348, 311);
            this.pgTopPanelsRepSettings.TabIndex = 4;
            this.pgTopPanelsRepSettings.Click += new System.EventHandler(this.pgTopPanelsRepSettings_Click);
            // 
            // riProdmonth
            // 
            this.riProdmonth.AutoHeight = false;
            this.riProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
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
            // rgBottom
            // 
            this.rgBottom.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "Top Panels"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Selected Panels")});
            this.rgBottom.Name = "rgBottom";
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
            this.iSection.Fixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iSection.InternalFixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iSection.IsChildRowsLoaded = true;
            this.iSection.Name = "iSection";
            this.iSection.Properties.Caption = "Section";
            this.iSection.Properties.FieldName = "SectionID";
            this.iSection.Properties.RowEdit = this.riSection;
            // 
            // iType
            // 
            this.iType.Fixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iType.Height = 17;
            this.iType.InternalFixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iType.Name = "iType";
            this.iType.Properties.Caption = "Type";
            this.iType.Properties.FieldName = "Type";
            this.iType.Properties.RowEdit = this.repositoryItemRadioGroup2;
            // 
            // iMeas
            // 
            this.iMeas.Name = "iMeas";
            this.iMeas.Properties.Caption = "Measurement";
            this.iMeas.Properties.FieldName = "Meas";
            this.iMeas.Properties.RowEdit = this.repositoryItemRadioGroup1;
            // 
            // iBottomFilter
            // 
            this.iBottomFilter.Fixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iBottomFilter.InternalFixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iBottomFilter.Name = "iBottomFilter";
            this.iBottomFilter.Properties.Caption = "Report Type";
            this.iBottomFilter.Properties.FieldName = "ReportType";
            this.iBottomFilter.Properties.RowEdit = this.rgBottom;
            // 
            // ucTopPanelsReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgTopPanelsRepSettings);
            this.Name = "ucTopPanelsReport";
            this.Size = new System.Drawing.Size(348, 311);
            this.Load += new System.EventHandler(this.ucTopPanelsReport_Load);
            this.Controls.SetChildIndex(this.pgTopPanelsRepSettings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgTopPanelsRepSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgBottom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgTopPanelsRepSettings;
        private Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSection;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup2;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow iType;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow iMeas;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rgBottom;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow iBottomFilter;
    }
}
