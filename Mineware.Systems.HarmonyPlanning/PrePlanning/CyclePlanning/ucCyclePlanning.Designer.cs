namespace Mineware.Systems.Production.Planning.PrePlanning.CyclePlanning
{
    partial class ucCyclePlanning
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
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Location = new System.Drawing.Point(18, 28);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.pivotGridControl1.Size = new System.Drawing.Size(580, 423);
            this.pivotGridControl1.TabIndex = 4;
            // 
            // ucCyclePlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pivotGridControl1);
            this.Name = "ucCyclePlanning";
            this.ShowIInfo = false;
            this.Controls.SetChildIndex(this.pivotGridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;

    }
}
