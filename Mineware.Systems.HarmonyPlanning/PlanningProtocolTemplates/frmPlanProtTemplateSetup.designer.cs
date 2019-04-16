namespace Mineware.Systems.Planning.PlanningProtocolTemplates
{
    partial class frmPlanProtTemplateSetup
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlanProtTemplateSetup));
            this.gvUnits = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.userList = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colUser22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.theTemplatesBindingSource = new System.Windows.Forms.BindingSource();
            this.dsTemplateSecurityBindingSource = new System.Windows.Forms.BindingSource();
            this.dsTemplateSecurity = new Mineware.Systems.Planning.PlanningProtocolTemplates.dsTemplateSecurity();
            this.gvTemplates = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTemplateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvShaft = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colShaft2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvSection = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSection2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.gridSecurity = new DevExpress.XtraGrid.GridControl();
            this.gvSecuritySettings = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gcUSERPROFILEID = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gcAccess = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.chkAccess = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcReadonly = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridOutput = new DevExpress.XtraGrid.GridControl();
            this.viewFields = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.rgReqApproval = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShaft = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTemplateID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSection1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShaft1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTemplateID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUser21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcShaft = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUser1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcUser2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.mwButton2 = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnSave = new Mineware.Systems.Global.CustomControls.MWButton();
            this.theTemplatesTableAdapter = new Mineware.Systems.Planning.PlanningProtocolTemplates.dsTemplateSecurityTableAdapters.theTemplatesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.theTemplatesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTemplateSecurityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTemplateSecurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvShaft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSecurity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSecuritySettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgReqApproval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvUnits
            // 
            this.gvUnits.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUnit,
            this.colUser12,
            this.colUser22});
            this.gvUnits.GridControl = this.gridControl1;
            this.gvUnits.Name = "gvUnits";
            this.gvUnits.OptionsView.ShowGroupPanel = false;
            this.gvUnits.ViewCaption = "theUnits";
            this.gvUnits.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvUnits_RowUpdated);
            // 
            // colUnit
            // 
            this.colUnit.CustomizationCaption = "Unit";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.OptionsColumn.ReadOnly = true;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 0;
            // 
            // colUser12
            // 
            this.colUser12.ColumnEdit = this.userList;
            this.colUser12.FieldName = "User1";
            this.colUser12.Name = "colUser12";
            this.colUser12.Visible = true;
            this.colUser12.VisibleIndex = 1;
            // 
            // userList
            // 
            this.userList.AutoHeight = false;
            this.userList.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.userList.Name = "userList";
            this.userList.NullText = "";
            // 
            // colUser22
            // 
            this.colUser22.ColumnEdit = this.userList;
            this.colUser22.FieldName = "User2";
            this.colUser22.Name = "colUser22";
            this.colUser22.Visible = true;
            this.colUser22.VisibleIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.AllowDrop = true;
            this.gridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl1.DataSource = this.theTemplatesBindingSource;
            gridLevelNode1.LevelTemplate = this.gvUnits;
            gridLevelNode2.LevelTemplate = this.gvShaft;
            gridLevelNode3.LevelTemplate = this.gvSection;
            gridLevelNode3.RelationName = "theShaft_theSection";
            gridLevelNode2.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode3});
            gridLevelNode2.RelationName = "theShaft_theUnits";
            gridLevelNode1.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            gridLevelNode1.RelationName = "theTemplates_theUnits";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(469, 28);
            this.gridControl1.MainView = this.gvTemplates;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.userList});
            this.gridControl1.Size = new System.Drawing.Size(485, 641);
            this.gridControl1.TabIndex = 19;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTemplates,
            this.gvShaft,
            this.gvSection,
            this.gvUnits});
            // 
            // theTemplatesBindingSource
            // 
            this.theTemplatesBindingSource.DataMember = "theTemplates";
            this.theTemplatesBindingSource.DataSource = this.dsTemplateSecurityBindingSource;
            // 
            // dsTemplateSecurityBindingSource
            // 
            this.dsTemplateSecurityBindingSource.DataSource = this.dsTemplateSecurity;
            this.dsTemplateSecurityBindingSource.Position = 0;
            // 
            // dsTemplateSecurity
            // 
            this.dsTemplateSecurity.DataSetName = "dsTemplateSecurity";
            this.dsTemplateSecurity.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvTemplates
            // 
            this.gvTemplates.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTemplateName});
            this.gvTemplates.GridControl = this.gridControl1;
            this.gvTemplates.Name = "gvTemplates";
            this.gvTemplates.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gvTemplates.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
            this.gvTemplates.OptionsView.ShowGroupPanel = false;
            // 
            // colTemplateName
            // 
            this.colTemplateName.FieldName = "TemplateName";
            this.colTemplateName.Name = "colTemplateName";
            this.colTemplateName.OptionsColumn.AllowEdit = false;
            this.colTemplateName.OptionsColumn.ReadOnly = true;
            this.colTemplateName.Visible = true;
            this.colTemplateName.VisibleIndex = 0;
            // 
            // gvShaft
            // 
            this.gvShaft.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colShaft2,
            this.colUser13,
            this.colUser23});
            this.gvShaft.GridControl = this.gridControl1;
            this.gvShaft.Name = "gvShaft";
            this.gvShaft.OptionsView.ShowGroupPanel = false;
            this.gvShaft.ViewCaption = "theShaft";
            this.gvShaft.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvShaft_RowUpdated_1);
            // 
            // colShaft2
            // 
            this.colShaft2.Caption = "Shaft";
            this.colShaft2.FieldName = "Shaft";
            this.colShaft2.Name = "colShaft2";
            this.colShaft2.OptionsColumn.AllowEdit = false;
            this.colShaft2.OptionsColumn.ReadOnly = true;
            this.colShaft2.Visible = true;
            this.colShaft2.VisibleIndex = 0;
            // 
            // colUser13
            // 
            this.colUser13.ColumnEdit = this.userList;
            this.colUser13.FieldName = "User1";
            this.colUser13.Name = "colUser13";
            this.colUser13.Visible = true;
            this.colUser13.VisibleIndex = 1;
            // 
            // colUser23
            // 
            this.colUser23.ColumnEdit = this.userList;
            this.colUser23.FieldName = "User2";
            this.colUser23.Name = "colUser23";
            this.colUser23.Visible = true;
            this.colUser23.VisibleIndex = 2;
            // 
            // gvSection
            // 
            this.gvSection.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSection2,
            this.colUser14,
            this.colUser24});
            this.gvSection.GridControl = this.gridControl1;
            this.gvSection.Name = "gvSection";
            this.gvSection.OptionsView.ShowGroupPanel = false;
            this.gvSection.ViewCaption = "theSection";
            this.gvSection.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvSection_RowUpdated);
            // 
            // colSection2
            // 
            this.colSection2.Caption = "Section";
            this.colSection2.FieldName = "Section";
            this.colSection2.Name = "colSection2";
            this.colSection2.OptionsColumn.AllowEdit = false;
            this.colSection2.OptionsColumn.ReadOnly = true;
            this.colSection2.Visible = true;
            this.colSection2.VisibleIndex = 0;
            // 
            // colUser14
            // 
            this.colUser14.ColumnEdit = this.userList;
            this.colUser14.FieldName = "User1";
            this.colUser14.Name = "colUser14";
            this.colUser14.Visible = true;
            this.colUser14.VisibleIndex = 1;
            // 
            // colUser24
            // 
            this.colUser24.ColumnEdit = this.userList;
            this.colUser24.FieldName = "User2";
            this.colUser24.Name = "colUser24";
            this.colUser24.Visible = true;
            this.colUser24.VisibleIndex = 2;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(170, 12);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(295, 21);
            this.cmbType.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(170, 37);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(295, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "no.png");
            this.imageList1.Images.SetKeyName(1, "OK.png");
            // 
            // cardView1
            // 
            this.cardView1.FocusedCardTopFieldIndex = 0;
            this.cardView1.GridControl = this.gridSecurity;
            this.cardView1.Name = "cardView1";
            this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            // 
            // gridSecurity
            // 
            this.gridSecurity.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridSecurity.Location = new System.Drawing.Point(12, 135);
            this.gridSecurity.MainView = this.gvSecuritySettings;
            this.gridSecurity.Name = "gridSecurity";
            this.gridSecurity.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkAccess});
            this.gridSecurity.Size = new System.Drawing.Size(453, 306);
            this.gridSecurity.TabIndex = 7;
            this.gridSecurity.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSecuritySettings,
            this.cardView1});
            this.gridSecurity.Click += new System.EventHandler(this.gridSecurity_Click);
            // 
            // gvSecuritySettings
            // 
            this.gvSecuritySettings.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.gvSecuritySettings.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.gcUSERPROFILEID,
            this.gcAccess,
            this.gcReadonly});
            this.gvSecuritySettings.GridControl = this.gridSecurity;
            this.gvSecuritySettings.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gvSecuritySettings.Name = "gvSecuritySettings";
            this.gvSecuritySettings.OptionsView.ShowGroupPanel = false;
            this.gvSecuritySettings.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.bandedGridView1_CellValueChanged);
            this.gvSecuritySettings.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.bandedGridView1_CellValueChanging);
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.gcUSERPROFILEID);
            this.gridBand1.Columns.Add(this.gcAccess);
            this.gridBand1.Columns.Add(this.gcReadonly);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 454;
            // 
            // gcUSERPROFILEID
            // 
            this.gcUSERPROFILEID.Caption = "Department";
            this.gcUSERPROFILEID.FieldName = "Description";
            this.gcUSERPROFILEID.Name = "gcUSERPROFILEID";
            this.gcUSERPROFILEID.OptionsColumn.AllowEdit = false;
            this.gcUSERPROFILEID.OptionsColumn.ReadOnly = true;
            this.gcUSERPROFILEID.Visible = true;
            this.gcUSERPROFILEID.Width = 284;
            // 
            // gcAccess
            // 
            this.gcAccess.Caption = "Full Access";
            this.gcAccess.ColumnEdit = this.chkAccess;
            this.gcAccess.FieldName = "FullAccess";
            this.gcAccess.Name = "gcAccess";
            this.gcAccess.Visible = true;
            this.gcAccess.Width = 80;
            // 
            // chkAccess
            // 
            this.chkAccess.AutoHeight = false;
            this.chkAccess.Caption = "Check";
            this.chkAccess.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.chkAccess.ImageOptions.ImageIndexChecked = 1;
            this.chkAccess.ImageOptions.ImageIndexUnchecked = 0;
            this.chkAccess.ImageOptions.Images = this.imageList1;
            this.chkAccess.Name = "chkAccess";
            // 
            // gcReadonly
            // 
            this.gcReadonly.Caption = "Read Only";
            this.gcReadonly.ColumnEdit = this.chkAccess;
            this.gcReadonly.FieldName = "ReadOnlyAccess";
            this.gcReadonly.Name = "gcReadonly";
            this.gcReadonly.Visible = true;
            this.gcReadonly.Width = 90;
            // 
            // gridOutput
            // 
            this.gridOutput.Location = new System.Drawing.Point(12, 461);
            this.gridOutput.MainView = this.viewFields;
            this.gridOutput.Name = "gridOutput";
            this.gridOutput.Size = new System.Drawing.Size(349, 208);
            this.gridOutput.TabIndex = 14;
            this.gridOutput.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewFields});
            // 
            // viewFields
            // 
            this.viewFields.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2});
            this.viewFields.GridControl = this.gridOutput;
            this.viewFields.Name = "viewFields";
            this.viewFields.OptionsBehavior.Editable = false;
            this.viewFields.OptionsBehavior.ReadOnly = true;
            this.viewFields.OptionsView.ShowGroupPanel = false;
            this.viewFields.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.bandedGridView2_RowCellClick);
            // 
            // gridBand2
            // 
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.rgReqApproval);
            this.layoutControl1.Controls.Add(this.cmbType);
            this.layoutControl1.Controls.Add(this.txtDescription);
            this.layoutControl1.Controls.Add(this.gridSecurity);
            this.layoutControl1.Controls.Add(this.gridOutput);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(966, 681);
            this.layoutControl1.TabIndex = 17;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Location = new System.Drawing.Point(365, 445);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(100, 224);
            this.panelControl1.TabIndex = 15;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 23);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(6, 34);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(91, 23);
            this.btnEdit.TabIndex = 18;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(6, 63);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 23);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // rgReqApproval
            // 
            this.rgReqApproval.Location = new System.Drawing.Point(170, 61);
            this.rgReqApproval.Name = "rgReqApproval";
            this.rgReqApproval.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "Yes"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "No")});
            this.rgReqApproval.Size = new System.Drawing.Size(295, 54);
            this.rgReqApproval.StyleController = this.layoutControl1;
            this.rgReqApproval.TabIndex = 18;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem8,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem9});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(966, 681);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem2.Control = this.txtDescription;
            this.layoutControlItem2.CustomizationFormText = "Template Desctiption";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(457, 24);
            this.layoutControlItem2.Text = "Template Description";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(155, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AllowHide = false;
            this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem1.Control = this.cmbType;
            this.layoutControlItem1.CustomizationFormText = "Template Type";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(457, 25);
            this.layoutControlItem1.Text = "Template Type";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(155, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem3.Control = this.gridSecurity;
            this.layoutControlItem3.CustomizationFormText = "Security Settings";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 107);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(457, 326);
            this.layoutControlItem3.Text = "Security Settings";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(155, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem8.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem8.Control = this.rgReqApproval;
            this.layoutControlItem8.CustomizationFormText = "Planning Requires Approval";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 49);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(457, 58);
            this.layoutControlItem8.Text = "Planning Requires Approval";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(155, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem4.Control = this.gridOutput;
            this.layoutControlItem4.CustomizationFormText = "Fields";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 433);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(353, 228);
            this.layoutControlItem4.Text = "Fields";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(155, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.panelControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(353, 433);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(104, 228);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.AppearanceItemCaption.BackColor = System.Drawing.Color.Transparent;
            this.layoutControlItem9.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem9.AppearanceItemCaption.Options.UseBackColor = true;
            this.layoutControlItem9.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem9.Control = this.gridControl1;
            this.layoutControlItem9.CustomizationFormText = "Approval";
            this.layoutControlItem9.Location = new System.Drawing.Point(457, 0);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(159, 40);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(489, 661);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "Approval";
            this.layoutControlItem9.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(155, 13);
            // 
            // colSection
            // 
            this.colSection.FieldName = "Section";
            this.colSection.Name = "colSection";
            this.colSection.Visible = true;
            this.colSection.VisibleIndex = 0;
            // 
            // colShaft
            // 
            this.colShaft.FieldName = "Shaft";
            this.colShaft.Name = "colShaft";
            this.colShaft.Visible = true;
            this.colShaft.VisibleIndex = 1;
            // 
            // colTemplateID
            // 
            this.colTemplateID.FieldName = "TemplateID";
            this.colTemplateID.Name = "colTemplateID";
            this.colTemplateID.Visible = true;
            this.colTemplateID.VisibleIndex = 2;
            // 
            // colUser1
            // 
            this.colUser1.FieldName = "User1";
            this.colUser1.Name = "colUser1";
            this.colUser1.Visible = true;
            this.colUser1.VisibleIndex = 3;
            // 
            // colUser2
            // 
            this.colUser2.FieldName = "User2";
            this.colUser2.Name = "colUser2";
            this.colUser2.Visible = true;
            this.colUser2.VisibleIndex = 4;
            // 
            // colSection1
            // 
            this.colSection1.FieldName = "Section";
            this.colSection1.Name = "colSection1";
            this.colSection1.Visible = true;
            this.colSection1.VisibleIndex = 0;
            // 
            // colShaft1
            // 
            this.colShaft1.FieldName = "Shaft";
            this.colShaft1.Name = "colShaft1";
            this.colShaft1.Visible = true;
            this.colShaft1.VisibleIndex = 1;
            // 
            // colTemplateID1
            // 
            this.colTemplateID1.FieldName = "TemplateID";
            this.colTemplateID1.Name = "colTemplateID1";
            this.colTemplateID1.Visible = true;
            this.colTemplateID1.VisibleIndex = 2;
            // 
            // colUser11
            // 
            this.colUser11.FieldName = "User1";
            this.colUser11.Name = "colUser11";
            this.colUser11.Visible = true;
            this.colUser11.VisibleIndex = 3;
            // 
            // colUser21
            // 
            this.colUser21.FieldName = "User2";
            this.colUser21.Name = "colUser21";
            this.colUser21.Visible = true;
            this.colUser21.VisibleIndex = 4;
            // 
            // gcShaft
            // 
            this.gcShaft.Caption = "Shaft";
            this.gcShaft.FieldName = "Shaft";
            this.gcShaft.Name = "gcShaft";
            this.gcShaft.Visible = true;
            this.gcShaft.VisibleIndex = 0;
            // 
            // gcUser1
            // 
            this.gcUser1.Caption = "User 1";
            this.gcUser1.FieldName = "User1";
            this.gcUser1.Name = "gcUser1";
            this.gcUser1.Visible = true;
            this.gcUser1.VisibleIndex = 1;
            // 
            // gcUser2
            // 
            this.gcUser2.Caption = "User 2";
            this.gcUser2.FieldName = "User2";
            this.gcUser2.Name = "gcUser2";
            this.gcUser2.Visible = true;
            this.gcUser2.VisibleIndex = 2;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.mwButton2);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(966, 40);
            this.panelControl2.TabIndex = 18;
            this.panelControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl2_Paint);
            // 
            // mwButton2
            // 
            this.mwButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.mwButton2.ImageLeftPadding = 0;
            this.mwButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mwButton2.ImageOptions.Image")));
            this.mwButton2.Location = new System.Drawing.Point(75, 0);
            this.mwButton2.Name = "mwButton2";
            this.mwButton2.Size = new System.Drawing.Size(75, 40);
            this.mwButton2.TabIndex = 17;
            this.mwButton2.Text = "Cancel";
            this.mwButton2.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Cancel;
            this.mwButton2.Click += new System.EventHandler(this.mwButton2_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.ImageLeftPadding = 0;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 40);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Save;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // theTemplatesTableAdapter
            // 
            this.theTemplatesTableAdapter.ClearBeforeFill = true;
            // 
            // frmPlanProtTemplateSetup
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "frmPlanProtTemplateSetup";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(966, 721);
            this.Load += new System.EventHandler(this.frmPlanProtTemplateSetup_Load);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvUnits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.theTemplatesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTemplateSecurityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsTemplateSecurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvShaft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSecurity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSecuritySettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgReqApproval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
        private DevExpress.XtraGrid.GridControl gridSecurity;
        private DevExpress.XtraGrid.GridControl gridOutput;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewFields;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvSecuritySettings;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcUSERPROFILEID;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcAccess;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkAccess;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gcReadonly;
        private DevExpress.XtraEditors.RadioGroup rgReqApproval;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.Columns.GridColumn colSection;
        private DevExpress.XtraGrid.Columns.GridColumn colShaft;
        private DevExpress.XtraGrid.Columns.GridColumn colTemplateID;
        private DevExpress.XtraGrid.Columns.GridColumn colUser1;
        private DevExpress.XtraGrid.Columns.GridColumn colUser2;
        private DevExpress.XtraGrid.Columns.GridColumn colSection1;
        private DevExpress.XtraGrid.Columns.GridColumn colShaft1;
        private DevExpress.XtraGrid.Columns.GridColumn colTemplateID1;
        private DevExpress.XtraGrid.Columns.GridColumn colUser11;
        private DevExpress.XtraGrid.Columns.GridColumn colUser21;
        private DevExpress.XtraGrid.Columns.GridColumn gcShaft;
        private DevExpress.XtraGrid.Columns.GridColumn gcUser1;
        private DevExpress.XtraGrid.Columns.GridColumn gcUser2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit userList;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.BindingSource dsTemplateSecurityBindingSource;
        private Mineware.Systems.Planning.PlanningProtocolTemplates.dsTemplateSecurity dsTemplateSecurity;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTemplates;
        private System.Windows.Forms.BindingSource theTemplatesBindingSource;
        private Mineware.Systems.Planning.PlanningProtocolTemplates.dsTemplateSecurityTableAdapters.theTemplatesTableAdapter theTemplatesTableAdapter;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUnits;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colUser12;
        private DevExpress.XtraGrid.Columns.GridColumn colUser22;
        private DevExpress.XtraGrid.Views.Grid.GridView gvShaft;
        private DevExpress.XtraGrid.Columns.GridColumn colShaft2;
        private DevExpress.XtraGrid.Columns.GridColumn colUser13;
        private DevExpress.XtraGrid.Columns.GridColumn colUser23;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSection;
        private DevExpress.XtraGrid.Columns.GridColumn colSection2;
        private DevExpress.XtraGrid.Columns.GridColumn colUser14;
        private DevExpress.XtraGrid.Columns.GridColumn colUser24;
        private DevExpress.XtraGrid.Columns.GridColumn colTemplateName;
        private Global.CustomControls.MWButton mwButton2;
        private Global.CustomControls.MWButton btnSave;
     
     
    }
}