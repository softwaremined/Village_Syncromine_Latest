namespace Mineware.Systems.Planning.MiningMethodsInPlanningSetup
{
    partial class ucMiningMethodsPlanningSetup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMiningMethodsPlanningSetup));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvActivity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvShowInPlanning = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkaccess = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gvRequireDocuments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkaccessd = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkaccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkaccessd)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(3, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Save";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(84, 12);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "Cancel";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(3, 55);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkaccess,
            this.chkaccessd});
            this.gridControl1.Size = new System.Drawing.Size(643, 473);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvActivity,
            this.gvMethod,
            this.gvShowInPlanning,
            this.gvRequireDocuments});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gvActivity
            // 
            this.gvActivity.Caption = "Activity";
            this.gvActivity.FieldName = "TheActivity";
            this.gvActivity.Name = "gvActivity";
            this.gvActivity.Visible = true;
            this.gvActivity.VisibleIndex = 0;
            // 
            // gvMethod
            // 
            this.gvMethod.Caption = "Method";
            this.gvMethod.FieldName = "Method";
            this.gvMethod.Name = "gvMethod";
            this.gvMethod.Visible = true;
            this.gvMethod.VisibleIndex = 1;
            // 
            // gvShowInPlanning
            // 
            this.gvShowInPlanning.Caption = "Show In Planning";
            this.gvShowInPlanning.ColumnEdit = this.chkaccess;
            this.gvShowInPlanning.FieldName = "ShowInPlanning";
            this.gvShowInPlanning.Name = "gvShowInPlanning";
            this.gvShowInPlanning.Visible = true;
            this.gvShowInPlanning.VisibleIndex = 2;
            // 
            // chkaccess
            // 
            this.chkaccess.AutoHeight = false;
            this.chkaccess.Caption = "Check";
            this.chkaccess.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.chkaccess.ImageIndexChecked = 1;
            this.chkaccess.ImageIndexUnchecked = 0;
            this.chkaccess.Images = this.imageList1;
            this.chkaccess.Name = "chkaccess";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "UpgradeReport_Error.png");
            this.imageList1.Images.SetKeyName(1, "UpgradeReport_Success.png");
            // 
            // gvRequireDocuments
            // 
            this.gvRequireDocuments.Caption = "Required Documents";
            this.gvRequireDocuments.ColumnEdit = this.chkaccessd;
            this.gvRequireDocuments.FieldName = "RequireDocuments";
            this.gvRequireDocuments.Name = "gvRequireDocuments";
            this.gvRequireDocuments.Visible = true;
            this.gvRequireDocuments.VisibleIndex = 3;
            // 
            // chkaccessd
            // 
            this.chkaccessd.AutoHeight = false;
            this.chkaccessd.Caption = "Check";
            this.chkaccessd.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.chkaccessd.ImageIndexChecked = 1;
            this.chkaccessd.ImageIndexUnchecked = 0;
            this.chkaccessd.Images = this.imageList1;
            this.chkaccessd.Name = "chkaccessd";
            // 
            // ucMiningMethodsPlanningSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Name = "ucMiningMethodsPlanningSetup";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(649, 531);
            this.Load += new System.EventHandler(this.ucMiningMethodsPlanningSetup_Load);
            this.Controls.SetChildIndex(this.simpleButton1, 0);
            this.Controls.SetChildIndex(this.simpleButton2, 0);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkaccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkaccessd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gvActivity;
        private DevExpress.XtraGrid.Columns.GridColumn gvMethod;
        private DevExpress.XtraGrid.Columns.GridColumn gvShowInPlanning;
        private DevExpress.XtraGrid.Columns.GridColumn gvRequireDocuments;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkaccess;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkaccessd;
        private System.Windows.Forms.ImageList imageList1;

    }
}