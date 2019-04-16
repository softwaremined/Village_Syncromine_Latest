using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.Survey
{
    public partial class ucSurveyDev : DevExpress.XtraEditors.XtraForm
    {
        public string theConnection;
        clsSurvey _clsSurvey = new clsSurvey();

        private DataTable dtEntries;
        private DataTable dtDensity;
        private DataTable dtEndTypes;
        private DataTable dtPlanDates;
        private DataTable dtPegNumbers;

        private DataTable dtPeg;
        private DataTable dtSurvey;
        private DataTable dtWorkplace;
        private DataTable dtSurveyDev;
        private DataTable dtMaxSeqNo;
        private DataTable dtPlanCrews;
        private DataTable dtSurveyVisibles;
        private DataTable dtTheWP;

        public string Workplace;
        public string WorkplaceID;
        public string TheWP;
        public string ProdMonth;
        public string Section;
        public string Contractor;
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

        public ucSurveyDev()
        {
            InitializeComponent();
        }

        private void ucSurveyDev_Load(object sender, EventArgs e)
        {
            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Do you want to save your changes?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            //{
            //    btnSave_Click(sender, e);
            //}

            Initialize_Development_Fields();
            Load_PegNumbers();
        }
        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            ErrorFound = false;
            Validate_Development();
            if (ErrorFound == false)
            {
                Save_SurveyDevelopment();
                Close();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ErrorFound = false;
            Validate_Development();
            if (ErrorFound == false)
            {
                Save_SurveyDevelopment();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtSurvey = _clsSurvey.get_MaxSeqNo(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Dev");

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
                                                                    lblWorkplaceID.Text, MaxSeqNo, "Dev");
                    if (_deleteSurvey == true)
                    {
                        Global.sysNotification.TsysNotification.showNotification("Data Saved", "Survey Deleted", Color.CornflowerBlue);
                    }
                    Load_Entries();
                    Load_SurveyDevelopment();
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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
            TheDens = 0;
            if (luDensity.EditValue != null)
            {

                DataRow[] EditedRow = dtDensity.Select(" TheType = '" + luDensity.EditValue.ToString() + "' ");
                foreach (DataRow dr in EditedRow)
                {
                    TheDens = Convert.ToDecimal(dr["Density"].ToString());
                }
            }
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
            TheDens = 0;
            if (luDensity.EditValue != null)
            {

                DataRow[] EditedRow = dtDensity.Select(" TheType = '" + luDensity.EditValue.ToString() + "' ");
                foreach (DataRow dr in EditedRow)
                {
                    TheDens = Convert.ToDecimal(dr["Density"].ToString());
                }
            }
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

       
        public void Validate_Development()
        {
            //string aa = luCleanTypes.EditValue.ToString();
            //if (luCleanTypes.EditValue != null && aa != "")
            //{
            //    if (Convert.ToDecimal(this.txtCleanSqm.Text) == 0)
            //    {
            //        ErrorFound = true;
            //        System.Windows.Forms.MessageBox.Show("Please enter clean SQM", "Invalid input!");
            //        txtCleanSqm.Focus();
            //    }
            //}

            if (ErrorFound == false)
            {
                if (Convert.ToDecimal(txtMainmetres.Text) == 0 && Convert.ToDecimal(txtCubicsmetres.Text) == 0)
                {
                    ErrorFound = true;
                    System.Windows.Forms.MessageBox.Show("Please enter Main metres or Cubic meters");
                    txtMainmetres.Focus();
                }
            }

            if (ErrorFound == false)
            {
                if ((Convert.ToDecimal(txtMainmetres.Text) < 0))
                {
                    ErrorFound = true;                    
                    System.Windows.Forms.MessageBox.Show("You have entered a negative advance");
                    txtMainmetres.Focus();
                }
            }
            if (ErrorFound == false)
            {
                if ((Convert.ToDecimal(txtMainmetres.Text) > 0) &&
                    (Convert.ToDecimal(txtMeasWidth.Text) == 0))
                {
                    ErrorFound = true;
                    System.Windows.Forms.MessageBox.Show("You must enter a Width where there are metres");
                    txtMeasWidth.Focus();
                }
            }
            if (ErrorFound == false)
            {
                _clsSurvey.theData.ConnectionString = theConnection;
                dtWorkplace = _clsSurvey.find_Workplace(lblWorkplaceID.Text);

                if (dtWorkplace.Rows.Count != 0)
                {
                    if ((dtWorkplace.Rows[0]["EndTypeID"].ToString() != "25") &&
                        (Convert.ToDecimal(txtMainmetres.Text) > 0) &&
                        (Convert.ToDecimal(txtMeasHeight.Text) == 0))
                    {
                        ErrorFound = true;
                        System.Windows.Forms.MessageBox.Show("You must enter a Height where there are meters");
                        txtMeasHeight.Focus();
                    }
                    if (ErrorFound == false)
                    {
                        if ((Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtCW.Text) * Convert.ToDecimal(txtcmgt.Text) == 0) &&
                            (Convert.ToDecimal(txtReefmetres.Text) > 0) &&
                            (dtWorkplace.Rows[0]["EndTypeID"].ToString() != "25"))
                        {
                            ErrorFound = true;                            
                            System.Windows.Forms.MessageBox.Show("You must enter a Channel Width and cmgt when there are reef metres");
                            txtCW.Focus();
                        }
                    }
                    if (ErrorFound == false)
                    {
                        if ((Convert.ToDecimal(txtCubicsReef.Text) > 0) &&
                            (Convert.ToDecimal(txtMeasHeight.Text) == 0) &&
                            (dtWorkplace.Rows[0]["EndTypeID"].ToString() != "25"))
                        {
                            ErrorFound = true;
                            txtMeasHeight.Focus();
                            System.Windows.Forms.MessageBox.Show("You must enter a Height when there are reef cubics");
                        }
                    }
                    if (ErrorFound == false)
                    {
                        if ((Convert.ToDecimal(txtCubicsReef.Text) > 0) &&
                            (Convert.ToDecimal(txtcmgt.Text) == 0) &&
                            (dtWorkplace.Rows[0]["EndTypeID"].ToString() != "25"))
                        {
                            ErrorFound = true;
                            txtcmgt.Focus();
                            System.Windows.Forms.MessageBox.Show("You must enter a cmgt when there are reef cubics");
                        }
                    }
                }
            }
            if (ErrorFound == false)
            {
                if ((Convert.ToDecimal(txtReefmetres.Text) > 0) &&
                    (Convert.ToDecimal(txtcmgt.Text) == 0) &&
                    (luOreflowID.EditValue.ToString() != "Dumped"))
                {
                    ErrorFound = true;
                    txtcmgt.Focus();
                    System.Windows.Forms.MessageBox.Show("You must enter a cmgt where there are metres");
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
            }
            if (ErrorFound == false)
            {
                if ((Convert.ToDecimal(txtReefmetres.Text) > 0) &&
                     (Convert.ToDecimal(txtCW.Text) == 0))
                {
                    ErrorFound = true;
                    txtcmgt.Focus();
                    System.Windows.Forms.MessageBox.Show("You must enter a Channel Width where there are metres");
                }
            }
        }
        public void Save_SurveyDevelopment()
        {
            try
            {
                _clsSurvey.theData.ConnectionString = theConnection;
                dtTheWP = _clsSurvey.find_TheWP(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, txtSeqNo.Text, "Dev");

                decimal TheCont = 0;
                if (Convert.ToDecimal(txtMeasHeight.Text) == 0)
                    TheCont = Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasWidth.Text) *
                              (Convert.ToDecimal(txtcmgt.Text) / 100) * TheDens;
                else
                    TheCont = (Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasWidth.Text) *
                              (Convert.ToDecimal(txtcmgt.Text) / 100) * TheDens) +
                              (Convert.ToDecimal(txtCubicsReef.Text) / Convert.ToDecimal(txtMeasHeight.Text) *
                               (Convert.ToDecimal(txtcmgt.Text) / 100) * TheDens);
                TonsWasteTotal = ((Convert.ToDecimal(txtWastemetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                   Convert.ToDecimal(txtMeasWidth.Text)) + Convert.ToDecimal(txtCubicsWaste.Text)) *
                                    TheDens;
                TonsReefTotal = ((Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                     Convert.ToDecimal(txtMeasWidth.Text)) + Convert.ToDecimal(txtCubicsReef.Text)) *
                                        TheDens;
                TonsCubicsWaste = Convert.ToDecimal(txtCubicsWaste.Text) * TheDens;
                TonsCubicsReef = Convert.ToDecimal(txtCubicsReef.Text) * TheDens;
                TonsWasteBroken = Convert.ToDecimal(txtWastemetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                    Convert.ToDecimal(txtMeasWidth.Text) * TheDens;
                TonsReefBroken = Convert.ToDecimal(txtReefmetres.Text) * Convert.ToDecimal(txtMeasHeight.Text) *
                                    Convert.ToDecimal(txtMeasWidth.Text) * TheDens;
                TonsTotal = TonsReefTotal + TonsWasteTotal;
                TonsBallast = Convert.ToDecimal(txtMainmetres.Text) * (Convert.ToDecimal(txtBallDepth.Text) / 100) *
                                Convert.ToDecimal(txtMeasWidth.Text) * TheBrokenRockDens;
                TonsTrammed = TonsTotal - TonsBallast;

                if (dtTheWP.Rows.Count == 0)
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = theConnection;
                    _dbMan.SqlStatement = "insert into Survey ( \r\n  " +
                                          "  WorkplaceID, SectionID, Activity, CalendarDate, ProdMonth, SeqNo, \r\n " +
                                          "  Dip, BallDepth, MineMethod, Indicator, Density, \r\n " +
                                          "  CrewMorning, CrewAfternoon, CrewEvening,CleaningCrew,TrammingCrew,HlgeMaintainanceCrew,RiggerCrew,RseCleaningCrew, \r\n " +
                                          "  PegNo, PegValue, PegToFace, ProgFrom, ProgTo, \r\n " +
                                          "  Mainmetres, \r\n " +
                                          "  MeasWidth, MeasHeight, \r\n " +
                                          "  PlanWidth, PlanHeight, \r\n " +
                                          "  Reefmetres, Wastemetres, Totalmetres, \r\n " +
                                          "  Labour, PaidUnpaid, \r\n " +
                                          "  CW, ValHeight, GT, cmgt, \r\n " +
                                          "  ExtraType, \r\n " +
                                          "  Cubicsmetres,CubicsReef, CubicsWaste,  \r\n " +
                                          "  OpenUpSqm, ReefDevSqm, OpenUpcmgt, Reefdevcmgt, OpenUpFL, ReefDevFL, \r\n " +
                                          "  OpenUpEquip, ReefDevEquip, TonsCubicsReef, TonsCubicsWaste,  \r\n " +
                                          "  TonsWasteBroken, TonsReefBroken, TonsWaste, TonsReef, TonsTotal, TonsTrammed, TonsBallast, " +
                                          "  Destination, OreflowID, Payment, \r\n " +
                                          "  Cleantype, CleanSqm, CleanDist, CleanVamp, CleanTons, Cleangt, CleanContents, \r\n " + // Added - Shaista Anjum For Inserting Cleaning data - 13/FEB/2013
                                          "  TotalContent, PlanNo) \r\n " +
                                          " values( \r\n " +
                                          "  '" + lblWorkplaceID.Text + "', '" + lblContractor.Text + "', 1, \r\n " +
                                          "  '" + String.Format("{0:yyyy-MM-dd}", CalendarDate.Value) + "', \r\n " +
                                          "  '" + lblProdMonth.Text + "', '" + txtSeqNo.Text + "',  '" + txtDip.Text + "', \r\n " +
                                          "  '" + txtBallDepth.Text + "', \r\n ";
                    if (luMiningMethod.EditValue == null)
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                    {
                        if (luMiningMethod.EditValue.ToString() == "")
                            _dbMan.SqlStatement += "null, \r\n ";
                        else
                            _dbMan.SqlStatement += " '" + luMiningMethod.EditValue.ToString() + "', \r\n ";
                    }
                    if (luIndicator.EditValue == null)
                        _dbMan.SqlStatement += "null, \r\n ";
                    else
                    {
                        if (luIndicator.EditValue.ToString() == "")
                            _dbMan.SqlStatement += "null, \r\n ";
                        else
                            _dbMan.SqlStatement += " '" + luIndicator.EditValue.ToString() + "', \r\n ";
                    }
                    _dbMan.SqlStatement = _dbMan.SqlStatement +
                                          "   " + TheDens + ", \r\n ";
                    if (cmbMCrew.EditValue == null)
                        _dbMan.SqlStatement += "null, \r\n ";
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
                            _dbMan.SqlStatement += "'N/A', \r\n ";
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
                    if (luPegNumbers.EditValue == null)
                        _dbMan.SqlStatement += "'', \r\n ";
                    else
                    {
                        if (luPegNumbers.EditValue.ToString() == "")
                            _dbMan.SqlStatement += "'', \r\n ";
                        else
                            _dbMan.SqlStatement += " '" + luPegNumbers.EditValue.ToString() + "', \r\n ";
                    }
                    _dbMan.SqlStatement = _dbMan.SqlStatement +
                                          "  '" + txtPegValue.Text + "', '" + txtPegToFace.Text + "', \r\n " +
                                          "  '" + txtProgFrom.Text + "', '" + txtProgTo.Text + "', \r\n " +
                                          "  '" + txtMainmetres.Text + "', \r\n " +
                                          "  '" + txtMeasWidth.Text + "', '" + txtMeasHeight.Text + "', \r\n " +
                                          "  '" + txtPlanWidth.Text + "', '" + txtPlanHeight.Text + "', \r\n " +
                                          "  '" + txtReefmetres.Text + "', '" + txtWastemetres.Text + "', \r\n " +
                                          "  '" + txtTotalmetres.Text + "', \r\n " +
                                          "  '" + txtLabour.Text + "',  \r\n ";
                    if (chkboxPaid.Checked == true)
                    {
                        _dbMan.SqlStatement += " 'Y' \r\n ,";
                    }
                    else
                    {
                        _dbMan.SqlStatement += " 'N', \r\n ";
                    }
                    _dbMan.SqlStatement = _dbMan.SqlStatement +
                                          "  '" + txtCW.Text + "', '" + txtHeight.Text + "', \r\n " +
                                          "  '" + txtGT.Text + "', '" + txtcmgt.Text + "', \r\n ";
                    if (luCubicTypes.EditValue == null)
                        _dbMan.SqlStatement += "-1, \r\n ";
                    else
                    {
                        if (luCubicTypes.EditValue.ToString() == "")
                            _dbMan.SqlStatement += "-1, \r\n ";
                        else
                            _dbMan.SqlStatement += " '" + luCubicTypes.EditValue + "', \r\n ";
                    }

                    _dbMan.SqlStatement = _dbMan.SqlStatement +
                                          "  '" + txtCubicsmetres.Text + "','" + txtCubicsReef.Text + "', '" + txtCubicsWaste.Text + "', \r\n " +
                                          "  '" + txtOpenUpSqm.Text + "', '" + txtReefDevSqm.Text + "', \r\n " +
                                          "  '" + txtOpenUpcmgt.Text + "', '" + txtReefDevcmgt.Text + "', \r\n " +
                                          "  '" + txtOpenUpFL.Text + "', '" + txtReefDevFL.Text + "', \r\n " +
                                          "  '" + txtOpenUpEquip.Text + "', '" + txtReefDevEquip.Text + "', \r\n " +
                                          "  '" + TonsCubicsReef + "','" + TonsCubicsWaste + "', \r\n " +
                                          "  '" + TonsWasteBroken + "','" + TonsReefBroken + "', \r\n " +
                                          "  '" + TonsWasteTotal + "', \r\n " +
                                          "  '" + TonsReefTotal + "', '" + TonsTotal + "', \r\n " +
                                          "  '" + TonsTrammed + "', '" + TonsBallast + "', \r\n ";
                    _dbMan.SqlStatement += " '" + rdgrpDestination.SelectedIndex + "', \r\n " +
                        " '" + luOreflowID.EditValue.ToString() + "', \r\n ";

                    _dbMan.SqlStatement += " '" + rdgrpPayments.EditValue + "', \r\n ";
                    if (luCleanTypes.EditValue == null)
                        _dbMan.SqlStatement += "-1, \r\n ";
                    else
                    {
                        if (luCleanTypes.EditValue.ToString() == "")
                            _dbMan.SqlStatement += "-1, \r\n ";
                        else
                            _dbMan.SqlStatement += " '" + luCleanTypes.EditValue + "', \r\n ";
                    }

                    _dbMan.SqlStatement += "  '" + txtCleanSqm.Text + "', '" + txtCleanDist.Text + "','" + txtCleanVamp.Text + "', \r\n ";
                    _dbMan.SqlStatement += "  '" + txtCleanTons.Text + "', ";
                    _dbMan.SqlStatement += "  '" + txtCleangt.Text + "', '" + txtCleanContents.Text + "', \r\n ";

                    _dbMan.SqlStatement += " '" + TheCont + "', '" + txtPlanNo.Text + "') ";
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
                                            "  Dip = '" + txtDip.Text + "', \r\n " +
                                            "  BallDepth = '" + txtBallDepth.Text + "', \r\n ";
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
                    if (luIndicator.EditValue == null)
                        _dbMan.SqlStatement += " Indicator = null, \r\n ";
                    else
                    {
                        if (luIndicator.EditValue.ToString() == "")
                            _dbMan.SqlStatement += " Indicator = null, \r\n ";
                        else
                            _dbMan.SqlStatement += " Indicator = '" + luIndicator.EditValue.ToString() + "', \r\n ";
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
                    if (luPegNumbers.EditValue == null)
                        _dbMan.SqlStatement += "  PegNo = null, \r\n ";
                    else
                    {
                        if (luPegNumbers.EditValue.ToString() == "")
                            _dbMan.SqlStatement += "  PegNo = null, \r\n ";
                        else
                            _dbMan.SqlStatement += "  PegNo = '" + luPegNumbers.EditValue.ToString() + "', \r\n ";
                    }

                    _dbMan.SqlStatement = _dbMan.SqlStatement +
                                          "  PegValue = '" + txtPegValue.Text + "', \r\n " +
                                          "  PegToFace = '" + txtPegToFace.Text + "', \r\n " +
                                          "  ProgFrom = '" + txtProgFrom.Text + "', \r\n " +
                                          "  ProgTo = '" + txtProgTo.Text + "', \r\n " +
                                          "  Mainmetres =  '" + txtMainmetres.Text + "', \r\n " +
                                          "  Totalmetres =  '" + txtTotalmetres.Text + "', \r\n " +
                                          "  MeasWidth = '" + txtMeasWidth.Text + "', \r\n " +
                                          "  MeasHeight = '" + txtMeasHeight.Text + "', \r\n " +
                                          "  PlanWidth =  '" + txtPlanWidth.Text + "', \r\n " +
                                          "  PlanHeight = '" + txtPlanHeight.Text + "', \r\n " +
                                          "  Reefmetres = '" + txtReefmetres.Text + "', \r\n " +
                                          "  Wastemetres = '" + txtWastemetres.Text + "', \r\n " +
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
                                          "  CW = '" + txtCW.Text + "', \r\n " +
                                          "  ValHeight = '" + txtHeight.Text + "', \r\n " +
                                          "  GT = '" + txtGT.Text + "', \r\n " +
                                          "  cmgt = '" + txtcmgt.Text + "', \r\n ";
                    if (luCubicTypes.EditValue == null)
                        _dbMan.SqlStatement += "  ExtraType = 0, \r\n ";
                    else
                    {
                        if (luCubicTypes.EditValue.ToString() == "")
                            _dbMan.SqlStatement += "  ExtraType = 0, \r\n ";
                        else
                            _dbMan.SqlStatement += "  ExtraType =  '" + luCubicTypes.EditValue.ToString() + "', \r\n ";
                    }

                    _dbMan.SqlStatement = _dbMan.SqlStatement +
                                          "  Cubicsmetres = '" + txtCubicsmetres.Text + "', \r\n " +
                                          "  CubicsReef = '" + txtCubicsReef.Text + "', \r\n " +
                                          "  CubicsWaste = '" + txtCubicsWaste.Text + "', \r\n " +
                                          "  OpenUpSqm = '" + txtLabour.Text + "', \r\n " +
                                          "  ReefDevSqm = '" + txtReefDevSqm.Text + "', \r\n " +
                                          "  OpenUpcmgt = '" + txtOpenUpcmgt.Text + "', \r\n " +
                                          "  ReefDevcmgt = '" + txtReefDevcmgt.Text + "', \r\n " +
                                          "  OpenUpFL = '" + txtOpenUpFL.Text + "', \r\n " +
                                          "  ReefDevFL = '" + txtReefDevFL.Text + "', \r\n " +
                                          "  OpenUpEquip = '" + txtOpenUpEquip.Text + "', \r\n " +
                                          "  ReefDevEquip = '" + txtReefDevEquip.Text + "', \r\n " +
                                          "  TonsCubicsReef = '" + TonsCubicsReef + "', \r\n " +
                                          "  TonsCubicsWaste = '" + TonsCubicsWaste + "', \r\n " +
                                          "  TonsWasteBroken = '" + TonsWasteBroken + "', \r\n " +
                                          "  TonsReefBroken = '" + TonsReefBroken + "', \r\n " +
                                          "  TonsWaste = '" + TonsWasteTotal + "', \r\n " +
                                          "  TonsReef = '" + TonsReefTotal + "', \r\n " +
                                          "  TonsTotal = '" + TonsTotal + "', \r\n " +
                                          "  TonsTrammed = '" + TonsTrammed + "', \r\n " +
                                          "  TonsBallast = '" + TonsBallast + "', \r\n " +
                                          "  TotalContent = '" + TheCont + "', \r\n ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " Destination = '" + rdgrpDestination.SelectedIndex + "', \r\n " +
                        " OreflowID = '" + luOreflowID.EditValue.ToString() + "', \r\n ";

                    _dbMan.SqlStatement += " Payment ='" + rdgrpPayments.EditValue.ToString() + "', \r\n ";
                    if (luCleanTypes.EditValue == null)
                        _dbMan.SqlStatement += "  Cleantype =  null, \r\n ";
                    else
                    {
                        if (luCleanTypes.EditValue.ToString() == "")
                            _dbMan.SqlStatement += "  Cleantype =  null, \r\n ";
                        else
                            _dbMan.SqlStatement += " Cleantype ='" + luCleanTypes.EditValue + "', \r\n ";
                    }

                    _dbMan.SqlStatement += "  CleanSqm = '" + txtCleanSqm.Text + "', \r\n " +
                        "  CleanDist = '" + txtCleanDist.Text + "', \r\n " +
                        "  CleanVamp = '" + txtCleanVamp.Text + "', \r\n " +
                        "  CleanTons = '" + txtCleanTons.Text + "', \r\n " +
                        "  Cleangt = '" + txtCleangt.Text + "', \r\n " +
                        "  CleanContents = '" + txtCleanContents.Text + "', \r\n ";

                    _dbMan.SqlStatement = _dbMan.SqlStatement +
                                          "  PlanNo = '" + txtPlanNo.Text + "' \r\n " +
                                          "where ProdMonth = '" + lblProdMonth.Text + "'and \r\n  " +
                                          "      SECTIONID = '" + lblContractor.Text + "' and \r\n " +
                                          "      WorkplaceID = '" + lblWorkplaceID.Text + "' and \r\n " +
                                          "      Activity = 1 and \r\n  " +
                                          "      SeqNo = '" + txtSeqNo.Text + "' ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
                    _dbMan.ExecuteInstruction();

                    Global.sysNotification.TsysNotification.showNotification("Data Saved", "Survey Updated", Color.CornflowerBlue);
                }
            }
            catch (Exception Ex) { MessageBox.Show(Ex.ToString()); }
            try
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.SqlStatement = "update p set Bookadv = isnull(pg.value,0) + p.pegtoface - '" + Convert.ToDecimal(txtProgTo.Text) + "' \r\n";
                _dbMan.SqlStatement += " from (select top 1 * from planning \r\n";
                _dbMan.SqlStatement += " where ProdMonth > '" + lblProdMonth.Text + "' and  \r\n";
                _dbMan.SqlStatement += "      WorkplaceID = '" + lblWorkplaceID.Text + "' and \r\n";
                _dbMan.SqlStatement += "      Activity = 1 and (bookadv > 0 or bookadv < 0) \r\n";
                _dbMan.SqlStatement += "  order by calendardate) p left outer join peg pg on \r\n";
                _dbMan.SqlStatement += "  (pg.pegid + ':' + cast(pg.value as varchar(10)) = p.pegid or \r\n";
                _dbMan.SqlStatement += "  pg.pegid + ':' + cast(convert(numeric(10,1), pg.value,1) as varchar(10)) = p.pegid) and \r\n";
                _dbMan.SqlStatement += "  pg.workplaceid = p.workplaceid";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.longNumber;
                _dbMan.ExecuteInstruction();
            }
            catch { }
        }

        private void luPegNumbers_EditValueChanged(object sender, EventArgs e)
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            if (luPegNumbers.EditValue != null)
                if (luPegNumbers.EditValue.ToString() != "")
                {
                    dtPeg = _clsSurvey.find_Peg(lblWorkplaceID.Text, luPegNumbers.EditValue.ToString());

                    txtPegToFace.Text = "0.0";
                    if (dtPeg.Rows.Count == 0)
                    {
                        txtPegValue.Text = "0.0";
                    }
                    else
                    {
                        txtPegValue.Text = dtPeg.Rows[0]["PegValue"].ToString();
                        dtSurvey = _clsSurvey.find_Survey(lblProdMonth.Text, lblWorkplaceID.Text);

                        if (dtSurvey.Rows.Count == 0)
                        {
                            txtProgFrom.Text = "0.0";
                        }
                        else
                        {
                            txtProgFrom.Text = dtSurvey.Rows[0]["ProgTo"].ToString();
                        }
                    }
                }
        }

        private void txtPegToFace_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_ToPeg();
            Calc_metres();  //Main
        }

        private void txtMeasHeight_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_Height();
            Calc_Tons();
        }

        private void txtReefmetres_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_Wastemetres();//Reef + Waste
        }

        private void txtWastemetres_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_Reefmetres();
        }

        private void txtBallDepth_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_Tons();
        }

        private void txtProgFrom_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_metres();
        }

        private void txtcmgt_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_ValGT();
        }

        private void cmbSeqNo_EditValueChanged(object sender, EventArgs e)
        {
            txtSeqNo.Text = cmbSeqNo.SelectedItem.ToString();
            Load_SurveyDevelopment();
        }

        private void rdgrpDestination_EditValueChanged(object sender, EventArgs e)
        {
            luOreflowID.EditValue = rdgrpDestination.EditValue;
            //luOreflowID.Text = 
        }

        private void txtCleanTons_KeyUp(object sender, KeyEventArgs e)
        {
            txtCleanContents.Text = string.Format("{0:0}", Convert.ToDecimal(this.txtCleanTons.Text) * Convert.ToDecimal(this.txtCleangt.Text));
        }

        private void txtCubicsReef_KeyUp(object sender, KeyEventArgs e)
        {
            Calc_Tons();
        }
        public void Load_Entries()
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtEntries = _clsSurvey.get_Entries(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Dev");

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
        public void Load_PegNumbers()
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtPegNumbers = _clsSurvey.get_PegNumbers(lblWorkplaceID.Text);

            luPegNumbers.Properties.DataSource = dtPegNumbers;
            luPegNumbers.Properties.DisplayMember = "PegNo";
            luPegNumbers.Properties.ValueMember = "PegNo";
        }
        public void Load_SurveyDevelopment()
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtSurveyDev = _clsSurvey.get_Development_Detail(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text,txtSeqNo.Text);

            DataTable What = dtSurveyDev;

            CalendarDate.Value = System.DateTime.Now;

            Load_Density();

            if (What.Rows.Count != 0)
            {
                CalendarDate.Value = Convert.ToDateTime(What.Rows[0]["calendardate"].ToString());
                txtDip.Text = What.Rows[0]["Dip"].ToString();
                txtBallDepth.Text = What.Rows[0]["BallDepth"].ToString();
                luMiningMethod.EditValue = Convert.ToInt32(What.Rows[0]["MineMethod"].ToString());
                luDensity.EditValue = "";
                luIndicator.EditValue = Convert.ToInt32(What.Rows[0]["Indicator"].ToString());
                cmbMCrew.Text = What.Rows[0]["CrewMorning"].ToString();
                cmbACrew.Text = What.Rows[0]["CrewAfternoon"].ToString();
                cmbECrew.Text = What.Rows[0]["CrewEvening"].ToString();
                cmbCleaningCrew.Text = What.Rows[0]["CleaningCrew"].ToString();
                cmbTrammingCrew.Text = What.Rows[0]["TrammingCrew"].ToString();
                cmbHlgeMaintainCrew.Text = What.Rows[0]["HlgeMaintainanceCrew"].ToString();
                cmbRiggerCrew.Text = What.Rows[0]["RiggerCrew"].ToString();
                cmbRseCleaningCrew.Text = What.Rows[0]["RseCleaningCrew"].ToString();

                luPegNumbers.EditValue = What.Rows[0]["PegNo"].ToString();
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

                luCleanTypes.EditValue = Convert.ToInt32(What.Rows[0]["CleanTypeID"].ToString());
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

                luCubicTypes.EditValue = Convert.ToInt32(What.Rows[0]["ExtraType"].ToString());
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

                luOreflowID.EditValue = What.Rows[0]["OreflowID"].ToString();
                rdgrpDestination.EditValue = What.Rows[0]["OreflowID"].ToString();
                rdgrpPayments.EditValue = Convert.ToInt32(What.Rows[0]["Payment"].ToString());
                txtPlanNo.Text = What.Rows[0]["PlanNo"].ToString();

                Load_SurveyVisibles();
            }
            else
            {
                Initialize_Development_Fields();
            }
        }
        public void Load_Density()
        {
            _clsSurvey.theData.ConnectionString = theConnection;
            dtDensity = _clsSurvey.get_Density(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Dev");
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
        public void Initialize_Development_Fields()
        {
            int MaxSeqNo = 0;
            _clsSurvey.theData.ConnectionString = theConnection;
            dtMaxSeqNo = _clsSurvey.get_MaxSeqNo(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Dev");

            if (dtMaxSeqNo.Rows.Count != 0)
                MaxSeqNo = Convert.ToInt32(dtMaxSeqNo.Rows[0]["SeqNo"].ToString());
            if (MaxSeqNo == 0)
                TheWP = "N";
            else
                TheWP = "Y";

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

            Load_SurveyVisibles();

            txtMeasWidth.Text = "0.0";
            txtMeasHeight.Text = "0.0";
            txtHeight.Text = "0";
            txtReefWaste.Text = "";

            dtEndTypes = _clsSurvey.get_EndTypes(lblWorkplaceID.Text);

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

            txtDip.Text = "0";
            txtBallDepth.Text = "0.0";
            luMiningMethod.EditValue = 1;
            luIndicator.EditValue = 1;

            cmbMCrew.EditValue = null;
            cmbACrew.EditValue = null;
            cmbECrew.EditValue = null;
            cmbCleaningCrew.EditValue = null;
            cmbTrammingCrew.EditValue = null;
            cmbHlgeMaintainCrew.EditValue = null;
            cmbRiggerCrew.EditValue = null;
            cmbRseCleaningCrew.EditValue = null;

            luDensity.EditValue = "RD";
            luPegNumbers.EditValue = null; ;
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
            luCubicTypes.EditValue = null;
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
            luCleanTypes.EditValue = null;
            txtCleanSqm.Text = "0";
            txtCleanDist.Text = "0.0";
            txtCleanVamp.Text = "0.0";
            txtCleanTons.Text = "0";
            txtCleangt.Text = "0.00";
            txtCleanContents.Text = "0.000";
            txtTonsCubicsReef.EditValue = "0";
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
            rdgrpPayments.EditValue = 3;

            txtPlanNo.Text = "";

            dtPlanCrews = _clsSurvey.get_PlanCrews(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text, "Dev");

            if (dtPlanCrews.Rows.Count != 0)
            {
                cmbMCrew.EditValue = dtPlanCrews.Rows[0]["OrgUnitDay"].ToString();
                cmbACrew.EditValue = dtPlanCrews.Rows[0]["OrgUnitAfternoon"].ToString();
                cmbECrew.EditValue = dtPlanCrews.Rows[0]["OrgUnitNight"].ToString();
            }


            dtPlanDates = _clsSurvey.get_PlanDates(lblProdMonth.Text, lblContractor.Text, lblWorkplaceID.Text);

            if ((dtPlanDates != null) &&
                (dtPlanDates.Rows.Count > 0) &&
                (dtPlanDates.Rows[0]["CD"].ToString().Trim() != ""))
            {
                CalendarDate.Value = Convert.ToDateTime(dtPlanDates.Rows[0]["CD"].ToString());
            }
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
                    case "OpenUpSquareMeter":
                    case "ReefDevlopmentSquareMeter":
                        label133.Visible = blnConfigurationValue;
                        label130.Visible = blnConfigurationValue;
                        txtOpenUpSqm.Visible = blnConfigurationValue;
                        label129.Visible = blnConfigurationValue;
                        txtReefDevSqm.Visible = blnConfigurationValue;
                        label23.Visible = blnConfigurationValue;
                        break;
                    case "OpenUpCentimeterGramPerTon":
                    case "ReefDevelopmentCentimeterGramPerTon":
                        label132.Visible = blnConfigurationValue;
                        label128.Visible = blnConfigurationValue;
                        txtOpenUpcmgt.Visible = blnConfigurationValue;
                        label127.Visible = blnConfigurationValue;
                        txtReefDevcmgt.Visible = blnConfigurationValue;
                        label23.Visible = blnConfigurationValue;
                        break;
                    case "OpenUpFaceLength":
                    case "ReefDevelopmentFaceLength":
                        label131.Visible = blnConfigurationValue;
                        label126.Visible = blnConfigurationValue;
                        txtOpenUpFL.Visible = blnConfigurationValue;
                        label110.Visible = blnConfigurationValue;
                        txtReefDevFL.Visible = blnConfigurationValue;
                        label23.Visible = blnConfigurationValue;
                        break;
                    case "OpenUpEquipped":
                    case "ReefDevelopmentEquipped":
                        label113.Visible = blnConfigurationValue;
                        label120.Visible = blnConfigurationValue;
                        txtOpenUpEquip.Visible = blnConfigurationValue;
                        label119.Visible = blnConfigurationValue;
                        txtReefDevEquip.Visible = blnConfigurationValue;
                        label23.Visible = blnConfigurationValue;
                        break;
                    default:
                        break;
                }
            }
        }

        private void luDensity_EditValueChanged(object sender, EventArgs e)
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
            Calc_Tons();
        }

        private void ucSurveyDev_Load_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtMeasHeight_Enter(object sender, EventArgs e)
        {
            txtMeasHeight.SelectAll();
        }

        private void txtMeasWidth_Enter(object sender, EventArgs e)
        {
            txtMeasWidth.SelectAll();
        }

        private void txtPlanHeight_Enter(object sender, EventArgs e)
        {
            txtPlanHeight.SelectAll();
        }

        private void txtPlanWidth_Enter(object sender, EventArgs e)
        {
            txtPlanWidth.SelectAll();
        }

        private void txtReefmetres_Enter(object sender, EventArgs e)
        {
            txtReefmetres.SelectAll();
        }

        private void txtWastemetres_Enter(object sender, EventArgs e)
        {
            txtWastemetres.SelectAll();
        }

        private void txtTotalmetres_Enter(object sender, EventArgs e)
        {
            txtTotalmetres.SelectAll();
        }

        private void txtLabour_Enter(object sender, EventArgs e)
        {
            txtLabour.SelectAll();
        }

        private void txtCW_Enter(object sender, EventArgs e)
        {
            txtCW.SelectAll();
        }

        private void txtGT_Enter(object sender, EventArgs e)
        {
            txtGT.SelectAll();
        }

        private void txtCubicsmetres_Enter(object sender, EventArgs e)
        {
            txtCubicsmetres.SelectAll();
        }

        private void txtCubicsReef_Enter(object sender, EventArgs e)
        {
            txtCubicsReef.SelectAll();
        }

        private void txtCubicsWaste_Enter(object sender, EventArgs e)
        {
            txtCubicsWaste.SelectAll();
        }

        private void txtOpenUpSqm_Enter(object sender, EventArgs e)
        {
            txtOpenUpSqm.SelectAll();
        }

        private void txtReefDevSqm_Enter(object sender, EventArgs e)
        {
            txtReefDevSqm.SelectAll();
        }

        private void txtOpenUpcmgt_Enter(object sender, EventArgs e)
        {
            txtOpenUpcmgt.SelectAll();
        }

        private void txtReefDevcmgt_Enter(object sender, EventArgs e)
        {
            txtReefDevcmgt.SelectAll();
        }

        private void txtOpenUpFL_Enter(object sender, EventArgs e)
        {
            txtOpenUpFL.SelectAll();
        }

        private void txtReefDevFL_Enter(object sender, EventArgs e)
        {
            txtReefDevFL.SelectAll();
        }

        private void txtOpenUpEquip_Enter(object sender, EventArgs e)
        {
            txtOpenUpEquip.SelectAll();
        }

        private void txtReefDevEquip_Enter(object sender, EventArgs e)
        {
            txtReefDevEquip.SelectAll();
        }

        private void txtCleanSqm_Enter(object sender, EventArgs e)
        {
            txtCleanSqm.SelectAll();
        }

        private void txtCleanDist_Enter(object sender, EventArgs e)
        {
            txtCleanDist.SelectAll();
        }

        private void txtCleanVamp_Enter(object sender, EventArgs e)
        {
            txtCleanVamp.SelectAll();
        }

        private void txtCleanTons_Enter(object sender, EventArgs e)
        {
            txtCleanTons.SelectAll();
        }

        private void txtCleangt_Enter(object sender, EventArgs e)
        {
            txtCleangt.SelectAll();
        }

        private void txtCleanContents_Enter(object sender, EventArgs e)
        {
            txtCleanContents.SelectAll();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
