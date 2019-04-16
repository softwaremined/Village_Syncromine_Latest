Create Procedure dbo.sp_Development_Equipping_Dev				--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 11) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,

dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Support Conditions],
dbo.GetPlanProdFieldName(1100001) [Support Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Support Position],
dbo.GetPlanProdFieldName(1100002) [Support Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Support Action Require],
dbo.GetPlanProdFieldName(1100003) [Support Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Support Completion Date],
dbo.GetPlanProdFieldName(1100004) [Support Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Secondary Support Completion Date],
dbo.GetPlanProdFieldName(1100005) [Secondary Support Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Secondary Support Action Require],
dbo.GetPlanProdFieldName(1100006) [Secondary Support Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Secondary Support Position],
dbo.GetPlanProdFieldName(1100007) [Secondary Support Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Secondary Support Conditions],
dbo.GetPlanProdFieldName(1100008) [Secondary Support Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Air Conditions],
dbo.GetPlanProdFieldName(1100009) [Pipes: Air Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Air Position],
dbo.GetPlanProdFieldName(1100010) [Pipes: Air Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Air Action Require],
dbo.GetPlanProdFieldName(1100011) [Pipes: Air Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Air Completion Date],
dbo.GetPlanProdFieldName(1100012) [Pipes: Air Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Water Completion Date],
dbo.GetPlanProdFieldName(1100013) [Pipes: Water Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Water Action Require],
dbo.GetPlanProdFieldName(1100014) [Pipes: Water Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Water Position],
dbo.GetPlanProdFieldName(1100015) [Pipes: Water Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Water Conditions],
dbo.GetPlanProdFieldName(1100016) [Pipes: Water Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Vent Conditions],
dbo.GetPlanProdFieldName(1100017) [Pipes: Vent Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Vent Position],
dbo.GetPlanProdFieldName(1100018) [Pipes: Vent Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Vent Action Require],
dbo.GetPlanProdFieldName(1100019) [Pipes: Vent Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Pipes: Vent Completion Date],
dbo.GetPlanProdFieldName(1100020) [Pipes: Vent Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Tracks & Sleepers Completion Date],
dbo.GetPlanProdFieldName(1100021) [Tracks & Sleepers Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Tracks & Sleepers Action Require],
dbo.GetPlanProdFieldName(1100022) [Tracks & Sleepers Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Tracks & Sleepers Position],
dbo.GetPlanProdFieldName(1100023) [Tracks & Sleepers Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Tracks & Sleepers Conditions],
dbo.GetPlanProdFieldName(1100024) [Tracks & Sleepers Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Drains Conditions],
dbo.GetPlanProdFieldName(1100025) [Drains Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Drains Position],
dbo.GetPlanProdFieldName(1100026) [Drains Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Drains Action Require],
dbo.GetPlanProdFieldName(1100027) [Drains Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Drains Completion Date],
dbo.GetPlanProdFieldName(1100028) [Drains Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Gully Winch Completion Date],
dbo.GetPlanProdFieldName(1100029) [Gully Winch Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Gully Winch Action Require],
dbo.GetPlanProdFieldName(1100030) [Gully Winch Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Gully Winch Position],
dbo.GetPlanProdFieldName(1100031) [Gully Winch Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Gully Winch Conditions],
dbo.GetPlanProdFieldName(1100032) [Gully Winch Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Mono Winch Conditions],
dbo.GetPlanProdFieldName(1100033) [Mono Winch Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Mono Winch Position],
dbo.GetPlanProdFieldName(1100034) [Mono Winch Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Mono Winch Action Require],
dbo.GetPlanProdFieldName(1100035) [Mono Winch Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Mono Winch Completion Date],
dbo.GetPlanProdFieldName(1100036) [Mono Winch Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Grizzly Installation Completion Date],
dbo.GetPlanProdFieldName(1100037) [Grizzly Installation Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Grizzly Installation Action Require],
dbo.GetPlanProdFieldName(1100038) [Grizzly Installation Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Grizzly Installation Position],
dbo.GetPlanProdFieldName(1100039) [Grizzly Installation Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Grizzly Installation Conditions],
dbo.GetPlanProdFieldName(1100040) [Grizzly Installation Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Haulage Civils Conditions],
dbo.GetPlanProdFieldName(1100041) [Haulage Civils Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Haulage Civils Position],
dbo.GetPlanProdFieldName(1100042) [Haulage Civils Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Haulage Civils Action Require],
dbo.GetPlanProdFieldName(1100043) [Haulage Civils Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Haulage Civils Completion Date],
dbo.GetPlanProdFieldName(1100044) [Haulage Civils Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Workshops Completion Date],
dbo.GetPlanProdFieldName(1100045) [Workshops Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Workshops Action Require],
dbo.GetPlanProdFieldName(1100046) [Workshops Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Workshops Position],
dbo.GetPlanProdFieldName(1100047) [Workshops Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Workshops Conditions],
dbo.GetPlanProdFieldName(1100048) [Workshops Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Box Fronts Conditions],
dbo.GetPlanProdFieldName(1100049) [Box Fronts Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Box Fronts Position],
dbo.GetPlanProdFieldName(1100050) [Box Fronts Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Box Fronts Action Require],
dbo.GetPlanProdFieldName(1100051) [Box Fronts Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Box Fronts Completion Date],
dbo.GetPlanProdFieldName(1100052) [Box Fronts Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Refuge Chambers Completion Date],
dbo.GetPlanProdFieldName(1100053) [Refuge Chambers Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Refuge Chambers Action Require],
dbo.GetPlanProdFieldName(1100054) [Refuge Chambers Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Refuge Chambers Position],
dbo.GetPlanProdFieldName(1100055) [Refuge Chambers Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Refuge Chambers Conditions],
dbo.GetPlanProdFieldName(1100056) [Refuge Chambers Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Material Bay Conditions],
dbo.GetPlanProdFieldName(1100057) [Material Bay Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Material Bay Position],
dbo.GetPlanProdFieldName(1100058) [Material Bay Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Material Bay Action Require],
dbo.GetPlanProdFieldName(1100059) [Material Bay Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Material Bay Completion Date],
dbo.GetPlanProdFieldName(1100060) [Material Bay Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Cubbies for pumps Completion Date],
dbo.GetPlanProdFieldName(1100061) [Cubbies for pumps Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Cubbies for pumps Action Require],
dbo.GetPlanProdFieldName(1100062) [Cubbies for pumps Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Cubbies for pumps Position],
dbo.GetPlanProdFieldName(1100063) [Cubbies for pumps Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Cubbies for pumps Conditions],
dbo.GetPlanProdFieldName(1100064) [Cubbies for pumps Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Box Front Slot to size Conditions],
dbo.GetPlanProdFieldName(1100065) [Box Front Slot to size Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Box Front Slot to size Position],
dbo.GetPlanProdFieldName(1100066) [Box Front Slot to size Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Box Front Slot to size Action Require],
dbo.GetPlanProdFieldName(1100067) [Box Front Slot to size Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Box Front Slot to size Completion Date],
dbo.GetPlanProdFieldName(1100068) [Box Front Slot to size Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Transformer Cubbies Completion Date],
dbo.GetPlanProdFieldName(1100069) [Transformer Cubbies Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Transformer Cubbies Action Require],
dbo.GetPlanProdFieldName(1100070) [Transformer Cubbies Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Transformer Cubbies Position],
dbo.GetPlanProdFieldName(1100071) [Transformer Cubbies Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Transformer Cubbies Conditions],
dbo.GetPlanProdFieldName(1100072) [Transformer Cubbies Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Drain Holes Conditions],
dbo.GetPlanProdFieldName(1100073) [Drain Holes Conditions],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Drain Holes Position],
dbo.GetPlanProdFieldName(1100074) [Drain Holes Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Drain Holes Action Require],
dbo.GetPlanProdFieldName(1100075) [Drain Holes Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Drain Holes Completion Date],
dbo.GetPlanProdFieldName(1100076) [Drain Holes Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Sumps Completion Date],
dbo.GetPlanProdFieldName(1100077) [Sumps Completion Date],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Sumps Action Require],
dbo.GetPlanProdFieldName(1100078) [Sumps Action Require],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Sumps Position],
dbo.GetPlanProdFieldName(1100089) [Sumps Position],
dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,1100001,SC.Sectionid_2)[Sumps Conditions],
dbo.GetPlanProdFieldName(1100080) [Sumps Conditions],

       
	   

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
						PPDA.TemplateID = 11
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'

