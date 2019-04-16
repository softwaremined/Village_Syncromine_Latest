namespace Mineware.Systems.Production.Controls.BookingsABS
{
    partial class ucBookingsABSPlanStop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBookingsABSPlanStop));
            this.lbSearch = new System.Windows.Forms.Label();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.gc10 = new DevExpress.XtraGrid.GridControl();
            this.gv10 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc10Notes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lbWorkplace = new DevExpress.XtraEditors.LabelControl();
            this.lbDate = new DevExpress.XtraEditors.LabelControl();
            this.lbSection = new DevExpress.XtraEditors.LabelControl();
            this.lbNoteID = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv10)).BeginInit();
            this.SuspendLayout();
            // 
            // lbSearch
            // 
            this.lbSearch.AutoSize = true;
            this.lbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSearch.Location = new System.Drawing.Point(170, 9);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(47, 13);
            this.lbSearch.TabIndex = 54;
            this.lbSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(173, 24);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(252, 20);
            this.txtSearch.TabIndex = 53;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // gc10
            // 
            this.gc10.Location = new System.Drawing.Point(12, 62);
            this.gc10.MainView = this.gv10;
            this.gc10.Name = "gc10";
            this.gc10.Size = new System.Drawing.Size(446, 349);
            this.gc10.TabIndex = 55;
            this.gc10.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv10});
            // 
            // gv10
            // 
            this.gv10.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc10Notes});
            this.gv10.GridControl = this.gc10;
            this.gv10.Name = "gv10";
            this.gv10.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gv10.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gv10.OptionsView.ShowGroupedColumns = true;
            this.gv10.OptionsView.ShowGroupPanel = false;
            this.gv10.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gv10_RowClick);
            this.gv10.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gv10_RowCellClick);
            // 
            // gc10Notes
            // 
            this.gc10Notes.Caption = "gridColumn1";
            this.gc10Notes.FieldName = "PS";
            this.gc10Notes.Name = "gc10Notes";
            this.gc10Notes.OptionsColumn.AllowEdit = false;
            this.gc10Notes.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gc10Notes.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gc10Notes.OptionsColumn.ReadOnly = true;
            this.gc10Notes.Visible = true;
            this.gc10Notes.VisibleIndex = 0;
            this.gc10Notes.Width = 83;
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(219, 452);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 43);
            this.btnCancel.TabIndex = 57;
            this.btnCancel.Text = "Cancel";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(61, 452);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 43);
            this.btnSave.TabIndex = 56;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbWorkplace
            // 
            this.lbWorkplace.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbWorkplace.Appearance.Options.UseFont = true;
            this.lbWorkplace.Location = new System.Drawing.Point(12, 27);
            this.lbWorkplace.Name = "lbWorkplace";
            this.lbWorkplace.Size = new System.Drawing.Size(75, 13);
            this.lbWorkplace.TabIndex = 60;
            this.lbWorkplace.Text = "labelControl3";
            // 
            // lbDate
            // 
            this.lbDate.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbDate.Appearance.Options.UseFont = true;
            this.lbDate.Location = new System.Drawing.Point(12, 43);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(75, 13);
            this.lbDate.TabIndex = 59;
            this.lbDate.Text = "labelControl2";
            // 
            // lbSection
            // 
            this.lbSection.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbSection.Appearance.Options.UseFont = true;
            this.lbSection.Location = new System.Drawing.Point(12, 9);
            this.lbSection.Name = "lbSection";
            this.lbSection.Size = new System.Drawing.Size(75, 13);
            this.lbSection.TabIndex = 58;
            this.lbSection.Text = "labelControl1";
            // 
            // lbNoteID
            // 
            this.lbNoteID.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbNoteID.Appearance.Options.UseFont = true;
            this.lbNoteID.Location = new System.Drawing.Point(12, 417);
            this.lbNoteID.Name = "lbNoteID";
            this.lbNoteID.Size = new System.Drawing.Size(42, 13);
            this.lbNoteID.TabIndex = 61;
            this.lbNoteID.Text = "Note ID";
            // 
            // ucBookingsABSPlanStop
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 507);
            this.Controls.Add(this.lbNoteID);
            this.Controls.Add(this.lbWorkplace);
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.lbSection);
            this.Controls.Add(this.gc10);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbSearch);
            this.Controls.Add(this.txtSearch);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ucBookingsABSPlanStop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Planned Stoppages";
            this.Text = "Planned Stoppages";
            this.Load += new System.EventHandler(this.ucBookingsABSPlanStop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gc10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv10)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSearch;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraGrid.GridControl gc10;
        private DevExpress.XtraGrid.Views.Grid.GridView gv10;
        private DevExpress.XtraGrid.Columns.GridColumn gc10Notes;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        public DevExpress.XtraEditors.LabelControl lbWorkplace;
        public DevExpress.XtraEditors.LabelControl lbDate;
        public DevExpress.XtraEditors.LabelControl lbSection;
        public DevExpress.XtraEditors.LabelControl lbNoteID;
    }
}
