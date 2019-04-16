namespace Mineware.Systems.Production.SysAdminScreens.SamplingCapture
{
    partial class ucSamplingCapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSamplingCapture));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcSamplingScreen = new DevExpress.XtraGrid.GridControl();
            this.gvSamplingScreen = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_WPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_StopeWidth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Channel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Footwall = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_HangingWall = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Grade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_RIF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSamplingScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSamplingScreen)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.panelControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gcSamplingScreen);
            this.splitContainer1.Size = new System.Drawing.Size(868, 350);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnBack);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(868, 49);
            this.panelControl1.TabIndex = 4;
            // 
            // gcSamplingScreen
            // 
            this.gcSamplingScreen.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcSamplingScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSamplingScreen.Location = new System.Drawing.Point(0, 0);
            this.gcSamplingScreen.MainView = this.gvSamplingScreen;
            this.gcSamplingScreen.Name = "gcSamplingScreen";
            this.gcSamplingScreen.Size = new System.Drawing.Size(868, 306);
            this.gcSamplingScreen.TabIndex = 3;
            this.gcSamplingScreen.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSamplingScreen});
            // 
            // gvSamplingScreen
            // 
            this.gvSamplingScreen.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_Status,
            this.col_Date,
            this.col_WPID,
            this.col_Description,
            this.col_StopeWidth,
            this.col_Channel,
            this.col_Footwall,
            this.col_HangingWall,
            this.col_Grade,
            this.col_RIF});
            this.gvSamplingScreen.GridControl = this.gcSamplingScreen;
            this.gvSamplingScreen.Name = "gvSamplingScreen";
            this.gvSamplingScreen.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSamplingScreen.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSamplingScreen.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gvSamplingScreen.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvSamplingScreen.OptionsBehavior.ReadOnly = true;
            this.gvSamplingScreen.OptionsCustomization.AllowColumnMoving = false;
            this.gvSamplingScreen.OptionsView.ShowAutoFilterRow = true;
            this.gvSamplingScreen.OptionsView.ShowGroupPanel = false;
            this.gvSamplingScreen.EditFormPrepared += new DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventHandler(this.gvSamplingScreen_EditFormPrepared);
            this.gvSamplingScreen.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvSamplingScreen_InitNewRow);
            this.gvSamplingScreen.ColumnFilterChanged += new System.EventHandler(this.gvSamplingScreen_ColumnFilterChanged);
            this.gvSamplingScreen.DoubleClick += new System.EventHandler(this.gvSamplingScreen_DoubleClick);
            // 
            // col_Status
            // 
            this.col_Status.Caption = "Status";
            this.col_Status.FieldName = "Status";
            this.col_Status.Name = "col_Status";
            // 
            // col_Date
            // 
            this.col_Date.Caption = "Date";
            this.col_Date.FieldName = "CalendarDate";
            this.col_Date.Name = "col_Date";
            this.col_Date.Visible = true;
            this.col_Date.VisibleIndex = 0;
            // 
            // col_WPID
            // 
            this.col_WPID.Caption = "WP ID.";
            this.col_WPID.FieldName = "WorkplaceID";
            this.col_WPID.Name = "col_WPID";
            this.col_WPID.Visible = true;
            this.col_WPID.VisibleIndex = 1;
            // 
            // col_Description
            // 
            this.col_Description.Caption = "Description";
            this.col_Description.FieldName = "Description";
            this.col_Description.Name = "col_Description";
            this.col_Description.Visible = true;
            this.col_Description.VisibleIndex = 2;
            // 
            // col_StopeWidth
            // 
            this.col_StopeWidth.Caption = "Stope Width";
            this.col_StopeWidth.FieldName = "SWidth";
            this.col_StopeWidth.Name = "col_StopeWidth";
            this.col_StopeWidth.Visible = true;
            this.col_StopeWidth.VisibleIndex = 3;
            // 
            // col_Channel
            // 
            this.col_Channel.Caption = "Channel";
            this.col_Channel.FieldName = "Channelwidth";
            this.col_Channel.Name = "col_Channel";
            this.col_Channel.Visible = true;
            this.col_Channel.VisibleIndex = 4;
            // 
            // col_Footwall
            // 
            this.col_Footwall.Caption = "Footwall";
            this.col_Footwall.FieldName = "Footwall";
            this.col_Footwall.Name = "col_Footwall";
            this.col_Footwall.Visible = true;
            this.col_Footwall.VisibleIndex = 6;
            // 
            // col_HangingWall
            // 
            this.col_HangingWall.Caption = "Hanging Wall";
            this.col_HangingWall.FieldName = "HangingWall";
            this.col_HangingWall.Name = "col_HangingWall";
            this.col_HangingWall.Visible = true;
            this.col_HangingWall.VisibleIndex = 5;
            // 
            // col_Grade
            // 
            this.col_Grade.Caption = "Grade";
            this.col_Grade.FieldName = "Cmgt";
            this.col_Grade.Name = "col_Grade";
            this.col_Grade.Visible = true;
            this.col_Grade.VisibleIndex = 7;
            // 
            // col_RIF
            // 
            this.col_RIF.Caption = "RIF";
            this.col_RIF.FieldName = "RIF";
            this.col_RIF.Name = "col_RIF";
            this.col_RIF.Visible = true;
            this.col_RIF.VisibleIndex = 8;
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 34);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ucSamplingCapture
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucSamplingCapture";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(868, 350);
            this.Load += new System.EventHandler(this.ucSamplingCapture_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSamplingScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSamplingScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gcSamplingScreen;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSamplingScreen;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn col_Date;
        private DevExpress.XtraGrid.Columns.GridColumn col_WPID;
        private DevExpress.XtraGrid.Columns.GridColumn col_Description;
        private DevExpress.XtraGrid.Columns.GridColumn col_StopeWidth;
        private DevExpress.XtraGrid.Columns.GridColumn col_Channel;
        private DevExpress.XtraGrid.Columns.GridColumn col_Footwall;
        private DevExpress.XtraGrid.Columns.GridColumn col_HangingWall;
        private DevExpress.XtraGrid.Columns.GridColumn col_Grade;
        private DevExpress.XtraGrid.Columns.GridColumn col_RIF;
        private DevExpress.XtraGrid.Columns.GridColumn col_Status;
        private DevExpress.XtraEditors.SimpleButton btnBack;
    }
}
