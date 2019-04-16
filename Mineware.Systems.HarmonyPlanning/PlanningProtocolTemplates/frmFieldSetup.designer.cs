namespace Mineware.Systems.Planning.PlanningProtocolTemplates
{
    partial class frmFieldSetup
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.txtFieldName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceDeleted = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.txtCharacters = new DevExpress.XtraEditors.TextEdit();
            this.txtLines = new DevExpress.XtraEditors.TextEdit();
            this.cmbParent = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbFieldType = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcRiskRatingGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcLines = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcCharacters = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFieldName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceDeleted.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCharacters.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLines.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbParent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFieldType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRiskRatingGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCharacters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 194);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(396, 178);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.bandedGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand1
            // 
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(111, 12);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(297, 20);
            this.txtFieldName.StyleController = this.layoutControl1;
            this.txtFieldName.TabIndex = 11;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceDeleted);
            this.layoutControl1.Controls.Add(this.checkEdit1);
            this.layoutControl1.Controls.Add(this.txtCharacters);
            this.layoutControl1.Controls.Add(this.txtLines);
            this.layoutControl1.Controls.Add(this.cmbParent);
            this.layoutControl1.Controls.Add(this.cmbFieldType);
            this.layoutControl1.Controls.Add(this.txtFieldName);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(420, 384);
            this.layoutControl1.TabIndex = 14;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceDeleted
            // 
            this.ceDeleted.Location = new System.Drawing.Point(12, 132);
            this.ceDeleted.Name = "ceDeleted";
            this.ceDeleted.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ceDeleted.Properties.Appearance.Options.UseFont = true;
            this.ceDeleted.Properties.Caption = "Deleted";
            this.ceDeleted.Size = new System.Drawing.Size(396, 19);
            this.ceDeleted.StyleController = this.layoutControl1;
            this.ceDeleted.TabIndex = 19;
            this.ceDeleted.Visible = false;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(12, 155);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.checkEdit1.Properties.Appearance.Options.UseFont = true;
            this.checkEdit1.Properties.Caption = "Field Required";
            this.checkEdit1.Size = new System.Drawing.Size(396, 19);
            this.checkEdit1.StyleController = this.layoutControl1;
            this.checkEdit1.TabIndex = 18;
            this.checkEdit1.Visible = false;
            // 
            // txtCharacters
            // 
            this.txtCharacters.Location = new System.Drawing.Point(111, 108);
            this.txtCharacters.Name = "txtCharacters";
            this.txtCharacters.Size = new System.Drawing.Size(297, 20);
            this.txtCharacters.StyleController = this.layoutControl1;
            this.txtCharacters.TabIndex = 17;
            // 
            // txtLines
            // 
            this.txtLines.Location = new System.Drawing.Point(111, 84);
            this.txtLines.Name = "txtLines";
            this.txtLines.Size = new System.Drawing.Size(297, 20);
            this.txtLines.StyleController = this.layoutControl1;
            this.txtLines.TabIndex = 16;
            // 
            // cmbParent
            // 
            this.cmbParent.Location = new System.Drawing.Point(111, 60);
            this.cmbParent.Name = "cmbParent";
            this.cmbParent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbParent.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FieldName", "Field Name")});
            this.cmbParent.Properties.NullText = "[Select a parent field]";
            this.cmbParent.Size = new System.Drawing.Size(297, 20);
            this.cmbParent.StyleController = this.layoutControl1;
            this.cmbParent.TabIndex = 15;
            // 
            // cmbFieldType
            // 
            this.cmbFieldType.Location = new System.Drawing.Point(111, 36);
            this.cmbFieldType.Name = "cmbFieldType";
            this.cmbFieldType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFieldType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FieldDescription", "Field Type")});
            this.cmbFieldType.Properties.NullText = "[Select a field type]";
            this.cmbFieldType.Size = new System.Drawing.Size(297, 20);
            this.cmbFieldType.StyleController = this.layoutControl1;
            this.cmbFieldType.TabIndex = 14;
            this.cmbFieldType.EditValueChanged += new System.EventHandler(this.cmbFieldType_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lcRiskRatingGrid,
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.lcLines,
            this.lcCharacters,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(420, 384);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtFieldName;
            this.layoutControlItem1.CustomizationFormText = "Field Name";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(400, 24);
            this.layoutControlItem1.Text = "Field Name";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(96, 13);
            // 
            // lcRiskRatingGrid
            // 
            this.lcRiskRatingGrid.Control = this.gridControl1;
            this.lcRiskRatingGrid.CustomizationFormText = "Risk Rating";
            this.lcRiskRatingGrid.Location = new System.Drawing.Point(0, 166);
            this.lcRiskRatingGrid.Name = "lcRiskRatingGrid";
            this.lcRiskRatingGrid.Size = new System.Drawing.Size(400, 198);
            this.lcRiskRatingGrid.Text = "Risk Rating";
            this.lcRiskRatingGrid.TextLocation = DevExpress.Utils.Locations.Top;
            this.lcRiskRatingGrid.TextSize = new System.Drawing.Size(96, 13);
            this.lcRiskRatingGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbFieldType;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(400, 24);
            this.layoutControlItem5.Text = "Field Type";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbParent;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(400, 24);
            this.layoutControlItem2.Text = "Parent Field";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(96, 13);
            // 
            // lcLines
            // 
            this.lcLines.Control = this.txtLines;
            this.lcLines.CustomizationFormText = "No. of Lines";
            this.lcLines.Location = new System.Drawing.Point(0, 72);
            this.lcLines.Name = "lcLines";
            this.lcLines.Size = new System.Drawing.Size(400, 24);
            this.lcLines.Text = "No. of Lines";
            this.lcLines.TextSize = new System.Drawing.Size(96, 13);
            // 
            // lcCharacters
            // 
            this.lcCharacters.Control = this.txtCharacters;
            this.lcCharacters.CustomizationFormText = "No. of Characters";
            this.lcCharacters.Location = new System.Drawing.Point(0, 96);
            this.lcCharacters.Name = "lcCharacters";
            this.lcCharacters.Size = new System.Drawing.Size(400, 24);
            this.lcCharacters.Text = "No. of Characters";
            this.lcCharacters.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.checkEdit1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 143);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(400, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ceDeleted;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(400, 23);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 384);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(420, 40);
            this.panelControl1.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(252, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(333, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmFieldSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(420, 424);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFieldSetup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Field Setup";
            this.Load += new System.EventHandler(this.frmFieldSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFieldName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceDeleted.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCharacters.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLines.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbParent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFieldType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRiskRatingGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCharacters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.TextEdit txtFieldName;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lcRiskRatingGrid;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LookUpEdit cmbFieldType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LookUpEdit cmbParent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtCharacters;
        private DevExpress.XtraEditors.TextEdit txtLines;
        private DevExpress.XtraLayout.LayoutControlItem lcLines;
        private DevExpress.XtraLayout.LayoutControlItem lcCharacters;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit ceDeleted;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}