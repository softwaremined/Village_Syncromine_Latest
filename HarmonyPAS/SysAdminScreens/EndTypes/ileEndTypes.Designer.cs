namespace Mineware.Systems.Production.SysAdminScreens.EndTypes
{
    partial class ileEndTypes
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.rdReefWaste = new DevExpress.XtraEditors.RadioGroup();
            this.txtDetRatio = new DevExpress.XtraEditors.TextEdit();
            this.lblDetRatio = new DevExpress.XtraEditors.LabelControl();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.lblRate = new DevExpress.XtraEditors.LabelControl();
            this.txtEndWidth = new DevExpress.XtraEditors.TextEdit();
            this.lblEndWidth = new DevExpress.XtraEditors.LabelControl();
            this.txtEndHeight = new DevExpress.XtraEditors.TextEdit();
            this.lblEndHeight = new DevExpress.XtraEditors.LabelControl();
            this.txtProcessCode = new DevExpress.XtraEditors.TextEdit();
            this.lblProcessCode = new DevExpress.XtraEditors.LabelControl();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.txtEndTypeID = new DevExpress.XtraEditors.TextEdit();
            this.lblEndTypeId = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdReefWaste.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetRatio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndTypeID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.SetBoundPropertyName(this.panelControl1, "");
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.rdReefWaste);
            this.panelControl1.Controls.Add(this.txtDetRatio);
            this.panelControl1.Controls.Add(this.lblDetRatio);
            this.panelControl1.Controls.Add(this.txtRate);
            this.panelControl1.Controls.Add(this.lblRate);
            this.panelControl1.Controls.Add(this.txtEndWidth);
            this.panelControl1.Controls.Add(this.lblEndWidth);
            this.panelControl1.Controls.Add(this.txtEndHeight);
            this.panelControl1.Controls.Add(this.lblEndHeight);
            this.panelControl1.Controls.Add(this.txtProcessCode);
            this.panelControl1.Controls.Add(this.lblProcessCode);
            this.panelControl1.Controls.Add(this.txtDescription);
            this.panelControl1.Controls.Add(this.lblDescription);
            this.panelControl1.Controls.Add(this.txtEndTypeID);
            this.panelControl1.Controls.Add(this.lblEndTypeId);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(383, 267);
            this.panelControl1.TabIndex = 0;
            // 
            // rdReefWaste
            // 
            this.SetBoundFieldName(this.rdReefWaste, "ReefWaste");
            this.SetBoundPropertyName(this.rdReefWaste, "EditValue");
            this.rdReefWaste.Location = new System.Drawing.Point(38, 217);
            this.rdReefWaste.Name = "rdReefWaste";
            this.rdReefWaste.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("R", "Reef"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("W", "Waste")});
            this.rdReefWaste.Size = new System.Drawing.Size(240, 33);
            this.rdReefWaste.TabIndex = 18;
            // 
            // txtDetRatio
            // 
            this.SetBoundFieldName(this.txtDetRatio, "DetRatio");
            this.SetBoundPropertyName(this.txtDetRatio, "EditValue");
            this.txtDetRatio.Location = new System.Drawing.Point(129, 181);
            this.txtDetRatio.Name = "txtDetRatio";
            this.txtDetRatio.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDetRatio.Size = new System.Drawing.Size(149, 20);
            this.txtDetRatio.TabIndex = 15;
            // 
            // lblDetRatio
            // 
            this.SetBoundPropertyName(this.lblDetRatio, "");
            this.lblDetRatio.Location = new System.Drawing.Point(39, 188);
            this.lblDetRatio.Name = "lblDetRatio";
            this.lblDetRatio.Size = new System.Drawing.Size(45, 13);
            this.lblDetRatio.TabIndex = 14;
            this.lblDetRatio.Text = "Det Ratio";
            // 
            // txtRate
            // 
            this.SetBoundFieldName(this.txtRate, "Rate");
            this.SetBoundPropertyName(this.txtRate, "EditValue");
            this.txtRate.Location = new System.Drawing.Point(129, 155);
            this.txtRate.Name = "txtRate";
            this.txtRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtRate.Size = new System.Drawing.Size(149, 20);
            this.txtRate.TabIndex = 13;
            // 
            // lblRate
            // 
            this.SetBoundPropertyName(this.lblRate, "");
            this.lblRate.Location = new System.Drawing.Point(39, 162);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(23, 13);
            this.lblRate.TabIndex = 12;
            this.lblRate.Text = "Rate";
            // 
            // txtEndWidth
            // 
            this.SetBoundFieldName(this.txtEndWidth, "EndWidth");
            this.SetBoundPropertyName(this.txtEndWidth, "EditValue");
            this.txtEndWidth.Location = new System.Drawing.Point(129, 129);
            this.txtEndWidth.Name = "txtEndWidth";
            this.txtEndWidth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtEndWidth.Size = new System.Drawing.Size(149, 20);
            this.txtEndWidth.TabIndex = 11;
            // 
            // lblEndWidth
            // 
            this.SetBoundPropertyName(this.lblEndWidth, "");
            this.lblEndWidth.Location = new System.Drawing.Point(39, 136);
            this.lblEndWidth.Name = "lblEndWidth";
            this.lblEndWidth.Size = new System.Drawing.Size(49, 13);
            this.lblEndWidth.TabIndex = 10;
            this.lblEndWidth.Text = "End Width";
            // 
            // txtEndHeight
            // 
            this.SetBoundFieldName(this.txtEndHeight, "EndHeight");
            this.SetBoundPropertyName(this.txtEndHeight, "EditValue");
            this.txtEndHeight.Location = new System.Drawing.Point(129, 103);
            this.txtEndHeight.Name = "txtEndHeight";
            this.txtEndHeight.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtEndHeight.Size = new System.Drawing.Size(149, 20);
            this.txtEndHeight.TabIndex = 9;
            // 
            // lblEndHeight
            // 
            this.SetBoundPropertyName(this.lblEndHeight, "");
            this.lblEndHeight.Location = new System.Drawing.Point(39, 106);
            this.lblEndHeight.Name = "lblEndHeight";
            this.lblEndHeight.Size = new System.Drawing.Size(52, 13);
            this.lblEndHeight.TabIndex = 8;
            this.lblEndHeight.Text = "End Height";
            // 
            // txtProcessCode
            // 
            this.SetBoundFieldName(this.txtProcessCode, "ProcessCode");
            this.SetBoundPropertyName(this.txtProcessCode, "EditValue");
            this.txtProcessCode.Location = new System.Drawing.Point(130, 77);
            this.txtProcessCode.Name = "txtProcessCode";
            this.txtProcessCode.Size = new System.Drawing.Size(149, 20);
            this.txtProcessCode.TabIndex = 7;
            // 
            // lblProcessCode
            // 
            this.SetBoundPropertyName(this.lblProcessCode, "");
            this.lblProcessCode.Location = new System.Drawing.Point(39, 80);
            this.lblProcessCode.Name = "lblProcessCode";
            this.lblProcessCode.Size = new System.Drawing.Size(65, 13);
            this.lblProcessCode.TabIndex = 6;
            this.lblProcessCode.Text = "Process Code";
            // 
            // txtDescription
            // 
            this.SetBoundFieldName(this.txtDescription, "Description");
            this.SetBoundPropertyName(this.txtDescription, "EditValue");
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(129, 48);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(149, 20);
            this.txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.SetBoundPropertyName(this.lblDescription, "");
            this.lblDescription.Location = new System.Drawing.Point(38, 48);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(53, 13);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description";
            // 
            // txtEndTypeID
            // 
            this.SetBoundFieldName(this.txtEndTypeID, "EndTypeID");
            this.SetBoundPropertyName(this.txtEndTypeID, "EditValue");
            this.txtEndTypeID.Enabled = false;
            this.txtEndTypeID.Location = new System.Drawing.Point(129, 19);
            this.txtEndTypeID.Name = "txtEndTypeID";
            this.txtEndTypeID.Size = new System.Drawing.Size(149, 20);
            this.txtEndTypeID.TabIndex = 3;
            // 
            // lblEndTypeId
            // 
            this.SetBoundPropertyName(this.lblEndTypeId, "");
            this.lblEndTypeId.Location = new System.Drawing.Point(39, 22);
            this.lblEndTypeId.Name = "lblEndTypeId";
            this.lblEndTypeId.Size = new System.Drawing.Size(59, 13);
            this.lblEndTypeId.TabIndex = 2;
            this.lblEndTypeId.Text = "End Type ID";
            // 
            // labelControl1
            // 
            this.SetBoundPropertyName(this.labelControl1, "");
            this.labelControl1.Location = new System.Drawing.Point(284, 106);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(8, 13);
            this.labelControl1.TabIndex = 19;
            this.labelControl1.Text = "m";
            // 
            // labelControl2
            // 
            this.SetBoundPropertyName(this.labelControl2, "");
            this.labelControl2.Location = new System.Drawing.Point(284, 132);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(8, 13);
            this.labelControl2.TabIndex = 20;
            this.labelControl2.Text = "m";
            // 
            // ileEndTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ileEndTypes";
            this.Size = new System.Drawing.Size(383, 267);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdReefWaste.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetRatio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProcessCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndTypeID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.RadioGroup rdReefWaste;
        private DevExpress.XtraEditors.TextEdit txtDetRatio;
        private DevExpress.XtraEditors.LabelControl lblDetRatio;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private DevExpress.XtraEditors.LabelControl lblRate;
        private DevExpress.XtraEditors.TextEdit txtEndWidth;
        private DevExpress.XtraEditors.LabelControl lblEndWidth;
        private DevExpress.XtraEditors.TextEdit txtEndHeight;
        private DevExpress.XtraEditors.LabelControl lblEndHeight;
        private DevExpress.XtraEditors.TextEdit txtProcessCode;
        private DevExpress.XtraEditors.LabelControl lblProcessCode;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.TextEdit txtEndTypeID;
        private DevExpress.XtraEditors.LabelControl lblEndTypeId;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
