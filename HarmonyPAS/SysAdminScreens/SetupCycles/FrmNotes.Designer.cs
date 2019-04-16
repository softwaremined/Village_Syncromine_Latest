namespace Mineware.Systems.Production.SysAdminScreens.SetupCycles
{
    partial class FrmNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNotes));
            this.RCVentilation = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.pcReport = new FastReport.Preview.PreviewControl();
            this.label2 = new System.Windows.Forms.Label();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.DateTxt = new System.Windows.Forms.Label();
            this.WPlabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NotesTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RCVentilation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // RCVentilation
            // 
            this.RCVentilation.AllowKeyTips = false;
            this.RCVentilation.AllowMdiChildButtons = false;
            this.RCVentilation.AllowMinimizeRibbon = false;
            this.RCVentilation.AllowTrimPageText = false;
            this.RCVentilation.DrawGroupsBorderMode = DevExpress.Utils.DefaultBoolean.False;
            this.RCVentilation.ExpandCollapseItem.Id = 0;
            this.RCVentilation.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.RCVentilation.ExpandCollapseItem,
            this.barButtonItem1,
            this.btnSave,
            this.btnPrint});
            this.RCVentilation.Location = new System.Drawing.Point(0, 0);
            this.RCVentilation.Margin = new System.Windows.Forms.Padding(4);
            this.RCVentilation.MaxItemId = 8;
            this.RCVentilation.Name = "RCVentilation";
            this.RCVentilation.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.RCVentilation.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCVentilation.ShowCategoryInCaption = false;
            this.RCVentilation.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCVentilation.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCVentilation.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.RCVentilation.ShowToolbarCustomizeItem = false;
            this.RCVentilation.Size = new System.Drawing.Size(1381, 94);
            this.RCVentilation.Toolbar.ShowCustomizeItem = false;
            this.RCVentilation.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.RCVentilation.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "                                         ";
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.btnSave.Id = 5;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.LargeImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.Caption = "Print";
            this.btnPrint.Id = 7;
            this.btnPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.Image")));
            this.btnPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.ImageOptions.LargeImage")));
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnPrint);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // pcReport
            // 
            this.pcReport.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pcReport.Buttons = ((FastReport.PreviewButtons)((FastReport.PreviewButtons.Zoom | FastReport.PreviewButtons.Navigator)));
            this.pcReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcReport.Font = new System.Drawing.Font("Tahoma", 8F);
            this.pcReport.Location = new System.Drawing.Point(0, 94);
            this.pcReport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcReport.Name = "pcReport";
            this.pcReport.PageOffset = new System.Drawing.Point(10, 10);
            this.pcReport.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pcReport.Size = new System.Drawing.Size(1381, 582);
            this.pcReport.TabIndex = 82;
            this.pcReport.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 85;
            this.label2.Text = "Impact";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(235, 9);
            this.radioGroup1.Margin = new System.Windows.Forms.Padding(4);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Low"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Medium"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "High")});
            this.radioGroup1.Size = new System.Drawing.Size(91, 76);
            this.radioGroup1.TabIndex = 84;
            // 
            // radioGroup2
            // 
            this.radioGroup2.Location = new System.Drawing.Point(364, 9);
            this.radioGroup2.Margin = new System.Windows.Forms.Padding(4);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.radioGroup2.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "This Weeks"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Next Weeks")});
            this.radioGroup2.Size = new System.Drawing.Size(105, 76);
            this.radioGroup2.TabIndex = 86;
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(739, 37);
            this.dateTimePicker3.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(232, 23);
            this.dateTimePicker3.TabIndex = 89;
            this.dateTimePicker3.Visible = false;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(492, 63);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(232, 23);
            this.dateTimePicker2.TabIndex = 88;
            this.dateTimePicker2.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(492, 37);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(232, 23);
            this.dateTimePicker1.TabIndex = 87;
            this.dateTimePicker1.Visible = false;
            // 
            // DateTxt
            // 
            this.DateTxt.AutoSize = true;
            this.DateTxt.Location = new System.Drawing.Point(916, 69);
            this.DateTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DateTxt.Name = "DateTxt";
            this.DateTxt.Size = new System.Drawing.Size(42, 17);
            this.DateTxt.TabIndex = 91;
            this.DateTxt.Text = "label2";
            this.DateTxt.Visible = false;
            // 
            // WPlabel
            // 
            this.WPlabel.AutoSize = true;
            this.WPlabel.BackColor = System.Drawing.Color.White;
            this.WPlabel.Location = new System.Drawing.Point(579, 8);
            this.WPlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WPlabel.Name = "WPlabel";
            this.WPlabel.Size = new System.Drawing.Size(42, 17);
            this.WPlabel.TabIndex = 90;
            this.WPlabel.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(993, -1);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 93;
            this.label1.Text = "Notes";
            // 
            // NotesTxt
            // 
            this.NotesTxt.Location = new System.Drawing.Point(993, 8);
            this.NotesTxt.Margin = new System.Windows.Forms.Padding(4);
            this.NotesTxt.MaxLength = 300;
            this.NotesTxt.Multiline = true;
            this.NotesTxt.Name = "NotesTxt";
            this.NotesTxt.Size = new System.Drawing.Size(529, 76);
            this.NotesTxt.TabIndex = 92;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(489, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 95;
            this.label3.Text = "Workplace :";
            // 
            // FrmNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 676);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NotesTxt);
            this.Controls.Add(this.DateTxt);
            this.Controls.Add(this.WPlabel);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.radioGroup2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.pcReport);
            this.Controls.Add(this.RCVentilation);
            this.Name = "FrmNotes";
            this.Text = "Notes";
            this.Load += new System.EventHandler(this.FrmNotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RCVentilation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl RCVentilation;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnPrint;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private FastReport.Preview.PreviewControl pcReport;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.Label DateTxt;
        public System.Windows.Forms.Label WPlabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NotesTxt;
        public System.Windows.Forms.Label label3;
    }
}