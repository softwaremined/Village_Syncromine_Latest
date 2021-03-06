USE [PAS_DNK_Syncromine]
GO
/****** Object:  StoredProcedure [dbo].[sp_RevisedPlanning_AddWorkPlace]    Script Date: 2019/03/01 11:46:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[sp_RevisedPlanning_AddWorkPlace] 
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
@ActivityCode NUMERIC(7,0), 
@SQL Varchar(1000),
@WPDesc Varchar(50),
@OrgDay Varchar(20),
@OrgNight Varchar(20),
@OrgAfternoon Varchar(20),
@Roming Varchar(20),
@ReefSQM NUMERIC(7,3),
@WasteSQM NUMERIC(7,3),
@ReefMeters NUMERIC(7,3),
@WasteMeters NUMERIC(7,3),
@Startdate datetime,
@IsCubic Varchar(20),
@MiningMethod varchar(20),
@FL numeric(10,3)


--SET @RequestID = 1110
--SET @UserID = 'MINEWARE'

SET @SQM = (SELECT CR.ReefSQM + CR.WasteSQM FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Cubes = (SELECT CR.CubicMeters FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Prodmonth =(SELECT CR.ProdMonth  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Sectionid_2 =(SELECT CR.SectionID_2  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @SectionID =(SELECT CR.SectionID   FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID = (SELECT CR.WorkplaceID FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @MiningMethod =(SELECT CR.MiningMethod  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

--IF @WorkplaceID  NOT IN (SELECT pp.Workplaceid    FROM PrePlanning pp WHERE Prodmonth =@Prodmonth   and Sectionid_2 =@Sectionid_2  )
--BEGIN
--SET @ActivityCode =  0
--END
--ELSE BEGIN
--SET @ActivityCode =1
--END
--SET @ActivityCode =(SELECT TOP 1 Activity    FROM PLANMONTH pp WHERE Prodmonth =@Prodmonth   and Sectionid_2 =@Sectionid_2  )
SET @ActivityCode =(SELECT Activity    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID  )

SET @IsCubic ='N'

DELETE FROM dbo.PLANMONTH WHERE Workplaceid = @WorkplaceID AND Prodmonth = @Prodmonth AND AutoUnPlan = 'Y' AND Activity = @ActivityCode
DELETE FROM dbo.PLANNING WHERE Workplaceid = @WorkplaceID AND Prodmonth = @Prodmonth AND Activity = @ActivityCode


--(SELECT DESCRIPTION  FROM WORKPLCE WHERE WORKPLACEID =@WorkplaceID )
SET @WPDesc =(SELECT DESCRIPTION  FROM WORKPLACE WHERE WORKPLACEID =@WorkplaceID )
SET @OrgDay =(SELECT DayCrew  FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @OrgNight =(SELECT NightCrew   FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @OrgAfternoon =(SELECT AfternoonCrew   FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @Roming =(SELECT RovingCrew   FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @ReefSQM =(SELECT ReefSQM    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @WasteSQM =(SELECT WasteSQM    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @ReefMeters =(SELECT Meters    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @WasteMeters =(SELECT MetersWaste    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @SectionID =(SELECT SectionID    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @Sectionid_2 =(SELECT SectionID_2    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @Startdate =(SELECT StartDate   FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
set @FL =(SELECT FL  FROM PREPLANNING_CHANGEREQUEST CR WHERE CR.ChangeRequestID =@RequestID )

IF @ActivityCode =0
BEGIN
INSERT INTO PLANMONTH (Prodmonth , Sectionid ,Activity ,IsCubics ,PlanCode, Workplaceid ,TargetID ,MonthlyTotalSQM, MonthlyReefSQM,MonthlyWatseSQM ,FL, CubicMetres ,StartDate ,OrgUnitDay ,OrgUnitNight ,OrgUnitAfternoon ,RomingCrew ) VALUES
(@Prodmonth , @SectionID ,@ActivityCode ,@IsCubic ,'MP',@WorkplaceID ,@MiningMethod,@ReefSQM + @WasteSQM , @ReefSQM ,@WasteSQM ,@FL,@Cubes , @Startdate ,@OrgDay ,@OrgNight ,@OrgAfternoon ,@Roming )
END

ELSE BEGIN
IF @ActivityCode =1
BEGIN
INSERT INTO PLANMONTH (Prodmonth  , Sectionid  ,Activity ,IsCubics ,PlanCode,  Workplaceid ,TargetID , [Metresadvance], ReefAdv ,WasteAdv ,FL , CubicMetres ,StartDate ,OrgUnitDay ,OrgUnitNight ,OrgUnitAfternoon ,RomingCrew ) VALUES
(@Prodmonth , @SectionID ,@ActivityCode ,@IsCubic ,'MP',@WorkplaceID ,@MiningMethod ,  @ReefMeters + @WasteMeters, @ReefMeters  ,@WasteMeters  ,@FL ,@Cubes, @Startdate ,@OrgDay ,@OrgNight ,@OrgAfternoon ,@Roming )
END
END









