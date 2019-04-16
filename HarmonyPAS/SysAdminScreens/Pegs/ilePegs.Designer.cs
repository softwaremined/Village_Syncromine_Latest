namespace Mineware.Systems.Production.SysAdminScreens.Pegs
{
    partial class ilePegs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ilePegs));
            this.txtWorkplaceID = new DevExpress.XtraEditors.TextEdit();
            this.txtLetter2 = new DevExpress.XtraEditors.TextEdit();
            this.txtPegID = new DevExpress.XtraEditors.TextEdit();
            this.txtDescription = new DevExpress.XtraEditors.TextEdit();
            this.txttheValue = new DevExpress.XtraEditors.TextEdit();
            this.txtLetter1 = new DevExpress.XtraEditors.TextEdit();
            this.lblWorkplaceID = new DevExpress.XtraEditors.LabelControl();
            this.lblLetter1 = new DevExpress.XtraEditors.LabelControl();
            this.lbltheValue = new DevExpress.XtraEditors.LabelControl();
            this.lblPegID = new DevExpress.XtraEditors.LabelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblLetter2 = new DevExpress.XtraEditors.LabelControl();
            this.lblLetter3 = new DevExpress.XtraEditors.LabelControl();
            this.txtLetter3 = new DevExpress.XtraEditors.TextEdit();
            this.gcWPPegs = new DevExpress.XtraGrid.GridControl();
            this.gvWPPegs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_WPPegID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_WPtheValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_WPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.dxPegsErrorSetup = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetter2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPegID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttheValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetter1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetter3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWPPegs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWPPegs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxPegsErrorSetup)).BeginInit();
            this.SuspendLayout();
            // 
            // txtWorkplaceID
            // 
            this.SetBoundFieldName(this.txtWorkplaceID, "WorkplaceID");
            this.SetBoundPropertyName(this.txtWorkplaceID, "EditValue");
            this.txtWorkplaceID.Enabled = false;
            this.txtWorkplaceID.Location = new System.Drawing.Point(107, 23);
            this.txtWorkplaceID.Name = "txtWorkplaceID";
            this.txtWorkplaceID.Size = new System.Drawing.Size(100, 20);
            this.txtWorkplaceID.TabIndex = 0;
            // 
            // txtLetter2
            // 
            this.SetBoundFieldName(this.txtLetter2, "Letter2");
            this.SetBoundPropertyName(this.txtLetter2, "EditValue");
            this.txtLetter2.Location = new System.Drawing.Point(107, 129);
            this.txtLetter2.Name = "txtLetter2";
            this.txtLetter2.Properties.MaxLength = 50;
            this.txtLetter2.Size = new System.Drawing.Size(285, 20);
            this.txtLetter2.TabIndex = 5;
            // 
            // txtPegID
            // 
            this.SetBoundFieldName(this.txtPegID, "PegID");
            this.SetBoundPropertyName(this.txtPegID, "EditValue");
            this.txtPegID.Location = new System.Drawing.Point(107, 77);
            this.txtPegID.Name = "txtPegID";
            this.txtPegID.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPegID.Size = new System.Drawing.Size(80, 20);
            this.txtPegID.TabIndex = 2;
            this.txtPegID.Enter += new System.EventHandler(this.txtPegID_Enter);
            // 
            // txtDescription
            // 
            this.SetBoundFieldName(this.txtDescription, "Description");
            this.SetBoundPropertyName(this.txtDescription, "EditValue");
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(107, 51);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(168, 20);
            this.txtDescription.TabIndex = 1;
            // 
            // txttheValue
            // 
            this.SetBoundFieldName(this.txttheValue, "theValue");
            this.SetBoundPropertyName(this.txttheValue, "EditValue");
            this.txttheValue.Location = new System.Drawing.Point(277, 77);
            this.txttheValue.Name = "txttheValue";
            this.txttheValue.Properties.Mask.EditMask = "-?\\d+(\\R.\\d{0,1})";
            this.txttheValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txttheValue.Size = new System.Drawing.Size(80, 20);
            this.txttheValue.TabIndex = 3;
            // 
            // txtLetter1
            // 
            this.SetBoundFieldName(this.txtLetter1, "Letter1");
            this.SetBoundPropertyName(this.txtLetter1, "EditValue");
            this.txtLetter1.Location = new System.Drawing.Point(107, 103);
            this.txtLetter1.Name = "txtLetter1";
            this.txtLetter1.Properties.MaxLength = 50;
            this.txtLetter1.Size = new System.Drawing.Size(285, 20);
            this.txtLetter1.TabIndex = 4;
            // 
            // lblWorkplaceID
            // 
            this.SetBoundPropertyName(this.lblWorkplaceID, "");
            this.lblWorkplaceID.Location = new System.Drawing.Point(19, 26);
            this.lblWorkplaceID.Name = "lblWorkplaceID";
            this.lblWorkplaceID.Size = new System.Drawing.Size(64, 13);
            this.lblWorkplaceID.TabIndex = 6;
            this.lblWorkplaceID.Text = "Workplace ID";
            // 
            // lblLetter1
            // 
            this.SetBoundPropertyName(this.lblLetter1, "");
            this.lblLetter1.Location = new System.Drawing.Point(19, 106);
            this.lblLetter1.Name = "lblLetter1";
            this.lblLetter1.Size = new System.Drawing.Size(38, 13);
            this.lblLetter1.TabIndex = 7;
            this.lblLetter1.Text = "Letter 1";
            // 
            // lbltheValue
            // 
            this.SetBoundPropertyName(this.lbltheValue, "");
            this.lbltheValue.Location = new System.Drawing.Point(224, 80);
            this.lbltheValue.Name = "lbltheValue";
            this.lbltheValue.Size = new System.Drawing.Size(47, 13);
            this.lbltheValue.TabIndex = 8;
            this.lbltheValue.Text = "Peg Value";
            // 
            // lblPegID
            // 
            this.SetBoundPropertyName(this.lblPegID, "");
            this.lblPegID.Location = new System.Drawing.Point(19, 80);
            this.lblPegID.Name = "lblPegID";
            this.lblPegID.Size = new System.Drawing.Size(32, 13);
            this.lblPegID.TabIndex = 9;
            this.lblPegID.Text = "Peg ID";
            // 
            // lblDescription
            // 
            this.SetBoundPropertyName(this.lblDescription, "");
            this.lblDescription.Location = new System.Drawing.Point(19, 54);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(53, 13);
            this.lblDescription.TabIndex = 10;
            this.lblDescription.Text = "Description";
            // 
            // lblLetter2
            // 
            this.SetBoundPropertyName(this.lblLetter2, "");
            this.lblLetter2.Location = new System.Drawing.Point(19, 132);
            this.lblLetter2.Name = "lblLetter2";
            this.lblLetter2.Size = new System.Drawing.Size(38, 13);
            this.lblLetter2.TabIndex = 11;
            this.lblLetter2.Text = "Letter 2";
            // 
            // lblLetter3
            // 
            this.SetBoundPropertyName(this.lblLetter3, "");
            this.lblLetter3.Location = new System.Drawing.Point(19, 158);
            this.lblLetter3.Name = "lblLetter3";
            this.lblLetter3.Size = new System.Drawing.Size(38, 13);
            this.lblLetter3.TabIndex = 13;
            this.lblLetter3.Text = "Letter 3";
            // 
            // txtLetter3
            // 
            this.SetBoundFieldName(this.txtLetter3, "Letter3");
            this.SetBoundPropertyName(this.txtLetter3, "EditValue");
            this.txtLetter3.Location = new System.Drawing.Point(107, 155);
            this.txtLetter3.Name = "txtLetter3";
            this.txtLetter3.Properties.MaxLength = 50;
            this.txtLetter3.Size = new System.Drawing.Size(285, 20);
            this.txtLetter3.TabIndex = 6;
            // 
            // gcWPPegs
            // 
            this.SetBoundPropertyName(this.gcWPPegs, "");
            this.gcWPPegs.Location = new System.Drawing.Point(472, 26);
            this.gcWPPegs.MainView = this.gvWPPegs;
            this.gcWPPegs.Name = "gcWPPegs";
            this.gcWPPegs.Size = new System.Drawing.Size(325, 149);
            this.gcWPPegs.TabIndex = 7;
            this.gcWPPegs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWPPegs});
            // 
            // gvWPPegs
            // 
            this.gvWPPegs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_WPPegID,
            this.col_WPtheValue,
            this.col_WPID});
            this.gvWPPegs.GridControl = this.gcWPPegs;
            this.gvWPPegs.Name = "gvWPPegs";
            this.gvWPPegs.OptionsBehavior.ReadOnly = true;
            this.gvWPPegs.OptionsView.ShowGroupPanel = false;
            this.gvWPPegs.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvWPPegs_RowClick);
            // 
            // col_WPPegID
            // 
            this.col_WPPegID.AppearanceCell.Options.UseTextOptions = true;
            this.col_WPPegID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_WPPegID.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_WPPegID.AppearanceHeader.Options.UseFont = true;
            this.col_WPPegID.AppearanceHeader.Options.UseTextOptions = true;
            this.col_WPPegID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_WPPegID.Caption = "Peg ID";
            this.col_WPPegID.FieldName = "WPPegID";
            this.col_WPPegID.Name = "col_WPPegID";
            this.col_WPPegID.OptionsColumn.AllowEdit = false;
            this.col_WPPegID.OptionsColumn.AllowFocus = false;
            this.col_WPPegID.OptionsColumn.AllowMove = false;
            this.col_WPPegID.OptionsColumn.AllowSize = false;
            this.col_WPPegID.OptionsColumn.FixedWidth = true;
            this.col_WPPegID.OptionsColumn.TabStop = false;
            this.col_WPPegID.Visible = true;
            this.col_WPPegID.VisibleIndex = 0;
            this.col_WPPegID.Width = 100;
            // 
            // col_WPtheValue
            // 
            this.col_WPtheValue.AppearanceCell.Options.UseTextOptions = true;
            this.col_WPtheValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_WPtheValue.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.col_WPtheValue.AppearanceHeader.Options.UseFont = true;
            this.col_WPtheValue.AppearanceHeader.Options.UseTextOptions = true;
            this.col_WPtheValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col_WPtheValue.Caption = "Value";
            this.col_WPtheValue.FieldName = "WPtheValue";
            this.col_WPtheValue.Name = "col_WPtheValue";
            this.col_WPtheValue.OptionsColumn.AllowEdit = false;
            this.col_WPtheValue.OptionsColumn.AllowFocus = false;
            this.col_WPtheValue.OptionsColumn.AllowMove = false;
            this.col_WPtheValue.OptionsColumn.AllowSize = false;
            this.col_WPtheValue.OptionsColumn.FixedWidth = true;
            this.col_WPtheValue.OptionsColumn.TabStop = false;
            this.col_WPtheValue.Visible = true;
            this.col_WPtheValue.VisibleIndex = 1;
            this.col_WPtheValue.Width = 100;
            // 
            // col_WPID
            // 
            this.col_WPID.Caption = "WP ID";
            this.col_WPID.FieldName = "WPID";
            this.col_WPID.Name = "col_WPID";
            this.col_WPID.OptionsColumn.AllowMove = false;
            this.col_WPID.OptionsColumn.AllowSize = false;
            this.col_WPID.OptionsColumn.FixedWidth = true;
            this.col_WPID.Width = 107;
            // 
            // btnDelete
            // 
            this.SetBoundPropertyName(this.btnDelete, "");
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.Location = new System.Drawing.Point(603, 191);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(111, 44);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dxPegsErrorSetup
            // 
            this.dxPegsErrorSetup.ContainerControl = this;
            // 
            // ilePegs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.gcWPPegs);
            this.Controls.Add(this.lblLetter3);
            this.Controls.Add(this.txtLetter3);
            this.Controls.Add(this.lblLetter2);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblPegID);
            this.Controls.Add(this.lbltheValue);
            this.Controls.Add(this.lblLetter1);
            this.Controls.Add(this.lblWorkplaceID);
            this.Controls.Add(this.txtLetter1);
            this.Controls.Add(this.txttheValue);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtPegID);
            this.Controls.Add(this.txtLetter2);
            this.Controls.Add(this.txtWorkplaceID);
            this.Name = "ilePegs";
            this.Size = new System.Drawing.Size(862, 251);
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkplaceID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetter2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPegID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttheValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetter1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetter3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcWPPegs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWPPegs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxPegsErrorSetup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txtWorkplaceID;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblPegID;
        private DevExpress.XtraEditors.LabelControl lbltheValue;
        private DevExpress.XtraEditors.LabelControl lblLetter1;
        private DevExpress.XtraEditors.LabelControl lblWorkplaceID;
        public DevExpress.XtraEditors.TextEdit txtLetter1;
        public DevExpress.XtraEditors.TextEdit txttheValue;
        public DevExpress.XtraEditors.TextEdit txtDescription;
        public DevExpress.XtraEditors.TextEdit txtPegID;
        public DevExpress.XtraEditors.TextEdit txtLetter2;
        private DevExpress.XtraEditors.LabelControl lblLetter3;
        public DevExpress.XtraEditors.TextEdit txtLetter3;
        private DevExpress.XtraEditors.LabelControl lblLetter2;
        public DevExpress.XtraGrid.GridControl gcWPPegs;
        public DevExpress.XtraGrid.Views.Grid.GridView gvWPPegs;
        public DevExpress.XtraGrid.Columns.GridColumn col_WPPegID;
        public DevExpress.XtraGrid.Columns.GridColumn col_WPtheValue;
        private DevExpress.XtraGrid.Columns.GridColumn col_WPID;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        public DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxPegsErrorSetup;
    }
}
