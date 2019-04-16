using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    public partial class ucSystemSettingsCurrent : ucBaseUserControl
    {
        clsSystemSettings _clsSystemSettings = new clsSystemSettings();
        private DataTable dtSysset;
        public ucSystemSettingsCurrent()
        {
            InitializeComponent();
        }

        private void ucSystemSettingsCurrent_Load(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysset = _clsSystemSettings.getSysset();
            try
            {
                if (dtSysset.Rows.Count > 0)
                {
                    txtMaxAllowedAdvStp.Text = dtSysset.Rows[0]["MAXADVANCE"].ToString();
                    txtPercQual.Text = dtSysset.Rows[0]["PERCBLASTQUALIFICATION"].ToString();
                    txtMaxAllowedAdvDev.Text = dtSysset.Rows[0]["MaxAdvDev"].ToString();
                }
            }
            catch (Exception _exception)
            {
                MessageBox.Show(_exception.ToString());
            }
        }

        private void btnSaveCurrent_Click(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            bool theSave = _clsSystemSettings.updateCurrentSettings(txtMaxAllowedAdvStp.Text, txtPercQual.Text, txtMaxAllowedAdvDev.Text);
            if (theSave == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "System Settings Current Data Updated ", Color.CornflowerBlue);
            }
        }
    }
}
