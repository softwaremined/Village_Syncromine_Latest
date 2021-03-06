
ALTER procedure [dbo].[sp_Save_Planning] 

	@Prodmonth numeric(7,0),@Sectionid VARCHAR(20), @Sectionid_2 VARCHAR(20), @Workplaceid  VARCHAR(20), @Activity NUMERIC(7,0),
	@IsCubics VARCHAR(5),@SQM NUMERIC(10,3),@WasteSQM NUMERIC(10,3),@FL NUMERIC(10,3),@FaceAdvance NUMERIC(10,3), @IdealSW NUMERIC(10,3),
	@ActualSW NUMERIC(10,3),@ChannelW NUMERIC(10,3),@CMGT NUMERIC(10,3),@FaceCMGT NUMERIC(10,3),@FaceBrokenKG NUMERIC(10,3),
	@FaceTons NUMERIC(10,3),@FaceValue NUMERIC(10,3),@TrammedTons NUMERIC(10,3),@TrammedValue NUMERIC(10,3),@TrammedLevel VarChar(10),
	@CubicMetres NUMERIC(10,3),@OrgUnitDay VARCHAR(50) , @OrgUnitAfternoon VARCHAR(50),
	@OrgUnitNight VARCHAR(50),@RomingCrew VARCHAR(50), @Meters NUMERIC(10,3),@MetersWaste NUMERIC(10,3),@WorkplaceDesc VARCHAR(32), @TargetID NUMERIC(13),
	@Locked BIT, @DEVTONS NUMERIC(10,3),@DEVMAIN NUMERIC(13,3),@DEVSEC NUMERIC(10,3),@DEVCAP NUMERIC(10,3),@DrillRig VARCHAR(20),
	@StartDate DATETIME,@EndDate DATETIME, @GramPerTon  NUMERIC(13,3), @SSID NUMERIC(7,0),@Units NUMERIC(10,2),@AddInfo VARCHAR(50),@AutoUnPlan VARCHAR(2),
	@CrewStrength int, @PlanCode varchar(2),@OGID int,@Depth int,@grams int,@actdepth int,@grade numeric(7,2),@gt NUmeric(7,2),@kg NUMERIC(10,3),@cubicgrams NUMERIC(10,3)
	,@EndHeight numeric(13,3),@EndWidth numeric(13,3),
	@cubicreef numeric(10,3),@cubicwaste numeric(10,3),@CMKGT NUMERIC(7,0),@UraniumBrokenKg NUMERIC(10,3), @RockType VarChar(5)
	--, @StoppedDate DATETIME	,@GoldBroken NUMERIC(10,3),
AS


DECLARE @AddUpdate INT
Declare @Density Numeric(7,3)
Declare @ReefWaste Int

if @rocktype = 'OFF' 
  set @ReefWaste = 1 
else 
  set @ReefWaste = 0

Select @Density = isnull(Density, 2.75) from WORKPLACE where workplaceid = @Workplaceid

if @Activity in(0,1)
begin     
SET @AddUpdate = (

SELECT COUNT(WorkplaceID) totalRows FROM dbo.Planmonth 
WHERE Prodmonth = @Prodmonth AND
      --Sectionid = @Sectionid AND
      Sectionid = @Sectionid AND
      WorkplaceID = @WorkplaceID AND
      Activity = @Activity and Plancode = 'MP') 
IF @AddUpdate = 0 
BEGIN                          
--select * from planmonth where prodmonth = 201602
INSERT INTO dbo.Planmonth
        ( Prodmonth ,
          Sectionid ,
          --Sectionid_2 ,
          --WorkplaceDesc ,
          Activity ,
          IsCubics ,
          Workplaceid,
          TargetID,
          SQM ,
		  ReefSQM ,
          WasteSQM ,
          CubicMetres ,
		  CubicsReef ,
		  CubicsWaste ,
          FL ,
          FaceAdvance ,
          IdealSW ,
          SW ,
          CW ,
          CMGT ,
          Kg ,
		  CubicGrams,
          FaceCMGT ,
          FaceKG ,
          Tons ,
		  ReefTons,
		  WasteTons,
          FaceValue ,
          TrammedTons ,
          TrammedValue ,
          TrammedLevel ,
          OrgUnitDay ,
          OrgUnitAfternoon ,
          OrgUnitNight ,
          RomingCrew ,
          Metresadvance ,
		  ReefAdv ,
          WasteAdv,
          Locked,
          DEVMAIN,
          DEVSEC,
          DEVCAP,
          DrillRig,
          StartDate,
          EndDate,
          GT,
		  CubicGT,
		  PlanCode,
		  CMKGT,
		  UraniumBrokenKG,
		  CubicsTons,
		  DHeight, 
		  DWidth, 
		  Density,
		  ReefWaste
        )
VALUES  ( @Prodmonth , -- Prodmonth - numeric
          @Sectionid , -- Sectionid - varchar(20)
          --@Sectionid_2 , -- Sectionid_2 - varchar(20)
          --@WorkplaceDesc,          
          @Activity , -- Activity - numeric
          @IsCubics , -- IsCubics - varchar(5)
          @Workplaceid , -- Workplaceid - varchar(20)                   
          @TargetID,
		  @SQM + @WasteSQM,
          @SQM , -- SQM - numeric
          @WasteSQM , -- WasteSQM - numeric
          @CubicMetres , -- CubicMetres - numeric
		  @Cubicreef, 
		  @cubicwaste, 
          @FL , -- FL - numeric
          @FaceAdvance , -- FaceAdvance - numeric
          @IdealSW , -- IdealSW - numeric
          @ActualSW , -- ActualSW - numeric
          @ChannelW , -- ChannelW - numeric
          @CMGT , -- CMGT - numeric
		  @kg,
          @cubicgrams , -- GoldBroken - numeric
          @FaceCMGT , -- FaceCMGT - numeric
          @FaceBrokenKG , -- FaceBrokenKG - numeric
          case when @Activity = 0 then @FaceTons else (@Meters + @MetersWaste)*@EndHeight*@EndHeight*@Density end , -- FaceTons - numeric
		  case when @Activity = 0 then @SQM*@ActualSW/100*@Density else @Meters*@EndHeight*@EndHeight*@Density end, -- Reef Tons--
		  case when @Activity = 0 then @WasteSQM*@ActualSW/100*@Density else @MetersWaste*@EndHeight*@EndHeight*@Density end, -- Waste Tons--
          @FaceValue , -- FaceValue - numeric
          @TrammedTons , -- TrammedTons - numeric
          @TrammedValue , -- TrammedValue - numeric
          @TrammedLevel , -- TrammedLevel - VarChar
          @OrgUnitDay , -- OrgUnitDay - varchar(50)
          @OrgUnitAfternoon , -- OrgUnitAfternoon - varchar(50)
          @OrgUnitNight , -- OrgUnitNight - varchar(50)
          @RomingCrew , -- RomingCrew - varchar(50)
          @Meters + @MetersWaste,
		  @Meters , -- Meters - numeric
          @MetersWaste,  -- MetersWaste - numeric
          @Locked,
          @DEVMAIN,
          @DEVSEC,
          @DEVCAP,
          @DrillRig,
          @StartDate,
          @EndDate,
          @GramPerTon,
		  @GramPerTon,
		 'MP',
		  @CMKGT,
		  @UraniumBrokenKg,
		  @CubicMetres*@Density,
		  @EndHeight,
		  @EndWidth,
		  @Density,
		  @ReefWaste
        )     

	  update WORKPLACE SET EndHeight=@EndHeight, EndWidth=@EndWidth where WorkplaceID = @WorkplaceID AND
      Activity = @Activity   
