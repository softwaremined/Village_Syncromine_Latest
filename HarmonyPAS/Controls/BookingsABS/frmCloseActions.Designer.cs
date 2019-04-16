namespace Mineware.Systems.Production.Controls.BookingsABS
{
    partial class frmCloseActions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCloseActions));
            this.RCRockEngineering = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.Savebtn = new DevExpress.XtraBars.BarButtonItem();
            this.RockEnginAddImagebtn = new DevExpress.XtraBars.BarButtonItem();
            this.Closebtn = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.Notestxt = new System.Windows.Forms.TextBox();
            this.lblActionID = new System.Windows.Forms.Label();
            this.cbxResolved = new System.Windows.Forms.CheckBox();
            this.lblActionType = new System.Windows.Forms.Label();
            this.lblWorkplace = new System.Windows.Forms.Label();
            this.lblPivotActionID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).BeginInit();
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
            this.RCRockEngineering.Margin = new System.Windows.Forms.Padding(4);
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
            this.RCRockEngineering.Size = new System.Drawing.Size(491, 94);
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
            this.RockEnginAddImagebtn.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("RockEnginAddImagebtn.ImageOptions.Image")));
            this.RockEnginAddImagebtn.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("RockEnginAddImagebtn.ImageOptions.LargeImage")));
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
            this.ribbonPageGroup1.ItemLinks.Add(this.Savebtn);
            this.ribbonPageGroup1.ItemLinks.Add(this.Closebtn);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // Notestxt
            // 
            this.Notestxt.Location = new System.Drawing.Point(12, 102);
            this.Notestxt.Multiline = true;
            this.Notestxt.Name = "Notestxt";
            this.Notestxt.Size = new System.Drawing.Size(471, 215);
            this.Notestxt.TabIndex = 1;
            // 
            // lblActionID
            // 
            this.lblActionID.AutoSize = true;
            this.lblActionID.Location = new System.Drawing.Point(150, 9);
            this.lblActionID.Name = "lblActionID";
            this.lblActionID.Size = new System.Drawing.Size(42, 17);
            this.lblActionID.TabIndex = 2;
            this.lblActionID.Text = "label1";
            this.lblActionID.Visible = false;
            // 
            // cbxResolved
            // 
            this.cbxResolved.AutoSize = true;
            this.cbxResolved.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbxResolved.Location = new System.Drawing.Point(393, 323);
            this.cbxResolved.Name = "cbxResolved";
            this.cbxResolved.Size = new System.Drawing.Size(85, 21);
            this.cbxResolved.TabIndex = 4;
            this.cbxResolved.Text = "Resolved";
            this.cbxResolved.UseVisualStyleBackColor = true;
            // 
            // lblActionType
            // 
            this.lblActionType.AutoSize = true;
            this.lblActionType.Location = new System.Drawing.Point(379, 59);
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Size = new System.Drawing.Size(77, 17);
            this.lblActionType.TabIndex = 6;
            this.lblActionType.Text = "ActionType";
            this.lblActionType.Visible = false;
            // 
            // lblWorkplace
            // 
            this.lblWorkplace.AutoSize = true;
            this.lblWorkplace.Location = new System.Drawing.Point(379, 35);
            this.lblWorkplace.Name = "lblWorkplace";
            this.lblWorkplace.Size = new System.Drawing.Size(73, 17);
            this.lblWorkplace.TabIndex = 7;
            this.lblWorkplace.Text = "Workplace";
            this.lblWorkplace.Visible = false;
            // 
            // lblPivotActionID
            // 
            this.lblPivotActionID.AutoSize = true;
            this.lblPivotActionID.Location = new System.Drawing.Point(379, 9);
            this.lblPivotActionID.Name = "lblPivotActionID";
            this.lblPivotActionID.Size = new System.Drawing.Size(91, 17);
            this.lblPivotActionID.TabIndex = 8;
            this.lblPivotActionID.Text = "PivotActionID";
            this.lblPivotActionID.Visible = false;
            // 
            // frmCloseActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 348);
            this.ControlBox = false;
            this.Controls.Add(this.lblPivotActionID);
            this.Controls.Add(this.lblWorkplace);
            this.Controls.Add(this.lblActionType);
            this.Controls.Add(this.cbxResolved);
            this.Controls.Add(this.lblActionID);
            this.Controls.Add(this.Notestxt);
            this.Controls.Add(this.RCRockEngineering);
            this.Name = "frmCloseActions";
            this.Text = "Close Actions";
            this.Load += new System.EventHandler(this.frmCloseActions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RCRockEngineering)).EndInit();
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
        private System.Windows.Forms.TextBox Notestxt;
        public System.Windows.Forms.Label lblActionID;
        private System.Windows.Forms.CheckBox cbxResolved;
        public System.Windows.Forms.Label lblActionType;
        public System.Windows.Forms.Label lblWorkplace;
        public System.Windows.Forms.Label lblPivotActionID;
    }
}