namespace Mineware.Systems.Reports.ProblemHistoryGraph
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
            this.ToLbl = new System.Windows.Forms.Label();
            this.ToDate = new System.Windows.Forms.DateTimePicker();
            this.FromDate = new System.Windows.Forms.DateTimePicker();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.ProblemsRgb = new DevExpress.XtraEditors.RadioGroup();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ProbGroupCmb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HQCatCmb = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NoBlastsLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.NoDualBlastsLbl = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.NoProblemsLbl = new System.Windows.Forms.Label();
            this.LostBlastLbl = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.UnderSectionCmb = new System.Windows.Forms.ComboBox();
            this.FromLbl = new System.Windows.Forms.Label();
            this.ProdMonthTxt = new System.Windows.Forms.NumericUpDown();
            this.ProdMonth1Txt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.OuncesBtn = new DevExpress.XtraEditors.SimpleButton();
            this.TonsBtn = new DevExpress.XtraEditors.SimpleButton();
            this.SqmAdvBtn = new DevExpress.XtraEditors.SimpleButton();
            this.NumberBtn = new DevExpress.XtraEditors.SimpleButton();
            this.TypeRgb = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.pgProblemReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riIncludeGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProblemsRgb.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeRgb.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pgProblemReport
            // 
            this.pgProblemReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProblemReport.Location = new System.Drawing.Point(0, 0);
            this.pgProblemReport.Name = "pgProblemReport";
            this.pgProblemReport.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.pgProblemReport.Padding = new System.Windows.Forms.Padding(5);
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
            this.pgProblemReport.Size = new System.Drawing.Size(300, 492);
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
            // ToLbl
            // 
            this.ToLbl.AutoSize = true;
            this.ToLbl.BackColor = System.Drawing.Color.White;
            this.ToLbl.Location = new System.Drawing.Point(114, 57);
            this.ToLbl.Name = "ToLbl";
            this.ToLbl.Size = new System.Drawing.Size(19, 13);
            this.ToLbl.TabIndex = 80;
            this.ToLbl.Text = "To";
            this.ToLbl.Visible = false;
            // 
            // ToDate
            // 
            this.ToDate.CustomFormat = "ddd dd-MMM-yyyy";
            this.ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDate.Location = new System.Drawing.Point(117, 73);
            this.ToDate.Name = "ToDate";
            this.ToDate.Size = new System.Drawing.Size(124, 21);
            this.ToDate.TabIndex = 79;
            this.ToDate.Visible = false;
            // 
            // FromDate
            // 
            this.FromDate.CustomFormat = "ddd dd-MMM-yyyy";
            this.FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDate.Location = new System.Drawing.Point(117, 34);
            this.FromDate.Name = "FromDate";
            this.FromDate.Size = new System.Drawing.Size(124, 21);
            this.FromDate.TabIndex = 78;
            this.FromDate.Visible = false;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(12, 15);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Prodmonth"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "From To"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Last 7 Days"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Last 30 Days")});
            this.radioGroup1.Size = new System.Drawing.Size(99, 81);
            this.radioGroup1.TabIndex = 77;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // ProblemsRgb
            // 
            this.ProblemsRgb.Location = new System.Drawing.Point(12, 173);
            this.ProblemsRgb.Name = "ProblemsRgb";
            this.ProblemsRgb.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.ProblemsRgb.Properties.Appearance.Options.UseBackColor = true;
            this.ProblemsRgb.Properties.Columns = 1;
            this.ProblemsRgb.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "All Problems"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lost Blasts Optimal", false),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "No Lost Blasts"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Plan To Be Blasted Lost Blast")});
            this.ProblemsRgb.Size = new System.Drawing.Size(271, 105);
            this.ProblemsRgb.TabIndex = 76;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 335);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(70, 61);
            this.textBox1.TabIndex = 81;
            this.textBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(55, 422);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 85;
            this.label3.Text = "Prob Group";
            this.label3.Visible = false;
            // 
            // ProbGroupCmb
            // 
            this.ProbGroupCmb.BackColor = System.Drawing.Color.White;
            this.ProbGroupCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProbGroupCmb.FormattingEnabled = true;
            this.ProbGroupCmb.Location = new System.Drawing.Point(58, 438);
            this.ProbGroupCmb.Name = "ProbGroupCmb";
            this.ProbGroupCmb.Size = new System.Drawing.Size(210, 21);
            this.ProbGroupCmb.TabIndex = 84;
            this.ProbGroupCmb.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(74, 406);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "HO Cat";
            this.label2.Visible = false;
            // 
            // HQCatCmb
            // 
            this.HQCatCmb.BackColor = System.Drawing.Color.White;
            this.HQCatCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HQCatCmb.FormattingEnabled = true;
            this.HQCatCmb.Location = new System.Drawing.Point(77, 422);
            this.HQCatCmb.Name = "HQCatCmb";
            this.HQCatCmb.Size = new System.Drawing.Size(210, 21);
            this.HQCatCmb.TabIndex = 82;
            this.HQCatCmb.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.NoBlastsLbl);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.NoDualBlastsLbl);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.NoProblemsLbl);
            this.panel3.Controls.Add(this.LostBlastLbl);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Location = new System.Drawing.Point(29, 326);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(278, 77);
            this.panel3.TabIndex = 86;
            this.panel3.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(109, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 61;
            this.label5.Text = "Summary";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Nº Blasts:";
            // 
            // NoBlastsLbl
            // 
            this.NoBlastsLbl.Location = new System.Drawing.Point(99, 27);
            this.NoBlastsLbl.Name = "NoBlastsLbl";
            this.NoBlastsLbl.Size = new System.Drawing.Size(35, 13);
            this.NoBlastsLbl.TabIndex = 54;
            this.NoBlastsLbl.Text = "0";
            this.NoBlastsLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 55;
            this.label6.Text = "Nº of Dual Panels:";
            // 
            // NoDualBlastsLbl
            // 
            this.NoDualBlastsLbl.Location = new System.Drawing.Point(99, 56);
            this.NoDualBlastsLbl.Name = "NoDualBlastsLbl";
            this.NoDualBlastsLbl.Size = new System.Drawing.Size(35, 13);
            this.NoDualBlastsLbl.TabIndex = 56;
            this.NoDualBlastsLbl.Text = "0";
            this.NoDualBlastsLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(150, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 57;
            this.label8.Text = "Nº Problems:";
            // 
            // NoProblemsLbl
            // 
            this.NoProblemsLbl.Location = new System.Drawing.Point(221, 27);
            this.NoProblemsLbl.Name = "NoProblemsLbl";
            this.NoProblemsLbl.Size = new System.Drawing.Size(41, 13);
            this.NoProblemsLbl.TabIndex = 58;
            this.NoProblemsLbl.Text = "0";
            this.NoProblemsLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LostBlastLbl
            // 
            this.LostBlastLbl.Location = new System.Drawing.Point(221, 54);
            this.LostBlastLbl.Name = "LostBlastLbl";
            this.LostBlastLbl.Size = new System.Drawing.Size(41, 13);
            this.LostBlastLbl.TabIndex = 60;
            this.LostBlastLbl.Text = "0";
            this.LostBlastLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(151, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 59;
            this.label10.Text = "Lost Blasts";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(9, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 88;
            this.label9.Text = "Section:";
            // 
            // UnderSectionCmb
            // 
            this.UnderSectionCmb.BackColor = System.Drawing.Color.White;
            this.UnderSectionCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UnderSectionCmb.FormattingEnabled = true;
            this.UnderSectionCmb.Location = new System.Drawing.Point(12, 112);
            this.UnderSectionCmb.Name = "UnderSectionCmb";
            this.UnderSectionCmb.Size = new System.Drawing.Size(152, 21);
            this.UnderSectionCmb.TabIndex = 87;
            this.UnderSectionCmb.SelectedIndexChanged += new System.EventHandler(this.UnderSectionCmb_SelectedIndexChanged);
            // 
            // FromLbl
            // 
            this.FromLbl.AutoSize = true;
            this.FromLbl.BackColor = System.Drawing.Color.White;
            this.FromLbl.Location = new System.Drawing.Point(114, 15);
            this.FromLbl.Name = "FromLbl";
            this.FromLbl.Size = new System.Drawing.Size(91, 13);
            this.FromLbl.TabIndex = 91;
            this.FromLbl.Text = "Production Month";
            // 
            // ProdMonthTxt
            // 
            this.ProdMonthTxt.Location = new System.Drawing.Point(199, 32);
            this.ProdMonthTxt.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ProdMonthTxt.Name = "ProdMonthTxt";
            this.ProdMonthTxt.Size = new System.Drawing.Size(18, 21);
            this.ProdMonthTxt.TabIndex = 90;
            this.ProdMonthTxt.ValueChanged += new System.EventHandler(this.ProdMonthTxt_ValueChanged);
            this.ProdMonthTxt.Click += new System.EventHandler(this.ProdMonthTxt_Click);
            // 
            // ProdMonth1Txt
            // 
            this.ProdMonth1Txt.BackColor = System.Drawing.Color.White;
            this.ProdMonth1Txt.Location = new System.Drawing.Point(117, 31);
            this.ProdMonth1Txt.MaxLength = 10000000;
            this.ProdMonth1Txt.Name = "ProdMonth1Txt";
            this.ProdMonth1Txt.ReadOnly = true;
            this.ProdMonth1Txt.Size = new System.Drawing.Size(100, 21);
            this.ProdMonth1Txt.TabIndex = 89;
            this.ProdMonth1Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 310);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 92;
            this.label11.Text = "label11";
            this.label11.Visible = false;
            // 
            // OuncesBtn
            // 
            this.OuncesBtn.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.OuncesBtn.Appearance.Options.UseBackColor = true;
            this.OuncesBtn.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.OuncesBtn.AppearancePressed.Options.UseBackColor = true;
            this.OuncesBtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.OuncesBtn.Location = new System.Drawing.Point(220, 289);
            this.OuncesBtn.Name = "OuncesBtn";
            this.OuncesBtn.Size = new System.Drawing.Size(63, 19);
            this.OuncesBtn.TabIndex = 98;
            this.OuncesBtn.Text = "Kgs";
            this.OuncesBtn.Click += new System.EventHandler(this.OuncesBtn_Click);
            // 
            // TonsBtn
            // 
            this.TonsBtn.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TonsBtn.Appearance.Options.UseBackColor = true;
            this.TonsBtn.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.TonsBtn.AppearancePressed.Options.UseBackColor = true;
            this.TonsBtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.TonsBtn.Location = new System.Drawing.Point(150, 289);
            this.TonsBtn.Name = "TonsBtn";
            this.TonsBtn.Size = new System.Drawing.Size(63, 19);
            this.TonsBtn.TabIndex = 97;
            this.TonsBtn.Text = "Tons";
            this.TonsBtn.Click += new System.EventHandler(this.TonsBtn_Click);
            // 
            // SqmAdvBtn
            // 
            this.SqmAdvBtn.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SqmAdvBtn.Appearance.Options.UseBackColor = true;
            this.SqmAdvBtn.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.SqmAdvBtn.AppearancePressed.Options.UseBackColor = true;
            this.SqmAdvBtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.SqmAdvBtn.Location = new System.Drawing.Point(81, 288);
            this.SqmAdvBtn.Name = "SqmAdvBtn";
            this.SqmAdvBtn.Size = new System.Drawing.Size(63, 20);
            this.SqmAdvBtn.TabIndex = 96;
            this.SqmAdvBtn.Text = "Sqm";
            this.SqmAdvBtn.Click += new System.EventHandler(this.SqmAdvBtn_Click);
            // 
            // NumberBtn
            // 
            this.NumberBtn.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NumberBtn.Appearance.Options.UseBackColor = true;
            this.NumberBtn.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.NumberBtn.AppearancePressed.Options.UseBackColor = true;
            this.NumberBtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.NumberBtn.Location = new System.Drawing.Point(12, 289);
            this.NumberBtn.Name = "NumberBtn";
            this.NumberBtn.Size = new System.Drawing.Size(63, 19);
            this.NumberBtn.TabIndex = 95;
            this.NumberBtn.Text = "Number";
            this.NumberBtn.Click += new System.EventHandler(this.NumberBtn_Click);
            // 
            // TypeRgb
            // 
            this.TypeRgb.Location = new System.Drawing.Point(12, 139);
            this.TypeRgb.Name = "TypeRgb";
            this.TypeRgb.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.TypeRgb.Properties.Appearance.Options.UseBackColor = true;
            this.TypeRgb.Properties.Columns = 3;
            this.TypeRgb.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Development"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Vamping")});
            this.TypeRgb.Size = new System.Drawing.Size(271, 28);
            this.TypeRgb.TabIndex = 99;
            this.TypeRgb.SelectedIndexChanged += new System.EventHandler(this.TypeRgb_SelectedIndexChanged);
            // 
            // ucProblemsReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ToLbl);
            this.Controls.Add(this.ToDate);
            this.Controls.Add(this.FromDate);
            this.Controls.Add(this.TypeRgb);
            this.Controls.Add(this.OuncesBtn);
            this.Controls.Add(this.TonsBtn);
            this.Controls.Add(this.SqmAdvBtn);
            this.Controls.Add(this.NumberBtn);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.FromLbl);
            this.Controls.Add(this.ProdMonthTxt);
            this.Controls.Add(this.ProdMonth1Txt);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.UnderSectionCmb);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ProbGroupCmb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HQCatCmb);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.ProblemsRgb);
            this.Controls.Add(this.pgProblemReport);
            this.Name = "ucProblemsReport";
            this.Size = new System.Drawing.Size(300, 492);
            this.Load += new System.EventHandler(this.ucProblemsReport_Load);
            this.Controls.SetChildIndex(this.pgProblemReport, 0);
            this.Controls.SetChildIndex(this.ProblemsRgb, 0);
            this.Controls.SetChildIndex(this.radioGroup1, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.HQCatCmb, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ProbGroupCmb, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.UnderSectionCmb, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.ProdMonth1Txt, 0);
            this.Controls.SetChildIndex(this.ProdMonthTxt, 0);
            this.Controls.SetChildIndex(this.FromLbl, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.NumberBtn, 0);
            this.Controls.SetChildIndex(this.SqmAdvBtn, 0);
            this.Controls.SetChildIndex(this.TonsBtn, 0);
            this.Controls.SetChildIndex(this.OuncesBtn, 0);
            this.Controls.SetChildIndex(this.TypeRgb, 0);
            this.Controls.SetChildIndex(this.FromDate, 0);
            this.Controls.SetChildIndex(this.ToDate, 0);
            this.Controls.SetChildIndex(this.ToLbl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pgProblemReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riReportDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riIncludeGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProblemsRgb.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeRgb.Properties)).EndInit();
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
        private System.Windows.Forms.Label ToLbl;
        private System.Windows.Forms.DateTimePicker ToDate;
        private System.Windows.Forms.DateTimePicker FromDate;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label FromLbl;
        private System.Windows.Forms.NumericUpDown ProdMonthTxt;
        private System.Windows.Forms.TextBox ProdMonth1Txt;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.SimpleButton OuncesBtn;
        private DevExpress.XtraEditors.SimpleButton TonsBtn;
        private DevExpress.XtraEditors.SimpleButton SqmAdvBtn;
        private DevExpress.XtraEditors.SimpleButton NumberBtn;
        public DevExpress.XtraEditors.RadioGroup ProblemsRgb;
        public System.Windows.Forms.ComboBox ProbGroupCmb;
        public System.Windows.Forms.ComboBox HQCatCmb;
        public System.Windows.Forms.Label NoBlastsLbl;
        public System.Windows.Forms.Label NoDualBlastsLbl;
        public System.Windows.Forms.Label NoProblemsLbl;
        public System.Windows.Forms.Label LostBlastLbl;
        public System.Windows.Forms.ComboBox UnderSectionCmb;
        public DevExpress.XtraEditors.RadioGroup TypeRgb;
    }
}