END      
ELSE 
BEGIN
UPDATE dbo.Planmonth SET SQM = @SQM + @WasteSQM,
						   ReefSQM = @SQM,  
                           WasteSQM = @WasteSQM, 
                           CubicMetres = @CubicMetres, 
						   CubicsReef = @CubicReef,
		                   CubicsWaste = @cubicwaste,
                           FL = @FL,
                           FaceAdvance = @FaceAdvance, 
                           IdealSW = @IdealSW, 
                           SW = @ActualSW,
                           CW = @ChannelW,
                           CMGT = @CMGT, 
                           Kg = @kg, 
						   CubicGrams=@cubicgrams,
                           FaceCMGT = @FaceCMGT, 
                           FaceKG = @FaceBrokenKG,
                           Tons = case when @Activity = 0 then @FaceTons else (@Meters + @MetersWaste)*@EndHeight*@EndHeight*@Density end ,
						   ReefTons = case when @Activity = 0 then @SQM*@ActualSW/100*@Density else @Meters*@EndHeight*@EndHeight*@Density end,
						   WasteTons = case when @Activity = 0 then @WasteSQM*@ActualSW/100*@Density else @MetersWaste*@EndHeight*@EndHeight*@Density end,
                           FaceValue = @FaceValue, 
                           TrammedTons = @TrammedTons, 
                           TrammedValue = @TrammedValue,
                           TrammedLevel = @TrammedLevel, 
                           OrgUnitDay = @OrgUnitDay, 
                           OrgUnitAfternoon = @OrgUnitAfternoon,
                           OrgUnitNight = @OrgUnitNight, 
                           RomingCrew = @RomingCrew, 
                           Metresadvance = @Meters + @MetersWaste,
						   ReefAdv = @Meters, 
                           WasteAdv = @MetersWaste,
                           -- WorkplaceDesc = @WorkplaceDesc,
                           Workplaceid = @Workplaceid,  
                           Sectionid = @Sectionid,
                           TargetID = @TargetID,
                           Locked = @Locked,
                           DEVMAIN = @DEVMAIN,
                           DEVSEC = @DEVSEC,
                           DEVCAP = @DEVCAP,
                           DrillRig = @DrillRig,
                           StartDate = @StartDate,
                           EndDate = @EndDate,
                           GT = @GramPerTon,
						   CubicGT = @GramPerTon,
						   CMKGT=@CMKGT,
						   UraniumBrokenKG=@UraniumBrokenKg,
						   CubicsTons = @CubicMetres*@Density,
						   DHeight = @EndHeight,
						   DWidth = @EndWidth,
						   Density = @Density,
						   ReefWaste = @ReefWaste 
                           
WHERE Prodmonth = @Prodmonth AND
      --Sectionid = @Sectionid AND
      Sectionid = @Sectionid AND
      WorkplaceID = @WorkplaceID AND
      Activity = @Activity AND
      IsCubics = @IsCubics and PLANCODE = 'MP'   
	 
	 	  update WORKPLACE SET EndHeight=@EndHeight, EndWidth=@EndWidth where WorkplaceID = @WorkplaceID AND
      Activity = @Activity                            
END
end
else
if @Activity in(2)
begin
SET @AddUpdate = (

SELECT COUNT(WorkplaceID ) totalRows FROM dbo.PLANMONTH_SUNDRYMINING  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @Sectionid AND
      --Sectionid_2 = @Sectionid_2 AND
      --WorkplaceDesc = @WorkplaceDesc AND
	  WorkplaceID = @Workplaceid and
	  SMID =@SSID and
      Activity = @Activity 
	  and OrgunitDay = @OrgUnitDay
	  and Plancode = 'MP')

IF @AddUpdate = 0 
BEGIN
INSERT INTO [dbo].[PLANMONTH_SUNDRYMINING]
           ([Prodmonth]
           ,[SectionID]
           ,[WorkplaceID]
           ,[Activity]
           ,[SMID]
           ,[PlanCode]
           ,[OrgunitDay]
           ,[OrgunitNight]
           ,[Units]
           ,[AddInfo]
           ,[AutoUnPlan]
           ,[CrewStrength]
           ,[MetresAdvance])
VALUES  (   @Prodmonth , -- Prodmonth - numeric
			@Sectionid , -- Sectionid - varchar(20)
			@Workplaceid , -- Workplaceid - varchar(20)
			-- @Sectionid_2 , -- Sectionid_2 - varchar(20)
			-- @WorkplaceDesc,          
			@Activity , -- Activity - numeric
			-- @IsCubics , -- IsCubics - varchar(5)
			@SSID,
			@PlanCode,		 
			@OrgUnitDay , -- OrgUnitDay - varchar(50)        
			@OrgUnitNight , -- OrgUnitNight - varchar(50)
			@Units,
			@AddInfo,
			@AutoUnPlan,
			@CrewStrength,    
			@Meters  -- Meters - numeric
         
        )     
END      
ELSE 
BEGIN
UPDATE dbo.PLANMONTH_SUNDRYMINING  SET 
OrgUnitNight = @OrgUnitNight,                            
Metresadvance = @Meters,
Units = @Units
--,
--SMID = @SSID,						  
--Workplaceid = @Workplaceid,  
--Sectionid = @Sectionid
                          
                           
WHERE Prodmonth = @Prodmonth AND
      --Sectionid = @Sectionid AND
      Sectionid = @Sectionid AND
      --WorkplaceDesc = @WorkplaceDesc AND
	  WorkplaceID = @Workplaceid and
      Activity = @Activity AND
	  SMID = @SSID and
	  OrgUnitDay = @OrgUnitDay and
      PLANCODE = 'MP'   
	 
	                          
