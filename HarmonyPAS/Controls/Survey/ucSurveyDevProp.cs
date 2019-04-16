using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mineware.Systems.Global;
using DevExpress.XtraEditors.Controls;

namespace Mineware.Systems.HarmonyPAS.Controls.Survey
{
    public partial class ucSurveyDevProp : ucBaseUserControl
    {
        clsSurvey _clsSurvey = new clsSurvey();

        private DataTable dtSysset;
        private DataTable dtContractors;
        private DataTable dtMineMethods;
        private DataTable dtIndicators;
        private DataTable dtCrewsM;
        private DataTable dtCrewsA;
        private DataTable dtCrewsE;
        private DataTable dtCrewsClean;
        private DataTable dtCrewsTram;
        private DataTable dtCrewsHlge;
        private DataTable dtCrewsRig;
        private DataTable dtCrewsRse;
        private DataTable dtPegNumbers;
        private DataTable dtCubicTypes;
        private DataTable dtCleanTypes;
        private DataTable dtEntries;
        private DataTable dtDestinations;
        private DataTable dtDensity;
        private DataTable dtCheckLockStatus;
        private DataTable dtSurveyDev;
        private DataTable dtMaxSeqNo;
        private DataTable dtEndTypes;
        private DataTable dtPlanCrews;
        private DataTable dtPlanDates;

        public string Workplace;
        public string WorkplaceID;
        public string TheWP;
        public string ProdMonth;
        public string TheSection;
        public bool ErrorFound;
        public int MaxSeqNo;
        public bool FromMtrs;
        public decimal TheDens;
        public decimal TheBrokenRockDens;
        private decimal TonsCubicsReef;
        private decimal TonsCubicsWaste;
        private decimal TonsReefBroken;
        private decimal TonsWasteBroken;
        private decimal TonsReefTotal;
        private decimal TonsWasteTotal;
        private decimal TonsTotal;
        private decimal TonsBallast;
        private decimal TonsTrammed;

        private string _prodmonth;
        public ucSurveyDevProp()
        {
            InitializeComponent();
        }

        private void ucSurveyDevProp_Load(object sender, EventArgs e)
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _prodmonth = String.Format("{0:yyyyMM}", DateTime.Now);

            dtSysset = _clsSurvey.get_Sysset();
            if (dtSysset.Rows.Count > 0)
            {
                _prodmonth = dtSysset.Rows[0]["CurrentProductionMonth"].ToString();
            }
            lblProdMonth.Text = _prodmonth;
            lblWorkplaceID.Text = WorkplaceID;
            lblWorkplaceName.Text = Workplace;
            lblContractor.Text = TheSection;
            Load_Contractor();
            Load_MineMethod();
            Load_Indicators();
            Load_Crews();
            Load_PegNumbers();
            Load_CubicTypes();
            Load_CleanTypes();// Added - Shaista Anjum Load Clean Types for Survey Development - 13/FEB/2013
            Load_Entries();
            Load_Destination();
            Load_SurveyDevelopment();
            Load_Density();
            CheckLockedStatus();
        }
        private void Load_Contractor()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtContractors = _clsSurvey.get_Contractors("201703", "aa");

