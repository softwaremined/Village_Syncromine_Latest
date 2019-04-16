namespace Mineware.Systems.Reports._6ShiftRecon
{
    partial class ucRecon
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
            this.TypeRgb = new DevExpress.XtraEditors.RadioGroup();
            this.FromLbl = new System.Windows.Forms.Label();
            this.ProdMonthTxt = new System.Windows.Forms.NumericUpDown();
            this.ProdMonth1Txt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Otherradio = new DevExpress.XtraEditors.RadioGroup();
            this.SectionsCombo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.TypeRgb.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Otherradio.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // TypeRgb
            // 
            this.TypeRgb.Location = new System.Drawing.Point(6, 87);
            this.TypeRgb.Name = "TypeRgb";
            this.TypeRgb.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.TypeRgb.Properties.Appearance.Options.UseBackColor = true;
            this.TypeRgb.Properties.Columns = 1;
            this.TypeRgb.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Stoping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Development"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Vamping")});
            this.TypeRgb.Size = new System.Drawing.Size(181, 61);
            this.TypeRgb.TabIndex = 105;
            // 
            // FromLbl
            // 
            this.FromLbl.AutoSize = true;
            this.FromLbl.BackColor = System.Drawing.Color.White;
            this.FromLbl.Location = new System.Drawing.Point(3, 4);
            this.FromLbl.Name = "FromLbl";
            this.FromLbl.Size = new System.Drawing.Size(91, 13);
            this.FromLbl.TabIndex = 104;
            this.FromLbl.Text = "Production Month";
            // 
            // ProdMonthTxt
            // 
            this.ProdMonthTxt.Location = new System.Drawing.Point(88, 20);
            this.ProdMonthTxt.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ProdMonthTxt.Name = "ProdMonthTxt";
            this.ProdMonthTxt.Size = new System.Drawing.Size(18, 21);
            this.ProdMonthTxt.TabIndex = 103;
            this.ProdMonthTxt.ValueChanged += new System.EventHandler(this.ProdMonthTxt_ValueChanged);
            this.ProdMonthTxt.Click += new System.EventHandler(this.ProdMonthTxt_Click);
            // 
            // ProdMonth1Txt
            // 
            this.ProdMonth1Txt.BackColor = System.Drawing.Color.White;
            this.ProdMonth1Txt.Location = new System.Drawing.Point(6, 20);
            this.ProdMonth1Txt.MaxLength = 10000000;
            this.ProdMonth1Txt.Name = "ProdMonth1Txt";
            this.ProdMonth1Txt.ReadOnly = true;
            this.ProdMonth1Txt.Size = new System.Drawing.Size(100, 21);
            this.ProdMonth1Txt.TabIndex = 102;
            this.ProdMonth1Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ProdMonth1Txt.TextChanged += new System.EventHandler(this.ProdMonth1Txt_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(3, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 101;
            this.label9.Text = "Section:";
            // 
            // Otherradio
            // 
            this.Otherradio.Location = new System.Drawing.Point(6, 154);
            this.Otherradio.Name = "Otherradio";
            this.Otherradio.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.Otherradio.Properties.Appearance.Options.UseBackColor = true;
            this.Otherradio.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Adv"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Sqm")});
            this.Otherradio.Size = new System.Drawing.Size(181, 29);
            this.Otherradio.TabIndex = 106;
            this.Otherradio.Visible = false;
            // 
            // SectionsCombo
            // 
            this.SectionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectionsCombo.FormattingEnabled = true;
            this.SectionsCombo.Location = new System.Drawing.Point(6, 60);
            this.SectionsCombo.Name = "SectionsCombo";
            this.SectionsCombo.Size = new System.Drawing.Size(176, 21);
            this.SectionsCombo.TabIndex = 107;
            this.SectionsCombo.SelectedIndexChanged += new System.EventHandler(this.SectionsCombo_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(6, 189);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(171, 18);
            this.panel1.TabIndex = 108;
            // 
            // ucRecon
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SectionsCombo);
            this.Controls.Add(this.Otherradio);
            this.Controls.Add(this.TypeRgb);
            this.Controls.Add(this.FromLbl);
            this.Controls.Add(this.ProdMonthTxt);
            this.Controls.Add(this.ProdMonth1Txt);
            this.Controls.Add(this.label9);
            this.Name = "ucRecon";
            this.Size = new System.Drawing.Size(228, 456);
            this.Load += new System.EventHandler(this.ucRecon_Load);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.ProdMonth1Txt, 0);
            this.Controls.SetChildIndex(this.ProdMonthTxt, 0);
            this.Controls.SetChildIndex(this.FromLbl, 0);
            this.Controls.SetChildIndex(this.TypeRgb, 0);
            this.Controls.SetChildIndex(this.Otherradio, 0);
            this.Controls.SetChildIndex(this.SectionsCombo, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.TypeRgb.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Otherradio.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.RadioGroup TypeRgb;
        private System.Windows.Forms.Label FromLbl;
        private System.Windows.Forms.NumericUpDown ProdMonthTxt;
        private System.Windows.Forms.TextBox ProdMonth1Txt;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.RadioGroup Otherradio;
        private System.Windows.Forms.ComboBox SectionsCombo;
        private System.Windows.Forms.Panel panel1;
    }
}
