using System;
using System.Collections.Generic;
using System.Linq;
using Mineware.Systems.Global;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.GangNo
{
    public partial class ucGangNoScreen : ucBaseUserControl
    {
        clsGangNoScreenData _clsGangNoScreenData = new clsGangNoScreenData();
        DataTable GangNoScreenData;
        string aa;
        string maxdate;
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();

        public ucGangNoScreen()
        {
            InitializeComponent();
            this.CanClose = true;
        }

        public void loadScreenData()
        {
            _clsGangNoScreenData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            GangNoScreenData = _clsGangNoScreenData.getGangNoData(Convert.ToDateTime(dteCalendarDate.EditValue).ToString("yyyy-MM-dd"));
            gcGangNo.DataSource = GangNoScreenData;

        }

        void SetCalendarDate()
        {
            dteCalendarDate.EditValue = DateTime.Now;
        }


        private void dteCalendarDate_EditValueChanged(object sender, EventArgs e)
        {
            loadScreenData();
        }

        private void ucGangNoScreen_Load(object sender, EventArgs e)
        {
            SetCalendarDate();
            loadScreenData();
        }

        private void gcGangNo_Click(object sender, EventArgs e)
        {

        }
    }
}
