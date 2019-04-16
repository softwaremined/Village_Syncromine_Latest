update planmonth set stoppeddate = null where stoppeddate = '1900/01/01' 
update planmonth set autounplan = null where autounplan = '0' 
go
Alter Table planprot_fields add Deleted Bit
GO

update planprot_fields set Deleted = 0
go

Create Procedure [dbo].[sp_PlanProt_TemplateSetupSave]
@TemplateID int, -- TemplateTable
@TemplateName varchar(200),--TemplateTable
@Activity numeric(6,0),--TemplateTable
@FieldID int, --FieldsTable
--@FieldType varchar(10), --FieldsTable
@SelectedValue varchar(50),--FieldValueTable
@MinValue varchar (50),--FieldValueTable
@MaxValue varchar(50),--FieldValueTable
@RiskRating int,--FieldValueTable
@ParentID int,--Grouping
@FrontBack int,
@Action int,
@FieldUpdate int,
@User1 varchar(50), 
@User2 varchar(50), 
@AprovalReq BIT

AS
--Declare @FieldId int --FieldsTable

DECLARE @countGrouping INT

if @FrontBack = 1
begin

	if @Action = 0
	begin
		delete from PlanProt_Template
		where TemplateName = @TemplateName AND Activity = @Activity

		insert into PlanProt_Template
		(
		TemplateID,
		TemplateName,
		Activity,
		User1,
		User2,
		ApprovalRequired
		)
		values
		(
		@TemplateID,
		@TemplateName,
		@Activity,
		@User1,
		@User2,
		@AprovalReq
		)
		end
	else
	begin
		update PlanProt_Template Set TemplateName = @TemplateName, Activity = @Activity, User1 = @User1, User2 = @User2, ApprovalRequired = @AprovalReq where TemplateID = @TemplateID
	end

end

if @FrontBack = 2
	begin
	if @FieldUpdate = 0
	begin
		--delete from PlanProt_Fields where
		--FieldName = @FieldName


		--insert into PlanProt_Fields
		--(
		--TemplateID,
		--FieldName,
		--FieldType
		--)
		--values(
		--@TemplateID,
		--@FieldName,
		--@FieldType
		--)

		--select @FieldId = FieldID from PlanProt_Fields
		--where FieldName = @FieldName

		insert into PlanProt_FieldValue
		(
		FieldID,
		SelectedValue,
		MinValue,
		MaxValue,
		RiskRating
		)
		values
		(
		@FieldId,
		@SelectedValue,
		@MinValue,
		@MaxValue,
		@RiskRating
		)

		update PlanProt_fields Set ParentID = @ParentID where FieldID = @FieldID 

		end
		
		--end
		
		if @FieldUpdate = 1
		begin
		
		insert into PlanProt_FieldValue
		(
		FieldID,
		SelectedValue,
		MinValue,
		MaxValue,
		RiskRating
		)
		values
		(
		@FieldId,
		@SelectedValue,
		@MinValue,
		@MaxValue,
		@RiskRating
		)
		
		
		update PlanProt_fields Set ParentID = @ParentID where FieldID = @FieldID 
		end

	end
GO

ALTER Procedure [dbo].[sp_RevisedPLanning_Request]
--Declare

 @requestType int,        
       @ProdMonth NUMERIC(7,0), 
       @WorkplaceID varchar(20),
       @SectionID varchar(20),
       @SectionID_2 varchar(20),
       @StopDate DateTime, 
       @UserComments varchar(150), 
       @RequestBy varchar(50),
       @SQMOn numeric(7,3),
       @SQMOff numeric(7,3),
       @Cube numeric(7,3),
       @MeterOn numeric(7,3),
       @MeterOff numeric(7,3),
	   @DayCrew varchar(50), 
	   @NightCrew varchar(50),
	   @AfternoonCrew varchar(50),
	   @RovingCrew varchar(50),
	   @StartDate datetime,
	   @MiningMethod varchar(20),
	   @OldWorkplaceID varchar(20),
	   @activity int,
	   @Facelength numeric(13,3),
	   @DrillRig  varchar(150),
	   @DeleteBookings bit = 0


