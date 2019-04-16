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
using Mineware.Systems.ProductionGlobal;
using DevExpress.XtraGrid.Columns;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.QAQC
{
    public partial class ileCRM : EditFormUserControl
    {
        public string theSystemDBTag;
        public TUserCurrentInfo UserCurrentInfo;
        clsQAQCData _clsQAQCData = new clsQAQCData();
        public string shaft = "";
        public string amis = "";
        public string crm = "";
        public string theAction = "";
        public decimal meanValue = Convert.ToDecimal("0.00");

        public ileCRM()
        {
            InitializeComponent();
        }

        private void ileCRM_Load(object sender, EventArgs e)
        {
            _clsQAQCData.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            _clsQAQCData.SaveStandardCRMData(shaft, amis, Convert.ToDateTime(dteDate.EditValue.ToString()),
                                        txtTicketNumber.Text, Convert.ToDecimal(txtAssayValue.Text), txtOutcome.Text, Convert.ToDecimal(txtReAssayValue.Text), txtReAssayOutcome.Text, crm, theAction);
        }

        private void txtAssayValue_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal _assayValue = Convert.ToDecimal("0.00");

                if (txtAssayValue.Text == "")
                {
                    txtAssayValue.Text = Convert.ToDecimal("0.00").ToString();
                }

                else
                    _assayValue = Convert.ToDecimal(txtAssayValue.Text);

                if (Math.Abs((_assayValue - meanValue)) > (2 * meanValue))
                {
                    txtOutcome.Text = "FAIL";
                    txtOutcome.ForeColor = Color.Red;
                    lblReAssayValue.Visible = true;
                    txtReAssayValue.Visible = true;
                    lblReAssayOutcome.Visible = true;
                    txtReAssayOutcome.Visible = true;
                }

                else if (Math.Abs((_assayValue - meanValue)) > (1 * meanValue))
                {
                    txtOutcome.Text = "WARNING";
                    txtOutcome.ForeColor = Color.Orange;
                    lblReAssayValue.Visible = false;
                    txtReAssayValue.Visible = false;

                    txtReAssayValue.EditValue = Convert.ToDecimal("0.00");

                    lblReAssayOutcome.Visible = false;
                    txtReAssayOutcome.Visible = false;
                }

                else
                {
                    txtOutcome.Text = "PASS";
                    txtOutcome.ForeColor = Color.Green;
                    txtOutcome.Text = "";
                    txtReAssayValue.Text = "";
                    txtReAssayOutcome.Text = "";
                    lblReAssayValue.Visible = false;
                    txtReAssayValue.Visible = false;

                    txtReAssayValue.EditValue = Convert.ToDecimal("0.00");

                    lblReAssayOutcome.Visible = false;
                    txtReAssayOutcome.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", ex.Message, Color.Red);
            }

        }

        private void txtReAssayValue_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal _reAssayValue = Convert.ToDecimal("0.00");
                if (txtReAssayValue.Text == "")
                {
                    txtReAssayValue.Text = Convert.ToDecimal("0.00").ToString();
                }

                else
                    _reAssayValue = Convert.ToDecimal(txtReAssayValue.Text);

                if (txtOutcome.Text == "FAIL")
                {
                    if (Math.Abs((_reAssayValue - meanValue)) > (3 * meanValue))
                    {
                        txtReAssayOutcome.EditValue = "DELETE ALL SAMPLES ON TRAY";
                        txtReAssayOutcome.ForeColor = Color.Red;
                    }

                    else
                    {
                        txtReAssayOutcome.EditValue = "ACCEPT THE 2ND TRAY VALUES";
                        txtReAssayOutcome.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                Mineware.Systems.Global.sysNotification.TsysNotification.showNotification("Error", ex.Message, Color.Red);
            }

        }
    }
}
