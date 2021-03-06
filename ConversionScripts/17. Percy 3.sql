-- [sp_DailyGradeReport] '2017-07-10', 2, 'Y', 1330, 860, 'T'
ALTER Procedure [dbo].[sp_DailyGradeReport]
--Declare
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int,
@TopPanels varchar(1)

AS
if @TopPanels = 'G'
Begin

Select 
Host_Name(), 
ROW_NUMBER() OVER(ORDER BY MO Desc, Description Desc) AS RowID,
Detail = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then '      '+Description
when isnull(MO,'') <> '' and isnull(Description,'') = '' then '  Total '+MO
end,
Flag = 
Case 
when isnull(Description,'') <> '' then 1
when isnull(Description,'') = '' then 2
else 3
end,
Top10 = Case when isnull(Description,'') <> '' then cast(Top10 as Varchar(10)) else '' end,
ShiftNo = Case when isnull(Description,'') <> '' then cast(ShiftNo as Varchar(10)) else '' end,
TotalShifts = Case when isnull(Description,'') <> '' then cast(TotalShifts as Varchar(10)) else '' end,
LockPlan_SQM,
LockPlan_Tonnes,
LockPlan_cmgt,
LockPlan_gt,
LockPlan_Kg,

DynPlan_SQM,
DynPlan_Tonnes,
DynPlan_cmgt,
DynPlan_gt,
DynPlan_Kg,

DynDayPlan_SQM,
DynDayPlan_Tonnes,
DynDayPlan_cmgt,
DynDayPlan_gt,
DynDayPlan_Kg,

DayBook_SQM,
DayBook_Tonnes,
DayBook_cmgt,
DayBook_gt,
DayBook_Kg,

DynProgPlan_SQM,
DynProgPlan_Tonnes,
DynProgPlan_cmgt,
DynProgPlan_gt,
DynProgPlan_Kg,

ProgBook_SQM,
ProgBook_Tonnes,
ProgBook_cmgt,
ProgBook_gt,
ProgBook_Kg,

ProgVar_SQM = ProgBook_SQM-DynProgPlan_SQM,
ProgVar_Tonnes = ProgBook_Tonnes-DynProgPlan_Tonnes,
ProgVar_cmgt = ProgBook_cmgt-DynProgPlan_cmgt,
ProgVar_gt = ProgBook_gt-DynProgPlan_gt,
ProgVar_Kg = ProgBook_Kg-DynProgPlan_Kg,

ForeCast_SQM = case when @DefaultShift = 'Y' then ProgBook_SQM/ShiftNo * TotalShifts
					else ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo) end,
ForeCast_Tonnes = case when @DefaultShift = 'Y' then ProgBook_Tonnes/ShiftNo * TotalShifts
						else ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo) end,


ForeCast_cmgt = case when ProgBook_SQMDens = 0 then 0 
				 when @DefaultShift = 'Y' then
					((ProgBook_Kg * 1000) / ShiftNo * TotalShifts * 100) / (ProgBook_SQMDens / ShiftNo * TotalShifts * 100)
				else  ((ProgBook_Kg * 1000) / ShiftNo * (TotalShifts - @ShiftsNo) * 100) / 
					 (ProgBook_SQMDens / ShiftNo * (TotalShifts - @ShiftsNo) * 100)
				end,

ForeCast_gt = Case when ProgBook_Tonnes = 0 then 0 
				when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * TotalShifts)
				else ((ProgBook_SQM/ShiftNo * (TotalShifts - @ShiftsNo))*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo))
end,
ForeCast_Kg = case when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/1000
					else ((ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo))*DynPlan_cmgt/100*2.75)/1000
				end
,@TheDate TheDate,
DynProgPlan_SW,
ProgBook_SW,
DynProgPlan_SQMSW,
ProgBook_SQMSW,
Colour = cast((case when DynPlan_cmgt > @PayLimit and ProgBook_SQM = 0  then 1   
						when ProgBook_SQM < DynProgPlan_SQM and DynPlan_cmgt > @PayLimit then 2  
						 when  ProgBook_SQM > DynProgPlan_SQM and DynPlan_cmgt < @CutOffGrade then 3  
						 else 0  end) as varchar(1)),