--| Request Types
--| 1 = Stop Workplace

AS

--DECLARE
-- @requestType int,        
--       @ProdMonth NUMERIC(7,0), 
--       @WorkplaceID varchar(20),
--       @SectionID varchar(20),
--       @SectionID_2 varchar(20),
--       @StopDate DateTime, 
--       @UserComments varchar(150), 
--       @RequestBy varchar(50),
--       @SQMOn numeric(7,3),
--       @SQMOff numeric(7,3),
--       @Cube numeric(7,3),
--       @MeterOn numeric(7,3),
--       @MeterOff numeric(7,3),
--	   @DayCrew varchar(10), 
--	   @NightCrew varchar(10),
--	   @AfternoonCrew varchar(10),
--	   @RovingCrew varchar(10),
--	   @StartDate datetime,
--	   @MiningMethod varchar(20),
--	   @OldWorkplaceID varchar(20),
--	   @activity int,
--	   @Facelength numeric(13,3)

--Select @requestType = 5,
--@ProdMonth = 201706,
--@WorkplaceID = 'RE002474',
--@SectionID = 'REAAHCA',
--@SectionID_2 = 'REA',
--@DayCrew = 'REAAAANURH',
--@NightCrew =  '',
--@AfternoonCrew =  '',
--@RovingCrew = '',
--@StartDate = '001/01/01',
--@StopDate = '001/01/01',
--@UserComments = 'test',
--@RequestBy = 'mineware',
--@SQMOn = 150,
--@SQMOff = 0,
--@Cube = 0,
--@MeterOn = 0,
--@MeterOff = 0,
--@MiningMethod = 2,
--@OldWorkplaceID = '192 S5 E 1B',
--@activity = 0,
--@Facelength = 20,
--@DrillRig = '',
--@DeleteBookings = 0

DECLARE @theShaft varchar(50),
@ucPlanningValueChanges INT,
@ucCrewMinerChange int,
@ucAddWorkplace INT, 
@StopWorkplace int,
@MovePlanning INT,
@StartWorkplace int,
@MiningMethodChange int, 
@ChnagrequestID int,
@theSection varchar(50),
@AppReqd int,
@DrillRigChange int,
@DeleteWorkplace int
SET @StopWorkplace = 1
SET @ucCrewMinerChange=3
SET @ucAddWorkplace=2
SET @ucPlanningValueChanges=4
SET @MovePlanning=5
SET @StartWorkplace=6
SET @MiningMethodChange=7
set @DrillRigChange=8
set @DeleteWorkplace=9