            if (dtContractors.Rows.Count != 0)
            {
                lblSection.Text = dtContractors.Rows[0]["SectionID"].ToString();
            }
        }

        private void Load_MineMethod()
        {
            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtMineMethods = _clsSurvey.get_MineMethods();
                cmbMiningMethod.DataSource = dtMineMethods;
                cmbMiningMethod.DisplayMember = "MethodDesc";
                cmbMiningMethod.ValueMember = "MethodID";
                cmbMiningMethod.SelectedIndex = 1;
            }
            catch { }
        }

        private void Load_Indicators()
        {
            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtIndicators = _clsSurvey.get_Indicators();

                cmbIndicator.DataSource = dtIndicators;
                cmbIndicator.DisplayMember = "IndicatorDesc";
                cmbIndicator.ValueMember = "IndicatorID";
                cmbIndicator.SelectedIndex = 1;
            }
            catch { }
        }


        private void Load_Crews()
        {
            try
            {
                //_clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //dtCrewsM = _clsSurvey.get_Crews(lblSection.Text);

                //cmbMCrew.Properties.DataSource = dtCrewsM;
                //cmbMCrew.Properties.DisplayMember = "CrewMorning";
                //cmbMCrew.Properties.ValueMember = "CrewMorning";

                //dtCrewsA = _clsSurvey.get_CrewsA(lblSection.Text);
                //cmbACrew.Properties.DataSource = dtCrewsA;
                //cmbACrew.Properties.DisplayMember = "CrewAfternoon";
                //cmbACrew.Properties.ValueMember = "CrewAfternoon";

                //dtCrewsE = _clsSurvey.get_CrewsA(lblSection.Text);
                //cmbECrew.Properties.DataSource = dtCrewsE;
                //cmbECrew.Properties.DisplayMember = "CrewEvening";
                //cmbECrew.Properties.ValueMember = "CrewEvening";

                //dtCrewsClean = _clsSurvey.get_CrewsA(lblSection.Text);
                //cmbCleaningCrew.Properties.DataSource = dtCrewsClean;
                //cmbCleaningCrew.Properties.DisplayMember = "CleaningCrew";
                //cmbCleaningCrew.Properties.ValueMember = "CleaningCrew";

                //dtCrewsTram = _clsSurvey.get_CrewsA(lblSection.Text);
                //cmbTrammingCrew.Properties.DataSource = dtCrewsTram;
                //cmbTrammingCrew.Properties.DisplayMember = "TrammingCrew";
                //cmbTrammingCrew.Properties.ValueMember = "TrammingCrew";

                //dtCrewsHlge = _clsSurvey.get_CrewsA(lblSection.Text);
                //cmbHlgeMaintainCrew.Properties.DataSource = dtCrewsHlge;
                //cmbHlgeMaintainCrew.Properties.DisplayMember = "HlgeMaintainanceCrew";
                //cmbHlgeMaintainCrew.Properties.ValueMember = "HlgeMaintainanceCrew";

                //dtCrewsRig = _clsSurvey.get_CrewsA(lblSection.Text);
                //cmbRiggerCrew.Properties.DataSource = dtCrewsRig;
                //cmbRiggerCrew.Properties.DisplayMember = "RiggerCrew";
                //cmbRiggerCrew.Properties.ValueMember = "RiggerCrew";

                //dtCrewsRse = _clsSurvey.get_CrewsA(lblSection.Text);
                //cmbRseCleaningCrew.Properties.DataSource = dtCrewsRse;
                //cmbRseCleaningCrew.Properties.DisplayMember = "RseCleaningCrew";
                //cmbRseCleaningCrew.Properties.ValueMember = "RseCleaningCrew";
            }
            catch { }
        }

        private void Load_PegNumbers()
        {
            try
            {
                cmbPegNumber.Items.Clear();
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtPegNumbers = _clsSurvey.get_PegNumbers("aa");

                cmbPegNumber.Items.Add("");
                txtPegValue.Text = "0.0";
                if (dtPegNumbers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPegNumbers.Rows)
                    {
                        cmbPegNumber.Items.Add(dr["PegDesc"].ToString());
                    }
                }
            }
            catch { }
        }

        private void Load_CubicTypes()
        {
            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtCubicTypes = _clsSurvey.get_CubicTypes();

                cmbExtras.DataSource = dtCubicTypes;
                cmbExtras.DisplayMember = "CubicTypeDesc";
                cmbExtras.ValueMember = "CubicTypeID";
            }
            catch { }
        }
        private void Load_CleanTypes()
        {
            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtCleanTypes = _clsSurvey.get_CubicTypes();

                cmbCleanType.DataSource = dtCleanTypes;
                cmbCleanType.DisplayMember = "CleanTypeDesc";
                cmbCleanType.ValueMember = "CleanTypeID";
            }
            catch { }
        }

        private void Load_Entries()
        {
            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtEntries = _clsSurvey.get_Entries("aa","aa","aa");

                if (dtEntries.Rows.Count != 0)
                {
                    MaxSeqNo = 0;
                    foreach (DataRow drStatus in dtEntries.Rows)
                    {
                        if (MaxSeqNo < Convert.ToInt32(drStatus["SeqNo"].ToString()))
                        {
                            MaxSeqNo = Convert.ToInt32(drStatus["SeqNo"].ToString());
                        }
                        cmbSeqNo.Items.Add(drStatus["SeqNo"].ToString());
                    }
                }
                else
                {
                    MaxSeqNo = 1;
                    cmbSeqNo.Items.Add(MaxSeqNo.ToString());
                }
                txtSeqNo.Text = MaxSeqNo.ToString();
                cmbSeqNo.Text = MaxSeqNo.ToString();
            }
            catch { }
        }
        private void Load_SurveyDevelopment()
        {
            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtSurveyDev = _clsSurvey.get_SurveyDev("aa", "aa", "aa", "aa");

                DataTable What = dtSurveyDev;

                Load_Density();

                if (What.Rows.Count != 0)
                {
                    CalendarDate.Value = Convert.ToDateTime(What.Rows[0]["calendardate"].ToString());
                    txtDip.Text = What.Rows[0]["Dip"].ToString();
                    txtBallDepth.Text = What.Rows[0]["BallDepth"].ToString();
                    cmbMiningMethod.SelectedValue = Convert.ToInt32(What.Rows[0]["MineMethod"].ToString());

                    cmbDens.SelectedValue = Convert.ToDecimal(What.Rows[0]["Density"].ToString());
                    cmbIndicator.SelectedValue = Convert.ToInt32(What.Rows[0]["Indicator"].ToString());
                    cmbMCrew.Text = What.Rows[0]["CrewMorning"].ToString();
                    cmbACrew.Text = What.Rows[0]["CrewAfternoon"].ToString();
                    cmbECrew.Text = What.Rows[0]["CrewEvening"].ToString();

                    cmbCleaningCrew.Text = What.Rows[0]["CleaningCrew"].ToString();
                    cmbTrammingCrew.Text = What.Rows[0]["TrammingCrew"].ToString();
                    cmbHlgeMaintainCrew.Text = What.Rows[0]["HlgeMaintainanceCrew"].ToString();
                    cmbRiggerCrew.Text = What.Rows[0]["RiggerCrew"].ToString();
                    cmbRseCleaningCrew.Text = What.Rows[0]["RseCleaningCrew"].ToString();
                    cmbPegNumber.SelectedItem = What.Rows[0]["PegNo"].ToString();
                    cmbPegNumber.Text = What.Rows[0]["PegNo"].ToString();
                    txtPegValue.Text = What.Rows[0]["PegValue"].ToString();
                    txtPegToFace.Text = What.Rows[0]["PegToFace"].ToString();
                    txtProgFrom.Text = What.Rows[0]["ProgFrom"].ToString();
                    txtProgTo.Text = What.Rows[0]["ProgTo"].ToString();
                    txtMainmetres.Text = What.Rows[0]["Mainmetres"].ToString();
                    txtMeasWidth.Text = What.Rows[0]["MeasWidth"].ToString();
                    txtMeasHeight.Text = What.Rows[0]["MeasHeight"].ToString();
                    txtPlanWidth.Text = What.Rows[0]["PlanWidth"].ToString();
                    txtPlanHeight.Text = What.Rows[0]["PlanHeight"].ToString();
                    txtReefmetres.Text = What.Rows[0]["Reefmetres"].ToString();
                    txtWastemetres.Text = What.Rows[0]["Wastemetres"].ToString();
                    txtTotalmetres.Text = What.Rows[0]["Totalmetres"].ToString();
                    txtLabour.Text = What.Rows[0]["Labour"].ToString();

                    cmbCleanType.SelectedValue = Convert.ToInt32(What.Rows[0]["Cleantype"].ToString());
                    txtCleanSqm.Text = What.Rows[0]["CleanSqm"].ToString();
                    txtCleanDist.Text = What.Rows[0]["CleanDist"].ToString();
                    txtCleanVamp.Text = What.Rows[0]["CleanVamp"].ToString();
                    txtCleanTons.Text = What.Rows[0]["CleanTons"].ToString();
                    txtCleangt.Text = What.Rows[0]["Cleangt"].ToString();
                    txtCleanContents.Text = What.Rows[0]["CleanContents"].ToString();

                    if (What.Rows[0]["PaidUnpaid"].ToString() == "Y")
                    {
                        chkboxPaid.Checked = true;
                    }
                    else
                    {
                        chkboxPaid.Checked = false;
                    }
                    txtCW.Text = What.Rows[0]["CW"].ToString();
                    txtHeight.Text = What.Rows[0]["ValHeight"].ToString();
                    txtGT.Text = What.Rows[0]["GT"].ToString();
                    txtcmgt.Text = What.Rows[0]["cmgt"].ToString();
                    cmbExtras.SelectedValue = Convert.ToInt32(What.Rows[0]["ExtraType"].ToString());
                    txtCubicsmetres.Text = What.Rows[0]["Cubicsmetres"].ToString();
                    txtCubicsReef.Text = What.Rows[0]["CubicsReef"].ToString();
                    txtCubicsWaste.Text = What.Rows[0]["CubicsWaste"].ToString();
                    txtOpenUpSqm.Text = What.Rows[0]["OpenUpSqm"].ToString();
                    txtReefDevSqm.Text = What.Rows[0]["ReefDevSqm"].ToString();
                    txtOpenUpcmgt.Text = What.Rows[0]["OpenUpcmgt"].ToString();
                    txtReefDevcmgt.Text = What.Rows[0]["ReefDevcmgt"].ToString();
                    txtOpenUpFL.Text = What.Rows[0]["OpenUpFL"].ToString();
                    txtReefDevFL.Text = What.Rows[0]["ReefDevFL"].ToString();
                    txtOpenUpEquip.Text = What.Rows[0]["OpenUpEquip"].ToString();
                    txtReefDevEquip.Text = What.Rows[0]["ReefDevEquip"].ToString();

                    txtTonsWasteBroken.EditValue = What.Rows[0]["TonsWasteBroken"].ToString();
                    txtTonsReefBroken.EditValue = What.Rows[0]["TonsReefBroken"].ToString();
                    txtTonsCubicsWaste.EditValue = What.Rows[0]["TonsCubicsWaste"].ToString();
                    txtTonsCubicsReef.EditValue = What.Rows[0]["TonsCubicsReef"].ToString();
                    txtTonsWaste.EditValue = What.Rows[0]["TonsWaste"].ToString();
                    txtTonsReef.EditValue = What.Rows[0]["TonsReef"].ToString();
                    txtTonsTotal.EditValue = What.Rows[0]["TonsTotal"].ToString();
                    txtTonsTrammed.EditValue = What.Rows[0]["TonsTrammed"].ToString();
                    txtTonsBallast.EditValue = What.Rows[0]["TonsBallast"].ToString();
                    cmbOreflowID.SelectedValue = What.Rows[0]["OreflowID"].ToString();
                    rdgrpDestination.SelectedIndex = cmbOreflowID.SelectedIndex;
                    if (What.Rows[0]["Payment"].ToString() == "1")
                    {
                        rdbtnPayment.Checked = true;
                    }
                    else
                    {
                        rdbtnPayment.Checked = false;
                    }
                    if (What.Rows[0]["Payment"].ToString() == "2")
                    {
                        rdbtnOreFlow.Checked = true;
                    }
                    else
                    {
                        rdbtnOreFlow.Checked = false;
                    }
                    if (What.Rows[0]["Payment"].ToString() == "3")
                    {
                        rdbtnPayOre.Checked = true;
                    }
                    else
                    {
                        rdbtnPayOre.Checked = false;
                    }

                    txtPlanNo.Text = What.Rows[0]["PlanNo"].ToString();
                }
                else
                {
                    Initialize_Development_Fields();
                }
            }
            catch (Exception Ex) { MessageBox.Show(Ex.ToString()); }
        }
        private void Load_Density()
        {
            //try
            //{
            //    _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //    dtDensity = _clsSurvey.get_Density(_prodmonth, bar);

            //    cmbDens.Items.Clear();
            //    //if (dtDensity.Rows.Count != 0
            //    //    && dtDensity.Rows[0][0] != DBNull.Value)
            //    //{
            //    //    cmbDens.Items.Add("Insitu:" + dtDensity.Rows[0][0]);
            //    //}

            //    if (dtDensity.Rows.Count != 0)
            //    {
            //        foreach (DataRow dr in dtDensity.Rows)
            //        {
            //            cmbDens.Items.Add(dr["RockDensity"]);
            //            if (dr["TheType"].ToString() == "RD")
            //                TheDens = Convert.ToDecimal(dr["Density"].ToString());
            //            if (dr["TheType"].ToString() == "BRD")
            //                TheBrokenRockDens = Convert.ToDecimal(dr["Density"].ToString());
            //        }
            //    }
            //    cmbDens.SelectedIndex = 0;
            //}
            //catch { }
        }

        private void Load_Destination()
        {
            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtDestinations = _clsSurvey.get_Destinations();

                foreach (DataRow r in dtDestinations.Rows)
                {
                    RadioGroupItem item1 = new RadioGroupItem();
                    item1.Description = r["Name"].ToString();
                    item1.Value = r["Name"].ToString();
                    rdgrpDestination.Properties.Items.Add(item1);
                }
                if (dtDestinations.Rows.Count != 0)
                {
                    cmbOreflowID.DataSource = dtDestinations;
                    cmbOreflowID.DisplayMember = "Name";
                    cmbOreflowID.ValueMember = "OreflowID";
                }
            }
            catch { }
        }
        protected void CheckLockedStatus()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtCheckLockStatus = _clsSurvey.get_CheckLockedStatus("201703");

            if (dtCheckLockStatus.Rows.Count != 0)
            {
                if (Convert.ToBoolean(dtCheckLockStatus.Rows[0][0]))
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    btnSaveClose.Enabled = false;
                }
            }
        }
        private void Initialize_Development_Fields()
        {
            int MaxSeqNo = 0;
            cmbSeqNo.Items.Clear();
            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtMaxSeqNo = _clsSurvey.get_MaxSeqNo("RE010986", "aa","aa");

                if (dtMaxSeqNo.Rows.Count != 0)
                    MaxSeqNo = Convert.ToInt32(dtMaxSeqNo.Rows[0]["SeqNo"].ToString());
                if (MaxSeqNo == 0)
                    TheWP = "N";
                else
                    TheWP = "Y";

                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtEntries = _clsSurvey.get_Entries("RE010986", "aa", "aa");

                foreach (DataRow dr in dtEntries.Rows)
                {
                    cmbSeqNo.Items.Add(dr["SeqNo"].ToString());
                }

                foreach (DataRow row in dtSysset.Rows)
                {
                    bool blnConfigurationValue = false;
                    bool.TryParse(row["ConfigurationValue"].ToString(), out blnConfigurationValue);
                    switch (row["ConfigurationKey"].ToString())
                    {
                        case "OpenUpSquareMeter":
                        case "ReefDevlopmentSquareMeter":
                            this.label133.Visible = blnConfigurationValue;
                            this.label130.Visible = blnConfigurationValue;
                            this.txtOpenUpSqm.Visible = blnConfigurationValue;
                            this.label129.Visible = blnConfigurationValue;
                            this.txtReefDevSqm.Visible = blnConfigurationValue;
                            this.label23.Visible = blnConfigurationValue;
                            break;
                        case "OpenUpCentimeterGramPerTon":
                        case "ReefDevelopmentCentimeterGramPerTon":
                            this.label132.Visible = blnConfigurationValue;
                            this.label128.Visible = blnConfigurationValue;
                            this.txtOpenUpcmgt.Visible = blnConfigurationValue;
                            this.label127.Visible = blnConfigurationValue;
                            this.txtReefDevcmgt.Visible = blnConfigurationValue;
                            this.label23.Visible = blnConfigurationValue;
                            break;
                        case "OpenUpFaceLength":
                        case "ReefDevelopmentFaceLength":
                            this.label131.Visible = blnConfigurationValue;
                            this.label126.Visible = blnConfigurationValue;
                            this.txtOpenUpFL.Visible = blnConfigurationValue;
                            this.label110.Visible = blnConfigurationValue;
                            this.txtReefDevFL.Visible = blnConfigurationValue;
                            this.label23.Visible = blnConfigurationValue;
                            break;
                        case "OpenUpEquipped":
                        case "ReefDevelopmentEquipped":
                            this.label113.Visible = blnConfigurationValue;
                            this.label120.Visible = blnConfigurationValue;
                            this.txtOpenUpEquip.Visible = blnConfigurationValue;
                            this.label119.Visible = blnConfigurationValue;
                            this.txtReefDevEquip.Visible = blnConfigurationValue;
                            this.label23.Visible = blnConfigurationValue;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch { }

            MaxSeqNo = MaxSeqNo + 1;

            cmbSeqNo.Items.Add(MaxSeqNo.ToString());
            txtSeqNo.Text = MaxSeqNo.ToString();
            cmbSeqNo.Text = MaxSeqNo.ToString();
            txtMeasWidth.Text = "0.0";
            txtMeasHeight.Text = "0.0";
            txtHeight.Text = "0";
            txtReefWaste.Text = "";

            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtEndTypes = _clsSurvey.get_EndTypes("RE010986");

                if (dtEndTypes.Rows.Count > 0)
                {
                    foreach (DataRow r in dtEndTypes.Rows)
                    {
                        if (TheWP == "N")
                        {
                            txtPlanWidth.Text = string.Format("{0:0.00}", r["EndWidth"].ToString());
                            txtPlanHeight.Text = string.Format("{0:0.00}", r["EndHeight"].ToString());
                        }
                        txtReefWaste.Text = r["ReefWaste"].ToString();
                    }
                }
            }
            catch { }

            txtDip.Text = "0";
            txtBallDepth.Text = "0.0";
            cmbMiningMethod.SelectedIndex = 1;
            cmbIndicator.SelectedValue = 1;
            //cmbMCrew.PropertiesSelectedIndex = -1;
            //cmbACrew.SelectedIndex = -1;
            //cmbECrew.SelectedIndex = -1;
            //cmbECrew.SelectedIndex = -1;
            //cmbECrew.SelectedIndex = -1;
            //cmbECrew.SelectedIndex = -1;
            //cmbECrew.SelectedIndex = -1;
            //cmbECrew.SelectedIndex = -1;
            cmbDens.Text = "0.00";
            cmbPegNumber.SelectedValue = -1;
            txtPegValue.Text = "0.0";
            txtPegToFace.Text = "0.0";
            txtProgFrom.Text = "0.0";
            txtProgTo.Text = "0.0";
            txtMainmetres.Text = "0.0";
            txtReefmetres.Text = "0.0";
            txtWastemetres.Text = "0.0";
            txtTonsReef.EditValue = "0.0";
            txtLabour.Text = "0";
            chkboxPaid.Checked = true;
            txtCW.Text = "0";
            txtGT.Text = "0.00";
            txtcmgt.Text = "0";
            cmbExtras.SelectedValue = -1;
            txtCubicsmetres.Text = "0.0";
            txtCubicsReef.Text = "0";
            txtCubicsWaste.Text = "0";
            txtOpenUpSqm.Text = "0";
            txtReefDevSqm.Text = "0";
            txtOpenUpcmgt.Text = "0";
            txtReefDevcmgt.Text = "0";
            txtOpenUpFL.Text = "0";
            txtReefDevFL.Text = "0";
            txtOpenUpEquip.Text = "0";
            txtReefDevEquip.Text = "0";
            txtTonsCubicsWaste.EditValue = "0";
            txtTonsWasteBroken.EditValue = "0";
            txtTonsWaste.EditValue = "0";
            txtTonsReef.EditValue = "0";
            txtTonsTotal.EditValue = "0";
            txtTonsTrammed.EditValue = "0";
            txtTonsBallast.EditValue = "0";
            if (txtReefWaste.Text == "W")
            {
                rdgrpDestination.SelectedIndex = 1;
            }
            else
            {
                rdgrpDestination.SelectedIndex = 2;
            }
            rdbtnPayment.Checked = false;
            rdbtnOreFlow.Checked = false;
            rdbtnPayOre.Checked = true;
            txtPlanNo.Text = "";

            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtPlanCrews = _clsSurvey.get_PlanCrews("201703","aa","RE010986");

                if (dtPlanCrews.Rows.Count != 0)
                {
                    cmbMCrew.SelectedText = dtPlanCrews.Rows[0]["OrgUnitDS"].ToString();
                    cmbACrew.SelectedText = dtPlanCrews.Rows[0]["OrgUnitAS"].ToString();
                    cmbECrew.SelectedText = dtPlanCrews.Rows[0]["OrgUnitNS"].ToString();
                    cmbMCrew.Text = dtPlanCrews.Rows[0]["OrgUnitDS"].ToString();
                    cmbACrew.Text = dtPlanCrews.Rows[0]["OrgUnitAS"].ToString();
                    cmbECrew.Text = dtPlanCrews.Rows[0]["OrgUnitNS"].ToString();
                }
            }
            catch { }

            try
            {
                _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                dtPlanDates = _clsSurvey.get_PlanDates("201703", "aa", "RE010986");

                if ((dtPlanDates != null) && 
                    (dtPlanDates.Rows.Count > 0) && 
                    (dtPlanDates.Rows[0]["CD"].ToString().Trim() != ""))
                {
                    this.CalendarDate.Value = Convert.ToDateTime(dtPlanDates.Rows[0]["CD"].ToString());
                }

            }
            catch { }
        }
        private void Calc_metres()
        {
            FromMtrs = true;
            txtMainmetres.Text = string.Format("{0:0.0}", Convert.ToDecimal(txtProgTo.Text) - Convert.ToDecimal(txtProgFrom.Text));
            txtTotalmetres.Text = txtMainmetres.Text;
            if (txtReefWaste.Text == "R")
            {
                txtReefmetres.Text = "0";
                txtReefmetres.Text = Convert.ToString(Convert.ToDecimal(txtTotalmetres.Text) - Convert.ToDecimal(txtWastemetres.Text));
            }
            else
            {
                txtWastemetres.Text = "0";
                txtWastemetres.Text = Convert.ToString(Convert.ToDecimal(txtTotalmetres.Text) - Convert.ToDecimal(txtReefmetres.Text));
            }
            FromMtrs = false;
            Calc_Tons();
        }

        private void Calc_Reefmetres()
        {
            if (FromMtrs == false)
                txtReefmetres.Text = Convert.ToString(Convert.ToDecimal(txtTotalmetres.Text) - Convert.ToDecimal(txtWastemetres.Text));
            Calc_Tons();
        }

        private void Calc_Wastemetres()
        {
            if (FromMtrs == false)
                txtWastemetres.Text = Convert.ToString(Convert.ToDecimal(txtTotalmetres.Text) - Convert.ToDecimal(txtReefmetres.Text));
            Calc_Tons();
        }

        private void Calc_ToPeg()
        {
            decimal TheVal = Math.Abs(Convert.ToDecimal(txtPegValue.Text) + Convert.ToDecimal(txtPegToFace.Text));
            txtProgTo.Text = string.Format("{0:0.0}", TheVal);
        }

        public void Calc_Tons()
        {
            txtTonsWaste.EditValue = Convert.ToDecimal(((Convert.ToDecimal(txtWastemetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                                Convert.ToDecimal(txtMeasWidth.Text)) + Convert.ToDecimal(txtCubicsWaste.Text)) *
                                                TheDens);
            txtTonsReef.EditValue = Convert.ToDecimal(((Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                                Convert.ToDecimal(txtMeasWidth.Text)) + Convert.ToDecimal(txtCubicsReef.Text)) *
                                                TheDens).ToString("F2");
            txtTonsCubicsWaste.EditValue = Convert.ToDecimal(Convert.ToDecimal(txtCubicsWaste.Text) * TheDens).ToString("F2");
            txtTonsCubicsReef.EditValue = Convert.ToDecimal(Convert.ToDecimal(txtCubicsReef.Text) * TheDens).ToString("F2");
            txtTonsWasteBroken.EditValue = Convert.ToDecimal(Convert.ToDecimal(txtWastemetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                                Convert.ToDecimal(txtMeasWidth.Text) * TheDens).ToString("F2");
            txtTonsReefBroken.EditValue = Convert.ToDecimal(Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                                Convert.ToDecimal(txtMeasWidth.Text) * TheDens).ToString("F2");
            txtTonsTotal.EditValue = Convert.ToDecimal(((Convert.ToDecimal(txtWastemetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                                Convert.ToDecimal(txtMeasWidth.Text)) + Convert.ToDecimal(txtCubicsWaste.Text)) *
                                                TheDens +
                                                ((Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                                Convert.ToDecimal(txtMeasWidth.Text)) + Convert.ToDecimal(txtCubicsReef.Text)) *
                                                TheDens).ToString("F2");
            txtTonsBallast.EditValue = Convert.ToDecimal(Convert.ToDecimal(txtMainmetres.Text) * (Convert.ToDecimal(txtBallDepth.Text) / 100) *
                                                Convert.ToDecimal(txtMeasWidth.Text) * TheBrokenRockDens).ToString("F2");
            txtTonsTrammed.EditValue = Convert.ToDecimal((((Convert.ToDecimal(txtWastemetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                                Convert.ToDecimal(txtMeasWidth.Text)) + Convert.ToDecimal(txtCubicsWaste.Text)) *
                                                TheDens +
                                                ((Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                                Convert.ToDecimal(txtMeasWidth.Text)) + Convert.ToDecimal(txtCubicsReef.Text)) *
                                                TheDens) -
                                                   ((Convert.ToDecimal(txtMainmetres.Text) *
                                                    (Convert.ToDecimal(txtBallDepth.Text) / 100)) *
                                                     Convert.ToDecimal(txtMeasWidth.Text) * TheBrokenRockDens)).ToString("F2");
            Calc_ValGT();
        }

        public void Calc_Height()
        {
            txtHeight.Text = string.Format("{0:0}", Convert.ToDecimal(txtMeasHeight.Text) * 100);
        }

        public void Calc_ValGT()
        {
            decimal TheGrams = 0;
            if (Convert.ToDecimal(txtMeasHeight.Text) > 0)
                TheGrams = (Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasWidth.Text) *
                            Convert.ToDecimal(txtcmgt.Text) * TheDens / 100) +
                           (Convert.ToDecimal(txtCubicsReef.Text) / Convert.ToDecimal(txtMeasHeight.Text) *
                            Convert.ToDecimal(txtcmgt.Text) * TheDens / 100);
            else
                TheGrams = (Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasWidth.Text) *
                            Convert.ToDecimal(txtcmgt.Text) * TheDens / 100);
            if (Convert.ToDecimal(txtTonsReef.EditValue) != 0)
                txtGT.Text = string.Format("{0:0.00}", TheGrams / Convert.ToDecimal(txtTonsReef.EditValue));
        }
    }
}
