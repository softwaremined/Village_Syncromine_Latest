namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    partial class ucSystemSettingsFactors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSystemSettingsFactors));
            this.pnlFactors = new DevExpress.XtraEditors.PanelControl();
            this.btnSaveFactors = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            this.pnlFactorsData = new DevExpress.XtraEditors.PanelControl();
            this.lblBrokenRockDensity1 = new DevExpress.XtraEditors.LabelControl();
            this.lblBrokenRockDensity = new DevExpress.XtraEditors.LabelControl();
            this.txtBrokenRockDensity = new DevExpress.XtraEditors.TextEdit();
            this.lblRockDensity1 = new DevExpress.XtraEditors.LabelControl();
            this.lblRockDensity = new DevExpress.XtraEditors.LabelControl();
            this.txtRockDensity = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFactors)).BeginInit();
            this.pnlFactors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFactorsData)).BeginInit();
            this.pnlFactorsData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBrokenRockDensity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRockDensity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFactors
            // 
            this.pnlFactors.Controls.Add(this.btnSaveFactors);
            this.pnlFactors.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFactors.Location = new System.Drawing.Point(0, 0);
            this.pnlFactors.Name = "pnlFactors";
            this.pnlFactors.Size = new System.Drawing.Size(887, 67);
            this.pnlFactors.TabIndex = 5;
            // 
            // btnSaveFactors
            // 
            this.btnSaveFactors.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSaveFactors.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveFactors.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSaveFactors.Location = new System.Drawing.Point(2, 2);
            this.btnSaveFactors.Margin = new System.Windows.Forms.Padding(6, 12, 6, 12);
            this.btnSaveFactors.Name = "btnSaveFactors";
            this.btnSaveFactors.Size = new System.Drawing.Size(141, 63);
            this.btnSaveFactors.TabIndex = 2;
            this.btnSaveFactors.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Save;
            this.btnSaveFactors.theCustomeText = null;
            this.btnSaveFactors.theImage = ((System.Drawing.Image)(resources.GetObject("btnSaveFactors.theImage")));
            this.btnSaveFactors.theImageHot = ((System.Drawing.Image)(resources.GetObject("btnSaveFactors.theImageHot")));
            this.btnSaveFactors.theSuperToolTip = null;
            this.btnSaveFactors.Click += new System.EventHandler(this.btnSaveFactors_Click);
            // 
            // pnlFactorsData
            // 
            this.pnlFactorsData.Controls.Add(this.lblBrokenRockDensity1);
            this.pnlFactorsData.Controls.Add(this.lblBrokenRockDensity);
            this.pnlFactorsData.Controls.Add(this.txtBrokenRockDensity);
            this.pnlFactorsData.Controls.Add(this.lblRockDensity1);
            this.pnlFactorsData.Controls.Add(this.lblRockDensity);
            this.pnlFactorsData.Controls.Add(this.txtRockDensity);
            this.pnlFactorsData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFactorsData.Location = new System.Drawing.Point(0, 67);
            this.pnlFactorsData.Name = "pnlFactorsData";
            this.pnlFactorsData.Size = new System.Drawing.Size(887, 684);
            this.pnlFactorsData.TabIndex = 6;
            // 
            // lblBrokenRockDensity1
            // 
            this.lblBrokenRockDensity1.Location = new System.Drawing.Point(224, 58);
            this.lblBrokenRockDensity1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblBrokenRockDensity1.Name = "lblBrokenRockDensity1";
            this.lblBrokenRockDensity1.Size = new System.Drawing.Size(40, 13);
            this.lblBrokenRockDensity1.TabIndex = 19;
            this.lblBrokenRockDensity1.Text = "Tons/m³";
            // 
            // lblBrokenRockDensity
            // 
            this.lblBrokenRockDensity.Location = new System.Drawing.Point(15, 58);
            this.lblBrokenRockDensity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblBrokenRockDensity.Name = "lblBrokenRockDensity";
            this.lblBrokenRockDensity.Size = new System.Drawing.Size(102, 13);
            this.lblBrokenRockDensity.TabIndex = 18;
            this.lblBrokenRockDensity.Text = "Broken Rock Density:";
            // 
            // txtBrokenRockDensity
            // 
            this.txtBrokenRockDensity.EditValue = "0.00";
            this.txtBrokenRockDensity.Location = new System.Drawing.Point(130, 55);
            this.txtBrokenRockDensity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBrokenRockDensity.Name = "txtBrokenRockDensity";
            this.txtBrokenRockDensity.Properties.DisplayFormat.FormatString = "n2";
            this.txtBrokenRockDensity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtBrokenRockDensity.Properties.EditFormat.FormatString = "n2";
            this.txtBrokenRockDensity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtBrokenRockDensity.Properties.Mask.EditMask = "n2";
            this.txtBrokenRockDensity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtBrokenRockDensity.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtBrokenRockDensity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtBrokenRockDensity.Size = new System.Drawing.Size(86, 20);
            this.txtBrokenRockDensity.TabIndex = 17;
            // 
            // lblRockDensity1
            // 
            this.lblRockDensity1.Location = new System.Drawing.Point(224, 17);
            this.lblRockDensity1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblRockDensity1.Name = "lblRockDensity1";
            this.lblRockDensity1.Size = new System.Drawing.Size(40, 13);
            this.lblRockDensity1.TabIndex = 16;
            this.lblRockDensity1.Text = "Tons/m³";
            // 
            // lblRockDensity
            // 
            this.lblRockDensity.Location = new System.Drawing.Point(15, 17);
            this.lblRockDensity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblRockDensity.Name = "lblRockDensity";
            this.lblRockDensity.Size = new System.Drawing.Size(66, 13);
            this.lblRockDensity.TabIndex = 15;
            this.lblRockDensity.Text = "Rock Density:";
            // 
            // txtRockDensity
            // 
            this.txtRockDensity.EditValue = "0.00";
            this.txtRockDensity.Location = new System.Drawing.Point(130, 14);
            this.txtRockDensity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRockDensity.Name = "txtRockDensity";
            this.txtRockDensity.Properties.EditFormat.FormatString = "n2";
            this.txtRockDensity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtRockDensity.Properties.Mask.EditMask = "n2";
            this.txtRockDensity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRockDensity.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtRockDensity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRockDensity.Size = new System.Drawing.Size(86, 20);
            this.txtRockDensity.TabIndex = 14;
            // 
            // ucSystemSettingsFactors
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFactorsData);
            this.Controls.Add(this.pnlFactors);
            this.Name = "ucSystemSettingsFactors";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(887, 751);
            this.Load += new System.EventHandler(this.ucSystemSettingsFactors_Load);
            this.Controls.SetChildIndex(this.pnlFactors, 0);
            this.Controls.SetChildIndex(this.pnlFactorsData, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFactors)).EndInit();
            this.pnlFactors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFactorsData)).EndInit();
            this.pnlFactorsData.ResumeLayout(false);
            this.pnlFactorsData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBrokenRockDensity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRockDensity.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlFactors;
        private Global.sysButtons.ucSysBtn btnSaveFactors;
        private DevExpress.XtraEditors.PanelControl pnlFactorsData;
        private DevExpress.XtraEditors.LabelControl lblBrokenRockDensity1;
        private DevExpress.XtraEditors.LabelControl lblBrokenRockDensity;
        private DevExpress.XtraEditors.TextEdit txtBrokenRockDensity;
        private DevExpress.XtraEditors.LabelControl lblRockDensity1;
        private DevExpress.XtraEditors.LabelControl lblRockDensity;
        private DevExpress.XtraEditors.TextEdit txtRockDensity;
    }
}