IF @requestType = @StopWorkplace 
BEGIN

	INSERT INTO PREPLANNING_CHANGEREQUEST 
		  ([ProdMonth]
		  ,[RequestBy]
		  ,[SectionID]
		  ,[SectionID_2]
		  ,[WorkplaceID]
		  ,[ChangeID]
		  ,[DayCrew]
		  ,[NightCrew]
		  ,[AfternoonCrew]
		  ,[RovingCrew]
		  ,[StartDate]
		  ,[StopDate]
		  ,[Comments]
		  ,[ReefSQM]
		  ,[WasteSQM]
		  ,[CubicMeters]
		  ,[Meters]
		  ,[MetersWaste]
		  ,[MiningMethod]
		  ,[OldWorkplaceID]
		  ,[Activity]
		  ,[FL] 
		  ,[DrillRig]
		  ,[DeleteBookings])

	VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@StopWorkplace,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
			@SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig,@DeleteBookings)
			SELECT @ChnagrequestID = SCOPE_IDENTITY()

			--select * from PREPLANNING_CHANGEREQUEST 

	SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
	WHERE PRODMONTH = @ProdMonth and
		  SECTIONID = @SectionID_2 ) 

	SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
	WHERE SC.PRODMONTH = @ProdMonth and
		  SC.Name_2 = @theSection)
      
     

	-- Get all the users that needs to approve tha request
	DECLARE @User1 varchar(50), @User2 varchar(50), @Department varchar(50),@approvalReqd int
	DECLARE db_Approval CURSOR FOR  
			Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
		--  rs.Department  =ppns.Department AND
		  RS.DEPARTMENT=PPNS.DEPARTMENT
	INNER JOIN SECTION_COMPLETE SC ON
	--rs.Section = SC.SECTIONID_3 AND
	RS.SECTION=SC.SECTIONID_3
	WHERE SC.PRODMONTH = @ProdMonth and 
		  SC.SECTIONID_2 = @SectionID_2 and 
		 -- PPNS.Section = '2' and
		  PPNS.ChangeID = 1   and active=1 and RS.SecurityType =1 and
		  rs.UserID > '' and rs.UserID is not null
		  UNION
			Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
		  --rs.UserID =ppns.UserID AND
		  RS.DEPARTMENT=PPNS.DEPARTMENT
	INNER JOIN SECTION_COMPLETE SC ON
	--PPNS.Section = SC.SECTIONID_2 AND
	RS.SECTION=SC.SECTIONID_2
	WHERE SC.PRODMONTH = @ProdMonth and 
		  SC.SECTIONID_2 = @SectionID_2 and 
		  --PPNS.Section = '318460' and
		  PPNS.ChangeID = 1   and active=1 and RS.SecurityType =1 and
		  rs.UserID > '' and rs.UserID is not null

	OPEN db_Approval   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval INTO @User1,@User2,@Department,@approvalReqd
WHILE @@FETCH_STATUS = 0   
BEGIN

	INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
				(ChangeRequestID,
				 User1,
				 User2,
				 Department,
				 RequestDate,
				 ApprovalRequired	         )
	VALUES (@ChnagrequestID,@User1,@User2,@Department,GETDATE(),@approvalReqd )
	         
	FETCH NEXT FROM db_Approval INTO @User1,@User2,@Department,@approvalReqd
	END 

	CLOSE db_Approval   
	DEALLOCATE db_Approval   

	-- This mail
	EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 

	-- End Mail       

	END  -- IF @requestType = @StopWorkplace   

IF @requestType = @ucCrewMinerChange 
BEGIN

	INSERT INTO PREPLANNING_CHANGEREQUEST 
		  ([ProdMonth]
		  ,[RequestBy]
		  ,[SectionID]
		  ,[SectionID_2]
		  ,[WorkplaceID]
		  ,[ChangeID]
		  ,[DayCrew]
		  ,[NightCrew]
		  ,[AfternoonCrew]
		  ,[RovingCrew]
		  ,[StartDate]
		  ,[StopDate]
		  ,[Comments]
		  ,[ReefSQM]
		  ,[WasteSQM]
		  ,[CubicMeters]
		  ,[Meters]
		  ,[MetersWaste]
		  ,[MiningMethod]
		  ,[OldWorkplaceID]
		  ,[Activity]
		  ,[FL] 
		  ,[DrillRig])

	VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@ucCrewMinerChange,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
			@SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
			SELECT @ChnagrequestID = SCOPE_IDENTITY()

	SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
	WHERE PRODMONTH = @ProdMonth and
		  SECTIONID = @SectionID_2 ) 

	SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
	WHERE SC.PRODMONTH = @ProdMonth and
		  SC.Name_2 = @theSection)
      
     

	-- Get all the users that needs to approve tha request
	DECLARE @User11 varchar(50), @User22 varchar(50), @Department1 varchar(50),@approvalReqd1 int
	DECLARE db_Approval1 CURSOR FOR  
			Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
		--  rs.Department  =ppns.Department AND
		  RS.DEPARTMENT=PPNS.DEPARTMENT
	INNER JOIN SECTION_COMPLETE SC ON
	--rs.Section = SC.SECTIONID_3 AND
	RS.SECTION=SC.SECTIONID_3
	WHERE SC.PRODMONTH = @ProdMonth and 
		  SC.SECTIONID_2 = @SectionID_2 and 
		 -- PPNS.Section = '2' and
		  PPNS.ChangeID = 3   and active=1 and RS.SecurityType =1 and
		  rs.UserID > '' and rs.UserID is not null
		  UNION
			Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
		  --rs.UserID =ppns.UserID AND
		  RS.DEPARTMENT=PPNS.DEPARTMENT
	INNER JOIN SECTION_COMPLETE SC ON
	--PPNS.Section = SC.SECTIONID_2 AND
	RS.SECTION=SC.SECTIONID_2
	WHERE SC.PRODMONTH = @ProdMonth and 
		  SC.SECTIONID_2 = @SectionID_2 and 
		  --PPNS.Section = '318460' and
		  PPNS.ChangeID = 3   and active=1 and RS.SecurityType =1 and
		  rs.UserID > '' and rs.UserID is not null

	OPEN db_Approval1   
	-- end

	-- Add a approval record for each department that needs to approve the request.
	FETCH NEXT FROM db_Approval1 INTO @User11,@User22,@Department1,@approvalReqd1
	WHILE @@FETCH_STATUS = 0   
	BEGIN

	INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
				(ChangeRequestID,
				 User1,
				 User2,
				 Department,
				 RequestDate	      ,
				 ApprovalRequired	         )
	VALUES (@ChnagrequestID,@User11,@User22,@Department1,GETDATE(),@approvalReqd1 )
	         
	FETCH NEXT FROM db_Approval1 INTO @User11,@User22,@Department1,@approvalReqd1