GO

Alter Procedure dbo.sp_PlanProd_Saftey_Dev				--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 11) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11001,SC.Sectionid_2)[Date],
	   dbo.GetPlanProdFieldName(11001) [Date Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11002,SC.Sectionid_2)[Last Accident],
	   dbo.GetPlanProdFieldName(11002) [Last Accident Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11003,SC.Sectionid_2)[A` Hazards],
	   dbo.GetPlanProdFieldName(11003) [A` Hazards Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11004,SC.Sectionid_2)[B` Hazards],
	   dbo.GetPlanProdFieldName(11004) [B` Hazards Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11005,SC.Sectionid_2)[C` Hazards],
	   dbo.GetPlanProdFieldName(11005) [C` Hazards Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11006,SC.Sectionid_2)[Dressing Cases],
	   dbo.GetPlanProdFieldName(11006) [Dressing Cases Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11007,SC.Sectionid_2)[Lost time],
	   dbo.GetPlanProdFieldName(11007) [Lost time Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11008,SC.Sectionid_2)[Serious],
	   dbo.GetPlanProdFieldName(11008) [Serious Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11009,SC.Sectionid_2)[White Flag Days],
	   dbo.GetPlanProdFieldName(11009) [White Flag Days Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11010,SC.Sectionid_2)[Physical Condition Rating],
	   dbo.GetPlanProdFieldName(11010) [Physical Condition Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11011,SC.Sectionid_2)[Standards Rating  Rating],
	   dbo.GetPlanProdFieldName(11011) [Standards Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11012,SC.Sectionid_2)[Explosives Rating],
	   dbo.GetPlanProdFieldName(11012) [Explosives Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11013,SC.Sectionid_2)[Pre Conditioning Drilled To Std],
	   dbo.GetPlanProdFieldName(11013) [Pre Conditioning Drilled To Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11014,SC.Sectionid_2)[Temp & Permanent Support & Resin Bolts To Std],
	   dbo.GetPlanProdFieldName(11014) [Temp & Permanent Support & Resin Bolts To Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11015,SC.Sectionid_2)[Stop & Fixes Conceded],
	   dbo.GetPlanProdFieldName(11015) [Stop & Fixes Conceded Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11016,SC.Sectionid_2)[Explosives Stored & Controlled As Per Std],
	   dbo.GetPlanProdFieldName(11016) [Explosives Stored & Controlled As Per Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11017,SC.Sectionid_2)[Winches , Rigging, ect. ],
	   dbo.GetPlanProdFieldName(11017) [Winches , Rigging, ect.  Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11018,SC.Sectionid_2)[Appointments Done],
	   dbo.GetPlanProdFieldName(11018) [Appointments Done Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11019,SC.Sectionid_2)[Early Shift],
	   dbo.GetPlanProdFieldName(11019) [Early Shift Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11020,SC.Sectionid_2)[Late Shift],
	   dbo.GetPlanProdFieldName(11020) [Late Shift Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11021,SC.Sectionid_2)[Declaration],
	   dbo.GetPlanProdFieldName(11021) [Declaration Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11022,SC.Sectionid_2)[Other Safety Concerns in Panel],
	   dbo.GetPlanProdFieldName(11022) [Other Safety Concerns in Panel Name],
	   

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
						PPDA.TemplateID = 11
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'

