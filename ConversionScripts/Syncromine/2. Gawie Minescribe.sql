-- =============================================
-- Author:		Gawie Schneider
-- Create date: 2017/09/19
-- Description:	Select data for MS SystemConnection
-- =============================================
-- Changed Date		| Description
-- yyyy/MM/dd		| Some Description
-- =============================================
CREATE PROCEDURE [dbo].[sp_Select_tblMS_SystemConnection] 
	@Connectionid int,
	@System varchar(50)
AS
BEGIN	
	--DECLARE @Connectionid int,
	--		@System varchar(50)
	--SET @Connectionid = 16
	--SET @System = 'Doornkop'
	
	Select Id, ConnectionId, [System], ConnectionString, IsSelected
	FROM tblMS_SystemConnection sc
	WHERE sc.ConnectionId = @Connectionid
	AND (ISNULL(@System, '') = '' OR sc.[System] = @System)
END
GO

CREATE TABLE [dbo].[tblMS_SystemConnection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConnectionId] [int] NOT NULL,
	[System] [varchar](50) NULL,
	[ConnectionString] [varchar](max) NULL,
	[IsSelected] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- =============================================
-- Author:		Gawie Schneider
-- Create date: 2017/09/19
-- Description:	Insert Or Update System Connections for Minescribe
-- =============================================
-- Changed Date		| Description
-- yyyy/MM/dd		| Some Description
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertUpd_tblMS_SystemConnection] 
	@ConnectionId int,
	@System varchar(50),
	@ConnectionString varchar(max),
	@IsSelected bit
AS
BEGIN

	--DECLARE @ConnectionId int,
	--@System VarChar(50),
	--@ConnectionString VarChar(max),
	--@IsSelected bit
	--SET @ConnectionId = '16'
	--SET @System = 'Doornkop'
	--SET @ConnectionString = 'MyConnection'
	--SET @IsSelected = 1	

	--Insert / Update using Merge
	MERGE dbo.tblMS_SystemConnection connections
	USING (SELECT @ConnectionId ConnectionId,
				@System [System],
				@ConnectionString ConnectionString,
				@IsSelected IsSelected
	) match
		ON connections.[ConnectionId] = match.[ConnectionId]
		AND connections.[System] = match.[System]
	WHEN MATCHED THEN
		--Only Update what is required
		UPDATE SET connections.ConnectionId = match.ConnectionId,
					connections.[System] = match.[System],
					connections.ConnectionString = match.ConnectionString,
					connections.IsSelected = match.IsSelected
	WHEN NOT MATCHED THEN
	INSERT 
        --Insert all values if not found
		VALUES (@ConnectionId
			,@System
			,@ConnectionString
			,@IsSelected
			);
END
GO