END 

CLOSE db_Approval1   
DEALLOCATE db_Approval1   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 

-- End Mail      
end 

IF @requestType = @ucAddWorkplace 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@ucAddWorkplace,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User111 varchar(50), @User222 varchar(50), @Department11 varchar(50),@approvalReqd2 int
DECLARE db_Approval11 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 2   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 2   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval11   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval11 INTO @User111,@User222,@Department11,@approvalReqd2
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	         ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User111,@User222,@Department11,GETDATE(), @approvalReqd2)
	         
FETCH NEXT FROM db_Approval11 INTO @User111,@User222,@Department11,@approvalReqd2
END 

CLOSE db_Approval11   
DEALLOCATE db_Approval11   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 

-- End Mail  
END

IF @requestType = @ucPlanningValueChanges 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@ucPlanningValueChanges,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User1111 varchar(50), @User2222 varchar(50), @Department111 varchar(50),@approvalReqd3 int
DECLARE db_Approval111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 4   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 4   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval111 INTO @User1111,@User2222,@Department111,@approvalReqd3
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	          ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User1111,@User2222,@Department111,GETDATE(),@approvalReqd3)
	         
FETCH NEXT FROM db_Approval111 INTO @User1111,@User2222,@Department111,@approvalReqd3
END 

CLOSE db_Approval111   
DEALLOCATE db_Approval111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 

-- End Mail  
end

IF @requestType = @MovePlanning 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID_2]
      ,[SectionID]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

SELECT a.[ProdMonth]
      ,@RequestBy [RequestBy]
	  ,@SectionID_2
      ,a.[SectionID]
      ,@WorkplaceID [WorkplaceID]
      ,@MovePlanning [ChangeID]
      ,OrgUnitDay
      ,OrgUnitNight
      ,OrgUnitAfternoon
      ,RomingCrew
      ,[StartDate]
      ,StoppedDate
      ,@UserComments [Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,CubicMetres
      ,ReefAdv
      ,WasteAdv
	  ,@MiningMethod [MiningMethod]
	  ,@OldWorkplaceID [OldWorkplaceID]
	  ,a.Activity
	   ,@Facelength,@DrillRig FROM planmonth  a inner join workplace b on a.workplaceid = b.WorkplaceID 
	    where b.Description = @OldWorkplaceID and a.prodmonth = @ProdMonth and plancode = 'MP' and a.Activity = @activity
	   SELECT @ChnagrequestID = SCOPE_IDENTITY()
--VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@MovePlanning,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
--        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength)
		

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User11111 varchar(50), @User22222 varchar(50), @Department1111 varchar(50),@approvalReqd4 int
DECLARE db_Approval11 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 5   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 5   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval11    
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval11 INTO @User11111,@User22222,@Department1111,@approvalReqd4
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	          ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User11111,@User22222,@Department1111,GETDATE(),@approvalReqd4 )
	         