GO

Alter Procedure dbo.sp_PlanProd_Saftey_STP				--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 1) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10001,SC.Sectionid_2)[Date],
	   dbo.GetPlanProdFieldName(10001) [Date Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Last Accident],
	   dbo.GetPlanProdFieldName(10002) [Last Accident Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[A` Hazards],
	   dbo.GetPlanProdFieldName(10003) [A` Hazards Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[B` Hazards],
	   dbo.GetPlanProdFieldName(10004) [B` Hazards Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[C` Hazards],
	   dbo.GetPlanProdFieldName(10005) [C` Hazards Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Dressing Cases],
	   dbo.GetPlanProdFieldName(10006) [Dressing Cases Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Lost time],
	   dbo.GetPlanProdFieldName(10007) [Lost time Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Serious],
	   dbo.GetPlanProdFieldName(10008) [Serious Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[White Flag Days],
	   dbo.GetPlanProdFieldName(10009) [White Flag Days Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Physical Condition Rating],
	   dbo.GetPlanProdFieldName(10010) [Physical Condition Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Standards Rating  Rating],
	   dbo.GetPlanProdFieldName(10011) [Standards Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Explosives Rating],
	   dbo.GetPlanProdFieldName(10012) [Explosives Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Pre Conditioning Drilled To Std],
	   dbo.GetPlanProdFieldName(10013) [Pre Conditioning Drilled To Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Temp & Permanent Support & Resin Bolts To Std],
	   dbo.GetPlanProdFieldName(10014) [Temp & Permanent Support & Resin Bolts To Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Stop & Fixes Conceded],
	   dbo.GetPlanProdFieldName(10015) [Stop & Fixes Conceded Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Explosives Stored & Controlled As Per Std],
	   dbo.GetPlanProdFieldName(10016) [Explosives Stored & Controlled As Per Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Winches , Rigging, ect. ],
	   dbo.GetPlanProdFieldName(10017) [Winches , Rigging, ect.  Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Appointments Done],
	   dbo.GetPlanProdFieldName(10018) [Appointments Done Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Early Shift],
	   dbo.GetPlanProdFieldName(10019) [Early Shift Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Late Shift],
	   dbo.GetPlanProdFieldName(10020) [Late Shift Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Declaration],
	   dbo.GetPlanProdFieldName(10021) [Declaration Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Other Safety Concerns in Panel],
	   dbo.GetPlanProdFieldName(10022) [Other Safety Concerns in Panel Name],
	   

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
						PPDA.TemplateID = 1
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
GO

Create Procedure dbo.sp_Production_STP				--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 11) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,
	   
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11001,SC.Sectionid_2)[Pump Required],
	   dbo.GetPlanProdFieldName(100001) [Pump Required Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11002,SC.Sectionid_2)[Pump Completion Date],
	   dbo.GetPlanProdFieldName(100002) [Pump Completion Date Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11003,SC.Sectionid_2)[Winch Bed Required],
	   dbo.GetPlanProdFieldName(100003) [Winch Bed Required Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11004,SC.Sectionid_2)[Winch Bed Completion Date],
	   dbo.GetPlanProdFieldName(100004) [Winch Bed Completion Date],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11005,SC.Sectionid_2)[Effective B/Barricade Action Required],
	   dbo.GetPlanProdFieldName(100005) [Effective B/Barricade Action Required Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11006,SC.Sectionid_2)[Effective B/Barricade Completion Date],
	   dbo.GetPlanProdFieldName(100006) [Effective B/Barricade Completion Date Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11007,SC.Sectionid_2)[Second Escape Required],
	   dbo.GetPlanProdFieldName(100007) [Second Escape Required Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11008,SC.Sectionid_2)[Second Escape Completion Date],
	   dbo.GetPlanProdFieldName(100008) [Second Escape Completion Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11009,SC.Sectionid_2)[ Face winch distance to face Action Required],
	   dbo.GetPlanProdFieldName(100009) [ Face winch distance to face Action Required Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,11010,SC.Sectionid_2)[Face winch distance to face Completion Date],
	   dbo.GetPlanProdFieldName(100010) [Face winch distance to face Completion Date Name],
	 

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
						PPDA.TemplateID = 11
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'

GO

Alter Procedure dbo.sp_Engineering_Equipping_DEV			--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 22) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22001,SC.Sectionid_2)[Loco's/ Loaders Conditions],
	   dbo.GetPlanProdFieldName(22001) [Loco's/ Loaders Conditions],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22002,SC.Sectionid_2)[Loco's/ Loaders Condition No.],
	   dbo.GetPlanProdFieldName(22002) [Loco's/ Loaders Condition No. Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22003,SC.Sectionid_2)[Mini-sub Cubbies Requiredh],
	   dbo.GetPlanProdFieldName(22003) [Mini-sub Cubbies Required Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22004,SC.Sectionid_2)[Mini-sub Cubbies No.],
	   dbo.GetPlanProdFieldName(22004) [Mini-sub Cubbies No. Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22005,SC.Sectionid_2)[Winch Cubbies Fire Proofed],
	   dbo.GetPlanProdFieldName(22005) [Winch Cubbies Fire Proofed Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22006,SC.Sectionid_2)[Winch Cubbies Fire Proofed Number],
	   dbo.GetPlanProdFieldName(22006) [Winch Cubbies Fire Proofed Number Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22007,SC.Sectionid_2)[Lights installed],
	   dbo.GetPlanProdFieldName(22007) [Lights installed Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22008,SC.Sectionid_2)[Lights installed Number],
	   dbo.GetPlanProdFieldName(22008) [Lights installed Number Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22009,SC.Sectionid_2)[Winch Move Planned],
	   dbo.GetPlanProdFieldName(22009) [Winch Move Planned Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22010,SC.Sectionid_2)[Winch Move Planned Date],
	   dbo.GetPlanProdFieldName(22010) [Winch Move Planned Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22011,SC.Sectionid_2)[Mono Wich Move Planned],
	   dbo.GetPlanProdFieldName(22011) [Mono Wich Move Planned Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22012,SC.Sectionid_2)[Mono Wich Move Planned Date],
	   dbo.GetPlanProdFieldName(22012) [Mono Wich Move Planned Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22013,SC.Sectionid_2)[Power Extensions Required],
	   dbo.GetPlanProdFieldName(22013) [Power Extensions Required Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22014,SC.Sectionid_2)[Power Extensions Required Date],
	   dbo.GetPlanProdFieldName(22014) [Power Extensions Required Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22015,SC.Sectionid_2)[Machinery Maintenace Program ],
	   dbo.GetPlanProdFieldName(22015) [Machinery Maintenace Program Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22016,SC.Sectionid_2)[Machinery Maintenace Program Date],
	   dbo.GetPlanProdFieldName(22016) [Machinery Maintenace Program Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,22017,SC.Sectionid_2)[Equipment Required for new Month],
	   dbo.GetPlanProdFieldName(22017) [Equipment Required for new Month Name],


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
						PPDA.TemplateID = 22
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Engineering_Equipping_STP				--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 2) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20001,SC.Sectionid_2)[Centre Gully Winch],
	   dbo.GetPlanProdFieldName(20001) [Centre Gully Winch Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20002,SC.Sectionid_2)[Centre Gully Winch Number],
	   dbo.GetPlanProdFieldName(20002) [Centre Gully Winch Number Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20003,SC.Sectionid_2)[Face Winch],
	   dbo.GetPlanProdFieldName(20003) [Face Winch Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20004,SC.Sectionid_2)[Face Winch Number],
	   dbo.GetPlanProdFieldName(20004) [Face Winch Number Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20005,SC.Sectionid_2)[Winch Cubbies Fire Proofed],
	   dbo.GetPlanProdFieldName(20005) [Winch Cubbies Fire Proofed Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20006,SC.Sectionid_2)[Winch Cubbies Fire Proofed Number],
	   dbo.GetPlanProdFieldName(20006) [Winch Cubbies Fire Proofed Number Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20007,SC.Sectionid_2)[Lights installed],
	   dbo.GetPlanProdFieldName(20007) [Lights installed Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20008,SC.Sectionid_2)[Lights installed Number],
	   dbo.GetPlanProdFieldName(20008) [Lights installed Number Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20009,SC.Sectionid_2)[Winch Move Planned],
	   dbo.GetPlanProdFieldName(20009) [Winch Move Planned Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20010,SC.Sectionid_2)[Winch Move Planned Date],
	   dbo.GetPlanProdFieldName(20010) [Winch Move Planned Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20011,SC.Sectionid_2)[Mono Wich Move Planned],
	   dbo.GetPlanProdFieldName(20011) [Mono Wich Move Planned Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20012,SC.Sectionid_2)[Mono Wich Move Planned Date],
	   dbo.GetPlanProdFieldName(20012) [Mono Wich Move Planned Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20013,SC.Sectionid_2)[Power Extensions Required],
	   dbo.GetPlanProdFieldName(20013) [Power Extensions Required Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20014,SC.Sectionid_2)[Power Extensions Required Date],
	   dbo.GetPlanProdFieldName(20014) [Power Extensions Required Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20015,SC.Sectionid_2)[Machinery Maintenace Program ],
	   dbo.GetPlanProdFieldName(20015) [Machinery Maintenace Program Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20016,SC.Sectionid_2)[Machinery Maintenace Program Date],
	   dbo.GetPlanProdFieldName(20016) [Machinery Maintenace Program Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,20017,SC.Sectionid_2)[Equipment Required for new Month],
	   dbo.GetPlanProdFieldName(20017) [Equipment Required for new Month Name],


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
						PPDA.TemplateID = 2
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Finance_DEV			--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 99) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99001,SC.Sectionid_2)[Prev Mth Labour Cost/m2],
	   dbo.GetPlanProdFieldName(99001) [Prev Mth Labour Cost/m2Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99002,SC.Sectionid_2)[Prev Mth Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(99002) [Prev Mth Explosives Cost/m2 Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99003,SC.Sectionid_2)[Pev Mth Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(99003) [Pev Mth Drilling Cost/m2 Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99004,SC.Sectionid_2)[Prev Mth Support Cost/m2],
	   dbo.GetPlanProdFieldName(99004) [Prev Mth Support Cost/m2 Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99005,SC.Sectionid_2)[Prev Mth Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(99005) [Prev Mth Cleaining Cost/m2 Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99006,SC.Sectionid_2)[Prev Mth Other Cost/m2],
	   dbo.GetPlanProdFieldName(99006) [Prev Mth Other Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99007,SC.Sectionid_2)[Current Mth Labour Cost/m2],
	   dbo.GetPlanProdFieldName(99007) [Current Mth Labour Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99008,SC.Sectionid_2)[Current Mth Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(99008) [Current Mth Explosives Cost/m2Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99009,SC.Sectionid_2)[Current Mth Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(99009) [Current Mth Drilling Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99010,SC.Sectionid_2)[Current Mth Support Cost/m2],
	   dbo.GetPlanProdFieldName(99010) [Current Mth Support Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99011,SC.Sectionid_2)[Current Mth Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(99011) [Current Mth Cleaining Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99012,SC.Sectionid_2)[Current Mth Other Cost/m2],
	   dbo.GetPlanProdFieldName(99012) [Current Mth Other Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99013,SC.Sectionid_2)[Planned Labour Cost/m2],
	   dbo.GetPlanProdFieldName(99013) [Planned Labour Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99014,SC.Sectionid_2)[Planned Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(99014) [Planned Explosives Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99015,SC.Sectionid_2)[Planned Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(99015) [Planned Drilling Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99016,SC.Sectionid_2)[Planned Support Cost/m2],
	   dbo.GetPlanProdFieldName(99016) [Planned Support Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99017,SC.Sectionid_2)[Planned Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(99017) [Planned Cleaining Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,99018,SC.Sectionid_2)[Planned Other Cost/m2],
	   dbo.GetPlanProdFieldName(99018) [Planned Other Cost/m2 Name],  


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
						PPDA.TemplateID = 99
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Finance_STP				
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 9) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,

		dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90001,SC.Sectionid_2)[Prev Mth Labour Cost/m2],
	   dbo.GetPlanProdFieldName(90001) [Prev Mth Labour Cost/m2Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90002,SC.Sectionid_2)[Prev Mth Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(90002) [Prev Mth Explosives Cost/m2 Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90003,SC.Sectionid_2)[Pev Mth Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(90003) [Pev Mth Drilling Cost/m2 Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90004,SC.Sectionid_2)[Prev Mth Support Cost/m2],
	   dbo.GetPlanProdFieldName(90004) [Prev Mth Support Cost/m2 Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90005,SC.Sectionid_2)[Prev Mth Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(90005) [Prev Mth Cleaining Cost/m2 Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90006,SC.Sectionid_2)[Prev Mth Other Cost/m2],
	   dbo.GetPlanProdFieldName(90006) [Prev Mth Other Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90007,SC.Sectionid_2)[Current Mth Labour Cost/m2],
	   dbo.GetPlanProdFieldName(90007) [Current Mth Labour Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90008,SC.Sectionid_2)[Current Mth Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(90008) [Current Mth Explosives Cost/m2Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90009,SC.Sectionid_2)[Current Mth Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(90009) [Current Mth Drilling Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90010,SC.Sectionid_2)[Current Mth Support Cost/m2],
	   dbo.GetPlanProdFieldName(90010) [Current Mth Support Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90011,SC.Sectionid_2)[Current Mth Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(90011) [Current Mth Cleaining Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90012,SC.Sectionid_2)[Current Mth Other Cost/m2],
	   dbo.GetPlanProdFieldName(90012) [Current Mth Other Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90013,SC.Sectionid_2)[Planned Labour Cost/m2],
	   dbo.GetPlanProdFieldName(90013) [Planned Labour Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90014,SC.Sectionid_2)[Planned Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(90014) [Planned Explosives Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90015,SC.Sectionid_2)[Planned Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(90015) [Planned Drilling Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90016,SC.Sectionid_2)[Planned Support Cost/m2],
	   dbo.GetPlanProdFieldName(90016) [Planned Support Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90017,SC.Sectionid_2)[Planned Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(90017) [Planned Cleaining Cost/m2 Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,90018,SC.Sectionid_2)[Planned Other Cost/m2],
	   dbo.GetPlanProdFieldName(90018) [Planned Other Cost/m2 Name],  


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
						PPDA.TemplateID = 9
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Latest_Geology_Report_Number_STP				--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 6) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60001,SC.Sectionid_2)[Dykes],
	   dbo.GetPlanProdFieldName(60001) [DykesName],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60002,SC.Sectionid_2)[Faults > 0.5m],
	   dbo.GetPlanProdFieldName(60002) [Faults > 0.5m Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60003,SC.Sectionid_2)[Faults < 0.5m],
	   dbo.GetPlanProdFieldName(60003) [Faults < 0.5m Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60004,SC.Sectionid_2)[Faults/Dykes < 400],
	   dbo.GetPlanProdFieldName(60004) [Faults/Dykes < 400 Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60005,SC.Sectionid_2)[Quartz veins/Joints],
	   dbo.GetPlanProdFieldName(60005) [Quartz veins/Joints Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60006,SC.Sectionid_2)[Dip of Reef],
	   dbo.GetPlanProdFieldName(60006) [Dip of Reef Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60007,SC.Sectionid_2)[Argillite exposure ],
	   dbo.GetPlanProdFieldName(60007) [Argillite exposure  Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60008,SC.Sectionid_2)[FOG Gravity],
	   dbo.GetPlanProdFieldName(60008) [FOG Gravity Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60009,SC.Sectionid_2)[RIH],
	   dbo.GetPlanProdFieldName(60009) [RIH Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60010,SC.Sectionid_2)[RIF],
	   dbo.GetPlanProdFieldName(60010) [RIF Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60011,SC.Sectionid_2)[Rolling],
	   dbo.GetPlanProdFieldName(60011) [Rolling Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60012,SC.Sectionid_2)[Extent of roll (high-Low)],
	   dbo.GetPlanProdFieldName(60012) [Extent of roll (high-Low) Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60014,SC.Sectionid_2)[SW Recommended],
	   dbo.GetPlanProdFieldName(60013) [SW Recommended Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,60015,SC.Sectionid_2)[Reef Detector no.],
	   dbo.GetPlanProdFieldName(60014) [Reef Detector no.Name],


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
						PPDA.TemplateID = 6
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Latest_RME_Recommendation_Number_DEV			
--Declare																					
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 44) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,44001,SC.Sectionid_2)[Latest RME Recommendation Number],
	   dbo.GetPlanProdFieldName(44001) [Latest RME Recommendation Number Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,44002,SC.Sectionid_2)[Support Compliance %],
	   dbo.GetPlanProdFieldName(44002) [Support Compliance % Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,44003,SC.Sectionid_2)[Panel Rating],
	   dbo.GetPlanProdFieldName(44003) [Panel Rating Name],  
	   


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
						PPDA.TemplateID = 44
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Latest_RME_Report_Number_DEV			--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 33) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33001,SC.Sectionid_2)[RMR],
	   dbo.GetPlanProdFieldName(33001) [RMR Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33002,SC.Sectionid_2)[Energy Release Rate],
	   dbo.GetPlanProdFieldName(33002) [Energy Release Rate Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33003,SC.Sectionid_2)[Seismic Rating],
	   dbo.GetPlanProdFieldName(33003) [Seismic Rating Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33004,SC.Sectionid_2)[Geological Features],
	   dbo.GetPlanProdFieldName(33004) [Geological Features Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33005,SC.Sectionid_2)[FOG Seismic],
	   dbo.GetPlanProdFieldName(33005) [FOG Seismic Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33006,SC.Sectionid_2)[Brows present],
	   dbo.GetPlanProdFieldName(33006) [Brows presentName], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33007,SC.Sectionid_2)[Face Shape],
	   dbo.GetPlanProdFieldName(33007) [Face Shape Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33008,SC.Sectionid_2)[Crush Pillar Req],
	   dbo.GetPlanProdFieldName(33008) [Crush Pillar Req Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33009,SC.Sectionid_2)[Secondary Support Req],
	   dbo.GetPlanProdFieldName(33009) [Secondary Support Req Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33010,SC.Sectionid_2)[Additional Support Req],
	   dbo.GetPlanProdFieldName(33010) [Additional Support Req Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,33011,SC.Sectionid_2)[Special Area],
	   dbo.GetPlanProdFieldName(33011) [Special Area Name],


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
						PPDA.TemplateID = 33
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Latest_RME_Report_Number_STP				--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 3) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30001,SC.Sectionid_2)[RMR],
	   dbo.GetPlanProdFieldName(30001) [RMR Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30002,SC.Sectionid_2)[Energy Release Rate],
	   dbo.GetPlanProdFieldName(30002) [Energy Release Rate Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30003,SC.Sectionid_2)[Seismic Rating],
	   dbo.GetPlanProdFieldName(30003) [Seismic Rating Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30004,SC.Sectionid_2)[Lead & Lag],
	   dbo.GetPlanProdFieldName(30004) [Lead & Lag Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30005,SC.Sectionid_2)[FOG Seismic],
	   dbo.GetPlanProdFieldName(30005) [FOG Seismic Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30006,SC.Sectionid_2)[Brows present],
	   dbo.GetPlanProdFieldName(30006) [Brows presentName], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30007,SC.Sectionid_2)[Face Shape],
	   dbo.GetPlanProdFieldName(30007) [Face Shape Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30008,SC.Sectionid_2)[Crush Pillar Req],
	   dbo.GetPlanProdFieldName(30008) [Crush Pillar Req Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30009,SC.Sectionid_2)[Secondary Support Req],
	   dbo.GetPlanProdFieldName(30009) [Secondary Support Req Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30010,SC.Sectionid_2)[Additional Support Req],
	   dbo.GetPlanProdFieldName(30010) [Additional Support Req Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,30011,SC.Sectionid_2)[Special Area],
	   dbo.GetPlanProdFieldName(30011) [Special Area Name],

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
						PPDA.TemplateID = 3
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Latest_Survey_Note_Number_DEV			--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 77) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,77001,SC.Sectionid_2)[Stop Note No.],
	   dbo.GetPlanProdFieldName(77001) [Stop Note No. Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,77002,SC.Sectionid_2)[Holing Note No.],
	   dbo.GetPlanProdFieldName(77002) [Holing Note No. Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,77003,SC.Sectionid_2)[Survey Note No.],
	   dbo.GetPlanProdFieldName(77003) [Survey Note No. Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,77004,SC.Sectionid_2)[Adhering to Survey lines ],
	   dbo.GetPlanProdFieldName(77004) [Adhering to Survey lines  Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,77005,SC.Sectionid_2)[Over & Under Breaking],
	   dbo.GetPlanProdFieldName(77005) [Over & Under Breaking Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,77006,SC.Sectionid_2)[Dimentions],
	   dbo.GetPlanProdFieldName(77006) [Dimentions Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,77007,SC.Sectionid_2)[Cubby ( Y/N )],
	   dbo.GetPlanProdFieldName(77007) [Cubby ( Y/N ) Name], 


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
						PPDA.TemplateID = 77
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
GO

Alter Procedure dbo.sp_Latest_Survey_Note_Number_STP				--in
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 7) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,70001,SC.Sectionid_2)[Stop Note No.],
	   dbo.GetPlanProdFieldName(70001) [Stop Note No. Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,70002,SC.Sectionid_2)[Holing Note No.],
	   dbo.GetPlanProdFieldName(70002) [Holing Note No. Name],  
	    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,70003,SC.Sectionid_2)[30m Rule],
	   dbo.GetPlanProdFieldName(70003) [30m Rule Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,70004,SC.Sectionid_2)[Stopping Distance To Pillar],
	   dbo.GetPlanProdFieldName(70004) [Stopping Distance To Pillar Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,70005,SC.Sectionid_2)[Adhering To Gully Lines],
	   dbo.GetPlanProdFieldName(70005) [Adhering To Gully Lines Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,70006,SC.Sectionid_2)[dhering To Limit Lines],
	   dbo.GetPlanProdFieldName(70006) [dhering To Limit Lines Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,70007,SC.Sectionid_2)[Actual Stoping Width],
	   dbo.GetPlanProdFieldName(70007) [Actual Stoping Width Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,70007,SC.Sectionid_2)[  Gully Dept and Distance],
	   dbo.GetPlanProdFieldName(70008) [  Gully Dept and Distance Name], 


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
						PPDA.TemplateID = 7
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
GO

