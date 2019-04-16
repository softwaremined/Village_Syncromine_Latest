namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    partial class frmVampingProp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVampingProp));
            this.RCRockEngineering = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.Savebtn = new DevExpress.XtraBars.BarButtonItem();
            this.RockEnginAddImagebtn = new DevExpress.XtraBars.BarButtonItem();
            this.Closebtn = new DevExpress.XtraBars.BarButtonItem();
            this.WorkplaceEdit = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.lblsection = new System.Windows.Forms.Label();
            this.lblprodmonth = new System.Windows.Forms.Label();
            this.lbxMiners = new System.Windows.Forms.ListBox();
            this.lbxGang = new System.Windows.Forms.ListBox();
            this.lbxBoxHole = new System.Windows.Forms.ListBox();
            this.label17 = new System.Windows.Forms.Label();
            this.TotWpFilterTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GangFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BoxholeFilter = new System.Windows.Forms.TextBox();
            this.lblWorkplace = new System.Windows.Forms.Label();
            this.lblSaved = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // RCRockEngineering
            // 
            this.RCRockEngineering.AllowKeyTips = false;
            this.RCRockEngineering.AllowMdiChildButtons = false;
            this.RCRockEngineering.AllowMinimizeRibbon = false;
            this.RCRockEngineering.AllowTrimPageText = false;
            this.RCRockEngineering.ExpandCollapseItem.Id = 0;
            this.RCRockEngineering.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.RCRockEngineering.ExpandCollapseItem,
            this.barButtonItem1,
            this.Savebtn,
            this.RockEnginAddImagebtn,
            this.Closebtn,
            this.WorkplaceEdit});
            this.RCRockEngineering.Location = new System.Drawing.Point(0, 0);
            this.RCRockEngineering.MaxItemId = 10;
            this.RCRockEngineering.Name = "RCRockEngineering";
            this.RCRockEngineering.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.RCRockEngineering.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.RCRockEngineering.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowCategoryInCaption = false;
            this.RCRockEngineering.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.RCRockEngineering.ShowToolbarCustomizeItem = false;
            this.RCRockEngineering.Size = new System.Drawing.Size(893, 95);
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
            // Savebtn
            // 
            this.Savebtn.Caption = "Save";
            this.Savebtn.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.Savebtn.Id = 5;
            this.Savebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Savebtn.ImageOptions.Image")));
            this.Savebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Savebtn.ImageOptions.LargeImage")));
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Savebtn_ItemClick);
            // 
            // RockEnginAddImagebtn
            // 
            this.RockEnginAddImagebtn.Caption = "Add Image";
            this.RockEnginAddImagebtn.Id = 7;
            this.RockEnginAddImagebtn.Name = "RockEnginAddImagebtn";
            this.RockEnginAddImagebtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // Closebtn
            // 
            this.Closebtn.Caption = "Close";
            this.Closebtn.Id = 8;
            this.Closebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Closebtn.ImageOptions.Image")));
            this.Closebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("Closebtn.ImageOptions.LargeImage")));
            this.Closebtn.Name = "Closebtn";
            this.Closebtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Closebtn_ItemClick);
            // 
            // WorkplaceEdit
            // 
            this.WorkplaceEdit.Caption = "Workplace";
            this.WorkplaceEdit.Edit = this.repositoryItemTextEdit1;
            this.WorkplaceEdit.EditWidth = 250;
            this.WorkplaceEdit.Enabled = false;
            this.WorkplaceEdit.Id = 9;
            this.WorkplaceEdit.Name = "WorkplaceEdit";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.WorkplaceEdit);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Info";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.Savebtn);
            this.ribbonPageGroup1.ItemLinks.Add(this.Closebtn);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Options";
            // 
            // lblsection
            // 
            this.lblsection.AutoSize = true;
            this.lblsection.Location = new System.Drawing.Point(443, 15);
            this.lblsection.Name = "lblsection";
            this.lblsection.Size = new System.Drawing.Size(27, 13);
            this.lblsection.TabIndex = 2;
            this.lblsection.Text = "sect";
            this.lblsection.Visible = false;
            // 
            // lblprodmonth
            // 
            this.lblprodmonth.AutoSize = true;
            this.lblprodmonth.Location = new System.Drawing.Point(443, 42);
            this.lblprodmonth.Name = "lblprodmonth";
            this.lblprodmonth.Size = new System.Drawing.Size(59, 13);
            this.lblprodmonth.TabIndex = 3;
            this.lblprodmonth.Text = "Prodmonth";
            this.lblprodmonth.Visible = false;
            // 
            // lbxMiners
            // 
            this.lbxMiners.FormattingEnabled = true;
            this.lbxMiners.Location = new System.Drawing.Point(15, 147);
            this.lbxMiners.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxMiners.Name = "lbxMiners";
            this.lbxMiners.Size = new System.Drawing.Size(270, 147);
            this.lbxMiners.TabIndex = 5;
            // 
            // lbxGang
            // 
            this.lbxGang.FormattingEnabled = true;
            this.lbxGang.Location = new System.Drawing.Point(307, 147);
            this.lbxGang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxGang.Name = "lbxGang";
            this.lbxGang.Size = new System.Drawing.Size(270, 147);
            this.lbxGang.TabIndex = 6;
            // 
            // lbxBoxHole
            // 
            this.lbxBoxHole.FormattingEnabled = true;
            this.lbxBoxHole.Location = new System.Drawing.Point(607, 147);
            this.lbxBoxHole.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxBoxHole.Name = "lbxBoxHole";
            this.lbxBoxHole.Size = new System.Drawing.Size(270, 147);
            this.lbxBoxHole.TabIndex = 7;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(12, 104);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 13);
            this.label17.TabIndex = 89;
            this.label17.Text = "Miner";
            // 
            // TotWpFilterTxt
            // 
            this.TotWpFilterTxt.Location = new System.Drawing.Point(15, 122);
            this.TotWpFilterTxt.Name = "TotWpFilterTxt";
            this.TotWpFilterTxt.Size = new System.Drawing.Size(188, 21);
            this.TotWpFilterTxt.TabIndex = 88;
            this.TotWpFilterTxt.TextChanged += new System.EventHandler(this.TotWpFilterTxt_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(305, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 91;
            this.label4.Text = "Gang";
            // 
            // GangFilter
            // 
            this.GangFilter.Location = new System.Drawing.Point(307, 122);
            this.GangFilter.Name = "GangFilter";
            this.GangFilter.Size = new System.Drawing.Size(188, 21);
            this.GangFilter.TabIndex = 90;
            this.GangFilter.TextChanged += new System.EventHandler(this.GangFilter_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(604, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 93;
            this.label5.Text = "BoxHole";
            // 
            // BoxholeFilter
            // 
            this.BoxholeFilter.Location = new System.Drawing.Point(607, 122);
            this.BoxholeFilter.Name = "BoxholeFilter";
            this.BoxholeFilter.Size = new System.Drawing.Size(188, 21);
            this.BoxholeFilter.TabIndex = 92;
            this.BoxholeFilter.TextChanged += new System.EventHandler(this.BoxholeFilter_TextChanged);
            // 
            // lblWorkplace
            // 
            this.lblWorkplace.AutoSize = true;
            this.lblWorkplace.Location = new System.Drawing.Point(549, 35);
            this.lblWorkplace.Name = "lblWorkplace";
            this.lblWorkplace.Size = new System.Drawing.Size(55, 13);
            this.lblWorkplace.TabIndex = 4;
            this.lblWorkplace.Text = "workplace";
            this.lblWorkplace.Visible = false;
            this.lblWorkplace.TextChanged += new System.EventHandler(this.lblWorkplace_TextChanged);
            // 
            // lblSaved
            // 
            this.lblSaved.AutoSize = true;
            this.lblSaved.Location = new System.Drawing.Point(704, 35);
            this.lblSaved.Name = "lblSaved";
            this.lblSaved.Size = new System.Drawing.Size(14, 13);
            this.lblSaved.TabIndex = 95;
            this.lblSaved.Text = "N";
            this.lblSaved.Visible = false;
            // 
            // frmVampingProp
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 306);
            this.ControlBox = false;
            this.Controls.Add(this.lblSaved);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BoxholeFilter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.GangFilter);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.TotWpFilterTxt);
            this.Controls.Add(this.lbxBoxHole);
            this.Controls.Add(this.lbxGang);
            this.Controls.Add(this.lbxMiners);
            this.Controls.Add(this.lblWorkplace);
            this.Controls.Add(this.lblprodmonth);
            this.Controls.Add(this.lblsection);
            this.Controls.Add(this.RCRockEngineering);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmVampingProp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vamping Planning";
            this.Load += new System.EventHandler(this.frmVampingProp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl RCRockEngineering;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem Savebtn;
        private DevExpress.XtraBars.BarButtonItem RockEnginAddImagebtn;
        private DevExpress.XtraBars.BarButtonItem Closebtn;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox TotWpFilterTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox GangFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox BoxholeFilter;
        public System.Windows.Forms.Label lblsection;
        public System.Windows.Forms.Label lblprodmonth;
        private DevExpress.XtraBars.BarEditItem WorkplaceEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        public System.Windows.Forms.Label lblWorkplace;
        public System.Windows.Forms.ListBox lbxMiners;
        public System.Windows.Forms.ListBox lbxGang;
        public System.Windows.Forms.ListBox lbxBoxHole;
        public System.Windows.Forms.Label lblSaved;
    }
}