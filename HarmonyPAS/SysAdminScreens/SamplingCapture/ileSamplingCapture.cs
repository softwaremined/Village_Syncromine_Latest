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
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SamplingCapture
{
    public partial class ileSamplingCapture : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        public string theAction = "";

        clsSamplingCaptureData _clsSamplingCaptureData = new clsSamplingCaptureData();

        public ileSamplingCapture()
        {
            InitializeComponent();
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dteCalendarDate.EditValue.ToString() != "" && txtSWidth.Text != "" && txtChannelwidth.Text != "" && txtHangingWall.Text != "" &&
                txtFootwall.Text != "" && txtCmgt.Text != "" && txtRIF.Text != "")
                _clsSamplingCaptureData.SaveData(Convert.ToDateTime(dteCalendarDate.EditValue.ToString()), txtWorkplaceID.Text,
                                                Convert.ToInt32(txtSWidth.Text), Convert.ToInt32(txtChannelwidth.Text), Convert.ToInt32(txtHangingWall.Text),
                                                Convert.ToInt32(txtFootwall.Text), Convert.ToInt32(txtCmgt.Text), Convert.ToInt32(txtRIF.Text), theAction);

            else
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", "Required fields cannot be empty", Color.Red);
            }

        }

        private void ileSamplingCapture_Load(object sender, EventArgs e)
        {
            _clsSamplingCaptureData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        }
    }
}
