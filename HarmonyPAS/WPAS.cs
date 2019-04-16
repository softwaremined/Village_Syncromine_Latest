
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
                return resWPAS.systemTag;
            }
        }

        public string SystemDBTag
        {
            get
            {
                return resWPAS.systemDBTag;

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
            int theLevel = TUserInfo.theSecurityLevel(TProductionGlobal.WPASMenuStructure.miPAS_HPAS_MinewareSystemsHarmonyPAS.ItemID);
            if (theLevel > 0)
            {
                var theResult = new TileItem();
                var imageServices = resWPAS.ProductionStart48x48;
                theResult.Text = "<size=20>PAS</size>";
                theResult.TextAlignment = TileItemContentAlignment.TopLeft;
                theResult.AppearanceItem.Normal.BackColor = Color.FromArgb(255,0,0);
                theResult.AppearanceItem.Normal.BackColor2 = Color.FromArgb(255, 0, 0);
                theResult.AppearanceItem.Normal.BorderColor = Color.Transparent;
                theResult.ImageAlignment = TileItemContentAlignment.BottomRight;
                theResult.Image = imageServices;
                theResult.Tag = resWPAS.systemTag;
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

            if (TProductionGlobal.WPASMenuStructure.miSections_HPASSystemAdminOrgStructureSections_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SectionScreen.ucSectionScreen();
            }

            if (TProductionGlobal.WPASMenuStructure.miBookingSettings_HPASSystemAdminSettingsBook_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsBookings();
            }
            if (TProductionGlobal.WPASMenuStructure.miCurrent_HPPASSystemAdminSettingsCurrent_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsCurrent();
            }
            if (TProductionGlobal.WPASMenuStructure.miDates_HPASSystemAdminSettingsDates_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsDates();
            }
            if (TProductionGlobal.WPASMenuStructure.miFactors_HPASSystemAdminSettingsFactors_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsFactors();
            }
            if (TProductionGlobal.WPASMenuStructure.miMines_HPASSystemAdminSettingsMines_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucSystemSettingsMines();
            }


            if (TProductionGlobal.WPASMenuStructure.miPegs_HPPASSystemAdminSetupCodesPeg_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Pegs.ucPegs();
            }
            if (TProductionGlobal.WPASMenuStructure.miSurveyCapture_HPASSurveyCapture_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.Survey.ucSurvey();
            }
            if (TProductionGlobal.WPASMenuStructure.miDailyBookings_HPASBookingDailyBookings_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.BookingsABS.ucBookingsABS();
            }
            if (TProductionGlobal.WPASMenuStructure.miSICCapture_HPASBookingSIC_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.SICCapture.ucSICCapture();
            }
            if (TProductionGlobal.WPASMenuStructure.miKriging_HPASWorkplacesKriging_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.Kriging.ucKriging();
            }

            if (TProductionGlobal.WPASMenuStructure.miCalendarTypes_HPASSystemAdminCalendarsCaltype_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.CalendarTypes.ucCalendarTypes();
            }

            if (TProductionGlobal.WPASMenuStructure.miSectionCalendar_HPASSystemAdminCalendarsSectionCalendar_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SectionCalendar.ucSectionCalendar();
            }

            if (TProductionGlobal.WPASMenuStructure.miBoxholes_HPASSystemAdminBoxholes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TrammingBoxholes.ucTrammingBoxholes();
            }
            if (TProductionGlobal.WPASMenuStructure.miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Workplace_Codes.ucWorkplace_Codes();
            }
            if (TProductionGlobal.WPASMenuStructure.miSampling_HPASDepCaptSampling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SamplingCapture.ucSamplingCapture();
            }

            if (TProductionGlobal.WPASMenuStructure.miWorkplaceCodes_HPASWorkplaceCodes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Workplace_Codes.ucWorkplace_Codes();
            }

            if (TProductionGlobal.WPASMenuStructure.miWorkplaces_HPASWorkplaces_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Workplaces.ucWorkplaces();
            }

            if (TProductionGlobal.WPASMenuStructure.miOreBody_HPASSystemAdminOreBody_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.OreBody.ucOreBody();
            }

            if (TProductionGlobal.WPASMenuStructure.miEndtypes_HPASSystemAdminEndtypes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.EndTypes.ucEndTypes();
               // theResult = new SysAdminScreens.Users.ucUserProductionSettings();
            }

            if (TProductionGlobal.WPASMenuStructure.miCrews_HPASSystemAdminOrgStructureCrews_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.GangNo.ucGangNoScreen();
            }

            if (TProductionGlobal.WPASMenuStructure.miProcessCodes_HPASWorkplaceProcessCodes_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.ProcessCodes.ucProcessCodes();
            }
            if (TProductionGlobal.WPASMenuStructure.miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.ProblemSetup.ucProblemSetup();
            }

            if (TProductionGlobal.WPASMenuStructure.miLevels_HPASSystemAdminLevels_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Level.ucLevel();
            }

            if (TProductionGlobal.WPASMenuStructure.miOtherCalendars_HPASSystemAdminCalendarsMillCalendar_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.Calendars.ucCalendars();
            }

            if (TProductionGlobal.WPASMenuStructure.miProblems_HPASSystemAdminSetupCodesProblems_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.ProblemSetup.ucProblemSetup();
            }

            if (TProductionGlobal.WPASMenuStructure.miTrammingPlanning_HPASPlanningTramming_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TrammingPlanning.ucTrammingPlanning();
            }

            if (TProductionGlobal.WPASMenuStructure.miTrammingBooking_HPASBookingTramming_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TrammingBooking.ucTrammingBooking();
            }

            if (TProductionGlobal.WPASMenuStructure.miMillingBooking_HPASBookingMilling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.MillingBooking.ucMillingBooking();
            }

            if (TProductionGlobal.WPASMenuStructure.miHoistingBooking_HPASBookingHoisting_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.HoistingBooking.ucHoistingBooking();
            }

            if (TProductionGlobal.WPASMenuStructure.miTrammingProblems_HPASSystemAdminSetupCodesTramProblems_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TrammingProblems.ucTrammingProblems();
            }


            if (TProductionGlobal.WPASMenuStructure.miPlanWorkplaces_HPASPlanningWorkplace_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PrePlanning.ucPrePlanningMain(false);
            }

            if (TProductionGlobal.WPASMenuStructure.miRevisedPlanning_HPASRevisedPlanning_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PrePlanning.ucPrePlanningMain(true);
            }

            if (TProductionGlobal.WPASMenuStructure.miRevisedPlanningSecurity_HPASRevisedPlanningSecurity_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PrePlanning.RevisedPlanning_Security.ucRevisedPlanningSecurity();
            }

            if (TProductionGlobal.WPASMenuStructure.miPlanningProtocolTemplates_HPASPlanningProtocolTemplates_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PlanningProtocolTemplates.ucPlanProtTemplates();
            }
            if (TProductionGlobal.WPASMenuStructure.miCapturePlanningProtocol_HPASCapturePlanningProtocol_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Planning.PlanningProtocolCapture.ucPlanProtCapture();
            }

            if (TProductionGlobal.WPASMenuStructure.miOreflowStorages_HPASBookingsOreflowStorages_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.OreflowStorages.ucOreflowStorages();
            }

            if (TProductionGlobal.WPASMenuStructure.miBonusCapture_HPASBookingBonusCapture_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.BonusCapture.ucBonusCapture();
            }

            if (TProductionGlobal.WPASMenuStructure.miLockMonth_HPASSystemAdminSetupCodesLockPlan_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.LockMonth.ucLockMonth();
            }

            if (TProductionGlobal.WPASMenuStructure.miTopPanelsSetup_HPASSystemAdminSetupCodesTopPanels_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.TopPanels.ucTopPanels();
            }

           

            //if (TProductionGlobal.WPASMenuStructure.miAMISSetup_HPASSystemAdminQAQCAMISSetup_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    theResult = new SysAdminScreens.AMISSetup.ucAMISSetup();
            //}

            //if (TProductionGlobal.WPASMenuStructure.miQAQC_HPASSystemAdminQAQCQAQC_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    theResult = new SysAdminScreens.QAQC.ucQAQC();
            //}

            if (TProductionGlobal.WPASMenuStructure.miBusinessPlanImport_HPASPlanningBusinessPlan_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new Controls.BusinessPlanImport.ucBusinessPlanImport();
            }

            //TODO: GS - Find out why there are 2 items linked to LockPlan. Perhaps just a typo
            if (TProductionGlobal.WPASMenuStructure.miLockPlanning_HPASLockPlanning_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.AuthLockPlan.ucAuthLockPlan();
            }
            if (TProductionGlobal.WPASMenuStructure.miAuthorisePlan_HPASLockPlanningAuthorise_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.AuthLockPlan.ucAuthLockPlan();
            }


            if (TProductionGlobal.WPASMenuStructure.miSetupCycles_HPASSystemAdminCycles_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SetupCycles.ucSetupCycles();
            }


            ////Departmental Capture

            if (TProductionGlobal.WPASMenuStructure.miSurvey_HPASDepCaptSurvey_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucSurvey();
            }

            if (TProductionGlobal.WPASMenuStructure.miGeology_HPASDepCaptGeology_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucGeology();
            }

            if (TProductionGlobal.WPASMenuStructure.miSampling_HPASDepCaptSampling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucSampling();
            }

            if (TProductionGlobal.WPASMenuStructure.miVentilation_HPASDepCaptVentilation_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucVentilation();
            }

            if (TProductionGlobal.WPASMenuStructure.miWorkNote_HPASDepCaptMinersReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucMinersReport();
            }


            if (TProductionGlobal.WPASMenuStructure.miRockEngineering_HPASDepCaptRockEngineering_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucRockEng();
            }

            if (TProductionGlobal.WPASMenuStructure.miSampling_HPASDepCaptSampling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucSampling();
            }


            if (TProductionGlobal.WPASMenuStructure.miVamping_HPASDepCaptVamping_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucVamping();
            }



            //if (TProductionGlobal.WPASMenuStructure.miOCRScheduling_HPASDepCaptOCRScheduling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    theResult = new SysAdminScreens.DepartmentalCapture.ucMinersNotes();
            //}

            


            ///New
            ///

            if (TProductionGlobal.WPASMenuStructure.miOCRScheduling_HPASOCRSchedulingMain_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.OCRScheduling.ucOCRSchedulingMain();
            }


            if (TProductionGlobal.WPASMenuStructure.miMONotes_HPASDepCaptOCRScheduling_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucMinersNotes();
            }


            if (TProductionGlobal.WPASMenuStructure.miExtendedBreaks_HPASSystemAdminExtBreaks_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.SystemSettings.ucExtendedBreak();
            }


            ////Vamping

            if (TProductionGlobal.WPASMenuStructure.miVampingPlanning_HPASPlanningVamping_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucVampPlanning();
            }


            if (TProductionGlobal.WPASMenuStructure.miVampingBooking_HPASBookingVamping_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new SysAdminScreens.DepartmentalCapture.ucVampingBooking();
            }

            //Plant Sections

            if (TProductionGlobal.WPASMenuStructure.miOtherSections_HPASStstemAdminOrgStructureOtherSections_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new SysAdminScreens.SectionScreen.ucOtherSections();
            }

            ///GeoScience
            ///
            if (TProductionGlobal.WPASMenuStructure.miGeoScience_HPASDepCaptGeoScience_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new SysAdminScreens.DepartmentalCapture.ucGeoScienseMain();
            }


            ///Labor
            ///
            if (TProductionGlobal.WPASMenuStructure.miEarlyMorningShiftReport_HPASEarlyMorningShiftReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new SysAdminScreens.Labour.ucEarlyMorningReport();
            }


            return theResult;


        }

        public mainMenu getMenuStructure()
        {
            return TProductionGlobal.WPASMenuStructure.theMenu;
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

            if (TProductionGlobal.WPASMenuStructure.miPlanningProtocolReport_HPASPlanningProtocolReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                theResult = new ucPlanningProtocolReport();
            }

            if (TProductionGlobal.WPASMenuStructure.miMeasuringList_HPASReportsMeasuringList_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.MeasuringListReport.ucMeasuringListReport();         
            }

            if (TProductionGlobal.WPASMenuStructure.miPlanVsBook_HPASReportsPlanVsBook_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.PlannedVsBooked.ucPlannedVsBooked();
            }

            if (TProductionGlobal.WPASMenuStructure.miGradeReport_HPASReportsGradeReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.GradeReport.ucGradeReport();
            }

            //if (TProductionGlobal.WPASMenuStructure.miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    return new Reports.PlanningReportNewStyle.ucPlanningReportNewStyle();
            //}

            if (TProductionGlobal.WPASMenuStructure.miPlathond_HPASReportWallRoom_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.PlathondWallChartReport.PlathondWallChartReportUserControl();
            }

            if (TProductionGlobal.WPASMenuStructure.miBonusReport_HPASReportBonus_MinewareSystemsHarmonyPAS .ItemID  == itemID)
            {
                return new Reports.BonusReport.ucBonusReport();
            }

            //if (TProductionGlobal.WPASMenuStructure.miCourseBlankReport_HPASReportCourseBlank_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    return new Reports.CourseBlankSampleReport.ucCourseBlankSampleReport();
            //}

            //if (TProductionGlobal.WPASMenuStructure.miStandardSRMReport_HPASReportStandardCRM_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    return new Reports.StandardCRMReport.ucStandardCRMReport();
            //}
            if (TProductionGlobal.WPASMenuStructure.miTopPanelsReport_HPASReportTopPanels_MinewareSystemsHarmonyPAS .ItemID  == itemID)
            {
                return new Reports.TopPanelsReport.ucTopPanelsReport();
            }
            if (TProductionGlobal.WPASMenuStructure.miTonnage_HPASReportTonnage_MinewareSystemsHarmonyPAS.ItemID  == itemID)
            {
                return new Reports.TonnageReport.ucTonnageReport();
            }

            if (TProductionGlobal.WPASMenuStructure.miProductionAnalysis_HPASReportProductionAnalysis_MinewareSystemsHarmonyPAS .ItemID  == itemID)
            {
                return new Reports.ProductionAnalysisReport.ucProductionAnalysisReport();
            }

            if (TProductionGlobal.WPASMenuStructure.miMODaily_HPASReportMODaily_MinewareSystemsHarmonyPAS.ItemID  == itemID)
            {
                return new Reports.MODaily.ucMODailyProduction();
            }

            if (TProductionGlobal.WPASMenuStructure.miCrewRanking_HPASReportCrewRanking_MinewareSystemsHarmonyPAS .ItemID  == itemID)
            {
                return new Reports.CrewRanking.ucCrewRanking();
            }

            if (TProductionGlobal.WPASMenuStructure.miProblemAnalysis_HPASReportProblemAnalysis_MinewareSystemsHarmonyPAS .ItemID == itemID)
            {
                return new Reports.Problem_Analysis_Report.ucProblemAnalysisReport();
            }

            //if (TProductionGlobal.WPASMenuStructure.miPlanningReportNewStyle_HPASReportsPlanningReportNewStyle_MinewareSystemsHarmonyPAS.ItemID == itemID)
            //{
            //    return new Reports.PlanningReportNewStyle.ucPlanningReportNewStyle();
            //}

            if (TProductionGlobal.WPASMenuStructure.miSICReport_HPASReportSICReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.SICReport.ucSICReport();
            }
            if (TProductionGlobal.WPASMenuStructure.miProblemHistoryPareto_HPASReportLostBlastReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.ProblemsReport.ucProblemsReport();
            }

            ///New Graph Problems

            if (TProductionGlobal.WPASMenuStructure.miProblemHistoryGraph_HPASReportProblemHistGraph_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.ProblemHistoryGraph.ucProblemsReport();
            }

            if (TProductionGlobal.WPASMenuStructure.miSurveyReport_HPASReportSurveyReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                 return new Reports.SurveyReport.ucSurveyReport();
            }

            if (TProductionGlobal.WPASMenuStructure.miPlanningReport_HPASReportsPlanningReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.PlanningReport.ucPlanningReport();
            }

            if (TProductionGlobal.WPASMenuStructure.miGenericReport_HPASReportGenericReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.GenericReport.ucGenericReport();
            }

            if (TProductionGlobal.WPASMenuStructure.miRevisedPlanningAuditReport_HPASRevisedAuditReport_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.RevisedPlanning_Audit_Report.ucRevisedPlanningAuditReport();
            }

            if (TProductionGlobal.WPASMenuStructure.miWorstPerformers_HPASReportWorstPerformers_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.WorstPerformers.ucWorstPerformerss();
            }

            ///NEW
            ///

            if (TProductionGlobal.WPASMenuStructure.miMODailyDetail_HPASReportMODailyDetail_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports.MODailyDetail.ucMODailyProduction();
            }


            if (TProductionGlobal.WPASMenuStructure.mi6ShiftRecon_HPASReport6ShiftRecon_MinewareSystemsHarmonyPAS.ItemID == itemID)
            {
                return new Reports._6ShiftRecon.ucRecon();
            }

            if (TProductionGlobal.WPASMenuStructure.miPlanningReportStyle2_HPASReportsPlanningReport2_MinewareSystemsHarmonyPAS.ItemID == itemID)
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
            return resWPAS.systemDBTag;
        }

        public string getSystemTag()
        {
            return resWPAS.systemTag;
        }

        public global::DevExpress.XtraNavBar.NavBarItem getUserSettingsNavBarItem()
        {
            var theResult = new DevExpress.XtraNavBar.NavBarItem() { Caption = "Production", Tag = resWPAS.systemTag };
            return theResult;
        }

        public ucBaseUserControl getUserSettingsScreen(ScreenStatus _theScreenStatus, string _userID, TUserCurrentInfo userInfo, string theConnection)
        {
            
            SysAdminScreens.Users.ucUserProductionSettings theResult = 
                    new SysAdminScreens.Users.ucUserProductionSettings() { theScreenStatus = _theScreenStatus, UserID = _userID, theSystemDBTag = resWPAS.systemDBTag, theSystemTag = resWPAS.systemTag, UserCurrentInfo = userInfo };
            theResult.currentConnection = theConnection;
            return theResult;
        }

        public void InitializeModule()
        {
            TProductionGlobal.WPASMenuStructure.setMenuItems();
            TProductionGlobal.WPASMenuStructure.theMenu.systemDBTag = resWPAS.systemDBTag;
            TProductionGlobal.WPASMenuStructure.theMenu.systemTag = resWPAS.systemTag;
        }

        public void LoggedOn()
        {
            // Use this section to load all module settings like user logon info or production info
            //TProductionGlobal.get(resWPAS .systemDBTag,TUserInfo .Connection );
            TProductionGlobal.SetProductionGlobalInfo(resWPAS.systemDBTag);
            TProductionGlobal.getSystemSettingsProductioInfo(resWPAS.systemDBTag);
            TProductionGlobal.SetUserInfo(resWPAS.systemDBTag);
            //TProductionGlobal.getUserInfo(resWPAS.systemDBTag);
        }

        //ucReportSettingsControl PluginInterface.getReportSettings(string itemID)
        //{
        //     throw new NotImplementedException();
        //}
    }
}
