namespace Mineware.Systems.Production.SysAdminScreens.ProcessCodes
{
    partial class ucProcessCodes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProcessCodes));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDelete = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.btnAdd = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.gcProcessCode = new DevExpress.XtraGrid.GridControl();
            this.gvProcessCode = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProcessCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpProcessCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPCID = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProcessCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProcessCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpProcessCode)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcProcessCode);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(647, 318);
            this.splitContainerControl1.SplitterPosition = 41;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(647, 100);
            this.panelControl1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageLeftPadding = 0;
            this.btnDelete.Location = new System.Drawing.Point(140, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 40);
            this.btnDelete.TabIndex = 26;
            this.btnDelete.Text = "Delete";
            this.btnDelete.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Delete;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageLeftPadding = 0;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 40);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "Add";
            this.btnAdd.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Add;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageLeftPadding = 0;
            this.btnEdit.Location = new System.Drawing.Point(70, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 40);
            this.btnEdit.TabIndex = 24;
            this.btnEdit.Text = "Edit";
            this.btnEdit.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Edit;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // gcProcessCode
            // 
            this.gcProcessCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcProcessCode.Location = new System.Drawing.Point(0, 0);
            this.gcProcessCode.MainView = this.gvProcessCode;
            this.gcProcessCode.Name = "gcProcessCode";
            this.gcProcessCode.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpProcessCode});
            this.gcProcessCode.Size = new System.Drawing.Size(647, 272);
            this.gcProcessCode.TabIndex = 0;
            this.gcProcessCode.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProcessCode});
            // 
            // gvProcessCode
            // 
            this.gvProcessCode.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPCID,
            this.colProcessCode});
            this.gvProcessCode.GridControl = this.gcProcessCode;
            this.gvProcessCode.Name = "gvProcessCode";
            this.gvProcessCode.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvProcessCode.OptionsView.ShowGroupPanel = false;
            this.gvProcessCode.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvProcessCode_RowUpdated);
            this.gvProcessCode.DoubleClick += new System.EventHandler(this.gvProcessCode_DoubleClick);
            // 
            // colProcessCode
            // 
            this.colProcessCode.Caption = "Process Code";
            this.colProcessCode.ColumnEdit = this.rpProcessCode;
            this.colProcessCode.FieldName = "Name";
            this.colProcessCode.Name = "colProcessCode";
            this.colProcessCode.Visible = true;
            this.colProcessCode.VisibleIndex = 1;
            this.colProcessCode.Width = 601;
            // 
            // rpProcessCode
            // 
            this.rpProcessCode.AutoHeight = false;
            this.rpProcessCode.MaxLength = 10;
            this.rpProcessCode.Name = "rpProcessCode";
            // 
            // colPCID
            // 
            this.colPCID.Caption = "ID";
            this.colPCID.FieldName = "Id";
            this.colPCID.Name = "colPCID";
            this.colPCID.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.colPCID.Visible = true;
            this.colPCID.VisibleIndex = 0;
            // 
            // ucProcessCodes
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ucProcessCodes";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(647, 318);
            this.Load += new System.EventHandler(this.ucProcessCodes_Load);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcProcessCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProcessCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpProcessCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Global.CustomControls.MWButton btnDelete;
        private Global.CustomControls.MWButton btnAdd;
        private Global.CustomControls.MWButton btnEdit;
        private DevExpress.XtraGrid.GridControl gcProcessCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProcessCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpProcessCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPCID;
    }
}
