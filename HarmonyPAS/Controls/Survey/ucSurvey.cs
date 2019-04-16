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
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.Production.Controls.Survey
{
    public partial class ucSurvey : ucBaseUserControl
    {
        private Mineware.Systems.Global.sysMessages.sysMessagesClass _sysMessagesClass = new Global.sysMessages.sysMessagesClass();
        clsSurvey _clsSurvey = new clsSurvey();
        ucSurveyDev SurveyDev = new ucSurveyDev();
        ucSurveyStp SurveyStp = new ucSurveyStp();
        ucWorkplaceAdd _WorkplaceAdd = new ucWorkplaceAdd();

        private DataTable dtSysset;
        private DataTable dtSections;
        private DataTable dtStoping;
        private DataTable dtDevelopment;
        private DataTable dtSurvey;
        private DataTable dtPlan;
        private DataTable dtDeletePlan;

        private DataTable dtContractors;
        private DataTable dtMineMethods;
        private DataTable dtStopeTypes;
        private DataTable dtIndicators;
        private DataTable dtCrews;
        private DataTable dtCubicTypes;
        private DataTable dtCleanTypes;
        private DataTable dtDestinations;
        private DataTable dtCheckLockStatus;
        private DataTable dtEndTypes;
        private DataTable dtPlanCrews;
        private DataTable dtPlanDates;

        private string _prodmonth;
        private string _WPID;
        private string _WPDesc;

        private bool _clickStp;
        private bool _clickDev;

        private bool _foundStp;
        private bool _foundDev;

        public ucSurvey()
        {
            InitializeComponent();
        }

        private void ucSurvey_Load(object sender, EventArgs e)
        {
            rpgWorkplaces.Enabled = false;
            _prodmonth = String.Format("{0:yyyyMM}", DateTime.Now);

            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtSysset = _clsSurvey.get_Sysset();
            if (dtSysset.Rows.Count > 0)
            {
                _prodmonth = dtSysset.Rows[0]["CurrentProductionMonth"].ToString();
            }
            barProdMonth.EditValue = ProductionGlobal.TProductionGlobal.ProdMonthAsDate(_prodmonth);//getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tabType.Visible = false;
            _foundStp = false;
            _foundDev = false;
            _WPID = "";
            if (barSectionID.EditValue != null)
            {
                if (barSectionID.EditValue.ToString() != "")
                {
                    _clickStp = false;
                    _clickDev = false;
                    rpgWorkplaces.Enabled = true;
                    _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    dtStoping = _clsSurvey.get_Stoping(_prodmonth, barSectionID.EditValue.ToString());
                    if (dtStoping.Rows.Count > 0)
                    {
                        _foundStp = true;
                        gcStoping.DataSource = dtStoping;
                    }

                    dtDevelopment = _clsSurvey.get_Development(_prodmonth, barSectionID.EditValue.ToString());
                    if (dtDevelopment.Rows.Count > 0)
                    {
                        _foundDev = true;
                        gcDevelopment.DataSource = dtDevelopment;
                    }
                    if (dtDevelopment.Rows.Count > 0)
                    {
                        tabType.SelectedPage = tabDevelopment;
                        tabDevelopment.PageVisible = true;
                        tabType.Visible = true;
                    }
                    else
                    {
                        tabDevelopment.PageVisible = false;
                    }
                    if (dtStoping.Rows.Count > 0)
                    {
                        tabType.SelectedPage = tabStoping;
                        tabStoping.PageVisible = true;
                        tabType.Visible = true;
                    }
                    else
                    {
                        tabStoping.PageVisible = false;
                    }
                    if ((dtStoping.Rows.Count == 0) &&
                        (dtDevelopment.Rows.Count == 0))
                        MessageBox.Show("There are no Planning for Section "+barSectionID.EditValue.ToString() ,"No Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select a section", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a section", "Invalid Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }
        private void btnAddWP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_clickStp = true;
            _WorkplaceAdd.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _WorkplaceAdd.ProdMonth = _prodmonth;
            _WorkplaceAdd.SectionID = barSectionID.EditValue.ToString();
            _WorkplaceAdd.lbWorkplace.Text = "";
            _WorkplaceAdd.lbWorkplaceID.Text = "";
            if (tabType.SelectedPage == tabStoping)
                _WorkplaceAdd.Activity = "0";
            else
                _WorkplaceAdd.Activity = "1";

            _WorkplaceAdd.ShowDialog();
            btnShow_ItemClick(null, null);
        }

        private void btnDeleteWP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string strActivity = "";
            if (_WPID != "")
            {
                if (tabType.SelectedPage == tabStoping)
                    strActivity = "Stp";
                else
                    strActivity = "Dev";

                if (MessageBox.Show("Are you sure you want to delete the workplace?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dtSurvey = _clsSurvey.find_Survey(_prodmonth, barSectionID.EditValue.ToString(), _WPID, strActivity);
                    if (dtSurvey != null)
                        if (Convert.ToInt32(dtSurvey.Rows[0]["cntSurbvey"].ToString()) != 0)
                            MessageBox.Show("There are surveys attached to selected workplace. Please delete the survey first?", "Surveys Linked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            dtPlan = _clsSurvey.find_Plan(_prodmonth, barSectionID.EditValue.ToString(), _WPID, strActivity);

                            if (dtPlan.Rows.Count > 0
                                && (dtPlan.Rows[0]["SurveyAdded"].ToString() == "Y"))
                            {
                                bool deleteWP = false;
                                deleteWP = _clsSurvey.delete_WP(_prodmonth, barSectionID.EditValue.ToString(), _WPID, strActivity);
                                if (deleteWP == true)
                                {
                                    Global.sysNotification.TsysNotification.showNotification("Data Deleted", "WP Deleted", Color.CornflowerBlue);
                                    tabType.Visible = false;
                                    rpgWorkplaces.Enabled = false;
                                    btnShow_ItemClick(null, null);
                                }
                                else
                                    Global.sysNotification.TsysNotification.showNotification("Data Not Deleted", "WP Deleted not Deleted", Color.CornflowerBlue);
                            }
                            else
                            {
                                MessageBox.Show("Workplace cannot be deleted as it is not added via survey screen.", "Invalid Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }

                }
            }
            else
                MessageBox.Show("Please select a workplace to delete", "Select Workplace", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void barProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            tabType.Visible = false;
            rpgWorkplaces.Enabled = false;
            DateTime dd = Convert.ToDateTime(barProdMonth.EditValue.ToString());
            _prodmonth = dd.ToString("yyyyMM");
            barSectionID.EditValue = null;
            Load_Sections();
        }

        private void rpProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            tabType.Visible = false;
            rpgWorkplaces.Enabled = false;
            barSectionID.EditValue = null;
        }

        private void barSections_EditValueChanged(object sender, EventArgs e)
        {
            tabType.Visible = false;
            rpgWorkplaces.Enabled = false;
        }
        private void Load_Sections()
        {
            dtSections = _clsSurvey.get_Sections(_prodmonth);
            rpSectionID.DataSource = dtSections;
            rpSectionID.ValueMember = "SectionID";
            rpSectionID.DisplayMember = "Name";
        }
        private void gvStoping_DoubleClick(object sender, EventArgs e)
        {
            _clickStp = true;
            SurveyStp.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            SurveyStp.lblProdMonth.Text = _prodmonth;
            SurveyStp.lblWorkplaceID.Text = _WPID;
            SurveyStp.lblWorkplaceName.Text = _WPDesc;
            SurveyStp.lblContractor.Text = barSectionID.EditValue.ToString();
            Load_Contractors();
            Load_MineMethods();
            Load_StopeTypes();
            Load_Crews();
            Load_CubicTypes("Stp");
            Load_CleanTypes("Stp");
            SurveyStp.Load_Entries();
            Load_Destination();
            SurveyStp.Load_SurveyStoping();
            SurveyStp.Load_Density();
            CheckLockedStatus();
            SurveyStp.ShowDialog();

            btnShow_ItemClick(null, null);
        }
        private void gvDevelopment_DoubleClick(object sender, EventArgs e)
        {
            _clickDev = true;
            SurveyDev.theConnection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            SurveyDev.lblProdMonth.Text = _prodmonth;
            SurveyDev.lblWorkplaceID.Text = _WPID;
            SurveyDev.lblWorkplaceName.Text = _WPDesc;
            SurveyDev.lblContractor.Text = barSectionID.EditValue.ToString();
            Load_Contractors();
            SurveyDev.lblProdMonth.Text = _prodmonth;
            SurveyDev.lblWorkplaceID.Text = _WPID;
            SurveyDev.lblWorkplaceName.Text = _WPDesc;
            SurveyDev.lblContractor.Text = barSectionID.EditValue.ToString();
            Load_Contractors();
            Load_MineMethods();
            Load_Indicators();
            Load_Crews();
            SurveyDev.Load_PegNumbers();
            Load_CubicTypes("Dev");
            Load_CleanTypes("Dev");
            SurveyDev.Load_Entries();
            Load_Destination();
            SurveyDev.Load_SurveyDevelopment();
            SurveyDev.Load_Density();
            CheckLockedStatus();
            SurveyDev.ShowDialog();

            btnShow_ItemClick(null, null);
        }
        private void gvDevelopment_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            _WPID = "";
            if (gvDevelopment.GetRowCellValue(gvDevelopment.FocusedRowHandle, gvDevelopment.Columns["WorkplaceID"]) != null)
            {
                var WorkplaceID = gvDevelopment.GetRowCellValue(gvDevelopment.FocusedRowHandle, gvDevelopment.Columns["WorkplaceID"]);
                _WPID = WorkplaceID.ToString();
            }
            _WPDesc = "";
            if (gvDevelopment.GetRowCellValue(gvDevelopment.FocusedRowHandle, gvDevelopment.Columns["Workplace"]) != null)
            {
                var Workplace = gvDevelopment.GetRowCellValue(gvDevelopment.FocusedRowHandle, gvDevelopment.Columns["Workplace"]);
                _WPDesc = Workplace.ToString();
            }
        }
        private void Load_Contractors()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtContractors = _clsSurvey.get_Contractors(_prodmonth, barSectionID.EditValue.ToString());

            if (dtContractors.Rows.Count != 0)
            {
                if (_clickDev == true)
                    SurveyDev.lblSection.Text = dtContractors.Rows[0]["SectionID"].ToString();
                else
                    SurveyStp.lblSection.Text = dtContractors.Rows[0]["SectionID"].ToString();
            }
        }
        private void Load_MineMethods()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtMineMethods = _clsSurvey.get_MineMethods();
            if (_clickDev == true)
            {
                SurveyDev.luMiningMethod.Properties.DataSource = dtMineMethods;
                SurveyDev.luMiningMethod.Properties.DisplayMember = "MethodDesc";
                SurveyDev.luMiningMethod.Properties.ValueMember = "MethodID";
            }
            else
            {
                SurveyStp.luMiningMethod.Properties.DataSource = dtMineMethods;
                SurveyStp.luMiningMethod.Properties.DisplayMember = "MethodDesc";
                SurveyStp.luMiningMethod.Properties.ValueMember = "MethodID";
            }
        }
        private void Load_StopeTypes()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtStopeTypes = _clsSurvey.get_StopeTypes();

            SurveyStp.luStopeType.Properties.DataSource = dtStopeTypes;
            SurveyStp.luStopeType.Properties.DisplayMember = "StopeTypeDesc";
            SurveyStp.luStopeType.Properties.ValueMember = "StopeTypeID";
        }
        private void Load_Indicators()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtIndicators = _clsSurvey.get_Indicators();

            SurveyDev.luIndicator.Properties.DataSource = dtIndicators;
            SurveyDev.luIndicator.Properties.DisplayMember = "IndicatorDesc";
            SurveyDev.luIndicator.Properties.ValueMember = "IndicatorID";
        }
        private void Load_Crews()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtCrews = _clsSurvey.get_Crews("2017-03-16");

            if (_clickDev == true)
            {
                SurveyDev.cmbMCrew.Properties.DataSource = dtCrews;
                SurveyDev.cmbMCrew.Properties.DisplayMember = "GangNo";
                SurveyDev.cmbMCrew.Properties.ValueMember = "GangNo";

                SurveyDev.cmbACrew.Properties.DataSource = dtCrews;
                SurveyDev.cmbACrew.Properties.DisplayMember = "GangNo";
                SurveyDev.cmbACrew.Properties.ValueMember = "GangNo";

                SurveyDev.cmbECrew.Properties.DataSource = dtCrews;
                SurveyDev.cmbECrew.Properties.DisplayMember = "GangNo";
                SurveyDev.cmbECrew.Properties.ValueMember = "GangNo";

                SurveyDev.cmbCleaningCrew.Properties.DataSource = dtCrews;
                SurveyDev.cmbCleaningCrew.Properties.DisplayMember = "GangNo";
                SurveyDev.cmbCleaningCrew.Properties.ValueMember = "GangNo";

                SurveyDev.cmbTrammingCrew.Properties.DataSource = dtCrews;
                SurveyDev.cmbTrammingCrew.Properties.DisplayMember = "GangNo";
                SurveyDev.cmbTrammingCrew.Properties.ValueMember = "GangNo";

                SurveyDev.cmbHlgeMaintainCrew.Properties.DataSource = dtCrews;
                SurveyDev.cmbHlgeMaintainCrew.Properties.DisplayMember = "GangNo";
                SurveyDev.cmbHlgeMaintainCrew.Properties.ValueMember = "GangNo";

                SurveyDev.cmbRiggerCrew.Properties.DataSource = dtCrews;
                SurveyDev.cmbRiggerCrew.Properties.DisplayMember = "GangNo";
                SurveyDev.cmbRiggerCrew.Properties.ValueMember = "GangNo";

                SurveyDev.cmbRseCleaningCrew.Properties.DataSource = dtCrews;
                SurveyDev.cmbRseCleaningCrew.Properties.DisplayMember = "GangNo";
                SurveyDev.cmbRseCleaningCrew.Properties.ValueMember = "GangNo";
            }
            else
            {
                SurveyStp.cmbMCrew.Properties.DataSource = dtCrews;
                SurveyStp.cmbMCrew.Properties.DisplayMember = "GangNo";
                SurveyStp.cmbMCrew.Properties.ValueMember = "GangNo";

                SurveyStp.cmbACrew.Properties.DataSource = dtCrews;
                SurveyStp.cmbACrew.Properties.DisplayMember = "GangNo";
                SurveyStp.cmbACrew.Properties.ValueMember = "GangNo";

                SurveyStp.cmbECrew.Properties.DataSource = dtCrews;
                SurveyStp.cmbECrew.Properties.DisplayMember = "GangNo";
                SurveyStp.cmbECrew.Properties.ValueMember = "GangNo";

                SurveyStp.cmbCleaningCrew.Properties.DataSource = dtCrews;
                SurveyStp.cmbCleaningCrew.Properties.DisplayMember = "GangNo";
                SurveyStp.cmbCleaningCrew.Properties.ValueMember = "GangNo";

                SurveyStp.cmbTrammingCrew.Properties.DataSource = dtCrews;
                SurveyStp.cmbTrammingCrew.Properties.DisplayMember = "GangNo";
                SurveyStp.cmbTrammingCrew.Properties.ValueMember = "GangNo";

                SurveyStp.cmbHlgeMaintainCrew.Properties.DataSource = dtCrews;
                SurveyStp.cmbHlgeMaintainCrew.Properties.DisplayMember = "GangNo";
                SurveyStp.cmbHlgeMaintainCrew.Properties.ValueMember = "GangNo";

                SurveyStp.cmbRiggerCrew.Properties.DataSource = dtCrews;
                SurveyStp.cmbRiggerCrew.Properties.DisplayMember = "GangNo";
                SurveyStp.cmbRiggerCrew.Properties.ValueMember = "GangNo";

                SurveyStp.cmbRseCleaningCrew.Properties.DataSource = dtCrews;
                SurveyStp.cmbRseCleaningCrew.Properties.DisplayMember = "GangNo";
                SurveyStp.cmbRseCleaningCrew.Properties.ValueMember = "GangNo";
            }
        }


        private void Load_CubicTypes(string _act)
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtCubicTypes = _clsSurvey.get_CubicTypes(_act);

            if (_clickDev == true)
            {
                SurveyDev.luCubicTypes.Properties.DataSource = dtCubicTypes;
                SurveyDev.luCubicTypes.Properties.DisplayMember = "CubicTypeDesc";
                SurveyDev.luCubicTypes.Properties.ValueMember = "CubicTypeID";
            }
            else
            {
                SurveyStp.luCubicTypes.Properties.DataSource = dtCubicTypes;
                SurveyStp.luCubicTypes.Properties.DisplayMember = "CubicTypeDesc";
                SurveyStp.luCubicTypes.Properties.ValueMember = "CubicTypeID";
            }
        }
        private void Load_CleanTypes(string _act)
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtCleanTypes = _clsSurvey.get_CleanTypes(_act);

            if (_act == "Dev")
            {
                SurveyDev.luCleanTypes.Properties.DataSource = dtCleanTypes;
                SurveyDev.luCleanTypes.Properties.DisplayMember = "CleanTypeDesc";
                SurveyDev.luCleanTypes.Properties.ValueMember = "CleanTypeID";
            }
            else
            {
                SurveyStp.luCleanTypes.Properties.DataSource = dtCleanTypes;
                SurveyStp.luCleanTypes.Properties.DisplayMember = "CleanTypeDesc";
                SurveyStp.luCleanTypes.Properties.ValueMember = "CleanTypeID";
            }
        }

        private void Load_Destination()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtDestinations = _clsSurvey.get_Destinations();
            
            if (dtDestinations.Rows.Count != 0)
            {
                if (_clickDev == true)
                {
                    SurveyDev.rdgrpDestination.Properties.Items.Clear();
                    foreach (DataRow r in dtDestinations.Rows)
                    {
                        RadioGroupItem item1 = new RadioGroupItem();
                        item1.Description = r["Name"].ToString();
                        item1.Value = r["OreflowID"].ToString();
                        SurveyDev.rdgrpDestination.Properties.Items.Add(item1);
                    }
                    SurveyDev.luOreflowID.Properties.DataSource = dtDestinations;
                    SurveyDev.luOreflowID.Properties.DisplayMember = "Name";
                    SurveyDev.luOreflowID.Properties.ValueMember = "OreflowID";
                }
                else
                {
                    SurveyStp.rdgrpDestination.Properties.Items.Clear();
                    foreach (DataRow r in dtDestinations.Rows)
                    {
                        RadioGroupItem item1 = new RadioGroupItem();
                        item1.Description = r["Name"].ToString();
                        item1.Value = r["OreflowID"].ToString();
                        SurveyStp.rdgrpDestination.Properties.Items.Add(item1);
                    }
                    SurveyStp.luOreflowID.Properties.DataSource = dtDestinations;
                    SurveyStp.luOreflowID.Properties.DisplayMember = "Name";
                    SurveyStp.luOreflowID.Properties.ValueMember = "OreflowID";
                }
            }
        }
        protected void CheckLockedStatus()
        {
            _clsSurvey.theData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            dtCheckLockStatus = _clsSurvey.get_CheckLockedStatus(_prodmonth);

            if (dtCheckLockStatus.Rows.Count != 0)
            {
                if (Convert.ToBoolean(dtCheckLockStatus.Rows[0][0]))
                {
                    if (_clickDev == true)
                    {
                        SurveyDev.btnAdd.Enabled = false;
                        SurveyDev.btnDelete.Enabled = false;
                        SurveyDev.btnSave.Enabled = false;
                        SurveyDev.btnSaveClose.Enabled = false;
                    }
                    else
                    {
                        SurveyStp.btnAdd.Enabled = false;
                        SurveyStp.btnDelete.Enabled = false;
                        SurveyStp.btnSave.Enabled = false;
                        SurveyStp.btnSaveClose.Enabled = false;
                    }
                }
            }
        }

        private void gvStoping_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            _WPID = "";
            if (gvStoping.GetRowCellValue(gvStoping.FocusedRowHandle, gvStoping.Columns["WorkplaceID"]) != null)
            {
                var WorkplaceID = gvStoping.GetRowCellValue(gvStoping.FocusedRowHandle, gvStoping.Columns["WorkplaceID"]);
                _WPID = WorkplaceID.ToString();
            }
            _WPDesc = "";
            if (gvStoping.GetRowCellValue(gvStoping.FocusedRowHandle, gvStoping.Columns["Workplace"]) != null)
            {
                var Workplace = gvStoping.GetRowCellValue(gvStoping.FocusedRowHandle, gvStoping.Columns["Workplace"]);
                _WPDesc = Workplace.ToString();
            }
        }

        private void rpSectionID_EditValueChanged(object sender, EventArgs e)
        {
            tabType.Visible = false;
        }
    }
}
