namespace Mineware.Systems.Production.SysAdminScreens.TopPanels
{
    partial class ucTopPanels
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.lueProdMonth = new DevExpress.XtraBars.BarEditItem();
            this.mwRepositoryItemProdMonth1 = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.btnShow = new DevExpress.XtraBars.BarButtonItem();
            this.lueSectionID = new DevExpress.XtraBars.BarEditItem();
            this.rpSectionID = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.btnBack = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgSelection = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgShow = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgSave = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpMill = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.rpTonsTreated = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rpTonsToPlant = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcTopPanels = new DevExpress.XtraGrid.GridControl();
            this.gvTopPanels = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_WPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Workplace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_CMGT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_KG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Selected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.col_Activity = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwRepositoryItemProdMonth1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpTonsTreated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpTonsToPlant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTopPanels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTopPanels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.ribbonControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(846, 97);
            this.panelControl2.TabIndex = 3;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.lueProdMonth,
            this.btnShow,
            this.lueSectionID,
            this.btnBack});
            this.ribbonControl1.Location = new System.Drawing.Point(2, 2);
            this.ribbonControl1.MaxItemId = 12;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpSectionID,
            this.rpMill,
            this.rpTonsTreated,
            this.rpTonsToPlant,
            this.repositoryItemLookUpEdit2,
            this.mwRepositoryItemProdMonth1,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(842, 95);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // lueProdMonth
            // 
            this.lueProdMonth.Caption = "Prod Month";
            this.lueProdMonth.Edit = this.mwRepositoryItemProdMonth1;
            this.lueProdMonth.EditWidth = 150;
            this.lueProdMonth.Id = 1;
            this.lueProdMonth.Name = "lueProdMonth";
            // 
            // mwRepositoryItemProdMonth1
            // 
            this.mwRepositoryItemProdMonth1.AutoHeight = false;
            this.mwRepositoryItemProdMonth1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2)});
            this.mwRepositoryItemProdMonth1.Mask.EditMask = "yyyyMM";
            this.mwRepositoryItemProdMonth1.Mask.IgnoreMaskBlank = false;
            this.mwRepositoryItemProdMonth1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.mwRepositoryItemProdMonth1.Mask.UseMaskAsDisplayFormat = true;
            this.mwRepositoryItemProdMonth1.Name = "mwRepositoryItemProdMonth1";
            // 
            // btnShow
            // 
            this.btnShow.Caption = "Show";
            this.btnShow.Id = 5;
            this.btnShow.ImageOptions.ImageUri.Uri = "Zoom";
            this.btnShow.Name = "btnShow";
            this.btnShow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShow_ItemClick);
            // 
            // lueSectionID
            // 
            this.lueSectionID.Caption = "Section";
            this.lueSectionID.Edit = this.rpSectionID;
            this.lueSectionID.EditWidth = 170;
            this.lueSectionID.Id = 8;
            this.lueSectionID.Name = "lueSectionID";
            // 
            // rpSectionID
            // 
            this.rpSectionID.AutoHeight = false;
            this.rpSectionID.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "1", -1, true, true, false, editorButtonImageOptions3)});
            this.rpSectionID.Name = "rpSectionID";
            this.rpSectionID.NullText = "Select Section ID";
            // 
            // btnBack
            // 
            this.btnBack.Caption = "Back";
            this.btnBack.Id = 9;
            this.btnBack.ImageOptions.ImageUri.Uri = "Backward";
            this.btnBack.Name = "btnBack";
            this.btnBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBack_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgSelection,
            this.rpgShow,
            this.rpgSave});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // rpgSelection
            // 
            this.rpgSelection.ItemLinks.Add(this.lueProdMonth);
            this.rpgSelection.ItemLinks.Add(this.lueSectionID);
            this.rpgSelection.Name = "rpgSelection";
            // 
            // rpgShow
            // 
            this.rpgShow.ItemLinks.Add(this.btnShow);
            this.rpgShow.Name = "rpgShow";
            // 
            // rpgSave
            // 
            this.rpgSave.ItemLinks.Add(this.btnBack);
            this.rpgSave.Name = "rpgSave";
            // 
            // rpMill
            // 
            this.rpMill.AutoHeight = false;
            this.rpMill.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rpMill.Name = "rpMill";
            // 
            // rpTonsTreated
            // 
            this.rpTonsTreated.AutoHeight = false;
            this.rpTonsTreated.Name = "rpTonsTreated";
            // 
            // rpTonsToPlant
            // 
            this.rpTonsToPlant.AutoHeight = false;
            this.rpTonsToPlant.Name = "rpTonsToPlant";
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcTopPanels);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 97);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(846, 365);
            this.panelControl1.TabIndex = 4;
            // 
            // gcTopPanels
            // 
            this.gcTopPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTopPanels.Location = new System.Drawing.Point(2, 2);
            this.gcTopPanels.MainView = this.gvTopPanels;
            this.gcTopPanels.MenuManager = this.ribbonControl1;
            this.gcTopPanels.Name = "gcTopPanels";
            this.gcTopPanels.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkSelected});
            this.gcTopPanels.Size = new System.Drawing.Size(842, 361);
            this.gcTopPanels.TabIndex = 0;
            this.gcTopPanels.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTopPanels});
            // 
            // gvTopPanels
            // 
            this.gvTopPanels.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_WPID,
            this.col_Workplace,
            this.col_CMGT,
            this.col_KG,
            this.col_Selected,
            this.col_Activity});
            this.gvTopPanels.GridControl = this.gcTopPanels;
            this.gvTopPanels.Name = "gvTopPanels";
            this.gvTopPanels.OptionsView.ShowGroupPanel = false;
            this.gvTopPanels.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvTopPanels_RowUpdated);
            // 
            // col_WPID
            // 
            this.col_WPID.Caption = "WPID";
            this.col_WPID.FieldName = "WPID";
            this.col_WPID.Name = "col_WPID";
            this.col_WPID.OptionsColumn.ReadOnly = true;
            this.col_WPID.Visible = true;
            this.col_WPID.VisibleIndex = 0;
            // 
            // col_Workplace
            // 
            this.col_Workplace.Caption = "Workplace";
            this.col_Workplace.FieldName = "Workplace";
            this.col_Workplace.Name = "col_Workplace";
            this.col_Workplace.OptionsColumn.ReadOnly = true;
            this.col_Workplace.Visible = true;
            this.col_Workplace.VisibleIndex = 1;
            // 
            // col_CMGT
            // 
            this.col_CMGT.Caption = "Cmgt";
            this.col_CMGT.FieldName = "CMGT";
            this.col_CMGT.Name = "col_CMGT";
            this.col_CMGT.OptionsColumn.ReadOnly = true;
            this.col_CMGT.Visible = true;
            this.col_CMGT.VisibleIndex = 2;
            // 
            // col_KG
            // 
            this.col_KG.Caption = "Kg";
            this.col_KG.FieldName = "KG";
            this.col_KG.Name = "col_KG";
            this.col_KG.OptionsColumn.ReadOnly = true;
            this.col_KG.Visible = true;
            this.col_KG.VisibleIndex = 3;
            // 
            // col_Selected
            // 
            this.col_Selected.Caption = "Selected";
            this.col_Selected.ColumnEdit = this.chkSelected;
            this.col_Selected.FieldName = "Selected";
            this.col_Selected.Name = "col_Selected";
            this.col_Selected.Visible = true;
            this.col_Selected.VisibleIndex = 4;
            // 
            // chkSelected
            // 
            this.chkSelected.AutoHeight = false;
            this.chkSelected.Name = "chkSelected";
            this.chkSelected.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkSelected.Leave += new System.EventHandler(this.chkSelected_Leave);
            // 
            // col_Activity
            // 
            this.col_Activity.Caption = "Activity";
            this.col_Activity.FieldName = "Activity";
            this.col_Activity.Name = "col_Activity";
            // 
            // ucTopPanels
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "ucTopPanels";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(846, 462);
            this.Load += new System.EventHandler(this.ucMillingBooking_Load);
            this.Controls.SetChildIndex(this.panelControl2, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwRepositoryItemProdMonth1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpSectionID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpTonsTreated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpTonsToPlant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTopPanels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTopPanels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelected)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarEditItem lueProdMonth;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rpSectionID;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rpMill;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpTonsTreated;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpTonsToPlant;
        private DevExpress.XtraBars.BarButtonItem btnShow;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgSelection;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgShow;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgSave;
        private DevExpress.XtraBars.BarEditItem lueSectionID;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private Global.CustomControls.MWRepositoryItemProdMonth mwRepositoryItemProdMonth1;
        private DevExpress.XtraBars.BarButtonItem btnBack;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraGrid.GridControl gcTopPanels;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTopPanels;
        private DevExpress.XtraGrid.Columns.GridColumn col_WPID;
        private DevExpress.XtraGrid.Columns.GridColumn col_Workplace;
        private DevExpress.XtraGrid.Columns.GridColumn col_CMGT;
        private DevExpress.XtraGrid.Columns.GridColumn col_KG;
        private DevExpress.XtraGrid.Columns.GridColumn col_Selected;
        private DevExpress.XtraGrid.Columns.GridColumn col_Activity;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkSelected;
    }
}
