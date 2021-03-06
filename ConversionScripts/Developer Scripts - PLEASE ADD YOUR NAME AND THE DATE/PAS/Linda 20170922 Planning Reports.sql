
drop PROCEDURE [dbo].[sp_PlanningReportTotalDynamic]
go
drop PROCEDURE [dbo].[sp_PlanningReportTotalLocked]
go

create PROCEDURE [dbo].[sp_PlanningReportTotalMine]
--declare
@Banner varchar(200), 
@ProdMonth char(6),
@TypeReport varchar(1)

AS
--set @ProdMonth=201707
--set @Banner='Linda'
--set @TypeReport=''

declare @SQL varchar(max)
declare @SQL1 varchar(max)
declare @SQL2 varchar(max)
 
IF (@TypeReport = 'D')
	set @SQL = 'select ''Planning Report (Dynamic)'' label1, '
ELSE
	set @SQL = 'select ''Planning Report (Locked)'' label1, '
set @SQL = @SQL + '
	'''+@Banner+''' banner, 
	'''+@ProdMonth+''' Myprodmonth , ''Total Mine'' thesection,
	MOSection,  MOName, 
	ActDesc, SortOrder,

	Facelength = sum(a.Facelength),
	SQMReef = sum(a.SQMReef),
	SQMWaste = sum(a.SQMWaste),
	Sqm = sum(a.Sqm),
	Adv = sum(a.Adv), 
	ReefMeters = sum(a.ReefMeters), 
	WasteMeters = sum(a.WasteMeters), 
	TotalMeters = sum(a.TotalMeters),
	StopeTons = sum(CONVERT(numeric(15, 5),a.StopeTons)),
	StopeTonsReef = sum(a.StopeTonsReef), 
	StopeTonsWaste = sum(a.StopeTonsWaste),
	TotalTons = sum(a.StopeTons + a.DevTons),
	StopeContent = sum(a.StopeContent), 
	DevTons = sum(CONVERT(numeric(15, 5), a.DevTons)),
	DevTonsReef = sum(a.DevTonsReef), 
	DevTonsWaste = sum(a.DevTonsWaste),
	DevContent = sum(CONVERT(numeric(15, 5), a.DevContent)),
	TotalContent = sum(CONVERT(numeric(15, 5), a.StopeContent + a.DevContent)),
	OldGoldContents = sum(0), 
	OldGoldCubics = sum(0),  
	OldGoldGT = sum(0), 
	OldGoldTons = sum(0), 
	SQMSW = sum(SQMSW),
	SQMCW = sum(SQMCW),
	StpSqmCmgt = sum(StpSqmCmgt),
	DevRAdvCmgt = sum(DevRAdvCmgt),
	StpTonsCmgt = sum(StpTonsCmgt),
	DevTonsCmgt = sum(DevTonsCmgt),
		
	BP_FL = sum(convert(numeric(18,5), BP_Facelength,0)),
	BP_ReefSQM = sum(convert(numeric(18,5), BP_SqmReef,0)),
	BP_WasteSQM = sum(convert(numeric(18,5), BP_SqmWaste,0)),
	BP_Sqm = sum(convert(numeric(18,5), BP_SQM,0)),
	BP_Adv = sum(convert(numeric(18,5), BP_Adv,0)),
	BP_ReefMeters = sum(convert(numeric(18,5), BP_ReefMeters,0)), 
	BP_WasteMeters = sum(convert(numeric(18,5), BP_WasteMeters,0)),	
	BP_TotalMeters = sum(convert(numeric(18,5), BP_TotalMeters,0)),		
	BP_StopeTons = sum(convert(numeric(18,5), BP_StopeTons,0)),  
	BP_StopeTonsReef = sum(convert(numeric(18,5), BP_StopeTonsReef,0)),  
	BP_StopeTonsWaste = sum(convert(numeric(18,5), BP_StopeTonsWaste,0)),   
	BP_StopeContent = sum(convert(numeric(18,5), BP_StopeContent,0)),
	BP_DevTons = sum(convert(numeric(18,5), BP_DevTons,0)),
	BP_DevTonsReef = sum(convert(numeric(18,5), BP_DevTonsReef,0)),
	BP_DevTonsWaste = sum(convert(numeric(18,5), BP_DevTonsWaste,0)),
	BP_DevContent = sum(convert(numeric(18,5), BP_DevContent,0)),
 	BP_SQMSW = sum(convert(numeric(18,5), BP_SQMSW,0)),
	BP_SQMCW = sum(convert(numeric(18,5), BP_SQMCW,0)),
	BP_StpSQMCmgt = sum(convert(numeric(18,5), BP_StpSqmCmgt,0)),
	BP_DevRAdvCmgt = sum(convert(decimal(18,5), BP_DevRAdvCmgt,0)),
	BP_StpTonsCmgt = sum(convert(decimal(18,5), BP_StpTonsCmgt)),
	BP_DevTonsCmgt = sum(convert(decimal(18,5), BP_DevTonsCmgt))
