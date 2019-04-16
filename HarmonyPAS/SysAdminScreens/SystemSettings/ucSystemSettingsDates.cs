using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    public partial class ucSystemSettingsDates : ucBaseUserControl
    {
        clsSystemSettings _clsSystemSettings = new clsSystemSettings();
        private DataTable dtSysset;
        public ucSystemSettingsDates()
        {
            InitializeComponent();
        }

        private void ucSystemSettingsDates_Load(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysset = _clsSystemSettings.getSysset();
            try
            {
                if (dtSysset.Rows.Count > 0)
                {
                    dteRunDate.Text = dtSysset.Rows[0]["theRunDate"].ToString();
                    dteProdMonth.Text = dtSysset.Rows[0]["Currentproductionmonth"].ToString();
                    dteMillMonth.Text = dtSysset.Rows[0]["Currentmillmonth"].ToString();
                    dteFinancialStart.Text = dtSysset.Rows[0]["FinYearStart"].ToString();
                    dteFinancialEnd.Text = dtSysset.Rows[0]["FinYearEnd"].ToString();
                }
            }
            catch (Exception _exception)
            {
                MessageBox.Show(_exception.ToString());
            }
        }
        private void btnSaveDates_Click(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            bool theSave = _clsSystemSettings.updateDatesSettings(dteRunDate.Text, dteProdMonth.Text, 
                    dteMillMonth.Text, dteFinancialStart.Text, dteFinancialEnd.Text);
            if (theSave == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "System Settings Dates Data Updated ", Color.CornflowerBlue);
            }
        }
        private void dteProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            dteProdMonth.Value = TProductionGlobal.spinMonth(Convert.ToDecimal(dteProdMonth.EditValue));
        }

        private void dteMillMonth_EditValueChanged(object sender, EventArgs e)
        {
            dteMillMonth.Value = TProductionGlobal.spinMonth(Convert.ToDecimal(dteMillMonth.EditValue));
        }

        private void pnlDatesData_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
