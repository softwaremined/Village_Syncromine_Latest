namespace Mineware.Systems.Production.SysAdminScreens.TrammingBoxholes
{
    partial class ucTrammingBoxholes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTrammingBoxholes));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAdd = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.gcTrammingBoxholes = new DevExpress.XtraGrid.GridControl();
            this.gvTrammingBoxholes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_lvl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Distance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Inactive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_SectionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_LevelNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ReefType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_BoxholeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_OreFlowID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mwButton1 = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTrammingBoxholes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrammingBoxholes)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.btnEdit);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gcTrammingBoxholes);
            this.splitContainer1.Size = new System.Drawing.Size(868, 350);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 4;
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
            // gcTrammingBoxholes
            // 
            this.gcTrammingBoxholes.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcTrammingBoxholes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTrammingBoxholes.Location = new System.Drawing.Point(0, 0);
            this.gcTrammingBoxholes.MainView = this.gvTrammingBoxholes;
            this.gcTrammingBoxholes.Name = "gcTrammingBoxholes";
            this.gcTrammingBoxholes.Size = new System.Drawing.Size(868, 306);
            this.gcTrammingBoxholes.TabIndex = 3;
            this.gcTrammingBoxholes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTrammingBoxholes});
            // 
            // gvTrammingBoxholes
            // 
            this.gvTrammingBoxholes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_lvl,
            this.col_Distance,
            this.col_Inactive,
            this.col_SectionID,
            this.col_LevelNumber,
            this.col_ReefType,
            this.col_BoxholeName,
            this.col_OreFlowID,
            this.col_Status});
            this.gvTrammingBoxholes.GridControl = this.gcTrammingBoxholes;
            this.gvTrammingBoxholes.Name = "gvTrammingBoxholes";
            this.gvTrammingBoxholes.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvTrammingBoxholes.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvTrammingBoxholes.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            this.gvTrammingBoxholes.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvTrammingBoxholes.OptionsBehavior.ReadOnly = true;
            this.gvTrammingBoxholes.OptionsCustomization.AllowColumnMoving = false;
            this.gvTrammingBoxholes.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.False;
            this.gvTrammingBoxholes.OptionsView.ShowAutoFilterRow = true;
            this.gvTrammingBoxholes.OptionsView.ShowGroupPanel = false;
            this.gvTrammingBoxholes.EditFormPrepared += new DevExpress.XtraGrid.Views.Grid.EditFormPreparedEventHandler(this.gvTrammingBoxholes_EditFormPrepared);
            this.gvTrammingBoxholes.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvTrammingBoxholes_InitNewRow);
            this.gvTrammingBoxholes.DoubleClick += new System.EventHandler(this.gvTrammingBoxholes_DoubleClick);
            // 
            // col_lvl
            // 
            this.col_lvl.Caption = "lvl";
            this.col_lvl.FieldName = "lvl";
            this.col_lvl.Name = "col_lvl";
            // 
            // col_Distance
            // 
            this.col_Distance.Caption = "Distance";
            this.col_Distance.FieldName = "BoxDistance";
            this.col_Distance.Name = "col_Distance";
            this.col_Distance.Visible = true;
            this.col_Distance.VisibleIndex = 6;
            // 
            // col_Inactive
            // 
            this.col_Inactive.Caption = "In Act.";
            this.col_Inactive.FieldName = "Inactive";
            this.col_Inactive.Name = "col_Inactive";
            this.col_Inactive.Visible = true;
            this.col_Inactive.VisibleIndex = 5;
            // 
            // col_SectionID
            // 
            this.col_SectionID.Caption = "Section";
            this.col_SectionID.FieldName = "SectionID";
            this.col_SectionID.Name = "col_SectionID";
            this.col_SectionID.Visible = true;
            this.col_SectionID.VisibleIndex = 4;
            // 
            // col_LevelNumber
            // 
            this.col_LevelNumber.Caption = "Level";
            this.col_LevelNumber.FieldName = "LevelNumber";
            this.col_LevelNumber.Name = "col_LevelNumber";
            this.col_LevelNumber.Visible = true;
            this.col_LevelNumber.VisibleIndex = 3;
            // 
            // col_ReefType
            // 
            this.col_ReefType.Caption = "Reef Type";
            this.col_ReefType.FieldName = "ReefType";
            this.col_ReefType.Name = "col_ReefType";
            this.col_ReefType.Visible = true;
            this.col_ReefType.VisibleIndex = 2;
            // 
            // col_BoxholeName
            // 
            this.col_BoxholeName.Caption = "Boxhole Name";
            this.col_BoxholeName.FieldName = "Name";
            this.col_BoxholeName.Name = "col_BoxholeName";
            this.col_BoxholeName.Visible = true;
            this.col_BoxholeName.VisibleIndex = 1;
            // 
            // col_OreFlowID
            // 
            this.col_OreFlowID.Caption = "BoxholeID";
            this.col_OreFlowID.FieldName = "OreFlowID";
            this.col_OreFlowID.Name = "col_OreFlowID";
            this.col_OreFlowID.Visible = true;
            this.col_OreFlowID.VisibleIndex = 0;
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
            // ucTrammingBoxholes
            // 
            this.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ucTrammingBoxholes";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(868, 350);
            this.Load += new System.EventHandler(this.ucTrammingBoxholes_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTrammingBoxholes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrammingBoxholes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gcTrammingBoxholes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTrammingBoxholes;
        private DevExpress.XtraGrid.Columns.GridColumn col_Status;
        private Global.CustomControls.MWButton btnAdd;
        private Global.CustomControls.MWButton btnEdit;
        private DevExpress.XtraGrid.Columns.GridColumn col_Distance;
        private DevExpress.XtraGrid.Columns.GridColumn col_Inactive;
        private DevExpress.XtraGrid.Columns.GridColumn col_SectionID;
        private DevExpress.XtraGrid.Columns.GridColumn col_ReefType;
        private DevExpress.XtraGrid.Columns.GridColumn col_BoxholeName;
        private DevExpress.XtraGrid.Columns.GridColumn col_OreFlowID;
        private DevExpress.XtraGrid.Columns.GridColumn col_LevelNumber;
        private DevExpress.XtraGrid.Columns.GridColumn col_lvl;
        private Global.CustomControls.MWButton mwButton1;
    }
}
