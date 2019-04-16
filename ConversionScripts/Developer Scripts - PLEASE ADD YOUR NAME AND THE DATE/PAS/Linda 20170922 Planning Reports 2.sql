
 -- sp_PlanningReportLockedDetail '201709', 'REA'
ALTER Procedure [dbo].[sp_PlanningReportLockedDetail]
@Prodmonth varchar(6),
@SectionID varchar(10)

AS
 
 select 'Planning Detail Report (Locked)' label1, '' banner, 
 @Prodmonth Myprodmonth , 'Total Mine' thesection, Fl,  
 * from (select CONVERT(numeric(11, 0), CMGT) CMGT, CONVERT(numeric(15, 0), AdvCMGT) AdvCMGT, 
 CONVERT(numeric(15, 0), SqmCMGT) SqmCMGT, 
 Vamps, s2reptosecid, mosection, ReptoSecid, MoName, Fl, 
 Adv , 
 SW, CW, SQM*sw SQMSW, reefsqm*cw SQMCW, 
 Reefmetres,    Wastemetres,  ReefSQM, WasteSQM, OldGoldTons, OldGoldContents, Cubics, Dens, BrokenRockDensity, 
 OrgUnitDay, ShiftBossName, MinerName, 
 Totalmetres,   wpDesc, wpID,  SbSecID,MinerSecID, 
 DevOunces, StopeOunces,    TotalOunces,  
 CONVERT(numeric(15, 0), DevContent) DevContent, CONVERT(numeric(15, 0), StopeContent) StopeContent, 
 CONVERT(numeric(15, 0), TotalContent) TotalContent, 
 StopeTons,    DevTons, TotalTons,    
 Facelength,  SQM SQM, AveFAdv from (     
 select CMGT CMGT,0 Vamps, w.Description wpDesc, w.workplaceid wpID, NAME_1 ShiftBossName, SECTIONID_1 SbSecID, 
 sc.SECTIONID MinerSecID, NAME MinerName, OrgUnitDay, pm.FL Fl,
 Adv = case when pm.Activity IN( 0,9) and pm.FL > 0 then pm.SQM/pm.fl else 0 end, 
 pm.SW SW, w.BrokenRockDensity BrokenRockDensity,
 0 OldGoldTons, 0 OldGoldContents, 0 Cubics, case when SQM != 0 then pm.Density else 0 end as Dens, 
 sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid,  
 name_2 MoName,  
 case when pm.Activity = 1 then KG/1000/31.10348 else 0 end as DevOunces,     
 case when pm.Activity IN (0,9)   then KG/1000/31.10348 else 0 end as StopeOunces,   
 KG/1000/31.10348 TotalOunces,    

 case when pm.Activity = 1 then KG*1000 else 0 end as DevContent,     
 case when pm.Activity IN (0,9)   then KG*1000 else 0 end as StopeContent,   
 (KG/1000) TotalContent,    

 case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
 case when pm.activity IN (0,9)   then (ReefTons+WasteTons) else 0 END as StopeTons,     
 (ReefTons+WasteTons) TotalTons, FL Facelength, SQM,     
 case when pm.Activity IN( 0,9) and FL > 0  
  then SQM/FL  else 0 end as AveFAdv, 
case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end as ReefMetres,
 
case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end as WasteMetres,
 
 case when ReefSQM is not null then ReefSQM else 0 end ReefSQM, 
 case when WasteSQM is not null then WasteSQM else 0 end WasteSQM, 
 case when ReefAdv is not null AND pm.Activity = 1 then ReefAdv * CMGT else 0 end AdvCMGT, 
 case when ReefSQM is not null AND pm.Activity IN( 0,9) then ReefSQM * CMGT else 0 end SqmCMGT, 
 ISNULL(CW, 0) CW, 
 case when  pm.activity = 1 then Metresadvance else 0 end as Totalmetres 
 from planmonth pm, section_complete sc, workplace w   where  
 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
 pm.prodmonth = @Prodmonth and sc.SECTIONID_2 = @SectionID and pm.PlanCode = 'LP') a 
 )q  order by SbSecID, MinerSecID, OrgUnitDay, wpDesc 
go