END
end

else
if @Activity in(8)
begin
	SET @AddUpdate = (

	SELECT COUNT(WorkplaceID ) totalRows FROM dbo.[PLANMONTH_OLDGOLD]  
	WHERE Prodmonth = @Prodmonth AND
		  Sectionid = @Sectionid AND
		  OGID = @OGID and
		  WorkplaceID = @Workplaceid and
		  OrgUnitDay = @OrgUnitDay and
		  Activity = @Activity and 
		  Plancode = 'MP')

	IF @AddUpdate = 0 
	BEGIN
	INSERT INTO [dbo].[PLANMONTH_OLDGOLD]
			   ([Prodmonth]
			   ,[SectionID]
			   ,[WorkplaceID]
			   ,[Activity]
			   ,[OGID]
			   ,[PlanCode]
			   ,[OrgunitDay]
			   ,[OrgunitNight]
			   ,[Units]
			   ,[OrgunitAfterNoon]
			   ,[ActualDepth]
			   ,[Depth]
			   ,[Grade],
			  [Grams])
	VALUES  ( @Prodmonth , -- Prodmonth - numeric
			  @Sectionid , -- Sectionid - varchar(20)
			  @Workplaceid , -- Workplaceid - varchar(20)        
			  @Activity , -- Activity - numeric
			  @OGID,
			  @PlanCode,		 
			  @OrgUnitDay , -- OrgUnitDay - varchar(50)        
			  @OrgUnitNight , -- OrgUnitNight - varchar(50)
			  @Units,
		      @OrgUnitAfternoon ,
			  @actdepth ,
			  @Depth ,    
			  @gt ,  -- Meters - numeric
			  @grams 
         
			)     
	END      
	ELSE 
	BEGIN
		UPDATE dbo.PLANMONTH_OLDGOLD  SET 
			OrgUnitNight = @OrgUnitNight,   
			OrgunitAfterNoon =@OrgUnitAfternoon ,                         
			Units  = @Units ,
			Grams= @grams,
			Grade = @gt, 
			ActualDepth = @actdepth  				  
                          
                           
		WHERE Prodmonth = @Prodmonth AND
			  --Sectionid = @Sectionid AND
			  Sectionid = @Sectionid AND
			  --WorkplaceDesc = @WorkplaceDesc AND
			  WorkplaceID =@Workplaceid and
			  Activity = @Activity AND
			  PLANCODE = 'MP' and
			  OrgUnitDay = @OrgUnitDay and  
			  OGID = @OGID

	end 

	SET @AddUpdate = (
	select Count(*) from vw_Kriging_Latest where Prodmonth =@Prodmonth and WorkplaceID =@Workplaceid and WeekNo = 1)

	IF @AddUpdate = 0 
	begin
	  Insert into KRIGING(Prodmonth, SectionID,Workplaceid, Activity, WeekNo, ChannelWidth, StopeWidth, CMGT, gt, EndHeight, EndWidth)
	  Values (@Prodmonth, @Sectionid, @Workplaceid, @Activity, 1, @ChannelW, 1000, @CMGT,@gt,@EndHeight,@EndWidth)
	end
	else
	begin
		update KRIGING set
		ChannelWidth = @ChannelW,
		StopeWidth = 1000,
		CMGT = @CMGT, 
		GT =@gt,
		EndHeight = @EndHeight,
		EndWidth = @EndWidth
		where Prodmonth =@Prodmonth and WorkplaceID =@Workplaceid and
		WeekNo = 1 
	end
end


GO

ALTER Procedure [dbo].[SP_Save_Stope_CyclePlan]
--declare
@Username VARCHAR(50), 
@ProdMonth VARCHAR(20), 
@WorkplaceID VARCHAR(50),
@SectionID VARCHAR(50), 
@Activity VARCHAR(20), 
@IsCubics Varchar(1)

as

Declare
@SB VARCHAR(50), 
@Sqm Numeric(18,10), 
@ManualSqm Numeric(18,10), 
@CMGT NUMERIC(18,2), 
@Tons NUMERIC(18,2), 
@StartShift NUMERIC(18),
@EndShift NUMERIC(18),
@STOPEWIDTH NUMERIC(18),
@Cubics NUMERIC(18),
@ManualCubics NUMERIC(18),
@ChannelWIDTH NUMERIC(18),
@Facelength NUMERIC(18),
@GoldGramsPerTon NUMERIC(18,3),
@OnReefSQM NUMERIC(18),
@OffreefPer Numeric(18,3),
@BeginDate DateTime,
@EndDate DateTime,
@Locked int,
@PLanCode Varchar(2),
@Workingday Varchar(1),
@RemainderSQM Numeric(10),
@RemainderCubics Numeric(10),
@TotalShifts int,
@ShiftNo int,
@SaveShiftNo int,
@TotalBlasts int,
@BlastNo int,
@SQMPerBlast Numeric(10),
@CubicsPerBlast Numeric(10),
@Remaining_Shifts Numeric(10),
@Remaining_Blasts Numeric(10),
@TheDate DateTime,
@Density Numeric(18,3),
@MOCycle Varchar(5),
@CycleInput Varchar(10)



--Select @Username = N'MINEWARE_IGGY',
--		@ProdMonth = N'201706',
--		@WorkplaceID = N'RE007667',
--		@SectionID = N'REAAHDA',
--		@Activity = N'0',
--		@IsCubics = N'N'


	--	Select * from planmonth where workplaceid = 54432 and prodmonth = 201611
		


select @SB = ReportToSectionid from SECTION where 
PRODMONTH = @ProdMonth
and SECTIONID = @SectionID

select @BeginDate = BeginDate, @EndDate = EndDate from SECCAL where 
PRODMONTH = @ProdMonth
and SECTIONID = @SB

--select @BeginDate, @EndDate from SECCAL where 
--PRODMONTH = @ProdMonth
--and SECTIONID = @SB

--select 
--@BeginDate = StartDate, @EndDate = StoppedDate
--from PLANMONTH
--  WHERE Prodmonth = @Prodmonth AND
--      Sectionid = @SectionID AND
--      Workplaceid = @WorkplaceID AND
--      Activitycode = @Activity and
--      IsCubics = @IsCubics 





