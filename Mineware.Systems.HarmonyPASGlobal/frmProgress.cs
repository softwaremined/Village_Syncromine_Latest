using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;


namespace Mineware.Systems.ProductionGlobal
{
    public partial class frmProgress : Form 
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        public void SetProgresMax(int MaxValue)
        {
            pbTheProgress.Properties.Maximum = MaxValue;
        }

        public void SetProgressPosition(int thePos)
        {
            pbTheProgress.Position = thePos;
            this.Refresh();
        }

        public void SetDescription(string theDescription)
        {
            labDescription.Text = theDescription;
        }

        public void SetCaption(string theCaption)
        {
            this.Text = theCaption;
        }

    }
}