using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.SysAdminScreens.SystemSettings
{
    public partial class ucSystemSettingsBookings : ucBaseUserControl
    {
        clsSystemSettings _clsSystemSettings = new clsSystemSettings();

        private string BookFL;
        private string DisableBookingCyclePlan;
        private string ProblemNew;
        private string Problem_ProblemTypeLink;
        private string ProblemGroup_ProblemTypeLink;
        private string ProblemForceNote;
        private string ProblemNewValidation;
        private string AllowSWCWBook;
        private string SamplingValue;
        private string SamplingUseLatestForPlan;
        private string CheckMeasLvl;

        private DataTable dtSysset;
        private DataTable dtCheckMeas;
        public ucSystemSettingsBookings()
        {
            InitializeComponent();
        }

        private void ucSystemSettingsBookings_Load(object sender, EventArgs e)
        {
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysset = _clsSystemSettings.getSysset();

            dtSysset = _clsSystemSettings.getSysset();
            try
            {
                if (dtSysset.Rows.Count > 0)
                {
                    chkBookFaceLength.Checked = false;
                    chkDisableBookingCyclePlan.Checked = false;
                    chkUseNewValidationMethod.Checked = false;
                    chkForceOwnNotes.Checked = false;
                    chkForceProblemGroup.Checked = false;
                    chkForceProblemType.Checked = false;
                    chkNewProblemBookingMethod.Checked = false;
                    chkAllowBookingofSwCw.Checked = false;
                    chkSamplingUseLatestForPlan.Checked = false;

                    if (dtSysset.Rows[0]["AllowSWCWBook"].ToString() == "Y")
                    {
                        chkAllowBookingofSwCw.Checked = true;
                        txtPlanSW.Text = dtSysset.Rows[0]["SWCheck"].ToString();
                        txtPlanCW.Text = dtSysset.Rows[0]["CWCheck"].ToString();
                        txtPlanSW.Enabled = true;
                        txtPlanCW.Enabled = true;
                    }
                    else
                    {
                        txtPlanSW.Text = "0";
                        txtPlanCW.Text = "0";
                        txtPlanSW.Enabled = false;
                        txtPlanCW.Enabled = false;
                    }

                    if (dtSysset.Rows[0]["BookFL"].ToString() == "Y")
                        chkBookFaceLength.Checked = true;

                    if (dtSysset.Rows[0]["DisableBookingCyclePlan"].ToString() == "Y")
                        chkDisableBookingCyclePlan.Checked = true;

                    if (dtSysset.Rows[0]["ProblemNew"].ToString() == "Y")
                        chkUseNewValidationMethod.Checked = true;

                    if (dtSysset.Rows[0]["Problem_ProblemTypeLink"].ToString() == "Y")
                        chkForceProblemType.Checked = true;

                    if (dtSysset.Rows[0]["ProblemGroup_ProblemTypeLink"].ToString() == "Y")
                        chkForceProblemGroup.Checked = true;

                    if (dtSysset.Rows[0]["ProblemForceNote"].ToString() == "Y")
                        chkForceOwnNotes.Checked = true; 

                    if (dtSysset.Rows[0]["ProblemNewValidation"].ToString() == "Y")
                        chkNewProblemBookingMethod.Checked = true;
    
                    if (dtSysset.Rows[0]["SamplingValue"].ToString() == "K")
                    {
                        rdgrpGradeBookingsSettings.SelectedIndex = 0;
                    }
                    else if (dtSysset.Rows[0]["SamplingValue"].ToString() == "S")
                    {
                        rdgrpGradeBookingsSettings.SelectedIndex = 1;
                    }
                    else if (dtSysset.Rows[0]["SamplingValue"].ToString() == "B")
                    {
                        rdgrpGradeBookingsSettings.SelectedIndex = 2;
                    }

                    if (dtSysset.Rows[0]["SamplingUseLatestForPlan"].ToString() == "Y")
                        chkSamplingUseLatestForPlan.Checked = true;

                    string _colorA = dtSysset.Rows[0]["A_Color"].ToString();
                    string _colorB = dtSysset.Rows[0]["B_Color"].ToString();
                    string _colorS = dtSysset.Rows[0]["S_Color"].ToString();
                    txtColorA.Color = Color.FromArgb(Convert.ToInt32(_colorA));
                    txtColorB.Color = Color.FromArgb(Convert.ToInt32(_colorB));
                    txtColorS.Color = Color.FromArgb(Convert.ToInt32(_colorS));

                    if (dtSysset.Rows[0]["CheckMeas"].ToString() != "None")
                    {
                        cmbCheckMeas.Text = dtSysset.Rows[0]["CheckMeas"].ToString();
                        if (dtSysset.Rows[0]["CheckMeasLvl"].ToString() == "MO")
                            rdgrpCheckMeasType.SelectedIndex = 1;
                        else
                        {
                            if (dtSysset.Rows[0]["CheckMeasLvl"].ToString() == "SB")
                                rdgrpCheckMeasType.SelectedIndex = 2;
                            else
                                rdgrpCheckMeasType.SelectedIndex = 0;
                        }
                        rdgrpCheckMeasType.Enabled = true;
                    }
                    else
                    {
                        rdgrpCheckMeasType.SelectedIndex = 0;
                        rdgrpCheckMeasType.Enabled = false;
                    }
                    txtPercBlastQual.Text = dtSysset.Rows[0]["PERCBLASTQUALIFICATION"].ToString();
                }
            }
            catch (Exception _exception)
            {
                MessageBox.Show(_exception.ToString());
            }
        }

        private void btnSaveBookings_Click(object sender, EventArgs e)
        {
            AllowSWCWBook = "N";

            BookFL = "N";
            DisableBookingCyclePlan = "N";

            ProblemNew = "N";
            Problem_ProblemTypeLink = "N";
            ProblemGroup_ProblemTypeLink = "N";
            ProblemForceNote = "N";
            ProblemNewValidation = "N";
            CheckMeasLvl = "";
            ProblemNewValidation = "N";

            if (chkAllowBookingofSwCw.Checked == true)
                AllowSWCWBook = "Y";

            if (chkBookFaceLength.Checked == true)
                BookFL = "Y";

            if (chkDisableBookingCyclePlan.Checked == true)
                DisableBookingCyclePlan = "Y";

            if (chkUseNewValidationMethod.Checked == true)
                ProblemNew = "Y";

            if (chkForceOwnNotes.Checked == true)
                Problem_ProblemTypeLink = "Y";

            if (chkForceProblemGroup.Checked == true)
                ProblemGroup_ProblemTypeLink = "Y";

            if (chkForceProblemType.Checked == true)
                ProblemForceNote = "Y";

            if (chkNewProblemBookingMethod.Checked == true)
                ProblemNewValidation = "Y";

            if (rdgrpGradeBookingsSettings.SelectedIndex == 0)
            {
                SamplingValue = "K";
            }
            else if (rdgrpGradeBookingsSettings.SelectedIndex == 1)
            {
                SamplingValue = "S";
            }
            else if (rdgrpGradeBookingsSettings.SelectedIndex == 2)
            {
                SamplingValue = "B";
            }

            if (chkSamplingUseLatestForPlan.Checked == true)
                SamplingUseLatestForPlan = "Y";

            if (rdgrpCheckMeasType.SelectedIndex == 0)
            {
                CheckMeasLvl = "";
            }
            else
            {
                if (rdgrpCheckMeasType.SelectedIndex == 1)
                    CheckMeasLvl = "MO";
                else
                    CheckMeasLvl = "SB";
            }
            int aa = txtColorA.Color.ToArgb();
            int bb = txtColorB.Color.ToArgb();
            int ss = txtColorS.Color.ToArgb();
            _clsSystemSettings.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            bool theSave = _clsSystemSettings.updateBookingsSettings(AllowSWCWBook, txtPlanSW.Text,
                                          txtPlanCW.Text,
                                          BookFL,
                                          DisableBookingCyclePlan,
                                          ProblemNew,
                                          Problem_ProblemTypeLink,
                                          ProblemGroup_ProblemTypeLink,
                                          ProblemForceNote,
                                          ProblemNewValidation,                                         
                                          SamplingValue,
                                          SamplingUseLatestForPlan,
                                          aa, bb, ss,
                                          cmbCheckMeas.Text, txtPercBlastQual.Text, CheckMeasLvl);
            if (theSave == true)
            {
                Global.sysNotification.TsysNotification.showNotification("Data Saved", "System Settings Bookings Data Updated ", Color.CornflowerBlue);
            }
        }

        private void chkAllowBookingofSwCw_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllowBookingofSwCw.Checked == true)
            {
                txtPlanSW.Enabled = true;
                txtPlanCW.Enabled = true;
            }
            else
            {
                txtPlanSW.Text = "0";
                txtPlanCW.Text = "0";
                txtPlanSW.Enabled = false;
                txtPlanCW.Enabled = false;
            }
        }

        private void cmbCheckMeas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCheckMeas.Text == "None")
            {
                rdgrpCheckMeasType.SelectedIndex = 0;
                rdgrpCheckMeasType.Enabled = false;
            }
            else
            {
                rdgrpCheckMeasType.Enabled = true;
            }
        }
    }
}
