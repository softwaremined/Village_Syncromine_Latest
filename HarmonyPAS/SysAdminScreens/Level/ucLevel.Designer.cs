namespace Mineware.Systems.Production.SysAdminScreens.Level
{
    partial class ucLevel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLevel));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDelete = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnAdd = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton();
            this.gcLevel = new DevExpress.XtraGrid.GridControl();
            this.gvLevel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOreflowID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLevelName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colParentOreflowID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLevelNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHopperFactor = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLevel)).BeginInit();
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
            this.splitContainerControl1.Panel2.Controls.Add(this.gcLevel);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(813, 359);
            this.splitContainerControl1.SplitterPosition = 61;
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
            this.panelControl1.Size = new System.Drawing.Size(813, 61);
            this.panelControl1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageLeftPadding = 0;
            this.btnDelete.Location = new System.Drawing.Point(140, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 58);
            this.btnDelete.TabIndex = 32;
            this.btnDelete.Text = "Delete";
            this.btnDelete.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Delete;
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageLeftPadding = 0;
            this.btnAdd.Location = new System.Drawing.Point(0, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 58);
            this.btnAdd.TabIndex = 31;
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
            this.btnEdit.Location = new System.Drawing.Point(70, 2);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 58);
            this.btnEdit.TabIndex = 30;
            this.btnEdit.Text = "Edit";
            this.btnEdit.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Edit;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // gcLevel
            // 
            this.gcLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLevel.Location = new System.Drawing.Point(0, 0);
            this.gcLevel.MainView = this.gvLevel;
            this.gcLevel.Name = "gcLevel";
            this.gcLevel.Size = new System.Drawing.Size(813, 293);
            this.gcLevel.TabIndex = 0;
            this.gcLevel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLevel});
            // 
            // gvLevel
            // 
            this.gvLevel.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDIV,
            this.colOreflowID,
            this.colLevelName,
            this.colParentOreflowID,
            this.colLevelNo,
            this.colHopperFactor});
            this.gvLevel.GridControl = this.gcLevel;
            this.gvLevel.Name = "gvLevel";
            this.gvLevel.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvLevel.OptionsView.ShowGroupPanel = false;
            this.gvLevel.EditFormPrepared += new DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventHandler(this.gvLevel_EditFormPrepared);
            this.gvLevel.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvLevel_InitNewRow);
            this.gvLevel.DoubleClick += new System.EventHandler(this.gvLevel_DoubleClick);
            // 
            // colDIV
            // 
            this.colDIV.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDIV.AppearanceHeader.Options.UseFont = true;
            this.colDIV.Caption = "Division";
            this.colDIV.FieldName = "Division";
            this.colDIV.Name = "colDIV";
            this.colDIV.Visible = true;
            this.colDIV.VisibleIndex = 0;
            // 
            // colOreflowID
            // 
            this.colOreflowID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colOreflowID.AppearanceHeader.Options.UseFont = true;
            this.colOreflowID.Caption = "Oreflow ID";
            this.colOreflowID.FieldName = "OreFlowID";
            this.colOreflowID.Name = "colOreflowID";
            this.colOreflowID.Visible = true;
            this.colOreflowID.VisibleIndex = 1;
            // 
            // colLevelName
            // 
            this.colLevelName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLevelName.AppearanceHeader.Options.UseFont = true;
            this.colLevelName.Caption = "Level Name";
            this.colLevelName.FieldName = "Descp";
            this.colLevelName.Name = "colLevelName";
            this.colLevelName.Visible = true;
            this.colLevelName.VisibleIndex = 2;
            // 
            // colParentOreflowID
            // 
            this.colParentOreflowID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colParentOreflowID.AppearanceHeader.Options.UseFont = true;
            this.colParentOreflowID.Caption = "Parent Oreflow ID";
            this.colParentOreflowID.FieldName = "lvl";
            this.colParentOreflowID.Name = "colParentOreflowID";
            this.colParentOreflowID.Visible = true;
            this.colParentOreflowID.VisibleIndex = 3;
            // 
            // colLevelNo
            // 
            this.colLevelNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colLevelNo.AppearanceHeader.Options.UseFont = true;
            this.colLevelNo.Caption = "Level Number";
            this.colLevelNo.FieldName = "LevelNumber";
            this.colLevelNo.Name = "colLevelNo";
            this.colLevelNo.Visible = true;
            this.colLevelNo.VisibleIndex = 4;
            // 
            // colHopperFactor
            // 
            this.colHopperFactor.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colHopperFactor.AppearanceHeader.Options.UseFont = true;
            this.colHopperFactor.Caption = "Hopper Factor";
            this.colHopperFactor.FieldName = "HopperFactor";
            this.colHopperFactor.Name = "colHopperFactor";
            this.colHopperFactor.Visible = true;
            this.colHopperFactor.VisibleIndex = 5;
            // 
            // ucLevel
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ucLevel";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(813, 359);
            this.Load += new System.EventHandler(this.ucLevel_Load);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcLevel;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLevel;
        private DevExpress.XtraGrid.Columns.GridColumn colDIV;
        private DevExpress.XtraGrid.Columns.GridColumn colOreflowID;
        private DevExpress.XtraGrid.Columns.GridColumn colLevelName;
        private DevExpress.XtraGrid.Columns.GridColumn colParentOreflowID;
        private DevExpress.XtraGrid.Columns.GridColumn colLevelNo;
        private DevExpress.XtraGrid.Columns.GridColumn colHopperFactor;
        private Global.CustomControls.MWButton btnDelete;
        private Global.CustomControls.MWButton btnAdd;
        public Global.CustomControls.MWButton btnEdit;
    }
}
