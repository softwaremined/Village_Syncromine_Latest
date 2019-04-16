namespace Mineware.Systems.ProductionGlobal
{
    partial class frmWorkplaceSelect
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcWorkPlaces = new DevExpress.XtraGrid.GridControl();
            this.viewWorkplaces = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ceSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcolWPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkPlaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewWorkplaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcWorkPlaces);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(387, 281);
            this.panelControl1.TabIndex = 1;
            // 
            // gcWorkPlaces
            // 
            this.gcWorkPlaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWorkPlaces.Location = new System.Drawing.Point(2, 2);
            this.gcWorkPlaces.MainView = this.viewWorkplaces;
            this.gcWorkPlaces.Name = "gcWorkPlaces";
            this.gcWorkPlaces.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ceSelected});
            this.gcWorkPlaces.Size = new System.Drawing.Size(383, 277);
            this.gcWorkPlaces.TabIndex = 2;
            this.gcWorkPlaces.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewWorkplaces});
            // 
            // viewWorkplaces
            // 
            this.viewWorkplaces.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolSelected,
            this.gcolWPID,
            this.gcolDESCRIPTION});
            this.viewWorkplaces.GridControl = this.gcWorkPlaces;
            this.viewWorkplaces.Name = "viewWorkplaces";
            this.viewWorkplaces.OptionsView.ColumnAutoWidth = false;
            this.viewWorkplaces.OptionsView.ShowAutoFilterRow = true;
            this.viewWorkplaces.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewWorkplaces.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewWorkplaces.OptionsView.ShowGroupPanel = false;
            // 
            // gcolSelected
            // 
            this.gcolSelected.Caption = "Selection";
            this.gcolSelected.ColumnEdit = this.ceSelected;
            this.gcolSelected.FieldName = "Selected";
            this.gcolSelected.Name = "gcolSelected";
            this.gcolSelected.Visible = true;
            this.gcolSelected.VisibleIndex = 0;
            this.gcolSelected.Width = 62;
            // 
            // ceSelected
            // 
            this.ceSelected.AutoHeight = false;
            this.ceSelected.Caption = "Check";
            this.ceSelected.Name = "ceSelected";
            // 
            // gcolWPID
            // 
            this.gcolWPID.Caption = "Workplace ID";
            this.gcolWPID.FieldName = "Workplaceid";
            this.gcolWPID.Name = "gcolWPID";
            this.gcolWPID.OptionsColumn.AllowEdit = false;
            this.gcolWPID.Visible = true;
            this.gcolWPID.VisibleIndex = 1;
            this.gcolWPID.Width = 100;
            // 
            // gcolDESCRIPTION
            // 
            this.gcolDESCRIPTION.Caption = "Description";
            this.gcolDESCRIPTION.FieldName = "DESCRIPTION";
            this.gcolDESCRIPTION.Name = "gcolDESCRIPTION";
            this.gcolDESCRIPTION.OptionsColumn.AllowEdit = false;
            this.gcolDESCRIPTION.Visible = true;
            this.gcolDESCRIPTION.VisibleIndex = 2;
            this.gcolDESCRIPTION.Width = 198;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 281);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(387, 33);
            this.panelControl2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(300, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(219, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmWorkplaceSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 314);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWorkplaceSelect";
            this.ShowInTaskbar = false;
            this.Text = "Select Workplaces";
            this.Load += new System.EventHandler(this.frmWorkplaceSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkPlaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewWorkplaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl gcWorkPlaces;
        private DevExpress.XtraGrid.Views.Grid.GridView viewWorkplaces;
        private DevExpress.XtraGrid.Columns.GridColumn gcolSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ceSelected;
        private DevExpress.XtraGrid.Columns.GridColumn gcolWPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDESCRIPTION;
    }
}