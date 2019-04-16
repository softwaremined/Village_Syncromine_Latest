update sysset 
set YEARENDMONTH = 6
go
CREATE TABLE [dbo].[SURVEY_FIELDS](
	[Name] varchar(50) not null,
	[Value] varchar(5) null,
) ON [PRIMARY]

GO

insert into SURVEY_FIELDS
select ConfigurationKey,
ConfigurationValue = case when isnull(ConfigurationValue,'') = '' then 'false' 
					else ConfigurationValue  end 
from PAS_Dnk.dbo.SystemSettings

go


ALTER Procedure [dbo].[sp_ProblemsReport_Week] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20)  
as

--set @ToDate = '2017/07/28 00:00:00'
--set @Activity = 'Stoping'
--set @Level = 4

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

ALTER Procedure [dbo].[sp_ProblemsReport_Month] 
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

 ALTER Procedure [dbo].[sp_ProblemsReport_MonthPie] 
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
where 
	isnull(pc.CausedLostBlast, ''N'') = ''N'' and
	b.PlanCode = ''MP'' and 
	b.Activity in '+@Activity +'
group by sc.'+@RunName +', pc.Note,pc.ProblemColorText 
order by sc.'+@RunName +', NumberOFProb desc '

--print (@SQL)
exec (@SQL)
go



ALTER Procedure [dbo].[sp_ProblemsReport_FinYear] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20)   
as

--set @ToDate = '2017/05/28 00:00:00'
--set @Activity = 'Stoping'
--set @Level = 4

Declare @WeekStart DateTime,
@TheYear varchar(4),
@TheMonth varchar(2),
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

declare @TheFinStart datetime
declare @TheFinEnd datetime

declare @FinStart varchar(6)
declare @FinEnd varchar(6)

declare @Month varchar(2)

select @Month = YEARENDMONTH from SYSSET

set @WeekStart = convert(varchar(10), @ToDate-6, 120)
set @TheYear = DATEPART(YYYY,@ToDate)
set @TheMonth = DATEPART(MM,@ToDate)

IF (@Month < @TheMonth)
BEGIN
	IF (@Month < 10)
		set @TheFinEnd = convert(varchar(4), (@TheYear + 1)) + '-0' + convert(varchar(1), @Month) + '-01'
	ELSE
		set @TheFinEnd = convert(varchar(4), (@TheYear + 1)) + convert(varchar(1), @Month) + '-01'
	select @TheFinStart = DATEADD(month, -11, @TheFinEnd)

	set @FinStart = LEFT(CONVERT(varchar, @TheFinStart,112),6)
	set @FinEnd = LEFT(CONVERT(varchar, @TheFinEnd,112),6)
END
ELSE
BEGIN
	IF (@Month < 10)
		set @TheFinEnd = convert(varchar(4), @TheYear) + '-0' + convert(varchar(1), @Month) + '-01'
	ELSE
		set @TheFinEnd = convert(varchar(4), @TheYear) + convert(varchar(1), @Month) + '-01'
	select @TheFinStart = DATEADD(month, -11, @TheFinEnd)

	set @FinStart = LEFT(CONVERT(varchar, @TheFinStart,112),6)
	set @FinEnd = LEFT(CONVERT(varchar, @TheFinEnd,112),6)
END

--select @TheFinStart
--select @TheFinEnd

--select @FinStart
--select @FinEnd
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
		where 
			b.ProdMonth >= '''+@FinStart+''' and
			b.ProdMonth <= '''+@FinEnd+''' and
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
		where 
			b.ProdMonth >= '''+@FinStart+''' and
			b.ProdMonth <= '''+@FinEnd+''' and
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
	where 
		b.ProdMonth >= '''+@FinStart+''' and
		b.ProdMonth <= '''+@FinEnd+''' and
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

 ALTER Procedure [dbo].[sp_ProblemsReport_FinYearPie] 
--Declare
@ToDate DateTime,
@Activity varchar(20),
@Level varchar(20) 
as

--set @ToDate = '2014-06-04'

Declare 
@WeekStart DateTime,
@TheYear varchar(4),
@TheMonth varchar(2),
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

declare @TheFinStart datetime
declare @TheFinEnd datetime

declare @FinStart varchar(6)
declare @FinEnd varchar(6)

declare @Month varchar(2)

select @Month = YEARENDMONTH from SYSSET

