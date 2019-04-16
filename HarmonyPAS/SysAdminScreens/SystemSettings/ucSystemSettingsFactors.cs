using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    public partial class ucSystemSettingsFactors : ucBaseUserControl
    {
        clsSystemSettings _clsSystemSettings = new clsSystemSettings();
        private DataTable dtSysset;
        public ucSystemSettingsFactors()
        {
            InitializeComponent();
        }

        private void ucSystemSettingsFactors_Load(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysset = _clsSystemSettings.getSysset();
            try
            {
                if (dtSysset.Rows.Count > 0)
                {
                    txtRockDensity.Text = dtSysset.Rows[0]["Rockdensity"].ToString();
                    txtBrokenRockDensity.Text = dtSysset.Rows[0]["Brokenrockdensity"].ToString();
                }
            }
            catch (Exception _exception)
            {
                MessageBox.Show(_exception.ToString());
            }
        }

        private void btnSaveFactors_Click(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            bool theSave = _clsSystemSettings.updateFactorsSettings(txtRockDensity.Text, txtBrokenRockDensity.Text);
            if (theSave == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "System Settings Factors Data Updated ", Color.CornflowerBlue);
            }
        }
    }
}
