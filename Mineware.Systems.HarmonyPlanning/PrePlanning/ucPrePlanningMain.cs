using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using FastReport;
using System.Data.SqlClient;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Planning.PrePlanning.ChangeOfPlan;
using DevExpress.XtraEditors.DXErrorProvider;
using Mineware.Systems.GlobalConnect;


namespace Mineware.Systems.Planning.PrePlanning
{
    public partial class ucPrePlanningMain : ucBaseUserControl
    {
        public PlanningDefaults PlanningClass;
        public int mainpass;
        public string abc1;
        public bool rvpm;
        bool canLoad;
        bool cyceleresult = true;
        bool labourresult;
        public bool saved = false;

        decimal theNewMonth = 0;
        ucPreplanning ucPreplanning;
        ucApprovePlanning ucApprovePlanning;
        ucUnApprovePlanning ucUnApprovePlanning;
        ucStopWorkplace ucStopWorkplace;
        ucAddWorkplace ucAddWorkplace;
        replanningType planninType;
        ucCrewMinerChange ucCrewMinerChange;
        ucPlanningValueChanges ucPlanningValueChanges;
        ucRequeststatus ucrequeststatus;
        MoveBookings ucMoveBookings;
        StartWorkplace ucStartWP;
        ucMiningMethodChange miningMethodChange;
        ucDrillRiggChanges drillRigChange;
        ucDeleteWorkplace DeleteWorkplace;
        TUserProduction theUser = new TUserProduction();
        Mineware.Systems.Global.sysMessages.sysMessagesClass theMessage = new Global.sysMessages.sysMessagesClass();
        private DataTable dtProdmonth;
        public ucPrePlanningMain()
        {
            InitializeComponent();
        }

        public override void setSecurity()
        {
            updateSecurity();
        }

        public ucPrePlanningMain(bool isRevised)
        {
            PlanningClass = new PlanningDefaults();
            PlanningClass.isRevised = isRevised;
            PlanningClass.systemDBTag = this.theSystemDBTag;
            InitializeComponent();

        }

        private void editProductionMonth_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            theNewMonth = Convert.ToInt32(e.NewValue.ToString());
        }

