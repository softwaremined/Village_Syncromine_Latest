namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    partial class frmManageChangeOfPlann
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageChangeOfPlann));
            this.gcRequestList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvRequests = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProdMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWORKPLACEID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWPName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChangeType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.focusControle = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnDecline = new DevExpress.XtraEditors.SimpleButton();
            this.btnApprove = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcRequestList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.focusControle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcRequestList
            // 
            this.gcRequestList.Location = new System.Drawing.Point(12, 90);
            this.gcRequestList.MainView = this.gridView1;
            this.gcRequestList.Name = "gcRequestList";
            this.gcRequestList.Size = new System.Drawing.Size(529, 502);
            this.gcRequestList.TabIndex = 4;
            this.gcRequestList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gvRequests});
            this.gcRequestList.Click += new System.EventHandler(this.gcRequestList_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcRequestList;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            // 
            // gvRequests
            // 
            this.gvRequests.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProdMonth,
            this.gridColumn1,
            this.colWORKPLACEID,
            this.colWPName,
            this.colRequestedBy,
            this.colChangeType});
            this.gvRequests.GridControl = this.gcRequestList;
            this.gvRequests.GroupCount = 1;
            this.gvRequests.Name = "gvRequests";
            this.gvRequests.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvRequests.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvRequests.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvRequests.OptionsBehavior.Editable = false;
            this.gvRequests.OptionsCustomization.AllowColumnResizing = false;
            this.gvRequests.OptionsDetail.ShowDetailTabs = false;
            this.gvRequests.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False;
            this.gvRequests.OptionsView.ColumnAutoWidth = false;
            this.gvRequests.OptionsView.ShowAutoFilterRow = true;
            this.gvRequests.OptionsView.ShowGroupPanel = false;
            this.gvRequests.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colChangeType, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvRequests.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvRequests_RowClick);
            // 
            // colProdMonth
            // 
            this.colProdMonth.Caption = "Production Month";
            this.colProdMonth.FieldName = "ProdMonth";
            this.colProdMonth.Name = "colProdMonth";
            this.colProdMonth.Visible = true;
            this.colProdMonth.VisibleIndex = 0;
            this.colProdMonth.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Request ID";
            this.gridColumn1.FieldName = "ChangeRequestID1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // colWORKPLACEID
            // 
            this.colWORKPLACEID.Caption = "Workplace ID";
            this.colWORKPLACEID.FieldName = "WORKPLACEID";
            this.colWORKPLACEID.Name = "colWORKPLACEID";
            this.colWORKPLACEID.Visible = true;
            this.colWORKPLACEID.VisibleIndex = 2;
            this.colWORKPLACEID.Width = 100;
            // 
            // colWPName
            // 
            this.colWPName.Caption = "Workplace Name";
            this.colWPName.FieldName = "WPName";
            this.colWPName.Name = "colWPName";
            this.colWPName.Visible = true;
            this.colWPName.VisibleIndex = 3;
            this.colWPName.Width = 130;
            // 
            // colRequestedBy
            // 
            this.colRequestedBy.Caption = "Requested By";
            this.colRequestedBy.FieldName = "RequestedBy";
            this.colRequestedBy.Name = "colRequestedBy";
            this.colRequestedBy.Visible = true;
            this.colRequestedBy.VisibleIndex = 4;
            this.colRequestedBy.Width = 120;
            // 
            // colChangeType
            // 
            this.colChangeType.Caption = "Request Type";
            this.colChangeType.FieldName = "ChangeType";
            this.colChangeType.Name = "colChangeType";
            this.colChangeType.Visible = true;
            this.colChangeType.VisibleIndex = 4;
            this.colChangeType.Width = 80;
            // 
            // focusControle
            // 
            this.focusControle.Location = new System.Drawing.Point(545, 90);
            this.focusControle.Name = "focusControle";
            this.focusControle.Size = new System.Drawing.Size(482, 502);
            this.focusControle.TabIndex = 5;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnDecline);
            this.panelControl2.Controls.Add(this.btnApprove);
            this.panelControl2.Location = new System.Drawing.Point(12, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1015, 58);
            this.panelControl2.TabIndex = 6;
            // 
            // btnDecline
            // 
            this.btnDecline.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDecline.ImageOptions.Image")));
            this.btnDecline.Location = new System.Drawing.Point(5, 5);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(135, 49);
            this.btnDecline.TabIndex = 2;
            this.btnDecline.Text = "Decline Request";
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnApprove.ImageOptions.Image")));
            this.btnApprove.Location = new System.Drawing.Point(146, 5);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(135, 49);
            this.btnApprove.TabIndex = 1;
            this.btnApprove.Text = "Approve Request";
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(907, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 34);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1039, 646);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AllowHide = false;
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.gcRequestList;
            this.layoutControlItem1.CustomizationFormText = "Request List";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 62);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(533, 522);
            this.layoutControlItem1.Text = "Request List";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(70, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.focusControle;
            this.layoutControlItem2.CustomizationFormText = "Detail";
            this.layoutControlItem2.Location = new System.Drawing.Point(533, 62);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(486, 522);
            this.layoutControlItem2.Text = "Detail";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(70, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl2;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1019, 62);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.panelControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 584);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1019, 42);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Location = new System.Drawing.Point(12, 596);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1015, 38);
            this.panelControl1.TabIndex = 7;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.focusControle);
            this.layoutControl1.Controls.Add(this.gcRequestList);
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1039, 646);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // frmManageChangeOfPlann
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 646);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmManageChangeOfPlann";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "-";
            this.Load += new System.EventHandler(this.frmManageChangeOfPlann_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcRequestList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.focusControle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcRequestList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRequests;
        private DevExpress.XtraGrid.Columns.GridColumn colProdMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colWORKPLACEID;
        private DevExpress.XtraGrid.Columns.GridColumn colWPName;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colChangeType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl focusControle;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDecline;
        private DevExpress.XtraEditors.SimpleButton btnApprove;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;

    }
}