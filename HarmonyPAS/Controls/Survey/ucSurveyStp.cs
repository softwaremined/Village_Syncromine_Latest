using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.Survey
{
    public partial class ucSurveyStp : DevExpress.XtraEditors.XtraForm
    {
        public string theConnection;
        clsSurvey _clsSurvey = new clsSurvey();

        private DataTable dtEntries;
        private DataTable dtDensity;
        private DataTable dtMaxSeqNo;
        private DataTable dtTheWP;
        private DataTable dtSurvey;
        private DataTable dtSurveyWP;
        private DataTable dtPlanCrews;
        private DataTable dtSurveyVisibles;

        private int MaxSeqNo;

        private decimal TheDens;
        private decimal TheBrokenRockDens;
        private string SType;

        private bool ErrorFound;
        private bool _loadSurvey;
        public ucSurveyStp()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Initialize_Stoping_Fields();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ErrorFound = false;
            Validate_Stoping();
            if (ErrorFound == false)
            {
                Save_SurveyStoping();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtSurvey = _clsSurvey.get_MaxSeqNo(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Stp");

            if (dtSurvey.Rows.Count != 0)
            {
                MaxSeqNo = Convert.ToInt32(dtSurvey.Rows[0]["SeqNo"].ToString());
                if (MaxSeqNo != Convert.ToInt32(txtSeqNo.Text))
                {
                    if (MaxSeqNo != 0)
                        System.Windows.Forms.MessageBox.Show("You may only Delete the Last Survey Entry");
                    else
                        System.Windows.Forms.MessageBox.Show("No Entries have been added - cannot delete");
                }
                else
                {
                    bool _deleteSurvey = _clsSurvey.delete_Survey(lblProdMonth.Text, lblContractor.Text,
                                                                    lblWorkplaceID.Text, MaxSeqNo, "Stp");
                    if (_deleteSurvey == true)
                    {
                        Global.sysNotification.TsysNotification.showNotification("Data Saved", "Survey Deleted", Color.CornflowerBlue);
                    }
                    Load_Entries();
                    Load_SurveyStoping();
                }
            }
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            ErrorFound = false;
            Validate_Stoping();
            if (ErrorFound == false)
            {
                Save_SurveyStoping();
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void Load_Entries()
        {
            cmbSeqNo.Properties.Items.Clear();
            _clsSurvey.theData.ConnectionString = theConnection;
            dtEntries = _clsSurvey.get_Entries(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Stp");

            if (dtEntries.Rows.Count != 0)
            {
                MaxSeqNo = 0;
                foreach (DataRow drStatus in dtEntries.Rows)
                {
                    if (MaxSeqNo < Convert.ToInt32(drStatus["SeqNo"].ToString()))
                    {
                        MaxSeqNo = Convert.ToInt32(drStatus["SeqNo"].ToString());
                    }
                    cmbSeqNo.Properties.Items.Add(drStatus["SeqNo"].ToString());
                }
            }
            else
            {
                MaxSeqNo = 1;
                cmbSeqNo.Properties.Items.Add(MaxSeqNo.ToString());
            }
            txtSeqNo.Text = MaxSeqNo.ToString();
            cmbSeqNo.Text = MaxSeqNo.ToString();
        }
        public void Load_Density()
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtDensity = _clsSurvey.get_Density(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Stp");
            if (dtDensity.Rows.Count != 0)
            {
                foreach (DataRow dr in dtDensity.Rows)
                {
                    if (dr["TheType"].ToString() == "RD")
                        TheDens = Convert.ToDecimal(dr["Density"].ToString());
                    if (dr["TheType"].ToString() == "BRD")
                        TheBrokenRockDens = Convert.ToDecimal(dr["Density"].ToString());
                }
                luDensity.Properties.DataSource = dtDensity;
                luDensity.Properties.ValueMember = "TheType";
                luDensity.Properties.DisplayMember = "RockDensity";
            }
        }
        public void Load_SurveyStoping()
        {
            _loadSurvey = true;
            _clsSurvey.theData.ConnectionString = theConnection;
            dtSurveyWP = _clsSurvey.get_Stoping_Detail(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, txtSeqNo.Text);

            if (dtSurveyWP.Rows.Count != 0)
            {
                Load_Density();
                //CalendarDate.Value = Convert.ToDateTime(dtSurveyWP.Rows[0]["calendardate"].ToString());
                CalendarDate.Value = System.DateTime.Now;
                txtDip.Text = dtSurveyWP.Rows[0]["Dip"].ToString();

                luMiningMethod.EditValue = Convert.ToInt32(dtSurveyWP.Rows[0]["MineMethod"].ToString());
                //cmbDensity.SelectedItem = dtSurveyWP.Rows[0]["Density"].ToString();
                //cmbDensity.Text = dtSurveyWP.Rows[0]["Density"].ToString();
                luDensity.EditValue = "";
                luStopeType.EditValue = Convert.ToInt32(dtSurveyWP.Rows[0]["StopeTypeID"].ToString());

                cmbMCrew.Text = dtSurveyWP.Rows[0]["CrewMorning"].ToString();
                cmbACrew.Text = dtSurveyWP.Rows[0]["CrewAfternoon"].ToString();
                cmbECrew.Text = dtSurveyWP.Rows[0]["CrewEvening"].ToString();
                cmbCleaningCrew.Text = dtSurveyWP.Rows[0]["CleaningCrew"].ToString();
                cmbTrammingCrew.Text = dtSurveyWP.Rows[0]["TrammingCrew"].ToString();
                cmbHlgeMaintainCrew.Text = dtSurveyWP.Rows[0]["HlgeMaintainanceCrew"].ToString();
                cmbRiggerCrew.Text = dtSurveyWP.Rows[0]["RiggerCrew"].ToString();
                cmbRseCleaningCrew.Text = dtSurveyWP.Rows[0]["RseCleaningCrew"].ToString();
                txtReefMetres.Text = dtSurveyWP.Rows[0]["Reefmetres"].ToString();
                txtWasteMetres.Text = dtSurveyWP.Rows[0]["Wastemetres"].ToString();
                txtMeasWidth.Text = dtSurveyWP.Rows[0]["MeasWidth"].ToString();
                txtMeasHeight.Text = dtSurveyWP.Rows[0]["MeasHeight"].ToString();
                txtPlanWidth.Text = dtSurveyWP.Rows[0]["PlanWidth"].ToString();
                txtPlanHeight.Text = dtSurveyWP.Rows[0]["PlanHeight"].ToString();
                txtLockUpTons.Text = dtSurveyWP.Rows[0]["LockUpTons"].ToString();

                //cmbBlockNo.Text = dtSurveyWP.Rows[0]["Blockno"].ToString();
                //txtBlockWidth.Text = dtSurveyWP.Rows[0]["BlockWidth"].ToString();
                //txtBlockValue.Text = dtSurveyWP.Rows[0]["BlockValue"].ToString();

                txtStopeSqm.Text = dtSurveyWP.Rows[0]["StopeSqm"].ToString();
                txtStopeSqmOSF.Text = dtSurveyWP.Rows[0]["StopeSqmOSF"].ToString();
                txtStopeSqmOS.Text = dtSurveyWP.Rows[0]["StopeSqmOS"].ToString();
                txtStopeSqmTotal.Text = dtSurveyWP.Rows[0]["StopeSqmTotal"].ToString();
                txtLedgeSqm.Text = dtSurveyWP.Rows[0]["LedgeSqm"].ToString();
                txtLedgeSqmOSF.Text = dtSurveyWP.Rows[0]["LedgeSqmOSF"].ToString();
                txtLedgeSqmOS.Text = dtSurveyWP.Rows[0]["LedgeSqmOS"].ToString();
                txtLedgeSqmTotal.Text = dtSurveyWP.Rows[0]["LedgeSqmTotal"].ToString();
                txtStopeFL.Text = dtSurveyWP.Rows[0]["StopeFL"].ToString();
                txtStopeFLOS.Text = dtSurveyWP.Rows[0]["StopeFLOS"].ToString();
                txtLedgeFL.Text = dtSurveyWP.Rows[0]["LedgeFL"].ToString();
                txtLedgeFLOS.Text = dtSurveyWP.Rows[0]["LedgeFLOS"].ToString();
                txtFLOSTotal.Text = dtSurveyWP.Rows[0]["FLOSTotal"].ToString();
                txtMeasAdvSW.Text = dtSurveyWP.Rows[0]["MeasAdvSW"].ToString();
                txtSWIdeal.Text = dtSurveyWP.Rows[0]["SWIdeal"].ToString();
                txtFLTotal.Text = dtSurveyWP.Rows[0]["FLTotal"].ToString();
                txtSqmConvTotal.Text = dtSurveyWP.Rows[0]["SqmConvTotal"].ToString();
                txtSqmOSFTotal.Text = dtSurveyWP.Rows[0]["SqmOSFTotal"].ToString();
                txtSqmOSTotal.Text = dtSurveyWP.Rows[0]["SqmOSTotal"].ToString();
                txtSqmTotal.Text = dtSurveyWP.Rows[0]["SqmTotal"].ToString();
                int aa = Convert.ToInt32(dtSurveyWP.Rows[0]["ExtraType"].ToString());
                luCubicTypes.EditValue = aa.ToString();
                txtCubicsmetres.Text = dtSurveyWP.Rows[0]["Cubicsmetres"].ToString();
                txtCubicsReef.Text = dtSurveyWP.Rows[0]["CubicsReef"].ToString();
                txtCubicsWaste.Text = dtSurveyWP.Rows[0]["CubicsWaste"].ToString();
                txtLabour.Text = dtSurveyWP.Rows[0]["Labour"].ToString();
                if (dtSurveyWP.Rows[0]["PaidUnpaid"].ToString() == "Y")
                {
                    chkboxPaid.Checked = true;
                }
                else
                {
                    chkboxPaid.Checked = false;
                }
                txtFW.Text = dtSurveyWP.Rows[0]["FW"].ToString();
                txtCW.Text = dtSurveyWP.Rows[0]["CW"].ToString();
                txtHW.Text = dtSurveyWP.Rows[0]["HW"].ToString();
                txtSWSqm.Text = dtSurveyWP.Rows[0]["SWSqm"].ToString();
                txtSWOSF.Text = dtSurveyWP.Rows[0]["SWOSF"].ToString();
                txtSWOS.Text = dtSurveyWP.Rows[0]["SWOS"].ToString();
                txtcmgt.Text = dtSurveyWP.Rows[0]["cmgt"].ToString();
                luOreflowID.EditValue = dtSurveyWP.Rows[0]["OreflowID"].ToString();
                rdgrpDestination.EditValue = dtSurveyWP.Rows[0]["OreflowID"].ToString();
                //MessageBox.Show(dtSurveyWP.Rows[0]["CleanTypeID"].ToString());
                if (dtSurveyWP.Rows[0]["CleanTypeID"] == null)
                    luCleanTypes.EditValue = null;
                else
                    if (Convert.ToInt32(dtSurveyWP.Rows[0]["CleanTypeID"].ToString()) == -1)
                    luCleanTypes.EditValue = null;
                else
                    luCleanTypes.EditValue = dtSurveyWP.Rows[0]["CleanTypeID"].ToString();//Convert.ToInt32(dtSurveyWP.Rows[0]["CleanTypeID"].ToString());
                if (Convert.ToInt32(dtSurveyWP.Rows[0]["CleanTypeID"].ToString()) == 13)
                {
                    txtOGTons.Visible = true;
                    txtOGDepth.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    txtOGTons.Text = dtSurveyWP.Rows[0]["CleanTons"].ToString();
                    txtOGDepth.Text = dtSurveyWP.Rows[0]["CleanDepth"].ToString();
                    label94.Visible = false;
                    txtCleanTons.Visible = false;
                }
                else
                {
                    txtOGTons.Visible = false;
                    txtOGDepth.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label94.Visible = true;
                    txtCleanTons.Visible = true;
                    txtCleanTons.Text = dtSurveyWP.Rows[0]["CleanTons"].ToString();
                }
                txtCleanSqm.Text = dtSurveyWP.Rows[0]["CleanSqm"].ToString();
                txtCleanDist.Text = dtSurveyWP.Rows[0]["CleanDist"].ToString();
                txtCleanVamp.Text = dtSurveyWP.Rows[0]["CleanVamp"].ToString();
                txtCleanTons.Text = dtSurveyWP.Rows[0]["CleanTons"].ToString();
                txtCleangt.Text = dtSurveyWP.Rows[0]["Cleangt"].ToString();
                txtCleanContents.Text = dtSurveyWP.Rows[0]["CleanContents"].ToString();
                txtTonsReef.EditValue = dtSurveyWP.Rows[0]["TonsReef"].ToString();
                txtTonsWaste.EditValue = dtSurveyWP.Rows[0]["TonsWaste"].ToString();
                txtTonsOSF.EditValue = dtSurveyWP.Rows[0]["TonsOSF"].ToString();
                txtTonsOS.EditValue = dtSurveyWP.Rows[0]["TonsOS"].ToString();
                txtTonsTotal.EditValue = dtSurveyWP.Rows[0]["TonsTotal"].ToString();
                rdgrpPayments.EditValue = Convert.ToInt32(dtSurveyWP.Rows[0]["Payment"].ToString());

                txtPlanNo.Text = dtSurveyWP.Rows[0]["PlanNo"].ToString();
                Load_SurveyVisibles();
            }
            else
            {
                Load_Density();
                Initialize_Stoping_Fields();
            }
        }
        private void Initialize_Stoping_Fields()
        {
            int MaxSeqNo = 0;
            _clsSurvey.theData.ConnectionString = theConnection;
            dtMaxSeqNo = _clsSurvey.get_MaxSeqNo(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Stp");

            if (dtMaxSeqNo.Rows.Count != 0)
                MaxSeqNo = Convert.ToInt32(dtMaxSeqNo.Rows[0]["SeqNo"].ToString());

            MaxSeqNo = MaxSeqNo + 1;
            int i;
            bool _addSeqno = false;
            for (i = 0; i <= cmbSeqNo.Properties.Items.Count - 1; i++)
            {
                if (Convert.ToInt32(cmbSeqNo.Properties.Items[i].ToString()) == MaxSeqNo)
                    _addSeqno = true;
            }
            if (_addSeqno == false)
                cmbSeqNo.Properties.Items.Add(MaxSeqNo.ToString());
            txtSeqNo.Text = MaxSeqNo.ToString();
            cmbSeqNo.Text = MaxSeqNo.ToString();
            cmbSeqNo.SelectedItem = MaxSeqNo;

            txtDip.Text = "0";
            luMiningMethod.EditValue = 1;
            luStopeType.EditValue = 1;
            luDensity.EditValue = "RD";

            cmbMCrew.EditValue = null;
            cmbACrew.EditValue = null;
            cmbECrew.EditValue = null;
            cmbECrew.EditValue = null;
            cmbCleaningCrew.EditValue = null;
            cmbTrammingCrew.EditValue = null;
            cmbHlgeMaintainCrew.EditValue = null;
            cmbRiggerCrew.EditValue = null;
            cmbRseCleaningCrew.EditValue = null;

            txtReefMetres.Text = "0.0";
            txtWasteMetres.Text = "0.0";
            txtMeasWidth.Text = "0.00";
            txtMeasHeight.Text = "0.00";
            txtPlanWidth.Text = "0.00";
            txtPlanHeight.Text = "0.00";
            txtLockUpTons.Text = "0";
            luBlockNo.EditValue = null;
            txtBlockWidth.Text = "0";
            txtBlockValue.Text = "0";
            txtStopeSqm.Text = "0";
            txtStopeSqmOSF.Text = "0";
            txtStopeSqmOS.Text = "0";
            txtStopeSqmTotal.Text = "0";
            txtLedgeSqm.Text = "0";
            txtLedgeSqmOSF.Text = "0";
            txtLedgeSqmOS.Text = "0";
            txtLedgeSqmTotal.Text = "0";
            txtStopeFL.Text = "0";
            txtStopeFLOS.Text = "0";
            txtLedgeFL.Text = "0";
            txtLedgeFLOS.Text = "0";
            txtFLOSTotal.Text = "0";
            txtMeasAdvSW.Text = "0.0";
            txtSWIdeal.Text = "0";
            txtFLTotal.Text = "0";
            txtSqmConvTotal.Text = "0";
            txtSqmOSFTotal.Text = "0";
            txtSqmOSTotal.Text = "0";
            txtSqmTotal.Text = "0";
            luCubicTypes.EditValue = null;
            txtCubicsmetres.Text = "0.0";
            txtCubicsReef.Text = "0";
            txtCubicsWaste.Text = "0";
            txtLabour.Text = "0";
            chkboxPaid.Checked = true;
            txtFW.Text = "0";
            txtCW.Text = "0";
            txtHW.Text = "0";
            txtSWSqm.Text = "0";
            txtSWOSF.Text = "0";
            txtSWOS.Text = "0";
            txtcmgt.Text = "0";
            luCleanTypes.EditValue = null;
            txtCleanSqm.Text = "0";
            txtCleanDist.Text = "0.0";
            txtCleanVamp.Text = "0.0";
            txtCleanTons.Text = "0";
            label94.Visible = true;
            txtCleanTons.Visible = true;
            txtCleangt.Text = "0.00";
            txtCleanContents.Text = "0";
            label5.Visible = false;
            label6.Visible = false;
            txtOGTons.Visible = false;
            txtOGDepth.Visible = false;
            txtOGTons.Text = "0";
            txtOGDepth.Text = "0";
            txtTonsReef.EditValue = "0";
            txtTonsWaste.EditValue = "0";
            txtTonsOSF.EditValue = "0";
            txtTonsOS.EditValue = "0";
            txtTonsTotal.EditValue = "0";
            rdgrpDestination.SelectedIndex = 2;
            rdgrpPayments.EditValue = 3;

            txtPlanNo.Text = "";
            dtPlanCrews = _clsSurvey.get_PlanCrews(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Stp");

            if (dtPlanCrews.Rows.Count != 0)
            {
                cmbMCrew.EditValue = dtPlanCrews.Rows[0]["OrgUnitDay"].ToString();
                cmbACrew.EditValue = dtPlanCrews.Rows[0]["OrgUnitAfternoon"].ToString();
                cmbECrew.EditValue = dtPlanCrews.Rows[0]["OrgUnitNight"].ToString();
            }
            Load_SurveyVisibles();            
        }
        private void Load_SurveyVisibles()
        {
            dtSurveyVisibles = _clsSurvey.get_SurveyVisibles();
            foreach (DataRow row in dtSurveyVisibles.Rows)
            {
                bool blnConfigurationValue = false;
                bool.TryParse(row["Value"].ToString(), out blnConfigurationValue);
                switch (row["Name"].ToString())
                {
                    case "ReefMetres":
                    case "WasteMetres":
                        lblmetres.Visible = blnConfigurationValue;
                        lblReefmetres.Visible = blnConfigurationValue;
                        txtReefMetres.Visible = blnConfigurationValue;
                        lblWastemetres.Visible = blnConfigurationValue;
                        txtWasteMetres.Visible = blnConfigurationValue;
                        break;
                    case "ActualHeight":
                    case "ActualWidth":
                        lblActual.Visible = blnConfigurationValue;
                        lblMeasHeight.Visible = blnConfigurationValue;
                        txtMeasHeight.Visible = blnConfigurationValue;
                        lblMeasWidth.Visible = blnConfigurationValue;
                        txtMeasWidth.Visible = blnConfigurationValue;
                        break;
                    case "IdealHeight":
                    case "IdealWidth":
                        lblIdeal.Visible = blnConfigurationValue;
                        lblPlanHeight.Visible = blnConfigurationValue;
                        txtPlanHeight.Visible = blnConfigurationValue;
                        lblPlanWidth.Visible = blnConfigurationValue;
                        txtPlanWidth.Visible = blnConfigurationValue;
                        break;
                    case "LockupTons":
                        lblLockUpTons.Visible = blnConfigurationValue;
                        txtLockUpTons.Visible = blnConfigurationValue;
                        break;
                    case "MeasurementFaceAdvanceSW":
                        lblMeasured.Visible = blnConfigurationValue;
                        lblFaceAdvSW.Visible = blnConfigurationValue;
                        txtMeasAdvSW.Visible = blnConfigurationValue;
                        break;
                    case "SWIdeal":
                        lblSWIdeal.Visible = blnConfigurationValue;
                        txtSWIdeal.Visible = blnConfigurationValue;
                        break;
                    case "Labour":
                        lblLabour.Visible = blnConfigurationValue;
                        txtLabour.Visible = blnConfigurationValue;
                        break;
                    case "Conv":
                        lblSqmConvTotal.Visible = blnConfigurationValue;
                        txtSqmConvTotal.Visible = blnConfigurationValue;
                        break;
                    default:
                        break;
                }
            }
        }
        public void Validate_Stoping()
        {

            if (Convert.ToInt32(txtSqmTotal.Text) == 0 && Convert.ToDecimal(txtCubicsmetres.Text) == 0 &&
                Convert.ToDecimal(txtReefMetres.Text) == 0 && Convert.ToDecimal(txtWasteMetres.Text) == 0)
            { 
                if (luCleanTypes.EditValue == null)
                {
                    luCleanTypes.Focus();
                    ErrorFound = true;
                    System.Windows.Forms.MessageBox.Show("Please enter Stope or Ledge Sqm or Cubicmetres or a Cleaning Type");
                }
                if (ErrorFound == false)
                {
                    if (luCleanTypes.EditValue.ToString() == "")
                    {
                        luCleanTypes.Focus();
                        ErrorFound = true;
                        System.Windows.Forms.MessageBox.Show("Please enter Stope or Ledge Sqm or Cubicmetres or a Cleaning Type");
                    }
                }
                if (ErrorFound == false)
                {
                    if (luCleanTypes.EditValue.ToString() == "-1")
                    {
                        luCleanTypes.Focus();
                        ErrorFound = true;
                        System.Windows.Forms.MessageBox.Show("Please enter Stope or Ledge Sqm or Cubicmetres or a Cleaning Type");
                    }
                }
            }

            if (ErrorFound == false)
            {
                if ((Convert.ToDecimal(txtTonsReef.EditValue) * Convert.ToDecimal(txtSWSqm.Text) * Convert.ToDecimal(txtcmgt.Text) == 0) &&
                    (Convert.ToDecimal(txtTonsReef.EditValue) != 0))
                {
                    if ((Convert.ToDecimal(txtSqmTotal.Text) - Convert.ToDecimal(txtSqmOSFTotal.Text)) != 0)
                    {
                        ErrorFound = true;
                        txtcmgt.Focus();
                        System.Windows.Forms.MessageBox.Show("You must enter SW and cmgt when there is Reef Tons");
                    }
                }
            }
            if (ErrorFound == false)
            {
                if (((Convert.ToDecimal(txtLedgeSqm.Text) != 0) |
                     (Convert.ToDecimal(txtStopeSqm.Text) != 0)) &&
                    (Convert.ToDecimal(txtcmgt.Text) == 0))
                {
                    ErrorFound = true;
                    txtcmgt.Focus();
                    System.Windows.Forms.MessageBox.Show("You must enter SW and cmgt when there is Stoping or Ledging Square metres");
                }
            }
            if (ErrorFound == false)
            {
                if ((Convert.ToDecimal(txtCubicsReef.Text) * Convert.ToDecimal(txtSWSqm.Text) * Convert.ToDecimal(txtcmgt.Text) == 0) &&
                    (Convert.ToDecimal(txtCubicsReef.Text) != 0))
                {
                    ErrorFound = true;
                    txtcmgt.Focus();
                    System.Windows.Forms.MessageBox.Show("You must enter SW and cmgt when there is Reef Cubics");
                }
            }
            if (ErrorFound == false)
            {
                if ((Convert.ToDecimal(txtSqmTotal.Text) + Convert.ToDecimal(txtCubicsReef.Text) > 0) &&
                    (Convert.ToDecimal(txtSqmTotal.Text) - Convert.ToDecimal(txtSqmOSFTotal.Text) -
                    Convert.ToDecimal(txtSqmOSTotal.Text) != 0))
                {
                    if (Convert.ToDecimal(txtSWSqm.Text) == 0)
                    {
                        ErrorFound = true;
                        txtSWSqm.Focus();
                        System.Windows.Forms.MessageBox.Show("You must enter a SW in this casa");
                    }
                }
            }
            if (ErrorFound == false)
            {
                if (Convert.ToDecimal(txtHW.Text) < 0)
                {
                    ErrorFound = true;
                    txtHW.Focus();
                    System.Windows.Forms.MessageBox.Show("Check Widths - HW is negative and not allowed");
                }
            }
            if (ErrorFound == false)
            {
                if (((Convert.ToDecimal(txtStopeSqm.Text) != 0) |
                     (Convert.ToDecimal(txtLedgeSqm.Text) != 0)) &&
                     (Convert.ToDecimal(txtFLTotal.Text) <= 0))
                {
                    ErrorFound = true;
                    txtFLTotal.Focus();
                    System.Windows.Forms.MessageBox.Show("Please enter a Face Length");
                }
            }
            if (ErrorFound == false)
            {
                if (Convert.ToDecimal(txtCleanSqm.Text) != 0)
                {
                    if (luCleanTypes.EditValue == null)
                    {
                        ErrorFound = true;
                        luCleanTypes.Focus();
                        System.Windows.Forms.MessageBox.Show("Please select a Cleaning Type");
                    }
                    if (ErrorFound == false)
                    {
                        if (luCleanTypes.EditValue.ToString() == "")
                        {
                            ErrorFound = true;
                            luCleanTypes.Focus();
                            System.Windows.Forms.MessageBox.Show("Please select a Cleaning Type");
                        }
                    }
                    if (ErrorFound == false)
                    {
                        if (luCleanTypes.EditValue.ToString() == "-1")
                        {
                            ErrorFound = true;
                            luCleanTypes.Focus();
                            System.Windows.Forms.MessageBox.Show("Please select a Cleaning Type");
                        }
                    }
                }
            }
            if (ErrorFound == false)
            {
                if ((Convert.ToDecimal(txtCubicsmetres.Text) != 0) |
                     (Convert.ToDecimal(txtCubicsReef.Text) != 0) |
                     (Convert.ToDecimal(txtCubicsWaste.Text) != 0))
                {
                    if (luCubicTypes.EditValue == null)
                    {
                        ErrorFound = true;
                        luCubicTypes.Focus();
                        System.Windows.Forms.MessageBox.Show("Please select Extras");
                    }
                    if (ErrorFound == false)
                    {
                        if (luCubicTypes.EditValue.ToString() == "")
                        {
                            ErrorFound = true;
                            luCubicTypes.Focus();
                            System.Windows.Forms.MessageBox.Show("Please select Extras");
                        }
                    }
                    if (ErrorFound == false)
                    {
                        if (luCubicTypes.EditValue.ToString() == "-1")
                        {
                            ErrorFound = true;
                            luCubicTypes.Focus();
                            System.Windows.Forms.MessageBox.Show("Please select Extras");
                        }
                    }
                }
            }
            if (ErrorFound == false)
            {
                if (cmbMCrew.EditValue == null)
                {
                    ErrorFound = true;
                    cmbMCrew.Focus();
                    System.Windows.Forms.MessageBox.Show("Please select a Day Crew");
                }
                if (ErrorFound == false)
                {
                    if (cmbMCrew.EditValue.ToString() == "")
                    {
                        ErrorFound = true;
                        cmbMCrew.Focus();
                        System.Windows.Forms.MessageBox.Show("Please select a Day Crew");
                    }
                }
                if (ErrorFound == false)
                {
                    if (cmbMCrew.EditValue.ToString() == "-1")
                    {
                        ErrorFound = true;
                        cmbMCrew.Focus();
                        System.Windows.Forms.MessageBox.Show("Please select a Day Crew");
                    }
                }
            }
            if (ErrorFound == false)
            {
                SType = luMiningMethod.EditValue.ToString();
                //if (luMiningMethod.EditValue.ToString() == "C")
                //{
                //    SType = "C";
                //}
                //if (luMiningMethod.EditValue.ToString() == 1)
                //{
                //    SType = "T";
                //}
                //if (cmbMiningMethod.SelectedIndex == 2)
                //{
                //    SType = "P";
                //}
                //if (cmbMiningMethod.SelectedIndex == 3)
                //{
                //    SType = "ST";
                //}
            }
        }
        public void Save_SurveyStoping()
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtTheWP = _clsSurvey.find_TheWP(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, txtSeqNo.Text, "Stp");

            decimal TheGrams = 0;
            if (Convert.ToInt32(txtSWSqm.Text) > 0)
            {

                TheGrams = (((Convert.ToDecimal(txtSqmTotal.Text) -
                                Convert.ToDecimal(txtSqmOSFTotal.Text) -
                                Convert.ToDecimal(txtSqmOSTotal.Text)) *
                                Convert.ToDecimal(txtcmgt.Text) *
                                Convert.ToDecimal(TheDens) / Convert.ToDecimal(100)) +
                            (Convert.ToDecimal(txtCubicsReef.Text) *
                                Convert.ToDecimal(txtcmgt.Text) /
                                Convert.ToDecimal(txtSWSqm.Text) *
                                Convert.ToDecimal(TheDens)));
            }
            else
            {
                TheGrams = ((Convert.ToDecimal(txtSqmTotal.Text) -
                                                    Convert.ToDecimal(txtSqmOSFTotal.Text) -
                                                    Convert.ToDecimal(txtSqmOSTotal.Text)) *
                                                    Convert.ToDecimal(txtcmgt.Text) *
                                                    Convert.ToDecimal(TheDens) / Convert.ToDecimal(100));
            }

            if (dtTheWP.Rows.Count == 0)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = theConnection;
                _dbMan.SqlStatement = "insert into Survey ( \r\n  " +
                                        "  WorkplaceID, SectionID, Activity, CalendarDate, ProdMonth, SeqNo, \r\n " +
                                        "  Dip, MineMethod, Density, \r\n " +
                                        "  SType, CrewMorning, CrewAfternoon, CrewEvening,CleaningCrew,TrammingCrew,HlgeMaintainanceCrew,RiggerCrew,RseCleaningCrew, \r\n " +
                                        "  ReefMetres, Wastemetres, \r\n " +
                                        "  MeasWidth, MeasHeight, \r\n " +
                                        "  PlanWidth, PlanHeight, \r\n " +
                                        "  LockUpTons, \r\n " +
                                        "  Blockno, BlockWidth, BlockValue, \r\n " +
                                        "  StopeSqm, StopeSqmOSF, StopeSqmOS, StopeSqmTotal, \r\n " +
                                        "  LedgeSqm, LedgeSqmOSF, LedgeSqmOS, LedgeSqmTotal, \r\n " +
                                        "  StopeFL, StopeFLOS, LedgeFL, LedgeFLOS, FLOSTotal, \r\n " +
                                        "  MeasAdvSW, SWIdeal, \r\n " +
                                        "  FLTotal, SqmConvTotal, SqmOSFTotal, SqmOSTotal, SqmTotal, \r\n " +
                                        "  ExtraType, \r\n " +
                                        "  Cubicsmetres,CubicsReef, CubicsWaste, Labour, PaidUnpaid, \r\n " +
                                        "  FW, CW, HW, SWSqm, SWOSF, SWOS, cmgt, \r\n " +
                                        "  Destination, OreflowID, Cleantype, CleanSqm, CleanDist, CleanVamp, \r\n " +
                                        "  CleanTons, Cleangt, CleanContents, \r\n " +
                                        "  TonsReef, TonsWaste, TonsOSF, TonsOS, TonsTotal, \r\n " +
                                        "  CleanDepth," +
                                        "  Payment, TotalContent, PlanNo) \r\n " +
                                        " values( \r\n " +
                                        "  '" + lblWorkplaceID.Text + "', '" + lblContractor.Text + "', 0, \r\n " +
                                        "  '" + String.Format("{0:yyyy-MM-dd}", CalendarDate.Value) + "', \r\n " +
                                        "  '" + lblProdMonth.Text + "', '" + txtSeqNo.Text + "',  '" + txtDip.Text + "', \r\n ";
                if (luMiningMethod.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (luMiningMethod.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + luMiningMethod.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement + "  " + TheDens + ", \r\n ";
                if (luStopeType.EditValue == null)
                    _dbMan.SqlStatement += " null, \r\n ";
                else
                {
                    if (luStopeType.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + luStopeType.EditValue.ToString() + "', \r\n ";
                }
                if (cmbMCrew.EditValue == null)
                    _dbMan.SqlStatement += " null, \r\n ";
                else
                {
                    if (cmbMCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + cmbMCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbACrew.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (cmbACrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + cmbACrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbECrew.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (cmbECrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + cmbECrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbCleaningCrew.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (cmbCleaningCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + cmbCleaningCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbTrammingCrew.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (cmbTrammingCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + cmbTrammingCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbHlgeMaintainCrew.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (cmbHlgeMaintainCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + cmbHlgeMaintainCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbRiggerCrew.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (cmbRiggerCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + cmbRiggerCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbRseCleaningCrew.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (cmbRseCleaningCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + cmbRseCleaningCrew.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  '" + txtReefMetres.Text + "', '" + txtWasteMetres.Text + "', \r\n " +
                                        "  '" + txtMeasWidth.Text + "', '" + txtMeasHeight.Text + "', \r\n " +
                                        "  '" + txtPlanWidth.Text + "', '" + txtPlanHeight.Text + "', \r\n " +
                                        "  '" + txtLockUpTons.Text + "', \r\n ";
                if (luBlockNo.EditValue == null)
                    _dbMan.SqlStatement += " null, \r\n ";
                else
                {
                    if (luBlockNo.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + luBlockNo.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  '" + txtBlockWidth.Text + "', '" + txtBlockValue.Text + "', \r\n " +
                                        "  '" + txtStopeSqm.Text + "', '" + txtStopeSqmOSF.Text + "', '" + txtStopeSqmOS.Text + "', '" + txtStopeSqmTotal.Text + "', \r\n " +
                                        "  '" + txtLedgeSqm.Text + "', '" + txtLedgeSqmOSF.Text + "',  '" + txtLedgeSqmOS.Text + "',  '" + txtLedgeSqmTotal.Text + "', \r\n " +
                                        "  '" + txtStopeFL.Text + "', '" + txtStopeFLOS.Text + "', '" + txtLedgeFL.Text + "', '" + txtLedgeFLOS.Text + "', '" + txtFLOSTotal.Text + "', \r\n " +
                                        "  '" + txtMeasAdvSW.Text + "', '" + txtSWIdeal.Text + "', \r\n " +
                                        "  '" + txtFLTotal.Text + "', '" + txtSqmConvTotal.Text + "', '" + txtSqmOSFTotal.Text + "', \r\n " +
                                        "  '" + txtSqmOSTotal.Text + "', '" + txtSqmTotal.Text + "', \r\n ";
                if (luCubicTypes.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (luCubicTypes.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + luCubicTypes.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  '" + txtCubicsmetres.Text + "','" + txtCubicsReef.Text + "', '" + txtCubicsWaste.Text + "', '" + txtLabour.Text + "', \r\n ";
                if (chkboxPaid.Checked == true)
                {
                    _dbMan.SqlStatement += " 'Y' \r\n ,";
                }
                else
                {
                    _dbMan.SqlStatement += " 'N', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  '" + txtFW.Text + "', '" + txtCW.Text + "', '" + txtHW.Text + "', '" + txtSWSqm.Text + "', '" + txtSWOSF.Text + "', '" + txtSWOS.Text + "', '" + txtcmgt.Text + "', \r\n ";
                _dbMan.SqlStatement += " '" + rdgrpDestination.SelectedIndex + "', \r\n " +
                    " '" + luOreflowID.EditValue.ToString() + "', \r\n ";
                if (luCleanTypes.EditValue == null)
                    _dbMan.SqlStatement += "null, \r\n ";
                else
                {
                    if (luCleanTypes.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                        _dbMan.SqlStatement += " '" + luCleanTypes.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  '" + txtCleanSqm.Text + "', '" + txtCleanDist.Text + "','" + txtCleanVamp.Text + "', \r\n ";
                if (luCleanTypes.EditValue == null)
                {
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "  null, ";
                }
                else
                { 
                    if (Convert.ToInt32(luCleanTypes.EditValue.ToString()) == 13)
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "  '" + txtOGTons.Text + "', ";
                    else
                        _dbMan.SqlStatement = _dbMan.SqlStatement + "  '" + txtCleanTons.Text + "', ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  '" + txtCleangt.Text + "', '" + txtCleanContents.Text + "', \r\n " +
                                        "  '" + txtTonsReef.EditValue + "', '" + txtTonsWaste.EditValue + "', '" + txtTonsOSF.EditValue + "', '" + txtTonsOS.EditValue + "', '" + txtTonsTotal.EditValue + "', \r\n " +
                                        "  '" + txtOGDepth.Text + "', \r\n ";
                _dbMan.SqlStatement += " '" + rdgrpPayments.EditValue + "', \r\n ";

                _dbMan.SqlStatement += " '" + TheGrams + "', '" + txtPlanNo.Text + "') ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
                _dbMan.ExecuteInstruction();

                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Survey Added", Color.CornflowerBlue);
            }
            else
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = theConnection;
                _dbMan.SqlStatement = "Update Survey set \r\n " +
                                        "  Dip = '" + txtDip.Text + "', \r\n ";
                if (luMiningMethod.EditValue == null)
                    _dbMan.SqlStatement += " MineMethod = -1, \r\n ";
                else
                {
                    if (luMiningMethod.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " MineMethod = -1, \r\n ";
                    else
                        _dbMan.SqlStatement += " MineMethod = '" + luMiningMethod.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement += "  Density = " + TheDens + ", \r\n ";
                if (luStopeType.EditValue == null)
                    _dbMan.SqlStatement += " SType = 1, \r\n ";
                else
                {
                    if (luStopeType.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " SType = 1, \r\n ";
                    else
                        _dbMan.SqlStatement += " SType = '" + luStopeType.EditValue.ToString() + "', \r\n ";
                }
                if (cmbMCrew.EditValue == null)
                    _dbMan.SqlStatement += " CrewMorning = null, \r\n ";
                else
                {
                    if (cmbMCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " CrewMorning = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " CrewMorning = '" + cmbMCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbACrew.EditValue == null)
                    _dbMan.SqlStatement += " CrewAfternoon = null, \r\n ";
                else
                {
                    if (cmbACrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " CrewAfternoon = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " CrewAfternoon = '" + cmbACrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbECrew.EditValue == null)
                    _dbMan.SqlStatement += " CrewEvening = null, \r\n ";
                else
                {
                    if (cmbECrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " CrewEvening = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " CrewEvening = '" + cmbECrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbCleaningCrew.EditValue == null)
                    _dbMan.SqlStatement += " CleaningCrew = null, \r\n ";
                else
                {
                    if (cmbCleaningCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " CleaningCrew = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " CleaningCrew = '" + cmbCleaningCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbTrammingCrew.EditValue == null)
                    _dbMan.SqlStatement += " TrammingCrew = null, \r\n ";
                else
                {
                    if (cmbTrammingCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " TrammingCrew = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " TrammingCrew = '" + cmbTrammingCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbHlgeMaintainCrew.EditValue == null)
                    _dbMan.SqlStatement += " HlgeMaintainanceCrew = null, \r\n ";
                else
                {
                    if (cmbHlgeMaintainCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " HlgeMaintainanceCrew = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " HlgeMaintainanceCrew = '" + cmbHlgeMaintainCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbRiggerCrew.EditValue == null)
                    _dbMan.SqlStatement += " RiggerCrew = null, \r\n ";
                else
                {
                    if (cmbRiggerCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " RiggerCrew = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " RiggerCrew = '" + cmbRiggerCrew.EditValue.ToString() + "', \r\n ";
                }
                if (cmbRseCleaningCrew.EditValue == null)
                    _dbMan.SqlStatement += " RseCleaningCrew = null, \r\n ";
                else
                {
                    if (cmbRseCleaningCrew.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " RseCleaningCrew = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " RseCleaningCrew = '" + cmbRseCleaningCrew.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  Reefmetres = '" + txtReefMetres.Text + "', \r\n " +
                                        "  Wastemetres = '" + txtWasteMetres.Text + "', \r\n " +
                                        "  MeasWidth = '" + txtMeasWidth.Text + "', \r\n " +
                                        "  MeasHeight = '" + txtMeasHeight.Text + "', \r\n " +
                                        "  PlanWidth =  '" + txtPlanWidth.Text + "', \r\n " +
                                        "  PlanHeight = '" + txtPlanHeight.Text + "', \r\n " +
                                        "  LockUpTons = '" + txtLockUpTons.Text + "', \r\n ";
                if (luBlockNo.EditValue == null)
                    _dbMan.SqlStatement += " Blockno = null, \r\n ";
                else
                {
                    if (luBlockNo.EditValue.ToString() == "")
                        _dbMan.SqlStatement += " Blockno = null, \r\n ";
                    else
                        _dbMan.SqlStatement += " Blockno = '" + luBlockNo.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  BlockWidth = '" + txtBlockWidth.Text + "', \r\n " +
                                        "  BlockValue = '" + txtBlockValue.Text + "', \r\n " +
                                        "  StopeSqm = '" + txtStopeSqm.Text + "', \r\n " +
                                        "  StopeSqmOSF = '" + txtStopeSqmOSF.Text + "', \r\n " +
                                        "  StopeSqmOS = '" + txtStopeSqmOS.Text + "', \r\n " +
                                        "  StopeSqmTotal = '" + txtStopeSqmTotal.Text + "', \r\n " +
                                        "  LedgeSqm = '" + txtLedgeSqm.Text + "', \r\n " +
                                        "  LedgeSqmOSF = '" + txtLedgeSqmOSF.Text + "', \r\n " +
                                        "  LedgeSqmOS = '" + txtLedgeSqmOS.Text + "', \r\n " +
                                        "  LedgeSqmTotal = '" + txtLedgeSqmTotal.Text + "', \r\n " +
                                        "  StopeFL = '" + txtStopeFL.Text + "', \r\n " +
                                        "  StopeFLOS = '" + txtStopeFLOS.Text + "', \r\n " +
                                        "  LedgeFL = '" + txtLedgeFL.Text + "', \r\n " +
                                        "  LedgeFLOS = '" + txtLedgeFLOS.Text + "', \r\n " +
                                        "  FLOSTotal = '" + txtFLOSTotal.Text + "', \r\n " +
                                        "  MeasAdvSW = '" + txtMeasAdvSW.Text + "', \r\n " +
                                        "  SWIdeal = '" + txtSWIdeal.Text + "', \r\n " +
                                        "  FLTotal = '" + txtFLTotal.Text + "', \r\n " +
                                        "  SqmConvTotal = '" + txtSqmConvTotal.Text + "', \r\n " +
                                        "  SqmOSFTotal = '" + txtSqmOSFTotal.Text + "', \r\n " +
                                        "  SqmOSTotal = '" + txtSqmOSTotal.Text + "', \r\n " +
                                        "  SqmTotal = '" + txtSqmTotal.Text + "', \r\n ";

                if (luCubicTypes.EditValue == null)
                    _dbMan.SqlStatement += "  ExtraType = null, \r\n ";
                else
                {
                    if (luCubicTypes.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "  ExtraType = null, \r\n ";
                    else
                        _dbMan.SqlStatement += "  ExtraType =  '" + luCubicTypes.EditValue.ToString() + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  Cubicsmetres = '" + txtCubicsmetres.Text + "', \r\n " +
                                        "  CubicsReef = '" + txtCubicsReef.Text + "', \r\n " +
                                        "  CubicsWaste = '" + txtCubicsWaste.Text + "', \r\n " +
                                        "  Labour = '" + txtLabour.Text + "', \r\n ";
                if (chkboxPaid.Checked == true)
                {
                    _dbMan.SqlStatement += " PaidUnpaid = 'Y', \r\n ";
                }
                else
                {
                    _dbMan.SqlStatement += " PaidUnpaid = 'N', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  FW = '" + txtFW.Text + "', \r\n " +
                                        "  CW = '" + txtCW.Text + "', \r\n " +
                                        "  HW = '" + txtHW.Text + "', \r\n  " +
                                        "  SWSqm = '" + txtSWSqm.Text + "', \r\n " +
                                        "  SWOSF = '" + txtSWOSF.Text + "', \r\n " +
                                        "  SWOS = '" + txtSWOS.Text + "', \r\n " +
                                        "  cmgt = '" + txtcmgt.Text + "', \r\n " +
                                        "  TotalContent = '" + TheGrams + "', \r\n ";
                if (luCleanTypes.EditValue != null)
                {
                    if (luCleanTypes.EditValue.ToString() == "13")
                        _dbMan.SqlStatement = _dbMan.SqlStatement +
                                            "  CleanTons = '" + txtOGTons.Text + "', \r\n ";
                    else
                        _dbMan.SqlStatement = _dbMan.SqlStatement +
                            "  CleanTons = '" + txtCleanTons.Text + "', \r\n ";
                }
                else
                    _dbMan.SqlStatement = _dbMan.SqlStatement +
                        "  CleanTons = '" + txtCleanTons.Text + "', \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  CleanDepth = '" + txtOGDepth.Text + "', \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " Destination = '" + rdgrpDestination.SelectedIndex + "', \r\n " +
                    " OreflowID = '" + luOreflowID.EditValue.ToString() + "', \r\n ";

                if (luCleanTypes.EditValue == null)
                    _dbMan.SqlStatement += "  Cleantype =  null, \r\n ";
                else
                {
                    if (luCleanTypes.EditValue.ToString() == "")
                        _dbMan.SqlStatement += "  Cleantype =  null, \r\n ";
                    else
                        _dbMan.SqlStatement += " Cleantype ='" + luCleanTypes.EditValue + "', \r\n ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement +
                            "  CleanSqm = '" + txtCleanSqm.Text + "', \r\n " +
                            "  CleanDist = '" + txtCleanDist.Text + "', \r\n " +
                            "  CleanVamp = '" + txtCleanVamp.Text + "', \r\n " +
                            "  Cleangt = '" + txtCleangt.Text + "', \r\n " +
                            "  CleanContents = '" + txtCleanContents.Text + "', \r\n " +
                            "  TonsReef = '" + txtTonsReef.EditValue + "', \r\n " +
                            "  TonsWaste = '" + txtTonsWaste.EditValue + "', \r\n " +
                            "  TonsOSF = '" + txtTonsOSF.EditValue + "', \r\n " +
                            "  TonsOS = '" + txtTonsOS.EditValue + "', \r\n " +
                            "  TonsTotal = '" + txtTonsTotal.EditValue + "', \r\n ";
                // "  Payment, 
                _dbMan.SqlStatement += " Payment ='" + rdgrpPayments.EditValue.ToString() + "', \r\n ";

                _dbMan.SqlStatement = _dbMan.SqlStatement +
                                        "  PlanNo = '" + txtPlanNo.Text + "' \r\n " +
                                        "where ProdMonth = '" + lblProdMonth.Text + "'and \r\n  " +
                                        "      SECTIONID = '" + lblContractor.Text + "' and \r\n " +
                                        "      WorkplaceID = '" + lblWorkplaceID.Text + "' and \r\n " +
                                        "      Activity IN(0,9) and \r\n  " +
                                        "      SeqNo = '" + txtSeqNo.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
                _dbMan.ExecuteInstruction();
                clsDataResult errorMsg = _dbMan.ExecuteInstruction();
                if (errorMsg.success == false)
                {
                    System.Windows.Forms.MessageBox.Show(errorMsg.Message);
                   // _sysMessagesClass.viewMessage(MessageType.Error, "SQL ERROR", resWPAS.systemTag, "clsSurvey", "get_CleanTypes", errorMsg.Message, ButtonTypes.OK, MessageDisplayType.FullScreen);
                    //return theData.ResultsDataTable;
                }
                else
                    //return theData.ResultsDataTable;

                Global.sysNotification.TsysNotification.showNotification("Data Saved", "Survey Updated", Color.CornflowerBlue);
            }
        }
        public void Calc_StopeSqmTotal()
        {
            txtStopeSqmTotal.Text = string.Format("{0:0}", Convert.ToDecimal(txtStopeSqm.Text) + Convert.ToDecimal(txtStopeSqmOSF.Text) +
                                                     Convert.ToDecimal(txtStopeSqmOS.Text));
        }

        public void Calc_SqmTotal()
        {
            txtSqmTotal.Text = string.Format("{0:0}", Convert.ToDecimal(txtStopeSqm.Text) + Convert.ToDecimal(txtLedgeSqm.Text) +
                                                Convert.ToDecimal(txtSqmConvTotal.Text) + Convert.ToDecimal(txtSqmOSFTotal.Text) +
                                                Convert.ToDecimal(txtSqmOSTotal.Text));
        }
        public void Calc_SqmConv()
        {
            txtSqmConvTotal.Text = string.Format("{0:0}", Convert.ToDecimal(txtReefMetres.Text) *
                                                    Convert.ToDecimal(txtMeasWidth.Text));
        }
        public void Calc_LedgeSqmTotal()
        {
            txtLedgeSqmTotal.Text = string.Format("{0:0}", Convert.ToDecimal(txtLedgeSqm.Text) + Convert.ToDecimal(txtLedgeSqmOSF.Text) +
                                                     Convert.ToDecimal(txtLedgeSqmOS.Text));
        }
        public void Calc_FLOSTotal()
        {
            txtFLOSTotal.Text = string.Format("{0:0}", Convert.ToDecimal(txtStopeFLOS.Text) + Convert.ToDecimal(txtLedgeFLOS.Text));
        }

        public void Calc_FLTotal()
        {
            txtFLTotal.Text = string.Format("{0:0}", Convert.ToDecimal(txtFLOSTotal.Text) + Convert.ToDecimal(txtStopeFL.Text) +
                                               Convert.ToDecimal(txtLedgeFL.Text));
        }
        private void Calc_SqmOSFTotal()
        {
            txtSqmOSFTotal.Text = string.Format("{0:0}", Convert.ToDecimal(txtStopeSqmOSF.Text) + Convert.ToDecimal(txtLedgeSqmOSF.Text));
        }

        private void Calc_SqmOSTotal()
        {
            txtSqmOSTotal.Text = string.Format("{0:0}", Convert.ToDecimal(txtStopeSqmOS.Text) + Convert.ToDecimal(txtLedgeSqmOS.Text));
        }

        private void Calc_HangWall()
        {
            txtHW.Text = string.Format("{0:0}", Convert.ToDecimal(txtSWSqm.Text) - Convert.ToDecimal(txtFW.Text) -
                                                Convert.ToDecimal(txtCW.Text));
        }
        private void Calc_Tons()
        {
            TheDens = 0;
            if (luDensity.EditValue != null)
            {

                DataRow[] EditedRow = dtDensity.Select(" TheType = '" + luDensity.EditValue.ToString() + "' ");
                foreach (DataRow dr in EditedRow)
                {
                    TheDens = Convert.ToDecimal(dr["Density"].ToString());
                }
            }
            decimal x = 0;
            decimal y = 0;
            txtTonsOSF.EditValue = Convert.ToDecimal(Convert.ToDecimal(txtSWOSF.Text) / 100 * (Convert.ToDecimal(txtStopeSqmOSF.Text) + Convert.ToDecimal(txtLedgeSqmOSF.Text)) * TheDens).ToString("F2");

            txtTonsOS.EditValue = Convert.ToDecimal(Convert.ToDecimal(txtSWOS.Text) / 100 * (Convert.ToDecimal(txtStopeSqmOS.Text) + Convert.ToDecimal(txtLedgeSqmOS.Text)) * TheDens).ToString("F2");

            if ((Convert.ToDecimal(txtMeasHeight.Text) * 100) > (Convert.ToDecimal(txtSWSqm.Text)))
            {
                x = (Convert.ToDecimal(txtStopeSqm.Text) + Convert.ToDecimal(txtLedgeSqm.Text)) *
                     Convert.ToDecimal(txtSWSqm.Text) / 100 * TheDens;
                x = x + (Convert.ToDecimal(txtReefMetres.Text) * Convert.ToDecimal(txtSWSqm.Text) / 100 *
                         Convert.ToDecimal(txtMeasWidth.Text) * TheDens);
                x = x + (Convert.ToDecimal(txtCubicsReef.Text) * TheDens);
                y = (Convert.ToDecimal(txtWasteMetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                     Convert.ToDecimal(txtMeasWidth.Text) *
                     TheDens);
                y = y + (Convert.ToDecimal(txtCubicsWaste.Text) * TheDens);
                y = y + (Convert.ToDecimal(txtReefMetres.Text) *
                        (Convert.ToDecimal(txtMeasHeight.Text) - (Convert.ToDecimal(txtSWSqm.Text) / 100)) *
                         Convert.ToDecimal(txtMeasWidth.Text) * TheDens);
            }
            else
            {
                x = (Convert.ToDecimal(txtStopeSqm.Text) + Convert.ToDecimal(txtLedgeSqm.Text)) *
                     Convert.ToDecimal(txtSWSqm.Text) / 100 * TheDens;
                x = x + (Convert.ToDecimal(txtReefMetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                         Convert.ToDecimal(txtMeasWidth.Text) * TheDens);
                x = x + (Convert.ToDecimal(txtCubicsReef.Text) * TheDens);
                y = (Convert.ToDecimal(txtWasteMetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                      Convert.ToDecimal(txtMeasWidth.Text) * TheDens);
                y = y + (Convert.ToDecimal(txtReefMetres.Text) *
                        ((Convert.ToDecimal(txtSWSqm.Text) / 100) - Convert.ToDecimal(txtMeasHeight.Text)) *
                         Convert.ToDecimal(txtMeasWidth.Text) * TheDens);
                y = y + (Convert.ToDecimal(txtCubicsWaste.Text) * TheDens);
            }

            txtTonsReef.EditValue = x.ToString("F2");
            txtTonsWaste.EditValue = y.ToString("F2");
            txtTonsTotal.EditValue = Convert.ToDecimal(x + y + Convert.ToDecimal(txtTonsOSF.EditValue) + Convert.ToDecimal(txtTonsOS.EditValue)).ToString("F2");
        }

        private void Calc_Content()
        {
            txtCleanContents.Text = string.Format("{0:0}", Convert.ToDecimal(txtOGTons.Text) * Convert.ToDecimal(txtCleangt.Text));
        }

        private void Calc_Depth()
        {
            if (Convert.ToDecimal(txtCleanSqm.Text) > 0)
                txtOGDepth.Text = string.Format("{0:0}", Convert.ToDecimal(txtOGTons.Text) / Convert.ToDecimal(txtCleanSqm.Text) * TheDens);
        }

        public void Calc_OSSW()
        {
            if ((Convert.ToDecimal(txtStopeSqmOS.Text) > 0 || Convert.ToDecimal(txtLedgeSqmOS.Text) > 0) && Convert.ToDecimal(txtSWOS.Text) == 0)
                txtSWOS.Text = string.Format("{0:0}", Convert.ToDecimal(txtSWSqm.Text));
        }

        private void Calc_OSFSW()
        {
            // if ((Convert.ToDecimal(txtStopeSqmOSF.Text) > 0 || Convert.ToDecimal(txtLedgeSqmOSF.Text) > 0) && Convert.ToDecimal(txtSWOSF.Text) == 0)
            //    txtSWOSF.Text = string.Format("{0:0}", Convert.ToDecimal(txtSWSqm.Text));
        }
        private void txtStopeSqm_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_StopeSqmTotal();
            Calc_SqmTotal();
            Calc_Tons();
        }
        private void txtStopeSqmOSF_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_SqmOSFTotal();
            Calc_StopeSqmTotal();
            Calc_SqmTotal();
            Calc_Tons();
            Calc_OSFSW();
        }     
        private void txtStopeSqmOS_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_SqmOSTotal();
            Calc_StopeSqmTotal();
            Calc_SqmTotal();
            Calc_Tons();
            Calc_OSSW();
        }
        private void txtLedgeSqm_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_LedgeSqmTotal();
            Calc_SqmTotal();
            Calc_Tons();
        }
        private void txtLedgeSqmOSF_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_SqmOSFTotal();
            Calc_LedgeSqmTotal();
            Calc_SqmTotal();
            Calc_Tons();
            Calc_OSFSW();
        }
        private void txtLedgeSqmOS_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_SqmOSTotal();
            Calc_LedgeSqmTotal();
            Calc_SqmTotal();
            Calc_Tons();
            Calc_OSSW();
        }
        private void txtStopeFL_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_FLTotal();
        }
        private void txtStopeFLOS_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_FLOSTotal();
            Calc_FLTotal();
            Calc_Tons();
        }
        private void txtReefMetres_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_SqmConv();
            Calc_SqmTotal();
            Calc_HangWall();
            Calc_Tons();
        }
        private void txtFW_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_HangWall();
        }
        private void txtSWSqm_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_SqmConv();
            Calc_SqmTotal();
            Calc_HangWall();
            Calc_Tons();
        }
        private void txtSWSqm_Leave(object sender, EventArgs e)
        {
            Calc_OSSW();
            Calc_OSFSW();
        }
        private void txtOGTons_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_Depth();
            Calc_Content();
        }

        private void txtCleanSqm_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_Depth();
        }
        private void rdgrpPayments_EditValueChanged(object sender, EventArgs e)
        {
            luOreflowID.EditValue = rdgrpDestination.EditValue;
        }

        private void cmbSeqNo_EditValueChanged(object sender, EventArgs e)
        {
            txtSeqNo.Text = cmbSeqNo.SelectedItem.ToString();
            Load_SurveyStoping();
        }

        private void luCleanTypes_EditValueChanged(object sender, EventArgs e)
        {
            if (luCleanTypes.EditValue == null)
            {
                txtOGTons.Visible = false;
                txtOGDepth.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label94.Visible = true;
                txtCleanTons.Visible = true;
            }
            else
            {
                if (luCleanTypes.EditValue.ToString() == "")
                {
                    txtOGTons.Visible = false;
                    txtOGDepth.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label94.Visible = true;
                    txtCleanTons.Visible = true;
                }
                else
                {
                    if (Convert.ToInt32(luCleanTypes.EditValue.ToString()) == 13)
                    {
                        txtOGTons.Visible = true;
                        txtOGDepth.Visible = true;
                        label5.Visible = true;
                        label6.Visible = true;
                        label94.Visible = false;
                        txtCleanTons.Visible = false;
                    }
                    else
                    {
                        txtOGTons.Visible = false;
                        txtOGDepth.Visible = false;
                        label5.Visible = false;
                        label6.Visible = false;
                        label94.Visible = true;
                        txtCleanTons.Visible = true;
                    }
                }
            }
        }

        private void rdgrpDestination_EditValueChanged(object sender, EventArgs e)
        {
            luOreflowID.EditValue = rdgrpDestination.EditValue;
        }

        private void luDensity_EditValueChanged(object sender, EventArgs e)
        {
            TheDens = 0;
            if (luDensity.EditValue != null)
            {

                DataRow[] EditedRow = dtDensity.Select(" TheType = '" + luDensity.EditValue.ToString()+"' ");
                foreach (DataRow dr in EditedRow)
                {
                    TheDens = Convert.ToDecimal(dr["Density"].ToString());
                }
            }
            Calc_Tons();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void ucSurveyStp_Load(object sender, EventArgs e)
        {

        }

        private void txtCleanContents_KeyDown(object sender, KeyEventArgs e)
        {
           
            if ((e.KeyCode.ToString() == "Tab"))
            {
                luCubicTypes.Focus();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
