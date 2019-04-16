namespace Mineware.Systems.Planning.PlanningProtocolCapture
{
    partial class ucUnapproveWorkplaceList
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
            this.gcRequestList = new DevExpress.XtraGrid.GridControl();
            this.gvRequestList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbProdmonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbFieldname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbSectionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcRequestList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRequestList)).BeginInit();
            this.SuspendLayout();
            // 
            // gcRequestList
            // 
            this.gcRequestList.Location = new System.Drawing.Point(1, 20);
            this.gcRequestList.MainView = this.gvRequestList;
            this.gcRequestList.Name = "gcRequestList";
            this.gcRequestList.Size = new System.Drawing.Size(744, 304);
            this.gcRequestList.TabIndex = 0;
            this.gcRequestList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRequestList});
            // 
            // gvRequestList
            // 
            this.gvRequestList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cmbProdmonth,
            this.cmbDescription,
            this.cmbFieldname,
            this.cmbSectionID});
            this.gvRequestList.GridControl = this.gcRequestList;
            this.gvRequestList.Name = "gvRequestList";
            this.gvRequestList.OptionsBehavior.Editable = false;
            this.gvRequestList.OptionsBehavior.ReadOnly = true;
            this.gvRequestList.OptionsView.ShowGroupPanel = false;
            // 
            // cmbProdmonth
            // 
            this.cmbProdmonth.Caption = "ProdMonth";
            this.cmbProdmonth.FieldName = "PRODMONTH";
            this.cmbProdmonth.Name = "cmbProdmonth";
            this.cmbProdmonth.Visible = true;
            this.cmbProdmonth.VisibleIndex = 0;
            // 
            // cmbDescription
            // 
            this.cmbDescription.Caption = "Description";
            this.cmbDescription.FieldName = "DESCRIPTION";
            this.cmbDescription.Name = "cmbDescription";
            this.cmbDescription.Visible = true;
            this.cmbDescription.VisibleIndex = 1;
            // 
            // cmbFieldname
            // 
            this.cmbFieldname.Caption = "Fieldname";
            this.cmbFieldname.FieldName = "FieldName";
            this.cmbFieldname.Name = "cmbFieldname";
            this.cmbFieldname.Visible = true;
            this.cmbFieldname.VisibleIndex = 2;
            // 
            // cmbSectionID
            // 
            this.cmbSectionID.Caption = "SectionID";
            this.cmbSectionID.FieldName = "SECTIONID";
            this.cmbSectionID.Name = "cmbSectionID";
            this.cmbSectionID.Visible = true;
            this.cmbSectionID.VisibleIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(1, 1);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(91, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "DATA REQUIRED";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(627, 324);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(118, 34);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "OK";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ucUnapproveWorkplaceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 359);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.gcRequestList);
            this.Name = "ucUnapproveWorkplaceList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LIST OF WORKPLACES THAT CANNOT BE APPROVED";
            ((System.ComponentModel.ISupportInitialize)(this.gcRequestList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRequestList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gvRequestList;
        private DevExpress.XtraGrid.Columns.GridColumn cmbProdmonth;
        private DevExpress.XtraGrid.Columns.GridColumn cmbDescription;
        private DevExpress.XtraGrid.Columns.GridColumn cmbFieldname;
        private DevExpress.XtraGrid.Columns.GridColumn cmbSectionID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraGrid.GridControl gcRequestList;
    }
}