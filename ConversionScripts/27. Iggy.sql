--USE [PAS_DNK_Syncromine]
--GO
--/****** Object:  StoredProcedure [dbo].[sp_Get_Cycle_Protocol]    Script Date: 2017/09/19 8:21:43 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO

ALTER Procedure [dbo].[sp_Get_Cycle_Protocol]
--Declare
@Prodmonth numeric(7,0),@SectionID_2 varchar(10), @Activity Int

----DECLARE
----@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)
--SET @Prodmonth = 201709
--SET @SectionID_2 = 'REA'
--Set @Activity = 0

--Select * from planmonth where prodmonth = 201601 and workplaceid = '21924'


AS
CREATE TABLE #cycleData
( 
    Prodmonth VARCHAR(40)
    ,WorkplaceID VARCHAR(40)
	,RowType varchar(20)
	,Name varchar(20)
	,SQM numeric(10,3)
	,Metresadvance numeric(10,3)
	,Cubes numeric(10,3)
	,Day1 VARCHAR(40)
	,Day2 VARCHAR(40)
	,Day3 VARCHAR(40)
	,Day4 VARCHAR(40)
	,Day5 VARCHAR(40)
	,Day6 VARCHAR(40)
	,Day7 VARCHAR(40)
	,Day8 VARCHAR(40)
	,Day9 VARCHAR(40)
	,Day10 VARCHAR(40)
	,Day11 VARCHAR(40)
	,Day12 VARCHAR(40)
	,Day13 VARCHAR(40)
	,Day14 VARCHAR(40)
	,Day15 VARCHAR(40)
	,Day16 VARCHAR(40)
	,Day17 VARCHAR(40)
	,Day18 VARCHAR(40)
	,Day19 VARCHAR(40)
	,Day20 VARCHAR(40)
	,Day21 VARCHAR(40)
	,Day22 VARCHAR(40)
	,Day23 VARCHAR(40)
    ,Day24 VARCHAR(40)
	,Day25 VARCHAR(40)
	,Day26 VARCHAR(40)
	,Day27 VARCHAR(40)
	,Day28 VARCHAR(40)
	,Day29 VARCHAR(40)
	,Day30 VARCHAR(40)
	,Day31 VARCHAR(40)
	,Day32 VARCHAR(40)
	,Day33 VARCHAR(40)
	,Day34 VARCHAR(40)
	,Day35 VARCHAR(40)
	,Day36 VARCHAR(40)
	,Day37 VARCHAR(40)
	,Day38 VARCHAR(40)
	,Day39 VARCHAR(40)
	,Day40 VARCHAR(40)
	,Day41 VARCHAR(40)
	,Day42 VARCHAR(40)
	,Day43 VARCHAR(40)
	,Day44 VARCHAR(40)
	,Day45 VARCHAR(40)
	,Day46 VARCHAR(40)
	,Day47 VARCHAR(40)
	,Day48 VARCHAR(40)
	,Day49 VARCHAR(40)
	,Day50 VARCHAR(40)
)

DECLARE
@WorkplaceID VARCHAR(40), 
@CycleValue VARCHAR(40), 
@CycleValueCude VARCHAR(40),
@SectionID VARCHAR(40),
@Calendardate VARCHAR(40), 
@MOCycle VARCHAR(40),
@MOCycleCube VARCHAR(40), 
@WorkingDay VARCHAR(40), 
@theSQLCalDate varchar(max),
@name VARCHAR(40),
@CycleInput VARCHAR(40),
@theInsert varchar(max), 
@dayCount int, 
@theSQLCycleVal varchar(max),
@theSQLInputVal varchar(max),
@theSQLCycleValCube varchar(max),
@theSQLMOCycle varchar(max),
@theSQLMOCycleCube varchar(max),
@theSQLWorkingDay varchar(max),
@theSQLAdvVal varchar(max),
@SQM numeric(10,3),
@Metresadvance numeric(10,3),
@Cubes numeric(10,3),
@ADV numeric(10,3)


DECLARE _Cursor1 CURSOR FOR

  select a.Sectionid, a.Workplaceid from planmonth a inner join Section_Complete b on
  a.prodmonth = b.prodmonth and
  a.sectionid = b.sectionid and
  PlanCode = 'MP'
  where a.Prodmonth = @Prodmonth
  and b.SectionID_2 = @SectionID_2
  and activity in (@Activity)

 OPEN _Cursor1;
 FETCH NEXT FROM _Cursor1
 into  @Sectionid, @Workplaceid;

 WHILE @@FETCH_STATUS = 0
   BEGIN


		SET @theInsert = 'INSERT INTO #cycleData (Prodmonth,WorkplaceID,RowType,Name,SQM,Metresadvance,Cubes'


		SET @dayCount = 1;
		DECLARE db_Items CURSOR FOR 
		SELECT PM.Prodmonth,PM.WorkplaceID
			  ,CASE WHEN  PM.Activity in (0) and PM.SQM > 0 and PLAND.SQM IS NOT NULL  THEN PLAND.SQM 
					WHEN  PM.Activity in (1) and PM.Metresadvance  >0 and PLAND.Metresadvance  IS NOT NULL  THEN PLAND.Metresadvance 
					ELSE 0 END CycleValue
			  ,CASE WHEN  PM.Activity in (0,1) and PM.[CubicMetres] > 0  and PLAND.CubicMetres IS NOT NULL THEN PLAND.CubicMetres ELSE 0 END CycleValueCube
			  ,CT.Calendardate
			  ,CASE WHEN ct.Workingday = 'N' then 'OFF' 
					WHEN PLAND.MOCycle is null then 'BL' else PLAND.MOCycle
			  end MOCycle
			  ,CASE WHEN ct.Workingday = 'N' then 'OFF' 
					WHEN PLAND.MOCycleCube is null then 'BL' else PLAND.MOCycleCube
			  end MOCycleCude
			  ,CASE WHEN ct.Workingday = 'Y' THEN 'WD' ELSE 'NWD' END WorkingDay
			  ,CASE WHEN PM.SQM IS NULL then 0 ELSE PM.SQM END SQM
			  ,CASE WHEN PM.Metresadvance IS NULL then 0 ELSE PM.Metresadvance END Metresadvance
			  ,CASE WHEN PM.[CubicMetres] IS NULL THEN 0 ELSE PM.[CubicMetres] END [CubicMetres],
			  CASE WHEN PLAND.CycleInput IS NULL THEN 'CAL' ELSE CycleInput END CycleInput,
			  Case When isnull(PLAND.FL,0) = 0 then 0 else PLAND.SQM/PLAND.FL end ADV
		  FROM PLANMONTH PM 
			Inner join 
		  [dbo].[SECTION_COMPLETE] SCOM ON
		  PM.SectionID = SCOM.sectionID and
		  PM.Prodmonth = SCOM.PRODMONTH 
		  INNER JOIN
		  [dbo].[SECCAL] SC on
		  SCOM.SectionID_1 = SC.sectionID and
		  SCOM.Prodmonth = SC.PRODMONTH 
		  Inner join 
		   CalType CT on
		 SC.CalendarCode = CT.CalendarCode and
		 SC.BeginDate <= CT.CALENDARDATE and
		 SC.ENDDATE >= CT.CALENDARDATE 
		  LEFT JOIN [dbo].[PLANNING] PLAND on
		  PM.Prodmonth = PLAND.PRODMONTH and
		  PM.[SectionID] = PLAND.[SectionID] and
		  PM.Workplaceid = PLAND.WorkplaceID and
		  PM.Activity = PLAND.Activity and
		  CT.Calendardate = PLAND.CalendarDate
		  WHERE PM.Prodmonth = @Prodmonth
				and PM.SECTIONID = @SectionID
				and PM.WorkplaceID = @WorkplaceID
				and PM.PlanCode = 'MP'
				and ct.Workingday = 'Y'
		
				ORDER BY CT.Calendardate

		OPEN db_Items   
		FETCH NEXT FROM db_Items INTO @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput, @ADV

		--select @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput, @ADV
		SET @theSQLCalDate = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sDate'',''Date'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLInputVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sInput'',''Input'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLAdvVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sAdv'',''Adv'',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))

		IF @SQM > 0
		BEGIN
		SET @theSQLCycleVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValue'',''SQM'','  + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		END
		IF @Metresadvance > 0
		BEGIN
		SET @theSQLCycleVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValue'',''Meters'','  + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		END
		SET @theSQLCycleValCube = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValueCube'',''Cubes'','+ CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLMOCycle = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sCycleCode'','''',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLMOCycleCube = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sCycleCodeCube'','''','+ CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		WHILE @@FETCH_STATUS = 0   
		BEGIN
		SET @theInsert = @theInsert + ',Day' + Cast(@dayCount as varchar(10))
		SET @theSQLCalDate = @theSQLCalDate  + ',''' + Cast([dbo].[FormatDateTime](@Calendardate,'YYYY-MM-DD') as varchar(MAX)) + ''''
		SET @theSQLInputVal = @theSQLInputVal  + ',''' +@CycleInput + ''''
		SET @theSQLAdvVal = @theSQLAdvVal  + ',''' +Cast(@ADV as varchar(10)) + ''''
		SET @theSQLCycleVal = @theSQLCycleVal  + ',''' + Cast(@CycleValue as varchar(MAX)) + ''''
		SET @theSQLCycleValCube = @theSQLCycleValCube  + ',''' + Cast(@CycleValueCude as varchar(MAX)) + ''''
		SET @theSQLMOCycle = @theSQLMOCycle  + ',''' + Cast(@MOCycle as varchar(MAX)) + ''''
		SET @theSQLMOCycleCube = @theSQLMOCycleCube  + ',''' + Cast(@MOCycleCube as varchar(MAX)) + ''''
		--SET @theSQL = @theSQL + Cast(@Prodmonth as varchar(MAX))  + ',' + Cast(@WorkplaceID as varchar(MAX)) + ',' + Cast(@CycleValue as varchar(MAX))+ ',' + Cast(@Calendardate as varchar(MAX)) + ',' + Cast(@MOCycle as varchar(MAX)) + ',' + Cast(@WorkingDay as varchar(MAX))
		SET @dayCount = @dayCount + 1
		FETCH NEXT FROM db_Items INTO  @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput, @ADV 
		END

		CLOSE db_Items;
        DEALLOCATE db_Items;

		--SET @theSQLCalDate = @theSQLCalDate + ')'
		--SET @theSQLInputVal = @theSQLInputVal + ')'
		SET @theSQLCycleVal = @theSQLCycleVal + ')'
		SET @theSQLAdvVal = @theSQLAdvVal + ')'
		SET @theSQLMOCycle = @theSQLMOCycle + ')'
		SET @theSQLMOCycleCube = @theSQLMOCycleCube + ')'
		SET @theSQLCycleValCube = @theSQLCycleValCube + ')'
		SET @theInsert = @theInsert + ') VALUES '
		--select (@theInsert + @theSQLCalDate)
		--select (@theInsert + @theSQLMOCycle)
		--select (@theInsert + @theSQLCycleVal)
		IF @theSQLCalDate is not null
		begin
		--exec (@theInsert + @theSQLCalDate)
		--exec (@theInsert + @theSQLInputVal)
		IF @SQM + @Metresadvance > 0
		BEGIN
		exec (@theInsert + @theSQLMOCycle)
		exec (@theInsert + @theSQLCycleVal)
		--select (@theInsert + @theSQLAdvVal)
		exec (@theInsert + @theSQLAdvVal)

		END
		IF @Cubes > 0
		BEGIN
		exec (@theInsert + @theSQLMOCycleCube)
		exec (@theInsert + @theSQLCycleValCube)
		END
		end
		

		 FETCH NEXT FROM _Cursor1
       into  @Sectionid, @Workplaceid;

END

CLOSE _Cursor1;
DEALLOCATE _Cursor1;

SELECT 
    Prodmonth
    ,WorkplaceID
	,RowType
	,Name
	,SQM
	,Metresadvance
	,Cubes
	,Isnull(Day1,0) Day1
	,Isnull(Day2,0) Day2
	,Isnull(Day3,0) Day3
	,Isnull(Day4,0) Day4
	,Isnull(Day5,0) Day5
	,Isnull(Day6,0) Day6
	,Isnull(Day7,0) Day7
	,Isnull(Day8,0) Day8
	,Isnull(Day9,0) Day9
	,Isnull(Day10,0) Day10
	,Isnull(Day11,0) Day11
	,Isnull(Day12,0) Day12
	,Isnull(Day13,0) Day13
	,Isnull(Day14,0) Day14
	,Isnull(Day15,0) Day15
	,Isnull(Day16,0) Day16
	,Isnull(Day17,0) Day17
	,Isnull(Day18,0) Day18
	,Isnull(Day19,0) Day19
	,Isnull(Day20,0) Day20
	,Isnull(Day21,0) Day21
	,Isnull(Day22,0) Day22 
	,Isnull(Day23,0) Day23
    ,Isnull(Day24,0) Day24
	,Isnull(Day25,0) Day25
	,Isnull(Day26,0) Day26
	,Isnull(Day27,0) Day27
	,Isnull(Day28,0) Day28
	,Isnull(Day29,0) Day29
	,Isnull(Day30,0) Day30
	,Isnull(Day31,0) Day31
	,Isnull(Day32,0) Day32
	,Isnull(Day33,0) Day33
	,Isnull(Day34,0) Day34
	,Isnull(Day35,0) Day35
	,Isnull(Day36,0) Day36
	,Isnull(Day37,0) Day37
	,Isnull(Day38,0) Day38
	,Isnull(Day39,0) Day39
	,Isnull(Day40,0) Day40
	,Isnull(Day41,0) Day41
	,Isnull(Day42,0) Day42
	,Isnull(Day43,0) Day43
	,Isnull(Day44,0) Day44
	,Isnull(Day45,0) Day45
	,Isnull(Day46,0) Day46
	,Isnull(Day47,0) Day47
	,Isnull(Day48,0) Day48
	,Isnull(Day49,0) Day49
	,Isnull(Day50,0) Day50
 FROM #cycleData

DROP TABLE #cycleData

GO