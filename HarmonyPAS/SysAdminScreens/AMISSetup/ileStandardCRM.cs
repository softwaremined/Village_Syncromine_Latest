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
using Mineware.Systems.ProductionGlobal ;
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.AMISSetup
{
    public partial class ileStandardCRM : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsAMISSetupData _clsAMISSetupData = new clsAMISSetupData();
        public string theAction = "";
        public string shaft = "";

        public ileStandardCRM()
        {
            InitializeComponent();
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            _clsAMISSetupData.SaveStandardCRMData(shaft, txtCRM.Text, Convert.ToDecimal(txtMeanValue.Text), Convert.ToDecimal(txtStdDev.Text), theAction);
        }

        private void ileStandardCRM_Load(object sender, EventArgs e)
        {
            _clsAMISSetupData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        }
    }
}
