
-- [sp_AuthLockPlan] 201706, '1.1 - A Dube', 0
 CREATE procedure [dbo].[sp_AuthLockPlan]
 @prodmonth numeric(7,0), @description varchar(100),@activity int
 
 as


 --declare  @prodmonth numeric(7,0), @description varchar(100),@activity int
 --set @prodmonth = 201510
 --set @description = 'Mine Overseer 0312'
 --set @activity =1

 if @activity in (0,3)
 begin


 select prodmonth,NAME,WorkplaceID,WPDescription,SectionID,OrgunitDay,OrgunitAfternoon,OrgunitNight,RomingCrew,Auth,locked,
        cast(PSQM as int) PSQM, cast(LSQM as int) LSQM,PSQM - LSQM DiffSQM,POnSQM,LOnSQM,POffSQM,LOffSQM,
		Pcmgt,Lcmgt,Pcmkgt,Lcmkgt,
		PCubics,LCubics, PCubics - LCubics DiffCubics,
		Pkg,Lkg, Pkg - Lkg Diffkg,PUkg,LUkg, PUkg - LUkg DiffUkg
		  from
 (
 select MP.prodmonth, 
        SC.NAME_2 NAME,
		WP.WorkplaceID,
		WP.[Description] WPDescription,
		MP.SectionID,
		isNull(LP.OrgunitDay,MP.OrgunitDay) OrgunitDay, 
		isNull(LP.OrgunitAfternoon,MP.OrgunitAfternoon) OrgunitAfternoon, 
		isNull(LP.OrgunitNight,MP.OrgunitNight) OrgunitNight, 
		isNull(LP.RomingCrew,MP.RomingCrew) RomingCrew, 
		case when LP.Auth='Y' then cast(1 as bit) when LP.Auth='N' then cast( 0 as bit) end Auth,
		LP.locked,
		isNUll(MP.SQM,0) PSQM,
		CASE WHEN lp.Locked = 1 THEN isNUll(LP.SQM,0) ELSE 0 END LSQM,
		isNull(MP.[ReefSQM],0) POnSQM,
		CASE WHEN lp.Locked = 1 THEN isNull(LP.[ReefSQM],0) ELSE 0 END LOnSQM,
		isNull(MP.[WasteSQM],0) POffSQM,
		CASE WHEN lp.Locked = 1 THEN isNull(LP.[WasteSQM],0) ELSE 0 END LOffSQM,
		isNUll(MP.cmgt,0) Pcmgt,
		isNUll(LP.cmgt,0) Lcmgt,
		isNull(MP.cmkgt,0) Pcmkgt,
		isNull(LP.cmkgt,0) Lcmkgt,
		isNUll(MP.Cubicmetres,0) PCubics,
		isNUll(LP.Cubicmetres,0) LCubics,
	    isNUll(MP.Kg,0) Pkg,
		isNUll(LP.Kg,0) Lkg,
		isNull(MP.UraniumBrokenKg,0) PUkg,
		isNull(LP.UraniumBrokenKg,0) LUkg from 
 planmonth MP
 left join 
 (select * from  planmonth where plancode = 'LP') LP ON
 MP.WorkplaceID = LP.WorkplaceID and
 MP.sectionID = LP.SectionID and
 MP.prodmonth = LP.Prodmonth and
 MP.activity = LP.activity
 inner join Section_Complete SC on
 MP.prodmonth = SC.prodmonth and 
 MP.sectionid = SC.sectionid
 inner join [dbo].[WORKPLACE] WP on
 MP.WorkplaceID = WP.WorkplaceID
 WHERE MP.PlanCode = 'MP' and MP.prodmonth = @prodmonth and MP.activity = @activity and SC.NAME_2  =@description
 ) mainData


end
	
if @activity in (1,7)
begin
SELECT distinct mainData.*,metresadvance - lM diffm2,Plan_Off_M - Lock_Off_M diffoffm, Plan_On_M - Lock_On_M diffm3,PUkg,LUkg,PUkg - LUkg DiffUkg FROM (
 select e.name, a.prodmonth, a.orgunitday, a.orgunitAfternoon, a.orgunitNight, a.RomingCrew , a.IsCubics,
      a.workplaceid, c.description,  a.Activity, 
      a.sectionid, a.metresadvance,case when  c.reefwaste=1 then 'Off Reef' else 'On Reef' end reefwaste, '' AccountCode, ''GG025_TMS , 
      a.ReefAdv Plan_On_M,  a.WasteAdv Plan_Off_M, 
      a.CUBICMETRES Plan_Cubics ,b.locked,
	  CASE WHEN b.Locked = 1 then b.Metresadvance else 0 end   lM, 
	  CASE WHEN b.Locked = 1 then b.ReefAdv else 0 end Lock_On_M , 
	  case when b.Auth='Y' then cast(1 as bit) when b.Auth='N' or b.Auth = '' then cast( 0 as bit) end Auth,  
	   case when  b.Topend='Y' then cast(1 as bit) when  b.Topend='N' then cast( 0 as bit) end Topend,b.CubicMetres Lock_Cubics,
 CASE WHEN b.Locked = 1 then b.WasteAdv ELSE 0 END Lock_Off_M,
 isNull(a.UraniumBrokenKg,0) PUkg,
		isNull(b.UraniumBrokenKg,0) LUkg,
		isNull(a.cmkgt,0) Pcmkgt,
		isNull(b.cmkgt,0) Lcmkgt
       from
      planmonth a
      left join 
 (select * from  planmonth where plancode = 'LP') b ON
 a.WorkplaceID = b.WorkplaceID and
 a.sectionID = b.SectionID and
 a.prodmonth = b.Prodmonth and
 a.activity = b.activity
 left outer join workplAce c on
      a.workplaceid = c.workplaceid
      inner join Section_Complete e on
      a.prodmonth = e.prodmonth
      and a.sectionid = e.sectionid
 inner join [dbo].[WORKPLACE] WP on
 a.WorkplaceID = WP.WorkplaceID
 WHERE a.PlanCode = 'MP' and a.prodmonth = @prodmonth and a.activity = @activity and e.NAME_2  =@description) mainData




	--   SELECT distinct DATA.*,b.locked,CASE WHEN b.Locked = 1 then b.Metresadvance else 0 end   lM, CASE WHEN b.Locked = 1 then b.ReefAdv else 0 end Lock_On_M,b.Prodmonth , case when b.Auth='Y' then cast(1 as bit) when b.Auth='N' or b.Auth = '' then cast( 0 as bit) end Auth,  
	--   case when  b.Topend='Y' then cast(1 as bit) when  b.Topend='N' then cast( 0 as bit) end Topend,b.CubicMetres Lock_Cubics,
 --CASE WHEN b.Locked = 1 then b.WasteAdv ELSE 0 END Lock_Off_M  FROM (
 --select e.name, a.prodmonth, a.orgunitday, a.orgunitAfternoon, a.orgunitNight, a.RomingCrew , a.IsCubics,
 --     a.workplaceid, c.description,  a.Activity, 
 --     a.sectionid, a.metresadvance,case when  c.reefwaste=1 then 'Off Reef' else 'On Reef' end reefwaste,case when C.AccountCode=0 then 'Working' else '1' end AccountCode,case when c.GG025_TMS='S' THEN 'Secondary'  when c.GG025_TMS='M' THEN 'Medium' when c.GG025_TMS='T' THEN 'Tertiary' end GG025_TMS , 
 --     a.ReefAdv Plan_On_M,  a.WasteAdv Plan_Off_M, 
 --     a.CUBICMETRES Plan_Cubics 
 --      from
 --     planmonth a
      
 --     left outer join workplAce c on
 --     a.workplaceid = c.workplaceid
 --     inner join Section_Complete e on
 --     a.prodmonth = e.prodmonth
 --     and a.sectionid = e.sectionid
 --     where
 --     a.Prodmonth = @prodmonth 
 --     and e.NAME_2  = @description
 --     and a.activity in (1,7)  and a.AutoUnPlan is null and a.PlanCode ='MP')DATA LEFT join planmonth b on
 --     DATA.prodmonth = b.prodmonth
 --     and DATA.sectionid = b.sectionid
 --     and DATA.workplaceid = B.workplaceid
 --     and DATA.activity = b.activity and b.PlanCode ='LP'
 --     -- and a.isCubics = ''N''
 --     order by name, description
