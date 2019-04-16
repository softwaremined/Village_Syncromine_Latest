namespace Mineware.Systems.Production.SysAdminScreens.OCRScheduling
{
    partial class AddWorkplaceOCR
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWorkplaceOCR));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.gcWorkPlaces = new DevExpress.XtraGrid.GridControl();
            this.viewWorkplaces = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolChecked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.gcolWPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolSupervisor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SecLookUp = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ceSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkPlaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewWorkplaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecLookUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 373);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(590, 33);
            this.panelControl2.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(508, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.ImageOptions.Image")));
            this.btnOK.Location = new System.Drawing.Point(427, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gcWorkPlaces
            // 
            this.gcWorkPlaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcWorkPlaces.Location = new System.Drawing.Point(0, 0);
            this.gcWorkPlaces.MainView = this.viewWorkplaces;
            this.gcWorkPlaces.Name = "gcWorkPlaces";
            this.gcWorkPlaces.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ceSelected,
            this.repositoryItemImageComboBox1,
            this.SecLookUp});
            this.gcWorkPlaces.Size = new System.Drawing.Size(590, 373);
            this.gcWorkPlaces.TabIndex = 4;
            this.gcWorkPlaces.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewWorkplaces});
            // 
            // viewWorkplaces
            // 
            this.viewWorkplaces.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolChecked,
            this.gcolWPID,
            this.gcolDESCRIPTION,
            this.gcolSupervisor});
            this.viewWorkplaces.GridControl = this.gcWorkPlaces;
            this.viewWorkplaces.Name = "viewWorkplaces";
            this.viewWorkplaces.OptionsView.ColumnAutoWidth = false;
            this.viewWorkplaces.OptionsView.ShowAutoFilterRow = true;
            this.viewWorkplaces.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewWorkplaces.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewWorkplaces.OptionsView.ShowGroupPanel = false;
            // 
            // gcolChecked
            // 
            this.gcolChecked.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gcolChecked.FieldName = "Selected";
            this.gcolChecked.Name = "gcolChecked";
            this.gcolChecked.OptionsColumn.AllowEdit = false;
            this.gcolChecked.OptionsColumn.AllowSize = false;
            this.gcolChecked.OptionsColumn.FixedWidth = true;
            this.gcolChecked.Width = 25;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "Y", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "N", 1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imageSmall;
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageSmall.ImageStream")));
            this.imageSmall.InsertGalleryImage("apply_16x16.png", "images/actions/apply_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/apply_16x16.png"), 0);
            this.imageSmall.Images.SetKeyName(0, "apply_16x16.png");
            this.imageSmall.InsertGalleryImage("cancel_16x16.png", "images/actions/cancel_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/cancel_16x16.png"), 1);
            this.imageSmall.Images.SetKeyName(1, "cancel_16x16.png");
            // 
            // gcolWPID
            // 
            this.gcolWPID.Caption = "Workplace ID";
            this.gcolWPID.FieldName = "Workplaceid";
            this.gcolWPID.Name = "gcolWPID";
            this.gcolWPID.OptionsColumn.AllowEdit = false;
            this.gcolWPID.Visible = true;
            this.gcolWPID.VisibleIndex = 0;
            this.gcolWPID.Width = 100;
            // 
            // gcolDESCRIPTION
            // 
            this.gcolDESCRIPTION.Caption = "Description";
            this.gcolDESCRIPTION.FieldName = "DESCRIPTION";
            this.gcolDESCRIPTION.Name = "gcolDESCRIPTION";
            this.gcolDESCRIPTION.OptionsColumn.AllowEdit = false;
            this.gcolDESCRIPTION.Visible = true;
            this.gcolDESCRIPTION.VisibleIndex = 1;
            this.gcolDESCRIPTION.Width = 198;
            // 
            // gcolSupervisor
            // 
            this.gcolSupervisor.Caption = "Supervisor";
            this.gcolSupervisor.ColumnEdit = this.SecLookUp;
            this.gcolSupervisor.FieldName = "SectionID";
            this.gcolSupervisor.Name = "gcolSupervisor";
            this.gcolSupervisor.Visible = true;
            this.gcolSupervisor.VisibleIndex = 2;
            this.gcolSupervisor.Width = 201;
            // 
            // SecLookUp
            // 
            this.SecLookUp.AutoHeight = false;
            this.SecLookUp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SecLookUp.Name = "SecLookUp";
            this.SecLookUp.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // ceSelected
            // 
            this.ceSelected.AutoHeight = false;
            this.ceSelected.Caption = "Check";
            this.ceSelected.Name = "ceSelected";
            // 
            // AddWorkplaceOCR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 406);
            this.Controls.Add(this.gcWorkPlaces);
            this.Controls.Add(this.panelControl2);
            this.Name = "AddWorkplaceOCR";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Workplace OCR";
            this.Load += new System.EventHandler(this.AddWorkplaceOCR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkPlaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewWorkplaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecLookUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceSelected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.GridControl gcWorkPlaces;
        private DevExpress.XtraGrid.Views.Grid.GridView viewWorkplaces;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ceSelected;
        private DevExpress.XtraGrid.Columns.GridColumn gcolWPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDESCRIPTION;
        private DevExpress.XtraGrid.Columns.GridColumn gcolSupervisor;
        private DevExpress.XtraGrid.Columns.GridColumn gcolChecked;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.Utils.ImageCollection imageSmall;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit SecLookUp;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
    }
}