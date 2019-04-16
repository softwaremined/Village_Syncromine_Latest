namespace Mineware.Systems.Production.SysAdminScreens.OreBody
{
    partial class ileOreBody
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.gcOreBodyEdit = new DevExpress.XtraGrid.GridControl();
            this.gvOreBodyEdit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpSubSection = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOreBodyEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOreBodyEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSubSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.SetBoundPropertyName(this.panelControl1, "");
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnUpdate);
            this.panelControl1.Controls.Add(this.gcOreBodyEdit);
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(775, 261);
            this.panelControl1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.SetBoundPropertyName(this.btnCancel, "");
            this.btnCancel.Location = new System.Drawing.Point(695, 217);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.Location = new System.Drawing.Point(605, 217);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // gcOreBodyEdit
            // 
            this.SetBoundPropertyName(this.gcOreBodyEdit, "");
            this.gcOreBodyEdit.Location = new System.Drawing.Point(0, 59);
            this.gcOreBodyEdit.MainView = this.gvOreBodyEdit;
            this.gcOreBodyEdit.Name = "gcOreBodyEdit";
            this.gcOreBodyEdit.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpSubSection});
            this.gcOreBodyEdit.Size = new System.Drawing.Size(772, 152);
            this.gcOreBodyEdit.TabIndex = 2;
            this.gcOreBodyEdit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOreBodyEdit});
            // 
            // gvOreBodyEdit
            // 
            this.gvOreBodyEdit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colSubSection});
            this.gvOreBodyEdit.GridControl = this.gcOreBodyEdit;
            this.gvOreBodyEdit.Name = "gvOreBodyEdit";
            this.gvOreBodyEdit.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvOreBodyEdit.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.FieldName = "Id";
            this.colID.MinWidth = 10;
            this.colID.Name = "colID";
            this.colID.OptionsColumn.AllowEdit = false;
            this.colID.OptionsColumn.ReadOnly = true;
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 10;
            // 
            // colSubSection
            // 
            this.colSubSection.Caption = "Sub Section";
            this.colSubSection.ColumnEdit = this.rpSubSection;
            this.colSubSection.FieldName = "SubSection";
            this.colSubSection.Name = "colSubSection";
            this.colSubSection.Visible = true;
            this.colSubSection.VisibleIndex = 1;
            this.colSubSection.Width = 744;
            // 
            // rpSubSection
            // 
            this.rpSubSection.AutoHeight = false;
            this.rpSubSection.Name = "rpSubSection";
            // 
            // txtName
            // 
            this.SetBoundFieldName(this.txtName, "Name");
            this.SetBoundPropertyName(this.txtName, "EditValue");
            this.txtName.Location = new System.Drawing.Point(122, 23);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 1;
            this.txtName.EditValueChanged += new System.EventHandler(this.textEdit1_EditValueChanged);
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // labelControl1
            // 
            this.SetBoundPropertyName(this.labelControl1, "");
            this.labelControl1.Location = new System.Drawing.Point(24, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(67, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Common Area";
            // 
            // ileOreBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ileOreBody";
            this.Size = new System.Drawing.Size(775, 261);
            this.Leave += new System.EventHandler(this.ileOreBody_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOreBodyEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOreBodyEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSubSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpSubSection;
        public DevExpress.XtraGrid.Columns.GridColumn colSubSection;
        public DevExpress.XtraGrid.GridControl gcOreBodyEdit;
        public DevExpress.XtraGrid.Views.Grid.GridView gvOreBodyEdit;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
    }
}