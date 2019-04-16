using System;
using System.Collections.Generic;
using Mineware.Menu.Structure;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.HarmonyPASGlobal
{
    public class HarmonyPasMenuStructure
    {
        public mainMenu theMenu = new mainMenu();
        public menuItem miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miBookings_HPASBooking_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miLockPlan_HPASLockPlanningLockPlan_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miEditMeters_HPASPlanningAllowEditMeters_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miEditQubes_HPASPlanningAllowEditQubes_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miEditSQM_HPASPlanningAllowEditSQM_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miEditFaceLength_HPASPlanningAllowFaceLengthEdit_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCyclePlan_HPASPlanningCycle_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanningProtocol_HPASPlanningProtocol_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanningProtocolReport_HPASPlanningProtocolReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanningProtocolTemplates_HPASPlanningProtocolTemplates_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanWorkplaces_HPASPlanningWorkplace_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miAddWorkplace_HPASPlanningWPAddWP_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miApproveWorkplace_HPASPlanningWPApproveWP_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miDeleteWorkplace_HPASPlanningWPDelWP_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miImportCADSMineworkplace_HPASPlanningWPImpWP_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miReplaceWorkplace_HPASPlanningWPReplace_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miUnApproveWorkplace_HPASPlanningWPUnAppWP_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miDeletePlanningProtocolTemplate_HPASPlanProtDelete_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miEditPlanningProtocolTemplate_HPASPlanProtEdit_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miNewPlanningProtocolTemplate_HPASPlanProtNew_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanningProtocolTemplateAccess_HPASPlanProtTempAccess_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanningProtocolTemplateSecurity_HPASPlanProtTempSecurity_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miUnlockPlanningProtcol_HPASPlanProtUnlock_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miBonusReport_HPASReportBonus_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCourseBlankReport_HPASReportCourseBlank_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miStandardSRMReport_HPASReportStandardCRM_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCrewRanking_HPASReportCrewRanking_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miDailyBlast_HPASReportDailyBlast_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miGenericReport_HPASReportGenericReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miLostBlastReport_HPASReportLostBlastReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miMODaily_HPASReportMODaily_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miProblemAnalysis_HPASReportProblemAnalysis_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miProductionAnalysis_HPASReportProductionAnalysis_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miGradeReport_HPASReportsGradeReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSICReport_HPASReportSICReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miMeasuringList_HPASReportsMeasuringList_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlathondWallChartReport_HPASReportsPlathondWallChartReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanningReport_HPASReportsPlanningReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPlanVsBook_HPASReportsPlanVsBook_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSurveyReport_HPASReportSurveyReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miTonnage_HPASReportTonnage_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miTopPanels_HPASReportTopPanels_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miRevisedPlanningAuditReport_HPASRevisedAuditReport_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miQAQC_HPASSystemAdminQAQC_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miAMISSetup_HPASSystemAdminQAQCAMISSetup_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miQAQC_HPASSystemAdminQAQCQAQC_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSystemSettings_HPASSystemAdminSettings_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miBookingSettings_HPASSystemAdminSettingsBook_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miDates_HPASSystemAdminSettingsDates_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miFactors_HPASSystemAdminSettingsFactors_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miMines_HPASSystemAdminSettingsMines_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miLockMonth_HPASSystemAdminSetupCodesLockPlan_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCurrent_HPPASSystemAdminSettingsCurrent_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miLockPlanning_HPASLockPlannning_MinewareSystemsHarmonyPAS = new menuItem();

        public void setMenuItems()
        {
            theMenu.MenuItems.Clear();
            miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS.setItemInfo("HPAS", "Harmony PAS", "Mineware.Systems.HarmonyPAS", "Mineware.Systems.HarmonyPAS", "SI", -1, MenuItemType.SI, "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");
            theMenu.addMenuItem(miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS);
            miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS.setItemInfo("HPAS SystemAdminOrgStructureCrews", "Crews", "HPASSystemAdminOrgStructure", "Mineware.Systems.HarmonyPAS", "SSUI", 2, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS);
            miBookings_HPASBooking_MinewareSystemsHarmonyPAS.setItemInfo("HPASBooking", "Bookings", "HPAS", "Mineware.Systems.HarmonyPAS", "MI", 2, MenuItemType.MI, "", "", "", "");
            theMenu.addMenuItem(miBookings_HPASBooking_MinewareSystemsHarmonyPAS);
            miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingBonusCapture", "Bonus Capture", "HPASBooking", "Mineware.Systems.HarmonyPAS", "UI", 6, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS);
            miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingDailyBookings", "Daily Bookings", "HPASBooking", "Mineware.Systems.HarmonyPAS", "UI", 1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS);
            miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingHoisting", "Hoisting Booking", "HPASBooking", "Mineware.Systems.HarmonyPAS", "UI", 3, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS);
            miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingMilling", "Milling Booking", "HPASBooking", "Mineware.Systems.HarmonyPAS", "UI", 4, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS);
            miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingSIC", "SIC Capture", "HPASBooking", "Mineware.Systems.HarmonyPAS", "UI", 5, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS);
            miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingsOreflowStorages", "Oreflow Storages", "HPASBooking", "Mineware.Systems.HarmonyPAS", "UI", -1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS);
            miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingTramming", "Tramming Booking", "HPASBooking", "Mineware.Systems.HarmonyPAS", "UI", 2, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS);
            miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS.setItemInfo("HPASCapturePlanningProtocol", "Capture Planning Protocol", "HPASPlanningProtocol", "Mineware.Systems.HarmonyPAS", "UI", -1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS);
            miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS.setItemInfo("HPASLockPlanningAuthorise", "Authorise Plan", "HPASLockPlannning", "Mineware.Systems.HarmonyPAS", "UI", -1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS);
            miLockPlan_HPASLockPlanningLockPlan_MinewareSystemsHarmonyPAS.setItemInfo("HPASLockPlanning", "Lock Plan", "HPASLockPlanning", "Mineware.Systems.HarmonyPAS", "UI", -1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miLockPlan_HPASLockPlanningLockPlan_MinewareSystemsHarmonyPAS);
            miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanning", "Planning", "HPAS", "Mineware.Systems.HarmonyPAS", "MI", 1, MenuItemType.MI, "", "", "", "");
            theMenu.addMenuItem(miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS);
            miEditMeters_HPASPlanningAllowEditMeters_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningAllowEditMeters", "Edit Meters", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miEditMeters_HPASPlanningAllowEditMeters_MinewareSystemsHarmonyPAS);
            miEditQubes_HPASPlanningAllowEditQubes_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningAllowEditQubes", "Edit Qubes", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miEditQubes_HPASPlanningAllowEditQubes_MinewareSystemsHarmonyPAS);
            miEditSQM_HPASPlanningAllowEditSQM_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningAllowEditSQM", "Edit SQM", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miEditSQM_HPASPlanningAllowEditSQM_MinewareSystemsHarmonyPAS);
            miEditFaceLength_HPASPlanningAllowFaceLengthEdit_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningAllowFaceLengthEdit", "Edit Face Length", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miEditFaceLength_HPASPlanningAllowFaceLengthEdit_MinewareSystemsHarmonyPAS);
            miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningBusinessPlan", "Business Plan Import", "HPASPlanning", "Mineware.Systems.HarmonyPAS", "UI", 1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS);
            miCyclePlan_HPASPlanningCycle_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningCycle", "Cycle Plan", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miCyclePlan_HPASPlanningCycle_MinewareSystemsHarmonyPAS);
            miPlanningProtocol_HPASPlanningProtocol_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningProtocol", "Planning Protocol", "HPASPlanning", "Mineware.Systems.HarmonyPAS", "MI", 2, MenuItemType.MI, "", "", "", "");
            theMenu.addMenuItem(miPlanningProtocol_HPASPlanningProtocol_MinewareSystemsHarmonyPAS);
            miPlanningProtocolReport_HPASPlanningProtocolReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningProtocolReport", "Planning Protocol Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miPlanningProtocolReport_HPASPlanningProtocolReport_MinewareSystemsHarmonyPAS);
            miPlanningProtocolTemplates_HPASPlanningProtocolTemplates_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningProtocolTemplates", "Planning Protocol Templates", "HPASPlanningProtocol", "Mineware.Systems.HarmonyPAS", "UI", -1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miPlanningProtocolTemplates_HPASPlanningProtocolTemplates_MinewareSystemsHarmonyPAS);
            miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningTramming", "Tramming Planning", "HPASPlanning", "Mineware.Systems.HarmonyPAS", "UI", 2, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS);
            miPlanWorkplaces_HPASPlanningWorkplace_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningWorkplace", "Plan Workplaces", "HPASPlanning", "Mineware.Systems.HarmonyPAS", "UI", 1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miPlanWorkplaces_HPASPlanningWorkplace_MinewareSystemsHarmonyPAS);
            miAddWorkplace_HPASPlanningWPAddWP_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningWPAddWP", "Add Workplace", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miAddWorkplace_HPASPlanningWPAddWP_MinewareSystemsHarmonyPAS);
            miApproveWorkplace_HPASPlanningWPApproveWP_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningWPApproveWP", "Approve Workplace", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miApproveWorkplace_HPASPlanningWPApproveWP_MinewareSystemsHarmonyPAS);
            miDeleteWorkplace_HPASPlanningWPDelWP_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningWPDelWP", "Delete Workplace", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miDeleteWorkplace_HPASPlanningWPDelWP_MinewareSystemsHarmonyPAS);
            miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningWPImportPreviousMonth", "Allow Import Previous Month", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS);
            miImportCADSMineworkplace_HPASPlanningWPImpWP_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningWPImpWP", "Import CADSMine workplace", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miImportCADSMineworkplace_HPASPlanningWPImpWP_MinewareSystemsHarmonyPAS);
            miReplaceWorkplace_HPASPlanningWPReplace_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningWPReplace", "Replace Workplace", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miReplaceWorkplace_HPASPlanningWPReplace_MinewareSystemsHarmonyPAS);
            miUnApproveWorkplace_HPASPlanningWPUnAppWP_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningWPUnAppWP", "Un Approve Workplace", "HPASPlanningWorkplace", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miUnApproveWorkplace_HPASPlanningWPUnAppWP_MinewareSystemsHarmonyPAS);
            miDeletePlanningProtocolTemplate_HPASPlanProtDelete_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanProtDelete", "Delete Planning Protocol Template", "HPASPlanningProtocolTemplates", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miDeletePlanningProtocolTemplate_HPASPlanProtDelete_MinewareSystemsHarmonyPAS);
            miEditPlanningProtocolTemplate_HPASPlanProtEdit_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanProtEdit", "Edit Planning Protocol Template", "HPASPlanningProtocolTemplates", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miEditPlanningProtocolTemplate_HPASPlanProtEdit_MinewareSystemsHarmonyPAS);
            miNewPlanningProtocolTemplate_HPASPlanProtNew_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanProtNew", "New Planning Protocol Template", "HPASPlanningProtocolTemplates", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miNewPlanningProtocolTemplate_HPASPlanProtNew_MinewareSystemsHarmonyPAS);
            miPlanningProtocolTemplateAccess_HPASPlanProtTempAccess_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanProtTempAccess", "Planning Protocol Template Access", "HPASPlanningProtocolTemplates", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miPlanningProtocolTemplateAccess_HPASPlanProtTempAccess_MinewareSystemsHarmonyPAS);
            miPlanningProtocolTemplateSecurity_HPASPlanProtTempSecurity_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanProtTempSecurity", "Planning Protocol Template Security", "HPASPlanningProtocolTemplates", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miPlanningProtocolTemplateSecurity_HPASPlanProtTempSecurity_MinewareSystemsHarmonyPAS);
            miUnlockPlanningProtcol_HPASPlanProtUnlock_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanProtUnlock", "Unlock Planning Protcol", "HPASPlanningProtocol", "Mineware.Systems.HarmonyPAS", "UIF", -1, MenuItemType.UIF, "", "", "", "");
            theMenu.addMenuItem(miUnlockPlanningProtcol_HPASPlanProtUnlock_MinewareSystemsHarmonyPAS);
            miBonusReport_HPASReportBonus_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportBonus", "Bonus Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miBonusReport_HPASReportBonus_MinewareSystemsHarmonyPAS);
            miCourseBlankReport_HPASReportCourseBlank_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportCourseBlank", "Course Blank Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miCourseBlankReport_HPASReportCourseBlank_MinewareSystemsHarmonyPAS);
            miStandardSRMReport_HPASReportStandardCRM_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportStandardSRM", "Standard SRM Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miStandardSRMReport_HPASReportStandardCRM_MinewareSystemsHarmonyPAS);
            miCrewRanking_HPASReportCrewRanking_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportCrewRanking", "Crew Ranking", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miCrewRanking_HPASReportCrewRanking_MinewareSystemsHarmonyPAS);
            miDailyBlast_HPASReportDailyBlast_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportDailyBlast", "Daily Blast", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miDailyBlast_HPASReportDailyBlast_MinewareSystemsHarmonyPAS);
            miGenericReport_HPASReportGenericReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportGenericReport", "Generic Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miGenericReport_HPASReportGenericReport_MinewareSystemsHarmonyPAS);
            miLostBlastReport_HPASReportLostBlastReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportLostBlastReport", "Lost Blast Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miLostBlastReport_HPASReportLostBlastReport_MinewareSystemsHarmonyPAS);
            miMODaily_HPASReportMODaily_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportMODaily", "MO Daily", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miMODaily_HPASReportMODaily_MinewareSystemsHarmonyPAS);
            miProblemAnalysis_HPASReportProblemAnalysis_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportProblemAnalysis", "Problem Analysis", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miProblemAnalysis_HPASReportProblemAnalysis_MinewareSystemsHarmonyPAS);
            miProductionAnalysis_HPASReportProductionAnalysis_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportProductionAnalysis", "Production Analysis", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miProductionAnalysis_HPASReportProductionAnalysis_MinewareSystemsHarmonyPAS);
            miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS.setItemInfo("HPASReports", "Harmony Reports", "HPAS", "Mineware.Systems.HarmonyPAS", "RMI", 4, MenuItemType.RMI, "", "", "", "");
            theMenu.addMenuItem(miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS);
            miGradeReport_HPASReportsGradeReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportsGradeReport", "Grade Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miGradeReport_HPASReportsGradeReport_MinewareSystemsHarmonyPAS);
            miSICReport_HPASReportSICReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportSICReport", "SIC Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miSICReport_HPASReportSICReport_MinewareSystemsHarmonyPAS);
            miMeasuringList_HPASReportsMeasuringList_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportsMeasuringList", "Measuring List", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miMeasuringList_HPASReportsMeasuringList_MinewareSystemsHarmonyPAS);
            miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportsPlanningReportNewStyle", "Planning Report New Style", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS);

            miPlathondWallChartReport_HPASReportsPlathondWallChartReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportsPlathondWallChartReport", "Plathond Wall Chart Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miPlathondWallChartReport_HPASReportsPlathondWallChartReport_MinewareSystemsHarmonyPAS);

            miPlanningReport_HPASReportsPlanningReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportsPlanningReport", "Planning Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miPlanningReport_HPASReportsPlanningReport_MinewareSystemsHarmonyPAS);
            miPlanVsBook_HPASReportsPlanVsBook_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportsPlanVsBook", "Plan Vs Book", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miPlanVsBook_HPASReportsPlanVsBook_MinewareSystemsHarmonyPAS);
            miSurveyReport_HPASReportSurveyReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportSurveyReport", "Survey Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miSurveyReport_HPASReportSurveyReport_MinewareSystemsHarmonyPAS);
            miTonnage_HPASReportTonnage_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportTonnage", "Tonnage", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miTonnage_HPASReportTonnage_MinewareSystemsHarmonyPAS);
            miTopPanels_HPASReportTopPanels_MinewareSystemsHarmonyPAS.setItemInfo("HPASReportTopPanels", "Top Panels", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miTopPanels_HPASReportTopPanels_MinewareSystemsHarmonyPAS);
            miRevisedPlanningAuditReport_HPASRevisedAuditReport_MinewareSystemsHarmonyPAS.setItemInfo("HPASRevisedAuditReport", "Revised Planning Audit Report", "HPASReports", "Mineware.Systems.HarmonyPAS", "RP", -1, MenuItemType.RP, "", "", "", "");
            theMenu.addMenuItem(miRevisedPlanningAuditReport_HPASRevisedAuditReport_MinewareSystemsHarmonyPAS);
            miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS.setItemInfo("HPASRevisedPlanning", "Revised Planning", "HPASPlanning", "Mineware.Systems.HarmonyPAS", "UI", 5, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS);
            miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS.setItemInfo("HPASRevisedPlanningSecurity", "Revised Planning Security", "HPASPlanning", "Mineware.Systems.HarmonyPAS", "UI", 6, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS);
            miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS.setItemInfo("HPASSurvey", "Survey", "HPAS", "Mineware.Systems.HarmonyPAS", "MI", 3, MenuItemType.MI, "", "", "", "");
            theMenu.addMenuItem(miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS);
            miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS.setItemInfo("HPASSurveyCapture", "Survey Capture", "HPASSurvey", "Mineware.Systems.HarmonyPAS", "UI", -1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS);
            miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdmin", "Production System Administration", "HPAS", "Mineware.Systems.HarmonyPAS", "SSMI", 5, MenuItemType.SSMI, "", "", "", "");
            theMenu.addMenuItem(miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS);
            miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminBoxholes", "Boxholes", "HPASSystemAdminGeoStructure", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS);
            miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendars", "Calendars", "HPASSystemAdmin", "Mineware.Systems.HarmonyPAS", "SSMG", -1, MenuItemType.SSMG, "", "", "", "");
            theMenu.addMenuItem(miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS);
            miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendarsCaltype", "Calendar Types", "HPASSystemAdminCalendars", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS);
            miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendarsMillCalendar", "Other Calendars", "HPASSystemAdminCalendars", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS);
            miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendarsSectionCalendar", "Section Calendar", "HPASSystemAdminCalendars", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS);
            miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminEndtypes", "Endtypes", "HPASSystemAdminGeoStructure", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS);
            miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminGeoStructure", "Geo Structure", "HPASSystemAdmin", "Mineware.Systems.HarmonyPAS", "SSMG", -1, MenuItemType.SSMG, "", "", "", "");
            theMenu.addMenuItem(miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS);
            miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminGrid", "Grids", "HPASSystemAdminGeoStructure", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS);
            miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminLevels", "Levels", "HPASSystemAdminGeoStructure", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS);
            miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminOreBody", "Ore Body", "HPASSystemAdminGeoStructure", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS);
            miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminOrgStructure", "Org Structure", "HPASSystemAdmin", "Mineware.Systems.HarmonyPAS", "SSMG", -1, MenuItemType.SSMG, "", "", "", "");
            theMenu.addMenuItem(miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS);
            miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminOrgStructureSections", "Sections", "HPASSystemAdminOrgStructure", "Mineware.Systems.HarmonyPAS", "SSUI", 1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS);
            miQAQC_HPASSystemAdminQAQC_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminQAQC", "QAQC", "HPASSystemAdmin", "Mineware.Systems.HarmonyPAS", "SSMG", -1, MenuItemType.SSMG, "", "", "", "");
            theMenu.addMenuItem(miQAQC_HPASSystemAdminQAQC_MinewareSystemsHarmonyPAS);
            miAMISSetup_HPASSystemAdminQAQCAMISSetup_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminQAQCAMISSetup", "AMIS Setup", "HPASSystemAdminQAQC", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miAMISSetup_HPASSystemAdminQAQCAMISSetup_MinewareSystemsHarmonyPAS);
            miQAQC_HPASSystemAdminQAQCQAQC_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminQAQCQAQC", "QAQC", "HPASSystemAdminQAQC", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miQAQC_HPASSystemAdminQAQCQAQC_MinewareSystemsHarmonyPAS);
            miSystemSettings_HPASSystemAdminSettings_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSettings", "System Settings", "HPASSystemAdmin", "Mineware.Systems.HarmonyPAS", "SSMG", 5, MenuItemType.SSMG, "", "", "", "");
            theMenu.addMenuItem(miSystemSettings_HPASSystemAdminSettings_MinewareSystemsHarmonyPAS);
            miBookingSettings_HPASSystemAdminSettingsBook_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSettingsBook", "Booking Settings", "HPASSystemAdminSettings", "Mineware.Systems.HarmonyPAS", "SSUI", 1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miBookingSettings_HPASSystemAdminSettingsBook_MinewareSystemsHarmonyPAS);
            miDates_HPASSystemAdminSettingsDates_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSettingsDates", "Dates", "HPASSystemAdminSettings", "Mineware.Systems.HarmonyPAS", "SSUI", 3, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miDates_HPASSystemAdminSettingsDates_MinewareSystemsHarmonyPAS);
            miFactors_HPASSystemAdminSettingsFactors_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSettingsFactors", "Factors", "HPASSystemAdminSettings", "Mineware.Systems.HarmonyPAS", "SSUI", 4, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miFactors_HPASSystemAdminSettingsFactors_MinewareSystemsHarmonyPAS);
            miMines_HPASSystemAdminSettingsMines_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSettingsMines", "Mines", "HPASSystemAdminSettings", "Mineware.Systems.HarmonyPAS", "SSUI", 5, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miMines_HPASSystemAdminSettingsMines_MinewareSystemsHarmonyPAS);
            miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodes", "Setup Codes", "HPASSystemAdmin", "Mineware.Systems.HarmonyPAS", "SSMG", -1, MenuItemType.SSMG, "", "", "", "");
            theMenu.addMenuItem(miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS);
            miLockMonth_HPASSystemAdminSetupCodesLockPlan_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodesLockPlan", "Lock Month", "HPASSystemAdminSetupCodes", "Mineware.Systems.HarmonyPAS", "SSUI", 7, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miLockMonth_HPASSystemAdminSetupCodesLockPlan_MinewareSystemsHarmonyPAS);
            miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodesProblems", "Problems", "HPASSystemAdminSetupCodes", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS);
            miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodesTopPanels", "Top Panels Setup", "HPASSystemAdminSetupCodes", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS);
            miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodesTramProblems", "Tramming Problems", "HPASSystemAdminSetupCodes", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS);
            miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplaceAdmin", "Workplace Admin", "HPAS", "Mineware.Systems.HarmonyPAS", "MI", -1, MenuItemType.MI, "", "", "", "");
            theMenu.addMenuItem(miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS);
            miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplaceCodes", "Workplace Codes", "HPASWorkplaceAdmin", "Mineware.Systems.HarmonyPAS", "UI", 2, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS);
            miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplaceProcessCodes", "Process Codes", "HPASWorkplaceAdmin", "Mineware.Systems.HarmonyPAS", "UI", 2, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS);
            miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplaces", "Workplaces", "HPASWorkplaceAdmin", "Mineware.Systems.HarmonyPAS", "UI", 1, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS);
            miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplacesKriging", "Kriging", "HPASWorkplaceAdmin", "Mineware.Systems.HarmonyPAS", "UI", 4, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS);
            miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplacesSampling", "Sampling", "HPASWorkplaceAdmin", "Mineware.Systems.HarmonyPAS", "UI", 3, MenuItemType.UI, "", "", "", "");
            theMenu.addMenuItem(miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS);
            miCurrent_HPPASSystemAdminSettingsCurrent_MinewareSystemsHarmonyPAS.setItemInfo("HPPASSystemAdminSettingsCurrent", "Current", "HPASSystemAdminSettings", "Mineware.Systems.HarmonyPAS", "SSUI", 2, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miCurrent_HPPASSystemAdminSettingsCurrent_MinewareSystemsHarmonyPAS);
            miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS.setItemInfo("HPPASSystemAdminSetupCodesPeg", "Pegs", "HPASSystemAdminSetupCodes", "Mineware.Systems.HarmonyPAS", "SSUI", 2, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS);
            miLockPlanning_HPASLockPlannning_MinewareSystemsHarmonyPAS.setItemInfo("HPASLockPlannning", "Lock Planning", "HPASPlanning", "Mineware.Systems.HarmonyPAS", "MI", 3, MenuItemType.MI, "", "", "", "");
            theMenu.addMenuItem(miLockPlanning_HPASLockPlannning_MinewareSystemsHarmonyPAS);
        }
        public void Dispose()
        {
            if (miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS != null)
            {
                miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS.Dispose();
                miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS = null;
            }
            if (miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS != null)
            {
                miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS.Dispose();
                miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS = null;
            }
            if (miBookings_HPASBooking_MinewareSystemsHarmonyPAS != null)
            {
                miBookings_HPASBooking_MinewareSystemsHarmonyPAS.Dispose();
                miBookings_HPASBooking_MinewareSystemsHarmonyPAS = null;
            }
            if (miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS != null)
            {
                miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS.Dispose();
                miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS = null;
            }
            if (miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS != null)
            {
                miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS.Dispose();
                miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS = null;
            }
            if (miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS != null)
            {
                miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS.Dispose();
                miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS = null;
            }
            if (miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS != null)
            {
                miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS.Dispose();
                miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS = null;
            }
            if (miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS != null)
            {
                miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS.Dispose();
                miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS = null;
            }
            if (miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS != null)
            {
                miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS.Dispose();
                miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS = null;
            }
            if (miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS != null)
            {
                miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS.Dispose();
                miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS = null;
            }
            if (miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS != null)
            {
                miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS.Dispose();
                miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS = null;
            }
            if (miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS != null)
            {
                miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS.Dispose();
                miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS = null;
            }
            if (miLockPlan_HPASLockPlanningLockPlan_MinewareSystemsHarmonyPAS != null)
            {
                miLockPlan_HPASLockPlanningLockPlan_MinewareSystemsHarmonyPAS.Dispose();
                miLockPlan_HPASLockPlanningLockPlan_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS != null)
            {
                miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS.Dispose();
                miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS = null;
            }
            if (miEditMeters_HPASPlanningAllowEditMeters_MinewareSystemsHarmonyPAS != null)
            {
                miEditMeters_HPASPlanningAllowEditMeters_MinewareSystemsHarmonyPAS.Dispose();
                miEditMeters_HPASPlanningAllowEditMeters_MinewareSystemsHarmonyPAS = null;
            }
            if (miEditQubes_HPASPlanningAllowEditQubes_MinewareSystemsHarmonyPAS != null)
            {
                miEditQubes_HPASPlanningAllowEditQubes_MinewareSystemsHarmonyPAS.Dispose();
                miEditQubes_HPASPlanningAllowEditQubes_MinewareSystemsHarmonyPAS = null;
            }
            if (miEditSQM_HPASPlanningAllowEditSQM_MinewareSystemsHarmonyPAS != null)
            {
                miEditSQM_HPASPlanningAllowEditSQM_MinewareSystemsHarmonyPAS.Dispose();
                miEditSQM_HPASPlanningAllowEditSQM_MinewareSystemsHarmonyPAS = null;
            }
            if (miEditFaceLength_HPASPlanningAllowFaceLengthEdit_MinewareSystemsHarmonyPAS != null)
            {
                miEditFaceLength_HPASPlanningAllowFaceLengthEdit_MinewareSystemsHarmonyPAS.Dispose();
                miEditFaceLength_HPASPlanningAllowFaceLengthEdit_MinewareSystemsHarmonyPAS = null;
            }
            if (miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS != null)
            {
                miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS.Dispose();
                miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS = null;
            }
            if (miCyclePlan_HPASPlanningCycle_MinewareSystemsHarmonyPAS != null)
            {
                miCyclePlan_HPASPlanningCycle_MinewareSystemsHarmonyPAS.Dispose();
                miCyclePlan_HPASPlanningCycle_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanningProtocol_HPASPlanningProtocol_MinewareSystemsHarmonyPAS != null)
            {
                miPlanningProtocol_HPASPlanningProtocol_MinewareSystemsHarmonyPAS.Dispose();
                miPlanningProtocol_HPASPlanningProtocol_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanningProtocolReport_HPASPlanningProtocolReport_MinewareSystemsHarmonyPAS != null)
            {
                miPlanningProtocolReport_HPASPlanningProtocolReport_MinewareSystemsHarmonyPAS.Dispose();
                miPlanningProtocolReport_HPASPlanningProtocolReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanningProtocolTemplates_HPASPlanningProtocolTemplates_MinewareSystemsHarmonyPAS != null)
            {
                miPlanningProtocolTemplates_HPASPlanningProtocolTemplates_MinewareSystemsHarmonyPAS.Dispose();
                miPlanningProtocolTemplates_HPASPlanningProtocolTemplates_MinewareSystemsHarmonyPAS = null;
            }
            if (miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS != null)
            {
                miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS.Dispose();
                miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanWorkplaces_HPASPlanningWorkplace_MinewareSystemsHarmonyPAS != null)
            {
                miPlanWorkplaces_HPASPlanningWorkplace_MinewareSystemsHarmonyPAS.Dispose();
                miPlanWorkplaces_HPASPlanningWorkplace_MinewareSystemsHarmonyPAS = null;
            }
            if (miAddWorkplace_HPASPlanningWPAddWP_MinewareSystemsHarmonyPAS != null)
            {
                miAddWorkplace_HPASPlanningWPAddWP_MinewareSystemsHarmonyPAS.Dispose();
                miAddWorkplace_HPASPlanningWPAddWP_MinewareSystemsHarmonyPAS = null;
            }
            if (miApproveWorkplace_HPASPlanningWPApproveWP_MinewareSystemsHarmonyPAS != null)
            {
                miApproveWorkplace_HPASPlanningWPApproveWP_MinewareSystemsHarmonyPAS.Dispose();
                miApproveWorkplace_HPASPlanningWPApproveWP_MinewareSystemsHarmonyPAS = null;
            }
            if (miDeleteWorkplace_HPASPlanningWPDelWP_MinewareSystemsHarmonyPAS != null)
            {
                miDeleteWorkplace_HPASPlanningWPDelWP_MinewareSystemsHarmonyPAS.Dispose();
                miDeleteWorkplace_HPASPlanningWPDelWP_MinewareSystemsHarmonyPAS = null;
            }
            if (miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS != null)
            {
                miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS.Dispose();
                miAllowImportPreviousMonth_HPASPlanningWPImportPreviousMonth_MinewareSystemsHarmonyPAS = null;
            }
            if (miImportCADSMineworkplace_HPASPlanningWPImpWP_MinewareSystemsHarmonyPAS != null)
            {
                miImportCADSMineworkplace_HPASPlanningWPImpWP_MinewareSystemsHarmonyPAS.Dispose();
                miImportCADSMineworkplace_HPASPlanningWPImpWP_MinewareSystemsHarmonyPAS = null;
            }
            if (miReplaceWorkplace_HPASPlanningWPReplace_MinewareSystemsHarmonyPAS != null)
            {
                miReplaceWorkplace_HPASPlanningWPReplace_MinewareSystemsHarmonyPAS.Dispose();
                miReplaceWorkplace_HPASPlanningWPReplace_MinewareSystemsHarmonyPAS = null;
            }
            if (miUnApproveWorkplace_HPASPlanningWPUnAppWP_MinewareSystemsHarmonyPAS != null)
            {
                miUnApproveWorkplace_HPASPlanningWPUnAppWP_MinewareSystemsHarmonyPAS.Dispose();
                miUnApproveWorkplace_HPASPlanningWPUnAppWP_MinewareSystemsHarmonyPAS = null;
            }
            if (miDeletePlanningProtocolTemplate_HPASPlanProtDelete_MinewareSystemsHarmonyPAS != null)
            {
                miDeletePlanningProtocolTemplate_HPASPlanProtDelete_MinewareSystemsHarmonyPAS.Dispose();
                miDeletePlanningProtocolTemplate_HPASPlanProtDelete_MinewareSystemsHarmonyPAS = null;
            }
            if (miEditPlanningProtocolTemplate_HPASPlanProtEdit_MinewareSystemsHarmonyPAS != null)
            {
                miEditPlanningProtocolTemplate_HPASPlanProtEdit_MinewareSystemsHarmonyPAS.Dispose();
                miEditPlanningProtocolTemplate_HPASPlanProtEdit_MinewareSystemsHarmonyPAS = null;
            }
            if (miNewPlanningProtocolTemplate_HPASPlanProtNew_MinewareSystemsHarmonyPAS != null)
            {
                miNewPlanningProtocolTemplate_HPASPlanProtNew_MinewareSystemsHarmonyPAS.Dispose();
                miNewPlanningProtocolTemplate_HPASPlanProtNew_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanningProtocolTemplateAccess_HPASPlanProtTempAccess_MinewareSystemsHarmonyPAS != null)
            {
                miPlanningProtocolTemplateAccess_HPASPlanProtTempAccess_MinewareSystemsHarmonyPAS.Dispose();
                miPlanningProtocolTemplateAccess_HPASPlanProtTempAccess_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanningProtocolTemplateSecurity_HPASPlanProtTempSecurity_MinewareSystemsHarmonyPAS != null)
            {
                miPlanningProtocolTemplateSecurity_HPASPlanProtTempSecurity_MinewareSystemsHarmonyPAS.Dispose();
                miPlanningProtocolTemplateSecurity_HPASPlanProtTempSecurity_MinewareSystemsHarmonyPAS = null;
            }
            if (miUnlockPlanningProtcol_HPASPlanProtUnlock_MinewareSystemsHarmonyPAS != null)
            {
                miUnlockPlanningProtcol_HPASPlanProtUnlock_MinewareSystemsHarmonyPAS.Dispose();
                miUnlockPlanningProtcol_HPASPlanProtUnlock_MinewareSystemsHarmonyPAS = null;
            }
            if (miBonusReport_HPASReportBonus_MinewareSystemsHarmonyPAS != null)
            {
                miBonusReport_HPASReportBonus_MinewareSystemsHarmonyPAS.Dispose();
                miBonusReport_HPASReportBonus_MinewareSystemsHarmonyPAS = null;
            }
            if (miCourseBlankReport_HPASReportCourseBlank_MinewareSystemsHarmonyPAS != null)
            {
                miCourseBlankReport_HPASReportCourseBlank_MinewareSystemsHarmonyPAS.Dispose();
                miCourseBlankReport_HPASReportCourseBlank_MinewareSystemsHarmonyPAS = null;
            }

            if (miStandardSRMReport_HPASReportStandardCRM_MinewareSystemsHarmonyPAS != null)
            {
                miStandardSRMReport_HPASReportStandardCRM_MinewareSystemsHarmonyPAS.Dispose();
                miStandardSRMReport_HPASReportStandardCRM_MinewareSystemsHarmonyPAS = null;
            }
            if (miCrewRanking_HPASReportCrewRanking_MinewareSystemsHarmonyPAS != null)
            {
                miCrewRanking_HPASReportCrewRanking_MinewareSystemsHarmonyPAS.Dispose();
                miCrewRanking_HPASReportCrewRanking_MinewareSystemsHarmonyPAS = null;
            }
            if (miDailyBlast_HPASReportDailyBlast_MinewareSystemsHarmonyPAS != null)
            {
                miDailyBlast_HPASReportDailyBlast_MinewareSystemsHarmonyPAS.Dispose();
                miDailyBlast_HPASReportDailyBlast_MinewareSystemsHarmonyPAS = null;
            }
            if (miGenericReport_HPASReportGenericReport_MinewareSystemsHarmonyPAS != null)
            {
                miGenericReport_HPASReportGenericReport_MinewareSystemsHarmonyPAS.Dispose();
                miGenericReport_HPASReportGenericReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miLostBlastReport_HPASReportLostBlastReport_MinewareSystemsHarmonyPAS != null)
            {
                miLostBlastReport_HPASReportLostBlastReport_MinewareSystemsHarmonyPAS.Dispose();
                miLostBlastReport_HPASReportLostBlastReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miMODaily_HPASReportMODaily_MinewareSystemsHarmonyPAS != null)
            {
                miMODaily_HPASReportMODaily_MinewareSystemsHarmonyPAS.Dispose();
                miMODaily_HPASReportMODaily_MinewareSystemsHarmonyPAS = null;
            }
            if (miProblemAnalysis_HPASReportProblemAnalysis_MinewareSystemsHarmonyPAS != null)
            {
                miProblemAnalysis_HPASReportProblemAnalysis_MinewareSystemsHarmonyPAS.Dispose();
                miProblemAnalysis_HPASReportProblemAnalysis_MinewareSystemsHarmonyPAS = null;
            }
            if (miProductionAnalysis_HPASReportProductionAnalysis_MinewareSystemsHarmonyPAS != null)
            {
                miProductionAnalysis_HPASReportProductionAnalysis_MinewareSystemsHarmonyPAS.Dispose();
                miProductionAnalysis_HPASReportProductionAnalysis_MinewareSystemsHarmonyPAS = null;
            }
            if (miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS != null)
            {
                miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS.Dispose();
                miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS = null;
            }
            if (miGradeReport_HPASReportsGradeReport_MinewareSystemsHarmonyPAS != null)
            {
                miGradeReport_HPASReportsGradeReport_MinewareSystemsHarmonyPAS.Dispose();
                miGradeReport_HPASReportsGradeReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miSICReport_HPASReportSICReport_MinewareSystemsHarmonyPAS != null)
            {
                miSICReport_HPASReportSICReport_MinewareSystemsHarmonyPAS.Dispose();
                miSICReport_HPASReportSICReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miMeasuringList_HPASReportsMeasuringList_MinewareSystemsHarmonyPAS != null)
            {
                miMeasuringList_HPASReportsMeasuringList_MinewareSystemsHarmonyPAS.Dispose();
                miMeasuringList_HPASReportsMeasuringList_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS != null)
            {
                miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS.Dispose();
                miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS = null;
            }

            if (miPlathondWallChartReport_HPASReportsPlathondWallChartReport_MinewareSystemsHarmonyPAS != null)
            {
                miPlathondWallChartReport_HPASReportsPlathondWallChartReport_MinewareSystemsHarmonyPAS.Dispose();
                miPlathondWallChartReport_HPASReportsPlathondWallChartReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanningReport_HPASReportsPlanningReport_MinewareSystemsHarmonyPAS != null)
            {
                miPlanningReport_HPASReportsPlanningReport_MinewareSystemsHarmonyPAS.Dispose();
                miPlanningReport_HPASReportsPlanningReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miPlanVsBook_HPASReportsPlanVsBook_MinewareSystemsHarmonyPAS != null)
            {
                miPlanVsBook_HPASReportsPlanVsBook_MinewareSystemsHarmonyPAS.Dispose();
                miPlanVsBook_HPASReportsPlanVsBook_MinewareSystemsHarmonyPAS = null;
            }
            if (miSurveyReport_HPASReportSurveyReport_MinewareSystemsHarmonyPAS != null)
            {
                miSurveyReport_HPASReportSurveyReport_MinewareSystemsHarmonyPAS.Dispose();
                miSurveyReport_HPASReportSurveyReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miTonnage_HPASReportTonnage_MinewareSystemsHarmonyPAS != null)
            {
                miTonnage_HPASReportTonnage_MinewareSystemsHarmonyPAS.Dispose();
                miTonnage_HPASReportTonnage_MinewareSystemsHarmonyPAS = null;
            }
            if (miTopPanels_HPASReportTopPanels_MinewareSystemsHarmonyPAS != null)
            {
                miTopPanels_HPASReportTopPanels_MinewareSystemsHarmonyPAS.Dispose();
                miTopPanels_HPASReportTopPanels_MinewareSystemsHarmonyPAS = null;
            }
            if (miRevisedPlanningAuditReport_HPASRevisedAuditReport_MinewareSystemsHarmonyPAS != null)
            {
                miRevisedPlanningAuditReport_HPASRevisedAuditReport_MinewareSystemsHarmonyPAS.Dispose();
                miRevisedPlanningAuditReport_HPASRevisedAuditReport_MinewareSystemsHarmonyPAS = null;
            }
            if (miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS != null)
            {
                miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS.Dispose();
                miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS = null;
            }
            if (miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS != null)
            {
                miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS.Dispose();
                miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS = null;
            }
            if (miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS != null)
            {
                miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS.Dispose();
                miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS = null;
            }
            if (miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS != null)
            {
                miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS.Dispose();
                miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS = null;
            }
            if (miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS != null)
            {
                miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS.Dispose();
                miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS = null;
            }
            if (miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS != null)
            {
                miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS.Dispose();
                miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS = null;
            }
            if (miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS != null)
            {
                miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS.Dispose();
                miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS = null;
            }
            if (miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS != null)
            {
                miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS.Dispose();
                miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS = null;
            }
            if (miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS != null)
            {
                miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS.Dispose();
                miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS = null;
            }
            if (miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS != null)
            {
                miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS.Dispose();
                miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS = null;
            }
            if (miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS != null)
            {
                miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS.Dispose();
                miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS = null;
            }
            if (miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS != null)
            {
                miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS.Dispose();
                miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS = null;
            }
            if (miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS != null)
            {
                miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS.Dispose();
                miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS = null;
            }
            if (miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS != null)
            {
                miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS.Dispose();
                miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS = null;
            }
            if (miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS != null)
            {
                miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS.Dispose();
                miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS = null;
            }
            if (miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS != null)
            {
                miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS.Dispose();
                miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS = null;
            }
            if (miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS != null)
            {
                miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS.Dispose();
                miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS = null;
            }
            if (miQAQC_HPASSystemAdminQAQC_MinewareSystemsHarmonyPAS != null)
            {
                miQAQC_HPASSystemAdminQAQC_MinewareSystemsHarmonyPAS.Dispose();
                miQAQC_HPASSystemAdminQAQC_MinewareSystemsHarmonyPAS = null;
            }
            if (miAMISSetup_HPASSystemAdminQAQCAMISSetup_MinewareSystemsHarmonyPAS != null)
            {
                miAMISSetup_HPASSystemAdminQAQCAMISSetup_MinewareSystemsHarmonyPAS.Dispose();
                miAMISSetup_HPASSystemAdminQAQCAMISSetup_MinewareSystemsHarmonyPAS = null;
            }
            if (miQAQC_HPASSystemAdminQAQCQAQC_MinewareSystemsHarmonyPAS != null)
            {
                miQAQC_HPASSystemAdminQAQCQAQC_MinewareSystemsHarmonyPAS.Dispose();
                miQAQC_HPASSystemAdminQAQCQAQC_MinewareSystemsHarmonyPAS = null;
            }
            if (miSystemSettings_HPASSystemAdminSettings_MinewareSystemsHarmonyPAS != null)
            {
                miSystemSettings_HPASSystemAdminSettings_MinewareSystemsHarmonyPAS.Dispose();
                miSystemSettings_HPASSystemAdminSettings_MinewareSystemsHarmonyPAS = null;
            }
            if (miBookingSettings_HPASSystemAdminSettingsBook_MinewareSystemsHarmonyPAS != null)
            {
                miBookingSettings_HPASSystemAdminSettingsBook_MinewareSystemsHarmonyPAS.Dispose();
                miBookingSettings_HPASSystemAdminSettingsBook_MinewareSystemsHarmonyPAS = null;
            }
            if (miDates_HPASSystemAdminSettingsDates_MinewareSystemsHarmonyPAS != null)
            {
                miDates_HPASSystemAdminSettingsDates_MinewareSystemsHarmonyPAS.Dispose();
                miDates_HPASSystemAdminSettingsDates_MinewareSystemsHarmonyPAS = null;
            }
            if (miFactors_HPASSystemAdminSettingsFactors_MinewareSystemsHarmonyPAS != null)
            {
                miFactors_HPASSystemAdminSettingsFactors_MinewareSystemsHarmonyPAS.Dispose();
                miFactors_HPASSystemAdminSettingsFactors_MinewareSystemsHarmonyPAS = null;
            }
            if (miMines_HPASSystemAdminSettingsMines_MinewareSystemsHarmonyPAS != null)
            {
                miMines_HPASSystemAdminSettingsMines_MinewareSystemsHarmonyPAS.Dispose();
                miMines_HPASSystemAdminSettingsMines_MinewareSystemsHarmonyPAS = null;
            }
            if (miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS != null)
            {
                miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS.Dispose();
                miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS = null;
            }
            if (miLockMonth_HPASSystemAdminSetupCodesLockPlan_MinewareSystemsHarmonyPAS != null)
            {
                miLockMonth_HPASSystemAdminSetupCodesLockPlan_MinewareSystemsHarmonyPAS.Dispose();
                miLockMonth_HPASSystemAdminSetupCodesLockPlan_MinewareSystemsHarmonyPAS = null;
            }
            if (miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS != null)
            {
                miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS.Dispose();
                miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS = null;
            }
            if (miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS != null)
            {
                miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS.Dispose();
                miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS = null;
            }
            if (miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS != null)
            {
                miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS.Dispose();
                miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS = null;
            }
            if (miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS != null)
            {
                miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS.Dispose();
                miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS = null;
            }
            if (miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS != null)
            {
                miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS.Dispose();
                miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS = null;
            }
            if (miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS != null)
            {
                miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS.Dispose();
                miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS = null;
            }
            if (miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS != null)
            {
                miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS.Dispose();
                miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS = null;
            }
            if (miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS != null)
            {
                miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS.Dispose();
                miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS = null;
            }
            if (miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS != null)
            {
                miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS.Dispose();
                miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS = null;
            }
            if (miCurrent_HPPASSystemAdminSettingsCurrent_MinewareSystemsHarmonyPAS != null)
            {
                miCurrent_HPPASSystemAdminSettingsCurrent_MinewareSystemsHarmonyPAS.Dispose();
                miCurrent_HPPASSystemAdminSettingsCurrent_MinewareSystemsHarmonyPAS = null;
            }
            if (miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS != null)
            {
                miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS.Dispose();
                miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS = null;
            }
            if (miLockPlanning_HPASLockPlannning_MinewareSystemsHarmonyPAS != null)
            {
                miLockPlanning_HPASLockPlannning_MinewareSystemsHarmonyPAS.Dispose();
                miLockPlanning_HPASLockPlannning_MinewareSystemsHarmonyPAS = null;
            }
        }
    }
}
