Declare @Mine varchar(100)
Set @Mine = 'PAS_dnk'

MERGE Pas_Central.[dbo].[OreFlowEntities] AS t
USING (

    Select Distinct [OreFlowID],[OreFlowCode],[Name],[ParentOreFlowID],[LevelNumber],[Capacity],[Cur_Tons],[Cur_Grade],[X1],[Y1],[X2],[Y2],[SX],[SY],[Inactive],[TFBox],[SectionID],[HopperFactor],
    [Surface],[Mine],[ReefType],[CrossTram],[BoxDistance],[SkipFactor],[Ug2SkipFactor],[WasteSkipFactor],[Division],[CostArea], @Mine Mine2
    FROM [dbo].[OreFlowEntities]

) as s
ON ( t.[Mine2] = s.[Mine2] AND t.[OreFlowID] = s.[OreFlowID] )
WHEN MATCHED THEN UPDATE SET
    [OreFlowCode] = s.[OreFlowCode],
    [Name] = s.[Name],
    [ParentOreFlowID] = s.[ParentOreFlowID],
    [LevelNumber] = s.[LevelNumber],
    [Capacity] = s.[Capacity],
    [Cur_Tons] = s.[Cur_Tons],
    [Cur_Grade] = s.[Cur_Grade],
    [X1] = s.[X1],
    [Y1] = s.[Y1],
    [X2] = s.[X2],
    [Y2] = s.[Y2],
    [SX] = s.[SX],
    [SY] = s.[SY],
    [Inactive] = s.[Inactive],
    [TFBox] = s.[TFBox],
    [SectionID] = s.[SectionID],
    [HopperFactor] = s.[HopperFactor],
    [Surface] = s.[Surface],
    [Mine] = s.[Mine],
    [ReefType] = s.[ReefType],
    [CrossTram] = s.[CrossTram],
    [BoxDistance] = s.[BoxDistance],
    [SkipFactor] = s.[SkipFactor],
    [Ug2SkipFactor] = s.[Ug2SkipFactor],
    [WasteSkipFactor] = s.[WasteSkipFactor],
    [Division] = s.[Division],
    [CostArea] = s.[CostArea]
 WHEN NOT MATCHED BY TARGET THEN
    INSERT([OreFlowID], [OreFlowCode], [Name], [ParentOreFlowID], [LevelNumber], [Capacity], [Cur_Tons], [Cur_Grade], [X1], [Y1], [X2], [Y2], [SX], [SY], [Inactive], [TFBox], [SectionID], [HopperFactor], [Surface], [Mine], [ReefType], [CrossTram], [BoxDistance], [SkipFactor], [Ug2SkipFactor], [WasteSkipFactor], [Division], [CostArea], [Mine2])
    VALUES(s.[OreFlowID], s.[OreFlowCode], s.[Name], s.[ParentOreFlowID], s.[LevelNumber], s.[Capacity], s.[Cur_Tons], s.[Cur_Grade], s.[X1], s.[Y1], s.[X2], s.[Y2], s.[SX], s.[SY], s.[Inactive], s.[TFBox], s.[SectionID], s.[HopperFactor], s.[Surface], s.[Mine], s.[ReefType], s.[CrossTram], s.[BoxDistance], s.[SkipFactor], s.[Ug2SkipFactor], s.[WasteSkipFactor], s.[Division], s.[CostArea], s.[Mine2]);

MERGE PAS_Central.dbo.[Workplace] AS t
USING (
    Select Distinct [WorkplaceID],[OreFlowID],[ReefID],[EndTypeID],[Description],[Activity],[ReefWaste],[StopingCode],[EndWidth],[EndHeight],[Line],[Direction],[Priority],[Mech],[Cap],[RiskRating],
	   [DefaultAdv],[GMSIWPID],[DivisionCode],[TypeCode],[GridCode],[DetailCode],[NumberCode],[DescrCode],[DescrNoCode],[Inactive],[Density],[BrokenRockDensity],[SubSection],[ProcessCode],
	   [Classification],[WPStatus],[CreationDate],[Converted],[OldWorkplaceid],[Userid],@Mine Mine
    From Workplace
    ) as s
ON ( t.[Mine] = s.[Mine] AND t.[WorkplaceID] = s.[WorkplaceID] )
WHEN MATCHED THEN UPDATE SET
    [OreFlowID] = s.[OreFlowID],
    [ReefID] = s.[ReefID],
    [EndTypeID] = s.[EndTypeID],
    [Description] = s.[Description],
    [Activity] = s.[Activity],
    [ReefWaste] = s.[ReefWaste],
    [StopingCode] = s.[StopingCode],
    [EndWidth] = s.[EndWidth],
    [EndHeight] = s.[EndHeight],
    [Line] = s.[Line],
    [Direction] = s.[Direction],
    [Priority] = s.[Priority],
    [Mech] = s.[Mech],
    [Cap] = s.[Cap],
    [RiskRating] = s.[RiskRating],
    [DefaultAdv] = s.[DefaultAdv],
    [GMSIWPID] = s.[GMSIWPID],
    [DivisionCode] = s.[DivisionCode],
    [TypeCode] = s.[TypeCode],
    [GridCode] = s.[GridCode],
    [DetailCode] = s.[DetailCode],
    [NumberCode] = s.[NumberCode],
    [DescrCode] = s.[DescrCode],
    [DescrNoCode] = s.[DescrNoCode],
    [Inactive] = s.[Inactive],
    [Density] = s.[Density],
    [BrokenRockDensity] = s.[BrokenRockDensity],
    [SubSection] = s.[SubSection],
    [ProcessCode] = s.[ProcessCode],
    [Classification] = s.[Classification],
    [WPStatus] = s.[WPStatus],
    [CreationDate] = s.[CreationDate],
    [Converted] = s.[Converted],
    [OldWorkplaceid] = s.[OldWorkplaceid],
    [Userid] = s.[Userid]
 WHEN NOT MATCHED BY TARGET THEN
    INSERT([WorkplaceID], [OreFlowID], [ReefID], [EndTypeID], [Description], [Activity], [ReefWaste], [StopingCode], [EndWidth], [EndHeight], [Line], [Direction], [Priority], [Mech], [Cap], [RiskRating], [DefaultAdv], [GMSIWPID], [DivisionCode], [TypeCode], [GridCode], [DetailCode], [NumberCode], [DescrCode], [DescrNoCode], [Inactive], [Density], [BrokenRockDensity], [SubSection], [ProcessCode], [Classification], [WPStatus], [CreationDate], [Converted], [OldWorkplaceid], [Userid], [Mine])
    VALUES(s.[WorkplaceID], s.[OreFlowID], s.[ReefID], s.[EndTypeID], s.[Description], s.[Activity], s.[ReefWaste], s.[StopingCode], s.[EndWidth], s.[EndHeight], s.[Line], s.[Direction], s.[Priority], s.[Mech], s.[Cap], s.[RiskRating], s.[DefaultAdv], s.[GMSIWPID], s.[DivisionCode], s.[TypeCode], s.[GridCode], s.[DetailCode], s.[NumberCode], s.[DescrCode], s.[DescrNoCode], s.[Inactive], s.[Density], s.[BrokenRockDensity], s.[SubSection], s.[ProcessCode], s.[Classification], s.[WPStatus], s.[CreationDate], s.[Converted], s.[OldWorkplaceid], s.[Userid], s.[Mine]);