set @WeekStart = @ToDate-6
set @TheYear = DATEPART(YYYY,@ToDate)
set @TheMonth = DATEPART(MM,@ToDate)

IF (@Month < @TheMonth)
BEGIN
	IF (@Month < 10)
		set @TheFinEnd = convert(varchar(4), (@TheYear + 1)) + '-0' + convert(varchar(1), @Month) + '-01'
	ELSE
		set @TheFinEnd = convert(varchar(4), (@TheYear + 1)) + convert(varchar(1), @Month) + '-01'
	select @TheFinStart = DATEADD(month, -11, @TheFinEnd)

	set @FinStart = LEFT(CONVERT(varchar, @TheFinStart,112),6)
	set @FinEnd = LEFT(CONVERT(varchar, @TheFinEnd,112),6)
END
ELSE
BEGIN
	IF (@Month < 10)
		set @TheFinEnd = convert(varchar(4), @TheYear) + '-0' + convert(varchar(1), @Month) + '-01'
	ELSE
		set @TheFinEnd = convert(varchar(4), @TheYear) + convert(varchar(1), @Month) + '-01'
	select @TheFinStart = DATEADD(month, -11, @TheFinEnd)

	set @FinStart = LEFT(CONVERT(varchar, @TheFinStart,112),6)
	set @FinEnd = LEFT(CONVERT(varchar, @TheFinEnd,112),6)
END

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
where b.ProdMonth >= '''+@FinStart+''' and
	b.ProdMonth <= '''+@FinEnd+''' and
	isnull(pc.CausedLostBlast, ''N'') = ''N'' and
	b.PlanCode = ''MP'' and 
	b.Activity in '+@Activity +'
group by sc.'+@RunName +', pc.Note,pc.ProblemColorText 
order by sc.'+@RunName +', NumberOFProb desc '

--print (@SQL)
exec (@SQL)

go

ALTER TABLE Businessplan_stoping
alter column[SQMFaultLedge] float   NULL
go 
ALTER TABLE Businessplan_stoping alter column
	[SQMOSLedge] float   NULL
go
ALTER TABLE Businessplan_stoping alter column
	[SQMReeflLedge] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SQMFaultStope] float   NULL 
go

ALTER TABLE Businessplan_stoping alter column
	[SQMOSStope] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SQMReefStope] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SW] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SWFault] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[FLReef] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[FLOS] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[FL] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[CMGT] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[CW] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[Cubics] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsFaultLedge] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsOSLedge] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsCubic] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsReefLedge] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsWasteLedge] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[ContentLedge] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsLedge] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SQMWasteLedge] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SQMLedge] float   NULL
go
ALTER TABLE Businessplan_stoping alter column 
	[TonsFaultStope] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsOSStope] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsReefStope] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsWasteStope] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[ContentStope] float NULL
go
ALTER TABLE Businessplan_stoping alter column 
	[TonsStope] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SQMWasteStope] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SQMStope] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsFault] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsOS] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsReef] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[TonsWaste] float NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[Content] float NULL
go
ALTER TABLE Businessplan_stoping alter column 
	[Tons] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SQMWaste] float   NULL 
go
ALTER TABLE Businessplan_stoping alter column
	[SQM] float   NULL
go

ALTER TABLE Businessplan_Development alter column
[MAdvReef]   float NULL 
go 
ALTER TABLE Businessplan_Development alter column
	[MAdvWaste]  float  NULL  
	go 
ALTER TABLE Businessplan_Development alter column
	[Height]  float  NULL  
	go 
ALTER TABLE Businessplan_Development alter column
	[Width]  float  NULL  
	go 
ALTER TABLE Businessplan_Development alter column
	[Density] float   NULL  
	go 
ALTER TABLE Businessplan_Development alter column
	[CMGT]  float   NULL  
	go 
ALTER TABLE Businessplan_Development alter column
	[Cubics] float  NULL 
	go 
ALTER TABLE Businessplan_Development alter column 
	[TonsReef] float  NULL 
	go 
ALTER TABLE Businessplan_Development alter column 
	[TonsWaste]  float NULL 
	go 
ALTER TABLE Businessplan_Development alter column 
	[Content] float  NULL  
	go 
ALTER TABLE Businessplan_Development alter column
	[EquivalentMetres] float null
	go 
ALTER TABLE Businessplan_Development alter column
	[Tons] float  NULL  
	go 
ALTER TABLE Businessplan_Development alter column
	[MAdv] float NULL  
go

