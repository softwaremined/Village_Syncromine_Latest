namespace Mineware.Systems.Planning.PlanningProtocolCapture
{
    partial class ucPlanProtCapture
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup radioEditCaptureOptions;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPlanProtCapture));
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
            this.CapProtocalribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.imageSmall = new DevExpress.Utils.ImageCollection();
            this.barProdMonth = new DevExpress.XtraBars.BarEditItem();
            this.editProdMonthPlanProt = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.barSetion = new DevExpress.XtraBars.BarEditItem();
            this.editListSections = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnShow = new DevExpress.XtraBars.BarButtonItem();
            this.barTemplate = new DevExpress.XtraBars.BarEditItem();
            this.editTmplates = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barWorkPlace = new DevExpress.XtraBars.BarEditItem();
            this.editWorkPlace = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.viewWorkplace = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barActitivity = new DevExpress.XtraBars.BarEditItem();
            this.editActitivity = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barCaptureOptions = new DevExpress.XtraBars.BarEditItem();
            this.btnLockData = new DevExpress.XtraBars.BarButtonItem();
            this.btnUnlockData = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.imageLarge = new DevExpress.Utils.ImageCollection();
            this.rpSettings = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpCapture = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.editProdmonth = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.barCaptureOption = new DevExpress.XtraBars.BarEditItem();
            radioEditCaptureOptions = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            ((System.ComponentModel.ISupportInitialize)(radioEditCaptureOptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CapProtocalribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editProdMonthPlanProt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editListSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editTmplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editWorkPlace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewWorkplace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editActitivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editProdmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // radioEditCaptureOptions
            // 
            radioEditCaptureOptions.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "From Preplanning"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "From Workplace List")});
            radioEditCaptureOptions.Name = "radioEditCaptureOptions";
            radioEditCaptureOptions.SelectedIndexChanged += new System.EventHandler(this.radioEditCaptureOption_SelectedIndexChanged);
            radioEditCaptureOptions.EditValueChanged += new System.EventHandler(this.radioEditCaptureOptions_EditValueChanged);
            // 
            // CapProtocalribbonControl1
            // 
            this.CapProtocalribbonControl1.ApplicationButtonText = null;
            this.CapProtocalribbonControl1.AutoSizeItems = true;
            this.CapProtocalribbonControl1.ExpandCollapseItem.Id = 0;
            this.CapProtocalribbonControl1.Images = this.imageSmall;
            this.CapProtocalribbonControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CapProtocalribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.CapProtocalribbonControl1.ExpandCollapseItem,
            this.barProdMonth,
            this.barSetion,
            this.btnShow,
            this.barTemplate,
            this.barWorkPlace,
            this.btnSave,
            this.btnCancel,
            this.barActitivity,
            this.barCaptureOptions,
            this.btnLockData,
            this.btnUnlockData,
            this.btnPrint});
            this.CapProtocalribbonControl1.LargeImages = this.imageLarge;
            this.CapProtocalribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.CapProtocalribbonControl1.MaxItemId = 22;
            this.CapProtocalribbonControl1.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Never;
            this.CapProtocalribbonControl1.Name = "CapProtocalribbonControl1";
            this.CapProtocalribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpSettings,
            this.rpCapture});
            this.CapProtocalribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.editProdmonth,
            this.editListSections,
            this.editTmplates,
            this.editActitivity,
            radioEditCaptureOptions,
            this.editWorkPlace,
            this.editProdMonthPlanProt});
            this.CapProtocalribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
            this.CapProtocalribbonControl1.ShowCategoryInCaption = false;
            this.CapProtocalribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.CapProtocalribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.CapProtocalribbonControl1.ShowToolbarCustomizeItem = false;
            this.CapProtocalribbonControl1.Size = new System.Drawing.Size(1020, 116);
            this.CapProtocalribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.CapProtocalribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            this.imageSmall.Images.SetKeyName(0, "ShowBME.png");
            this.imageSmall.Images.SetKeyName(1, "Close.png");
            this.imageSmall.Images.SetKeyName(2, "disk.png");
            this.imageSmall.Images.SetKeyName(3, "lock.png");
            this.imageSmall.Images.SetKeyName(4, "lock_open.png");
            this.imageSmall.Images.SetKeyName(5, "printer.png");
            // 
            // barProdMonth
            // 
            this.barProdMonth.Caption = "Production Month";
            this.barProdMonth.Edit = this.editProdMonthPlanProt;
            this.barProdMonth.EditWidth = 150;
            this.barProdMonth.Id = 1;
            this.barProdMonth.Name = "barProdMonth";
            this.barProdMonth.EditValueChanged += new System.EventHandler(this.barProdMonth_EditValueChanged);
            // 
            // editProdMonthPlanProt
            // 
            this.editProdMonthPlanProt.AutoHeight = false;
            this.editProdMonthPlanProt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.editProdMonthPlanProt.Mask.EditMask = "yyyyMM";
            this.editProdMonthPlanProt.Mask.IgnoreMaskBlank = false;
            this.editProdMonthPlanProt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.editProdMonthPlanProt.Mask.UseMaskAsDisplayFormat = true;
            this.editProdMonthPlanProt.Name = "editProdMonthPlanProt";
            // 
            // barSetion
            // 
            this.barSetion.Caption = "Section";
            this.barSetion.Edit = this.editListSections;
            this.barSetion.EditWidth = 150;
            this.barSetion.Id = 2;
            this.barSetion.Name = "barSetion";
            // 
            // editListSections
            // 
            this.editListSections.AutoHeight = false;
            this.editListSections.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editListSections.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SectionID", "SectionID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.editListSections.Name = "editListSections";
            this.editListSections.NullText = "";
            this.editListSections.PopupFormMinSize = new System.Drawing.Size(450, 250);
            this.editListSections.PopupSizeable = false;
            // 
            // btnShow
            // 
            this.btnShow.Caption = "Show";
            this.btnShow.Id = 4;
            this.btnShow.ImageOptions.ImageIndex = 0;
            this.btnShow.ImageOptions.LargeImageIndex = 0;
            this.btnShow.Name = "btnShow";
            this.btnShow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShow_ItemClick);
            // 
            // barTemplate
            // 
            this.barTemplate.Caption = "Template ";
            this.barTemplate.Edit = this.editTmplates;
            this.barTemplate.EditWidth = 250;
            this.barTemplate.Id = 6;
            this.barTemplate.Name = "barTemplate";
            this.barTemplate.ShowingEditor += new DevExpress.XtraBars.ItemCancelEventHandler(this.barEditItem1_ShowingEditor);
            this.barTemplate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barTemplate_ItemClick);
            // 
            // editTmplates
            // 
            this.editTmplates.AutoHeight = false;
            this.editTmplates.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.editTmplates.BestFitRowCount = 10;
            this.editTmplates.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editTmplates.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TemplateName", "Template")});
            this.editTmplates.Name = "editTmplates";
            this.editTmplates.NullText = "";
            this.editTmplates.EditValueChanged += new System.EventHandler(this.editTmplates_EditValueChanged);
            // 
            // barWorkPlace
            // 
            this.barWorkPlace.Caption = "Workplace";
            this.barWorkPlace.Edit = this.editWorkPlace;
            this.barWorkPlace.EditWidth = 250;
            this.barWorkPlace.Enabled = false;
            this.barWorkPlace.Id = 8;
            this.barWorkPlace.Name = "barWorkPlace";
            this.barWorkPlace.ShowingEditor += new DevExpress.XtraBars.ItemCancelEventHandler(this.barWorkPlace_ShowingEditor);
            // 
            // editWorkPlace
            // 
            this.editWorkPlace.AutoHeight = false;
            this.editWorkPlace.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editWorkPlace.Name = "editWorkPlace";
            this.editWorkPlace.NullText = "";
            this.editWorkPlace.PopupView = this.viewWorkplace;
            // 
            // viewWorkplace
            // 
            this.viewWorkplace.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.viewWorkplace.Name = "viewWorkplace";
            this.viewWorkplace.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.viewWorkplace.OptionsView.ShowAutoFilterRow = true;
            this.viewWorkplace.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewWorkplace.OptionsView.ShowGroupPanel = false;
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.Id = 9;
            this.btnSave.ImageOptions.ImageIndex = 2;
            this.btnSave.ImageOptions.LargeImageIndex = 2;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Caption = "Cancel";
            this.btnCancel.Id = 10;
            this.btnCancel.ImageOptions.ImageIndex = 1;
            this.btnCancel.ImageOptions.LargeImageIndex = 1;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancel_ItemClick);
            // 
            // barActitivity
            // 
            this.barActitivity.Caption = "Activity";
            this.barActitivity.Edit = this.editActitivity;
            this.barActitivity.EditWidth = 150;
            this.barActitivity.Id = 11;
            this.barActitivity.Name = "barActitivity";
            // 
            // editActitivity
            // 
            this.editActitivity.AutoHeight = false;
            this.editActitivity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editActitivity.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Desc", "Activity")});
            this.editActitivity.Name = "editActitivity";
            this.editActitivity.NullText = "";
            this.editActitivity.EditValueChanged += new System.EventHandler(this.editActitivity_EditValueChanged);
            // 
            // barCaptureOptions
            // 
            this.barCaptureOptions.Caption = "Capture Options";
            this.barCaptureOptions.Edit = radioEditCaptureOptions;
            this.barCaptureOptions.EditWidth = 250;
            this.barCaptureOptions.Id = 17;
            this.barCaptureOptions.Name = "barCaptureOptions";
            // 
            // btnLockData
            // 
            this.btnLockData.Caption = "Lock Data";
            this.btnLockData.Id = 19;
            this.btnLockData.ImageOptions.ImageIndex = 3;
            this.btnLockData.ImageOptions.LargeImageIndex = 3;
            this.btnLockData.Name = "btnLockData";
            this.btnLockData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockData_ItemClick);
            // 
            // btnUnlockData
            // 
            this.btnUnlockData.Caption = "Unlock Data";
            this.btnUnlockData.Id = 20;
            this.btnUnlockData.ImageOptions.ImageIndex = 4;
            this.btnUnlockData.ImageOptions.LargeImageIndex = 4;
            this.btnUnlockData.Name = "btnUnlockData";
            this.btnUnlockData.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUnlockData_ItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.Caption = "Print";
            this.btnPrint.Id = 21;
            this.btnPrint.ImageOptions.ImageIndex = 5;
            this.btnPrint.ImageOptions.LargeImageIndex = 5;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // imageLarge
            // 
            this.imageLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.imageLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageLarge.ImageStream")));
            this.imageLarge.Images.SetKeyName(0, "ShowBME.png");
            this.imageLarge.Images.SetKeyName(1, "Close.png");
            this.imageLarge.Images.SetKeyName(2, "disk.png");
            this.imageLarge.Images.SetKeyName(3, "lock.png");
            this.imageLarge.Images.SetKeyName(4, "lock_open.png");
            this.imageLarge.Images.SetKeyName(5, "printer.png");
            // 
            // rpSettings
            // 
            this.rpSettings.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.rpSettings.Name = "rpSettings";
            this.rpSettings.Text = "Settings";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barProdMonth);
            this.ribbonPageGroup1.ItemLinks.Add(this.barSetion);
            this.ribbonPageGroup1.ItemLinks.Add(this.barActitivity);
            this.ribbonPageGroup1.ItemLinks.Add(this.barCaptureOptions);
            this.ribbonPageGroup1.ItemLinks.Add(this.barWorkPlace);
            this.ribbonPageGroup1.ItemLinks.Add(this.barTemplate);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Settings";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnShow);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // rpCapture
            // 
            this.rpCapture.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4,
            this.ribbonPageGroup5,
            this.ribbonPageGroup3});
            this.rpCapture.Name = "rpCapture";
            this.rpCapture.Text = "Capture";
            this.rpCapture.Visible = false;
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.btnLockData);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnUnlockData);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Approve Data";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.btnPrint);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "Print Data";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnCancel);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            // 
            // editProdmonth
            // 
            this.editProdmonth.AutoHeight = false;
            this.editProdmonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.editProdmonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.editProdmonth.MaxLength = 6;
            this.editProdmonth.MaxValue = new decimal(new int[] {
            201812,
            0,
            0,
            0});
            this.editProdmonth.MinValue = new decimal(new int[] {
            201004,
            0,
            0,
            0});
            this.editProdmonth.Name = "editProdmonth";
            this.editProdmonth.EditValueChanged += new System.EventHandler(this.editProdmonth_EditValueChanged);
            // 
            // clientPanel
            // 
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 116);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(1020, 495);
            this.clientPanel.TabIndex = 1;
            // 
            // barCaptureOption
            // 
            this.barCaptureOption.Caption = "Capture Options";
            this.barCaptureOption.Edit = radioEditCaptureOptions;
            this.barCaptureOption.EditWidth = 250;
            this.barCaptureOption.Id = 12;
            this.barCaptureOption.Name = "barCaptureOption";
            // 
            // ucPlanProtCapture
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.CapProtocalribbonControl1);
            this.Name = "ucPlanProtCapture";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(1020, 611);
            this.Load += new System.EventHandler(this.frmPlanProtCpature_Load);
            this.Controls.SetChildIndex(this.CapProtocalribbonControl1, 0);
            this.Controls.SetChildIndex(this.clientPanel, 0);
            ((System.ComponentModel.ISupportInitialize)(radioEditCaptureOptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CapProtocalribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editProdMonthPlanProt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editListSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editTmplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editWorkPlace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewWorkplace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editActitivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editProdmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl CapProtocalribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpSettings;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarEditItem barProdMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit editProdmonth;
        private DevExpress.XtraBars.BarEditItem barSetion;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit editListSections;
        private DevExpress.XtraBars.BarButtonItem btnShow;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.PanelControl clientPanel;
        private DevExpress.Utils.ImageCollection imageSmall;
        private DevExpress.Utils.ImageCollection imageLarge;
        private DevExpress.XtraBars.BarEditItem barTemplate;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit editTmplates;
        private DevExpress.XtraBars.BarEditItem barWorkPlace;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpCapture;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnCancel;
        private DevExpress.XtraBars.BarEditItem barActitivity;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit editActitivity;
        private DevExpress.XtraBars.BarEditItem barCaptureOptions;
        private DevExpress.XtraBars.BarEditItem barCaptureOption;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit editWorkPlace;
        private DevExpress.XtraGrid.Views.Grid.GridView viewWorkplace;
        private DevExpress.XtraBars.BarButtonItem btnLockData;
        private DevExpress.XtraBars.BarButtonItem btnUnlockData;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.BarButtonItem btnPrint;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private Global.CustomControls.MWRepositoryItemProdMonth editProdMonthPlanProt;
    }
}