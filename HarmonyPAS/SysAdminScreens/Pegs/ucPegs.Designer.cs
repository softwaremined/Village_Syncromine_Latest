namespace Mineware.Systems.Production.SysAdminScreens.Pegs
{
    partial class ucPegs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPegs));
            this.gcPegs = new DevExpress.XtraGrid.GridControl();
            this.gvPegs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_WorkplaceID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_PegID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Value = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Letter1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Letter2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Letter3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pcButtons = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            ((System.ComponentModel.ISupportInitialize)(this.gcPegs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPegs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcButtons)).BeginInit();
            this.pcButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcPegs
            // 
            this.gcPegs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPegs.Location = new System.Drawing.Point(0, 40);
            this.gcPegs.MainView = this.gvPegs;
            this.gcPegs.Name = "gcPegs";
            this.gcPegs.Size = new System.Drawing.Size(662, 383);
            this.gcPegs.TabIndex = 6;
            this.gcPegs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPegs});
            this.gcPegs.Click += new System.EventHandler(this.gcPegs_Click);
            // 
            // gvPegs
            // 
            this.gvPegs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_WorkplaceID,
            this.col_Description,
            this.col_PegID,
            this.col_Value,
            this.col_Letter1,
            this.col_Letter2,
            this.col_Letter3});
            this.gvPegs.GridControl = this.gcPegs;
            this.gvPegs.Name = "gvPegs";
            this.gvPegs.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvPegs.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvPegs.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvPegs.OptionsEditForm.ShowOnDoubleClick = DevExpress.Utils.DefaultBoolean.False;
            this.gvPegs.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            this.gvPegs.OptionsView.ShowAutoFilterRow = true;
            this.gvPegs.OptionsView.ShowGroupPanel = false;
            this.gvPegs.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvPegs_RowClick);
            this.gvPegs.EditFormPrepared += new DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventHandler(this.gvPegs_EditFormPrepared);
            this.gvPegs.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvPegs_InitNewRow);
            this.gvPegs.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvPegs_InvalidRowException);
            this.gvPegs.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvPegs_ValidateRow);
            this.gvPegs.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvPegs_RowUpdated);
            this.gvPegs.Click += new System.EventHandler(this.gvPegs_Click);
            this.gvPegs.GotFocus += new System.EventHandler(this.gvPegs_GotFocus);
            // 
            // col_WorkplaceID
            // 
            this.col_WorkplaceID.AppearanceCell.Options.UseTextOptions = true;
            this.col_WorkplaceID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_WorkplaceID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_WorkplaceID.AppearanceHeader.Options.UseFont = true;
            this.col_WorkplaceID.AppearanceHeader.Options.UseTextOptions = true;
            this.col_WorkplaceID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_WorkplaceID.Caption = "WP ID";
            this.col_WorkplaceID.FieldName = "WorkplaceID";
            this.col_WorkplaceID.Name = "col_WorkplaceID";
            this.col_WorkplaceID.OptionsColumn.AllowFocus = false;
            this.col_WorkplaceID.OptionsColumn.FixedWidth = true;
            this.col_WorkplaceID.OptionsColumn.TabStop = false;
            this.col_WorkplaceID.Visible = true;
            this.col_WorkplaceID.VisibleIndex = 0;
            this.col_WorkplaceID.Width = 60;
            // 
            // col_Description
            // 
            this.col_Description.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_Description.AppearanceHeader.Options.UseFont = true;
            this.col_Description.AppearanceHeader.Options.UseTextOptions = true;
            this.col_Description.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Description.Caption = "Description";
            this.col_Description.FieldName = "Description";
            this.col_Description.Name = "col_Description";
            this.col_Description.OptionsColumn.AllowFocus = false;
            this.col_Description.OptionsColumn.FixedWidth = true;
            this.col_Description.OptionsColumn.TabStop = false;
            this.col_Description.Visible = true;
            this.col_Description.VisibleIndex = 1;
            this.col_Description.Width = 120;
            // 
            // col_PegID
            // 
            this.col_PegID.AppearanceCell.Options.UseTextOptions = true;
            this.col_PegID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_PegID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_PegID.AppearanceHeader.Options.UseFont = true;
            this.col_PegID.AppearanceHeader.Options.UseTextOptions = true;
            this.col_PegID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_PegID.Caption = "Peg ID";
            this.col_PegID.FieldName = "PegID";
            this.col_PegID.Name = "col_PegID";
            this.col_PegID.OptionsColumn.AllowFocus = false;
            this.col_PegID.OptionsColumn.FixedWidth = true;
            this.col_PegID.OptionsColumn.TabStop = false;
            this.col_PegID.Visible = true;
            this.col_PegID.VisibleIndex = 2;
            this.col_PegID.Width = 70;
            // 
            // col_Value
            // 
            this.col_Value.AppearanceCell.Options.UseTextOptions = true;
            this.col_Value.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Value.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_Value.AppearanceHeader.Options.UseFont = true;
            this.col_Value.AppearanceHeader.Options.UseTextOptions = true;
            this.col_Value.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Value.Caption = "Value";
            this.col_Value.FieldName = "theValue";
            this.col_Value.Name = "col_Value";
            this.col_Value.OptionsColumn.AllowFocus = false;
            this.col_Value.OptionsColumn.FixedWidth = true;
            this.col_Value.OptionsColumn.TabStop = false;
            this.col_Value.Visible = true;
            this.col_Value.VisibleIndex = 3;
            this.col_Value.Width = 70;
            // 
            // col_Letter1
            // 
            this.col_Letter1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_Letter1.AppearanceHeader.Options.UseFont = true;
            this.col_Letter1.AppearanceHeader.Options.UseTextOptions = true;
            this.col_Letter1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Letter1.Caption = "Letter 1";
            this.col_Letter1.FieldName = "Letter1";
            this.col_Letter1.Name = "col_Letter1";
            this.col_Letter1.OptionsColumn.AllowFocus = false;
            this.col_Letter1.OptionsColumn.FixedWidth = true;
            this.col_Letter1.OptionsColumn.TabStop = false;
            this.col_Letter1.Visible = true;
            this.col_Letter1.VisibleIndex = 4;
            this.col_Letter1.Width = 105;
            // 
            // col_Letter2
            // 
            this.col_Letter2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_Letter2.AppearanceHeader.Options.UseFont = true;
            this.col_Letter2.AppearanceHeader.Options.UseTextOptions = true;
            this.col_Letter2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Letter2.Caption = "Letter 2";
            this.col_Letter2.FieldName = "Letter2";
            this.col_Letter2.Name = "col_Letter2";
            this.col_Letter2.OptionsColumn.AllowFocus = false;
            this.col_Letter2.OptionsColumn.FixedWidth = true;
            this.col_Letter2.OptionsColumn.TabStop = false;
            this.col_Letter2.Visible = true;
            this.col_Letter2.VisibleIndex = 5;
            this.col_Letter2.Width = 105;
            // 
            // col_Letter3
            // 
            this.col_Letter3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_Letter3.AppearanceHeader.Options.UseFont = true;
            this.col_Letter3.AppearanceHeader.Options.UseTextOptions = true;
            this.col_Letter3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Letter3.Caption = "Letter 3";
            this.col_Letter3.FieldName = "Letter3";
            this.col_Letter3.Name = "col_Letter3";
            this.col_Letter3.OptionsColumn.AllowFocus = false;
            this.col_Letter3.OptionsColumn.FixedWidth = true;
            this.col_Letter3.OptionsColumn.TabStop = false;
            this.col_Letter3.Visible = true;
            this.col_Letter3.VisibleIndex = 6;
            this.col_Letter3.Width = 114;
            // 
            // pcButtons
            // 
            this.pcButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pcButtons.Controls.Add(this.btnAdd);
            this.pcButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcButtons.Location = new System.Drawing.Point(0, 0);
            this.pcButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pcButtons.Name = "pcButtons";
            this.pcButtons.Size = new System.Drawing.Size(662, 40);
            this.pcButtons.TabIndex = 17;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 40);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Add;
            this.btnAdd.theCustomeText = null;
            this.btnAdd.theImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.theImage")));
            this.btnAdd.theImageHot = ((System.Drawing.Image)(resources.GetObject("btnAdd.theImageHot")));
            this.btnAdd.theSuperToolTip = null;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ucPegs
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcPegs);
            this.Controls.Add(this.pcButtons);
            this.Name = "ucPegs";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(662, 423);
            this.Load += new System.EventHandler(this.ucPegs_Load);
            this.Controls.SetChildIndex(this.pcButtons, 0);
            this.Controls.SetChildIndex(this.gcPegs, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gcPegs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPegs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcButtons)).EndInit();
            this.pcButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gcPegs;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPegs;
        private DevExpress.XtraGrid.Columns.GridColumn col_WorkplaceID;
        private DevExpress.XtraGrid.Columns.GridColumn col_PegID;
        private DevExpress.XtraGrid.Columns.GridColumn col_Value;
        private DevExpress.XtraGrid.Columns.GridColumn col_Letter1;
        private DevExpress.XtraGrid.Columns.GridColumn col_Letter2;
        private DevExpress.XtraGrid.Columns.GridColumn col_Letter3;
        private DevExpress.XtraEditors.PanelControl pcButtons;
        public DevExpress.XtraGrid.Columns.GridColumn col_Description;
        public Global.sysButtons.ucSysBtn btnAdd;
    }
}