ALTER Procedure [dbo].[sp_Latest_Ventilation_Report_Number_DEV]	--55  
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																					--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 55) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55001,SC.Sectionid_2)[Velocity],
	   dbo.GetPlanProdFieldName(55001) [Velocity Name],
       dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55002,SC.Sectionid_2)[Wet Bulb ],
	   dbo.GetPlanProdFieldName(55002) [Wet Bulb Name],  
	    dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55003,SC.Sectionid_2)[Dry Bulb],
	   dbo.GetPlanProdFieldName(55003) [Dry Bulb Name],  
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55004,SC.Sectionid_2)[V/Controls],
	   dbo.GetPlanProdFieldName(55004) [V/Controls Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55005,SC.Sectionid_2)[Noise],
	   dbo.GetPlanProdFieldName(55005) [Noise Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55006,SC.Sectionid_2)[AU],
	   dbo.GetPlanProdFieldName(55006) [AU Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55007,SC.Sectionid_2)[GDI],
	   dbo.GetPlanProdFieldName(55007) [GDI Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55008,SC.Sectionid_2)[Drills],
	   dbo.GetPlanProdFieldName(55008) [Drills Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55009,SC.Sectionid_2)[Dust],
	   dbo.GetPlanProdFieldName(55009) [Dust Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55010,SC.Sectionid_2)[CH4],
	   dbo.GetPlanProdFieldName(55010) [CH4 Name], 
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55011,SC.Sectionid_2)[Leackages],
	   dbo.GetPlanProdFieldName(55011) [Leackages Name],  
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55012,SC.Sectionid_2)[Heat Sickness Casesq],
	   dbo.GetPlanProdFieldName(55012) [Heat Sickness Cases Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55013,SC.Sectionid_2)[Refuge Bay No],
	   dbo.GetPlanProdFieldName(55013) [Refuge Bay No Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55014,SC.Sectionid_2)[Life Sustainable],
	   dbo.GetPlanProdFieldName(55014) [Life Sustainable Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55015,SC.Sectionid_2)[Distance from Workplace],
	   dbo.GetPlanProdFieldName(55015) [Distance from Workplace Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55016,SC.Sectionid_2)[Hearing Protection],
	   dbo.GetPlanProdFieldName(55016) [Hearing Protection Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55017,SC.Sectionid_2)[Rating],
	   dbo.GetPlanProdFieldName(55017) [Rating Name],


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
						PPDA.TemplateID = 55
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'