OrderBy = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 'Total '+MO
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 'Total '+MO
else 'XXXXX'
end,
ProgBook_SQMDens
From
(
Select 
MO,
Description,
ShiftNo = Min(ShiftNo),
TotalShifts = Convert(Numeric(7,0), Min(TotalShifts)),
LockPlan_SQM  = Sum(LockPlan_SQM),
LockPlan_SQMDens  = Sum(LockPlan_SQMDens),
LockPlan_Tonnes = Sum(LockPlan_Tonnes),
LockPlan_cmgt = max(LockPlan_cmgt),
LockPlan_gt = Case When Sum(LockPlan_Tonnes) = 0 then 0 else Sum(LockPlan_Grams)/Sum(LockPlan_Tonnes) end,
LockPlan_Kg = Sum(LockPlan_Grams)/1000,

DynPlan_SQM  = Sum(DynPlan_SQM),
DynPlan_SQMDens  = Sum(DynPlan_SQMDens),
DynPlan_Tonnes = Sum(DynPlan_Tonnes),
DynPlan_cmgt = max(DynPlan_CMGT),
DynPlan_gt = Case When Sum(DynPlan_Tonnes) = 0 then 0 else Sum(DynPlan_Grams)/Sum(DynPlan_Tonnes) end,
DynPlan_Kg = Sum(DynPlan_Grams)/1000,

DynDayPlan_SQM  = Sum(DynDayPlan_SQM),
DynDayPlan_SQMDens  = Sum(DynDayPlan_SQMDens),
DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
DynDayPlan_cmgt = Case When Sum(DynDayPlan_SQMDens) = 0 then 0 else Sum(DynDayPlan_Grams*100)/Sum(DynDayPlan_SQMDens) end,
DynDayPlan_gt = Case When Sum(DynDayPlan_Tonnes) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_Tonnes) end,
DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

DayBook_SQM  = Sum(DayBook_SQM),
DayBook_Tonnes = Sum(DayBook_Tonnes),
DayBook_cmgt = Case When Sum(DayBook_SQMDens) = 0 then 0 else Sum(DayBook_Grams*100)/Sum(DayBook_SQMDens) end,
DayBook_gt = Case When Sum(DayBook_Tonnes) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_Tonnes) end,
DayBook_Kg = Sum(DayBook_Grams)/1000,

DynProgPlan_SQM  = Sum(DynProgPlan_SQM),
DynProgPlan_SQMDens  = Sum(DynProgPlan_SQMDens),
DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
DynProgPlan_cmgt = Case When Sum(DynProgPlan_SQMDens) = 0 then 0 else Sum(DynProgPlan_Grams*100)/Sum(DynProgPlan_SQMDens) end,
DynProgPlan_gt = Case When Sum(DynProgPlan_Tonnes) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_Tonnes) end,
DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

ProgBook_SQM  = Sum(ProgBook_SQM),
ProgBook_Tonnes = Sum(ProgBook_Tonnes),
ProgBook_cmgt = Case When Sum(ProgBook_SQMDens) = 0 then 0 else Sum(ProgBook_Grams*100)/Sum(ProgBook_SQMDens) end,
ProgBook_gt = Case When Sum(ProgBook_Tonnes) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_Tonnes) end,
ProgBook_Kg = Sum(ProgBook_Grams)/1000,
ProgBook_SQMDens = Sum(ProgBook_SQMDens),
[Top10] = Sum([Top10]),
DynProgPlan_SW = Case When Sum(DynProgPlan_SQM) = 0 then 0 else Sum(DynProgPlan_SQMSW)/Sum(DynProgPlan_SQM) end,
ProgBook_SW = Case When Sum(ProgBook_SQM) = 0 then 0 else Sum(ProgBook_SQMSW)/Sum(ProgBook_SQM) end,
DynProgPlan_SQMSW  = Sum(DynProgPlan_SQMSW),
ProgBook_SQMSW  = Sum(ProgBook_SQMSW)