end

if @activity in(8)
begin
 select distinct data.*, VarPlan = case when lp.Locked = 1 then
       isnull(data.Units,0) - isnull(lp.Units,0)
       else isnull(data.Units,0) end,lp.Locked,
	   LockPlan = case when lp.Locked = 1 then isnull(lp.Units,0) else 0 end ,
      case when lp.Auth=1 then cast(1 as bit) when lp.Auth=0 or lp.Auth = '' then cast( 0 as bit) end Auth,  LockVal = isnull(lp.Units,0)
	  from(
	   select  a.prodmonth, Name, a.Sectionid, A.Workplaceid+':'+b.Description Workplace,a.activity activity1,
      a.orgunitday OrgunitDay,A.Workplaceid,a.OGID,
      a.orgunitAfternoon OrgunitAfternoon,
      a.orgunitNight OrgunitNight, isnull(a.Units,0) Units,
      convert(varchar(2),c.OGID)+':'+C.OGDescription [Activity],
      Unittype = Case
      When c.unitbase = 0 then 'Linear metre'
      When c.unitbase = 1 then 'Squaremetres'
      When c.unitbase = 2 then 'Tons'
      end,
      isnull(a.Units,0) DynamicPlan,
      cast(0 as bit) hasChanged 
     
      from PlanMonth_Oldgold a
      inner join workplce b on
        a.workplaceid = b.workplaceid
      Inner join SECTIONS_COMPLETE sc on
        a.Prodmonth = sc.Prodmonth and
        a.SectionID = sc.SectionID
    
      inner join OLDGOLD_TYPE  c on
        a.OGID = c.OGID
      where
      a.Prodmonth = @prodmonth
      and sc.NAME_2 = @description
	
      and a.activity = 8 and a.plancode='MP')data   LEFT Join PLANMONTH_OLDGOLD Lp on
        data.Prodmonth = lp.Prodmonth and
        data.SectionID = lp.SectionID and
        data.WORKPLACEID = lp.WORKPLACEID and
        data.activity1 = lp.ACTIVITY and
        data.orgunitday = lp.orgunitday and
        data.OGID = lp.OGID and lp.plancode='LP'
      --order by NAME, b.Description

end

if @activity in (2)
begin
	       select distinct data.* ,LockPlan = case when lp.Locked = 1 then isnull(lp.Units,0) else 0 end, 
       VarPlan = case when lp.Locked = 1 then
        isnull(data.Units,0) - isnull(lp.Units,0)
        else isnull(data.Units,0) end,
       case when lp.Auth=1 then cast(1 as bit) when lp.Auth=0 or lp.Auth = '' then cast( 0 as bit) end Auth, lp.Locked,
	    LockVal = isnull(lp.Units,0) 
		from 
	   (
	     select distinct a.prodmonth,Name, a.Sectionid, A.Workplaceid+':'+b.Description Workplace,A.Workplaceid,
       a.Orgunitday,
       Null OrgunitAfternoon,
       a.OrgunitNight,isnull(a.Units,0) Units,
       convert(varchar(5),c.SMID)+':'+C.SMDescription [Activity],a.SMID,a.activity activity1,
       Unittype = Case
       When c.unitbase = 0 then 'Linear metre'
       When c.unitbase = 1 then 'Squaremetres'
       When c.unitbase = 2 then 'Units'
       When c.unitbase = 3 then 'Percent'
       When c.unitbase = 4 then 'Cubic metres'
       When c.unitbase = 5 then 'Tons'
       When c.unitbase = 6 then 'Centimeter Rise'
       end,
       isnull(a.Units,0) DynamicPlan,     
	  cast(0 as bit) hasChanged 
       from PLANMONTH_SUNDRYMINING a
       inner join workplce b on
         a.workplaceid = b.workplaceid
       Inner join SECTION_COMPLETE sc on
         a.Prodmonth = sc.Prodmonth and
         a.SectionID = sc.SectionID   
       inner join SUNDRYMINING_TYPE  c on
         a.SMID = c.SMID
       where
       a.Prodmonth = @prodmonth
      and sc.NAME_2 = @description
       and a.activity = 2 and a.plancode='MP')data LEFT Join PLANMONTH_SUNDRYMINING Lp on
         data.Prodmonth = lp.Prodmonth and
         data.SectionID = lp.SectionID and
         data.WORKPLACEID = lp.WORKPLACEID and
         data.ACTIVITY1 = lp.ACTIVITY and
         data.SMID = lp.SMID And
         data.Orgunitday = lp.Orgunitday and lp.plancode='LP'
end

go

-- [sp_PlanningReportDynamicDetail] '201612', 'REA'
ALTER Procedure [dbo].[sp_PlanningReportDynamicDetail]
@Prodmonth varchar(6),
@SectionID varchar(10)

AS
select 'Planning Detail Report (Dynamic)' label1, '' banner, 
@ProdMonth Myprodmonth , 'Total Mine' thesection, Fl,  
* from(select BrokenRockDensity, CONVERT(numeric(11, 0), CMGT) CMGT, CONVERT(numeric(15, 0), AdvCMGT) AdvCMGT,
CONVERT(numeric(15, 0), SqmCMGT) SqmCMGT, 0 Vamps,
s2reptosecid, mosection, ReptoSecid, MoName, Fl, Adv, 0 SW, CW, 0 SQMSW, reefsqm * cw SQMCW,
Reefmetres, Wastemetres, ReefSQM, WasteSQM, OldGoldTons, OldGoldContents, Cubics, Dens, OrgUnitDay, ShiftBossName,
MinerName,
Totalmetres, wpDesc, wpID, SbSecID, MinerSecID, DevOunces, StopeOunces, TotalOunces,
isnull(DevContent, 0) DevContent, isnull(StopeContent, 0) StopeContent, isnull(TotalContent, 0) TotalContent,
StopeTons, DevTons, TotalTons,
Facelength, SQM, AveFAdv from(
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
case when pm.Activity = 1 then Kg/1000 else 0 end as DevContent,
case when pm.Activity IN(0, 9)   then Kg/1000 else 0 end as StopeContent,
0 TotalContent,
case when pm.activity = 1   then(ReefTons + WasteTons) else 0 END as DevTons,
case when pm.activity IN(0, 9)   then  Tons else 0 END as StopeTons,
0 TotalTons, FL Facelength, SQM,     case when FL > 0
then SQM / FL  else 0 end as AveFAdv,
case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste = 'R' THEN DevSec ELSE 0 END else 0 end as ReefMetres,
case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste = 'W' THEN DevSec ELSE 0 END else 0 end as WasteMetres,
case when ReefSQM is not null then ReefSQM else 0 end ReefSQM,
case when WasteSQM is not null then WasteSQM else 0 end WasteSQM,
case when ReefAdv is not null AND pm.Activity = 1 then ReefAdv * GT else 0 end AdvCMGT,
case when ReefSQM is not null AND pm.Activity IN(0, 9) then ReefSQM * GT else 0 end SqmCMGT,
ISNULL(CW, 0) CW,
case when  pm.activity = 1 then Metresadvance else 0 end as Totalmetres
from planmonth pm, section_complete sc, workplace w   where
pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and
pm.prodmonth = @ProdMonth and sc.SECTIONID_2 = @SectionID and pm.PlanCode = 'MP') a
) q order by SbSecID, MinerSecID, wpDesc
go

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

 case when pm.Activity = 1 then KG/1000 else 0 end as DevContent,     
 case when pm.Activity IN (0,9)   then KG/1000 else 0 end as StopeContent,   
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
 case when ReefAdv is not null AND pm.Activity = 1 then ReefAdv * GT else 0 end AdvCMGT, 
 case when ReefSQM is not null AND pm.Activity IN( 0,9) then ReefSQM * GT else 0 end SqmCMGT, 
 ISNULL(CW, 0) CW, 
 case when  pm.activity = 1 then Metresadvance else 0 end as Totalmetres 
 from planmonth pm, section_complete sc, workplace w   where  
 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
 pm.prodmonth = @Prodmonth and sc.SECTIONID_2 = @SectionID and pm.PlanCode = 'LP') a 
 )q  order by SbSecID, MinerSecID, OrgUnitDay, wpDesc 
