namespace Mineware.Systems.Production.SysAdminScreens.GangNo
{
    partial class ucGangNoScreen
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.lblCalendarDate = new DevExpress.XtraEditors.LabelControl();
            this.dteCalendarDate = new DevExpress.XtraEditors.DateEdit();
            this.gcGangNo = new DevExpress.XtraGrid.GridControl();
            this.gvGangNo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_CalendarDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_GangNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CrewNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CrewName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ProcessCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CostArea = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteCalendarDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCalendarDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGangNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGangNo)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.lblCalendarDate);
            this.splitContainerControl1.Panel1.Controls.Add(this.dteCalendarDate);
            this.splitContainerControl1.Panel1.MinSize = 15;
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcGangNo);
            this.splitContainerControl1.Panel2.MinSize = 25;
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(892, 576);
            this.splitContainerControl1.SplitterPosition = 40;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // lblCalendarDate
            // 
            this.lblCalendarDate.Location = new System.Drawing.Point(13, 13);
            this.lblCalendarDate.Name = "lblCalendarDate";
            this.lblCalendarDate.Size = new System.Drawing.Size(69, 13);
            this.lblCalendarDate.TabIndex = 1;
            this.lblCalendarDate.Text = "Calendar Date";
            this.lblCalendarDate.Visible = false;
            // 
            // dteCalendarDate
            // 
            this.dteCalendarDate.EditValue = null;
            this.dteCalendarDate.Location = new System.Drawing.Point(104, 10);
            this.dteCalendarDate.Name = "dteCalendarDate";
            this.dteCalendarDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCalendarDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCalendarDate.Size = new System.Drawing.Size(163, 20);
            this.dteCalendarDate.TabIndex = 0;
            this.dteCalendarDate.Visible = false;
            this.dteCalendarDate.EditValueChanged += new System.EventHandler(this.dteCalendarDate_EditValueChanged);
            // 
            // gcGangNo
            // 
            this.gcGangNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcGangNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcGangNo.Location = new System.Drawing.Point(0, 0);
            this.gcGangNo.MainView = this.gvGangNo;
            this.gcGangNo.Name = "gcGangNo";
            this.gcGangNo.Size = new System.Drawing.Size(892, 531);
            this.gcGangNo.TabIndex = 1;
            this.gcGangNo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGangNo});
            this.gcGangNo.Click += new System.EventHandler(this.gcGangNo_Click);
            // 
            // gvGangNo
            // 
            this.gvGangNo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_CalendarDate,
            this.col_GangNo,
            this.col_CrewNo,
            this.col_CrewName,
            this.col_ProcessCode,
            this.col_CostArea});
            this.gvGangNo.GridControl = this.gcGangNo;
            this.gvGangNo.Name = "gvGangNo";
            this.gvGangNo.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvGangNo.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvGangNo.OptionsBehavior.ReadOnly = true;
            this.gvGangNo.OptionsCustomization.AllowColumnMoving = false;
            this.gvGangNo.OptionsView.ShowGroupPanel = false;
            // 
            // col_CalendarDate
            // 
            this.col_CalendarDate.Caption = "Calendar Date";
            this.col_CalendarDate.FieldName = "Calendardate";
            this.col_CalendarDate.Name = "col_CalendarDate";
            this.col_CalendarDate.Visible = true;
            this.col_CalendarDate.VisibleIndex = 0;
            // 
            // col_GangNo
            // 
            this.col_GangNo.Caption = "Gang No";
            this.col_GangNo.FieldName = "GangNo";
            this.col_GangNo.Name = "col_GangNo";
            this.col_GangNo.Visible = true;
            this.col_GangNo.VisibleIndex = 1;
            // 
            // col_CrewNo
            // 
            this.col_CrewNo.Caption = "CrewNo";
            this.col_CrewNo.FieldName = "CrewNo";
            this.col_CrewNo.Name = "col_CrewNo";
            this.col_CrewNo.Visible = true;
            this.col_CrewNo.VisibleIndex = 2;
            // 
            // col_CrewName
            // 
            this.col_CrewName.Caption = "Crew Name";
            this.col_CrewName.FieldName = "CrewName";
            this.col_CrewName.Name = "col_CrewName";
            this.col_CrewName.Visible = true;
            this.col_CrewName.VisibleIndex = 3;
            // 
            // col_ProcessCode
            // 
            this.col_ProcessCode.Caption = "Process Code";
            this.col_ProcessCode.FieldName = "ProcessCode";
            this.col_ProcessCode.Name = "col_ProcessCode";
            this.col_ProcessCode.Visible = true;
            this.col_ProcessCode.VisibleIndex = 4;
            // 
            // col_CostArea
            // 
            this.col_CostArea.Caption = "Cost Area";
            this.col_CostArea.FieldName = "CostArea";
            this.col_CostArea.Name = "col_CostArea";
            this.col_CostArea.Visible = true;
            this.col_CostArea.VisibleIndex = 5;
            // 
            // ucGangNoScreen
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ucGangNoScreen";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(892, 576);
            this.Load += new System.EventHandler(this.ucGangNoScreen_Load);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dteCalendarDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCalendarDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGangNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGangNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcGangNo;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGangNo;
        private DevExpress.XtraGrid.Columns.GridColumn col_GangNo;
        private DevExpress.XtraGrid.Columns.GridColumn col_CrewNo;
        private DevExpress.XtraGrid.Columns.GridColumn col_CrewName;
        private DevExpress.XtraGrid.Columns.GridColumn col_ProcessCode;
        private DevExpress.XtraGrid.Columns.GridColumn col_CostArea;
        private DevExpress.XtraEditors.LabelControl lblCalendarDate;
        private DevExpress.XtraEditors.DateEdit dteCalendarDate;
        private DevExpress.XtraGrid.Columns.GridColumn col_CalendarDate;
    }
}
