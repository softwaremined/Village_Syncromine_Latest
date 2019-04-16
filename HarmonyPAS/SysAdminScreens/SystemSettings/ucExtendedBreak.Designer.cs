namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    partial class ucExtendedBreak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucExtendedBreak));
            this.RCRockEngineering = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.RockEnginAddImagebtn = new DevExpress.XtraBars.BarButtonItem();
            this.Closebtn = new DevExpress.XtraBars.BarButtonItem();
            this.WorkplaceEdit = new DevExpress.XtraBars.BarEditItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gcExtendedBreaks = new DevExpress.XtraGrid.GridControl();
            this.gvExtendedBreaks = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSection = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colInitiateDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDaysBefore = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDescription = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExtendedBreaks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExtendedBreaks)).BeginInit();
            this.SuspendLayout();
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
            this.btnAdd,
            this.RockEnginAddImagebtn,
            this.Closebtn,
            this.WorkplaceEdit});
            this.RCRockEngineering.Location = new System.Drawing.Point(0, 0);
            this.RCRockEngineering.MaxItemId = 10;
            this.RCRockEngineering.Name = "RCRockEngineering";
            this.RCRockEngineering.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.RCRockEngineering.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowCategoryInCaption = false;
            this.RCRockEngineering.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.RCRockEngineering.ShowToolbarCustomizeItem = false;
            this.RCRockEngineering.Size = new System.Drawing.Size(867, 75);
            this.RCRockEngineering.Toolbar.ShowCustomizeItem = false;
            this.RCRockEngineering.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.RCRockEngineering.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "                                         ";
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "Add Extended Break";
            this.btnAdd.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.btnAdd.Id = 5;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.LargeImage")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);
            // 
            // RockEnginAddImagebtn
            // 
            this.RockEnginAddImagebtn.Caption = "Add Image";
            this.RockEnginAddImagebtn.Id = 7;
            this.RockEnginAddImagebtn.Name = "RockEnginAddImagebtn";
            this.RockEnginAddImagebtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // Closebtn
            // 
            this.Closebtn.Caption = "Close";
            this.Closebtn.Id = 8;
            this.Closebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Closebtn.ImageOptions.Image")));
            this.Closebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Closebtn.ImageOptions.LargeImage")));
            this.Closebtn.Name = "Closebtn";
            this.Closebtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.Closebtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Closebtn_ItemClick);
            // 
            // WorkplaceEdit
            // 
            this.WorkplaceEdit.Caption = "Workplace";
            this.WorkplaceEdit.Edit = null;
            this.WorkplaceEdit.EditWidth = 250;
            this.WorkplaceEdit.Id = 9;
            this.WorkplaceEdit.Name = "WorkplaceEdit";
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
            this.ribbonPageGroup1.ItemLinks.Add(this.btnAdd);
            this.ribbonPageGroup1.ItemLinks.Add(this.Closebtn);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // gcExtendedBreaks
            // 
            this.gcExtendedBreaks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcExtendedBreaks.Location = new System.Drawing.Point(0, 75);
            this.gcExtendedBreaks.MainView = this.gvExtendedBreaks;
            this.gcExtendedBreaks.Name = "gcExtendedBreaks";
            this.gcExtendedBreaks.Size = new System.Drawing.Size(867, 556);
            this.gcExtendedBreaks.TabIndex = 81;
            this.gcExtendedBreaks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExtendedBreaks});
            // 
            // gvExtendedBreaks
            // 
            this.gvExtendedBreaks.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.gvExtendedBreaks.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colSection,
            this.colInitiateDate,
            this.colDaysBefore,
            this.colStartDate,
            this.colEndDate,
            this.colDescription});
            this.gvExtendedBreaks.GridControl = this.gcExtendedBreaks;
            this.gvExtendedBreaks.Name = "gvExtendedBreaks";
            this.gvExtendedBreaks.OptionsView.ColumnAutoWidth = false;
            this.gvExtendedBreaks.OptionsView.ShowBands = false;
            this.gvExtendedBreaks.OptionsView.ShowGroupPanel = false;
            this.gvExtendedBreaks.OptionsView.ShowIndicator = false;
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.colSection);
            this.gridBand1.Columns.Add(this.colInitiateDate);
            this.gridBand1.Columns.Add(this.colDaysBefore);
            this.gridBand1.Columns.Add(this.colStartDate);
            this.gridBand1.Columns.Add(this.colEndDate);
            this.gridBand1.Columns.Add(this.colDescription);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 639;
            // 
            // colSection
            // 
            this.colSection.AppearanceCell.Options.UseTextOptions = true;
            this.colSection.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colSection.AppearanceHeader.Options.UseTextOptions = true;
            this.colSection.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSection.Caption = "Section";
            this.colSection.Name = "colSection";
            this.colSection.OptionsColumn.AllowEdit = false;
            this.colSection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colSection.OptionsColumn.AllowMove = false;
            this.colSection.OptionsColumn.AllowSize = false;
            this.colSection.OptionsColumn.FixedWidth = true;
            this.colSection.OptionsFilter.AllowAutoFilter = false;
            this.colSection.OptionsFilter.AllowFilter = false;
            this.colSection.Visible = true;
            this.colSection.Width = 100;
            // 
            // colInitiateDate
            // 
            this.colInitiateDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colInitiateDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInitiateDate.Caption = "Iniate Date";
            this.colInitiateDate.Name = "colInitiateDate";
            this.colInitiateDate.OptionsColumn.AllowEdit = false;
            this.colInitiateDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colInitiateDate.OptionsColumn.AllowMove = false;
            this.colInitiateDate.OptionsColumn.AllowSize = false;
            this.colInitiateDate.OptionsColumn.FixedWidth = true;
            this.colInitiateDate.OptionsFilter.AllowAutoFilter = false;
            this.colInitiateDate.OptionsFilter.AllowFilter = false;
            this.colInitiateDate.Visible = true;
            this.colInitiateDate.Width = 83;
            // 
            // colDaysBefore
            // 
            this.colDaysBefore.AppearanceHeader.Options.UseTextOptions = true;
            this.colDaysBefore.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDaysBefore.Caption = "Days Before";
            this.colDaysBefore.Name = "colDaysBefore";
            this.colDaysBefore.OptionsColumn.AllowEdit = false;
            this.colDaysBefore.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDaysBefore.OptionsColumn.AllowMove = false;
            this.colDaysBefore.OptionsColumn.AllowSize = false;
            this.colDaysBefore.OptionsColumn.FixedWidth = true;
            this.colDaysBefore.OptionsFilter.AllowAutoFilter = false;
            this.colDaysBefore.OptionsFilter.AllowFilter = false;
            this.colDaysBefore.Visible = true;
            this.colDaysBefore.Width = 80;
            // 
            // colStartDate
            // 
            this.colStartDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartDate.Caption = "Start Date";
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.OptionsColumn.AllowEdit = false;
            this.colStartDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colStartDate.OptionsColumn.AllowMove = false;
            this.colStartDate.OptionsColumn.AllowSize = false;
            this.colStartDate.OptionsColumn.FixedWidth = true;
            this.colStartDate.OptionsFilter.AllowAutoFilter = false;
            this.colStartDate.OptionsFilter.AllowFilter = false;
            this.colStartDate.Visible = true;
            this.colStartDate.Width = 83;
            // 
            // colEndDate
            // 
            this.colEndDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndDate.Caption = "EndDate";
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.OptionsColumn.AllowEdit = false;
            this.colEndDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colEndDate.OptionsColumn.AllowMove = false;
            this.colEndDate.OptionsColumn.AllowSize = false;
            this.colEndDate.OptionsColumn.FixedWidth = true;
            this.colEndDate.OptionsFilter.AllowAutoFilter = false;
            this.colEndDate.OptionsFilter.AllowFilter = false;
            this.colEndDate.Visible = true;
            this.colEndDate.Width = 83;
            // 
            // colDescription
            // 
            this.colDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescription.Caption = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDescription.OptionsColumn.AllowMove = false;
            this.colDescription.OptionsColumn.AllowShowHide = false;
            this.colDescription.OptionsColumn.AllowSize = false;
            this.colDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDescription.OptionsColumn.FixedWidth = true;
            this.colDescription.OptionsFilter.AllowAutoFilter = false;
            this.colDescription.OptionsFilter.AllowFilter = false;
            this.colDescription.Visible = true;
            this.colDescription.Width = 210;
            // 
            // ucExtendedBreak
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcExtendedBreaks);
            this.Controls.Add(this.RCRockEngineering);
            this.Name = "ucExtendedBreak";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(867, 631);
            this.Load += new System.EventHandler(this.ucExtendedBreak_Load);
            this.Controls.SetChildIndex(this.RCRockEngineering, 0);
            this.Controls.SetChildIndex(this.gcExtendedBreaks, 0);
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExtendedBreaks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExtendedBreaks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl RCRockEngineering;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem RockEnginAddImagebtn;
        private DevExpress.XtraBars.BarButtonItem Closebtn;
        private DevExpress.XtraBars.BarEditItem WorkplaceEdit;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraGrid.GridControl gcExtendedBreaks;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvExtendedBreaks;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSection;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colInitiateDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDaysBefore;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStartDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colEndDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDescription;
    }
}
