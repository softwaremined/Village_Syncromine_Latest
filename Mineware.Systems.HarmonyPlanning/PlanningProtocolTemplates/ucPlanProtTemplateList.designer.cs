namespace Mineware.Systems.Planning.PlanningProtocolTemplates
{
    partial class ucPlanProtTemplateList
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
            this.gridTemplate = new DevExpress.XtraGrid.GridControl();
            this.viewTemlates = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcTemplateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTemplateType = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTemlates)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTemplate
            // 
            this.gridTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTemplate.Location = new System.Drawing.Point(0, 0);
            this.gridTemplate.MainView = this.viewTemlates;
            this.gridTemplate.Name = "gridTemplate";
            this.gridTemplate.Size = new System.Drawing.Size(910, 533);
            this.gridTemplate.TabIndex = 1;
            this.gridTemplate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTemlates});
            // 
            // viewTemlates
            // 
            this.viewTemlates.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcTemplateName,
            this.gcTemplateType});
            this.viewTemlates.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.viewTemlates.GridControl = this.gridTemplate;
            this.viewTemlates.Name = "viewTemlates";
            this.viewTemlates.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewTemlates.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewTemlates.OptionsBehavior.AutoSelectAllInEditor = false;
            this.viewTemlates.OptionsBehavior.Editable = false;
            this.viewTemlates.OptionsBehavior.ReadOnly = true;
            this.viewTemlates.OptionsCustomization.AllowColumnMoving = false;
            this.viewTemlates.OptionsCustomization.AllowColumnResizing = false;
            this.viewTemlates.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.viewTemlates.OptionsView.ShowGroupPanel = false;
            // 
            // gcTemplateName
            // 
            this.gcTemplateName.Caption = "Template Name";
            this.gcTemplateName.FieldName = "TemplateName";
            this.gcTemplateName.Name = "gcTemplateName";
            this.gcTemplateName.Visible = true;
            this.gcTemplateName.VisibleIndex = 0;
            // 
            // gcTemplateType
            // 
            this.gcTemplateType.Caption = "Template Type";
            this.gcTemplateType.FieldName = "Activity";
            this.gcTemplateType.Name = "gcTemplateType";
            this.gcTemplateType.Visible = true;
            this.gcTemplateType.VisibleIndex = 1;
            // 
            // ucPlanProtTemplateList
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridTemplate);
            this.Name = "ucPlanProtTemplateList";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(910, 533);
            this.Load += new System.EventHandler(this.ucPlanProtTemplateList_Load);
            this.Controls.SetChildIndex(this.gridTemplate, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTemlates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridTemplate;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTemlates;
        private DevExpress.XtraGrid.Columns.GridColumn gcTemplateName;
        private DevExpress.XtraGrid.Columns.GridColumn gcTemplateType;
    }
}
