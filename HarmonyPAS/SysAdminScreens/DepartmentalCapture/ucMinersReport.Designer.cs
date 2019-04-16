namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    partial class ucMinersReport
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMinersReport));
            this.bandedGridView6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand27 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.WPSub = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.TimeSub = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.RiskCol = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.SeisCol = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridControl4 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.ColMainMOSection = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ColMainContrator = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ColMainTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand22 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand23 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand24 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.MoringPnl = new System.Windows.Forms.Panel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            this.MorningPrintbtn = new DevExpress.XtraEditors.SimpleButton();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.PC9 = new FastReport.Preview.PreviewControl();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.RCRockEngineering = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrintOnly = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrintExport = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportOnly = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnMONote = new System.Windows.Forms.Button();
            this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView4)).BeginInit();
            this.MoringPnl.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).BeginInit();
            this.SuspendLayout();
            // 
            // bandedGridView6
            // 
            this.bandedGridView6.BandPanelRowHeight = 35;
            this.bandedGridView6.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand27});
            this.bandedGridView6.ColumnPanelRowHeight = 35;
            this.bandedGridView6.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.WPSub,
            this.TimeSub,
            this.SeisCol,
            this.RiskCol});
            this.bandedGridView6.GridControl = this.gridControl4;
            this.bandedGridView6.Name = "bandedGridView6";
            this.bandedGridView6.OptionsView.ColumnAutoWidth = false;
            this.bandedGridView6.OptionsView.RowAutoHeight = true;
            this.bandedGridView6.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.bandedGridView6_RowCellClick);
            this.bandedGridView6.DoubleClick += new System.EventHandler(this.bandedGridView6_DoubleClick);
            // 
            // gridBand27
            // 
            this.gridBand27.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand27.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.gridBand27.Columns.Add(this.WPSub);
            this.gridBand27.Columns.Add(this.TimeSub);
            this.gridBand27.Columns.Add(this.RiskCol);
            this.gridBand27.Columns.Add(this.SeisCol);
            this.gridBand27.Name = "gridBand27";
            this.gridBand27.OptionsBand.AllowSize = false;
            this.gridBand27.OptionsBand.FixedWidth = true;
            this.gridBand27.VisibleIndex = 0;
            this.gridBand27.Width = 270;
            // 
            // WPSub
            // 
            this.WPSub.AppearanceHeader.Options.UseTextOptions = true;
            this.WPSub.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.WPSub.Caption = "Workplace";
            this.WPSub.Name = "WPSub";
            this.WPSub.OptionsColumn.AllowEdit = false;
            this.WPSub.OptionsColumn.AllowIncrementalSearch = false;
            this.WPSub.OptionsColumn.AllowMove = false;
            this.WPSub.OptionsColumn.AllowSize = false;
            this.WPSub.OptionsColumn.FixedWidth = true;
            this.WPSub.OptionsColumn.ReadOnly = true;
            this.WPSub.Visible = true;
            // 
            // TimeSub
            // 
            this.TimeSub.AppearanceCell.Options.UseTextOptions = true;
            this.TimeSub.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TimeSub.AppearanceHeader.Options.UseTextOptions = true;
            this.TimeSub.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.TimeSub.Caption = "Time Printed";
            this.TimeSub.Name = "TimeSub";
            this.TimeSub.OptionsColumn.AllowEdit = false;
            this.TimeSub.OptionsColumn.AllowMove = false;
            this.TimeSub.OptionsColumn.AllowSize = false;
            this.TimeSub.OptionsColumn.FixedWidth = true;
            this.TimeSub.OptionsColumn.ReadOnly = true;
            this.TimeSub.Visible = true;
            // 
            // RiskCol
            // 
            this.RiskCol.AppearanceCell.Options.UseTextOptions = true;
            this.RiskCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.RiskCol.AppearanceHeader.Options.UseTextOptions = true;
            this.RiskCol.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.RiskCol.Caption = "Risk Rating";
            this.RiskCol.Name = "RiskCol";
            this.RiskCol.OptionsColumn.AllowEdit = false;
            this.RiskCol.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.RiskCol.OptionsColumn.AllowIncrementalSearch = false;
            this.RiskCol.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.RiskCol.OptionsColumn.AllowMove = false;
            this.RiskCol.OptionsColumn.AllowSize = false;
            this.RiskCol.OptionsColumn.FixedWidth = true;
            this.RiskCol.OptionsColumn.ReadOnly = true;
            this.RiskCol.Visible = true;
            this.RiskCol.Width = 60;
            // 
            // SeisCol
            // 
            this.SeisCol.AppearanceCell.Options.UseTextOptions = true;
            this.SeisCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.SeisCol.AppearanceHeader.Options.UseTextOptions = true;
            this.SeisCol.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.SeisCol.Caption = "Seismic Rating";
            this.SeisCol.Name = "SeisCol";
            this.SeisCol.OptionsColumn.AllowEdit = false;
            this.SeisCol.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.SeisCol.OptionsColumn.AllowIncrementalSearch = false;
            this.SeisCol.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.SeisCol.OptionsColumn.AllowMove = false;
            this.SeisCol.OptionsColumn.AllowSize = false;
            this.SeisCol.OptionsColumn.FixedWidth = true;
            this.SeisCol.OptionsColumn.ReadOnly = true;
            this.SeisCol.Visible = true;
            this.SeisCol.Width = 60;
            // 
            // gridControl4
            // 
            this.gridControl4.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl4.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            gridLevelNode1.LevelTemplate = this.bandedGridView6;
            gridLevelNode1.RelationName = "Level1";
            this.gridControl4.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl4.Location = new System.Drawing.Point(4, 4);
            this.gridControl4.LookAndFeel.SkinName = "iMaginary";
            this.gridControl4.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl4.MainView = this.bandedGridView4;
            this.gridControl4.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl4.Name = "gridControl4";
            this.gridControl4.Size = new System.Drawing.Size(1176, 639);
            this.gridControl4.TabIndex = 11;
            this.gridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView4,
            this.bandedGridView6});
            this.gridControl4.Click += new System.EventHandler(this.gridControl4_Click);
            // 
            // bandedGridView4
            // 
            this.bandedGridView4.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.bandedGridView4.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bandedGridView4.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.bandedGridView4.Appearance.Row.Options.UseFont = true;
            this.bandedGridView4.BandPanelRowHeight = 35;
            this.bandedGridView4.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand20,
            this.gridBand21,
            this.gridBand24});
            this.bandedGridView4.ColumnPanelRowHeight = 35;
            this.bandedGridView4.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.ColMainMOSection,
            this.ColMainContrator,
            this.ColMainTime});
            this.bandedGridView4.FixedLineWidth = 12;
            this.bandedGridView4.GridControl = this.gridControl4;
            this.bandedGridView4.Name = "bandedGridView4";
            this.bandedGridView4.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridView4.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridView4.OptionsBehavior.Editable = false;
            this.bandedGridView4.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.bandedGridView4.OptionsView.AllowCellMerge = true;
            this.bandedGridView4.OptionsView.ColumnAutoWidth = false;
            this.bandedGridView4.OptionsView.ShowBands = false;
            this.bandedGridView4.OptionsView.ShowGroupPanel = false;
            this.bandedGridView4.OptionsView.ShowIndicator = false;
            this.bandedGridView4.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.bandedGridView4_RowCellClick);
            // 
            // gridBand20
            // 
            this.gridBand20.Columns.Add(this.ColMainMOSection);
            this.gridBand20.Columns.Add(this.ColMainContrator);
            this.gridBand20.Columns.Add(this.ColMainTime);
            this.gridBand20.Name = "gridBand20";
            this.gridBand20.VisibleIndex = 0;
            this.gridBand20.Width = 540;
            // 
            // ColMainMOSection
            // 
            this.ColMainMOSection.AppearanceCell.BorderColor = System.Drawing.Color.Transparent;
            this.ColMainMOSection.AppearanceCell.Options.UseBorderColor = true;
            this.ColMainMOSection.Caption = "Section";
            this.ColMainMOSection.Name = "ColMainMOSection";
            this.ColMainMOSection.OptionsColumn.AllowEdit = false;
            this.ColMainMOSection.OptionsColumn.AllowMove = false;
            this.ColMainMOSection.OptionsColumn.AllowSize = false;
            this.ColMainMOSection.OptionsColumn.FixedWidth = true;
            this.ColMainMOSection.OptionsColumn.ReadOnly = true;
            this.ColMainMOSection.Visible = true;
            this.ColMainMOSection.Width = 200;
            // 
            // ColMainContrator
            // 
            this.ColMainContrator.AppearanceHeader.Options.UseTextOptions = true;
            this.ColMainContrator.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColMainContrator.Caption = "Contractor";
            this.ColMainContrator.Name = "ColMainContrator";
            this.ColMainContrator.OptionsColumn.AllowEdit = false;
            this.ColMainContrator.OptionsColumn.AllowMove = false;
            this.ColMainContrator.OptionsColumn.AllowSize = false;
            this.ColMainContrator.OptionsColumn.FixedWidth = true;
            this.ColMainContrator.OptionsColumn.ReadOnly = true;
            this.ColMainContrator.Visible = true;
            this.ColMainContrator.Width = 220;
            // 
            // ColMainTime
            // 
            this.ColMainTime.AppearanceCell.Options.UseTextOptions = true;
            this.ColMainTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ColMainTime.Caption = "Time Printed";
            this.ColMainTime.Name = "ColMainTime";
            this.ColMainTime.OptionsColumn.AllowEdit = false;
            this.ColMainTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.ColMainTime.OptionsColumn.AllowMove = false;
            this.ColMainTime.OptionsColumn.AllowSize = false;
            this.ColMainTime.OptionsColumn.FixedWidth = true;
            this.ColMainTime.OptionsColumn.ReadOnly = true;
            this.ColMainTime.Visible = true;
            this.ColMainTime.Width = 120;
            // 
            // gridBand21
            // 
            this.gridBand21.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand22,
            this.gridBand23});
            this.gridBand21.Name = "gridBand21";
            this.gridBand21.OptionsBand.FixedWidth = true;
            this.gridBand21.Visible = false;
            this.gridBand21.VisibleIndex = -1;
            this.gridBand21.Width = 1454;
            // 
            // gridBand22
            // 
            this.gridBand22.Caption = "gridBand17";
            this.gridBand22.Name = "gridBand22";
            this.gridBand22.Visible = false;
            this.gridBand22.VisibleIndex = -1;
            this.gridBand22.Width = 50;
            // 
            // gridBand23
            // 
            this.gridBand23.Name = "gridBand23";
            this.gridBand23.VisibleIndex = 0;
            this.gridBand23.Width = 1454;
            // 
            // gridBand24
            // 
            this.gridBand24.Caption = "gridBand18";
            this.gridBand24.Name = "gridBand24";
            this.gridBand24.Visible = false;
            this.gridBand24.VisibleIndex = -1;
            // 
            // MoringPnl
            // 
            this.MoringPnl.Controls.Add(this.tabControl3);
            this.MoringPnl.Controls.Add(this.linkLabel1);
            this.MoringPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MoringPnl.Location = new System.Drawing.Point(0, 75);
            this.MoringPnl.Margin = new System.Windows.Forms.Padding(4);
            this.MoringPnl.Name = "MoringPnl";
            this.MoringPnl.Size = new System.Drawing.Size(1192, 673);
            this.MoringPnl.TabIndex = 83;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Controls.Add(this.tabPage6);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(1192, 673);
            this.tabControl3.TabIndex = 50;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.gridControl4);
            this.tabPage5.Controls.Add(this.simpleButton7);
            this.tabPage5.Controls.Add(this.simpleButton8);
            this.tabPage5.Controls.Add(this.MorningPrintbtn);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage5.Size = new System.Drawing.Size(1184, 647);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Data                                  ";
            // 
            // simpleButton7
            // 
            this.simpleButton7.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.simpleButton7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton7.ImageOptions.Image")));
            this.simpleButton7.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton7.Location = new System.Drawing.Point(33, 15);
            this.simpleButton7.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(180, 57);
            this.simpleButton7.TabIndex = 46;
            this.simpleButton7.Text = "&Print  Only   ";
            this.simpleButton7.Visible = false;
            // 
            // simpleButton8
            // 
            this.simpleButton8.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.simpleButton8.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton8.ImageOptions.Image")));
            this.simpleButton8.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.simpleButton8.Location = new System.Drawing.Point(560, 15);
            this.simpleButton8.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(180, 57);
            this.simpleButton8.TabIndex = 47;
            this.simpleButton8.Text = "&Export Only   ";
            this.simpleButton8.Visible = false;
            // 
            // MorningPrintbtn
            // 
            this.MorningPrintbtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.MorningPrintbtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("MorningPrintbtn.ImageOptions.Image")));
            this.MorningPrintbtn.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.MorningPrintbtn.Location = new System.Drawing.Point(301, 15);
            this.MorningPrintbtn.Margin = new System.Windows.Forms.Padding(4);
            this.MorningPrintbtn.Name = "MorningPrintbtn";
            this.MorningPrintbtn.Size = new System.Drawing.Size(180, 57);
            this.MorningPrintbtn.TabIndex = 45;
            this.MorningPrintbtn.Text = "&Print And Export   ";
            this.MorningPrintbtn.Visible = false;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.PC9);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage6.Size = new System.Drawing.Size(1184, 647);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Report                                  ";
            // 
            // PC9
            // 
            this.PC9.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.PC9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PC9.Font = new System.Drawing.Font("Tahoma", 8F);
            this.PC9.Location = new System.Drawing.Point(4, 4);
            this.PC9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PC9.Name = "PC9";
            this.PC9.PageOffset = new System.Drawing.Point(10, 10);
            this.PC9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PC9.Size = new System.Drawing.Size(1176, 639);
            this.PC9.TabIndex = 84;
            this.PC9.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(752, 54);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(70, 13);
            this.linkLabel1.TabIndex = 49;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Get PDF Files";
            this.linkLabel1.Visible = false;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(267, 22);
            this.radioGroup1.Margin = new System.Windows.Forms.Padding(4);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Day Shift"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Night Shift")});
            this.radioGroup1.Size = new System.Drawing.Size(351, 44);
            this.radioGroup1.TabIndex = 48;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // RCRockEngineering
            // 
            this.RCRockEngineering.AllowKeyTips = false;
            this.RCRockEngineering.AllowMdiChildButtons = false;
            this.RCRockEngineering.AllowMinimizeRibbon = false;
            this.RCRockEngineering.AllowTrimPageText = false;
            this.RCRockEngineering.DrawGroupsBorderMode = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ExpandCollapseItem.Id = 0;
            this.RCRockEngineering.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.RCRockEngineering.ExpandCollapseItem,
            this.barButtonItem1,
            this.btnPrintOnly,
            this.btnPrintExport,
            this.btnExportOnly});
            this.RCRockEngineering.Location = new System.Drawing.Point(0, 0);
            this.RCRockEngineering.Margin = new System.Windows.Forms.Padding(4);
            this.RCRockEngineering.MaxItemId = 11;
            this.RCRockEngineering.Name = "RCRockEngineering";
            this.RCRockEngineering.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.RCRockEngineering.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowCategoryInCaption = false;
            this.RCRockEngineering.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.RCRockEngineering.ShowToolbarCustomizeItem = false;
            this.RCRockEngineering.Size = new System.Drawing.Size(1192, 75);
            this.RCRockEngineering.Toolbar.ShowCustomizeItem = false;
            this.RCRockEngineering.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.RCRockEngineering.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.Click += new System.EventHandler(this.RCRockEngineering_Click);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "                                         ";
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btnPrintOnly
            // 
            this.btnPrintOnly.Caption = "Print Only";
            this.btnPrintOnly.Id = 8;
            this.btnPrintOnly.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintOnly.ImageOptions.Image")));
            this.btnPrintOnly.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPrintOnly.ImageOptions.LargeImage")));
            this.btnPrintOnly.Name = "btnPrintOnly";
            this.btnPrintOnly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrintOnly_ItemClick);
            // 
            // btnPrintExport
            // 
            this.btnPrintExport.Caption = "Print and Export";
            this.btnPrintExport.Id = 9;
            this.btnPrintExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintExport.ImageOptions.Image")));
            this.btnPrintExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPrintExport.ImageOptions.LargeImage")));
            this.btnPrintExport.Name = "btnPrintExport";
            this.btnPrintExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnPrintExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrintExport_ItemClick);
            // 
            // btnExportOnly
            // 
            this.btnExportOnly.Caption = "Export and  Print";
            this.btnExportOnly.Id = 10;
            this.btnExportOnly.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExportOnly.ImageOptions.Image")));
            this.btnExportOnly.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExportOnly.ImageOptions.LargeImage")));
            this.btnExportOnly.Name = "btnExportOnly";
            this.btnExportOnly.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnExportOnly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportOnly_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnPrintOnly);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnPrintExport);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExportOnly);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Actions";
            // 
            // btnMONote
            // 
            this.btnMONote.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMONote.Location = new System.Drawing.Point(778, 23);
            this.btnMONote.Name = "btnMONote";
            this.btnMONote.Size = new System.Drawing.Size(102, 43);
            this.btnMONote.TabIndex = 85;
            this.btnMONote.Text = "Create MO Note";
            this.btnMONote.UseVisualStyleBackColor = false;
            this.btnMONote.Visible = false;
            this.btnMONote.Click += new System.EventHandler(this.btnMONote_Click);
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Location = new System.Drawing.Point(670, 39);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(74, 29);
            this.pdfViewer1.TabIndex = 87;
            this.pdfViewer1.Visible = false;
            // 
            // ucMinersReport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pdfViewer1);
            this.Controls.Add(this.btnMONote);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.MoringPnl);
            this.Controls.Add(this.RCRockEngineering);
            this.Name = "ucMinersReport";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(1192, 748);
            this.Load += new System.EventHandler(this.ucMinersReport_Load);
            this.Controls.SetChildIndex(this.RCRockEngineering, 0);
            this.Controls.SetChildIndex(this.MoringPnl, 0);
            this.Controls.SetChildIndex(this.radioGroup1, 0);
            this.Controls.SetChildIndex(this.btnMONote, 0);
            this.Controls.SetChildIndex(this.pdfViewer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView4)).EndInit();
            this.MoringPnl.ResumeLayout(false);
            this.MoringPnl.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MoringPnl;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage5;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private DevExpress.XtraEditors.SimpleButton simpleButton8;
        public DevExpress.XtraGrid.GridControl gridControl4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand27;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn WPSub;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn TimeSub;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn RiskCol;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn SeisCol;
        public DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand20;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn ColMainMOSection;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn ColMainContrator;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn ColMainTime;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand21;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand22;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand23;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand24;
        private DevExpress.XtraEditors.SimpleButton MorningPrintbtn;
        private System.Windows.Forms.TabPage tabPage6;
        private FastReport.Preview.PreviewControl PC9;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private DevExpress.XtraBars.Ribbon.RibbonControl RCRockEngineering;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnPrintOnly;
        private DevExpress.XtraBars.BarButtonItem btnPrintExport;
        private DevExpress.XtraBars.BarButtonItem btnExportOnly;
        private System.Windows.Forms.Button btnMONote;
        private DevExpress.XtraPdfViewer.PdfViewer pdfViewer1;
    }
}
