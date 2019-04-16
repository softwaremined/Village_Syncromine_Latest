
using System.Collections.Generic;
using System.Linq;
using Mineware.Menu.Structure;
using DevExpress.XtraEditors;
using System.Drawing;
using Mineware.Systems.ProductionGlobal;
using Mineware.Systems.Reports;
using Mineware.Systems.GlobalConnect;
using Mineware.Plugin.Interface;
using Mineware.Systems.Global;
using Mineware.Systems.Global.ReportsControls;
using System;

namespace Mineware.Systems.Production 
{
    class HarmonyPAS : PluginInterface
    {
        public string SystemTag
        {
            get
            {
                return resHarmonyPAS.systemTag;
            }
        }

        public string SystemDBTag
        {
            get
            {
                return resHarmonyPAS.systemDBTag;

            }
        }
        static void Main(string[] args)
        {
        }

        public global::DevExpress.XtraNavBar.NavBarItem getApplicationSettingsNavBarItem()
        {
            return null;
        }

        public ucBaseUserControl getApplicationSettingsScreen()
        {
            return null;
        }

        public ucBaseUserControl getMainMenuAdditionalItem()
        {
            //ucBaseUserControl TheResult = new ucBaseUserControl();
            return null;
        }

        public TileItem getMainMenuItem()
        {
            int theLevel = TUserInfo.theSecurityLevel(TProductionGlobal.HarmonyPasMenuStructure.miPAS_HPAS_MinewareSystemsHarmonyPAS.ItemID);
            if (theLevel > 0)
            {
                var theResult = new TileItem();
                var imageServices = resHarmonyPAS.ProductionStart48x48;
                theResult.Text = "<size=20>PAS</size>";
                theResult.TextAlignment = TileItemContentAlignment.TopLeft;
                theResult.AppearanceItem.Normal.BackColor = Color.FromArgb(255,0,0);
                theResult.AppearanceItem.Normal.BackColor2 = Color.FromArgb(255, 0, 0);
                theResult.AppearanceItem.Normal.BorderColor = Color.Transparent;
                theResult.ImageAlignment = TileItemContentAlignment.BottomRight;
                theResult.Image = imageServices;
                theResult.Tag = resHarmonyPAS.systemTag;
                theResult.ItemSize = TileItemSize.Wide;
                return theResult;
            }
            else
            {
                return null;
            }
        }