GO

ALTER Procedure [dbo].[sp_Latest_Ventilation_Report_Number_STP]	--05  
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 5) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50001,SC.Sectionid_2)[Velocity],
	   dbo.GetPlanProdFieldName(50001) [Velocity Name],
       dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50002,SC.Sectionid_2)[Wet Bulb ],
	   dbo.GetPlanProdFieldName(50002) [Wet Bulb Name],  
	    dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50003,SC.Sectionid_2)[Dry Bulb],
	   dbo.GetPlanProdFieldName(50003) [Dry Bulb Name],  
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50004,SC.Sectionid_2)[V/Controls],
	   dbo.GetPlanProdFieldName(50004) [V/Controls Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50005,SC.Sectionid_2)[Noise],
	   dbo.GetPlanProdFieldName(50005) [Noise Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50006,SC.Sectionid_2)[AU],
	   dbo.GetPlanProdFieldName(50006) [AU Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50007,SC.Sectionid_2)[GDI],
	   dbo.GetPlanProdFieldName(50007) [GDI Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50008,SC.Sectionid_2)[Drills],
	   dbo.GetPlanProdFieldName(50008) [Drills Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50009,SC.Sectionid_2)[Dust],
	   dbo.GetPlanProdFieldName(50009) [Dust Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50010,SC.Sectionid_2)[CH4],
	   dbo.GetPlanProdFieldName(50010) [CH4 Name], 
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50011,SC.Sectionid_2)[Leackages],
	   dbo.GetPlanProdFieldName(50011) [Leackages Name],  
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50012,SC.Sectionid_2)[Heat Sickness Casesq],
	   dbo.GetPlanProdFieldName(50012) [Heat Sickness Cases Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50013,SC.Sectionid_2)[Refuge Bay No],
	   dbo.GetPlanProdFieldName(50013) [Refuge Bay No Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50014,SC.Sectionid_2)[Life Sustainable],
	   dbo.GetPlanProdFieldName(50014) [Life Sustainable Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50015,SC.Sectionid_2)[Distance from Workplace],
	   dbo.GetPlanProdFieldName(50015) [Distance from Workplace Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50016,SC.Sectionid_2)[Hearing Protection],
	   dbo.GetPlanProdFieldName(50016) [Hearing Protection Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50017,SC.Sectionid_2)[Rating],
	   dbo.GetPlanProdFieldName(50017) [Rating Name],


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
						PPDA.TemplateID = 5
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'        --done  