from 
(
Select 
MO = '',
e.Description,
ShiftNo = sum(Case when d.Workingday = 'Y' then 1 else 0 end),
TotalShifts = Avg(TotalShifts),

LockPlan_SQM = Isnull(max(Isnull(LP.SQM,0)),0),
LockPlan_CMGT =Isnull(max(LP.CMGT) ,0),
LockPlan_SQMDens = Isnull(max(Isnull(LP.ReefSQM*e.density,0)),0),
LockPlan_Tonnes = Isnull(max(LP.SQM * (LP.SW / 100) * e.Density) ,0),
LockPlan_Grams = Isnull(max(LP.CMGT * LP.ReefSQM / 100 * e.Density) ,0),

DynPlan_SQM = Isnull(max(a.SQM) ,0),
DynPlan_CMGT =Isnull(max(a.CMGT) ,0),
DynPlan_SQMDens = Isnull(max(Isnull(a.ReefSQM*e.density,0)),0),
DynPlan_Tonnes = Isnull(max(a.SQM * (a.SW / 100) * e.Density) ,0),
DynPlan_Grams = Isnull(max(a.CMGT * a.ReefSQM / 100 * e.Density) ,0),

DynProgPlan_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.SQM,0) end),
DynProgPlan_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*e.density,0) end),
DynProgPlan_SQMSW = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*CY.SW,0) end),
DynProgPlan_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				 Isnull(CY.SQM * (CY.SW/100) * e.density,0) end),
DynProgPlan_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.ReefSQM*(CY.CMGT/100)*e.Density,0) end),

ProgBook_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookSQM,0) end),
ProgBook_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookReefSQM*e.density,0) end),
ProgBook_SQMSW = Sum(Case when d.Calendardate <= @TheDate then Isnull(CY.BookReefSQM*CY.BookSW,0) end),
ProgBook_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				Isnull(CY.BookSQM * (CY.SW/100) * e.Density ,0) end),
ProgBook_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*e.Density,0) end),

DynDayPlan_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.SQM,0) end),
DynDayPlan_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.ReefSQM*e.density,0) end),
DynDayPlan_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.Sqm * (CY.SW/100) * e.density,0) end),
DynDayPlan_Grams = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.ReefSQM*(CY.CMGT/100)*e.Density,0) end),

DayBook_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookSQM,0) end),
DayBook_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookReefSQM*e.density,0) end),
DayBook_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookSQM * (CY.SW/100) * e.Density ,0) end),
DayBook_Grams = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*e.Density,0) end),

TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

--,*
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
left join planmonth LP on
a.prodmonth = lp.prodmonth and
a.sectionid = lp.sectionID and
a.Workplaceid = lp.Workplaceid and
a.Activity = lp.Activity and
lp.PlanCode = 'LP' and
lp.Locked = 1
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
inner join caltype d on 
c.Calendarcode = d.Calendarcode and
c.BeginDate <= d.Calendardate and
c.enddate >= d.Calendardate
Inner join Workplace e on
a.WorkplaceID = e.WorkplaceID
left join Planning CY on
a.prodmonth = CY.prodmonth and
a.sectionid = CY.sectionID and
a.Workplaceid = CY.Workplaceid and
a.Activity = CY.Activity and
a.PLancode = CY.PLancode and
d.CalendarDate = CY.Calendardate
--inner join TOPPANELSSELECTED tp
--on e.WorkplaceID = tp.WorkplaceID
Left Join 
(Select top 10 a.* from PLanmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
where Activity = 0
and a.Locked = 1
and isnull(a.IsStopped,'N') ='N'
and auth = 'Y'
order by KG desc) [Top] on
a.prodmonth = [Top].prodmonth and
a.sectionid = [Top].sectionID and
a.Workplaceid = [Top].Workplaceid and
a.Activity = [Top].Activity and
a.PLancode = [Top].PLancode 

where a.Activity in (0,3)
and d.Calendardate <= @TheDate
--and isnull(a.IsStopped,'N') ='N'

group by b.sectionid_2,e.Description) Main
Group by
MO,
Description
with rollup
) Final
end