        public ucBaseUserControl getMenuItem(string itemID)
        {
            ucBaseUserControl theResult = new ucBaseUserControl();

            if (TProductionGlobal.HarmonyPasMenuStructure.miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SectionScreen.ucSectionScreen();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miBookingSettings_HPASSystemAdminSettingsBook_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsBookings();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miCurrent_HPPASSystemAdminSettingsCurrent_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsCurrent();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miDates_HPASSystemAdminSettingsDates_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsDates();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miFactors_HPASSystemAdminSettingsFactors_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsFactors();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miMines_HPASSystemAdminSettingsMines_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsMines();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Pegs.ucPegs();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.Survey.ucSurvey();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.BookingsABS.ucBookingsABS();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.SICCapture.ucSICCapture();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.Kriging.ucKriging();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.CalendarTypes.ucCalendarTypes();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SectionCalendar.ucSectionCalendar();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TrammingBoxholes.ucTrammingBoxholes();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Workplace_Codes.ucWorkplace_Codes();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miSampling_HPASDepCaptSampling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SamplingCapture.ucSamplingCapture();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Workplace_Codes.ucWorkplace_Codes();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Workplaces.ucWorkplaces();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.OreBody.ucOreBody();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.EndTypes.ucEndTypes();
               // theResult = new SysAdminScreens.Users.ucUserProductionSettings();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.GangNo.ucGangNoScreen();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.ProcessCodes.ucProcessCodes();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.ProblemSetup.ucProblemSetup();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Level.ucLevel();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Calendars.ucCalendars();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.ProblemSetup.ucProblemSetup();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miTrammingPlanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TrammingPlanning.ucTrammingPlanning();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TrammingBooking.ucTrammingBooking();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.MillingBooking.ucMillingBooking();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.HoistingBooking.ucHoistingBooking();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TrammingProblems.ucTrammingProblems();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.miPlanWorkplaces_HPASPlanningWorkplace_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PrePlanning.ucPrePlanningMain(false);
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PrePlanning.ucPrePlanningMain(true);
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PrePlanning.RevisedPlanning_Security.ucRevisedPlanningSecurity();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miPlanningProtocolTemplates_HPASPlanningProtocolTemplates_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PlanningProtocolTemplates.ucPlanProtTemplates();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PlanningProtocolCapture.ucPlanProtCapture();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.OreflowStorages.ucOreflowStorages();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.BonusCapture.ucBonusCapture();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miLockMonth_HPASSystemAdminSetupCodesLockPlan_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.LockMonth.ucLockMonth();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TopPanels.ucTopPanels();
            }

           

            //if (TProductionGlobal.HarmonyPasMenuStructure.miAMISSetup_HPASSystemAdminQAQCAMISSetup_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    theResult = new SysAdminScreens.AMISSetup.ucAMISSetup();
            //}

            //if (TProductionGlobal.HarmonyPasMenuStructure.miQAQC_HPASSystemAdminQAQCQAQC_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    theResult = new SysAdminScreens.QAQC.ucQAQC();
            //}

            if (TProductionGlobal.HarmonyPasMenuStructure.miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.BusinessPlanImport.ucBusinessPlanImport();
            }

            //TODO: GS - Find out why there are 2 items linked to LockPlan. Perhaps just a typo
            if (TProductionGlobal.HarmonyPasMenuStructure.miLockPlanning_HPASLockPlanning_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.AuthLockPlan.ucAuthLockPlan();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.AuthLockPlan.ucAuthLockPlan();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.miSetupCycles_HPASSystemAdminCycles_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SetupCycles.ucSetupCycles();
            }


            ////Departmental Capture

            if (TProductionGlobal.HarmonyPasMenuStructure.miSurvey_HPASDepCaptSurvey_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucSurvey();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miGeology_HPASDepCaptGeology_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucGeology();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miSampling_HPASDepCaptSampling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucSampling();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miVentilation_HPASDepCaptVentilation_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucVentilation();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miWorkNote_HPASDepCaptMinersReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucMinersReport();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.miRockEngineering_HPASDepCaptRockEngineering_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucRockEng();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miSampling_HPASDepCaptSampling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucSampling();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.miVamping_HPASDepCaptVamping_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucVamping();
            }



            //if (TProductionGlobal.HarmonyPasMenuStructure.miOCRScheduling_HPASDepCaptOCRScheduling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    theResult = new SysAdminScreens.DepartmentalCapture.ucMinersNotes();
            //}

            


            ///New
            ///

            if (TProductionGlobal.HarmonyPasMenuStructure.miOCRScheduling_HPASOCRSchedulingMain_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.OCRScheduling.ucOCRSchedulingMain();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.miMONotes_HPASDepCaptOCRScheduling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucMinersNotes();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.miExtendedBreaks_HPASSystemAdminExtBreaks_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucExtendedBreak();
            }


            ////Vamping

            if (TProductionGlobal.HarmonyPasMenuStructure.miVampingPlanning_HPASPlanningVamping_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucVampPlanning();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.miVampingBooking_HPASBookingVamping_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucVampingBooking();
            }

            //Plant Sections

            if (TProductionGlobal.HarmonyPasMenuStructure.miOtherSections_HPASStstemAdminOrgStructureOtherSections_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new SysAdminScreens.SectionScreen.ucOtherSections();
            }

            ///GeoScience
            ///
            if (TProductionGlobal.HarmonyPasMenuStructure.miGeoScience_HPASDepCaptGeoScience_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new SysAdminScreens.DepartmentalCapture.ucGeoScienseMain();
            }


            ///Labor
            ///
            if (TProductionGlobal.HarmonyPasMenuStructure.miEarlyMorningShiftReport_HPASEarlyMorningShiftReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new SysAdminScreens.Labour.ucEarlyMorningReport();
            }


            return theResult;


        }

