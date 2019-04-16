namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    partial class frmPrePlanningNotification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrePlanningNotification));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gvShafts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colShaft = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCPM_UserID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rieUser = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCPM_UserID2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCallChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reCheckBox = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.imageCollection2 = new DevExpress.Utils.ImageCollection();
            this.colCrewChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewWPChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopWPChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWPMove = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiningMethodChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartWPChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSettings = new DevExpress.XtraGrid.GridControl();
            this.dsPrePlanningNotificationBindingSource = new System.Windows.Forms.BindingSource();
            this.dsPrePlanningNotification = new Mineware.Systems.Planning.PrePlanning.ChangeOfPlan.dsPrePlanningNotification();
            this.gvDepartments = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gvSections = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCPM_UserID11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCPM_UserID21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartment1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCallChange1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCrewChange1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewWPChange1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStopWPChange1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWPMove1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiningMethodChange1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartWPChange1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gvShafts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rieUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reCheckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPrePlanningNotificationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPrePlanningNotification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvShafts
            // 
            this.gvShafts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colShaft,
            this.colCPM_UserID1,
            this.colCPM_UserID2,
            this.colDepartment,
            this.colCallChange,
            this.colCrewChange,
            this.colNewWPChange,
            this.colStopWPChange,
            this.colWPMove,
            this.colMiningMethodChange,
            this.colStartWPChange});
            this.gvShafts.GridControl = this.gcSettings;
            this.gvShafts.Name = "gvShafts";
            this.gvShafts.OptionsDetail.ShowDetailTabs = false;
            this.gvShafts.ViewCaption = "Shafts";
            // 
            // colShaft
            // 
            this.colShaft.Caption = "Shaft";
            this.colShaft.FieldName = "Shaft";
            this.colShaft.Name = "colShaft";
            this.colShaft.OptionsColumn.AllowEdit = false;
            this.colShaft.OptionsColumn.ReadOnly = true;
            this.colShaft.Visible = true;
            this.colShaft.VisibleIndex = 0;
            // 
            // colCPM_UserID1
            // 
            this.colCPM_UserID1.Caption = "User 1";
            this.colCPM_UserID1.ColumnEdit = this.rieUser;
            this.colCPM_UserID1.FieldName = "CPM_UserID1";
            this.colCPM_UserID1.Name = "colCPM_UserID1";
            this.colCPM_UserID1.Visible = true;
            this.colCPM_UserID1.VisibleIndex = 1;
            // 
            // rieUser
            // 
            this.rieUser.AutoHeight = false;
            this.rieUser.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rieUser.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UserName", "User Name")});
            this.rieUser.Name = "rieUser";
            this.rieUser.NullText = "";
            // 
            // colCPM_UserID2
            // 
            this.colCPM_UserID2.Caption = "User 2";
            this.colCPM_UserID2.ColumnEdit = this.rieUser;
            this.colCPM_UserID2.FieldName = "CPM_UserID2";
            this.colCPM_UserID2.Name = "colCPM_UserID2";
            this.colCPM_UserID2.Visible = true;
            this.colCPM_UserID2.VisibleIndex = 2;
            // 
            // colDepartment
            // 
            this.colDepartment.FieldName = "Department";
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.Visible = true;
            this.colDepartment.VisibleIndex = 10;
            // 
            // colCallChange
            // 
            this.colCallChange.ColumnEdit = this.reCheckBox;
            this.colCallChange.FieldName = "CallChange";
            this.colCallChange.Name = "colCallChange";
            this.colCallChange.Visible = true;
            this.colCallChange.VisibleIndex = 3;
            // 
            // reCheckBox
            // 
            this.reCheckBox.AutoHeight = false;
            this.reCheckBox.Caption = "Check";
            this.reCheckBox.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.reCheckBox.ImageOptions.ImageIndexChecked = 1;
            this.reCheckBox.ImageOptions.ImageIndexUnchecked = 0;
            this.reCheckBox.ImageOptions.Images = this.imageCollection2;
            this.reCheckBox.Name = "reCheckBox";
            // 
            // imageCollection2
            // 
            this.imageCollection2.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection2.ImageStream")));
            this.imageCollection2.Images.SetKeyName(0, "notApprovedTick.png");
            this.imageCollection2.Images.SetKeyName(1, "approveTick.png");
            // 
            // colCrewChange
            // 
            this.colCrewChange.ColumnEdit = this.reCheckBox;
            this.colCrewChange.FieldName = "CrewChange";
            this.colCrewChange.Name = "colCrewChange";
            this.colCrewChange.Visible = true;
            this.colCrewChange.VisibleIndex = 4;
            // 
            // colNewWPChange
            // 
            this.colNewWPChange.ColumnEdit = this.reCheckBox;
            this.colNewWPChange.FieldName = "NewWPChange";
            this.colNewWPChange.Name = "colNewWPChange";
            this.colNewWPChange.Visible = true;
            this.colNewWPChange.VisibleIndex = 5;
            // 
            // colStopWPChange
            // 
            this.colStopWPChange.ColumnEdit = this.reCheckBox;
            this.colStopWPChange.FieldName = "StopWPChange";
            this.colStopWPChange.Name = "colStopWPChange";
            this.colStopWPChange.Visible = true;
            this.colStopWPChange.VisibleIndex = 6;
            // 
            // colWPMove
            // 
            this.colWPMove.ColumnEdit = this.reCheckBox;
            this.colWPMove.FieldName = "WPMove";
            this.colWPMove.Name = "colWPMove";
            this.colWPMove.Visible = true;
            this.colWPMove.VisibleIndex = 7;
            // 
            // colMiningMethodChange
            // 
            this.colMiningMethodChange.ColumnEdit = this.reCheckBox;
            this.colMiningMethodChange.FieldName = "MiningMethodChange";
            this.colMiningMethodChange.Name = "colMiningMethodChange";
            this.colMiningMethodChange.Visible = true;
            this.colMiningMethodChange.VisibleIndex = 9;
            // 
            // colStartWPChange
            // 
            this.colStartWPChange.ColumnEdit = this.reCheckBox;
            this.colStartWPChange.FieldName = "StartWPChange";
            this.colStartWPChange.Name = "colStartWPChange";
            this.colStartWPChange.Visible = true;
            this.colStartWPChange.VisibleIndex = 8;
            // 
            // gcSettings
            // 
            this.gcSettings.DataMember = "Department";
            this.gcSettings.DataSource = this.dsPrePlanningNotificationBindingSource;
            this.gcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gvShafts;
            gridLevelNode2.LevelTemplate = this.gvSections;
            gridLevelNode2.RelationName = "Shafts_Sections";
            gridLevelNode1.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            gridLevelNode1.RelationName = "Department_Shafts";
            this.gcSettings.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcSettings.Location = new System.Drawing.Point(2, 20);
            this.gcSettings.MainView = this.gvDepartments;
            this.gcSettings.Name = "gcSettings";
            this.gcSettings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reCheckBox,
            this.rieUser});
            this.gcSettings.Size = new System.Drawing.Size(1086, 605);
            this.gcSettings.TabIndex = 0;
            this.gcSettings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDepartments,
            this.gvSections,
            this.gvShafts});
            // 
            // dsPrePlanningNotificationBindingSource
            // 
            this.dsPrePlanningNotificationBindingSource.DataSource = this.dsPrePlanningNotification;
            this.dsPrePlanningNotificationBindingSource.Position = 0;
            this.dsPrePlanningNotificationBindingSource.CurrentChanged += new System.EventHandler(this.dsPrePlanningNotificationBindingSource_CurrentChanged);
            // 
            // dsPrePlanningNotification
            // 
            this.dsPrePlanningNotification.DataSetName = "dsPrePlanningNotification";
            this.dsPrePlanningNotification.EnforceConstraints = false;
            this.dsPrePlanningNotification.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvDepartments
            // 
            this.gvDepartments.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.gvDepartments.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn1});
            this.gvDepartments.GridControl = this.gcSettings;
            this.gvDepartments.Images = this.imageCollection2;
            this.gvDepartments.Name = "gvDepartments";
            this.gvDepartments.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDepartments.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDepartments.OptionsFilter.AllowFilterEditor = false;
            this.gvDepartments.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvDepartments.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "Security";
            this.gridBand1.Columns.Add(this.bandedGridColumn1);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 479;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "Department";
            this.bandedGridColumn1.FieldName = "Description";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn1.OptionsColumn.ReadOnly = true;
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 479;
            // 
            // gvSections
            // 
            this.gvSections.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSection,
            this.colCPM_UserID11,
            this.colCPM_UserID21,
            this.colDepartment1,
            this.colCallChange1,
            this.colCrewChange1,
            this.colNewWPChange1,
            this.colStopWPChange1,
            this.colWPMove1,
            this.colMiningMethodChange1,
            this.colStartWPChange1});
            this.gvSections.GridControl = this.gcSettings;
            this.gvSections.Name = "gvSections";
            this.gvSections.OptionsDetail.ShowDetailTabs = false;
            this.gvSections.ViewCaption = "Sections";
            // 
            // colSection
            // 
            this.colSection.FieldName = "Section";
            this.colSection.Name = "colSection";
            this.colSection.OptionsColumn.AllowEdit = false;
            this.colSection.OptionsColumn.ReadOnly = true;
            this.colSection.Visible = true;
            this.colSection.VisibleIndex = 0;
            // 
            // colCPM_UserID11
            // 
            this.colCPM_UserID11.Caption = "User 1";
            this.colCPM_UserID11.ColumnEdit = this.rieUser;
            this.colCPM_UserID11.FieldName = "CPM_UserID1";
            this.colCPM_UserID11.Name = "colCPM_UserID11";
            this.colCPM_UserID11.Visible = true;
            this.colCPM_UserID11.VisibleIndex = 1;
            // 
            // colCPM_UserID21
            // 
            this.colCPM_UserID21.Caption = "User 2";
            this.colCPM_UserID21.ColumnEdit = this.rieUser;
            this.colCPM_UserID21.FieldName = "CPM_UserID2";
            this.colCPM_UserID21.Name = "colCPM_UserID21";
            this.colCPM_UserID21.Visible = true;
            this.colCPM_UserID21.VisibleIndex = 2;
            // 
            // colDepartment1
            // 
            this.colDepartment1.FieldName = "Department";
            this.colDepartment1.Name = "colDepartment1";
            this.colDepartment1.Visible = true;
            this.colDepartment1.VisibleIndex = 10;
            // 
            // colCallChange1
            // 
            this.colCallChange1.ColumnEdit = this.reCheckBox;
            this.colCallChange1.FieldName = "CallChange";
            this.colCallChange1.Name = "colCallChange1";
            this.colCallChange1.Visible = true;
            this.colCallChange1.VisibleIndex = 3;
            // 
            // colCrewChange1
            // 
            this.colCrewChange1.ColumnEdit = this.reCheckBox;
            this.colCrewChange1.FieldName = "CrewChange";
            this.colCrewChange1.Name = "colCrewChange1";
            this.colCrewChange1.Visible = true;
            this.colCrewChange1.VisibleIndex = 4;
            // 
            // colNewWPChange1
            // 
            this.colNewWPChange1.ColumnEdit = this.reCheckBox;
            this.colNewWPChange1.FieldName = "NewWPChange";
            this.colNewWPChange1.Name = "colNewWPChange1";
            this.colNewWPChange1.Visible = true;
            this.colNewWPChange1.VisibleIndex = 5;
            // 
            // colStopWPChange1
            // 
            this.colStopWPChange1.ColumnEdit = this.reCheckBox;
            this.colStopWPChange1.FieldName = "StopWPChange";
            this.colStopWPChange1.Name = "colStopWPChange1";
            this.colStopWPChange1.Visible = true;
            this.colStopWPChange1.VisibleIndex = 6;
            // 
            // colWPMove1
            // 
            this.colWPMove1.ColumnEdit = this.reCheckBox;
            this.colWPMove1.FieldName = "WPMove";
            this.colWPMove1.Name = "colWPMove1";
            this.colWPMove1.Visible = true;
            this.colWPMove1.VisibleIndex = 7;
            // 
            // colMiningMethodChange1
            // 
            this.colMiningMethodChange1.ColumnEdit = this.reCheckBox;
            this.colMiningMethodChange1.FieldName = "MiningMethodChange";
            this.colMiningMethodChange1.Name = "colMiningMethodChange1";
            this.colMiningMethodChange1.Visible = true;
            this.colMiningMethodChange1.VisibleIndex = 9;
            // 
            // colStartWPChange1
            // 
            this.colStartWPChange1.ColumnEdit = this.reCheckBox;
            this.colStartWPChange1.FieldName = "StartWPChange";
            this.colStartWPChange1.Name = "colStartWPChange1";
            this.colStartWPChange1.Visible = true;
            this.colStartWPChange1.VisibleIndex = 8;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcSettings);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 47);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1090, 627);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Settings";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1090, 47);
            this.panelControl1.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(86, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(5, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmPrePlanningNotification
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmPrePlanningNotification";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(1090, 674);
            this.Load += new System.EventHandler(this.frmPrePlanningNotification_Load);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.groupControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvShafts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rieUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reCheckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPrePlanningNotificationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPrePlanningNotification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcSettings;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit reCheckBox;
        private DevExpress.Utils.ImageCollection imageCollection2;
        private DevExpress.XtraGrid.Views.Grid.GridView gvShafts;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSections;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gvDepartments;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private dsPrePlanningNotification dsPrePlanningNotification;
        private DevExpress.XtraGrid.Columns.GridColumn colShaft;
        private DevExpress.XtraGrid.Columns.GridColumn colCPM_UserID1;
        private DevExpress.XtraGrid.Columns.GridColumn colCPM_UserID2;
        private DevExpress.XtraGrid.Columns.GridColumn colCallChange;
        private DevExpress.XtraGrid.Columns.GridColumn colCrewChange;
        private DevExpress.XtraGrid.Columns.GridColumn colNewWPChange;
        private DevExpress.XtraGrid.Columns.GridColumn colStopWPChange;
        private DevExpress.XtraGrid.Columns.GridColumn colWPMove;
        private DevExpress.XtraGrid.Columns.GridColumn colSection;
        private DevExpress.XtraGrid.Columns.GridColumn colCPM_UserID11;
        private DevExpress.XtraGrid.Columns.GridColumn colCPM_UserID21;
        private DevExpress.XtraGrid.Columns.GridColumn colCallChange1;
        private DevExpress.XtraGrid.Columns.GridColumn colCrewChange1;
        private DevExpress.XtraGrid.Columns.GridColumn colNewWPChange1;
        private DevExpress.XtraGrid.Columns.GridColumn colStopWPChange1;
        private DevExpress.XtraGrid.Columns.GridColumn colWPMove1;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartment1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rieUser;
        private System.Windows.Forms.BindingSource dsPrePlanningNotificationBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colStartWPChange;
        private DevExpress.XtraGrid.Columns.GridColumn colStartWPChange1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.Columns.GridColumn colMiningMethodChange;
        private DevExpress.XtraGrid.Columns.GridColumn colMiningMethodChange1;
    }
}