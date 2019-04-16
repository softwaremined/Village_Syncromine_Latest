namespace Mineware.Systems.Production.SysAdminScreens.Calendars
{
    partial class ucCalendars
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCalendars));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lueCalendarCode = new DevExpress.XtraEditors.LookUpEdit();
            this.btnAdd = new Mineware.Systems.Global.CustomControls.MWButton();
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton();
            this.gcCalendars = new DevExpress.XtraGrid.GridControl();
            this.gvCalendars = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCalendarCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTotalShifts = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gdNextMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gdNextFirstDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mwButton1 = new Mineware.Systems.Global.CustomControls.MWButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCalendarCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCalendars)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.mwButton1);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel1.Controls.Add(this.lueCalendarCode);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnEdit);
            this.splitContainerControl1.Panel1.MinSize = 25;
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcCalendars);
            this.splitContainerControl1.Panel2.MinSize = 25;
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(892, 576);
            this.splitContainerControl1.SplitterPosition = 40;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(245, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Calendar Code";
            // 
            // lueCalendarCode
            // 
            this.lueCalendarCode.Location = new System.Drawing.Point(339, 10);
            this.lueCalendarCode.Name = "lueCalendarCode";
            this.lueCalendarCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCalendarCode.Properties.NullText = "Select Calendar Code";
            this.lueCalendarCode.Properties.Tag = "";
            this.lueCalendarCode.Size = new System.Drawing.Size(152, 20);
            this.lueCalendarCode.TabIndex = 2;
            this.lueCalendarCode.EditValueChanged += new System.EventHandler(this.lueCalendarCode_EditValueChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageLeftPadding = 0;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(70, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 40);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Add;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ImageLeftPadding = 0;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.Location = new System.Drawing.Point(0, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 40);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "Edit";
            this.btnEdit.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Edit;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // gcCalendars
            // 
            this.gcCalendars.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcCalendars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCalendars.Location = new System.Drawing.Point(0, 0);
            this.gcCalendars.MainView = this.gvCalendars;
            this.gcCalendars.Name = "gcCalendars";
            this.gcCalendars.Size = new System.Drawing.Size(892, 531);
            this.gcCalendars.TabIndex = 1;
            this.gcCalendars.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCalendars});
            // 
            // gvCalendars
            // 
            this.gvCalendars.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcMonth,
            this.gcCalendarCode,
            this.gcStartDate,
            this.gcEndDate,
            this.gcTotalShifts,
            this.gcStatus,
            this.gdNextMonth,
            this.gdNextFirstDate});
            this.gvCalendars.GridControl = this.gcCalendars;
            this.gvCalendars.Name = "gvCalendars";
            this.gvCalendars.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCalendars.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCalendars.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvCalendars.OptionsBehavior.ReadOnly = true;
            this.gvCalendars.OptionsCustomization.AllowColumnMoving = false;
            this.gvCalendars.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.False;
            this.gvCalendars.OptionsView.ShowGroupPanel = false;
            this.gvCalendars.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvSafetyCalendar_RowClick_1);
            this.gvCalendars.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvSafetyCalendar_InitNewRow);
            this.gvCalendars.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvSafetyCalendar_CellValueChanged);
            this.gvCalendars.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvSafetyCalendar_RowUpdated);
            // 
            // gcMonth
            // 
            this.gcMonth.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcMonth.AppearanceHeader.Options.UseFont = true;
            this.gcMonth.AppearanceHeader.Options.UseTextOptions = true;
            this.gcMonth.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcMonth.Caption = "Month";
            this.gcMonth.FieldName = "Month";
            this.gcMonth.Name = "gcMonth";
            this.gcMonth.Visible = true;
            this.gcMonth.VisibleIndex = 0;
            // 
            // gcCalendarCode
            // 
            this.gcCalendarCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcCalendarCode.AppearanceHeader.Options.UseFont = true;
            this.gcCalendarCode.Caption = "Calendar Code";
            this.gcCalendarCode.FieldName = "CalendarCode";
            this.gcCalendarCode.Name = "gcCalendarCode";
            this.gcCalendarCode.Visible = true;
            this.gcCalendarCode.VisibleIndex = 1;
            // 
            // gcStartDate
            // 
            this.gcStartDate.AppearanceCell.Options.UseTextOptions = true;
            this.gcStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStartDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcStartDate.AppearanceHeader.Options.UseFont = true;
            this.gcStartDate.AppearanceHeader.Options.UseTextOptions = true;
            this.gcStartDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcStartDate.Caption = "Start Date";
            this.gcStartDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gcStartDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcStartDate.FieldName = "StartDate";
            this.gcStartDate.Name = "gcStartDate";
            this.gcStartDate.Visible = true;
            this.gcStartDate.VisibleIndex = 2;
            // 
            // gcEndDate
            // 
            this.gcEndDate.AppearanceCell.Options.UseTextOptions = true;
            this.gcEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcEndDate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcEndDate.AppearanceHeader.Options.UseFont = true;
            this.gcEndDate.AppearanceHeader.Options.UseTextOptions = true;
            this.gcEndDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcEndDate.Caption = "End Date";
            this.gcEndDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.gcEndDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcEndDate.FieldName = "EndDate";
            this.gcEndDate.Name = "gcEndDate";
            this.gcEndDate.Visible = true;
            this.gcEndDate.VisibleIndex = 3;
            // 
            // gcTotalShifts
            // 
            this.gcTotalShifts.AppearanceCell.Options.UseTextOptions = true;
            this.gcTotalShifts.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcTotalShifts.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gcTotalShifts.AppearanceHeader.Options.UseFont = true;
            this.gcTotalShifts.AppearanceHeader.Options.UseTextOptions = true;
            this.gcTotalShifts.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcTotalShifts.Caption = "Total Shifts";
            this.gcTotalShifts.FieldName = "TotalShifts";
            this.gcTotalShifts.Name = "gcTotalShifts";
            this.gcTotalShifts.Visible = true;
            this.gcTotalShifts.VisibleIndex = 4;
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
            // 
            // gdNextMonth
            // 
            this.gdNextMonth.Caption = "gridColumn1";
            this.gdNextMonth.FieldName = "NextMonth";
            this.gdNextMonth.Name = "gdNextMonth";
            // 
            // gdNextFirstDate
            // 
            this.gdNextFirstDate.Caption = "gridColumn1";
            this.gdNextFirstDate.FieldName = "NextFirstDate";
            this.gdNextFirstDate.Name = "gdNextFirstDate";
            // 
            // mwButton1
            // 
            this.mwButton1.ImageLeftPadding = 0;
            this.mwButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mwButton1.ImageOptions.Image")));
            this.mwButton1.Location = new System.Drawing.Point(140, 0);
            this.mwButton1.Name = "mwButton1";
            this.mwButton1.Size = new System.Drawing.Size(70, 40);
            this.mwButton1.TabIndex = 4;
            this.mwButton1.Text = "Refresh";
            this.mwButton1.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Show;
            this.mwButton1.Click += new System.EventHandler(this.mwButton1_Click);
            // 
            // ucCalendars
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ucCalendars";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(892, 576);
            this.Load += new System.EventHandler(this.ucSafetyCalendar_Load);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueCalendarCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCalendars)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcCalendars;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCalendars;
        private DevExpress.XtraGrid.Columns.GridColumn gcMonth;
        private DevExpress.XtraGrid.Columns.GridColumn gcStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcTotalShifts;
        private DevExpress.XtraGrid.Columns.GridColumn gcStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gdNextMonth;
        private DevExpress.XtraGrid.Columns.GridColumn gdNextFirstDate;
        private Mineware .Systems .Global .CustomControls .MWButton btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gcCalendarCode;
        private Mineware.Systems.Global.CustomControls.MWButton btnAdd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.LookUpEdit lueCalendarCode;
        private Global.CustomControls.MWButton mwButton1;
    }
}