        public mainMenu getMenuStructure()
        {
            return TProductionGlobal.HarmonyPasMenuStructure.theMenu;
        }

        public List<clsParameters> getParameters()
        {
	        var parameters = new List<clsParameters>();
			var section = TProductionGlobal.UserInfo.FirstOrDefault().ReportSections.FirstOrDefault();
	        parameters.Add(new clsParameters()
	        {
		        ParameterName = "Section Id",
		        Value = section
	        });
	        return parameters;
        }

        public ucReportSettingsControl getReportSettings(string itemID)
        {
            ucReportSettingsControl theResult = new ucReportSettingsControl();

            if (TProductionGlobal.HarmonyPasMenuStructure.miPlanningProtocolReport_HPASPlanningProtocolReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new ucPlanningProtocolReport();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miMeasuringList_HPASReportsMeasuringList_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.MeasuringListReport.ucMeasuringListReport();         
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miPlanVsBook_HPASReportsPlanVsBook_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.PlannedVsBooked.ucPlannedVsBooked();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miGradeReport_HPASReportsGradeReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.GradeReport.ucGradeReport();
            }

            //if (TProductionGlobal.HarmonyPasMenuStructure.miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    return new Reports.PlanningReportNewStyle.ucPlanningReportNewStyle();
            //}

            if (TProductionGlobal.HarmonyPasMenuStructure.miPlathond_HPASReportWallRoom_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.PlathondWallChartReport.PlathondWallChartReportUserControl();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miBonusReport_HPASReportBonus_MinewareSystemsHarmonyPAS .ItemID  == itemID)
            {
                return new Reports.BonusReport.ucBonusReport();
            }

            //if (TProductionGlobal.HarmonyPasMenuStructure.miCourseBlankReport_HPASReportCourseBlank_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    return new Reports.CourseBlankSampleReport.ucCourseBlankSampleReport();
            //}

            //if (TProductionGlobal.HarmonyPasMenuStructure.miStandardSRMReport_HPASReportStandardCRM_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    return new Reports.StandardCRMReport.ucStandardCRMReport();
            //}
            if (TProductionGlobal.HarmonyPasMenuStructure.miTopPanelsReport_HPASReportTopPanels_MinewareSystemsHarmonyPAS .ItemID  == itemID)
            {
                return new Reports.TopPanelsReport.ucTopPanelsReport();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miTonnage_HPASReportTonnage_MinewareSystemsHarmonyPAS.ItemID  == itemID)
            {
                return new Reports.TonnageReport.ucTonnageReport();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miProductionAnalysis_HPASReportProductionAnalysis_MinewareSystemsHarmonyPAS .ItemID  == itemID)
            {
                return new Reports.ProductionAnalysisReport.ucProductionAnalysisReport();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miMODaily_HPASReportMODaily_MinewareSystemsHarmonyPAS.ItemID  == itemID)
            {
                return new Reports.MODaily.ucMODailyProduction();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miCrewRanking_HPASReportCrewRanking_MinewareSystemsHarmonyPAS .ItemID  == itemID)
            {
                return new Reports.CrewRanking.ucCrewRanking();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miProblemAnalysis_HPASReportProblemAnalysis_MinewareSystemsHarmonyPAS .ItemID == itemID)
            {
                return new Reports.Problem_Analysis_Report.ucProblemAnalysisReport();
            }

            //if (TProductionGlobal.HarmonyPasMenuStructure.miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    return new Reports.PlanningReportNewStyle.ucPlanningReportNewStyle();
            //}

            if (TProductionGlobal.HarmonyPasMenuStructure.miSICReport_HPASReportSICReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.SICReport.ucSICReport();
            }
            if (TProductionGlobal.HarmonyPasMenuStructure.miProblemHistoryPareto_HPASReportLostBlastReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.ProblemsReport.ucProblemsReport();
            }

            ///New Graph Problems

            if (TProductionGlobal.HarmonyPasMenuStructure.miProblemHistoryGraph_HPASReportProblemHistGraph_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.ProblemHistoryGraph.ucProblemsReport();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miSurveyReport_HPASReportSurveyReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                 return new Reports.SurveyReport.ucSurveyReport();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miPlanningReport_HPASReportsPlanningReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.PlanningReport.ucPlanningReport();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miGenericReport_HPASReportGenericReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.GenericReport.ucGenericReport();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miRevisedPlanningAuditReport_HPASRevisedAuditReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.RevisedPlanning_Audit_Report.ucRevisedPlanningAuditReport();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miWorstPerformers_HPASReportWorstPerformers_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.WorstPerformers.ucWorstPerformerss();
            }

            ///NEW
            ///

            if (TProductionGlobal.HarmonyPasMenuStructure.miMODailyDetail_HPASReportMODailyDetail_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.MODailyDetail.ucMODailyProduction();
            }


            if (TProductionGlobal.HarmonyPasMenuStructure.mi6ShiftRecon_HPASReport6ShiftRecon_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports._6ShiftRecon.ucRecon();
            }

            if (TProductionGlobal.HarmonyPasMenuStructure.miPlanningReportStyle2_HPASReportsPlanningReport2_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.PlanningReportNewStyle.ucPlanningReportNewStyle();
            }

            return theResult;
            

        }

