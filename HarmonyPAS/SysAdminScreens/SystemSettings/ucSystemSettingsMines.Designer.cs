namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    partial class ucSystemSettingsMines
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSystemSettingsMines));
            this.pnlMines = new DevExpress.XtraEditors.PanelControl();
            this.btnSaveBookings = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            this.pnlMinesData = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.TopPanelsTxt = new DevExpress.XtraEditors.TextEdit();
            this.lblEngNoOfDaysBackdate = new DevExpress.XtraEditors.LabelControl();
            this.txtEngNoOfDaysBackdate = new DevExpress.XtraEditors.TextEdit();
            this.grpHierarchicalLinkage = new DevExpress.XtraEditors.GroupControl();
            this.txtEngCaptLvl = new DevExpress.XtraEditors.TextEdit();
            this.lblEngCaptLvl = new DevExpress.XtraEditors.LabelControl();
            this.lblEngProdLink = new DevExpress.XtraEditors.LabelControl();
            this.txtEngProdLink = new DevExpress.XtraEditors.TextEdit();
            this.lblMOHierarchicalID = new DevExpress.XtraEditors.LabelControl();
            this.txtMOHierarchicalID = new DevExpress.XtraEditors.TextEdit();
            this.lblBanner = new DevExpress.XtraEditors.LabelControl();
            this.txtBanner = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMines)).BeginInit();
            this.pnlMines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMinesData)).BeginInit();
            this.pnlMinesData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopPanelsTxt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngNoOfDaysBackdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHierarchicalLinkage)).BeginInit();
            this.grpHierarchicalLinkage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngCaptLvl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngProdLink.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMOHierarchicalID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBanner.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMines
            // 
            this.pnlMines.Controls.Add(this.btnSaveBookings);
            this.pnlMines.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMines.Location = new System.Drawing.Point(0, 0);
            this.pnlMines.Name = "pnlMines";
            this.pnlMines.Size = new System.Drawing.Size(519, 67);
            this.pnlMines.TabIndex = 5;
            // 
            // btnSaveBookings
            // 
            this.btnSaveBookings.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSaveBookings.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveBookings.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSaveBookings.Location = new System.Drawing.Point(2, 2);
            this.btnSaveBookings.Margin = new System.Windows.Forms.Padding(6, 12, 6, 12);
            this.btnSaveBookings.Name = "btnSaveBookings";
            this.btnSaveBookings.Size = new System.Drawing.Size(141, 63);
            this.btnSaveBookings.TabIndex = 2;
            this.btnSaveBookings.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Save;
            this.btnSaveBookings.theCustomeText = null;
            this.btnSaveBookings.theImage = ((System.Drawing.Image)(resources.GetObject("btnSaveBookings.theImage")));
            this.btnSaveBookings.theImageHot = ((System.Drawing.Image)(resources.GetObject("btnSaveBookings.theImageHot")));
            this.btnSaveBookings.theSuperToolTip = null;
            this.btnSaveBookings.Click += new System.EventHandler(this.btnSaveBookings_Click);
            // 
            // pnlMinesData
            // 
            this.pnlMinesData.Controls.Add(this.labelControl1);
            this.pnlMinesData.Controls.Add(this.TopPanelsTxt);
            this.pnlMinesData.Controls.Add(this.lblEngNoOfDaysBackdate);
            this.pnlMinesData.Controls.Add(this.txtEngNoOfDaysBackdate);
            this.pnlMinesData.Controls.Add(this.grpHierarchicalLinkage);
            this.pnlMinesData.Controls.Add(this.lblBanner);
            this.pnlMinesData.Controls.Add(this.txtBanner);
            this.pnlMinesData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMinesData.Location = new System.Drawing.Point(0, 67);
            this.pnlMinesData.Name = "pnlMinesData";
            this.pnlMinesData.Size = new System.Drawing.Size(519, 299);
            this.pnlMinesData.TabIndex = 6;
            this.pnlMinesData.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMinesData_Paint);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(42, 160);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 13);
            this.labelControl1.TabIndex = 39;
            this.labelControl1.Text = "No. Top Panels";
            // 
            // TopPanelsTxt
            // 
            this.TopPanelsTxt.EditValue = "0";
            this.TopPanelsTxt.Location = new System.Drawing.Point(122, 157);
            this.TopPanelsTxt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TopPanelsTxt.Name = "TopPanelsTxt";
            this.TopPanelsTxt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TopPanelsTxt.Size = new System.Drawing.Size(72, 20);
            this.TopPanelsTxt.TabIndex = 38;
            // 
            // lblEngNoOfDaysBackdate
            // 
            this.lblEngNoOfDaysBackdate.Location = new System.Drawing.Point(42, 238);
            this.lblEngNoOfDaysBackdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblEngNoOfDaysBackdate.Name = "lblEngNoOfDaysBackdate";
            this.lblEngNoOfDaysBackdate.Size = new System.Drawing.Size(163, 13);
            this.lblEngNoOfDaysBackdate.TabIndex = 37;
            this.lblEngNoOfDaysBackdate.Text = "Engineering No. of Days Backdate";
            this.lblEngNoOfDaysBackdate.Visible = false;
            // 
            // txtEngNoOfDaysBackdate
            // 
            this.txtEngNoOfDaysBackdate.EditValue = "0";
            this.txtEngNoOfDaysBackdate.Location = new System.Drawing.Point(234, 235);
            this.txtEngNoOfDaysBackdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEngNoOfDaysBackdate.Name = "txtEngNoOfDaysBackdate";
            this.txtEngNoOfDaysBackdate.Properties.DisplayFormat.FormatString = "n0";
            this.txtEngNoOfDaysBackdate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtEngNoOfDaysBackdate.Properties.EditFormat.FormatString = "n0";
            this.txtEngNoOfDaysBackdate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtEngNoOfDaysBackdate.Properties.Mask.EditMask = "0";
            this.txtEngNoOfDaysBackdate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEngNoOfDaysBackdate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtEngNoOfDaysBackdate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtEngNoOfDaysBackdate.Size = new System.Drawing.Size(72, 20);
            this.txtEngNoOfDaysBackdate.TabIndex = 36;
            this.txtEngNoOfDaysBackdate.Visible = false;
            // 
            // grpHierarchicalLinkage
            // 
            this.grpHierarchicalLinkage.Controls.Add(this.txtEngCaptLvl);
            this.grpHierarchicalLinkage.Controls.Add(this.lblEngCaptLvl);
            this.grpHierarchicalLinkage.Controls.Add(this.lblEngProdLink);
            this.grpHierarchicalLinkage.Controls.Add(this.txtEngProdLink);
            this.grpHierarchicalLinkage.Controls.Add(this.lblMOHierarchicalID);
            this.grpHierarchicalLinkage.Controls.Add(this.txtMOHierarchicalID);
            this.grpHierarchicalLinkage.Location = new System.Drawing.Point(42, 54);
            this.grpHierarchicalLinkage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpHierarchicalLinkage.Name = "grpHierarchicalLinkage";
            this.grpHierarchicalLinkage.Size = new System.Drawing.Size(427, 81);
            this.grpHierarchicalLinkage.TabIndex = 35;
            this.grpHierarchicalLinkage.Text = "Hierarchical Linkage";
            // 
            // txtEngCaptLvl
            // 
            this.txtEngCaptLvl.EditValue = "0";
            this.txtEngCaptLvl.Location = new System.Drawing.Point(192, 118);
            this.txtEngCaptLvl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEngCaptLvl.Name = "txtEngCaptLvl";
            this.txtEngCaptLvl.Properties.DisplayFormat.FormatString = "n0";
            this.txtEngCaptLvl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtEngCaptLvl.Properties.EditFormat.FormatString = "n0";
            this.txtEngCaptLvl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtEngCaptLvl.Properties.Mask.EditMask = "n0";
            this.txtEngCaptLvl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEngCaptLvl.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtEngCaptLvl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtEngCaptLvl.Size = new System.Drawing.Size(72, 20);
            this.txtEngCaptLvl.TabIndex = 9;
            this.txtEngCaptLvl.Visible = false;
            // 
            // lblEngCaptLvl
            // 
            this.lblEngCaptLvl.Location = new System.Drawing.Point(22, 121);
            this.lblEngCaptLvl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblEngCaptLvl.Name = "lblEngCaptLvl";
            this.lblEngCaptLvl.Size = new System.Drawing.Size(126, 13);
            this.lblEngCaptLvl.TabIndex = 8;
            this.lblEngCaptLvl.Text = "Engineering Capture Level";
            this.lblEngCaptLvl.Visible = false;
            // 
            // lblEngProdLink
            // 
            this.lblEngProdLink.Location = new System.Drawing.Point(22, 83);
            this.lblEngProdLink.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblEngProdLink.Name = "lblEngProdLink";
            this.lblEngProdLink.Size = new System.Drawing.Size(132, 13);
            this.lblEngProdLink.TabIndex = 7;
            this.lblEngProdLink.Text = "Engineering/Production Link";
            this.lblEngProdLink.Visible = false;
            // 
            // txtEngProdLink
            // 
            this.txtEngProdLink.EditValue = "0";
            this.txtEngProdLink.Location = new System.Drawing.Point(192, 79);
            this.txtEngProdLink.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEngProdLink.Name = "txtEngProdLink";
            this.txtEngProdLink.Properties.DisplayFormat.FormatString = "n0";
            this.txtEngProdLink.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtEngProdLink.Properties.EditFormat.FormatString = "n0";
            this.txtEngProdLink.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtEngProdLink.Properties.Mask.EditMask = "n0";
            this.txtEngProdLink.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtEngProdLink.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtEngProdLink.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtEngProdLink.Size = new System.Drawing.Size(72, 20);
            this.txtEngProdLink.TabIndex = 6;
            this.txtEngProdLink.Visible = false;
            // 
            // lblMOHierarchicalID
            // 
            this.lblMOHierarchicalID.Location = new System.Drawing.Point(22, 42);
            this.lblMOHierarchicalID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblMOHierarchicalID.Name = "lblMOHierarchicalID";
            this.lblMOHierarchicalID.Size = new System.Drawing.Size(92, 13);
            this.lblMOHierarchicalID.TabIndex = 5;
            this.lblMOHierarchicalID.Text = "M/O Hierarchical ID";
            // 
            // txtMOHierarchicalID
            // 
            this.txtMOHierarchicalID.EditValue = "0";
            this.txtMOHierarchicalID.Location = new System.Drawing.Point(192, 39);
            this.txtMOHierarchicalID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMOHierarchicalID.Name = "txtMOHierarchicalID";
            this.txtMOHierarchicalID.Properties.DisplayFormat.FormatString = "n0";
            this.txtMOHierarchicalID.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMOHierarchicalID.Properties.EditFormat.FormatString = "n0";
            this.txtMOHierarchicalID.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMOHierarchicalID.Properties.Mask.EditMask = "n0";
            this.txtMOHierarchicalID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMOHierarchicalID.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMOHierarchicalID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMOHierarchicalID.Size = new System.Drawing.Size(72, 20);
            this.txtMOHierarchicalID.TabIndex = 1;
            // 
            // lblBanner
            // 
            this.lblBanner.Location = new System.Drawing.Point(42, 18);
            this.lblBanner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblBanner.Name = "lblBanner";
            this.lblBanner.Size = new System.Drawing.Size(34, 13);
            this.lblBanner.TabIndex = 34;
            this.lblBanner.Text = "Banner";
            // 
            // txtBanner
            // 
            this.txtBanner.Location = new System.Drawing.Point(94, 15);
            this.txtBanner.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBanner.Name = "txtBanner";
            this.txtBanner.Size = new System.Drawing.Size(312, 20);
            this.txtBanner.TabIndex = 33;
            // 
            // ucSystemSettingsMines
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMinesData);
            this.Controls.Add(this.pnlMines);
            this.Name = "ucSystemSettingsMines";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(519, 366);
            this.Load += new System.EventHandler(this.ucSystemSettingsMines_Load);
            this.Controls.SetChildIndex(this.pnlMines, 0);
            this.Controls.SetChildIndex(this.pnlMinesData, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMines)).EndInit();
            this.pnlMines.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMinesData)).EndInit();
            this.pnlMinesData.ResumeLayout(false);
            this.pnlMinesData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopPanelsTxt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngNoOfDaysBackdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHierarchicalLinkage)).EndInit();
            this.grpHierarchicalLinkage.ResumeLayout(false);
            this.grpHierarchicalLinkage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngCaptLvl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEngProdLink.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMOHierarchicalID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBanner.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMines;
        private Global.sysButtons.ucSysBtn btnSaveBookings;
        private DevExpress.XtraEditors.PanelControl pnlMinesData;
        private DevExpress.XtraEditors.LabelControl lblEngNoOfDaysBackdate;
        private DevExpress.XtraEditors.TextEdit txtEngNoOfDaysBackdate;
        private DevExpress.XtraEditors.GroupControl grpHierarchicalLinkage;
        private DevExpress.XtraEditors.TextEdit txtEngCaptLvl;
        private DevExpress.XtraEditors.LabelControl lblEngCaptLvl;
        private DevExpress.XtraEditors.LabelControl lblEngProdLink;
        private DevExpress.XtraEditors.TextEdit txtEngProdLink;
        private DevExpress.XtraEditors.LabelControl lblMOHierarchicalID;
        private DevExpress.XtraEditors.TextEdit txtMOHierarchicalID;
        private DevExpress.XtraEditors.LabelControl lblBanner;
        private DevExpress.XtraEditors.TextEdit txtBanner;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit TopPanelsTxt;
    }
}
