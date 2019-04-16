using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mineware.Systems.Planning
{
    public partial class RevisedPlanning : Form
    {
      public  bool cancelledaction;
        public RevisedPlanning()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cancelledaction = false;
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            cancelledaction = true;
            Close();
        }
    }
}
