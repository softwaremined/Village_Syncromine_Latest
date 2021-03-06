﻿namespace Mineware.Systems.Production.SysAdminScreens.CalendarTypes
{
    partial class ucCalendarTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCalendarTypes));
            DevExpress.XtraScheduler.TimeRuler timeRuler1 = new DevExpress.XtraScheduler.TimeRuler();
            DevExpress.XtraScheduler.TimeRuler timeRuler2 = new DevExpress.XtraScheduler.TimeRuler();
            this.gcCalTypes = new DevExpress.XtraGrid.GridControl();
            this.gvCalTypes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCalendarCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reCalCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcCalType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSetNWDay = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnSetWDay = new Mineware.Systems.Global.CustomControls.MWButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.scWorkingNonWorking = new DevExpress.XtraScheduler.SchedulerControl();
            this.ssWorkingDays = new DevExpress.XtraScheduler.SchedulerStorage();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.dnMain = new DevExpress.XtraScheduler.DateNavigator();
            ((System.ComponentModel.ISupportInitialize)(this.gcCalTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCalTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reCalCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scWorkingNonWorking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ssWorkingDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dnMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dnMain.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcCalTypes
            // 
            this.gcCalTypes.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcCalTypes.Location = new System.Drawing.Point(0, 40);
            this.gcCalTypes.MainView = this.gvCalTypes;
            this.gcCalTypes.Name = "gcCalTypes";
            this.gcCalTypes.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reCalCode});
            this.gcCalTypes.Size = new System.Drawing.Size(362, 637);
            this.gcCalTypes.TabIndex = 4;
            this.gcCalTypes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCalTypes});
            this.gcCalTypes.Click += new System.EventHandler(this.gcCalTypes_Click);
            // 
            // gvCalTypes
            // 
            this.gvCalTypes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCalendarCode,
            this.gcCalType});
            this.gvCalTypes.GridControl = this.gcCalTypes;
            this.gvCalTypes.Name = "gvCalTypes";
            this.gvCalTypes.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvCalTypes.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvCalTypes.OptionsView.ShowGroupPanel = false;
            this.gvCalTypes.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvCalTypes_FocusedRowChanged);
            this.gvCalTypes.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvCalTypes_RowUpdated);
            this.gvCalTypes.DoubleClick += new System.EventHandler(this.gvCalTypes_DoubleClick);
            // 
            // gcCalendarCode
            // 
            this.gcCalendarCode.Caption = "Calendar Code";
            this.gcCalendarCode.ColumnEdit = this.reCalCode;
            this.gcCalendarCode.FieldName = "CalendarCode";
            this.gcCalendarCode.Name = "gcCalendarCode";
            this.gcCalendarCode.Visible = true;
            this.gcCalendarCode.VisibleIndex = 0;
            this.gcCalendarCode.Width = 191;
            // 
            // reCalCode
            // 
            this.reCalCode.AutoHeight = false;
            this.reCalCode.MaxLength = 20;
            this.reCalCode.Name = "reCalCode";
            // 
            // gcCalType
            // 
            this.gcCalType.Caption = "Type";
            this.gcCalType.FieldName = "Description";
            this.gcCalType.Name = "gcCalType";
            this.gcCalType.Visible = true;
            this.gcCalType.VisibleIndex = 1;
            this.gcCalType.Width = 153;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnSetNWDay);
            this.panelControl1.Controls.Add(this.btnSetWDay);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(968, 40);
            this.panelControl1.TabIndex = 5;
            // 
            // btnSetNWDay
            // 
            this.btnSetNWDay.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnSetNWDay.Appearance.Options.UseForeColor = true;
            this.btnSetNWDay.Appearance.Options.UseImage = true;
            this.btnSetNWDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetNWDay.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSetNWDay.ImageLeftPadding = 0;
            this.btnSetNWDay.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSetNWDay.ImageOptions.Image")));
            this.btnSetNWDay.Location = new System.Drawing.Point(666, 0);
            this.btnSetNWDay.Name = "btnSetNWDay";
            this.btnSetNWDay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSetNWDay.Size = new System.Drawing.Size(139, 40);
            this.btnSetNWDay.TabIndex = 20;
            this.btnSetNWDay.Text = "Set Non Working Day";
            this.btnSetNWDay.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Custome;
            this.btnSetNWDay.Click += new System.EventHandler(this.btnSetNWDay_Click);
            // 
            // btnSetWDay
            // 
            this.btnSetWDay.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnSetWDay.Appearance.Options.UseForeColor = true;
            this.btnSetWDay.Appearance.Options.UseImage = true;
            this.btnSetWDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetWDay.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSetWDay.ImageLeftPadding = 0;
            this.btnSetWDay.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSetWDay.ImageOptions.Image")));
            this.btnSetWDay.Location = new System.Drawing.Point(546, 0);
            this.btnSetWDay.Name = "btnSetWDay";
            this.btnSetWDay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSetWDay.Size = new System.Drawing.Size(120, 40);
            this.btnSetWDay.TabIndex = 19;
            this.btnSetWDay.Text = "Set Working Day";
            this.btnSetWDay.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Custome;
            this.btnSetWDay.Click += new System.EventHandler(this.btnSetWDay_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl4.Location = new System.Drawing.Point(140, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(406, 40);
            this.panelControl4.TabIndex = 21;
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.btnAdd.Appearance.Options.UseForeColor = true;
            this.btnAdd.Appearance.Options.UseImage = true;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.ImageLeftPadding = 0;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(70, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAdd.Size = new System.Drawing.Size(70, 40);
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "Add";
            this.btnAdd.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Add;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.scWorkingNonWorking);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(362, 40);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(606, 637);
            this.panelControl2.TabIndex = 6;
            // 
            // scWorkingNonWorking
            // 
            this.scWorkingNonWorking.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Month;
            this.scWorkingNonWorking.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.scWorkingNonWorking.DataStorage = this.ssWorkingDays;
            this.scWorkingNonWorking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scWorkingNonWorking.Location = new System.Drawing.Point(0, 411);
            this.scWorkingNonWorking.Name = "scWorkingNonWorking";
            this.scWorkingNonWorking.OptionsBehavior.MouseWheelScrollAction = DevExpress.XtraScheduler.MouseWheelScrollAction.Auto;
            this.scWorkingNonWorking.OptionsBehavior.ShowRemindersForm = false;
            this.scWorkingNonWorking.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None;
            this.scWorkingNonWorking.OptionsRangeControl.AllowChangeActiveView = false;
            this.scWorkingNonWorking.OptionsView.NavigationButtons.Visibility = DevExpress.XtraScheduler.NavigationButtonVisibility.Never;
            this.scWorkingNonWorking.Size = new System.Drawing.Size(606, 226);
            this.scWorkingNonWorking.Start = new System.DateTime(2014, 9, 14, 0, 0, 0, 0);
            this.scWorkingNonWorking.TabIndex = 1;
            this.scWorkingNonWorking.Text = "scWorkingNonWorking";
            this.scWorkingNonWorking.Views.DayView.Enabled = false;
            this.scWorkingNonWorking.Views.DayView.TimeRulers.Add(timeRuler1);
            this.scWorkingNonWorking.Views.GanttView.Enabled = false;
            this.scWorkingNonWorking.Views.MonthView.AppointmentDisplayOptions.AppointmentAutoHeight = true;
            this.scWorkingNonWorking.Views.MonthView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never;
            this.scWorkingNonWorking.Views.TimelineView.Enabled = false;
            this.scWorkingNonWorking.Views.WeekView.Enabled = false;
            this.scWorkingNonWorking.Views.WorkWeekView.Enabled = false;
            this.scWorkingNonWorking.Views.WorkWeekView.TimeRulers.Add(timeRuler2);
            this.scWorkingNonWorking.Visible = false;
            this.scWorkingNonWorking.EditAppointmentFormShowing += new DevExpress.XtraScheduler.AppointmentFormEventHandler(this.schedulerControl1_EditAppointmentFormShowing);
            // 
            // panelControl3
            // 
            this.panelControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.dnMain);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(606, 411);
            this.panelControl3.TabIndex = 3;
            // 
            // dnMain
            // 
            this.dnMain.BoldAppointmentDates = false;
            this.dnMain.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dnMain.CellPadding = new System.Windows.Forms.Padding(2);
            this.dnMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.dnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dnMain.FirstDayOfWeek = System.DayOfWeek.Sunday;
            this.dnMain.HighlightHolidays = false;
            this.dnMain.Location = new System.Drawing.Point(0, 0);
            this.dnMain.Name = "dnMain";
            this.dnMain.SchedulerControl = this.scWorkingNonWorking;
            this.dnMain.SelectionBehavior = DevExpress.XtraEditors.Controls.CalendarSelectionBehavior.Simple;
            this.dnMain.ShowTodayButton = false;
            this.dnMain.ShowWeekNumbers = false;
            this.dnMain.ShowYearNavigationButtons = DevExpress.Utils.DefaultBoolean.False;
            this.dnMain.Size = new System.Drawing.Size(606, 411);
            this.dnMain.TabIndex = 3;
            this.dnMain.CustomDrawDayNumberCell += new DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventHandler(this.dnMain_CustomDrawDayNumberCell);
            // 
            // ucCalendarTypes
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.gcCalTypes);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucCalendarTypes";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(968, 677);
            this.Load += new System.EventHandler(this.ucCalendarTypes_Load);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.gcCalTypes, 0);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gcCalTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCalTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reCalCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scWorkingNonWorking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ssWorkingDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dnMain.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dnMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcCalTypes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCalTypes;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Global.CustomControls.MWButton btnSetWDay;
        private Global.CustomControls.MWButton btnEdit;
        private Global.CustomControls.MWButton btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraScheduler.SchedulerControl scWorkingNonWorking;
        private DevExpress.XtraScheduler.SchedulerStorage ssWorkingDays;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraScheduler.DateNavigator dnMain;
        private DevExpress.XtraGrid.Columns.GridColumn gcCalendarCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcCalType;
        private Global.CustomControls.MWButton btnSetNWDay;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reCalCode;
    }
}