        private void editProductionMonth1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            theNewMonth = Convert.ToInt32(e.NewValue.ToString());
        }

        private bool validateSelections()
        {
            bool theResult = false;
            if (barSectionPlanning.EditValue == null)
            {
                theResult = false;
                theMessage.viewMessage(MessageType.Info, "INVALID SECTION", "Please select a valid section", ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theResult;
            }
            else theResult = true;

            if (barActivityPlanning.EditValue == null)
            {
                theResult = false;
                theMessage.viewMessage(MessageType.Info, "INVALID ACTIVITY", "Please select a valid activity", ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theResult;
            }
            else theResult = true;

            return theResult;
        }

        public bool validateSelectionsRevised()
        {
            bool theResult = false;
            if (barSectionRevised.EditValue == null)
            {
                theResult = false;
                theMessage.viewMessage(MessageType.Info, "INVALID SECTION", "Please select a valid section", ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theResult;
            }
            else theResult = true;

            if (barActivity1.EditValue == null)
            {
                theResult = false;
                theMessage.viewMessage(MessageType.Info, "INVALID ACTIVITY", "Please select a valid activity", ButtonTypes.OK, MessageDisplayType.FullScreen);
                return theResult;
            }
            else theResult = true;
            return theResult;
        }

        private void btnShowPlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // added if statement to still load old dev screen 
            if (Convert.ToInt16(barActivityPlanning.EditValue.ToString()) == 0)
            {
                #region RegionName
                if (validateSelections())
                {
                    // New class to set selected planning settings i
                    if (PlanningClass.tblPlanningData != null)
                    {
                        PlanningClass.tblPlanningData.Rows.Clear();
                    }
                    if (PlanningClass.tblPlanningCycleData != null)
                    {
                        PlanningClass.tblPlanningCycleData.Rows.Clear();
                    }
                    PlanningClass.PlanningCycle = new PlanningCycle();
                    PlanningClass.PlanningSettings.ActivityID = Convert.ToInt16(barActivityPlanning.EditValue.ToString());
                    PlanningClass.PlanningSettings.ActivityString = editActivity.GetDisplayText(PlanningClass.PlanningSettings
                        .ActivityID);
                    PlanningClass.PlanningSettings.MOSectionID = barSectionPlanning.EditValue.ToString();
                    PlanningClass.PlanningSettings.SectionName = editSections.GetDisplayText(PlanningClass.PlanningSettings
                        .MOSectionID);
                    PlanningClass.PlanningSettings.ProdMonth = TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue
                        .ToString()));
                    PlanningClass.PlanningSettings.IsRevised = false;
                    //TODO: Need to get the location where DefaultDailyAdvance is saved in DB DvdB 2019/02/21
                    PlanningClass.PlanningSettings.DefaultAdvance = 0.8;

                    clsResults calValid = PlanningClass.ValidateSectionCalender();
                    if (!calValid.Successfull)
                    {
                        theMessage.viewMessage(MessageType.Warning,
                                               "INVALID CALENDARS",
                                               calValid.Message.ToString(),
                                               ButtonTypes.OK,
                                               MessageDisplayType.FullScreen);
                        return;
                    }
                    PlanningClass.LoadMinerList();
                    PlanningClass.LoadPlanning();
                    PlanningClass.LoadPlanningCycle();
                    PlanningClass.LoadOrgUnits();
                    PlanningClass.LoadMiningMethod();
                    switch (PlanningClass.PlanningSettings.ActivityID)
                    {
                        case 0:
                            PlanningClass.StopePrePlanning = new ucStopePrePlanning();
                            PlanningClass.StopePrePlanning.Dock = DockStyle.Fill;
                            PlanningClass.StopePrePlanning.SetPlanningClass(PlanningClass);
                            parentControle.Controls.Add(PlanningClass.StopePrePlanning);

                            break;
                    }

                    updateSecurity();
                    loadMenuItems();
                }
                #endregion
            }
            else
            {
                #region Old Code
                if (validateSelections())
                {
                    PlanningClass.PlanningSettings.ActivityID = Convert.ToInt16(barActivityPlanning.EditValue.ToString());
                    PlanningClass.PlanningSettings.ActivityString = editActivity.GetDisplayText(PlanningClass.PlanningSettings
                        .ActivityID);
                    PlanningClass.PlanningSettings.MOSectionID = barSectionPlanning.EditValue.ToString();
                    PlanningClass.PlanningSettings.SectionName = editSections.GetDisplayText(PlanningClass.PlanningSettings
                        .MOSectionID);
                    PlanningClass.PlanningSettings.ProdMonth = TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue
                        .ToString()));
                    PlanningClass.PlanningSettings.IsRevised = false;

                    PlanningClass.Activity = Convert.ToInt16(barActivityPlanning.EditValue.ToString());
                    PlanningClass.SectionID = barSectionPlanning.EditValue.ToString();
                    PlanningClass.ProdMonth = TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString()));
                    PlanningClass.isRevised = false;
                    PlanningClass.systemDBTag = this.theSystemDBTag;

                    PlanningClass.PlanningScreen.Dock = DockStyle.Fill;
                    PlanningClass.PlanningScreen.theSystemTag = this.theSystemTag;
                    PlanningClass.PlanningScreen.theSystemDBTag = this.theSystemDBTag;
                    PlanningClass.PlanningScreen.UserCurrentInfo = this.UserCurrentInfo;
                    PlanningClass.PlanningScreen.PlanningClass = this.PlanningClass;
                    canLoad = PlanningClass.PlanningScreen.loadPreplanning(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue)), barSectionPlanning.EditValue.ToString(), Convert.ToInt32(barActivityPlanning.EditValue.ToString()));

                    if (canLoad)
                    {
                        updateSecurity();
                        loadMenuItems();
                        parentControle.Controls.Add(PlanningClass.PlanningScreen);
                    }
                    else
                    {
                        updateSecurity();
                        loadMenuItems();
                        parentControle.Controls.Add(PlanningClass.PlanningScreen);
                    }
                }
                #endregion
            }

        }

        private void loadMenuItems()
        {
            if (PlanningClass.isRevised)
            {
                rpRevisedPlanningOptions.Visible = true;
                rpRevisedPlanning.Visible = false;
            }
            else
            {
                rpPreplanning.Visible = false;
                rpPreplanningManage.Visible = true;
            }
        }

        private void loadSystemSecurity()
        {
            if (!PlanningClass.isRevised)
            {
                if (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS.ItemID) == 2)
                {
                    switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miAddWorkplace_HPASPlanningWPAddWP_MinewareSystemsHarmonyPAS.ItemID))
                    {
                        case 0:
                        case 1:
                            btnAddWorkPlace.Enabled = false;
                            break;
                        case 2:
                            btnAddWorkPlace.Enabled = true;
                            break;
                    }
                    switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miDeleteWorkplace_HPASPlanningWPDelWP_MinewareSystemsHarmonyPAS.ItemID))
                    {
                        case 0:
                        case 1:
                            btnDeleteWorkplace.Enabled = false;
                            break;
                        case 2:
                            btnDeleteWorkplace.Enabled = true;
                            break;
                    }
                    switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miApproveWorkplace_HPASPlanningWPApproveWP_MinewareSystemsHarmonyPAS.ItemID))
                    {
                        case 0:
                            btnApprovePrePlanning.Enabled = false;
                            break;
                        case 1:
                        case 2:
                            btnApprovePrePlanning.Enabled = true;
                            break;
                    }
                    switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miReplaceWorkplace_HPASPlanningWPReplace_MinewareSystemsHarmonyPAS.ItemID))
                    {
                        case 0:
                        case 1:
                            btnReplaceWorkplace.Enabled = false;
                            break;
                        case 2:
                            btnReplaceWorkplace.Enabled = true;
                            break;
                    }
                    switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miUnApproveWorkplace_HPASPlanningWPUnAppWP_MinewareSystemsHarmonyPAS.ItemID))
                    {
                        case 0:
                        case 1:
                            btnUnApprovePlanning.Enabled = false;
                            break;
                        case 2:

                            btnUnApprovePlanning.Enabled = true;
                            break;
                    }
                    switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miCyclePlan_HPASPlanningCycle_MinewareSystemsHarmonyPAS.ItemID))
                    {
                        case 0:
                            btnCyclePlanning.Enabled = false;
                            break;
                        case 2:
                        case 1:
                            btnCyclePlanning.Enabled = true;
                            break;
                    }
                }
            }
            else
            {
                btnAddWorkPlace.Enabled = false;
                btnSavePlanning.Enabled = false;
                btnDeleteWorkplace.Enabled = false;
                btnReplaceWorkplace.Enabled = false;
                btnApprovePlanning.Enabled = false;
                btnUnApprovePlanning.Enabled = false;
                btnAllowWorkplace.Enabled = false;
            }
        }

        private void updateSecurity()
        {
            if (PlanningClass.Activity == 2 || PlanningClass.Activity == 8)
            {
                btnCyclePlanning.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnApprovePrePlanning.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnUnApprovePlanning.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnReplaceWorkplace.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnAllowWorkplace.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                loadSystemSecurity();
            }
            else
            {
                btnCyclePlanning.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnApprovePrePlanning.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnUnApprovePlanning.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnLabour.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnReplaceWorkplace.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnAllowWorkplace.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                loadSystemSecurity();


                if (PlanningClass.CurrentProdMonth != "")
                {
                    if (Convert.ToInt32(PlanningClass.ProdMonth) <= Convert.ToInt32(PlanningClass.CurrentProdMonth))
                    {
                        if (PlanningClass.isRevised)
                        {
                            switch (TUserInfo.theSecurityLevel(ProductionGlobal.TProductionGlobal.WPASMenuStructure.miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS.ItemID))
                            {
                                case 0:
                                case 1:
                                    btnRevisedStopWP.Enabled = false;
                                    btnRevisedAddWP.Enabled = false;
                                    btnRevisedCrewMiner.Enabled = false;
                                    btnRevisedMovePlan.Enabled = false;
                                    btnRevisedStartWP.Enabled = false;
                                    btnRevisedValues.Enabled = false;
                                    btnRevisedStatus.Enabled = true;
                                    btnMiningMethodChange.Enabled = false;
                                    btnDrillRigChange.Enabled = false;
                                    btnAllowWorkplace.Enabled = false;
                                    btnDelWorkplace.Enabled = false;
                                    break;
                                case 2:
                                    btnRevisedStopWP.Enabled = true;
                                    btnRevisedAddWP.Enabled = true;
                                    btnRevisedCrewMiner.Enabled = true;
                                    btnRevisedMovePlan.Enabled = true;
                                    btnRevisedStartWP.Enabled = true;
                                    btnRevisedValues.Enabled = true;
                                    btnRevisedStatus.Enabled = true;
                                    btnMiningMethodChange.Enabled = true;
                                    btnAllowWorkplace.Enabled = true;
                                    btnDelWorkplace.Enabled = true;

                                    if (barActivity1.EditValue.ToString() == "1")
                                    {
                                        btnDrillRigChange.Enabled = true;
                                    }
                                    else
                                    {
                                        btnDrillRigChange.Enabled = false;
                                    }
                                    break;
                            }
                            loadSystemSecurity();
                        }
                        else
                        {
                            loadSystemSecurity();
                            btnAddWorkPlace.Enabled = false;
                            btnReplaceWorkplace.Enabled = false;
                            btnUnApprovePlanning.Enabled = false;
                            btnDeleteWorkplace.Enabled = false;
                        }
                    }

                    else
                    {
                        if (canLoad == true)
                        {
                            loadSystemSecurity();
                        }
                    }
                }
            }
        }

        private void barShowBTN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barSectionPlanning.EditValue == null)
            {
                theMessage.viewMessage(MessageType.Info, "INFO NEEDED", "Please select a section to continue", ButtonTypes.OK, MessageDisplayType.FullScreen);
            }
            else
            {
                MWDataManager.clsDataAccess _RequiredPM = new MWDataManager.clsDataAccess();
                _RequiredPM.ConnectionString = TConnections.GetConnectionString(this.theSystemDBTag, UserCurrentInfo.Connection);
                _RequiredPM.SqlStatement = "sp_PrePlanning_Prodmonth";
                _RequiredPM.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                SqlParameter[] _paramCollection1 =
                {
                    _RequiredPM.CreateParameter("@SECTIONID_2", SqlDbType.VarChar , 50,barSectionPlanning.EditValue.ToString())
                };

                _RequiredPM.ParamCollection = _paramCollection1;
                _RequiredPM.queryReturnType = MWDataManager.ReturnType.DataTable;
                _RequiredPM.ExecuteInstruction();

                DataTable dt = new DataTable();
                dt = _RequiredPM.ResultsDataTable;
                int prm = Convert.ToInt32(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue)));

                updateSecurity();
                if (canLoad == false)
                {

                }
                else
                {
                    if (PlanningClass.isRevised == false)
                    {
                        if (dt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (prm < Convert.ToInt32(dr["CurrentProductionMonth"]))
                                {
                                    btnReplaceWorkplace.Enabled = false;
                                    btnSavePlanning.Enabled = true;
                                    btnApprovePrePlanning.Enabled = false;
                                    rpPreplanning.Visible = false;
                                    rpPreplanningManage.Visible = true;
                                    rpReplanningStatus.Visible = false;
                                    rpRevisedPlanning.Visible = false;
                                }
                                else if (prm > Convert.ToInt32(dr["CurrentProductionMonth"]))
                                {
                                    btnAddWorkPlace.Enabled = true;
                                    btnReplaceWorkplace.Enabled = true;
                                    btnApprovePrePlanning.Enabled = true;
                                    btnDeleteWorkplace.Enabled = true;
                                    btnSavePlanning.Enabled = true;
                                    rpPreplanning.Visible = false;
                                    rpPreplanningManage.Visible = true;
                                    rpReplanningStatus.Visible = false;
                                    rpRevisedPlanning.Visible = false;
                                }
                                else if (prm == Convert.ToInt32(dr["CurrentProductionMonth"]))
                                {
                                    btnReplaceWorkplace.Enabled = false;
                                    btnSavePlanning.Enabled = true;
                                    btnApprovePrePlanning.Enabled = false;
                                    rpPreplanning.Visible = false;
                                    rpPreplanningManage.Visible = true;
                                    rpReplanningStatus.Visible = false;
                                    rpRevisedPlanning.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            btnAddWorkPlace.Enabled = true;
                            btnReplaceWorkplace.Enabled = true;
                            btnDeleteWorkplace.Enabled = true;
                            btnSavePlanning.Enabled = true;
                            btnApprovePrePlanning.Enabled = true;
                            rpReplanning.Visible = false;
                            rpPreplanning.Visible = false;
                            rpPreplanningManage.Visible = true;
                            rpReplanningStatus.Visible = false;
                            rpRevisedPlanning.Visible = false;
                        }
                    }
                    else
                    {
                        btnAddWorkPlace.Enabled = false;
                        btnReplaceWorkplace.Enabled = false;
                        btnDeleteWorkplace.Enabled = false;
                        btnSavePlanning.Enabled = true;
                        btnApprovePrePlanning.Enabled = true;
                        rpReplanning.Visible = false;
                        rpPreplanning.Visible = false;
                        rpPreplanningManage.Visible = false;
                        rpReplanningStatus.Visible = false;
                        rpRevisedPlanning.Visible = true;
                    }
                }
            }
        }

        private void btnAddWorkPlace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // To acomedate old Planning screen for DEV
            if (PlanningClass.PlanningSettings.ActivityID == 0)
            {
                PlanningClass.addWorkPlace();
            }
            else
            {
                PlanningClass.PlanningScreen.addWorkPlace();
            }
        }

        private void btnDeleteWorkplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PlanningClass.PlanningSettings.ActivityID == 0)
            {
                PlanningClass.DeleteWorkplace();
            }
            else
            {
                PlanningClass.PlanningScreen.delteWorkPlace();
            }
        }

        private void btnSavePlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PlanningClass.PlanningSettings.ActivityID == 0)
            {
                PlanningClass.StopePrePlanning.ForceViewUpdate();
                PlanningClass.tblPlanningData.AcceptChanges();
                var checkSectionID = PlanningClass.tblPlanningData.Select("Sectionid ='-1'");
                if(checkSectionID.Length > 0)
                {
                    MessageBox.Show("All the workplaces needs to have a miner section selected, before you can save your data.");
                }
                else
                {
                    PlanningClass.StopePrePlanning.SavePlanning();
                }

            }
            else

            {
                PlanningClass.PlanningScreen.viewPlanningStoping.PostEditor();
                PlanningClass.PlanningScreen.viewPlanningDevelopment.PostEditor();
                PlanningClass.PlanningScreen.viewPlanningSundry.PostEditor();
                PlanningClass.PlanningScreen.viewPlanningSweepVamp.PostEditor();
                PlanningClass.PlanningScreen.SaveMOCycle();
                saved = true;
                return;
            }



            //PlanningClass.PlanningScreen.Insertplanning();

        }

        private void btnCancelPlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PlanningClass.PlanningSettings.ActivityID == 0)// needs to remove all workplaces with no section ID
            {
                PlanningClass.RemoveUnsavedWorkplaces();
            }
            btnCyclePlanning.Down = false;
            bool theResult = PlanningClass.PlanningScreen.canClosePlanning();
            if (theResult == true)
            {
                rpPreplanning.Visible = true;
                rpPreplanningManage.Visible = false;
                PlanningClass.PlanningScreen.Parent = null;
                PlanningClass.PlanningScreen.updateReadOnly();
                PlanningClass.SetCelarReadOnly(false);
                parentControle.Controls.Clear();
                PlanningClass.PlanningScreen.showLabour(false);
                PlanningClass.PlanningScreen.showCycle(false);
                saved = false;
            }
        }

        private void btnApprovePrePlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningClass.PlanningScreen.savePrePlanning();

            switch (PlanningClass.Activity)
            {
                case 0:
                case 1:
                    UpdateCompletedRequest += ucPrePlanningMain_UpdateCompleateRequest;

                    ucApprovePlanning = new ucApprovePlanning { Parent = parentControle, Dock = DockStyle.Fill, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                    string pm = TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString()));
                    bool canApprove = ucApprovePlanning.loadPrePlanning(TProductionGlobal.ProdMonthAsDate(pm), barSectionPlanning.EditValue.ToString(), Convert.ToInt32(barActivityPlanning.EditValue.ToString()));
                    ucApprovePlanning.UpdateCompletedRequest += ucPrePlanningMain_UpdateCompleateRequest;

                    if (canApprove == true)
                    {
                        parentControle.Visible = false;
                        parentControle.Controls.Clear();
                        ucApprovePlanning.Parent = parentControle;
                        rpPreplanningManage.Visible = false;
                        rpApprovePlanning.Visible = true;
                        parentControle.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("All the workplaces has been approved.");
                        parentControle.Visible = true;
                    }

                    break;
                case 2:
                    break;
            }
        }

        void ucPrePlanningMain_UpdateCompleateRequest(object sender, UpdateCompletedArg e)
        {
            btnApprovePlanning.Enabled = e.Done;
            btnCloseApprove.Enabled = e.Done;
            UpdateCur();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningClass.PlanningScreen.export();
        }

        private SqlParameter[] SetParamters(int ProdMonth, int activity, string section)
        {
            MWDataManager.clsDataAccess theData = new MWDataManager.clsDataAccess();

            SqlParameter[] _paramCollection =
            {
                theData.CreateParameter("@Prodmonth", SqlDbType.Int, 0,ProdMonth),
                theData.CreateParameter("@Section_2", SqlDbType.VarChar, 60,section),
                theData.CreateParameter("@Activity", SqlDbType.Int, 0,activity)
            };

            return _paramCollection;
        }

        private void PlanningReport(int ProdMonth, int activity, string section, reportAction theAction)
        {
            //SqlParameter[] _paramCollection;
            //Report PlanningReport = new Report();           
            Report theReport = new Report();
            //MWDataManager.clsDataAccess _PrePlanningReport = new MWDataManager.clsDataAccess();
            //_PrePlanningReport.ConnectionString = TConnections.GetConnectionString(this.theSystemDBTag, UserCurrentInfo.Connection);
            //_PrePlanningReport.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;
            //_PrePlanningReport.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_PrePlanningReport.SqlStatement = "Preplanning_Report";
            //switch (activity)
            //{
            //    case 0:
            //        _PrePlanningReport.ResultsTableName = "Stoping";
            //        PlanningReport.Load(TGlobalItems .ReportsFolder  + "\\PlanningReportStoping.frx");
            //        break;
            //    case 1:
            //        _PrePlanningReport.ResultsTableName = "Dev";
            //        PlanningReport.Load(TGlobalItems .ReportsFolder  + "\\PlanningReportDev.frx");
            //        break;

            //    case 2:
            //        _PrePlanningReport.ResultsTableName = "Sundry";
            //        PlanningReport.Load(TGlobalItems.ReportsFolder + "\\PlanningReportSundry.frx");
            //        break;

            //    case 8:
            //        _PrePlanningReport.ResultsTableName = "SweepVamp";
            //        PlanningReport.Load(TGlobalItems.ReportsFolder + "\\PlanningReportSweepVamp.frx");
            //        break;              
            //}           

            //_paramCollection = SetParamters(ProdMonth, activity, section);
            //_PrePlanningReport.ParamCollection = _paramCollection;
            //_PrePlanningReport.ExecuteInstruction();

            //DataSet repMODDataSet = new DataSet();
            //repMODDataSet.Tables.Add(_PrePlanningReport.ResultsDataTable);

            //PlanningReport.RegisterData(repMODDataSet);

            int prodmonth = ProdMonth;

            MWDataManager.clsDataAccess _dbManBanner = new MWDataManager.clsDataAccess();
            _dbManBanner.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManBanner.SqlStatement = " select '" + SysSettings.Banner + "', '" + section + "', '" + ProdMonth + "', '" + section + "'  ";

            _dbManBanner.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManBanner.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManBanner.ResultsTableName = "Banner";
            _dbManBanner.ExecuteInstruction();


            DataSet DsBanner = new DataSet();
            DsBanner.Tables.Add(_dbManBanner.ResultsDataTable);

            theReport.RegisterData(DsBanner);

            theReport.Load(TGlobalItems.ReportsFolder + "\\Planning.frx");


            MWDataManager.clsDataAccess _dbManDate = new MWDataManager.clsDataAccess();
            _dbManDate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " select *, DATEPART(ISOWK,BeginDate) ww  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " , DATEPART(ISOWK,BeginDate+7) ww1   ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+14) ww2  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+21) ww3  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+28) ww4  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+35) ww5  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+42) ww6  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + "  , DATEPART(ISOWK,BeginDate+49) ww7  ";






            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " from (select Min(s.BeginDate) BeginDate,MAX(s.EndDate) EndDate from SECCAL s  ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " left outer join Section sc ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " on s.Sectionid = sc.SectionID ";
            _dbManDate.SqlStatement = _dbManDate.SqlStatement + " where s.Prodmonth = '" + prodmonth + "' and sc.ReportToSectionid = '" + section + "') a ";
            _dbManDate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDate.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDate.ResultsTableName = "sysset";
            _dbManDate.ExecuteInstruction();

            //if (_dbManDate.ResultsDataTable.Rows[0][0].ToString() == 0)
            //{

            //}

            TimeSpan Span;
            DateTime BeginDate;
            DateTime EndDate;
            DataTable CalDate = new DataTable();
            int Day = 0;
            int Week = 0;
            int Weekno = 0;

            try
            {
                BeginDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("No Section Calendars found for section : " + section + " ");
                return;
            }

            BeginDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][0].ToString());
            EndDate = Convert.ToDateTime(_dbManDate.ResultsDataTable.Rows[0][1].ToString());
            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][2].ToString());

            CalDate.Rows.Add();

            for (int x = 0; x <= 45; x++)
            {
                if (BeginDate.AddDays(Day) <= EndDate)
                {

                    CalDate.Columns.Add();
                    CalDate.Rows[0][x] = BeginDate.AddDays(Day).ToString("dd MMM ddd");
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Mon")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "                W";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Tue")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "                E";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Wed")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "                E";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Thu")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "                K";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Fri")
                    {
                        CalDate.Rows[0][x] = CalDate.Rows[0][x] + "               -";
                    }
                    if (BeginDate.AddDays(Day).ToString("ddd") == "Sat")
                    {
                        // do first wwk
                        if (Weekno == 0)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][2].ToString());

                        if (Weekno == 1)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][3].ToString());

                        if (Weekno == 2)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][4].ToString());

                        if (Weekno == 3)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][5].ToString());

                        if (Weekno == 4)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][6].ToString());
                        if (Weekno == 5)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][7].ToString());
                        if (Weekno == 6)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][8].ToString());


                        // CalDate.Rows[0][x] = CalDate.Rows[0][x] + "        ";
                        if (Week >= 5000)
                        {
                            //Week = 1;

                            if (Week > 9)
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString().Substring(0, 1);

                            }
                            else
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + " " + "         0";

                            }
                        }
                        else
                        {
                            if (Week > 9)
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString().Substring(0, 1);

                            }
                            else
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + " " + "         0";

                            }
                        }



                    }

                    if (BeginDate.AddDays(Day).ToString("ddd") == "Sun")
                    {
                        if (Weekno == 0)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][2].ToString());

                        if (Weekno == 1)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][3].ToString());

                        if (Weekno == 2)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][4].ToString());

                        if (Weekno == 3)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][5].ToString());

                        if (Weekno == 4)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][6].ToString());
                        if (Weekno == 5)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][7].ToString());
                        if (Weekno == 6)
                            Week = Convert.ToInt32(_dbManDate.ResultsDataTable.Rows[0][8].ToString());

                        // CalDate.Rows[0][x] = CalDate.Rows[0][x] + "        ";
                        if (Week >= 54000)
                        {
                            // Week = 1;

                            if (Week > 9)
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString().Substring(1, 1);
                                //Week = Week + 1;

                            }
                            else
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString();
                                //Week = Week + 1;

                            }
                        }
                        else
                        {
                            if (Week > 9)
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString().Substring(1, 1);
                                // Week = Week + 1;

                            }
                            else
                            {
                                CalDate.Rows[0][x] = CalDate.Rows[0][x] + "              " + Week.ToString();
                                //Week = Week + 1;

                            }
                        }

                        Weekno = Weekno + 1;

                    }


                    // Weekno = Weekno + 1;

                    Day = Day + 1;


                }
                else
                {
                    CalDate.Columns.Add();
                    CalDate.Rows[0][x] = "";
                }

            }

            CalDate.Columns.Add();
            CalDate.Rows[0][CalDate.Columns.Count - 1] = Day.ToString();

            CalDate.TableName = "CalDates";
            DataSet DsCalDate = new DataSet();
            DsCalDate.Tables.Add(CalDate);

            theReport.RegisterData(DsCalDate);


            MWDataManager.clsDataAccess _dbManPlan = new MWDataManager.clsDataAccess();
            _dbManPlan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbManPlan.SqlStatement = _dbManPlan.SqlStatement + "declare @Start datetime      \r\n" +
