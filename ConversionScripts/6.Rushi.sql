create  PROCEDURE [dbo].[Report_TopPanels] --'201702', 'GM', '4'
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
 /*E*/PlanCMGT = case when p.reefsqm <> 0 then (p.ReefSQM * pp.gt) else 0 end,
 PlanGT = case when pp.sw <> 0 then pp.gt/pp.sw else 0 end,
 /*G /PlanGT = case when p.reefsqm <> 0 then p.sqmreef * (case when pp.sw <> 0 then pp.gt/pp.sw else 0 end)
   /p.sqmreef else 0 end,*/
 /*I*/PlanKG = p.Grams / 1000,
 /*F*/BookSqm = case when ct.calendardate is not null then BookReefSQM else 0 end,
 /*F*/BookCMGTT = case when ct.calendardate is not null then BookReefSQM * Bookcmgt else 0 end,
 /*F*/BookCMGT = case when ct.calendardate is not null then BookReefSQM * (case when BookSW <> 0 then Bookcmgt/BookSW else 0 end) else 0 end,
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



CREATE TABLE [dbo].[TonnageData](
	[UserID] [varchar](20) NULL,
	[WorkplaceID] [varchar](12) NULL,
	[CalendarDate] [varchar](10) NULL,
	[Heading] [varchar](50) NULL,
	[Tons] [varchar](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[TonnageReport](
	[UserID] [varchar](20) NULL,
	[Section] [varchar](73) NULL,
	[Workplace] [varchar](65) NULL,
	[WorkplaceID] [varchar](12) NULL,
	[CalendarDate] [varchar](10) NULL,
	[Heading] [varchar](50) NULL,
	[Tons] [varchar](10) NULL,
	[theSort] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[TonnageWP](
	[UserID] [varchar](20) NULL,
	[WorkplaceID] [varchar](12) NULL,
	[Section] [varchar](65) NULL,
	[Workplace] [varchar](73) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE View [dbo].[vw_WP_density] as
select a.workplaceid, c.REEFID,
c.DESCRIPTION,
c.DeterminantCW ,
c.MinTheoreticalSW ,
c.CondonedWaste ,
density = case when c.density is null then sysset.Rockdensity else 
c.density
end

 from workplace a left join reef c on 
a.reefid = c.reefid
 ,sysset


GO

CREATE TABLE [dbo].[MOMeas_Stope](
	[PRODMONTH] [numeric](7, 0) NOT NULL,
	[SECTIONID] [varchar](20) NOT NULL,
	[WORKPLACEID] [varchar](12) NOT NULL,
	[Activity] [numeric](7, 0) NOT NULL,
	[Tick1] [numeric](1, 0) NOT NULL,
	[Tick2] [numeric](1, 0) NOT NULL,
	[Tick3] [numeric](1, 0) NOT NULL,
	[Tick4] [numeric](1, 0) NOT NULL,
	[Tick5] [numeric](1, 0) NOT NULL,
	[Tick6] [numeric](1, 0) NOT NULL,
	[Tick7] [numeric](1, 0) NOT NULL
) ON [PRIMARY]

GO

create PROCEDURE [dbo].[Report_Tonnage] --'201309', '2', 4
@UserID varchar(20),
@Prodmonth varchar(6),
@SectionID varchar(20),
@HierID int,
@TypeReport varchar(1),
@BDate Datetime,
@EDate Datetime,
@ProgDay varchar(1),
@ReefWaste varchar(1)

AS

BEGIN
declare @theStartDate varchar(10)
declare @theEndDate varchar(10)
declare @repDate varchar(10)
declare @theSDate Datetime
declare @theEDate Datetime
declare @theStDate Datetime
declare @theEnDate Datetime

delete from TonnageReport where UserID = @UserID
delete from TonnageData where UserID = @UserID
delete from TonnageWP where UserID = @UserID

IF (@TypeReport = 'P')
BEGIN
	select  @theStartDate = convert(varchar(10), c.StartDate,120),
			@theStDate = c.StartDate,
			@theEnDate = c.EndDate,
			@theEndDate = convert(varchar(10), c.EndDate,120) from CALENDARMILL c
	where c.MillMonth = @Prodmonth
END
ELSE
BEGIN
	set @theStartDate = convert(varchar(10), @BDate, 120)
	set @theStDate = @BDate
	set @theEnDate = @EDate
	set @theEndDate = convert(varchar(10), @EDate ,120)
END

declare @Query varchar(max)
declare @scWorkplaceID varchar(12)
declare @scWorkplace varchar(65)
declare @scSection varchar(73)
declare @ToJoin varchar(20)

if (@HierID = 1)
	set @ToJoin = 'Sectionid_5'
if (@HierID = 2)
	set @ToJoin = 'Sectionid_4'
if (@HierID = 3)
	set @ToJoin = 'Sectionid_3'
if (@HierID = 4)
	set @ToJoin = 'Sectionid_2'
if (@HierID = 5)
	set @ToJoin = 'Sectionid_1'
if (@HierID = 6)
	set @ToJoin = 'SectionID'

set @Query = 
		' select '''+ @USerID +''' UserID, p.WorkplaceID, sc.sectionID+'' : ''+sc.Name Section,p.WorkplaceID+'' : ''+W.Description Workplace 
		from
		planmonth p
		inner join
		(
			select max(ProdMOnth) ProdMonth, WorkplaceID
			from
			(
				select distinct(p.workplaceid) WorkplaceID,  p.ProdMonth
						from planning p 
						inner join Section_Complete sc on
						  p.Prodmonth = sc.Prodmonth and
						  p.SectionID = sc.SectionID
						where p.calendarDate >= '''+ @theStartDate +''' and
								p.calendardate <= '''+ @theEndDate +''' and
								sc.'+@ToJoin+' = '''+ @SectionID +''' 
			) a
			group by WorkplaceID
		) pd on p.WorkplaceID=pd.Workplaceid and
				p.prodmonth=pd.prodmonth
		inner join Section_Complete sc on
		  sc.ProdMonth = p.Prodmonth and
		  sc.SectionID = p.SectionID
		inner join Workplace w on
		  w.WorkplaceID = p.WorkplaceID '

select @Query = 'insert into TonnageWP ' + @Query
exec (@Query)

DECLARE Tonnage_Cursor CURSOR FOR
select WorkplaceID, Section, Workplace from TonnageWP where UserID = @USerID
OPEN Tonnage_Cursor;
FETCH NEXT FROM Tonnage_Cursor
into @scWorkplaceID, @scSection, @scWorkplace;
 
WHILE @@FETCH_STATUS = 0
BEGIN
	--print (@theWorkplaceID)
	set @theSDate = @theStDate
	--print (@theSDate)
	set @theEDate = @theEnDate 
	--print (@theEDate)

    WHILE (@theSDate <= @theEDate)
	BEGIN				
		select @repDate = convert(varchar(10), @theSDate,120)
		--print (@scWorkplaceID)
		--print ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Planned Tons'', '''', 1)')


		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Planned Tons'', '''', 1)')
		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Broken Tons'', '''', 2)')
		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Trammed Tons'', '''', 3)')
		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Planned Grade'', '''', 4)')
		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Broken Grade'', '''', 5)')
		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Trammed Grade'', '''', 6)')
		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Planned Kilos'', '''', 7)')
		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', ''Broken Kilos'', '''', 8)')
		exec ('insert into TonnageReport (UserID, Section, Workplace, WorkplaceID, CalendarDate, Heading, Tons, theSort) values ('''+ @UserID +''', '''+ @scSection +''', '''+ @scWorkplace +''', '''+ @scWorkplaceID +''', '''+ @repDate +''', '''', '''', 9)')
		set @theSDate = DateAdd(day, 1, @theSDate)
	END 
	
	FETCH NEXT FROM Tonnage_Cursor
		Into @scWorkplaceID, @scSection, @scWorkplace;
END; 
CLOSE Tonnage_Cursor;
DEALLOCATE Tonnage_Cursor;

declare @eeWorkplaceID varchar(12)
declare @eeCalendarDate varchar(10)
declare @eeHeading varchar(50)
declare @eeTons varchar(50)
declare @ffTons varchar(50)
declare @ffKilos varchar(50)

declare @ttWorkplaceID varchar(12)
declare @ttCalendarDate varchar(10)
declare @ttHeading varchar(50)
declare @ttTons varchar(50)

set @Query = '
	--Planned Tons 
	SELECT '''+ @UserID+ ''' USerID, WorkplaceID, CalendarDate = convert(varchar(10),CalendarDate,120), ''Planned Tons'' AS Heading, '
	IF (@ReefWaste = 'R')
		set @Query = @Query + ' Tons = case when isnull(sum(ReefTons),0) = 0 then '''' else convert(varchar(10), Convert(decimal(5,0),round(sum(ReefTons),0))) end '
	ELSE
	IF (@ReefWaste = 'W')
		set @Query = @Query + ' Tons = case when isnull(sum(wastetons),0) = 0 then '''' else convert(varchar(10), Convert(decimal(5,0),round(sum(wastetons),0))) end '
	ELSE
		set @Query = @Query + ' Tons = case when isnull(sum(tons),0) = 0 then '''' else convert(varchar(10), Convert(decimal(5,0),round(sum(tons),0))) end '
	set @Query = @Query + ' FROM Planning
	where CalendarDate >= '''+ @theStartDate +''' and CalendarDate <= '''+ @theEndDate +'''
	GROUP BY CalendarDate, WorkplaceID 
	UNION all 
	--Booked Tons 
	SELECT '''+ @UserID+ ''' USerID, WorkplaceID, CalendarDate = convert(varchar(10),CalendarDate,120), ''Broken Tons'' AS Heading, '
	IF (@ReefWaste = 'R')
		set @Query = @Query + ' Tons = case when isnull(sum(BookReefTons),0) = 0 then '''' else convert(varchar(10),Convert(decimal(5,0),round(sum(BookReefTons),0))) end '
	ELSE
	IF (@ReefWaste = 'W')
		set @Query = @Query + ' Tons = case when isnull(sum(BookWasteTons),0) = 0 then '''' else convert(varchar(10),Convert(decimal(5,0),round(sum(BookWasteTons),0))) end '
	ELSE
		set @Query = @Query + ' Tons = case when isnull(sum(BookTons),0) = 0 then '''' else convert(varchar(10),Convert(decimal(5,0),round(sum(BookTons),0))) end  '
	set @Query = @Query + ' FROM Planning
	where CalendarDate >= '''+ @theStartDate +'''  and CalendarDate <= '''+ @theEndDate +'''
	GROUP BY CalendarDate, WorkplaceID 
	UNION all 
	--Trammed Tons 
	SELECT '''+ @UserID+ ''' USerID, p.WorkplaceID, CalendarDate = convert(varchar(10),p.CalendarDate,120), ''Trammed Tons'' AS Heading, '
	IF (@ReefWaste = 'R')
		set @Query = @Query + ' Tons = case when b.reefwaste = ''R'' then 
										case when isnull(sum(b.tons),0) = 0 then '''' 
										else convert(varchar(10),Convert(decimal(5,0),round(sum(b.tons),0))) end 
									   else '''' end'
	ELSE
	IF (@ReefWaste = 'W')
		set @Query = @Query + ' Tons = case when b.reefwaste = ''W'' then 
										case when isnull(sum(b.tons),0) = 0 then ''''
										else convert(varchar(10),Convert(decimal(5,0),round(sum(b.tons),0))) end
									   else '''' end '
	ELSE
		set @Query = @Query + ' Tons = case when isnull(sum(b.tons),0) = 0 then '''' else convert(varchar(10),Convert(decimal(5,0),round(sum(b.tons),0))) end '
	set @Query = @Query + ' FROM Planning p	 
	left outer join BookingTrammingWP b on 
	  p.CalendarDate = b.CalendarDate and 
	  --p.SectionID = b.SectionID and 
	  p.WorkplaceID = b.WorkplaceID and 
	  p.Activity = b.Activity
	where p.CalendarDate >= '''+ @theStartDate +'''  and p.CalendarDate <= '''+ @theEndDate +'''
	GROUP BY p.CalendarDate, p.WorkplaceID, b.ReefWaste 
	UNION all 
	-- Planned Grade 
	SELECT '''+ @UserID+ ''' USerID, WorkplaceID, CalendarDate = convert(varchar(10),CalendarDate,120), ''Planned Grade'' AS Heading, '
	IF (@ReefWaste = 'R')
		set @Query = @Query + ' Tons = case when isnull(sum(ReefTons),0) > 0 then convert(varchar(10),convert(decimal(5,2),(round(isnull(sum(GT),0) / isnull(sum(ReefTons),0),2)))) else '''' end '
	ELSE
	IF (@ReefWaste = 'W')
		set @Query = @Query + ' Tons = '''' '
	ELSE
		set @Query = @Query + ' Tons = case when isnull(sum(Tons),0) > 0 then convert(varchar(10),convert(decimal(5,2),round(isnull(sum(Content),0) / isnull(sum(Tons),0),2))) else '''' end '
	set @Query = @Query + ' FROM Planning
	where CalendarDate >= '''+ @theStartDate +'''  and CalendarDate <= '''+ @theEndDate +'''
	GROUP BY CalendarDate, WorkplaceID 
	UNION all 
	-- Broken Grade 
	SELECT '''+ @UserID+ ''' USerID, WorkplaceID, CalendarDate = convert(varchar(10),CalendarDate,120), ''Broken Grade'' AS Heading, '
	IF (@ReefWaste = 'R')
		set @Query = @Query + ' Tons = case when isnull(sum(BookReefTons),0) > 0 then convert(varchar(10),convert(decimal(5,2),round(isnull(sum(BookGrams),0) / isnull(sum(BookReefTons),0),2))) else '''' end '
	ELSE
	IF (@ReefWaste = 'W')
		set @Query = @Query + ' Tons = '''' '
	ELSE
		set @Query = @Query + '  Tons = case when isnull(sum(BookTons),0) > 0 then convert(varchar(10),convert(decimal(5,2),round(isnull(sum(BookGrams),0) / isnull(sum(BookTons),0),2))) else '''' end '
	set @Query = @Query + ' FROM Planning 
	where CalendarDate >= '''+ @theStartDate +'''  and CalendarDate <= '''+ @theEndDate +'''
	GROUP BY CalendarDate, WorkplaceID 
	UNION all 
	-- Grade Trammed 
	SELECT '''+ @UserID+ ''' USerID, WorkplaceID, CalendarDate = convert(varchar(10),CalendarDate,120), ''Trammed Grade'' AS Heading, '
	IF (@ReefWaste = 'R')
		set @Query = @Query + ' Tons = '''' '
	ELSE
	IF (@ReefWaste = 'W')
		set @Query = @Query + ' Tons = '''' '
	ELSE
		set @Query = @Query + ' Tons = '''' '
	set @Query = @Query + ' FROM Planning
	where CalendarDate >= '''+ @theStartDate +'''  and CalendarDate <= '''+ @theEndDate +'''
	GROUP BY CalendarDate, WorkplaceID 
	union all 
	-- Planned Kilos
	SELECT '''+ @UserID+ ''' USerID, workplaceID, CalendarDate = convert(varchar(10),CalendarDate,120), ''Planned Kilos'' AS Heading, '
	IF (@ReefWaste = 'R')
		set @Query = @Query + ' Tons = case when isnull(sum(GT),0) = 0 then '''' else convert(varchar(10),convert(decimal(6,3),round(sum(GT) / 1000,3))) end '
	ELSE
	IF (@ReefWaste = 'W')
		set @Query = @Query + ' Tons = '''' '
	ELSE
		set @Query = @Query + ' Tons = case when isnull(sum(GT),0) = 0 then '''' else convert(varchar(10),convert(decimal(6,3),round(sum(GT) / 1000,3))) end  '
	set @Query = @Query + ' FROM Planning 
	where CalendarDate >= '''+ @theStartDate +'''  and CalendarDate <= '''+ @theEndDate +'''
	GROUP BY CalendarDate, WorkplaceID 
	UNION all 
	-- KGs Broken 
	SELECT '''+ @UserID+ ''' USerID, WorkplaceID, CalendarDate = convert(varchar(10),CalendarDate,120), ''Broken Kilos'' AS Heading, '
	IF (@ReefWaste = 'R')
		set @Query = @Query + ' Tons = case when isnull(sum(bookgrams),0) = 0 then '''' else convert(varchar(10),convert(decimal(6,3),round(sum(bookgrams) / 1000,3))) end '
	ELSE
	IF (@ReefWaste = 'W')
		set @Query = @Query + ' Tons = '''' '
	ELSE
		set @Query = @Query + ' Tons = case when isnull(sum(bookgrams),0) = 0 then '''' else convert(varchar(10),convert(decimal(6,3),round(sum(bookgrams) / 1000,3))) end '
	set @Query = @Query + ' FROM Planning
	where CalendarDate >= '''+ @theStartDate +'''  and CalendarDate <= '''+ @theEndDate +'''
	GROUP BY CalendarDate, WorkplaceID '
--insert into linda (aa) values (@Query)
set @Query = 'insert into TonnageData ' + @Query

exec (@Query)

DECLARE TonnageD_Cursor CURSOR FOR 
select WorkplaceID, CalendarDate, Heading, Tons from TonnageData where UserID = @UserID  order by workplaceid, heading desc, calendardate
OPEN TonnageD_Cursor;
FETCH NEXT FROM TonnageD_Cursor
into @eeWorkplaceID, @eeCalendarDate, @eeHeading, @eeTons;
set @Query = '' 
WHILE @@FETCH_STATUS = 0
BEGIN
	IF @eeHeading = 'Broken grade' 
	BEGIN
		select @ffTons = tons from tonnagereport where UserID =  @UserID and WorkplaceID = @eeWorkplaceID and CalendarDate = @eeCalendarDate 
		and Heading = 'Broken Tons'
		select @ffKilos = tons from tonnagereport where UserID =  @UserID and WorkplaceID = @eeWorkplaceID and CalendarDate = @eeCalendarDate 
		and Heading = 'Broken Kilos'
		if @ffKilos = ''
			set @ffKilos = '0'
		if @ffTons = '' or @ffTons = 0  
			set @eeTons = ''
		else
			set @eeTons = cast(convert(numeric(10,3),(cast(@ffKilos as numeric(10,3)) * 1000) / cast(@ffTons as numeric(10,3)),3) as varchar(50))
		
		
	END
	IF @eeHeading = 'Planned grade' 
	BEGIN
		select @ffTons = tons from tonnagereport where UserID =  @UserID and WorkplaceID = @eeWorkplaceID and CalendarDate = @eeCalendarDate 
		and Heading = 'Planned Tons'
		select @ffKilos = tons from tonnagereport where UserID =  @UserID and WorkplaceID = @eeWorkplaceID and CalendarDate = @eeCalendarDate 
		and Heading = 'Planned Kilos'
		if @ffKilos = ''
			set @ffKilos = '0'
		if @ffTons = ''   or @ffTons = 0  
			set @eeTons = ''
		else
		set @eeTons = cast(convert(numeric(10,3),(cast(@ffKilos as numeric(10,3)) * 1000) / cast(@ffTons as numeric(10,3)),3) as varchar(50))
		
		
	END
	IF @eeWorkplaceID = @ttWorkplaceid and @eeHeading = @ttHeading and @ProgDay = 'P'
	BEGIN
		if @eeTons = '' 
			set @eeTons = '0'
		if @ttTons = '' 
			set @ttTons = '0'
		if right(@eeHeading, 4) = 'Tons' 				
			set @ttTons = cast(cast((cast(@ttTons as numeric(10,0)) + cast(@eeTons as numeric(10,0))) as numeric(10,0)) as varchar(50))
		if right(@eeHeading, 5) = 'Kilos' 				
			set @ttTons = cast(cast((cast(@ttTons as numeric(10,3)) + cast(@eeTons as numeric(10,3))) as numeric(10,3)) as varchar(50))

	END
	else
		set @ttTons = @eeTons
	set @ttWorkplaceid = @eeWorkplaceid	
	set @ttHeading = @eeHeading
	set @Query = '		
	update TonnageReport set Tons = '''+ @ttTons +''' where UserID = '''+ @UserID +''' and WorkplaceID = '''+@eeWorkplaceID +''' and CalendarDate = '''+ @eeCalendarDate +''' and Heading = '''+ @eeHeading +''' '
	--insert into linda (aa) values (@Query)
	--print (@Query)
	exec (@Query)
	FETCH NEXT FROM TonnageD_Cursor
		Into @eeWorkplaceID, @eeCalendarDate, @eeHeading, @eeTons;
END; 
CLOSE TonnageD_Cursor;
DEALLOCATE TonnageD_Cursor;	

END
select Section, Workplace, Heading,Tons ,day(CalendarDate) CalendarDate, theSort from TonnageReport where USerID = @UserID order by Section, workplace,theSort, calendardate
--print @Query
--drop table #tempData
--drop table #tempTonnage
go


create PROCEDURE [dbo].[Report_CrewRanking_New]
--declare 
@Month1 VARCHAR(100), @Month3 VARCHAR(100),@Month12 VARCHAR(100),@sectionid varchar(80),@ratingBy varchar(5),@orderby varchar(10),@Status varchar(10),@totalmine varchar(10),@persect varchar(10),
@type varchar(100)
AS
--set @Month1=201702
--set @Month3=201701
--set @Month12=201604
--set @sectionid='Total Mine'
--set @ratingBy='AB'
--set @orderby='1 Month'
--set @Status='T'
--set @totalmine='Total Mine'
--set @persect ='F'
--set @type='OrgUnitDay'

SET ARITHABORT OFF 
SET ANSI_WARNINGS OFF

declare @type1 varchar(100)
declare @type2 varchar(100)
DECLARE @groupby varchar(100)
DECLARE @groupby1 varchar(100)
if @type='OrgUnitDay'
begin
set @type1='CrewMorning'
set @groupby='OrgUnitDay'
set @groupby1='CrewMorning'
end

if @type='sc.SectionID_2+'':''+Name_2'
begin
set @groupby='sc.SectionID_2,Name_2'
set @type1 ='sc.SectionID_2+'':''+Name_2'
set @type2 ='Name_2'
set @groupby1 =@groupby
end

if @type='sc.SectionID_1+'':''+Name_1'
begin
set @groupby='sc.SectionID_1,Name_1'
set @type1 ='sc.SectionID_1+'':''+Name_1'
set @type2 ='Name_1'
set @groupby1 =@groupby
end


declare @TheQuery varchar(max)
--exec Report_CrewRanking_New '201607', '201605', '201508','AAD','B','1 Month','T' ,'AAD:E Tshemese' ,'T'  
--exec Report_CrewRanking_New '201607', '201605', '201508','Total Mine','AB','1 Month','T' ,'Total Mine' ,'F'  

if @totalmine!='Total Mine'         ---------------------------Actual/Booked----------------------
BEGIN
if @ratingBy='B' AND @Status='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by SQM12Percentage ASC) tot12mnthRank, rank() over (order by SQM12Percentage ASC)SQMRank12,* from
(select org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(d1.BookSqm12,-99999) desc) as SqmRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as int) Sqm12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm12M,''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm12M,sum(SqmTotal) BookSqm12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank 
end



if @orderby='3 Month' 
begin
select * from
(select  rank() over (order by SQM3Percentage ASC) tot3mnthRank,rank() over (order by SQM3Percentage ASC)SQMRank3,* from
(select org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(d1.BookSqm3,-99999) desc) as SqmRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as int) Sqm3Percentage FROM(
select pp.OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm3M,''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm3M,sum(SqmTotal) BookSqm3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank
END

if @orderby='1 Month' 
begin
select * from
(select distinct rank() over (order by SQM1Percentage ASC) tot1mnthRank,rank() over (order by SQM1Percentage ASC)SQMRank1,* from
(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(d1.BookSqm1,-99999) desc) as SqmRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) Sqm1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm1M,''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month1 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm1M,sum(SqmTotal) BookSqm1 from survey s
where prodmonth between @Month1 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank
END
END

if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by SQM12Percentage DESC) tot12mnthRank, rank() over (order by SQM12Percentage DESC)SQMRank12,* from
(select org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(d1.BookSqm12,-99999) desc) as SqmRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as int) Sqm12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm12M,''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm12M,sum(SqmTotal) BookSqm12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank DESC
end


if @orderby='3 Month' 
begin
select * from
(select  rank() over (order by SQM3Percentage DESC) tot3mnthRank,rank() over (order by SQM3Percentage DESC)SQMRank3,* from
(select org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(d1.BookSqm3,-99999) desc) as SqmRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as int) Sqm3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm3M,''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm3M,sum(SqmTotal) BookSqm3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select * from
(select distinct rank() over (order by SQM1Percentage DESC) tot1mnthRank,rank() over (order by SQM1Percentage DESC)SQMRank1,* from
(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(d1.BookSqm1,-99999) desc) as SqmRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) Sqm1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm1M,''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month1 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm1M,sum(SqmTotal) BookSqm1 from survey s
where prodmonth between @Month1 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank DESC
END
end

if @ratingBy='P' AND @Status ='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by SQM12Percentage ASC) tot12mnthRank, rank() over (order by SQM12Percentage ASC)SQMRank12,* from
(select org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(d1.BookSqm12,-99999) desc) as SqmRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as int) Sqm12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm12M,''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm12M,sum(SqmTotal) BookSqm12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by SQM12Percentage
end



if @orderby='3 Month' 
begin
select * from
(select  rank() over (order by SQM3Percentage ASC) tot3mnthRank,rank() over (order by SQM3Percentage ASC)SQMRank3,* from
(select org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(d1.BookSqm3,-99999) desc) as SqmRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as int) Sqm3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm3M,''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm3M,sum(SqmTotal) BookSqm3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by SQM3Percentage
END



if @orderby='1 Month' 
begin
select * from
(select distinct rank() over (order by SQM1Percentage ASC) tot1mnthRank,rank() over (order by SQM1Percentage ASC)SQMRank1,* from
(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(d1.BookSqm1,-99999) desc) as SqmRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) Sqm1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm1M,''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month1 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm1M,sum(SqmTotal) BookSqm1 from survey s
where prodmonth between @Month1 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY SQM1Percentage
END
end

if @ratingBy='P' AND @Status ='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by SQM12Percentage DESC) tot12mnthRank, rank() over (order by SQM12Percentage DESC)SQMRank12,* from
(select org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(d1.BookSqm12,-99999) desc) as SqmRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as int) Sqm12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm12M,''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm12M,sum(SqmTotal) BookSqm12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by SQM12Percentage DESC
end



if @orderby='3 Month' 
begin
select * from
(select  rank() over (order by SQM3Percentage DESC) tot3mnthRank,rank() over (order by SQM3Percentage DESC)SQMRank3,* from
(select org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(d1.BookSqm3,-99999) desc) as SqmRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as int) Sqm3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm3M,''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm3M,sum(SqmTotal) BookSqm3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by SQM3Percentage DESC
END



if @orderby='1 Month' 
begin
select * from
(select distinct rank() over (order by SQM1Percentage DESC) tot1mnthRank,rank() over (order by SQM1Percentage DESC)SQMRank1,* from
(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(d1.BookSqm1,-99999) desc) as SqmRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) Sqm1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookSqm) aa, sum(BookSqm) Sqm1M,''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month1 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm1M,sum(SqmTotal) BookSqm1 from survey s
where prodmonth between @Month1 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY SQM1Percentage DESC
END
end
END

if @totalmine='Total Mine' AND @persect ='F'
BEGIN
if @ratingBy='P' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery='select * from(
select * from
(select    rank() over (order by SQM12Percentage desc) tot12mnthRank,  rank() over (order by SQM12Percentage desc) SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12 ,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby +'  )d


FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =s.SectionID and
sc.Prodmonth =s.Prodmonth 
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)  
group by '+@groupby1 +'

union


select distinct '+@type1+' CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN PLANNING pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth  where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data

left outer join 
(select * from
(select  rank() over (order by SQM3Percentage desc)  tot3mnthRank,rank() over (order by SQM3Percentage desc) SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100  as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth  where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d

left outer join
(


select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =s.SectionID and
sc.Prodmonth =s.Prodmonth 
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3

left outer join 
(select distinct rank() over (order by SQM1Percentage desc) tot1mnthRank,rank() over (order by SQM1Percentage desc) SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100  as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'   )d

left outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12  ORDER BY SQM12Percentage desc'
--select(@TheQuery)
exec (@TheQuery)
END


if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select  rank() over (order by SQM3Percentage DESC) tot3mnthRank,rank() over (order by SQM3Percentage DESC)SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100  as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d

FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)  
group by '+@groupby1 +' 

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data
 

left outer join 
(select * from
(select distinct rank() over (order by SQM1Percentage desc) tot1mnthRank,rank() over (order by SQM1Percentage desc)SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100  as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'   )d

FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select   rank() over (order by SQM12Percentage desc) tot12mnthRank, rank() over (order by SQM12Percentage desc)SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100  as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d

FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union


select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY SQM3Percentage desc'

--select @TheQuery
exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from (
select distinct rank() over (order by SQM1Percentage DESC) tot1mnthRank,rank() over (order by SQM1Percentage DESC)SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100  as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'   )d

FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data


left outer join 
(select * from
(select  rank() over (order by SQM3Percentage desc) tot3mnthRank,rank() over (order by SQM3Percentage desc) SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100  as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d

FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)  
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   rank() over (order by SQM12Percentage desc) tot12mnthRank, rank() over (order by SQM12Percentage desc)SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100  as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d


FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union


select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode  inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY SQM1Percentage desc'

--select @TheQuery
exec (@TheQuery)
END
end


if @ratingBy='P' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by SQM12Percentage ASC) tot12mnthRank ,rank() over (order by SQM12Percentage ASC)SQMRank12,* from(
select org12,SQM12,BOOKSQM12, rank() over (order by SQM12,BOOKSQM12 asc) as SQMRank12_,cast((nullif(BOOKSQM12,0)/SQM12  )*100 as int) SQM12Percentage FROM(
SELECT DISTINCT org12,SUM(Sqm12)SQM12,sum(BookSqm12) BOOKSQM12 FROM (
select  org12 ,Sqm12m,ISNULL(BookSqm12,0) BookSqm12,  aa sqm12
from (select OrgUnitDay  org12, sum(BookSqm) aa, sum(BookSqm) Sqm12M,SqmTotal BookSqm12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID ) c)D GROUP BY org12)X)l  order by SQM12Percentage
END

if @orderby='3 Month' 
begin
select rank() over (order by SQM3Percentage ASC) tot3mnthRank,rank() over (order by SQM3Percentage ASC)SQMRank3,* from
(select org3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3_,cast((nullif(BOOKSQM3,0)/SQM3  )*100 as int) SQM3Percentage FROM(
SELECT DISTINCT org3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
select  org3 ,Sqm3M,isnull(BookSqm3,0) BookSqm3,  aa sqm3
from (select OrgUnitDay  org3, sum(BookSqm) aa, sum(BookSqm) Sqm3M,SqmTotal  BookSqm3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID ) c)D GROUP BY org3)X)M order by SQM3Percentage 
END

if @orderby='1 Month' 
begin
select rank() over (order by SQM1Percentage ASC) tot1mnthRank,rank() over (order by SQM1Percentage ASC)SQMRank1,* from
(select org1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1_,cast((nullif(BOOKSQM1,0)/SQM1  )*100 as int) SQM1Percentage FROM(
SELECT DISTINCT org1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
select  org1 ,Sqm1M,isnull(BookSqm1,0)BookSqm1 ,  aa sqm1
from (select OrgUnitDay  org1, sum(BookSqm) aa, sum(BookSqm) Sqm1M,SqmTotal  BookSqm1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID ) c)D GROUP BY org1)X)n ORDER BY SQM1Percentage  
END
end

if @ratingBy='B' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   SQMRank12_  tot12mnthRank, SQMRank12_ SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100  as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d

FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p  INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data


left outer join 
(select * from
(select  SQMRank3_  tot3mnthRank,SQMRank3_ SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100  as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d

FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3


left outer join 
(select distinct SQMRank1_  tot1mnthRank,SQMRank1_ SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100  as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'   )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12   ORDER BY tot12mnthRank asc,SQM12Percentage DESC'
--SELECT @TheQuery
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select  SQMRank3_  tot3mnthRank,SQMRank3_ SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'  )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct SQMRank1_  tot1mnthRank,SQMRank1_ SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100 as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'    )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union
select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select   SQMRank12_  tot12mnthRank, SQMRank12_ SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY tot3mnthRank asc,SQM3Percentage DESC'
--SELECT @TheQuery
exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from (
select distinct SQMRank1_  tot1mnthRank,SQMRank1_ SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100 as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth 
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby +'   )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union
select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data


left outer join 
(select * from
(select  SQMRank3_  tot3mnthRank,SQMRank3_ SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'  )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union
select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   SQMRank12_ tot12mnthRank, SQMRank12_ SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(BookSqm) aa, sum(BookSqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union
select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY tot1mnthRank asc,SQM1Percentage DESC'

--select (@TheQuery)
exec (@TheQuery)
END
end

if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by SQMRank12_ desc) tot12mnthRank ,rank() over (order by SQMRank12_ desc)SQMRank12,* from(
select org12,SQM12,BOOKSQM12, rank() over (order by SQM12,BOOKSQM12 asc) as SQMRank12_,cast((nullif(BOOKSQM12,0)/SQM12  )*100 as int) SQM12Percentage FROM(
SELECT DISTINCT org12,SUM(Sqm12)SQM12,sum(BookSqm12) BOOKSQM12 FROM (
select  org12 ,Sqm12m,ISNULL(BookSqm12,0) BookSqm12,  aa sqm12
from (select OrgUnitDay  org12, sum(BookSqm) aa, sum(BookSqm) Sqm12M,SqmTotal BookSqm12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.ACTIVITY in (0,9) --and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID  ) c)D GROUP BY org12)X)l  order by tot12mnthRank DESC
END

if @orderby='3 Month' 
begin
select rank() over (order by SQMRank3_ desc) tot3mnthRank,rank() over (order by SQMRank3_ desc)SQMRank3,* from
(select org3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3_,cast((nullif(BOOKSQM3,0)/SQM3  )*100 as int) SQM3Percentage FROM(
SELECT DISTINCT org3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
select  org3 ,Sqm3M,isnull(BookSqm3,0) BookSqm3,  aa sqm3
from (select OrgUnitDay  org3, sum(BookSqm) aa, sum(BookSqm) Sqm3M,SqmTotal  BookSqm3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID  ) c)D GROUP BY org3)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select rank() over (order by SQMRank1_ desc) tot1mnthRank,rank() over (order by SQMRank1_ desc)SQMRank1,* from
(select org1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1_,cast((nullif(BOOKSQM1,0)/SQM1  )*100 as int) SQM1Percentage FROM(
SELECT DISTINCT org1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
select  org1 ,Sqm1M,isnull(BookSqm1,0)BookSqm1 ,  aa sqm1
from (select OrgUnitDay  org1, sum(BookSqm)  aa, sum(BookSqm) Sqm1M,SqmTotal BookSqm1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID  ) c)D GROUP BY org1)X)n ORDER BY tot1mnthRank DESC
END
end							 ---------------------------Actual/Booked--------------------------------

end                        
--LEFT OUTER JOIN
--(select SQMRank3 tot3mnthRank,* from
--(select org3,sectionid3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3,cast((nullif(BOOKSQM3,0)/SQM3  )*100 as numeric(3)) SQM3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
--select DISTINCT org3 ,sectionid3,Sqm3M,BookSqm3,  aa sqm3
--from (select OrgUnitDay  org3,sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Sqm3M,sum(BookSqm) BookSqm3 from PLANNING 
--where prodmonth between 201509 and 201511 and SectionID ='AAB'
--group by OrgUnitDay,sectionid)c)D GROUP BY org3, sectionid3)X)M)q on l.org12 =q.org3 

--LEFT OUTER JOIN
--(select SQMRank1 tot1mnthRank,* from
--(select org1,sectionid1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1,cast((nullif(BOOKSQM1,0)/SQM1  )*100 as numeric(3)) SQM1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
--select DISTINCT org1 ,sectionid1,Sqm1M,BookSqm1,  aa sqm1
--from (select OrgUnitDay  org1,sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Sqm1M,sum(BookSqm) BookSqm1 from PLANNING 
--where prodmonth= 201511 and SectionID ='AAB'
--group by OrgUnitDay,sectionid)c)D GROUP BY org1, sectionid1)X)n)j on l.org12 =j.org1  


--exec Report_CrewRanking_New '201511', '201509', '201501','AAB','B','1 Month','F','Total Mine'   
--exec Report_CrewRanking_New '201506', '201504', '201407','Tot','B','1 Month','T' ,'Total Mine' ,'F' 

--exec Report_CrewRanking_New '201606', '201604', '201507','FMAAAA','B','3 Month','F','FMAAAA:BReef Drives'  ,'T'  

--SELECT

--if @totalmine='Total Mine' and @persect ='T'
--BEGIN
--if @ratingBy='AB' AND @Status='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by SQMRank12_ desc) tot12mnthRank ,rank() over (order by SQMRank12_ desc) SQMRank12,* from(
--select org12,sectionid12,SQM12,BOOKSQM12, rank() over (order by SQM12,BOOKSQM12 asc) as SQMRank12_,cast((nullif(BOOKSQM12,0)/SQM12  )*100 as int) SQM12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Sqm12)SQM12,sum(BookSqm12) BOOKSQM12 FROM (
--select DISTINCT org12 ,sectionid12,Sqm12m,ISNULL(BookSqm12,0) BookSqm12,  aa sqm12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) Sqm12M,SqmTotal BookSqm12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank , sectionid12 ,org12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by SQMRank3_ desc) tot3mnthRank,rank() over (order by SQMRank3_ desc)SQMRank3,* from
--(select org3,sectionid3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3_,cast((nullif(BOOKSQM3,0)/SQM3)*100 as int) SQM3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
--select DISTINCT org3 ,sectionid3,Sqm3M,isnull(BookSqm3,0) BookSqm3,  aa sqm3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Sqm3M,SqmTotal  BookSqm3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank ,sectionid3, org3 
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by SQMRank1_ desc) tot1mnthRank,rank() over (order by SQMRank1_ desc)SQMRank1,* from
--(select org1,sectionid1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1_,cast((nullif(BOOKSQM1,0)/SQM1)*100 as int) SQM1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
--select DISTINCT org1 ,sectionid1,Sqm1M,isnull(BookSqm1,0)BookSqm1 ,  aa sqm1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Sqm1M,SqmTotal  BookSqm1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank,sectionid1 , org1 
--END
--END

--if @ratingBy='AB' AND @Status='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by SQMRank12_ desc) tot12mnthRank ,rank() over (order by SQMRank12_ desc)SQMRank12,* from(
--select org12,sectionid12,SQM12,BOOKSQM12, rank() over (order by SQM12,BOOKSQM12 asc) as SQMRank12_,cast((nullif(BOOKSQM12,0)/SQM12  )*100 as int) SQM12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Sqm12)SQM12,sum(BookSqm12) BOOKSQM12 FROM (
--select DISTINCT org12 ,sectionid12,Sqm12m,isnull(BookSqm12,0)BookSqm12,  aa sqm12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) Sqm12M,SqmTotal BookSqm12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank DESC, sectionid12 desc, org12 desc
--end


--if @orderby='3 Month' 
--begin
--select rank() over (order by SQMRank3_ desc) tot3mnthRank,rank() over (order by SQMRank3_ desc)SQMRank3,* from
--(select org3,sectionid3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3_,cast((nullif(BOOKSQM3,0)/SQM3)*100 as int) SQM3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
--select DISTINCT org3 ,sectionid3,Sqm3M,isnull(BookSqm3,0)BookSqm3,  aa sqm3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Sqm3M,SqmTotal  BookSqm3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank DESC, sectionid3 desc, org3 desc
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by SQMRank1_ desc) tot1mnthRank,rank() over (order by SQMRank1_ desc)SQMRank1,* from
--(select org1,sectionid1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1_,cast((nullif(BOOKSQM1,0)/SQM1)*100 as int) SQM1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
--select DISTINCT org1 ,sectionid1,Sqm1M,isnull(BookSqm1,0)BookSqm1,  aa sqm1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Sqm1M,SqmTotal  BookSqm1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank DESC, sectionid1 desc, org1 desc
--END
--end

--if @ratingBy='AP' AND @Status ='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by SQMRank12_ desc) tot12mnthRank ,rank() over (order by SQMRank12_ desc)SQMRank12,* from(
--select org12,sectionid12,SQM12,BOOKSQM12, rank() over (order by SQM12,BOOKSQM12 asc) as SQMRank12_,cast((nullif(BOOKSQM12,0)/SQM12  )*100 as int) SQM12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Sqm12)SQM12,sum(BookSqm12) BOOKSQM12 FROM (
--select DISTINCT org12 ,sectionid12,Sqm12m,isnull(BookSqm12,0)BookSqm12,  aa sqm12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) Sqm12M,SqmTotal BookSqm12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by SQM12Percentage, sectionid12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by SQMRank3_ desc) tot3mnthRank,rank() over (order by SQMRank3_ desc)SQMRank3,* from
--(select org3,sectionid3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3_,cast((nullif(BOOKSQM3,0)/SQM3)*100 as int) SQM3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
--select DISTINCT org3 ,sectionid3,Sqm3M,isnull(BookSqm3,0)BookSqm3,  aa sqm3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Sqm3M,SqmTotal  BookSqm3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by SQM3Percentage, sectionid3 
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by SQMRank1_ desc) tot1mnthRank, rank() over (order by SQMRank1_ desc)SQMRank1,* from
--(select org1,sectionid1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1_,cast((nullif(BOOKSQM1,0)/SQM1)*100 as int) SQM1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
--select DISTINCT org1 ,sectionid1,Sqm1M,isnull(BookSqm1,0)BookSqm1,  aa sqm1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Sqm1M,SqmTotal  BookSqm1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY SQM1Percentage, sectionid1 
--END
--end

--if @ratingBy='AP' AND @Status ='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by SQMRank12_ desc) tot12mnthRank ,rank() over (order by SQMRank12_ desc)SQMRank12,* from(
--select org12,sectionid12,SQM12,BOOKSQM12, rank() over (order by SQM12,BOOKSQM12 asc) as SQMRank12_,cast((nullif(BOOKSQM12,0)/SQM12  )*100 as int) SQM12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Sqm12)SQM12,sum(BookSqm12) BOOKSQM12 FROM (
--select DISTINCT org12 ,sectionid12,Sqm12m,isnull(BookSqm12,0)BookSqm12,  aa sqm12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) Sqm12M,SqmTotal BookSqm12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by SQM12Percentage DESC, sectionid12 desc
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by SQMRank3_ desc) tot3mnthRank,rank() over (order by SQMRank3_ desc)SQMRank3,* from
--(select org3,sectionid3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3_,cast((nullif(BOOKSQM3,0)/SQM3)*100 as int) SQM3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
--select DISTINCT org3 ,sectionid3,Sqm3M,isnull(BookSqm3,0)BookSqm3,  aa sqm3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Sqm3M,SqmTotal  BookSqm3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by SQM3Percentage DESC, sectionid3 desc
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by SQMRank1_ desc) tot1mnthRank,rank() over (order by SQMRank1_ desc)SQMRank1,* from
--(select org1,sectionid1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1_,cast((nullif(BOOKSQM1,0)/SQM1)*100 as int) SQM1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
--select DISTINCT org1 ,sectionid1,Sqm1M,isnull(BookSqm1,0)BookSqm1,  aa sqm1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Sqm1M,SqmTotal  BookSqm1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY SQM1Percentage DESC, sectionid1 desc
--END
--end
--END


if @totalmine!='Total Mine'   --------------------------- Actual/Planned---------------------------------
BEGIN
if @ratingBy='AB' AND @Status='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by SQM12Percentage DESC) tot12mnthRank, rank() over (order by SQM12Percentage DESC)SQMRank12,* from
(select org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(d1.BookSqm12,-99999) desc) as SqmRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as int) Sqm12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm12M,''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm12M,sum(SqmTotal) BookSqm12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank 
end



if @orderby='3 Month' 
begin
select * from
(select  rank() over (order by SQM3Percentage ASC) tot3mnthRank,rank() over (order by SQM3Percentage ASC)SQMRank3,* from
(select org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(d1.BookSqm3,-99999) desc) as SqmRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as int) Sqm3Percentage FROM(
select pp.OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm3M,''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by pp.OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm3M,sum(SqmTotal) BookSqm3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank
END

if @orderby='1 Month' 
begin
select * from
(select distinct rank() over (order by SQM1Percentage DESC) tot1mnthRank,rank() over (order by SQM1Percentage DESC)SQMRank1,* from
(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(d1.BookSqm1,-99999) desc) as SqmRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) Sqm1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm1M,''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month1 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm1M,sum(SqmTotal) BookSqm1 from survey s
where prodmonth between @Month1 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank
END
END

if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by SQM12Percentage DESC) tot12mnthRank, rank() over (order by SQM12Percentage DESC)SQMRank12,* from
(select org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(d1.BookSqm12,-99999) desc) as SqmRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as int) Sqm12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm12M,''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm12M,sum(SqmTotal) BookSqm12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank DESC
end


if @orderby='3 Month' 
begin
select * from
(select  rank() over (order by SQM3Percentage ASC) tot3mnthRank,rank() over (order by SQM3Percentage ASC)SQMRank3,* from
(select org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(d1.BookSqm3,-99999) desc) as SqmRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as int) Sqm3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm3M,''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm3M,sum(SqmTotal) BookSqm3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select * from
(select distinct rank() over (order by SQM1Percentage DESC) tot1mnthRank,rank() over (order by SQM1Percentage DESC)SQMRank1,* from
(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(d1.BookSqm1,-99999) desc) as SqmRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) Sqm1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm1M,''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month1 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm1M,sum(SqmTotal) BookSqm1 from survey s
where prodmonth between @Month1 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank DESC
END
end

if @ratingBy='P' AND @Status ='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by SQM12Percentage ASC) tot12mnthRank, rank() over (order by SQM12Percentage ASC)SQMRank12,* from
(select org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(d1.BookSqm12,-99999) desc) as SqmRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as int) Sqm12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm12M,''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm12M,sum(SqmTotal) BookSqm12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by SQM12Percentage
end



if @orderby='3 Month' 
begin
select * from
(select  rank() over (order by SQM3Percentage ASC) tot3mnthRank,rank() over (order by SQM3Percentage ASC)SQMRank3,* from
(select org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(d1.BookSqm3,-99999) desc) as SqmRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as int) Sqm3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm3M,''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm3M,sum(SqmTotal) BookSqm3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by SQM3Percentage
END



if @orderby='1 Month' 
begin
select * from
(select distinct rank() over (order by SQM1Percentage ASC) tot1mnthRank,rank() over (order by SQM1Percentage ASC)SQMRank1,* from
(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(d1.BookSqm1,-99999) desc) as SqmRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) Sqm1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm1M,''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month1 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm1M,sum(SqmTotal) BookSqm1 from survey s
where prodmonth between @Month1 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY SQM1Percentage
END
end

if @ratingBy='P' AND @Status ='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by SQM12Percentage DESC) tot12mnthRank, rank() over (order by SQM12Percentage DESC)SQMRank12,* from
(select org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(d1.BookSqm12,-99999) desc) as SqmRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as int) Sqm12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm12M,''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm12M,sum(SqmTotal) BookSqm12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by SQM12Percentage DESC
end



if @orderby='3 Month' 
begin
select * from
(select  rank() over (order by SQM3Percentage ASC) tot3mnthRank,rank() over (order by SQM3Percentage ASC)SQMRank3,* from
(select org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(d1.BookSqm3,-99999) desc) as SqmRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as int) Sqm3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm3M,''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm3M,sum(SqmTotal) BookSqm3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by SQM3Percentage DESC
END



if @orderby='1 Month' 
begin
select * from
(select distinct rank() over (order by SQM1Percentage DESC) tot1mnthRank,rank() over (order by SQM1Percentage DESC)SQMRank1,* from
(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(d1.BookSqm1,-99999) desc) as SqmRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) Sqm1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Sqm) aa, sum(p.Sqm) Sqm1M,''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month1 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY in (0,9)--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Sqm1M,sum(SqmTotal) BookSqm1 from survey s
where prodmonth between @Month1 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY in (0,9)--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY SQM1Percentage DESC
END
end
END

if @totalmine='Total Mine' AND @persect ='F'
BEGIN
if @ratingBy='AP' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   rank() over (order by SQM12Percentage DESC) tot12mnthRank, rank() over (order by SQM12Percentage DESC)SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'  )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union
select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data


left outer join 
(select * from
(select  rank() over (order by SQM3Percentage desc) tot3mnthRank,rank() over (order by SQM3Percentage desc)SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)  
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3


left outer join 
(select distinct rank() over (order by SQM1Percentage desc) tot1mnthRank,rank() over (order by SQM1Percentage desc)SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100 as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'   )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12  ORDER BY SQM12Percentage desc'

--SELECT @TheQuery
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery='select * from
(select * from
(select  rank() over (order by SQM3Percentage DESC) tot3mnthRank,rank() over (order by SQM3Percentage DESC)SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+')d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct rank() over (order by SQM1Percentage desc) tot1mnthRank,rank() over (order by SQM1Percentage desc)SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100 as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+')d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1

 

left outer join 
(select   rank() over (order by SQM12Percentage desc) tot12mnthRank, rank() over (order by SQM12Percentage desc)SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY SQM3Percentage desc'

--SELECT @TheQuery
exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from (
select distinct rank() over (order by SQM1Percentage DESC) tot1mnthRank,rank() over (order by SQM1Percentage DESC)SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100 as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'   )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select  rank() over (order by SQM3Percentage DESC) tot3mnthRank,rank() over (order by SQM3Percentage DESC)SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'  )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)  
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   rank() over (order by SQM12Percentage DESC) tot12mnthRank,rank() over (order by  SQM12Percentage DESC)SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY SQM1Percentage desc' 
--SELECT @TheQuery
exec (@TheQuery)
END
end

if @ratingBy='AP' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by SQM12Percentage desc) tot12mnthRank ,rank() over (order by SQM12Percentage desc)SQMRank12,* from(
select org12,SQM12,BOOKSQM12, rank() over (order by SQM12,BOOKSQM12 asc) as SQMRank12_,cast((nullif(BOOKSQM12,0)/SQM12  )*100 as int) SQM12Percentage FROM(
SELECT DISTINCT org12,SUM(Sqm12)SQM12,sum(BookSqm12) BOOKSQM12 FROM (
select  org12 ,Sqm12m,ISNULL(BookSqm12,0) BookSqm12,  aa sqm12
from (select OrgUnitDay  org12, sum(p.Sqm) aa, sum(p.Sqm) Sqm12M,SqmTotal BookSqm12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID ) c)D GROUP BY org12)X)l  order by SQM12Percentage 
END

if @orderby='3 Month' 
begin
select rank() over (order by SQM3Percentage desc) tot3mnthRank,rank() over (order by SQM3Percentage desc)SQMRank3,* from
(select org3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3_,cast((nullif(BOOKSQM3,0)/SQM3  )*100 as int) SQM3Percentage FROM(
SELECT DISTINCT org3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
select  org3 ,Sqm3M,isnull(BookSqm3,0) BookSqm3,  aa sqm3
from (select OrgUnitDay  org3, sum(p.Sqm) aa, sum(p.Sqm) Sqm3M,SqmTotal  BookSqm3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID ) c)D GROUP BY org3)X)M order by SQM3Percentage 
END

if @orderby='1 Month' 
begin
select rank() over (order by SQM1Percentage desc) tot1mnthRank,rank() over (order by SQM1Percentage desc)SQMRank1,* from
(select org1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1_,cast((nullif(BOOKSQM1,0)/SQM1  )*100 as int) SQM1Percentage FROM(
SELECT DISTINCT org1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
select  org1 ,Sqm1M,isnull(BookSqm1,0)BookSqm1 ,  aa sqm1
from (select OrgUnitDay  org1, sum(p.Sqm) aa, sum(p.Sqm) Sqm1M,SqmTotal  BookSqm1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID ) c)D GROUP BY org1)X)n ORDER BY SQM1Percentage 
END
end

if @ratingBy='AB' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select  SQMRank12_  tot12mnthRank,SQMRank12_ SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data
 

left outer join 
(select * from
(select  SQMRank3_  tot3mnthRank,SQMRank3_ SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3 


left outer join 
(select distinct SQMRank1_  tot1mnthRank,SQMRank1_ SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100 as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'   )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12   ORDER BY tot12mnthRank asc,SQM12Percentage DESC'
--SELECT (@TheQuery)
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery='select * from
(select * from
(select  SQMRank3_  tot3mnthRank,SQMRank3_ SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+')d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct SQMRank1_  tot1mnthRank,SQMRank1_ SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100 as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'   org1,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'   )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select  SQMRank12_  tot12mnthRank,SQMRank12_ SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'   )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)  
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY tot3mnthRank asc,SQM3Percentage DESC'
--SELECT @TheQuery
exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from (
select distinct SQMRank1_  tot1mnthRank,SQMRank1_ SQMRank1,* from
--(select org1,d.Sqm1M,d1.BookSqm1, rank() over (order by d.Sqm1M,d1.BookSqm1 asc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M)*100 as int) SQM1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1, d.Sqm1M,d1.BookSqm1, rank() over (order by isnull(isnull(d1.BookSqm1,0),-99999) desc) as SQMRank1_,cast((nullif(d1.BookSqm1,0)/d.Sqm1M  )*100 as NUMERIC(18,2)) SQM1Percentage FROM(
select '+@type+'  org1,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm1M,''''BookSqm1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY in (0,9)     
group by '+@groupby+'   )d 
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth='+@Month1+' AND s.ACTIVITY in (0,9) 
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data 



