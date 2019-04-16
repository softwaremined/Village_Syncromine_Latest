namespace Mineware.Systems.Production.SysAdminScreens.EndTypes
{
    partial class ucEndTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucEndTypes));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.btnEdit = new Mineware.Systems.Global.CustomControls.MWButton(this.components);
            this.gcEndTypes = new DevExpress.XtraGrid.GridControl();
            this.gvEndTypes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colETNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETHeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETWidth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETRW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETRates = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETDetRatio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colETProcessCodes = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEndTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEndTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcEndTypes);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(836, 340);
            this.splitContainerControl1.SplitterPosition = 42;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(836, 42);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageLeftPadding = 0;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(72, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 40);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Cancel;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.ImageLeftPadding = 0;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.Location = new System.Drawing.Point(0, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 40);
            this.btnEdit.TabIndex = 21;
            this.btnEdit.Text = "Edit";
            this.btnEdit.theButtonTye = Mineware.Systems.Global.CustomControls.ButtonType.Edit;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // gcEndTypes
            // 
            this.gcEndTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcEndTypes.Location = new System.Drawing.Point(0, 0);
            this.gcEndTypes.MainView = this.gvEndTypes;
            this.gcEndTypes.Name = "gcEndTypes";
            this.gcEndTypes.Size = new System.Drawing.Size(836, 292);
            this.gcEndTypes.TabIndex = 0;
            this.gcEndTypes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEndTypes});
            this.gcEndTypes.Load += new System.EventHandler(this.gcEndTypes_Load);
            // 
            // gvEndTypes
            // 
            this.gvEndTypes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colETNo,
            this.colETName,
            this.colETHeight,
            this.colETWidth,
            this.colETRW,
            this.colETRates,
            this.colETDetRatio,
            this.colETProcessCodes});
            this.gvEndTypes.GridControl = this.gcEndTypes;
            this.gvEndTypes.Name = "gvEndTypes";
            this.gvEndTypes.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvEndTypes.OptionsView.ShowGroupPanel = false;
            this.gvEndTypes.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvEndTypes_RowUpdated);
            // 
            // colETNo
            // 
            this.colETNo.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colETNo.AppearanceHeader.Options.UseFont = true;
            this.colETNo.Caption = "Endtype No";
            this.colETNo.FieldName = "EndTypeID";
            this.colETNo.Name = "colETNo";
            this.colETNo.Visible = true;
            this.colETNo.VisibleIndex = 0;
            // 
            // colETName
            // 
            this.colETName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colETName.AppearanceHeader.Options.UseFont = true;
            this.colETName.Caption = "Endtype Name";
            this.colETName.FieldName = "Description";
            this.colETName.Name = "colETName";
            this.colETName.Visible = true;
            this.colETName.VisibleIndex = 1;
            // 
            // colETHeight
            // 
            this.colETHeight.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colETHeight.AppearanceHeader.Options.UseFont = true;
            this.colETHeight.Caption = "Height";
            this.colETHeight.FieldName = "EndHeight";
            this.colETHeight.Name = "colETHeight";
            this.colETHeight.Visible = true;
            this.colETHeight.VisibleIndex = 2;
            // 
            // colETWidth
            // 
            this.colETWidth.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colETWidth.AppearanceHeader.Options.UseFont = true;
            this.colETWidth.Caption = "Width";
            this.colETWidth.FieldName = "EndWidth";
            this.colETWidth.Name = "colETWidth";
            this.colETWidth.Visible = true;
            this.colETWidth.VisibleIndex = 3;
            // 
            // colETRW
            // 
            this.colETRW.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colETRW.AppearanceHeader.Options.UseFont = true;
            this.colETRW.Caption = "R/W";
            this.colETRW.FieldName = "ReefWaste";
            this.colETRW.Name = "colETRW";
            this.colETRW.Visible = true;
            this.colETRW.VisibleIndex = 4;
            // 
            // colETRates
            // 
            this.colETRates.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colETRates.AppearanceHeader.Options.UseFont = true;
            this.colETRates.Caption = "Rates";
            this.colETRates.FieldName = "Rate";
            this.colETRates.Name = "colETRates";
            this.colETRates.Visible = true;
            this.colETRates.VisibleIndex = 5;
            // 
            // colETDetRatio
            // 
            this.colETDetRatio.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colETDetRatio.AppearanceHeader.Options.UseFont = true;
            this.colETDetRatio.Caption = "Det Ratio";
            this.colETDetRatio.FieldName = "DetRatio";
            this.colETDetRatio.Name = "colETDetRatio";
            this.colETDetRatio.Visible = true;
            this.colETDetRatio.VisibleIndex = 6;
            // 
            // colETProcessCodes
            // 
            this.colETProcessCodes.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colETProcessCodes.AppearanceHeader.Options.UseFont = true;
            this.colETProcessCodes.Caption = "Process Codes";
            this.colETProcessCodes.FieldName = "ProcessCode";
            this.colETProcessCodes.Name = "colETProcessCodes";
            this.colETProcessCodes.Visible = true;
            this.colETProcessCodes.VisibleIndex = 7;
            // 
            // ucEndTypes
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ucEndTypes";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(836, 340);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcEndTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEndTypes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcEndTypes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvEndTypes;
        private DevExpress.XtraGrid.Columns.GridColumn colETNo;
        private DevExpress.XtraGrid.Columns.GridColumn colETName;
        private DevExpress.XtraGrid.Columns.GridColumn colETHeight;
        private DevExpress.XtraGrid.Columns.GridColumn colETWidth;
        private DevExpress.XtraGrid.Columns.GridColumn colETRW;
        private DevExpress.XtraGrid.Columns.GridColumn colETRates;
        private DevExpress.XtraGrid.Columns.GridColumn colETDetRatio;
        private DevExpress.XtraGrid.Columns.GridColumn colETProcessCodes;
        private Global.CustomControls.MWButton btnEdit;
        private Global.CustomControls.MWButton btnCancel;
    }
}
