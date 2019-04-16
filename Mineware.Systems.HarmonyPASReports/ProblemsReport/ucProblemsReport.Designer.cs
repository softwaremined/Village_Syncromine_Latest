namespace Mineware.Systems.Reports.ProblemsReport
{
    partial class ucProblemsReport
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
            this.pgProblemReport = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.riReportDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.riActivity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.riIncludeGraph = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.iActivity = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iReportDate = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iType = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iIncludeGraph = new DevExpress.XtraVerticalGrid.Rows.PGridEditorRow();
            this.iProdmonth = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.FromLbl = new System.Windows.Forms.Label();
            this.ProdMonthTxt = new System.Windows.Forms.NumericUpDown();
            this.ProdMonth1Txt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pgProblemReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riIncludeGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // pgProblemReport
            // 
            this.pgProblemReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProblemReport.Location = new System.Drawing.Point(0, 0);
            this.pgProblemReport.Name = "pgProblemReport";
            this.pgProblemReport.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgProblemReport.RecordWidth = 136;
            this.pgProblemReport.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riReportDate,
            this.riActivity,
            this.riType,
            this.riIncludeGraph,
            this.repositoryItemSpinEdit1});
            this.pgProblemReport.RowHeaderWidth = 64;
            this.pgProblemReport.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iActivity,
            this.iType,
            this.iIncludeGraph,
            this.iProdmonth});
            this.pgProblemReport.Size = new System.Drawing.Size(248, 311);
            this.pgProblemReport.TabIndex = 5;
            this.pgProblemReport.Click += new System.EventHandler(this.pgProblemReport_Click);
            // 
            // riReportDate
            // 
            this.riReportDate.AutoHeight = false;
            this.riReportDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riReportDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riReportDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.riReportDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.riReportDate.EditFormat.FormatString = "yyyy-MM-dd";
            this.riReportDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.riReportDate.Mask.EditMask = "yyyy-MM-dd";
            this.riReportDate.Name = "riReportDate";
            // 
            // riActivity
            // 
            this.riActivity.AutoHeight = false;
            this.riActivity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riActivity.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Activity", "Activity"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Activity", "Activity", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.riActivity.Name = "riActivity";
            this.riActivity.NullText = "";
            // 
            // riType
            // 
            this.riType.AutoHeight = false;
            this.riType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.riType.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HierID", "HierID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HierDesc", "Level")});
            this.riType.Name = "riType";
            this.riType.NullText = "";
            // 
            // riIncludeGraph
            // 
            this.riIncludeGraph.AutoHeight = false;
            this.riIncludeGraph.Caption = "Include Graphs";
            this.riIncludeGraph.Name = "riIncludeGraph";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // iActivity
            // 
            this.iActivity.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.iReportDate});
            this.iActivity.Height = 18;
            this.iActivity.IsChildRowsLoaded = true;
            this.iActivity.Name = "iActivity";
            this.iActivity.Properties.Caption = "Activity";
            this.iActivity.Properties.FieldName = "Activity";
            this.iActivity.Properties.RowEdit = this.riActivity;
            this.iActivity.Visible = false;
            // 
            // iReportDate
            // 
            this.iReportDate.IsChildRowsLoaded = true;
            this.iReportDate.Name = "iReportDate";
            this.iReportDate.Properties.Caption = "Report Date";
            this.iReportDate.Properties.FieldName = "ReportDate";
            this.iReportDate.Properties.RowEdit = this.riReportDate;
            this.iReportDate.Visible = false;
            // 
            // iType
            // 
            this.iType.IsChildRowsLoaded = true;
            this.iType.Name = "iType";
            this.iType.Properties.Caption = "Section";
            this.iType.Properties.FieldName = "TheType";
            this.iType.Properties.RowEdit = this.riType;
            this.iType.Visible = false;
            // 
            // iIncludeGraph
            // 
            this.iIncludeGraph.IsChildRowsLoaded = true;
            this.iIncludeGraph.Name = "iIncludeGraph";
            this.iIncludeGraph.Properties.Caption = "Include Graphs";
            this.iIncludeGraph.Properties.FieldName = "IncludeGraphs";
            this.iIncludeGraph.Properties.RowEdit = this.riIncludeGraph;
            this.iIncludeGraph.Visible = false;
            // 
            // iProdmonth
            // 
            this.iProdmonth.Fixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iProdmonth.InternalFixed = DevExpress.XtraVerticalGrid.Rows.FixedStyle.Top;
            this.iProdmonth.Name = "iProdmonth";
            this.iProdmonth.Properties.FieldName = "Prodmonth";
            this.iProdmonth.Properties.RowEdit = this.repositoryItemSpinEdit1;
            this.iProdmonth.Visible = false;
            // 
            // FromLbl
            // 
            this.FromLbl.AutoSize = true;
            this.FromLbl.BackColor = System.Drawing.Color.White;
            this.FromLbl.Location = new System.Drawing.Point(9, 13);
            this.FromLbl.Name = "FromLbl";
            this.FromLbl.Size = new System.Drawing.Size(59, 13);
            this.FromLbl.TabIndex = 94;
            this.FromLbl.Text = "ProdMonth";
            // 
            // ProdMonthTxt
            // 
            this.ProdMonthTxt.Location = new System.Drawing.Point(152, 10);
            this.ProdMonthTxt.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ProdMonthTxt.Name = "ProdMonthTxt";
            this.ProdMonthTxt.Size = new System.Drawing.Size(18, 21);
            this.ProdMonthTxt.TabIndex = 93;
            this.ProdMonthTxt.Click += new System.EventHandler(this.ProdMonthTxt_Click);
            // 
            // ProdMonth1Txt
            // 
            this.ProdMonth1Txt.BackColor = System.Drawing.Color.White;
            this.ProdMonth1Txt.Location = new System.Drawing.Point(70, 10);
            this.ProdMonth1Txt.MaxLength = 10000000;
            this.ProdMonth1Txt.Name = "ProdMonth1Txt";
            this.ProdMonth1Txt.ReadOnly = true;
            this.ProdMonth1Txt.Size = new System.Drawing.Size(100, 21);
            this.ProdMonth1Txt.TabIndex = 92;
            this.ProdMonth1Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ucProblemsReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FromLbl);
            this.Controls.Add(this.ProdMonthTxt);
            this.Controls.Add(this.ProdMonth1Txt);
            this.Controls.Add(this.pgProblemReport);
            this.Name = "ucProblemsReport";
            this.Size = new System.Drawing.Size(248, 311);
            this.Load += new System.EventHandler(this.ucProblemsReport_Load);
            this.Controls.SetChildIndex(this.pgProblemReport, 0);
            this.Controls.SetChildIndex(this.ProdMonth1Txt, 0);
            this.Controls.SetChildIndex(this.ProdMonthTxt, 0);
            this.Controls.SetChildIndex(this.FromLbl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgProblemReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riIncludeGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pgProblemReport;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riReportDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riActivity;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riType;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riIncludeGraph;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iReportDate;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iActivity;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iType;
        private DevExpress.XtraVerticalGrid.Rows.PGridEditorRow iIncludeGraph;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow iProdmonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private System.Windows.Forms.Label FromLbl;
        private System.Windows.Forms.NumericUpDown ProdMonthTxt;
        private System.Windows.Forms.TextBox ProdMonth1Txt;
    }
}