FETCH NEXT FROM db_Approval11 INTO @User11111,@User22222,@Department1111,@approvalReqd4
END 

CLOSE db_Approval11   
DEALLOCATE db_Approval11   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end

IF @requestType = @StartWorkplace 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@StartWorkplace,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User111111 varchar(50), @User222222 varchar(50), @Department11111 varchar(50),@approvalReqd5 int
DECLARE db_Approval111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 6   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 6   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval111 INTO @User111111,@User222222,@Department11111,@approvalReqd5
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	        ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User111111,@User222222,@Department11111,GETDATE(),@approvalReqd5 )
	         
FETCH NEXT FROM db_Approval111 INTO @User111111,@User222222,@Department11111,@approvalReqd5
END 

CLOSE db_Approval111   
DEALLOCATE db_Approval111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end

IF @requestType = @MiningMethodChange 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@MiningMethodChange,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User1111111 varchar(50), @User2222222 varchar(50), @Department111111 varchar(50),@approvalReqd6 int
DECLARE db_Approval1111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 7   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 7   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval1111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval1111 INTO @User1111111,@User2222222,@Department111111,@approvalReqd6
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	         ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User1111111,@User2222222,@Department111111,GETDATE(),@approvalReqd6 )

	         
FETCH NEXT FROM db_Approval1111 INTO @User1111111,@User2222222,@Department111111,@approvalReqd6
END 

CLOSE db_Approval1111   
DEALLOCATE db_Approval1111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end

IF @requestType = @DrillRigChange 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@DrillRigChange,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User11111111 varchar(50), @User22222222 varchar(50), @Department1111111 varchar(50),@approvalReqd7 int
DECLARE db_Approval11111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 8   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 8   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval11111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval11111 INTO @User11111111,@User22222222,@Department1111111,@approvalReqd7
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	         ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User11111111,@User22222222,@Department1111111,GETDATE(),@approvalReqd7 )

	         
FETCH NEXT FROM db_Approval11111 INTO @User1111111,@User2222222,@Department111111,@approvalReqd7
END 

CLOSE db_Approval11111   
DEALLOCATE db_Approval11111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end

IF @requestType = @DeleteWorkplace 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@DeleteWorkplace,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User111111111 varchar(50), @User222222222 varchar(50), @Department11111111 varchar(50),@approvalReqd8 int
DECLARE db_Approval111111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 9   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 9   and active=1 and RS.SecurityType = 1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval111111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval111111 INTO @User111111111,@User222222222,@Department11111111,@approvalReqd8
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	         ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User111111111,@User222222222,@Department11111111,GETDATE(),@approvalReqd8 )

	         
FETCH NEXT FROM db_Approval111111 INTO @User11111111,@User22222222,@Department1111111,@approvalReqd8
END 

CLOSE db_Approval111111   
DEALLOCATE db_Approval111111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end


GO


ALTER Procedure [dbo].[sp_PrePlanning_data]
 @ChangeRequestID INT
AS
SELECT DISTINCT SC.Name_2 Section,SC.name_2,SC.Name,PPCR.DrillRig,PPCR.ProdMonth,PPCR.OldWorkplaceID,PPCR.StartDate,PPCR.WorkplaceID,PPCR.DayCrew,
PPCR.SectionID,PPCR.AfternoonCrew,PPCR.NightCrew,PPCR.RovingCrew,WP.DESCRIPTION WPDesc,
PPCR.StopDate,PPCR.MiningMethod,cm.Description,cm.TargetID TargetID,PPCR.Comments,PPCR.ReefSQM,PPCR.WasteSQM,PPCR.Meters,PPCR.MetersWaste,PPCR.CubicMeters,
PPCR.SectionID,PPCR.SectionID_2,PPCR.FL,PPCR .Activity, DeleteBookings  FROM PrePlanning_ChangeRequest PPCR
INNER JOIN SECTION_COMPLETE SC on
--PPCR.SectionID_2 = SC.SECTIONID_2 and
PPCR.ProdMonth = SC.PRODMONTH and 
PPCR.SectionID = SC.SECTIONID 
INNER JOIN WORKPLACE WP on 
PPCR.WorkplaceID = WP.WORKPLACEID 
left Join Code_Methods CM on
ppcr.MiningMethod = cm.TargetID