left outer join 
(select * from
(select  SQMRank3_  tot3mnthRank,SQMRank3_ SQMRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Sqm3M,d1.BookSqm3, rank() over (order by isnull(isnull(d1.BookSqm3,0),-99999) desc) as SQMRank3_,cast((nullif(d1.BookSqm3,0)/d.Sqm3M)*100 as NUMERIC(18,2)) SQM3Percentage FROM(
select '+@type+'  org3,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm3M,''''BookSqm3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where 
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  
group by '+@groupby+'  )d
 FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm3M,sum(SqmTotal) BookSqm3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select  SQMRank12_  tot12mnthRank,SQMRank12_ SQMRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Sqm12M,d1.BookSqm12, rank() over (order by isnull(isnull(d1.BookSqm12,0),-99999) desc) as SQMRank12_,cast((nullif(d1.BookSqm12,0)/d.Sqm12M)*100 as NUMERIC(18,2)) SQM12Percentage FROM(
select '+@type+'  org12,''''Prodmonth , sum(P.Sqm) aa, sum(P.Sqm) Sqm12M,''''BookSqm12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9) 
group by '+@groupby+'  )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth, ''''aa,''''Sqm12M,sum(SqmTotal) BookSqm12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where S.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY in (0,9)   
group by '+@groupby1 +'

union

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth, ''''aa,''''Sqm1M,sum(SqmTotal) BookSqm12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY in (0,9)  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY in (0,9)  group by '+@groupby1 +')d1 on
d.Prodmonth =d1.ProdMonth and
--d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY tot1mnthRank asc,SQM1Percentage DESC'
--SELECT @TheQuery
exec (@TheQuery)
END
end


if @ratingBy='AB' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by SQMRank12_ desc) tot12mnthRank ,rank() over (order by SQMRank12_ desc)SQMRank12,* from(
select org12,SQM12,BOOKSQM12, rank() over (order by SQM12,BOOKSQM12 asc) as SQMRank12_,cast((nullif(BOOKSQM12,0)/SQM12  )*100 as int) SQM12Percentage FROM(
SELECT DISTINCT org12,SUM(Sqm12)SQM12,sum(BookSqm12) BOOKSQM12 FROM (
select  org12 ,Sqm12m,ISNULL(BookSqm12,0) BookSqm12,  aa sqm12
from (select OrgUnitDay  org12, sum(p.Sqm) aa, sum(p.Sqm) Sqm12M,SqmTotal BookSqm12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.ACTIVITY in (0,9) --and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID  ) c)D GROUP BY org12)X)l  order by tot12mnthRank DESC
END

if @orderby='3 Month' 
begin
select rank() over (order by SQMRank3_ desc) tot3mnthRank,rank() over (order by SQMRank3_ desc)SQMRank3,* from
(select org3,SQM3,BOOKSQM3, rank() over (order by SQM3,BOOKSQM3 asc) as SQMRank3_,cast((nullif(BOOKSQM3,0)/SQM3  )*100 as int) SQM3Percentage FROM(
SELECT DISTINCT org3,SUM(Sqm3)SQM3,sum(BookSqm3) BOOKSQM3 FROM (
select  org3 ,Sqm3M,isnull(BookSqm3,0) BookSqm3,  aa sqm3
from (select OrgUnitDay  org3, sum(p.Sqm) aa, sum(p.Sqm) Sqm3M,SqmTotal  BookSqm3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID  ) c)D GROUP BY org3)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select rank() over (order by SQMRank1_ desc) tot1mnthRank,rank() over (order by SQMRank1_ desc)SQMRank1,* from
(select org1,SQM1,BOOKSQM1, rank() over (order by SQM1,BOOKSQM1 asc) as SQMRank1_,cast((nullif(BOOKSQM1,0)/SQM1  )*100 as int) SQM1Percentage FROM(
SELECT DISTINCT org1,SUM(Sqm1)SQM1,sum(BookSqm1) BOOKSQM1 FROM (
select  org1 ,Sqm1M,isnull(BookSqm1,0)BookSqm1 ,  aa sqm1
from (select OrgUnitDay  org1, sum(p.Sqm)  aa, sum(p.Sqm) Sqm1M,SqmTotal BookSqm1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.ACTIVITY in (0,9)--and SectionID =@sectionid and activity=0
group by OrgUnitDay,SqmTotal,s.WorkplaceID  ) c)D GROUP BY org1)X)n ORDER BY tot1mnthRank DESC
END
end

end                          --------------------------- Actual/Planned---------------------------------
go

create PROCEDURE [dbo].[Report_CrewRanking_New_Dev]
--declare 
@Month1 VARCHAR(100), @Month3 VARCHAR(100),@Month12 VARCHAR(100),@sectionid varchar(80),@ratingBy varchar(5),@orderby varchar(10),@Status varchar(10),@totalmine varchar(10),@persect varchar(10),
@type varchar(100)
AS
--set @Month1=201611
--set @Month3=201608
--set @Month12=201512
--set @sectionid='Total Mine'
--set @ratingBy='AP'
--set @orderby='1 Month'
--set @Status='T'
--set @totalmine='Total Mine'
--set @persect ='F'
--set @type='sc.SectionID_1+'':''+Name_1'

SET ARITHABORT OFF 
SET ANSI_WARNINGS OFF
--exec Report_CrewRanking_New '201607', '201605', '201508','AAD','B','1 Month','T' ,'AAD:E Tshemese' ,'T'  
--exec Report_CrewRanking_New '201607', '201605', '201508','Total Mine','AB','1 Month','T' ,'Total Mine' ,'F' 

declare @type1 varchar(100)
declare @type2 varchar(100)
DECLARE @groupby varchar(100)
DECLARE @groupby1 varchar(100)
if @type='OrgUnitDay'
begin
set @type1='CrewMorning'
set @groupby='OrgUnitDay'
set @groupby1='CrewMorning'
end

if @type='sc.SectionID_2+'':''+Name_2'
begin
set @groupby='sc.SectionID_2,Name_2'
set @type1 ='sc.SectionID_2+'':''+Name_2'
set @type2 ='Name_2'
set @groupby1 =@groupby
end

if @type='sc.SectionID_1+'':''+Name_1'
begin
set @groupby='sc.SectionID_1,Name_1'
set @type1 ='sc.SectionID_1+'':''+Name_1'
set @type2 ='Name_1'
set @groupby1 =@groupby
end


declare @TheQuery varchar(max) 

--if @totalmine='Total Mine' and @persect ='T'
--BEGIN
--if @ratingBy='B' AND @Status='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc) MetresadvanceRank12,* from(
--select org12,sectionid12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
--select DISTINCT org12 ,sectionid12,Metresadvance12m,ISNULL(BookMetresadvance12,0) BookMetresadvance12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,SqmTotal BookMetresadvance12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank , sectionid12 ,org12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3_,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3)*100 as int) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,isnull(BookMetresadvance3,0) BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,SqmTotal  BookMetresadvance3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank ,sectionid3, org3 
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank,rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1)*100 as int) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1 ,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,SqmTotal  BookMetresadvance1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank,sectionid1 , org1 
--END
--END

--if @ratingBy='B' AND @Status='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc)MetresadvanceRank12,* from(
--select org12,sectionid12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
--select DISTINCT org12 ,sectionid12,Metresadvance12m,isnull(BookMetresadvance12,0)BookMetresadvance12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,SqmTotal BookMetresadvance12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank DESC, sectionid12 desc, org12 desc

--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3)*100 as int) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,isnull(BookMetresadvance3,0)BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,SqmTotal  BookMetresadvance3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank DESC, sectionid3 desc, org3 desc
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank,rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1)*100 as int) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,SqmTotal  BookMetresadvance1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank DESC, sectionid1 desc, org1 desc
--END
--end

--if @ratingBy='P' AND @Status ='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc)MetresadvanceRank12,* from(
--select org12,sectionid12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
--select DISTINCT org12 ,sectionid12,Metresadvance12m,isnull(BookMetresadvance12,0)BookMetresadvance12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,SqmTotal BookMetresadvance12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by Metresadvance12Percentage, sectionid12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3)*100 as int) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,isnull(BookMetresadvance3,0)BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,SqmTotal  BookMetresadvance3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by Metresadvance3Percentage, sectionid3 
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank, rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1)*100 as int) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,SqmTotal  BookMetresadvance1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY Metresadvance1Percentage, sectionid1 
--END
--end

--if @ratingBy='P' AND @Status ='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc)MetresadvanceRank12,* from(
--select org12,sectionid12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
--select DISTINCT org12 ,sectionid12,Metresadvance12m,isnull(BookMetresadvance12,0)BookMetresadvance12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,SqmTotal BookMetresadvance12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by Metresadvance12Percentage DESC, sectionid12 desc
--end


--if @orderby='3 Month' 
--begin
--select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3)*100 as int) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,isnull(BookMetresadvance3,0)BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,SqmTotal  BookMetresadvance3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by Metresadvance3Percentage DESC, sectionid3 desc
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank,rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1)*100 as int) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,SqmTotal  BookMetresadvance1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY Metresadvance1Percentage DESC, sectionid1 desc
--END
--end
--END

if @totalmine!='Total Mine'             ------------------Actual/booked---------------------
BEGIN
if @ratingBy='B' AND @Status='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(d1.BookMetresadvance12,-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/d.Metresadvance12M)*100 as int) Metresadvance12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank 
end



if @orderby='3 Month' 
begin
select * from
(select   MetresadvanceRank3_  tot3mnthRank, MetresadvanceRank3_ MetresadvanceRank3,* from
(select org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(d1.BookMetresadvance3,-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/d.Metresadvance3M)*100 as int) Metresadvance3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank 
END

if @orderby='1 Month' 
begin
select * from
(select   MetresadvanceRank1_  tot1mnthRank, MetresadvanceRank1_ MetresadvanceRank1,* from
(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(d1.BookMetresadvance1,-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank
END
END

if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(d1.BookMetresadvance12,-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/d.Metresadvance12M)*100 as int) Metresadvance12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank DESC
end


if @orderby='3 Month' 
begin
select * from
(select   MetresadvanceRank3_  tot3mnthRank, MetresadvanceRank3_ MetresadvanceRank3,* from
(select org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(d1.BookMetresadvance3,-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/d.Metresadvance3M)*100 as int) Metresadvance3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select * from
(select   MetresadvanceRank1_  tot1mnthRank, MetresadvanceRank1_ MetresadvanceRank1,* from
(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(d1.BookMetresadvance1,-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank DESC
END
end

if @ratingBy='P' AND @Status ='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by Metresadvance12Percentage ASC) tot12mnthRank, rank() over (order by Metresadvance12Percentage ASC)MetresadvanceRank12,* from
(select org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(d1.BookMetresadvance12,-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/d.Metresadvance12M)*100 as int) Metresadvance12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by Metresadvance12Percentage
end



if @orderby='3 Month' 
begin
select * from
(select   rank() over (order by Metresadvance3Percentage ASC) tot3mnthRank, rank() over (order by Metresadvance3Percentage ASC)MetresadvanceRank3,* from
(select org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(d1.BookMetresadvance3,-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/d.Metresadvance3M)*100 as int) Metresadvance3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by Metresadvance3Percentage
END



if @orderby='1 Month' 
begin
select * from
(select   rank() over (order by Metresadvance1Percentage ASC) tot1mnthRank, rank() over (order by Metresadvance1Percentage ASC)MetresadvanceRank1,* from
(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(d1.BookMetresadvance1,-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY Metresadvance1Percentage
END
end

if @ratingBy='P' AND @Status ='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by Metresadvance12Percentage DESC) tot12mnthRank, rank() over (order by Metresadvance12Percentage DESC)MetresadvanceRank12,* from
(select org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(d1.BookMetresadvance12,-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/d.Metresadvance12M)*100 as int) Metresadvance12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by Metresadvance12Percentage DESC
end




if @orderby='3 Month' 
begin
select * from
(select   rank() over (order by Metresadvance3Percentage DESC) tot3mnthRank, rank() over (order by Metresadvance3Percentage DESC)MetresadvanceRank3,* from
(select org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(d1.BookMetresadvance3,-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/d.Metresadvance3M)*100 as int) Metresadvance3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by Metresadvance3Percentage DESC
END



if @orderby='1 Month' 
begin
select * from
(select   rank() over (order by Metresadvance1Percentage DESC) tot1mnthRank, rank() over (order by Metresadvance1Percentage DESC)MetresadvanceRank1,* from
(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(d1.BookMetresadvance1,-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY Metresadvance1Percentage DESC
END
end
END

if @totalmine='Total Mine' AND @persect ='F'
BEGIN
if @ratingBy='P' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   rank() over (order by Metresadvance12Percentage desc) tot12mnthRank, rank() over (order by Metresadvance12Percentage desc)MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data


left outer join 
(select * from
(select   rank() over (order by Metresadvance3Percentage DESC) tot3mnthRank, rank() over (order by Metresadvance3Percentage DESC)MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3


left outer join 
(select distinct rank() over (order by Metresadvance1Percentage desc) tot1mnthRank,rank() over (order by Metresadvance1Percentage desc)MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct  '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12  ORDER BY Metresadvance12Percentage desc'
--select @TheQuery
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select   rank() over (order by Metresadvance3Percentage DESC) tot3mnthRank, rank() over (order by Metresadvance3Percentage DESC)MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct rank() over (order by Metresadvance1Percentage desc) tot1mnthRank,rank() over (order by Metresadvance1Percentage desc)MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select   rank() over (order by Metresadvance12Percentage desc) tot12mnthRank, rank() over (order by Metresadvance12Percentage desc)MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY Metresadvance3Percentage desc'

exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from 
(select distinct rank() over (order by Metresadvance1Percentage desc) tot1mnthRank,rank() over (order by Metresadvance1Percentage desc)MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select   rank() over (order by Metresadvance3Percentage desc) tot3mnthRank, rank() over (order by Metresadvance3Percentage desc)MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   rank() over (order by Metresadvance12Percentage desc) tot12mnthRank, rank() over (order by Metresadvance12Percentage desc)MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY Metresadvance1Percentage desc'

--select @TheQuery
exec (@TheQuery)
END
end

if @ratingBy='B' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data
 

left outer join 
(select * from
(select  MetresadvanceRank3_  tot3mnthRank,MetresadvanceRank3_ MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3 
 

left outer join 
(select distinct MetresadvanceRank1_  tot1mnthRank,MetresadvanceRank1_ MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+'  ,P.Activity )d
full outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12   ORDER BY tot12mnthRank asc'
--select @TheQuery
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select  MetresadvanceRank3_  tot3mnthRank,MetresadvanceRank3_ MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct MetresadvanceRank1_  tot1mnthRank,MetresadvanceRank1_ MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth  between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY tot3mnthRank asc'

exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from (
select distinct MetresadvanceRank1_  tot1mnthRank,MetresadvanceRank1_ MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select  MetresadvanceRank3_  tot3mnthRank,MetresadvanceRank3_ MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY tot1mnthRank asc'

--select @TheQuery
exec (@TheQuery)
END
end


if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc)MetresadvanceRank12,* from(
select org12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
SELECT DISTINCT org12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
select  org12 ,Metresadvance12m,ISNULL(BookMetresadvance12,0) BookMetresadvance12,  aa Metresadvance12
from (select OrgUnitDay  org12, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance12M,TotalMetres BookMetresadvance12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org12)X)l  order by tot12mnthRank DESC
END

if @orderby='3 Month' 
begin
select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3,* from
(select org3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3  )*100 as int) Metresadvance3Percentage FROM(
SELECT DISTINCT org3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
select  org3 ,Metresadvance3M,isnull(BookMetresadvance3,0) BookMetresadvance3,  aa Metresadvance3
from (select OrgUnitDay  org3, sum(BookMetresadvance) aa, sum(BookMetresadvance) Metresadvance3M,TotalMetres  BookMetresadvance3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org3)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank,rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
(select org1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1  )*100 as int) Metresadvance1Percentage FROM(
SELECT DISTINCT org1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
select  org1 ,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1 ,  aa Metresadvance1
from (select OrgUnitDay  org1, sum(p.BookMetresadvance)  aa, sum(p.BookMetresadvance) Metresadvance1M,TotalMetres BookMetresadvance1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org1)X)n ORDER BY tot1mnthRank DESC
END
end

end
--LEFT OUTER JOIN
--(select MetresadvanceRank3 tot3mnthRank,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3,cast((nullif(BookMetresadvance3,0)/Metresadvance3  )*100 as numeric(3)) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,sectionid sectionid3, sum(Metresadvance) aa, sum(Metresadvance) Metresadvance3m,sum(BookMetresadvance ) BookMetresadvance3 from PLANNING 
--where prodmonth between 201509 and 201511 and SectionID ='AAB'
--group by OrgUnitDay,sectionid)c)D GROUP BY org3, sectionid3)X)M)q on l.org12 =q.org3 

--LEFT OUTER JOIN
--(select MetresadvanceRank1 tot1mnthRank,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1,cast((nullif(BookMetresadvance1,0)/Metresadvance1  )*100 as numeric(3)) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,BookMetresadvance1,  aa Metresadvance1
--from (select OrgUnitDay  org1,sectionid sectionid1, sum(Metresadvance) aa, sum(Metresadvance) Metresadvance1m,sum(BookMetresadvance ) BookMetresadvance1 from PLANNING 
--where prodmonth= 201511 and SectionID ='AAB'
--group by OrgUnitDay,sectionid)c)D GROUP BY org1, sectionid1)X)n)j on l.org12 =j.org1  


--exec Report_CrewRanking_New '201511', '201509', '201501','AAB','B','1 Month','F','Total Mine'   
--exec Report_CrewRanking_New '201506', '201504', '201407','Tot','B','1 Month','T' ,'Total Mine' ,'F' 

--exec Report_CrewRanking_New '201606', '201604', '201507','FMAAAA','B','3 Month','F','FMAAAA:BReef Drives'  ,'T'  

--SELECT

--if @totalmine='Total Mine' and @persect ='T'
--BEGIN
--if @ratingBy='AB' AND @Status='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc) MetresadvanceRank12,* from(
--select org12,sectionid12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
--select DISTINCT org12 ,sectionid12,Metresadvance12m,ISNULL(BookMetresadvance12,0) BookMetresadvance12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) Metresadvance12M,SqmTotal BookMetresadvance12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank , sectionid12 ,org12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3_,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3)*100 as int) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,isnull(BookMetresadvance3,0) BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Metresadvance3M,SqmTotal  BookMetresadvance3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank ,sectionid3, org3 
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank,rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1)*100 as int) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1 ,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Metresadvance1M,SqmTotal  BookMetresadvance1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank,sectionid1 , org1 
--END
--END

--if @ratingBy='AB' AND @Status='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc)MetresadvanceRank12,* from(
--select org12,sectionid12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
--select DISTINCT org12 ,sectionid12,Metresadvance12m,isnull(BookMetresadvance12,0)BookMetresadvance12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) Metresadvance12M,SqmTotal BookMetresadvance12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank DESC, sectionid12 desc, org12 desc
--end


--if @orderby='3 Month' 
--begin
--select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3)*100 as int) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,isnull(BookMetresadvance3,0)BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Metresadvance3M,SqmTotal  BookMetresadvance3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank DESC, sectionid3 desc, org3 desc
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank,rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1)*100 as int) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Metresadvance1M,SqmTotal  BookMetresadvance1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank DESC, sectionid1 desc, org1 desc
--END
--end

--if @ratingBy='AP' AND @Status ='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc)MetresadvanceRank12,* from(
--select org12,sectionid12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
--select DISTINCT org12 ,sectionid12,Metresadvance12m,isnull(BookMetresadvance12,0)BookMetresadvance12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) Metresadvance12M,SqmTotal BookMetresadvance12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by Metresadvance12Percentage, sectionid12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3)*100 as int) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,isnull(BookMetresadvance3,0)BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Metresadvance3M,SqmTotal  BookMetresadvance3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by Metresadvance3Percentage, sectionid3 
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank, rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1)*100 as int) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Metresadvance1M,SqmTotal  BookMetresadvance1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY Metresadvance1Percentage, sectionid1 
--END
--end

--if @ratingBy='AP' AND @Status ='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc)MetresadvanceRank12,* from(
--select org12,sectionid12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
--select DISTINCT org12 ,sectionid12,Metresadvance12m,isnull(BookMetresadvance12,0)BookMetresadvance12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) Metresadvance12M,SqmTotal BookMetresadvance12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by Metresadvance12Percentage DESC, sectionid12 desc
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3,* from
--(select org3,sectionid3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3)*100 as int) Metresadvance3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
--select DISTINCT org3 ,sectionid3,Metresadvance3M,isnull(BookMetresadvance3,0)BookMetresadvance3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) Metresadvance3M,SqmTotal  BookMetresadvance3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by Metresadvance3Percentage DESC, sectionid3 desc
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank,rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
--(select org1,sectionid1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1)*100 as int) Metresadvance1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
--select DISTINCT org1 ,sectionid1,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) Metresadvance1M,SqmTotal  BookMetresadvance1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY Metresadvance1Percentage DESC, sectionid1 desc
--END
--end
--END

-----------

if @totalmine!='Total Mine'      ------------------ Actual/Planned----------------------------------------------
BEGIN
if @ratingBy='AB' AND @Status='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(d1.BookMetresadvance12,-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/d.Metresadvance12M)*100 as int) Metresadvance12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank 
end



if @orderby='3 Month' 
begin
select * from
(select   MetresadvanceRank3_  tot3mnthRank, MetresadvanceRank3_ MetresadvanceRank3,* from
(select org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(d1.BookMetresadvance3,-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/d.Metresadvance3M)*100 as int) Metresadvance3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank 
END

if @orderby='1 Month' 
begin
select * from
(select   MetresadvanceRank1_  tot1mnthRank, MetresadvanceRank1_ MetresadvanceRank1,* from
(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(d1.BookMetresadvance1,-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance1M,''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank
END
END

if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(d1.BookMetresadvance12,-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/d.Metresadvance12M)*100 as int) Metresadvance12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank DESC
end


if @orderby='3 Month' 
begin
select * from
(select   MetresadvanceRank3_  tot3mnthRank, MetresadvanceRank3_ MetresadvanceRank3,* from
(select org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(d1.BookMetresadvance3,-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/d.Metresadvance3M)*100 as int) Metresadvance3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select * from
(select   MetresadvanceRank1_  tot1mnthRank, MetresadvanceRank1_ MetresadvanceRank1,* from
(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(d1.BookMetresadvance1,-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance1M,''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank DESC
END
end

if @ratingBy='P' AND @Status ='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by Metresadvance12Percentage ASC) tot12mnthRank, rank() over (order by Metresadvance12Percentage ASC)MetresadvanceRank12,* from
(select org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(d1.BookMetresadvance12,-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/d.Metresadvance12M)*100 as int) Metresadvance12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by Metresadvance12Percentage
end



if @orderby='3 Month' 
begin
select * from
(select   rank() over (order by Metresadvance3Percentage ASC) tot3mnthRank, rank() over (order by Metresadvance3Percentage ASC)MetresadvanceRank3,* from
(select org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(d1.BookMetresadvance3,-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/d.Metresadvance3M)*100 as int) Metresadvance3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by Metresadvance3Percentage
END



if @orderby='1 Month' 
begin
select * from
(select   rank() over (order by Metresadvance1Percentage ASC) tot1mnthRank, rank() over (order by Metresadvance1Percentage ASC)MetresadvanceRank1,* from
(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(d1.BookMetresadvance1,-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance1M,''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY Metresadvance1Percentage
END
end

if @ratingBy='P' AND @Status ='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by Metresadvance12Percentage DESC) tot12mnthRank, rank() over (order by Metresadvance12Percentage DESC)MetresadvanceRank12,* from
(select org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(d1.BookMetresadvance12,-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/d.Metresadvance12M)*100 as int) Metresadvance12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by Metresadvance12Percentage DESC
end




if @orderby='3 Month' 
begin
select * from
(select   rank() over (order by Metresadvance3Percentage DESC) tot3mnthRank, rank() over (order by Metresadvance3Percentage DESC)MetresadvanceRank3,* from
(select org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(d1.BookMetresadvance3,-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/d.Metresadvance3M)*100 as int) Metresadvance3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by Metresadvance3Percentage DESC
END



if @orderby='1 Month' 
begin
select * from
(select   rank() over (order by Metresadvance1Percentage DESC) tot1mnthRank, rank() over (order by Metresadvance1Percentage DESC)MetresadvanceRank1,* from
(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(d1.BookMetresadvance1,-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance1M,''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY Metresadvance1Percentage DESC
END
end
END


if @totalmine='Total Mine' AND @persect ='F'
BEGIN
if @ratingBy='AP' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   rank() over (order by Metresadvance12Percentage desc) tot12mnthRank, rank() over (order by Metresadvance12Percentage desc)MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data


left outer join 
(select * from
(select   rank() over (order by Metresadvance3Percentage desc) tot3mnthRank, rank() over (order by Metresadvance3Percentage desc)MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth  where
p.prodmonth  between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3 


left outer join 
(select distinct rank() over (order by Metresadvance1Percentage desc) tot1mnthRank,rank() over (order by Metresadvance1Percentage desc)MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12  ORDER BY Metresadvance12Percentage desc'
--select @TheQuery
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select   rank() over (order by Metresadvance3Percentage desc) tot3mnthRank, rank() over (order by Metresadvance3Percentage DESC)MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss  inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct rank() over (order by Metresadvance1Percentage desc) tot1mnthRank,rank() over (order by Metresadvance1Percentage desc)MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1 


left outer join 
(select   rank() over (order by Metresadvance12Percentage desc) tot12mnthRank, rank() over (order by Metresadvance12Percentage desc)MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY Metresadvance3Percentage desc' 
--select @TheQuery
exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from 
(select distinct rank() over (order by Metresadvance1Percentage desc) tot1mnthRank,rank() over (order by Metresadvance1Percentage desc)MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select   rank() over (order by Metresadvance3Percentage desc) tot3mnthRank, rank() over (order by Metresadvance3Percentage desc)MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =s.SectionID and
sc.Prodmonth =s.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   rank() over (order by Metresadvance12Percentage desc) tot12mnthRank, rank() over (order by Metresadvance12Percentage desc)MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY Metresadvance1Percentage desc' 
--SELECT @TheQuery
exec (@TheQuery)
END
end

if @ratingBy='AP' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by Metresadvance12Percentage asc) tot12mnthRank ,rank() over (order by Metresadvance12Percentage asc)MetresadvanceRank12,* from(
select org12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
SELECT DISTINCT org12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
select  org12 ,Metresadvance12m,ISNULL(BookMetresadvance12,0) BookMetresadvance12,  aa Metresadvance12
from (select OrgUnitDay  org12, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,TotalMetres BookMetresadvance12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID ) c)D GROUP BY org12)X)l  order by Metresadvance12Percentage 
END

if @orderby='3 Month' 
begin
select rank() over (order by Metresadvance3Percentage asc) tot3mnthRank,rank() over (order by Metresadvance3Percentage asc)MetresadvanceRank3,* from
(select org3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3  )*100 as int) Metresadvance3Percentage FROM(
SELECT DISTINCT org3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
select  org3 ,Metresadvance3M,isnull(BookMetresadvance3,0) BookMetresadvance3,  aa Metresadvance3
from (select OrgUnitDay  org3, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,TotalMetres  BookMetresadvance3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID ) c)D GROUP BY org3)X)M order by Metresadvance3Percentage 
END

if @orderby='1 Month' 
begin
select rank() over (order by Metresadvance1Percentage asc) tot1mnthRank,rank() over (order by Metresadvance1Percentage asc)MetresadvanceRank1,* from
(select org1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1  )*100 as int) Metresadvance1Percentage FROM(
SELECT DISTINCT org1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
select  org1 ,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1 ,  aa Metresadvance1
from (select OrgUnitDay  org1, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance1M,TotalMetres  BookMetresadvance1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID ) c)D GROUP BY org1)X)n ORDER BY Metresadvance1Percentage 
END
end

if @ratingBy='AB' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data


left outer join 
(select * from
(select  MetresadvanceRank3_  tot3mnthRank,MetresadvanceRank3_ MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3
 

left outer join 
(select distinct MetresadvanceRank1_  tot1mnthRank,MetresadvanceRank1_ MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12   ORDER BY tot12mnthRank asc'

exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select  MetresadvanceRank3_  tot3mnthRank,MetresadvanceRank3_ MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
left outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct MetresadvanceRank1_  tot1mnthRank,MetresadvanceRank1_ MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M ,0) )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY tot3mnthRank asc'

exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from (
select distinct MetresadvanceRank1_  tot1mnthRank,MetresadvanceRank1_ MetresadvanceRank1,* from
--(select org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by d.Metresadvance1M,d1.BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/d.Metresadvance1M)*100 as int) Metresadvance1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.Metresadvance1M,d1.BookMetresadvance1, rank() over (order by isnull(isnull(d1.BookMetresadvance1,0),-99999) desc) as MetresadvanceRank1_,cast((nullif(d1.BookMetresadvance1,0)/nullif(d.Metresadvance1M,0)  )*100 as NUMERIC(18,2)) Metresadvance1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance1M,''''BookMetresadvance1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance1M,sum(TotalMetres) BookMetresadvance1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select  MetresadvanceRank3_  tot3mnthRank,MetresadvanceRank3_ MetresadvanceRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.Metresadvance3M,d1.BookMetresadvance3, rank() over (order by isnull(isnull(d1.BookMetresadvance3,0),-99999) desc) as MetresadvanceRank3_,cast((nullif(d1.BookMetresadvance3,0)/nullif(d.Metresadvance3M,0))*100 as NUMERIC(18,2)) Metresadvance3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance3M,''''BookMetresadvance3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance3M,sum(TotalMetres) BookMetresadvance3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   MetresadvanceRank12_  tot12mnthRank, MetresadvanceRank12_ MetresadvanceRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.Metresadvance12M,d1.BookMetresadvance12, rank() over (order by isnull(isnull(d1.BookMetresadvance12,0),-99999) desc) as MetresadvanceRank12_,cast((nullif(d1.BookMetresadvance12,0)/nullif(d.Metresadvance12M,0))*100 as NUMERIC(18,2)) Metresadvance12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) Metresadvance12M,''''BookMetresadvance12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''Metresadvance12M,sum(TotalMetres) BookMetresadvance12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY tot1mnthRank asc'

exec (@TheQuery)
END
end

if @ratingBy='AB' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by MetresadvanceRank12_ desc) tot12mnthRank ,rank() over (order by MetresadvanceRank12_ desc)MetresadvanceRank12,* from(
select org12,Metresadvance12,BookMetresadvance12, rank() over (order by Metresadvance12,BookMetresadvance12 asc) as MetresadvanceRank12_,cast((nullif(BookMetresadvance12,0)/Metresadvance12  )*100 as int) Metresadvance12Percentage FROM(
SELECT DISTINCT org12,SUM(Metresadvance12)Metresadvance12,sum(BookMetresadvance12) BookMetresadvance12 FROM (
select  org12 ,Metresadvance12m,ISNULL(BookMetresadvance12,0) BookMetresadvance12,  aa Metresadvance12
from (select OrgUnitDay  org12, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance12M,TotalMetres BookMetresadvance12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org12)X)l  order by tot12mnthRank DESC
END

if @orderby='3 Month' 
begin
select rank() over (order by MetresadvanceRank3_ desc) tot3mnthRank,rank() over (order by MetresadvanceRank3_ desc)MetresadvanceRank3,* from
(select org3,Metresadvance3,BookMetresadvance3, rank() over (order by Metresadvance3,BookMetresadvance3 asc) as MetresadvanceRank3_,cast((nullif(BookMetresadvance3,0)/Metresadvance3  )*100 as int) Metresadvance3Percentage FROM(
SELECT DISTINCT org3,SUM(Metresadvance3)Metresadvance3,sum(BookMetresadvance3) BookMetresadvance3 FROM (
select  org3 ,Metresadvance3M,isnull(BookMetresadvance3,0) BookMetresadvance3,  aa Metresadvance3
from (select OrgUnitDay  org3, sum(p.Metresadvance) aa, sum(p.Metresadvance) Metresadvance3M,TotalMetres  BookMetresadvance3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org3)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select rank() over (order by MetresadvanceRank1_ desc) tot1mnthRank,rank() over (order by MetresadvanceRank1_ desc)MetresadvanceRank1,* from
(select org1,Metresadvance1,BookMetresadvance1, rank() over (order by Metresadvance1,BookMetresadvance1 asc) as MetresadvanceRank1_,cast((nullif(BookMetresadvance1,0)/Metresadvance1  )*100 as int) Metresadvance1Percentage FROM(
SELECT DISTINCT org1,SUM(Metresadvance1)Metresadvance1,sum(BookMetresadvance1) BookMetresadvance1 FROM (
select  org1 ,Metresadvance1M,isnull(BookMetresadvance1,0)BookMetresadvance1 ,  aa Metresadvance1
from (select OrgUnitDay  org1, sum(p.Metresadvance)  aa, sum(p.Metresadvance) Metresadvance1M,TotalMetres BookMetresadvance1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org1)X)n ORDER BY tot1mnthRank DESC
END
end

end

go

create PROCEDURE [dbo].[Report_Production_Analysis_Development_MAdv]
	-- Add the parameters for the stored procedure here
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

	set @Query = 
		'select * from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				SUM(p.ReefAdv) AdvReef,
				SUM(p.WasteAdv) AdvWaste,
				SUM(case when pp.ReefWaste = ''R'' then p.BookMetresadvance else 0 end) BookAdvReef,
				SUM(case when pp.ReefWaste != ''R'' then p.BookMetresadvance else 0 end) BookAdvWaste
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
			INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
			if (@TypeReport = 'M')
				set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
			ELSE
				set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
									and p.Activity = 1
									and CT.WorkingDay = ''Y''
			group by p.CalendarDate
		) a order by CalendarDate '

--insert into linda (aa) values (@Query)
	exec (@Query)

END
GO

create PROCEDURE [dbo].[Report_Production_Analysis_Development_MAdv_Monthly_Average]
	-- Add the parameters for the stored procedure here
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
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
	declare @ToJoin varchar(20)
	declare @ShiftsQuery varchar(max)

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
				 and p.Activity = 1
				 and CT.WorkingDay = ''Y'' '
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
		'select (sum(p.ReefAdv)+sum(p.WasteAdv)) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			(SUM(case when pp.ReefWaste = ''R'' then p.BookMetresadvance else 0 end))/ ' + convert(varchar, @Shifts) + ' as ''BookReef'',
			(SUM(case when pp.ReefWaste != ''R'' then p.BookMetresadvance else 0 end))/ ' + convert(varchar, @Shifts) + ' as ''BookWaste''
		from
		PLANNING p
		inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
		INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
		where '
		IF (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @ProdMonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
		set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
								and p.Activity = 1
								and p.WorkingDay = ''Y'' '

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0, 0

END


GO

create PROCEDURE [dbo].[Report_Production_Analysis_Development_NoOfBlasts]
-- Add the parameters for the stored procedure here
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

	set @Query = 
		'select * from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				COUNT(distinct(case when p.Metresadvance > 0 then p.WorkplaceID end)) PlanNoOfBlasts,
				COUNT(distinct(case when p.BookMetresadvance > 0 then p.WorkplaceID end)) BookNoOfBlasts
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
			if (@TypeReport = 'M')
				set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
			ELSE
				set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
									and p.Activity = 1
									and CT.WorkingDay = ''Y''
			group by p.CalendarDate
		) a 
		order by CalendarDate '

	--print (@Query)
	exec (@Query)

END



GO

create PROCEDURE [dbo].[Report_Production_Analysis_Development_NoOfBlasts_Monthly_Average]
	-- Add the parameters for the stored procedure here
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
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
	declare @ToJoin varchar(20)
	declare @ShiftsQuery varchar(max)

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
			inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth
			where p.Prodmonth = '''+ @ProdMonth +''' 
				  and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
				 and p.Activity = 1'
				-- and p.WorkingDay = ''Y'' '
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

	--set @Query = 
	--	'-- P3 Plan Blasts 
	--	select sum(Sqm) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
	--		SUM(BookSqm)/ ' + convert(varchar, @Shifts) + ' as ''Book''
	--		from
	--		PLANNING p
	--		inner join SectionComplete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
	--		where '
	--IF (@TypeReport = 'M')
	--	set @Query = @Query + ' p.Prodmonth = '''+ @ProdMonth +''' '
	--ELSE
	--	set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
	--set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
	--						and p.Activity IN(0,9)
	--						and p.WorkingDay = ''Y'' '

	set @Query = 
		'-- P3 Plan Blasts
		 select 
			(select sum(NoOfBlasts) 
			from
				(select  COUNT(distinct(p.WorkplaceID)) NoOfBlasts
				from
				PLANNING p
				inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
				where '
				IF (@TypeReport = 'M')
					set @Query = @Query + ' p.Prodmonth = '''+ @ProdMonth +''' '
				else
					set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
				set @Query = @Query + ' and sc.' + @ToJoin + ' = '''+ convert(varchar, @SectionID) +'''
									and p.Activity = 1
									and CT.WorkingDay = ''Y''
									and (ISNULL(p.Metresadvance, 0)) > 0
				group by p.CalendarDate) as a) / '+ convert(varchar, @Shifts) +' as ''PlanBlasts'', 
		-- P3 Book Blasts
			(select sum(NoOfBlasts) 
			from
				(select  COUNT(distinct(p.WorkplaceID)) NoOfBlasts
				from
				PLANNING p
				inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
				where '
				IF (@TypeReport = 'M')
					set @Query = @Query + ' p.Prodmonth = '''+ @ProdMonth +''' '
				else
					set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
				set @Query = @Query + ' and sc.' + @ToJoin + ' = '''+ convert(varchar, @SectionID) +'''
										and p.Activity = 1
										and CT.WorkingDay = ''Y''
										and (ISNULL(p.BookMetresadvance, 0)) > 0 
				group by p.CalendarDate) as a) / '+ convert(varchar, @Shifts) +' as ''BookBlasts'' '


	--print (@Query)
	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0


END


GO

create PROCEDURE [dbo].[Report_Production_Analysis_Development_Tons]
@Prodmonth varchar(6),
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

	set @Query = 
		'select * from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				SUM(p.Tons) TonsPlanned, SUM(p.BookTons) TonsBooked
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
			if (@TypeReport = 'M')
				set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
			ELSE
				set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
									and p.Activity = 1
									and CT.WorkingDay = ''Y''
			group by p.CalendarDate
		) a order by CalendarDate '

	--print (@Query)
	exec (@Query)

END
GO

create PROCEDURE [dbo].[Report_Production_Analysis_Development_Tons_Monthly_Average]
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
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
				 and CT.WorkingDay = ''Y'' '
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
		'select sum(Tons) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			SUM(BookTons)/ ' + convert(varchar, @Shifts) + ' as ''Book''
		from
		PLANNING p
		inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
		where '
		IF (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @ProdMonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
		set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
								and p.Activity = 1
								and CT.WorkingDay = ''Y'' '

	--print (@Query)

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0
	
END

GO

create PROCEDURE [dbo].[Report_Production_Analysis_Engineering_Breakdowns]
	-- Add the parameters for the stored procedure here
--declare
@Prodmonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID varchar(20),
@Section int,
@TypeReport varchar(1)

AS
BEGIN
	--set @Prodmonth = '201410'
	--set @FromDate = '2014-09-15'
	--set @ToDate = '2014-10-15'
	--set @SectionID = '4'
	--set @Section = 3
	--set @TypeReport = 'D'

	declare @ProdMonthDate date
	declare @ProdMonthDateP1 date
	declare @ProdMonthDateP2 date
	declare @ProdMonthDateP3 date
	declare @ProdMonthDateF1 date
	declare @ProdMonthDateF2 date
	declare @ProdMonthDateF3 date
	declare @ProdMonthP1 varchar(6)
	declare @ProdMonthP2 varchar(6)
	declare @ProdMonthP3 varchar(6)
	declare @ProdMonthF1 varchar(6)
	declare @ProdMonthF2 varchar(6)
	declare @ProdMonthF3 varchar(6)

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

	set @ProdMonthDate = SUBSTRING(@ProdMonth, 0, 5) + '-' + SUBSTRING(@ProdMonth, 5, 2) + '-01'
	set @ProdMonthDateP1 = DATEADD(Month, -1,@ProdMonthDate)
	set @ProdMonthP1 = convert(varchar, YEAR(@ProdMonthDateP1)) +  case when (LEN(convert(varchar, MONTH(@ProdMonthDateP1)))) = 1 then '0' + convert(varchar, MONTH(@ProdMonthDateP1)) else convert(varchar, MONTH(@ProdMonthDateP1)) end
	set @ProdMonthDateP2 = DATEADD(Month, -2,@ProdMonthDate)
	set @ProdMonthP2 = convert(varchar, YEAR(@ProdMonthDateP2)) +  case when (LEN(convert(varchar, MONTH(@ProdMonthDateP2)))) = 1 then '0' + convert(varchar, MONTH(@ProdMonthDateP2)) else convert(varchar, MONTH(@ProdMonthDateP2)) end
	set @ProdMonthDateP3 = DATEADD(Month, -3,@ProdMonthDate)
	set @ProdMonthP3 = convert(varchar, YEAR(@ProdMonthDateP3)) +  case when (LEN(convert(varchar, MONTH(@ProdMonthDateP3)))) = 1 then '0' + convert(varchar, MONTH(@ProdMonthDateP3)) else convert(varchar, MONTH(@ProdMonthDateP3)) end
	set @ProdMonthDateF1 = DATEADD(Month, 1,@ProdMonthDate)
	set @ProdMonthF1 = convert(varchar, YEAR(@ProdMonthDateF1)) +  case when (LEN(convert(varchar, MONTH(@ProdMonthDateF1)))) = 1 then '0' + convert(varchar, MONTH(@ProdMonthDateF1)) else convert(varchar, MONTH(@ProdMonthDateF1)) end
	set @ProdMonthDateF2 = DATEADD(Month, 2,@ProdMonthDate)
	set @ProdMonthF2 = convert(varchar, YEAR(@ProdMonthDateF2)) +  case when (LEN(convert(varchar, MONTH(@ProdMonthDateF2)))) = 1 then '0' + convert(varchar, MONTH(@ProdMonthDateF2)) else convert(varchar, MONTH(@ProdMonthDateF2)) end
	set @ProdMonthDateF3 = DATEADD(Month, 3,@ProdMonthDate)
	set @ProdMonthF3 = convert(varchar, YEAR(@ProdMonthDateF3)) +  case when (LEN(convert(varchar, MONTH(@ProdMonthDateF3)))) = 1 then '0' + convert(varchar, MONTH(@ProdMonthDateF3)) else convert(varchar, MONTH(@ProdMonthDateF3)) end

	set @Query =	
			'select CalendarDate, SUM(Value) Value
			from 
				(select  distinct(case when (LEN(convert(varchar, datepart(MM, s.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, s.CalendarDate)) else convert(varchar, datepart(MM, s.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, s.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, s.CalendarDate)) else convert(varchar, datepart(DD, s.CalendarDate)) end) CalendarDate,
						s.Value
				from
				SICCapture s
				inner join Section_Complete sc on s.SectionID = sc.SectionID_2 and sc.Prodmonth = s.MillMonth
				where '
				if (@TypeReport = 'M')
					set @Query = @Query + ' s.MillMonth = '''+ @Prodmonth +''' '
				ELSE
					set @Query = @Query + ' s.CalendarDate >= '''+ @FromDate +''' and s.CalendarDate <= '''+ @ToDate +''' '
				set @Query = @Query + ' and sc.'+ @ToJoin +' = '''+ @SectionID +'''
				) as a
			group by CalendarDate
			order by CalendarDate '

	--print (@Query)
	exec (@Query)

END




GO

create PROCEDURE [dbo].[Report_Production_Analysis_Engineering_Breakdowns_Monthly_Average]
	-- Add the parameters for the stored procedure here
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
@Section int,
@IsProgressive bit,
@TypeReport varchar(1),
@Shft int
AS
BEGIN
	--set @Prodmonth = '201410'
	--set @FromDate = '2014-09-16'
	--set @ToDate = '2014-10-16'
	--set @SectionID = '4'
	--set @Section = 3
	--set @IsProgressive = 0
	--set @TypeReport = 'D'
	--set @shft = 20

	declare @Query varchar(max)
	declare @ToJoin varchar(20)
	declare @ShiftsQuery varchar(max)

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
			inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth
			where p.Prodmonth = '''+ @ProdMonth +''' 
				  and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
				 and p.WorkingDay = ''Y'' '
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
		'select sum(value) / '+ convert(varchar, @Shifts) +' 
		from
			(select  distinct(case when (LEN(convert(varchar, datepart(MM, s.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, s.CalendarDate)) else convert(varchar, datepart(MM, s.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, s.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, s.CalendarDate)) else convert(varchar, datepart(DD, s.CalendarDate)) end) CalendarDate,
				s.Value
			from
			SICCapture s
			inner join Section_Complete sc on s.SectionID = sc.SectionID_2 and s.MillMonth = sc.Prodmonth
			where '
			IF (@TypeReport = 'M')
				set @Query = @Query + ' s.MillMonth = '''+ @ProdMonth +''' '
			ELSE
				set @Query = @Query + ' s.CalendarDate >= '''+ @FromDate +''' and s.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.' + @ToJoin + ' = '''+ @SectionID +''' ) a'
	--print (@Query)
	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0

END





GO

create PROCEDURE [dbo].[Report_Production_Analysis_HR_Compliment]
	-- Add the parameters for the stored procedure here
--declare
@Prodmonth varchar(6),
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


DECLARE @SICKeys VARCHAR(8000) 

SELECT @SICKeys = COALESCE(@SICKeys + ', ', '') + convert(varchar, SICKey) FROM code_siccapture
where kpi in ('Labour Stoping', 'Labour Development')
	  and (Description in ('Plan at Work', 'Actual at Work', 'Plan Unavailables', 'Actual Unavailables'))


set @Query = 
		'select 
			CalendarDate, 
			sum(case when (SICKey = 26 or SICKey = 28 or SICKey = 32 or SICKey = 34) then Value else 0 end) PlanValue,
			sum(case when SICKey in (27, 33) then Value else 0 end) BookValueAtWork,
			sum(case when SICKey in (35, 29) then Value else 0 end) BookValueUnavailables
		from 
			(select  distinct(case when (LEN(convert(varchar, datepart(MM, s.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, s.CalendarDate)) else convert(varchar, datepart(MM, s.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, s.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, s.CalendarDate)) else convert(varchar, datepart(DD, s.CalendarDate)) end) CalendarDate,
			 SICKey, Value
			from SICCapture s
			inner join Section_Complete sc on s.SectionID = sc.MOID and sc.Prodmonth = s.MillMonth
			where '
			IF (@TypeReport = 'M')
				set @Query = @Query + ' s.MillMonth = '''+ @Prodmonth +''' '
			ELSE
				set @Query = @Query + ' s.CalendarDate >= '''+ @FromDate +''' and s.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.'+ @ToJoin +' = ''' + @SectionID +'''
			and s.SICKey in (26, 27, 28, 29, 32, 33, 34, 35)
		) as a
		group by CalendarDate
		order by CalendarDate '

	--print (@Query)
	exec (@Query)

END

GO

create PROCEDURE [dbo].[Report_Production_Analysis_HR_Compliment_Monthly_Average]
	-- Add the parameters for the stored procedure here
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
@Section int,
@IsProgressive bit,
@TypeReport varchar(1),
@Shft int

AS
BEGIN
	--set @Prodmonth = '201410'
	--set @FromDate = '2014-09-16'
	--set @ToDate = '2014-10-16'
	--set @SectionID = '4'
	--set @Section = 3
	--set @IsProgressive = 0
	--set @TypeReport = 'D'
	--set @shft = 20

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

	declare @ShiftsQuery varchar(max)

	IF (@TypeReport = 'M')
	BEGIN
		create table #tempShiftCount (ShiftCount int)
		set @ShiftsQuery = '
			select count(distinct(p.CalendarDate)) 
			from PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth
			where p.Prodmonth = '''+ @ProdMonth +''' 
				  and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + ''''
				-- and p.WorkingDay = ''Y'' '
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
		' select sum(PlanValue) / ' + convert(varchar, @Shifts) + ' as ''PlanValue'',
			sum(BookValueAtWork)/ ' + convert(varchar, @Shifts) + ' as ''BookValueAtWork'',
			sum(BookValueUnavailables)/' + CONVERT(varchar, @Shifts) + 'as ''BookValueUnavailables''
		from
			(select  distinct(case when (LEN(convert(varchar, datepart(MM, s.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, s.CalendarDate)) else convert(varchar, datepart(MM, s.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, s.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, s.CalendarDate)) else convert(varchar, datepart(DD, s.CalendarDate)) end) CalendarDate,
				case when s.SICKey in (26, 28, 32, 34) then s.Value else 0 end PlanValue,
				case when s.SICKey in (27, 33) then s.Value else 0 end BookValueAtWork,
				case when s.SICKey in (35, 29) then s.Value else 0 end BookValueUnavailables
			from
			SICCapture s
			inner join Section_Complete sc on s.SectionID = sc.SectionID and s.MillMonth = sc.Prodmonth
			where '
			IF (@TypeReport = 'M')
				set @Query = @Query + ' s.MillMonth = '''+ @ProdMonth +''' '
			ELSE
				set @Query = @Query + ' s.CalendarDate >= '''+ @FromDate +''' and s.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.' + @ToJoin + ' = '''+ @SectionID +''' 
		) a'

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0, 0

END






GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_AdvancePerBlast]
	-- Add the parameters for the stored procedure here
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


	set @Query = 
		'select 
			CalendarDate,
			convert(decimal(16, 1), isnull(case when (sum(PLFL) <> 0) then sum(PLSQM) / sum(PLFL) else 0 end, 1)) PlanAdvancePerBlast,
			convert(decimal(16, 1), isnull(case when (sum(BkFL) <> 0) then sum(BKSQM) / sum(BKFL) else 0 end, 1)) BookAdvancePerBlast
		from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				(case when (p.Sqm <> 0) then (pm.FL) else 0 end) PLFL, 
				(p.SQM) PLSQM, 
				 (case when (p.BookSqm <> 0) then (p.BookFL) else 0 end) BKFL, 
				(p.BookSQM) BKSQM from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			inner join PLANMONTH pm on
			  p.Prodmonth = pm.Prodmonth and
			  p.Activity = pm.Activity and
			  p.WorkplaceID = pm.Workplaceid and
			  p.SectionID = pm.Sectionid
			where '
		if (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.'+ @ToJoin +' = '''+ @SectionID +'''
			and p.Activity IN (0,9)
			and CT.WorkingDay = ''Y'' 
			) a
		group by CalendarDate
		order by CalendarDate '

	--print (@Query)
	exec (@Query)

END




GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average]
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
				 and CT.WorkingDay = ''Y'' '
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
		' select 
				convert(decimal(16, 1), isnull(case when (sum(PLFL) <> 0) then sum(PLSQM) / sum(PLFL) else 0 end, 1)) as ''Plan'',
				convert(decimal(16, 1), isnull(case when (sum(BkFL) <> 0) then sum(BKSQM) / sum(BKFL) else 0 end, 1)) as ''Book''
			from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				(case when (p.Sqm <> 0) then (pm.FL) else 0 end) PLFL, 
				(p.SQM) PLSQM, 
				(case when (p.BookSqm <> 0) then (p.BookFL) else 0 end) BKFL, 
				(p.BookSQM) BKSQM 
				from
				PLANNING p
				inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
				inner join PLANMONTH pm on
				  p.Prodmonth = pm.Prodmonth and
				  p.Activity = pm.Activity and
				  p.WorkplaceID = pm.Workplaceid and
				  p.SectionID = pm.Sectionid INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
				where '
			IF (@TypeReport = 'M')
				set @Query = @Query + ' p.ProdMonth = '''+ @ProdMonth +''' '
			ELSE
				set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.'+ @ToJoin +' = '''+ @SectionID +'''
									and p.Activity IN(0,9)
									and CT.WorkingDay = ''Y'' 
			) a'

	--print(@Query)
	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0
	
END
GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_GT]
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
			convert(decimal(18, 0), case when SUM(p.reefsqm) <> 0 then sum(pm.GT * p.reefsqm) / SUM(p.reefsqm) else 0 end) ''Plan'', 
			convert(decimal(18, 0), case when SUM(p.bookreefsqm) <> 0 then sum(p.Bookcmgt * p.bookreefsqm) / SUM(p.bookreefsqm) else 0 end) ''Book''
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
			  and CT.WorkingDay = ''Y''
			group by p.CalendarDate) as a
			order by CalendarDate '

	--print (@Query)
	exec (@Query)

END
GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_GT_Monthly_Average]
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
				 and CT.WorkingDay = ''Y'' '
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
				convert(decimal(18, 0), case when SUM(p.reefsqm) <> 0 then sum(pm.GT * p.reefsqm) / SUM(p.reefsqm) else 0 end) as ''PlanValue'', 
				convert(decimal(18, 0), case when SUM(p.bookreefsqm) <> 0 then sum(p.Bookcmgt * p.bookreefsqm) / SUM(p.bookreefsqm) else 0 end) as ''BookValue''
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
									and CT.WorkingDay = ''Y''
			group by p.CalendarDate
		) as a '

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0

END
GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_KG]
	-- Add the parameters for the stored procedure here
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


	set @Query = 
		'select * from
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate, 
				convert(decimal(18, 3), SUM(ISNULL(p.grams, 0) / 1000)) as ''Plan'', 
				convert(decimal(18, 3), SUM(ISNULL(p.BookGrams, 0) / 1000)) as ''Book''
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
		if (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
		set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
								and p.Activity IN (0,9)
								and CT.WorkingDay = ''Y''
			group by p.CalendarDate) as a
		order by CalendarDate '

	--print (@Query)
	exec (@Query)

END




GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_KG_Monthly_Average]
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
				 and CT.WorkingDay = ''Y'' '
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
		' select sum(p.grams / 1000) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			SUM(p.BookGrams / 1000) / ' + convert(varchar, @Shifts) + ' as ''Book''
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
	IF (@TypeReport = 'M')
		set @Query = @Query + ' p.ProdMonth = '''+ @ProdMonth +''' '
	ELSE
		set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
	set @Query = @Query + ' and sc.' + @ToJoin + ' = '''+ @SectionID +'''
							 and p.Activity IN(0,9)
							 and CT.WorkingDay = ''Y'' '

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0

	--print(@Query)

END

GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_SQM]
	-- Add the parameters for the stored procedure here
--declare
@Prodmonth varchar(6),
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

	set @Query = 
		'select * from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				SUM(p.Sqm) SQMPlanned, SUM(p.BookSqm) SQMBooked
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
			if (@TypeReport = 'M')
				set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
			ELSE
				set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
									and p.Activity IN (0,9)
									and CT.WorkingDay = ''Y''
			group by p.CalendarDate
		) a order by CalendarDate '

	exec (@Query)

END
GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_SQM_Monthly_Average]
	-- Add the parameters for the stored procedure here
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
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
				 and CT.WorkingDay = ''Y'' '
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
		' select sum(Sqm) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			SUM(BookSqm)/ ' + convert(varchar, @Shifts) + ' as ''Book''
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
	IF (@TypeReport = 'M')
		set @Query = @Query + ' p.Prodmonth = '''+ @ProdMonth +''' '
	ELSE
		set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
	set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
							and p.Activity IN(0,9)
							and CT.WorkingDay = ''Y'' '

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0
	--print (@Query)
END
GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_Tons]
--declare
@Prodmonth varchar(6),
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

	set @Query = 
		' select * from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate, 
				SUM(p.Tons) TonsPlanned, SUM(p.BookTons) TonsBooked
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
		if (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + 'and sc.' + @ToJoin + ' = '''+  @SectionID +'''
									and p.Activity IN (0,9)
									and CT.WorkingDay = ''Y''
			group by p.CalendarDate) as a
		order by CalendarDate '

	--print (@Query)
	exec (@Query)

END
GO

create PROCEDURE [dbo].[Report_Production_Analysis_Stoping_Tons_Monthly_Average]
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
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
				 and CT.WorkingDay = ''Y'' '
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
		'select sum(Tons) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			SUM(BookTons)/ ' + convert(varchar, @Shifts) + ' as ''Book''
		from
		PLANNING p
		inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
		where '
	IF (@TypeReport = 'M')
	BEGIN
		set @Query = @Query + '	p.ProdMonth = '''+ @ProdMonth +''' '
	END
	ELSE
	BEGIN
		set @Query = @Query + '	p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
	END
	set @Query = @Query + '	and sc.' + @ToJoin + ' = '''+ @SectionID +'''
							and p.Activity IN(0,9)
							and CT.WorkingDay = ''Y'' '

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0

	--print (@Query)
END







GO

CREATE TABLE [dbo].[DailyBlast_Crew](
	[UserID] [varchar](30) NULL,
	[BU] [varchar](10) NULL,
	[Shaft] [varchar](10) NULL,
	[OrderID] [int] NULL,
	[Label] [varchar](30) NULL,
	[Crews] [int] NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[DailyBlast_Crew_Labels](
	[OrderID] [int] NULL,
	[Label] [varchar](30) NULL,
	[BU] [varchar](30) NULL,
	[Shaft] [varchar](30) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


CREATE TABLE [dbo].[DailyBlast_Gold](
	[UserID] [varchar](30) NULL,
	[Region] [varchar](10) NULL,
	[BU] [varchar](10) NULL,
	[Shaft] [varchar](10) NULL,
	[TheDate] [datetime] NULL,
	[BookGrams] [numeric](15, 3) NULL,
	[PlanGrams] [numeric](15, 3) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[DailyBlast_Section_Prodmonth](
	[UserID] [varchar](30) NULL,
	[Prodmonth] [numeric](7, 0) NULL,
	[SectionID] [varchar](30) NULL,
	[OrgUnitDay] [varchar](30) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[DashBoard_Crew_Data_Temp](
	[UserID] [varchar](30) NULL,
	[BU] [varchar](30) NULL,
	[Shaft] [varchar](30) NULL,
	[OrgunitDay] [varchar](30) NULL,
	[CALENDARDATE] [datetime] NULL,
	[Squaremetres] [int] NULL
) ON [PRIMARY]

GO



create Procedure [dbo].[Sp_DailyBlast_Crew_List] --'2017-03-12'
--Declare 
  @TheDate Datetime
as

Declare @LocalDate DateTime

--select @LocalDate = @TheDate

select @LocalDate = max(calendardate) from  SECTION_COMPLETE b 
inner join SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
and d.CALENDARDATE <= @TheDate
and WORKINGDAY = 'Y'
where b.prodmonth >= (select currentproductionmonth from DBO.sysset)



Delete from DBO.DailyBlast_Section_Prodmonth where UserID = Host_Name()

Insert into DBO.DailyBlast_Section_Prodmonth
select distinct Host_Name(), a.Prodmonth, a.SectioniD, A.OrgUnitDay from DBO.PLANMONTH a 
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID 
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
and d.CALENDARDATE = @LocalDate

Delete from DashBoard_Crew_Data_Temp where UserID = Host_Name()  

insert into DashBoard_Crew_Data_Temp
select [Host_Name],
BU, 
Shaft,
OrgunitDay, CALENDARDATE, Sum(BookSQM) Squaremetres from
(select Host_Name() [Host_Name],
'D-MU' +Name_4 BU, 
'D' +Substring(NAME_3,1,1) Shaft,
a.OrgunitDay, d.CALENDARDATE CALENDARDATE, e.BookSQM 
from
DBO.PLANMONTH a 
inner join DBO.DailyBlast_Section_Prodmonth SM on
a.PRODMONTH = SM.PRODMONTH and
a.SECTIONID = SM.SECTIONID and
a.OrgunitDay = SM.OrgunitDay and 
sm.UserID = Host_Name()
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID 
and A.Plancode = 'MP'
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join DBO.PLANNING e on
a.PRODMONTH = e.PRODMONTH and
a.SECTIONID = e.SECTIONID and
a.WORKPLACEID = e.WORKPLACEID and
a.ACTIVITY = e.ACTIVITY and
a.IsCubics = e.IsCubics and
a.PLanCode = e.PlanCode and
d.CALENDARDATE = e.CALENDARDATE
where d.CALENDARDATE <= @LocalDate
and a.OrgunitDay is not null
and a.Activity in (0,3)
and a.IsCubics = 'N'
) a
group by [Host_Name],
BU, 
Shaft,
OrgunitDay, CALENDARDATE 

Select Host_Name() UserID, a.BU, a.Shaft, OrderID, a.Label, OrgunitDay+', ' OrgunitDay from 
DailyBlast_Crew_Labels a left join
(select BU, Shaft, OrgunitDay, CALENDARDATE, 1 Total,
Label = Case when  @LocalDate - CALENDARDATE = 0 then 'Blasted Today'
when @LocalDate - CALENDARDATE = 1 then 'No blast 1 day'
when @LocalDate - CALENDARDATE = 2 then 'No blast 2 days'
when @LocalDate - CALENDARDATE = 3 then 'No blast 3 days'
when @LocalDate - CALENDARDATE = 4 then 'No blast 4 days'
when @LocalDate - CALENDARDATE >= 5 then 'No blast 5+ days'
end
from
(select BU, 
Shaft,
OrgunitDay, MAX(CALENDARDATE) CALENDARDATE
 from
(Select * from DashBoard_Crew_Data_Temp where UserID = Host_Name()) a
Where Squaremetres > 0
and OrgunitDay is not null
group by BU, Shaft, OrgunitDay
) a 

) b on
a.BU = b.BU and
a.Shaft = b.Shaft and
a.Label = b.label
where a.Label in ('No blast 3 days',
'No blast 4 days',
'No blast 5+ days')
--group by a.BU, a.Shaft, OrderID, a.Label 
order by a.BU, a.Shaft, OrderID
GO

create Procedure [dbo].[Sp_DailyBlast_Crews] --'2017-03-12'
--Declare 
  @TheDate Datetime
as

Declare @LocalDate DateTime

-- select @TheDate = '2015-10-01'

Set @LocalDate = @TheDate


select @LocalDate = max(calendardate) from DBO.SECTION_COMPLETE b 
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
and d.CALENDARDATE <= @TheDate
and WORKINGDAY = 'Y'
where b.prodmonth >= (select currentproductionmonth from sysset)


Delete from DailyBlast_Section_Prodmonth where UserID = Host_Name()

Insert into DailyBlast_Section_Prodmonth
select distinct Host_Name(), a.Prodmonth, a.SectioniD, A.OrgUnitDay from DBO.PLANMONTH a 
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID 
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
and Plancode = 'MP'
and d.CALENDARDATE = @LocalDate

Delete from DashBoard_Crew_Data_Temp where UserID = Host_Name()  

insert into DashBoard_Crew_Data_Temp
select [Host_Name],
BU, 
Shaft,
OrgunitDay, CALENDARDATE, Sum(BookSQM) Squaremetres from
(select Host_Name() [Host_Name],
'D-MU' +Name_4 BU, 
'D' +Substring(NAME_3,1,1) Shaft,
a.OrgunitDay, d.CALENDARDATE CALENDARDATE, e.BookSQM 
from
DBO.PLANMONTH a 
inner join DBO.DailyBlast_Section_Prodmonth SM on
a.PRODMONTH = SM.PRODMONTH and
a.SECTIONID = SM.SECTIONID and
a.OrgunitDay = SM.OrgunitDay  and 
sm.UserID = Host_Name()
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID and
a.PlanCode = 'MP'
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CalenDarCode = d.CalenDarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join DBO.Planning e on
a.PRODMONTH = e.PRODMONTH and
a.SECTIONID = e.SECTIONID and
a.WORKPLACEID = e.WORKPLACEID and
a.ACTIVITY = e.ACTIVITY and
a.IsCubics = e.IsCubics and
a.PlanCode = e.Plancode and
d.CALENDARDATE = e.CALENDARDATE
where d.CALENDARDATE <= @LocalDate
and a.OrgunitDay is not null
and a.Activity in (0,3)
and a.IsCubics = 'N'
) a
group by [Host_Name],
BU, 
Shaft,
OrgunitDay, CALENDARDATE 

Delete from DailyBlast_Crew where 
UserID = Host_Name() 

Insert into DailyBlast_Crew
Select Host_Name() UserID, a.BU, a.Shaft, OrderID, a.Label, isnull(Sum(Total),0) Total from 
DailyBlast_Crew_Labels a left join
(select BU, Shaft, OrgunitDay, CALENDARDATE, 1 Total,
Label = Case when  @LocalDate - CALENDARDATE = 0 then 'Blasted Today'
when @LocalDate - CALENDARDATE = 1 then 'No blast 1 day'
when @LocalDate - CALENDARDATE = 2 then 'No blast 2 days'
when @LocalDate - CALENDARDATE = 3 then 'No blast 3 days'
when @LocalDate - CALENDARDATE = 4 then 'No blast 4 days'
when @LocalDate - CALENDARDATE >= 5 then 'No blast 5+ days'
end
from
(select BU, 
Shaft,
OrgunitDay, MAX(CALENDARDATE) CALENDARDATE
 from
(Select * from DashBoard_Crew_Data_Temp where UserID = Host_Name()) a
Where Squaremetres > 0
and OrgunitDay is not null
group by BU, Shaft, OrgunitDay
) a 

) b on
a.BU = b.BU and
a.Shaft = b.Shaft and
a.Label = b.label
group by a.BU, a.Shaft, OrderID, a.Label 
order by a.BU, a.Shaft, OrderID

insert into DailyBlast_Crew
select Host_Name(), BU, Shaft, 1, 'Total', sum(Crews) Total from DailyBlast_Crew
where UserID = Host_Name()
and OrderID <> 1
group by BU, Shaft

select * from DailyBlast_Crew
where UserID = Host_Name()
order by BU,Shaft, OrderID
GO

create Procedure [dbo].[SP_DailyBlast_KG]  --'2016-03-16'
@TheDate DateTime
as
 select 
   Region,
   TheDate,
   RIGHT('0' +cast(datepart(DD,TheDate) as Varchar), 2) + '-' + substring(DATENAME(m, TheDate),1,3) FormatedDate,
   case when TheDate <= @TheDate then Sum(BookGrams/1000) else Null end Book_KG,
   --Sum(BookGrams/1000) Book_KG,
   Sum(PlanGrams/1000) Plan_KG
    from DailyBlast_Gold where UserID = HOST_NAME()
   Group by Region, TheDate
   order by Region, TheDate
GO

create Procedure [dbo].[SP_DailyBlast_KG_Shaft] --'2016-03-16'
@TheDate DateTime
as
 select 
   --Region,
   --BU,
   Shaft,
   TheDate,
   RIGHT('0' +cast(datepart(DD,TheDate) as Varchar), 2) + '-' + substring(DATENAME(m, TheDate),1,3) FormatedDate,
   case when TheDate <= @TheDate then Sum(BookGrams/1000) else Null end Book_KG,
   --Sum(BookGrams/1000) Book_KG,
   Sum(PlanGrams/1000) Plan_KG
    from DailyBlast_Gold where UserID = HOST_NAME()
	--and Shaft = 'D1'
	group by Shaft, TheDate
   order by Shaft, TheDate
GO

create Procedure [dbo].[SP_DailyBlast_Shafts]
as
Select 'D-MU1' BU ,'D1' Shaft Union
Select 'D-MU4' ,'D5' Union
Select 'D-MU2' ,'D2' Union
Select 'D-MU2' ,'D4' Union
Select 'D-MU3' ,'D6' Union
Select 'D-MU3' ,'D8' 
--Union
--Select 'K-MU3' ,'K3' Union
--Select 'K-MU4' ,'K4' Union
--Select 'K-MU1' ,'K1' Union
--Select 'K-MU1' ,'K2' Union
--Select 'K-MU2' ,'K7' Union
--Select 'K-MU2' ,'K8' 
GO

CREATE Procedure [dbo].[SP_DailyBlastDEV_BU] --'2016-03-16'
  @TheDate Datetime
as

declare @Prodmonth Numeric(7)
Declare @Local_Date Datetime

set @Local_Date = @TheDate

--set @local_date = '2012-05-29'

select @Prodmonth = Min(PRODMONTH) from 
SECCAL c
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= @Local_Date-28 and
c.ENDDATE >= @Local_Date-28

Select BU,
Shaft,
--MO,
CALENDARDATE,
SatDay = DATEPART(DW,CALENDARDATE),
Max(WORKINGDAY) WORKINGDAY,
Max(MeasureDate) MeasureDate,
Convert(Numeric(10,3),SUM(BookedM)) BookedM,
Convert(Numeric(10,3),SUM(PlanM)) PlanM,
PerBooked = case when
SUM(PlanM) = 0 then 0 else
Convert(Numeric(6,3),Round(SUM(BookedM)/SUM(PlanM),3)) end
from
(select
'D-MU' +Name_4 BU, 
'D' +Substring(NAME_3,1,1) Shaft,
--Peername_2 MO,
d.CALENDARDATE,
d.WORKINGDAY,
isnull(f.BookMetresadvance,0) BookedM,
isnull(f.METRESADVANCE,0) PlanM,
MeasureDate = Case when d.CALENDARDATE = c.BeginDate then 'Y' Else 'N' end
 from DBO.PLANMONTH a 
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID and
PlanCode = 'MP'
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join DBO.PLANNING f on
a.PRODMONTH = f.PRODMONTH and
a.SECTIONID = f.SECTIONID and
a.WORKPLACEID = f.WORKPLACEID and
a.ACTIVITY = f.ACTIVITY and
a.IsCubics = f.IsCubics and
d.CALENDARDATE = f.CALENDARDATE
where a.PRODMONTH >= @Prodmonth
and d.CALENDARDATE <= @Local_Date
and d.CALENDARDATE >= @Local_Date-28
and a.activity in (1)
and a.Iscubics = 'N'
and datepart(dw,d.CALENDARDATE) <> 1
union all
select
'Total' BU, 
Shaft = 'Total',
--Peername_2 MO,
d.CALENDARDATE,
d.WORKINGDAY,
isnull(f.BookMetresadvance,0) BookedM,
isnull(f.METRESADVANCE,0) PlanM,
MeasureDate = Case when d.CALENDARDATE = c.BeginDate then 'Y' Else 'N' end
 from DBO.PLANMONTH a 
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID and
PlanCode = 'MP'
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join DBO.PLANNING f on
a.PRODMONTH = f.PRODMONTH and
a.SECTIONID = f.SECTIONID and
a.WORKPLACEID = f.WORKPLACEID and
a.ACTIVITY = f.ACTIVITY and
a.IsCubics = f.IsCubics and
d.CALENDARDATE = f.CALENDARDATE
where a.PRODMONTH >= @Prodmonth
and d.CALENDARDATE <= @Local_Date
and d.CALENDARDATE >= @Local_Date-28
and a.activity in (1)
and a.Iscubics = 'N'
and datepart(dw,d.CALENDARDATE) <> 1
) a
group by BU,
Shaft,
--MO,
CALENDARDATE
order by BU,
Shaft,
--MO,
CALENDARDATE,
MeasureDate
GO

create Procedure [dbo].[SP_DailyBlastStope_BU] --'2016-03-06'
--Declare
 @TheDate Datetime
as

declare @Prodmonth Numeric(7)
Declare @Local_Date Datetime
set @Local_Date = @TheDate

--select @Local_Date =  '2014-03-06'

select @Prodmonth = Min(PRODMONTH) from 
SECCAL c
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= @Local_Date-28 and
c.ENDDATE >= @Local_Date-28

Select 
MAX(ORDERID) OrderID,
BU,
Shaft,
--MO,
CALENDARDATE,
SatDay = DATEPART(DW,CALENDARDATE),
Max(WORKINGDAY) WORKINGDAY,
Max(MeasureDate) MeasureDate,
SUM(BookedSQM) BookedSQM,
SUM(PlanSQM) PlanSQM,
Bookedcmgt = Case when SUM(BookedSQMDens) = 0 then 0 else convert(int,round(SUM(BookedGrams)*100/SUM(BookedSQMDens),0)) end,
Plancmgt = Case when SUM(PlanSQMDens) = 0 then 0 else convert(int,round(SUM(PlanGrams)*100/SUM(PlanSQMDens),0)) end,
PerBooked = case when
SUM(PlanSQM) = 0 then 0 else
Convert(Numeric(6,3),Round(SUM(BookedSQM)/SUM(PlanSQM),3)) end,
PerBookedcmgt = case when (SUM(PlanGrams) = 0) or (SUM(PlanSQMDens) = 0) or (SUM(BookedSQMDens) = 0) then 0 else 
convert(int,round(SUM(BookedGrams)*100/SUM(BookedSQMDens),0))/(convert(int,round(SUM(PlanGrams)*100/SUM(PlanSQMDens),0))+0.0001)
 end
from
(
select 1 OrderID,
'D-MU' +Name_4 BU, 
'D' +Substring(NAME_3,1,1) Shaft,
--Peername_2 MO,
d.CALENDARDATE,
d.WORKINGDAY,
isnull(f.BookSQM,0) BookedSQM,
isnull(f.SQM,0) PlanSQM,
isnull(f.BookreefSQM*wp.density,0) BookedSQMDens,
isnull(f.reefSQM*wp.density,0) PlanSQMDens,
isnull(f.BookGrams,0) BookedGrams,
isnull(f.Grams,0) PlanGrams,
MeasureDate = Case when d.CALENDARDATE = c.BeginDate then 'Y' Else 'N' end
 from DBO.PLANMONTH a 
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID 
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CALENDARCode = d.CALENDARCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join DBO.Planning f on
a.PRODMONTH = f.PRODMONTH and
a.SECTIONID = f.SECTIONID and
a.WORKPLACEID = f.WORKPLACEID and
a.ACTIVITY = f.ACTIVITY and
a.IsCubics = f.IsCubics and
d.CALENDARDATE = f.CALENDARDATE and
a.Plancode = f.PLanCode
inner join DBO.vw_Wp_density wp on
a.workplaceid = wp.workplaceid
where a.PRODMONTH >= @Prodmonth
and d.CALENDARDATE <= @Local_Date
and d.CALENDARDATE >= @Local_Date-28
and a.activity in (0,3)
and a.Iscubics = 'N'
and datepart(dw,d.CALENDARDATE) <> 1
union all
select 2 OrderID,
'Total' BU, 
Shaft = 'Total',
--Peername_2 MO,
d.CALENDARDATE,
d.WORKINGDAY,
isnull(f.BookSQM,0) BookedSQM,
isnull(f.SQM,0) PlanSQM,
isnull(f.BookreefSQM*wp.density,0) BookedSQMDens,
isnull(f.reefSQM*wp.density,0) PlanSQMDens,
isnull(f.BookGrams,0) BookedGrams,
isnull(f.Grams,0) PlanGrams,
MeasureDate = Case when d.CALENDARDATE = c.BeginDate then 'Y' Else 'N' end
 from DBO.PLANMONTH a 
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID 
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CALENDARCode = d.CALENDARCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join DBO.Planning f on
a.PRODMONTH = f.PRODMONTH and
a.SECTIONID = f.SECTIONID and
a.WORKPLACEID = f.WORKPLACEID and
a.ACTIVITY = f.ACTIVITY and
a.IsCubics = f.IsCubics and
d.CALENDARDATE = f.CALENDARDATE and
a.Plancode = f.PLanCode
inner join DBO.vw_Wp_density wp on
a.workplaceid = wp.workplaceid
where a.PRODMONTH >= @Prodmonth
and d.CALENDARDATE <= @Local_Date
and d.CALENDARDATE >= @Local_Date-28
and a.activity in (0,3)
and a.Iscubics = 'N'
and datepart(dw,d.CALENDARDATE) <> 1


) a
group by BU,
Shaft,
--MO,
CALENDARDATE
order by BU,
Shaft,
--MO,
CALENDARDATE,
MeasureDate
GO

create Procedure [dbo].[SP_DailyBlastStope_Dev_Riggs] --'2016-03-16'
@TheDate Datetime
as

declare @Prodmonth Numeric(7)
Declare @Local_Date Datetime

set @Local_Date = @TheDate

select @Prodmonth = Min(PRODMONTH) from 
SECCAL c
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= @Local_Date-28 and
c.ENDDATE >= @Local_Date-28

Select BU,
Shaft,
MO,
DrillRig,
Workplace,
CALENDARDATE,
SatDay = DATEPART(DW,CALENDARDATE),
Max(WORKINGDAY) WORKINGDAY,
SatDay = DATEPART(DW,CALENDARDATE),
Max(WORKINGDAY) WORKINGDAY,
Max(MeasureDate) MeasureDate,
Convert(Numeric(8,1),SUM(BookedM)) BookedM,
Convert(Numeric(8,1),SUM(PlanM)) PlanM,
PerBooked = case when
SUM(PlanM) = 0 then 0 else
Convert(Numeric(6,3),Round(SUM(BookedM)/SUM(PlanM),3)) end
from
(select
'D-MU' +Name_4 BU, 
'D' +Substring(NAME_3,1,1) Shaft,
Substring(name_2,2,5) MO,
DrillRig,
/*a.WORKPLACEID+':'+*/g.DESCRIPTION Workplace,
d.CALENDARDATE,
d.WORKINGDAY,
isnull(f.BookMetresadvance,0) BookedM,
isnull(f.METRESADVANCE,0) PlanM,
MeasureDate = Case when d.CALENDARDATE = c.BeginDate then 'Y' Else 'N' end
 from DBO.PLANMONTH a 
inner join DBO.SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID 
inner join DBO.SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join DBO.CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join DBO.PLANNING f on
a.PRODMONTH = f.PRODMONTH and
a.SECTIONID = f.SECTIONID and
a.WORKPLACEID = f.WORKPLACEID and
a.ACTIVITY = f.ACTIVITY and
a.IsCubics = f.IsCubics and
a.PlanCode = f.PlanCode and
d.CALENDARDATE = f.CALENDARDATE
Inner join DBO.WORKPLACE g on
a.WORKPLACEID = g.WORKPLACEID 
where a.PRODMONTH >= @Prodmonth
and d.CALENDARDATE <= @Local_Date
and d.CALENDARDATE >= @Local_Date-28
and a.activity in (1)
and a.Iscubics = 'N'
and datepart(dw,d.CALENDARDATE) <> 1
and AUTOUNPLAN is null
and Drillrig <> ''
) a
group by BU,
Shaft,
MO,
DrillRig,
Workplace,
CALENDARDATE
order by BU,
Shaft,
MO,
DrillRig,
Workplace,
CALENDARDATE,
MeasureDate
GO

create Procedure [dbo].[SP_DailyBlastStope_Gold_BuildData] --'2016-03-16'
--declare 
@thedate DateTime

as

--Set @Thedate = '2016-03-16'

Declare @StartDate DateTime,
@LocalDate DateTime,
@EndDate DateTime,
@Prodmonth Numeric(7),
@RunDate  DateTime,
@MillStartDate DateTime,
@MillEndDate DateTime,
@MillProdmonth Numeric(7),
@FinYear Numeric(7),
@TheMonth Numeric(7),
@Totalshifts Int,
@Shift Int

Set @LocalDate = @thedate

Select @MillStartDate = StartDate,
@MillEndDate = ENDDATE,
@MillProdmonth = MIllMonth
from CalendarMill where 
 StartDate <= @LocalDate 
and enddate >= @LocalDate

select @Totalshifts = Count(calendardate) from 
(select distinct b.calendardate, WorkingDay  from 
CalendarMill a inner join caltype b on
a.CalendarCode = b.CalendarCode and
a.StartDate <= b.calendardate and
a.enddate >= b.calendardate 
where MillMonth = @MillProdmonth 
and DatePart(DW,b.calendardate) <> 1) a



Select @StartDate = StartDate,
@EndDate = ENDDATE,
@Prodmonth = MillMonth
from CalenDarMill where 
 StartDate <= @LocalDate 
and enddate >= @LocalDate

Delete from DailyBlast_Gold where UserID = HOST_NAME() 

Set @RunDate = @StartDate

Set @Shift = 0

WHILE @Rundate <= @EndDate
   BEGIN
      If DatePart(DW,@RunDate) <> 1
	  BEGIN
	      Set @Shift = @Shift + 1
	      Insert into DailyBlast_Gold 
			Select 
			HOST_NAME(),
			'Total' Region,
			BU,
			Shaft,
			@RunDate Thedate,
			SUM(BookedGRAMS) BookedGrams,
			SUM(PlanGRAMS) PlanGrams
			from
			(select
			'D-MU' +Name_4 BU, 
			'D' +Substring(NAME_3,1,1) Shaft,
			name_2 MO,
			d.CALENDARDATE,
			isnull(e.BookGRAMS,0) BookedGRAMS,
			isnull(e.GRAMS,0) PlanGrams
				from DBO.PLANMONTH a 
			inner join DBO.SECTION_COMPLETE b on
			a.PRODMONTH = b.PRODMONTH and
			a.SECTIONID = b.SECTIONID and
			a.Plancode = 'MP'
			inner join DBO.SECCAL c on
			b.PRODMONTH = c.PRODMONTH and
			b.SECTIONID_1 = c.SECTIONID
			inner join DBO.CALTYPE d on
			c.CalendarCode = d.CalendarCode and
			c.BeginDate <= d.CALENDARDATE and
			c.ENDDATE >= d.CALENDARDATE
			left join DBO.Planning e on
			a.PRODMONTH = e.PRODMONTH and
			a.SECTIONID = e.SECTIONID and
			a.WORKPLACEID = e.WORKPLACEID and
			a.ACTIVITY = e.ACTIVITY and
			a.IsCubics = e.IsCubics and
			a.Plancode = e.Plancode and
			d.CALENDARDATE = e.CALENDARDATE
			Where a.PRODMONTH >= @MillProdmonth
			and d.CALENDARDATE <= @RunDate
			and d.CALENDARDATE >= @MillStartDate
			and a.activity in (0,3)
			and a.Iscubics = 'N'
			and datepart(dw,d.CALENDARDATE) <> 1
			) a
			group by BU,
			Shaft
			order by BU,
			Shaft	
		END
	  Set @Rundate = @Rundate + 1
   END;
  
  GO

create Procedure [dbo].[SP_DailyBlastStope_MO]  --'2016-03-06'
--Declare
  @TheDate Datetime
as

declare @Prodmonth Numeric(7)
Declare @Local_Date Datetime
set @Local_Date = @TheDate
--select @Local_Date =  '2014-03-11'

select @Prodmonth = Min(PRODMONTH) from 
SECCAL c
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= @Local_Date-28 and
c.ENDDATE >= @Local_Date-28

Select
TheOrder,
 BU,
Shaft,
MO,
CALENDARDATE,
SatDay = DATEPART(DW,CALENDARDATE),
Max(WORKINGDAY) WORKINGDAY,
SatDay = DATEPART(DW,CALENDARDATE),
Max(WORKINGDAY) WORKINGDAY,
Max(MeasureDate) MeasureDate,
SUM(BookedSQM) BookedSQM,
SUM(PlanSQM) PlanSQM,
Bookedcmgt = Case when SUM(BookedSQMDens) = 0 then 0 else convert(int,round(SUM(BookedGrams)*100/SUM(BookedSQMDens),0)) end,
Plancmgt = Case when SUM(PlanSQMDens) = 0 then 0 else convert(int,round(SUM(PlanGrams)*100/SUM(PlanSQMDens),0)) end,
PerBooked = case when
SUM(PlanSQM) = 0 then 0 else
Convert(Numeric(6,3),Round(SUM(BookedSQM)/SUM(PlanSQM),3)) end,
PerBookedcmgt = case when (SUM(PlanGrams) = 0) or (SUM(PlanSQMDens) = 0) or (SUM(BookedSQMDens) = 0) then 0 else 
convert(int,round(SUM(BookedGrams)*100/SUM(BookedSQMDens),0))/(convert(int,round(SUM(PlanGrams)*100/SUM(PlanSQMDens),0))+0.00001)
 end
from
(select
1 Theorder,
'D-MU' +Name_4 BU, 
'D' +Substring(NAME_3,1,1) Shaft,
name_2 MO,
d.CALENDARDATE,
d.WORKINGDAY,
isnull(f.BookSQM,0) BookedSQM,
isnull(f.SQM,0) PlanSQM,
isnull(F.BookreefSQM*wp.density,0) BookedSQMDens,
isnull(f.reefSQM*wp.density,0) PlanSQMDens,
isnull(f.BookGrams,0) BookedGrams,
isnull(f.Grams,0) PlanGrams,
MeasureDate = Case when d.CALENDARDATE = c.BeginDate then 'Y' Else 'N' end
 from PLANMONTH a 
inner join SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID 
inner join SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join CALTYPE d on
c.CALENDARCODE = d.CALENDARCODE and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join PLANNING f on
a.PRODMONTH = f.PRODMONTH and
a.SECTIONID = f.SECTIONID and
a.WORKPLACEID = f.WORKPLACEID and
a.Activity = f.Activity and
a.IsCubics = f.IsCubics and
d.CALENDARDATE = f.CALENDARDATE and
a.Plancode = f.PLanCode
inner join vw_Wp_density wp on
a.workplaceid = wp.workplaceid
where a.PRODMONTH >= @Prodmonth
and d.CALENDARDATE <= @Local_Date
and d.CALENDARDATE >= @Local_Date-28
and a.Activity in (0,3)
and a.Iscubics = 'N'
and datepart(dw,d.CALENDARDATE) <> 1
and AUTOUNPLAN is null
and a.Plancode = 'MP'
union all
select
2 Theorder,
'D-Total' BU, 
Shaft = '',
'' MO,
d.CALENDARDATE,
d.WORKINGDAY,
isnull(f.BookSQM,0) BookedSQM,
isnull(f.SQM,0) PlanSQM,
isnull(F.BookreefSQM*wp.density,0) BookedSQMDens,
isnull(f.reefSQM*wp.density,0) PlanSQMDens,
isnull(f.BookGrams,0) BookedGrams,
isnull(f.Grams,0) PlanGrams,
MeasureDate = Case when d.CALENDARDATE = c.BeginDate then 'Y' Else 'N' end
 from PLANMONTH a 
inner join SECTION_COMPLETE b on
a.PRODMONTH = b.PRODMONTH and
a.SECTIONID = b.SECTIONID 
inner join SECCAL c on
b.PRODMONTH = c.PRODMONTH and
b.SECTIONID_1 = c.SECTIONID
inner join CALTYPE d on
c.CALENDARCODE = d.CALENDARCODE and
c.BeginDate <= d.CALENDARDATE and
c.ENDDATE >= d.CALENDARDATE
left join PLANNING f on
a.PRODMONTH = f.PRODMONTH and
a.SECTIONID = f.SECTIONID and
a.WORKPLACEID = f.WORKPLACEID and
a.Activity = f.Activity and
a.IsCubics = f.IsCubics and
d.CALENDARDATE = f.CALENDARDATE and
a.Plancode = f.PLanCode
inner join vw_Wp_density wp on
a.workplaceid = wp.workplaceid
where a.PRODMONTH >= @Prodmonth
and d.CALENDARDATE <= @Local_Date
and d.CALENDARDATE >= @Local_Date-28
and a.Activity in (0,3)
and a.Iscubics = 'N'
and datepart(dw,d.CALENDARDATE) <> 1
and AUTOUNPLAN is null
and a.Plancode = 'MP'
) a
group by 
TheOrder,
BU,
Shaft,
MO,
CALENDARDATE
order by BU,
Shaft,
MO,
CALENDARDATE,
MeasureDate

GO


alter PROCEDURE [dbo].[Report_Production_Analysis_Stoping_GT]
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
			convert(decimal(18, 0), case when SUM(p.bookreefsqm) <> 0 then sum(p.Bookcmgt * p.bookreefsqm) / SUM(p.bookreefsqm) else 0 end) ''Book''
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
			  and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill''
			group by p.CalendarDate) as a
			order by CalendarDate '

	--print (@Query)
	exec (@Query)

END
go
alter PROCEDURE [dbo].[Report_Production_Analysis_Stoping_AdvancePerBlast_Monthly_Average]
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
				 and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill'''
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
		' select 
				convert(decimal(16, 1), isnull(case when (sum(PLFL) <> 0) then sum(PLSQM) / sum(PLFL) else 0 end, 1)) as ''Plan'',
				convert(decimal(16, 1), isnull(case when (sum(BkFL) <> 0) then sum(BKSQM) / sum(BKFL) else 0 end, 1)) as ''Book''
			from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				(case when (p.Sqm <> 0) then (pm.FL) else 0 end) PLFL, 
				(p.SQM) PLSQM, 
				(case when (p.BookSqm <> 0) then (p.BookFL) else 0 end) BKFL, 
				(p.BookSQM) BKSQM 
				from
				PLANNING p
				inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
				inner join PLANMONTH pm on
				  p.Prodmonth = pm.Prodmonth and
				  p.Activity = pm.Activity and
				  p.WorkplaceID = pm.Workplaceid and
				  p.SectionID = pm.Sectionid INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
				where '
			IF (@TypeReport = 'M')
				set @Query = @Query + ' p.ProdMonth = '''+ @ProdMonth +''' '
			ELSE
				set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.'+ @ToJoin +' = '''+ @SectionID +'''
									and p.Activity IN(0,9)
									and CT.WorkingDay = ''Y'' 
			) a'

	--print(@Query)
	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0
	
END

go
alter PROCEDURE [dbo].[Report_Production_Analysis_Stoping_AdvancePerBlast]
	-- Add the parameters for the stored procedure here
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


	set @Query = 
		'select 
			CalendarDate,
			convert(decimal(16, 1), isnull(case when (sum(PLFL) <> 0) then sum(PLSQM) / sum(PLFL) else 0 end, 1)) PlanAdvancePerBlast,
			convert(decimal(16, 1), isnull(case when (sum(BkFL) <> 0) then sum(BKSQM) / sum(BKFL) else 0 end, 1)) BookAdvancePerBlast
		from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				(case when (p.Sqm <> 0) then (pm.FL) else 0 end) PLFL, 
				(p.SQM) PLSQM, 
				 (case when (p.BookSqm <> 0) then (p.BookFL) else 0 end) BKFL, 
				(p.BookSQM) BKSQM from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			inner join PLANMONTH pm on
			  p.Prodmonth = pm.Prodmonth and
			  p.Activity = pm.Activity and
			  p.WorkplaceID = pm.Workplaceid and
			  p.SectionID = pm.Sectionid
			where '
		if (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.'+ @ToJoin +' = '''+ @SectionID +'''
			and p.Activity IN (0,9)
			and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill''
			) a
		group by CalendarDate
		order by CalendarDate '

	--print (@Query)
	exec (@Query)

END

go
alter PROCEDURE [dbo].[Report_Production_Analysis_Development_MAdv_Monthly_Average]
	-- Add the parameters for the stored procedure here
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
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
	declare @ToJoin varchar(20)
	declare @ShiftsQuery varchar(max)

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
				 and p.Activity = 1
				 and CT.WorkingDay = ''Y'' '
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
		'select (sum(p.ReefAdv)+sum(p.WasteAdv)) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			(SUM(case when pp.ReefWaste = ''R'' then p.BookMetresadvance else 0 end))/ ' + convert(varchar, @Shifts) + ' as ''BookReef'',
			(SUM(case when pp.ReefWaste != ''R'' then p.BookMetresadvance else 0 end))/ ' + convert(varchar, @Shifts) + ' as ''BookWaste''
		from
		PLANNING p
		inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth
		INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
		where '
		IF (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @ProdMonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
		set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
								and p.Activity = 1
								and CT.WorkingDay = ''Y'' '

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0, 0

END

go

ALTER PROCEDURE [dbo].[Report_CrewRanking_New_Dev]
--declare 
@Month1 VARCHAR(100), @Month3 VARCHAR(100),@Month12 VARCHAR(100),@sectionid varchar(80),@ratingBy varchar(5),@orderby varchar(10),@Status varchar(10),@totalmine varchar(10),@persect varchar(10),
@type varchar(100)
AS
--set @Month1=201702
--set @Month3=201611
--set @Month12=201603
--set @sectionid='Total Mine'
--set @ratingBy='AP'
--set @orderby='3 Month'
--set @Status='T'
--set @totalmine='Total Mine'
--set @persect ='F'
--set @type='OrgUnitDay'

SET ARITHABORT OFF 
SET ANSI_WARNINGS OFF
--exec Report_CrewRanking_New '201607', '201605', '201508','AAD','B','1 Month','T' ,'AAD:E Tshemese' ,'T'  
--exec Report_CrewRanking_New '201607', '201605', '201508','Total Mine','AB','1 Month','T' ,'Total Mine' ,'F' 

declare @type1 varchar(100)
declare @type2 varchar(100)
DECLARE @groupby varchar(100)
DECLARE @groupby1 varchar(100)
if @type='OrgUnitDay'
begin
set @type1='CrewMorning'
set @groupby='OrgUnitDay'
set @groupby1='CrewMorning'
end

if @type='sc.SectionID_2+'':''+Name_2'
begin
set @groupby='sc.SectionID_2,Name_2'
set @type1 ='sc.SectionID_2+'':''+Name_2'
set @type2 ='Name_2'
set @groupby1 =@groupby
end

if @type='sc.SectionID_1+'':''+Name_1'
begin
set @groupby='sc.SectionID_1,Name_1'
set @type1 ='sc.SectionID_1+'':''+Name_1'
set @type2 ='Name_1'
set @groupby1 =@groupby
end


declare @TheQuery varchar(max) 

--if @totalmine='Total Mine' and @persect ='T'
--BEGIN
--if @ratingBy='B' AND @Status='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc) ADVRank12,* from(
--select org12,sectionid12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
--select DISTINCT org12 ,sectionid12,ADV12M,ISNULL(BookADV12,0) BookADV12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,SqmTotal BookADV12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank , sectionid12 ,org12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3_,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3)*100 as int) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,isnull(BookADV3,0) BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,SqmTotal  BookADV3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank ,sectionid3, org3 
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by ADVRank1_ desc) tot1mnthRank,rank() over (order by ADVRank1_ desc)ADVRank1,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1)*100 as int) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,isnull(BookADV1,0)BookADV1 ,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,SqmTotal  BookADV1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank,sectionid1 , org1 
--END
--END

--if @ratingBy='B' AND @Status='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc)ADVRank12,* from(
--select org12,sectionid12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
--select DISTINCT org12 ,sectionid12,ADV12M,isnull(BookADV12,0)BookADV12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,SqmTotal BookADV12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank DESC, sectionid12 desc, org12 desc

--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3)*100 as int) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,isnull(BookADV3,0)BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,SqmTotal  BookADV3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank DESC, sectionid3 desc, org3 desc
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by ADVRank1_ desc) tot1mnthRank,rank() over (order by ADVRank1_ desc)ADVRank1,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1)*100 as int) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,isnull(BookADV1,0)BookADV1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,SqmTotal  BookADV1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank DESC, sectionid1 desc, org1 desc
--END
--end

--if @ratingBy='P' AND @Status ='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc)ADVRank12,* from(
--select org12,sectionid12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
--select DISTINCT org12 ,sectionid12,ADV12M,isnull(BookADV12,0)BookADV12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,SqmTotal BookADV12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by ADV12Percentage, sectionid12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3)*100 as int) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,isnull(BookADV3,0)BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,SqmTotal  BookADV3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by ADV3Percentage, sectionid3 
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by ADVRank1_ desc) tot1mnthRank, rank() over (order by ADVRank1_ desc)ADVRank1,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1)*100 as int) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,isnull(BookADV1,0)BookADV1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,SqmTotal  BookADV1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY ADV1Percentage, sectionid1 
--END
--end

--if @ratingBy='P' AND @Status ='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc)ADVRank12,* from(
--select org12,sectionid12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
--select DISTINCT org12 ,sectionid12,ADV12M,isnull(BookADV12,0)BookADV12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,SqmTotal BookADV12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by ADV12Percentage DESC, sectionid12 desc
--end


--if @orderby='3 Month' 
--begin
--select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3)*100 as int) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,isnull(BookADV3,0)BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,SqmTotal  BookADV3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by ADV3Percentage DESC, sectionid3 desc
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by ADVRank1_ desc) tot1mnthRank,rank() over (order by ADVRank1_ desc)ADVRank1,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1)*100 as int) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,isnull(BookADV1,0)BookADV1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,SqmTotal  BookADV1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY ADV1Percentage DESC, sectionid1 desc
--END
--end
--END

if @totalmine!='Total Mine'             ------------------Actual/booked---------------------
BEGIN
if @ratingBy='B' AND @Status='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(d1.BookADV12,-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/d.ADV12M)*100 as int) ADV12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV12M,sum(TotalMetres) BookADV12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank 
end



if @orderby='3 Month' 
begin
select * from
(select   ADVRank3_  tot3mnthRank, ADVRank3_ ADVRank3,* from
(select org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(d1.BookADV3,-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/d.ADV3M)*100 as int) ADV3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV3M,sum(TotalMetres) BookADV3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank 
END

if @orderby='1 Month' 
begin
select * from
(select   ADVRank1_  tot1mnthRank, ADVRank1_ ADVRank1,* from
(select org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(d1.BookADV1,-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV1M,sum(TotalMetres) BookADV1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank
END
END

if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(d1.BookADV12,-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/d.ADV12M)*100 as int) ADV12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV12M,sum(TotalMetres) BookADV12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank DESC
end


if @orderby='3 Month' 
begin
select * from
(select   ADVRank3_  tot3mnthRank, ADVRank3_ ADVRank3,* from
(select org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(d1.BookADV3,-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/d.ADV3M)*100 as int) ADV3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV3M,sum(TotalMetres) BookADV3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select * from
(select   ADVRank1_  tot1mnthRank, ADVRank1_ ADVRank1,* from
(select org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(d1.BookADV1,-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV1M,sum(TotalMetres) BookADV1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank DESC
END
end

if @ratingBy='P' AND @Status ='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by ADV12Percentage ASC) tot12mnthRank, rank() over (order by ADV12Percentage ASC)ADVRank12,* from
(select org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(d1.BookADV12,-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/d.ADV12M)*100 as int) ADV12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV12M,sum(TotalMetres) BookADV12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by ADV12Percentage
end



if @orderby='3 Month' 
begin
select * from
(select   rank() over (order by ADV3Percentage ASC) tot3mnthRank, rank() over (order by ADV3Percentage ASC)ADVRank3,* from
(select org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(d1.BookADV3,-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/d.ADV3M)*100 as int) ADV3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV3M,sum(TotalMetres) BookADV3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by ADV3Percentage
END



if @orderby='1 Month' 
begin
select * from
(select   rank() over (order by ADV1Percentage ASC) tot1mnthRank, rank() over (order by ADV1Percentage ASC)ADVRank1,* from
(select org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(d1.BookADV1,-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV1M,sum(TotalMetres) BookADV1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY ADV1Percentage
END
end

if @ratingBy='P' AND @Status ='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by ADV12Percentage DESC) tot12mnthRank, rank() over (order by ADV12Percentage DESC)ADVRank12,* from
(select org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(d1.BookADV12,-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/d.ADV12M)*100 as int) ADV12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV12M,sum(TotalMetres) BookADV12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by ADV12Percentage DESC
end




if @orderby='3 Month' 
begin
select * from
(select   rank() over (order by ADV3Percentage DESC) tot3mnthRank, rank() over (order by ADV3Percentage DESC)ADVRank3,* from
(select org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(d1.BookADV3,-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/d.ADV3M)*100 as int) ADV3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV3M,sum(TotalMetres) BookADV3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by ADV3Percentage DESC
END



if @orderby='1 Month' 
begin
select * from
(select   rank() over (order by ADV1Percentage DESC) tot1mnthRank, rank() over (order by ADV1Percentage DESC)ADVRank1,* from
(select org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(d1.BookADV1,-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV1M,sum(TotalMetres) BookADV1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY ADV1Percentage DESC
END
end
END

if @totalmine='Total Mine' AND @persect ='F'
BEGIN
if @ratingBy='P' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   rank() over (order by ADV12Percentage desc) tot12mnthRank, rank() over (order by ADV12Percentage desc)ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data


left outer join 
(select * from
(select   rank() over (order by ADV3Percentage DESC) tot3mnthRank, rank() over (order by ADV3Percentage DESC)ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3


left outer join 
(select distinct rank() over (order by ADV1Percentage desc) tot1mnthRank,rank() over (order by ADV1Percentage desc)ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct  '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12  ORDER BY ADV12Percentage desc'
--select @TheQuery
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select   rank() over (order by ADV3Percentage DESC) tot3mnthRank, rank() over (order by ADV3Percentage DESC)ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct rank() over (order by ADV1Percentage desc) tot1mnthRank,rank() over (order by ADV1Percentage desc)ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select   rank() over (order by ADV12Percentage desc) tot12mnthRank, rank() over (order by ADV12Percentage desc)ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY ADV3Percentage desc'

exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from 
(select distinct rank() over (order by ADV1Percentage desc) tot1mnthRank,rank() over (order by ADV1Percentage desc)ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select   rank() over (order by ADV3Percentage desc) tot3mnthRank, rank() over (order by ADV3Percentage desc)ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   rank() over (order by ADV12Percentage desc) tot12mnthRank, rank() over (order by ADV12Percentage desc)ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY ADV1Percentage desc'

--select @TheQuery
exec (@TheQuery)
END
end

if @ratingBy='B' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data
 

left outer join 
(select * from
(select  ADVRank3_  tot3mnthRank,ADVRank3_ ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3 
 

left outer join 
(select distinct ADVRank1_  tot1mnthRank,ADVRank1_ ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+'  ,P.Activity )d
full outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12   ORDER BY tot12mnthRank asc'
--select @TheQuery
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select  ADVRank3_  tot3mnthRank,ADVRank3_ ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct ADVRank1_  tot1mnthRank,ADVRank1_ ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth  between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY tot3mnthRank asc'

exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from (
select distinct ADVRank1_  tot1mnthRank,ADVRank1_ ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select  ADVRank3_  tot3mnthRank,ADVRank3_ ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY tot1mnthRank asc'

--select @TheQuery
exec (@TheQuery)
END
end


if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc)ADVRank12,* from(
select org12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
SELECT DISTINCT org12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
select  org12 ,ADV12M,ISNULL(BookADV12,0) BookADV12,  aa Metresadvance12
from (select OrgUnitDay  org12, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV12M,TotalMetres BookADV12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org12)X)l  order by tot12mnthRank DESC
END

if @orderby='3 Month' 
begin
select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3,* from
(select org3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3  )*100 as int) ADV3Percentage FROM(
SELECT DISTINCT org3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
select  org3 ,ADV3M,isnull(BookADV3,0) BookADV3,  aa Metresadvance3
from (select OrgUnitDay  org3, sum(BookMetresadvance) aa, sum(BookMetresadvance) ADV3M,TotalMetres  BookADV3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org3)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select rank() over (order by ADVRank1_ desc) tot1mnthRank,rank() over (order by ADVRank1_ desc)ADVRank1,* from
(select org1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1  )*100 as int) ADV1Percentage FROM(
SELECT DISTINCT org1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
select  org1 ,ADV1M,isnull(BookADV1,0)BookADV1 ,  aa Metresadvance1
from (select OrgUnitDay  org1, sum(p.BookMetresadvance)  aa, sum(p.BookMetresadvance) ADV1M,TotalMetres BookADV1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org1)X)n ORDER BY tot1mnthRank DESC
END
end

end
--LEFT OUTER JOIN
--(select ADVRank3 tot3mnthRank,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3,cast((nullif(BookADV3,0)/Metresadvance3  )*100 as numeric(3)) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,sectionid sectionid3, sum(Metresadvance) aa, sum(Metresadvance) ADV3M,sum(BookMetresadvance ) BookADV3 from PLANNING 
--where prodmonth between 201509 and 201511 and SectionID ='AAB'
--group by OrgUnitDay,sectionid)c)D GROUP BY org3, sectionid3)X)M)q on l.org12 =q.org3 

--LEFT OUTER JOIN
--(select ADVRank1 tot1mnthRank,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1,cast((nullif(BookADV1,0)/Metresadvance1  )*100 as numeric(3)) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,BookADV1,  aa Metresadvance1
--from (select OrgUnitDay  org1,sectionid sectionid1, sum(Metresadvance) aa, sum(Metresadvance) ADV1M,sum(BookMetresadvance ) BookADV1 from PLANNING 
--where prodmonth= 201511 and SectionID ='AAB'
--group by OrgUnitDay,sectionid)c)D GROUP BY org1, sectionid1)X)n)j on l.org12 =j.org1  


--exec Report_CrewRanking_New '201511', '201509', '201501','AAB','B','1 Month','F','Total Mine'   
--exec Report_CrewRanking_New '201506', '201504', '201407','Tot','B','1 Month','T' ,'Total Mine' ,'F' 

--exec Report_CrewRanking_New '201606', '201604', '201507','FMAAAA','B','3 Month','F','FMAAAA:BReef Drives'  ,'T'  

--SELECT

--if @totalmine='Total Mine' and @persect ='T'
--BEGIN
--if @ratingBy='AB' AND @Status='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc) ADVRank12,* from(
--select org12,sectionid12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
--select DISTINCT org12 ,sectionid12,ADV12M,ISNULL(BookADV12,0) BookADV12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) ADV12M,SqmTotal BookADV12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank , sectionid12 ,org12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3_,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3)*100 as int) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,isnull(BookADV3,0) BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) ADV3M,SqmTotal  BookADV3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank ,sectionid3, org3 
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by ADVRank1_ desc) tot1mnthRank,rank() over (order by ADVRank1_ desc)ADVRank1,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1)*100 as int) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,isnull(BookADV1,0)BookADV1 ,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) ADV1M,SqmTotal  BookADV1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank,sectionid1 , org1 
--END
--END

--if @ratingBy='AB' AND @Status='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc)ADVRank12,* from(
--select org12,sectionid12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
--select DISTINCT org12 ,sectionid12,ADV12M,isnull(BookADV12,0)BookADV12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) ADV12M,SqmTotal BookADV12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by tot12mnthRank DESC, sectionid12 desc, org12 desc
--end


--if @orderby='3 Month' 
--begin
--select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3)*100 as int) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,isnull(BookADV3,0)BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) ADV3M,SqmTotal  BookADV3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by tot3mnthRank DESC, sectionid3 desc, org3 desc
--END

--if @orderby='1 Month' 
--begin
--select rank() over (order by ADVRank1_ desc) tot1mnthRank,rank() over (order by ADVRank1_ desc)ADVRank1,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1)*100 as int) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,isnull(BookADV1,0)BookADV1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) ADV1M,SqmTotal  BookADV1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY tot1mnthRank DESC, sectionid1 desc, org1 desc
--END
--end

--if @ratingBy='AP' AND @Status ='T'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc)ADVRank12,* from(
--select org12,sectionid12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
--select DISTINCT org12 ,sectionid12,ADV12M,isnull(BookADV12,0)BookADV12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) ADV12M,SqmTotal BookADV12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by ADV12Percentage, sectionid12 
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3)*100 as int) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,isnull(BookADV3,0)BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) ADV3M,SqmTotal  BookADV3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by ADV3Percentage, sectionid3 
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by ADVRank1_ desc) tot1mnthRank, rank() over (order by ADVRank1_ desc)ADVRank1,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1)*100 as int) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,isnull(BookADV1,0)BookADV1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) ADV1M,SqmTotal  BookADV1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY ADV1Percentage, sectionid1 
--END
--end

--if @ratingBy='AP' AND @Status ='F'
--begin
--if @orderby='12 Month'  
--begin
--select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc)ADVRank12,* from(
--select org12,sectionid12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
--SELECT DISTINCT org12,sectionid12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
--select DISTINCT org12 ,sectionid12,ADV12M,isnull(BookADV12,0)BookADV12,  aa Metresadvance12
--from (select OrgUnitDay  org12,p.sectionid sectionid12, sum(Sqm) aa, sum(Sqm) ADV12M,SqmTotal BookADV12  from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month12 and @Month1  and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org12, sectionid12)X)l order by ADV12Percentage DESC, sectionid12 desc
--end



--if @orderby='3 Month' 
--begin
--select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3,* from
--(select org3,sectionid3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3)*100 as int) ADV3Percentage FROM(
--SELECT DISTINCT org3,sectionid3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
--select DISTINCT org3 ,sectionid3,ADV3M,isnull(BookADV3,0)BookADV3,  aa Metresadvance3
--from (select OrgUnitDay  org3,p.sectionid sectionid3, sum(Sqm) aa, sum(Sqm) ADV3M,SqmTotal  BookADV3 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org3, sectionid3)X)M order by ADV3Percentage DESC, sectionid3 desc
--END



--if @orderby='1 Month' 
--begin
--select rank() over (order by ADVRank1_ desc) tot1mnthRank,rank() over (order by ADVRank1_ desc)ADVRank1,* from
--(select org1,sectionid1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1)*100 as int) ADV1Percentage FROM(
--SELECT DISTINCT org1,sectionid1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
--select DISTINCT org1 ,sectionid1,ADV1M,isnull(BookADV1,0)BookADV1,  aa Metresadvance1
--from (select OrgUnitDay  org1,p.sectionid sectionid1, sum(Sqm) aa, sum(Sqm) ADV1M,SqmTotal  BookADV1 from PLANNING p inner join survey s on
--s.ProdMonth =p.Prodmonth and
----s.WorkplaceID =p.WorkplaceID and
----s.SectionID =p.SectionID and
--s.Activity =p.Activity and
--s.CrewMorning =p.OrgUnitDay 
--where p.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid and activity=0
--group by OrgUnitDay,P.sectionid,SqmTotal,s.WorkplaceID) c)D GROUP BY org1, sectionid1)X)n ORDER BY ADV1Percentage DESC, sectionid1 desc
--END
--end
--END

-----------

if @totalmine!='Total Mine'      ------------------ Actual/Planned----------------------------------------------
BEGIN
if @ratingBy='AB' AND @Status='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(d1.BookADV12,-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/d.ADV12M)*100 as int) ADV12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV12M,sum(TotalMetres) BookADV12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank 
end



if @orderby='3 Month' 
begin
select * from
(select   ADVRank3_  tot3mnthRank, ADVRank3_ ADVRank3,* from
(select org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(d1.BookADV3,-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/d.ADV3M)*100 as int) ADV3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV3M,sum(TotalMetres) BookADV3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank 
END

if @orderby='1 Month' 
begin
select * from
(select   ADVRank1_  tot1mnthRank, ADVRank1_ ADVRank1,* from
(select org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(d1.BookADV1,-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV1M,''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV1M,sum(TotalMetres) BookADV1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank
END
END

if @ratingBy='B' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(d1.BookADV12,-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/d.ADV12M)*100 as int) ADV12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV12M,sum(TotalMetres) BookADV12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by tot12mnthRank DESC
end


if @orderby='3 Month' 
begin
select * from
(select   ADVRank3_  tot3mnthRank, ADVRank3_ ADVRank3,* from
(select org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(d1.BookADV3,-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/d.ADV3M)*100 as int) ADV3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV3M,sum(TotalMetres) BookADV3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select * from
(select   ADVRank1_  tot1mnthRank, ADVRank1_ ADVRank1,* from
(select org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(d1.BookADV1,-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV1M,''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV1M,sum(TotalMetres) BookADV1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY tot1mnthRank DESC
END
end

if @ratingBy='P' AND @Status ='F'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by ADV12Percentage ASC) tot12mnthRank, rank() over (order by ADV12Percentage ASC)ADVRank12,* from
(select org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(d1.BookADV12,-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/d.ADV12M)*100 as int) ADV12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV12M,sum(TotalMetres) BookADV12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by ADV12Percentage
end



if @orderby='3 Month' 
begin
select * from
(select   rank() over (order by ADV3Percentage ASC) tot3mnthRank, rank() over (order by ADV3Percentage ASC)ADVRank3,* from
(select org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(d1.BookADV3,-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/d.ADV3M)*100 as int) ADV3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV3M,sum(TotalMetres) BookADV3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by ADV3Percentage
END



if @orderby='1 Month' 
begin
select * from
(select   rank() over (order by ADV1Percentage ASC) tot1mnthRank, rank() over (order by ADV1Percentage ASC)ADVRank1,* from
(select org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(d1.BookADV1,-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV1M,''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV1M,sum(TotalMetres) BookADV1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY ADV1Percentage
END
end

if @ratingBy='P' AND @Status ='T'
begin
if @orderby='12 Month'  
begin
select * from
(select   rank() over (order by ADV12Percentage DESC) tot12mnthRank, rank() over (order by ADV12Percentage DESC)ADVRank12,* from
(select org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(d1.BookADV12,-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/d.ADV12M)*100 as int) ADV12Percentage FROM(
select OrgUnitDay  org12,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month12 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV12M,sum(TotalMetres) BookADV12 from survey s
where prodmonth between @Month12 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)X)l order by ADV12Percentage DESC
end




if @orderby='3 Month' 
begin
select * from
(select   rank() over (order by ADV3Percentage DESC) tot3mnthRank, rank() over (order by ADV3Percentage DESC)ADVRank3,* from
(select org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(d1.BookADV3,-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/d.ADV3M)*100 as int) ADV3Percentage FROM(
select OrgUnitDay  org3,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth between @Month3 and @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV3M,sum(TotalMetres) BookADV3 from survey s
where prodmonth between @Month3 and @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)M order by ADV3Percentage DESC
END



if @orderby='1 Month' 
begin
select * from
(select   rank() over (order by ADV1Percentage DESC) tot1mnthRank, rank() over (order by ADV1Percentage DESC)ADVRank1,* from
(select org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(d1.BookADV1,-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
select OrgUnitDay  org1,P.sectionid sectionid1,''Prodmonth ,p.Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV1M,''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode where
p.prodmonth= @Month1 AND  P.SectionID =@sectionid AND p.ACTIVITY=1--and p.OrgUnitDay ='FMABCANURA' 
group by OrgUnitDay ,P.sectionid,p.Activity )d
left outer join
(
select distinct CrewMorning,S.sectionid sectionid1,''ProdMonth,Activity, ''aa,''ADV1M,sum(TotalMetres) BookADV1 from survey s
where prodmonth= @Month1 and S.SectionID =@sectionid AND s.ACTIVITY=1--and s.CrewMorning ='FMABCANURA'  
group by CrewMorning,S.sectionid,Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning)X)n ORDER BY ADV1Percentage DESC
END
end
END


if @totalmine='Total Mine' AND @persect ='F'
BEGIN
if @ratingBy='AP' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   rank() over (order by ADV12Percentage desc) tot12mnthRank, rank() over (order by ADV12Percentage desc)ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth  between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data


left outer join 
(select * from
(select   rank() over (order by ADV3Percentage desc) tot3mnthRank, rank() over (order by ADV3Percentage desc)ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth  where
p.prodmonth  between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3 


left outer join 
(select distinct rank() over (order by ADV1Percentage desc) tot1mnthRank,rank() over (order by ADV1Percentage desc)ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12  ORDER BY ADV12Percentage desc'
--select @TheQuery
exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select   rank() over (order by ADV3Percentage desc) tot3mnthRank, rank() over (order by ADV3Percentage DESC)ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+'  CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss  inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct rank() over (order by ADV1Percentage desc) tot1mnthRank,rank() over (order by ADV1Percentage desc)ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1 


left outer join 
(select   rank() over (order by ADV12Percentage desc) tot12mnthRank, rank() over (order by ADV12Percentage desc)ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY ADV3Percentage desc' 
--select @TheQuery
exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from 
(select distinct rank() over (order by ADV1Percentage desc) tot1mnthRank,rank() over (order by ADV1Percentage desc)ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select   rank() over (order by ADV3Percentage desc) tot3mnthRank, rank() over (order by ADV3Percentage desc)ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =s.SectionID and
sc.Prodmonth =s.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   rank() over (order by ADV12Percentage desc) tot12mnthRank, rank() over (order by ADV12Percentage desc)ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY ADV1Percentage desc' 
--SELECT @TheQuery
exec (@TheQuery)
END
end

if @ratingBy='AP' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by ADV12Percentage asc) tot12mnthRank ,rank() over (order by ADV12Percentage asc)ADVRank12,* from(
select org12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
SELECT DISTINCT org12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
select  org12 ,ADV12M,ISNULL(BookADV12,0) BookADV12,  aa Metresadvance12
from (select OrgUnitDay  org12, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,TotalMetres BookADV12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID ) c)D GROUP BY org12)X)l  order by ADV12Percentage 
END

if @orderby='3 Month' 
begin
select rank() over (order by ADV3Percentage asc) tot3mnthRank,rank() over (order by ADV3Percentage asc)ADVRank3,* from
(select org3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3  )*100 as int) ADV3Percentage FROM(
SELECT DISTINCT org3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
select  org3 ,ADV3M,isnull(BookADV3,0) BookADV3,  aa Metresadvance3
from (select OrgUnitDay  org3, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,TotalMetres  BookADV3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID ) c)D GROUP BY org3)X)M order by ADV3Percentage 
END

if @orderby='1 Month' 
begin
select rank() over (order by ADV1Percentage asc) tot1mnthRank,rank() over (order by ADV1Percentage asc)ADVRank1,* from
(select org1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1  )*100 as int) ADV1Percentage FROM(
SELECT DISTINCT org1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
select  org1 ,ADV1M,isnull(BookADV1,0)BookADV1 ,  aa Metresadvance1
from (select OrgUnitDay  org1, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV1M,TotalMetres  BookADV1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID ) c)D GROUP BY org1)X)n ORDER BY ADV1Percentage 
END
end

if @ratingBy='AB' AND @Status='T'
begin
if @orderby='12 Month'  
begin
set @TheQuery=
'select * from(
select * from
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning )X)n)data


left outer join 
(select * from
(select  ADVRank3_  tot3mnthRank,ADVRank3_ ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning  )X )M)data1 on
data .org12 =data1.org3
 

left outer join 
(select distinct ADVRank1_  tot1mnthRank,ADVRank1_ ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning )M )data2 on
data2 .org1 =data.org12   ORDER BY tot12mnthRank asc'

exec (@TheQuery)
END

if @orderby='3 Month' 
begin
set @TheQuery=
'select * from
(select * from
(select  ADVRank3_  tot3mnthRank,ADVRank3_ ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
left outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1  
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning)X)n)data


left outer join 
(select * from
(select distinct ADVRank1_  tot1mnthRank,ADVRank1_ ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M ,0) )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X )M)data1 on
data .org3 =data1.org1


left outer join 
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct  '+@type1+' CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data2 .org12 =data.org3   ORDER BY tot3mnthRank asc'

exec (@TheQuery)
END

if @orderby='1 Month' 
begin
set @TheQuery=
'select * from(
select * from (
select distinct ADVRank1_  tot1mnthRank,ADVRank1_ ADVRank1,* from
--(select org1,d.ADV1M,d1.BookADV1, rank() over (order by d.ADV1M,d1.BookADV1 asc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/d.ADV1M)*100 as int) ADV1Percentage FROM(
(select case when org1 is null then d1 .CrewMorning else org1 end org1,d.ADV1M,d1.BookADV1, rank() over (order by isnull(isnull(d1.BookADV1,0),-99999) desc) as ADVRank1_,cast((nullif(d1.BookADV1,0)/nullif(d.ADV1M,0)  )*100 as NUMERIC(18,2)) ADV1Percentage FROM(
select '+@type+'  org1,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV1M,''''BookADV1 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth
where
p.prodmonth='+@Month1+' AND p.ACTIVITY=1  
group by '+@groupby+'  ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth='+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV1M,sum(TotalMetres) BookADV1
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth ='+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth ='+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org1 =d1 .CrewMorning  )X)n)data



left outer join 
(select * from
(select  ADVRank3_  tot3mnthRank,ADVRank3_ ADVRank3,* from
(select case when org3 is null then d1 .CrewMorning else org3 end org3,d.ADV3M,d1.BookADV3, rank() over (order by isnull(isnull(d1.BookADV3,0),-99999) desc) as ADVRank3_,cast((nullif(d1.BookADV3,0)/nullif(d.ADV3M,0))*100 as NUMERIC(18,2)) ADV3Percentage FROM(
select '+@type+'  org3,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV3M,''''BookADV3 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month3+' and '+@Month1+' AND s.ACTIVITY=1 
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV3M,sum(TotalMetres) BookADV3
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month3+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month3+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org3 =d1 .CrewMorning )X )M)data1 on
data .org1 =data1.org3


left outer join 
(select   ADVRank12_  tot12mnthRank, ADVRank12_ ADVRank12,* from
(select case when org12 is null then d1 .CrewMorning else org12 end org12,d.ADV12M,d1.BookADV12, rank() over (order by isnull(isnull(d1.BookADV12,0),-99999) desc) as ADVRank12_,cast((nullif(d1.BookADV12,0)/nullif(d.ADV12M,0))*100 as NUMERIC(18,2)) ADV12Percentage FROM(
select '+@type+'  org12,''''Prodmonth ,''''Activity, sum(P.Metresadvance) aa, sum(P.Metresadvance) ADV12M,''''BookADV12 from Planning p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY=1 
group by '+@groupby+' ,P.Activity )d
FULL outer join
(
select distinct '+@type1+' CrewMorning,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12 from survey s inner join Section_Complete sc on
sc.SECTIONID =S.SectionID and
sc.Prodmonth =S.Prodmonth
where s.prodmonth between '+@Month12+' and '+@Month1+' AND s.ACTIVITY=1   
group by '+@groupby1+',Activity

union 

select distinct '+@type1+'  CrewMorning  ,''''ProdMonth,''''Activity, ''''aa,''''ADV12M,sum(TotalMetres) BookADV12
from survey ss inner join Section_Complete sc on
sc.SECTIONID =SS.SectionID and
sc.Prodmonth =SS.Prodmonth
where ss.CrewMorning not in 
(select '+@type+' from  PLANMONTH p INNER JOIN Planning pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join Section_Complete sc on
sc.SECTIONID =p.SectionID and
sc.Prodmonth =p.Prodmonth where
p.prodmonth between '+@Month12+' and '+@Month1+' AND p.ACTIVITY =1  group by '+@groupby +')
and ss.prodmonth between '+@Month12+' and '+@Month1+' AND ss.ACTIVITY =1  group by '+@groupby1+', Activity)d1 on
d.Prodmonth =d1.ProdMonth and
d .Activity =d1 .Activity and
d .org12 =d1 .CrewMorning)M )data2 on
data .org1 =data2.org12 ORDER BY tot1mnthRank asc'

exec (@TheQuery)
END
end

if @ratingBy='AB' AND @Status='F'
begin
if @orderby='12 Month'  
begin
select rank() over (order by ADVRank12_ desc) tot12mnthRank ,rank() over (order by ADVRank12_ desc)ADVRank12,* from(
select org12,Metresadvance12,BookADV12, rank() over (order by Metresadvance12,BookADV12 asc) as ADVRank12_,cast((nullif(BookADV12,0)/Metresadvance12  )*100 as int) ADV12Percentage FROM(
SELECT DISTINCT org12,SUM(Metresadvance12)Metresadvance12,sum(BookADV12) BookADV12 FROM (
select  org12 ,ADV12M,ISNULL(BookADV12,0) BookADV12,  aa Metresadvance12
from (select OrgUnitDay  org12, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV12M,TotalMetres BookADV12  from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month12 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org12)X)l  order by tot12mnthRank DESC
END

if @orderby='3 Month' 
begin
select rank() over (order by ADVRank3_ desc) tot3mnthRank,rank() over (order by ADVRank3_ desc)ADVRank3,* from
(select org3,Metresadvance3,BookADV3, rank() over (order by Metresadvance3,BookADV3 asc) as ADVRank3_,cast((nullif(BookADV3,0)/Metresadvance3  )*100 as int) ADV3Percentage FROM(
SELECT DISTINCT org3,SUM(Metresadvance3)Metresadvance3,sum(BookADV3) BookADV3 FROM (
select  org3 ,ADV3M,isnull(BookADV3,0) BookADV3,  aa Metresadvance3
from (select OrgUnitDay  org3, sum(p.Metresadvance) aa, sum(p.Metresadvance) ADV3M,TotalMetres  BookADV3 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth between @Month3 and @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org3)X)M order by tot3mnthRank DESC
END

if @orderby='1 Month' 
begin
select rank() over (order by ADVRank1_ desc) tot1mnthRank,rank() over (order by ADVRank1_ desc)ADVRank1,* from
(select org1,Metresadvance1,BookADV1, rank() over (order by Metresadvance1,BookADV1 asc) as ADVRank1_,cast((nullif(BookADV1,0)/Metresadvance1  )*100 as int) ADV1Percentage FROM(
SELECT DISTINCT org1,SUM(Metresadvance1)Metresadvance1,sum(BookADV1) BookADV1 FROM (
select  org1 ,ADV1M,isnull(BookADV1,0)BookADV1 ,  aa Metresadvance1
from (select OrgUnitDay  org1, sum(p.Metresadvance)  aa, sum(p.Metresadvance) ADV1M,TotalMetres BookADV1 from PLANNING p INNER JOIN PLANMONTH pp on p.sectionid=pp.sectionid and p.prodmonth=pp.prodmonth and
		p.workplaceid=pp.workplaceid and p.activity=pp.activity and p.plancode=pp.plancode inner join survey s on
s.ProdMonth =p.Prodmonth and
--s.WorkplaceID =p.WorkplaceID and
--s.SectionID =p.SectionID and
s.Activity =p.Activity and
s.CrewMorning =pp.OrgUnitDay  
where s.prodmonth= @Month1 and p.activity=1--and SectionID =@sectionid
group by OrgUnitDay,TotalMetres,s.WorkplaceID  ) c)D GROUP BY org1)X)n ORDER BY tot1mnthRank DESC
END
end

end


go
ALTER PROCEDURE [dbo].[Report_Production_Analysis_Stoping_Tons_Monthly_Average]
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
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
				 and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill'''
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
		'select sum(Tons) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			SUM(BookTons)/ ' + convert(varchar, @Shifts) + ' as ''Book''
		from
		PLANNING p
		inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
		where '
	IF (@TypeReport = 'M')
	BEGIN
		set @Query = @Query + '	p.ProdMonth = '''+ @ProdMonth +''' '
	END
	ELSE
	BEGIN
		set @Query = @Query + '	p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
	END
	set @Query = @Query + '	and sc.' + @ToJoin + ' = '''+ @SectionID +'''
							and p.Activity IN(0,9)
							and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill'''

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0

	--print (@Query)
END








go
ALTER PROCEDURE [dbo].[Report_Production_Analysis_Stoping_Tons]
--declare
@Prodmonth varchar(6),
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

	set @Query = 
		' select * from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate, 
				SUM(p.Tons) TonsPlanned, SUM(p.BookTons) TonsBooked
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
		if (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + 'and sc.' + @ToJoin + ' = '''+  @SectionID +'''
									and p.Activity IN (0,9)
									and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill''
			group by p.CalendarDate) as a
		order by CalendarDate '

	--print (@Query)
	exec (@Query)

END
go

ALTER PROCEDURE [dbo].[Report_Production_Analysis_Stoping_SQM_Monthly_Average]
	-- Add the parameters for the stored procedure here
--declare
@ProdMonth varchar(6),
@FromDate varchar(10),
@ToDate varchar(10),
@SectionID char(20),
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
				 and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill'''
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
		' select sum(Sqm) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			SUM(BookSqm)/ ' + convert(varchar, @Shifts) + ' as ''Book''
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
	IF (@TypeReport = 'M')
		set @Query = @Query + ' p.Prodmonth = '''+ @ProdMonth +''' '
	ELSE
		set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
	set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
							and p.Activity IN(0,9)
							and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill'''

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0
	--print (@Query)
END

go
ALTER PROCEDURE [dbo].[Report_Production_Analysis_Stoping_SQM]
	-- Add the parameters for the stored procedure here
--declare
@Prodmonth varchar(6),
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

	set @Query = 
		'select * from 
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate,
				SUM(p.Sqm) SQMPlanned, SUM(p.BookSqm) SQMBooked
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
			if (@TypeReport = 'M')
				set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
			ELSE
				set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
			set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
									and p.Activity IN (0,9)
									and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill''
			group by p.CalendarDate
		) a order by CalendarDate '

	exec (@Query)

END


GO
ALTER PROCEDURE [dbo].[Report_Production_Analysis_Stoping_KG_Monthly_Average]
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
				 and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill'''
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
		' select sum(p.grams / 1000) / ' + convert(varchar, @Shifts) + ' as ''Plan'',
			SUM(p.BookGrams / 1000) / ' + convert(varchar, @Shifts) + ' as ''Book''
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and p.Prodmonth = sc.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
	IF (@TypeReport = 'M')
		set @Query = @Query + ' p.ProdMonth = '''+ @ProdMonth +''' '
	ELSE
		set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
	set @Query = @Query + ' and sc.' + @ToJoin + ' = '''+ @SectionID +'''
							 and p.Activity IN(0,9)
							 and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill'''

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0

	--print(@Query)

END



GO
ALTER PROCEDURE [dbo].[Report_Production_Analysis_Stoping_KG]
	-- Add the parameters for the stored procedure here
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


	set @Query = 
		'select * from
			(select  case when (LEN(convert(varchar, datepart(MM, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(MM, p.CalendarDate)) else convert(varchar, datepart(MM, p.CalendarDate)) end + ''/'' + case when (LEN(convert(varchar, datepart(DD, p.CalendarDate)))) = 1 then ''0'' + convert(varchar, datepart(DD, p.CalendarDate)) else convert(varchar, datepart(DD, p.CalendarDate)) end CalendarDate, 
				convert(decimal(18, 3), SUM(ISNULL(p.grams, 0) / 1000)) as ''Plan'', 
				convert(decimal(18, 3), SUM(ISNULL(p.BookGrams, 0) / 1000)) as ''Book''
			from
			PLANNING p
			inner join Section_Complete sc on p.SectionID = sc.SectionID and sc.Prodmonth = p.Prodmonth INNER JOIN CALTYPE CT ON
		P.Calendardate=CT.Calendardate
			where '
		if (@TypeReport = 'M')
			set @Query = @Query + ' p.Prodmonth = '''+ @Prodmonth +''' '
		ELSE
			set @Query = @Query + ' p.CalendarDate >= '''+ @FromDate +''' and p.CalendarDate <= '''+ @ToDate +''' '
		set @Query = @Query + ' and sc.' + @ToJoin + ' = ''' + convert(varchar, @SectionID) + '''
								and p.Activity IN (0,9)
								and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill''
			group by p.CalendarDate) as a
		order by CalendarDate '

	--print (@Query)
	exec (@Query)

END






GO


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
				 and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill'''
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
				convert(decimal(18, 0), case when SUM(p.bookreefsqm) <> 0 then sum(p.Bookcmgt * p.bookreefsqm) / SUM(p.bookreefsqm) else 0 end) as ''BookValue''
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
									and CT.WorkingDay = ''Y'' AND CT.CALENDARCODE=''Mill''
			group by p.CalendarDate
		) as a '

	IF (@Shifts <> 0)
		exec (@Query)
	else
		select 0, 0

END
go

IF NOT EXISTS
(select * from INFORMATION_SCHEMA .COLUMNS C WHERE TABLE_NAME  ='USERS' and COLUMN_NAME ='WPProduction')
BEGIN
ALTER TABLE [dbo].[USERS] add WPProduction varchar(1)
END

IF NOT EXISTS
(select * from INFORMATION_SCHEMA .COLUMNS C WHERE TABLE_NAME  ='USERS' and COLUMN_NAME ='WPSurface')
BEGIN
ALTER TABLE [dbo].[USERS] add WPSurface varchar(1)
END

IF NOT EXISTS
(select * from INFORMATION_SCHEMA .COLUMNS C WHERE TABLE_NAME  ='USERS' and COLUMN_NAME ='WPUnderGround')
BEGIN
ALTER TABLE [dbo].[USERS] add WPUnderGround varchar(1)
END

IF NOT EXISTS
(select * from INFORMATION_SCHEMA .COLUMNS C WHERE TABLE_NAME  ='USERS' and COLUMN_NAME ='WPEditName')
BEGIN
ALTER TABLE [dbo].[USERS] add WPEditName varchar(1)
END

IF NOT EXISTS
(select * from INFORMATION_SCHEMA .COLUMNS C WHERE TABLE_NAME  ='USERS' and COLUMN_NAME ='WPEditAttribute')
BEGIN
ALTER TABLE [dbo].[USERS] add WPEditAttribute varchar(1)
END

IF NOT EXISTS
(select * from INFORMATION_SCHEMA .COLUMNS C WHERE TABLE_NAME  ='USERS' and COLUMN_NAME ='WPClassify')
BEGIN
ALTER TABLE [dbo].[USERS] add WPClassify varchar(1)
END





go
insert into CODE_PROBLEM values('B','Blasting',0,0,'N',9,'Gainsboro','N')
insert into CODE_PROBLEM values('B','Blasting',1,0,'N',9,'Gainsboro','N')
insert into CODE_PROBLEM values('C','Cleaning',0,0,'N',7,'SteelBlue','N')
insert into CODE_PROBLEM values('C','Cleaning',1,0,'N',7,'SteelBlue','N')
insert into CODE_PROBLEM values('D','Geological and environment',0,0,'N',5,'Brown','N')
insert into CODE_PROBLEM values('D','Geological and environment',1,0,'N',5,'Brown','N')
insert into CODE_PROBLEM values('E','Engineering',0,0,'N',10,'LightBlue','N')
insert into CODE_PROBLEM values('E','Engineering',1,0,'N',10,'LightBlue','N')
insert into CODE_PROBLEM values('F','Face-time',0,0,'N',9,'Gainsboro','N')
insert into CODE_PROBLEM values('F','Face-time',1,0,'N',9,'Gainsboro','N')
insert into CODE_PROBLEM values('L','Labour',0,0,'N',8,'Blue','N')
insert into CODE_PROBLEM values('L','Labour',1,0,'N',8,'Blue','N')
insert into CODE_PROBLEM values('P','Partial Blast',0,0,'N',1,'DarkOrange','N')
insert into CODE_PROBLEM values('P','Partial Blast',1,0,'N',1,'DarkOrange','N')
insert into CODE_PROBLEM values('Q','Equipping',0,0,'N',2,'CadetBlue','N')
insert into CODE_PROBLEM values('Q','Equipping',1,0,'N',2,'CadetBlue','N')
insert into CODE_PROBLEM values('S','Safety',0,0,'N',6,'Red','N')
insert into CODE_PROBLEM values('S','Safety',1,0,'N',6,'Red','N')
insert into CODE_PROBLEM values('T','Stores',0,0,'N',3,'DarkBlue','N')
insert into CODE_PROBLEM values('T','Stores',1,0,'N',3,'DarkBlue','N')

CREATE TABLE [dbo].[Temp_Problem_Analysis](
	[UserID] [varchar](30) NULL,
	[ProblemCode] [varchar](2) NULL
) ON [PRIMARY]

GO

CREATE View [dbo].[vw_Problem_Complete] as
select p.problemid, p.activity, p.color ProblemColor, p.colortext ProblemColorText, p.description Problem, p.displayorder, p.drillrig, 
n.Description Note, n.noteid, n.Color , n.Explanation, n.NotLostBlastProblem,
t.ProblemTypeID, t.Description Type, t.Color TypeColor from  code_problem p 
inner join problem_note pn on p.problemid = pn.problemid and p.activity = pn.activity
inner join code_problem_note n on n.noteid = pn.noteid and n.activity = pn.activity
inner join problem_type pt on pt.ProblemID = p.problemid and pt.activity = p.activity
inner join code_problem_type t on t.ProblemTypeID = pt.ProblemTypeID and t.activity = pt.activity

GO


go
create Procedure [dbo].[SP_Problem_Analysis_Report_Stp]  
--Declare 
        @Period VarChar(10),
        @FromMonth Numeric(7),
        @ToMonth Numeric(7),		
		@Section VarChar(50),
		@UserID VarChar(200),
		@FromDate DateTime,
        @ToDate DateTime


as

--Select 
	--set	@Period = 'FromTo'
	--set	@FromMonth = 201703
 --   set    @ToMonth = 201703
	--set	@Section = 'S Mofokeng'
	--set	@UserID = 'MINEWARE'

	--set	@FromDate = '2017-03-12'
 --   set    @ToDate = '2017-06-08'

Declare @TheLevel Int
Declare @TheSecionID VarChar(50)

Declare @SQL Varchar(MAX)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Tomonth and
name = @Section

--SELECT @TheLevel

if @TheLevel = 1
  set @TheSecionID = 'Name_5'
  
if @TheLevel = 2
  set @TheSecionID = 'Name_4'
  
if @TheLevel = 3
  set @TheSecionID = 'Name_3'    
  
if @TheLevel = 4
  set @TheSecionID = 'Name_2'
  
if @TheLevel = 5
  set @TheSecionID = 'Name_1'
  
if @TheLevel = 6
  set @TheSecionID = 'Name' 


--SELECT * FROM SECTION_COMPLETE

Set @SQL = '
select 
a.Prodmonth,
a.Calendardate,
f.Name_2 MO,
f.Name Miner,
a.WorkplaceID,
g.Description TheWorkplace,
b.ProblemID+'':''+b.Problem TheProblem,
 TheNote = case when b.NoteID = ''OTHR'' then
 b.NoteID+'':''+a.SBossNotes
else
b.NoteID+'':''+b.Note end,
a.BookSQM Book_SQM,
a.SQM Plan_SQM,
convert(int, y.FL*1) Pot_SQM,
a.SQM-a.BookSQM Plan_Var,
convert(int, (y.FL*1)-a.BookSQM) Pot_Var,
LostBlast = case when a.Causedlostblast = ''Y'' then ''Yes'' else ''No'' end

  from
PLANNING a
  inner join
    vw_Problem_Complete b on
      a.problemid = b.problemid
      and a.activity = b.activity 
      and a.activity in (0,3)
  inner join
    Section_Complete f on
      a.Prodmonth = f.Prodmonth and
      a.sectionid = f.sectionid
  inner join
    workplce g on
      a.workplaceid = g.workplaceid
 -- inner join
 --   PLANNING z on
 --   a.prodmonth = z.prodmonth and
 --   a.sectionid = z.sectionid and
 --   a.workplaceid = z.workplaceid and
 --   a.activity = z.activity and
 --   a.Calendardate = z.Calendardate and
	--a.PLanCode = z.PLanCode
    and a.iscubics = ''N'' and
	a.PlanCode = ''MP''
  Left Outer join
    Planmonth y on
    a.prodmonth = y.prodmonth and
    a.sectionid = y.sectionid and
    a.workplaceid = y.workplaceid and
    a.activity = y.activity
    and y.iscubics = ''N''and
	y.PlanCode = ''MP''
where '

if @Period = 'Prodmonth'
Begin
set @SQL = @SQL + 
	'a.prodmonth >= '+convert(Varchar(7),@Frommonth)+'
	and a.prodmonth <= '+convert(Varchar(7),@Tomonth)+'
	and f.'+@TheSecionID+' = '''+@Section+'''
	and b.ProblemID  in (Select ProblemCode from Temp_Problem_Analysis Where UserID = '''+@UserID+''')

	order by a.Prodmonth, f.SectionID_2,
	a.Calendardate, f.Name,
	g.Description,  a.ProblemID'
end

if @Period = 'FromTo'
Begin
set @SQL = @SQL +
	'a.calendardate BETWEEN '''+ CONVERT(VARCHAR(10), @FromDate, 111) + ''' AND '''+ CONVERT(VARCHAR(10), @ToDate, 111) +'''
	and f.'+@TheSecionID+' = '''+@Section+'''
	and b.ProblemID  in (Select ProblemCode from Temp_Problem_Analysis Where UserID = '''+@UserID+''')

	order by a.Prodmonth, f.SectionID_2,
	a.Calendardate, f.Name,
	g.Description,  a.ProblemID'
end

--select (@SQL)
--PRINT (@SQL)
exec(@SQL)

--GO

go


create Procedure [dbo].[SP_Problem_Analysis_Report_Dev]  
--Declare 

		@Period VarChar(10),
        @FromMonth Numeric(7),
        @ToMonth Numeric(7),		
		@Section VarChar(50),
		@UserID VarChar(200),
		@FromDate DateTime,
        @ToDate DateTime
as

--Select 
	--		set	@Period = 'FromTo'
	--set	@FromMonth = 201703
 --   set    @ToMonth = 201703
	--set	@Section = 'S Mofokeng'
	--set	@UserID = 'MINEWARE'

	--set	@FromDate = '2017-03-12'
 --   set    @ToDate = '2017-06-08'

Declare @TheLevel Int
Declare @TheSecionID VarChar(50)

Declare @SQL Varchar(MAX)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Tomonth and
name = @Section

if @TheLevel = 1
  set @TheSecionID = 'Name_5'
  
if @TheLevel = 2
  set @TheSecionID = 'Name_4'
  
if @TheLevel = 3
  set @TheSecionID = 'Name_3'    
  
if @TheLevel = 4
  set @TheSecionID = 'Name_2'
  
if @TheLevel = 5
  set @TheSecionID = 'Name_1'
  
if @TheLevel = 6
  set @TheSecionID = 'Name'

Set @SQL = '
select 
a.Prodmonth,
a.Calendardate,
f.Name_2 MO,
f.Name Miner,
a.WorkplaceID,
g.Description TheWorkplace,
b.ProblemID+'':''+b.Problem TheProblem,
 TheNote = case when b.NoteID = ''OTHR'' then
 b.NoteID+'':''+a.SBossNotes
else
b.NoteID+'':''+b.Note end,
a.BookMetresadvance Book_M,
a.Metresadvance Plan_M,
convert(Numeric(5,1), 1) Pot_M,
a.Metresadvance-a.BookMetresadvance Plan_Var,
convert(Numeric(5,1), 1-a.BookMetresadvance) Pot_Var,
LostBlast = case when a.Causedlostblast = ''Y'' then ''Yes'' else ''No'' end

  from
PLANNING a
  inner join
    vw_Problem_Complete b on
      a.problemid = b.problemid
      and a.activity = b.activity 
      and a.activity in (1)
  inner join
    Section_Complete f on
      a.Prodmonth = f.Prodmonth and
      a.sectionid = f.sectionid
  inner join
    workplce g on
      a.workplaceid = g.workplaceid
 -- inner join
 --   PLANNING z on
 --   a.prodmonth = z.prodmonth and
 --   a.sectionid = z.sectionid and
 --   a.workplaceid = z.workplaceid and
 --   a.activity = z.activity and
 --   a.Calendardate = z.Calendardate and
	--a.PLanCode = z.PLanCode
    and a.iscubics = ''N'' and
	a.PlanCode = ''MP''
  Left Outer join
    Planmonth y on
    a.prodmonth = y.prodmonth and
    a.sectionid = y.sectionid and
    a.workplaceid = y.workplaceid and
    a.activity = y.activity
    and y.iscubics = ''N''and
	y.PlanCode = ''MP''
where '

if @Period = 'Prodmonth'
Begin
set @SQL = @SQL + 
	'a.prodmonth >= '+convert(Varchar(7),@Frommonth)+'
	and a.prodmonth <= '+convert(Varchar(7),@Tomonth)+'
	and f.'+@TheSecionID+' = '''+@Section+'''
	and b.ProblemID  in (Select ProblemCode from Temp_Problem_Analysis Where UserID = '''+@UserID+''')

	order by a.Prodmonth, f.SectionID_2,
	a.Calendardate, f.Name,
	g.Description,  a.ProblemID'
end

if @Period = 'FromTo'
Begin
set @SQL = @SQL +
	'a.calendardate BETWEEN '''+ CONVERT(VARCHAR(10), @FromDate, 111) + ''' AND '''+ CONVERT(VARCHAR(10), @ToDate, 111) +'''
	and f.'+@TheSecionID+' = '''+@Section+'''
	and b.ProblemID  in (Select ProblemCode from Temp_Problem_Analysis Where UserID = '''+@UserID+''')

	order by a.Prodmonth, f.SectionID_2,
	a.Calendardate, f.Name,
	g.Description,  a.ProblemID'
end

--select @SQL
--print @SQL
exec(@SQL)

--GO
