        public ucBaseUserControl getStartScreen()
        {
            return new SysAdminScreens.Dashboards.ucMainDashboard();
        }

        public string getSystemDBTag()
        {
            return resHarmonyPAS.systemDBTag;
        }

        public string getSystemTag()
        {
            return resHarmonyPAS.systemTag;
        }

        public global::DevExpress.XtraNavBar.NavBarItem getUserSettingsNavBarItem()
        {
            var theResult = new DevExpress.XtraNavBar.NavBarItem() { Caption = "Production", Tag = resHarmonyPAS.systemTag };
            return theResult;
        }

        public ucBaseUserControl getUserSettingsScreen(ScreenStatus _theScreenStatus, string _userID, TUserCurrentInfo userInfo, string theConnection)
        {
            
            SysAdminScreens.Users.ucUserProductionSettings theResult = 
                    new SysAdminScreens.Users.ucUserProductionSettings() { theScreenStatus = _theScreenStatus, UserID = _userID, theSystemDBTag = resHarmonyPAS.systemDBTag, theSystemTag = resHarmonyPAS.systemTag, UserCurrentInfo = userInfo };
            theResult.currentConnection = theConnection;
            return theResult;
        }

        public void InitializeModule()
        {
            TProductionGlobal.HarmonyPasMenuStructure.setMenuItems();
            TProductionGlobal.HarmonyPasMenuStructure.theMenu.systemDBTag = resHarmonyPAS.systemDBTag;
            TProductionGlobal.HarmonyPasMenuStructure.theMenu.systemTag = resHarmonyPAS.systemTag;
        }

        public void LoggedOn()
        {
            // Use this section to load all module settings like user logon info or production info
            //TProductionGlobal.get(resHarmonyPAS .systemDBTag,TUserInfo .Connection );
            TProductionGlobal.SetProductionGlobalInfo(resHarmonyPAS.systemDBTag);
            TProductionGlobal.getSystemSettingsProductioInfo(resHarmonyPAS.systemDBTag);
            TProductionGlobal.SetUserInfo(resHarmonyPAS.systemDBTag);
            //TProductionGlobal.getUserInfo(resHarmonyPAS.systemDBTag);
        }

        //ucReportSettingsControl PluginInterface.getReportSettings(string itemID)
        //{
        //     throw new NotImplementedException();
        //}
    }
}
