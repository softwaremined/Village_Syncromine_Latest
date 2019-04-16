namespace Mineware.Systems.Production.SysAdminScreens.Workplace_Codes
{
    partial class ileGridEdit
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
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.gcGridEdit = new DevExpress.XtraGrid.GridControl();
            this.gvGridEdit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGRIDEDITCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGRIDEDITDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGRIDEDITSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rpGRIDEDITSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.txtCostarea = new DevExpress.XtraEditors.TextEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txtGrid = new DevExpress.XtraEditors.TextEdit();
            this.lblCostArea = new DevExpress.XtraEditors.LabelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblGrid = new DevExpress.XtraEditors.LabelControl();
            this.lblDivision = new DevExpress.XtraEditors.LabelControl();
            this.lkpDivision = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGridEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpGRIDEDITSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostarea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpDivision.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.SetBoundPropertyName(this.panelControl1, "");
            this.panelControl1.Controls.Add(this.btnUpdate);
            this.panelControl1.Controls.Add(this.gcGridEdit);
            this.panelControl1.Controls.Add(this.txtCostarea);
            this.panelControl1.Controls.Add(this.txtDescription);
            this.panelControl1.Controls.Add(this.txtGrid);
            this.panelControl1.Controls.Add(this.lblCostArea);
            this.panelControl1.Controls.Add(this.lblDescription);
            this.panelControl1.Controls.Add(this.lblGrid);
            this.panelControl1.Controls.Add(this.lblDivision);
            this.panelControl1.Controls.Add(this.lkpDivision);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(502, 328);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.Location = new System.Drawing.Point(207, 295);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 19;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // gcGridEdit
            // 
            this.SetBoundPropertyName(this.gcGridEdit, "");
            this.gcGridEdit.Location = new System.Drawing.Point(2, 142);
            this.gcGridEdit.MainView = this.gvGridEdit;
            this.gcGridEdit.Name = "gcGridEdit";
            this.gcGridEdit.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpGRIDEDITSelected});
            this.gcGridEdit.Size = new System.Drawing.Size(498, 147);
            this.gcGridEdit.TabIndex = 18;
            this.gcGridEdit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGridEdit});
            // 
            // gvGridEdit
            // 
            this.gvGridEdit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGRIDEDITCode,
            this.colGRIDEDITDescription,
            this.colGRIDEDITSelected});
            this.gvGridEdit.GridControl = this.gcGridEdit;
            this.gvGridEdit.Name = "gvGridEdit";
            this.gvGridEdit.OptionsView.ShowGroupPanel = false;
            // 
            // colGRIDEDITCode
            // 
            this.colGRIDEDITCode.Caption = "Code";
            this.colGRIDEDITCode.FieldName = "Code";
            this.colGRIDEDITCode.Name = "colGRIDEDITCode";
            this.colGRIDEDITCode.OptionsColumn.AllowEdit = false;
            this.colGRIDEDITCode.OptionsColumn.ReadOnly = true;
            this.colGRIDEDITCode.Visible = true;
            this.colGRIDEDITCode.VisibleIndex = 0;
            // 
            // colGRIDEDITDescription
            // 
            this.colGRIDEDITDescription.Caption = "Description";
            this.colGRIDEDITDescription.FieldName = "Description";
            this.colGRIDEDITDescription.Name = "colGRIDEDITDescription";
            this.colGRIDEDITDescription.OptionsColumn.AllowEdit = false;
            this.colGRIDEDITDescription.OptionsColumn.ReadOnly = true;
            this.colGRIDEDITDescription.Visible = true;
            this.colGRIDEDITDescription.VisibleIndex = 1;
            // 
            // colGRIDEDITSelected
            // 
            this.colGRIDEDITSelected.Caption = "Selected";
            this.colGRIDEDITSelected.ColumnEdit = this.rpGRIDEDITSelected;
            this.colGRIDEDITSelected.FieldName = "Selected";
            this.colGRIDEDITSelected.Name = "colGRIDEDITSelected";
            this.colGRIDEDITSelected.Visible = true;
            this.colGRIDEDITSelected.VisibleIndex = 2;
            // 
            // rpGRIDEDITSelected
            // 
            this.rpGRIDEDITSelected.AutoHeight = false;
            this.rpGRIDEDITSelected.Name = "rpGRIDEDITSelected";
            // 
            // txtCostarea
            // 
            this.SetBoundPropertyName(this.txtCostarea, "");
            this.txtCostarea.Location = new System.Drawing.Point(175, 106);
            this.txtCostarea.Name = "txtCostarea";
            this.txtCostarea.Properties.MaxLength = 1;
            this.txtCostarea.Size = new System.Drawing.Size(192, 20);
            this.txtCostarea.TabIndex = 17;
            // 
            // txtDescription
            // 
            this.SetBoundPropertyName(this.txtDescription, "");
            this.txtDescription.Location = new System.Drawing.Point(175, 71);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(192, 20);
            this.txtDescription.TabIndex = 16;
            // 
            // txtGrid
            // 
            this.SetBoundPropertyName(this.txtGrid, "");
            this.txtGrid.Location = new System.Drawing.Point(175, 39);
            this.txtGrid.Name = "txtGrid";
            this.txtGrid.Size = new System.Drawing.Size(192, 20);
            this.txtGrid.TabIndex = 15;
            // 
            // lblCostArea
            // 
            this.SetBoundPropertyName(this.lblCostArea, "");
            this.lblCostArea.Location = new System.Drawing.Point(36, 109);
            this.lblCostArea.Name = "lblCostArea";
            this.lblCostArea.Size = new System.Drawing.Size(48, 13);
            this.lblCostArea.TabIndex = 14;
            this.lblCostArea.Text = "Cost Area";
            // 
            // lblDescription
            // 
            this.SetBoundPropertyName(this.lblDescription, "");
            this.lblDescription.Location = new System.Drawing.Point(36, 74);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(53, 13);
            this.lblDescription.TabIndex = 13;
            this.lblDescription.Text = "Description";
            // 
            // lblGrid
            // 
            this.SetBoundPropertyName(this.lblGrid, "");
            this.lblGrid.Location = new System.Drawing.Point(36, 42);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(19, 13);
            this.lblGrid.TabIndex = 12;
            this.lblGrid.Text = "Grid";
            // 
            // lblDivision
            // 
            this.SetBoundPropertyName(this.lblDivision, "");
            this.lblDivision.Location = new System.Drawing.Point(36, 12);
            this.lblDivision.Name = "lblDivision";
            this.lblDivision.Size = new System.Drawing.Size(36, 13);
            this.lblDivision.TabIndex = 11;
            this.lblDivision.Text = "Division";
            // 
            // lkpDivision
            // 
            this.SetBoundFieldName(this.lkpDivision, "Division");
            this.SetBoundPropertyName(this.lkpDivision, "EditValue");
            this.lkpDivision.Location = new System.Drawing.Point(175, 5);
            this.lkpDivision.Name = "lkpDivision";
            this.lkpDivision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpDivision.Properties.NullText = "";
            this.lkpDivision.Size = new System.Drawing.Size(192, 20);
            this.lkpDivision.TabIndex = 10;
            // 
            // ileGridEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ileGridEdit";
            this.Size = new System.Drawing.Size(502, 328);
            this.Load += new System.EventHandler(this.frmGridEdit_Load);
            this.Leave += new System.EventHandler(this.ileGridEdit_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGridEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGridEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpGRIDEDITSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCostarea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpDivision.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraGrid.GridControl gcGridEdit;
        public DevExpress.XtraGrid.Views.Grid.GridView gvGridEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colGRIDEDITCode;
        private DevExpress.XtraGrid.Columns.GridColumn colGRIDEDITDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colGRIDEDITSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rpGRIDEDITSelected;
        public DevExpress.XtraEditors.TextEdit txtCostarea;
        public DevExpress.XtraEditors.TextEdit txtDescription;
        public DevExpress.XtraEditors.TextEdit txtGrid;
        private DevExpress.XtraEditors.LabelControl lblCostArea;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblGrid;
        private DevExpress.XtraEditors.LabelControl lblDivision;
        public DevExpress.XtraEditors.LookUpEdit lkpDivision;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
    }
}