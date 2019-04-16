using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    public partial class ucSystemSettingsMines : ucBaseUserControl
    {
        clsSystemSettings _clsSystemSettings = new clsSystemSettings();
        private DataTable dtSysset;
       // private DataTable dtSysset;

        private string _MOHierID;
        private string _EngCapLvl;
        private string _EngProdlink;
        private string _EngNoBackDays;
        public ucSystemSettingsMines()
        {
            InitializeComponent();
        }

        private void ucSystemSettingsMines_Load(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysset = _clsSystemSettings.getSysset();
            try
            {
                if (dtSysset.Rows.Count > 0)
                {
                    txtBanner.Text = dtSysset.Rows[0]["Banner"].ToString();
                    txtMOHierarchicalID.Text = dtSysset.Rows[0]["MOHierarchicalID"].ToString();
                    txtEngCaptLvl.Text = dtSysset.Rows[0]["EngCaptlevel"].ToString();
                    txtEngProdLink.Text = dtSysset.Rows[0]["EngToProdLink"].ToString();
                    txtEngNoOfDaysBackdate.Text = dtSysset.Rows[0]["EngBackDateDays"].ToString();
                    TopPanelsTxt.Text = dtSysset.Rows[0]["NoOfPanels"].ToString();
                }
            }
            catch (Exception _exception)
            {
                MessageBox.Show(_exception.ToString());
            }
        }

        private void btnSaveBookings_Click(object sender, EventArgs e)
        {
            _MOHierID = txtMOHierarchicalID.Text;
            _EngCapLvl = txtEngCaptLvl.Text;
            _EngProdlink = txtEngProdLink.Text;
            _EngNoBackDays = txtEngNoOfDaysBackdate.Text;

            if (txtMOHierarchicalID.Text == "")
            {
                _MOHierID = "5";
            }
            if (txtEngCaptLvl.Text == "")
            {
                _EngCapLvl = "0";
            }
            if (txtEngProdLink.Text == "")
            {
                _EngProdlink = "0";
            }
            if (txtEngNoOfDaysBackdate.Text == "")
            {
                _EngNoBackDays = "0";
            }

            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            bool theSave = _clsSystemSettings.updateMinesSettings(
                        txtBanner.Text, txtMOHierarchicalID.Text, txtEngCaptLvl.Text, 
                        txtEngProdLink.Text, txtEngNoOfDaysBackdate.Text);
            if (theSave == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "System Settings Mines Data Updated ", Color.CornflowerBlue);
            }

            bool theSave2 = _clsSystemSettings.updateTopPanelsSettings(Convert.ToInt32(TopPanelsTxt.Text));
            
        }

        private void pnlMinesData_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