select 
--*
@StartShift = COUNT(CALENDARDATE)
 from (select * from SECCAL) a inner join 
 (select * from CalType) b on
a.CalendarCode = b.CalendarCode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @BeginDate
and b.WORKINGDAY = 'Y'

select 
--*
@EndShift = COUNT(CALENDARDATE)
 from (select * from SECCAL) a inner join 
 (select * from CalType) b on
a.CalendarCode = b.CalendarCode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @EndDate
and b.WORKINGDAY = 'Y'


--select @SB, @BeginDate, @EndDate, @StartShift, @EndShift

--Drop table #Planning

CREATE TABLE #PLANNING(
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
	[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[IsCubics] [char](1) NOT NULL,
	[ShiftDay] [numeric](5, 0) NULL,
	[SQM] [numeric](5, 0) NULL,
	[ReefSQM] [int] NULL,
	[WasteSQM] [int] NULL,
	[Metresadvance] [numeric](9, 3) NULL,
	[ReefAdv] [numeric](10, 3) NULL,
	[WasteAdv] [numeric](10, 3) NULL,
	[Tons] [numeric](5, 0) NULL,
	[ReefTons] [int] NULL,
	[WasteTons] [int] NULL,
	[Grams] [numeric](9, 3) NULL,
	[FL] [numeric](9, 3) NULL,
	[ReefFL] [int] NULL,
	[WasteFL] [int] NULL,
	[SW] [numeric](5, 0) NULL,
	[CW] [numeric](5, 0) NULL,
	[CMGT] [numeric](5, 0) NULL,
	[GT] [numeric](8, 2) NULL,
	[CubicMetres] [numeric](13, 3) NULL,
	[Cubics] [numeric](10, 3) NULL,
	[ReefCubics] [numeric](10, 3) NULL,
	[WasteCubics] [numeric](10, 3) NULL,
	[CubicTons] [numeric](10, 3) NULL,
	[CubicGrams] [numeric](10, 3) NULL,
	[CubicDepth] [numeric](10, 3) NULL,
	[CubicGT] [numeric](10, 3) NULL,
	[MOCycle] [varchar](5) Null,
	[CycleInput] [varchar](10) Null
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[Calendardate] ASC,
	[PlanCode] ASC,
	[IsCubics] ASC
))

CREATE TABLE #MOCycle(
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
	[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[MOCycle] [varchar](5) Null,
	[CycleInput] [varchar](10) Null)


insert into #MOCycle
select 
b.ProdMonth,
b.SectionID,
w.WorkplaceID,
@Activity Activity,
d.CalendarDate,
MOCycle = Case 
When d.Workingday = 'N' then 'OFF'
When d.Workingday = 'Y' And p.MOCycle is null then 'BL'
else p.MOCycle end,
CycleInput = Case 
When CycleInput is null then 'CAL' else CycleInput end
from 

 SECTION_COMPLETE b 
inner join seccal c on 
b.Prodmonth = c.prodmonth and
b.SectionID_1 = c.sectionID 
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CalendarDate and
c.EndDate >= d.CalendarDate 
left join planning p on
b.Prodmonth = p.Prodmonth AND
      b.Sectionid = p.SectionID AND
	  d.CalendarDate = p.CalendarDate 
	  and p.Workplaceid = @WorkplaceID AND
	  p.Activity = @Activity and
      p.IsCubics = @IsCubics and
	  p.PlanCode = 'MP',
	  (Select * from Workplace where WorkplaceID = @WorkplaceID) w
 WHERE b.Prodmonth = @Prodmonth AND
      b.Sectionid = @SectionID 
      
--Select * From #MOCycle

select 
--*
@TotalBlasts = COUNT(CALENDARDATE)
 from #MOCycle a
where a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
	  a.MOCycle = 'BL' and
	  a.CycleInput = 'CAL'

SELECT 
--*
@Sqm = SQM,
@Tons = Tons,
@STOPEWIDTH = SW,
@ChannelWIDTH = CW,
@Facelength = FL,
@GoldGramsPerTon = GT,
@CMGT = cmgt,
@OnReefSQM = ReefSQM,
@PLanCode = PlanCode,
@Density = b.density,
@Cubics = CubicMetres
FROM Planmonth a inner join Workplace b on
a.Workplaceid = b.workplaceid
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP' 

select 
--*
@ManualSqm = isnull(sum(SQM),0),
@ManualCubics = isnull(sum(Cubics),0)
 from Planning a
where a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
	  --a.MOCycle = 'BL' and
	  a.CycleInput = 'MAN'

