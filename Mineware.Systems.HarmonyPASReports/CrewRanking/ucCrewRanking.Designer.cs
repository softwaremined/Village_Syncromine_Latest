namespace Mineware.Systems.Reports.CrewRanking
{
    partial class ucCrewRanking
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
            this.pgCrewRankingRepSettings = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riProdmonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.riSection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riActivity = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riRatingBy = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riFrom = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iByy = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.riOrderBy = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iSectionID = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iRatingBy = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iFrom = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iBy = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iOrderBy = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgCrewRankingRepSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRatingBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iByy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riOrderBy)).BeginInit();
            this.SuspendLayout();
            // 
            // pgCrewRankingRepSettings
            // 
            this.pgCrewRankingRepSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgCrewRankingRepSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgCrewRankingRepSettings.Location = new System.Drawing.Point(0, 0);
            this.pgCrewRankingRepSettings.Name = "pgCrewRankingRepSettings";
            this.pgCrewRankingRepSettings.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgCrewRankingRepSettings.RecordWidth = 154;
            this.pgCrewRankingRepSettings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riProdmonth,
            this.riSection,
            this.riActivity,
            this.riRatingBy,
            this.riFrom,
            this.iByy,
            this.riOrderBy});
            this.pgCrewRankingRepSettings.RowHeaderWidth = 46;
            this.pgCrewRankingRepSettings.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iProdmonth,
            this.iSectionID,
            this.iActivity,
            this.iRatingBy,
            this.iFrom,
            this.iBy,
            this.iOrderBy});
            this.pgCrewRankingRepSettings.Size = new System.Drawing.Size(387, 356);
            this.pgCrewRankingRepSettings.TabIndex = 0;
            this.pgCrewRankingRepSettings.Click += new System.EventHandler(this.pgCrewRankingRepSettings_Click);
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
            // riActivity
            // 
            this.riActivity.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Stoping", "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Development", "Development")});
            this.riActivity.Name = "riActivity";
            this.riActivity.EditValueChanged += new System.EventHandler(this.riActivity_EditValueChanged);
            // 
            // riRatingBy
            // 
            this.riRatingBy.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("M2", "M2"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("M2 Percentage", "M2 Percentage")});
            this.riRatingBy.Name = "riRatingBy";
            // 
            // riFrom
            // 
            this.riFrom.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Actual/Book", "Actual/Book"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Actual/Planned", "Actual/Planned")});
            this.riFrom.Name = "riFrom";
            // 
            // iByy
            // 
            this.iByy.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Crew", "Crew"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("MO", "MO"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Shift Boss", "Shift Boss")});
            this.iByy.Name = "iByy";
            // 
            // riOrderBy
            // 
            this.riOrderBy.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1 Month", "1 Month"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("3 Month", "3 Month"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("12 Month", "12 Month")});
            this.riOrderBy.Name = "riOrderBy";
            // 
            // iProdmonth
            // 
            this.iProdmonth.Height = 16;
            this.iProdmonth.IsChildRowsLoaded = true;
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.Caption = "Prodmonth";
            this.iProdmonth.Properties.FieldName = "Prodmonth";
            this.iProdmonth.Properties.RowEdit = this.riProdmonth;
            // 
            // iSectionID
            // 
            this.iSectionID.IsChildRowsLoaded = true;
            this.iSectionID.Name = "iSectionID";
            this.iSectionID.Properties.Caption = "Section";
            this.iSectionID.Properties.FieldName = "SectionID";
            this.iSectionID.Properties.RowEdit = this.riSection;
            // 
            // iActivity
            // 
            this.iActivity.IsChildRowsLoaded = true;
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Activity";
            this.iActivity.Properties.RowEdit = this.riActivity;
            // 
            // iRatingBy
            // 
            this.iRatingBy.IsChildRowsLoaded = true;
            this.iRatingBy.Name = "iRatingBy";
            this.iRatingBy.Properties.Caption = "Rating By";
            this.iRatingBy.Properties.FieldName = "RatingBy";
            this.iRatingBy.Properties.RowEdit = this.riRatingBy;
            // 
            // iFrom
            // 
            this.iFrom.IsChildRowsLoaded = true;
            this.iFrom.Name = "iFrom";
            this.iFrom.Properties.Caption = "From";
            this.iFrom.Properties.FieldName = "From";
            this.iFrom.Properties.RowEdit = this.riFrom;
            // 
            // iBy
            // 
            this.iBy.IsChildRowsLoaded = true;
            this.iBy.Name = "iBy";
            this.iBy.Properties.Caption = "By";
            this.iBy.Properties.FieldName = "By";
            this.iBy.Properties.RowEdit = this.iByy;
            // 
            // iOrderBy
            // 
            this.iOrderBy.IsChildRowsLoaded = true;
            this.iOrderBy.Name = "iOrderBy";
            this.iOrderBy.Properties.Caption = "Order By";
            this.iOrderBy.Properties.FieldName = "OrderBy";
            this.iOrderBy.Properties.RowEdit = this.riOrderBy;
            // 
            // ucCrewRanking
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgCrewRankingRepSettings);
            this.Name = "ucCrewRanking";
            this.Size = new System.Drawing.Size(387, 356);
            this.Load += new System.EventHandler(this.ucCrewRanking_Load);
            this.Controls.SetChildIndex(this.pgCrewRankingRepSettings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgCrewRankingRepSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riRatingBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iByy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riOrderBy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgCrewRankingRepSettings;
        private Global.CustomControls.MWRepositoryItemProdMonth riProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riActivity;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riRatingBy;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riFrom;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup iByy;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup riOrderBy;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iProdmonth;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iSectionID;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iActivity;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iRatingBy;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iFrom;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iBy;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iOrderBy;
    }
}
