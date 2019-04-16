namespace Mineware.Systems.Production.SysAdminScreens.SectionScreen
{
    partial class frmUpdateSection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateSection));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbEmployee2 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtHierarchicalID = new DevExpress.XtraEditors.TextEdit();
            this.lblReportTo = new DevExpress.XtraEditors.LabelControl();
            this.lblHierarchicalID = new DevExpress.XtraEditors.LabelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblSectionID = new DevExpress.XtraEditors.LabelControl();
            this.lblProdMonth = new DevExpress.XtraEditors.LabelControl();
            this.cmbReportTo = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbHierarchicalID = new DevExpress.XtraEditors.LookUpEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtSectionID = new DevExpress.XtraEditors.TextEdit();
            this.txtProdMonth = new DevExpress.XtraEditors.TextEdit();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txtOccupation = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.cmbHierarchicalID2 = new System.Windows.Forms.ComboBox();
            this.cmbReportTo2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEmployee2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHierarchicalID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHierarchicalID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOccupation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 66);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 13);
            this.labelControl1.TabIndex = 50;
            this.labelControl1.Text = "Name";
            // 
            // cmbEmployee2
            // 
            this.cmbEmployee2.Location = new System.Drawing.Point(469, 63);
            this.cmbEmployee2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbEmployee2.Name = "cmbEmployee2";
            this.cmbEmployee2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEmployee2.Properties.PopupView = this.searchLookUpEdit1View;
            this.cmbEmployee2.Size = new System.Drawing.Size(166, 20);
            this.cmbEmployee2.TabIndex = 49;
            this.cmbEmployee2.Visible = false;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtHierarchicalID
            // 
            this.txtHierarchicalID.Location = new System.Drawing.Point(469, 97);
            this.txtHierarchicalID.Name = "txtHierarchicalID";
            this.txtHierarchicalID.Size = new System.Drawing.Size(100, 20);
            this.txtHierarchicalID.TabIndex = 48;
            this.txtHierarchicalID.Visible = false;
            // 
            // lblReportTo
            // 
            this.lblReportTo.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportTo.Appearance.Options.UseFont = true;
            this.lblReportTo.Location = new System.Drawing.Point(12, 118);
            this.lblReportTo.Name = "lblReportTo";
            this.lblReportTo.Size = new System.Drawing.Size(56, 13);
            this.lblReportTo.TabIndex = 46;
            this.lblReportTo.Text = "Report To";
            // 
            // lblHierarchicalID
            // 
            this.lblHierarchicalID.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHierarchicalID.Appearance.Options.UseFont = true;
            this.lblHierarchicalID.Location = new System.Drawing.Point(12, 92);
            this.lblHierarchicalID.Name = "lblHierarchicalID";
            this.lblHierarchicalID.Size = new System.Drawing.Size(83, 13);
            this.lblHierarchicalID.TabIndex = 45;
            this.lblHierarchicalID.Text = "Hierarchical ID";
            // 
            // lblName
            // 
            this.lblName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Appearance.Options.UseFont = true;
            this.lblName.Location = new System.Drawing.Point(12, 186);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(96, 13);
            this.lblName.TabIndex = 44;
            this.lblName.Text = "Industry Number";
            // 
            // lblSectionID
            // 
            this.lblSectionID.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSectionID.Appearance.Options.UseFont = true;
            this.lblSectionID.Location = new System.Drawing.Point(12, 40);
            this.lblSectionID.Name = "lblSectionID";
            this.lblSectionID.Size = new System.Drawing.Size(58, 13);
            this.lblSectionID.TabIndex = 43;
            this.lblSectionID.Text = "Section ID";
            // 
            // lblProdMonth
            // 
            this.lblProdMonth.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdMonth.Appearance.Options.UseFont = true;
            this.lblProdMonth.Location = new System.Drawing.Point(12, 15);
            this.lblProdMonth.Name = "lblProdMonth";
            this.lblProdMonth.Size = new System.Drawing.Size(65, 13);
            this.lblProdMonth.TabIndex = 42;
            this.lblProdMonth.Text = "Prod Month";
            // 
            // cmbReportTo
            // 
            this.cmbReportTo.Location = new System.Drawing.Point(379, 106);
            this.cmbReportTo.Name = "cmbReportTo";
            this.cmbReportTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReportTo.Size = new System.Drawing.Size(166, 20);
            this.cmbReportTo.TabIndex = 41;
            this.cmbReportTo.Visible = false;
            // 
            // cmbHierarchicalID
            // 
            this.cmbHierarchicalID.Location = new System.Drawing.Point(379, 80);
            this.cmbHierarchicalID.Name = "cmbHierarchicalID";
            this.cmbHierarchicalID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHierarchicalID.Size = new System.Drawing.Size(166, 20);
            this.cmbHierarchicalID.TabIndex = 40;
            this.cmbHierarchicalID.Visible = false;
            this.cmbHierarchicalID.EditValueChanged += new System.EventHandler(this.cmbHierarchicalID_EditValueChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(114, 64);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(166, 20);
            this.txtName.TabIndex = 39;
            // 
            // txtSectionID
            // 
            this.txtSectionID.Location = new System.Drawing.Point(114, 38);
            this.txtSectionID.Name = "txtSectionID";
            this.txtSectionID.Size = new System.Drawing.Size(166, 20);
            this.txtSectionID.TabIndex = 38;
            // 
            // txtProdMonth
            // 
            this.txtProdMonth.Enabled = false;
            this.txtProdMonth.Location = new System.Drawing.Point(114, 12);
            this.txtProdMonth.Name = "txtProdMonth";
            this.txtProdMonth.Size = new System.Drawing.Size(100, 20);
            this.txtProdMonth.TabIndex = 37;
            // 
            // btnUpdate
            // 
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(179, 232);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 36;
            this.btnUpdate.Text = "Save";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtOccupation
            // 
            this.txtOccupation.Location = new System.Drawing.Point(469, 38);
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(100, 20);
            this.txtOccupation.TabIndex = 47;
            this.txtOccupation.Visible = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(260, 232);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 24);
            this.simpleButton1.TabIndex = 51;
            this.simpleButton1.Text = "Cancel";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(114, 183);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(221, 21);
            this.cmbEmployee.TabIndex = 52;
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(114, 161);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Size = new System.Drawing.Size(166, 20);
            this.searchControl1.TabIndex = 53;
            this.searchControl1.SelectedIndexChanged += new System.EventHandler(this.searchControl1_SelectedIndexChanged);
            this.searchControl1.TextChanged += new System.EventHandler(this.searchControl1_TextChanged);
            this.searchControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchControl1_KeyDown);
            // 
            // cmbHierarchicalID2
            // 
            this.cmbHierarchicalID2.FormattingEnabled = true;
            this.cmbHierarchicalID2.Location = new System.Drawing.Point(114, 89);
            this.cmbHierarchicalID2.Name = "cmbHierarchicalID2";
            this.cmbHierarchicalID2.Size = new System.Drawing.Size(221, 21);
            this.cmbHierarchicalID2.TabIndex = 54;
            this.cmbHierarchicalID2.SelectedIndexChanged += new System.EventHandler(this.cmbHierarchicalID2_SelectedIndexChanged);
            // 
            // cmbReportTo2
            // 
            this.cmbReportTo2.FormattingEnabled = true;
            this.cmbReportTo2.Location = new System.Drawing.Point(114, 115);
            this.cmbReportTo2.Name = "cmbReportTo2";
            this.cmbReportTo2.Size = new System.Drawing.Size(221, 21);
            this.cmbReportTo2.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(111, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Search Name <Enter>";
            // 
            // frmUpdateSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 265);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbReportTo2);
            this.Controls.Add(this.cmbHierarchicalID2);
            this.Controls.Add(this.searchControl1);
            this.Controls.Add(this.cmbEmployee);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbEmployee2);
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
            this.Name = "frmUpdateSection";
            this.ShowIcon = false;
            this.Text = "Update Section";
            this.Load += new System.EventHandler(this.frmUpdateSection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbEmployee2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHierarchicalID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReportTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHierarchicalID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOccupation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbEmployee2;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        public DevExpress.XtraEditors.TextEdit txtHierarchicalID;
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
        public DevExpress.XtraEditors.SimpleButton btnUpdate;
        public DevExpress.XtraEditors.TextEdit txtOccupation;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private System.Windows.Forms.ComboBox cmbHierarchicalID2;
        private System.Windows.Forms.ComboBox cmbReportTo2;
        private System.Windows.Forms.Label label1;
    }
}