GO

ALTER Procedure [dbo].[sp_PlanProd_WPList]
--Declare
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Numeric(7)
as

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'
--SET @Activity = 0


			SELECT pp.Prodmonth,pp.Workplaceid,w.Description WorkplaceDesc,SC.NAME_2 MO, pp.SQM, isnull(PPP.SQM,0) PrevSQM, pp.ReefSQM, pp.WasteSQM, pp.SW,pp.IdealSW,pp.cw,pp.CMGT,gt = case when pp.ReefTons = 0 then 0 else pp.kg*1000/pp.reeftons end
			,pp.OrgUnitDay,pp.OrgUnitAfternoon,pp.OrgUnitNight, pp.DHeight, pp.DWidth, SCA.BeginDate, SCA.EndDate
            , pp.Metresadvance, ppp.Metresadvance PrevAdv, pp.ReefAdv, pp.WasteAdv, pp.FL, pp.Kg, Name_2, Name_1,Name
			,meas.SQM MeasSQM, meas.ADV MeasAdv
			FROM PLANMONTH  PP 
			INNER JOIN dbo.SECTION_COMPLETE SC ON
			pp.Prodmonth = SC.PRODMONTH AND
			pp.Sectionid = SC.SECTIONID 
			INNER JOIN dbo.SECCAL SCA ON
			SC.Prodmonth = SCA.PRODMONTH AND
			SC.Sectionid_1 = SCA.SECTIONID 
			inner join Workplace W on
			pp.Workplaceid = w.Workplaceid
			LEFT JOIN PLANMONTH  PPP ON
			PPP.Prodmonth = dbo.GetPrevProdMonth(pp.Prodmonth) and
			ppp.Workplaceid = pp.Workplaceid and
			ppp.PlanCode = pp.PlanCode
			left join (
			Select WorkplaceID, Sum(SqmTotal) SQM, SUM(TotalMetres) ADV from SURVEY
			where ProdMonth = dbo.GetPrevProdMonth(@Prodmonth)
			group by WorkplaceID 
			) Meas on
			pp.Workplaceid = meas.WorkplaceID
			WHERE 
			    PP.Prodmonth = @Prodmonth AND
				sc.Sectionid_2 = @SectionID_2 AND
				pp.Activity = @Activity  and
				pp.PlanCode = 'MP'       --done

GO









