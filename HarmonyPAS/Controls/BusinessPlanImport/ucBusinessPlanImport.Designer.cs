namespace Mineware.Systems.Production.Controls.BusinessPlanImport
{
    partial class ucBusinessPlanImport
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
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barFileName = new DevExpress.XtraBars.BarEditItem();
            this.rpFileName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnBrowse = new DevExpress.XtraBars.BarButtonItem();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportFile = new DevExpress.XtraBars.BarButtonItem();
            this.barActivity = new DevExpress.XtraBars.BarEditItem();
            this.rpActivity = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.barFileNameError = new DevExpress.XtraBars.BarEditItem();
            this.rpFileNameError = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rpageImport = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgImport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpgButtons = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpFileName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpFileNameError)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AllowKeyTips = false;
            this.ribbonControl1.AllowMdiChildButtons = false;
            this.ribbonControl1.AllowMinimizeRibbon = false;
            this.ribbonControl1.AllowTrimPageText = false;
            this.ribbonControl1.AutoSizeItems = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barFileName,
            this.btnBrowse,
            this.btnClose,
            this.btnImportFile,
            this.barActivity,
            this.barFileNameError});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 14;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpageImport});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rpFileName,
            this.rpActivity,
            this.rpFileNameError});
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(905, 95);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barFileName
            // 
            this.barFileName.CanOpenEdit = false;
            this.barFileName.Caption = "Import File";
            this.barFileName.Edit = this.rpFileName;
            this.barFileName.EditWidth = 500;
            this.barFileName.Id = 2;
            this.barFileName.Name = "barFileName";
            // 
            // rpFileName
            // 
            this.rpFileName.AutoHeight = false;
            this.rpFileName.Name = "rpFileName";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Caption = "Browse";
            this.btnBrowse.Id = 3;
            this.btnBrowse.ImageOptions.ImageUri.Uri = "Open";
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBrowse_ItemClick);
            // 
            // btnClose
            // 
            this.btnClose.Caption = "Close";
            this.btnClose.Id = 4;
            this.btnClose.ImageOptions.ImageUri.Uri = "Close";
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // btnImportFile
            // 
            this.btnImportFile.Caption = "Import";
            this.btnImportFile.Id = 5;
            this.btnImportFile.ImageOptions.ImageUri.Uri = "IndentIncrease";
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barImportFile_ItemClick);
            // 
            // barActivity
            // 
            this.barActivity.Edit = this.rpActivity;
            this.barActivity.EditWidth = 200;
            this.barActivity.Id = 8;
            this.barActivity.Name = "barActivity";
            // 
            // rpActivity
            // 
            this.rpActivity.Columns = 2;
            this.rpActivity.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Stoping", "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Development", "Development")});
            this.rpActivity.Name = "rpActivity";
            // 
            // barFileNameError
            // 
            this.barFileNameError.Caption = "Error File Name";
            this.barFileNameError.Edit = this.rpFileNameError;
            this.barFileNameError.EditWidth = 500;
            this.barFileNameError.Id = 13;
            this.barFileNameError.Name = "barFileNameError";
            this.barFileNameError.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // rpFileNameError
            // 
            this.rpFileNameError.AutoHeight = false;
            this.rpFileNameError.Name = "rpFileNameError";
            // 
            // rpageImport
            // 
            this.rpageImport.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgImport,
            this.rpgButtons});
            this.rpageImport.Name = "rpageImport";
            // 
            // rpgImport
            // 
            this.rpgImport.ItemLinks.Add(this.barActivity);
            this.rpgImport.ItemLinks.Add(this.barFileName);
            this.rpgImport.ItemLinks.Add(this.barFileNameError);
            this.rpgImport.Name = "rpgImport";
            // 
            // rpgButtons
            // 
            this.rpgButtons.ItemLinks.Add(this.btnBrowse);
            this.rpgButtons.ItemLinks.Add(this.btnImportFile);
            this.rpgButtons.ItemLinks.Add(this.btnClose);
            this.rpgButtons.Name = "rpgButtons";
            // 
            // ucBusinessPlanImport
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ribbonControl1);
            this.Name = "ucBusinessPlanImport";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(905, 157);
            this.Load += new System.EventHandler(this.ucBusinessPlanImport_Load);
            this.Controls.SetChildIndex(this.ribbonControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpFileName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpFileNameError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpageImport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgImport;
        //private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rpImport;
        private DevExpress.XtraBars.BarEditItem barFileName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpFileName;
        private DevExpress.XtraBars.BarButtonItem btnBrowse;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgButtons;
        private DevExpress.XtraBars.BarButtonItem btnImportFile;
        private DevExpress.XtraBars.BarEditItem barActivity;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rpActivity;
        private DevExpress.XtraBars.BarEditItem barFileNameError;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpFileNameError;
        //private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpFileNameError;
        //private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rpErrorFileName;
        //private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rpActivity;
        //private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
    }
}