from 
(
--DECLARE @ProdMonth varchar(6)
--SET @ProdMonth= 201707

	select sc.SectionID_2 MOSection, sc.Name_2 MOName, 
		ActDesc = case when pm.Activity = 0 and TargetID = 9 then ''Ledging''   
						when pm.Activity = 1 then ''Develpment''
						else ''Stoping'' end,	
		SortOrder = case when pm.Activity = 0 and TargetID = 9 then ''4''   
						when pm.Activity = 1 then ''2''
						else ''1'' end,
		Facelength = FL, 
		SqmReef = case when pm.activity = 0 then pm.ReefSQM else 0 end,
		SqmWaste = case when pm.activity = 0 then pm.WasteSQM else 0 end,
		SQM = case when pm.activity = 0 then pm.SQM else 0 end,
		Adv = case when pm.activity = 0 and FL > 0 then SQM / FL  else 0 end, 
		ReefMeters = case when pm.activity = 1 then pm.ReefAdv else 0 end,
		WasteMeters = case when pm.activity = 1 then pm.WasteAdv else 0 end,
		TotalMeters	=	case when  pm.activity = 1 then pm.Metresadvance else 0 end,  
		StopeTons = case when pm.activity = 0 then pm.Tons else 0 end,     
		StopeTonsReef = case when pm.activity = 0 then pm.ReefTons else 0 end,     
		StopeTonsWaste = case when pm.activity = 0 then pm.WasteTons else 0 end,  
		StopeContent = 	case when pm.Activity = 0 then (kg * 1000) else 0 end,    
		DevTons = case when pm.activity = 1 then (pm.Tons) else 0 END,   
		DevTonsReef = case when pm.activity = 1 then (pm.ReefTons) else 0 END,   
		DevTonsWaste = case when pm.activity = 1 then (pm.WasteTons) else 0 END,   
		DevContent = case when pm.Activity = 1 then (pm.kg*1000) else 0 end, 	
		OldGoldContents = 0, 
		OldGoldCubics = 0,  
		OldGoldGT = 0, 
		OldGoldTons = 0, 
		SQMSW = pm.SW * pm.SQM ,
		SQMCW = pm.CW * pm.ReefSQM, 
		StpSqmCmgt = pm.Cmgt * pm.ReefSQM,
		DevRAdvCmgt = pm.Cmgt * pm.ReefAdv,
		StpTonsCmgt = case when pm.activity = 0 then pm.Cmgt * pm.ReefTons else 0 end,
		DevTonsCmgt = case when pm.activity = 1 then pm.Cmgt * pm.ReefTons else 0 end,

		BP_Facelength = 0, 
		BP_SqmReef = 0,
		BP_SqmWaste = 0,
		BP_SQM = 0,
		BP_Adv = 0,
		BP_ReefMeters = 0,
		BP_WasteMeters = 0,
		BP_TotalMeters	=	0,
		BP_StopeTons = 0,     
		BP_StopeTonsReef = 0,    
		BP_StopeTonsWaste = 0, 
		BP_StopeContent = 	0,   
		BP_DevTons = 0,   
		BP_DevTonsReef = 0,   
		BP_DevTonsWaste = 0, 
		BP_DevContent = 0, 
		BP_OldGoldContents = 0,  
		BP_OldGoldCubics = 0, 
		BP_OldGoldGT = 0, 
		BP_OldGoldTons = 0, 
		BP_SQMSW = 0,
		BP_SQMCW = 0 ,
		BP_StpSqmCmgt = 0,
		BP_DevRAdvCmgt = 0,
		BP_StpTonsCmgt = 0,
		BP_DevTonsCmgt = 0
	from planmonth pm, SECTION_COMPLETE sc, workplace w   
	where pm.sectionid = sc.sectionid and 
			pm.prodmonth = sc.prodmonth and 
			pm.workplaceid = w.workplaceid and   
			pm.prodmonth = '''+@ProdMonth+''' and '
IF (@TypeReport = 'D')
	 set @SQL1 = ' pm.PlanCode = ''MP'' '
ELSE
	 set @SQL1 = ' pm.PlanCode = ''LP'' '		-- and (pm.OldGoldContents IS not null or pm.OldGoldContents <> 0)
set @SQL1 = @SQL1 + ' 
	union all
	select sc.SectionID_2 MOSection, sc.Name_2 MOName, 
		ActDesc =  ''Develpment'',
		SortOrder = 2,
		Facelength = 0, 
		SqmReef = 0,
		SqmWaste = 0,
		SQM = 0,
		Adv = 0,
		ReefMeters = 0,
		WasteMeters = 0,
		TotalMeters	=	0,
		StopeTons = 0,     
		StopeTonsReef = 0,    
		StopeTonsWaste = 0, 
		StopeContent = 	0,   
		DevTons = 0,   
		DevTonsReef = 0,   
		DevTonsWaste = 0, 
		DevContent = 0, 
		OldGoldContents = 0,  
		OldGoldCubics = 0, 
		OldGoldGT = 0, 
		OldGoldTons = 0, 
		SQMSW = 0,
		SQMCW = 0 ,
		StpSqmCmgt = 0,
		DevRAdvCmgt = 0,
		StpTonsCmgt = 0,
		DevTonsCmgt = 0,

		BP_Facelength = 0, 
		BP_SqmReef = 0,
		BP_SqmWaste = 0,
		BP_SQM = 0,
		BP_Adv = 0,
		BP_ReefMeters = pm.MAdvReef,
		BP_WasteMeters = pm.MAdvWaste,
		BP_TotalMeters = pm.MAdv,
		BP_StopeTons = 0,     
		BP_StopeTonsReef = 0,    
		BP_StopeTonsWaste = 0, 
		BP_StopeContent = 	0,   
		BP_DevTons = pm.Tons,   
		BP_DevTonsReef = pm.TonsReef,   
		BP_DevTonsWaste = pm.TonsWaste, 
		BP_DevContent = Content, 
		BP_OldGoldContents = 0,  
		BP_OldGoldCubics = 0, 
		BP_OldGoldGT = 0, 
		BP_OldGoldTons = 0, 
		BP_SQMSW = 0,
		BP_SQMCW = 0 ,
		BP_StpSqmCmgt = 0,
		BP_DevRAdvCmgt = pm.MAdvReef * pm.CMGT,
		BP_StpTonsCmgt = 0,
		BP_DevTonsCmgt = pm.Tons * pm.CMGT
	from BusinessPlan_Development pm, vw_Section_From_MO sc, workplace w   
	where pm.SectionID = sc.sectionid_2 and 
			pm.prodmonth = sc.prodmonth and 
			pm.workplaceid = w.workplaceid and   
			pm.prodmonth = '''+@ProdMonth +'''

	union all '
set @SQL2 = '

	select sc.SectionID_2 MOSection, sc.Name_2 MOName, 
		ActDesc =  ''Stoping'',
		SortOrder = 1,
		Facelength = 0, 
		SqmReef = 0,
		SqmWaste = 0,
		SQM = 0,
		Adv = 0,
		ReefMeters = 0,
		WasteMeters = 0,
		TotalMeters	=	0,
		StopeTons = 0,     
		StopeTonsReef = 0,    
		StopeTonsWaste = 0, 
		StopeContent = 	0,   
		DevTons = 0,   
		DevTonsReef = 0,   
		DevTonsWaste = 0, 
		DevContent = 0, 
		OldGoldContents = 0,  
		OldGoldCubics = 0, 
		OldGoldGT = 0, 
		OldGoldTons = 0, 
		SQMSW = 0,
		SQMCW = 0 ,
		StpSqmCmgt = 0,
		DevRAdvCmgt = 0,
		StpTonsCmgt = 0,
		DevTonsCmgt = 0,

		BP_Facelength = case when pm.SqmStope > 0 then pm.FL else 0 end, 
		BP_SqmReef = pm.SqmReefStope,
		BP_SqmWaste = pm.SqmWasteStope,
		BP_SQM = pm.SQMStope,
		BP_Adv = case when pm.FL > 0 then pm.SQMStope / pm.FL else 0 end,
		BP_ReefMeters = 0,
		BP_WasteMeters = 0,
		BP_TotalMeters = 0,
		BP_StopeTons = pm.TonsStope,     
		BP_StopeTonsReef = pm.TonsReefStope,    
		BP_StopeTonsWaste = pm.TonsWasteStope, 
		BP_StopeContent = pm.ContentStope,   
		BP_DevTons = 0,   
		BP_DevTonsReef = 0,  
		BP_DevTonsWaste = 0,
		BP_DevContent = 0, 
		BP_OldGoldContents = 0,  
		BP_OldGoldCubics = 0, 
		BP_OldGoldGT = 0, 
		BP_OldGoldTons = 0, 
		BP_SQMSW = pm.SQMStope * pm.SW,
		BP_SQMCW = pm.SQMReefStope * pm.CW,
		BP_StpSqmCmgt = pm.SQMReefStope * pm.CMGT,
		BP_DevRAdvCmgt = 0,
		BP_StpTonsCmgt = pm.TonsReefStope * pm.CMGT,
		BP_DevTonsCmgt = 0
	from BusinessPlan_Stoping pm, vw_Section_From_MO sc, workplace w   
	where pm.SectionID = sc.sectionid_2 and 
			pm.prodmonth = sc.prodmonth and 
			pm.workplaceid = w.workplaceid and   
			pm.prodmonth = '''+@ProdMonth+''' and
			pm.SQMStope > 0

	union all

	select sc.SectionID_2 MOSection, sc.Name_2 MOName, 
		ActDesc = ''Ledging'',
		SortOrder = 4,
		Facelength = 0, 
		SqmReef = 0,
		SqmWaste = 0,
		SQM = 0,
		Adv = 0,
		ReefMeters = 0,
		WasteMeters = 0,
		TotalMeters	=	0,
		StopeTons = 0,     
		StopeTonsReef = 0,    
		StopeTonsWaste = 0, 
		StopeContent = 	0,   
		DevTons = 0,   
		DevTonsReef = 0,   
		DevTonsWaste = 0, 
		DevContent = 0, 
		OldGoldContents = 0,  
		OldGoldCubics = 0, 
		OldGoldGT = 0, 
		OldGoldTons = 0, 
		SQMSW = 0,
		SQMCW = 0 ,
		StpSqmCmgt = 0,
		DevRAdvCmgt = 0,
		StpTonsCmgt = 0,
		DevTonsCmgt = 0,

		BP_Facelength = case when pm.SqmLedge > 0 then pm.FL else 0 end,
		BP_SqmReef = pm.SqmReeflLedge,
		BP_SqmWaste = pm.SqmWasteLedge,
		BP_SQM = pm.SQMLedge,
		BP_Adv = case when pm.FL > 0 then pm.SQMLedge / pm.FL else 0 end,
		BP_ReefMeters = 0,
		BP_WasteMeters = 0,
		BP_TotalMeters = 0,
		BP_StopeTons = pm.TonsLedge,     
		BP_StopeTonsReef = pm.TonsReefLedge,    
		BP_StopeTonsWaste = pm.TonsWasteLedge, 
		BP_StopeContent = pm.ContentLedge,   
		BP_DevTons = 0,   
		BP_DevTonsReef = 0,  
		BP_DevTonsWaste = 0,
		BP_DevContent = 0, 
		BP_OldGoldContents = 0,  
		BP_OldGoldCubics = 0, 
		BP_OldGoldGT = 0, 
		BP_OldGoldTons = 0, 
		BP_SQMSW = pm.SQMLedge * pm.SW,
		BP_SQMCW = pm.SQMReeflLedge * pm.CW,
		BP_StpSqmCmgt = pm.SQMReeflLedge * pm.CMGT,
		BP_DevRAdvCmgt = 0,
		BP_StpTonsCmgt = pm.TonsReefLedge * pm.CMGT,
		BP_DevTonsCmgt = 0
	from BusinessPlan_Stoping pm, vw_Section_From_MO sc, workplace w   
	where pm.SectionID = sc.sectionid_2 and 
			pm.prodmonth = sc.prodmonth and 
			pm.workplaceid = w.workplaceid and   
			pm.prodmonth = '''+@ProdMonth+''' and
			pm.SQMLedge > 0
) a
group by SortOrder, ActDesc, MOSection, MOName  '

exec (@SQL+@SQL1+@SQL2)
--select(@SQL+@SQL1+@SQL2)