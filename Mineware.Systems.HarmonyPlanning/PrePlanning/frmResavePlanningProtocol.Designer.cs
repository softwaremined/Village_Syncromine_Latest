namespace Mineware.Systems.Planning
{
    partial class frmResavePlanningProtocol
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lcSections = new DevExpress.XtraEditors.ListBoxControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProdMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProgress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pbTheProgress = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.mwProdmonthEdit1 = new Mineware.Systems.Global.CustomControls.MWProdmonthEdit();
            this.editActivity = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTheProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwProdmonthEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editActivity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lcSections
            // 
            this.lcSections.Location = new System.Drawing.Point(12, 70);
            this.lcSections.Name = "lcSections";
            this.lcSections.Size = new System.Drawing.Size(233, 487);
            this.lcSections.TabIndex = 0;
            this.lcSections.SelectedIndexChanged += new System.EventHandler(this.lcSections_SelectedIndexChanged);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(251, 70);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(537, 487);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(794, 573);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(165, 52);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Resave Reports";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(794, 70);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.pbTheProgress});
            this.gridControl2.Size = new System.Drawing.Size(284, 487);
            this.gridControl2.TabIndex = 5;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSection,
            this.colProdMonth,
            this.colProgress});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            // 
            // colSection
            // 
            this.colSection.Caption = "Section";
            this.colSection.FieldName = "theSection";
            this.colSection.Name = "colSection";
            this.colSection.Visible = true;
            this.colSection.VisibleIndex = 0;
            // 
            // colProdMonth
            // 
            this.colProdMonth.Caption = "ProdMonth";
            this.colProdMonth.FieldName = "theProdmonth";
            this.colProdMonth.Name = "colProdMonth";
            this.colProdMonth.Visible = true;
            this.colProdMonth.VisibleIndex = 1;
            // 
            // colProgress
            // 
            this.colProgress.Caption = "Progress";
            this.colProgress.ColumnEdit = this.pbTheProgress;
            this.colProgress.FieldName = "theProgress";
            this.colProgress.Name = "colProgress";
            this.colProgress.Visible = true;
            this.colProgress.VisibleIndex = 2;
            // 
            // pbTheProgress
            // 
            this.pbTheProgress.Name = "pbTheProgress";
            // 
            // mwProdmonthEdit1
            // 
            this.mwProdmonthEdit1.Location = new System.Drawing.Point(116, 12);
            this.mwProdmonthEdit1.Name = "mwProdmonthEdit1";
            this.mwProdmonthEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.mwProdmonthEdit1.Properties.Mask.EditMask = "yyyyMM";
            this.mwProdmonthEdit1.Properties.Mask.IgnoreMaskBlank = false;
            this.mwProdmonthEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.mwProdmonthEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.mwProdmonthEdit1.Size = new System.Drawing.Size(112, 20);
            this.mwProdmonthEdit1.TabIndex = 6;
            this.mwProdmonthEdit1.EditValueChanged += new System.EventHandler(this.mwProdmonthEdit1_EditValueChanged);
            // 
            // editActivity
            // 
            this.editActivity.Location = new System.Drawing.Point(116, 38);
            this.editActivity.Name = "editActivity";
            this.editActivity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editActivity.Size = new System.Drawing.Size(112, 20);
            this.editActivity.TabIndex = 7;
            this.editActivity.EditValueChanged += new System.EventHandler(this.editActivity_EditValueChanged);
            // 
            // frmResavePlanningProtocol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 678);
            this.Controls.Add(this.editActivity);
            this.Controls.Add(this.mwProdmonthEdit1);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.lcSections);
            this.Name = "frmResavePlanningProtocol";
            this.Text = "frmResavePlanningProtocol";
            this.Load += new System.EventHandler(this.frmResavePlanningProtocol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTheProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwProdmonthEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editActivity.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lcSections;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colSection;
        private DevExpress.XtraGrid.Columns.GridColumn colProdMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colProgress;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar pbTheProgress;
        private Global.CustomControls.MWProdmonthEdit mwProdmonthEdit1;
        private DevExpress.XtraEditors.LookUpEdit editActivity;
    }
}
