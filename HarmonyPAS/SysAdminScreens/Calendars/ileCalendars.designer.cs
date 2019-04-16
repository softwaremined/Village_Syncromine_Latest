namespace Mineware.Systems.Production.SysAdminScreens.Calendars
{
    partial class ileCalendars
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ileCalendars));
            this.lblStartDate = new DevExpress.XtraEditors.LabelControl();
            this.lblEndDate = new DevExpress.XtraEditors.LabelControl();
            this.lblMonth = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalShifts = new DevExpress.XtraEditors.TextEdit();
            this.blTotalShifts = new DevExpress.XtraEditors.LabelControl();
            this.txtMonth = new DevExpress.XtraEditors.TextEdit();
            this.dtStartdate = new DevExpress.XtraEditors.DateEdit();
            this.dtEndDate = new DevExpress.XtraEditors.DateEdit();
            this.lblCalendarType = new DevExpress.XtraEditors.LabelControl();
            this.txtCalendarCode = new DevExpress.XtraEditors.TextEdit();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalShifts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartdate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCalendarCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStartDate
            // 
            this.lblStartDate.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblStartDate.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblStartDate, "");
            this.lblStartDate.Location = new System.Drawing.Point(407, 27);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(52, 12);
            this.lblStartDate.TabIndex = 32;
            this.lblStartDate.Text = "Start Date";
            // 
            // lblEndDate
            // 
            this.lblEndDate.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblEndDate.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblEndDate, "");
            this.lblEndDate.Location = new System.Drawing.Point(633, 27);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(45, 12);
            this.lblEndDate.TabIndex = 33;
            this.lblEndDate.Text = "End Date";
            // 
            // lblMonth
            // 
            this.lblMonth.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblMonth.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblMonth, "");
            this.lblMonth.Location = new System.Drawing.Point(13, 28);
            this.lblMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(31, 12);
            this.lblMonth.TabIndex = 34;
            this.lblMonth.Text = "Month";
            // 
            // txtTotalShifts
            // 
            this.SetBoundFieldName(this.txtTotalShifts, "TotalShifts");
            this.SetBoundPropertyName(this.txtTotalShifts, "EditValue");
            this.txtTotalShifts.Enabled = false;
            this.txtTotalShifts.Location = new System.Drawing.Point(900, 25);
            this.txtTotalShifts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotalShifts.Name = "txtTotalShifts";
            this.txtTotalShifts.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalShifts.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTotalShifts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalShifts.Size = new System.Drawing.Size(46, 20);
            this.txtTotalShifts.TabIndex = 4;
            this.txtTotalShifts.EditValueChanged += new System.EventHandler(this.txtTotalShifts_EditValueChanged);
            // 
            // blTotalShifts
            // 
            this.blTotalShifts.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.blTotalShifts.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.blTotalShifts, "");
            this.blTotalShifts.Location = new System.Drawing.Point(834, 28);
            this.blTotalShifts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.blTotalShifts.Name = "blTotalShifts";
            this.blTotalShifts.Size = new System.Drawing.Size(57, 12);
            this.blTotalShifts.TabIndex = 36;
            this.blTotalShifts.Text = "Total Shifts";
            // 
            // txtMonth
            // 
            this.SetBoundFieldName(this.txtMonth, "Month");
            this.SetBoundPropertyName(this.txtMonth, "EditValue");
            this.txtMonth.Enabled = false;
            this.txtMonth.Location = new System.Drawing.Point(50, 24);
            this.txtMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMonth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMonth.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMonth.Size = new System.Drawing.Size(72, 20);
            this.txtMonth.TabIndex = 37;
            // 
            // dtStartdate
            // 
            this.SetBoundFieldName(this.dtStartdate, "StartDate");
            this.SetBoundPropertyName(this.dtStartdate, "EditValue");
            this.dtStartdate.EditValue = null;
            this.dtStartdate.Location = new System.Drawing.Point(465, 24);
            this.dtStartdate.Name = "dtStartdate";
            this.dtStartdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStartdate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStartdate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtStartdate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtStartdate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtStartdate.Size = new System.Drawing.Size(127, 20);
            this.dtStartdate.TabIndex = 38;
            this.dtStartdate.DrawItem += new DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventHandler(this.dtStartdate_DrawItem);
            this.dtStartdate.EditValueChanged += new System.EventHandler(this.dtStartdate_EditValueChanged);
            this.dtStartdate.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.dtStartdate_EditValueChanging);
            this.dtStartdate.Enter += new System.EventHandler(this.dtStartdate_Enter);
            // 
            // dtEndDate
            // 
            this.SetBoundFieldName(this.dtEndDate, "EndDate");
            this.SetBoundPropertyName(this.dtEndDate, "EditValue");
            this.dtEndDate.EditValue = null;
            this.dtEndDate.Location = new System.Drawing.Point(684, 24);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtEndDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtEndDate.Size = new System.Drawing.Size(126, 20);
            this.dtEndDate.TabIndex = 39;
            this.dtEndDate.EditValueChanged += new System.EventHandler(this.dtEndDate_EditValueChanged);
            this.dtEndDate.Enter += new System.EventHandler(this.dtEndDate_Enter);
            // 
            // lblCalendarType
            // 
            this.lblCalendarType.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCalendarType.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblCalendarType, "");
            this.lblCalendarType.Location = new System.Drawing.Point(156, 28);
            this.lblCalendarType.Name = "lblCalendarType";
            this.lblCalendarType.Size = new System.Drawing.Size(81, 13);
            this.lblCalendarType.TabIndex = 40;
            this.lblCalendarType.Text = "Calendar Type";
            // 
            // txtCalendarCode
            // 
            this.SetBoundFieldName(this.txtCalendarCode, "CalendarCode");
            this.SetBoundPropertyName(this.txtCalendarCode, "EditValue");
            this.txtCalendarCode.Enabled = false;
            this.txtCalendarCode.Location = new System.Drawing.Point(243, 25);
            this.txtCalendarCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCalendarCode.Name = "txtCalendarCode";
            this.txtCalendarCode.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCalendarCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCalendarCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCalendarCode.Size = new System.Drawing.Size(124, 20);
            this.txtCalendarCode.TabIndex = 42;
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(856, 65);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(79, 24);
            this.btnUpdate.TabIndex = 43;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // ileCalendars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtCalendarCode);
            this.Controls.Add(this.lblCalendarType);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.dtStartdate);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtTotalShifts);
            this.Controls.Add(this.blTotalShifts);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ileCalendars";
            this.Size = new System.Drawing.Size(952, 92);
            this.Load += new System.EventHandler(this.ileCalendars_Load);
            this.Enter += new System.EventHandler(this.ileSafetyCalendar_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalShifts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartdate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCalendarCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblMonth;
        private DevExpress.XtraEditors.LabelControl lblEndDate;
        private DevExpress.XtraEditors.LabelControl lblStartDate;
        private DevExpress.XtraEditors.TextEdit txtTotalShifts;
        private DevExpress.XtraEditors.LabelControl blTotalShifts;
        private DevExpress.XtraEditors.TextEdit txtMonth;
        private DevExpress.XtraEditors.LabelControl lblCalendarType;
        public DevExpress.XtraEditors.DateEdit dtEndDate;
        public DevExpress.XtraEditors.DateEdit dtStartdate;
        private DevExpress.XtraEditors.TextEdit txtCalendarCode;
        public DevExpress.XtraEditors.SimpleButton btnUpdate;
    }
}