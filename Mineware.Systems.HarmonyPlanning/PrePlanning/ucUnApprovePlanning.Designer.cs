namespace Mineware.Systems.Planning
{
    partial class ucUnApprovePlanning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucUnApprovePlanning));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.imageCollection2 = new DevExpress.Utils.ImageCollection();
            this.editReason = new DevExpress.XtraEditors.MemoEdit();
            this.gcWorkplaceList = new DevExpress.XtraGrid.GridControl();
            this.gvWorkplace = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.riSelection = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcWorkplace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkplaceList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkplace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.editReason);
            this.layoutControl1.Controls.Add(this.gcWorkplaceList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(797, 505);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.checkEdit1);
            this.panelControl1.Location = new System.Drawing.Point(12, 90);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(773, 41);
            this.panelControl1.TabIndex = 6;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkEdit1.Location = new System.Drawing.Point(670, 11);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Select All";
            this.checkEdit1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.checkEdit1.Properties.ImageIndexChecked = 1;
            this.checkEdit1.Properties.ImageIndexUnchecked = 0;
            this.checkEdit1.Properties.Images = this.imageCollection2;
            this.checkEdit1.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.checkEdit1.Size = new System.Drawing.Size(75, 20);
            this.checkEdit1.TabIndex = 0;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // imageCollection2
            // 
            this.imageCollection2.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection2.ImageStream")));
            this.imageCollection2.Images.SetKeyName(0, "notApprovedTick.png");
            this.imageCollection2.Images.SetKeyName(1, "approveTick.png");
            // 
            // editReason
            // 
            this.editReason.Location = new System.Drawing.Point(84, 12);
            this.editReason.Name = "editReason";
            this.editReason.Size = new System.Drawing.Size(701, 74);
            this.editReason.StyleController = this.layoutControl1;
            this.editReason.TabIndex = 5;
            // 
            // gcWorkplaceList
            // 
            this.gcWorkplaceList.Location = new System.Drawing.Point(12, 151);
            this.gcWorkplaceList.MainView = this.gvWorkplace;
            this.gcWorkplaceList.Name = "gcWorkplaceList";
            this.gcWorkplaceList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riSelection});
            this.gcWorkplaceList.Size = new System.Drawing.Size(773, 342);
            this.gcWorkplaceList.TabIndex = 4;
            this.gcWorkplaceList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWorkplace});
            // 
            // gvWorkplace
            // 
            this.gvWorkplace.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSelected,
            this.gcWorkplace});
            this.gvWorkplace.GridControl = this.gcWorkplaceList;
            this.gvWorkplace.Name = "gvWorkplace";
            // 
            // gcSelected
            // 
            this.gcSelected.Caption = "Selection";
            this.gcSelected.ColumnEdit = this.riSelection;
            this.gcSelected.FieldName = "Selected";
            this.gcSelected.Name = "gcSelected";
            this.gcSelected.Visible = true;
            this.gcSelected.VisibleIndex = 0;
            this.gcSelected.Width = 121;
            // 
            // riSelection
            // 
            this.riSelection.AutoHeight = false;
            this.riSelection.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.riSelection.ImageIndexChecked = 1;
            this.riSelection.ImageIndexUnchecked = 0;
            this.riSelection.Images = this.imageCollection2;
            this.riSelection.Name = "riSelection";
            this.riSelection.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // gcWorkplace
            // 
            this.gcWorkplace.Caption = "Workplace";
            this.gcWorkplace.FieldName = "WorkplaceDesc";
            this.gcWorkplace.Name = "gcWorkplace";
            this.gcWorkplace.Visible = true;
            this.gcWorkplace.VisibleIndex = 1;
            this.gcWorkplace.Width = 634;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(797, 505);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcWorkplaceList;
            this.layoutControlItem1.CustomizationFormText = "Workplace List";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 123);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(777, 362);
            this.layoutControlItem1.Text = "Workplace List";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(69, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.editReason;
            this.layoutControlItem2.CustomizationFormText = "Reason";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(777, 78);
            this.layoutControlItem2.Text = "Reason";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(69, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(777, 45);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // ucUnApprovePlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucUnApprovePlanning";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(797, 505);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkplaceList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorkplace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.riSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.MemoEdit editReason;
        private DevExpress.XtraGrid.GridControl gcWorkplaceList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvWorkplace;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.Utils.ImageCollection imageCollection2;
        private DevExpress.XtraGrid.Columns.GridColumn gcSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riSelection;
        private DevExpress.XtraGrid.Columns.GridColumn gcWorkplace;

    }
}
