namespace Mineware.Systems.Production.SysAdminScreens.AMISSetup
{
    partial class ileStandardCRM
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
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txtMeanValue = new DevExpress.XtraEditors.TextEdit();
            this.lblMeanValue = new DevExpress.XtraEditors.LabelControl();
            this.dxStandardCRM = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            this.lblStdDev = new DevExpress.XtraEditors.LabelControl();
            this.txtStdDev = new DevExpress.XtraEditors.TextEdit();
            this.lblCRM = new DevExpress.XtraEditors.LabelControl();
            this.txtCRM = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeanValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxStandardCRM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStdDev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCRM.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.Location = new System.Drawing.Point(125, 89);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtMeanValue
            // 
            this.SetBoundPropertyName(this.txtMeanValue, "");
            this.txtMeanValue.Location = new System.Drawing.Point(125, 37);
            this.txtMeanValue.Name = "txtMeanValue";
            this.txtMeanValue.Size = new System.Drawing.Size(100, 20);
            this.txtMeanValue.TabIndex = 22;
            // 
            // lblMeanValue
            // 
            this.lblMeanValue.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeanValue.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblMeanValue, "");
            this.lblMeanValue.Location = new System.Drawing.Point(54, 40);
            this.lblMeanValue.Name = "lblMeanValue";
            this.lblMeanValue.Size = new System.Drawing.Size(65, 13);
            this.lblMeanValue.TabIndex = 27;
            this.lblMeanValue.Text = "Mean Value";
            // 
            // dxStandardCRM
            // 
            this.dxStandardCRM.ContainerControl = this;
            // 
            // lblStdDev
            // 
            this.lblStdDev.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStdDev.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblStdDev, "");
            this.lblStdDev.Location = new System.Drawing.Point(15, 66);
            this.lblStdDev.Name = "lblStdDev";
            this.lblStdDev.Size = new System.Drawing.Size(104, 13);
            this.lblStdDev.TabIndex = 29;
            this.lblStdDev.Text = "Sandard Deviation";
            // 
            // txtStdDev
            // 
            this.SetBoundPropertyName(this.txtStdDev, "");
            this.txtStdDev.Location = new System.Drawing.Point(125, 63);
            this.txtStdDev.Name = "txtStdDev";
            this.txtStdDev.Size = new System.Drawing.Size(100, 20);
            this.txtStdDev.TabIndex = 28;
            // 
            // lblCRM
            // 
            this.lblCRM.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCRM.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblCRM, "");
            this.lblCRM.Location = new System.Drawing.Point(94, 14);
            this.lblCRM.Name = "lblCRM";
            this.lblCRM.Size = new System.Drawing.Size(25, 13);
            this.lblCRM.TabIndex = 31;
            this.lblCRM.Text = "CRM";
            // 
            // txtCRM
            // 
            this.SetBoundPropertyName(this.txtCRM, "");
            this.txtCRM.Location = new System.Drawing.Point(125, 11);
            this.txtCRM.Name = "txtCRM";
            this.txtCRM.Size = new System.Drawing.Size(100, 20);
            this.txtCRM.TabIndex = 30;
            // 
            // ileAMIS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCRM);
            this.Controls.Add(this.txtCRM);
            this.Controls.Add(this.lblStdDev);
            this.Controls.Add(this.txtStdDev);
            this.Controls.Add(this.lblMeanValue);
            this.Controls.Add(this.txtMeanValue);
            this.Controls.Add(this.btnUpdate);
            this.Name = "ileAMIS";
            this.Size = new System.Drawing.Size(237, 120);
            this.Load += new System.EventHandler(this.ileStandardCRM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMeanValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxStandardCRM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStdDev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCRM.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.LabelControl lblMeanValue;
        public DevExpress.XtraEditors.TextEdit txtMeanValue;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxStandardCRM;
        private DevExpress.XtraEditors.LabelControl lblCRM;
        public DevExpress.XtraEditors.TextEdit txtCRM;
        private DevExpress.XtraEditors.LabelControl lblStdDev;
        public DevExpress.XtraEditors.TextEdit txtStdDev;
    }
}
