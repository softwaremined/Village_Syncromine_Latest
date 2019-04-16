namespace Mineware.Systems.Production.SysAdminScreens.SectionScreen
{
    partial class ucSectionScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSectionScreen));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mwButton1 = new Mineware.Systems.Global.CustomControls.MWButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.editProdmonth = new Mineware.Systems.Global.CustomControls.MWProdmonthEdit();
            this.lblProdMonth = new DevExpress.XtraEditors.LabelControl();
            this.btnAdd = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton();
            this.gcSectionScreen = new DevExpress.XtraGrid.GridControl();
            this.gvSectionScreen = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSectionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReportToSectionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcHierarchicalID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOccupation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProdMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIndustryNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editProdmonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSectionScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSectionScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mwButton1);
            this.splitContainer1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.btnEdit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gcSectionScreen);
            this.splitContainer1.Size = new System.Drawing.Size(868, 350);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 4;
            // 
            // mwButton1
            // 
            this.mwButton1.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.mwButton1.Appearance.Options.UseFont = true;
            this.mwButton1.ImageLeftPadding = 0;
            this.mwButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mwButton1.ImageOptions.Image")));
            this.mwButton1.Location = new System.Drawing.Point(140, 0);
            this.mwButton1.Margin = new System.Windows.Forms.Padding(4);
            this.mwButton1.Name = "mwButton1";
            this.mwButton1.Size = new System.Drawing.Size(70, 40);
            this.mwButton1.TabIndex = 17;
            this.mwButton1.Text = "Delete";
            this.mwButton1.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Delete;
            this.mwButton1.Click += new System.EventHandler(this.mwButton1_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.editProdmonth);
            this.panelControl1.Controls.Add(this.lblProdMonth);
            this.panelControl1.Location = new System.Drawing.Point(212, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(656, 40);
            this.panelControl1.TabIndex = 4;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // editProdmonth
            // 
            this.editProdmonth.EditValue = new System.DateTime(2014, 3, 4, 10, 58, 26, 92);
            this.editProdmonth.Location = new System.Drawing.Point(124, 11);
            this.editProdmonth.Name = "editProdmonth";
            this.editProdmonth.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.editProdmonth.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.editProdmonth.Properties.Appearance.Options.UseBackColor = true;
            this.editProdmonth.Properties.Appearance.Options.UseForeColor = true;
            this.editProdmonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.editProdmonth.Properties.Mask.EditMask = "yyyyMM";
            this.editProdmonth.Properties.Mask.IgnoreMaskBlank = false;
            this.editProdmonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.editProdmonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.editProdmonth.Properties.ReadOnly = true;
            this.editProdmonth.Size = new System.Drawing.Size(105, 20);
            this.editProdmonth.TabIndex = 16;
            this.editProdmonth.Tag = "201402";
            this.editProdmonth.EditValueChanged += new System.EventHandler(this.editProdmonth_EditValueChanged_1);
            // 
            // lblProdMonth
            // 
            this.lblProdMonth.Location = new System.Drawing.Point(19, 14);
            this.lblProdMonth.Name = "lblProdMonth";
            this.lblProdMonth.Size = new System.Drawing.Size(84, 13);
            this.lblProdMonth.TabIndex = 15;
            this.lblProdMonth.Text = "Production Month";
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.ImageLeftPadding = 0;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 40);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Add;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.ImageLeftPadding = 0;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.Location = new System.Drawing.Point(70, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 40);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Edit";
            this.btnEdit.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Edit;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // gcSectionScreen
            // 
            this.gcSectionScreen.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcSectionScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSectionScreen.Location = new System.Drawing.Point(0, 0);
            this.gcSectionScreen.MainView = this.gvSectionScreen;
            this.gcSectionScreen.Name = "gcSectionScreen";
            this.gcSectionScreen.Size = new System.Drawing.Size(868, 306);
            this.gcSectionScreen.TabIndex = 3;
            this.gcSectionScreen.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSectionScreen});
            this.gcSectionScreen.Click += new System.EventHandler(this.gcSectionScreen_Click);
            // 
            // gvSectionScreen
            // 
            this.gvSectionScreen.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSectionID,
            this.gcName,
            this.gcReportToSectionID,
            this.gcHierarchicalID,
            this.gcOccupation,
            this.gcStatus,
            this.gcProdMonth,
            this.gcIndustryNumber});
            this.gvSectionScreen.GridControl = this.gcSectionScreen;
            this.gvSectionScreen.Name = "gvSectionScreen";
            this.gvSectionScreen.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSectionScreen.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSectionScreen.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gvSectionScreen.OptionsBehavior.ReadOnly = true;
            this.gvSectionScreen.OptionsCustomization.AllowColumnMoving = false;
            this.gvSectionScreen.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.False;
            this.gvSectionScreen.OptionsView.ShowAutoFilterRow = true;
            this.gvSectionScreen.OptionsView.ShowGroupPanel = false;
            this.gvSectionScreen.EditFormPrepared += new DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventHandler(this.gvSectionScreen_EditFormPrepared);
            this.gvSectionScreen.ShowingPopupEditForm += new DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventHandler(this.gvSectionScreen_ShowingPopupEditForm);
            this.gvSectionScreen.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvSectionScreen_InitNewRow);
            this.gvSectionScreen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvSectionScreen_KeyDown);
            this.gvSectionScreen.DoubleClick += new System.EventHandler(this.gvSectionScreen_DoubleClick);
            // 
            // gcSectionID
            // 
            this.gcSectionID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcSectionID.AppearanceHeader.Options.UseFont = true;
            this.gcSectionID.Caption = "Section";
            this.gcSectionID.FieldName = "SectionID";
            this.gcSectionID.Name = "gcSectionID";
            this.gcSectionID.OptionsColumn.AllowEdit = false;
            this.gcSectionID.Visible = true;
            this.gcSectionID.VisibleIndex = 0;
            this.gcSectionID.Width = 121;
            // 
            // gcName
            // 
            this.gcName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcName.AppearanceHeader.Options.UseFont = true;
            this.gcName.Caption = "Name";
            this.gcName.FieldName = "Name";
            this.gcName.Name = "gcName";
            this.gcName.OptionsColumn.AllowEdit = false;
            this.gcName.Visible = true;
            this.gcName.VisibleIndex = 1;
            this.gcName.Width = 180;
            // 
            // gcReportToSectionID
            // 
            this.gcReportToSectionID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcReportToSectionID.AppearanceHeader.Options.UseFont = true;
            this.gcReportToSectionID.Caption = "Report to ID";
            this.gcReportToSectionID.FieldName = "ReportToSectionID";
            this.gcReportToSectionID.Name = "gcReportToSectionID";
            this.gcReportToSectionID.OptionsColumn.AllowEdit = false;
            this.gcReportToSectionID.Visible = true;
            this.gcReportToSectionID.VisibleIndex = 2;
            this.gcReportToSectionID.Width = 120;
            // 
            // gcHierarchicalID
            // 
            this.gcHierarchicalID.AppearanceCell.Options.UseTextOptions = true;
            this.gcHierarchicalID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcHierarchicalID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcHierarchicalID.AppearanceHeader.Options.UseFont = true;
            this.gcHierarchicalID.AppearanceHeader.Options.UseTextOptions = true;
            this.gcHierarchicalID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcHierarchicalID.Caption = "Hierarchical Level";
            this.gcHierarchicalID.FieldName = "HierarchicalID";
            this.gcHierarchicalID.Name = "gcHierarchicalID";
            this.gcHierarchicalID.OptionsColumn.AllowEdit = false;
            this.gcHierarchicalID.Visible = true;
            this.gcHierarchicalID.VisibleIndex = 3;
            this.gcHierarchicalID.Width = 135;
            // 
            // gcOccupation
            // 
            this.gcOccupation.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcOccupation.AppearanceHeader.Options.UseFont = true;
            this.gcOccupation.Caption = "Occupation";
            this.gcOccupation.FieldName = "Occupation";
            this.gcOccupation.Name = "gcOccupation";
            this.gcOccupation.OptionsColumn.AllowEdit = false;
            this.gcOccupation.Visible = true;
            this.gcOccupation.VisibleIndex = 4;
            this.gcOccupation.Width = 169;
            // 
            // gcStatus
            // 
            this.gcStatus.AppearanceCell.Options.UseTextOptions = true;
            this.gcStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStatus.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcStatus.AppearanceHeader.Options.UseFont = true;
            this.gcStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStatus.Caption = "Status";
            this.gcStatus.FieldName = "Status";
            this.gcStatus.Name = "gcStatus";
            this.gcStatus.OptionsColumn.AllowEdit = false;
            this.gcStatus.Width = 118;
            // 
            // gcProdMonth
            // 
            this.gcProdMonth.Caption = "Prod Month";
            this.gcProdMonth.FieldName = "ProdMonth";
            this.gcProdMonth.Name = "gcProdMonth";
            this.gcProdMonth.OptionsColumn.AllowEdit = false;
            // 
            // gcIndustryNumber
            // 
            this.gcIndustryNumber.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcIndustryNumber.AppearanceHeader.Options.UseFont = true;
            this.gcIndustryNumber.Caption = "Industry Number";
            this.gcIndustryNumber.FieldName = "IndustryNo";
            this.gcIndustryNumber.Name = "gcIndustryNumber";
            this.gcIndustryNumber.OptionsColumn.AllowEdit = false;
            this.gcIndustryNumber.Visible = true;
            this.gcIndustryNumber.VisibleIndex = 5;
            this.gcIndustryNumber.Width = 125;
            // 
            // ucSectionScreen
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucSectionScreen";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(868, 350);
            this.Load += new System.EventHandler(this.ucSectionScreen_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editProdmonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSectionScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSectionScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gcSectionScreen;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSectionScreen;
        private DevExpress.XtraGrid.Columns.GridColumn gcSectionID;
        private DevExpress.XtraGrid.Columns.GridColumn gcName;
        private DevExpress.XtraGrid.Columns.GridColumn gcReportToSectionID;
        private DevExpress.XtraGrid.Columns.GridColumn gcHierarchicalID;
        private DevExpress.XtraGrid.Columns.GridColumn gcOccupation;
        private DevExpress.XtraGrid.Columns.GridColumn gcStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gcProdMonth;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblProdMonth;
        private Global.CustomControls.MWButton btnAdd;
        private Global.CustomControls.MWButton btnEdit;
        public Global.CustomControls.MWProdmonthEdit editProdmonth;
        private DevExpress.XtraGrid.Columns.GridColumn gcIndustryNumber;
        private Global.CustomControls.MWButton mwButton1;
    }
}
