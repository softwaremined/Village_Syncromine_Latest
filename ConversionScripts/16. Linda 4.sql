drop table GENERICREPORT_OPTIONS
go

CREATE TABLE [dbo].[GENERICREPORT_OPTIONS](
	[UserID] [varchar](30) NULL,
	[ReportName] [varchar](30) NULL,
	[DateType] [varchar](1) NULL,
	[ProdMonth] [varchar](6) NULL,
	[FromProdMonth] [varchar](6) NULL,
	[ToProdMonth] [varchar](6) NULL,
	[CalendarDate] [datetime] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[SectionID] [varchar](10) NULL,
	[HierID] [numeric](7, 0) NULL,
	[SectionName] [varchar](60) NULL,
	[AuthPlanOnly] [varchar](1) NULL,
	[StopeLedge] [varchar](1) NULL,
	[LevelGM] [varchar](1) NULL,
	[LevelMN] [varchar](1) NULL,
	[LevelMNM] [varchar](1) NULL,
	[LevelMO] [varchar](1) NULL,
	[LevelSB] [varchar](1) NULL,
	[LevelMiner] [varchar](1) NULL,
	[LevelWP] [varchar](1) NULL,
	[PlanDyn] [varchar](1) NULL,
	[PlanLock] [varchar](1) NULL,
	[PlanDynProg] [varchar](1) NULL,
	[PlanLockProg] [varchar](1) NULL,
	[Book] [varchar](1) NULL,
	[Meas] [varchar](1) NULL,
	[PlanBuss] [varchar](1) NULL,
	[FC] [varchar](1) NULL,
	[AbsVar] [varchar](1) NULL,
	[StpSqm] [varchar](2) NULL,
	[StpSqmOn] [varchar](2) NULL,
	[StpSqmOff] [varchar](2) NULL,
	[StpSqmOS] [varchar](2) NULL,
	[StpSqmOSF] [varchar](2) NULL,
	[StpCmgt] [varchar](2) NULL,
	[StpCmgtTot] [varchar](2) NULL,
	[StpGT] [varchar](2) NULL,
	[StpGTTot] [varchar](2) NULL,
	[StpSW] [varchar](2) NULL,
	[StpSWIdeal] [varchar](2) NULL,
	[StpSWFault] [varchar](2) NULL,
	[StpCW] [varchar](2) NULL,
	[StpKG] [varchar](2) NULL,
	[StpFL] [varchar](2) NULL,
	[StpFLOn] [varchar](2) NULL,
	[StpFLOff] [varchar](2) NULL,
	[StpFLOS] [varchar](2) NULL,
	[StpAdv] [varchar](2) NULL,
	[StpAdvOn] [varchar](2) NULL,
	[StpAdvOff] [varchar](2) NULL,
	[StpTons] [varchar](2) NULL,
	[StpTonsOn] [varchar](2) NULL,
	[StpTonsOff] [varchar](2) NULL,
	[StpTonsOS] [varchar](2) NULL,
	[StpTonsFault] [varchar](2) NULL,
	[StpCubics] [varchar](2) NULL,
	[StpCubTons] [varchar](2) NULL,
	[StpCubGT] [varchar](2) NULL,
	[StpCubKG] [varchar](2) NULL,
	[StpMeasSweeps] [varchar](2) NULL,
	[StpLabour] [varchar](2) NULL,
	[StpShftInfo] [varchar](2) NULL,
	[DevKG] [varchar](2) NULL,
	[DevAdv] [varchar](2) NULL,
	[DevAdvOn] [varchar](2) NULL,
	[DevAdvOff] [varchar](2) NULL,
	[DevTons] [varchar](2) NULL,
	[DevTonsOn] [varchar](2) NULL,
	[DevTonsOff] [varchar](2) NULL,
	[DevCmgt] [varchar](2) NULL,
	[DevCmgtTot] [varchar](2) NULL,
	[DevGT] [varchar](2) NULL,
	[DevGTTot] [varchar](2) NULL,
	[DevCubics] [varchar](2) NULL,
	[DevCubTons] [varchar](2) NULL,
	[DevCubGT] [varchar](2) NULL,
	[DevCubKG] [varchar](2) NULL,
	[DevLabour] [varchar](2) NULL,
	[DevShftInfo] [varchar](2) NULL,
	[DevDrillRig] [varchar](2) NULL,
	[DevEH] [varchar](1) NULL,
	[DevEW] [varchar](1) NULL
) ON [PRIMARY]

GO




ALTER  PROCEDURE [dbo].[sp_GenericReport]
--declare
@NAME_5 varchar(1) = 'Y', 
@NAME_4 varchar(1) = 'Y', 
@NAME_3 varchar(1) = 'Y',  
@NAME_2 varchar(1) = 'Y', 
@NAME_1 varchar(1) = 'Y', 
@NAME varchar(1) = 'Y', 
@Workplace varchar(1) = 'Y',

@RunLevel int = 1,
@SectionID varchar(20) = 'GM',

@ProdMonth varchar(6) = '201704',
@FromMonth varchar(6) = '201704', 
@ToMonth varchar(6) = '201704',
@CalendarDate varchar(10) = '2017-05-21',
@FromDate varchar(10) = '2017-05-21',
@ToDate varchar(10) = '2017-05-21',
 
@ReportType varchar(20) = 'P',  --  P-Production Month, M-From_To Month, D-From-To Date
@TotalsPerMonth Varchar(2) = 'Y',

--@TheOreFlowLevel varchar(8000),
--@TheReef varchar(1000),

--@TheIndicator char,
--@TheMiningMethod char,
@PlanMonth varchar(1) = 'Y',
@PlanMonthLock varchar(1) = 'Y',
@PlanProg varchar(1) = 'N',
@PlanProgLock varchar(1) = 'N',
@Book varchar(1) = 'Y',
@Meas varchar(1) = 'N',
@PlanBuss varchar(1) = 'N',
@Abs varchar(1) = 'N',
@FC varchar(1) = 'N',

@TheStopeLedge varchar(1) = '0' -- 0 All, 1 Stoping, 2 Ledging
as

declare @SelectPart varchar(max)  --varchar(8000)
declare @SelectPart1 varchar(max)  --varchar(8000)
declare @SelectPart2 varchar(max)  --varchar(8000)
declare @SelectPart3 varchar(max)  --varchar(8000)
set @SelectPart  = ''
set @SelectPart1 = ''
set @SelectPart2 = ''
set @SelectPart3 = ''

declare @PlanSelect varchar(max)  --varchar(8000)
declare @PlanSelect1 varchar(max)  --varchar(8000)
declare @PlanSelect2 varchar(max)  --varchar(8000)
declare @PlanFrom varchar(max)  --varchar(8000)
declare @PlanWhere varchar(max)  --varchar(8000)
set @PlanSelect = ''
set @PlanSelect1 = ''
set @PlanSelect2 = ''
set @PlanFrom = ''
set @PlanWhere = ''


declare @SurveyUnion varchar(max)  --varchar(8000)
declare @SurveySelect varchar(max)  --varchar(8000)
declare @SurveySelect1 varchar(max)  --varchar(8000)
declare @SurveyFrom varchar(max)  --varchar(8000)
declare @SurveyWhere varchar(max)  --varchar(8000)
set @SurveyUnion = ''
set @SurveySelect = ''
set @SurveySelect1 = ''
set @SurveyFrom = ''
set @SurveyWhere = ''

declare @BusUnion varchar(max)  --varchar(8000)
declare @BusSelect varchar(max)  --varchar(8000)
declare @BusSelect1 varchar(max)  --varchar(8000)
declare @BusFrom varchar(max)  --varchar(8000)
declare @BusWhere varchar(max)  --varchar(8000)
set @BusUnion = ''
set @BusSelect = ''
set @BusSelect1 = ''
set @BusFrom = ''
set @BusWhere = ''

declare @BusDevUnion varchar(max)  --varchar(8000)
declare @BusDevSelect varchar(max)  --varchar(8000)
declare @BusDevSelect1 varchar(max)  --varchar(8000)
declare @BusDevFrom varchar(max)  --varchar(8000)
declare @BusDevWhere varchar(max)  --varchar(8000)
set @BusDevUnion = ''
set @BusDevSelect = ''
set @BusDevSelect1 = ''
set @BusDevFrom = ''
set @BusDevWhere = ''

declare @LPlanUnion varchar(max)  --varchar(8000)
declare @LPlanSelect varchar(max)  --varchar(8000)
declare @LPlanSelect1 varchar(max)  --varchar(8000)
declare @LPlanSelect2 varchar(max)  --varchar(8000)
declare @LPlanFrom varchar(max)  --varchar(8000)
declare @LPlanWhere varchar(max)  --varchar(8000)
set @LPlanUnion = ''
set @LPlanSelect = ''
set @LPlanSelect1 = ''
set @LPlanSelect2 = ''
set @LPlanFrom = ''
set @LPlanWhere = ''

declare @GroupBy varchar(max)  --varchar(8000)
set @GroupBy = ''

declare @ReadSection varchar (30) 
 
declare @RunName VARCHAR(100)
declare @TheStopeLedge1 varchar(100)


IF (@TheStopeLedge = '0')
	set @TheStopeLedge1 = ' pm.Activity = 0 '

IF (@TheStopeLedge = '1')
	set @TheStopeLedge1 = ' pm.Activity = 0 and pm.TargetID <> 9 '

IF (@TheStopeLedge = '2')
	set @TheStopeLedge1 = ' pm.Activity = 0 and pm.TargetID = 9 '

IF @RunLevel = 1
BEGIN
  SET @RunName = 'Name_5'
  SET @ReadSection = ' sc.SectionID_5 ' 
END
  
IF @RunLevel = 2
BEGIN
  SET @RunName = 'Name_4'
  SET @ReadSection = ' sc.SectionID_4 ' 
END
IF @RunLevel = 3
BEGIN
  SET @RunName = 'Name_3'
  SET @ReadSection = ' sc.SectionID_3 ' 
END
IF @RunLevel = 4
BEGIN
  SET @RunName = 'Name_2'
  SET @ReadSection = ' sc.SectionID_2 ' 
END
IF @RunLevel = 5
BEGIN
  SET @RunName = 'Name_1'
  SET @ReadSection = ' sc.SectionID_1 ' 
END
IF @RunLevel = 6
BEGIN
  SET @RunName = 'Name'
  SET @ReadSection = ' sc.SectionID ' 
END


set @SelectPart = 'Select a.Prodmonth, '

--if @ReportType = 'P' or @ReportType = 'M'
--begin
--  if @TotalsPerMonth = 'Y' 
--    set @SelectPart = @SelectPart+ ' Prodmonth = cast(a.Prodmonth as varchar(6)), '
--	else 
--	set @SelectPart = @SelectPart+ ' '''' Prodmonth, '
--end  

--if @ReportType = 'D'
--begin
--    set @SelectPart = @SelectPart+ ' '''' Prodmonth, ' 
--end  

--SET the grouping/ selection Column's   

IF @NAME_5 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_5, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_5, ' 
  
IF @NAME_4 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_4, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_4, ' 
  
IF @NAME_3 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_3, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_3, ' 
  
IF @NAME_2 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_2, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_2, ' 
  
IF @NAME_1 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_1, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_1, ' 
  
IF @NAME = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME, '   

IF @Workplace = 'Y' 
  set @SelectPart = @SelectPart+ ' a.Workplace, '  
else
  set @SelectPart = @SelectPart+ ' Null workplace, ' 

