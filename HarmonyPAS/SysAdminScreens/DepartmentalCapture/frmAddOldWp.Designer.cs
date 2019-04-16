namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    partial class frmAddOldWp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddOldWp));
            this.WPAddPnl = new System.Windows.Forms.Panel();
            this.VampwplistBox = new System.Windows.Forms.ListBox();
            this.label132 = new System.Windows.Forms.Label();
            this.WPlinetxt = new System.Windows.Forms.TextBox();
            this.label134 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.NewWpLbl = new System.Windows.Forms.Label();
            this.WPDescNewTxt = new System.Windows.Forms.TextBox();
            this.label138 = new System.Windows.Forms.Label();
            this.WPLevelcmb = new System.Windows.Forms.ComboBox();
            this.label139 = new System.Windows.Forms.Label();
            this.WpIDtxt = new System.Windows.Forms.TextBox();
            this.RCRockEngineering = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.RockEnginSavebtn = new DevExpress.XtraBars.BarButtonItem();
            this.RockEnginAddImagebtn = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.WPAddPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).BeginInit();
            this.SuspendLayout();
            // 
            // WPAddPnl
            // 
            this.WPAddPnl.Controls.Add(this.VampwplistBox);
            this.WPAddPnl.Controls.Add(this.label132);
            this.WPAddPnl.Controls.Add(this.WPlinetxt);
            this.WPAddPnl.Controls.Add(this.label134);
            this.WPAddPnl.Controls.Add(this.textEdit1);
            this.WPAddPnl.Controls.Add(this.NewWpLbl);
            this.WPAddPnl.Controls.Add(this.WPDescNewTxt);
            this.WPAddPnl.Controls.Add(this.label138);
            this.WPAddPnl.Controls.Add(this.WPLevelcmb);
            this.WPAddPnl.Controls.Add(this.label139);
            this.WPAddPnl.Controls.Add(this.WpIDtxt);
            this.WPAddPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WPAddPnl.Location = new System.Drawing.Point(0, 75);
            this.WPAddPnl.Name = "WPAddPnl";
            this.WPAddPnl.Size = new System.Drawing.Size(279, 398);
            this.WPAddPnl.TabIndex = 62;
            // 
            // VampwplistBox
            // 
            this.VampwplistBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.VampwplistBox.FormattingEnabled = true;
            this.VampwplistBox.Location = new System.Drawing.Point(22, 64);
            this.VampwplistBox.Name = "VampwplistBox";
            this.VampwplistBox.Size = new System.Drawing.Size(229, 316);
            this.VampwplistBox.TabIndex = 43;
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(55, 258);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(26, 13);
            this.label132.TabIndex = 42;
            this.label132.Text = "Line";
            this.label132.Visible = false;
            // 
            // WPlinetxt
            // 
            this.WPlinetxt.BackColor = System.Drawing.Color.White;
            this.WPlinetxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.WPlinetxt.Location = new System.Drawing.Point(48, 274);
            this.WPlinetxt.MaxLength = 50;
            this.WPlinetxt.Name = "WPlinetxt";
            this.WPlinetxt.Size = new System.Drawing.Size(260, 21);
            this.WPlinetxt.TabIndex = 41;
            this.WPlinetxt.Visible = false;
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(51, 416);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(52, 13);
            this.label134.TabIndex = 37;
            this.label134.Text = "Distance:";
            this.label134.Visible = false;
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "0.0";
            this.textEdit1.Location = new System.Drawing.Point(51, 435);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEdit1.Properties.Mask.EditMask = "n1";
            this.textEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textEdit1.Size = new System.Drawing.Size(73, 20);
            this.textEdit1.TabIndex = 36;
            this.textEdit1.Visible = false;
            // 
            // NewWpLbl
            // 
            this.NewWpLbl.AutoSize = true;
            this.NewWpLbl.Location = new System.Drawing.Point(279, 149);
            this.NewWpLbl.Name = "NewWpLbl";
            this.NewWpLbl.Size = new System.Drawing.Size(113, 13);
            this.NewWpLbl.TabIndex = 7;
            this.NewWpLbl.Text = "Workplace Description";
            this.NewWpLbl.Visible = false;
            // 
            // WPDescNewTxt
            // 
            this.WPDescNewTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.WPDescNewTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.WPDescNewTxt.Location = new System.Drawing.Point(267, 217);
            this.WPDescNewTxt.Name = "WPDescNewTxt";
            this.WPDescNewTxt.ReadOnly = true;
            this.WPDescNewTxt.Size = new System.Drawing.Size(260, 21);
            this.WPDescNewTxt.TabIndex = 6;
            this.WPDescNewTxt.Visible = false;
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(87, 224);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(32, 13);
            this.label138.TabIndex = 5;
            this.label138.Text = "Level";
            this.label138.Visible = false;
            // 
            // WPLevelcmb
            // 
            this.WPLevelcmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WPLevelcmb.FormattingEnabled = true;
            this.WPLevelcmb.Location = new System.Drawing.Point(44, 246);
            this.WPLevelcmb.Name = "WPLevelcmb";
            this.WPLevelcmb.Size = new System.Drawing.Size(247, 21);
            this.WPLevelcmb.TabIndex = 4;
            this.WPLevelcmb.Visible = false;
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Location = new System.Drawing.Point(16, 18);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(70, 13);
            this.label139.TabIndex = 3;
            this.label139.Text = "Workplace Id";
            // 
            // WpIDtxt
            // 
            this.WpIDtxt.BackColor = System.Drawing.Color.White;
            this.WpIDtxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.WpIDtxt.Location = new System.Drawing.Point(19, 37);
            this.WpIDtxt.Name = "WpIDtxt";
            this.WpIDtxt.Size = new System.Drawing.Size(162, 21);
            this.WpIDtxt.TabIndex = 2;
            // 
            // RCRockEngineering
            // 
            this.RCRockEngineering.AllowKeyTips = false;
            this.RCRockEngineering.AllowMdiChildButtons = false;
            this.RCRockEngineering.AllowMinimizeRibbon = false;
            this.RCRockEngineering.AllowTrimPageText = false;
            this.RCRockEngineering.DrawGroupsBorderMode = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ExpandCollapseItem.Id = 0;
            this.RCRockEngineering.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.RCRockEngineering.ExpandCollapseItem,
            this.barButtonItem1,
            this.RockEnginSavebtn,
            this.RockEnginAddImagebtn});
            this.RCRockEngineering.Location = new System.Drawing.Point(0, 0);
            this.RCRockEngineering.MaxItemId = 8;
            this.RCRockEngineering.Name = "RCRockEngineering";
            this.RCRockEngineering.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.RCRockEngineering.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowCategoryInCaption = false;
            this.RCRockEngineering.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.RCRockEngineering.ShowToolbarCustomizeItem = false;
            this.RCRockEngineering.Size = new System.Drawing.Size(279, 75);
            this.RCRockEngineering.Toolbar.ShowCustomizeItem = false;
            this.RCRockEngineering.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.RCRockEngineering.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "                                         ";
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // RockEnginSavebtn
            // 
            this.RockEnginSavebtn.Caption = "Save";
            this.RockEnginSavebtn.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.RockEnginSavebtn.Id = 5;
            this.RockEnginSavebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RockEnginSavebtn.ImageOptions.Image")));
            this.RockEnginSavebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RockEnginSavebtn.ImageOptions.LargeImage")));
            this.RockEnginSavebtn.Name = "RockEnginSavebtn";
            this.RockEnginSavebtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RockEnginSavebtn_ItemClick);
            // 
            // RockEnginAddImagebtn
            // 
            this.RockEnginAddImagebtn.Caption = "Add Image";
            this.RockEnginAddImagebtn.Id = 7;
            this.RockEnginAddImagebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RockEnginAddImagebtn.ImageOptions.Image")));
            this.RockEnginAddImagebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RockEnginAddImagebtn.ImageOptions.LargeImage")));
            this.RockEnginAddImagebtn.Name = "RockEnginAddImagebtn";
            this.RockEnginAddImagebtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            this.ribbonPageGroup1.ItemLinks.Add(this.RockEnginSavebtn);
            this.ribbonPageGroup1.ItemLinks.Add(this.RockEnginAddImagebtn);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // frmAddOldWp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 473);
            this.Controls.Add(this.WPAddPnl);
            this.Controls.Add(this.RCRockEngineering);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmAddOldWp";
            this.Text = "Add Old Workplace";
            this.Load += new System.EventHandler(this.frmAddOldWp_Load);
            this.WPAddPnl.ResumeLayout(false);
            this.WPAddPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel WPAddPnl;
        private System.Windows.Forms.ListBox VampwplistBox;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.TextBox WPlinetxt;
        private System.Windows.Forms.Label label134;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private System.Windows.Forms.Label NewWpLbl;
        private System.Windows.Forms.TextBox WPDescNewTxt;
        private System.Windows.Forms.Label label138;
        public System.Windows.Forms.ComboBox WPLevelcmb;
        private System.Windows.Forms.Label label139;
        public System.Windows.Forms.TextBox WpIDtxt;
        private DevExpress.XtraBars.Ribbon.RibbonControl RCRockEngineering;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem RockEnginSavebtn;
        private DevExpress.XtraBars.BarButtonItem RockEnginAddImagebtn;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    }
}