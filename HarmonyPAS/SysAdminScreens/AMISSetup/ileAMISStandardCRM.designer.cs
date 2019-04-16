namespace Mineware.Systems.HarmonyPAS.SysAdminScreens.AMISSetup
{
    partial class ileAMISStandardCRM
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
            this.dxStandardCRM = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            this.lblSRMName = new DevExpress.XtraEditors.LabelControl();
            this.txtSRMName = new DevExpress.XtraEditors.TextEdit();
            this.lblSRMStdDev = new DevExpress.XtraEditors.LabelControl();
            this.txtSRMStdDev = new DevExpress.XtraEditors.TextEdit();
            this.lblSRMMeanValue = new DevExpress.XtraEditors.LabelControl();
            this.txtSRMMeanValue = new DevExpress.XtraEditors.TextEdit();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dxStandardCRM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSRMName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSRMStdDev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSRMMeanValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dxStandardCRM
            // 
            this.dxStandardCRM.ContainerControl = this;
            // 
            // lblSRMName
            // 
            this.lblSRMName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSRMName.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblSRMName, "");
            this.lblSRMName.Location = new System.Drawing.Point(90, 70);
            this.lblSRMName.Name = "lblSRMName";
            this.lblSRMName.Size = new System.Drawing.Size(60, 13);
            this.lblSRMName.TabIndex = 38;
            this.lblSRMName.Text = "SRM Name";
            // 
            // txtSRMName
            // 
            this.SetBoundPropertyName(this.txtSRMName, "");
            this.txtSRMName.Enabled = false;
            this.txtSRMName.Location = new System.Drawing.Point(156, 67);
            this.txtSRMName.Name = "txtSRMName";
            this.txtSRMName.Size = new System.Drawing.Size(100, 20);
            this.txtSRMName.TabIndex = 37;
            // 
            // lblSRMStdDev
            // 
            this.lblSRMStdDev.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSRMStdDev.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblSRMStdDev, "");
            this.lblSRMStdDev.Location = new System.Drawing.Point(13, 44);
            this.lblSRMStdDev.Name = "lblSRMStdDev";
            this.lblSRMStdDev.Size = new System.Drawing.Size(137, 13);
            this.lblSRMStdDev.TabIndex = 36;
            this.lblSRMStdDev.Text = "SRM Standard Deviation";
            // 
            // txtSRMStdDev
            // 
            this.SetBoundPropertyName(this.txtSRMStdDev, "");
            this.txtSRMStdDev.Enabled = false;
            this.txtSRMStdDev.Location = new System.Drawing.Point(156, 41);
            this.txtSRMStdDev.Name = "txtSRMStdDev";
            this.txtSRMStdDev.Size = new System.Drawing.Size(100, 20);
            this.txtSRMStdDev.TabIndex = 35;
            // 
            // lblSRMMeanValue
            // 
            this.lblSRMMeanValue.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSRMMeanValue.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblSRMMeanValue, "");
            this.lblSRMMeanValue.Location = new System.Drawing.Point(57, 18);
            this.lblSRMMeanValue.Name = "lblSRMMeanValue";
            this.lblSRMMeanValue.Size = new System.Drawing.Size(93, 13);
            this.lblSRMMeanValue.TabIndex = 34;
            this.lblSRMMeanValue.Text = "SRM Mean Value";
            // 
            // txtSRMMeanValue
            // 
            this.SetBoundPropertyName(this.txtSRMMeanValue, "");
            this.txtSRMMeanValue.Enabled = false;
            this.txtSRMMeanValue.Location = new System.Drawing.Point(156, 15);
            this.txtSRMMeanValue.Name = "txtSRMMeanValue";
            this.txtSRMMeanValue.Size = new System.Drawing.Size(100, 20);
            this.txtSRMMeanValue.TabIndex = 33;
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.Location = new System.Drawing.Point(156, 93);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 32;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Visible = false;
            // 
            // ileAMISStandardCRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSRMName);
            this.Controls.Add(this.txtSRMName);
            this.Controls.Add(this.lblSRMStdDev);
            this.Controls.Add(this.txtSRMStdDev);
            this.Controls.Add(this.lblSRMMeanValue);
            this.Controls.Add(this.txtSRMMeanValue);
            this.Controls.Add(this.btnUpdate);
            this.Name = "ileAMISStandardCRM";
            this.Size = new System.Drawing.Size(269, 128);
            this.Load += new System.EventHandler(this.ileSectionScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxStandardCRM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSRMName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSRMStdDev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSRMMeanValue.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxStandardCRM;
        private DevExpress.XtraEditors.LabelControl lblSRMName;
        public DevExpress.XtraEditors.TextEdit txtSRMName;
        private DevExpress.XtraEditors.LabelControl lblSRMStdDev;
        public DevExpress.XtraEditors.TextEdit txtSRMStdDev;
        private DevExpress.XtraEditors.LabelControl lblSRMMeanValue;
        public DevExpress.XtraEditors.TextEdit txtSRMMeanValue;
        public DevExpress.XtraEditors.SimpleButton btnUpdate;
    }
}
