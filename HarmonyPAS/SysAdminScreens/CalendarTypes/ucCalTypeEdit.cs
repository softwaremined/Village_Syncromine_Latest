using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace Mineware.Systems.Production.SysAdminScreens.CalendarTypes
{
    public partial class ucCalTypeEdit : EditFormUserControl
    {
        public bool isAdd
        {
            set
            {
                editName.Enabled = value;
            }
        }
        public ucCalTypeEdit()
        {
            InitializeComponent();
        }


    }
}
