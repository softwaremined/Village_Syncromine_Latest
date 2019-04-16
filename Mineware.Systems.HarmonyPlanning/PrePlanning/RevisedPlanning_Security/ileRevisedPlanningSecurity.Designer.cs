namespace Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security
{
    partial class ileRevisedPlanningSecurity
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkApprovalRequired = new DevExpress.XtraEditors.CheckEdit();
            this.editUser = new DevExpress.XtraEditors.LookUpEdit();
            this.editSecurity = new DevExpress.XtraEditors.LookUpEdit();
            this.editSection = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkApprovalRequired.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSecurity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.SetBoundPropertyName(this.layoutControl1, "");
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.chkApprovalRequired);
            this.layoutControl1.Controls.Add(this.editUser);
            this.layoutControl1.Controls.Add(this.editSecurity);
            this.layoutControl1.Controls.Add(this.editSection);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(617, 135);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl1
            // 
            this.SetBoundPropertyName(this.labelControl1, "");
            this.labelControl1.Location = new System.Drawing.Point(12, 84);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(593, 13);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 9;
            // 
            // chkApprovalRequired
            // 
            this.SetBoundFieldName(this.chkApprovalRequired, "ApprovalRequired");
            this.SetBoundPropertyName(this.chkApprovalRequired, "EditValue");
            this.chkApprovalRequired.Location = new System.Drawing.Point(12, 101);
            this.chkApprovalRequired.Name = "chkApprovalRequired";
            this.chkApprovalRequired.Properties.Caption = "Approval Required";
            this.chkApprovalRequired.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkApprovalRequired.Size = new System.Drawing.Size(593, 19);
            this.chkApprovalRequired.StyleController = this.layoutControl1;
            this.chkApprovalRequired.TabIndex = 8;
            // 
            // editUser
            // 
            this.SetBoundFieldName(this.editUser, "UserID");
            this.SetBoundPropertyName(this.editUser, "EditValue");
            this.editUser.Location = new System.Drawing.Point(81, 36);
            this.editUser.Name = "editUser";
            this.editUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editUser.Size = new System.Drawing.Size(524, 20);
            this.editUser.StyleController = this.layoutControl1;
            this.editUser.TabIndex = 6;
            this.editUser.EditValueChanged += new System.EventHandler(this.editUser_EditValueChanged);
            // 
            // editSecurity
            // 
            this.SetBoundFieldName(this.editSecurity, "SecurityType");
            this.SetBoundPropertyName(this.editSecurity, "EditValue");
            this.editSecurity.Location = new System.Drawing.Point(81, 60);
            this.editSecurity.Name = "editSecurity";
            this.editSecurity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editSecurity.Properties.DisplayMember = "Description";
            this.editSecurity.Properties.ValueMember = "SecurityType";
            this.editSecurity.Size = new System.Drawing.Size(524, 20);
            this.editSecurity.StyleController = this.layoutControl1;
            this.editSecurity.TabIndex = 4;
            this.editSecurity.EditValueChanged += new System.EventHandler(this.editSecurity_EditValueChanged);
            // 
            // editSection
            // 
            this.SetBoundFieldName(this.editSection, "ID");
            this.SetBoundPropertyName(this.editSection, "EditValue");
            this.editSection.Location = new System.Drawing.Point(81, 12);
            this.editSection.Name = "editSection";
            this.editSection.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editSection.Properties.DisplayMember = "Name";
            this.editSection.Properties.NullText = "";
            this.editSection.Properties.TreeList = this.treeListLookUpEdit1TreeList;
            this.editSection.Properties.ValueMember = "ID";
            this.editSection.Size = new System.Drawing.Size(524, 20);
            this.editSection.StyleController = this.layoutControl1;
            this.editSection.TabIndex = 5;
            // 
            // treeListLookUpEdit1TreeList
            // 
            this.SetBoundPropertyName(this.treeListLookUpEdit1TreeList, "");
            this.treeListLookUpEdit1TreeList.Location = new System.Drawing.Point(0, 0);
            this.treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            this.treeListLookUpEdit1TreeList.OptionsBehavior.EnableFiltering = true;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            this.treeListLookUpEdit1TreeList.Size = new System.Drawing.Size(400, 200);
            this.treeListLookUpEdit1TreeList.TabIndex = 0;
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem4});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(617, 135);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.editSecurity;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(597, 24);
            this.layoutControlItem1.Text = "Security Type";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(66, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.editSection;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(597, 24);
            this.layoutControlItem2.Text = "Section";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(66, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.editUser;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(597, 24);
            this.layoutControlItem3.Text = "User";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(66, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkApprovalRequired;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 89);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(597, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.labelControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(597, 17);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // ileRevisedPlanningSecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ileRevisedPlanningSecurity";
            this.Size = new System.Drawing.Size(617, 135);
            this.Load += new System.EventHandler(this.ileRevisedPlanningSecurity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkApprovalRequired.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSecurity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LookUpEdit editUser;
        private DevExpress.XtraEditors.LookUpEdit editSecurity;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TreeListLookUpEdit editSection;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        public DevExpress.XtraEditors.CheckEdit chkApprovalRequired;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;

    }
}