set @SelectPart = @SelectPart+'	
			sum(Plan_Stope) Plan_Stope, 
			sum(Plan_Dev) Plan_Dev, 
			TotalShifts = max(TotalShifts),
			ShiftNo = max(ShiftNo), 
			'''+@TotalsPerMonth+''' TotalsPerMonth,
			Stp_Plan_FL = cast(sum(Stp_Plan_FL) as numeric(18,4)),
			Stp_Plan_FLReef = cast(sum(Stp_Plan_FLReef) as numeric(18,4)),
			Stp_Plan_FLWaste = cast(sum(Stp_Plan_FLWaste) as numeric(18,4)),
			Stp_Plan_Sqm = cast(sum(Stp_Plan_Sqm) as numeric(18,4)),
			Stp_Plan_SqmReef = cast(sum(Stp_Plan_SqmReef) as numeric(10,1)),
			Stp_Plan_SqmWaste = cast(sum(Stp_Plan_SqmWaste) as numeric(18,4)),  
			Stp_Plan_Adv = cast(sum(Stp_Plan_Adv) as numeric(18,4)),
			Stp_Plan_AdvReef = cast(sum(Stp_Plan_AdvReef) as numeric(10,1)),
			Stp_Plan_AdvWaste = cast(sum(Stp_Plan_AdvWaste) as numeric(18,4)),   
			Stp_Plan_SqmSW = cast(sum(Stp_Plan_SqmSW) as numeric(18,4)),
			Stp_Plan_SqmCW = cast(sum(Stp_Plan_SqmCW) as numeric(18,4)),
			Stp_Plan_SqmCmgt = cast(sum(Stp_Plan_SqmCmgt) as numeric(18,4)),
			Stp_Plan_SqmCmgtTotal = cast(sum(Stp_Plan_SqmCmgtTotal) as numeric(18,4)),
			Stp_Plan_Tons = cast(sum(Stp_Plan_Tons) as numeric(18,4)),   
			Stp_Plan_TonsReef = cast(sum(Stp_Plan_TonsReef)as numeric(18,4)),
			Stp_Plan_TonsWaste = cast(sum(Stp_Plan_TonsWaste) as numeric(18,4)),
			Stp_Plan_Kg = cast(sum(Stp_Plan_Kg)  as numeric(18,6)),
			Stp_Plan_Cubics = cast(sum(Stp_Plan_Cubics) as numeric(18,6)),
			Stp_Plan_CubicTons = cast(sum(Stp_Plan_CubicTons) as numeric(18,6)),
			Stp_Plan_CubicGrams = cast(sum(Stp_Plan_CubicGrams) as numeric(18,6)),

			Dev_Plan_AdvReef = cast(sum(Dev_Plan_AdvReef) as numeric(18,6)),
			Dev_Plan_AdvWaste = cast(sum(Dev_Plan_AdvWaste) as numeric(18,6)),
			Dev_Plan_Primm = cast(sum(Dev_Plan_Primm) as numeric(18,6)),
			Dev_Plan_Secm = cast(sum(Dev_Plan_Secm) as numeric(18,6)), 
			Dev_Plan_Adv = cast(sum(Dev_Plan_Adv) as numeric(18,6)),
			Dev_Plan_TonsReef = cast(sum(Dev_Plan_TonsReef) as numeric(18,6)),
			Dev_Plan_TonsWaste = cast(sum(Dev_Plan_TonsWaste) as numeric(18,6)), 
			Dev_Plan_Tons = cast(sum( Dev_Plan_Tons) as numeric(18,6)),
			Dev_Plan_KG = cast(sum(Dev_Plan_KG) as numeric(18,6)),
			Dev_Plan_AdvEH = cast(sum(Dev_Plan_AdvEH) as numeric(18,6)), 
			Dev_Plan_AdvEW = cast(sum(Dev_Plan_AdvEW) as numeric(18,6)),
			Dev_Plan_AdvCmgt = cast(sum(Dev_Plan_AdvCmgt) as numeric(18,6)),
			Dev_Plan_AdvCmgtTotal = cast(sum(Dev_Plan_AdvCmgtTotal) as numeric(18,6)),
			Dev_Plan_Cubics = cast(Sum(Dev_Plan_Cubics) as numeric(18,6)), 
			Dev_Plan_CubicTons = cast(Sum(Dev_Plan_CubicTons) as numeric(18,6)), 
			Dev_Plan_CubicGrams = cast(Sum(Dev_Plan_CubicGrams) / 1000 as numeric(18,6)), 
			Dev_Plan_Labour = cast(Sum(Dev_Plan_Labour) as numeric(18,6)), 
			Dev_Plan_ShftInfo = cast(Sum(Dev_Plan_ShftInfo) as numeric(18,6)), 
			Dev_Plan_DrillRig = max(Dev_Plan_DrillRig), 

			Stp_PPlan_Sqm = cast(Sum(Stp_PPlan_Sqm) as numeric(18,4)),  
			Stp_PPlan_SqmReef = cast(Sum(Stp_PPlan_SqmReef) as numeric(18,1)),
			Stp_PPlan_SqmWaste = cast(Sum(Stp_PPlan_SqmWaste) as numeric(18,4)),
			Stp_PPlan_Adv = cast(Sum(Stp_PPlan_Adv) as numeric(18,4)),  
			Stp_PPlan_AdvReef = cast(Sum(Stp_PPlan_AdvReef) as numeric(18,1)),
			Stp_PPlan_AdvWaste = cast(Sum(Stp_PPlan_AdvWaste) as numeric(18,4)),
			Stp_PPlan_SqmSW = cast(Sum(Stp_PPlan_SqmSW) as numeric(18,4)),
			Stp_PPlan_SqmCW = cast(Sum(Stp_PPlan_SqmCW) as numeric(18,4)),  
			Stp_PPlan_SqmCmgt = cast(Sum(Stp_PPlan_SqmCmgt) as numeric(18,4)),
			Stp_PPlan_SqmCmgtTotal = cast(Sum(Stp_PPlan_SqmCmgtTotal) as numeric(18,4)),
			Stp_PPlan_FL = cast(sum(Stp_PPlan_FL) as numeric(18,4)),
			Stp_PPlan_FLReef = cast(sum(Stp_PPlan_FLReef) as numeric(18,4)),
			Stp_PPlan_FLWaste = cast(sum(Stp_PPlan_FLWaste) as numeric(18,4)),
			Stp_PPlan_Tons = cast(Sum(Stp_PPlan_Tons) as decimal(18,4)),
			Stp_PPlan_TonsReef = cast(Sum(Stp_PPlan_TonsReef) as decimal(18,4)),   
			Stp_PPlan_TonsWaste = cast(Sum(Stp_PPlan_TonsWaste) as decimal(18,4)),
			Stp_PPlan_KG = cast(Sum(Stp_PPlan_KG) as decimal(18,6)),
			Stp_PPlan_Cubics = cast(sum(Stp_PPlan_Cubics) as numeric(18,6)),
			Stp_PPlan_CubicTons = cast(sum(Stp_PPlan_CubicTons) as numeric(18,6)),
			Stp_PPlan_CubicGrams = cast(sum(Stp_PPlan_CubicGrams) as numeric(18,6)),

			Dev_PPlan_AdvReef = cast(sum(Dev_PPlan_AdvReef) as numeric(18,6)), 
			Dev_PPlan_AdvWaste = cast(sum(Dev_PPlan_AdvWaste) as numeric(18,6)),
			Dev_PPlan_Primm = cast(sum(Dev_PPlan_Primm) as numeric(18,6)),
			Dev_PPlan_Secm = cast(sum(Dev_PPlan_Secm) as numeric(18,6)), 
			Dev_PPlan_Adv = cast(sum(Dev_PPlan_Adv) as numeric(18,6)),
			Dev_PPlan_TonsReef = cast(sum(Dev_PPlan_TonsReef) as numeric(18,6)), 
			Dev_PPlan_TonsWaste = cast(sum(Dev_PPlan_TonsWaste) as numeric(18,6)), 
			Dev_PPlan_Tons = cast(sum(Dev_PPlan_Tons) as numeric(18,6)),
			Dev_PPlan_KG = cast(sum(Dev_PPlan_KG) as numeric(18,6)),
			Dev_PPlan_AdvEH = cast(sum(Dev_PPlan_AdvEH) as numeric(18,6)), 
			Dev_PPlan_AdvEW = cast(sum(Dev_PPlan_AdvEW) as numeric(18,6)),
			Dev_PPlan_AdvCmgt = cast(sum(Dev_PPlan_AdvCmgt) as numeric(18,6)),
			Dev_PPlan_AdvCmgtTotal = cast(sum(Dev_PPlan_AdvCmgtTotal) as numeric(18,6)),
			Dev_PPlan_Cubics = cast(Sum(Dev_PPlan_Cubics) as numeric(18,6)), 
			Dev_PPlan_CubicTons = cast(Sum(Dev_PPlan_CubicTons) as numeric(18,6)), 
			Dev_PPlan_CubicGrams = cast(Sum(Dev_PPlan_CubicGrams) / 1000 as numeric(18,6)), 
			Dev_PPlan_Labour = cast(Sum(Dev_PPlan_Labour) as numeric(18,6)), 
			Dev_PPlan_ShftInfo = cast(Sum(Dev_PPlan_ShftInfo) as numeric(18,6)), '
			set @SelectPart1 = '
			Dev_PPlan_DrillRig = max(Dev_PPlan_DrillRig), 

		
			Stp_LPlan_FL = cast(Sum(Stp_LPlan_FL) as numeric(18,4)),
			Stp_LPlan_FLReef = cast(Sum(Stp_LPlan_FLReef) as numeric(18,4)),
			Stp_LPlan_FLWaste = cast(Sum(Stp_LPlan_FLWaste) as numeric(18,4)),
			Stp_LPlan_Sqm = cast(Sum(Stp_LPlan_Sqm) as numeric(18,4)), 
			Stp_LPlan_SqmReef = cast(Sum(Stp_LPlan_SqmReef) as numeric(18,4)),
			Stp_LPlan_SqmWaste = cast(Sum(Stp_LPlan_SqmWaste) as numeric(18,4)),
			Stp_LPlan_Adv = cast(Sum(Stp_LPlan_Adv) as numeric(18,4)),
			Stp_LPlan_AdvReef= cast(Sum(Stp_LPlan_AdvReef) as numeric(18,4)),
			Stp_LPlan_AdvWaste = cast(Sum(Stp_LPlan_AdvWaste) as numeric(18,4)),
			Stp_LPlan_SqmSW = cast(Sum(Stp_LPlan_SqmSW) as numeric(18,4)),
			Stp_LPlan_SqmCW = cast(Sum(Stp_LPlan_SqmCW) as numeric(18,4)),
			Stp_LPlan_SqmCmgt = cast(Sum(Stp_LPlan_SqmCmgt) as numeric(18,4)),
			Stp_LPlan_SqmCmgtTotal = cast(Sum(Stp_LPlan_SqmCmgtTotal) as numeric(18,4)),
			Stp_LPlan_Tons = cast(Sum(Stp_LPlan_Tons) as numeric(18,4)),
			Stp_LPlan_TonsReef = cast(Sum(Stp_LPlan_TonsReef) as numeric(18,4)),
			Stp_LPlan_TonsWaste = cast(Sum(Stp_LPlan_TonsWaste) as numeric(18,4)),
			Stp_LPlan_Kg = cast(Sum(Stp_LPlan_Kg) as numeric(18,4)),
			Stp_LPlan_Cubics = cast(sum(Stp_LPlan_Cubics) as numeric(18,6)),
			Stp_LPlan_CubicTons = cast(sum(Stp_LPlan_CubicTons) as numeric(18,6)),
			Stp_LPlan_CubicGrams = cast(sum(Stp_LPlan_CubicGrams) as numeric(18,6)),

			Dev_LPlan_AdvReef = cast(sum(Dev_LPlan_AdvReef) as numeric(18,6)),
			Dev_LPlan_AdvWaste = cast(sum(Dev_LPlan_AdvWaste) as numeric(18,6)),
			Dev_LPlan_Primm = cast(sum(Dev_LPlan_Primm) as numeric(18,6)),
			Dev_LPlan_Secm = cast(sum(Dev_LPlan_Secm) as numeric(18,6)), 
			Dev_LPlan_Adv = cast(sum(Dev_LPlan_Adv) as numeric(18,6)),
			Dev_LPlan_TonsReef = cast(sum(Dev_LPlan_TonsReef) as numeric(18,6)),
			Dev_LPlan_TonsWaste = cast(sum(Dev_LPlan_TonsWaste) as numeric(18,6)), 
			Dev_LPlan_Tons = cast(sum( Dev_LPlan_Tons) as numeric(18,6)),
			Dev_LPlan_KG = cast(sum(Dev_LPlan_KG) as numeric(18,6)),
			Dev_LPlan_AdvEH = cast(sum(Dev_LPlan_AdvEH) as numeric(18,6)), 
			Dev_LPlan_AdvEW = cast(sum(Dev_LPlan_AdvEW) as numeric(18,6)),
			Dev_LPlan_AdvCmgt = cast(sum(Dev_LPlan_AdvCmgt) as numeric(18,6)),
			Dev_LPlan_AdvCmgtTotal = cast(sum(Dev_LPlan_AdvCmgtTotal) as numeric(18,6)),
			Dev_LPlan_Cubics = cast(Sum(Dev_LPlan_Cubics) as numeric(18,6)), 
			Dev_LPlan_CubicTons = cast(Sum(Dev_LPlan_CubicTons) as numeric(18,6)), 
			Dev_LPlan_CubicGrams = cast(Sum(Dev_LPlan_CubicGrams) / 1000 as numeric(18,6)), 
			Dev_LPlan_Labour = cast(Sum(Dev_LPlan_Labour) as numeric(18,6)), 
			Dev_LPlan_ShftInfo = cast(Sum(Dev_LPlan_ShftInfo) as numeric(18,6)), 
			Dev_LPlan_DrillRig = max(Dev_LPlan_DrillRig), 

			Stp_LPPlan_FL = cast(Sum(Stp_LPPlan_FL) as numeric(18,4)),
			Stp_LPPlan_FLReef = cast(Sum(Stp_LPPlan_FLReef) as numeric(18,4)),
			Stp_LPPlan_FLWaste = cast(Sum(Stp_LPPlan_FLWaste) as numeric(18,4)),
			Stp_LPPlan_Sqm = cast(Sum(Stp_LPPlan_Sqm) as numeric(18,4)), 
			Stp_LPPlan_SqmReef = cast(Sum(Stp_LPPlan_SqmReef) as numeric(18,4)),
			Stp_LPPlan_SqmWaste = cast(Sum(Stp_LPPlan_SqmWaste)as numeric(18,4)),
			Stp_LPPlan_Adv = cast(Sum(Stp_LPPlan_Adv) as numeric(18,4)),
			Stp_LPPlan_AdvReef = cast(Sum(Stp_LPPlan_AdvReef) as numeric(18,4)),
			Stp_LPPlan_AdvWaste = cast(Sum(Stp_LPPlan_AdvWaste) as numeric(18,4)),
			Stp_LPPlan_SqmSW = cast(Sum(Stp_LPPlan_SqmSW) as numeric(18,4)),
			Stp_LPPlan_SqmCW = cast(Sum(Stp_LPPlan_SqmCW) as numeric(18,4)),
			Stp_LPPlan_SqmCmgt = cast(Sum(Stp_LPPlan_SqmCmgt) as numeric(18,4)),
			Stp_LPPlan_SqmCmgtTotal = cast(Sum(Stp_LPPlan_SqmCmgtTotal) as numeric(18,4)),
			Stp_LPPlan_Tons = cast(Sum(Stp_LPPlan_Tons) as numeric(18,4)),
			Stp_LPPlan_TonsReef = cast(Sum(Stp_LPPlan_TonsReef) as numeric(18,4)),
			Stp_LPPlan_TonsWaste = cast(Sum(Stp_LPPlan_TonsWaste) as numeric(18,4)),
			Stp_LPPlan_Kg = cast(Sum(Stp_LPPlan_Kg) as numeric(18,4)),
			Stp_LPPlan_Cubics = cast(sum(Stp_LPPlan_Cubics) as numeric(18,6)),
			Stp_LPPlan_CubicTons = cast(sum(Stp_LPPlan_CubicTons) as numeric(18,6)),
			Stp_LPPlan_CubicGrams = cast(sum(Stp_LPPlan_CubicGrams) as numeric(18,6)),

			Dev_LPPlan_AdvReef = cast(sum(Dev_LPPlan_AdvReef) as numeric(18,6)), 
			Dev_LPPlan_AdvWaste = cast(sum(Dev_LPPlan_AdvWaste) as numeric(18,6)),
			Dev_LPPlan_Primm = cast(sum(Dev_LPPlan_Primm) as numeric(18,6)),
			Dev_LPPlan_Secm = cast(sum(Dev_LPPlan_Secm) as numeric(18,6)), 
			Dev_LPPlan_Adv = cast(sum(Dev_LPPlan_Adv) as numeric(18,6)),
			Dev_LPPlan_TonsReef = cast(sum(Dev_LPPlan_TonsReef) as numeric(18,6)), 
			Dev_LPPlan_TonsWaste = cast(sum(Dev_LPPlan_TonsWaste) as numeric(18,6)), 
			Dev_LPPlan_Tons = cast(sum(Dev_LPPlan_Tons) as numeric(18,6)),
			Dev_LPPlan_KG = cast(sum(Dev_LPPlan_KG) as numeric(18,6)),
			Dev_LPPlan_AdvEH = cast(sum(Dev_LPPlan_AdvEH) as numeric(18,6)), 
			Dev_LPPlan_AdvEW = cast(sum(Dev_LPPlan_AdvEW) as numeric(18,6)),
			Dev_LPPlan_AdvCmgt = cast(sum(Dev_LPPlan_AdvCmgt) as numeric(18,6)),
			Dev_LPPlan_AdvCmgtTotal = cast(sum(Dev_LPPlan_AdvCmgtTotal) as numeric(18,6)),
			Dev_LPPlan_Cubics = cast(Sum(Dev_LPPlan_Cubics) as numeric(18,6)), 
			Dev_LPPlan_CubicTons = cast(Sum(Dev_LPPlan_CubicTons) as numeric(18,6)), 
			Dev_LPPlan_CubicGrams = cast(Sum(Dev_LPPlan_CubicGrams) / 1000 as numeric(18,6)), 
			Dev_LPPlan_Labour = cast(Sum(Dev_LPPlan_Labour) as numeric(18,6)), 
			Dev_LPPlan_ShftInfo = cast(Sum(Dev_LPPlan_ShftInfo) as numeric(18,6)), 
			Dev_LPPlan_DrillRig = max(Dev_LPPlan_DrillRig), '

		set @SelectPart2 = '
			Stp_Book_FL = cast(sum(Stp_Book_FL) as numeric(18,4)),
			Stp_Book_Sqm = cast(Sum(Stp_Book_Sqm) as numeric(18,4)),   
			Stp_Book_SqmReef = cast(Sum(Stp_Book_SqmReef) as numeric(18,1)),
			Stp_Book_SqmWaste = cast(Sum(Stp_Book_SqmWaste) as numeric(18,4)),
			Stp_Book_Adv = cast(Sum(Stp_Book_Adv) as numeric(18,4)),   
			Stp_Book_AdvReef = cast(Sum(Stp_Book_AdvReef) as numeric(18,1)),
			Stp_Book_AdvWaste = cast(Sum(Stp_Book_AdvWaste) as numeric(18,4)),			
			Stp_Book_SqmSW = cast(sum(Stp_Book_SqmSW) as numeric(18,4)), 
			Stp_Book_SqmCW = cast(sum(Stp_Book_SqmCW) as numeric(18,4)),
			Stp_Book_SqmCmgt = cast(sum(Stp_Book_SqmCmgt) as numeric(18,4)),
			Stp_Book_SqmCmgtTotal = cast(sum(Stp_Book_SqmCmgtTotal) as numeric(18,4)),
			Stp_Book_Tons = cast(Sum(Stp_Book_Tons) as numeric(18,4)),
			Stp_Book_TonsReef = cast(Sum(Stp_Book_TonsReef) as numeric(18,4)),  
			Stp_Book_TonsWaste = cast(Sum(Stp_Book_TonsWaste) as numeric(18,4)),
			Stp_Book_Kg = cast(Sum(Stp_Book_Kg) as numeric(18,4)),
			Stp_Book_Cubics = cast(sum(Stp_Book_Cubics) as numeric(18,6)),
			Stp_Book_CubicTons = cast(sum(Stp_Book_CubicTons) as numeric(18,6)),
			Stp_Book_CubicGrams = cast(sum(Stp_Book_CubicGrams) as numeric(18,6)), 

			Dev_Book_AdvReef = cast(sum(Dev_Book_AdvReef) as numeric(18,6)), 
			Dev_Book_AdvWaste = cast(sum(Dev_Book_AdvWaste) as numeric(18,6)),
			Dev_Book_Primm = cast(sum(Dev_Book_Primm) as numeric(18,6)),
			Dev_Book_Secm = cast(sum(Dev_Book_Secm) as numeric(18,6)), 
			Dev_Book_Adv = cast(sum(Dev_Book_Adv) as numeric(18,6)),
			Dev_Book_TonsReef = cast(sum(Dev_Book_TonsReef) as numeric(18,6)),
			Dev_Book_TonsWaste = cast(sum(Dev_Book_TonsWaste) as numeric(18,6)), 
			Dev_Book_Tons = cast(sum(Dev_Book_Tons) as numeric(18,6)),
			Dev_Book_KG = cast(sum(Dev_Book_KG) as numeric(18,6)),
			Dev_Book_AdvEH = cast(sum(Dev_Book_AdvEH) as numeric(18,6)), 
			Dev_Book_AdvEW = cast(sum(Dev_Book_AdvEW) as numeric(18,6)),
			Dev_Book_AdvCmgt = cast(sum(Dev_Book_AdvCmgt) as numeric(18,6)), 
			Dev_Book_AdvCmgtTotal = cast(sum(Dev_Book_AdvCmgtTotal) as numeric(18,6)),
			Dev_Book_Cubics = cast(Sum(Dev_Book_Cubics) as numeric(18,6)), 
			Dev_Book_CubicTons = cast(Sum(Dev_Book_CubicTons) as numeric(18,6)), 
			Dev_Book_CubicGrams = cast(Sum(Dev_Book_CubicGrams) / 1000 as numeric(18,6)), 
			Dev_Book_Labour = Sum(0), 
			Dev_Book_ShftInfo = Sum(0), 
			Dev_Book_DrillRig = max(''''), 

			Stp_Meas_Sqm = cast(sum(Stp_Meas_Sqm) as numeric(18,4)),
			Stp_Meas_SqmReef = cast(sum(Stp_Meas_SqmReef) as numeric(18,1)),
			Stp_Meas_SqmWaste = cast(sum(Stp_Meas_SqmWaste) as numeric(18,4)),
			Stp_Meas_FL = cast(sum(Stp_Meas_FL) as numeric(18,4)),
			Stp_Meas_FLReef = cast(sum(Stp_Meas_FLReef) as numeric(18,4)),
			Stp_Meas_FLWaste = cast(sum(Stp_Meas_FLWaste) as numeric(18,4)),
			Stp_Meas_Adv = cast(sum(Stp_Meas_Adv) as numeric(18,4)),
			Stp_Meas_AdvReef = cast(sum(Stp_Meas_AdvReef) as numeric(18,4)),
			Stp_Meas_AdvWaste = cast(sum(Stp_Meas_AdvWaste) as numeric(18,4)),
			Stp_Meas_SqmCMGT = cast(sum(Stp_Meas_SqmCMGT) as numeric(18,4)),
			Stp_Meas_SqmCMGTTotal = cast(sum(Stp_Meas_SqmCMGTTotal) as numeric(18,4)),
			Stp_Meas_SqmSW = cast(sum(Stp_Meas_SqmSW)  as numeric(18,4)),
			Stp_Meas_SqmCW = cast(sum(Stp_Meas_SqmCW)  as numeric(18,4)),
			Stp_Meas_Tons = cast(sum(Stp_Meas_Tons) as numeric(18,4)),
			Stp_Meas_TonsReef = cast(sum(Stp_Meas_TonsReef) as numeric(18,4)),   
			Stp_Meas_TonsWaste = cast(sum(Stp_Meas_TonsWaste) as numeric(18,4)),
			Stp_Meas_Kg = cast(sum(Stp_Meas_Kg) as numeric(18,4)), 
			Stp_Meas_Cubics = cast(sum(Stp_Meas_Cubics) as numeric(18,6)),
			Stp_Meas_CubicTons = cast(sum(Stp_Meas_CubicTons) as numeric(18,6)),
			Stp_Meas_CubicGrams = cast(sum(Stp_Meas_CubicGrams) as numeric(18,6)), 
			
			Dev_Meas_AdvReef = cast(sum(Dev_Meas_AdvReef) as numeric(18,4)), 
			Dev_Meas_AdvWaste = cast(sum(Dev_Meas_AdvWaste) as numeric(18,4)),
			Dev_Meas_Primm = cast(sum(Dev_Meas_Primm) as numeric(18,4)),
			Dev_Meas_Secm = cast(sum(Dev_Meas_Secm) as numeric(18,4)), 
			Dev_Meas_Adv = cast(sum(Dev_Meas_Adv) as numeric(18,4)),
			Dev_Meas_TonsReef = cast(sum(Dev_Meas_TonsReef) as numeric(18,4)),
			Dev_Meas_TonsWaste = cast(sum(Dev_Meas_TonsWaste) as numeric(18,4)), 
			Dev_Meas_Tons = cast(sum(Dev_Meas_Tons) as numeric(18,4)),
			Dev_Meas_KG = cast(sum(Dev_Meas_KG) as numeric(18,4)),
			Dev_Meas_AdvEH = cast(sum(Dev_Meas_AdvEH) as numeric(18,4)), 
			Dev_Meas_AdvEW = cast(sum(Dev_Meas_AdvEW) as numeric(18,4)),
			Dev_Meas_AdvCmgt = cast(sum(Dev_Meas_AdvCmgt) as numeric(18,4)),
			Dev_Meas_AdvCmgtTotal = cast(sum(Dev_Meas_AdvCmgtTotal) as numeric(18,4)),
			Dev_Meas_Cubics = cast(Sum(Dev_Meas_Cubics) as numeric(18,4)), 
			Dev_Meas_CubicTons = cast(Sum(Dev_Meas_CubicTons) as numeric(18,4)), 
			Dev_Meas_CubicGrams = cast(Sum(Dev_Meas_CubicGrams) / 1000 as numeric(18,4)), '

		set @SelectPart3 = '	
			Stp_BPlan_FL = cast(sum(Stp_BPlan_FL) as numeric(18,4)),
			Stp_BPlan_Adv = cast(sum(Stp_BPlan_Adv) as numeric(18,4)),
			Stp_BPlan_AdvReef = cast(sum(Stp_BPlan_AdvReef) as numeric(18,4)),
			Stp_BPlan_AdvWaste = cast(sum(Stp_BPlan_AdvWaste) as numeric(18,4)),
			Stp_BPlan_Sqm = cast(sum(Stp_BPlan_Sqm) as numeric(18,4)),
			Stp_BPlan_SqmReef = cast(sum(Stp_BPlan_SqmReef) as numeric(18,1)),
			Stp_BPlan_SqmWaste = cast(sum(Stp_BPlan_SqmWaste) as numeric(18,4)),    
			Stp_BPlan_SqmSW = cast(sum(Stp_BPlan_SqmSW) as numeric(18,4)),
			Stp_BPlan_SqmCW = cast(sum(Stp_BPlan_SqmCW) as numeric(18,4)),
			Stp_BPlan_SqmCmgt = cast(sum(Stp_BPlan_SqmCmgt) as numeric(18,4)),
			Stp_BPlan_Tons = cast(sum(Stp_BPlan_Tons) as numeric(18,4)),   
			Stp_BPlan_TonsReef = cast(sum(Stp_BPlan_TonsReef) as numeric(18,4)),
			Stp_BPlan_TonsWaste = cast(sum(Stp_BPlan_TonsWaste) as numeric(18,4)),
			Stp_BPlan_Kg = cast(sum(Stp_BPlan_Kg) as numeric(18,4)),
			Stp_BPlan_OSSqm = cast(sum(Stp_BPlan_OSSqm) as numeric(18,4)),
			Stp_BPlan_OSFSqm = cast(sum(Stp_BPlan_OSFSqm) as numeric(18,4)),
			Stp_BPlan_REEFFL = cast(sum(Stp_BPlan_REEFFL) as numeric(18,4)),
			Stp_BPlan_OSFL = cast(sum(Stp_BPlan_OSFL) as numeric(18,4)),    
			Stp_BPlan_CUBICS = cast(sum(Stp_BPlan_CUBICS) as numeric(18,4)),
			Stp_BPlan_SqmSWFAULT = cast(sum(Stp_BPlan_SqmSWFAULT) as numeric(18,4)),  
			Stp_BPlan_SqmFault = cast(sum(Stp_BPlan_SqmFault) as numeric(18,4)),   
			Stp_BPlan_FaultTons = cast(sum(Stp_BPlan_FaultTons) as numeric(18,4)),
			Stp_BPlan_OSTons = cast(sum(Stp_BPlan_OSTons) as numeric(18,4)),
			Stp_BPlan_CUBICTons = cast(sum(Stp_BPlan_CUBICTons) as numeric(18,4)),

			Dev_BPlan_AdvReef = cast(sum(Dev_BPlan_AdvReef) as numeric(18,4)), 
			Dev_BPlan_AdvWaste = cast(sum(Dev_BPlan_AdvWaste) as numeric(18,4)),
			Dev_BPlan_Primm = cast(sum(Dev_BPlan_Primm) as numeric(18,4)),
			Dev_BPlan_Secm = cast(sum(Dev_BPlan_Secm) as numeric(18,4)), 
			Dev_BPlan_Adv = cast(sum(Dev_BPlan_Adv) as numeric(18,4)),
			Dev_BPlan_TonsReef = cast(sum(Dev_BPlan_TonsReef) as numeric(18,4)),
			Dev_BPlan_TonsWaste = cast(sum(Dev_BPlan_TonsWaste) as numeric(18,4)), 
			Dev_BPlan_Tons = cast(sum(Dev_BPlan_Tons) as numeric(18,4)),
			Dev_BPlan_KG = cast(sum(Dev_BPlan_KG) as numeric(18,4)),
			Dev_BPlan_AdvEH = cast(sum(Dev_BPlan_AdvEH) as numeric(18,4)), 
			Dev_BPlan_AdvEW = cast(sum(Dev_BPlan_AdvEW) as numeric(18,4)),
			Dev_BPlan_AdvCmgt = cast(sum(Dev_BPlan_AdvCmgt) as numeric(18,4)),
			Dev_BPlan_AdvCmgtTotal = cast(sum(Dev_BPlan_AdvCmgtTotal) as numeric(18,4)),
			Dev_BPlan_Cubics = cast(sum(Dev_BPlan_Cubics) as numeric(18,4)),
			Dev_BPlan_CubicTons = cast(sum(Dev_BPlan_CubicTons) as numeric(18,4)), 
			Dev_BPlan_TonsCmgt = cast(sum(Dev_BPlan_TonsCmgt) as numeric(18,4)),
			BusFlag = max(BusFlag),

			Abs_PlanMeas_Sqm = cast(Abs(Sum(Stp_Plan_Sqm) - Sum(Stp_Meas_Sqm))  as numeric(18,4)), 
			Abs_LockMeas_Sqm = cast(Abs(Sum(Stp_LPlan_Sqm) - Sum(Stp_Meas_Sqm)) as numeric(18,4)), 
			Abs_PlanLock_Sqm = cast(Abs(Sum(Stp_LPlan_Sqm) - Sum(Stp_Plan_Sqm)) as numeric(18,4)), 
			Abs_BookMeas_Sqm = cast(Abs(Sum(Stp_Book_Sqm) - Sum(Stp_Meas_Sqm)) as numeric(18,4)), 
			Abs_PlanBook_Sqm = cast(Abs(Sum(Stp_Book_Sqm) - Sum(Stp_Plan_Sqm)) as numeric(18,4)), 
			Abs_LockBook_Sqm = cast(Abs(Sum(Stp_Book_Sqm) - Sum(Stp_LPlan_Sqm)) as numeric(18,4)), 

			ForeCast_SQM = cast(sum(ForeCast_SQM) as numeric(18,4)),
			ForeCast_Tons = cast(sum(ForeCast_Tons) as numeric(18,4)),
			ForeCast_KG = cast(sum(ForeCast_KG) as numeric(18,4)),
			ForeCast_Cmgt = cast(case when sum(ForeCast_SQMDens) = 0 then 0 else
                    sum(ForeCast_Grams) * 100 / sum(ForeCast_SQMDens) end as numeric(18,4))
		 from   
		 ('
IF (@ReportType = 'P') or (@ReportType = 'M')
BEGIN
	IF (@PlanMonth = 'Y') or (@PlanProg = 'Y') or (@Book = 'Y')
	BEGIN
		set @PlanSelect =  
			' select 
				sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
				sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
				sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME,'

		if @TotalsPerMonth = 'Y' 
			set @PlanSelect = @PlanSelect + ' Prodmonth = cast(pm.Prodmonth as varchar(6)), '
				else 
			set @PlanSelect = @PlanSelect + ' '''' Prodmonth, '
		set @PlanSelect = @PlanSelect + '
			pm.Workplaceid+'':''+w.Description workplace,   pm.Activity,
			Plan_Stope = case when pm.Activity = 0 then 1 else 0 end, 
			Plan_Dev = case when pm.Activity = 1 then 1 else 0 end,
			TotalShifts = max(s.TotalShifts),
			ShiftNo = max(p.ShiftDay), 
			Stp_Plan_FL = max(case when '+@TheStopeLedge1+' then isnull(pm.FL,0) else 0 end),
			Stp_Plan_FLReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefFL,0) else 0 end),
			Stp_Plan_FLWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.WasteFL,0) else 0 end),
			Stp_Plan_Sqm = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) else 0 end),
			Stp_Plan_SqmReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefSqm,0) else 0 end),
			Stp_Plan_SqmWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.wasteSqm,0) else 0 end), 
			Stp_Plan_Adv = sum(case when '+@TheStopeLedge1+' then isnull(pm.MetresAdvance,0) else 0 end),
			Stp_Plan_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(pm.ReefAdv,0) else 0 end), 
			Stp_Plan_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(pm.WasteAdv,0) else 0 end),
			Stp_Plan_SqmSW = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) * isnull(pm.SW,0) else 0 end),
			Stp_Plan_SqmCW = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefSqm,0) * isnull(pm.CW,0) else 0 end),
			Stp_Plan_SqmCmgt = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefSqm,0) * isnull(pm.cmgt,0) else 0 end),
			Stp_Plan_SqmCmgtTotal = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) * isnull(pm.cmgt,0) else 0 end),
			Stp_Plan_Tons = max(case when '+@TheStopeLedge1+' then isnull(pm.Tons,0) else 0 end), 
			Stp_Plan_TonsReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefTons,0) else 0 end),
			Stp_Plan_TonsWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.WasteTons,0) else 0 end),
			Stp_Plan_Kg = max(case when '+@TheStopeLedge1+' then isnull(pm.KG,0) else 0 end), 
			Stp_Plan_Cubics = max(case when '+@TheStopeLedge1+' then isnull(pm.Cubics,0) else 0 end), 
			Stp_Plan_CubicTons = max(case when '+@TheStopeLedge1+' then isnull(pm.CubicsTons,0) else 0 end), 
			Stp_Plan_CubicGrams = max(case when '+@TheStopeLedge1+' then isnull(pm.CubicGrams,0) else 0 end), 

			Dev_Plan_AdvReef = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) else 0 end),
			Dev_Plan_AdvWaste = max(case when pm.Activity = 1 then isnull(pm.WasteAdv,0) else 0 end),
			Dev_Plan_Primm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
			Dev_Plan_Secm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end), 
			Dev_Plan_Adv = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
			Dev_Plan_TonsReef = max(case when pm.Activity = 1 then isnull(pm.ReefTons,0) else 0 end),
			Dev_Plan_TonsWaste = max(case when pm.Activity = 1 then isnull(pm.WasteTons,0) else 0 end),
			Dev_Plan_Tons = max(case when pm.Activity = 1 then isnull(pm.Tons,0) else 0 end), 
			Dev_Plan_KG = max(case when pm.Activity = 1 then isnull(pm.KG,0) else 0 end),
			Dev_Plan_AdvEH = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DHeight,0) else 0 end),
			Dev_Plan_AdvEW = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DWidth,0) else 0 end), 
			Dev_Plan_AdvCmgt = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) * isnull(pm.CMGT,0) else 0 end), 
			Dev_Plan_AdvCmgtTotal = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(pm.CMGT,0) else 0 end),
			Dev_Plan_Cubics = max(case when pm.Activity = 1 then isnull(pm.Cubics,0) else 0 end), 
			Dev_Plan_CubicTons = max(case when pm.Activity = 1 then isnull(pm.CubicsTons,0) else 0 end), 
			Dev_Plan_CubicGrams = max(case when pm.Activity = 1 then isnull(pm.CubicGrams,0) / 1000 else 0 end), 
			Dev_Plan_Labour = max(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
			Dev_Plan_ShftInfo = max(0), 
			Dev_Plan_DrillRig = max(case when pm.Activity = 1 then isnull(pm.DrillRig,'''') else '''' end), 

			-- DPLAN - Progessive Plan
			Stp_PPlan_FL = max(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
			Stp_PPlan_FLReef = max(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.ReefFL,0) else 0 end),
			Stp_PPlan_FLWaste = max(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.WasteFL,0) else 0 end),
			Stp_PPlan_Sqm = sum(case when '+@TheStopeLedge1+' then isnull(p.Sqm,0) else 0 end),
			Stp_PPlan_SqmReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefSqm,0) else 0 end), 
			Stp_PPlan_SqmWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteSqm,0) else 0 end),
			Stp_PPlan_Adv = sum(case when '+@TheStopeLedge1+' then isnull(p.MetresAdvance,0) else 0 end),
			Stp_PPlan_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefAdv,0) else 0 end), 
			Stp_PPlan_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteAdv,0) else 0 end),
			Stp_PPlan_SqmSW = sum(case when '+@TheStopeLedge1+' then isnull(pm.SW,0) * isnull(p.Sqm,0) else 0 end), 
			Stp_PPlan_SqmCW = sum(case when '+@TheStopeLedge1+' then isnull(pm.CW,0) * isnull(p.ReefSqm,0) else 0 end),
			Stp_PPlan_SqmCmgt = sum(case when '+@TheStopeLedge1+' then isnull(pm.cmgt,0) * isnull(p.ReefSqm,0) else 0 end), 
			Stp_PPlan_SqmCmgtTotal = sum(case when '+@TheStopeLedge1+' then isnull(pm.cmgt,0) * isnull(p.Sqm,0) else 0 end),
			Stp_PPlan_Tons = sum(case when '+@TheStopeLedge1+' then isnull(p.Tons,0) else 0 end),
			Stp_PPlan_TonsReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefTons,0) else 0 end), 
			Stp_PPlan_TonsWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteTons,0) else 0 end),
			Stp_PPlan_Kg = sum(case when '+@TheStopeLedge1+' then isnull(p.Grams,0) / 1000 else 0 end), 
			Stp_PPlan_Cubics = max(case when '+@TheStopeLedge1+' then isnull(p.Cubics,0) else 0 end), 
			Stp_PPlan_CubicTons = max(case when '+@TheStopeLedge1+' then isnull(p.CubicTons,0) else 0 end), 
			Stp_PPlan_CubicGrams = max(case when '+@TheStopeLedge1+' then isnull(p.CubicGrams,0) else 0 end), '

		set @PlanSelect1 = '
			Dev_PPlan_AdvReef = sum(case when p.Activity = 1 then p.ReefAdv else 0 end), 
			Dev_PPlan_AdvWaste = sum(case when p.Activity = 1 then p.WasteAdv else 0 end), 
			Dev_PPlan_Primm = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_Secm = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_Adv = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_TonsReef = sum(case when p.Activity = 1 then p.ReefTons else 0 end), 
			Dev_PPlan_TonsWaste = sum(case when p.Activity = 1 then p.WasteTons else 0 end), 
			Dev_PPlan_Tons = sum(case when p.Activity = 1 then p.Tons else 0 end), 
			Dev_PPlan_KG = sum(case when p.Activity = 1 then p.Grams / 1000 else 0 end), 
			Dev_PPlan_AdvEH = sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * pm.DHeight else 0 end), 
			Dev_PPlan_AdvEW= sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * pm.DWidth else 0 end), 
			Dev_PPlan_AdvCmgt = sum(case when p.Activity = 1 and p.ReefAdv > 0 then p.ReefAdv * p.Cmgt else 0 end), 
			Dev_PPlan_AdvCmgtTotal = sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * p.Cmgt else 0 end), 
			Dev_PPlan_Cubics = Sum(case when p.Activity = 1 then p.Cubics else 0 end), 
			Dev_PPlan_CubicTons = Sum(case when p.Activity = 1 then p.CubicTons else 0 end), 
			Dev_PPlan_CubicGrams = Sum(case when p.Activity = 1 then p.CubicGrams / 1000 else 0 end), 
			Dev_PPlan_Labour = Sum(case when p.Activity = 1 then pm.LabourStrength else 0 end), 
			Dev_PPlan_ShftInfo = Sum(0), 
			Dev_PPlan_DrillRig = max(''''), 

			Stp_Book_FL = max(case when '+@TheStopeLedge1+' then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_FLReef = max(case when '+@TheStopeLedge1+' then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_FLWaste = max(case when '+@TheStopeLedge1+' then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_Sqm = sum(case when '+@TheStopeLedge1+' then isnull(p.BookSqm,0) else 0 end),
			Stp_Book_SqmReef = sum(case when '+@TheStopeLedge1+' then isnull(p.BookReefSqm,0) else 0 end), 
			Stp_Book_SqmWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.BookWasteSqm,0) else 0 end),
			Stp_Book_Adv = sum(case when '+@TheStopeLedge1+' then isnull(p.BookMetresAdvance,0) else 0 end),
			Stp_Book_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(p.BookReefAdv,0) else 0 end), 
			Stp_Book_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.BookWasteAdv,0) else 0 end),
			Stp_Book_SqmSW = sum(case when '+@TheStopeLedge1+' then isnull(p.BookSW,0) * isnull(p.BookSqm,0) else 0 end),
			Stp_Book_SqmCW = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCW,0) * isnull(p.BookReefSqm,0) else 0 end), 
			Stp_Book_SqmCmgt = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCmgt,0) * isnull(p.BookReefSqm,0) else 0 end),
			Stp_Book_SqmCmgtTotal = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCmgt,0) * isnull(p.BookSqm,0) else 0 end),
			Stp_Book_Tons = sum(case when '+@TheStopeLedge1+' then isnull(p.BookTons,0) else 0 end), 
			Stp_Book_TonsReef = sum(case when '+@TheStopeLedge1+' then isnull(p.BookReefTons,0) else 0 end),
			Stp_Book_TonsWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.BookWasteTons,0) else 0 end), 
			Stp_Book_KG = sum(case when '+@TheStopeLedge1+' then isnull(p.BookGrams,0) / 1000 else 0 end),
			Stp_Book_Cubics = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCubics,0) else 0 end), 
			Stp_Book_CubicTons = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCubicTons,0) else 0 end), 
			Stp_Book_CubicGrams = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCubicGrams,0) else 0 end), 

			Dev_Book_AdvReef = sum(case when p.Activity = 1 then isnull(p.BookReefAdv,0) else 0 end), 
			Dev_Book_AdvWaste = sum(case when p.Activity = 1 then isnull(p.BookWasteAdv,0) else 0 end), 
			Dev_Book_Primm = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_Secm = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_Adv = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_TonsReef = sum(case when p.Activity = 1 then isnull(p.BookReefTons,0) else 0 end),
			Dev_Book_TonsWaste = sum(case when p.Activity = 1 then isnull(p.BookWasteTons,0) else 0 end), 
			Dev_Book_Tons = sum(case when p.Activity = 1 then isnull(p.BookTons,0) else 0 end), 
			Dev_Book_KG = sum(case when p.Activity = 1 then isnull(p.BookGrams,0) / 1000 else 0 end), 
			Dev_Book_AdvEH = sum(case when p.Activity = 1 and isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(pm.DHeight,0) * isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_AdvEW = sum(case when p.Activity = 1 and isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(pm.DWidth,0) * isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_AdvCmgt = sum(case when isnull(p.BookReefAdv,0) > 0  then 
						isnull(p.Bookcmgt,0) * isnull(p.BookReefAdv,0) else 0 end),
			Dev_Book_AdvCmgtTotal = sum(case when isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(p.BookMetresAdvance,0) * isnull(p.Cmgt,0) else 0 end), 
			Dev_Book_Cubics = Sum(case when p.Activity = 1 then isnull(p.BookCubicMetres,0) else 0 end), 
			Dev_Book_CubicTons = Sum(case when p.Activity = 1 then isnull(p.BookCubicTons,0) else 0 end), 
			Dev_Book_CubicGrams = Sum(case when p.Activity = 1 then isnull(p.BookCubicGrams,0) / 1000 else 0 end), 
			--Dev_Book_Labour = Sum(pm.LabourStrength), 
			--Dev_Book_ShftInfo = Sum(p.ShftInfo), 

			Stp_LPlan_FL = sum(0),
			Stp_LPlan_FLReef = sum(0),
			Stp_LPlan_FLWaste = sum(0),
			Stp_LPlan_Sqm = sum(0),
			Stp_LPlan_SqmReef = sum(0),
			Stp_LPlan_SqmWaste = sum(0), 
			Stp_LPlan_Adv = sum(0),
			Stp_LPlan_AdvReef = sum(0),
			Stp_LPlan_AdvWaste = sum(0),
			Stp_LPlan_SqmSW = sum(0),
			Stp_LPlan_SqmCW = sum(0),
			Stp_LPlan_SqmCmgt = sum(0),
			Stp_LPlan_SqmCmgtTotal = sum(0),
			Stp_LPlan_Tons = sum(0),
			Stp_LPlan_TonsReef = sum(0),
			Stp_LPlan_TonsWaste = sum(0),
			Stp_LPlan_Kg = sum(0),
			Stp_LPlan_Cubics = sum(0),
			Stp_LPlan_CubicTons = sum(0), 
			Stp_LPlan_CubicGrams = sum(0), '

		set @PlanSelect2 = '
			Dev_LPlan_AdvReef = sum(0),
			Dev_LPlan_AdvWaste = sum(0),
			Dev_LPlan_Primm = sum(0),
			Dev_LPlan_Secm = sum(0),
			Dev_LPlan_Adv = sum(0),
			Dev_LPlan_TonsReef = sum(0),
			Dev_LPlan_TonsWaste = sum(0),
			Dev_LPlan_Tons = sum(0),
			Dev_LPlan_KG = sum(0),
			Dev_LPlan_AdvEH = sum(0),
			Dev_LPlan_AdvEW = sum(0), 
			Dev_LPlan_AdvCmgt = sum(0),
			Dev_LPlan_AdvCmgtTotal = sum(0),
			Dev_LPlan_Cubics = sum(0),
			Dev_LPlan_CubicTons = sum(0),
			Dev_LPlan_CubicGrams = sum(0),
			Dev_LPlan_Labour = sum(0),
			Dev_LPlan_ShftInfo = sum(0), 
			Dev_LPlan_DrillRig = max(''''),

			Stp_LPPlan_FL = sum(0),
			Stp_LPPlan_FLReef = sum(0),
			Stp_LPPlan_FLWaste = sum(0),
			Stp_LPPlan_Sqm = sum(0),
			Stp_LPPlan_SqmReef = sum(0),
			Stp_LPPlan_SqmWaste = sum(0), 
			Stp_LPPlan_Adv = sum(0),
			Stp_LPPlan_AdvReef = sum(0), 
			Stp_LPPlan_AdvWaste = sum(0),
			Stp_LPPlan_SqmSW = sum(0),
			Stp_LPPlan_SqmCW = sum(0),
			Stp_LPPlan_SqmCmgt = sum(0),
			Stp_LPPlan_SqmCmgtTotal = sum(0),
			Stp_LPPlan_Tons = sum(0),
			Stp_LPPlan_TonsReef = sum(0),
			Stp_LPPlan_TonsWaste = sum(0),
			Stp_LPPlan_Kg = sum(0),
			Stp_LPPlan_Cubics = sum(0),
			Stp_LPPlan_CubicTons = sum(0), 
			Stp_LPPlan_CubicGrams = sum(0),

			Dev_LPPlan_AdvReef = sum(0),
			Dev_LPPlan_AdvWaste = sum(0),
			Dev_LPPlan_Primm = sum(0),
			Dev_LPPlan_Secm = sum(0),
			Dev_LPPlan_Adv = sum(0), 
			Dev_LPPlan_TonsReef = sum(0),
			Dev_LPPlan_TonsWaste = sum(0),
			Dev_LPPlan_Tons = sum(0),
			Dev_LPPlan_KG = sum(0),
			Dev_LPPlan_AdvEH = sum(0),
			Dev_LPPlan_AdvEW = sum(0),
			Dev_LPPlan_AdvCmgt = sum(0),
			Dev_LPPlan_AdvCmgtTotal = sum(0),
			Dev_LPPlan_Cubics = sum(0),
			Dev_LPPlan_CubicTons = sum(0),
			Dev_LPPlan_CubicGrams = sum(0),
			Dev_LPPlan_Labour = sum(0),
			Dev_LPPlan_ShftInfo = sum(0),
			Dev_LPPlan_DrillRig = max(''''),

			Stp_Meas_FL = sum(0),
			Stp_Meas_FLReef = sum(0),
			Stp_Meas_FLWaste = sum(0),
			Stp_Meas_Sqm = sum(0),
			Stp_Meas_SqmReef = sum(0),
			Stp_Meas_SqmWaste = sum(0),
			Stp_Meas_Adv = sum(0),
			Stp_Meas_AdvReef = sum(0),
			Stp_Meas_Advwaste = sum(0),
			Stp_Meas_SqmSW = sum(0),
			Stp_Meas_SqmCW = sum(0),
			Stp_Meas_SqmCMGT = sum(0 ),
			Stp_Meas_SqmCMGTTotal = sum(0 ),				
			Stp_Meas_Tons = sum(0), 
			Stp_Meas_TonsReef = sum(0.0), 
			Stp_Meas_TonsWaste = sum(0.0),
			Stp_Meas_Kg = sum(0), 
			Stp_Meas_Cubics = sum(0),
			Stp_Meas_CubicTons = sum(0), 
			Stp_Meas_CubicGrams = sum(0),

			Dev_Meas_AdvReef = sum(0),
			Dev_Meas_AdvWaste = sum(0),
			Dev_Meas_Primm = sum(0),
			Dev_Meas_Secm = sum(0),
			Dev_Meas_Adv = sum(0),
			Dev_Meas_TonsReef = sum(0),
			Dev_Meas_TonsWaste = sum(0), 
			Dev_Meas_Tons = sum(0),
			Dev_Meas_KG = sum(0),
			Dev_Meas_AdvEH = sum(0),
			Dev_Meas_AdvEW = sum(0),
			Dev_Meas_AdvCmgt = sum(0),
			Dev_Meas_AdvCmgtTotal = sum(0),
			Dev_Meas_Cubics = sum(0), 
			Dev_Meas_CubicTons = sum(0),
			Dev_Meas_CubicGrams = sum(0),
				
			BusFlag = max(''N''), 
			Stp_BPlan_FL = sum(0.000) ,
			Stp_BPlan_Adv = sum(0.0) ,
			Stp_BPlan_AdvReef = sum(0.0) ,
			Stp_BPlan_AdvWaste = sum(0.0) ,
			Stp_BPlan_Sqm = sum(0.0) ,
			Stp_BPlan_SqmReef = sum(0.0) ,
			Stp_BPlan_SqmWaste = sum(0.0) ,
			Stp_BPlan_SqmSW = sum(0.0) ,
			Stp_BPlan_SqmCW = sum(0.0) ,
			Stp_BPlan_SqmCmgt = sum(0.0) , 
			Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
			Stp_BPlan_Tons = sum(0) ,
			Stp_BPlan_TonsReef = sum(0.0) ,
			Stp_BPlan_TonsWaste = sum(0.0),
			Stp_BPlan_Kg = sum(0),

			Stp_BPlan_OSSqm = sum(0.0),
			Stp_BPlan_OSFSqm = sum(0.0),
			Stp_BPlan_REEFFL = sum(0.0),
			Stp_BPlan_OSFL = sum(0.0),     
			Stp_BPlan_CUBICS = sum(0),
			Stp_BPlan_SqmSWFAULT = sum(0.0),
			Stp_BPlan_SqmFault = sum(0.0),
			Stp_BPlan_FaultTons = sum(0),
			Stp_BPlan_OSTons = sum(0),
			Stp_BPlan_CUBICTons = sum(0), 

			Dev_BPlan_AdvReef = sum(0),
			Dev_BPlan_AdvWaste = sum(0),
			Dev_BPlan_Primm = sum(0),
			Dev_BPlan_Secm = sum(0), 
			Dev_BPlan_Adv = sum(0),
			Dev_BPlan_TonsReef = sum(0),
			Dev_BPlan_TonsWaste = sum(0),
			Dev_BPlan_Tons = sum(0),
			Dev_BPlan_KG = sum(0),
			Dev_BPlan_AdvEH = sum(0),
			Dev_BPlan_AdvEW = sum(0),
			Dev_BPlan_AdvCmgt = sum(0),
			Dev_BPlan_AdvCmgtTotal = sum(0),
			Dev_BPlan_Cubics = sum(0),
			Dev_BPlan_CubicTons = sum(0),
			Dev_BPlan_TonsCmgt = sum(0) ,
			
			ForeCast_SQM = case when '+@TheStopeLedge1+' and max(p.ShiftDay) > 0 then 
							sum(p.BookSQM) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end, 
			ForeCast_SQMDens = case when '+@TheStopeLedge1+'  and max(p.ShiftDay) > 0 then 
							sum(p.BookSQM) * max(pm.Density) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end, 
			ForeCast_Grams = case when '+@TheStopeLedge1+'  and max(p.ShiftDay) > 0 then 
							sum(p.BookGrams) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end,
			ForeCast_Tons = case when '+@TheStopeLedge1+' and max(p.ShiftDay) > 0 then 
							sum(p.BookTons) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end, 
			ForeCast_KG = case when '+@TheStopeLedge1+'  and max(p.ShiftDay) > 0 then 
							(sum(p.BookGrams) / 1000) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end '
		set @PlanFrom = '
		    from Planmonth pm   
			left outer join Planning p on 
				p.Prodmonth = pm.Prodmonth and 
				p.SectionID = pm.Sectionid and 
				p.WorkplaceID = pm.Workplaceid and 
				p.Activity = pm.Activity and
				p.PlanCode = pm.PlanCode and
				p.IsCubics = pm.IsCubics 
				and p. CalendarDate <= '''+@Calendardate+''' 
			inner join section_complete sc on  
				 sc.prodmonth = pm.prodmonth and  
				 sc.sectionid  = pm.sectionid
			inner join  (select distinct(ProdMonth) ProdMonth , SectionID,TotalShifts from seccal ) s on
			   s.ProdMonth = sc.prodmonth and
			   s.SectionID = sc.SectionID_1  
			inner join Workplace w on  
				 w.WorkplaceID = pm.WorkplaceID 
			inner join CommonAreaSubSections CS on
				 cs.Id = w.SubSection 
			inner join CommonAreas c on
				 c.Id = cs.CommonArea '

		SET @PlanWhere = 
			' where pm.PlanCode = ''MP'' and pm.IsCubics = ''N'' and'
		IF (@ReportType = 'P')
		BEGIN
			set @PlanWhere = @PlanWhere + ' pm.prodmonth = '''+@ProdMonth+''' '
		END
		IF (@ReportType = 'M')
		BEGIN
			set @PlanWhere = @PlanWhere + 
				' pm.prodmonth >= '''+@FromMonth+''' and 
					pm.prodmonth <= '''+@ToMonth+''' ' 
		END
			
		--IF (@TheStopeLedge = 0)
		--BEGIN
		--	set @PlanWhere = @PlanWhere + ' and pm.Activity IN(0,3) '
		--END
		--IF (@TheStopeLedge = 1)
		--BEGIN
		--	set @PlanWhere = @PlanWhere + ' and pm.Activity = 0 '
		--END
		--IF (@TheStopeLedge = 2)
		--BEGIN
		--	set @PlanWhere = @PlanWhere + ' and pm.Activity = 3 '
		--END

		set @PlanWhere = @PlanWhere + '
			and '+@ReadSection+' = '''+@SectionID+'''
			group by  
				pm.Prodmonth, sc.SectionID_5, sc.Name_5, sc.Name_4, sc.SectionID_4, 
				sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,
				sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name, 
				pm.workplaceid, w.Description, pm.Activity '
		
			
	END
	IF (@Meas = 'Y') --or ()   --) and (( @DailyPlan= '1') or (@ProgPlan = '1') or (@Book = '1')) 
	BEGIN
		IF (@PlanMonth = 'Y') or (@PlanProg = 'Y') or (@Book = 'Y')
			set @SurveyUnion = 'union all '
		set @SurveySelect = 
			' select 
				sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
				sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
				sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME, '
		if @TotalsPerMonth = 'Y' 
			set @SurveySelect = @SurveySelect + ' Prodmonth = cast(pm.Prodmonth as varchar(6)), '
				else 
			set @SurveySelect = @SurveySelect + ' '''' Prodmonth, '
		set @SurveySelect = @SurveySelect + '
				pm.Workplaceid+'':''+w.Description workplace,   pm.Activity,
				Plan_Stope = case when pm.Activity in (0,3) then 1 else 0 end, 
				Plan_Dev = case when pm.Activity in (1) then 1 else 0 end,
				TotalShifts = sum(0),
				ShiftNo = sum(0), 
				Stp_Plan_FL = sum(0), 
				Stp_Plan_FLReef = sum(0), 
				Stp_Plan_FLWaste = sum(0), 
				Stp_Plan_Sqm = sum(0),
				Stp_Plan_SqmReef = sum(0),
				Stp_Plan_SqmWaste = sum(0),
				Stp_Plan_Adv = sum(0),
				Stp_Plan_AdvReef = sum(0),
				Stp_Plan_AdvWaste = sum(0),
				Stp_Plan_SqmSW = sum(0),
				Stp_Plan_SqmCW = sum(0),
				Stp_Plan_SqmCmgt = sum(0),
				Stp_Plan_SqmCmgtTotal = sum(0),
				Stp_Plan_Tons = sum(0),
				Stp_Plan_TonsReef = sum(0),
				Stp_Plan_TonsWaste = sum(0),
				Stp_Plan_Kg = sum(0),
				Stp_Plan_Cubics = sum(0),
				Stp_Plan_CubicTons = sum(0),
				Stp_Plan_CubicGrams = sum(0),

				Dev_Plan_AdvReef = sum(0),
				Dev_Plan_AdvWaste = sum(0),
				Dev_Plan_Primm = sum(0),
				Dev_Plan_Secm = sum(0),
				Dev_Plan_Adv = sum(0),
				Dev_Plan_TonsReef = sum(0),
				Dev_Plan_TonsWaste = sum(0),
				Dev_Plan_Tons = sum(0),
				Dev_Plan_KG = sum(0),
				Dev_Plan_AdvEH = sum(0),
				Dev_Plan_AdvEW = sum(0),
				Dev_Plan_AdvCmgt = sum(0),
				Dev_Plan_AdvCmgtTotal = sum(0),
				Dev_Plan_Cubics = sum(0), 
				Dev_Plan_CubicTons = sum(0),
				Dev_Plan_CubicGrams = sum(0),
				Dev_Plan_Labour = sum(0),
				Dev_Plan_ShftInfo = sum(0),
				Dev_Plan_DrillRig = max(''''),

				Stp_PPlan_FL = sum(0), 
				Stp_PPlan_FLReef = sum(0), 
				Stp_PPlan_FLWaste = sum(0), 
				Stp_PPlan_Sqm = sum(0),
				Stp_PPlan_SqmReef = sum(0),
				Stp_PPlan_SqmWaste = sum(0),
				Stp_PPlan_Adv = sum(0),
				Stp_PPlan_AdvReef = sum(0),
				Stp_PPlan_AdvWaste = sum(0),
				Stp_PPlan_SqmSW = sum(0),
				Stp_PPlan_SqmCW = sum(0),
				Stp_PPlan_SqmCmgt = sum(0),
				Stp_PPlan_SqmCmgtTotal = sum(0),
				Stp_PPlan_Tons = sum(0),
				Stp_PPlan_TonsReef = sum(0),
				Stp_PPlan_TonsWaste = sum(0),
				Stp_PPlan_Kg = sum(0),
				Stp_PPlan_Cubics = sum(0),
				Stp_PPlan_CubicTons = sum(0),
				Stp_PPlan_CubicGrams = sum(0),

				Dev_PPlan_AdvReef = sum(0),
				Dev_PPlan_AdvWaste = sum(0), 
				Dev_PPlan_Primm = sum(0),
				Dev_PPlan_Secm = sum(0),
				Dev_PPlan_Adv = sum(0),
				Dev_PPlan_TonsReef = sum(0), 
				Dev_PPlan_TonsWaste = sum(0), 
				Dev_PPlan_Tons = sum(0), 
				Dev_PPlan_KG = sum(0),
				Dev_PPlan_AdvEH = sum(0),
				Dev_PPlan_AdvEW = sum(0),
				Dev_PPlan_AdvCmgt = sum(0),
				Dev_PPlan_AdvCmgtTotal = sum(0), 
				Dev_PPlan_Cubics = sum(0),
				Dev_PPlan_CubicTons = sum(0),
				Dev_PPlan_CubicGrams = sum(0),
				Dev_PPlan_Labour = sum(0),
				Dev_PPlan_ShftInfo = sum(0), 
				Dev_PPlan_DrillRig = max(''''), 

				Stp_Book_FL = sum(0),
				Stp_Book_FLReef = sum(0),
				Stp_Book_FLWaste = sum(0),
				Stp_Book_Sqm = sum(0),
				Stp_Book_SqmReef = sum(0), 
				Stp_Book_SqmWaste = sum(0),
				Stp_Book_Adv = sum(0),
				Stp_Book_AdvReef = sum(0), 
				Stp_Book_AdvWaste = sum(0),
				Stp_Book_SqmSW = sum(0),
				Stp_Book_SqmCW = sum(0), 
				Stp_Book_SqmCmgt = sum(0), 
				Stp_Book_SqmCmgtTotal = sum(0),
				Stp_Book_Tons = sum(0),
				Stp_Book_TonsReef = sum(0),
				Stp_Book_TonsWaste = sum(0),
				Stp_Book_KG = sum(0),
				Stp_Book_Cubics = sum(0),
				Stp_Book_CubicTons = sum(0),
				Stp_Book_CubicGrams = sum(0),

				Dev_Book_AdvReef = sum(0),
				Dev_Book_AdvWaste = sum(0),
				Dev_Book_Primm = sum(0),
				Dev_Book_Secm = sum(0), 
				Dev_Book_Adv = sum(0), 
				Dev_Book_TonsReef = sum(0),
				Dev_Book_TonsWaste = sum(0),
				Dev_Book_Tons = sum(0), 
				Dev_Book_KG = sum(0), 
				Dev_Book_AdvEH = sum(0), 
				Dev_Book_AdvEW = sum(0),
				Dev_Book_AdvCmgt = sum(0),
				Dev_Book_AdvCmgtTotal = sum(0),
				Dev_Book_Cubics = sum(0),
				Dev_Book_CubicTons = sum(0),
				Dev_Book_CubicGrams = sum(0),
				--Dev_Book_Labour = sum(0), 
				--Dev_Book_ShftInfo = sum(0),
				
				Stp_LPlan_FL = sum(0),
				Stp_LPlan_FLReef = sum(0),
				Stp_LPlan_FLWaste = sum(0),
				Stp_LPlan_Sqm = sum(0),
				Stp_LPlan_SqmReef = sum(0),
				Stp_LPlan_SqmWaste = sum(0), 
				Stp_LPlan_Adv = sum(0),
				Stp_LPlan_AdvReef = sum(0),
				Stp_LPlan_AdvWaste = sum(0),
				Stp_LPlan_SqmSW = sum(0),
				Stp_LPlan_SqmCW = sum(0),
				Stp_LPlan_SqmCmgt = sum(0),
				Stp_LPlan_SqmCmgtTotal = sum(0),
				Stp_LPlan_Tons = sum(0),
				Stp_LPlan_TonsReef = sum(0),
				Stp_LPlan_TonsWaste = sum(0),
				Stp_LPlan_Kg = sum(0),
				Stp_LPlan_Cubics = sum(0),
				Stp_LPlan_CubicTons = sum(0),
				Stp_LPlan_CubicGrams = sum(0),

				Dev_LPlan_AdvReef = sum(0),
				Dev_LPlan_AdvWaste = sum(0),
				Dev_LPlan_Primm = sum(0),
				Dev_LPlan_Secm = sum(0),
				Dev_LPlan_Adv = sum(0),
				Dev_LPlan_TonsReef = sum(0),
				Dev_LPlan_TonsWaste = sum(0),
				Dev_LPlan_Tons = sum(0),
				Dev_LPlan_KG = sum(0),
				Dev_LPlan_AdvEH = sum(0),
				Dev_LPlan_AdvEW = sum(0),
				Dev_LPlan_AdvCmgt = sum(0),
				Dev_LPlan_AdvCmgtTotal = sum(0),
				Dev_LPlan_Cubics = sum(0),
				Dev_LPlan_CubicTons = sum(0),
				Dev_LPlan_CubicGrams = sum(0),
				Dev_LPlan_Labour = sum(0),
				Dev_LPlan_ShftInfo = sum(0),
				Dev_LPlan_DrillRig = max(''''),

				Stp_LPPlan_FL = sum(0),
				Stp_LPPlan_FLReef = sum(0),
				Stp_LPPlan_FLWaste = sum(0),
				Stp_LPPlan_Sqm = sum(0),
				Stp_LPPlan_SqmReef = sum(0),
				Stp_LPPlan_SqmWaste = sum(0), 
				Stp_LPPlan_Adv = sum(0),
				Stp_LPPlan_AdvReef = sum(0), 
				Stp_LPPlan_AdvWaste = sum(0),
				Stp_LPPlan_SqmSW = sum(0),
				Stp_LPPlan_SqmCW = sum(0),
				Stp_LPPlan_SqmCmgt = sum(0),
				Stp_LPPlan_SqmCmgtTotal = sum(0),
				Stp_LPPlan_Tons = sum(0),
				Stp_LPPlan_TonsReef = sum(0),
				Stp_LPPlan_TonsWaste = sum(0),
				Stp_LPPlan_Kg = sum(0),
				Stp_LPPlan_Cubics = sum(0),
				Stp_LPPlan_CubicTons = sum(0),
				Stp_LPPlan_CubicGrams = sum(0), 
				
				Dev_LPPlan_AdvReef = sum(0),
				Dev_LPPlan_AdvWaste = sum(0),
				Dev_LPPlan_Primm = sum(0),
				Dev_LPPlan_Secm = sum(0),
				Dev_LPPlan_Adv = sum(0),
				Dev_LPPlan_TonsReef = sum(0),
				Dev_LPPlan_TonsWaste = sum(0), 
				Dev_LPPlan_Tons = sum(0),
				Dev_LPPlan_KG = sum(0),
				Dev_LPPlan_AdvEH = sum(0),
				Dev_LPPlan_AdvEW= sum(0),
				Dev_LPPlan_AdvCmgt = sum(0), 
				Dev_LPPlan_AdvCmgtTotal = sum(0), 
				Dev_LPPlan_Cubics = sum(0),
				Dev_LPPlan_CubicTons = sum(0),
				Dev_LPPlan_CubicGrams = sum(0),
				Dev_LPPlan_Labour =sum(0),
				Dev_LPPlan_ShftInfo = sum(0),
				Dev_LPPlan_DrillRig = max(''''), ' 
	
		IF (@TheStopeLedge = 0) --Stoping and Ledging
		BEGIN
			set @SurveySelect1 = @SurveySelect1 + ' 
				Stp_Meas_FL = sum(case when pm.Activity = 0 then isnull(pm.FLTotal,0) else 0 end),
				Stp_Meas_FLReef = sum(case when pm.Activity = 0 then isnull(pm.StopeFl,0) + isnull(pm.LedgeFl,0) else 0 end),
				Stp_Meas_FLWaste = sum(case when pm.Activity = 0 then isnull(pm.FLOSTotal,0) else 0 end),
				Stp_Meas_Sqm = sum(case when pm.Activity = 0 then isnull(pm.SqmTotal,0) else 0 end), 
				Stp_Meas_SqmReef =  sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0) else 0 end),
				Stp_Meas_SqmWaste = sum(case when pm.Activity = 0 then isnull(pm.SqmOSFTotal,0) + isnull(pm.SqmOSTotal,0) else 0 end),
				Stp_Meas_Adv = sum(case when pm.Activity = 0 and isnull(pm.FLTotal,0) > 0 then 
							isnull(pm.SqmTotal,0) / isnull(pm.FLTotal,0) else 0 end),
				Stp_Meas_AdvReef =  sum(case when pm.Activity = 0 and (isnull(pm.StopeFl,0) + isnull(pm.LedgeFl,0)) > 0 then 
							(isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0)) / (isnull(pm.StopeFl,0) + isnull(pm.LedgeFl,0)) else 0 end),
				Stp_Meas_AdvWaste = sum(case when pm.Activity = 0 and isnull(pm.FLOSTotal,0) > 0 then 
							(isnull(pm.SqmOSFTotal,0) + isnull(pm.SqmOSTotal,0)) / isnull(pm.FLOSTotal,0) else 0 end),
				Stp_Meas_SqmSW = sum(case when pm.Activity = 0 then isnull(pm.SqmTotal,0) * isnull(pm.SWSqm,0) else 0 end),
				Stp_Meas_SqmCW = sum(case when pm.Activity = 0 then 
						(isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0) + isnull(pm.SqmConvTotal,0)) * isnull(pm.CW,0) else 0 end),
				Stp_Meas_SqmCMGT = sum(case when pm.Activity = 0 then 
						(isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0) + isnull(pm.SqmConvTotal,0)) * isnull(pm.cmgt,0) else 0 end),
				Stp_Meas_SqmCMGTTotal = sum(case when pm.Activity = 0 then 
						(isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0) + isnull(pm.SqmConvTotal,0)) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_Tons = sum(case when pm.Activity = 0 then isnull(pm.TonsTotal,0) else 0 end),
				Stp_Meas_TonsReef = sum(case when pm.Activity = 0 then isnull(pm.TonsReef,0) else 0 end),
				Stp_Meas_TonsWaste = sum(case when pm.Activity = 0 then 
							isnull(pm.TonsOS,0) + isnull(pm.TonsOSF,0) + isnull(pm.TonsWaste,0) else 0 end),
				Stp_Meas_Kg = sum(case when pm.Activity = 0 and isnull(pm.SWSqm,0) > 0 then 
					(
						((isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0)) * isnull(pm.cmgt,0) * isnull(pm.Density,0) / 100) + 
						(isnull(pm.CubicsReef,0) * isnull(pm.Cmgt,0) / isnull(pm.SWSqm,0) * isnull(pm.Density,0))
					) / 1000 else 0 end), '
		END
		IF (@TheStopeLedge = 1)
		BEGIN
			set @SurveySelect1 = @SurveySelect1 + ' 
				Stp_Meas_FL = sum(case when pm.Activity = 0 then isnull(pm.StopeFL,0) + isnull(pm.StopeFLOS,0) else 0 end),
				Stp_Meas_FLReef = sum(case when pm.Activity = 0 then isnull(pm.StopeFl,0) else 0 end),
				Stp_Meas_FLWaste = sum(case when pm.Activity = 0 then isnull(pm.StopeFLOS,0) else 0 end),
				Stp_Meas_Sqm = sum(case when pm.Activity = 0 then isnull(pm.StopeSqmTotal,0) else 0 end), 
				Stp_Meas_SqmReef = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) else 0 end),
				Stp_Meas_SqmWaste = sum(case when pm.Activity = 0 then isnull(pm.StopeSqmOSF,0) + isnull(pm.StopeSqmOS,0) else 0 end),
				Stp_Meas_Adv = sum(case when pm.Activity = 0 and (isnull(pm.StopeFL,0) + isnull(pm.StopeFLOS,0)) > 0 then 
							isnull(pm.StopeSqmTotal,0) / (isnull(pm.StopeFL,0) + isnull(pm.StopeFLOS,0)) else 0 end),
				Stp_Meas_AdvReef =  sum(case when pm.Activity = 0 and isnull(pm.StopeFl,0) > 0 then 
							isnull(pm.StopeSqm,0) / isnull(pm.StopeFl,0) else 0 end),
				Stp_Meas_AdvWaste = sum(case when pm.Activity = 0 and isnull(pm.StopeFLOS,0) > 0 then 
							isnull(pm.StopeSqmOSF,0) + isnull(pm.StopeSqmOS,0) / isnull(pm.StopeFLOS,0) else 0 end),
				Stp_Meas_SqmSW = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) * isnull(pm.SWSqm,0) else 0 end),
				Stp_Meas_SqmCW = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) * isnull(pm.CW,0) else 0 end),
				Stp_Meas_SqmCMGT = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_SqmCMGTTotal = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_Tons = sum(case when pm.Activity = 0 then 
							isnull(StopeSqmTotal,0) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_TonsReef = sum(case when pm.Activity = 0 then 
							isnull(StopeSqm,0) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Sensity,0) else 0 end),
				Stp_Meas_TonsWaste = sum(case when pm.Activity = 0 then 
							(isnull(pm.StopeSqmOSF,0) + isnull(pm.StopeSqmOS,0)) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_Kg = sum(case when pm.Activity = 0 and isnull(pm.SWSqm,0) > 0 then 
						(
							(isnull(pm.StopeSqm,0) * isnull(pm.cmgt,0) * isnull(pm.Density,0) / 100) + 
							(isnull(pm.CubicsReef,0) * isnull(pm.Cmgt,0) / isnull(pm.SWSqm,0) * isnull(pm.Density,0))
						) / 1000 else 0 end),'
		END
		IF (@TheStopeLedge = 2)	
		BEGIN
			set @SurveySelect1 = @SurveySelect1 + ' 
				Stp_Meas_FL = sum(case when pm.Activity = 0 then isnull(pm.LedgeFL,0) + isnull(pm.LedgeFLOS,0) else 0 end),
				Stp_Meas_FLReef = sum(case when pm.Activity = 0 then isnull(pm.LedgeFl,0) else 0 end),
				Stp_Meas_FLWaste = sum(case when pm.Activity = 0 then isnull(pm.LedgeFLOS,0) else 0 end),
				Stp_Meas_Sqm = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqmTotal,0) else 0 end), 
				Stp_Meas_SqmReef = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) else 0 end),
				Stp_Meas_SqmWaste = sum(case when pm.Activity = 0 then 
							isnull(pm.LedgeSqmOSF,0) + isnull(pm.LedgeSqmOS,0) else 0 end),
				Stp_Meas_Adv = sum(case when pm.Activity = 0 and (isnull(pm.LedgeFL,0) + isnull(pm.LedgeFLOS,0)) > 0 then 
							isnull(pm.LedgeSqmTotal,0) / (isnull(pm.LedgeFL,0) + isnull(pm.LedgeFLOS,0)) else 0 end),
				Stp_Meas_AdvReef =  sum(case when pm.Activity = 0 and isnull(pm.LedgeFl,0) > 0 then 
							isnull(pm.LedgeSqm,0) / isnull(pm.LedgeFl,0) else 0 end),
				Stp_Meas_AdvWaste = sum(case when pm.Activity = 0 and isnull(pm.LedgeFLOS,0) > 0 then 
							(isnull(pm.LedgeSqmOSF,0) + isnull(pm.LedgeSqmOS,0)) / isnull(pm.LedgeFLOS,0) else 0 end),
				Stp_Meas_SqmSW = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) * isnull(pm.SWSqm,0) else 0 end),
				Stp_Meas_SqmCW = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) * isnull(pm.CW,0) else 0 end),
				Stp_Meas_SqmCMGT = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_SqmCMGTTotal = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_Tons = sum(case when pm.Activity = 0 then 
							isnull(LedgeSqmTotal,0) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_OnTons = sum(case when pm.Activity = 0 then 
							isnull(LedgeSqm,0) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_OffTons = sum(case when pm.Activity = 0 then 
							(isnull(pm.LedgeSqmOSF,0) + isnull(pm.LedgeSqmOS,0)) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_Kg = sum(case when pm.Activity = 0 and isnull(pm.SWSqm,0) > 0 then 
					(
						(isnull(pm.LedgeSqm,0) * isnull(pm.cmgt,0) * isnull(pm.Density,0) / 100) + 
						(isnull(pm.CubicsReef,0) * isnull(pm.Cmgt,0) / isnull(pm.SWSqm,0) * isnull(pm.StpDensity,0))
					) / 1000 else 0 end),'
		END
		set @SurveySelect1 = @SurveySelect1 + '
				Stp_Meas_Cubics = sum(0), 
				Stp_Meas_CubicTons = sum(0),
				Stp_Meas_CubicGrams = sum(0),

				Dev_Meas_AdvReef = sum(case when pm.Activity = 1 then isnull(pm.ReefMetres,0) else 0 end), 
				Dev_Meas_AdvWaste = sum(case when pm.Activity = 1 then isnull(pm.WasteMetres,0) else 0 end), 
				Dev_Meas_Primm = sum(case when pm.Activity = 1 then isnull(pm.ReefMetres + pm.WasteMetres,0) else 0 end), 
				Dev_Meas_Secm = sum(0.0), 
				Dev_Meas_Adv = sum(case when pm.Activity = 1 then isnull(pm.ReefMetres,0) + isnull(pm.WasteMetres,0) else 0 end), 
				Dev_Meas_TonsReef = sum(case when pm.Activity = 1 then isnull(TonsReef,0) else 0 end), 
				Dev_Meas_TonsWaste = sum(case when pm.Activity = 1 then isnull(TonsWaste,0) else 0 end), 
				Dev_Meas_Tons = sum(case when pm.Activity = 1 then isnull(TonsWaste,0) + isnull(TonsReef,0) else 0 end), 
				Dev_Meas_KG = sum(case when pm.Activity = 1 then isnull(pm.TotalContent,0) / 1000 else 0 end), 
				Dev_Meas_AdvEH = sum(case when pm.Activity = 1 then isnull(pm.MainMetres,0) * isnull(pm.MeasHeight,0) else 0 end), 
				Dev_Meas_AdvEW = sum(case when pm.Activity = 1 then isnull(pm.MainMetres,0) * isnull(pm.MeasWidth,0) else 0 end), 
				Dev_Meas_AdvCmgt = sum(case when pm.Activity = 1 then isnull(pm.ReefMetres,0) * isnull(pm.cmgt,0) else 0 end),
				Dev_Meas_AdvCmgtTotal = sum(0),
				Dev_Meas_Cubics = sum(0), 
				Dev_Meas_CubicTons = sum(0),
				Dev_Meas_CubicGrams = sum(0),

				BusFlag = max(''N''), 
				Stp_BPlan_FL = sum(0.000) ,
				Stp_BPlan_Adv = sum(0.0) ,
				Stp_BPlan_AdvReef = sum(0.0) ,
				Stp_BPlan_AdvWaste = sum(0.0) ,
				Stp_BPlan_Sqm = sum(0.0) ,
				Stp_BPlan_SqmReef = sum(0.0) ,
				Stp_BPlan_SqmWaste = sum(0.0) ,
				Stp_BPlan_SqmSW = sum(0.0) ,
				Stp_BPlan_SqmCW = sum(0.0) ,
				Stp_BPlan_SqmCmgt = sum(0.0) , 
				Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
				Stp_BPlan_Tons = sum(0) ,
				Stp_BPlan_TonsReef = sum(0.0) ,
				Stp_BPlan_TonsWaste = sum(0.0),
				Stp_BPlan_Kg = sum(0),

				Stp_BPlan_OSSqm = sum(0.0),
				Stp_BPlan_OSFSqm = sum(0.0),
				Stp_BPlan_REEFFL = sum(0.0),
				Stp_BPlan_OSFL = sum(0.0),     
				Stp_BPlan_CUBICS = sum(0),
				Stp_BPlan_SqmSWFAULT = sum(0.0),
				Stp_BPlan_SqmFault = sum(0.0),
				Stp_BPlan_FaultTons = sum(0),
				Stp_BPlan_OSTons = sum(0),
				Stp_BPlan_CUBICTons = sum(0),

				Dev_BPlan_AdvReef = sum(0),
				Dev_BPlan_AdvWaste = sum(0),
				Dev_BPlan_Primm = sum(0),
				Dev_BPlan_Secm = sum(0), 
				Dev_BPlan_Adv = sum(0),
				Dev_BPlan_TonsReef = sum(0),
				Dev_BPlan_TonsWaste = sum(0),
				Dev_BPlan_Tons = sum(0),
				Dev_BPlan_KG = sum(0),
				Dev_BPlan_AdvEH = sum(0),
				Dev_BPlan_AdvEW =sum(0),
				Dev_BPlan_AdvCmgt = sum(0),
				Dev_BPlan_AdvCmgtTotal = sum(0),
				Dev_BPlan_Cubics = sum(0),
				Dev_BPlan_CubicTons = sum(0),
				Dev_BPlan_TonsCmgt = sum(0) ,
				
				ForeCast_SQM = sum(0) ,
				ForeCast_SQMDens = sum(0) ,
				ForeCast_Grams = sum(0) ,
				ForeCast_Tons = sum(0),
				ForeCast_KG = sum(0) '
		set @SurveyFrom = '
			from Survey pm 
			inner join section_complete sc on 
				pm.PRODMONTH = sc.PRODMONTH and 
				pm.SECTIONID = sc.sectionID 
			inner join workplace w on 
				pm.WorkplaceID = w.WorkplaceID  
			inner join CommonAreaSubSections CS on
				cs.Id = w.SubSection 
			inner join CommonAreas c on
				c.Id = cs.CommonArea '
		SET @SurveyWhere = ' where '
				  
		IF (@ReportType = 'P')
		BEGIN
			set @SurveyWhere = @SurveyWhere + ' pm.prodmonth = '''+@ProdMonth+''' and ' 
		END
		IF (@ReportType = 'M')
		BEGIN
			set @SurveyWhere = @SurveyWhere + 
				' pm.prodmonth >= '''+@FromMonth+''' and 
				  pm.prodmonth <= '''+@ToMonth+''' and ' 
		END
		set @SurveyWhere = @SurveyWhere + '
				'+@ReadSection+' = '''+@SectionID+''' '

		--IF (@TheStopeLedge = 0)
		--BEGIN
		--	set @SurveyWhere = @SurveyWhere + ' pm.Activity = 0 '
		--END
		--IF (@TheStopeLedge = 1)
		--BEGIN
		--	set @SurveyWhere = @SurveyWhere + ' pm.Activity = 0 '
		--END
		--IF (@TheStopeLedge = 2)
		--BEGIN
		--	set @SurveyWhere = @SurveyWhere + ' pm.Activity IN (0,3) and  LedgeSqmTotal <>0 '
		--END

		set @SurveyWhere = @SurveyWhere + '
			group by  
				pm.Prodmonth, sc.SectionID_5, sc.Name_5, sc.Name_4, sc.SectionID_4, 
				sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,
				sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name, 
				pm.workplaceid, w.Description, pm.Activity '

	
	END
	IF (@PlanMonthLock = 'Y') or (@PlanProgLock = 'Y')
	BEGIN
		IF (@PlanMonth = 'Y') or (@PlanProg = 'Y') or (@Book = 'Y') or (@Meas = 'Y') 
			set @LPlanUnion = 'union all '
		set @LPlanSelect =  
			' select 
				sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
				sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
				sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME, '
		if @TotalsPerMonth = 'Y' 
			set @LPlanSelect = @LPlanSelect + ' Prodmonth = cast(pm.Prodmonth as varchar(6)), '
				else 
			set @LPlanSelect = @LPlanSelect + ' '''' Prodmonth, '
		set @LPlanSelect = @LPlanSelect + '
				pm.Workplaceid+'':''+w.Description workplace,   pm.Activity,
				Plan_Stope = case when pm.Activity in (0,3) then 1 else 0 end, 
				Plan_Dev = case when pm.Activity in (1) then 1 else 0 end,
				TotalShifts = max(s.TotalShifts),
				ShiftNo = max(p.ShiftDay), 
				Stp_Plan_FL = sum(0),
				Stp_Plan_FLReef = sum(0),
				Stp_Plan_FLWaste = sum(0),
				Stp_Plan_Sqm = sum(0),
				Stp_Plan_SqmReef = sum(0),
				Stp_Plan_SqmWaste = sum(0), 
				Stp_Plan_Adv = sum(0),
				Stp_Plan_AdvReef = sum(0),
				Stp_Plan_AdvWaste = sum(0),
				Stp_Plan_SqmSW = sum(0),
				Stp_Plan_SqmCW = sum(0),
				Stp_Plan_SqmCmgt = sum(0),
				Stp_Plan_SqmCmgtTotal = sum(0),
				Stp_Plan_Tons = sum(0),
				Stp_Plan_TonsReef = sum(0),
				Stp_Plan_TonsWaste = sum(0),
				Stp_Plan_Kg = sum(0),
				Stp_Plan_Cubics = sum(0), 
				Stp_Plan_CubicTons = sum(0),
				Stp_Plan_CubicGrams = sum(0),

				Dev_Plan_AdvReef = sum(0),
				Dev_Plan_AdvWaste = sum(0),
				Dev_Plan_Primm = sum(0),
				Dev_Plan_Secm = sum(0),
				Dev_Plan_Adv = sum(0),
				Dev_Plan_TonsReef = sum(0),
				Dev_Plan_TonsWaste = sum(0),
				Dev_Plan_Tons = sum(0),
				Dev_Plan_KG = sum(0),
				Dev_Plan_AdvEH = sum(0),
				Dev_Plan_AdvEW = sum(0),
				Dev_Plan_AdvCmgt = sum(0),
				Dev_Plan_AdvCmgtTotal = sum(0),
				Dev_Plan_Cubics = sum(0), 
				Dev_Plan_CubicTons = sum(0),
				Dev_Plan_CubicGrams = sum(0),
				Dev_Plan_Labour = sum(0),
				Dev_Plan_ShftInfo = sum(0),
				Dev_Plan_DrillRig = max(''''),

				Stp_PPlan_FL = sum(0),
				Stp_PPlan_FLReef = sum(0),
				Stp_PPlan_FLWaste = sum(0),
				Stp_PPlan_Sqm = sum(0),
				Stp_PPlan_SqmReef = sum(0),
				Stp_PPlan_SqmWaste = sum(0), 
				Stp_PPlan_Adv = sum(0),
				Stp_PPlan_AdvReef = sum(0), 
				Stp_PPlan_AdvWaste = sum(0),
				Stp_PPlan_SqmSW = sum(0),
				Stp_PPlan_SqmCW = sum(0),
				Stp_PPlan_SqmCmgt = sum(0),
				Stp_PPlan_SqmCmgtTotal = sum(0),
				Stp_PPlan_Tons = sum(0),
				Stp_PPlan_TonsReef = sum(0),
				Stp_PPlan_TonsWaste = sum(0),
				Stp_PPlan_Kg = sum(0),
				Stp_PPlan_Cubics = sum(0), 
				Stp_PPlan_CubicTons = sum(0),
				Stp_PPlan_CubicGrams = sum(0),

				Dev_PPlan_AdvReef = sum(0),
				Dev_PPlan_AdvWaste = sum(0), 
				Dev_PPlan_Primm = sum(0),
				Dev_PPlan_Secm = sum(0),
				Dev_PPlan_Adv = sum(0),
				Dev_PPlan_TonsReef = sum(0), 
				Dev_PPlan_TonsWaste = sum(0), 
				Dev_PPlan_Tons = sum(0), 
				Dev_PPlan_KG = sum(0),
				Dev_PPlan_AdvEH = sum(0),
				Dev_PPlan_AdvEW = sum(0),
				Dev_PPlan_AdvCmgt = sum(0),
				Dev_PPlan_AdvCmgtTotal = sum(0),
				Dev_PPlan_Cubics = sum(0),
				Dev_PPlan_CubicTons = sum(0),
				Dev_PPlan_CubicGrams = sum(0),
				Dev_PPlan_Labour = sum(0),
				Dev_PPlan_ShftInfo = sum(0), 
				Dev_PPlan_DrillRig = max(''''), 

				Stp_Book_FL = sum(0),
				Stp_Book_FLReef = sum(0),
				Stp_Book_FLWaste = sum(0),
				Stp_Book_Sqm = sum(0),
				Stp_Book_SqmReef = sum(0),
				Stp_Book_SqmWaste = sum(0),
				Stp_Book_Adv = sum(0),
				Stp_Book_AdvReef = sum(0),
				Stp_Book_AdvWaste = sum(0),
				Stp_Book_SqmSW = sum(0),
				Stp_Book_SqmCW = sum(0), 
				Stp_Book_SqmCmgt = sum(0),
				Stp_Book_SqmCmgtTotal = sum(0),
				Stp_Book_Tons = sum(0),
				Stp_Book_TonsReef = sum(0),
				Stp_Book_TonsWaste = sum(0), 
				Stp_Book_KG = sum(0),
				Stp_Book_Cubics = sum(0), 
				Stp_Book_CubicTons = sum(0),
				Stp_Book_CubicGrams = sum(0),

				Dev_Book_AdvReef = sum(0),
				Dev_Book_AdvWaste = sum(0),
				Dev_Book_Primm = sum(0),
				Dev_Book_Secm = sum(0), 
				Dev_Book_Adv = sum(0), 
				Dev_Book_TonsReef = sum(0),
				Dev_Book_TonsWaste = sum(0),
				Dev_Book_Tons = sum(0), 
				Dev_Book_KG = sum(0), 
				Dev_Book_AdvEH = sum(0), 
				Dev_Book_AdvEW = sum(0),
				Dev_Book_AdvCmgt = sum(0),
				Dev_Book_AdvCmgtTotal = sum(0),
				Dev_Book_Cubics = sum(0),
				Dev_Book_CubicTons = sum(0),
				Dev_Book_CubicGrams = sum(0),
				--Dev_Book_Labour = sum(0), 
				--Dev_Book_ShftInfo = sum(0),

				Stp_LPlan_FL = max(case when '+@TheStopeLedge1+' then isnull(pm.FL,0) else 0 end),
				Stp_LPlan_FLReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefFL,0) else 0 end),
				Stp_LPlan_FLWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.WasteFL,0) else 0 end),
				Stp_LPlan_Sqm = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) else 0 end),
				Stp_LPlan_SqmReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefSqm,0) else 0 end),
				Stp_LPlan_SqmWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.wasteSqm,0) else 0 end), 
				Stp_LPlan_Adv = sum(case when '+@TheStopeLedge1+' then isnull(pm.MetresAdvance,0) else 0 end),
				Stp_LPlan_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(pm.ReefAdv,0) else 0 end), 
				Stp_LPlan_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(pm.WasteAdv,0) else 0 end),
				Stp_LPlan_SqmSW = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) * isnull(pm.SW,0) else 0 end),
				Stp_LPlan_SqmCW = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) * isnull(pm.CW,0) else 0 end),
				Stp_LPlan_SqmCmgt = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefSqm,0) * isnull(pm.cmgt,0) else 0 end),
				Stp_LPlan_SqmCmgtTotal = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) * isnull(pm.cmgt,0) else 0 end),
				Stp_LPlan_Tons = max(case when '+@TheStopeLedge1+' then isnull(pm.Tons,0) else 0 end), 
				Stp_LPlan_TonsReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefTons,0) else 0 end),
				Stp_LPlan_TonsWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.WasteTons,0) else 0 end),
				Stp_LPlan_Kg = sum(case when '+@TheStopeLedge1+' then isnull(pm.KG,0) else 0 end),
				Stp_LPlan_Cubics = max(case when '+@TheStopeLedge1+' then isnull(pm.Cubics,0) else 0 end), 
				Stp_LPlan_CubicTons = max(case when '+@TheStopeLedge1+' then isnull(pm.CubicsTons,0) else 0 end), 
				Stp_LPlan_CubicGrams = max(case when '+@TheStopeLedge1+' then isnull(pm.CubicGrams,0) else 0 end), ' 

			set @LPlanSelect1 = '
				Dev_LPlan_AdvReef = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) else 0 end),
				Dev_LPlan_AdvWaste = max(case when pm.Activity = 1 then isnull(pm.WasteAdv,0) else 0 end),
				Dev_LPlan_Primm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
				Dev_LPlan_Secm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end), 
				Dev_LPlan_Adv = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
				Dev_LPlan_TonsReef = max(case when pm.Activity = 1 then isnull(pm.ReefTons,0) else 0 end),
				Dev_LPlan_TonsWaste = max(case when pm.Activity = 1 then isnull(pm.WasteTons,0) else 0 end),
				Dev_LPlan_Tons = max(case when pm.Activity = 1 then isnull(pm.Tons,0) else 0 end), 
				Dev_LPlan_KG = max(case when pm.Activity = 1 then isnull(pm.KG,0) else 0 end),
				Dev_LPlan_AdvEH = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DHeight,0) else 0 end),
				Dev_LPlan_AdvEW = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DWidth,0) else 0 end), 
				Dev_LPlan_AdvCmgt = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) * isnull(pm.CMGT,0) else 0 end), 
				Dev_LPlan_AdvCmgtTotal = sum(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(pm.CMGT,0) else 0 end),
				Dev_LPlan_Cubics = Sum(case when pm.Activity = 1 then isnull(pm.Cubics,0) else 0 end), 
				Dev_LPlan_CubicTons = Sum(case when pm.Activity = 1 then isnull(pm.CubicsTons,0) else 0 end), 
				Dev_LPlan_CubicGrams = Sum(case when pm.Activity = 1 then isnull(pm.CubicGrams,0) / 1000 else 0 end), 
				Dev_LPlan_Labour = Sum(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
				Dev_LPlan_ShftInfo = Sum(0), 
				Dev_LPlan_DrillRig = max(''''), 
				
				-- DPLAN - Progessive Plan
				Stp_LPPlan_FL = max(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
				Stp_LPPlan_FLReef = max(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
				Stp_LPPlan_FLWaste = max(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
				Stp_LPPlan_Sqm = sum(case when '+@TheStopeLedge1+' then isnull(p.Sqm,0) else 0 end),
				Stp_LPPlan_SqmReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefSqm,0) else 0 end), 
				Stp_LPPlan_SqmWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteSqm,0) else 0 end),
				Stp_LPPlan_Adv = sum(case when '+@TheStopeLedge1+' then isnull(p.MetresAdvance,0) else 0 end),
				Stp_LPPlan_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefAdv,0) else 0 end), 
				Stp_LPPlan_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteAdv,0) else 0 end),
				Stp_LPPlan_SqmSW = sum(case when '+@TheStopeLedge1+' then isnull(pm.SW,0) * isnull(p.Sqm,0) else 0 end), 
				Stp_LPPlan_SqmCW = sum(case when '+@TheStopeLedge1+' then isnull(pm.CW,0) * isnull(p.Sqm,0) else 0 end),
				Stp_LPPlan_SqmCmgt = sum(case when '+@TheStopeLedge1+' then isnull(pm.cmgt,0) * isnull(p.ReefSqm,0) else 0 end), 
				Stp_LPPlan_SqmCmgtTotal = sum(case when '+@TheStopeLedge1+' then isnull(pm.cmgt,0) * isnull(p.Sqm,0) else 0 end),
				Stp_LPPlan_Tons = sum(case when '+@TheStopeLedge1+' then isnull(p.Tons,0) else 0 end),
				Stp_LPPlan_TonsReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefTons,0) else 0 end), 
				Stp_LPPlan_TonsWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteTons,0) else 0 end),
				Stp_LPPlan_Kg = sum(case when '+@TheStopeLedge1+' then isnull(p.Grams,0) / 1000 else 0 end),
				Stp_LPPlan_Cubics = max(case when '+@TheStopeLedge1+' then isnull(p.Cubics,0) else 0 end), 
				Stp_LPPlan_CubicTons = max(case when '+@TheStopeLedge1+' then isnull(p.CubicTons,0) else 0 end), 
				Stp_LPPlan_CubicGrams = max(case when '+@TheStopeLedge1+' then isnull(p.CubicGrams,0) else 0 end), 
					
				Dev_LPPlan_AdvReef = sum(case when pm.Activity = 1 then isnull(p.ReefAdv,0) else 0 end), 
				Dev_LPPlan_AdvWaste = sum(case when pm.Activity = 1 then isnull(p.WasteAdv,0) else 0 end), 
				Dev_LPPlan_Primm = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
				Dev_LPPlan_Secm = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
				Dev_LPPlan_Adv = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
				Dev_LPPlan_TonsReef = sum(case when pm.Activity = 1 then isnull(p.ReefTons,0) else 0 end), 
				Dev_LPPlan_TonsWaste = sum(case when pm.Activity = 1 then isnull(p.WasteTons,0) else 0 end), 
				Dev_LPPlan_Tons = sum(case when pm.Activity = 1 then isnull(p.Tons,0) else 0 end), 
				Dev_LPPlan_KG = sum(case when pm.Activity = 1 then isnull(p.Grams,0) / 1000 else 0 end), 
				Dev_LPPlan_AdvEH = sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
							isnull(p.MetresAdvance,0) * isnull(pm.DHeight,0) else 0 end), 
				Dev_LPPlan_AdvEW= sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
							isnull(p.MetresAdvance,0) * isnull(pm.DWidth,0) else 0 end), 
				Dev_LPPlan_AdvCmgt = sum(case when pm.Activity = 1 and isnull(p.ReefAdv,0) > 0 then 
							isnull(p.ReefAdv,0) * isnull(p.Cmgt,0) else 0 end), 
				Dev_LPPlan_AdvCmgtTotal = sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
							isnull(p.MetresAdvance,0) * isnull(p.Cmgt,0) else 0 end), 
				Dev_LPPlan_Cubics = Sum(case when pm.Activity = 1 then isnull(p.Cubics,0) else 0 end), 
				Dev_LPPlan_CubicTons = Sum(case when pm.Activity = 1 then isnull(p.CubicTons,0) else 0 end), 
				Dev_LPPlan_CubicGrams = Sum(case when pm.Activity = 1 then isnull(p.CubicGrams,0) / 1000 else 0 end), 
				Dev_LPPlan_Labour = Sum(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
				Dev_LPPlan_ShftInfo = Sum(0), 
				Dev_LPPlan_DrillRig = max(''''),  '

			set @LPlanSelect2 = '
				Stp_Meas_FL = sum(0),
				Stp_Meas_FLReef = sum(0),
				Stp_Meas_FLWaste = sum(0),
				Stp_Meas_Sqm = sum(0),
				Stp_Meas_SqmReef = sum(0),
				Stp_Meas_SqmWaste = sum(0),
				Stp_Meas_Adv = sum(0),
				Stp_Meas_AdvReef = sum(0),
				Stp_Meas_Advwaste = sum(0),
				Stp_Meas_SqmSW = sum(0),
				Stp_Meas_SqmCW = sum(0),
				Stp_Meas_SqmCMGT = sum(0 ),
				Stp_Meas_SqmCMGTTotal = sum(0 ),				
				Stp_Meas_Tons = sum(0), 
				Stp_Meas_TonsReef = sum(0.0), 
				Stp_Meas_TonsWaste = sum(0.0),
				Stp_Meas_Kg = sum(0), 
				Stp_Meas_Cubics = sum(0),
				Stp_Meas_CubicTons = sum(0), 
				Stp_Meas_CubicGrams = sum(0),

				Dev_Meas_AdvReef = sum(0),
				Dev_Meas_AdvWaste = sum(0),
				Dev_Meas_Primm = sum(0),
				Dev_Meas_Secm = sum(0),
				Dev_Meas_Adv = sum(0),
				Dev_Meas_TonsReef = sum(0),
				Dev_Meas_TonsWaste = sum(0), 
				Dev_Meas_Tons = sum(0),
				Dev_Meas_KG = sum(0),
				Dev_Meas_AdvEH = sum(0),
				Dev_Meas_AdvEW = sum(0),
				Dev_Meas_AdvCmgt = sum(0),
				Dev_Meas_AdvCmgtTotal = sum(0),
				Dev_Meas_Cubics = sum(0), 
				Dev_Meas_CubicTons = sum(0),
				Dev_Meas_CubicGrams = sum(0),
				
				BusFlag = max(''N''), 
				Stp_BPlan_Adv = sum(0.0) ,
				Stp_BPlan_AdvReef = sum(0.0) ,
				Stp_BPlan_AdvWaste = sum(0.0) ,
				Stp_BPlan_FL = sum(0.000) ,
				Stp_BPlan_Sqm = sum(0.0) ,
				Stp_BPlan_SqmReef = sum(0.0) ,
				Stp_BPlan_SqmWaste = sum(0.0) ,
				Stp_BPlan_SqmSW = sum(0.0) ,
				Stp_BPlan_SqmCW = sum(0.0) ,
				Stp_BPlan_SqmCmgt = sum(0.0) , 
				Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
				Stp_BPlan_Tons = sum(0) ,
				Stp_BPlan_TonsReef = sum(0.0) ,
				Stp_BPlan_TonsWaste = sum(0.0),
				Stp_BPlan_Kg = sum(0),

				Stp_BPlan_OSSqm = sum(0.0),
				Stp_BPlan_OSFSqm = sum(0.0),
				Stp_BPlan_REEFFL = sum(0.0),
				Stp_BPlan_OSFL = sum(0.0),     
				Stp_BPlan_CUBICS = sum(0),
				Stp_BPlan_SqmSWFAULT = sum(0.0),
				Stp_BPlan_SqmFault = sum(0.0),
				Stp_BPlan_FaultTons = sum(0),
				Stp_BPlan_OSTons = sum(0),
				Stp_BPlan_CUBICTons = sum(0),
					 
				Dev_BPlan_AdvReef = sum(0),
				Dev_BPlan_AdvWaste = sum(0),
				Dev_BPlan_Primm = sum(0),
				Dev_BPlan_Secm = sum(0), 
				Dev_BPlan_Adv = sum(0),
				Dev_BPlan_TonsReef = sum(0),
				Dev_BPlan_TonsWaste = sum(0),
				Dev_BPlan_Tons = sum(0),
				Dev_BPlan_KG = sum(0),
				Dev_BPlan_AdvEH = sum(0),
				Dev_BPlan_AdvEW =sum(0),
				Dev_BPlan_AdvCmgt = sum(0),
				Dev_BPlan_AdvCmgtTotal = sum(0),
				Dev_BPlan_Cubics = sum(0),
				Dev_BPlan_CubicTons = sum(0),
				Dev_BPlan_TonsCmgt = sum(0),
					
				ForeCast_SQM = sum(0) ,
				ForeCast_SQMDens = sum(0) ,
				ForeCast_Grams = sum(0) ,
				ForeCast_Tons = sum(0),
				ForeCast_KG = sum(0) '
		set @LPlanFrom = '
			from Planmonth pm 
			left outer join Planning p on 
				p.Prodmonth = pm.Prodmonth and 
				p.SectionID = pm.Sectionid and 
				p.WorkplaceID = pm.Workplaceid and 
				p.Activity = pm.Activity and
				p.PlanCode = pm.PlanCode and
				p.IsCubics = pm.IsCubics and
				p. CalendarDate <= '''+@Calendardate+'''

			inner join section_complete sc on  
					sc.prodmonth = pm.prodmonth and  
					sc.sectionid  = pm.sectionid 
			inner join  (select distinct(ProdMonth) ProdMonth , SectionID,TotalShifts from seccal ) s on
			   s.ProdMonth = sc.prodmonth and
			   s.SectionID = sc.SectionID_1   
			inner join Workplace w on  
					w.WorkplaceID = pm.WorkplaceID 
			inner join CommonAreaSubSections CS on
					cs.Id = w.SubSection 
			inner join CommonAreas c on
					c.Id = cs.CommonArea '

		SET @LPlanWhere = 
			' where pm.PlanCode = ''LP'' and pm.IsCubics = ''N'' and'
		IF (@ReportType = 'P')
		BEGIN
			set @LPlanWhere = @LPlanWhere + ' pm.prodmonth = '''+@ProdMonth+''' ' 
		END
			
		IF (@ReportType = 'M')
		BEGIN
			set @LPlanWhere = @LPlanWhere + 
				' pm.prodmonth >= '''+@FromMonth+''' and 
					pm.prodmonth <= '''+@ToMonth+''' ' 
		END

		set @LPlanWhere = @LPlanWhere + '
			and '+@ReadSection+' = '''+@SectionID+'''
			group by  
				pm.Prodmonth, sc.SectionID_5, sc.Name_5, sc.Name_4, sc.SectionID_4, 
				sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,
				sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name, 
				pm.workplaceid, w.Description, pm.Activity '
	END
	IF (@PlanBuss = 'Y') -- and ( (@Meas = '1') or (@DailyPlan= '1') or (@ProgPlan = '1') or (@Book = '1'))
	BEGIN
		IF (@PlanMonth = 'Y') or (@PlanProg = 'Y') or (@Book = 'Y') or (@Meas = 'Y') or (@PlanMonthLock = 'Y') or (@PlanProgLock = 'Y')
			set @BusUnion = ' union all '
		set @BusSelect = ' select '
		IF (@RunLevel < 5)
		BEGIN
		
			IF (@RunLevel = 1)
				set @BusSelect = @BusSelect + 'scm.SectionID_5+'':''+scm.Name_5 NAME_5,scm.SectionID_4+'':''+scm.Name_4 NAME_4,
					scm.SectionID_3+'':''+scm.Name_3 NAME_3,scm.SectionID_2+'':''+scm.Name_2 NAME_2, '

			IF (@RunLevel = 2)
				set @BusSelect =  @BusSelect + ''''' NAME_5,scm.SectionID_4+'':''+scm.Name_4 NAME_4,scm.SectionID_3+'':''+scm.Name_3 NAME_3,
					scm.SectionID_2+'':''+scm.Name_2 NAME_2, '

			IF (@RunLevel = 3)
				set @BusSelect =  @BusSelect + '  
					'''' NAME_5, '''' NAME_4,scm.SectionID_3+'':''+scm.Name_3 NAME_3,scm.SectionID_2+'':''+scm.Name_2 NAME_2, '

			IF (@RunLevel = 4)
				set @BusSelect = @BusSelect + ''''' NAME_5, '''' NAME_4, '''' NAME_3,scm.SectionID_2+'':''+scm.Name_2 NAME_2, '

			set @BusSelect = @BusSelect + '
				NAME_1 =  case when isnull(sc.SectionID_1,'''') = '''' then ''Business SB'' else
							sc.SectionID_1+'':''+sc.Name_1 end,
				NAME = case when isnull(sc.SectionID,'''') = '''' then ''Business Miner'' else
							sc.SectionID+'':''+sc.Name end ,' 
		END
		IF (@RunLevel > 4)
		BEGIN
			IF (@RunLevel = 5)
				set @BusSelect = @BusSelect + ''''' NAME_5, '''' NAME_4,''''NAME_3, '''' NAME_2,
					sc.SectionID_1+'':''+sc.Name_1 NAME_1, sc.SectionID+'':''+sc.Name NAME,' 

			IF (@RunLevel = 6)
				set @BusSelect = @BusSelect + ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,'''' NAME_1,
					sc.SectionID+'':''+sc.Name NAME,' 

			IF (@RunLevel = 7)
			  set @BusSelect  = @BusSelect +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, ''Total Mine'' NAME_2,c.Name NAME_1,cs.Name NAME, '    

			IF (@RunLevel = 8)
			  set @BusSelect  = @BusSelect +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,c.Name NAME_1, cs.Name NAME, '    

			IF (@RunLevel = 9)
			  set @BusSelect  = @BusSelect +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,'''' NAME_1, cs.Name NAME, '    
		END
		if @TotalsPerMonth = 'Y' 
			set @BusSelect = @BusSelect + ' Prodmonth = cast(pm.Prodmonth as varchar(6)), '
				else 
			set @BusSelect = @BusSelect + ' '''' Prodmonth, '
		set @BusSelect = @BusSelect + '	 	 
				pm.Workplaceid+'':''+w.Description workplace, 
				--pm.Workplaceid,  
				Activity = 0,
				Plan_Stope = sum(1),  
				Plan_Dev = sum(0), 
				TotalShifts = sum(0),
				ShiftNo = sum(0), 
				Stp_Plan_FL = sum(0), 
				Stp_Plan_FLReef = sum(0), 
				Stp_Plan_FLWaste = sum(0), 
				Stp_Plan_Sqm = sum(0),
				Stp_Plan_SqmReef = sum(0),
				Stp_Plan_SqmWaste = sum(0),
				Stp_Plan_Adv = sum(0),
				Stp_Plan_AdvReef = sum(0),
				Stp_Plan_AdvWaste = sum(0),
				Stp_Plan_SqmSW = sum(0),
				Stp_Plan_SqmCW = sum(0),
				Stp_Plan_SqmCmgt = sum(0),
				Stp_Plan_SqmCmgtTotal = sum(0),
				Stp_Plan_Tons = sum(0),
				Stp_Plan_TonsReef = sum(0),
				Stp_Plan_TonsWaste = sum(0),
				Stp_Plan_Kg = sum(0),
				Stp_Plan_Cubics = sum(0), 
				Stp_Plan_CubicTons = sum(0),
				Stp_Plan_CubicGrams = sum(0),

				Dev_Plan_AdvReef = sum(0),
				Dev_Plan_AdvWaste = sum(0),
				Dev_Plan_Primm = sum(0),
				Dev_Plan_Secm = sum(0),
				Dev_Plan_Adv = sum(0),
				Dev_Plan_TonsReef = sum(0),
				Dev_Plan_TonsWaste = sum(0),
				Dev_Plan_Tons = sum(0),
				Dev_Plan_KG = sum(0),
				Dev_Plan_AdvEH = sum(0),
				Dev_Plan_AdvEW = sum(0),
				Dev_Plan_AdvCmgt = sum(0),
				Dev_Plan_AdvCmgtTotal = sum(0),
				Dev_Plan_Cubics = sum(0), 
				Dev_Plan_CubicTons = sum(0),
				Dev_Plan_CubicGrams = sum(0),
				Dev_Plan_Labour = sum(0),
				Dev_Plan_ShftInfo = sum(0),
				Dev_Plan_DrillRig = max(''''), 

				Stp_PPlan_FL = sum(0), 
				Stp_PPlan_FLReef = sum(0), 
				Stp_PPlan_FLWaste = sum(0), 
				Stp_PPlan_Sqm = sum(0),
				Stp_PPlan_SqmReef = sum(0),
				Stp_PPlan_SqmWaste = sum(0),
				Stp_PPlan_Adv = sum(0),
				Stp_PPlan_AdvReef = sum(0),
				Stp_PPlan_AdvWaste = sum(0),
				Stp_PPlan_SqmSW = sum(0),
				Stp_PPlan_SqmCW = sum(0),
				Stp_PPlan_SqmCmgt = sum(0),
				Stp_PPlan_SqmCmgtTotal = sum(0),
				Stp_PPlan_Tons = sum(0),
				Stp_PPlan_TonsReef = sum(0),
				Stp_PPlan_TonsWaste = sum(0),
				Stp_PPlan_Kg = sum(0),
				Stp_PPlan_Cubics = sum(0), 
				Stp_PPlan_CubicTons = sum(0),
				Stp_PPlan_CubicGrams = sum(0),

				Dev_PPlan_AdvReef = sum(0),
				Dev_PPlan_AdvWaste = sum(0), 
				Dev_PPlan_Primm = sum(0),
				Dev_PPlan_Secm = sum(0),
				Dev_PPlan_Adv = sum(0),
				Dev_PPlan_TonsReef = sum(0), 
				Dev_PPlan_TonsWaste = sum(0), 
				Dev_PPlan_Tons = sum(0), 
				Dev_PPlan_KG = sum(0),
				Dev_PPlan_AdvEH = sum(0),
				Dev_PPlan_AdvEW = sum(0),
				Dev_PPlan_AdvCmgt = sum(0),
				Dev_PPlan_AdvCmgtTotal = sum(0), 
				Dev_PPlan_Cubics = sum(0),
				Dev_PPlan_CubicTons = sum(0),
				Dev_PPlan_CubicGrams = sum(0),
				Dev_PPlan_Labour = sum(0),
				Dev_PPlan_ShftInfo = sum(0), 
				Dev_PPlan_DrillRig = max(''''), 
			
				Stp_Book_FL = sum(0),
				Stp_Book_FLReef = sum(0),
				Stp_Book_FLWaste = sum(0),
				Stp_Book_Sqm = sum(0),
				Stp_Book_SqmReef = sum(0), 
				Stp_Book_SqmWaste = sum(0),
				Stp_Book_Adv = sum(0),
				Stp_Book_AdvReef = sum(0), 
				Stp_Book_AdvWaste = sum(0),
				Stp_Book_SqmSW = sum(0),
				Stp_Book_SqmCW = sum(0), 
				Stp_Book_SqmCmgt = sum(0), 
				Stp_Book_SqmCmgtTotal = sum(0),
				Stp_Book_Tons = sum(0),
				Stp_Book_TonsReef = sum(0),
				Stp_Book_TonsWaste = sum(0),
				Stp_Book_KG = sum(0),
				Stp_Book_Cubics = sum(0), 
				Stp_Book_CubicTons = sum(0),
				Stp_Book_CubicGrams = sum(0),

				Dev_Book_AdvReef = sum(0),
				Dev_Book_AdvWaste = sum(0),
				Dev_Book_Primm = sum(0),
				Dev_Book_Secm = sum(0), 
				Dev_Book_Adv = sum(0), 
				Dev_Book_TonsReef = sum(0),
				Dev_Book_TonsWaste = sum(0),
				Dev_Book_Tons = sum(0), 
				Dev_Book_KG = sum(0), 
				Dev_Book_AdvEH = sum(0), 
				Dev_Book_AdvEW = sum(0),
				Dev_Book_AdvCmgt = sum(0),
				Dev_Book_AdvCmgtTotal = sum(0),
				Dev_Book_Cubics = sum(0),
				Dev_Book_CubicTons = sum(0),
				Dev_Book_CubicGrams = sum(0),
				--Dev_Book_Labour = sum(0), 
				--Dev_Book_ShftInfo = sum(0),
				
				Stp_LPlan_FL = sum(0),
				Stp_LPlan_FLReef = sum(0),
				Stp_LPlan_FLWaste = sum(0),
				Stp_LPlan_Sqm = sum(0),
				Stp_LPlan_SqmReef = sum(0),
				Stp_LPlan_SqmWaste = sum(0), 
				Stp_LPlan_Adv = sum(0),
				Stp_LPlan_AdvReef = sum(0),
				Stp_LPlan_AdvWaste = sum(0),
				Stp_LPlan_SqmSW = sum(0),
				Stp_LPlan_SqmCW = sum(0),
				Stp_LPlan_SqmCmgt = sum(0),
				Stp_LPlan_SqmCmgtTotal = sum(0),
				Stp_LPlan_Tons = sum(0),
				Stp_LPlan_TonsReef = sum(0),
				Stp_LPlan_TonsWaste = sum(0),
				Stp_LPlan_Kg = sum(0),
				Stp_LPlan_Cubics = sum(0), 
				Stp_LPlan_CubicTons = sum(0),
				Stp_LPlan_CubicGrams = sum(0),

				Dev_LPlan_AdvReef = sum(0),
				Dev_LPlan_AdvWaste = sum(0),
				Dev_LPlan_Primm = sum(0),
				Dev_LPlan_Secm = sum(0),
				Dev_LPlan_Adv = sum(0),
				Dev_LPlan_TonsReef = sum(0),
				Dev_LPlan_TonsWaste = sum(0),
				Dev_LPlan_Tons = sum(0),
				Dev_LPlan_KG = sum(0),
				Dev_LPlan_AdvEH = sum(0),
				Dev_LPlan_AdvEW = sum(0),
				Dev_LPlan_AdvCmgt = sum(0),
				Dev_LPlan_AdvCmgtTotal = sum(0),
				Dev_LPlan_Cubics = sum(0),
				Dev_LPlan_CubicTons = sum(0),
				Dev_LPlan_CubicGrams = sum(0),
				Dev_LPlan_Labour = sum(0),
				Dev_LPlan_ShftInfo = sum(0),
				Dev_LPlan_DrillRig = max(''''), 

				Stp_LPPlan_FL = sum(0),
				Stp_LPPlan_FLReef = sum(0),
				Stp_LPPlan_FLWaste = sum(0),
				Stp_LPPlan_Sqm = sum(0),
				Stp_LPPlan_SqmReef = sum(0),
				Stp_LPPlan_SqmWaste = sum(0), 
				Stp_LPPlan_Adv = sum(0),
				Stp_LPPlan_AdvReef = sum(0), 
				Stp_LPPlan_AdvWaste = sum(0),
				Stp_LPPlan_SqmSW = sum(0),
				Stp_LPPlan_SqmCW = sum(0),
				Stp_LPPlan_SqmCmgt = sum(0),
				Stp_LPPlan_SqmCmgtTotal = sum(0),
				Stp_LPPlan_Tons = sum(0),
				Stp_LPPlan_TonsReef = sum(0),
				Stp_LPPlan_TonsWaste = sum(0),
				Stp_LPPlan_Kg = sum(0),
				Stp_LPPlan_Cubics = sum(0), 
				Stp_LPPlan_CubicTons = sum(0),
				Stp_LPPlan_CubicGrams = sum(0),

				Dev_LPPlan_AdvReef = sum(0),
				Dev_LPPlan_AdvWaste = sum(0),
				Dev_LPPlan_Primm = sum(0),
				Dev_LPPlan_Secm = sum(0),
				Dev_LPPlan_Adv = sum(0),
				Dev_LPPlan_TonsReef = sum(0),
				Dev_LPPlan_TonsWaste = sum(0), 
				Dev_LPPlan_Tons = sum(0),
				Dev_LPPlan_KG = sum(0),
				Dev_LPPlan_AdvEH = sum(0),
				Dev_LPPlan_AdvEW= sum(0),
				Dev_LPPlan_AdvCmgt = sum(0), 
				Dev_LPPlan_AdvCmgtTotal = sum(0), 
				Dev_LPPlan_Cubics = sum(0),
				Dev_LPPlan_CubicTons = sum(0),
				Dev_LPPlan_CubicGrams = sum(0),
				Dev_LPPlan_Labour =sum(0),
				Dev_LPPlan_ShftInfo = sum(0),
				Dev_LPPlan_DrillRig = max(''''), 
				
				Stp_Meas_FL = sum(0),
				Stp_Meas_FLReef = sum(0),
				Stp_Meas_FLWaste = sum(0),
				Stp_Meas_Sqm = sum(0),
				Stp_Meas_SqmReef = sum(0),
				Stp_Meas_SqmWaste = sum(0),
				Stp_Meas_Adv = sum(0),
				Stp_Meas_AdvReef = sum(0),
				Stp_Meas_Advwaste = sum(0),
				Stp_Meas_SqmSW = sum(0),
				Stp_Meas_SqmCW = sum(0),
				Stp_Meas_SqmCMGT = sum(0 ),
				Stp_Meas_SqmCMGTTotal = sum(0 ),				
				Stp_Meas_Tons = sum(0), 
				Stp_Meas_TonsReef = sum(0.0), 
				Stp_Meas_TonsWaste = sum(0.0),
				Stp_Meas_Kg = sum(0), 
				Stp_Meas_Cubics = sum(0),
				Stp_Meas_CubicTons = sum(0), 
				Stp_Meas_CubicGrams = sum(0), '
				
			set @BusSelect1 = '
				Dev_Meas_AdvReef = sum(0),
				Dev_Meas_AdvWaste = sum(0),
				Dev_Meas_Primm = sum(0),
				Dev_Meas_Secm = sum(0),
				Dev_Meas_Adv = sum(0),
				Dev_Meas_TonsReef = sum(0),
				Dev_Meas_TonsWaste = sum(0), 
				Dev_Meas_Tons = sum(0),
				Dev_Meas_KG = sum(0),
				Dev_Meas_AdvEH = sum(0),
				Dev_Meas_AdvEW = sum(0),
				Dev_Meas_AdvCmgt = sum(0),
				Dev_Meas_AdvCmgtTotal = sum(0),
				Dev_Meas_Cubics = sum(0), 
				Dev_Meas_CubicTons = sum(0),
				Dev_Meas_CubicGrams = sum(0),
				 
				BusFlag = max(''Y''), '
			IF (@TheStopeLedge = '0')
			BEGIN
				set @BusSelect1 = @BusSelect1 + '
					Stp_BPlan_FL = sum(case when isnull(pm.Sqm,0) > 0 then isnull(pm.FL,0) else 0 end),
					Stp_BPlan_Adv = sum(case when pm.FL > 0 then pm.Sqm / pm.FL else 0 end),
					Stp_BPlan_AdvReef = sum(case when pm.FLReef > 0 then (pm.SqmReefStope + pm.SqmReeflLedge) / pm.FLReef else 0 end),
					Stp_BPlan_AdvWaste = sum(case when pm.FLOS > 0 then pm.SqmWaste / pm.FLOS else 0 end),
					Stp_BPlan_Sqm = sum(pm.Sqm), 
    				Stp_BPlan_SqmReef = sum(pm.SqmReefStope + pm.SqmReeflLedge),
					Stp_BPlan_SqmWaste = sum(pm.SqmWaste),
					Stp_BPlan_SqmSW = sum(pm.Sqm * pm.SW), 
    				Stp_BPlan_SqmCW = sum((pm.SqmReefStope + pm.SqmReeflLedge) * pm.CW),
					Stp_BPlan_SqmCmgt = sum((pm.SqmReefStope + pm.SqmReeflLedge) * pm.Cmgt),
					Stp_BPlan_SqmCmgtTotal = sum(pm.Sqm * pm.Cmgt),
					Stp_BPlan_Tons = sum(pm.Tons), 
    				Stp_BPlan_TonsReef = sum(pm.TonsReef),
					Stp_BPlan_TonsWaste = sum(pm.TonsWaste),
					Stp_BPlan_Kg = sum(pm.Content / 1000),
					Stp_BPlan_OSSqm = sum(pm.SqmOSStope + pm.SqmOSLedge),
					Stp_BPlan_OSFSqm = sum(pm.SqmOSFStope + pm.SqmOSFLedge),
					Stp_BPlan_REEFFL = sum(case when isnull(pm.SqmReefStope,0) > 0 or isnull(pm.SqmReeflLedge,0) > 0 then 
							isnull(pm.FLReef,0) else 0 end), 
					Stp_BPlan_OSFL = sum(case when isnull(pm.SqmWaste,0) > 0 then isnull(pm.FLOS,0) else 0 end),
					Stp_BPlan_CUBICS = sum(pm.Cubics),
					Stp_BPlan_SqmSWFAULT = sum(case when isnull(pm.SWFault,0) > 0 then 
							isnull(pm.Sqm,0) * isnull(pm.SWFault,0) else 0 end),  
					Stp_BPlan_SqmFault = sum(case when isnull(pm.SWFault,0) > 0 then isnull(pm.Sqm,0) else 0 end),       
					Stp_BPlan_FaultTons = sum(pm.TonsFault),
					Stp_BPlan_OSTons = sum(pm.TonsOS),
					Stp_BPlan_CUBICTons = sum(TonsCubic), '
			END
			IF (@TheStopeLedge = '1')
			BEGIN
				set @BusSelect1 = @BusSelect1 + '
					Stp_BPlan_FL = sum(case when isnull(pm.SqmStope,0) > 0 then isnull(pm.FL,0) else 0 end),
					Stp_BPlan_Adv = sum(case when isnull(pm.SqmStope,0) > 0 and pm.FL > 0 then 
									pm.Sqmstope / pm.FL else 0 end),
					Stp_BPlan_AdvReef = sum(case when isnull(pm.SqmReefStope,0) > 0 and pm.FLReef > 0 then 
									pm.SqmReefStope / pm.FLReef else 0 end),
					Stp_BPlan_AdvWaste = sum(case when pm.SqmWasteStope > 0 and pm.FLOS > 0 then pm.SqmWasteStope / pm.FLOS else 0 end),
					Stp_BPlan_Sqm = sum(pm.SqmStope), 
    				Stp_BPlan_SqmReef = sum(pm.SqmReefStope),
					Stp_BPlan_SqmWaste = sum(pm.SqmWasteStope),
					Stp_BPlan_SqmSW = sum(pm.SqmStope * pm.SW), 
    				Stp_BPlan_SqmCW = sum(pm.SqmReefStope * pm.CW),
					Stp_BPlan_SqmCmgt = sum(pm.SqmReefStope * pm.Cmgt),
					Stp_BPlan_SqmCmgtTotal = sum(pm.SqmStope * pm.Cmgt),
					Stp_BPlan_Tons = sum(pm.TonsStope), 
    				Stp_BPlan_TonsReef = sum(pm.TonsReefStope),
					Stp_BPlan_TonsWaste = sum(pm.TonsWasteStope),
					Stp_BPlan_Kg = sum(pm.ContentStope / 1000),
					Stp_BPlan_OSSqm = sum(pm.SqmOSStope),
					Stp_BPlan_OSFSqm = sum(pm.SqmOSFStope),
					Stp_BPlan_REEFFL = sum(case when isnull(pm.SqmReefStope,0) > 0 then 
							isnull(pm.FLReef,0) else 0 end), 
					Stp_BPlan_OSFL = sum(case when isnull(pm.SqmWasteStope,0) > 0 then isnull(pm.FLOS,0) else 0 end),
					Stp_BPlan_REEFFL = sum(pm.ReefFL), 
					Stp_BPlan_OSFL = sum(pm.FLOS),
					Stp_BPlan_CUBICS = sum(0),
					Stp_BPlan_SqmSWFAULT = sum(case when isnull(pm.SWFault,0) > 0 then 
							isnull(pm.SqmStope,0) * isnull(pm.SWFault,0) else 0 end),  
					Stp_BPlan_SqmFault = sum(case when isnull(pm.SWFault,0) > 0 then isnull(pm.SqmStope,0) else 0 end),     
					Stp_BPlan_FaultTons = sum(pm.TonsFault),
					Stp_BPlan_OSTons = sum(pm.TonsOS),
					Stp_BPlan_CUBICTons = sum(0), '
			END
			IF (@TheStopeLedge = '2')
			BEGIN
				set @BusSelect1 = @BusSelect1 + '
					Stp_BPlan_FL = sum(case when isnull(pm.SqmLedge,0) > 0 then isnull(pm.FL,0) else 0 end),
					Stp_BPlan_Adv = sum(case when isnull(pm.SqmLedge,0) > 0 and pm.FL > 0 then 
									pm.SqmLedge / pm.FL else 0 end),
					Stp_BPlan_AdvReef = sum(case when isnull(pm.SqmReeflLedge,0) > 0 and pm.FLReef > 0 then 
									pm.SqmReeflLedge / pm.FLReef else 0 end),
					Stp_BPlan_AdvWaste = sum(case when pm.SqmWasteLedge > 0 and pm.FLOS > 0 then pm.SqmWasteLedge / pm.FLOS else 0 end),
					Stp_BPlan_Sqm = sum(pm.SqmLedge), 
    				Stp_BPlan_SqmReef = sum(pm.SqmReeflLedge),
					Stp_BPlan_SqmWaste = sum(pm.SqmWasteLedge),
					Stp_BPlan_SqmSW = sum(pm.SqmLedge * pm.SW), 
    				Stp_BPlan_SqmCW = sum(pm.SqmReeflLedge * pm.CW),
					Stp_BPlan_SqmCmgt = sum(pm.SqmReeflLedge * pm.Cmgt),
					Stp_BPlan_SqmCmgtTotal = sum(pm.SqmLedge * pm.Cmgt),
					Stp_BPlan_Tons = sum(pm.TonsLedge), 
    				Stp_BPlan_TonsReef = sum(pm.TonsReefLedge),
					Stp_BPlan_TonsWaste = sum(pm.TonsWasteLedge),
					Stp_BPlan_Kg = sum(pm.ContentLedge / 1000),
					Stp_BPlan_OSSqm = sum(pm.SqmOSLedge),
					Stp_BPlan_OSFSqm = sum(pm.SqmOSFLedge),
					Stp_BPlan_REEFFL = sum(case when isnull(pm.SqmReeflLedge,0) > 0 then 
							isnull(pm.FLReef,0) else 0 end), 
					Stp_BPlan_OSFL = sum(case when isnull(pm.SqmWasteLedge,0) > 0 then isnull(pm.FLOS,0) else 0 end),
					Stp_BPlan_CUBICS = sum(0), 
					Stp_BPlan_SqmSWFAULT = sum(case when isnull(pm.SWFault,0) > 0 then 
							isnull(pm.SqmLedge,0) * isnull(pm.SWFault,0) else 0 end),  
					Stp_BPlan_SqmFault = sum(case when isnull(pm.SWFault,0) > 0 then isnull(pm.SqmLedge,0) else 0 end),         
					Stp_BPlan_FaultTons = sum(pm.TonsFault),
					Stp_BPlan_OSTons = sum(pm.TonsOS),
					Stp_BPlan_CUBICTons = sum(0), '
			END
			set @BusSelect1 = @BusSelect1 + '
				Dev_BPlan_AdvReef = sum(0),
				Dev_BPlan_AdvWaste = sum(0),
				Dev_BPlan_Primm = sum(0),
				Dev_BPlan_Secm = sum(0), 
				Dev_BPlan_Adv = sum(0),
				Dev_BPlan_TonsReef = sum(0),
				Dev_BPlan_TonsWaste = sum(0),
				Dev_BPlan_Tons = sum(0),
				Dev_BPlan_KG = sum(0),
				Dev_BPlan_AdvEH = sum(0),
				Dev_BPlan_AdvEW =sum(0),
				Dev_BPlan_AdvCmgt = sum(0),
				Dev_BPlan_AdvCmgtTotal = sum(0),
				Dev_BPlan_Cubics = sum(0),
				Dev_BPlan_CubicTons = sum(0),
				Dev_BPlan_TonsCmgt = sum(0),
				
				ForeCast_SQM = sum(0) ,
				ForeCast_SQMDens = sum(0) ,
				ForeCast_Grams = sum(0) ,
				ForeCast_Tons = sum(0),
				ForeCast_KG = sum(0) '			
			
				set @BusFrom =  '
			from BusinessPlan_Stoping pm '
			IF (@RunLevel < 5)
			BEGIN
				set @BusFrom = @BusFrom + '
					left outer join 
						(select p.Prodmonth, p.SectioniD, p.workplaceid, sc.sectionID_2, sc.sectionid_1, sc.name_1, sc.name 
						from planmonth p 
						inner join  Section_Complete sc on
						sc.ProdMonth = p.ProdMonth and
						sc.SectionID = p.SectionID
						where activity= 0 and
						p.plancode=''MP'' and '
				IF (@ReportType = 'M')
				BEGIN
					set @BusFrom = @BusFrom + 
						' p.ProdMonth >= '''+ @FromMonth +''' and
						  p.ProdMonth <= '''+ @ToMonth +''' '
				END
				IF (@ReportType = 'P')
				BEGIN
					set @BusFrom = @BusFrom + 
						' p.ProdMonth = '''+ @ProdMonth+''' '
				END 
				set @BusFrom = @BusFrom + '
						) sc on
						sc.ProdMonth = pm.prodmonth and
						sc.sectionid_2 = pm.sectionid and
						sc.workplaceid=pm.workplaceid 
					left outer join vw_Sections_Complete_MO scm on
					  scm.ProdMonth = pm.ProdMonth and
					  scm.SectionID_2 = pm.SectionID	'
				
			END
			IF (@RunLevel > 4)
			BEGIN
				set @BusFrom = @BusFrom + '
					left outer join planmonth p on
						p.Prodmonth = pm.ProdMonth and
						p.WorkplaceID = pm.WorkplaceID and
						p.Activity = 1 and 
						p.PlanCode = pm.PlanCode
					left outer join Section_Complete sc on
						sc.ProdMonth = p.ProdMonth and
						sc.SectionID = p.SectionID and '
						IF (@RunLevel = 5)
						  set @BusFrom = @BusFrom + 'sc.SectionID_1 = '''+@SectionID+'''  '
						IF (@RunLevel = 6)
						  set @BusFrom = @BusFrom + 'sc.SectionID = '''+@SectionID+'''  '
			END
			set @BusFrom = @BusFrom + '			
				inner join Workplace w on
					pm.WorkplaceID = w.WorkplaceID '
			set @BusWhere = ''
			IF (@ReportType = 'M')
			BEGIN
				set @BusWhere = @BusWhere + 
					' where pm.ProdMonth >= '''+ @FromMonth +''' and
						pm.ProdMonth <= '''+ @ToMonth +''' '
			END
			IF (@ReportType = 'P')
			BEGIN
				set @BusWhere = @BusWhere + 
					' where pm.ProdMonth = '''+ @ProdMonth +''' '
			END 

			IF (@RunLevel = 1)
				set @BusWhere = @BusWhere + ' and scm.SectionID_5 = '''+@SectionID+''' '  

			IF (@RunLevel = 2)
				set @BusWhere = @BusWhere + ' and scm.SectionID_4 = '''+@SectionID+''' '

			IF (@RunLevel = 3)
				set @BusWhere = @BusWhere + ' and scm.SectionID_3 = '''+@SectionID+''' '

			IF (@RunLevel = 4)
				set @BusWhere = @BusWhere + ' and scm.SectionID_2 = '''+@SectionID+''' '
			IF (@RunLevel > 4)
			BEGIN
				IF (@RunLevel = 5)
					set @BusWhere = @BusWhere + ' and sc.SectionID_1 = '''+@SectionID+''' '

				IF (@RunLevel = 6)
					set @BusWhere = @BusWhere + ' and sc.SectionID = '''+@SectionID+''' '
			END


			set @BusWhere = @BusWhere + '
				group by  '

			IF (@RunLevel < 5)
			BEGIN
				IF (@RunLevel = 1)
					set @BusWhere = @BusWhere + 
						 ' scm.SectionID_5, scm.Name_5, scm.SectionID_4, scm.Name_4 ,scm.SectionID_3, scm.Name_3, scm.SectionID_2, scm.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name ' 

				IF (@RunLevel = 2)
					set @BusWhere = @BusWhere +
							' scm.SectionID_4, scm.Name_4, scm.SectionID_3, scm.Name_3, scm.SectionID_2, scm.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name' 
				IF (@RunLevel = 3)
					set @BusWhere = @BusWhere +
							' scm.SectionID_3, scm.Name_3, scm.SectionID_2, scm.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, scm.Name' 
				IF (@RunLevel = 4)
					set @BusWhere = @BusWhere +
							' scm.SectionID_2, scm.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name '
			END
			IF (@RunLevel > 4)
			BEGIN
				IF (@RunLevel = 5)
					set @BusWhere = @BusWhere +'  sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name '  

				IF (@RunLevel = 6)
					set @BusWhere = @BusWhere + ' sc.SectionID, sc.Name ' 

				IF (@RunLevel = 7)
					set @BusWhere = @BusWhere + 'Name_2, c.Name, cs.Name '  

				IF (@RunLevel = 8)
					set @BusWhere = @BusWhere + 'c.Name, cs.Name ' 


				IF (@RunLevel = 9)
					set @BusWhere = @BusWhere + ' cs.Name '  
			END

			set @BusWhere = @BusWhere + ', pm.Prodmonth, pm.WORKPLACEID, w.Description '

		set @BusDevUnion = ' union all '
		set @BusDevSelect = ' select '
		IF (@RunLevel < 5)
		BEGIN
		
			IF (@RunLevel = 1)
				set @BusDevSelect = @BusDevSelect + 'scm.SectionID_5+'':''+scm.Name_5 NAME_5,scm.SectionID_4+'':''+scm.Name_4 NAME_4,
					scm.SectionID_3+'':''+scm.Name_3 NAME_3,scm.SectionID_2+'':''+scm.Name_2 NAME_2, '

			IF (@RunLevel = 2)
				set @BusDevSelect =  @BusDevSelect + ''''' NAME_5,scm.SectionID_4+'':''+scm.Name_4 NAME_4,scm.SectionID_3+'':''+scm.Name_3 NAME_3,
					scm.SectionID_2+'':''+scm.Name_2 NAME_2, '
					
			IF (@RunLevel = 3)
				set @BusDevSelect =  @BusDevSelect + '  
					'''' NAME_5, '''' NAME_4,scm.SectionID_3+'':''+scm.Name_3 NAME_3,scm.SectionID_2+'':''+scm.Name_2 NAME_2, '
			IF (@RunLevel = 4)
				set @BusDevSelect = @BusDevSelect + ''''' NAME_5, '''' NAME_4, '''' NAME_3,scm.SectionID_2+'':''+scm.Name_2 NAME_2, '

			set @BusDevSelect = @BusDevSelect + '
				NAME_1 =  case when isnull(sc.SectionID_1,'''') = '''' then ''Business SB'' else
							sc.SectionID_1+'':''+sc.Name_1 end,
					NAME = case when isnull(sc.SectionID,'''') = '''' then ''Business Miner'' else
							sc.SectionID+'':''+sc.Name end,' 
		END
		IF (@RunLevel > 4)
		BEGIN
			IF (@RunLevel = 5)
				set @BusDevSelect = @BusDevSelect + ''''' NAME_5, '''' NAME_4,''''NAME_3, '''' NAME_2,
					sc.SectionID_1+'':''+sc.Name_1 NAME_1, sc.SectionID+'':''+sc.Name NAME,' 

			IF (@RunLevel = 6)
				set @BusDevSelect = @BusDevSelect + ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,'''' NAME_1,
					sc.SectionID+'':''+sc.Name NAME,' 

			IF (@RunLevel = 7)
			  set @BusDevSelect  = @BusDevSelect +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, ''Total Mine'' NAME_2,c.Name NAME_1,cs.Name NAME, '    

			IF (@RunLevel = 8)
			  set @BusDevSelect  = @BusDevSelect +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,c.Name NAME_1, cs.Name NAME, '    

			IF (@RunLevel = 9)
			  set @BusDevSelect  = @BusDevSelect +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,'''' NAME_1, cs.Name NAME, '    
		END
		if @TotalsPerMonth = 'Y' 
			set @BusDevSelect = @BusDevSelect + ' Prodmonth = cast(pm.Prodmonth as varchar(6)), '
				else 
			set @BusDevSelect = @BusDevSelect + ' '''' Prodmonth, '
		set @BusDevSelect = @BusDevSelect + '	  
				pm.Workplaceid+'':''+w.Description workplace, 
				--pm.workplaceid,  
				Activity = 1,
				Plan_Stope = sum(0),  
				Plan_Dev = sum(0),
				TotalShifts = sum(0),
				ShiftNo = sum(0),
				Stp_Plan_FL = sum(0), 
				Stp_Plan_FLReef = sum(0), 
				Stp_Plan_FLWaste = sum(0), 
				Stp_Plan_Sqm = sum(0),
				Stp_Plan_SqmReef = sum(0),
				Stp_Plan_SqmWaste = sum(0),
				Stp_Plan_Adv = sum(0),
				Stp_Plan_AdvReef = sum(0),
				Stp_Plan_AdvWaste = sum(0),
				Stp_Plan_SqmSW = sum(0),
				Stp_Plan_SqmCW = sum(0),
				Stp_Plan_SqmCmgt = sum(0),
				Stp_Plan_SqmCmgtTotal = sum(0),
				Stp_Plan_Tons = sum(0),
				Stp_Plan_TonsReef = sum(0),
				Stp_Plan_TonsWaste = sum(0),
				Stp_Plan_Kg = sum(0),
				Stp_Plan_Cubics = sum(0), 
				Stp_Plan_CubicTons = sum(0),
				Stp_Plan_CubicGrams = sum(0),

				Dev_Plan_AdvReef = sum(0),
				Dev_Plan_AdvWaste = sum(0),
				Dev_Plan_Primm = sum(0),
				Dev_Plan_Secm = sum(0),
				Dev_Plan_Adv = sum(0),
				Dev_Plan_TonsReef = sum(0),
				Dev_Plan_TonsWaste = sum(0),
				Dev_Plan_Tons = sum(0),
				Dev_Plan_KG = sum(0),
				Dev_Plan_AdvEH = sum(0),
				Dev_Plan_AdvEW = sum(0),
				Dev_Plan_AdvCmgt = sum(0),
				Dev_Plan_AdvCmgtTotal = sum(0),
				Dev_Plan_Cubics = sum(0), 
				Dev_Plan_CubicTons = sum(0),
				Dev_Plan_CubicGrams = sum(0),
				Dev_Plan_Labour = sum(0),
				Dev_Plan_ShftInfo = sum(0),
				Dev_Plan_DrillRig = max(''''), 

				Stp_PPlan_FL = sum(0), 
				Stp_PPlan_FLReef = sum(0), 
				Stp_PPlan_FLWaste = sum(0), 
				Stp_PPlan_Sqm = sum(0),
				Stp_PPlan_SqmReef = sum(0),
				Stp_PPlan_SqmWaste = sum(0),
				Stp_PPlan_Adv = sum(0),
				Stp_PPlan_AdvReef = sum(0),
				Stp_PPlan_AdvWaste = sum(0),
				Stp_PPlan_SqmSW = sum(0),
				Stp_PPlan_SqmCW = sum(0),
				Stp_PPlan_SqmCmgt = sum(0),
				Stp_PPlan_SqmCmgtTotal = sum(0),
				Stp_PPlan_Tons = sum(0),
				Stp_PPlan_TonsReef = sum(0),
				Stp_PPlan_TonsWaste = sum(0),
				Stp_PPlan_Kg = sum(0),
				Stp_PPlan_Cubics = sum(0), 
				Stp_PPlan_CubicTons = sum(0),
				Stp_PPlan_CubicGrams = sum(0),

				Dev_PPlan_AdvReef = sum(0),
				Dev_PPlan_AdvWaste = sum(0), 
				Dev_PPlan_Primm = sum(0),
				Dev_PPlan_Secm = sum(0),
				Dev_PPlan_Adv = sum(0),
				Dev_PPlan_TonsReef = sum(0), 
				Dev_PPlan_TonsWaste = sum(0), 
				Dev_PPlan_Tons = sum(0), 
				Dev_PPlan_KG = sum(0),
				Dev_PPlan_AdvEH = sum(0),
				Dev_PPlan_AdvEW = sum(0),
				Dev_PPlan_AdvCmgt = sum(0),
				Dev_PPlan_AdvCmgtTotal = sum(0), 
				Dev_PPlan_Cubics = sum(0),
				Dev_PPlan_CubicTons = sum(0),
				Dev_PPlan_CubicGrams = sum(0),
				Dev_PPlan_Labour = sum(0),
				Dev_PPlan_ShftInfo = sum(0), 
				Dev_PPlan_DrillRig = max(''''), 
			
				Stp_Book_FL = sum(0),
				Stp_Book_FLReef = sum(0),
				Stp_Book_FLWaste = sum(0),
				Stp_Book_Sqm = sum(0),
				Stp_Book_SqmReef = sum(0), 
				Stp_Book_SqmWaste = sum(0),
				Stp_Book_Adv = sum(0),
				Stp_Book_AdvReef = sum(0), 
				Stp_Book_AdvWaste = sum(0),
				Stp_Book_SqmSW = sum(0),
				Stp_Book_SqmCW = sum(0), 
				Stp_Book_SqmCmgt = sum(0), 
				Stp_Book_SqmCmgtTotal = sum(0),
				Stp_Book_Tons = sum(0),
				Stp_Book_TonsReef = sum(0),
				Stp_Book_TonsWaste = sum(0),
				Stp_Book_KG = sum(0),
				Stp_Book_Cubics = sum(0), 
				Stp_Book_CubicTons = sum(0),
				Stp_Book_CubicGrams = sum(0),

				Dev_Book_AdvReef = sum(0),
				Dev_Book_AdvWaste = sum(0),
				Dev_Book_Primm = sum(0),
				Dev_Book_Secm = sum(0), 
				Dev_Book_Adv = sum(0), 
				Dev_Book_TonsReef = sum(0),
				Dev_Book_TonsWaste = sum(0),
				Dev_Book_Tons = sum(0), 
				Dev_Book_KG = sum(0), 
				Dev_Book_AdvEH = sum(0), 
				Dev_Book_AdvEW = sum(0),
				Dev_Book_AdvCmgt = sum(0),
				Dev_Book_AdvCmgtTotal = sum(0),
				Dev_Book_Cubics = sum(0),
				Dev_Book_CubicTons = sum(0),
				Dev_Book_CubicGrams = sum(0),
				--Dev_Book_Labour = sum(0), 
				--Dev_Book_ShftInfo = sum(0),
				
				Stp_LPlan_FL = sum(0),
				Stp_LPlan_FLReef = sum(0),
				Stp_LPlan_FLWaste = sum(0),
				Stp_LPlan_Sqm = sum(0),
				Stp_LPlan_SqmReef = sum(0),
				Stp_LPlan_SqmWaste = sum(0), 
				Stp_LPlan_Adv = sum(0),
				Stp_LPlan_AdvReef = sum(0),
				Stp_LPlan_AdvWaste = sum(0),
				Stp_LPlan_SqmSW = sum(0),
				Stp_LPlan_SqmCW = sum(0),
				Stp_LPlan_SqmCmgt = sum(0),
				Stp_LPlan_SqmCmgtTotal = sum(0),
				Stp_LPlan_Tons = sum(0),
				Stp_LPlan_TonsReef = sum(0),
				Stp_LPlan_TonsWaste = sum(0),
				Stp_LPlan_Kg = sum(0),
				Stp_LPlan_Cubics = sum(0), 
				Stp_LPlan_CubicTons = sum(0),
				Stp_LPlan_CubicGrams = sum(0),

				Dev_LPlan_AdvReef = sum(0),
				Dev_LPlan_AdvWaste = sum(0),
				Dev_LPlan_Primm = sum(0),
				Dev_LPlan_Secm = sum(0),
				Dev_LPlan_Adv = sum(0),
				Dev_LPlan_TonsReef = sum(0),
				Dev_LPlan_TonsWaste = sum(0),
				Dev_LPlan_Tons = sum(0),
				Dev_LPlan_KG = sum(0),
				Dev_LPlan_AdvEH = sum(0),
				Dev_LPlan_AdvEW = sum(0),
				Dev_LPlan_AdvCmgt = sum(0),
				Dev_LPlan_AdvCmgtTotal = sum(0),
				Dev_LPlan_Cubics = sum(0),
				Dev_LPlan_CubicTons = sum(0),
				Dev_LPlan_CubicGrams = sum(0),
				Dev_LPlan_Labour = sum(0),
				Dev_LPlan_ShftInfo = sum(0),
				Dev_LPlan_DrillRig = max(''''), 

				Stp_LPPlan_FL = sum(0),
				Stp_LPPlan_FLReef = sum(0),
				Stp_LPPlan_FLWaste = sum(0),
				Stp_LPPlan_Sqm = sum(0),
				Stp_LPPlan_SqmReef = sum(0),
				Stp_LPPlan_SqmWaste = sum(0), 
				Stp_LPPlan_Adv = sum(0),
				Stp_LPPlan_AdvReef = sum(0), 
				Stp_LPPlan_AdvWaste = sum(0),
				Stp_LPPlan_SqmSW = sum(0),
				Stp_LPPlan_SqmCW = sum(0),
				Stp_LPPlan_SqmCmgt = sum(0),
				Stp_LPPlan_SqmCmgtTotal = sum(0),
				Stp_LPPlan_Tons = sum(0),
				Stp_LPPlan_TonsReef = sum(0),
				Stp_LPPlan_TonsWaste = sum(0),
				Stp_LPPlan_Kg = sum(0),
				Stp_LPPlan_Cubics = sum(0), 
				Stp_LPPlan_CubicTons = sum(0),
				Stp_LPPlan_CubicGrams = sum(0),

				Dev_LPPlan_AdvReef = sum(0),
				Dev_LPPlan_AdvWaste = sum(0),
				Dev_LPPlan_Primm = sum(0),
				Dev_LPPlan_Secm = sum(0),
				Dev_LPPlan_Adv = sum(0),
				Dev_LPPlan_TonsReef = sum(0),
				Dev_LPPlan_TonsWaste = sum(0), 
				Dev_LPPlan_Tons = sum(0),
				Dev_LPPlan_KG = sum(0),
				Dev_LPPlan_AdvEH = sum(0),
				Dev_LPPlan_AdvEW= sum(0),
				Dev_LPPlan_AdvCmgt = sum(0), 
				Dev_LPPlan_AdvCmgtTotal = sum(0), 
				Dev_LPPlan_Cubics = sum(0),
				Dev_LPPlan_CubicTons = sum(0),
				Dev_LPPlan_CubicGrams = sum(0),
				Dev_LPPlan_Labour =sum(0),
				Dev_LPPlan_ShftInfo = sum(0),
				Dev_LPPlan_DrillRig = max(''''),  
				
				Stp_Meas_FL = sum(0),
				Stp_Meas_FLReef = sum(0),
				Stp_Meas_FLWaste = sum(0),
				Stp_Meas_Sqm = sum(0),
				Stp_Meas_SqmReef = sum(0),
				Stp_Meas_SqmWaste = sum(0),
				Stp_Meas_Adv = sum(0),
				Stp_Meas_AdvReef = sum(0),
				Stp_Meas_Advwaste = sum(0),
				Stp_Meas_SqmSW = sum(0),
				Stp_Meas_SqmCW = sum(0),
				Stp_Meas_SqmCMGT = sum(0 ),
				Stp_Meas_SqmCMGTTotal = sum(0 ),				
				Stp_Meas_Tons = sum(0), 
				Stp_Meas_TonsReef = sum(0.0), 
				Stp_Meas_TonsWaste = sum(0.0),
				Stp_Meas_Kg = sum(0), 
				Stp_Meas_Cubics = sum(0),
				Stp_Meas_CubicTons = sum(0), 
				Stp_Meas_CubicGrams = sum(0), '
				
			set @BusDevSelect1 = '
				Dev_Meas_AdvReef = sum(0),
				Dev_Meas_AdvWaste = sum(0),
				Dev_Meas_Primm = sum(0),
				Dev_Meas_Secm = sum(0),
				Dev_Meas_Adv = sum(0),
				Dev_Meas_TonsReef = sum(0),
				Dev_Meas_TonsWaste = sum(0), 
				Dev_Meas_Tons = sum(0),
				Dev_Meas_KG = sum(0),
				Dev_Meas_AdvEH = sum(0),
				Dev_Meas_AdvEW = sum(0),
				Dev_Meas_AdvCmgt = sum(0),
				Dev_Meas_AdvCmgtTotal = sum(0),
				Dev_Meas_Cubics = sum(0), 
				Dev_Meas_CubicTons = sum(0),
				Dev_Meas_CubicGrams = sum(0),
				 
				BusFlag = max(''Y''), 
				Stp_BPlan_Adv = sum(0.0) ,
				Stp_BPlan_AdvReef = sum(0.0) ,
				Stp_BPlan_AdvWaste = sum(0.0) ,
				Stp_BPlan_FL = sum(0.000) ,
				Stp_BPlan_Sqm = sum(0.0) ,
				Stp_BPlan_SqmReef = sum(0.0) ,
				Stp_BPlan_SqmWaste = sum(0.0) ,
				Stp_BPlan_SqmSW = sum(0.0) ,
				Stp_BPlan_SqmCW = sum(0.0) ,
				Stp_BPlan_SqmCmgt = sum(0.0) , 
				Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
				Stp_BPlan_Tons = sum(0) ,
				Stp_BPlan_TonsReef = sum(0.0) ,
				Stp_BPlan_TonsWaste = sum(0.0),
				Stp_BPlan_Kg = sum(0),

				Stp_BPlan_OSSqm = sum(0.0),
				Stp_BPlan_OSFSqm = sum(0.0),
				Stp_BPlan_REEFFL = sum(0.0),
				Stp_BPlan_OSFL = sum(0.0),     
				Stp_BPlan_CUBICS = sum(0),
				Stp_BPlan_SqmSWFAULT = sum(0.0),
				Stp_BPlan_SqmFault = sum(0.0),
				Stp_BPlan_FaultTons = sum(0),
				Stp_BPlan_OSTons = sum(0),
				Stp_BPlan_CUBICTons = sum(0), '
			
			set @BusDevSelect1 = @BusDevSelect1 + '
				Dev_BPlan_AdvReef = sum(pm.MAdvReef),
				Dev_BPlan_AdvWaste = sum(pm.MAdvWaste),
				Dev_BPlan_Primm = sum(0),
				Dev_BPlan_Secm = sum(0), 
				Dev_BPlan_Adv = sum(pm.MAdv),
				Dev_BPlan_TonsReef = sum(pm.TonsReef),
				Dev_BPlan_TonsWaste = sum(pm.TonsWaste),
				Dev_BPlan_Tons = sum(pm.Tons), 
				Dev_BPlan_KG = sum(pm.Content / 1000),
				Dev_BPlan_AdvEH = sum(pm.MAdv * pm.Height),
				Dev_BPlan_AdvEW = sum(pm.MAdv * pm.Width),
				Dev_BPlan_AdvCmgt = sum(pm.MAdvReef * pm.Cmgt),  
				Dev_BPlan_AdvCmgtTotal = sum(0),
				Dev_BPlan_Cubics = sum(pm.Cubics),
				Dev_BPlan_CubicTons = sum(0), 
				Dev_BPlan_TonsCmgt = sum(0), 				  				 
				
				ForeCast_SQM = sum(0) ,
				ForeCast_SQMDens = sum(0) ,
				ForeCast_Grams = sum(0) ,
				ForeCast_Tons = sum(0),
				ForeCast_KG = sum(0) '			  				 

			set @BusDevFrom =  '
				from BusinessPlan_Development pm '
			IF (@RunLevel < 5)
			BEGIN
				set @BusDevFrom = @BusDevFrom + '
					left outer join 
						(select p.Prodmonth, p.SectioniD, p.workplaceid, sc.sectionID_2, sc.sectionid_1, sc.name_1, sc.name 
						from planmonth p 
						inner join  Section_Complete sc on
						sc.ProdMonth = p.ProdMonth and
						sc.SectionID = p.SectionID
						where activity=1 and
						p.plancode=''MP'' and '
				IF (@ReportType = 'M')
				BEGIN
					set @BusDevFrom = @BusDevFrom + 
						' p.ProdMonth >= '''+ @FromMonth +''' and
						  p.ProdMonth <= '''+ @ToMonth +''' '
				END
				IF (@ReportType = 'P')
				BEGIN
					set @BusDevFrom = @BusDevFrom + 
						' p.ProdMonth = '''+ @ProdMonth+''' '
				END 
				set @BusDevFrom = @BusDevFrom + ') sc on
						sc.ProdMonth = pm.prodmonth and
						sc.sectionid_2 = pm.sectionid and
						sc.workplaceid=pm.workplaceid 
					left outer join vw_Sections_Complete_MO scm on
					  scm.ProdMonth = pm.ProdMonth and
					  scm.SectionID_2 = pm.SectionID 	'
				
			END
			IF (@RunLevel > 4)
			BEGIN
				set @BusDevFrom = @BusDevFrom + '
					left outer join planmonth p on
						p.Prodmonth = pm.ProdMonth and
						p.WorkplaceID = pm.WorkplaceID and
						p.Activity = 1 and p.PlanCode = ''MP''
					left outer join Section_Complete sc on
						sc.ProdMonth = p.ProdMonth and
						sc.SectionID = p.SectionID and '
						IF (@RunLevel = 5)
						  set @BusDevFrom = @BusDevFrom + 'sc.SectionID_1 = '''+@SectionID+'''  '
						IF (@RunLevel = 6)
						  set @BusDevFrom = @BusDevFrom + 'sc.SectionID = '''+@SectionID+'''  '
			END
			set @BusDevFrom = @BusDevFrom + '			
				inner join Workplace w on
					pm.WorkplaceID = w.WorkplaceID '

			set @BusDevWhere = ''
			IF (@ReportType = 'M')
			BEGIN
				set @BusDevWhere = @BusDevWhere + 
					' where pm.ProdMonth >= '''+ @FromMonth +''' and
						pm.ProdMonth <= '''+ @ToMonth +''' '
			END
			IF (@ReportType = 'P')
			BEGIN
				set @BusDevWhere = @BusDevWhere + 
					' where pm.ProdMonth = '''+ @ProdMonth +''' '
			END 

			IF (@RunLevel = 1)
				set @BusDevWhere = @BusDevWhere + ' and scm.SectionID_5 = '''+@SectionID+''' '  

			IF (@RunLevel = 2)
				set @BusDevWhere = @BusDevWhere + ' and scm.SectionID_4 = '''+@SectionID+''' '

			IF (@RunLevel = 3)
				set @BusDevWhere = @BusDevWhere + ' and scm.SectionID_3 = '''+@SectionID+''' '

			IF (@RunLevel = 4)
				set @BusDevWhere = @BusDevWhere + ' and scm.SectionID_2 = '''+@SectionID+''' '
			IF (@RunLevel > 4)
			BEGIN
				IF (@RunLevel = 5)
					set @BusDevWhere = @BusDevWhere + ' and sc.SectionID_1 = '''+@SectionID+''' '

				IF (@RunLevel = 6)
					set @BusDevWhere = @BusDevWhere + ' and sc.SectionID = '''+@SectionID+''' '
			END


			set @BusDevWhere = @BusDevWhere + '
				group by  '

			IF (@RunLevel < 5)
			BEGIN
				IF (@RunLevel = 1)
					set @BusDevWhere = @BusDevWhere + 
						 ' scm.SectionID_5, scm.Name_5, scm.SectionID_4, scm.Name_4 ,scm.SectionID_3, scm.Name_3, scm.SectionID_2, scm.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name ' 

				IF (@RunLevel = 2)
					set @BusDevWhere = @BusDevWhere +
							' scm.SectionID_4, scm.Name_4, scm.SectionID_3, scm.Name_3, scm.SectionID_2, scm.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name' 
				IF (@RunLevel = 3)
					set @BusDevWhere = @BusDevWhere +
							' scm.SectionID_3, scm.Name_3, scm.SectionID_2, scm.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, scm.Name' 
				IF (@RunLevel = 4)
					set @BusDevWhere = @BusDevWhere +
							' scm.SectionID_2, scm.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name '
			END
			IF (@RunLevel > 4)
			BEGIN
				IF (@RunLevel = 5)
					set @BusDevWhere = @BusDevWhere +'  sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name '  

				IF (@RunLevel = 6)
					set @BusDevWhere = @BusDevWhere + ' sc.SectionID, sc.Name ' 

				IF (@RunLevel = 7)
					set @BusDevWhere = @BusDevWhere + 'Name_2, c.Name, cs.Name '  

				IF (@RunLevel = 8)
					set @BusDevWhere = @BusDevWhere + 'c.Name, cs.Name ' 


				IF (@RunLevel = 9)
					set @BusDevWhere = @BusDevWhere + ' cs.Name '  
			END

			set @BusDevWhere = @BusDevWhere + ', pm.Prodmonth, pm.WORKPLACEID, w.Description '
	END
END

IF (@ReportType = 'D') 
BEGIN
	IF (@PlanProg = 'Y') or (@Book = 'Y')
	BEGIN
		set @PlanSelect = 
		 ' select 
			sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
			sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
			sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME, '
		if @TotalsPerMonth = 'Y' 
			set @PlanSelect = @PlanSelect + ' Prodmonth = cast(pm.Prodmonth as varchar(6)), '
				else 
			set @PlanSelect = @PlanSelect + ' '''' Prodmonth, '
		set @PlanSelect = @PlanSelect + '
			pm.Workplaceid+'':''+w.Description workplace,   pm.Activity,
			Plan_Stope = case when pm.Activity = 0 then 1 else 0 end, 
			Plan_Dev = case when pm.Activity = 1 then 1 else 0 end,
			TotalShifts = max(s.TotalShifts),
			ShiftNo = max(p.ShiftDay), 
			Stp_Plan_FL = max(case when pm.Activity = 0 then isnull(pm.FL,0) else 0 end),
			Stp_Plan_FLReef = max(case when pm.Activity = 0 then isnull(pm.ReefFL,0) else 0 end),
			Stp_Plan_FLWaste = max(case when pm.Activity = 0 then isnull(pm.WasteFL,0) else 0 end),
			Stp_Plan_Sqm = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) else 0 end),
			Stp_Plan_SqmReef = max(case when pm.Activity = 0 then isnull(pm.ReefSqm,0) else 0 end),
			Stp_Plan_SqmWaste = max(case when pm.Activity = 0 then isnull(pm.wasteSqm,0) else 0 end), 
			Stp_Plan_Adv = sum(case when pm.Activity = 0 then isnull(pm.MetresAdvance,0) else 0 end),
			Stp_Plan_AdvReef = sum(case when pm.Activity = 0 then isnull(pm.ReefAdv,0) else 0 end), 
			Stp_Plan_AdvWaste = sum(case when pm.Activity = 0 then isnull(pm.WasteAdv,0) else 0 end),
			Stp_Plan_SqmSW = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) * isnull(pm.SW,0) else 0 end),
			Stp_Plan_SqmCW = max(case when pm.Activity = 0 then isnull(pm.ReefSqm,0) * isnull(pm.CW,0) else 0 end),
			Stp_Plan_SqmCmgt = max(case when pm.Activity = 0 then isnull(pm.ReefSqm,0) * isnull(pm.cmgt,0) else 0 end),
			Stp_Plan_SqmCmgtTotal = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) * isnull(pm.cmgt,0) else 0 end),
			Stp_Plan_Tons = max(case when pm.Activity = 0 then isnull(pm.Tons,0) else 0 end), 
			Stp_Plan_TonsReef = max(case when pm.Activity = 0 then isnull(pm.ReefTons,0) else 0 end),
			Stp_Plan_TonsWaste = max(case when pm.Activity = 0 then isnull(pm.WasteTons,0) else 0 end),
			Stp_Plan_Kg = max(case when pm.Activity = 0 then isnull(pm.KG,0) / 1000 else 0 end), 
			Stp_Plan_Cubics = max(case when pm.Activity = 0 then isnull(pm.Cubics,0) else 0 end), 
			Stp_Plan_CubicTons = max(case when pm.Activity = 0 then isnull(pm.CubicsTons,0) else 0 end), 
			Stp_Plan_CubicGrams = max(case when pm.Activity = 0 then isnull(pm.CubicGrams,0) else 0 end), 

			Dev_Plan_AdvReef = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) else 0 end),
			Dev_Plan_AdvWaste = max(case when pm.Activity = 1 then isnull(pm.WasteAdv,0) else 0 end),
			Dev_Plan_Primm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
			Dev_Plan_Secm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end), 
			Dev_Plan_Adv = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
			Dev_Plan_TonsReef = max(case when pm.Activity = 1 then isnull(pm.ReefTons,0) else 0 end),
			Dev_Plan_TonsWaste = max(case when pm.Activity = 1 then isnull(pm.WasteTons,0) else 0 end),
			Dev_Plan_Tons = max(case when pm.Activity = 1 then isnull(pm.Tons,0) else 0 end), 
			Dev_Plan_KG = max(case when pm.Activity = 1 then isnull(pm.KG,0) / 1000 else 0 end),
			Dev_Plan_AdvEH = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DHeight,0) else 0 end),
			Dev_Plan_AdvEW = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DWidth,0) else 0 end), 
			Dev_Plan_AdvCmgt = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) * isnull(pm.CMGT,0) else 0 end), 
			Dev_Plan_AdvCmgtTotal = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(pm.CMGT,0) else 0 end),
			Dev_Plan_Cubics = max(case when pm.Activity = 1 then isnull(pm.Cubics,0) else 0 end), 
			Dev_Plan_CubicTons = max(case when pm.Activity = 1 then isnull(pm.CubicsTons,0) else 0 end), 
			Dev_Plan_CubicGrams = max(case when pm.Activity = 1 then isnull(pm.CubicGrams,0) / 1000 else 0 end), 
			Dev_Plan_Labour = max(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
			Dev_Plan_ShftInfo = max(0), 
			Dev_Plan_DrillRig = max(case when pm.Activity = 1 then isnull(pm.DrillRig,'''') else '''' end), 

			-- DPLAN - Progessive Plan
			Stp_PPlan_FL = sum(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
			Stp_PPlan_FLReef = sum(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.ReefFL,0) else 0 end),
			Stp_PPlan_FLWaste = sum(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.WasteFL,0) else 0 end),
			Stp_PPlan_Sqm = sum(case when '+@TheStopeLedge1+' then isnull(p.Sqm,0) else 0 end),
			Stp_PPlan_SqmReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefSqm,0) else 0 end), 
			Stp_PPlan_SqmWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteSqm,0) else 0 end),
			Stp_PPlan_Adv = sum(case when '+@TheStopeLedge1+' then isnull(p.MetresAdvance,0) else 0 end),
			Stp_PPlan_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefAdv,0) else 0 end), 
			Stp_PPlan_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteAdv,0) else 0 end),
			Stp_PPlan_SqmSW = sum(case when '+@TheStopeLedge1+' then isnull(pm.SW,0) * isnull(p.Sqm,0) else 0 end), 
			Stp_PPlan_SqmCW = sum(case when '+@TheStopeLedge1+' then isnull(pm.CW,0) * isnull(p.ReefSqm,0) else 0 end),
			Stp_PPlan_SqmCmgt = sum(case when '+@TheStopeLedge1+' then isnull(pm.cmgt,0) * isnull(p.ReefSqm,0) else 0 end), 
			Stp_PPlan_SqmCmgtTotal = sum(case when '+@TheStopeLedge1+' then isnull(pm.cmgt,0) * isnull(p.Sqm,0) else 0 end),
			Stp_PPlan_Tons = sum(case when '+@TheStopeLedge1+' then isnull(p.Tons,0) else 0 end),
			Stp_PPlan_TonsReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefTons,0) else 0 end), 
			Stp_PPlan_TonsWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteTons,0) else 0 end),
			Stp_PPlan_Kg = sum(case when '+@TheStopeLedge1+' then isnull(p.Grams,0) / 1000 else 0 end), 
			Stp_PPlan_Cubics = max(case when '+@TheStopeLedge1+' then isnull(p.Cubics,0) else 0 end), 
			Stp_PPlan_CubicTons = max(case when '+@TheStopeLedge1+' then isnull(p.CubicTons,0) else 0 end), 
			Stp_PPlan_CubicGrams = max(case when '+@TheStopeLedge1+' then isnull(p.CubicGrams,0) else 0 end), '

		set @PlanSelect1 = '
			Dev_PPlan_AdvReef = sum(case when p.Activity = 1 then p.ReefAdv else 0 end), 
			Dev_PPlan_AdvWaste = sum(case when p.Activity = 1 then p.WasteAdv else 0 end), 
			Dev_PPlan_Primm = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_Secm = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_Adv = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_TonsReef = sum(case when p.Activity = 1 then p.ReefTons else 0 end), 
			Dev_PPlan_TonsWaste = sum(case when p.Activity = 1 then p.WasteTons else 0 end), 
			Dev_PPlan_Tons = sum(case when p.Activity = 1 then p.Tons else 0 end), 
			Dev_PPlan_KG = sum(case when p.Activity = 1 then p.Grams / 1000 else 0 end), 
			Dev_PPlan_AdvEH = sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * pm.DHeight else 0 end), 
			Dev_PPlan_AdvEW= sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * pm.DWidth else 0 end), 
			Dev_PPlan_AdvCmgt = sum(case when p.Activity = 1 and p.ReefAdv > 0 then p.ReefAdv * p.Cmgt else 0 end), 
			Dev_PPlan_AdvCmgtTotal = sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * p.Cmgt else 0 end), 
			Dev_PPlan_Cubics = Sum(case when p.Activity = 1 then p.Cubics else 0 end), 
			Dev_PPlan_CubicTons = Sum(case when p.Activity = 1 then p.CubicTons else 0 end), 
			Dev_PPlan_CubicGrams = Sum(case when p.Activity = 1 then p.CubicGrams / 1000 else 0 end), 
			Dev_PPlan_Labour = Sum(case when p.Activity = 1 then pm.LabourStrength else 0 end), 
			Dev_PPlan_ShftInfo = Sum(0), 
			Dev_PPlan_DrillRig = max(''''),  

			Stp_Book_FL = max(case when '+@TheStopeLedge1+' then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_FLReef = max(case when '+@TheStopeLedge1+' then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_FLWaste = max(case when '+@TheStopeLedge1+' then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_Sqm = sum(case when '+@TheStopeLedge1+' then isnull(p.BookSqm,0) else 0 end),
			Stp_Book_SqmReef = sum(case when '+@TheStopeLedge1+' then isnull(p.BookReefSqm,0) else 0 end), 
			Stp_Book_SqmWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.BookWasteSqm,0) else 0 end),
			Stp_Book_Adv = sum(case when '+@TheStopeLedge1+' then isnull(p.BookMetresAdvance,0) else 0 end),
			Stp_Book_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(p.BookReefAdv,0) else 0 end), 
			Stp_Book_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.BookWasteAdv,0) else 0 end),
			Stp_Book_SqmSW = sum(case when '+@TheStopeLedge1+' then isnull(p.BookSW,0) * isnull(p.BookSqm,0) else 0 end),
			Stp_Book_SqmCW = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCW,0) * isnull(p.BookReefSqm,0) else 0 end), 
			Stp_Book_SqmCmgt = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCmgt,0) * isnull(p.BookReefSqm,0) else 0 end),
			Stp_Book_SqmCmgtTotal = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCmgt,0) * isnull(p.BookSqm,0) else 0 end),
			Stp_Book_Tons = sum(case when '+@TheStopeLedge1+' then isnull(p.BookTons,0) else 0 end), 
			Stp_Book_TonsReef = sum(case when '+@TheStopeLedge1+' then isnull(p.BookReefTons,0) else 0 end),
			Stp_Book_TonsWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.BookWasteTons,0) else 0 end), 
			Stp_Book_KG = sum(case when '+@TheStopeLedge1+' then isnull(p.BookGrams,0) / 1000 else 0 end),
			Stp_Book_Cubics = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCubics,0) else 0 end), 
			Stp_Book_CubicTons = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCubicTons,0) else 0 end), 
			Stp_Book_CubicGrams = sum(case when '+@TheStopeLedge1+' then isnull(p.BookCubicGrams,0) else 0 end), 

			Dev_Book_AdvReef = sum(case when p.Activity = 1 then isnull(p.BookReefAdv,0) else 0 end), 
			Dev_Book_AdvWaste = sum(case when p.Activity = 1 then isnull(p.BookWasteAdv,0) else 0 end), 
			Dev_Book_Primm = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_Secm = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_Adv = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_TonsReef = sum(case when p.Activity = 1 then isnull(p.BookReefTons,0) else 0 end),
			Dev_Book_TonsWaste = sum(case when p.Activity = 1 then isnull(p.BookWasteTons,0) else 0 end), 
			Dev_Book_Tons = sum(case when p.Activity = 1 then isnull(p.BookTons,0) else 0 end), 
			Dev_Book_KG = sum(case when p.Activity = 1 then isnull(p.BookGrams,0) / 1000 else 0 end), 
			Dev_Book_AdvEH = sum(case when p.Activity = 1 and isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(pm.DHeight,0) * isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_AdvEW = sum(case when p.Activity = 1 and isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(pm.DWidth,0) * isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_AdvCmgt = sum(case when isnull(p.BookReefAdv,0) > 0  then 
						isnull(p.Bookcmgt,0) * isnull(p.BookReefAdv,0) else 0 end),
			Dev_Book_AdvCmgtTotal = sum(case when isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(p.BookMetresAdvance,0) * isnull(p.Cmgt,0) else 0 end), 
			Dev_Book_Cubics = Sum(case when p.Activity = 1 then isnull(p.BookCubicMetres,0) else 0 end), 
			Dev_Book_CubicTons = Sum(case when p.Activity = 1 then isnull(p.BookCubicTons,0) else 0 end), 
			Dev_Book_CubicGrams = Sum(case when p.Activity = 1 then isnull(p.BookCubicGrams,0) / 1000 else 0 end), 
			--Dev_Book_Labour = Sum(pm.LabourStrength), 
			--Dev_Book_ShftInfo = Sum(p.ShftInfo), 

			Stp_LPlan_FL = sum(0),
			Stp_LPlan_FLReef = sum(0),
			Stp_LPlan_FLWaste = sum(0),
			Stp_LPlan_Sqm = sum(0),
			Stp_LPlan_SqmReef = sum(0),
			Stp_LPlan_SqmWaste = sum(0), 
			Stp_LPlan_Adv = sum(0),
			Stp_LPlan_AdvReef = sum(0),
			Stp_LPlan_AdvWaste = sum(0),
			Stp_LPlan_SqmSW = sum(0),
			Stp_LPlan_SqmCW = sum(0),
			Stp_LPlan_SqmCmgt = sum(0),
			Stp_LPlan_SqmCmgtTotal = sum(0),
			Stp_LPlan_Tons = sum(0),
			Stp_LPlan_TonsReef = sum(0),
			Stp_LPlan_TonsWaste = sum(0),
			Stp_LPlan_Kg = sum(0),
			Stp_LPlan_Cubics = sum(0),
			Stp_LPlan_CubicTons = sum(0), 
			Stp_LPlan_CubicGrams = sum(0), '

		set @PlanSelect2 = '
			Dev_LPlan_AdvReef = sum(0),
			Dev_LPlan_AdvWaste = sum(0),
			Dev_LPlan_Primm = sum(0),
			Dev_LPlan_Secm = sum(0),
			Dev_LPlan_Adv = sum(0),
			Dev_LPlan_TonsReef = sum(0),
			Dev_LPlan_TonsWaste = sum(0),
			Dev_LPlan_Tons = sum(0),
			Dev_LPlan_KG = sum(0),
			Dev_LPlan_AdvEH = sum(0),
			Dev_LPlan_AdvEW = sum(0), 
			Dev_LPlan_AdvCmgt = sum(0),
			Dev_LPlan_AdvCmgtTotal = sum(0),
			Dev_LPlan_Cubics = sum(0),
			Dev_LPlan_CubicTons = sum(0),
			Dev_LPlan_CubicGrams = sum(0),
			Dev_LPlan_Labour = sum(0),
			Dev_LPlan_ShftInfo = sum(0), 
			Dev_LPlan_DrillRig = max(''''), 

			Stp_LPPlan_FL = sum(0),
			Stp_LPPlan_FLReef = sum(0),
			Stp_LPPlan_FLWaste = sum(0),
			Stp_LPPlan_Sqm = sum(0),
			Stp_LPPlan_SqmReef = sum(0),
			Stp_LPPlan_SqmWaste = sum(0), 
			Stp_LPPlan_Adv = sum(0),
			Stp_LPPlan_AdvReef = sum(0), 
			Stp_LPPlan_AdvWaste = sum(0),
			Stp_LPPlan_SqmSW = sum(0),
			Stp_LPPlan_SqmCW = sum(0),
			Stp_LPPlan_SqmCmgt = sum(0),
			Stp_LPPlan_SqmCmgtTotal = sum(0),
			Stp_LPPlan_Tons = sum(0),
			Stp_LPPlan_TonsReef = sum(0),
			Stp_LPPlan_TonsWaste = sum(0),
			Stp_LPPlan_Kg = sum(0),
			Stp_LPPlan_Cubics = sum(0),
			Stp_LPPlan_CubicTons = sum(0), 
			Stp_LPPlan_CubicGrams = sum(0),

			Dev_LPPlan_AdvReef = sum(0),
			Dev_LPPlan_AdvWaste = sum(0),
			Dev_LPPlan_Primm = sum(0),
			Dev_LPPlan_Secm = sum(0),
			Dev_LPPlan_Adv = sum(0), 
			Dev_LPPlan_TonsReef = sum(0),
			Dev_LPPlan_TonsWaste = sum(0),
			Dev_LPPlan_Tons = sum(0),
			Dev_LPPlan_KG = sum(0),
			Dev_LPPlan_AdvEH = sum(0),
			Dev_LPPlan_AdvEW = sum(0),
			Dev_LPPlan_AdvCmgt = sum(0),
			Dev_LPPlan_AdvCmgtTotal = sum(0),
			Dev_LPPlan_Cubics = sum(0),
			Dev_LPPlan_CubicTons = sum(0),
			Dev_LPPlan_CubicGrams = sum(0),
			Dev_LPPlan_Labour = sum(0),
			Dev_LPPlan_ShftInfo = sum(0),
			Dev_LPPlan_DrillRig = max(''''), 

			Stp_Meas_FL = sum(0),
			Stp_Meas_FLReef = sum(0),
			Stp_Meas_FLWaste = sum(0),
			Stp_Meas_Sqm = sum(0),
			Stp_Meas_SqmReef = sum(0),
			Stp_Meas_SqmWaste = sum(0),
			Stp_Meas_Adv = sum(0),
			Stp_Meas_AdvReef = sum(0),
			Stp_Meas_Advwaste = sum(0),
			Stp_Meas_SqmSW = sum(0),
			Stp_Meas_SqmCW = sum(0),
			Stp_Meas_SqmCMGT = sum(0 ),
			Stp_Meas_SqmCMGTTotal = sum(0 ),				
			Stp_Meas_Tons = sum(0), 
			Stp_Meas_TonsReef = sum(0.0), 
			Stp_Meas_TonsWaste = sum(0.0),
			Stp_Meas_Kg = sum(0), 
			Stp_Meas_Cubics = sum(0),
			Stp_Meas_CubicTons = sum(0), 
			Stp_Meas_CubicGrams = sum(0),

			Dev_Meas_AdvReef = sum(0),
			Dev_Meas_AdvWaste = sum(0),
			Dev_Meas_Primm = sum(0),
			Dev_Meas_Secm = sum(0),
			Dev_Meas_Adv = sum(0),
			Dev_Meas_TonsReef = sum(0),
			Dev_Meas_TonsWaste = sum(0), 
			Dev_Meas_Tons = sum(0),
			Dev_Meas_KG = sum(0),
			Dev_Meas_AdvEH = sum(0),
			Dev_Meas_AdvEW = sum(0),
			Dev_Meas_AdvCmgt = sum(0),
			Dev_Meas_AdvCmgtTotal = sum(0),
			Dev_Meas_Cubics = sum(0), 
			Dev_Meas_CubicTons = sum(0),
			Dev_Meas_CubicGrams = sum(0),
				
			BusFlag = max(''N''), 
			Stp_BPlan_FL = sum(0.000) ,
			Stp_BPlan_Adv = sum(0.000) ,
			Stp_BPlan_AdvReef = sum(0.000) ,
			Stp_BPlan_AdvWaste = sum(0.000) ,
			Stp_BPlan_Sqm = sum(0.0) ,
			Stp_BPlan_SqmReef = sum(0.0) ,
			Stp_BPlan_SqmWaste = sum(0.0) ,
			Stp_BPlan_SqmSW = sum(0.0) ,
			Stp_BPlan_SqmCW = sum(0.0) ,
			Stp_BPlan_SqmCmgt = sum(0.0) , 
			Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
			Stp_BPlan_Tons = sum(0) ,
			Stp_BPlan_TonsReef = sum(0.0) ,
			Stp_BPlan_TonsWaste = sum(0.0),
			Stp_BPlan_Kg = sum(0),

			Stp_BPlan_OSSqm = sum(0.0),
			Stp_BPlan_OSFSqm = sum(0.0),
			Stp_BPlan_REEFFL = sum(0.0),
			Stp_BPlan_OSFL = sum(0.0),     
			Stp_BPlan_CUBICS = sum(0),
			Stp_BPlan_SqmSWFAULT = sum(0.0),
			Stp_BPlan_SqmFault = sum(0.0),
			Stp_BPlan_FaultTons = sum(0),
			Stp_BPlan_OSTons = sum(0),
			Stp_BPlan_CUBICTons = sum(0), 

			Dev_BPlan_AdvReef = sum(0),
			Dev_BPlan_AdvWaste = sum(0),
			Dev_BPlan_Primm = sum(0),
			Dev_BPlan_Secm = sum(0), 
			Dev_BPlan_Adv = sum(0),
			Dev_BPlan_TonsReef = sum(0),
			Dev_BPlan_TonsWaste = sum(0),
			Dev_BPlan_Tons = sum(0),
			Dev_BPlan_KG = sum(0),
			Dev_BPlan_AdvEH = sum(0),
			Dev_BPlan_AdvEW = sum(0),
			Dev_BPlan_AdvCmgt = sum(0),
			Dev_BPlan_AdvCmgtTotal = sum(0),
			Dev_BPlan_Cubics = sum(0),
			Dev_BPlan_CubicTons = sum(0),
			Dev_BPlan_TonsCmgt = sum(0) ,
			
			ForeCast_SQM = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							sum(p.BookSQM) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end, 
			ForeCast_SQMDens = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							sum(p.BookSQM) * max(pm.Density) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end, 
			ForeCast_Grams = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							sum(p.BookGrams) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end,
			ForeCast_Tons = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							sum(p.BookTons) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end,
			ForeCast_KG = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							(sum(p.BookGrams) / 1000) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end '
	set @PlanFrom = '
		from Planmonth pm   
		inner join Planning p on 
			p.Prodmonth = pm.Prodmonth and 
			p.SectionID = pm.Sectionid and 
			p.WorkplaceID = pm.Workplaceid and 
			p.Activity = pm.Activity and
			p.PlanCode = pm.PlanCode and
			p.IsCubics = pm.IsCubics 
		inner join section_complete sc on  
				sc.prodmonth = pm.prodmonth and  
				sc.sectionid  = pm.sectionid
		inner join  (select distinct(ProdMonth) ProdMonth , SectionID,TotalShifts from seccal ) s on
			s.ProdMonth = sc.prodmonth and
			s.SectionID = sc.SectionID_1  
		inner join Workplace w on  
				w.WorkplaceID = pm.WorkplaceID 
		inner join CommonAreaSubSections CS on
				cs.Id = w.SubSection 
		inner join CommonAreas c on
				c.Id = cs.CommonArea '
		SET @PlanWhere = 
			' where pm.PlanCode = ''MP'' and pm.IsCubics = ''N'' and 
					p.CalendarDate >= '''+@FromDate+''' and 
					p.CalendarDate <= '''+@ToDate+''' and 
				'+@ReadSection+' = '''+@SectionID+''' '
		set @PlanWhere = @PlanWhere + '
			group by pm.Prodmonth, sc.SectionID_5, sc.Name_5, sc.SectionID_4, sc.Name_4, sc.SectionID_3, sc.Name_3,
						sc.SectionID_2, sc.Name_2, sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name,
						pm.WorkplaceiD, w.Description, pm.Activity '
	END
	IF (@PlanProgLock = 'Y')
	BEGIN
		IF (@PlanProg = 'Y')
			set @LPlanUnion = 'union all '
		set @LPlanSelect =  
			' select 
				sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
				sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
				sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME, '
		if @TotalsPerMonth = 'Y' 
			set @LPlanSelect = @LPlanSelect + ' Prodmonth = cast(pm.Prodmonth as varchar(6)), '
				else 
			set @LPlanSelect = @LPlanSelect + ' '''' Prodmonth, '
		set @LPlanSelect = @LPlanSelect + '
				pm.Workplaceid+'':''+w.Description workplace,   pm.Activity,
				Plan_Stope = case when pm.Activity in (0,3) then 1 else 0 end, 
				Plan_Dev = case when pm.Activity in (1) then 1 else 0 end,
				TotalShifts = max(s.TotalShifts),
				ShiftNo = max(p.ShiftDay), 
				Stp_Plan_FL = sum(0),
				Stp_Plan_FLReef = sum(0),
				Stp_Plan_FLWaste = sum(0),
				Stp_Plan_Sqm = sum(0),
				Stp_Plan_SqmReef = sum(0),
				Stp_Plan_SqmWaste = sum(0), 
				Stp_Plan_Adv = sum(0),
				Stp_Plan_AdvReef = sum(0),
				Stp_Plan_AdvWaste = sum(0),
				Stp_Plan_SqmSW = sum(0),
				Stp_Plan_SqmCW = sum(0),
				Stp_Plan_SqmCmgt = sum(0),
				Stp_Plan_SqmCmgtTotal = sum(0),
				Stp_Plan_Tons = sum(0),
				Stp_Plan_TonsReef = sum(0),
				Stp_Plan_TonsWaste = sum(0),
				Stp_Plan_Kg = sum(0),
				Stp_Plan_Cubics = sum(0), 
				Stp_Plan_CubicTons = sum(0),
				Stp_Plan_CubicGrams = sum(0),

				Dev_Plan_AdvReef = sum(0),
				Dev_Plan_AdvWaste = sum(0),
				Dev_Plan_Primm = sum(0),
				Dev_Plan_Secm = sum(0),
				Dev_Plan_Adv = sum(0),
				Dev_Plan_TonsReef = sum(0),
				Dev_Plan_TonsWaste = sum(0),
				Dev_Plan_Tons = sum(0),
				Dev_Plan_KG = sum(0),
				Dev_Plan_AdvEH = sum(0),
				Dev_Plan_AdvEW = sum(0),
				Dev_Plan_AdvCmgt = sum(0),
				Dev_Plan_AdvCmgtTotal = sum(0),
				Dev_Plan_Cubics = sum(0), 
				Dev_Plan_CubicTons = sum(0),
				Dev_Plan_CubicGrams = sum(0),
				Dev_Plan_Labour = sum(0),
				Dev_Plan_ShftInfo = sum(0),
				Dev_Plan_DrillRig = max(''''), 

				Stp_PPlan_FL = sum(0),
				Stp_PPlan_FLReef = sum(0),
				Stp_PPlan_FLWaste = sum(0),
				Stp_PPlan_Sqm = sum(0),
				Stp_PPlan_SqmReef = sum(0),
				Stp_PPlan_SqmWaste = sum(0), 
				Stp_PPlan_Adv = sum(0),
				Stp_PPlan_AdvReef = sum(0), 
				Stp_PPlan_AdvWaste = sum(0),
				Stp_PPlan_SqmSW = sum(0),
				Stp_PPlan_SqmCW = sum(0),
				Stp_PPlan_SqmCmgt = sum(0),
				Stp_PPlan_SqmCmgtTotal = sum(0),
				Stp_PPlan_Tons = sum(0),
				Stp_PPlan_TonsReef = sum(0),
				Stp_PPlan_TonsWaste = sum(0),
				Stp_PPlan_Kg = sum(0),
				Stp_PPlan_Cubics = sum(0), 
				Stp_PPlan_CubicTons = sum(0),
				Stp_PPlan_CubicGrams = sum(0),

				Dev_PPlan_AdvReef = sum(0),
				Dev_PPlan_AdvWaste = sum(0), 
				Dev_PPlan_Primm = sum(0),
				Dev_PPlan_Secm = sum(0),
				Dev_PPlan_Adv = sum(0),
				Dev_PPlan_TonsReef = sum(0), 
				Dev_PPlan_TonsWaste = sum(0), 
				Dev_PPlan_Tons = sum(0), 
				Dev_PPlan_KG = sum(0),
				Dev_PPlan_AdvEH = sum(0),
				Dev_PPlan_AdvEW = sum(0),
				Dev_PPlan_AdvCmgt = sum(0),
				Dev_PPlan_AdvCmgtTotal = sum(0),
				Dev_PPlan_Cubics = sum(0),
				Dev_PPlan_CubicTons = sum(0),
				Dev_PPlan_CubicGrams = sum(0),
				Dev_PPlan_Labour = sum(0),
				Dev_PPlan_ShftInfo = sum(0), 
				Dev_PPlan_DrillRig = max(''''),  

				Stp_Book_FL = sum(0),
				Stp_Book_FLReef = sum(0),
				Stp_Book_FLWaste = sum(0),
				Stp_Book_Sqm = sum(0),
				Stp_Book_SqmReef = sum(0),
				Stp_Book_SqmWaste = sum(0),
				Stp_Book_Adv = sum(0),
				Stp_Book_AdvReef = sum(0),
				Stp_Book_AdvWaste = sum(0),
				Stp_Book_SqmSW = sum(0),
				Stp_Book_SqmCW = sum(0), 
				Stp_Book_SqmCmgt = sum(0),
				Stp_Book_SqmCmgtTotal = sum(0),
				Stp_Book_Tons = sum(0),
				Stp_Book_TonsReef = sum(0),
				Stp_Book_TonsWaste = sum(0), 
				Stp_Book_KG = sum(0),
				Stp_Book_Cubics = sum(0), 
				Stp_Book_CubicTons = sum(0),
				Stp_Book_CubicGrams = sum(0),

				Dev_Book_AdvReef = sum(0),
				Dev_Book_AdvWaste = sum(0),
				Dev_Book_Primm = sum(0),
				Dev_Book_Secm = sum(0), 
				Dev_Book_Adv = sum(0), 
				Dev_Book_TonsReef = sum(0),
				Dev_Book_TonsWaste = sum(0),
				Dev_Book_Tons = sum(0), 
				Dev_Book_KG = sum(0), 
				Dev_Book_AdvEH = sum(0), 
				Dev_Book_AdvEW = sum(0),
				Dev_Book_AdvCmgt = sum(0),
				Dev_Book_AdvCmgtTotal = sum(0),
				Dev_Book_Cubics = sum(0),
				Dev_Book_CubicTons = sum(0),
				Dev_Book_CubicGrams = sum(0),
				--Dev_Book_Labour = sum(0), 
				--Dev_Book_ShftInfo = sum(0),

				Stp_LPlan_FL = max(case when '+@TheStopeLedge1+' then isnull(pm.FL,0) else 0 end),
				Stp_LPlan_FLReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefFL,0) else 0 end),
				Stp_LPlan_FLWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.WasteFL,0) else 0 end),
				Stp_LPlan_Sqm = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) else 0 end),
				Stp_LPlan_SqmReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefSqm,0) else 0 end),
				Stp_LPlan_SqmWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.wasteSqm,0) else 0 end), 
				Stp_LPlan_Adv = sum(case when '+@TheStopeLedge1+' then isnull(pm.MetresAdvance,0) else 0 end),
				Stp_LPlan_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(pm.ReefAdv,0) else 0 end), 
				Stp_LPlan_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(pm.WasteAdv,0) else 0 end),
				Stp_LPlan_SqmSW = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) * isnull(pm.SW,0) else 0 end),
				Stp_LPlan_SqmCW = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) * isnull(pm.CW,0) else 0 end),
				Stp_LPlan_SqmCmgt = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefSqm,0) * isnull(pm.cmgt,0) else 0 end),
				Stp_LPlan_SqmCmgtTotal = max(case when '+@TheStopeLedge1+' then isnull(pm.Sqm,0) * isnull(pm.cmgt,0) else 0 end),
				Stp_LPlan_Tons = max(case when '+@TheStopeLedge1+' then isnull(pm.Tons,0) else 0 end), 
				Stp_LPlan_TonsReef = max(case when '+@TheStopeLedge1+' then isnull(pm.ReefTons,0) else 0 end),
				Stp_LPlan_TonsWaste = max(case when '+@TheStopeLedge1+' then isnull(pm.WasteTons,0) else 0 end),
				Stp_LPlan_Kg = sum(case when '+@TheStopeLedge1+' then isnull(pm.KG,0) / 1000 else 0 end),
				Stp_LPlan_Cubics = max(case when '+@TheStopeLedge1+' then isnull(pm.Cubics,0) else 0 end), 
				Stp_LPlan_CubicTons = max(case when '+@TheStopeLedge1+' then isnull(pm.CubicsTons,0) else 0 end), 
				Stp_LPlan_CubicGrams = max(case when '+@TheStopeLedge1+' then isnull(pm.CubicGrams,0) else 0 end), ' 

			set @LPlanSelect1 = '
				Dev_LPlan_AdvReef = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) else 0 end),
				Dev_LPlan_AdvWaste = max(case when pm.Activity = 1 then isnull(pm.WasteAdv,0) else 0 end),
				Dev_LPlan_Primm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
				Dev_LPlan_Secm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end), 
				Dev_LPlan_Adv = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
				Dev_LPlan_TonsReef = max(case when pm.Activity = 1 then isnull(pm.ReefTons,0) else 0 end),
				Dev_LPlan_TonsWaste = max(case when pm.Activity = 1 then isnull(pm.WasteTons,0) else 0 end),
				Dev_LPlan_Tons = max(case when pm.Activity = 1 then isnull(pm.Tons,0) else 0 end), 
				Dev_LPlan_KG = max(case when pm.Activity = 1 then isnull(pm.KG,0) / 1000 else 0 end),
				Dev_LPlan_AdvEH = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DHeight,0) else 0 end),
				Dev_LPlan_AdvEW = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DWidth,0) else 0 end), 
				Dev_LPlan_AdvCmgt = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) * isnull(pm.CMGT,0) else 0 end), 
				Dev_LPlan_AdvCmgtTotal = sum(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(pm.CMGT,0) else 0 end),
				Dev_LPlan_Cubics = Sum(case when pm.Activity = 1 then isnull(pm.Cubics,0) else 0 end), 
				Dev_LPlan_CubicTons = Sum(case when pm.Activity = 1 then isnull(pm.CubicsTons,0) else 0 end), 
				Dev_LPlan_CubicGrams = Sum(case when pm.Activity = 1 then isnull(pm.CubicGrams,0) / 1000 else 0 end), 
				Dev_LPlan_Labour = Sum(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
				Dev_LPlan_ShftInfo = Sum(0), 
				Dev_LPlan_DrillRig = max(''''), 
				
				-- DPLAN - Progessive Plan
				Stp_LPPlan_FL = sum(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
				Stp_LPPlan_FLReef = sum(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
				Stp_LPPlan_FLWaste = sum(case when '+@TheStopeLedge1+' and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
				Stp_LPPlan_Sqm = sum(case when '+@TheStopeLedge1+' then isnull(p.Sqm,0) else 0 end),
				Stp_LPPlan_SqmReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefSqm,0) else 0 end), 
				Stp_LPPlan_SqmWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteSqm,0) else 0 end),
				Stp_LPPlan_Adv = sum(case when '+@TheStopeLedge1+' then isnull(p.MetresAdvance,0) else 0 end),
				Stp_LPPlan_AdvReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefAdv,0) else 0 end), 
				Stp_LPPlan_AdvWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteAdv,0) else 0 end),
				Stp_LPPlan_SqmSW = sum(case when '+@TheStopeLedge1+' then isnull(pm.SW,0) * isnull(p.Sqm,0) else 0 end), 
				Stp_LPPlan_SqmCW = sum(case when '+@TheStopeLedge1+' then isnull(pm.CW,0) * isnull(p.Sqm,0) else 0 end),
				Stp_LPPlan_SqmCmgt = sum(case when '+@TheStopeLedge1+' then isnull(pm.cmgt,0) * isnull(p.ReefSqm,0) else 0 end), 
				Stp_LPPlan_SqmCmgtTotal = sum(case when '+@TheStopeLedge1+' then isnull(pm.cmgt,0) * isnull(p.Sqm,0) else 0 end),
				Stp_LPPlan_Tons = sum(case when '+@TheStopeLedge1+' then isnull(p.Tons,0) else 0 end),
				Stp_LPPlan_TonsReef = sum(case when '+@TheStopeLedge1+' then isnull(p.ReefTons,0) else 0 end), 
				Stp_LPPlan_TonsWaste = sum(case when '+@TheStopeLedge1+' then isnull(p.WasteTons,0) else 0 end),
				Stp_LPPlan_Kg = sum(case when '+@TheStopeLedge1+' then isnull(p.Grams,0) / 1000 else 0 end),
				Stp_LPPlan_Cubics = max(case when '+@TheStopeLedge1+' then isnull(p.Cubics,0) else 0 end), 
				Stp_LPPlan_CubicTons = max(case when '+@TheStopeLedge1+' then isnull(p.CubicTons,0) else 0 end), 
				Stp_LPPlan_CubicGrams = max(case when '+@TheStopeLedge1+' then isnull(p.CubicGrams,0) else 0 end), 
					
				Dev_LPPlan_AdvReef = sum(case when pm.Activity = 1 then isnull(p.ReefAdv,0) else 0 end), 
				Dev_LPPlan_AdvWaste = sum(case when pm.Activity = 1 then isnull(p.WasteAdv,0) else 0 end), 
				Dev_LPPlan_Primm = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
				Dev_LPPlan_Secm = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
				Dev_LPPlan_Adv = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
				Dev_LPPlan_TonsReef = sum(case when pm.Activity = 1 then isnull(p.ReefTons,0) else 0 end), 
				Dev_LPPlan_TonsWaste = sum(case when pm.Activity = 1 then isnull(p.WasteTons,0) else 0 end), 
				Dev_LPPlan_Tons = sum(case when pm.Activity = 1 then isnull(p.Tons,0) else 0 end), 
				Dev_LPPlan_KG = sum(case when pm.Activity = 1 then isnull(p.Grams,0) / 1000 else 0 end), 
				Dev_LPPlan_AdvEH = sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
							isnull(p.MetresAdvance,0) * isnull(pm.DHeight,0) else 0 end), 
				Dev_LPPlan_AdvEW= sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
							isnull(p.MetresAdvance,0) * isnull(pm.DWidth,0) else 0 end), 
				Dev_LPPlan_AdvCmgt = sum(case when pm.Activity = 1 and isnull(p.ReefAdv,0) > 0 then 
							isnull(p.ReefAdv,0) * isnull(p.Cmgt,0) else 0 end), 
				Dev_LPPlan_AdvCmgtTotal = sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
							isnull(p.MetresAdvance,0) * isnull(p.Cmgt,0) else 0 end), 
				Dev_LPPlan_Cubics = Sum(case when pm.Activity = 1 then isnull(p.Cubics,0) else 0 end), 
				Dev_LPPlan_CubicTons = Sum(case when pm.Activity = 1 then isnull(p.CubicTons,0) else 0 end), 
				Dev_LPPlan_CubicGrams = Sum(case when pm.Activity = 1 then isnull(p.CubicGrams,0) / 1000 else 0 end), 
				Dev_LPPlan_Labour = Sum(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
				Dev_LPPlan_ShftInfo = Sum(0), 
				Dev_LPPlan_DrillRig = max(''''),  '

			set @LPlanSelect2 = '
				Stp_Meas_FL = sum(0),
				Stp_Meas_FLReef = sum(0),
				Stp_Meas_FLWaste = sum(0),
				Stp_Meas_Sqm = sum(0),
				Stp_Meas_SqmReef = sum(0),
				Stp_Meas_SqmWaste = sum(0),
				Stp_Meas_Adv = sum(0),
				Stp_Meas_AdvReef = sum(0),
				Stp_Meas_Advwaste = sum(0),
				Stp_Meas_SqmSW = sum(0),
				Stp_Meas_SqmCW = sum(0),
				Stp_Meas_SqmCMGT = sum(0 ),
				Stp_Meas_SqmCMGTTotal = sum(0 ),				
				Stp_Meas_Tons = sum(0), 
				Stp_Meas_TonsReef = sum(0.0), 
				Stp_Meas_TonsWaste = sum(0.0),
				Stp_Meas_Kg = sum(0), 
				Stp_Meas_Cubics = sum(0),
				Stp_Meas_CubicTons = sum(0), 
				Stp_Meas_CubicGrams = sum(0),

				Dev_Meas_AdvReef = sum(0),
				Dev_Meas_AdvWaste = sum(0),
				Dev_Meas_Primm = sum(0),
				Dev_Meas_Secm = sum(0),
				Dev_Meas_Adv = sum(0),
				Dev_Meas_TonsReef = sum(0),
				Dev_Meas_TonsWaste = sum(0), 
				Dev_Meas_Tons = sum(0),
				Dev_Meas_KG = sum(0),
				Dev_Meas_AdvEH = sum(0),
				Dev_Meas_AdvEW = sum(0),
				Dev_Meas_AdvCmgt = sum(0),
				Dev_Meas_AdvCmgtTotal = sum(0),
				Dev_Meas_Cubics = sum(0), 
				Dev_Meas_CubicTons = sum(0),
				Dev_Meas_CubicGrams = sum(0),
				
				BusFlag = max(''N''), 
				Stp_BPlan_FL = sum(0.000) ,
				Stp_BPlan_Adv = sum(0.000) ,
				Stp_BPlan_AdvReef = sum(0.000) ,
				Stp_BPlan_AdvWaste = sum(0.000) ,
				Stp_BPlan_Sqm = sum(0.0) ,
				Stp_BPlan_SqmReef = sum(0.0) ,
				Stp_BPlan_SqmWaste = sum(0.0) ,
				Stp_BPlan_SqmSW = sum(0.0) ,
				Stp_BPlan_SqmCW = sum(0.0) ,
				Stp_BPlan_SqmCmgt = sum(0.0) , 
				Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
				Stp_BPlan_Tons = sum(0) ,
				Stp_BPlan_TonsReef = sum(0.0) ,
				Stp_BPlan_TonsWaste = sum(0.0),
				Stp_BPlan_Kg = sum(0),

				Stp_BPlan_OSSqm = sum(0.0),
				Stp_BPlan_OSFSqm = sum(0.0),
				Stp_BPlan_REEFFL = sum(0.0),
				Stp_BPlan_OSFL = sum(0.0),     
				Stp_BPlan_CUBICS = sum(0),
				Stp_BPlan_SqmSWFAULT = sum(0.0),
				Stp_BPlan_SqmFault = sum(0.0),
				Stp_BPlan_FaultTons = sum(0),
				Stp_BPlan_OSTons = sum(0),
				Stp_BPlan_CUBICTons = sum(0),
					 
				Dev_BPlan_AdvReef = sum(0),
				Dev_BPlan_AdvWaste = sum(0),
				Dev_BPlan_Primm = sum(0),
				Dev_BPlan_Secm = sum(0), 
				Dev_BPlan_Adv = sum(0),
				Dev_BPlan_TonsReef = sum(0),
				Dev_BPlan_TonsWaste = sum(0),
				Dev_BPlan_Tons = sum(0),
				Dev_BPlan_KG = sum(0),
				Dev_BPlan_AdvEH = sum(0),
				Dev_BPlan_AdvEW =sum(0),
				Dev_BPlan_AdvCmgt = sum(0),
				Dev_BPlan_AdvCmgtTotal = sum(0),
				Dev_BPlan_Cubics = sum(0),
				Dev_BPlan_CubicTons = sum(0),
				Dev_BPlan_TonsCmgt = sum(0),
					
				ForeCast_SQM = sum(0) ,
				ForeCast_SQMDens = sum(0) ,
				ForeCast_Grams = sum(0) ,
				ForeCast_Tons = sum(0) ,
				ForeCast_KG = sum(0) '
		set @LPlanFrom = '
			from Planmonth pm 
			left outer join Planning p on 
				p.Prodmonth = pm.Prodmonth and 
				p.SectionID = pm.Sectionid and 
				p.WorkplaceID = pm.Workplaceid and 
				p.Activity = pm.Activity and
				p.PlanCode = pm.PlanCode and
				p.IsCubics = pm.IsCubics and
				p. CalendarDate <= '''+@Calendardate+'''
			inner join section_complete sc on  
					sc.prodmonth = pm.prodmonth and  
					sc.sectionid  = pm.sectionid  
			inner join  (select distinct(ProdMonth) ProdMonth , SectionID,TotalShifts from seccal ) s on
				s.ProdMonth = sc.prodmonth and
				s.SectionID = sc.SectionID_1  
			inner join Workplace w on  
					w.WorkplaceID = pm.WorkplaceID 
			inner join CommonAreaSubSections CS on
					cs.Id = w.SubSection 
			inner join CommonAreas c on
					c.Id = cs.CommonArea '
		SET @LPlanWhere = 
			' where pm.PlanCode = ''LP'' and pm.IsCubics = ''N'' and 
					p.CalendarDate >= '''+@FromDate+''' and 
					p.CalendarDate <= '''+@ToDate+''' and 
				'+@ReadSection+' = '''+@SectionID+''' '
		set @LPlanWhere = @LPlanWhere + '
			group by pm.Prodmonth, sc.SectionID_5, sc.Name_5, sc.SectionID_4, sc.Name_4, sc.SectionID_3, sc.Name_3,
						sc.SectionID_2, sc.Name_2, sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name,
						pm.WorkplaceiD, w.Description, pm.Activity '
	END
END  --if StpDaytoDay
set @GroupBy = '
		) a group by a.Prodmonth '
IF @NAME_5 = 'Y' 
  set @GroupBy = @GroupBy+ ' , a.NAME_5 '  
--else
--  set @GroupBy = @GroupBy+ ' Null NAME_5, ' 
  
IF @NAME_4 = 'Y' 
  set @GroupBy = @GroupBy+ ' , a.NAME_4 '  
--else
--  set @GroupBy = @GroupBy+ ' Null NAME_4, ' 
  
IF @NAME_3 = 'Y' 
  set @GroupBy = @GroupBy+ ' , a.NAME_3 '  
--else
--  set @GroupBy = @GroupBy+ ' Null NAME_3, ' 
  
IF @NAME_2 = 'Y' 
  set @GroupBy = @GroupBy+ ' , a.NAME_2 '  
--else
--  set @GroupBy = @GroupBy+ ' Null NAME_2, ' 
  
IF @NAME_1 = 'Y' 
  set @GroupBy = @GroupBy+ ' , a.NAME_1 '  
--else
--  set @GroupBy = @GroupBy+ ' Null NAME_1, ' 
  
IF @NAME = 'Y' 
  set @GroupBy = @GroupBy+ ' , NAME '  
--else
--  set @GroupBy = @GroupBy+ ' a.Null NAME, '   

IF @Workplace = 'Y' 
  set @GroupBy = @GroupBy+ ' , a.Workplace '  
--else
--  set @GroupBy = @GroupBy+ ' Null workplace, ' 

set @GroupBy = @GroupBy +' with rollup '


IF (@ReportType = 'P') or (@ReportType = 'M')
BEGIN


--print(@SelectPart)
--print(@SelectPart1)
--print(@SelectPart2)
--print(@SelectPart3)
--print(@PlanSelect)
--print(@PlanSelect1)
--print(@PlanSelect2)
--print(@PlanFrom)
--print(@PlanWhere)
--print(@SurveyUnion)
--print(@SurveySelect)
--print(@SurveySelect1)
--print(@SurveyFrom)
--print(@SurveyWhere)
--print(@LPlanUnion)
--print(@LPlanSelect)
--print(@LPlanSelect1)
--print(@LPlanSelect2)
--print(@LPlanFrom)
--print(@LPlanWhere)
--print(@BusUnion)
--print(@BusSelect)
--print(@BusSelect1)
--print(@BusFrom)
--print(@BusWhere)
--print(@BusDevUnion)
--print(@BusDevSelect)
--print(@BusDevSelect1)
--print(@BusDevFrom)
--print(@BusDevWhere)

--print(@GroupBy)


	exec (@SelectPart+@SelectPart1+@SelectPart2+@SelectPart3+
			@PlanSelect+@PlanSelect1+@PlanSelect2+@PlanFrom+@PlanWhere+
			@SurveyUnion+@SurveySelect+@SurveySelect1+@SurveyFrom+@SurveyWhere+
			@LPlanUnion+@LPlanSelect+@LPlanSelect1+@LPlanSelect2+@LPlanFrom+@LPlanWhere+
			@BusUnion+@BusSelect+@BusSelect1+@BusFrom+@BusWhere+
			@BusDevUnion+@BusDevSelect+@BusDevSelect1+@BusDevFrom+@BusDevWhere+
			@GroupBy)
END
ELSE
BEGIN
--	print(@SelectPart)
--	print(@SelectPart1)
--	print(@SelectPart2)
--	print(@SelectPart3)
--	print(@PlanSelect)
--	print(@PlanSelect1)
--	print(@PlanSelect2)
--	print(@PlanFrom)
--	print(@PlanWhere)
--	print(@LPlanUnion)
--	print(@LPlanSelect)
--	print(@LPlanSelect1)
--	print(@LPlanSelect2)
--	print(@LPlanFrom)
--	print(@LPlanWhere)
--	print(@GroupBy)
	exec (@SelectPart+@SelectPart1+@SelectPart2+@SelectPart3+@PlanSelect+@PlanSelect1+@PlanSelect2+@PlanFrom+@PlanWhere+
		@LPlanUnion+ @LPlanSelect+@LPlanSelect1+@LPlanSelect2+@LPlanFrom+@LPlanWhere+@GroupBy)
END

--exec sp_GenericReport 'Y','Y','Y','Y','Y','Y','Y',   1, 'GM',  '201707','201707','201707',   '2017-08-07','2017-08-07','2017-08-07',   'P', 'Y',   'Y','Y','N','N','Y','N','Y','N' ,'N', 0 

go



ALTER Procedure [dbo].[sp_GenericReport_CheckBoxes] 
--Input Parametres
--declare
  @Section varchar(100),
  @Banner varchar(30),
  @theReportHeadings  VARCHAR(100), 
  @theTypeOfReport  VARCHAR(100), 
  @RunLevel  int, 

  @PlanDyn  VARCHAR(2), 
  @PlanLock  VARCHAR(2), 
  @PlanDynProg  VARCHAR(2), 
  @PlanLockProg  VARCHAR(2), 
  @Book  VARCHAR(2), 
  @Meas  VARCHAR(2), 
  @PlanBuss  VARCHAR(2), 
  @FC  VARCHAR(2), 
  @AbsVar  VARCHAR(2),

  @AuthPlanOnly varchar(1),
  @StopeLedge varchar(1),

  @StpSqm  VARCHAR(2),
  @StpSqmOn  VARCHAR(2),
  @StpSqmOff  VARCHAR(2),
  @StpSqmOS  VARCHAR(2),
  @StpSqmOSF  VARCHAR(2),
  @StpCmgt  VARCHAR(2),
  @StpCmgtTotal  VARCHAR(2),
  @StpGT  VARCHAR(2),
  @StpGTTotal  VARCHAR(2),
  @StpSW  VARCHAR(2),
  @StpSWIdeal  VARCHAR(2),
  @StpSWFault  VARCHAR(2),
  @StpCW  VARCHAR(2),
  @StpKG  VARCHAR(2),
  @StpFL  VARCHAR(2),
  @StpFLOn  VARCHAR(2),
  @StpFLOff  VARCHAR(2),
  @StpFLOS  VARCHAR(2),
  @StpAdv  VARCHAR(2),
  @StpAdvOn  VARCHAR(2),
  @StpAdvOff  VARCHAR(2),
  @StpTons  VARCHAR(2),
  @StpTonsOn  VARCHAR(2),
  @StpTonsOff  VARCHAR(2),
  @StpTonsOS  VARCHAR(2),
  @StpTonsFault  VARCHAR(2),
  @StpCubics  VARCHAR(2),  
  @StpCubTons  VARCHAR(2), 
  @StpCubGT  VARCHAR(2),
  @StpCubKG  VARCHAR(2),
  @StpMeasSweeps  VARCHAR(2),
  @StpLabour  VARCHAR(2),
  @StpShftInfo  VARCHAR(2),

  @DevAdv  VARCHAR(2),
  @DevAdvOn  VARCHAR(2),
  @DevAdvOff  VARCHAR(2),
  @DevEH  VARCHAR(2),
  @DevEW  VARCHAR(2),
  @DevTons  VARCHAR(2),
  @DevTonsOn  VARCHAR(2),
  @DevTonsOff  VARCHAR(2),
  @DevCmgt  VARCHAR(2),
  @DevCmgtTotal  VARCHAR(2),
  @DevGT  VARCHAR(2),
  @DevGTTotal  VARCHAR(2),
  @DevKG  VARCHAR(2),
  @DevCubics  VARCHAR(2),
  @DevCubTons  VARCHAR(2),
  @DevCubGT  VARCHAR(2),
  @DevCubKG  VARCHAR(2),
  @DevLabour  VARCHAR(2),
  @DevShftInfo  VARCHAR(2),
  @DevDrillRig  VARCHAR(2)
 
as


SELECT
  @Section Section,
  @Banner Banner,
  @theReportHeadings theReportHeadings, 
  @theTypeOfReport  theTypeOfReport, 
  @RunLevel  RunLevel, 

  @PlanDyn  PlanDyn, 
  @PlanLock  PlanLock, 
  @PlanDynProg  PlanDynProg, 
  @PlanLockProg  PlanLockProg, 
  @Book  Book, 
  @Meas  Meas, 
  @PlanBuss  PlanBuss, 
  @FC  FC, 
  @AbsVar  AbsVar,

  @AuthPlanOnly AuthPlanOnly,
  @StopeLedge StopeLedge,

  @StpSqm  StpSqm,
  @StpSqmOn  StpSqmOn,
  @StpSqmOff  StpSqmOff,
  @StpSqmOS  StpSqmOS,
  @StpSqmOSF  StpSqmOSF,
  @StpCmgt  StpCmgt,
  @StpCmgtTotal  StpCmgtTotal,
  @StpGT  StpGT,
  @StpGTTotal  StpGTTotal,
  @StpSW  StpSW,
  @StpSWIdeal  StpSWIdeal,
  @StpSWFault  StpSWFault,
  @StpCW  StpCW,
  @StpKG  StpKG,
  @StpFL  StpFL,
  @StpFLOn  StpFLOn,
  @StpFLOff  StpFLOff,
  @StpFLOS  StpFLOS,
  @StpAdv  StpAdv,
  @StpAdvOn  StpAdvOn,
  @StpAdvOff  StpAdvOff,
  @StpTons  StpTons,
  @StpTonsOn  StpTonsOn,
  @StpTonsOff  StpTonsOff,
  @StpTonsOS  StpTonsOS,
  @StpTonsFault  StpTonsFault,
  @StpCubics  StpCubics, 
  @StpCubTons  StpCubTons,
  @StpCubGT  StpCubGT,
  @StpCubKG  StpCubKG,
  @StpMeasSweeps  StpMeasSweeps,
  @StpLabour  StpLabour,
  @StpShftInfo  StpShftInfo,

  @DevAdv  DevAdv,
  @DevAdvOn  DevAdvOn,
  @DevAdvOff  DevAdvOff,
  @DevEH  DevEH,
  @DevEW  DevEW,
  @DevTons  DevTons,
  @DevTonsOn  DevTonsOn,
  @DevTonsOff  DevTonsOff,
  @DevCmgt  DevCmgt,
  @DevCmgtTotal  DevCmgtTotal,
  @DevGT  DevGT,
  @DevGTTotal  DevGTTotal,
  @DevKg  DevKg,
  @DevCubics  DevCubics,
  @DevCubTons  DevCubTons,
  @DevCubGT  DevCubGT,
  @DevCubKg  DevCubKg,
  @DevLabour  DevLabour,
  @DevShftInfo  DevShftInfo,
  @DevDrillRig  DevDrillRig


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
         pm.Activity, 
		 ActDesc = case when isnull(pm.Sqm,0) > 0 and isnull(pm.CubicMetres,0) > 0 and pm.TargetID = 9 then ''Ledg / Cubics''
						when isnull(pm.Sqm,0) > 0 and isnull(pm.CubicMetres,0) = 0 and pm.TargetID = 9  then ''Ledging''
						when isnull(pm.Sqm,0) > 0 and isnull(pm.CubicMetres,0) > 0 and pm.TargetID <> 9 then ''Stp / Cubics''
						when isnull(pm.Sqm,0) > 0 and isnull(pm.CubicMetres,0) = 0 and pm.TargetID <> 9 then ''Stoping''
						when isnull(pm.Sqm,0) = 0 and isnull(pm.CubicMetres,0) > 0 then ''Cubics'' 
						else
						''Stoping'' end, 
		 pd.ShiftDay, 
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
		 BookAdvReef = case when isnull(pd.BookMetresAdvance,0) = 0 then cast(1.2 as numeric(7,2))
		    else cast(isnull(pd.BookReefAdv, 0) as numeric(7,2)) end, 
		 BookAdvWaste = cast(isnull(pd.BookWasteAdv, 0) as numeric(7,2)), 
		 DefaultAdv = cast(1.2 as numeric(7,2)), 
         BookCmgt = case when isnull(pd.BookCMGT, 0) = 0 then isnull(pd.CMGT, 0) else isnull(pd.BookCMGT, 0) end, 
		 BookGT = case when isnull(pd.BookGT, 0) = 0 then isnull(pd.GT, 0) else isnull(pd.BookGT, 0) end,
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
		 isnull(pd.CausedLostBlast,'''') CausedLostBlast,
		 isnull(pd.BookCubicMetres,0) BookCubicMetres,
		 isnull(pd.BookCubicTons,0) BookCubicTons,
		 isnull(pd.BookCubicGrams,0) BookCubicGrams,
		 BookCubicKG = isnull(pd.BookCubicGrams,0) / 1000,
		 BookCubicGT = case when isnull(pd.BookCubicGT,0) = 0 then 
				isnull(pd.CubicGT,0) else isnull(pd.BookCubicGT,0) end,
		 RecCubics = case when isnull(pm.CubicMetres,0) > 0 then ''Y'' else ''N'' end, 
		 RecSqm = case when isnull(pm.SQM,0) > 0 then ''Y'' else ''N'' end
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
drop table [BusinessPlan_Stoping]
go
CREATE TABLE [dbo].[BusinessPlan_Stoping](
	[WorkplaceID] [varchar](12) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[Prodmonth] [int] NOT NULL,
	[Density] [decimal](18, 2) NOT NULL,
	[SQMFaultLedge] [int] NOT NULL,
	[SQMOSLedge] [int] NOT NULL,
	[SQMReeflLedge] [int] NOT NULL,
	[SQMFaultStope] [int] NOT NULL,
	[SQMOSStope] [int] NOT NULL,
	[SQMReefStope] [int] NOT NULL,
	[SW] [int] NOT NULL,
	[SWFault] [int] NOT NULL,
	[FLReef] [int] NOT NULL,
	[FLOS] [int] NOT NULL,
	[FL] [int] NOT NULL,
	[CMGT] [int] NOT NULL,
	[CW] [int] NOT NULL,
	[Cubics] [int] NOT NULL,
	[TonsFaultLedge] [decimal](18, 2) NULL,
	[TonsOSLedge] [decimal](18, 2) NULL,
	[TonsCubic] [decimal](18, 2) NULL,
	[TonsReefLedge] [decimal](18, 2) NULL,
	[TonsWasteLedge] [decimal](18, 2) NULL,
	[ContentLedge] [decimal](18, 2) NULL,
	[TonsLedge] [decimal](18, 2) NULL,
	[SQMWasteLedge] [int] NOT NULL,
	[SQMLedge] [int] NOT NULL,
	[TonsFaultStope] [decimal](18, 2) NULL,
	[TonsOSStope] [decimal](18, 2) NULL,
	[TonsReefStope] [decimal](18, 2) NULL,
	[TonsWasteStope] [decimal](18, 2) NULL,
	[ContentStope] [decimal](18, 2) NULL,
	[TonsStope] [decimal](18, 2) NULL,
	[SQMWasteStope] [int] NOT NULL,
	[SQMStope] [int] NOT NULL,
	[TonsFault] [decimal](18, 2) NULL,
	[TonsOS] [decimal](18, 2) NULL,
	[TonsReef] [decimal](18, 2) NULL,
	[TonsWaste] [decimal](18, 2) NULL,
	[Content] [decimal](18, 2) NULL,
	[Tons] [decimal](18, 2) NOT NULL,
	[SQMWaste] [int] NOT NULL,
	[SQM] [int] NULL,
	[ImportSourceID] [int] NOT NULL,
	[SQMOSFLedge] [int] NULL,
	[SQMOSFStope] [int] NULL,
	[TonsOSFStope] [decimal](18, 2) NULL,
	[TonsOSFLedge] [decimal](18, 2) NULL,
	[TonsOSF] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
drop table [BusinessPlan_Development]
go
CREATE TABLE [dbo].[BusinessPlan_Development](
	[WorkplaceID] [varchar](20) NOT NULL,
	[SectionID] [varchar](20) NOT NULL,
	[ProdMonth] [int] NOT NULL,
	[MAdvReef] [decimal](18, 1) NOT NULL,
	[MAdvWaste] [decimal](18, 1) NOT NULL,
	[Height] [decimal](18, 1) NOT NULL,
	[Width] [decimal](18, 1) NOT NULL,
	[Density] [decimal](18, 2) NOT NULL,
	[CMGT] [int] NOT NULL,
	[Cubics] [decimal](18, 2) NULL,
	[TonsReef] [decimal](18, 2) NULL,
	[TonsWaste] [decimal](18, 2) NULL,
	[Content] [decimal](18, 2) NULL,
	[IndicatorID] [int] NOT NULL,
	[MiningMethodID] [int] NOT NULL,
	[EquivalentMetres] [decimal](18, 1) NULL,
	[TonsCubic] [decimal](18, 2) NULL,
	[Tons] [decimal](18, 2) NULL,
	[MAdv] [decimal](18, 1) NULL,
	[ImportSourceID] [int] NOT NULL
) ON [PRIMARY]

GO

