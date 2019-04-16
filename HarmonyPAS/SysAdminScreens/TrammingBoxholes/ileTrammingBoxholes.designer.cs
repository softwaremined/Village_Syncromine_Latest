namespace Mineware.Systems.Production.SysAdminScreens.TrammingBoxholes
{
    partial class ileTrammingBoxholes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ileTrammingBoxholes));
            this.lueInactive = new DevExpress.XtraEditors.LookUpEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.lueSectionID = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.lueReefType = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDistance = new DevExpress.XtraEditors.TextEdit();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblReefType = new System.Windows.Forms.Label();
            this.lueMineOverseer = new DevExpress.XtraEditors.LookUpEdit();
            this.txtBoxholeDescription = new DevExpress.XtraEditors.TextEdit();
            this.txtOreFlowID = new DevExpress.XtraEditors.TextEdit();
            this.lblMineOverseer = new System.Windows.Forms.Label();
            this.lblBoxholeDescription = new System.Windows.Forms.Label();
            this.lblBoxholeID = new System.Windows.Forms.Label();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txtLevelNumber = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.dxBoxholes = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.lueLevelNumber = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInactive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSectionID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueReefType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDistance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMineOverseer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxholeDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOreFlowID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLevelNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxBoxholes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLevelNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lueInactive
            // 
            this.SetBoundPropertyName(this.lueInactive, "");
            this.lueInactive.Location = new System.Drawing.Point(170, 104);
            this.lueInactive.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueInactive.Name = "lueInactive";
            this.lueInactive.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueInactive.Properties.NullText = "N";
            this.lueInactive.Size = new System.Drawing.Size(111, 20);
            this.lueInactive.TabIndex = 91;
            this.lueInactive.Enter += new System.EventHandler(this.lueInactive_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.SetBoundPropertyName(this.label2, "");
            this.label2.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(15, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 90;
            this.label2.Text = "Inactive";
            // 
            // lueSectionID
            // 
            this.SetBoundPropertyName(this.lueSectionID, "");
            this.lueSectionID.Location = new System.Drawing.Point(170, 128);
            this.lueSectionID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueSectionID.Name = "lueSectionID";
            this.lueSectionID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSectionID.Properties.NullText = "Select Section";
            this.lueSectionID.Size = new System.Drawing.Size(178, 20);
            this.lueSectionID.TabIndex = 89;
            this.lueSectionID.Enter += new System.EventHandler(this.lueSectionID_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.SetBoundPropertyName(this.label1, "");
            this.label1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Section";
            // 
            // lueReefType
            // 
            this.SetBoundPropertyName(this.lueReefType, "");
            this.lueReefType.Location = new System.Drawing.Point(170, 80);
            this.lueReefType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueReefType.Name = "lueReefType";
            this.lueReefType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueReefType.Properties.NullText = "Select Reef Type";
            this.lueReefType.Size = new System.Drawing.Size(111, 20);
            this.lueReefType.TabIndex = 87;
            this.lueReefType.Enter += new System.EventHandler(this.lueReefType_Enter);
            // 
            // txtDistance
            // 
            this.SetBoundPropertyName(this.txtDistance, "");
            this.txtDistance.Location = new System.Drawing.Point(170, 152);
            this.txtDistance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(111, 20);
            this.txtDistance.TabIndex = 85;
            this.txtDistance.Leave += new System.EventHandler(this.txtDistance_Leave);
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.SetBoundPropertyName(this.lblDistance, "");
            this.lblDistance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblDistance.Location = new System.Drawing.Point(15, 156);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(56, 13);
            this.lblDistance.TabIndex = 86;
            this.lblDistance.Text = "Distance";
            // 
            // lblReefType
            // 
            this.lblReefType.AutoSize = true;
            this.SetBoundPropertyName(this.lblReefType, "");
            this.lblReefType.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblReefType.Location = new System.Drawing.Point(15, 84);
            this.lblReefType.Name = "lblReefType";
            this.lblReefType.Size = new System.Drawing.Size(64, 13);
            this.lblReefType.TabIndex = 84;
            this.lblReefType.Text = "Reef Type";
            // 
            // lueMineOverseer
            // 
            this.SetBoundPropertyName(this.lueMineOverseer, "");
            this.lueMineOverseer.Location = new System.Drawing.Point(17, 199);
            this.lueMineOverseer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueMineOverseer.Name = "lueMineOverseer";
            this.lueMineOverseer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueMineOverseer.Properties.NullText = "Select Mine Overseer";
            this.lueMineOverseer.Size = new System.Drawing.Size(178, 20);
            this.lueMineOverseer.TabIndex = 77;
            this.lueMineOverseer.Visible = false;
            this.lueMineOverseer.Enter += new System.EventHandler(this.lueMineOverseer_Enter);
            // 
            // txtBoxholeDescription
            // 
            this.SetBoundPropertyName(this.txtBoxholeDescription, "");
            this.txtBoxholeDescription.Location = new System.Drawing.Point(170, 32);
            this.txtBoxholeDescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxholeDescription.Name = "txtBoxholeDescription";
            this.txtBoxholeDescription.Size = new System.Drawing.Size(178, 20);
            this.txtBoxholeDescription.TabIndex = 76;
            // 
            // txtOreFlowID
            // 
            this.SetBoundPropertyName(this.txtOreFlowID, "");
            this.txtOreFlowID.Enabled = false;
            this.txtOreFlowID.Location = new System.Drawing.Point(170, 8);
            this.txtOreFlowID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOreFlowID.Name = "txtOreFlowID";
            this.txtOreFlowID.Size = new System.Drawing.Size(111, 20);
            this.txtOreFlowID.TabIndex = 75;
            // 
            // lblMineOverseer
            // 
            this.lblMineOverseer.AutoSize = true;
            this.SetBoundPropertyName(this.lblMineOverseer, "");
            this.lblMineOverseer.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblMineOverseer.Location = new System.Drawing.Point(15, 184);
            this.lblMineOverseer.Name = "lblMineOverseer";
            this.lblMineOverseer.Size = new System.Drawing.Size(89, 13);
            this.lblMineOverseer.TabIndex = 81;
            this.lblMineOverseer.Text = "Mine Overseer";
            this.lblMineOverseer.Visible = false;
            // 
            // lblBoxholeDescription
            // 
            this.lblBoxholeDescription.AutoSize = true;
            this.SetBoundPropertyName(this.lblBoxholeDescription, "");
            this.lblBoxholeDescription.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblBoxholeDescription.Location = new System.Drawing.Point(15, 36);
            this.lblBoxholeDescription.Name = "lblBoxholeDescription";
            this.lblBoxholeDescription.Size = new System.Drawing.Size(119, 13);
            this.lblBoxholeDescription.TabIndex = 79;
            this.lblBoxholeDescription.Text = "Boxhole Description";
            // 
            // lblBoxholeID
            // 
            this.lblBoxholeID.AutoSize = true;
            this.SetBoundPropertyName(this.lblBoxholeID, "");
            this.lblBoxholeID.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblBoxholeID.Location = new System.Drawing.Point(15, 12);
            this.lblBoxholeID.Name = "lblBoxholeID";
            this.lblBoxholeID.Size = new System.Drawing.Size(68, 13);
            this.lblBoxholeID.TabIndex = 78;
            this.lblBoxholeID.Text = "Boxhole ID";
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(298, 184);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 92;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.Leave += new System.EventHandler(this.btnUpdate_Leave);
            this.btnUpdate.Validated += new System.EventHandler(this.btnUpdate_Validated);
            // 
            // txtLevelNumber
            // 
            this.SetBoundPropertyName(this.txtLevelNumber, "");
            this.txtLevelNumber.Enabled = false;
            this.txtLevelNumber.Location = new System.Drawing.Point(287, 57);
            this.txtLevelNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLevelNumber.Name = "txtLevelNumber";
            this.txtLevelNumber.Size = new System.Drawing.Size(111, 20);
            this.txtLevelNumber.TabIndex = 93;
            this.txtLevelNumber.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.SetBoundPropertyName(this.label3, "");
            this.label3.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(15, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 94;
            this.label3.Text = "Level Number";
            // 
            // dxBoxholes
            // 
            this.dxBoxholes.ContainerControl = this;
            // 
            // lueLevelNumber
            // 
            this.SetBoundPropertyName(this.lueLevelNumber, "");
            this.lueLevelNumber.Location = new System.Drawing.Point(170, 57);
            this.lueLevelNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueLevelNumber.Name = "lueLevelNumber";
            this.lueLevelNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueLevelNumber.Properties.NullText = "Select Level";
            this.lueLevelNumber.Size = new System.Drawing.Size(111, 20);
            this.lueLevelNumber.TabIndex = 95;
            this.lueLevelNumber.EditValueChanged += new System.EventHandler(this.lueLevelNumber_EditValueChanged);
            this.lueLevelNumber.Enter += new System.EventHandler(this.lueLevelNumber_Enter);
            // 
            // ileTrammingBoxholes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lueLevelNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLevelNumber);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lueInactive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lueSectionID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lueReefType);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.lblReefType);
            this.Controls.Add(this.lueMineOverseer);
            this.Controls.Add(this.txtBoxholeDescription);
            this.Controls.Add(this.txtOreFlowID);
            this.Controls.Add(this.lblMineOverseer);
            this.Controls.Add(this.lblBoxholeDescription);
            this.Controls.Add(this.lblBoxholeID);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ileTrammingBoxholes";
            this.Size = new System.Drawing.Size(476, 242);
            this.Load += new System.EventHandler(this.btnUpdate_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.lueInactive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueSectionID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueReefType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDistance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMineOverseer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxholeDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOreFlowID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLevelNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxBoxholes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLevelNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.LookUpEdit lueInactive;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.LookUpEdit lueSectionID;
        private System.Windows.Forms.Label label1;
        public DevExpress.XtraEditors.LookUpEdit lueReefType;
        public DevExpress.XtraEditors.TextEdit txtDistance;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label lblReefType;
        public DevExpress.XtraEditors.LookUpEdit lueMineOverseer;
        public DevExpress.XtraEditors.TextEdit txtBoxholeDescription;
        public DevExpress.XtraEditors.TextEdit txtOreFlowID;
        private System.Windows.Forms.Label lblMineOverseer;
        private System.Windows.Forms.Label lblBoxholeDescription;
        private System.Windows.Forms.Label lblBoxholeID;
        public DevExpress.XtraEditors.SimpleButton btnUpdate;
        private System.Windows.Forms.Label label3;
        public DevExpress.XtraEditors.TextEdit txtLevelNumber;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxBoxholes;
        public DevExpress.XtraEditors.LookUpEdit lueLevelNumber;
    }
}