--select @Sqm,
--@STOPEWIDTH,
--@ChannelWIDTH,
--@Facelength,
--@GoldGramsPerTon,
--@CMGT,
--@OnReefSQM,
--@PLanCode,
--@Density,
--@Cubics

    DECLARE Shaft_Cursor CURSOR FOR
	select CALENDARDATE, WORKINGDAY, TOTALSHIFTS
	 from SECCAL a inner join 
	 CALTYPE b on
	a.CALENDARCODE = b.CALENDARCODE and
	a.BeginDate <= b.CALENDARDATE and
	a.ENDDATE >= b.CALENDARDATE 
	where a.PRODMONTH = @ProdMonth and
	a.SECTIONID = @SB
	--and b.Workingday = 'Y'
	OPEN Shaft_Cursor;
	FETCH NEXT FROM Shaft_Cursor
	into @TheDate, @Workingday, @TotalShifts;

	Set @ShiftNo = 0
	Set @BlastNo = 0
	 Set @RemainderSQM = @Sqm-@ManualSqm
	 Set @RemainderCubics = @Cubics-@ManualCubics
	 Set @Remaining_Blasts = @TotalBlasts
	 WHILE @@FETCH_STATUS = 0
	   BEGIN
		 --set @SQMPerBlast = round(@Sqm/@TotalShifts,0)
		 --set @CubicsPerBlast = round(@Cubics/@TotalShifts,0)

		 Select @MOCycle = mocycle, @CycleInput = CycleInput from #MOCycle where Calendardate = @TheDate

		 if @Workingday = 'Y'
		 begin

		   set @Remaining_Shifts = @TotalShifts - @ShiftNo
		   
		   if @MOCycle = 'BL' and @CycleInput = 'CAL'
		   begin
		     set @Remaining_Blasts = @TotalBlasts - @BlastNo

		     set @SQMPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderSQM/@Remaining_Blasts,0)) end
		     set @CubicsPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderCubics/@Remaining_Blasts,0)) end

			 set @BlastNo = @BlastNo + 1

			 set @RemainderSQM = @RemainderSQM - @SQMPerBlast
		     set @RemainderCubics = @RemainderCubics - @CubicsPerBlast

		   end 
		   else
		   begin
		     set @SQMPerBlast = 0
		     set @CubicsPerBlast = 0
		   end		   

		   set @ShiftNo = @ShiftNo + 1
		   Set @SaveShiftNo = @ShiftNo
		   
		 end 
		 else
		 begin
		    set @SQMPerBlast = 0
		    set @CubicsPerBlast = 0
			Set @SaveShiftNo = 0
		 end

		 --Select @SQMPerBlast, @CubicsPerBlast, @SaveShiftNo, @Remaining_Shifts, @ShiftNo

		 if @CycleInput = 'CAL'
		 begin
			 insert into #PLANNING 
				(Prodmonth,
				SectionID,
				WorkplaceID,
				Activity,
				Calendardate,
				PlanCode,
				IsCubics,
				ShiftDay,
				SQM,
				ReefSQM,
				WasteSQM,
				Metresadvance,
				ReefAdv,
				WasteAdv,
				Tons,
				ReefTons,
				WasteTons,
				Grams,
				FL,
				ReefFL,
				WasteFL,
				SW,
				CW,
				CMGT,
				GT,
				CubicMetres,
				Cubics,
				ReefCubics,
				WasteCubics,
				CubicTons,
				CubicGrams,
				CubicDepth,
				CubicGT,
				[MOCycle],
				CycleInput)

				select
				@ProdMonth ,
				@SectionID SECTIONID,
				@WorkplaceID WORKPLACEID,
				@Activity ACTIVITY,
				@TheDate CALENDARDATE,
				'MP' PLanCode,
				@IsCubics,
				@SaveShiftNo SHIFTDAY,
				SQUAREMETRES = @SQMPerBlast,
				0 ReefSQM,
				0 WasteSQM,
				0 METRESADVANCE,
				0 ReefAdv,
				0 WasteAdv,
				TONS = @SQMPerBlast*@STOPEWIDTH/100*@Density,
				0 ReefTons,
				0 WasteTons,
				0 GRAMS, 
				@Facelength FL,
				0 ReefFL,
				0 WasteFL,
				@STOPEWIDTH SW,
				@ChannelWIDTH CW,
				@CMGT cmgt,
				@GoldGramsPerTon gt,
				CubicMetres = @CubicsPerBlast,
				Cubics = 0,
				ReefCubics = 0,
				WasteCubics = 0,
				CubicTons = @CubicsPerBlast*@Density,
				CubicGrams = @CubicsPerBlast*@Density*@GoldGramsPerTon,
				CubicDepth = 0,
				CubicGT = @GoldGramsPerTon,
				MOCycle =  @MOCycle,
				CycleInput = @CycleInput
            end
			else
			begin
			  	insert into #PLANNING 
				(Prodmonth,
				SectionID,
				WorkplaceID,
				Activity,
				Calendardate,
				PlanCode,
				IsCubics,
				ShiftDay,
				SQM,
				ReefSQM,
				WasteSQM,
				Metresadvance,
				ReefAdv,
				WasteAdv,
				Tons,
				ReefTons,
				WasteTons,
				Grams,
				FL,
				ReefFL,
				WasteFL,
				SW,
				CW,
				CMGT,
				GT,
				CubicMetres,
				Cubics,
				ReefCubics,
				WasteCubics,
				CubicTons,
				CubicGrams,
				CubicDepth,
				CubicGT,
				[MOCycle],
				CycleInput)

				select
				@ProdMonth ,
				@SectionID SECTIONID,
				@WorkplaceID WORKPLACEID,
				@Activity ACTIVITY,
				@TheDate CALENDARDATE,
				'MP' PLanCode,
				@IsCubics,
				@SaveShiftNo SHIFTDAY,
				SQUAREMETRES = SQM,
				0 ReefSQM,
				0 WasteSQM,
				0 METRESADVANCE,
				0 ReefAdv,
				0 WasteAdv,
				TONS = SQM*@STOPEWIDTH/100*@Density,
				0 ReefTons,
				0 WasteTons,
				0 GRAMS, 
				@Facelength FL,
				0 ReefFL,
				0 WasteFL,
				@STOPEWIDTH SW,
				@ChannelWIDTH CW,
				@CMGT cmgt,
				@GoldGramsPerTon gt,
				CubicMetres = CubicMetres,
				Cubics = 0,
				ReefCubics = 0,
				WasteCubics = 0,
				CubicTons = CubicMetres*@Density,
				CubicGrams = 0,
				CubicDepth = 0,
				CubicGT = @GoldGramsPerTon,
				MOCycle =  @MOCycle,
				CycleInput = @CycleInput
				from Planning where Prodmonth = @ProdMonth and sectionid = @SectionID and WorkplaceID = @WorkplaceID and Calendardate = @TheDate and Plancode = 'MP'
			end

		 FETCH NEXT FROM Shaft_Cursor
		   into @TheDate, @Workingday, @TotalShifts;
	   end

CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;




select @OffreefPer = case when @Sqm = 0 then 0 else @OnReefSQM/@Sqm end

--select @OffreefPer

--select * from #PLANNING

--insert into #PLANNING 
--(Prodmonth,
--SectionID,
--WorkplaceID,
--Activity,
--Calendardate,
--PlanCode,
--IsCubics,
--ShiftDay,
--SQM,
--ReefSQM,
--WasteSQM,
--Metresadvance,
--ReefAdv,
--WasteAdv,
--Tons,
--ReefTons,
--WasteTons,
--Grams,
--FL,
--ReefFL,
--WasteFL,
--SW,
--CW,
--CMGT,
--GT,
--CubicMetres,
--Cubics,
--ReefCubics,
--WasteCubics,
--CubicTons,
--CubicGrams,
--CubicDepth,
--CubicGT,
--MOCycle)

--select 