if @TopPanels = 'T'
begin
Select 
Host_Name(), 
ROW_NUMBER() OVER(ORDER BY MO Desc, Description Desc) AS RowID,
Detail = Case 
when isnull(Description,'') <> '' then '      '+Description
when isnull(Description,'') <> '' then '      '+Description
when isnull(Description,'') = '' then '  Total '+MO
end,
Flag = 
Case 
when isnull(Description,'') <> '' then 1
when isnull(Description,'') = '' then 2
else 3
end,
Top10 = Case when isnull(Description,'') <> '' then cast(Top10 as Varchar(10)) else '' end,
ShiftNo = Case when isnull(Description,'') <> '' then cast(ShiftNo as Varchar(10)) else '' end,
TotalShifts = Case when isnull(Description,'') <> '' then cast(TotalShifts as Varchar(10)) else '' end,
LockPlan_SQM,
LockPlan_Tonnes,
LockPlan_cmgt,
LockPlan_gt,
LockPlan_Kg,

DynPlan_SQM,
DynPlan_Tonnes,
DynPlan_cmgt,
DynPlan_gt,
DynPlan_Kg,

DynDayPlan_SQM,
DynDayPlan_Tonnes,
DynDayPlan_cmgt,
DynDayPlan_gt,
DynDayPlan_Kg,

DayBook_SQM,
DayBook_Tonnes,
DayBook_cmgt,
DayBook_gt,
DayBook_Kg,

DynProgPlan_SQM,
DynProgPlan_Tonnes,
DynProgPlan_cmgt,
DynProgPlan_gt,
DynProgPlan_Kg,

ProgBook_SQM,
ProgBook_Tonnes,
ProgBook_cmgt,
ProgBook_gt,
ProgBook_Kg,

ProgVar_SQM = ProgBook_SQM-DynProgPlan_SQM,
ProgVar_Tonnes = ProgBook_Tonnes-DynProgPlan_Tonnes,
ProgVar_cmgt = ProgBook_cmgt-DynProgPlan_cmgt,
ProgVar_gt = ProgBook_gt-DynProgPlan_gt,
ProgVar_Kg = ProgBook_Kg-DynProgPlan_Kg,

ForeCast_SQM = case when @DefaultShift = 'Y' then ProgBook_SQM/ShiftNo * TotalShifts
					else ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo) end,
ForeCast_Tonnes = case when @DefaultShift = 'Y' then ProgBook_Tonnes/ShiftNo * TotalShifts
						else ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo) end,


ForeCast_cmgt = case when ProgBook_SQMDens = 0 then 0 
				 when @DefaultShift = 'Y' then
					((ProgBook_Kg * 1000) / ShiftNo * TotalShifts * 100) / (ProgBook_SQMDens / ShiftNo * TotalShifts * 100)
				else  ((ProgBook_Kg * 1000) / ShiftNo * (TotalShifts - @ShiftsNo) * 100) / 
					 (ProgBook_SQMDens / ShiftNo * (TotalShifts - @ShiftsNo) * 100)
				end,

ForeCast_gt = Case when ProgBook_Tonnes = 0 then 0 
				when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * TotalShifts)
				else ((ProgBook_SQM/ShiftNo * (TotalShifts - @ShiftsNo))*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo))
end,
ForeCast_Kg = case when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/1000
					else ((ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo))*DynPlan_cmgt/100*2.75)/1000
				end
,@TheDate TheDate,
DynProgPlan_SW,
ProgBook_SW,
DynProgPlan_SQMSW,
ProgBook_SQMSW,
Colour = cast((case when DynPlan_cmgt > @PayLimit and ProgBook_SQM = 0  then 1   
						when ProgBook_SQM < DynProgPlan_SQM and DynPlan_cmgt > @PayLimit then 2  
						 when  ProgBook_SQM > DynProgPlan_SQM and DynPlan_cmgt < @CutOffGrade then 3  
						 else 0  end) as varchar(1)),
