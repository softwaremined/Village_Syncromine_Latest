namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    partial class frmGeologyProp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeologyProp));
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Delaytxt = new System.Windows.Forms.NumericUpDown();
            this.PevHolelabel = new System.Windows.Forms.Label();
            this.Starttm = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Holelabel = new System.Windows.Forms.Label();
            this.WPlabel = new System.Windows.Forms.Label();
            this.PrevWPList = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Machlabel = new System.Windows.Forms.Label();
            this.ribbonControl2 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem5 = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem6 = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem7 = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem8 = new DevExpress.XtraBars.BarEditItem();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.Delaytxt)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(413, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 53;
            this.label7.Text = "(Working Days)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(318, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 52;
            this.label6.Text = "Transport Days";
            // 
            // Delaytxt
            // 
            this.Delaytxt.Location = new System.Drawing.Point(358, 250);
            this.Delaytxt.Name = "Delaytxt";
            this.Delaytxt.Size = new System.Drawing.Size(49, 21);
            this.Delaytxt.TabIndex = 51;
            // 
            // PevHolelabel
            // 
            this.PevHolelabel.AutoSize = true;
            this.PevHolelabel.Location = new System.Drawing.Point(308, 79);
            this.PevHolelabel.Name = "PevHolelabel";
            this.PevHolelabel.Size = new System.Drawing.Size(125, 13);
            this.PevHolelabel.TabIndex = 50;
            this.PevHolelabel.Text = "Machine Commencement";
            this.PevHolelabel.Visible = false;
            this.PevHolelabel.TextChanged += new System.EventHandler(this.PevHolelabel_TextChanged);
            // 
            // Starttm
            // 
            this.Starttm.CustomFormat = "dd MMM yyyy";
            this.Starttm.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.Starttm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Starttm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Starttm.Location = new System.Drawing.Point(295, 168);
            this.Starttm.Name = "Starttm";
            this.Starttm.Size = new System.Drawing.Size(124, 20);
            this.Starttm.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Available Date (Before Move)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Preceding  Hole";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(111, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Hole";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(64, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Workplace";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Machine";
            // 
            // Holelabel
            // 
            this.Holelabel.AutoSize = true;
            this.Holelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Holelabel.ForeColor = System.Drawing.Color.Blue;
            this.Holelabel.Location = new System.Drawing.Point(168, 72);
            this.Holelabel.Name = "Holelabel";
            this.Holelabel.Size = new System.Drawing.Size(35, 13);
            this.Holelabel.TabIndex = 27;
            this.Holelabel.Text = "label2";
            // 
            // WPlabel
            // 
            this.WPlabel.AutoSize = true;
            this.WPlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WPlabel.ForeColor = System.Drawing.Color.Blue;
            this.WPlabel.Location = new System.Drawing.Point(138, 42);
            this.WPlabel.Name = "WPlabel";
            this.WPlabel.Size = new System.Drawing.Size(35, 13);
            this.WPlabel.TabIndex = 1;
            this.WPlabel.Text = "label2";
            // 
            // PrevWPList
            // 
            this.PrevWPList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.PrevWPList.FormattingEnabled = true;
            this.PrevWPList.Location = new System.Drawing.Point(32, 140);
            this.PrevWPList.Name = "PrevWPList";
            this.PrevWPList.Size = new System.Drawing.Size(213, 121);
            this.PrevWPList.TabIndex = 3;
            this.PrevWPList.SelectedIndexChanged += new System.EventHandler(this.PrevWPList_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.Delaytxt);
            this.panel2.Controls.Add(this.PevHolelabel);
            this.panel2.Controls.Add(this.Starttm);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Holelabel);
            this.panel2.Controls.Add(this.Machlabel);
            this.panel2.Controls.Add(this.WPlabel);
            this.panel2.Controls.Add(this.PrevWPList);
            this.panel2.Location = new System.Drawing.Point(28, 91);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(521, 292);
            this.panel2.TabIndex = 28;
            // 
            // Machlabel
            // 
            this.Machlabel.AutoSize = true;
            this.Machlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Machlabel.ForeColor = System.Drawing.Color.Blue;
            this.Machlabel.Location = new System.Drawing.Point(78, 19);
            this.Machlabel.Name = "Machlabel";
            this.Machlabel.Size = new System.Drawing.Size(35, 13);
            this.Machlabel.TabIndex = 0;
            this.Machlabel.Text = "label1";
            // 
            // ribbonControl2
            // 
            this.ribbonControl2.AllowKeyTips = false;
            this.ribbonControl2.AllowMdiChildButtons = false;
            this.ribbonControl2.AllowMinimizeRibbon = false;
            this.ribbonControl2.AllowTrimPageText = false;
            this.ribbonControl2.DrawGroupsBorderMode = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl2.ExpandCollapseItem.Id = 0;
            this.ribbonControl2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl2.ExpandCollapseItem,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barEditItem5,
            this.barEditItem6,
            this.barEditItem7,
            this.barEditItem8});
            this.ribbonControl2.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl2.MaxItemId = 13;
            this.ribbonControl2.Name = "ribbonControl2";
            this.ribbonControl2.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage2});
            this.ribbonControl2.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl2.ShowCategoryInCaption = false;
            this.ribbonControl2.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl2.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl2.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl2.ShowToolbarCustomizeItem = false;
            this.ribbonControl2.Size = new System.Drawing.Size(615, 75);
            this.ribbonControl2.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl2.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl2.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "                                         ";
            this.barButtonItem3.Id = 3;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Save";
            this.barButtonItem4.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem4.Id = 5;
            this.barButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Add Image";
            this.barButtonItem5.Id = 7;
            this.barButtonItem5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.Image")));
            this.barButtonItem5.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.LargeImage")));
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Print Grid";
            this.barButtonItem6.Id = 8;
            this.barButtonItem6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.ImageOptions.Image")));
            this.barButtonItem6.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.ImageOptions.LargeImage")));
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barEditItem5
            // 
            this.barEditItem5.Caption = "                                                                                 " +
    "            ";
            this.barEditItem5.Edit = null;
            this.barEditItem5.EditWidth = 1;
            this.barEditItem5.Id = 9;
            this.barEditItem5.Name = "barEditItem5";
            // 
            // barEditItem6
            // 
            this.barEditItem6.Caption = "                                                                                 " +
    "         ";
            this.barEditItem6.Edit = null;
            this.barEditItem6.EditWidth = 1;
            this.barEditItem6.Id = 10;
            this.barEditItem6.Name = "barEditItem6";
            // 
            // barEditItem7
            // 
            this.barEditItem7.Caption = "                                                                           ";
            this.barEditItem7.Edit = null;
            this.barEditItem7.EditWidth = 1;
            this.barEditItem7.Id = 11;
            this.barEditItem7.Name = "barEditItem7";
            // 
            // barEditItem8
            // 
            this.barEditItem8.Caption = "                                                                      ";
            this.barEditItem8.Edit = null;
            this.barEditItem8.EditWidth = 1;
            this.barEditItem8.Id = 12;
            this.barEditItem8.Name = "barEditItem8";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage1";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItem4);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Options";
            // 
            // frmGeologyProp
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 406);
            this.Controls.Add(this.ribbonControl2);
            this.Controls.Add(this.panel2);
            this.Name = "frmGeologyProp";
            this.ShowIcon = false;
            this.Text = "GeoScience - Setup";
            this.Load += new System.EventHandler(this.frmGeologyProp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Delaytxt)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown Delaytxt;
        private System.Windows.Forms.Label PevHolelabel;
        public System.Windows.Forms.DateTimePicker Starttm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label Holelabel;
        public System.Windows.Forms.Label WPlabel;
        private System.Windows.Forms.ListBox PrevWPList;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label Machlabel;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarEditItem barEditItem5;
        private DevExpress.XtraBars.BarEditItem barEditItem6;
        private DevExpress.XtraBars.BarEditItem barEditItem7;
        private DevExpress.XtraBars.BarEditItem barEditItem8;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
    }
}