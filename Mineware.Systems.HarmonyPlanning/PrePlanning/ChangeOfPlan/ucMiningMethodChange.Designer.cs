namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    partial class ucMiningMethodChange
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.memoReason = new DevExpress.XtraEditors.MemoEdit();
            this.editMiningMethod = new DevExpress.XtraEditors.LookUpEdit();
            this.txtWorkplaceID = new DevExpress.XtraEditors.TextEdit();
            this.txtProdMonth = new DevExpress.XtraEditors.TextEdit();
            this.txtSection = new DevExpress.XtraEditors.TextEdit();
            this.txtWorkplaceName = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editMiningMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.layoutControl1);
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(601, 474);
            this.panelControl1.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.memoReason);
            this.layoutControl1.Controls.Add(this.editMiningMethod);
            this.layoutControl1.Controls.Add(this.txtWorkplaceID);
            this.layoutControl1.Controls.Add(this.txtProdMonth);
            this.layoutControl1.Controls.Add(this.txtSection);
            this.layoutControl1.Controls.Add(this.txtWorkplaceName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(601, 474);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // memoReason
            // 
            this.memoReason.Location = new System.Drawing.Point(105, 122);
            this.memoReason.Name = "memoReason";
            this.memoReason.Size = new System.Drawing.Size(494, 255);
            this.memoReason.StyleController = this.layoutControl1;
            this.memoReason.TabIndex = 9;
            this.memoReason.EditValueChanged += new System.EventHandler(this.memoReason_EditValueChanged);
            // 
            // editMiningMethod
            // 
            this.editMiningMethod.Location = new System.Drawing.Point(105, 98);
            this.editMiningMethod.Name = "editMiningMethod";
            this.editMiningMethod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editMiningMethod.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description")});
            this.editMiningMethod.Size = new System.Drawing.Size(494, 20);
            this.editMiningMethod.StyleController = this.layoutControl1;
            this.editMiningMethod.TabIndex = 8;
            this.editMiningMethod.EditValueChanged += new System.EventHandler(this.editMiningMethod_EditValueChanged);
            // 
            // txtWorkplaceID
            // 
            this.txtWorkplaceID.Location = new System.Drawing.Point(105, 50);
            this.txtWorkplaceID.Name = "txtWorkplaceID";
            this.txtWorkplaceID.Size = new System.Drawing.Size(494, 20);
            this.txtWorkplaceID.StyleController = this.layoutControl1;
            this.txtWorkplaceID.TabIndex = 7;
            // 
            // txtProdMonth
            // 
            this.txtProdMonth.Location = new System.Drawing.Point(105, 26);
            this.txtProdMonth.Name = "txtProdMonth";
            this.txtProdMonth.Size = new System.Drawing.Size(494, 20);
            this.txtProdMonth.StyleController = this.layoutControl1;
            this.txtProdMonth.TabIndex = 6;
            // 
            // txtSection
            // 
            this.txtSection.EditValue = "";
            this.txtSection.Location = new System.Drawing.Point(105, 2);
            this.txtSection.Name = "txtSection";
            this.txtSection.Size = new System.Drawing.Size(494, 20);
            this.txtSection.StyleController = this.layoutControl1;
            this.txtSection.TabIndex = 5;
            // 
            // txtWorkplaceName
            // 
            this.txtWorkplaceName.Location = new System.Drawing.Point(105, 74);
            this.txtWorkplaceName.Name = "txtWorkplaceName";
            this.txtWorkplaceName.Size = new System.Drawing.Size(494, 20);
            this.txtWorkplaceName.StyleController = this.layoutControl1;
            this.txtWorkplaceName.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.CustomizationFormText = "Root";
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem6,
            this.emptySpaceItem1});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(601, 474);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem2.Control = this.txtSection;
            this.layoutControlItem2.CustomizationFormText = "Section";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem2.Text = "Section";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem3.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem3.Control = this.txtProdMonth;
            this.layoutControlItem3.CustomizationFormText = "Production Month";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem3.Text = "Production Month";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem4.Control = this.txtWorkplaceID;
            this.layoutControlItem4.CustomizationFormText = "WorkplaceID";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem4.Text = "WorkplaceID";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem5.Control = this.editMiningMethod;
            this.layoutControlItem5.CustomizationFormText = "Mining Method";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem5.Text = "Mining Method";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem1.Control = this.txtWorkplaceName;
            this.layoutControlItem1.CustomizationFormText = "Workplace Name";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem1.Text = "Workplace Name";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem6.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem6.Control = this.memoReason;
            this.layoutControlItem6.CustomizationFormText = "Reason";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(601, 259);
            this.layoutControlItem6.Text = "Reason";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(100, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 379);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(601, 95);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ucMiningMethodChange
            // 
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ucMiningMethodChange";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(607, 477);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editMiningMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtWorkplaceID;
        private DevExpress.XtraEditors.TextEdit txtProdMonth;
        private DevExpress.XtraEditors.TextEdit txtSection;
        private DevExpress.XtraEditors.TextEdit txtWorkplaceName;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LookUpEdit editMiningMethod;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.MemoEdit memoReason;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