-- [sp_PlanningReportDynamicDetail] '201612', 'REA'
ALTER Procedure [dbo].[sp_PlanningReportDynamicDetail]
--declare
@Prodmonth varchar(6),
@SectionID varchar(10)

AS
--set @Prodmonth =201707
--set @SectionID = 'rea'
select 'Planning Detail Report (Dynamic)' label1, '' banner, 
@ProdMonth Myprodmonth , 'Total Mine' thesection, Fl,  
* 
from
(	select BrokenRockDensity, CONVERT(numeric(11, 0), CMGT) CMGT, CONVERT(numeric(15, 0), AdvCMGT) AdvCMGT,
		CONVERT(numeric(15, 0), SqmCMGT) SqmCMGT, 0 Vamps,
		s2reptosecid, mosection, ReptoSecid, MoName, Fl, Adv, 0 SW, CW, 0 SQMSW, reefsqm * cw SQMCW,
		Reefmetres, Wastemetres, ReefSQM, WasteSQM, OldGoldTons, OldGoldContents, Cubics, Dens, OrgUnitDay, ShiftBossName,
		MinerName,
		Totalmetres, wpDesc, wpID, SbSecID, MinerSecID, DevOunces, StopeOunces, TotalOunces,
		isnull(DevContent, 0) DevContent, isnull(StopeContent, 0) StopeContent, isnull(TotalContent, 0) TotalContent,
		StopeTons, DevTons, TotalTons,
		Facelength, SQM, AveFAdv
	from
	(
		select CMGT = case when pm.activity IN(0, 9) and isnull(pm.ReefSQM, 0) > 0 then pm.CMGT
			when pm.activity = 1 and isnull(pm.ReefAdv, 0) > 0 then pm.CMGT else 0 end,
			0 Vamps, w.Description wpDesc, w.workplaceid wpID, NAME_1 ShiftBossName,
			SECTIONID_1 SbSecID, sc.SECTIONID MinerSecID, NAME MinerName, OrgUnitDay,
			pm.FL Fl, Adv = case when pm.Activity IN(0, 9) and pm.FL > 0 then pm.SQM / pm.FL else 0 end,
			0 BrokenRockDensity,
			0 OldGoldTons, 0 OldGoldContents, 0 Cubics, case when SQM != 0
			then pm.Density else 0 end as Dens, sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 MoName,
			case when pm.Activity = 1 then Kg/1000 / 31.10348 else 0 end as DevOunces,
			case when pm.Activity IN(0, 9)   then Kg/1000 / 31.10348 else 0 end as StopeOunces,
			Kg/1000 / 31.10348 TotalOunces,
			case when pm.Activity = 1 then Kg*1000 else 0 end as DevContent,
			case when pm.Activity IN(0, 9)   then Kg*1000 else 0 end as StopeContent,
			0 TotalContent,
			case when pm.activity = 1   then(ReefTons + WasteTons) else 0 END as DevTons,
			case when pm.activity IN(0, 9)   then  Tons else 0 END as StopeTons,
			0 TotalTons, FL Facelength, SQM,     case when FL > 0
			then SQM / FL  else 0 end as AveFAdv,
			case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste = 'R' THEN DevSec ELSE 0 END else 0 end as ReefMetres,
			case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste = 'W' THEN DevSec ELSE 0 END else 0 end as WasteMetres,
			case when ReefSQM is not null then ReefSQM else 0 end ReefSQM,
			case when WasteSQM is not null then WasteSQM else 0 end WasteSQM,
			case when ReefAdv is not null AND pm.Activity = 1 then ReefAdv * isnull(pm.Cmgt,0) else 0 end AdvCMGT,
			case when pm.Activity IN(0, 9) then isnull(ReefSQM,0) * isnull(pm.Cmgt,0) else 0 end SqmCMGT,
			ISNULL(CW, 0) CW,
			case when  pm.activity = 1 then Metresadvance else 0 end as Totalmetres
		from planmonth pm, section_complete sc, workplace w   where
			pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and
			pm.prodmonth = @ProdMonth and sc.SECTIONID_2 = @SectionID and pm.PlanCode = 'MP'
	) a
) q order by SbSecID, MinerSecID, wpDesc
go
