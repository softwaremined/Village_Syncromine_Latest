namespace Mineware.Systems.Production.SysAdminScreens.OreBody
{
    partial class ucOreBody
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOreBody));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDelete = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.btnAdd = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.gcOreBody = new DevExpress.XtraGrid.GridControl();
            this.gvOreBody = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCommonArea = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOreBody)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOreBody)).BeginInit();
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
            this.splitContainerControl1.Panel2.Controls.Add(this.gcOreBody);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(871, 352);
            this.splitContainerControl1.SplitterPosition = 58;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnDelete);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(871, 58);
            this.panelControl1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.ImageLeftPadding = 0;
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.Location = new System.Drawing.Point(140, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 58);
            this.btnDelete.TabIndex = 29;
            this.btnDelete.Text = "Delete";
            this.btnDelete.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Delete;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.ImageLeftPadding = 0;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 58);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "Add";
            this.btnAdd.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Add;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.ImageLeftPadding = 0;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.Location = new System.Drawing.Point(70, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 58);
            this.btnEdit.TabIndex = 27;
            this.btnEdit.Text = "Edit";
            this.btnEdit.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Edit;
            this.btnEdit.Click += new System.EventHandler(this.gvOreBody_DoubleClick);
            // 
            // gcOreBody
            // 
            this.gcOreBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOreBody.Location = new System.Drawing.Point(0, 0);
            this.gcOreBody.MainView = this.gvOreBody;
            this.gcOreBody.Name = "gcOreBody";
            this.gcOreBody.Size = new System.Drawing.Size(871, 288);
            this.gcOreBody.TabIndex = 1;
            this.gcOreBody.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOreBody});
            this.gcOreBody.Click += new System.EventHandler(this.gcOreBody_Click);
            // 
            // gvOreBody
            // 
            this.gvOreBody.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colCommonArea});
            this.gvOreBody.GridControl = this.gcOreBody;
            this.gvOreBody.Name = "gvOreBody";
            this.gvOreBody.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvOreBody.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gvOreBody.OptionsView.ShowGroupPanel = false;
            this.gvOreBody.EditFormPrepared += new DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventHandler(this.gvOreBody_EditFormPrepared);
            this.gvOreBody.ShowingPopupEditForm += new DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventHandler(this.gvOreBody_ShowingPopupEditForm);
            this.gvOreBody.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvOreBody_ValidateRow);
            this.gvOreBody.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvOreBody_RowUpdated);
            this.gvOreBody.DoubleClick += new System.EventHandler(this.gvOreBody_DoubleClick);
            this.gvOreBody.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gvOreBody_ValidatingEditor);
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "Id";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 1;
            this.colID.Width = 427;
            // 
            // colCommonArea
            // 
            this.colCommonArea.Caption = "Common Area";
            this.colCommonArea.FieldName = "Name";
            this.colCommonArea.Name = "colCommonArea";
            this.colCommonArea.Visible = true;
            this.colCommonArea.VisibleIndex = 0;
            this.colCommonArea.Width = 426;
            // 
            // ucOreBody
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ucOreBody";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(871, 352);
            this.Load += new System.EventHandler(this.ucOreBody_Load);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOreBody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOreBody)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Global.CustomControls.MWButton btnDelete;
        private Global.CustomControls.MWButton btnAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colCommonArea;
        public DevExpress.XtraGrid.GridControl gcOreBody;
        public DevExpress.XtraGrid.Views.Grid.GridView gvOreBody;
        public Global.CustomControls.MWButton btnEdit;
    }
}
