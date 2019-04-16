USE [PAS_DNK_Syncromine]
GO

alter Procedure sp_preplanning_save_previus_month
(	@Prodmonth NUMERIC(7,0),@Sectionid_2 VARCHAR(20),@Activity NUMERIC(1,0))

as

--declare	@Prodmonth NUMERIC(7,0),@Sectionid_2 VARCHAR(20),@Activity NUMERIC(1,0)

--SET @Prodmonth = 201905
--SET @Sectionid_2 = 'REA'
--SET @Activity = 0

--select top 1 (BeginDate) from SECCAL sec
--inner join SECTION_COMPLETE sc on sec.Sectionid = sc.Sectionid_1  
--where sec.Prodmonth = @Prodmonth and sc.Sectionid_2 = @Sectionid_2

SELECT SC.SectionID,Begindate StartDate, EndDate  FROM  SECTION_COMPLETE SC 
                                         inner join SECCAL on
                                         SC.PRODMONTH = SECCAL.PRODMONTH and 
                                         SC.SECTIONID_1 = SECCAL.SECTIONID  
										 
                                         WHERE SC.PRODMONTH = @Prodmonth and sc.Sectionid_2 = @Sectionid_2 

INSERT INTO [dbo].[PLANMONTH]
           ([Prodmonth]
           ,[Sectionid]
           ,[Activity]
           ,[IsCubics]
           ,[PlanCode]
           ,[StartDate]
           ,[Workplaceid]
           ,[TargetID]
           ,[OrgUnitDay]
           ,[OrgUnitAfternoon]
           ,[OrgUnitNight]
           ,[RomingCrew]
           ,[Locked]
           ,[Auth]
           ,[SQM]
           ,[ReefSQM]
           ,[WasteSQM]
           ,[FL]
           ,[ReefFL]
           ,[WasteFL]
           ,[FaceAdvance]
           ,[IdealSW]
           ,[SW]
           ,[CW]
           ,[CMGT]
           ,[GT]
           ,[Kg]
           ,[FaceCMGT]
           ,[FaceKG]
           ,[Tons]
           ,[ReefTons]
           ,[WasteTons]
           ,[FaceValue]
           ,[CubicMetres]
           ,[Cubics]
           ,[CubicsReef]
           ,[CubicsWaste]
           ,[CubicsTons]
           ,[CubicsReefTons]
           ,[CubicsWasteTons]
           ,[CubicGrams]
           ,[CubicDepth]
           ,[ActualDepth]
           ,[CubicGT]
           ,[TrammedTons]
           ,[TrammedValue]
           ,[TrammedLevel]
           ,[Metresadvance]
           ,[ReefAdv]
           ,[WasteAdv]
           ,[DevMain]
           ,[DevSec]
           ,[DevSecReef]
           ,[DevCap]
           ,[LockedDate]
           ,[LockedBY]
           ,[DrillRig]
           ,[StoppedDate]
           ,[EndDate]
           ,[IsStopped]
           ,[TopEnd]
           ,[AutoUnPlan]
           ,[LabourStrength]
           ,[BoxHoleID]
           ,[MOCycle]
           ,[Vac]
           ,[DC]
           ,[MOCycleNum]
           ,[DevFlag]
           ,[CMKGT]
           ,[UraniumBrokenKG]
           ,[DHeight]
           ,[DWidth]
           ,[Density]
           ,[ReefWaste]
           ,[SurveyAdded]
           ,[DefaultCycle]
           ,[MonthlyReefSQM]
           ,[MonthlyWatseSQM]
           ,[MonthlyTotalSQM])

SELECT @Prodmonth [Prodmonth]
      ,pm.[Sectionid]
      ,[Activity]
      ,[IsCubics]
      ,[PlanCode]
      ,[StartDate] = (select top 1 (BeginDate) from SECCAL sec
inner join SECTION_COMPLETE sc on sec.Sectionid = sc.Sectionid_1  
where sec.Prodmonth = @Prodmonth and sc.Sectionid_2 = @Sectionid_2)
      ,[Workplaceid]
      ,[TargetID]
      ,[OrgUnitDay]
      ,[OrgUnitAfternoon]
      ,[OrgUnitNight]
      ,[RomingCrew]
      ,0 [Locked]
      ,[Auth]
      ,0 [SQM]
      ,0 [ReefSQM]
      ,0 [WasteSQM]
      ,[FL]
      ,0 [ReefFL]
      ,0 [WasteFL]
      ,0 [FaceAdvance]
      ,0 [IdealSW]
      ,0 [SW]
      ,0 [CW]
      ,0 [CMGT]
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
      , 0[TrammedValue]
      , [TrammedLevel]
      ,0 [Metresadvance]
      ,0 [ReefAdv]
      ,0 [WasteAdv]
      ,0 [DevMain]
      ,0 [DevSec]
      ,0 [DevSecReef]
      ,0 [DevCap]
      ,NULL [LockedDate]
      ,NULL [LockedBY]
      ,[DrillRig]
      ,[StoppedDate]
      ,[EndDate] =  (select top 1 (EndDate) from SECCAL sec
inner join SECTION_COMPLETE sc on sec.Sectionid = sc.Sectionid_1  
where sec.Prodmonth = @Prodmonth and sc.Sectionid_2 = @Sectionid_2)
      ,'N' [IsStopped]
      ,[TopEnd]
      ,[AutoUnPlan]
      ,[LabourStrength]
      ,[BoxHoleID]
      ,[MOCycle]
      ,[Vac]
      ,[DC]
      ,[MOCycleNum]
      ,[DevFlag]
      ,[CMKGT]
      ,[UraniumBrokenKG]
      ,[DHeight]
      ,[DWidth]
      ,[Density]
      ,0 [ReefWaste]
      ,[SurveyAdded]
      ,[DefaultCycle]
      ,0 [MonthlyReefSQM]
      ,0 [MonthlyWatseSQM]
      ,0 [MonthlyTotalSQM]
  FROM [dbo].[PLANMONTH] PM
  INNER JOIN dbo.SECTION_COMPLETE SC ON
PM.PRODMONTH = SC.PRODMONTH AND
PM.SECTIONID = SC.SECTIONID
 
  WHERE PM.PRODMONTH = dbo.GetPrevProdMonth(@Prodmonth) AND
      SC.SECTIONID_2 = @Sectionid_2 AND
      (PM.IsStopped <> 'Y' OR PM.IsStopped IS NULL) AND 
      PM.ACTIVITY = @Activity AND
      PM.IsCubics = 'N' and PM.Plancode = 'MP' and PM.Locked = 1
GO


