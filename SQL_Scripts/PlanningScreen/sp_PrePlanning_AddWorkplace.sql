USE [PAS_DNK_Syncromine]
GO
/****** Object:  StoredProcedure [dbo].[sp_PrePlanning_AddWorkplace]    Script Date: 2019/02/27 12:34:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter procedure [dbo].[sp_PrePlanning_AddWorkplace]
@Prodmonth NUMERIC(7,0),@SectionID_2 VARCHAR(20),@WPDescription VARCHAR(100), @ActivityCode int

AS

		--DECLARE @Prodmonth NUMERIC(7,0),@SectionID_2 VARCHAR(20),@WPDescription VARCHAR(100), @ActivityCode int
		--SET @Prodmonth = 201903
		--SET @SectionID_2 = 'REA'
		--SET @WPDescription = '197 S4 N 12'
		--SET @ActivityCode = 0

DECLARE @WPType varchar(2)

if @ActivityCode = 2
begin
select DISTINCT TOP 1 @Prodmonth Prodmonth,-1 Sectionid, @SectionID_2 Sectionid_2,WP.WORKPLACEID,WP.DESCRIPTION WorkplaceDesc, wp.activity Activity,wp.REEFWASTE,'' OrgUnitDay,
'' OrgUnitNight ,'' SMDescription,'' UnitType,0.00 Call,0.00 Meters,Min(SECCAL.BeginDate) StartDate,Max(SECCAL.EndDate) EndDate,0 Locked from WORKPLACE WP 
INNER JOIN SECTION_COMPLETE SC on
        SC.SECTIONID_2 = @SectionID_2 and 
        SC.PRODMONTH = @Prodmonth
        inner join SECCAL on
	    SC.PRODMONTH = SECCAL.PRODMONTH and
	    SC.SECTIONID_1 = SECCAL.SECTIONID WHERE WP.Description =@WPDescription 
		group by SC.PRODMONTH, SC.Sectionid, Sectionid_2,WP.WORKPLACEID,WP.DESCRIPTION, wp.activity,wp.REEFWASTE
end

IF @ActivityCode = 8
BEGIN
select distinct TOP 1 @Prodmonth Prodmonth,-1 Sectionid, @SectionID_2 Sectionid_2,WP.WORKPLACEID,WP.DESCRIPTION WorkplaceDesc,wp.REEFWASTE,'' OrgunitDay,
'' OrgunitNight ,'' OrgunitAfterNoon ,'' Activity,'' UnitType,0.00 Call,0.00 ccall,0 content,0 Depth, 0 ActualDepth,CASE WHEN sam.GT is null THEN 0 ELSE sam.GT  END  gt,SECCAL.BeginDate StartDate,SECCAL.EndDate,0 Locked from WORKPLACE WP 
INNER JOIN SECTION_COMPLETE SC on
        SC.SECTIONID_2 = @SectionID_2 and 
        SC.PRODMONTH = @Prodmonth
		LEFT JOIN   (SELECT MAX(WeekNo) WeekNo,WORKPLACEID FROM dbo.vw_Kriging_Latest
WHERE Prodmonth = dbo.GetPrevProdMonth(@Prodmonth) or Prodmonth = @Prodmonth
GROUP BY WORKPLACEID) SAMTemp ON
(SELECT [WorkplaceID] FROM [dbo].[WORKPLACE] WHERE Description =@WPDescription) = SAMTemp.WORKPLACEID
LEFT JOIN dbo.vw_Kriging_Latest SAM ON
SAMTemp.WORKPLACEID = SAM.WORKPLACEID AND
SAMTemp.WeekNo = SAM.WeekNo
        inner join SECCAL on
	    SC.PRODMONTH = SECCAL.PRODMONTH and
	    SC.SECTIONID_1 = SECCAL.SECTIONID WHERE WP.Description =@WPDescription
--END
END

if  @ActivityCode in (0,1,3)
begin
SET @WPType = (SELECT [Activity] FROM dbo.WORKPLACE WHERE [DESCRIPTION] = @WPDescription) 

declare @totalrows int
--IF @WPType = '0'
--Begin
set @totalrows=(select count(WorkplaceDesc) from( SELECT  
DISTINCT 
		0 MonthlyReefSQM,
		0 MonthlyWatseSQM,
		0 MonthlyTotalSQM,
	   @Prodmonth [Prodmonth]
      ,-1 [Sectionid]
      ,@SectionID_2 [Sectionid_2]
      ,WP.DESCRIPTION [WorkplaceDesc]
      ,wp.activity [Activity]
      ,'N' [IsCubics]
      ,'MP' [PlanCode]
      ,SECCAL.BeginDate  [StartDate]
      ,WP.Workplaceid [Workplaceid]
      ,-1 [TargetID]
      ,'' [OrgUnitDay]
      ,'' [OrgUnitAfternoon]
      ,'' [OrgUnitNight]
      ,'' [RomingCrew]
      ,cast(0 as bit) Locked
      ,cast(0 as bit) [Auth]
      ,0 [SQM]
      ,0 [ReefSQM]
      ,0 [WasteSQM]
      ,20 [FL]
      ,0 [ReefFL]
      ,0 [WasteFL]
      ,0 [FaceAdvance]
      ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0  
            WHEN SAM.ChannelWidth < 80 THEN 100 ELSE SAM.ChannelWidth + 20 END [IdealSW]
      ,CASE WHEN SAM.StopeWidth IS NULL THEN 0 ELSE SAM.StopeWidth END [SW]
      ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0 ELSE SAM.ChannelWidth END [CW]
      ,CASE WHEN SAM.CMGT IS NULL THEN 0 ELSE SAM.CMGT END [CMGT]
	  ,0 [CMKGT]
	  ,CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType
      ,0 [GT]
      ,0 [Kg]
	  ,0 [UraniumBroken]
      ,0 [FaceCMGT]
      ,0 [FaceKG]
      ,0 [Tons]
      ,0 [ReefTons]
      ,0 [WasteTons]
      ,0 [FaceValue]
      ,0 [CubicMetres]
      ,0 [Cubics]
      ,0 [CubicsReef]
      ,0 [CubicsWaste]
      ,0 [CubicsTons]
      ,0 [CubicsReefTons]
      ,0 [CubicsWasteTons]
      ,0 [CubicGrams]
      ,0 [CubicDepth]
      ,0 [ActualDepth]
      ,0 [CubicGT]
      ,0 [TrammedTons]
      ,0 [TrammedValue]
      ,'' [TrammedLevel]
      ,0 [Metresadvance]
      ,0 [ReefAdv]
      ,0 [WasteAdv]
      ,0 [DevMain]
      ,0 [DevSec]
      ,0 [DevSecReef]
      ,0 [DevCap]
	  ,0 DEVTONS
      ,null [LockedDate]
      ,'' [LockedBY]
      ,'' [DrillRig]
      ,null [StoppedDate]
      ,SECCAL.EndDate [EndDate]
      ,cast(0 as bit) [IsStopped]
      ,null [TopEnd]
      ,null [AutoUnPlan]
      ,0 [LabourStrength]
      ,'' [MOCycle]
      ,'' [Vac]
      ,'' [DC]
      ,'' [MOCycleNum]
      ,'' [DevFlag]
      ,'' [BoxHoleID]
	  ,cast(1 as bit) hasChanged
	  ,cast(0 as bit) hasRevised
	  ,ENDHEIGHT = isnull(wp.ENDHEIGHT,0)
	  ,ENDWIDTH = isnull(Wp.ENDWIDTH,0)
		
		FROM dbo.WORKPLACE WP
		INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
        LEFT JOIN dbo.vw_Kriging_Latest SAM ON 
		WP.WORKPLACEID = SAM.WORKPLACEID and
		SAM.ProdMonth = @Prodmonth  
         inner JOIN SECTION_COMPLETE SC on
        SC.SECTIONID_2 = @SectionID_2 and 
        SC.PRODMONTH = @Prodmonth
        left join SECCAL on
	    SC.PRODMONTH = SECCAL.PRODMONTH and
	    SC.SECTIONID_1 = SECCAL.SECTIONID  
        WHERE [DESCRIPTION] = @WPDescription

)data)

if @totalrows > 1
begin

 insert into dbo.PLANMONTH
	(
	      MonthlyReefSQM
		  ,MonthlyWatseSQM
		   ,MonthlyTotalSQM
		    ,[Prodmonth]
		  ,[Sectionid]
		  , [Activity]
		  , [IsCubics]
		  , [PlanCode]
		  ,  [StartDate]
		  , [Workplaceid]
		  , [TargetID]
		  , [OrgUnitDay]
		  , [OrgUnitAfternoon]
		  , [OrgUnitNight]
		  , [RomingCrew]
		  , Locked
		  , [Auth]
		  , [SQM]
		  , [ReefSQM]
		  , [WasteSQM]
		  , [FL]
		  , [ReefFL]
		  , [WasteFL]
		  , [FaceAdvance]
		  ,[IdealSW]
		  , [SW]
		  , [CW]
		  , [CMGT]
		  , [CMKGT]
		  , [GT]
		  , [Kg]
		  , [FaceCMGT]
		  , [FaceKG]
		  , [Tons]
		  , [ReefTons]
		  , [WasteTons]
		  , [FaceValue]
		  , [CubicMetres]
		  , [Cubics]
		  , [CubicsReef]
		  , [CubicsWaste]
		  , [CubicsTons]
		  , [CubicsReefTons]
		  , [CubicsWasteTons]
		  , [CubicGrams]
		  , [CubicDepth]
		  , [ActualDepth]
		  , [CubicGT]
		  , [TrammedTons]
		  , [TrammedValue]
		  ,[TrammedLevel]
		  , [Metresadvance]
		  , [ReefAdv]
		  , [WasteAdv]
		  , [DevMain]
		  , [DevSec]
		  , [DevSecReef]
		  , [DevCap]
		 
		  , [LockedDate]
		  ,[LockedBY]
		  , [DrillRig]
		  , [StoppedDate]
		  ,[EndDate]
		  , [IsStopped]
		  , [TopEnd]
		  , [AutoUnPlan]
		  ,[LabourStrength]
		  , [MOCycle]
		  , [Vac]
		  , [DC]
		  , [MOCycleNum]
		  , [DevFlag]
		  , [BoxHoleID]

	      ,DHeight 
	      ,DWidth 

	)
	SELECT  
	DISTINCT 
		  0 MonthlyReefSQM,
		  0 MonthlyWatseSQM,
		  0 MonthlyTotalSQM,
		   @Prodmonth [Prodmonth]
		  ,-1 [Sectionid]
		  ,wp.activity [Activity]
		  ,'N' [IsCubics]
		  ,'MP' [PlanCode]
		  ,SECCAL.BeginDate [StartDate]
		  ,WP.Workplaceid [Workplaceid]
		  ,-1 [TargetID]
		  ,'' [OrgUnitDay]
		  ,'' [OrgUnitAfternoon]
		  ,'' [OrgUnitNight]
		  ,'' [RomingCrew]
		  ,cast(0 as bit) Locked
		  ,cast(0 as bit) [Auth]
		  ,0 [SQM]
		  ,0 [ReefSQM]
		  ,0 [WasteSQM]
		  ,20 [FL]
		  ,0 [ReefFL]
		  ,0 [WasteFL]
		  ,0 [FaceAdvance]
		  ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0  
				WHEN SAM.ChannelWidth < 80 THEN 100 ELSE SAM.ChannelWidth + 20 END [IdealSW]
		  ,CASE WHEN SAM.StopeWidth IS NULL THEN 0 ELSE SAM.StopeWidth END [SW]
		  ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0 ELSE SAM.ChannelWidth END [CW]
		  ,CASE WHEN SAM.CMGT IS NULL THEN 0 ELSE SAM.CMGT END [CMGT]
		  ,0 [CMKGT]
		  ,0 [GT]
		  ,0 [Kg]
		  ,0 [FaceCMGT]
		  ,0 [FaceKG]
		  ,0 [Tons]
		  ,0 [ReefTons]
		  ,0 [WasteTons]
		  ,0 [FaceValue]
		  ,0 [CubicMetres]
		  ,0 [Cubics]
		  ,0 [CubicsReef]
		  ,0 [CubicsWaste]
		  ,0 [CubicsTons]
		  ,0 [CubicsReefTons]
		  ,0 [CubicsWasteTons]
		  ,0 [CubicGrams]
		  ,0 [CubicDepth]
		  ,0 [ActualDepth]
		  ,0 [CubicGT]
		  ,0 [TrammedTons]
		  ,0 [TrammedValue]
		  ,o.LEVELNUMber [TrammedLevel]
		  ,0 [Metresadvance]
		  ,0 [ReefAdv]
		  ,0 [WasteAdv]
		  ,0 [DevMain]
		  ,0 [DevSec]
		  ,0 [DevSecReef]
		  ,0 [DevCap]
		  ,null [LockedDate]
		  ,'' [LockedBY]
		  ,'' [DrillRig]
		  ,null [StoppedDate]
		  ,SECCAL.EndDate [EndDate]
		  ,cast(0 as bit) [IsStopped]
		  ,null [TopEnd]
		  ,null [AutoUnPlan]
		  ,0 [LabourStrength]
		  ,'' [MOCycle]
		  ,'' [Vac]
		  ,'' [DC]
		  ,'' [MOCycleNum]
		  ,'' [DevFlag]
		  ,'' [BoxHoleID]
	      ,ENDHEIGHT = isnull(wp.ENDHEIGHT,0)
	      ,ENDWIDTH = isnull(Wp.ENDWIDTH,0)

		FROM dbo.WORKPLACE WP
			INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
			LEFT JOIN dbo.vw_Kriging_Latest SAM ON  
			 WP.WORKPLACEID = SAM.WORKPLACEID and
			 SAM.ProdMonth = @Prodmonth

			 inner JOIN SECTION_COMPLETE SC on
			SC.SECTIONID_2 = @SectionID_2 and 
			SC.PRODMONTH = @Prodmonth

			 left join SECCAL on
	    SC.PRODMONTH = SECCAL.PRODMONTH and
	    SC.SECTIONID_1 = SECCAL.SECTIONID  
  
			WHERE [DESCRIPTION] = @WPDescription

SELECT 
DISTINCT 
	   0 MonthlyReefSQM,
		0 MonthlyWatseSQM,
		0 MonthlyTotalSQM,
	   @Prodmonth [Prodmonth]
      ,-1 [Sectionid]
      ,@SectionID_2 [Sectionid_2]
      ,WP.DESCRIPTION [WorkplaceDesc]
      ,wp.activity [Activity]
      ,'N' [IsCubics]
      ,'MP' [PlanCode]
      ,SECCAL.BeginDate  [StartDate]
      ,WP.Workplaceid [Workplaceid]
      ,-1 [TargetID]
      ,'' [OrgUnitDay]
      ,'' [OrgUnitAfternoon]
      ,'' [OrgUnitNight]
      ,'' [RomingCrew]
      ,cast(0 as bit) Locked
      ,cast(0 as bit) [Auth]
      ,0 [SQM]
      ,0 [ReefSQM]
      ,0 [WasteSQM]
      ,20 [FL]
      ,0 [ReefFL]
      ,0 [WasteFL]
      ,0 [FaceAdvance]
      ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0  
            WHEN SAM.ChannelWidth < 80 THEN 100 ELSE SAM.ChannelWidth + 20 END [IdealSW]
      ,CASE WHEN SAM.StopeWidth IS NULL THEN 0 ELSE SAM.StopeWidth END [SW]
      ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0 ELSE SAM.ChannelWidth END [CW]
      ,CASE WHEN SAM.CMGT IS NULL THEN 0 ELSE SAM.CMGT END [CMGT]
	  ,0 [CMKGT]
	  ,CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType
      ,0 [GT]
      ,0 [Kg]
	  ,0 [UraniumBroken]
      ,0 [FaceCMGT]
      ,0 [FaceKG]
      ,0 [Tons]
      ,0 [ReefTons]
      ,0 [WasteTons]
      ,0 [FaceValue]
      ,0 [CubicMetres]
      ,0 [Cubics]
      ,0 [CubicsReef]
      ,0 [CubicsWaste]
      ,0 [CubicsTons]
      ,0 [CubicsReefTons]
      ,0 [CubicsWasteTons]
      ,0 [CubicGrams]
      ,0 [CubicDepth]
      ,0 [ActualDepth]
      ,0 [CubicGT]
      ,0 [TrammedTons]
      ,0 [TrammedValue]
      ,o.LEVELNUMber [TrammedLevel]
      ,0 [Metresadvance]
      ,0 [ReefAdv]
      ,0 [WasteAdv]
      ,0 [DevMain]
      ,0 [DevSec]
      ,0 [DevSecReef]
      ,0 [DevCap]
	  ,0 DEVTONS
      ,null [LockedDate]
      ,'' [LockedBY]
      ,'' [DrillRig]
      ,null [StoppedDate]
      ,SECCAL.EndDate [EndDate]
      ,cast(0 as bit) [IsStopped]
      ,null [TopEnd]
      ,null [AutoUnPlan]
      ,0 [LabourStrength]
      ,'' [MOCycle]
      ,'' [Vac]
      ,'' [DC]
      ,'' [MOCycleNum]
      ,'' [DevFlag]
      ,'' [BoxHoleID]
	  ,cast(1 as bit) hasChanged
	   ,cast(0 as bit) hasRevised
	   ,ENDHEIGHT = isnull(wp.ENDHEIGHT,0)
	   ,ENDWIDTH = isnull(Wp.ENDWIDTH,0)


		FROM dbo.WORKPLACE WP
		INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
        LEFT JOIN dbo.vw_Kriging_Latest SAM ON
		WP.WORKPLACEID = SAM.WORKPLACEID and
		SAM.ProdMonth = @Prodmonth 
          
         inner JOIN SECTION_COMPLETE SC on
        SC.SECTIONID_2 = @SectionID_2 and 
        SC.PRODMONTH = @Prodmonth
        left join SECCAL on
	    SC.PRODMONTH = SECCAL.PRODMONTH and
	    SC.SECTIONID_1 = SECCAL.SECTIONID  
        WHERE [DESCRIPTION] = @WPDescription

end
else 
if @totalrows=1
begin
    insert into dbo.PLANMONTH
	(
	      MonthlyReefSQM
		  ,MonthlyWatseSQM
		   ,MonthlyTotalSQM
		    ,[Prodmonth]
		  ,[Sectionid]
		  , [Activity]
		  , [IsCubics]
		  , [PlanCode]
		  ,  [StartDate]
		  , [Workplaceid]
		  , [TargetID]
		  , [OrgUnitDay]
		  , [OrgUnitAfternoon]
		  , [OrgUnitNight]
		  , [RomingCrew]
		  , Locked
		  , [Auth]
		  , [SQM]
		  , [ReefSQM]
		  , [WasteSQM]
		  , [FL]
		  , [ReefFL]
		  , [WasteFL]
		  , [FaceAdvance]
		  ,[IdealSW]
		  , [SW]
		  , [CW]
		  , [CMGT]
		  , [CMKGT]
		  , [GT]
		  , [Kg]
		  , [FaceCMGT]
		  , [FaceKG]
		  , [Tons]
		  , [ReefTons]
		  , [WasteTons]
		  , [FaceValue]
		  , [CubicMetres]
		  , [Cubics]
		  , [CubicsReef]
		  , [CubicsWaste]
		  , [CubicsTons]
		  , [CubicsReefTons]
		  , [CubicsWasteTons]
		  , [CubicGrams]
		  , [CubicDepth]
		  , [ActualDepth]
		  , [CubicGT]
		  , [TrammedTons]
		  , [TrammedValue]
		  ,[TrammedLevel]
		  , [Metresadvance]
		  , [ReefAdv]
		  , [WasteAdv]
		  , [DevMain]
		  , [DevSec]
		  , [DevSecReef]
		  , [DevCap]
		 
		  , [LockedDate]
		  ,[LockedBY]
		  , [DrillRig]
		  , [StoppedDate]
		  ,[EndDate]
		  , [IsStopped]
		  , [TopEnd]
		  , [AutoUnPlan]
		  ,[LabourStrength]
		  , [MOCycle]
		  , [Vac]
		  , [DC]
		  , [MOCycleNum]
		  , [DevFlag]
		  , [BoxHoleID]

	      ,DHeight 
	      ,DWidth 

	)
	SELECT  
	DISTINCT 
		  0 MonthlyReefSQM,
		  0 MonthlyWatseSQM,
		  0 MonthlyTotalSQM,
		   @Prodmonth [Prodmonth]
		  ,-1 [Sectionid]
		  ,wp.activity [Activity]
		  ,'N' [IsCubics]
		  ,'MP' [PlanCode]
		  ,SECCAL.BeginDate  [StartDate]
		  ,WP.Workplaceid [Workplaceid]
		  ,-1 [TargetID]
		  ,'' [OrgUnitDay]
		  ,'' [OrgUnitAfternoon]
		  ,'' [OrgUnitNight]
		  ,'' [RomingCrew]
		  ,cast(0 as bit) Locked
		  ,cast(0 as bit) [Auth]
		  ,0 [SQM]
		  ,0 [ReefSQM]
		  ,0 [WasteSQM]
		  ,20 [FL]
		  ,0 [ReefFL]
		  ,0 [WasteFL]
		  ,0 [FaceAdvance]
		  ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0  
				WHEN SAM.ChannelWidth < 80 THEN 100 ELSE SAM.ChannelWidth + 20 END [IdealSW]
		  ,CASE WHEN SAM.StopeWidth IS NULL THEN 0 ELSE SAM.StopeWidth END [SW]
		  ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0 ELSE SAM.ChannelWidth END [CW]
		  ,CASE WHEN SAM.CMGT IS NULL THEN 0 ELSE SAM.CMGT END [CMGT]
		  ,0 [CMKGT]
		  ,0 [GT]
		  ,0 [Kg]
		  ,0 [FaceCMGT]
		  ,0 [FaceKG]
		  ,0 [Tons]
		  ,0 [ReefTons]
		  ,0 [WasteTons]
		  ,0 [FaceValue]
		  ,0 [CubicMetres]
		  ,0 [Cubics]
		  ,0 [CubicsReef]
		  ,0 [CubicsWaste]
		  ,0 [CubicsTons]
		  ,0 [CubicsReefTons]
		  ,0 [CubicsWasteTons]
		  ,0 [CubicGrams]
		  ,0 [CubicDepth]
		  ,0 [ActualDepth]
		  ,0 [CubicGT]
		  ,0 [TrammedTons]
		  ,0 [TrammedValue]
		  ,o.LEVELNUMber [TrammedLevel]
		  ,0 [Metresadvance]
		  ,0 [ReefAdv]
		  ,0 [WasteAdv]
		  ,0 [DevMain]
		  ,0 [DevSec]
		  ,0 [DevSecReef]
		  ,0 [DevCap]
		  ,null [LockedDate]
		  ,'' [LockedBY]
		  ,'' [DrillRig]
		  ,null [StoppedDate]
		  ,SECCAL.EndDate [EndDate]
		  ,cast(0 as bit) [IsStopped]
		  ,null [TopEnd]
		  ,null [AutoUnPlan]
		  ,0 [LabourStrength]
		  ,'' [MOCycle]
		  ,'' [Vac]
		  ,'' [DC]
		  ,'' [MOCycleNum]
		  ,'' [DevFlag]
		  ,'' [BoxHoleID]
	      ,ENDHEIGHT = isnull(wp.ENDHEIGHT,0)
	      ,ENDWIDTH = isnull(Wp.ENDWIDTH,0)

		FROM dbo.WORKPLACE WP
			INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
			LEFT JOIN dbo.vw_Kriging_Latest SAM ON  
			 WP.WORKPLACEID = SAM.WORKPLACEID and
			 SAM.ProdMonth = @Prodmonth

			 inner JOIN SECTION_COMPLETE SC on
			SC.SECTIONID_2 = @SectionID_2 and 
			SC.PRODMONTH = @Prodmonth

			 left join SECCAL on
	    SC.PRODMONTH = SECCAL.PRODMONTH and
	    SC.SECTIONID_1 = SECCAL.SECTIONID  
  
			WHERE [DESCRIPTION] = @WPDescription



			SELECT  
	DISTINCT 
		  0 MonthlyReefSQM,
		  0 MonthlyWatseSQM,
		  0 MonthlyTotalSQM,
		   @Prodmonth [Prodmonth]
		  ,-1 [Sectionid]
		  ,@SectionID_2 [Sectionid_2]
		  ,WP.DESCRIPTION [WorkplaceDesc]
		  ,wp.activity [Activity]
		  ,'N' [IsCubics]
		  ,'MP' [PlanCode]
		  ,SECCAL.BeginDate [StartDate]
		  ,WP.Workplaceid [Workplaceid]
		  ,-1 [TargetID]
		  ,'' [OrgUnitDay]
		  ,'' [OrgUnitAfternoon]
		  ,'' [OrgUnitNight]
		  ,'' [RomingCrew]
		  ,cast(0 as bit) Locked
		  ,cast(0 as bit) [Auth]
		  ,0 [SQM]
		  ,0 [ReefSQM]
		  ,0 [WasteSQM]
		  ,20 [FL]
		  ,0 [ReefFL]
		  ,0 [WasteFL]
		  ,0 [FaceAdvance]
		  ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0  
				WHEN SAM.ChannelWidth < 80 THEN 100 ELSE SAM.ChannelWidth + 20 END [IdealSW]
		  ,CASE WHEN SAM.StopeWidth IS NULL THEN 0 ELSE SAM.StopeWidth END [SW]
		  ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0 ELSE SAM.ChannelWidth END [CW]
		  ,CASE WHEN SAM.CMGT IS NULL THEN 0 ELSE SAM.CMGT END [CMGT]
		  ,0 [CMKGT]
		  ,CASE WHEN wp.ReefWaste = 'R' THEN 'ON' ELSE 'OFF' END RockType
		  ,0 [GT]
		  ,0 [Kg]
		  ,0 [UraniumBroken]
		  ,0 [FaceCMGT]
		  ,0 [FaceKG]
		  ,0 [Tons]
		  ,0 [ReefTons]
		  ,0 [WasteTons]
		  ,0 [FaceValue]
		  ,0 [CubicMetres]
		  ,0 [Cubics]
		  ,0 [CubicsReef]
		  ,0 [CubicsWaste]
		  ,0 [CubicsTons]
		  ,0 [CubicsReefTons]
		  ,0 [CubicsWasteTons]
		  ,0 [CubicGrams]
		  ,0 [CubicDepth]
		  ,0 [ActualDepth]
		  ,0 [CubicGT]
		  ,0 [TrammedTons]
		  ,0 [TrammedValue]
		  ,o.LEVELNUMber [TrammedLevel]
		  ,0 [Metresadvance]
		  ,0 [ReefAdv]
		  ,0 [WasteAdv]
		  ,0 [DevMain]
		  ,0 [DevSec]
		  ,0 [DevSecReef]
		  ,0 [DevCap]
		  ,0 DEVTONS
		  ,null [LockedDate]
		  ,'' [LockedBY]
		  ,'' [DrillRig]
		  ,null [StoppedDate]
		  ,SECCAL.EndDate [EndDate]
		  ,cast(0 as bit) [IsStopped]
		  ,null [TopEnd]
		  ,null [AutoUnPlan]
		  ,0 [LabourStrength]
		  ,'' [MOCycle]
		  ,'' [Vac]
		  ,'' [DC]
		  ,'' [MOCycleNum]
		  ,'' [DevFlag]
		  ,'' [BoxHoleID]
		  ,cast(1 as bit) hasChanged
		  ,cast(0 as bit) hasRevised
	      ,ENDHEIGHT = isnull(wp.ENDHEIGHT,0)
	      ,ENDWIDTH = isnull(Wp.ENDWIDTH,0)

		FROM dbo.WORKPLACE WP
			INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
			LEFT JOIN dbo.vw_Kriging_Latest SAM ON  
			 WP.WORKPLACEID = SAM.WORKPLACEID and
			 SAM.ProdMonth = @Prodmonth

			 inner JOIN SECTION_COMPLETE SC on
			SC.SECTIONID_2 = @SectionID_2 and 
			SC.PRODMONTH = @Prodmonth

			 left join SECCAL on
	    SC.PRODMONTH = SECCAL.PRODMONTH and
	    SC.SECTIONID_1 = SECCAL.SECTIONID  
  
			WHERE [DESCRIPTION] = @WPDescription

				  --select * from workplace where workplaceid='53017'


end
else
begin

 insert into dbo.PLANMONTH
	(
	      MonthlyReefSQM
		  ,MonthlyWatseSQM
		   ,MonthlyTotalSQM
		    ,[Prodmonth]
		  ,[Sectionid]
		  , [Activity]
		  , [IsCubics]
		  , [PlanCode]
		  ,  [StartDate]
		  , [Workplaceid]
		  , [TargetID]
		  , [OrgUnitDay]
		  , [OrgUnitAfternoon]
		  , [OrgUnitNight]
		  , [RomingCrew]
		  , Locked
		  , [Auth]
		  , [SQM]
		  , [ReefSQM]
		  , [WasteSQM]
		  , [FL]
		  , [ReefFL]
		  , [WasteFL]
		  , [FaceAdvance]
		  ,[IdealSW]
		  , [SW]
		  , [CW]
		  , [CMGT]
		  , [CMKGT]
		  , [GT]
		  , [Kg]
		  , [FaceCMGT]
		  , [FaceKG]
		  , [Tons]
		  , [ReefTons]
		  , [WasteTons]
		  , [FaceValue]
		  , [CubicMetres]
		  , [Cubics]
		  , [CubicsReef]
		  , [CubicsWaste]
		  , [CubicsTons]
		  , [CubicsReefTons]
		  , [CubicsWasteTons]
		  , [CubicGrams]
		  , [CubicDepth]
		  , [ActualDepth]
		  , [CubicGT]
		  , [TrammedTons]
		  , [TrammedValue]
		  ,[TrammedLevel]
		  , [Metresadvance]
		  , [ReefAdv]
		  , [WasteAdv]
		  , [DevMain]
		  , [DevSec]
		  , [DevSecReef]
		  , [DevCap]
		 
		  , [LockedDate]
		  ,[LockedBY]
		  , [DrillRig]
		  , [StoppedDate]
		  ,[EndDate]
		  , [IsStopped]
		  , [TopEnd]
		  , [AutoUnPlan]
		  ,[LabourStrength]
		  , [MOCycle]
		  , [Vac]
		  , [DC]
		  , [MOCycleNum]
		  , [DevFlag]
		  , [BoxHoleID]

	      ,DHeight 
	      ,DWidth 

	)
	SELECT  
	DISTINCT 
		  0 MonthlyReefSQM,
		  0 MonthlyWatseSQM,
		  0 MonthlyTotalSQM,
		   @Prodmonth [Prodmonth]
		  ,-1 [Sectionid]
		  ,wp.activity [Activity]
		  ,'N' [IsCubics]
		  ,'MP' [PlanCode]
		  ,SECCAL.BeginDate  [StartDate]
		  ,WP.Workplaceid [Workplaceid]
		  ,-1 [TargetID]
		  ,'' [OrgUnitDay]
		  ,'' [OrgUnitAfternoon]
		  ,'' [OrgUnitNight]
		  ,'' [RomingCrew]
		  ,cast(0 as bit) Locked
		  ,cast(0 as bit) [Auth]
		  ,0 [SQM]
		  ,0 [ReefSQM]
		  ,0 [WasteSQM]
		  ,20 [FL]
		  ,0 [ReefFL]
		  ,0 [WasteFL]
		  ,0 [FaceAdvance]
		  ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0  
				WHEN SAM.ChannelWidth < 80 THEN 100 ELSE SAM.ChannelWidth + 20 END [IdealSW]
		  ,CASE WHEN SAM.StopeWidth IS NULL THEN 0 ELSE SAM.StopeWidth END [SW]
		  ,CASE WHEN SAM.ChannelWidth IS NULL THEN 0 ELSE SAM.ChannelWidth END [CW]
		  ,CASE WHEN SAM.CMGT IS NULL THEN 0 ELSE SAM.CMGT END [CMGT]
		  ,0 [CMKGT]
		  ,0 [GT]
		  ,0 [Kg]
		  ,0 [FaceCMGT]
		  ,0 [FaceKG]
		  ,0 [Tons]
		  ,0 [ReefTons]
		  ,0 [WasteTons]
		  ,0 [FaceValue]
		  ,0 [CubicMetres]
		  ,0 [Cubics]
		  ,0 [CubicsReef]
		  ,0 [CubicsWaste]
		  ,0 [CubicsTons]
		  ,0 [CubicsReefTons]
		  ,0 [CubicsWasteTons]
		  ,0 [CubicGrams]
		  ,0 [CubicDepth]
		  ,0 [ActualDepth]
		  ,0 [CubicGT]
		  ,0 [TrammedTons]
		  ,0 [TrammedValue]
		  ,o.LEVELNUMber [TrammedLevel]
		  ,0 [Metresadvance]
		  ,0 [ReefAdv]
		  ,0 [WasteAdv]
		  ,0 [DevMain]
		  ,0 [DevSec]
		  ,0 [DevSecReef]
		  ,0 [DevCap]
		  ,null [LockedDate]
		  ,'' [LockedBY]
		  ,'' [DrillRig]
		  ,null [StoppedDate]
		  ,SECCAL.EndDate [EndDate]
		  ,cast(0 as bit) [IsStopped]
		  ,null [TopEnd]
		  ,null [AutoUnPlan]
		  ,0 [LabourStrength]
		  ,'' [MOCycle]
		  ,'' [Vac]
		  ,'' [DC]
		  ,'' [MOCycleNum]
		  ,'' [DevFlag]
		  ,'' [BoxHoleID]
	      ,ENDHEIGHT = isnull(wp.ENDHEIGHT,0)
	      ,ENDWIDTH = isnull(Wp.ENDWIDTH,0)

		FROM dbo.WORKPLACE WP
			INNER JOIN OREFLOWENTITIES o on o.oreflowid = wp.oreflowid 
			LEFT JOIN dbo.vw_Kriging_Latest SAM ON  
			 WP.WORKPLACEID = SAM.WORKPLACEID and
			 SAM.ProdMonth = @Prodmonth

			 inner JOIN SECTION_COMPLETE SC on
			SC.SECTIONID_2 = @SectionID_2 and 
			SC.PRODMONTH = @Prodmonth

			 left join SECCAL on
	    SC.PRODMONTH = SECCAL.PRODMONTH and
	    SC.SECTIONID_1 = SECCAL.SECTIONID  
  
			WHERE [DESCRIPTION] = @WPDescription
	SELECT  
	DISTINCT 
		   0 MonthlyReefSQM,
		   0 MonthlyWatseSQM,
		   0 MonthlyTotalSQM,
		   @Prodmonth [Prodmonth]
		  ,-1 [Sectionid]
		  ,@SectionID_2 [Sectionid_2]
		  ,@WPDescription [WorkplaceDesc]
		  ,@ActivityCode [Activity]
		  ,'N' [IsCubics]
		  ,'MP' [PlanCode]
		  ,GetDate()  [StartDate]
		 -- ,-1 [Workplaceid]
		  ,WP.Workplaceid [Workplaceid]
		  ,-1 [TargetID]
		  ,'' [OrgUnitDay]
		  ,'' [OrgUnitAfternoon]
		  ,'' [OrgUnitNight]
		  ,'' [RomingCrew]
		  ,cast(0 as bit) Locked
		  ,cast(0 as bit) [Auth]
		  ,0 [SQM]
		  ,0 [ReefSQM]
		  ,0 [WasteSQM]
		  ,0 [FL]
		  ,0 [ReefFL]
		  ,0 [WasteFL]
		  ,0 [FaceAdvance]
		  ,0 [IdealSW]
		  ,0 [SW]
		  ,0 [CW]
		  ,0 [CMGT]
		  ,0 [CMKGT]
		  ,'OFF' RockType
		  ,0 [GT]
		  ,0 [Kg]
		  ,0 [UranuimBroken]
		  ,0 [FaceCMGT]
		  ,0 [FaceKG]
		  ,0 [Tons]
		  ,0 [ReefTons]
		  ,0 [WasteTons]
		  ,0 [FaceValue]
		  ,0 [CubicMetres]
		  ,0 [Cubics]
		  ,0 [CubicsReef]
		  ,0 [CubicsWaste]
		  ,0 [CubicsTons]
		  ,0 [CubicsReefTons]
		  ,0 [CubicsWasteTons]
		  ,0 [CubicGrams]
		  ,0 [CubicDepth]
		  ,0 [ActualDepth]
		  ,0 [CubicGT]
		  ,0 [TrammedTons]
		  ,0 [TrammedValue]
		  ,0 [TrammedLevel]
		  ,0 [Metresadvance]
		  ,0 [ReefAdv]
		  ,0 [WasteAdv]
		  ,0 [DevMain]
		  ,0 [DevSec]
		  ,0 [DevSecReef]
		  ,0 [DevCap]
		  ,0 DEVTONS
		  ,null [LockedDate]
		  ,'' [LockedBY]
		  ,'' [DrillRig]
		  ,null [StoppedDate]
		  ,GetDate() [EndDate]
		  ,cast(0 as bit) [IsStopped]
		  ,null [TopEnd]
		  ,null [AutoUnPlan]
		  ,0 [LabourStrength]
		  ,'' [MOCycle]
		  ,'' [Vac]
		  ,'' [DC]
		  ,'' [MOCycleNum]
		  ,'' [DevFlag]
		  ,'' [BoxHoleID]
		  ,cast(1 as bit) hasChanged
		  ,cast(0 as bit) hasRevised
	      ,ENDHEIGHT = isnull(wp.ENDHEIGHT,0)
	      ,ENDWIDTH = isnull(Wp.ENDWIDTH,0)

		FROM dbo.WORKPLACE WP where description=@WPDescription

	end
end       


