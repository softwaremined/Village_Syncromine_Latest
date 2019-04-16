
--drop table [CODE_SIGNOFFS]

CREATE TABLE [dbo].[CODE_SIGNOFFS](
	
	[Name] [varchar](10) NOT NULL,
	[OrderBy] int NULL,
	[Description] [varchar](50) NULL) ON [PRIMARY]

GO


insert into CODE_SIGNOFFS
values ('Survey',1,'Mine Overseer')
go
insert into CODE_SIGNOFFS
values ('Survey',2,'Production Manager')
go
insert into CODE_SIGNOFFS
values ('Survey',3,'Mining Manager')
go
insert into CODE_SIGNOFFS
values ('Survey',4,'Planner')
go
insert into CODE_SIGNOFFS
values ('Survey',5,'Rock Mechanics')
go
insert into CODE_SIGNOFFS
values ('Survey',6,'Evaluation')
go
insert into CODE_SIGNOFFS
values ('Survey',7,'Survey')
go
insert into CODE_SIGNOFFS
values ('Survey',8,'Geology')
go

drop table  BOOKINGPROBLEM
go
drop table SICCLEANED
go

update planning 
set PegID = 'START:0.0'
where pegid='start:0.00'
go

update planning 
set PegID = 'START:0.0'
where pegid='start:0.0'
go


  update  CODE_PROBLEM_TYPE
  set deleted = 'N'
  go
  update CODE_PROBLEM_NOTE
    set deleted = 'N'
	go
 update CODE_PROBLEM
   set deleted = 'N'
   go

 update PROBLEM_NOTE
   set deleted = 'N'
   go
update problem_type
  set deleted = 'N'
  go

CREATE TABLE [dbo].[Code_MiningMethod](
	[MethodID] [int] IDENTITY(1,1) NOT NULL,
	[MethodDesc] [varchar](40) NULL,
	[MethodShortDesc] [varchar](3) NULL
) ON [PRIMARY]

GO



ALTER procedure [dbo].[sp_GetShiftMO]
@UserID varchar(20),
@Prodmonth varchar(10),
@SectionID varchar (20)
as
declare @BeginDate Datetime
declare @EndDate Datetime
declare @TheDate Datetime
declare @Calendarcode varchar(50)
declare @TheCount integer
declare @Calendardate Datetime
declare @Workday Char(1)
declare @TotalShifts varchar(2)


select 
  @BeginDate = begindate, @EndDate = enddate, 
  @Calendarcode = calendarcode, @TotalShifts = TotalShifts 
from Seccal p 
inner join Section_Complete sc on
  sc.Prodmonth = p.Prodmonth and
  sc.sectionID_1 = p.sectionid
where p.prodmonth = @Prodmonth and
      sc.SectionID_2 = @SectionID


set @TheDate = @Begindate
set @TheCount = 0
set @WorkDay = 'Y'
Declare Nonworkdays cursor
for select Calendardate from Caltype where calendarcode = @Calendarcode and 
calendardate >= @Begindate and calendardate <= @Enddate and workingday = 'N'
order by calendardate

OPEN Nonworkdays
FETCH NEXT FROM Nonworkdays
into @Calendardate
WHILE (@TheDate <= @EndDate)
BEGIN
if @Calendardate = @TheDate
begin 
	set @WorkDay = 'N'
end	
 if @Workday = 'Y' 
 begin
	set @TheCount = @TheCount + 1
 end
 insert into [TempWorkdaysMO] values (@UserID, @SectionID, @ProdMonth, @TheDate, @Workday, @TheCount, @TotalShifts)
 set @TheDate = @TheDate + 1
 set @WorkDay = 'Y'
 if @TheDate > @Calendardate 
begin
FETCH NEXT FROM Nonworkdays
 into @Calendardate
 END
End
 CLOSE Nonworkdays
 DEALLOCATE Nonworkdays


 go
CREATE TABLE [dbo].[Temp_MOStartDate](
	[UserID] [varchar](20) NULL,
	[SectionID] [varchar](20) NULL,
	[ProdMonth] [numeric](7, 0) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[Temp_SectionStartDate](
	[UserID] [varchar](20) NULL,
	[SectionID] [varchar](20) NULL,
	[SectionID_1] [varchar](20) NULL,
	[ProdMonth] [numeric](7, 0) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[PrevProdMonth] [numeric](7, 0) NULL,
	[PrevStartDate] [datetime] NULL,
	[PrevEndDate] [datetime] NULL
) ON [PRIMARY]

GO


Create View [dbo].[vw_Sections_Complete_MO] as
SELECT DISTINCT SECTIONS.Prodmonth, 
 SECTIONS_2.SECTIONID AS SECTIONID_2,
 SECTIONS_2.NAME AS NAME_2, 
 SECTIONS_3.SECTIONID AS SECTIONID_3,
 SECTIONS_3.NAME AS NAME_3,
 SECTIONS_4.SECTIONID AS SECTIONID_4,
 SECTIONS_4.NAME AS NAME_4,
 SECTIONS_5.SECTIONID AS SECTIONID_5,
 SECTIONS_5.NAME AS NAME_5
FROM dbo.section SECTIONS INNER JOIN
dbo.section SECTIONS_1 ON SECTIONS.Prodmonth = SECTIONS_1.Prodmonth AND 
SECTIONS.ReportToSectionID  = SECTIONS_1.SECTIONID INNER JOIN
dbo.section SECTIONS_2 ON SECTIONS_1.Prodmonth = SECTIONS_2.Prodmonth AND 
SECTIONS_1.ReportToSectionID = SECTIONS_2.SECTIONID INNER JOIN
dbo.section SECTIONS_3 ON SECTIONS_2.Prodmonth = SECTIONS_3.Prodmonth AND 
SECTIONS_2.ReportToSectionID = SECTIONS_3.SECTIONID INNER JOIN
dbo.section SECTIONS_4 ON SECTIONS_3.Prodmonth = SECTIONS_4.Prodmonth AND 
SECTIONS_3.ReportToSectionID = SECTIONS_4.SECTIONID INNER JOIN
dbo.section SECTIONS_5 ON SECTIONS_4.Prodmonth = SECTIONS_5.Prodmonth AND 
SECTIONS_4.ReportToSectionID = SECTIONS_5.SECTIONID


GO


ALTER Procedure [dbo].[sp_SICCapture_Load] 
--Declare 
	@TypeMonth varchar(20),
    @ProdMonth varchar(6),
    @SectionID VarChar(20),
	@SectionLevel varchar(1),
	@KPI varchar(50)
as

--set @TypeMonth = 'Production'
--set @Prodmonth = 201703
--set @SectionID = 'RECA'
--set @SectionLevel = 5  --5 for production and Cleaning
--set @KPI = 'Cleaned'

declare @StartDate varchar(10)
declare @EndDate varchar(10)
declare @Diffs varchar(10)
declare @TotalShifts int
declare @CalendarCode varchar(20)

declare @SelectID varchar(20)

declare @RunDate varchar(10)
select @RunDate =  convert(varchar(10), RunDate, 120) from SYSSET

declare @HierID int
select @HierID = MOHierarchicalID from Sysset
IF (@HierID = @SectionLevel)
	 set @SelectID = 'SectionID_2'
ELSE
	set @SelectID = 'SectionID_1'

IF (@TypeMonth = 'Safety') or
   (@TypeMonth = 'Costing')
BEGIN
	select @StartDate = convert(varchar(10),StartDate,120), 
		@EndDate = convert(varchar(10),EndDate,120), 
		@Diffs = datediff(d,Startdate,enddate) + 1, 
		@TotalShifts = convert(varchar(2),TotalShifts) 
	from CalendarOther 
	where [Month] = @ProdMonth and
		  CalendarCode = @TypeMonth
END
IF (@TypeMonth = 'Production')
BEGIN
	IF (@HierID = @SectionLevel)
	BEGIN
		select top 1 @StartDate = convert(varchar(10),s.BeginDate,120), 
			@EndDate = convert(varchar(10),s.EndDate,120), 
			@diffs = datediff(d,s.BeginDate,s.EndDate) + 1, 
			@TotalShifts = s.TotalShifts, 
			@CalendarCode = s.CalendarCode 
		from SECCAL s 
		inner join Section_Complete sc on 
			sc.ProdMonth = s.Prodmonth and 
			sc.SectionID_1 = s.SectionID 
		where s.ProdMonth = @ProdMonth and
				sc.SectionID_2 = @SectionID
	END
	ELSE
	BEGIN
		select top 1 @StartDate = convert(varchar(10),s.BeginDate,120), 
			@EndDate = convert(varchar(10),s.EndDate,120), 
			@diffs = datediff(d,s.BeginDate,s.EndDate) + 1, 
			@TotalShifts = s.TotalShifts, 
			@CalendarCode = s.CalendarCode 
		from SECCAL s 
		inner join Section_Complete sc on 
			sc.ProdMonth = s.Prodmonth and 
			sc.SectionID_1 = s.SectionID 
		where s.ProdMonth = @ProdMonth and
				sc.SectionID_1 = @SectionID
	END
END
IF (@TypeMonth = 'Mill')
BEGIN
	select @StartDate = convert(varchar(10),a.StartDate,120), 
		@EndDate = convert(varchar(10),a.EndDate,120), 
		@diffs = datediff(d,a.Startdate,a.enddate) + 1, 
		@TotalShifts = a.TotalShifts 
	from 
    (
		SELECT DISTINCT StartDate, EndDate, TotalShifts 
		from CALENDARMILL 
		where MillMonth = @ProdMonth and 
			  CalendarCode = @TypeMonth
	) a 
END

declare @TheSICKey int 
declare @TheKPI varchar(30) 
declare @TheDescription varchar(50)

IF (@KPI <> 'Cleaned')
BEGIN
	--delete  from Linda
	if object_id('#tempSic') is not null
   		drop table #tempSic
	CREATE TABLE #tempSic(SICKey int, KPI varchar(30) , Description VARCHAR(50), 
							CalendarDate DateTime, ShiftNo varchar(2), TotalShifts varchar(2), 
							WorkingDay varchar(1), TheValue numeric(11,4), ProgValue numeric(11,4), ShiftValue decimal)

	DECLARE AA_Cursor CURSOR FOR
	select SICKey, KPI, Description from Code_SICCapture where KPI = @KPI

	OPEN AA_Cursor;
	FETCH NEXT FROM AA_Cursor
	into @TheSICKey, @TheKPI, @TheDescription;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		insert into #tempSic  --Linda
		select @TheSICKey, @TheKPI, @TheDescription, 
		ct.CalendarDate,
		ShiftNo = (select count(calendarDate) ShiftNo from Caltype where CalendarCode = @TypeMonth and
				   CalendarDate >= @StartDate and CalendarDate <= ct.CalendarDate and
				   WorkingDay = 'Y'),
		cc.TotalShifts, ct.WorkingDay,  
		TheValue = isnull(s.[Value],0), 0, 0
		from CalType ct
		left outer join SicCapture s on
		   s.CalendarDate = ct.CalendarDate and
		   s.SicKey = @TheSICKey and
		   s.SectionID = @SectionID
		left outer join 
			(select CalendarCode, count(CalendarDate) TotalShifts from CalType
			 where CalendarCode = @TypeMonth and
				   CalendarDate >= @StartDate and
				   CalendarDate <= @EndDate
			 group by CalendarCode) cc on
			ct.CalendarCode = cc.CalendarCode
		where ct.CalendarCode = @TypeMonth and
			@StartDate <= ct.CalendarDate and
			@EndDate >= ct.CalendarDate
		FETCH NEXT FROM AA_Cursor
		 into @TheSICKey, @TheKPI, @TheDescription;
	END; 
	CLOSE AA_Cursor;
	DEALLOCATE AA_Cursor;

	update #tempSic  --Linda
	set ProgValue =
	(select sum(Value) sumValue from SICCAPTURE where
	  SectionID = @SectionID and
	  CalendarDate >= @StartDate and
	  CalendarDate <= @EndDate and
	  SICKey = #tempSic.SICKey)  --linda.SICKey)

	update #tempSic  --Linda
	set ShiftValue =
	(select shiftValue = sum(case when Value > 0 then 1 else 0 end) from SICCAPTURE where
	  SectionID = @SectionID and
	  CalendarDate >= @StartDate and
	  CalendarDate <= @EndDate and
	  SICKey = #tempSic.SICKey)  --linda.SICKey)

	select SICKey, KPI, Description, 
		CalendarDate, 
		ShiftNo = case when WorkingDay = 'Y' then ShiftNo else 'N' end, 
		TotalShifts, 
		TheValue = case when isnull(TheValue,0) = 0 then '' else
		convert(varchar(10), convert(numeric(7,2),TheValue)) end,
		ProgValue = case when SicKey in (7,14,21) then ''
				when ShiftValue > 0 then convert(varchar(10), convert(numeric(7,2),ProgValue / ShiftValue))
				else '' end					
	from #tempSic  --Linda
	order by SicKey, CalendarDate

	DEALLOCATE aa_cursor
	drop table #tempSic
END
ELSE
BEGIN
	select * from
	(
		select SICKey = case 
					  when z.[Type] = 'Book FL' then ''
					  when z.[Type] = 'Plan' then '22'
					  when z.[Type] = 'Book' then '23' else '' end,
		  p.Sectionid +' : '+sc.Name Name, 
		  --p.Sectionid SectionID, sc.Name Name, 
		  p.Workplaceid+' : '+w.Description Workplace,
		  --p.Workplaceid WorkplaceID, w.Description Description, 
		  p.Activity Activity, 
		  p.CalendarDate CalendarDate,
		  s23.ProblemCode, s23.SBNotes, isnull(s23.CausedLostBlast,'') CausedLostBlast,
		  ShiftNo = case when ct.WorkingDay = 'Y' then convert(varchar(2), p.ShiftDay) else 'N' end, 
		  TheValue = case 
					  when z.[Type] = 'Book FL' and p.Activity <> 1 and isnull(p.BookFL,0) = 0 and ct.WorkingDay = 'Y' then '0'
					  when z.[Type] = 'Book FL' and p.Activity <> 1 and isnull(p.BookFL,0) > 0 and ct.WorkingDay = 'Y' then 
							convert(varchar(10), convert(Numeric(7,0),isnull(p.BookFL,0)))
					  when z.[Type] = 'Plan' and p.Activity <> 1 and isnull(s22.Value,0) = 0 then ''
					  when z.[Type] = 'Plan' and p.Activity <> 1 and isnull(s22.Value,0) > 0 then 
							convert(varchar(10), convert(Numeric(7,0),isnull(s22.Value,0)))
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.ProblemCode, '') <> '' then 
							isnull(s23.ProblemCode, '')
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.Value,0) = 0 then '' 
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.Value,0) > 0 then
						   convert(varchar(10), convert(Numeric(7,0),isnull(s23.Value, 0)))
					  when z.[Type] = 'Book FL' and p.Activity = 1 then '' 
					  when z.[Type] = 'Plan' and p.Activity = 1 and isnull(s22.Value,0) = 0 then ''
					  when z.[Type] = 'Plan' and p.Activity = 1 and isnull(s22.Value,0) = 99 then 
							'Yes'
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.ProblemCode, '') <> '' then 
							isnull(s23.ProblemCode, '')
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.Value,0) = 0 then '' 
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.Value,0) = 99 then
						  'Yes' else ''
				  end,
		  Type = z.[Type],
		  [Order] 
		from  Planning p 
		inner join Section_Complete sc on 
		  sc.ProdMonth = p.ProdMonth and 
		  sc.SectionID = p.SectionID
		inner join Seccal ss on
		  ss.ProdMonth = sc.ProdMonth and
		  ss.SectionID = sc.SectionID_1 
	    inner join CalType ct on
		  ss.CalendarCode = ct.CalendarCode and
		  ss.BeginDate <= ct.CalendarDate and
		  ss.Enddate >= ct.CalendarDate and
		  ct.CalendarDate = p.Calendardate
	    left outer join SICCapture s22 on
		  s22.SectionID = sc.SectionID_1 and
		  s22.WorkplaceID = p.WorkplaceID and
		  s22.CalendarDate = ct.CalendarDate and
		  s22.SICKey = 22
	    left outer join SICCapture s23 on
		  s23.SectionID = sc.SectionID_1 and
		  s23.WorkplaceID = p.WorkplaceID and
		  s23.CalendarDate = ct.CalendarDate and
		  s23.SICKey = 23
	    inner join WORKPLACE w on 
		  w.Workplaceid = p.WorkplaceID,
	    (select [Order] = 1,
		  Type = 'Book FL'
	     union
	     select [Order] = 2,
		  Type = 'Plan'
	     union
	     select [Order] = 3,
		  Type = 'Book') z
	    where sc.SectionID_1 = @SectionID and 
			  p.Prodmonth = @ProdMonth and
			  p.PlanCode = 'MP' and
			  p.IsCubics = 'N'
	--and ct.calendardate <= ss.rundate
	 ) a 
	 order by Name, Workplace, CalendarDate, [Order]    
END
go


ALTER Procedure [dbo].[sp_Survey_DEV_Load] 
--Declare 
    @ProdMonth varchar(6),
    @SectionID VarChar(20)
as

--set @Prodmonth = 201702
--set @SectionID = 'REAAHDA'

Select p.WorkplaceID WorkplaceID, w.description as Workplace,
	convert(numeric(7,1), isnull(p.Metresadvance, 0)) PlanAdv,
	convert(numeric(7,1), isnull(pd.BookAdv, 0)) BookAdv,
	convert(numeric(7,1), isnull(s.Adv, 0)) MeasAdv,
	convert(numeric(7,2), isnull(s.Width, 0)) Width,
	convert(numeric(7,2), isnull(s.Height, 0)) Height,
	convert(numeric(7,0), isnull(s.Grade, 0)) Grade,
	convert(numeric(7,0), isnull(s.Cubics, 0)) Cubics,
	convert(numeric(7,0), isnull(s.Content, 0)) Content, 
	convert(numeric(7,0), isnull(s.Tons, 0)) Tons, 
	convert(numeric(7,3), isnull(s.KGs, 0)) KG,
	convert(numeric(7,3), isnull(s.GTs, 0)) GT,
	convert(numeric(7,3), isnull(s.RMs, 0)) RM
from PlanMonth p
inner join Section_Complete sc on
   sc.ProdMonth = p.ProdMonth and
   sc.SectionID = p.SectionID
inner join Workplace w on
  p.WorkplaceID = w.WorkplaceID
left outer join
	(select s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity,
			Adv = sum(Reefmetres + Wastemetres),
			Height = case when sum(Mainmetres) > 0 then sum(Mainmetres * MeasHeight) / sum(Mainmetres) else 0 end,
			Width = case when sum(Mainmetres) > 0 then sum(Mainmetres * MeasWidth) / sum(Mainmetres) else 0 end,
			Grade = case when sum(Mainmetres) > 0 then sum(Mainmetres * cmgt) / sum(Mainmetres) else 0 end,
			Cubics = sum(cubicsReef + CubicsWaste),
			Tons = SUM(s.TonsTotal),
			Content = SUM(s.TotalContent),
			KGs = SUM(s.TotalContent) / 1000,
			GTs = SUM(Convert(numeric(7,2), isnull(s.GT,0))),
			RMs = SUM(Convert(numeric(7,2), isnull(s.Reefmetres,0)))
	 from Survey s
	 where s.Prodmonth = @ProdMonth and
		   s.Activity = 1 and
		   s.SectionID = @SectionID
	 group by s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity
	 ) s on
	   p.ProdMonth = s.Prodmonth and
	   p.SectionID = s.SectionID and
	   p.Activity = s.Activity  and
	   p.WorkplaceID = s.WorkplaceID
left outer join
	(select ProdMonth, WorkplaceID, SectionID, Activity, PlanCode, IsCubics,
			BookAdv = sum(BookMetresadvance)
	 from Planning 
	 where Prodmonth = @ProdMonth and
		   Activity = 1 and
		   SectionID = @SectionID and
		   PlanCode = 'MP' and
		   IsCubics = 'N'
	 group by ProdMonth, WorkplaceID, SectionID, Activity, PlanCode, IsCubics) pd on
	   p.ProdMonth = pd.Prodmonth and
	   p.SectionID = pd.SectionID and
	   p.Activity = pd.Activity  and
	   p.WorkplaceID = pd.WorkplaceID and
	   p.PlanCode = pd.PlanCode and
	   p.IsCubics = pd.IsCubics
where p.SectionID = @SectionID and 
      p.activity = 1 and
      p.Prodmonth = @ProdMonth and
	  p.PlanCode = 'MP' and
	  p.IsCubics = 'N'
 
go


ALTER Procedure [dbo].[sp_Survey_DEV_Load_Detail] 
--Declare 
    @ProdMonth varchar(6),
    @SectionID VarChar(20),
	@WorkplaceID VarChar(12),
	@SeqNo VarChar(7)
as

--set @Prodmonth = 201701
--set @SectionID = 'RECAHGA'
--set @WorkplaceID = 'RE010043'
--set @SeqNo = '1'


select CalendarDate,  
   Dip = Convert(numeric(7,0), isnull(Dip,0)),  
   BallDepth = Convert(numeric(7,1), isnull(BallDepth,0)),  
   MineMethod = Convert(numeric(7,0), isnull(MineMethod,'')),  
   Density = case when isnull(Density, 0) = 0 then '' else 'Insitu: '+convert(varchar(10),Convert(numeric(7,2), isnull(Density,0))) end,  
   Indicator = Convert(numeric(7,0), isnull(Indicator,-1)),  
   CrewMorning = CrewMorning,  
   CrewAfternoon = CrewAfternoon,  
   CrewEvening = CrewEvening,  
   CleaningCrew = CleaningCrew,  
   TrammingCrew = TrammingCrew,  
   HlgeMaintainanceCrew = HlgeMaintainanceCrew,  
   RiggerCrew = RiggerCrew,  
   RseCleaningCrew = RseCleaningCrew,  
   PegNo PegNo,  
   PegValue = Convert(numeric(7,1), isnull(PegValue,0)),  
   PegToFace = Convert(numeric(7,1), isnull(PegToFace,0)),  
   ProgFrom = Convert(numeric(7,1), isnull(ProgFrom,0)),  
   ProgTo = Convert(numeric(7,1), isnull(ProgTo,0)),  
   Mainmetres = Convert(numeric(7,1), isnull(Mainmetres,0)),  
   MeasWidth = Convert(numeric(7,2), isnull(MeasWidth,0)),  
   MeasHeight = Convert(numeric(7,2), isnull(MeasHeight,0)),  
   PlanWidth = Convert(numeric(7,2), isnull(PlanWidth,0)),  
   PlanHeight = Convert(numeric(7,2), isnull(PlanHeight,0)),  
   Reefmetres = Convert(numeric(7,1), isnull(Reefmetres,0)),  
   Wastemetres = Convert(numeric(7,1), isnull(Wastemetres,0)),  
   Totalmetres = Convert(numeric(7,1), isnull(Totalmetres,0)),  
   Labour = Convert(numeric(7,0), isnull(Labour,0)),  
   PaidUnpaid = isnull(PaidUnpaid,'N'),  
   CW = Convert(numeric(7,0), isnull(CW,0)),  
   ValHeight = Convert(numeric(7,0), isnull(ValHeight,0)),  
   GT = Convert(numeric(7,2), isnull(GT,0)),  
   cmgt = Convert(numeric(7,0), isnull(cmgt,0)),  
   ExtraType = Convert(numeric(7,0), isnull(ExtraType,0)),  
   Cubicsmetres = Convert(numeric(7,1), isnull(Cubicsmetres,-1)),  
   CubicsReef = Convert(numeric(7,0), isnull(CubicsReef,0)),  
   CubicsWaste = Convert(numeric(7,0), isnull(CubicsWaste,0)),  
   OpenUpSqm = Convert(numeric(7,0), isnull(OpenUpSqm,0)),  
   ReefDevSqm = Convert(numeric(7,0), isnull(ReefDevSqm,0)),  
   OpenUpcmgt = Convert(numeric(7,0), isnull(OpenUpcmgt,0)),  
   ReefDevcmgt = Convert(numeric(7,0), isnull(ReefDevcmgt,0)),  
   OpenUpFL = Convert(numeric(7,0), isnull(OpenUpFL,0)),  
   ReefDevFL = Convert(numeric(7,0), isnull(ReefDevFL,0)),  
   OpenUpEquip = Convert(numeric(7,0), isnull(OpenUpEquip,0)),  
   ReefDevEquip = Convert(numeric(7,0), isnull(ReefDevEquip,0)),  
   TonsWasteBroken = Convert(numeric(7,2), isnull(TonsWasteBroken,0)),  
   TonsReefBroken = Convert(numeric(7,2), isnull(TonsReefBroken,0)),  
   TonsCubicsWaste = Convert(numeric(7,2), isnull(TonsCubicsWaste,0)),  
   TonsCubicsReef = Convert(numeric(7,2), isnull(TonsCubicsReef,0)),  
   TonsReef = Convert(numeric(7,2), isnull(TonsReef,0)),  
   TonsWaste = Convert(numeric(7,2), isnull(TonsWaste,0)),  
   TonsTotal = Convert(numeric(7,2), isnull(TonsTotal,0)),  
   TonsTrammed = Convert(numeric(7,2), isnull(TonsTrammed,0)),  
   TonsBallast = Convert(numeric(7,2), isnull(TonsBallast,0)),  
   Destination = Convert(numeric(7,0), isnull(Destination,-1)),  
   OreFlowID = OreFlowID,  
   Payment = Convert(numeric(7,0), isnull(Payment,-1)),  
   PlanNo = isnull(PlanNo,''),  
   CleanTypeID = Convert(numeric(7,0), isnull(Cleantype,-1)),  
   CleanSqm = Convert(numeric(7,0), isnull(CleanSqm,0)),  
   CleanDist = Convert(numeric(7,1), isnull(CleanDist,0)),  
   CleanVamp = Convert(numeric(7,1), isnull(CleanVamp,0)),  
   CleanTons = Convert(numeric(7,0), isnull(CleanTons,0)),  
   Cleangt = Convert(numeric(7,2), isnull(Cleangt,0)),  
   CleanContents = Convert(numeric(7,0), isnull(CleanContents,0))  
 from Survey  
 where ProdMonth = @ProdMonth and   
       SECTIONID = @SectionID and  
       WorkplaceID = @WorkplaceID and  
       Activity = 1  and  
       SeqNo = @SeqNo  
 order by SeqNo Desc  
 go

 

ALTER Procedure [dbo].[sp_Survey_STP_Load] 
--Declare 
    @ProdMonth varchar(6),
    @SectionID VarChar(20)
as

--set @Prodmonth = 201702
--set @SectionID = 'REAAHDA'

Select p.WorkplaceID WorkplaceID, w.Description as Workplace, 
   convert(numeric(7,0), p.Sqm) PlanSqm,  
   convert(numeric(7,0), pd.BookSqm) BookSqm,   
   convert(numeric(7,0), s.MeasSQM) MeasSqm,  
   convert(numeric(7,0), s.FL) FL,  
   convert(numeric(7,0), s.SW) SW, 
   convert(numeric(7,0), s.CW) CW,  
   convert(numeric(7,0), s.Grade) Cmgt,  
   convert(numeric(7,0), s.Tons) Tons,  
   convert(numeric(7,0), s.Grams) Content,  
   convert(numeric(7,0), s.Sweeps) Sweeps, 
   convert(numeric(7,0), s.Cubics) Cubics,  
   convert(numeric(7,0), s.OldTons) OldTons,  
   convert(numeric(7,0),  s.OldGrams) OldGrams  
from PlanMonth p 
inner join Workplace w on 
  p.WorkplaceID = w.WorkplaceID  
left outer join  
	(select s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity,
        MeasSQM = sum(MeasSQM),  
        FL = sum(FL),  
        SW = case when sum(MeasSQM) > 0 then sum(SQMSW) / sum(MeasSQM) else 0 end,  
        CW = case when sum(MeasSQM) > 0 then sum(SQMCW) / sum(MeasSQM) else 0 end,  
        Grade = case when sum(MeasSQM) > 0 then sum(Sqmcmgt) / sum(MeasSQM) else 0 end,  
        Tons = sum(Tons),  
        Grams = sum(Grams),  
        Sweeps = sum(Sweeps),  
        Cubics = sum(Cubics),  
        OldTons = sum(OldTons),  
        OldGrams = sum(OldGrams)  
	from 
		(select s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity,  
			MeasSQM = SqmTotal,  
			FL = FLTotal,  
			SQMSW = SqmTotal * SWSqm, 
			SQMCW = SqmTotal * CW, 
			Sqmcmgt = SqmTotal * cmgt,  
			Tons = TonsTotal,  
			Grams = TotalContent,  
			Sweeps = CleanSqm,  
			Cubics = CubicsReef + CubicsWaste, 
			OldTons =  case when s.Cleantype = 13 then s.CleanTons else 0 end,  
			OldGrams = case when s.Cleantype = 13 then s.CleanContents else 0 end 
		from Survey s 
		where s.Prodmonth = @ProdMonth and  
		   s.Activity IN(0,9) and  
		   s.SectionID = @SectionID
		) s  
	group by s.ProdMonth, s.WorkplaceID, s.SectionID, s.Activity 
	) s on  
	   p.ProdMonth = s.Prodmonth and  
	   p.SectionID = s.SectionID and  
	   p.WorkplaceID = s.WorkplaceID and
	   p.Activity = s.Activity 
	left outer join  
	 (select pd.ProdMonth, pd.WorkplaceID, pd.SectionID, pd.Activity, pd.PlanCode, pd.IsCubics, 
         BookSQM = sum(BookSqm), MeasSQM = SUM(SQM)  
	  from Planning pd  
	  where pd.Prodmonth = @ProdMonth and 
			pd.Activity IN(0,9) and 
			pd.SectionID = @SectionID 
	  group by pd.ProdMonth, pd.WorkplaceID, pd.SectionID, pd.Activity, pd.PlanCode, pd.IsCubics
	  ) pd on  
    p.ProdMonth = pd.Prodmonth and 
    p.SectionID = pd.SectionID and  
    p.WorkplaceID = pd.WorkplaceID and
	p.Activity = pd.Activity and
	p.PlanCode = pd.PlanCode and
	p.IsCubics = pd.IsCubics
where p.SectionID = @SectionID and   
      p.activity IN(0,9) and  
      p.Prodmonth = @ProdMonth and
	  p.PlanCode = 'MP' and
	  p.IsCubics = 'N'
go



ALTER Procedure [dbo].[sp_Survey_STP_Load_Detail] 
--Declare 
    @ProdMonth varchar(6),
    @SectionID VarChar(20),
	@WorkplaceID VarChar(12),
	@SeqNo VarChar(7)
as

--set @Prodmonth = 201703
--set @SectionID = 'REAAHDA'
--set @WorkplaceID = 'RE007667'
--set @SeqNo = '1'

select   
	CalendarDate,   
	Dip = Convert(numeric(7,0), isnull(Dip,0)),   
	MineMethod = Convert(numeric(7,0), isnull(MineMethod,-1)),   
	Density = case when isnull(Density,0) = 0 then '-1' else 'Insitu: '+convert(varchar(10),Convert(numeric(7,2), isnull(Density,0))) end,    
	StopeTypeID = Convert(numeric(7,0), isnull(SType,-1)),   
	CrewMorning = CrewMorning,   
	CrewAfternoon = CrewAfternoon,   
	CrewEvening =  CrewEvening,   
	CleaningCrew = CleaningCrew,   
	TrammingCrew = TrammingCrew,   
	HlgeMaintainanceCrew = HlgeMaintainanceCrew,   
	RiggerCrew = RiggerCrew,   
	RseCleaningCrew = RseCleaningCrew,   
	Reefmetres = Convert(numeric(7,1), isnull(Reefmetres,0)),   
	Wastemetres = Convert(numeric(7,1), isnull(Wastemetres,0)),   
	MeasWidth = Convert(numeric(7,2), isnull(MeasWidth,0)),   
	MeasHeight = Convert(numeric(7,2), isnull(MeasHeight,0)),   
	PlanWidth = Convert(numeric(7,2), isnull(PlanWidth,0)),   
	PlanHeight = Convert(numeric(7,2), isnull(PlanHeight,0)),   
	LockUpTons = Convert(numeric(7,0), isnull(LockUpTons,0)),   
	Blockno = isnull(Blockno,''),   
	BlockWidth = Convert(numeric(7,0), isnull(BlockWidth,0)),   
	BlockValue = Convert(numeric(7,0), isnull(BlockValue,0)),   
	StopeSqm = Convert(numeric(7,0), isnull(StopeSqm,0)),   
	StopeSqmOSF = Convert(numeric(7,0), isnull(StopeSqmOSF,0)),   
	StopeSqmOS = Convert(numeric(7,0), isnull(StopeSqmOS,0)),   
	StopeSqmTotal = Convert(numeric(7,0), isnull(StopeSqmTotal,0)),   
	LedgeSqm  = Convert(numeric(7,0), isnull(LedgeSqm,0)),   
	LedgeSqmOSF = Convert(numeric(7,0), isnull(LedgeSqmOSF,0)),   
	LedgeSqmOS  = Convert(numeric(7,0), isnull(LedgeSqmOS,0)),   
	LedgeSqmTotal = Convert(numeric(7,0), isnull(LedgeSqmTotal,0)),   
	StopeFL = Convert(numeric(7,0), isnull(StopeFL,0)),   
	StopeFLOS = Convert(numeric(7,0), isnull(StopeFLOS,0)),   
	LedgeFL = Convert(numeric(7,0), isnull(LedgeFL,0)),   
	LedgeFLOS = Convert(numeric(7,0), isnull(LedgeFLOS,0)),   
	FLOSTotal = Convert(numeric(7,0), isnull(FLOSTotal,0)),   
	MeasAdvSW = Convert(numeric(7,1), isnull(MeasAdvSW,0)),   
	SWIdeal = Convert(numeric(7,0), isnull(SWIdeal,0)),  
	FLTotal = Convert(numeric(7,0), isnull(FLTotal,0)),   
	SqmConvTotal = Convert(numeric(7,0), isnull(SqmConvTotal,0)),   
	SqmOSFTotal = Convert(numeric(7,0), isnull(SqmOSFTotal,0)),   
	SqmOSTotal = Convert(numeric(7,0), isnull(SqmOSTotal,0)),   
	SqmTotal = Convert(numeric(7,0), isnull(SqmTotal,0)),   
	ExtraType = Convert(numeric(7,0), isnull(ExtraType,-1)),   
	Cubicsmetres = Convert(numeric(7,1), isnull(Cubicsmetres,0)),   
	CubicsReef = Convert(numeric(7,0), isnull(CubicsReef,0)),   
	CubicsWaste = Convert(numeric(7,0), isnull(CubicsWaste,0)),   
	Labour = Convert(numeric(7,0), isnull(Labour,0)),   
	PaidUnpaid = isnull(PaidUnpaid,'N'),   
	FW = Convert(numeric(7,0), isnull(FW,0)),   
	CW = Convert(numeric(7,0), isnull(CW,0)),   
	HW = Convert(numeric(7,0), isnull(HW,0)),   
	SWSqm = Convert(numeric(7,0), isnull(SWSqm,0)),   
	SWOSF = Convert(numeric(7,0), isnull(SWOSF,0)),   
	SWOS = Convert(numeric(7,0), isnull(SWOS,0)),   
	cmgt = Convert(numeric(7,0), isnull(cmgt,0)),   
	Destination = Convert(numeric(7,0), isnull(Destination,-1)),   
	OreFlowID = OreFlowID,   
	CleanTypeID = Convert(numeric(7,0), isnull(Cleantype,-1)),   
	CleanSqm = Convert(numeric(7,0), isnull(CleanSqm,0)),   
	CleanDist = Convert(numeric(7,1), isnull(CleanDist,0)),   
	CleanVamp = Convert(numeric(7,1), isnull(CleanVamp,0)),   
	CleanTons = Convert(numeric(7,0), isnull(CleanTons,0)),   
	Cleangt = Convert(numeric(7,2), isnull(Cleangt,0)),   
	CleanContents = Convert(numeric(7,0), isnull(CleanContents,0)),   
	TonsReef = Convert(numeric(7,2), isnull(TonsReef,0)),   
	TonsWaste = Convert(numeric(7,2), isnull(TonsWaste,0)),   
	TonsOSF = Convert(numeric(7,2), isnull(TonsOSF,0)),   
	TonsOS = Convert(numeric(7,2), isnull(TonsOS,0)),   
	TonsTotal = Convert(numeric(7,2), isnull(TonsTotal,0)),   
	TotalContent = Convert(numeric(13,0), isnull(TotalContent,0)),   
	CleanDepth = Convert(numeric(13,0), isnull(CleanDepth,0)),   
	Payment = Convert(numeric(7,0), isnull(Payment,-1)),   
	PlanNo = isnull(PlanNo,'') 
from Survey 
where ProdMonth = @ProdMonth and        
	  SECTIONID = @SectionID and       
	  WorkplaceID = @WorkplaceID and       
	  Activity IN(0,9)  and       
	  SeqNo = @SeqNo 
order by SeqNo Desc 

go


create 
procedure [dbo].[sp_SICReport_Tramming_Zeroes] 
as
select '' TheSection1, '' TheSection,
	 Dev_Count = Convert(decimal(13,2),0),
	 Stp_Count = Convert(decimal(13,2),0),
	 TheActivity = '',
	 DayP_Broken = Convert(decimal(13,2),0),
	 DayB_Broken = Convert(decimal(13,2),0),
	 DayV_Broken = Convert(decimal(13,2),0),
	 ProgP_Broken = Convert(decimal(13,2),0),	
	 ProgF_Broken = Convert(decimal(13,2),0),
	 ProgB_Broken = Convert(decimal(13,2),0),	
	 ProgV_Broken = Convert(decimal(13,2),0),
	 DayP_Tot_Tons = Convert(decimal(13,2),0),
	 DayB_Tot_Tons = Convert(decimal(13,2),0),				  
	 ProgP_Tot_Tons = Convert(decimal(13,2),0),
	 ProgF_Tot_Tons = Convert(decimal(13,2),0),
	 ProgB_Tot_Tons = Convert(decimal(13,2),0),		
	 MonthPlan_Stp_Tons = Convert(decimal(13,2),0),
	 DayP_Stp_Tons = Convert(decimal(13,2),0),
	 DayB_Stp_Tons = Convert(decimal(13,2),0),				  
	 ProgP_Stp_Tons = Convert(decimal(13,2),0),
	 ProgF_Stp_Tons = Convert(decimal(13,2),0),
	 ProgB_Stp_Tons = Convert(decimal(13,2),0),					  					  						  					  
	 MonthPlan_Dev_ReefTons = Convert(decimal(13,2),0),					  
	 DayP_Dev_ReefTons = Convert(decimal(13,2),0),
	 DayB_Dev_ReefTons = Convert(decimal(13,2),0),				   
	 ProgP_Dev_ReefTons = Convert(decimal(13,2),0),
	 ProgB_Dev_ReefTons = Convert(decimal(13,2),0),				  
	 MonthPlan_Dev_WasteTons = Convert(decimal(13,2),0),				  						  
	 DayP_Dev_WasteTons = Convert(decimal(13,2),0),
	 DayB_Dev_WasteTons = Convert(decimal(13,2),0),				  
	 ProgP_Dev_WasteTons = Convert(decimal(13,2),0),
	 ProgB_Dev_WasteTons = Convert(decimal(13,2),0),						  
	 MonthPlan_Dev_TotalTons = Convert(decimal(13,2),0),						  					  
	 DayP_Dev_TotalTons = Convert(decimal(13,2),0),
	 DayB_Dev_TotalTons = Convert(decimal(13,2),0),				  					  					  
	 ProgP_Dev_TotalTons = Convert(decimal(13,2),0),
	 ProgB_Dev_TotalTons = Convert(decimal(13,2),0),
	 DayB_Stp_TopTons = Convert(decimal(13,2),0),
	 ProgB_Stp_TopTons = Convert(decimal(13,2),0),
	 MonthPlan_Stp_TopTons = Convert(decimal(13,2),0),
	 DayP_Stp_TopTons = Convert(decimal(13,2),0),
	 ProgP_Stp_TopTons = Convert(decimal(13,2),0),
	 DayV_Stp_TopTons = Convert(decimal(13,2),0),					  						  
	 ProgV_Stp_TopTons = Convert(decimal(13,2),0),
	 DayV_Stp_Tons = Convert(decimal(13,2),0),
	 ProgV_Stp_Tons = Convert(decimal(13,2),0),
	 ProgF_Stp_Tons = Convert(decimal(13,2),0),
	 DayV_Tot_Tons = Convert(decimal(13,2),0),
	 ProgV_Tot_Tons = Convert(decimal(13,2),0),
	 ProgF_Tot_Tons = Convert(decimal(13,2),0),
	 DayV_Dev_ReefTons = Convert(decimal(13,2),0),
	 ProgV_Dev_ReefTons = Convert(decimal(13,2),0),
	 DayV_Dev_WasteTons = Convert(decimal(13,2),0),
	 ProgV_Dev_WasteTons = Convert(decimal(13,2),0),					   
	 DayV_Dev_TotalTons = Convert(decimal(13,2),0),					  						  
	 ProgV_Dev_TotalTons = Convert(decimal(13,2),0)


go



create procedure [dbo].[sp_SICReport_Tramming] --'MINEWARE', '2013/12/02', '1', '1' 
--declare
@UserID varchar(20),
@CalendarDate varchar(10),
@SectionID varchar(20),
@Section int,
@MOName varchar(50)
as

--set @UserID = 'MINEWARE'
--set @CalendarDate = '2017-03-01'
--set @SectionID = 'B'
--set @Section = 4
--set @MOName = 'PietEngelbrecht'

declare @TheQuery varchar(8000)
declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)

declare @TheStartDate varchar(10),
@TheEndDate varchar(10),
@TheMonth varchar(10)

SET NOCOUNT ON;

--IF (@Section = 1) or (@Section = 2) or (@Section = 3)
--BEGIN
	select @TheStartDate = convert(varchar(10),a.StartDate,120),
		   @TheEndDate = convert(varchar(10),a.EndDate,120),	
		   @TheMonth = a.MillMonth
	from
		(select min(cm.StartDate) StartDate, max(cm.EndDate) EndDate, cm.MillMonth  
			  from CODE_CALENDAR cc
			  inner join CALENDARMILL cm on
				cm.CalendarCode = cc.CalendarCode
			  where cm.StartDate <= @CalendarDate and 
			  cm.EndDate >= @CalendarDate and
			  cc.Description = 'Mill Calendar' 
			  group by cm.MillMonth
		) a
--END
--ELSE
--BEGIN
--	select @TheStartDate = convert(varchar(10),a.StartDate,120),
--		   @TheEndDate = convert(varchar(10),a.EndDate,120),
--		   @TheMonth = a.Prodmonth
--	from
--		(select distinct(s.BeginDate) StartDate, s.EndDate EndDate, s.Prodmonth
--			from seccal s 
--			inner join SectionComplete sc on
--			  sc.ProdMonth = s.ProdMonth and
--			  sc.SBID = s.SectionID
--			where s.BeginDate <= @CalendarDate and
--				  s.EndDate >= @CalendarDate and
--				  sc.MOID = @SectionID
--		) a
--END  

if (@TheStartDate is NULL)
BEGIN
  select '' TheSection1, '' TheSection,
	 Dev_Count = Convert(decimal(13,2),0),
	 Stp_Count = Convert(decimal(13,2),0),
	 TheActivity = '',
	 DayP_Broken = Convert(decimal(13,2),0),
	 DayB_Broken = Convert(decimal(13,2),0),
	 DayV_Broken = Convert(decimal(13,2),0),
	 ProgP_Broken = Convert(decimal(13,2),0),	
	 ProgF_Broken = Convert(decimal(13,2),0),
	 ProgB_Broken = Convert(decimal(13,2),0),	
	 ProgV_Broken = Convert(decimal(13,2),0),
	 DayP_Tot_Tons = Convert(decimal(13,2),0),
	 DayB_Tot_Tons = Convert(decimal(13,2),0),				  
	 ProgP_Tot_Tons = Convert(decimal(13,2),0),
	 ProgF_Tot_Tons = Convert(decimal(13,2),0),
	 ProgB_Tot_Tons = Convert(decimal(13,2),0),		
	 MonthPlan_Stp_Tons = Convert(decimal(13,2),0),
	 DayP_Stp_Tons = Convert(decimal(13,2),0),
	 DayB_Stp_Tons = Convert(decimal(13,2),0),				  
	 ProgP_Stp_Tons = Convert(decimal(13,2),0),
	 ProgF_Stp_Tons = Convert(decimal(13,2),0),
	 ProgB_Stp_Tons = Convert(decimal(13,2),0),					  					  						  					  
	 MonthPlan_Dev_ReefTons = Convert(decimal(13,2),0),					  
	 DayP_Dev_ReefTons = Convert(decimal(13,2),0),
	 DayB_Dev_ReefTons = Convert(decimal(13,2),0),				   
	 ProgP_Dev_ReefTons = Convert(decimal(13,2),0),
	 ProgB_Dev_ReefTons = Convert(decimal(13,2),0),				  
	 MonthPlan_Dev_WasteTons = Convert(decimal(13,2),0),				  						  
	 DayP_Dev_WasteTons = Convert(decimal(13,2),0),
	 DayB_Dev_WasteTons = Convert(decimal(13,2),0),				  
	 ProgP_Dev_WasteTons = Convert(decimal(13,2),0),
	 ProgB_Dev_WasteTons = Convert(decimal(13,2),0),						  
	 MonthPlan_Dev_TotalTons = Convert(decimal(13,2),0),						  					  
	 DayP_Dev_TotalTons = Convert(decimal(13,2),0),
	 DayB_Dev_TotalTons = Convert(decimal(13,2),0),				  					  					  
	 ProgP_Dev_TotalTons = Convert(decimal(13,2),0),
	 ProgB_Dev_TotalTons = Convert(decimal(13,2),0),
	 DayB_Stp_TopTons = Convert(decimal(13,2),0),
	 ProgB_Stp_TopTons = Convert(decimal(13,2),0),
	 MonthPlan_Stp_TopTons = Convert(decimal(13,2),0),
	 DayP_Stp_TopTons = Convert(decimal(13,2),0),
	 ProgP_Stp_TopTons = Convert(decimal(13,2),0),
	 DayV_Stp_TopTons = Convert(decimal(13,2),0),					  						  
	 ProgV_Stp_TopTons = Convert(decimal(13,2),0),
	 DayV_Stp_Tons = Convert(decimal(13,2),0),
	 ProgV_Stp_Tons = Convert(decimal(13,2),0),
	 ProgF_Stp_Tons = Convert(decimal(13,2),0),
	 DayV_Tot_Tons = Convert(decimal(13,2),0),
	 ProgV_Tot_Tons = Convert(decimal(13,2),0),
	 ProgF_Tot_Tons = Convert(decimal(13,2),0),
	 DayV_Dev_ReefTons = Convert(decimal(13,2),0),
	 ProgV_Dev_ReefTons = Convert(decimal(13,2),0),
	 DayV_Dev_WasteTons = Convert(decimal(13,2),0),
	 ProgV_Dev_WasteTons = Convert(decimal(13,2),0),					   
	 DayV_Dev_TotalTons = Convert(decimal(13,2),0),					  						  
	 ProgV_Dev_TotalTons = Convert(decimal(13,2),0)
END
ELSE
BEGIN
	set @TheQuery = '
select isnull(TheSection,'''') TheSection1, isnull(TheSection,'''') TheSection, 
 TheActivity = isnull(TheActivity,''''),
 Dev_Count = Convert(decimal(13,2),sum(Dev_Count)),
 Stp_Count = Convert(decimal(13,2),sum(Stp_Count)),
 DayP_Broken = Convert(decimal(13,2),sum(DayP_Broken)),
 DayB_Broken = Convert(decimal(13,2),sum(DayB_Broken)),
 DayV_Broken = Convert(decimal(13,2),sum(DayB_Broken) - sum(DayP_Broken)),
 ProgP_Broken = Convert(decimal(13,2),sum(ProgP_Broken)),		
 ProgB_Broken = Convert(decimal(13,2),sum(ProgB_Broken)),	
 ProgF_Broken = Convert(decimal(13,2),sum(ProgB_Broken) + sum(ProgF_Broken)),	
 ProgV_Broken = Convert(decimal(13,2),sum(ProgB_Broken) - sum(ProgP_Broken)),	
 DayP_Tot_Tons = Convert(decimal(13,2),isnull(sum(DayP_Tot_Tons),0)),
 DayB_Tot_Tons = Convert(decimal(13,2),isnull(sum(DayB_Tot_Tons),0)),				  
 ProgP_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgP_Tot_Tons),0)),
 ProgF_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgF_Tot_Tons),0)),
 ProgB_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgB_Tot_Tons),0)),		
 MonthPlan_Stp_Tons = Convert(decimal(13,2),sum(MonthPlan_Stp_Tons)),
 DayP_Stp_Tons = Convert(decimal(13,2),sum(DayP_Stp_Tons)),
 DayB_Stp_Tons = Convert(decimal(13,2),sum(DayB_Stp_Tons)),				  
 ProgP_Stp_Tons = Convert(decimal(13,2),sum(ProgP_Stp_Tons)),
 ProgF_Stp_Tons = Convert(decimal(13,2),sum(ProgF_Stp_Tons)),
 ProgB_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons)),					  						  						  					  
 MonthPlan_Dev_ReefTons = Convert(decimal(13,2),sum(MonthPlan_Dev_ReefTons)),					  
 DayP_Dev_ReefTons = Convert(decimal(13,2),sum(DayP_Dev_ReefTons)),
 DayB_Dev_ReefTons = Convert(decimal(13,2),sum(DayB_Dev_ReefTons)),				   
 ProgP_Dev_ReefTons = Convert(decimal(13,2),sum(ProgP_Dev_ReefTons)),
 ProgB_Dev_ReefTons = Convert(decimal(13,2),sum(ProgB_Dev_ReefTons)),				  
 MonthPlan_Dev_WasteTons = Convert(decimal(13,2),sum(MonthPlan_Dev_WasteTons)),				  						  
 DayP_Dev_WasteTons = Convert(decimal(13,2),sum(DayP_Dev_WasteTons)),
 DayB_Dev_WasteTons = Convert(decimal(13,2),sum(DayB_Dev_WasteTons)),				  
 ProgP_Dev_WasteTons = Convert(decimal(13,2),sum(ProgP_Dev_WasteTons)),
 ProgB_Dev_WasteTons = Convert(decimal(13,2),sum(ProgB_Dev_WasteTons)),						  
 MonthPlan_Dev_TotalTons = Convert(decimal(13,2),sum(MonthPlan_Dev_TotalTons)),						  					  
 DayP_Dev_TotalTons = Convert(decimal(13,2),sum(DayP_Dev_TotalTons)),
 DayB_Dev_TotalTons = Convert(decimal(13,2),sum(DayB_Dev_TotalTons)),				  					  					  
 ProgP_Dev_TotalTons = Convert(decimal(13,2),sum(ProgP_Dev_TotalTons)),
 ProgB_Dev_TotalTons = Convert(decimal(13,2),sum(ProgB_Dev_TotalTons)),
 DayB_Stp_TopTons = Convert(decimal(13,2),sum(DayB_Stp_TopTons)),
 ProgB_Stp_TopTons = Convert(decimal(13,2),sum(ProgB_Stp_TopTons)),
 MonthPlan_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then MonthPlan_Stp_TopTons1 else MonthPlan_Stp_TopTons end)),	
 DayP_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then DayP_Stp_TopTons1 else DayP_Stp_TopTons end)),
 ProgP_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then ProgP_Stp_TopTons1 else ProgP_Stp_TopTons end )),
 DayV_Stp_TopTons = Convert(decimal(13,2),sum(DayB_Stp_TopTons) - sum(case when TopPanelsMO <> 0 then DayP_Stp_TopTons1 else DayP_Stp_TopTons end)),					  						  
 ProgV_Stp_TopTons = Convert(decimal(13,2),sum(ProgB_Stp_TopTons) - sum(ProgP_Stp_TopTons)),
 DayV_Stp_Tons = Convert(decimal(13,2),sum(DayB_Stp_Tons) - sum(DayP_Stp_Tons)),
 ProgV_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons) - sum(ProgP_Stp_Tons)),
 ProgF_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons) + sum(ProgF_Stp_Tons)),
 DayV_Tot_Tons = Convert(decimal(13,2),sum(DayB_Tot_Tons) - sum(DayP_Tot_Tons)),
 ProgV_Tot_Tons = Convert(decimal(13,2),sum(ProgB_Tot_Tons) - sum(ProgP_Tot_Tons)),
 ProgF_Tot_Tons = Convert(decimal(13,2),sum(ProgB_Tot_Tons) + sum(ProgF_Tot_Tons)),
 DayV_Dev_ReefTons = Convert(decimal(13,2),sum(DayB_Dev_ReefTons) - sum(DayP_Dev_ReefTons)),
 ProgV_Dev_ReefTons = Convert(decimal(13,2),sum(ProgB_Dev_ReefTons) - sum(ProgP_Dev_ReefTons)),
 DayV_Dev_WasteTons = Convert(decimal(13,2),sum(DayB_Dev_WasteTons) - sum(DayP_Dev_WasteTons)),
 ProgV_Dev_WasteTons = Convert(decimal(13,2),sum(ProgB_Dev_WasteTons) - sum(ProgP_Dev_WasteTons)),					   
 DayV_Dev_TotalTons = Convert(decimal(13,2),sum(DayB_Dev_TotalTons) - sum(DayP_Dev_TotalTons)),					  						  
 ProgV_Dev_TotalTons = Convert(decimal(13,2),sum(ProgB_Dev_TotalTons) -	sum(ProgP_Dev_TotalTons))
from
(
(
  select  '
IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery = @TheQuery + ' case when t.Sectionid = ''Unidentified'' then t.SectionID else isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') end TheSection1, 
		case when t.Sectionid = ''Unidentified'' then t.SectionID else isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') end TheSection,  '
END
IF (@Section = 4)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END  
set @TheQuery = @TheQuery + ' Activity, TheActivity = case when t.Activity IN (0,9) then ''Stp'' else ''Dev'' end,
   DayP_Broken = 0,	
   DayB_Broken = 0,	
   ProgP_Broken = 0,		
   ProgF_Broken = 0,
   ProgB_Broken = 0,		
   DayP_Tot_Tons = 0,	
   DayB_Tot_Tons = sum(case when (t.CalendarDate = '''+@CalendarDate+''')   
						then (t.DTons + t.ATons + t.NTons)  else 0 end),					     		  
   ProgP_Tot_Tons = Convert(decimal(13,2),0),
   ProgF_Tot_Tons = Convert(decimal(13,2),0),
   ProgB_Tot_Tons = sum(t.DTons + t.ATons + t.NTons),		
   DayB_Stp_Tons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Stp_Tons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then (t.DTons + t.ATons + t.NTons) else 0 end), 
   DayB_Dev_ReefTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.ReefWaste = ''R'') and
							(t.Activity = 1) 
					    then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Dev_WasteTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.ReefWaste = ''W'') and
							(t.Activity = 1)  
  					   then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Dev_TotalTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity = 1)  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_ReefTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.ReefWaste = ''R'') and
							(t.Activity = 1)   
  					   then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_WasteTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.ReefWaste = ''W'') and
							(t.Activity = 1)   
  						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_TotalTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity = 1)  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Stp_TopTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then t.TopTons else 0 end),
   ProgB_Stp_TopTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then t.TopTons else 0 end),		 						
   Dev_Count = sum(case when (t.Activity = 1) then 1 else 0 end), 	
   Stp_Count = sum(case when (t.Activity IN (0,9)) then 1 else 0 end),
   TopPanelsMO = 0,
   TopPanelsMAN = 0,
   MonthPlan_Stp_Tons = 0,	
   DayP_Stp_Tons = 0,						  
   ProgP_Stp_Tons = 0,				  					  						  					  
   ProgF_Stp_Tons = 0,
   MonthPlan_Dev_ReefTons = 0,					  
   DayP_Dev_ReefTons = 0,				   
   ProgP_Dev_ReefTons = 0,						  
   MonthPlan_Dev_WasteTons = 0,					  						  
   DayP_Dev_WasteTons = 0,							  
   ProgP_Dev_WasteTons = 0,							  
   MonthPlan_Dev_TotalTons = 0,				  					  
   DayP_Dev_TotalTons = 0,ProgP_Dev_TotalTons = 0,MonthPlan_Stp_TopTons = 0,
   MonthPlan_Stp_TopTons1 = 0,DayP_Stp_TopTons = 0, 
   ProgP_Stp_TopTons = 0,DayP_Stp_TopTons1 = 0, ProgP_Stp_TopTons1 = 0  									
 from BookingTramming t
 left outer join vw_Sections_Complete_MO sc on
   sc.ProdMonth = t.millMonth and
   sc.SectionID_2 = t.SectionID  
 where t.CalendarDate >= '''+@TheStartDate+''' and
	   t.CalendarDate <= '''+@CalendarDate+''' '
set @TheQuery2 = ''
IF (@Section = 4)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_2 = '''+@SectionID+''' and sc.Name_2 = '''+@MOName+''' '

END 
IF (@Section = 3)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_3 = '''+@SectionID+''' '

END
IF (@Section = 2)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_4 = '''+@SectionID+''' '

END
IF (@Section = 1)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_5 = '''+@SectionID+''' '

END
set @TheQuery1 = ' group by '
IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' t.SectionID, sc.SectionID_2, sc.Name_2, '
END
IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' sc.SectionID_2, sc.Name_2, '
END
set @TheQuery1 = @TheQuery1 + 'Activity '
  set @TheQuery1 = @TheQuery1 + '
 )union
(select '
--IF (@Section = 1)
--BEGIN
--   set @TheQuery1 = @TheQuery1 + ' isnull(sc.MOID +'':''+sc.MOName,'''') TheSection1, 
--								   isnull(sc.MOID +'':''+sc.MOName,'''') TheSection, '
--END
--IF (@Section = 4)
--BEGIN
--   set @TheQuery1 = @TheQuery1 + ' isnull(sc.MOID +'':''+sc.MOName,'''') TheSection1, 
--								   isnull(sc.MOID +'':''+sc.MOName,'''') TheSection, '
--END  
set @TheQuery1 = @TheQuery1 + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								   isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
set @TheQuery1 = @TheQuery1 + ' p.Activity, TheActivity=  case when p.Activity IN (0,9) then ''Stp'' else ''Dev'' end,
 DayP_Broken = sum(case when (p.CalendarDate = '''+@CalendarDate+''')   and (p.Activity IN (0,9)) 
						  then (p.BookTons) else 0 end),
 DayB_Broken = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.booktons)
						  else 0 end),
 ProgP_Broken = sum(case when (p.CalendarDate <= '''+@CalendarDate+''')  and (p.Activity IN (0,9)) 
						  then (p.BookTons) else 0 end),
 ProgF_Broken = sum(case when (p.CalendarDate > '''+@CalendarDate+''')  and (p.Activity IN (0,9)) 
						  then (p.Tons) else 0 end),
 ProgB_Broken = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.Booktons)
						  else 0 end),		
 DayP_Tot_Tons = sum(case when (p.CalendarDate = '''+@CalendarDate+''')  
						  then (p.BookTons) else 0 end),	
 DayB_Tot_Tons = Convert(decimal(13,5),0),				  
 ProgP_Tot_Tons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''')
						  then (p.BookTons)else 0 end),
 ProgF_Tot_Tons = sum(case when (p.CalendarDate > '''+@CalendarDate+''')
						  then (p.Tons)else 0 end),
 ProgB_Tot_Tons = 0,	
 DayB_Stp_Tons = 0,	
 ProgB_Stp_Tons = 0,	
 DayB_Dev_ReefTons = 0,	
 DayB_Dev_WasteTons = 0,	
 DayB_Dev_TotalTons = 0,	
 ProgB_Dev_ReefTons = 0,	
 ProgB_Dev_WasteTons = 0,	
 ProgB_Dev_TotalTons = 0,	
 DayB_Stp_TopTons = 0,	
 ProgB_Stp_TopTons = 0,				
 Dev_Count = sum(case when (p.Activity = 1) then 1 else 0 end),
 Stp_Count = sum(case when (p.Activity IN (0,9)) then 1 else 0 end),
 ToppanelsMO = sum(case when tp1.workplaceid is not null then 1 else 0 end),
 ToppanelsMAN = sum(case when tp.workplaceid is not null then 1 else 0 end),
 MonthPlan_Stp_Tons =sum(case when (p.Activity IN (0,9)) 
						  then p.Tons
						  else 0 end),  							  							  
 DayP_Stp_Tons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.BookTons)
						  else 0 end),						  
 ProgP_Stp_Tons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.BookTons)
						  else 0 end),
 ProgF_Stp_Tons = sum(case when (p.CalendarDate > '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.Tons)
						  else 0 end),					  								  						  					  
 MonthPlan_Dev_ReefTons = sum(case when (p.Activity = 1)  
						  then p.ReefTons
						  else 0 end),						  
 DayP_Dev_ReefTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.BookReefTons 
						  else 0 end),					   
 ProgP_Dev_ReefTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1)   
						  then p.bookReeftons
						  else 0 end),						  
 MonthPlan_Dev_WasteTons = sum(case when (p.Activity = 1)  
						  then p.WasteTons
						  else 0 end),						  						  
 DayP_Dev_WasteTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1)   
						  then p.bookWastetons
						  else 0 end),						  
 ProgP_Dev_WasteTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.bookWastetons
						  else 0 end),								  
 MonthPlan_Dev_TotalTons = sum(case when (p.Activity = 1) 
						  then p.tons
						  else 0 end),		
				  					  
 DayP_Dev_TotalTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.booktons
						  else 0 end) ,					  					  					  
 ProgP_Dev_TotalTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.booktons
						  else 0 end),
 MonthPlan_Stp_TopTons = sum(case when (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null)
						  then p.tons
						  else 0 end), 
 MonthPlan_Stp_TopTons1 = sum(case when (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null)
						  then p.tons
						  else 0 end),
 DayP_Stp_TopTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null)
						  then p.booktons
						  else 0 end), 
 ProgP_Stp_TopTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null) 
						  then p.booktons
						   else 0 end),
 DayP_Stp_TopTons1 = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null)
						  then p.booktons
						  else 0 end), 
 ProgP_Stp_TopTons1 = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null) 
						  then p.booktons
						   else 0 end)   						  						  						  
from Planning  p 
inner join PLANMONTH pp on
  p.Prodmonth = pp.Prodmonth and
  p.SectionID = pp.Sectionid and
  p.WorkplaceID = pp.Workplaceid and
  p.Activity = pp.Activity
inner join Section_Complete sc on
  sc.Prodmonth = p.Prodmonth and
  sc.SectionID = p.SectionID    
inner join WORKPLACE w on
  w.WorkplaceID = p.WorkplaceID '
 IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + '
left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_5 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity 
  left outer join TopPanelsSelected tp1 on
  tp1.Prodmonth = p.Prodmonth and
  tp1.SectionID = sc.SectionID_2 and
  tp1.WorkplaceID = p.WorkplaceID and
  tp1.Activity = p.Activity  '
 END 
  IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + '
left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_5 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity 
  left outer join TopPanelsSelected tp1 on
  tp1.Prodmonth = p.Prodmonth and
  tp1.SectionID = sc.SectionID_2 and
  tp1.WorkplaceID = p.WorkplaceID and
  tp1.Activity = p.Activity'
 END
 set @TheQuery1 = @TheQuery1 + ' 
 
, SYSSET sl 
 where p.CalendarDate >= '''+@TheStartDate+''' and
       p.CalendarDate <= '''+@TheEndDate+''' '
IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_2 = '''+@SectionID +'''  and sc.Name_2 = '''+@MOName+''' '
END   
IF (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_3 = '''+@SectionID +''' '
END 
IF (@Section = 2)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_4 = '''+@SectionID +''' '
END 
IF (@Section = 1)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_5 = '''+@SectionID +''' '
END   

set @TheQuery1 = @TheQuery1 + 'group by sc.SectionID_2, sc.Name_2, p.Activity ' 
set @TheQuery1 = @TheQuery1 + ' 
 )) a
group by a.TheActivity, a.TheSection
with Rollup '


--print @TheQuery
--print @TheQuery2
--print @TheQuery1
exec (@TheQuery+@TheQuery2+@TheQuery1)

end


go


create 
procedure [dbo].[sp_SICReport_Total_Zeroes]
as 

Select '' TheSection1, '' TheSection, 
'' workplaceid,
Dev_Check = Convert(decimal(10,1), 0),
Stp_Check = Convert(decimal(10,1), 0),
--Nr of Panels   
[Stp_PD_NoPanels] = Convert(Numeric(10,0), 0),  
[Stp_BD_NoPanels] = Convert(Numeric(10,0), 0),
[Stp_PP_NoPanels] = Convert(Numeric(10,0), 0),
[Stp_BP_NoPanels] = Convert(Numeric(10,0), 0),
--Nr of Dev Panels 
[Dev_PD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_BD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_VD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_PP_NoPanels] = Convert(Numeric(10,0), 0),  
[Dev_BP_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_VP_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_FP_NoPanels] = Convert(Numeric(10,0), 0),
--No of Cleaned Stope Panels
[Stp_BD_ClnPanels] = Convert(Numeric(10,0), 0),
--No of Cleaned Dev Panels
[Dev_PD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_BD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_VD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_PP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_BP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_VP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_FP_ClnPanels] = Convert(Numeric(10,0), 0),
--Shiftnr   
TheField1 = '', 
ShiftNo =  0, 
TotalShft = Convert(Numeric(10,0), 0),
--M2 Mined
Stp_Plan_Sqm = Convert(Numeric(10,0), 0),
Stp_PD_Sqm = Convert(Numeric(10,0), 0),
Stp_BD_Sqm = Convert(Numeric(10,0), 0),
Stp_VD_Sqm = Convert(Numeric(10,0), 0),
Stp_PP_Sqm = Convert(Numeric(10,0), 0),
Stp_BP_Sqm = Convert(Numeric(10,0), 0),  
Stp_VP_Sqm = Convert(Numeric(10,0), 0),
Stp_FP_Sqm = Convert(Numeric(10,0), 0),
Stp_NewDay_Sqm = Convert(Numeric(10,0), 0),
Stp_Prev_Sqm = Convert(Numeric(10,0), 0),
Stp_PrevVar_Sqm = Convert(Numeric(10,0), 0),
Stp_DPerc_Sqm = Convert(Numeric(10,2), 0),
Stp_PPerc_Sqm = Convert(Numeric(10,2), 0),
Stp_FPSqm = Convert(Numeric(10,0), 0),	                                                                                                                         
--High Grade m2 Mined  
Stp_PD_HgSqm = Convert(Numeric(10,0), 0),
Stp_BD_HgSqm = Convert(Numeric(10,0), 0),
Stp_VD_HgSqm = Convert(Numeric(10,0), 0),
Stp_PP_HgSqm = Convert(Numeric(10,0), 0),
Stp_BP_HgSqm = Convert(Numeric(10,0), 0),  
Stp_VP_HgSqm = Convert(Numeric(10,0), 0),
Stp_FP_HgSqm = Convert(Numeric(10,0), 0),
Stp_NewDay_HgSqm = Convert(Numeric(10,0), 0),     
--Total Dev m2 Mined  
[Dev_PD_Sqm] = Convert(Numeric(10,0), 0),
[Dev_BD_Sqm] = Convert(Numeric(10,0), 0),  
[Dev_PP_Sqm] = Convert(Numeric(10,0), 0),
[Dev_BP_Sqm] =Convert(Numeric(10,0), 0),
--Total FL                               
[Stp_PD_FL] = Convert(Numeric(10,0), 0),
[Stp_BD_FL] = Convert(Numeric(10,0), 0),
[Stp_VD_FL] = Convert(Numeric(10,0), 0),
[Stp_PP_FL] =Convert(Numeric(10,0), 0),
[Stp_BP_FL] = Convert(Numeric(10,0), 0),
[Stp_VP_FL] = Convert(Numeric(10,0), 0),
[Stp_FP_FL] = Convert(Numeric(10,0), 0),

[Stp_PD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_BD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_VD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_PP_FLNS] =Convert(Numeric(10,0), 0),
[Stp_BP_FLNS] = Convert(Numeric(10,0), 0),
[Stp_VP_FLNS] = Convert(Numeric(10,0), 0),
[Stp_FP_FLNS] = Convert(Numeric(10,0), 0),
	
[Stp_PD_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_BD_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_VD_CleanFL] =Convert(Numeric(10,0), 0),
[Stp_PP_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_BP_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_VP_CleanFL] = Convert(Numeric(10,0), 0), 
[Stp_FP_CleanFL] = Convert(Numeric(10,0), 0),
--Total Dev FL   
[Dev_PD_FL] = Convert(Numeric(10,0), 0),
[Dev_BD_FL] = Convert(Numeric(10,0), 0),
[Dev_VD_FL] = Convert(Numeric(10,0), 0),             
[Dev_PP_FL] = Convert(Numeric(10,0), 0),
[Dev_BP_FL] = Convert(Numeric(10,0), 0),
[Dev_VP_FL] = Convert(Numeric(10,0), 0),
 
--Total Reef Metres  
Dev_Plan_Adv = Convert(Numeric(10,2), 0),
Dev_PD_RAdv = Convert(Numeric(10,2), 0),
Dev_BD_RAdv = Convert(Numeric(10,2), 0),
Dev_VD_RAdv = Convert(Numeric(10,2), 0),
Dev_PP_RAdv = Convert(Numeric(10,2), 0),
Dev_BP_RAdv = Convert(Numeric(10,2), 0),
Dev_VP_RAdv = Convert(Numeric(10,2), 0),
Dev_Prev_RAdv = Convert(Numeric(10,2), 0),
Dev_PrevVar_RAdv = Convert(Numeric(10,2), 0),
Dev_DPerc_RAdv = Convert(Numeric(10,2), 0),
Dev_PPerc_RAdv = Convert(Numeric(10,2), 0),
Dev_FP_RAdv = Convert(Numeric(10,2), 0),
		
--Total Waste Metres                                   
Dev_PD_WAdv = Convert(Numeric(10,2), 0),
Dev_BD_WAdv = Convert(Numeric(10,2), 0),
Dev_VD_WAdv = Convert(Numeric(10,2), 0),
Dev_PP_WAdv = Convert(Numeric(10,2), 0),
Dev_BP_WAdv = Convert(Numeric(10,2), 0),
Dev_VP_WAdv = Convert(Numeric(10,2), 0),
Dev_Prev_WAdv = Convert(Numeric(10,2), 0),
Dev_PrevVar_WAdv = Convert(Numeric(10,2), 0),
Dev_DPerc_WAdv = Convert(Numeric(10,2), 0),
Dev_PPerc_WAdv = Convert(Numeric(10,2), 0),
Dev_FP_WAdv = Convert(Numeric(10,2), 0),	

--Total Metres                                                  
Dev_PD_Adv = Convert(Numeric(10,2), 0),
Dev_BD_Adv = Convert(Numeric(10,2), 0),
Dev_VD_Adv = Convert(Numeric(10,2), 0),
Dev_PP_Adv = Convert(Numeric(10,2), 0),
Dev_BP_Adv = Convert(Numeric(10,2), 0),
Dev_VP_Adv = Convert(Numeric(10,2), 0),
Dev_Prev_Adv = Convert(Numeric(10,2), 0),
Dev_PrevVar_Adv = Convert(Numeric(10,2), 0),
Dev_DPerc_Adv = Convert(Numeric(10,2), 0),
Dev_PPerc_Adv = Convert(Numeric(10,2), 0),	
Dev_FP_Adv = Convert(Numeric(10,2), 0),

--High Grade Reef Metres
Dev_PD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_BD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_VD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_PP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_BP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_VP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_FP_HgRAdv = Convert(Numeric(10,2), 0),

--tons                          
Dev_PD_RTons = Convert(Numeric(10,2), 0),
Dev_BD_RTons = Convert(Numeric(10,2), 0),
Dev_VD_RTons = Convert(Numeric(10,2), 0),
Dev_PP_RTons = Convert(Numeric(10,2), 0),
Dev_BP_RTons = Convert(Numeric(10,2), 0),
Dev_VP_RTons = Convert(Numeric(10,2), 0),
Dev_FP_RTons = Convert(Numeric(10,2), 0),
Dev_PD_WTons =Convert(Numeric(10,2), 0),
Dev_BD_WTons = Convert(Numeric(10,2), 0),
Dev_VD_WTons = Convert(Numeric(10,2), 0),
Dev_PP_WTons = Convert(Numeric(10,2), 0),
Dev_BP_WTons = Convert(Numeric(10,2), 0),
Dev_VP_WTons = Convert(Numeric(10,2), 0),
Dev_FP_WTons = Convert(Numeric(10,2), 0),

Da_StopeTons = Convert(Numeric(10,2), 0),
DaB_StopeTons = Convert(Numeric(10,2), 0),
DaV_StopeTons = Convert(Numeric(10,2), 0),   
D_StopeTons = Convert(Numeric(10,2), 0),           
B_StopeTons = Convert(Numeric(10,2), 0),
V_StopeTons = Convert(Numeric(10,2), 0),       
F_StopeTons = Convert(Numeric(10,2), 0),

--SQM Lost
Stp_BD_Mis = Convert(Numeric(10,1), 0),
Stp_BD_Labour = Convert(Numeric(10,1), 0),
Stp_BD_Other = Convert(Numeric(10,1), 0),
Stp_BP_Mis = Convert(Numeric(10,1), 0),
Stp_BP_Labour = Convert(Numeric(10,1), 0),
Stp_BP_Other = Convert(Numeric(10,1), 0),
TheField20 = '''',
TheField21 = '''',TheField22 = '''',    
TheField23 = '''', TheField24 = '''', TheField25 = '''',TheField26 = '''',TheField27 = '''',    
TheField28 = '''',

--Sweepings       
Stp_PP_Sweeps = Convert(Numeric(10,0), 0),

--Reef SW Excl Gullies
Stp_PP_ReefSW = Convert(Numeric(10,1), 0),
Stp_BP_ReefSW = Convert(Numeric(10,1), 0),
Stp_VP_ReefSW = Convert(Numeric(10,1), 0),
Stp_FP_ReefSW = Convert(Numeric(10,1), 0),

--Total cmg/t                         
[Stp_PP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_BP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_VP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_FP_Sqmcmgt] = Convert(Numeric(10,0), 0),
 
 --Content   
[Stp_PP_Content] = Convert(Numeric(10,4), 0),                                    
[Stp_BP_Content] = Convert(Numeric(10,4), 0),    
[Stp_VP_Content] = Convert(Numeric(10,4), 0),    
[Stp_PD_Content] = Convert(Numeric(10,4), 0),    
[Stp_BD_Content] =Convert(Numeric(10,4), 0),    
[Stp_VD_Content] = Convert(Numeric(10,4), 0),    
[Stp_FP_Content] = Convert(Numeric(10,4), 0)




go


create procedure [dbo].[sp_SICReport_Total]  --'MINEWARE', '2016-07-01', 'F', '4' 
--declare
@UserID varchar(10),
@CalendarDate varchar(10),
@SectionID varchar(20),
@Section int,
@MOName varchar(50)
--set @UserID ='MINEWARE'
--set @CalendarDate = '2017-01-17'
--set @SectionID = 'REA'
--set @Section ='4'
--set @MOName ='1.1 - A Dube'
as

declare @TheQuery varchar(8000)
declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)
declare @TheQuery3 varchar(8000)
declare @TheQuery4 varchar(8000)
declare @TheQuery5 varchar(8000)
declare @TheQuery6 varchar(8000)
declare @TheQuery7 varchar(8000)
declare @TheStartDate varchar(10)
declare @TheEndDate varchar(10)

select @TheStartDate = case when  convert(varchar(10),min(prevstartdate),120) is null then 
convert(varchar(10),min(startdate),120) else  convert(varchar(10),min(prevstartdate),120) end,
 @TheEndDate = convert(varchar(10),max(enddate),120)
from temp_sectionstartdate
where UserID = @UserID and
StartDate <= @CalendarDate and
EndDate >= @CalendarDate
--SELECT @TheStartDate
if (@TheStartDate is NULL)
BEGIN
	Select '' TheSection1, '' TheSection, 
'' workplaceid,
Dev_Check = Convert(decimal(10,1), 0),
Stp_Check = Convert(decimal(10,1), 0),
--Nr of Panels   
[Stp_PD_NoPanels] = Convert(Numeric(10,0), 0),  
[Stp_BD_NoPanels] = Convert(Numeric(10,0), 0),
[Stp_PP_NoPanels] = Convert(Numeric(10,0), 0),
[Stp_BP_NoPanels] = Convert(Numeric(10,0), 0),
--Nr of Dev Panels 
[Dev_PD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_BD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_VD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_PP_NoPanels] = Convert(Numeric(10,0), 0),  
[Dev_BP_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_VP_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_FP_NoPanels] = Convert(Numeric(10,0), 0),
--No of Cleaned Stope Panels
[Stp_BD_ClnPanels] = Convert(Numeric(10,0), 0),
--No of Cleaned Dev Panels
[Dev_PD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_BD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_VD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_PP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_BP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_VP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_FP_ClnPanels] = Convert(Numeric(10,0), 0),
--Shiftnr   
TheField1 = '''', 
ShiftNo =  Convert(Numeric(10,0), 0), 
TotalShft = Convert(Numeric(10,0), 0),
--M2 Mined
Stp_Plan_Sqm = Convert(Numeric(10,0), 0),
Stp_PD_Sqm = Convert(Numeric(10,0), 0),
Stp_BD_Sqm = Convert(Numeric(10,0), 0),
Stp_VD_Sqm = Convert(Numeric(10,0), 0),
Stp_PP_Sqm = Convert(Numeric(10,0), 0),
Stp_BP_Sqm = Convert(Numeric(10,0), 0),  
Stp_VP_Sqm = Convert(Numeric(10,0), 0),
Stp_FP_Sqm = Convert(Numeric(10,0), 0),
Stp_NewDay_Sqm = Convert(Numeric(10,0), 0),
Stp_Prev_Sqm = Convert(Numeric(10,0), 0),
Stp_PrevVar_Sqm = Convert(Numeric(10,0), 0),
Stp_DPerc_Sqm = Convert(Numeric(10,2), 0),
Stp_PPerc_Sqm = Convert(Numeric(10,2), 0),
Stp_FPSqm = Convert(Numeric(10,0), 0),	                                                                                                                         
--High Grade m2 Mined  
Stp_PD_HgSqm = Convert(Numeric(10,0), 0),
Stp_BD_HgSqm = Convert(Numeric(10,0), 0),
Stp_VD_HgSqm = Convert(Numeric(10,0), 0),
Stp_PP_HgSqm = Convert(Numeric(10,0), 0),
Stp_BP_HgSqm = Convert(Numeric(10,0), 0),  
Stp_VP_HgSqm = Convert(Numeric(10,0), 0),
Stp_FP_HgSqm = Convert(Numeric(10,0), 0),
Stp_NewDay_HgSqm = Convert(Numeric(10,0), 0),     
--Total Dev m2 Mined  
[Dev_PD_Sqm] = Convert(Numeric(10,0), 0),
[Dev_BD_Sqm] = Convert(Numeric(10,0), 0),  
[Dev_PP_Sqm] = Convert(Numeric(10,0), 0),
[Dev_BP_Sqm] =Convert(Numeric(10,0), 0),
--Total FL                               
[Stp_PD_FL] = Convert(Numeric(10,0), 0),
[Stp_BD_FL] = Convert(Numeric(10,0), 0),
[Stp_VD_FL] = Convert(Numeric(10,0), 0),
[Stp_PP_FL] =Convert(Numeric(10,0), 0),
[Stp_BP_FL] = Convert(Numeric(10,0), 0),
[Stp_VP_FL] = Convert(Numeric(10,0), 0),
[Stp_FP_FL] = Convert(Numeric(10,0), 0),

[Stp_PD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_BD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_VD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_PP_FLNS] =Convert(Numeric(10,0), 0),
[Stp_BP_FLNS] = Convert(Numeric(10,0), 0),
[Stp_VP_FLNS] = Convert(Numeric(10,0), 0),
[Stp_FP_FLNS] = Convert(Numeric(10,0), 0),
	
[Stp_PD_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_BD_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_VD_CleanFL] =Convert(Numeric(10,0), 0),
[Stp_PP_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_BP_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_VP_CleanFL] = Convert(Numeric(10,0), 0), 
[Stp_FP_CleanFL] = Convert(Numeric(10,0), 0),
--Total Dev FL   
[Dev_PD_FL] = Convert(Numeric(10,0), 0),
[Dev_BD_FL] = Convert(Numeric(10,0), 0),
[Dev_VD_FL] = Convert(Numeric(10,0), 0),             
[Dev_PP_FL] = Convert(Numeric(10,0), 0),
[Dev_BP_FL] = Convert(Numeric(10,0), 0),
[Dev_VP_FL] = Convert(Numeric(10,0), 0),
 
--Total Reef Metres  
Dev_Plan_Adv = Convert(Numeric(10,2), 0),
Dev_PD_RAdv = Convert(Numeric(10,2), 0),
Dev_BD_RAdv = Convert(Numeric(10,2), 0),
Dev_VD_RAdv = Convert(Numeric(10,2), 0),
Dev_PP_RAdv = Convert(Numeric(10,2), 0),
Dev_BP_RAdv = Convert(Numeric(10,2), 0),
Dev_VP_RAdv = Convert(Numeric(10,2), 0),
Dev_Prev_RAdv = Convert(Numeric(10,2), 0),
Dev_PrevVar_RAdv = Convert(Numeric(10,2), 0),
Dev_DPerc_RAdv = Convert(Numeric(10,2), 0),
Dev_PPerc_RAdv = Convert(Numeric(10,2), 0),
Dev_FP_RAdv = Convert(Numeric(10,2), 0),
		
--Total Waste Metres                                   
Dev_PD_WAdv = Convert(Numeric(10,2), 0),
Dev_BD_WAdv = Convert(Numeric(10,2), 0),
Dev_VD_WAdv = Convert(Numeric(10,2), 0),
Dev_PP_WAdv = Convert(Numeric(10,2), 0),
Dev_BP_WAdv = Convert(Numeric(10,2), 0),
Dev_VP_WAdv = Convert(Numeric(10,2), 0),
Dev_Prev_WAdv = Convert(Numeric(10,2), 0),
Dev_PrevVar_WAdv = Convert(Numeric(10,2), 0),
Dev_DPerc_WAdv = Convert(Numeric(10,2), 0),
Dev_PPerc_WAdv = Convert(Numeric(10,2), 0),
Dev_FP_WAdv = Convert(Numeric(10,2), 0),	

--Total Metres                                                  
Dev_PD_Adv = Convert(Numeric(10,2), 0),
Dev_BD_Adv = Convert(Numeric(10,2), 0),
Dev_VD_Adv = Convert(Numeric(10,2), 0),
Dev_PP_Adv = Convert(Numeric(10,2), 0),
Dev_BP_Adv = Convert(Numeric(10,2), 0),
Dev_VP_Adv = Convert(Numeric(10,2), 0),
Dev_Prev_Adv = Convert(Numeric(10,2), 0),
Dev_PrevVar_Adv = Convert(Numeric(10,2), 0),
Dev_DPerc_Adv = Convert(Numeric(10,2), 0),
Dev_PPerc_Adv = Convert(Numeric(10,2), 0),	
Dev_FP_Adv = Convert(Numeric(10,2), 0),

--High Grade Reef Metres
Dev_PD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_BD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_VD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_PP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_BP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_VP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_FP_HgRAdv = Convert(Numeric(10,2), 0),

--tons                          
Dev_PD_RTons = Convert(Numeric(10,2), 0),
Dev_BD_RTons = Convert(Numeric(10,2), 0),
Dev_VD_RTons = Convert(Numeric(10,2), 0),
Dev_PP_RTons = Convert(Numeric(10,2), 0),
Dev_BP_RTons = Convert(Numeric(10,2), 0),
Dev_VP_RTons = Convert(Numeric(10,2), 0),
Dev_FP_RTons = Convert(Numeric(10,2), 0),
Dev_PD_WTons =Convert(Numeric(10,2), 0),
Dev_BD_WTons = Convert(Numeric(10,2), 0),
Dev_VD_WTons = Convert(Numeric(10,2), 0),
Dev_PP_WTons = Convert(Numeric(10,2), 0),
Dev_BP_WTons = Convert(Numeric(10,2), 0),
Dev_VP_WTons = Convert(Numeric(10,2), 0),
Dev_FP_WTons = Convert(Numeric(10,2), 0),

Da_StopeTons = Convert(Numeric(10,2), 0),
DaB_StopeTons = Convert(Numeric(10,2), 0),
DaV_StopeTons = Convert(Numeric(10,2), 0),   
D_StopeTons = Convert(Numeric(10,2), 0),           
B_StopeTons = Convert(Numeric(10,2), 0),
V_StopeTons = Convert(Numeric(10,2), 0),       
F_StopeTons = Convert(Numeric(10,2), 0),

--SQM Lost
Stp_BD_Mis = Convert(Numeric(10,1), 0),
Stp_BD_Labour = Convert(Numeric(10,1), 0),
Stp_BD_Other = Convert(Numeric(10,1), 0),
Stp_BP_Mis = Convert(Numeric(10,1), 0),
Stp_BP_Labour = Convert(Numeric(10,1), 0),
Stp_BP_Other = Convert(Numeric(10,1), 0),
TheField20 = '''',
TheField21 = '''',TheField22 = '''',    
TheField23 = '''', TheField24 = '''', TheField25 = '''',TheField26 = '''',TheField27 = '''',    
TheField28 = '''',

--Sweepings       
Stp_PP_Sweeps = Convert(Numeric(10,0), 0),

--Reef SW Excl Gullies
Stp_PP_ReefSW = Convert(Numeric(10,1), 0),
Stp_BP_ReefSW = Convert(Numeric(10,1), 0),
Stp_VP_ReefSW = Convert(Numeric(10,1), 0),
Stp_FP_ReefSW = Convert(Numeric(10,1), 0),

--Total cmg/t                         
[Stp_PP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_BP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_VP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_FP_Sqmcmgt] = Convert(Numeric(10,0), 0),
 
 --Content   
[Stp_PP_Content] = Convert(Numeric(10,4), 0),                                    
[Stp_BP_Content] = Convert(Numeric(10,4), 0),    
[Stp_VP_Content] = Convert(Numeric(10,4), 0),    
[Stp_PD_Content] = Convert(Numeric(10,4), 0),    
[Stp_BD_Content] =Convert(Numeric(10,4), 0),    
[Stp_VD_Content] = Convert(Numeric(10,4), 0),    
[Stp_FP_Content] = Convert(Numeric(10,4), 0)

END
ELSE
BEGIN
   
set @TheQuery = 'select ' 
IF (@Section = 1)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_5 +'':''+sc.Name_5,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 2)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_4 +'':''+sc.Name_4,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 3)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_3 +'':''+sc.Name_3,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 4)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								 isnull(sc.SectionID_1 +'':''+sc.Name_1,'''') TheSection, '
END 
set @TheQuery = @TheQuery + ' 
isnull(workplaceid,'''') workplaceid,
Dev_Check = Convert(decimal(10,1),sum(isnull(DevCheck,0))),
Stp_Check = Convert(decimal(10,1),sum(isnull(StopeCheck,0))),
--Nr of Stope Blasted Panels   
[Stp_PD_NoPanels] = sum(Da_StopeTrue),  
[Stp_BD_NoPanels] = sum(DaB_StopeTrue), 
[Stp_PP_NoPanels] = sum(D_StopeTrue), 
[Stp_BP_NoPanels] = sum(B_StopeTrue),
--Nr of Dev Blasted Panels 
[Dev_PD_NoPanels] = sum(Da_DevTrue), 
[Dev_BD_NoPanels] = sum(DaB_DevTrue), 
[Dev_VD_NoPanels] = sum(DaB_DevTrue) - sum(Da_DevTrue), 
[Dev_PP_NoPanels] = sum(D_DevTrue),    
[Dev_BP_NoPanels] = sum(B_DevTrue),
[Dev_VP_NoPanels] = sum(B_DevTrue) - sum(D_DevTrue),
[Dev_FP_NoPanels] = sum(B_DevTrue) + sum(F_DevTrue),
--No of Cleaned Stope Panels
[Stp_BD_ClnPanels] = sum(DaB_CleanTrue),
--No of Cleaned Dev Panels
[Dev_PD_ClnPanels] = sum(Da_DevCleanTrue),
[Dev_BD_ClnPanels] = sum(DaB_DevCleanTrue),
[Dev_VD_ClnPanels] = sum(DaB_DevCleanTrue) - sum(Da_DevCleanTrue),
[Dev_PP_ClnPanels] = sum(D_DevCleanTrue),
[Dev_BP_ClnPanels] = sum(B_DevCleanTrue),
[Dev_VP_ClnPanels] = sum(B_DevCleanTrue) - sum(D_DevCleanTrue),
[Dev_FP_ClnPanels] = sum(B_DevCleanTrue) + sum(F_DevCleanTrue),

--Shiftnr   

TheField1 = '''', 
ShiftNo =  max(isnull(shiftnr,0)),   
TotalShft = max(isnull(Totalnumdays,0)),
--M2 Mined
Stp_Plan_Sqm = Convert(Numeric(10,0),sum(DM_m2)),
Stp_PD_Sqm = Convert(Numeric(10,0),sum(Da_m2)),  
Stp_BD_Sqm = Convert(Numeric(10,0),SUM(DaB_Sqm)),
Stp_VD_Sqm = Convert(Numeric(10,0),SUM(DaB_Sqm) - sum(Da_m2)),
Stp_PP_Sqm = Convert(Numeric(10,0),sum(D_m2)),
Stp_BP_Sqm = Convert(Numeric(10,0),sum(B_Sqm)),  
Stp_VP_Sqm = Convert(Numeric(10,0),sum(B_Sqm) - sum(D_m2)),
Stp_FP_Sqm = Convert(Numeric(10,0),sum(B_Sqm) + sum(F_m2)),
Stp_NewDay_Sqm = Convert(Numeric(10,0),case when (max(totalnumdays)- max(Shiftnr)) = 0 then 0 
				else (sum(DM_m2) - sum(B_Sqm))/(max(totalnumdays)- max(Shiftnr)) end), 
Stp_Prev_Sqm = Convert(Numeric(10,0),sum(BP_Sqm)),
Stp_PrevVar_Sqm = Convert(Numeric(10,0),sum(B_Sqm) - sum(BP_Sqm)), 
Stp_DPerc_Sqm = case when sum(isnull(Da_m2,0)) > 0 then
		Convert(Numeric(10,2),sum(DaB_Sqm)/sum(isnull(Da_m2,0)) * 100 ) else 0 end,
Stp_PPerc_Sqm = case when sum(isnull(D_m2,0)) > 0 then
		Convert(Numeric(10,2),sum(B_Sqm)/sum(isnull(D_m2,0)) * 100 ) else 0 end,
Stp_FPSqm = Convert(Numeric(10,0),sum(B_Sqm) + sum(F_m2)), 			                                                                                                                         

--High Grade m2 Mined  
Stp_PD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(Da_tm21)) else Convert(Numeric(10,0),sum(Da_tm2)) end, 
Stp_BD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(DaB_tm21)) else Convert(Numeric(10,0),sum(DaB_tm2)) end, 
Stp_VD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(DaB_tm21) - sum(Da_tm21)) else Convert(Numeric(10,0),sum(DaB_tm2) - sum(Da_tm2)) end, 
Stp_PP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(D_tm21)) else Convert(Numeric(10,0),sum(D_tm2)) end,
Stp_BP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(B_tm21)) else Convert(Numeric(10,0),sum(B_tm2)) end,   
Stp_VP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(B_tm21) - sum(D_tm21)) else Convert(Numeric(10,0),sum(B_tm2) - sum(D_tm2)) end, 
Stp_FP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(F_Tm21)) else Convert(Numeric(10,0),sum(F_Tm2)) end, 
--Stp_NewDay_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
--					else (sum(DM_tm21) - sum(B_tm21))/(max(totalnumdays) - max(Shiftnr)) end) else 0 end, 
Stp_NewDay_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),
						case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
							 else (sum(DM_tm21) - sum(B_tm21))/(max(totalnumdays) - max(Shiftnr)) end) 
					    else Convert(Numeric(10,0),case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
				   else (sum(DM_tm2) - sum(B_tm2))/(max(totalnumdays) - max(Shiftnr)) end) end, 
     
--Total Dev m2 Mined  
[Dev_PD_Sqm] = Convert(Numeric(10,0),sum(Da_Devm2)),  
[Dev_BD_Sqm] = Convert(Numeric(10,0),sum(DaB_Devm2)),     
[Dev_PP_Sqm] = Convert(Numeric(10,0),sum(D_Devm2)),
[Dev_BP_Sqm] = Convert(Numeric(10,0),sum(B_Devm2)),   
--Total FL                               
[Stp_PD_FL] = Convert(Numeric(10,0),isnull(sum(Da_FL),0)),
[Stp_BD_FL] = Convert(Numeric(10,0),isnull(sum(DaB_FL),0)),
[Stp_VD_FL] = Convert(Numeric(10,0),isnull(sum(DaB_FL),0) - isnull(sum(Da_FL),0)),
[Stp_PP_FL] = Convert(Numeric(10,0),isnull(sum(D_FL),0)),
[Stp_BP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0)),
[Stp_VP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0) - isnull(sum(D_FL),0)),
[Stp_FP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0) + isnull(sum(F_FL),0)),

[Stp_PD_FLNS] = Convert(Numeric(10,0),isnull(sum(Da_FLNS),0)),
[Stp_BD_FLNS] = Convert(Numeric(10,0),isnull(sum(DaB_FLNS),0)),
[Stp_VD_FLNS] = Convert(Numeric(10,0),isnull(sum(DaB_FLNS),0) - isnull(sum(Da_FLNS),0)),
[Stp_PP_FLNS] = Convert(Numeric(10,0),isnull(sum(D_FLNS),0)),
[Stp_BP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0)),
[Stp_VP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0) - isnull(sum(D_FLNS),0)),
[Stp_FP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0) + isnull(sum(F_FLNS),0)),
	
[Stp_PD_CleanFL] = Convert(Numeric(10,0),sum(isnull(Da_CleanFL,0))), 
[Stp_BD_CleanFL] = Convert(Numeric(10,0),sum(isnull(DaB_CleanFL,0))), 
[Stp_VD_CleanFL] = Convert(Numeric(10,0),sum(isnull(DaB_CleanFL,0)) - sum(isnull(Da_CleanFL,0))),  
[Stp_PP_CleanFL] = Convert(Numeric(10,0),sum(isnull(D_CleanFL,0))),
[Stp_BP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0))), 
[Stp_VP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0)) - sum(isnull(D_CleanFL,0))),  
[Stp_FP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0)) + sum(isnull(F_CleanFL,0))),  
--Total Dev FL   
[Dev_PD_FL] = case when sum(Da_DevWAdv) + sum(Da_DevRAdv) > 0 then      
  Convert(Numeric(10,0),sum(Da_Devm2) / (sum(Da_DevWAdv) + sum(Da_DevRAdv))) else 0 end,  
[Dev_BD_FL] = case when sum(DaB_DevRAdv) > 0 then     
  Convert(Numeric(10,0),sum(DaB_Devm2) / sum(DaB_DevRAdv)) else 0 end,    
[Dev_VD_FL] = (case when sum(DaB_DevWAdv) + sum(DaB_DevRAdv) > 0 then     
   Convert(Numeric(10,0),sum(DaB_Devm2) / (sum(DaB_DevWAdv) + sum(DaB_DevRAdv))) else 0 end) - 
   (case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then
  Convert(Numeric(10,0),sum(D_Devm2) / sum(D_DevWAdv + D_DevRAdv)) else 0 end),                             
[Dev_PP_FL] = case when sum(D_DevWAdv + D_DevRAdv) > 0 then   
  Convert(Numeric(10,0),sum(D_Devm2) / (sum(D_DevWAdv) + sum(D_DevRAdv))) else 0 end, 
[Dev_BP_FL] = case when sum(B_DevRAdv) > 0 then    
   Convert(Numeric(10,0),sum(B_Devm2) / sum(B_DevRAdv)) else 0 end, 
[Dev_VP_FL] = (case when sum(B_DevWAdv) + sum(B_DevRAdv) > 0 then          
   Convert(Numeric(10,0),sum(B_Devm2) / (sum(B_DevWAdv) + sum(B_DevRAdv))) else 0 end) - 
   (case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then 
   Convert(Numeric(10,0),sum(D_Devm2) / (sum(D_DevWAdv) + sum(D_DevRAdv))) else 0 end),'
set @TheQuery1 = '   
--Total Reef Metres  
Dev_Plan_Adv = Convert(Numeric(10,2),sum(isnull(DM_DevAdv,0))), 
Dev_PD_RAdv = Convert(Numeric(10,2),sum(Da_DevRAdv)),
Dev_BD_RAdv = Convert(Numeric(10,2),sum(DaB_DevRAdv)), 
Dev_VD_RAdv = Convert(Numeric(10,2),sum(DaB_DevRAdv) - sum(Da_DevRAdv)),  
Dev_PP_RAdv = Convert(Numeric(10,2),sum(D_DevRAdv)),
Dev_BP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv)),
Dev_VP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) - sum(D_DevRAdv)), 
Dev_Prev_RAdv = Convert(Numeric(10,2),sum(BP_DevRAdv)),
Dev_PrevVar_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) - sum(BP_DevRAdv)), 
Dev_DPerc_RAdv = case when sum(Da_DevRAdv) > 0 then
		Convert(Numeric(10,2),(sum(DaB_DevRAdv)/sum(Da_DevRAdv)*100)) else 0 end, 
Dev_PPerc_RAdv = case when sum(D_DevRAdv) > 0 then
		Convert(Numeric(10,2),(sum(B_DevRAdv)/sum(D_DevRAdv)*100)) else 0 end,
Dev_FP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) + sum(F_DevRAdv)),  
		
--Total Waste Metres                                   
Dev_PD_WAdv = Convert(Numeric(10,2),sum(Da_DevWAdv)), 
Dev_BD_WAdv = Convert(Numeric(10,2),sum(DaB_DevWAdv)), 
Dev_VD_WAdv = Convert(Numeric(10,2),sum(DaB_DevWAdv)- sum(Da_DevWAdv)),
Dev_PP_WAdv = Convert(Numeric(10,2),sum(D_DevWAdv)),
Dev_BP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv)),
Dev_VP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) - sum(D_DevWAdv)), 
Dev_Prev_WAdv = Convert(Numeric(10,2),sum(BP_DevWAdv)),
Dev_PrevVar_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) - sum(BP_DevWAdv)), 
Dev_DPerc_WAdv = case when sum(Da_DevWAdv) > 0 then
		Convert(Numeric(10,2),(sum(DaB_DevWAdv)/sum(Da_DevWAdv)*100)) else 0 end, 
Dev_PPerc_WAdv = case when sum(D_DevWAdv) > 0 then
		Convert(Numeric(10,2),(sum(B_DevWAdv)/sum(D_DevWAdv)*100)) else 0 end, 
Dev_FP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) + sum(F_DevWAdv)), 		

--Total Metres                                                  
Dev_PD_Adv = Convert(Numeric(10,2),sum(Da_DevWAdv) + sum(Da_DevRAdv)), 
Dev_BD_Adv = Convert(Numeric(10,2),sum(DaB_DevRAdv)+sum(DaB_DevWAdv)), 
Dev_VD_Adv = Convert(Numeric(10,2),(sum(DaB_DevRAdv) + sum(DaB_DevWAdv)) - (sum(Da_DevWAdv) +sum(Da_DevRAdv))),
Dev_PP_Adv = Convert(Numeric(10,2),sum(D_DevWAdv) + sum(D_DevRAdv)), 
Dev_BP_Adv = Convert(Numeric(10,2),sum(B_DevRAdv) + sum(B_DevWAdv)),   
Dev_VP_Adv = Convert(Numeric(10,2),(sum(B_DevRAdv) + sum(B_DevWAdv)) - (sum(D_DevWAdv) + sum(D_DevRAdv))),
Dev_Prev_Adv = Convert(Numeric(10,2),sum(BP_DevRAdv) + sum(BP_DevWAdv)),   
Dev_PrevVar_Adv = Convert(Numeric(10,2),(sum(B_DevRAdv) + sum(B_DevWAdv)) - (sum(BP_DevRAdv) + sum(BP_DevWAdv))),
Dev_DPerc_Adv = case when sum(Da_DevRAdv) + sum(Da_DevWAdv) > 0 then
		Convert(Numeric(10,2),((sum(DaB_DevRAdv) + sum(DaB_DevWAdv)) / (sum(Da_DevRAdv) + sum(Da_DevWAdv))*100)) else 0 end, 
Dev_PPerc_Adv = case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then
		Convert(Numeric(10,2),((sum(B_DevRAdv) + sum(B_DevWAdv)) / (sum(D_DevWAdv) + sum(D_DevRAdv))*100)) else 0 end, 	
Dev_FP_Adv = Convert(Numeric(10,2),sum(B_TDevRAdv) + sum(B_TDevWAdv) + sum(F_TDevRAdv)),

--High Grade Reef Metres
case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(Da_tm21)) else Convert(Numeric(10,0),sum(Da_tm2)) end, 
Dev_PD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(Da_TDevRAdv1)) else Convert(Numeric(10,2),sum(Da_TDevRAdv)) end,
Dev_BD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(DaB_TDevRAdv1)) else Convert(Numeric(10,2),sum(DaB_TDevRAdv)) end,
Dev_VD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(DaB_TDevRAdv1) - sum(Da_TDevRAdv1)) else Convert(Numeric(10,2),sum(DaB_TDevRAdv) - sum(Da_TDevRAdv)) end,
Dev_PP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(D_TDevRAdv1)) else Convert(Numeric(10,2),sum(D_TDevRAdv)) end,
Dev_BP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(B_TDevRAdv1)) else Convert(Numeric(10,2),sum(B_TDevRAdv)) end,
Dev_VP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(B_TDevRAdv1) - sum(D_TDevRAdv1)) else Convert(Numeric(10,2),sum(B_TDevRAdv) - sum(D_TDevRAdv)) end,
Dev_FP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(F_TDevRAdv1)) else Convert(Numeric(10,2),sum(F_TDevRAdv)) end,

--tons                          
Dev_PD_RTons = Convert(Numeric(10,2),sum(Da_DevRTons)),
Dev_BD_RTons = Convert(Numeric(10,2),sum(DaB_DevRTons)),
Dev_VD_RTons = Convert(Numeric(10,2),sum(DaB_DevRTons) - sum(Da_DevRTons)),
Dev_PP_RTons = Convert(Numeric(10,2),sum(D_DevRTons)),
Dev_BP_RTons = Convert(Numeric(10,2),sum(B_DevRTons)), 
Dev_VP_RTons = Convert(Numeric(10,2),sum(B_DevRTons) - sum(D_DevRTons)), 
Dev_FP_RTons = Convert(Numeric(10,2),sum(B_DevRTons) + sum(F_DevRTons)),
Dev_PD_WTons = Convert(Numeric(10,2),sum(Da_DevWTons)), 
Dev_BD_WTons = Convert(Numeric(10,2),sum(DaB_DevWTons)),
Dev_VD_WTons = Convert(Numeric(10,2),sum(DaB_DevWTons) - sum(Da_DevWTons)), 
Dev_PP_WTons = Convert(Numeric(10,2),sum(D_DevWTons)), 
Dev_BP_WTons = Convert(Numeric(10,2),sum(B_DevWTons)), 
Dev_VP_WTons = Convert(Numeric(10,2),sum(B_DevWTons) - sum(D_DevWTons)),
Dev_FP_WTons = Convert(Numeric(10,2),sum(B_DevWTons) + sum(F_DevWTons)),

Da_StopeTons = Convert(Numeric(10,2),sum(Da_StopeTons)), 
DaB_StopeTons = Convert(Numeric(10,2),sum(DaB_StopeTons)), 
DaV_StopeTons = Convert(Numeric(10,2),sum(DaB_StopeTons) - sum(Da_StopeTons)),        
D_StopeTons = Convert(Numeric(10,2),sum(D_StopeTons)),                 
B_StopeTons = Convert(Numeric(10,2),sum(B_StopeTons)),  
V_StopeTons = Convert(Numeric(10,2),sum(B_StopeTons) - sum(D_StopeTons)),         
F_StopeTons = Convert(Numeric(10,2),sum(F_StopeTons)), ' 
set @TheQuery2 = '
--SQM Lost
Stp_BD_Mis = Convert(Numeric(10,1),sum(Da_Misefires)), 
Stp_BD_Labour = Convert(Numeric(10,1),sum(Da_Labour)), 
Stp_BD_Other = Convert(Numeric(10,1),sum(Da_Other)), 
Stp_BP_Mis = Convert(Numeric(10,1),sum(D_Misefires)), 
Stp_BP_Labour = Convert(Numeric(10,1),sum(D_Labour)), 
Stp_BP_Other = Convert(Numeric(10,1),sum(D_Other)),   
TheField20 = '''',
TheField21 = '''',TheField22 = '''',    
TheField23 = '''', TheField24 = '''', TheField25 = '''',TheField26 = '''',TheField27 = '''',    
TheField28 = '''',

--Sweepings       
Stp_PP_Sweeps = Convert(Numeric(10,0),sum(D_ReefSweep + D_WasteSweep)),   

--Reef SW Excl Gullies
Stp_PP_ReefSW = case when sum(D_Reefm2) > 0 then
	Convert(Numeric(10,1),sum(D_ReefSqmSW) / sum(D_Reefm2)) else 0 end, 
Stp_BP_ReefSW = case when sum(B_Reefm2) > 0 then
	Convert(Numeric(10,1),sum(B_ReefSqmSW) / sum(B_Reefm2)) else 0 end, 
Stp_VP_ReefSW = case when sum(B_Reefm2) + sum(D_Reefm2) > 0 then
	Convert(Numeric(10,1),(sum(B_ReefSqmSW) + sum(D_ReefSqmSW)) / (sum(B_Reefm2) + sum(D_Reefm2))) else 0 end,
Stp_FP_ReefSW = case when sum(B_Reefm2) + sum(F_Reefm2) > 0 then
	(sum(B_ReefSqmSW) + sum(F_ReefSqmSW))/(sum(B_Reefm2) + sum(F_Reefm2)) else 0 end,	

--Total cmg/t                         
[Stp_PP_Sqmcmgt] = case when sum(D_Reefm2) > 0 then
	Convert(Numeric(10,0),sum(D_sqmreefcmgt) / sum(D_Reefm2)) else 0 end,
[Stp_BP_Sqmcmgt] = case when sum(B_Reefm2) > 0 then
	Convert(Numeric(10,0),sum(B_sqmreefcmgt) / sum(B_Reefm2)) else 0 end, 
[Stp_VP_Sqmcmgt] = case when sum(B_Reefm2) + sum(D_Reefm2) > 0 then
	Convert(Numeric(10,0),(sum(B_sqmreefcmgt) + sum(D_sqmreefcmgt)) / (sum(B_Reefm2) + sum(D_Reefm2))) else 0 end,
[Stp_FP_Sqmcmgt] = case when sum(B_Reefm2) + sum(F_reefm2) > 0 then
	(sum(B_sqmreefcmgt) + sum(F_sqmreefcmgt)) / (sum(B_Reefm2) + sum(F_reefm2)) else 0 end, 
 
 --Content   
[Stp_PP_Content] = Convert(Numeric(10,4),sum(D_grams)/1000) ,                                       
[Stp_BP_Content] = Convert(Numeric(10,4),sum(B_grams)/1000), 
[Stp_VP_Content] = Convert(Numeric(10,4),(sum(B_grams)/1000) - (sum(D_grams) / 1000)),
[Stp_PD_Content] = Convert(Numeric(10,4),sum(Da_grams)/1000),
[Stp_BD_Content] = Convert(Numeric(10,4),sum(DaB_grams)/1000),   
[Stp_VD_Content] = Convert(Numeric(10,4),(sum(DaB_grams)/1000) - (sum(Da_grams) / 1000)),
[Stp_FP_Content] = Convert(Numeric(10,4),(sum(B_grams)/ 1000) + (sum(F_grams) / 1000))
from
		 (select 
p.workplaceid,p.prodmonth,p.sectionid,p.calendardate,
totalnumdays = case when p.prodmonth = ts.ProdMonth then wd.TotalShifts else 0 end,
Shiftnr = case when (p.prodmonth = ts.ProdMonth) and (wd.calendardate = '''+@CalendarDate+''') 
	then wd.shift else 0 end,
DevCheck = case when (p.prodmonth = ts.ProdMonth) and (p.activity = 1) 
	then 1 else 0 end,
StopeCheck = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then 1 else 0 end,  
TheToppanelsMO = (case when tp.workplaceid is not null then 1 else 0 end),
TheToppanelsMAN = (case when t.workplaceid is not null then 1 else 0 end),
Da_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.sqm > 0) then 1 else 0 end,
DaB_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then 1 else 0 end,     
D_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.sqm > 0) then 1 else 0 end, 
B_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then 1 else 0 end,    
	      
DaB_Cleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.Value > 0) then 1 else 0 end,
	 
Da_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end,
DaB_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (cl.Value > 0) and (cl.Sickey = 22) then 1 else 0 end,     
D_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
B_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (cl.Value > 0) and (cl.Sickey = 22) then 1 else 0 end,          
F_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
Da_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.BookMetresAdvance > 0) then 1 else 0 end,
DaB_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (cl1.Value > 0) and (cl1.Sickey = 23) then 1 else 0 end,     
D_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.BookMetresAdvance > 0) then 1 else 0 end, 
B_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (cl1.Value > 0) and (cl1.Sickey = 23) then 1 else 0 end, 
F_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
D_ReefSweep = 0,                                                                                                                           
D_WasteSweep = 0,'
set @TheQuery3 = '
DM_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) then p.sqm else 0 end, 
DM_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then p.reefsqm else 0 end, 
DM_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then p.wastesqm  else 0 end,     
DM_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and (t.workplaceid is not null)
	then p.sqm else 0 end, 
DM_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and
	 (t.workplaceid is not null) then p.reefsqm else 0 end, 
DM_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and 
	(t.workplaceid is not null) then p.wastesqm  else 0 end, 
Da_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.sqm else 0 end,
Da_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9))then p.reefsqm else 0 end,
Da_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.wastesqm  else 0 end,  
DaB_Sqm =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookSqm else 0 end,     
DaB_reefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookReefSqm else 0 end,     
DaB_offreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookWasteSqm else 0 end,        
D_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.sqm else 0 end, 
D_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm else 0 end, 
D_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and 
	(p.activity IN (0,9)) then p.wastesqm  else 0 end, 
B_Sqm =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookSqm else 0 end,    
B_reefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookReefSqm else 0 end,    
B_offreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookWasteSqm else 0 end,   
F_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.sqm else 0 end, 
F_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	 (p.activity IN (0,9)) then p.reefsqm else 0 end, 
F_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.wastesqm else 0 end,'
set @TheQuery4 = ' 	
Da_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end,
Da_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.reefsqm else 0 end,
Da_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.wastesqm  else 0 end,  
DaB_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.BookSqm else 0 end, 
DaB_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookReefSqm else 0 end,     
DaB_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) then p.BookWasteSqm else 0 end,        
D_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end, 
D_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.reefsqm else 0 end, 
D_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.wastesqm  else 0 end, 
B_Tm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.BookSqm else 0 end,    
B_Treefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) 
	then p.BookReefSqm else 0 end,    
B_Toffreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) 
	then p.BookWasteSqm else 0 end, 
F_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end,

DM_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and (tp.workplaceid is not null)
	then p.sqm else 0 end,	
Da_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end,
Da_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.reefsqm else 0 end,
Da_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.wastesqm  else 0 end,  
DaB_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.BookSqm else 0 end, 
DaB_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookReefSqm else 0 end,     
DaB_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) then p.BookWasteSqm else 0 end,        
D_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end, 
D_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.reefsqm else 0 end, 
D_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.wastesqm  else 0 end, 
B_Tm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.BookSqm else 0 end,    
B_Treefm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) 
	then p.BookReefSqm else 0 end,    
B_Toffreefm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) 
	then p.BookWasteSqm else 0 end, 
F_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end,
	
BP_Sqm =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and  
	(p.activity IN (0,9)) then p.BookSqm else 0 end,    
--BP_reefm2 =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
--	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') then p.BookSqm else 0 end, 
--BP_offreefm2 =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
--	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') then p.BookSqm else 0 end,                               
 
Da_FL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end,   
DaB_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.bookSqm > 0) then P.bookFL else 0 end,   
D_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end, 
B_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.bookSqm > 0) then P.bookFL else 0 end,       
F_FL = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end, 
	
Da_FLNS =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end,   
DaB_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.SicKey = 22) then cl.Value else 0 end,   
D_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end, 
B_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.SicKey = 22) then cl.Value else 0 end,       
F_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end, 

Da_CleanFL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end,
DaB_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl1.SicKey = 23) then cl1.Value else 0 end,     
D_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end, '
set @TheQuery5 = ' 
B_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (cl1.SicKey = 23) then cl1.Value else 0 end,
F_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end,
-- vir stoping advance per blast
Da_MAdv =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end,   
DaB_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookMetresAdvance else 0 end,  
D_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end,   
B_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookMetresAdvance else 0 end,        
F_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end, 
	   
DM_DevAdv = case when (p.prodmonth = ts.ProdMonth) and (p.activity = 1) 
	then p.MetresAdvance else 0 end, 
BP_DevRAdv = case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end,
Da_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end, 
DaB_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end,
D_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end,
B_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end, 
F_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end,                               

BP_DevWAdv = case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
Da_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteAdv else 0 end,  
DaB_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
D_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteAdv else 0 end,
B_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
F_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate >'''+@CalendarDate+''') AND 
	(p.activity = 1) then p.WasteAdv else 0 end,                                

Da_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,  
DaB_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end,
D_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,
B_TDevRAdv = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end, 
F_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,                                   

B_TDevWAdv = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end,
Da_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,  
DaB_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end,
D_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,
B_TDevRAdv1 = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end, 
F_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,                                   

B_TDevWAdv1 = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end,


Da_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end,
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity  else 0 end, 
DaB_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.BookReefTons else 0 end,
	--(pp.ReefWaste = ''R'') then p.BookMetresAdvance * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                               
D_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end,
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity  else 0 end, 
B_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookReefTons else 0 end,
	--and (pp.ReefWaste = ''R'') p.BookMetresAdvance * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                               
F_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end, 
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity else 0 end,   

Da_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) then p.WasteTons else 0 end,
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity  else 0 end,
DaB_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookWasteTons else 0 end,
	--and (pp.ReefWaste = ''W'') then p.BookAdv * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                   
D_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteTons else 0 end,
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity  else 0 end,
B_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookWasteTons else 0 end,
	--and (pp.ReefWaste = ''W'') then p.BookAdv * pp.DHeight * pp.DWidth * sl.RockDensity else 0 end,  
F_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) then p.WasteTons else 0 end,  
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity else 0 end, '
Set @TheQuery7 = '      
Da_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,        
D_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,         
DaB_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookTons else 0 end,
	--p.BookSqm * p.BookSW / 100 * sl.RockDensity else 0 end,         
B_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookTons else 0 end, 
	--p.BookSqm * p.BookSW / 100 * sl.RockDensity else 0 end,          
F_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,                             

Da_DevUReefTons = 0, 
	--case when (p.prodmonth = ts.ProdMonth) and 
	--(p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) and (pp.ReefWaste = ''R'') then p.Units * pp.dens  else 0 end,    
D_DevUReefTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and 
	--(p.activity = 1) and (pp.ReefWaste = ''R'') then p.Units * pp.dens  else 0 end,
Da_DevUWasteTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	--(p.activity = 1) and (pp.ReefWaste = ''W'') then p.Units * pp.dens  else 0 end, 
D_DevUWasteTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	--(p.activity = 1) and (pp.ReefWaste = ''W'') then p.Units * pp.dens  else 0 end,

Da_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Grams else 0 end, 
DaB_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookGrams else 0 end,
D_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Grams else 0 end, 
B_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and    
	(p.activity IN (0,9)) then p.BookGrams else 0 end,                                       
F_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.Grams else 0 end,

Da_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm * pp.SW else 0 end,
DaB_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm * p.BookSW else 0 end,                                          
D_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm * pp.SW else 0 end,
B_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm  * p.BookSW else 0 end,                                                                                                          
F_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then pp.SW * p.SQM else 0 end,                                                                                                 

D_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then pp.GT * p.reefsqm else 0 end,
B_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <='''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm * p.Bookcmgt  else 0 end,
Da_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then pp.GT * p.reefsqm else 0 end, 
DaB_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	 (p.activity IN (0,9)) then p.BookReefSqm * p.Bookcmgt  else 0 end,  
F_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then pp.GT * p.reefsqm else 0 end,

Da_Misefires = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and
		    (p.ProblemID = ''B1'') and (p.CausedLostBlast = ''Y'') and (p.activity IN (0,9)) then p.sqm else 0 end, 
Da_Labour = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
			(p.ProblemID in (''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and
			(p.activity IN (0,9)) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
Da_Other =  
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
			(p.ProblemID not in (''B1'',''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
			(p.activity IN (0,9)) and (p.ProblemID is not null) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
D_Misefires = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and 
		    (p.ProblemID = ''B1'') and (p.CausedLostBlast = ''Y'') and 
			(p.activity IN (0,9)) and (p.sqm > 0) then p.sqm else 0 end, 
D_Labour = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and  
            (p.ProblemID in (''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
            (p.activity IN (0,9)) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
D_Other = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and  
			(p.ProblemID not in (''B1'',''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
			(p.activity IN (0,9))  and (p.ProblemID is not null) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end,
F_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) then (p.MetresAdvance * pp.DWidth) else 0 end,  			 
--DaB_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance + p.BookAdv) * pp.DWidth else 0 end,
DaB_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance) * pp.DWidth else 0 end,
D_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and (p.activity = 1) then (p.WasteAdv + p.ReefAdv) * pp.dwidth else 0 end,
Da_Devm2 = case when(p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''')and (p.activity = 1) then (p.WasteAdv + p.ReefAdv) * pp.dwidth else 0 end,  
B_Devm2 = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance) * pp.dwidth else 0 end                               
--B_Devm2 = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance + p.BookMetresAdvance) * pp.DWidth else 0 end
from Planning p 
left outer join PLANMONTH pp on
  pp.workplaceid = p.workplaceid and 
  pp.Prodmonth = p.Prodmonth and  
  pp.SectionID = p.SectionID and 
  pp.Activity = p.Activity  
--left outer join problems_Complete r on
--	p.workplaceid = r.workplaceid and 
--	p.Prodmonth = r.Prodmonth and  
--	p.SectionID = r.SectionID and 
--	p.Activity = r.Activity and   
--	p.CalendarDate = r.CalendarDate
inner join section_complete sc on
  sc.SectionID = p.sectionid and 
  sc.prodmonth = p.prodmonth ' 
set @TheQuery6 = ''
  IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + '
left outer join TopPanelsSelected t on
  t.Prodmonth = p.Prodmonth and
  t.SectionID = sc.SectionID_5 and
  t.WorkplaceID = p.WorkplaceID and
  t.Activity = p.Activity 
  left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_2 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity  '
 END 
  IF (@Section = 1) or (@Section = 2)or (@Section = 3)
BEGIN
   set @TheQuery6 = @TheQuery6 + '
left outer join TopPanelsSelected t on
  t.Prodmonth = p.Prodmonth and
  t.SectionID = sc.SectionID_5 and
  t.WorkplaceID = p.WorkplaceID and
  t.Activity = p.Activity 
  left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_2 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity'
 END
 set @TheQuery6 = @TheQuery6 + '  
left outer join Temp_SectionStartDate ts on
  ts.SectionID = p.SectionID and
  ts.UserID = '''+@UserID+''' 
left outer join TempWorkdaysMO wd on
  wd.UserID = '''+@UserID+''' and
  wd.SectionID = sc.SectionID_2 and
  wd.Prodmonth = ts.ProdMonth and
  wd.CalendarDate = '''+@CalendarDate+'''
left outer join tempworkdaysMO wd1 on
  wd1.UserID = '''+@UserID+''' and   
  wd1.sectionid = ts.SectionID and  
  wd1.ProdMonth = ts.PrevProdMonth and
  wd1.Shift = wd.Shift 
left outer join SICCapture cl on
  cl.CalendarDate = p.CalendarDate and
  cl.SICKey in (22) and
  cl.SectionID = sc.SectionID_1 and
  cl.WorkplaceID = p.WorkplaceID
  left outer join SICCapture cl1 on
  cl1.CalendarDate = p.CalendarDate and
  cl1.SICKey in (23) and
  cl1.SectionID = sc.SectionID_1 and
  cl1.WorkplaceID = p.WorkplaceID,    
 Sysset sl                   
where p.CalendarDate >= '''+@TheStartDate+''' and p.CalendarDate <= '''+@TheEndDate+''' '
IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_2 = '''+@SectionID+''' and sc.Name_2 = '''+@MOName+''' '
END 
if(@Section = 3)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_3 = '''+@SectionID+''' '
end
if(@Section = 2)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_4 = '''+@SectionID+''' '
end 
if(@Section = 1)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_5 = '''+@SectionID+''' '
end
set @TheQuery6 = @TheQuery6 + ') a 
left outer join Temp_SectionStartDate t on
  t.SectionID = a.SectionID and
  t.UserID = '''+@UserID+''' 
inner join Section_Complete sc on
  sc.ProdMonth = t.ProdMonth and
  sc.SectionID = a.SectionID ' 
  if(@Section = 4)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_2 ='''+CAST(@SectionID AS VARCHAR(10))+''' and 
				sc.Name_2 = '''+@MOName+''' '
  end
  if(@Section = 3)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_3 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
  if(@Section = 2)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_4 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
  if(@Section = 1)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_5 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
set @TheQuery6 = @TheQuery6 +'group by ' 
IF (@Section = 1)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_5 +'':''+sc.Name_5,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 2)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_4 +'':''+sc.Name_4,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 3)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_3 +'':''+sc.Name_3,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , 
								 isnull(sc.SectionID_1 +'':''+sc.Name_1,'''') , '
END 
set @TheQuery6 = @TheQuery6 + ' a.Workplaceid with rollup '


--Print @TheQuery
--Print @TheQuery1
--Print @TheQuery2
--Print @TheQuery3
--Print @TheQuery4
--Print @TheQuery5
--Print @TheQuery7
--Print @TheQuery6

Exec (@TheQuery+@TheQuery1+@TheQuery2+@TheQuery3+@TheQuery4+@TheQuery5+ @TheQuery7+@TheQuery6)
	
END


 

go


create 
procedure [dbo].[sp_SICReport_Milling] 
@CalendarDate varchar(10),
@Mill Varchar(10)

as

declare @TheStartDate varchar(10)

SET NOCOUNT ON;
select @TheStartDate = convert(varchar(10),a.StartDate,120)
	from
	(select min(cm.StartDate) StartDate 
		  from CODE_CALENDAR cc
		  inner join CALENDARMILL cm on
		    cm.CalendarCode = cc.CalendarCode
		  where cm.StartDate <= @CalendarDate and 
		  cm.EndDate >= @CalendarDate and
		  cc.Description = 'Mill Calendar' ) a

SET NOCOUNT ON;

IF (@TheStartDate is NULL) 
BEGIN
select
	DayB_ToPlant = convert(decimal(13,1), 0),
	DayB_PlannedToPlant = convert(decimal(13,1), 0),
	ProgB_ToPlant = convert(decimal(13,1), 0),
	ProgB_PlannedToPlant = convert(decimal(13,1), 0),
	DayB_Treated = convert(decimal(13,1), 0),
	DayB_PlannedTreated = convert(decimal(13,1), 0),	
	ProgB_Treated = convert(decimal(13,1), 0),
	ProgB_PlannedTreated = convert(decimal(13,1), 0)
END
ELSE
BEGIN
	if @Mill = ''
	BEGIN
	select
	   DayB_ToPlant = convert(decimal(13,1), isnull(sum(DayB_ToPlant),0)),
	   DayB_PlannedToPlant = convert(decimal(13,1), isnull(sum(DayB_PlannedToPlant),0)),
	   ProgB_ToPlant = convert(decimal(13,1), isnull(sum(ProgB_ToPlant),0)),
	   ProgB_PlannedToPlant = convert(decimal(13,1), isnull(sum(ProgB_PlannedToPlant),0)),
	   DayB_Treated = convert(decimal(13,1), isnull(sum(DayB_Treated),0)),
	   DayB_PlannedTreated = convert(decimal(13,1), isnull(sum(DayB_PlannedTreated),0)),
	   ProgB_Treated = convert(decimal(13,1), isnull(sum(ProgB_Treated),0)),
	   ProgB_PlannedTreated = convert(decimal(13,1), isnull(sum(ProgB_PlannedTreated),0))
	from
	(
	select 
	   DayB_Treated = case when (b.CalendarDate = @CalendarDate)
							then b.TonsTreated else 0 end,
	   ProgB_Treated = case when (b.CalendarDate <= @CalendarDate) 
							then b.TonsTreated  else 0 end,
	   DayB_ToPlant = case when (b.CalendarDate = @CalendarDate)
							then b.TonsToPlant else 0 end,
	   ProgB_ToPlant = case when (b.CalendarDate <= @CalendarDate) 
							then b.TonsToPlant  else 0 end,
	   DayB_PlannedToPlant = case when (b.CalendarDate = @CalendarDate) 
							then b.PlannedTonsToPlant  else 0 end,
	   ProgB_PlannedToPlant = case when (b.CalendarDate <= @CalendarDate) 
							then b.PlannedTonsToPlant  else 0 end,
	   DayB_PlannedTreated = case when (b.CalendarDate = @CalendarDate) 
							then b.PlannedTonsTreated  else 0 end,
	   ProgB_PlannedTreated = case when (b.CalendarDate <= @CalendarDate) 
							then b.PlannedTonsTreated  else 0 end

	from BookingMilling b
	where b.CalendarDate >= @TheStartDate and
		  b.CalendarDate <= @CalendarDate
	 ) a
	 END
	 ELSE
	 BEGIN
	 select
	   DayB_ToPlant = convert(decimal(13,1), isnull(sum(DayB_ToPlant),0)),
	   DayB_PlannedToPlant = convert(decimal(13,1), isnull(sum(DayB_PlannedToPlant),0)),
	   ProgB_ToPlant = convert(decimal(13,1), isnull(sum(ProgB_ToPlant),0)),
	   ProgB_PlannedToPlant = convert(decimal(13,1), isnull(sum(ProgB_PlannedToPlant),0)),
	   DayB_Treated = convert(decimal(13,1), isnull(sum(DayB_Treated),0)),
	   DayB_PlannedTreated = convert(decimal(13,1), isnull(sum(DayB_PlannedTreated),0)),
	   ProgB_Treated = convert(decimal(13,1), isnull(sum(ProgB_Treated),0)),
	   ProgB_PlannedTreated = convert(decimal(13,1), isnull(sum(ProgB_PlannedTreated),0))
	from
	(
	select 
	   DayB_Treated = case when (b.CalendarDate = @CalendarDate)
							then b.TonsTreated else 0 end,
	   ProgB_Treated = case when (b.CalendarDate <= @CalendarDate) 
							then b.TonsTreated  else 0 end,
	   DayB_ToPlant = case when (b.CalendarDate = @CalendarDate)
							then b.TonsToPlant else 0 end,
	   ProgB_ToPlant = case when (b.CalendarDate <= @CalendarDate) 
							then b.TonsToPlant  else 0 end,
	   DayB_PlannedToPlant = case when (b.CalendarDate = @CalendarDate) 
							then b.PlannedTonsToPlant  else 0 end,
	   ProgB_PlannedToPlant = case when (b.CalendarDate <= @CalendarDate) 
							then b.PlannedTonsToPlant  else 0 end,
	   DayB_PlannedTreated = case when (b.CalendarDate = @CalendarDate) 
							then b.PlannedTonsTreated  else 0 end,
	   ProgB_PlannedTreated = case when (b.CalendarDate <= @CalendarDate) 
							then b.PlannedTonsTreated  else 0 end

	from BookingMilling b
	where b.CalendarDate >= @TheStartDate and
		  b.CalendarDate <= @CalendarDate
	and Oreflowid = @Mill	
	 ) a
	 END
END	 


go




create procedure [dbo].[sp_SICReport_KPI] --'MINEWARE', '2013/03/21' , '113', '4'
--declare
@UserID varchar(20),
@CalendarDate varchar(10),
@SectionID varchar(20),
@Section int

as
--set @UserID = 'mineware'
--set @CalendarDate = '2017-01-17'
--set @SectionID = 'FM'
--set @Section = '2'

declare @TheQuery varchar(8000)
declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)
declare @TheQuery3 varchar(8000)
declare @TheQuery4 varchar(8000)

declare @StartSafety varchar(10)
declare @StartCost varchar(10)
declare @StartMill varchar(10)
declare @StartProd varchar(10)
declare @EndSafety varchar(10)
declare @EndCost varchar(10)
declare @EndMill varchar(10)
declare @EndProd varchar(10)
declare @TheStartDate varchar(10)
declare @TheEndDate varchar (10)


SET NOCOUNT ON;

select 
	@TheStartDate = convert(varchar(10),min(a.StartDate),120),
	@TheEndDate = convert(varchar(10),max(a.EndDate),120)
from 
(
select 
  StartDate = case when Min(StartDate) is null then @CalendarDate else Min(StartDate) end,  
  EndDate = case when Max(EndDate) is null then @CalendarDate else Min(EndDate) end
from CALENDAROTHER
where StartDate< = @CalendarDate and
EndDate >= @CalendarDate and
calendarCode = 'Safety'
union
select 
  StartDate = case when Min(StartDate) is null then @CalendarDate else Min(StartDate) end,  
  EndDate = case when Max(EndDate) is null then @CalendarDate else Min(EndDate) end
from CALENDARMILL 
where StartDate< = @CalendarDate and
EndDate >= @CalendarDate
union
select 
  StartDate = case when Min(StartDate) is null then @CalendarDate else Min(StartDate) end,  
  EndDate = case when Max(EndDate) is null then @CalendarDate else Min(EndDate) end
from CALENDAROTHER
where StartDate <= @CalendarDate and
EndDate >= @CalendarDate and
CalendarCode = 'Costing'
union	
select 
a.StartDate,a.EndDate 
from
	(select StartDate = case when Min(BeginDate) is null then @CalendarDate else Min(BeginDate) end,  
            EndDate = case when Max(EndDate) is null then @CalendarDate else Min(EndDate) end
		from seccal s 
		inner join Section_Complete sc on
		  sc.ProdMonth = s.ProdMonth and
		  sc.SectionID_1 = s.SectionID
		where s.BeginDate< = @CalendarDate and
			  s.EndDate >= @CalendarDate
	) a
) a	

select 
@StartSafety = convert(varchar(10),a.StartDate,120),
@EndSafety = convert(varchar(10),a.EndDate,120)
from
	(select 
		StartDate = case when Min(StartDate) is null then @CalendarDate else Min(StartDate) end,  
            EndDate = case when Max(EndDate) is null then @CalendarDate else Min(EndDate) end
		from CALENDAROTHER
		where StartDate< = @CalendarDate and
		EndDate >= @CalendarDate and
		CalendarCode = 'Safety'
	) a

select 
@StartMill = convert(varchar(10),a.StartDate,120),
@EndMill = convert(varchar(10),a.EndDate,120)
from
	(select 
		StartDate = case when Min(StartDate) is null then @CalendarDate else Min(StartDate) end,  
            EndDate = case when Max(EndDate) is null then @CalendarDate else Min(EndDate) end
		from CALENDARMILL 
		where StartDate< = @CalendarDate and
		EndDate >= @CalendarDate
	) a

select 
@StartCost = convert(varchar(10),a.StartDate,120),
@EndCost = convert(varchar(10),a.EndDate,120)
from
	(select 
		StartDate = case when Min(StartDate) is null then @CalendarDate else Min(StartDate) end,  
            EndDate = case when Max(EndDate) is null then @CalendarDate else Min(EndDate) end
		from CALENDAROTHER
		where StartDate< = @CalendarDate and
		EndDate >= @CalendarDate and
		CalendarCode = 'Costing'
	) a
	
select  
	@StartProd = convert(varchar(10),min(s.BeginDate),120),
	@EndProd = convert(varchar(10),max(s.EndDate),120)

	from seccal s 
	inner join Section_Complete sc on
	  sc.ProdMonth = s.ProdMonth and
	  sc.SectionID_1 = s.SectionID
	where s.BeginDate< = @CalendarDate and
		  s.EndDate >= @CalendarDate

IF (@StartProd is NULL) and
   (@StartMill is NULL) and
   (@StartCost is NULL) and
   (@StartSafety is NULL)	
begin


set @TheQuery = '
select 

[DaB StopeDressings] = Convert(decimal(10,1),0),
[B StopeDressings] = Convert(decimal(10,1),0),
[DaB DevDressings] = Convert(decimal(10,1),0),
[B DevDressings] = Convert(decimal(10,1),0),
[DaB SerDressings] = Convert(decimal(10,1),0),
[B SerDressings] = Convert(decimal(10,1),0),
[DaB TotDressings] = Convert(decimal(10,1),0),
[B TotDressings] = Convert(decimal(10,1),0),
[DaB StopeLTI] = Convert(decimal(10,1),0),
[B StopeLTI]  = Convert(decimal(10,1),0),
[DaB DevLTI] = Convert(decimal(10,1),0),
[B DevLTI] = Convert(decimal(10,1),0),
[DaB SerLTI]  = Convert(decimal(10,1), 0),
[B SerLTI] = convert(decimal(10,1), 0),
[DaB TotLTI] = convert(decimal(10,1), 0),
[B TotLTI] = convert(decimal(10,1), 0),
[DaB StopeReportables] = convert(decimal(10,1), 0),
[B StopeReportables] = convert(decimal(10,1), 0),
[DaB DevReportables]  = convert(decimal(10,1), 0),
[B DevReportables] = convert(decimal(10,1), 0),
[DaB SerReportables]  = convert(decimal(10,1), 0), 
[B SerReportables] = convert(decimal(10,1), 0),
[DaB TotReportables] = convert(decimal(10,1), 0),
[B TotReportables]  = convert(decimal(10,1), 0),
[DaB StopeFOG] = convert(decimal(10,1), 0),
[B StopeFOG] = convert(decimal(10,1), 0),
[DaB DevFOG] = convert(decimal(10,1), 0),
[B DevFOG] = convert(decimal(10,1), 0),
[DaB SerFOG] = convert(decimal(10,1), 0),
[B SerFOG] = convert(decimal(10,1), 0),
[DaB TotFOG] = convert(decimal(10,1), 0),
[B TotFOG] = convert(decimal(10,1), 0),
[DaB StopeRBE] = convert(decimal(10,1), 0),
[B StopeRBE] = convert(decimal(10,1), 0),
[DaB DevRBE] = convert(decimal(10,1), 0),
[B DevRBE] = convert(decimal(10,1), 0),
[DaB SerRBE] = convert(decimal(10,1), 0), 
[B SerRBE]  = convert(decimal(10,1), 0),
[DaB TotRBE] = convert(decimal(10,1), 0),
[B TotRBE] = convert(decimal(10,1), 0),

[DaB StopeAHazards] = convert(decimal(10,1), 0),
[B StopeAHazards] = convert(decimal(10,1), 0),
[DaB DevAHazards] = convert(decimal(10,1), 0),
[B DevAHazards] = convert(decimal(10,1), 0),
[DaB SerAHazards] = convert(decimal(10,1), 0),
[B SerAHazards] = convert(decimal(10,1), 0),
[DaB TotAHazards] = convert(decimal(10,1), 0),
[B TotAHazards] = convert(decimal(10,1), 0),
[DaB StopeWhiteFlag] = convert(decimal(10,1), 0),
[B StopeWhiteFlag] = convert(decimal(10,1), 0),
[DaB DevWhiteFlag] = convert(decimal(10,1), 0),
[B DevWhiteFlag]  = convert(decimal(10,1), 0),
[DaB SerWhiteFlag]  = convert(decimal(10,1), 0),
[B SerWhiteFlag] = convert(decimal(10,1), 0),
[DaB TotWhiteFlag] = convert(decimal(10,1), 0),
[B TotWhiteFlag] = convert(decimal(10,1), 0),
[Da StopeComplement] = convert(decimal(10,1), 0),
[DaB StopeComplement] = convert(decimal(10,1), 0),
[Va StopeComplement] = convert(decimal(10,1), 0),
[Da DevComplement] = convert(decimal(10,1), 0),
[DaB DevComplement] = convert(decimal(10,1), 0),
[Va DevComplement] = convert(decimal(10,1), 0),

[Da StopeAtWork] = convert(decimal(10,1), 0),
[DaB StopeAtWork] = convert(decimal(10,1), 0),
[Va StopeAtWork] = convert(decimal(10,1), 0),
[Da DevAtWork] = convert(decimal(10,1), 0),
[DaB DevAtWork] = convert(decimal(10,1), 0),
[Va DevAtWork] = convert(decimal(10,1), 0),

[Da StopeUnavailables] = convert(decimal(10,1), 0),
[DaB StopeUnavailables] = convert(decimal(10,1), 0),
[Va StopeUnavailables]  = convert(decimal(10,1), 0), 
[Da DevUnavailables] = convert(decimal(10,1), 0),
[DaB DevUnavailables]  = convert(decimal(10,1), 0),
[Va DevUnavailables]  = convert(decimal(10,1), 0),
[Da StopePerc] = convert(decimal(10,0), 0),
[DaB StopePerc] = convert(decimal(10,0), 0),
[Va StopePerc] = convert(decimal(10,0), 0),
[Da DevPerc] = convert(decimal(10,0), 0),
[DaB DevPerc] = convert(decimal(10,0), 0),
[Va DevPerc] = convert(decimal(10,0), 0),

[Da Breakdown] = convert(decimal(10,1), 0),
[DaB Breakdown] = convert(decimal(10,1), 0),
[D Breakdown] = convert(decimal(10,1), 0),
[B Breakdown]  = convert(decimal(10,1), 0),
[V Breakdown] = convert(decimal(10,1), 0),
[Va Breakdown] = convert(decimal(10,1), 0),

[D Total] = convert(decimal(10,1), 0),
[B Total] = convert(decimal(10,1), 0),
[V Total] = convert(decimal(10,1), 0),
[F Total] = convert(decimal(10,1), 0),

[D Explosives] = convert(decimal(10,1), 0),
[B Explosives] = convert(decimal(10,1), 0),
[V Explosives] = convert(decimal(10,1), 0),
[F Explosives]  = convert(decimal(10,1), 0),

[D Timber] = convert(decimal(10,1), 0),
[B Timber]  = convert(decimal(10,1), 0), 
[V Timber] = convert(decimal(10,1), 0),
[F Timber]  = convert(decimal(10,1), 0),

[D Overtime] = convert(decimal(10,1), 0),
[B Overtime] = convert(decimal(10,1), 0),
[V Overtime] = convert(decimal(10,1), 0),
[F Overtime]  = convert(decimal(10,1), 0),

[D Utilities]  = convert(decimal(10,1), 0),
[B Utilities] = convert(decimal(10,1), 0),
[V Utilities]  = convert(decimal(10,1), 0),
[F Utilities] = convert(decimal(10,1), 0),

[D Stores] = convert(decimal(10,1), 0),
[B Stores]  = convert(decimal(10,1), 0),
[V Stores]  = convert(decimal(10,1), 0),
[F Stores] = convert(decimal(10,1), 0),

[D Callout] = convert(decimal(10,1), 0), 
[B Callout]  = convert(decimal(10,1), 0),
[V Callout]  = convert(decimal(10,1), 0),
[F Callout]  = convert(decimal(10,1), 0), 

[D Abnormal] = convert(decimal(10,1), 0),
[B Abnormal]  = convert(decimal(10,1), 0),
[V Abnormal]  = convert(decimal(10,1), 0), 
[F Abnormal]  = convert(decimal(10,1), 0),

[Da Available] = convert(decimal(10,1), 0),
[DaB Available] = convert(decimal(10,1), 0),
[D Available]  = convert(decimal(10,1), 0),
[B Available] = convert(decimal(10,1), 0),
[V Available] = convert(decimal(10,1), 0),
[Va Available]  = convert(decimal(10,1), 0),
[F Available] = convert(decimal(10,1), 0),

[Da TonnesPlaced] = convert(decimal(10,1), 0),
[DaB TonnesPlaced] = convert(decimal(10,1), 0),
[D TonnesPlaced]  = convert(decimal(10,1), 0),
[B TonnesPlaced] = convert(decimal(10,1), 0),
[V TonnesPlaced] = convert(decimal(10,1), 0),
[Va TonnesPlaced]  = convert(decimal(10,1), 0),
[F TonnesPlaced]  = convert(decimal(10,1), 0),

[Da PanelsPlaced] = convert(decimal(10,1), 0),
[DaB PanelsPlaced] = convert(decimal(10,1), 0),
[D PanelsPlaced]  = convert(decimal(10,1), 0),
[B PanelsPlaced] = convert(decimal(10,1), 0),
[V PanelsPlaced]  = convert(decimal(10,1), 0),
[Va PanelsPlaced]  = convert(decimal(10,1), 0),
[F PanelsPlaced] = convert(decimal(10,1), 0),

[DaB Permability] = convert(decimal(10,1), 0),
[B Permability] = convert(decimal(10,1), 0),
[DaB RD] = convert(decimal(10,1), 0),
[B RD] = convert(decimal(10,1), 0)
'
--select @TheQuery

exec (@TheQuery)
end
else
Begin

set @TheQuery = '
  
select 

[DaB StopeDressings] = Convert(decimal(10,1),sum(DaB_StopeDressings)), 
[B StopeDressings] = Convert(decimal(10,1),sum(B_StopeDressings)), 
[DaB DevDressings] = Convert(decimal(10,1),sum(DaB_DevDressings)), 
[B DevDressings] = Convert(decimal(10,1),sum(B_DevDressings)), 
[DaB SerDressings] = Convert(decimal(10,1),sum(DaB_SerDressings)), 
[B SerDressings] = Convert(decimal(10,1),sum(B_SerDressings)), 
[DaB TotDressings] = Convert(decimal(10,1),sum(DaB_StopeDressings) + sum(DaB_DevDressings) + sum(DaB_SerDressings)),
[B TotDressings] = Convert(decimal(10,1),sum(B_StopeDressings) + sum(B_DevDressings) + sum(B_SerDressings)),

[DaB StopeLTI] = Convert(decimal(10,1),sum(DaB_StopeLTI)), 
[B StopeLTI] = Convert(decimal(10,1),sum(B_StopeLTI)), 
[DaB DevLTI] = Convert(decimal(10,1),sum(DaB_DevLTI)), 
[B DevLTI] = Convert(decimal(10,1),sum(B_DevLTI)), 
[DaB SerLTI] = Convert(decimal(10,1),sum(DaB_SerLTI)), 
[B SerLTI] = Convert(decimal(10,1),sum(B_SerLTI)), 
[DaB TotLTI] = Convert(decimal(10,1),sum(DaB_StopeLTI) + sum(DaB_DevLTI) + sum(DaB_SerLTI)),
[B TotLTI] = Convert(decimal(10,1),sum(B_StopeLTI) + sum(B_DevLTI) + sum(B_SerLTI)),

[DaB StopeReportables] = Convert(decimal(10,1),sum(DaB_StopeReportables)), 
[B StopeReportables] = Convert(decimal(10,1),sum(B_StopeReportables)), 
[DaB DevReportables] = Convert(decimal(10,1),sum(DaB_DevReportables)), 
[B DevReportables] = Convert(decimal(10,1),sum(B_DevReportables)), 
[DaB SerReportables] = Convert(decimal(10,1),sum(DaB_SerReportables)), 
[B SerReportables] = Convert(decimal(10,1),sum(B_SerReportables)), 
[DaB TotReportables] = Convert(decimal(10,1),sum(DaB_StopeReportables) + sum(DaB_DevReportables) + sum(DaB_SerReportables)),
[B TotReportables] = Convert(decimal(10,1),sum(B_StopeReportables) + sum(B_DevReportables) + sum(B_SerReportables)),

[DaB StopeFOG] = Convert(decimal(10,1),sum(DaB_StopeFOG)), 
[B StopeFOG] = Convert(decimal(10,1),sum(B_StopeFOG)), 
[DaB DevFOG] = Convert(decimal(10,1),sum(DaB_DevFOG)), 
[B DevFOG] = Convert(decimal(10,1),sum(B_DevFOG)), 
[DaB SerFOG] = Convert(decimal(10,1),sum(DaB_SerFOG)), 
[B SerFOG] = Convert(decimal(10,1),sum(B_SerFOG)), 
[DaB TotFOG] = Convert(decimal(10,1),sum(DaB_StopeFOG) + sum(DaB_DevFOG) + sum(DaB_SerFOG)),
[B TotFOG] = Convert(decimal(10,1),sum(B_StopeFOG) + sum(B_DevFOG) + sum(B_SerFOG)),

[DaB StopeRBE] = Convert(decimal(10,1),sum(DaB_StopeRBE)), 
[B StopeRBE] = Convert(decimal(10,1),sum(B_StopeRBE)), 
[DaB DevRBE] = Convert(decimal(10,1),sum(DaB_DevRBE)), 
[B DevRBE] = Convert(decimal(10,1),sum(B_DevRBE)), 
[DaB SerRBE] = Convert(decimal(10,1),sum(DaB_SerRBE)), 
[B SerRBE] = Convert(decimal(10,1),sum(B_SerRBE)), 
[DaB TotRBE] = Convert(decimal(10,1),sum(DaB_StopeRBE) + sum(DaB_DevRBE) + sum(DaB_SerRBE)),
[B TotRBE] = Convert(decimal(10,1),sum(B_StopeRBE) + sum(B_DevRBE) + sum(B_SerRBE)),

[DaB StopeAHazards] = Convert(decimal(10,1),sum(DaB_StopeAHazards)), 
[B StopeAHazards] = Convert(decimal(10,1),sum(B_StopeAHazards)), 
[DaB DevAHazards] = Convert(decimal(10,1),sum(DaB_DevAHazards)), 
[B DevAHazards] = Convert(decimal(10,1),sum(B_DevAHazards)), 
[DaB SerAHazards] = Convert(decimal(10,1),sum(DaB_SerAHazards)), 
[B SerAHazards] = Convert(decimal(10,1),sum(B_SerAHazards)), 
[DaB TotAHazards] = Convert(decimal(10,1),sum(DaB_StopeAHazards) + sum(DaB_DevAHazards) + sum(DaB_SerAHazards)),
[B TotAHazards] = Convert(decimal(10,1),sum(B_StopeAHazards) + sum(B_DevAHazards) + sum(B_SerAHazards)),

[DaB StopeWhiteFlag] = Convert(decimal(10,1),min(isnull(DaB_StopeWhiteFlag,0))), 
[B StopeWhiteFlag] = Convert(decimal(10,1),min(isnull(B_StopeWhiteFlag,0))), 
[DaB DevWhiteFlag] = Convert(decimal(10,1),min(isnull(DaB_DevWhiteFlag,0))), 
[B DevWhiteFlag] = Convert(decimal(10,1),min(isnull(B_DevWhiteFlag,0))), 
[DaB SerWhiteFlag] = Convert(decimal(10,1),min(isnull(DaB_SerWhiteFlag,0))), 
[B SerWhiteFlag] = Convert(decimal(10,1),min(isnull(B_SerWhiteFlag,0))), 
[DaB TotWhiteFlag] = case when min(isnull(DaB_StopeWhiteFlag,1000)) <= min(isnull(DaB_DevWhiteFlag,1000)) then
						  case when min(isnull(DaB_StopeWhiteFlag,1000)) <= min(isnull(DaB_SerWhiteFlag,1000))
							   then min(isnull(DaB_StopeWhiteFlag,0))
						  else
							  case when min(isnull(DaB_DevWhiteFlag,1000)) <= min(isnull(DaB_SerWhiteFlag,1000))
							       then min(isnull(DaB_DevWhiteFlag,0))
							  else
								   min(isnull(DaB_SerWhiteFlag,0))
							  end
						  end
					 else
						case when min(isnull(DaB_DevWhiteFlag,1000)) <= min(isnull(DaB_SerWhiteFlag,1000))
							 then min(isnull(DaB_DevWhiteFlag,0))
						else
							min(isnull(DaB_SerWhiteFlag,0))
						end	
					 end,		 	    	      
[B TotWhiteFlag] = case when min(isnull(B_StopeWhiteFlag,1000)) <= min(isnull(B_DevWhiteFlag,1000)) then
						case when min(isnull(B_StopeWhiteFlag,1000)) <= min(isnull(B_SerWhiteFlag,1000))
						    then min(isnull(B_StopeWhiteFlag,0))
						else
							case when min(isnull(B_DevWhiteFlag,1000)) <= min(isnull(B_SerWhiteFlag,1000))
							     then min(isnull(B_DevWhiteFlag,0))
							else
								 min(isnull(B_SerWhiteFlag,0))
							end
						end
				   else
						case when min(isnull(B_DevWhiteFlag,1000)) <= min(isnull(B_SerWhiteFlag,1000))
							 then min(isnull(B_DevWhiteFlag,0))
						else
							min(isnull(B_SerWhiteFlag,0))
						end	
				  end,
[Da StopeComplement] = Convert(decimal(10,1),sum(Da_StopeComplement)), 
[DaB StopeComplement] = Convert(decimal(10,1),sum(DaB_StopeComplement)), 
[Va StopeComplement] = Convert(decimal(10,1),sum(DaB_StopeComplement) - sum(Da_StopeComplement)),
[Da DevComplement] = Convert(decimal(10,1),sum(Da_DevComplement)), 
[DaB DevComplement] = Convert(decimal(10,1),sum(DaB_DevComplement)), 
[Va DevComplement] = Convert(decimal(10,1),sum(DaB_DevComplement) - sum(Da_DevComplement)),

[Da StopeAtWork] = Convert(decimal(10,1),sum(Da_StopeAtWork)), 
[DaB StopeAtWork] = Convert(decimal(10,1),sum(DaB_StopeAtWork)), 
[Va StopeAtWork] = Convert(decimal(10,1),sum(DaB_StopeAtWork) - sum(Da_StopeAtWork)),
[Da DevAtWork] = Convert(decimal(10,1),sum(Da_DevAtWork)), 
[DaB DevAtWork] = Convert(decimal(10,1),sum(DaB_DevAtWork)), 
[Va DevAtWork] = Convert(decimal(10,1),sum(DaB_DevAtWork) - sum(Da_DevAtWork)),

[Da StopeUnavailables] = Convert(decimal(10,1),sum(Da_StopeUnavailables)), 
[DaB StopeUnavailables] = Convert(decimal(10,1),sum(DaB_StopeUnavailables)), 
[Va StopeUnavailables] = Convert(decimal(10,1),sum(DaB_StopeUnavailables) - sum(Da_StopeUnavailables)),
[Da DevUnavailables] = Convert(decimal(10,1),sum(Da_DevUnavailables)), 
[DaB DevUnavailables] = Convert(decimal(10,1),sum(DaB_DevUnavailables)), 
[Va DevUnavailables] = Convert(decimal(10,1),sum(DaB_DevUnavailables) - sum(Da_DevUnavailables)),

[Da StopePerc] = case when sum(Da_StopeComplement) <> 0 then Convert(decimal(10,0),sum(Da_StopeUnavailables) / sum(Da_StopeComplement) * 100) else 0 end, 
[DaB StopePerc] = case when sum(DaB_StopeComplement) <> 0 then Convert(decimal(10,0),sum(DaB_StopeUnavailables) / sum(DaB_StopeComplement) * 100) else 0 end, 
[Va StopePerc] = case when (sum(DaB_StopeComplement) - sum(Da_StopeComplement)) <> 0 then 
Convert(decimal(10,0),(sum(DaB_StopeUnavailables) - sum(Da_StopeUnavailables)) / (sum(DaB_StopeComplement) - sum(Da_StopeComplement)) * 100) else 0 end, 
[Da DevPerc] = case when sum(Da_DevComplement) <> 0 then Convert(decimal(10,0),sum(Da_DevUnavailables) / sum(Da_DevComplement) * 100) else 0 end, 
[DaB DevPerc] = case when sum(DaB_DevComplement) <> 0 then Convert(decimal(10,0),sum(DaB_DevUnavailables) / sum(DaB_DevComplement) * 100) else 0 end, 
[Va DevPerc] = case when (sum(DaB_DevComplement) - sum(Da_DevComplement)) <> 0 then 
Convert(decimal(10,0),(sum(DaB_DevUnavailables) - sum(Da_DevUnavailables)) / (sum(DaB_DevComplement) - sum(Da_DevComplement)) * 100) else 0 end, 
'
 set @TheQuery1 = '
[Da Breakdown] = Convert(decimal(10,1),sum(Da_Breakdown)), 
[DaB Breakdown] = Convert(decimal(10,1),sum(DaB_Breakdown)), 
[D Breakdown] = Convert(decimal(10,1),sum(D_Breakdown)), 
[B Breakdown] = Convert(decimal(10,1),sum(B_Breakdown)), 
[V Breakdown] = Convert(decimal(10,1),sum(B_Breakdown) - sum(D_Breakdown)),
[Va Breakdown] = Convert(decimal(10,1),sum(DaB_Breakdown) - sum(Da_Breakdown)),

[D Total] = Convert(Numeric(10,2),sum(D_Explosives) + sum(D_Timber) + sum(D_Overtime)), 
[B Total] = Convert(Numeric(10,2),sum(B_Explosives) + sum(B_Timber) + sum(B_Overtime)), 
[V Total] = Convert(Numeric(10,2),sum(B_Explosives) + sum(B_Timber) + sum(B_Overtime) - (sum(D_Explosives) + sum(D_Timber) + sum(D_Overtime))), 
[F Total] = Convert(decimal(10,1),sum(B_Explosives) + sum(B_Timber) + sum(B_Overtime) + sum(F_Explosives) + sum(F_Timber) + sum(F_Overtime)), 

[D Explosives] = Convert(Numeric(10,2),sum(D_Explosives)), 
[B Explosives] = Convert(Numeric(10,2),sum(B_Explosives)), 
[V Explosives] = Convert(Numeric(10,2),sum(B_Explosives) - sum(D_Explosives)), 
[F Explosives] = Convert(decimal(10,1),sum(B_Explosives + F_Explosives)), 

[D Timber] = Convert(Numeric(10,2),sum(D_Timber)), 
[B Timber] = Convert(Numeric(10,2),sum(B_Timber)), 
[V Timber] = Convert(Numeric(10,2),sum(B_Timber) - sum(D_Timber)), 
[F Timber] = Convert(decimal(10,1),sum(B_Timber + F_Timber)),

[D Overtime] = Convert(Numeric(10,2),sum(D_Overtime)), 
[B Overtime] = Convert(Numeric(10,2),sum(B_Overtime)), 
[V Overtime] = Convert(Numeric(10,2),sum(B_Overtime) - sum(D_Overtime)), 
[F Overtime] = Convert(decimal(10,1),sum(B_Overtime + F_Overtime)), 

[D Utilities] = Convert(Numeric(10,2),sum(D_Utilities)), 
[B Utilities] = Convert(Numeric(10,2),sum(B_Utilities)), 
[V Utilities] = Convert(Numeric(10,2),sum(B_Utilities) - sum(D_Utilities)), 
[F Utilities] = Convert(decimal(10,1),sum(B_Utilities + F_Utilities)), 

[D Stores] = Convert(Numeric(10,2),sum(D_Stores)), 
[B Stores] = Convert(Numeric(10,2),sum(B_Stores)), 
[V Stores] = Convert(Numeric(10,2),sum(B_Stores) - sum(D_Stores)), 
[F Stores] = Convert(decimal(10,1),sum(B_Stores + F_Stores)), 

[D Callout] = Convert(Numeric(10,2),sum(D_Callout)), 
[B Callout] = Convert(Numeric(10,2),sum(B_Callout)), 
[V Callout] = Convert(Numeric(10,2),sum(B_Callout) - sum(D_Callout)), 
[F Callout] = Convert(decimal(10,1),sum(B_Callout + F_Callout)), 

[D Abnormal] = Convert(Numeric(10,2),sum(D_Abnormal)), 
[B Abnormal] = Convert(Numeric(10,2),sum(B_Abnormal)), 
[V Abnormal] = Convert(Numeric(10,2),sum(B_Abnormal) - sum(D_Abnormal)), 
[F Abnormal] = Convert(decimal(10,1),sum(B_Abnormal + F_Abnormal)), 

[Da Available] = Convert(decimal(10,1),sum(Da_Available)), 
[DaB Available] = Convert(decimal(10,1),sum(DaB_Available)), 
[D Available] = Convert(decimal(10,1),sum(D_Available)), 
[B Available] = Convert(decimal(10,1),sum(B_Available)), 
[V Available] = Convert(decimal(10,1),sum(B_Available) - sum(D_Available)),
[Va Available] = Convert(decimal(10,1),sum(DaB_Available) - sum(Da_Available)),
[F Available] = Convert(decimal(10,1),sum(B_Available + F_Available)), 

[Da TonnesPlaced] = Convert(decimal(10,1),sum(Da_TonnesPlaced)), 
[DaB TonnesPlaced] = Convert(decimal(10,1),sum(DaB_TonnesPlaced)), 
[D TonnesPlaced] = Convert(decimal(10,1),sum(D_TonnesPlaced)), 
[B TonnesPlaced] = Convert(decimal(10,1),sum(B_TonnesPlaced)), 
[V TonnesPlaced] = Convert(decimal(10,1),sum(B_TonnesPlaced) - sum(D_TonnesPlaced)),
[Va TonnesPlaced] = Convert(decimal(10,1),sum(DaB_TonnesPlaced) - sum(Da_TonnesPlaced)),
[F TonnesPlaced] = Convert(decimal(10,1),sum(B_TonnesPlaced + F_TonnesPlaced)), 

[Da PanelsPlaced] = Convert(decimal(10,1),sum(Da_PanelsPlaced)), 
[DaB PanelsPlaced] = Convert(decimal(10,1),sum(DaB_PanelsPlaced)), 
[D PanelsPlaced] = Convert(decimal(10,1),sum(D_PanelsPlaced)), 
[B PanelsPlaced] = Convert(decimal(10,1),sum(B_PanelsPlaced)), 
[V PanelsPlaced] = Convert(decimal(10,1),sum(B_PanelsPlaced) - sum(D_PanelsPlaced)),
[Va PanelsPlaced] = Convert(decimal(10,1),sum(DaB_PanelsPlaced) - sum(Da_PanelsPlaced)),
[F PanelsPlaced] = Convert(decimal(10,1),sum(B_PanelsPlaced + F_PanelsPlaced)),

[DaB Permability] = Convert(decimal(10,1),sum(DaB_Permability)), 
[B Permability] = Convert(decimal(10,1),sum(B_Permability)), 

[DaB RD] = Convert(decimal(10,2),sum(DaB_RD)), 
[B RD] = Convert(decimal(10,2),sum(B_RD))
 

    from  '                                                                   
set @TheQuery2 =  '
(select                                                                       
DaB_StopeDressings = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 1) then c.value else 0 end),
B_StopeDressings = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 1) then c.value else 0 end),
DaB_DevDressings = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 8) then c.value else 0 end),
B_DevDressings = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 8) then c.value else 0 end),
DaB_SerDressings = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 15) then c.value else 0 end),
B_SerDressings = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 15) then c.value else 0 end),
DaB_StopeLTI = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 2) then c.value else 0 end),
B_StopeLTI = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 2) then c.value else 0 end),
DaB_DevLTI = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 9) then c.value else 0 end),
B_DevLTI = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 9) then c.value else 0 end),
DaB_SerLTI = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 16) then c.value else 0 end),
B_SerLTI = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 16) then c.value else 0 end), 
DaB_StopeReportables = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 3) then c.value else 0 end),
B_StopeReportables = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 3) then c.value else 0 end),
DaB_DevReportables = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 10) then c.value else 0 end),
B_DevReportables = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 10) then c.value else 0 end),
DaB_SerReportables = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 17) then c.value else 0 end),
B_SerReportables = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 17) then c.value else 0 end),
DaB_StopeFOG = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 4) then c.value else 0 end),
B_StopeFOG = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 4) then c.value else 0 end),
DaB_DevFOG = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 11) then c.value else 0 end),
B_DevFOG = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 11) then c.value else 0 end),
DaB_SerFOG = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 18) then c.value else 0 end),
B_SerFOG = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 18) then c.value else 0 end),
DaB_StopeRBE = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 5) then c.value else 0 end),
B_StopeRBE = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 5) then c.value else 0 end),
DaB_DevRBE = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 12) then c.value else 0 end),
B_DevRBE = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 12) then c.value else 0 end),
DaB_SerRBE = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 19) then c.value else 0 end),
B_SerRBE = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 19) then c.value else 0 end),
DaB_StopeAHazards = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 6) then c.value else 0 end),
B_StopeAHazards = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 6) then c.value else 0 end),
DaB_DevAHazards = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 13) then c.value else 0 end),
B_DevAHazards = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 13) then c.value else 0 end),
DaB_SerAHazards = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 20) then c.value else 0 end),
B_SerAHazards = sum(case when (c.CalendarDate >= '''+@StartSafety+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 20) then c.value else 0 end),
DaB_StopeWhiteFlag = min(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 7) and c.value > 0 then 1 
	else case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 7) and c.value = 0 then 0 else null end end),
B_StopeWhiteFlag = min(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 7) then c.value else null end),
DaB_DevWhiteFlag = min(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 14) and c.value > 0 then 1 
	else case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 14) and c.value = 0 then 0 else null end end),
B_DevWhiteFlag = min(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 14) then c.value else null end),
DaB_SerWhiteFlag = min(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 21) and c.value > 0 then 1 
	else case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 21) and c.value = 0 then 0 else null end end),
B_SerWhiteFlag = min(case when (c.CalendarDate = '''+@CalendarDate+''') and c.SICKey = 21 then c.value else null end),
TotB_WhiteFlag = min(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey in (7,14,21)) and c.value > 0 then 1 
	else case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey in (7,14,21)) and c.value = 0 then 0 else null end end), 
Tot_WhiteFlag = min(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey in (7,14,21)) then c.value else null end),
Da_StopeComplement = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 24) then c.value else 0 end),
DaB_StopeComplement = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 25) then c.value else 0 end),
Da_DevComplement = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 30) then c.value else 0 end),
DaB_DevComplement = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 31) then c.value else 0 end),'
set @TheQuery3 ='	
Da_StopeAtWork = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 26) then c.value else 0 end),
DaB_StopeAtWork = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 27) then c.value else 0 end),
Da_DevAtWork = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 32) then c.value else 0 end),
DaB_DevAtWork = sum(case when  (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 33) then c.value else 0 end),
Da_StopeUnavailables = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 28) then c.value else 0 end),
DaB_StopeUnavailables = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 29) then c.value else 0 end),
Da_DevUnavailables = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 34) then c.value else 0 end),
DaB_DevUnavailables = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 35) then c.value else 0 end),
Da_Breakdown = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 36) then c.value else 0 end),
DaB_Breakdown = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 37) then c.value else 0 end),
D_Breakdown = sum(case when (c.CalendarDate >= t.StartDate) and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 36) then c.value else 0 end),
B_Breakdown = sum(case when (c.CalendarDate >= t.StartDate) and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 37) then c.value else 0 end),
D_Explosives = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 38) then c.value else 0 end),
B_Explosives = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 39) then c.value else 0 end),
F_Explosives = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndCost+''') and 
	(c.SICKey = 38) then c.value else 0 end), 
D_Timber = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 40) then c.value else 0 end),
B_Timber = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 41) then c.value else 0 end),
F_Timber = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndCost+''') and 
	(c.SICKey = 40) then c.value else 0 end),
D_Overtime = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 42) then c.value else 0 end),
B_Overtime = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and		
	(c.SICKey = 43) then c.value else 0 end),
F_Overtime = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndCost+''') and 
	(c.SICKey = 42) then c.value else 0 end),
D_Utilities = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and	
	(c.SICKey = 44) then c.value else 0 end),
B_Utilities = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and	
	(c.SICKey = 45) then c.value else 0 end),
F_Utilities = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndCost+''') and 
	(c.SICKey = 44) then c.value else 0 end),
D_Stores = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and	
	(c.SICKey = 46) then c.value else 0 end),
B_Stores = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and	
	(c.SICKey = 47) then c.value else 0 end),
F_Stores = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndCost+''') and 
	(c.SICKey = 46) then c.value else 0 end),
D_Callout = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and	
	(c.SICKey = 48) then c.value else 0 end),
B_Callout = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and	
	(c.SICKey = 49) then c.value else 0 end),
F_Callout = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndCost+''') and 
	(c.SICKey = 48) then c.value else 0 end),
D_Abnormal = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 50) then c.value else 0 end),
B_Abnormal = sum(case when (c.CalendarDate >= '''+@StartCost+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 51) then c.value else 0 end),
F_Abnormal = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndCost+''') and 
	(c.SICKey = 50) then c.value else 0 end),
D_Available = sum(case when (c.CalendarDate >= '''+@StartMill+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 52) then c.value else 0 end),
B_Available = sum(case when (c.CalendarDate >= '''+@StartMill+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 53) then c.value else 0 end),
Da_Available = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 52) then c.value else 0 end),
DaB_Available = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 53) then c.value else 0 end),
F_Available = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndMill+''') and 
	(c.SICKey = 52) then c.value else 0 end),
D_TonnesPlaced = sum(case when (c.CalendarDate >= '''+@StartMill+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 54) then c.value else 0 end),
B_TonnesPlaced = sum(case when (c.CalendarDate >= '''+@StartMill+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 55) then c.value else 0 end),
Da_TonnesPlaced = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 54) then c.value else 0 end),
DaB_TonnesPlaced = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 55) then c.value else 0 end),
F_TonnesPlaced = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndMill+''') and 
	(c.SICKey = 54) then c.value else 0 end),
D_PanelsPlaced = sum(case when (c.CalendarDate >= '''+@StartMill+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 56) then c.value else 0 end),
B_PanelsPlaced = sum(case when (c.CalendarDate >= '''+@StartMill+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 57) then c.value else 0 end),
Da_PanelsPlaced = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 56) then c.value else 0 end),
DaB_PanelsPlaced = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 57) then c.value else 0 end),
F_PanelsPlaced = sum(case when (c.CalendarDate > '''+@CalendarDate+''') and (c.CalendarDate <= '''+@EndMill+''') and 
	(c.SICKey = 56) then c.value else 0 end),
B_Permability = sum(case when (c.CalendarDate >= '''+@StartMill+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 58) then c.value else 0 end),
DaB_Permability = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 58) then c.value else 0 end),
B_RD = sum(case when (c.CalendarDate >= '''+@StartMill+''') and (c.CalendarDate <= '''+@CalendarDate+''') and 
	(c.SICKey = 59) then c.value else 0 end),
DaB_RD = sum(case when (c.CalendarDate = '''+@CalendarDate+''') and (c.SICKey = 59) then c.value else 0 end)
from SICCAPTURE c
left outer join Temp_MOStartDate t on
  t.SectionID = c.SectionID and
  t.UserID = '''+@UserID+'''
left outer join vw_Sections_Complete_MO sc on
  sc.ProdMonth = t.ProdMonth and
  sc.SectionID_2 = t.SectionID  
where c.CalendarDate >= '''+@TheStartDate+''' and 
      c.CalendarDate <= '''+@TheEndDate+''''
 
IF (@Section = 4)
BEGIN
   set @TheQuery3 = @TheQuery3 + ' and  c.SectionID = '''+@SectionID +''' '
END  
IF (@Section = 3)
BEGIN
   set @TheQuery3 = @TheQuery3 + ' and  sc.SectionID_3 = '''+@SectionID +''' '
END  
IF (@Section = 2)
BEGIN
   set @TheQuery3 = @TheQuery3 + ' and  sc.SectionID_4 = '''+@SectionID +''' '
END 
 
 set @TheQuery3 = @TheQuery3 + '      
     ) c '

--print @TheQuery
--print @TheQuery1
--print @TheQuery2
--print @TheQuery3

exec (@TheQuery+@TheQuery1+@TheQuery2+@TheQuery3)
end





go


create 
procedure [dbo].[sp_SICReport_Hoisting] 
--declare
@CalendarDate varchar(10),
@Section int

as
set @CalendarDate = '2017-03-01'
set @Section = 4

declare @TheStartDate varchar(10)

SET NOCOUNT ON;
  
select @TheStartDate = convert(varchar(10),a.StartDate,120)
	from
	(select min(cm.StartDate) StartDate 
		  from CODE_CALENDAR cc
		  inner join CALENDARMILL cm on
		    cm.CalendarCode = cc.CalendarCode
		  where cm.StartDate <= @CalendarDate and 
		  cm.EndDate >= @CalendarDate and
		  cc.Description = 'Mill Calendar' ) a

IF (@TheStartDate is NULL) 
BEGIN
	select
		DayP_HoistTons = convert(numeric(13,0), 0),
		DayB_HoistTons = convert(numeric(13,0), 0),
		DayV_HoistTons = convert(numeric(13,0), 0),
		ProgP_HoistTons = convert(numeric(13,0), 0),
		ProgB_HoistTons = convert(numeric(13,0), 0),
		ProgV_HoistTons = convert(numeric(13,0), 0),
		ProgF_HoistTons = convert(numeric(13,0), 0),
		DayP_HoistGold = convert(numeric(13,3), 0),
		DayB_HoistGold = convert(numeric(13,3), 0),
		DayV_HoistGold = convert(numeric(13,3), 0),
		ProgP_HoistGold = convert(numeric(13,3), 0),
		ProgB_HoistGold = convert(numeric(13,3), 0),
		ProgV_HoistGold = convert(numeric(13,3), 0),
		DayB_HoistReefTons = convert(numeric(13,0), 0),
		ProgB_HoistReefTons = convert(numeric(13,0), 0)
END
ELSE
BEGIN	
	IF (@Section = 4)	 
	BEGIN
		select
		DayP_HoistTons = convert(numeric(13,0), 0),
		DayB_HoistTons = convert(numeric(13,0), 0),
		DayV_HoistTons = convert(numeric(13,0), 0),
		ProgP_HoistTons = convert(numeric(13,0), 0),
		ProgB_HoistTons = convert(numeric(13,0), 0),
		ProgV_HoistTons = convert(numeric(13,0), 0),
		ProgF_HoistTons = convert(numeric(13,0), 0),
		DayP_HoistGold = convert(numeric(13,3), 0),
		DayB_HoistGold = convert(numeric(13,3), 0),
		DayV_HoistGold = convert(numeric(13,3), 0),
		ProgP_HoistGold = convert(numeric(13,3), 0),
		ProgB_HoistGold = convert(numeric(13,3), 0),
		ProgV_HoistGold = convert(numeric(13,3), 0),
		DayB_HoistReefTons = convert(numeric(13,0), 0),
		ProgB_HoistReefTons = convert(numeric(13,0), 0)
	END
	ELSE
	BEGIN 
		  
		select 
			DayP_HoistTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate = @CalendarDate)
								then b.PlanTons else 0 end),0)),
			DayB_HoistTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate = @CalendarDate)
								then b.ReefTons + b.WasteTons else 0 end),0)),
			DayV_HoistTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate = @CalendarDate)
								then (b.ReefTons + b.WasteTons) - b.PlanTons else 0 end),0)),					
			ProgP_HoistTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate <= @CalendarDate) 
								then b.PlanTons else 0 end),0)),					
			ProgB_HoistTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate <= @CalendarDate) 
								then b.ReefTons + b.WasteTons else 0 end),0)),
			ProgV_HoistTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate <= @CalendarDate) 
								then (b.ReefTons + b.WasteTons) - b.PlanTons else 0 end),0)),					
			ProgF_HoistTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate > @CalendarDate) 
								then (b.PlanTons) else 0 end) + sum(case when (b.CalendarDate <= @CalendarDate) 
								then (b.ReefTons + b.WasteTons) else 0 end),0)),
			DayP_HoistGold = convert(numeric(13,3), isnull(sum(case when (b.CalendarDate = @CalendarDate)
								then b.PlanGold else 0 end),0)),
			DayB_HoistGold = convert(numeric(13,3), isnull(sum(case when (b.CalendarDate = @CalendarDate)
								then b.Gold else 0 end),0)),
			DayV_HoistGold = convert(numeric(13,3), isnull(sum(case when (b.CalendarDate = @CalendarDate)
								then b.Gold - b.PlanGold else 0 end),0)),					
			ProgP_HoistGold = convert(numeric(13,3), isnull(sum(case when (b.CalendarDate <= @CalendarDate) 
								then b.PlanGold else 0 end),0)),					
			ProgB_HoistGold = convert(numeric(13,3), isnull(sum(case when (b.CalendarDate <= @CalendarDate) 
								then b.Gold else 0 end),0)),	
			ProgV_HoistGold = convert(numeric(13,3), isnull(sum(case when (b.CalendarDate <= @CalendarDate) 
								then b.Gold - b.PlanGold else 0 end),0)),						
			DayB_HoistReefTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate = @CalendarDate)
								then b.ReefTons else 0 end),0)),
			ProgB_HoistReefTons = convert(numeric(13,0), isnull(sum(case when (b.CalendarDate <= @CalendarDate) 
								then b.ReefTons else 0 end),0))											
		from BookingHoist b
		where b.CalendarDate >= @TheStartDate and
			  b.CalendarDate <= @CalendarDate
	END
END		  

go



create procedure [dbo].[sp_SICReport_Detail] --'MINEWARE', '2013/09/02', 'LORSTMO', 4
--declare
@UserID varchar(10),
@CalendarDate varchar(10),
@SectionID varchar(20),
@Section int

as

--set @UserID = 'MINEWARE'
--set @CalendarDate = '2017-01-17'
--set @SectionID = 'rea'
--set @Section = 4

declare @TheQuery varchar(8000)
declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)

set @TheQuery = '
select 
TheSection2, TheSection1, TheSection, TheWorkplace,
NightProb = max(isnull(NightProb,'''')), 
DayProb = max(isnull(DayProb,'''')),
                              
Shifts = max(isnull(Shifts,'''')),
TotalShifts = max(isnull(TotalShifts,'''')),
DevCheck = sum(DevCheck),
StopeCheck = sum(StopeCheck),

DM_m2 = Convert(numeric(13,0), sum(DM_m2)),
DM_FL = Convert(numeric(13,0), sum(DM_FL)),
DM_ReefSQMcmgt = Convert(numeric(13,0), sum(DM_ReefSQMcmgt)),
DM_reefm2 = Convert(numeric(13,0), sum(DM_reefm2)),
DM_cmgt = Convert(numeric(13,0), case when sum(DM_reefm2) > 0 
	then sum(DM_ReefSQMcmgt) / sum(DM_reefm2) else 0 end), 
      
Da_m2 = Convert(numeric(13,0), sum(Da_m2)),
DaB_Sqm =  Convert(numeric(13,0), sum(DaB_Sqm)),  
DaV_Sqm =  Convert(numeric(13,0), sum(DaB_Sqm) - sum(Da_m2)),  
D_m2 = Convert(numeric(13,0), sum(D_m2)),
B_Sqm =  Convert(numeric(13,0), sum(B_Sqm)),  
V_Sqm =  Convert(numeric(13,0), sum(B_Sqm) - sum(D_m2)),  
DaM_Sqm = Convert(numeric(13,0), 0), 
VM_Sqm =  Convert(numeric(13,0), 0 - sum(DaB_Sqm)), 
F_Sqm = Convert(numeric(13,0), sum(B_Sqm) + sum(F_Sqm)),  
	
Da_NightBlast = cast(sum(Da_NightBlast) as numeric(13,0)),
Da_NightClean = cast(sum(Da_NightClean) as numeric(13,0)),
 
DaB_Adv = Convert(numeric(13,1), case when sum(DaB_FL) > 0 then sum(DaB_Sqm) / sum(DaB_FL) else 0 end), 
B_Adv = Convert(numeric(13,1), case when sum(B_FL) > 0 then sum(B_Sqm) / sum(B_FL) else 0 end), 
B_FL = sum(B_FL),
DaB_FL = sum(DaB_FL),

DM_DevAdv = Convert(numeric(13,1), sum(DM_DevAdv)),
Da_DevAdv = Convert(numeric(13,1), sum(Da_DevAdv)),
DaB_DevAdv = Convert(numeric(13,1), sum(DaB_DevAdv)),
DaV_DevAdv = Convert(numeric(13,1), sum(DaB_DevAdv) - sum(Da_DevAdv)),
D_DevAdv = Convert(numeric(13,1), sum(D_DevAdv)),
B_DevAdv = Convert(numeric(13,1), sum(B_DevAdv)),
V_DevAdv = Convert(numeric(13,1), sum(B_DevAdv) - sum(D_DevAdv)),
F_DevAdv = Convert(numeric(13,1), sum(B_DevAdv) + sum(F_DevAdv)),
DaM_DevAdv = Convert(numeric(13,1), 0), 
VM_DevAdv =  Convert(numeric(13,1), 0 - sum(DaB_DevAdv)),  

Da_DevNightBlast = max(Da_DevNightBlast),
Da_DevNightClean = max(Da_DevNightClean)

from
(
select isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection2, --p.CalendarDate,
isnull(sc.SectionID_1 +'':''+sc.Name_1,'''') TheSection1,
isnull(sc.SectionID +'':''+sc.NAME,'''') TheSection,
isnull(p.workplaceid +'':''+ w.description,'''') TheWorkplace,
--p.workplaceid, p.prodmonth, p.sectionid, p.calendardate, w.description, 
NightProb = case when (p.CalendarDate = '''+@CalendarDate+''') then cl1.Problemcode else '''' end, 
DayProb = case when (p.CalendarDate = '''+@CalendarDate+''') then dp.Problemid +'':''+dp.Description else '''' end, 
                              
Shifts = case when (wd.calendardate = '''+@CalendarDate+''') then wd.shift else null end,
TotalShifts = case when (wd.calendardate = '''+@CalendarDate+''') then wd.TotalShifts else null end,
DevCheck = case when p.activity = 1 then 1 else 0 end,
StopeCheck = case when p.activity in (0,9) then 1 else 0 end,

DM_m2 = case when (p.activity in (0, 9)) then p.sqm else 0 end,
DM_ReefSQMcmgt = case when (p.activity in (0, 9))
	then pp.gt * p.reefsqm else 0 end,
DM_reefm2 = case when (p.activity in (0, 9)) then p.reefsqm else 0 end,   
DM_FL = case when (p.activity in (0, 9)) and (p.CalendarDate <= '''+@CalendarDate+''') and (p.sqm > 0) then pp.FL else 0 end, 
DM_cmgt = case when (p.activity in (0, 9)) then pp.gt else 0 end, 
      
Da_m2 = case when (p.activity in (0, 9)) and (p.CalendarDate = '''+@CalendarDate+''') 
	then p.sqm else 0 end,
DaB_Sqm =  case when (p.activity in (0, 9)) and (p.CalendarDate = '''+@CalendarDate+''') 
	then p.BookSqm else 0 end,     
D_m2 = case when (p.activity in (0, 9)) and (p.CalendarDate <= '''+@CalendarDate+''') 
	then p.sqm else 0 end, 
B_Sqm =  case when (p.activity in (0, 9)) and (p.CalendarDate <= '''+@CalendarDate+''')
	then p.BookSQM else 0 end,    
F_Sqm =  case when (p.activity in (0, 9)) and (p.CalendarDate > '''+@CalendarDate+''') 
	then p.SQM else 0 end,    	
	
Da_NightBlast = case when (p.activity in (0, 9)) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(cl.SICKey = 22) then cl.Value else 0 end,
Da_NightClean = case when (p.activity in (0, 9)) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(cl1.SICKey = 23) then cl1.Value else 0 end,
 
DaB_FL = case when (p.activity in (0, 9)) and (p.CalendarDate = '''+@CalendarDate+''') 
	then p.BookFL else 0 end,  
B_FL = case when (p.activity in (0, 9)) and (p.CalendarDate <= '''+@CalendarDate+''') 
	then p.BookFL else 0 end,'  

set @TheQuery1='
DM_DevAdv = case when (p.activity = 1) then p.MetresAdvance else 0 end,
Da_DevAdv = case when (p.activity = 1) and (p.CalendarDate = '''+@CalendarDate+''') 
	then p.MetresAdvance else 0 end,  
DaB_DevAdv = case when  (p.activity = 1) and (p.CalendarDate = '''+@CalendarDate+''')
	then p.BookMetresAdvance else 0 end,
D_DevAdv = case when (p.activity = 1) and (p.CalendarDate <= '''+@CalendarDate+''') 
	then p.MetresAdvance else 0 end,
B_DevAdv = case when  (p.activity = 1)  and   (p.CalendarDate <= '''+@CalendarDate+''')
	then p.BookMetresAdvance else 0 end,
F_DevAdv = case when  (p.activity = 1)  and   (p.CalendarDate > '''+@CalendarDate+''')
	then p.MetresAdvance else 0 end,

Da_DevNightBlast = case when (p.activity = 1) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(cl.SICKey = 22) and cl.Value = 99 then ''Yes'' else '''' end,
Da_DevNightClean = case when (p.activity = 1) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(cl1.SICKey = 23) and cl1.Value = 99 then ''Yes'' else '''' end       
from
  Planning p 
inner join workplace w on
p.workplaceid = w.workplaceid 
--left outer join problembook pb on
--p.workplaceid = pb.workplaceid and 
--	p.Prodmonth = pb.Prodmonth and  
--	p.SectionID = pb.SectionID and 
--	p.Activity = pb.Activity and   
--	p.CalendarDate = pb.CalendarDate
left outer join PlanMonth pp on
	p.workplaceid = pp.workplaceid and 
	p.Prodmonth = pp.Prodmonth and  
	p.SectionID = pp.SectionID and 
	p.Activity = pp.Activity 	
inner join section_complete sc on
  sc.sectionid = p.sectionid and 
  sc.prodmonth = p.prodmonth
--left outer join ProblemBook pbd on
--	p.workplaceid = pbd.workplaceid and 
--	p.Prodmonth = pbd.Prodmonth and  
--	p.SectionID = pbd.SectionID and 
--	p.Activity = pbd.Activity and
--	p.CalendarDate = pbd.CalendarDate and
--	pbd.Shift = ''D''
left outer join Code_Problem dp on
  dp.ProblemID = p.ProblemID	  
left outer join TempworkdaysMO wd on 
  wd.SectionID = sc.SectionID_2 and  
  wd.calendardate = '''+@CalendarDate+''' and
  wd.Userid = '''+@UserID+'''
left outer join SICCapture cl on
  cl.CalendarDate = p.CalendarDate and
  cl.SICKey in (22) and
  cl.SectionID = sc.SectionID_1 and
  cl.WorkplaceID = p.WorkplaceID 
 left outer join SICCapture cl1 on
  cl1.CalendarDate = p.CalendarDate and
  cl1.SICKey in (23) and
  cl1.SectionID = sc.SectionID_1 and
  cl1.WorkplaceID = p.WorkplaceID 
where --p.calendardate = '''+@CalendarDate+'''  and 
 p.ProdMonth = wd.ProdMonth' 
IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_2 = '''+@SectionID+''' '
END 
IF (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_3 = '''+@SectionID+''' '
END
IF (@Section = 2)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_4 = '''+@SectionID+''' '
END

set @TheQuery1 = @TheQuery1 + '
) c
group by TheSection2, TheSection1, TheSection, TheWorkplace
with Rollup'

--print @TheQuery
--print @TheQuery1

exec (@TheQuery+@TheQuery1)

go




create 
procedure [dbo].[sp_SurveyReport_SweepingsSum_Zeroes] 
as 
select  
     CleanTypes = '', 
     CleanSqm = 0,
     CleanTons = 0,
     CleanContents = 0,
     CleanDist = 0,
     StopeSqm  = 0,
     Sweepable  = 0,
     SqmOSFTotal = 0,
     SqmTotal = 0,
     SVPerc = 0,
     CurrIMLPerc = 0,
     BLMLPerc = 0,
     TotalMLPerc = 0,
     NAFS = 0,
     NTBS  = 0,
     CUR = 0,
     CURV = 0,
     BKL  = 0,
     BVM = 0
     

go


create 
procedure [dbo].[sp_SurveyReport_SweepingsSum] 
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheCleanType varchar(200),
@TheSectionID varchar (20),
@TheSelectBy varchar (100)

as

declare @TheQuery1 varchar(8000)
set @TheQuery1 = '
  select isnull(CleanTypes,'''') CleanTypes, 
    OGSqm = sum(OGSqm),
    OGMetres = sum (OGMetres),
    OGTons = sum (OGTons),
    OGContent = sum(OGContent),
    OGGT = case when sum(OGTons) > 0 then sum(OGContent) / sum(OGTons) else 0 end,
    CleanSqm = sum(CleanSqm), 
    CleanTons = sum(CleanTons), 
    CleanContents = sum(CleanContents),
    CleanDist = case when sum(CleanSqm) > 0 then sum(CleanDist) / sum(CleanSqm) else 0 end,
    StopeSqm = sum(SqmTotal) - sum(SqmOSFTotal), 
    Sweepable = sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) - sum(NTBS), 
    SqmOSFTotal =sum(SqmOSFTotal),
    SqmTotal = sum(SqmTotal),
    SVPerc = case when sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) = 0 then 0
                  else (sum(CleanSqm) - sum(NAFS)) / (sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS)) * 100 end,
    CurrIMLPerc = case when sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) = 0 then 0
                  else (sum(CUR) + sum(CURV)) / (sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS)) * 100 end,
    BLMLPerc = case when sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) = 0 then 0
                  else (sum(BKL) + sum(BVM)) / (sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS)) * 100 end,
    TotalMLPerc = case when sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) = 0 then 0
                  else (sum(CUR) + sum(CURV) + sum(BKL) + sum(BVM)) / 
                       (sum(SqmTotal)  - sum(SqmOSFTotal)- sum(NAFS)) * 100 end,				      			      	      
    NAFS = sum(NAFS), 
    NTBS = sum(NTBS), 
    CUR = sum(CUR), 
    CURV = sum(CURV), 
    BKL = sum(BKL), 
    BVM = sum(BVM), 
    PlanNo = max(PlanNo), 
    ReefDesc = max(ReefDesc),
    PrintLine = sum(PrintLine)
 from ( 
 select CleanTypes = cc.CleanTypeDesc,
    PrintLine = case when s.CleanType is null then 0 else 1 end,
	OGSqm = case when isnull(s.CleanType,0) = 13 then isnull(s.CleanSqm,0) else 0 end,
    OGMetres = case when isnull(s.CleanType,0) = 13 then isnull(s.CleanVamp,0) else 0 end,
    OGTons = case when isnull(s.CleanType,0) = 13 then isnull(s.CleanTons,0) else 0 end,
    OGContent = case when isnull(s.CleanType,0) = 13 then isnull(s.CleanContents,0) else 0 end,
    CleanSqm = case when isnull(s.CleanType,0) <> 13 then isnull(s.CleanSqm,0) else 0 end,
    CleanTons = case when isnull(s.CleanType,0) <> 13 then isnull(s.CleanTons,0) else 0 end,
    CleanContents = case when isnull(s.CleanType,0) <> 13 then isnull(s.CleanContents,0) else 0 end,
    CleanDist = case when isnull(s.CleanType,0) <> 13 then isnull(s.CleanDist,0) * isnull(s.CleanSqm,0) else 0 end,
    StopeSqm = case when isnull(s.CleanType,0) <> 13 then isnull(s.StopeSqm,0) else 0 end,
    SqmTotal = case when isnull(s.CleanType,0) <> 13 then isnull(s.SqmTotal,0) else 0 end,
    SqmOSFTotal = case when isnull(s.CleanType,0) <> 13 then isnull(s.SqmOSFTotal,0) else 0 end,
    NAFS = case when isnull(s.CleanType,0) = 4 then isnull(s.CleanSqm,0) else 0 end,
    NTBS = case when isnull(s.CleanType,0) = 14 or isnull(s.CleanType,0) = 15 
                then isnull(s.CleanSqm,0) else 0 end,
    CUR = case when isnull(s.CleanType,0) = 1 then isnull(s.CleanSqm,0) else 0 end,
    CURV = case when isnull(s.CleanType,0) = 5 then isnull(s.CleanSqm,0) else 0 end,
    BKL = case when isnull(s.CleanType,0) = 2 then isnull(s.CleanSqm,0) else 0 end,
    BVM = case when isnull(s.CleanType,0) = 7 then isnull(s.CleanSqm,0) else 0 end,
    isnull(s.PlanNo,'''') PlanNo, isnull(r.Description,'''') ReefDesc
  from Survey s
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  left join Code_CleanTypes cc on
    cc.CleanTypeID = s.CleanType
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
	  r.reefid in (' + @TheReef + ') and
	  sh.oreflowid in (' + @TheShaft + ') and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' and
      s.Activity IN(0,1,9) and
        (s.CleanType in ' + @TheCleanType + ' or s.CleanType is null or s.cleantype = -1 or CleanType = '''') 
        and s.CleanType <> 13) a 
group by a.CleanTypes '

--print @TheQuery1
exec (@TheQuery1)


go



create 
procedure [dbo].[sp_SurveyReport_Sweepings_Zeroes] 
as 
  select '' name_5, '' name_4, 
     '' name_3, '' name_2, 
     '' name_1, '' name, '' Workplace, 
     '' CleanTypes, 
     OGSqm = 0, 
     OGMetres = 0, 
     OGTons = 0, 
     OGContent = 0, 
     OGGT = 0, 
     CleanSqm = 0, 
     CleanTons = 0, 
     CleanContents = 0, 
     CleanDist = 0, 
     StopeSqm = 0, 
     SqmOSFTotal = 0, 
     SVPerc = 0, 
     CurrIMLPerc = 0, 
     BLMLPerc = 0, 
     TotalMLPerc = 0, 
     NAFS = 0, 
     NTBS = 0, 
     CUR = 0, 
     CURV = 0, 
     BKL = 0, 
     BVM = 0, 
     PlanNo = '', 
     ReefDesc = ''

go


create 
procedure [dbo].[sp_SurveyReport_Sweepings] 
--declare
@TheTypeReport varchar(1), --1=Stoping, 2=Development, 3=Sweepings, 4=Total Mine
@TheSection varchar(1),    --1=SectionID, 2=SectionID+Name, 3=Name
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheCleanType varchar(200),
@TheSectionID varchar (20),
@TheSelectBy varchar (100)

as

declare @TheQuery1 varchar(8000)
IF @TheTypeReport = '3'
BEGIN
  set @TheQuery1 = 
  ' select isnull(a.name_5,'''') name_5, isnull(a.name_4,'''') name_4, 
           isnull(a.name_3,'''') name_3, isnull(a.name_2,'''') name_2, 
           isnull(a.name_1,'''') name_1, isnull(a.name,'''') name, 
           isnull(a.Workplace,'''') Workplace,
           isnull(CleanTypes,'''') CleanTypes, '
END

IF @TheTypeReport = '4'
BEGIN
  set @TheQuery1 = 
  ' select isnull(a.name_5,'''') name_5, '''' name_4, '''' name_3, '''' name_2, 
           '''' name_1, '''' name, '''' Workplace, '''' CleanTypes, '
END
set @TheQuery1 = @TheQuery1 + '
    OGSqm = sum(OGSqm),
    OGMetres = sum (OGMetres),
    OGTons = sum (OGTons),
    OGContent = sum(OGContent),
    OGGT = case when sum(OGTons) > 0 then sum(OGContent) / sum(OGTons) else 0 end,
    CleanSqm = sum(CleanSqm) - sum(NAFS), 
    CleanTons = sum(CleanTons), 
    CleanContents = sum(CleanContents),
    CleanDist = case when sum(CleanSqm) > 0 then sum(CleanDist) / sum(CleanSqm) else 0 end,
    StopeSqm = sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS), 
    Sweepable = sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) - sum(NTBS), 
    SqmOSFTotal =sum(SqmOSFTotal),
    SVPerc = case when sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) = 0 then 0
                  else (sum(CleanSqm) - sum(NAFS)) / (sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS)) * 100 end,
    CurrIMLPerc = case when sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) = 0 then 0
                  else (sum(CUR) + sum(CURV)) / (sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS)) * 100 end,
    BLMLPerc = case when sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) = 0 then 0
                  else (sum(BKL) + sum(BVM)) / (sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS)) * 100 end,
    TotalMLPerc = case when sum(SqmTotal) - sum(SqmOSFTotal) - sum(NAFS) = 0 then 0
                  else (sum(CUR) + sum(CURV) + sum(BKL) + sum(BVM)) / 
                       (sum(SqmTotal)  - sum(SqmOSFTotal)- sum(NAFS)) * 100 end,				      			      	      
    NAFS = sum(NAFS), 
    NTBS = sum(NTBS), 
    CUR = sum(CUR), 
    CURV = sum(CURV), 
    BKL = sum(BKL), 
    BVM = sum(BVM), 
    PlanNo = max(PlanNo), 
    ReefDesc = max(ReefDesc),
    PrintLine = sum(PrintLine)
 from ( '
IF @TheTypeReport = '3'
BEGIN
  IF @TheSection = '1'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.sectionid_5 name_5, sc.sectionid_4 name_4,
			   sc.sectionid_3 name_3,sc.sectionid_2 name_2 ,
			   sc.sectionid_1 name_1, sc.sectionid name, '
  END
  IF @TheSection = '2'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select  sc.sectionid_5 +'':''+sc.name_5 name_5, sc.sectionid_4 +'':''+sc.name_4 name_4,
				sc.sectionid_3 +'':''+sc.name_3 name_3,sc.sectionid_2 +'':''+sc.name_2 name_2,
                sc.sectionid_1 +'':''+sc.name_1 name_1, sc.sectionid +'':''+sc.name name, ' 
  END
  IF @TheSection = '3'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.name_5 name_5, sc.name_4 name_4,
               sc.name_3 name_3, sc.name_2 name_2,
               sc.name_1 name_1, sc.name name, '
               
  END
  set @TheQuery1 = @TheQuery1 + ' s.WorkPlaceID+'':''+w.Description Workplace,
      CleanTypes = case when s.CleanType not in (6,11) then cc.CleanTypeDesc 
                 else '''' end, '
END

IF @TheTypeReport = '4'
BEGIN
  IF @TheSection = '1'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.sectionid_5 name_5, '
  END
  IF @TheSection = '2'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select  sc.sectionid_5 +'':''+sc.name_5 name_5, ' 
  END
  IF @TheSection = '3'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.name_5 name_5, '
               
  END
  set @TheQuery1 = @TheQuery1 + ' '''' Workplace, '''' CleanTypes, '
END
set @TheQuery1 = @TheQuery1 + '
	PrintLine = case when s.CleanType is null then 0 else 1 end,
	OGSqm = case when isnull(s.CleanType,0) = 13 then isnull(s.CleanSqm,0) else 0 end,
    OGMetres = case when isnull(s.CleanType,0) = 13 then isnull(s.CleanVamp,0) else 0 end,
    OGTons = case when isnull(s.CleanType,0) = 13 then isnull(s.CleanTons,0) else 0 end,
    OGContent = case when isnull(s.CleanType,0) = 13 then isnull(s.CleanContents,0) else 0 end,
    CleanSqm = case when isnull(s.CleanType,0) <> 13 then isnull(s.CleanSqm,0) else 0 end,
    CleanTons = case when isnull(s.CleanType,0) <> 13 then isnull(s.CleanTons,0) else 0 end,
    CleanContents = case when isnull(s.CleanType,0) <> 13 then isnull(s.CleanContents,0) else 0 end,
    CleanDist = case when isnull(s.CleanType,0) <> 13 then isnull(s.CleanDist,0) * isnull(s.CleanSqm,0) else 0 end,
    StopeSqm = case when isnull(s.CleanType,0) <> 13 then isnull(s.StopeSqm,0) else 0 end,
    SqmTotal = case when isnull(s.CleanType,0) <> 13 then isnull(s.SqmTotal,0) else 0 end,
    SqmOSFTotal = case when isnull(s.CleanType,0) <> 13 then isnull(s.SqmOSFTotal,0) else 0 end,
    NAFS = case when isnull(s.CleanType,0) = 4 then isnull(s.CleanSqm,0) else 0 end,
    NTBS = case when isnull(s.CleanType,0) = 14 or isnull(s.CleanType,0) = 15 
                then isnull(s.CleanSqm,0) else 0 end,
    CUR = case when isnull(s.CleanType,0) = 1 then isnull(s.CleanSqm,0) else 0 end,
    CURV = case when isnull(s.CleanType,0) = 5 then isnull(s.CleanSqm,0) else 0 end,
    BKL = case when isnull(s.CleanType,0) = 2 then isnull(s.CleanSqm,0) else 0 end,
    BVM = case when isnull(s.CleanType,0) = 7 then isnull(s.CleanSqm,0) else 0 end,
    isnull(s.PlanNo,'''') PlanNo, isnull(r.Description,'''') ReefDesc
  from Survey s
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  left join Code_CleanTypes cc on
    cc.CleanTypeID = s.CleanType
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
	  r.reefid in (' + @TheReef + ') and
	  sh.oreflowid in (' + @TheShaft + ') and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' and 
      s.Activity IN(0,1,9) and 
        (s.CleanType in ' + @TheCleanType + ' or s.CleanType is null or s.CleanType = -1 or s.CleanTYpe = '''')  ) a '
IF @TheTypeReport = '3'  
BEGIN            
  set @TheQuery1 = @TheQuery1 + 
    ' group by a.name_5, a.name_4, a.name_3, a.name_2,
               a.name_1, a.name, a.Workplace, a.CleanTypes with rollup'
END 
IF @TheTypeReport = '4'  
BEGIN            
  set @TheQuery1 = @TheQuery1 + ' group by a.name_5 with rollup '
END  

--print @TheQuery1
exec (@TheQuery1)

go



create 
procedure [dbo].[sp_SurveyReport_StopingType] 
@TheReclamation varchar(1),
@ThePaid varchar(1),       --1=Paid, 2=Unpaid, 3=Paid and Unpaid
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheSectionID varchar (20),
@TheSelectBy varchar (100)
--exec Report_Survey_Stopingtype 'N','3','201112','201112',
as

declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)

set @TheQuery1 =  'select Stype = Stype,
    
    SqmTotal = sum(a.SqmTotal),
    SQMFW = sum(a.SQMFW),
    FW = case when sum(a.SqmReef) > 0 then sum(a.SQMFW) / sum(a.SqmReef) else 0 end,
    CW = case when sum(a.SqmReef) > 0 then sum(a.SQMCW) / sum(a.SqmReef) else 0 end,
    HW = case when sum(a.SqmReef) > 0 then sum(a.SQMHW) / sum(a.SqmReef) else 0 end,
    SQMCW = sum(a.SQMCW),
    SQMHW = sum(a.SQMHW),
    SQMSW = sum(a.SQMSW),
    BrokenSW = case when sum(a.BrokenSQM) > 0 then sum(a.BrokenSQMSW) / sum(a.BrokenSQM) else 0 end,
    ReefSW = case when sum(a.SqmReef) > 0 then sum(a.SQMReefSW) / sum(a.SqmReef) else 0 end,
    OSSW = case when sum(a.SqmOSTotal) > 0 then sum(a.StopeSqmOSSW) / sum(a.SqmOSTotal) else 0 end,
    OSFSW = case when sum(a.SqmOSFTotal) > 0 then sum(a.StopeSqmOSFSW) / sum(a.SqmOSFTotal) else 0 end,
    SqmReefSW = sum(a.SqmReefSW), '
IF @TheReclamation = 'Y'
BEGIN
  set @TheQuery1 = @TheQuery1 + '
    TonsReef = sum(a.TonsReef) - sum(a.RecTons),
    Content = sum(a.Content) - sum(a.RecGrams),
    SqmReef = sum(a.SqmReef) - sum(a.RecSqmTotal), 
	SqmOSTotal = sum(a.SqmOSTotal)  - sum(a.RecSqmOSTotal),
    StopeSqmOS = sum(a.StopeSqmOS) - sum(a.RecStopeSqmOS),
    LedgeSqm = sum(a.LedgeSqm) - sum(a.RecLedgeSqm),
    StopeSqm = sum(a.StopeSqm ) - sum(a.RecStopeSqm),
    SqmConvTotal = sum(a.SqmConvTotal) - sum(a.RecSqmConvTotal),
    SqmOSFTotal = sum(a.SqmOSFTotal) - sum(a.RecSqmOSFTotal),'
END
ELSE
BEGIN
  set @TheQuery1 = @TheQuery1 + '
    TonsReef = sum(a.TonsReef),
    Content = sum(a.Content),
    SqmReef = sum(a.SqmReef),
	SqmOSTotal = sum(a.SqmOSTotal),
    StopeSqmOS = sum(a.StopeSqmOS),
    LedgeSqm = sum(a.LedgeSqm),
    StopeSqm = sum(a.StopeSqm ),
    SqmConvTotal = sum(a.SqmConvTotal),
    SqmOSFTotal = sum(a.SqmOSFTotal), '
END    
set @TheQuery1 = @TheQuery1 + '
		TonsOSF = sum(a.TonsOSF),
		ASGTons = sum(a.ASGTons),
		TonsWaste = sum(a.TonsWaste),
		ExtraTons = sum(a.ExtraTons),
		PackedTons = sum(a.PackedTons),
		TonsTotal = sum(a.TonsTotal),
		LockUpTons = sum(a.LockUpTons),
		ReefGT = case when sum(a.TonsReef) > 0 then sum(a.Content) / sum(a.TonsReef) else 0 end,
		ReefCmgt = case when sum(a.SqmReef) > 0 then sum(a.SqmReefcmgt) / sum(a.SqmReef) else 0 end,
		TotalReefGT = case when sum(a.TonsReef) + sum(a.TonsWaste) > 0 
				then sum(a.Content) / (sum(a.TonsReef) + sum(a.TonsWaste)) else 0 end,
		TotalReefCmgt = case when (sum(a.SqmOSTotal) + sum(a.StopeSqm) + 
								  sum(a.LedgeSqm) + sum(a.SqmConvTotal)) > 0 
							 then sum(a.SqmReefcmgt) / (sum(a.SqmOSTotal) + sum(a.StopeSqm) + 
								  sum(a.LedgeSqm) + sum(a.SqmConvTotal)) else 0 end,
		FAdv = case when sum(a.FLTotal) > 0 then sum(a.SqmTotal) / sum(a.FLTotal) else 0 end,
		LedgeFL = sum(a.LedgeFL),
		StopeFL = sum(a.StopeFL),
		StopeFLOS = sum(a.StopeFLOS),
		LedgeFLOS = sum(a.LedgeFLOS),
		FLOSTotal = sum(a.FLOSTotal),
		FLTotal = sum(a.FLTotal),
		RecSqmTotal = sum(a.RecSqmTotal),
		RecTons = sum(a.RecTons),
		RecGrams = sum(a.RecGrams),
		Destination = max(a.Destination), 
		ReefDesc = max(a.ReefDesc)
    from ( '

set  @TheQuery2 = 'select case when st.StopeTypeDesc is null then ''Open Mining'' else st.StopeTypeDesc end Stype,
  s.StopeSqmOS, s.LedgeSqm, s.StopeSqm, s.SqmConvTotal,
  s.SqmOSFTotal, s.SqmOSTotal, s.SqmTotal,
  SQMFW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.FW,
  SQMCW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.CW,
  SQMHW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.HW,
  SqmSW = s.SWSqm * s.SqmTotal,
  BrokenSQM = case when SWOSF <> 0 then s.SqmTotal else s.SqmTotal - s.SqmOSFTotal end,
  BrokenSQMSW = 
	case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) = 0 and isnull(s.SWOS,0) = 0 then 0 else
	  case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) = 0 and isnull(s.SWOS,0) <> 0 then 
	    isnull(s.SWOS,0) * (isnull(s.SqmTotal,0) - isnull(s.SqmOSFTotal,0)) else
		case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) <> 0 then 
			isnull(s.SWOSF,0) * isnull(s.SqmTotal,0) else	
			case when isnull(s.SWSqm,0) <> 0 and isnull(s.SWOSF,0) = 0 then 
				isnull(s.SWSQM,0) * (isnull(s.SqmTotal,0) - isnull(s.SqmOSFTotal,0)) else
				case when isnull(s.SWSqm,0) <> 0 and isnull(s.SWOSF,0) <> 0 then 
					isnull(s.SWSQM,0) * isnull(s.SqmTotal,0) 
				end			
			end
		end
	  end
	end,  	
  TonsTotal = isnull(((s.StopeSqm + s.LedgeSqm + SqmConvTotal) * s.SWSQM / 100 * s.Density),0) + isnull(s.TonsOS,0) + isnull(s.TonsOSF,0) + 
  ((s.CubicsReef + s.CubicsWaste)*s.density) +
			(case when s.measheight*100 > s.SWSqm then
              (((s.Wastemetres*s.measwidth*s.measheight)*s.density+s.reefmetres *
              (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
            else
              (((s.Wastemetres*s.measwidth*s.measheight)*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
            end),
  TonsReef = (s.StopeSqm + s.LedgeSqm + SqmConvTotal) * s.SWSQM / 100 * s.Density,
  TonsWaste = s.TonsOS, LockUpTons = s.LockupTons, 
  s.TonsOSF,
  ASGTons = case when s.measheight*100 > s.SWSqm then
              (s.Wastemetres*s.measwidth*s.measheight*s.density)+(s.reefmetres *
              (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)
            else
              (s.Wastemetres*s.measwidth*s.measheight*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)
            end,
  ExtraTons = (s.CubicsReef + s.CubicsWaste)*s.density,
  PackedTons = case when Destination = 0 then
          		            ((s.CubicsReef + s.CubicsWaste)*s.density) +
          			        (s.SqmOSFTotal*s.SWOSF/100*s.density) +
          				    (((s.SqmOSTotal)*s.SWOS/100)*s.density) +
          					        (((s.StopeSqm+s.LedgeSqm+s.SqmConvTotal)*s.SWSqm/100)*s.density) +
              (case when
          		                s.measheight*100 > s.SWSqm then
          	                (((s.Wastemetres*s.measwidth*s.measheight)*s.density+s.reefmetres*
          		                (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
          		                (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
              else
          	                (((s.Wastemetres*s.measwidth*s.measheight)*s.density) +
          	                (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)) end) else 0 end,
  s.LedgeFL, --Ledge FL m
  s.StopeFL, --Stope FL m
  s.StopeFLOS, --OS Stope FL m
  s.LedgeFLOS, --OS Ledge FL m
  s.FLOSTotal, --OS  FL m
  s.FLTotal, --Total FL
  destination = case when s.Destination = ''0'' then ''P'' else
                case when s.Destination = ''1'' then ''D'' else ''M'' end end,
  r.Description ReefDesc,
  SQMReefSW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.SWSqm, --Reef SW cm
  StopeSqmOSSW = s.SqmOSTotal * s.SWOS,
  StopeSqmOSFSW = s.SqmOSFTotal * s.SWOSF,
  Content = case when s.SWSqm <> 0 then
              (((s.SqmTotal-s.SqmOSTotal-s.SqmOSFTotal)*s.cmgt*s.density/100)+
              (s.cubicsreef*(s.cmgt/(s.SWSqm)*s.density))) else 0 end,
  SqmReef = s.StopeSqm+s.LedgeSqm+s.SqmConvTotal,
  SqmReefcmgt = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.cmgt,
  SqmTotalcmgt = s.SqmTotal * s.cmgt,

 
  RecSqmOSTotal = case when Stype = 15 then s.SqmOSTotal else 0 end,
  RecSqmTotal = case when Stype = 15 then s.SqmTotal else 0 end,
  RecSqmConvTotal = case when Stype = 15 then s.SqmConvTotal else 0 end,
  RecSqmOSFTotal = case when Stype = 15 then s.SqmOSFTotal else 0 end,
  RecStopeSqmOS = case when Stype = 15 then s.StopesqmOS else 0 end,
  RecLedgeSqm = case when Stype = 15 then s.LedgeSqm else 0 end,
  RecStopeSqm = case when Stype = 15 then s.StopeSqm else 0 end,
  RecTons = case when stype = 15 then
      (((s.StopeSqm+s.LedgeSqm+s.SqmConvTotal)*s.SWSqm/100)*s.density) else 0 end,
  RecGrams = case when stype = 15 then
                 case when s.SWSqm <> 0 then
 	                (((s.SqmTotal-s.SqmOSTotal-s.SqmOSFTotal)*s.cmgt*s.density/100)+
                  (s.cubicsreef*(s.cmgt/(s.SWSqm)*s.density)))
                 else 0 end
              else 0 end	
  from Survey s '
set @TheQuery2 = @TheQuery2 + '
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid
  left outer join code_stopetypes st on
  st.stopetypeid = s.stype, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
      s.Activity IN(0,9) and
      (isnull(s.SqmTotal,0) > 0 or isnull(s.ReefMetres,0) > 0 or isnull(s.WasteMetres,0) > 0 or 
      isnull(CubicsReef,0) > 0 or isnull(CubicsWaste,0) > 0) and '
IF @ThePaid = '1'     
   set @TheQuery2 = @TheQuery2 + ' s.Payment <> 1 and '
IF @ThePaid = '2'     
   set @TheQuery2 = @TheQuery2 + ' s.Payment = 1 and '  
set @TheQuery2 = @TheQuery2 + 
	' r.reefid in (' + @TheReef + ') and
	  sh.oreflowid in (' + @TheShaft + ') and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' ) a  group by Stype with rollup '

--print @TheQuery1
--print @TheQuery2
exec (@TheQuery1+@TheQuery2)

go


create procedure [dbo].[sp_SurveyReport_Stoping_Zeroes] 
as 
 
 select	null Prodmonth, 
     '' name_5, '' name_4, 
     '' name_3, '' name_2 , 
     '' name_1, '' name, '' Workplace, 0 SeqNo, 
     '' CostArea, 
     0 SqmOSTotal,
     0 StopeSqmOS, 
     0 LedgeSqm, 
     0 StopeSqm, 
     0 SqmConvTotal, 
     0 SqmOSFTotal, 
     0 SqmTotal, 
     0 SQMFW, 
     0 FW, 
     0 CW, 
     0 HW, 
     0 SQMCW, 
     0 SQMHW, 
     0 SQMSW, 
     0 BrokenSW, 
     0 ReefSW, 
     0 OSSW, 
     0 OSFSW, 
     0 SqmReefSW, 
     0 TonsReef, 
     0 TonsOS, 
     0 TonsOSF, 
     0 ASGTons, 
     0 TonsWaste,
     0 ExtraTons,
     0 PackedTons,
     0 TonsTotal,
     0 LockUpTons,
     0 SqmReef,
     0 ReefGT, 
     0 ReefCmgt, 
     0 TotalReefGT,
     0 TotalReefCmgt, 
    cast( 0 as numeric(18,8)) Content,
     0 FAdv,
     0 LedgeFL,
     0 StopeFL,
     0 StopeFLOS,
     0 LedgeFLOS,
     0 FLOSTotal,
     0 FLTotal,
     0 RecSqmTotal,
     0 RecTons,
    cast( 0 as numeric(18,8)) RecGrams,
     '' Destination,
     '' ReefDesc


go


create 
procedure [dbo].[sp_SurveyReport_Stoping] 
--declare
@TheTypeReport varchar(1), --1=Stoping, 2=Development, 3=Sweepings, 4=Total Mine
@TheReclamation varchar(1),
@TheSection varchar(1),    --1=SectionID, 2=SectionID+Name, 3=Name
@ThePaid varchar(1),       --1=Paid, 2=Unpaid, 3=Paid and Unpaid
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheSectionID varchar (20),
@TheSelectBy varchar (100)
--exec Report_Survey_Stoping] '1','N','2','3','201112','201112',
as

declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)

IF @TheTypeReport = '1'
BEGIN
  set @TheQuery1 = 
  ' select isnull(a.name_5,'''') name_5, isnull(a.name_4,'''') name_4, 
           isnull(a.name_3,'''') name_3, isnull(a.name_2,'''') name_2, 
           isnull(a.name_1,'''') name_1, isnull(a.name,'''') name, 
           isnull(a.Workplace,'''') Workplace, isnull(a.SeqNo,0) SeqNo, ' 
END

IF @TheTypeReport = '4'
BEGIN
  set @TheQuery1 = 
  ' select isnull(a.name_5,'''') name_5, '''' name_4, '''' name_3, '''' name_2, 
           '''' name_1, '''' name, '''' Workplace, 0 SeqNo, '
END
set @TheQuery1 = @TheQuery1 + '
    CostArea = max(CostArea),
    
    SqmTotal = sum(a.SqmTotal),
    SQMFW = sum(a.SQMFW),
    FW = case when sum(a.SqmReef) > 0 then sum(a.SQMFW) / sum(a.SqmReef) else 0 end,
    CW = case when sum(a.SqmReef) > 0 then sum(a.SQMCW) / sum(a.SqmReef) else 0 end,
    HW = case when sum(a.SqmReef) > 0 then sum(a.SQMHW) / sum(a.SqmReef) else 0 end,
    SQMCW = sum(a.SQMCW),
    SQMHW = sum(a.SQMHW),
    SQMSW = sum(a.SQMSW),
    BrokenSW = case when sum(a.BrokenSQM) > 0 then sum(a.BrokenSQMSW) / sum(a.BrokenSQM) else 0 end,
    ReefSW = case when sum(a.SqmReef) > 0 then sum(a.SQMReefSW) / sum(a.SqmReef) else 0 end,
    OSSW = case when sum(a.SqmOSTotal) > 0 then sum(a.StopeSqmOSSW) / sum(a.SqmOSTotal) else 0 end,
    OSFSW = case when sum(a.SqmOSFTotal) > 0 then sum(a.StopeSqmOSFSW) / sum(a.SqmOSFTotal) else 0 end,
    SqmReefSW = sum(a.SqmReefSW), '
IF @TheReclamation = 'Y'
BEGIN
  set @TheQuery1 = @TheQuery1 + '
    TonsReef = sum(a.TonsReef) - sum(a.RecTons),
    Content = sum(a.Content) - sum(a.RecGrams),
    SqmReef = sum(a.SqmReef) - sum(a.RecSqmTotal), 
	SqmOSTotal = sum(a.SqmOSTotal)  - sum(a.RecSqmOSTotal),
    StopeSqmOS = sum(a.StopeSqmOS) - sum(a.RecStopeSqmOS),
    LedgeSqm = sum(a.LedgeSqm) - sum(a.RecLedgeSqm),
    StopeSqm = sum(a.StopeSqm ) - sum(a.RecStopeSqm),
    SqmConvTotal = sum(a.SqmConvTotal) - sum(a.RecSqmConvTotal),
    SqmOSFTotal = sum(a.SqmOSFTotal) - sum(a.RecSqmOSFTotal),'
END
ELSE
BEGIN
  set @TheQuery1 = @TheQuery1 + '
    TonsReef = sum(a.TonsReef),
    Content = sum(a.Content),
    SqmReef = sum(a.SqmReef),
	SqmOSTotal = sum(a.SqmOSTotal),
    StopeSqmOS = sum(a.StopeSqmOS),
    LedgeSqm = sum(a.LedgeSqm),
    StopeSqm = sum(a.StopeSqm ),
    SqmConvTotal = sum(a.SqmConvTotal),
    SqmOSFTotal = sum(a.SqmOSFTotal), '
END    
set @TheQuery1 = @TheQuery1 + '
		TonsOSF = sum(a.TonsOSF),
		ASGTons = sum(a.ASGTons),
		TonsWaste = sum(a.TonsWaste),
		ExtraTons = sum(a.ExtraTons),
		PackedTons = sum(a.PackedTons),
		TonsTotal = sum(a.TonsTotal),
		LockUpTons = sum(a.LockUpTons),
		ReefGT = case when sum(a.TonsReef) > 0 then sum(a.Content) / sum(a.TonsReef) else 0 end,
		ReefCmgt = case when sum(a.SqmReef) > 0 then sum(a.SqmReefcmgt) / sum(a.SqmReef) else 0 end,
		TotalReefGT = case when sum(a.TonsReef) + sum(a.TonsWaste) > 0 
				then sum(a.Content) / (sum(a.TonsReef) + sum(a.TonsWaste)) else 0 end,
		TotalReefCmgt = case when (sum(a.SqmOSTotal) + sum(a.StopeSqm) + 
								  sum(a.LedgeSqm) + sum(a.SqmConvTotal)) > 0 
							 then sum(a.SqmReefcmgt) / (sum(a.SqmOSTotal) + sum(a.StopeSqm) + 
								  sum(a.LedgeSqm) + sum(a.SqmConvTotal)) else 0 end,
		FAdv = case when sum(a.FLTotal) > 0 then sum(a.SqmTotal) / sum(a.FLTotal) else 0 end,
		LedgeFL = sum(a.LedgeFL),
		StopeFL = sum(a.StopeFL),
		StopeFLOS = sum(a.StopeFLOS),
		LedgeFLOS = sum(a.LedgeFLOS),
		FLOSTotal = sum(a.FLOSTotal),
		FLTotal = sum(a.FLTotal),
		RecSqmTotal = sum(a.RecSqmTotal),
		RecTons = sum(a.RecTons),
		RecGrams = sum(a.RecGrams),
		Destination = max(a.Destination), 
		ReefDesc = max(a.ReefDesc)
    from ( '
IF @TheTypeReport = '1'
BEGIN
  IF @TheSection = '1'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.sectionid_5 name_5, sc.sectionid_4 name_4,
			   sc.sectionid_3 name_3,sc.sectionid_2 name_2 ,
			   sc.sectionid_1 name_1, sc.sectionid name, '
  END
  IF @TheSection = '2'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select  sc.sectionid_5 +'':''+sc.name_5 name_5, sc.sectionid_4 +'':''+sc.name_4 name_4,
				sc.sectionid_3 +'':''+sc.name_3 name_3,sc.sectionid_2 +'':''+sc.name_2 name_2,
                sc.sectionid_1 +'':''+sc.name_1 name_1, sc.sectionid +'':''+sc.name name, ' 
  END
  IF @TheSection = '3'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.name_5 name_5, sc.name_4 name_4,
               sc.name_3 name_3, sc.name_2 name_2,
               sc.name_1 name_1, sc.name name, '
               
  END
  set @TheQuery1 = @TheQuery1 + ' s.WorkPlaceID+'':''+w.Description Workplace, isnull(s.SeqNo,0) SeqNo, ' 
END

IF @TheTypeReport = '4'
BEGIN
  IF @TheSection = '1'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.sectionid_5 name_5, '
  END
  IF @TheSection = '2'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select  sc.sectionid_5 +'':''+sc.name_5 name_5, ' 
  END
  IF @TheSection = '3'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.name_5 name_5, '
               
  END
  set @TheQuery1 = @TheQuery1 + ' '''' Workplace, 0 SeqNo, '
END
set  @TheQuery2 = '
  CostArea = '''',
  s.StopeSqmOS, s.LedgeSqm, s.StopeSqm, s.SqmConvTotal,
  s.SqmOSFTotal, s.SqmOSTotal, s.SqmTotal,
  SQMFW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.FW,
  SQMCW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.CW,
  SQMHW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.HW,
  SqmSW = s.SWSqm * s.SqmTotal,
  BrokenSQM = case when SWOSF <> 0 then s.SqmTotal else s.SqmTotal - s.SqmOSFTotal end,
  BrokenSQMSW = 
	case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) = 0 and isnull(s.SWOS,0) = 0 then 0 else
	  case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) = 0 and isnull(s.SWOS,0) <> 0 then 
	    isnull(s.SWOS,0) * (isnull(s.SqmTotal,0) - isnull(s.SqmOSFTotal,0)) else
		case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) <> 0 then 
			isnull(s.SWOSF,0) * isnull(s.SqmTotal,0) else	
			case when isnull(s.SWSqm,0) <> 0 and isnull(s.SWOSF,0) = 0 then 
				isnull(s.SWSQM,0) * (isnull(s.SqmTotal,0) - isnull(s.SqmOSFTotal,0)) else
				case when isnull(s.SWSqm,0) <> 0 and isnull(s.SWOSF,0) <> 0 then 
					isnull(s.SWSQM,0) * isnull(s.SqmTotal,0) 
				end			
			end
		end
	  end
	end,  	
  TonsTotal = isnull(((s.StopeSqm + s.LedgeSqm + SqmConvTotal) * s.SWSQM / 100 * s.Density),0) + isnull(s.TonsOS,0) + isnull(s.TonsOSF,0) + 
  ((s.CubicsReef + s.CubicsWaste)*s.density) +
			(case when s.measheight*100 > s.SWSqm then
              (((s.Wastemetres*s.measwidth*s.measheight)*s.density+s.reefmetres *
              (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
            else
              (((s.Wastemetres*s.measwidth*s.measheight)*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
            end),
  TonsReef = (s.StopeSqm + s.LedgeSqm + SqmConvTotal) * s.SWSQM / 100 * s.Density,
  TonsWaste = s.TonsOS, LockUpTons = s.LockupTons, 
  s.TonsOSF,
  ASGTons = case when s.measheight*100 > s.SWSqm then
              (s.Wastemetres*s.measwidth*s.measheight*s.density)+(s.reefmetres *
              (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)
            else
              (s.Wastemetres*s.measwidth*s.measheight*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)
            end,
  ExtraTons = (s.CubicsReef + s.CubicsWaste)*s.density,
  PackedTons = case when Destination = 0 then
          		            ((s.CubicsReef + s.CubicsWaste)*s.density) +
          			        (s.SqmOSFTotal*s.SWOSF/100*s.density) +
          				    (((s.SqmOSTotal)*s.SWOS/100)*s.density) +
          					        (((s.StopeSqm+s.LedgeSqm+s.SqmConvTotal)*s.SWSqm/100)*s.density) +
              (case when
          		                s.measheight*100 > s.SWSqm then
          	                (((s.Wastemetres*s.measwidth*s.measheight)*s.density+s.reefmetres*
          		                (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
          		                (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
              else
          	                (((s.Wastemetres*s.measwidth*s.measheight)*s.density) +
          	                (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)) end) else 0 end,
  s.LedgeFL, --Ledge FL m
  s.StopeFL, --Stope FL m
  s.StopeFLOS, --OS Stope FL m
  s.LedgeFLOS, --OS Ledge FL m
  s.FLOSTotal, --OS  FL m
  s.FLTotal, --Total FL
  destination = case when s.Destination = ''0'' then ''P'' else
                case when s.Destination = ''1'' then ''D'' else ''M'' end end,
  r.Description ReefDesc,
  SQMReefSW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.SWSqm, --Reef SW cm
  StopeSqmOSSW = s.SqmOSTotal * s.SWOS,
  StopeSqmOSFSW = s.SqmOSFTotal * s.SWOSF,
  Content = case when s.SWSqm <> 0 then
              (((s.SqmTotal-s.SqmOSTotal-s.SqmOSFTotal)*s.cmgt*s.density/100)+
              (s.cubicsreef*(s.cmgt/(s.SWSqm)*s.density))) else 0 end,
  SqmReef = s.StopeSqm+s.LedgeSqm+s.SqmConvTotal,
  SqmReefcmgt = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.cmgt,
  SqmTotalcmgt = s.SqmTotal * s.cmgt,

 
  RecSqmOSTotal = case when Stype = 15 then s.SqmOSTotal else 0 end,
  RecSqmTotal = case when Stype = 15 then s.SqmTotal else 0 end,
  RecSqmConvTotal = case when Stype = 15 then s.SqmConvTotal else 0 end,
  RecSqmOSFTotal = case when Stype = 15 then s.SqmOSFTotal else 0 end,
  RecStopeSqmOS = case when Stype = 15 then s.StopesqmOS else 0 end,
  RecLedgeSqm = case when Stype = 15 then s.LedgeSqm else 0 end,
  RecStopeSqm = case when Stype = 15 then s.StopeSqm else 0 end,
  RecTons = case when stype = 15 then
      (((s.StopeSqm+s.LedgeSqm+s.SqmConvTotal)*s.SWSqm/100)*s.density) else 0 end,
  RecGrams = case when stype = 15 then
                 case when s.SWSqm <> 0 then
 	                (((s.SqmTotal-s.SqmOSTotal-s.SqmOSFTotal)*s.cmgt*s.density/100)+
                  (s.cubicsreef*(s.cmgt/(s.SWSqm)*s.density)))
                 else 0 end
              else 0 end	
  from Survey s '
set @TheQuery2 = @TheQuery2 + '
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
      s.Activity IN(0,9) and
      (isnull(s.SqmTotal,0) > 0 or isnull(s.ReefMetres,0) > 0 or isnull(s.WasteMetres,0) > 0 or 
      isnull(CubicsReef,0) > 0 or isnull(CubicsWaste,0) > 0 or isnull(LockUpTons,0) > 0) and '
IF @ThePaid = '1'     
   set @TheQuery2 = @TheQuery2 + ' s.Payment <> 1 and '
IF @ThePaid = '2'     
   set @TheQuery2 = @TheQuery2 + ' s.Payment = 1 and '  
set @TheQuery2 = @TheQuery2 + 
	' r.reefid in (' + @TheReef + ') and
	  sh.oreflowid in (' + @TheShaft + ') and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' ) a '
IF @TheTypeReport = '1'  
BEGIN            
  set @TheQuery2 = @TheQuery2 + 
    ' group by a.name_5, a.name_4, a.name_3, a.name_2,
               a.name_1, a.name, a.Workplace, a.SeqNo with rollup'
END 
IF @TheTypeReport = '4'  
BEGIN            
  set @TheQuery2 = @TheQuery2 + ' group by a.name_5 with rollup '
END  

--print @TheQuery1
--print @TheQuery2
exec (@TheQuery1+@TheQuery2)

go


create 
procedure [dbo].[sp_SurveyReport_StopeType_Zeroes] 
as 
 
 select	null Stype, 
     0 SqmOSTotal,
     0 StopeSqmOS, 
     0 LedgeSqm, 
     0 StopeSqm, 
     0 SqmConvTotal, 
     0 SqmOSFTotal, 
     0 SqmTotal, 
     0 SQMFW, 
     0 FW, 
     0 CW, 
     0 HW, 
     0 SQMCW, 
     0 SQMHW, 
     0 SQMSW, 
     0 BrokenSW, 
     0 ReefSW, 
     0 OSSW, 
     0 OSFSW, 
     0 SqmReefSW, 
     0 TonsReef, 
     0 TonsOS, 
     0 TonsOSF, 
     0 ASGTons, 
     0 TonsWaste,
     0 ExtraTons,
     0 PackedTons,
     0 TonsTotal,
     0 LockUpTons,
     0 SqmReef,
     0 ReefGT, 
     0 ReefCmgt, 
     0 TotalReefGT,
     0 TotalReefCmgt, 
     0 Content,
     0 FAdv,
     0 LedgeFL,
     0 StopeFL,
     0 StopeFLOS,
     0 LedgeFLOS,
     0 FLOSTotal,
     0 FLTotal,
     0 RecSqmTotal,
     0 RecTons,
     0 RecGrams,
     '' Destination,
     '' ReefDesc

go


create 
procedure [dbo].[sp_SurveyReport_Development_Zeroes] 
as 
 select	null Prodmonth, 
     '' name_5, '' name_4, 
     '' name_3, '' name_2 , 
     '' name_1, '' name, '' Workplace, 0 SeqNo, 
     0 Brokenm ,
     0 TonsExcl,
     0 CubicTons,
     0 BrokenTons,
     0 BallastTons,
     0 TramTons,
     0 Hlgem,
     0 XCm,
     0 FWDm,
     0 BHm,
     0 TWm,
     0 OtherWastem,
     0 Wastem,
     0 WasteCubics,
     0 WasteCubicTons,
     0 WasteBrokenTons,
     0 WasteBallastTons,
     0 WasteTramTons,
     0 DRm,
     0 Rsem,
     0 Wnzm,
     0 OtherReefm,
     0 Reefm,
     0 ReefCubics,
     0 ReefCubicTons,
     0 ReefBrokenTons,
     0 Val,
     0 Content,
     '' Dest,
     '' ReefDesc,
     '' PaidUnpaid,
     0 BallReef,
     0 BallWaste,
     0 ReefWidth,
     0 ReefHeight


go



create  procedure [dbo].[sp_SurveyReport_Development] 
@TheTypeReport varchar(1), --1=Stoping, 2=Development, 3=Sweepings, 4=Total Mine
@TheSection varchar(1),    --1=SectionID, 2=SectionID+Name, 3=Name
@ThePaid varchar(1),       --1=Paid, 2=Unpaid, 3=Paid and Unpaid
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheSectionID varchar(20),
@TheSelectBy varchar(100)

as

declare @TheQuery1 varchar(8000)
IF @TheTypeReport = '2'
BEGIN
  set @TheQuery1 = 
  ' select isnull(a.name_5,'''') name_5, isnull(a.name_4,'''') name_4, 
           isnull(a.name_3,'''') name_3, isnull(a.name_2,'''') name_2, 
           isnull(a.name_1,'''') name_1, isnull(a.name,'''') name, 
           isnull(a.Workplace,'''') Workplace, isnull(a.SeqNo,0) SeqNo, ' 
END

IF @TheTypeReport = '4'
BEGIN
  set @TheQuery1 = 
  ' select isnull(a.name_5,'''') name_5, '''' name_4, '''' name_3, '''' name_2, 
           '''' name_1, '''' name, '''' Workplace, 0 SeqNo, '
END
set @TheQuery1 = @TheQuery1 + '
    Brokenm = sum(a.MainMetres),
    TonsExcl = sum(a.TotalTons),
    CubicTons = sum(a.WasteCubicTons) + sum(a.ReefCubicTons),
    BrokenTons = sum(a.TonsWaste) + sum(a.TonsReef),
    BallastTons = sum(a.packed),
    TramTons = sum(a.TonsWaste) - sum(a.wastepacked) + sum(a.TonsReef) - sum(a.Reefpacked),
    Hlgem = sum(a.HWM),
    XCm = sum(a.XC),
    FWDm = sum(a.FWD),
    BHm = sum(a.BH),
    TWm = sum(a.TW),
    OtherWastem = sum(a.WasteMetres)-sum(a.BH)-sum(a.FWD)-sum(a.XC)-sum(a.TW)-sum(a.HWM),
    Wastem = sum(a.WasteMetres),
    WasteCubics = sum(a.cubicsWaste),
    WasteCubicTons = sum(a.WasteCubicTons),
    WasteBrokenTons = sum(a.TonsWaste),
    WasteBallastTons = sum(a.wastepacked),
    WasteTramTons = sum(a.TonsWaste) - sum(a.wastepacked),
    DRm = sum(a.Drives),
    Rsem = sum(a.Raise),
    Wnzm = sum(a.Winze), 
    OtherReefm = sum(a.ReefMetres)-sum(a.Winze)-sum(a.Raise)-sum(a.Drives),
    Reefm = sum(a.ReefMetres),
    ReefCubics = sum(a.cubicsReef),
    ReefCubicTons = sum(a.ReefCubicTons),
    ReefBrokenTons = sum(a.TonsReef),
    Val = case when sum(a.TonsReef) > 0 then sum(a.grams) / sum(a.TonsReef) else 0 end,
    Content = sum(a.grams),
    Dest = max(Destination),
    ReefDesc = max(ReefDesc),
    PaidUnpaid = max(PaidUnpaid),
    BallReef = sum(a.BallReef),
    BallWaste = sum(a.BallWaste),
    ReefWidth = case when sum(a.ReefMetres) > 0 then sum(a.AdvWidth) / SUM(a.ReefMetres) else 0 end,
    ReefHeight = case when sum(a.ReefMetres) > 0 then sum(a.AdvHeight) / SUM(a.ReefMetres) else 0 end
from ( ' 
IF @TheTypeReport = '2'
BEGIN
  IF @TheSection = '1'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.sectionid_5 name_5, sc.sectionid_4 name_4,
			   sc.sectionid_3 name_3, sc.sectionid_2 name_2 ,
			   sc.sectionid_1 name_1, sc.sectionid name, '
  END
  IF @TheSection = '2'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select  sc.sectionid_5 +'':''+sc.name_5 name_5, sc.sectionid_4 +'':''+sc.name_4 name_4,
				sc.sectionid_3 +'':''+sc.name_3 name_3,sc.sectionid_2 +'':''+sc.name_2 name_2,
                sc.sectionid_1 +'':''+sc.name_1 name_1, sc.sectionid +'':''+sc.name name, ' 
  END
  IF @TheSection = '3'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.name_5 name_5, sc.name_4 name_4,
               sc.name_3 name_3, sc.name_2 name_2,
               sc.name_1 name_1, sc.name name, '
               
  END
  set @TheQuery1 = @TheQuery1 + ' s.WorkPlaceID+'':''+w.Description Workplace, isnull(s.SeqNo,0) SeqNo, ' 
END

IF @TheTypeReport = '4'
BEGIN
  IF @TheSection = '1'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.sectionid_5 name_5, '
  END
  IF @TheSection = '2'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select  sc.sectionid_5 +'':''+sc.name_5 name_5, ' 
  END
  IF @TheSection = '3'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.name_5 name_5, '
               
  END
  set @TheQuery1 = @TheQuery1 + ' '''' Workplace, 0 SeqNo, '
END
set @TheQuery1 = @TheQuery1 + '
	sy.BrokenRockDensity BrokenRockDensity,
    WasteCubicTons = s.TonscubicsWaste,
    ReefCubicTons = s.TonscubicsReef,
    BallReef = (s.BallDepth/100) * (s.ReefMetres * s.MeasWidth) * sy.BrokenRockDensity,
    BallWaste = (s.BallDepth/100) * (s.WasteMetres * s.MeasWidth) * sy.BrokenRockDensity,
    AdvWidth = s.MeasWidth * s.ReefMetres,
    AdvHeight = s.MeasHeight * s.ReefMetres,
    PaidUnpaid = case when s.PaidUnpaid = ''Y'' then ''Y'' else ''N'' end,
    r.description ReefDesc,
    destination = case when s.Destination = ''0'' then ''P'' else
                  case when s.Destination = ''1'' then ''D'' else ''M'' end end,
    s.MainMetres,
    s.MeasWidth, s.MeasHeight,
    s.Density, s.CubicsReef,
    s.CubicsWaste,
    TonsWaste = s.TonsWaste,
    TonsReef = s.TonsReef,
    s.BallDepth, e.EndTypeID, s.WasteMetres,
    s.ReefMetres,
    s.GT,
    TotalTons = s.TonsWasteBroken + s.TonsReefBroken,
    Broken = case when s.Destination = ''0'' then s.tonstotal
                else (s.MainMetres * s.BallDepth * sy.BrokenRockDensity * s.MEASWidth/100) end,
    Packed = case when s.Destination = ''0'' then s.tonstotal
                else (s.MainMetres * s.BallDepth * sy.BrokenRockDensity * s.MEASWidth/100) end,
    HWM = case when e.ENDTYPEID in (1,2,8) then s.WasteMetres else 0 end,
    XC = case when e.ENDTYPEID = 3 then s.WasteMetres else 0 end,
    FWD = case when e.ENDTYPEID = 14 then s.WasteMetres else 0 end,
    BH = case when e.ENDTYPEID in (10,11) then s.WasteMetres else 0 end,
    TW = case when e.ENDTYPEID = 9 then s.WasteMetres else 0 end,
    wastePacked = case when s.Destination = ''0'' then
        s.TonsWaste
        else s.WasteMetres *s.MEASWidth * s.BallDepth * sy.BrokenRockDensity/100 end,
    reefPacked = case when s.Destination = ''0'' then
        s.TonsReef
        else s.ReefMetres *s.MEASWidth * s.BallDepth * sy.BrokenRockDensity/100 end,
    Drives = case when e.ENDTYPEID in (12,13) then s.ReefMetres else 0 end,
    Raise = case when e.ENDTYPEID = 4 then s.ReefMetres else 0 end,
    Winze = case when e.ENDTYPEID = 5 then s.ReefMetres else 0 end,
    Grams = case when s.measheight > 0 then
        (s.ReefMetres * s.measwidth * s.cmgt * s.Density/100) + 
        (s.Cubicsreef/s.measheight * s.cmgt * s.density / 100)
        else (s.ReefMetres * s.measwidth * s.cmgt * s.Density/100) end

  from Survey s
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid
  inner join EndType e on
    e.EndTypeID = w.EndTypeID, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
      s.Activity = 1  and '
IF @ThePaid = '1'     
   set @TheQuery1 = @TheQuery1 + ' s.Payment <> 1 and '
IF @ThePaid = '2'     
   set @TheQuery1 = @TheQuery1 + ' s.Payment = 1 and '  
set @TheQuery1 = @TheQuery1 + 
	' r.reefid in (' + @TheReef + ') and
	  sh.oreflowid in (' + @TheShaft + ') and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' ) a '
IF @TheTypeReport = '2'  
BEGIN            
  set @TheQuery1 = @TheQuery1 + 
    ' group by a.name_5, a.name_4, a.name_3, a.name_2,
               a.name_1, a.name, a.Workplace, a.SeqNo with rollup'
END 
IF @TheTypeReport = '4'  
BEGIN            
  set @TheQuery1 = @TheQuery1 + ' group by a.name_5 with rollup '
END  

--print @TheQuery1
exec (@TheQuery1)

go



create
procedure [dbo].[sp_SurveyReport_DevDumpMillPack_Zeroes] 
as 
 select	'' Destination,
     0 Brokenm ,
     0 TonsExcl,
     0 CubicTons,
     0 BrokenTons,
     0 BallastTons,
     0 TramTons,
     0 Hlgem,
     0 XCm,
     0 FWDm,
     0 BHm,
     0 TWm,
     0 OtherWastem,
     0 Wastem,
     0 WasteCubics,
     0 WasteCubicTons,
     0 WasteBrokenTons,
     0 WasteBallastTons,
     0 WasteTramTons,
     0 DRm,
     0 Rsem,
     0 Wnzm,
     0 OtherReefm,
     0 Reefm,
     0 ReefCubics,
     0 ReefCubicTons,
     0 ReefBrokenTons,
     0 Val,
     0 Content,
     '' Dest,
     '' ReefDesc,
     '' PaidUnpaid,
     0 BallReef,
     0 BallWaste,
     0 ReefWidth,
     0 ReefHeight


go



create procedure [dbo].[sp_SurveyReport_DevDumpMillPack] 
@ThePaid varchar(1),       --1=Paid, 2=Unpaid, 3=Paid and Unpaid
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheSectionID varchar (20),
@TheSelectBy varchar (100)
as

declare @TheQuery1 varchar(8000)


set @TheQuery1 = 'select isnull(a.Destination,'''') Destination, 
    Brokenm = sum(a.MainMetres),
    TonsExcl = sum(a.TotalTons),
    CubicTons = sum(a.WasteCubicTons) + sum(a.ReefCubicTons),
    BrokenTons = sum(a.TonsWaste) + sum(a.TonsReef),
    BallastTons = sum(a.packed),
    TramTons = sum(a.TonsWaste) - sum(a.wastepacked) + sum(a.TonsReef) - sum(a.Reefpacked),
    Hlgem = sum(a.HWM),
    XCm = sum(a.XC),
    FWDm = sum(a.FWD),
    BHm = sum(a.BH),
    TWm = sum(a.TW),
    OtherWastem = sum(a.WasteMetres)-sum(a.BH)-sum(a.FWD)-sum(a.XC)-sum(a.TW)-sum(a.HWM),
    Wastem = sum(a.WasteMetres),
    WasteCubics = sum(a.cubicsWaste),
    WasteCubicTons = sum(a.WasteCubicTons),
    WasteBrokenTons = sum(a.TonsWaste),
    WasteBallastTons = sum(a.wastepacked),
    WasteTramTons = sum(a.TonsWaste) - sum(a.wastepacked),
    DRm = sum(a.Drives),
    Rsem = sum(a.Raise),
    Wnzm = sum(a.Winze), 
    OtherReefm = sum(a.ReefMetres)-sum(a.Winze)-sum(a.Raise)-sum(a.Drives),
    Reefm = sum(a.ReefMetres),
    ReefCubics = sum(a.cubicsReef),
    ReefCubicTons = sum(a.ReefCubicTons),
    ReefBrokenTons = sum(a.TonsReef),
    Val = case when sum(a.TonsReef) > 0 then sum(a.grams) / sum(a.TonsReef) else 0 end,
    Content = sum(a.grams),
    Dest = max(Destination),
    ReefDesc = max(ReefDesc),
    PaidUnpaid = max(PaidUnpaid),
    BallReef = sum(a.BallReef),
    BallWaste = sum(a.BallWaste),
    ReefWidth = case when sum(a.ReefMetres) > 0 then sum(a.AdvWidth) / SUM(a.ReefMetres) else 0 end,
    ReefHeight = case when sum(a.ReefMetres) > 0 then sum(a.AdvHeight) / SUM(a.ReefMetres) else 0 end
from ( select  
	sy.BrokenRockDensity BrokenRockDensity,
    WasteCubicTons = s.TonscubicsWaste,
    ReefCubicTons = s.TonscubicsReef,
    BallReef = (s.BallDepth/100) * (s.ReefMetres * s.MeasWidth) * sy.BrokenRockDensity,
    BallWaste = (s.BallDepth/100) * (s.WasteMetres * s.MeasWidth) * sy.BrokenRockDensity,
    AdvWidth = s.MeasWidth * s.ReefMetres,
    AdvHeight = s.MeasHeight * s.ReefMetres,
    PaidUnpaid = case when s.PaidUnpaid = ''Y'' then ''Y'' else ''N'' end,
    '''' ReefDesc,
    Destination = case when s.Destination = 0 then ''Packed Summary''
                       when s.Destination = 1 then ''Dumped Summary''
                       else ''Milled Summary'' end, 
    s.MainMetres,
    s.MeasWidth, s.MeasHeight,
    s.Density, s.CubicsReef,
    s.CubicsWaste,
    TonsWaste = s.TonsWaste,
    TonsReef = s.TonsReef,
    s.BallDepth, e.EndTypeID, s.WasteMetres,
    s.ReefMetres,
    s.GT,
    TotalTons = s.TonsWasteBroken + s.TonsReefBroken,
    Broken = case when s.Destination = ''0'' then s.tonstotal
                else (s.MainMetres * s.BallDepth * sy.BrokenRockDensity * s.MEASWidth/100) end,
    Packed = case when s.Destination = ''0'' then s.tonstotal
                else (s.MainMetres * s.BallDepth * sy.BrokenRockDensity * s.MEASWidth/100) end,
    HWM = case when e.ENDTYPEID in (1,2,8) then s.WasteMetres else 0 end,
    XC = case when e.ENDTYPEID = 3 then s.WasteMetres else 0 end,
    FWD = case when e.ENDTYPEID = 14 then s.WasteMetres else 0 end,
    BH = case when e.ENDTYPEID in (10,11) then s.WasteMetres else 0 end,
    TW = case when e.ENDTYPEID = 9 then s.WasteMetres else 0 end,
    wastePacked = case when s.Destination = ''0'' then
        (s.WasteMetres*s.MEASWidth*s.measheight*s.Density)+ (s.CubicsWaste * s.Density)
        else s.WasteMetres *s.MEASWidth * s.BallDepth * sy.BrokenRockDensity/100 end,
    reefPacked = case when s.Destination = ''0'' then
        (((s.ReefMetres*s.MEASWidth*s.MeasHeight) + Cubicsreef)*s.Density)
        else s.ReefMetres *s.MEASWidth * s.BallDepth * sy.BrokenRockDensity/100 end,
    Drives = case when e.ENDTYPEID in (12,13) then s.ReefMetres else 0 end,
    Raise = case when e.ENDTYPEID = 4 then s.ReefMetres else 0 end,
    Winze = case when e.ENDTYPEID = 5 then s.ReefMetres else 0 end,
    Grams = case when s.measheight > 0 then
        (s.ReefMetres * s.measwidth * s.cmgt * s.Density/100) + 
        (s.Cubicsreef/s.measheight * s.cmgt * s.density / 100)
        else (s.ReefMetres * s.measwidth * s.cmgt * s.Density/100) end
  from Survey s
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid
  inner join EndType e on
    e.EndTypeID = w.EndTypeID, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
      s.Activity = 1 and '
IF @ThePaid = '1'     
   set @TheQuery1 = @TheQuery1 + ' s.Payment = ''1'' and '
IF @ThePaid = '2'     
   set @TheQuery1 = @TheQuery1 + ' s.Payment <> ''2'' and '  
set @TheQuery1 = @TheQuery1 + 
	' r.reefid in (' + @TheReef + ') and
	  sh.oreflowid in (' + @TheShaft + ') and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' ) a 
group by a.Destination with rollup'

--print @TheQuery1
exec (@TheQuery1)

go



 create 
procedure [dbo].[sp_SurveyReport_DevCapOngoingWork_Zeroes] 
as 
 select	'' Destination,
     0 Brokenm ,
     0 TonsExcl,
     0 CubicTons,
     0 BrokenTons,
     0 BallastTons,
     0 TramTons,
     0 Hlgem,
     0 XCm,
     0 FWDm,
     0 BHm,
     0 TWm,
     0 OtherWastem,
     0 Wastem,
     0 WasteCubics,
     0 WasteCubicTons,
     0 WasteBrokenTons,
     0 WasteBallastTons,
     0 WasteTramTons,
     0 DRm,
     0 Rsem,
     0 Wnzm,
     0 OtherReefm,
     0 Reefm,
     0 ReefCubics,
     0 ReefCubicTons,
     0 ReefBrokenTons,
     0 Val,
     0 Content,
     '' Dest,
     '' ReefDesc,
     '' PaidUnpaid,
     0 BallReef,
     0 BallWaste,
     0 ReefWidth,
     0 ReefHeight

go


create procedure [dbo].[sp_SurveyReport_DevCapOngoingWork] 
@ThePaid varchar(1),       --1=Paid, 2=Unpaid, 3=Paid and Unpaid
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheSectionID varchar (20),
@TheSelectBy varchar (100)
as

declare @TheQuery1 varchar(8000)


set @TheQuery1 = 'select isnull(a.Destination,'''') Destination, 
    Brokenm = sum(a.MainMetres),
    TonsExcl = sum(a.TotalTons),
    CubicTons = sum(a.WasteCubicTons) + sum(a.ReefCubicTons),
    BrokenTons = sum(a.TonsWaste) + sum(a.TonsReef),
    BallastTons = sum(a.packed),
    TramTons = sum(a.TonsWaste) - sum(a.wastepacked) + sum(a.TonsReef) - sum(a.Reefpacked),
    Hlgem = sum(a.HWM),
    XCm = sum(a.XC),
    FWDm = sum(a.FWD),
    BHm = sum(a.BH),
    TWm = sum(a.TW),
    OtherWastem = sum(a.WasteMetres)-sum(a.BH)-sum(a.FWD)-sum(a.XC)-sum(a.TW)-sum(a.HWM),
    Wastem = sum(a.WasteMetres),
    WasteCubics = sum(a.cubicsWaste),
    WasteCubicTons = sum(a.WasteCubicTons),
    WasteBrokenTons = sum(a.TonsWaste),
    WasteBallastTons = sum(a.wastepacked),
    WasteTramTons = sum(a.TonsWaste) - sum(a.wastepacked),
    DRm = sum(a.Drives),
    Rsem = sum(a.Raise),
    Wnzm = sum(a.Winze), 
    OtherReefm = sum(a.ReefMetres)-sum(a.Winze)-sum(a.Raise)-sum(a.Drives),
    Reefm = sum(a.ReefMetres),
    ReefCubics = sum(a.cubicsReef),
    ReefCubicTons = sum(a.ReefCubicTons),
    ReefBrokenTons = sum(a.TonsReef),
    Val = case when sum(a.TonsReef) > 0 then sum(a.grams) / sum(a.TonsReef) else 0 end,
    Content = sum(a.grams),
    Dest = max(Destination),
    ReefDesc = max(ReefDesc),
    PaidUnpaid = max(PaidUnpaid),
    BallReef = sum(a.BallReef),
    BallWaste = sum(a.BallWaste),
    ReefWidth = case when sum(a.ReefMetres) > 0 then sum(a.AdvWidth) / SUM(a.ReefMetres) else 0 end,
    ReefHeight = case when sum(a.ReefMetres) > 0 then sum(a.AdvHeight) / SUM(a.ReefMetres) else 0 end
from ( select  
	sy.BrokenRockDensity BrokenRockDensity,
    WasteCubicTons = s.cubicsWaste * s.density,
    ReefCubicTons = s.density * s.cubicsReef,
    BallReef = (s.BallDepth/100) * (s.ReefMetres * s.MeasWidth) * sy.BrokenRockDensity,
    BallWaste = (s.BallDepth/100) * (s.WasteMetres * s.MeasWidth) * sy.BrokenRockDensity,
    AdvWidth = s.MeasWidth * s.ReefMetres,
    AdvHeight = s.MeasHeight * s.ReefMetres,
    PaidUnpaid = case when s.PaidUnpaid = ''Y'' then ''Y'' else ''N'' end,
    '''' ReefDesc,
    destination = ci.IndicatorDesc,
    s.MainMetres,
    s.MeasWidth, s.MeasHeight,
    s.Density, s.CubicsReef,
    s.CubicsWaste,
    TonsWaste = (s.WasteMetres*s.MEASWidth*s.measheight*s.Density) +
                (s.CubicsWaste * s.Density),
    TonsReef = (((s.ReefMetres*s.MEASWidth*s.MeasHeight) + Cubicsreef)*s.Density),
    s.BallDepth, e.EndTypeID, s.WasteMetres,
    s.ReefMetres,
    s.GT,
    TotalTons = ((s.ReefMetres*s.MEASWidth*s.MeasHeight*s.Density) +
                 (s.WasteMetres*s.MEASWidth*s.measheight*s.Density)),
    Broken = case when s.Destination = ''0'' then s.tonstotal
                else (s.MainMetres * s.BallDepth * sy.BrokenRockDensity * s.MEASWidth/100) end,
    Packed = case when s.Destination = ''0'' then s.tonstotal
                else (s.MainMetres * s.BallDepth * sy.BrokenRockDensity * s.MEASWidth/100) end,
    HWM = case when e.ENDTYPEID in (1,2,8) then s.WasteMetres else 0 end,
    XC = case when e.ENDTYPEID = 3 then s.WasteMetres else 0 end,
    FWD = case when e.ENDTYPEID = 14 then s.WasteMetres else 0 end,
    BH = case when e.ENDTYPEID in (10,11) then s.WasteMetres else 0 end,
    TW = case when e.ENDTYPEID = 9 then s.WasteMetres else 0 end,
    wastePacked = case when s.Destination = ''0'' then
        (s.WasteMetres*s.MEASWidth*s.measheight*s.Density)+ (s.CubicsWaste * s.Density)
        else s.WasteMetres *s.MEASWidth * s.BallDepth * sy.BrokenRockDensity/100 end,
    reefPacked = case when s.Destination = ''0'' then
        (((s.ReefMetres*s.MEASWidth*s.MeasHeight) + Cubicsreef)*s.Density)
        else s.ReefMetres *s.MEASWidth * s.BallDepth * sy.BrokenRockDensity/100 end,
    Drives = case when e.ENDTYPEID in (12,13) then s.ReefMetres else 0 end,
    Raise = case when e.ENDTYPEID = 4 then s.ReefMetres else 0 end,
    Winze = case when e.ENDTYPEID = 5 then s.ReefMetres else 0 end,
    Grams = case when s.measheight > 0 then
        (s.ReefMetres * s.measwidth * s.cmgt * s.Density/100) + 
        (s.Cubicsreef/s.measheight * s.cmgt * s.density / 100)
        else (s.ReefMetres * s.measwidth * s.cmgt * s.Density/100) end
  from Survey s
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  inner join Code_Indicators ci on
    ci.IndicatorID = s.Indicator
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid
  inner join EndType e on
    e.EndTypeID = w.EndTypeID, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
      s.Activity = 1 and '
IF @ThePaid = '1'     
   set @TheQuery1 = @TheQuery1 + ' s.Payment = ''1'' and '
IF @ThePaid = '2'     
   set @TheQuery1 = @TheQuery1 + ' s.Payment <> ''2'' and '  
set @TheQuery1 = @TheQuery1 + 
	' r.reefid in (' + @TheReef + ') and
	  sh.oreflowid in (' + @TheShaft + ') and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' ) a 
group by a.Destination with rollup '

--print @TheQuery1
exec (@TheQuery1)

go



ALTER Procedure [dbo].[sp_Load_BookABSStoping_Detail] 
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
		@BookDate varchar(10)
as

--set @Prodmonth = 201703
--set @SectionID = 'REAA'
--set @BookDate = '2017-03-14'


Declare @SQL VarChar(8000)

set @SQL = ' 
 select * from 
  ( 
     select pm.ProdMonth, pm.sectionid Sec, pm.sectionid SectionID, 
         pm.workplaceid WPID, w.Description, 
         pm.workplaceid + '':'' +w.Description WP, 
         pm.Activity, act.Description ActDesc, pd.ShiftDay, 
         isnull(pm.OrgUnitDay, '''') OrgUnitDS, 
         CalendarDate = convert(varchar(10), pd.CalendarDate, 120), 
         isnull(pd.ABSPicNo, '''') ABSPicNo, 
         ABSCodeDisplay = case when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                        when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = '''' then ''S'' 
                        when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                        when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = '''' then ''B'' 
                        when isnull(pd.ABSCode, '''') = ''A'' then ''A'' else '''' end, 
         isnull(pd.ABSCode, '''') ABSCode, 
         isnull(pd.ABSPrec, '''') ABSPrec, 
         isnull(pd.ABSNotes,'''') ABSNotes, 
         isnull(pm.FL, 0) FL, 
		 BookAdv = case when isnull(pd.BookMetresAdvance,0) = 0 then cast(1.2 as numeric(7,2))
		    else cast(isnull(pd.BookMetresAdvance, 0) as numeric(7,2)) end, 
		 DefaultAdv = cast(1.2 as numeric(7,2)), 
         isnull(pd.CMGT, 0) BookCmgt, 
         isnull(pd.BookSqm, 0) BookSqm, 
         isnull(pd.BookReefSqm, 0) BookReefSqm, 
         isnull(pd.BookWasteSqm, 0) BookWasteSqm, 
         cast(isnull(pd.BookMetresAdvance, 0) as numeric(7,2)) BookMetresAdvance, 
         cast(isnull(pd.BookFL, 0) as numeric(7,0)) BookFL, 
		 isnull(pd.BookTons, 0) BookTons, 
		 isnull(pd.BookReefTons, 0) BookReefTons, 
		 isnull(pd.BookWasteTons, 0) BookWasteTons, 
		 BookKG = isnull(pd.BookGrams, 0) / 1000, 
         BookGrams = isnull(pd.BookGrams, 0), 
         isnull(ProgSum.AdjSqm, 0) PrevAdjSqm, 
         0 AdjSqm, 0 AdjTons, 0 AdjGrams, 
         isnull(pm.SW,0) BookSW, ss.RockDensity BookDens, 
         BookCodeStp = case when prbook.ProblemID = ''ST'' then prbook.ProblemID else isnull(pd.BookCode,'''') end, 
         isnull(pd.SBossNotes,'''') BookProb, 
         isnull(pd.ProblemID,'''') ProblemID, isnull(pd.SBossNotes,'''') SBossNotes, 
         isnull(pd.CheckSqm, 0) CheckSqm, isnull(pd.MOFC, 0) MOFC ,
		 isnull(pd.CausedLostBlast,'''') CausedLostBlast
      from planmonth pm 
      inner 
      join planning pd on 
        pm.Prodmonth = pd.Prodmonth and 
        pm.SectionID = pd.SectionID and 
        pm.WorkplaceID = pd.WorkplaceID and 
        pm.Activity = pd.Activity and 
        pm.PlanCode = pd.PlanCode and
		pm.IsCubics = pd.IsCubics
      inner join SECTION_COMPLETE sc on 
        sc.ProdMonth = pm.ProdMonth and 
        sc.SectionID = pm.SectionID 
      inner join Seccal s on 
        s.ProdMonth = sc.ProdMonth and 
        s.SectionID = sc.SectionID_1 
      inner join Caltype ct on 
        ct.CalendarCode = s.CalendarCode and 
        ct.CalendarDate = pd.CalendarDate 
      left outer join Code_Activity act on 
        act.Activity = pm.Activity 
      inner join Workplace w on 
        pm.WorkplaceID = w.WorkplaceID 
      left outer join 
       ( 
             select b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics,
                max(CalendarDate) CalendarDate 
             from Planning b 
             where ProdMonth = '''+@Prodmonth+''' and 
                   Activity in (0, 9) and 
                   CalendarDate < '''+@BookDate+''' and 
                   PlanCode = ''MP'' 
             group by b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics 
       ) prbook1 on 
         pm.Prodmonth = prbook1.Prodmonth and 
         pm.SectionID = prbook1.SectionID and 
         pm.WorkplaceID = prbook1.WorkplaceID and 
         pm.Activity = prbook1.Activity and 
         pm.PlanCode = prbook1.PlanCode and
		 pm.IsCubics = prbook1.IsCubics
       left outer join Planning prbook on 
         prbook.Prodmonth = prbook1.Prodmonth and 
         prbook.SectionID = prbook1.SectionID and 
         prbook.WorkplaceID = prbook1.WorkplaceID and 
         prbook.Activity = prbook1.Activity and 
         prbook.PlanCode = prbook1.PlanCode and 
		 prbook.IsCubics = prbook1.IsCubics and
         prbook.Calendardate = prbook1.Calendardate 
      left outer join 
         (Select workplaceid, max(CalendarDate) CalendarDate from sampling group by workplaceid 
         ) a on a.WorkplaceID = pm.WorkplaceID 
      left outer join Sampling aa on 
        aa.WorkplaceID = pm.WorkplaceID and 
        aa.CalendarDate = a.calendarDate 
      left outer join 
          (select p.ProdMonth, WorkplaceID, p.SectionID, Activity,  
                  PlanCode, IsCubics, sum(BookSqm) ProgBookSqm, sum(AdjSqm) AdjSqm 
           from planning p 
           inner 
           join SECTION_COMPLETE sc on 
             p.ProdMonth = sc.ProdMonth and 
             p.SectionID = sc.SectionID 
           where p.ProdMonth = '''+@Prodmonth+''' and 
                 p.calendardate < '''+@BookDate+''' and 
                 sc.SectionID_1 = '''+@SectionID+''' and 
                 p.Activity = 1 and
				 p.PlanCode = ''MP'' and
				 p.IsCubics = ''N''
           group by p.ProdMonth, WorkplaceID, p.SectionID, Activity, PlanCode, IsCubics 
          ) ProgSum on 
             pm.Prodmonth = ProgSum.Prodmonth and 
             pm.SectionID = ProgSum.SectionID and 
             pm.WorkplaceID = ProgSum.WorkplaceID and 
             pm.Activity = ProgSum.Activity and 
             pm.PlanCode = ProgSum.PlanCode and
			 pm.IsCubics = ProgSum.IsCubics, SYSSET ss  
      where pm.ProdMonth = '''+@Prodmonth+''' and 
            pm.Activity in (0, 9) and 
            pm.PlanCode = ''MP'' and 
            pd.CalendarDate = '''+@BookDate+''' and 
            pm.SQM > 0 and 
            ct.WorkingDay = ''Y'' and 
            sc.SectionID_1 = '''+@SectionID+''' 
			and pm.IsCubics = ''N''
            --(p.Pumahola = ''Y'' or ct.WorkingDay = ''Y'')) a 
  ) a '

--print (@SQL)
exec (@SQL)

go


ALTER Procedure [dbo].[sp_Load_BookABSStoping]
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
        @DaysBackdate int,
        @Shift int
as

--set @Prodmonth = 201612
--set @SectionID = 'REAA'
--Set @DaysBackdate = 0
--set @Shift = 3


Declare @SQL VarChar(8000),
 @Backdate DateTime


 --Select @TheshiftTime

--@SQL1 VarChar(8000), -- Forecast and Cleaning Bookings
--@SQL2 VarChar(8000)
select @backdate = Rundate - @DaysBackdate  From sysset 

set @SQL = ' select * from 
	(select sc.Name_1 SBName, sc.SectionID_2, sc.Name Name, pm.SectionID, 
	pm.Workplaceid+'' : ''+w.Description Workplace,
	pm.Activity, 
	ShiftDay = case when ct.WorkingDay = ''Y'' then convert(varchar(3), p.ShiftDay) else ''N'' end,
	isnull(ct.WorkingDay,'''') WorkingDay,
	ct.CalendarDate CalendarDate,  
	''Book'' BookType,
	isnull(p.ProblemID,'''') ProblemID,
	Type = z.[Type],
	ABSCode = isnull(p.AbsCode,''''), 
	MonthSqm = case when z.[Type] = ''Plan'' then convert(varchar(10), cast(pm.SQM as numeric(7,0))) else '''' end,
	--case when isnull(ProgP.Prog_Plan_SQM,0) ProgPlan,
	ProgSqm =  case when z.[Type] = ''Plan'' then 
					convert(varchar(10), cast(pp.ProgPlanSQM as numeric(7,0))) else 
					convert(varchar(10), cast(pp.ProgBookSQM as numeric(7,0))) end,
	theVal = case 
  when z.[Type] = ''Plan'' and isnull(p.SQM,0) = 0 then ''''
  when z.[Type] = ''Plan'' and isnull(p.SQM,0) > 0 then convert(varchar(10), convert(Numeric(7,0),isnull(p.SQM,0)))
  when z.[Type] = ''Book'' and isnull(p.ProblemID, '''') <> '''' then isnull(p.ProblemID,'''')
  when z.[Type] = ''Book'' and isnull(p.BookSQM,0) = 0 then '''' 
  when z.[Type] = ''Book'' and isnull(p.BookSQM,0) > 0 then
		convert(varchar(10), convert(Numeric(7,0),isnull(p.BookSQM,0))) else ''''
	end,
	theValue = case 
  when z.[Type] = ''Plan'' then convert(Numeric(7,0),isnull(p.SQM,0))
  when z.[Type] = ''Book'' then convert(Numeric(7,0),isnull(p.BookSQM,0)) else 0 end
	from planmonth pm	
	inner join Section_Complete sc on 
	  pm.SectionID = sc.SectionID and
	  pm.ProdMonth = sc.ProdMonth
	inner join Seccal s on
	  sc.ProdMonth = s.ProdMonth and
	  sc.SectionID_1 = s.SectionID
	inner join CalType ct on
	  s.CalendarCode = ct.CalendarCode and
	  s.BeginDate <= ct.CalendarDate and
	  s.enddate >= ct.CalendarDate
	inner join Planning p on
	  p.ProdMonth = pm.ProdMonth and 
	  p.SectionID = pm.SectionID and
	  p.WorkplaceID = pm.WorkplaceID and
	  p.Activity = pm.Activity and
	  p.IsCubics = pm.IsCubics and
	  p.PlanCode = pm.PlanCode and
	  p.Calendardate = ct.CalendarDate
	left outer join
		(select ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode, sum(isnull(Booksqm,0)) ProgBookSQM,
		 sum(isnull(SQM,0)) ProgPlanSQM
		 from Planning, Sysset
		 where CalendarDate <= SYSSET.RUNDATE and
		 PlanCode = ''MP'' and
		 Activity <> 1 and
		 IsCubics = ''N''
		 group by ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode
		) pp on
	  pp.ProdMonth = pm.ProdMonth and 
	  pp.SectionID = pm.SectionID and
	  pp.WorkplaceID = pm.WorkplaceID and
	  pp.Activity = pm.Activity and
	  pp.IsCubics = pm.IsCubics and
	  pp.PlanCode = pm.PlanCode
	inner join Workplace W on
	  pm.WorkplaceID = w.WorkplaceID, SYSSET ss,
	(select [Order] = 1,
	Type = ''Plan''
	union
	select [Order] = 2,
	Type = ''Book''
	) z
	where pm.prodmonth = '''+ @Prodmonth +''' and sc.SectionID_1= '''+@SectionID+''' 
	and pm.Activity in (0,9) and pm.tons > 0 and pm.PlanCode = ''MP'' and pm.IsCubics = ''N'''

--if @DaysBackdate = 0
--begin  
--Set @SQL = @SQL+'and ct.calendardate = convert(Date, ss.rundate) ) a '
--end
--else
--begin
Set @SQL = @SQL+'and ct.calendardate <= ss.rundate ) a 
		order by SectionID_2, SectionID, Workplace,CalendarDate ' 
			--	and p.calendardate >= '''+Convert(Varchar(10),@backdate,120)+''' '
--end


--print (@SQL)
exec (@SQL)
go


 ALTER Procedure [dbo].[sp_Load_BookABSDevelopment_Detail] 
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
		@BookDate varchar(10)
as

--set @Prodmonth = 201703
--set @SectionID = 'RECA'
--set @BookDate = '2017-03-14'


Declare @SQL VarChar(8000)
Declare @SQL1 VarChar(8000)

set @SQL = ' 
 select * from 
  ( 
     select pm.ProdMonth, pm.sectionid Sec, pm.sectionid SectionID, 
            pm.workplaceid WPID, w.Description, 
            pm.workplaceid + '':'' + w.Description WP, pm.ReefWaste,   
            1 Activity, ''Development'' ActDesc, pd.ShiftDay, 
            isnull(pm.OrgUnitDay, '''') OrgUnitDS, 
           CalendarDate = convert(varchar(10), pd.CalendarDate, 120), 
           isnull(pd.ABSPicNo, '''') ABSPicNo, 
           ABSCodeDisplay = case when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                                when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = '''' then ''S'' 
                                when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                                when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = '''' then ''B'' 
                                when isnull(pd.ABSCode, '''') = ''A'' then ''A'' else '''' end, 
           isnull(pd.ABSCode, '''') ABSCode, 
           isnull(pd.ABSPrec, '''') ABSPrec, 
           isnull(pd.ABSNotes, '''') ABSNotes, 
           --isnull(pd.PegID, '''') PegID, 
		   PegID = pd.PegID, -- +'':''+convert(varchar(10), cast(pg.Value as numeric(10,1))),
		   isnull(pd.PegDist, 0) PegDist, 
           cast(isnull(pd.PegToFace, 0) as numeric(10,1)) PegToFace, 
		  -- isnull(pd.PegToFace, 0) PegToFaceOld, 
		   PPegID = case when isnull(c.ppeg,'''') = '''' then (case when isnull(sss.ppeg,'''') ='''' or (sss.cal < e.cal) then 
		                     e.ppeg else sss.ppeg end) else c.ppeg end ,  
			PPegToFace = case when c.ppegtoface is null then (case when sss.ppegtoface is null or (sss.cal < e.cal) then 
							e.ppegtoface else sss.ppegtoface end) else c.ppegtoface end,
			PPegDist = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
							e.ppegdist else sss.ppegdist end)  else c.ppegdist end,
			--PegFrom = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
			--				e.ppegdist else sss.ppegdist end)  else c.ppegdist end,
			--PegTo = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
			--				convert(e.ppegdist + e.ppeg else sss.ppegdist+sss.ppeg end)  else c.ppegdist + c.ppeg end,
           isnull(pd.BookMetresAdvance, 0) BookAdv, 
		   isnull(pd.BookReefAdv, 0) BookReefAdv, 
		   isnull(pd.BookWasteAdv, 0) BookWasteAdv, 
		   isnull(pd.BookTons, 0) BookTons,
		   isnull(pd.BookReefTons, 0) BookReefTons,
		   isnull(pd.BookWasteTons, 0) BookWasteTons,
           isnull(pd.BookGrams, 0) BookGrams, BookKG = isnull(pd.BookGrams, 0) / 1000, 
          -- isnull(pd.BookCubics, 0) BookCubics, isnull(pd.BookSweeps, 0) BookSweeps, 
          -- isnull(pd.BookResweeps, 0) BookReSweeps, isnull(pd.BookVamps, 0) BookVamps, 
           --BookTotal = isnull(pd.BookMetresAdvance, 0) + isnull(pd.BookSecM, 0), 
          -- isnull(pd.BookOpenUp, 0) BookOpenUp, isnull(pd.BookSecM, 0) BookSecM, 
           DHeight = case when isnull(pm.DHeight, 0) = 0 then w.EndHeight else isnull(pm.DHeight, 0) end, 
		   DWidth =  case when isnull(pm.DWidth, 0) = 0 then w.EndWidth else isnull(pm.DWidth, 0) end, 
		   isnull(ss.RockDensity, 0) Density, 
           isnull(pm.GT,0) gt, 
		   isnull(pm.CMGT,0) cmgt, 
           BookCodeDev = case when prbook.ProblemID = ''ST'' then prbook.ProblemID else isnull(pd.BookCode,'''') end, 
           isnull(pd.ProblemID, '''') ProblemID, isnull(pd.SBossNotes, '''') SBossNotes,
		    isnull(pd.CausedLostBlast, '''') CausedLostBlast
      from planmonth pm 
      inner join planning pd on 
        pm.Prodmonth = pd.Prodmonth and 
        pm.SectionID = pd.SectionID and 
        pm.WorkplaceID = pd.WorkplaceID and 
        pm.Activity = pd.Activity and 
        pm.PlanCode = pd.PlanCode and
		pm.IsCubics = pd.IsCubics
	left outer join 

		(select a.wp1, b.bookcode prevcode,  b.pegid ppeg, b.pegtoface ppegtoface, b.pegdist ppegdist, cal 
		from 
			(select p.workplaceid wp1,  max(calendardate) cal 
			from planning p 
			where calendardate < '''+@BookDate+''' and prodmonth = '''+@Prodmonth+''' and isCubics = ''N'' and PlanCode=''MP''
			   and p.activity in (1) and bookmetresadvance is not null group by p.workplaceid
			) a 
			left outer join 
				(select workplaceid , max(bookcode)  bookcode,  max(pegid)  pegid, max(pegtoface) pegtoface, max(pegdist) pegdist,  calendardate 
				from planning 
				where bookmetresadvance is not null and activity in (1) and isCubics = ''N'' and PlanCode=''MP''
				group by workplaceid, calendardate
				) b on a.wp1 = b.workplaceid and a.cal = b.calendardate 
		) c on pm.WorkplaceID = c.wp1 
 
		left outer join  
		 (select a.wp1,  b.pegid + '':'' + convert(varchar(10),b.pegvalue) ppeg, b.pegtoface ppegtoface, b.pegdist ppegdist, cal 
		 from    
			(select p.workplaceid wp1,  max(calendardate) cal	  
			from survey p 
			where prodmonth < '''+@Prodmonth+''' and p.activity in (1) and mainmetres is not null 
			group by p.workplaceid
			) a    
		 left outer join    
			(select workplaceid ,  max(pegno)  pegid, max(pegvalue) pegvalue, max(pegtoface) pegtoface, max(progto) pegdist, calendardate    
			from survey
			where mainmetres is not null and activity in (1) 
			group by workplaceid, calendardate
			) b  on a.wp1 = b.workplaceid and a.cal = b.calendardate
		) sss on pm.WorkplaceID = sss.wp1  '
set @SQL1 = ' 

		left outer join  
			(select a.wp1, b.bookcode prevcode,  b.pegid ppeg, b.pegtoface ppegtoface, b.pegdist ppegdist, cal 
			from 
				(select p.workplaceid wp1,  max(calendardate) cal  
				 from planning p 
				 where calendardate < '''+@BookDate+''' and p.activity in (1) and (bookmetresadvance > 0 or bookmetresadvance < 0) 
				 group by p.workplaceid
				) a    
			left outer join    
				(select workplaceid , max(bookcode)  bookcode,  max(pegid)  pegid, max(pegtoface) pegtoface, max(pegdist) pegdist, calendardate    
				from planning 
				where bookcode is not null and activity in (1) 
				group by workplaceid, calendardate
				) b on a.wp1 = b.workplaceid and a.cal = b.calendardate 
		) e on pm.WorkplaceID = e.wp1  
      inner join SECTION_COMPLETE sc on 
        sc.ProdMonth = pm.ProdMonth and 
        sc.SectionID = pm.SectionID 
      inner join Seccal s on 
        s.ProdMonth = sc.ProdMonth and 
        s.SectionID = sc.SectionID_1 
      inner join Caltype ct on 
        ct.CalendarCode = s.CalendarCode and 
        ct.CalendarDate = pd.CalendarDate 
      left outer join Code_Activity act on 
        act.Activity = pm.Activity 
      inner join Workplace w on 
        pm.WorkplaceID = w.WorkplaceID and
		pm.Activity = w.Activity
	  left outer join Peg pg on
	    pg.PegID = pd.PegID and
		pg.WorkplaceID = pd.WorkplaceID
      left outer join 
       ( 
             select b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics, 
                max(CalendarDate) CalendarDate 
             from planning b 
             where ProdMonth = '''+@Prodmonth+''' and 
                   Activity = 1 and 
                   CalendarDate < '''+@BookDate+''' and 
                   PlanCode = ''MP'' and
				   IsCubics = ''N''
             group by b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics
       ) prbook1 on 
         pm.Prodmonth = prbook1.Prodmonth and 
         pm.SectionID = prbook1.SectionID and 
         pm.WorkplaceID = prbook1.WorkplaceID and 
         pm.Activity = prbook1.Activity and 
         pm.PlanCode = prbook1.PlanCode and
		 pm.IsCubics = prbook1.IsCubics
       left outer join Planning prbook on 
         prbook.Prodmonth = prbook1.Prodmonth and 
         prbook.SectionID = prbook1.SectionID and 
         prbook.WorkplaceID = prbook1.WorkplaceID and 
         prbook.Activity = prbook1.Activity and 
         prbook.PlanCode = prbook1.PlanCode and 
		 prbook.IsCubics = prbook1.IsCubics and
         prbook.Calendardate = prbook1.Calendardate 
      left outer join 
         (Select workplaceid, max(CalendarDate) CalendarDate from sampling group by workplaceid 
         ) a on a.WorkplaceID = pm.WorkplaceID 
      left outer join Sampling aa on 
        aa.WorkplaceID = pm.WorkplaceID and 
        aa.CalendarDate = a.calendarDate 
      left outer join 
          (select p.ProdMonth, WorkplaceID, p.SectionID, Activity, 
                  PlanCode, IsCubics, sum(BookSqm) progbook, sum(AdjSqm) AdjSqm 
           from planning p 
           inner 
           join SECTION_COMPLETE sc on 
             p.ProdMonth = sc.ProdMonth and 
             p.SectionID = sc.SectionID 
           where p.ProdMonth = '''+@Prodmonth+''' and 
                 p.calendardate < '''+@BookDate+''' and 
                 sc.SectionID_1 = '''+@SectionID+''' and 
                 p.Activity = 1 and
				 p.PlanCode = ''MP'' and
				 p.IsCubics = ''N'' 
           group by p.ProdMonth, WorkplaceID, p.SectionID, Activity, PlanCode, IsCubics
          ) ProgSum on 
             pm.Prodmonth = ProgSum.Prodmonth and 
             pm.SectionID = ProgSum.SectionID and 
             pm.WorkplaceID = ProgSum.WorkplaceID and 
             pm.Activity = ProgSum.Activity and 
             pm.PlanCode = ProgSum.PlanCode and
			 pm.IsCubics = ProgSum.IsCubics, Sysset ss
      where pm.ProdMonth = '''+@Prodmonth+''' and 
            pm.Activity = 1 and 
            pm.PlanCode = ''MP'' and 
			pm.IsCubics = ''N'' and 
            pd.CalendarDate = '''+@BookDate+''' and 
            pm.Metresadvance > 0 and 
            ct.WorkingDay = ''Y'' and 
            sc.SectionID_1 = '''+@SectionID+''' 
            --(p.Pumahola = ''Y'' or ct.WorkingDay = ''Y'')) a 
  ) z '

  --print (@SQL)
  -- print (@SQL1)
exec (@SQL+@SQL1)
go


ALTER Procedure [dbo].[sp_Load_BookABSDevelopment]
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
        @DaysBackdate int,
        @Shift int
as

--set @Prodmonth = 201612
--set @SectionID = 'RECA'
--Set @DaysBackdate = 0
--set @Shift = 3


Declare @SQL VarChar(8000),
 @Backdate DateTime


 --Select @TheshiftTime

--@SQL1 VarChar(8000), -- Forecast and Cleaning Bookings
--@SQL2 VarChar(8000)
select @backdate = Rundate - @DaysBackdate  From sysset 

set @SQL = ' select * from 
	(select sc.Name_1 SBName, sc.SectionID_2, sc.Name Name, pm.SectionID, 
	pm.Workplaceid+'' : ''+w.Description Workplace,
	pm.Activity, 
	ShiftDay = case when ct.WorkingDay = ''Y'' then convert(varchar(3), p.ShiftDay) else ''N'' end,
	isnull(ct.WorkingDay,'''') WorkingDay,
	ct.CalendarDate CalendarDate,  
	''Book'' BookType,
	isnull(p.ProblemID,'''') ProblemID,
	Type = z.[Type],
	ABSCode = isnull(p.AbsCode,''''),
	MonthAdv = case when z.[Type] = ''Plan'' then convert(varchar(10), cast(pm.MetresAdvance as numeric(7,0))) else '''' end,
	ProgAdv = case when z.[Type] = ''Plan'' then 
					convert(varchar(10), cast(pp.ProgPlanAdv as numeric(7,0))) else 
					convert(varchar(10), cast(pp.ProgBookAdv as numeric(7,0))) end,
	theVal = case 
		  when z.[Type] = ''Plan'' and isnull(p.MetresAdvance,0) = 0 then ''''
		  when z.[Type] = ''Plan'' and isnull(p.MetresAdvance,0) > 0 then convert(varchar(10), convert(Numeric(7,1),isnull(p.MetresAdvance,0)))
		  when z.[Type] = ''Book'' and isnull(p.ProblemID, '''') <> '''' then isnull(p.ProblemID,'''')
		  when z.[Type] = ''Book'' and isnull(p.BookMetresAdvance,0) = 0 then '''' 
		  when z.[Type] = ''Book'' and isnull(p.BookMetresAdvance,0) > 0 then
		convert(varchar(10), convert(Numeric(7,1),isnull(p.BookMetresAdvance,0))) else ''''
	end,
	theValue = case 
		  when z.[Type] = ''Plan'' then convert(Numeric(7,1),isnull(p.MetresAdvance,0))
		  when z.[Type] = ''Book'' then convert(Numeric(7,1),isnull(p.BookMetresAdvance,0)) else 0 end 	
	from planmonth pm	
	inner join Section_Complete sc on 
	  pm.SectionID = sc.SectionID and
	  pm.ProdMonth = sc.ProdMonth
	inner join Seccal s on
	  sc.ProdMonth = s.ProdMonth and
	  sc.SectionID_1 = s.SectionID
	inner join CalType ct on
	  s.CalendarCode = ct.CalendarCode and
	  s.BeginDate <= ct.CalendarDate and
	  s.enddate >= ct.CalendarDate
	inner join Planning p on
	  p.ProdMonth = pm.ProdMonth and 
	  p.SectionID = pm.SectionID and
	  p.WorkplaceID = pm.WorkplaceID and
	  p.Activity = pm.Activity and
	  p.IsCubics = pm.IsCubics and
	  p.PlanCode = pm.PlanCode and
	  p.IsCubics = pm.IsCubics and
	  p.Calendardate = ct.CalendarDate
	left outer join
		(select ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode, sum(isnull(BookMetresAdvance,0)) ProgBookAdv,
		 sum(isnull(MetresAdvance,0)) ProgPlanAdv
		 from Planning, Sysset
		 where CalendarDate <= SYSSET.RUNDATE and
		 PlanCode = ''MP'' and
		 Activity = 1 and
		 IsCubics = ''N''
		 group by ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode
		) pp on
	  pp.ProdMonth = pm.ProdMonth and 
	  pp.SectionID = pm.SectionID and
	  pp.WorkplaceID = pm.WorkplaceID and
	  pp.Activity = pm.Activity and
	  pp.IsCubics = pm.IsCubics and
	  pp.PlanCode = pm.PlanCode
	inner join Workplace W on
	  pm.WorkplaceID = w.WorkplaceID, SYSSET ss,
	(select [Order] = 1,
	Type = ''Plan''
	union
	select [Order] = 2,
	Type = ''Book''
	) z
	where pm.prodmonth = '''+ @Prodmonth +''' and sc.SectionID_1= '''+@SectionID+''' 
	and pm.Activity = 1 and pm.tons > 0 and pm.PlanCode = ''MP'' and pm.IsCubics = ''N''
	and ct.calendardate <= ss.rundate ) a 
		order by SectionID_2, SectionID, Workplace,CalendarDate ' 


--print (@SQL)
exec (@SQL)
go


ALTER Procedure [dbo].[sp_SICCapture_Load] 
--Declare 
	@TypeMonth varchar(20),
    @ProdMonth varchar(6),
    @SectionID VarChar(20),
	@SectionLevel varchar(1),
	@KPI varchar(50)
as

--set @TypeMonth = 'Production'
--set @Prodmonth = 201612
--set @SectionID = 'REAA'
--set @SectionLevel = 5  --5 for production and Cleaning
--set @KPI = 'Cleaned'

declare @StartDate varchar(10)
declare @EndDate varchar(10)
declare @Diffs varchar(10)
declare @TotalShifts int
declare @CalendarCode varchar(20)

declare @SelectID varchar(20)

declare @RunDate varchar(10)
select @RunDate =  convert(varchar(10), RunDate, 120) from SYSSET

declare @HierID int
select @HierID = MOHierarchicalID from Sysset
IF (@HierID = @SectionLevel)
	 set @SelectID = 'SectionID_2'
ELSE
	set @SelectID = 'SectionID_1'

IF (@TypeMonth = 'Safety') or
   (@TypeMonth = 'Costing')
BEGIN
	select @StartDate = convert(varchar(10),StartDate,120), 
		@EndDate = convert(varchar(10),EndDate,120), 
		@Diffs = datediff(d,Startdate,enddate) + 1, 
		@TotalShifts = convert(varchar(2),TotalShifts) 
	from CalendarOther 
	where [Month] = @ProdMonth and
		  CalendarCode = @TypeMonth
END
IF (@TypeMonth = 'Production')
BEGIN
	IF (@HierID = @SectionLevel)
	BEGIN
		select top 1 @StartDate = convert(varchar(10),s.BeginDate,120), 
			@EndDate = convert(varchar(10),s.EndDate,120), 
			@diffs = datediff(d,s.BeginDate,s.EndDate) + 1, 
			@TotalShifts = s.TotalShifts, 
			@CalendarCode = s.CalendarCode 
		from SECCAL s 
		inner join Section_Complete sc on 
			sc.ProdMonth = s.Prodmonth and 
			sc.SectionID_1 = s.SectionID 
		where s.ProdMonth = @ProdMonth and
				sc.SectionID_2 = @SectionID
	END
	ELSE
	BEGIN
		select top 1 @StartDate = convert(varchar(10),s.BeginDate,120), 
			@EndDate = convert(varchar(10),s.EndDate,120), 
			@diffs = datediff(d,s.BeginDate,s.EndDate) + 1, 
			@TotalShifts = s.TotalShifts, 
			@CalendarCode = s.CalendarCode 
		from SECCAL s 
		inner join Section_Complete sc on 
			sc.ProdMonth = s.Prodmonth and 
			sc.SectionID_1 = s.SectionID 
		where s.ProdMonth = @ProdMonth and
				sc.SectionID_1 = @SectionID
	END
END
IF (@TypeMonth = 'Mill')
BEGIN
	select @StartDate = convert(varchar(10),a.StartDate,120), 
		@EndDate = convert(varchar(10),a.EndDate,120), 
		@diffs = datediff(d,a.Startdate,a.enddate) + 1, 
		@TotalShifts = a.TotalShifts 
	from 
    (
		SELECT DISTINCT StartDate, EndDate, TotalShifts 
		from CALENDARMILL 
		where MillMonth = @ProdMonth and 
			  CalendarCode = @TypeMonth
	) a 
END

declare @TheSICKey int 
declare @TheKPI varchar(30) 
declare @TheDescription varchar(50)

IF (@KPI <> 'Cleaned')
BEGIN
	--delete  from Linda
	if object_id('#tempSic') is not null
   		drop table #tempSic
	CREATE TABLE #tempSic(SICKey int, KPI varchar(30) , Description VARCHAR(50), 
							CalendarDate DateTime, ShiftNo varchar(2), TotalShifts varchar(2), 
							WorkingDay varchar(1), TheValue numeric(11,4), ProgValue numeric(11,4), ShiftValue decimal)

	DECLARE AA_Cursor CURSOR FOR
	select SICKey, KPI, Description from Code_SICCapture where KPI = @KPI

	OPEN AA_Cursor;
	FETCH NEXT FROM AA_Cursor
	into @TheSICKey, @TheKPI, @TheDescription;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		insert into #tempSic  --Linda
		select @TheSICKey, @TheKPI, @TheDescription, 
		ct.CalendarDate,
		ShiftNo = (select count(calendarDate) ShiftNo from Caltype where CalendarCode = @TypeMonth and
				   CalendarDate >= @StartDate and CalendarDate <= ct.CalendarDate and
				   WorkingDay = 'Y'),
		cc.TotalShifts, ct.WorkingDay,  
		TheValue = isnull(s.[Value],0), 0, 0
		from CalType ct
		left outer join SicCapture s on
		   s.CalendarDate = ct.CalendarDate and
		   s.SicKey = @TheSICKey and
		   s.SectionID = @SectionID
		left outer join 
			(select CalendarCode, count(CalendarDate) TotalShifts from CalType
			 where CalendarCode = @TypeMonth and
				   CalendarDate >= @StartDate and
				   CalendarDate <= @EndDate
			 group by CalendarCode) cc on
			ct.CalendarCode = cc.CalendarCode
		where ct.CalendarCode = @TypeMonth and
			@StartDate <= ct.CalendarDate and
			@EndDate >= ct.CalendarDate
		FETCH NEXT FROM AA_Cursor
		 into @TheSICKey, @TheKPI, @TheDescription;
	END; 
	CLOSE AA_Cursor;
	DEALLOCATE AA_Cursor;

	update #tempSic  --Linda
	set ProgValue =
	(select sum(Value) sumValue from SICCAPTURE where
	  SectionID = @SectionID and
	  CalendarDate >= @StartDate and
	  CalendarDate <= @EndDate and
	  SICKey = #tempSic.SICKey)  --linda.SICKey)

	update #tempSic  --Linda
	set ShiftValue =
	(select shiftValue = sum(case when Value > 0 then 1 else 0 end) from SICCAPTURE where
	  SectionID = @SectionID and
	  CalendarDate >= @StartDate and
	  CalendarDate <= @EndDate and
	  SICKey = #tempSic.SICKey)  --linda.SICKey)

	select SICKey, KPI, Description, 
		CalendarDate, 
		ShiftNo = case when WorkingDay = 'Y' then ShiftNo else 'N' end, 
		TotalShifts, 
		TheValue = case when isnull(TheValue,0) = 0 then '' else
		convert(varchar(10), convert(numeric(7,2),TheValue)) end,
		ProgValue = case when SicKey in (7,14,21) then ''
				when ShiftValue > 0 then convert(varchar(10), convert(numeric(7,2),ProgValue / ShiftValue))
				else '' end					
	from #tempSic  --Linda
	order by SicKey, CalendarDate

	DEALLOCATE aa_cursor
	drop table #tempSic
END
ELSE
BEGIN
	select * from
	(
		select SICKey = case 
					  when z.[Type] = 'Book FL' then ''
					  when z.[Type] = 'Plan' then '22'
					  when z.[Type] = 'Book' then '23' else '' end,
		  p.Sectionid +' : '+sc.Name Name, 
		  --p.Sectionid SectionID, sc.Name Name, 
		  p.Workplaceid+' : '+w.Description Workplace,
		  --p.Workplaceid WorkplaceID, w.Description Description, 
		  p.Activity Activity, 
		  p.CalendarDate CalendarDate,
		  s23.ProblemCode, s23.SBNotes, isnull(s23.CausedLostBlast,'') CausedLostBlast,
		  ProblemDesc = ppp.Description, 
		  ShiftNo = case when ct.WorkingDay = 'Y' then convert(varchar(2), p.ShiftDay) else 'N' end, 
		  TheValue = case 
					  when z.[Type] = 'Book FL' and p.Activity <> 1 and isnull(p.BookFL,0) = 0 and ct.WorkingDay = 'Y' then '0'
					  when z.[Type] = 'Book FL' and p.Activity <> 1 and isnull(p.BookFL,0) > 0 and ct.WorkingDay = 'Y' then 
							convert(varchar(10), convert(Numeric(7,0),isnull(p.BookFL,0)))
					  when z.[Type] = 'Plan' and p.Activity <> 1 and isnull(s22.Value,0) = 0 then ''
					  when z.[Type] = 'Plan' and p.Activity <> 1 and isnull(s22.Value,0) > 0 then 
							convert(varchar(10), convert(Numeric(7,0),isnull(s22.Value,0)))
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.ProblemCode, '') <> '' then 
							isnull(s23.ProblemCode, '')+':'+isnull(ppp.Description, '')
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.Value,0) = 0 then '' 
					  when z.[Type] = 'Book' and p.Activity <> 1 and isnull(s23.Value,0) > 0 then
						   convert(varchar(10), convert(Numeric(7,0),isnull(s23.Value, 0)))
					  when z.[Type] = 'Book FL' and p.Activity = 1 then '' 
					  when z.[Type] = 'Plan' and p.Activity = 1 and isnull(s22.Value,0) = 0 then ''
					  when z.[Type] = 'Plan' and p.Activity = 1 and isnull(s22.Value,0) = 99 then 
							'Yes'
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.ProblemCode, '') <> '' then 
							isnull(s23.ProblemCode, '')+':'+isnull(ppp.Description, '')
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.Value,0) = 0 then '' 
					  when z.[Type] = 'Book' and p.Activity = 1 and isnull(s23.Value,0) = 99 then
						  'Yes' else ''
				  end,
		  Type = z.[Type],
		  [Order] 
		from  Planning p 
		inner join Section_Complete sc on 
		  sc.ProdMonth = p.ProdMonth and 
		  sc.SectionID = p.SectionID
		inner join Seccal ss on
		  ss.ProdMonth = sc.ProdMonth and
		  ss.SectionID = sc.SectionID_1 
	    inner join CalType ct on
		  ss.CalendarCode = ct.CalendarCode and
		  ss.BeginDate <= ct.CalendarDate and
		  ss.Enddate >= ct.CalendarDate and
		  ct.CalendarDate = p.Calendardate
	    left outer join SICCapture s22 on
		  s22.SectionID = sc.SectionID_1 and
		  s22.WorkplaceID = p.WorkplaceID and
		  s22.CalendarDate = ct.CalendarDate and
		  s22.SICKey = 22
	    left outer join SICCapture s23 on
		  s23.SectionID = sc.SectionID_1 and
		  s23.WorkplaceID = p.WorkplaceID and
		  s23.CalendarDate = ct.CalendarDate and
		  s23.SICKey = 23
		left outer join CODE_PROBLEM ppp on
		  ppp.ProblemID = s23.ProblemCode and
		  ppp.Activity = p.Activity
	    inner join WORKPLACE w on 
		  w.Workplaceid = p.WorkplaceID,
	    (select [Order] = 1,
		  Type = 'Book FL'
	     union
	     select [Order] = 2,
		  Type = 'Plan'
	     union
	     select [Order] = 3,
		  Type = 'Book') z
	    where sc.SectionID_1 = @SectionID and 
			  p.Prodmonth = @ProdMonth and
			  p.PlanCode = 'MP' and
			  p.IsCubics = 'N'
	--and ct.calendardate <= ss.rundate
	 ) a 
	 order by Name, Workplace, CalendarDate, [Order]    
END
go
Alter table CODE_PROBLEM  
ADD [CausedLostBlast] varchar(2) NULL
GO

ALTER procedure [dbo].[sp_SurveyReport_StopingType] 
@TheReclamation varchar(1),
@ThePaid varchar(1),       --1=Paid, 2=Unpaid, 3=Paid and Unpaid
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheSectionID varchar (20),
@TheSelectBy varchar (100)
--exec Report_Survey_Stopingtype 'N','3','201112','201112',
as

declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)

set @TheQuery1 =  'select Stype = Stype,
    
    SqmTotal = sum(a.SqmTotal),
    SQMFW = sum(a.SQMFW),
    FW = case when sum(a.SqmReef) > 0 then sum(a.SQMFW) / sum(a.SqmReef) else 0 end,
    CW = case when sum(a.SqmReef) > 0 then sum(a.SQMCW) / sum(a.SqmReef) else 0 end,
    HW = case when sum(a.SqmReef) > 0 then sum(a.SQMHW) / sum(a.SqmReef) else 0 end,
    SQMCW = sum(a.SQMCW),
    SQMHW = sum(a.SQMHW),
    SQMSW = sum(a.SQMSW),
    BrokenSW = case when sum(a.BrokenSQM) > 0 then sum(a.BrokenSQMSW) / sum(a.BrokenSQM) else 0 end,
    ReefSW = case when sum(a.SqmReef) > 0 then sum(a.SQMReefSW) / sum(a.SqmReef) else 0 end,
    OSSW = case when sum(a.SqmOSTotal) > 0 then sum(a.StopeSqmOSSW) / sum(a.SqmOSTotal) else 0 end,
    OSFSW = case when sum(a.SqmOSFTotal) > 0 then sum(a.StopeSqmOSFSW) / sum(a.SqmOSFTotal) else 0 end,
    SqmReefSW = sum(a.SqmReefSW), '
IF @TheReclamation = 'Y'
BEGIN
  set @TheQuery1 = @TheQuery1 + '
    TonsReef = sum(a.TonsReef) - sum(a.RecTons),
    Content = sum(a.Content) - sum(a.RecGrams),
    SqmReef = sum(a.SqmReef) - sum(a.RecSqmTotal), 
	SqmOSTotal = sum(a.SqmOSTotal)  - sum(a.RecSqmOSTotal),
    StopeSqmOS = sum(a.StopeSqmOS) - sum(a.RecStopeSqmOS),
    LedgeSqm = sum(a.LedgeSqm) - sum(a.RecLedgeSqm),
    StopeSqm = sum(a.StopeSqm ) - sum(a.RecStopeSqm),
    SqmConvTotal = sum(a.SqmConvTotal) - sum(a.RecSqmConvTotal),
    SqmOSFTotal = sum(a.SqmOSFTotal) - sum(a.RecSqmOSFTotal),'
END
ELSE
BEGIN
  set @TheQuery1 = @TheQuery1 + '
    TonsReef = sum(a.TonsReef),
    Content = sum(a.Content),
    SqmReef = sum(a.SqmReef),
	SqmOSTotal = sum(a.SqmOSTotal),
    StopeSqmOS = sum(a.StopeSqmOS),
    LedgeSqm = sum(a.LedgeSqm),
    StopeSqm = sum(a.StopeSqm ),
    SqmConvTotal = sum(a.SqmConvTotal),
    SqmOSFTotal = sum(a.SqmOSFTotal), '
END    
set @TheQuery1 = @TheQuery1 + '
		TonsOSF = sum(a.TonsOSF),
		ASGTons = sum(a.ASGTons),
		TonsWaste = sum(a.TonsWaste),
		ExtraTons = sum(a.ExtraTons),
		PackedTons = sum(a.PackedTons),
		TonsTotal = sum(a.TonsTotal),
		LockUpTons = sum(a.LockUpTons),
		ReefGT = case when sum(a.TonsReef) > 0 then sum(a.Content) / sum(a.TonsReef) else 0 end,
		ReefCmgt = case when sum(a.SqmReef) > 0 then sum(a.SqmReefcmgt) / sum(a.SqmReef) else 0 end,
		TotalReefGT = case when sum(a.TonsReef) + sum(a.TonsWaste) > 0 
				then sum(a.Content) / (sum(a.TonsReef) + sum(a.TonsWaste)) else 0 end,
		TotalReefCmgt = case when (sum(a.SqmOSTotal) + sum(a.StopeSqm) + 
								  sum(a.LedgeSqm) + sum(a.SqmConvTotal)) > 0 
							 then sum(a.SqmReefcmgt) / (sum(a.SqmOSTotal) + sum(a.StopeSqm) + 
								  sum(a.LedgeSqm) + sum(a.SqmConvTotal)) else 0 end,
		FAdv = case when sum(a.FLTotal) > 0 then sum(a.SqmTotal) / sum(a.FLTotal) else 0 end,
		LedgeFL = sum(a.LedgeFL),
		StopeFL = sum(a.StopeFL),
		StopeFLOS = sum(a.StopeFLOS),
		LedgeFLOS = sum(a.LedgeFLOS),
		FLOSTotal = sum(a.FLOSTotal),
		FLTotal = sum(a.FLTotal),
		RecSqmTotal = sum(a.RecSqmTotal),
		RecTons = sum(a.RecTons),
		RecGrams = sum(a.RecGrams),
		Destination = max(a.Destination), 
		ReefDesc = max(a.ReefDesc)
    from ( '

set  @TheQuery2 = 'select case when st.StopeTypeDesc is null then ''Open Mining'' else st.StopeTypeDesc end Stype,
  s.StopeSqmOS, s.LedgeSqm, s.StopeSqm, s.SqmConvTotal,
  s.SqmOSFTotal, s.SqmOSTotal, s.SqmTotal,
  SQMFW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.FW,
  SQMCW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.CW,
  SQMHW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.HW,
  SqmSW = s.SWSqm * s.SqmTotal,
  BrokenSQM = case when SWOSF <> 0 then s.SqmTotal else s.SqmTotal - s.SqmOSFTotal end,
  BrokenSQMSW = 
	case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) = 0 and isnull(s.SWOS,0) = 0 then 0 else
	  case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) = 0 and isnull(s.SWOS,0) <> 0 then 
	    isnull(s.SWOS,0) * (isnull(s.SqmTotal,0) - isnull(s.SqmOSFTotal,0)) else
		case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) <> 0 then 
			isnull(s.SWOSF,0) * isnull(s.SqmTotal,0) else	
			case when isnull(s.SWSqm,0) <> 0 and isnull(s.SWOSF,0) = 0 then 
				isnull(s.SWSQM,0) * (isnull(s.SqmTotal,0) - isnull(s.SqmOSFTotal,0)) else
				case when isnull(s.SWSqm,0) <> 0 and isnull(s.SWOSF,0) <> 0 then 
					isnull(s.SWSQM,0) * isnull(s.SqmTotal,0) 
				end			
			end
		end
	  end
	end,  	
  TonsTotal = isnull(((s.StopeSqm + s.LedgeSqm + SqmConvTotal) * s.SWSQM / 100 * s.Density),0) + isnull(s.TonsOS,0) + isnull(s.TonsOSF,0) + 
  ((s.CubicsReef + s.CubicsWaste)*s.density) +
			(case when s.measheight*100 > s.SWSqm then
              (((s.Wastemetres*s.measwidth*s.measheight)*s.density+s.reefmetres *
              (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
            else
              (((s.Wastemetres*s.measwidth*s.measheight)*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
            end),
  TonsReef = (s.StopeSqm + s.LedgeSqm + SqmConvTotal) * s.SWSQM / 100 * s.Density,
  TonsWaste = s.TonsOS, LockUpTons = s.LockupTons, 
  s.TonsOSF,
  ASGTons = case when s.measheight*100 > s.SWSqm then
              (s.Wastemetres*s.measwidth*s.measheight*s.density)+(s.reefmetres *
              (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)
            else
              (s.Wastemetres*s.measwidth*s.measheight*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)
            end,
  ExtraTons = (s.CubicsReef + s.CubicsWaste)*s.density,
  PackedTons = case when Destination = 0 then
          		            ((s.CubicsReef + s.CubicsWaste)*s.density) +
          			        (s.SqmOSFTotal*s.SWOSF/100*s.density) +
          				    (((s.SqmOSTotal)*s.SWOS/100)*s.density) +
          					        (((s.StopeSqm+s.LedgeSqm+s.SqmConvTotal)*s.SWSqm/100)*s.density) +
              (case when
          		                s.measheight*100 > s.SWSqm then
          	                (((s.Wastemetres*s.measwidth*s.measheight)*s.density+s.reefmetres*
          		                (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
          		                (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
              else
          	                (((s.Wastemetres*s.measwidth*s.measheight)*s.density) +
          	                (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)) end) else 0 end,
  s.LedgeFL, --Ledge FL m
  s.StopeFL, --Stope FL m
  s.StopeFLOS, --OS Stope FL m
  s.LedgeFLOS, --OS Ledge FL m
  s.FLOSTotal, --OS  FL m
  s.FLTotal, --Total FL
  destination = case when s.Destination = ''0'' then ''P'' else
                case when s.Destination = ''1'' then ''D'' else ''M'' end end,
  r.Description ReefDesc,
  SQMReefSW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.SWSqm, --Reef SW cm
  StopeSqmOSSW = s.SqmOSTotal * s.SWOS,
  StopeSqmOSFSW = s.SqmOSFTotal * s.SWOSF,
  Content = case when s.SWSqm <> 0 then
              (((s.SqmTotal-s.SqmOSTotal-s.SqmOSFTotal)*s.cmgt*s.density/100)+
              (s.cubicsreef*(s.cmgt/(s.SWSqm)*s.density))) else 0 end,
  SqmReef = s.StopeSqm+s.LedgeSqm+s.SqmConvTotal,
  SqmReefcmgt = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.cmgt,
  SqmTotalcmgt = s.SqmTotal * s.cmgt,

 
  RecSqmOSTotal = case when Stype = 15 then s.SqmOSTotal else 0 end,
  RecSqmTotal = case when Stype = 15 then s.SqmTotal else 0 end,
  RecSqmConvTotal = case when Stype = 15 then s.SqmConvTotal else 0 end,
  RecSqmOSFTotal = case when Stype = 15 then s.SqmOSFTotal else 0 end,
  RecStopeSqmOS = case when Stype = 15 then s.StopesqmOS else 0 end,
  RecLedgeSqm = case when Stype = 15 then s.LedgeSqm else 0 end,
  RecStopeSqm = case when Stype = 15 then s.StopeSqm else 0 end,
  RecTons = case when stype = 15 then
      (((s.StopeSqm+s.LedgeSqm+s.SqmConvTotal)*s.SWSqm/100)*s.density) else 0 end,
  RecGrams = case when stype = 15 then
                 case when s.SWSqm <> 0 then
 	                (((s.SqmTotal-s.SqmOSTotal-s.SqmOSFTotal)*s.cmgt*s.density/100)+
                  (s.cubicsreef*(s.cmgt/(s.SWSqm)*s.density)))
                 else 0 end
              else 0 end	
  from Survey s '
set @TheQuery2 = @TheQuery2 + '
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid
  left outer join code_stopetypes st on
  st.stopetypeid = s.stype, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
      s.Activity IN(0,9) and
      (isnull(s.SqmTotal,0) > 0 or isnull(s.ReefMetres,0) > 0 or isnull(s.WasteMetres,0) > 0 or 
      isnull(CubicsReef,0) > 0 or isnull(CubicsWaste,0) > 0) and '
IF @ThePaid = '1'     
   set @TheQuery2 = @TheQuery2 + ' s.Payment <> 1 and '
IF @ThePaid = '2'     
   set @TheQuery2 = @TheQuery2 + ' s.Payment = 1 and '  
set @TheQuery2 = @TheQuery2 + 
	' r.reefid in ' + @TheReef + ' and
	  sh.oreflowid in ' + @TheShaft + ' and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' ) a  group by Stype with rollup '

--print @TheQuery1
--print @TheQuery2
exec (@TheQuery1+@TheQuery2)

go

ALTER 
procedure [dbo].[sp_SurveyReport_Stoping] 
--declare
@TheTypeReport varchar(1), --1=Stoping, 2=Development, 3=Sweepings, 4=Total Mine
@TheReclamation varchar(1),
@TheSection varchar(1),    --1=SectionID, 2=SectionID+Name, 3=Name
@ThePaid varchar(1),       --1=Paid, 2=Unpaid, 3=Paid and Unpaid
@TheFromMonth varchar(10),
@TheToMonth varchar(10),
@TheReef varchar(1000),
@TheShaft varchar(200),
@TheSectionID varchar (20),
@TheSelectBy varchar (100)
--exec Report_Survey_Stoping] '1','N','2','3','201112','201112',
as

declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)

IF @TheTypeReport = '1'
BEGIN
  set @TheQuery1 = 
  ' select isnull(a.name_5,'''') name_5, isnull(a.name_4,'''') name_4, 
           isnull(a.name_3,'''') name_3, isnull(a.name_2,'''') name_2, 
           isnull(a.name_1,'''') name_1, isnull(a.name,'''') name, 
           isnull(a.Workplace,'''') Workplace, isnull(a.SeqNo,0) SeqNo, ' 
END

IF @TheTypeReport = '4'
BEGIN
  set @TheQuery1 = 
  ' select isnull(a.name_5,'''') name_5, '''' name_4, '''' name_3, '''' name_2, 
           '''' name_1, '''' name, '''' Workplace, 0 SeqNo, '
END
set @TheQuery1 = @TheQuery1 + '
    CostArea = max(CostArea),
    
    SqmTotal = sum(a.SqmTotal),
    SQMFW = sum(a.SQMFW),
    FW = case when sum(a.SqmReef) > 0 then sum(a.SQMFW) / sum(a.SqmReef) else 0 end,
    CW = case when sum(a.SqmReef) > 0 then sum(a.SQMCW) / sum(a.SqmReef) else 0 end,
    HW = case when sum(a.SqmReef) > 0 then sum(a.SQMHW) / sum(a.SqmReef) else 0 end,
    SQMCW = sum(a.SQMCW),
    SQMHW = sum(a.SQMHW),
    SQMSW = sum(a.SQMSW),
    BrokenSW = case when sum(a.BrokenSQM) > 0 then sum(a.BrokenSQMSW) / sum(a.BrokenSQM) else 0 end,
    ReefSW = case when sum(a.SqmReef) > 0 then sum(a.SQMReefSW) / sum(a.SqmReef) else 0 end,
    OSSW = case when sum(a.SqmOSTotal) > 0 then sum(a.StopeSqmOSSW) / sum(a.SqmOSTotal) else 0 end,
    OSFSW = case when sum(a.SqmOSFTotal) > 0 then sum(a.StopeSqmOSFSW) / sum(a.SqmOSFTotal) else 0 end,
    SqmReefSW = sum(a.SqmReefSW), '
IF @TheReclamation = 'Y'
BEGIN
  set @TheQuery1 = @TheQuery1 + '
    TonsReef = sum(a.TonsReef) - sum(a.RecTons),
    Content = sum(a.Content) - sum(a.RecGrams),
    SqmReef = sum(a.SqmReef) - sum(a.RecSqmTotal), 
	SqmOSTotal = sum(a.SqmOSTotal)  - sum(a.RecSqmOSTotal),
    StopeSqmOS = sum(a.StopeSqmOS) - sum(a.RecStopeSqmOS),
    LedgeSqm = sum(a.LedgeSqm) - sum(a.RecLedgeSqm),
    StopeSqm = sum(a.StopeSqm ) - sum(a.RecStopeSqm),
    SqmConvTotal = sum(a.SqmConvTotal) - sum(a.RecSqmConvTotal),
    SqmOSFTotal = sum(a.SqmOSFTotal) - sum(a.RecSqmOSFTotal),'
END
ELSE
BEGIN
  set @TheQuery1 = @TheQuery1 + '
    TonsReef = sum(a.TonsReef),
    Content = sum(a.Content),
    SqmReef = sum(a.SqmReef),
	SqmOSTotal = sum(a.SqmOSTotal),
    StopeSqmOS = sum(a.StopeSqmOS),
    LedgeSqm = sum(a.LedgeSqm),
    StopeSqm = sum(a.StopeSqm ),
    SqmConvTotal = sum(a.SqmConvTotal),
    SqmOSFTotal = sum(a.SqmOSFTotal), '
END    
set @TheQuery1 = @TheQuery1 + '
		TonsOSF = sum(a.TonsOSF),
		ASGTons = sum(a.ASGTons),
		TonsWaste = sum(a.TonsWaste),
		ExtraTons = sum(a.ExtraTons),
		PackedTons = sum(a.PackedTons),
		TonsTotal = sum(a.TonsTotal),
		LockUpTons = sum(a.LockUpTons),
		ReefGT = case when sum(a.TonsReef) > 0 then sum(a.Content) / sum(a.TonsReef) else 0 end,
		ReefCmgt = case when sum(a.SqmReef) > 0 then sum(a.SqmReefcmgt) / sum(a.SqmReef) else 0 end,
		TotalReefGT = case when sum(a.TonsReef) + sum(a.TonsWaste) > 0 
				then sum(a.Content) / (sum(a.TonsReef) + sum(a.TonsWaste)) else 0 end,
		TotalReefCmgt = case when (sum(a.SqmOSTotal) + sum(a.StopeSqm) + 
								  sum(a.LedgeSqm) + sum(a.SqmConvTotal)) > 0 
							 then sum(a.SqmReefcmgt) / (sum(a.SqmOSTotal) + sum(a.StopeSqm) + 
								  sum(a.LedgeSqm) + sum(a.SqmConvTotal)) else 0 end,
		FAdv = case when sum(a.FLTotal) > 0 then sum(a.SqmTotal) / sum(a.FLTotal) else 0 end,
		LedgeFL = sum(a.LedgeFL),
		StopeFL = sum(a.StopeFL),
		StopeFLOS = sum(a.StopeFLOS),
		LedgeFLOS = sum(a.LedgeFLOS),
		FLOSTotal = sum(a.FLOSTotal),
		FLTotal = sum(a.FLTotal),
		RecSqmTotal = sum(a.RecSqmTotal),
		RecTons = sum(a.RecTons),
		RecGrams = sum(a.RecGrams),
		Destination = max(a.Destination), 
		ReefDesc = max(a.ReefDesc)
    from ( '
IF @TheTypeReport = '1'
BEGIN
  IF @TheSection = '1'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.sectionid_5 name_5, sc.sectionid_4 name_4,
			   sc.sectionid_3 name_3,sc.sectionid_2 name_2 ,
			   sc.sectionid_1 name_1, sc.sectionid name, '
  END
  IF @TheSection = '2'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select  sc.sectionid_5 +'':''+sc.name_5 name_5, sc.sectionid_4 +'':''+sc.name_4 name_4,
				sc.sectionid_3 +'':''+sc.name_3 name_3,sc.sectionid_2 +'':''+sc.name_2 name_2,
                sc.sectionid_1 +'':''+sc.name_1 name_1, sc.sectionid +'':''+sc.name name, ' 
  END
  IF @TheSection = '3'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.name_5 name_5, sc.name_4 name_4,
               sc.name_3 name_3, sc.name_2 name_2,
               sc.name_1 name_1, sc.name name, '
               
  END
  set @TheQuery1 = @TheQuery1 + ' s.WorkPlaceID+'':''+w.Description Workplace, isnull(s.SeqNo,0) SeqNo, ' 
END

IF @TheTypeReport = '4'
BEGIN
  IF @TheSection = '1'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.sectionid_5 name_5, '
  END
  IF @TheSection = '2'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select  sc.sectionid_5 +'':''+sc.name_5 name_5, ' 
  END
  IF @TheSection = '3'
  BEGIN
  set @TheQuery1 = @TheQuery1 +
	  ' select sc.name_5 name_5, '
               
  END
  set @TheQuery1 = @TheQuery1 + ' '''' Workplace, 0 SeqNo, '
END
set  @TheQuery2 = '
  CostArea = '''',
  s.StopeSqmOS, s.LedgeSqm, s.StopeSqm, s.SqmConvTotal,
  s.SqmOSFTotal, s.SqmOSTotal, s.SqmTotal,
  SQMFW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.FW,
  SQMCW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.CW,
  SQMHW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.HW,
  SqmSW = s.SWSqm * s.SqmTotal,
  BrokenSQM = case when SWOSF <> 0 then s.SqmTotal else s.SqmTotal - s.SqmOSFTotal end,
  BrokenSQMSW = 
	case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) = 0 and isnull(s.SWOS,0) = 0 then 0 else
	  case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) = 0 and isnull(s.SWOS,0) <> 0 then 
	    isnull(s.SWOS,0) * (isnull(s.SqmTotal,0) - isnull(s.SqmOSFTotal,0)) else
		case when isnull(s.SWSqm,0) = 0 and isnull(s.SWOSF,0) <> 0 then 
			isnull(s.SWOSF,0) * isnull(s.SqmTotal,0) else	
			case when isnull(s.SWSqm,0) <> 0 and isnull(s.SWOSF,0) = 0 then 
				isnull(s.SWSQM,0) * (isnull(s.SqmTotal,0) - isnull(s.SqmOSFTotal,0)) else
				case when isnull(s.SWSqm,0) <> 0 and isnull(s.SWOSF,0) <> 0 then 
					isnull(s.SWSQM,0) * isnull(s.SqmTotal,0) 
				end			
			end
		end
	  end
	end,  	
  TonsTotal = isnull(((s.StopeSqm + s.LedgeSqm + SqmConvTotal) * s.SWSQM / 100 * s.Density),0) + isnull(s.TonsOS,0) + isnull(s.TonsOSF,0) + 
  ((s.CubicsReef + s.CubicsWaste)*s.density) +
			(case when s.measheight*100 > s.SWSqm then
              (((s.Wastemetres*s.measwidth*s.measheight)*s.density+s.reefmetres *
              (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
            else
              (((s.Wastemetres*s.measwidth*s.measheight)*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
            end),
  TonsReef = (s.StopeSqm + s.LedgeSqm + SqmConvTotal) * s.SWSQM / 100 * s.Density,
  TonsWaste = s.TonsOS, LockUpTons = s.LockupTons, 
  s.TonsOSF,
  ASGTons = case when s.measheight*100 > s.SWSqm then
              (s.Wastemetres*s.measwidth*s.measheight*s.density)+(s.reefmetres *
              (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)
            else
              (s.Wastemetres*s.measwidth*s.measheight*s.density) +
              (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)
            end,
  ExtraTons = (s.CubicsReef + s.CubicsWaste)*s.density,
  PackedTons = case when Destination = 0 then
          		            ((s.CubicsReef + s.CubicsWaste)*s.density) +
          			        (s.SqmOSFTotal*s.SWOSF/100*s.density) +
          				    (((s.SqmOSTotal)*s.SWOS/100)*s.density) +
          					        (((s.StopeSqm+s.LedgeSqm+s.SqmConvTotal)*s.SWSqm/100)*s.density) +
              (case when
          		                s.measheight*100 > s.SWSqm then
          	                (((s.Wastemetres*s.measwidth*s.measheight)*s.density+s.reefmetres*
          		                (s.measheight-s.SWSqm/100)*s.measwidth*s.density) +
          		                (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density))
              else
          	                (((s.Wastemetres*s.measwidth*s.measheight)*s.density) +
          	                (s.Reefmetres * s.SWSqm/100 * s.measwidth * s.density)) end) else 0 end,
  s.LedgeFL, --Ledge FL m
  s.StopeFL, --Stope FL m
  s.StopeFLOS, --OS Stope FL m
  s.LedgeFLOS, --OS Ledge FL m
  s.FLOSTotal, --OS  FL m
  s.FLTotal, --Total FL
  destination = case when s.Destination = ''0'' then ''P'' else
                case when s.Destination = ''1'' then ''D'' else ''M'' end end,
  r.Description ReefDesc,
  SQMReefSW = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.SWSqm, --Reef SW cm
  StopeSqmOSSW = s.SqmOSTotal * s.SWOS,
  StopeSqmOSFSW = s.SqmOSFTotal * s.SWOSF,
  Content = case when s.SWSqm <> 0 then
              (((s.SqmTotal-s.SqmOSTotal-s.SqmOSFTotal)*s.cmgt*s.density/100)+
              (s.cubicsreef*(s.cmgt/(s.SWSqm)*s.density))) else 0 end,
  SqmReef = s.StopeSqm+s.LedgeSqm+s.SqmConvTotal,
  SqmReefcmgt = (s.StopeSqm+s.LedgeSqm+s.SqmConvTotal) * s.cmgt,
  SqmTotalcmgt = s.SqmTotal * s.cmgt,

 
  RecSqmOSTotal = case when Stype = 15 then s.SqmOSTotal else 0 end,
  RecSqmTotal = case when Stype = 15 then s.SqmTotal else 0 end,
  RecSqmConvTotal = case when Stype = 15 then s.SqmConvTotal else 0 end,
  RecSqmOSFTotal = case when Stype = 15 then s.SqmOSFTotal else 0 end,
  RecStopeSqmOS = case when Stype = 15 then s.StopesqmOS else 0 end,
  RecLedgeSqm = case when Stype = 15 then s.LedgeSqm else 0 end,
  RecStopeSqm = case when Stype = 15 then s.StopeSqm else 0 end,
  RecTons = case when stype = 15 then
      (((s.StopeSqm+s.LedgeSqm+s.SqmConvTotal)*s.SWSqm/100)*s.density) else 0 end,
  RecGrams = case when stype = 15 then
                 case when s.SWSqm <> 0 then
 	                (((s.SqmTotal-s.SqmOSTotal-s.SqmOSFTotal)*s.cmgt*s.density/100)+
                  (s.cubicsreef*(s.cmgt/(s.SWSqm)*s.density)))
                 else 0 end
              else 0 end	
  from Survey s '
set @TheQuery2 = @TheQuery2 + '
  inner join Section_Complete sc on
    sc.Prodmonth = s.ProdMonth and
    sc.SECTIONID = s.SectionID
  INNER JOIN WORKPLACE w ON 
    s.WorkPlaceID=w.WorkplaceID
  inner join reef r on 
    r.reefid = w.reefid
  inner join oreflowentities le on
    le.oreflowid = w.oreflowid
  inner join oreflowentities ore on
    ore.oreflowid = le.parentoreflowid
  inner join oreflowentities sh on
    sh.oreflowid = ore.parentoreflowid, SysSet sy
  WHERE  s.Prodmonth >= ''' + @TheFromMonth + ''' and 
      s.Prodmonth <= ''' + @TheToMonth + ''' and
      s.Activity IN(0,9) and
      (isnull(s.SqmTotal,0) > 0 or isnull(s.ReefMetres,0) > 0 or isnull(s.WasteMetres,0) > 0 or 
      isnull(CubicsReef,0) > 0 or isnull(CubicsWaste,0) > 0 or isnull(LockUpTons,0) > 0) and '
IF @ThePaid = '1'     
   set @TheQuery2 = @TheQuery2 + ' s.Payment <> 1 and '
IF @ThePaid = '2'     
   set @TheQuery2 = @TheQuery2 + ' s.Payment = 1 and '  
set @TheQuery2 = @TheQuery2 + 
	' r.reefid in ' + @TheReef + ' and
	  sh.oreflowid in ' + @TheShaft + ' and
      ' + @TheSelectBy + ' = ''' + @TheSectionID + ''' ) a '
IF @TheTypeReport = '1'  
BEGIN            
  set @TheQuery2 = @TheQuery2 + 
    ' group by a.name_5, a.name_4, a.name_3, a.name_2,
               a.name_1, a.name, a.Workplace, a.SeqNo with rollup'
END 
IF @TheTypeReport = '4'  
BEGIN            
  set @TheQuery2 = @TheQuery2 + ' group by a.name_5 with rollup '
END  

--print @TheQuery1
--print @TheQuery2
exec (@TheQuery1+@TheQuery2)

go

 ALTER Procedure [dbo].[sp_Load_BookABSDevelopment_Detail] 
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
		@BookDate varchar(10)
as

--set @Prodmonth = 201707
--set @SectionID = 'RElD'
--set @BookDate = '2017-06-14'


Declare @SQL VarChar(8000)
Declare @SQL1 VarChar(8000)

set @SQL = ' 
 select * from 
  ( 
     select pm.ProdMonth, pm.sectionid Sec, pm.sectionid SectionID, 
            pm.workplaceid WPID, w.Description, 
            pm.workplaceid + '':'' + w.Description WP, pm.ReefWaste,   
            1 Activity, ''Development'' ActDesc, pd.ShiftDay, 
            isnull(pm.OrgUnitDay, '''') OrgUnitDS, 
           CalendarDate = convert(varchar(10), pd.CalendarDate, 120), 
           isnull(pd.ABSPicNo, '''') ABSPicNo, 
           ABSCodeDisplay = case when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                                when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = '''' then ''S'' 
                                when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                                when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = '''' then ''B'' 
                                when isnull(pd.ABSCode, '''') = ''A'' then ''A'' else '''' end, 
           isnull(pd.ABSCode, '''') ABSCode, 
           isnull(pd.ABSPrec, '''') ABSPrec, 
           isnull(pd.ABSNotes, '''') ABSNotes, 
           --isnull(pd.PegID, '''') PegID, 
		   PegID = pd.PegID, -- +'':''+convert(varchar(10), cast(pg.Value as numeric(10,1))),
		   isnull(pd.PegDist, 0) PegDist, 
           cast(isnull(pd.PegToFace, 0) as numeric(10,1)) PegToFace, 
		  -- isnull(pd.PegToFace, 0) PegToFaceOld, 
		   PPegID = case when isnull(c.ppeg,'''') = '''' then (case when isnull(sss.ppeg,'''') ='''' or (sss.cal < e.cal) then 
		                     isnull(e.ppeg,'''') else isnull(sss.ppeg,'''') end) else isnull(c.ppeg,'''') end ,  
			PPegToFace = case when c.ppegtoface is null then (case when sss.ppegtoface is null or (sss.cal < e.cal) then 
							isnull(e.ppegtoface,0) else isnull(sss.ppegtoface,0) end) else isnull(c.ppegtoface,0) end,
			PPegDist = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
							isnull(e.ppegdist,0) else isnull(sss.ppegdist,0) end)  else isnull(c.ppegdist,0) end,
			--PegFrom = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
			--				e.ppegdist else sss.ppegdist end)  else c.ppegdist end,
			--PegTo = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
			--				convert(e.ppegdist + e.ppeg else sss.ppegdist+sss.ppeg end)  else c.ppegdist + c.ppeg end,
           isnull(pd.BookMetresAdvance, 0) BookAdv, 
		   isnull(pd.BookReefAdv, 0) BookReefAdv, 
		   isnull(pd.BookWasteAdv, 0) BookWasteAdv, 
		   isnull(pd.BookTons, 0) BookTons,
		   isnull(pd.BookReefTons, 0) BookReefTons,
		   isnull(pd.BookWasteTons, 0) BookWasteTons,
           isnull(pd.BookGrams, 0) BookGrams, BookKG = isnull(pd.BookGrams, 0) / 1000, 
          -- isnull(pd.BookCubics, 0) BookCubics, isnull(pd.BookSweeps, 0) BookSweeps, 
          -- isnull(pd.BookResweeps, 0) BookReSweeps, isnull(pd.BookVamps, 0) BookVamps, 
           --BookTotal = isnull(pd.BookMetresAdvance, 0) + isnull(pd.BookSecM, 0), 
          -- isnull(pd.BookOpenUp, 0) BookOpenUp, isnull(pd.BookSecM, 0) BookSecM, 
           DHeight = case when isnull(pm.DHeight, 0) = 0 then w.EndHeight else isnull(pm.DHeight, 0) end, 
		   DWidth =  case when isnull(pm.DWidth, 0) = 0 then w.EndWidth else isnull(pm.DWidth, 0) end, 
		   isnull(ss.RockDensity, 0) Density, 
           isnull(pm.GT,0) gt, 
		   isnull(pm.CMGT,0) cmgt, 
           BookCodeDev = case when prbook.ProblemID = ''ST'' then prbook.ProblemID else isnull(pd.BookCode,'''') end, 
           isnull(pd.ProblemID, '''') ProblemID, isnull(pd.SBossNotes, '''') SBossNotes,
		    isnull(pd.CausedLostBlast, '''') CausedLostBlast
      from planmonth pm 
      inner join planning pd on 
        pm.Prodmonth = pd.Prodmonth and 
        pm.SectionID = pd.SectionID and 
        pm.WorkplaceID = pd.WorkplaceID and 
        pm.Activity = pd.Activity and 
        pm.PlanCode = pd.PlanCode and
		pm.IsCubics = pd.IsCubics
	left outer join 

		(select a.wp1, b.bookcode prevcode,  b.pegid ppeg, 
		ppegtoface = case when isnull(b.pegtoface,0) = 0 then 0 
					      when convert(varchar(10),b.pegtoface) = '''' then 0 else b.pegtoface end,
		ppegdist = case when isnull(b.pegdist,0) = 0 then 0 
					      when convert(varchar(10),b.pegdist) = '''' then 0 else b.pegdist end,
		cal 
		from 
			(select p.workplaceid wp1,  max(calendardate) cal 
			from planning p 
			where calendardate < '''+@BookDate+''' and prodmonth = '''+@Prodmonth+''' and isCubics = ''N'' and PlanCode=''MP''
			   and p.activity in (1) and bookmetresadvance is not null group by p.workplaceid
			) a 
			left outer join 
				(select workplaceid , max(bookcode)  bookcode,  max(pegid)  pegid, max(pegtoface) pegtoface, max(pegdist) pegdist,  calendardate 
				from planning 
				where bookmetresadvance is not null and activity in (1) and isCubics = ''N'' and PlanCode=''MP''
				group by workplaceid, calendardate
				) b on a.wp1 = b.workplaceid and a.cal = b.calendardate 
		) c on pm.WorkplaceID = c.wp1 
 
		left outer join  
		 (select a.wp1,  b.pegid + '':'' + convert(varchar(10),b.pegvalue) ppeg, 
		  ppegtoface = case when isnull(b.pegtoface,0) = 0 then 0 
					      when convert(varchar(10),b.pegtoface) = '''' then 0 else b.pegtoface end,
		ppegdist = case when isnull(b.pegdist,0) = 0 then 0 
					      when convert(varchar(10),b.pegdist) = '''' then 0 else b.pegdist end,
		  --b.pegtoface ppegtoface, b.pegdist ppegdist, 
		  cal 
		 from    
			(select p.workplaceid wp1,  max(calendardate) cal	  
			from survey p 
			where prodmonth < '''+@Prodmonth+''' and p.activity in (1) and mainmetres is not null 
			group by p.workplaceid
			) a    
		 left outer join    
			(select workplaceid ,  max(pegno)  pegid, max(pegvalue) pegvalue, max(pegtoface) pegtoface, max(progto) pegdist, calendardate    
			from survey
			where mainmetres is not null and activity in (1) 
			group by workplaceid, calendardate
			) b  on a.wp1 = b.workplaceid and a.cal = b.calendardate
		) sss on pm.WorkplaceID = sss.wp1  '
set @SQL1 = ' 

		left outer join  
			(select a.wp1, b.bookcode prevcode,  b.pegid ppeg, 
			ppegtoface = case when isnull(b.pegtoface,0) = 0 then 0 
					      when convert(varchar(10),b.pegtoface) = '''' then 0 else b.pegtoface end,
		ppegdist = case when isnull(b.pegdist,0) = 0 then 0 
					      when convert(varchar(10),b.pegdist) = '''' then 0 else b.pegdist end,
			--b.pegtoface ppegtoface, b.pegdist ppegdist, 
			cal 
			from 
				(select p.workplaceid wp1,  max(calendardate) cal  
				 from planning p 
				 where calendardate < '''+@BookDate+''' and p.activity in (1) and (bookmetresadvance > 0 or bookmetresadvance < 0) 
				 group by p.workplaceid
				) a    
			left outer join    
				(select workplaceid , max(bookcode)  bookcode,  max(pegid)  pegid, max(pegtoface) pegtoface, max(pegdist) pegdist, calendardate    
				from planning 
				where bookcode is not null and activity in (1) 
				group by workplaceid, calendardate
				) b on a.wp1 = b.workplaceid and a.cal = b.calendardate 
		) e on pm.WorkplaceID = e.wp1  
      inner join SECTION_COMPLETE sc on 
        sc.ProdMonth = pm.ProdMonth and 
        sc.SectionID = pm.SectionID 
      inner join Seccal s on 
        s.ProdMonth = sc.ProdMonth and 
        s.SectionID = sc.SectionID_1 
      inner join Caltype ct on 
        ct.CalendarCode = s.CalendarCode and 
        ct.CalendarDate = pd.CalendarDate 
      left outer join Code_Activity act on 
        act.Activity = pm.Activity 
      inner join Workplace w on 
        pm.WorkplaceID = w.WorkplaceID and
		pm.Activity = w.Activity
	  left outer join Peg pg on
	    pg.PegID = pd.PegID and
		pg.WorkplaceID = pd.WorkplaceID
      left outer join 
       ( 
             select b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics, 
                max(CalendarDate) CalendarDate 
             from planning b 
             where ProdMonth = '''+@Prodmonth+''' and 
                   Activity = 1 and 
                   CalendarDate < '''+@BookDate+''' and 
                   PlanCode = ''MP'' and
				   IsCubics = ''N''
             group by b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics
       ) prbook1 on 
         pm.Prodmonth = prbook1.Prodmonth and 
         pm.SectionID = prbook1.SectionID and 
         pm.WorkplaceID = prbook1.WorkplaceID and 
         pm.Activity = prbook1.Activity and 
         pm.PlanCode = prbook1.PlanCode and
		 pm.IsCubics = prbook1.IsCubics
       left outer join Planning prbook on 
         prbook.Prodmonth = prbook1.Prodmonth and 
         prbook.SectionID = prbook1.SectionID and 
         prbook.WorkplaceID = prbook1.WorkplaceID and 
         prbook.Activity = prbook1.Activity and 
         prbook.PlanCode = prbook1.PlanCode and 
		 prbook.IsCubics = prbook1.IsCubics and
         prbook.Calendardate = prbook1.Calendardate 
      left outer join 
         (Select workplaceid, max(CalendarDate) CalendarDate from sampling group by workplaceid 
         ) a on a.WorkplaceID = pm.WorkplaceID 
      left outer join Sampling aa on 
        aa.WorkplaceID = pm.WorkplaceID and 
        aa.CalendarDate = a.calendarDate 
      left outer join 
          (select p.ProdMonth, WorkplaceID, p.SectionID, Activity, 
                  PlanCode, IsCubics, sum(BookSqm) progbook, sum(AdjSqm) AdjSqm 
           from planning p 
           inner 
           join SECTION_COMPLETE sc on 
             p.ProdMonth = sc.ProdMonth and 
             p.SectionID = sc.SectionID 
           where p.ProdMonth = '''+@Prodmonth+''' and 
                 p.calendardate < '''+@BookDate+''' and 
                 sc.SectionID_1 = '''+@SectionID+''' and 
                 p.Activity = 1 and
				 p.PlanCode = ''MP'' and
				 p.IsCubics = ''N'' 
           group by p.ProdMonth, WorkplaceID, p.SectionID, Activity, PlanCode, IsCubics
          ) ProgSum on 
             pm.Prodmonth = ProgSum.Prodmonth and 
             pm.SectionID = ProgSum.SectionID and 
             pm.WorkplaceID = ProgSum.WorkplaceID and 
             pm.Activity = ProgSum.Activity and 
             pm.PlanCode = ProgSum.PlanCode and
			 pm.IsCubics = ProgSum.IsCubics, Sysset ss
      where pm.ProdMonth = '''+@Prodmonth+''' and 
            pm.Activity = 1 and 
            pm.PlanCode = ''MP'' and 
			pm.IsCubics = ''N'' and 
            pd.CalendarDate = '''+@BookDate+''' and 
            pm.Metresadvance > 0 and 
            ct.WorkingDay = ''Y'' and 
            sc.SectionID_1 = '''+@SectionID+''' 
            --(p.Pumahola = ''Y'' or ct.WorkingDay = ''Y'')) a 
  ) z '

  --print (@SQL)
  -- print (@SQL1)
exec (@SQL+@SQL1)
go
CREATE View [dbo].[vw_CALTYPES] as
select calendarcode, calendardate, workingday from caltype
GO
CREATE TABLE [dbo].[Temp_ProblemsReport_Shaft_Host](
	[TheID] [int] NULL,
	[Host] [varchar](30) NULL,
	[Shaft] [varchar](60) NULL,
	[Department] [varchar](150) NULL,
	[Problem] [varchar](150) NULL,
	[NumDepProb] [numeric](7, 0) NULL,
	[NumProb] [numeric](7, 0) NULL,
	[TotalProb] [numeric](7, 0) NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[Temp_ProblemsReport_Shaft](
	[TheID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[Host] [varchar](30) NULL,
	[Shaft] [varchar](60) NULL,
	[Department] [varchar](60) NULL,
	[NumDepProb] [numeric](7, 0) NULL,
	[TotalProb] [numeric](7, 0) NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[Temp_ProblemsReport_Host](
	[Host] [varchar](30) NULL,
	[Type] [varchar](50) NULL,
	[Shaft] [varchar](50) NULL,
	[OrderBy] [int] NULL,
	[Week4] [numeric](7, 0) NULL,
	[Week3] [numeric](7, 0) NULL,
	[Week2] [numeric](7, 0) NULL,
	[Week1] [numeric](7, 0) NULL,
	[Month2] [numeric](7, 0) NULL,
	[Month1] [numeric](7, 0) NULL,
	[Month] [numeric](7, 0) NULL
) ON [PRIMARY]

GO




CREATE TABLE [dbo].[Temp_ProblemsReport](
	[Host] [varchar](30) NULL,
	[FinYear] [numeric](7, 0) NULL,
	[Quater] [varchar](20) NULL,
	[Prodmonth] [numeric](7, 0) NULL,
	[Shaft] [varchar](100) NULL,
	[SECTIONID] [varchar](60) NULL,
	[Calendardate] [datetime] NULL,
	[Workingday] [varchar](1) NULL,
	[WorkplaceID] [varchar](60) NULL,
	[Activity] [varchar](60) NULL,
	[NonLostBlast] [numeric](7, 0) NULL,
	[PotentialBlast] [numeric](7, 0) NULL,
	[PlanBlast] [numeric](7, 0) NULL,
	[LostBlast] [numeric](7, 0) NULL,
	[QualityBlast] [numeric](7, 0) NULL,
	[NoQualityBlast] [numeric](7, 0) NULL,
	[BookBlast] [numeric](7, 0) NULL,
	[Plan_SQM] [numeric](7, 0) NULL,
	[Book_SQM] [numeric](7, 0) NULL,
	[Plan_Metres] [numeric](7, 0) NULL,
	[Book_Metres] [numeric](7, 0) NULL,
	[Department] [varchar](100) NULL,
	[Problem] [varchar](100) NULL
) ON [PRIMARY]

GO

ALTER View [dbo].[vw_Problem_Complete] as
select p.problemid, p.activity, p.color ProblemColor, p.colortext ProblemColorText, p.description Problem, p.displayorder, p.drillrig, 
n.Description Note, n.noteid, n.Color , n.Explanation, p.CausedLostBlast,
t.ProblemTypeID, t.Description Type, t.Color TypeColor from  code_problem p 
inner join problem_note pn on p.problemid = pn.problemid and p.activity = pn.activity
inner join code_problem_note n on n.noteid = pn.noteid and n.activity = pn.activity
inner join problem_type pt on pt.ProblemID = p.problemid and pt.activity = p.activity
inner join code_problem_type t on t.ProblemTypeID = pt.ProblemTypeID and t.activity = pt.activity

GO
create Procedure [dbo].[sp_ProblemsReport_Detail] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20)
as 

--set @ToDate = '2017/01/17 12:00:00 AM'--'2016-12-09'
--set @Level = 'Shaft Managers'
--set @Activity = 'Stoping'

declare @WeekStart DateTime,
@Week1End DateTime,
@Week1Start DateTime,
@Week2End DateTime,
@Week2Start DateTime,
@Week3End DateTime,
@Week3Start DateTime,
@Month Numeric(7),
@Month1 Numeric(7),
@Month2 Numeric(7),
@RunfromMonth varchar(6),
@SQL varchar(8000),
@TheDate varchar(6),
@RunName VARCHAR(100)

IF @Level = 3
  SET @RunName = 'Name_3'
  
IF @Level = 4
  SET @RunName = 'Name_2'

IF (DATEPART(MM, @ToDate) < 10)
	set @TheDate = convert(varchar(4),DATEPART(yyyy, @ToDate))+'0'+convert(varchar(2),DATEPART(MM, @ToDate))
ELSE
	set @TheDate = convert(varchar(4),DATEPART(yyyy, @ToDate))+convert(varchar(4),DATEPART(MM, @ToDate))

set @WeekStart = @ToDate-6
set @Week1End = @WeekStart-1
set @Week1Start = @Week1End-6
set @Week2End = @Week1Start-1
set @Week2Start = @Week2End-6
set @Week3End = @Week2Start-1
set @Week3Start = @Week3End-6

select 
	@Month2 =
		case 
		when (convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),5,2)) = 1) then 
		convert(Numeric(7),Convert(Varchar(4),convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),1,4))-1)+'11')

		when (convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),5,2)) = 2) then 
		convert(Numeric(7),Convert(Varchar(4),convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),1,4))-1)+'12')

		else
		CURRENTPRODUCTIONMONTH - 2
		end,
	@Month1 =
		case 
		when (convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),5,2)) = 1) then 
		convert(Numeric(7),Convert(Varchar(4),convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),1,4))-1)+'12')
		else
		CURRENTPRODUCTIONMONTH - 1
		end,
	@Month = CURRENTPRODUCTIONMONTH  from sysset

select @RunfromMonth = case when  DatePart(YYYY,@ToDate) > convert(int,substring(convert(Varchar(7),@Month2),1,4)) 
						then @Month2 else convert(Numeric(7),CONVERT(varchar(7),DatePart(YYYY,@ToDate))+'01') end

delete from Temp_ProblemsReport where Host = (select HOST_NAME())

delete from Temp_ProblemsReport_Host where Host = (select HOST_NAME())


set @SQL = '
insert into Temp_ProblemsReport '
IF (@Activity = 'Stoping')
BEGIN
	set @SQL = @SQL + '
	select 
		(Select HOST_NAME()) Host,
		FinYear = '''+@TheDate+''' ,
		Quater =  
			case 
			  when (convert(int,substring(CONVERT(varchar(7),pm.PRODMONTH),5,2)) between 1 and 3) then ''Quater1''
			  when (convert(int,substring(CONVERT(varchar(7),pm.PRODMONTH),5,2)) between 4 and 6) then ''Quater2''
			  when (convert(int,substring(CONVERT(varchar(7),pm.PRODMONTH),5,2)) between 7 and 9) then ''Quater3''
			  when (convert(int,substring(CONVERT(varchar(7),pm.PRODMONTH),5,2)) between 10 and 12) then ''Quater4''
			end,
		pm.Prodmonth, 
		sc.'+@RunName +' Shaft, 
		pm.SECTIONID,
		Caltype.Calendardate, 
		Caltype.Workingday , 
		pm.WorkplaceID, 
		Activity = ''Stoping'',
		NonLostBlast = 0,
		PotentialBlast =  case when Caltype.workingday = ''Y'' and isnull(pc.CausedLostBlast,''N'') = ''N'' 
							then 1 else 0 end,
		PlanBlast = case when (pm.Activity in (0,3)) and (p.SQM > 0) then 1 else 0 end,
		LostBlast = case when Caltype.workingday = ''Y'' And isnull(p.BookSQM,0) = 0 and
							p.CausedLostBlast = ''Y''  then 1 else 0 end,

		QualityBlast = 
		case 
		  when caltype.workingday = ''Y'' and isnull(pc.CausedLostBlast,''N'') = ''N'' and
		   p.BookSQM >= round(pm.FL*0.8,0) then 1
		  else 0
		end,

		NoQualityBlast = 
		case 
		  when Caltype.workingday = ''Y'' And isnull(pc.CausedLostBlast,''N'') = ''N'' and 
		  p.BookSQM < round(pm.FL*0.8,0)
		  and (p.BookSQM > 0) then 1
		  else 0
		end,

		BookBlast = case when p.BookSQM > 0 then 1 else 0 end,
		p.SQM Plan_SQM,
		p.BookSQM Book_SQM,
		Plan_Metres = p.MetresAdvance,
		Book_Metres = 0 ,
		pc.Problem Department, 
		pc.Note Problem
	from PLANMONTH pm 
	inner join SECTION_COMPLETE sc on
	  pm.PRODMONTH = sc.PRODMONTH and
	  pm.SECTIONID = sc.SECTIONID 
	inner join SECCAL s on
	  sc.PRODMONTH = s.PRODMONTH and
	  sc.SECTIONID_1 = s.SECTIONID 
	inner join vw_CalTypes Caltype on
	  s.CalendarCode = Caltype.CalendarCode and
	  s.BeginDate <= Caltype.Calendardate and
	  s.ENDDATE >= Caltype.Calendardate
	left join PLanning p on
	  pm.PRODMONTH = p.PRODMONTH and
	  pm.SECTIONID = p.SECTIONID and
	  pm.Workplaceid = p.Workplaceid and
	  pm.Activity = p.Activity and
	  pm.IsCubics = p.IsCubics and
	  pm.PlanCode = p.PlanCode and
	  Caltype.Calendardate = p.Calendardate 
	left join vw_Problem_Complete pc on
	  p.ProblemID = pc.noteid and
	  p.Activity = pc.Activity
	where 
		pm.Prodmonth >= '''+@runFromMonth+''' and
		pm.IsCubics = ''N'' and
		pm.PlanCode = ''MP'' and 
		pm.Activity in (0, 3) '
END
ELSE
BEGIN
	set @SQL = @SQL + '
	select 
	(Select HOST_NAME()) Host,
	DatePart(YYYY,@ToDate) FinYear,
	Quater =  
		case 
		  when (convert(int,substring(CONVERT(varchar(7),pm.PRODMONTH),5,2)) between 1 and 3) then ''Quater1''
		  when (convert(int,substring(CONVERT(varchar(7),pm.PRODMONTH),5,2)) between 4 and 6) then ''Quater2''
		  when (convert(int,substring(CONVERT(varchar(7),pm.PRODMONTH),5,2)) between 7 and 9) then ''Quater3''
		  when (convert(int,substring(CONVERT(varchar(7),pm.PRODMONTH),5,2)) between 10 and 12) then ''Quater4''
		end,
	pm.Prodmonth, 
	sc.'+@RunName +' Shaft, 
	pm.SECTIONID,
	ct.Calendardate, 
	ct.Workingday , 
	pm.WorkplaceID, 
	Activity = ''Development''
	NonLostBlast = case when (pc.CausedLostBlast = ''Y'') then 1 else 0 end,
	PotentialBlast = case when (ct.workingday = ''Y'') then 1 else 0 end,
	PlanBlast = cas when p.MetresAdvance > 0 then 1 else 0 end,
	LostBlast = 
	case 
	  when (ct.workingday = ''Y'') And (p.BookMetresadvance = 0) or (p.BookMetresadvance is null)  then 1 else 0 
	end,
	QualityBlast = 
	case 
	  when ((pc.CausedLostBlast = ''N'') or (pc.CausedLostBlast is null)) and (p.BookMetresadvance > 0) then 1
	  else 0
	end,
	NoQualityBlast = 0,
	BookBlast = case when p.BookMetresadvance > 0 then 1 else 0 end,
	p.MetresAdvance Plan_SQM,
	p.BookMetresadvance Book_SQM,
	0,0,
	pc.Problem Department, 
	pc.Note Problem
from  PLANMONTH pm 
inner join SECTION_COMPLETE sc on
  pm.PRODMONTH = sc.PRODMONTH and
  pm.SECTIONID = sc.SECTIONID 
inner join SECCAL ss on
  sc.PRODMONTH = ss.PRODMONTH and
  sc.SECTIONID_1 = ss.SECTIONID 
inner join vw_CalTypes ct on
  ss.CalendarCode = ct.CalendarCode and
  ss.BeginDate <= ct.Calendardate and
  ss.ENDDATE >= ct.Calendardate
left join PLanning p on
  pm.PRODMONTH = p.PRODMONTH and
  pm.SECTIONID = p.SECTIONID and
  pm.Workplaceid = p.Workplaceid and
  pm.Activity = p.Activity and
  pm.IsCubics = p.IsCubics and
  pm.PlanCode = p.PlanCode and
  ct.Calendardate = p.Calendardate 
left join vw_Problem_Complete pc on
  p.ProblemID = pc.noteid and
  p.Activity = pc.Activity
where 
	pm.PRODMONTH = '''+@RunFromMonth+'''
	and pm.IsCubics = ''N''
	and pm.ACTIVITY = 1
	and pm.PlanCode = ''MP'' '
END		

--print (@SQL)
exec (@SQL)

Insert into Temp_ProblemsReport_Host
select 
	(Select Host_Name()),
	[Type],
	Shaft, 1 OrderBy,
	isnull(sum(Week4),0) Week4,
	isnull(sum(Week3),0) Week3,
	isnull(sum(Week2),0) Week2,
	isnull(sum(Week1),0) Week1,
	isnull(sum(Month2),0) Month2,
	isnull(sum(Month1),0) Month1,
	isnull(sum([Month]),0) [Month]
from
(
	select
		'Actual Blasts' [Type], Shaft,
		Week4 = case when (CalendarDate between  @Week3Start and @Week3End) then QualityBlast+NoQualityBlast end,
		Week3 = case when (CalendarDate between  @Week2Start and @Week2End) then QualityBlast+NoQualityBlast end,
		Week2 = case when (CalendarDate between  @Week1Start and @Week1End) then QualityBlast+NoQualityBlast end,
		Week1 = case when (CalendarDate between  @WeekStart and @ToDate) then QualityBlast+NoQualityBlast end,
		Month2 = case when (Prodmonth = @Month2) then QualityBlast+NoQualityBlast end,
		Month1 = case when (Prodmonth = @Month1) then QualityBlast+NoQualityBlast end,
		[Month] = case when (Prodmonth = @Month) then QualityBlast+NoQualityBlast end
	from Temp_ProblemsReport 
	where HOST = (Select HOST_Name())
) a
group by [Type], Shaft

union

select 
	(Select Host_Name()),
	[Type],
	Shaft, 3 OrderBy,
	isnull(sum(Week4),0) Week4,
	isnull(sum(Week3),0) Week3,
	isnull(sum(Week2),0) Week2,
	isnull(sum(Week1),0) Week1,
	isnull(sum(Month2),0) Month2,
	isnull(sum(Month1),0) Month1,
	isnull(sum([Month]),0) [Month]
from
(
	select
		'Potential Blasts' [Type], Shaft,
		Week4 = case when (CalendarDate between  @Week3Start and @Week3End) then (PotentialBlast-NonLostBlast) end,
		Week3 = case when (CalendarDate between  @Week2Start and @Week2End) then (PotentialBlast-NonLostBlast) end,
		Week2 = case when (CalendarDate between  @Week1Start and @Week1End) then (PotentialBlast-NonLostBlast) end,
		Week1 = case when (CalendarDate between  @WeekStart and @ToDate) then (PotentialBlast-NonLostBlast) end,
		Month2 = case when (Prodmonth = @Month2) then (PotentialBlast-NonLostBlast) end,
		Month1 = case when (Prodmonth = @Month1) then (PotentialBlast-NonLostBlast) end,
		[Month] = case when (Prodmonth = @Month) then (PotentialBlast-NonLostBlast) end
	from Temp_ProblemsReport 
	where HOST = (Select HOST_Name())
) a
group by [Type], Shaft

union

select 
	(Select Host_Name()),
	[Type],
	Shaft, 4 OrderBy,
	isnull(sum(Week4),0) Week4,
	isnull(sum(Week3),0) Week3,
	isnull(sum(Week2),0) Week2,
	isnull(sum(Week1),0) Week1,
	isnull(sum(Month2),0) Month2,
	isnull(sum(Month1),0) Month1,
	isnull(sum([Month]),0) [Month]
from
(
	select
		'Lost Blasts' [Type], Shaft,
		Week4 = case when (CalendarDate between  @Week3Start and @Week3End) then PotentialBlast-NonLostBlast-(QualityBlast+NoQualityBlast) end,
		Week3 = case when (CalendarDate between  @Week2Start and @Week2End) then PotentialBlast-NonLostBlast-(QualityBlast+NoQualityBlast) end,
		Week2 = case when (CalendarDate between  @Week1Start and @Week1End) then PotentialBlast-NonLostBlast-(QualityBlast+NoQualityBlast) end,
		Week1 = case when (CalendarDate between  @WeekStart and @ToDate) then PotentialBlast-NonLostBlast-(QualityBlast+NoQualityBlast) end,
		Month2 = case when (Prodmonth = @Month2) then PotentialBlast-NonLostBlast-(QualityBlast+NoQualityBlast) end,
		Month1 = case when (Prodmonth = @Month1) then PotentialBlast-NonLostBlast-(QualityBlast+NoQualityBlast) end,
		[Month] = case when (Prodmonth = @Month) then PotentialBlast-NonLostBlast-(QualityBlast+NoQualityBlast) end
	from Temp_ProblemsReport 
	where HOST = (Select HOST_Name())
) a
group by [Type], Shaft

union

select
	(Select Host_Name()), 
		[Type],
		Shaft, 6 OrderBy,
		isnull(sum(Week4),0) Week4,
		isnull(sum(Week3),0) Week3,
		isnull(sum(Week2),0) Week2,
		isnull(sum(Week1),0) Week1,
		isnull(sum(Month2),0) Month2,
		isnull(sum(Month1),0) Month1,
		isnull(sum([Month]),0) [Month]
from
(
	select
		'Book/Measured SQM' [Type], Shaft,
		Week4 = case when (CalendarDate between  @Week3Start and @Week3End) then Book_SQM end,
		Week3 = case when (CalendarDate between  @Week2Start and @Week2End) then Book_SQM end,
		Week2 = case when (CalendarDate between  @Week1Start and @Week1End) then Book_SQM end,
		Week1 = case when (CalendarDate between  @WeekStart and @ToDate) then Book_SQM end,
		Month2 = case when (Prodmonth = @Month2) then Book_SQM end,
		Month1 = case when (Prodmonth = @Month1) then Book_SQM end,
		[Month] = case when (Prodmonth = @Month) then Book_SQM end
	from Temp_ProblemsReport 
	where HOST = (Select HOST_Name())
) a
group by [Type], Shaft

union

select 
	(Select Host_Name()),
	[Type],
	Shaft, 7 OrderBy,
	isnull(sum(Week4),0) Week4,
	isnull(sum(Week3),0) Week3,
	isnull(sum(Week2),0) Week2,
	isnull(sum(Week1),0) Week1,
	isnull(sum(Month2),0) Month2,
	isnull(sum(Month1),0) Month1,
	isnull(sum([Month]),0) [Month]
from
(
	select
		'Plan SQM' [Type], Shaft,
		Week4 = case when (CalendarDate between  @Week3Start and @Week3End) then Plan_SQM end,
		Week3 = case when (CalendarDate between  @Week2Start and @Week2End) then Plan_SQM end,
		Week2 = case when (CalendarDate between  @Week1Start and @Week1End) then Plan_SQM end,
		Week1 = case when (CalendarDate between  @WeekStart and @ToDate) then Plan_SQM end,
		Month2 = case when (Prodmonth = @Month2) then Plan_SQM end,
		Month1 = case when (Prodmonth = @Month1) then Plan_SQM end,
		[Month] = case when (Prodmonth = @Month) then Plan_SQM end
	from Temp_ProblemsReport 
	where HOST = (Select HOST_Name())
) a
group by [Type], Shaft 

union

select 
	(Select Host_Name()),
	[Type],
	Shaft, 20 OrderBy,
	isnull(sum(Week4),0) Week4,
	isnull(sum(Week3),0) Week3,
	isnull(sum(Week2),0) Week2,
	isnull(sum(Week1),0) Week1,
	isnull(sum(Month2),0) Month2,
	isnull(sum(Month1),0) Month1,
	isnull(sum([Month]),0) [Month]
from
(
	select
		'Quality Blasts' [Type], Shaft,
		Week4 = case when (CalendarDate between  @Week3Start and @Week3End) then QualityBlast end,
		Week3 = case when (CalendarDate between  @Week2Start and @Week2End) then QualityBlast end,
		Week2 = case when (CalendarDate between  @Week1Start and @Week1End) then QualityBlast end,
		Week1 = case when (CalendarDate between  @WeekStart and @ToDate) then QualityBlast end,
		Month2 = case when (Prodmonth = @Month2) then QualityBlast end,
		Month1 = case when (Prodmonth = @Month1) then QualityBlast end,
		[Month] = case when (Prodmonth = @Month) then QualityBlast end
	from Temp_ProblemsReport 
	where HOST = (Select HOST_Name())
) a
group by [Type], Shaft 

select
	[Type],Shaft, OrderBy,
	Week4 = Case when ([Type] = '              Percentage of blasts lost') or ([Type] = '              Quality blasts (%)') or ([Type] = '              Deviation %')
		then
		  Convert(Varchar(10),Week4)+' %'
		else 
		  Convert(Varchar(10),Week4)
		end,
	Week3 = Case when ([Type] = '              Percentage of blasts lost') or ([Type] = '              Quality blasts (%)') or ([Type] = '              Deviation %')
		then
		  Convert(Varchar(10),Week3)+' %'
		else 
		  Convert(Varchar(10),Week3)
	end,
	Week2 = Case when ([Type] = '              Percentage of blasts lost') or ([Type] = '              Quality blasts (%)') or ([Type] = '              Deviation %')
		then
		  Convert(Varchar(10),Week2)+' %'
		else 
		  Convert(Varchar(10),Week2)
		end,
	Week1 = Case when ([Type] = '              Percentage of blasts lost') or ([Type] = '              Quality blasts (%)') or ([Type] = '              Deviation %')
		then
		  Convert(Varchar(10),Week1)+' %'
		else 
		  Convert(Varchar(10),Week1)
		end,
	Month2 = Case when ([Type] = '              Percentage of blasts lost') or ([Type] = '              Quality blasts (%)') or ([Type] = '              Deviation %')
		then
			Convert(Varchar(10),Month2)+' %'
		else 
			Convert(Varchar(10),Month2)
		end,
	Month1 = Case when ([Type] = '              Percentage of blasts lost') or ([Type] = '              Quality blasts (%)') or ([Type] = '              Deviation %')
		then
		  Convert(Varchar(10),Month1)+' %'
		else 
		  Convert(Varchar(10),Month1)
		end,
	[Month] = Case when ([Type] = '              Percentage of blasts lost') or ([Type] = '              Quality blasts (%)') or ([Type] = '              Deviation %')
		then
		  Convert(Varchar(10),[Month])+' %'
		else 
		  Convert(Varchar(10),[Month])
	end
from 
(
	select * from
	(
		select * from Temp_ProblemsReport_Host Where HOST = (Select HOST_Name())

		union

		SELECT 
			(Select Host_Name()),
			'              Percentage of blasts lost',
			a.Shaft,
			5,
			case when a.week4 = 0 then 0 else Convert(int,Round(b.Week4/a.week4*100,0)) end,
			case when a.Week3 = 0 then 0 else Convert(int,Round(b.Week3/a.Week3*100,0)) end,
			case when a.Week2 = 0 then 0 else Convert(int,Round(b.Week2/a.Week2*100,0)) end,
			case when a.week1 = 0 then 0 else Convert(int,Round(b.Week1/a.Week1*100,0)) end,
			case when a.Month2 = 0 then 0 else Convert(int,Round(b.Month2/a.Month2*100,0)) end,
			case when a.Month1 = 0 then 0 else Convert(int,Round(b.Month1/a.Month1*100,0)) end,
			case when a.[Month] = 0 then 0 else Convert(int,Round(b.[Month]/a.[Month]*100,0)) end
		FROM 
		(
			select * from Temp_ProblemsReport_Host
			where [Type] = 'Potential Blasts' and HOST = (Select HOST_Name())
		) a 
		inner join
		(
			select * from Temp_ProblemsReport_Host 
			where [Type] = 'Lost Blasts' and HOST = (Select HOST_Name())
		) b on a.Shaft = b.Shaft

		union

		SELECT 
			(Select Host_Name()),
			'              Quality blasts (%)',
			a.Shaft,
			2,
			case when a.week4 = 0 then 0 else Convert(int,Round(b.Week4/a.week4*100,0)) end,
			case when a.Week3 = 0 then 0 else Convert(int,Round(b.Week3/a.Week3*100,0)) end,
			case when a.Week2 = 0 then 0 else Convert(int,Round(b.Week2/a.Week2*100,0)) end,
			case when a.week1 = 0 then 0 else Convert(int,Round(b.Week1/a.Week1*100,0)) end,
			case when a.Month2 = 0 then 0 else Convert(int,Round(b.Month2/a.Month2*100,0)) end,
			case when a.Month1 = 0 then 0 else Convert(int,Round(b.Month1/a.Month1*100,0)) end,
			case when a.[Month] = 0 then 0 else Convert(int,Round(b.[Month]/a.[Month]*100,0)) end
		FROM 
		(
			select * from Temp_ProblemsReport_Host 
			where [Type] = 'Actual Blasts' and HOST = (Select HOST_Name())
		) a 
		inner join
		(
			select * from Temp_ProblemsReport_Host 
			where [Type] = 'Quality Blasts' and HOST = (Select HOST_Name())
		) b on a.Shaft = b.Shaft

		union

		SELECT 
			(Select Host_Name()),
			'              Deviation %',
			a.Shaft,
			8,
			case when a.week4 = 0 then 0 else Convert(int,Round((a.week4-b.week4)/a.week4*100,0)) end,
			case when a.Week3 = 0 then 0 else Convert(int,Round((a.week3-b.week3)/a.Week3*100,0)) end,
			case when a.Week2 = 0 then 0 else Convert(int,Round((a.week2-b.week2)/a.Week2*100,0)) end,
			case when a.week1 = 0 then 0 else Convert(int,Round((a.week1-b.week1)/a.Week1*100,0)) end,
			case when a.Month2 = 0 then 0 else Convert(int,Round((a.Month2-b.Month2)/a.Month2*100,0)) end,
			case when a.Month1 = 0 then 0 else Convert(int,Round((a.Month1-b.Month1)/a.Month1*100,0)) end,
			case when a.[Month] = 0 then 0 else Convert(int,Round((a.[Month]-b.[Month])/a.[Month]*100,0)) end
		FROM 
		(
			select * from Temp_ProblemsReport_Host 
			where [Type] = 'Book/Measured SQM' and HOST = (Select HOST_Name())
		) a inner join
		(
			select * from Temp_ProblemsReport_Host 
			where [Type] = 'Plan SQM' and HOST = (Select HOST_Name())
		) b on a.Shaft = b.Shaft

		union

		SELECT
			(Select Host_Name()),
			'              Deviation m²',
			a.Shaft,
			9,
			a.week4-b.week4,
			a.week3-b.week3,
			a.week2-b.week2,
			a.week1-b.week1,
			a.Month2-b.Month2,
			a.Month1-b.Month1,
			a.[Month]-b.[Month]
		FROM 
		(
			select * from Temp_ProblemsReport_Host 
			where [Type] = 'Book/Measured SQM' and HOST = (Select HOST_Name())
		) a 
		inner join
		(	
			select * from Temp_ProblemsReport_Host 
			where [Type] = 'Plan SQM' and HOST = (Select HOST_Name())
		) b on a.Shaft = b.Shaft
	) a
	where [Type] <> 'Quality Blasts'
) a
order by Shaft, OrderBy
go

create Procedure [dbo].[sp_ProblemsReport_FinYear] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20)   
as

--set @ToDate = '2016-12-09'
--set @Level = 'Shaft'

Declare @WeekStart DateTime,
@TheYear varchar(4),
@Shaft varchar(60),
@Department varchar(150),
@NumDepProb varchar(7),
@TotalProb varchar(7),
@TheID varchar(7),
@SQL varchar(8000),
@RunName VARCHAR(100)

IF @Level = 3
  SET @RunName = 'Name_3'
  
IF @Level = 4
  SET @RunName = 'Name_2'

IF (@Activity = 'Stoping')
BEGIN
	set @Activity = '(0,3)'
END
ELSE
BEGIN
	set @Activity = '(1)'
END


set @WeekStart = convert(varchar(10), @ToDate-6, 120)
set @TheYear = DATEPART(YYYY,@ToDate)

delete from Temp_ProblemsReport_Shaft where Host = (select HOST_NAME())

delete from Temp_ProblemsReport_Shaft_Host where Host = (select HOST_NAME());

--get to 3 depertment per shaft
set @SQL = ''
set @SQL = '
	DECLARE Shaft_Cursor CURSOR FOR
	select distinct b.'+@RunName +' Name_3 
	from Planmonth a inner join section_complete b on
		a.Prodmonth = b.prodmonth and
		a.Sectionid = b.sectionid, sysset
	where a.prodmonth = sysset.currentProductionMonth  
	and a.Activity in '+@Activity +' '

--print (@SQL)
exec (@SQL)

OPEN Shaft_Cursor;
FETCH NEXT FROM Shaft_Cursor
into @Shaft;

Select @TheID = 0 
 
WHILE @@FETCH_STATUS = 0
BEGIN
	set @SQL = '
	insert into Temp_ProblemsReport_Shaft (Host, Shaft, Department, NumDepProb, TotalProb)
	select a.*, b.TotProblems from
	(
		select top 3 (select HOST_NAME()) Host,
			sc.'+@RunName +' Shaft, 
			isnull(pc.Note,'''') Department, 
			isnull(Count(pc.Note),0) NumProblems
		from PLANNING b 
		inner join SECTION_COMPLETE sc on
			b.PRODMONTH = sc.PRODMONTH and
			b.SECTIONID = sc.SECTIONID 
		inner join SECCAL s on
			sc.PRODMONTH = s.PRODMONTH and
			sc.SECTIONID_1 = s.SECTIONID 
		inner join vw_CalTypes ct on
			s.CalendarCode = ct.CalendarCode and
			s.BeginDate <= ct.Calendardate and
			s.ENDDATE >= ct.Calendardate and
			b.CALENDARDATE = ct.CALENDARDATE
		left join vw_Problem_Complete pc on
			b.ProblemID = pc.ProblemID and
			b.ACTIVITY = pc.Activity
		inner join [Code_FinancialYear] fy on
			fy.StartProdMonth <= b.PRODMONTH and
			fy.EndProdMonth >= b.PRODMONTH
		where 
			fy.FinYear = '''+@TheYear+''' and
			isnull(pc.CausedLostBlast, ''N'') = ''N'' and
			b.PlanCode = ''MP'' and 
			b.Activity in '+@Activity +' and 
			sc.'+@RunName +' = '''+@Shaft+''' 
		group by sc.'+@RunName +', pc.Note 
		order by NumProblems desc
	) a inner join 

	(
		select 
			sc.'+@RunName +' Shaft, 
			isnull(Count(pc.Note),0) TotProblems
		from PLANNING b 
		inner join SECTION_COMPLETE sc on
			b.PRODMONTH = sc.PRODMONTH and
			b.SECTIONID = sc.SECTIONID 
		inner join SECCAL s on
			sc.PRODMONTH = s.PRODMONTH and
			sc.SECTIONID_1 = s.SECTIONID 
		inner join vw_CalTypes ct on
			s.CalendarCode = ct.CalendarCode and
			s.BeginDate <= ct.Calendardate and
			s.ENDDATE >= ct.Calendardate and
			b.Calendardate = ct.CALENDARDATE
		left join vw_Problem_Complete pc on
			b.ProblemID = pc.ProblemID and
			b.ACTIVITY = pc.Activity
		inner join Code_FinancialYear fy on
			fy.StartProdMonth <= b.PRODMONTH and
			fy.EndProdMonth >= b.PRODMONTH
		where 
			fy.FinYear = '''+@TheYear+''' and
			isnull(pc.CausedLostBlast, ''N'') = ''N'' and
			b.PlanCode = ''MP'' and 
			b.Activity in '+@Activity +' and 
			sc.'+@RunName +' = '''+@Shaft+'''
		group by sc.'+@RunName +' 
	) b on
	a.shaft = b.shaft
	order by NumProblems desc '
	--print (@SQL)
	exec (@SQL)  
   FETCH NEXT FROM Shaft_Cursor
     Into @Shaft;
END; 

CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;

set @SQL = ''

-- get top 3 reasons per depart ment per shaft
DECLARE Department_Cursor CURSOR FOR
select TheID, isnull(Shaft,'') Shaft, isnull(Department,'') Department, 
	isnull(NumDepProb,0) NumDepProb, isnull(TotalProb,0) TotalProb  
from Temp_ProblemsReport_Shaft where Host = (Select Host_Name()) 

OPEN Department_Cursor;
FETCH NEXT FROM Department_Cursor
into @TheID, @Shaft, @Department, @NumDepProb, @TotalProb;
  
WHILE @@FETCH_STATUS = 0
BEGIN
	set @SQL = '
	insert into Temp_ProblemsReport_Shaft_Host (TheID, Host, Shaft, Department, Problem, NumDepProb, NumProb, TotalProb )
	select top 3 '''+@TheID+''', (Select Host_Name()), 
		sc.'+@RunName +' Shaft,
		'''+@Department+''', isnull(pc.Problem,'''') Problem, isnull('+@NumDepProb+',0),
		isnull(Count(pc.Problem),0) NumProblems, isnull('+@TotalProb+',0)
	from PLANNING b 
	inner join SECTION_COMPLETE sc on
		b.PRODMONTH = sc.PRODMONTH and
		b.SECTIONID = sc.SECTIONID 
	inner join SECCAL s on
		sc.PRODMONTH = s.PRODMONTH and
		sc.SECTIONID_1 = s.SECTIONID 
	inner join vw_CalTypes ct on
		s.CalendarCode = ct.CalendarCode and
		s.BeginDate <= ct.Calendardate and
		s.ENDDATE >= ct.Calendardate and
		b.Calendardate = ct.CALENDARDATE
	left join vw_Problem_Complete pc on
		b.ProblemID = pc.ProblemID and
		b.Activity = pc.Activity
	inner join [Code_FinancialYear] fy on
		fy.StartProdMonth <= b.PRODMONTH and
		fy.EndProdMonth >= b.PRODMONTH
	where 
		fy.FinYear = '''+@TheYear+''' and
		isnull(pc.CausedLostBlast, ''N'') = ''N'' and
		pc.Note = '''+@Department+''' and 
		b.PlanCode = ''MP'' and 
		b.Activity in '+@Activity +' and 
		sc.'+@RunName +' = '''+@Shaft+''' 
	group by sc.'+@RunName +', pc.Problem 
	order by NumProblems desc '
    --print (@SQL)
	exec (@SQL) 
   FETCH NEXT FROM Department_Cursor
     into @TheID, @Shaft, @Department, @NumDepProb, @TotalProb;
END;

CLOSE Department_Cursor;
DEALLOCATE Department_Cursor;

select *, 
	DepPer = Case when TotalProb = 0 then 0 else 
	  convert (Real,round(NumDepProb/TotalProb,2))
	end,
	ProbPer = Case when NumDepProb = 0 then 0 else 
	  convert (Real,round(NumProb/TotalProb,2))
	end
from Temp_ProblemsReport_Shaft_Host 
where Host = (Select Host_Name()) 
order by Shaft, DepPer desc, ProbPer desc
go
 create Procedure [dbo].[sp_ProblemsReport_FinYearPie] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20) 
as

--set @ToDate = '2014-06-04'

Declare 
@WeekStart DateTime,
@TheYear varchar(4),
@SQL varchar(8000),
@RunName VARCHAR(100)

IF @Level = 3
  SET @RunName = 'Name_3'
  
IF @Level = 4
  SET @RunName = 'Name_2'

IF (@Activity = 'Stoping')
BEGIN
	set @Activity = '(0,3)'
END
ELSE
BEGIN
	set @Activity = '(1)'
END


set @WeekStart = @ToDate-6
set @TheYear = DATEPART(YYYY,@ToDate)

--create temp table to populate
set @SQL = ' select 
	sc.'+@RunName +' Shaft, 
	isnull(pc.Note,'''') Department, 
	isnull(ProblemColorText,'''') ColorText , isnull(COUNT(pc.Note),0) NumberOFProb
from PLANNING b 
inner join SECTION_COMPLETE sc on
	b.PRODMONTH = sc.PRODMONTH and
	b.SECTIONID = sc.SECTIONID 
inner join SECCAL s on
	sc.PRODMONTH = s.PRODMONTH and
	sc.SECTIONID_1 = s.SECTIONID 
inner join vw_CalTypes ct on
	s.CalendarCode = ct.CalendarCode and
	s.BeginDate <= ct.Calendardate and
	s.ENDDATE >= ct.Calendardate and
	b.Calendardate = ct.Calendardate
left join vw_Problem_Complete pc on
	b.ProblemID = pc.ProblemID and
	b.ACTIVITY = pc.Activity 
inner join Code_FinancialYear fy on
	fy.StartProdMonth <= b.PRODMONTH and
	fy.EndProdMonth >= b.PRODMONTH 
where fy.FinYear = '''+@TheYear+''' and
	isnull(pc.CausedLostBlast, ''N'') = ''N'' and
	b.PlanCode = ''MP'' and 
	b.Activity in '+@Activity +'
group by sc.'+@RunName +', pc.Note,pc.ProblemColorText 
order by sc.'+@RunName +', NumberOFProb desc '

--print (@SQL)
exec (@SQL)

go
create Procedure [dbo].[sp_ProblemsReport_Week] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20)  
as

--set @ToDate = '2016-12-09'
--set @Level = 'Shaft'

Declare @WeekStart varchar(10), 
@TheDate varchar(10),
@Shaft varchar(60),
@Department varchar(150),
@NumDepProb varchar(7),
@TotalProb varchar(7),
@TheID varchar(7),
@SQL varchar(8000),
@RunName VARCHAR(100)

IF @Level = 3
  SET @RunName = 'Name_3'
  
IF @Level = 4
  SET @RunName = 'Name_2'

IF (@Activity = 'Stoping')
BEGIN
	set @Activity = '(0,3)'
END
ELSE
BEGIN
	set @Activity = '(1)'
END

set @WeekStart = convert(varchar(10), @ToDate-6, 120)
set @TheDate = convert(varchar(10), @ToDate, 120)

delete from Temp_ProblemsReport_Shaft where Host = (select HOST_NAME())

delete from Temp_ProblemsReport_Shaft_Host where Host = (select HOST_NAME());
--print @WeekStart
--print @ToDate

--get to 3 depertment per shaft
set @SQL = ''

set @SQL = '
	DECLARE Shaft_Cursor CURSOR FOR
	select distinct b.'+@RunName +' Name_3 
	from Planmonth a inner join section_complete b on
		a.Prodmonth = b.prodmonth and
		a.Sectionid = b.sectionid, sysset
	where a.prodmonth = sysset.currentProductionMonth  
	and a.Activity in '+@Activity +' '

--print (@SQL)
exec (@SQL)

OPEN Shaft_Cursor;
FETCH NEXT FROM Shaft_Cursor
into @Shaft;

Select @TheID = 0 
 
WHILE @@FETCH_STATUS = 0
BEGIN
	set @SQL = '
	insert into Temp_ProblemsReport_Shaft (Host, Shaft, Department, NumDepProb, TotalProb)
	select a.*, b.TotProblems from
	(
		select top 3 (select HOST_NAME()) Host,
			sc.'+@RunName +' Shaft, 
			isnull(pc.Note,'''') Department, 
			isnull(Count(pc.Note),0) NumProblems
		from PLANNING b 
		inner join SECTION_COMPLETE sc on
			b.PRODMONTH = sc.PRODMONTH and
			b.SECTIONID = sc.SECTIONID 
		inner join SECCAL s on
			sc.PRODMONTH = s.PRODMONTH and
			sc.SECTIONID_1 = s.SECTIONID 
		inner join vw_CalTypes ct on
			s.CalendarCode = ct.CalendarCode and
			s.BeginDate <= ct.Calendardate and
			s.ENDDATE >= ct.Calendardate and
			b.CALENDARDATE = ct.CALENDARDATE
		left join vw_Problem_Complete pc on
			b.ProblemID = pc.ProblemID and
			b.ACTIVITY = pc.Activity
		inner join Code_FinancialYear fy on
			fy.StartProdMonth <= b.PRODMONTH and
			fy.EndProdMonth >= b.PRODMONTH
		where 
			b.Calendardate >= '''+@WeekStart+''' and
			b.Calendardate <= '''+@TheDate+''' and
			isnull(pc.CausedLostBlast, ''N'') = ''N'' and
			b.PlanCode = ''MP'' and 
			b.Activity in '+@Activity +' and 
			sc.'+@RunName +' = '''+@Shaft+''' 
		group by sc.'+@RunName +', pc.Note 
		order by NumProblems desc
	) a inner join 

	(
		select 
			sc.'+@RunName +' Shaft, 
			isnull(Count(pc.Note),0) TotProblems
		from PLANNING b 
		inner join SECTION_COMPLETE sc on
			b.PRODMONTH = sc.PRODMONTH and
			b.SECTIONID = sc.SECTIONID 
		inner join SECCAL s on
			sc.PRODMONTH = s.PRODMONTH and
			sc.SECTIONID_1 = s.SECTIONID 
		inner join vw_CalTypes ct on
			s.CalendarCode = ct.CalendarCode and
			s.BeginDate <= ct.Calendardate and
			s.ENDDATE >= ct.Calendardate and
			b.Calendardate = ct.CALENDARDATE
		left join vw_Problem_Complete pc on
			b.ProblemID = pc.ProblemID and
			b.ACTIVITY = pc.Activity
		inner join Code_FinancialYear fy on
			fy.StartProdMonth <= b.PRODMONTH and
			fy.EndProdMonth >= b.PRODMONTH
		where 
			b.Calendardate >= '''+@WeekStart+''' and
			b.Calendardate <= '''+@TheDate+''' and '	
		set @SQL = @SQL + '	
			isnull(pc.CausedLostBlast, ''N'') = ''N'' and
			b.PlanCode = ''MP'' and 
			b.Activity in '+@Activity +' and 
			sc.'+@RunName +' = '''+@Shaft+'''
		group by sc.'+@RunName +' 
	) b on
	a.shaft = b.shaft
	order by NumProblems desc '

	--print(@SQL) 
	exec (@SQL)
   FETCH NEXT FROM Shaft_Cursor
     Into @Shaft;
END; 

CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;

set @SQL = ''

-- get top 3 reasons per depart ment per shaft
DECLARE Department_Cursor CURSOR FOR
select TheID, isnull(Shaft,'') Shaft, isnull(Department,'') Department, 
	isnull(NumDepProb,0) NumDepProb, isnull(TotalProb,0) TotalProb  
from Temp_ProblemsReport_Shaft where Host = (Select Host_Name()) 

OPEN Department_Cursor;
FETCH NEXT FROM Department_Cursor
into @TheID, @Shaft, @Department, @NumDepProb, @TotalProb;
  
WHILE @@FETCH_STATUS = 0
BEGIN
	set @SQL = '
	insert into Temp_ProblemsReport_Shaft_Host (TheID, Host, Shaft, Department, Problem, NumDepProb, NumProb, TotalProb )
	select top 3 '''+@TheID+''', (select HOST_NAME()), 
		sc.'+@RunName +' Shaft, 
		'''+@Department+''', isnull(pc.Problem,'''') Problem, isnull('+@NumDepProb+',0),
		isnull(Count(pc.Problem),0) NumProblems, isnull('+@TotalProb+',0)
	from PLANNING b 
	inner join SECTION_COMPLETE sc on
		b.PRODMONTH = sc.PRODMONTH and
		b.SECTIONID = sc.SECTIONID 
	inner join SECCAL s on
		sc.PRODMONTH = s.PRODMONTH and
		sc.SECTIONID_1 = s.SECTIONID 
	inner join vw_CalTypes ct on
		s.CalendarCode = ct.CalendarCode and
		s.BeginDate <= ct.Calendardate and
		s.ENDDATE >= ct.Calendardate and
		b.Calendardate = ct.CALENDARDATE
	left join vw_Problem_Complete pc on
		b.ProblemID = pc.ProblemID and
		b.Activity = pc.Activity
	inner join Code_FinancialYear fy on
		fy.StartProdMonth <= b.PRODMONTH and
		fy.EndProdMonth >= b.PRODMONTH
	where 
		b.Calendardate >= '''+@WeekStart+''' and
		b.Calendardate <= '''+@TheDate+''' and  
		isnull(pc.CausedLostBlast, ''N'') = ''N'' and
		pc.Note = '''+@Department+''' and 
		b.PlanCode = ''MP'' and 
		b.Activity in '+@Activity +' and 
		sc.'+@RunName +' = '''+@Shaft+''' 
	group by sc.'+@RunName +', pc.Problem 
	order by NumProblems desc '
		--print (@SQL)
		exec (@SQL)
   FETCH NEXT FROM Department_Cursor
     into @TheID, @Shaft, @Department, @NumDepProb, @TotalProb;
END;

CLOSE Department_Cursor;
DEALLOCATE Department_Cursor;

select *, 
	DepPer = Case when TotalProb = 0 then 0 else 
	  convert (Real,round(NumDepProb/TotalProb,2))
	end,
	ProbPer = Case when NumDepProb = 0 then 0 else 
	  convert (Real,round(NumProb/TotalProb,2))
	end
from Temp_ProblemsReport_Shaft_Host 
where Host = (Select Host_Name()) 
order by Shaft, DepPer desc, ProbPer desc
go

 create Procedure [dbo].[sp_ProblemsReport_WeekPie] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20) 
as

--set @ToDate = '2014-06-04'

Declare 
@WeekStart varchar(10), 
@TheDate varchar(10),
@SQL varchar(8000),
@RunName VARCHAR(100)

IF @Level = 3
  SET @RunName = 'Name_3'
  
IF @Level = 4
  SET @RunName = 'Name_2'

IF (@Activity = 'Stoping')
BEGIN
	set @Activity = '(0,3)'
END
ELSE
BEGIN
	set @Activity = '(1)'
END

set @WeekStart = convert(varchar(10), @ToDate-6, 120)
set @TheDate = convert(varchar(10), @ToDate, 120)

--create temp table to populate
set @SQL = ' select 
	sc.'+@RunName +' Shaft, 
	isnull(pc.Note,'''') Department, 
	isnull(ProblemColorText,'''') ColorText , isnull(COUNT(pc.Note),0) NumberOFProb
from PLANNING b 
inner join SECTION_COMPLETE sc on
	b.PRODMONTH = sc.PRODMONTH and
	b.SECTIONID = sc.SECTIONID 
inner join SECCAL s on
	sc.PRODMONTH = s.PRODMONTH and
	sc.SECTIONID_1 = s.SECTIONID 
inner join vw_CalTypes ct on
	s.CalendarCode = ct.CalendarCode and
	s.BeginDate <= ct.Calendardate and
	s.ENDDATE >= ct.Calendardate and
	b.Calendardate = ct.Calendardate
left join vw_Problem_Complete pc on
	b.ProblemID = pc.ProblemID and
	b.ACTIVITY = pc.Activity 
where --b.PRODMONTH >= 201210 and
	b.Calendardate >= '''+@WeekStart+''' and
	b.Calendardate <= '''+@TheDate+''' and 
	isnull(pc.CausedLostBlast,''N'') = ''N'' and
	b.PlanCode = ''MP'' and 
	b.Activity in '+@Activity +'
group by sc.'+@RunName +', pc.Note,pc.ProblemColorText 
order by sc.'+@RunName +', NumberOFProb desc '

--print (@SQL)
exec (@SQL)
go

create Procedure [dbo].[sp_ProblemsReport_Month] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20) 
as

--set @ToDate = '2016-12-09'
--set @Level = 'Shaft'

Declare @WeekStart DateTime,
@Shaft varchar(60),
@Department varchar(150),
@NumDepProb varchar(7),
@TotalProb varchar(7),
@TheID varchar(7),
@SQL varchar(8000),
@RunName VARCHAR(100)

IF @Level = 3
  SET @RunName = 'Name_3'
  
IF @Level = 4
  SET @RunName = 'Name_2'

IF (@Activity = 'Stoping')
BEGIN
	set @Activity = '(0,3)'
END
ELSE
BEGIN
	set @Activity = '(1)'
END


set @WeekStart = convert(varchar(10), @ToDate-6, 120)

delete from Temp_ProblemsReport_Shaft where Host = (select HOST_NAME())

delete from Temp_ProblemsReport_Shaft_Host where Host = (select HOST_NAME());

--get to 3 depertment per shaft

set @SQL = '
	DECLARE Shaft_Cursor CURSOR FOR
	select distinct b.'+@RunName +' Name_3 
	from Planmonth a inner join section_complete b on
		a.Prodmonth = b.prodmonth and
		a.Sectionid = b.sectionid, sysset
	where a.prodmonth = sysset.currentProductionMonth  
	and a.Activity in '+@Activity +' '

--print (@SQL)
exec (@SQL)

OPEN Shaft_Cursor;
FETCH NEXT FROM Shaft_Cursor
into @Shaft;

Select @TheID = 0 
 
WHILE @@FETCH_STATUS = 0
BEGIN
	set @SQL = '
	insert into Temp_ProblemsReport_Shaft (Host, Shaft, Department, NumDepProb, TotalProb)
	select a.*, b.TotProblems from
	(
		select top 3 (select HOST_NAME()) Host,
			sc.'+@RunName +' Shaft,
			isnull(pc.Note,'''') Department, 
			isnull(Count(pc.Note),0) NumProblems
		from PLANNING b 
		inner join SECTION_COMPLETE sc on
			b.PRODMONTH = sc.PRODMONTH and
			b.SECTIONID = sc.SECTIONID 
		inner join SECCAL s on
			sc.PRODMONTH = s.PRODMONTH and
			sc.SECTIONID_1 = s.SECTIONID 
		inner join vw_CalTypes ct on
			s.CalendarCode = ct.CalendarCode and
			s.BeginDate <= ct.Calendardate and
			s.ENDDATE >= ct.Calendardate and
			b.CALENDARDATE = ct.CALENDARDATE
		left join vw_Problem_Complete pc on
			b.ProblemID = pc.ProblemID and
			b.ACTIVITY = pc.Activity
		inner join [Code_FinancialYear] fy on
			fy.StartProdMonth <= b.PRODMONTH and
			fy.EndProdMonth >= b.PRODMONTH
		where 
			b.PRODMONTH = (select CURRENTPRODUCTIONMONTH from SYSSET) and
			isnull(pc.CausedLostBlast, ''N'') = ''N'' and
			b.PlanCode = ''MP'' and
			b.Activity in '+@Activity +' and 
			sc.'+@RunName +' = '''+@Shaft+''' 
		group by sc.'+@RunName +', pc.Note 
		order by NumProblems desc
	) a inner join 

	(
		select 
			sc.'+@RunName +' Shaft, 
			isnull(Count(pc.Note),0) TotProblems
		from PLANNING b 
		inner join SECTION_COMPLETE sc on
			b.PRODMONTH = sc.PRODMONTH and
			b.SECTIONID = sc.SECTIONID 
		inner join SECCAL s on
			sc.PRODMONTH = s.PRODMONTH and
			sc.SECTIONID_1 = s.SECTIONID 
		inner join vw_CalTypes ct on
			s.CalendarCode = ct.CalendarCode and
			s.BeginDate <= ct.Calendardate and
			s.ENDDATE >= ct.Calendardate and
			b.Calendardate = ct.CALENDARDATE
		left join vw_Problem_Complete pc on
			b.ProblemID = pc.ProblemID and
			b.ACTIVITY = pc.Activity
		inner join Code_FinancialYear fy on
			fy.StartProdMonth <= b.PRODMONTH and
			fy.EndProdMonth >= b.PRODMONTH
		where b.PRODMONTH = (select CURRENTPRODUCTIONMONTH from SYSSET) and 
			isnull(pc.CausedLostBlast, ''N'') = ''N'' and
			b.PlanCode = ''MP'' and
			b.Activity in '+@Activity +' and 
			sc.'+@RunName +' = '''+@Shaft+'''
		group by sc.'+@RunName +' 
	) b on
	a.shaft = b.shaft
	order by NumProblems desc '
    --print (@SQL)
	exec (@SQL) 
   FETCH NEXT FROM Shaft_Cursor
     Into @Shaft;
END; 

CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;

set @SQL = ''

-- get top 3 reasons per depart ment per shaft
DECLARE Department_Cursor CURSOR FOR
select TheID, isnull(Shaft,'') Shaft, isnull(Department,'') Department, 
	isnull(NumDepProb,0) NumDepProb, isnull(TotalProb,0) TotalProb  
from Temp_ProblemsReport_Shaft where Host = (Select Host_Name()) 

OPEN Department_Cursor;
FETCH NEXT FROM Department_Cursor
into @TheID, @Shaft, @Department, @NumDepProb, @TotalProb;
  
WHILE @@FETCH_STATUS = 0
BEGIN
	set @SQL = '
	insert into Temp_ProblemsReport_Shaft_Host (TheID, Host, Shaft, Department, Problem, NumDepProb, NumProb, TotalProb )
	select top 3 '''+@TheID+''', (Select Host_Name()), 
		sc.'+@RunName +' Shaft, 
		'''+@Department+''', isnull(pc.Problem,'''') Problem, isnull('+@NumDepProb+',0),
		isnull(Count(pc.Problem),0) NumProblems, isnull('+@TotalProb+',0)
	from PLANNING b 
	inner join SECTION_COMPLETE sc on
		b.PRODMONTH = sc.PRODMONTH and
		b.SECTIONID = sc.SECTIONID 
	inner join SECCAL s on
		sc.PRODMONTH = s.PRODMONTH and
		sc.SECTIONID_1 = s.SECTIONID 
	inner join vw_CalTypes ct on
		s.CalendarCode = ct.CalendarCode and
		s.BeginDate <= ct.Calendardate and
		s.ENDDATE >= ct.Calendardate and
		b.Calendardate = ct.CALENDARDATE
	left join vw_Problem_Complete pc on
		b.ProblemID = pc.ProblemID and
		b.Activity = pc.Activity
	inner join [Code_FinancialYear] fy on
		fy.StartProdMonth <= b.PRODMONTH and
		fy.EndProdMonth >= b.PRODMONTH
	where b.PRODMONTH = (select CURRENTPRODUCTIONMONTH from SYSSET) and  
		  isnull(pc.CausedLostBlast, ''N'') = ''N'' and
		  pc.Note = '''+@Department+''' and 
		  b.PlanCode = ''MP'' and
		  b.Activity in '+@Activity +' and 
			sc.'+@RunName +' = '''+@Shaft+''' 
	group by sc.'+@RunName +', pc.Problem 
	order by NumProblems desc '
    --print (@SQL)
	exec (@SQL) 
   FETCH NEXT FROM Department_Cursor
     into @TheID, @Shaft, @Department, @NumDepProb, @TotalProb;
END;

CLOSE Department_Cursor;
DEALLOCATE Department_Cursor;

select *, 
	DepPer = Case when TotalProb = 0 then 0 else 
	  convert (Real,round(NumDepProb/TotalProb,2))
	end,
	ProbPer = Case when NumDepProb = 0 then 0 else 
	  convert (Real,round(NumProb/TotalProb,2))
	end
from Temp_ProblemsReport_Shaft_Host 
where Host = (Select Host_Name()) 
order by Shaft, DepPer desc, ProbPer desc
go

 create Procedure [dbo].[sp_ProblemsReport_MonthPie] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20) 
as

--set @ToDate = '2014-06-04'

Declare 
@SQL varchar(8000),
@RunName VARCHAR(100)

IF @Level = 3
  SET @RunName = 'Name_3'
  
IF @Level = 4
  SET @RunName = 'Name_2'

IF (@Activity = 'Stoping')
BEGIN
	set @Activity = '(0,3)'
END
ELSE
BEGIN
	set @Activity = '(1)'
END

set @SQL = ' select 
	sc.'+@RunName +' Shaft, 
	isnull(pc.Note,'''') Department, 
	isnull(ProblemColorText,'''') ColorText , isnull(COUNT(pc.Note),0) NumberOFProb
from PLANNING b 
inner join SECTION_COMPLETE sc on
	b.PRODMONTH = sc.PRODMONTH and
	b.SECTIONID = sc.SECTIONID 
inner join SECCAL s on
	sc.PRODMONTH = s.PRODMONTH and
	sc.SECTIONID_1 = s.SECTIONID 
inner join vw_CalTypes ct on
	s.CalendarCode = ct.CalendarCode and
	s.BeginDate <= ct.Calendardate and
	s.ENDDATE >= ct.Calendardate and
	b.Calendardate = ct.Calendardate
left join vw_Problem_Complete pc on
	b.ProblemID = pc.ProblemID and
	b.ACTIVITY = pc.Activity 
inner join Code_FinancialYear fy on
	fy.StartProdMonth <= b.PRODMONTH and
	fy.EndProdMonth >= b.PRODMONTH 
where 
	isnull(pc.CausedLostBlast, ''N'') = ''N'' and
	b.PlanCode = ''MP'' and 
	b.Activity in '+@Activity +'
group by sc.'+@RunName +', pc.Note,pc.ProblemColorText 
order by sc.'+@RunName +', NumberOFProb desc '

--print (@SQL)
exec (@SQL)
go

create Procedure [dbo].[sp_ProblemsReport_Headings] 
--Declare
@ToDate DateTime
as 

--Declare @ToDate DateTime

declare @WeekStart DateTime,
@Week1End DateTime,
@Week1Start DateTime,
@Week2End DateTime,
@Week2Start DateTime,
@Week3End DateTime,
@Week3Start DateTime,
@Month Numeric(7),
@Month1 Numeric(7),
@Month2 Numeric(7)

--set @ToDate = '2014-06-04'

set @WeekStart = @ToDate-6
set @Week1End = @WeekStart-1
set @Week1Start = @Week1End-6
set @Week2End = @Week1Start-1
set @Week2Start = @Week2End-6
set @Week3End = @Week2Start-1
set @Week3Start = @Week3End-6


select

@Month2 =
case 
when (convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),5,2)) = 1) then 
convert(Numeric(7),Convert(Varchar(4),convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),1,4))-1)+'11')

when (convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),5,2)) = 2) then 
convert(Numeric(7),Convert(Varchar(4),convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),1,4))-1)+'12')

else
CURRENTPRODUCTIONMONTH - 2
end,

@Month1 =
case 
when (convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),5,2)) = 1) then 
convert(Numeric(7),Convert(Varchar(4),convert(int,substring(CONVERT(varchar(7),sysset.CURRENTPRODUCTIONMONTH),1,4))-1)+'12')
else
CURRENTPRODUCTIONMONTH - 1
end,
@Month = CURRENTPRODUCTIONMONTH  from sysset


Select RIGHT('0' +cast(datepart(MM,@WeekStart) as Varchar), 2)+'/'+RIGHT('0' +cast(datepart(DD,@WeekStart) as Varchar), 2)+'-'+RIGHT('0' +cast(datepart(MM,@ToDate) as Varchar), 2)+'/'+RIGHT('0' +cast(datepart(DD,@ToDate) as Varchar), 2) [Week1],
RIGHT('0' +cast(datepart(MM,@Week1Start) as Varchar), 2)+'/'+RIGHT('0' +cast(datepart(DD,@Week1Start) as Varchar), 2)+'-'+RIGHT('0' +cast(datepart(MM,@Week1End) as Varchar), 2)+'/'+RIGHT('0' +cast(datepart(DD,@Week1End) as Varchar), 2) [Week2],
RIGHT('0' +cast(datepart(MM,@Week2Start) as Varchar), 2)+'/'+RIGHT('0' +cast(datepart(DD,@Week2Start) as Varchar), 2)+'-'+RIGHT('0' +cast(datepart(MM,@Week2End) as Varchar), 2)+'/'+RIGHT('0' +cast(datepart(DD,@Week2End) as Varchar), 2) [Week3],
RIGHT('0' +cast(datepart(MM,@Week3Start) as Varchar), 2)+'/'+RIGHT('0' +cast(datepart(DD,@Week1Start) as Varchar), 2)+'-'+RIGHT('0' +cast(datepart(MM,@Week3End) as Varchar), 2)+'/'+RIGHT('0' +cast(datepart(DD,@Week3End) as Varchar), 2) [Week4],
[MONTH] = 
Case 
when Substring(convert(Varchar(7),@Month),5,2) = '01' Then 'Jan-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '02' Then 'Feb-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '03' Then 'Mar-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '04' Then 'Apr-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '05' Then 'May-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '06' Then 'Jun-'+Substring(convert(Varchar(7),@Month),3,2)+' To date' 
when Substring(convert(Varchar(7),@Month),5,2) = '07' Then 'Jul-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '08' Then 'Aug-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '09' Then 'Sep-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '10' Then 'Oct-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '11' Then 'Nov-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
when Substring(convert(Varchar(7),@Month),5,2) = '12' Then 'Dec-'+Substring(convert(Varchar(7),@Month),3,2)+' To date'  
end,
[MONTH1] = 
Case 
when Substring(convert(Varchar(7),@Month1),5,2) = '01' Then 'Jan-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '02' Then 'Feb-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '03' Then 'Mar-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '04' Then 'Apr-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '05' Then 'May-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '06' Then 'Jun-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '07' Then 'Jul-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '08' Then 'Aug-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '09' Then 'Sep-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '10' Then 'Oct-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '11' Then 'Nov-'+Substring(convert(Varchar(7),@Month1),3,2) 
when Substring(convert(Varchar(7),@Month1),5,2) = '12' Then 'Dec-'+Substring(convert(Varchar(7),@Month1),3,2) 
end,
[MONTH2] = 
Case 
when Substring(convert(Varchar(7),@Month2),5,2) = '01' Then 'Jan-'+Substring(convert(Varchar(7),@Month2),3,2)
when Substring(convert(Varchar(7),@Month2),5,2) = '02' Then 'Feb-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '03' Then 'Mar-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '04' Then 'Apr-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '05' Then 'May-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '06' Then 'Jun-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '07' Then 'Jul-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '08' Then 'Aug-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '09' Then 'Sep-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '10' Then 'Oct-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '11' Then 'Nov-'+Substring(convert(Varchar(7),@Month2),3,2) 
when Substring(convert(Varchar(7),@Month2),5,2) = '12' Then 'Dec-'+Substring(convert(Varchar(7),@Month2),3,2) 
end,
Quater =  
case 
  when (convert(int,substring(CONVERT(varchar(7),@Month),5,2)) between 1 and 3) then 'Quater1'
  when (convert(int,substring(CONVERT(varchar(7),@Month),5,2)) between 4 and 6) then 'Quater2'
  when (convert(int,substring(CONVERT(varchar(7),@Month),5,2)) between 7 and 9) then 'Quater3'
  when (convert(int,substring(CONVERT(varchar(7),@Month),5,2)) between 10 and 12) then 'Quater4'
end
go

create Procedure [dbo].[sp_ProblemsReport_Zeroes] 
as
select 0 TheID, '' Host, '' Shaft, '' Department, '' Problem,
	0 NumDepProb, 0 NumProb, 0 TotalProb,
	0 DepPer, 0 ProbPer
go
 create Procedure [dbo].[sp_ProblemsReport_ZeroesPie]  
as
select '' Shaft, '' Department, '' ColorText, 0 NumberOFProb
go



ALTER procedure [dbo].[sp_SICReport_Total]  --'MINEWARE', '2016-07-01', 'F', '4' 
--declare
@UserID varchar(10),
@CalendarDate varchar(10),
@SectionID varchar(20),
@Section int,
@MOName varchar(50)
--set @UserID ='MINEWARE'
--set @CalendarDate = '2017-01-11'
--set @SectionID = 'GM'
--set @Section ='1'
--set @MOName ='S Mofokeng'
as

declare @TheQuery varchar(8000)
declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)
declare @TheQuery3 varchar(8000)
declare @TheQuery4 varchar(8000)
declare @TheQuery5 varchar(8000)
declare @TheQuery6 varchar(8000)
declare @TheQuery7 varchar(8000)
declare @TheStartDate varchar(10)
declare @TheEndDate varchar(10)

select @TheStartDate = case when  convert(varchar(10),min(prevstartdate),120) is null then 
convert(varchar(10),min(startdate),120) else  convert(varchar(10),min(prevstartdate),120) end,
 @TheEndDate = convert(varchar(10),max(enddate),120)
from temp_sectionstartdate
where UserID = @UserID and
StartDate <= @CalendarDate and
EndDate >= @CalendarDate
--SELECT @TheStartDate
if (@TheStartDate is NULL)
BEGIN
	Select '' TheSection1, '' TheSection, 
'' workplaceid,
Dev_Check = Convert(decimal(10,1), 0),
Stp_Check = Convert(decimal(10,1), 0),
--Nr of Panels   
[Stp_PD_NoPanels] = Convert(Numeric(10,0), 0),  
[Stp_BD_NoPanels] = Convert(Numeric(10,0), 0),
[Stp_PP_NoPanels] = Convert(Numeric(10,0), 0),
[Stp_BP_NoPanels] = Convert(Numeric(10,0), 0),
--Nr of Dev Panels 
[Dev_PD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_BD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_VD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_PP_NoPanels] = Convert(Numeric(10,0), 0),  
[Dev_BP_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_VP_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_FP_NoPanels] = Convert(Numeric(10,0), 0),
--No of Cleaned Stope Panels
[Stp_BD_ClnPanels] = Convert(Numeric(10,0), 0),
--No of Cleaned Dev Panels
[Dev_PD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_BD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_VD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_PP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_BP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_VP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_FP_ClnPanels] = Convert(Numeric(10,0), 0),
--Shiftnr   
TheField1 = '''', 
ShiftNo =  Convert(Numeric(10,0), 0), 
TotalShft = Convert(Numeric(10,0), 0),
--M2 Mined
Stp_Plan_Sqm = Convert(Numeric(10,0), 0),
Stp_PD_Sqm = Convert(Numeric(10,0), 0),
Stp_BD_Sqm = Convert(Numeric(10,0), 0),
Stp_VD_Sqm = Convert(Numeric(10,0), 0),
Stp_PP_Sqm = Convert(Numeric(10,0), 0),
Stp_BP_Sqm = Convert(Numeric(10,0), 0),  
Stp_VP_Sqm = Convert(Numeric(10,0), 0),
Stp_FP_Sqm = Convert(Numeric(10,0), 0),
Stp_NewDay_Sqm = Convert(Numeric(10,0), 0),
Stp_Prev_Sqm = Convert(Numeric(10,0), 0),
Stp_PrevVar_Sqm = Convert(Numeric(10,0), 0),
Stp_DPerc_Sqm = Convert(Numeric(10,2), 0),
Stp_PPerc_Sqm = Convert(Numeric(10,2), 0),
Stp_FPSqm = Convert(Numeric(10,0), 0),	                                                                                                                         
--High Grade m2 Mined  
Stp_PD_HgSqm = Convert(Numeric(10,0), 0),
Stp_BD_HgSqm = Convert(Numeric(10,0), 0),
Stp_VD_HgSqm = Convert(Numeric(10,0), 0),
Stp_PP_HgSqm = Convert(Numeric(10,0), 0),
Stp_BP_HgSqm = Convert(Numeric(10,0), 0),  
Stp_VP_HgSqm = Convert(Numeric(10,0), 0),
Stp_FP_HgSqm = Convert(Numeric(10,0), 0),
Stp_NewDay_HgSqm = Convert(Numeric(10,0), 0),     
--Total Dev m2 Mined  
[Dev_PD_Sqm] = Convert(Numeric(10,0), 0),
[Dev_BD_Sqm] = Convert(Numeric(10,0), 0),  
[Dev_PP_Sqm] = Convert(Numeric(10,0), 0),
[Dev_BP_Sqm] =Convert(Numeric(10,0), 0),
--Total FL                               
[Stp_PD_FL] = Convert(Numeric(10,0), 0),
[Stp_BD_FL] = Convert(Numeric(10,0), 0),
[Stp_VD_FL] = Convert(Numeric(10,0), 0),
[Stp_PP_FL] =Convert(Numeric(10,0), 0),
[Stp_BP_FL] = Convert(Numeric(10,0), 0),
[Stp_VP_FL] = Convert(Numeric(10,0), 0),
[Stp_FP_FL] = Convert(Numeric(10,0), 0),

[Stp_PD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_BD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_VD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_PP_FLNS] =Convert(Numeric(10,0), 0),
[Stp_BP_FLNS] = Convert(Numeric(10,0), 0),
[Stp_VP_FLNS] = Convert(Numeric(10,0), 0),
[Stp_FP_FLNS] = Convert(Numeric(10,0), 0),
	
[Stp_PD_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_BD_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_VD_CleanFL] =Convert(Numeric(10,0), 0),
[Stp_PP_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_BP_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_VP_CleanFL] = Convert(Numeric(10,0), 0), 
[Stp_FP_CleanFL] = Convert(Numeric(10,0), 0),
--Total Dev FL   
[Dev_PD_FL] = Convert(Numeric(10,0), 0),
[Dev_BD_FL] = Convert(Numeric(10,0), 0),
[Dev_VD_FL] = Convert(Numeric(10,0), 0),             
[Dev_PP_FL] = Convert(Numeric(10,0), 0),
[Dev_BP_FL] = Convert(Numeric(10,0), 0),
[Dev_VP_FL] = Convert(Numeric(10,0), 0),
 
--Total Reef Metres  
Dev_Plan_Adv = Convert(Numeric(10,2), 0),
Dev_PD_RAdv = Convert(Numeric(10,2), 0),
Dev_BD_RAdv = Convert(Numeric(10,2), 0),
Dev_VD_RAdv = Convert(Numeric(10,2), 0),
Dev_PP_RAdv = Convert(Numeric(10,2), 0),
Dev_BP_RAdv = Convert(Numeric(10,2), 0),
Dev_VP_RAdv = Convert(Numeric(10,2), 0),
Dev_Prev_RAdv = Convert(Numeric(10,2), 0),
Dev_PrevVar_RAdv = Convert(Numeric(10,2), 0),
Dev_DPerc_RAdv = Convert(Numeric(10,2), 0),
Dev_PPerc_RAdv = Convert(Numeric(10,2), 0),
Dev_FP_RAdv = Convert(Numeric(10,2), 0),
		
--Total Waste Metres                                   
Dev_PD_WAdv = Convert(Numeric(10,2), 0),
Dev_BD_WAdv = Convert(Numeric(10,2), 0),
Dev_VD_WAdv = Convert(Numeric(10,2), 0),
Dev_PP_WAdv = Convert(Numeric(10,2), 0),
Dev_BP_WAdv = Convert(Numeric(10,2), 0),
Dev_VP_WAdv = Convert(Numeric(10,2), 0),
Dev_Prev_WAdv = Convert(Numeric(10,2), 0),
Dev_PrevVar_WAdv = Convert(Numeric(10,2), 0),
Dev_DPerc_WAdv = Convert(Numeric(10,2), 0),
Dev_PPerc_WAdv = Convert(Numeric(10,2), 0),
Dev_FP_WAdv = Convert(Numeric(10,2), 0),	

--Total Metres                                                  
Dev_PD_Adv = Convert(Numeric(10,2), 0),
Dev_BD_Adv = Convert(Numeric(10,2), 0),
Dev_VD_Adv = Convert(Numeric(10,2), 0),
Dev_PP_Adv = Convert(Numeric(10,2), 0),
Dev_BP_Adv = Convert(Numeric(10,2), 0),
Dev_VP_Adv = Convert(Numeric(10,2), 0),
Dev_Prev_Adv = Convert(Numeric(10,2), 0),
Dev_PrevVar_Adv = Convert(Numeric(10,2), 0),
Dev_DPerc_Adv = Convert(Numeric(10,2), 0),
Dev_PPerc_Adv = Convert(Numeric(10,2), 0),	
Dev_FP_Adv = Convert(Numeric(10,2), 0),

--High Grade Reef Metres
Dev_PD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_BD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_VD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_PP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_BP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_VP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_FP_HgRAdv = Convert(Numeric(10,2), 0),

--tons                          
Dev_PD_RTons = Convert(Numeric(10,2), 0),
Dev_BD_RTons = Convert(Numeric(10,2), 0),
Dev_VD_RTons = Convert(Numeric(10,2), 0),
Dev_PP_RTons = Convert(Numeric(10,2), 0),
Dev_BP_RTons = Convert(Numeric(10,2), 0),
Dev_VP_RTons = Convert(Numeric(10,2), 0),
Dev_FP_RTons = Convert(Numeric(10,2), 0),
Dev_PD_WTons =Convert(Numeric(10,2), 0),
Dev_BD_WTons = Convert(Numeric(10,2), 0),
Dev_VD_WTons = Convert(Numeric(10,2), 0),
Dev_PP_WTons = Convert(Numeric(10,2), 0),
Dev_BP_WTons = Convert(Numeric(10,2), 0),
Dev_VP_WTons = Convert(Numeric(10,2), 0),
Dev_FP_WTons = Convert(Numeric(10,2), 0),

Da_StopeTons = Convert(Numeric(10,2), 0),
DaB_StopeTons = Convert(Numeric(10,2), 0),
DaV_StopeTons = Convert(Numeric(10,2), 0),   
D_StopeTons = Convert(Numeric(10,2), 0),           
B_StopeTons = Convert(Numeric(10,2), 0),
V_StopeTons = Convert(Numeric(10,2), 0),       
F_StopeTons = Convert(Numeric(10,2), 0),

--SQM Lost
Stp_BD_Mis = Convert(Numeric(10,1), 0),
Stp_BD_Labour = Convert(Numeric(10,1), 0),
Stp_BD_Other = Convert(Numeric(10,1), 0),
Stp_BP_Mis = Convert(Numeric(10,1), 0),
Stp_BP_Labour = Convert(Numeric(10,1), 0),
Stp_BP_Other = Convert(Numeric(10,1), 0),
TheField20 = '''',
TheField21 = '''',TheField22 = '''',    
TheField23 = '''', TheField24 = '''', TheField25 = '''',TheField26 = '''',TheField27 = '''',    
TheField28 = '''',

--Sweepings       
Stp_PP_Sweeps = Convert(Numeric(10,0), 0),

--Reef SW Excl Gullies
Stp_PP_ReefSW = Convert(Numeric(10,1), 0),
Stp_BP_ReefSW = Convert(Numeric(10,1), 0),
Stp_VP_ReefSW = Convert(Numeric(10,1), 0),
Stp_FP_ReefSW = Convert(Numeric(10,1), 0),

--Total cmg/t                         
[Stp_PP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_BP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_VP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_FP_Sqmcmgt] = Convert(Numeric(10,0), 0),
 
 --Content   
[Stp_PP_Content] = Convert(Numeric(10,4), 0),                                    
[Stp_BP_Content] = Convert(Numeric(10,4), 0),    
[Stp_VP_Content] = Convert(Numeric(10,4), 0),    
[Stp_PD_Content] = Convert(Numeric(10,4), 0),    
[Stp_BD_Content] =Convert(Numeric(10,4), 0),    
[Stp_VD_Content] = Convert(Numeric(10,4), 0),    
[Stp_FP_Content] = Convert(Numeric(10,4), 0)

END
ELSE
BEGIN
   
set @TheQuery = 'select ' 
IF (@Section = 1)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_5 +'':''+sc.Name_5,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 2)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_4 +'':''+sc.Name_4,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 3)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_3 +'':''+sc.Name_3,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 4)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								 isnull(sc.SectionID_1 +'':''+sc.Name_1,'''') TheSection, '
END 
set @TheQuery = @TheQuery + ' 
isnull(workplaceid,'''') workplaceid,
Dev_Check = Convert(decimal(10,1),sum(isnull(DevCheck,0))),
Stp_Check = Convert(decimal(10,1),sum(isnull(StopeCheck,0))),
--Nr of Stope Blasted Panels   
[Stp_PD_NoPanels] = sum(Da_StopeTrue),  
[Stp_BD_NoPanels] = sum(DaB_StopeTrue), 
[Stp_PP_NoPanels] = sum(D_StopeTrue), 
[Stp_BP_NoPanels] = sum(B_StopeTrue),
--Nr of Dev Blasted Panels 
[Dev_PD_NoPanels] = sum(Da_DevTrue), 
[Dev_BD_NoPanels] = sum(DaB_DevTrue), 
[Dev_VD_NoPanels] = sum(DaB_DevTrue) - sum(Da_DevTrue), 
[Dev_PP_NoPanels] = sum(D_DevTrue),    
[Dev_BP_NoPanels] = sum(B_DevTrue),
[Dev_VP_NoPanels] = sum(B_DevTrue) - sum(D_DevTrue),
[Dev_FP_NoPanels] = sum(B_DevTrue) + sum(F_DevTrue),
--No of Cleaned Stope Panels
[Stp_BD_ClnPanels] = sum(DaB_CleanTrue),
--No of Cleaned Dev Panels
[Dev_PD_ClnPanels] = sum(Da_DevCleanTrue),
[Dev_BD_ClnPanels] = sum(DaB_DevCleanTrue),
[Dev_VD_ClnPanels] = sum(DaB_DevCleanTrue) - sum(Da_DevCleanTrue),
[Dev_PP_ClnPanels] = sum(D_DevCleanTrue),
[Dev_BP_ClnPanels] = sum(B_DevCleanTrue),
[Dev_VP_ClnPanels] = sum(B_DevCleanTrue) - sum(D_DevCleanTrue),
[Dev_FP_ClnPanels] = sum(B_DevCleanTrue) + sum(F_DevCleanTrue),

--Shiftnr   

TheField1 = '''', 
ShiftNo =  max(isnull(shiftnr,0)),   
TotalShft = max(isnull(Totalnumdays,0)),
--M2 Mined
Stp_Plan_Sqm = Convert(Numeric(10,0),sum(DM_m2)),
Stp_PD_Sqm = Convert(Numeric(10,0),sum(Da_m2)),  
Stp_BD_Sqm = Convert(Numeric(10,0),SUM(DaB_Sqm)),
Stp_VD_Sqm = Convert(Numeric(10,0),SUM(DaB_Sqm) - sum(Da_m2)),
Stp_PP_Sqm = Convert(Numeric(10,0),sum(D_m2)),
Stp_BP_Sqm = Convert(Numeric(10,0),sum(B_Sqm)),  
Stp_VP_Sqm = Convert(Numeric(10,0),sum(B_Sqm) - sum(D_m2)),
Stp_FP_Sqm = Convert(Numeric(10,0),sum(B_Sqm) + sum(F_m2)),
Stp_NewDay_Sqm = Convert(Numeric(10,0),case when (max(totalnumdays)- max(Shiftnr)) = 0 then 0 
				else (sum(DM_m2) - sum(B_Sqm))/(max(totalnumdays)- max(Shiftnr)) end), 
Stp_Prev_Sqm = Convert(Numeric(10,0),sum(BP_Sqm)),
Stp_PrevVar_Sqm = Convert(Numeric(10,0),sum(B_Sqm) - sum(BP_Sqm)), 
Stp_DPerc_Sqm = case when sum(isnull(Da_m2,0)) > 0 then
		Convert(Numeric(10,2),sum(DaB_Sqm)/sum(isnull(Da_m2,0)) * 100 ) else 0 end,
Stp_PPerc_Sqm = case when sum(isnull(D_m2,0)) > 0 then
		Convert(Numeric(10,2),sum(B_Sqm)/sum(isnull(D_m2,0)) * 100 ) else 0 end,
Stp_FPSqm = Convert(Numeric(10,0),sum(B_Sqm) + sum(F_m2)), 			                                                                                                                         

--High Grade m2 Mined  
Stp_PD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(Da_tm21)) else Convert(Numeric(10,0),sum(Da_tm2)) end, 
Stp_BD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(DaB_tm21)) else Convert(Numeric(10,0),sum(DaB_tm2)) end, 
Stp_VD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(DaB_tm21) - sum(Da_tm21)) else Convert(Numeric(10,0),sum(DaB_tm2) - sum(Da_tm2)) end, 
Stp_PP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(D_tm21)) else Convert(Numeric(10,0),sum(D_tm2)) end,
Stp_BP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(B_tm21)) else Convert(Numeric(10,0),sum(B_tm2)) end,   
Stp_VP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(B_tm21) - sum(D_tm21)) else Convert(Numeric(10,0),sum(B_tm2) - sum(D_tm2)) end, 
Stp_FP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(F_Tm21)) else Convert(Numeric(10,0),sum(F_Tm2)) end, 
--Stp_NewDay_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
--					else (sum(DM_tm21) - sum(B_tm21))/(max(totalnumdays) - max(Shiftnr)) end) else 0 end, 
Stp_NewDay_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),
						case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
							 else (sum(DM_tm21) - sum(B_tm21))/(max(totalnumdays) - max(Shiftnr)) end) 
					    else Convert(Numeric(10,0),case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
				   else (sum(DM_tm2) - sum(B_tm2))/(max(totalnumdays) - max(Shiftnr)) end) end, 
     
--Total Dev m2 Mined  
[Dev_PD_Sqm] = Convert(Numeric(10,0),sum(Da_Devm2)),  
[Dev_BD_Sqm] = Convert(Numeric(10,0),sum(DaB_Devm2)),     
[Dev_PP_Sqm] = Convert(Numeric(10,0),sum(D_Devm2)),
[Dev_BP_Sqm] = Convert(Numeric(10,0),sum(B_Devm2)),   
--Total FL                               
[Stp_PD_FL] = Convert(Numeric(10,0),isnull(sum(Da_FL),0)),
[Stp_BD_FL] = Convert(Numeric(10,0),isnull(sum(DaB_FL),0)),
[Stp_VD_FL] = Convert(Numeric(10,0),isnull(sum(DaB_FL),0) - isnull(sum(Da_FL),0)),
[Stp_PP_FL] = Convert(Numeric(10,0),isnull(sum(D_FL),0)),
[Stp_BP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0)),
[Stp_VP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0) - isnull(sum(D_FL),0)),
[Stp_FP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0) + isnull(sum(F_FL),0)),

[Stp_PD_FLNS] = Convert(Numeric(10,0),isnull(sum(Da_FLNS),0)),
[Stp_BD_FLNS] = Convert(Numeric(10,0),isnull(sum(DaB_FLNS),0)),
[Stp_VD_FLNS] = Convert(Numeric(10,0),isnull(sum(DaB_FLNS),0) - isnull(sum(Da_FLNS),0)),
[Stp_PP_FLNS] = Convert(Numeric(10,0),isnull(sum(D_FLNS),0)),
[Stp_BP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0)),
[Stp_VP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0) - isnull(sum(D_FLNS),0)),
[Stp_FP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0) + isnull(sum(F_FLNS),0)),
	
[Stp_PD_CleanFL] = Convert(Numeric(10,0),sum(isnull(Da_CleanFL,0))), 
[Stp_BD_CleanFL] = Convert(Numeric(10,0),sum(isnull(DaB_CleanFL,0))), 
[Stp_VD_CleanFL] = Convert(Numeric(10,0),sum(isnull(DaB_CleanFL,0)) - sum(isnull(Da_CleanFL,0))),  
[Stp_PP_CleanFL] = Convert(Numeric(10,0),sum(isnull(D_CleanFL,0))),
[Stp_BP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0))), 
[Stp_VP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0)) - sum(isnull(D_CleanFL,0))),  
[Stp_FP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0)) + sum(isnull(F_CleanFL,0))),  
--Total Dev FL   
[Dev_PD_FL] = case when sum(Da_DevWAdv) + sum(Da_DevRAdv) > 0 then      
  Convert(Numeric(10,0),sum(Da_Devm2) / (sum(Da_DevWAdv) + sum(Da_DevRAdv))) else 0 end,  
[Dev_BD_FL] = case when sum(DaB_DevRAdv) > 0 then     
  Convert(Numeric(10,0),sum(DaB_Devm2) / sum(DaB_DevRAdv)) else 0 end,    
[Dev_VD_FL] = (case when sum(DaB_DevWAdv) + sum(DaB_DevRAdv) > 0 then     
   Convert(Numeric(10,0),sum(DaB_Devm2) / (sum(DaB_DevWAdv) + sum(DaB_DevRAdv))) else 0 end) - 
   (case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then
  Convert(Numeric(10,0),sum(D_Devm2) / sum(D_DevWAdv + D_DevRAdv)) else 0 end),                             
[Dev_PP_FL] = case when sum(D_DevWAdv + D_DevRAdv) > 0 then   
  Convert(Numeric(10,0),sum(D_Devm2) / (sum(D_DevWAdv) + sum(D_DevRAdv))) else 0 end, 
[Dev_BP_FL] = case when sum(B_DevRAdv) > 0 then    
   Convert(Numeric(10,0),sum(B_Devm2) / sum(B_DevRAdv)) else 0 end, 
[Dev_VP_FL] = (case when sum(B_DevWAdv) + sum(B_DevRAdv) > 0 then          
   Convert(Numeric(10,0),sum(B_Devm2) / (sum(B_DevWAdv) + sum(B_DevRAdv))) else 0 end) - 
   (case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then 
   Convert(Numeric(10,0),sum(D_Devm2) / (sum(D_DevWAdv) + sum(D_DevRAdv))) else 0 end),'
set @TheQuery1 = '   
--Total Reef Metres  
Dev_Plan_Adv = Convert(Numeric(10,2),sum(isnull(DM_DevAdv,0))), 
Dev_PD_RAdv = Convert(Numeric(10,2),sum(Da_DevRAdv)),
Dev_BD_RAdv = Convert(Numeric(10,2),sum(DaB_DevRAdv)), 
Dev_VD_RAdv = Convert(Numeric(10,2),sum(DaB_DevRAdv) - sum(Da_DevRAdv)),  
Dev_PP_RAdv = Convert(Numeric(10,2),sum(D_DevRAdv)),
Dev_BP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv)),
Dev_VP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) - sum(D_DevRAdv)), 
Dev_Prev_RAdv = Convert(Numeric(10,2),sum(BP_DevRAdv)),
Dev_PrevVar_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) - sum(BP_DevRAdv)), 
Dev_DPerc_RAdv = case when sum(Da_DevRAdv) > 0 then
		Convert(Numeric(10,2),(sum(DaB_DevRAdv)/sum(Da_DevRAdv)*100)) else 0 end, 
Dev_PPerc_RAdv = case when sum(D_DevRAdv) > 0 then
		Convert(Numeric(10,2),(sum(B_DevRAdv)/sum(D_DevRAdv)*100)) else 0 end,
Dev_FP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) + sum(F_DevRAdv)),  
		
--Total Waste Metres                                   
Dev_PD_WAdv = Convert(Numeric(10,2),sum(Da_DevWAdv)), 
Dev_BD_WAdv = Convert(Numeric(10,2),sum(DaB_DevWAdv)), 
Dev_VD_WAdv = Convert(Numeric(10,2),sum(DaB_DevWAdv)- sum(Da_DevWAdv)),
Dev_PP_WAdv = Convert(Numeric(10,2),sum(D_DevWAdv)),
Dev_BP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv)),
Dev_VP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) - sum(D_DevWAdv)), 
Dev_Prev_WAdv = Convert(Numeric(10,2),sum(BP_DevWAdv)),
Dev_PrevVar_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) - sum(BP_DevWAdv)), 
Dev_DPerc_WAdv = case when sum(Da_DevWAdv) > 0 then
		Convert(Numeric(10,2),(sum(DaB_DevWAdv)/sum(Da_DevWAdv)*100)) else 0 end, 
Dev_PPerc_WAdv = case when sum(D_DevWAdv) > 0 then
		Convert(Numeric(10,2),(sum(B_DevWAdv)/sum(D_DevWAdv)*100)) else 0 end, 
Dev_FP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) + sum(F_DevWAdv)), 		

--Total Metres                                                  
Dev_PD_Adv = Convert(Numeric(10,2),sum(Da_DevWAdv) + sum(Da_DevRAdv)), 
Dev_BD_Adv = Convert(Numeric(10,2),sum(DaB_DevRAdv)+sum(DaB_DevWAdv)), 
Dev_VD_Adv = Convert(Numeric(10,2),(sum(DaB_DevRAdv) + sum(DaB_DevWAdv)) - (sum(Da_DevWAdv) +sum(Da_DevRAdv))),
Dev_PP_Adv = Convert(Numeric(10,2),sum(D_DevWAdv) + sum(D_DevRAdv)), 
Dev_BP_Adv = Convert(Numeric(10,2),sum(B_DevRAdv) + sum(B_DevWAdv)),   
Dev_VP_Adv = Convert(Numeric(10,2),(sum(B_DevRAdv) + sum(B_DevWAdv)) - (sum(D_DevWAdv) + sum(D_DevRAdv))),
Dev_Prev_Adv = Convert(Numeric(10,2),sum(BP_DevRAdv) + sum(BP_DevWAdv)),   
Dev_PrevVar_Adv = Convert(Numeric(10,2),(sum(B_DevRAdv) + sum(B_DevWAdv)) - (sum(BP_DevRAdv) + sum(BP_DevWAdv))),
Dev_DPerc_Adv = case when sum(Da_DevRAdv) + sum(Da_DevWAdv) > 0 then
		Convert(Numeric(10,2),((sum(DaB_DevRAdv) + sum(DaB_DevWAdv)) / (sum(Da_DevRAdv) + sum(Da_DevWAdv))*100)) else 0 end, 
Dev_PPerc_Adv = case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then
		Convert(Numeric(10,2),((sum(B_DevRAdv) + sum(B_DevWAdv)) / (sum(D_DevWAdv) + sum(D_DevRAdv))*100)) else 0 end, 	
Dev_FP_Adv = Convert(Numeric(10,2),sum(B_TDevRAdv) + sum(B_TDevWAdv) + sum(F_TDevRAdv)),

--High Grade Reef Metres
case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(Da_tm21)) else Convert(Numeric(10,0),sum(Da_tm2)) end, 
Dev_PD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(Da_TDevRAdv1)) else Convert(Numeric(10,2),sum(Da_TDevRAdv)) end,
Dev_BD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(DaB_TDevRAdv1)) else Convert(Numeric(10,2),sum(DaB_TDevRAdv)) end,
Dev_VD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(DaB_TDevRAdv1) - sum(Da_TDevRAdv1)) else Convert(Numeric(10,2),sum(DaB_TDevRAdv) - sum(Da_TDevRAdv)) end,
Dev_PP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(D_TDevRAdv1)) else Convert(Numeric(10,2),sum(D_TDevRAdv)) end,
Dev_BP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(B_TDevRAdv1)) else Convert(Numeric(10,2),sum(B_TDevRAdv)) end,
Dev_VP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(B_TDevRAdv1) - sum(D_TDevRAdv1)) else Convert(Numeric(10,2),sum(B_TDevRAdv) - sum(D_TDevRAdv)) end,
Dev_FP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(F_TDevRAdv1)) else Convert(Numeric(10,2),sum(F_TDevRAdv)) end,

--tons                          
Dev_PD_RTons = Convert(Numeric(10,2),sum(Da_DevRTons)),
Dev_BD_RTons = Convert(Numeric(10,2),sum(DaB_DevRTons)),
Dev_VD_RTons = Convert(Numeric(10,2),sum(DaB_DevRTons) - sum(Da_DevRTons)),
Dev_PP_RTons = Convert(Numeric(10,2),sum(D_DevRTons)),
Dev_BP_RTons = Convert(Numeric(10,2),sum(B_DevRTons)), 
Dev_VP_RTons = Convert(Numeric(10,2),sum(B_DevRTons) - sum(D_DevRTons)), 
Dev_FP_RTons = Convert(Numeric(10,2),sum(B_DevRTons) + sum(F_DevRTons)),
Dev_PD_WTons = Convert(Numeric(10,2),sum(Da_DevWTons)), 
Dev_BD_WTons = Convert(Numeric(10,2),sum(DaB_DevWTons)),
Dev_VD_WTons = Convert(Numeric(10,2),sum(DaB_DevWTons) - sum(Da_DevWTons)), 
Dev_PP_WTons = Convert(Numeric(10,2),sum(D_DevWTons)), 
Dev_BP_WTons = Convert(Numeric(10,2),sum(B_DevWTons)), 
Dev_VP_WTons = Convert(Numeric(10,2),sum(B_DevWTons) - sum(D_DevWTons)),
Dev_FP_WTons = Convert(Numeric(10,2),sum(B_DevWTons) + sum(F_DevWTons)),

Da_StopeTons = Convert(Numeric(10,2),sum(Da_StopeTons)), 
DaB_StopeTons = Convert(Numeric(10,2),sum(DaB_StopeTons)), 
DaV_StopeTons = Convert(Numeric(10,2),sum(DaB_StopeTons) - sum(Da_StopeTons)),        
D_StopeTons = Convert(Numeric(10,2),sum(D_StopeTons)),                 
B_StopeTons = Convert(Numeric(10,2),sum(B_StopeTons)),  
V_StopeTons = Convert(Numeric(10,2),sum(B_StopeTons) - sum(D_StopeTons)),         
F_StopeTons = Convert(Numeric(10,2),sum(F_StopeTons)), ' 
set @TheQuery2 = '
--SQM Lost
Stp_BD_Mis = Convert(Numeric(10,1),sum(Da_Misefires)), 
Stp_BD_Labour = Convert(Numeric(10,1),sum(Da_Labour)), 
Stp_BD_Other = Convert(Numeric(10,1),sum(Da_Other)), 
Stp_BP_Mis = Convert(Numeric(10,1),sum(D_Misefires)), 
Stp_BP_Labour = Convert(Numeric(10,1),sum(D_Labour)), 
Stp_BP_Other = Convert(Numeric(10,1),sum(D_Other)),   
TheField20 = '''',
TheField21 = '''',TheField22 = '''',    
TheField23 = '''', TheField24 = '''', TheField25 = '''',TheField26 = '''',TheField27 = '''',    
TheField28 = '''',

--Sweepings       
Stp_PP_Sweeps = Convert(Numeric(10,0),sum(D_ReefSweep + D_WasteSweep)),   

--Reef SW Excl Gullies
Stp_PP_ReefSW = case when sum(D_Reefm2) > 0 then
	Convert(Numeric(10,1),sum(D_ReefSqmSW) / sum(D_Reefm2)) else 0 end, 
Stp_BP_ReefSW = case when sum(B_Reefm2) > 0 then
	Convert(Numeric(10,1),sum(B_ReefSqmSW) / sum(B_Reefm2)) else 0 end, 
Stp_VP_ReefSW = case when sum(B_Reefm2) + sum(D_Reefm2) > 0 then
	Convert(Numeric(10,1),(sum(B_ReefSqmSW) + sum(D_ReefSqmSW)) / (sum(B_Reefm2) + sum(D_Reefm2))) else 0 end,
Stp_FP_ReefSW = case when sum(B_Reefm2) + sum(F_Reefm2) > 0 then
	(sum(B_ReefSqmSW) + sum(F_ReefSqmSW))/(sum(B_Reefm2) + sum(F_Reefm2)) else 0 end,	

--Total cmg/t                         
[Stp_PP_Sqmcmgt] = case when sum(D_Reefm2) > 0 then
	Convert(Numeric(10,0),sum(D_sqmreefcmgt) / sum(D_Reefm2)) else 0 end,
[Stp_BP_Sqmcmgt] = case when sum(B_Reefm2) > 0 then
	Convert(Numeric(10,0),sum(B_sqmreefcmgt) / sum(B_Reefm2)) else 0 end, 
[Stp_VP_Sqmcmgt] = case when sum(B_Reefm2) + sum(D_Reefm2) > 0 then
	Convert(Numeric(10,0),(sum(B_sqmreefcmgt) + sum(D_sqmreefcmgt)) / (sum(B_Reefm2) + sum(D_Reefm2))) else 0 end,
[Stp_FP_Sqmcmgt] = case when sum(B_Reefm2) + sum(F_reefm2) > 0 then
	(sum(B_sqmreefcmgt) + sum(F_sqmreefcmgt)) / (sum(B_Reefm2) + sum(F_reefm2)) else 0 end, 
 
 --Content   
[Stp_PP_Content] = Convert(Numeric(10,4),sum(D_grams)/1000) ,                                       
[Stp_BP_Content] = Convert(Numeric(10,4),sum(B_grams)/1000), 
[Stp_VP_Content] = Convert(Numeric(10,4),(sum(B_grams)/1000) - (sum(D_grams) / 1000)),
[Stp_PD_Content] = Convert(Numeric(10,4),sum(Da_grams)/1000),
[Stp_BD_Content] = Convert(Numeric(10,4),sum(DaB_grams)/1000),   
[Stp_VD_Content] = Convert(Numeric(10,4),(sum(DaB_grams)/1000) - (sum(Da_grams) / 1000)),
[Stp_FP_Content] = Convert(Numeric(10,4),(sum(B_grams)/ 1000) + (sum(F_grams) / 1000))
from
		 (select 
p.workplaceid,p.prodmonth,p.sectionid,p.calendardate,
totalnumdays = case when p.prodmonth = ts.ProdMonth then wd.TotalShifts else 0 end,
Shiftnr = case when (p.prodmonth = ts.ProdMonth) and (wd.calendardate = '''+@CalendarDate+''') 
	then wd.shift else 0 end,
DevCheck = case when (p.prodmonth = ts.ProdMonth) and (p.activity = 1) 
	then 1 else 0 end,
StopeCheck = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then 1 else 0 end,  
TheToppanelsMO = (case when tp.workplaceid is not null then 1 else 0 end),
TheToppanelsMAN = (case when t.workplaceid is not null then 1 else 0 end),
Da_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.sqm > 0) then 1 else 0 end,
DaB_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then 1 else 0 end,     
D_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.sqm > 0) then 1 else 0 end, 
B_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then 1 else 0 end,    
	      
DaB_Cleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.Value > 0) then 1 else 0 end,
	 
Da_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end,
DaB_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (cl.Value > 0) and (cl.Sickey = 22) then 1 else 0 end,     
D_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
B_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (cl.Value > 0) and (cl.Sickey = 22) then 1 else 0 end,          
F_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
Da_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.BookMetresAdvance > 0) then 1 else 0 end,
DaB_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (cl1.Value > 0) and (cl1.Sickey = 23) then 1 else 0 end,     
D_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.BookMetresAdvance > 0) then 1 else 0 end, 
B_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (cl1.Value > 0) and (cl1.Sickey = 23) then 1 else 0 end, 
F_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
D_ReefSweep = 0,                                                                                                                           
D_WasteSweep = 0,'
set @TheQuery3 = '
DM_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) then p.sqm else 0 end, 
DM_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then p.reefsqm else 0 end, 
DM_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then p.wastesqm  else 0 end,     
DM_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and (t.workplaceid is not null)
	then p.sqm else 0 end, 
DM_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and
	 (t.workplaceid is not null) then p.reefsqm else 0 end, 
DM_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and 
	(t.workplaceid is not null) then p.wastesqm  else 0 end, 
Da_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.sqm else 0 end,
Da_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9))then p.reefsqm else 0 end,
Da_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.wastesqm  else 0 end,  
DaB_Sqm =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookSqm else 0 end,     
DaB_reefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookReefSqm else 0 end,     
DaB_offreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookWasteSqm else 0 end,        
D_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.sqm else 0 end, 
D_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm else 0 end, 
D_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and 
	(p.activity IN (0,9)) then p.wastesqm  else 0 end, 
B_Sqm =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookSqm else 0 end,    
B_reefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookReefSqm else 0 end,    
B_offreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookWasteSqm else 0 end,   
F_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.sqm else 0 end, 
F_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	 (p.activity IN (0,9)) then p.reefsqm else 0 end, 
F_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.wastesqm else 0 end,'
set @TheQuery4 = ' 	
Da_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end,
Da_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.reefsqm else 0 end,
Da_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.wastesqm  else 0 end,  
DaB_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.BookSqm else 0 end, 
DaB_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookReefSqm else 0 end,     
DaB_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) then p.BookWasteSqm else 0 end,        
D_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end, 
D_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.reefsqm else 0 end, 
D_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.wastesqm  else 0 end, 
B_Tm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.BookSqm else 0 end,    
B_Treefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) 
	then p.BookReefSqm else 0 end,    
B_Toffreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) 
	then p.BookWasteSqm else 0 end, 
F_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end,

DM_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and (tp.workplaceid is not null)
	then p.sqm else 0 end,	
Da_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end,
Da_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.reefsqm else 0 end,
Da_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.wastesqm  else 0 end,  
DaB_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.BookSqm else 0 end, 
DaB_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookReefSqm else 0 end,     
DaB_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) then p.BookWasteSqm else 0 end,        
D_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end, 
D_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.reefsqm else 0 end, 
D_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.wastesqm  else 0 end, 
B_Tm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.BookSqm else 0 end,    
B_Treefm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) 
	then p.BookReefSqm else 0 end,    
B_Toffreefm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) 
	then p.BookWasteSqm else 0 end, 
F_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end,
	
BP_Sqm =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and  
	(p.activity IN (0,9)) then p.BookSqm else 0 end,    
--BP_reefm2 =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
--	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') then p.BookSqm else 0 end, 
--BP_offreefm2 =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
--	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') then p.BookSqm else 0 end,                               
 
Da_FL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end,   
DaB_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.bookSqm > 0) then P.bookFL else 0 end,   
D_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end, 
B_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.bookSqm > 0) then P.bookFL else 0 end,       
F_FL = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end, 
	
Da_FLNS =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end,   
DaB_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.SicKey = 22) then cl.Value else 0 end,   
D_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end, 
B_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.SicKey = 22) then cl.Value else 0 end,       
F_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end, 

Da_CleanFL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end,
DaB_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl1.SicKey = 23) then cl1.Value else 0 end,     
D_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end, '
set @TheQuery5 = ' 
B_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (cl1.SicKey = 23) then cl1.Value else 0 end,
F_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end,
-- vir stoping advance per blast
Da_MAdv =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end,   
DaB_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookMetresAdvance else 0 end,  
D_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end,   
B_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookMetresAdvance else 0 end,        
F_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end, 
	   
DM_DevAdv = case when (p.prodmonth = ts.ProdMonth) and (p.activity = 1) 
	then p.MetresAdvance else 0 end, 
BP_DevRAdv = case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end,
Da_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end, 
DaB_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end,
D_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end,
B_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end, 
F_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end,                               

BP_DevWAdv = case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
Da_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteAdv else 0 end,  
DaB_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
D_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteAdv else 0 end,
B_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
F_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate >'''+@CalendarDate+''') AND 
	(p.activity = 1) then p.WasteAdv else 0 end,                                

Da_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,  
DaB_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end,
D_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,
B_TDevRAdv = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end, 
F_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,                                   

B_TDevWAdv = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end,
Da_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,  
DaB_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end,
D_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,
B_TDevRAdv1 = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end, 
F_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,                                   

B_TDevWAdv1 = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end,


Da_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end,
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity  else 0 end, 
DaB_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.BookReefTons else 0 end,
	--(pp.ReefWaste = ''R'') then p.BookMetresAdvance * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                               
D_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end,
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity  else 0 end, 
B_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookReefTons else 0 end,
	--and (pp.ReefWaste = ''R'') p.BookMetresAdvance * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                               
F_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end, 
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity else 0 end,   

Da_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) then p.WasteTons else 0 end,
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity  else 0 end,
DaB_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookWasteTons else 0 end,
	--and (pp.ReefWaste = ''W'') then p.BookAdv * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                   
D_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteTons else 0 end,
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity  else 0 end,
B_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookWasteTons else 0 end,
	--and (pp.ReefWaste = ''W'') then p.BookAdv * pp.DHeight * pp.DWidth * sl.RockDensity else 0 end,  
F_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) then p.WasteTons else 0 end,  
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity else 0 end, '
Set @TheQuery7 = '      
Da_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,        
D_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,         
DaB_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookTons else 0 end,
	--p.BookSqm * p.BookSW / 100 * sl.RockDensity else 0 end,         
B_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookTons else 0 end, 
	--p.BookSqm * p.BookSW / 100 * sl.RockDensity else 0 end,          
F_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,                             

Da_DevUReefTons = 0, 
	--case when (p.prodmonth = ts.ProdMonth) and 
	--(p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) and (pp.ReefWaste = ''R'') then p.Units * pp.dens  else 0 end,    
D_DevUReefTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and 
	--(p.activity = 1) and (pp.ReefWaste = ''R'') then p.Units * pp.dens  else 0 end,
Da_DevUWasteTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	--(p.activity = 1) and (pp.ReefWaste = ''W'') then p.Units * pp.dens  else 0 end, 
D_DevUWasteTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	--(p.activity = 1) and (pp.ReefWaste = ''W'') then p.Units * pp.dens  else 0 end,

Da_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Grams else 0 end, 
DaB_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookGrams else 0 end,
D_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Grams else 0 end, 
B_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and    
	(p.activity IN (0,9)) then p.BookGrams else 0 end,                                       
F_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.Grams else 0 end,

Da_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm * p.SW else 0 end,
DaB_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm * p.BookSW else 0 end,                                          
D_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm * p.SW else 0 end,
B_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm  * p.BookSW else 0 end,                                                                                                          
F_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.SW * p.SQM else 0 end,                                                                                                 

D_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.CMGT * p.reefsqm else 0 end,
B_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <='''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm * p.Bookcmgt  else 0 end,
Da_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.CMGT * p.reefsqm else 0 end, 
DaB_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	 (p.activity IN (0,9)) then p.BookReefSqm * p.Bookcmgt  else 0 end,  
F_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.CMGT * p.reefsqm else 0 end,

Da_Misefires = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and
		    (p.ProblemID = ''B1'') and (p.CausedLostBlast = ''Y'') and (p.activity IN (0,9)) then p.sqm else 0 end, 
Da_Labour = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
			(p.ProblemID in (''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and
			(p.activity IN (0,9)) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
Da_Other =  
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
			(p.ProblemID not in (''B1'',''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
			(p.activity IN (0,9)) and (p.ProblemID is not null) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
D_Misefires = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and 
		    (p.ProblemID = ''B1'') and (p.CausedLostBlast = ''Y'') and 
			(p.activity IN (0,9)) and (p.sqm > 0) then p.sqm else 0 end, 
D_Labour = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and  
            (p.ProblemID in (''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
            (p.activity IN (0,9)) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
D_Other = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and  
			(p.ProblemID not in (''B1'',''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
			(p.activity IN (0,9))  and (p.ProblemID is not null) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end,
F_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) then (p.MetresAdvance * pp.DWidth) else 0 end,  			 
--DaB_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance + p.BookAdv) * pp.DWidth else 0 end,
DaB_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance) * pp.DWidth else 0 end,
D_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and (p.activity = 1) then (p.WasteAdv + p.ReefAdv) * pp.dwidth else 0 end,
Da_Devm2 = case when(p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''')and (p.activity = 1) then (p.WasteAdv + p.ReefAdv) * pp.dwidth else 0 end,  
B_Devm2 = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance) * pp.dwidth else 0 end                               
--B_Devm2 = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance + p.BookMetresAdvance) * pp.DWidth else 0 end
from Planning p 
left outer join PLANMONTH pp on
  pp.workplaceid = p.workplaceid and 
  pp.Prodmonth = p.Prodmonth and  
  pp.SectionID = p.SectionID and 
  pp.Activity = p.Activity and 
  pp.PlanCode = p.PlanCode and
  pp.IsCubics = p.IsCubics 
--left outer join problems_Complete r on
--	p.workplaceid = r.workplaceid and 
--	p.Prodmonth = r.Prodmonth and  
--	p.SectionID = r.SectionID and 
--	p.Activity = r.Activity and   
--	p.CalendarDate = r.CalendarDate
inner join section_complete sc on
  sc.SectionID = p.sectionid and 
  sc.prodmonth = p.prodmonth ' 
set @TheQuery6 = ''
  IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + '
left outer join TopPanelsSelected t on
  t.Prodmonth = p.Prodmonth and
  t.SectionID = sc.SectionID_5 and
  t.WorkplaceID = p.WorkplaceID and
  t.Activity = p.Activity 
  left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_2 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity  '
 END 
  IF (@Section = 1) or (@Section = 2)or (@Section = 3)
BEGIN
   set @TheQuery6 = @TheQuery6 + '
left outer join TopPanelsSelected t on
  t.Prodmonth = p.Prodmonth and
  t.SectionID = sc.SectionID_5 and
  t.WorkplaceID = p.WorkplaceID and
  t.Activity = p.Activity 
  left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_2 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity'
 END
 set @TheQuery6 = @TheQuery6 + '  
left outer join Temp_SectionStartDate ts on
  ts.SectionID = p.SectionID and
  ts.UserID = '''+@UserID+''' 
left outer join TempWorkdaysMO wd on
  wd.UserID = '''+@UserID+''' and
  wd.SectionID = sc.SectionID_2 and
  wd.Prodmonth = ts.ProdMonth and
  wd.CalendarDate = '''+@CalendarDate+'''
left outer join tempworkdaysMO wd1 on
  wd1.UserID = '''+@UserID+''' and   
  wd1.sectionid = ts.SectionID and  
  wd1.ProdMonth = ts.PrevProdMonth and
  wd1.Shift = wd.Shift 
left outer join SICCapture cl on
  cl.CalendarDate = p.CalendarDate and
  cl.SICKey in (22) and
  cl.SectionID = sc.SectionID_1 and
  cl.WorkplaceID = p.WorkplaceID
  left outer join SICCapture cl1 on
  cl1.CalendarDate = p.CalendarDate and
  cl1.SICKey in (23) and
  cl1.SectionID = sc.SectionID_1 and
  cl1.WorkplaceID = p.WorkplaceID,    
 Sysset sl                   
where p.CalendarDate >= '''+@TheStartDate+''' and p.CalendarDate <= '''+@TheEndDate+''' and
		p.PlanCode = ''MP'' and p.IsCubics = ''N'' '
IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_2 = '''+@SectionID+''' and sc.Name_2 = '''+@MOName+''' '
END 
if(@Section = 3)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_3 = '''+@SectionID+''' '
end
if(@Section = 2)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_4 = '''+@SectionID+''' '
end 
if(@Section = 1)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_5 = '''+@SectionID+''' '
end
set @TheQuery6 = @TheQuery6 + ') a 
left outer join Temp_SectionStartDate t on
  t.SectionID = a.SectionID and
  t.UserID = '''+@UserID+''' 
inner join Section_Complete sc on
  sc.ProdMonth = t.ProdMonth and
  sc.SectionID = a.SectionID ' 
  if(@Section = 4)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_2 ='''+CAST(@SectionID AS VARCHAR(10))+''' and 
				sc.Name_2 = '''+@MOName+''' '
  end
  if(@Section = 3)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_3 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
  if(@Section = 2)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_4 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
  if(@Section = 1)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_5 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
set @TheQuery6 = @TheQuery6 +'group by ' 
IF (@Section = 1)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_5 +'':''+sc.Name_5,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 2)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_4 +'':''+sc.Name_4,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 3)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_3 +'':''+sc.Name_3,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , 
								 isnull(sc.SectionID_1 +'':''+sc.Name_1,'''') , '
END 
set @TheQuery6 = @TheQuery6 + ' a.Workplaceid with rollup '


--Print @TheQuery
--Print @TheQuery1
--Print @TheQuery2
--Print @TheQuery3
--Print @TheQuery4
--Print @TheQuery5
--Print @TheQuery7
--Print @TheQuery6

Exec (@TheQuery+@TheQuery1+@TheQuery2+@TheQuery3+@TheQuery4+@TheQuery5+ @TheQuery7+@TheQuery6)
	
END
 go

 ALTER procedure [dbo].[sp_SICReport_Tramming] --'MINEWARE', '2013/12/02', '1', '1' 
--declare
@UserID varchar(20),
@CalendarDate varchar(10),
@SectionID varchar(20),
@Section int,
@MOName varchar(50)
as

--set @UserID = 'MINEWARE'
--set @CalendarDate = '2017-01-11'
--set @SectionID = 'GM'
--set @Section = 1
--set @MOName = 'S Mofokeng'

declare @TheQuery varchar(8000)
declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)

declare @TheStartDate varchar(10),
@TheEndDate varchar(10),
@TheMonth varchar(10)

SET NOCOUNT ON;

--IF (@Section = 1) or (@Section = 2) or (@Section = 3)
--BEGIN
	select @TheStartDate = convert(varchar(10),a.StartDate,120),
		   @TheEndDate = convert(varchar(10),a.EndDate,120),	
		   @TheMonth = a.MillMonth
	from
		(select min(cm.StartDate) StartDate, max(cm.EndDate) EndDate, cm.MillMonth  
			  from CODE_CALENDAR cc
			  inner join CALENDARMILL cm on
				cm.CalendarCode = cc.CalendarCode
			  where cm.StartDate <= @CalendarDate and 
			  cm.EndDate >= @CalendarDate and
			  cc.Description = 'Mill Calendar' 
			  group by cm.MillMonth
		) a
--END
--ELSE
--BEGIN
--	select @TheStartDate = convert(varchar(10),a.StartDate,120),
--		   @TheEndDate = convert(varchar(10),a.EndDate,120),
--		   @TheMonth = a.Prodmonth
--	from
--		(select distinct(s.BeginDate) StartDate, s.EndDate EndDate, s.Prodmonth
--			from seccal s 
--			inner join SectionComplete sc on
--			  sc.ProdMonth = s.ProdMonth and
--			  sc.SBID = s.SectionID
--			where s.BeginDate <= @CalendarDate and
--				  s.EndDate >= @CalendarDate and
--				  sc.MOID = @SectionID
--		) a
--END  

if (@TheStartDate is NULL)
BEGIN
  select '' TheSection1, '' TheSection,
	 Dev_Count = Convert(decimal(13,2),0),
	 Stp_Count = Convert(decimal(13,2),0),
	 TheActivity = '',
	 DayP_Broken = Convert(decimal(13,2),0),
	 DayB_Broken = Convert(decimal(13,2),0),
	 DayV_Broken = Convert(decimal(13,2),0),
	 ProgP_Broken = Convert(decimal(13,2),0),	
	 ProgF_Broken = Convert(decimal(13,2),0),
	 ProgB_Broken = Convert(decimal(13,2),0),	
	 ProgV_Broken = Convert(decimal(13,2),0),
	 DayP_Tot_Tons = Convert(decimal(13,2),0),
	 DayB_Tot_Tons = Convert(decimal(13,2),0),				  
	 ProgP_Tot_Tons = Convert(decimal(13,2),0),
	 ProgF_Tot_Tons = Convert(decimal(13,2),0),
	 ProgB_Tot_Tons = Convert(decimal(13,2),0),		
	 MonthPlan_Stp_Tons = Convert(decimal(13,2),0),
	 DayP_Stp_Tons = Convert(decimal(13,2),0),
	 DayB_Stp_Tons = Convert(decimal(13,2),0),				  
	 ProgP_Stp_Tons = Convert(decimal(13,2),0),
	 ProgF_Stp_Tons = Convert(decimal(13,2),0),
	 ProgB_Stp_Tons = Convert(decimal(13,2),0),					  					  						  					  
	 MonthPlan_Dev_ReefTons = Convert(decimal(13,2),0),					  
	 DayP_Dev_ReefTons = Convert(decimal(13,2),0),
	 DayB_Dev_ReefTons = Convert(decimal(13,2),0),				   
	 ProgP_Dev_ReefTons = Convert(decimal(13,2),0),
	 ProgB_Dev_ReefTons = Convert(decimal(13,2),0),				  
	 MonthPlan_Dev_WasteTons = Convert(decimal(13,2),0),				  						  
	 DayP_Dev_WasteTons = Convert(decimal(13,2),0),
	 DayB_Dev_WasteTons = Convert(decimal(13,2),0),				  
	 ProgP_Dev_WasteTons = Convert(decimal(13,2),0),
	 ProgB_Dev_WasteTons = Convert(decimal(13,2),0),						  
	 MonthPlan_Dev_TotalTons = Convert(decimal(13,2),0),						  					  
	 DayP_Dev_TotalTons = Convert(decimal(13,2),0),
	 DayB_Dev_TotalTons = Convert(decimal(13,2),0),				  					  					  
	 ProgP_Dev_TotalTons = Convert(decimal(13,2),0),
	 ProgB_Dev_TotalTons = Convert(decimal(13,2),0),
	 DayB_Stp_TopTons = Convert(decimal(13,2),0),
	 ProgB_Stp_TopTons = Convert(decimal(13,2),0),
	 MonthPlan_Stp_TopTons = Convert(decimal(13,2),0),
	 DayP_Stp_TopTons = Convert(decimal(13,2),0),
	 ProgP_Stp_TopTons = Convert(decimal(13,2),0),
	 DayV_Stp_TopTons = Convert(decimal(13,2),0),					  						  
	 ProgV_Stp_TopTons = Convert(decimal(13,2),0),
	 DayV_Stp_Tons = Convert(decimal(13,2),0),
	 ProgV_Stp_Tons = Convert(decimal(13,2),0),
	 ProgF_Stp_Tons = Convert(decimal(13,2),0),
	 DayV_Tot_Tons = Convert(decimal(13,2),0),
	 ProgV_Tot_Tons = Convert(decimal(13,2),0),
	 ProgF_Tot_Tons = Convert(decimal(13,2),0),
	 DayV_Dev_ReefTons = Convert(decimal(13,2),0),
	 ProgV_Dev_ReefTons = Convert(decimal(13,2),0),
	 DayV_Dev_WasteTons = Convert(decimal(13,2),0),
	 ProgV_Dev_WasteTons = Convert(decimal(13,2),0),					   
	 DayV_Dev_TotalTons = Convert(decimal(13,2),0),					  						  
	 ProgV_Dev_TotalTons = Convert(decimal(13,2),0)
END
ELSE
BEGIN
	set @TheQuery = '
select isnull(TheSection,'''') TheSection1, isnull(TheSection,'''') TheSection, 
 TheActivity = isnull(TheActivity,''''),
 Dev_Count = Convert(decimal(13,2),sum(Dev_Count)),
 Stp_Count = Convert(decimal(13,2),sum(Stp_Count)),
 DayP_Broken = Convert(decimal(13,2),sum(DayP_Broken)),
 DayB_Broken = Convert(decimal(13,2),sum(DayB_Broken)),
 DayV_Broken = Convert(decimal(13,2),sum(DayB_Broken) - sum(DayP_Broken)),
 ProgP_Broken = Convert(decimal(13,2),sum(ProgP_Broken)),		
 ProgB_Broken = Convert(decimal(13,2),sum(ProgB_Broken)),	
 ProgF_Broken = Convert(decimal(13,2),sum(ProgB_Broken) + sum(ProgF_Broken)),	
 ProgV_Broken = Convert(decimal(13,2),sum(ProgB_Broken) - sum(ProgP_Broken)),	
 DayP_Tot_Tons = Convert(decimal(13,2),isnull(sum(DayP_Tot_Tons),0)),
 DayB_Tot_Tons = Convert(decimal(13,2),isnull(sum(DayB_Tot_Tons),0)),				  
 ProgP_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgP_Tot_Tons),0)),
 ProgF_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgF_Tot_Tons),0)),
 ProgB_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgB_Tot_Tons),0)),		
 MonthPlan_Stp_Tons = Convert(decimal(13,2),sum(MonthPlan_Stp_Tons)),
 DayP_Stp_Tons = Convert(decimal(13,2),sum(DayP_Stp_Tons)),
 DayB_Stp_Tons = Convert(decimal(13,2),sum(DayB_Stp_Tons)),				  
 ProgP_Stp_Tons = Convert(decimal(13,2),sum(ProgP_Stp_Tons)),
 ProgF_Stp_Tons = Convert(decimal(13,2),sum(ProgF_Stp_Tons)),
 ProgB_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons)),					  						  						  					  
 MonthPlan_Dev_ReefTons = Convert(decimal(13,2),sum(MonthPlan_Dev_ReefTons)),					  
 DayP_Dev_ReefTons = Convert(decimal(13,2),sum(DayP_Dev_ReefTons)),
 DayB_Dev_ReefTons = Convert(decimal(13,2),sum(DayB_Dev_ReefTons)),				   
 ProgP_Dev_ReefTons = Convert(decimal(13,2),sum(ProgP_Dev_ReefTons)),
 ProgB_Dev_ReefTons = Convert(decimal(13,2),sum(ProgB_Dev_ReefTons)),				  
 MonthPlan_Dev_WasteTons = Convert(decimal(13,2),sum(MonthPlan_Dev_WasteTons)),				  						  
 DayP_Dev_WasteTons = Convert(decimal(13,2),sum(DayP_Dev_WasteTons)),
 DayB_Dev_WasteTons = Convert(decimal(13,2),sum(DayB_Dev_WasteTons)),				  
 ProgP_Dev_WasteTons = Convert(decimal(13,2),sum(ProgP_Dev_WasteTons)),
 ProgB_Dev_WasteTons = Convert(decimal(13,2),sum(ProgB_Dev_WasteTons)),						  
 MonthPlan_Dev_TotalTons = Convert(decimal(13,2),sum(MonthPlan_Dev_TotalTons)),						  					  
 DayP_Dev_TotalTons = Convert(decimal(13,2),sum(DayP_Dev_TotalTons)),
 DayB_Dev_TotalTons = Convert(decimal(13,2),sum(DayB_Dev_TotalTons)),				  					  					  
 ProgP_Dev_TotalTons = Convert(decimal(13,2),sum(ProgP_Dev_TotalTons)),
 ProgB_Dev_TotalTons = Convert(decimal(13,2),sum(ProgB_Dev_TotalTons)),
 DayB_Stp_TopTons = Convert(decimal(13,2),sum(DayB_Stp_TopTons)),
 ProgB_Stp_TopTons = Convert(decimal(13,2),sum(ProgB_Stp_TopTons)),
 MonthPlan_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then MonthPlan_Stp_TopTons1 else MonthPlan_Stp_TopTons end)),	
 DayP_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then DayP_Stp_TopTons1 else DayP_Stp_TopTons end)),
 ProgP_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then ProgP_Stp_TopTons1 else ProgP_Stp_TopTons end )),
 DayV_Stp_TopTons = Convert(decimal(13,2),sum(DayB_Stp_TopTons) - sum(case when TopPanelsMO <> 0 then DayP_Stp_TopTons1 else DayP_Stp_TopTons end)),					  						  
 ProgV_Stp_TopTons = Convert(decimal(13,2),sum(ProgB_Stp_TopTons) - sum(ProgP_Stp_TopTons)),
 DayV_Stp_Tons = Convert(decimal(13,2),sum(DayB_Stp_Tons) - sum(DayP_Stp_Tons)),
 ProgV_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons) - sum(ProgP_Stp_Tons)),
 ProgF_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons) + sum(ProgF_Stp_Tons)),
 DayV_Tot_Tons = Convert(decimal(13,2),sum(DayB_Tot_Tons) - sum(DayP_Tot_Tons)),
 ProgV_Tot_Tons = Convert(decimal(13,2),sum(ProgB_Tot_Tons) - sum(ProgP_Tot_Tons)),
 ProgF_Tot_Tons = Convert(decimal(13,2),sum(ProgB_Tot_Tons) + sum(ProgF_Tot_Tons)),
 DayV_Dev_ReefTons = Convert(decimal(13,2),sum(DayB_Dev_ReefTons) - sum(DayP_Dev_ReefTons)),
 ProgV_Dev_ReefTons = Convert(decimal(13,2),sum(ProgB_Dev_ReefTons) - sum(ProgP_Dev_ReefTons)),
 DayV_Dev_WasteTons = Convert(decimal(13,2),sum(DayB_Dev_WasteTons) - sum(DayP_Dev_WasteTons)),
 ProgV_Dev_WasteTons = Convert(decimal(13,2),sum(ProgB_Dev_WasteTons) - sum(ProgP_Dev_WasteTons)),					   
 DayV_Dev_TotalTons = Convert(decimal(13,2),sum(DayB_Dev_TotalTons) - sum(DayP_Dev_TotalTons)),					  						  
 ProgV_Dev_TotalTons = Convert(decimal(13,2),sum(ProgB_Dev_TotalTons) -	sum(ProgP_Dev_TotalTons))
from
(
(
  select  '
IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery = @TheQuery + ' case when t.Sectionid = ''Unidentified'' then t.SectionID else isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') end TheSection1, 
		case when t.Sectionid = ''Unidentified'' then t.SectionID else isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') end TheSection,  '
END
IF (@Section = 4)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END  
set @TheQuery = @TheQuery + ' Activity, TheActivity = case when t.Activity IN (0,9) then ''Stp'' else ''Dev'' end,
   DayP_Broken = 0,	
   DayB_Broken = 0,	
   ProgP_Broken = 0,		
   ProgF_Broken = 0,
   ProgB_Broken = 0,		
   DayP_Tot_Tons = 0,	
   DayB_Tot_Tons = sum(case when (t.CalendarDate = '''+@CalendarDate+''')   
						then (t.DTons + t.ATons + t.NTons)  else 0 end),					     		  
   ProgP_Tot_Tons = Convert(decimal(13,2),0),
   ProgF_Tot_Tons = Convert(decimal(13,2),0),
   ProgB_Tot_Tons = sum(t.DTons + t.ATons + t.NTons),		
   DayB_Stp_Tons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Stp_Tons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then (t.DTons + t.ATons + t.NTons) else 0 end), 
   DayB_Dev_ReefTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.ReefWaste = ''R'') and
							(t.Activity = 1) 
					    then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Dev_WasteTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.ReefWaste = ''W'') and
							(t.Activity = 1)  
  					   then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Dev_TotalTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity = 1)  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_ReefTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.ReefWaste = ''R'') and
							(t.Activity = 1)   
  					   then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_WasteTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.ReefWaste = ''W'') and
							(t.Activity = 1)   
  						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_TotalTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity = 1)  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Stp_TopTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then t.TopTons else 0 end),
   ProgB_Stp_TopTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then t.TopTons else 0 end),		 						
   Dev_Count = sum(case when (t.Activity = 1) then 1 else 0 end), 	
   Stp_Count = sum(case when (t.Activity IN (0,9)) then 1 else 0 end),
   TopPanelsMO = 0,
   TopPanelsMAN = 0,
   MonthPlan_Stp_Tons = 0,	
   DayP_Stp_Tons = 0,						  
   ProgP_Stp_Tons = 0,				  					  						  					  
   ProgF_Stp_Tons = 0,
   MonthPlan_Dev_ReefTons = 0,					  
   DayP_Dev_ReefTons = 0,				   
   ProgP_Dev_ReefTons = 0,						  
   MonthPlan_Dev_WasteTons = 0,					  						  
   DayP_Dev_WasteTons = 0,							  
   ProgP_Dev_WasteTons = 0,							  
   MonthPlan_Dev_TotalTons = 0,				  					  
   DayP_Dev_TotalTons = 0,ProgP_Dev_TotalTons = 0,MonthPlan_Stp_TopTons = 0,
   MonthPlan_Stp_TopTons1 = 0,DayP_Stp_TopTons = 0, 
   ProgP_Stp_TopTons = 0,DayP_Stp_TopTons1 = 0, ProgP_Stp_TopTons1 = 0  									
 from BookingTramming t
 left outer join vw_Sections_Complete_MO sc on
   sc.ProdMonth = t.millMonth and
   sc.SectionID_2 = t.SectionID  
 where t.CalendarDate >= '''+@TheStartDate+''' and
	   t.CalendarDate <= '''+@CalendarDate+''' '
set @TheQuery2 = ''
IF (@Section = 4)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_2 = '''+@SectionID+''' and sc.Name_2 = '''+@MOName+''' '

END 
IF (@Section = 3)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_3 = '''+@SectionID+''' '

END
IF (@Section = 2)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_4 = '''+@SectionID+''' '

END
IF (@Section = 1)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_5 = '''+@SectionID+''' '

END
set @TheQuery1 = ' group by '
IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' t.SectionID, sc.SectionID_2, sc.Name_2, '
END
IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' sc.SectionID_2, sc.Name_2, '
END
set @TheQuery1 = @TheQuery1 + 'Activity '
  set @TheQuery1 = @TheQuery1 + '
 )union
(select '
--IF (@Section = 1)
--BEGIN
--   set @TheQuery1 = @TheQuery1 + ' isnull(sc.MOID +'':''+sc.MOName,'''') TheSection1, 
--								   isnull(sc.MOID +'':''+sc.MOName,'''') TheSection, '
--END
--IF (@Section = 4)
--BEGIN
--   set @TheQuery1 = @TheQuery1 + ' isnull(sc.MOID +'':''+sc.MOName,'''') TheSection1, 
--								   isnull(sc.MOID +'':''+sc.MOName,'''') TheSection, '
--END  
set @TheQuery1 = @TheQuery1 + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								   isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
set @TheQuery1 = @TheQuery1 + ' p.Activity, TheActivity=  case when p.Activity IN (0,9) then ''Stp'' else ''Dev'' end,
 DayP_Broken = sum(case when (p.CalendarDate = '''+@CalendarDate+''')   and (p.Activity IN (0,9)) 
						  then (p.BookTons) else 0 end),
 DayB_Broken = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.booktons)
						  else 0 end),
 ProgP_Broken = sum(case when (p.CalendarDate <= '''+@CalendarDate+''')  and (p.Activity IN (0,9)) 
						  then (p.BookTons) else 0 end),
 ProgF_Broken = sum(case when (p.CalendarDate > '''+@CalendarDate+''')  and (p.Activity IN (0,9)) 
						  then (p.Tons) else 0 end),
 ProgB_Broken = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.Booktons)
						  else 0 end),		
 DayP_Tot_Tons = sum(case when (p.CalendarDate = '''+@CalendarDate+''')  
						  then (p.BookTons) else 0 end),	
 DayB_Tot_Tons = Convert(decimal(13,5),0),				  
 ProgP_Tot_Tons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''')
						  then (p.BookTons)else 0 end),
 ProgF_Tot_Tons = sum(case when (p.CalendarDate > '''+@CalendarDate+''')
						  then (p.Tons)else 0 end),
 ProgB_Tot_Tons = 0,	
 DayB_Stp_Tons = 0,	
 ProgB_Stp_Tons = 0,	
 DayB_Dev_ReefTons = 0,	
 DayB_Dev_WasteTons = 0,	
 DayB_Dev_TotalTons = 0,	
 ProgB_Dev_ReefTons = 0,	
 ProgB_Dev_WasteTons = 0,	
 ProgB_Dev_TotalTons = 0,	
 DayB_Stp_TopTons = 0,	
 ProgB_Stp_TopTons = 0,				
 Dev_Count = sum(case when (p.Activity = 1) then 1 else 0 end),
 Stp_Count = sum(case when (p.Activity IN (0,9)) then 1 else 0 end),
 ToppanelsMO = sum(case when tp1.workplaceid is not null then 1 else 0 end),
 ToppanelsMAN = sum(case when tp.workplaceid is not null then 1 else 0 end),
 MonthPlan_Stp_Tons =sum(case when (p.Activity IN (0,9)) 
						  then p.Tons
						  else 0 end),  							  							  
 DayP_Stp_Tons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.BookTons)
						  else 0 end),						  
 ProgP_Stp_Tons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.BookTons)
						  else 0 end),
 ProgF_Stp_Tons = sum(case when (p.CalendarDate > '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.Tons)
						  else 0 end),					  								  						  					  
 MonthPlan_Dev_ReefTons = sum(case when (p.Activity = 1)  
						  then p.ReefTons
						  else 0 end),						  
 DayP_Dev_ReefTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.BookReefTons 
						  else 0 end),					   
 ProgP_Dev_ReefTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1)   
						  then p.bookReeftons
						  else 0 end),						  
 MonthPlan_Dev_WasteTons = sum(case when (p.Activity = 1)  
						  then p.WasteTons
						  else 0 end),						  						  
 DayP_Dev_WasteTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1)   
						  then p.bookWastetons
						  else 0 end),						  
 ProgP_Dev_WasteTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.bookWastetons
						  else 0 end),								  
 MonthPlan_Dev_TotalTons = sum(case when (p.Activity = 1) 
						  then p.tons
						  else 0 end),		
				  					  
 DayP_Dev_TotalTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.booktons
						  else 0 end) ,					  					  					  
 ProgP_Dev_TotalTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.booktons
						  else 0 end),
 MonthPlan_Stp_TopTons = sum(case when (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null)
						  then p.tons
						  else 0 end), 
 MonthPlan_Stp_TopTons1 = sum(case when (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null)
						  then p.tons
						  else 0 end),
 DayP_Stp_TopTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null)
						  then p.booktons
						  else 0 end), 
 ProgP_Stp_TopTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null) 
						  then p.booktons
						   else 0 end),
 DayP_Stp_TopTons1 = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null)
						  then p.booktons
						  else 0 end), 
 ProgP_Stp_TopTons1 = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null) 
						  then p.booktons
						   else 0 end)   						  						  						  
from Planning  p 
inner join PLANMONTH pp on
  p.Prodmonth = pp.Prodmonth and
  p.SectionID = pp.Sectionid and
  p.WorkplaceID = pp.Workplaceid and
  p.Activity = pp.Activity and
  p.PlanCode = pp.PlanCode and
  p.IsCubics = pp.IsCubics
inner join Section_Complete sc on
  sc.Prodmonth = p.Prodmonth and
  sc.SectionID = p.SectionID    
inner join WORKPLACE w on
  w.WorkplaceID = p.WorkplaceID '
 IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + '
left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_5 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity 
  left outer join TopPanelsSelected tp1 on
  tp1.Prodmonth = p.Prodmonth and
  tp1.SectionID = sc.SectionID_2 and
  tp1.WorkplaceID = p.WorkplaceID and
  tp1.Activity = p.Activity  '
 END 
  IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + '
left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_5 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity 
  left outer join TopPanelsSelected tp1 on
  tp1.Prodmonth = p.Prodmonth and
  tp1.SectionID = sc.SectionID_2 and
  tp1.WorkplaceID = p.WorkplaceID and
  tp1.Activity = p.Activity'
 END
 set @TheQuery1 = @TheQuery1 + ' 
 
, SYSSET sl 
 where p.CalendarDate >= '''+@TheStartDate+''' and
       p.CalendarDate <= '''+@TheEndDate+''' and p.PlanCode = ''MP'' and p.IsCubics = ''N'' '
IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_2 = '''+@SectionID +'''  and sc.Name_2 = '''+@MOName+''' '
END   
IF (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_3 = '''+@SectionID +''' '
END 
IF (@Section = 2)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_4 = '''+@SectionID +''' '
END 
IF (@Section = 1)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_5 = '''+@SectionID +''' '
END   

set @TheQuery1 = @TheQuery1 + 'group by sc.SectionID_2, sc.Name_2, p.Activity ' 
set @TheQuery1 = @TheQuery1 + ' 
 )) a
group by a.TheActivity, a.TheSection 
with Rollup '


--print @TheQuery
--print @TheQuery2
--print @TheQuery1
exec (@TheQuery+@TheQuery2+@TheQuery1)

end
go

