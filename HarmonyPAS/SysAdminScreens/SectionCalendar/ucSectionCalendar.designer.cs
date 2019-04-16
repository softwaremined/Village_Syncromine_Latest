namespace Mineware.Systems.Production.SysAdminScreens.SectionCalendar
{
    partial class ucSectionCalendar
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSectionCalendar));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labProdMonth = new DevExpress.XtraEditors.LabelControl();
            this.editProdmonth = new Mineware.Systems.Global.CustomControls.MWProdmonthEdit();
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.gcSectionCal = new DevExpress.XtraGrid.GridControl();
            this.gvSectionCal = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSupervisor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCalendarType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTotalShifts = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLastDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcFirstDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewConvBeginDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewBeginDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrevMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTestDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editProdmonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSectionCal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSectionCal)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(639, 40);
            this.panelControl1.TabIndex = 6;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.labProdMonth);
            this.panelControl2.Controls.Add(this.editProdmonth);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(70, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(235, 40);
            this.panelControl2.TabIndex = 23;
            // 
            // labProdMonth
            // 
            this.labProdMonth.Location = new System.Drawing.Point(17, 13);
            this.labProdMonth.Name = "labProdMonth";
            this.labProdMonth.Size = new System.Drawing.Size(84, 13);
            this.labProdMonth.TabIndex = 24;
            this.labProdMonth.Text = "Production Month";
            // 
            // editProdmonth
            // 
            this.editProdmonth.EditValue = "2014-03-04 10:58:26 AM";
            this.editProdmonth.Location = new System.Drawing.Point(116, 10);
            this.editProdmonth.Name = "editProdmonth";
            this.editProdmonth.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.editProdmonth.Properties.Appearance.Options.UseBackColor = true;
            this.editProdmonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2)});
            this.editProdmonth.Properties.Mask.EditMask = "yyyyMM";
            this.editProdmonth.Properties.Mask.IgnoreMaskBlank = false;
            this.editProdmonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.editProdmonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.editProdmonth.Properties.ReadOnly = true;
            this.editProdmonth.Size = new System.Drawing.Size(106, 20);
            this.editProdmonth.TabIndex = 23;
            this.editProdmonth.EditValueChanged += new System.EventHandler(this.editProdmonth_EditValueChanged);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnEdit.Appearance.Options.UseForeColor = true;
            this.btnEdit.Appearance.Options.UseImage = true;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEdit.ImageLeftPadding = 0;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.Location = new System.Drawing.Point(0, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEdit.Size = new System.Drawing.Size(70, 40);
            this.btnEdit.TabIndex = 18;
            this.btnEdit.Text = "Edit";
            this.btnEdit.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Edit;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click_1);
            // 
            // gcSectionCal
            // 
            this.gcSectionCal.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcSectionCal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSectionCal.Location = new System.Drawing.Point(0, 40);
            this.gcSectionCal.MainView = this.gvSectionCal;
            this.gcSectionCal.Name = "gcSectionCal";
            this.gcSectionCal.Size = new System.Drawing.Size(639, 432);
            this.gcSectionCal.TabIndex = 7;
            this.gcSectionCal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSectionCal});
            // 
            // gvSectionCal
            // 
            this.gvSectionCal.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcSection,
            this.gcSupervisor,
            this.gcCalendarType,
            this.colBDate,
            this.colEDate,
            this.gcTotalShifts,
            this.gcSect,
            this.gcLastDay,
            this.gcFirstDay,
            this.colNewConvBeginDate,
            this.colNewBeginDate,
            this.colPrevMonth,
            this.colStatus,
            this.colTestDate});
            this.gvSectionCal.GridControl = this.gcSectionCal;
            this.gvSectionCal.Name = "gvSectionCal";
            this.gvSectionCal.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvSectionCal.OptionsCustomization.AllowColumnMoving = false;
            this.gvSectionCal.OptionsCustomization.AllowColumnResizing = false;
            this.gvSectionCal.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            this.gvSectionCal.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gvSectionCal.OptionsView.ShowGroupPanel = false;
            this.gvSectionCal.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvSectionCal_RowClick);
            this.gvSectionCal.ShowingPopupEditForm += new DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventHandler(this.gvSectionCal_ShowingPopupEditForm);
            this.gvSectionCal.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvSectionCal_CellValueChanged);
            this.gvSectionCal.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvSectionCal_RowUpdated);
            // 
            // gcSection
            // 
            this.gcSection.Caption = "Section";
            this.gcSection.FieldName = "sectionid";
            this.gcSection.Name = "gcSection";
            this.gcSection.OptionsColumn.AllowEdit = false;
            this.gcSection.OptionsColumn.FixedWidth = true;
            this.gcSection.Visible = true;
            this.gcSection.VisibleIndex = 0;
            this.gcSection.Width = 100;
            // 
            // gcSupervisor
            // 
            this.gcSupervisor.Caption = "Supervisor";
            this.gcSupervisor.FieldName = "name";
            this.gcSupervisor.Name = "gcSupervisor";
            this.gcSupervisor.OptionsColumn.AllowEdit = false;
            this.gcSupervisor.OptionsColumn.FixedWidth = true;
            this.gcSupervisor.Visible = true;
            this.gcSupervisor.VisibleIndex = 1;
            this.gcSupervisor.Width = 120;
            // 
            // gcCalendarType
            // 
            this.gcCalendarType.AppearanceCell.Options.UseTextOptions = true;
            this.gcCalendarType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCalendarType.AppearanceHeader.Options.UseTextOptions = true;
            this.gcCalendarType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcCalendarType.Caption = "Calendar Type";
            this.gcCalendarType.FieldName = "calendarcode";
            this.gcCalendarType.Name = "gcCalendarType";
            this.gcCalendarType.OptionsColumn.AllowEdit = false;
            this.gcCalendarType.OptionsColumn.FixedWidth = true;
            this.gcCalendarType.Visible = true;
            this.gcCalendarType.VisibleIndex = 2;
            this.gcCalendarType.Width = 80;
            // 
            // colBDate
            // 
            this.colBDate.AppearanceCell.Options.UseTextOptions = true;
            this.colBDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colBDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBDate.Caption = "First Date";
            this.colBDate.FieldName = "BDate";
            this.colBDate.Name = "colBDate";
            this.colBDate.OptionsColumn.FixedWidth = true;
            this.colBDate.Visible = true;
            this.colBDate.VisibleIndex = 4;
            this.colBDate.Width = 80;
            // 
            // colEDate
            // 
            this.colEDate.AppearanceCell.Options.UseTextOptions = true;
            this.colEDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colEDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEDate.Caption = "Last Date";
            this.colEDate.FieldName = "EDate";
            this.colEDate.Name = "colEDate";
            this.colEDate.OptionsColumn.FixedWidth = true;
            this.colEDate.Visible = true;
            this.colEDate.VisibleIndex = 5;
            this.colEDate.Width = 80;
            // 
            // gcTotalShifts
            // 
            this.gcTotalShifts.AppearanceCell.Options.UseTextOptions = true;
            this.gcTotalShifts.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcTotalShifts.AppearanceHeader.Options.UseTextOptions = true;
            this.gcTotalShifts.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcTotalShifts.Caption = "Total Shifts";
            this.gcTotalShifts.FieldName = "totalshifts";
            this.gcTotalShifts.Name = "gcTotalShifts";
            this.gcTotalShifts.OptionsColumn.AllowEdit = false;
            this.gcTotalShifts.OptionsColumn.FixedWidth = true;
            this.gcTotalShifts.Visible = true;
            this.gcTotalShifts.VisibleIndex = 3;
            this.gcTotalShifts.Width = 80;
            // 
            // gcSect
            // 
            this.gcSect.Caption = "sect";
            this.gcSect.FieldName = "sectionid";
            this.gcSect.Name = "gcSect";
            this.gcSect.OptionsColumn.AllowEdit = false;
            // 
            // gcLastDay
            // 
            this.gcLastDay.Caption = "Last Day";
            this.gcLastDay.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gcLastDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcLastDay.FieldName = "enddate";
            this.gcLastDay.Name = "gcLastDay";
            this.gcLastDay.OptionsColumn.AllowEdit = false;
            this.gcLastDay.Width = 91;
            // 
            // gcFirstDay
            // 
            this.gcFirstDay.Caption = "First Day";
            this.gcFirstDay.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gcFirstDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcFirstDay.FieldName = "begindate";
            this.gcFirstDay.Name = "gcFirstDay";
            this.gcFirstDay.OptionsColumn.AllowEdit = false;
            this.gcFirstDay.Width = 91;
            // 
            // colNewConvBeginDate
            // 
            this.colNewConvBeginDate.Caption = "NewBeginDate(char)";
            this.colNewConvBeginDate.FieldName = "NewConvBeginDate";
            this.colNewConvBeginDate.Name = "colNewConvBeginDate";
            // 
            // colNewBeginDate
            // 
            this.colNewBeginDate.Caption = "colNewBeginDate(Date)";
            this.colNewBeginDate.DisplayFormat.FormatString = "d";
            this.colNewBeginDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNewBeginDate.FieldName = "NewBeginDate";
            this.colNewBeginDate.Name = "colNewBeginDate";
            // 
            // colPrevMonth
            // 
            this.colPrevMonth.Caption = "PrevMonth";
            this.colPrevMonth.FieldName = "PrevMonth";
            this.colPrevMonth.Name = "colPrevMonth";
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            // 
            // colTestDate
            // 
            this.colTestDate.Caption = "To force on change";
            this.colTestDate.FieldName = "TestDate";
            this.colTestDate.Name = "colTestDate";
            // 
            // ucSectionCalendar
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcSectionCal);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucSectionCalendar";
            this.ShowIInfo = false;
            this.Load += new System.EventHandler(this.ucSectionCalendars_Load);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.gcSectionCal, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editProdmonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSectionCal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSectionCal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Global.CustomControls.MWButton btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gcSection;
        private DevExpress.XtraGrid.Columns.GridColumn gcSupervisor;
        private DevExpress.XtraGrid.Columns.GridColumn gcCalendarType;
        private DevExpress.XtraGrid.Columns.GridColumn gcFirstDay;
        private DevExpress.XtraGrid.Columns.GridColumn gcLastDay;
        private DevExpress.XtraGrid.Columns.GridColumn gcTotalShifts;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Global.CustomControls.MWProdmonthEdit editProdmonth;
        private DevExpress.XtraEditors.LabelControl labProdMonth;
        private DevExpress.XtraGrid.Columns.GridColumn gcSect;
        public DevExpress.XtraGrid.Views.Grid.GridView gvSectionCal;
        public DevExpress.XtraGrid.GridControl gcSectionCal;
        private DevExpress.XtraGrid.Columns.GridColumn colBDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEDate;
        private DevExpress.XtraGrid.Columns.GridColumn colNewConvBeginDate;
        private DevExpress.XtraGrid.Columns.GridColumn colNewBeginDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPrevMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colTestDate;
    }
}
