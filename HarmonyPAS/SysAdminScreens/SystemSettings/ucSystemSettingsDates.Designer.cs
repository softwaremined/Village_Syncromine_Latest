namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    partial class ucSystemSettingsDates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSystemSettingsDates));
            this.pnlDates = new DevExpress.XtraEditors.PanelControl();
            this.btnSaveDates = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            this.pnlDatesData = new DevExpress.XtraEditors.PanelControl();
            this.dteMillMonth = new DevExpress.XtraEditors.SpinEdit();
            this.dteProdMonth = new DevExpress.XtraEditors.SpinEdit();
            this.dteFinancialEnd = new DevExpress.XtraEditors.SpinEdit();
            this.lblFinancialEnd = new DevExpress.XtraEditors.LabelControl();
            this.dteFinancialStart = new DevExpress.XtraEditors.SpinEdit();
            this.lblFinancialStart = new DevExpress.XtraEditors.LabelControl();
            this.lblMillMonth = new DevExpress.XtraEditors.LabelControl();
            this.lblProdMonth = new DevExpress.XtraEditors.LabelControl();
            this.lblRunDate = new DevExpress.XtraEditors.LabelControl();
            this.dteRunDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDates)).BeginInit();
            this.pnlDates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDatesData)).BeginInit();
            this.pnlDatesData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteMillMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteProdMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinancialEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinancialStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteRunDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteRunDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDates
            // 
            this.pnlDates.Controls.Add(this.btnSaveDates);
            this.pnlDates.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDates.Location = new System.Drawing.Point(0, 0);
            this.pnlDates.Name = "pnlDates";
            this.pnlDates.Size = new System.Drawing.Size(887, 67);
            this.pnlDates.TabIndex = 5;
            // 
            // btnSaveDates
            // 
            this.btnSaveDates.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSaveDates.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveDates.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSaveDates.Location = new System.Drawing.Point(2, 2);
            this.btnSaveDates.Margin = new System.Windows.Forms.Padding(6, 12, 6, 12);
            this.btnSaveDates.Name = "btnSaveDates";
            this.btnSaveDates.Size = new System.Drawing.Size(141, 63);
            this.btnSaveDates.TabIndex = 2;
            this.btnSaveDates.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Save;
            this.btnSaveDates.theCustomeText = null;
            this.btnSaveDates.theImage = ((System.Drawing.Image)(resources.GetObject("btnSaveDates.theImage")));
            this.btnSaveDates.theImageHot = ((System.Drawing.Image)(resources.GetObject("btnSaveDates.theImageHot")));
            this.btnSaveDates.theSuperToolTip = null;
            this.btnSaveDates.Click += new System.EventHandler(this.btnSaveDates_Click);
            // 
            // pnlDatesData
            // 
            this.pnlDatesData.Controls.Add(this.dteMillMonth);
            this.pnlDatesData.Controls.Add(this.dteProdMonth);
            this.pnlDatesData.Controls.Add(this.dteFinancialEnd);
            this.pnlDatesData.Controls.Add(this.lblFinancialEnd);
            this.pnlDatesData.Controls.Add(this.dteFinancialStart);
            this.pnlDatesData.Controls.Add(this.lblFinancialStart);
            this.pnlDatesData.Controls.Add(this.lblMillMonth);
            this.pnlDatesData.Controls.Add(this.lblProdMonth);
            this.pnlDatesData.Controls.Add(this.lblRunDate);
            this.pnlDatesData.Controls.Add(this.dteRunDate);
            this.pnlDatesData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDatesData.Location = new System.Drawing.Point(0, 67);
            this.pnlDatesData.Name = "pnlDatesData";
            this.pnlDatesData.Size = new System.Drawing.Size(887, 684);
            this.pnlDatesData.TabIndex = 6;
            this.pnlDatesData.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDatesData_Paint);
            // 
            // dteMillMonth
            // 
            this.dteMillMonth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dteMillMonth.Location = new System.Drawing.Point(160, 85);
            this.dteMillMonth.Name = "dteMillMonth";
            this.dteMillMonth.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteMillMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteMillMonth.Properties.DisplayFormat.FormatString = "0";
            this.dteMillMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.dteMillMonth.Properties.EditFormat.FormatString = "0";
            this.dteMillMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.dteMillMonth.Properties.IsFloatValue = false;
            this.dteMillMonth.Properties.Mask.PlaceHolder = ' ';
            this.dteMillMonth.Properties.Mask.ShowPlaceHolders = false;
            this.dteMillMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteMillMonth.Size = new System.Drawing.Size(104, 20);
            this.dteMillMonth.TabIndex = 37;
            this.dteMillMonth.EditValueChanged += new System.EventHandler(this.dteMillMonth_EditValueChanged);
            // 
            // dteProdMonth
            // 
            this.dteProdMonth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dteProdMonth.Location = new System.Drawing.Point(160, 52);
            this.dteProdMonth.Name = "dteProdMonth";
            this.dteProdMonth.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dteProdMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteProdMonth.Properties.DisplayFormat.FormatString = "0";
            this.dteProdMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.dteProdMonth.Properties.EditFormat.FormatString = "0";
            this.dteProdMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.dteProdMonth.Properties.IsFloatValue = false;
            this.dteProdMonth.Properties.Mask.PlaceHolder = ' ';
            this.dteProdMonth.Properties.Mask.ShowPlaceHolders = false;
            this.dteProdMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteProdMonth.Size = new System.Drawing.Size(104, 20);
            this.dteProdMonth.TabIndex = 36;
            this.dteProdMonth.EditValueChanged += new System.EventHandler(this.dteProdMonth_EditValueChanged);
            // 
            // dteFinancialEnd
            // 
            this.dteFinancialEnd.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dteFinancialEnd.Location = new System.Drawing.Point(160, 153);
            this.dteFinancialEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dteFinancialEnd.Name = "dteFinancialEnd";
            this.dteFinancialEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFinancialEnd.Properties.Mask.EditMask = "d";
            this.dteFinancialEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteFinancialEnd.Properties.MaxValue = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.dteFinancialEnd.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dteFinancialEnd.Size = new System.Drawing.Size(104, 20);
            this.dteFinancialEnd.TabIndex = 35;
            // 
            // lblFinancialEnd
            // 
            this.lblFinancialEnd.Location = new System.Drawing.Point(32, 156);
            this.lblFinancialEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblFinancialEnd.Name = "lblFinancialEnd";
            this.lblFinancialEnd.Size = new System.Drawing.Size(95, 13);
            this.lblFinancialEnd.TabIndex = 34;
            this.lblFinancialEnd.Text = "Financial End Month";
            // 
            // dteFinancialStart
            // 
            this.dteFinancialStart.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dteFinancialStart.Location = new System.Drawing.Point(160, 120);
            this.dteFinancialStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dteFinancialStart.Name = "dteFinancialStart";
            this.dteFinancialStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFinancialStart.Properties.Mask.EditMask = "d";
            this.dteFinancialStart.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteFinancialStart.Properties.MaxValue = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.dteFinancialStart.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dteFinancialStart.Size = new System.Drawing.Size(104, 20);
            this.dteFinancialStart.TabIndex = 33;
            // 
            // lblFinancialStart
            // 
            this.lblFinancialStart.Location = new System.Drawing.Point(32, 127);
            this.lblFinancialStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblFinancialStart.Name = "lblFinancialStart";
            this.lblFinancialStart.Size = new System.Drawing.Size(101, 13);
            this.lblFinancialStart.TabIndex = 32;
            this.lblFinancialStart.Text = "Financial Start Month";
            // 
            // lblMillMonth
            // 
            this.lblMillMonth.Location = new System.Drawing.Point(32, 91);
            this.lblMillMonth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblMillMonth.Name = "lblMillMonth";
            this.lblMillMonth.Size = new System.Drawing.Size(47, 13);
            this.lblMillMonth.TabIndex = 29;
            this.lblMillMonth.Text = "Mill Month";
            // 
            // lblProdMonth
            // 
            this.lblProdMonth.Location = new System.Drawing.Point(32, 56);
            this.lblProdMonth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblProdMonth.Name = "lblProdMonth";
            this.lblProdMonth.Size = new System.Drawing.Size(84, 13);
            this.lblProdMonth.TabIndex = 27;
            this.lblProdMonth.Text = "Production Month";
            // 
            // lblRunDate
            // 
            this.lblRunDate.Location = new System.Drawing.Point(32, 24);
            this.lblRunDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblRunDate.Name = "lblRunDate";
            this.lblRunDate.Size = new System.Drawing.Size(45, 13);
            this.lblRunDate.TabIndex = 25;
            this.lblRunDate.Text = "Run Date";
            // 
            // dteRunDate
            // 
            this.dteRunDate.EditValue = null;
            this.dteRunDate.Location = new System.Drawing.Point(160, 21);
            this.dteRunDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dteRunDate.Name = "dteRunDate";
            this.dteRunDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteRunDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteRunDate.Properties.CalendarTimeProperties.Mask.EditMask = "dd-MM-yyyy";
            this.dteRunDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dteRunDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dteRunDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dteRunDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dteRunDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteRunDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteRunDate.Size = new System.Drawing.Size(129, 20);
            this.dteRunDate.TabIndex = 24;
            // 
            // ucSystemSettingsDates
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDatesData);
            this.Controls.Add(this.pnlDates);
            this.Name = "ucSystemSettingsDates";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(887, 751);
            this.Load += new System.EventHandler(this.ucSystemSettingsDates_Load);
            this.Controls.SetChildIndex(this.pnlDates, 0);
            this.Controls.SetChildIndex(this.pnlDatesData, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDates)).EndInit();
            this.pnlDates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDatesData)).EndInit();
            this.pnlDatesData.ResumeLayout(false);
            this.pnlDatesData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteMillMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteProdMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinancialEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinancialStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteRunDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteRunDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlDates;
        private Global.sysButtons.ucSysBtn btnSaveDates;
        private DevExpress.XtraEditors.PanelControl pnlDatesData;
        private DevExpress.XtraEditors.SpinEdit dteMillMonth;
        private DevExpress.XtraEditors.SpinEdit dteProdMonth;
        private DevExpress.XtraEditors.SpinEdit dteFinancialEnd;
        private DevExpress.XtraEditors.LabelControl lblFinancialEnd;
        private DevExpress.XtraEditors.SpinEdit dteFinancialStart;
        private DevExpress.XtraEditors.LabelControl lblFinancialStart;
        private DevExpress.XtraEditors.LabelControl lblMillMonth;
        private DevExpress.XtraEditors.LabelControl lblProdMonth;
        private DevExpress.XtraEditors.LabelControl lblRunDate;
        private DevExpress.XtraEditors.DateEdit dteRunDate;
    }
}
