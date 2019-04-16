namespace Mineware.Systems.Production.SysAdminScreens.DepartmentalCapture
{
    partial class SamplingChecklistAdditInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SamplingChecklistAdditInfo));
            this.Addit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.Addit2 = new DevExpress.XtraEditors.LookUpEdit();
            this.CommTxt = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.lueMillMonth = new DevExpress.XtraBars.BarEditItem();
            this.btnShow = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.lueMill = new DevExpress.XtraBars.BarEditItem();
            this.btnBack = new DevExpress.XtraBars.BarButtonItem();
            this.rgType = new DevExpress.XtraBars.BarEditItem();
            this.btnAddWorktype = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditWorkType = new DevExpress.XtraBars.BarButtonItem();
            this.btnStpApply = new DevExpress.XtraBars.BarButtonItem();
            this.cbxCycleNum = new DevExpress.XtraBars.BarEditItem();
            this.txtCycle = new DevExpress.XtraBars.BarEditItem();
            this.txtAdv = new DevExpress.XtraBars.BarEditItem();
            this.btnDepartments = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.ChecklistSelectedItem = new DevExpress.XtraBars.BarEditItem();
            this.MonthItem1 = new DevExpress.XtraBars.BarEditItem();
            this.SecItem1 = new DevExpress.XtraBars.BarEditItem();
            this.Month1 = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.SelectedPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Addit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Addit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // Addit1
            // 
            this.Addit1.Location = new System.Drawing.Point(15, 104);
            this.Addit1.Name = "Addit1";
            this.Addit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Addit1.Size = new System.Drawing.Size(170, 20);
            this.Addit1.TabIndex = 0;
            // 
            // Addit2
            // 
            this.Addit2.Location = new System.Drawing.Point(15, 154);
            this.Addit2.Name = "Addit2";
            this.Addit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Addit2.Size = new System.Drawing.Size(170, 20);
            this.Addit2.TabIndex = 1;
            this.Addit2.EditValueChanged += new System.EventHandler(this.lookUpEdit2_EditValueChanged);
            // 
            // CommTxt
            // 
            this.CommTxt.Location = new System.Drawing.Point(15, 205);
            this.CommTxt.Name = "CommTxt";
            this.CommTxt.Size = new System.Drawing.Size(432, 218);
            this.CommTxt.TabIndex = 2;
            this.CommTxt.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Additional Workplace 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Additional Workplace 2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Comments:";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowKeyTips = false;
            this.ribbonControl1.AllowMdiChildButtons = false;
            this.ribbonControl1.AllowMinimizeRibbon = false;
            this.ribbonControl1.AllowTrimPageText = false;
            this.ribbonControl1.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.lueMillMonth,
            this.btnShow,
            this.btnSave,
            this.lueMill,
            this.btnBack,
            this.rgType,
            this.btnAddWorktype,
            this.btnEditWorkType,
            this.btnStpApply,
            this.cbxCycleNum,
            this.txtCycle,
            this.txtAdv,
            this.btnDepartments,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.ChecklistSelectedItem,
            this.MonthItem1,
            this.SecItem1,
            this.Month1,
            this.barEditItem1,
            this.barButtonItem4,
            this.barEditItem2,
            this.barEditItem3});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 33;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowCategoryInCaption = false;
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.ShowQatLocationSelector = false;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(473, 77);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // lueMillMonth
            // 
            this.lueMillMonth.Caption = "Mill Month";
            this.lueMillMonth.Edit = null;
            this.lueMillMonth.EditWidth = 150;
            this.lueMillMonth.Id = 1;
            this.lueMillMonth.Name = "lueMillMonth";
            // 
            // btnShow
            // 
            this.btnShow.Caption = "Show";
            this.btnShow.Id = 5;
            this.btnShow.ImageOptions.ImageUri.Uri = "Zoom";
            this.btnShow.Name = "btnShow";
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.Id = 7;
            this.btnSave.ImageOptions.ImageUri.Uri = "Save";
            this.btnSave.Name = "btnSave";
            // 
            // lueMill
            // 
            this.lueMill.Caption = "Mill           ";
            this.lueMill.Edit = null;
            this.lueMill.EditWidth = 150;
            this.lueMill.Id = 8;
            this.lueMill.Name = "lueMill";
            // 
            // btnBack
            // 
            this.btnBack.Caption = "Back";
            this.btnBack.Id = 9;
            this.btnBack.ImageOptions.ImageUri.Uri = "Backward";
            this.btnBack.Name = "btnBack";
            // 
            // rgType
            // 
            this.rgType.Caption = "Type";
            this.rgType.Edit = null;
            this.rgType.EditWidth = 280;
            this.rgType.Id = 12;
            this.rgType.Name = "rgType";
            // 
            // btnAddWorktype
            // 
            this.btnAddWorktype.Caption = "Add    Workplace";
            this.btnAddWorktype.Id = 13;
            this.btnAddWorktype.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddWorktype.ImageOptions.Image")));
            this.btnAddWorktype.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAddWorktype.ImageOptions.LargeImage")));
            this.btnAddWorktype.Name = "btnAddWorktype";
            // 
            // btnEditWorkType
            // 
            this.btnEditWorkType.Caption = "Edit Work Type";
            this.btnEditWorkType.Id = 14;
            this.btnEditWorkType.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditWorkType.ImageOptions.Image")));
            this.btnEditWorkType.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEditWorkType.ImageOptions.LargeImage")));
            this.btnEditWorkType.Name = "btnEditWorkType";
            // 
            // btnStpApply
            // 
            this.btnStpApply.Caption = "Apply Cycle";
            this.btnStpApply.Id = 16;
            this.btnStpApply.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnStpApply.ImageOptions.Image")));
            this.btnStpApply.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnStpApply.ImageOptions.LargeImage")));
            this.btnStpApply.Name = "btnStpApply";
            // 
            // cbxCycleNum
            // 
            this.cbxCycleNum.Caption = "Cycle        ";
            this.cbxCycleNum.CaptionAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.cbxCycleNum.Edit = null;
            this.cbxCycleNum.EditWidth = 199;
            this.cbxCycleNum.Id = 18;
            this.cbxCycleNum.Name = "cbxCycleNum";
            // 
            // txtCycle
            // 
            this.txtCycle.Caption = "Cycle Num";
            this.txtCycle.CaptionAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txtCycle.CausesValidation = true;
            this.txtCycle.Edit = null;
            this.txtCycle.EditWidth = 200;
            this.txtCycle.Id = 19;
            this.txtCycle.Name = "txtCycle";
            // 
            // txtAdv
            // 
            this.txtAdv.Caption = "Adv/Blast ";
            this.txtAdv.CaptionAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.txtAdv.Edit = null;
            this.txtAdv.EditWidth = 200;
            this.txtAdv.Id = 20;
            this.txtAdv.Name = "txtAdv";
            // 
            // btnDepartments
            // 
            this.btnDepartments.Caption = "Departments";
            this.btnDepartments.Id = 21;
            this.btnDepartments.Name = "btnDepartments";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Delete Checklist";
            this.barButtonItem1.Id = 22;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Preview Checklist";
            this.barButtonItem2.Id = 23;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.LargeImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Print Checklist";
            this.barButtonItem3.Id = 24;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // ChecklistSelectedItem
            // 
            this.ChecklistSelectedItem.Caption = "Checklist Name";
            this.ChecklistSelectedItem.Edit = null;
            this.ChecklistSelectedItem.EditWidth = 180;
            this.ChecklistSelectedItem.Id = 25;
            this.ChecklistSelectedItem.Name = "ChecklistSelectedItem";
            // 
            // MonthItem1
            // 
            this.MonthItem1.Caption = "Month  ";
            this.MonthItem1.Edit = null;
            this.MonthItem1.EditWidth = 100;
            this.MonthItem1.Id = 26;
            this.MonthItem1.Name = "MonthItem1";
            // 
            // SecItem1
            // 
            this.SecItem1.Caption = "Section                                                               ";
            this.SecItem1.Edit = null;
            this.SecItem1.EditWidth = 1;
            this.SecItem1.Id = 27;
            this.SecItem1.Name = "SecItem1";
            // 
            // Month1
            // 
            this.Month1.Caption = "Month  ";
            this.Month1.Edit = null;
            this.Month1.EditWidth = 1;
            this.Month1.Id = 28;
            this.Month1.Name = "Month1";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "Printed";
            this.barEditItem1.Edit = null;
            this.barEditItem1.Id = 29;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Bulk Print";
            this.barButtonItem4.Id = 30;
            this.barButtonItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.Image")));
            this.barButtonItem4.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem4.ImageOptions.LargeImage")));
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "Section";
            this.barEditItem2.Edit = null;
            this.barEditItem2.EditWidth = 1;
            this.barEditItem2.Id = 31;
            this.barEditItem2.Name = "barEditItem2";
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "Date    ";
            this.barEditItem3.Edit = null;
            this.barEditItem3.EditWidth = 160;
            this.barEditItem3.Id = 32;
            this.barEditItem3.Name = "barEditItem3";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.SelectedPageGroup});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // SelectedPageGroup
            // 
            this.SelectedPageGroup.ItemLinks.Add(this.barButtonItem3);
            this.SelectedPageGroup.Name = "SelectedPageGroup";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(363, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(363, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "label6";
            this.label6.Visible = false;
            // 
            // SamplingChecklistAdditInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 439);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CommTxt);
            this.Controls.Add(this.Addit2);
            this.Controls.Add(this.Addit1);
            this.Name = "SamplingChecklistAdditInfo";
            this.ShowIcon = false;
            this.Text = "Sampling Checklist Additional Info";
            this.Load += new System.EventHandler(this.SamplingChecklistAdditInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Addit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Addit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit Addit1;
        private DevExpress.XtraEditors.LookUpEdit Addit2;
        private System.Windows.Forms.RichTextBox CommTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarEditItem lueMillMonth;
        private DevExpress.XtraBars.BarButtonItem btnShow;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarEditItem lueMill;
        private DevExpress.XtraBars.BarButtonItem btnBack;
        private DevExpress.XtraBars.BarEditItem rgType;
        private DevExpress.XtraBars.BarButtonItem btnAddWorktype;
        private DevExpress.XtraBars.BarButtonItem btnEditWorkType;
        private DevExpress.XtraBars.BarButtonItem btnStpApply;
        private DevExpress.XtraBars.BarEditItem cbxCycleNum;
        private DevExpress.XtraBars.BarEditItem txtCycle;
        private DevExpress.XtraBars.BarEditItem txtAdv;
        private DevExpress.XtraBars.BarButtonItem btnDepartments;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarEditItem ChecklistSelectedItem;
        private DevExpress.XtraBars.BarEditItem MonthItem1;
        private DevExpress.XtraBars.BarEditItem SecItem1;
        private DevExpress.XtraBars.BarEditItem Month1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        public DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup SelectedPageGroup;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
    }
}