namespace Mineware.Systems.Reports.SICReport
{
    partial class ucSICReport
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
            this.pgSettingsMain = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.dteDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.luSectionID = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rdgrpReportType = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.luMill = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.rowDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.rowSectionID = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.rowReportType = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.rowMill = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            ((System.ComponentModel.ISupportInitialize)(this.pgSettingsMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luSectionID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdgrpReportType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luMill)).BeginInit();
            this.SuspendLayout();
            // 
            // pgSettingsMain
            // 
            this.pgSettingsMain.Location = new System.Drawing.Point(0, 0);
            this.pgSettingsMain.Name = "pgSettingsMain";
            this.pgSettingsMain.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgSettingsMain.Padding = new System.Windows.Forms.Padding(5);
            this.pgSettingsMain.RecordWidth = 150;
            this.pgSettingsMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.dteDate,
            this.luSectionID,
            this.rdgrpReportType,
            this.luMill});
            this.pgSettingsMain.RowHeaderWidth = 50;
            this.pgSettingsMain.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowDate,
            this.rowSectionID,
            this.rowReportType,
            this.rowMill});
            this.pgSettingsMain.Size = new System.Drawing.Size(316, 462);
            this.pgSettingsMain.TabIndex = 0;
            this.pgSettingsMain.Click += new System.EventHandler(this.pgSettingsMain_Click);
            // 
            // dteDate
            // 
            this.dteDate.AutoHeight = false;
            this.dteDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dteDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteDate.EditFormat.FormatString = "yyyy-MM-dd";
            this.dteDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteDate.Name = "dteDate";
            this.dteDate.EditValueChanged += new System.EventHandler(this.dteDate_EditValueChanged);
            // 
            // luSectionID
            // 
            this.luSectionID.AutoHeight = false;
            this.luSectionID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luSectionID.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SectionID", "Section ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.luSectionID.Name = "luSectionID";
            this.luSectionID.NullText = "[Select a Section]";
            this.luSectionID.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            this.luSectionID.EditValueChanged += new System.EventHandler(this.luSectionID_EditValueChanged);
            // 
            // rdgrpReportType
            // 
            this.rdgrpReportType.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Summary", "Summary"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Detail", "Detail")});
            this.rdgrpReportType.Name = "rdgrpReportType";
            this.rdgrpReportType.EditValueChanged += new System.EventHandler(this.rdgrpReportType_EditValueChanged);
            // 
            // luMill
            // 
            this.luMill.AutoHeight = false;
            this.luMill.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luMill.Name = "luMill";
            this.luMill.NullText = "[Select a Mill]";
            // 
            // rowDate
            // 
            this.rowDate.Name = "rowDate";
            this.rowDate.Properties.Caption = "Date";
            this.rowDate.Properties.FieldName = "CalendarDate";
            this.rowDate.Properties.RowEdit = this.dteDate;
            // 
            // rowSectionID
            // 
            this.rowSectionID.Name = "rowSectionID";
            this.rowSectionID.Properties.Caption = "Sections";
            this.rowSectionID.Properties.FieldName = "SectionID";
            this.rowSectionID.Properties.RowEdit = this.luSectionID;
            // 
            // rowReportType
            // 
            this.rowReportType.Name = "rowReportType";
            this.rowReportType.Properties.Caption = "Report Type";
            this.rowReportType.Properties.FieldName = "ReportType";
            this.rowReportType.Properties.RowEdit = this.rdgrpReportType;
            // 
            // rowMill
            // 
            this.rowMill.Name = "rowMill";
            this.rowMill.Properties.Caption = "Mills";
            this.rowMill.Properties.FieldName = "OreflowID";
            this.rowMill.Properties.RowEdit = this.luMill;
            // 
            // ucSICReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pgSettingsMain);
            this.Name = "ucSICReport";
            this.Size = new System.Drawing.Size(316, 462);
            this.Load += new System.EventHandler(this.ucSICReport_Load);
            this.Controls.SetChildIndex(this.pgSettingsMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgSettingsMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luSectionID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdgrpReportType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luMill)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgSettingsMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dteDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luSectionID;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rdgrpReportType;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow rowDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow rowSectionID;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow rowReportType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luMill;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow rowMill;
        //private TabPane tabPane1;
        //private TabNavigationPage tabNavigationPage1;
        // private TabNavigationPage tabNavigationPage2;
    }
}
