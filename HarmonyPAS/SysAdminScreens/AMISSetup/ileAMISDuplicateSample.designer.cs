namespace Mineware.Systems.HarmonyPAS.SysAdminScreens.AMISSetup
{
    partial class ileAMISDuplicateSample
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
            this.txtPayLimit = new DevExpress.XtraEditors.TextEdit();
            this.lblPayLimit = new DevExpress.XtraEditors.LabelControl();
            this.dxDuplicateSample = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxDuplicateSample)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.Location = new System.Drawing.Point(72, 42);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtPayLimit
            // 
            this.SetBoundPropertyName(this.txtPayLimit, "");
            this.txtPayLimit.Enabled = false;
            this.txtPayLimit.Location = new System.Drawing.Point(72, 16);
            this.txtPayLimit.Name = "txtPayLimit";
            this.txtPayLimit.Size = new System.Drawing.Size(100, 20);
            this.txtPayLimit.TabIndex = 22;
            // 
            // lblPayLimit
            // 
            this.lblPayLimit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPayLimit.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblPayLimit, "");
            this.lblPayLimit.Location = new System.Drawing.Point(14, 19);
            this.lblPayLimit.Name = "lblPayLimit";
            this.lblPayLimit.Size = new System.Drawing.Size(52, 13);
            this.lblPayLimit.TabIndex = 27;
            this.lblPayLimit.Text = "Pay Limit";
            // 
            // dxDuplicateSample
            // 
            this.dxDuplicateSample.ContainerControl = this;
            // 
            // ileAMISDuplicateSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPayLimit);
            this.Controls.Add(this.txtPayLimit);
            this.Controls.Add(this.btnUpdate);
            this.Name = "ileAMISDuplicateSample";
            this.Size = new System.Drawing.Size(184, 80);
            this.Load += new System.EventHandler(this.ileAMISDuplicateSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPayLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxDuplicateSample)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.LabelControl lblPayLimit;
        public DevExpress.XtraEditors.TextEdit txtPayLimit;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxDuplicateSample;
    }
}
