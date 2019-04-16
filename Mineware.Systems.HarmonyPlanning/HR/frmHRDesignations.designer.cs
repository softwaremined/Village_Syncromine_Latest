namespace Mineware.Systems.Planning.HR
{
    partial class frmHRDesignations
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gcSelectedData = new DevExpress.XtraGrid.GridControl();
            this.gvSelectedData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDesignationSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAvalibalData = new DevExpress.XtraGrid.GridControl();
            this.gvAvalibalData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDesignation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSelectedData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectedData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAvalibalData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAvalibalData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.gcSelectedData);
            this.layoutControl1.Controls.Add(this.gcAvalibalData);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(796, 627);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.simpleButton3);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Location = new System.Drawing.Point(336, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(104, 603);
            this.panelControl1.TabIndex = 6;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(3, 577);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(97, 23);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "OK";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(2, 3);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(98, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = ">>";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(2, 32);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(98, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "<<";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gcSelectedData
            // 
            this.gcSelectedData.Location = new System.Drawing.Point(444, 31);
            this.gcSelectedData.MainView = this.gvSelectedData;
            this.gcSelectedData.Name = "gcSelectedData";
            this.gcSelectedData.Size = new System.Drawing.Size(340, 584);
            this.gcSelectedData.TabIndex = 5;
            this.gcSelectedData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSelectedData});
            // 
            // gvSelectedData
            // 
            this.gvSelectedData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDesignationSelected});
            this.gvSelectedData.GridControl = this.gcSelectedData;
            this.gvSelectedData.Name = "gvSelectedData";
            this.gvSelectedData.OptionsView.ShowAutoFilterRow = true;
            this.gvSelectedData.OptionsView.ShowGroupPanel = false;
            // 
            // gcDesignationSelected
            // 
            this.gcDesignationSelected.Caption = "Designations";
            this.gcDesignationSelected.FieldName = "Designation";
            this.gcDesignationSelected.Name = "gcDesignationSelected";
            this.gcDesignationSelected.OptionsColumn.AllowEdit = false;
            this.gcDesignationSelected.OptionsColumn.ReadOnly = true;
            this.gcDesignationSelected.Visible = true;
            this.gcDesignationSelected.VisibleIndex = 0;
            // 
            // gcAvalibalData
            // 
            this.gcAvalibalData.Location = new System.Drawing.Point(12, 31);
            this.gcAvalibalData.MainView = this.gvAvalibalData;
            this.gcAvalibalData.Name = "gcAvalibalData";
            this.gcAvalibalData.Size = new System.Drawing.Size(320, 584);
            this.gcAvalibalData.TabIndex = 4;
            this.gcAvalibalData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAvalibalData});
            // 
            // gvAvalibalData
            // 
            this.gvAvalibalData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDesignation});
            this.gvAvalibalData.GridControl = this.gcAvalibalData;
            this.gvAvalibalData.Name = "gvAvalibalData";
            this.gvAvalibalData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvAvalibalData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvAvalibalData.OptionsView.ShowAutoFilterRow = true;
            this.gvAvalibalData.OptionsView.ShowGroupPanel = false;
            // 
            // gcDesignation
            // 
            this.gcDesignation.Caption = "Designations";
            this.gcDesignation.FieldName = "designation";
            this.gcDesignation.Name = "gcDesignation";
            this.gcDesignation.OptionsColumn.AllowEdit = false;
            this.gcDesignation.OptionsColumn.ReadOnly = true;
            this.gcDesignation.Visible = true;
            this.gcDesignation.VisibleIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(796, 627);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.gcAvalibalData;
            this.layoutControlItem1.CustomizationFormText = "Avalibal Designations";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(324, 607);
            this.layoutControlItem1.Text = "Avalibal Designations";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(143, 16);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.gcSelectedData;
            this.layoutControlItem2.CustomizationFormText = "Selected Designations";
            this.layoutControlItem2.Location = new System.Drawing.Point(432, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(344, 607);
            this.layoutControlItem2.Text = "Selected Designations";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(143, 16);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(324, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(108, 607);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmHRDesignations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 627);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmHRDesignations";
            this.Text = "Designations";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSelectedData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelectedData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAvalibalData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAvalibalData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcSelectedData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSelectedData;
        private DevExpress.XtraGrid.GridControl gcAvalibalData;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAvalibalData;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn gcDesignation;
        private DevExpress.XtraGrid.Columns.GridColumn gcDesignationSelected;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
    }
}