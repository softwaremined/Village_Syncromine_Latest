using System;
using System.Collections.Generic;
using Mineware.Menu.Structure;
using Mineware.Systems.Global;

namespace Mineware.Systems.HarmonyPASGlobal
{
    public class HarmonyPasMenuStructure
    {
        public mainMenu theMenu = new mainMenu();
        public menuItem miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miBookings_HPASBooking_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS = new menuItem();
      public menuItem miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCalendars_HPASSystemAdminCalendarsCalendars_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miCalendarType_HPASCalendarType_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miMillCalendar_HPASMillCalendar_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miSectionCalendar_HPASSectionCalendar_MinewareSystemsHarmonyPAS = new menuItem();
        public menuItem miProblemSetup_HPASProblemSetup_MinewareSystemsHarmonyPAS = new menuItem();

        public void setMenuItems()
        {
            theMenu.MenuItems.Clear();
      miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS.setItemInfo("HPAS", "Harmony PAS", "Mineware.Systems.HarmonyPAS","Mineware.Systems.HarmonyPAS" ,"SI",-1,MenuItemType.SI,"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");
            theMenu.addMenuItem(miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS);
      miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS.setItemInfo("HPASReports", "Harmony Reports", "HPAS","Mineware.Systems.HarmonyPAS" ,"RMI",4,MenuItemType.RMI,"","","","");
      theMenu.addMenuItem(miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS);
      miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanning", "Planning", "HPAS","Mineware.Systems.HarmonyPAS" ,"MI",1,MenuItemType.MI,"","","","");
            theMenu.addMenuItem(miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS);
      miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdmin", "Production System Administration", "HPAS","Mineware.Systems.HarmonyPAS" ,"SSMI",5,MenuItemType.SSMI,"","","","");
      theMenu.addMenuItem(miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS);
      miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendars", "Calendars", "HPASSystemAdmin","Mineware.Systems.HarmonyPAS" ,"SSMG",-1,MenuItemType.SSMG,"","","","");
      theMenu.addMenuItem(miCalendars_HPASSystemAdminCalendars_MinewareSystemsHarmonyPAS);
      miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendarsCaltype", "Calendar Types", "HPASSystemAdminCalendars","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS);
      miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendarsSectionCalendar", "Section Calendar", "HPASSystemAdminCalendars","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS);
      miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminOrgStructure", "Org Structure", "HPASSystemAdmin","Mineware.Systems.HarmonyPAS" ,"SSMG",-1,MenuItemType.SSMG,"","","","");
      theMenu.addMenuItem(miOrgStructure_HPASSystemAdminOrgStructure_MinewareSystemsHarmonyPAS);
      miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminOrgStructureSections", "Sections", "HPASSystemAdminOrgStructure","Mineware.Systems.HarmonyPAS" ,"SSUI",1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS);
      miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplaceAdmin", "Workplace Admin", "HPASPlanning","Mineware.Systems.HarmonyPAS" ,"MI",-1,MenuItemType.MI,"","","","");
      theMenu.addMenuItem(miWorkplaceAdmin_HPASWorkplaceAdmin_MinewareSystemsHarmonyPAS);
      miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplaceCodes", "Workplace Codes", "HPASWorkplaceAdmin","Mineware.Systems.HarmonyPAS" ,"UI",2,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS);
      miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplaces", "Workplaces", "HPASWorkplaceAdmin","Mineware.Systems.HarmonyPAS" ,"UI",1,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS);
      miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminGeoStructure", "Geo Structure", "HPASSystemAdmin","Mineware.Systems.HarmonyPAS" ,"SSMG",-1,MenuItemType.SSMG,"","","","");
      theMenu.addMenuItem(miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS);
      miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminEndtypes", "Endtypes", "HPASSystemAdminGeoStructure","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS);
      miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminLevels", "Levels", "HPASSystemAdminGeoStructure","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS);
      miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminGrid", "Grids", "HPASSystemAdminGeoStructure","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS);
      miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminBoxholes", "Boxholes", "HPASSystemAdminGeoStructure","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS);
      miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminOreBody", "Ore Body", "HPASSystemAdminGeoStructure","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS);
      miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendarsMillCalendar", "Other Calendars", "HPASSystemAdminCalendars","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS);
      miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS.setItemInfo("HPAS SystemAdminOrgStructureCrews", "Crews", "HPASSystemAdminOrgStructure","Mineware.Systems.HarmonyPAS" ,"SSUI",2,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS);
      miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplaceProcessCodes", "Process Codes", "HPASWorkplaceAdmin","Mineware.Systems.HarmonyPAS" ,"UI",2,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS);
      miBookings_HPASBooking_MinewareSystemsHarmonyPAS.setItemInfo("HPASBooking", "Bookings", "HPAS","Mineware.Systems.HarmonyPAS" ,"MI",2,MenuItemType.MI,"","","","");
      theMenu.addMenuItem(miBookings_HPASBooking_MinewareSystemsHarmonyPAS);
      miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingTramming", "Tramming Booking", "HPASBooking","Mineware.Systems.HarmonyPAS" ,"UI",2,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS);
      miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingMilling", "Milling Booking", "HPASBooking","Mineware.Systems.HarmonyPAS" ,"UI",4,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS);
      miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingDailyBookings", "Daily Bookings", "HPASBooking","Mineware.Systems.HarmonyPAS" ,"UI",1,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS);
      miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingSIC", "SIC Capture", "HPASBooking","Mineware.Systems.HarmonyPAS" ,"UI",5,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS);
      miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningBusinessPlan", "Business Plan Import", "HPASPlanning","Mineware.Systems.HarmonyPAS" ,"UI",1,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS);
      miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplacesSampling", "Sampling", "HPASWorkplaceAdmin","Mineware.Systems.HarmonyPAS" ,"UI",3,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS);
      miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS.setItemInfo("HPPASSystemAdminSetupCodesPeg", "Pegs", "HPASSystemAdminSetupCodes","Mineware.Systems.HarmonyPAS" ,"SSUI",2,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS);
      miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS.setItemInfo("HPASWorkplacesKriging", "Kriging", "HPASWorkplaceAdmin","Mineware.Systems.HarmonyPAS" ,"UI",4,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS);
      miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS.setItemInfo("HPASSurvey", "Survey", "HPAS","Mineware.Systems.HarmonyPAS" ,"MI",3,MenuItemType.MI,"","","","");
      theMenu.addMenuItem(miSurvey_HPASSurvey_MinewareSystemsHarmonyPAS);
      miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS.setItemInfo("HPASSurveyCapture", "Survey Capture", "HPASSurvey","Mineware.Systems.HarmonyPAS" ,"UI",-1,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS);
      miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingsOreflowStorages", "Oreflow Storages", "HPASBooking","Mineware.Systems.HarmonyPAS" ,"UI",-1,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS);
      miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodesTramProblems", "Tramming Problems", "HPASSystemAdminSetupCodes","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS);
      miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodesTopPanels", "Top Panels Setup", "HPASSystemAdminSetupCodes","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS);
      miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingBonusCapture", "Bonus Capture", "HPASBooking","Mineware.Systems.HarmonyPAS" ,"UI",6,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS);
      miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS.setItemInfo("HPASBookingHoisting", "Hoisting Booking", "HPASBooking","Mineware.Systems.HarmonyPAS" ,"UI",3,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS);
      miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS.setItemInfo("HPASPlanningTramming", "Tramming PLanning", "HPASPlanning","Mineware.Systems.HarmonyPAS" ,"UI",2,MenuItemType.UI,"","","","");
      theMenu.addMenuItem(miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS);
      miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodes", "Setup Codes", "HPASSystemAdmin","Mineware.Systems.HarmonyPAS" ,"SSMG",-1,MenuItemType.SSMG,"","","","");
      theMenu.addMenuItem(miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS);
      miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminSetupCodesProblems", "Problems", "HPASSystemAdminSetupCodes","Mineware.Systems.HarmonyPAS" ,"SSUI",-1,MenuItemType.SSUI,"","","","");
      theMenu.addMenuItem(miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS);
            miCalendars_HPASSystemAdminCalendarsCalendars_MinewareSystemsHarmonyPAS.setItemInfo("HPASSystemAdminCalendarsCalendars", "Calendars", "HPASSystemAdminCalendars", "Mineware.Systems.HarmonyPAS", "SSUI", -1, MenuItemType.SSUI, "", "", "", "");
            theMenu.addMenuItem(miCalendars_HPASSystemAdminCalendarsCalendars_MinewareSystemsHarmonyPAS);
        }
        public void Dispose()
        {
            if (miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS != null)
            {
                miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS.Dispose();
                miHarmonyPAS_HPAS_MinewareSystemsHarmonyPAS = null;
            }
       if (miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS != null)
        {
            miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS.Dispose();
            miHarmonyReports_HPASReports_MinewareSystemsHarmonyPAS = null;
        }
            if (miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS != null)
            {
                miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS.Dispose();
                miPlanning_HPASPlanning_MinewareSystemsHarmonyPAS = null;
            }
       if (miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS != null)
        {
            miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS.Dispose();
            miProductionSystemAdministration_HPASSystemAdmin_MinewareSystemsHarmonyPAS = null;
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
       if (miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS != null)
        {
            miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS.Dispose();
            miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS = null;
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
       if (miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS != null)
        {
            miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS.Dispose();
            miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS = null;
        }
       if (miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS != null)
        {
            miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS.Dispose();
            miGeoStructure_HPASSystemAdminGeoStructure_MinewareSystemsHarmonyPAS = null;
        }
       if (miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS != null)
        {
            miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS.Dispose();
            miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS = null;
        }
       if (miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS != null)
        {
            miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS.Dispose();
            miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS = null;
        }
       if (miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS != null)
        {
            miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS.Dispose();
            miGrids_HPASSystemAdminGrid_MinewareSystemsHarmonyPAS = null;
        }
       if (miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS != null)
        {
            miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS.Dispose();
            miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS = null;
        }
       if (miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS != null)
        {
            miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS.Dispose();
            miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS = null;
        }
       if (miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS != null)
        {
            miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS.Dispose();
            miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS = null;
        }
       if (miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS != null)
        {
            miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS.Dispose();
            miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS = null;
        }
       if (miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS != null)
        {
            miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS.Dispose();
            miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS = null;
        }
       if (miBookings_HPASBooking_MinewareSystemsHarmonyPAS != null)
        {
            miBookings_HPASBooking_MinewareSystemsHarmonyPAS.Dispose();
            miBookings_HPASBooking_MinewareSystemsHarmonyPAS = null;
        }
       if (miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS != null)
        {
            miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS.Dispose();
            miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS = null;
        }
       if (miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS != null)
        {
            miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS.Dispose();
            miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS = null;
        }
       if (miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS != null)
        {
            miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS.Dispose();
            miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS = null;
        }
       if (miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS != null)
        {
            miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS.Dispose();
            miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS = null;
        }
       if (miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS != null)
        {
            miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS.Dispose();
            miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS = null;
        }
       if (miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS != null)
        {
            miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS.Dispose();
            miSampling_HPASWorkplacesSampling_MinewareSystemsHarmonyPAS = null;
        }
       if (miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS != null)
        {
            miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS.Dispose();
            miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS = null;
        }
       if (miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS != null)
        {
            miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS.Dispose();
            miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS = null;
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
       if (miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS != null)
        {
            miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS.Dispose();
            miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS = null;
        }
       if (miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS != null)
        {
            miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS.Dispose();
            miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS = null;
        }
       if (miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS != null)
        {
            miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS.Dispose();
            miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS = null;
        }
       if (miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS != null)
        {
            miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS.Dispose();
            miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS = null;
        }
       if (miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS != null)
            {
            miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS.Dispose();
            miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS = null;
            }
       if (miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS != null)
            {
            miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS.Dispose();
            miTrammingPLanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS = null;
            }
       if (miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS != null)
            {
            miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS.Dispose();
            miSetupCodes_HPASSystemAdminSetupCodes_MinewareSystemsHarmonyPAS = null;
            }
       if (miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS != null)
            {
            miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS.Dispose();
            miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS = null;
            }

            if (miCalendars_HPASSystemAdminCalendarsCalendars_MinewareSystemsHarmonyPAS != null)
            {
                miCalendars_HPASSystemAdminCalendarsCalendars_MinewareSystemsHarmonyPAS.Dispose();
                miCalendars_HPASSystemAdminCalendarsCalendars_MinewareSystemsHarmonyPAS = null;
            }

            if (miProblemSetup_HPASProblemSetup_MinewareSystemsHarmonyPAS != null)
            {
                miProblemSetup_HPASProblemSetup_MinewareSystemsHarmonyPAS.Dispose();
                miProblemSetup_HPASProblemSetup_MinewareSystemsHarmonyPAS = null;
            }
        }
    }
}
