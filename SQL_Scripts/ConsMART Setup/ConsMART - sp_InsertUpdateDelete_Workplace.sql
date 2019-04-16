-- =============================================
-- Author:		Gawie Schneider
-- Create date:	2018/02/12
-- Description:	Inserts new Records, Updates Existing Records, Deletes records that were not found
-- =============================================
-- Changed Date	| Changed By		| Description
-- yyyy/MM/dd		| Name Surname		| Some Description
-- =============================================
Create Procedure sp_InsertUpdateDelete_Workplace
	   @Data WorkplaceType READONLY
As
Begin
    MERGE [dbo].[Workplace] AS t
    USING @Data as s ON ( t.[Operation] = s.[Operation] AND t.[WorkplaceID] = s.[WorkplaceID] )

--UPDATES EXISTING RECORDS
    WHEN MATCHED THEN UPDATE SET
	   [Description] = s.[Description],
	   [DivisionCode] = s.[DivisionCode],
	   [DivisionDescription] = s.[DivisionDescription],
	   [TypeCode] = s.[TypeCode],
	   [TypeDescription] = s.[TypeDescription],
	   [OreFlowID] = s.[OreFlowID],
	   [LevelNumber] = s.[LevelNumber],
	   [LevelDescription] = s.[LevelDescription],
	   [GridCode] = s.[GridCode],
	   [GridDescription] = s.[GridDescription],
	   [DetailCode] = s.[DetailCode],
	   [DetailDescription] = s.[DetailDescription],
	   [NumberCode] = s.[NumberCode],
	   [NumberDescription] = s.[NumberDescription],
	   [DirectionCode] = s.[DirectionCode],
	   [DirectionDescription] = s.[DirectionDescription],
	   [DescrCode] = s.[DescrCode],
	   [DescrDescription] = s.[DescrDescription],
	   [DescrNoCode] = s.[DescrNoCode],
	   [DescrNoDescription] = s.[DescrNoDescription],
	   [ReefID] = s.[ReefID],
	   [ReefName] = s.[ReefName],
	   [ReefDescription] = s.[ReefDescription],
	   [Converted] = s.[Converted],
	   [WPStatus] = s.[WPStatus],
	   [WPStatusDescription] = s.[WPStatusDescription],
	   [CreationDate] = s.[CreationDate],
	   [Classification] = s.[Classification],
	   [ClassificationDescription] = s.[ClassificationDescription],
	   [OldWorkplaceid] = s.[OldWorkplaceid],
	   [EndTypeID] = s.[EndTypeID],
	   [Activity] = s.[Activity],
	   [ReefWaste] = s.[ReefWaste],
	   [StopingCode] = s.[StopingCode],
	   [EndWidth] = s.[EndWidth],
	   [EndHeight] = s.[EndHeight],
	   [Line] = s.[Line],
	   [Priority] = s.[Priority],
	   [Mech] = s.[Mech],
	   [Cap] = s.[Cap],
	   [RiskRating] = s.[RiskRating],
	   [DefaultAdv] = s.[DefaultAdv],
	   [GMSIWPID] = s.[GMSIWPID],
	   [Inactive] = s.[Inactive],
	   [Density] = s.[Density],
	   [BrokenRockDensity] = s.[BrokenRockDensity],
	   [SubSection] = s.[SubSection],
	   [ProcessCode] = s.[ProcessCode],
	   [Userid] = s.[Userid],
	   [BoxholeID] = s.[BoxholeID]

--INSERT NEW RECORDS
    WHEN NOT MATCHED BY TARGET THEN
	   INSERT([Operation], [WorkplaceID], [Description], [DivisionCode], [DivisionDescription], [TypeCode], [TypeDescription], [OreFlowID], [LevelNumber], [LevelDescription], [GridCode], [GridDescription], [DetailCode], [DetailDescription], [NumberCode], [NumberDescription], [DirectionCode], [DirectionDescription], [DescrCode], [DescrDescription], [DescrNoCode], [DescrNoDescription], [ReefID], [ReefName], [ReefDescription], [Converted], [WPStatus], [WPStatusDescription], [CreationDate], [Classification], [ClassificationDescription], [OldWorkplaceid], [EndTypeID], [Activity], [ReefWaste], [StopingCode], [EndWidth], [EndHeight], [Line], [Priority], [Mech], [Cap], [RiskRating], [DefaultAdv], [GMSIWPID], [Inactive], [Density], [BrokenRockDensity], [SubSection], [ProcessCode], [Userid], [BoxholeID])
	   VALUES(s.[Operation], s.[WorkplaceID], s.[Description], s.[DivisionCode], s.[DivisionDescription], s.[TypeCode], s.[TypeDescription], s.[OreFlowID], s.[LevelNumber], s.[LevelDescription], s.[GridCode], s.[GridDescription], s.[DetailCode], s.[DetailDescription], s.[NumberCode], s.[NumberDescription], s.[DirectionCode], s.[DirectionDescription], s.[DescrCode], s.[DescrDescription], s.[DescrNoCode], s.[DescrNoDescription], s.[ReefID], s.[ReefName], s.[ReefDescription], s.[Converted], s.[WPStatus], s.[WPStatusDescription], s.[CreationDate], s.[Classification], s.[ClassificationDescription], s.[OldWorkplaceid], s.[EndTypeID], s.[Activity], s.[ReefWaste], s.[StopingCode], s.[EndWidth], s.[EndHeight], s.[Line], s.[Priority], s.[Mech], s.[Cap], s.[RiskRating], s.[DefaultAdv], s.[GMSIWPID], s.[Inactive], s.[Density], s.[BrokenRockDensity], s.[SubSection], s.[ProcessCode], s.[Userid], s.[BoxholeID])

--Note the Delete Statement if the record was not found in source
--DELETES RECORDS NOT FOUND IN SOURCE
    WHEN NOT MATCHED BY SOURCE THEN DELETE; 

End;