--@ProdMonth ,
--@SectionID SECTIONID,
--@WorkplaceID WORKPLACEID,
--@Activity ACTIVITY,
--CALENDARDATE,
--@PLanCode,
--@IsCubics,
--0 SHIFTDAY,
--SQUAREMETRES = 0,
--0 ReefSQM,
--0 WasteSQM,
--0 METRESADVANCE,
--0 ReefAdv,
--0 WasteAdv,
--TONS = 0,
--0 ReefTons,
--0 WasteTons,
--0 GRAMS, 
--@Facelength FL,
--0 ReefFL,
--0 WasteFL,
--0 SW,
--0 CW,
--0 cmgt,
--0 gt,
--CubicMetres = 0,
--Cubics = 0,
--ReefCubics = 0,
--WasteCubics = 0,
--CubicTons = 0,
--CubicGrams = 0,
--CubicDepth = 0,
--CubicGT = 0,
--MOCycle = CASE WHEN workingday = 'N' THEN 'OFF' ELSE 'BL' END

-- from (select * from SECCAL) a inner join 
-- (select * from CalType) b on
--a.CalendarCode = b.CalendarCode and
--a.BeginDate <= b.CALENDARDATE and
--a.ENDDATE >= b.CALENDARDATE 
--where a.PRODMONTH = @ProdMonth and
--a.SECTIONID = @SB and 
--b.CALENDARDATE <= @EndDate
--and b.WORKINGDAY = 'N'

Update #PLANNING set 
--Select
GRAMS = Round(SQM*@OffreefPer,0)*cmgt/100*(vw_WP_density.density),
METRESADVANCE = case when FL = 0 then 0 else SQM/FL end,
ReefFL = Round(FL*@OffreefPer,0),
WasteFL = FL-Round(FL*@OffreefPer,0),
ReefTons = Round(TONS*@OffreefPer,0),
WasteTons = TONS - Round(TONS*@OffreefPer,0),
ReefSQM = Round(SQM*@OffreefPer,0),
WasteSQM = SQM-Round(SQM*@OffreefPer,0),
ReefAdv = case when FL = 0 then 0 else (SQM/FL)*@OffreefPer end,
WasteAdv = case when FL = 0 then 0 else (SQM/FL)-((SQM/FL)*@OffreefPer) end
from
 #PLANNING PLANNING inner join WORKPLACE vw_WP_density  on
 PLANNING.WORKPLACEID Collate SQL_Latin1_General_CP1_CI_AS = vw_wp_density.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      PLANNING.Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'

--select * from #PLANNING

Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate > @EndDate
 
Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate < @BeginDate

Update PLANMONTH set 
ReefFL = Round(FL*@OffreefPer,0),
WasteFL = FL- Round(FL*@OffreefPer,0)
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      Workplaceid = @WorkplaceID AND
      Activity = @Activity and
	  PlanCode = 'MP' and
      IsCubics = @IsCubics

insert into PLANNING 
(Prodmonth,
SectionID,
WorkplaceID,
Activity,
Calendardate,
PlanCode,
IsCubics,
ShiftDay,
SQM,
ReefSQM,
WasteSQM,
Metresadvance,
ReefAdv,
WasteAdv,
Tons,
ReefTons,
WasteTons,
Grams,
FL,
ReefFL,
WasteFL,
SW,
CW,
CMGT,
GT,
CubicMetres,
Cubics,
ReefCubics,
WasteCubics,
CubicTons,
CubicGrams,
CubicDepth,
CubicGT,
MOCycle,
CycleInput)
Select a.* from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS
Where b.Prodmonth is null


Update PLANNING set
ShiftDay = a.ShiftDay,
SQM = a.SQM,
ReefSQM = a.ReefSQM,
WasteSQM = a.WasteSQM,
Metresadvance = a.Metresadvance,
ReefAdv = a.ReefAdv,
WasteAdv = a.WasteAdv,
Tons = a.Tons,
ReefTons = a.ReefTons,
WasteTons = a.WasteTons,
Grams = a.Grams,
FL = a.FL,
ReefFL = a.ReefFL,
WasteFL = a.WasteFL,
SW = a.SW,
CW = a.CW,
CMGT = a.CMGT,
GT = a.GT,
CubicMetres = a.CubicMetres,
Cubics = a.Cubics,
ReefCubics = a.ReefCubics,
WasteCubics = a.WasteCubics,
CubicTons = a.CubicTons,
CubicGrams = a.CubicGrams,
CubicDepth = a.CubicDepth,
CubicGT = a.CubicGT
from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS


Drop Table #PLANNING
DROP Table #MOCycle


GO

ALTER Procedure [dbo].[sp_PrePlanning_CallValueChanges] 
	@RequestID INT,
@UserID VARCHAR(10)

AS

DECLARE
@Prodmonth NUMERIC(7,0),
@Cubes NUMERIC(7,3),
@SQM NUMERIC(7,3),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@Activity NUMERIC(7,0), 
@SQL Varchar(1000),
@Username VARCHAR(50)