"set @Start = (select      \r\n" +
"min(calendardate) ss from Planning v, section s , section s1, section s2      \r\n" +
"where v.Sectionid = s.SectionID and v.Prodmonth = s.Prodmonth      \r\n" +
" and s.ReportToSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
" and s1.ReportToSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth      \r\n" +
" and v.Prodmonth = '" + prodmonth + "' and s2.SectionID = '" + section + "')         \r\n\r\n" +

"declare @VampingStart datetime      \r\n" +
"set @VampingStart = (select      \r\n" +
"min(calendardate) ss from Planning_vamping v, section s , section s1, section s2      \r\n" +
"where v.Sectionid = s.SectionID and v.Prodmonth = s.Prodmonth      \r\n" +
" and s.ReportToSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
" and s1.ReportToSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth      \r\n" +
" and v.Prodmonth = '" + prodmonth + "' and s2.SectionID = '" + section + "')         \r\n\r\n" +

" declare @prev integer      \r\n" +
" set @prev = (select max(prodmonth) aaaa from vw_planmonth where prodmonth < '" + prodmonth + "')        \r\n\r\n" +

" select  case when newptt is null then 'Red' when newptt is not null and newptt < @prev then 'orange' else '' end as newwpflag, *from      \r\n" +
"  (select '1' Line, s1.Sectionid sbid, s1.Name sbname, s.Sectionid minid, s.Name minname, pm.workplaceid, case when pm.activity = 9 then w.description + ' Ledge' else w.description end as description,      \r\n" +
"     case when pm.Activity = 9 then 'Ledge'      \r\n" +
"    when pm.Activity = 0 then 'Stope'      \r\n" +
"    when pm.Activity = 1 then 'Dev' End as Act,      \r\n" +
" case when pm.activity = 1 then MetresAdvance else 0 end as DevTotAdv,      \r\n" +
" case when pm.activity = 1 and w.ReefWaste = 'W' then MetresAdvance else 0 end as DevWasteAdv,      \r\n" +
"  case when pm.activity = 1 then Tons else 0 end as DevTons,      \r\n" +
"  case when  pm.Activity = 1 then kg else 0 end as DevCont,      \r\n" +
" case when pm.activity <> 1 then Tons else 0 end as StpTons,      \r\n" +
"  case when pm.activity <> 1 then kg else 0 end as StpCont,      \r\n" +
"     case when pm.Activity <> 1 then 'a.Stoping' else 'b.Development' end as MainGroup,      \r\n" +
"      case when pm.Activity <> 1 then pm.FL else 0 end AS FL, 	 case when pm.Activity <> 1 then pm.Sqm else 0 end as plansqm, 	case when pm.Activity <> 1 then	cyclesqm else pm.MonthlyTotalSQM end as cyclesqm,  cr.CrewName orgunitds, 'Std Cycle' aa, pm.MetresAdvance Metresadvance, tons, content,pm.WasteSqm  offreefsqm, 0 Budget, pm.activity,      \r\n" +
"    case when pm.activity <> 1 then pm.extrabudget else 0 end as extrabudget, case when pm.activity = 1 then pm.extrabudget else 0 end as extrabudget1, SUBSTRING(DefaultCycle, 1, 4) aa1,      \r\n" +
"      SUBSTRING(DefaultCycle, 5, 4) aa2, SUBSTRING(DefaultCycle, 9, 4) aa3, SUBSTRING(DefaultCycle, 13, 4) aa4,      \r\n" +
"     SUBSTRING(DefaultCycle, 17, 4) aa5, SUBSTRING(DefaultCycle, 21, 4) aa6, SUBSTRING(DefaultCycle, 25, 4) aa7, SUBSTRING(DefaultCycle, 29, 4) aa8,      \r\n" +
"       SUBSTRING(DefaultCycle, 33, 4) aa9, SUBSTRING(DefaultCycle, 37, 4) aa10, SUBSTRING(DefaultCycle, 41, 4) aa11, SUBSTRING(DefaultCycle, 45, 4) aa12,      \r\n" +
"      SUBSTRING(DefaultCycle, 49, 4) aa13, SUBSTRING(DefaultCycle, 53, 4) aa14, SUBSTRING(DefaultCycle, 57, 4) aa15, SUBSTRING(DefaultCycle, 61, 4) aa16,      \r\n" +
"      SUBSTRING(DefaultCycle, 65, 4) aa17, SUBSTRING(DefaultCycle, 69, 4) aa18, SUBSTRING(DefaultCycle, 73, 4) aa19, SUBSTRING(DefaultCycle, 77, 4) aa20,      \r\n" +
"       SUBSTRING(DefaultCycle, 81, 4) aa21, SUBSTRING(DefaultCycle, 85, 4) aa22, SUBSTRING(DefaultCycle, 89, 4) aa23, SUBSTRING(DefaultCycle, 93, 4) aa24,      \r\n" +
"       SUBSTRING(DefaultCycle, 97, 4) aa25, SUBSTRING(DefaultCycle, 101, 4) aa26, SUBSTRING(DefaultCycle, 105, 4) aa27, SUBSTRING(DefaultCycle, 109, 4) aa28,      \r\n" +
"       SUBSTRING(DefaultCycle, 113, 4) aa29, SUBSTRING(DefaultCycle, 117, 4) aa30, SUBSTRING(DefaultCycle, 121, 4) aa31, SUBSTRING(DefaultCycle, 125, 4) aa32,      \r\n" +
"       SUBSTRING(DefaultCycle, 129, 4) aa33, SUBSTRING(DefaultCycle, 133, 4) aa34, SUBSTRING(DefaultCycle, 137, 4) aa35, SUBSTRING(DefaultCycle, 141, 4) aa36,      \r\n" +
"       SUBSTRING(DefaultCycle, 145, 4) aa37, SUBSTRING(DefaultCycle, 149, 4) aa38, SUBSTRING(DefaultCycle, 153, 4) aa39, SUBSTRING(DefaultCycle, 157, 4) aa40,      \r\n" +
"       SUBSTRING(DefaultCycle, 161, 4) aa41, SUBSTRING(DefaultCycle, 165, 4) aa42, SUBSTRING(DefaultCycle, 169, 4) aa43, SUBSTRING(DefaultCycle, 173, 4) aa44,      \r\n" +
"       SUBSTRING(DefaultCycle, 177, 4) aa45, s1.ReportToSectionid+':'+s2.Name ReportToSectionid, '' Mprass, '' wpexternalid, '' Surv, pmold1 newptt       from vw_PlanMonth pm, (select w.*, pmold1 from Workplace w left outer join(select workplaceid wz, max(prodmonth) pmold1 from vw_PlanMonth      where prodmonth < '" + prodmonth + "' group by workplaceid) newwp on  w.workplaceid = newwp.wz) w, Section s, Section s1,Section s2, Workplace wt,crew cr where pm.workplaceid = w.workplaceid \r\n" +
" -------and w.GMSIWPID = wt.GMSIWPID      \r\n" +
"       and pm.Sectionid = s.SectionID and pm.Prodmonth = s.Prodmonth      \r\n" +
"       and s.reporttoSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
" and s1.reporttoSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth   \r\n" +
"       and pm.prodmonth = '" + prodmonth + "'      and s1.ReportToSectionid = '" + section + "' and pm.Plancode = 'MP'  and pm.OrgUnitDay = cr.GangNo) a      \r\n" +
"       left outer join      \r\n" +
"       (select '2' Line, s1.Sectionid sbid, s1.Name sbname, s.Sectionid minid, s.Name minname, pm.workplaceid, w.description, pm.activity,      \r\n" +
"       case when pm.Activity = 9 then 'Ledge'      \r\n" +
"     when pm.Activity = 0 then 'Stope'      \r\n" +
"     when pm.Activity = 1 then 'Dev' End as Act,      \r\n" +
"      case when pm.Activity <> 1 then 'a.Stoping' else 'b.Development' end as MainGroup,      \r\n" +
"      pm.FL, pm.SqmTotal plansqm, cyclesqm, orgunitds, 'Crew Cycle' aa,      \r\n" +
"       SUBSTRING(MOCycle, 1, 4) a1,      \r\n" +
"       SUBSTRING(MOCycle, 5, 4) a2, SUBSTRING(MOCycle, 9, 4) a3, SUBSTRING(MOCycle, 13, 4) a4,      \r\n" +
"       SUBSTRING(MOCycle, 17, 4) a5, SUBSTRING(MOCycle, 21, 4) a6, SUBSTRING(MOCycle, 25, 4) a7, SUBSTRING(MOCycle, 29, 4) a8,      \r\n" +
"       SUBSTRING(MOCycle, 33, 4) a9, SUBSTRING(MOCycle, 37, 4) a10, SUBSTRING(MOCycle, 41, 4) a11, SUBSTRING(MOCycle, 45, 4) a12,      \r\n" +
"       SUBSTRING(MOCycle, 49, 4) a13, SUBSTRING(MOCycle, 53, 4) a14, SUBSTRING(MOCycle, 57, 4) a15, SUBSTRING(MOCycle, 61, 4) a16,      \r\n" +
"       SUBSTRING(MOCycle, 65, 4) a17, SUBSTRING(MOCycle, 69, 4) a18, SUBSTRING(MOCycle, 73, 4) a19, SUBSTRING(MOCycle, 77, 4) a20,      \r\n" +
"       SUBSTRING(MOCycle, 81, 4) a21, SUBSTRING(MOCycle, 85, 4) a22, SUBSTRING(MOCycle, 89, 4) a23, SUBSTRING(MOCycle, 93, 4) a24,      \r\n" +
"       SUBSTRING(MOCycle, 97, 4) a25, SUBSTRING(MOCycle, 101, 4) a26, SUBSTRING(MOCycle, 105, 4) a27, SUBSTRING(MOCycle, 109, 4) a28,      \r\n" +
"       SUBSTRING(MOCycle, 113, 4) a29, SUBSTRING(MOCycle, 117, 4) a30, SUBSTRING(MOCycle, 121, 4) a31, SUBSTRING(MOCycle, 125, 4) a32,      \r\n" +
"       SUBSTRING(MOCycle, 129, 4) a33, SUBSTRING(MOCycle, 133, 4) a34, SUBSTRING(MOCycle, 137, 4) a35, SUBSTRING(MOCycle, 141, 4) a36,      \r\n" +
"       SUBSTRING(MOCycle, 145, 4) a37, SUBSTRING(MOCycle, 149, 4) a38, SUBSTRING(MOCycle, 153, 4) a39, SUBSTRING(MOCycle, 157, 4) a40,      \r\n" +
"       SUBSTRING(MOCycle, 161, 4) a41, SUBSTRING(MOCycle, 165, 4) a42, SUBSTRING(MOCycle, 169, 4) a43, SUBSTRING(MOCycle, 173, 4) a44,      \r\n" +
"       SUBSTRING(MOCycle, 177, 4) a45, s1.ReportToSectionid , pm.MonthlyTotalSQM MonthSqm      from vw_PlanMonth pm, Workplace w, section s, section s1 where pm.workplaceid = w.workplaceid      \r\n" +
"       and pm.Sectionid = s.SectionID and pm.Prodmonth = s.Prodmonth      \r\n" +
"       and s.reporttoSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
"       and pm.prodmonth = '" + prodmonth + "'  and pm.Plancode = 'MP'    \r\n" +
"       and s1.ReportToSectionid = '" + section + "') b on a.workplaceid = b.workplaceid and a.activity = b.activity and a.minid = b.minid      \r\n" +
"        left outer join      \r\n" +
"       (select '3' Line, s1.Sectionid sbid, s1.Name sbname, s.Sectionid minid, s.Name minname, pm.workplaceid, w.description,      \r\n" +
"      case when pm.Activity = 9 then 'Ledge'      \r\n" +
"     when pm.Activity = 0 then 'Stope'      \r\n" +
"     when pm.Activity = 1 then 'Dev' End as Act,      \r\n" +
"       case when pm.Activity <> 1 then 'a.Stoping' else 'b.Development' end as MainGroup,      \r\n" +
"       pm.FL, pm.SqmTotal plansqm, cyclesqm, orgunitds, case when pm.Activity in (0, 9) then 'Day Sqm' else 'Day M' end as aa,        \r\n" +
"       SUBSTRING(MOCycleNum, 1, 4) b1,       \r\n" +
"       SUBSTRING(MOCycleNum, 5, 4) b2, SUBSTRING(MOCycleNum, 9, 4) b3, SUBSTRING(MOCycleNum, 13, 4) b4,       \r\n" +
"       SUBSTRING(MOCycleNum, 17, 4) b5, SUBSTRING(MOCycleNum, 21, 4) b6, SUBSTRING(MOCycleNum, 25, 4) b7, SUBSTRING(MOCycleNum, 29, 4) b8,       \r\n" +
"       SUBSTRING(MOCycleNum, 33, 4) b9, SUBSTRING(MOCycleNum, 37, 4) b10, SUBSTRING(MOCycleNum, 41, 4) b11, SUBSTRING(MOCycleNum, 45, 4) b12,       \r\n" +
"       SUBSTRING(MOCycleNum, 49, 4) b13, SUBSTRING(MOCycleNum, 53, 4) b14, SUBSTRING(MOCycleNum, 57, 4) b15, SUBSTRING(MOCycleNum, 61, 4) b16,       \r\n" +
"       SUBSTRING(MOCycleNum, 65, 4) b17, SUBSTRING(MOCycleNum, 69, 4) b18, SUBSTRING(MOCycleNum, 73, 4) b19, SUBSTRING(MOCycleNum, 77, 4) b20,       \r\n" +
"       SUBSTRING(MOCycleNum, 81, 4) b21, SUBSTRING(MOCycleNum, 85, 4) b22, SUBSTRING(MOCycleNum, 89, 4) b23, SUBSTRING(MOCycleNum, 93, 4) b24,       \r\n" +
"       SUBSTRING(MOCycleNum, 97, 4) b25, SUBSTRING(MOCycleNum, 101, 4) b26, SUBSTRING(MOCycleNum, 105, 4) b27, SUBSTRING(MOCycleNum, 109, 4) b28,       \r\n" +
"       SUBSTRING(MOCycleNum, 113, 4) b29, SUBSTRING(MOCycleNum, 117, 4) b30, SUBSTRING(MOCycleNum, 121, 4) b31, SUBSTRING(MOCycleNum, 125, 4) b32,       \r\n" +
"       SUBSTRING(MOCycleNum, 129, 4) b33, SUBSTRING(MOCycleNum, 133, 4) b34, SUBSTRING(MOCycleNum, 137, 4) b35, SUBSTRING(MOCycleNum, 141, 4) b36,       \r\n" +
"       SUBSTRING(MOCycleNum, 145, 4) b37, SUBSTRING(MOCycleNum, 149, 4) b38, SUBSTRING(MOCycleNum, 153, 4) b39, SUBSTRING(MOCycleNum, 157, 4) b40,       \r\n" +
"       SUBSTRING(MOCycleNum, 161, 4) b41, SUBSTRING(MOCycleNum, 165, 4) b42, SUBSTRING(MOCycleNum, 169, 4) b43, SUBSTRING(MOCycleNum, 173, 4) b44,       \r\n" +
"       SUBSTRING(MOCycleNum, 177, 4) b45 , s1.ReportToSectionid, pm.activity      \r\n" +
"         from vw_PlanMonth pm, Workplace w, section s, section s1 where pm.workplaceid = w.workplaceid      \r\n" +
"       and pm.Sectionid = s.SectionID and pm.Prodmonth = s.Prodmonth      \r\n" +
"      and s.reporttoSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
"      and pm.prodmonth = '" + prodmonth + "' and pm.Plancode = 'MP'     \r\n" +
"       and s1.ReportToSectionid = '" + section + "') c on a.Workplaceid = c.workplaceid  and a.activity = c.activity  and a.minid = c.minid      \r\n" +
"  , (select 0 act1, max(rand) rr FROM[dbo].[Rates_Stoping]) d      \r\n" +
"union      \r\n" +




