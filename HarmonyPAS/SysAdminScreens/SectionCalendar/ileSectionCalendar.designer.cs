namespace Mineware.Systems.Production.SysAdminScreens.SectionCalendar
{
    partial class ileSectionCalendar
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
            this.lstAvailable = new DevExpress.XtraEditors.ListBoxControl();
            this.lstApply = new DevExpress.XtraEditors.ListBoxControl();
            this.labAvailableShiftbosses = new DevExpress.XtraEditors.LabelControl();
            this.btnAvailtoApply = new DevExpress.XtraEditors.SimpleButton();
            this.btnApplytoAvail = new DevExpress.XtraEditors.SimpleButton();
            this.txtProdMonth = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.grpbx5 = new System.Windows.Forms.GroupBox();
            this.lblTotalShifts = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dteBeginDate = new DevExpress.XtraEditors.DateEdit();
            this.dteEndDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbCalTypes = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProviderSection = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dteTestDate = new DevExpress.XtraEditors.DateEdit();
            this.txtPrevMonth = new DevExpress.XtraEditors.TextEdit();
            this.txtSectionID = new DevExpress.XtraEditors.TextEdit();
            this.dteNewBeginDate = new DevExpress.XtraEditors.DateEdit();
            this.txtNewConvBeginDate = new DevExpress.XtraEditors.TextEdit();
            this.txtEDate = new DevExpress.XtraEditors.TextEdit();
            this.txtBDate = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lstAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).BeginInit();
            this.grpbx5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteBeginDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCalTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteTestDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTestDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrevMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNewBeginDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNewBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewConvBeginDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lstAvailable
            // 
            this.SetBoundPropertyName(this.lstAvailable, "");
            this.lstAvailable.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstAvailable.Location = new System.Drawing.Point(14, 25);
            this.lstAvailable.Name = "lstAvailable";
            this.lstAvailable.Size = new System.Drawing.Size(272, 192);
            this.lstAvailable.TabIndex = 0;
            this.lstAvailable.SelectedIndexChanged += new System.EventHandler(this.lstAvailable_SelectedIndexChanged);
            this.lstAvailable.DoubleClick += new System.EventHandler(this.lstAvailable_DoubleClick);
            // 
            // lstApply
            // 
            this.SetBoundFieldName(this.lstApply, "sectionid");
            this.lstApply.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstApply.Location = new System.Drawing.Point(341, 25);
            this.lstApply.Name = "lstApply";
            this.lstApply.Size = new System.Drawing.Size(267, 192);
            this.lstApply.TabIndex = 1;
            this.lstApply.DoubleClick += new System.EventHandler(this.lstApply_DoubleClick);
            // 
            // labAvailableShiftbosses
            // 
            this.SetBoundPropertyName(this.labAvailableShiftbosses, "");
            this.labAvailableShiftbosses.Location = new System.Drawing.Point(13, 5);
            this.labAvailableShiftbosses.Name = "labAvailableShiftbosses";
            this.labAvailableShiftbosses.Size = new System.Drawing.Size(101, 13);
            this.labAvailableShiftbosses.TabIndex = 3;
            this.labAvailableShiftbosses.Text = "Available Shiftbosses";
            // 
            // btnAvailtoApply
            // 
            this.SetBoundPropertyName(this.btnAvailtoApply, "");
            this.btnAvailtoApply.Location = new System.Drawing.Point(292, 80);
            this.btnAvailtoApply.Name = "btnAvailtoApply";
            this.btnAvailtoApply.Size = new System.Drawing.Size(39, 23);
            this.btnAvailtoApply.TabIndex = 4;
            this.btnAvailtoApply.Text = ">";
            this.btnAvailtoApply.Click += new System.EventHandler(this.btnAvailtoApply_Click);
            // 
            // btnApplytoAvail
            // 
            this.SetBoundPropertyName(this.btnApplytoAvail, "");
            this.btnApplytoAvail.Location = new System.Drawing.Point(292, 109);
            this.btnApplytoAvail.Name = "btnApplytoAvail";
            this.btnApplytoAvail.Size = new System.Drawing.Size(39, 23);
            this.btnApplytoAvail.TabIndex = 5;
            this.btnApplytoAvail.Text = "<";
            this.btnApplytoAvail.Click += new System.EventHandler(this.btnApplytoAvail_Click);
            // 
            // txtProdMonth
            // 
            this.SetBoundPropertyName(this.txtProdMonth, "");
            this.txtProdMonth.Enabled = false;
            this.txtProdMonth.Location = new System.Drawing.Point(109, 228);
            this.txtProdMonth.Name = "txtProdMonth";
            this.txtProdMonth.Size = new System.Drawing.Size(100, 20);
            this.txtProdMonth.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.SetBoundPropertyName(this.labelControl1, "");
            this.labelControl1.Location = new System.Drawing.Point(14, 231);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "ProdMonth";
            // 
            // grpbx5
            // 
            this.SetBoundPropertyName(this.grpbx5, "");
            this.grpbx5.Controls.Add(this.lblTotalShifts);
            this.grpbx5.Location = new System.Drawing.Point(278, 234);
            this.grpbx5.Name = "grpbx5";
            this.grpbx5.Size = new System.Drawing.Size(76, 40);
            this.grpbx5.TabIndex = 8;
            this.grpbx5.TabStop = false;
            this.grpbx5.Text = "Total Shifts";
            // 
            // lblTotalShifts
            // 
            this.SetBoundFieldName(this.lblTotalShifts, "totalshifts");
            this.lblTotalShifts.Location = new System.Drawing.Point(30, 20);
            this.lblTotalShifts.Name = "lblTotalShifts";
            this.lblTotalShifts.Size = new System.Drawing.Size(6, 13);
            this.lblTotalShifts.TabIndex = 0;
            this.lblTotalShifts.Text = "0";
            // 
            // labelControl2
            // 
            this.SetBoundPropertyName(this.labelControl2, "");
            this.labelControl2.Location = new System.Drawing.Point(13, 255);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Calendar Types";
            // 
            // labelControl3
            // 
            this.SetBoundPropertyName(this.labelControl3, "");
            this.labelControl3.Location = new System.Drawing.Point(13, 283);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "Begin Date";
            // 
            // labelControl4
            // 
            this.SetBoundPropertyName(this.labelControl4, "");
            this.labelControl4.Location = new System.Drawing.Point(13, 309);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(44, 13);
            this.labelControl4.TabIndex = 11;
            this.labelControl4.Text = "End Date";
            // 
            // dteBeginDate
            // 
            this.SetBoundFieldName(this.dteBeginDate, "begindate");
            this.SetBoundPropertyName(this.dteBeginDate, "EditValue");
            this.dteBeginDate.EditValue = "";
            this.dteBeginDate.Location = new System.Drawing.Point(109, 280);
            this.dteBeginDate.Name = "dteBeginDate";
            this.dteBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBeginDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteBeginDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dteBeginDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteBeginDate.Properties.NullDate = "";
            this.dteBeginDate.Size = new System.Drawing.Size(100, 20);
            this.dteBeginDate.TabIndex = 12;
            this.dteBeginDate.EditValueChanged += new System.EventHandler(this.dteBeginDate_EditValueChanged);
            // 
            // dteEndDate
            // 
            this.SetBoundFieldName(this.dteEndDate, "enddate");
            this.SetBoundPropertyName(this.dteEndDate, "EditValue");
            this.dteEndDate.EditValue = "";
            this.dteEndDate.Location = new System.Drawing.Point(109, 306);
            this.dteEndDate.Name = "dteEndDate";
            this.dteEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEndDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dteEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteEndDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dteEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteEndDate.Properties.NullDate = "";
            this.dteEndDate.Size = new System.Drawing.Size(100, 20);
            this.dteEndDate.TabIndex = 13;
            this.dteEndDate.EditValueChanged += new System.EventHandler(this.dteEndDate_EditValueChanged);
            // 
            // cmbCalTypes
            // 
            this.SetBoundFieldName(this.cmbCalTypes, "calendarcode");
            this.SetBoundPropertyName(this.cmbCalTypes, "EditValue");
            this.cmbCalTypes.EditValue = "";
            this.cmbCalTypes.Location = new System.Drawing.Point(109, 254);
            this.cmbCalTypes.Name = "cmbCalTypes";
            this.cmbCalTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCalTypes.Properties.DisplayMember = "CalendarCode";
            this.cmbCalTypes.Properties.NullText = "";
            this.cmbCalTypes.Properties.ValueMember = "CalendarCode";
            this.cmbCalTypes.Properties.View = this.gridLookUpEdit1View;
            this.cmbCalTypes.Size = new System.Drawing.Size(100, 20);
            this.cmbCalTypes.TabIndex = 15;
            this.cmbCalTypes.EditValueChanged += new System.EventHandler(this.cmbCalTypes_EditValueChanged_1);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl5
            // 
            this.SetBoundPropertyName(this.labelControl5, "");
            this.labelControl5.Location = new System.Drawing.Point(341, 6);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 13);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "Selected List";
            // 
            // dxErrorProviderSection
            // 
            this.dxErrorProviderSection.ContainerControl = this;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.SetBoundPropertyName(this.panelControl1, "");
            this.panelControl1.Controls.Add(this.dteTestDate);
            this.panelControl1.Controls.Add(this.txtPrevMonth);
            this.panelControl1.Controls.Add(this.txtSectionID);
            this.panelControl1.Controls.Add(this.dteNewBeginDate);
            this.panelControl1.Controls.Add(this.txtNewConvBeginDate);
            this.panelControl1.Controls.Add(this.txtEDate);
            this.panelControl1.Controls.Add(this.txtBDate);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(617, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(149, 334);
            this.panelControl1.TabIndex = 25;
            // 
            // dteTestDate
            // 
            this.SetBoundFieldName(this.dteTestDate, "TestDate");
            this.SetBoundPropertyName(this.dteTestDate, "EditValue");
            this.dteTestDate.EditValue = null;
            this.dteTestDate.Enabled = false;
            this.dteTestDate.Location = new System.Drawing.Point(25, 162);
            this.dteTestDate.Name = "dteTestDate";
            this.dteTestDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTestDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTestDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dteTestDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteTestDate.Size = new System.Drawing.Size(100, 20);
            this.dteTestDate.TabIndex = 31;
            this.dteTestDate.TabStop = false;
            // 
            // txtPrevMonth
            // 
            this.SetBoundFieldName(this.txtPrevMonth, "PrevMonth");
            this.SetBoundPropertyName(this.txtPrevMonth, "EditValue");
            this.txtPrevMonth.Enabled = false;
            this.txtPrevMonth.Location = new System.Drawing.Point(25, 138);
            this.txtPrevMonth.Name = "txtPrevMonth";
            this.txtPrevMonth.Size = new System.Drawing.Size(100, 20);
            this.txtPrevMonth.TabIndex = 30;
            this.txtPrevMonth.TabStop = false;
            // 
            // txtSectionID
            // 
            this.SetBoundFieldName(this.txtSectionID, "sectionid");
            this.SetBoundPropertyName(this.txtSectionID, "EditValue");
            this.txtSectionID.Enabled = false;
            this.txtSectionID.Location = new System.Drawing.Point(25, 114);
            this.txtSectionID.Name = "txtSectionID";
            this.txtSectionID.Size = new System.Drawing.Size(100, 20);
            this.txtSectionID.TabIndex = 29;
            this.txtSectionID.TabStop = false;
            // 
            // dteNewBeginDate
            // 
            this.SetBoundFieldName(this.dteNewBeginDate, "NewBeginDate");
            this.SetBoundPropertyName(this.dteNewBeginDate, "EditValue");
            this.dteNewBeginDate.EditValue = null;
            this.dteNewBeginDate.Enabled = false;
            this.dteNewBeginDate.Location = new System.Drawing.Point(25, 263);
            this.dteNewBeginDate.Name = "dteNewBeginDate";
            this.dteNewBeginDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteNewBeginDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteNewBeginDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dteNewBeginDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteNewBeginDate.Size = new System.Drawing.Size(100, 20);
            this.dteNewBeginDate.TabIndex = 28;
            this.dteNewBeginDate.TabStop = false;
            // 
            // txtNewConvBeginDate
            // 
            this.SetBoundFieldName(this.txtNewConvBeginDate, "NewConvBeginDate");
            this.SetBoundPropertyName(this.txtNewConvBeginDate, "EditValue");
            this.txtNewConvBeginDate.Enabled = false;
            this.txtNewConvBeginDate.Location = new System.Drawing.Point(25, 239);
            this.txtNewConvBeginDate.Name = "txtNewConvBeginDate";
            this.txtNewConvBeginDate.Size = new System.Drawing.Size(100, 20);
            this.txtNewConvBeginDate.TabIndex = 27;
            this.txtNewConvBeginDate.TabStop = false;
            // 
            // txtEDate
            // 
            this.SetBoundFieldName(this.txtEDate, "EDate");
            this.SetBoundPropertyName(this.txtEDate, "EditValue");
            this.txtEDate.Enabled = false;
            this.txtEDate.Location = new System.Drawing.Point(25, 214);
            this.txtEDate.Name = "txtEDate";
            this.txtEDate.Size = new System.Drawing.Size(100, 20);
            this.txtEDate.TabIndex = 26;
            this.txtEDate.TabStop = false;
            // 
            // txtBDate
            // 
            this.SetBoundFieldName(this.txtBDate, "BDate");
            this.SetBoundPropertyName(this.txtBDate, "EditValue");
            this.txtBDate.Enabled = false;
            this.txtBDate.Location = new System.Drawing.Point(25, 187);
            this.txtBDate.Name = "txtBDate";
            this.txtBDate.Size = new System.Drawing.Size(100, 20);
            this.txtBDate.TabIndex = 25;
            this.txtBDate.TabStop = false;
            // 
            // ileSectionCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.cmbCalTypes);
            this.Controls.Add(this.dteEndDate);
            this.Controls.Add(this.dteBeginDate);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.grpbx5);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtProdMonth);
            this.Controls.Add(this.btnApplytoAvail);
            this.Controls.Add(this.btnAvailtoApply);
            this.Controls.Add(this.labAvailableShiftbosses);
            this.Controls.Add(this.lstApply);
            this.Controls.Add(this.lstAvailable);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ileSectionCalendar";
            this.Size = new System.Drawing.Size(766, 334);
            this.Load += new System.EventHandler(this.ileSectionCalendars_Load);
            this.Enter += new System.EventHandler(this.ileSectionCalendars_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.lstAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).EndInit();
            this.grpbx5.ResumeLayout(false);
            this.grpbx5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteBeginDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCalTypes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProviderSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dteTestDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTestDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrevMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSectionID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNewBeginDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteNewBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewConvBeginDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lstAvailable;
        private DevExpress.XtraEditors.ListBoxControl lstApply;
        private DevExpress.XtraEditors.LabelControl labAvailableShiftbosses;
        private DevExpress.XtraEditors.DateEdit dteEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.GroupBox grpbx5;
        private DevExpress.XtraEditors.LabelControl lblTotalShifts;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtProdMonth;
        private DevExpress.XtraEditors.SimpleButton btnApplytoAvail;
        private DevExpress.XtraEditors.SimpleButton btnAvailtoApply;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        public DevExpress.XtraEditors.GridLookUpEdit cmbCalTypes;
        public DevExpress.XtraEditors.DateEdit dteBeginDate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProviderSection;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit dteTestDate;
        private DevExpress.XtraEditors.TextEdit txtPrevMonth;
        private DevExpress.XtraEditors.TextEdit txtSectionID;
        private DevExpress.XtraEditors.DateEdit dteNewBeginDate;
        private DevExpress.XtraEditors.TextEdit txtNewConvBeginDate;
        private DevExpress.XtraEditors.TextEdit txtEDate;
        private DevExpress.XtraEditors.TextEdit txtBDate;
    }
}
