namespace Mineware.Systems.ProductionGlobal
{
    partial class frmProgress
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
            this.pbTheProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.labDescription = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pbTheProgress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTheProgress
            // 
            this.pbTheProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTheProgress.Location = new System.Drawing.Point(0, 0);
            this.pbTheProgress.Name = "pbTheProgress";
            this.pbTheProgress.Size = new System.Drawing.Size(280, 21);
            this.pbTheProgress.TabIndex = 0;
            // 
            // labDescription
            // 
            this.labDescription.Location = new System.Drawing.Point(0, 0);
            this.labDescription.Name = "labDescription";
            this.labDescription.Size = new System.Drawing.Size(0, 13);
            this.labDescription.TabIndex = 1;
            // 
            // frmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 21);
            this.ControlBox = false;
            this.Controls.Add(this.labDescription);
            this.Controls.Add(this.pbTheProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmProgress";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pbTheProgress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl pbTheProgress;
        private DevExpress.XtraEditors.LabelControl labDescription;
    }
}