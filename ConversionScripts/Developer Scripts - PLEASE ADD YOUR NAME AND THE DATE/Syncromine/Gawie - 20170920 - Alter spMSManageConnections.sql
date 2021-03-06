-- =============================================
-- Author:		Dolf van den Berg
-- Create date: 2015-07-10
-- Description:	Manage connections
-- =============================================
-- Changed Date		| Description
-- 2017/09/20		| Edit & Delete is never done even if no link is available
--					| Delete has to include the SystemConnections
-- =============================================
ALTER PROCEDURE [dbo].[spMSManageConnections]
	-- Add the parameters for the stored procedure here
	@ConnectionID int,
	@Description varchar(100),
	@ConnectionString varchar(200),
	@SQLScript varchar(max),
	@ManageType int -- 1 = new, 2 = Edit, 3 = Delete
AS
DECLARE @Count int;
BEGIN

	IF @ManageType = 1
	BEGIN

		SET @Count = (SELECT count([Description]) FROM [dbo].[tblMS_Connections]
		WHERE [Description] = @Description)

		If @Count = 0
		BEGIN

			INSERT INTO [dbo].[tblMS_Connections]
					   ([Description]
					   ,[ConnectionString]
					   ,[SQLScript])
				 VALUES
					   (@Description
					   ,@ConnectionString
					   ,@SQLScript)

		END -- end If @Count = 0
		ELSE
		BEGIN
		SELECT 'Connection name already exists' theMessage
		END

	END -- end IF @ManageType = 1

	IF @ManageType = 2
	BEGIN

		SET @Count = (SELECT count(ConnectionID) FROM tblMS_DashConnectionLink where ConnectionID = @ConnectionID AND Linked = 1)
		If @Count = 0
		BEGIN
			UPDATE [dbo].[tblMS_Connections]
			   SET [Description] = @Description
				  ,[ConnectionString] = @ConnectionString
				  ,[SQLScript] = @SQLScript
			 WHERE ConectionID = @ConnectionID
		END -- end If @Count = 0
		ELSE 
		BEGIN
			SELECT 'The Connection is in use. Please remove link first' theMessage
		END

	END -- end @ManageType = 2

	IF @ManageType = 3
	BEGIN

		SET @Count = (SELECT count(ConnectionID) FROM tblMS_DashConnectionLink where ConnectionID = @ConnectionID AND Linked = 1)
		If @Count = 0
		BEGIN
			DELETE FROM [dbo].[tblMS_Connections]
			 WHERE ConectionID = @ConnectionID

			DELETE FROM [dbo].[tblMS_SystemConnection]
			 WHERE ConnectionId = @ConnectionID
		END -- end If @Count = 0
		ELSE 
		BEGIN
			SELECT 'The Connection is in use. Please remove link first' theMessage
		END

	END -- end @ManageType = 3

END