--SET @RequestID = 1076
--SET @UserID = 'MINEWARE'
SET @Prodmonth=( SELECT CR.Prodmonth FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
SET @SectionID=( SELECT CR.SectionID FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
SET @WorkplaceID=( SELECT CR.WorkplaceID FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)


SET @Username=@UserID
SET @Activity = --( SELECT CR.Activity FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
 (SELECT PP.Activity FROM PrePlanning_ChangeRequest CR 
INNER JOIN Planmonth PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and Plancode = 'MP')

IF  @Activity = 0
BEGIN


UPDATE pp SET SQM = CR.ReefSQM + CR.WasteSQM,ReefSQM = CR.ReefSQM,
                       WasteSQM = CR.WasteSQM,
                       --StoppedDate = CR.StopDate,
                       CubicMetres = cr.CubicMeters,
					   CubicsReef = case when b.ReefWaste = 0 then cr.CubicMeters else 0 end,
					   CubicsWaste = case when b.ReefWaste = 1 then cr.CubicMeters else 0 end,
					   KG = Round((CR.ReefSQM*(CMGT/100)*b.density)/1000,3),
					   tons = Round((CR.ReefSQM + CR.WasteSQM)*(SW/100)*b.density,0),
					   Reeftons = Round(CR.ReefSQM*(SW/100)*b.density,0),
			           Wastetons = Round(CR.WasteSQM*(SW/100)*b.density,0),
					   IsStopped = 'N',
					   FL=CR.FL
					  
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN planmonth PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth inner join 
	 WORKPLACE b on
	 PP.Workplaceid = b.workplaceid
WHERE CR.ChangeRequestID = @RequestID  and Plancode = 'MP'



--UPDATE planning  SET SQM = CR.ReefSQM + CR.WasteSQM,
--                     ReefSQM = CR.ReefSQM,
--                     WasteSQM = CR.WasteSQM,
                                    
--					  CUBICMETRES = CR.CubicMeters
					
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN planmonth PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN planning  PD on
--PD.PRODMONTH = pp.Prodmonth and
--PD.WORKPLACEID = PP.Workplaceid and
--PD.SECTIONID = pp.Sectionid and
--pd.plancode = pp.plancode
--WHERE CR.ChangeRequestID = @RequestID --and PM.IsCubics = 'N'
--and pp.plancode = 'MP'

  set @SQL = '[SP_Save_Stope_CyclePlan] '''+@Username+''','+Cast(@ProdMonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'N'+''''
      
      exec (@SQL)


END  -- end Stoping  
ELSE BEGIN
IF @Activity =1
BEGIN
UPDATE planmonth SET ReefSQM = CR.ReefSQM,
                       WasteSQM = CR.WasteSQM,
                       --StoppedDate = CR.StopDate,
					   METRESADVANCE  =CR.Meters +CR.MetersWaste ,
                       CubicMetres = cr.CubicMeters,
					   CubicsReef = case when b.ReefWaste = 0 then cr.CubicMeters else 0 end,
					   CubicsWaste = case when b.ReefWaste = 1 then cr.CubicMeters else 0 end,
					  Reefadv =CR.Meters ,
					  Wasteadv =CR.MetersWaste,  IsStopped = 'N'--,  SQM = CR.Meters +CR.MetersWaste
					  
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN planmonth PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
inner join 
	 WORKPLACE b on
	 PP.Workplaceid = b.workplaceid
WHERE CR.ChangeRequestID = @RequestID and plancode = 'MP'


--UPDATE planning  SET METRESADVANCE  =CR.Meters +CR.MetersWaste ,					
--                     ReefAdv   = CR.Meters,
--                     WasteAdv   = CR.MetersWaste,
--   					 sqm = CR.Meters +CR.MetersWaste,					 
--					  CUBICMETRES = CR.CubicMeters,
--					  ReefTons =CR.Meters ,
--					  WasteTons =CR.MetersWaste 
					
----SELECT * 
-- FROM PrePlanning_ChangeRequest CR 
--INNER JOIN planmonth PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN planning  PD on
--PD.PRODMONTH = pp.Prodmonth and
--PD.WORKPLACEID = PP.Workplaceid AND
--PD.SECTIONID = pp.Sectionid and
--pd.plancode = pp.plancode
--WHERE CR.ChangeRequestID = @RequestID and pp.plancode = 'MP'

  set @SQL = '[SP_Save_Dev_CyclePlan] '''+@Username+''','+Cast(@ProdMonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'N'+''''
      
      exec (@SQL)


END
END



GO



ALTER Procedure [dbo].[sp_PrePlanning_CallValueChanges] 
--Declare
	@RequestID INT,
@UserID VARCHAR(10)

AS

DECLARE
@Prodmonth NUMERIC(7,0),
@Cubes NUMERIC(7,3),
@SQM NUMERIC(7,3),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@Activity NUMERIC(7,0), 
@SQL Varchar(1000),
@Username VARCHAR(50)

--SET @RequestID = 2
--SET @UserID = 'MINEWARE'

SET @Prodmonth=( SELECT CR.Prodmonth FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
SET @SectionID=( SELECT CR.SectionID FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
SET @WorkplaceID=( SELECT CR.WorkplaceID FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)


SET @Username=@UserID
SET @Activity = --( SELECT CR.Activity FROM PrePlanning_ChangeRequest CR where  CR.ChangeRequestID = @RequestID)
 (SELECT PP.Activity FROM PrePlanning_ChangeRequest CR 
INNER JOIN Planmonth PP on
CR.SectionID Collate SQL_Latin1_General_CP1_CI_AS= PP.Sectionid Collate SQL_Latin1_General_CP1_CI_AS and
CR.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS = PP.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and Plancode = 'MP')

Select @Prodmonth, @SectionID, @WorkplaceID

IF  @Activity = 0
BEGIN


UPDATE pp SET SQM = CR.ReefSQM + CR.WasteSQM,ReefSQM = CR.ReefSQM,
                       WasteSQM = CR.WasteSQM,
                       --StoppedDate = CR.StopDate,
                       CubicMetres = cr.CubicMeters,
					   CubicsReef = case when b.ReefWaste = 'R' then cr.CubicMeters else 0 end,
					   CubicsWaste = case when b.ReefWaste = 'W' then cr.CubicMeters else 0 end,
					   KG = Round((CR.ReefSQM*(CMGT/100)*b.density)/1000,3),
					   tons = Round((CR.ReefSQM + CR.WasteSQM)*(SW/100)*b.density,0),
					   Reeftons = Round(CR.ReefSQM*(SW/100)*b.density,0),
			           Wastetons = Round(CR.WasteSQM*(SW/100)*b.density,0),
					   IsStopped = 'N',
					   FL=CR.FL
					  
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN planmonth PP on
CR.SectionID Collate SQL_Latin1_General_CP1_CI_AS = PP.Sectionid Collate SQL_Latin1_General_CP1_CI_AS and
CR.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS = PP.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS and 
CR.ProdMonth = PP.Prodmonth inner join 
	 WORKPLACE b on
	 PP.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS  = b.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE CR.ChangeRequestID = @RequestID  and Plancode = 'MP'



--UPDATE planning  SET SQM = CR.ReefSQM + CR.WasteSQM,
--                     ReefSQM = CR.ReefSQM,
--                     WasteSQM = CR.WasteSQM,
                                    
--					  CUBICMETRES = CR.CubicMeters
					
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN planmonth PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN planning  PD on
--PD.PRODMONTH = pp.Prodmonth and
--PD.WORKPLACEID = PP.Workplaceid and
--PD.SECTIONID = pp.Sectionid and
--pd.plancode = pp.plancode
--WHERE CR.ChangeRequestID = @RequestID --and PM.IsCubics = 'N'
--and pp.plancode = 'MP'

  set @SQL = '[SP_Save_Stope_CyclePlan] '''+@Username+''','+Cast(@ProdMonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'N'+''''
      
      exec (@SQL)


END  -- end Stoping  
ELSE 
BEGIN
IF @Activity =1
BEGIN
UPDATE planmonth SET ReefSQM = CR.ReefSQM,
                       WasteSQM = CR.WasteSQM,
                       --StoppedDate = CR.StopDate,
					   METRESADVANCE  =CR.Meters +CR.MetersWaste ,
                       CubicMetres = cr.CubicMeters,
					   CubicsReef = case when b.ReefWaste = 'R' then cr.CubicMeters else 0 end,
					   CubicsWaste = case when b.ReefWaste = 'W' then cr.CubicMeters else 0 end,
					  Reefadv =CR.Meters ,
					  Wasteadv =CR.MetersWaste,  IsStopped = 'N'--,  SQM = CR.Meters +CR.MetersWaste
					  
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN planmonth PP on
CR.SectionID Collate SQL_Latin1_General_CP1_CI_AS = PP.Sectionid Collate SQL_Latin1_General_CP1_CI_AS and
CR.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS = PP.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS and
CR.ProdMonth = PP.Prodmonth
inner join 
	 WORKPLACE b on
	 PP.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE CR.ChangeRequestID = @RequestID and plancode = 'MP'


--UPDATE planning  SET METRESADVANCE  =CR.Meters +CR.MetersWaste ,					
--                     ReefAdv   = CR.Meters,
--                     WasteAdv   = CR.MetersWaste,
--   					 sqm = CR.Meters +CR.MetersWaste,					 
--					  CUBICMETRES = CR.CubicMeters,
--					  ReefTons =CR.Meters ,
--					  WasteTons =CR.MetersWaste 
					
----SELECT * 
-- FROM PrePlanning_ChangeRequest CR 
--INNER JOIN planmonth PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN planning  PD on
--PD.PRODMONTH = pp.Prodmonth and
--PD.WORKPLACEID = PP.Workplaceid AND
--PD.SECTIONID = pp.Sectionid and
--pd.plancode = pp.plancode
--WHERE CR.ChangeRequestID = @RequestID and pp.plancode = 'MP'

  set @SQL = '[SP_Save_Dev_CyclePlan] '''+@Username+''','+Cast(@ProdMonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'N'+''''
      
      exec (@SQL)


END
END



GO

ALTER Procedure [dbo].[sp_PrePlanning_ChangeMail_Approve]
--declare 
@ChangeRequestID VARCHAR(50)

 as
 --set @ChangeRequestID='2'

 declare @ToMailScript  nvarchar(4000), @ToMail VarChar(60),@ChangeRequestIDFormat AS VARCHAR(50)
 Declare @SyncroDB VarChar(50)

declare @sqlstatement nvarchar(4000)


 SET @ChangeRequestIDFormat= dbo.RequestIDFormat(@ChangeRequestID)
set @ToMailScript='
	SELECT  U1.EMail UMail
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') RU On
PPCR.RequestBy Collate SQL_Latin1_General_CP1_CI_AS = RU.UserID Collate SQL_Latin1_General_CP1_CI_AS
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') U1 On
PPCR.RequestBy Collate SQL_Latin1_General_CP1_CI_AS = U1.UserID Collate SQL_Latin1_General_CP1_CI_AS
WHERE PPCR.ChangeRequestID = '+@ChangeRequestID+'
UNION
SELECT 
U2.EMail UMail
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') RU On
PPCR.RequestBy Collate SQL_Latin1_General_CP1_CI_AS = RU.UserID Collate SQL_Latin1_General_CP1_CI_AS
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '''') U2 On
PPCR.RequestBy Collate SQL_Latin1_General_CP1_CI_AS = U2.UserID Collate SQL_Latin1_General_CP1_CI_AS
WHERE PPCR.ChangeRequestID = '+@ChangeRequestID+''

--select @ToMailScript

CREATE TABLE #mail(
	[USERMAIL] [varchar](60) NOT NULL )
	INSERT INTO #mail
 exec (@ToMailScript)
set @ToMail = (select [USERMAIL] from  #mail)


DECLARE @Message varchar(120),@Department varchar(120),@ApprovedDeclinedByUser varchar(50),@ApprovedDeclinedDate datetime,@xml NVARCHAR(MAX),@body NVARCHAR(MAX)


SET @xml = CAST(( 
select Department as 'td','', ApprovedDeclinedByUser as 'td','',CONVERT(VARCHAR(10), ApprovedDeclinedDate,101) as 'td','', Comments  as 'td' from 
(
select Department,ApprovedDeclinedByUser,ApprovedDeclinedDate,Comments from
(
SELECT  
PPCRA.Comments,PPCRA.Department ,U1.Name+' '+U1.LastName ApprovedDeclinedByUser,PPCRA.ApprovedDeclinedDate  
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '') RU On 
PPCR.RequestBy Collate SQL_Latin1_General_CP1_CI_AS = RU.UserID Collate SQL_Latin1_General_CP1_CI_AS
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '') U1 On
PPCRA.User1 Collate SQL_Latin1_General_CP1_CI_AS = U1.UserID Collate SQL_Latin1_General_CP1_CI_AS
WHERE PPCR.ChangeRequestID = @ChangeRequestID)abc
UNION

select Department,ApprovedDeclinedByUser,ApprovedDeclinedDate,Comments from
(
SELECT 

PPCRA.Comments,PPCRA.Department ,U2.Name+' '+U2.LastName ApprovedDeclinedByUser,PPCRA.ApprovedDeclinedDate  
FROM PrePlanning_ChangeRequest PPCR
INNER JOIN PrePlanning_ChangeRequest_Approval PPCRA ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID 
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '') RU On
PPCR.RequestBy Collate SQL_Latin1_General_CP1_CI_AS = RU.UserID Collate SQL_Latin1_General_CP1_CI_AS
INNER JOIN (SELECT * FROM tblUsers WHERE  UserID <> '') U2 On
PPCRA.User2 Collate SQL_Latin1_General_CP1_CI_AS = U2.UserID Collate SQL_Latin1_General_CP1_CI_AS
WHERE PPCR.ChangeRequestID = @ChangeRequestID)ase)main 
FOR XML PATH('tr'), ELEMENTS ) AS NVARCHAR(MAX))




SET @body ='<html><body><H3>Your Request ' + cast(@ChangeRequestIDFormat as NVARCHAR(MAX)) + ' has been Approved </H3>
<table border = 1> 
<tr>
<th> Department </th> <th> Approved By User </th> <th> Approved Date </th> <th> Comments </th></tr>'
 
SET @body = @body + cast(@xml as NVARCHAR(MAX)) +'</table><p class="style4"> Mail Generated from Syncromine</p></body></html>'                        

EXEC msdb.dbo.sp_send_dbmail
@profile_name = 'GFL_MAIL',
@recipients =  @ToMail, 
@body = @body,
@body_format = 'HTML' ,
@subject = 'Planning Change Request';  
 drop table #mail  
 

GO

