USE PAS_DNK_Syncromine
GO
/****** Object:  StoredProcedure [dbo].[sp_LoadPlanningCycle]    Script Date: 2019/02/25 1:49:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter procedure [dbo].[sp_LoadPlanningCycle](

        @sectionid varchar(20),
		@sectionidMO varchar(20),
		@ProdMonth numeric(7,0),
		@Activity numeric(7,0)
)

as 

--declare --@workplaceID varchar(12),
--        @sectionid varchar(10),
--		@sectionidMO varchar(10),
--		@ProdMonth numeric(7,0),
--		@Activity numeric(7,0)

--set @workplaceID = 'RE007722' 
--set @sectionidMO = 'REA'
--set @sectionid = 'RECAHHA'
--set @ProdMonth = 201905
--set @Activity = 0








select cd.CalendarCode,
       cd.CalendarDate,
	   cd.CycleCode,
	   cd.FL,
	   case when Workingday = 'Y' and  cd.MOCycle is null then DefaultValue 
	        when Workingday = 'N' then 'OFF' else cd.MOCycle
	   end MOCycle,
	   cd.MonthlyReefSQM,
	   cd.MonthlyTotalSQM,
	   cd.MonthlyWatseSQM,
	   cd.ReefSQM,
	   cd.ReefSQMCycle,
	   cd.Sectionid,
	   SQM,
	   case when Workingday = 'Y' and  cd.SQMCycle is null then cast(Round((cd.fl * AdvBlast)  * cc.DayCallPercentage,0)  as numeric(5,0)) else cd.SQMCycle end SQMCycle ,
	   cd.TotalShifts,
	   cd.WasteSQM,
	   cd.WasteSQMCycle,
	   cd.Workingday,
	   cd.Workplaceid,
       case when Workingday = 'Y' then ShiftDay else -1 end ShiftDay,
	    rd.Name DefaultName,
		rd.AdvBlast,
		 case when Workingday = 'Y' then DefaultValue  else 'OFF' end DefaultValue,
		case when Workingday = 'Y' and cc.CanBlast = 1 then  cast(Round((cd.fl * AdvBlast)  * cc.DayCallPercentage,0)  as numeric(5,0))   else 0 end DailyDefaultBlastValue,
		 CC.*

 from (

select pm.Sectionid,pm.Workplaceid,pm.SQM,pm.ReefSQM,pm.WasteSQM,pm.FL,pm.MonthlyReefSQM,pm.MonthlyWatseSQM,pm.MonthlyTotalSQM,
       dbo.GetCycleCode(p.WorkplaceID,@sectionidMO,@Activity) CycleCode,
	   sec.TotalShifts,
	   ct.CalendarDate,
	   ct.Workingday,
	   case when ct.Workingday = 'N' then 'OFF' else ct.CalendarCode end  CalendarCode,
	   case when ct.Workingday = 'N' then 0 else p.SQM end  SQMCycle,
	   case when ct.Workingday = 'N' then 0 else p.ReefSQM end ReefSQMCycle, 
	   case when ct.Workingday = 'N' then 0 else p.WasteSQM end WasteSQMCycle,
	   case when ct.Workingday = 'N' then 'OFF' else p.MOCycle end MOCycle

 from PLANMONTH pm
 inner join SECTION_COMPLETE sc on pm.Prodmonth = sc.Prodmonth and pm.SectionID = sc.Sectionid
 inner join SECCAL sec on sc.Prodmonth = sec.Prodmonth and sc.Sectionid_1 = sec.Sectionid
 inner join CALTYPE ct on ct.CalendarCode = sec.CalendarCode
 left join PLANNING p on pm.Workplaceid = p.WorkplaceID and 
                         p.SectionID= pm.Sectionid and 
						 p.Prodmonth = pm.Prodmonth and 
						 p.Activity = pm.Activity and 
						 p.PlanCode = pm.PlanCode and
						 CONVERT(VARCHAR(10), p.Calendardate, 103)  = CONVERT(VARCHAR(10), ct.CalendarDate , 103) 
						 
where pm.Prodmonth = @ProdMonth and 
      pm.PlanCode = 'MP' and 
	  pm.Sectionid in (select SectionID from SECTION_COMPLETE where Sectionid_2 = @sectionidMO and Prodmonth = @ProdMonth) and 
	  pm.Activity = @Activity and
	  ct.CalendarDate between sec.BeginDate and sec.EndDate 
	  
	  ) cd
left join

(  select distinct sc.Sectionid,ct.CalendarDate,sec.CalendarCode, ROW_NUMBER() OVER(PARTITION BY sc.Sectionid ORDER BY sc.Sectionid ) AS ShiftDay from SECTION_COMPLETE sc
	   inner join SECCAL sec on sc.Prodmonth = sec.Prodmonth and sc.Sectionid_1 = sec.Sectionid 
	   inner join CALTYPE ct on ct.CalendarCode = sec.CalendarCode
	  where 
	  sc.Sectionid_2 = @sectionidMO and sc.Prodmonth = @ProdMonth and  ct.CalendarDate between sec.BeginDate and sec.EndDate and Workingday = 'Y') sd
	  on cd.CalendarCode = sd.CalendarCode and cd.CalendarDate = sd.CalendarDate and cd.Sectionid = sd.SectionID
left join 
(select Name, fl,AdvBlast,Type,
  DayCol,
  CAST(SUBSTRING(DayCol,4,2) as numeric(5,0)) ShiftDayStd,
  DefaultValue
from [dbo].[Code_Cycle_RawData]
unpivot
(
  DefaultValue
  for DayCol in (Day1, Day2, day3,day4,day5,day6,day7,day8,day9,day10,day11,day12,day13,day14,day15,day16,day17,day18,day19,day20,day21,day23,day24,day25)
) unpiv
) rd
on cd.FL = rd.FL and sd.ShiftDay = rd.ShiftDayStd and  cd.CycleCode = rd.Name
left join 
 
[CODE_CYCLE]  CC on
rd.DefaultValue = cc.CycleCode
order by Sectionid,Workplaceid,CalendarDate