OrderBy = 
Case 
when isnull(Description,'') <> '' then 'Total '+MO
when isnull(Description,'') = '' then 'Total '+MO
else 'XXXXX'
end,
ProgBook_SQMDens
From
(
Select 
MO,
Description,
ShiftNo = Min(ShiftNo),
TotalShifts = Convert(Numeric(7,0), Min(TotalShifts)),
LockPlan_SQM  = Sum(LockPlan_SQM),
LockPlan_SQMDens  = Sum(LockPlan_SQMDens),
LockPlan_Tonnes = Sum(LockPlan_Tonnes),
LockPlan_cmgt = max(LockPlan_cmgt),
LockPlan_gt = Case When Sum(LockPlan_Tonnes) = 0 then 0 else Sum(LockPlan_Grams)/Sum(LockPlan_Tonnes) end,
LockPlan_Kg = Sum(LockPlan_Grams)/1000,

DynPlan_SQM  = Sum(DynPlan_SQM),
DynPlan_SQMDens  = Sum(DynPlan_SQMDens),
DynPlan_Tonnes = Sum(DynPlan_Tonnes),
DynPlan_cmgt = max(DynPlan_CMGT),
DynPlan_gt = Case When Sum(DynPlan_Tonnes) = 0 then 0 else Sum(DynPlan_Grams)/Sum(DynPlan_Tonnes) end,
DynPlan_Kg = Sum(DynPlan_Grams)/1000,

DynDayPlan_SQM  = Sum(DynDayPlan_SQM),
DynDayPlan_SQMDens  = Sum(DynDayPlan_SQMDens),
DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
DynDayPlan_cmgt = Case When Sum(DynDayPlan_SQMDens) = 0 then 0 else Sum(DynDayPlan_Grams*100)/Sum(DynDayPlan_SQMDens) end,
DynDayPlan_gt = Case When Sum(DynDayPlan_Tonnes) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_Tonnes) end,
DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

DayBook_SQM  = Sum(DayBook_SQM),
DayBook_Tonnes = Sum(DayBook_Tonnes),
DayBook_cmgt = Case When Sum(DayBook_SQMDens) = 0 then 0 else Sum(DayBook_Grams*100)/Sum(DayBook_SQMDens) end,
DayBook_gt = Case When Sum(DayBook_Tonnes) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_Tonnes) end,
DayBook_Kg = Sum(DayBook_Grams)/1000,

DynProgPlan_SQM  = Sum(DynProgPlan_SQM),
DynProgPlan_SQMDens  = Sum(DynProgPlan_SQMDens),
DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
DynProgPlan_cmgt = Case When Sum(DynProgPlan_SQMDens) = 0 then 0 else Sum(DynProgPlan_Grams*100)/Sum(DynProgPlan_SQMDens) end,
DynProgPlan_gt = Case When Sum(DynProgPlan_Tonnes) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_Tonnes) end,
DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

ProgBook_SQM  = Sum(ProgBook_SQM),
ProgBook_Tonnes = Sum(ProgBook_Tonnes),
ProgBook_cmgt = Case When Sum(ProgBook_SQMDens) = 0 then 0 else Sum(ProgBook_Grams*100)/Sum(ProgBook_SQMDens) end,
ProgBook_gt = Case When Sum(ProgBook_Tonnes) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_Tonnes) end,
ProgBook_Kg = Sum(ProgBook_Grams)/1000,
ProgBook_SQMDens = Sum(ProgBook_SQMDens),
[Top10] = Sum([Top10]),
DynProgPlan_SW = Case When Sum(DynProgPlan_SQM) = 0 then 0 else Sum(DynProgPlan_SQMSW)/Sum(DynProgPlan_SQM) end,
ProgBook_SW = Case When Sum(ProgBook_SQM) = 0 then 0 else Sum(ProgBook_SQMSW)/Sum(ProgBook_SQM) end,
DynProgPlan_SQMSW  = Sum(DynProgPlan_SQMSW),
ProgBook_SQMSW  = Sum(ProgBook_SQMSW)

