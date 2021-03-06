
ALTER  PROCEDURE [dbo].[Report_TopPanels] --'201702', 'GM', '4'
--declare
@Prodmonth varchar(6),
@SectionID varchar(20),
@Section varchar(6)

--set @Prodmonth='201702'
--set @SectionID='2'
--set @Section='4'
AS

BEGIN

declare @ToSelect varchar(max)
declare @ToJoin varchar(max)
declare @ToSelectSectionID varchar(max)
declare @Hierarch int

select @Hierarch = hierarchicalid from section where prodmonth = @Prodmonth and Sectionid = @SectionID
   
if ((select COUNT(*) from TopPanelsSelected
 where SectionID = @SectionID
 and Prodmonth = @Prodmonth) != 0)
 begin
  set @ToSelectSectionID = @SectionID
   if (@Hierarch = 1)
    begin 
    set @ToSelect = 'Sectionid_5'
    set @ToJoin = 'Sectionid_5'
    end
   else
    begin 
    set @ToSelect = 'Sectionid_2'
    set @ToJoin = 'Sectionid_2'
    end
 end

else
begin
 if (@Hierarch = 1)
 begin
  if ((select COUNT(*) 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_2
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_5 = @SectionID )!= 0)
   -- Does managers children have any top panels selected
   begin
    set @ToSelectSectionID = (select top (1) sc.Sectionid_5 
          from TopPanelsSelected tps
          inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_2
          where tps.Prodmonth = @Prodmonth
          and sc.Sectionid_5 = @SectionID)
    set @ToSelect = 'Sectionid_5'
    set @ToJoin = 'Sectionid_2'
   end
 end


 else
 begin

  if ((select COUNT(*) 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_5
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_3 = @SectionID) != 0)
  begin
   set @ToSelectSectionID = (select distinct Sectionid_3 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_5
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_3 = @SectionID)
   set @ToSelect = 'Sectionid_3'
   set @ToJoin = 'Sectionid_2'
  end
  else
  if ((select COUNT(*) 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_2
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_4 = @SectionID) != 0)
  begin
   set @ToSelectSectionID = (select distinct Sectionid_4 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_2
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_4 = @SectionID)
   set @ToSelect = 'Sectionid_4'
   set @ToJoin = 'Sectionid_2'
  end
  else
  if ((select COUNT(*) 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_5
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_5 = 1) != 0)
  begin
   set @ToSelectSectionID = @SectionID
   set @ToSelect = 'Sectionid_2'
   set @ToJoin = 'Sectionid_5'
  end 
 end
end
   
declare @Query varchar(max)


--select @ToSelect, @ToSelectSectionID, @ToJoin

set @Query = '
select 
 MO, 
 Workplace, 
 Reef, 
 /*A*/MonthlyPlanningSQM = SUM(PlanSQM), 
 /*E*/MonthlyPlanningCMGT =  case when SUM(PlanSQMReef) <> 0 then SUM(PlanCMGT) / SUM(PlanSQMReef) else 0 end,
 /*G*/MonthlyPlanningGT = case when SUM(PlanSQMReef) <> 0 then SUM(PlanSQMReef * PlanGT) / SUM(PlanSQMReef) else 0 end,
 /*I*/MonthlyPlanningKGs = SUM(PlanKG),
 /*F*/ProgressGradeActualCMGT = case when sum(BookSqm) > 0 then SUM(BookCMGTT)/SUM(BookSqm) else 0 end,
 /*H*/ProgressGradeActualGT = (case when sum(BookSqm) > 0 then SUM(BookCMGT)/SUM(BookSqm) else 0 end),
 /*J*/ProgressGradeVarCMGT = (case when sum(BookSqm) > 0 then SUM(BookCMGTT)/SUM(BookSqm) else 0 end)
     - (case when SUM(PlanSQMReef) <> 0 then SUM(PlanCMGT) / SUM(PlanSQMReef) else 0 end),
 /*H*/ProgressGradeVarGT = (case when sum(BookSqm) > 0 then SUM(BookCMGT)/SUM(BookSqm) else 0 end)
     - (case when SUM(PlanSQMReef) <> 0 then SUM(PlanSQMReef * PlanGT) / SUM(PlanSQMReef) else 0 end),
 /*B*/ProgressivePlannedSQM = SUM(PlanSQMProg),
 /*C*/ProgressiveActualSQM = SUM(BookSQMProg),
 /*D*/ProgressiveVarSQM = SUM(BookSQMProg)-SUM(PlanSQMProg),
 /*B*/ProgressivePlannedKG = SUM(PlanKGProg),
 /*C*/ProgressiveActualKG = SUM(BookKGProg),
 /*D*/ProgressiveVarKG = SUM(BookKGProg)-SUM(PlanKGProg)
 
