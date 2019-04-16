namespace Mineware.Systems.Production.SysAdminScreens.QAQC
{
    partial class ileCRM
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
            this.dxCRM = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.lblAssayDate = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketNo = new DevExpress.XtraEditors.LabelControl();
            this.lblAssayValue = new DevExpress.XtraEditors.LabelControl();
            this.lblOutcome = new DevExpress.XtraEditors.LabelControl();
            this.txtTicketNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtAssayValue = new DevExpress.XtraEditors.TextEdit();
            this.txtOutcome = new DevExpress.XtraEditors.TextEdit();
            this.dteDate = new DevExpress.XtraEditors.DateEdit();
            this.txtReAssayOutcome = new DevExpress.XtraEditors.TextEdit();
            this.txtReAssayValue = new DevExpress.XtraEditors.TextEdit();
            this.lblReAssayOutcome = new DevExpress.XtraEditors.LabelControl();
            this.lblReAssayValue = new DevExpress.XtraEditors.LabelControl();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dxCRM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTicketNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssayValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutcome.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReAssayOutcome.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReAssayValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dxCRM
            // 
            this.dxCRM.ContainerControl = this;
            // 
            // lblAssayDate
            // 
            this.SetBoundPropertyName(this.lblAssayDate, "");
            this.lblAssayDate.Location = new System.Drawing.Point(129, 17);
            this.lblAssayDate.Name = "lblAssayDate";
            this.lblAssayDate.Size = new System.Drawing.Size(23, 13);
            this.lblAssayDate.TabIndex = 24;
            this.lblAssayDate.Text = "Date";
            // 
            // lblTicketNo
            // 
            this.SetBoundPropertyName(this.lblTicketNo, "");
            this.lblTicketNo.Location = new System.Drawing.Point(84, 43);
            this.lblTicketNo.Name = "lblTicketNo";
            this.lblTicketNo.Size = new System.Drawing.Size(68, 13);
            this.lblTicketNo.TabIndex = 25;
            this.lblTicketNo.Text = "Ticket Number";
            // 
            // lblAssayValue
            // 
            this.SetBoundPropertyName(this.lblAssayValue, "");
            this.lblAssayValue.Location = new System.Drawing.Point(94, 69);
            this.lblAssayValue.Name = "lblAssayValue";
            this.lblAssayValue.Size = new System.Drawing.Size(58, 13);
            this.lblAssayValue.TabIndex = 27;
            this.lblAssayValue.Text = "Assay Value";
            // 
            // lblOutcome
            // 
            this.SetBoundPropertyName(this.lblOutcome, "");
            this.lblOutcome.Location = new System.Drawing.Point(108, 95);
            this.lblOutcome.Name = "lblOutcome";
            this.lblOutcome.Size = new System.Drawing.Size(43, 13);
            this.lblOutcome.TabIndex = 28;
            this.lblOutcome.Text = "Outcome";
            // 
            // txtTicketNumber
            // 
            this.SetBoundPropertyName(this.txtTicketNumber, "");
            this.txtTicketNumber.Location = new System.Drawing.Point(158, 40);
            this.txtTicketNumber.Name = "txtTicketNumber";
            this.txtTicketNumber.Size = new System.Drawing.Size(166, 20);
            this.txtTicketNumber.TabIndex = 32;
            // 
            // txtAssayValue
            // 
            this.SetBoundPropertyName(this.txtAssayValue, "");
            this.txtAssayValue.Location = new System.Drawing.Point(158, 66);
            this.txtAssayValue.Name = "txtAssayValue";
            this.txtAssayValue.Size = new System.Drawing.Size(166, 20);
            this.txtAssayValue.TabIndex = 34;
            this.txtAssayValue.EditValueChanged += new System.EventHandler(this.txtAssayValue_EditValueChanged);
            // 
            // txtOutcome
            // 
            this.SetBoundPropertyName(this.txtOutcome, "");
            this.txtOutcome.Enabled = false;
            this.txtOutcome.Location = new System.Drawing.Point(158, 92);
            this.txtOutcome.Name = "txtOutcome";
            this.txtOutcome.Size = new System.Drawing.Size(166, 20);
            this.txtOutcome.TabIndex = 35;
            // 
            // dteDate
            // 
            this.SetBoundPropertyName(this.dteDate, "");
            this.dteDate.EditValue = null;
            this.dteDate.Location = new System.Drawing.Point(158, 14);
            this.dteDate.Name = "dteDate";
            this.dteDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDate.Size = new System.Drawing.Size(166, 20);
            this.dteDate.TabIndex = 36;
            // 
            // txtReAssayOutcome
            // 
            this.SetBoundPropertyName(this.txtReAssayOutcome, "");
            this.txtReAssayOutcome.Enabled = false;
            this.txtReAssayOutcome.Location = new System.Drawing.Point(158, 144);
            this.txtReAssayOutcome.Name = "txtReAssayOutcome";
            this.txtReAssayOutcome.Size = new System.Drawing.Size(166, 20);
            this.txtReAssayOutcome.TabIndex = 40;
            this.txtReAssayOutcome.Visible = false;
            // 
            // txtReAssayValue
            // 
            this.SetBoundPropertyName(this.txtReAssayValue, "");
            this.txtReAssayValue.Location = new System.Drawing.Point(158, 118);
            this.txtReAssayValue.Name = "txtReAssayValue";
            this.txtReAssayValue.Size = new System.Drawing.Size(166, 20);
            this.txtReAssayValue.TabIndex = 39;
            this.txtReAssayValue.Visible = false;
            this.txtReAssayValue.EditValueChanged += new System.EventHandler(this.txtReAssayValue_EditValueChanged);
            // 
            // lblReAssayOutcome
            // 
            this.SetBoundPropertyName(this.lblReAssayOutcome, "");
            this.lblReAssayOutcome.Location = new System.Drawing.Point(61, 147);
            this.lblReAssayOutcome.Name = "lblReAssayOutcome";
            this.lblReAssayOutcome.Size = new System.Drawing.Size(91, 13);
            this.lblReAssayOutcome.TabIndex = 38;
            this.lblReAssayOutcome.Text = "Re Assay Outcome";
            this.lblReAssayOutcome.Visible = false;
            // 
            // lblReAssayValue
            // 
            this.SetBoundPropertyName(this.lblReAssayValue, "");
            this.lblReAssayValue.Location = new System.Drawing.Point(78, 121);
            this.lblReAssayValue.Name = "lblReAssayValue";
            this.lblReAssayValue.Size = new System.Drawing.Size(74, 13);
            this.lblReAssayValue.TabIndex = 37;
            this.lblReAssayValue.Text = "Re Assay Value";
            this.lblReAssayValue.Visible = false;
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.Location = new System.Drawing.Point(158, 170);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 48;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // ileCRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtReAssayOutcome);
            this.Controls.Add(this.txtReAssayValue);
            this.Controls.Add(this.lblReAssayOutcome);
            this.Controls.Add(this.lblReAssayValue);
            this.Controls.Add(this.dteDate);
            this.Controls.Add(this.txtOutcome);
            this.Controls.Add(this.txtAssayValue);
            this.Controls.Add(this.txtTicketNumber);
            this.Controls.Add(this.lblOutcome);
            this.Controls.Add(this.lblAssayValue);
            this.Controls.Add(this.lblTicketNo);
            this.Controls.Add(this.lblAssayDate);
            this.Name = "ileCRM";
            this.Size = new System.Drawing.Size(337, 205);
            this.Load += new System.EventHandler(this.ileCRM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxCRM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTicketNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssayValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutcome.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReAssayOutcome.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReAssayValue.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxCRM;
        private DevExpress.XtraEditors.LabelControl lblOutcome;
        private DevExpress.XtraEditors.LabelControl lblAssayValue;
        private DevExpress.XtraEditors.LabelControl lblTicketNo;
        private DevExpress.XtraEditors.LabelControl lblAssayDate;
        private DevExpress.XtraEditors.LabelControl lblReAssayOutcome;
        private DevExpress.XtraEditors.LabelControl lblReAssayValue;
        public DevExpress.XtraEditors.TextEdit txtOutcome;
        public DevExpress.XtraEditors.TextEdit txtAssayValue;
        public DevExpress.XtraEditors.TextEdit txtTicketNumber;
        public DevExpress.XtraEditors.DateEdit dteDate;
        public DevExpress.XtraEditors.TextEdit txtReAssayOutcome;
        public DevExpress.XtraEditors.TextEdit txtReAssayValue;
        public DevExpress.XtraEditors.SimpleButton btnUpdate;
    }
}