"   select   case when MAX(pmold1) is null then 'Red' when MAX(pmold1) is not null and MAX(pmold1) < @prev then 'orange' else '' end as newwpflag,       \r\n" +
"   Line, sbid, sbname, minid, minname, WorkplaceID,  Description, Act,       \r\n" +
"   sum(DevTotAdv) DevTotAdv, sum(DevWasteAdv) DevWasteAdv, sum(DevTons) DevTons, sum(DevCont) DevCont, sum(StpTons) StpTons       \r\n" +
"   , sum(StpCont) StpCont, max(MainGroup) MainGroup, max(FL) FL, convert(int, sum(plansqm)) plansqm       \r\n" +
"   , convert(int, sum(cyclesqm)) cyclesqm, MAX(orgunitds) orgunitds, MAX(aa)  aa       \r\n" +
"   , MAX(adv) adv, convert(int, sum(tons)) tons, convert(int, sum(content)) content, MAX(offreefsqm) offreefsqm, 0 Budget, MAX(activity) activity        \r\n" +
"   ,MAX(extrabudget) extrabudget, MAX(extrabudget1) extrabudget1       \r\n" +
"   ,MAX(aa1) aa1,MAX(aa2) aa2,MAX(aa3) aa3,MAX(aa4) aa4,MAX(aa5) aa5,MAX(aa6) aa6,MAX(aa7) aa7,MAX(aa8) aa8,MAX(aa9) aa9,MAX(aa10) aa10       \r\n" +
"   ,MAX(aa11) aa11,MAX(aa12) aa12,MAX(aa13) aa13,MAX(aa14) aa14,MAX(aa15) aa15,MAX(aa16) aa16,MAX(aa17) aa17,MAX(aa18) aa18,MAX(aa19) aa19,MAX(aa20) aa20       \r\n" +
"   ,MAX(aa21) aa21,MAX(aa22) aa22,MAX(aa23) aa23,MAX(aa24) aa24,MAX(aa25) aa25,MAX(aa26) aa26,MAX(aa27) aa27,MAX(aa28) aa28,MAX(aa29) aa29,MAX(aa30) aa30       \r\n" +
"   ,MAX(aa31) aa31,MAX(aa32) aa32,MAX(aa33) aa33,MAX(aa34) aa34,MAX(aa35) aa35,MAX(aa36) aa36,MAX(aa37) aa37,MAX(aa38) aa38,MAX(aa39) aa39,MAX(aa40) aa40       \r\n" +
"   ,MAX(aa41) aa41,MAX(aa42) aa42,MAX(aa43) aa43,MAX(aa44) aa44,MAX(aa45) aa45       \r\n" +
"   , MAX(SectionID) SectionID, MAX(Mprass) Mprass, MAX(wpexternalid) wpexternalid, MAX(Surv) Surv       \r\n" +
"   , MAX(pmold1) newptt,  2 Line2,       \r\n" +
"   MAX(sbid1) sbid1, MAX(sbname1) sbname1, MAX(minid1) minid1, MAX(minname1) minname1,       \r\n" +
"   MAX(wp1) wp1,MAX(wd1) wd1, 9 Act1a, MAX(Act1) Act1,       \r\n" +
"   MAX(MainGroup1) MainGroup1,        \r\n" +
"   sum(FL1) FL1, convert(int, sum(plansqm1)) plansqm1, convert(int, sum(cyclesqma)) cyclesqma , MAX(da) da, MAX(aaa) aaa       \r\n" +
"   ,MAX(a1) a1,MAX(a2) a2,MAX(a3) a3,MAX(a4) a4,MAX(a5) a5,MAX(a6) a6,MAX(a7) a7,MAX(a8) a8,MAX(a9) a9,MAX(a10) a10       \r\n" +
"   ,MAX(a11) a11,MAX(a12) a12,MAX(a13) a13,MAX(a14) a14,MAX(a15) a15,MAX(a16) a16,MAX(a17) a17,MAX(a18) a18,MAX(a19) a19,MAX(a20) a20       \r\n" +
"   ,MAX(a21) a21,MAX(a22) a22,MAX(a23) a23,MAX(a24) a24,MAX(a25) a25,MAX(a26) a26,MAX(a27) a27,MAX(a28) a28,MAX(a29) a29,MAX(a30) a30       \r\n" +
"   ,MAX(a31) a31,MAX(a32) a32,MAX(a33) a33,MAX(a34) a34,MAX(a35) a35,MAX(a36) a36,MAX(a37) a37,MAX(a38) a38,MAX(a39) a39,MAX(a40) a40       \r\n" +
"   ,MAX(a41) a41,MAX(a42) a42,MAX(a43) a43,MAX(a44) a44,MAX(a45) a45       \r\n" +
"   , MAX(SectionID) SectionID2,    0 MonthSqm  ,     \r\n" +
"   MAX(Line3) Line3,       \r\n" +
"   MAX(sbid1) sbid2, MAX(sbname1) sbname2, MAX(minid1) minid2, MAX(minname1) minname2,       \r\n" +
"   MAX(wp1) wp2,MAX(wd1) wd2, MAX(Act1) Act2,       \r\n" +
"   MAX(MainGroup1) MainGroup2,        \r\n" +
"   sum(FL1) FL2, convert(int, sum(plansqm1)) plansqm2, convert(int, sum(cyclesqma)) cyclesqma2 , MAX(da) da2, MAX(aaa) aaa2       \r\n" +
"   ,MAX(b1) b1,MAX(b2) b2,MAX(b3) b3,MAX(b4) b4,MAX(b5) b5,MAX(b6) b6,MAX(b7) b7,MAX(b8) b8,MAX(b9) b9,MAX(b10) b10       \r\n" +
"   ,MAX(b11) b11,MAX(b12) b12,MAX(b13) b13,MAX(b14) b14,MAX(b15) b15,MAX(b16) b16,MAX(b17) b17,MAX(b18) b18,MAX(b19) b19,MAX(b20) b20       \r\n" +
"   ,MAX(b21) b21,MAX(b22) b22,MAX(b23) b23,MAX(b24) b24,MAX(b25) b25,MAX(b26) b26,MAX(b27) b27,MAX(b28) b28,MAX(b29) b29,MAX(b30) b30       \r\n" +
"   ,MAX(b31) b31,MAX(b32) b32,MAX(b33) b33,MAX(b34) b34,MAX(b35) b35,MAX(b36) b36,MAX(b37) b37,MAX(b38) b38,MAX(b39) b39,MAX(b40) b40       \r\n" +
"   ,MAX(b41) b41,MAX(b42) b42,MAX(b43) b43,MAX(b44) b44,MAX(b45) b45       \r\n" +
"   ,max(mo) mo , 9 ActF, pm, sum(rr) rr      \r\n" +
"    from(      \r\n" +
"   select 1 Line, s1.SectionID sbid, s1.Name sbname, s.SectionID minid, s.Name minname,      \r\n" +
"   v.WorkplaceID, w.Description, 'Vamping' Act,      \r\n" +
"   0 DevTotAdv,      \r\n" +
"   0 DevWasteAdv,      \r\n" +
"   0 DevTons,      \r\n" +
"   0 DevCont,      \r\n" +
"   plantons StpTons,      \r\n" +
"   PlanContent StpCont,      \r\n" +
"   'C.Vamping'  MainGroup,      \r\n" +
"   0 FL, PlanSqm plansqm, PlanSqm cyclesqm, orgunitds,      \r\n" +
"   'Default Cycle' aa, 0 adv, plantons tons, PlanContent content, 0 offreefsqm, 0 Budget, 9 activity,      \r\n" +
"   0 extrabudget, 0 extrabudget1,      \r\n" +
"   '' aa1,      \r\n" +
"   '' aa2, '' aa3, '' aa4,      \r\n" +
"   '' aa5, '' aa6, '' aa7, '' aa8,      \r\n" +
"   '' aa9, '' aa10, '' aa11, '' aa12,      \r\n" +
"   '' aa13, '' aa14,'' aa15, '' aa16,      \r\n" +
"   '' aa17, '' aa18, '' aa19, '' aa20,      \r\n" +
"   '' aa21, '' aa22, '' aa23, '' aa24,      \r\n" +
"   '' aa25, '' aa26, '' aa27, '' aa28,      \r\n" +
"   '' aa29, '' aa30, '' aa31, '' aa32,      \r\n" +
"   '' aa33, '' aa34, '' aa35, '' aa36,      \r\n" +
"   '' aa37, '' aa38, '' aa39, '' aa40,      \r\n" +
"   '' aa41, '' aa42, '' aa43, '' aa44,      \r\n" +
"   '' aa45,      \r\n" +
"   s2.SectionID, '' Mprass,'' wpexternalid, '' Surv,      \r\n" +
"   2 Line3,      \r\n" +
"    s1.SectionID sbid1, s1.Name sbname1, s.SectionID minid1, s.Name minname1,      \r\n" +
"   v.WorkplaceID wp1, w.Description wd1, 'Vamping' Act1,      \r\n" +
"   'C.Vamping'  MainGroup1,      \r\n" +
"   0 FL1, PlanSqm plansqm1, PlanSqm cyclesqma, orgunitds da, 'MO Cycle' aaa,      \r\n" +
"   case when calendardate = @VampingStart and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a1,      \r\n" +
"   case when calendardate = @VampingStart + 1 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 1 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a2,      \r\n" +
"   case when calendardate = @VampingStart + 2 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 2 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a3,      \r\n" +
"   case when calendardate = @VampingStart + 3 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 3 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a4,      \r\n" +
"   case when calendardate = @VampingStart + 4 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 4 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a5,      \r\n" +
"   case when calendardate = @VampingStart + 5 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 5 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a6,      \r\n" +
"   case when calendardate = @VampingStart + 6 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 6 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a7,      \r\n" +
"   case when calendardate = @VampingStart + 7 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 7 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a8,      \r\n" +
"   case when calendardate = @VampingStart + 8 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 8 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a9,      \r\n" +
"   case when calendardate = @VampingStart + 9 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 9 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a10,      \r\n" +
"   case when calendardate = @VampingStart + 10 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 10 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a11,      \r\n" +
"   case when calendardate = @VampingStart + 11 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 11 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a12,      \r\n" +
"   case when calendardate = @VampingStart + 12 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 12 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a13,      \r\n" +
"   case when calendardate = @VampingStart + 13 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 13 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a14,      \r\n" +
"   case when calendardate = @VampingStart + 14 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 14 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a15,      \r\n" +
"   case when calendardate = @VampingStart + 15 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 15 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a16,      \r\n" +
"   case when calendardate = @VampingStart + 16 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 16 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a17,      \r\n" +
"   case when calendardate = @VampingStart + 17 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 17 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a18,      \r\n" +
"   case when calendardate = @VampingStart + 18 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 18 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a19,      \r\n" +
"   case when calendardate = @VampingStart + 19 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 19 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a20,      \r\n" +
"   case when calendardate = @VampingStart + 20 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 20 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a21,      \r\n" +
"   case when calendardate = @VampingStart + 21 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 21 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a22,      \r\n" +
"   case when calendardate = @VampingStart + 22 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 22 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a23,      \r\n" +
"   case when calendardate = @VampingStart + 23 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 23 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a24,      \r\n" +
"   case when calendardate = @VampingStart + 24 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 24 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a25,      \r\n" +
"   case when calendardate = @VampingStart + 25 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 25 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a26,      \r\n" +
"   case when calendardate = @VampingStart + 26 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 26 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a27,      \r\n" +
"   case when calendardate = @VampingStart + 27 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 27 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a28,      \r\n" +
"   case when calendardate = @VampingStart + 28 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 28 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a29,      \r\n" +
"   case when calendardate = @VampingStart + 29 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 29 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a30,      \r\n" +
"   case when calendardate = @VampingStart + 30 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 30 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a31,      \r\n" +
"   case when calendardate = @VampingStart + 31 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 31 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a32,      \r\n" +
"   case when calendardate = @VampingStart + 32 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 32 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a33,      \r\n" +
"   case when calendardate = @VampingStart + 33 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 33 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a34,      \r\n" +
"   case when calendardate = @VampingStart + 34 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 34 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a35,      \r\n" +
"   case when calendardate = @VampingStart + 35 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 35 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a36,      \r\n" +
"   case when calendardate = @VampingStart + 36 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 36 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a37,      \r\n" +
"   case when calendardate = @VampingStart + 37 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 37 and workingday = 'N' then 'OFF'       \r\n" +
"    else '' end as a38,      \r\n" +
"   case when calendardate = @VampingStart + 38 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 38 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a39,      \r\n" +
"   case when calendardate = @VampingStart + 39 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 39 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a40,      \r\n" +
"   case when calendardate = @VampingStart + 40 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 40 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a41,      \r\n" +
"   case when calendardate = @VampingStart + 41 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 41 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a42,      \r\n" +
"   case when calendardate = @VampingStart + 42 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 42 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a43,      \r\n" +
"   case when calendardate = @VampingStart + 43 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 43 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a44,      \r\n" +
"   case when calendardate = @VampingStart + 44 and workingday = 'Y' then PlanActivity      \r\n" +
"   when calendardate = @VampingStart + 44 and workingday = 'N' then 'OFF'       \r\n" +
"   else '' end as a45,      \r\n" +
"   s2.SectionID ReporttoSectioniD,      \r\n" +
"   3 Line3a,      \r\n" +
"    s1.SectionID sbid3, s1.Name sbname3, s.SectionID minid3, s.Name minname3,      \r\n" +
"   v.WorkplaceID wp2, w.Description wd2, 'Vamping' Act3,      \r\n" +
"   'C.Vamping'  MainGroupaaa,      \r\n" +
"   0 FLz, PlanSqm plansqmz, PlanSqm cyclesqm1, orgunitds ds1, 'Day Sqm' aaz,      \r\n" +
"   case when calendardate = @VampingStart and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b1,       \r\n" +
"   case when calendardate = @VampingStart + 1 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b2,       \r\n" +
"   case when calendardate = @VampingStart + 2 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b3,       \r\n" +
"   case when calendardate = @VampingStart + 3 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))       \r\n" +
"   else '' end as b4,       \r\n" +
"   case when calendardate = @VampingStart + 4 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b5,       \r\n" +
"   case when calendardate = @VampingStart + 5 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b6,       \r\n" +
"   case when calendardate = @VampingStart + 6 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b7,       \r\n" +
"   case when calendardate = @VampingStart + 7 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b8,       \r\n" +
"   case when calendardate = @VampingStart + 8 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b9,       \r\n" +
"   case when calendardate = @VampingStart + 9 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b10,       \r\n" +
"   case when calendardate = @VampingStart + 10 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b11,       \r\n" +
"   case when calendardate = @VampingStart + 11 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b12,       \r\n" +
"   case when calendardate = @VampingStart + 12 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b13,       \r\n" +
"   case when calendardate = @VampingStart + 13 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b14,       \r\n" +
"   case when calendardate = @VampingStart + 14 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b15,       \r\n" +
"   case when calendardate = @VampingStart + 15 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b16,       \r\n" +
"   case when calendardate = @VampingStart + 16 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b17,       \r\n" +
"   case when calendardate = @VampingStart + 17 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b18,       \r\n" +
"   case when calendardate = @VampingStart + 18 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b19,       \r\n" +
"   case when calendardate = @VampingStart + 19 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b20,       \r\n" +
"   case when calendardate = @VampingStart + 20 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b21,       \r\n" +
"   case when calendardate = @VampingStart + 21 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b22,       \r\n" +
"   case when calendardate = @VampingStart + 22 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b23,       \r\n" +
"   case when calendardate = @VampingStart + 23 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b24,       \r\n" +
"   case when calendardate = @VampingStart + 24 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b25,       \r\n" +
"   case when calendardate = @VampingStart + 25 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b26,       \r\n" +
"   case when calendardate = @VampingStart + 26 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b27,       \r\n" +
"   case when calendardate = @VampingStart + 27 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b28,       \r\n" +
"   case when calendardate = @VampingStart + 28 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b29,       \r\n" +
"   case when calendardate = @VampingStart + 29 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b30,       \r\n" +
"   case when calendardate = @VampingStart + 30 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))       \r\n" +
"   else '' end as b31,       \r\n" +
"   case when calendardate = @VampingStart + 31 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b32,       \r\n" +
"   case when calendardate = @VampingStart + 32 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b33,       \r\n" +
"   case when calendardate = @VampingStart + 33 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b34,       \r\n" +
"   case when calendardate = @VampingStart + 34 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))       \r\n" +
"   else '' end as b35,       \r\n" +
"   case when calendardate = @VampingStart + 35 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b36,       \r\n" +
"   case when calendardate = @VampingStart + 36 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b37,       \r\n" +
"   case when calendardate = @VampingStart + 37 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b38,       \r\n" +
"   case when calendardate = @VampingStart + 38 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b39,       \r\n" +
"   case when calendardate = @VampingStart + 39 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b40,       \r\n" +
"   case when calendardate = @VampingStart + 40 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b41,       \r\n" +
"   case when calendardate = @VampingStart + 41 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b42,       \r\n" +
"   case when calendardate = @VampingStart + 42and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b43,       \r\n" +
"   case when calendardate = @VampingStart + 43 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b44,       \r\n" +
"   case when calendardate = @VampingStart + 44 and plansqm > 0 then convert(varchar(10),convert(int, PlanSqm))        \r\n" +
"   else '' end as b45,        \r\n" +
"   s2.SectionID mo, v.Prodmonth pm, 0 rr, pmold1      \r\n" +
"    from Planning_Vamping v, Section s , Section s1, Section s2, (select w.*, pmold1 from Workplace w left outer join   (select workplaceid wz, max(prodmonth) pmold1 from Planning_Vamping      \r\n" +
"    where prodmonth < '" + prodmonth + "' group by workplaceid) newwp on  w.workplaceid = newwp.wz) w, Workplace wt      \r\n" +
"   where v.Sectionid = s.SectionID and v.Prodmonth = s.Prodmonth      \r\n" +
"    and s.ReportToSectionid = s1.SectionID and s.Prodmonth = s1.Prodmonth      \r\n" +
"    and s1.ReportToSectionid = s2.SectionID and s1.Prodmonth = s2.Prodmonth      \r\n" +
"    and v.WorkplaceID = w.WorkplaceID  and w.workplaceid = wt.workplaceid     \r\n" +
"    and v.Prodmonth = '" + prodmonth + "' and s2.SectionID = '" + section + "') a      \r\n" +
"    group by Line, sbid, sbname, minid, minname, WorkplaceID, Description, Act, pm      \r\n" +
"   order by  a.sbid, a.MainGroup, a.OrgUnitDS, a.Workplaceid, a.aa1 Desc, a.Line";



            _dbManPlan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPlan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPlan.ResultsTableName = "Planning";
            _dbManPlan.ExecuteInstruction();


            /////

            DataSet DsPlanning = new DataSet();
            DsPlanning.Tables.Add(_dbManPlan.ResultsDataTable);
            theReport.RegisterData(DsPlanning);

            theReport.Load(TGlobalItems.ReportsFolder + "\\Planning.frx");

            switch (theAction)
            {
                case reportAction.raPrint:
                    // PlanningReport.Design();

                    if (TParameters.DesignReport)
                    {
                        theReport.Design();
                    }

                    theReport.Print();
                    break;
                case reportAction.raAdobe:
                    theReport.Prepare();
                    using (FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport())
                    {
                        if (export.ShowDialog())
                        {
                            using (SaveFileDialog DialogSave = new SaveFileDialog())
                            {
                                DialogSave.DefaultExt = "pdf";
                                DialogSave.RestoreDirectory = true;
                                DialogSave.Filter = "Adobe PDF File (*.pdf)|*.pdf";
                                DialogSave.Title = "Where do you want to save the file?";
                                if (DialogSave.ShowDialog() == DialogResult.OK)
                                    theReport.Export(export, DialogSave.FileName);
                            }
                        }
                    }
                    break;
                case reportAction.raExcel:
                    theReport.Prepare();
                    using (FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export())
                    {
                        if (export.ShowDialog())
                        {
                            using (SaveFileDialog DialogSave = new SaveFileDialog())
                            {
                                DialogSave.DefaultExt = "xlsx";
                                DialogSave.RestoreDirectory = true;
                                DialogSave.Filter = "Excel 2007 File (*.xlsx)|*.xlsx";
                                DialogSave.Title = "Where do you want to save the file?";
                                if (DialogSave.ShowDialog() == DialogResult.OK)
                                    theReport.Export(export, DialogSave.FileName);
                            }
                        }
                    }
                    break;
                case reportAction.raWord:
                    theReport.Prepare();
                    using (FastReport.Export.OoXML.Word2007Export export = new FastReport.Export.OoXML.Word2007Export())
                    {
                        if (export.ShowDialog())
                        {
                            using (SaveFileDialog DialogSave = new SaveFileDialog())
                            {
                                DialogSave.DefaultExt = "docx";
                                DialogSave.RestoreDirectory = true;
                                DialogSave.Filter = "Word 2007 File (*.docx)|*.docx";
                                DialogSave.Title = "Where do you want to save the file?";
                                if (DialogSave.ShowDialog() == DialogResult.OK)
                                    theReport.Export(export, DialogSave.FileName);
                            }
                        }
                    }
                    break;
                case reportAction.raPoerpoint:
                    theReport.Prepare();
                    using (FastReport.Export.OoXML.PowerPoint2007Export export = new FastReport.Export.OoXML.PowerPoint2007Export())
                    {
                        if (export.ShowDialog())
                        {
                            using (SaveFileDialog DialogSave = new SaveFileDialog())
                            {
                                DialogSave.DefaultExt = "pptx";
                                DialogSave.RestoreDirectory = true;
                                DialogSave.Filter = "PowerPoint 2007 File (*.pptx)|*.pptx";
                                DialogSave.Title = "Where do you want to save the file?";
                                if (DialogSave.ShowDialog() == DialogResult.OK)
                                    theReport.Export(export, DialogSave.FileName);
                            }
                        }
                    }
                    break;
                case reportAction.raImage:
                    theReport.Prepare();
                    using (FastReport.Export.Image.ImageExport export = new FastReport.Export.Image.ImageExport())
                    {
                        export.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png;
                        if (export.ShowDialog())
                        {
                            using (SaveFileDialog DialogSave = new SaveFileDialog())
                            {
                                DialogSave.DefaultExt = "png";
                                DialogSave.RestoreDirectory = true;
                                DialogSave.Filter = "PNG Image File (*.png)|*.png";
                                DialogSave.Title = "Where do you want to save the file?";
                                if (DialogSave.ShowDialog() == DialogResult.OK)
                                    theReport.Export(export, DialogSave.FileName);
                            }
                        }
                    }
                    break;
            }
        }

        private void barButtonPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningReport(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), Convert.ToInt32(barActivityPlanning.EditValue.ToString()), barSectionPlanning.EditValue.ToString(), reportAction.raPrint);
        }

        private void barBottonExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningReport(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), Convert.ToInt32(barActivityPlanning.EditValue.ToString()), barSectionPlanning.EditValue.ToString(), reportAction.raExcel);
        }

        private void exsportAdobe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningReport(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), Convert.ToInt32(barActivityPlanning.EditValue.ToString()), barSectionPlanning.EditValue.ToString(), reportAction.raAdobe);
        }

        private void exsportExcel2007_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningReport(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), Convert.ToInt32(barActivityPlanning.EditValue.ToString()), barSectionPlanning.EditValue.ToString(), reportAction.raExcel);
        }

        private void exsportWord2007_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningReport(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), Convert.ToInt32(barActivityPlanning.EditValue.ToString()), barSectionPlanning.EditValue.ToString(), reportAction.raWord);
        }

        private void exsportPowerpoint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningReport(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), Convert.ToInt32(barActivityPlanning.EditValue.ToString()), barSectionPlanning.EditValue.ToString(), reportAction.raPoerpoint);
        }

        private void exsportImages_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningReport(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), Convert.ToInt32(barActivityPlanning.EditValue.ToString()), barSectionPlanning.EditValue.ToString(), reportAction.raImage);
        }

        private void barProductionMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (barProductionMonthPlanning.EditValue != null)
            {
                PlanningClass.ProdMonth = TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString()));
                setSections();
            }
        }

        private void btnUnApprovePlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucUnApprovePlanning = new ucUnApprovePlanning { Parent = parentControle, Dock = DockStyle.Fill, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            bool canApprove = ucUnApprovePlanning.loadUnApprovePlanning(TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), barSectionPlanning.EditValue.ToString(), Convert.ToInt32(barActivityPlanning.EditValue.ToString()));

            if (canApprove == true)
            {
                parentControle.Visible = false;
                parentControle.Controls.Clear();
                ucUnApprovePlanning.Parent = parentControle;
                rpPreplanningManage.Visible = false;
                rpPlanningUnApprove.Visible = true;
                parentControle.Visible = true;
            }
            else
            {
                MessageBox.Show("No workplaces to Un-Approved.");
                parentControle.Visible = true;
            }
        }

        private void btnApprovePlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnApprovePlanning.Enabled = false;
            btnCloseApprove.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            ucApprovePlanning.ApprovePlanning();
        }

        public void UpdateCur()
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate ()
                {
                    UpdateCur();
                });
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCloseApprove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PlanningClass.PlanningSettings.ActivityID == 0)
            {
                parentControle.Visible = false;
                parentControle.Controls.Clear();
                parentControle.Controls.Add(PlanningClass.StopePrePlanning);
                rpPreplanningManage.Visible = true;
                rpApprovePlanning.Visible = false;
                parentControle.Visible = true;
            }
            else
            {
                parentControle.Visible = false;
                parentControle.Controls.Clear();
                PlanningClass.PlanningScreen.reloadWorkplaces();
                PlanningClass.PlanningScreen.Parent = parentControle;
                rpPreplanningManage.Visible = true;
                rpApprovePlanning.Visible = false;
                parentControle.Visible = true;

            }

        }

        private void btnStopWP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();
            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();

            if (workplaceID != "NONE")
            {
                if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue))) == false)
                {
                    if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == false)
                    {
                        planninType = replanningType.rpStopWp;
                        parentControle.Visible = false;
                        parentControle.Controls.Clear();
                        ucStopWorkplace = new ucStopWorkplace { Parent = parentControle, Dock = DockStyle.Left };
                        ucStopWorkplace.StopWorkplace(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), sectionID, barSectionPlanning.EditValue.ToString());

                        rpPreplanningManage.Visible = false;
                        rpReplanning.Visible = true;
                        parentControle.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("The selected workplace is already stopped");
                    }
                }
                else
                {
                    MessageBox.Show("Un approved Replanning already exists for this workplace.");
                }
            }
        }

        private void btnCancelReplanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            parentControle.Visible = false;
            parentControle.Controls.Clear();
            PlanningClass.PlanningScreen.Parent = parentControle;
            rpRevisedPlanning.Visible = true;
            rpReplanning.Visible = false;
            parentControle.Visible = true;
        }

        private void btnSendRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool messageSend = false;
            switch (planninType)
            {
                case replanningType.rpStopWp:
                    messageSend = ucStopWorkplace.SendRequest();
                    break;

                case replanningType.rpNewWP:
                    messageSend = ucAddWorkplace.SendRequest();
                    break;

                case replanningType.rpCrewChnages:
                    messageSend = ucCrewMinerChange.SendRequest();
                    break;

                case replanningType.rpCallCahnges:
                    messageSend = ucPlanningValueChanges.SendRequest();
                    break;

                case replanningType.rpMoveWP:
                    messageSend = ucMoveBookings.SendRequest();
                    break;

                case replanningType.rpStartWP:
                    messageSend = ucStartWP.SendRequest();
                    break;

                case replanningType.rpMiningMethodChange:
                    messageSend = miningMethodChange.SendRequest();
                    break;

                case replanningType.rpDrillRig:
                    messageSend = drillRigChange.SendRequest();
                    break;
                case replanningType.rpDeleteWorkplace:
                    messageSend = DeleteWorkplace.SendRequest();
                    break;
            }

            if (messageSend == true)
            {
                MessageBox.Show("Request was sent.");

                parentControle.Visible = false;
                parentControle.Controls.Clear();
                PlanningClass.PlanningScreen.Parent = parentControle;
                rpRevisedPlanning.Visible = true;
                rpReplanning.Visible = false;
                parentControle.Visible = true;
            }
        }

        private void btnRePlanAddWorkplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();
            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();

            planninType = replanningType.rpNewWP;
            parentControle.Visible = false;
            parentControle.Controls.Clear();
            ucAddWorkplace = new ucAddWorkplace { Parent = parentControle, Dock = DockStyle.Left };
            ucAddWorkplace.addWorkplace(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), sectionID, barSectionPlanning.EditValue.ToString(), Convert.ToInt32(barActivityPlanning.EditValue.ToString()));

            rpPreplanningManage.Visible = false;
            rpReplanning.Visible = true;
            parentControle.Visible = true;
        }

        private void btnCrewMiner_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue))) == false)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue))) == false)
                    {
                        if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == true)
                        {
                            planninType = replanningType.rpCrewChnages;
                            parentControle.Visible = false;
                            parentControle.Controls.Clear();
                            ucCrewMinerChange = new ucCrewMinerChange { Parent = parentControle, Dock = DockStyle.Left };
                            ucCrewMinerChange.changecrew(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(barProductionMonthPlanning.EditValue.ToString())), sectionID, barSectionPlanning.EditValue.ToString(), Convert.ToInt32(barActivityPlanning.EditValue.ToString()));
                            rpPreplanningManage.Visible = false;
                            rpReplanning.Visible = true;
                            parentControle.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("The selected workplace is already stopped");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to approved workplaces.");
                }
            }
        }

        private void btnChangeValues_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == true)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == true)
                        {
                            planninType = replanningType.rpCallCahnges;
                            parentControle.Visible = false;
                            parentControle.Controls.Clear();
                            ucPlanningValueChanges = new ucPlanningValueChanges { Parent = parentControle, Dock = DockStyle.Left };
                            ucPlanningValueChanges.PlanningChange(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivityPlanning.EditValue.ToString()));
                            rpPreplanningManage.Visible = false;
                            rpReplanning.Visible = true;
                            parentControle.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("The selected workplace is already stopped");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to approved workplaces.");
                }
            }
        }

        private void btnReplanningStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            parentControle.Visible = false;
            parentControle.Controls.Clear();
            ucrequeststatus = new ucRequeststatus { Parent = parentControle, Dock = DockStyle.Fill, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            rpPreplanningManage.Visible = false;
            rpReplanningStatus.Visible = true;
            parentControle.Visible = true;

            if (ucrequeststatus._RequestList1.ResultsDataTable.DefaultView.Count == 0)
            {
                btnApprove.Enabled = false;
                btnDecline.Enabled = false;
            }
            else
            {
                ucrequeststatus.gcRequestList.Focus();
                btnApprove.Enabled = true;
                btnDecline.Enabled = true;
            }
        }

        public void btnApprove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucrequeststatus.loadapprove();
            btnApprove.Enabled = true;
            btnDecline.Enabled = true;
        }

        public void btnEnabled()
        {
            btnDecline.Enabled = true;
            btnApprove.Enabled = true;
        }

        private void btnDecline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucrequeststatus.loaddecline();
            btnApprove.Enabled = true;
            btnDecline.Enabled = true;
        }

        private void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            parentControle.Visible = false;
            parentControle.Controls.Clear();
            PlanningClass.PlanningScreen.reloadWorkplaces();
            PlanningClass.PlanningScreen.Parent = parentControle;
            rpRevisedPlanning.Visible = true;
            rpReplanningStatus.Visible = false;
            parentControle.Visible = true;
            PlanningClass.PlanningScreen.Update();
            PlanningClass.PlanningScreen.MainGrid.Update();
            PlanningClass.PlanningScreen.MainGrid.EndUpdate();
            PlanningClass.PlanningScreen.MainGrid.RefreshDataSource();
            PlanningClass.PlanningScreen.MainGrid.Refresh();
            PlanningClass.PlanningScreen.viewPlanningStoping.RefreshData();
            ucPreplanning ab = new ucPreplanning();
            ab.MainGrid.Update();
            ab.MainGrid.EndUpdate();
            ab.MainGrid.RefreshDataSource();
            ab.MainGrid.Refresh();
        }

        private void btnMovePlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == true)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                        {
                            if (hasReplannig.result(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                            {
                                if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == true)
                                {
                                    planninType = replanningType.rpMoveWP;
                                    parentControle.Visible = false;
                                    parentControle.Controls.Clear();
                                    ucMoveBookings = new MoveBookings { Parent = parentControle, Dock = DockStyle.Left };
                                    ucMoveBookings.MoveBooking(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));
                                    rpPreplanningManage.Visible = false;
                                    rpReplanning.Visible = true;
                                    parentControle.Visible = true;
                                }
                                else
                                {
                                    MessageBox.Show("The selected workplace is already stopped");
                                }
                            }
                            else
                            {
                                MessageBox.Show("The selected workplace is already stopped");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Un approved Replanning already exists for this workplace.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to unapproved workplaces.");
                }
            }
            else
            {
                MessageBox.Show("Replanning cannot be applied to Unapproved workplaces.");
            }
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmResavePlanningProtocol _frmResavePlanningProtocol = new frmResavePlanningProtocol();
            _frmResavePlanningProtocol.UserCurrentInfo = this.UserCurrentInfo;
            _frmResavePlanningProtocol.theSystemDBTag = this.theSystemDBTag;
            _frmResavePlanningProtocol.ShowDialog();
        }

        private void btnStartWP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();

            if (workplaceID != "NONE")
            {
                if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                {
                    if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (hasReplannig.Startwp(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                        {
                            planninType = replanningType.rpStartWP;
                            parentControle.Visible = false;
                            parentControle.Controls.Clear();
                            ucStartWP = new StartWorkplace { Parent = parentControle, Dock = DockStyle.Left };
                            ucStartWP.StopWorkplace(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString());
                            rpPreplanningManage.Visible = false;
                            rpReplanning.Visible = true;
                            parentControle.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("The selected workplace is already moved/is not stopped");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Un approved Replanning already exists for this workplace.");
                }
            }
            else
            {
                MessageBox.Show("Replanning cannot be applied to Unapproved workplaces.");
            }
        }

        private void btnReplaceWorkplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PlanningClass.PlanningScreen.ReplaceWorkplace() == true)
            {

            }
            else
            {
                if (PlanningClass.PlanningScreen.theActivity == 0 || PlanningClass.PlanningScreen.theActivity == 1)
                {
                    ucPreplanning ucp = new ucPreplanning { theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                    ucp.LoadPrePlanningData(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(barProductionMonthPlanning.EditValue)), barSectionPlanning.EditValue.ToString(), Convert.ToInt32(barActivityPlanning.EditValue.ToString()));
                }
                else
                {

                }
            }
        }

        private void btnStopWP1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();
            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            hasReplannig.SetsystemDBTag = this.theSystemDBTag;
            hasReplannig.SetUserCurrentInfo = this.UserCurrentInfo;

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == true)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                        {
                            if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == false)
                            {
                                planninType = replanningType.rpStopWp;
                                parentControle.Visible = false;
                                parentControle.Controls.Clear();
                                ucStopWorkplace = new ucStopWorkplace { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                                ucStopWorkplace.StopWorkplace(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString());

                                rpPreplanningManage.Visible = false;
                                rpRevisedPlanning.Visible = false;
                                rpReplanning.Visible = true;
                                parentControle.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("The selected workplace is already stopped");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Un approved Replanning already exists for this workplace.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to unapproved workplaces.");
                }
            }
        }

        private void btnRePlanAddWorkplace1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();
            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            planninType = replanningType.rpNewWP;
            parentControle.Visible = false;
            parentControle.Controls.Clear();
            ucAddWorkplace = new ucAddWorkplace { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
            ucAddWorkplace.addWorkplace(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));

            rpPreplanningManage.Visible = false;
            rpRevisedPlanning.Visible = false;
            rpReplanning.Visible = true;
            parentControle.Visible = true;
        }

        private void btnDoUnApprovePlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ucUnApprovePlanning.unApproveSelected() == true)
            {
                parentControle.Visible = false;
                parentControle.Controls.Clear();
                switch (PlanningClass.PlanningSettings.ActivityID)
                {
                    case 0:
                        PlanningClass.LoadPlanning();
                        PlanningClass.StopePrePlanning = new ucStopePrePlanning();
                        PlanningClass.StopePrePlanning.Dock = DockStyle.Fill;
                        PlanningClass.StopePrePlanning.SetPlanningClass(PlanningClass);
                        parentControle.Controls.Add(PlanningClass.StopePrePlanning);
                        break;
                    case 1:
                        PlanningClass.PlanningScreen.reloadWorkplaces();
                        PlanningClass.PlanningScreen.Parent = parentControle;
                        break;

                }

                rpPreplanningManage.Visible = true;
                rpPlanningUnApprove.Visible = false;
                parentControle.Visible = true;
            }
        }

        private void btnCancelPlanUnApprove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            parentControle.Visible = false;
            parentControle.Controls.Clear();
            switch (PlanningClass.PlanningSettings.ActivityID)
            {
                case 0:
                    PlanningClass.LoadPlanning();
                    PlanningClass.StopePrePlanning = new ucStopePrePlanning();
                    PlanningClass.StopePrePlanning.Dock = DockStyle.Fill;
                    PlanningClass.StopePrePlanning.SetPlanningClass(PlanningClass);
                    parentControle.Controls.Add(PlanningClass.StopePrePlanning);
                    break;
                case 1:
                    PlanningClass.PlanningScreen.reloadWorkplaces();
                    PlanningClass.PlanningScreen.Parent = parentControle;
                    break;

            }
            rpPreplanningManage.Visible = true;
            rpPlanningUnApprove.Visible = false;
            parentControle.Visible = true;
        }

        private void btnChangeValues1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            hasReplannig.SetsystemDBTag = this.theSystemDBTag;
            hasReplannig.SetUserCurrentInfo = this.UserCurrentInfo;

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == true)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                        {
                            if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == false)
                            {
                                planninType = replanningType.rpCallCahnges;
                                parentControle.Visible = false;
                                parentControle.Controls.Clear();
                                ucPlanningValueChanges = new ucPlanningValueChanges { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                                ucPlanningValueChanges.PlanningChange(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));
                                rpPreplanningManage.Visible = false;
                                rpRevisedPlanning.Visible = false;
                                rpReplanning.Visible = true;
                                parentControle.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("The selected workplace is already stopped");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Un approved Replanning already exists for this workplace.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to unapproved workplaces.");
                }
            }
        }

        private void btnCrewMiner1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            hasReplannig.SetsystemDBTag = this.theSystemDBTag;
            hasReplannig.SetUserCurrentInfo = this.UserCurrentInfo;

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == true)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                        {
                            if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == false)
                            {
                                planninType = replanningType.rpCrewChnages;
                                parentControle.Visible = false;
                                parentControle.Controls.Clear();
                                ucCrewMinerChange = new ucCrewMinerChange { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                                ucCrewMinerChange.changecrew(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));
                                rpPreplanningManage.Visible = false;
                                rpRevisedPlanning.Visible = false;
                                rpReplanning.Visible = true;
                                parentControle.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("The selected workplace is already stopped");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Un approved Replanning already exists for this workplace.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to unapproved workplaces.");
                }
            }
        }

        private void btnMovePlanning1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            hasReplannig.SetsystemDBTag = this.theSystemDBTag;
            hasReplannig.SetUserCurrentInfo = this.UserCurrentInfo;

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == true)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                        {
                            if (hasReplannig.result(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                            {
                                if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == false)
                                {
                                    planninType = replanningType.rpMoveWP;
                                    parentControle.Visible = false;
                                    parentControle.Controls.Clear();
                                    ucMoveBookings = new MoveBookings { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                                    ucMoveBookings.MoveBooking(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));
                                    rpPreplanningManage.Visible = false;
                                    rpRevisedPlanning.Visible = false;
                                    rpReplanning.Visible = true;
                                    rpRevisedPlanning.Visible = false;
                                    parentControle.Visible = true;
                                }
                                else
                                {
                                    MessageBox.Show("The selected workplace is already stopped");
                                }
                            }
                            else
                            {
                                MessageBox.Show("The selected workplace is already stopped");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Un approved Replanning already exists for this workplace.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to approved workplaces.");
                }
            }
            else
            {
                MessageBox.Show("Replanning cannot be applied to Unapproved workplaces.");
            }
        }

        private void btnReplanningStatus1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            parentControle.Visible = false;
            parentControle.Controls.Clear();
            ucrequeststatus = new ucRequeststatus { Parent = parentControle, Dock = DockStyle.Fill, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo, MOID = PlanningClass.PlanningScreen.theSectionIDMO, Prodmonth = PlanningClass.PlanningScreen.theProdMonth };
            rpPreplanningManage.Visible = false;
            rpRevisedPlanning.Visible = false;
            rpReplanningStatus.Visible = true;
            parentControle.Visible = true;

            if (ucrequeststatus._RequestList1.ResultsDataTable.DefaultView.Count == 0)
            {
                btnApprove.Enabled = false;
                btnDecline.Enabled = false;
            }
            else
            {
                ucrequeststatus.gcRequestList.Focus();
                btnApprove.Enabled = true;
                btnDecline.Enabled = true;
            }
        }

        private void btnStartWP1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            hasReplannig.SetsystemDBTag = this.theSystemDBTag;
            hasReplannig.SetUserCurrentInfo = this.UserCurrentInfo;

            if (workplaceID != "NONE")
            {
                if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                {

                    if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        planninType = replanningType.rpStartWP;
                        parentControle.Visible = false;
                        parentControle.Controls.Clear();
                        ucStartWP = new StartWorkplace { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                        ucStartWP.StopWorkplace(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString());
                        rpPreplanningManage.Visible = false;
                        rpRevisedPlanning.Visible = false;
                        rpReplanning.Visible = true;
                        parentControle.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Un approved Replanning already exists for this workplace.");
                }
            }
            else
            {
                MessageBox.Show("Replanning cannot be applied to Unapproved workplaces.");
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barSectionRevised.EditValue == null)
            {
                theMessage.viewMessage(MessageType.Info, "INVALID SECTION", "Please select a valid section", ButtonTypes.OK, MessageDisplayType.FullScreen);
            }
            else if (barActivity1.EditValue == null)
            {
                theMessage.viewMessage(MessageType.Info, "INVALID ACTIVITY", "Please select a valid activity", ButtonTypes.OK, MessageDisplayType.FullScreen);
            }
            else
            {
                Int32 TheCurrentmonth = Convert.ToInt32(PlanningClass.CurrentProdMonth);
                Int32 TheSelectmonth = TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue));
                

                //if (TheCurrentmonth < TheSelectmonth)
                //{
                //    return;
                //}
                if (barActivity1.EditValue.ToString() == "1")
                {
                    btnDrillRigChange.Enabled = true;
                }
                else
                {
                    btnDrillRigChange.Enabled = false;
                }

                MWDataManager.clsDataAccess _RevisedPlansecuritycheck = new MWDataManager.clsDataAccess();
                _RevisedPlansecuritycheck.ConnectionString = TConnections.GetConnectionString(this.theSystemDBTag, UserCurrentInfo.Connection);
                _RevisedPlansecuritycheck.SqlStatement = "sp_CheckRevisedPlanning";
                _RevisedPlansecuritycheck.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                SqlParameter[] _paramCollection11 =
                {
                    _RevisedPlansecuritycheck.CreateParameter("@Prodmonth", SqlDbType.Int  , 0, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))),
                    _RevisedPlansecuritycheck.CreateParameter("@Section", SqlDbType.VarChar , 50,barSectionRevised.EditValue.ToString())
                };

                _RevisedPlansecuritycheck.ParamCollection = _paramCollection11;
                _RevisedPlansecuritycheck.queryReturnType = MWDataManager.ReturnType.DataTable;
                _RevisedPlansecuritycheck.ExecuteInstruction();
                DataTable revisedcheck = new DataTable();
                revisedcheck = _RevisedPlansecuritycheck.ResultsDataTable;
                if (revisedcheck.Rows.Count == 0)
                {
                    MessageBox.Show("There are no users setup to approve for this section. Please setup a user for approval.", "", MessageBoxButtons.OK);
                }
                else
                {
                    bool canLoad = false;
                    if (PlanningClass.isRevised == true)
                    {
                        editProdmonthRevised.Enabled = false;
                    }
                    else
                    {
                        editProdmonthRevised.Enabled = true;
                    }
                    if (barSectionRevised.EditValue == null)
                    {
                        MessageBox.Show("Please select a section!");
                    }
                    else
                    {
                        if (validateSelectionsRevised())
                        {
                            PlanningClass.PlanningSettings.ActivityID = Convert.ToInt16(barActivity1.EditValue.ToString());
                            PlanningClass.PlanningSettings.MOSectionID = barSectionRevised.EditValue.ToString();
                            PlanningClass.PlanningSettings.ActivityString = editActivity4.GetDisplayText(PlanningClass.PlanningSettings
                        .ActivityID);
                            PlanningClass.PlanningSettings.IsRevised = true;
                            PlanningClass.PlanningSettings.ProdMonth = TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue));
                            PlanningClass.Activity = Convert.ToInt16(barActivity1.EditValue.ToString());
                            PlanningClass.SectionID = barSectionRevised.EditValue.ToString();
                            PlanningClass.ProdMonth = TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue));
                            PlanningClass.isRevised = true;
                            PlanningClass.systemDBTag = this.theSystemDBTag;

                            PlanningClass.PlanningScreen.Dock = DockStyle.Fill;
                            PlanningClass.PlanningScreen.theSystemTag = this.theSystemTag;
                            PlanningClass.PlanningScreen.theSystemDBTag = this.theSystemDBTag;
                            PlanningClass.PlanningScreen.UserCurrentInfo = this.UserCurrentInfo;
                            PlanningClass.PlanningScreen.PlanningClass = this.PlanningClass;
                            canLoad = PlanningClass.PlanningScreen.loadPreplanning(TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue)), barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));

                            if (canLoad)
                            {
                                loadMenuItems();
                                parentControle.Controls.Add(PlanningClass.PlanningScreen);
                            }

                            updateSecurity();
                        }

                        MWDataManager.clsDataAccess _RequiredPM = new MWDataManager.clsDataAccess();
                        _RequiredPM.ConnectionString = TConnections.GetConnectionString(this.theSystemDBTag, UserCurrentInfo.Connection);
                        _RequiredPM.SqlStatement = "sp_PrePlanning_Prodmonth";
                        _RequiredPM.queryExecutionType = MWDataManager.ExecutionType.StoreProcedure;

                        SqlParameter[] _paramCollection1 =
                        {
                            _RequiredPM.CreateParameter("@SECTIONID_2", SqlDbType.VarChar , 50,barSectionRevised.EditValue.ToString())
                        };

                        _RequiredPM.ParamCollection = _paramCollection1;
                        _RequiredPM.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _RequiredPM.ExecuteInstruction();

                        DataTable dt = new DataTable();
                        dt = _RequiredPM.ResultsDataTable;
                        int prm = TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue));

                        if (canLoad == false)
                        {
                            if (Convert.ToString(PlanningClass.isRevised) == "False")
                            {
                                updateSecurity();
                            }
                            else
                            {
                                rpRevisedPlanning.Visible = true;
                                btnAddWorkPlace.Enabled = false;
                                btnDeleteWorkplace.Enabled = false;
                                btnSavePlanning.Enabled = true;
                                btnApprovePrePlanning.Enabled = false;
                                rpPreplanning.Visible = false;
                                rpPreplanningManage.Visible = false;
                                rpReplanningStatus.Visible = false;

                                btnRevisedAddWP.Enabled = false;
                                btnRevisedCrewMiner.Enabled = false;
                                btnRevisedStatus.Enabled = true;
                                btnRevisedValues.Enabled = false;
                                btnRevisedStopWP.Enabled = false;
                                btnRevisedStartWP.Enabled = false;
                                btnRevisedMovePlan.Enabled = false;
                                rpRevisedPlanningOptions.Visible = false;
                                btnMiningMethodChange.Enabled = false;

                                if (barActivity1.EditValue.ToString() == "1")
                                {
                                    btnDrillRigChange.Enabled = true;
                                }
                                else
                                {
                                    btnDrillRigChange.Enabled = false;
                                }
                                updateSecurity();
                            }
                            if (canLoad)
                            {
                                updateSecurity();
                                loadMenuItems();
                                parentControle.Controls.Add(PlanningClass.PlanningScreen);
                            }
                        }
                        else
                        {
                            if (Convert.ToString(PlanningClass.isRevised) == "False")
                            {
                                switch (TUserInfo.theSecurityLevel("PPAPP"))
                                {
                                    case 0:
                                        btnApprovePrePlanning.Enabled = false;
                                        break;
                                    case 2:
                                        btnApprovePrePlanning.Enabled = true;
                                        break;
                                }
                                switch (TUserInfo.theSecurityLevel("PPUAP"))
                                {
                                    case 0:
                                        btnUnApprovePlanning.Enabled = false;
                                        break;
                                    case 2:
                                        btnUnApprovePlanning.Enabled = true;
                                        break;
                                }
                                if (dt.Rows.Count != 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        if (prm < Convert.ToInt32(dr["CurrentProductionMonth"]))
                                        {
                                            btnReplaceWorkplace.Enabled = false;
                                            btnSavePlanning.Enabled = true;
                                            rpPreplanning.Visible = false;
                                            rpPreplanningManage.Visible = true;
                                            rpReplanningStatus.Visible = false;
                                            rpRevisedPlanning.Visible = false;
                                            rpRevisedPlanningOptions.Visible = false;
                                        }
                                        else if (prm > Convert.ToInt32(dr["CurrentProductionMonth"]))
                                        {
                                            btnAddWorkPlace.Enabled = true;
                                            btnReplaceWorkplace.Enabled = true;
                                            btnDeleteWorkplace.Enabled = true;
                                            btnSavePlanning.Enabled = true;
                                            rpPreplanning.Visible = false;
                                            rpPreplanningManage.Visible = true;
                                            rpReplanningStatus.Visible = false;
                                            rpRevisedPlanning.Visible = false;
                                            rpRevisedPlanningOptions.Visible = false;
                                        }
                                        else if (prm == Convert.ToInt32(dr["CurrentProductionMonth"]))
                                        {
                                            btnReplaceWorkplace.Enabled = false;
                                            btnSavePlanning.Enabled = true;
                                            btnApprovePrePlanning.Enabled = true;
                                            rpPreplanning.Visible = false;
                                            rpPreplanningManage.Visible = true;
                                            rpReplanningStatus.Visible = false;
                                            rpRevisedPlanning.Visible = false;
                                            rpRevisedPlanningOptions.Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    btnAddWorkPlace.Enabled = true;
                                    btnReplaceWorkplace.Enabled = true;
                                    btnDeleteWorkplace.Enabled = true;
                                    btnSavePlanning.Enabled = true;

                                    rpReplanning.Visible = false;
                                    rpPreplanning.Visible = false;
                                    rpPreplanningManage.Visible = true;
                                    rpReplanningStatus.Visible = false;
                                    rpRevisedPlanning.Visible = false;
                                    rpRevisedPlanningOptions.Visible = false;
                                }
                            }
                            else
                            {
                                btnAddWorkPlace.Enabled = false;
                                btnReplaceWorkplace.Enabled = false;
                                btnDeleteWorkplace.Enabled = false;
                                btnSavePlanning.Enabled = true;
                                btnRevisedStatus.Enabled = true;
                                btnRevisedAddWP.Enabled = true;
                                rpReplanning.Visible = false;
                                rpPreplanning.Visible = false;
                                rpPreplanningManage.Visible = false;
                                rpReplanningStatus.Visible = false;
                                rpRevisedPlanning.Visible = true;
                                rpRevisedPlanningOptions.Visible = false;

                                if (barActivity1.EditValue.ToString() == "1")
                                {
                                    btnDrillRigChange.Enabled = true;
                                }
                                else
                                {
                                    btnDrillRigChange.Enabled = false;
                                }
                                updateSecurity();
                            }
                        }
                    }
                }
            }
        }

        private void btnCancelRvp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool theResult = PlanningClass.PlanningScreen.canClosePlanning();
            if (theResult == true)
            {
                rpRevisedPlanningOptions.Visible = true;
                rpPreplanningManage.Visible = false;
                rpRevisedPlanning.Visible = false;
                PlanningClass.PlanningScreen.Parent = null;
                PlanningClass.PlanningScreen.updateReadOnly();
            }

            if (PlanningClass.isRevised)
            {
                theUser.SetUserInfo(TUserInfo.UserID, theSystemDBTag, UserCurrentInfo.Connection);
                if (theUser.BackdatedRevisedPlanning == true)
                {
                    editProdmonthRevised.Enabled = true;
                }
                else
                    editProdmonthRevised.Enabled = false;
            }
        }

        private void ucPrePlanningMain_Load(object sender, EventArgs e)
        {
            if (UserCurrentInfo.UserID.Contains("MINEWARE") == true || UserCurrentInfo.UserID.Contains("mineware") == true)
            {
                btnGeneratePPReports.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnUpdateApproveFlag.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                rpgCreatePPReport.Visible = true;
            }
            else
            {
                btnGeneratePPReports.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnUpdateApproveFlag.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                rpgCreatePPReport.Visible = false;
            }

            CbxCallDriven.EditValue = false;

            PlanningClass.UserCurrentInfo = this.UserCurrentInfo;
            PlanningClass.systemDBTag = this.theSystemDBTag;
            PlanningClass.theData.ConnectionString = TConnections.GetConnectionString(this.theSystemDBTag, UserCurrentInfo.Connection);

            if (PlanningClass.isRevised)
            {
                rpPreplanning.Visible = false;
                rpRevisedPlanningOptions.Visible = true;
                theUser.SetUserInfo(TUserInfo.UserID, theSystemDBTag, UserCurrentInfo.Connection);
                if (theUser.BackdatedRevisedPlanning == true)
                {
                    editProdmonthRevised.Enabled = true;
                }
                else
                    editProdmonthRevised.Enabled = false;
            }
            else
            {
                rpPreplanning.Visible = true;
                rpRevisedPlanningOptions.Visible = false;
            }

            CPMBusinessLayer.clsBusinessLayer BMEBL = new CPMBusinessLayer.clsBusinessLayer();
            BMEBL._queryReturnType = CPMBusinessLayer.ReturnType.DataTable;

            BMEBL.SetsystemDBTag = this.theSystemDBTag;
            BMEBL.SetUserCurrentInfo = this.UserCurrentInfo;

            setSections();

            if (PlanningClass.isRevised == true)
            {
                if (BMEBL.get_ReviseActivityPlan() == true)
                {
                    editActivity.DataSource = BMEBL.ResultsDataTable;
                    editActivity.DisplayMember = "Desc";
                    editActivity.ValueMember = "Code";

                    editActivity4.DataSource = BMEBL.ResultsDataTable;
                    editActivity4.DisplayMember = "Desc";
                    editActivity4.ValueMember = "Code";
                }
            }
            else
            {
                if (BMEBL.get_ActivityPlan() == true)
                {
                    editActivity.DataSource = BMEBL.ResultsDataTable;
                    editActivity.DisplayMember = "Desc";
                    editActivity.ValueMember = "Code";

                    editActivity4.DataSource = BMEBL.ResultsDataTable;
                    editActivity4.DisplayMember = "Desc";
                    editActivity4.ValueMember = "Code";

                }
            }

            barProductionMonthPlanning.EditValue = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
            editProdmonthRevised.EditValue = TProductionGlobal.ProdMonthAsDate(TProductionGlobal.getSystemSettingsProductioInfo(UserCurrentInfo.Connection).CurrentProductionMonth.ToString());
        }

        private void setSections()
        {
            if (PlanningClass.tblSections != null)
            {
                editSections.DataSource = PlanningClass.tblSections;
                editSections.DisplayMember = "Name";
                editSections.ValueMember = "SectionID";

                editRevisedSections.DataSource = PlanningClass.tblSections;
                editRevisedSections.DisplayMember = "Name";
                editRevisedSections.ValueMember = "SectionID";

                if (PlanningClass.tblSections.Rows.Count == 0)
                    theMessage.viewMessage(MessageType.Info, "NO SECTIONS", "There are no sections avaliable for production month " + PlanningClass.ProdMonth, ButtonTypes.OK, MessageDisplayType.FullScreen);
            }
        }

        private void editProductionMonth1_EditValueChanging_1(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            theNewMonth = Convert.ToInt32(e.NewValue.ToString());
        }

        private void btnCyclePlanning_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PlanningClass.PlanningSettings.ActivityID == 0)
            {
                PlanningClass.StopePrePlanning.ShowCycle(btnCyclePlanning.Down);
            }
            else
            {
                PlanningClass.PlanningScreen.viewPlanningStoping.PostEditor();
                PlanningClass.PlanningScreen.viewPlanningDevelopment.PostEditor();
                PlanningClass.PlanningScreen.viewPlanningSundry.PostEditor();
                PlanningClass.PlanningScreen.viewPlanningSweepVamp.PostEditor();

                //PlanningClass.PlanningScreen.cycleActive = !btnCyclePlanning.Down;
                // PlanningClass.PlanningScreen.showCycle(!btnCyclePlanning.Down);
                //PlanningClass.PlanningScreen.cycleActive = btnCyclePlanning.Down;
                PlanningClass.PlanningScreen.showCycle(btnCyclePlanning.Down);
                //PlanningClass.PlanningScreen.savePrePlanning();
                saved = true;


                //PlanningClass.PlanningScreen.cycleActive = !btnCyclePlanning.Down;
                //PlanningClass.PlanningScreen.showCycle(!btnCyclePlanning.Down);
                //PlanningClass.PlanningScreen.cycleActive = btnCyclePlanning.Down;
                //PlanningClass.PlanningScreen.showCycle(btnCyclePlanning.Down); 
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrePlanningNotification _frmPrePlanningNotification = new frmPrePlanningNotification();
            parentControle.Controls.Clear();
            _frmPrePlanningNotification.UserCurrentInfo = this.UserCurrentInfo;
            _frmPrePlanningNotification.theSystemDBTag = this.theSystemDBTag;
            parentControle.Controls.Add(_frmPrePlanningNotification);
        }

        private void btnLabour_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningClass.PlanningScreen.cycleActive = !btnLabour.Down;
            PlanningClass.PlanningScreen.showLabour(!btnLabour.Down);
            PlanningClass.PlanningScreen.cycleActive = btnLabour.Down;
            PlanningClass.PlanningScreen.showLabour(btnLabour.Down);
            labourresult = false;
        }

        private void barSectionRevised_EditValueChanged(object sender, EventArgs e)
        {
            PlanningClass.SectionID = barSectionRevised.EditValue.ToString();
            dtProdmonth = PlanningClass.GetProdmonth(PlanningClass.SectionID, System.DateTime.Today.ToString());
            //string thePM = PlanningClass.CurrentProdMonth;
            //if(thePM != "")
            //   editProdmonthRevised.EditValue = TProductionGlobal.ProdMonthAsDate(thePM);

            if (dtProdmonth.Rows.Count != 0)
            {
                foreach (DataRow dr in dtProdmonth.Rows)
                {
                    string thePM = dr["Prodmonth"].ToString();
                    if(thePM != "")
                    {
                        editProdmonthRevised.EditValue = TProductionGlobal.ProdMonthAsDate(thePM);
                    }
                    else
                    {
                        editProdmonthRevised.EditValue = TProductionGlobal.ProdMonthAsDate(PlanningClass.CurrentProdMonth);

                    }
                }
            }
            else
            {
                string thePM = PlanningClass.CurrentProdMonth;
                if (thePM != "")
                {
                    editProdmonthRevised.EditValue = TProductionGlobal.ProdMonthAsDate(thePM);
                }
            }
        }

        private void btnMiningMethodChange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            hasReplannig.SetsystemDBTag = this.theSystemDBTag;
            hasReplannig.SetUserCurrentInfo = this.UserCurrentInfo;

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == true)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                        {
                            if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == false)
                            {
                                planninType = replanningType.rpMiningMethodChange;
                                parentControle.Visible = false;
                                parentControle.Controls.Clear();

                                miningMethodChange = new ucMiningMethodChange { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                                miningMethodChange.MiningMethod(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));
                                rpPreplanningManage.Visible = false;
                                rpRevisedPlanning.Visible = false;
                                rpReplanning.Visible = true;
                                parentControle.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("The selected workplace is already stopped");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Un approved Replanning already exists for this workplace.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to unapproved workplaces.");
                }
            }
        }

        private void btnDrillRigChange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            hasReplannig.SetsystemDBTag = this.theSystemDBTag;
            hasReplannig.SetUserCurrentInfo = this.UserCurrentInfo;

            if (workplaceID != "NONE")
            {
                if (hasReplannig.approved(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == true)
                {
                    if (hasReplannig.hasReplanning(workplaceID, TProductionGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                    {
                        if (hasReplannig.CheckWP(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue))) == false)
                        {
                            if (PlanningClass.PlanningScreen.isWorplaceStopped(workplaceID) == false)
                            {
                                planninType = replanningType.rpDrillRig;
                                parentControle.Visible = false;
                                parentControle.Controls.Clear();

                                drillRigChange = new ucDrillRiggChanges { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                                drillRigChange.DrillRig(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));
                                rpPreplanningManage.Visible = false;
                                rpRevisedPlanning.Visible = false;
                                rpReplanning.Visible = true;
                                parentControle.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("The selected workplace is already stopped");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Un approved Replanning already exists for this workplace.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Un approved Replanning already exists for this workplace.");
                    }
                }
                else
                {
                    MessageBox.Show("Replanning cannot be applied to unapproved workplaces.");
                }
            }
        }

        private void btnUpdateApproveFlag_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PlanningClass.PlanningScreen.UpdateApproveFlag();
        }

        private void btnDelWorkplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string workplaceID = PlanningClass.PlanningScreen.getSelectedWorkplace();

            string sectionID = PlanningClass.PlanningScreen.getSelectedSectionID();
            CPMBusinessLayer.clsBusinessLayer hasReplannig = new CPMBusinessLayer.clsBusinessLayer();
            hasReplannig.SetsystemDBTag = this.theSystemDBTag;
            hasReplannig.SetUserCurrentInfo = this.UserCurrentInfo;

            if (workplaceID != "NONE")
            {
                planninType = replanningType.rpDeleteWorkplace;
                parentControle.Visible = false;
                parentControle.Controls.Clear();

                DeleteWorkplace = new ucDeleteWorkplace { Parent = parentControle, Dock = DockStyle.Left, theSystemDBTag = this.theSystemDBTag, UserCurrentInfo = this.UserCurrentInfo };
                DeleteWorkplace.DeleteWorkplace(workplaceID, TProductionGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonthRevised.EditValue)), sectionID, barSectionRevised.EditValue.ToString(), Convert.ToInt32(barActivity1.EditValue.ToString()));
                rpPreplanningManage.Visible = false;
                rpRevisedPlanning.Visible = false;
                rpReplanning.Visible = true;
                parentControle.Visible = true;
            }
        }

        private void CbxCallDriven_EditValueChanged(object sender, EventArgs e)
        {
            string aa = CbxCallDriven.EditValue.ToString();

            if (CbxCallDriven.EditValue.ToString() == "True")
            {
                PlanningClass.PlanningScreen._CyclingType = "CallDriven";

                if (lblFirstTime.Text != "Y")
                {
                    PlanningClass.PlanningScreen.ResetSQM();
                    PlanningClass.PlanningScreen.LoadMOSqm();
                }

                lblFirstTime.Text = "N";
            }

            if (CbxCallDriven.EditValue.ToString() == "False")
            {
                PlanningClass.PlanningScreen._CyclingType = "CycDriven";

                if (lblFirstTime.Text != "Y")
                {
                    PlanningClass.PlanningScreen.LoadMOSqm();
                }

                lblFirstTime.Text = "N";
            }
        }
    }
}