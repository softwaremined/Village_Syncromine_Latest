namespace Mineware.Systems.Production.Controls.BookingsABS
{
    partial class frmProblemBookCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProblemBookCode));
            this.RCRockEngineering = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.Savebtn = new DevExpress.XtraBars.BarButtonItem();
            this.RockEnginAddImagebtn = new DevExpress.XtraBars.BarButtonItem();
            this.Closebtn = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.txtProblemID = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProblemID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.lblAcitvity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProblemID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
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
            this.Savebtn,
            this.RockEnginAddImagebtn,
            this.Closebtn});
            this.RCRockEngineering.Location = new System.Drawing.Point(0, 0);
            this.RCRockEngineering.MaxItemId = 9;
            this.RCRockEngineering.Name = "RCRockEngineering";
            this.RCRockEngineering.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.RCRockEngineering.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowCategoryInCaption = false;
            this.RCRockEngineering.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.RCRockEngineering.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.RCRockEngineering.ShowToolbarCustomizeItem = false;
            this.RCRockEngineering.Size = new System.Drawing.Size(378, 75);
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
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.Closebtn);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // txtProblemID
            // 
            this.txtProblemID.Enabled = false;
            this.txtProblemID.Location = new System.Drawing.Point(96, 83);
            this.txtProblemID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtProblemID.MenuManager = this.RCRockEngineering;
            this.txtProblemID.Name = "txtProblemID";
            this.txtProblemID.Size = new System.Drawing.Size(31, 20);
            this.txtProblemID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ProblemID :";
            // 
            // lblProblemID
            // 
            this.lblProblemID.AutoSize = true;
            this.lblProblemID.Location = new System.Drawing.Point(241, 41);
            this.lblProblemID.Name = "lblProblemID";
            this.lblProblemID.Size = new System.Drawing.Size(56, 13);
            this.lblProblemID.TabIndex = 4;
            this.lblProblemID.Text = "ProblemID";
            this.lblProblemID.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Description :";
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(96, 115);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDescription.MenuManager = this.RCRockEngineering;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(272, 20);
            this.txtDescription.TabIndex = 5;
            // 
            // lblAcitvity
            // 
            this.lblAcitvity.AutoSize = true;
            this.lblAcitvity.Location = new System.Drawing.Point(241, 17);
            this.lblAcitvity.Name = "lblAcitvity";
            this.lblAcitvity.Size = new System.Drawing.Size(43, 13);
            this.lblAcitvity.TabIndex = 7;
            this.lblAcitvity.Text = "Acitvity";
            this.lblAcitvity.Visible = false;
            // 
            // frmProblemBookCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 150);
            this.ControlBox = false;
            this.Controls.Add(this.lblAcitvity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblProblemID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProblemID);
            this.Controls.Add(this.RCRockEngineering);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmProblemBookCode";
            this.Text = "Problem Description";
            this.Load += new System.EventHandler(this.frmProblemBookCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProblemID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit txtProblemID;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblProblemID;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtDescription;
        public System.Windows.Forms.Label lblAcitvity;
    }
}