
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

Alter Procedure dbo.sp_Latest_Geology_Report_Number_DEV   --in
--Declare                     --out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20)
as                      --in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'             --out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 66) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,

       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66001,SC.Sectionid_2)[ Approaching/ Exiting a dyke],
    dbo.GetPlanProdFieldName(66001) [ Approaching/ Exiting a dyke Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66002,SC.Sectionid_2)[Dyke ˂ 35˚],
    dbo.GetPlanProdFieldName(66002) [Dyke ˂ 35˚ Name],  
     dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66003,SC.Sectionid_2)[Approaching Major Fault  ˃ 4m ],
    dbo.GetPlanProdFieldName(66003) [Approaching Major Fault  ˃ 4m  Name],  
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66004,SC.Sectionid_2)[End Lithology],
    dbo.GetPlanProdFieldName(66004) [End Lithology Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66005,SC.Sectionid_2)[Quartz veins/ Joints / Minor faults],
    dbo.GetPlanProdFieldName(66005) [Quartz veins/ Joints / Minor faults Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66006,SC.Sectionid_2)[Expected downthrow fault ˃ 1m],
    dbo.GetPlanProdFieldName(66006) [Expected downthrow fault ˃ 1m Name], 
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66007,SC.Sectionid_2)[Argillite exposure ],
    dbo.GetPlanProdFieldName(66007) [Argillite exposure  Name],  
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66008,SC.Sectionid_2)[FOG Gravity],
    dbo.GetPlanProdFieldName(66008) [FOG Gravity Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66009,SC.Sectionid_2)[Dip of Strata],
    dbo.GetPlanProdFieldName(66009) [RIH Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66010,SC.Sectionid_2)[Expected Reef roll ( Y/N )],
    dbo.GetPlanProdFieldName(66010) [Expected Reef roll ( Y/N ) Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66011,SC.Sectionid_2)[FW Width],
    dbo.GetPlanProdFieldName(66011) [FW Width Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66012,SC.Sectionid_2)[Reef Detector no.],
    dbo.GetPlanProdFieldName(66012) [Reef Detector no.Name],
    dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,66013,SC.Sectionid_2)[In Cover],
    dbo.GetPlanProdFieldName(66013) [In Cover Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
      pp.Prodmonth = SC.PRODMONTH AND
      pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
      PPDA.PRODMONTH = @PRODMONTH AND 
      sc.Sectionid_2 = PPDA.SECTIONID AND
      pp.Workplaceid = PPDA.WORKPLACEID AND
      pp.Activity = PPDA.ActivityType AND
      PPDA.TemplateID = 66
INNER JOIN Workplace W on
      pp.Workplaceid = w.Workplaceid
      WHERE 
      PP.Prodmonth = @PRODMONTH AND
      sc.Sectionid_2 = @SectionID_2 AND
      pp.Activity = 1 AND 
      PP.PlanCode ='MP'

GO

ALTER Procedure [dbo].[sp_Get_Cycle]
--Declare
@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)

--DECLARE
--@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)
--SET @theProdmonth = 201710
--SET @theWorkplaceID = 'RE008078'
--SET @theSectionID = 'REACHCA'


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
@Prodmonth VARCHAR(40),
@WorkplaceID VARCHAR(40), 
@CycleValue VARCHAR(40), 
@CycleValueCude VARCHAR(40),
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
@SQM numeric(10,3),
@Metresadvance numeric(10,3),
@Cubes numeric(10,3)



SET @theInsert = 'INSERT INTO #cycleData (Prodmonth,WorkplaceID,RowType,Name,SQM,Metresadvance,Cubes'


SET @dayCount = 1;
DECLARE db_Items CURSOR FOR 
SELECT PM.Prodmonth,PM.WorkplaceID
      ,CASE WHEN  PM.Activity in (0) and PM.SQM > 0 and PLAND.SQM IS NOT NULL  THEN PLAND.SQM 
	        WHEN  PM.Activity in (1) and PM.Metresadvance > 0 and PLAND.Metresadvance  IS NOT NULL  THEN PLAND.Metresadvance 
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
	  ,CASE WHEN PM.Activity in (0) then isnull(PM.SQM,0) ELSE 0 END SQM
	  ,CASE WHEN PM.Activity in (1) then isnull(PM.Metresadvance,0) ELSE 0 END Metresadvance
	  ,CASE WHEN PM.[CubicMetres] IS NULL THEN 0 ELSE PM.[CubicMetres] END [CubicMetres],
	  CASE 
	  WHEN s.RUNDATE > PLAND.Calendardate then 'MAN'
	  WHEN PLAND.CycleInput IS NULL THEN 'CAL' 
	  ELSE CycleInput END CycleInput
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
  CT.Calendardate = PLAND.CalendarDate, SYSSET S
  WHERE PM.Prodmonth = @theProdmonth
        and PM.SECTIONID = @theSectionID
		and PM.WorkplaceID = @theWorkplaceID
		and PM.PlanCode = 'MP'
		
		ORDER BY CT.Calendardate

