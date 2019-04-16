namespace Mineware.Systems.Production.SysAdminScreens.TrammingProblems
{
    partial class ileTrammingProblems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ileTrammingProblems));
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txtProblemCode = new DevExpress.XtraEditors.TextEdit();
            this.lblProblem = new DevExpress.XtraEditors.LabelControl();
            this.dxProblems = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProblemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxProblems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.SetBoundPropertyName(this.btnUpdate, "");
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(105, 62);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 24);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Save";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtProblemCode
            // 
            this.SetBoundPropertyName(this.txtProblemCode, "");
            this.txtProblemCode.Location = new System.Drawing.Point(105, 26);
            this.txtProblemCode.Name = "txtProblemCode";
            this.txtProblemCode.Size = new System.Drawing.Size(166, 20);
            this.txtProblemCode.TabIndex = 24;
            this.txtProblemCode.Leave += new System.EventHandler(this.txtProblemCode_Leave);
            // 
            // lblProblem
            // 
            this.lblProblem.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProblem.Appearance.Options.UseFont = true;
            this.SetBoundPropertyName(this.lblProblem, "");
            this.lblProblem.Location = new System.Drawing.Point(16, 29);
            this.lblProblem.Name = "lblProblem";
            this.lblProblem.Size = new System.Drawing.Size(47, 13);
            this.lblProblem.TabIndex = 29;
            this.lblProblem.Text = "Problem";
            // 
            // dxProblems
            // 
            this.dxProblems.ContainerControl = this;
            // 
            // txtID
            // 
            this.SetBoundPropertyName(this.txtID, "");
            this.txtID.Location = new System.Drawing.Point(105, 3);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(40, 20);
            this.txtID.TabIndex = 30;
            this.txtID.Visible = false;
            // 
            // ileTrammingProblems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblProblem);
            this.Controls.Add(this.txtProblemCode);
            this.Controls.Add(this.btnUpdate);
            this.Name = "ileTrammingProblems";
            this.Size = new System.Drawing.Size(286, 103);
            this.Load += new System.EventHandler(this.ileSectionScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtProblemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxProblems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.LabelControl lblProblem;
        public DevExpress.XtraEditors.TextEdit txtProblemCode;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxProblems;
        public DevExpress.XtraEditors.TextEdit txtID;
    }
}
