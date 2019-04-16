namespace Mineware.Systems.Planning.PrePlanning.RevisedPlanning_Security
{
    partial class frmUpdateRevicedRecords
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
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnMove = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(45, 74);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(295, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add th User to the Revised Planning";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(351, 74);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(304, 23);
            this.btnMove.TabIndex = 1;
            this.btnMove.Text = "Move the Revised Planning to the User";
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(87, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(537, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "There Are Unapproved Revised Planning For this Section. What do you want to do?";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // frmUpdateRevicedRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 109);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.btnAdd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateRevicedRecords";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmUpdateRevicedRecords_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnMove;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
