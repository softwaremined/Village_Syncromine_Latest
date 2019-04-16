namespace Mineware.Systems.Production.Controls.Survey
{
    partial class ucWorkplaceAdd
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
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.lblDouble = new DevExpress.XtraEditors.LabelControl();
            this.lbWorkplace = new DevExpress.XtraEditors.LabelControl();
            this.lbWorkplaceID = new DevExpress.XtraEditors.LabelControl();
            this.lblWorkplace = new DevExpress.XtraEditors.LabelControl();
            this.lblWorkplaceID = new DevExpress.XtraEditors.LabelControl();
            this.gcWorkplace = new DevExpress.XtraGrid.GridControl();
            this.gvWorkplace = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_WorkplaceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Workplace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_RW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Dens = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_cmgt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_SW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_EH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_EW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnWPSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnWPClose = new DevExpress.XtraBars.BarButtonItem();
            this.rpButtons = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgButtons = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkplace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkplace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(154, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Double click to select Workplace";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 95);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.lblDouble);
            this.splitContainerControl1.Panel1.Controls.Add(this.lbWorkplace);
            this.splitContainerControl1.Panel1.Controls.Add(this.lbWorkplaceID);
            this.splitContainerControl1.Panel1.Controls.Add(this.lblWorkplace);
            this.splitContainerControl1.Panel1.Controls.Add(this.lblWorkplaceID);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcWorkplace);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(479, 548);
            this.splitContainerControl1.SplitterPosition = 51;
            this.splitContainerControl1.TabIndex = 23;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // lblDouble
            // 
            this.lblDouble.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblDouble.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblDouble.Appearance.Options.UseFont = true;
            this.lblDouble.Appearance.Options.UseForeColor = true;
            this.lblDouble.Location = new System.Drawing.Point(173, 37);
            this.lblDouble.Name = "lblDouble";
            this.lblDouble.Size = new System.Drawing.Size(153, 13);
            this.lblDouble.TabIndex = 4;
            this.lblDouble.Text = "Double click to Select Workplace";
            // 
            // lbWorkplace
            // 
            this.lbWorkplace.Location = new System.Drawing.Point(258, 6);
            this.lbWorkplace.Name = "lbWorkplace";
            this.lbWorkplace.Size = new System.Drawing.Size(0, 13);
            this.lbWorkplace.TabIndex = 3;
            // 
            // lbWorkplaceID
            // 
            this.lbWorkplaceID.Location = new System.Drawing.Point(85, 6);
            this.lbWorkplaceID.Name = "lbWorkplaceID";
            this.lbWorkplaceID.Size = new System.Drawing.Size(0, 13);
            this.lbWorkplaceID.TabIndex = 2;
            // 
            // lblWorkplace
            // 
            this.lblWorkplace.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblWorkplace.Appearance.Options.UseFont = true;
            this.lblWorkplace.Location = new System.Drawing.Point(188, 6);
            this.lblWorkplace.Name = "lblWorkplace";
            this.lblWorkplace.Size = new System.Drawing.Size(64, 13);
            this.lblWorkplace.TabIndex = 1;
            this.lblWorkplace.Text = "Description";
            // 
            // lblWorkplaceID
            // 
            this.lblWorkplaceID.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblWorkplaceID.Appearance.Options.UseFont = true;
            this.lblWorkplaceID.Location = new System.Drawing.Point(45, 6);
            this.lblWorkplaceID.Name = "lblWorkplaceID";
            this.lblWorkplaceID.Size = new System.Drawing.Size(34, 13);
            this.lblWorkplaceID.TabIndex = 0;
            this.lblWorkplaceID.Text = "WP ID";
            // 
            // gcWorkplace
            // 
            this.gcWorkplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWorkplace.Location = new System.Drawing.Point(0, 0);
            this.gcWorkplace.MainView = this.gvWorkplace;
            this.gcWorkplace.Name = "gcWorkplace";
            this.gcWorkplace.Size = new System.Drawing.Size(479, 492);
            this.gcWorkplace.TabIndex = 24;
            this.gcWorkplace.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWorkplace});
            // 
            // gvWorkplace
            // 
            this.gvWorkplace.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_WorkplaceID,
            this.col_Workplace,
            this.col_RW,
            this.col_Dens,
            this.col_cmgt,
            this.col_SW,
            this.col_CW,
            this.col_EH,
            this.col_EW});
            this.gvWorkplace.GridControl = this.gcWorkplace;
            this.gvWorkplace.Name = "gvWorkplace";
            this.gvWorkplace.OptionsBehavior.ReadOnly = true;
            this.gvWorkplace.OptionsCustomization.AllowColumnMoving = false;
            this.gvWorkplace.OptionsCustomization.AllowColumnResizing = false;
            this.gvWorkplace.OptionsCustomization.AllowFilter = false;
            this.gvWorkplace.OptionsView.ShowAutoFilterRow = true;
            this.gvWorkplace.OptionsView.ShowGroupPanel = false;
            this.gvWorkplace.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvWorkplace_RowCellClick);
            this.gvWorkplace.DoubleClick += new System.EventHandler(this.gvWorkplace_DoubleClick);
            // 
            // col_WorkplaceID
            // 
            this.col_WorkplaceID.AppearanceCell.Options.UseTextOptions = true;
            this.col_WorkplaceID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_WorkplaceID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.col_WorkplaceID.AppearanceHeader.Options.UseFont = true;
            this.col_WorkplaceID.AppearanceHeader.Options.UseTextOptions = true;
            this.col_WorkplaceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_WorkplaceID.Caption = "Workplace ID";
            this.col_WorkplaceID.FieldName = "WorkplaceID";
            this.col_WorkplaceID.Name = "col_WorkplaceID";
            this.col_WorkplaceID.OptionsColumn.AllowEdit = false;
            this.col_WorkplaceID.OptionsColumn.AllowFocus = false;
            this.col_WorkplaceID.OptionsColumn.AllowMove = false;
            this.col_WorkplaceID.OptionsColumn.ReadOnly = true;
            this.col_WorkplaceID.OptionsColumn.TabStop = false;
            this.col_WorkplaceID.Visible = true;
            this.col_WorkplaceID.VisibleIndex = 0;
            // 
            // col_Workplace
            // 
            this.col_Workplace.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.col_Workplace.AppearanceHeader.Options.UseFont = true;
            this.col_Workplace.Caption = "Description";
            this.col_Workplace.FieldName = "Workplace";
            this.col_Workplace.Name = "col_Workplace";
            this.col_Workplace.OptionsColumn.AllowEdit = false;
            this.col_Workplace.OptionsColumn.AllowFocus = false;
            this.col_Workplace.OptionsColumn.AllowMove = false;
            this.col_Workplace.OptionsColumn.ReadOnly = true;
            this.col_Workplace.OptionsColumn.TabStop = false;
            this.col_Workplace.Visible = true;
            this.col_Workplace.VisibleIndex = 1;
            // 
            // col_RW
            // 
            this.col_RW.Caption = "RW";
            this.col_RW.FieldName = "RW";
            this.col_RW.Name = "col_RW";
            this.col_RW.OptionsColumn.AllowEdit = false;
            this.col_RW.OptionsColumn.AllowFocus = false;
            this.col_RW.OptionsColumn.AllowMove = false;
            this.col_RW.OptionsColumn.ReadOnly = true;
            this.col_RW.OptionsColumn.TabStop = false;
            // 
            // col_Dens
            // 
            this.col_Dens.Caption = "Dens";
            this.col_Dens.FieldName = "Dens";
            this.col_Dens.Name = "col_Dens";
            this.col_Dens.OptionsColumn.AllowEdit = false;
            this.col_Dens.OptionsColumn.AllowFocus = false;
            this.col_Dens.OptionsColumn.AllowMove = false;
            this.col_Dens.OptionsColumn.ReadOnly = true;
            this.col_Dens.OptionsColumn.TabStop = false;
            // 
            // col_cmgt
            // 
            this.col_cmgt.Caption = "cmgt";
            this.col_cmgt.FieldName = "cmgt";
            this.col_cmgt.Name = "col_cmgt";
            this.col_cmgt.OptionsColumn.AllowEdit = false;
            this.col_cmgt.OptionsColumn.AllowFocus = false;
            this.col_cmgt.OptionsColumn.AllowMove = false;
            this.col_cmgt.OptionsColumn.ReadOnly = true;
            this.col_cmgt.OptionsColumn.TabStop = false;
            // 
            // col_SW
            // 
            this.col_SW.Caption = "SW";
            this.col_SW.FieldName = "SW";
            this.col_SW.Name = "col_SW";
            this.col_SW.OptionsColumn.AllowEdit = false;
            this.col_SW.OptionsColumn.AllowFocus = false;
            this.col_SW.OptionsColumn.AllowMove = false;
            this.col_SW.OptionsColumn.ReadOnly = true;
            this.col_SW.OptionsColumn.TabStop = false;
            // 
            // col_CW
            // 
            this.col_CW.Caption = "CW";
            this.col_CW.FieldName = "CW";
            this.col_CW.Name = "col_CW";
            this.col_CW.OptionsColumn.AllowEdit = false;
            this.col_CW.OptionsColumn.AllowFocus = false;
            this.col_CW.OptionsColumn.AllowMove = false;
            this.col_CW.OptionsColumn.ReadOnly = true;
            this.col_CW.OptionsColumn.TabStop = false;
            // 
            // col_EH
            // 
            this.col_EH.Caption = "EH";
            this.col_EH.FieldName = "EH";
            this.col_EH.Name = "col_EH";
            this.col_EH.OptionsColumn.AllowEdit = false;
            this.col_EH.OptionsColumn.AllowFocus = false;
            this.col_EH.OptionsColumn.AllowMove = false;
            this.col_EH.OptionsColumn.ReadOnly = true;
            this.col_EH.OptionsColumn.TabStop = false;
            // 
            // col_EW
            // 
            this.col_EW.Caption = "EW";
            this.col_EW.FieldName = "EW";
            this.col_EW.Name = "col_EW";
            this.col_EW.OptionsColumn.AllowEdit = false;
            this.col_EW.OptionsColumn.AllowFocus = false;
            this.col_EW.OptionsColumn.AllowMove = false;
            this.col_EW.OptionsColumn.ReadOnly = true;
            this.col_EW.OptionsColumn.TabStop = false;
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
            this.btnWPSave,
            this.btnWPClose});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 3;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpButtons});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(479, 95);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl1.Click += new System.EventHandler(this.ribbonControl1_Click);
            // 
            // btnWPSave
            // 
            this.btnWPSave.Caption = "Save";
            this.btnWPSave.Id = 1;
            this.btnWPSave.ImageOptions.ImageUri.Uri = "Save";
            this.btnWPSave.Name = "btnWPSave";
            this.btnWPSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnWPSave_ItemClick);
            // 
            // btnWPClose
            // 
            this.btnWPClose.Caption = "Close";
            this.btnWPClose.Id = 2;
            this.btnWPClose.ImageOptions.ImageUri.Uri = "Close";
            this.btnWPClose.Name = "btnWPClose";
            this.btnWPClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnWPClose_ItemClick);
            // 
            // rpButtons
            // 
            this.rpButtons.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgButtons});
            this.rpButtons.Name = "rpButtons";
            // 
            // rpgButtons
            // 
            this.rpgButtons.ItemLinks.Add(this.btnWPSave);
            this.rpgButtons.ItemLinks.Add(this.btnWPClose);
            this.rpgButtons.Name = "rpgButtons";
            // 
            // ucWorkplaceAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 643);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.label3);
            this.Name = "ucWorkplaceAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ucWorkplaceAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkplace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkplace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcWorkplace;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWorkplace;
        private DevExpress.XtraGrid.Columns.GridColumn col_WorkplaceID;
        private DevExpress.XtraGrid.Columns.GridColumn col_Workplace;
        private DevExpress.XtraEditors.LabelControl lblDouble;
        private DevExpress.XtraEditors.LabelControl lblWorkplace;
        private DevExpress.XtraEditors.LabelControl lblWorkplaceID;
        private DevExpress.XtraGrid.Columns.GridColumn col_RW;
        private DevExpress.XtraGrid.Columns.GridColumn col_Dens;
        private DevExpress.XtraGrid.Columns.GridColumn col_cmgt;
        private DevExpress.XtraGrid.Columns.GridColumn col_SW;
        private DevExpress.XtraGrid.Columns.GridColumn col_CW;
        private DevExpress.XtraGrid.Columns.GridColumn col_EH;
        private DevExpress.XtraGrid.Columns.GridColumn col_EW;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnWPSave;
        private DevExpress.XtraBars.BarButtonItem btnWPClose;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpButtons;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgButtons;
        public DevExpress.XtraEditors.LabelControl lbWorkplace;
        public DevExpress.XtraEditors.LabelControl lbWorkplaceID;
    }
}
