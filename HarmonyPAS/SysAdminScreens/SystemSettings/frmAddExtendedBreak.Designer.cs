namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    partial class frmAddExtendedBreak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddExtendedBreak));
            this.RCRockEngineering = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.RockEnginAddImagebtn = new DevExpress.XtraBars.BarButtonItem();
            this.Closebtn = new DevExpress.XtraBars.BarButtonItem();
            this.WorkplaceEdit = new DevExpress.XtraBars.BarEditItem();
            this.SectionEdit = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.InitiateDateEdit = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.DefDaysTxt = new DevExpress.XtraEditors.TextEdit();
            this.StopNoteCB = new System.Windows.Forms.CheckBox();
            this.label197 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label196 = new System.Windows.Forms.Label();
            this.DescTxt = new System.Windows.Forms.TextBox();
            this.label195 = new System.Windows.Forms.Label();
            this.EndDateDTP = new System.Windows.Forms.DateTimePicker();
            this.StartDateDTP = new System.Windows.Forms.DateTimePicker();
            this.label198 = new System.Windows.Forms.Label();
            this.label194 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefDaysTxt.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.btnSave,
            this.RockEnginAddImagebtn,
            this.Closebtn,
            this.WorkplaceEdit,
            this.SectionEdit,
            this.InitiateDateEdit});
            this.RCRockEngineering.Location = new System.Drawing.Point(0, 0);
            this.RCRockEngineering.MaxItemId = 12;
            this.RCRockEngineering.Name = "RCRockEngineering";
            this.RCRockEngineering.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.RCRockEngineering.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            this.RCRockEngineering.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowCategoryInCaption = false;
            this.RCRockEngineering.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.RCRockEngineering.ShowToolbarCustomizeItem = false;
            this.RCRockEngineering.Size = new System.Drawing.Size(742, 75);
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
            this.WorkplaceEdit.Edit = null;
            this.WorkplaceEdit.EditWidth = 250;
            this.WorkplaceEdit.Id = 9;
            this.WorkplaceEdit.Name = "WorkplaceEdit";
            // 
            // SectionEdit
            // 
            this.SectionEdit.Caption = "Section          ";
            this.SectionEdit.CaptionAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.SectionEdit.Edit = this.repositoryItemTextEdit1;
            this.SectionEdit.EditWidth = 100;
            this.SectionEdit.Id = 10;
            this.SectionEdit.Name = "SectionEdit";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // InitiateDateEdit
            // 
            this.InitiateDateEdit.Caption = "Initiated Date";
            this.InitiateDateEdit.CaptionAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.InitiateDateEdit.Edit = this.repositoryItemTextEdit2;
            this.InitiateDateEdit.EditWidth = 100;
            this.InitiateDateEdit.Id = 11;
            this.InitiateDateEdit.Name = "InitiateDateEdit";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.Closebtn);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.SectionEdit);
            this.ribbonPageGroup2.ItemLinks.Add(this.InitiateDateEdit);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // DefDaysTxt
            // 
            this.DefDaysTxt.EditValue = "3";
            this.DefDaysTxt.Location = new System.Drawing.Point(134, 107);
            this.DefDaysTxt.Name = "DefDaysTxt";
            this.DefDaysTxt.Properties.Mask.EditMask = "d";
            this.DefDaysTxt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.DefDaysTxt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DefDaysTxt.Size = new System.Drawing.Size(42, 20);
            this.DefDaysTxt.TabIndex = 202;
            this.DefDaysTxt.TextChanged += new System.EventHandler(this.DefDaysTxt_TextChanged);
            // 
            // StopNoteCB
            // 
            this.StopNoteCB.AutoSize = true;
            this.StopNoteCB.Checked = true;
            this.StopNoteCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StopNoteCB.Location = new System.Drawing.Point(456, 317);
            this.StopNoteCB.Name = "StopNoteCB";
            this.StopNoteCB.Size = new System.Drawing.Size(120, 17);
            this.StopNoteCB.TabIndex = 201;
            this.StopNoteCB.Text = "Stop Note Required";
            this.StopNoteCB.UseVisualStyleBackColor = true;
            // 
            // label197
            // 
            this.label197.AutoSize = true;
            this.label197.BackColor = System.Drawing.Color.Transparent;
            this.label197.ForeColor = System.Drawing.Color.Black;
            this.label197.Location = new System.Drawing.Point(66, 173);
            this.label197.Name = "label197";
            this.label197.Size = new System.Drawing.Size(45, 13);
            this.label197.TabIndex = 199;
            this.label197.Text = "Section ";
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.White;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.HideSelection = false;
            this.treeView1.Indent = 15;
            this.treeView1.Location = new System.Drawing.Point(69, 189);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(266, 235);
            this.treeView1.TabIndex = 200;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label196
            // 
            this.label196.AutoSize = true;
            this.label196.Location = new System.Drawing.Point(65, 110);
            this.label196.Name = "label196";
            this.label196.Size = new System.Drawing.Size(76, 13);
            this.label196.TabIndex = 198;
            this.label196.Text = "Default Days: ";
            // 
            // DescTxt
            // 
            this.DescTxt.Location = new System.Drawing.Point(456, 189);
            this.DescTxt.MaxLength = 12;
            this.DescTxt.Multiline = true;
            this.DescTxt.Name = "DescTxt";
            this.DescTxt.Size = new System.Drawing.Size(217, 92);
            this.DescTxt.TabIndex = 197;
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.Location = new System.Drawing.Point(453, 173);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(67, 13);
            this.label195.TabIndex = 196;
            this.label195.Text = "Description: ";
            // 
            // EndDateDTP
            // 
            this.EndDateDTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDateDTP.Location = new System.Drawing.Point(456, 107);
            this.EndDateDTP.Name = "EndDateDTP";
            this.EndDateDTP.Size = new System.Drawing.Size(143, 21);
            this.EndDateDTP.TabIndex = 195;
            this.EndDateDTP.ValueChanged += new System.EventHandler(this.EndDateDTP_ValueChanged);
            // 
            // StartDateDTP
            // 
            this.StartDateDTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartDateDTP.Location = new System.Drawing.Point(237, 107);
            this.StartDateDTP.Name = "StartDateDTP";
            this.StartDateDTP.Size = new System.Drawing.Size(143, 21);
            this.StartDateDTP.TabIndex = 194;
            this.StartDateDTP.ValueChanged += new System.EventHandler(this.StartDateDTP_ValueChanged);
            // 
            // label198
            // 
            this.label198.AutoSize = true;
            this.label198.Location = new System.Drawing.Point(456, 93);
            this.label198.Name = "label198";
            this.label198.Size = new System.Drawing.Size(62, 13);
            this.label198.TabIndex = 193;
            this.label198.Text = "Stop Date: ";
            // 
            // label194
            // 
            this.label194.AutoSize = true;
            this.label194.Location = new System.Drawing.Point(234, 93);
            this.label194.Name = "label194";
            this.label194.Size = new System.Drawing.Size(61, 13);
            this.label194.TabIndex = 203;
            this.label194.Text = "Start Date:";
            // 
            // frmAddExtendedBreak
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 459);
            this.ControlBox = false;
            this.Controls.Add(this.label194);
            this.Controls.Add(this.DefDaysTxt);
            this.Controls.Add(this.StopNoteCB);
            this.Controls.Add(this.label197);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label196);
            this.Controls.Add(this.DescTxt);
            this.Controls.Add(this.label195);
            this.Controls.Add(this.EndDateDTP);
            this.Controls.Add(this.StartDateDTP);
            this.Controls.Add(this.label198);
            this.Controls.Add(this.RCRockEngineering);
            this.Name = "frmAddExtendedBreak";
            this.Text = "Add Extended Break";
            this.Load += new System.EventHandler(this.frmAddExtendedBreak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefDaysTxt.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl RCRockEngineering;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem RockEnginAddImagebtn;
        private DevExpress.XtraBars.BarButtonItem Closebtn;
        private DevExpress.XtraBars.BarEditItem WorkplaceEdit;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarEditItem SectionEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarEditItem InitiateDateEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.TextEdit DefDaysTxt;
        private System.Windows.Forms.CheckBox StopNoteCB;
        private System.Windows.Forms.Label label197;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label196;
        public System.Windows.Forms.TextBox DescTxt;
        private System.Windows.Forms.Label label195;
        public System.Windows.Forms.DateTimePicker EndDateDTP;
        public System.Windows.Forms.DateTimePicker StartDateDTP;
        private System.Windows.Forms.Label label198;
        private System.Windows.Forms.Label label194;
    }
}