namespace Mineware.Systems.Production.SysAdminScreens.AMISSetup
{
    partial class ileCourseBlankSample
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
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.dxStandardCRM = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.lblLabDetectionLimit = new DevExpress.XtraEditors.LabelControl();
            this.txtLabDetectionLimit = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dxStandardCRM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLabDetectionLimit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.Location = new System.Drawing.Point(133, 39);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dxStandardCRM
            // 
            this.dxStandardCRM.ContainerControl = this;
            // 
            // lblLabDetectionLimit
            // 
            this.lblLabDetectionLimit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabDetectionLimit.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblLabDetectionLimit, "");
            this.lblLabDetectionLimit.Location = new System.Drawing.Point(18, 16);
            this.lblLabDetectionLimit.Name = "lblLabDetectionLimit";
            this.lblLabDetectionLimit.Size = new System.Drawing.Size(109, 13);
            this.lblLabDetectionLimit.TabIndex = 29;
            this.lblLabDetectionLimit.Text = "Lab Detection Limit";
            // 
            // txtLabDetectionLimit
            // 
            this.SetBoundPropertyName(this.txtLabDetectionLimit, "");
            this.txtLabDetectionLimit.Location = new System.Drawing.Point(133, 13);
            this.txtLabDetectionLimit.Name = "txtLabDetectionLimit";
            this.txtLabDetectionLimit.Size = new System.Drawing.Size(100, 20);
            this.txtLabDetectionLimit.TabIndex = 28;
            // 
            // ileCourseBlankSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLabDetectionLimit);
            this.Controls.Add(this.txtLabDetectionLimit);
            this.Controls.Add(this.btnUpdate);
            this.Name = "ileCourseBlankSample";
            this.Size = new System.Drawing.Size(246, 74);
            this.Load += new System.EventHandler(this.ileCourseBlankSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxStandardCRM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLabDetectionLimit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxStandardCRM;
        private DevExpress.XtraEditors.LabelControl lblLabDetectionLimit;
        public DevExpress.XtraEditors.TextEdit txtLabDetectionLimit;
    }
}