from (
select  sc.Sectionid_2 +'':''+sc.Name_2 as ''MO'', 
 p.workplaceid +'':''+w.description as ''Workplace'',
 r.description as ''Reef'',
 /*A*/PlanSQM = p.sqm,
 /*A*/PlanSQMReef = p.ReefSQM,
 /*E*/PlanCMGT = case when p.reefsqm <> 0 then (p.ReefSQM * pp.cmgt) else 0 end,
 PlanGT = case when pp.sw <> 0 then pp.cmgt/pp.sw else 0 end,
 /*G /PlanGT = case when p.reefsqm <> 0 then p.sqmreef * (case when pp.sw <> 0 then pp.cmgt/pp.sw else 0 end)
   /p.sqmreef else 0 end,*/
 /*I*/PlanKG = p.Grams / 1000,
 /*F*/BookSqm = case when ct.calendardate is not null then BookReefSQM else 0 end,
 /*F*/BookCMGTT = case when ct.calendardate is not null then BookReefSQM * Bookgt else 0 end,
 /*F*/BookCMGT = case when ct.calendardate is not null then BookReefSQM * (case when BookSW <> 0 then Bookgt/BookSW else 0 end) else 0 end,
 /*H*/BookSW = case when ct.calendardate is not null then BookSW else 0 end,
 /*B*/PlanSQMProg = case when ct.calendardate is not null then p.sqm else 0 end,
 /*C*/BookSQMProg = case when ct.calendardate is not null then booksqm else 0 end,
 /*D*/VarSQM = case when ct.calendardate is not null then booksqm else 0 end
     -case when ct.calendardate is not null then p.sqm else 0 end,
 /*L*/PlanKGProg = case when ct.calendardate is not null then p.Grams / 1000 else 0 end,
 /*L*/BookKGProg = case when ct.calendardate is not null then p.BookGrams / 1000 else 0 end,
 /*O*/PlanSweepings = ''??'',
 /*P*/BookSweepings = ''??'',
 /*Q*/VarSweepings = ''??''
 

from PLANNING p
inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
inner join seccal scc on
scc.sectionid = sc.Sectionid_1 and scc.prodmonth = sc.prodmonth
left outer join (select Calendarcode, max(calendardate) calendardate from caltype where workingday = ''Y'' and calendardate < getdate() -1
group by calendarcode) ct on
ct.calendarcode = scc.calendarcode and
ct.calendardate >= p.calendardate
left outer join PLANMONTH pp on
  pp.Workplaceid = p.WorkplaceID and 
  pp.Prodmonth = p.Prodmonth and  
  pp.SectionID = p.SectionID and 
  pp.Activity = p.Activity and
  pp.PlanCode=p.PlanCode
  inner join WORKPLACE w on w.WorkplaceID = p.workplaceid
  inner join reef r on w.reefid = r.reefid
  inner join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.' + @ToJoin + ' and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity '
  
set @query = @query + ' where p.Prodmonth = ' + convert(varchar, @Prodmonth)
set @query = @query + ' and sc.' + @ToSelect + ' = '''
set @query = @query + convert(varchar, @ToSelectSectionID) + ''') a
group by MO,WORKPLACE, Reef
order by MonthlyPlanningKGs desc'
exec (@query)
print (@query)
END

go

ALTER PROCEDURE [dbo].[Report_Production_Analysis_Stoping_GT_Monthly_Average]
	-- Add the parameters for the stored procedure here
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID varchar(20),
@Section int,
@IsProgressive bit,
@TypeReport varchar(1),
@Shft int

AS
BEGIN
	--set @Prodmonth = '201504'
	--set @FromDate = '2015-04-01'
	--set @ToDate = '2015-05-01'
	--set @SectionID = 'AA'
	--set @Section = 5
	--set @IsProgressive = 0
	--set @TypeReport = 'D'
	--set @shft = 20

	declare @Query varchar(max)
	declare @ShiftsQuery varchar(max)
	declare @ToJoin varchar(20)

	if (@Section = 1)
		set @ToJoin = 'SectionID_5'
	if (@Section = 2)
		set @ToJoin = 'SectionID_4'
	if (@Section = 3)
		set @ToJoin = 'SectionID_3'
	if (@Section = 4)
		set @ToJoin = 'SectionID_2'
	if (@Section = 5)
		set @ToJoin = 'SectionID_1'
	if (@Section = 6)
		set @ToJoin = 'SectionID'

	IF (@TypeReport = 'M')
	BEGIN
		create table #tempShiftCount (ShiftCount int)
		set @ShiftsQuery = '
			select count(distinct(p.CalendarDate)) 
			from PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where p.Prodmonth = '''+ @ProdMonth +''' 
				  and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
				 and p.Activity IN(0,9)
				 and CT.WorkingDay = ''Y'' and CalendarCode=''Mill'''
		declare @Shifts int
		select @ShiftsQuery = 'insert into #tempShiftCount ' + @ShiftsQuery
		exec (@ShiftsQuery)
		select @Shifts = ShiftCount from #tempShiftCount
		drop table #tempShiftCount
	END
	ELSE
	BEGIN
		set @Shifts = @Shft
	END

	IF(@IsProgressive = 1)
	BEGIN
		SET @Shifts = 1;
	END

	set @Query = 
		'select SUM(PlanValue) / ' + convert(varchar, @Shifts) + ' PlanValue, 
			    SUM(BookValue) / ' + convert(varchar, @Shifts) + ' BookValue
		from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate, 
				convert(decimal(18, 0), case when SUM(p.reefsqm) <> 0 then sum(pm.CMGT * p.reefsqm) / SUM(p.reefsqm) else 0 end) as ''PlanValue'', 
				convert(decimal(18, 0), case when SUM(p.bookreefsqm) <> 0 then sum(p.Bookgt * p.bookreefsqm) / SUM(p.bookreefsqm) else 0 end) as ''BookValue''
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
			inner join PLANMONTH pm on 
				pm.Activity = p.Activity and 
				pm.Sectionid = p.SectionID and 
				pm.Workplaceid = p.WorkplaceID and 
				pm.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
			IF (@TypeReport = 'M')
				set @Query = @Query + ' p.ProdMonth = '''+ @ProdMonth +''' '
			ELSE
				set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.' + @ToJoin + ' = '''+ @SectionID +'''
									and p.Activity IN(0,9)
									and CT.WorkingDay = ''Y'' and CalendarCode=''Mill''
			group by p.CalendarDate
		) as a '

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0

END
go

ALTER PROCEDURE [dbo].[Report_Production_Analysis_Stoping_GT]
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID varchar(20),
@Section int,
@TypeReport varchar(1)

AS
BEGIN
	--set @Prodmonth = '201504'
	--set @FromDate = '2015-04-01'
	--set @ToDate = '2015-05-01'
	--set @SectionID = 'AA'
	--set @Section = 5
	--set @TypeReport = 'D'

	declare @Query varchar(max)
	declare @ToJoin varchar(20)

	if (@Section = 1)
		set @ToJoin = 'SectionID_5'
	if (@Section = 2)
		set @ToJoin = 'SectionID_4'
	if (@Section = 3)
		set @ToJoin = 'SectionID_3'
	if (@Section = 4)
		set @ToJoin = 'SectionID_2'
	if (@Section = 5)
		set @ToJoin = 'SectionID_1'
	if (@Section = 6)
		set @ToJoin = 'SectionID'


	set @Query = ' select * from
		(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate, 
			convert(decimal(18, 0), case when SUM(p.reefsqm) <> 0 then sum(pm.CMGT * p.reefsqm) / SUM(p.reefsqm) else 0 end) ''Plan'', 
			convert(decimal(18, 0), case when SUM(p.bookreefsqm) <> 0 then sum(p.Bookgt * p.bookreefsqm) / SUM(p.bookreefsqm) else 0 end) ''Book''
		from
		PLANNING p
		inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth
		inner join PLANMONTH pm on 
		  pm.Activity = p.Activity and pm.Sectionid = p.SectionID and 
		  pm.Workplaceid = p.WorkplaceID and pm.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
		where '
	if (@TypeReport = 'M')
		set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
	ELSE
		set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
	set @Query = @Query + ' and sc.'+ @ToJoin +' = '''+ @SectionID +'''
			  and p.Activity IN (0,9) 
			  and CT.WorkingDay = ''Y'' and CalendarCode=''Mill''
			group by p.CalendarDate) as a
			order by CalendarDate '

	print (@Query)
	exec (@Query)

END
go



ALTER  PROCEDURE [dbo].[Report_TopPanels] --'201702', 'GM', '4'
--declare
@Prodmonth varchar(6),
@SectionID varchar(20),
@Section varchar(6)

--set @Prodmonth='201702'
--set @SectionID='2'
--set @Section='4'
AS

BEGIN

declare @ToSelect varchar(max)
declare @ToJoin varchar(max)
declare @ToSelectSectionID varchar(max)
declare @Hierarch int

select @Hierarch = hierarchicalid from section where prodmonth = @Prodmonth and Sectionid = @SectionID
   
if ((select COUNT(*) from TopPanelsSelected
 where SectionID = @SectionID
 and Prodmonth = @Prodmonth) != 0)
 begin
  set @ToSelectSectionID = @SectionID
   if (@Hierarch = 1)
    begin 
    set @ToSelect = 'Sectionid_5'
    set @ToJoin = 'Sectionid_5'
    end
   else
    begin 
    set @ToSelect = 'Sectionid_2'
    set @ToJoin = 'Sectionid_2'
    end
 end

else
begin
 if (@Hierarch = 1)
 begin
  if ((select COUNT(*) 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_2
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_5 = @SectionID )!= 0)
   -- Does managers children have any top panels selected
   begin
    set @ToSelectSectionID = (select top (1) sc.Sectionid_5 
          from TopPanelsSelected tps
          inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_2
          where tps.Prodmonth = @Prodmonth
          and sc.Sectionid_5 = @SectionID)
    set @ToSelect = 'Sectionid_5'
    set @ToJoin = 'Sectionid_2'
   end
 end


 else
 begin

  if ((select COUNT(*) 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_5
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_3 = @SectionID) != 0)
  begin
   set @ToSelectSectionID = (select distinct Sectionid_3 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_5
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_3 = @SectionID)
   set @ToSelect = 'Sectionid_3'
   set @ToJoin = 'Sectionid_2'
  end
  else
  if ((select COUNT(*) 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_2
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_4 = @SectionID) != 0)
  begin
   set @ToSelectSectionID = (select distinct Sectionid_4 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_2
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_4 = @SectionID)
   set @ToSelect = 'Sectionid_4'
   set @ToJoin = 'Sectionid_2'
  end
  else
  if ((select COUNT(*) 
   from TopPanelsSelected tps
   inner join Section_Complete sc on tps.Prodmonth = sc.Prodmonth and tps.SectionID = sc.Sectionid_5
   where tps.Prodmonth = @Prodmonth
   and sc.Sectionid_5 = '1') != 0)
  begin
   set @ToSelectSectionID = @SectionID
   set @ToSelect = 'Sectionid_2'
   set @ToJoin = 'Sectionid_5'
  end 
 end
end
   
declare @Query varchar(max)


--select @ToSelect, @ToSelectSectionID, @ToJoin

set @Query = '
select 
 MO, 
 Workplace, 
 Reef, 
 /*A*/MonthlyPlanningSQM = SUM(PlanSQM), 
 /*E*/MonthlyPlanningCMGT =  case when SUM(PlanSQMReef) <> 0 then SUM(PlanCMGT) / SUM(PlanSQMReef) else 0 end,
 /*G*/MonthlyPlanningGT = case when SUM(PlanSQMReef) <> 0 then SUM(PlanSQMReef * PlanGT) / SUM(PlanSQMReef) else 0 end,
 /*I*/MonthlyPlanningKGs = SUM(PlanKG),
 /*F*/ProgressGradeActualCMGT = case when sum(BookSqm) > 0 then SUM(BookCMGTT)/SUM(BookSqm) else 0 end,
 /*H*/ProgressGradeActualGT = (case when sum(BookSqm) > 0 then SUM(BookCMGT)/SUM(BookSqm) else 0 end),
 /*J*/ProgressGradeVarCMGT = (case when sum(BookSqm) > 0 then SUM(BookCMGTT)/SUM(BookSqm) else 0 end)
     - (case when SUM(PlanSQMReef) <> 0 then SUM(PlanCMGT) / SUM(PlanSQMReef) else 0 end),
 /*H*/ProgressGradeVarGT = (case when sum(BookSqm) > 0 then SUM(BookCMGT)/SUM(BookSqm) else 0 end)
     - (case when SUM(PlanSQMReef) <> 0 then SUM(PlanSQMReef * PlanGT) / SUM(PlanSQMReef) else 0 end),
 /*B*/ProgressivePlannedSQM = SUM(PlanSQMProg),
 /*C*/ProgressiveActualSQM = SUM(BookSQMProg),
 /*D*/ProgressiveVarSQM = SUM(BookSQMProg)-SUM(PlanSQMProg),
 /*B*/ProgressivePlannedKG = SUM(PlanKGProg),
 /*C*/ProgressiveActualKG = SUM(BookKGProg),
 /*D*/ProgressiveVarKG = SUM(BookKGProg)-SUM(PlanKGProg)
 
from (
select  sc.Sectionid_2 +'':''+sc.Name_2 as ''MO'', 
 p.workplaceid +'':''+w.description as ''Workplace'',
 r.description as ''Reef'',
 /*A*/PlanSQM = p.sqm,
 /*A*/PlanSQMReef = p.ReefSQM,
 /*E*/PlanCMGT = case when p.reefsqm <> 0 then (p.ReefSQM * pp.cmgt) else 0 end,
 PlanGT = case when pp.sw <> 0 then pp.cmgt/pp.sw else 0 end,
 /*G /PlanGT = case when p.reefsqm <> 0 then p.sqmreef * (case when pp.sw <> 0 then pp.cmgt/pp.sw else 0 end)
   /p.sqmreef else 0 end,*/
 /*I*/PlanKG = p.Grams / 1000,
 /*F*/BookSqm = case when ct.calendardate is not null then BookReefSQM else 0 end,
 /*F*/BookCMGTT = case when ct.calendardate is not null then BookReefSQM * Bookgt else 0 end,
 /*F*/BookCMGT = case when ct.calendardate is not null then BookReefSQM * (case when BookSW <> 0 then Bookgt/BookSW else 0 end) else 0 end,
 /*H*/BookSW = case when ct.calendardate is not null then BookSW else 0 end,
 /*B*/PlanSQMProg = case when ct.calendardate is not null then p.sqm else 0 end,
 /*C*/BookSQMProg = case when ct.calendardate is not null then booksqm else 0 end,
 /*D*/VarSQM = case when ct.calendardate is not null then booksqm else 0 end
     -case when ct.calendardate is not null then p.sqm else 0 end,
 /*L*/PlanKGProg = case when ct.calendardate is not null then p.Grams / 1000 else 0 end,
 /*L*/BookKGProg = case when ct.calendardate is not null then p.BookGrams / 1000 else 0 end,
 /*O*/PlanSweepings = ''??'',
 /*P*/BookSweepings = ''??'',
 /*Q*/VarSweepings = ''??''
 

from PLANNING p
inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
inner join seccal scc on
scc.sectionid = sc.Sectionid_1 and scc.prodmonth = sc.prodmonth
left outer join (select Calendarcode, max(calendardate) calendardate from caltype where workingday = ''Y'' and calendardate < getdate() -1
group by calendarcode) ct on
ct.calendarcode = scc.calendarcode and
ct.calendardate >= p.calendardate
left outer join PLANMONTH pp on
  pp.Workplaceid = p.WorkplaceID and 
  pp.Prodmonth = p.Prodmonth and  
  pp.SectionID = p.SectionID and 
  pp.Activity = p.Activity and
  pp.PlanCode=p.PlanCode
  inner join WORKPLACE w on w.WorkplaceID = p.workplaceid
  inner join reef r on w.reefid = r.reefid
  inner join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.' + @ToJoin + ' and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity '
  
set @query = @query + ' where p.Prodmonth = ' + convert(varchar, @Prodmonth)
set @query = @query + ' and sc.' + @ToSelect + ' = '''
set @query = @query + convert(varchar, @ToSelectSectionID) + ''') a
group by MO,WORKPLACE, Reef
order by MonthlyPlanningKGs desc'
exec (@query)
print (@query)
END









