namespace Mineware.Systems.Reports.Planning_Protocol_Report
{
    partial class frmLockedPLanning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLockedPLanning));
            this.pcButtons = new DevExpress.XtraEditors.PanelControl();
            this.btnLast = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnNext = new Mineware.Systems.Global.CustomControls.MWButton();
            this.labNumberOfPages = new DevExpress.XtraEditors.LabelControl();
            this.txtPageNumber = new DevExpress.XtraEditors.TextEdit();
            this.btnPrevius = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnFirst = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnCopy = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnSave = new Mineware.Systems.Global.CustomControls.MWBottonDropDown();
            this.btnPrint = new Mineware.Systems.Global.CustomControls.MWButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.previewReport = new FastReport.Preview.PreviewControl();
            this.lbWorkplace = new DevExpress.XtraEditors.ListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcButtons)).BeginInit();
            this.pcButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbWorkplace)).BeginInit();
            this.SuspendLayout();
            // 
            // pcButtons
            // 
            this.pcButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcButtons.Controls.Add(this.btnLast);
            this.pcButtons.Controls.Add(this.btnNext);
            this.pcButtons.Controls.Add(this.labNumberOfPages);
            this.pcButtons.Controls.Add(this.txtPageNumber);
            this.pcButtons.Controls.Add(this.btnPrevius);
            this.pcButtons.Controls.Add(this.btnFirst);
            this.pcButtons.Controls.Add(this.btnCopy);
            this.pcButtons.Controls.Add(this.btnSave);
            this.pcButtons.Controls.Add(this.btnPrint);
            this.pcButtons.Controls.Add(this.panelControl1);
            this.pcButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcButtons.Location = new System.Drawing.Point(0, 0);
            this.pcButtons.Name = "pcButtons";
            this.pcButtons.Size = new System.Drawing.Size(975, 31);
            this.pcButtons.TabIndex = 13;
            // 
            // btnLast
            // 
            this.btnLast.AllowFocus = false;
            this.btnLast.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnLast.Appearance.Options.UseForeColor = true;
            this.btnLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.ImageLeftPadding = 0;
            this.btnLast.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLast.Location = new System.Drawing.Point(402, 0);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 30);
            this.btnLast.TabIndex = 101;
            this.btnLast.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Custome;
            this.btnLast.Visible = false;
            // 
            // btnNext
            // 
            this.btnNext.AllowFocus = false;
            this.btnNext.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnNext.Appearance.Options.UseForeColor = true;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageLeftPadding = 0;
            this.btnNext.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNext.Location = new System.Drawing.Point(372, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 30);
            this.btnNext.TabIndex = 100;
            this.btnNext.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Custome;
            this.btnNext.Visible = false;
            // 
            // labNumberOfPages
            // 
            this.labNumberOfPages.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labNumberOfPages.Dock = System.Windows.Forms.DockStyle.Left;
            this.labNumberOfPages.Location = new System.Drawing.Point(330, 0);
            this.labNumberOfPages.Name = "labNumberOfPages";
            this.labNumberOfPages.Size = new System.Drawing.Size(42, 30);
            this.labNumberOfPages.TabIndex = 92;
            this.labNumberOfPages.Text = " of 30";
            this.labNumberOfPages.Visible = false;
            // 
            // txtPageNumber
            // 
            this.txtPageNumber.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtPageNumber.EditValue = "1";
            this.txtPageNumber.Location = new System.Drawing.Point(289, 0);
            this.txtPageNumber.Name = "txtPageNumber";
            this.txtPageNumber.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPageNumber.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPageNumber.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtPageNumber.Properties.AutoHeight = false;
            this.txtPageNumber.Properties.Mask.EditMask = "n0";
            this.txtPageNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPageNumber.Size = new System.Drawing.Size(41, 30);
            this.txtPageNumber.TabIndex = 91;
            this.txtPageNumber.Visible = false;
            // 
            // btnPrevius
            // 
            this.btnPrevius.AllowFocus = false;
            this.btnPrevius.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnPrevius.Appearance.Options.UseForeColor = true;
            this.btnPrevius.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevius.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevius.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevius.Image")));
            this.btnPrevius.ImageLeftPadding = 0;
            this.btnPrevius.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnPrevius.Location = new System.Drawing.Point(259, 0);
            this.btnPrevius.Name = "btnPrevius";
            this.btnPrevius.Size = new System.Drawing.Size(30, 30);
            this.btnPrevius.TabIndex = 99;
            this.btnPrevius.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Custome;
            this.btnPrevius.Visible = false;
            // 
            // btnFirst
            // 
            this.btnFirst.AllowFocus = false;
            this.btnFirst.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnFirst.Appearance.Options.UseForeColor = true;
            this.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirst.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.ImageLeftPadding = 0;
            this.btnFirst.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnFirst.Location = new System.Drawing.Point(229, 0);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 30);
            this.btnFirst.TabIndex = 98;
            this.btnFirst.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Custome;
            this.btnFirst.Visible = false;
            // 
            // btnCopy
            // 
            this.btnCopy.AllowFocus = false;
            this.btnCopy.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnCopy.Appearance.Options.UseForeColor = true;
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopy.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageLeftPadding = 0;
            this.btnCopy.Location = new System.Drawing.Point(154, 0);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 30);
            this.btnCopy.TabIndex = 97;
            this.btnCopy.Text = "Copy";
            this.btnCopy.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Custome;
            this.btnCopy.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AllowDrop = true;
            this.btnSave.AllowFocus = false;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(71, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 30);
            this.btnSave.TabIndex = 96;
            this.btnSave.Text = "Save";
            this.btnSave.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Save;
            this.btnSave.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.AllowFocus = false;
            this.btnPrint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnPrint.Appearance.Options.UseForeColor = true;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageLeftPadding = 0;
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(71, 30);
            this.btnPrint.TabIndex = 95;
            this.btnPrint.Text = "Print";
            this.btnPrint.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Print;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.SystemColors.Highlight;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 30);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(975, 1);
            this.panelControl1.TabIndex = 86;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.previewReport);
            this.panelControl2.Controls.Add(this.lbWorkplace);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 31);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(975, 492);
            this.panelControl2.TabIndex = 14;
            // 
            // previewReport
            // 
            this.previewReport.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.previewReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewReport.Font = new System.Drawing.Font("Tahoma", 8F);
            this.previewReport.Location = new System.Drawing.Point(191, 2);
            this.previewReport.Name = "previewReport";
            this.previewReport.PageOffset = new System.Drawing.Point(10, 10);
            this.previewReport.Size = new System.Drawing.Size(782, 488);
            this.previewReport.TabIndex = 2;
            this.previewReport.ToolbarVisible = false;
            this.previewReport.UIStyle = FastReport.Utils.UIStyle.VisualStudio2005;
            // 
            // lbWorkplace
            // 
            this.lbWorkplace.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbWorkplace.Location = new System.Drawing.Point(2, 2);
            this.lbWorkplace.Name = "lbWorkplace";
            this.lbWorkplace.Size = new System.Drawing.Size(189, 488);
            this.lbWorkplace.TabIndex = 3;
            this.lbWorkplace.SelectedIndexChanged += new System.EventHandler(this.listBoxControl1_SelectedIndexChanged);
            // 
            // frmLockedPLanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.pcButtons);
            this.Name = "frmLockedPLanning";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(975, 523);
            this.Load += new System.EventHandler(this.frmLockedPLanning_Load);
            this.Controls.SetChildIndex(this.pcButtons, 0);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcButtons)).EndInit();
            this.pcButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPageNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbWorkplace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pcButtons;
        private Global.CustomControls.MWButton btnLast;
        private Global.CustomControls.MWButton btnNext;
        private DevExpress.XtraEditors.LabelControl labNumberOfPages;
        private DevExpress.XtraEditors.TextEdit txtPageNumber;
        private Global.CustomControls.MWButton btnPrevius;
        private Global.CustomControls.MWButton btnFirst;
        private Global.CustomControls.MWButton btnCopy;
        private Global.CustomControls.MWBottonDropDown btnSave;
        private Global.CustomControls.MWButton btnPrint;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private FastReport.Preview.PreviewControl previewReport;
        private DevExpress.XtraEditors.ListBoxControl lbWorkplace;
    }
}