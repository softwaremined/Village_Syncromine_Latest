namespace Mineware.Systems.Production.SysAdminScreens.ProblemSetup
{
    partial class ucProblemSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProblemSetup));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.BtnEdit = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            this.BtnAdd = new Mineware.Systems.Global.sysButtons.ucSysBtn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.rgActivity = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtrTProblemTypes = new DevExpress.XtraTab.XtraTabPage();
            this.gcProblemTypes = new DevExpress.XtraGrid.GridControl();
            this.gvProblemTypes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcEnquirersID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcColorPicker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtrTProblemDescription = new DevExpress.XtraTab.XtraTabPage();
            this.gcProblemDescription = new DevExpress.XtraGrid.GridControl();
            this.gvProblemDescription = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcProblemID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDescProblemID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDrillrig = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCausedLostBlast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtrTProblemNotes = new DevExpress.XtraTab.XtraTabPage();
            this.gcProblemNotes = new DevExpress.XtraGrid.GridControl();
            this.gvProblemNotes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcNoteID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDescNoteID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcExplanation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNotLostBlast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgActivity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtrTProblemTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProblemTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProblemTypes)).BeginInit();
            this.xtrTProblemDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProblemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProblemDescription)).BeginInit();
            this.xtrTProblemNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProblemNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProblemNotes)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(964, 68);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.BtnEdit);
            this.panelControl3.Controls.Add(this.BtnAdd);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(315, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(649, 68);
            this.panelControl3.TabIndex = 1;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnEdit.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnEdit.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnEdit.Location = new System.Drawing.Point(102, 2);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(6, 12, 6, 12);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(100, 64);
            this.BtnEdit.TabIndex = 31;
            this.BtnEdit.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Edit;
            this.BtnEdit.theCustomeText = null;
            this.BtnEdit.theImage = ((System.Drawing.Image)(resources.GetObject("BtnEdit.theImage")));
            this.BtnEdit.theImageHot = ((System.Drawing.Image)(resources.GetObject("BtnEdit.theImageHot")));
            this.BtnEdit.theSuperToolTip = null;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnAdd.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnAdd.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnAdd.Location = new System.Drawing.Point(2, 2);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(100, 64);
            this.BtnAdd.TabIndex = 42;
            this.BtnAdd.theButtonTye = Mineware.Systems.Global.sysButtons.ButtonType.Add;
            this.BtnAdd.theCustomeText = null;
            this.BtnAdd.theImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.theImage")));
            this.BtnAdd.theImageHot = ((System.Drawing.Image)(resources.GetObject("BtnAdd.theImageHot")));
            this.BtnAdd.theSuperToolTip = null;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.rgActivity);
            this.panelControl2.Controls.Add(this.labelControl24);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(315, 68);
            this.panelControl2.TabIndex = 31;
            // 
            // rgActivity
            // 
            this.rgActivity.Location = new System.Drawing.Point(71, 17);
            this.rgActivity.Name = "rgActivity";
            this.rgActivity.Properties.Columns = 2;
            this.rgActivity.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Development")});
            this.rgActivity.Size = new System.Drawing.Size(228, 29);
            this.rgActivity.TabIndex = 29;
            this.rgActivity.SelectedIndexChanged += new System.EventHandler(this.rgActivity_SelectedIndexChanged);
            // 
            // labelControl24
            // 
            this.labelControl24.Location = new System.Drawing.Point(15, 24);
            this.labelControl24.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(40, 13);
            this.labelControl24.TabIndex = 28;
            this.labelControl24.Text = "Activity:";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 68);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtrTProblemTypes;
            this.xtraTabControl1.Size = new System.Drawing.Size(964, 437);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtrTProblemTypes,
            this.xtrTProblemDescription,
            this.xtrTProblemNotes});
            this.xtraTabControl1.DoubleClick += new System.EventHandler(this.BtnEdit_Click);
            // 
            // xtrTProblemTypes
            // 
            this.xtrTProblemTypes.Controls.Add(this.gcProblemTypes);
            this.xtrTProblemTypes.Name = "xtrTProblemTypes";
            this.xtrTProblemTypes.Size = new System.Drawing.Size(958, 409);
            this.xtrTProblemTypes.Text = "Problem Types";
            // 
            // gcProblemTypes
            // 
            this.gcProblemTypes.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcProblemTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcProblemTypes.Location = new System.Drawing.Point(0, 0);
            this.gcProblemTypes.MainView = this.gvProblemTypes;
            this.gcProblemTypes.Name = "gcProblemTypes";
            this.gcProblemTypes.Size = new System.Drawing.Size(958, 409);
            this.gcProblemTypes.TabIndex = 0;
            this.gcProblemTypes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProblemTypes});
            // 
            // gvProblemTypes
            // 
            this.gvProblemTypes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcEnquirersID,
            this.gcDesc,
            this.gcColorPicker,
            this.gridColumn1});
            this.gvProblemTypes.GridControl = this.gcProblemTypes;
            this.gvProblemTypes.Name = "gvProblemTypes";
            this.gvProblemTypes.OptionsBehavior.Editable = false;
            this.gvProblemTypes.OptionsView.ShowGroupPanel = false;
            this.gvProblemTypes.DoubleClick += new System.EventHandler(this.BtnEdit_Click);
            // 
            // gcEnquirersID
            // 
            this.gcEnquirersID.Caption = "Enquirers ID";
            this.gcEnquirersID.FieldName = "ProblemTypeID";
            this.gcEnquirersID.Name = "gcEnquirersID";
            this.gcEnquirersID.OptionsColumn.FixedWidth = true;
            this.gcEnquirersID.Visible = true;
            this.gcEnquirersID.VisibleIndex = 0;
            this.gcEnquirersID.Width = 90;
            // 
            // gcDesc
            // 
            this.gcDesc.Caption = "Description";
            this.gcDesc.FieldName = "Description";
            this.gcDesc.Name = "gcDesc";
            this.gcDesc.OptionsColumn.FixedWidth = true;
            this.gcDesc.Visible = true;
            this.gcDesc.VisibleIndex = 1;
            this.gcDesc.Width = 250;
            // 
            // gcColorPicker
            // 
            this.gcColorPicker.Name = "gcColorPicker";
            this.gcColorPicker.OptionsColumn.FixedWidth = true;
            this.gcColorPicker.Visible = true;
            this.gcColorPicker.VisibleIndex = 2;
            this.gcColorPicker.Width = 90;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 637;
            // 
            // xtrTProblemDescription
            // 
            this.xtrTProblemDescription.Controls.Add(this.gcProblemDescription);
            this.xtrTProblemDescription.Name = "xtrTProblemDescription";
            this.xtrTProblemDescription.Size = new System.Drawing.Size(958, 409);
            this.xtrTProblemDescription.Text = "Problem Description";
            // 
            // gcProblemDescription
            // 
            this.gcProblemDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcProblemDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcProblemDescription.Location = new System.Drawing.Point(0, 0);
            this.gcProblemDescription.MainView = this.gvProblemDescription;
            this.gcProblemDescription.Name = "gcProblemDescription";
            this.gcProblemDescription.Size = new System.Drawing.Size(958, 409);
            this.gcProblemDescription.TabIndex = 0;
            this.gcProblemDescription.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProblemDescription});
            this.gcProblemDescription.DoubleClick += new System.EventHandler(this.BtnEdit_Click);
            // 
            // gvProblemDescription
            // 
            this.gvProblemDescription.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcProblemID,
            this.gcDescProblemID,
            this.gcDrillrig,
            this.gcCausedLostBlast});
            this.gvProblemDescription.GridControl = this.gcProblemDescription;
            this.gvProblemDescription.Name = "gvProblemDescription";
            this.gvProblemDescription.OptionsBehavior.Editable = false;
            this.gvProblemDescription.OptionsView.ShowGroupPanel = false;
            // 
            // gcProblemID
            // 
            this.gcProblemID.Caption = "Problem ID";
            this.gcProblemID.FieldName = "ProblemID";
            this.gcProblemID.Name = "gcProblemID";
            this.gcProblemID.OptionsColumn.FixedWidth = true;
            this.gcProblemID.Visible = true;
            this.gcProblemID.VisibleIndex = 0;
            this.gcProblemID.Width = 90;
            // 
            // gcDescProblemID
            // 
            this.gcDescProblemID.Caption = "Description";
            this.gcDescProblemID.FieldName = "Description";
            this.gcDescProblemID.Name = "gcDescProblemID";
            this.gcDescProblemID.OptionsColumn.FixedWidth = true;
            this.gcDescProblemID.Visible = true;
            this.gcDescProblemID.VisibleIndex = 1;
            this.gcDescProblemID.Width = 250;
            // 
            // gcDrillrig
            // 
            this.gcDrillrig.Caption = "Drillrig";
            this.gcDrillrig.FieldName = "TheDrillRig";
            this.gcDrillrig.MinWidth = 10;
            this.gcDrillrig.Name = "gcDrillrig";
            this.gcDrillrig.Width = 20;
            // 
            // gcCausedLostBlast
            // 
            this.gcCausedLostBlast.Caption = "Caused Lost Blast";
            this.gcCausedLostBlast.FieldName = "IsCausedLostBlast";
            this.gcCausedLostBlast.Name = "gcCausedLostBlast";
            this.gcCausedLostBlast.OptionsColumn.FixedWidth = true;
            this.gcCausedLostBlast.Visible = true;
            this.gcCausedLostBlast.VisibleIndex = 2;
            // 
            // xtrTProblemNotes
            // 
            this.xtrTProblemNotes.Controls.Add(this.gcProblemNotes);
            this.xtrTProblemNotes.Name = "xtrTProblemNotes";
            this.xtrTProblemNotes.Size = new System.Drawing.Size(958, 409);
            this.xtrTProblemNotes.Text = "Problem Notes";
            // 
            // gcProblemNotes
            // 
            this.gcProblemNotes.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcProblemNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcProblemNotes.Location = new System.Drawing.Point(0, 0);
            this.gcProblemNotes.MainView = this.gvProblemNotes;
            this.gcProblemNotes.Name = "gcProblemNotes";
            this.gcProblemNotes.Size = new System.Drawing.Size(958, 409);
            this.gcProblemNotes.TabIndex = 1;
            this.gcProblemNotes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProblemNotes});
            this.gcProblemNotes.DoubleClick += new System.EventHandler(this.BtnEdit_Click);
            // 
            // gvProblemNotes
            // 
            this.gvProblemNotes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcNoteID,
            this.gcDescNoteID,
            this.gcExplanation,
            this.gcNotLostBlast,
            this.gridColumn8});
            this.gvProblemNotes.GridControl = this.gcProblemNotes;
            this.gvProblemNotes.Name = "gvProblemNotes";
            this.gvProblemNotes.OptionsBehavior.Editable = false;
            this.gvProblemNotes.OptionsView.ShowGroupPanel = false;
            // 
            // gcNoteID
            // 
            this.gcNoteID.Caption = "Note ID";
            this.gcNoteID.FieldName = "NoteID";
            this.gcNoteID.Name = "gcNoteID";
            this.gcNoteID.OptionsColumn.FixedWidth = true;
            this.gcNoteID.Visible = true;
            this.gcNoteID.VisibleIndex = 0;
            this.gcNoteID.Width = 90;
            // 
            // gcDescNoteID
            // 
            this.gcDescNoteID.Caption = "Description";
            this.gcDescNoteID.FieldName = "Description";
            this.gcDescNoteID.Name = "gcDescNoteID";
            this.gcDescNoteID.OptionsColumn.FixedWidth = true;
            this.gcDescNoteID.Visible = true;
            this.gcDescNoteID.VisibleIndex = 1;
            this.gcDescNoteID.Width = 250;
            // 
            // gcExplanation
            // 
            this.gcExplanation.Caption = "Explanation";
            this.gcExplanation.FieldName = "Explanation";
            this.gcExplanation.Name = "gcExplanation";
            this.gcExplanation.OptionsColumn.FixedWidth = true;
            this.gcExplanation.Visible = true;
            this.gcExplanation.VisibleIndex = 2;
            this.gcExplanation.Width = 200;
            // 
            // gcNotLostBlast
            // 
            this.gcNotLostBlast.Caption = "Lost Blast";
            this.gcNotLostBlast.FieldName = "LostBlast";
            this.gcNotLostBlast.Name = "gcNotLostBlast";
            this.gcNotLostBlast.OptionsColumn.FixedWidth = true;
            this.gcNotLostBlast.Visible = true;
            this.gcNotLostBlast.VisibleIndex = 3;
            this.gcNotLostBlast.Width = 130;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 270;
            // 
            // ucProblemSetup
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucProblemSetup";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(964, 505);
            this.Load += new System.EventHandler(this.ucCPMProblemSetup_Load);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.xtraTabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgActivity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtrTProblemTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcProblemTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProblemTypes)).EndInit();
            this.xtrTProblemDescription.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcProblemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProblemDescription)).EndInit();
            this.xtrTProblemNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcProblemNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProblemNotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtrTProblemTypes;
        private DevExpress.XtraTab.XtraTabPage xtrTProblemDescription;
        private DevExpress.XtraTab.XtraTabPage xtrTProblemNotes;
        private DevExpress.XtraGrid.GridControl gcProblemTypes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProblemTypes;
        private DevExpress.XtraGrid.GridControl gcProblemDescription;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProblemDescription;
        private DevExpress.XtraGrid.GridControl gcProblemNotes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProblemNotes;
        private DevExpress.XtraGrid.Columns.GridColumn gcEnquirersID;
        private DevExpress.XtraGrid.Columns.GridColumn gcDesc;
        private DevExpress.XtraGrid.Columns.GridColumn gcColorPicker;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gcProblemID;
        private DevExpress.XtraGrid.Columns.GridColumn gcDescProblemID;
        private DevExpress.XtraGrid.Columns.GridColumn gcNoteID;
        private DevExpress.XtraGrid.Columns.GridColumn gcDescNoteID;
        private DevExpress.XtraGrid.Columns.GridColumn gcExplanation;
        private DevExpress.XtraGrid.Columns.GridColumn gcNotLostBlast;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private Global.sysButtons.ucSysBtn BtnEdit;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.RadioGroup rgActivity;
        private DevExpress.XtraEditors.LabelControl labelControl24;
        private DevExpress.XtraGrid.Columns.GridColumn gcDrillrig;
        private Global.sysButtons.ucSysBtn BtnAdd;
        private DevExpress.XtraGrid.Columns.GridColumn gcCausedLostBlast;
    }
}
