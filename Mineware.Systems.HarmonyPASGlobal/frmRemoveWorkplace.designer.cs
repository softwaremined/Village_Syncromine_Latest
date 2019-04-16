using System.Drawing;

namespace Mineware.Systems.ProductionGlobal
{
    partial class frmRemoveWorkplace
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
            this.gcWorkPlaces = new DevExpress.XtraGrid.GridControl();
            this.viewWorkplaces = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolWPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkPlaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewWorkplaces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcWorkPlaces
            // 
            this.gcWorkPlaces.Location = new System.Drawing.Point(0, 0);
            this.gcWorkPlaces.MainView = this.viewWorkplaces;
            this.gcWorkPlaces.Name = "gcWorkPlaces";
            this.gcWorkPlaces.Size = new System.Drawing.Size(400, 200);
            this.gcWorkPlaces.TabIndex = 0;
            this.gcWorkPlaces.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewWorkplaces});
            // 
            // viewWorkplaces
            // 
            this.viewWorkplaces.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolWPID,
            this.gcolDESCRIPTION});
            this.viewWorkplaces.GridControl = this.gcWorkPlaces;
            this.viewWorkplaces.Name = "viewWorkplaces";
            this.viewWorkplaces.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            this.viewWorkplaces.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.viewWorkplaces.OptionsView.ShowGroupPanel = false;
            this.viewWorkplaces.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.viewWorkplaces_RowCellClick);
            this.viewWorkplaces.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.viewWorkplaces_CellValueChanging);
            this.viewWorkplaces.ColumnFilterChanged += new System.EventHandler(this.viewWorkplaces_ColumnFilterChanged);
            this.viewWorkplaces.RowCountChanged += new System.EventHandler(this.viewWorkplaces_RowCountChanged);
            // 
            // gcolWPID
            // 
            this.gcolWPID.Caption = "Workplace ID";
            this.gcolWPID.FieldName = "Workplaceid";
            this.gcolWPID.Name = "gcolWPID";
            this.gcolWPID.OptionsColumn.AllowEdit = false;
            this.gcolWPID.Visible = true;
            this.gcolWPID.VisibleIndex = 0;
            // 
            // gcolDESCRIPTION
            // 
            this.gcolDESCRIPTION.Caption = "Description";
            this.gcolDESCRIPTION.FieldName = "DESCRIPTION";
            this.gcolDESCRIPTION.Name = "gcolDESCRIPTION";
            this.gcolDESCRIPTION.OptionsColumn.AllowEdit = false;
            this.gcolDESCRIPTION.OptionsEditForm.VisibleIndex = 1;
            this.gcolDESCRIPTION.Visible = true;
            this.gcolDESCRIPTION.VisibleIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(200, 100);
            this.panelControl1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(0, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmRemoveWorkplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gcWorkPlaces);
            this.Name = "frmRemoveWorkplace";
            this.Text = "frmRemoveWorkplace";
            ((System.ComponentModel.ISupportInitialize)(this.gcWorkPlaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewWorkplaces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcWorkPlaces;
        private DevExpress.XtraGrid.Views.Grid.GridView viewWorkplaces;
        private DevExpress.XtraGrid.Columns.GridColumn gcolWPID;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDESCRIPTION;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}