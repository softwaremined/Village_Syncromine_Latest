namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    partial class ucMinersNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMinersNotes));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gvWPLevel = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colWPChecked = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemImageComboBox13 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.colWPID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcMOWorkplaces = new DevExpress.XtraGrid.GridControl();
            this.gvMoLevel = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSection = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colWorkplace = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colLatestMoNote = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colChecked = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colworkplaceID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.Savebtn = new DevExpress.XtraBars.BarButtonItem();
            this.Closebtn = new DevExpress.XtraBars.BarButtonItem();
            this.RockEnginAddImagebtn = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.RCRockEngineering = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.Notestxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvWPLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMOWorkplaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMoLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).BeginInit();
            this.SuspendLayout();
            // 
            // gvWPLevel
            // 
            this.gvWPLevel.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand21});
            this.gvWPLevel.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colWPID,
            this.colDate,
            this.colNote,
            this.colWPChecked});
            this.gvWPLevel.GridControl = this.gcMOWorkplaces;
            this.gvWPLevel.Name = "gvWPLevel";
            this.gvWPLevel.OptionsFind.AlwaysVisible = true;
            this.gvWPLevel.OptionsView.ColumnAutoWidth = false;
            this.gvWPLevel.OptionsView.ShowBands = false;
            this.gvWPLevel.OptionsView.ShowGroupPanel = false;
            this.gvWPLevel.OptionsView.ShowIndicator = false;
            this.gvWPLevel.DoubleClick += new System.EventHandler(this.gvWPLevel_DoubleClick);
            // 
            // gridBand21
            // 
            this.gridBand21.Columns.Add(this.colDate);
            this.gridBand21.Columns.Add(this.colNote);
            this.gridBand21.Columns.Add(this.colWPChecked);
            this.gridBand21.Name = "gridBand21";
            this.gridBand21.VisibleIndex = 0;
            this.gridBand21.Width = 395;
            // 
            // colDate
            // 
            this.colDate.Caption = "Date";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDate.OptionsColumn.AllowMove = false;
            this.colDate.OptionsColumn.AllowShowHide = false;
            this.colDate.OptionsColumn.AllowSize = false;
            this.colDate.OptionsColumn.FixedWidth = true;
            this.colDate.OptionsFilter.AllowAutoFilter = false;
            this.colDate.OptionsFilter.AllowFilter = false;
            this.colDate.Visible = true;
            this.colDate.Width = 95;
            // 
            // colNote
            // 
            this.colNote.Caption = "Notes";
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.AllowEdit = false;
            this.colNote.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colNote.OptionsColumn.AllowMove = false;
            this.colNote.OptionsColumn.AllowSize = false;
            this.colNote.OptionsColumn.FixedWidth = true;
            this.colNote.OptionsFilter.AllowAutoFilter = false;
            this.colNote.OptionsFilter.AllowFilter = false;
            this.colNote.Visible = true;
            this.colNote.Width = 300;
            // 
            // colWPChecked
            // 
            this.colWPChecked.AppearanceHeader.ForeColor = System.Drawing.Color.Transparent;
            this.colWPChecked.AppearanceHeader.Options.UseForeColor = true;
            this.colWPChecked.Caption = "a";
            this.colWPChecked.ColumnEdit = this.repositoryItemImageComboBox13;
            this.colWPChecked.Name = "colWPChecked";
            this.colWPChecked.OptionsColumn.AllowEdit = false;
            this.colWPChecked.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colWPChecked.OptionsColumn.AllowMove = false;
            this.colWPChecked.OptionsColumn.AllowSize = false;
            this.colWPChecked.OptionsColumn.FixedWidth = true;
            this.colWPChecked.Width = 50;
            // 
            // repositoryItemImageComboBox13
            // 
            this.repositoryItemImageComboBox13.AutoHeight = false;
            this.repositoryItemImageComboBox13.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox13.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "Y", 0)});
            this.repositoryItemImageComboBox13.Name = "repositoryItemImageComboBox13";
            this.repositoryItemImageComboBox13.SmallImages = this.imageCollection1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "check.png");
            // 
            // colWPID
            // 
            this.colWPID.Caption = "bandedGridColumn38";
            this.colWPID.Name = "colWPID";
            this.colWPID.OptionsColumn.FixedWidth = true;
            // 
            // gcMOWorkplaces
            // 
            this.gcMOWorkplaces.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcMOWorkplaces.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            gridLevelNode1.LevelTemplate = this.gvWPLevel;
            gridLevelNode1.RelationName = "Level1";
            this.gcMOWorkplaces.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcMOWorkplaces.Location = new System.Drawing.Point(0, 75);
            this.gcMOWorkplaces.MainView = this.gvMoLevel;
            this.gcMOWorkplaces.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcMOWorkplaces.Name = "gcMOWorkplaces";
            this.gcMOWorkplaces.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox13});
            this.gcMOWorkplaces.Size = new System.Drawing.Size(495, 622);
            this.gcMOWorkplaces.TabIndex = 86;
            this.gcMOWorkplaces.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMoLevel,
            this.gvWPLevel});
            // 
            // gvMoLevel
            // 
            this.gvMoLevel.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand20});
            this.gvMoLevel.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colworkplaceID,
            this.colWorkplace,
            this.colChecked,
            this.colLatestMoNote,
            this.colSection});
            this.gvMoLevel.GridControl = this.gcMOWorkplaces;
            this.gvMoLevel.Name = "gvMoLevel";
            this.gvMoLevel.OptionsSelection.MultiSelect = true;
            this.gvMoLevel.OptionsView.ColumnAutoWidth = false;
            this.gvMoLevel.OptionsView.ShowIndicator = false;
            this.gvMoLevel.DoubleClick += new System.EventHandler(this.gvMoLevel_DoubleClick);
            // 
            // gridBand20
            // 
            this.gridBand20.Columns.Add(this.colSection);
            this.gridBand20.Columns.Add(this.colWorkplace);
            this.gridBand20.Columns.Add(this.colLatestMoNote);
            this.gridBand20.Columns.Add(this.colChecked);
            this.gridBand20.Name = "gridBand20";
            this.gridBand20.VisibleIndex = 0;
            this.gridBand20.Width = 647;
            // 
            // colSection
            // 
            this.colSection.Caption = "Section";
            this.colSection.Name = "colSection";
            this.colSection.OptionsColumn.AllowEdit = false;
            this.colSection.Visible = true;
            this.colSection.Width = 72;
            // 
            // colWorkplace
            // 
            this.colWorkplace.AppearanceHeader.Options.UseTextOptions = true;
            this.colWorkplace.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colWorkplace.Caption = "Workplace";
            this.colWorkplace.Name = "colWorkplace";
            this.colWorkplace.OptionsColumn.AllowEdit = false;
            this.colWorkplace.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colWorkplace.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colWorkplace.OptionsColumn.AllowMove = false;
            this.colWorkplace.OptionsColumn.AllowShowHide = false;
            this.colWorkplace.OptionsColumn.AllowSize = false;
            this.colWorkplace.OptionsColumn.FixedWidth = true;
            this.colWorkplace.OptionsFilter.AllowAutoFilter = false;
            this.colWorkplace.OptionsFilter.AllowFilter = false;
            this.colWorkplace.Visible = true;
            this.colWorkplace.Width = 120;
            // 
            // colLatestMoNote
            // 
            this.colLatestMoNote.Caption = "Latest Note";
            this.colLatestMoNote.Name = "colLatestMoNote";
            this.colLatestMoNote.OptionsColumn.AllowEdit = false;
            this.colLatestMoNote.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colLatestMoNote.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colLatestMoNote.OptionsColumn.AllowMove = false;
            this.colLatestMoNote.OptionsColumn.AllowShowHide = false;
            this.colLatestMoNote.OptionsColumn.AllowSize = false;
            this.colLatestMoNote.OptionsColumn.FixedWidth = true;
            this.colLatestMoNote.OptionsFilter.AllowAutoFilter = false;
            this.colLatestMoNote.OptionsFilter.AllowFilter = false;
            this.colLatestMoNote.Visible = true;
            this.colLatestMoNote.Width = 455;
            // 
            // colChecked
            // 
            this.colChecked.AppearanceHeader.ForeColor = System.Drawing.Color.Transparent;
            this.colChecked.AppearanceHeader.Options.UseForeColor = true;
            this.colChecked.Caption = "a";
            this.colChecked.ColumnEdit = this.repositoryItemImageComboBox13;
            this.colChecked.Name = "colChecked";
            this.colChecked.OptionsColumn.AllowEdit = false;
            this.colChecked.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colChecked.OptionsColumn.AllowMove = false;
            this.colChecked.OptionsColumn.AllowSize = false;
            this.colChecked.OptionsColumn.FixedWidth = true;
            this.colChecked.Width = 50;
            // 
            // colworkplaceID
            // 
            this.colworkplaceID.Caption = "bandedGridColumn1";
            this.colworkplaceID.Name = "colworkplaceID";
            this.colworkplaceID.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
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
            this.ribbonPageGroup1.ItemLinks.Add(this.Savebtn);
            this.ribbonPageGroup1.ItemLinks.Add(this.Closebtn);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // Savebtn
            // 
            this.Savebtn.Caption = "Save";
            this.Savebtn.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.Savebtn.Id = 5;
            this.Savebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Savebtn.ImageOptions.Image")));
            this.Savebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Savebtn.ImageOptions.LargeImage")));
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Savebtn_ItemClick);
            // 
            // Closebtn
            // 
            this.Closebtn.Caption = "Close";
            this.Closebtn.Id = 8;
            this.Closebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Closebtn.ImageOptions.Image")));
            this.Closebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Closebtn.ImageOptions.LargeImage")));
            this.Closebtn.Name = "Closebtn";
            this.Closebtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Closebtn_ItemClick);
            // 
            // RockEnginAddImagebtn
            // 
            this.RockEnginAddImagebtn.Caption = "Add Image";
            this.RockEnginAddImagebtn.Id = 7;
            this.RockEnginAddImagebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RockEnginAddImagebtn.ImageOptions.Image")));
            this.RockEnginAddImagebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RockEnginAddImagebtn.ImageOptions.LargeImage")));
            this.RockEnginAddImagebtn.Name = "RockEnginAddImagebtn";
            this.RockEnginAddImagebtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "                                         ";
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.Name = "barButtonItem1";
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
            this.Savebtn,
            this.RockEnginAddImagebtn,
            this.Closebtn});
            this.RCRockEngineering.Location = new System.Drawing.Point(0, 0);
            this.RCRockEngineering.Margin = new System.Windows.Forms.Padding(4);
            this.RCRockEngineering.MaxItemId = 9;
            this.RCRockEngineering.Name = "RCRockEngineering";
            this.RCRockEngineering.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.RCRockEngineering.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowCategoryInCaption = false;
            this.RCRockEngineering.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.RCRockEngineering.ShowToolbarCustomizeItem = false;
            this.RCRockEngineering.Size = new System.Drawing.Size(1111, 75);
            this.RCRockEngineering.Toolbar.ShowCustomizeItem = false;
            this.RCRockEngineering.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.RCRockEngineering.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.Click += new System.EventHandler(this.RCRockEngineering_Click);
            // 
            // Notestxt
            // 
            this.Notestxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Notestxt.Location = new System.Drawing.Point(495, 88);
            this.Notestxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Notestxt.MaxLength = 300;
            this.Notestxt.Multiline = true;
            this.Notestxt.Name = "Notestxt";
            this.Notestxt.Size = new System.Drawing.Size(616, 609);
            this.Notestxt.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(495, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "Mo Note";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(3, 113);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(342, 13);
            this.label11.TabIndex = 87;
            this.label11.Text = "To Select multiple workplaces press Shift and Left Click the workplaces";
            // 
            // ucMinersNotes
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Notestxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gcMOWorkplaces);
            this.Controls.Add(this.RCRockEngineering);
            this.Name = "ucMinersNotes";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(1111, 697);
            this.Load += new System.EventHandler(this.ucMinersNotes_Load);
            this.Controls.SetChildIndex(this.RCRockEngineering, 0);
            this.Controls.SetChildIndex(this.gcMOWorkplaces, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.Notestxt, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvWPLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMOWorkplaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMoLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem Savebtn;
        private DevExpress.XtraBars.BarButtonItem Closebtn;
        private DevExpress.XtraBars.BarButtonItem RockEnginAddImagebtn;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.Ribbon.RibbonControl RCRockEngineering;
        private System.Windows.Forms.TextBox Notestxt;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gcMOWorkplaces;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvWPLevel;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand21;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNote;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWPChecked;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox13;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWPID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMoLevel;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand20;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSection;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWorkplace;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colLatestMoNote;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colChecked;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colworkplaceID;
        private System.Windows.Forms.Label label11;
    }
}