WHERE PPCR.ChangeRequestID = @ChangeRequestID
GO

ALTER Procedure [dbo].[sp_Get_Cycle]
--Declare
@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)

--DECLARE
--@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)
--SET @theProdmonth = 201707
--SET @theWorkplaceID = 'RE007968'
--SET @theSectionID = 'REABHDD'



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
	  CASE WHEN PLAND.CycleInput IS NULL THEN 'CAL' ELSE CycleInput END CycleInput
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

Update Planning set
MOCycle = case when SQM+Metresadvance > 0 then 'BL' else 'CL' end,
MOCycleCube = 'BL'
GO


ALTER Function [dbo].[RequestType_MessageBody] 
( 
   @ChnagrequestID INT
) 
RETURNS VARCHAR(MAX)
AS 
BEGIN
--DECLARE @ChnagrequestID INT
--SET @ChnagrequestID=2570
DECLARE @tableHTML  VARCHAR(MAX), @changeID INT, @daycrew varchar(50),@AfternoonCrew varchar(50),@NightCrew varchar(50),
@RovingCrew varchar(50),@ReefSQM numeric(7,0),@WasteSQM numeric(7,0),@Meters numeric(7,0),@MetersWaste numeric(7,0),
@CubicMeters numeric(7,0),@MiningMethod varchar(20),@FL NUMERIC(13,3),@ReefSQMOLD  numeric(7,0),@WasteSQMOLD numeric(7,0),
@MetersOLD numeric(7,0),@MetersWasteOLD numeric(7,0),
@CubicMetersOLD numeric(7,0),@Workplaceid varchar(20),@prodmonth numeric(7,0),@sectionid varchar(20), @sectionid_2 varchar(20),
@activity int,@FLOLD NUMERIC(13,3),@WPDesc varchar(50),@SectName varchar(50),@daycrewOLD varchar(50),@AfternoonCrewOLD varchar(50),@NightCrewOLD varchar(50),
@RovingCrewOLD varchar(50),@sectionidOLD varchar(20),@sectionNameOLD VARCHAR(50),@SectionName varchar(50),@stopdateOLD varchar(50),@stopdate varchar(50),
@startdateOLD VARCHAR(50),@startdate varchar(50),@OldWP VARCHAR(50),@MiningMethodOLD varchar(20),@miningmethodMOVEPLAN VARCHAR(50),@FLMOVEPLAN VARCHAR(50),
@DrillRig  VARCHAR(50)


