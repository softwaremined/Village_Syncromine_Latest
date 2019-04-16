namespace Mineware.Systems.Production.Controls.SICCapture
{
    partial class ucSICCapture
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
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barKPI = new DevExpress.XtraBars.BarEditItem();
            this.rpKPI = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.barMonth = new DevExpress.XtraBars.BarEditItem();
            this.rpMonth = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            //this.rpSections = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnShow = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnProblemAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnProblemEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnProblemRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barSectionID = new DevExpress.XtraBars.BarEditItem();
            this.rpSectionID = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rpSelection = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgSelection = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgButton = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pvgSICCapture = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.col_SICKey = new DevExpress.XtraPivotGrid.PivotGridField();
            this.col_KPI = new DevExpress.XtraPivotGrid.PivotGridField();
            this.col_Description = new DevExpress.XtraPivotGrid.PivotGridField();
            this.col_ProgValue = new DevExpress.XtraPivotGrid.PivotGridField();
            this.col_CalendarDate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.col_ShiftNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.col_TheValue = new DevExpress.XtraPivotGrid.PivotGridField();
            this.rpText = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.popupRow = new DevExpress.XtraBars.PopupMenu();
            this.btnFillRow = new DevExpress.XtraBars.BarButtonItem();
            this.btnClearRow = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl21 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl22 = new DevExpress.XtraBars.BarDockControl();
            this.pvgSICCleaned = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.colc_Name = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colc_Workplace = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colc_Order = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colc_Activity = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colc_WP = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colc_Type = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colc_CalendarDate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colc_ShiftNo = new DevExpress.XtraPivotGrid.PivotGridField();
            this.colc_TheValue = new DevExpress.XtraPivotGrid.PivotGridField();
            this.rpTextClean = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.popupProblem = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpKPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMonth)).BeginInit();
           // ((System.ComponentModel.ISupportInitialize)(this.rpSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvgSICCapture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvgSICCleaned)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpTextClean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupProblem)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowKeyTips = false;
            this.ribbonControl1.AllowMdiChildButtons = false;
            this.ribbonControl1.AllowMinimizeRibbon = false;
            this.ribbonControl1.AllowTrimPageText = false;
            this.ribbonControl1.AutoSizeItems = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barKPI,
            this.barMonth,
            this.btnShow,
            this.btnClose,
            this.btnSave,
            this.btnProblemAdd,
            this.btnProblemEdit,
            this.btnProblemRemove,
            this.barSectionID});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 12;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpSelection});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpKPI,
            this.rpMonth,
            //this.rpSections,
            this.rpSectionID});
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(905, 95);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barKPI
            // 
            this.barKPI.Caption = "KPI\'s";
            this.barKPI.Edit = this.rpKPI;
            this.barKPI.EditHeight = 70;
            this.barKPI.EditWidth = 400;
            this.barKPI.Id = 1;
            this.barKPI.Name = "barKPI";
            this.barKPI.EditValueChanged += new System.EventHandler(this.barKPI_EditValueChanged);
            // 
            // rpKPI
            // 
            this.rpKPI.Columns = 3;
            this.rpKPI.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Safety Stoping", "Safety Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Safety Development", "Safety Development"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Safety Services", "Safety Services"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Cleaned", "Cleaning"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Labour Stoping", "Labour Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Labour Development", "Labour Development"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Cost Production", "Cost Production"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Cost Engineering", "Cost Engineering"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Engineering", "Engineering"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Backfill", "Backfill")});
            this.rpKPI.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Column;
            this.rpKPI.Name = "rpKPI";
            // 
            // barMonth
            // 
            this.barMonth.Caption = "Month";
            this.barMonth.Edit = this.rpMonth;
            this.barMonth.EditWidth = 200;
            this.barMonth.Id = 2;
            this.barMonth.Name = "barMonth";
            this.barMonth.EditValueChanged += new System.EventHandler(this.barMonth_EditValueChanged);
            // 
            // rpMonth
            // 
            this.rpMonth.AutoHeight = false;
            this.rpMonth.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.rpMonth.Mask.EditMask = "yyyyMM";
            this.rpMonth.Mask.IgnoreMaskBlank = false;
            this.rpMonth.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.rpMonth.Mask.UseMaskAsDisplayFormat = true;
            this.rpMonth.Name = "rpMonth";
            this.rpMonth.EditValueChanged += new System.EventHandler(this.rpMonth_EditValueChanged);
            // 
            // rpSections
            // 
            //this.rpSections.AutoHeight = false;
            //this.rpSections.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            //new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            //this.rpSections.DropDownRows = 25;
            //this.rpSections.Name = "rpSections";
            //this.rpSections.NullText = "[Select a Section]";
            // 
            // btnShow
            // 
            this.btnShow.Caption = "Show";
            this.btnShow.Id = 4;
            this.btnShow.ImageUri.Uri = "Zoom";
            this.btnShow.Name = "btnShow";
            this.btnShow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShow_ItemClick);
            // 
            // btnClose
            // 
            this.btnClose.Caption = "Close";
            this.btnClose.Id = 5;
            this.btnClose.ImageUri.Uri = "Close";
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.Id = 6;
            this.btnSave.ImageUri.Uri = "Save";
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnProblemAdd
            // 
            this.btnProblemAdd.Caption = "Add Problem";
            this.btnProblemAdd.Id = 7;
            this.btnProblemAdd.Name = "btnProblemAdd";
            this.btnProblemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnProblemAdd_ItemClick);
            // 
            // btnProblemEdit
            // 
            this.btnProblemEdit.Caption = "Edit Problem";
            this.btnProblemEdit.Id = 8;
            this.btnProblemEdit.Name = "btnProblemEdit";
            this.btnProblemEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnProblemEdit_ItemClick);
            // 
            // btnProblemRemove
            // 
            this.btnProblemRemove.Caption = "Remove Problem";
            this.btnProblemRemove.Id = 9;
            this.btnProblemRemove.Name = "btnProblemRemove";
            this.btnProblemRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnProblemRemove_ItemClick);
            // 
            // barSectionID
            // 
            this.barSectionID.Caption = "Sections";
            this.barSectionID.Edit = this.rpSectionID;
            this.barSectionID.EditWidth = 200;
            this.barSectionID.Id = 11;
            this.barSectionID.Name = "barSectionID";
            this.barSectionID.EditValueChanged += new System.EventHandler(this.barSectionID_EditValueChanged);
            // 
            // rpSectionID
            // 
            this.rpSectionID.AutoHeight = false;
            this.rpSectionID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpSectionID.Name = "rpSectionID";
            this.rpSectionID.NullText = "[Select a Section]";
            this.rpSectionID.View = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // rpSelection
            // 
            this.rpSelection.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgSelection,
            this.rpgButton});
            this.rpSelection.Name = "rpSelection";
            // 
            // rpgSelection
            // 
            this.rpgSelection.ItemLinks.Add(this.barKPI);
            this.rpgSelection.ItemLinks.Add(this.barMonth);
            this.rpgSelection.ItemLinks.Add(this.barSectionID);
            this.rpgSelection.Name = "rpgSelection";
            this.rpgSelection.Text = "Selection";
            // 
            // rpgButton
            // 
            this.rpgButton.ItemLinks.Add(this.btnShow);
            this.rpgButton.ItemLinks.Add(this.btnClose);
            this.rpgButton.ItemLinks.Add(this.btnSave);
            this.rpgButton.Name = "rpgButton";
            // 
            // pvgSICCapture
            // 
            this.pvgSICCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvgSICCapture.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.col_SICKey,
            this.col_KPI,
            this.col_Description,
            this.col_ProgValue,
            this.col_CalendarDate,
            this.col_ShiftNo,
            this.col_TheValue});
            this.pvgSICCapture.Location = new System.Drawing.Point(0, 0);
            this.pvgSICCapture.Name = "pvgSICCapture";
            this.pvgSICCapture.OptionsCustomization.AllowDrag = false;
            this.pvgSICCapture.OptionsCustomization.AllowExpand = false;
            this.pvgSICCapture.OptionsCustomization.AllowExpandOnDoubleClick = false;
            this.pvgSICCapture.OptionsCustomization.AllowFilter = false;
            this.pvgSICCapture.OptionsCustomization.AllowFilterBySummary = false;
            this.pvgSICCapture.OptionsCustomization.AllowPrefilter = false;
            this.pvgSICCapture.OptionsCustomization.AllowResizing = false;
            this.pvgSICCapture.OptionsCustomization.AllowSort = false;
            this.pvgSICCapture.OptionsView.ShowColumnGrandTotalHeader = false;
            this.pvgSICCapture.OptionsView.ShowColumnGrandTotals = false;
            this.pvgSICCapture.OptionsView.ShowColumnHeaders = false;
            this.pvgSICCapture.OptionsView.ShowColumnTotals = false;
            this.pvgSICCapture.OptionsView.ShowDataHeaders = false;
            this.pvgSICCapture.OptionsView.ShowFilterHeaders = false;
            this.pvgSICCapture.OptionsView.ShowFilterSeparatorBar = false;
            this.pvgSICCapture.OptionsView.ShowRowGrandTotalHeader = false;
            this.pvgSICCapture.OptionsView.ShowRowGrandTotals = false;
            this.pvgSICCapture.OptionsView.ShowRowTotals = false;
            this.pvgSICCapture.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpText});
            this.pvgSICCapture.Size = new System.Drawing.Size(905, 483);
            this.pvgSICCapture.TabIndex = 9;
            this.pvgSICCapture.CustomDrawCell += new DevExpress.XtraPivotGrid.PivotCustomDrawCellEventHandler(this.pvgSICCapture_CustomDrawCell);
            this.pvgSICCapture.EditValueChanged += new DevExpress.XtraPivotGrid.EditValueChangedEventHandler(this.pvgSICCapture_EditValueChanged);
            this.pvgSICCapture.ShowingEditor += new System.EventHandler<DevExpress.XtraPivotGrid.CancelPivotCellEditEventArgs>(this.pvgSICCapture_ShowingEditor);
            this.pvgSICCapture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pvgSICCapture_MouseMove);
            this.pvgSICCapture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pvgSICCapture_MouseUp);
            // 
            // col_SICKey
            // 
            this.col_SICKey.Appearance.Cell.Options.UseTextOptions = true;
            this.col_SICKey.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_SICKey.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.col_SICKey.Appearance.Header.Options.UseFont = true;
            this.col_SICKey.Appearance.Header.Options.UseTextOptions = true;
            this.col_SICKey.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_SICKey.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.col_SICKey.Appearance.Value.Options.UseFont = true;
            this.col_SICKey.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.col_SICKey.AreaIndex = 0;
            this.col_SICKey.Caption = "SIC Key";
            this.col_SICKey.FieldName = "SICKey";
            this.col_SICKey.MinWidth = 0;
            this.col_SICKey.Name = "col_SICKey";
            this.col_SICKey.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.col_SICKey.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.col_SICKey.Options.AllowEdit = false;
            this.col_SICKey.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.col_SICKey.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.col_SICKey.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_SICKey.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.col_SICKey.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.col_SICKey.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_SICKey.Options.ReadOnly = true;
            this.col_SICKey.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.col_SICKey.Width = 0;
            // 
            // col_KPI
            // 
            this.col_KPI.Appearance.Cell.Options.UseTextOptions = true;
            this.col_KPI.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_KPI.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.col_KPI.Appearance.Header.Options.UseFont = true;
            this.col_KPI.Appearance.Header.Options.UseTextOptions = true;
            this.col_KPI.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_KPI.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.col_KPI.Appearance.Value.Options.UseFont = true;
            this.col_KPI.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.col_KPI.AreaIndex = 0;
            this.col_KPI.Caption = "KPI";
            this.col_KPI.FieldName = "KPI";
            this.col_KPI.Name = "col_KPI";
            this.col_KPI.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.col_KPI.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.col_KPI.Options.AllowEdit = false;
            this.col_KPI.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.col_KPI.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.col_KPI.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_KPI.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.col_KPI.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.col_KPI.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_KPI.Options.ReadOnly = true;
            this.col_KPI.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.col_KPI.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.col_KPI.Visible = false;
            // 
            // col_Description
            // 
            this.col_Description.Appearance.Cell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.col_Description.Appearance.Cell.Options.UseBackColor = true;
            this.col_Description.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.col_Description.Appearance.Header.Options.UseFont = true;
            this.col_Description.Appearance.Header.Options.UseTextOptions = true;
            this.col_Description.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Description.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.col_Description.Appearance.Value.Options.UseFont = true;
            this.col_Description.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.col_Description.AreaIndex = 1;
            this.col_Description.Caption = "Description";
            this.col_Description.FieldName = "Description";
            this.col_Description.Name = "col_Description";
            this.col_Description.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.col_Description.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.col_Description.Options.AllowEdit = false;
            this.col_Description.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.col_Description.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.col_Description.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_Description.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.col_Description.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.col_Description.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_Description.Options.ReadOnly = true;
            this.col_Description.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.col_Description.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.col_Description.Width = 140;
            // 
            // col_ProgValue
            // 
            this.col_ProgValue.Appearance.Cell.Options.UseTextOptions = true;
            this.col_ProgValue.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_ProgValue.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.col_ProgValue.Appearance.Header.Options.UseFont = true;
            this.col_ProgValue.Appearance.Header.Options.UseTextOptions = true;
            this.col_ProgValue.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_ProgValue.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.col_ProgValue.Appearance.Value.Options.UseFont = true;
            this.col_ProgValue.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.col_ProgValue.AreaIndex = 2;
            this.col_ProgValue.Caption = "Total";
            this.col_ProgValue.FieldName = "ProgValue";
            this.col_ProgValue.Name = "col_ProgValue";
            this.col_ProgValue.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.col_ProgValue.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.col_ProgValue.Width = 50;
            // 
            // col_CalendarDate
            // 
            this.col_CalendarDate.Appearance.Cell.Options.UseTextOptions = true;
            this.col_CalendarDate.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_CalendarDate.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.col_CalendarDate.Appearance.Value.Options.UseFont = true;
            this.col_CalendarDate.Appearance.Value.Options.UseTextOptions = true;
            this.col_CalendarDate.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_CalendarDate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.col_CalendarDate.AreaIndex = 0;
            this.col_CalendarDate.FieldName = "CalendarDate";
            this.col_CalendarDate.Name = "col_CalendarDate";
            this.col_CalendarDate.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.col_CalendarDate.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.col_CalendarDate.Options.AllowEdit = false;
            this.col_CalendarDate.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.col_CalendarDate.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.col_CalendarDate.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_CalendarDate.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.col_CalendarDate.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.col_CalendarDate.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_CalendarDate.Options.ReadOnly = true;
            this.col_CalendarDate.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.col_CalendarDate.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.col_CalendarDate.ValueFormat.FormatString = "dd-MM";
            this.col_CalendarDate.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.col_CalendarDate.Width = 40;
            // 
            // col_ShiftNo
            // 
            this.col_ShiftNo.Appearance.Cell.Options.UseTextOptions = true;
            this.col_ShiftNo.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_ShiftNo.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.col_ShiftNo.Appearance.Value.Options.UseFont = true;
            this.col_ShiftNo.Appearance.Value.Options.UseTextOptions = true;
            this.col_ShiftNo.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_ShiftNo.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.col_ShiftNo.AreaIndex = 1;
            this.col_ShiftNo.FieldName = "ShiftNo";
            this.col_ShiftNo.Name = "col_ShiftNo";
            this.col_ShiftNo.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.col_ShiftNo.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.col_ShiftNo.Options.AllowEdit = false;
            this.col_ShiftNo.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.col_ShiftNo.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.col_ShiftNo.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_ShiftNo.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.col_ShiftNo.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.col_ShiftNo.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.col_ShiftNo.Options.ReadOnly = true;
            this.col_ShiftNo.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.col_ShiftNo.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.col_ShiftNo.Width = 40;
            // 
            // col_TheValue
            // 
            this.col_TheValue.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.col_TheValue.Appearance.Cell.Options.UseFont = true;
            this.col_TheValue.Appearance.Cell.Options.UseTextOptions = true;
            this.col_TheValue.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_TheValue.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.col_TheValue.AreaIndex = 0;
            this.col_TheValue.FieldEdit = this.rpText;
            this.col_TheValue.FieldName = "TheValue";
            this.col_TheValue.Name = "col_TheValue";
            this.col_TheValue.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.col_TheValue.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            this.col_TheValue.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.col_TheValue.Width = 40;
            // 
            // rpText
            // 
            this.rpText.AutoHeight = false;
            this.rpText.MaxLength = 5;
            this.rpText.Name = "rpText";
            this.rpText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rpText_KeyPress);
            // 
            // popupRow
            // 
            this.popupRow.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnFillRow),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnClearRow)});
            this.popupRow.Manager = this.barManager1;
            this.popupRow.Name = "popupRow";
            // 
            // btnFillRow
            // 
            this.btnFillRow.Caption = "Fill Row";
            this.btnFillRow.Id = 6;
            this.btnFillRow.Name = "btnFillRow";
            this.btnFillRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFillRow_ItemClick);
            // 
            // btnClearRow
            // 
            this.btnClearRow.Caption = "Clear Row";
            this.btnClearRow.Id = 7;
            this.btnClearRow.Name = "btnClearRow";
            this.btnClearRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClearRow_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl21);
            this.barManager1.DockControls.Add(this.barDockControl22);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnFillRow,
            this.btnClearRow});
            this.barManager1.MaxItemId = 8;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Size = new System.Drawing.Size(905, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 483);
            this.barDockControl2.Size = new System.Drawing.Size(905, 0);
            // 
            // barDockControl21
            // 
            this.barDockControl21.CausesValidation = false;
            this.barDockControl21.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl21.Location = new System.Drawing.Point(0, 0);
            this.barDockControl21.Size = new System.Drawing.Size(0, 483);
            // 
            // barDockControl22
            // 
            this.barDockControl22.CausesValidation = false;
            this.barDockControl22.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl22.Location = new System.Drawing.Point(905, 0);
            this.barDockControl22.Size = new System.Drawing.Size(0, 483);
            // 
            // pvgSICCleaned
            // 
            this.pvgSICCleaned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvgSICCleaned.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.colc_Name,
            this.colc_Workplace,
            this.colc_Order,
            this.colc_Activity,
            this.colc_WP,
            this.colc_Type,
            this.colc_CalendarDate,
            this.colc_ShiftNo,
            this.colc_TheValue});
            this.pvgSICCleaned.Location = new System.Drawing.Point(0, 95);
            this.pvgSICCleaned.Name = "pvgSICCleaned";
            this.pvgSICCleaned.OptionsCustomization.AllowDrag = false;
            this.pvgSICCleaned.OptionsCustomization.AllowExpand = false;
            this.pvgSICCleaned.OptionsCustomization.AllowExpandOnDoubleClick = false;
            this.pvgSICCleaned.OptionsCustomization.AllowFilter = false;
            this.pvgSICCleaned.OptionsCustomization.AllowFilterBySummary = false;
            this.pvgSICCleaned.OptionsCustomization.AllowPrefilter = false;
            this.pvgSICCleaned.OptionsCustomization.AllowResizing = false;
            this.pvgSICCleaned.OptionsCustomization.AllowSort = false;
            this.pvgSICCleaned.OptionsView.ShowColumnGrandTotalHeader = false;
            this.pvgSICCleaned.OptionsView.ShowColumnGrandTotals = false;
            this.pvgSICCleaned.OptionsView.ShowColumnHeaders = false;
            this.pvgSICCleaned.OptionsView.ShowColumnTotals = false;
            this.pvgSICCleaned.OptionsView.ShowDataHeaders = false;
            this.pvgSICCleaned.OptionsView.ShowFilterHeaders = false;
            this.pvgSICCleaned.OptionsView.ShowFilterSeparatorBar = false;
            this.pvgSICCleaned.OptionsView.ShowRowGrandTotalHeader = false;
            this.pvgSICCleaned.OptionsView.ShowRowGrandTotals = false;
            this.pvgSICCleaned.OptionsView.ShowRowTotals = false;
            this.pvgSICCleaned.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpTextClean});
            this.pvgSICCleaned.Size = new System.Drawing.Size(905, 388);
            this.pvgSICCleaned.TabIndex = 15;
            this.pvgSICCleaned.CustomDrawCell += new DevExpress.XtraPivotGrid.PivotCustomDrawCellEventHandler(this.pvgSICCleaned_CustomDrawCell);
            this.pvgSICCleaned.EditValueChanged += new DevExpress.XtraPivotGrid.EditValueChangedEventHandler(this.pvgSICCleaned_EditValueChanged);
            this.pvgSICCleaned.ShowingEditor += new System.EventHandler<DevExpress.XtraPivotGrid.CancelPivotCellEditEventArgs>(this.pvgSICCleaned_ShowingEditor);
            this.pvgSICCleaned.DoubleClick += new System.EventHandler(this.pvgSICCleaned_DoubleClick);
            this.pvgSICCleaned.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pvgSICCleaned_MouseMove);
            this.pvgSICCleaned.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pvgSICCleaned_MouseUp);
            // 
            // colc_Name
            // 
            this.colc_Name.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.colc_Name.Appearance.Header.Options.UseFont = true;
            this.colc_Name.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.colc_Name.Appearance.Value.Options.UseFont = true;
            this.colc_Name.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colc_Name.AreaIndex = 0;
            this.colc_Name.Caption = "Name";
            this.colc_Name.FieldName = "Name";
            this.colc_Name.MinWidth = 0;
            this.colc_Name.Name = "colc_Name";
            this.colc_Name.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Name.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Name.Options.AllowEdit = false;
            this.colc_Name.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Name.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Name.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Name.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Name.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Name.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Name.Options.ReadOnly = true;
            this.colc_Name.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.colc_Name.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            this.colc_Name.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.colc_Name.Width = 150;
            // 
            // colc_Workplace
            // 
            this.colc_Workplace.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.colc_Workplace.Appearance.Header.Options.UseFont = true;
            this.colc_Workplace.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.colc_Workplace.Appearance.Value.Options.UseFont = true;
            this.colc_Workplace.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colc_Workplace.AreaIndex = 1;
            this.colc_Workplace.Caption = "Workplace";
            this.colc_Workplace.FieldName = "Workplace";
            this.colc_Workplace.Name = "colc_Workplace";
            this.colc_Workplace.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Workplace.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Workplace.Options.AllowEdit = false;
            this.colc_Workplace.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Workplace.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Workplace.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Workplace.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Workplace.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Workplace.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Workplace.Options.ReadOnly = true;
            this.colc_Workplace.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.colc_Workplace.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            this.colc_Workplace.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.colc_Workplace.Width = 150;
            // 
            // colc_Order
            // 
            this.colc_Order.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.colc_Order.Appearance.Header.Options.UseFont = true;
            this.colc_Order.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.colc_Order.Appearance.Value.Options.UseFont = true;
            this.colc_Order.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colc_Order.AreaIndex = 2;
            this.colc_Order.FieldName = "Order";
            this.colc_Order.MinWidth = 0;
            this.colc_Order.Name = "colc_Order";
            this.colc_Order.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Order.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Order.Options.AllowEdit = false;
            this.colc_Order.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Order.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Order.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Order.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Order.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Order.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Order.Options.ReadOnly = true;
            this.colc_Order.Width = 0;
            // 
            // colc_Activity
            // 
            this.colc_Activity.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.colc_Activity.Appearance.Header.Options.UseFont = true;
            this.colc_Activity.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.colc_Activity.Appearance.Value.Options.UseFont = true;
            this.colc_Activity.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colc_Activity.AreaIndex = 3;
            this.colc_Activity.FieldName = "Activity";
            this.colc_Activity.MinWidth = 0;
            this.colc_Activity.Name = "colc_Activity";
            this.colc_Activity.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Activity.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Activity.Options.AllowEdit = false;
            this.colc_Activity.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Activity.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Activity.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Activity.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Activity.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Activity.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Activity.Options.ReadOnly = true;
            this.colc_Activity.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colc_Activity.Width = 0;
            // 
            // colc_WP
            // 
            this.colc_WP.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.colc_WP.Appearance.Header.Options.UseFont = true;
            this.colc_WP.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.colc_WP.Appearance.Value.Options.UseFont = true;
            this.colc_WP.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colc_WP.AreaIndex = 4;
            this.colc_WP.FieldName = "Workplace";
            this.colc_WP.MinWidth = 0;
            this.colc_WP.Name = "colc_WP";
            this.colc_WP.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_WP.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_WP.Options.AllowEdit = false;
            this.colc_WP.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_WP.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_WP.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_WP.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_WP.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_WP.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_WP.Options.ReadOnly = true;
            this.colc_WP.Width = 0;
            // 
            // colc_Type
            // 
            this.colc_Type.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
            this.colc_Type.Appearance.Header.Options.UseFont = true;
            this.colc_Type.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.colc_Type.Appearance.Value.Options.UseFont = true;
            this.colc_Type.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.colc_Type.AreaIndex = 5;
            this.colc_Type.FieldName = "Type";
            this.colc_Type.Name = "colc_Type";
            this.colc_Type.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Type.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Type.Options.AllowEdit = false;
            this.colc_Type.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Type.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Type.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Type.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Type.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Type.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_Type.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending;
            this.colc_Type.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            this.colc_Type.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.colc_Type.Width = 60;
            // 
            // colc_CalendarDate
            // 
            this.colc_CalendarDate.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.colc_CalendarDate.Appearance.Value.Options.UseFont = true;
            this.colc_CalendarDate.Appearance.Value.Options.UseTextOptions = true;
            this.colc_CalendarDate.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colc_CalendarDate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.colc_CalendarDate.AreaIndex = 0;
            this.colc_CalendarDate.FieldName = "CalendarDate";
            this.colc_CalendarDate.Name = "colc_CalendarDate";
            this.colc_CalendarDate.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_CalendarDate.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_CalendarDate.Options.AllowEdit = false;
            this.colc_CalendarDate.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_CalendarDate.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_CalendarDate.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_CalendarDate.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_CalendarDate.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_CalendarDate.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_CalendarDate.Options.ReadOnly = true;
            this.colc_CalendarDate.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.colc_CalendarDate.ValueFormat.FormatString = "dd-MM";
            this.colc_CalendarDate.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colc_CalendarDate.Width = 40;
            // 
            // colc_ShiftNo
            // 
            this.colc_ShiftNo.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.colc_ShiftNo.Appearance.Value.Options.UseFont = true;
            this.colc_ShiftNo.Appearance.Value.Options.UseTextOptions = true;
            this.colc_ShiftNo.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colc_ShiftNo.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.colc_ShiftNo.AreaIndex = 1;
            this.colc_ShiftNo.FieldName = "ShiftNo";
            this.colc_ShiftNo.Name = "colc_ShiftNo";
            this.colc_ShiftNo.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_ShiftNo.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_ShiftNo.Options.AllowEdit = false;
            this.colc_ShiftNo.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_ShiftNo.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_ShiftNo.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_ShiftNo.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_ShiftNo.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_ShiftNo.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_ShiftNo.Options.ReadOnly = true;
            this.colc_ShiftNo.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.colc_ShiftNo.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            this.colc_ShiftNo.Width = 40;
            // 
            // colc_TheValue
            // 
            this.colc_TheValue.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 7.5F);
            this.colc_TheValue.Appearance.Cell.Options.UseFont = true;
            this.colc_TheValue.Appearance.Cell.Options.UseTextOptions = true;
            this.colc_TheValue.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colc_TheValue.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.colc_TheValue.AreaIndex = 0;
            this.colc_TheValue.FieldEdit = this.rpTextClean;
            this.colc_TheValue.FieldName = "TheValue";
            this.colc_TheValue.Name = "colc_TheValue";
            this.colc_TheValue.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.colc_TheValue.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.colc_TheValue.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.colc_TheValue.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False;
            this.colc_TheValue.Options.AllowFilterBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_TheValue.Options.AllowHide = DevExpress.Utils.DefaultBoolean.False;
            this.colc_TheValue.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colc_TheValue.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.colc_TheValue.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.colc_TheValue.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
            this.colc_TheValue.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            this.colc_TheValue.Width = 40;
            // 
            // rpTextClean
            // 
            this.rpTextClean.AutoHeight = false;
            this.rpTextClean.Name = "rpTextClean";
            this.rpTextClean.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rpTextClean_KeyPress);
            // 
            // popupProblem
            // 
            this.popupProblem.ItemLinks.Add(this.btnProblemAdd);
            this.popupProblem.ItemLinks.Add(this.btnProblemEdit);
            this.popupProblem.ItemLinks.Add(this.btnProblemRemove);
            this.popupProblem.Name = "popupProblem";
            this.popupProblem.Ribbon = this.ribbonControl1;
            // 
            // ucSICCapture
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pvgSICCleaned);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.pvgSICCapture);
            this.Controls.Add(this.barDockControl21);
            this.Controls.Add(this.barDockControl22);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "ucSICCapture";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(905, 483);
            this.Load += new System.EventHandler(this.ucSICCapture_Load);
            this.Controls.SetChildIndex(this.barDockControl1, 0);
            this.Controls.SetChildIndex(this.barDockControl2, 0);
            this.Controls.SetChildIndex(this.barDockControl22, 0);
            this.Controls.SetChildIndex(this.barDockControl21, 0);
            this.Controls.SetChildIndex(this.pvgSICCapture, 0);
            this.Controls.SetChildIndex(this.ribbonControl1, 0);
            this.Controls.SetChildIndex(this.pvgSICCleaned, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpKPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMonth)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.rpSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvgSICCapture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvgSICCleaned)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpTextClean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupProblem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpSelection;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgSelection;
        private DevExpress.XtraBars.BarEditItem barKPI;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rpKPI;
        private DevExpress.XtraBars.BarEditItem barMonth;
        private Global.CustomControls.MWRepositoryItemProdMonth rpMonth;
       // private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpSections;
        private DevExpress.XtraBars.BarButtonItem btnShow;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgButton;
        private DevExpress.XtraPivotGrid.PivotGridControl pvgSICCapture;
        private DevExpress.XtraPivotGrid.PivotGridField col_SICKey;
        private DevExpress.XtraPivotGrid.PivotGridField col_KPI;
        private DevExpress.XtraPivotGrid.PivotGridField col_Description;
        private DevExpress.XtraPivotGrid.PivotGridField col_ProgValue;
        private DevExpress.XtraPivotGrid.PivotGridField col_CalendarDate;
        private DevExpress.XtraPivotGrid.PivotGridField col_ShiftNo;
        private DevExpress.XtraPivotGrid.PivotGridField col_TheValue;
        //private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpNumeric2;
        //private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpFillRow;
        //private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rpClearRow;
        //private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rpDate;
        private DevExpress.XtraBars.PopupMenu popupRow;
        private DevExpress.XtraBars.BarButtonItem btnFillRow;
        private DevExpress.XtraBars.BarButtonItem btnClearRow;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl21;
        private DevExpress.XtraBars.BarDockControl barDockControl22;
        //private DevExpress.XtraBars.BarButtonItem btnProblemEdit;
        //private DevExpress.XtraBars.BarButtonItem btnProblemRemove;
        //private DevExpress.XtraBars.BarButtonItem btnRemoveABSDev;
        //private DevExpress.XtraBars.BarButtonItem btnProbemEditDev;
        //private DevExpress.XtraBars.BarButtonItem btnProblemRemoveDev;
        //private DevExpress.XtraBars.BarButtonItem btnRemoveABS;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpText;
        private DevExpress.XtraPivotGrid.PivotGridControl pvgSICCleaned;
        private DevExpress.XtraPivotGrid.PivotGridField colc_Name;
        private DevExpress.XtraPivotGrid.PivotGridField colc_Workplace;
        private DevExpress.XtraPivotGrid.PivotGridField colc_Type;
        private DevExpress.XtraPivotGrid.PivotGridField colc_CalendarDate;
        private DevExpress.XtraPivotGrid.PivotGridField colc_ShiftNo;
        private DevExpress.XtraPivotGrid.PivotGridField colc_TheValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpTextClean;
        private DevExpress.XtraPivotGrid.PivotGridField colc_Order;
        private DevExpress.XtraPivotGrid.PivotGridField colc_Activity;
        private DevExpress.XtraPivotGrid.PivotGridField colc_WP;
        private DevExpress.XtraBars.BarButtonItem btnProblemAdd;
        private DevExpress.XtraBars.BarButtonItem btnProblemEdit;
        private DevExpress.XtraBars.PopupMenu popupProblem;
        private DevExpress.XtraBars.BarButtonItem btnProblemRemove;
        private DevExpress.XtraBars.BarEditItem barSectionID;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit rpSectionID;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
    }
}
