namespace Mineware.Systems.Production.Controls
{
    partial class ReconBookingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReconBookingForm));
            this.reSQM = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtProdMonth = new DevExpress.XtraEditors.TextEdit();
            this.txtSection = new DevExpress.XtraEditors.TextEdit();
            this.txtCalendarDate = new DevExpress.XtraEditors.TextEdit();
            this.lblCalendarDate = new DevExpress.XtraEditors.LabelControl();
            this.lblWorkplace = new DevExpress.XtraEditors.LabelControl();
            this.lblProdMonth = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcWorkplace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcAct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProgFaceLength = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProgAdvance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReconAdvance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcProgCubics = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReconCubics = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcApprove = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reApprove = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcMOFC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcComments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcMonthPlan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReconFaceLength = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reMOFC = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            this.btnSave = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            ((System.ComponentModel.ISupportInitialize)(this.reSQM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCalendarDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reApprove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reMOFC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // reSQM
            // 
            this.reSQM.AutoHeight = false;
            this.reSQM.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.reSQM.DisplayFormat.FormatString = "0.0";
            this.reSQM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.reSQM.EditFormat.FormatString = "0.0";
            this.reSQM.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.reSQM.Name = "reSQM";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtProdMonth);
            this.panelControl1.Controls.Add(this.txtSection);
            this.panelControl1.Controls.Add(this.txtCalendarDate);
            this.panelControl1.Controls.Add(this.lblCalendarDate);
            this.panelControl1.Controls.Add(this.lblWorkplace);
            this.panelControl1.Controls.Add(this.lblProdMonth);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(946, 88);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // txtProdMonth
            // 
            this.txtProdMonth.Enabled = false;
            this.txtProdMonth.Location = new System.Drawing.Point(123, 9);
            this.txtProdMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtProdMonth.Name = "txtProdMonth";
            this.txtProdMonth.Size = new System.Drawing.Size(62, 20);
            this.txtProdMonth.TabIndex = 15;
            // 
            // txtSection
            // 
            this.txtSection.Enabled = false;
            this.txtSection.Location = new System.Drawing.Point(123, 32);
            this.txtSection.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSection.Name = "txtSection";
            this.txtSection.Size = new System.Drawing.Size(228, 20);
            this.txtSection.TabIndex = 14;
            // 
            // txtCalendarDate
            // 
            this.txtCalendarDate.Enabled = false;
            this.txtCalendarDate.Location = new System.Drawing.Point(123, 56);
            this.txtCalendarDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCalendarDate.Name = "txtCalendarDate";
            this.txtCalendarDate.Size = new System.Drawing.Size(86, 20);
            this.txtCalendarDate.TabIndex = 13;
            // 
            // lblCalendarDate
            // 
            this.lblCalendarDate.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblCalendarDate.Appearance.Options.UseFont = true;
            this.lblCalendarDate.Location = new System.Drawing.Point(12, 59);
            this.lblCalendarDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblCalendarDate.Name = "lblCalendarDate";
            this.lblCalendarDate.Size = new System.Drawing.Size(78, 12);
            this.lblCalendarDate.TabIndex = 12;
            this.lblCalendarDate.Text = "Calendar Date :";
            // 
            // lblWorkplace
            // 
            this.lblWorkplace.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblWorkplace.Appearance.Options.UseFont = true;
            this.lblWorkplace.Location = new System.Drawing.Point(12, 34);
            this.lblWorkplace.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblWorkplace.Name = "lblWorkplace";
            this.lblWorkplace.Size = new System.Drawing.Size(42, 12);
            this.lblWorkplace.TabIndex = 11;
            this.lblWorkplace.Text = "Section :";
            // 
            // lblProdMonth
            // 
            this.lblProdMonth.Appearance.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblProdMonth.Appearance.Options.UseFont = true;
            this.lblProdMonth.Location = new System.Drawing.Point(12, 11);
            this.lblProdMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblProdMonth.Name = "lblProdMonth";
            this.lblProdMonth.Size = new System.Drawing.Size(94, 12);
            this.lblProdMonth.TabIndex = 10;
            this.lblProdMonth.Text = "Production Month :";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 88);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(946, 316);
            this.panelControl2.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reApprove,
            this.reSQM,
            this.repositoryItemTextEdit1,
            this.reMOFC});
            this.gridControl1.Size = new System.Drawing.Size(942, 278);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcWorkplace,
            this.gcAct,
            this.gcProgFaceLength,
            this.gcProgAdvance,
            this.gcReconAdvance,
            this.gcProgCubics,
            this.gcReconCubics,
            this.gcApprove,
            this.gridColumn1,
            this.gcMOFC,
            this.gcComments,
            this.gcMonthPlan,
            this.gcReconFaceLength});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            // 
            // gcWorkplace
            // 
            this.gcWorkplace.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcWorkplace.AppearanceHeader.Options.UseFont = true;
            this.gcWorkplace.Caption = "Workplace";
            this.gcWorkplace.FieldName = "Workplace";
            this.gcWorkplace.Name = "gcWorkplace";
            this.gcWorkplace.OptionsColumn.AllowEdit = false;
            this.gcWorkplace.OptionsColumn.FixedWidth = true;
            this.gcWorkplace.OptionsColumn.ReadOnly = true;
            this.gcWorkplace.Visible = true;
            this.gcWorkplace.VisibleIndex = 0;
            this.gcWorkplace.Width = 182;
            // 
            // gcAct
            // 
            this.gcAct.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcAct.AppearanceHeader.Options.UseFont = true;
            this.gcAct.Caption = "Activity";
            this.gcAct.FieldName = "Activity";
            this.gcAct.Name = "gcAct";
            this.gcAct.OptionsColumn.AllowEdit = false;
            this.gcAct.OptionsColumn.FixedWidth = true;
            this.gcAct.OptionsColumn.ReadOnly = true;
            this.gcAct.Width = 57;
            // 
            // gcProgFaceLength
            // 
            this.gcProgFaceLength.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcProgFaceLength.AppearanceHeader.Options.UseFont = true;
            this.gcProgFaceLength.Caption = "Prog Book";
            this.gcProgFaceLength.FieldName = "ProgressiveFaceLength";
            this.gcProgFaceLength.Name = "gcProgFaceLength";
            this.gcProgFaceLength.OptionsColumn.AllowEdit = false;
            this.gcProgFaceLength.OptionsColumn.FixedWidth = true;
            this.gcProgFaceLength.OptionsColumn.ReadOnly = true;
            this.gcProgFaceLength.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ProgressiveFaceLength", "{0:0.##}")});
            this.gcProgFaceLength.Visible = true;
            this.gcProgFaceLength.VisibleIndex = 1;
            this.gcProgFaceLength.Width = 83;
            // 
            // gcProgAdvance
            // 
            this.gcProgAdvance.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcProgAdvance.AppearanceHeader.Options.UseFont = true;
            this.gcProgAdvance.Caption = "Prog Adv";
            this.gcProgAdvance.ColumnEdit = this.reSQM;
            this.gcProgAdvance.FieldName = "ProgressiveAdvance";
            this.gcProgAdvance.Name = "gcProgAdvance";
            this.gcProgAdvance.OptionsColumn.AllowEdit = false;
            this.gcProgAdvance.OptionsColumn.FixedWidth = true;
            this.gcProgAdvance.OptionsColumn.ReadOnly = true;
            this.gcProgAdvance.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ProgressiveAdvance", "{0:0.##}")});
            this.gcProgAdvance.Width = 61;
            // 
            // gcReconAdvance
            // 
            this.gcReconAdvance.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcReconAdvance.AppearanceHeader.Options.UseFont = true;
            this.gcReconAdvance.Caption = "Recon Adv";
            this.gcReconAdvance.ColumnEdit = this.reSQM;
            this.gcReconAdvance.FieldName = "ReconAdvance";
            this.gcReconAdvance.Name = "gcReconAdvance";
            this.gcReconAdvance.OptionsColumn.FixedWidth = true;
            this.gcReconAdvance.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ReconAdvance", "{0:0.##}")});
            this.gcReconAdvance.Width = 71;
            // 
            // gcProgCubics
            // 
            this.gcProgCubics.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcProgCubics.AppearanceHeader.Options.UseFont = true;
            this.gcProgCubics.Caption = "Prog Cubics";
            this.gcProgCubics.FieldName = "ProgressiveCubics";
            this.gcProgCubics.Name = "gcProgCubics";
            this.gcProgCubics.OptionsColumn.AllowEdit = false;
            this.gcProgCubics.OptionsColumn.FixedWidth = true;
            this.gcProgCubics.OptionsColumn.ReadOnly = true;
            this.gcProgCubics.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ProgressiveCubics", "{0:0.##}")});
            this.gcProgCubics.Width = 78;
            // 
            // gcReconCubics
            // 
            this.gcReconCubics.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcReconCubics.AppearanceHeader.Options.UseFont = true;
            this.gcReconCubics.Caption = "Recon Cubics";
            this.gcReconCubics.ColumnEdit = this.reSQM;
            this.gcReconCubics.FieldName = "ReconCubics";
            this.gcReconCubics.Name = "gcReconCubics";
            this.gcReconCubics.OptionsColumn.FixedWidth = true;
            this.gcReconCubics.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ReconCubics", "{0:0.##}")});
            this.gcReconCubics.Width = 82;
            // 
            // gcApprove
            // 
            this.gcApprove.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcApprove.AppearanceHeader.Options.UseFont = true;
            this.gcApprove.Caption = "Blast Barricade";
            this.gcApprove.ColumnEdit = this.reApprove;
            this.gcApprove.FieldName = "Approved";
            this.gcApprove.Name = "gcApprove";
            this.gcApprove.OptionsColumn.FixedWidth = true;
            this.gcApprove.Visible = true;
            this.gcApprove.VisibleIndex = 5;
            this.gcApprove.Width = 96;
            // 
            // reApprove
            // 
            this.reApprove.AutoHeight = false;
            this.reApprove.Name = "reApprove";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowSize = false;
            this.gridColumn1.Width = 22;
            // 
            // gcMOFC
            // 
            this.gcMOFC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcMOFC.AppearanceHeader.Options.UseFont = true;
            this.gcMOFC.Caption = "Forecast";
            this.gcMOFC.FieldName = "MOFC";
            this.gcMOFC.Name = "gcMOFC";
            this.gcMOFC.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MOFC", "{0:0.#}")});
            this.gcMOFC.Visible = true;
            this.gcMOFC.VisibleIndex = 4;
            this.gcMOFC.Width = 64;
            // 
            // gcComments
            // 
            this.gcComments.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcComments.AppearanceHeader.Options.UseFont = true;
            this.gcComments.Caption = "Shiftboss Forecast Issue";
            this.gcComments.ColumnEdit = this.repositoryItemTextEdit1;
            this.gcComments.FieldName = "MOComment";
            this.gcComments.Name = "gcComments";
            this.gcComments.Visible = true;
            this.gcComments.VisibleIndex = 6;
            this.gcComments.Width = 349;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.MaxLength = 42;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gcMonthPlan
            // 
            this.gcMonthPlan.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcMonthPlan.AppearanceHeader.Options.UseFont = true;
            this.gcMonthPlan.Caption = "Tot Plan";
            this.gcMonthPlan.FieldName = "MonthPlan";
            this.gcMonthPlan.Name = "gcMonthPlan";
            this.gcMonthPlan.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MonthPlan", "{0:0.#}")});
            this.gcMonthPlan.Visible = true;
            this.gcMonthPlan.VisibleIndex = 3;
            this.gcMonthPlan.Width = 67;
            // 
            // gcReconFaceLength
            // 
            this.gcReconFaceLength.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcReconFaceLength.AppearanceHeader.Options.UseFont = true;
            this.gcReconFaceLength.Caption = "Recon Sqm";
            this.gcReconFaceLength.ColumnEdit = this.reSQM;
            this.gcReconFaceLength.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcReconFaceLength.FieldName = "ReconFaceLength";
            this.gcReconFaceLength.Name = "gcReconFaceLength";
            this.gcReconFaceLength.OptionsColumn.FixedWidth = true;
            this.gcReconFaceLength.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ReconFaceLength", "{0:0.#}")});
            this.gcReconFaceLength.Visible = true;
            this.gcReconFaceLength.VisibleIndex = 2;
            this.gcReconFaceLength.Width = 83;
            // 
            // reMOFC
            // 
            this.reMOFC.AutoHeight = false;
            this.reMOFC.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.reMOFC.Mask.EditMask = "n0";
            this.reMOFC.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.reMOFC.Mask.UseMaskAsDisplayFormat = true;
            this.reMOFC.Name = "reMOFC";
            this.reMOFC.NullText = "0";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnClose);
            this.panelControl3.Controls.Add(this.btnSave);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 280);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(942, 34);
            this.panelControl3.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.Location = new System.Drawing.Point(849, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 27);
            this.btnClose.TabIndex = 73;
            this.btnClose.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Cancel;
            this.btnClose.theCustomeText = null;
            this.btnClose.theImage = ((System.Drawing.Image)(resources.GetObject("btnClose.theImage")));
            this.btnClose.theImageHot = ((System.Drawing.Image)(resources.GetObject("btnClose.theImageHot")));
            this.btnClose.theSuperToolTip = null;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(765, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 27);
            this.btnSave.TabIndex = 71;
            this.btnSave.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Save;
            this.btnSave.theCustomeText = null;
            this.btnSave.theImage = ((System.Drawing.Image)(resources.GetObject("btnSave.theImage")));
            this.btnSave.theImageHot = ((System.Drawing.Image)(resources.GetObject("btnSave.theImageHot")));
            this.btnSave.theSuperToolTip = null;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ReconBookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 404);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "ReconBookingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Booking Recon";
            this.Load += new System.EventHandler(this.frmReconBooking_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReconBooking_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.reSQM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProdMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCalendarDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reApprove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reMOFC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtProdMonth;
        private DevExpress.XtraEditors.TextEdit txtSection;
        private DevExpress.XtraEditors.TextEdit txtCalendarDate;
        private DevExpress.XtraEditors.LabelControl lblCalendarDate;
        private DevExpress.XtraEditors.LabelControl lblWorkplace;
        private DevExpress.XtraEditors.LabelControl lblProdMonth;
        private DevExpress.XtraGrid.Columns.GridColumn gcWorkplace;
        private DevExpress.XtraGrid.Columns.GridColumn gcProgFaceLength;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reSQM;
        private DevExpress.XtraGrid.Columns.GridColumn gcApprove;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit reApprove;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private Global.sysButtons.ucSysBtn btnSave;
        private DevExpress.XtraGrid.Columns.GridColumn gcAct;
        private DevExpress.XtraGrid.Columns.GridColumn gcProgAdvance;
        private DevExpress.XtraGrid.Columns.GridColumn gcReconAdvance;
        private DevExpress.XtraGrid.Columns.GridColumn gcProgCubics;
        private DevExpress.XtraGrid.Columns.GridColumn gcReconCubics;
        private DevExpress.XtraGrid.Columns.GridColumn gcMOFC;
        private DevExpress.XtraGrid.Columns.GridColumn gcComments;
        private DevExpress.XtraGrid.Columns.GridColumn gcMonthPlan;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private Global.sysButtons.ucSysBtn btnClose;
        private DevExpress.XtraGrid.Columns.GridColumn gcReconFaceLength;
        public DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reMOFC;
    }
}