go
-- sp_PlanningReportTotalDynamic 'xx', '201709'
ALTER PROCEDURE [dbo].sp_PlanningReportTotalDynamic
@Banner varchar(200), 
@ProdMonth char(6)

AS
 select 'Planning Report (Dynamic)' label1, @Banner banner, 
	 @ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv 
	 from 
	 (
		  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
				Activity = convert(int, sc.Activity), a.ReefMeters, a.wastemeters, a.totalmeters,
				a.DevOunces,  a.StopeOunces,a.TotalOunces,
				CONVERT(numeric(15, 5),a.DevContent)DevContent, 
				CONVERT(numeric(15, 5),a.StopeContent)StopeContent, 
				CONVERT(numeric(15, 5),a.TotalContent)TotalContent,
				CONVERT(numeric(15, 5),a.StopeTons) StopeTons,
				CONVERT(numeric(15, 5),a.DevTons) DevTons,
				CONVERT(numeric(15, 5),a.TotalTons) TotalTons,    
				a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv, 
				a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons,
				a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
				convert(numeric(18,5),bus.budgetReefSQM,0) budgetReefSQM,
				convert(numeric(18,5),bus.budget_WasteSQM,0) budget_WasteSQM,
				convert(numeric(18,5),bus.budget_Sqm,0) budget_Sqm,
				convert(numeric(18,5),bus.BAveFAdv,0) BAveFAdv,
				convert(numeric(18,5),bus.budget_Fl,0) budget_Fl,
				convert(numeric(18,5),bus.budget_StopeTons,0) budget_StopeTons,    
 				convert(numeric(18,5),bus.budget_StopeContent,0) budget_StopeContent,
				convert(numeric(18,5),bus.budget_SQMSW,0) budget_SQMSW,
				convert(numeric(18,5),bus.budget_SQMCW,0) budget_SQMCW,
				convert(decimal(18,5),bus.budget_SQMCMGT,0) budget_SQMCMGT, 
				convert(numeric(18,5),bus.budget_ReefMeters,0) budget_ReefMeters, 
				convert(numeric(18,5),bus.budget_WasteMeters,0) budget_WasteMeters,				
				convert(numeric(18,5),bus.budget_DevTons,0) budget_DevTons,
				convert(numeric(18,5),bus.budget_DevContent,0) budget_DevContent,
				convert(numeric(18,5),bus.budget_DevRMadvCmgt,0) budget_DevRMadvCmgt,
				convert(numeric(18,5),bus.budget_StopeTonsCmgt,0) budget_StopeTonsCmgt, 
				convert(numeric(18,5),bus.budget_DevTonsCmgt,0) budget_DevTonsCmgt ,
				convert(numeric(18,5),bus.budget_DevReefTons ,0) budget_DevReefTons  				
		from 
 		(
			select distinct Sectionid_2, Name_2, Sectionid_3, Name_3, activity 
			from SECTION_COMPLETE, 
				(select Activity = case when Activity = 3 then 2 else Activity end 
				 from code_activity 
				 where Activity in (0,1)
				 ) p 
			where prodmonth = @ProdMonth
		) sc 
		left outer join		
		(
			select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity, 
				isnull(sum(ReefMeters),0) ReefMeters,   
				isnull(sum(WasteMeters),0) WasteMeters,  
				isnull(sum(TotalMeters),0) TotalMeters, 
				isnull(sum(DevOunces),0) DevOunces,  
				isnull(sum(StopeOunces),0) StopeOunces,   
				isnull(sum(TotalOunces),0) TotalOunces,  
				isnull(sum(convert(numeric(15,5),DevContent)),0) DevContent, 
				isnull(sum(convert(numeric(15,5),StopeContent)),0) StopeContent,  
				isnull(sum(convert(numeric(15,5),TotalContent)),0) TotalContent, 
				isnull(sum(StopeTons),0) StopeTons,   
				isnull(sum(DevTons),0) DevTons, 
				isnull(sum(TotalTons),0) TotalTons,    
				isnull(sum(Facelength),0) Facelength, 
				isnull(sum(ReefSQM),0) ReefSQM, 
				isnull(sum(WasteSQM),0) WasteSQM, 
				isnull(sum(Sqm),0) Sqm,   
				isnull(sum(AveFAdv),0) AveFAdv 
				,0 OldGoldContents, 
				0 OldGoldCubics, 
				0 OldGoldGT, 
				0 OldGoldTons,
				sum(SQMReefForCalc)  CMGT
				,isnull(sum(SW),0) SW
				,isnull(sum(CW),0) CW
				,isnull(sum(SQMReefForCalc * GT),0) GTForCalc
				, isnull(sum(SQMSW),0) SQMSW
				, isnull(sum(SQMCW),0) SQMCW
			from 
			(     
				select Sectionid_3 s2reptosecid, Name_3 mosection, Sectionid_2 ReptoSecid, Name_2 sectionname,  
						 case when pm.Activity = 1 and pm.TargetID = 11 then (Kg/1000)/31.10348 else 0 end as DevOunces,     
						 case when pm.Activity = 0  and pm.TargetID = 1 then (Kg/1000)/31.10348 else 0 end as StopeOunces,   
						 (Kg/1000)/31.10348 TotalOunces,    
						 case when pm.activity = 1 and pm.TargetID = 11   then (ReefTons+WasteTons) else 0 END as DevTons,   
						 case when pm.Activity = 1 and pm.TargetID = 11 then (Kg/1000) else 0 end as DevContent, 
						 case when pm.Activity = 0  and pm.TargetID = 1 then (Kg/1000) else 0 end as StopeContent, 
						 (Kg/1000) as TotalContent,   
						 case when pm.activity = 0 and pm.TargetID = 1 then WasteTons when pm.activity = 0 then ReefTons else 0 END as StopeTons,     
						 (ReefTons+WasteTons) as TotalTons, 
						 FL Facelength, ReefSQM 
						 ,case when pm.Activity = 0 and pm.TargetID = 1 then ReefSQM when pm.activity = 1 and pm.TargetID = 11 then (ReefAdv) else 0 end as SQMReefForCalc, 
						 WasteSQM, SQM Sqm,     
						 case when FL > 0 then SQM/FL  else 0 end as AveFAdv, 
						 case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN 
								DevSec ELSE 0 END else 0 end as ReefMeters, 
						 case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end as WasteMeters,  
						 case when  pm.activity = 1 and pm.TargetID = 11 then Metresadvance else 0 end as TotalMeters 
						 ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
						 ,pm.Activity
						 ,pm.SW, pm.CW, pm.GT
						 ,pm.SW * pm.SQM SQMSW
						 ,pm.CW * pm.SQM SQMCW
				from planmonth pm, SECTION_COMPLETE sc, workplace w   
				where pm.sectionid = sc.sectionid and 
					  pm.prodmonth = sc.prodmonth and 
					  pm.workplaceid = w.workplaceid and   
					  pm.prodmonth = @ProdMonth  -- and (pm.OldGoldContents IS null or pm.OldGoldContents = 0)
			) a 
			group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity
		) a on a.ReptoSecID = sc.Sectionid_2 and a.Activity = sc.Activity 
		left outer join  
 		(	
		select Sectionid_3, Name_3, Sectionid_2, Name_2, Act,
budgetReefSQM = max(budgetReefSQM),
				budget_WasteSQM = max(budget_WasteSQM),
				budget_SQM = max(budget_SQM),
				BAveFAdv = max(BAveFAdv),
				budget_FL = max(budget_FL),
				budget_StopeTons = max(budget_StopeTons),			
				budget_StopeContent = max(budget_StopeContent),
				budget_SQMSW = max(budget_SQMSW),
				budget_SQMCW = max(budget_SQMCW),
				budget_SQMCMGT = max(budget_SQMCMGT),			 
				budget_ReefMeters = max(budget_ReefMeters),
				budget_WasteMeters = max(budget_WasteMeters),				 
				budget_DevTons = max(budget_DevTons),	
				budget_DevContent = max(budget_DevContent),
				budget_MadvCmgt = max(budget_MadvCmgt),
				budget_Devcmgt = max(budget_Devcmgt),
				budget_DevRMadvCmgt = max(budget_DevRMadvCmgt),
				budget_StopeTonsCmgt =max(budget_StopeTonsCmgt),
				budget_DevTonsCmgt = max(budget_DevTonsCmgt),
				budget_DevReefTons=max(budget_DevReefTons) from(
			select Sectionid_3, Name_3, sc.Sectionid_2, Name_2, Act,PROJECT_TASK,
				budgetReefSQM = sum(ReefSQM),budget_WasteSQM = sum(WasteSQM) + sum(FaultSQM),
				budget_SQM = sum(ReefSQM) + sum(WasteSQM) + sum(FaultSQM),
				BAveFAdv = case when sum(FL) > 0 then (sum(ReefSQM) + sum(WasteSQM)  + sum(FaultSQM)) / sum(FL) else 0 end,budget_FL = sum(FL), 
				budget_StopeTons = (sum(ReefSQM) + sum(WasteSQM) + sum(FaultSQM)) * sum(Stopewidth)/100 * sum(Density),				
				budget_StopeContent = sum(ReefSQM) / 100 * sum(Density) * sum(CMGT),
				budget_SQMSW = sum(ReefSQM) * sum(Stopewidth),
				budget_SQMCW = sum(0),budget_SQMCMGT = sum(ReefSQM) * sum(CMGT),			 
				budget_ReefMeters = sum(ReefMadv) + sum(ReefCapMAdv), 
				budget_WasteMeters = sum(WasteMadv) + sum(WasteCapMAdv),budget_DevTons =sum(DevTons),
				budget_DevContent = sum(DevGrams) * 1000, budget_MadvCmgt = sum(ReefMadv) * sum(Devcmgt),

	budget_Devcmgt = sum(Devcmgt),budget_DevRMadvCmgt = sum(ReefMadv) * sum(Devcmgt),
				budget_StopeTonsCmgt = ((sum(ReefSQM) + sum(WasteSQM) + sum(FaultSQM)) * sum(Stopewidth)/100 * sum(Density)) *	sum(CMGT),
				budget_DevTonsCmgt = sum(DevTons) * sum(Devcmgt),
				budget_DevReefTons=(sum(ReefMadv)+SUM(ReefCapMadv )) * sum(DevHeight) * sum(DevWidth) * (select top 1 density from workplace)  
			from 
		   (
				select project_no MOID, fiscal_period,PROJECT_TASK,
					left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end) prodmonth,
					Act = 0,
					ReefSQM = case when Bud_Val_Type = 'Reef M2' then Bud_val else 0 end,
					WasteSQM = case when Bud_Val_Type = 'Off Reef M2' then Bud_val else 0 end,
					FaultSQM = case when Bud_Val_Type = 'Fault M2' then Bud_val else 0 end,
					Stopewidth = case when Bud_Val_Type = 'Stoping WIdth' then Bud_Val else 0 end, 
					FL = case when Bud_Val_Type = 'Face Length' then Bud_Val else 0 end,
					cmgt = case when Bud_Val_Type = 'Stoping - Block Val (Conv On Reef cmg/t)' then Bud_Val else 0 end,
					Density = case when Bud_Val_Type = 'Relative Density' then Bud_Val else 0 end,
					ReefMadv = 0,
					WasteMAdv = 0,
					ReefCapMAdv = 0,
					WasteCapMAdv = 0,
					DevTons = 0,					 
					DevGrams = 0,
					Devcmgt = 0,
					DevWidth = 0 ,
					DevHeight =0
				from tm1import  
				where 
				left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end)  = @ProdMonth
				union 
				select project_no MOID, fiscal_period, PROJECT_TASK,
					left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end) prodmonth,
					Act = 1,
					ReefSQM = 0,
					WasteSQM = 0,
					FaultSQM = 0,
					Stopewidth = 0,
					FL = 0,						 
					cmgt = 0,
					Density = case when Bud_Val_Type = 'Relative Density' then Bud_Val else 0 end,
					ReefMadv = case when Bud_Val_Type = 'Reef Ongoing Metres' then Bud_Val else 0 end,
					WasteMAdv = case when Bud_Val_Type = 'Waste Ongoing Metres' then Bud_Val else 0 end,
					ReefCapMAdv = case when Bud_Val_Type = 'Reef Capital Metres' then Bud_Val else 0 end,
					WasteCapMAdv = case when Bud_Val_Type = 'Waste Capital Metres' then Bud_Val else 0 end,
					DevTons = case when Bud_Val_Type = 'Tonnes' then Bud_Val else 0 end,						 										 
					DevGrams = case when Bud_Val_Type = 'Kg' then Bud_Val else 0 end,
					Devcmgt = case when Bud_Val_Type = 'Dev Cmg/t' then Bud_Val else 0 end,		
					DevWidth = case when Bud_Val_Type = 'OP - On Reef Dev Width (m)' then Bud_Val else 0 end,
					DevHeight = case when Bud_Val_Type = 'OP - On Reef Dev Height (m)' then Convert(numeric(10,5), Bud_Val) else 0 end
				from tm1import  
				where 
					left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end)  = @ProdMonth
			) bsd  
			inner join section s on 
				s.OpsplanLink = bsd.MOID and 
				s.prodmonth = bsd.prodmonth 
			inner join (select distinct prodmonth, Sectionid_3, Name_3, Sectionid_2, Name_2 from SECTION_COMPLETE) sc on
				sc.Sectionid_2 = s.sectionid and 
				sc.prodmonth = bsd.prodmonth 
			where bsd.prodmonth = @ProdMonth
			group by bsd.prodmonth, Sectionid_3, Name_3, sc.Sectionid_2, Name_2, Act,PROJECT_TASK)d
			group by Sectionid_3, Name_3, Sectionid_2, Name_2, Act
		) bus on bus.Sectionid_2 = sc.Sectionid_2 and bus.Act = sc.Activity 
		where a.Activity is not null or bus.ACt is not null
	) e 
