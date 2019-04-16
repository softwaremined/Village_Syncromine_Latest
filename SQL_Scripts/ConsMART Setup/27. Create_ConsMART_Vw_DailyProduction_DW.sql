CREATE View [dbo].[Vw_DailyProduction_DW] as 
(
    select m.divisioncode, m.description, p.* 
    from
	   (
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Reef Sqm' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Reef Sqm' And Version = 'Booked' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Content' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Content' And Version = 'Booked' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Waste Sqm' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Waste Sqm' And Version = 'Booked' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Face length' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Face length' And Version = 'Booked' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where UnitOfMeasure = 'cmg/t' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where UnitOfMeasure = 'cmg/t' And Version = 'Booked' And Amount > 0
		  union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Rock Density' Unit, pm.Dens Amount, 'Dynamic Plan' Version
			 --from planning p inner join planmonth pm on p.Prodmonth = pm.Prodmonth and p.WorkplaceID = pm.Workplaceid and p.SectionID = pm.Sectionid and
			 --p.Activity = pm.activity  
			 --where  pm.Dens > 0 and (pm.OldGoldContents = 0 or pm.OldGoldContents is null)
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Rock Density' Unit, pm.Dens Amount, 'Booked' Version
			 --from planning p inner join planmonth pm on p.Prodmonth = pm.Prodmonth and p.WorkplaceID = pm.Workplaceid and p.SectionID = pm.Sectionid and
			 --p.Activity = pm.activity  
			 --where  pm.Dens > 0 and p.BookCode is not null and (pm.OldGoldContents = 0 or pm.OldGoldContents is null)
		  --union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Face Advance' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Face Advance' And Version = 'Booked' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where UnitOfMeasure = 'SW' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'SW' And Version = 'Booked' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where UnitOfMeasure = 'g/t' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where UnitOfMeasure = 'g/t' And Version = 'Booked' And Amount > 0
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Reef m (Cap)' Unit, advreef Amount, 'Dynamic Plan' Version
			 --from planning p left outer join 
			 --(select workplaceid, mine, indicator, MAX(prodmonth) prodmonth from SURVEY where activity = 1 group by workplaceid, mine, indicator) s 
			 --on s.workplaceid = p.workplaceid and s.mine = p.mine 
			 --where Activity = 1 and ( s.Indicator > '1') and advreef > 0
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Reef m (Cap)' Unit, bookadv Amount, 'Booked' Version
			 --from planning p left outer join 
			 --(select workplaceid, mine, indicator, MAX(prodmonth) prodmonth from SURVEY where activity = 1 group by workplaceid, mine, indicator) s 
			 --on s.workplaceid = p.workplaceid and s.mine = p.mine 
			 --where Activity = 1 and ( s.Indicator > '1') and bookadv > 0 and ReefWaste = 'R'
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Waste m (Cap)' Unit, advwaste Amount, 'Dynamic Plan' Version
			 --from planning p left outer join 
			 --(select workplaceid, mine, indicator, MAX(prodmonth) prodmonth from SURVEY where activity = 1 group by workplaceid, mine, indicator) s 
			 --on s.workplaceid = p.workplaceid and s.mine = p.mine 
			 --where Activity = 1 and ( s.Indicator > '1') and advwaste > 0
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Waste m (Cap)' Unit, bookadv Amount, 'Booked' Version
			 --from planning p left outer join 
			 --(select workplaceid, mine, indicator, MAX(prodmonth) prodmonth from SURVEY where activity = 1 group by workplaceid, mine, indicator) s 
			 --on s.workplaceid = p.workplaceid and s.mine = p.mine 
			 --where Activity = 1 and ( s.Indicator > '1') and bookadv > 0 and ReefWaste = 'W'
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Reef m (W/C)' Unit, advreef Amount, 'Dynamic Plan' Version
			 --from planning p 
			 --left outer join 
				--(select workplaceid, mine, indicator, MAX(prodmonth) prodmonth from SURVEY where activity = 1 group by workplaceid, mine, indicator) s on s.workplaceid = p.workplaceid and s.mine = p.mine 
			 --where Activity = 1 and (s.Indicator = null or s.Indicator = '1') and advreef > 0
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Reef m (W/C)' Unit, bookadv Amount, 'Booked' Version
			 --from planning p left outer join 
			 --(select workplaceid, mine, indicator, MAX(prodmonth) prodmonth from SURVEY where activity = 1 group by workplaceid, mine, indicator) s 
			 --on s.workplaceid = p.workplaceid and s.mine = p.mine 
			 --where Activity = 1 and (s.Indicator = null or s.Indicator = '1') and bookadv > 0 and ReefWaste = 'R'
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Waste m (W/C)' Unit, advwaste Amount, 'Dynamic Plan' Version
			 --from planning p left outer join 
			 --(select workplaceid, mine, indicator, MAX(prodmonth) prodmonth from SURVEY where activity = 1 group by workplaceid, mine, indicator) s 
			 --on s.workplaceid = p.workplaceid and s.mine = p.mine 
			 --where Activity = 1 and (s.Indicator = null or s.Indicator = '1') and advwaste > 0
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'Waste m (W/C)' Unit, bookadv Amount, 'Booked' Version
			 --from planning p left outer join 
			 --(select workplaceid, mine, indicator, MAX(prodmonth) prodmonth from SURVEY where activity = 1 group by workplaceid, mine, indicator) s 
			 --on s.workplaceid = p.workplaceid and s.mine = p.mine 
			 --where Activity = 1 and (s.Indicator = null or s.Indicator = '1') and bookadv > 0 and ReefWaste = 'W'
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code = 1 And UnitOfMeasure = 'On Reef Development Value (g/t)' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code = 1 And UnitOfMeasure = 'On Reef Development Value (g/t)' And Version = 'Booked' And Amount > 0
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'On Reef Development Height (m)' Unit, pm.DHeight Amount, 'Dynamic Plan' Version
			 --from planning p  inner join PLANMONTH pm on pm.workplaceid = p.workplaceid and pm.Prodmonth = p.Prodmonth and pm.Sectionid = p.Sectionid and
			 --pm.Activity = p.activity
			 --where p.Activity = 1
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'On Reef Development Height (m)' Unit, bookheight Amount, 'Booked' Version
			 --from planning p  
			 --where Activity = 1 and bookwidth > 0 
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'On Reef Development Width (m)' Unit, pm.DWidth Amount, 'Dynamic Plan' Version
			 --from planning p  inner join PLANMONTH pm on pm.workplaceid = p.workplaceid and pm.Prodmonth = p.Prodmonth and pm.Sectionid = p.Sectionid and
			 --pm.Activity = p.activity
			 --where p.Activity = 1
		  --union
			 --select p.mine, p.workplaceid, p.sectionid, p.prodmonth, p.calendardate, 'On Reef Development Width (m)' Unit, bookwidth Amount, 'Booked' Version
			 --from planning p  
			 --where Activity = 1 and bookwidth > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code = 1 And UnitOfMeasure = 'Dev Reef Tons' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code = 1 And UnitOfMeasure = 'Dev Reef Tons' And Version = 'Booked' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code = 1 And UnitOfMeasure = 'Dev Waste Tons' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code = 1 And UnitOfMeasure = 'Dev Waste Tons' And Version = 'Booked' And Amount > 0

		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Reef Tons' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Reef Tons' And Version = 'Booked' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Waste Tons' And Version = 'Dynamic Plan' And Amount > 0
		  union
			 Select Distinct Operation Mine,WorkplaceId,SectionId,ProdMonth,CalendarDate,UnitOfMeasure Unit,Amount, Version
			 From ConsMART.dbo.ProductionData
			 Where Activity_Code in (0,9) And UnitOfMeasure = 'Stope Waste Tons' And Version = 'Booked' And Amount > 0
	   ) p
    inner join workplace w on w.WorkplaceID = P.WORKPLACEID 
    INNER JOIN (select * from CODE_WPDIVISION where Mine <> 'Tshepong Mine') M ON M.DivisionCode = W.DIVISIONCODE 
)