namespace Mineware.Systems.Production.SysAdminScreens.SectionScreen
{
    partial class ileSectionScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ileSectionScreen));
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txtProdMonth = new DevExpress.XtraEditors.TextEdit();
            this.txtSectionID = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.cmbHierarchicalID = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbReportTo = new DevExpress.XtraEditors.LookUpEdit();
            this.lblProdMonth = new DevExpress.XtraEditors.LabelControl();
            this.lblSectionID = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblHierarchicalID = new DevExpress.XtraEditors.LabelControl();
            this.lblReportTo = new DevExpress.XtraEditors.LabelControl();
            this.txtOccupation = new DevExpress.XtraEditors.TextEdit();
            this.txtHierarchicalID = new DevExpress.XtraEditors.TextEdit();
            this.dxSections = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHierarchicalID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOccupation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHierarchicalID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEmployee.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(222, 139);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Save";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtProdMonth
            // 
            this.SetBoundPropertyName(this.txtProdMonth, "");
            this.txtProdMonth.Enabled = false;
            this.txtProdMonth.Location = new System.Drawing.Point(443, 50);
            this.txtProdMonth.Name = "txtProdMonth";
            this.txtProdMonth.Size = new System.Drawing.Size(100, 20);
            this.txtProdMonth.TabIndex = 22;
            this.txtProdMonth.Visible = false;
            // 
            // txtSectionID
            // 
            this.SetBoundPropertyName(this.txtSectionID, "");
            this.txtSectionID.Location = new System.Drawing.Point(131, 11);
            this.txtSectionID.Name = "txtSectionID";
            this.txtSectionID.Size = new System.Drawing.Size(166, 20);
            this.txtSectionID.TabIndex = 23;
            this.txtSectionID.Leave += new System.EventHandler(this.txtSectionID_Leave);
            // 
            // txtName
            // 
            this.SetBoundPropertyName(this.txtName, "");
            this.txtName.Location = new System.Drawing.Point(131, 61);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(166, 20);
            this.txtName.TabIndex = 24;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // cmbHierarchicalID
            // 
            this.SetBoundPropertyName(this.cmbHierarchicalID, "");
            this.cmbHierarchicalID.Location = new System.Drawing.Point(131, 87);
            this.cmbHierarchicalID.Name = "cmbHierarchicalID";
            this.cmbHierarchicalID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHierarchicalID.Size = new System.Drawing.Size(166, 20);
            this.cmbHierarchicalID.TabIndex = 25;
            this.cmbHierarchicalID.EditValueChanged += new System.EventHandler(this.cmbHierarchicalID_EditValueChanged);
            this.cmbHierarchicalID.Enter += new System.EventHandler(this.cmbHierarchicalID_Enter);
            // 
            // cmbReportTo
            // 
            this.SetBoundPropertyName(this.cmbReportTo, "");
            this.cmbReportTo.Location = new System.Drawing.Point(131, 113);
            this.cmbReportTo.Name = "cmbReportTo";
            this.cmbReportTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReportTo.Size = new System.Drawing.Size(166, 20);
            this.cmbReportTo.TabIndex = 26;
            this.cmbReportTo.Enter += new System.EventHandler(this.cmbReportTo_Enter);
            // 
            // lblProdMonth
            // 
            this.lblProdMonth.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdMonth.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblProdMonth, "");
            this.lblProdMonth.Location = new System.Drawing.Point(354, 53);
            this.lblProdMonth.Name = "lblProdMonth";
            this.lblProdMonth.Size = new System.Drawing.Size(65, 13);
            this.lblProdMonth.TabIndex = 27;
            this.lblProdMonth.Text = "Prod Month";
            this.lblProdMonth.Visible = false;
            // 
            // lblSectionID
            // 
            this.lblSectionID.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSectionID.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblSectionID, "");
            this.lblSectionID.Location = new System.Drawing.Point(29, 13);
            this.lblSectionID.Name = "lblSectionID";
            this.lblSectionID.Size = new System.Drawing.Size(58, 13);
            this.lblSectionID.TabIndex = 28;
            this.lblSectionID.Text = "Section ID";
            // 
            // lblName
            // 
            this.lblName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblName, "");
            this.lblName.Location = new System.Drawing.Point(29, 39);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(96, 13);
            this.lblName.TabIndex = 29;
            this.lblName.Text = "Industry Number";
            // 
            // lblHierarchicalID
            // 
            this.lblHierarchicalID.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHierarchicalID.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblHierarchicalID, "");
            this.lblHierarchicalID.Location = new System.Drawing.Point(29, 89);
            this.lblHierarchicalID.Name = "lblHierarchicalID";
            this.lblHierarchicalID.Size = new System.Drawing.Size(83, 13);
            this.lblHierarchicalID.TabIndex = 30;
            this.lblHierarchicalID.Text = "Hierarchical ID";
            // 
            // lblReportTo
            // 
            this.lblReportTo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportTo.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblReportTo, "");
            this.lblReportTo.Location = new System.Drawing.Point(29, 115);
            this.lblReportTo.Name = "lblReportTo";
            this.lblReportTo.Size = new System.Drawing.Size(56, 13);
            this.lblReportTo.TabIndex = 31;
            this.lblReportTo.Text = "Report To";
            // 
            // txtOccupation
            // 
            this.SetBoundPropertyName(this.txtOccupation, "");
            this.txtOccupation.Location = new System.Drawing.Point(435, 24);
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(100, 20);
            this.txtOccupation.TabIndex = 32;
            this.txtOccupation.Visible = false;
            // 
            // txtHierarchicalID
            // 
            this.SetBoundPropertyName(this.txtHierarchicalID, "");
            this.txtHierarchicalID.Location = new System.Drawing.Point(435, 103);
            this.txtHierarchicalID.Name = "txtHierarchicalID";
            this.txtHierarchicalID.Size = new System.Drawing.Size(100, 20);
            this.txtHierarchicalID.TabIndex = 33;
            this.txtHierarchicalID.Visible = false;
            // 
            // dxSections
            // 
            this.dxSections.ContainerControl = this;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // cmbEmployee
            // 
            this.SetBoundPropertyName(this.cmbEmployee, "");
            this.cmbEmployee.Location = new System.Drawing.Point(131, 36);
            this.cmbEmployee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEmployee.Properties.PopupView = this.searchLookUpEdit1View;
            this.cmbEmployee.Size = new System.Drawing.Size(166, 20);
            this.cmbEmployee.TabIndex = 34;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.labelControl1, "");
            this.labelControl1.Location = new System.Drawing.Point(29, 63);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 13);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "Name";
            // 
            // ileSectionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbEmployee);
            this.Controls.Add(this.txtHierarchicalID);
            this.Controls.Add(this.lblReportTo);
            this.Controls.Add(this.lblHierarchicalID);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblSectionID);
            this.Controls.Add(this.lblProdMonth);
            this.Controls.Add(this.cmbReportTo);
            this.Controls.Add(this.cmbHierarchicalID);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtSectionID);
            this.Controls.Add(this.txtProdMonth);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtOccupation);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ileSectionScreen";
            this.Size = new System.Drawing.Size(560, 176);
            this.Load += new System.EventHandler(this.ileSectionScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHierarchicalID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOccupation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHierarchicalID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEmployee.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.LabelControl lblReportTo;
        private DevExpress.XtraEditors.LabelControl lblHierarchicalID;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblSectionID;
        private DevExpress.XtraEditors.LabelControl lblProdMonth;
        public DevExpress.XtraEditors.LookUpEdit cmbReportTo;
        public DevExpress.XtraEditors.LookUpEdit cmbHierarchicalID;
        public DevExpress.XtraEditors.TextEdit txtName;
        public DevExpress.XtraEditors.TextEdit txtSectionID;
        public DevExpress.XtraEditors.TextEdit txtProdMonth;
        public DevExpress.XtraEditors.TextEdit txtOccupation;
        public DevExpress.XtraEditors.TextEdit txtHierarchicalID;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxSections;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbEmployee;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