union
	select 'Planning Report (Dynamic)' label1, @Banner banner, 
		@ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from  (
		select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
		Activity = convert(int, sc.Activity), pm.ReefMeters, pm.wastemeters, pm.totalmeters,
		pm.DevOunces,  pm.StopeOunces,pm.TotalOunces,pm.DevContent,pm.StopeContent,
		pm.TotalContent,pm.StopeTons,pm.DevTons,pm.TotalTons,    
		pm.Facelength,pm.ReefSQM,pm.WasteSQM,pm.Sqm,pm.AveFAdv       
		,pm.OldGoldContents,pm.OldGoldCubics,pm.OldGoldGT,pm.OldGoldTons
		,pm.CMGT,pm.SW,pm.CW,pm.GTForCalc,pm.SQMSW,pm.SQMCW,
		convert(numeric(10,0),bus.budgetReefSQM,0) budgetReefSQM,
		convert(numeric(10,0),bus.budget_WasteSQM,0) budget_WasteSQM,
		convert(numeric(10,0),bus.budget_Sqm,0) budget_Sqm,
		convert(numeric(10,0),bus.BAveFAdv,0) BAveFAdv,
		convert(numeric(10,0),bus.budget_Fl,0) budget_Fl,
		convert(numeric(10,0),bus.budget_StopeTons,0) budget_StopeTons, 
		convert(numeric(10,0),bus.budget_StopeContent,0) budget_StopeContent,
		convert(numeric(10,0),bus.budget_SQMSW,0) budget_SQMSW,
		convert(numeric(10,0),bus.budget_SQMCW,0) budget_SQMCW,
		convert(numeric(10,0),bus.budget_SQMCMGT,0) budget_SQMCMGT,  
 		convert(numeric(10,0),bus.budget_ReefMeters,0) budget_ReefMeters, 
		convert(numeric(10,0),bus.budget_WasteMeters,0) budget_WasteMeters,
		convert(numeric(10,0),bus.budget_DevTons,0) budget_DevTons,		
		convert(numeric(10,0),bus.budget_DevContent,0) budget_DevContent, 
		convert(numeric(10,0),bus.budget_DevRMadvCmgt,0) budget_DevRMadvCmgt,
		budget_StopeTonsCmgt = 0, 
		budget_DevTonsCmgt = 0  ,
		convert(numeric(10,0),bus.budget_DevReefTons,0) budget_DevReefTons   
	from 
 	(
		select distinct Sectionid_2, Name_2, Sectionid_3, Name_3, activity 
		from SECTION_COMPLETE, 
		(
			select Activity = case when Activity = 3 then 2 else Activity end 
			from code_activity where Activity in (0,1)
		) p 
		where prodmonth = @ProdMonth
	) sc 
	left outer join 
	(
		select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity,  
			sum(0) ReefMeters,   sum(0) WasteMeters,  sum(0) TotalMeters, 
			sum(0) DevOunces,  sum(0) StopeOunces,   sum(0) TotalOunces,  
			sum(.000000) DevContent, sum(0.0000) StopeContent,   sum(0.00000) TotalContent,  
			sum(0.00000) StopeTons,   sum(0.00000) DevTons, sum(0.00000) TotalTons,    
			sum(0) Facelength, sum(0) ReefSQM, sum(0) WasteSQM, sum(0) Sqm,   sum(0) AveFAdv 
			,0 OldGoldContents, 0 OldGoldCubics, 
			0 OldGoldGT, 0 OldGoldTons
			,'0' CMGT
			,sum(0) SW
			,sum(0) CW
			,sum(0) GTForCalc
			, sum(0) SQMSW
			, sum(0) SQMCW
		from 
		(     
			select Sectionid_3 s2reptosecid, Name_3 mosection, Sectionid_2 ReptoSecid, Name_2 sectionname,  
				case when pm.Activity = 1 and pm.TargetID = 11 then (kg/1000)/31.10348 else 0 end as DevOunces,     
				case when pm.Activity = 0  and pm.TargetID = 1 then (kg/1000)/31.10348 else 0 end as StopeOunces,   
				(kg/1000)/31.10348 TotalOunces,    
				case when pm.activity = 1 and pm.TargetID = 11   then (ReefTons+WasteTons) else 0 END as DevTons,   
				case when pm.Activity = 1 and pm.TargetID = 11 then (kg/1000) else 0 end as DevContent, 
				case when pm.Activity = 0 and pm.TargetID = 1  then (kg/1000) else 0 end as StopeContent, 
				(kg/1000) as TotalContent,   
				case when pm.activity = 0 and pm.TargetID = 1 then WasteTons when pm.activity = 1 and pm.TargetID = 11 then ReefTons else 0 END as StopeTons,     
				(ReefTons+WasteTons) as TotalTons, 
				FL Facelength, ReefSQM, WasteSQM,SQM Sqm,     
				case when FL > 0 then SQM/FL  else 0 end as AveFAdv, 
				case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste='R' THEN 
						DevSec ELSE 0 END else 0 end as ReefMeters, 
				case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste='W' THEN 
						WasteAdv ELSE 0 END else 0 end as WasteMeters,  
				case when  pm.activity = 1 and pm.TargetID = 11 then Metresadvance else 0 end as TotalMeters, 
				0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons,
				case when pm.Activity = '0' then '0' else pm.Activity end Activity,
				pm.SW, pm.CW, pm.GT,
				pm.SW * pm.SQM SQMSW,
				pm.CW * pm.SQM SQMCW
			from planmonth pm, SECTION_COMPLETE sc, workplace w   
			where pm.sectionid = sc.sectionid and 
					pm.prodmonth = sc.prodmonth and 
					pm.workplaceid = w.workplaceid and   
					pm.prodmonth = @ProdMonth -- and (pm.OldGoldContents IS not null or pm.OldGoldContents <> 0)
		) a 
		group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity
	) pm on pm.ReptoSecID = sc.Sectionid_2 and pm.Activity = sc.Activity
	left outer join  
 	( 
		select Sectionid_3, Name_3, sc.Sectionid_2, Name_2, Act, 
			budgetReefSQM = sum(ReefSQM),
			budget_WasteSQM = sum(0),
			budget_SQM = sum(0),
			BAveFAdv = sum(0),
			budget_FL = sum(0),
			budget_StopeTons = sum(0),
			budget_StopeContent = sum(0),
			budget_SQMCMGT = sum(0),
			budget_SQMCW = sum(0),
			budget_SQMSW = sum(0),
			budget_WasteMeters = sum(0),
			budget_ReefMeters = sum(0),
			budget_DevTons = sum(0),
			budget_DevContent = sum(0),
			budget_DevRMadvCmgt = sum(0),
			budget_DevReefTons= sum(0)				
		from
	 
		( 
			select project_no MOID, fiscal_period, 
				left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end) prodmonth,
				Act = 0,
				ReefSQM = case when Bud_Val_Type = 'Ledge M2' then Bud_Val else 0 end
			from tm1import 
			where 
				left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end)  = @ProdMonth
		) b  
		inner join section s on 
			s.OpsplanLink = b.MOID and s.prodmonth = b.prodmonth 
		inner join (select distinct prodmonth, Sectionid_3, Name_3, Sectionid_2, Name_2 from SECTION_COMPLETE) sc on 
			sc.Sectionid_2 = s.sectionid and sc.prodmonth = b.prodmonth 
		where b.prodmonth = @ProdMonth
		group by b.prodmonth, Sectionid_3, Name_3, sc.Sectionid_2, Name_2, Act
	) bus on bus.Sectionid_2 = sc.Sectionid_2 and bus.Act = sc.Activity
	where pm.Activity is not null 
) e   
go

