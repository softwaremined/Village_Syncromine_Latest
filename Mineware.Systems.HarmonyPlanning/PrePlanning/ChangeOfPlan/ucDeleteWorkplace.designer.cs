namespace Mineware.Systems.Planning.PrePlanning.ChangeOfPlan
{
    partial class ucDeleteWorkplace
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.memoReason = new DevExpress.XtraEditors.MemoEdit();
            this.txtWorkplaceID = new DevExpress.XtraEditors.TextEdit();
            this.txtProdMonth = new DevExpress.XtraEditors.TextEdit();
            this.txtSection = new DevExpress.XtraEditors.TextEdit();
            this.txtWorkplaceName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.memoReason);
            this.layoutControl1.Controls.Add(this.txtWorkplaceID);
            this.layoutControl1.Controls.Add(this.txtProdMonth);
            this.layoutControl1.Controls.Add(this.txtSection);
            this.layoutControl1.Controls.Add(this.txtWorkplaceName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(526, 459);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // memoReason
            // 
            this.memoReason.Location = new System.Drawing.Point(115, 108);
            this.memoReason.Name = "memoReason";
            this.memoReason.Size = new System.Drawing.Size(399, 339);
            this.memoReason.StyleController = this.layoutControl1;
            this.memoReason.TabIndex = 9;
            this.memoReason.EditValueChanged += new System.EventHandler(this.memoReason_EditValueChanged);
            // 
            // txtWorkplaceID
            // 
            this.txtWorkplaceID.Location = new System.Drawing.Point(115, 60);
            this.txtWorkplaceID.Name = "txtWorkplaceID";
            this.txtWorkplaceID.Size = new System.Drawing.Size(399, 20);
            this.txtWorkplaceID.StyleController = this.layoutControl1;
            this.txtWorkplaceID.TabIndex = 7;
            // 
            // txtProdMonth
            // 
            this.txtProdMonth.Location = new System.Drawing.Point(115, 36);
            this.txtProdMonth.Name = "txtProdMonth";
            this.txtProdMonth.Size = new System.Drawing.Size(399, 20);
            this.txtProdMonth.StyleController = this.layoutControl1;
            this.txtProdMonth.TabIndex = 6;
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(115, 12);
            this.txtSection.Name = "txtSection";
            this.txtSection.Size = new System.Drawing.Size(399, 20);
            this.txtSection.StyleController = this.layoutControl1;
            this.txtSection.TabIndex = 5;
            // 
            // txtWorkplaceName
            // 
            this.txtWorkplaceName.Location = new System.Drawing.Point(115, 84);
            this.txtWorkplaceName.Name = "txtWorkplaceName";
            this.txtWorkplaceName.Size = new System.Drawing.Size(399, 20);
            this.txtWorkplaceName.StyleController = this.layoutControl1;
            this.txtWorkplaceName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.layoutControlItem6});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(526, 459);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem2.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem2.Control = this.txtSection;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(506, 24);
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
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(506, 24);
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
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(506, 24);
            this.layoutControlItem4.Text = "WorkplaceID";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(100, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlItem1.AppearanceItemCaption.ForeColor = System.Drawing.Color.Black;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem1.Control = this.txtWorkplaceName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(506, 24);
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
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(506, 343);
            this.layoutControlItem6.Text = "Reason";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(100, 13);
            // 
            // ucDeleteWorkplace
            // 
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucDeleteWorkplace";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(526, 459);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtWorkplaceID;
        private DevExpress.XtraEditors.TextEdit txtProdMonth;
        private DevExpress.XtraEditors.TextEdit txtSection;
        private DevExpress.XtraEditors.TextEdit txtWorkplaceName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.MemoEdit memoReason;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}