from 
(
Select 
MO = tp.SectionID,
e.Description,
ShiftNo = Sum(Case when d.WorkingDay = 'Y' then 1 else 0 end),
TotalShifts = Avg(TotalShifts),

LockPlan_SQM = Isnull(max(Isnull(LP.SQM,0)),0),
LockPlan_CMGT =Isnull(max(LP.CMGT) ,0),
LockPlan_SQMDens = Isnull(max(Isnull(LP.ReefSQM*e.density,0)),0),
LockPlan_Tonnes = Isnull(max(LP.SQM * (LP.SW / 100) * e.Density) ,0),
LockPlan_Grams = Isnull(max(LP.CMGT * LP.ReefSQM / 100 * e.Density) ,0),

DynPlan_SQM = Isnull(max(a.SQM) ,0),
DynPlan_CMGT =Isnull(max(a.CMGT) ,0),
DynPlan_SQMDens = Isnull(max(Isnull(a.ReefSQM*e.density,0)),0),
DynPlan_Tonnes = Isnull(max(a.SQM * (a.SW / 100) * e.Density) ,0),
DynPlan_Grams = Isnull(max(a.CMGT * a.ReefSQM / 100 * e.Density) ,0),

DynProgPlan_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.SQM,0) end),
DynProgPlan_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*e.density,0) end),
DynProgPlan_SQMSW = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*CY.SW,0) end),
DynProgPlan_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				 Isnull(CY.SQM * (CY.SW/100) * e.density,0) end),
DynProgPlan_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.ReefSQM*(CY.CMGT/100)*e.Density,0) end),

ProgBook_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookSQM,0) end),
ProgBook_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookReefSQM*e.density,0) end),
ProgBook_SQMSW = Sum(Case when d.Calendardate <= @TheDate then Isnull(CY.BookReefSQM*CY.BookSW,0) end),
ProgBook_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				Isnull(CY.BookSQM * (CY.SW/100) * e.Density ,0) end),
ProgBook_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*e.Density,0) end),

DynDayPlan_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.SQM,0) end),
DynDayPlan_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.ReefSQM*e.density,0) end),
DynDayPlan_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.Sqm * (CY.SW/100) * e.density,0) end),
DynDayPlan_Grams = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.ReefSQM*(CY.CMGT/100)*e.Density,0) end),

DayBook_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookSQM,0) end),
DayBook_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookReefSQM*e.density,0) end),
DayBook_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookSQM * (CY.SW/100) * e.Density ,0) end),
DayBook_Grams = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*e.Density,0) end),

TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

--,*
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
left join planmonth LP on
a.prodmonth = lp.prodmonth and
a.sectionid = lp.sectionID and
a.Workplaceid = lp.Workplaceid and
a.Activity = lp.Activity and
lp.PlanCode = 'LP' and
lp.Locked = 1
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
inner join caltype d on 
c.Calendarcode = d.Calendarcode and
c.BeginDate <= d.Calendardate and
c.enddate >= d.Calendardate
Inner join Workplace e on
a.WorkplaceID = e.WorkplaceID
left join Planning CY on
a.prodmonth = CY.prodmonth and
a.sectionid = CY.sectionID and
a.Workplaceid = CY.Workplaceid and
a.Activity = CY.Activity and
a.PLancode = CY.PLancode and
d.CalendarDate = CY.Calendardate
left outer join TOPPANELSSELECTED tp
on b.Prodmonth = tp.Prodmonth
and e.WorkplaceID = tp.WorkplaceID
Left Join 
(Select top 10 a.* from PLanmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
inner join 
Seccal c on 
--select * from section_complete
--select * from toppanelsselected
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
where Activity = 0
and a.Locked = 1
and isnull(a.IsStopped,'N') ='N'
and auth = 'Y'
order by KG desc) [Top] on
a.prodmonth = [Top].prodmonth and
a.sectionid = [Top].sectionID and
a.Workplaceid = [Top].Workplaceid and
a.Activity = [Top].Activity and
a.PLancode = [Top].PLancode 

where a.Activity in (0,3)
and d.Calendardate <= @TheDate
--and isnull(a.IsStopped,'N') ='N'

group by tp.SectionID, b.Sectionid_2,e.Description) Main
Group by
MO,
Description
with rollup
) Final
end