-- [sp_PlanningReportTotalLocked] 'xx', '201607'
ALTER PROCEDURE [dbo].[sp_PlanningReportTotalLocked]--'xx', '201607'
@Banner varchar(200), 
@ProdMonth char(6)
AS
 select 'Planning Report (Locked)' label1, @Banner banner, 
	 @ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv 
	 from 
	 (
		select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
			Activity = convert(int, sc.Activity), a.ReefMeters, a.wastemeters, a.totalmeters,
			a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,
			a.StopeTons,a.DevTons,a.TotalTons,    
			a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv, 
			a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons,
			a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
			convert(numeric(18,5),bus.budgetReefSQM,0) budgetReefSQM,
			convert(numeric(18,5),bus.budget_WasteSQM,0) budget_WasteSQM,
			convert(numeric(18,5),bus.budget_Sqm,0) budget_Sqm,
			convert(numeric(18,5),bus.BAveFAdv,0) BAveFAdv,
			convert(numeric(18,5),bus.budget_Fl,0) budget_Fl,
			convert(numeric(18,5),bus.budget_StopeTons,0) budget_StopeTons,    
 			convert(numeric(18,5),bus.budget_StopeContent,0) budget_StopeContent,
			convert(numeric(18,5),bus.budget_SQMSW,0) budget_SQMSW,
			convert(numeric(18,5),bus.budget_SQMCW,0) budget_SQMCW,
			convert(numeric(18,5),bus.budget_SQMCMGT,0) budget_SQMCMGT, 
			convert(numeric(18,5),bus.budget_ReefMeters,0) budget_ReefMeters, 
			convert(numeric(18,5),bus.budget_WasteMeters,0) budget_WasteMeters,
			convert(numeric(18,5),bus.budget_DevTons,0) budget_DevTons,
			convert(numeric(18,5),bus.budget_DevContent,0) budget_DevContent,  
 			convert(numeric(18,5),bus.budget_DevRMadvCmgt,0) budget_DevRMadvCmgt,
			convert(numeric(18,5),bus.budget_StopeTonsCmgt,0) budget_StopeTonsCmgt, 
			convert(numeric(18,5),bus.budget_DevTonsCmgt,0) budget_DevTonsCmgt ,
				convert(numeric(18,5),bus.budget_DevReefTons ,0) budget_DevReefTons
			from 
 			(
				select distinct Sectionid_2, Name_2, Sectionid_3, Name_3, activity 
				from SECTION_COMPLETE, 
				(
					select Activity from code_activity where Activity in (0,1)
				) p 
				where prodmonth = @ProdMonth
			) sc 
			left outer join 
			(
				select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity, 
					isnull(sum(ReefMeters),0) ReefMeters,   
					isnull(sum(WasteMeters),0) WasteMeters,  
					isnull(sum(TotalMeters),0) TotalMeters, 
					isnull(sum(DevOunces),0) DevOunces,  
					isnull(sum(StopeOunces),0) StopeOunces,   
					isnull(sum(TotalOunces),0) TotalOunces,  
					isnull(sum(DevContent),0) DevContent, 
					isnull(sum(StopeContent),0) StopeContent,   
					isnull(sum(TotalContent),0) TotalContent,  
					isnull(sum(StopeTons),0) StopeTons,   
					isnull(sum(DevTons),0) DevTons, 
					isnull(sum(TotalTons),0) TotalTons,    
					isnull(sum(Facelength),0) Facelength, 
					isnull(sum(ReefSQM),0) ReefSQM, 
					isnull(sum(WasteSQM),0) WasteSQM, 
					isnull(sum(Sqm),0) Sqm,   
					isnull(sum(AveFAdv),0) AveFAdv, 
					0 OldGoldContents, 
					0 OldGoldCubics, 
					0 OldGoldGT, 
					0 OldGoldTons,
					sum(SQMReefForCalc) CMGT,
					isnull(sum(SW),0) SW,
					isnull(sum(CW),0) CW,
					isnull(sum(SQMReefForCalc * GT),0) GTForCalc,
					isnull(sum(SQMSW),0) SQMSW,
					isnull(sum(SQMCW),0) SQMCW
				from 
				(     
					 select Sectionid_3 s2reptosecid, Name_3 mosection, Sectionid_2 ReptoSecid, Name_2 sectionname,  
						case when pm.Activity = 1 and pm.TargetID = 11 then (Kg/1000)/31.10348 else 0 end as DevOunces,     
						case when pm.Activity = 0 and pm.TargetID = 1  then (Kg/1000)/31.10348 else 0 end as StopeOunces,   
						(Kg/1000)/31.10348 TotalOunces,    
						case when pm.activity = 1 and pm.TargetID = 11  then (ReefTons+WasteTons) else 0 END as DevTons,   
						case when pm.Activity = 1 and pm.TargetID = 11 then (Kg/1000) else 0 end as DevContent,
						case when pm.Activity = 0 and pm.TargetID = 1  then (Kg/1000) else 0 end as StopeContent,
						(Kg/1000) as TotalContent,   
						case when pm.activity = 0 then WasteTons when pm.activity = 0 then ReefTons else 0 END as StopeTons,     
						(ReefTons+WasteTons) as TotalTons, 
						Facelength = case when pm.Activity = 0 and pm.TargetID = 1 then pm.FL end,  
						ReefSQM = case when pm.Activity = 0 and pm.TargetID = 1 then pm.ReefSQM end,
						case when pm.Activity = 0 and pm.TargetID = 1 then ReefSQM 
						when pm.activity = 1 then (ReefAdv) else 0 end as SQMReefForCalc, 
						WasteSQM = case when pm.Activity = 0 and pm.TargetID = 1 then pm.WasteSQM end,
						Sqm = case when pm.Activity = 0 and pm.TargetID = 1 then pm.SQM end,     
						case when FL > 0  
						then SQM/FL  else 0 end as AveFAdv, 
						case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end as ReefMeters, 
						case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end as WasteMeters,  
						case when  pm.activity = 1 and pm.TargetID = 11 then Metresadvance else 0 end as TotalMeters 
						,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
						,pm.Activity
						,pm.SW, pm.CW, pm.GT
						,pm.SW * pm.SQM SQMSW
						,pm.CW * pm.ReefSQM SQMCW
					from planmonth pm, SECTION_COMPLETE sc, workplace w   
					where  
						pm.sectionid = sc.sectionid and 
						pm.prodmonth = sc.prodmonth and 
						pm.workplaceid = w.workplaceid and   
						pm.prodmonth = @ProdMonth  and pm.PlanCode = 'LP'
						--(pm.OldGoldContents IS null or pm.OldGoldContents = 0)
				) a 
				group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity
			) a on a.ReptoSecID = sc.sectionid_2 and a.Activity = sc.Activity 
			left outer join  
 			(	
		select sectionid_3, name_3, sectionid_2, name_2, Act,
budgetReefSQM = max(budgetReefSQM),
				budget_WasteSQM = max(budget_WasteSQM),
				budget_SQM = max(budget_SQM),
				BAveFAdv = max(BAveFAdv),
				budget_FL = max(budget_FL),
				budget_StopeTons = max(budget_StopeTons),			
				budget_StopeContent = max(budget_StopeContent),
				budget_SQMSW = max(budget_SQMSW),
				budget_SQMCW = max(budget_SQMCW),
				budget_SQMCMGT = max(budget_SQMCMGT),			 
				budget_ReefMeters = max(budget_ReefMeters),
				budget_WasteMeters = max(budget_WasteMeters),				 
				budget_DevTons = max(budget_DevTons),	
				budget_DevContent = max(budget_DevContent),
				budget_MadvCmgt = max(budget_MadvCmgt),
				budget_Devcmgt = max(budget_Devcmgt),
				budget_DevRMadvCmgt = max(budget_DevRMadvCmgt),
				budget_StopeTonsCmgt =max(budget_StopeTonsCmgt),
				budget_DevTonsCmgt = max(budget_DevTonsCmgt),
				budget_DevReefTons=max(budget_DevReefTons) from(

				select Sectionid_3, Name_3, sc.Sectionid_2, Name_2, Act, 
				budgetReefSQM = sum(ReefSQM),
				budget_WasteSQM = sum(WasteSQM) + sum(FaultSQM),
				budget_SQM = sum(ReefSQM) + sum(WasteSQM) + sum(FaultSQM),
				BAveFAdv = case when sum(FL) > 0 then (sum(ReefSQM) + sum(WasteSQM)  + sum(FaultSQM)) / sum(FL) else 0 end,
				budget_FL = sum(FL), 
				budget_StopeTons = (sum(ReefSQM) + sum(WasteSQM) + sum(FaultSQM)) * sum(Stopewidth)/100 * sum(Density),				
				budget_StopeContent = sum(ReefSQM) / 100 * sum(Density) * sum(CMGT),
				budget_SQMSW = sum(ReefSQM) * sum(Stopewidth),
				budget_SQMCW = sum(0),
				budget_SQMCMGT = sum(ReefSQM) * sum(CMGT),			 
				budget_ReefMeters = sum(ReefMadv) + sum(ReefCapMAdv), 
				budget_WasteMeters = sum(WasteMadv) + sum(WasteCapMAdv),				 
				budget_DevTons = sum(DevTons),	
				budget_DevContent = sum(DevGrams) * 1000, 
				budget_MadvCmgt = sum(ReefMadv) * sum(Devcmgt),
				budget_Devcmgt = sum(Devcmgt),
				budget_DevRMadvCmgt = sum(ReefMadv) * sum(Devcmgt),
				budget_StopeTonsCmgt = ((sum(ReefSQM) + sum(WasteSQM) + sum(FaultSQM)) * sum(Stopewidth)/100 * sum(Density)) *
										sum(CMGT),
				budget_DevTonsCmgt = sum(DevTons) * sum(Devcmgt),
				budget_DevReefTons=(sum(ReefMadv)+SUM(ReefCapMadv )) * sum(DevHeight) * sum(DevWidth) * (select top 1 density from workplace)  
			from 
(
				select project_no MOID, fiscal_period,PROJECT_TASK,
					left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end) prodmonth,
					Act = 0,
					ReefSQM = case when Bud_Val_Type = 'Reef M2' then Bud_val else 0 end,
					WasteSQM = case when Bud_Val_Type = 'Off Reef M2' then Bud_val else 0 end,
					FaultSQM = case when Bud_Val_Type = 'Fault M2' then Bud_val else 0 end,
					Stopewidth = case when Bud_Val_Type = 'Stoping WIdth' then Bud_Val else 0 end, 
					FL = case when Bud_Val_Type = 'Face Length' then Bud_Val else 0 end,
					cmgt = case when Bud_Val_Type = 'Stoping - Block Val (Conv On Reef cmg/t)' then Bud_Val else 0 end,
					Density = case when Bud_Val_Type = 'Relative Density' then Bud_Val else 0 end,
					ReefMadv = 0,
					WasteMAdv = 0,
					ReefCapMAdv = 0,
					WasteCapMAdv = 0,
					DevTons = 0,					 
					DevGrams = 0,
					Devcmgt = 0,
					DevWidth = 0,
					DevHeight =0 
				from tm1import  
				where 
				left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end)  = @ProdMonth
				union 

				select project_no MOID, fiscal_period,PROJECT_TASK, 
					left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end) prodmonth,
					Act = 1,
					ReefSQM = 0,
					WasteSQM = 0,
					FaultSQM = 0,
					Stopewidth = 0,
					FL = 0,						 
					cmgt = 0,
					Density = case when Bud_Val_Type = 'Relative Density' then Bud_Val else 0 end,
					ReefMadv = case when Bud_Val_Type = 'Reef Ongoing Metres' then Bud_Val else 0 end,
					WasteMAdv = case when Bud_Val_Type = 'Waste Ongoing Metres' then Bud_Val else 0 end,
					ReefCapMAdv = case when Bud_Val_Type = 'Reef Capital Metres' then Bud_Val else 0 end,
					WasteCapMAdv = case when Bud_Val_Type = 'Waste Capital Metres' then Bud_Val else 0 end,
					DevTons = case when Bud_Val_Type = 'Tonnes' then Bud_Val else 0 end,						 										 
					DevGrams = case when Bud_Val_Type = 'Kg' then Bud_Val else 0 end,
					Devcmgt = case when Bud_Val_Type = 'Dev Cmg/t' then Bud_Val else 0 end,		
					DevWidth = case when Bud_Val_Type = 'OP - On Reef Dev Width (m)' then Bud_Val else 0 end,
					DevHeight = case when Bud_Val_Type = 'OP - On Reef Dev Height (m)' then Convert(numeric(10,5), Bud_Val) else 0 end
				from tm1import  
				where 
					left(fiscal_period,4) + 
					(case when right(fiscal_period,3) = 'Sep' then '09'
						when right(fiscal_period,3) = 'Oct' then '10' 
						when right(fiscal_period,3) = 'Nov' then '11'
						when right(fiscal_period,3) = 'Dec' then '12'
						when right(fiscal_period,3) = 'Jan' then '01' 
						when right(fiscal_period,3) = 'Feb' then '02'
						when right(fiscal_period,3) = 'Mar' then '03'
						when right(fiscal_period,3) = 'Apr' then '04'
						when right(fiscal_period,3) = 'May' then '05'
						when right(fiscal_period,3) = 'Jun' then '06'
						when right(fiscal_period,3) = 'Jul' then '07'
						else '08' end)  = @ProdMonth
			) bsd  
			inner join section s on 
				s.OpsplanLink = bsd.MOID and 
				s.prodmonth = bsd.prodmonth 
			inner join (select distinct prodmonth, Sectionid_3, Name_3, Sectionid_2, Name_2 from SECTION_COMPLETE) sc on
				sc.Sectionid_2 = s.sectionid and 
				sc.prodmonth = bsd.prodmonth 
			where bsd.prodmonth = @ProdMonth
			group by bsd.prodmonth, Sectionid_3, Name_3, sc.Sectionid_2, Name_2, Act,PROJECT_TASK)d
			group by Sectionid_3, Name_3, Sectionid_2, Name_2, Act
		) bus on bus.Sectionid_2 = sc.Sectionid_2 and bus.Act = sc.Activity 
		where a.Activity is not null or bus.ACt is not null
	) e 
