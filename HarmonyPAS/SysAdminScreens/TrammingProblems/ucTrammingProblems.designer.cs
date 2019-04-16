namespace Mineware.Systems.Production.SysAdminScreens.TrammingProblems
{
    partial class ucTrammingProblems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTrammingProblems));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.gcSectionScreen = new DevExpress.XtraGrid.GridControl();
            this.gvSectionScreen = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ProblemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mwButton1 = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSectionScreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSectionScreen)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.mwButton1);
            this.splitContainer1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.btnEdit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gcSectionScreen);
            this.splitContainer1.Size = new System.Drawing.Size(868, 350);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Location = new System.Drawing.Point(207, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(661, 40);
            this.panelControl1.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.ImageLeftPadding = 0;
            this.btnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.ImageOptions.Image")));
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 40);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Add;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.ImageLeftPadding = 0;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.Location = new System.Drawing.Point(70, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 40);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Edit";
            this.btnEdit.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Edit;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // gcSectionScreen
            // 
            this.gcSectionScreen.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcSectionScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSectionScreen.Location = new System.Drawing.Point(0, 0);
            this.gcSectionScreen.MainView = this.gvSectionScreen;
            this.gcSectionScreen.Name = "gcSectionScreen";
            this.gcSectionScreen.Size = new System.Drawing.Size(868, 306);
            this.gcSectionScreen.TabIndex = 3;
            this.gcSectionScreen.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSectionScreen});
            // 
            // gvSectionScreen
            // 
            this.gvSectionScreen.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_ID,
            this.col_ProblemCode,
            this.col_Status});
            this.gvSectionScreen.GridControl = this.gcSectionScreen;
            this.gvSectionScreen.Name = "gvSectionScreen";
            this.gvSectionScreen.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSectionScreen.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSectionScreen.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gvSectionScreen.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvSectionScreen.OptionsBehavior.ReadOnly = true;
            this.gvSectionScreen.OptionsCustomization.AllowColumnMoving = false;
            this.gvSectionScreen.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.False;
            this.gvSectionScreen.OptionsView.ShowAutoFilterRow = true;
            this.gvSectionScreen.OptionsView.ShowGroupPanel = false;
            this.gvSectionScreen.EditFormPrepared += new DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventHandler(this.gvSectionScreen_EditFormPrepared);
            this.gvSectionScreen.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvSectionScreen_InitNewRow);
            this.gvSectionScreen.DoubleClick += new System.EventHandler(this.gvSectionScreen_DoubleClick);
            // 
            // col_ID
            // 
            this.col_ID.Caption = "ID";
            this.col_ID.FieldName = "ID";
            this.col_ID.Name = "col_ID";
            // 
            // col_ProblemCode
            // 
            this.col_ProblemCode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.col_ProblemCode.AppearanceHeader.Options.UseFont = true;
            this.col_ProblemCode.Caption = "Problem";
            this.col_ProblemCode.FieldName = "ProblemCode";
            this.col_ProblemCode.Name = "col_ProblemCode";
            this.col_ProblemCode.Visible = true;
            this.col_ProblemCode.VisibleIndex = 0;
            this.col_ProblemCode.Width = 160;
            // 
            // col_Status
            // 
            this.col_Status.AppearanceCell.Options.UseTextOptions = true;
            this.col_Status.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Status.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.col_Status.AppearanceHeader.Options.UseFont = true;
            this.col_Status.AppearanceHeader.Options.UseTextOptions = true;
            this.col_Status.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_Status.Caption = "Status";
            this.col_Status.FieldName = "Status";
            this.col_Status.Name = "col_Status";
            this.col_Status.Width = 118;
            // 
            // mwButton1
            // 
            this.mwButton1.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.mwButton1.Appearance.Options.UseFont = true;
            this.mwButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.mwButton1.ImageLeftPadding = 0;
            this.mwButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mwButton1.ImageOptions.Image")));
            this.mwButton1.Location = new System.Drawing.Point(139, 0);
            this.mwButton1.Margin = new System.Windows.Forms.Padding(4);
            this.mwButton1.Name = "mwButton1";
            this.mwButton1.Size = new System.Drawing.Size(70, 40);
            this.mwButton1.TabIndex = 13;
            this.mwButton1.Text = "Refresh";
            this.mwButton1.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Show;
            this.mwButton1.Click += new System.EventHandler(this.mwButton1_Click);
            // 
            // ucTrammingProblems
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucTrammingProblems";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(868, 350);
            this.Load += new System.EventHandler(this.ucSectionScreen_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSectionScreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSectionScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gcSectionScreen;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSectionScreen;
        private DevExpress.XtraGrid.Columns.GridColumn col_ProblemCode;
        private DevExpress.XtraGrid.Columns.GridColumn col_Status;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Global.CustomControls.MWButton btnAdd;
        private Global.CustomControls.MWButton btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn col_ID;
        private Global.CustomControls.MWButton mwButton1;
    }
}