SET @changeID=(select changeid from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @daycrew=(select DAYCREW from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @AfternoonCrew=(select AfternoonCrew from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @NightCrew=(select NightCrew from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @RovingCrew=(select RovingCrew from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @ReefSQM=(select ReefSQM from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @WasteSQM=(select WasteSQM from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @Meters=(select Meters from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @MetersWaste=(select MetersWaste from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @CubicMeters=(select CubicMeters from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
--SET @MiningMethod=(select MiningMethod from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @FL=(select FL from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @Workplaceid=(select Workplaceid from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @prodmonth=(select prodmonth from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @sectionid=(select sectionid from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @sectionid_2=(select sectionid_2 from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @activity=(select activity from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
set @WPDesc=(select description from workplace where workplaceid=@Workplaceid)
set @SectName=(select distinct name from section where Prodmonth = @prodmonth and sectionid=@sectionid_2)
SET @SectionName=(select distinct Name from section where Prodmonth = @prodmonth and sectionid=@sectionid)
SET @stopdate=(select stopdate from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @startdate=(select startdate from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @MiningMethod=(select Description from PrePlanning_ChangeRequest PPCR  LEFT JOIN Code_Methods BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PPCR.MiningMethod=BPD.TargetID where changerequestid=@ChnagrequestID)
SET @OldWP=(select OldWorkplaceID  from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)


SET @ReefSQMOLD=(select ReefSQM from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @WasteSQMOLD=(select WasteSQM from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @CubicMetersOLD=(select CubicMetres from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @MetersOLD=(select Reefadv from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @MetersWasteOLD=(select Wasteadv from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @FLOLD=(select FL from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @sectionidOLD=(select sectionid from planmonth where prodmonth=@prodmonth and workplaceid=@Workplaceid and plancode='LP')
SET @daycrewOLD=(select orgunitday from planmonth where prodmonth=@prodmonth and sectionid=@sectionidOLD  and workplaceid=@Workplaceid and plancode='LP')
SET @AfternoonCrewOLD=(select [OrgUnitAfternoon] from planmonth where prodmonth=@prodmonth and sectionid=@sectionidOLD and workplaceid=@Workplaceid and plancode='LP')
SET @NightCrewOLD=(select [OrgUnitNight] from planmonth where prodmonth=@prodmonth and sectionid=@sectionidOLD and workplaceid=@Workplaceid and plancode='LP')
SET @RovingCrewOLD=(select [RomingCrew] from planmonth where prodmonth=@prodmonth and sectionid=@sectionidOLD and workplaceid=@Workplaceid and plancode='LP')
SET @SectionNameOLD=(select distinct Name from section where Prodmonth = @prodmonth and sectionid=@sectionidOLD)
SET @stopdateOLD =(select StoppedDate from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @startdateOLD =(select StartDate from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @MiningMethodOLD=(select Description from planmonth PP LEFT JOIN [PERS].[dbo].[Bonus_PoolDefaults] BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PP.TargetID=BPD.TargetID where prodmonth=@prodmonth and sectionid=@sectionid and PP.workplaceid=@Workplaceid and plancode='LP')
SET @miningmethodMOVEPLAN=(select Description from planmonth PP LEFT JOIN [PERS].[dbo].[Bonus_PoolDefaults] BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PP.TargetID=BPD.TargetID where prodmonth=@prodmonth and sectionid=@sectionid and PP.Workplaceid=@OldWP and plancode='LP')
SET @FLMOVEPLAN=(select FL from planmonth PP LEFT JOIN [PERS].[dbo].[Bonus_PoolDefaults] BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PP.TargetID=BPD.TargetID where prodmonth=@prodmonth and sectionid=@sectionid and PP.WORKPLACEID=@OldWP and plancode='LP')

SET @DrillRig=(select DrillRig from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)

if @changeID=4 and @activity=1
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th></tr>'+
				--N'<tr>'+
				--N'<th> Facelength  </th>'+
				--N'<td>' + ISNULL(cast(@FLOLD as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
			
				N'<tr>'+
				N'<th> On Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>' +
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersWasteOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML
 if @changeID=4 and @activity=0
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Facelength  </th>'+
				N'<td>' + ISNULL(cast(@FLOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> On Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@ReefSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@WasteSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML

 if @changeID=3
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Miner  </th>'+
				N'<td>' + ISNULL(cast(@SectionNameOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Day Crew </th>'+
				N'<td>' + ISNULL(cast(@daycrewOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@daycrew as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Afternoon Crew </th>'+
				N'<td>' + ISNULL(cast(@AfternoonCrewOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@AfternoonCrew as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Night Crew </th>'+
				N'<td>' + ISNULL(cast(@NightCrewOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@NightCrew as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Roming Crew </th>'+
				N'<td>' + ISNULL(cast(@RovingCrewOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@RovingCrew as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML
 if @changeID=1 and @activity=0
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Stop Date  </th>'+
				N'<td>' + ISNULL(cast(@stopdateOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@stopdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> On Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@ReefSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@WasteSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML
if @changeID=1 and @activity=1
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Stop Date  </th>'+
				N'<td>' + ISNULL(cast(@stopdateOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@stopdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			
				N'<tr>'+
				N'<th> On Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>' +
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersWasteOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML

 if @changeID=6 and @activity=0
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th></tr>'+
				N'<tr>'+
				N'<th> Start Date  </th>'+
				N'<td>' + ISNULL(cast(@startdateOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			
				--N'</tr>'+
				N'<tr>'+
				N'<th> On Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@ReefSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@WasteSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML

 if @changeID=6 and @activity=1
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th></tr>'+
				N'<tr>'+
				N'<th> Start Date  </th>'+
				N'<td>' + ISNULL(cast(@startdateOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			
				N'<tr>'+
				N'<th> On Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>' +
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersWasteOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML

  if @changeID=2 and @activity=1
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th><th width="70%"> New Plan </th></tr>'+
				N'<tr>'+
				N'<th> Start Date  </th>'+
				N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			N'<tr>'+
				N'<th> Miner  </th>'+
				N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Mining Method  </th>'+
				N'<td>' + ISNULL(cast(@MiningMethod as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> On Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>' +
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML
  if @changeID=2 and @activity=0
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th><th width="70%"> New Plan </th></tr>'+
				N'<tr>'+
				N'<th> Start Date  </th>'+
				N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			N'<tr>'+
				N'<th> Miner  </th>'+
				N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Mining Method  </th>'+
				N'<td>' + ISNULL(cast(@MiningMethod as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Facelength  </th>'+
				N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> On Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML
  if @changeID=5
begin
 SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th width="70%"> Lock Plan </th> <th width="70%"> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Workplace  </th>'+
				N'<td>' + ISNULL(cast(@OldWP as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@WPDesc as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Mining Method  </th>'+
				N'<td>' + ISNULL(cast(@miningmethodMOVEPLAN as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MiningMethod as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Facelength  </th>'+
				N'<td>' + ISNULL(cast(@FLMOVEPLAN as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML

 if @changeID=7
begin
 SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th width="70%"> Lock Plan </th> <th width="70%"> New Plan </th> </tr>'+
				--N'<tr>'+
				--N'<th> Workplace  </th>'+
				--N'<td>' + ISNULL(cast(@OldWP as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@WPDesc as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				N'<tr>'+
				N'<th> Mining Method  </th>'+
				N'<td>' + ISNULL(cast(@MiningMethodOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MiningMethod as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				--N'<tr>'+
				--N'<th> Facelength  </th>'+
				--N'<td>' + ISNULL(cast(@FLMOVEPLAN as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				
				N'</table>'+
				N'</body>'+
				N'</html>'
end

 if @changeID=8
begin
 SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th width="70%"> Lock Plan </th> <th width="70%"> New Plan </th> </tr>'+
				--N'<tr>'+
				--N'<th> Workplace  </th>'+
				--N'<td>' + ISNULL(cast(@OldWP as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@WPDesc as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				N'<tr>'+
				N'<th> Drill Rig  </th>'+
				N'<td>' + ISNULL(cast(@DrillRig as varchar(50)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				--N'<tr>'+
				--N'<th> Facelength  </th>'+
				--N'<td>' + ISNULL(cast(@FLMOVEPLAN as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				
				N'</table>'+
				N'</body>'+
				N'</html>'
end

 if @changeID=9
begin
 SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th width="70%"> Lock Plan </th> <th width="70%"> New Plan </th> </tr>'+
				--N'<tr>'+
				--N'<th> Workplace  </th>'+
				--N'<td>' + ISNULL(cast(@OldWP as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@WPDesc as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				N'<tr>'+
				N'<th> Delete Workplace  </th>'+
				N'<td>' + ISNULL(cast(@WPDesc as varchar(50)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				--N'<tr>'+
				--N'<th> Facelength  </th>'+
				--N'<td>' + ISNULL(cast(@FLMOVEPLAN as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML
RETURN @tableHTML  
END	
GO
