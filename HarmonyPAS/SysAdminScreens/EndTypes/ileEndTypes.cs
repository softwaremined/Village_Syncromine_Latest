using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Mineware.Systems.Global;
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.EndTypes
{
    public partial class ileEndTypes : EditFormUserControl 
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public ileEndTypes()
        {
            InitializeComponent();
        }
    }
}
