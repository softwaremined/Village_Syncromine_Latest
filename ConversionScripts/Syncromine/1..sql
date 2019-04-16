ALTER TABLE dbo.tblMS_Dashboards ADD ModuleID VARCHAR(150)
ALTER TABLE dbo.tblSysset ADD SystemTimeOut INT

go
ALTER PROCEDURE [dbo].[spMSManageDashboards]
	-- Add the parameters for the stored procedure here
	@DashID INT,
	@Description VARCHAR(100),
	@DashFile VARCHAR(MAX),
	@IsSubDash BIT,
	@SubDashKey VARCHAR(200),
	@TopDashID INT,
	@DashProfiles [dbo].[typeMS_DashProfileLink] READONLY,
	@DashConnections [dbo].[typeMS_DashConnectionLink] READONLY,
	@ManageType INT, -- 1 = new, 2 = Edit, 3 = Delete
	@ModuleID  VARCHAR(200)
AS
DECLARE @Count int, @newDashID int, @SQL varchar(MAX);
BEGIN

IF @ManageType = 1
BEGIN
SET @Count = (SELECT count([Description]) FROM [dbo].tblMS_Dashboards
WHERE [Description] = @Description)

IF @Count = 0
BEGIN
if(@DashFile = '')
BEGIN
INSERT INTO [dbo].tblMS_Dashboards
           ([Description]
		    ,[IsSubDash]
			,[SubDashKey]
			,[TopDashID]
			,[ModuleID])
     VALUES
           (@Description,@IsSubDash,@SubDashKey,@TopDashID,@ModuleID)
SET @newDashID = (SELECT SCOPE_IDENTITY());

IF @IsSubDash = 0
BEGIN
INSERT INTO tblMS_DashProfileLink
SELECT @newDashID,ProfileID,case when Linked is null then cast(0 as bit) else Linked End Linked,
CASE WHEN ShowOnStartup IS NULL THEN CAST(0 AS BIT) ELSE ShowOnStartup END ShowOnStartup FROM @DashProfiles

INSERT INTO [dbo].[tblMS_DashConnectionLink]
SELECT @newDashID,ConnectionID,CASE WHEN Linked IS NULL THEN CAST(0 AS BIT) ELSE Linked END Linked FROM @DashConnections
END

SELECT 'DONE' theMessage,@newDashID DashID
END
ELSE
BEGIN
SET @SQL = 'INSERT INTO [dbo].tblMS_Dashboards
           ([Description]
           ,[DashFile]
		   ,[IsSubDash]
		   ,[SubDashKey]
		   ,[TopDashID]
		   ,[ModuleID])
     VALUES
           (''' + cast(@Description as varchar(max)) + '''
           ,(SELECT *    
    FROM  ''' +  cast(@DashFile as varchar(max)) + ''',
		' +  cast(@IsSubDash as varchar(max)) + ',''' + cast(@SubDashKey as varchar(max)) + ',' + cast(@TopDashID as varchar(max)) + ',' + cast(@ModuleID as varchar(max))+ ')'
	EXEC( @SQL)
SET @newDashID = (SELECT SCOPE_IDENTITY());

IF @IsSubDash = 0
BEGIN
INSERT INTO tblMS_DashProfileLink
SELECT @newDashID,ProfileID,case when Linked is null then cast(0 as bit) else Linked End Linked,
CASE WHEN  ShowOnStartup IS NULL THEN CAST(0 AS BIT) ELSE ShowOnStartup END ShowOnStartup FROM @DashProfiles

INSERT INTO [dbo].[tblMS_DashConnectionLink]
SELECT @newDashID,ConnectionID,CASE WHEN Linked IS NULL THEN CAST(0 AS BIT) ELSE Linked END Linked FROM @DashConnections
END

SELECT 'DONE' theMessage,@newDashID DashID

END
END -- end If @Count = 0
ELSE
BEGIN
SELECT 'Connection name already exists' theMessage,-1 DashID
END

END -- end IF @ManageType = 1

IF @ManageType = 2
BEGIN

IF(@DashFile = '')
BEGIN
UPDATE [dbo].tblMS_Dashboards
   SET [Description] = @Description,
       [IsSubDash] = @IsSubDash,
	   [SubDashKey] = @SubDashKey,
	   [TopDashID] = @TopDashID,
	   [ModuleID] = @ModuleID
 WHERE DashID = @DashID

IF @IsSubDash = 0
BEGIN
DELETE FROM tblMS_DashProfileLink where DashID = @DashID
INSERT INTO tblMS_DashProfileLink
SELECT @DashID,ProfileID,case when Linked is null then cast(0 as bit) else Linked End Linked,
CASE WHEN ShowOnStartup IS NULL THEN CAST(0 AS BIT) ELSE ShowOnStartup END ShowOnStartup FROM @DashProfiles

DELETE FROM [tblMS_DashConnectionLink] WHERE DashID = @DashID
INSERT INTO [dbo].[tblMS_DashConnectionLink]
SELECT @DashID,ConnectionID,CASE WHEN Linked IS NULL THEN CAST(0 AS BIT) ELSE Linked END Linked FROM @DashConnections
END

SELECT 'DONE' theMessage,@DashID DashID

END -- end if(@DashFile = '')
ELSE
BEGIN
SET @SQL = 'UPDATE [dbo].tblMS_Dashboards
   SET [Description] = ''' + CAST(@Description AS VARCHAR(MAX)) + ''',
       [IsSubDash] =''' + CAST(@IsSubDash AS VARCHAR(MAX)) + ''',
	   [SubDashKey] =''' + CAST(@SubDashKey AS VARCHAR(MAX)) + ''',
	   [TopDashID] =''' + CAST(@TopDashID AS VARCHAR(MAX)) + ''',
	   [ModuleID] =''' + CAST(@ModuleID AS VARCHAR(MAX)) + ''',
       [DashFile] =  ''' +  CAST(@DashFile AS VARCHAR(MAX)) + '''
 WHERE DashID = ' + CAST(@DashID AS VARCHAR(MAX))
EXEC (@SQL)

IF @IsSubDash = 0
BEGIN
DELETE FROM tblMS_DashProfileLink WHERE DashID = @DashID
INSERT INTO tblMS_DashProfileLink
SELECT @DashID,ProfileID,CASE WHEN Linked IS NULL THEN CAST(0 AS BIT) ELSE Linked END Linked,
CASE WHEN ShowOnStartup IS NULL THEN CAST(0 AS BIT) ELSE ShowOnStartup END ShowOnStartup FROM @DashProfiles

DELETE FROM [tblMS_DashConnectionLink] WHERE DashID = @DashID
INSERT INTO [dbo].[tblMS_DashConnectionLink]
SELECT @DashID,ConnectionID,CASE WHEN Linked IS NULL THEN CAST(0 AS BIT) ELSE Linked END Linked FROM @DashConnections	
END

SELECT 'DONE' theMessage,@DashID DashID
END


END -- end @ManageType = 2

IF @ManageType = 3
BEGIN

DELETE FROM dbo.tblMS_DashProfileLink WHERE DashID = @DashID
DELETE FROM dbo.tblMS_DashConnectionLink WHERE DashID = @DashID
DELETE FROM dbo.tblMS_Dashboards WHERE DashID = @DashID

SELECT 'DONE' theMessage,@DashID DashID
END -- end @ManageType = 3

END
go
update tblsysset set systemtimeout = 30


