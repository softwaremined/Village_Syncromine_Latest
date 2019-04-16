namespace WinNtServiceDebugger
{
    partial class PluginWindow
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
			this.btnLoadAssembly = new System.Windows.Forms.Button();
			this.btnRunAssemblyType = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lstBxTypes = new System.Windows.Forms.ListBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnLoadAssembly
			// 
			this.btnLoadAssembly.Location = new System.Drawing.Point(15, 12);
			this.btnLoadAssembly.Name = "btnLoadAssembly";
			this.btnLoadAssembly.Size = new System.Drawing.Size(134, 23);
			this.btnLoadAssembly.TabIndex = 0;
			this.btnLoadAssembly.Text = "Load Service Assembly";
			this.btnLoadAssembly.UseVisualStyleBackColor = true;
			this.btnLoadAssembly.Click += new System.EventHandler(this.btnLoadAssembly_Click);
			// 
			// btnRunAssemblyType
			// 
			this.btnRunAssemblyType.Location = new System.Drawing.Point(15, 168);
			this.btnRunAssemblyType.Name = "btnRunAssemblyType";
			this.btnRunAssemblyType.Size = new System.Drawing.Size(134, 23);
			this.btnRunAssemblyType.TabIndex = 1;
			this.btnRunAssemblyType.Text = "Run Service Plugin";
			this.btnRunAssemblyType.UseVisualStyleBackColor = true;
			this.btnRunAssemblyType.Click += new System.EventHandler(this.btnRunAssembly_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Loaded Assembly :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(95, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 13);
			this.label2.TabIndex = 3;
			// 
			// lstBxTypes
			// 
			this.lstBxTypes.FormattingEnabled = true;
			this.lstBxTypes.Location = new System.Drawing.Point(15, 67);
			this.lstBxTypes.Name = "lstBxTypes";
			this.lstBxTypes.Size = new System.Drawing.Size(737, 95);
			this.lstBxTypes.TabIndex = 4;
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(155, 168);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(134, 23);
			this.btnStop.TabIndex = 5;
			this.btnStop.Text = "Stop Service Plugin";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// PluginWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(761, 205);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.lstBxTypes);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnRunAssemblyType);
			this.Controls.Add(this.btnLoadAssembly);
			this.Name = "PluginWindow";
			this.Text = "WinNtDebugger";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadAssembly;
        private System.Windows.Forms.Button btnRunAssemblyType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstBxTypes;
		private System.Windows.Forms.Button btnStop;
    }
}

