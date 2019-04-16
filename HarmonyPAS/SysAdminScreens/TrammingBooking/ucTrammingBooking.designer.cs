namespace Mineware.Systems.Production.SysAdminScreens.TrammingBooking
{
    partial class ucTrammingBooking
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
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcBookings = new DevExpress.XtraGrid.GridControl();
            this.bgvBookings = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bgcXC = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcBH = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcTons = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcBookedDailyTons = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcHoppers = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcProgressive = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcUnits = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gbDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bgcNight = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.TextEditUpdateTotal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.bgcMorning = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcAfternoon = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcTotal = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bgcBlank = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bgcWorkplace = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.lueMillMonth = new DevExpress.XtraBars.BarEditItem();
            this.btnBookingShow = new DevExpress.XtraBars.BarButtonItem();
            this.btnBookingSave = new DevExpress.XtraBars.BarButtonItem();
            this.lueMill = new DevExpress.XtraBars.BarEditItem();
            this.btnBookingBack = new DevExpress.XtraBars.BarButtonItem();
            this.txtTonsTreated = new DevExpress.XtraBars.BarEditItem();
            this.txtTonsToPlant = new DevExpress.XtraBars.BarEditItem();
            this.lueBookingProdMonth = new DevExpress.XtraBars.BarEditItem();
            this.mwRepositoryItemProdMonth1 = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.dteDate = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.cbBookingSections = new DevExpress.XtraBars.BarEditItem();
            this.rpBookingSections = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgSelection = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgShow = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgSave = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpSection = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBookings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgvBookings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditUpdateTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwRepositoryItemProdMonth1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpBookingSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelControl1);
            this.panelControl3.Controls.Add(this.ribbonControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(963, 507);
            this.panelControl3.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcBookings);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 97);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(959, 408);
            this.panelControl1.TabIndex = 11;
            // 
            // gcBookings
            // 
            this.gcBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBookings.Location = new System.Drawing.Point(2, 2);
            this.gcBookings.MainView = this.bgvBookings;
            this.gcBookings.Name = "gcBookings";
            this.gcBookings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.TextEditUpdateTotal});
            this.gcBookings.Size = new System.Drawing.Size(955, 404);
            this.gcBookings.TabIndex = 9;
            this.gcBookings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bgvBookings});
            // 
            // bgvBookings
            // 
            this.bgvBookings.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gbDate,
            this.gridBand2});
            this.bgvBookings.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bgcXC,
            this.bgcBH,
            this.bgcTons,
            this.bgcHoppers,
            this.bgcBookedDailyTons,
            this.bgcProgressive,
            this.bgcUnits,
            this.bgcNight,
            this.bgcMorning,
            this.bgcAfternoon,
            this.bgcTotal,
            this.bgcBlank,
            this.bgcWorkplace});
            this.bgvBookings.GridControl = this.gcBookings;
            this.bgvBookings.Name = "bgvBookings";
            this.bgvBookings.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.bgvBookings.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.bgvBookings.OptionsView.ShowGroupPanel = false;
            this.bgvBookings.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.bgvBookings_CellValueChanged);
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.bgcXC);
            this.gridBand1.Columns.Add(this.bgcBH);
            this.gridBand1.Columns.Add(this.bgcTons);
            this.gridBand1.Columns.Add(this.bgcBookedDailyTons);
            this.gridBand1.Columns.Add(this.bgcHoppers);
            this.gridBand1.Columns.Add(this.bgcProgressive);
            this.gridBand1.Columns.Add(this.bgcUnits);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.OptionsBand.FixedWidth = true;
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 718;
            // 
            // bgcXC
            // 
            this.bgcXC.AppearanceCell.Options.UseTextOptions = true;
            this.bgcXC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcXC.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.bgcXC.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcXC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcXC.Caption = "Boxhole";
            this.bgcXC.FieldName = "BH";
            this.bgcXC.Name = "bgcXC";
            this.bgcXC.OptionsColumn.AllowEdit = false;
            this.bgcXC.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.bgcXC.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcXC.OptionsColumn.FixedWidth = true;
            this.bgcXC.Visible = true;
            this.bgcXC.Width = 113;
            // 
            // bgcBH
            // 
            this.bgcBH.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcBH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcBH.Caption = "BH";
            this.bgcBH.FieldName = "BH";
            this.bgcBH.Name = "bgcBH";
            this.bgcBH.OptionsColumn.AllowEdit = false;
            this.bgcBH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.bgcBH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcBH.OptionsColumn.FixedWidth = true;
            this.bgcBH.Width = 118;
            // 
            // bgcTons
            // 
            this.bgcTons.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcTons.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcTons.Caption = "Planned Daily Tons";
            this.bgcTons.FieldName = "Tons";
            this.bgcTons.Name = "bgcTons";
            this.bgcTons.OptionsColumn.AllowEdit = false;
            this.bgcTons.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.bgcTons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcTons.OptionsColumn.FixedWidth = true;
            this.bgcTons.Visible = true;
            this.bgcTons.Width = 107;
            // 
            // bgcBookedDailyTons
            // 
            this.bgcBookedDailyTons.Caption = "Booked Daily Tons";
            this.bgcBookedDailyTons.FieldName = "BookedDailyTons";
            this.bgcBookedDailyTons.Name = "bgcBookedDailyTons";
            this.bgcBookedDailyTons.OptionsColumn.AllowEdit = false;
            this.bgcBookedDailyTons.OptionsColumn.ReadOnly = true;
            this.bgcBookedDailyTons.Visible = true;
            this.bgcBookedDailyTons.Width = 107;
            // 
            // bgcHoppers
            // 
            this.bgcHoppers.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcHoppers.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcHoppers.Caption = "Planned Hoppers Per Shift";
            this.bgcHoppers.FieldName = "HoppersPerShift";
            this.bgcHoppers.Name = "bgcHoppers";
            this.bgcHoppers.OptionsColumn.AllowEdit = false;
            this.bgcHoppers.OptionsColumn.AllowFocus = false;
            this.bgcHoppers.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcHoppers.OptionsColumn.FixedWidth = true;
            this.bgcHoppers.Visible = true;
            this.bgcHoppers.Width = 157;
            // 
            // bgcProgressive
            // 
            this.bgcProgressive.Caption = "Progressive";
            this.bgcProgressive.FieldName = "Progressive";
            this.bgcProgressive.Name = "bgcProgressive";
            this.bgcProgressive.OptionsColumn.AllowEdit = false;
            this.bgcProgressive.OptionsColumn.ReadOnly = true;
            this.bgcProgressive.Visible = true;
            // 
            // bgcUnits
            // 
            this.bgcUnits.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcUnits.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcUnits.Caption = "Units";
            this.bgcUnits.FieldName = "Units";
            this.bgcUnits.Name = "bgcUnits";
            this.bgcUnits.OptionsColumn.AllowEdit = false;
            this.bgcUnits.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.bgcUnits.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcUnits.OptionsColumn.ReadOnly = true;
            this.bgcUnits.Visible = true;
            this.bgcUnits.Width = 159;
            // 
            // gbDate
            // 
            this.gbDate.AppearanceHeader.Options.UseTextOptions = true;
            this.gbDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gbDate.Caption = "2014-01-25";
            this.gbDate.Columns.Add(this.bgcNight);
            this.gbDate.Columns.Add(this.bgcMorning);
            this.gbDate.Columns.Add(this.bgcAfternoon);
            this.gbDate.Columns.Add(this.bgcTotal);
            this.gbDate.Name = "gbDate";
            this.gbDate.OptionsBand.FixedWidth = true;
            this.gbDate.VisibleIndex = 1;
            this.gbDate.Width = 240;
            // 
            // bgcNight
            // 
            this.bgcNight.AppearanceCell.Options.UseTextOptions = true;
            this.bgcNight.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcNight.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.bgcNight.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcNight.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcNight.Caption = "N";
            this.bgcNight.ColumnEdit = this.TextEditUpdateTotal;
            this.bgcNight.DisplayFormat.FormatString = "{0:0}";
            this.bgcNight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bgcNight.FieldName = "Night";
            this.bgcNight.Name = "bgcNight";
            this.bgcNight.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcNight.OptionsColumn.FixedWidth = true;
            this.bgcNight.Visible = true;
            this.bgcNight.Width = 60;
            // 
            // TextEditUpdateTotal
            // 
            this.TextEditUpdateTotal.AutoHeight = false;
            this.TextEditUpdateTotal.Name = "TextEditUpdateTotal";
            this.TextEditUpdateTotal.EditValueChanged += new System.EventHandler(this.TextEditUpdateTotal_EditValueChanged);
            this.TextEditUpdateTotal.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.TextEditUpdateTotal_EditValueChanging);
            this.TextEditUpdateTotal.Leave += new System.EventHandler(this.TextEditUpdateTotal_Leave);
            // 
            // bgcMorning
            // 
            this.bgcMorning.AppearanceCell.Options.UseTextOptions = true;
            this.bgcMorning.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcMorning.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.bgcMorning.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcMorning.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcMorning.Caption = "M";
            this.bgcMorning.ColumnEdit = this.TextEditUpdateTotal;
            this.bgcMorning.DisplayFormat.FormatString = "{0:0}";
            this.bgcMorning.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bgcMorning.FieldName = "Morning";
            this.bgcMorning.Name = "bgcMorning";
            this.bgcMorning.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.bgcMorning.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcMorning.OptionsColumn.FixedWidth = true;
            this.bgcMorning.Visible = true;
            this.bgcMorning.Width = 60;
            // 
            // bgcAfternoon
            // 
            this.bgcAfternoon.AppearanceCell.Options.UseTextOptions = true;
            this.bgcAfternoon.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcAfternoon.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.bgcAfternoon.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcAfternoon.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcAfternoon.Caption = "A";
            this.bgcAfternoon.ColumnEdit = this.TextEditUpdateTotal;
            this.bgcAfternoon.DisplayFormat.FormatString = "{0:0}";
            this.bgcAfternoon.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bgcAfternoon.FieldName = "Afternoon";
            this.bgcAfternoon.Name = "bgcAfternoon";
            this.bgcAfternoon.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.bgcAfternoon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcAfternoon.OptionsColumn.FixedWidth = true;
            this.bgcAfternoon.Visible = true;
            this.bgcAfternoon.Width = 60;
            // 
            // bgcTotal
            // 
            this.bgcTotal.AppearanceCell.Options.UseTextOptions = true;
            this.bgcTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcTotal.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.bgcTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.bgcTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bgcTotal.Caption = "T";
            this.bgcTotal.FieldName = "Total";
            this.bgcTotal.Name = "bgcTotal";
            this.bgcTotal.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.bgcTotal.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcTotal.OptionsColumn.FixedWidth = true;
            this.bgcTotal.OptionsColumn.ReadOnly = true;
            this.bgcTotal.Visible = true;
            this.bgcTotal.Width = 60;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand2";
            this.gridBand2.Columns.Add(this.bgcBlank);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.OptionsBand.ShowCaption = false;
            this.gridBand2.VisibleIndex = 2;
            this.gridBand2.Width = 20;
            // 
            // bgcBlank
            // 
            this.bgcBlank.Name = "bgcBlank";
            this.bgcBlank.OptionsColumn.AllowEdit = false;
            this.bgcBlank.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.bgcBlank.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.bgcBlank.OptionsColumn.ReadOnly = true;
            this.bgcBlank.Visible = true;
            this.bgcBlank.Width = 20;
            // 
            // bgcWorkplace
            // 
            this.bgcWorkplace.Caption = "Workplaceid";
            this.bgcWorkplace.FieldName = "Workplaceid";
            this.bgcWorkplace.Name = "bgcWorkplace";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.lueMillMonth,
            this.btnBookingShow,
            this.btnBookingSave,
            this.lueMill,
            this.btnBookingBack,
            this.txtTonsTreated,
            this.txtTonsToPlant,
            this.lueBookingProdMonth,
            this.dteDate,
            this.cbBookingSections});
            this.ribbonControl1.Location = new System.Drawing.Point(2, 2);
            this.ribbonControl1.MaxItemId = 16;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpBookingSections,
            this.rpSection,
            this.repositoryItemDateEdit1,
            this.mwRepositoryItemProdMonth1,
            this.repositoryItemLookUpEdit2});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(959, 95);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // lueMillMonth
            // 
            this.lueMillMonth.Caption = "Mill Month";
            this.lueMillMonth.Edit = null;
            this.lueMillMonth.EditWidth = 150;
            this.lueMillMonth.Id = 1;
            this.lueMillMonth.Name = "lueMillMonth";
            // 
            // btnBookingShow
            // 
            this.btnBookingShow.Caption = "Show";
            this.btnBookingShow.Id = 5;
            this.btnBookingShow.ImageUri.Uri = "Zoom";
            this.btnBookingShow.Name = "btnBookingShow";
            this.btnBookingShow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBookingShow_ItemClick);
            // 
            // btnBookingSave
            // 
            this.btnBookingSave.Caption = "Save";
            this.btnBookingSave.Id = 7;
            this.btnBookingSave.ImageUri.Uri = "Save";
            this.btnBookingSave.Name = "btnBookingSave";
            this.btnBookingSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBookingSave_ItemClick);
            // 
            // lueMill
            // 
            this.lueMill.Caption = "Mill";
            this.lueMill.Edit = null;
            this.lueMill.EditWidth = 183;
            this.lueMill.Id = 8;
            this.lueMill.Name = "lueMill";
            // 
            // btnBookingBack
            // 
            this.btnBookingBack.Caption = "Back";
            this.btnBookingBack.Id = 9;
            this.btnBookingBack.ImageUri.Uri = "Backward";
            this.btnBookingBack.Name = "btnBookingBack";
            this.btnBookingBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBookingBack_ItemClick);
            // 
            // txtTonsTreated
            // 
            this.txtTonsTreated.Caption = "Plan Tons Treated";
            this.txtTonsTreated.Edit = null;
            this.txtTonsTreated.EditWidth = 150;
            this.txtTonsTreated.Id = 10;
            this.txtTonsTreated.Name = "txtTonsTreated";
            // 
            // txtTonsToPlant
            // 
            this.txtTonsToPlant.Caption = "Plan Tons To Plant";
            this.txtTonsToPlant.Edit = null;
            this.txtTonsToPlant.EditWidth = 150;
            this.txtTonsToPlant.Id = 11;
            this.txtTonsToPlant.Name = "txtTonsToPlant";
            // 
            // lueBookingProdMonth
            // 
            this.lueBookingProdMonth.Caption = "ProdMonth";
            this.lueBookingProdMonth.Edit = this.mwRepositoryItemProdMonth1;
            this.lueBookingProdMonth.EditWidth = 183;
            this.lueBookingProdMonth.Id = 12;
            this.lueBookingProdMonth.Name = "lueBookingProdMonth";
            this.lueBookingProdMonth.EditValueChanged += new System.EventHandler(this.lueBookingProdMonth_EditValueChanged);
            // 
            // mwRepositoryItemProdMonth1
            // 
            this.mwRepositoryItemProdMonth1.AutoHeight = false;
            this.mwRepositoryItemProdMonth1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.mwRepositoryItemProdMonth1.Mask.EditMask = "yyyyMM";
            this.mwRepositoryItemProdMonth1.Mask.IgnoreMaskBlank = false;
            this.mwRepositoryItemProdMonth1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.mwRepositoryItemProdMonth1.Mask.UseMaskAsDisplayFormat = true;
            this.mwRepositoryItemProdMonth1.Name = "mwRepositoryItemProdMonth1";
            // 
            // dteDate
            // 
            this.dteDate.Caption = "Date";
            this.dteDate.Edit = this.repositoryItemDateEdit1;
            this.dteDate.EditWidth = 212;
            this.dteDate.Id = 14;
            this.dteDate.Name = "dteDate";
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "yyyy-MM-dd";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.MaxLength = 10;
            this.repositoryItemDateEdit1.MaxValue = new System.DateTime(9999, 12, 31, 0, 0, 0, 0);
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // cbBookingSections
            // 
            this.cbBookingSections.Caption = "Section";
            this.cbBookingSections.Edit = this.rpBookingSections;
            this.cbBookingSections.EditWidth = 200;
            this.cbBookingSections.Id = 15;
            this.cbBookingSections.Name = "cbBookingSections";
            // 
            // rpBookingSections
            // 
            this.rpBookingSections.AutoHeight = false;
            this.rpBookingSections.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpBookingSections.Name = "rpBookingSections";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgSelection,
            this.rpgShow,
            this.rpgSave});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // rpgSelection
            // 
            this.rpgSelection.ItemLinks.Add(this.lueMillMonth);
            this.rpgSelection.ItemLinks.Add(this.lueMill);
            this.rpgSelection.ItemLinks.Add(this.lueBookingProdMonth);
            this.rpgSelection.ItemLinks.Add(this.cbBookingSections);
            this.rpgSelection.ItemLinks.Add(this.dteDate);
            this.rpgSelection.Name = "rpgSelection";
            // 
            // rpgShow
            // 
            this.rpgShow.ItemLinks.Add(this.btnBookingShow);
            this.rpgShow.Name = "rpgShow";
            // 
            // rpgSave
            // 
            this.rpgSave.ItemLinks.Add(this.btnBookingSave);
            this.rpgSave.ItemLinks.Add(this.btnBookingBack);
            this.rpgSave.Name = "rpgSave";
            // 
            // rpSection
            // 
            this.rpSection.AutoHeight = false;
            this.rpSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpSection.Name = "rpSection";
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            // 
            // ucTrammingBooking
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl3);
            this.Name = "ucTrammingBooking";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(963, 507);
            this.Load += new System.EventHandler(this.ucBooking_Load);
            this.Controls.SetChildIndex(this.panelControl3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBookings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bgvBookings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextEditUpdateTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwRepositoryItemProdMonth1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpBookingSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarEditItem lueMillMonth;
        private DevExpress.XtraBars.BarButtonItem btnBookingShow;
        private DevExpress.XtraBars.BarButtonItem btnBookingSave;
        private DevExpress.XtraBars.BarEditItem lueMill;
        private DevExpress.XtraBars.BarButtonItem btnBookingBack;
        private DevExpress.XtraBars.BarEditItem txtTonsTreated;
        private DevExpress.XtraBars.BarEditItem txtTonsToPlant;
        private DevExpress.XtraBars.BarEditItem lueBookingProdMonth;
        private Global.CustomControls.MWRepositoryItemProdMonth mwRepositoryItemProdMonth1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rpSection;
        private DevExpress.XtraBars.BarEditItem dteDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgSelection;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgShow;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgSave;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpBookingSections;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcBookings;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bgvBookings;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcXC;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcBH;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcTons;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcHoppers;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcProgressive;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcUnits;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gbDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcNight;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit TextEditUpdateTotal;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcMorning;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcAfternoon;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcTotal;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcBlank;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcWorkplace;
        private DevExpress.XtraBars.BarEditItem cbBookingSections;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bgcBookedDailyTons;
    }
}
