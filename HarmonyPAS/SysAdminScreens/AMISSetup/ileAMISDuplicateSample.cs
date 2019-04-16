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
using Mineware.Systems.HarmonyPASGlobal ;
using DevExpress.XtraGrid.Columns;

namespace Mineware.Systems.HarmonyPAS.SysAdminScreens.AMISSetup
{
    public partial class ileAMISDuplicateSample : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsAMISSetupData _clsAMISSetupData = new clsAMISSetupData();
        public string theAction = "";
        int _hierID = 0;

        public ileAMISDuplicateSample()
        {
            InitializeComponent();
        }

        private void ileAMISDuplicateSample_Load(object sender, EventArgs e)
        {
            _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }


    }
}
