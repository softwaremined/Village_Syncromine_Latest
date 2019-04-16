Alter Procedure dbo.sp_Latest_Geology_Report_Number_DEV   --in
--Declare                     --out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as                      --in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'             --out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 66) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,

       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66001,SC.Sectionid_2)[ Approaching/ Exiting a dyke],
    dbo.GetPlanProdFieldName(66001) [ Approaching/ Exiting a dyke Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66002,SC.Sectionid_2)[Dyke 35],
    dbo.GetPlanProdFieldName(66002) [Dyke 35 Name],  
     dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66003,SC.Sectionid_2)[Approaching Major Fault 4m ],
    dbo.GetPlanProdFieldName(66003) [Approaching Major Fault 4m  Name],  
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66004,SC.Sectionid_2)[End Lithology],
    dbo.GetPlanProdFieldName(66004) [End Lithology Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66005,SC.Sectionid_2)[Quartz veins/ Joints / Minor faults],
    dbo.GetPlanProdFieldName(66005) [Quartz veins/ Joints / Minor faults Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66006,SC.Sectionid_2)[Expected downthrow fault 1m],
    dbo.GetPlanProdFieldName(66006) [Expected downthrow fault 1m Name], 
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66007,SC.Sectionid_2)[Argillite exposure ],
    dbo.GetPlanProdFieldName(66007) [Argillite exposure  Name],  
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66008,SC.Sectionid_2)[FOG Gravity],
    dbo.GetPlanProdFieldName(66008) [FOG Gravity Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66009,SC.Sectionid_2)[Dip of Strata],
    dbo.GetPlanProdFieldName(66009) [RIH Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66010,SC.Sectionid_2)[Expected Reef roll ( Y/N )],
    dbo.GetPlanProdFieldName(66010) [Expected Reef roll ( Y/N ) Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66011,SC.Sectionid_2)[FW Width],
    dbo.GetPlanProdFieldName(66011) [FW Width Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66012,SC.Sectionid_2)[Reef Detector no.],
    dbo.GetPlanProdFieldName(66012) [Reef Detector no.Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66013,SC.Sectionid_2)[In Cover],
    dbo.GetPlanProdFieldName(66013) [In Cover Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
      pp.Prodmonth = SC.PRODMONTH AND
      pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
      PPDA.PRODMONTH = @PRODMONTH AND 
      sc.Sectionid_2 = PPDA.SECTIONID AND
      pp.Workplaceid = PPDA.WORKPLACEID AND
      pp.Activity = PPDA.ActivityType AND
      PPDA.TemplateID = 66
INNER JOIN Workplace W on
      pp.Workplaceid = w.Workplaceid
      WHERE 
      PP.Prodmonth = @PRODMONTH AND
      sc.Sectionid_2 = @SectionID_2 AND
      pp.Activity = 1 AND 
      PP.PlanCode ='MP'

GO