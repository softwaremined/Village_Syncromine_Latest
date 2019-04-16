namespace Mineware.Systems.Planning.PlanningProtocolCapture
{
    partial class ucHRPlanning
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition3 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition4 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucHRPlanning));
            this.colPlanDay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPlanNigth = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gvHRPlann = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colDesignation = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colOrgUnitDay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colOrgUnitNight = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colStdDay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStdNight = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colPlanDayLast = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colPlanNigthLast = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.dsHRPlanningBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsHRPlanning = new Mineware.Systems.Planning.PlanningProtocolCapture.dsHRPlanning();
            this.gvWorkplaceList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWorkplaceDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcValidMiningMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.imageIndication = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gcNightCrewValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPanelLengthValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStopeWidthValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDrillRigValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNumberOfEndsValid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPlanDAY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPlanNIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.imagesStatus = new DevExpress.Utils.ImageCollection(this.components);
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPL = new DevExpress.XtraTab.XtraTabPage();
            this.tabAL = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gvUnplannedLabour = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAtWorkPlan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcInServicePlan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinEditPlannedValue = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHRPlann)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHRPlanningBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHRPlanning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkplaceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageIndication)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagesStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPL.SuspendLayout();
            this.tabAL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnplannedLabour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditPlannedValue)).BeginInit();
            this.SuspendLayout();
            // 
            // colPlanDay
            // 
            this.colPlanDay.FieldName = "PlanDay";
            this.colPlanDay.Name = "colPlanDay";
            this.colPlanDay.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colPlanDay.Visible = true;
            this.colPlanDay.Width = 59;
            // 
            // colPlanNigth
            // 
            this.colPlanNigth.Caption = "Plan Night";
            this.colPlanNigth.FieldName = "PlanNigth";
            this.colPlanNigth.Name = "colPlanNigth";
            this.colPlanNigth.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colPlanNigth.Visible = true;
            this.colPlanNigth.Width = 63;
            // 
            // gvHRPlann
            // 
            this.gvHRPlann.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3,
            this.gridBand4,
            this.gridBand5});
            this.gvHRPlann.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colDesignation,
            this.colOrgUnitDay,
            this.colOrgUnitNight,
            this.colStdDay,
            this.colStdNight,
            this.colPlanDay,
            this.colPlanNigth,
            this.colPlanDayLast,
            this.colPlanNigthLast});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.Column = this.colPlanDay;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[PlanDay] > [StdDay]";
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.Column = this.colPlanDay;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition2.Expression = "[PlanDay] < [StdDay]";
            styleFormatCondition3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            styleFormatCondition3.Appearance.Options.UseBackColor = true;
            styleFormatCondition3.Column = this.colPlanNigth;
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition3.Expression = "[PlanNigth] > [StdNight]";
            styleFormatCondition4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            styleFormatCondition4.Appearance.Options.UseBackColor = true;
            styleFormatCondition4.Column = this.colPlanNigth;
            styleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition4.Expression = "[PlanNigth] < [StdNight]";
            this.gvHRPlann.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2,
            styleFormatCondition3,
            styleFormatCondition4});
            this.gvHRPlann.GridControl = this.gridControl1;
            this.gvHRPlann.Name = "gvHRPlann";
            this.gvHRPlann.OptionsView.ShowFooter = true;
            this.gvHRPlann.OptionsView.ShowGroupPanel = false;
            this.gvHRPlann.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvHRPlann_RowCellClick);
            this.gvHRPlann.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvHRPlann_CustomRowCellEdit);
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.colDesignation);
            this.gridBand1.MinWidth = 20;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 190;
            // 
            // colDesignation
            // 
            this.colDesignation.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colDesignation.AppearanceCell.Options.UseBackColor = true;
            this.colDesignation.FieldName = "Designation";
            this.colDesignation.Name = "colDesignation";
            this.colDesignation.OptionsColumn.AllowEdit = false;
            this.colDesignation.OptionsColumn.ReadOnly = true;
            this.colDesignation.Visible = true;
            this.colDesignation.Width = 190;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "Crew";
            this.gridBand2.Columns.Add(this.colOrgUnitDay);
            this.gridBand2.Columns.Add(this.colOrgUnitNight);
            this.gridBand2.MinWidth = 20;
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 150;
            // 
            // colOrgUnitDay
            // 
            this.colOrgUnitDay.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colOrgUnitDay.AppearanceCell.Options.UseBackColor = true;
            this.colOrgUnitDay.FieldName = "OrgUnitDay";
            this.colOrgUnitDay.Name = "colOrgUnitDay";
            this.colOrgUnitDay.OptionsColumn.AllowEdit = false;
            this.colOrgUnitDay.OptionsColumn.ReadOnly = true;
            this.colOrgUnitDay.Visible = true;
            // 
            // colOrgUnitNight
            // 
            this.colOrgUnitNight.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colOrgUnitNight.AppearanceCell.Options.UseBackColor = true;
            this.colOrgUnitNight.FieldName = "OrgUnitNight";
            this.colOrgUnitNight.Name = "colOrgUnitNight";
            this.colOrgUnitNight.OptionsColumn.AllowEdit = false;
            this.colOrgUnitNight.OptionsColumn.ReadOnly = true;
            this.colOrgUnitNight.Visible = true;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "Standard & Norm";
            this.gridBand3.Columns.Add(this.colStdDay);
            this.gridBand3.Columns.Add(this.colStdNight);
            this.gridBand3.MinWidth = 20;
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 2;
            this.gridBand3.Width = 130;
            // 
            // colStdDay
            // 
            this.colStdDay.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colStdDay.AppearanceCell.Options.UseBackColor = true;
            this.colStdDay.FieldName = "StdDay";
            this.colStdDay.Name = "colStdDay";
            this.colStdDay.OptionsColumn.AllowEdit = false;
            this.colStdDay.OptionsColumn.ReadOnly = true;
            this.colStdDay.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colStdDay.Visible = true;
            this.colStdDay.Width = 65;
            // 
            // colStdNight
            // 
            this.colStdNight.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colStdNight.AppearanceCell.Options.UseBackColor = true;
            this.colStdNight.FieldName = "StdNight";
            this.colStdNight.Name = "colStdNight";
            this.colStdNight.OptionsColumn.AllowEdit = false;
            this.colStdNight.OptionsColumn.ReadOnly = true;
            this.colStdNight.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colStdNight.Visible = true;
            this.colStdNight.Width = 65;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "Plan";
            this.gridBand4.Columns.Add(this.colPlanDay);
            this.gridBand4.Columns.Add(this.colPlanNigth);
            this.gridBand4.MinWidth = 20;
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.VisibleIndex = 3;
            this.gridBand4.Width = 122;
            // 
            // gridBand5
            // 
            this.gridBand5.Caption = "Plan Previous Month";
            this.gridBand5.Columns.Add(this.colPlanDayLast);
            this.gridBand5.Columns.Add(this.colPlanNigthLast);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.VisibleIndex = 4;
            this.gridBand5.Width = 116;
            // 
            // colPlanDayLast
            // 
            this.colPlanDayLast.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colPlanDayLast.AppearanceCell.Options.UseBackColor = true;
            this.colPlanDayLast.Caption = "Plan Day";
            this.colPlanDayLast.FieldName = "PlanDayLast";
            this.colPlanDayLast.Name = "colPlanDayLast";
            this.colPlanDayLast.OptionsColumn.AllowEdit = false;
            this.colPlanDayLast.OptionsColumn.ReadOnly = true;
            this.colPlanDayLast.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colPlanDayLast.Visible = true;
            this.colPlanDayLast.Width = 57;
            // 
            // colPlanNigthLast
            // 
            this.colPlanNigthLast.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colPlanNigthLast.AppearanceCell.Options.UseBackColor = true;
            this.colPlanNigthLast.Caption = "Plan Night";
            this.colPlanNigthLast.FieldName = "PlanNigthLast";
            this.colPlanNigthLast.Name = "colPlanNigthLast";
            this.colPlanNigthLast.OptionsColumn.AllowEdit = false;
            this.colPlanNigthLast.OptionsColumn.ReadOnly = true;
            this.colPlanNigthLast.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.colPlanNigthLast.Visible = true;
            this.colPlanNigthLast.Width = 59;
            // 
            // gridControl1
            // 
            this.gridControl1.DataMember = "PrePlanning";
            this.gridControl1.DataSource = this.dsHRPlanningBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gvHRPlann;
            gridLevelNode1.RelationName = "PrePlanning_HR_GetStdAndNorm";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gvWorkplaceList;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.imageIndication});
            this.gridControl1.Size = new System.Drawing.Size(884, 570);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWorkplaceList,
            this.gvHRPlann});
            // 
            // dsHRPlanningBindingSource
            // 
            this.dsHRPlanningBindingSource.DataSource = this.dsHRPlanning;
            this.dsHRPlanningBindingSource.Position = 0;
            // 
            // dsHRPlanning
            // 
            this.dsHRPlanning.DataSetName = "dsHRPlanning";
            this.dsHRPlanning.EnforceConstraints = false;
            this.dsHRPlanning.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvWorkplaceList
            // 
            this.gvWorkplaceList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWorkplaceDesc,
            this.gcValidMiningMethod,
            this.gcNightCrewValid,
            this.gcPanelLengthValid,
            this.gcStopeWidthValid,
            this.gcDrillRigValid,
            this.gcNumberOfEndsValid,
            this.gcPlanDAY,
            this.gcPlanNIGHT});
            this.gvWorkplaceList.GridControl = this.gridControl1;
            this.gvWorkplaceList.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "HR_GetStdAndNorm.OrgUnitDay", null, "")});
            this.gvWorkplaceList.Images = this.imagesStatus;
            this.gvWorkplaceList.Name = "gvWorkplaceList";
            this.gvWorkplaceList.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gvWorkplaceList.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvWorkplaceList.OptionsView.ShowGroupPanel = false;
            this.gvWorkplaceList.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvWorkplaceList_CustomDrawCell);
            this.gvWorkplaceList.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gvWorkplaceList_MasterRowExpanded);
            this.gvWorkplaceList.MasterRowCollapsed += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gvWorkplaceList_MasterRowCollapsed);
            this.gvWorkplaceList.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvWorkplaceList_CustomUnboundColumnData);
            // 
            // colWorkplaceDesc
            // 
            this.colWorkplaceDesc.FieldName = "WorkplaceDesc";
            this.colWorkplaceDesc.Name = "colWorkplaceDesc";
            this.colWorkplaceDesc.OptionsColumn.AllowEdit = false;
            this.colWorkplaceDesc.OptionsColumn.ReadOnly = true;
            this.colWorkplaceDesc.Visible = true;
            this.colWorkplaceDesc.VisibleIndex = 0;
            // 
            // gcValidMiningMethod
            // 
            this.gcValidMiningMethod.Caption = "Mining Method Valid";
            this.gcValidMiningMethod.ColumnEdit = this.imageIndication;
            this.gcValidMiningMethod.FieldName = "TargetIDValid1";
            this.gcValidMiningMethod.Name = "gcValidMiningMethod";
            this.gcValidMiningMethod.OptionsColumn.AllowEdit = false;
            this.gcValidMiningMethod.OptionsColumn.ReadOnly = true;
            this.gcValidMiningMethod.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gcValidMiningMethod.Visible = true;
            this.gcValidMiningMethod.VisibleIndex = 1;
            // 
            // imageIndication
            // 
            this.imageIndication.Name = "imageIndication";
            // 
            // gcNightCrewValid
            // 
            this.gcNightCrewValid.Caption = "Night Crew Valid";
            this.gcNightCrewValid.ColumnEdit = this.imageIndication;
            this.gcNightCrewValid.FieldName = "NightCrewValid1";
            this.gcNightCrewValid.Name = "gcNightCrewValid";
            this.gcNightCrewValid.OptionsColumn.AllowEdit = false;
            this.gcNightCrewValid.OptionsColumn.ReadOnly = true;
            this.gcNightCrewValid.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gcNightCrewValid.Visible = true;
            this.gcNightCrewValid.VisibleIndex = 2;
            // 
            // gcPanelLengthValid
            // 
            this.gcPanelLengthValid.Caption = "Panel Length Valid";
            this.gcPanelLengthValid.ColumnEdit = this.imageIndication;
            this.gcPanelLengthValid.FieldName = "PanelLengthValid1";
            this.gcPanelLengthValid.Name = "gcPanelLengthValid";
            this.gcPanelLengthValid.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gcPanelLengthValid.Visible = true;
            this.gcPanelLengthValid.VisibleIndex = 3;
            // 
            // gcStopeWidthValid
            // 
            this.gcStopeWidthValid.Caption = "Stope Width Valid";
            this.gcStopeWidthValid.ColumnEdit = this.imageIndication;
            this.gcStopeWidthValid.FieldName = "StopeWidthValid1";
            this.gcStopeWidthValid.Name = "gcStopeWidthValid";
            this.gcStopeWidthValid.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gcStopeWidthValid.Visible = true;
            this.gcStopeWidthValid.VisibleIndex = 4;
            // 
            // gcDrillRigValid
            // 
            this.gcDrillRigValid.Caption = "Drill Rig Valid";
            this.gcDrillRigValid.ColumnEdit = this.imageIndication;
            this.gcDrillRigValid.FieldName = "DrillRigValid1";
            this.gcDrillRigValid.Name = "gcDrillRigValid";
            this.gcDrillRigValid.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gcDrillRigValid.Visible = true;
            this.gcDrillRigValid.VisibleIndex = 5;
            // 
            // gcNumberOfEndsValid
            // 
            this.gcNumberOfEndsValid.Caption = "Number of Ends Valid";
            this.gcNumberOfEndsValid.ColumnEdit = this.imageIndication;
            this.gcNumberOfEndsValid.FieldName = "NumberOfEndsValid1";
            this.gcNumberOfEndsValid.Name = "gcNumberOfEndsValid";
            this.gcNumberOfEndsValid.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.gcNumberOfEndsValid.Visible = true;
            this.gcNumberOfEndsValid.VisibleIndex = 6;
            // 
            // gcPlanDAY
            // 
            this.gcPlanDAY.Caption = "PlanDay";
            this.gcPlanDAY.FieldName = "PlanDay";
            this.gcPlanDAY.Name = "gcPlanDAY";
            this.gcPlanDAY.OptionsColumn.AllowEdit = false;
            this.gcPlanDAY.OptionsColumn.ReadOnly = true;
            this.gcPlanDAY.Visible = true;
            this.gcPlanDAY.VisibleIndex = 7;
            // 
            // gcPlanNIGHT
            // 
            this.gcPlanNIGHT.Caption = "PlanNight";
            this.gcPlanNIGHT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcPlanNIGHT.FieldName = "PlanNight";
            this.gcPlanNIGHT.Name = "gcPlanNIGHT";
            this.gcPlanNIGHT.OptionsColumn.AllowEdit = false;
            this.gcPlanNIGHT.OptionsColumn.ReadOnly = true;
            this.gcPlanNIGHT.Visible = true;
            this.gcPlanNIGHT.VisibleIndex = 8;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.PictureChecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemCheckEdit1.PictureChecked")));
            this.repositoryItemCheckEdit1.PictureUnchecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemCheckEdit1.PictureUnchecked")));
            // 
            // imagesStatus
            // 
            this.imagesStatus.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imagesStatus.ImageStream")));
            this.imagesStatus.InsertImage(global::Mineware.Systems.Planning.Properties.Resources.delete, "delete", typeof(global::Mineware.Systems.Planning.Properties.Resources), 0);
            this.imagesStatus.Images.SetKeyName(0, "delete");
            this.imagesStatus.InsertImage(global::Mineware.Systems.Planning.Properties.Resources.accept, "accept", typeof(global::Mineware.Systems.Planning.Properties.Resources), 1);
            this.imagesStatus.Images.SetKeyName(1, "accept");
            this.imagesStatus.InsertImage(global::Mineware.Systems.Planning.Properties.Resources.error, "error", typeof(global::Mineware.Systems.Planning.Properties.Resources), 2);
            this.imagesStatus.Images.SetKeyName(2, "error");
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPL;
            this.xtraTabControl1.Size = new System.Drawing.Size(890, 598);
            this.xtraTabControl1.TabIndex = 4;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPL,
            this.tabAL});
            // 
            // tabPL
            // 
            this.tabPL.Controls.Add(this.gridControl1);
            this.tabPL.Name = "tabPL";
            this.tabPL.Size = new System.Drawing.Size(884, 570);
            this.tabPL.Text = "Production Month Labour";
            // 
            // tabAL
            // 
            this.tabAL.Controls.Add(this.gridControl2);
            this.tabAL.Name = "tabAL";
            this.tabAL.Size = new System.Drawing.Size(884, 570);
            this.tabAL.Text = "Additional Labour";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gvUnplannedLabour;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spinEditPlannedValue});
            this.gridControl2.Size = new System.Drawing.Size(884, 570);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUnplannedLabour});
            // 
            // gvUnplannedLabour
            // 
            this.gvUnplannedLabour.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSection,
            this.gcAtWorkPlan,
            this.gcInServicePlan});
            this.gvUnplannedLabour.GridControl = this.gridControl2;
            this.gvUnplannedLabour.Name = "gvUnplannedLabour";
            this.gvUnplannedLabour.OptionsView.ShowFooter = true;
            this.gvUnplannedLabour.OptionsView.ShowGroupPanel = false;
            // 
            // gcSection
            // 
            this.gcSection.Caption = "Section";
            this.gcSection.FieldName = "Name_2";
            this.gcSection.Name = "gcSection";
            this.gcSection.OptionsColumn.AllowEdit = false;
            this.gcSection.OptionsColumn.ReadOnly = true;
            this.gcSection.Visible = true;
            this.gcSection.VisibleIndex = 0;
            this.gcSection.Width = 277;
            // 
            // gcAtWorkPlan
            // 
            this.gcAtWorkPlan.Caption = "At Work Plan";
            this.gcAtWorkPlan.FieldName = "AtWorkPlan";
            this.gcAtWorkPlan.Name = "gcAtWorkPlan";
            this.gcAtWorkPlan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gcAtWorkPlan.Visible = true;
            this.gcAtWorkPlan.VisibleIndex = 1;
            this.gcAtWorkPlan.Width = 83;
            // 
            // gcInServicePlan
            // 
            this.gcInServicePlan.Caption = "In Service Plan";
            this.gcInServicePlan.FieldName = "InServicePlan";
            this.gcInServicePlan.Name = "gcInServicePlan";
            this.gcInServicePlan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gcInServicePlan.Visible = true;
            this.gcInServicePlan.VisibleIndex = 2;
            this.gcInServicePlan.Width = 86;
            // 
            // spinEditPlannedValue
            // 
            this.spinEditPlannedValue.AutoHeight = false;
            this.spinEditPlannedValue.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditPlannedValue.Name = "spinEditPlannedValue";
            // 
            // ucHRPlanning
            // 
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "ucHRPlanning";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(890, 598);
            this.Controls.SetChildIndex(this.xtraTabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvHRPlann)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHRPlanningBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsHRPlanning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkplaceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageIndication)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagesStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPL.ResumeLayout(false);
            this.tabAL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnplannedLabour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditPlannedValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource dsHRPlanningBindingSource;
        private Mineware.Systems.Planning.PlanningProtocolCapture.dsHRPlanning dsHRPlanning;
        private DevExpress.Utils.ImageCollection imagesStatus;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPL;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gvHRPlann;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDesignation;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colOrgUnitDay;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colOrgUnitNight;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStdDay;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStdNight;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPlanDay;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPlanNigth;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPlanDayLast;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPlanNigthLast;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWorkplaceList;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkplaceDesc;
        private DevExpress.XtraGrid.Columns.GridColumn gcValidMiningMethod;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit imageIndication;
        private DevExpress.XtraGrid.Columns.GridColumn gcNightCrewValid;
        private DevExpress.XtraGrid.Columns.GridColumn gcPanelLengthValid;
        private DevExpress.XtraGrid.Columns.GridColumn gcStopeWidthValid;
        private DevExpress.XtraGrid.Columns.GridColumn gcDrillRigValid;
        private DevExpress.XtraGrid.Columns.GridColumn gcNumberOfEndsValid;
        private DevExpress.XtraGrid.Columns.GridColumn gcPlanDAY;
        private DevExpress.XtraGrid.Columns.GridColumn gcPlanNIGHT;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraTab.XtraTabPage tabAL;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUnplannedLabour;
        private DevExpress.XtraGrid.Columns.GridColumn gcSection;
        private DevExpress.XtraGrid.Columns.GridColumn gcAtWorkPlan;
        private DevExpress.XtraGrid.Columns.GridColumn gcInServicePlan;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinEditPlannedValue;


    }
}
