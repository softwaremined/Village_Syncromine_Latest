namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    partial class ucSystemSettingsCurrent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSystemSettingsCurrent));
            this.pnlCurrent = new DevExpress.XtraEditors.PanelControl();
            this.btnSaveCurrent = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            this.pnlCurrentData = new DevExpress.XtraEditors.PanelControl();
            this.lblPercQual = new DevExpress.XtraEditors.LabelControl();
            this.lblPercQual1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPercQual = new DevExpress.XtraEditors.TextEdit();
            this.lblMaxAllowedAdvDev1 = new DevExpress.XtraEditors.LabelControl();
            this.lblMaxAllowedAdvDev = new DevExpress.XtraEditors.LabelControl();
            this.txtMaxAllowedAdvDev = new DevExpress.XtraEditors.TextEdit();
            this.lblMaxAllowedAdvStp = new DevExpress.XtraEditors.LabelControl();
            this.txtMaxAllowedAdvStp = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCurrent)).BeginInit();
            this.pnlCurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCurrentData)).BeginInit();
            this.pnlCurrentData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPercQual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxAllowedAdvDev.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxAllowedAdvStp.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCurrent
            // 
            this.pnlCurrent.Controls.Add(this.btnSaveCurrent);
            this.pnlCurrent.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCurrent.Location = new System.Drawing.Point(0, 0);
            this.pnlCurrent.Name = "pnlCurrent";
            this.pnlCurrent.Size = new System.Drawing.Size(887, 67);
            this.pnlCurrent.TabIndex = 5;
            // 
            // btnSaveCurrent
            // 
            this.btnSaveCurrent.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSaveCurrent.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveCurrent.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSaveCurrent.Location = new System.Drawing.Point(2, 2);
            this.btnSaveCurrent.Margin = new System.Windows.Forms.Padding(6, 12, 6, 12);
            this.btnSaveCurrent.Name = "btnSaveCurrent";
            this.btnSaveCurrent.Size = new System.Drawing.Size(141, 63);
            this.btnSaveCurrent.TabIndex = 2;
            this.btnSaveCurrent.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Save;
            this.btnSaveCurrent.theCustomeText = null;
            this.btnSaveCurrent.theImage = ((System.Drawing.Image)(resources.GetObject("btnSaveCurrent.theImage")));
            this.btnSaveCurrent.theImageHot = ((System.Drawing.Image)(resources.GetObject("btnSaveCurrent.theImageHot")));
            this.btnSaveCurrent.theSuperToolTip = null;
            this.btnSaveCurrent.Click += new System.EventHandler(this.btnSaveCurrent_Click);
            // 
            // pnlCurrentData
            // 
            this.pnlCurrentData.Controls.Add(this.lblPercQual);
            this.pnlCurrentData.Controls.Add(this.lblPercQual1);
            this.pnlCurrentData.Controls.Add(this.txtPercQual);
            this.pnlCurrentData.Controls.Add(this.lblMaxAllowedAdvDev1);
            this.pnlCurrentData.Controls.Add(this.lblMaxAllowedAdvDev);
            this.pnlCurrentData.Controls.Add(this.txtMaxAllowedAdvDev);
            this.pnlCurrentData.Controls.Add(this.lblMaxAllowedAdvStp);
            this.pnlCurrentData.Controls.Add(this.txtMaxAllowedAdvStp);
            this.pnlCurrentData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCurrentData.Location = new System.Drawing.Point(0, 67);
            this.pnlCurrentData.Name = "pnlCurrentData";
            this.pnlCurrentData.Size = new System.Drawing.Size(887, 684);
            this.pnlCurrentData.TabIndex = 6;
            // 
            // lblPercQual
            // 
            this.lblPercQual.Location = new System.Drawing.Point(18, 104);
            this.lblPercQual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPercQual.Name = "lblPercQual";
            this.lblPercQual.Size = new System.Drawing.Size(187, 13);
            this.lblPercQual.TabIndex = 39;
            this.lblPercQual.Text = "% of Face Blasted to Qualify as a Blast";
            // 
            // lblPercQual1
            // 
            this.lblPercQual1.Location = new System.Drawing.Point(324, 104);
            this.lblPercQual1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPercQual1.Name = "lblPercQual1";
            this.lblPercQual1.Size = new System.Drawing.Size(11, 13);
            this.lblPercQual1.TabIndex = 38;
            this.lblPercQual1.Text = "%";
            // 
            // txtPercQual
            // 
            this.txtPercQual.EditValue = "0.00";
            this.txtPercQual.Location = new System.Drawing.Point(235, 101);
            this.txtPercQual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPercQual.Name = "txtPercQual";
            this.txtPercQual.Properties.DisplayFormat.FormatString = "n2";
            this.txtPercQual.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPercQual.Properties.EditFormat.FormatString = "n2";
            this.txtPercQual.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPercQual.Properties.Mask.EditMask = "n2";
            this.txtPercQual.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPercQual.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPercQual.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPercQual.Size = new System.Drawing.Size(81, 20);
            this.txtPercQual.TabIndex = 37;
            // 
            // lblMaxAllowedAdvDev1
            // 
            this.lblMaxAllowedAdvDev1.Location = new System.Drawing.Point(324, 64);
            this.lblMaxAllowedAdvDev1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblMaxAllowedAdvDev1.Name = "lblMaxAllowedAdvDev1";
            this.lblMaxAllowedAdvDev1.Size = new System.Drawing.Size(8, 13);
            this.lblMaxAllowedAdvDev1.TabIndex = 36;
            this.lblMaxAllowedAdvDev1.Text = "m";
            // 
            // lblMaxAllowedAdvDev
            // 
            this.lblMaxAllowedAdvDev.Location = new System.Drawing.Point(18, 64);
            this.lblMaxAllowedAdvDev.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblMaxAllowedAdvDev.Name = "lblMaxAllowedAdvDev";
            this.lblMaxAllowedAdvDev.Size = new System.Drawing.Size(195, 13);
            this.lblMaxAllowedAdvDev.TabIndex = 35;
            this.lblMaxAllowedAdvDev.Text = "Maximum Allowed Advance Development";
            // 
            // txtMaxAllowedAdvDev
            // 
            this.txtMaxAllowedAdvDev.EditValue = "0.00";
            this.txtMaxAllowedAdvDev.Location = new System.Drawing.Point(235, 61);
            this.txtMaxAllowedAdvDev.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaxAllowedAdvDev.Name = "txtMaxAllowedAdvDev";
            this.txtMaxAllowedAdvDev.Properties.DisplayFormat.FormatString = "n2";
            this.txtMaxAllowedAdvDev.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMaxAllowedAdvDev.Properties.EditFormat.FormatString = "n2";
            this.txtMaxAllowedAdvDev.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMaxAllowedAdvDev.Properties.Mask.EditMask = "n2";
            this.txtMaxAllowedAdvDev.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMaxAllowedAdvDev.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMaxAllowedAdvDev.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMaxAllowedAdvDev.Size = new System.Drawing.Size(81, 20);
            this.txtMaxAllowedAdvDev.TabIndex = 34;
            // 
            // lblMaxAllowedAdvStp
            // 
            this.lblMaxAllowedAdvStp.Location = new System.Drawing.Point(18, 23);
            this.lblMaxAllowedAdvStp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblMaxAllowedAdvStp.Name = "lblMaxAllowedAdvStp";
            this.lblMaxAllowedAdvStp.Size = new System.Drawing.Size(163, 13);
            this.lblMaxAllowedAdvStp.TabIndex = 32;
            this.lblMaxAllowedAdvStp.Text = "Maximum Allowed Stoping Booking";
            // 
            // txtMaxAllowedAdvStp
            // 
            this.txtMaxAllowedAdvStp.EditValue = "";
            this.txtMaxAllowedAdvStp.Location = new System.Drawing.Point(235, 20);
            this.txtMaxAllowedAdvStp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaxAllowedAdvStp.Name = "txtMaxAllowedAdvStp";
            this.txtMaxAllowedAdvStp.Properties.DisplayFormat.FormatString = "n2";
            this.txtMaxAllowedAdvStp.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMaxAllowedAdvStp.Properties.EditFormat.FormatString = "n2";
            this.txtMaxAllowedAdvStp.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMaxAllowedAdvStp.Properties.Mask.EditMask = "n2";
            this.txtMaxAllowedAdvStp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMaxAllowedAdvStp.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMaxAllowedAdvStp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMaxAllowedAdvStp.Size = new System.Drawing.Size(81, 20);
            this.txtMaxAllowedAdvStp.TabIndex = 31;
            // 
            // ucSystemSettingsCurrent
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCurrentData);
            this.Controls.Add(this.pnlCurrent);
            this.Name = "ucSystemSettingsCurrent";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(887, 751);
            this.Load += new System.EventHandler(this.ucSystemSettingsCurrent_Load);
            this.Controls.SetChildIndex(this.pnlCurrent, 0);
            this.Controls.SetChildIndex(this.pnlCurrentData, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCurrent)).EndInit();
            this.pnlCurrent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCurrentData)).EndInit();
            this.pnlCurrentData.ResumeLayout(false);
            this.pnlCurrentData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPercQual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxAllowedAdvDev.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxAllowedAdvStp.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlCurrent;
        private Global.sysButtons.ucSysBtn btnSaveCurrent;
        private DevExpress.XtraEditors.PanelControl pnlCurrentData;
        private DevExpress.XtraEditors.LabelControl lblPercQual;
        private DevExpress.XtraEditors.LabelControl lblPercQual1;
        private DevExpress.XtraEditors.TextEdit txtPercQual;
        private DevExpress.XtraEditors.LabelControl lblMaxAllowedAdvDev1;
        private DevExpress.XtraEditors.LabelControl lblMaxAllowedAdvDev;
        private DevExpress.XtraEditors.TextEdit txtMaxAllowedAdvDev;
        private DevExpress.XtraEditors.LabelControl lblMaxAllowedAdvStp;
        private DevExpress.XtraEditors.TextEdit txtMaxAllowedAdvStp;
    }
}
