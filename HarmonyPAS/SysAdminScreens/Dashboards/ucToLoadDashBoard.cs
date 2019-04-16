using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using System.Collections;
using MWDataManager;
using DevExpress.XtraGrid.Views.Grid;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.Dashboards
{
    public partial class ucToLoadDashBoard : Mineware.Systems.Global.ucBaseUserControl
    {
        public ucToLoadDashBoard()
        {
            InitializeComponent();
        }

        private void ucToLoadDashBoard_Load(object sender, EventArgs e)
        {
            ucMainDashboard ucDashboard = new ucMainDashboard();

            OnSendControlToMainTab(new SendControlToMainTabHandlerArgs(ucDashboard,

                                                                       "ucMainDashboard",

                                                                       this.UserCurrentInfo,

                                                                       false));

            //OnSendControlToMainTab(new SendControlToMainTabHandlerArgs(ucDashboard,

            //                                                           "Form Designer ucMainDashboard",

            //                                                           this.UserCurrentInfo,

            //                                                           false));
        }
    }
}