union
	select 'Planning Report (Locked)' label1, @Banner banner, 
		@ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from  (
		select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
		Activity = convert(int, sc.Activity), pm.ReefMeters, pm.wastemeters, pm.totalmeters,
		pm.DevOunces,  pm.StopeOunces,pm.TotalOunces,pm.DevContent,pm.StopeContent,pm.TotalContent,
		pm.StopeTons,pm.DevTons,pm.TotalTons,    
		pm.Facelength,pm.ReefSQM,pm.WasteSQM,pm.Sqm,pm.AveFAdv, 
		pm.OldGoldContents,pm.OldGoldCubics,pm.OldGoldGT,pm.OldGoldTons,
		pm.CMGT,pm.SW,pm.CW,pm.GTForCalc,pm.SQMSW,pm.SQMCW,
		convert(numeric(10,0),bus.budgetReefSQM,0) budgetReefSQM,
		convert(numeric(10,0),bus.budget_WasteSQM,0) budget_WasteSQM,
		convert(numeric(10,0),bus.budget_Sqm,0) budget_Sqm,
		convert(numeric(10,0),bus.BAveFAdv,0) BAveFAdv,
		convert(numeric(10,0),bus.budget_Fl,0) budget_Fl,
		convert(numeric(10,0),bus.budget_StopeTons,0) budget_StopeTons,    
 		convert(numeric(10,0),bus.budget_StopeContent,0) budget_StopeContent,
		convert(numeric(10,0),bus.budget_SQMSW,0) budget_SQMSW,
		convert(numeric(10,0),bus.budget_SQMCW,0) budget_SQMCW,
		convert(numeric(10,0),bus.budget_SQMCMGT,0) budget_SQMCMGT,
		convert(numeric(10,0),bus.budget_ReefMeters,0) budget_ReefMeters, 
		convert(numeric(10,0),bus.budget_WasteMeters,0) budget_WasteMeters,
		convert(numeric(10,0),bus.budget_DevTons,0) budget_DevTons,		
		convert(numeric(10,0),bus.budget_DevContent,0) budget_DevContent,  
 		convert(numeric(10,0),bus.budget_DevRMadvCmgt,0) budget_DevRMadvCmgt,
		budget_StopeTonsCmgt = 0, 
		budget_DevTonsCmgt = 0   ,
		convert(numeric(10,0),bus.budget_DevReefTons,0) budget_DevReefTons 
	from 
 	(
		select distinct Sectionid_2, Name_2, Sectionid_3, Name_3, activity 
		from Section_complete, 
		(
			select Activity from code_activity where Activity in (0,1)
		) p 
		where prodmonth = @ProdMonth
	) sc 
	left outer join 
	(
		select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity,  
			sum(0) ReefMeters,   sum(0) WasteMeters,  sum(0) TotalMeters, 
			sum(0) DevOunces,  sum(0) StopeOunces,   sum(0) TotalOunces,  
			sum(0.00000) DevContent, sum(0.0000) StopeContent,   sum(0.00000) TotalContent,  
			sum(0.000) StopeTons,   sum(0.000) DevTons, sum(0.000) TotalTons,    
			sum(0) Facelength, sum(0) ReefSQM, sum(0) WasteSQM, sum(0) Sqm,   sum(0) AveFAdv 
			,isnull(sum(OldGoldContents),0) OldGoldContents, isnull(sum(OldGoldCubics),0) OldGoldCubics, isnull(sum(OldGoldGT),0) OldGoldGT, isnull(sum(OldGoldTons),0) OldGoldTons
			,'0' CMGT
			,sum(0) SW
			,sum(0) CW
			,sum(0) GTForCalc
			, sum(0) SQMSW
			, sum(0) SQMCW
		from 
		(     
			select Sectionid_3 s2reptosecid, Name_3 mosection, Sectionid_2 ReptoSecid, Name_2 sectionname,  
				case when pm.Activity = 1 and pm.TargetID = 11 then (kg/1000)/31.10348 else 0 end as DevOunces,     
				case when pm.Activity = 0  and pm.TargetID = 1 then (kg/1000)/31.10348 else 0 end as StopeOunces,   
				(kg/1000)/31.10348 TotalOunces,    
				case when pm.activity = 1 and pm.TargetID = 11   then (ReefTons+WasteTons) else 0 END as DevTons,   
				case when pm.Activity = 1 and pm.TargetID = 11 then (kg/1000) else 0 end as DevContent, 
				case when pm.Activity = 0 and pm.TargetID = 1  then (kg/1000) else 0 end as StopeContent, 
				(kg/1000) as TotalContent,   
				case when pm.activity = 0 and pm.TargetID = 1 then WasteTons 
				when pm.activity = 0 then ReefTons else 0 END as StopeTons,     
				(ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM, WasteSQM, SQM Sqm,     
				case when FL > 0 then SQM/FL  else 0 end as AveFAdv, 
				case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste='R' THEN 
				DevSec ELSE 0 END else 0 end as ReefMeters, 
				case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste='W' THEN 
					DevSec ELSE 0 END else 0 end as WasteMeters,  
				case when  pm.activity = 1 and pm.TargetID = 11 then Metresadvance else 0 end as TotalMeters 
				,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
				,'' Activity
				,pm.SW, pm.CW, pm.GT
				,pm.SW * pm.SQM SQMSW
				,pm.CW * pm.SQM SQMCW
			from planmonth pm, SECTION_COMPLETE sc, workplace w   
			where  
				pm.sectionid = sc.sectionid and 
				pm.prodmonth = sc.prodmonth and 
				pm.workplaceid = w.workplaceid and   
				pm.prodmonth = @ProdMonth and pm.PlanCode = 'LP'
				--(pm.OldGoldContents IS not null or pm.OldGoldContents <> 0)
		) a 
		group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity
	) pm on pm.ReptoSecID = sc.Sectionid_2 and pm.Activity = sc.Activity
	left outer join  
 	( 
		select Sectionid_3, Name_3, sc.Sectionid_2, Name_2, Act, 
			budgetReefSQM = sum(ReefSQM),
			budget_WasteSQM = sum(0),
			budget_SQM = sum(0),
			BAveFAdv = sum(0),
			budget_FL = sum(0),
			budget_StopeTons = sum(0),
			budget_StopeContent = sum(0),
			budget_SQMCMGT = sum(0),
			budget_SQMCW = sum(0),
			budget_SQMSW = sum(0),
			budget_WasteMeters = sum(0),
			budget_ReefMeters = sum(0),
			budget_DevTons = sum(0),
			budget_DevContent = sum(0),
			budget_DevRMadvCmgt = sum(0),
			budget_DevReefTons= sum(0)				
		from
( 
		select project_no MOID, fiscal_period, 
			left(fiscal_period,4) + 
				(case when right(fiscal_period,3) = 'Sep' then '09'
					when right(fiscal_period,3) = 'Oct' then '10' 
					when right(fiscal_period,3) = 'Nov' then '11'
					when right(fiscal_period,3) = 'Dec' then '12'
					when right(fiscal_period,3) = 'Jan' then '01' 
					when right(fiscal_period,3) = 'Feb' then '02'
					when right(fiscal_period,3) = 'Mar' then '03'
					when right(fiscal_period,3) = 'Apr' then '04'
					when right(fiscal_period,3) = 'May' then '05'
					when right(fiscal_period,3) = 'Jun' then '06'
					when right(fiscal_period,3) = 'Jul' then '07'
					else '08' end) prodmonth,
			Act = 0,
			ReefSQM = case when Bud_Val_Type = 'Ledge M2' then Bud_Val else 0 end
		from tm1import 
		where 
			left(fiscal_period,4) + 
				(case when right(fiscal_period,3) = 'Sep' then '09'
					when right(fiscal_period,3) = 'Oct' then '10' 
					when right(fiscal_period,3) = 'Nov' then '11'
					when right(fiscal_period,3) = 'Dec' then '12'
					when right(fiscal_period,3) = 'Jan' then '01' 
					when right(fiscal_period,3) = 'Feb' then '02'
					when right(fiscal_period,3) = 'Mar' then '03'
					when right(fiscal_period,3) = 'Apr' then '04'
					when right(fiscal_period,3) = 'May' then '05'
					when right(fiscal_period,3) = 'Jun' then '06'
					when right(fiscal_period,3) = 'Jul' then '07'
					else '08' end)  = @ProdMonth
		) b  
		inner join section s on 
			s.OpsplanLink = b.MOID and s.prodmonth = b.prodmonth 
		inner join (select distinct prodmonth, Sectionid_3, Name_3, Sectionid_2, Name_2 from SECTION_COMPLETE) sc on 
			sc.Sectionid_2 = s.sectionid and sc.prodmonth = b.prodmonth 
		where b.prodmonth = @ProdMonth
		group by b.prodmonth, Sectionid_3, Name_3, sc.Sectionid_2, Name_2, Act
	) bus on bus.Sectionid_2 = sc.Sectionid_2 and bus.Act = sc.Activity
	where pm.Activity is not null 
) e   
go

