SELECT MOName, SUM(PlanDaySqm) PlanDaySqm, SUM(BookDaySqm) BookDaySqm, SUM(PlanDayAdv) PlanDayAdv, SUM(BookDayAdv) BookDayAdv
FROM (

	SELECT cast(p.calendardate as varchar(11)) Calendardate, p.prodmonth, pm.OrgUnitDay PlanCrew, 
	'' SurveyCrew,  
	sc.Sectionid_2 MO_Code, sc.Name_2 MOName, sc.Sectionid_1 SBID, sc.Name_1 SBName, sc.sectionid, sc.Name, p.workplaceid, w.description Workplace_name,
	o.levelnumber, ShiftDay = case when ct.WorkingDay = 'N' then 0 else p.ShiftDay end, 
	ct.WorkingDay,
	Activity = case when p.activity = 0 then 'Stoping'
			   when p.activity = 1 then 'Development'
			   when p.activity = 9 then 'Ledging' 
			   else 'Unknown' end,
	case when p.activity = 1 then p.Metresadvance else 0 end PlanDayAdv,
	case when p.activity = 1 then  p.BookMetresadvance  else 0 end BookDayAdv, 
	case when p.activity in (0,9) then  p.sqm  else 0 end PlanDaySqm,
	case when p.activity in (0,9) then  p.ReefSQM  else 0 end PlanDaySqmReef,
	case when p.activity in (0,9) then  p.WasteSQM  else 0 end PlanDaySqmWaste,
	case when p.activity in (0,9) then p.booksqm  else 0 end BookDaysqm, 
	case when p.activity in (0,9) then p.BookReefSQM  else 0 end BookDaySqmReef,
	case when p.activity in (0,9) then p.BookWasteSQM  else 0 end BookDaySqmWaste,
	PlanDayTonsTotal = p.Tons,
	PlanDayTonsReef = p.ReefTons,
	PlanDayTonsWaste = p.WasteTons,
	BookDayTonsTotal = BookTons,
	BookDayTonsReef = BookReefTons,
	BookDayTonsWaste = BookWasteTons,
	PlanDayContent = p.Grams,
	BookDayContent= p.BookGrams,
	0 PlanAdv,
	0 bookadv,
	0 SurveyAdv,
	0 PlanSQM,
	0 BookSqm,
	0 SurveySqm,
	0 PlanTons,
	0 BookTons,
	0 SurveyTons,
	0 PlanContent,
	0 BookContent,
	0 SurveyContent,
	PlanFL = case when ct.Workingday = 'N' then 0 else pm.FL end,
	BookFL = case when ct.Workingday = 'N' then 0 else p.BookFL end,
	0 SurveyFL,
	PlanSW = case when ct.Workingday = 'N' then 0 else pm.SW end,
	BookSW = case when ct.Workingday = 'N' then 0 else p.BookSW end,
	0 SurveySW,
	cpt.Description ProblemGroupCode, p.ProblemID, pr.Description, null HQCat,
	p.SBossNotes,p.CausedLostBlast, 
	LostSqm = case when p.CausedLostBlast = 'Y' then p.Sqm else 0 end, 
	LostMetres = case when p.CausedLostBlast = 'Y' then p.Metresadvance else 0 end,
	ReefWaste = case when pm.ReefWaste = 'R' then 'Reef'
					 when pm.ReefWaste = 'W' then 'Waste' else 'Unknown' end, 
	EndTypeDesc = e.Description
	 from planning  p 
	 Left Join PLANMONTH pm on p.Prodmonth = pm.Prodmonth And p.SectionID = pm.Sectionid And p.WorkplaceID = pm.Workplaceid And p.Activity = pm.Activity And p.PlanCode = pm.PlanCode
	 inner join WORKPLACE w on w.workplaceid = p.workplaceid 
	 left outer join EndType e on w.EndTypeID = e.endTypeID
	 inner join SECTION_COMPLETE sc on sc.prodmonth = p.prodmonth and sc.sectionid = p.sectionid
	 inner join seccal s on  s.ProdMonth = sc.ProdMonth and s.SectionID = sc.Sectionid_1
	 inner join caltype ct on ct.CalendarDate = p.CalendarDate and ct.CalendarCode = s.CalendarCode 
	 inner join oreflowentities o on o.oreflowid = w.oreflowid 
	 left outer join CODE_PROBLEM pr on pr.ProblemID = p.ProblemID And pr.Activity = p.Activity 
	 Left Join PROBLEM_TYPE pt on pr.ProblemID = pt.ProblemID And pt.Activity = pr.Activity
	 Left Join CODE_PROBLEM_TYPE cpt on pt.ProblemTypeID = cpt.ProblemTypeID And pt.Activity = cpt.Activity
	 Inner Join SysSet ss on p.Prodmonth = ss.CurrentProductionMonth
	where p.prodmonth >= '201601' 
	And p.PlanCode = 'MP'
	And p.Calendardate = ss.RUNDATE
	AND (
		sc.SectionID = '[SYS:Mineware.Systems.HarmonyPAS][PM:Section Id]'
		OR sc.Sectionid_1 = '[SYS:Mineware.Systems.HarmonyPAS][PM:Section Id]'
		OR sc.Sectionid_2 = '[SYS:Mineware.Systems.HarmonyPAS][PM:Section Id]'
		OR sc.Sectionid_3 = '[SYS:Mineware.Systems.HarmonyPAS][PM:Section Id]'
		OR sc.Sectionid_4 = '[SYS:Mineware.Systems.HarmonyPAS][PM:Section Id]'
		OR sc.Sectionid_5 = '[SYS:Mineware.Systems.HarmonyPAS][PM:Section Id]'
	)
) a
GROUP BY MOName