OPEN db_Items   
FETCH NEXT FROM db_Items INTO @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput


SET @theSQLCalDate = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sDate'',''Date'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
SET @theSQLInputVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sInput'',''Input'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))

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
SET @theSQLCycleVal = @theSQLCycleVal  + ',''' + Cast(@CycleValue as varchar(MAX)) + ''''
SET @theSQLCycleValCube = @theSQLCycleValCube  + ',''' + Cast(@CycleValueCude as varchar(MAX)) + ''''
SET @theSQLMOCycle = @theSQLMOCycle  + ',''' + Cast(@MOCycle as varchar(MAX)) + ''''
SET @theSQLMOCycleCube = @theSQLMOCycleCube  + ',''' + Cast(@MOCycleCube as varchar(MAX)) + ''''
--SET @theSQL = @theSQL + Cast(@Prodmonth as varchar(MAX))  + ',' + Cast(@WorkplaceID as varchar(MAX)) + ',' + Cast(@CycleValue as varchar(MAX))+ ',' + Cast(@Calendardate as varchar(MAX)) + ',' + Cast(@MOCycle as varchar(MAX)) + ',' + Cast(@WorkingDay as varchar(MAX))
SET @dayCount = @dayCount + 1
FETCH NEXT FROM db_Items INTO  @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput 
END

SET @theSQLCalDate = @theSQLCalDate + ')'
SET @theSQLInputVal = @theSQLInputVal + ')'
SET @theSQLCycleVal = @theSQLCycleVal + ')'
SET @theSQLMOCycle = @theSQLMOCycle + ')'
SET @theSQLMOCycleCube = @theSQLMOCycleCube + ')'
SET @theSQLCycleValCube = @theSQLCycleValCube + ')'
SET @theInsert = @theInsert + ') VALUES '
--select (@theInsert + @theSQLCalDate)
--select (@theInsert + @theSQLMOCycle)
--select (@theInsert + @theSQLCycleVal)
IF @theSQLCalDate is not null
begin
exec (@theInsert + @theSQLCalDate)
exec (@theInsert + @theSQLInputVal)
IF @SQM + @Metresadvance > 0
BEGIN
exec (@theInsert + @theSQLMOCycle)
exec (@theInsert + @theSQLCycleVal)
END
IF @Cubes > 0
BEGIN
exec (@theInsert + @theSQLMOCycleCube)
exec (@theInsert + @theSQLCycleValCube)
END
end

SELECT * FROM #cycleData

DROP TABLE #cycleData

CLOSE db_Items   
DEALLOCATE db_Items



GO

ALTER procedure [dbo].[sp_Revised_Audit_Summary]
--Declare 
@Prodmonth numeric(7),
@ToProdmonth numeric(7),
@Section Varchar(30),
@between varchar(5)
as

--set @Prodmonth = 201606
--set @ToProdmonth = 201608
--set @Section = '1999'
--set @between='0'

Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)
Declare @SyncroDB VarChar(50),@bonus varchar(50)
select @TheLevel = HIERARCHICALID from section 
where PRODMONTH = @Prodmonth 
and
sectionid = @Section

--select @TheLevel


  If @TheLevel = 1 
    set @GroupLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @GroupLevel = 'NAME_1'


  If @TheLevel = 1 
    set @SectionLevel = 'SECTIONID_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'SECTIONID_4'  
    
   If @TheLevel = 3 
    set @SectionLevel = 'SECTIONID_3'  
     
   If @TheLevel = 4 
    set @SectionLevel = 'SECTIONID_2'  
    
   If @TheLevel = 5 
    set @SectionLevel = 'SECTIONID_1'

   If @TheLevel = 6 
    set @SectionLevel = 'SECTIONID'
Declare 
        @MINDATE varchar(50),
		@MAXDATE varchar(50),
		@SQL4 Varchar(MAX),
		@SQL5 Varchar(MAX)


 SET @SQL4='SELECT MIN(STARTDATE) FROM PLANMONTH PP INNER JOIN SECTION_COMPLETE SC on 
            			PP.SectionID = sc.SECTIONID and 
            			PP.ProdMonth = SC.PRODMONTH 
						WHERE 
						--PP.PRODMONTH=' + Convert(Varchar(7),@Prodmonth) +' 

						PP.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
						and PP.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'


						AND SC.'+@SectionLevel+'=''' + @Section + ''' 
						AND PP.PLANCODE=''MP'''

	CREATE TABLE #TheMinDate(TheMinDate VARCHAR(50))
	INSERT #TheMinDate EXEC(@SQL4)
	  
	SET @MINDATE = (SELECT TheMinDate FROM  #TheMinDate)
	--select @MINDATE
	DROP TABLE #TheMinDate
SET @SQL5='SELECT MAX(ENDDATE) FROM PLANMONTH PP INNER JOIN SECTION_COMPLETE SC on 
            			PP.SectionID = sc.SECTIONID and 
            			PP.ProdMonth = SC.PRODMONTH 
						WHERE 
						--PP.PRODMONTH=' + Convert(Varchar(7),@Prodmonth) +' 

						PP.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
						and PP.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'

						AND  SC.'+@SectionLevel+'=''' + @Section + '''  
						AND PP.PLANCODE=''MP'''
	CREATE TABLE #TheMaxDate(TheMinDate VARCHAR(50))
	INSERT #TheMaxDate EXEC(@SQL5)
	  
	SET @MAXDATE = (SELECT TheMinDate FROM  #TheMaxDate)
	--select @MAXDATE
	DROP TABLE #TheMaxDate

if @between='0'
begin
 Set @SQL1 = 'select distinct SC.NAME_2,sc.'+@GroupLevel+' SectName,'''+ @MAXDATE+''' maxdate,'''+ @MINDATE+''' Mindate, sd.ChangeType,
	SUM(case when	sd.ChangeType=''New Workplace'' THEN 1 else 0 END) [New Workplace], 
	SUM(case when	sd.ChangeType=''Call Changes'' THEN 1 else 0 end) [Call Changes],--,ppcr.changeid,ppcr.workplaceid
	SUM(				case when	sd.ChangeType=''Move Planning'' THEN 1 else 0 end) [Move Planning],
	SUM(				case when	sd.ChangeType=''Stop Workplace'' THEN 1 else 0 end) [Stop Workplace],
	SUM(				case when	sd.ChangeType=''Crew Miner Changes'' THEN 1 else 0 end) [Crew Miner Changes],
	SUM(				case when	sd.ChangeType=''Start Workplace'' THEN 1 else 0 end) [Start Workplace],
	SUM(				case when	sd.ChangeType=''Mining Method Change'' THEN 1 else 0 end) [Mining Method Change],
	SUM(				case when	sd.ChangeType=''Drill Rig Change'' THEN 1 else 0 end) [Drill Rig Change],
	SUM(				case when	sd.ChangeType=''Delete Planning'' THEN 1 else 0 end) [Delete Planning]
   from PREPLANNING_CHANGEREQUEST PPCR 
   left join PLANMONTH pp on
  --pp.sectionid=PPCR.sectionid AND
  PP.PRODMONTH=PPCR.PRODMONTH and
  --pp.sectionid=ppcr.sectionid and
  pp.workplaceid=ppcr.workplaceid 
  INNER JOIN SECTION_COMPLETE SC on 
	PPCR.SectionID = sc.SECTIONID and 
	PPCR.ProdMonth = SC.PRODMONTH 
	INNER Join Seccal sec on
	sc.prodmonth = sec.Prodmonth AND
	sc.SectionID_1 = sec.SectionID
			LEFT JOIN (select distinct ChangeRequestID,ChangeType from StatusDetails) sd ON
			SD.ChangeRequestID=PPCR.ChangeRequestID 
			inner join PREPLANNING_CHANGEREQUEST_APPROVAL App on
			PPCR.ChangeRequestID = APP.ChangeRequestID
			WHERE  SC.'+@SectionLevel+'=''' + @Section + ''' 
			--and ppcr.prodmonth=' + Convert(Varchar(7),@Prodmonth) +' 

			and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
			and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'

			AND PP.PLANCODE=''MP'' 
			AND (Case When pp.Prodmonth = sec.Prodmonth and sec.enddate < app.requestDate then 1 else 0 end) = 0
			GROUP BY Sc.NAME_2,sc.'+@GroupLevel+', sd.ChangeType
--union 
--select distinct S.NAME,sc.'+@GroupLevel+' SectName,'''+ @MAXDATE+''' maxdate,'''+ @MINDATE+''' Mindate,
-- COUNT(case when	sd.ChangeType=''New Workplace'' THEN sd.ChangeType END) [New Workplace], 
--	COUNT(case when	sd.ChangeType=''Call Changes'' THEN sd.ChangeType end) [Call Changes],--,ppcr.changeid,ppcr.workplaceid
--	COUNT(				case when	sd.ChangeType=''Move Planning'' THEN sd.ChangeType end) [Move Planning],
--	COUNT(				case when	sd.ChangeType=''Stop Workplace'' THEN sd.ChangeType end) [Stop Workplace],
--	COUNT(				case when	sd.ChangeType=''Crew Miner Changes'' THEN sd.ChangeType end) [Crew Miner Changes],
--	COUNT(				case when	sd.ChangeType=''Start Workplace'' THEN sd.ChangeType end) [Start Workplace],
--	COUNT(				case when	sd.ChangeType=''Mining Method Change'' THEN sd.ChangeType end) [Mining Method Change],
--	COUNT(				case when	sd.ChangeType=''Drill Rig Change'' THEN sd.ChangeType end) [Drill Rig Change],
--	COUNT(				case when	sd.ChangeType=''Delete Planning'' THEN sd.ChangeType end) [Delete Planning]
--   from PREPLANNING_CHANGEREQUEST PPCR left join PLANMONTH pp on
--  --pp.sectionid=PPCR.sectionid AND
--  PP.PRODMONTH=PPCR.PRODMONTH and
--  pp.sectionid=ppcr.sectionid and
--  pp.workplaceid=ppcr.workplaceid INNER JOIN SECTION_COMPLETE SC on 
--        PPCR.SectionID = sc.SECTIONID and 
--        PPCR.ProdMonth = SC.PRODMONTH 
--		INNER Join Seccal sec on
--        sc.prodmonth = sec.Prodmonth AND
--        sc.SectionID_1 = sec.SectionID
--		LEFT JOIN (select distinct ChangeRequestID,ChangeType from StatusDetails) sd ON
--		SD.ChangeRequestID=PPCR.ChangeRequestID WHERE  SC.'+@SectionLevel+'=''' + @Section + ''' 
--		--and ppcr.prodmonth=' + Convert(Varchar(7),@Prodmonth) +' 

--		and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
--		and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'		
		
--		AND PP.PLANCODE=''MP'' 
--		AND pp.startDATE  BETWEEN '''+ @MINDATE+''' AND  '''+ @MAXDATE+''' and pp.enddate  is null
--		GROUP BY S.NAME,sc.'+@GroupLevel+',sd.ChangeType						'
 exec (@SQL1)
-- SELECT @SQL1
 end

  declare @SQL3 VARCHAR(MAX)
  if @between='1'
  begin
  Set @SQL3 = 'select distinct SC.NAME_2,sc.'+@GroupLevel+' SectName,'''+ @MAXDATE+''' maxdate,'''+ @MINDATE+''' Mindate, sd.ChangeType,
	SUM(case when	sd.ChangeType=''New Workplace'' THEN 1 else 0 END) [New Workplace], 
	SUM(case when	sd.ChangeType=''Call Changes'' THEN 1 else 0 end) [Call Changes],--,ppcr.changeid,ppcr.workplaceid
	SUM(				case when	sd.ChangeType=''Move Planning'' THEN 1 else 0 end) [Move Planning],
	SUM(				case when	sd.ChangeType=''Stop Workplace'' THEN 1 else 0 end) [Stop Workplace],
	SUM(				case when	sd.ChangeType=''Crew Miner Changes'' THEN 1 else 0 end) [Crew Miner Changes],
	SUM(				case when	sd.ChangeType=''Start Workplace'' THEN 1 else 0 end) [Start Workplace],
	SUM(				case when	sd.ChangeType=''Mining Method Change'' THEN 1 else 0 end) [Mining Method Change],
	SUM(				case when	sd.ChangeType=''Drill Rig Change'' THEN 1 else 0 end) [Drill Rig Change],
	SUM(				case when	sd.ChangeType=''Delete Planning'' THEN 1 else 0 end) [Delete Planning]
   from PREPLANNING_CHANGEREQUEST PPCR 
   left join PLANMONTH pp on
  --pp.sectionid=PPCR.sectionid AND
  PP.PRODMONTH=PPCR.PRODMONTH and
  --pp.sectionid=ppcr.sectionid and
  pp.workplaceid=ppcr.workplaceid 
  INNER JOIN SECTION_COMPLETE SC on 
	PPCR.SectionID = sc.SECTIONID and 
	PPCR.ProdMonth = SC.PRODMONTH 
	INNER Join Seccal sec on
	sc.prodmonth = sec.Prodmonth AND
	sc.SectionID_1 = sec.SectionID
			LEFT JOIN (select distinct ChangeRequestID,ChangeType from StatusDetails) sd ON
			SD.ChangeRequestID=PPCR.ChangeRequestID 
			inner join PREPLANNING_CHANGEREQUEST_APPROVAL App on
			PPCR.ChangeRequestID = APP.ChangeRequestID
			WHERE  SC.'+@SectionLevel+'=''' + @Section + ''' 
			--and ppcr.prodmonth=' + Convert(Varchar(7),@Prodmonth) +' 

			and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
			and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'

			AND PP.PLANCODE=''MP'' 
			AND (Case When pp.Prodmonth = sec.Prodmonth and sec.enddate < app.requestDate then 1 else 0 end) = 1
			GROUP BY Sc.NAME_2,sc.'+@GroupLevel+', sd.ChangeType
--union 
--select distinct S.NAME,sc.'+@GroupLevel+' SectName,'''+ @MAXDATE+''' maxdate,'''+ @MINDATE+''' Mindate,
-- COUNT(case when	sd.ChangeType=''New Workplace'' THEN sd.ChangeType END) [New Workplace], 
--	COUNT(case when	sd.ChangeType=''Call Changes'' THEN sd.ChangeType end) [Call Changes],--,ppcr.changeid,ppcr.workplaceid
--	COUNT(				case when	sd.ChangeType=''Move Planning'' THEN sd.ChangeType end) [Move Planning],
--	COUNT(				case when	sd.ChangeType=''Stop Workplace'' THEN sd.ChangeType end) [Stop Workplace],
--	COUNT(				case when	sd.ChangeType=''Crew Miner Changes'' THEN sd.ChangeType end) [Crew Miner Changes],
--	COUNT(				case when	sd.ChangeType=''Start Workplace'' THEN sd.ChangeType end) [Start Workplace],
--	COUNT(				case when	sd.ChangeType=''Mining Method Change'' THEN sd.ChangeType end) [Mining Method Change],
--	COUNT(				case when	sd.ChangeType=''Drill Rig Change'' THEN sd.ChangeType end) [Drill Rig Change],
--	COUNT(				case when	sd.ChangeType=''Delete Planning'' THEN sd.ChangeType end) [Delete Planning]
--   from PREPLANNING_CHANGEREQUEST PPCR left join PLANMONTH pp on
--  --pp.sectionid=PPCR.sectionid AND
--  PP.PRODMONTH=PPCR.PRODMONTH and
--  pp.sectionid=ppcr.sectionid and
--  pp.workplaceid=ppcr.workplaceid INNER JOIN SECTION_COMPLETE SC on 
--        PPCR.SectionID = sc.SECTIONID and 
--        PPCR.ProdMonth = SC.PRODMONTH 
--		INNER Join Seccal sec on
--        sc.prodmonth = sec.Prodmonth AND
--        sc.SectionID_1 = sec.SectionID
--		LEFT JOIN (select distinct ChangeRequestID,ChangeType from StatusDetails) sd ON
--		SD.ChangeRequestID=PPCR.ChangeRequestID WHERE  SC.'+@SectionLevel+'=''' + @Section + ''' 
--		--and ppcr.prodmonth=' + Convert(Varchar(7),@Prodmonth) +' 

--		and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
--		and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'		
		
--		AND PP.PLANCODE=''MP'' 
--		AND pp.startDATE  BETWEEN '''+ @MINDATE+''' AND  '''+ @MAXDATE+''' and pp.enddate  is null
--		GROUP BY S.NAME,sc.'+@GroupLevel+',sd.ChangeType	'
 exec (@SQL3)
 --SELECT @SQL3
 end

 
GO

ALTER procedure [dbo].[sp_RevisedPlanningAudit_StatusDetail]
--Declare
	@Prodmonth numeric(7),
	@ToProdmonth numeric(7)
as


--set @Prodmonth = 201710
--set @ToProdmonth = 201710


Declare 
        @SQL1 Varchar(MAX)
 Set @SQL1 = 'SELECT Distinct max(ppcra.ApprovedDeclinedDate) ApprovedDeclinedDate, ppcra.ApprovedDeclinedByUser, RU.Name + '' '' + 
 RU.LastName FullName, sd.ChangeRequestID, Dep.Description Department, sd.ProdMonth, sd.[Workplace Name], ppcra.Comments, Status 
 from StatusDetails sd 
 left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on 
 ppcra.ChangeRequestID = sd.ChangeRequestID 
 and sd.Department = ppcra.Department  
 inner join [tblDepartments] Dep on 	
 sd.Department = dep.[DepartmentID] 
 INNER JOIN 
 (SELECT * FROM tblUsers WHERE  UserID <> '''') RU On
 PPCRA.ApprovedDeclinedByUser collate SQL_Latin1_General_CP1_CI_AS = RU.UserID collate SQL_Latin1_General_CP1_CI_AS
 --and sd.prodmonth = ' + Convert(Varchar(7),@Prodmonth) +'  

 and sd.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
 and sd.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'	

 group by ppcra.ApprovedDeclinedByUser, RU.Name, RU.LastName, sd.ChangeRequestID, Dep.Description, sd.ProdMonth,sd.[Workplace Name], ppcra.Comments, Status
 union
 SELECT Distinct ppcra.ApprovedDeclinedDate, ppcra.ApprovedDeclinedByUser, RU.Name + '' '' + RU.LastName FullName, sd.ChangeRequestID, Dep.Description Department, 
 sd.ProdMonth, sd.[Workplace Name], ppcra.Comments, Status 
 from StatusDetails sd 
 left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on 
 ppcra.ChangeRequestID = sd.ChangeRequestID 
 and sd.Department = ppcra.Department  
 inner join 
 [tblDepartments] Dep on 
 sd.Department = dep.[DepartmentID] 
 INNER JOIN 
 (SELECT * FROM tblUsers WHERE  UserID <> '''') RU On
 PPCRA.User1 collate SQL_Latin1_General_CP1_CI_AS = RU.UserID collate SQL_Latin1_General_CP1_CI_AS
 and status = 2 
 --and sd.prodmonth = ' + Convert(Varchar(7),@Prodmonth) + ' 
 and sd.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
 and sd.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'	
 group by 
 ppcra.ApprovedDeclinedDate, ppcra.ApprovedDeclinedByUser, RU.Name, RU.LastName, sd.ChangeRequestID, Dep.Description, 
 sd.ProdMonth, sd.[Workplace Name], ppcra.Comments, Status'
 exec (@SQL1)

GO

ALTER procedure [dbo].[sp_RevisedPlanningAudit]
--Declare 
@Prodmonth numeric(7),
@ToProdmonth numeric(7),
@Section Varchar(30),
@RevisedType varchar(50)
as

--set @Prodmonth = 201507
--set @Section = '2818460'
--set @RevisedType='New Workplace'

--set @Prodmonth = 201710
--set @ToProdmonth = 201710
--set @Section = 'GM'
--set @RevisedType='All'


Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)
 Declare @SyncroDB VarChar(50),@bonus varchar(50)
select @TheLevel = HIERARCHICALID from section where PRODMONTH = @ToProdmonth and
sectionid = @Section

--select @TheLevel


  If @TheLevel = 1 
    set @GroupLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @GroupLevel = 'NAME_1'


  If @TheLevel = 1 
    set @SectionLevel = 'SECTIONID_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'SECTIONID_4'  
    
   If @TheLevel = 3 
    set @SectionLevel = 'SECTIONID_3'  
     
   If @TheLevel = 4 
    set @SectionLevel = 'SECTIONID_2'  
    
   If @TheLevel = 5 
    set @SectionLevel = 'SECTIONID_1'

   If @TheLevel = 6 
    set @SectionLevel = 'SECTIONID'
    
 if @RevisedType<>'All'
 begin
  Set @SQL1 = 'SELECT DISTINCT sd.ChangeRequestID, sc.'+@GroupLevel+',sc.Name,
                            sd.WorkplaceID, RU.Name + '' '' + RU.LastName FullName,
            			sd.ChangeType, 
            			sd.ProdMonth, 
            			sd.[Workplace Name],
            			sd.Name,sd.Comments,ChangeID , PPCR.DAYCREW,PPCR.NIGHTCREW,
						PPCR.AFTERNOONCREW,PPCR.ROVINGCREW,PPCR.REEFSQM,PPCR.WASTESQM,
						PPCR.METERS,PPCR.METERSWASTE,PPCR.CUBICMETERS,PPCR.STOPDATE,PPCR.STARTDATE,PPCR.OLDWORKPLACEID,BPD.Description,LBPD.Description LockMethod,PPCR.FL,
            			CONVERT(varchar(50), ppcra.RequestDate,103) RequestDate, PPCR.DrillRig, p.DrillRig LockDrillRig
            			,max(Status) Status,s.name Section from StatusDetails sd  
            			left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on  
            			ppcra.ChangeRequestID =sd.ChangeRequestID   
            			INNER JOIN 				 
            			[dbo].[PREPLANNING_CHANGEREQUEST] PPCR on 
            			ppcra.ChangeRequestID = PPCR.ChangeRequestID 
            			INNER JOIN SECTION_COMPLETE SC on 
            			PPCR.SectionID = sc.SECTIONID and 
            			PPCR.ProdMonth = SC.PRODMONTH 
						INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') RU On
						PPCR.RequestBy collate SQL_Latin1_General_CP1_CI_AS = RU.UserID collate SQL_Latin1_General_CP1_CI_AS inner join section s on
						s.sectionid=ppcr.sectionid_2 and
						s.prodmonth=ppcr.prodmonth left join Code_Methods BPD on
						PPCR.MiningMethod=BPD.TargetID 
						left join Planmonth P on
						PPCR.ProdMonth = p.PRODMONTH and
						PPCR.SectionID = p.SECTIONID and 
						PPCR.WorkplaceID = p.WorkplaceID and 
						p.PLancode = ''LP''
						left join [Bonus_PoolDefaults] LBPD on
						p.TargetID =LBPD.TargetID  
                        	WHERE  CHANGETYPE=''' + @RevisedType + ''' AND SC.'+@SectionLevel+'=''' + @Section + ''' 

						and	ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
						and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'	
						--AND SC.'+@SectionLevel+' in (SELECT SectionID from SECTION WHERE SectionID IN (SELECT SectionID FROM USERS_SECTION where LinkType = ''P'')) --or
            			 
            			group by sd.ChangeRequestID,sc.'+@GroupLevel+',sc.Name,sd.ProdMonth,RU.Name,RU.LastName,sd.[Workplace Name],sd.Name,sd.Comments,sd.WorkplaceID,sd.ChangeType,ppcra.RequestDate,ChangeID,
						PPCR.DAYCREW,PPCR.NIGHTCREW,PPCR.AFTERNOONCREW,PPCR.ROVINGCREW,PPCR.REEFSQM,PPCR.WASTESQM,PPCR.METERS,PPCR.METERSWASTE,PPCR.CUBICMETERS,PPCR.STOPDATE,PPCR.STARTDATE,PPCR.OLDWORKPLACEID,BPD.Description,LBPD.Description,PPCR.FL,s.name, PPCR.DrillRig, p.DrillRig
            			order by sd.ProdMonth desc, sd.ChangeRequestID desc'
  Exec(@SQL1)
  --select @SQL1
  end
  else
  begin
   Set @SQL1 = 'SELECT DISTINCT sd.ChangeRequestID, sc.'+@GroupLevel+',sc.Name,
                            sd.WorkplaceID, RU.Name + '' '' + RU.LastName FullName,
            			sd.ChangeType, 
            			sd.ProdMonth, 
            			sd.[Workplace Name],
            			sd.Name,sd.Comments,ChangeID , PPCR.DAYCREW,PPCR.NIGHTCREW,
						PPCR.AFTERNOONCREW,PPCR.ROVINGCREW,PPCR.REEFSQM,PPCR.WASTESQM,
						PPCR.METERS,PPCR.METERSWASTE,PPCR.CUBICMETERS,PPCR.STOPDATE,PPCR.STARTDATE,PPCR.OLDWORKPLACEID,BPD.Description,LBPD.Description LockMethod,PPCR.FL,
            			CONVERT(varchar(50), ppcra.RequestDate,103) RequestDate, PPCR.DrillRig, p.DrillRig LockDrillRig  
            			,max(Status) Status,s.name Section from StatusDetails sd  
            			left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on  
            			ppcra.ChangeRequestID =sd.ChangeRequestID   
            			INNER JOIN 				 
            			[dbo].[PREPLANNING_CHANGEREQUEST] PPCR on 
            			ppcra.ChangeRequestID = PPCR.ChangeRequestID 
            			INNER JOIN SECTION_COMPLETE SC on 
            			PPCR.SectionID = sc.SECTIONID and 
            			PPCR.ProdMonth = SC.PRODMONTH 
						INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') RU On
						PPCR.RequestBy collate SQL_Latin1_General_CP1_CI_AS = RU.UserID collate SQL_Latin1_General_CP1_CI_AS inner join section s on
						s.sectionid=ppcr.sectionid_2 and
						s.prodmonth=ppcr.prodmonth left join Code_Methods BPD on
						PPCR.MiningMethod=BPD.TargetID
						left join Planmonth P on
						PPCR.ProdMonth = p.PRODMONTH and
						PPCR.SectionID = p.SECTIONID and 
						PPCR.WorkplaceID = p.WorkplaceID and 
						p.PLancode = ''LP''
						left join Code_Methods LBPD on
						p.TargetID =LBPD.TargetID  
                        	WHERE   SC.'+@SectionLevel+'=''' + @Section + ''' 

						and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
						and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'
						--AND SC.'+@SectionLevel+' in (SELECT SectionID from SECTION WHERE SectionID IN (SELECT SectionID FROM USERS_SECTION where LinkType = ''P'')) --or
            			 
            			group by sd.ChangeRequestID,sc.'+@GroupLevel+',sc.Name,sd.ProdMonth,RU.Name,RU.LastName,sd.[Workplace Name],sd.Name,sd.Comments,sd.WorkplaceID,sd.ChangeType,ppcra.RequestDate,ChangeID,
						PPCR.DAYCREW,PPCR.NIGHTCREW,PPCR.AFTERNOONCREW,PPCR.ROVINGCREW,PPCR.REEFSQM,PPCR.WASTESQM,PPCR.METERS,PPCR.METERSWASTE,PPCR.CUBICMETERS,PPCR.STOPDATE,PPCR.STARTDATE,PPCR.OLDWORKPLACEID,BPD.Description,LBPD.Description,PPCR.FL,s.name, PPCR.DrillRig, p.DrillRig
            			order by sd.ProdMonth desc, sd.ChangeRequestID desc'
  Exec(@SQL1)
  --select @SQL1
  end

  
GO

ALTER Procedure [dbo].[sp_StatusDetails]
as

DELETE FROM StatusDetails
INSERT INTO StatusDetails (Approved,RequestDate, Department , Comments , Declined , ApprovedDeclinedDate ,
Status,ChangeRequestID , ApproveRequestID , Name ,ChangeType, [Workplace Name] ,WorkplaceID, ProdMonth )

SELECT DISTINCT 
PPCRA .Approved ,PPCRA.RequestDate,PPCRA.Department,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate , 
 case when  PPCRA .Approved=1 and PPCRA.Declined=0 then 0 end status
,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.USERID RequestedBy,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN [CODE_PREPLANNINGTYPES] PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth left join [dbo].[StatusDetails] SD on
PPCRA.ApproveRequestID=SD.ApproveRequestID AND
PPCR.ChangeRequestID=SD.ChangeRequestID where PPCRA.Approved=1 and PPCRA.Declined=0 AND SD.ApproveRequestID IS NULL
AND SD.ChangeRequestID IS NULL 

union

--INSERT INTO StatusDetails (Approved, Department , Comments , Declined , ApprovedDeclinedDate ,
--ChangeRequestID , ApproveRequestID , Name ,ChangeType, [Workplace Name] ,WorkplaceID, ProdMonth ,Status)

SELECT DISTINCT 
PPCRA .Approved ,PPCRA.RequestDate,PPCRA.Department,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate , 
 case when  PPCRA .Declined=1 and PPCRA.Approved=0 then 1  end status
,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.USERID RequestedBy,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN [CODE_PREPLANNINGTYPES] PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth left join [dbo].[StatusDetails] SD on
PPCRA.ApproveRequestID=SD.ApproveRequestID AND
PPCR.ChangeRequestID=SD.ChangeRequestID where PPCRA.Declined=1 and PPCRA.Approved=0 AND SD.ApproveRequestID IS NULL
AND SD.ChangeRequestID IS NULL 

union
--INSERT INTO StatusDetails (Approved, Department , Comments , Declined , ApprovedDeclinedDate ,
--ChangeRequestID , ApproveRequestID , Name ,ChangeType, [Workplace Name] ,WorkplaceID, ProdMonth ,Status)

SELECT DISTINCT 
PPCRA .Approved ,PPCRA.RequestDate,PPCRA.Department,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate , 
 case when  PPCRA .Approved=0 and PPCRA.Declined=0  then 2  end status
,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.USERID RequestedBy,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN [CODE_PREPLANNINGTYPES] PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth left join [dbo].[StatusDetails] SD on
PPCRA.ApproveRequestID=SD.ApproveRequestID AND
PPCR.ChangeRequestID=SD.ChangeRequestID where PPCRA.Approved=0 and PPCRA.Declined=0 AND SD.ApproveRequestID IS NULL
AND SD.ChangeRequestID IS NULL --order by PPCRA.ChangeRequestID

union

SELECT DISTINCT 
PPCRA .Approved ,PPCRA.RequestDate,PPCRA.Department,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate , 
 case when  PPCRA .Approved=1 and PPCRA.Declined=1  then 1  end status
,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.USERID RequestedBy,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN [CODE_PREPLANNINGTYPES] PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth left join [dbo].[StatusDetails] SD on
PPCRA.ApproveRequestID=SD.ApproveRequestID AND
PPCR.ChangeRequestID=SD.ChangeRequestID where PPCRA.Approved=1 and PPCRA.Declined=1 AND SD.ApproveRequestID IS NULL
AND SD.ChangeRequestID IS NULL --order by PPCRA.ChangeRequestID
--select * from StatusDetails
GO